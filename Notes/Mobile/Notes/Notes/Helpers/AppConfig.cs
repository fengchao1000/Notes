namespace Notes.Helpers
{
    public class AppConfig
    {
        #region base config
        
        /// <summary>
        /// Syncfusion报表插件的密钥
        /// </summary>
        public const string SyncfusionLicense = "NzEzNDVAMzEzNjJlMzQyZTMwSUlPcU1RaktJK1BNd3NvQzRBYnByYW9mbVdtcG5qUGQ2aHFZMHAyViswUT0=";

        /// <summary>
        /// 是否使用模拟服务
        /// </summary>
        public static bool IsUseTheMockService = false;

        /// <summary>
        /// 客户端id，必须和IS4的Config保持一致
        /// </summary>
        public const string ClientId = "App";

        /// <summary>
        /// 客户端密钥，必须和IS4的Config保持一致
        /// </summary>
        public const string ClientSecret = "secret";

        /// <summary>
        /// PA主系统API地址
        /// </summary> 
        public const string PAAPIEndpoint = "http://106.52.218.254:8081";

        /// <summary>
        /// PA系统登录地址，即IS4地址
        /// </summary> 
        public const string BaseIdentityEndpoint = "";

        /// <summary>
        /// 认证地址
        /// </summary>
        public static string AuthorizeEndpoint = $"{BaseIdentityEndpoint}/connect/authorize";

        /// <summary>
        /// 获取用户信息地址
        /// </summary>
        public static string UserInfoEndpoint = $"{BaseIdentityEndpoint}/connect/userinfo";

        /// <summary>
        /// 获取token地址
        /// </summary>
        public static string TokenEndpoint = $"{BaseIdentityEndpoint}/connect/token";

        /// <summary>
        /// PA Sqlite数据库名称
        /// </summary>
        public const string SqliteFilename = "notes.db3";

        #endregion

        #region api config

        public static string BookmarkUrl = $"{PAAPIEndpoint}/api/app/bookmark";

        public static string CategoryUrl = $"{PAAPIEndpoint}/api/app/category";
         
        #endregion
    }
}