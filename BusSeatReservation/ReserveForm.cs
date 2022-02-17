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

namespace BusSeatReservation
{
    public partial class ReserveForm : Form
    {
        MainForm parent;

        public ReserveForm(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            string queryStr = "SELECT name FROM lhjtest.location";
            parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
            object[] dataList = parent.ReceiveMessage();

            departure.BeginUpdate();
            destination.BeginUpdate();
            
            foreach (var data in dataList)
            {
                departure.Items.Add(data.ToString());
                destination.Items.Add(data.ToString());
            }

            departure.EndUpdate();
            destination.EndUpdate();

            date.BeginUpdate();

            DateTime dt = DateTime.Now;
            CultureInfo c = new CultureInfo("ko-KR");
            for (int i=0; i<28; i++)
            {
                date.Items.Add(dt.ToString("d", c));
                dt = dt.AddDays(1);
            }
            date.SelectedIndex = 0;
            date.EndUpdate();

            showbusinfo.BeginUpdate();
            showbusinfo.Columns.Add("버스 ID", 60, HorizontalAlignment.Left);
            showbusinfo.Columns.Add("출발 시간", 165, HorizontalAlignment.Left);
            showbusinfo.Columns.Add("좌석", 40, HorizontalAlignment.Left);
            showbusinfo.EndUpdate();
        }

        private void search_businfo_Click(object sender, EventArgs e)
        {
            if (departure.SelectedItems.Count == 0 || (destination.SelectedItems.Count == 0)) {
                MessageBox.Show("출발지와 도착지를 설정해주세요.");
                return;
            }
                

            string queryStr = "SELECT busid, departure, seatcount FROM lhjtest.busdata ";
            queryStr += string.Format("WHERE locations LIKE '{0}%{1}' ", departure.SelectedItems[0].Text, destination.SelectedItems[0].Text);
            queryStr += string.Format("AND departure LIKE '{0}%'", date.SelectedItem);
            
            parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
            object[] dataList = parent.ReceiveMessage();

            if (dataList.Length != 0)
            {
                showbusinfo.BeginUpdate();
                showbusinfo.Items.Clear();
                ListViewItem item = default(ListViewItem);
                for (int i = 0; i < dataList.Length; i++)
                {
                    int index = i % 3;
                    if (index == 0)
                    {
                        if (i != 0)
                            showbusinfo.Items.Add(item);
                        item = new ListViewItem(dataList[i].ToString());
                    }
                    else
                        item.SubItems.Add(dataList[i].ToString());
                }
                showbusinfo.Items.Add(item);
                showbusinfo.EndUpdate();
            }
            else
            {
                MessageBox.Show("해당하는 버스 정보가 없습니다.");
            }
        }

        private void showbusinfo_DoubleClick(object sender, EventArgs e)
        {

            return;
        }
    }
}
