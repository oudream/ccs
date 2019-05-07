using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Xpo.UiModule
{
    public class CxmlDocumentManager
    {
        public CxmlDocumentManager()
        {
            _cxmls = new List<CxmlDocument>();
            Load();
        }

        private static CxmlDocumentManager _instance;
        public static CxmlDocumentManager Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CxmlDocumentManager();
                }
                return  _instance;
            }
        }

        private List<CxmlDocument> _cxmls;
        public List<CxmlDocument> Cxmls
        {
            get
            {
                return _cxmls;
            }
        }

        private void Load()
        {
            //todo
            CxmlDocument cxml;

            cxml = new CxmlDocument();
            cxml.FileName = @"D:\Project\LibraryCSharp\Hong.ChildSafeSystem.WinModule\SchoolTemplet\TeamLooker.xml";
            _cxmls.Add(cxml);

            cxml.FileName = @"D:\Project\LibraryCSharp\Hong.ChildSafeSystem.WinModule\SchoolTemplet\TeamEditor.xml";
            _cxmls.Add(cxml);
        }

        public CxmlDocument[] GetCxmlDocuments(Type type, WindowStyle style)
        {
            List<ViewerType> viewerTypes = new List<ViewerType>();
            switch (style)
            {
                case WindowStyle.Looking:
                    viewerTypes.Add(ViewerType.Lookor);
                    break;

                case WindowStyle.Editing:
                    viewerTypes.Add(ViewerType.Editor);
                    break;

                case WindowStyle.Simple:
                    viewerTypes.Add(ViewerType.Lookor);
                    viewerTypes.Add(ViewerType.Editor);
                    break;
                default:
                    break;
            }
            List<CxmlDocument> cxmls = new List<CxmlDocument>();
            foreach (ViewerType viewertype in viewerTypes)
            {
                foreach (CxmlDocument cxml in _cxmls)
                {
                    if (cxml.GetXPObjectType() == type && cxml.GetViewerStyle() == viewertype)
                    {
                        cxmls.Add(cxml);
                    }
                }
            }
            return cxmls.ToArray();
        }
    }
}
