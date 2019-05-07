using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Hong.Xpo.WinModule
{
    public class WinCheckBox : WinControlEasy<bool>
    {
        public WinCheckBox()
        {
            _checkBox = new CheckBox();
            _checkBox.AutoSize = false;
        }

        private CheckBox _checkBox;
        public CheckBox CheckBox
        {
            get
            {
                return _checkBox;
            }
        }
        protected override bool ComponentToValueImpl(out bool value)
        {
            value = _checkBox.Checked;
            return true;
        }

        protected override bool ValueToComponentImpl(bool value)
        {
            _checkBox.Checked = value;
            return true;
        }

        protected override void AddToWinContain(WinContain contain)
        {
            contain.AddControl(_checkBox);
        }

        protected override void RemoveFromWinContain(WinContain contain)
        {
            contain.RemoveControl(_checkBox);
        }

        protected override void TitleValueChanged(string value)
        {
            _checkBox.Text = value;
        }

        protected override void LayoutChanged(WinLayout layout, Hong.Profile.Base.VariableListChangedArgs e)
        {
            int height = 25;
            int width = layout.Width.Value;
            int top = layout.LocationY.Value + layout.Height.Value / 2 - height / 2;
            int left = layout.LocationX.Value;

            _checkBox.Location = new System.Drawing.Point(left, top);
            _checkBox.Width = width;
            _checkBox.Height = height;
        }
    }
}
