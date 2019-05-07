namespace Hong.Channel.SerialTester
{
    partial class ChannelTesterWin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.BufferTextBox = new System.Windows.Forms.TextBox();
			this.SendTextBox = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.SendDataAsTextEd = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.InfoTextBox = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ChannelTypeEd = new System.Windows.Forms.ComboBox();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.panel2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 42);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.BufferTextBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.SendTextBox);
			this.splitContainer1.Size = new System.Drawing.Size(764, 372);
			this.splitContainer1.SplitterDistance = 382;
			this.splitContainer1.TabIndex = 1;
			// 
			// BufferTextBox
			// 
			this.BufferTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BufferTextBox.Location = new System.Drawing.Point(0, 0);
			this.BufferTextBox.Multiline = true;
			this.BufferTextBox.Name = "BufferTextBox";
			this.BufferTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.BufferTextBox.Size = new System.Drawing.Size(382, 372);
			this.BufferTextBox.TabIndex = 1;
			this.BufferTextBox.TextChanged += new System.EventHandler(this.RecieveTextBox_TextChanged);
			// 
			// SendTextBox
			// 
			this.SendTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SendTextBox.Location = new System.Drawing.Point(0, 0);
			this.SendTextBox.Multiline = true;
			this.SendTextBox.Name = "SendTextBox";
			this.SendTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.SendTextBox.Size = new System.Drawing.Size(378, 372);
			this.SendTextBox.TabIndex = 2;
			this.SendTextBox.Text = "Hello world ";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
			this.statusStrip1.Location = new System.Drawing.Point(0, 530);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(764, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(60, 17);
			this.toolStripStatusLabel1.Text = "Received";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(15, 17);
			this.toolStripStatusLabel2.Text = "0";
			// 
			// toolStripStatusLabel5
			// 
			this.toolStripStatusLabel5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
			this.toolStripStatusLabel5.Size = new System.Drawing.Size(11, 17);
			this.toolStripStatusLabel5.Text = "|";
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(33, 17);
			this.toolStripStatusLabel3.Text = "Sent";
			// 
			// toolStripStatusLabel4
			// 
			this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
			this.toolStripStatusLabel4.Size = new System.Drawing.Size(15, 17);
			this.toolStripStatusLabel4.Text = "0";
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.SendDataAsTextEd);
			this.panel1.Controls.Add(this.checkBox1);
			this.panel1.Controls.Add(this.button4);
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(764, 42);
			this.panel1.TabIndex = 2;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.numericUpDown2);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox2.Location = new System.Drawing.Point(627, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(70, 42);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "接收缓存";
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.numericUpDown2.Location = new System.Drawing.Point(3, 17);
			this.numericUpDown2.Maximum = new decimal(new int[] {
            10240,
            0,
            0,
            0});
			this.numericUpDown2.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(64, 21);
			this.numericUpDown2.TabIndex = 0;
			this.numericUpDown2.Value = new decimal(new int[] {
            2048,
            0,
            0,
            0});
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.numericUpDown1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox1.Location = new System.Drawing.Point(557, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(70, 42);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "发送间隔";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.numericUpDown1.Location = new System.Drawing.Point(3, 17);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(64, 21);
			this.numericUpDown1.TabIndex = 0;
			this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// SendDataAsTextEd
			// 
			this.SendDataAsTextEd.AutoSize = true;
			this.SendDataAsTextEd.Checked = true;
			this.SendDataAsTextEd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.SendDataAsTextEd.Dock = System.Windows.Forms.DockStyle.Left;
			this.SendDataAsTextEd.Location = new System.Drawing.Point(473, 0);
			this.SendDataAsTextEd.Name = "SendDataAsTextEd";
			this.SendDataAsTextEd.Size = new System.Drawing.Size(84, 42);
			this.SendDataAsTextEd.TabIndex = 5;
			this.SendDataAsTextEd.Text = "按文本发送";
			this.SendDataAsTextEd.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.checkBox1.Location = new System.Drawing.Point(401, 0);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(72, 42);
			this.checkBox1.TabIndex = 4;
			this.checkBox1.Text = "定时发送";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// button4
			// 
			this.button4.Dock = System.Windows.Forms.DockStyle.Left;
			this.button4.Location = new System.Drawing.Point(326, 0);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 42);
			this.button4.TabIndex = 3;
			this.button4.Text = "发送";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Dock = System.Windows.Forms.DockStyle.Left;
			this.button3.Location = new System.Drawing.Point(251, 0);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 42);
			this.button3.TabIndex = 2;
			this.button3.Text = "配置";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Dock = System.Windows.Forms.DockStyle.Left;
			this.button2.Location = new System.Drawing.Point(176, 0);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 42);
			this.button2.TabIndex = 1;
			this.button2.Text = "关闭";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Left;
			this.button1.Location = new System.Drawing.Point(101, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 42);
			this.button1.TabIndex = 0;
			this.button1.Text = "打开";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.InfoTextBox);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 414);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(764, 116);
			this.panel2.TabIndex = 2;
			// 
			// InfoTextBox
			// 
			this.InfoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InfoTextBox.Location = new System.Drawing.Point(0, 0);
			this.InfoTextBox.Multiline = true;
			this.InfoTextBox.Name = "InfoTextBox";
			this.InfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.InfoTextBox.Size = new System.Drawing.Size(764, 116);
			this.InfoTextBox.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.ChannelTypeEd);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(101, 42);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "通道类型";
			// 
			// ChannelTypeEd
			// 
			this.ChannelTypeEd.FormattingEnabled = true;
			this.ChannelTypeEd.Items.AddRange(new object[] {
            "SerialPort",
            "TcpClient",
            "TcpListener",
            "UdpClient"});
			this.ChannelTypeEd.Location = new System.Drawing.Point(6, 16);
			this.ChannelTypeEd.Name = "ChannelTypeEd";
			this.ChannelTypeEd.Size = new System.Drawing.Size(89, 20);
			this.ChannelTypeEd.TabIndex = 0;
			this.ChannelTypeEd.SelectedIndexChanged += new System.EventHandler(this.ChannelTypeEd_SelectedIndexChanged);
			// 
			// ChannelTesterWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(764, 552);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "ChannelTesterWin";
			this.Text = "通道测试工具";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SerialTesterWin_FormClosing);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox BufferTextBox;
		private System.Windows.Forms.TextBox SendTextBox;
        private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.CheckBox SendDataAsTextEd;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TextBox InfoTextBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox ChannelTypeEd;
    }
}

