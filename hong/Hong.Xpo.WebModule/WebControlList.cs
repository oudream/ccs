using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Web.UI.WebControls;
using Hong.Profile.Base;
using Hong.Xpo.Module;

namespace Hong.Xpo.WebModule
{
    public abstract class WebControlList : UiControlList
    {
        public WebControlList()
        {
            _label = new Label();
            _listControl = CreateListControl();
            
            WebLayout layout = Layout as WebLayout;
            layout.TableCell.Controls.Add(_label);
            layout.TableCell.Controls.Add(_listControl);
        }

        private Label _label;
        private ListControl _listControl;

        protected abstract ListControl CreateListControl();

        protected override UiLayout CreateLayout()
        {
            WebLayout layout = new WebLayout(this);
            return layout;
        }

        protected override void SetSectionName(string value)
        {
            base.SetSectionName(value);
            _label.ID = "Label_" + value;
            _listControl.ID = "ListControl_" + value;
        }

        protected override void LayoutChanged(object sender, VariableListChangedArgs e)
        {
            if (Layout is WebLayout)
            {
                LayoutChanged(Layout as WebLayout, e);
            }
        }

        protected void LayoutChanged(WebLayout layout, VariableListChangedArgs e)
        {
            _listControl.Width = layout.ComponentWidth;
            _listControl.Height = Unit.Percentage(100);
        }

        protected override void AddToContain(UiContain contain)
        {
            //nothing to do
        }

        protected override void RemoveFromContain(UiContain contain)
        {
            //nothing to do
        }

        protected override void XpobjectToControlList(DevExpress.Xpo.XPObject value)
        {
            foreach (ListItem item in _listControl.Items)
            {
                if (item.Value == value.Oid.ToString())
                {
                    item.Selected = true;
                }
            }
        }

        protected override void CreateControlItem(Hong.Xpo.Module.XpobjectItem xpobjectItem, int index)
        {
            ListItem li = new ListItem();
            li.Text = xpobjectItem.ToString();
            li.Value = xpobjectItem.XPObject.Oid.ToString();
            _listControl.Items.Add(li);
        }

        protected override void ClearControlList()
        {
            _listControl.Items.Clear();
        }

        protected override bool ComponentToValueImpl(out DevExpress.Xpo.XPObject value)
        {
            value = null;
            if (_listControl.Page == null)
            {
                return false;
            }
            object session = _listControl.Page.Session[WebSessionNameDefine.ObjectTypeFullName];
            if (session == null)
            {
                return false;
            }
            XpobjectManager manager = XpobjectCenter.Singleton.GetManager(session.ToString());

            ListItem item = _listControl.SelectedItem;
            if (item == null)
            {
                return false;
            }
            value = manager.GetXpobject(Convert.ToInt32(item.Value));
            return true;
        }

        protected override void TitleValueChanged(string value)
        {
            _label.Text = value + @"<br/>";
        }
    }
}
