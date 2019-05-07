using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Reflection;

public class WebTable
{
    public WebTable(Control control)
    {
        _table = new Table();
        //_table.BorderWidth = 2;
        //_table.BorderStyle = BorderStyle.Dashed;
        control.Controls.Add(_table);

        // Total number of rows.
        int rowCnt = 10;
        // Current row count.
        int rowCtr;
        // Total number of cells per row (columns).
        int cellCtr;
        // Current cell counter
        int cellCnt = WebManager.Singleton.Times;

        for (rowCtr = 1; rowCtr <= rowCnt; rowCtr++)
        {
            // Create new row and add it to the table.
            TableRow tRow = new TableRow();
            //tRow.Height = 200;
            //tRow.Width = 600;
            //tRow.BorderStyle = BorderStyle.Dashed;
            //tRow.BorderWidth = 2;
            _table.Rows.Add(tRow);
            for (cellCtr = 1; cellCtr <= cellCnt; cellCtr++)
            {
                // Create a new cell and add it to the row.
                TableCell tCell = new TableCell();
                TextBox textBox = new TextBox();
                textBox.ID = "textBox" + (rowCtr * 10 + cellCtr).ToString();
                textBox.Text = "textBox" + (rowCtr * 10 + cellCtr).ToString();
                tCell.Controls.Add(textBox);
                Button bn = new Button();
                bn.Text = cellCtr.ToString();
                bn.Click += new EventHandler(bn_Click);
                _page = control.Page;
                tCell.Controls.Add(bn);
                tCell.Height = 200;
                tCell.Width = 100 + cellCtr * 50;
                tCell.BorderStyle = BorderStyle.Dashed;
                tCell.BorderWidth = 1;
                //img.ImageUrl = @"C:\Users\Public\Pictures\Sample Pictures\沙漠.jpg";
                //tCell.Text = "Row " + rowCtr + ", Cell " + cellCtr;
                tRow.Cells.Add(tCell);
                tRow.Cells.Add(tCell);
                tRow.Cells.Add(tCell);
                tRow.Cells.Add(tCell);
                tRow.Cells.Add(tCell);
            }
        }
    }

    void bn_Click(object sender, EventArgs e)
    {
        string s = "";
        TextBox textBox = _page.FindControl("textBox11") as TextBox;
        if (textBox != null)
        {
            s = s + textBox.Text;
        }
        textBox = _page.FindControl("textBox12") as TextBox;
        if (textBox != null)
        {
            s = s + textBox.Text;
        }
        textBox = _page.FindControl("textBox13") as TextBox;
        if (textBox != null)
        {
            s = s + textBox.Text;
        }

        
        Unit a = 3;
        if (a == 3)
        {
            
        }

        _page.Response.Write(s);
    }

    private Page _page;

    private Table _table;
    public Table Table
    {
        get
        {
            return _table;
        }
        set
        {
            _table = value;
        }
    }
}
