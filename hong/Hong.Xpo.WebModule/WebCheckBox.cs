using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Hong.Xpo.WebModule
{
    public class WebCheckBox : WebControlEasy<bool>
    {
        public WebCheckBox()
        {
            _checkBox = new CheckBox();
            
            WebLayout layout = Layout as WebLayout;
            layout.TableCell.Controls.Add(_checkBox);
        }

        private CheckBox _checkBox;

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

        protected override void TitleValueChanged(string value)
        {
            _checkBox.Text = value;
        }

        protected override void LayoutChanged(WebLayout layout, Hong.Profile.Base.VariableListChangedArgs e)
        {
        }

        protected override void SetWebControlsID(string value)
        {
            _checkBox.ID = "CheckBox_" + value;
        }
    }
}
