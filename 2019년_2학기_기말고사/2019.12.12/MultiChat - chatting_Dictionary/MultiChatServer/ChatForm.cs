using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MultiChatServer
{
    public partial class ChatForm : Form
    {
        delegate void AppendTextDelegate(Control ctrl, string s);
        AppendTextDelegate _textAppender;
        Socket mainSock;
        IPAddress thisAddress;
        IPEndPoint serverEP;
        Dictionary<string, Socket> connectedClients;
        int card_send = 0;
        List<int> card_number_list = new List<int>();//랜덤
        List<int> card_OX_list = new List<int>();
        int clientNum;
        //int esc_count = 0;
        int gameover = 1;
        List<string> client_ID = new List<string>();//접속한 아이디
        //string OXs = "";
        public ChatForm()
        {
            InitializeComponent();
            mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _textAppender = new AppendTextDelegate(AppendText);
            connectedClients = new Dictionary<string, Socket>();
            clientNum = 0; //초기화

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
            IPHostEntry he = Dns.GetHostEntry(Dns.GetHostName());
            // 처음으로 발견되는 ipv4 주소를 사용한다.
            foreach (IPAddress addr in he.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    AppendText(txtHistory, addr.ToString());
                }
            }
        }
        void BeginStartServer(object sender, EventArgs e)
        {
            int port;
            if (!int.TryParse(txtPort.Text, out port))
            { //문자열을 int port로 변환
                MsgBoxHelper.Error("포트 번호가 잘못 입력되었거나 입력되지 않았습니다.");
                txtPort.Focus();
                txtPort.SelectAll();
                return;
            }

            thisAddress = IPAddress.Parse(txtAddress.Text);
            if (thisAddress == null)
            {// 로컬호스트 주소를 사용한다.                
                thisAddress = IPAddress.Loopback;
                txtAddress.Text = thisAddress.ToString();
            }

            // 서버에서 클라이언트의 연결 요청을 대기하기 위해
            // 소켓을 열어둔다.
            serverEP = new IPEndPoint(thisAddress, port);
            mainSock.Bind(serverEP);
            mainSock.Listen(10);

            AppendText(txtHistory, string.Format("서버 시작: @{0}", serverEP));
            // 비동기적으로 클라이언트의 연결 요청을 받는다.
            mainSock.BeginAccept(AcceptCallback, null);
        }


        void AcceptCallback(IAsyncResult ar)
        {
            // 클라이언트의 연결 요청을 수락한다.
            Socket client = mainSock.EndAccept(ar);

            // 또 다른 클라이언트의 연결을 대기한다.
            mainSock.BeginAccept(AcceptCallback, null);

            AsyncObject obj = new AsyncObject(4096);// 4096 buffer size
            obj.WorkingSocket = client;

            AppendText(txtHistory, string.Format("클라이언트 접속 : @{0}",
                client.RemoteEndPoint));

            // 클라이언트의 ID 데이터를 받는다.
            client.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
        }

        void DataReceived(IAsyncResult ar)
        {
            // BeginReceive에서 추가적으로 넘어온 데이터를 AsyncObject 형식으로 변환한다.
            AsyncObject obj = (AsyncObject)ar.AsyncState;

            // 데이터 수신을 끝낸다.
            int received = obj.WorkingSocket.EndReceive(ar);

            // 받은 데이터가 없으면(연결끊어짐) 끝낸다.
            if (received <= 0)
            {
                // AppendText(txtHistory, string.Format("클라이언트 접속해제?{0}", clientNum));
                if (clientNum > 0)
                {
                    foreach (KeyValuePair<string, Socket> clients in connectedClients)
                    {
                        if (obj.WorkingSocket == clients.Value)
                        {
                            string key = clients.Key;
                            string clientIDs = "";
                            try
                            {
                                client_ID.RemoveAt(client_ID.IndexOf(key));
                                this.Invoke(new Action(delegate ()
                                {
                                    listView1.Clear(); //리스트뷰 clear
                                }));
                                for (int i = 0; i < client_ID.Count; i++)
                                {
                                    this.Invoke(new Action(delegate ()
                                    {
                                        listView1.Items.Add(client_ID[i]); //서버에 접속한 클라이언트 추가
                                    }));
                                    clientIDs += client_ID[i];
                                    clientIDs += ":";
                                }
                                connectedClients.Remove(key);
                                AppendText(txtHistory, string.Format("접속해제완료:{0}", key));
                                clientNum--;
                                byte[] bDts = Encoding.UTF8.GetBytes("ClientName" + ':' + clientNum + ":" + clientIDs);
                                sendAll(null, bDts);
                            }
                            catch { }
                            break;
                        }
                    }
                }
                try
                {
                    obj.WorkingSocket.Disconnect(false);
                }
                catch { }
                obj.WorkingSocket.Disconnect(false);
                obj.WorkingSocket.Close();
                             // AppendText(txtHistory, string.Format("클라이언트 접속해제완료{0}", clientNum));
                return;
            }

            // 텍스트로 변환한다.
            string text = Encoding.UTF8.GetString(obj.Buffer);

            // : 기준으로 짜른다.
            string[] tokens = text.Split(':');
            string fromID = null;
            string toID = null;
            string code = tokens[0];

            if (code.Equals("ID"))    // 받은 문자열   id:자신의 id
            {
                fromID = tokens[1].Trim(); //fromID;
                if (client_ID.Contains(fromID) == true)//같은 아이디 접속
                {
                    if (client_ID.Contains(fromID + "(2)") == true)
                        fromID += "(3)";
                    else
                        fromID += "(2)";
                }

                    clientNum++; //참여자
                this.Invoke(new Action(delegate ()
                {
                    this.Invoke(new Action(delegate ()
                    {
                        listView1.Items.Add(fromID);//참여자 목록
                    }));
                })); 

                    AppendText(txtHistory, string.Format("[접속{0}]ID:{1}:{2}",
                                clientNum, fromID, obj.WorkingSocket.RemoteEndPoint.ToString()));

                    // 연결된 클라이언트 리스트에 추가해준다.
                    connectedClients.Add(fromID, obj.WorkingSocket);
                    // 전체 클라이언트에게 데이터를 보낸다.
                    sendAll(obj.WorkingSocket, obj.Buffer);

                    if (clientNum > 0)
                    { //참여자 목록

                        client_ID.Add(fromID);
                        string clientIDs = "";
                        for (int i = 0; i < client_ID.Count; i++)
                        {
                            clientIDs += client_ID[i];
                            clientIDs += ":";
                        }
                        //접속을 1명했다면 clientName : clientNum : 1 :;
                        //접속을 2명했다면 clientName : clientNum : 1 : 2 :;
                        byte[] bDts = Encoding.UTF8.GetBytes("ClientName" + ':' + clientNum + ":" + clientIDs);
                        sendAll(null, bDts);
                    }

                    if (clientNum == 3 && card_send == 0) // 3명 접속하면 시작
                    {
                        card_mix();
                        //esc_count = 0;
                        string card_list = "";
                        for (int i = 0; i < card_number_list.Count; i++)
                        {
                            card_list += card_number_list[i];
                            card_list += ":";
                        }
                        AppendText(txtHistory, card_list);
                        byte[] bDtss = Encoding.UTF8.GetBytes("Card" + ':' + card_list);
                        sendAll(null, bDtss);
                        card_send++;//한번만 보내주기 위해
                    }
                 
            }

            //전체 메시지
            else if (code.Equals("BR"))
            {
                fromID = tokens[1].Trim(); //fromID
                string msg = tokens[2];
                AppendText(txtHistory, string.Format("[전체]{0}: {1}", fromID, msg));
                sendAll(obj.WorkingSocket, obj.Buffer);
            }

            //귓속말
            else if (code.Equals("TO"))   // TO:to_id:message
            {
                fromID = tokens[1].Trim(); //fromID
                toID = tokens[2].Trim();
                string msg = tokens[3];
                string sendingMsg = "[FROM:" + fromID + "][TO:" + toID + "]" + msg;
                AppendText(txtHistory, sendingMsg);
                connectedClients.TryGetValue(toID, out obj.WorkingSocket);
                sendTo(obj.WorkingSocket, obj.Buffer);
                //AppendText(txtHistory, "To socket" + socket.RemoteEndPoint.ToString());
            }

            else
            {
            }

            //Client가 답을 보냈을 때
            //Client가 답을 썼을 때 맞는지 틀렸는지 비교
            if (code.Equals("PRE"))
            {
                fromID = tokens[1].Trim(); //fromID

                if (card_number_list[Int32.Parse(tokens[2]) -1] == card_number_list[Int32.Parse(tokens[3]) -1])
                {
                    button_color_change_O(Int32.Parse(tokens[2]));
                    button_color_change_O(Int32.Parse(tokens[3]));
                    card_OX_list.Add(Int32.Parse(tokens[2]));
                    card_OX_list.Add(Int32.Parse(tokens[3]));
                    byte[] bDtss = Encoding.UTF8.GetBytes("PREO" + ":" + fromID + ":" + tokens[2] + ":" + tokens[3]);
                    sendAll(null, bDtss);
                }
                else {
                    button_color_change_X(Int32.Parse(tokens[2]));
                    button_color_change_X(Int32.Parse(tokens[3]));
                    byte[] bDtss = Encoding.UTF8.GetBytes("PREX" + ":" + fromID + ":" + tokens[2] + ":" + tokens[3]);
                    sendAll(null, bDtss);
                }
            }

            //게임 종료
            if (code.Equals("ESC"))
            {
                if (gameover == 1)
                {
                    byte[] bDtss = Encoding.UTF8.GetBytes("ESC" + ":" + "게임종료");
                    sendAll(null, bDtss);
                    client_ID.Clear();
                    connectedClients.Clear();
                    for (int i = 1; i < 17; i++) { button_color_change_X(i); }
                    clientNum = 0;
                    card_send = 0;
                    card_OX_list.Clear();

                    this.Invoke(new Action(delegate ()
                    {
                        listView1.Clear();
                    }));
                    gameover++;
                }
            }

            // 텍스트박스에 추가해준다.
            // 비동기식으로 작업하기 때문에 폼의 UI 스레드에서 작업을 해줘야 한다.
            // 따라서 대리자를 통해 처리한다.
            // AppendText(txtHistory, string.Format("[받음]{0}: {1}", id, msg));
            // 데이터를 받은 후엔 다시 버퍼를 비워주고 같은 방법으로 수신을 대기한다.
            obj.ClearBuffer();
            // 수신 대기
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);

        }
        void button_color_change_O(int i) // 맞췄을 경우 색깔 검은색으로 바꾸기
        {
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

        void button_color_change_X(int i)// 틀렸을 경우 색깔 원래색으로 바꾸기
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
        void sendTo(Socket socket, byte[] buffer)
        {
            try
            {
                socket.Send(buffer);
            }
            catch
            {// 오류 발생하면 전송 취소
                try { AppendText(txtHistory, "dispose"); socket.Dispose(); } catch { }
            }
        }

        void sendAll(Socket except, byte[] buffer)
        {
            foreach (Socket socket in connectedClients.Values)
            {
                if (socket != except)
                {
                    try { socket.Send(buffer); }
                    catch
                    {// 오류 발생하면 전송 취소하고 삭제
                        try { socket.Dispose(); } catch { }
                    }
                }
            }
        }

//카드 랜덤으로 섞기
        void card_mix() //(1~8) * 2
        {
            card_number_list.Clear();
            int while_count = 0; // random 6번으로 제한
            while (true)
            {
                Random r = new Random();
                int card_number = r.Next(1, 9); // 1~8
                AppendText(txtHistory, card_number+"");
                if (while_count < 16)
                {
                    int count_card_e = 0;//같은 수는 2개까지
                    for (int i = 0; i < card_number_list.Count; i++)
                    {
                        if (card_number_list[i] == card_number)
                        {
                            count_card_e++;
                            
                        }
                    }
                    if (count_card_e == 2)
                    {
                        AppendText(txtHistory, "중복");
                        continue;
                    }
                    else // 리스트에 동일한 값이 없을 때 추가
                    {
                        card_number_list.Add(card_number);
                        while_count++;
                    }
                }
                else
                {
                    break;
                }
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

            // 문자열을 utf8 형식의 바이트로 변환한다.
            byte[] bDts = Encoding.UTF8.GetBytes("Server" + ':' + tts);

            // 연결된 모든 클라이언트에게 전송한다.
            sendAll(null, bDts);

            // 전송 완료 후 텍스트박스에 추가하고, 원래의 내용은 지운다.
            AppendText(txtHistory, string.Format("[보냄]Server: {0}", tts));
            txtTTS.Clear();

        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                mainSock.Close();
            }
            catch { }
        }

        private void tblMainLayout_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}