using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusSeatReservation
{
    public partial class LoginForm : Form
    {
        MainForm parent;

        public LoginForm(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        /* 로그인 요청 : 서버에 DB 쿼리를 나타내는 바이트와 함께 쿼리문을 전송함
         * (해당 username과 password를 가진 데이터의 count)
         * 그 결과 데이터를 받아 count가 1이면 계정이 있는 것으로 로그인할 수 있게 됨
         * count가 0이면 일치하는 ID와 비밀번호가 없음
         */
        private void btn_login_Click(object sender, EventArgs e)
        {
            string queryStr = "SELECT userid FROM lhjtest.userdata ";
            queryStr += string.Format("WHERE username = '{0}' AND password = SHA2('{1}', 256)", textBox_id.Text, textBox_password.Text);
            
            parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
            object[] data = parent.ReceiveMessage();

            if (data.Length == 0)
            {
                MessageBox.Show("아이디와 비밀번호를 확인해주세요");
                return;
            }
            
            if (MainForm.checkDBFailure(data))
            {
                MessageBox.Show("DB에서 문제가 발생했습니다. 다시 시도해주세요.");
                return;
            }

            Setting.Instance.num = int.Parse(data[0].ToString());
            Setting.Instance.Id = textBox_id.Text;
            MessageBox.Show("로그인되었습니다.");
            Close();
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            RegisterForm form = new RegisterForm(parent);
            form.ShowDialog();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Setting.Instance.Id.Equals(string.Empty))
            {
                Application.Exit();
            }
        }
    }
}
