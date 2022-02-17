using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

using MySql.Data.MySqlClient;
namespace BusSeatReservation
{
    public partial class OrderForm : Form
    {
        private List<(int id, string name)> _startPoint = new List<(int id, string name)>();
        private List<(int id, string name)> _endPoint = new List<(int id, string name)>();
        private List<(int busid, string name, DateTime start, DateTime end)> _busInfo = new List<(int busid, string name, DateTime start, DateTime end)>();
        private List<BusSeat> _seats;

        private int selectBusid = -1;

        MainForm parent;

        public OrderForm(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;

            // DB로부터 위치 정보를 가져옴
            string queryStr = "SELECT * FROM lhjtest.destination";
            parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
            object[] dataList = parent.ReceiveMessage();

            departure.BeginUpdate();
            destination.BeginUpdate();

            int id = 0;
            for (int i = 0; i < dataList.Length; i++)
            {
                id = (int)dataList[i];
                string loc = dataList[++i].ToString();
                _startPoint.Add((id, loc));
                _endPoint.Add((id, loc));
                departure.Items.Add(loc);
                destination.Items.Add(loc);
                
            }
            departure.EndUpdate();
            destination.EndUpdate();

            // 콤보박스에 날짜 추가
            date.BeginUpdate();
            DateTime dt = DateTime.Now;
            CultureInfo c = new CultureInfo("ko-KR");
            for (int i = 0; i < 28; i++)
            {
                date.Items.Add(dt.ToString("d", c));
                dt = dt.AddDays(1);
            }
            date.SelectedIndex = 0;
            date.EndUpdate();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {

        }

        // [검색] 버튼. 해당 출발지 / 목적지 / 날짜에 맞는 버스 정보를 DB에서 검색
        // 데이터 부족으로 날짜 부분은 주석처리함...
        private void search_businfo_Click(object sender, EventArgs e)
        {
            if (departure.SelectedItems.Count == 0 || (destination.SelectedItems.Count == 0))
            {
                MessageBox.Show("출발지와 도착지를 설정해주세요.");
                return;
            }

            string queryStr = "SELECT id, name, departure, arrival FROM lhjtest.bus ";
            queryStr += string.Format("WHERE startid = {0} AND destinationid = {1}",
                                       _startPoint[departure.SelectedIndices[0]].id, _endPoint[destination.SelectedIndices[0]].id);
            //queryStr += string.Format(" AND departure LIKE '{0}%'", date.SelectedItem.ToString());

            parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
            object[] dataList = parent.ReceiveMessage();
            if (MainForm.checkDBFailure(dataList))
            {
                MessageBox.Show("DB에서 문제가 발생했습니다. 다시 시도해주세요.");
                return;
            }

            // 버스 정보를 ListView에 추가
            _busInfo = new List<(int busid, string name, DateTime start, DateTime end)>();

            showbusinfo.BeginUpdate();
            showbusinfo.Items.Clear();

            int id = 0;
            string name = string.Empty;
            DateTime start = default(DateTime);
            DateTime end = default(DateTime);
            for (int i = 0; i < dataList.Length; i++)
            {   
                id = (int)dataList[i];
                name = dataList[++i].ToString();
                start = DateTime.Parse(dataList[++i].ToString());
                end = DateTime.Parse(dataList[++i].ToString());

                _busInfo.Add((id, name, start, end));
                var lvi = new ListViewItem(name);
                lvi.SubItems.Add(start.ToString());
                lvi.SubItems.Add(end.ToString());
                showbusinfo.Items.Add(lvi);
            }
            showbusinfo.EndUpdate();
            // SELECT * FROM lhjtest.bus WHERE startid = 1 AND destinationid = 3;
        }

        // 버스 정보를 더블클릭하여 현재 예매 상황을 표시함
        private void showbusinfo_DoubleClick(object sender, EventArgs e)
        {
            if (showbusinfo.SelectedIndices.Count == 0)
                return;

            int index = showbusinfo.SelectedIndices[0];
            _seats = new List<BusSeat>();
            selectBusid = _busInfo[index].busid;

            string queryStr = "SELECT seatnum FROM lhjtest.reserve ";
            queryStr += string.Format("WHERE busid = {0}", selectBusid);
            parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
            object[] dataList = parent.ReceiveMessage();

            // 좌석 정보 생성하여 Control에 추가
            for (int i = 0; i < 37; i++)
            {
                _seats.Add(new BusSeat { Available = true, SeatNumber = i + 1 });
            }
            for (int i = 0; i < dataList.Length; i++)
            {
                int num = (int)dataList[i];
                _seats[num - 1].Available = false;
            }

            busReservationControl1.Seats = _seats;
            busReservationControl1.UpdateBusSeat();
        }


        // [예매] 버튼 클릭
        private void reservationBtn_Click(object sender, EventArgs e)
        {
            if (busReservationControl1.SelectIndex == 0)
            {
                MessageBox.Show("좌석을 선택해주세요.");
                return;
            }

            var selectSeat = _seats[busReservationControl1.SelectIndex - 1];

            if (selectSeat.Available)
            {
                string msgStr = $"정말로 {selectSeat.SeatNumber}번 좌석을 예약 하시겠습니까?";
                var ok = MessageBox.Show(msgStr, "예약", MessageBoxButtons.OKCancel);
                if (ok == DialogResult.OK)
                {
                    int userNum = Setting.Instance.num;
                    string executeStr = "INSERT INTO lhjtest.reserve (busid, userid, seatnum)";
                    executeStr += string.Format("VALUES( '{0}', '{1}', '{2}')", selectBusid, userNum, selectSeat.SeatNumber);

                    parent.SendMessage((char)MainForm.MSG.DB_EXECUTE + "$" + executeStr);
                    object[] result = parent.ReceiveMessage();

                    if (MainForm.checkDBFailure(result))
                    {
                        MessageBox.Show("DB에서 문제가 발생하여 예약하지 못했습니다. 다시 시도해주세요.");
                        return;
                    }
                    MessageBox.Show("예약 완료");
                    Close();
                }
            }
            else
            {
                MessageBox.Show("이미 예약된 좌석 입니다.");
            }
        }
    }
}
