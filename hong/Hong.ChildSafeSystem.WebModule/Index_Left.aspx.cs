using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Hong.Xpo.WebModule;

public partial class Index_Left : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.menupanel.Controls.Add(WebMenuNavigation.GetMenuNavigator());
    }


    protected void LogoutBn_Click(object sender, EventArgs e)
    {
        //Page.Session.Remove("CustomerId");
        //Page.Session.Remove("Username");
        //Page.Session.Remove("Password");

        //ChildSafeSystemWebHelper.Singleton.CheckLogin(Page);
    }
}
