using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.Module;
using Hong.Xpo.UiModule;
using System.Windows.Forms;

namespace Hong.Xpo.WinModule
{
    public class WinWindow : UiWindow
    {
        public WinWindow()
        {
            _form = new Form();
        }

        private Form _form;
        public Form Form
        {
            get
            {
                return _form;
            }
        }

        protected override void AutoLayout(ViewerBase viewer, WindowStyle style)
        {
            Control contain;
            if (style == WindowStyle.Simple)
            {
                contain = new Panel();
                if (viewer is ViewerLooker)
                {
                    contain.Width = 360;
                    contain.Dock = DockStyle.Left;
                    _form.Controls.Add(contain);
                }
                else
                {
                    contain.Dock = DockStyle.Fill;
                    _form.Controls.Add(contain);
                    _form.Controls.SetChildIndex(contain, 0);
                }

            }
            else
            {
                contain = _form;
            }
            foreach (CellerBase celler in viewer.Cellers)
            {
                if (celler is WinContain)
                {
                    WinContain winContain = celler as WinContain;
                    contain.Controls.Add(winContain.ContainControl);
                    AutoLayout(celler as WinContain);
                }
            }
        }

        private void AutoLayout(WinContain contain)
        {
            if (contain.Layout is WinLayout)
            {
                WinLayout layout = contain.Layout as WinLayout;
                if (contain.Name.IndexOf("Main") > -1)
                {
                    layout.Dock.Value = DockStyle.Fill;
                }
                else
                {
                    layout.Dock.Value = DockStyle.Bottom;
                    layout.Height.Value = 60;
                }
            }
            int width = 300;
            int heigth = 60;
            int x = 25;
            int y = 25;
            foreach (CellerBase celler in contain.Cellers)
            {
                WinLayout layout;
                if (celler.Layout is WinLayout)
                {
                    layout = celler.Layout as WinLayout;
                }
                else
                {
                    continue;
                }
                if (celler is WinControlTable)
                {
                    layout.Dock.Value = DockStyle.Fill;
                    continue;
                }
                if (celler is WinButton)
                {
                    layout.Dock.Value = DockStyle.Left;
                    layout.Width.Value = 110;
                    continue;
                }
                layout.Width.Value = width;
                layout.Height.Value = heigth;
                layout.LocationX.Value = x;
                layout.LocationY.Value = y;
                x = x + width + 100;
                if (x > 500)
                {
                    x = 25;
                    y = y + heigth;
                }
            }
        }
    }
}
