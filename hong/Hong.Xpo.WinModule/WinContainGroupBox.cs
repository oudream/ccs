using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Windows.Forms;

namespace Hong.Xpo.WinModule
{
    public class WinContainGroupBox : WinContain
    {
        protected override void TitleValueChanged(string value)
        {
            ContainControl.Text = value;
        }

        protected override Control CreateContainControl()
        {
            return new GroupBox();
        }
    }
}
