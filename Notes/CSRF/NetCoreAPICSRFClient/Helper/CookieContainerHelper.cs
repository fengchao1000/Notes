using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreAPICSRFClient.Helper
{
    public static class CookieContainerHelper
    {
        /// <summary>
        /// 全局CookieContainer
        /// </summary>
        private static CookieContainer cookieContainer;

        /// <summary>
        /// XSRTOKEN
        /// </summary>
        private static string XSRTOKEN;

        /// <summary>
        /// 添加XSRTOKEN认证
        /// </summary>
        /// <returns></returns>
        public static void AddXSRTOKEN(this HttpWebRequest request)
        {
            if (cookieContainer != null && !string.IsNullOrEmpty(XSRTOKEN))
            {
                request.CookieContainer = cookieContainer;
                request.Headers.Add("X-XSRF-TOKEN", XSRTOKEN);
                return;
            }

            int retryCount = 0;
            bool retry = false;

            do
            {
                retry = false;
                try
                {
                    HttpWebRequest requestXSRTOKEN = (HttpWebRequest)WebRequest.Create("http://localhost:50188/api/Security");
                    requestXSRTOKEN.Method = ConstanceHelper.requestAPIMethod_Get;
                    requestXSRTOKEN.ContentType = ConstanceHelper.requestAPIContentType_json;
                    requestXSRTOKEN.KeepAlive = true;
                    requestXSRTOKEN.CookieContainer = new CookieContainer();
                    HttpWebResponse responseXSRTOKEN = (HttpWebResponse)requestXSRTOKEN.GetResponse();

                    using (System.IO.Stream streamReceive = responseXSRTOKEN.GetResponseStream())
                    {
                        Encoding enc = Encoding.UTF8;
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(streamReceive, enc))
                        {
                            XSRTOKEN = sr.ReadToEnd();
                        }
                    }

                    cookieContainer = new CookieContainer();

                    foreach (Cookie item in responseXSRTOKEN.Cookies)
                    {
                        cookieContainer.Add(new Uri("http://localhost:50188"), new Cookie(item.Name, item.Value));
                    }

                    request.CookieContainer = cookieContainer;
                    request.Headers.Add("X-XSRF-TOKEN", XSRTOKEN);
                }
                catch (Exception ex)
                {
                    retry = true;
                    retryCount++;
                    Thread.Sleep(1000);

                    if (retryCount >= 3)
                    {
                        throw new Exception(String.Format("An error occurred while acquiring a XSRF-TOKEN \nTime: {0}\nError: {1}\nRetry: {2}\n",
                        DateTime.Now.ToString(),
                        ex.ToString(),
                        retry.ToString()));
                    }
                }

            } while ((retry == true) && (retryCount < 3));
        }
    }
}
