using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPICSRFClient.Helper
{
    public class ConstanceHelper
    {

        #region Request API Type
        public const string apiType_PO = "PO";
        public const string apiType_ReportBottomSellingSKU = "ReportBottomSellingSKU";
        public const string apiType_ReportBottomTurnaroundSKU = "ReportBottomTurnaroundSKU";
        public const string apiType_ReportCommercialPerformance = "ReportCommercialPerformance";
        public const string apiType_ReportCommercialPerformanceDay = "ReportCommercialPerformanceDay";
        public const string apiType_ReportCommercialPerformanceHour = "ReportCommercialPerformanceHour";
        public const string apiType_ReportCommercialPerformanceWeek = "ReportCommercialPerformanceWeek";
        public const string apiType_ReportAvgCommercialPerformance = "ReportAvgCommercialPerformance";
        public const string apiType_ReportInventory = "ReportInventory";
        public const string apiType_ReportKPIMonth = "ReportKPIMonth";
        public const string apiType_ReportKPIQuarter = "ReportKPIQuarter";
        public const string apiType_ReportKPIWeek = "ReportKPIWeek";
        public const string apiType_ReportPOStatus = "ReportPOStatus";
        public const string apiType_ReportReturnRate = "ReportReturnRate";
        public const string apiType_ReportSalesPerformanceHeatmap = "ReportSalesPerformanceHeatmap";
        public const string apiType_ReportTopConsumer = "ReportTopConsumer";
        public const string apiType_ReportTopReturnSKU = "ReportTopReturnSKU";
        public const string apiType_ReportTopSellingSKU = "ReportTopSellingSKU";
        public const string apiType_ReportTopTurnaroundSKU = "ReportTopTurnaroundSKU";
        public const string apiType_DataAreaEntity = "DataAreaEntity";
        public const string apiType_AllDataAreaEntity = "AllDataAreaEntity";
        public const string apiType_InventLocationEntity = "InventLocationEntity";
        public const string apiType_InventSiteEntity = "InventSiteEntity";
        public const string apiType_ReportReturnSKU = "ReportReturnSKU";
        public const string apiType_ReportConsumer = "ReportConsumer";
        public const string apiType_ReportSellingSKU = "ReportSellingSKU";
        public const string apiType_ReportTurnaroundSKU = "ReportTurnaroundSKU";
        public const string apiType_CustomerHistoryEntity = "CustomerHistoryEntity";
        public const string apiType_SalesTableTotalQty = "SalesTableTotalQty";
        public const string apiType_SalesLineTotalQty = "SalesLineTotalQty";
        public const string apiType_WMSInboundTrackEntity = "WMSInboundTrackEntity";
        public const string apiType_WMSInboundPutAwayEntity = "WMSInboundPutAwayEntity";
        public const string apiType_WMSInboundPutAwayTotalQty = "WMSInboundPutAwayTotalQty";
        public const string apiType_SalesParmTableEntity = "SalesParmTableEntity";
        public const string apiType_SalesParmTableTotalQty = "SalesParmTableTotalQty";
        public const string apiType_SalesParmLineEntity = "SalesParmLineEntity";
        public const string apiType_SalesParmLineTotalQty = "SalesParmLineTotalQty";
        public const string apiType_ReturnParmLineEntity = "ReturnParmLineEntity";
        public const string apiType_ReturnParmLineTotalQty = "ReturnParmLineTotalQty";
        public const string apiType_InventCountTrackEntity = "InventCountTrackEntity";
        public const string apiType_InventCountTotalQty = "InventCountTotalQty";
        public const string apiType_ItemMasterEntity = "ItemMasterEntity";
        public const string apiType_ItemMasterTableEntity = "ItemMasterTableEntity";
        public const string apiType_ItemMasterTableTotalQty = "ItemMasterTableTotalQty";
        public const string apiType_ItemMasterTotalQty = "ItemMasterTotalQty";
        public const string apiType_CustAddressEntity = "CustAddressEntity";
        public const string apiType_ProductImageTable = "ProductImageTable";
        public const string apiType_ProductImageEntity = "ProductImageEntity";
        public const string apiType_SalesTableTrackingEntity = "SalesTableTrackingEntity";
        public const string apiType_LogisticsAddressStateEntity = "LogisticsAddressStateEntity";
        public const string apiType_DocuTextEntity = "DocuTextEntity";
        public const string apiType_DocuFileEntity = "DocuFileEntity";
        public const string apiType_DocuFileTableEntity = "DocuFileTableEntity";
        public const string apiType_DocuFileTableTotalQty = "DocuFileTableTotalQty";
        public const string apiType_SalesLineTrackingEntity = "SalesLineTrackingEntity";
        public const string apiType_WMSReasonCodeEntity = "WMSReasonCodeEntity";
        public const string apiType_AdminCompanyEntity = "AdminCompanyEntity";
        public const string apiType_AdminCompanyTotalQty = "AdminCompanyTotalQty";
        public const string apiType_ReasonTableEntity = "ReasonTableEntity";
        public const string apiType_ReasonCodeMapEntity = "ReasonCodeMapEntity";
        public const string apiType_DeleteReasonCodeMapEntity = "DeleteReasonCodeMapEntity";
        public const string apiType_AllReasonCodeMapEntity = "AllReasonCodeMapEntity";
        public const string apiType_AllReasonCodeGroupEntity = "AllReasonCodeGroupEntity";
        public const string apiType_PutawayMyDamco = "PutawayMyDamco";

        public const string apiType_AutomaticReportKPIWeek = "AutomaticReportKPIWeek";
        public const string apiType_ReceiptToPutaway = "ReceiptToPutaway";
        public const string apiType_CourierPerformance = "CourierPerformance";
        public const string apiType_ReturnManagement = "ReturnManagement";
        public const string apiType_PickSuccess = "PickSuccess";
        public const string apiType_ProcessToShip = "ProcessToShip";
        public const string apiType_WarehouseStockTakeReport = "WarehouseStockTakeReport";
        public const string apiType_CycleCountStyle = "CycleCountStyle";
        public const string apiType_CycleCountLocations = "CycleCountLocations";
        public const string apiType_CycleCountUnits = "CycleCountUnits";
        public const string apiType_SystemAvailability = "SystemAvailability";
        public const string apiType_Outbound = "Outbound";
        public const string apiType_ReturnViewModel = "ReturnViewModel";
        public const string apiType_InboundEntity = "InboundEntity";
        public const string apiType_KPIsettingEntity = "KPISettingEntity";
        public const string apiType_KPIWorkdayEntity = "KPIWorkdayEntity";
        public const string apiType_ProductImageViewLogEntity = "ProductImageViewLogEntity";
        public const string apiType_WmsshipmentEntity = "WmsshipmentEntity";
        public const string apiType_WmsshipmentEntityQty = "WmsshipmentEntityQty";
        public const string apiType_KewillMyDamcoEntity = "KewillMyDamcoEntity";
        public const string apiType_ProductImageEntityQty = "ProductImageEntityQty";
        public const string apiType_ShippingOrderEntity = "ShippingOrderEntity";
        public const string apiType_ShippingOrderEntityQty = "ShippingOrderEntityQty";
        public const string apiType_SystemExecutionHistory = "SystemExecutionHistory";
        public const string apiType_SysExecutionHistoryPage = "SysExecutionHistoryPage";
        public const string apiType_prodGroup = "ProdGroup";



        #endregion

        #region Request API Parameter
        public const string requestAPIParameter_InventLocationID = "inventLocationID";
        public const string requestAPIParameter_InventSiteID = "inventSiteID";
        public const string requestAPIParameter_DataAreaID = "dataAreaID";
        public const string requestAPIParameter_CurrentPage = "currentPage";
        public const string requestAPIParameter_IsDemo = "isDemo";
        public const string requestAPIParameter_CustID = "custID";
        public const string requestAPIParameter_Partition = "partition";
        public const string requestAPIParameter_ItemID = "itemID";
        public const string requestAPIParameter_SalesID = "salesID";
        public const string requestAPIParameter_PageIndex = "pageIndex";
        public const string requestAPIParameter_PageSize = "pageSize";
        public const string requestAPIParameter_SortBy = "sortBy";
        public const string requestAPIParameter_isGlobalSearch = "isGlobalSearch";
        public const string requestAPIParameter_SearchFields = "searchFields";
        public const string requestAPIParameter_IsAsc = "isAsc";
        public const string requestAPIParameter_SoldDateFrom = "soldDateFrom";
        public const string requestAPIParameter_SoldDateTo = "soldDateTo";
        public const string requestAPIParameter_SoldDateTimeFrom = "soldDateTimeFrom";
        public const string requestAPIParameter_SoldDateTimeTo = "soldDateTimeTo";
        public const string requestAPIParameter_Party = "party";
        public const string requestAPIParameter_Status = "status";
        public const string requestAPIParameter_ChannelType = "channelType";
        public const string requestAPIParameter_State = "state";
        public const string requestAPIParameter_CustAccount = "custAccount";
        public const string requestAPIParameter_PostedDateTimeFrom = "postedDateTimeFrom";
        public const string requestAPIParameter_PostedDateTimeTo = "postedDateTimeTo";
        public const string requestAPIParameter_ReasonCodeIDExist = "reasonCodeIDExist";
        public const string requestAPIParameter_DeliveryDateTimeFrom = "deliveryDateTimeFrom";
        public const string requestAPIParameter_DeliveryDateTimeTo = "deliveryDateTimeTo";
        public const string requestAPIParameter_ReceiptConfirmDateTimeFrom = "receiptConfirmDateTimeFrom";
        public const string requestAPIParameter_ReceiptConfirmDateTimeTo = "receiptConfirmDateTimeTo";
        public const string requestAPIParameter_ReceiptDateRequestedFrom = "receiptDateRequestedFrom";
        public const string requestAPIParameter_ReceiptDateRequestedTo = "receiptDateRequestedTo";
        public const string requestAPIParameter_TrackingNumber = "trackingNumber";
        public const string requestAPIParameter_ReturnRegisterDateTimeFrom = "returnRegisterDateTimeFrom";
        public const string requestAPIParameter_ReturnRegisterDateTimeTo = "returnRegisterDateTimeTo";
        public const string requestAPIParameter_ReturnRequestDateTimeFrom = "returnRequestDateTimeFrom";
        public const string requestAPIParameter_ReturnRequestDateTimeTo = "returnRequestDateTimeTo";
        public const string requestAPIParameter_SKUOnTheWay = "skuOnTheWay";
        public const string requestAPIParameter_SKUAvailable = "skuAvailable";
        public const string requestAPIParameter_SKUHasReturn = "skuHasReturn";
        public const string requestAPIParameter_IsTopConsumer = "isTopConsumer";
        public const string requestAPIParameter_ConsumerCount = "consumerCount";
        public const string requestAPIParameter_IsTopReturnSKU = "isTopReturnSKU";
        public const string requestAPIParameter_ReturnSKUCount = "returnSKUCount";
        public const string requestAPIParameter_IsTopSellingSKU = "isTopSellingSKU";
        public const string requestAPIParameter_SellingSKUCount = "sellingSKUCount";
        public const string requestAPIParameter_IsTopTurnaroundSKU = "isTopTurnaroundSKU";
        public const string requestAPIParameter_TurnaroundSKUCount = "turnaroundSKUCount";
        public const string requestAPIParameter_CountDateFrom = "countDateFrom";
        public const string requestAPIParameter_CountDateTo = "CountDateTo";
        public const string requestAPIParameter_Style = "style";
        public const string requestAPIParameter_WMSLocationID = "wmsLocationID";
        public const string requestAPIParameter_InventCategoryID = "inventCategoryID";
        public const string requestAPIParameter_ShipmentID = "shipmentID";
        public const string requestAPIParameter_CountryRegionID = "countryRegionID";
        public const string requestAPIParameter_StateID = "stateID";
        public const string requestAPIParameter_ReasonGroupID = "reasonGroupID";
        public const string requestAPIParameter_Ordering = "ordering";
        public const string requestAPIParameter_RollingDays = "rollingDays";
        public const string requestAPIParameter_SalesType = "salesType";
        public const string requestAPIParameter_RefRecID = "refRecID";
        public const string requestAPIParameter_RefTable = "refTable";
        public const string requestAPIParameter_PreferredTimeZoneID = "preferredTimeZoneID";
        public const string requestAPIParameter_Localization = "localization";
        public const string requestAPIParameter_CompanyName = "companyName";
        public const string requestAPIParameter_RecIDs = "recIDs";
        public const string requestAPIParameter_RecID = "recID";
        public const string requestAPIParameter_ModifiedBy = "modifiedBy";
        public const string requestAPIParameter_Name = "name";
        public const string requestAPIParameter_Type = "type";
        public const string requestAPIParameter_Warehousee = "warehouse";
        public const string requestAPIParameter_kPIBreach = "kPIBreach";
        public const string requestAPIParameter_ShipCarrierID = "shipCarrierID";
        public const string requestAPIParameter_IsWorkday = "isWorkday";
        public const string requestAPIParameter_Year = "year";
        public const string requestAPIParameter_UserID = "userID";
        public const string requestAPIParameter_ProductImageViewLogCount = "productImageViewLogCount";
        public const string requestAPIParameter_DateTimeFrom = "dateTimeFrom";
        public const string requestAPIParameter_DateTimeTo = "dateTimeTo";
        public const string requestAPIParameter_TimeZone = "timeZone";
        public const string requestAPIParameter_ResponseReasonCode = "responseReasonCode";
        public const string requestAPIParameter_MessageType = "messageType";
        public const string requestAPIParameter_Correlationid = "correlationid";
        public const string requestAPIParameter_JournalID = "journalID";
        #endregion

        #region Request API Method
        public const string requestAPIMethod_Get = "GET";
        public const string requestAPIMethod_Post = "POST";
        public const string requestAPIMethod_Put = "PUT";
        public const string requestAPIMethod_Delete = "DELETE";
        #endregion

        #region Request API Content Type
        public const string requestAPIContentType_json = "application/json";
        #endregion


        #region Request CSV Name
        public const string csvFileName_CustAddressEntityView = "CustAddressEntityView_";
        public const string csvFileName_CustomerHistoryEntityView = "CustomerHistoryEntityView_";
        public const string csvFileName_SalesOrderSalesParmTableEntityView = "SalesOrderSalesParmTableEntityView_";
        public const string csvFileName_DocuRef = "DocuRef_";
        public const string csvFileName_InventTable = "InventTable_";
        public const string csvFileName_ProductDim = "ProductDim_";
        public const string csvFileName_ProductDimDef = "ProductDimDef_";
        public const string csvFileName_InventItemBarcode = "InventItemBarcode_";
        public const string csvFileName_WmsinboundTrackEntityView = "WMSInboundTrackEntityView_";
        public const string csvFileName_ProductImage = "ProductImage_";
        public const string csvFileName_InventCountLineEntityView = "InventCountLineEntityView_";
        public const string csvFileName_ItemEntityView = "ItemEntityView_";
        public const string csvFileName_InventLocation = "InventLocation_";
        public const string csvFileName_InventSite = "InventSite_";
        public const string csvFileName_LogisticsAddressState = "LogisticsAddressState_";
        public const string csvFileName_ReasonWMSTable = "ReasonWMSTable_";
        public const string csvFileName_ReturnOrderSalesLineView = "ReturnOrderSalesLineView_";
        public const string csvFileName_ReturnOrderTrackingEntityView = "ReturnOrderTrackingEntityView_";
        public const string csvFileName_SalesLineTrackingEntityView = "SalesLineTrackingEntityView_";
        public const string csvFileName_SalesOrderSalesParmLineEntityView = "SalesOrderSalesParmLineEntityView_";
        public const string csvFileName_SalesTableTrackingEntityView = "SalesTableTrackingEntityView_";
        public const string csvFileName_WMSInboundPutAwayEntityView = "WMSInboundPutAwayEntityView_";
        public const string csvFileName_DataArea = "DataArea_";
        public const string csvFileName_MyDamco = "MyDamco_";
        public const string csvFileName_ReportSellingSkuentityView = "ReportSellingSKUEntityView_";
        public const string csvFileName_ReportTurnaroundSkuentityView = "ReportTurnaroundSKUEntityView_";
        public const string csvFileName_ReportCommercialPerformance = "ReportCommercialPerformance_";
        public const string csvFileName_ReportInventory = "ReportInventory_";
        public const string csvFileName_ReportKPIMonth = "ReportKPIMonth_";
        public const string csvFileName_ReportKPIQuarter = "ReportKPIQuarter_";
        public const string csvFileName_ReportKPIWeek = "ReportKPIWeek_";
        public const string csvFileName_ReportReturnRate = "ReportReturnRate_";
        public const string csvFileName_ReportSalesPerformanceHeatmap = "ReportSalesPerformanceHeatmap_";
        public const string csvFileName_ReportShippingOrder = "ReportShippingOrder_";
        public const string csvFileName_ReportInventoryItem = "ReportInventoryItem_";
        public const string csvFileName_ReportConsumerEntityView = "ReportConsumerEntityView_";
        public const string csvFileName_ReportTopReturnSKUEntityView = "ReportTopReturnSKUEntityView_";
        #endregion

        #region Commercial Performance Type
        public const string commercialPerformanceType_OrderQty = "order qty";
        public const string commercialPerformanceType_Revenue = "revenue";
        public const string commercialPerformanceType_SKUQty = "sku qty";
        public const string commercialPerformanceType_ItemsQty = "items qty";
        #endregion

        #region DateTime Format
        public const string dateFormat = "yyyy-MM-dd";
        public const string monthFormat = "yyyy-MM";
        public const string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        #endregion

        #region Country 
        public const string countryCode_China = "CN";
        public const string country_China = "China";
        #endregion

        #region Warehouse
        public const string warehouseCode_OceanEast = "OE";
        public const string warehouse_OceanEast = "Ocean East";
        #endregion

        #region Request API Parameter Default Value
        public const long partition = 5000000000;
        public const int pageIndex = 1;
        public const int pageSize = 20;
        public const bool isAsc = false;
        public const int timeZone = 0;
        public const int channelType = 0;
        public const bool errorCodeExist = false;
        public const int sellingSKUCount = 10;
        public const int turnaroundSKUCount = 10;
        public const int returnSKUCount = 10;
        public const int reportDataCount = 10;
        public const int consumerCount = 10;
        public const int rollingWeeks = 6;
        public const int rollingDaysForInventory = 14;
        public const int avgRollingWeeks = 8;
        public const int ordering = 0;
        public const int rollingDaysForPO = 180;
        public const int firstDay = 1;
        public const bool isDemo = false;
        public const string salesSortBy = "ReceiptDateRequested";
        #endregion

        public const string dateTimeDefault = "1900/1/1 0:00:00";
        public static DateTime defaultDateTime = Convert.ToDateTime("1900-01-01");

        #region API error handling message custom message  
        public const string error_MissingField = "Missing fields {0}";
        public const string error_InvalidField = "Invalid field {0}";
        public const string error_RequiredField = "{0} is required";
        public const string error_SameField = "All {0}s must be the same";
        public const string error_FailedUpdate = "Failed to update {0}";
        public const string error_FailedRenew = "Failed to renew {0}";
        public const string error_FailedCreate = "Failed to create {0}";
        public const string error_Exist = "{0} already exist";
        public const string error_NotFound = "{0} not found";
        public const string error_FieldIsNull = "The field {0} is null";
        public const string error_Unavailablel = "temporarily_unavailable";
        #endregion

        public const string CreatedDateTimeName = "createddatetime";
        public const string ModifiedDateTimeName = "modifieddatetime";
        public const string itemSortBy = "ItemID";

        #region
        public const string Monday = "monday";
        public const string Tuesday = "tuesday";
        public const string Wednesday = "wednesday";
        public const string Thursday = "thursday";
        public const string Friday = "friday";
        public const string Saturday = "saturday";
        public const string Sunday = "sunday";
        #endregion
    }
}
