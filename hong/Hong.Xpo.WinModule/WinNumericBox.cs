using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Windows.Forms;

namespace Hong.Xpo.WinModule
{
    public class WinNumericBox : WinControlEasy<int>
    {
        public WinNumericBox()
        {
            _numericUpDown = new NumericUpDown();

            _lable = new Label();
            _lable.AutoSize = false;
        }

        private NumericUpDown _numericUpDown;
        public NumericUpDown NumericUpDown
        {
            get
            {
                return _numericUpDown;
            }
        }

        private Label _lable;
        public Label Lable
        {
            get
            {
                return _lable;
            }
        }

        protected override bool ComponentToValueImpl(out int value)
        {
            value = Convert.ToInt32(_numericUpDown.Value);
            return true;
        }

        protected override bool ValueToComponentImpl(int value)
        {
            _numericUpDown.Value = value;
            return true;
        }

        protected override void AddToWinContain(WinContain contain)
        {
            contain.AddControl(_lable);
            contain.AddControl(_numericUpDown);
        }

        protected override void RemoveFromWinContain(WinContain contain)
        {
            contain.RemoveControl(_lable);
            contain.RemoveControl(_numericUpDown);
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
            _numericUpDown.Location = new System.Drawing.Point(left, top);
            _numericUpDown.Width = width;
            _numericUpDown.Height = height;
        }
    }
}
