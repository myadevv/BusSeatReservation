namespace BusSeatReservation
{
    partial class ReserveForm
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
            this.busReservationControl1 = new BusSeatReservation.BusReservationControl();
            this.SuspendLayout();
            // 
            // departure
            // 
            this.departure.FullRowSelect = true;
            this.departure.GridLines = true;
            this.departure.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.departure.HideSelection = false;
            this.departure.Location = new System.Drawing.Point(14, 15);
            this.departure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.departure.MultiSelect = false;
            this.departure.Name = "departure";
            this.departure.Size = new System.Drawing.Size(177, 264);
            this.departure.TabIndex = 0;
            this.departure.UseCompatibleStateImageBehavior = false;
            // 
            // destination
            // 
            this.destination.FullRowSelect = true;
            this.destination.GridLines = true;
            this.destination.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.destination.HideSelection = false;
            this.destination.Location = new System.Drawing.Point(198, 15);
            this.destination.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.destination.MultiSelect = false;
            this.destination.Name = "destination";
            this.destination.Size = new System.Drawing.Size(177, 264);
            this.destination.TabIndex = 1;
            this.destination.UseCompatibleStateImageBehavior = false;
            // 
            // date
            // 
            this.date.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.date.FormattingEnabled = true;
            this.date.Location = new System.Drawing.Point(14, 292);
            this.date.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.date.Name = "date";
            this.date.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.date.Size = new System.Drawing.Size(361, 23);
            this.date.TabIndex = 2;
            // 
            // search_businfo
            // 
            this.search_businfo.Location = new System.Drawing.Point(14, 325);
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
            this.showbusinfo.FullRowSelect = true;
            this.showbusinfo.GridLines = true;
            this.showbusinfo.HideSelection = false;
            this.showbusinfo.Location = new System.Drawing.Point(382, 15);
            this.showbusinfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.showbusinfo.Name = "showbusinfo";
            this.showbusinfo.Size = new System.Drawing.Size(414, 366);
            this.showbusinfo.TabIndex = 4;
            this.showbusinfo.UseCompatibleStateImageBehavior = false;
            this.showbusinfo.View = System.Windows.Forms.View.Details;
            this.showbusinfo.DoubleClick += new System.EventHandler(this.showbusinfo_DoubleClick);
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
            this.busReservationControl1.TabIndex = 5;
            // 
            // ReserveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 411);
            this.Controls.Add(this.showbusinfo);
            this.Controls.Add(this.search_businfo);
            this.Controls.Add(this.date);
            this.Controls.Add(this.destination);
            this.Controls.Add(this.departure);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ReserveForm";
            this.Text = "OrderForm";
            this.Load += new System.EventHandler(this.OrderForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView departure;
        private System.Windows.Forms.ListView destination;
        private System.Windows.Forms.ComboBox date;
        private System.Windows.Forms.Button search_businfo;
        private System.Windows.Forms.ListView showbusinfo;
        private BusReservationControl busReservationControl1;
    }
}