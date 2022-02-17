namespace BusSeatReservation
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
            this.listView_reserve = new System.Windows.Forms.ListView();
            this.btn_reserve = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView_reserve
            // 
            this.listView_reserve.FullRowSelect = true;
            this.listView_reserve.GridLines = true;
            this.listView_reserve.HideSelection = false;
            this.listView_reserve.LabelWrap = false;
            this.listView_reserve.Location = new System.Drawing.Point(12, 12);
            this.listView_reserve.Name = "listView_reserve";
            this.listView_reserve.Size = new System.Drawing.Size(534, 321);
            this.listView_reserve.TabIndex = 0;
            this.listView_reserve.UseCompatibleStateImageBehavior = false;
            this.listView_reserve.View = System.Windows.Forms.View.Details;
            // 
            // btn_reserve
            // 
            this.btn_reserve.Location = new System.Drawing.Point(569, 66);
            this.btn_reserve.Name = "btn_reserve";
            this.btn_reserve.Size = new System.Drawing.Size(105, 48);
            this.btn_reserve.TabIndex = 4;
            this.btn_reserve.Text = "버스 예약";
            this.btn_reserve.UseVisualStyleBackColor = true;
            this.btn_reserve.Click += new System.EventHandler(this.btn_reserve_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(569, 121);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(105, 48);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "예약 취소";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 346);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_reserve);
            this.Controls.Add(this.listView_reserve);
            this.Name = "MainForm";
            this.Text = " MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_reserve;
        private System.Windows.Forms.Button btn_reserve;
        private System.Windows.Forms.Button btn_cancel;
    }
}

