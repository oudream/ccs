using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Windows.Forms;

namespace Hong.Xpo.WinModule
{
    public class WinTextBox : WinControlEasy<string>
    {
        public WinTextBox()
        {
            _textBox = new TextBox();
            _lable = new Label();
        }

        private TextBox _textBox;

        private Label _lable;

        protected override bool ComponentToValueImpl(out string value)
        {
            value = _textBox.Text;
            return true;
        }

        protected override bool ValueToComponentImpl(string value)
        {
            _textBox.Text = value;
            return true;
        }

        protected override void AddToWinContain(WinContain contain)
        {
            contain.AddControl(_lable);
            contain.AddControl(_textBox);
        }

        protected override void RemoveFromWinContain(WinContain contain)
        {
            contain.RemoveControl(_lable);
            contain.RemoveControl(_textBox);
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
            _textBox.Location = new System.Drawing.Point(left, top);
            _textBox.Width = width;
            _textBox.Height = height;
        }
    }
}
