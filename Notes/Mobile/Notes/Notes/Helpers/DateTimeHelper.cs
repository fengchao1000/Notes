using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Notes.Helpers
{
    /// <summary>
    /// 日期时间操作
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 日期
        /// </summary>
        private static DateTime? _dateTime;

        public static DateTime DefaultDateTime = Convert.ToDateTime("1900-1-1");

        public static readonly string HourFormat = " HH:mm";

        public static readonly string eBayNewAPITimeFormate = "yyyy-MM-ddThh:mm:ssZ";
        
         
    }
}
