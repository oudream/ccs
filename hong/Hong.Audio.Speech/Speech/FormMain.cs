using System;
using System.Windows.Forms;
using System.IO;
using System.Text;
using InfoQuick.SinoVoice.Tts;

namespace Hong.Audio.Speech
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class formMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxContent;
		private System.Windows.Forms.Button buttonPlay;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonPause;
		private System.Windows.Forms.Button buttonInit;
		private System.Windows.Forms.Button buttonSetting;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button buttonResume;
		private System.Windows.Forms.Button buttonPlayToFile;
		private System.Windows.Forms.Button buttonCancle;
		private System.Windows.Forms.Button buttonOpenFile;

		//User defined data.
		private bool bInitialed = false;
		private int iFileFormat = 0;
		private int iFileHead = 0;

		public formMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			
			int iErr = Jtts.jTTS_Init(null, null);
			if (Jtts.ERR_NONE == iErr || Jtts.ERR_ALREADYINIT == iErr)
			{
				bInitialed = true;
			}
			Jtts.JTTS_CONFIG config = new InfoQuick.SinoVoice.Tts.Jtts.JTTS_CONFIG();
			iErr = Jtts.jTTS_Get(out config);
			config.nCodePage = (ushort)Encoding.Default.CodePage;
			Jtts.jTTS_Set(ref config);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBoxContent = new System.Windows.Forms.TextBox();
			this.buttonPlay = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.buttonPause = new System.Windows.Forms.Button();
			this.buttonInit = new System.Windows.Forms.Button();
			this.buttonSetting = new System.Windows.Forms.Button();
			this.buttonResume = new System.Windows.Forms.Button();
			this.buttonPlayToFile = new System.Windows.Forms.Button();
			this.buttonCancle = new System.Windows.Forms.Button();
			this.buttonOpenFile = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxContent
			// 
			this.textBoxContent.Location = new System.Drawing.Point(8, 8);
			this.textBoxContent.Multiline = true;
			this.textBoxContent.Name = "textBoxContent";
			this.textBoxContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxContent.Size = new System.Drawing.Size(416, 312);
			this.textBoxContent.TabIndex = 0;
			this.textBoxContent.Text = "��ͨ���������ϳ���ʾ����";
			// 
			// buttonPlay
			// 
			this.buttonPlay.Location = new System.Drawing.Point(440, 16);
			this.buttonPlay.Name = "buttonPlay";
			this.buttonPlay.TabIndex = 1;
			this.buttonPlay.Text = "����";
			this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(440, 48);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.TabIndex = 2;
			this.buttonStop.Text = "ֹͣ";
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// buttonPause
			// 
			this.buttonPause.Location = new System.Drawing.Point(440, 80);
			this.buttonPause.Name = "buttonPause";
			this.buttonPause.TabIndex = 3;
			this.buttonPause.Text = "��ͣ";
			this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
			// 
			// buttonInit
			// 
			this.buttonInit.Location = new System.Drawing.Point(440, 216);
			this.buttonInit.Name = "buttonInit";
			this.buttonInit.TabIndex = 5;
			this.buttonInit.Text = "��ʼ��";
			this.buttonInit.Click += new System.EventHandler(this.buttonInit_Click);
			// 
			// buttonSetting
			// 
			this.buttonSetting.Location = new System.Drawing.Point(440, 248);
			this.buttonSetting.Name = "buttonSetting";
			this.buttonSetting.TabIndex = 6;
			this.buttonSetting.Text = "����";
			this.buttonSetting.Click += new System.EventHandler(this.buttonSetting_Click);
			// 
			// buttonResume
			// 
			this.buttonResume.Location = new System.Drawing.Point(440, 112);
			this.buttonResume.Name = "buttonResume";
			this.buttonResume.TabIndex = 8;
			this.buttonResume.Text = "����";
			this.buttonResume.Click += new System.EventHandler(this.buttonResume_Click);
			// 
			// buttonPlayToFile
			// 
			this.buttonPlayToFile.Location = new System.Drawing.Point(440, 144);
			this.buttonPlayToFile.Name = "buttonPlayToFile";
			this.buttonPlayToFile.TabIndex = 9;
			this.buttonPlayToFile.Text = "�ϳɵ��ļ�";
			this.buttonPlayToFile.Click += new System.EventHandler(this.buttonPlayToFile_Click);
			// 
			// buttonCancle
			// 
			this.buttonCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancle.Location = new System.Drawing.Point(440, 288);
			this.buttonCancle.Name = "buttonCancle";
			this.buttonCancle.TabIndex = 10;
			this.buttonCancle.Text = "�˳�";
			this.buttonCancle.Click += new System.EventHandler(this.buttonCancle_Click);
			// 
			// buttonOpenFile
			// 
			this.buttonOpenFile.Location = new System.Drawing.Point(440, 176);
			this.buttonOpenFile.Name = "buttonOpenFile";
			this.buttonOpenFile.TabIndex = 11;
			this.buttonOpenFile.Text = "���ı�";
			this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
			// 
			// formMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.buttonCancle;
			this.ClientSize = new System.Drawing.Size(528, 325);
			this.Controls.Add(this.buttonOpenFile);
			this.Controls.Add(this.buttonCancle);
			this.Controls.Add(this.buttonPlayToFile);
			this.Controls.Add(this.buttonResume);
			this.Controls.Add(this.buttonSetting);
			this.Controls.Add(this.buttonInit);
			this.Controls.Add(this.buttonPause);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.buttonPlay);
			this.Controls.Add(this.textBoxContent);
			this.Name = "formMain";
			this.Text = "��ͨ���������ϳ���ʾ����(.NET)";
			this.Closed += new System.EventHandler(this.formMain_Closed);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new formMain());
		}

		private void JttsErrMsg(int iErr)
		{
			MessageBox.Show("����ţ�" + iErr.ToString(), "����");
		}

		private bool CheckTextIsEmpty()
		{
			string strText = textBoxContent.Text;
			if (0 == strText.Trim().Length)
			{
				MessageBox.Show("�������ı���");
				textBoxContent.Focus();
				return true;
			}
			return false;
		}

		private void buttonInit_Click(object sender, System.EventArgs e)
		{
			DlgInit dlg = new DlgInit();
			if (DialogResult.OK == dlg.ShowDialog(this))
			{
				string strLibPath = null;
				string strSerialNO = null;
				int iErr = 0;

				dlg.GetData(out strLibPath, out strSerialNO);

				if (bInitialed)
				{
					iErr = Jtts.jTTS_End();
				}
				iErr = Jtts.jTTS_Init(strLibPath, strSerialNO);
				if (Jtts.ERR_NONE == iErr || Jtts.ERR_ALREADYINIT == iErr)
				{
					bInitialed = true;
					MessageBox.Show("��ʼ���ɹ���");
				}
				else
				{
					JttsErrMsg(iErr);
				}
			}
			dlg.Dispose();
		}

		private void buttonPlay_Click(object sender, System.EventArgs e)
		{
			if (CheckTextIsEmpty())
				return;
			int iErr = Jtts.jTTS_Play(textBoxContent.Text, 0);
			if (Jtts.ERR_NONE != iErr)
			{
				JttsErrMsg(iErr);
			}
		}

		private void buttonStop_Click(object sender, System.EventArgs e)
		{
			int iErr = Jtts.jTTS_Stop();
			if (Jtts.ERR_NONE != iErr)
			{
				JttsErrMsg(iErr);
			}
		}

		private void buttonPause_Click(object sender, System.EventArgs e)
		{
			int iErr = Jtts.jTTS_Pause();
			if (Jtts.ERR_NONE != iErr)
			{
				JttsErrMsg(iErr);
			}
		}

		private void buttonResume_Click(object sender, System.EventArgs e)
		{
			int iErr = Jtts.jTTS_Resume();
			if (Jtts.ERR_NONE != iErr)
			{
				JttsErrMsg(iErr);
			}
		}

		private void formMain_Closed(object sender, System.EventArgs e)
		{
			Jtts.jTTS_End();
		}

		private void buttonPlayToFile_Click(object sender, System.EventArgs e)
		{
			if (CheckTextIsEmpty())
				return;
			SaveFileDialog fDlg = new SaveFileDialog();
			//fDlg.Filter = "Wave File(*.wav)|*.wav|All File(*.*)|*.*";
			if (iFileFormat == Jtts.FORMAT_WAV ||
				iFileFormat == Jtts.FORMAT_WAV_8K8B || iFileFormat == Jtts.FORMAT_WAV_8K16B ||
				iFileFormat == Jtts.FORMAT_WAV_16K8B || iFileFormat == Jtts.FORMAT_WAV_16K16B ||
				iFileFormat == Jtts.FORMAT_WAV_11K8B || iFileFormat == Jtts.FORMAT_WAV_11K16B
				|| ((iFileFormat == Jtts.FORMAT_ALAW_8K || iFileFormat == Jtts.FORMAT_uLAW_8K) 
				&& (iFileHead == Jtts.PLAYTOFILE_ADDHEAD)) )
			{
				fDlg.Filter = "Wave File (*.wav)|*.wav|All Files(*.*)|*.*";
			}
			else if (iFileFormat == Jtts.FORMAT_VOX_6K || iFileFormat == Jtts.FORMAT_VOX_8K)
			{
				fDlg.Filter = "Vox File (*.vox)|*.vox|All Files(*.*)|*.*";
			}
			else
			{
				fDlg.Filter = "ALaw or uLaw File (*.law)|*.law|All Files(*.*)|*.*";
			}
			if (DialogResult.OK == fDlg.ShowDialog(this))
			{
				Jtts.JTTS_CONFIG config = new Jtts.JTTS_CONFIG();
				int iErr = Jtts.jTTS_Get(out config);
				Jtts.jTTS_PlayToFile(textBoxContent.Text, fDlg.FileName, 0,ref config, 0, 0, 0);
			}
		}

		private void buttonCancle_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonSetting_Click(object sender, System.EventArgs e)
		{
			int iErr = 0;

			Jtts.JTTS_CONFIG config = new InfoQuick.SinoVoice.Tts.Jtts.JTTS_CONFIG();
			iErr = Jtts.jTTS_Get(out config);
			DlgSetup dlg = new DlgSetup();
			//Set data
			dlg.SetJttsConfig(config);
			dlg.FileFormat = iFileFormat;
			dlg.FileHead = iFileHead;
			if (DialogResult.OK == dlg.ShowDialog(this))
			{
				dlg.GetJttsConfig(ref config);
				Jtts.jTTS_Set(ref config);
				iFileFormat = dlg.FileFormat;
				iFileHead = dlg.FileHead;
			}
			dlg.Dispose();
		}

		private void buttonOpenFile_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog fDlg = new OpenFileDialog();
			fDlg.Filter = "Text File(*.txt)|*.txt|All File(*.*)|*.*";
			if (DialogResult.OK == fDlg.ShowDialog(this))
			{
				try
				{
					//���Ҫ�򿪷�Unicode���Һ͵�ǰϵͳ�ַ�����һ�µ��ļ�����Ҫָ����ȡʱ�����õ��ַ���
					//���磺 Encoding.GetEncoding(950)
					StreamReader sr = new StreamReader(fDlg.FileName, Encoding.Default);
					textBoxContent.Text = sr.ReadToEnd();
					sr.Close();
				}
				catch
				{
					MessageBox.Show("���ļ�����");
				}
			}
		}
	}
}
