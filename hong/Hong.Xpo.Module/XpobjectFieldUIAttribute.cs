using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Xpo.Module
{
    public class XpobjectFieldUIAttribute : Attribute
    {
        public XpobjectFieldUIAttribute(int ordernumber, bool cannull, object defaultvalue)
        {
            _ordernumber = ordernumber;
            _cannull = cannull;
            _defaultvalue = defaultvalue;

            _fieldname = "";
            _fieldType = null;
            _fieldWidth = -1;
            _visible = false;
        }

        public XpobjectFieldUIAttribute() : this(-1, true, null)
        {
        }

        private int _ordernumber;
        public int Ordernumber
        {
            get
            {
                return _ordernumber;
            }
        }

        private bool _cannull;
        public bool Cannull
        {
            get
            {
                return _cannull;
            }
        }

        private object _defaultvalue;
        public object Defaultvalue
        {
            get
            {
                return _defaultvalue;
            }
        }

        private string _fieldname;
        public string FieldName
        {
            get
            {
                return _fieldname;
            }
        }

        public void SetFieldName(string name)
        {
            _fieldname = name;
        }

        private Type _fieldType;
        public Type FieldType
        {
            get
            {
                return _fieldType;
            }
        }

        public void SetFieldType(Type type)
        {
            _fieldType = type;
        }

        private string _fieldTitle;
        public string FieldTitle
        {
            get
            {
                return _fieldTitle;
            }
        }

        public void SetFieldTitle(string title)
        {
            _fieldTitle = title;
        }

        private int _fieldWidth;
        public int FieldWidth
        {
            get
            {
                return _fieldWidth;
            }
        }

        public void SetFieldWidth(int width)
        {
            _fieldWidth = width;
        }

        private bool _visible;
        public bool Visible
        {
            get
            {
                return _visible;
            }
        }

        public void SetVisible(bool visible)
        {
            _visible = visible;
        }

        public void AssignFrom(XpobjectFieldUIAttribute source)
        {
            _ordernumber = source.Ordernumber;
            _cannull = source.Cannull;
            _defaultvalue = source.Defaultvalue;
            _fieldname = source.FieldName;
            _fieldTitle = source.FieldTitle;
        }
    }
}
