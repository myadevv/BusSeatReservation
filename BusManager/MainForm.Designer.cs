namespace BusManager
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView_bus = new System.Windows.Forms.ListView();
            this.listView_reserve = new System.Windows.Forms.ListView();
            this.btn_register = new System.Windows.Forms.Button();
            this.btn_deleteBusdata = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView_bus
            // 
            this.listView_bus.FullRowSelect = true;
            this.listView_bus.HideSelection = false;
            this.listView_bus.Location = new System.Drawing.Point(12, 48);
            this.listView_bus.Name = "listView_bus";
            this.listView_bus.Size = new System.Drawing.Size(600, 300);
            this.listView_bus.TabIndex = 0;
            this.listView_bus.UseCompatibleStateImageBehavior = false;
            this.listView_bus.View = System.Windows.Forms.View.Details;
            // 
            // listView_reserve
            // 
            this.listView_reserve.FullRowSelect = true;
            this.listView_reserve.HideSelection = false;
            this.listView_reserve.Location = new System.Drawing.Point(12, 403);
            this.listView_reserve.Name = "listView_reserve";
            this.listView_reserve.Size = new System.Drawing.Size(450, 300);
            this.listView_reserve.TabIndex = 1;
            this.listView_reserve.UseCompatibleStateImageBehavior = false;
            this.listView_reserve.View = System.Windows.Forms.View.Details;
            // 
            // btn_register
            // 
            this.btn_register.Location = new System.Drawing.Point(492, 403);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(120, 120);
            this.btn_register.TabIndex = 2;
            this.btn_register.Text = "버스 정보 등록";
            this.btn_register.UseVisualStyleBackColor = true;
            this.btn_register.Click += new System.EventHandler(this.btn_register_Click);
            // 
            // btn_deleteBusdata
            // 
            this.btn_deleteBusdata.Location = new System.Drawing.Point(492, 583);
            this.btn_deleteBusdata.Name = "btn_deleteBusdata";
            this.btn_deleteBusdata.Size = new System.Drawing.Size(120, 120);
            this.btn_deleteBusdata.TabIndex = 3;
            this.btn_deleteBusdata.Text = "버스 정보 삭제";
            this.btn_deleteBusdata.UseVisualStyleBackColor = true;
            this.btn_deleteBusdata.Click += new System.EventHandler(this.btn_deleteBusdata_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(274, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "버스 정보";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 368);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "예약 정보";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 731);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_deleteBusdata);
            this.Controls.Add(this.btn_register);
            this.Controls.Add(this.listView_reserve);
            this.Controls.Add(this.listView_bus);
            this.Name = "MainForm";
            this.Text = "버스 관리";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_bus;
        private System.Windows.Forms.ListView listView_reserve;
        private System.Windows.Forms.Button btn_register;
        private System.Windows.Forms.Button btn_deleteBusdata;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

