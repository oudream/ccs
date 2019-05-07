namespace Hong.Channel.NetWork
{
	partial class TcpClientConfigWin
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.PortRemoteEd = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.IPAddressRemoteEd = new Hong.Control.IPAddressBox.IPAddressBox();
			this.label4 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PortRemoteEd)).BeginInit();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.groupBox3);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(599, 179);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "一般设置";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.PortRemoteEd);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.IPAddressRemoteEd);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Location = new System.Drawing.Point(19, 19);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(560, 122);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "连接的目标地址、端口";
			// 
			// PortRemoteEd
			// 
			this.PortRemoteEd.Location = new System.Drawing.Point(313, 56);
			this.PortRemoteEd.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.PortRemoteEd.Name = "PortRemoteEd";
			this.PortRemoteEd.Size = new System.Drawing.Size(134, 21);
			this.PortRemoteEd.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(266, 58);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "端口：";
			// 
			// IPAddressRemoteEd
			// 
			this.IPAddressRemoteEd.BackColor = System.Drawing.SystemColors.Window;
			this.IPAddressRemoteEd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.IPAddressRemoteEd.Location = new System.Drawing.Point(91, 55);
			this.IPAddressRemoteEd.MinimumSize = new System.Drawing.Size(116, 21);
			this.IPAddressRemoteEd.Name = "IPAddressRemoteEd";
			this.IPAddressRemoteEd.ReadOnly = false;
			this.IPAddressRemoteEd.Size = new System.Drawing.Size(116, 21);
			this.IPAddressRemoteEd.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(26, 58);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(59, 12);
			this.label4.TabIndex = 1;
			this.label4.Text = "IP 地址：";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.button4);
			this.panel3.Controls.Add(this.button3);
			this.panel3.Controls.Add(this.button2);
			this.panel3.Controls.Add(this.button1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 179);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(599, 64);
			this.panel3.TabIndex = 12;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(164, 6);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(126, 46);
			this.button4.TabIndex = 24;
			this.button4.Text = "默认";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(19, 6);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(126, 46);
			this.button3.TabIndex = 23;
			this.button3.Text = "还原";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(453, 6);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(126, 46);
			this.button2.TabIndex = 1;
			this.button2.Text = "取消";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(309, 6);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(126, 46);
			this.button1.TabIndex = 0;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// TcpClientConfigWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(599, 243);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel3);
			this.Name = "TcpClientConfigWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "TcpClient配置";
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PortRemoteEd)).EndInit();
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.NumericUpDown PortRemoteEd;
		private System.Windows.Forms.Label label3;
		private Hong.Control.IPAddressBox.IPAddressBox IPAddressRemoteEd;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;

	}
}