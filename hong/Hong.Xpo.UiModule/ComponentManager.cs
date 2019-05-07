using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Hong.Profile.Base;

namespace Hong.Xpo.UiModule
{
    public class ComponentManager
    {
        public static CellerBase CreateCeller(ProfileBase profile, string section)
        {
            string typeName = profile.GetValue(section, "TypeName").ToString();
            foreach (Type type in _componentTypes)
            {
                if (type.FullName.Equals(typeName))
                {
                    //object[] paramValues = { driver, section };
                    Assembly asm = Assembly.GetAssembly(type);
                    //object obj = asm.CreateInstance(type.FullName, true, BindingFlags.Default, null, paramValues, null, null);
                    CellerBase celler = asm.CreateInstance(type.FullName) as CellerBase;
                    return celler;
                }
            }
            return null;
        }

        public static void RegisterComponentType(Type type)
        {
            _componentTypes.Add(type);
        }

        static ComponentManager()
        {
            _componentTypes = new List<Type>();
        }

        private static List<Type> _componentTypes;
        public static List<Type> ComponentTypes
        {
            get
            {
                return _componentTypes;
            }
        }

        internal static UiContain AutoCreateContain(string name)
        {
            foreach (Type type in _componentTypes)
            {
                if (type.IsSubclassOf(typeof(UiContain)))
                {
                    UiContain table = type.Assembly.CreateInstance(type.FullName) as UiContain;
                    table.SectionName = name;
                    return table;
                }
            }
            return null;
        }

        internal static UiControlTable AutoCreateControlTable(string name)
        {
            foreach (Type type in _componentTypes)
            {
                if (type.IsSubclassOf(typeof(UiControlTable)))
                {
                    UiControlTable table = type.Assembly.CreateInstance(type.FullName) as UiControlTable;
                    table.SectionName = name;
                    return table;
                }
            }
            return null;
        }

        internal static UiControlObject AutoCreateControl(string name, Type valueType)
        {
            foreach (Type type in _componentTypes)
            {
                if (type.IsSubclassOf(typeof(UiControlObject)) && IsValueControl(type, valueType))
                {
                    UiControlObject control = type.Assembly.CreateInstance(type.FullName) as UiControlObject;
                    control.SectionName = name;
                    return control;
                }
            }
            return null;
        }

        private static bool IsValueControl(Type type, Type valueType)
        {
            PropertyInfo info = type.GetProperty("Value");
            if (info != null)
            {
                if (info.PropertyType == valueType || valueType.IsSubclassOf(info.PropertyType))
                {
                    object[] objs = type.GetCustomAttributes(false);
                    foreach (object obj in objs)
                    {
                        if (obj is EditorTypeAttribute)
                        {
                            if ((obj as EditorTypeAttribute).EditorType == EditorType.Read)
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
            }
            //if (type.IsGenericType)
            //{
                //Type[] typeArguments = type.GetGenericArguments();
                //foreach (Type tParam in typeArguments)
                //{
                //    if (! tParam.IsGenericParameter && (tParam == valueType || valueType.IsSubclassOf(tParam)))
                //    {
                //        return true;
                //    }
                //}
            //}
            return false;
        }

        internal static UiButton AutoCreateButton(string name)
        {
            foreach (Type type in _componentTypes)
            {
                if (type.IsSubclassOf(typeof(UiButton)))
                {
                    UiButton button = type.Assembly.CreateInstance(type.FullName) as UiButton;
                    button.SectionName = name;
                    return button;
                }
            }
            return null;
        }
    }
}
