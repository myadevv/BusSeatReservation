namespace BusSeatReservation
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1_setID = new System.Windows.Forms.TextBox();
            this.textBox2_setPW = new System.Windows.Forms.TextBox();
            this.textBox3_setEMAIL = new System.Windows.Forms.TextBox();
            this.label_id = new System.Windows.Forms.Label();
            this.label_pwd = new System.Windows.Forms.Label();
            this.label_email = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_register = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1_setID
            // 
            this.textBox1_setID.Location = new System.Drawing.Point(163, 152);
            this.textBox1_setID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1_setID.Name = "textBox1_setID";
            this.textBox1_setID.Size = new System.Drawing.Size(318, 25);
            this.textBox1_setID.TabIndex = 0;
            // 
            // textBox2_setPW
            // 
            this.textBox2_setPW.Location = new System.Drawing.Point(163, 212);
            this.textBox2_setPW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox2_setPW.Name = "textBox2_setPW";
            this.textBox2_setPW.PasswordChar = '●';
            this.textBox2_setPW.Size = new System.Drawing.Size(318, 25);
            this.textBox2_setPW.TabIndex = 1;
            // 
            // textBox3_setEMAIL
            // 
            this.textBox3_setEMAIL.Location = new System.Drawing.Point(163, 269);
            this.textBox3_setEMAIL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox3_setEMAIL.Name = "textBox3_setEMAIL";
            this.textBox3_setEMAIL.Size = new System.Drawing.Size(318, 25);
            this.textBox3_setEMAIL.TabIndex = 2;
            // 
            // label_id
            // 
            this.label_id.AutoSize = true;
            this.label_id.Location = new System.Drawing.Point(161, 134);
            this.label_id.Name = "label_id";
            this.label_id.Size = new System.Drawing.Size(52, 15);
            this.label_id.TabIndex = 3;
            this.label_id.Text = "아이디";
            // 
            // label_pwd
            // 
            this.label_pwd.AutoSize = true;
            this.label_pwd.Location = new System.Drawing.Point(161, 194);
            this.label_pwd.Name = "label_pwd";
            this.label_pwd.Size = new System.Drawing.Size(67, 15);
            this.label_pwd.TabIndex = 4;
            this.label_pwd.Text = "비밀번호";
            // 
            // label_email
            // 
            this.label_email.AutoSize = true;
            this.label_email.Location = new System.Drawing.Point(161, 250);
            this.label_email.Name = "label_email";
            this.label_email.Size = new System.Drawing.Size(52, 15);
            this.label_email.TabIndex = 5;
            this.label_email.Text = "이메일";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 39F);
            this.label4.Location = new System.Drawing.Point(14, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(744, 65);
            this.label4.TabIndex = 7;
            this.label4.Text = "버스 예약 관리 프로그램";
            // 
            // btn_register
            // 
            this.btn_register.Location = new System.Drawing.Point(163, 335);
            this.btn_register.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(319, 38);
            this.btn_register.TabIndex = 8;
            this.btn_register.Text = "가입하기";
            this.btn_register.UseVisualStyleBackColor = true;
            this.btn_register.Click += new System.EventHandler(this.btn_register_Click);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 411);
            this.Controls.Add(this.btn_register);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_email);
            this.Controls.Add(this.label_pwd);
            this.Controls.Add(this.label_id);
            this.Controls.Add(this.textBox3_setEMAIL);
            this.Controls.Add(this.textBox2_setPW);
            this.Controls.Add(this.textBox1_setID);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RegisterForm";
            this.Text = "RegisterForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1_setID;
        private System.Windows.Forms.TextBox textBox2_setPW;
        private System.Windows.Forms.TextBox textBox3_setEMAIL;
        private System.Windows.Forms.Label label_id;
        private System.Windows.Forms.Label label_pwd;
        private System.Windows.Forms.Label label_email;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_register;
    }
}