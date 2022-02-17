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

namespace BusSeatReservation
{
    public partial class MainForm : Form
    {
        TcpClient client = new TcpClient();
        NetworkStream stream = default(NetworkStream);
        const int BUFF_MAX_SIZE = 1024;

        private string IP_ADDRESS;
        private int PORT_NUM;

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

            // 서버 연결, 리스트뷰 Column 추가, 예약 데이터 얻기
            try
            {
                client.Connect(IP_ADDRESS, PORT_NUM);
                stream = client.GetStream();

                byte[] buffer = Encoding.Unicode.GetBytes("0$"); // 0 = COMMON_USER
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();

                LoginForm loginform = new LoginForm(this);
                loginform.ShowDialog();
                if (Setting.Instance.Id.Equals(string.Empty))
                    return;

                listView_reserve.BeginUpdate();
                listView_reserve.Columns.Add("예약 ID", 0);
                listView_reserve.Columns.Add("버스 ID", 60, HorizontalAlignment.Left);
                listView_reserve.Columns.Add("행선지", 100, HorizontalAlignment.Left);
                listView_reserve.Columns.Add("출발 시간", 165, HorizontalAlignment.Left);
                listView_reserve.Columns.Add("도착 시간", 165, HorizontalAlignment.Left);
                listView_reserve.Columns.Add("좌석", 40, HorizontalAlignment.Left);
                listView_reserve.EndUpdate();

                LoadReservation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }

        }

        // 서버로 메세지 보냄
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
                        // DB_FAILURE : DB 처리 실패
                        // DB_FAILURE 반환하고, checkDBFailure에서 처리하도록 함
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

        // 프로그램 종료 시 서버에게 종료 알림
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SendMessage((char)MSG.EXIT + "$");
        }

        // OrderForm 실행
        private void btn_reserve_Click(object sender, EventArgs e)
        {
            OrderForm form = new OrderForm(this);
            form.ShowDialog();
            LoadReservation();
        }

        // 예약 취소 (예약 정보 삭제)
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (listView_reserve.SelectedIndices.Count == 0)
            {
                MessageBox.Show("취소할 예약 정보를 선택해주세요.");
                return;
            }
            
            if (MessageBox.Show("정말 예약을 취소하겠습니까?", "예약 취소", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int reserveID = int.Parse(listView_reserve.SelectedItems[0].Text);
                string executeStr = $"DELETE FROM lhjtest.reserve WHERE reserveid={reserveID}";
                SendMessage((char)MSG.DB_EXECUTE + "$" + executeStr);
                object[] result = ReceiveMessage();
                if (checkDBFailure(result))
                {
                    MessageBox.Show("DB에서 문제가 발생하여 예약을 취소하지 못했습니다. 다시 시도해주십시오.");
                    return;
                }
                MessageBox.Show("예약을 취소하였습니다.");
                LoadReservation();
            }
        }

        // 서버와 연결하여 DB에서 예약 정보를 가져옴
        private void LoadReservation()
        {
            listView_reserve.BeginUpdate();
            listView_reserve.Items.Clear();
            int userNum = Setting.Instance.num;

            string queryStr = $"select * from lhjtest.reserve WHERE userid='{userNum}'";
            SendMessage((char)MSG.DB_QUERY + "$" + queryStr);
            object[] dataList = ReceiveMessage();

            var reservationList = new List<(int reservationId, int busid, int userid, int seatnum, DateTime reservationDateTime)>();
            for (int i = 0; i < dataList.Length; i++)
            {
                int reserveId = (int)dataList[i];
                int busId = (int)dataList[++i];
                int userId = (int)dataList[++i];
                int seatnum = (int)dataList[++i];
                DateTime reserveTime = DateTime.Parse(dataList[++i].ToString());
                reservationList.Add((reserveId, busId, userId, seatnum, reserveTime));
            }

            foreach (var info in reservationList)
            {
                var lvi = new ListViewItem(info.reservationId.ToString());

                queryStr = $"select name, startid, destinationid, departure, arrival from lhjtest.bus WHERE id='{info.busid}'";
                SendMessage((char)MSG.DB_QUERY + "$" + queryStr);
                object[] busDataList = ReceiveMessage();
                if (checkDBFailure(busDataList))
                {
                    MessageBox.Show("DB에서 문제가 발생하여 예약 정보를 가져오지 못했습니다.");
                    return;
                }

                var startName = getLocation(busDataList[1].ToString());
                var endName = getLocation(busDataList[2].ToString());
                lvi.SubItems.Add(busDataList[0].ToString()); // reserve No.
                lvi.SubItems.Add($"{startName} - > {endName}"); // 행선지
                lvi.SubItems.Add(busDataList[3].ToString()); // 출발 시각
                lvi.SubItems.Add(busDataList[4].ToString()); // 도착 시각
                lvi.SubItems.Add(info.seatnum.ToString()); // 좌석
                listView_reserve.Items.Add(lvi);
            }

            listView_reserve.EndUpdate();
        }

        // 지역 ID로부터 지역 이름을 가져옴
        private string getLocation(string locid)
        {
            string queryStr = $"select name from lhjtest.destination WHERE id='{locid}'";
            SendMessage((char)MSG.DB_QUERY + "$" + queryStr);
            object[] location = ReceiveMessage();
            if (checkDBFailure(location))
            {
                MessageBox.Show("DB에서 문제가 발생하여 예약 정보를 가져오지 못했습니다.");
                return "0";
            }
            return location[0].ToString();
        }

        // DB에서 문제가 발생하여 DB_FAILURE가 반환되었을 때 처리
        public static bool checkDBFailure (object[] data)
        {
            int tmp;
            if (data.Length > 0 && int.TryParse(data[0].ToString(), out tmp) && tmp == (int)MSG.DB_FAILURE)
                return true;
            return false;
        }
    }
}
