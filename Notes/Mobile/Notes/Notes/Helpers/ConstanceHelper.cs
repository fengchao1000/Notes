using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Helpers
{
    public class ConstanceHelper
    { 
        /// <summary>
        /// 数据刷新时间间隔，不经常改变的数据，一天刷新一次
        /// </summary>
        public const int NextRefreshIntervalForDataThatDoNotChange = 60*24;
        
        /// <summary>
        /// //数据刷新时间间隔，30分钟
        /// </summary>
        public const int NextRefreshInterval = 30;
        
        /// <summary>
        /// 数据加载状态条延迟时间，50ms
        /// </summary>
        public const int RefreshDelay = 50;
        
        /// <summary>
        /// 列表金额标题名称
        /// </summary>
        public const string AmountHeaderText = "金额";
       
        /// <summary>
       /// 列表时间段标题名称
       /// </summary>
        public const string PeriodOfTimeHeaderText = "时间段";
       
        /// <summary>
        /// 列表数量标题名称
        /// </summary>
        public const string NumberHeaderText = "数量";
        
        /// <summary>
        /// 默认timeFormat
        /// </summary>
        public const string DefaultTimeFormat = "yyyy/MM/dd HH:mm";
    } 

    public class ErrorMessages
    {
        public const string ErrorUnavailablel = "temporarily_unavailable";
        public const string ErrorInvalidUsernameOrPassword = "invalid_username_or_password";//必须与IS4的ConstanceHelper中定义的值保持一致
        public const string ErrorInvalidGrant = "invalid_grant";
        public const string ErrorSystemException = "系统异常，请稍后重试!";
        public const string ErrorInvalidUsernameOrPasswordCN = "登录失败，用户名或密码错误!";
    }

    public class MessageKeys
    { 
        public const string BookmarkReadKey = "BookmarkReadKey";
        public const string TaskListKey = "TaskListKey";
        public const string AddTaskStartDateKey = "AddTaskStartDateKey";
        public const string AddTaskEndDateKey = "AddTaskEndDateKey";
    }

    public class RefreshTimeKeys
    {
        public const string OverViewNextRefreshTimeKey = "OverViewNextRefreshTime";
        public const string MessagePagedListRefreshTimeKey = "MessagePagedListRefreshTime";
        public const string EbayUserRefreshTimeKey = "EbayUserRefreshTime";
        public const string PayPalEmailAddressRefreshTimeKey = "PayPalEmailAddressRefreshTime"; 
    }
     

    
}
