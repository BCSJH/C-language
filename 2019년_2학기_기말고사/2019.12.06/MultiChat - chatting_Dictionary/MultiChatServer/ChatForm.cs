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

        int clientNum;

        List<string> client_ID = new List<string>();//접속한 아이디

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
                                listView1.Clear();
                                for (int i = 0; i < client_ID.Count; i++)
                                {
                                    listView1.Items.Add(client_ID[i]);
                                    clientIDs += client_ID[i];
                                    clientIDs += ":";
                                }
                                connectedClients.Remove(key);
                                AppendText(txtHistory, string.Format("접속해제완료:{0}", key));
                                byte[] bDts = Encoding.UTF8.GetBytes("ClientName" + ':' + clientNum + ":" + clientIDs);
                                sendAll(null, bDts);
                            }
                            catch { }
                            break;
                        }
                    }
                }
                obj.WorkingSocket.Disconnect(false);
                obj.WorkingSocket.Close();
                clientNum--; //참여자
                             // AppendText(txtHistory, string.Format("클라이언트 접속해제완료{0}", clientNum));
                return;
            }

            // 텍스트로 변환한다.
            string text = Encoding.UTF8.GetString(obj.Buffer);
            AppendText(txtHistory, text);

            // : 기준으로 짜른다.
            string[] tokens = text.Split(':');
            string fromID = null;
            string toID = null;
            string code = tokens[0];

            if (code.Equals("ID"))    // 받은 문자열   id:자신의 id
            {
                fromID = tokens[1].Trim(); //fromID;
                if (client_ID.Contains(fromID) == true)//접속 불가능
                {
                    if (client_ID.Contains(fromID + "(2)") == true)
                        fromID += "(3)";
                    else
                        fromID += "(2)";
                }

                    clientNum++; //참여자
                this.Invoke(new Action(delegate ()
                {
                    listView1.Items.Add(fromID);//참여자 목록
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

            else if (code.Equals("BR"))
            {
                fromID = tokens[1].Trim(); //fromID
                string msg = tokens[2];
                AppendText(txtHistory, string.Format("[전체]{0}: {1}", fromID, msg));
                sendAll(obj.WorkingSocket, obj.Buffer);
            }
            else if (code.Equals("PRE"))
            {
                //3명이 되지 않았는데 선택하면 경고문 보내기
                fromID = tokens[1].Trim(); //fromID
                string choose1 = tokens[2];
                string choose2 = tokens[3];
                byte[] bDtss = Encoding.UTF8.GetBytes("PRE" + ":" + fromID + "->" + choose1 + "," + choose2 + ":" + "선택" + "->" + OX(choose1,choose2));
                sendAll(null, bDtss);
            }
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


            // 텍스트박스에 추가해준다.
            // 비동기식으로 작업하기 때문에 폼의 UI 스레드에서 작업을 해줘야 한다.
            // 따라서 대리자를 통해 처리한다.
            // AppendText(txtHistory, string.Format("[받음]{0}: {1}", id, msg));
            // 데이터를 받은 후엔 다시 버퍼를 비워주고 같은 방법으로 수신을 대기한다.
            obj.ClearBuffer();
            // 수신 대기
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);

        }
        string OX(string i, string ii)
        {
            if (card_number_list[int.Parse(i) - 1].Equals(card_number_list[int.Parse(ii) - 1]))
            {
                return "정답";
            }
            else
                return "오답";
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

        void card_mix() //(1~8) * 2
        {
            card_number_list.AddRange(card_random());
            card_number_list.AddRange(card_random());
        }
        static List<int> card_random()
        {
            List<int> card_number_list_random = new List<int>();
            int while_count = 0; // random 6번으로 제한
            while (true)
            {
                Random r = new Random();
                int card_number = r.Next(1, 9); // 1~8

                if (while_count < 8)
                {
                    if (card_number_list_random.Contains(card_number) == true)
                    {
                        continue;
                    }
                    else // 리스트에 동일한 값이 없을 때 추가
                    {
                        card_number_list_random.Add(card_number);
                        while_count++;
                    }
                }
                else
                {
                    break;
                }
            }
            return card_number_list_random;
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