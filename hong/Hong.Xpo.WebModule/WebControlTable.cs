using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.UiModule;
using Hong.Profile.Base;
using System.Web.UI.WebControls;

namespace Hong.Xpo.WebModule
{
    public abstract class WebControlTable : UiControlTable
    {
        public WebControlTable()
        {

        }

        protected override UiLayout CreateLayout()
        {
            WebLayout layout = new WebLayout(this);
            return layout;
        }

        protected override void SetSectionName(string value)
        {
            base.SetSectionName(value);
            SetWebControlsID(value);
        }

        protected abstract void SetWebControlsID(string value);
        
        protected override void LayoutChanged(object sender, VariableListChangedArgs e)
        {
            if (Layout is WebLayout)
            {
                LayoutChanged(Layout as WebLayout, e);
            }
        }

        protected abstract void LayoutChanged(WebLayout layout, VariableListChangedArgs e);

        protected override void AddToContain(UiContain contain)
        {
            //nothing to do
        }

        protected override void RemoveFromContain(UiContain contain)
        {
            //nothing to do
        }
    }
}
