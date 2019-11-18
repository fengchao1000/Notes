using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NetCoreAPICSRFClient.Helper;

namespace NetCoreAPICSRFClient.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestAPIHelper
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entityString"></param>
        /// <param name="method"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public string RequestAPI(string url, string entityString, string method, out string errorMsg)
        {
            errorMsg = string.Empty;
            HttpWebResponse response = new HttpWebResponse();

            byte[] entityByte = new UTF8Encoding().GetBytes(entityString);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method.ToUpper();
            request.ContentType = ConstanceHelper.requestAPIContentType_json;
            request.ContentLength = entityByte.Length;
           
            try
            {
                request.Headers.Add("Authorization", "Bearer " + GetTokenAsync());
                request.AddXSRTOKEN();

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(entityByte, 0, entityByte.Length);
                }

                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                errorMsg = ex.Message;
                response = (HttpWebResponse)ex.Response;
            }

            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public string RequestAPI(string url, string method, out string errorMsg)
        {
            errorMsg = string.Empty;
            HttpWebResponse response = new HttpWebResponse();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = ConstanceHelper.requestAPIContentType_json;

            try
            {
                request.Headers.Add("Authorization", "Bearer " + GetTokenAsync());
                request.AddXSRTOKEN();
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                errorMsg = ex.Message;
                response = (HttpWebResponse)ex.Response;
            }

            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public string RequestAPI(string url, out string errorMsg)
        {
            errorMsg = string.Empty;
            HttpWebResponse response = new HttpWebResponse();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = ConstanceHelper.requestAPIMethod_Get;
            request.ContentType = ConstanceHelper.requestAPIContentType_json;

            try
            {
                request.Headers.Add("Authorization", "Bearer " + GetTokenAsync());
                request.AddXSRTOKEN();
                response = (HttpWebResponse)request.GetResponse();

            }
            catch (WebException ex)
            {
                errorMsg = ex.Message;
                response = (HttpWebResponse)ex.Response;
            }

            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetTokenAsync()
        {
            return "";
        }


    }
}
