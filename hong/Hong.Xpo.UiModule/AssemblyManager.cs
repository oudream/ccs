using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Hong.Xpo.UiModule
{
    public class AssemblyManager
    {
        public AssemblyManager()
        {
            _xpObjectAssemblys = new List<Assembly>();
        }

        private static AssemblyManager _instance;
        public static AssemblyManager Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssemblyManager();
                }
                return _instance;
            }
        }

        private List<Assembly> _xpObjectAssemblys;
        public List<Assembly> XPObjectAssemblys
        {
            get
            {
                return _xpObjectAssemblys;
            }
        }

        public void RegisterXPObjectAssembly(Assembly xpObjectAssembly)
        {
            _xpObjectAssemblys.Add(xpObjectAssembly);
        }
    
        public Type FindXPObjectType(string xpObjectTypeName)
        {
            foreach (Assembly assembly in _xpObjectAssemblys)
            {
                Type type = assembly.GetType(xpObjectTypeName);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
}
