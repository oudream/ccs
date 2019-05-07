using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Windows.Forms;
using Hong.Xpo.Module;

namespace Hong.Xpo.WinModule
{
    public class WinComboBox : WinControlList
    {
        public WinComboBox()
        {
            _comboBox = new ComboBox();
            _lable = new Label();
            _lable.AutoSize = false;
        }

        private Label _lable;

        private ComboBox _comboBox;

        protected override void XpobjectToControlList(DevExpress.Xpo.XPObject value)
        {
            foreach (object obj in _comboBox.Items)
            {
                if (obj is XpobjectItem)
                {
                    if ((obj as XpobjectItem).XPObject == value)
                    {
                        _comboBox.SelectedItem = obj;
                        break;
                    }
                }
            }
        }

        protected override void CreateControlItem(Hong.Xpo.Module.XpobjectItem xpobjectItem, int index)
        {
            _comboBox.Items.Add(xpobjectItem);
        }

        protected override void ClearControlList()
        {
            _comboBox.Items.Clear();
        }

        protected override bool ComponentToValueImpl(out DevExpress.Xpo.XPObject value)
        {
            object obj = _comboBox.SelectedItem;
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
            contain.AddControl(_lable);
            contain.AddControl(_comboBox);
        }

        protected override void RemoveFromWinContain(WinContain contain)
        {
            contain.RemoveControl(_lable);
            contain.RemoveControl(_comboBox);
        }

        protected override void TitleValueChanged(string value)
        {
            _lable.Text = value;
        }

        protected override void LayoutChanged(WinLayout layout, Hong.Profile.Base.VariableListChangedArgs e)
        {
            int height = 25;
            int width = 100;
            int top = layout.LocationY.Value + layout.Height.Value / 2 - height / 2;
            int left = layout.LocationX.Value;
            _lable.AutoSize = false;
            _lable.Location = new System.Drawing.Point(left, top);
            _lable.Width = width;
            _lable.Height = height;

            left += _lable.Width + 10;
            width = layout.Width.Value - _lable.Width - 20;
            _comboBox.Location = new System.Drawing.Point(left, top);
            _comboBox.Width = width;
            _comboBox.Height = height;
        }
    }
}
