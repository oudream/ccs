using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
    public class FingerPrintManager : XpobjectManager
    {
        public FingerPrintManager()
            : base(typeof(FingerPrint))
        {
            
        }

        public FingerPrint GetFingerPrint(byte[] matBuf, int matSize, FingerSourceType fingerSourceType)
        {
            byte[] tpBuf;
            int tpSize;
            foreach (XPObject item in Xpobjects)
            {
                FingerPrint fingerPrint = item as FingerPrint;
                if (fingerPrint.FingerSourceType != fingerSourceType)
                {
                    continue;
                }
                tpBuf = fingerPrint.Template;
                tpSize = fingerPrint.TemplateSize;
                if (FingerprintWrapper.Default().VerifyTemplateOneToOne(ref tpBuf[0], tpSize, ref matBuf[0], matSize))
                {
                    return fingerPrint;
                }
            }
            return null;
        }

        public FingerPrint[] GetFingerPrints(FingerSourceType fingerSourceType, int peopleId)
        {
            List<FingerPrint> fingerPrints = new List<FingerPrint>();
            foreach (XPObject item in Xpobjects)
            {
                FingerPrint fingerPrint = item as FingerPrint;
                if (fingerPrint.FingerSourceType == fingerSourceType && fingerPrint.PeopleId == peopleId)
                {
                    fingerPrints.Add(fingerPrint);
                }
            }
            return fingerPrints.ToArray();
        }

        public FingerPrint GetFingerPrint(FingerSourceType fingerSourceType, int peopleId, FingerType fingerType)
        {
            FingerPrint result = null;
            foreach (XPObject item in Xpobjects)
            {
                FingerPrint fingerPrint = item as FingerPrint;
                if (fingerPrint.FingerSourceType == fingerSourceType && fingerPrint.PeopleId == peopleId && fingerPrint.FingerType == fingerType)
                {
                    result = fingerPrint;
                    break;
                }
            }
            return result;
        }
    }
}
