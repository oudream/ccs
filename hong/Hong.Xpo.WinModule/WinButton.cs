using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Windows.Forms;
using Hong.Profile.Base;
using System.Drawing;

namespace Hong.Xpo.WinModule
{
    [EditorTypeAttribute(EditorType.Read)]
    public class WinButton : UiButton
    {
        public WinButton()
        {
            _button = new Button();
            _button.Click += new EventHandler(_button_Click);
        }

        protected override UiLayout CreateLayout()
        {
            return new WinLayout(this);
        }

        protected override void LayoutChanged(object sender, VariableListChangedArgs e)
        {
            if (Layout is WinLayout)
            {
                WinLayout layout = Layout as WinLayout;
                _button.Height = layout.Height.Value;
                _button.Width = layout.Width.Value;
                _button.Location = new Point(layout.LocationX.Value, layout.LocationY.Value);
                _button.Dock = layout.Dock.Value;
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
            if (contain is WinContain)
            {
                WinContain winContain = contain as WinContain;
                winContain.AddControl(_button);
            }
        }

        protected override void RemoveFromContain(UiContain contain)
        {
            if (contain is WinContain)
            {
                WinContain winContain = contain as WinContain;
                winContain.RemoveControl(_button);
            }
        }

        protected override void TitleValueChanged(string value)
        {
            _button.Text = value;
        }
    }
}
