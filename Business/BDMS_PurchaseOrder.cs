using DataAccess;
using Newtonsoft.Json;
using Properties; 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Script.Serialization;

namespace Business
{
    public class BDMS_PurchaseOrder
    {
        private IDataAccess provider;
        public BDMS_PurchaseOrder()
        {
            try
            {
                provider = new ProviderFactory().GetProvider();
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_ICTicket", "provider : " + e1.Message, null);
            }
        }

        public List<PProcurementStatus> GetProcurementStatus(int ProcurementStatusHeaderID)
        {
            string endPoint = "ProcurementMasters/GetProcurementStatus?ProcurementStatusHeaderID=" + ProcurementStatusHeaderID;
            return JsonConvert.DeserializeObject< List<PProcurementStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
            

        public PApiResult GetPurchaseOrderHeader(int? DealerID, string VendorID, string PurchaseOrderNo, DateTime? PurchaseOrderDateF, DateTime? PurchaseOrderDateT
            , int? PurchaseOrderStatusID, int? PurchaseOrderTypeID, int? DivisionID, int? LocationID, int? PageIndex = null, int? PageSize = null)
        {
            //string endPoint = "PurchaseOrder/PurchaseOrderHeader?DealerID=" + DealerID + "&VendorID=" + VendorID  + "&PurchaseOrderNo=" + PurchaseOrderNo
            //    + "&PurchaseOrderDateF=" + PurchaseOrderDateF + "&PurchaseOrderDateT=" + PurchaseOrderDateT + "&PurchaseOrderStatusID=" + PurchaseOrderStatusID 
            //    + "&PurchaseOrderTypeID=" + PurchaseOrderTypeID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;


            string endPoint = "PurchaseOrder/PurchaseOrderHeader?DealerID=" + DealerID + "&VendorID=" + VendorID + "&PurchaseOrderNo=" + PurchaseOrderNo
                + "&PurchaseOrderDateF=" + PurchaseOrderDateF + "&PurchaseOrderDateT=" + PurchaseOrderDateT + "&PurchaseOrderStatusID=" + PurchaseOrderStatusID
                + "&PurchaseOrderTypeID=" + PurchaseOrderTypeID + "&DivisionID=" + DivisionID + "&LocationID=" + LocationID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public PPurchaseOrder GetPurchaseOrderByID(long PurchaseOrderID)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderByID?PurchaseOrderID=" + PurchaseOrderID;
            return JsonConvert.DeserializeObject<PPurchaseOrder>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PPurchaseOrder GetPurchaseOrderByMaterial(string PurchaseOrderNumber, int DivisionID)
        {
            string endPoint = "PurchaseOrder/GetPurchaseOrderByMaterial?PurchaseOrderNumber=" + PurchaseOrderNumber + "&DivisionID=" + DivisionID;
            return JsonConvert.DeserializeObject<PPurchaseOrder>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PDMS_PurchaseOrder> GetPurchaseOrderPG(string filter)
        {
            string endPoint = "PurchaseOrder/PurchaseOrder?Filter=" + filter;
            return JsonConvert.DeserializeObject<List<PDMS_PurchaseOrder>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }
        public List<PDMS_PurchaseOrder> GetPurchaseOrderPerformanceLinq(string filter1, string filter2)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderPerformanceLinq?filter1=" + filter1 + "&filter2=" + filter2;
            return JsonConvert.DeserializeObject<List<PDMS_PurchaseOrder>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }

        public List<PDMS_PurchaseOrder> GetPurchaseOrderMonthily(string filter)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderMonthily?filter=" + filter;
            return JsonConvert.DeserializeObject<List<PDMS_PurchaseOrder>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }

        public DataTable GetPurchaseOrderReport(int? DealerID, string CustomerCode, string PurchaseOrderNo, DateTime? PurchaseOrderDateF, DateTime? PurchaseOrderDateT, string PurchaseOrderStatusID, string MaterialCode, int? PurchaseOrderTypeID, long UserID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter PurchaseOrderNoP = provider.CreateParameter("PurchaseOrderNo", string.IsNullOrEmpty(PurchaseOrderNo) ? null : PurchaseOrderNo, DbType.String);
                DbParameter PurchaseOrderDateFP = provider.CreateParameter("PurchaseOrderDateF", PurchaseOrderDateF, DbType.DateTime);
                DbParameter PurchaseOrderDateTP = provider.CreateParameter("PurchaseOrderDateT", PurchaseOrderDateT, DbType.DateTime);
                DbParameter PurchaseOrderStatusIDP = provider.CreateParameter("PurchaseOrderStatusID", string.IsNullOrEmpty(PurchaseOrderStatusID) ? null : PurchaseOrderStatusID, DbType.String);
                DbParameter PurchaseOrderTypeIDP = provider.CreateParameter("PurchaseOrderTypeID", PurchaseOrderTypeID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                DbParameter[] Params = new DbParameter[9] { DealerIDP, CustomerCodeP, PurchaseOrderNoP, PurchaseOrderDateFP, PurchaseOrderDateTP, PurchaseOrderStatusIDP, MaterialCodeP, PurchaseOrderTypeIDP, UserIDP };


                using (DataSet DataSet = provider.Select("ZDMS_GetPurchaseOrderReport", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
                return null;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return null;
        }

        public DataTable GetPurchaseOrderAsnReport(int? DealerID, string CustomerCode, string AsnNumber, DateTime? AsnDateF, DateTime? AsnDateT, int? PurchaseOrderAsnStatusID, string PurchaseOrderNo, DateTime? PurchaseOrderDateF, DateTime? PurchaseOrderDateT, int? PurchaseOrderTypeID, string MaterialCode, long UserID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);

                DbParameter AsnNumberP = provider.CreateParameter("AsnNumber", string.IsNullOrEmpty(AsnNumber) ? null : AsnNumber, DbType.String);
                DbParameter AsnDateFP = provider.CreateParameter("AsnDateF", AsnDateF, DbType.DateTime);
                DbParameter AsnDateTP = provider.CreateParameter("AsnDateT", AsnDateT, DbType.DateTime);
                DbParameter PurchaseOrderAsnStatusIDP = provider.CreateParameter("PurchaseOrderAsnStatusID", PurchaseOrderAsnStatusID, DbType.Int32);

                DbParameter PurchaseOrderNoP = provider.CreateParameter("PurchaseOrderNo", string.IsNullOrEmpty(PurchaseOrderNo) ? null : PurchaseOrderNo, DbType.String);
                DbParameter PurchaseOrderDateFP = provider.CreateParameter("PurchaseOrderDateF", PurchaseOrderDateF, DbType.DateTime);
                DbParameter PurchaseOrderDateTP = provider.CreateParameter("PurchaseOrderDateT", PurchaseOrderDateT, DbType.DateTime);

                DbParameter PurchaseOrderTypeIDP = provider.CreateParameter("PurchaseOrderTypeID", PurchaseOrderTypeID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                DbParameter[] Params = new DbParameter[12] { DealerIDP, CustomerCodeP, MaterialCodeP, AsnNumberP, AsnDateFP, AsnDateTP, PurchaseOrderAsnStatusIDP
                    , PurchaseOrderNoP, PurchaseOrderDateFP, PurchaseOrderDateTP, PurchaseOrderTypeIDP,UserIDP };


                using (DataSet DataSet = provider.Select("ZDMS_GetPurchaseOrderAsnReport", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
                return null;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return null;
        }

        public DataTable GetPurchaseOrderInvoiceReport(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, string PurchaseOrderNo, DateTime? PurchaseOrderDateF, DateTime? PurchaseOrderDateT, int? PurchaseOrderTypeID, string MaterialCode, long UserID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);

                DbParameter PurchaseOrderNoP = provider.CreateParameter("PurchaseOrderNo", string.IsNullOrEmpty(PurchaseOrderNo) ? null : PurchaseOrderNo, DbType.String);
                DbParameter PurchaseOrderDateFP = provider.CreateParameter("PurchaseOrderDateF", PurchaseOrderDateF, DbType.DateTime);
                DbParameter PurchaseOrderDateTP = provider.CreateParameter("PurchaseOrderDateT", PurchaseOrderDateT, DbType.DateTime);

                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);

                DbParameter PurchaseOrderTypeIDP = provider.CreateParameter("PurchaseOrderTypeID", PurchaseOrderTypeID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                DbParameter[] Params = new DbParameter[11] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, PurchaseOrderNoP, PurchaseOrderDateFP, PurchaseOrderDateTP, PurchaseOrderTypeIDP, MaterialCodeP, UserIDP };

                using (DataSet DataSet = provider.Select("ZDMS_GetPurchaseOrderInvoiceReport", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
                return null;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return null;
        }
        public PApiResult GetPurchaseOrderAsnHeader(int? DealerID, int? DealerOfficeID, string VendorID, string AsnNumber, DateTime? AsnDateF, DateTime? AsnDateT
            , int? AsnStatusID, string PurchaseOrderNo, string SaleOrderNo, string InvoiceNo, int? PurchaseOrderTypeID, int? DivisionID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderAsnHeader?DealerID=" + DealerID + "&DealerOfficeID=" + DealerOfficeID + "&VendorID=" + VendorID + "&AsnNumber=" + AsnNumber
                + "&AsnDateF=" + AsnDateF + "&AsnDateT=" + AsnDateT + "&AsnStatusID=" + AsnStatusID + "&PurchaseOrderNo=" + PurchaseOrderNo
                + "&SaleOrderNo=" + SaleOrderNo + "&InvoiceNo=" + InvoiceNo + "&PurchaseOrderTypeID=" + PurchaseOrderTypeID + "&DivisionID=" + DivisionID
                + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public List<PAsn> GetPurchaseOrderAsnByID(long? PurchaseOrderID, long? AsnID)
        {
            string endPoint = "PurchaseOrder/GetPurchaseOrderAsnByID?PurchaseOrderID=" + PurchaseOrderID + "&AsnID=" + AsnID;
            return JsonConvert.DeserializeObject<List<PAsn>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        //public PAsn GetPurchaseOrderAsnByID(long AsnID)
        //{
        //    string endPoint = "PurchaseOrder/PurchaseOrderAsnByID?AsnID=" + AsnID;
        //    return JsonConvert.DeserializeObject<PAsn>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        //}
        public List<PGr> GetPurchaseOrderAsnGrDetByID(long AsnID)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderAsnGrDetByID?AsnID=" + AsnID;
            return JsonConvert.DeserializeObject<List<PGr>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        //public List<PAsnItem> GetPurchaseOrderAsnItemByID(long AsnID)
        //{
        //    string endPoint = "PurchaseOrder/PurchaseOrderAsnItemByID?AsnID=" + AsnID;
        //    return JsonConvert.DeserializeObject<List<PAsnItem>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        //}
        public PApiResult GetPurchaseOrderAsnGrHeader(int? DealerID,int? DealerOfficeID, string VendorID, string GrNumber, DateTime? GrDateF, DateTime? GrDateT
           , int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderAsnGrHeader?DealerID=" + DealerID + "&DealerOfficeID=" + DealerOfficeID + "&VendorID=" + VendorID + "&GrNumber=" + GrNumber
                + "&GrDateF=" + GrDateF + "&GrDateT=" + GrDateT  + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PGr GetPurchaseOrderAsnGrByID(long GrID)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderAsnGrByID?GrID=" + GrID;
            return JsonConvert.DeserializeObject<PGr>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PGrItem> GetPurchaseOrderAsnGrByIDPOItem(long GrID)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderAsnGrByIDPOItem?GrID=" + GrID;
            return JsonConvert.DeserializeObject<List<PGrItem>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        
        public List<PAsnItem> GetPurchaseOrderAsnByIDPOItem(long AsnID)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderAsnByIDPOItem?AsnID=" + AsnID;
            return JsonConvert.DeserializeObject<List<PAsnItem>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
         
            
            
        public PApiResult GetPurchaseOrderAsnGrForPoReturnCreation(int DealerID, int? VendorID, int? LocationID, int? DivisionID,string GrNumber)
        {
            string endPoint = "PurchaseOrder/GetPurchaseOrderAsnGrForPoReturnCreation?DealerID=" + DealerID + "&VendorID=" + VendorID
                + "&LocationID=" + LocationID + "&DivisionID=" + DivisionID + "&GrNumber=" + GrNumber;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetPurchaseOrderReturnHeader(int? DealerID, string PurchaseOrderReturnNo, DateTime? PurchaseOrderReturnDateF, DateTime? PurchaseOrderReturnDateT
            , int? LocationID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderReturnHeader?DealerID=" + DealerID + "&PurchaseOrderReturnNo=" + PurchaseOrderReturnNo
                 + "&PurchaseOrderReturnDateF=" + PurchaseOrderReturnDateF + "&PurchaseOrderReturnDateT=" + PurchaseOrderReturnDateT
                 + "&LocationID=" + LocationID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PPurchaseOrderReturn GetPurchaseOrderReturnByID(long PurchaseOrderReturnID)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderReturnByID?PurchaseOrderReturnID=" + PurchaseOrderReturnID;
            return JsonConvert.DeserializeObject<PPurchaseOrderReturn>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetPurchaseOrderReturnItemForDeliveryCreation(long PurchaseOrderReturnID)
        {
            string endPoint = "PurchaseOrder/GetPurchaseOrderReturnItemForDeliveryCreation?PurchaseOrderReturnID=" + PurchaseOrderReturnID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public List<PPurchaseOrderReturnDelivery> GetPurchaseOrderReturnDeliveryByPoReturnID(long PurchaseOrderReturnID)
        {

            string endPoint = "PurchaseOrder/GetPurchaseOrderReturnDeliveryByPoReturnID?PurchaseOrderReturnID=" + PurchaseOrderReturnID;
            return JsonConvert.DeserializeObject<List< PPurchaseOrderReturnDelivery>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }


        public PApiResult GetValidateDealerStockOrderControl(int DealerID)
        {
            string endPoint = "PurchaseOrder/GetValidateDealerStockOrderControl?DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public DataTable GetPurchaseOrderFromCart(string DealerCode)
        {
            string endPoint = "PurchaseOrder/GetPurchaseOrderFromCart?DealerCode=" + DealerCode;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetDealerStockOrderControl(int? DealerID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "PurchaseOrder/GetDealerStockOrderControl?DealerID=" + DealerID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetPurchaseOrderExportToExcel(int? DealerID, string VendorID, string PurchaseOrderNo, DateTime? PurchaseOrderDateF, DateTime? PurchaseOrderDateT
            , int? PurchaseOrderStatusID, int? PurchaseOrderTypeID, int? DivisionID, int? LocationID)
        {
            string endPoint = "PurchaseOrder/GetPurchaseOrderExportToExcel?DealerID=" + DealerID + "&VendorID=" + VendorID + "&PurchaseOrderNo=" + PurchaseOrderNo
                + "&PurchaseOrderDateF=" + PurchaseOrderDateF + "&PurchaseOrderDateT=" + PurchaseOrderDateT + "&PurchaseOrderStatusID=" + PurchaseOrderStatusID
                + "&PurchaseOrderTypeID=" + PurchaseOrderTypeID + "&DivisionID=" + DivisionID + "&LocationID=" + LocationID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult UpdatePurchaseOrderReturnStatus(long PurchaseOrderReturnID, int StatusID, string Remarks)
        {
            string endPoint = "PurchaseOrder/UpdatePurchaseOrderReturnStatus?PurchaseOrderReturnID=" + PurchaseOrderReturnID + "&StatusID=" + StatusID + "&Remarks=" + Remarks;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PAttachedFile AttachmentsForDownload(string FileName)
        {
            string endPoint = "PurchaseOrder/AttachmentsForDownload?FileName=" + FileName ;
            return JsonConvert.DeserializeObject<PAttachedFile>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public DataTable GetPurchaseOrderAsnExcel(int? DealerID, int? DealerOfficeID, string VendorID, string AsnNumber, DateTime? AsnDateF, DateTime? AsnDateT
    , int? AsnStatusID, string PurchaseOrderNo, string SaleOrderNo, string InvoiceNo, int? PurchaseOrderTypeID, int? DivisionID, int WithDetail)
        {
            string endPoint = "PurchaseOrder/GetPurchaseOrderAsnExcel?DealerID=" + DealerID + "&DealerOfficeID=" + DealerOfficeID + "&VendorID=" + VendorID + "&AsnNumber=" + AsnNumber
                + "&AsnDateF=" + AsnDateF + "&AsnDateT=" + AsnDateT + "&AsnStatusID=" + AsnStatusID + "&PurchaseOrderNo=" + PurchaseOrderNo
                + "&SaleOrderNo=" + SaleOrderNo + "&PurchaseOrderTypeID=" + PurchaseOrderTypeID + "&DivisionID=" + DivisionID;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}
