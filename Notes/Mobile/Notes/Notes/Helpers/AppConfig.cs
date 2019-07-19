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
        //public const string PAAPIEndpoint = "https://api.pushauction.com";//production
        //public const string PAAPIEndpoint = "https://betaapi.pushauction.com";//beta
        public const string PAAPIEndpoint = "http://219.134.95.26:8001";//local

        /// <summary>
        /// PA系统登录地址，即IS4地址
        /// </summary>
        //public const string BaseIdentityEndpoint = "https://login.pushauction.com";//production 
        //public const string BaseIdentityEndpoint = "https://betalogin.pushauction.com";//beta
        public const string BaseIdentityEndpoint = "http://219.134.95.30:8001";//local

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
        public const string SqliteFilename = "pushauction.db3";

        #endregion

        #region api config

        public static string SearchEbayListingSummaryListingHourlyReportTotalEndpoint = $"{PAAPIEndpoint}/EbayListingSummary/ListingHourly/ReportTotal/Search";

        public static string GetEbayUserShortNameListEndpoint = $"{PAAPIEndpoint}/Cross/EbayUser/ShortName/Get";

        public static string SearchMobileOverViewListEndpoint = $"{PAAPIEndpoint}/Mobile/MobileOverView/Search";

        public static string SearchMobileFeedbackReportListEndpoint = $"{PAAPIEndpoint}/Mobile/MobileFeedbackReport/Search";

        public static string SearchMobileListingReportListEndpoint = $"{PAAPIEndpoint}/Mobile/MobileListingReport/Search";

        public static string SearchMobileDispatchReportListEndpoint = $"{PAAPIEndpoint}/Mobile/MobileShippedReport/Search";

        public static string SearchMobilePayPalReportListEndpoint = $"{PAAPIEndpoint}/Mobile/MobilePayPalReport/Search";

        public static string SearchMobileSalesReportListEndpoint = $"{PAAPIEndpoint}/Mobile/MobileSalesReport/Search";

        public static string SearchMobileVisitReportListEndpoint = $"{PAAPIEndpoint}/Mobile/MobileVisitReport/Search";

        public static string SearchMobileMessageEndpoint = $"{PAAPIEndpoint}/Mobile/MobileMessage/Search";

        public static string GetMobileMessageEndpoint = $"{PAAPIEndpoint}/Mobile/MobileMessage/Get";

        public static string AddUserDeviceEndpoint = $"{PAAPIEndpoint}/Mobile/MobileUserDevice/Add";

        public static string DeleteUserDeviceEndpoint = $"{PAAPIEndpoint}/Mobile/MobileUserDevice/Delete";

        public static string UpdateMessageIsNotDisplayEndpoint = $"{PAAPIEndpoint}/Mobile/MobileReadMessage/UpdateMessageIsNotDisplay";

        public static string AddReadMessageEndpoint = $"{PAAPIEndpoint}/Mobile/MobileReadMessage/Add";

        public static string GetUserDetailInfoEndpoint = $"{PAAPIEndpoint}/Data/User/CurrentUser/Get";

        public static string SearchPayPalEmailAddressEndpoint = $"{PAAPIEndpoint}/Mobile/PayPalEmailAddress/Search";

        #endregion
    }
}