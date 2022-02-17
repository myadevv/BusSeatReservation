using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BusSeatReservation
{
    public partial class RegisterForm : Form
    {
        MainForm parent;
        public RegisterForm(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            if (textBox1_setID.Text.Length == 0)
            {
                MessageBox.Show("ID를 입력해주세요.");
                return;
            }
            if (textBox2_setPW.Text.Length == 0)
            {
                MessageBox.Show("패스워드를 입력해주세요.");
                return;
            }
            if (textBox3_setEMAIL.Text.Length == 0)
            {
                MessageBox.Show("이메일을 입력해주세요.");
                return;
            }
            if (!IsValidEmail(textBox3_setEMAIL.Text))
            {
                MessageBox.Show("이메일 양식이 올바른 지 확인해주세요.");
                return;
            }
            
            try
            {
                string queryStr = "SELECT count(*) FROM lhjtest.userdata ";
                queryStr += string.Format("WHERE username = '{0}'", textBox1_setID.Text);
                parent.SendMessage((char)MainForm.MSG.DB_QUERY + "$" + queryStr);
                object[] data = parent.ReceiveMessage();

                if (MainForm.checkDBFailure(data)) {
                    MessageBox.Show("DB에서 문제가 발생했습니다. 다시 시도해주세요.");
                    return;
                }
                
                int getIdExist = Convert.ToInt32(data[0]);
                if (getIdExist == 1)
                {
                    MessageBox.Show("이미 존재하는 아이디입니다.");
                    return;
                }

                string executeStr = "INSERT INTO lhjtest.userdata(username,password,email) ";
                executeStr += string.Format("value('{0}', SHA2('{1}', 256), '{2}')", textBox1_setID.Text, textBox2_setPW.Text, textBox3_setEMAIL.Text);

                parent.SendMessage((char)MainForm.MSG.DB_EXECUTE + "$" + executeStr);
                object[] result = parent.ReceiveMessage();
                
                if (MainForm.checkDBFailure(result)) {
                    MessageBox.Show("DB에서 문제가 발생했습니다. 다시 시도해주세요.");
                    return;
                }

                MessageBox.Show("회원가입 완료되었습니다. 로그인해 주세요");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        // check email valid. https://www.csharpstudy.com/Practical/Prac-validemail.aspx
        public bool IsValidEmail(string email)
        {
            bool valid = Regex.IsMatch(email, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
            return valid;
        }
    }
}
