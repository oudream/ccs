using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
    public class InSchoolManager : XpobjectManager
    {
        public InSchoolManager()
            : base(typeof(InSchool))
        {
        }

        protected override XPCollection CreateXPObjects(Type objType)
        {
            GroupOperator ops = new GroupOperator();
            BinaryOperator op;
            op = new BinaryOperator("InSchoolTime", DateTime.Today, BinaryOperatorType.Greater);
            ops.Operands.Add(op);
            op = new BinaryOperator("InSchoolTime", DateTime.Today.AddDays(1), BinaryOperatorType.Less);
            ops.Operands.Add(op);
            return new XPCollection(XpoHelper.Singleton.Session, objType, ops, null);
        }
    }
}
