using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MySql.Data.MySqlClient;

namespace CardGame
{
    /* 서버의 통신 Flow
     * 1. 최초 실행 시, 포트를 바인딩하고 Listen 상태로 만드는 InitSocket()을 스레드 방식으로 실행함
     *    (스레드로 실행하는 이유 : 클라이언트에서 접속하기 전까지 무한 대기(응답 없음) 상태에 빠지기 때문)
     * 2. 클라이언트로부터 접속 요청이 오면, 해당 클라이언트를 client 변수에 넣은 다음 메세지를 받을 수 있도록
     *    ReceiveMessage(client)를 스레드 방식으로 실행함
     *    (스레드로 실행하는 이유 : 한 클라이언트에서 Receive를 대기하는 동안 다른 클라이언트에서도 메세지를 받을 수 있도록)
     *    (3개의 클라이언트가 접속되어 있으면, 3개의 ReceiveMessage(** 각 클라이언트 **) 스레드가 실행 중인 것임)
     * 3. ReceiveMessage를 통해 데이터를 받은 경우, MSG 열거형을 통해 받은 데이터가 무엇에 관한 것인지 식별하고
     *    종류에 따라 알맞은 처리를 실행함
     *    (EXIT_MSG인 경우, 해당 클라이언트를 연결 목록에서 삭제 후 ReceiveMessage를 종료함)
     * 4. 클라이언트에 메세지를 보내야 하는 경우, SendMessage(msg) 혹은 SendMessageAll(msg)을 통해 메세지를 전송함
     */

    public partial class ServerForm : Form
    {
        const int PORT_NUM = 12345;         // 포트 번호
        const int BUFF_MAX_SIZE = 1024;     // 클라이언트로부터 받을 최대 버퍼 크기

        class clientStatus  // 클라이언트 정보를 기록합니다
        {
            const int COMMON_USER = 0;
            const int ADMIN_USER = 1;
            public string userIP = string.Empty;  // 클라이언트(플레이어) 이름
            public int userType;

            public clientStatus(string ip, int type)        // clientStatus 생성자
            {
                userIP = ip;
                userType = type;
            }
        }

        public enum MSG : ushort   // 주고받은 데이터가 무엇에 관한 데이터인지 메시지의 맨 처음에 기록합니다
        {
            ENTRY,   // 클라이언트 접속
            EXIT,    // 클라이언트 종료
            DB_EXECUTE, // DB 정보 수정 (INSERT, ALTER, DELETE)
                        // 로그인, 회원가입, 버스 정보 등록, 수정, 삭제
                        // 버스 좌석 예약, 예약 정보 수정, 삭제
            DB_QUERY, // DB 질의 (SELECT)
                      // 유저 정보 열람, 버스 정보 열람, 버스 예약 정보 열람
                      // 출발지, 도착지에 맞는 정보 열람
            DB_RESPONSE_END, // Execute의 경우 단순 완료를 알려줌
                             // Query의 경우 DB 결과 DataSet을 반환. 버스 예약 정보, 회원 정보 등
                             // 서버에서는 Dataset에 대해 특별한 작업을 하지 않고 클라이언트에서 처리하게 함
                             // 데이터 전송이 끝나면 END 메세지를 보냄
            DB_FAILURE = byte.MaxValue // DB에서 예외처리 발생 시 전송

            // ENTRY_DENY   // 접속 거부 - what case?
        }

        TcpListener server = null;
        TcpClient client = default(TcpClient);
        Dictionary<TcpClient, clientStatus> clientList = new Dictionary<TcpClient, clientStatus>();
        MySqlConnection conn = default(MySqlConnection); // DB 연결

        public ServerForm()
        {
            InitializeComponent();
            Thread th = new Thread(Init);
            th.IsBackground = true;
            th.Start();
        }

        private void Init()
        {
            try
            {
                string connstr = "datasource=155.230.235.245;port=3306;username=cduser1;password=cduser@2020";
                conn = new MySqlConnection(connstr);
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
            DisplayText("DB 서버와 연결 완료");

            server = new TcpListener(IPAddress.Any, PORT_NUM);  // Bind
            server.Start();                                     // Listen
            DisplayText("서버가 시작되었습니다.");
            
            // Connect 요청이 올 때까지 스레드 방식으로 무한정 대기한다.
            // Connect 요청이 오면 해당 처리를 하고 다시 무한정 대기한다.
            while(true)
            {
                try
                {
                    client = server.AcceptTcpClient(); // 여기에서 무한정 대기

                    // Connect 요청이 오면, 연결 후 사용자 IP 정보를 얻음
                    Socket c = client.Client;
                    IPEndPoint ip_point = (IPEndPoint)c.RemoteEndPoint;
                    string userIP = ip_point.Address.ToString();

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[BUFF_MAX_SIZE];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string userType = Encoding.Unicode.GetString(buffer, 0, bytes);
                    userType = userType.Substring(0, userType.IndexOf("$"));

                    DisplayText(userIP + " 입장");
                    clientList.Add(client, new clientStatus(userIP, Convert.ToInt32(userType)));
                    AddUser(userIP);

                    Thread t = new Thread(() => ReceiveMessage(client));
                    t.IsBackground = true;
                    t.Start();
                }
                catch (Exception ex) {
                    MessageBox.Show("접속 오류 : " + ex.Message);
                    break;
                }
            }

            conn.Close();
            server.Stop();
            Application.Exit();
        } // end of InitSocket
        
        private void ReceiveMessage(TcpClient myClient)
        {
            byte[] buffer = new byte[BUFF_MAX_SIZE];

            // 메세지가 오지 않으면 while 문을 무한정 반복하고,
            // 메세지가 오면 MSG 열거형에 따라 어떤 데이터인지 구분한 뒤 알맞은 처리를 한다.
            while (true)
            {
                try
                {
                    NetworkStream stream = myClient.GetStream();
                    int bytes = stream.Read(buffer, 0, buffer.Length); // 스트림으로부터 데이터를 읽음
                    string data = Encoding.Unicode.GetString(buffer, 0, bytes);
                    string[] msg = data.Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);

                    bool exitFlag = false;
                    byte[] sendBuffer;

                    switch (msg[0][0])
                    {
                        // EXIT_MSG : 클라이언트 접속 종료 알림
                        // 연결 목록에서 해당 클라이언트를 삭제하고,
                        // while 루프를 빠져나와 ReceiveMessage 스레드를 종료한다.
                        case (char)MSG.EXIT:
                            string userIP = clientList[myClient].userIP;
                            DisplayText(userIP + " 퇴장");
                            RemoveUser(userIP);
                            clientList.Remove(myClient);
                            exitFlag = true;
                            break;

                        case (char)MSG.DB_EXECUTE:
                            string executeStr = msg[1];
                            DisplayText(clientList[myClient].userIP + " DB_EXECUTE : " + executeStr);
                            MySqlCommand executeCmd = new MySqlCommand(executeStr, conn);
                            executeCmd.ExecuteNonQuery();
                            sendBuffer = new byte[BUFF_MAX_SIZE];
                            sendBuffer[0] = (byte)MSG.DB_RESPONSE_END;
                            SendMessage(sendBuffer, myClient);
                            break;

                        case (char)MSG.DB_QUERY:
                            string queryStr = msg[1];
                            DisplayText(clientList[myClient].userIP + " DB_QUERY : " + queryStr);
                            MySqlCommand queryCmd = new MySqlCommand(queryStr, conn);

                            MySqlDataReader reader;
                            reader = queryCmd.ExecuteReader();
                            
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    sendBuffer = new byte[BUFF_MAX_SIZE];
                                    byte[] tmp = ObjectToByte(reader[i]);
                                    Array.Copy(tmp, sendBuffer, tmp.Length);
                                    SendMessage(sendBuffer, myClient);
                                }
                            }
                            sendBuffer = new byte[BUFF_MAX_SIZE];
                            sendBuffer[0] = (byte)MSG.DB_RESPONSE_END;
                            SendMessage(sendBuffer, myClient);
                            reader.Close();
                            break;
                    }

                    stream.Flush();
                    if (exitFlag) break; // 클라이언트 종료
                }
                catch (MySqlException ex)
                {
                    DisplayText(clientList[myClient].userIP + " DB 오류 : " + ex.Message);
                    byte[] buf = new byte[BUFF_MAX_SIZE];
                    buf[0] = (byte)MSG.DB_FAILURE;
                    SendMessage(buf, myClient);
                    break;
                }
                // 클라이언트 연결에서 문제가 발생하면 해당 클라이언트를 연결 목록에서 삭제하고 스레드 종료함
                catch (Exception ex)
                {
                    DisplayText(clientList[myClient].userIP + " 오류 : " + ex.Message);
                    listBoxPlayers.Items.Remove(clientList[myClient].userIP);
                    clientList.Remove(myClient);
                    break;
                }
            }
        } // end of ReceiveMessage
        
        // SendMessage : 특정 클라이언트(Receiver)를 대상으로 메세지를 전달함
        private void SendMessage(byte[] buffer, TcpClient receiver)
        {
            NetworkStream stream = receiver.GetStream();
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }


        // 프로그래머가 작성한 코드에서 실행된 스레드에서 컨트롤을 호출할 경우 크로스 스레드 관련 문제가 발생.
        // 이를 해결하기 위해 InvokeRequired 변수 및 BeginInvoke 함수를 사용해야 한다.
        // DisplayText : RichTextBox에 정보 기록
        // AddUser : ListBox에 플레이어 이름 추가
        // RemoveUser : ListBox에서 플레이어 이름 삭제
        private void DisplayText(string str)
        {
            str = "[" + DateTime.Now.ToString() + "] " + str + Environment.NewLine;
            if (MyRichTextBox.InvokeRequired)
            {
                MyRichTextBox.BeginInvoke(new MethodInvoker(delegate
                {
                    MyRichTextBox.AppendText(str);
                }));
            }
            else
                MyRichTextBox.AppendText(str);
        }
        private void AddUser(string name)
        {
            if (listBoxPlayers.InvokeRequired)
            {
                listBoxPlayers.BeginInvoke(new MethodInvoker(delegate
                {
                    listBoxPlayers.Items.Add(name);
                }));
            }
            else
                listBoxPlayers.Items.Add(name);
        }
        private void RemoveUser(string name)
        {
            if (listBoxPlayers.InvokeRequired)
            {
                listBoxPlayers.BeginInvoke(new MethodInvoker(delegate
                {
                    listBoxPlayers.Items.Remove(name);
                }));
            }
            else
                listBoxPlayers.Items.Remove(name);
        }

        private void button_execute_Click(object sender, EventArgs e)
        {
            try
            {
                string executeStr = textBox_command.Text;
                DisplayText("DB_EXECUTE : " + executeStr);
                MySqlCommand executeCmd = new MySqlCommand(executeStr, conn);
                executeCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void button_query_Click(object sender, EventArgs e)
        {
            MySqlDataReader reader = default(MySqlDataReader);
            try
            {
                string queryStr = textBox_command.Text;
                DisplayText("DB_QUERY : " + queryStr);
                MySqlCommand queryCmd = new MySqlCommand(queryStr, conn);
                
                reader = queryCmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i=0; i<reader.FieldCount; i++)
                        DisplayText((string)reader[i]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        // object to byte[] 출처: https://shine10e.tistory.com/121
        public static byte[] ObjectToByte(object obj)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, obj);
                    return stream.ToArray();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            return null;
        }
    }
}
