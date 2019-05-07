using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Xpo.UiModule
{
    public class WindowManager<T> where T : UiWindow, new()
    {
        public T GetWindow(Type type, WindowStyle style)
        {
            T window = new T();
            CxmlDocument[] cxmls = CxmlDocumentManager.Singleton.GetCxmlDocuments(type, style);
            if (cxmls.Length > 0)
            {
                window.Load(cxmls);
            }
            else
            {
                window.WindowStyle = style;
                window.XpobjectType = type;
            }
            return window;
        }
    }
}
