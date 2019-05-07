using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Hong.Xpo.UiModule;
using System.Web.UI.WebControls;

namespace Hong.Xpo.WebModule
{
    /// <summary>
    /// 这个容器包容方式与 WIN 的不一样，包容动作放到 
    /// </summary>
    public class WebContain : UiContain
    {
        public WebContain()
        {
            _table = new Table();
            _table.BorderStyle = BorderStyle.Dashed;
            _table.BorderWidth = 1;

            WebLayout layout = Layout as WebLayout;
            layout.TableCell.Controls.Add(_table);
        }

        protected override void SetSectionName(string value)
        {
            base.SetSectionName(value);
            _table.ID = "Table_" + value;
        }

        private Table _table;

        protected override void AddToContain(UiContain contain)
        {
            //nothing to do
        }

        protected override void RemoveFromContain(UiContain contain)
        {
            //nothing to do
        }

        protected override UiLayout CreateLayout()
        {
            WebLayout layout = new WebLayout(this);
            return layout;
        }

        protected override void LayoutChanged(object sender, Hong.Profile.Base.VariableListChangedArgs e)
        {
            if (Layout is WebLayout)
            {
                WebLayout webLayout = Layout as WebLayout;
                _table.Height = webLayout.CellHeight;
                _table.Width = webLayout.CellWidth;
            }
        }

        public void LayoutCellers()
        {
            _table.Rows.Clear();
            int rowCount = LayoutGetRowCount();
            //for (int i = 0; i < rowCount; i++)
            //{
            //    TableRow row = new TableRow();
            //    _table.Rows.Add(row);
            //}
            //for (int i = 0; i < Cellers.Count; i++)
            //{
            //    CellerBase celler = Cellers[i];
            //    if (celler.Layout is WebLayout)
            //    {
            //        WebLayout layout = celler.Layout as WebLayout;
            //        _table.Rows[layout.RowIndex].Cells.Add(layout.TableCell);
            //    }
            //}
            int columnCount = LayoutGetColumnCount();
            for (int i = 0; i < rowCount; i++)
            {
                TableRow row = new TableRow();
                _table.Rows.Add(row);
                
                for (int j = 0; j < columnCount; j++)
                {
                    WebLayout layout = LayoutGetTableCell(i, j);
                    if (layout != null)
                    {
                        if (layout.RowIndex.Value == i && layout.ColumnIndex.Value == j)
                        {
                            row.Cells.Add(layout.TableCell);
                        }
                    }
                    else
                    {
                        TableCell cell = new TableCell();
                        //cell.Height = 60;
                        row.Cells.Add(cell);
                    }
                }
            }
        }

        private WebLayout LayoutGetTableCell(int rowIndex, int columnIndex)
        {
            foreach (CellerBase celler in Cellers)
            {
                if (celler.Layout is WebLayout)
                {
                    WebLayout layout = celler.Layout as WebLayout;
                    if (layout.IsInside(rowIndex, columnIndex))
                    {
                        return layout;
                    }
                }
            }
            return null;
        }

        private int LayoutGetColumnCount()
        {
            int columnCount = 0;
            foreach (CellerBase celler in Cellers)
            {
                if (celler.Layout is WebLayout)
                {
                    WebLayout layout = celler.Layout as WebLayout;
                    if (layout.ColumnEndIndex() + 1 > columnCount)
                    {
                        columnCount = layout.ColumnEndIndex() + 1;
                    }
                }
            }
            return columnCount;
        }

        private int LayoutGetRowCount()
        {
            int rowCount = 0;
            foreach (CellerBase celler in Cellers)
            {
                if (celler.Layout is WebLayout)
                {
                    WebLayout layout = celler.Layout as WebLayout;
                    if (layout.RowEndIndex() + 1 > rowCount)
                    {
                        rowCount = layout.RowEndIndex() + 1;
                    }
                }
            }
            return rowCount;
        }

        protected override void TitleValueChanged(string value)
        {
            //nothing todo
        }
    }
}
