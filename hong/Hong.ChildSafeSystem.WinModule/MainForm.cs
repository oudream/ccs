using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Hong.Xpo.Module;
using Hong.Xpo.UiModule;
using Hong.Xpo.WinModule;
using Hong.Common.SystemWin;
using Hong.ChildSafeSystem.Module;
using System.Reflection;

namespace Hong.ChildSafeSystem.WinModule
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            WinSystemHelper.Singleton.Parent = this.panel1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SchoolCenter.Singleton.ShowWindow(typeof(People), WindowStyle.Simple);
            
            return;
            
            this.textBox1.AppendText(ComponentManager.ComponentTypes.Count.ToString());
            foreach (Type type in ComponentManager.ComponentTypes)
            {
                PropertyInfo info = type.GetProperty("Value");
                if (info != null)
                {
                    this.textBox1.AppendText(info.PropertyType.FullName);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PropertyInfo[] infos = typeof(Member).GetProperties();
            foreach (PropertyInfo info in infos)
            {
                object[] objs = info.GetCustomAttributes(true);

            }
        }
    }
}
