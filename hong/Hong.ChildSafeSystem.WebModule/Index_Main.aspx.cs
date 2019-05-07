using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hong.Xpo.WebModule;
using Hong.Xpo.UiModule;

public partial class Index_Main : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SchoolsCenter.Singleton.CheckLogin(Page);

        string styleString = Page.Request[WebSessionNameDefine.WindowStyle];
        string fullName = Page.Request[WebSessionNameDefine.ObjectTypeFullName];
        string id = Page.Request[WebSessionNameDefine.ObjectId];

        WindowStyle style = WindowStyle.Looking;
        if (styleString != null)
        {
            style = (WindowStyle)Enum.Parse(typeof(WindowStyle), styleString, true);
        }

        SchoolsCenter.Singleton.ShowWindow(this.Panel1, style, fullName, id);
    }
}
