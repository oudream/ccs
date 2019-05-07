using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Hong.Xpo.WinModule
{
    public class WinDatetimeBox : WinControlEasy<DateTime>
    {
        public WinDatetimeBox()
        {
            _dateTimePicker = new DateTimePicker();
            _dateTimePicker.Format = DateTimePickerFormat.Custom;
            _dateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";

            _lable = new Label();
            _lable.AutoSize = false;
        }

        private DateTimePicker _dateTimePicker;
        public DateTimePicker DateTimePicker
        {
            get
            {
                return _dateTimePicker;
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

        protected override bool ComponentToValueImpl(out DateTime value)
        {
            value = _dateTimePicker.Value;
            return true;
        }

        protected override bool ValueToComponentImpl(DateTime value)
        {
            _dateTimePicker.Value = value;
            return true;
        }

        protected override void AddToWinContain(WinContain contain)
        {
            contain.AddControl(_lable);
            contain.AddControl(_dateTimePicker);
        }

        protected override void RemoveFromWinContain(WinContain contain)
        {
            contain.RemoveControl(_lable);
            contain.RemoveControl(_dateTimePicker);
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
            _dateTimePicker.Location = new System.Drawing.Point(left, top);
            _dateTimePicker.Width = width;
            _dateTimePicker.Height = height;
        }
    }
}
