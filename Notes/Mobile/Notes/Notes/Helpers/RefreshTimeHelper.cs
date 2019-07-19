using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Notes.Helpers
{
    /// <summary>
    /// 缓存刷新时间
    /// </summary>
    public static class RefreshTimeHelper
    {
        /// <summary>
        /// 用ConcurrentDictionary作为缓存，把刷新时间存储在内存中
        /// </summary>
        private static ConcurrentDictionary<string, DateTime> RefreshTimeDictionary = new ConcurrentDictionary<string, DateTime>();
         
        /// <summary>
        /// 通过Key获取刷新时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime GetRefreshTime(string key)
        {
            try
            {
                DateTime refreshTime = RefreshTimeDictionary[key];
                if (refreshTime == null)
                {
                    return DateTimeHelper.DefaultDateTime;
                }
                return refreshTime;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return DateTimeHelper.DefaultDateTime; 
            }
        }
         
        /// <summary>
        /// 设置刷新时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="refreshTime"></param>
        public static void SetRefreshTime(string key, DateTime refreshTime)
        {
            try
            {
                if (RefreshTimeDictionary.ContainsKey(key))
                {
                    RefreshTimeDictionary[key] = refreshTime;
                }
                else
                {
                    RefreshTimeDictionary.TryAdd(key, refreshTime);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        } 
    }
}
