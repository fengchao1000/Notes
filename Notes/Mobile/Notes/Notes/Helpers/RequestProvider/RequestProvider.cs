
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Notes.Models;
using Notes.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Notes.Helpers
{
    public class RequestProvider
    {
        private readonly JsonSerializerSettings serializerSettings;

        public RequestProvider()
        {
            serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            serializerSettings.Converters.Add(new StringEnumConverter());
        }

        static RequestProvider baseRequestProvider;
        public static RequestProvider Current
        {
            get { return baseRequestProvider ?? (baseRequestProvider = new RequestProvider()); }
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <typeparam name="TResult">返回实体类型</typeparam>
        /// <param name="uri">请求地址</param>
        /// <returns></returns>
        public async Task<ResultData<TResult>> GetAsync<TResult>(string uri)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = CreateHttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                result = await GetResultData<TResult>(response);
            }
            catch (Exception ex)
            {
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
                Crashes.TrackError(ex);
            }
            return result;
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <typeparam name="TResult">返回实体类型</typeparam>
        /// <param name="uri">请求地址</param>
        /// <param name="content">HttpContent</param>
        /// <returns></returns>
        public async Task<ResultData<TResult>> PostAsync<TResult>(string uri, HttpContent content)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = CreateHttpClient();
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                result = await GetResultData<TResult>(response);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
            }
            return result;
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <typeparam name="TResult">返回实体类型</typeparam>
        /// <param name="uri">请求地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="header">请求头</param>
        /// <returns></returns>
        public async Task<ResultData<TResult>> PostAsync<TResult>(string uri, Object data, string header = "")
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = CreateHttpClient();

                if (!string.IsNullOrEmpty(header))
                {
                    AddHeaderParameter(httpClient, header);
                }

                string contentString = JsonConvert.SerializeObject(data);//debug

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                result = await GetResultData<TResult>(response);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
            }
            return result;
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <typeparam name="TResult">返回实体类型</typeparam>
        /// <param name="uri">请求地址</param>
        /// <returns></returns>
        public async Task<ResultData<TResult>> PostAsync<TResult>(string uri)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = CreateHttpClient();
                var content = new StringContent("");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                result = await GetResultData<TResult>(response);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
            }
            return result;
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <typeparam name="TResult">返回实体类型</typeparam>
        /// <typeparam name="TFilter">请求数据类型</typeparam>
        /// <param name="uri">请求地址</param>
        /// <param name="data">请求数据</param>
        /// <returns></returns>
        public async Task<ResultData<TResult>> PostAsync<TResult, TFilter>(string uri, TFilter data)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = CreateHttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                result = await GetResultData<TResult>(response);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
            }
            return result;
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <typeparam name="TResult">返回实体类型</typeparam>
        /// <typeparam name="TFilter">请求数据类型</typeparam>
        /// <param name="uri">请求地址</param>
        /// <param name="data">请求数据</param>
        /// <returns></returns>
        public async Task<ResultData<TResult>> PostAsync<TResult>(string uri, string data)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = CreateHttpClient();

                var content = new StringContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                result = await GetResultData<TResult>(response);
            }
            catch (Exception ex)
            {
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
                Crashes.TrackError(ex);
            }
            return result;
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <typeparam name="TResult">返回实体类型</typeparam>
        /// <param name="uri">请求地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="clientId">clientId</param>
        /// <param name="clientSecret">clientSecret</param>
        /// <returns></returns>
        public async Task<ResultData<TResult>> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = CreateHttpClient();

                if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
                {
                    AddBasicAuthenticationHeader(httpClient, clientId, clientSecret);
                }

                var content = new StringContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                result = await GetResultData<TResult>(response);
            }
            catch (Exception ex)
            {
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
                Crashes.TrackError(ex);
            }
            return result;
        }

        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <typeparam name="TResult">返回实体类型</typeparam>
        /// <param name="uri">请求地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="header">请求头</param>
        /// <returns></returns>
        public async Task<ResultData<TResult>> PutAsync<TResult>(string uri, TResult data, string header = "")
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = CreateHttpClient();

                if (!string.IsNullOrEmpty(header))
                {
                    AddHeaderParameter(httpClient, header);
                }

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PutAsync(uri, content);
                result = await GetResultData<TResult>(response);
            }
            catch (Exception ex)
            {
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
                Crashes.TrackError(ex);
            }
            return result;
        }

        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <param name="uri">请求地址</param>
        /// <returns></returns>
        public async Task DeleteAsync(string uri)
        {
            HttpClient httpClient = CreateHttpClient();
            await httpClient.DeleteAsync(uri);
        }

        /// <summary>
        /// 创建HttpClient
        /// </summary>
        /// <returns></returns>
        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //if (!string.IsNullOrEmpty(UserTokenSetting.Current.AccessToken))
            //{
            //    //todo
            //    //string testToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjZFMEUwM0E2NjM1Q0VGRUE4NzYzNjk4RTE4Q0QxMEFFOEQyMzE3REMiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJiZzREcG1OYzctcUhZMm1PR00wUXJvMGpGOXcifQ.eyJuYmYiOjE1NTg1OTkzMjksImV4cCI6MTU1OTIwNDEyOSwiaXNzIjoiaHR0cDovLzE5Mi4xNjguMS41MiIsImF1ZCI6WyJodHRwOi8vMTkyLjE2OC4xLjUyL3Jlc291cmNlcyIsImFwaSJdLCJjbGllbnRfaWQiOiJBcHAiLCJzdWIiOiI2YmI2MDAyOS04NTUzLTRmMTktYmVlMy01YzQzM2FmZjg3ZWUiLCJhdXRoX3RpbWUiOjE1NTg1OTkzMjksImlkcCI6ImxvY2FsIiwiQWRtaW5Vc2VyS2V5IjoiNmJiNjAwMjktODU1My00ZjE5LWJlZTMtNWM0MzNhZmY4N2VlIiwiVXNlcklEIjoiU2Vhbl90ZXN0IiwiVXNlck5hbWUiOiJYRyIsIlVzZXJLZXkiOiI1NTRmNDY1NS0yNjY1LTQ4NGMtYjAwNy1hMjhmNjJjZDQyNTciLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiYXBpIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInB3ZCJdfQ.ZgmByyreeLer7gkUZZONNc6JwPe31Xn4g7thP3O22dD7dn52L-SXeB7YetevnOjjUnmXZnnsB4XYuiUVRzDx5JgqD7NVkcAgTVY9YCFhBAEqikPrmivhhwSuOK2_b8awSGBvPPaBSl0U1OfZa2ExVh4WQ1EEyHFR8vW5AXvOQL5ICXWxUq5SuJFBXkc2i6F5ZCqgMiYFiL-nfOUBuKpOMq4Z6EQx7GyJPddujHZIPKXlGOeXi6eZBinitkeX7v4psagWzUyLJt2UiJpQ2_awHDztFecQQMEg1A-1UUihGOChNJlC8ZYmS951IoDquTRH2LJhm-Td6QByaxx339Vxxg";
            //    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(UserTokenSetting.Current.TokenType, testToken);
            //    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(UserTokenSetting.Current.TokenType, UserTokenSetting.Current.AccessToken);
            //}
            return httpClient;
        }

        /// <summary>
        /// 添加请求头参数
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="parameter">参数</param>
        private void AddHeaderParameter(HttpClient httpClient, string parameter)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrEmpty(parameter))
                return;

            httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
        }

        /// <summary>
        /// 添加认证请求头
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        private void AddBasicAuthenticationHeader(HttpClient httpClient, string clientId, string clientSecret)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                return;

            httpClient.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(clientId, clientSecret);
        }

        /// <summary>
        /// 获取返回结果，解析成ResultData<TResult>
        /// </summary>
        /// <typeparam name="TResult">返回数据类型</typeparam>
        /// <param name="response">HttpResponseMessage</param>
        /// <returns></returns>
        private async Task<ResultData<TResult>> GetResultData<TResult>(HttpResponseMessage response)
        {
            var code = response.StatusCode;
            switch (code)
            {
                case HttpStatusCode.OK:
                    string serialized = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(serialized))
                    {
                        return new ResultData<TResult>() { IsSuccess = true, Message = "" };
                    }
                    else
                    {
                        TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized, serializerSettings));
                        return new ResultData<TResult>() { IsSuccess = true, Data = result, Message = "" }; 
                    } 
                case HttpStatusCode.Created:
                    return new ResultData<TResult>() { IsSuccess = true, Message = await response.Content.ReadAsStringAsync() };
                case HttpStatusCode.Unauthorized:
                    //401可能出现的原因token过期，则需要刷新token
                    //await ServicesManager.IdentityService.RefreshToken(UserTokenSetting.Current.RefreshToken);
                    return new ResultData<TResult>() { IsSuccess = false, Message = "" };
                default:
                    var message = await response.Content.ReadAsStringAsync();
                    return new ResultData<TResult>() { IsSuccess = false, Message = message };
            }
        }

    }
}
