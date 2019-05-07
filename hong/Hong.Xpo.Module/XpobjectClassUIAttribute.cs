using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Xpo.Module
{
    public class XpobjectClassUIAttribute : Attribute
    {
        public XpobjectClassUIAttribute(string[] titlePropertyNames)
        {
            _titlePropertyNames = titlePropertyNames;
        }

        public XpobjectClassUIAttribute()
        {
            _titlePropertyNames = new string[0];
        }

        private string[] _titlePropertyNames;
        public string[] TitlePropertyNames
        {
            get
            {
                return _titlePropertyNames;
            }
        }

        public void SetTitlePropertyNames(string[] names)
        {
            if (names == null)
            {
                return;
            }
            _titlePropertyNames = new string[names.Length];
            names.CopyTo(_titlePropertyNames, 0);
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
        }

        public void SetTitle(string title)
        {
            _title = title;
        }
    }
}
