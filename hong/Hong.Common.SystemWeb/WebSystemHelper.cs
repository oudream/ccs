using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
/// <summary>
/// SystemHelper 的摘要说明
/// </summary>

namespace Hong.Common.SystemWeb
{
    public class SystemHelper
    {
        private static SystemHelper FInstance;
        public static SystemHelper Singleton
        {
            get
            {
                if (FInstance == null)
                {
                    FInstance = new SystemHelper();
                }
                return FInstance;
            }
        }

        public void InitData(Page page)
        {
            _physicalPathApplication = page.Server.MapPath(page.Request.ApplicationPath);
            _physicalPathImages = _physicalPathApplication + @"\Images\";
        }

        public string HomeUrl
        {
            get
            {
                return "~/Index.aspx";
            }
        }

        private string _physicalPathImages;
        public string PhysicalPathImages
        {
            get
            {
                return _physicalPathImages;
            }
        }

        private string _physicalPathApplication;
        public string PhysicalPathApplication
        {
            get
            {
                return _physicalPathApplication;
            }
        }

        public void ShowInfo(string info, Page page)
        {
            page.Response.Write(string.Format("<script>window.alert('{0}！')</script>", info));
        }
    }
}