using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;
using Hong.Xpo.Module;
using DevExpress.Xpo;
using System.IO;

namespace Hong.Xpo.WebModule
{
    public class ImageResponseHttpHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string objectTypeFullName = context.Request.QueryString[WebSessionNameDefine.ObjectTypeFullName];
            string objectId = context.Request.QueryString[WebSessionNameDefine.ObjectId];
            string objectPropertyName = context.Request.QueryString[WebSessionNameDefine.ObjectPropertyName];
            if (String.IsNullOrEmpty(objectTypeFullName) || String.IsNullOrEmpty(objectId) || String.IsNullOrEmpty(objectPropertyName))
            {
                return;
            }
            int oid;
            if (! int.TryParse(objectId, out oid))
            {
                return;
            }
            XpobjectManager manager = XpobjectCenter.Singleton.GetManager(objectTypeFullName);
            if (manager == null)
            {
                return;
            }
            XPObject xpobject = manager.GetXpobject(oid);
            if (xpobject == null)
            {
                return;
            }
            object obj = xpobject.GetMemberValue(objectPropertyName);
            if (! (obj is System.Drawing.Image))
            {
                return;
            }
            System.Drawing.Image image = obj as System.Drawing.Image;
            MemoryStream stream = new MemoryStream();
            image.Save(stream, image.RawFormat);
            context.Response.BinaryWrite(stream.ToArray());
            context.Response.ContentType = "image/" + image.RawFormat.ToString();
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
