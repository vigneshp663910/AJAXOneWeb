using DataAccess;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Script.Serialization;

namespace Business
{
    public class BDMS_SalesOrder
    {

        private IDataAccess provider;
        public BDMS_SalesOrder()
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


        public List<PDMS_SalesOrder> GetSalesOrder(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesOrder> SOIs = new List<PDMS_SalesOrder>();
            try
            {
                string query = "SELECT  * from pr_getsalesorderBasedOnInvoice(" + filter + ")";

                //   DataTable dt = new NpgsqlServer().ExecuteReader(query);
                DataTable dt = new BPG().OutputDataTable(query);
                PDMS_SalesOrder SOI = new PDMS_SalesOrder();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_SalesOrder();
                    SOI.SalesOrderID = Convert.ToString(dr["SalesOrderID"]);
                    SOI.SalesOrderNumber = Convert.ToString(dr["SalesOrderID"]);
                    SOI.SalesOrderDate = DBNull.Value == dr["so_date"] ? (DateTime?)null : Convert.ToDateTime(dr["so_date"]);
                    SOI.SalesOrderStatus = Convert.ToString(dr["SalesOrderStatus"]);
                    SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["Dealer_Code"]) };
                    SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                    SOI.InvoiceNumber = Convert.ToString(dr["p_inv_id"]);
                    SOI.InvoiceDate = DBNull.Value == dr["r_inv_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_inv_date"]);
                    SOI.SalesOrderItem = new PDMS_SalesOrderItems();
                    SOI.SalesOrderItem.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["MaterialCode"]),
                        MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                        HSN = Convert.ToString(dr["HSN"]),
                        MaterialType = Convert.ToString(dr["MaterialType"]),
                        MaterialDivision = Convert.ToString(dr["MaterialDivision"])
                    };
                    SOI.SalesOrderItem.UnitBasicPrice = DBNull.Value == dr["UnitBasicPrice"] ? 0 : Convert.ToDecimal(dr["UnitBasicPrice"]);
                    SOI.SalesOrderItem.Qty = DBNull.Value == dr["r_qty"] ? 0 : Convert.ToDecimal(dr["r_qty"]);
                    SOI.SalesOrderItem.Value = Convert.ToDecimal(dr["r_qty"]);
                    SOI.SalesOrderItem.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    SOI.SalesOrderItem.DiscountedPrice = SOI.SalesOrderItem.Value - SOI.SalesOrderItem.Discount;
                    SOI.SalesOrderItem.Tax = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    SOI.SalesOrderItem.FreightAmount = DBNull.Value == dr["ZFRH"] ? 0 : Convert.ToDecimal(dr["ZFRH"]);
                    SOI.SalesOrderItem.TotalAmt = SOI.SalesOrderItem.DiscountedPrice + SOI.SalesOrderItem.Tax + SOI.SalesOrderItem.FreightAmount;

                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrder", ex);
                throw ex;
            }
            return SOIs;
        }

        public List<PDMS_SalesInvoice> GetSaleOrderInvoicePartsReport(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };


                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoicePartsReport", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]), State = Convert.ToString(dr["DealerState"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]), State = new PDMS_State() { State = Convert.ToString(dr["CustomerState"]) } };
                            SOI.GSTNo = Convert.ToString(dr["GSTNO"]);
                            SOI.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            SOI.InvoiceDate = DBNull.Value == dr["InvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["InvoiceDate"]);

                            SOI.SaleOrderNumber = Convert.ToString(dr["SaleOrderNumber"]);
                            SOI.SaleOrderDate = DBNull.Value == dr["SaleOrderDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SaleOrderDate"]);

                            SOI.Status = Convert.ToString(dr["SaleOrderInvoiceStatus"]);
                            SOI.Location = Convert.ToString(dr["Location"]);
                            SOI.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            SOI.ContactNumber = Convert.ToString(dr["ContactNumber"]);
                            SOI.SalesPerson = Convert.ToString(dr["SalesPerson"]);
                            SOI.Reference = Convert.ToString(dr["ContactPerson"]);
                            SOI.ReferenceDate = DBNull.Value == dr["ReferenceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["ReferenceDate"]);
                            SOI.McMode = Convert.ToString(dr["McMode"]);
                            SOI.MachineSlNo = Convert.ToString(dr["MachineSlNo"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();

                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                HSN = Convert.ToString(dr["HSNCode"]),
                                MaterialType = Convert.ToString(dr["MaterialType"]),
                                Product = Convert.ToString(dr["Product"]),
                                ProductGroup = Convert.ToString(dr["ProductGroup"])
                            };
                            SOI.InvoiceItem.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["UnitBasicPrice"]));
                            SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["Qty"]));
                            SOI.InvoiceItem.ReturnQty = Convert.ToDecimal("0" + Convert.ToString(dr["ReturnQty"]));
                            SOI.InvoiceItem.Value = SOI.InvoiceItem.UnitBasicPrice * SOI.InvoiceItem.Qty;

                            SOI.InvoiceItem.Discount = Convert.ToDecimal(dr["Discount"]);

                            SOI.InvoiceItem.DiscountedPrice = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;

                            SOI.InvoiceItem.FreightAmount = DBNull.Value == dr["FreightAmount"] ? 0 : Convert.ToDecimal(dr["FreightAmount"]);

                            SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Discount + SOI.InvoiceItem.Value + SOI.InvoiceItem.FreightAmount;
                            SOI.InvoiceItem.SGST = DBNull.Value == dr["SGST"] ? (decimal?)null : Convert.ToDecimal(dr["SGST"]);
                            SOI.InvoiceItem.SGSTAmt = Convert.ToDecimal(dr["SGSTAmt"]);
                            SOI.InvoiceItem.CGST = DBNull.Value == dr["CGST"] ? (decimal?)null : Convert.ToDecimal(dr["CGST"]);
                            SOI.InvoiceItem.CGSTAmt = Convert.ToDecimal(dr["CGSTAmt"]);

                            SOI.InvoiceItem.IGST = DBNull.Value == dr["IGST"] ? (decimal?)null : Convert.ToDecimal(dr["IGST"]);
                            SOI.InvoiceItem.IGSTAmt = Convert.ToDecimal(dr["IGSTAmt"]);
                            SOI.InvoiceItem.Tax = SOI.InvoiceItem.SGSTAmt + SOI.InvoiceItem.CGSTAmt + SOI.InvoiceItem.IGSTAmt;
                            SOI.InvoiceItem.TaxP = (SOI.InvoiceItem.SGST == null ? 0 : (decimal)SOI.InvoiceItem.SGST)
                                + (SOI.InvoiceItem.CGST == null ? 0 : (decimal)SOI.InvoiceItem.CGST)
                                + (SOI.InvoiceItem.IGST == null ? 0 : (decimal)SOI.InvoiceItem.IGST);
                            SOI.InvoiceItem.TotalAmt = SOI.InvoiceItem.Tax + SOI.InvoiceItem.TaxableAmount;


                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSaleOrderInvoicePartsDealerAndMaterialWise(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };

                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoicePartsDealerAndMaterialWise", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.HeaderCount = Convert.ToInt64(dr["HeaderCount"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                            };
                            SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                            SOI.InvoiceItem.NetAmount = DBNull.Value == dr["NetAmount"] ? 0 : Convert.ToDecimal(dr["NetAmount"]);
                            SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["GrossAmount"] ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
                            SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["ItemCount"]);

                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSaleOrderInvoicePartsDealerCustomerAndMaterialWise(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };

                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoicePartsDealerCustomerAndMaterialWise", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerCode"]) };
                            SOI.HeaderCount = Convert.ToInt64(dr["HeaderCount"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                            };
                            SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                            SOI.InvoiceItem.NetAmount = DBNull.Value == dr["NetAmount"] ? 0 : Convert.ToDecimal(dr["NetAmount"]);
                            SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["GrossAmount"] ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
                            SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["ItemCount"]);

                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }

        public List<PDMS_SalesInvoice> GetSaleOrderInvoiceMcReport(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };


                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceMcReport", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            SOI.GSTNo = Convert.ToString(dr["GSTNO"]);
                            SOI.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            SOI.InvoiceDate = DBNull.Value == dr["InvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["InvoiceDate"]);
                            SOI.Status = Convert.ToString(dr["SaleOrderInvoiceStatus"]);
                            SOI.Location = Convert.ToString(dr["Location"]);
                            SOI.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            SOI.ContactNumber = Convert.ToString(dr["ContactNumber"]);

                            SOI.Reference = Convert.ToString(dr["ContactPerson"]);
                            SOI.ReferenceDate = DBNull.Value == dr["ReferenceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["ReferenceDate"]);
                            SOI.McMode = Convert.ToString(dr["McMode"]);
                            SOI.MachineSlNo = Convert.ToString(dr["MachineSlNo"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();

                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                HSN = Convert.ToString(dr["HSNCode"]),
                                MaterialType = Convert.ToString(dr["MaterialType"]),
                                Product = Convert.ToString(dr["Product"]),
                                ProductGroup = Convert.ToString(dr["ProductGroup"])
                            };
                            SOI.InvoiceItem.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["UnitBasicPrice"]));
                            SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["Qty"]));
                            SOI.InvoiceItem.ReturnQty = Convert.ToDecimal("0" + Convert.ToString(dr["ReturnQty"]));
                            SOI.InvoiceItem.Value = SOI.InvoiceItem.UnitBasicPrice * SOI.InvoiceItem.Qty;

                            SOI.InvoiceItem.Discount = Convert.ToDecimal(dr["Discount"]);

                            SOI.InvoiceItem.DiscountedPrice = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;

                            SOI.InvoiceItem.FreightAmount = DBNull.Value == dr["FreightAmount"] ? 0 : Convert.ToDecimal(dr["FreightAmount"]);

                            SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Discount + SOI.InvoiceItem.Value + SOI.InvoiceItem.FreightAmount;
                            SOI.InvoiceItem.SGST = DBNull.Value == dr["SGST"] ? (decimal?)null : Convert.ToDecimal(dr["SGST"]);
                            SOI.InvoiceItem.SGSTAmt = Convert.ToDecimal(dr["SGSTAmt"]);
                            SOI.InvoiceItem.CGST = DBNull.Value == dr["CGST"] ? (decimal?)null : Convert.ToDecimal(dr["CGST"]);
                            SOI.InvoiceItem.CGSTAmt = Convert.ToDecimal(dr["CGSTAmt"]);

                            SOI.InvoiceItem.IGST = DBNull.Value == dr["IGST"] ? (decimal?)null : Convert.ToDecimal(dr["IGST"]);
                            SOI.InvoiceItem.IGSTAmt = Convert.ToDecimal(dr["IGSTAmt"]);
                            SOI.InvoiceItem.Tax = SOI.InvoiceItem.SGSTAmt + SOI.InvoiceItem.CGSTAmt + SOI.InvoiceItem.IGSTAmt;
                            SOI.InvoiceItem.TaxP = (SOI.InvoiceItem.SGST == null ? 0 : (decimal)SOI.InvoiceItem.SGST)
                               + (SOI.InvoiceItem.CGST == null ? 0 : (decimal)SOI.InvoiceItem.CGST)
                               + (SOI.InvoiceItem.IGST == null ? 0 : (decimal)SOI.InvoiceItem.IGST);
                            SOI.InvoiceItem.TotalAmt = SOI.InvoiceItem.Tax + SOI.InvoiceItem.TaxableAmount;


                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSaleOrderInvoiceMcDealerAndMaterialWise(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };

                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceMcDealerAndMaterialWise", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.HeaderCount = Convert.ToInt64(dr["HeaderCount"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                            };
                            SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                            SOI.InvoiceItem.NetAmount = DBNull.Value == dr["NetAmount"] ? 0 : Convert.ToDecimal(dr["NetAmount"]);
                            SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["GrossAmount"] ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
                            SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["ItemCount"]);

                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSaleOrderInvoiceMcDealerCustomerAndMaterialWise(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };

                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceMcDealerCustomerAndMaterialWise", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerCode"]) };
                            SOI.HeaderCount = Convert.ToInt64(dr["HeaderCount"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                            };
                            SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                            SOI.InvoiceItem.NetAmount = DBNull.Value == dr["NetAmount"] ? 0 : Convert.ToDecimal(dr["NetAmount"]);
                            SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["GrossAmount"] ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
                            SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["ItemCount"]);

                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }


        public List<PDMS_SalesInvoice> GetSaleOrderInvoiceWarrantyReport(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };


                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceWarrantyReport", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            SOI.GSTNo = Convert.ToString(dr["GSTNO"]);
                            SOI.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            SOI.InvoiceDate = DBNull.Value == dr["InvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["InvoiceDate"]);
                            SOI.Status = Convert.ToString(dr["SaleOrderInvoiceStatus"]);
                            SOI.Location = Convert.ToString(dr["Location"]);
                            SOI.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            SOI.ContactNumber = Convert.ToString(dr["ContactNumber"]);
                            SOI.Reference = Convert.ToString(dr["ContactPerson"]);
                            SOI.ReferenceDate = DBNull.Value == dr["ReferenceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["ReferenceDate"]);
                            SOI.McMode = Convert.ToString(dr["McMode"]);
                            SOI.MachineSlNo = Convert.ToString(dr["MachineSlNo"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();

                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                HSN = Convert.ToString(dr["HSNCode"]),
                                MaterialType = Convert.ToString(dr["MaterialType"]),
                                Product = Convert.ToString(dr["Product"]),
                                ProductGroup = Convert.ToString(dr["ProductGroup"])
                            };
                            SOI.InvoiceItem.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["UnitBasicPrice"]));
                            SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["Qty"]));
                            SOI.InvoiceItem.ReturnQty = Convert.ToDecimal("0" + Convert.ToString(dr["ReturnQty"]));
                            SOI.InvoiceItem.Value = SOI.InvoiceItem.UnitBasicPrice * SOI.InvoiceItem.Qty;

                            SOI.InvoiceItem.Discount = Convert.ToDecimal(dr["Discount"]);

                            SOI.InvoiceItem.DiscountedPrice = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;

                            SOI.InvoiceItem.FreightAmount = DBNull.Value == dr["FreightAmount"] ? 0 : Convert.ToDecimal(dr["FreightAmount"]);

                            SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Discount + SOI.InvoiceItem.Value + SOI.InvoiceItem.FreightAmount;
                            SOI.InvoiceItem.SGST = DBNull.Value == dr["SGST"] ? (decimal?)null : Convert.ToDecimal(dr["SGST"]);
                            SOI.InvoiceItem.SGSTAmt = Convert.ToDecimal(dr["SGSTAmt"]);
                            SOI.InvoiceItem.CGST = DBNull.Value == dr["CGST"] ? (decimal?)null : Convert.ToDecimal(dr["CGST"]);
                            SOI.InvoiceItem.CGSTAmt = Convert.ToDecimal(dr["CGSTAmt"]);

                            SOI.InvoiceItem.IGST = DBNull.Value == dr["IGST"] ? (decimal?)null : Convert.ToDecimal(dr["IGST"]);
                            SOI.InvoiceItem.IGSTAmt = Convert.ToDecimal(dr["IGSTAmt"]);
                            SOI.InvoiceItem.Tax = SOI.InvoiceItem.SGSTAmt + SOI.InvoiceItem.CGSTAmt + SOI.InvoiceItem.IGSTAmt;
                            SOI.InvoiceItem.TaxP = (SOI.InvoiceItem.SGST == null ? 0 : (decimal)SOI.InvoiceItem.SGST)
                               + (SOI.InvoiceItem.CGST == null ? 0 : (decimal)SOI.InvoiceItem.CGST)
                               + (SOI.InvoiceItem.IGST == null ? 0 : (decimal)SOI.InvoiceItem.IGST);
                            SOI.InvoiceItem.TotalAmt = SOI.InvoiceItem.Tax + SOI.InvoiceItem.TaxableAmount;


                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSaleOrderInvoiceWarrantyDealerAndMaterialWise(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };

                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceWarrantyDealerAndMaterialWise", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.HeaderCount = Convert.ToInt64(dr["HeaderCount"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                            };
                            SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                            SOI.InvoiceItem.NetAmount = DBNull.Value == dr["NetAmount"] ? 0 : Convert.ToDecimal(dr["NetAmount"]);
                            SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["GrossAmount"] ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
                            SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["ItemCount"]);

                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSaleOrderInvoiceWarrantyDealerCustomerAndMaterialWise(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderWarrantyStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };

                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceWarrantyDealerCustomerAndMaterialWise", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerCode"]) };
                            SOI.HeaderCount = Convert.ToInt64(dr["HeaderCount"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                            };
                            SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                            SOI.InvoiceItem.NetAmount = DBNull.Value == dr["NetAmount"] ? 0 : Convert.ToDecimal(dr["NetAmount"]);
                            SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["GrossAmount"] ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
                            SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["ItemCount"]);

                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }

        public List<PDMS_SalesInvoice> GetSalesInvoiceDetails(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string SalesTypeID)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);
                DbParameter SalesTypeIDP = provider.CreateParameter("SalesTypeID", string.IsNullOrEmpty(SalesTypeID) ? null : SalesTypeID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, SalesTypeIDP };


                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceReport", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            SOI = new PDMS_SalesInvoice();
                            SOI.InvoiceID = Convert.ToInt64(dr["SaleOrderInvoiceID"]);
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            SOI.GSTNo = Convert.ToString(dr["GSTNO"]);
                            SOI.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            SOI.InvoiceDate = DBNull.Value == dr["InvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["InvoiceDate"]);
                            SOI.Status = Convert.ToString(dr["SaleOrderInvoiceStatus"]);
                            SOI.Location = Convert.ToString(dr["Location"]);
                            SOI.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            SOI.ContactNumber = Convert.ToString(dr["ContactNumber"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();

                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                HSN = Convert.ToString(dr["HSNCode"]),
                                MaterialType = Convert.ToString(dr["MaterialType"]),
                                Product = Convert.ToString(dr["Product"]),
                                ProductGroup = Convert.ToString(dr["ProductGroup"])
                            };
                            SOI.InvoiceItem.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["UnitBasicPrice"]));
                            SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["Qty"]));
                            SOI.InvoiceItem.Value = SOI.InvoiceItem.UnitBasicPrice * SOI.InvoiceItem.Qty;

                            SOI.InvoiceItem.Discount = Convert.ToDecimal(dr["Discount"]);

                            SOI.InvoiceItem.DiscountedPrice = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;

                            SOI.InvoiceItem.FreightAmount = DBNull.Value == dr["FreightAmount"] ? 0 : Convert.ToDecimal(dr["FreightAmount"]);

                            SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Discount + SOI.InvoiceItem.Value + SOI.InvoiceItem.FreightAmount;
                            SOI.InvoiceItem.SGST = DBNull.Value == dr["SGST"] ? (decimal?)null : Convert.ToDecimal(dr["SGST"]);
                            SOI.InvoiceItem.SGSTAmt = Convert.ToDecimal(dr["SGSTAmt"]);
                            SOI.InvoiceItem.CGST = DBNull.Value == dr["CGST"] ? (decimal?)null : Convert.ToDecimal(dr["CGST"]);
                            SOI.InvoiceItem.CGSTAmt = Convert.ToDecimal(dr["CGSTAmt"]);

                            SOI.InvoiceItem.IGST = DBNull.Value == dr["IGST"] ? (decimal?)null : Convert.ToDecimal(dr["IGST"]);
                            SOI.InvoiceItem.IGSTAmt = Convert.ToDecimal(dr["IGSTAmt"]);
                            SOI.InvoiceItem.Tax = SOI.InvoiceItem.SGSTAmt + SOI.InvoiceItem.CGSTAmt + SOI.InvoiceItem.IGSTAmt;
                            SOI.InvoiceItem.TotalAmt = SOI.InvoiceItem.Tax + SOI.InvoiceItem.TaxableAmount;


                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }

        public List<PDMS_SalesOrderItems> GetSalesOrderItems(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesOrderItems> SOIs = new List<PDMS_SalesOrderItems>();
            try
            {
                //  string query   = "SELECT  * from pr_getsalesorderitems(" + filter + ")";

                string query = PQuery.GetSalesOrder + filter;

                //DataTable dt = new NpgsqlServer().ExecuteReader(query);
                DataTable dt = new BPG().OutputDataTable(query);
                PDMS_SalesOrderItems SOI = new PDMS_SalesOrderItems();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_SalesOrderItems();
                    SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["Dealer_Code"]), DealerName = Convert.ToString(dr["Dealer_Name"]) };
                    SOI.Customer = Convert.ToString(dr["p_bp_id"]);
                    SOI.CustomerName = Convert.ToString(dr["r_org_name"]);
                    SOI.GSTNo = Convert.ToString(dr["GSTNO"]);
                    SOI.SONumber = Convert.ToString(dr["p_so_Id"]);
                    SOI.SODate = Convert.ToDateTime(dr["r_order_date"]);
                    SOI.SOStatus = Convert.ToString(dr["So_status"]);
                    SOI.InvoiceNumber = Convert.ToString(dr["p_inv_id"]);
                    SOI.InvoiceDate = DBNull.Value == dr["r_inv_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_inv_date"]);

                    SOI.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]), MaterialDescription = Convert.ToString(dr["r_description"]), HSN = Convert.ToString(dr["r_hsn_id"]) };
                    //SOI.PartNumber = Convert.ToString(dr["f_material_id"]);
                    //SOI.Description = Convert.ToString(dr["r_description"]);

                    //SOI.HSNCode = Convert.ToString(dr["r_hsn_id"]);
                    SOI.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["r_unit_price"]));
                    SOI.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["r_order_qty"]));
                    // SOI.Value = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);
                    SOI.Value = SOI.UnitBasicPrice * SOI.Qty;

                    SOI.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);

                    SOI.DiscountedPrice = SOI.Value + SOI.Discount;

                    SOI.FreightAmount = DBNull.Value == dr["freight"] ? 0 : Convert.ToDecimal(dr["freight"]);

                    SOI.TaxableAmount = SOI.Discount + SOI.Value + SOI.FreightAmount;
                    SOI.SGST = DBNull.Value == dr["SGST"] ? (decimal?)null : Convert.ToDecimal(dr["SGST"]);
                    SOI.SGSTAmt = DBNull.Value == dr["SGSTAmt"] ? 0 : Convert.ToDecimal(dr["SGSTAmt"]);
                    SOI.CGST = DBNull.Value == dr["CGST"] ? (decimal?)null : Convert.ToDecimal(dr["CGST"]);
                    SOI.CGSTAmt = DBNull.Value == dr["CGSTAmt"] ? 0 : Convert.ToDecimal(dr["CGSTAmt"]);

                    SOI.IGST = DBNull.Value == dr["IGST"] ? (decimal?)null : Convert.ToDecimal(dr["IGST"]);
                    SOI.IGSTAmt = DBNull.Value == dr["IGSTAmt"] ? 0 : Convert.ToDecimal(dr["IGSTAmt"]);
                    SOI.Tax = SOI.SGSTAmt + SOI.CGSTAmt + SOI.IGSTAmt;
                    SOI.TotalAmt = SOI.Tax + SOI.TaxableAmount;
                    SOI.MatType = Convert.ToString(dr["f_Mat_Type"]);
                    SOI.Division = Convert.ToString(dr["f_Division"]);
                    SOI.Location = Convert.ToString(dr["f_location"]);
                    SOI.ContactPerson = Convert.ToString(dr["r_contact_prsn"]);
                    SOI.ContactNumber = Convert.ToString(dr["r_contact_no"]);
                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesOrder1> GetSalesOrderPerfomance(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesOrder1> SOIs = new List<PDMS_SalesOrder1>();
            try
            {
                string query = "select  Qt.s_tenant_id as Dealer_Code  ,t.description  as Dealer_Name ,Qt.f_location  as location  ,Qt.s_status as  Quotation_Status  ,so.s_status as  Sales_Order_Status"
 + "  ,bp.p_bp_id as Customer_Code  ,bp.r_org_name as Customer_Name ,Qt.r_contact_prsn as contact_person ,Qt.r_contact_no  as contact_No ,Qti.f_material_id as Material_Code"
 + " ,mm.r_description  as Material_Description ,mm.r_hsn_id as hsn , Qt.p_so_Id as Quotation_Number ,Qt.r_order_date as Quotation_Date ,Qti.r_order_qty as Quotation_Quantity , so.p_so_Id as Sales_Order_Number"
 + " ,so.r_order_date as Sales_Order_Date ,soi.r_order_qty as Sales_Order_Quantity ,d.p_del_id as Delivery_Number ,d.r_del_date as Delivery_Date ,di.r_pick_qty as Delivery_Quantity ,inv.p_inv_id as Invoice_Number"
 + " ,inv.r_inv_date as Invoice_Date , invi.r_qty as Invoice_Quantity ,soi.r_unit_price as Unit_Price,  soi.r_gross_amt as Gross_Amount, soi.r_discount_amt as discount_Amount "
+ " from dssor_sales_order_hdr Qt "
+ " inner join dssor_sales_order_item Qti on Qti.p_so_Id = Qt.p_so_Id   "
+ " inner join m_tenant t on t.tenantid = Qt.s_tenant_id"
+ "  inner join doohr_bp bp on bp.p_bp_id = Qt.f_customer_id  and bp.s_tenant_id <> 20   and bp.s_tenant_id = Qt.s_tenant_id"

+ "  left join dssor_sales_order_hdr so on so.r_ext_id = Qt.p_so_Id"
+ "  left join dssor_sales_order_item soi on so.p_so_Id = soi.p_so_Id and  soi.f_material_id =  Qti.f_material_id  "

 + " left join dsder_delv_item di on di.f_so_id = soi.p_so_Id  and di.f_material_id = soi.f_material_id  "
 + " left join dsder_delv_hdr d on d.p_del_id = di.p_del_id"

 + " left Join dsinr_inv_hdr inv on inv.f_so_id = so.p_so_Id "
 + " left Join dsinr_inv_item invi on invi.k_id = inv.p_id and invi.f_material_id  = di.f_material_id"
 + " left join af_m_materials mm on mm.p_material = Qti.f_material_id where Qt.s_object_type = 208 " + filter + " order by inv.f_customer_id, so.p_so_Id desc";

                //  string query ="SELECT  * from pr_get_sales_order_perfomance(" + filter + ")";
                // DataTable dt = new NpgsqlServer().ExecuteReader(query);
                DataTable dt = new BPG().OutputDataTable(query);
                PDMS_SalesOrder1 SO = new PDMS_SalesOrder1();
                PDMS_SalesOrderItems1 SOi = null;
                foreach (DataRow dr in dt.Rows)
                {

                    SO = new PDMS_SalesOrder1();
                    SOIs.Add(SO);
                    SO.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["Dealer_Code"]), DealerName = Convert.ToString(dr["Dealer_Name"]) };
                    SO.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["Customer_Code"]), CustomerName = Convert.ToString(dr["Customer_Name"]) };
                    SO.Location = Convert.ToString(dr["Location"]);
                    SO.ContactPerson = Convert.ToString(dr["Contact_Person"]);
                    SO.ContactNumber = Convert.ToString(dr["Contact_Number"]);


                    SO.QuotationNumber = Convert.ToString(dr["Quotation_Number"]);
                    SO.QuotationDate = Convert.ToDateTime(dr["Quotation_Date"]);
                    SO.QuotationStatus = Convert.ToString(dr["Quotation_Status"]);

                    SO.SalesOrderNumber = Convert.ToString(dr["Sales_Order_Number"]);
                    SO.SalesOrderDate = DBNull.Value == dr["Sales_Order_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Sales_Order_Date"]);
                    SO.SalesOrderStatus = Convert.ToString(dr["Sales_Order_Status"]);

                    SO.DeliveryNumber = Convert.ToString(dr["Delivery_Number"]);
                    SO.DeliveryDate = DBNull.Value == dr["Delivery_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Delivery_Date"]);

                    SO.InvoiceNumber = Convert.ToString(dr["Invoice_Number"]);
                    SO.InvoiceDate = DBNull.Value == dr["Invoice_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Invoice_Date"]);

                    SO.DeliveryMinusSalesOrderDays = DBNull.Value == dr["Delivery_Date"] ? (int?)null : (int)(((DateTime)SO.DeliveryDate - (DateTime)SO.SalesOrderDate).TotalDays);
                    SO.InvoiceMinusSalesOrderDays = DBNull.Value == dr["Invoice_Date"] ? (int?)null : (int)(((DateTime)SO.InvoiceDate - (DateTime)SO.SalesOrderDate).TotalDays);

                    SO.SalesOrderItems = new PDMS_SalesOrderItems1();

                    SOi = new PDMS_SalesOrderItems1();
                    SO.SalesOrderItems = SOi;

                    SOi.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["Material_Code"]),
                        MaterialDescription = Convert.ToString(dr["Material_Description"]),
                        HSN = Convert.ToString(dr["hsn"]),
                        //  MaterialType =
                    };


                    SOi.QuotationQuantity = DBNull.Value == dr["Quotation_Quantity"] ? 0 : Convert.ToDecimal(dr["Quotation_Quantity"]);
                    SOi.SalesOrderQuantity = DBNull.Value == dr["Sales_Order_Quantity"] ? 0 : Convert.ToDecimal(dr["Sales_Order_Quantity"]);
                    SOi.DeliveryQuantity = DBNull.Value == dr["Delivery_Quantity"] ? 0 : Convert.ToDecimal(dr["Delivery_Quantity"]);
                    SOi.InvoiceQuantity = DBNull.Value == dr["Invoice_Quantity"] ? 0 : Convert.ToDecimal(dr["Invoice_Quantity"]);

                    SOi.QuotationMinusInvoiceQuantity = SOi.QuotationQuantity - SOi.InvoiceQuantity;
                    SOi.SalesOrderMinusDeliveryQuantity = SOi.SalesOrderQuantity - SOi.DeliveryQuantity;
                    SOi.SalesOrderMinusInvoiceQuantity = SOi.SalesOrderQuantity - SOi.InvoiceQuantity;



                    //public decimal? DeliveryMinusSalesOrderDays { get; set; }
                    //public decimal? InvoiceMinusSalesOrderDays { get; set; }


                    // SOI.Basic = DBNull.Value == dr["ZPRP"] ? 0 : Convert.ToDecimal(dr["ZPRP"]);

                    //  SOI.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    //  SOI.BasicAfterDisc = SOI.Basic - SOI.Discount;
                    //   SOI.Tax = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    //  SOI.FreightInsurance = DBNull.Value == dr["ZFRH"] ? 0 : Convert.ToDecimal(dr["ZFRH"]);// + (DBNull.Value == dr["ZINS"] ? 0 : Convert.ToDecimal(dr["ZINS"]));
                    //   SOI.TotalAmt = SOI.BasicAfterDisc + SOI.Tax + SOI.FreightInsurance;

                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderPerfomance", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesOrder1> GetSalesOrderPerfomanceParts(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesOrder1> SOIs = new List<PDMS_SalesOrder1>();
            try
            {

                string query = " select so.s_tenant_id as Dealer_Code,t.description  as Dealer_Name,so.f_location  as location,so.s_status as  Sales_Order_Status"
  + " ,bp.p_bp_id as Customer_Code,bp.r_org_name as Customer_Name,so.r_contact_prsn as contact_person,so.r_contact_no  as Contact_Number,soi.f_material_id as Material_Code"
  + " ,mm.r_description  as Material_Description,mm.r_hsn_id as hsn, so.p_so_Id as Sales_Order_Number"
 + " ,so.r_order_date as Sales_Order_Date,soi.r_order_qty as Sales_Order_Quantity,d.p_del_id as Delivery_Number,d.r_del_date as Delivery_Date,di.r_pick_qty as Delivery_Quantity,inv.p_inv_id as Invoice_Number"
+ " ,inv.r_inv_date as Invoice_Date, invi.r_qty as Invoice_Quantity,soi.r_unit_price as Unit_Price,soi.r_gross_amt as Gross_Amount,soi.r_discount_amt as discount_Amount"

+ " from   dssor_sales_order_hdr so "
+ "  inner join dssor_sales_order_item soi on so.p_so_Id = soi.p_so_Id  and so.s_object_type =101 and so.f_division = 'SP'   and so.r_ext_id is null "
+ " inner join m_tenant t on t.tenantid = so.s_tenant_id "
+ " inner join doohr_bp bp on bp.p_bp_id = so.f_customer_id  and bp.s_tenant_id <> 20   and bp.s_tenant_id = so.s_tenant_id "
+ " left join dsder_delv_item di on di.f_so_id = soi.p_so_Id and di.f_material_id = soi.f_material_id   "
+ " left join dsder_delv_hdr d on d.p_del_id = di.p_del_id "
+ " left Join dsinr_inv_hdr inv on inv.f_so_id = so.p_so_Id  "
+ " left Join dsinr_inv_item invi on invi.k_id = inv.p_id and invi.f_material_id  = di.f_material_id   "
+ " left join af_m_materials mm on mm.p_material = soi.f_material_id  where 1 = 1  " + filter;

                //+ " from dssor_sales_order_hdr Qt"
                //+ " inner join dssor_sales_order_item Qti on Qti.p_so_Id = Qt.p_so_Id   "
                //+ " inner join m_tenant t on t.tenantid = Qt.s_tenant_id "
                //+ " inner join doohr_bp bp on bp.p_bp_id = Qt.f_customer_id  and bp.s_tenant_id <> 20   and bp.s_tenant_id = Qt.s_tenant_id "
                //+ " left join dssor_sales_order_hdr so on so.r_ext_id = Qt.p_so_Id "
                //+ " left join dssor_sales_order_item soi on so.p_so_Id = soi.p_so_Id and  soi.f_material_id =  Qti.f_material_id  "
                //+ " left join dsder_delv_item di on di.f_so_id = soi.p_so_Id  and di.f_material_id = soi.f_material_id   "
                //+ " left join dsder_delv_hdr d on d.p_del_id = di.p_del_id "
                //+ " left Join dsinr_inv_hdr inv on inv.f_so_id = so.p_so_Id  "
                //+ " left Join dsinr_inv_item invi on invi.k_id = inv.p_id and invi.f_material_id  = di.f_material_id    "
                //+ " left join af_m_materials mm on mm.p_material = Qti.f_material_id where Qt.s_object_type = 208  " + filter;

                //   string query = "SELECT  * from pr_get_sales_order_perfomance_parts(" + filter + ")";

                //  DataTable dt = new NpgsqlServer().ExecuteReader(query);
                DataTable dt = new BPG().OutputDataTable(query);
                PDMS_SalesOrder1 SO = new PDMS_SalesOrder1();
                PDMS_SalesOrderItems1 SOi = null;
                foreach (DataRow dr in dt.Rows)
                {

                    SO = new PDMS_SalesOrder1();
                    SOIs.Add(SO);
                    SO.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["Dealer_Code"]), DealerName = Convert.ToString(dr["Dealer_Name"]) };
                    SO.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["Customer_Code"]), CustomerName = Convert.ToString(dr["Customer_Name"]) };
                    SO.Location = Convert.ToString(dr["Location"]);
                    SO.ContactPerson = Convert.ToString(dr["Contact_Person"]);
                    SO.ContactNumber = Convert.ToString(dr["Contact_Number"]);


                    //  SO.QuotationNumber = Convert.ToString(dr["Quotation_Number"]);
                    //  SO.QuotationDate = Convert.ToDateTime(dr["Quotation_Date"]);
                    //  SO.QuotationStatus = Convert.ToString(dr["Quotation_Status"]);

                    SO.SalesOrderNumber = Convert.ToString(dr["Sales_Order_Number"]);
                    SO.SalesOrderDate = DBNull.Value == dr["Sales_Order_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Sales_Order_Date"]);
                    SO.SalesOrderStatus = Convert.ToString(dr["Sales_Order_Status"]);

                    SO.DeliveryNumber = Convert.ToString(dr["Delivery_Number"]);
                    SO.DeliveryDate = DBNull.Value == dr["Delivery_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Delivery_Date"]);

                    SO.InvoiceNumber = Convert.ToString(dr["Invoice_Number"]);
                    SO.InvoiceDate = DBNull.Value == dr["Invoice_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Invoice_Date"]);

                    SO.DeliveryMinusSalesOrderDays = DBNull.Value == dr["Delivery_Date"] ? (int?)null : (int)(((DateTime)SO.DeliveryDate - (DateTime)SO.SalesOrderDate).TotalDays);
                    SO.InvoiceMinusSalesOrderDays = DBNull.Value == dr["Invoice_Date"] ? (int?)null : (int)(((DateTime)SO.InvoiceDate - (DateTime)SO.SalesOrderDate).TotalDays);

                    SO.SalesOrderItems = new PDMS_SalesOrderItems1();

                    SOi = new PDMS_SalesOrderItems1();
                    SO.SalesOrderItems = SOi;

                    SOi.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["Material_Code"]),
                        MaterialDescription = Convert.ToString(dr["Material_Description"]),
                        HSN = Convert.ToString(dr["hsn"]),
                        //  MaterialType =
                    };


                    //  SOi.QuotationQuantity = DBNull.Value == dr["Quotation_Quantity"] ? 0 : Convert.ToDecimal(dr["Quotation_Quantity"]);
                    SOi.SalesOrderQuantity = DBNull.Value == dr["Sales_Order_Quantity"] ? 0 : Convert.ToDecimal(dr["Sales_Order_Quantity"]);
                    SOi.DeliveryQuantity = DBNull.Value == dr["Delivery_Quantity"] ? 0 : Convert.ToDecimal(dr["Delivery_Quantity"]);
                    SOi.InvoiceQuantity = DBNull.Value == dr["Invoice_Quantity"] ? 0 : Convert.ToDecimal(dr["Invoice_Quantity"]);

                    //    SOi.QuotationMinusInvoiceQuantity = SOi.QuotationQuantity - SOi.InvoiceQuantity;
                    SOi.SalesOrderMinusDeliveryQuantity = SOi.SalesOrderQuantity - SOi.DeliveryQuantity;
                    SOi.SalesOrderMinusInvoiceQuantity = SOi.SalesOrderQuantity - SOi.InvoiceQuantity;



                    //public decimal? DeliveryMinusSalesOrderDays { get; set; }
                    //public decimal? InvoiceMinusSalesOrderDays { get; set; }


                    // SOI.Basic = DBNull.Value == dr["ZPRP"] ? 0 : Convert.ToDecimal(dr["ZPRP"]);

                    //  SOI.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    //  SOI.BasicAfterDisc = SOI.Basic - SOI.Discount;
                    //   SOI.Tax = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    //  SOI.FreightInsurance = DBNull.Value == dr["ZFRH"] ? 0 : Convert.ToDecimal(dr["ZFRH"]);// + (DBNull.Value == dr["ZINS"] ? 0 : Convert.ToDecimal(dr["ZINS"]));
                    //   SOI.TotalAmt = SOI.BasicAfterDisc + SOI.Tax + SOI.FreightInsurance;

                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderPerfomance", ex);
                throw ex;
            }
            return SOIs;
        }


        public List<PDMS_SalesInvoice> GetSalesInvoiceDealerAndMaterialWise(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                //  string query   = "SELECT  * from pr_getsalesorderitems(" + filter + ")";

                string query = PQuery.GetSalesInvoiceDealerAndMaterialWise.Replace("@@Filter", filter);

                //   DataTable dt = new NpgsqlServer().ExecuteReader(query);
                DataTable dt = new BPG().OutputDataTable(query);
                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_SalesInvoice();
                    i = i + 1;
                    SOI.InvoiceID = i;
                    SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["Dealer"]), DealerName = Convert.ToString(dr["Dealer_Name"]) };
                    SOI.HeaderCount = Convert.ToInt64(dr["Header_count"]);
                    SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                    SOI.InvoiceItem.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["material"]),
                        MaterialDescription = Convert.ToString(dr["material_description"])
                    };
                    SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["qty"]));
                    SOI.InvoiceItem.NetAmount = DBNull.Value == dr["net_amt"] ? 0 : Convert.ToDecimal(dr["net_amt"]);
                    SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["gross_amt"] ? 0 : Convert.ToDecimal(dr["gross_amt"]);
                    SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["item_count"]);

                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSalesInvoiceDealerCustomerAndMaterialWise(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                //  string query   = "SELECT  * from pr_getsalesorderitems(" + filter + ")";

                string query = PQuery.GetSalesInvoiceDealerCustomerAndMaterialWise.Replace("@@Filter", filter);

                // DataTable dt = new NpgsqlServer().ExecuteReader(query);
                DataTable dt = new BPG().OutputDataTable(query);
                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {

                    SOI = new PDMS_SalesInvoice();
                    i = i + 1;
                    SOI.InvoiceID = i;
                    SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };

                    SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                    SOI.HeaderCount = Convert.ToInt64(dr["Header_count"]);
                    SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                    SOI.InvoiceItem.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["material"]),
                        MaterialDescription = Convert.ToString(dr["material_description"])
                    };
                    SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["qty"]));
                    SOI.InvoiceItem.NetAmount = DBNull.Value == dr["net_amt"] ? 0 : Convert.ToDecimal(dr["net_amt"]);
                    SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["gross_amt"] ? 0 : Convert.ToDecimal(dr["gross_amt"]);
                    SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["item_count"]);

                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        //    public string dssor_sales_order_hdr = " insert into dssor_sales_order_hdr ( s_establishment,p_so_id,s_tenant_id,f_customer_id,f_location,f_currency,f_bill_to,s_modified_by "
        //             + ",r_insurance_p,r_discount_amt_additional,s_status,r_tax_amt,s_created_on,r_net_amt,f_ship_to"
        //             + ",r_contact_no,r_gross_amt,r_contact_prsn,r_discount_amt,s_created_by,s_modified_on,f_office,f_order_type,s_object_type,r_remarks,r_exp_del_date"
        //             + ",r_frieght_p,r_order_date,channel,f_division,r_auto,r_ref_obj_name,r_ref_obj_type,r_price_grp,r_model,r_model_no,s_last_request_index,r_insurance_amt,r_packing_chrgs,r_freight_amt ) values ";
        //    public string dssor_sales_order_item = "insert into dssor_sales_order_item (s_establishment,p_so_item, p_so_id,s_tenant_id,f_location,s_modified_by,f_uom,r_tax_amt,f_division"
        //+ ",s_status,f_office,r_exp_del_date,f_material_id,s_last_request_index,r_order_qty,r_add_discount_amt"
        //+ ",s_created_on,r_net_amt,d_material_desc,r_resvered_qty,r_gross_amt,r_cancel_qty"
        //+ ",r_shiped_qty,r_discount_amt,s_created_by,r_unit_price,r_pending_qty,s_modified_on"
        //+ ",s_object_type,r_approved_qty,s_channel) values ";
        //    public string dssor_sales_order_cond = "insert into dssor_sales_order_cond (s_establishment,p_so_item,p_so_id,s_tenant_id,p_condition_type,f_currency,"
        //   + "r_cond_amt,r_order_qty,r_pric_date,s_created_by,s_created_on,d_cond_desc,r_cond_cls,f_percentage,channel) values ";
        public List<PAjaxOneStatus> GetSaleOrderStatus(int? SaleOrderStatusID, string SaleOrderStatus)
        {
            string endPoint = "SaleOrder/GetSaleOrderStatus?SaleOrderStatusID=" + SaleOrderStatusID + "&SaleOrderStatus=" + SaleOrderStatus;
            return JsonConvert.DeserializeObject<List<PAjaxOneStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetSaleOrderHeader(long? SaleOrderID, string DateFrom, string DateTo, string QuotationNumber, string SaleOrderNumber, string EquipmentSerialNo, int? DealerID, int? OfficeCodeID, int? DivisionID, string CustomerCode, int? SaleOrderStatusID, int? SaleOrderTypeID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "SaleOrder/GetSaleOrderHeader?SaleOrderID=" + SaleOrderID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo
                + "&QuotationNumber=" + QuotationNumber + "&SaleOrderNumber=" + SaleOrderNumber + "&EquipmentSerialNo=" + EquipmentSerialNo + "&DealerID=" + DealerID + "&OfficeCodeID=" + OfficeCodeID + "&DivisionID=" + DivisionID
                + "&CustomerCode=" + CustomerCode + "&SaleOrderStatusID=" + SaleOrderStatusID + "&SaleOrderTypeID=" + SaleOrderTypeID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PSaleOrder GetSaleOrderByID(long SaleOrderID)
        {
            string endPoint = "SaleOrder/SaleOrderByID?SaleOrderID=" + SaleOrderID;
            return JsonConvert.DeserializeObject<PSaleOrder>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetSaleOrderReport(long? SaleOrderID, string DateFrom, string DateTo, string QuotationNumber, string SaleOrderNumber, string EquipmentSerialNo, int? DealerID, int? OfficeCodeID, int? DivisionID, string CustomerCode, int? SaleOrderStatusID, int? SaleOrderTypeID, int WithDetails)
        {
            string endPoint = "SaleOrder/GetSaleOrderReport?SaleOrderID=" + SaleOrderID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&QuotationNumber=" + QuotationNumber + "&SaleOrderNumber=" + SaleOrderNumber + "&DealerID=" + DealerID + "&OfficeCodeID=" + OfficeCodeID + "&DivisionID=" + DivisionID
                + "&CustomerCode=" + CustomerCode + "&SaleOrderStatusID=" + SaleOrderStatusID + "&SaleOrderTypeID=" + SaleOrderTypeID + "&WithDetails=" + WithDetails;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public List<PSaleOrderType> GetSaleOrderType(int? SaleOrderTypeID, string SaleOrderType)
        {
            string endPoint = "SaleOrder/GetSaleOrderType?SaleOrderTypeID=" + SaleOrderTypeID + "&SaleOrderType=" + SaleOrderType;
            return JsonConvert.DeserializeObject<List<PSaleOrderType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult InsertSaleOrderDelivery(object obj)
        {
            string endPoint = "SaleOrder/InsertSaleOrderDelivery";
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut(endPoint, obj));
        }
        public PApiResult InsertSaleOrderDeliveryShipping(object obj)
        {
            string endPoint = "SaleOrder/InsertSaleOrderDeliveryShipping";
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut(endPoint, obj));
        }
        public PApiResult UpdateSaleOrderStatus(long SaleOrderID, int StatusID)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/UpdateSaleOrderStatus?SaleOrderID=" + SaleOrderID + "&StatusID=" + StatusID));

        }
        public PApiResult GetSaleOrderDeliveryHeader(long? SaleOrderDeliveryID, string DateFrom, string DateTo, string DeliveryNumber, string InvoiceNumber, string SaleOrderNumber, int? DealerID, int? OfficeCodeID, int? DivisionID, string CustomerCode, int? SaleOrderTypeID, int? DeliveryStatusID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "SaleOrder/GetSaleOrderDeliveryHeader?SaleOrderDeliveryID=" + SaleOrderDeliveryID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo
                + "&DeliveryNumber=" + DeliveryNumber + "&InvoiceNumber=" + InvoiceNumber + "&SaleOrderNumber=" + SaleOrderNumber + "&DealerID=" + DealerID + "&OfficeCodeID=" + OfficeCodeID
                + "&DivisionID=" + DivisionID + "&CustomerCode=" + CustomerCode + "&SaleOrderTypeID=" + SaleOrderTypeID + "&DeliveryStatusID=" + DeliveryStatusID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetSaleOrderDeliveryReport(long? SaleOrderDeliveryID, string DateFrom, string DateTo, string DeliveryNumber, int? DealerID, int? OfficeCodeID, int? DivisionID, string CustomerCode, int? SaleOrderTypeID, int? DeliveryStatusID, int WithDetails)
        {
            string endPoint = "SaleOrder/GetSaleOrderDeliveryReport?SaleOrderDeliveryID=" + SaleOrderDeliveryID + "&DateFrom=" + DateFrom
                + "&DateTo=" + DateTo + "&DeliveryNumber=" + DeliveryNumber + "&DealerID=" + DealerID + "&OfficeCodeID=" + OfficeCodeID + "&DivisionID=" + DivisionID
                + "&CustomerCode=" + CustomerCode + "&SaleOrderTypeID=" + SaleOrderTypeID + "&DeliveryStatusID=" + DeliveryStatusID + "&WithDetails=" + WithDetails;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PSaleOrderDelivery GetSaleOrderDeliveryByID(long SaleOrderDeliveryID)
        {
            string endPoint = "SaleOrder/SaleOrderDeliveryByID?SaleOrderDeliveryID=" + SaleOrderDeliveryID;
            return JsonConvert.DeserializeObject<PSaleOrderDelivery>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PSaleOrderItem> GetSaleOrderItemByDeliveryID(long SaleOrderDeliveryID)
        {
            string endPoint = "SaleOrder/GetSaleOrderItemByDeliveryID?SaleOrderDeliveryID=" + SaleOrderDeliveryID;
            return JsonConvert.DeserializeObject<List<PSaleOrderItem>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GenerateSaleInvoice(long SaleOrderDeliveryID)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/GenerateSaleInvoice?SaleOrderDeliveryID=" + SaleOrderDeliveryID));

        }
        public PApiResult UpdateSaleOrderDeliveryCancel(long SaleOrderDeliveryID)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/UpdateSaleOrderDeliveryCancel?SaleOrderDeliveryID=" + SaleOrderDeliveryID));

        }
        public PSaleOrderItem_Insert ReadItem(PDMS_Material m, int DealerID, int DealerOfficeID, int Qty, string CustomerCode, string DealerCode, decimal HDiscountPercentage, decimal IDiscountValue, decimal IDiscountPercentage, string TaxType)
        {
            PSaleOrderItem_Insert SoI = new PSaleOrderItem_Insert();
            SoI.MaterialID = m.MaterialID;
            SoI.MaterialCode = m.MaterialCode;
            SoI.Quantity = Qty;

            if (string.IsNullOrEmpty(m.HSN))
            {
                throw new Exception("HSN Code is not updated for this Material. Please contact Parts Admin.");
            }
            SoI.MaterialDescription = m.MaterialDescription;
            SoI.HSN = m.HSN;
            SoI.UOM = m.BaseUnit;

            PMaterial Mat = null;
            if (m.MaterialGroup != "887")
            {
                PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
                MaterialPrice.Customer = CustomerCode;
                MaterialPrice.Vendor = DealerCode;
                MaterialPrice.OrderType = "DEFAULT_SEC_AUART";
                MaterialPrice.Division = "SP";
                MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
                MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
                {
                    ItemNo = "10",
                    Material = SoI.MaterialCode,
                    Quantity = SoI.Quantity
                });
                List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
                Mat = Mats[0];
            }
            else
            {
                Mat = new PMaterial();
                Mat.Discount = 0;
                Mat.CurrentPrice = m.CurrentPrice * SoI.Quantity;
                Mat.TaxablePrice = m.CurrentPrice * SoI.Quantity;
                if (TaxType == "SGST & CGST")
                {
                    Mat.SGST = m.TaxPercentage;
                    Mat.CGST = m.TaxPercentage;
                    Mat.SGSTValue = Mat.SGST * Mat.CurrentPrice / 100;
                    Mat.CGSTValue = Mat.CGST * Mat.CurrentPrice / 100;

                    Mat.IGST = 0;
                    SoI.IGSTValue = 0;
                }
                else
                {
                    Mat.SGST = 0;
                    Mat.CGST = 0;
                    Mat.SGSTValue = 0;
                    Mat.CGSTValue = 0;

                    Mat.IGST = m.TaxPercentage * 2;
                    Mat.IGSTValue = Mat.IGST * Mat.CurrentPrice / 100;
                }
            }
            if (Mat.CurrentPrice <= 0)
            {
                throw new Exception("Please maintain the Price for Material " + SoI.MaterialCode + " in SAP.");
            }
            if (Mat.SGST <= 0 && Mat.IGST <= 0)
            {
                throw new Exception("Please maintain the Tax for Material " + SoI.MaterialCode + " in SAP.");
            }
            if (Mat.SGSTValue <= 0 && Mat.IGSTValue <= 0)
            {
                throw new Exception("GST Tax value not found this Material " + SoI.MaterialCode + " in SAP.");
            }

            SoI.PerRate = Mat.CurrentPrice / SoI.Quantity;
            SoI.Value = Mat.CurrentPrice;
            SoI.ItemDiscountValue = IDiscountValue + (SoI.Value * IDiscountPercentage / 100);
            SoI.DiscountValue = (SoI.Value * HDiscountPercentage / 100) + SoI.ItemDiscountValue;
            SoI.TaxableValue = SoI.Value - SoI.DiscountValue;

            if (TaxType == "SGST & CGST")
            {
                SoI.SGST = (Mat.SGST + Mat.CGST + Mat.IGST) / 2;
                SoI.SGSTValue = (Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue) / 2;
                SoI.CGST = (Mat.SGST + Mat.CGST + Mat.IGST) / 2;
                SoI.CGSTValue = (Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue) / 2;
                SoI.IGST = 0;
                SoI.IGSTValue = 0;
            }
            else
            {
                SoI.SGST = 0;
                SoI.SGSTValue = 0;
                SoI.CGST = 0;
                SoI.CGSTValue = 0;
                SoI.IGST = Mat.SGST + Mat.CGST + Mat.IGST;
                SoI.IGSTValue = Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue;
            }


            SoI.SGSTValue = SoI.TaxableValue * (SoI.SGST / 100);
            SoI.CGSTValue = SoI.TaxableValue * (SoI.CGST / 100);
            SoI.IGSTValue = SoI.TaxableValue * (SoI.IGST / 100);



            SoI.NetAmount = SoI.TaxableValue + SoI.SGSTValue + SoI.CGSTValue + SoI.IGSTValue;
            SoI.Tcs = Mat.Tcs;
            PDealerStock s = new BInventory().GetDealerStockCountByID(DealerID, DealerOfficeID, SoI.MaterialID);
            SoI.OnOrderQty = s.OnOrderQty;
            SoI.TransitQty = s.TransitQty;
            SoI.UnrestrictedQty = s.UnrestrictedQty;

            return SoI;
        }


        public PAttachedFile GetPartInvoiceFile(long ID)
        {
            PAttachedFile Files = null;
            try
            {
                string endPoint = "SaleOrder/DowloadSalesInvoice?FileName=" + ID;
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                Files = JsonConvert.DeserializeObject<PAttachedFile>(JsonConvert.SerializeObject(Result.Data));

                if (Files.AttachedFile == null || Files.AttachedFile.Length == 0)
                {
                    return Invoicefile(ID);
                    //  new BAPI().ApiPut("SaleOrder/UpdateSalesInvoice", Invoicefile(ID));
                    //  Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                    // Files = JsonConvert.DeserializeObject<PAttachedFile>(JsonConvert.SerializeObject(Result.Data));
                }
                return Files;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", "GetServiceInvoiceFile", ex);
                throw;
            }
        }
        private PAttachedFile Invoicefile(long ID)
        {
            try
            {
                PSaleOrderDelivery D = GetSaleOrderDeliveryByID(ID);

                //PDMS_Customer Dealer = new BDMS_Customer().getCustomerAddressFromSAP(D.SaleOrder.Dealer.DealerCode);
                PDMS_Dealer DealerN = new BDMS_Dealer().GetDealer(D.SaleOrder.Dealer.DealerID, null, null, null)[0];



                PDMS_Dealer Dealer = new BDealer().GetDealerAddress(D.SaleOrder.Dealer.DealerID)[0];

                //string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                // string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.StateN.State) ? "" : "," + Dealer.StateN.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

                PDMS_DealerOffice DealerOffice = new BDMS_Dealer().GetDealerOffice(null, D.SaleOrder.Dealer.DealerOffice.OfficeID, null)[0];

                string DealerAddress1 = (DealerOffice.Address1 + (string.IsNullOrEmpty(DealerOffice.Address2) ? "" : "," + DealerOffice.Address2) + (string.IsNullOrEmpty(DealerOffice.Address3) ? "" : "," + DealerOffice.Address3)).Trim(',', ' ');
                string DealerAddress2 = (DealerOffice.City + (string.IsNullOrEmpty(DealerOffice.State) ? "" : "," + DealerOffice.State) + (string.IsNullOrEmpty(DealerOffice.Pincode) ? "" : "-" + DealerOffice.Pincode)).Trim(',', ' ');


                PDMS_Dealer DealerBank = new BDMS_Dealer().GetDealerBankDetails(D.SaleOrder.Dealer.DealerID, null, null)[0];
                PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(D.SaleOrder.Customer.CustomerID);
                // PDMS_Customer Customer = new BDMS_Customer().getCustomerAddressFromSAP(D.SaleOrder.Customer.CustomerCode);
                string CustomerAddress = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                CustomerAddress = CustomerAddress + "," + (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

                string ShippingAddress = string.IsNullOrEmpty(D.ShippingAddress.Trim()) ? CustomerAddress : D.ShippingAddress.Trim();


                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN");
                CommissionDT.Columns.Add("Uom");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("Rate");
                CommissionDT.Columns.Add("Value", typeof(decimal));
                CommissionDT.Columns.Add("CGST");
                CommissionDT.Columns.Add("SGST");
                CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                //  decimal GrandTotal = 0;
                string StateCode = Dealer.Address.State.StateCode;
                string GST_Header = "";
                int i = 0;
                decimal CessValue = 0;

                decimal CessSubTotal = 0;
                foreach (PSaleOrderDeliveryItem item in D.SaleOrderDeliveryItems)
                {
                    i = i + 1;
                    if (item.SGST != 0)
                    {
                        GST_Header = "CGST & SGST";
                        CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Material.BaseUnit, item.Qty, item.Value, item.TaxableValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.TaxableValue + item.CGSTValue + item.SGSTValue);

                        CessValue = CessValue + item.CessValue;
                        CessSubTotal = CessSubTotal + item.TaxableValue + item.CGSTValue + item.SGSTValue + item.CessValue;
                    }
                    else
                    {
                        GST_Header = "IGST";
                        CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Material.BaseUnit, item.Qty, item.Value, item.TaxableValue, item.IGST, null, item.IGSTValue, null, item.TaxableValue + item.IGSTValue);

                        CessValue = CessValue + item.CessValue;
                        CessSubTotal = CessSubTotal + item.TaxableValue + item.IGSTValue + item.CessValue;
                    }
                }
                //if (D.Freight != 0)
                //{
                //    i = i + 1;
                //    if (GST_Header == "CGST & SGST")
                //    {
                //        decimal GSTValue = D.Freight * 9 / 100;
                //        CommissionDT.Rows.Add(i, "Freight", "Freight Charges", "998719", "LE", "", String.Format("{0:n}", D.Freight), String.Format("{0:n}", D.Freight)
                //            , String.Format("{0:n}", 9),  String.Format("{0:n}", 9), String.Format("{0:n}", GSTValue), String.Format("{0:n}", GSTValue), D.Freight+ GSTValue+ GSTValue);
                //        CessSubTotal = CessSubTotal + (D.Freight + GSTValue + GSTValue);
                //    }
                //    else
                //    {
                //        decimal GSTValue = D.Freight * 18 / 100;
                //        CommissionDT.Rows.Add(i, "Freight", "Freight Charges", "998719", "", "LE", String.Format("{0:n}", D.Freight),  String.Format("{0:n}", D.Freight)
                //            , null, String.Format("{0:n}", 18), null, String.Format("{0:n}", GSTValue), D.Freight + GSTValue);
                //        CessSubTotal = CessSubTotal + (D.Freight + GSTValue);
                //    }
                //}
                //if (D.PackingAndForward != 0)
                //{
                //    i = i + 1;
                //    if (GST_Header == "CGST & SGST")
                //    {
                //        decimal GSTValue = D.PackingAndForward * 9 / 100;
                //        CommissionDT.Rows.Add(i, "Packing", "Packing Charges", "998719", "LE", "", String.Format("{0:n}", D.PackingAndForward), String.Format("{0:n}", D.PackingAndForward)
                //            , String.Format("{0:n}", 9), String.Format("{0:n}", 9), String.Format("{0:n}", GSTValue), String.Format("{0:n}", GSTValue), D.PackingAndForward + GSTValue + GSTValue);
                //        CessSubTotal = CessSubTotal + (D.PackingAndForward + GSTValue + GSTValue);
                //    }
                //    else
                //    {
                //        decimal GSTValue = D.PackingAndForward * 18 / 100;
                //        CommissionDT.Rows.Add(i, "Packing", "Packing Charges", "998719", "", "LE", String.Format("{0:n}", D.PackingAndForward), String.Format("{0:n}", D.PackingAndForward)
                //            , null, String.Format("{0:n}", 18), null, String.Format("{0:n}", GSTValue), D.PackingAndForward + GSTValue);
                //        CessSubTotal = CessSubTotal + (D.PackingAndForward + GSTValue);
                //    } 
                //}
                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;

                ReportParameter[] P = new ReportParameter[29];
                report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/PartsInvoiceQRCode.rdlc");

                P[24] = new ReportParameter("QRCodeImg", "", false);
                P[25] = new ReportParameter("IRN", "", false);
                if ((DealerN.ServicePaidEInvoice) && (DealerN.EInvoiceDate <= D.InvoiceDate) && (Customer.GSTIN != "URD")&& (Customer.GSTIN != DealerN.GSTIN))
                {
                    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().GetSaleOrderDeliveryInvoiceESigned(ID);
                    if (EInvoiceSigned != null)
                    {
                        if (string.IsNullOrEmpty(EInvoiceSigned.SignedQRCode))
                        {
                            throw new Exception("E Invoice not generated.: " + EInvoiceSigned.Comments);
                        }
                    }
                    if (string.IsNullOrEmpty(D.IRN))
                    {
                        throw new Exception("E Invoice not generated. Please contact IT Team.");
                    }
                    if (!string.IsNullOrEmpty(D.IRN))
                    {
                        P[24] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, D.InvoiceNumber), false);
                        P[25] = new ReportParameter("IRN", "IRN : " + D.IRN, false);
                    } 
                }


                // ReportParameter[] P = null;
                //if ((DealerN.IsEInvoice) && (DealerN.EInvoiceDate <= D.InvoiceDate) && (Customer.GSTIN != "URD"))
                //{
                //    if (string.IsNullOrEmpty(D.IRN))
                //    {
                //        throw new Exception("E Invoice not generated. Please contact IT Team.");
                //    }
                //    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().GetSaleOrderDeliveryInvoiceESigned(ID);
                //    P = new ReportParameter[26];
                //    P[24] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, D.InvoiceNumber), false);
                //    P[25] = new ReportParameter("IRN", "IRN : " + D.IRN, false);
                //    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/PartsInvoiceQRCode.rdlc");
                //}
                //else
                //{
                //    P = new ReportParameter[24];
                //    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/PartsInvoice.rdlc");
                //}
                long GrandTotal = Convert.ToInt64(Math.Round(CessSubTotal + D.TCSValue));

                //   ViewState["Month"] = ddlMonth.SelectedValue;
                P[0] = new ReportParameter("DealerCode", Dealer.DealerCode, false);
                P[1] = new ReportParameter("DealerName", Dealer.DealerName, false);
                P[2] = new ReportParameter("Address1", DealerAddress1, false);
                P[3] = new ReportParameter("Address2", DealerAddress2, false);
                P[4] = new ReportParameter("Contact", "Contact", false);
                P[5] = new ReportParameter("GSTIN", Dealer.Address.GSTIN, false);
                P[6] = new ReportParameter("GST_Header", GST_Header, false);
                P[7] = new ReportParameter("GrandTotal", (GrandTotal).ToString(), false);
                P[8] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                P[9] = new ReportParameter("InvoiceNumber", D.InvoiceNumber, false);

                P[10] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                P[11] = new ReportParameter("CustomerName", Customer.CustomerName, false);
                P[12] = new ReportParameter("CustomerAddress", CustomerAddress, false);
                P[13] = new ReportParameter("ShippingAddress", ShippingAddress, false);
                P[14] = new ReportParameter("CustomerMail", Customer.Email, false);
                P[15] = new ReportParameter("CustomerStateCode", Customer.State.StateCode, false);
                P[16] = new ReportParameter("CustomerGST", Customer.GSTIN, false);
                P[17] = new ReportParameter("KindAttn", D.SaleOrder.Attn, false);
                P[18] = new ReportParameter("Ref", D.SaleOrder.RefNumber, false);
                P[19] = new ReportParameter("RefDate", Convert.ToString(D.SaleOrder.RefDate), false);
                P[20] = new ReportParameter("BankDetails", "Our Bank details are : A/C No " + DealerBank.DealerBank.AcNumber + ", Bank : " + DealerBank.DealerBank.BankName + ", Branch : " + DealerBank.DealerBank.Branch + ", IFSC Code : " + DealerBank.DealerBank.IfscCode, false);
                P[21] = new ReportParameter("InvDate", Convert.ToString(D.InvoiceDate), false);
                P[22] = new ReportParameter("CessValue", Convert.ToString(CessValue), false);
                P[23] = new ReportParameter("CessSubTotal", Convert.ToString(CessSubTotal), false);
                P[26] = new ReportParameter("Remarks", "Remarks : " + D.Remarks, false);
                P[27] = new ReportParameter("Tcs", Convert.ToString(D.TCSTax), false);
                P[28] = new ReportParameter("TcsValue", Convert.ToString(D.TCSValue), false);
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);
                report.SetParameters(P);
                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                PAttachedFile InvF = new PAttachedFile();

                InvF.FileType = mimeType;
                InvF.AttachedFile = mybytes;
                InvF.AttachedFileID = 0;
                InvF.FileName = Convert.ToString(ID);
                if ((Customer.GSTIN == "URD") || !string.IsNullOrEmpty(D.IRN))
                {
                    new BAPI().ApiPut("SaleOrder/UpdateSalesInvoice", InvF);
                }
                return InvF;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public PApiResult GetSaleOrderInvoiceReport(int? DealerID, int? OfficeCodeID, string CustomerCode, string InvoiceNumber, string InvoiceDateFrom, string InvoiceDateTo, string DeliveryNumber, string SaleOrderNumber, int? SaleOrderTypeID, int? DeliveryStatusID, int? DivisionID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "SaleOrder/GetSaleOrderInvoiceReport?DealerID=" + DealerID + "&OfficeCodeID=" + OfficeCodeID + "&CustomerCode=" + CustomerCode + "&InvoiceNumber=" + InvoiceNumber + "&InvoiceDateFrom=" + InvoiceDateFrom + "&InvoiceDateTo=" + InvoiceDateTo
                + "&DeliveryNumber=" + DeliveryNumber + "&SaleOrderNumber=" + SaleOrderNumber + "&SaleOrderTypeID=" + SaleOrderTypeID + "&DeliveryStatusID=" + DeliveryStatusID + "&DivisionID=" + DivisionID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetSaleOrderInvoiceReportByExcelDownload(int? DealerID, int? OfficeCodeID, string CustomerCode, string InvoiceNumber, string InvoiceDateFrom, string InvoiceDateTo, string DeliveryNumber, string SaleOrderNumber, int? SaleOrderTypeID, int? DeliveryStatusID, int? DivisionID)
        {
            string endPoint = "SaleOrder/GetSaleOrderInvoiceReportByExcelDownload?DealerID=" + DealerID + "&OfficeCodeID=" + OfficeCodeID + "&CustomerCode=" + CustomerCode + "&InvoiceNumber=" + InvoiceNumber + "&InvoiceDateFrom=" + InvoiceDateFrom + "&InvoiceDateTo=" + InvoiceDateTo
                + "&DeliveryNumber=" + DeliveryNumber + "&SaleOrderNumber=" + SaleOrderNumber + "&SaleOrderTypeID=" + SaleOrderTypeID + "&DeliveryStatusID=" + DeliveryStatusID + "&DivisionID=" + DivisionID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetOOFCustomerReport(int? DealerID, string SaleOrderDate, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "SaleOrder/GetOOFCustomerReport?DealerID=" + DealerID + "&SaleOrderDate=" + SaleOrderDate
                + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public string ValidationCustomerGST(long CustomerId,int DealerID,string TaxType)
        {
            string Message = "";
            PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(CustomerId);
            PDMS_Dealer Dealer = new BDMS_Dealer().GetDealer(DealerID, null, null, null)[0];
            if (Customer.GSTIN != "URD")
            {
                if (TaxType == "IGST")
                {
                    if (Customer.State.StateID == Dealer.StateN.StateID)
                    {
                        return "Please check the Tax Type w.r.t to Customer.";
                    }
                }
                else
                {
                    if (Customer.State.StateID != Dealer.StateN.StateID)
                    {
                        return "Please check the Tax Type w.r.t to Customer.";
                    }
                }
            }
            return Message;
        }

        public Byte[] SalesMachineInvoiceRdlc(PSaleOrderDelivery SaleOrderDeliveryByID, out string mimeType)
        {
            var CC = CultureInfo.CurrentCulture;
            Random r = new Random();
            string extension;
            string encoding;
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;

            PDMS_Dealer Dealer = new BDealer().GetDealerAddress(SaleOrderDeliveryByID.SaleOrder.Dealer.DealerID)[0];

            PDMS_DealerOffice DealerOffice = new BDMS_Dealer().GetDealerOffice(null, SaleOrderDeliveryByID.SaleOrder.Dealer.DealerOffice.OfficeID, null)[0];

            string DealerAddress1 = (DealerOffice.Address1 + (string.IsNullOrEmpty(DealerOffice.Address2) ? "" : "," + DealerOffice.Address2) + (string.IsNullOrEmpty(DealerOffice.Address3) ? "" : "," + DealerOffice.Address3)).Trim(',', ' ');
            string DealerAddress2 = (DealerOffice.City + (string.IsNullOrEmpty(DealerOffice.State) ? "" : "," + DealerOffice.State) + (string.IsNullOrEmpty(DealerOffice.Pincode) ? "" : "-" + DealerOffice.Pincode)).Trim(',', ' ');

            PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(SaleOrderDeliveryByID.SaleOrder.Customer.CustomerID);
            string CustomerAddress = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
            CustomerAddress = CustomerAddress + "," + (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

             string ShippingAddress = string.IsNullOrEmpty(SaleOrderDeliveryByID.ShippingAddress.Trim()) ? CustomerAddress : SaleOrderDeliveryByID.ShippingAddress.Trim();

            
            PApiResult Result = new BSalesQuotation().GetSalesQuotationBasic(null, SaleOrderDeliveryByID.SaleOrder.QuotationNumber, null, null
               , null, null, null, null, null, null, null, null, null, null, null);

            List<PSalesQuotation> QL = JsonConvert.DeserializeObject<List<PSalesQuotation>>(JsonConvert.SerializeObject(Result.Data));

            PSalesQuotation Q = new BSalesQuotation().GetSalesQuotationByID(QL[0].QuotationID);
            string KindAttention = "", Hypothecation = "", PhoneNumber = "";

            foreach (PSalesQuotationNote Note in Q.Notes)
            {
                if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.KindAttention) { KindAttention = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Hypothecation) { Hypothecation = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.PhoneNumber) { PhoneNumber = Note.Remark; }
            }
            ReportParameter[] P = new ReportParameter[43];
            P[0] = new ReportParameter("CompanyName", Dealer.DealerName.ToUpper(), false);
            P[1] = new ReportParameter("CompanyAddress1", DealerAddress1, false);
            P[2] = new ReportParameter("CompanyAddress2", DealerAddress2, false);
            P[3] = new ReportParameter("CompanyTelephoneandEmail", "T:" + Dealer.Address.Mobile + "," + Environment.NewLine + "Email:" + Dealer.Address.Email);
            P[4] = new ReportParameter("CompanyPAN", Dealer.Address.PAN, false);
            P[5] = new ReportParameter("CompanyGSTIN", Dealer.Address.GSTIN, false);
            P[6] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
            P[7] = new ReportParameter("CustomerName", Customer.CustomerName, false);
            P[8] = new ReportParameter("CustomerAddress", CustomerAddress, false);
            P[9] = new ReportParameter("CustomerAddress2", "", false);
            P[10] = new ReportParameter("Hypothecation", Hypothecation, false);
            P[11] = new ReportParameter("CustomerShipToCode", Customer.CustomerCode, false);
            P[12] = new ReportParameter("CustomerShipToName", Customer.CustomerName, false);
            P[13] = new ReportParameter("ShippingAddress", ShippingAddress, false);
            P[14] = new ReportParameter("CustomerShipToAddress2", "", false);
            P[15] = new ReportParameter("CustomerShipToPAN", Customer.PAN, false);
            P[16] = new ReportParameter("CustomerShipToGSTIN", Customer.GSTIN, false);
            P[17] = new ReportParameter("InvoiceNo", SaleOrderDeliveryByID.InvoiceNumber, false);
            P[18] = new ReportParameter("InvoiceDate", (SaleOrderDeliveryByID.InvoiceDate == null) ? "" : Convert.ToDateTime(SaleOrderDeliveryByID.InvoiceDate).ToShortDateString(), false);
            P[19] = new ReportParameter("Attn", Customer.ContactPerson, false);
            P[20] = new ReportParameter("Mobile", Customer.Mobile, false);
            P[21] = new ReportParameter("Ref", SaleOrderDeliveryByID.SaleOrder.RefNumber, false);
            P[22] = new ReportParameter("CGST_Header", "", false);
            P[23] = new ReportParameter("CGSTVal_Header", "", false);
            P[24] = new ReportParameter("SGST_Header", "", false);
            P[25] = new ReportParameter("SGSTVal_Header", "", false);
            P[26] = new ReportParameter("DateOfPreparationOfInvoice", (SaleOrderDeliveryByID.InvoiceDate == null) ? "" : Convert.ToDateTime(SaleOrderDeliveryByID.InvoiceDate).ToShortDateString(), false);
            P[27] = new ReportParameter("DateOfRemovalOfGoods", (SaleOrderDeliveryByID.InvoiceDate == null) ? "" : Convert.ToDateTime(SaleOrderDeliveryByID.InvoiceDate).ToShortDateString(), false);
            //P[28] = new ReportParameter("MannerOfTransport", SaleOrderDeliveryByID.Shipping.TransportDetails, false);
            P[28] = new ReportParameter("MannerOfTransport", "", false);
            P[29] = new ReportParameter("Destination", SaleOrderDeliveryByID.SaleOrder.Dealer.DealerOffice.OfficeName, false);
            P[30] = new ReportParameter("SubTotal", "", false);
            P[31] = new ReportParameter("TCSTotal", "", false);
            P[32] = new ReportParameter("GrandTotal", "", false);
            P[33] = new ReportParameter("GrandTotalInwords", "", false);
            P[34] = new ReportParameter("Model", Q.LeadProduct.Product.Product, false);
            P[35] = new ReportParameter("MachineSlno", SaleOrderDeliveryByID.Equipment.EquipmentSerialNo, false);
            P[36] = new ReportParameter("EngineNo", SaleOrderDeliveryByID.Equipment.EngineSerialNo, false);
            P[37] = new ReportParameter("ChassisNo", SaleOrderDeliveryByID.Equipment.ChassisSlNo, false);
            P[38] = new ReportParameter("Remarks", "", false);
            P[39] = new ReportParameter("DeliveryNo", SaleOrderDeliveryByID.DeliveryNumber, false);
            P[40] = new ReportParameter("IRNo", "", false);
            P[41] = new ReportParameter("TCSTaxPer", "", false);
            P[42] = new ReportParameter("QRCodeImg", "", false);

            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("Sn");
            dtItem.Columns.Add("PartNo");
            dtItem.Columns.Add("Description");
            dtItem.Columns.Add("HSN");
            dtItem.Columns.Add("Qty");
            dtItem.Columns.Add("UOM");
            dtItem.Columns.Add("UnitPrice");
            dtItem.Columns.Add("TotalValue");
            dtItem.Columns.Add("Discount");
            dtItem.Columns.Add("Taxable");
            dtItem.Columns.Add("CGSTPer");
            dtItem.Columns.Add("CGSTVal");
            dtItem.Columns.Add("SGSTPer");
            dtItem.Columns.Add("SGSTVal");

            int sno = 0;
            decimal SubTotal = 0, GrandTotal = 0;
            foreach (PSaleOrderDeliveryItem Item in SaleOrderDeliveryByID.SaleOrderDeliveryItems)
            {
                if (Item.IGST == 0)
                {
                    dtItem.Rows.Add(sno += 1, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Material.HSN, Item.Qty.ToString("0"), Item.Material.BaseUnit, String.Format("{0:n}", Item.Value / Item.Qty), String.Format("{0:n}", Item.Value), String.Format("{0:n}", Item.DiscountValue), String.Format("{0:n}", Item.TaxableValue), String.Format("{0:n}", Item.CGST), String.Format("{0:n}", Item.CGSTValue), String.Format("{0:n}", Item.SGST), String.Format("{0:n}", Item.SGSTValue));
                    SubTotal += (Item.TaxableValue + Item.CGSTValue + Item.SGSTValue);
                    P[22] = new ReportParameter("CGST_Header", "%", false);
                    P[23] = new ReportParameter("CGSTVal_Header", "CGST", false);
                    P[24] = new ReportParameter("SGST_Header", "%", false);
                    P[25] = new ReportParameter("SGSTVal_Header", "SGST", false);
                }
                else
                {
                    dtItem.Rows.Add(sno += 1, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Material.HSN, Item.Qty.ToString("0"), Item.Material.BaseUnit, String.Format("{0:n}", Item.Value / Item.Qty), String.Format("{0:n}", Item.Value), String.Format("{0:n}", Item.DiscountValue), String.Format("{0:n}", Item.TaxableValue), "", "", String.Format("{0:n}", Item.IGST), String.Format("{0:n}", Item.IGSTValue));
                    SubTotal += (Item.TaxableValue + Item.IGSTValue);
                    P[22] = new ReportParameter("CGST_Header", "", false);
                    P[23] = new ReportParameter("CGSTVal_Header", "", false);
                    P[24] = new ReportParameter("SGST_Header", "%", false);
                    P[25] = new ReportParameter("SGSTVal_Header", "IGST", false);
                }
            }
            GrandTotal = Math.Round(SubTotal + SaleOrderDeliveryByID.TCSValue);
            P[30] = new ReportParameter("SubTotal", String.Format("{0:n}", SubTotal), false);
            P[31] = new ReportParameter("TCSTotal", String.Format("{0:n}", SaleOrderDeliveryByID.TCSValue), false);
            P[32] = new ReportParameter("GrandTotal", String.Format("{0:n}", GrandTotal), false);
            P[33] = new ReportParameter("GrandTotalInwords", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
            P[41] = new ReportParameter("TCSTaxPer", String.Format("{0:n}", SaleOrderDeliveryByID.TCSTax), false);
            PDMS_Dealer DealerN = new BDMS_Dealer().GetDealer(SaleOrderDeliveryByID.SaleOrder.Dealer.DealerID, null, null, null)[0];
            if ((DealerN.ServicePaidEInvoice) && (DealerN.EInvoiceDate <= SaleOrderDeliveryByID.InvoiceDate) && (Customer.GSTIN != "URD"))
            {
                PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().GetSaleOrderDeliveryInvoiceESigned(SaleOrderDeliveryByID.SaleOrderDeliveryID);
                if (EInvoiceSigned != null)
                {
                    if (string.IsNullOrEmpty(EInvoiceSigned.SignedQRCode))
                    {
                        throw new Exception("E Invoice not generated.: " + EInvoiceSigned.Comments);
                    }
                }
                if (string.IsNullOrEmpty(SaleOrderDeliveryByID.IRN))
                {
                    throw new Exception("E Invoice not generated. Please contact IT Team.");
                }
                else
                {
                    P[40] = new ReportParameter("IRNo", "IRN : " + SaleOrderDeliveryByID.IRN, false);
                    P[42] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, SaleOrderDeliveryByID.InvoiceNumber), false);
                }

            }
            else
            {
                P[40] = new ReportParameter("IRNo", "", false);
                P[42] = new ReportParameter("QRCodeImg", "", false);
            }


            report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/SalesMachineInvoice.rdlc");
            report.SetParameters(P);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "SalesMachineInvoice";//This refers to the dataset name in the RDLC file  
            rds.Value = dtItem;
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF 
            return mybytes;
        }
    }
}
