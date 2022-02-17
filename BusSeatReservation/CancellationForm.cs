using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BusSeatReservation
{
    public partial class CancellationForm : Form
    {
        private List<int> _reservations = new List<int>();
        MainForm parent;

        public CancellationForm(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            LoadReservation();
        }

        private void LoadReservation()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            int userNum = Setting.Instance.num;
            _reservations = new List<int>();

            string queryStr = $"select * from lhjtest.reserve WHERE userid='{userNum}'";
            parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
            object[] dataList = parent.ReceiveMessage();

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
                parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
                object[] busDataList = parent.ReceiveMessage();
                if (MainForm.checkDBFailure(busDataList))
                    showErrorMsg();

                lvi.SubItems.Add(busDataList[0].ToString());
                lvi.SubItems.Add(busDataList[3].ToString());
                lvi.SubItems.Add(busDataList[4].ToString());

                queryStr = $"select name from lhjtest.destination WHERE id='{busDataList[1]}'";
                parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
                object[] location = parent.ReceiveMessage();
                if (MainForm.checkDBFailure(location))
                    showErrorMsg();
                var startName = location[0].ToString();

                queryStr = $"select name from lhjtest.destination WHERE id='{busDataList[2]}'";
                parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
                location = parent.ReceiveMessage();
                if (MainForm.checkDBFailure(location))
                    showErrorMsg();
                var endName = location[0].ToString();

                lvi.SubItems.Add(info.seatnum.ToString());
                lvi.SubItems.Add($"{startName} - > {endName}");
                listView1.Items.Add(lvi);
            }

            listView1.EndUpdate();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
            {
                MessageBox.Show("취소할 예약 정보를 선택해주세요.");
                return;
            }

            var result = MessageBox.Show("정말 예약을 취소하겠습니까?", "예약 취소", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                int index = listView1.SelectedIndices[0];
                if (BusHelper.DeleteReservation(_reservations[index]))
                {
                    MessageBox.Show("예약을 취소하였습니다.");
                }

                LoadReservation();
            }
        }

        private void showErrorMsg()
        {
            MessageBox.Show("DB에서 문제가 발생하여 예약 정보를 가져오지 못했습니다.");
            Close();
        }
    }
}
