using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacebookBotCSharpSample
{
    public partial class webhookStart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var page_token =
             "YOUR_PAGE_TOKEN";
            
            var  uri = "https://graph.facebook.com/v2.6/me/subscribed_apps";
            string parameters = "access_token=" + page_token;

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string result = wc.UploadString(uri, parameters);
                Response.Write(result);
            }
        }
    }
}