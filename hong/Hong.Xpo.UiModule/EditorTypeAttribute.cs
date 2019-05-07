using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Xpo.UiModule
{
    public class EditorTypeAttribute : Attribute
    {
        public EditorTypeAttribute(EditorType editorType)
        {
            _editorType = editorType;
        }

        private EditorType _editorType;
        public EditorType EditorType
        {
            get
            {
                return _editorType;
            }
        }
    }
}
