
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization; 
using Notes.Models; 
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
                ContractResolver = new CamelCasePropertyNamesContractResolver(),//CamelCase 风格
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
        public async Task<ResultData<TResult>> GetAsync<TResult>(string uri, bool needAuthorization = false)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = await CreateHttpClient(needAuthorization);
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                result = await GetResultData<TResult>(response,uri);
            }
            catch (Exception ex)
            {
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
                LoggerHelper.Current.Error(ex.ToString());
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
        public async Task<ResultData<TResult>> PostAsync<TResult>(string uri, HttpContent content, bool needAuthorization = true)
        {
            ResultData<TResult> result = new ResultData<TResult>(); 
            try
            {
                HttpClient httpClient = await CreateHttpClient(needAuthorization); 
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);//.ConfigureAwait(false);
                result = await GetResultData<TResult>(response,uri);
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
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
        public async Task<ResultData<TResult>> PostAsync<TResult>(string uri, Object data, string header = "", bool needAuthorization = false)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = await CreateHttpClient(needAuthorization);

                if (!string.IsNullOrEmpty(header))
                {
                    AddHeaderParameter(httpClient, header);
                }

                string contentString = JsonConvert.SerializeObject(data, serializerSettings);//debug

                var content = new StringContent(JsonConvert.SerializeObject(data, serializerSettings));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                result = await GetResultData<TResult>(response,uri,data);
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
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
        public async Task<ResultData<TResult>> PostAsync<TResult>(string uri, bool needAuthorization = false)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = await CreateHttpClient(needAuthorization);
                var content = new StringContent("");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                result = await GetResultData<TResult>(response,uri);
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
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
        public async Task<ResultData<TResult>> PostAsync<TResult, TFilter>(string uri, TFilter data, bool needAuthorization = false)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = await CreateHttpClient(needAuthorization);

                var content = new StringContent(JsonConvert.SerializeObject(data, serializerSettings));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                result = await GetResultData<TResult>(response,uri,data);
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
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
        public async Task<ResultData<TResult>> PostAsync<TResult>(string uri, string data, bool needAuthorization = false)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = await CreateHttpClient(needAuthorization);

                var content = new StringContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                result = await GetResultData<TResult>(response,uri,data);
            }
            catch (Exception ex)
            {
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
                LoggerHelper.Current.Error(ex.ToString());
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
        public async Task<ResultData<TResult>> PostAsync<TResult>(string uri, HttpContent content, string clientId, string clientSecret, bool needAuthorization = true)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = await CreateHttpClient(needAuthorization);

                if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
                {
                    AddBasicAuthenticationHeader(httpClient, clientId, clientSecret);
                }

                HttpResponseMessage response = await httpClient.PostAsync(uri, content);//.ConfigureAwait(false);
                result = await GetResultData<TResult>(response, uri);
            }
            catch (Exception ex)
            {
                LoggerHelper.Current.Error(ex.ToString());
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
        /// <param name="clientId">clientId</param>
        /// <param name="clientSecret">clientSecret</param>
        /// <returns></returns>
        public async Task<ResultData<TResult>> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret, bool needAuthorization = true)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = await CreateHttpClient(needAuthorization);

                if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
                {
                    AddBasicAuthenticationHeader(httpClient, clientId, clientSecret);
                }

                var content = new StringContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                result = await GetResultData<TResult>(response,uri,data);
            }
            catch (Exception ex)
            {
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
                LoggerHelper.Current.Error(ex.ToString());
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
        public async Task<ResultData<TResult>> PutAsync<TResult>(string uri, Object data, string header = "", bool needAuthorization = false)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = await CreateHttpClient(needAuthorization);

                if (!string.IsNullOrEmpty(header))
                {
                    AddHeaderParameter(httpClient, header);
                }

                var content = new StringContent(JsonConvert.SerializeObject(data, serializerSettings));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PutAsync(uri, content);
                result = await GetResultData<TResult>(response,uri,data);
            }
            catch (Exception ex)
            {
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
                LoggerHelper.Current.Error(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <typeparam name="TResult">返回实体类型</typeparam>
        /// <param name="uri">请求地址</param>
        /// <param name="header">请求头</param>
        /// <returns></returns>
        public async Task<ResultData<TResult>> PutAsync<TResult>(string uri,string header = "", bool needAuthorization = false)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = await CreateHttpClient(needAuthorization);

                if (!string.IsNullOrEmpty(header))
                {
                    AddHeaderParameter(httpClient, header);
                }

                var content = new StringContent("");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PutAsync(uri, content);
                result = await GetResultData<TResult>(response, uri, "");
            }
            catch (Exception ex)
            {
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
                LoggerHelper.Current.Error(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <param name="uri">请求地址</param>
        /// <returns></returns>
        public async Task DeleteAsync(string uri, bool needAuthorization = true)
        {
            HttpClient httpClient = await CreateHttpClient(needAuthorization);
            await httpClient.DeleteAsync(uri);
        }

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <typeparam name="TResult">返回实体类型</typeparam>
        /// <param name="uri">请求地址</param>
        /// <returns></returns>
        public async Task<ResultData<TResult>> DeleteAsync<TResult>(string uri, bool needAuthorization = false)
        {
            ResultData<TResult> result = new ResultData<TResult>();
            try
            {
                HttpClient httpClient = await CreateHttpClient(needAuthorization);
                HttpResponseMessage response = await httpClient.DeleteAsync(uri);
                result = await GetResultData<TResult>(response, uri);
            }
            catch (Exception ex)
            {
                result = new ResultData<TResult>() { IsSuccess = false, Message = ex.Message };
                LoggerHelper.Current.Error(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 创建HttpClient
        /// </summary>
        /// <returns></returns>
        private async Task<HttpClient> CreateHttpClient(bool needAuthorization)
        {

            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!needAuthorization)
            {
                return httpClient;
            }

            Token token = await TokenHelper.AcquireTokenAsync().ConfigureAwait(false);
            if (token == null)
            {
                return httpClient;
            }
            if (string.IsNullOrEmpty(token.AccessToken))
            {
                return httpClient;
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

            //if (AppConfig.CurrentEnvironment == AppEnvironment.Development)
            //{
            //    //    string testToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjZFMEUwM0E2NjM1Q0VGRUE4NzYzNjk4RTE4Q0QxMEFFOEQyMzE3REMiLCJ0eXAiOiJhdCtqd3QiLCJ4NXQiOiJiZzREcG1OYzctcUhZMm1PR00wUXJvMGpGOXcifQ.eyJuYmYiOjE1NzE5NzY2MDgsImV4cCI6MTU3MTk4MDIwOCwiaXNzIjoiaHR0cDovLzE5Mi4xNjguMS41MiIsImF1ZCI6InBhYXBpbW9iaWxlIiwiY2xpZW50X2lkIjoiQXBwIiwic3ViIjoiNmJiNjAwMjktODU1My00ZjE5LWJlZTMtNWM0MzNhZmY4N2VlIiwiYXV0aF90aW1lIjoxNTcxOTc2NjA4LCJpZHAiOiJsb2NhbCIsIkFkbWluVXNlcktleSI6IjZiYjYwMDI5LTg1NTMtNGYxOS1iZWUzLTVjNDMzYWZmODdlZSIsIlVzZXJJRCI6Im1heHRyYWRlIiwiVXNlck5hbWUiOiJNYXggVHJhZGUiLCJVc2VyS2V5IjoiNmJiNjAwMjktODU1My00ZjE5LWJlZTMtNWM0MzNhZmY4N2VlIiwic2NvcGUiOlsib3BlbmlkIiwicHJvZmlsZSIsInBhYXBpbW9iaWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInB3ZCJdfQ.U8rjvYS4uGk0YMrWiC6XE-YbPD6i0XjjWcgncNJCRJk1HVXjl42THUHI6AN5sbTcKeVY_JXao_JEP1KgxsW7meV1fIlmjJw4NabaSYmjxVMnOE0NUDyER7aQOk7XUb-AI9FmF5r6yVGK1hkkpATRHtHQ0ZnmGqxgFXWigMIj9TVujGqAolTsnccY-vm2nTOZRjjvCO2BDfOikY1TjZ77p4A7A20pomsnxBqWih2ahWFc428qSuztb9tWEXaT03w9frKh5WzGbv3PiDcjmSKafoHPNqlf5npkAHaB0tuhyCVDwBZ6KhpmtYcPvog_jvq45VpxDAq-f7dTf1QkF1YzcQ";
            //    //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, testToken);
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);
            //}
            //else
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);
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
        /// <param name="uri">用于记录日志</param>
        /// <param name="data">用于记录日志</param>
        /// <returns></returns>
        private async Task<ResultData<TResult>> GetResultData<TResult>(HttpResponseMessage response, string uri,Object data = null)
        {
            var code = response.StatusCode;
            string responseContent = await response.Content.ReadAsStringAsync();
            //全局日志，用于调试，设置logger的level可以关闭Debug日志
            if (code == HttpStatusCode.OK)
            {
                //LoggerHelper.Current.Debug($"====APILog===ResponseCode:{code}===RquestURI:{uri}===RquestBody:{JsonConvert.SerializeObject(data)}===ResponseContent{responseContent}");
            }
            else
            {
                //LoggerHelper.Current.Error($"====APILog===ResponseCode:{code}===RquestURI:{uri}===RquestBody:{JsonConvert.SerializeObject(data)}===ResponseContent{responseContent}");
            }

            switch (code)
            {
                case HttpStatusCode.OK:
                    
                    if (string.IsNullOrEmpty(responseContent))
                    {
                        return new ResultData<TResult>() { IsSuccess = true, Message = "" };
                    }
                    else
                    {
                        TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseContent, serializerSettings));
                        return new ResultData<TResult>() { IsSuccess = true, Data = result, Message = "" };
                    }
                case HttpStatusCode.BadRequest:
                    if (string.IsNullOrEmpty(responseContent))
                    {
                        return new ResultData<TResult>() { IsSuccess = false, Message = "" };
                    } else if (responseContent.Contains("invalid_grant"))
                    {
                        return new ResultData<TResult>() { IsSuccess = false, Message = "invalid_grant" };
                    }
                    else
                    { 
                        return new ResultData<TResult>() { IsSuccess = false, Message = responseContent };
                    } 
                case HttpStatusCode.Created:
                    return new ResultData<TResult>() { IsSuccess = true, Message = responseContent };
                case HttpStatusCode.RequestTimeout:
                    ToastHelper.Current.SendToast("请求超时，请稍后重试!");
                    return new ResultData<TResult>() { IsSuccess = false, Message = responseContent };
                case HttpStatusCode.Unauthorized:
                    todo://退出登录
                    return new ResultData<TResult>() { IsSuccess = false, Message = "" }; 
                default: 
                    return new ResultData<TResult>() { IsSuccess = false, Message = responseContent };
            }
        }

    }
}
