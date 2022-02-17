using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusManager
{
    public partial class RegisterForm : Form
    {
        MainForm parent;

        public RegisterForm(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // 목적지 정보를 DB에서 가져와 리스트박스에 추가
            string queryStr = "SELECT name FROM lhjtest.destination";
            parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
            object[] dataList = parent.ReceiveMessage();
            if (MainForm.checkDBFailure(dataList))
            {
                MessageBox.Show("프로세스 처리 중 DB에서 문제가 발생했습니다. 관리자에게 문의하십시오.");
                Close();
            }

            listBox_locations.BeginUpdate();
            for (int i=0; i<dataList.Length; i++)
            {
               listBox_locations.Items.Add(dataList[i].ToString());
            }
            listBox_locations.EndUpdate();

            // datePicker에서 선택할 수 있는 날짜를 현재 날짜부터 1년 뒤까지로 설정
            datePicker_single.MinDate = datePicker_multiFrom.MinDate = datePicker_multiTo.MinDate
                = datePicker_single.Value = datePicker_multiFrom.Value = datePicker_multiTo.Value
                = DateTime.Now;

            datePicker_single.MaxDate = datePicker_multiFrom.MaxDate = DateTime.Now.AddYears(1);
            datePicker_multiTo.MaxDate = DateTime.Now.AddYears(1).AddDays(1);
        }

        // [DB 추가하기] 버튼 클릭
        private void btn_apply_Click(object sender, EventArgs e)
        {
            if (textBox_busID.Text.Equals(string.Empty))
            {
                MessageBox.Show("버스 ID를 적어주세요.");
                return;
            }
            if (listBox_departTime.Items.Count == 0)
            {
                MessageBox.Show("시간표가 존재하지 않습니다.");
                return;
            }
            if (textBox_depart.Text.Length == 0 || textBox_arrival.Text.Length == 0)
            {
                MessageBox.Show("행선지를 선택해주세요.");
                return;
            }
            if (timePicker_arrival.Value.TimeOfDay.TotalMinutes == 0)
            {
                MessageBox.Show("소요 시간을 설정해주세요.");
                return;
            }

            DateTime from = datePicker_multiFrom.Value;
            DateTime to = datePicker_multiTo.Value;
            int applyCount = 0;

            if (radioButton_multi.Checked)
            {
                if (checkBox_setWeekday.Checked)
                {
                    int cnt = listBox_departTime.Items.Count;
                    for (DateTime date = from; date.Day <= to.Day; date = date.AddDays(1))
                    {
                        string week = date.DayOfWeek.ToString().Substring(0, 3);
                        if (checkedListBox_weekdays.CheckedItems.Contains(week))
                            applyCount += cnt;
                    }
                }
                else
                    applyCount = listBox_departTime.Items.Count * (to.Subtract(from).Days + 1);
            }
            else applyCount = listBox_departTime.Items.Count;

            string msgStr = string.Format("버스 ID = {0}. 총 {1}건의 노선을 추가합니다. 괜찮습니까?",
                                          textBox_busID.Text, applyCount.ToString());

            if (MessageBox.Show(msgStr, "DB 등록", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string departure = textBox_depart.Text;
                string arrival = textBox_arrival.Text;
                
                // 단일 날짜에 시간표 추가
                if (radioButton_single.Checked)
                {
                    if (!addDBdata(datePicker_single.Value, departure, arrival))
                        return;
                }
                // 복수 날짜
                else
                {
                    for (DateTime date = from; date.Day <= to.Day; date = date.AddDays(1))
                    {
                        // 요일 설정 체크시
                        if (checkBox_setWeekday.Checked)
                        {
                            string week = date.DayOfWeek.ToString().Substring(0, 3);
                            if (checkedListBox_weekdays.CheckedItems.Contains(week))
                                if (!addDBdata(date, departure, arrival))
                                    return;
                        }
                        else
                            if (!addDBdata(date, departure, arrival))
                                return;
                    }
                }
                
                MessageBox.Show("등록 완료");
                Close();
            }
        }

        // DB 등록
        private bool addDBdata(DateTime date, string depart, string arrival)
        {
            string defaultStr = "INSERT INTO lhjtest.bus(name, startid, destinationid, departure, arrival) ";

            int y = date.Year;
            int m = date.Month;
            int d = date.Day;
            foreach (string t in listBox_departTime.Items)
            {
                // DateTime 포매팅
                string[] time = t.Split(':');
                string depart_time = string.Format("{0}-{1}-{2} {3}:{4}:{5}",
                                                  y, m, d, time[0], time[1], time[2]);
                TimeSpan leadTime = timePicker_arrival.Value.TimeOfDay;
                DateTime arr = DateTime.Parse(depart_time).AddMinutes(leadTime.TotalMinutes);
                string arrival_time = arr.ToString("yyyy-MM-dd HH-mm-ss");
                
                // 지역 이름으로부터 지역 ID를 얻어옴
                int startID = getID(textBox_depart.Text);
                int destinationID = getID(textBox_arrival.Text);
                if (startID == -1 || destinationID == -1)
                    return false;

                string executeStr = string.Copy(defaultStr);
                executeStr += string.Format("VALUE('{0}', {1}, {2}, '{3}', '{4}')",
                                            textBox_busID.Text, startID, destinationID, depart_time, arrival_time);
                parent.SendMessage((char)MainForm.MSG.DB_EXECUTE + "$" + executeStr);

                object[] result = parent.ReceiveMessage();
                if (MainForm.checkDBFailure(result))
                {
                    MessageBox.Show("프로세스 처리 중 DB에서 문제가 발생했습니다. 관리자에게 문의하십시오.");
                    return false;
                }
            }
            return true;
        }

        // 지역 이름으로부터 지역 ID를 얻어옴
        private int getID(string locName)
        {
            string queryStr = "SELECT id FROM lhjtest.destination ";
            queryStr += $"WHERE name = '{locName}'";
            parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
            object[] result = parent.ReceiveMessage();
            if (MainForm.checkDBFailure(result))
            {
                MessageBox.Show("프로세스 처리 중 DB에서 문제가 발생했습니다. 관리자에게 문의하십시오.");
                return -1;
            }

            return int.Parse(result[0].ToString());
        }

        // 기타 잡설정들
        // 복수 날짜 설정 시 요일 설정 체크박스 해제, 리스트박스 더블클릭 시 행선지 추가 등 ...
        private void checkBox_setWeekday_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_setWeekday.Checked)
                checkedListBox_weekdays.Enabled = true;
            else
                checkedListBox_weekdays.Enabled = false;
        }

        private void radioButton_multi_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_multi.Checked)
            {
                checkBox_setWeekday.Enabled = true;
                datePicker_multiFrom.Enabled = true;
                datePicker_multiTo.Enabled = true;
                datePicker_single.Enabled = false;
            }
            else
            {
                checkBox_setWeekday.Enabled = false;
                datePicker_multiFrom.Enabled = false;
                datePicker_multiTo.Enabled = false;
                datePicker_single.Enabled = true;
            }
        }

        private void button_addTime_Click(object sender, EventArgs e)
        {
            if (!listBox_departTime.Items.Contains(timePicker_depart.Value.TimeOfDay.ToString()))
                listBox_departTime.Items.Add(timePicker_depart.Value.TimeOfDay.ToString());
        }

        private void listBox_departTime_DoubleClick(object sender, EventArgs e)
        {
            if (listBox_departTime.SelectedIndices.Count != 0)
                listBox_departTime.Items.Remove(listBox_departTime.SelectedItem);
        }

        private void datePicker_multiFrom_ValueChanged(object sender, EventArgs e)
        {
            datePicker_multiTo.MinDate = datePicker_multiFrom.Value.AddDays(1);
        }

        private void btn_insertDepart_Click(object sender, EventArgs e)
        {
            if (listBox_locations.SelectedItems.Count == 0)
                return;

            if (!textBox_depart.Text.Equals(string.Empty))
                listBox_locations.Items.Add(textBox_depart.Text);
            textBox_depart.Text = listBox_locations.SelectedItem.ToString();
            listBox_locations.Items.Remove(listBox_locations.SelectedItem);
        }

        private void btn_insertArrival_Click(object sender, EventArgs e)
        {
            if (listBox_locations.SelectedItems.Count == 0)
                return;

            if (!textBox_arrival.Text.Equals(string.Empty))
                listBox_locations.Items.Add(textBox_arrival.Text);
            textBox_arrival.Text = listBox_locations.SelectedItem.ToString();
            listBox_locations.Items.Remove(listBox_locations.SelectedItem);
        }

        private void listBox_locations_DoubleClick(object sender, EventArgs e)
        {
            if (listBox_locations.SelectedIndices.Count == 0)
                return;

            if (textBox_depart.Text.Equals(string.Empty))
            {
                textBox_depart.Text = listBox_locations.SelectedItem.ToString();
                listBox_locations.Items.Remove(listBox_locations.SelectedItem);
            }
            else
            {
                if (!textBox_arrival.Text.Equals(string.Empty))
                    listBox_locations.Items.Add(textBox_arrival.Text);
                textBox_arrival.Text = listBox_locations.SelectedItem.ToString();
                listBox_locations.Items.Remove(listBox_locations.SelectedItem);
            }   
        }
    }
}