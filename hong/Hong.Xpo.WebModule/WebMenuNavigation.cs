using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Hong.Xpo.Module;
using Hong.Xpo.UiModule;

namespace Hong.Xpo.WebModule
{
    public class WebMenuNavigation
    {
        public static WebControl GetMenuNavigator()
        {
            Table table = new Table();
            foreach (XpobjectManager manager in XpobjectCenter.Singleton.Managers)
            {
                TableCell cell;
                cell = CreateSingleRowCell(table);
                
                cell = CreateSingleRowCell(table);
                cell.Controls.Add(CreateMenu(manager));
            }
            return table;
        }

        private static TableCell CreateSingleRowCell(Table table)
        {
            TableRow row = new TableRow();
            table.Rows.Add(row);
            TableCell cell = new TableCell();
            row.Cells.Add(cell);
            cell.Height = 30;
            cell.BorderStyle = BorderStyle.Dashed;
            cell.BorderWidth = 1;
            return cell;
        }

        private static Table CreateMenu(XpobjectManager manager)
        {
            Table table = new Table();
            TableCell cell;
            HyperLink link;

            cell = CreateSingleRowCell(table);
            link = new HyperLink();
            link.Text = manager.XpobjectClassUIAttribute.Title + " -> Look";
            link.NavigateUrl = WebUrlDefine.MainProcessUrl(manager.XpobjectFullName, "", WindowStyle.Looking.ToString());
            link.Target = "MainFrame";
            cell.Controls.Add(link);

            cell = CreateSingleRowCell(table);
            link = new HyperLink();
            link.Text = manager.XpobjectClassUIAttribute.Title + " -> Edit";
            link.NavigateUrl = WebUrlDefine.MainProcessUrl(manager.XpobjectFullName, "", WindowStyle.Editing.ToString());
            link.Target = "MainFrame";
            cell.Controls.Add(link);

            return table;
        }
    }
}
