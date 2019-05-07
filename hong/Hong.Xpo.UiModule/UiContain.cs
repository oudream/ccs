using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Xpo.UiModule
{
    public abstract class UiContain : CellerBase
    {
        public UiContain()
        {
            _cellers = new List<CellerBase>();
        }

        private List<CellerBase> _cellers;
        public List<CellerBase> Cellers
        {
            get
            {
                return _cellers;
            }
        }
    }
}
