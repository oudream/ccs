using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Hong.Xpo.Module;

namespace Hong.Xpo.WebModule
{
    public class WebDropDownList : WebControlList
    {
        protected override ListControl CreateListControl()
        {
            return new DropDownList();
        }
    }
}
