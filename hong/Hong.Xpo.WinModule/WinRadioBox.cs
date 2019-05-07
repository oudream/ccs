using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Windows.Forms;
using System.Drawing;

namespace Hong.Xpo.WinModule
{
    public class WinRadioBox : WinControlEasy<Enum>
    {
        public WinRadioBox()
        {
            _groupBox = new GroupBox();
        }

        private GroupBox _groupBox;

        protected override bool ComponentToValueImpl(out Enum value)
        {
            value = GetEnumValue() as Enum;
            if (value != null)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        public override void Initialization(System.Type valueType)
        {
            base.Initialization(valueType);
            if (valueType != null)
            {
                if (_valueType != valueType)
                {
                    LoadRadioButton(valueType);
                    _valueType = valueType;
                }
            }
        }

        private Type _valueType;
        protected override bool ValueToComponentImpl(Enum value)
        {
            Type type = value.GetType();
            if (_valueType != type)
            {
                LoadRadioButton(type);
                _valueType = type;
            }
            SetEnumValue(value);
            return true;
        }

        private object GetEnumValue()
        {
            if (_valueType == null)
            {
                return null;
            }
            foreach (Control control in _groupBox.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radio = control as RadioButton;
                    if (radio.Checked)
                    {
                        return Enum.Parse(_valueType, radio.Text);
                    }
                }
            }
            return null;
        }

        private void SetEnumValue(Enum value)
        {
            string nameValue = Enum.GetName(value.GetType(), value);
            foreach (Control control in _groupBox.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radio = control as RadioButton;
                    if (radio.Text == nameValue)
                    {
                        radio.Checked = true;
                    }
                }
            }
        }

        private void LoadRadioButton(Type type)
        {
            if (type == null || ! type.IsEnum)
            {
                return;
            }
            _groupBox.Controls.Clear();
            string[] names = Enum.GetNames(type);
            foreach (string name in names)
            {
                RadioButton radio = new RadioButton();
                radio.Text = name;
                _groupBox.Controls.Add(radio);
            }
            LayoutRadio();
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
            LayoutRadio();
        }

        private void LayoutRadio()
        {
            int defaultWidth = 80;
            int measure = 25;
            int x = 25;
            int y = 25;
            foreach (Control control in _groupBox.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radio = control as RadioButton;
                    radio.Location = new Point(x, y);
                    x = x + defaultWidth + measure;
                    if (x + defaultWidth > _groupBox.Width)
                    {
                        x = measure;
                        y = y + measure;
                    }
                }
            }
        }
    }
}
