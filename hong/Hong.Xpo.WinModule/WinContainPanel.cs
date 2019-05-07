using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Windows.Forms;

namespace Hong.Xpo.WinModule
{
    public class WinContainPanel : WinContain
    {
        protected override Control CreateContainControl()
        {
            return new Panel();
        }

        protected override void TitleValueChanged(string value)
        {
            // nothing todo;
        }
    }
}
