using System;
using System.Collections.Generic;
using System.Text;
using Hong.Xpo.Module;
using DevExpress.Xpo;

namespace Hong.ChildSafeSystem.Module
{
    public class UserManager : XpobjectManager
    {
        public UserManager()
            : base(typeof(InSchool))
        {
        }

        public User GetUser(string username, string password)
        {
            foreach (XPObject item in Xpobjects)
            {
                User user = item as User;
                if (user.Username == username && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
