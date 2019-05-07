using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Windows.Forms;

namespace Hong.Xpo.WinModule
{
    public abstract class WinContain : UiContain
    {
        public WinContain()
        {
            _containControl = CreateContainControl();
        }

        protected abstract Control CreateContainControl();

        private Control _containControl;
        public Control ContainControl
        {
            get
            {
                return _containControl;
            }
        }

        public void AddControl(Control control)
        {
            ContainControl.Controls.Add(control);
        }

        public void RemoveControl(Control control)
        {
            ContainControl.Controls.Remove(control);
        }

        protected override void AddToContain(UiContain contain)
        {
            if (contain is WinContain)
            {
                WinContain winContain = contain as WinContain;
                winContain.AddControl(ContainControl);
            }
        }

        protected override void RemoveFromContain(UiContain contain)
        {
            if (contain is WinContain)
            {
                WinContain winContain = contain as WinContain;
                winContain.RemoveControl(ContainControl);
            }
        }

        protected override UiLayout CreateLayout()
        {
            return new WinLayout(this);
        }

        protected override void LayoutChanged(object sender, Hong.Profile.Base.VariableListChangedArgs e)
        {
            if (Layout is WinLayout)
            {
                WinLayout winLayout = Layout as WinLayout;
                _containControl.Location = new System.Drawing.Point(winLayout.LocationX.Value, winLayout.LocationY.Value);
                _containControl.Width = winLayout.Width.Value;
                _containControl.Height = winLayout.Height.Value;
                _containControl.Dock = winLayout.Dock.Value;
            }
        }
    }
}
