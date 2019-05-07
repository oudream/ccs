using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class TestWebA : System.Web.UI.Page
{
    private WebTable _webTable = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (_webTable == null)
        {
            //_webTable = new WebTable(this
        }
    }
}
