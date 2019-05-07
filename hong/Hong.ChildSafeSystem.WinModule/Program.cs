using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hong.Xpo.UiModule;
using Hong.Xpo.WinModule;
using Hong.Xpo.Module;
using Hong.ChildSafeSystem.Module;

namespace Hong.ChildSafeSystem.WinModule
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SchoolCenter.Singleton.InitData();
            Application.Run(new MainForm());
        }
    }
}
