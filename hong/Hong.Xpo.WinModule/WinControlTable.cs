using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using System.Windows.Forms;

namespace Hong.Xpo.WinModule
{
    public abstract class WinControlTable : UiControlTable
    {
        protected override UiLayout CreateLayout()
        {
            return new WinLayout(this);
        }

        protected override void LayoutChanged(object sender, Hong.Profile.Base.VariableListChangedArgs e)
        {
            if (Layout is WinLayout)
            {
                LayoutChanged(Layout as WinLayout, e);
            }
        }

        protected abstract void LayoutChanged(WinLayout layout, Hong.Profile.Base.VariableListChangedArgs e);

        #region Contain
        protected override void AddToContain(UiContain contain)
        {
            if (contain is WinContain)
            {
                WinContain winContain = contain as WinContain;
                AddToWinContain(winContain);
            }
        }

        protected abstract void AddToWinContain(WinContain contain);

        protected override void RemoveFromContain(UiContain contain)
        {
            if (contain is WinContain)
            {
                WinContain winContain = contain as WinContain;
                RemoveFromWinContain(winContain);
            }
        }

        protected abstract void RemoveFromWinContain(WinContain contain);
        #endregion
    }
}
