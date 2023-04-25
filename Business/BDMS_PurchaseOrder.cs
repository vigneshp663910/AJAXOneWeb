using DataAccess;
using Newtonsoft.Json;
using Properties;
using SapIntegration;
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

        public PApiResult GetPurchaseOrderHeader(int? DealerID, string VendorID, string PurchaseOrderNo, DateTime? PurchaseOrderDateF, DateTime? PurchaseOrderDateT
            , int? PurchaseOrderStatusID, int? PurchaseOrderTypeID, int? PageIndex = null, int? PageSize = null)
        {
            //string endPoint = "PurchaseOrder/PurchaseOrderHeader?DealerID=" + DealerID + "&VendorID=" + VendorID  + "&PurchaseOrderNo=" + PurchaseOrderNo
            //    + "&PurchaseOrderDateF=" + PurchaseOrderDateF + "&PurchaseOrderDateT=" + PurchaseOrderDateT + "&PurchaseOrderStatusID=" + PurchaseOrderStatusID 
            //    + "&PurchaseOrderTypeID=" + PurchaseOrderTypeID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;


            string endPoint = "PurchaseOrder/PurchaseOrderHeader?DealerID=" + DealerID + "&VendorID=" + VendorID + "&PurchaseOrderNo=" + PurchaseOrderNo
                + "&PurchaseOrderDateF=" + PurchaseOrderDateF + "&PurchaseOrderDateT=" + PurchaseOrderDateT + "&PurchaseOrderStatusID=" + PurchaseOrderStatusID
                + "&PurchaseOrderTypeID=" + PurchaseOrderTypeID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public List<PDMS_PurchaseOrder> GetPurchaseOrderPG(string filter)
        {
            string endPoint = "PurchaseOrder/PurchaseOrder?Filter=" + filter;
            return JsonConvert.DeserializeObject<List<PDMS_PurchaseOrder>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
 
        } 
        public List<PDMS_PurchaseOrder> GetPurchaseOrderPerformanceLinq(string filter1, string filter2)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderPerformanceLinq?filter1=" + filter1+ "&filter2=" + filter2;
            return JsonConvert.DeserializeObject<List<PDMS_PurchaseOrder>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }

        public List<PDMS_PurchaseOrder> GetPurchaseOrderMonthily(string filter)
        {
            string endPoint = "PurchaseOrder/PurchaseOrderMonthily?filter=" + filter;
            return JsonConvert.DeserializeObject<List<PDMS_PurchaseOrder>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }
          
        public DataTable GetPurchaseOrderReport(int? DealerID, string CustomerCode, string PurchaseOrderNo, DateTime? PurchaseOrderDateF, DateTime? PurchaseOrderDateT, string PurchaseOrderStatusID, string MaterialCode, int? PurchaseOrderTypeID,long UserID)
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


                PDMS_PurchaseOrderN SOI = new PDMS_PurchaseOrderN();
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


                PDMS_PurchaseOrderN SOI = new PDMS_PurchaseOrderN();
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


                PDMS_PurchaseOrderN SOI = new PDMS_PurchaseOrderN();
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
       
    }
}
