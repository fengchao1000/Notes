using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Helpers
{
    /// <summary>
    /// 任务类型，年月日
    /// </summary>
    public enum TaskType
    {
        Year,
        Month,
        Day
    }

    /// <summary>
    /// 任务优先级别
    /// </summary>
    public enum TaskPriority
    {
        Lower,
        Ordinary,
        emergency,
        VeryUrgent
    }

    /// <summary>
    /// 连接来源
    /// </summary>
    public enum LinkSourceType
    {
        Cnblogs,
        Jianshu,
        CSDN,
        Other
    }

    public enum LoadMoreStatus
    {
        StausDefault = 0,
        StausLoading = 1,
        StausNodata = 2,
        StausEnd = 3,
        StausFail = 4,
        StausError = 5,
        StausNologin = 6
    }

    public enum PushType
    {
        /// <summary>
        /// 群发
        /// </summary>
        Group = 0,
        /// <summary>
        /// 单发
        /// </summary>
        Single = 1
    }

    /// <summary>
    /// 通知类型，必须和API端保持一致
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// 消息通知
        /// </summary>
        Message = 0,

        /// <summary>
        /// 更新通知
        /// </summary>
        Update = 1, 
    } 

    /// <summary>
    /// 统计日期类型
    /// </summary>
    public enum DateType
    {
        /// <summary>
        /// 按天分组统计
        /// </summary>
        Day = 0,

        /// <summary>
        /// 按周分组统计
        /// </summary>
        Week = 1,

        /// <summary>
        /// 按月分组统计
        /// </summary>
        Month = 2,
    }

    /// <summary>
    /// PayPal统计类型，必须和API端保持一致
    /// </summary>
    public enum PayPalStatisticsType
    {
        /// <summary>
        /// 收款
        /// </summary>
        PayPalPaymentReceived = 0,
        /// <summary>
        /// 净额
        /// </summary>
        PayPalNetReceived = 1,
        /// <summary>
        /// 费用
        /// </summary>
        PayPalFee = 2, 
        /// <summary>
        /// 退款
        /// </summary>
        PayPalPaymentRefund = 3

    }

    /// <summary>
    /// 评价统计类型，必须和API端保持一致
    /// </summary>
    public enum FeedbackStatisticsType
    {
        /// <summary>
        /// 好评
        /// </summary>
        FeedbackPositiveReceived = 0,

        /// <summary>
        /// 中评
        /// </summary>
        FeedbackNeturalReceived = 1,

        /// <summary>
        /// 负评
        /// </summary>
        FeedbackNegativeReceived = 2
    }

    /// <summary>
    /// 刊登统计类型，必须和API端保持一致
    /// </summary>
    public enum ListingStatisticsType
    {
        /// <summary>
        /// 新的刊登
        /// </summary>
        Listing = 0,

        /// <summary>
        /// 新的刊登费
        /// </summary>
        ListingEbayFee = 1,

        /// <summary>
        /// 结束的刊登
        /// </summary>
        ListingEnd = 2,

        /// <summary>
        /// 卖出的刊登
        /// </summary>
        ListingEndSold = 3

    }

    /// <summary>
    /// 销售统计类型，必须和API端保持一致
    /// </summary>
    public enum SalesStatisticsType
    {
        /// <summary>
        /// 销售数量/订单数量
        /// </summary>
        SalesQTY = 0,

        /// <summary>
        /// 销售物品数量
        /// </summary>
        SalesItemQTY = 1,

        /// <summary>
        /// 成交额/售价
        /// </summary>
        SalesAmount = 2,

        /// <summary>
        /// 成交费
        /// </summary>
        EbayFVF = 3
    }


    /// <summary>
    /// 发货统计类型，必须和API端保持一致
    /// </summary>
    public enum ShippedStatisticsType
    {
        /// <summary>
        /// 物品
        /// </summary>
        ShippedItem = 0,

        /// <summary>
        /// 销售总额
        /// </summary>
        SalesAmount = 1,

       /// <summary>
       /// 运费
       /// </summary>
        ShippedActualShippingFee = 2,
         
        /// <summary>
        /// 成本
        /// </summary>
        ShippedProductCost = 3,

        /// <summary>
        /// 成交费
        /// </summary>
        ShippedeBayFVF = 4  

    }

    /// <summary>
    /// 访问统计类型，必须和API端保持一致
    /// </summary>
    public enum VisitStatisticsType
    {
        /// <summary>
        /// 国家
        /// </summary>
        VisitCountry = 0,

        /// <summary>
        /// 科技
        /// </summary>
        VisitOS = 1

    }
}
