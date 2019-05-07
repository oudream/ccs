using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public class WebManager
{
    public WebManager()
    {
    }

    private static WebManager _instance;
    public static WebManager Singleton
    {
        get
        {
            if (_instance == null)
            {
                _instance = new WebManager();
            }
            return _instance;
        }
    }

    private int _times = 0;
    public int Times
    {
        get
        {
            _times++;
            return _times;
        }
        set
        {
            _times = value;
        }
    }
}
