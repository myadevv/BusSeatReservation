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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BusManager
{
    public partial class MainForm : Form
    {
        TcpClient client = new TcpClient();
        NetworkStream stream = default(NetworkStream);
        const int BUFF_MAX_SIZE = 1024;

        public string IP_ADDRESS;
        public int PORT_NUM;
        public string userID = string.Empty;

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

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            IP_ADDRESS = "127.0.0.1"; // 임시 IP, 포트
            PORT_NUM = 12345;

            try
            {
                client.Connect(IP_ADDRESS, PORT_NUM);
                stream = client.GetStream();

                byte[] buffer = Encoding.Unicode.GetBytes("1$"); // 1 = ADMIN_USER
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();

                listView_bus.BeginUpdate();
                listView_bus.Columns.Add("No.", 0);
                listView_bus.Columns.Add("버스 ID", 60, HorizontalAlignment.Left);
                listView_bus.Columns.Add("출발지", 70, HorizontalAlignment.Left);
                listView_bus.Columns.Add("도착지", 70, HorizontalAlignment.Left);
                listView_bus.Columns.Add("출발 시간", 160, HorizontalAlignment.Left);
                listView_bus.Columns.Add("도착 시간", 160, HorizontalAlignment.Left);
                listView_bus.EndUpdate();
                
                listView_reserve.BeginUpdate();
                listView_reserve.Columns.Add("No.", 0, HorizontalAlignment.Left);
                listView_reserve.Columns.Add("버스 ID", 60, HorizontalAlignment.Left);
                listView_reserve.Columns.Add("예약자 ID", 100, HorizontalAlignment.Left);
                listView_reserve.Columns.Add("좌석", 70, HorizontalAlignment.Left);
                listView_reserve.Columns.Add("예약 시간", 160, HorizontalAlignment.Left);
                listView_reserve.EndUpdate();

                getDBData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

        public void SendMessage(string msg)
        {
            try
            {
                stream = client.GetStream();
                byte[] buffer = Encoding.Unicode.GetBytes(msg + "$");
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

        public object[] ReceiveMessage()
        {
            // 서버로부터 메세지가 오지 않으면 while 문을 무한 루프하고,
            // 메세지를 받으면, MSG 열거형에 따라 어떤 종류의 데이터인지 식별하고,
            // 데이터에 따라 알맞은 처리를 실행함
            byte[] buffer = new byte[BUFF_MAX_SIZE];
            List<object> dataList = new List<object>();
            bool exitFlag = false;

            while (true)
            {
                try
                {
                    stream = client.GetStream();
                    int bytes = stream.Read(buffer, 0, buffer.Length); // 스트림으로부터 데이터를 읽음

                    switch (buffer[0])
                    {
                        case (byte)MSG.DB_FAILURE:
                            return new object[1] { (byte)MSG.DB_FAILURE };

                        case (byte)MSG.DB_RESPONSE_END:
                            exitFlag = true;
                            break;

                        default:
                            object data = ByteToObject(buffer);
                            dataList.Add(data);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                if (exitFlag == true)
                    break;
            }
            return dataList.ToArray();
        }

        private void getDBData() {
            listView_bus.BeginUpdate();
            listView_reserve.BeginUpdate();
            listView_bus.Items.Clear();
            listView_reserve.Items.Clear();

            string queryStr = "SELECT id, name, startid, destinationid, departure, arrival FROM lhjtest.bus";

            SendMessage((char)MSG.DB_QUERY + "$" + queryStr);
            object[] dataList = ReceiveMessage();
            if (checkDBFailure(dataList))
            {
                MessageBox.Show("DB 불러오기에 실패했습니다. 관리자에게 문의하세요.");
                Application.Exit();
                return;
            }

            ListViewItem item = default(ListViewItem);
            if (dataList.Length > 0)
            {
                for (int i = 0; i < dataList.Length; i++)
                {
                    item = new ListViewItem(dataList[i].ToString()); // id
                    item.SubItems.Add(dataList[++i].ToString()); // name
                    item.SubItems.Add(getLocation(dataList[++i].ToString())); // startid
                    item.SubItems.Add(getLocation(dataList[++i].ToString())); // destid
                    item.SubItems.Add(dataList[++i].ToString()); // depart
                    item.SubItems.Add(dataList[++i].ToString()); // arrival
                    listView_bus.Items.Add(item);
                }
            }

            queryStr = "SELECT reserveid, busid, userid, seatnum, reserve_time FROM lhjtest.reserve";
            SendMessage((char)MSG.DB_QUERY + "$" + queryStr);
            dataList = ReceiveMessage();
            if (checkDBFailure(dataList))
            {
                MessageBox.Show("DB 불러오기에 실패했습니다. 관리자에게 문의하세요.");
                Application.Exit();
                return;
            }

            if (dataList.Length > 0)
            {
                for (int i = 0; i < dataList.Length; i++)
                {
                    item = new ListViewItem(dataList[i].ToString()); // id
                    item.SubItems.Add(getBusname(dataList[++i].ToString())); // busid
                    item.SubItems.Add(getUsername(dataList[++i].ToString())); // userid
                    item.SubItems.Add(dataList[++i].ToString()); // seatnum
                    item.SubItems.Add(dataList[++i].ToString()); // reservetime
                    listView_reserve.Items.Add(item);
                }
            }

            listView_bus.EndUpdate();
            listView_reserve.EndUpdate();
        }

        // byte[] to object. 출처: https://shine10e.tistory.com/121
        public static object ByteToObject(byte[] buffer)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    stream.Position = 0;
                    return binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            return null;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SendMessage((char)MSG.EXIT + "$");
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            RegisterForm form = new RegisterForm(this);
            form.ShowDialog();
            getDBData();
        }

        private void btn_deleteBusdata_Click(object sender, EventArgs e)
        {
            if (listView_bus.SelectedItems.Count == 0)
            {
                MessageBox.Show("선택된 정보가 없습니다.");
                return;
            }
            
            string msgStr = string.Format("{0} 건의 버스 정보를 삭제합니다. 괜찮습니까?",
                                           listView_bus.SelectedItems.Count.ToString());
            if (MessageBox.Show(msgStr, "DB 정보 삭제", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string defaultStr = "DELETE FROM lhjtest.bus ";
                foreach (ListViewItem item in listView_bus.SelectedItems)
                {
                    string executeStr = string.Copy(defaultStr);
                    executeStr += string.Format("WHERE id = {0}", item.Text);

                    SendMessage((char)MSG.DB_EXECUTE + "$" + executeStr);
                    object[] result = ReceiveMessage();
                    if (checkDBFailure(result))
                    {
                        MessageBox.Show("DB에서 에러가 발생하여 항목을 삭제하지 못했습니다.");
                        return;
                    }
                }
                MessageBox.Show("삭제 완료");
                getDBData();
            }
        }

        private string getLocation(string locID)
        {
            string queryStr = $"select name from lhjtest.destination WHERE id={locID}";
            SendMessage((char)MSG.DB_QUERY + "$" + queryStr);
            object[] location = ReceiveMessage();
            if (checkDBFailure(location))
            {
                MessageBox.Show("DB 불러오기에 실패했습니다. 관리자에게 문의하세요.");
                Application.Exit();
            }
            return location[0].ToString();
        }

        private string getBusname(string busID)
        {
            string queryStr = $"select name from lhjtest.bus WHERE id={busID}";
            SendMessage((char)MSG.DB_QUERY + "$" + queryStr);
            object[] location = ReceiveMessage();
            if (checkDBFailure(location))
            {
                MessageBox.Show("DB 불러오기에 실패했습니다. 관리자에게 문의하세요.");
                Application.Exit();
            }
            return location[0].ToString();
        }

        private string getUsername(string userID)
        {
            string queryStr = $"select username from lhjtest.userdata WHERE userid={userID}";
            SendMessage((char)MSG.DB_QUERY + "$" + queryStr);
            object[] location = ReceiveMessage();
            if (checkDBFailure(location))
            {
                MessageBox.Show("DB 불러오기에 실패했습니다. 관리자에게 문의하세요.");
                Application.Exit();
            }
            return location[0].ToString();
        }

        public static bool checkDBFailure(object[] data)
        {
            int tmp;
            if (data.Length > 0 && int.TryParse(data[0].ToString(), out tmp) && tmp == (int)MSG.DB_FAILURE)
                return true;
            return false;
        }
    }
}
