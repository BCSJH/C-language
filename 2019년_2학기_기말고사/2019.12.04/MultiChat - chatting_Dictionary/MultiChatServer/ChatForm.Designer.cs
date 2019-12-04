namespace MultiChatServer
{
    partial class ChatForm
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
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("참여자");
            this.tblMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.txtTTS = new System.Windows.Forms.TextBox();
            this.lblTTS = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtHistory = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.tblMainLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMainLayout
            // 
            this.tblMainLayout.ColumnCount = 6;
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblMainLayout.Controls.Add(this.txtTTS, 1, 5);
            this.tblMainLayout.Controls.Add(this.lblTTS, 0, 5);
            this.tblMainLayout.Controls.Add(this.lblAddress, 0, 0);
            this.tblMainLayout.Controls.Add(this.txtAddress, 1, 0);
            this.tblMainLayout.Controls.Add(this.btnStart, 4, 0);
            this.tblMainLayout.Controls.Add(this.btnSend, 4, 5);
            this.tblMainLayout.Controls.Add(this.lblPort, 2, 0);
            this.tblMainLayout.Controls.Add(this.txtPort, 3, 0);
            this.tblMainLayout.Controls.Add(this.txtHistory, 5, 1);
            this.tblMainLayout.Controls.Add(this.button1, 1, 1);
            this.tblMainLayout.Controls.Add(this.listView1, 0, 1);
            this.tblMainLayout.Controls.Add(this.button2, 2, 1);
            this.tblMainLayout.Controls.Add(this.button3, 3, 1);
            this.tblMainLayout.Controls.Add(this.button4, 4, 1);
            this.tblMainLayout.Controls.Add(this.button5, 1, 2);
            this.tblMainLayout.Controls.Add(this.button6, 2, 2);
            this.tblMainLayout.Controls.Add(this.button7, 3, 2);
            this.tblMainLayout.Controls.Add(this.button8, 4, 2);
            this.tblMainLayout.Controls.Add(this.button9, 1, 3);
            this.tblMainLayout.Controls.Add(this.button10, 2, 3);
            this.tblMainLayout.Controls.Add(this.button11, 3, 3);
            this.tblMainLayout.Controls.Add(this.button12, 4, 3);
            this.tblMainLayout.Controls.Add(this.button13, 1, 4);
            this.tblMainLayout.Controls.Add(this.button14, 2, 4);
            this.tblMainLayout.Controls.Add(this.button15, 3, 4);
            this.tblMainLayout.Controls.Add(this.button16, 4, 4);
            this.tblMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tblMainLayout.Name = "tblMainLayout";
            this.tblMainLayout.Padding = new System.Windows.Forms.Padding(8);
            this.tblMainLayout.RowCount = 6;
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMainLayout.Size = new System.Drawing.Size(731, 481);
            this.tblMainLayout.TabIndex = 1;
            this.tblMainLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.tblMainLayout_Paint);
            // 
            // txtTTS
            // 
            this.tblMainLayout.SetColumnSpan(this.txtTTS, 3);
            this.txtTTS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTTS.Location = new System.Drawing.Point(112, 442);
            this.txtTTS.Margin = new System.Windows.Forms.Padding(4, 2, 3, 3);
            this.txtTTS.MaxLength = 260;
            this.txtTTS.Name = "txtTTS";
            this.txtTTS.Size = new System.Drawing.Size(293, 32);
            this.txtTTS.TabIndex = 7;
            // 
            // lblTTS
            // 
            this.lblTTS.AutoSize = true;
            this.lblTTS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTTS.Location = new System.Drawing.Point(9, 441);
            this.lblTTS.Margin = new System.Windows.Forms.Padding(1);
            this.lblTTS.Name = "lblTTS";
            this.lblTTS.Size = new System.Drawing.Size(98, 31);
            this.lblTTS.TabIndex = 6;
            this.lblTTS.Text = "보낼 텍스트";
            this.lblTTS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAddress.Location = new System.Drawing.Point(9, 9);
            this.lblAddress.Margin = new System.Windows.Forms.Padding(1);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(98, 30);
            this.lblAddress.TabIndex = 0;
            this.lblAddress.Text = "서버 주소";
            this.lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.White;
            this.txtAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAddress.Location = new System.Drawing.Point(112, 10);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4, 2, 3, 3);
            this.txtAddress.MaxLength = 260;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(93, 32);
            this.txtAddress.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.Location = new System.Drawing.Point(409, 9);
            this.btnStart.Margin = new System.Windows.Forms.Padding(1);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(98, 30);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "시작";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BeginStartServer);
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSend.Location = new System.Drawing.Point(409, 441);
            this.btnSend.Margin = new System.Windows.Forms.Padding(1);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(98, 31);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "보내기";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.OnSendData);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPort.Location = new System.Drawing.Point(209, 9);
            this.lblPort.Margin = new System.Windows.Forms.Padding(1);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(98, 30);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "포트 번호";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPort
            // 
            this.txtPort.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtPort.Location = new System.Drawing.Point(312, 10);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 2, 3, 3);
            this.txtPort.MaxLength = 5;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(93, 32);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "15000";
            // 
            // txtHistory
            // 
            this.txtHistory.BackColor = System.Drawing.Color.White;
            this.txtHistory.Location = new System.Drawing.Point(512, 43);
            this.txtHistory.Margin = new System.Windows.Forms.Padding(4, 3, 2, 3);
            this.txtHistory.Multiline = true;
            this.txtHistory.Name = "txtHistory";
            this.txtHistory.ReadOnly = true;
            this.tblMainLayout.SetRowSpan(this.txtHistory, 4);
            this.txtHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHistory.Size = new System.Drawing.Size(208, 394);
            this.txtHistory.TabIndex = 5;
            this.txtHistory.Text = "채팅";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(111, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 93);
            this.button1.TabIndex = 10;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.GridLines = true;
            listViewItem2.Tag = "";
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listView1.Location = new System.Drawing.Point(11, 43);
            this.listView1.Name = "listView1";
            this.tblMainLayout.SetRowSpan(this.listView1, 4);
            this.listView1.Size = new System.Drawing.Size(94, 394);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(211, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 93);
            this.button2.TabIndex = 11;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(311, 43);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 93);
            this.button3.TabIndex = 12;
            this.button3.Text = "3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(411, 43);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 93);
            this.button4.TabIndex = 13;
            this.button4.Text = "4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(111, 143);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(93, 93);
            this.button5.TabIndex = 14;
            this.button5.Text = "5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(211, 143);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(93, 93);
            this.button6.TabIndex = 15;
            this.button6.Text = "6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(311, 143);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(93, 93);
            this.button7.TabIndex = 16;
            this.button7.Text = "7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(411, 143);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(93, 93);
            this.button8.TabIndex = 17;
            this.button8.Text = "8";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(111, 243);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(93, 93);
            this.button9.TabIndex = 18;
            this.button9.Text = "9";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(211, 243);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(93, 93);
            this.button10.TabIndex = 19;
            this.button10.Text = "10";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(311, 243);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(93, 93);
            this.button11.TabIndex = 20;
            this.button11.Text = "11";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(411, 243);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(93, 93);
            this.button12.TabIndex = 21;
            this.button12.Text = "12";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(111, 343);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(93, 93);
            this.button13.TabIndex = 22;
            this.button13.Text = "13";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(211, 343);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(93, 93);
            this.button14.TabIndex = 23;
            this.button14.Text = "14";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(311, 343);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(93, 93);
            this.button15.TabIndex = 24;
            this.button15.Text = "15";
            this.button15.UseVisualStyleBackColor = true;
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(411, 343);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(93, 93);
            this.button16.TabIndex = 25;
            this.button16.Text = "16";
            this.button16.UseVisualStyleBackColor = true;
            // 
            // ChatForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(731, 481);
            this.Controls.Add(this.tblMainLayout);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F);
            this.Name = "ChatForm";
            this.Text = "Multi Chat Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Load += new System.EventHandler(this.OnFormLoaded);
            this.tblMainLayout.ResumeLayout(false);
            this.tblMainLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMainLayout;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox txtHistory;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblTTS;
        private System.Windows.Forms.TextBox txtTTS;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
    }
}

