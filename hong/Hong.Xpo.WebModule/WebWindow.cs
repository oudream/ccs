using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Hong.Xpo.UiModule;

namespace Hong.Xpo.WebModule
{
    /// <summary>
    /// web 的 容器方式与 win 的不一样
    /// web 的 把 web 组件放到 webLayout 的 TableCell 内面，当设 Parent 时，容器只收集各子控件中 webLayout，然后提供方法重新加载容器界面
    /// </summary>
    public class WebWindow : UiWindow
    {
        public WebWindow()
        {
            _table = new Table();
        }

        private Table _table;
        public Control Face
        {
            get
            {
                return _table;
            }
        }

        protected override void AutoLayout(ViewerBase viewer, WindowStyle style)
        {
            Table table;
            if (style == WindowStyle.Simple)
            {
                table = new Table();
                TableCell cell = new TableCell();
                cell.Controls.Add(table);
                if (viewer is ViewerLooker)
                {
                    cell.Width = 360;
                }
                else
                {
                    cell.Width = Unit.Percentage(100);
                }
                AddToTable(cell);
            }
            else
            {
                table = _table;
            }

            foreach (CellerBase celler in viewer.Cellers)
            {
                if (celler is WebContain && celler.Layout is WebLayout)
                {
                    WebContain webContain = celler as WebContain;
                    WebLayout layout = celler.Layout as WebLayout;
                    TableRow row = new TableRow();
                    table.Rows.Add(row);
                    row.Cells.Add(layout.TableCell);
                    AutoLayout(celler as WebContain);
                }
            }
            ContainsLayoutCellers(viewer);
        }

        /// <summary>
        /// 如果 span 为0 时，表示随便一个单元格填入就可以了
        /// </summary>
        /// <param name="contain"></param>
        private void AutoLayout(WebContain contain)
        {
            int columnCount;
            if (String.Equals(SectionNameDefine.Contain_Editor_Main, contain.Name))
            {
                columnCount = 2;
            }
            else if (String.Equals(SectionNameDefine.Contain_Looker_Main, contain.Name))
            {
                columnCount = 1;
            }
            else
            {
                columnCount = 5;
            }

            if (contain.Layout is WebLayout)
            {
                WebLayout layout = contain.Layout as WebLayout;
                if (contain.Name.IndexOf("Main") > -1)
                {
                    layout.TableCell.Height = Unit.Percentage(100);
                }
                else
                {
                    layout.TableCell.Height = 60;
                }
            }
            const int defaultCellWidth = 360;
            const int defaultCellHeight = 60;
            const int defaultComponentWidth = defaultCellWidth - 60;
            string cellWidth = defaultCellWidth.ToString();
            string cellHeight = defaultCellHeight.ToString();
            int rowIndex = 0;
            int columnIndex = 0;
            int rowSpan = 1;
            int columnSpan = 1;
            string componentWidth = defaultComponentWidth.ToString();
            string componentHeight = WebLayout.DefaultUnit;
            TableSite site = new TableSite(columnCount);
            foreach (CellerBase celler in contain.Cellers)
            {
                WebLayout layout;
                if (celler.Layout is WebLayout)
                {
                    layout = celler.Layout as WebLayout;
                }
                else
                {
                    continue;
                }
                if (celler is WebControlTable)
                {
                    rowSpan = 1;
                    columnSpan = columnCount;
                    cellWidth = "100%";
                    cellHeight = "300";
                    componentWidth = "100%";
                    componentHeight = "100%";
                }
                else if (celler is WebButton)
                {
                    rowSpan = 1;
                    columnSpan = 1;
                    cellWidth = WebLayout.DefaultUnit;
                    cellHeight = WebLayout.DefaultUnit;
                    componentWidth = "120";
                    componentHeight = "45";
                }
                else if (celler is WebImage || celler is WebListBox)
                {
                    rowSpan = 5;
                    columnSpan = 1;
                    cellWidth = defaultCellWidth.ToString();
                    cellHeight = (defaultCellHeight * rowSpan).ToString();
                    componentWidth = defaultComponentWidth.ToString();
                    componentHeight = (defaultCellHeight * rowSpan - defaultCellHeight).ToString();
                    if (columnIndex + columnSpan > columnCount - 1)
                    {
                        columnIndex = 0;
                    }
                }
                else
                {
                    rowSpan = 1;
                    columnSpan = 1;
                    cellWidth = defaultCellWidth.ToString();
                    cellHeight = defaultCellHeight.ToString();
                    componentWidth = defaultComponentWidth.ToString();
                    componentHeight = WebLayout.DefaultUnit;
                }
                site.RequestTableSite(rowSpan, columnSpan, out rowIndex, out columnIndex);
                
                layout.RowSpan.Value = rowSpan;
                layout.ColumnSpan.Value = columnSpan;
                layout.RowIndex.Value = rowIndex;
                layout.ColumnIndex.Value = columnIndex;
                layout.SetCellPoint(cellWidth, cellHeight);
                layout.SetComponentPoint(componentWidth, componentHeight);
            }
        }

        protected override void LoadLayout(ViewerBase viewer)
        {
            base.LoadLayout(viewer);

            ContainsLayoutCellers(viewer);
        }

        private static void ContainsLayoutCellers(ViewerBase viewer)
        {
            foreach (CellerBase celler in viewer.Cellers)
            {
                if (celler is WebContain)
                {
                    WebContain webContain = celler as WebContain;
                    webContain.LayoutCellers();
                }
            }
        }

        private void AddToTable(TableCell cell)
        {
            if (_table.Rows.Count <= 0)
            {
                _table.Rows.Add(new TableRow());
            }
            TableRow row = _table.Rows[0];
            row.Cells.Add(cell);
        }

    }
}
