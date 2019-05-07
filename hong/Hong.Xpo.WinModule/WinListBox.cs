using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Windows.Forms;
using Hong.Xpo.Module;

namespace Hong.Xpo.WinModule
{
    public class WinListBox : WinControlList
    {
        public WinListBox()
        {
            _listBox = new ListBox();
            _groupBox = new GroupBox();
            _groupBox.Controls.Add(_listBox);
        }

        private GroupBox _groupBox;

        private ListBox _listBox;

        protected override void XpobjectToControlList(DevExpress.Xpo.XPObject value)
        {
            foreach (object obj in _listBox.Items)
            {
                if (obj is XpobjectItem)
                {
                    if ((obj as XpobjectItem).XPObject == value)
                    {
                        _listBox.SelectedItem = obj;
                        break;
                    }
                }
            }
        }

        protected override void CreateControlItem(Hong.Xpo.Module.XpobjectItem xpobjectItem, int index)
        {
            _listBox.Items.Add(xpobjectItem);
        }

        protected override void ClearControlList()
        {
            _listBox.Items.Clear();
        }

        protected override bool ComponentToValueImpl(out DevExpress.Xpo.XPObject value)
        {
            object obj = _listBox.SelectedItem;
            if (obj is XpobjectItem)
            {
                 value = (obj as XpobjectItem).XPObject;
                 return true;
            }
            value = null;
            return false;
        }

        protected override void AddToWinContain(WinContain contain)
        {
            contain.AddControl(_groupBox);
        }

        protected override void RemoveFromWinContain(WinContain contain)
        {
            contain.RemoveControl(_groupBox);
        }

        protected override void TitleValueChanged(string value)
        {
            _groupBox.Text = value;
        }

        protected override void LayoutChanged(WinLayout layout, Hong.Profile.Base.VariableListChangedArgs e)
        {
            _groupBox.Location = new System.Drawing.Point(layout.LocationX.Value, layout.LocationY.Value);
            _groupBox.Width = layout.Width.Value;
            _groupBox.Height = layout.Height.Value;
            _groupBox.Dock = layout.Dock.Value;
        }
    }
}
