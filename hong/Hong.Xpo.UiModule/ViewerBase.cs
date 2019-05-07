using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using Hong.Profile.Base;
using Hong.Xpo.Module;

namespace Hong.Xpo.UiModule
{
    public abstract class ViewerBase
    {
        public ViewerBase()
        {
            _variables = new VariableList();
            _variables.DefaultSection = "View";

            _cellersVariables = new VariableList();
            _cellersVariables.DefaultSection = SectionNameDefine.Cellers;

            _cellers = new List<CellerBase>();
        }

        private VariableList _variables;
        public VariableList Variables
        {
            get
            {
                return _variables;
            }
        }

        private VariableList _cellersVariables;
        public VariableList CellersVariables
        {
            get
            {
                return _cellersVariables;
            }
        }

        private List<CellerBase> _cellers;
        public List<CellerBase> Cellers
        {
            get
            {
                return _cellers;
            }
        }

        public CellerBase GetCeller(string name)
        {
            foreach (CellerBase celler in Cellers)
            {
                if (celler.Name.Equals(name))
                {
                    return celler;
                }
            }
            return null;
        }

        public UiContain GetContain(string name)
        {
            CellerBase celler = GetCeller(name);
            if (celler != null && celler is UiContain)
            {
                return celler as UiContain;
            }
            return null;
        }

        public UiControlObject GetControl(string name)
        {
            CellerBase celler = GetCeller(name);
            if (celler != null && celler is UiControlObject)
            {
                return celler as UiControlObject;
            }
            return null;
        }

        public void  EventsLink()
        {
            foreach (CellerBase celler in Cellers)
            {
                if (celler is UiControlObject)
                {
                    EventLink(celler as UiControlObject);
                }
            }
        }

        protected abstract void EventLink(UiControlObject control);

        private XPObject _currentXpobject;
        public XPObject CurrentXpobject
        {
            get
            {
                return _currentXpobject;
            }
            set
            {
                if (_currentXpobject != value)
                {
                    SetCurrentXpobject(value);
                }
            }
        }

        protected virtual void SetCurrentXpobject(XPObject value)
        {
            _currentXpobject = value;
            if (value != null)
            {
                XpobjectManager manager = XpobjectCenter.Singleton.GetManager(value.GetType());
                if (manager != _xpobjectManager)
                {
                    return;
                }
            }
        }

        private XpobjectManager _xpobjectManager;
        public XpobjectManager XpobjectManager
        {
            get
            {
                return _xpobjectManager;
            }
            set
            {
                if (_xpobjectManager != value)
                {
                    SetXpobjectManager(value);
                }
            }
        }

        protected virtual void SetXpobjectManager(XpobjectManager value)
        {
            _xpobjectManager = value;
            CurrentXpobject = null;
        }
    }
}
