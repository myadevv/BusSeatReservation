namespace CardGame
{
    partial class ServerForm
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
            this.MyRichTextBox = new System.Windows.Forms.RichTextBox();
            this.listBoxPlayers = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_command = new System.Windows.Forms.TextBox();
            this.button_execute = new System.Windows.Forms.Button();
            this.button_query = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MyRichTextBox
            // 
            this.MyRichTextBox.Location = new System.Drawing.Point(12, 12);
            this.MyRichTextBox.Name = "MyRichTextBox";
            this.MyRichTextBox.ReadOnly = true;
            this.MyRichTextBox.Size = new System.Drawing.Size(540, 469);
            this.MyRichTextBox.TabIndex = 0;
            this.MyRichTextBox.Text = "";
            // 
            // listBoxPlayers
            // 
            this.listBoxPlayers.FormattingEnabled = true;
            this.listBoxPlayers.ItemHeight = 15;
            this.listBoxPlayers.Location = new System.Drawing.Point(569, 46);
            this.listBoxPlayers.Name = "listBoxPlayers";
            this.listBoxPlayers.Size = new System.Drawing.Size(121, 424);
            this.listBoxPlayers.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(595, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "User List";
            // 
            // textBox_command
            // 
            this.textBox_command.Location = new System.Drawing.Point(27, 489);
            this.textBox_command.Name = "textBox_command";
            this.textBox_command.Size = new System.Drawing.Size(493, 25);
            this.textBox_command.TabIndex = 4;
            // 
            // button_execute
            // 
            this.button_execute.Location = new System.Drawing.Point(526, 491);
            this.button_execute.Name = "button_execute";
            this.button_execute.Size = new System.Drawing.Size(75, 23);
            this.button_execute.TabIndex = 5;
            this.button_execute.Text = "Execute";
            this.button_execute.UseVisualStyleBackColor = true;
            this.button_execute.Click += new System.EventHandler(this.button_execute_Click);
            // 
            // button_query
            // 
            this.button_query.Location = new System.Drawing.Point(607, 491);
            this.button_query.Name = "button_query";
            this.button_query.Size = new System.Drawing.Size(75, 23);
            this.button_query.TabIndex = 6;
            this.button_query.Text = "Query";
            this.button_query.UseVisualStyleBackColor = true;
            this.button_query.Click += new System.EventHandler(this.button_query_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 524);
            this.Controls.Add(this.button_query);
            this.Controls.Add(this.button_execute);
            this.Controls.Add(this.textBox_command);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxPlayers);
            this.Controls.Add(this.MyRichTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ServerForm";
            this.Text = "BusServer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox MyRichTextBox;
        private System.Windows.Forms.ListBox listBoxPlayers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_command;
        private System.Windows.Forms.Button button_execute;
        private System.Windows.Forms.Button button_query;
    }
}

