using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;

namespace Hong.Channel.Base
{
    public abstract class ChannelConfig : VariableList
    {
        public ChannelConfig()
        {
            _profile = new ProfileXml(this.GetType().ToString());
        }

        private ProfileBase _profile;
        public ProfileBase Profile
        {
            get
            {
                return _profile;
            }
        }

        protected abstract string SectionImpl();

        public void Load()
        {
            this.LoadVariablesValue(Profile);
        }

        public void Save()
        {
            this.SaveVariablesValue(Profile);
        }
    }
}
