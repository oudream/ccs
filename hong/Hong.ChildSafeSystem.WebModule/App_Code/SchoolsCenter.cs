using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Hong.Xpo.UiModule;
using Hong.Xpo.WebModule;
using Hong.Xpo.Module;
using Hong.ChildSafeSystem.Module;
using DevExpress.Xpo;

/// <summary>
///SchoolsCenter 的摘要说明
/// </summary>
public class SchoolsCenter
{
    public SchoolsCenter()
    {
        _windowManager = new WindowManager<WebWindow>();
    }

    private static SchoolsCenter _instance;
    public static SchoolsCenter Singleton
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SchoolsCenter();//todo
            }
            return _instance;
        }
    }

    private WindowManager<WebWindow> _windowManager;
    public WindowManager<WebWindow> WindowManager
    {
        get
        {
            return _windowManager;
        }
    }

    public bool IsLogged(Page page)
    {
        if (! String.IsNullOrEmpty(page.Session["Username"] as string))
        {
            return true;
        }
        return false;
    }

    public bool IsAdministrator(Page page)
    {
        if (IsLogged(page))
        {
            if (String.Equals(page.Session["Username"], "Administrator"))
            {
                return true;
            }
            return true;
        }
        return false;
    }

    public void CheckLogin(Page page)
    {
        //todo
    }

    public string LoginUsername(Page page)
    {
        return "";
    }

    public string LoginPassword(Page page)
    {
        return "";
    }

    public string LoginId(Page page)
    {
        return "";
    }

    public void ShowWindow(Control contain, WindowStyle style, string fullName, string id)
    {
        if (contain == null || fullName == null)
        {
            return;
        }
        XpobjectManager manager = XpobjectCenter.Singleton.GetManager(fullName);
        if (manager == null)
        {
            return;
        }
        WebWindow win = _windowManager.GetWindow(manager.XpobjectType, style);
        if (win == null)
        {
            return;
        }
        if (style == WindowStyle.Editing && ! String.IsNullOrEmpty(id))
        {
            int oid = -1;
            if (int.TryParse(id, out oid))
            {
                XPObject xpobject = manager.GetXpobject(oid);
                if (xpobject != null)
                {
                    foreach (ViewerBase viewer in win.Views)
                    {
                        viewer.CurrentXpobject = xpobject;
                    }
                }
            }
        }
        contain.Controls.Add(win.Face);
    }

    private bool _init = false;
    public void InitData(Page page)
    {
        if (_init)
        {
            return;
        }

        Hong.Common.SystemWeb.SystemHelper.Singleton.InitData(page);

        string msg = DataBaseHelper.Singleton.OpenConn();
        if (msg != "")
        {
            Hong.Common.SystemWeb.SystemHelper.Singleton.ShowInfo(msg, page);
            return;
        }

        AssemblyManager.Singleton.RegisterXPObjectAssembly(typeof(Team).Assembly);

        XpobjectCenter.Singleton.GetManager(typeof(Team));
        XpobjectCenter.Singleton.GetManager(typeof(Teacher));
        XpobjectCenter.Singleton.GetManager(typeof(Student));
        XpobjectCenter.Singleton.GetManager(typeof(Genearch));
        XpobjectCenter.Singleton.GetManager(typeof(Position));
        XpobjectCenter.Singleton.RegisterManager(new UserManager());

        ComponentManager.RegisterComponentType(typeof(WebButton));
        ComponentManager.RegisterComponentType(typeof(WebCheckBox));
        ComponentManager.RegisterComponentType(typeof(WebContain));
        ComponentManager.RegisterComponentType(typeof(WebDatetimeBox));
        ComponentManager.RegisterComponentType(typeof(WebImage));
        //ComponentManager.RegisterComponentType(typeof(WebListBox));
        ComponentManager.RegisterComponentType(typeof(WebDropDownList));
        ComponentManager.RegisterComponentType(typeof(WebNumericBox));
        ComponentManager.RegisterComponentType(typeof(WebRadioButtonList));
        ComponentManager.RegisterComponentType(typeof(WebTable));
        ComponentManager.RegisterComponentType(typeof(WebTextBox));

        _init = true;
    }
}
