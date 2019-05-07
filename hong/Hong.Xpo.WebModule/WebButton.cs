using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Web.UI.WebControls;
using Hong.Profile.Base;

namespace Hong.Xpo.WebModule
{
    [EditorTypeAttribute(EditorType.Read)]
    public class WebButton : UiButton
    {
        public WebButton()
        {
            _button = new Button();
            _button.Click += new EventHandler(_button_Click);

            WebLayout layout = Layout as WebLayout;
            layout.TableCell.Controls.Add(_button);
        }

        protected override void SetSectionName(string value)
        {
            base.SetSectionName(value);
            _button.ID = "Button_" + value;
        }
        
        protected override UiLayout CreateLayout()
        {
            WebLayout layout = new WebLayout(this);
            return layout;
        }

        protected override void LayoutChanged(object sender, VariableListChangedArgs e)
        {
            if (Layout is WebLayout)
            {
                WebLayout layout = Layout as WebLayout;

                _button.Height = layout.ComponentHeight;
                _button.Width = layout.ComponentWidth;
            }
        }

        void _button_Click(object sender, EventArgs e)
        {
            RaiseExecute();
        }

        private Button _button;

        protected override bool ComponentToValueImpl(out string value)
        {
            value = null;
            return false;
        }

        protected override bool ValueToComponentImpl(string value)
        {
            _button.Text = value;
            return true;
        }

        protected override void AddToContain(UiContain contain)
        {
            //nothing to do
        }

        protected override void RemoveFromContain(UiContain contain)
        {
            //nothing to do
        }

        protected override void TitleValueChanged(string value)
        {
            _button.Text = value;
        }
    }
}
