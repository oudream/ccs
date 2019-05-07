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

public partial class TempletSimple : System.Web.UI.Page
{
    private WebTable _webTable = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (_webTable == null)
        //{
        //    _webTable = new WebTable(Panel1);
        //}
        Table table = new Table();
        this.Panel1.Controls.Add(table);
        int rowCount = 10;
        int columnCount = 2;
        for (int i = 0; i < rowCount; i++)
        {
            TableRow row = new TableRow();
            table.Rows.Add(row);
            for (int j = 0; j < columnCount; j++)
            {

            }
        }
    }
}
