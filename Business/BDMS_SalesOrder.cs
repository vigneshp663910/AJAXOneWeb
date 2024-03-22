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
        public PApiResult GetSaleOrderReport(long? SaleOrderID, string DateFrom, string DateTo, string QuotationNumber, int? DealerID, int? OfficeCodeID, int? DivisionID, string CustomerCode, int? SaleOrderStatusID, int? SaleOrderTypeID)
        {
            string endPoint = "SaleOrder/GetSaleOrderReport?SaleOrderID=" + SaleOrderID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&SaleOrderNumber=" + QuotationNumber + "&DealerID=" + DealerID + "&OfficeCodeID=" + OfficeCodeID + "&DivisionID=" + DivisionID
                + "&CustomerCode=" + CustomerCode + "&SaleOrderStatusID=" + SaleOrderStatusID + "&SaleOrderTypeID=" + SaleOrderTypeID;
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

        public PApiResult UpdateSaleOrderStatus(long SaleOrderID, int StatusID)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/UpdateSaleOrderStatus?SaleOrderID=" + SaleOrderID + "&StatusID=" + StatusID));

        }


        public PApiResult GetSaleOrderDeliveryHeader(long? SaleOrderDeliveryID, string DateFrom, string DateTo, string DeliveryNumber, string SaleOrderNumber, int? DealerID, int? OfficeCodeID, int? DivisionID, string CustomerCode, int? SaleOrderTypeID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "Sne/GetSaleOrderDeliveryHeader?SaleOrderDeliveryID=" + SaleOrderDeliveryID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo 
                + "&DeliveryNumber=" + DeliveryNumber + "&SaleOrderNumber=" + SaleOrderNumber + "&DealerID=" + DealerID + "&OfficeCodeID=" + OfficeCodeID
                + "&DivisionID=" + DivisionID + "&CustomerCode=" + CustomerCode + "&SaleOrderTypeID=" + SaleOrderTypeID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetSaleOrderDeliveryReport(long? SaleOrderDeliveryID, string DateFrom, string DateTo, string DeliveryNumber, int? DealerID, int? OfficeCodeID, int? DivisionID, string CustomerCode, int? SaleOrderTypeID)
        {
            string endPoint = "Sne/GetSaleOrderDeliveryReport?SaleOrderDeliveryID=" + SaleOrderDeliveryID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&DeliveryNumber=" + DeliveryNumber + "&DealerID=" + DealerID + "&OfficeCodeID=" + OfficeCodeID + "&DivisionID=" + DivisionID
                + "&CustomerCode=" + CustomerCode + "&SaleOrderTypeID=" + SaleOrderTypeID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PSaleOrderDelivery GetSaleOrderDeliveryByID(long SaleOrderDeliveryID)
        {
            string endPoint = "Sne/SaleOrderDeliveryByID?SaleOrderDeliveryID=" + SaleOrderDeliveryID;
            return JsonConvert.DeserializeObject<PSaleOrderDelivery>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PSaleOrderItem> GetSaleOrderItemByDeliveryID(long SaleOrderDeliveryID)
        {
            string endPoint = "Sne/GetSaleOrderItemByDeliveryID?SaleOrderDeliveryID=" + SaleOrderDeliveryID;
            return JsonConvert.DeserializeObject<List<PSaleOrderItem>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GenerateSaleInvoice(long SaleOrderDeliveryID)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/GenerateSaleInvoice?SaleOrderDeliveryID=" + SaleOrderDeliveryID));

        }

        public PSaleOrderItem_Insert ReadItem(PDMS_Material m,int Qty,string CustomerCode, string DealerCode, decimal HDiscountPercentage, decimal IDiscountValue, string TaxType)
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
                    Mat.IGSTValue = Mat.IGST * Mat.CurrentPrice  / 100;
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
            SoI.ItemDiscountValue = IDiscountValue;
            SoI.DiscountValue = (SoI.Value * HDiscountPercentage / 100)+ IDiscountValue;
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
            
            return SoI;
        }

        protected PSaleOrderItem_Insert AddMat(PDMS_Material m, int Qty, string CustomerCode, string DealerCode, decimal HDiscountPercentage, string TaxType)
        {
            PSaleOrderItem_Insert SoI = new PSaleOrderItem_Insert();
            SoI.MaterialID = m.MaterialID;
            SoI.MaterialCode = m.MaterialCode;
            SoI.Quantity = Convert.ToInt32(Qty);
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
                Mat.CurrentPrice = m.CurrentPrice;
                Mat.TaxablePrice = m.CurrentPrice * SoI.Quantity;
                if (TaxType == "SGST & CGST")
                {
                    Mat.SGST = m.TaxPercentage;
                    Mat.CGST = m.TaxPercentage;
                    SoI.SGSTValue = Mat.SGST * Mat.CurrentPrice * SoI.Quantity / 100;
                    SoI.CGSTValue = Mat.CGST * Mat.CurrentPrice * SoI.Quantity / 100;

                    Mat.IGST = 0;
                    SoI.IGSTValue = 0;
                }
                else
                {
                    Mat.SGST = 0;
                    Mat.CGST = 0;
                    SoI.SGSTValue = 0;
                    SoI.CGSTValue = 0;

                    Mat.IGST = m.TaxPercentage * 2;
                    SoI.IGSTValue = Mat.IGST * Mat.CurrentPrice * SoI.Quantity / 100;
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
              
            SoI.DiscountValue = SoI.Value * HDiscountPercentage / 100; ;
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
            return SoI;
        }
    }
}
