using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Hong.Xpo.WebModule
{
    public class WebRadioButtonList : WebControlEasy<Enum>
    {
        public WebRadioButtonList()
        {
            _radioButtonList = new RadioButtonList();
            _radioButtonList.BorderStyle = BorderStyle.Dashed;
            _radioButtonList.BorderWidth = 1;
            _radioButtonList.RepeatDirection = RepeatDirection.Horizontal;
            _label = new Label();

            WebLayout layout = Layout as WebLayout;
            layout.TableCell.Controls.Add(_label);
            layout.TableCell.Controls.Add(_radioButtonList);
        }

        private RadioButtonList _radioButtonList;

        private Label _label;

        protected override void LayoutChanged(WebLayout layout, Hong.Profile.Base.VariableListChangedArgs e)
        {
            _radioButtonList.Height = layout.ComponentHeight;
            _radioButtonList.Width = layout.ComponentWidth;
        }

        protected override void SetWebControlsID(string value)
        {
            _radioButtonList.ID = "RadioButtonList_" + value;
            _label.ID = "Label_" + value;
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

        private void SetEnumValue(Enum value)
        {
            string nameValue = Enum.GetName(value.GetType(), value);
            foreach (ListItem item in _radioButtonList.Items)
            {
                if (item.Value == nameValue)
                {
                    item.Selected = true;
                }
            }
        }

        private void LoadRadioButton(Type type)
        {
            if (type == null || !type.IsEnum)
            {
                return;
            }
            _radioButtonList.Items.Clear();
            string[] names = Enum.GetNames(type);
            foreach (string name in names)
            {
                ListItem item = new ListItem();
                item.Text = name;
                item.Value = name;
                _radioButtonList.Items.Add(item);
            }
        }

        protected override void TitleValueChanged(string value)
        {
            _label.Text = value + @"<br/>";
        }

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

        private object GetEnumValue()
        {
            if (_valueType == null)
            {
                return null;
            }
            foreach (ListItem item in _radioButtonList.Items)
            {
                if (item.Selected)
                {
                    return Enum.Parse(_valueType, item.Value);
                }
            }
            return null;
        }
    }
}
