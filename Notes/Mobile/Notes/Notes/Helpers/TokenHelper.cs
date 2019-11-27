using Notes.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;

namespace Notes.Helpers
{
    public class TokenHelper
    {
        /// <summary>
        /// 从缓存获取token
        /// </summary>
        /// <returns> Token为null时跳到登录页面</returns>
        public static async Task<Token> AcquireTokenAsync()
        {
            Token currentToken;
            try
            {
                currentToken = TokenCacheHelper.LoadFromCache();

                //判断缓存是否有token，如果没有token
                if (currentToken != null )
                {
                    if (currentToken.AccessToken != null)
                    {
                        return currentToken;
                    }
                } 

                ResultData<Token> result = await RefreshAccessTokenAsync(currentToken).ConfigureAwait(false);
                if (result.IsSuccess && null != result.Data)
                {
                    currentToken = result.Data;
                    if (currentToken != null && currentToken.AccessToken != null)
                    {
                        TokenCacheHelper.StoreTokenToCache(currentToken);
                    }
                }
                else
                {
                    TokenCacheHelper.Clear();
                    return null;
                }
            }
            catch
            {
                return null;
            }
            return currentToken;
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public static async Task<ResultData<Token>> RefreshAccessTokenAsync(Token currentToken)
        {
            var fields = new Dictionary<string, string>
            { 
                { TokenRequest.GrantType, GrantTypes.ClientCredentials } 
            }; 

            ResultData<Token> result = new ResultData<Token>();
            result.IsSuccess = false;
            int retryCount = 0;
            bool retry = false;
            do
            {
                retry = false;
                result = await RequestProvider.Current.PostAsync<Token>(AppConfig.Token, new FormUrlEncodedContent(fields), AppConfig.ClientId, AppConfig.ClientSercret,false);
                if (result.IsSuccess)
                {
                    return result;
                } 
                else//网络或者其他原因导致不成功
                {
                    ToastHelper.Current.SendToast(result.Message);

                    retry = true;
                    retryCount++;
                    Thread.Sleep(1000);
                }
            }
            while ((retry == true) && (retryCount < 3));
              
            return result;
        }

    }
}
