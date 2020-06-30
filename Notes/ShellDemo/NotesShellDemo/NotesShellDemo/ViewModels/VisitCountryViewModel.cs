using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NotesShellDemo.ViewModels
{
    public class VisitCountryViewModel : BaseViewModel
    {
        #region 属性

        public ObservableCollection<CountryCountEntity> CountryCountTopTenCollection { get; set; }

        public ObservableCollection<VisitCommonTopTenTableCountEntity> VisitCommonTopTenTableCountEntityCollection { get; set; }

        /// <summary>
        /// 列表高度
        /// </summary>
        public double VisitCommonTopTenTableCountHeightRequest { get; set; } = 500;
          
        #endregion
         
    }

    public class CountryCountEntity
    {
        public string CountryCode { get; set; }
        public int Count { get; set; }
        public string Percentage { get; set; }
        public string Country { get; set; }
    }

    public class VisitCommonTopTenTableCountEntity
    {
        public string Name { get; set; }
        public int TodayCount { get; set; }
        public int YesterdayCount { get; set; }
        public int BeforeYesterdayCount { get; set; }
        public int SameDayLastWeekCount { get; set; }
         
        public string TodayCountFormat
        {
            get
            {
                if (TodayCount == 0)
                {
                    return "-";
                }
                return TodayCount.ToString();
            }
        }
         
        public string YesterdayCountFormat
        {
            get
            {
                if (YesterdayCount == 0)
                {
                    return "-";
                }
                return YesterdayCount.ToString();
            }
        }
         
        public string BeforeYesterdayCountFormat
        {
            get
            {
                if (BeforeYesterdayCount == 0)
                {
                    return "-";
                }
                return BeforeYesterdayCount.ToString();
            }
        }
         
        public string SameDayLastWeekCountFormat
        {
            get
            {
                if (SameDayLastWeekCount == 0)
                {
                    return "-";
                }
                return SameDayLastWeekCount.ToString();
            }
        }
    }
}
