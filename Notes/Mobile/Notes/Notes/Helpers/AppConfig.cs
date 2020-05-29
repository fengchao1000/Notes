namespace Notes.Helpers
{
    public class AppConfig
    {
        #region base config

        /// <summary>
        /// cnblogs ClientId
        /// </summary>
        public const string ClientId = "cda7b086-4cdf-4aaf-bde5-2aefabefa828";

        /// <summary>
        /// cnblogs ClientSercret
        /// </summary>
        public const string ClientSercret = "E9xR1T6fMb8WJ8fqr3AXMuSrAHUfD4Tgo6MxlArBF5o_XxVK9IWmC498PyM03aAZILhhYHTwgszFFmAk";

        /// <summary>
        /// AppCenter android Secret
        /// </summary>
        public const string AppCenterAndroidSecret = "04968255-5537-46bb-bbe9-fe96712d20db";

        /// <summary>
        /// Syncfusion报表插件的密钥
        /// </summary>
        public const string SyncfusionLicense = "";

        /// <summary>
        /// 是否使用模拟服务
        /// </summary>
        public static bool IsUseTheMockService = false;
         
        /// <summary>
        /// Notes API地址
        /// </summary> 
        public const string NotesAPIEndpoint = "http://106.52.218.254:8081";

        /// <summary>
        /// cnblogs API地址
        /// </summary> 
        public const string CnblogsAPIEndpoint = "https://api.cnblogs.com";

        /// <summary>
        /// 登录地址
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
        /// Notes Sqlite数据库名称
        /// </summary>
        public const string SqliteFilename = "notes.db3";

        #endregion

        #region api config

        public static string GetBookmarkUrl = $"{NotesAPIEndpoint}/api/app/bookmark";
        public static string GetBookmarkPagedUrl = $"{NotesAPIEndpoint}/api/app/bookmark/paged";
        public static string UpdateBookmarkReadUrl = NotesAPIEndpoint+"/api/app/bookmark/{0}/read?isRead={1}";
        public static string GetCategoryUrl = $"{NotesAPIEndpoint}/api/app/category";

        public static string GetTaskUrl = $"{NotesAPIEndpoint}/api/app/Task";
        public static string AddTaskUrl = $"{NotesAPIEndpoint}/api/app/Task";
        public static string DeleteTaskUrl = $"{NotesAPIEndpoint}/api/app/Task"; 

        public const string Token = "https://oauth.cnblogs.com/connect/token";
        public const string ArticleBody = CnblogsAPIEndpoint + "/api/blogposts/{0}/body";
        public const string ArticleHome = CnblogsAPIEndpoint + "/api/blogposts/@sitehome?pageIndex={0}&pageSize={1}";
        public const string ArticleHot = CnblogsAPIEndpoint + "/api/blogposts/@picked?pageIndex={0}&pageSize={1}";
        public const string ArticleComment = CnblogsAPIEndpoint + "/api/blogs/{0}/posts/{1}/comments?pageIndex={2}&pageSize={3}";
        public const string ArticleCommentAdd = CnblogsAPIEndpoint + "/api/blogs/{0}/posts/{1}/comments";

        #endregion
    }
}