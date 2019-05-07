using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Hong.Xpo.Module;
using DevExpress.Xpo;

namespace Hong.Xpo.WebModule
{
    public class WebTable : WebControlTable
    {
        public WebTable()
        {
            _table = new Table();

            WebLayout layout = Layout as WebLayout;
            layout.TableCell.Controls.Add(_table);
        }

        private Table _table;

        protected override void LayoutChanged(WebLayout layout, Hong.Profile.Base.VariableListChangedArgs e)
        {
            _table.Height = Unit.Percentage(100);
            _table.Width = Unit.Percentage(100);
        }

        protected override bool SelectXpobjectToTable(DevExpress.Xpo.XPObject value)
        {
            return false;
        }


        private List<XpobjectFieldUIAttribute> _fieldUIAttributes;
        protected override void TableCreateColumns(List<XpobjectFieldUIAttribute> fieldUIAttributes)
        {
            _table.Rows.Clear();
            _fieldUIAttributes = fieldUIAttributes;
            TableRow row;
            row = new TableRow();
            row.Height = 30;
            _table.Rows.Add(row);
            TableCell cell;
            Label label;
            foreach (XpobjectFieldUIAttribute fieldUIAttribute in _fieldUIAttributes)
            {
                if (fieldUIAttribute.Visible)
                {
                    cell = new TableCell();
                    cell.BorderStyle = BorderStyle.Dashed;
                    cell.BorderWidth = 1;
                    cell.Width = fieldUIAttribute.FieldWidth;
                    cell.Height = 30;
                    label = new Label();
                    label.Text = fieldUIAttribute.FieldTitle;
                    cell.Controls.Add(label);
                    row.Cells.Add(cell);
                }
            }
            cell = new TableCell();
            cell.BorderStyle = BorderStyle.Dashed;
            cell.BorderWidth = 1;
            cell.Width = 60;
            cell.Height = 30;
            label = new Label();
            label.Text = "Edit";
            cell.Controls.Add(label);
            row.Cells.Add(cell);
        }

        protected override void TableCreateRow(DevExpress.Xpo.XPObject xpobject, int index)
        {
            if (_fieldUIAttributes == null)
            {
                return;
            }
            TableRow row = new TableRow();
            _table.Rows.Add(row);
            TableCell cell;
            foreach (XpobjectFieldUIAttribute fieldUIAttribute in _fieldUIAttributes)
            {
                if (fieldUIAttribute.Visible)
                {
                    cell = new TableCell();
                    cell.Width = fieldUIAttribute.FieldWidth;
                    cell.Height = 30;
                    cell.BorderStyle = BorderStyle.Dashed;
                    cell.BorderWidth = 1;
                    WebControl fieldControl = CreateFieldWebControl(xpobject, fieldUIAttribute);
                    if (fieldControl != null)
                    {
                        cell.Controls.Add(fieldControl);
                    }
                    row.Cells.Add(cell);
                }
            }
            cell = new TableCell();
            cell.Width = 60;
            cell.Height = 30;
            cell.BorderStyle = BorderStyle.Dashed;
            cell.BorderWidth = 1;
            HyperLink link = new HyperLink();
            link.Text = "Edit";
            link.NavigateUrl = WebUrlDefine.MainProcessUrl(xpobject.GetType().FullName, xpobject.Oid.ToString(), Hong.Xpo.UiModule.WindowStyle.Editing.ToString());
            cell.Controls.Add(link);
            row.Cells.Add(cell);
        }

        protected override void TableCreateEnd()
        {
            _table.Rows.Add(new TableRow());
        }

        private WebControl CreateFieldWebControl(DevExpress.Xpo.XPObject xpobject, XpobjectFieldUIAttribute fieldUIAttribute)
        {
            WebControl webControl = null;
            if (Type.Equals(fieldUIAttribute.FieldType, typeof(System.Drawing.Image)))
            {
                Image image = new Image();
                image.ImageUrl = WebUrlDefine.ImageResponseUrl(xpobject.GetType().FullName, xpobject.Oid.ToString(), fieldUIAttribute.FieldName);
                image.Height = 60;
                image.ImageAlign = ImageAlign.Middle;
                webControl = image;
            }
            else if (Type.Equals(fieldUIAttribute.FieldType, typeof(XPObject)))
            {
                
            }
            else
            {
                Label label = new Label();
                try
                {
                    label.Text = xpobject.GetMemberValue(fieldUIAttribute.FieldName).ToString();
                    webControl = label;
                }
                catch (Exception)
                {
                }
            }
            return webControl;
        }

        protected override void SetWebControlsID(string value)
        {
            _table.ID = "Table_" + value;
        }

        protected override void TitleValueChanged(string value)
        {
            //nothing todo
        }
    }
}
