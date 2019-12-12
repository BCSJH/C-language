using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
namespace MultiChatClient
{
    public partial class ChatForm : Form
    {
        delegate void DsetLabel(string data);//스레드 오류 때문에 선언

        delegate void AppendTextDelegate(Control ctrl, string s);
        AppendTextDelegate _textAppender;
        Socket mainSock;
        IPAddress thisAddress;

        string nameID;// 자신이 입력한 ID
        List<string> check_name = new List<string>(); // 사용자 이름 체크
        List<int> check_card_number = new List<int>();// 2개 선택시 메시지 서버로 메세지 보내기 위한 리스트
        List<int> card_list = new List<int>();//random 카트의 리스트
        List<int> card_check = new List<int>(); // 맞춘 카드 체크 리스트
        int clientNum = 0;

        public ChatForm()
        {
            InitializeComponent();
            mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _textAppender = new AppendTextDelegate(AppendText);
        }

        void AppendText(Control ctrl, string s)
        {
            if (ctrl.InvokeRequired) ctrl.Invoke(_textAppender, ctrl, s);
            else {
                string source = ctrl.Text;
                ctrl.Text = source + Environment.NewLine + s;
            }
        }

        void OnFormLoaded(object sender, EventArgs e)
        {
            if (thisAddress == null)
            {
                // 로컬호스트 주소를 사용한다.
                thisAddress = IPAddress.Loopback;
                txtAddress.Text = thisAddress.ToString();
            }
            else
            {
                thisAddress = IPAddress.Parse(txtAddress.Text);
            }
        }

        void OnConnectToServer(object sender, EventArgs e)
        {
            if (mainSock.Connected)
            {
                MsgBoxHelper.Error("이미 연결되어 있습니다!");
                return;
            }

            int port = 15000;  //고정

            nameID = txtID.Text.Trim(); //ID
            if (string.IsNullOrEmpty(nameID))
            {
                MsgBoxHelper.Warn("ID가 입력되지 않았습니다!");
                txtID.Focus();
                return;
            }

            // 서버에 연결
            try
            {
                mainSock.Connect(txtAddress.Text, port);
            }
            catch (Exception ex)
            {
                MsgBoxHelper.Error("연결에 실패했습니다!\n오류 내용: {0}", MessageBoxButtons.OK, ex.Message);
                return;
            }

            // 서버로 ID 전송
            SendID();

            // 연결 완료, 서버에서 데이터가 올 수 있으므로 수신 대기한다.
            AsyncObject obj = new AsyncObject(4096);
            obj.WorkingSocket = mainSock;
            mainSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
        }
        void SendID()
        {
            // 문자열을 utf8 형식의 바이트로 변환한다.
            byte[] bDts = Encoding.UTF8.GetBytes("ID:" + nameID + ":");

            // 서버에 전송한다.
            mainSock.Send(bDts);

            // 연결 완료되었다는 메세지를 띄워준다.
            AppendText(txtHistory, string.Format("서버와 연결되었습니다.\n " +
                "특정 사용자에게 보낼 경우 ComboBox에서 (사용자) 선택하시고\n" +
                "브로드캐스트하려면 ComboBox에서 (BR) 선택해주세요.\n" +
                "게임은 사용자가 3명이 모였을 때 시작합니다."
                ));

        }

        void DataReceived(IAsyncResult ar)
        {
            // BeginReceive에서 추가적으로 넘어온 데이터를 AsyncObject 형식으로 변환한다.
            AsyncObject obj = (AsyncObject)ar.AsyncState;

            try
            {
                // 데이터 수신을 끝낸다.
                int received = obj.WorkingSocket.EndReceive(ar);
                if (received <= 0)
                {
                    obj.WorkingSocket.Disconnect(false);
                    obj.WorkingSocket.Close();
                    return;
                }
            }
            catch { }

            // 받은 데이터가 없으면(연결끊어짐) 끝낸다.


            // 텍스트로 변환한다.
            string text = Encoding.UTF8.GetString(obj.Buffer);

            // : 기준으로 짜른다.
            // tokens[0] - 보낸 사람 ID
            // tokens[1] - 보낸 메세지
            string[] tokens = text.Split(':');
            if (tokens[0].Equals("ID"))

            {// 새로 접속한 클라이언트가 "id:자신의_ID" 전송함
                string id = tokens[1];
                check_name.Add(id);
                AppendText(txtHistory, string.Format("[접속] ID: {0}", id));
            }

            //서버로 부터 메세지 받았을 경우
            else if (tokens[0].Equals("Server"))
            {
                string msg = tokens[1];
                AppendText(txtHistory, string.Format("[공지]: {0}", msg));

            }
            //전체 메세지 받았을 경우
            else if (tokens[0].Equals("BR"))
            {
                string fromID = tokens[1];
                string msg = tokens[2];
                AppendText(txtHistory, string.Format("[전체]{0}: {1}", fromID, msg));
            }

            //귓속말 받았을 경우
            else if (tokens[0].Equals("TO"))
            {
                string fromID = tokens[1];
                string toID = tokens[2];
                string msg = tokens[3];
                string receivedMsg = "[FROM:" + fromID + "][TO:" + toID + "]" + msg;
                AppendText(txtHistory, receivedMsg);
            }

            //접속을 1명했다면 clientName : 1 : 1 :;
            //접속을 2명했다면 clientName : 2 : 1 : 2 :;


            //Client가 보낸 답이 맞았을 경우
            if (tokens[0].Equals("PREO"))
            {
                string fromID = tokens[1];
                AppendText(txtHistory, string.Format("[정답]{0}:{1}:{2}", fromID, tokens[2],tokens[3]));
                button_color_change_O(Int32.Parse(tokens[2])); card_check.Add(Int32.Parse(tokens[2]));  //버튼 색깔 바꾸기
                button_color_change_O(Int32.Parse(tokens[3])); card_check.Add(Int32.Parse(tokens[3]));  
                if (card_check.Count == 16)
                {
                    byte[] bDts = Encoding.UTF8.GetBytes("ESC:");
                    mainSock.Send(bDts);
                }
            }

            //Client가 보낸 답이 틀렸을 경우
            if (tokens[0].Equals("PREX"))
            {
                string fromID = tokens[1];
                AppendText(txtHistory, string.Format("[오답]{0}:{1}:{2}", fromID, tokens[2], tokens[3]));
                button_color_change_X(Int32.Parse(tokens[2]));  //버튼 색깔 바꾸기
                button_color_change_X(Int32.Parse(tokens[3]));
            }
            
            if (tokens[0].Equals("ClientName"))//listView에 추가하기
            {
                this.Invoke(new Action(delegate ()
                {
                    listView1.Clear();
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("BR");
                }));
                clientNum = int.Parse(tokens[1]);//접속한 인원 수
                for (int i = 0; i < clientNum; i++)
                {
                    string clientName = tokens[2 + i];
                    this.Invoke(new Action(delegate ()
                    {
                        listView1.Items.Add(clientName);

                    }));
                    this.Invoke(new Action(delegate ()
                    {
                        comboBox1.Items.Add(clientName);
                    }));
                }
                this.Invoke(new Action(delegate ()
                {
                    comboBox1.SelectedIndex = 0;
                }));
            }
            if (tokens[0].Equals("Card"))//listView에 추가하기
            {
                for (int i = 0; i < 16; i++)//(1~7까의 수 * 2) 14개
                {
                    int card = int.Parse(tokens[i + 1]);//카드 값
                    card_list.Add(card);
                }
                this.Invoke(new Action(delegate ()
                {
                    button1.Text = "" + card_list[0];
                    button2.Text = "" + card_list[1];
                    button3.Text = "" + card_list[2];
                    button4.Text = "" + card_list[3];
                    button5.Text = "" + card_list[4];
                    button6.Text = "" + card_list[5];
                    button7.Text = "" + card_list[6];
                    button8.Text = "" + card_list[7];
                    button9.Text = "" + card_list[8];
                    button10.Text = "" + card_list[9];
                    button11.Text = "" + card_list[10];
                    button12.Text = "" + card_list[11];
                    button13.Text = "" + card_list[12];
                    button14.Text = "" + card_list[13];
                    button15.Text = "" + card_list[14];
                    button16.Text = "" + card_list[15];

                }));
                Thread.Sleep(5000);
                this.Invoke(new Action(delegate ()
                {
                    button1.Text = "1";
                    button2.Text = "2";
                    button3.Text = "3";
                    button4.Text = "4";
                    button5.Text = "5";
                    button6.Text = "6";
                    button7.Text = "7";
                    button8.Text = "8";
                    button9.Text = "9";
                    button10.Text = "10";
                    button11.Text = "11";
                    button12.Text = "12";
                    button13.Text = "13";
                    button14.Text = "14";
                    button15.Text = "15";
                    button16.Text = "16";
                }));
            }
            if (tokens[0].Equals("ESC"))
            {
                AppendText(txtHistory, string.Format("[게임종료]"));
                if (MessageBox.Show("게임을 더 하시겠습니까?", "게임종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //초-기-화
                    check_name.Clear();
                    check_card_number.Clear();
                    card_check.Clear();
                    card_list.Clear();
                    for (int i = 1; i < 17; i++) { button_color_change_X(i); }
                    SendID();
                }
                else
                {
                    Application.Exit();
                }
            }
            // 텍스트박스에 추가해준다.
            // 비동기식으로 작업하기 때문에 폼의 UI 스레드에서 작업을 해줘야 한다.
            // 따라서 대리자를 통해 처리한다.


            // 클라이언트에선 데이터를 전달해줄 필요가 없으므로 바로 수신 대기한다.
            // 데이터를 받은 후엔 다시 버퍼를 비워주고 같은 방법으로 수신을 대기한다.
            obj.ClearBuffer();

            // 수신 대기
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
        }
        void button_color_change_O(int i) {
            switch (i)
            {
                case 1:
                    button1.BackColor = System.Drawing.Color.Black; break;
                case 2:
                    button2.BackColor = System.Drawing.Color.Black; break;
                case 3:
                    button3.BackColor = System.Drawing.Color.Black; break;
                case 4:
                    button4.BackColor = System.Drawing.Color.Black; break;
                case 5:
                    button5.BackColor = System.Drawing.Color.Black; break;
                case 6:
                    button6.BackColor = System.Drawing.Color.Black; break;
                case 7:
                    button7.BackColor = System.Drawing.Color.Black; break;
                case 8:
                    button8.BackColor = System.Drawing.Color.Black; break;
                case 9:
                    button9.BackColor = System.Drawing.Color.Black; break;
                case 10:
                    button10.BackColor = System.Drawing.Color.Black; break;
                case 11:
                    button11.BackColor = System.Drawing.Color.Black; break;
                case 12:
                    button12.BackColor = System.Drawing.Color.Black; break;
                case 13:
                    button13.BackColor = System.Drawing.Color.Black; break;
                case 14:
                    button14.BackColor = System.Drawing.Color.Black; break;
                case 15:
                    button15.BackColor = System.Drawing.Color.Black; break;
                case 16:
                    button16.BackColor = System.Drawing.Color.Black; break;

            }
        }

        void button_color_change_X(int i)
        {
            switch (i)
            {
                case 1:
                    button1.BackColor = System.Drawing.Color.LightGray; break;
                case 2:
                    button2.BackColor = System.Drawing.Color.LightGray; break;
                case 3:
                    button3.BackColor = System.Drawing.Color.LightGray; break;
                case 4:
                    button4.BackColor = System.Drawing.Color.LightGray; break;
                case 5:
                    button5.BackColor = System.Drawing.Color.LightGray; break;
                case 6:
                    button6.BackColor = System.Drawing.Color.LightGray; break;
                case 7:
                    button7.BackColor = System.Drawing.Color.LightGray; break;
                case 8:
                    button8.BackColor = System.Drawing.Color.LightGray; break;
                case 9:
                    button9.BackColor = System.Drawing.Color.LightGray; break;
                case 10:
                    button10.BackColor = System.Drawing.Color.LightGray; break;
                case 11:
                    button11.BackColor = System.Drawing.Color.LightGray; break;
                case 12:
                    button12.BackColor = System.Drawing.Color.LightGray; break;
                case 13:
                    button13.BackColor = System.Drawing.Color.LightGray; break;
                case 14:
                    button14.BackColor = System.Drawing.Color.LightGray; break;
                case 15:
                    button15.BackColor = System.Drawing.Color.LightGray; break;
                case 16:
                    button16.BackColor = System.Drawing.Color.LightGray; break;

            }
        }
        void OnSendData(object sender, EventArgs e)
        {
            // 서버가 대기중인지 확인한다.
            if (!mainSock.IsBound)
            {
                MsgBoxHelper.Warn("서버가 실행되고 있지 않습니다!");
                return;
            }

            // 보낼 텍스트
            string tts = txtTTS.Text.Trim();
            if (string.IsNullOrEmpty(tts))
            {
                MsgBoxHelper.Warn("텍스트가 입력되지 않았습니다!");
                txtTTS.Focus();
                return;
            }

            // string[] tokens = tts.Split(':');
            byte[] bDts = new byte[4096];
            //AppendText(txtHistory, tokens.Length.ToString());
            AppendText(txtHistory, tts);

            if (comboBox1.SelectedItem.Equals("BR"))
            {
                bDts = Encoding.UTF8.GetBytes("BR:" + nameID + ':' + tts + ':');
                AppendText(txtHistory, string.Format("[전체전송]{0}", tts));
            }
            else
            {
                // 문자열을 utf8 형식의 바이트로 변환한다.
                bDts = Encoding.UTF8.GetBytes("TO:" + nameID + ':' + comboBox1.SelectedItem + ':' + tts + ':');
                AppendText(txtHistory, string.Format("[{0}에게 보냄]:{1}", comboBox1.SelectedItem, tts));
            }


            // 서버에 전송한다.
            mainSock.Send(bDts);

            // 전송 완료 후 텍스트박스에 추가하고, 원래의 내용은 지운다.
            //AppendText(txtHistory, string.Format("[보냄]{0}:{1}", nameID, tts));
            txtTTS.Clear();
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainSock != null)
            {
                mainSock.Disconnect(false);
                mainSock.Close();
            }

        }
        void button_click() {
            if (card_check.Count == 16)
            {
                AppendText(txtHistory, string.Format("[게임종료]"));
                if (MessageBox.Show("게임을 더 하시겠습니까?", "게임종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //초-기-화
                    check_name.Clear();
                    check_card_number.Clear();
                    card_check.Clear();
                    card_list.Clear();
                    for (int i = 1; i < 17; i++) { button_color_change_X(i); }
                    SendID();
                }
                else
                {
                    Application.Exit();
                }
            }
            if (check_card_number.Count == 2)
            {
                if (clientNum == 3)
                {
                    byte[] bDts = new byte[4096];
                    bDts = Encoding.UTF8.GetBytes("PRE:" + nameID + ':' + check_card_number[0] + ':' + check_card_number[1]);
                    AppendText(txtHistory, string.Format("선택 : {0}, {1}", check_card_number[0], check_card_number[1]));
                    mainSock.Send(bDts);//서버에 보내기
                }
                else
                {
                    MessageBox.Show("아직 게임이 시작되지 않았습니다.");
                    button_color_change_X(check_card_number[0]);
                    button_color_change_X(check_card_number[1]);
                }
                
                check_card_number.Clear();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.BackColor == System.Drawing.Color.DarkGray)
            {
                button1.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(1));
            }
            else if (button1.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button1.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(1);
            }
            button_click();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.BackColor == System.Drawing.Color.DarkGray)
            {
                button2.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(2));
            }
            else if (button2.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button2.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(2);
            }
            button_click();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.BackColor == System.Drawing.Color.DarkGray)
            {
                button3.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(3));
            }
            else if (button3.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button3.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(3);
            }
            button_click();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.BackColor == System.Drawing.Color.DarkGray)
            {
                button4.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(4));
            }
            else if (button4.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button4.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(4);
            }
            button_click();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.BackColor == System.Drawing.Color.DarkGray)
            {
                button5.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(5));
            }
            else if (button5.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else if (button5.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button5.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(5);
            }
            button_click();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.BackColor == System.Drawing.Color.DarkGray)
            {
                button6.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(6));
            }
            else if (button6.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button6.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(6);
            }
            button_click();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.BackColor == System.Drawing.Color.DarkGray)
            {
                button7.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(7));
            }
            else if (button7.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button7.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(7);
            }
            button_click();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.BackColor == System.Drawing.Color.DarkGray)
            {
                button8.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(8));
            }
            else if (button8.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button8.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(8);
            }
            button_click();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.BackColor == System.Drawing.Color.DarkGray)
            {
                button9.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(9));
            }
            else if (button9.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button9.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(9);
            }
            button_click();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (button10.BackColor == System.Drawing.Color.DarkGray)
            {
                button10.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(10));
            }
            else if (button10.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button10.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(10);
            }
            button_click();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if (button11.BackColor == System.Drawing.Color.DarkGray)
            {
                button11.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(11));
            }
            else if (button11.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button11.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(11);
            }
            button_click();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            if (button12.BackColor == System.Drawing.Color.DarkGray)
            {
                button12.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(12));
            }
            else if (button12.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button12.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(12);
            }
            button_click();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (button13.BackColor == System.Drawing.Color.DarkGray)
            {
                button13.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(13));
            }
            else if (button13.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button13.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(13);
            }
            button_click();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            if (button14.BackColor == System.Drawing.Color.DarkGray)
            {
                button14.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(14));
            }
            else if (button14.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button14.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(14);
            }
            button_click();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            if (button15.BackColor == System.Drawing.Color.DarkGray)
            {
                button15.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(15));
            }
            else if (button15.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button15.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(15);
            }
            button_click();
        }
        private void button16_Click(object sender, EventArgs e)
        {
            if (button16.BackColor == System.Drawing.Color.DarkGray)
            {
                button16.BackColor = System.Drawing.Color.LightGray;
                check_card_number.RemoveAt(check_card_number.IndexOf(16));
            }
            else if (button16.BackColor == System.Drawing.Color.Black)
            {
                MessageBox.Show("이미 맞춘 카드입니다.");
            }
            else
            {
                button16.BackColor = System.Drawing.Color.DarkGray;
                check_card_number.Add(16);
            }
            button_click();
        }


    }
}