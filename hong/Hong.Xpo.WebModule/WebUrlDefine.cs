using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Xpo.WebModule
{
    /// <summary>
    ///
    ///<httpHandlers>
    /// <add verb="*" path="ImageResponse.axd" type="Hong.Xpo.WebModule.ImageResponseHttpHandler, Hong.Xpo.WebModule" />
    ///</httpHandlers>
    /// 
    /// </summary>
    public static class WebUrlDefine
    {
        public static string ImageResponseUrl(string objectTypeFullName, string objectId, string objectPropertyName)
        {
            return String.Format("ImageResponse.axd?{0}={1}&{2}={3}&{4}={5}", WebSessionNameDefine.ObjectTypeFullName, objectTypeFullName, WebSessionNameDefine.ObjectId, objectId, WebSessionNameDefine.ObjectPropertyName, objectPropertyName);
        }

        public static string MainProcessUrl(string objectTypeFullName, string objectId, string style)
        {
            return String.Format("Index_Main.aspx?{0}={1}&{2}={3}&{4}={5}", WebSessionNameDefine.ObjectTypeFullName, objectTypeFullName, WebSessionNameDefine.ObjectId, objectId, WebSessionNameDefine.WindowStyle, style);
        }
    }
}
