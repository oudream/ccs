using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;
using System.Web.UI.WebControls;

namespace Hong.Xpo.WebModule
{
    public class WebNumericBox : WebControlEasy<int>
    {
        public WebNumericBox()
        {
            _textBox = new TextBox();
            _label = new Label();

            WebLayout layout = Layout as WebLayout;
            layout.TableCell.Controls.Add(_label);
            layout.TableCell.Controls.Add(_textBox);
        }

        private Label _label;

        private TextBox _textBox;

        protected override void LayoutChanged(WebLayout layout, Hong.Profile.Base.VariableListChangedArgs e)
        {
            _textBox.Height = layout.ComponentHeight;
            _textBox.Width = layout.ComponentWidth;
        }

        protected override bool ComponentToValueImpl(out int value)
        {
            value = -1;
            if (! String.IsNullOrEmpty(_textBox.Text))
            {
                int.TryParse(_textBox.Text, out value);
            }
            return true;
        }

        protected override bool ValueToComponentImpl(int value)
        {
            _textBox.Text = value.ToString();
            return true;
        }

        protected override void SetWebControlsID(string value)
        {
            _textBox.ID = "TextBox_" + value;
        }

        protected override void TitleValueChanged(string value)
        {
            _label.Text = value + @"<br/>";
        }
    }
}
