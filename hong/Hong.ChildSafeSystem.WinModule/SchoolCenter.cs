using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using Hong.Xpo.WinModule;
using DevExpress.Xpo;
using Hong.Common.SystemWin;
using Hong.Xpo.Module;
using System.Windows.Forms;
using Hong.ChildSafeSystem.Module;

namespace Hong.ChildSafeSystem.WinModule
{
    public class SchoolCenter
    {
        public SchoolCenter(string schoolName)
        {
            _windowManager = new WindowManager<WinWindow>();
        }

        private WindowManager<WinWindow> _windowManager;
        public WindowManager<WinWindow> WindowManager
        {
            get
            {
                return _windowManager;
            }
        }

        private static SchoolCenter _instance;
        public static SchoolCenter Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SchoolCenter("");//todo
                }
                return _instance;
            }
        }

        internal void ShowWindow(Type type, WindowStyle style)
        {
            WinWindow win = WindowManager.GetWindow(type, style);
            WinSystemHelper.Singleton.ShowInMainWindow(win.Form);
        }

        public void InitData()
        {
            string msg = DataBaseHelper.Singleton.OpenConn();
            if (msg != "")
            {
                MessageBox.Show(msg);
                return;
            }

            AssemblyManager.Singleton.RegisterXPObjectAssembly(typeof(Team).Assembly);

            XpobjectCenter.Singleton.GetManager(typeof(Team));
            XpobjectCenter.Singleton.GetManager(typeof(Teacher));
            XpobjectCenter.Singleton.GetManager(typeof(Student));
            XpobjectCenter.Singleton.GetManager(typeof(Genearch));
            XpobjectCenter.Singleton.GetManager(typeof(Position));
            XpobjectCenter.Singleton.RegisterManager(new UserManager());

            ComponentManager.RegisterComponentType(typeof(WinButton));
            ComponentManager.RegisterComponentType(typeof(WinCheckBox));
            ComponentManager.RegisterComponentType(typeof(WinComboBox));
            ComponentManager.RegisterComponentType(typeof(WinContainGroupBox));
            ComponentManager.RegisterComponentType(typeof(WinContainPanel));
            ComponentManager.RegisterComponentType(typeof(WinDatetimeBox));
            ComponentManager.RegisterComponentType(typeof(WinImageBox));
            ComponentManager.RegisterComponentType(typeof(WinListBox));
            ComponentManager.RegisterComponentType(typeof(WinListView));
            ComponentManager.RegisterComponentType(typeof(WinNumericBox));
            ComponentManager.RegisterComponentType(typeof(WinRadioBox));
            ComponentManager.RegisterComponentType(typeof(WinTextBox));
        }
    }
}
