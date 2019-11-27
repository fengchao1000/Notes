using Newtonsoft.Json;
using Notes.Models;
using System; 
using Xamarin.Essentials;

namespace Notes.Helpers
{
    public static class TokenCacheHelper
    {
        //提前判断token是否过期
        private const int ExpirationMarginInMinutes = 1;

        /// <summary>
        /// 把token存储在内存中
        /// </summary>
        private static Token currentToken = null;

        private static readonly Object cacheLock = new Object();

        /// <summary>
        /// 从缓存加载Token
        /// 如果缓存中没有token，或者RefreshToken为空则返回null，需要用户重新登录
        /// 如果AccessToken过期时间小于等于5分钟，则把AccessToken返回null,需要刷新AccessToken
        /// </summary>
        /// <returns>Token</returns>
        public static Token LoadFromCache()
        {
            lock (cacheLock)
            {
                if (currentToken == null)
                {
                    currentToken = LoadTokenFromSecureStorage();
                }

                if (currentToken == null)
                {
                    return null;
                }

                bool tokenNearExpiry = currentToken.ExpiresOn <= DateTime.UtcNow + TimeSpan.FromMinutes(ExpirationMarginInMinutes);
                if (tokenNearExpiry)
                {
                    currentToken.AccessToken = null;
                }

                return currentToken;
            }
        }

        /// <summary>
        /// 从SecureStorage加载Token
        /// </summary>
        /// <returns></returns>
        public static Token LoadTokenFromSecureStorage()
        {
            Token token = null;
            try
            {
                string tokenString = SecureStorage.GetAsync("token").Result;
                if (!string.IsNullOrEmpty(tokenString))
                {
                    token = JsonConvert.DeserializeObject<Token>(tokenString);
                }
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }

            return token;
        }

        /// <summary>
        /// 存储Token
        /// </summary>
        /// <param name="newToken"></param>
        public static void StoreTokenToCache(Token newToken)
        {
            lock (cacheLock)
            {
                newToken.ExpiresOn = DateTime.UtcNow.AddSeconds(newToken.ExpiresIn);
                currentToken = newToken;
                try
                {
                    //保存在SecureStorage中
                    SecureStorage.SetAsync("token", JsonConvert.SerializeObject(newToken));
                }
                catch (Exception ex)
                {
                    // Possible that device doesn't support secure storage on device.

                }
            }
        }

        /// <summary>
        /// 清空Token
        /// </summary>
        public static void Clear()
        {
            lock (cacheLock)
            {
                currentToken = null;
                try
                {
                    SecureStorage.Remove("token");
                }
                catch (Exception ex)
                {
                    // Possible that device doesn't support secure storage on device.

                }
            }
        }
    }
}
