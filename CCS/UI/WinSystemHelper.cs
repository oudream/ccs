using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Hong.Common.SystemWin
{
	public class WinSystemHelper
	{
		private static WinSystemHelper _instance;
		public static WinSystemHelper Singleton
		{
			get
			{
				if (_instance == null)
				{
					_instance = new WinSystemHelper();
				}
				return _instance;
			}
		}

		private OpenFileDialog _openFileDialog;
		private SaveFileDialog _saveFileDialog;

		public WinSystemHelper()
		{
			_openFileDialog = new System.Windows.Forms.OpenFileDialog();
			_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
		}

		public string ExecPath
		{
			get
			{
				return Application.ExecutablePath;
			}
		}

		public DialogResult ShowInfo(string info)
		{
			return MessageBox.Show(info);
		}

		private Control _parent;
		public Control Parent
		{
			get
			{
				return _parent;
			}
			set
			{
				_parent = value;
			}
		}

		public void ShowInMainWindow(Form form)
		{
			if (_parent == null)
			{
				return;
			}
			form.TopLevel = false;
			form.FormBorderStyle = FormBorderStyle.None;
			_parent.Controls.Add(form);
			form.WindowState = FormWindowState.Maximized;
			form.BringToFront();
			_formShowing = form;
			form.Show();
			form.Update();
		}

		private Form _formShowing;
		public Form FormShowing
		{
			get
			{
				return _formShowing;
			}
		}

		public string ShowOpenFileDialog()
		{
			if (_openFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (File.Exists(_openFileDialog.FileName))
				{
					return _openFileDialog.FileName;
				}
			}
			return "";
		}

		public string ShowSaveFileDialog()
		{
			if (_saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				return _saveFileDialog.FileName;
			}
			return "";
		}
	}
}
