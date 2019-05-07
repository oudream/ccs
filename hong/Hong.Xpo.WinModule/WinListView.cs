using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Windows.Forms;
using Hong.Profile.Base;
using DevExpress.Xpo;
using Hong.Xpo.Module;

namespace Hong.Xpo.WinModule
{
    [EditorTypeAttribute(EditorType.Read)]
    public class WinListView : WinControlTable
    {
        public WinListView()
        {
            _listView = new ListView();
            _listView.SelectedIndexChanged += new System.EventHandler(ListViewSelectedIndexChanged);
        }

        private ListView _listView;

        private void ListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lv = _listView;
            if (lv.SelectedItems.Count <= 0)
            {
                return;
            }
            ListViewItem li = lv.SelectedItems[0];

            RaiseSelectXpobjectChanged(li.Tag as XPObject);
        }

        private List<XpobjectFieldUIAttribute> _fieldUIAttributes = null;
        protected override void TableCreateColumns(List<XpobjectFieldUIAttribute> fieldUIAttributes)
        {
            _fieldUIAttributes = fieldUIAttributes;
            ListView lv = _listView;
            lv.Columns.Clear();
            foreach (XpobjectFieldUIAttribute fieldUIAttribute in fieldUIAttributes)
            {
                if (fieldUIAttribute.Visible)
                {
                    lv.Columns.Add(fieldUIAttribute.FieldTitle, fieldUIAttribute.FieldWidth);
                }
            }
        }

        protected override void TableCreateRow(XPObject xpobject, int index)
        {
            if (_fieldUIAttributes == null)
            {
                return;
            }
            ListView lv = _listView;
            ListViewItem li = new ListViewItem();
            li.Tag = xpobject;
            li.Text = index.ToString();
            foreach (XpobjectFieldUIAttribute fieldUIAttribute in _fieldUIAttributes)
            {
                if (fieldUIAttribute.Visible)
                {
                    li.SubItems.Add(GetMemberValue(xpobject, fieldUIAttribute.FieldName));
                }
            }
            lv.Items.Add(li);
        }

        protected override void TableCreateEnd()
        {
        }

        private string GetMemberValue(XPObject xpobject, string fieldName)
        {
            string value = "";
            try
            {
                value = xpobject.GetMemberValue(fieldName).ToString();
            }
            catch (Exception)
            {

            }
            return value;
        }

        protected override bool SelectXpobjectToTable(XPObject value)
        {
            foreach (ListViewItem item in _listView.Items)
            {
                if (item.Tag == value)
                {
                    item.Selected = true;
                    return true;
                }
            }
            return false;
        }

        protected override void AddToWinContain(WinContain contain)
        {
            contain.AddControl(_listView);
        }

        protected override void RemoveFromWinContain(WinContain contain)
        {
            contain.RemoveControl(_listView);
        }

        protected override void LayoutChanged(WinLayout layout, VariableListChangedArgs e)
        {
            _listView.View = View.Details;
            _listView.FullRowSelect = true;
            _listView.Location = new System.Drawing.Point(layout.LocationX.Value, layout.LocationY.Value);
            _listView.Width = layout.Width.Value;
            _listView.Height = layout.Height.Value;
            _listView.Dock = layout.Dock.Value;
        }

        protected override void TitleValueChanged(string value)
        {
            //nothing todo
        }
    }
}
