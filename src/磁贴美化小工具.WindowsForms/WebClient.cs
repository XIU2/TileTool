using System;
using System.IO;
using System.Net;

namespace WebClient_cs
{
    public class GetHTTP
    {
        /// <summary>
        /// 获取HTTP
        /// </summary>
        /// <param name="URL">URL</param>
        /// <param name="TimeOut">超时时间，默认：60000 毫秒，单位：毫秒</param>
        /// <param name="Encoding">编码，默认：utf-8</param>
        /// <returns>成功返回网页内容，失败返回 null</returns>
        public static string Get_HTTP(string Url, int TimeOut, string Encoding = "utf-8")
        {
            NewWebClient myWebClient = new NewWebClient(TimeOut);
            Stream myStream = myWebClient.OpenRead(Url);
            StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding(Encoding));
            string strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }
    }
    // 带超时时间的 WebClient
    public class NewWebClient : WebClient
    {
        private int _timeout;

        /// <summary>
        /// 超时时间(毫秒)
        /// </summary>
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public NewWebClient()
        {
            this._timeout = 60000;
        }

        public NewWebClient(int timeout)
        {
            this._timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.Timeout = this._timeout;
            return result;
        }
    }
}
