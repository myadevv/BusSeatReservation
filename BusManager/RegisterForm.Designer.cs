namespace BusManager
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
            this.btn_apply = new System.Windows.Forms.Button();
            this.listBox_locations = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton_single = new System.Windows.Forms.RadioButton();
            this.radioButton_multi = new System.Windows.Forms.RadioButton();
            this.checkBox_setWeekday = new System.Windows.Forms.CheckBox();
            this.datePicker_single = new System.Windows.Forms.DateTimePicker();
            this.datePicker_multiFrom = new System.Windows.Forms.DateTimePicker();
            this.datePicker_multiTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkedListBox_weekdays = new System.Windows.Forms.CheckedListBox();
            this.timePicker_depart = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.button_addTime = new System.Windows.Forms.Button();
            this.listBox_departTime = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_busID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_insertDepart = new System.Windows.Forms.Button();
            this.btn_insertArrival = new System.Windows.Forms.Button();
            this.textBox_depart = new System.Windows.Forms.TextBox();
            this.textBox_arrival = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.timePicker_arrival = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btn_apply
            // 
            this.btn_apply.Location = new System.Drawing.Point(542, 356);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(139, 52);
            this.btn_apply.TabIndex = 0;
            this.btn_apply.Text = "DB 추가하기";
            this.btn_apply.UseVisualStyleBackColor = true;
            this.btn_apply.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // listBox_locations
            // 
            this.listBox_locations.FormattingEnabled = true;
            this.listBox_locations.ItemHeight = 15;
            this.listBox_locations.Location = new System.Drawing.Point(629, 67);
            this.listBox_locations.Name = "listBox_locations";
            this.listBox_locations.Size = new System.Drawing.Size(87, 244);
            this.listBox_locations.Sorted = true;
            this.listBox_locations.TabIndex = 1;
            this.listBox_locations.DoubleClick += new System.EventHandler(this.listBox_locations_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(653, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "지역";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(509, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "출발";
            // 
            // radioButton_single
            // 
            this.radioButton_single.AutoSize = true;
            this.radioButton_single.Checked = true;
            this.radioButton_single.Location = new System.Drawing.Point(51, 45);
            this.radioButton_single.Name = "radioButton_single";
            this.radioButton_single.Size = new System.Drawing.Size(158, 19);
            this.radioButton_single.TabIndex = 4;
            this.radioButton_single.TabStop = true;
            this.radioButton_single.Text = "특정 날짜에만 추가";
            this.radioButton_single.UseVisualStyleBackColor = true;
            // 
            // radioButton_multi
            // 
            this.radioButton_multi.AutoSize = true;
            this.radioButton_multi.Location = new System.Drawing.Point(51, 87);
            this.radioButton_multi.Name = "radioButton_multi";
            this.radioButton_multi.Size = new System.Drawing.Size(158, 19);
            this.radioButton_multi.TabIndex = 5;
            this.radioButton_multi.Text = "특정 기간동안 추가";
            this.radioButton_multi.UseVisualStyleBackColor = true;
            this.radioButton_multi.CheckedChanged += new System.EventHandler(this.radioButton_multi_CheckedChanged);
            // 
            // checkBox_setWeekday
            // 
            this.checkBox_setWeekday.AutoSize = true;
            this.checkBox_setWeekday.Enabled = false;
            this.checkBox_setWeekday.Location = new System.Drawing.Point(51, 151);
            this.checkBox_setWeekday.Name = "checkBox_setWeekday";
            this.checkBox_setWeekday.Size = new System.Drawing.Size(129, 19);
            this.checkBox_setWeekday.TabIndex = 6;
            this.checkBox_setWeekday.Text = "요일 별로 추가";
            this.checkBox_setWeekday.UseVisualStyleBackColor = true;
            this.checkBox_setWeekday.CheckedChanged += new System.EventHandler(this.checkBox_setWeekday_CheckedChanged);
            // 
            // datePicker_single
            // 
            this.datePicker_single.Location = new System.Drawing.Point(215, 40);
            this.datePicker_single.Name = "datePicker_single";
            this.datePicker_single.Size = new System.Drawing.Size(200, 25);
            this.datePicker_single.TabIndex = 7;
            this.datePicker_single.Value = new System.DateTime(2020, 12, 25, 23, 59, 59, 0);
            // 
            // datePicker_multiFrom
            // 
            this.datePicker_multiFrom.Enabled = false;
            this.datePicker_multiFrom.Location = new System.Drawing.Point(215, 82);
            this.datePicker_multiFrom.Name = "datePicker_multiFrom";
            this.datePicker_multiFrom.Size = new System.Drawing.Size(200, 25);
            this.datePicker_multiFrom.TabIndex = 8;
            this.datePicker_multiFrom.Value = new System.DateTime(2020, 12, 25, 23, 59, 59, 0);
            this.datePicker_multiFrom.ValueChanged += new System.EventHandler(this.datePicker_multiFrom_ValueChanged);
            // 
            // datePicker_multiTo
            // 
            this.datePicker_multiTo.Enabled = false;
            this.datePicker_multiTo.Location = new System.Drawing.Point(215, 113);
            this.datePicker_multiTo.Name = "datePicker_multiTo";
            this.datePicker_multiTo.Size = new System.Drawing.Size(200, 25);
            this.datePicker_multiTo.TabIndex = 9;
            this.datePicker_multiTo.Value = new System.DateTime(2020, 12, 25, 23, 59, 59, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(421, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "부터";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(421, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "까지";
            // 
            // checkedListBox_weekdays
            // 
            this.checkedListBox_weekdays.CheckOnClick = true;
            this.checkedListBox_weekdays.Enabled = false;
            this.checkedListBox_weekdays.FormattingEnabled = true;
            this.checkedListBox_weekdays.Items.AddRange(new object[] {
            "Sun",
            "Mon",
            "Tue",
            "Wed",
            "Thu",
            "Fri",
            "Sat"});
            this.checkedListBox_weekdays.Location = new System.Drawing.Point(215, 151);
            this.checkedListBox_weekdays.Name = "checkedListBox_weekdays";
            this.checkedListBox_weekdays.Size = new System.Drawing.Size(75, 144);
            this.checkedListBox_weekdays.TabIndex = 12;
            // 
            // timePicker_depart
            // 
            this.timePicker_depart.CustomFormat = "tt HH:mm";
            this.timePicker_depart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePicker_depart.Location = new System.Drawing.Point(128, 319);
            this.timePicker_depart.Name = "timePicker_depart";
            this.timePicker_depart.ShowUpDown = true;
            this.timePicker_depart.Size = new System.Drawing.Size(107, 25);
            this.timePicker_depart.TabIndex = 14;
            this.timePicker_depart.Value = new System.DateTime(2020, 12, 6, 12, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 326);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "출발 시간";
            // 
            // button_addTime
            // 
            this.button_addTime.Location = new System.Drawing.Point(241, 320);
            this.button_addTime.Name = "button_addTime";
            this.button_addTime.Size = new System.Drawing.Size(58, 26);
            this.button_addTime.TabIndex = 16;
            this.button_addTime.Text = "추가";
            this.button_addTime.UseVisualStyleBackColor = true;
            this.button_addTime.Click += new System.EventHandler(this.button_addTime_Click);
            // 
            // listBox_departTime
            // 
            this.listBox_departTime.FormattingEnabled = true;
            this.listBox_departTime.ItemHeight = 15;
            this.listBox_departTime.Location = new System.Drawing.Point(344, 191);
            this.listBox_departTime.Name = "listBox_departTime";
            this.listBox_departTime.Size = new System.Drawing.Size(104, 184);
            this.listBox_departTime.Sorted = true;
            this.listBox_departTime.TabIndex = 17;
            this.listBox_departTime.DoubleClick += new System.EventHandler(this.listBox_departTime_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(370, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "시간표";
            // 
            // textBox_busID
            // 
            this.textBox_busID.Location = new System.Drawing.Point(128, 381);
            this.textBox_busID.MaxLength = 8;
            this.textBox_busID.Name = "textBox_busID";
            this.textBox_busID.Size = new System.Drawing.Size(107, 25);
            this.textBox_busID.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 384);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 15);
            this.label7.TabIndex = 20;
            this.label7.Text = "버스 ID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(509, 206);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 15);
            this.label9.TabIndex = 23;
            this.label9.Text = "도착";
            // 
            // btn_insertDepart
            // 
            this.btn_insertDepart.Location = new System.Drawing.Point(584, 116);
            this.btn_insertDepart.Name = "btn_insertDepart";
            this.btn_insertDepart.Size = new System.Drawing.Size(24, 24);
            this.btn_insertDepart.TabIndex = 24;
            this.btn_insertDepart.Text = "◀";
            this.btn_insertDepart.UseVisualStyleBackColor = true;
            this.btn_insertDepart.Click += new System.EventHandler(this.btn_insertDepart_Click);
            // 
            // btn_insertArrival
            // 
            this.btn_insertArrival.Location = new System.Drawing.Point(584, 235);
            this.btn_insertArrival.Name = "btn_insertArrival";
            this.btn_insertArrival.Size = new System.Drawing.Size(24, 24);
            this.btn_insertArrival.TabIndex = 25;
            this.btn_insertArrival.Text = "◀";
            this.btn_insertArrival.UseVisualStyleBackColor = true;
            this.btn_insertArrival.Click += new System.EventHandler(this.btn_insertArrival_Click);
            // 
            // textBox_depart
            // 
            this.textBox_depart.Location = new System.Drawing.Point(492, 115);
            this.textBox_depart.Name = "textBox_depart";
            this.textBox_depart.ReadOnly = true;
            this.textBox_depart.Size = new System.Drawing.Size(70, 25);
            this.textBox_depart.TabIndex = 26;
            // 
            // textBox_arrival
            // 
            this.textBox_arrival.Location = new System.Drawing.Point(492, 234);
            this.textBox_arrival.Name = "textBox_arrival";
            this.textBox_arrival.ReadOnly = true;
            this.textBox_arrival.Size = new System.Drawing.Size(70, 25);
            this.textBox_arrival.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 357);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 15);
            this.label10.TabIndex = 29;
            this.label10.Text = "소요 시간";
            // 
            // timePicker_arrival
            // 
            this.timePicker_arrival.CustomFormat = "HH:mm";
            this.timePicker_arrival.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePicker_arrival.Location = new System.Drawing.Point(128, 350);
            this.timePicker_arrival.Name = "timePicker_arrival";
            this.timePicker_arrival.ShowUpDown = true;
            this.timePicker_arrival.Size = new System.Drawing.Size(107, 25);
            this.timePicker_arrival.TabIndex = 28;
            this.timePicker_arrival.Value = new System.DateTime(2020, 12, 6, 0, 0, 0, 0);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 449);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.timePicker_arrival);
            this.Controls.Add(this.textBox_arrival);
            this.Controls.Add(this.textBox_depart);
            this.Controls.Add(this.btn_insertArrival);
            this.Controls.Add(this.btn_insertDepart);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_busID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listBox_departTime);
            this.Controls.Add(this.button_addTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.timePicker_depart);
            this.Controls.Add(this.checkedListBox_weekdays);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.datePicker_multiTo);
            this.Controls.Add(this.datePicker_multiFrom);
            this.Controls.Add(this.datePicker_single);
            this.Controls.Add(this.checkBox_setWeekday);
            this.Controls.Add(this.radioButton_multi);
            this.Controls.Add(this.radioButton_single);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox_locations);
            this.Controls.Add(this.btn_apply);
            this.Name = "RegisterForm";
            this.Text = "RegisterForm";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_apply;
        private System.Windows.Forms.ListBox listBox_locations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton_single;
        private System.Windows.Forms.RadioButton radioButton_multi;
        private System.Windows.Forms.CheckBox checkBox_setWeekday;
        private System.Windows.Forms.DateTimePicker datePicker_single;
        private System.Windows.Forms.DateTimePicker datePicker_multiFrom;
        private System.Windows.Forms.DateTimePicker datePicker_multiTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox checkedListBox_weekdays;
        private System.Windows.Forms.DateTimePicker timePicker_depart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_addTime;
        private System.Windows.Forms.ListBox listBox_departTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_busID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_insertDepart;
        private System.Windows.Forms.Button btn_insertArrival;
        private System.Windows.Forms.TextBox textBox_depart;
        private System.Windows.Forms.TextBox textBox_arrival;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker timePicker_arrival;
    }
}