namespace BusSeatReservation
{
    partial class OrderForm
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
            this.departure = new System.Windows.Forms.ListView();
            this.destination = new System.Windows.Forms.ListView();
            this.date = new System.Windows.Forms.ComboBox();
            this.search_businfo = new System.Windows.Forms.Button();
            this.showbusinfo = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.busReservationControl1 = new BusSeatReservation.BusReservationControl();
            this.reservationBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // departure
            // 
            this.departure.HideSelection = false;
            this.departure.Location = new System.Drawing.Point(13, 24);
            this.departure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.departure.Name = "departure";
            this.departure.Size = new System.Drawing.Size(177, 264);
            this.departure.TabIndex = 0;
            this.departure.UseCompatibleStateImageBehavior = false;
            // 
            // destination
            // 
            this.destination.HideSelection = false;
            this.destination.Location = new System.Drawing.Point(197, 24);
            this.destination.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.destination.Name = "destination";
            this.destination.Size = new System.Drawing.Size(177, 264);
            this.destination.TabIndex = 1;
            this.destination.UseCompatibleStateImageBehavior = false;
            // 
            // date
            // 
            this.date.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.date.FormattingEnabled = true;
            this.date.Location = new System.Drawing.Point(13, 301);
            this.date.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(361, 23);
            this.date.TabIndex = 2;
            // 
            // search_businfo
            // 
            this.search_businfo.Location = new System.Drawing.Point(13, 334);
            this.search_businfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.search_businfo.Name = "search_businfo";
            this.search_businfo.Size = new System.Drawing.Size(361, 58);
            this.search_businfo.TabIndex = 3;
            this.search_businfo.Text = "검색";
            this.search_businfo.UseVisualStyleBackColor = true;
            this.search_businfo.Click += new System.EventHandler(this.search_businfo_Click);
            // 
            // showbusinfo
            // 
            this.showbusinfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.showbusinfo.FullRowSelect = true;
            this.showbusinfo.GridLines = true;
            this.showbusinfo.HideSelection = false;
            this.showbusinfo.Location = new System.Drawing.Point(381, 24);
            this.showbusinfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.showbusinfo.MultiSelect = false;
            this.showbusinfo.Name = "showbusinfo";
            this.showbusinfo.Size = new System.Drawing.Size(436, 366);
            this.showbusinfo.TabIndex = 4;
            this.showbusinfo.UseCompatibleStateImageBehavior = false;
            this.showbusinfo.View = System.Windows.Forms.View.Details;
            this.showbusinfo.DoubleClick += new System.EventHandler(this.showbusinfo_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "버스";
            this.columnHeader1.Width = 68;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "출발";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "도착";
            this.columnHeader3.Width = 150;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.departure);
            this.groupBox1.Controls.Add(this.showbusinfo);
            this.groupBox1.Controls.Add(this.destination);
            this.groupBox1.Controls.Add(this.search_businfo);
            this.groupBox1.Controls.Add(this.date);
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(823, 401);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "버스 일정";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.busReservationControl1);
            this.groupBox2.Location = new System.Drawing.Point(838, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(257, 322);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "좌석";
            // 
            // busReservationControl1
            // 
            this.busReservationControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.busReservationControl1.Column = 5;
            this.busReservationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.busReservationControl1.Enterance = 2;
            this.busReservationControl1.Location = new System.Drawing.Point(3, 22);
            this.busReservationControl1.Margin = new System.Windows.Forms.Padding(1);
            this.busReservationControl1.Name = "busReservationControl1";
            this.busReservationControl1.Padding = new System.Windows.Forms.Padding(6);
            this.busReservationControl1.Row = 9;
            this.busReservationControl1.Seats = null;
            this.busReservationControl1.SelectIndex = 0;
            this.busReservationControl1.Size = new System.Drawing.Size(251, 296);
            this.busReservationControl1.TabIndex = 8;
            // 
            // reservationBtn
            // 
            this.reservationBtn.Location = new System.Drawing.Point(841, 344);
            this.reservationBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.reservationBtn.Name = "reservationBtn";
            this.reservationBtn.Size = new System.Drawing.Size(250, 58);
            this.reservationBtn.TabIndex = 5;
            this.reservationBtn.Text = "예매";
            this.reservationBtn.UseVisualStyleBackColor = true;
            this.reservationBtn.Click += new System.EventHandler(this.reservationBtn_Click);
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 425);
            this.Controls.Add(this.reservationBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "OrderForm";
            this.Text = "OrderForm";
            this.Load += new System.EventHandler(this.OrderForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView departure;
        private System.Windows.Forms.ListView destination;
        private System.Windows.Forms.ComboBox date;
        private System.Windows.Forms.Button search_businfo;
        private System.Windows.Forms.ListView showbusinfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private BusReservationControl busReservationControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button reservationBtn;
    }
}