using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace FacebookBotCSharpSample
{
    public partial class webhook : System.Web.UI.Page
    {
        private string FANPAGE_TOKEN = "YOUR_PAGE_TOKEN";


        protected void Page_Load(object sender, EventArgs e)
        {
            //讓facebook 過來跟我驗證的code 
            if (!string.IsNullOrEmpty(Request["hub.verify_token"]))
            {
                if (Request["hub.verify_token"] == "MY_CUSTOM_TOKEN_FOR_BOT_USE")
                {
                    Response.Write(Request["hub.challenge"]);
                }
            }

            //接收程式
            //用來暫存的資料夾，當然你不用這樣存下來，這是我拿來測試用的
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "tmp_msg");

            var sr = new StreamReader(Request.InputStream);
            string content = sr.ReadToEnd();
            //收到馬上把原始檔做一下紀錄
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "tmp_msg\\" + DateTime.Now.ToString("yyyyMMddHHmmsss") + "-origin.js", content);

            try
            {
                var mcs = JsonConvert.DeserializeObject<ReceiveBotModel>(content);
                //紀錄一下呼叫者 id 跟他的訊息
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "tmp_msg\\" + DateTime.Now.ToString("yyyyMMddHHmmsss") + "-chatinfo.js",
                    mcs.entry[0].messaging[0].sender.id+":" + mcs.entry[0].messaging[0].message.text);

                //傳送訊息
                SendTextToUser(mcs.entry[0].messaging[0].sender.id);
                
            }
            catch (Exception exx)
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "tmp_msg\\" + DateTime.Now.ToString("yyyyMMddHHmmsss") + "-error.js",
                    exx.Message);
            }


        }

        //傳送的code
        private void SendTextToUser(string userId)
        {
            var d = new SendModel();

            d.message = new message { text = "Hi,你好現在是下班時間，請稍後再傳。" };
            d.recipient = new recipient { id = userId };

            var path = "https://graph.facebook.com/v2.6/me/messages?access_token=" + FANPAGE_TOKEN;
            var request = (HttpWebRequest)WebRequest.Create(path);
            request.ContentType = "application/json";
            request.Method = "POST";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(d));
                streamWriter.Flush();
                streamWriter.Close();
            }

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (request.HaveResponse && response != null)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        Response.Write(result);
                    }
                }
            }


        }
    }
}