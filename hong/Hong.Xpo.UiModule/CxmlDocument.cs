using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;

namespace Hong.Xpo.UiModule
{
    public class CxmlDocument
    {
        public Type GetXPObjectType()
        {
            //todo
            return null;
        }

        public ViewerType GetViewerStyle()
        {
            //todo
            return ViewerType.Lookor;
        }

        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        private ProfileBase _profile;
        public ProfileBase GetProfile()
        {
            if (_profile == null)
            {
                if (System.IO.File.Exists(FileName))
                {
                    _profile = new ProfileXml(FileName);
                }
                else
                {
                    _profile = new ProfileXml();
                }
            }
            else if (System.IO.File.Exists(FileName))
            {
                _profile.Name = FileName;
            }
            return _profile;
        }
    }
}
