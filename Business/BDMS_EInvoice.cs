using DataAccess;
using Newtonsoft.Json;
using Properties;
using QRCoder; 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;

namespace Business
{
    public class BDMS_EInvoice
    {
        private IDataAccess provider;

        public BDMS_EInvoice() { provider = new ProviderFactory().GetProvider(); }
          
        public List<PDMS_PaidServiceInvoice> GetPaidServiceInvoiceForRequestEInvoice(string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID, string CustomerCode)
        {
            List<PDMS_PaidServiceInvoice> Services = new List<PDMS_PaidServiceInvoice>();
            try
            {
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);

                DbParameter DMSP = provider.CreateParameter("DMS", 2, DbType.Int32);

                DbParameter[] Params = new DbParameter[6] { InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerIDP, CustomerCodeP, DMSP };
                PDMS_PaidServiceInvoice Service = null;
                long InvoiceID = 0;
                using (DataSet DataSet = provider.Select("ZDMS_GetPaidServiceInvoiceForRequestEInvoice", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["ServiceInvoiceID"]))
                            {
                                Service = new PDMS_PaidServiceInvoice();
                                Services.Add(Service);
                                Service.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                Service.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                                Service.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);

                                Service.ICTicket = new PDMS_ICTicket();

                                Service.ICTicket.Customer = new PDMS_Customer();
                                Service.ICTicket.Customer.Mobile = Convert.ToString(dr["CustomerMobile"]);
                                Service.ICTicket.Customer.Email = Convert.ToString(dr["CustomerEmail"]);


                                
                                Service.ICTicket.Dealer = new PDMS_Dealer();
                                Service.ICTicket.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                                Service.ICTicket.Dealer.DealerName = Convert.ToString(dr["ContactName"]);
                                Service.ICTicket.Dealer.Mobile = Convert.ToString(dr["DealerMobile"]);
                                Service.ICTicket.Dealer.Email = Convert.ToString(dr["DealerEmail"]);
                                  

                                //Service.ICTicket.Customer = new PDMS_Customer();
                                //Service.ICTicket.Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                                //Service.ICTicket.Customer.CustomerName = Convert.ToString(dr["CustomerName"]);

                                Service.InvoiceDetails = new PDMS_PaidServiceInvoiceDetails();
                                Service.InvoiceDetails.SupplierGSTIN = Convert.ToString(dr["SupplierGSTIN"]);
                                Service.InvoiceDetails.Supplier_addr1 = Convert.ToString(dr["Supplier_addr1"]);
                                Service.InvoiceDetails.SupplierLocation = Convert.ToString(dr["SupplierLocation"]);
                                Service.InvoiceDetails.SupplierPincode = Convert.ToString(dr["SupplierPincode"]);
                                Service.InvoiceDetails.SupplierStateCode = Convert.ToString(dr["SupplierStateCode"]);

                                Service.InvoiceDetails.BuyerGSTIN = Convert.ToString(dr["BuyerGSTIN"]);
                                Service.InvoiceDetails.BuyerName = Convert.ToString(dr["CustomerName"]);
                                Service.InvoiceDetails.BuyerStateCode = Convert.ToString(dr["BuyerStateCode"]);
                                Service.InvoiceDetails.Buyer_addr1 = Convert.ToString(dr["Buyer_addr1"]);
                                Service.InvoiceDetails.Buyer_loc = Convert.ToString(dr["Buyer_loc"]);
                                Service.InvoiceDetails.BuyerPincode = Convert.ToString(dr["BuyerPincode"]);


                                InvoiceID = Service.PaidServiceInvoiceID;

                                Service.InvoiceItems = new List<PDMS_PaidServiceInvoiceItem>();
                            }
                            Service.InvoiceItems.Add(new PDMS_PaidServiceInvoiceItem()
                            {

                                PaidServiceInvoiceItemID = Convert.ToInt64(dr["ServiceInvoiceItemID"]),
                                Material = new PDMS_Material()
                                {
                                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                    MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                    HSN = Convert.ToString(dr["HSNCode"])
                                },
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Rate = Convert.ToDecimal(dr["TaxableValue"]) / Convert.ToInt32(dr["Qty"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToDecimal(dr["CGST"]),
                                SGST = Convert.ToDecimal(dr["SGST"]),
                                IGST = Convert.ToDecimal(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                                CessValue = dr["CessValue"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CessValue"])
                            });

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Services;
        }
        public List<PDMS_WarrantyClaimInvoice> getWarrantyClaimInvoiceForRequestEInvoice(string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID, string CustomerCode)
        {
            List<PDMS_WarrantyClaimInvoice> Ws = new List<PDMS_WarrantyClaimInvoice>();
            PDMS_WarrantyClaimInvoice W = null;

            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
            DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
            DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);

            DbParameter[] Params = new DbParameter[5] { InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerIDP, CustomerCodeP };
            try
            {
                long InvoiceID = 0;
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimInvoiceForRequestEInvoice", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["WarrantyClaimInvoiceID"]))
                            {
                                W = new PDMS_WarrantyClaimInvoice();
                                Ws.Add(W);
                                W.WarrantyClaimInvoiceID = Convert.ToInt64(dr["WarrantyClaimInvoiceID"]);
                                W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                W.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                                W.Dealer = new PDMS_Dealer()
                                {
                                    DealerCode = Convert.ToString(dr["UserName"]),
                                    DealerName = Convert.ToString(dr["ContactName"]),
                                    IsEInvoice = DBNull.Value == dr["IsEInvoice"] ? false : Convert.ToBoolean(dr["IsEInvoice"]),
                                    EInvoiceDate = DBNull.Value == dr["EInvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["EInvoiceDate"]), 
                                };
                                W.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                W.InvoiceItems = new List<PDMS_WarrantyClaimInvoiceItem>();
                                InvoiceID = W.WarrantyClaimInvoiceID;
                                W.InvoiceType = new PDMS_WarrantyInvoiceType() { InvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"]), InvoiceType = Convert.ToString(dr["InvoiceType"]) };

                                W.InvoiceDetails = new PDMS_WarrantyClaimInvoiceDetails();
                                W.InvoiceDetails.SupplierGSTIN = Convert.ToString(dr["SupplierGSTIN"]);
                                W.InvoiceDetails.Supplier_addr1 = Convert.ToString(dr["Supplier_addr1"]);
                                W.InvoiceDetails.SupplierLocation = Convert.ToString(dr["SupplierLocation"]);
                                W.InvoiceDetails.SupplierPincode = Convert.ToString(dr["SupplierPincode"]);
                                W.InvoiceDetails.SupplierStateCode = Convert.ToString(dr["SupplierStateCode"]);

                                W.InvoiceDetails.BuyerGSTIN = Convert.ToString(dr["BuyerGSTIN"]);
                                W.InvoiceDetails.BuyerName = Convert.ToString(dr["BuyerName"]);
                                W.InvoiceDetails.BuyerStateCode = Convert.ToString(dr["BuyerStateCode"]);
                                W.InvoiceDetails.Buyer_addr1 = Convert.ToString(dr["Buyer_addr1"]);
                                W.InvoiceDetails.Buyer_loc = Convert.ToString(dr["Buyer_loc"]);
                                W.InvoiceDetails.BuyerPincode = Convert.ToString(dr["BuyerPincode"]);



                                // W.InvoiceDetails = new PDMS_WarrantyClaimInvoiceDetails();
                            }
                            W.InvoiceItems.Add(new PDMS_WarrantyClaimInvoiceItem()
                            {
                                WarrantyClaimInvoiceItemID = Convert.ToInt64(dr["WarrantyClaimInvoiceItemID"]),
                                Material = Convert.ToString(dr["Material"]),
                                MaterialDesc = Convert.ToString(dr["MaterialDesc"]),
                                HSNCode = Convert.ToString(dr["HSNCode"]),
                                UOM = Convert.ToString(dr["UnitCode"]),
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Rate = Convert.ToDecimal(dr["TaxableValue"]) / Convert.ToInt32(dr["Qty"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToDecimal(dr["CGST"]),
                                SGST = Convert.ToDecimal(dr["SGST"]),
                                IGST = Convert.ToDecimal(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"])

                            });
                            W.TCSValue = dr["TCSValue"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TCSValue"]);
                            W.TempTcsMatCount = W.TempTcsMatCount + (Convert.ToString(dr["HSNCode"]) == "998719" ? 0 : 1);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }
        public List<PDMS_WarrantyClaimInvoice> getActivityInvoiceForRequestEInvoice(string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID)
        {
            List<PDMS_WarrantyClaimInvoice> Ws = new List<PDMS_WarrantyClaimInvoice>();
            PDMS_WarrantyClaimInvoice W = null;

            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
            DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
            DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

            DbParameter[] Params = new DbParameter[4] { InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerIDP };
            try
            {
                long InvoiceID = 0;
                using (DataSet EmployeeDataSet = provider.Select("YDMS_GetActivityInvoiceForRequestEInvoice_Z", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["AIH_PkHdrID"]))
                            {
                                W = new PDMS_WarrantyClaimInvoice();
                                Ws.Add(W);
                                W.WarrantyClaimInvoiceID = Convert.ToInt64(dr["AIH_PkHdrID"]);
                                W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                W.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                                W.Dealer = new PDMS_Dealer()
                                {
                                    DealerCode = Convert.ToString(dr["DealerCode"]),
                                    DealerName = Convert.ToString(dr["DealerName"]),
                                    IsEInvoice = DBNull.Value == dr["IsEInvoice"] ? false : Convert.ToBoolean(dr["IsEInvoice"]),
                                    EInvoiceDate = DBNull.Value == dr["EInvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["EInvoiceDate"]),
                                    GSTIN= Convert.ToString(dr["GSTIN"]),
                                    Address1 = Convert.ToString(dr["Address1"]),
                                    Address2 = Convert.ToString(dr["Address2"]),
                                    City = Convert.ToString(dr["City"]),
                                    Pincode = Convert.ToString(dr["Pincode"]),
                                    StateCode = Convert.ToString(dr["StateCode"]), 

                                };
                                W.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                W.InvoiceItems = new List<PDMS_WarrantyClaimInvoiceItem>();
                                InvoiceID = W.WarrantyClaimInvoiceID;
                                // W.InvoiceType = new PDMS_WarrantyInvoiceType() { InvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"]), InvoiceType = Convert.ToString(dr["InvoiceType"]) };

                                W.InvoiceDetails = new PDMS_WarrantyClaimInvoiceDetails();

                                //  PDMS_Customer Dealer = new SCustomer().getCustomerAddress(W.Dealer.DealerCode);
                                //  PDealerAddress Dealer = new PDealerAddress();

                                // string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                                // string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.StateN.State) ? "" : "," + Dealer.StateN.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');


                                W.InvoiceDetails.SupplierGSTIN = W.Dealer.GSTIN;
                                W.InvoiceDetails.Supplier_addr1 = (W.Dealer.Address1 + (string.IsNullOrEmpty(W.Dealer.Address2) ? "" : "," + W.Dealer.Address2)).Trim(',');
                                W.InvoiceDetails.SupplierLocation = W.Dealer.City;
                                W.InvoiceDetails.SupplierPincode = W.Dealer.Pincode;
                                W.InvoiceDetails.SupplierStateCode = W.Dealer.StateCode;

                                W.InvoiceDetails.BuyerGSTIN = Convert.ToString(dr["BuyerGSTIN"]);
                                W.InvoiceDetails.BuyerName = Convert.ToString(dr["BuyerName"]);
                                W.InvoiceDetails.BuyerStateCode = Convert.ToString(dr["BuyerStateCode"]);
                                W.InvoiceDetails.Buyer_addr1 = Convert.ToString(dr["Buyer_addr1"]);
                                W.InvoiceDetails.Buyer_loc = Convert.ToString(dr["Buyer_loc"]);
                                W.InvoiceDetails.BuyerPincode = Convert.ToString(dr["BuyerPincode"]);
                            }
                            W.InvoiceItems.Add(new PDMS_WarrantyClaimInvoiceItem()
                            {
                                // Material = Convert.ToString(dr["Material"]), 
                                MaterialDesc = Convert.ToString(dr["MaterialDesc"]),
                                HSNCode = Convert.ToString(dr["HSNCode"]),
                                UOM = Convert.ToString(dr["UnitCode"]),
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Rate = Convert.ToDecimal(dr["TaxableValue"]) / Convert.ToInt32(dr["Qty"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToDecimal(dr["CGST"]),
                                SGST = Convert.ToDecimal(dr["SGST"]),
                                IGST = Convert.ToDecimal(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"])
                            });
                            W.TCSValue = dr["TCSValue"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TCSValue"]);
                            W.TempTcsMatCount = W.TempTcsMatCount + (Convert.ToString(dr["HSNCode"]) == "998719" ? 0 : 1);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }
        public List<PSalesCommissionClaimInvoice> GetSalesCommissionClaimInvoiceForRequestEInvoice(long? SalesCommissionClaimInvoiceID, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID)
        {
            List<PSalesCommissionClaimInvoice> Services = new List<PSalesCommissionClaimInvoice>();
            try
            {

                DbParameter SalesCommissionClaimInvoiceIDP = provider.CreateParameter("SalesCommissionClaimInvoiceID", SalesCommissionClaimInvoiceID, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

                DbParameter[] Params = new DbParameter[5] { SalesCommissionClaimInvoiceIDP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerIDP };
                PSalesCommissionClaimInvoice Service = null;
                long InvoiceID = 0;
                using (DataSet DataSet = provider.Select("GetSalesCommissionClaimInvoiceForRequestEInvoice", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            Service = new PSalesCommissionClaimInvoice();
                            Services.Add(Service);
                            Service.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            Service.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                            Service.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);

                            Service.Dealer = new PDMS_Dealer();
                            Service.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                            Service.Dealer.DealerName = Convert.ToString(dr["ContactName"]); 
                            //Service.ICTicket.Customer = new PDMS_Customer();
                            //Service.ICTicket.Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                            //Service.ICTicket.Customer.CustomerName = Convert.ToString(dr["CustomerName"]);

                            Service.InvoiceDetails = new PSalesCommissionClaimInvoiceDetails();
                            Service.InvoiceDetails.SupplierGSTIN = Convert.ToString(dr["SupplierGSTIN"]);
                            Service.InvoiceDetails.Supplier_addr1 = Convert.ToString(dr["Supplier_addr1"]);
                            Service.InvoiceDetails.SupplierLocation = Convert.ToString(dr["SupplierLocation"]);
                            Service.InvoiceDetails.SupplierPincode = Convert.ToString(dr["SupplierPincode"]);
                            Service.InvoiceDetails.SupplierStateCode = Convert.ToString(dr["SupplierStateCode"]);

                            Service.InvoiceDetails.BuyerGSTIN = Convert.ToString(dr["BuyerGSTIN"]);
                            Service.InvoiceDetails.BuyerName = Convert.ToString(dr["CustomerName"]);
                            Service.InvoiceDetails.BuyerStateCode = Convert.ToString(dr["BuyerStateCode"]);
                            Service.InvoiceDetails.Buyer_addr1 = Convert.ToString(dr["Buyer_addr1"]);
                            Service.InvoiceDetails.Buyer_loc = Convert.ToString(dr["Buyer_loc"]);
                            Service.InvoiceDetails.BuyerPincode = Convert.ToString(dr["BuyerPincode"]);

                            Service.InvoiceItem = new PSalesCommissionClaimInvoiceItem()
                            {
                                SalesCommissionClaimInvoiceItemID = Convert.ToInt64(dr["SalesCommissionClaimInvoiceItemID"]),
                                Material = new PDMS_Material()
                                {
                                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                    MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                    HSN = Convert.ToString(dr["HSNCode"])
                                },
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Rate = Convert.ToDecimal(dr["TaxableValue"]) / Convert.ToInt32(dr["Qty"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToDecimal(dr["CGST"]),
                                SGST = Convert.ToDecimal(dr["SGST"]),
                                IGST = Convert.ToDecimal(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"])
                            };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Services;
        }
        public List<PSaleOrderDelivery> GetSaleInvoiceForRequestEInvoice(string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID, string CustomerCode)
        {
            List<PSaleOrderDelivery> Ws = new List<PSaleOrderDelivery>();
            PSaleOrderDelivery W = null;

            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
            DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
            DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);

            DbParameter[] Params = new DbParameter[5] { InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerIDP, CustomerCodeP };
            try
            {
                long InvoiceID = 0;
                using (DataSet EmployeeDataSet = provider.Select("GetSaleInvoiceForRequestEInvoice", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["SaleOrderDeliveryID"]))
                            {
                                W = new PSaleOrderDelivery();
                                Ws.Add(W);
                                W.SaleOrderDeliveryID = Convert.ToInt64(dr["SaleOrderDeliveryID"]);
                                W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                W.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                                W.SaleOrder = new PSaleOrder();
                                W.SaleOrder.TaxType = Convert.ToString(dr["TaxType"]);
                                W.SaleOrder.Dealer = new PDMS_Dealer()
                                {
                                    DealerCode = Convert.ToString(dr["UserName"]),
                                    DealerName = Convert.ToString(dr["ContactName"]),
                                    IsEInvoice = DBNull.Value == dr["IsEInvoice"] ? false : Convert.ToBoolean(dr["IsEInvoice"]),
                                    EInvoiceDate = DBNull.Value == dr["EInvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["EInvoiceDate"])
                                };
                                W.Freight = dr["Freight"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Freight"]);
                                W.PackingAndForward = dr["PackingAndForward"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PackingAndForward"]);
                                W.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                W.SaleOrderDeliveryItems = new List<PSaleOrderDeliveryItem>();
                                InvoiceID = W.SaleOrderDeliveryID; 
                                W.InvoiceDetails = new PInvoiceDetails();
                                W.InvoiceDetails.SupplierGSTIN = Convert.ToString(dr["SupplierGSTIN"]);
                                W.InvoiceDetails.Supplier_addr1 = Convert.ToString(dr["Supplier_addr1"]);
                                W.InvoiceDetails.SupplierLocation = Convert.ToString(dr["SupplierLocation"]);
                                W.InvoiceDetails.SupplierPincode = Convert.ToString(dr["SupplierPincode"]);
                                W.InvoiceDetails.SupplierStateCode = Convert.ToString(dr["SupplierStateCode"]);

                                W.InvoiceDetails.BuyerGSTIN = Convert.ToString(dr["BuyerGSTIN"]);
                                W.InvoiceDetails.BuyerName = Convert.ToString(dr["BuyerName"]);
                                W.InvoiceDetails.BuyerStateCode = Convert.ToString(dr["BuyerStateCode"]);
                                W.InvoiceDetails.Buyer_addr1 = Convert.ToString(dr["Buyer_addr1"]);
                                W.InvoiceDetails.Buyer_loc = Convert.ToString(dr["Buyer_loc"]);
                                W.InvoiceDetails.BuyerPincode = Convert.ToString(dr["BuyerPincode"]);


                                //if (W.Freight != 0)
                                //{
                                //    PSaleOrderDeliveryItem Item = new PSaleOrderDeliveryItem();
                                //    W.SaleOrderDeliveryItems.Add(Item);
                                //    Item.Material = new PDMS_Material()
                                //    {
                                //        MaterialCode = "Freight",
                                //        MaterialDescription = "Freight Charges",
                                //        HSN = "998719",
                                //        BaseUnit = "LE"
                                //    };
                                //    Item.Qty = 1;
                                //    Item.Value = W.Freight;
                                //    Item.TaxableValue = W.Freight;
                                //    if (W.SaleOrder.TaxType != "IGST")
                                //    {
                                //        Item.CGST = 9;
                                //        Item.SGST = 9;
                                //        Item.CGSTValue = W.Freight * 9 / 100;
                                //        Item.SGSTValue = W.Freight * 9 / 100;
                                //    }
                                //    else
                                //    {
                                //        Item.IGST = 18;
                                //        Item.IGSTValue = W.Freight * 18 / 100;
                                //    }
                                //}
                                //if (W.PackingAndForward != 0)
                                //{
                                //    PSaleOrderDeliveryItem Item = new PSaleOrderDeliveryItem();
                                //    W.SaleOrderDeliveryItems.Add(Item);
                                //    Item.Material = new PDMS_Material()
                                //    {
                                //        MaterialCode = "Packing",
                                //        MaterialDescription = "Packing Charges",
                                //        HSN = "998719",
                                //        BaseUnit = "LE"
                                //    };
                                //    Item.Qty = 1;
                                //    Item.Value = W.PackingAndForward;
                                //    Item.TaxableValue = W.PackingAndForward;
                                //    if (W.SaleOrder.TaxType != "IGST")
                                //    {
                                //        Item.CGST = 9;
                                //        Item.SGST = 9;
                                //        Item.CGSTValue = W.PackingAndForward * 9 / 100;
                                //        Item.SGSTValue = W.PackingAndForward * 9 / 100;
                                //    }
                                //    else
                                //    {
                                //        Item.IGST = 18;
                                //        Item.IGSTValue = W.PackingAndForward * 18 / 100;
                                //    }
                                //}
                                // W.InvoiceDetails = new PDMS_WarrantyClaimInvoiceDetails();
                            }
                            W.SaleOrderDeliveryItems.Add(new PSaleOrderDeliveryItem()
                            {
                                SaleOrderDeliveryItemID = Convert.ToInt64(dr["SaleOrderDeliveryItemID"]),
                                Material = new PDMS_Material()
                                {
                                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                    MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                    HSN  = Convert.ToString(dr["HSNCode"]),
                                    BaseUnit = Convert.ToString(dr["UnitCode"])
                                },
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Value = Convert.ToDecimal(dr["Value"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToDecimal(dr["CGST"]),
                                SGST = Convert.ToDecimal(dr["SGST"]),
                                IGST = Convert.ToDecimal(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"])

                            });

                           
                            W.TCSValue = dr["TCSValue"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TCSValue"]);
                            W.TempTcsMatCount = W.TempTcsMatCount + (Convert.ToString(dr["HSNCode"]) == "998719" ? 0 : 1);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }
        public List<PSaleOrderDelivery> GetSaleReturnCreditNoteForRequestEInvoice(string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID, string CustomerCode)
        {
            List<PSaleOrderDelivery> Ws = new List<PSaleOrderDelivery>();
            PSaleOrderDelivery W = null;

            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
            DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
            DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);

            DbParameter[] Params = new DbParameter[5] { InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerIDP, CustomerCodeP };
            try
            {
                long InvoiceID = 0;
                using (DataSet EmployeeDataSet = provider.Select("GetSaleReturnCreditNoteForRequestEInvoice", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["SaleOrderReturnID"]))
                            {
                                W = new PSaleOrderDelivery();
                                Ws.Add(W);
                                W.SaleOrderDeliveryID = Convert.ToInt64(dr["SaleOrderReturnID"]);
                                W.InvoiceNumber = Convert.ToString(dr["CreditNoteNumber"]);
                                W.InvoiceDate = Convert.ToDateTime(dr["CreditNoteDate"]);
                                W.SaleOrder = new PSaleOrder();
                                W.SaleOrder.Dealer = new PDMS_Dealer()
                                {
                                    DealerCode = Convert.ToString(dr["UserName"]),
                                    DealerName = Convert.ToString(dr["ContactName"]),
                                    IsEInvoice = DBNull.Value == dr["IsEInvoice"] ? false : Convert.ToBoolean(dr["IsEInvoice"]),
                                    EInvoiceDate = DBNull.Value == dr["EInvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["EInvoiceDate"]), 
                                };
                                W.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                W.SaleOrderDeliveryItems = new List<PSaleOrderDeliveryItem>();
                                InvoiceID = W.SaleOrderDeliveryID;
                                //W.InvoiceType = new PDMS_WarrantyInvoiceType() { InvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"]), InvoiceType = Convert.ToString(dr["InvoiceType"]) };

                                W.InvoiceDetails = new PInvoiceDetails();
                                W.InvoiceDetails.SupplierGSTIN = Convert.ToString(dr["SupplierGSTIN"]);
                                W.InvoiceDetails.Supplier_addr1 = Convert.ToString(dr["Supplier_addr1"]);
                                W.InvoiceDetails.SupplierLocation = Convert.ToString(dr["SupplierLocation"]);
                                W.InvoiceDetails.SupplierPincode = Convert.ToString(dr["SupplierPincode"]);
                                W.InvoiceDetails.SupplierStateCode = Convert.ToString(dr["SupplierStateCode"]);

                                W.InvoiceDetails.BuyerGSTIN = Convert.ToString(dr["BuyerGSTIN"]);
                                W.InvoiceDetails.BuyerName = Convert.ToString(dr["BuyerName"]);
                                W.InvoiceDetails.BuyerStateCode = Convert.ToString(dr["BuyerStateCode"]);
                                W.InvoiceDetails.Buyer_addr1 = Convert.ToString(dr["Buyer_addr1"]);
                                W.InvoiceDetails.Buyer_loc = Convert.ToString(dr["Buyer_loc"]);
                                W.InvoiceDetails.BuyerPincode = Convert.ToString(dr["BuyerPincode"]);



                                // W.InvoiceDetails = new PDMS_WarrantyClaimInvoiceDetails();
                            }
                            W.SaleOrderDeliveryItems.Add(new PSaleOrderDeliveryItem()
                            {
                                SaleOrderDeliveryItemID = Convert.ToInt64(dr["SaleOrderReturnItemID"]),
                                Material = new PDMS_Material()
                                {
                                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                    MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                    HSN = Convert.ToString(dr["HSNCode"]),
                                    BaseUnit = Convert.ToString(dr["UnitCode"])
                                },
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Value = Convert.ToDecimal(dr["Value"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToDecimal(dr["CGST"]),
                                SGST = Convert.ToDecimal(dr["SGST"]),
                                IGST = Convert.ToDecimal(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"])

                            });
                            W.TCSValue = dr["TCSValue"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TCSValue"]);
                            W.TempTcsMatCount = W.TempTcsMatCount + (Convert.ToString(dr["HSNCode"]) == "998719" ? 0 : 1);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }

        public string GetQRCodePath(string IRN, string InvoiceNumber)
        {
            string code = IRN.Trim();
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = null;
            try
            {
                qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            }
            catch (Exception ex)
            {
            }
            string QRCodeFilePath = string.Format("QRCode/{0}.png", InvoiceNumber);
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    string qrCodeImg = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/QRCode")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/QRCode"));
                    }
                    using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/" + QRCodeFilePath), FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            var base64Data = Regex.Match(qrCodeImg, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                            byte[] data = Convert.FromBase64String(base64Data);
                            bw.Write(data);
                            bw.Close();
                        }
                    }
                }
            }
            return new Uri(HttpContext.Current.Server.MapPath("~/" + QRCodeFilePath)).AbsoluteUri;
        }

       
        public Boolean GetTCS_Validation(string DealerCode, string CustomerCode, decimal Amount)
        {
            Boolean TCS = false;
            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", DealerCode, DbType.String);
            DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.DateTime);
            DbParameter AmountP = provider.CreateParameter("Amount", Amount, DbType.DateTime);
            DbParameter li_retP = provider.CreateParameter("li_ret", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[4] { DealerCodeP, CustomerCodeP, AmountP, li_retP };
            try
            {
                provider.Select("ZDMS_GetTCS_Validation", Params);
                TCS = Convert.ToBoolean(li_retP.Value);
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return TCS;
        }


        public PDMS_EInvoiceSigned GetPaidServiceInvoiceESigned(long InvoiceID)
        {
            PDMS_EInvoiceSigned InvoiceE = new PDMS_EInvoiceSigned();
            try
            {
                DbParameter InvoiceIDP = provider.CreateParameter("InvoiceID", InvoiceID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { InvoiceIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetPaidServiceInvoiceESigned", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            InvoiceE.RefInvoiceID = Convert.ToInt64(dr["RefInvoiceID"]);
                            InvoiceE.IRN = Convert.ToString(dr["IRN"]);
                            InvoiceE.SignedQRCode = Convert.ToString(dr["SignedQRCode"]);
                            InvoiceE.SignedInvoice = Convert.ToString(dr["SignedInvoice"]);
                            InvoiceE.Comments = Convert.ToString(dr["Comments"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return InvoiceE;
        }
        public PDMS_EInvoiceSigned getWarrantyClaimInvoiceESigned(long InvoiceID)
        {
            PDMS_EInvoiceSigned InvoiceE = new PDMS_EInvoiceSigned();
            try
            {
                DbParameter InvoiceIDP = provider.CreateParameter("InvoiceID", InvoiceID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { InvoiceIDP };
                using (DataSet DataSet = provider.Select("ZDMS_getWarrantyClaimInvoiceESigned", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            InvoiceE.RefInvoiceID = Convert.ToInt64(dr["RefInvoiceID"]);
                            InvoiceE.IRN = Convert.ToString(dr["IRN"]);
                            InvoiceE.SignedQRCode = Convert.ToString(dr["SignedQRCode"]);
                            InvoiceE.SignedInvoice = Convert.ToString(dr["SignedInvoice"]);
                            InvoiceE.Comments = Convert.ToString(dr["Comments"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return InvoiceE;
        }
        //public PDMS_EInvoiceSigned getWarrantyClaimDebitNoteESigned(long InvoiceID)
        //{
        //    PDMS_EInvoiceSigned InvoiceE = new PDMS_EInvoiceSigned();
        //    try
        //    {
        //        DbParameter InvoiceIDP = provider.CreateParameter("InvoiceID", InvoiceID, DbType.Int64);
        //        DbParameter[] Params = new DbParameter[1] { InvoiceIDP };
        //        using (DataSet DataSet = provider.Select("ZDMS_GetWarrantyClaimDebitNoteESigned", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    InvoiceE.RefInvoiceID = Convert.ToInt64(dr["RefInvoiceID"]);
        //                    InvoiceE.IRN = Convert.ToString(dr["IRN"]);
        //                    InvoiceE.SignedQRCode = Convert.ToString(dr["SignedQRCode"]);
        //                    InvoiceE.SignedInvoice = Convert.ToString(dr["SignedInvoice"]);
        //                    InvoiceE.Comments = Convert.ToString(dr["Comments"]);
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return InvoiceE;
        //}


        public PDMS_EInvoiceSigned getSalesCommissionClaimInvoiceESigned(long InvoiceID)
        {
            PDMS_EInvoiceSigned InvoiceE = new PDMS_EInvoiceSigned();
            try
            {
                DbParameter InvoiceIDP = provider.CreateParameter("InvoiceID", InvoiceID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { InvoiceIDP };
                using (DataSet DataSet = provider.Select("GetSalesCommissionClaimInvoiceESigned", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            InvoiceE.RefInvoiceID = Convert.ToInt64(dr["RefInvoiceID"]);
                            InvoiceE.IRN = Convert.ToString(dr["IRN"]);
                            InvoiceE.SignedQRCode = Convert.ToString(dr["SignedQRCode"]);
                            InvoiceE.SignedInvoice = Convert.ToString(dr["SignedInvoice"]);
                            InvoiceE.Comments = Convert.ToString(dr["Comments"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return InvoiceE;
        }

        public PDMS_EInvoiceSigned getActivityInvoiceESigned(long InvoiceID)
        {
            PDMS_EInvoiceSigned InvoiceE = new PDMS_EInvoiceSigned();
            try
            {
                DbParameter InvoiceIDP = provider.CreateParameter("InvoiceID", InvoiceID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { InvoiceIDP };
                using (DataSet DataSet = provider.Select("YDMS_getActivityInvoiceESigned_Z", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            InvoiceE.RefInvoiceID = Convert.ToInt64(dr["RefInvoiceID"]);
                            InvoiceE.IRN = Convert.ToString(dr["IRN"]);
                            InvoiceE.SignedQRCode = Convert.ToString(dr["SignedQRCode"]);
                            InvoiceE.SignedInvoice = Convert.ToString(dr["SignedInvoice"]);
                            InvoiceE.Comments = Convert.ToString(dr["Comments"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return InvoiceE;
        }

        public PDMS_EInvoiceSigned GetSaleOrderDeliveryInvoiceESigned(long InvoiceID)
        {
            PDMS_EInvoiceSigned InvoiceE = new PDMS_EInvoiceSigned();
            try
            {
                DbParameter InvoiceIDP = provider.CreateParameter("InvoiceID", InvoiceID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { InvoiceIDP };
                using (DataSet DataSet = provider.Select("GetSaleOrderDeliveryInvoiceESigned", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            InvoiceE.RefInvoiceID = Convert.ToInt64(dr["RefInvoiceID"]);
                            InvoiceE.IRN = Convert.ToString(dr["IRN"]);
                            InvoiceE.SignedQRCode = Convert.ToString(dr["SignedQRCode"]);
                            InvoiceE.SignedInvoice = Convert.ToString(dr["SignedInvoice"]);
                            InvoiceE.Comments = Convert.ToString(dr["Comments"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return InvoiceE;
        }
        public PDMS_EInvoiceSigned GetSaleOrderReturnCreditNoteESigned(long CreditNoteID)
        {
            PDMS_EInvoiceSigned InvoiceE = new PDMS_EInvoiceSigned();
            try
            {
                DbParameter CreditNoteIDP = provider.CreateParameter("CreditNoteID", CreditNoteID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { CreditNoteIDP };
                using (DataSet DataSet = provider.Select("GetSaleOrderReturnCreditNoteESigned", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            InvoiceE.RefInvoiceID = Convert.ToInt64(dr["RefInvoiceID"]);
                            InvoiceE.IRN = Convert.ToString(dr["IRN"]);
                            InvoiceE.SignedQRCode = Convert.ToString(dr["SignedQRCode"]);
                            InvoiceE.SignedInvoice = Convert.ToString(dr["SignedInvoice"]);
                            InvoiceE.Comments = Convert.ToString(dr["Comments"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return InvoiceE;
        }
        public Boolean ValidatePincode(string PinCode, string RegionCode)
        {
            try
            {
                DbParameter PinCodeP = provider.CreateParameter("PinCode", PinCode, DbType.String);
                DbParameter RegionCodeP = provider.CreateParameter("RegionCode", RegionCode, DbType.String);
                DbParameter[] Params = new DbParameter[2] { PinCodeP, RegionCodeP };
                using (DataSet DataSet = provider.Select("ZDMS_ValidatePincode", Params))
                {
                    return Convert.ToBoolean((DataSet.Tables[0].Rows[0][0]));
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return false;
        }

        public Boolean UpdateEInvoiveStatus(string InvoiceNumber, int EInvoiceStatusID, string InvName)
        {
            try
            {
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", InvoiceNumber, DbType.String);
                DbParameter EInvoiceStatusIDP = provider.CreateParameter("EInvoiceStatusID", EInvoiceStatusID, DbType.Int32);
                DbParameter InvNameP = provider.CreateParameter("InvName", InvName, DbType.String);
                DbParameter[] Params = new DbParameter[3] { InvoiceNumberP, EInvoiceStatusIDP, InvNameP };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateEInvoiceStatus", Params); ;
                    scope.Complete();
                }
                return true;
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_ICTicket", "UpdateEInvoiveStatus", e1);
            }
            return false;
        }
    

        public Boolean UpdateEInvoiveBuyerDetails(string InvoiceNumber, string BuyerGSTIN, string BuyerStateCode, string Buyer_addr1, string Buyer_loc, string BuyerPincode, Int32 UserID)
        {
            try
            {
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", InvoiceNumber, DbType.String);
                DbParameter BuyerGSTINP = provider.CreateParameter("BuyerGSTIN", BuyerGSTIN, DbType.String);
                DbParameter BuyerStateCodeP = provider.CreateParameter("BuyerStateCode", BuyerStateCode, DbType.String);
                DbParameter Buyer_addr1P = provider.CreateParameter("Buyer_addr1", Buyer_addr1, DbType.String);
                DbParameter Buyer_locP = provider.CreateParameter("Buyer_loc", Buyer_loc, DbType.String);
                DbParameter BuyerPincodeP = provider.CreateParameter("BuyerPincode", BuyerPincode, DbType.String);

                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.String);
                DbParameter[] Params = new DbParameter[7] { InvoiceNumberP, BuyerGSTINP, BuyerStateCodeP, Buyer_addr1P, Buyer_locP, BuyerPincodeP, UserIDP };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateEInvoiceBuyerDetails", Params); ;
                    scope.Complete();
                }
                return true;
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_EInvoice", "UpdateEInvoiveBuyerDetails", e1);
            }
            return false;
        }
        public Boolean UpdateEInvoiveHSNCode(string InvoiceNumber, string MaterialDesc, string HSNCode, Int32 UserID)
        {
            try
            {
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", InvoiceNumber, DbType.String);
                DbParameter MaterialDescP = provider.CreateParameter("MaterialDesc", MaterialDesc, DbType.String);
                DbParameter HSNCodeP = provider.CreateParameter("HSNCode", HSNCode, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.String);
                DbParameter[] Params = new DbParameter[4] { InvoiceNumberP, MaterialDescP, HSNCodeP, UserIDP };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //provider.Insert("ZDMS_InsertOrUpdateEInvoiceHSNCode", Params); 
                    provider.Insert("InsertOrUpdateEInvoiceHSNCode", Params);
                    scope.Complete();
                }
                return true;
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_EInvoice", "UpdateEInvoiveHSNCode", e1);
            }
            return false;
        }
  
        public void GeneratEInvoiceForSalesCommissionClaimInvoice(long? SalesCommissionClaimInvoiceID, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID)
        {
            PSalesCommissionClaimInvoice Pinv = GetSalesCommissionClaimInvoiceForRequestEInvoice(SalesCommissionClaimInvoiceID, InvoiceNumber, InvoiceDateF, InvoiceDateT, DealerID)[0];
            if ((Pinv.Dealer.IsEInvoice) && (Pinv.Dealer.EInvoiceDate <= Pinv.InvoiceDate))
            {
                if (string.IsNullOrEmpty(Pinv.IRN))
                {
                    try
                    {
                        PDealer Dealer = new BDealer().GetDealerByID(null, Pinv.Dealer.DealerCode);
                        PApiEInvHandle Handle = new PApiEInvHandle();
                        Handle.handle = Dealer.EInvUserAPI.Handle;
                        Handle.handleType = Dealer.EInvUserAPI.HandleType;
                        Handle.password = Dealer.EInvUserAPI.Password;
                        PApiHeader Header = null;
                        PApiResult ResultToken = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPutWithOutToken("EInvoice/GetEInvoiceToken", Handle));
                        if (ResultToken.Status == PApplication.Failure)
                        {
                            return ;
                        }
                        Header = JsonConvert.DeserializeObject<PApiHeader>(JsonConvert.SerializeObject(ResultToken.Data));


                        PEInvoice EInvoice = ConvertSalesCommissionClaimInvoice(Pinv);                            
                        PResultEInv Results = new BApiEInv().ApiPut(Header, Dealer, EInvoice);
                        if (Results.Status == PApplication.Failure)
                        {
                            PSuccessEInv data = (PSuccessEInv)Results.data;
                            IntegrationEInvoive(Pinv.InvoiceNumber, data.data.Irn, data.data.AckDt, data.data.SignedQRCode, data.data.SignedInvoice, "", "SalesCom");
                        }
                        else
                        {
                            PFailedEInv data = (PFailedEInv)Results.data;
                            IntegrationEInvoive(Pinv.InvoiceNumber, null, null, null, null, data.error.message, "SalesCom");
                        }
                    }
                    catch (Exception ex)
                    { 
                    } 
                }
            }
        }
       
        public void IntegrationEInvoive(string InvoiceNumber, string Irn, DateTime? AckDt, string SignedQRCode, string SignedInvoice, string Comments, string InvName)
        {
            try
            {

                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", InvoiceNumber, DbType.String);
                DbParameter IRN = provider.CreateParameter("IRN", Irn, DbType.String);
                DbParameter IRNDateP = provider.CreateParameter("IRNDate", AckDt, DbType.DateTime);
                DbParameter SignedQRCodeP = provider.CreateParameter("SignedQRCode", SignedQRCode, DbType.String);
                DbParameter SignedInvoiceP = provider.CreateParameter("SignedInvoice", SignedInvoice, DbType.String);
                DbParameter CommentsP = provider.CreateParameter("Comments", Comments, DbType.String);
                DbParameter InvNameP = provider.CreateParameter("InvName", InvName, DbType.String);
                DbParameter[] Params = new DbParameter[7] { InvoiceNumberP, IRN, IRNDateP, SignedQRCodeP, SignedInvoiceP, CommentsP, InvNameP };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateEInvoice", Params);
                    scope.Complete();
                }
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_EInvoice", "IntegrationEInvoive", e1);
            }

        }


        public PEInvoice ConvertSalesCommissionClaimInvoice(PSalesCommissionClaimInvoice Pinv)
        {
            PEInvoice EInvoice = new PEInvoice();
            try
            {
                int TOTALLINEITEMS = 0;
                EInvoice.TranDtls = new PTranDtls()
                {
                    //IgstOnIntra = Pinv.InvoiceDetails.SupplierGSTIN.Substring(0, 2) == Pinv.InvoiceDetails.BuyerGSTIN.Substring(0, 2) ? "Y" : "N",
                    IgstOnIntra = "N"
                };
                EInvoice.DocDtls = new PDocDtls()
                {
                    Typ = "INV",
                    No = Pinv.InvoiceNumber,
                    Dt = Pinv.InvoiceDate.ToShortDateString()// .Year.ToString() + Pinv.InvoiceDate.Month.ToString("00") + Pinv.InvoiceDate.Day.ToString("00"),
                };
                EInvoice.SellerDtls = new PSellerDtls()
                {

                    Gstin = Pinv.InvoiceDetails.SupplierGSTIN.Trim(),
                    LglNm = Pinv.Dealer.DealerName,
                    // TrdNm = Pinv.ICTicket.Dealer.DealerName,
                    Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    //Addr2 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                    Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
                    //Ph = Pinv.ICTicket.Dealer.Mobile, 
                    //Em = Pinv.ICTicket.Dealer.Email
                };
                //SupplierCode = Pinv.ICTicket.Dealer.DealerCode, 
                EInvoice.BuyerDtls = new PBuyerDtls()
                {
                    Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                    LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    //TrdNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    Pos = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                    Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    // Addr2 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                    Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                    //Ph = Pinv.ICTicket.Customer.Mobile,
                    //Em = Pinv.ICTicket.Customer.Email 
                };
                EInvoice.DispDtls = new PDispDtls()
                {
                    Nm = Pinv.Dealer.DealerName,
                    Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                    Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
                };

                EInvoice.ShipDtls = new PShipDtls()
                {
                    Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                    LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    //TrdNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    //Addr2 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                    Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                };

                EInvoice.ItemList = new List<PItemList>();
                decimal AccumulatedTotalAmount = 0, AccumulatedAssTotalAmount = 0, AccumulatedSgstVal = 0, AccumulatedIgstVal = 0, AccumulatedCgstVal = 0,
                AccumulatedCesVal = 0, AccumulatedOtherCharges = 0, AccumulatedTotItemVal = 0;
                TOTALLINEITEMS = 0;

                PSalesCommissionClaimInvoiceItem Pinvi = Pinv.InvoiceItem;

                TOTALLINEITEMS = TOTALLINEITEMS + 1;

                AccumulatedTotalAmount = AccumulatedTotalAmount + Pinvi.TaxableValue;
                AccumulatedAssTotalAmount = AccumulatedAssTotalAmount + Pinvi.TaxableValue;
                AccumulatedSgstVal = AccumulatedSgstVal + Pinvi.SGSTValue;
                AccumulatedIgstVal = AccumulatedIgstVal + Pinvi.IGSTValue;
                AccumulatedCgstVal = AccumulatedCgstVal + Pinvi.CGSTValue;
                AccumulatedCesVal = AccumulatedCesVal + 0;
                //     AccumulatedOtherCharges = AccumulatedOtherCharges + Pinvi.t;
                AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;

                EInvoice.ItemList.Add(new PItemList()
                {
                    SlNo = Convert.ToString(TOTALLINEITEMS),
                    PrdDesc = Pinvi.Material.MaterialDescription,
                    IsServc = "Y",
                    HsnCd = Pinvi.Material.HSN,
                    //Barcde = "",
                    Qty = Pinvi.Qty.ToString(),
                    FreeQty = "0",
                    Unit = "NOS",
                    UnitPrice = Math.Round(Pinvi.Rate, 2).ToString(),
                    TotAmt = Math.Round(Pinvi.TaxableValue, 2).ToString(),
                    Discount = "0",
                    PreTaxVal = "0",
                    AssAmt = Math.Round(Pinvi.TaxableValue, 2).ToString(),
                    GstRt = Convert.ToString(Math.Round(Pinvi.CGST + Pinvi.IGST + Pinvi.SGST, 2)),
                    IgstAmt = Convert.ToString(Math.Round(Pinvi.IGSTValue, 2)),
                    CgstAmt = Convert.ToString(Math.Round(Pinvi.CGSTValue, 2)),
                    SgstAmt = Convert.ToString(Math.Round(Pinvi.SGSTValue, 2)),
                    CesRt = "",
                    CesAmt = "0",
                    CesNonAdvlAmt = "",
                    StateCesRt = "",
                    StateCesAmt = "",
                    StateCesNonAdvlAmt = "",
                    OthChrg = "",
                    TotItemVal = Convert.ToString(Math.Round(Pinvi.TaxableValue + Pinvi.SGSTValue + Pinvi.IGSTValue + Pinvi.CGSTValue + 0, 2)),
                    OrdLineRef = Convert.ToString(TOTALLINEITEMS),
                    OrgCntry = "IN",
                    PrdSlNo = Convert.ToString(TOTALLINEITEMS),

                    // public PBchDtls BchDtls { get; set; }
                    // public List<PAttribDtls> AttribDtls { get; set; }
                });
                // InvoiceItemID = Pinvi.PaidServiceInvoiceItemID,
                // BillingDocument = Pinv.InvoiceNumber,   
                // CESSRate = "", 



                EInvoice.ValDtls = new PValDtls()
                {
                    AssVal = Convert.ToString(Math.Round(AccumulatedTotalAmount, 2)),
                    CgstVal = Convert.ToString(Math.Round(AccumulatedCgstVal, 2)),
                    SgstVal = Convert.ToString(Math.Round(AccumulatedSgstVal, 2)),
                    IgstVal = Convert.ToString(Math.Round(AccumulatedIgstVal, 2)),
                    CesVal = Convert.ToString(Math.Round(AccumulatedCesVal, 2)),
                    StCesVal = "0",
                    Discount = "0",
                    OthChrg = Convert.ToString(AccumulatedOtherCharges),
                    RndOffAmt = Convert.ToString(Math.Round(Math.Round(AccumulatedTotItemVal, 2) - AccumulatedTotItemVal, 2)),
                    TotInvVal = Convert.ToString(Math.Round(AccumulatedTotItemVal)),
                    TotInvValFc = Convert.ToString(Math.Round(AccumulatedTotItemVal, 2))

                    //   "ValDtls": {
                    //    "AssVal": 9978.84,
                    //    "CgstVal": 0,
                    //    "SgstVal": 0,
                    //    "IgstVal": 1197.46,
                    //    "CesVal": 508.94,
                    //    "StCesVal": 1202.46,
                    //    "Discount": 10,
                    //    "OthChrg": 20,
                    //    "RndOffAmt": 0.3,
                    //    "TotInvVal": 12908,
                    //    "TotInvValFc": 12897.7
                    //},
                };

                // EInvoice.TOTALLINEITEMS = Convert.ToString(TOTALLINEITEMS);
                // EInvoice.AccumulatedTotalAmount = Convert.ToString(AccumulatedTotalAmount);
                // EInvoice.AccumulatedAssTotalAmount = Convert.ToString(AccumulatedAssTotalAmount); 
                // EInvoice.AccumulatedOtherCharges = Convert.ToString(AccumulatedOtherCharges); 


                EInvoice.PayDtls = new PPayDtls() { };
                EInvoice.RefDtls = new PRefDtls() { };
                EInvoice.AddlDocDtls = new List<PAddlDocDtls>();
                EInvoice.ExpDtls = new PExpDtls()
                {

                };
                EInvoice.EwbDtls = new PEwbDtls() { };
                string SS = JsonConvert.SerializeObject(EInvoice);
            }
            catch (Exception ex)
            {
            }
            return EInvoice;
        }

        //public PEInvoice ConvertSalesCommissionClaimInvoice1(PSalesCommissionClaimInvoice Pinv)
        //{
        //    PEInvoice EInvoice = new PEInvoice();
        //    try
        //    { 
        //        int TOTALLINEITEMS = 0; 
        //        EInvoice.TranDtls = new PTranDtls() { 
        //        };
        //        EInvoice.DocDtls = new PDocDtls()
        //        {
        //            Typ = "INV",
        //            No = Pinv.InvoiceNumber,
        //            Dt = Pinv.InvoiceDate.Year.ToString() + Pinv.InvoiceDate.Month.ToString("00") + Pinv.InvoiceDate.Day.ToString("00"),
        //        };
        //        EInvoice.SellerDtls = new PSellerDtls()
        //        {

        //            Gstin = Pinv.InvoiceDetails.SupplierGSTIN.Trim(),
        //            LglNm = Pinv.Dealer.DealerName,
        //            TrdNm = "",
        //            Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
        //            Addr2 = "",
        //            Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
        //            Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
        //            Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
        //            Ph = "",
        //            Em = "",
        //        };

        //        //SupplierCode = Pinv.ICTicket.Dealer.DealerCode, 

        //        EInvoice.BuyerDtls = new PBuyerDtls()
        //        {
        //            Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
        //            LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
        //            TrdNm = "",
        //            Pos = "",
        //            Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
        //            Addr2 = "",
        //            Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
        //            Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
        //            Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
        //            Ph = "",
        //            Em = ""
        //        };
        //        EInvoice.DispDtls = new PDispDtls()
        //        {
        //            Nm = Pinv.Dealer.DealerName,
        //            Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
        //            Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
        //            Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
        //            Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),

        //        };
        //        EInvoice.ShipDtls = new PShipDtls() { };
        //        EInvoice.ItemList = new List<PItemList>();

        //        decimal AccumulatedTotalAmount = 0, AccumulatedAssTotalAmount = 0, AccumulatedSgstVal = 0, AccumulatedIgstVal = 0, AccumulatedCgstVal = 0,
        //          AccumulatedCesVal = 0, AccumulatedOtherCharges = 0, AccumulatedTotItemVal = 0;
        //        TOTALLINEITEMS = 0;


        //        PSalesCommissionClaimInvoiceItem Pinvi = Pinv.InvoiceItem;

        //        TOTALLINEITEMS = TOTALLINEITEMS + 1;

        //        AccumulatedTotalAmount = AccumulatedTotalAmount + Pinvi.TaxableValue;
        //        AccumulatedAssTotalAmount = AccumulatedAssTotalAmount + Pinvi.TaxableValue;
        //        AccumulatedSgstVal = AccumulatedSgstVal + Pinvi.SGSTValue;
        //        AccumulatedIgstVal = AccumulatedIgstVal + Pinvi.IGSTValue;
        //        AccumulatedCgstVal = AccumulatedCgstVal + Pinvi.CGSTValue;
        //        AccumulatedCesVal = AccumulatedCesVal + 0;
        //        //     AccumulatedOtherCharges = AccumulatedOtherCharges + Pinvi.t;
        //        AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;

        //        EInvoice.ItemList.Add(new PItemList()
        //        {

        //            SlNo = Convert.ToString(TOTALLINEITEMS),
        //            PrdDesc = Pinvi.Material.MaterialDescription,
        //            IsServc = "Y",
        //            HsnCd = Pinvi.Material.HSN,
        //            Barcde = "",
        //            Qty = Pinvi.Qty.ToString(),
        //            FreeQty = "0",
        //            Unit = "NOS",
        //            UnitPrice = Pinvi.Rate.ToString(),
        //            TotAmt = Pinvi.TaxableValue.ToString(),
        //            Discount = "0",
        //            PreTaxVal = "0",
        //            AssAmt = Pinvi.TaxableValue.ToString(),
        //            GstRt = Convert.ToString(Pinvi.CGST + Pinvi.IGST + Pinvi.SGST),
        //            IgstAmt = Convert.ToString(Pinvi.IGSTValue),
        //            CgstAmt = Convert.ToString(Pinvi.CGSTValue),
        //            SgstAmt = Convert.ToString(Pinvi.SGSTValue),
        //            CesRt = "",
        //            CesAmt = "0",
        //            CesNonAdvlAmt = "",
        //            StateCesRt = "",
        //            StateCesAmt = "",
        //            StateCesNonAdvlAmt = "",
        //            OthChrg = "",
        //            TotItemVal = Convert.ToString(Pinvi.TaxableValue + Pinvi.SGSTValue + Pinvi.IGSTValue + Pinvi.CGSTValue + 0),
        //            OrdLineRef = "",
        //            OrgCntry = "",
        //            PrdSlNo = "",

        //            // public PBchDtls BchDtls { get; set; }
        //            // public List<PAttribDtls> AttribDtls { get; set; }
        //        });
        //        // InvoiceItemID = Pinvi.PaidServiceInvoiceItemID,
        //        // BillingDocument = Pinv.InvoiceNumber,   
        //        // CESSRate = "", 



        //        EInvoice.ValDtls = new PValDtls()
        //        {
        //            AssVal = "",
        //            CgstVal = Convert.ToString(AccumulatedCgstVal),
        //            SgstVal = Convert.ToString(AccumulatedSgstVal),
        //            IgstVal = Convert.ToString(AccumulatedIgstVal),
        //            CesVal = Convert.ToString(AccumulatedCesVal),
        //            StCesVal = "0",
        //            Discount = "0",
        //            OthChrg = Convert.ToString(AccumulatedOtherCharges),
        //            RndOffAmt = Convert.ToString(Math.Round(AccumulatedTotItemVal) - AccumulatedTotItemVal),
        //            TotInvVal = Convert.ToString(Math.Round(AccumulatedTotItemVal)),
        //            TotInvValFc = Convert.ToString(AccumulatedTotItemVal)

        //            //   "ValDtls": {
        //            //    "AssVal": 9978.84,
        //            //    "CgstVal": 0,
        //            //    "SgstVal": 0,
        //            //    "IgstVal": 1197.46,
        //            //    "CesVal": 508.94,
        //            //    "StCesVal": 1202.46,
        //            //    "Discount": 10,
        //            //    "OthChrg": 20,
        //            //    "RndOffAmt": 0.3,
        //            //    "TotInvVal": 12908,
        //            //    "TotInvValFc": 12897.7
        //            //},
        //        };

        //        // EInvoice.TOTALLINEITEMS = Convert.ToString(TOTALLINEITEMS);
        //        // EInvoice.AccumulatedTotalAmount = Convert.ToString(AccumulatedTotalAmount);
        //        // EInvoice.AccumulatedAssTotalAmount = Convert.ToString(AccumulatedAssTotalAmount); 
        //        // EInvoice.AccumulatedOtherCharges = Convert.ToString(AccumulatedOtherCharges); 


        //        EInvoice.PayDtls = new PPayDtls() { };
        //        EInvoice.RefDtls = new PRefDtls() { };
        //        EInvoice.AddlDocDtls = new List<PAddlDocDtls>();
        //        EInvoice.ExpDtls = new PExpDtls() { };
        //        EInvoice.EwbDtls = new PEwbDtls() { };

        //        // BuyerCode = Pinv.ICTicket.Customer.CustomerCode,

        //        //Type = "U",
        //        //FileSubName = "PAY"  
                
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return EInvoice;
        //}
        public PEInvoice ConvertPaidServiceInvoice(PDMS_PaidServiceInvoice Pinv)
        {
            PEInvoice EInvoice = new PEInvoice();
            try
            {
                int TOTALLINEITEMS = 0;
                EInvoice.TranDtls = new PTranDtls()
                {
                    // IgstOnIntra = Pinv.InvoiceDetails.SupplierGSTIN.Substring(0, 2) == Pinv.InvoiceDetails.BuyerGSTIN.Substring(0, 2) ? "N" : "Y",
                    IgstOnIntra = "N"
                };
                EInvoice.DocDtls = new PDocDtls()
                {
                    Typ = "INV",
                    No = Pinv.InvoiceNumber,
                    Dt = Pinv.InvoiceDate.ToShortDateString()// .Year.ToString() + Pinv.InvoiceDate.Month.ToString("00") + Pinv.InvoiceDate.Day.ToString("00"),
                };
                EInvoice.SellerDtls = new PSellerDtls()
                {

                    Gstin = Pinv.InvoiceDetails.SupplierGSTIN.Trim(),
                    LglNm = Pinv.ICTicket.Dealer.DealerName,
                    // TrdNm = Pinv.ICTicket.Dealer.DealerName,
                    Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    //Addr2 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                    Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
                    //Ph = Pinv.ICTicket.Dealer.Mobile, 
                    //Em = Pinv.ICTicket.Dealer.Email
                };
                //SupplierCode = Pinv.ICTicket.Dealer.DealerCode, 
                EInvoice.BuyerDtls = new PBuyerDtls()
                {
                    Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                    LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    //TrdNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    Pos = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                    Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    // Addr2 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                    Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                    //Ph = Pinv.ICTicket.Customer.Mobile,
                    //Em = Pinv.ICTicket.Customer.Email 
                };
                EInvoice.DispDtls = new PDispDtls()
                {
                    Nm = Pinv.ICTicket.Dealer.DealerName,
                    Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                    Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
                };

                EInvoice.ShipDtls = new PShipDtls()
                {
                    Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                    LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    //TrdNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    //Addr2 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                    Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                };

                EInvoice.ItemList = new List<PItemList>();
                decimal AccumulatedTotalAmount = 0, AccumulatedAssTotalAmount = 0, AccumulatedSgstVal = 0, AccumulatedIgstVal = 0, AccumulatedCgstVal = 0,
                AccumulatedCesVal = 0, AccumulatedOtherCharges = 0, AccumulatedTotItemVal = 0;
                TOTALLINEITEMS = 0;

                foreach (PDMS_PaidServiceInvoiceItem Pinvi in Pinv.InvoiceItems)
                {
                    TOTALLINEITEMS = TOTALLINEITEMS + 1;

                    AccumulatedTotalAmount = AccumulatedTotalAmount + Pinvi.TaxableValue;
                    AccumulatedAssTotalAmount = AccumulatedAssTotalAmount + Pinvi.TaxableValue;
                    AccumulatedSgstVal = AccumulatedSgstVal + Pinvi.SGSTValue;
                    AccumulatedIgstVal = AccumulatedIgstVal + Pinvi.IGSTValue;
                    AccumulatedCgstVal = AccumulatedCgstVal + Pinvi.CGSTValue;
                    AccumulatedCesVal = AccumulatedCesVal + 0;
                    //     AccumulatedOtherCharges = AccumulatedOtherCharges + Pinvi.t;
                    AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;

                    EInvoice.ItemList.Add(new PItemList()
                    {
                        SlNo = Convert.ToString(TOTALLINEITEMS),
                        PrdDesc = Pinvi.Material.MaterialDescription,
                        IsServc = Pinvi.Material.HSN == "998719" ? "Y" : "N",
                        HsnCd = Pinvi.Material.HSN,
                        //Barcde = "",
                        Qty = Pinvi.Qty.ToString(),
                        FreeQty = "0",
                        Unit = "NOS",
                        UnitPrice = Math.Round((decimal)Pinvi.Rate, 2).ToString(),
                        TotAmt = Math.Round(Pinvi.TaxableValue, 2).ToString(),
                        Discount = Convert.ToString(Pinvi.Discount),
                        PreTaxVal = "0",
                        AssAmt = Math.Round(Pinvi.TaxableValue, 2).ToString(),
                        GstRt = Convert.ToString(Math.Round(Pinvi.CGST + Pinvi.IGST + Pinvi.SGST, 2)),
                        IgstAmt = Convert.ToString(Math.Round(Pinvi.IGSTValue, 2)),
                        CgstAmt = Convert.ToString(Math.Round(Pinvi.CGSTValue, 2)),
                        SgstAmt = Convert.ToString(Math.Round(Pinvi.SGSTValue, 2)),
                        CesRt = "",
                        CesAmt = "0",
                        CesNonAdvlAmt = "",
                        StateCesRt = "",
                        StateCesAmt = "",
                        StateCesNonAdvlAmt = "",
                        OthChrg = "",
                        TotItemVal = Convert.ToString(Math.Round(Pinvi.TaxableValue + Pinvi.SGSTValue + Pinvi.IGSTValue + Pinvi.CGSTValue + 0, 2)),
                        OrdLineRef = Convert.ToString(TOTALLINEITEMS),
                        OrgCntry = "IN",
                        PrdSlNo = Convert.ToString(TOTALLINEITEMS),

                        // public PBchDtls BchDtls { get; set; }
                        // public List<PAttribDtls> AttribDtls { get; set; }
                    });
                    // InvoiceItemID = Pinvi.PaidServiceInvoiceItemID,
                    // BillingDocument = Pinv.InvoiceNumber,   
                    // CESSRate = "", 

                }

                EInvoice.ValDtls = new PValDtls()
                {
                    AssVal = Convert.ToString(Math.Round(AccumulatedTotalAmount, 2)),
                    CgstVal = Convert.ToString(Math.Round(AccumulatedCgstVal, 2)),
                    SgstVal = Convert.ToString(Math.Round(AccumulatedSgstVal, 2)),
                    IgstVal = Convert.ToString(Math.Round(AccumulatedIgstVal, 2)),
                    CesVal = Convert.ToString(Math.Round(AccumulatedCesVal, 2)),
                    StCesVal = "0",
                    Discount = "0",
                    OthChrg = Convert.ToString(AccumulatedOtherCharges),
                    RndOffAmt = Convert.ToString(Math.Round(Math.Round(AccumulatedTotItemVal, 2) - AccumulatedTotItemVal, 2)),
                    TotInvVal = Convert.ToString(Math.Round(AccumulatedTotItemVal)),
                    TotInvValFc = Convert.ToString(Math.Round(AccumulatedTotItemVal, 2))

                    //   "ValDtls": {
                    //    "AssVal": 9978.84,
                    //    "CgstVal": 0,
                    //    "SgstVal": 0,
                    //    "IgstVal": 1197.46,
                    //    "CesVal": 508.94,
                    //    "StCesVal": 1202.46,
                    //    "Discount": 10,
                    //    "OthChrg": 20,
                    //    "RndOffAmt": 0.3,
                    //    "TotInvVal": 12908,
                    //    "TotInvValFc": 12897.7
                    //},
                };

                // EInvoice.TOTALLINEITEMS = Convert.ToString(TOTALLINEITEMS);
                // EInvoice.AccumulatedTotalAmount = Convert.ToString(AccumulatedTotalAmount);
                // EInvoice.AccumulatedAssTotalAmount = Convert.ToString(AccumulatedAssTotalAmount); 
                // EInvoice.AccumulatedOtherCharges = Convert.ToString(AccumulatedOtherCharges); 


                EInvoice.PayDtls = new PPayDtls() { };
                EInvoice.RefDtls = new PRefDtls() { };
                EInvoice.AddlDocDtls = new List<PAddlDocDtls>();
                EInvoice.ExpDtls = new PExpDtls()
                {

                };
                EInvoice.EwbDtls = new PEwbDtls() { };
                string SS = JsonConvert.SerializeObject(EInvoice);
            }
            catch (Exception ex)
            {
            }
            return EInvoice;


            //PEInvoice EInvoice = new PEInvoice();
            //try
            //{
            //    int TOTALLINEITEMS = 0;
            //    EInvoice.TranDtls = new PTranDtls()
            //    {
            //        IgstOnIntra = Pinv.InvoiceDetails.SupplierGSTIN.Substring(0,2) == Pinv.InvoiceDetails.BuyerGSTIN.Substring(0, 2)? "Y":"N",
            //    };
            //    EInvoice.DocDtls = new PDocDtls()
            //    {
            //        Typ = "INV",
            //        No = Pinv.InvoiceNumber,
            //        Dt = Pinv.InvoiceDate.ToShortDateString()// .Year.ToString() + Pinv.InvoiceDate.Month.ToString("00") + Pinv.InvoiceDate.Day.ToString("00"),
            //    };
            //    EInvoice.SellerDtls = new PSellerDtls()
            //    {

            //        Gstin = Pinv.InvoiceDetails.SupplierGSTIN.Trim(),
            //        LglNm = Pinv.ICTicket.Dealer.DealerName,
            //       // TrdNm = Pinv.ICTicket.Dealer.DealerName,
            //        Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
            //        //Addr2 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
            //        Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
            //        Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
            //        Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
            //        //Ph = Pinv.ICTicket.Dealer.Mobile, 
            //        //Em = Pinv.ICTicket.Dealer.Email
            //    };

            //    //SupplierCode = Pinv.ICTicket.Dealer.DealerCode, 

            //    EInvoice.BuyerDtls = new PBuyerDtls()
            //    {
            //        Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
            //        LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
            //        //TrdNm = Pinv.InvoiceDetails.BuyerName.Trim(),
            //        Pos = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
            //        Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
            //       // Addr2 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
            //        Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
            //        Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
            //        Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
            //        //Ph = Pinv.ICTicket.Customer.Mobile,
            //        //Em = Pinv.ICTicket.Customer.Email 
            //};
            //    EInvoice.DispDtls = new PDispDtls()
            //    {
            //        Nm = Pinv.ICTicket.Dealer.DealerName,
            //        Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
            //        Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
            //        Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
            //        Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
            //    };



            //    EInvoice.ShipDtls = new PShipDtls()
            //    { 
            //        Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
            //        LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
            //        //TrdNm = Pinv.InvoiceDetails.BuyerName.Trim(),
            //        Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
            //        //Addr2 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
            //        Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
            //        Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
            //        Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
            //    };

            //    EInvoice.ItemList = new List<PItemList>();
            //    decimal AccumulatedTotalAmount = 0, AccumulatedAssTotalAmount = 0, AccumulatedSgstVal = 0, AccumulatedIgstVal = 0, AccumulatedCgstVal = 0,
            //    AccumulatedCesVal = 0, AccumulatedOtherCharges = 0, AccumulatedTotItemVal = 0;
            //    TOTALLINEITEMS = 0;


            //    foreach (PDMS_PaidServiceInvoiceItem Pinvi in Pinv.InvoiceItems)
            //    { 
            //        TOTALLINEITEMS = TOTALLINEITEMS + 1;

            //        AccumulatedTotalAmount = AccumulatedTotalAmount + Pinvi.TaxableValue;
            //        AccumulatedAssTotalAmount = AccumulatedAssTotalAmount + Pinvi.TaxableValue;
            //        AccumulatedSgstVal = AccumulatedSgstVal + Pinvi.SGSTValue;
            //        AccumulatedIgstVal = AccumulatedIgstVal + Pinvi.IGSTValue;
            //        AccumulatedCgstVal = AccumulatedCgstVal + Pinvi.CGSTValue;
            //        AccumulatedCesVal = AccumulatedCesVal + 0;
            //        //     AccumulatedOtherCharges = AccumulatedOtherCharges + Pinvi.t;
            //        AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;

            //        EInvoice.ItemList.Add(new PItemList()
            //        {

            //            SlNo = Convert.ToString(TOTALLINEITEMS),
            //            PrdDesc = Pinvi.Material.MaterialDescription,
            //            IsServc = "Y",
            //            HsnCd = Pinvi.Material.HSN,
            //            //Barcde = "",
            //            Qty = Pinvi.Qty.ToString(),
            //            FreeQty = "0",
            //            Unit = "NOS",
            //            UnitPrice = Pinvi.Rate.ToString(),
            //            TotAmt = Pinvi.TaxableValue.ToString(),
            //            Discount = Convert.ToString( Pinvi.Discount),
            //            PreTaxVal = "0",
            //            AssAmt = Pinvi.TaxableValue.ToString(),
            //            GstRt = Convert.ToString(Pinvi.CGST + Pinvi.IGST + Pinvi.SGST),
            //            IgstAmt = Convert.ToString(Pinvi.IGSTValue),
            //            CgstAmt = Convert.ToString(Pinvi.CGSTValue),
            //            SgstAmt = Convert.ToString(Pinvi.SGSTValue),
            //            CesRt = "",
            //            CesAmt = "0",
            //            CesNonAdvlAmt = "",
            //            StateCesRt = "",
            //            StateCesAmt = "",
            //            StateCesNonAdvlAmt = "",
            //            OthChrg = "",
            //            TotItemVal = Convert.ToString(Pinvi.TaxableValue + Pinvi.SGSTValue + Pinvi.IGSTValue + Pinvi.CGSTValue + 0),
            //            OrdLineRef = Convert.ToString(TOTALLINEITEMS),
            //            OrgCntry = "IN",
            //            PrdSlNo = Convert.ToString(TOTALLINEITEMS),

            //            // public PBchDtls BchDtls { get; set; }
            //            // public List<PAttribDtls> AttribDtls { get; set; }
            //        });
            //        // InvoiceItemID = Pinvi.PaidServiceInvoiceItemID,
            //        // BillingDocument = Pinv.InvoiceNumber,   
            //        // CESSRate = "", 

            //    }

            //    EInvoice.ValDtls = new PValDtls()
            //    {
            //        AssVal = Convert.ToString(AccumulatedTotalAmount),
            //        CgstVal = Convert.ToString(AccumulatedCgstVal),
            //        SgstVal = Convert.ToString(AccumulatedSgstVal),
            //        IgstVal = Convert.ToString(AccumulatedIgstVal),
            //        CesVal = Convert.ToString(AccumulatedCesVal),
            //        StCesVal = "0",
            //        Discount = "0",
            //        OthChrg = Convert.ToString(AccumulatedOtherCharges),
            //        RndOffAmt = Convert.ToString(Math.Round(AccumulatedTotItemVal) - AccumulatedTotItemVal),
            //        TotInvVal = Convert.ToString(Math.Round(AccumulatedTotItemVal)),
            //        TotInvValFc = Convert.ToString(AccumulatedTotItemVal)

            //        //   "ValDtls": {
            //        //    "AssVal": 9978.84,
            //        //    "CgstVal": 0,
            //        //    "SgstVal": 0,
            //        //    "IgstVal": 1197.46,
            //        //    "CesVal": 508.94,
            //        //    "StCesVal": 1202.46,
            //        //    "Discount": 10,
            //        //    "OthChrg": 20,
            //        //    "RndOffAmt": 0.3,
            //        //    "TotInvVal": 12908,
            //        //    "TotInvValFc": 12897.7
            //        //},
            //    };

            //    // EInvoice.TOTALLINEITEMS = Convert.ToString(TOTALLINEITEMS);
            //    // EInvoice.AccumulatedTotalAmount = Convert.ToString(AccumulatedTotalAmount);
            //    // EInvoice.AccumulatedAssTotalAmount = Convert.ToString(AccumulatedAssTotalAmount); 
            //    // EInvoice.AccumulatedOtherCharges = Convert.ToString(AccumulatedOtherCharges); 


            //    EInvoice.PayDtls = new PPayDtls() { };
            //    EInvoice.RefDtls = new PRefDtls() { };
            //    EInvoice.AddlDocDtls = new List<PAddlDocDtls>();
            //    EInvoice.ExpDtls = new PExpDtls() {

            //    };
            //    EInvoice.EwbDtls = new PEwbDtls() { };
            //    string SS = JsonConvert.SerializeObject(EInvoice);
            //}
            //catch (Exception ex)
            //{
            //}
            //return EInvoice;
        }
        public PEInvoice ConvertActivityInvoice(PDMS_WarrantyClaimInvoice Pinv)
        {
            PEInvoice EInvoice = new PEInvoice();
            try
            {
                int TOTALLINEITEMS = 0;
                EInvoice.TranDtls = new PTranDtls()
                {
                    // IgstOnIntra = Pinv.InvoiceDetails.SupplierGSTIN.Substring(0, 2) == Pinv.InvoiceDetails.BuyerGSTIN.Substring(0, 2) ? "N" : "Y",
                    IgstOnIntra ="N"
                };
                EInvoice.DocDtls = new PDocDtls()
                {
                    Typ = "INV",
                    No = Pinv.InvoiceNumber,
                    Dt = Pinv.InvoiceDate.ToShortDateString()// .Year.ToString() + Pinv.InvoiceDate.Month.ToString("00") + Pinv.InvoiceDate.Day.ToString("00"),
                };
                EInvoice.SellerDtls = new PSellerDtls()
                {

                    Gstin = Pinv.InvoiceDetails.SupplierGSTIN.Trim(),
                    LglNm = Pinv.Dealer.DealerName,
                    // TrdNm = Pinv.ICTicket.Dealer.DealerName,
                    Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    //Addr2 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                    Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
                    //Ph = Pinv.ICTicket.Dealer.Mobile, 
                    //Em = Pinv.ICTicket.Dealer.Email
                };

                //SupplierCode = Pinv.ICTicket.Dealer.DealerCode, 

                EInvoice.BuyerDtls = new PBuyerDtls()
                {
                    Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                    LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    //TrdNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    Pos = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                    Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    // Addr2 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                    Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                    //Ph = Pinv.ICTicket.Customer.Mobile,
                    //Em = Pinv.ICTicket.Customer.Email 
                };
                EInvoice.DispDtls = new PDispDtls()
                {
                    Nm = Pinv.Dealer.DealerName,
                    Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                    Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
                };



                EInvoice.ShipDtls = new PShipDtls()
                {
                    Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                    LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    //TrdNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    //Addr2 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                    Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                };

                EInvoice.ItemList = new List<PItemList>();
                decimal AccumulatedTotalAmount = 0, AccumulatedAssTotalAmount = 0, AccumulatedSgstVal = 0, AccumulatedIgstVal = 0, AccumulatedCgstVal = 0,
                AccumulatedCesVal = 0, AccumulatedOtherCharges = 0, AccumulatedTotItemVal = 0;
                TOTALLINEITEMS = 0;


                foreach (PDMS_WarrantyClaimInvoiceItem Pinvi in Pinv.InvoiceItems)
                {
                    TOTALLINEITEMS = TOTALLINEITEMS + 1;

                    AccumulatedTotalAmount = AccumulatedTotalAmount + Pinvi.TaxableValue;
                    AccumulatedAssTotalAmount = AccumulatedAssTotalAmount + Pinvi.TaxableValue;
                    AccumulatedSgstVal = AccumulatedSgstVal + Pinvi.SGSTValue;
                    AccumulatedIgstVal = AccumulatedIgstVal + Pinvi.IGSTValue;
                    AccumulatedCgstVal = AccumulatedCgstVal + Pinvi.CGSTValue;
                    AccumulatedCesVal = AccumulatedCesVal + 0;
                    //     AccumulatedOtherCharges = AccumulatedOtherCharges + Pinvi.t;
                    AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;

                    EInvoice.ItemList.Add(new PItemList()
                    {

                        SlNo = Convert.ToString(TOTALLINEITEMS),
                        PrdDesc = Pinvi.MaterialDesc,
                        IsServc = "Y",
                        HsnCd = Pinvi.HSNCode,
                        //Barcde = "",
                        Qty = Pinvi.Qty.ToString(),
                        FreeQty = "0",
                        Unit = "NOS",
                        UnitPrice = Math.Round(Pinvi.Rate,2).ToString(),
                        TotAmt = Math.Round(Pinvi.TaxableValue,2).ToString(),
                        Discount = Convert.ToString(Pinvi.Discount),
                        PreTaxVal = "0",
                        AssAmt = Math.Round(Pinvi.TaxableValue,2).ToString(),
                        GstRt = Convert.ToString(Math.Round( Pinvi.CGST + Pinvi.IGST + Pinvi.SGST,2)),
                        IgstAmt = Convert.ToString(Math.Round(Pinvi.IGSTValue,2)),
                        CgstAmt = Convert.ToString(Math.Round(Pinvi.CGSTValue, 2)),
                        SgstAmt = Convert.ToString(Math.Round(Pinvi.SGSTValue, 2)),
                        CesRt = "",
                        CesAmt = "0",
                        CesNonAdvlAmt = "",
                        StateCesRt = "",
                        StateCesAmt = "",
                        StateCesNonAdvlAmt = "",
                        OthChrg = "",
                        TotItemVal = Convert.ToString(Math.Round(Pinvi.TaxableValue + Pinvi.SGSTValue + Pinvi.IGSTValue + Pinvi.CGSTValue + 0, 2)),
                        OrdLineRef = Convert.ToString(TOTALLINEITEMS),
                        OrgCntry = "IN",
                        PrdSlNo = Convert.ToString(TOTALLINEITEMS),

                        // public PBchDtls BchDtls { get; set; }
                        // public List<PAttribDtls> AttribDtls { get; set; }
                    });
                    // InvoiceItemID = Pinvi.PaidServiceInvoiceItemID,
                    // BillingDocument = Pinv.InvoiceNumber,   
                    // CESSRate = "", 

                }

                EInvoice.ValDtls = new PValDtls()
                {
                    AssVal = Convert.ToString(Math.Round(AccumulatedTotalAmount, 2)),
                    CgstVal = Convert.ToString(Math.Round(AccumulatedCgstVal, 2)),
                    SgstVal = Convert.ToString(Math.Round(AccumulatedSgstVal, 2)),
                    IgstVal = Convert.ToString(Math.Round(AccumulatedIgstVal, 2)),
                    CesVal = Convert.ToString(Math.Round(AccumulatedCesVal, 2)),
                    StCesVal = "0",
                    Discount = "0",
                    OthChrg = Convert.ToString(AccumulatedOtherCharges),
                    RndOffAmt = Convert.ToString(Math.Round(Math.Round(AccumulatedTotItemVal, 2) - AccumulatedTotItemVal,2)),
                    TotInvVal = Convert.ToString(Math.Round(AccumulatedTotItemVal)),
                    TotInvValFc = Convert.ToString(Math.Round(AccumulatedTotItemVal, 2))

                    //   "ValDtls": {
                    //    "AssVal": 9978.84,
                    //    "CgstVal": 0,
                    //    "SgstVal": 0,
                    //    "IgstVal": 1197.46,
                    //    "CesVal": 508.94,
                    //    "StCesVal": 1202.46,
                    //    "Discount": 10,
                    //    "OthChrg": 20,
                    //    "RndOffAmt": 0.3,
                    //    "TotInvVal": 12908,
                    //    "TotInvValFc": 12897.7
                    //},
                };

                // EInvoice.TOTALLINEITEMS = Convert.ToString(TOTALLINEITEMS);
                // EInvoice.AccumulatedTotalAmount = Convert.ToString(AccumulatedTotalAmount);
                // EInvoice.AccumulatedAssTotalAmount = Convert.ToString(AccumulatedAssTotalAmount); 
                // EInvoice.AccumulatedOtherCharges = Convert.ToString(AccumulatedOtherCharges); 


                EInvoice.PayDtls = new PPayDtls() { };
                EInvoice.RefDtls = new PRefDtls() { };
                EInvoice.AddlDocDtls = new List<PAddlDocDtls>();
                EInvoice.ExpDtls = new PExpDtls()
                {

                };
                EInvoice.EwbDtls = new PEwbDtls() { };
                string SS = JsonConvert.SerializeObject(EInvoice);
            }
            catch (Exception ex)
            {
            }
            return EInvoice;
        }
        public PEInvoice ConvertWarrantyInvoice(PDMS_WarrantyClaimInvoice Pinv)
        {
            PEInvoice EInvoice = new PEInvoice();
            try
            {
                int TOTALLINEITEMS = 0;
                EInvoice.TranDtls = new PTranDtls()
                {
                    //IgstOnIntra = Pinv.InvoiceDetails.SupplierGSTIN.Substring(0, 2) == Pinv.InvoiceDetails.BuyerGSTIN.Substring(0, 2) ? "Y" : "N",
                    IgstOnIntra = "N"
                };
                EInvoice.DocDtls = new PDocDtls()
                {
                    Typ = "INV",
                    No = Pinv.InvoiceNumber,
                    Dt = Pinv.InvoiceDate.ToShortDateString()// .Year.ToString() + Pinv.InvoiceDate.Month.ToString("00") + Pinv.InvoiceDate.Day.ToString("00"),
                };
                EInvoice.SellerDtls = new PSellerDtls()
                {

                    Gstin = Pinv.InvoiceDetails.SupplierGSTIN.Trim(),
                    LglNm = Pinv.Dealer.DealerName,
                    // TrdNm = Pinv.ICTicket.Dealer.DealerName,
                    Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    //Addr2 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                    Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
                    //Ph = Pinv.ICTicket.Dealer.Mobile, 
                    //Em = Pinv.ICTicket.Dealer.Email
                };
                //SupplierCode = Pinv.ICTicket.Dealer.DealerCode, 
                EInvoice.BuyerDtls = new PBuyerDtls()
                {
                    Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                    LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    //TrdNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    Pos = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                    Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    // Addr2 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                    Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                    //Ph = Pinv.ICTicket.Customer.Mobile,
                    //Em = Pinv.ICTicket.Customer.Email 
                };
                EInvoice.DispDtls = new PDispDtls()
                {
                    Nm = Pinv.Dealer.DealerName,
                    Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                    Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
                };
                 
                EInvoice.ShipDtls = new PShipDtls()
                {
                    Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                    LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    //TrdNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    //Addr2 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                    Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                };

                EInvoice.ItemList = new List<PItemList>();
                decimal AccumulatedTotalAmount = 0, AccumulatedAssTotalAmount = 0, AccumulatedSgstVal = 0, AccumulatedIgstVal = 0, AccumulatedCgstVal = 0,
                AccumulatedCesVal = 0, AccumulatedOtherCharges = 0, AccumulatedTotItemVal = 0;
                TOTALLINEITEMS = 0;

                foreach (PDMS_WarrantyClaimInvoiceItem Pinvi in Pinv.InvoiceItems)
                {
                    TOTALLINEITEMS = TOTALLINEITEMS + 1;

                    AccumulatedTotalAmount = AccumulatedTotalAmount + Pinvi.TaxableValue;
                    AccumulatedAssTotalAmount = AccumulatedAssTotalAmount + Pinvi.TaxableValue;
                    AccumulatedSgstVal = AccumulatedSgstVal + Pinvi.SGSTValue;
                    AccumulatedIgstVal = AccumulatedIgstVal + Pinvi.IGSTValue;
                    AccumulatedCgstVal = AccumulatedCgstVal + Pinvi.CGSTValue;
                    AccumulatedCesVal = AccumulatedCesVal + 0;
                    //     AccumulatedOtherCharges = AccumulatedOtherCharges + Pinvi.t;
                    AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;

                    EInvoice.ItemList.Add(new PItemList()
                    {
                        SlNo = Convert.ToString(TOTALLINEITEMS),
                        PrdDesc = Pinvi.MaterialDesc,
                        IsServc = Pinvi.HSNCode == "998719" ? "Y" : "N",
                        HsnCd = Pinvi.HSNCode,
                        //Barcde = "",
                        Qty = Pinvi.Qty.ToString(),
                        FreeQty = "0",
                        Unit = "NOS",
                        UnitPrice = Math.Round(Pinvi.Rate, 2).ToString(),
                        TotAmt = Math.Round(Pinvi.TaxableValue, 2).ToString(),
                        Discount = Convert.ToString(Pinvi.Discount),
                        PreTaxVal = "0",
                        AssAmt = Math.Round(Pinvi.TaxableValue, 2).ToString(),
                        GstRt = Convert.ToString(Math.Round(Pinvi.CGST + Pinvi.IGST + Pinvi.SGST, 2)),
                        IgstAmt = Convert.ToString(Math.Round(Pinvi.IGSTValue, 2)),
                        CgstAmt = Convert.ToString(Math.Round(Pinvi.CGSTValue, 2)),
                        SgstAmt = Convert.ToString(Math.Round(Pinvi.SGSTValue, 2)),
                        CesRt = "",
                        CesAmt = "0",
                        CesNonAdvlAmt = "",
                        StateCesRt = "",
                        StateCesAmt = "",
                        StateCesNonAdvlAmt = "",
                        OthChrg = "",
                        TotItemVal = Convert.ToString(Math.Round(Pinvi.TaxableValue + Pinvi.SGSTValue + Pinvi.IGSTValue + Pinvi.CGSTValue + 0, 2)),
                        OrdLineRef = Convert.ToString(TOTALLINEITEMS),
                        OrgCntry = "IN",
                        PrdSlNo = Convert.ToString(TOTALLINEITEMS),

                        // public PBchDtls BchDtls { get; set; }
                        // public List<PAttribDtls> AttribDtls { get; set; }
                    });
                    // InvoiceItemID = Pinvi.PaidServiceInvoiceItemID,
                    // BillingDocument = Pinv.InvoiceNumber,   
                    // CESSRate = "", 

                }

                EInvoice.ValDtls = new PValDtls()
                {
                    AssVal = Convert.ToString(Math.Round(AccumulatedTotalAmount, 2)),
                    CgstVal = Convert.ToString(Math.Round(AccumulatedCgstVal, 2)),
                    SgstVal = Convert.ToString(Math.Round(AccumulatedSgstVal, 2)),
                    IgstVal = Convert.ToString(Math.Round(AccumulatedIgstVal, 2)),
                    CesVal = Convert.ToString(Math.Round(AccumulatedCesVal, 2)),
                    StCesVal = "0",
                    Discount = "0",
                    OthChrg = Convert.ToString(AccumulatedOtherCharges),
                    RndOffAmt = Convert.ToString(Math.Round(Math.Round(AccumulatedTotItemVal, 2) - AccumulatedTotItemVal, 2)),
                    TotInvVal = Convert.ToString(Math.Round(AccumulatedTotItemVal)),
                    TotInvValFc = Convert.ToString(Math.Round(AccumulatedTotItemVal, 2))

                    //   "ValDtls": {
                    //    "AssVal": 9978.84,
                    //    "CgstVal": 0,
                    //    "SgstVal": 0,
                    //    "IgstVal": 1197.46,
                    //    "CesVal": 508.94,
                    //    "StCesVal": 1202.46,
                    //    "Discount": 10,
                    //    "OthChrg": 20,
                    //    "RndOffAmt": 0.3,
                    //    "TotInvVal": 12908,
                    //    "TotInvValFc": 12897.7
                    //},
                };

                // EInvoice.TOTALLINEITEMS = Convert.ToString(TOTALLINEITEMS);
                // EInvoice.AccumulatedTotalAmount = Convert.ToString(AccumulatedTotalAmount);
                // EInvoice.AccumulatedAssTotalAmount = Convert.ToString(AccumulatedAssTotalAmount); 
                // EInvoice.AccumulatedOtherCharges = Convert.ToString(AccumulatedOtherCharges); 


                EInvoice.PayDtls = new PPayDtls() { };
                EInvoice.RefDtls = new PRefDtls() { };
                EInvoice.AddlDocDtls = new List<PAddlDocDtls>();
                EInvoice.ExpDtls = new PExpDtls()
                {

                };
                EInvoice.EwbDtls = new PEwbDtls() { };
                string SS = JsonConvert.SerializeObject(EInvoice);
            }
            catch (Exception ex)
            {
            }
            return EInvoice;

           
        }
        public PEInvoice ConvertSaleInvoice(PSaleOrderDelivery Pinv)
        {
            PEInvoice EInvoice = new PEInvoice();
            try
            {
                int TOTALLINEITEMS = 0;
                EInvoice.TranDtls = new PTranDtls()
                { 
                    IgstOnIntra = "N"
                };
                EInvoice.DocDtls = new PDocDtls()
                {
                    Typ = "INV",
                    No = Pinv.InvoiceNumber,
                    Dt =((DateTime) Pinv.InvoiceDate).ToShortDateString() 
                };
                EInvoice.SellerDtls = new PSellerDtls()
                {

                    Gstin = Pinv.InvoiceDetails.SupplierGSTIN.Trim(),
                    LglNm = Pinv.SaleOrder.Dealer.DealerName, 
                    Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(), 
                    Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                    Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(), 
                }; 
                EInvoice.BuyerDtls = new PBuyerDtls()
                {
                    Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                    LglNm = Pinv.InvoiceDetails.BuyerName.Trim(), 
                    Pos = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                    Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(), 
                    Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                    Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(), 
                };
                EInvoice.DispDtls = new PDispDtls()
                {
                    Nm = Pinv.SaleOrder.Dealer.DealerName,
                    Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                    Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
                };

                EInvoice.ShipDtls = new PShipDtls()
                {
                    Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                    LglNm = Pinv.InvoiceDetails.BuyerName.Trim(), 
                    Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(), 
                    Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                    Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                };

                EInvoice.ItemList = new List<PItemList>();
                decimal AccumulatedTotalAmount = 0, AccumulatedAssTotalAmount = 0, AccumulatedSgstVal = 0, AccumulatedIgstVal = 0, AccumulatedCgstVal = 0,
                AccumulatedCesVal = 0, AccumulatedOtherCharges = 0, AccumulatedTotItemVal = 0;
                TOTALLINEITEMS = 0;

                foreach (PSaleOrderDeliveryItem Pinvi in Pinv.SaleOrderDeliveryItems)
                {
                    TOTALLINEITEMS = TOTALLINEITEMS + 1;

                    AccumulatedTotalAmount = AccumulatedTotalAmount + Pinvi.TaxableValue;
                    AccumulatedAssTotalAmount = AccumulatedAssTotalAmount + Pinvi.TaxableValue;
                    AccumulatedSgstVal = AccumulatedSgstVal + Pinvi.SGSTValue;
                    AccumulatedIgstVal = AccumulatedIgstVal + Pinvi.IGSTValue;
                    AccumulatedCgstVal = AccumulatedCgstVal + Pinvi.CGSTValue;
                    AccumulatedCesVal = AccumulatedCesVal + 0; 
                    AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;

                    EInvoice.ItemList.Add(new PItemList()
                    {
                        SlNo = Convert.ToString(TOTALLINEITEMS),
                        PrdDesc = Pinvi.Material.MaterialDescription,
                        IsServc = Pinvi.Material.HSN == "998719" ? "Y" : "N",
                        HsnCd = Pinvi.Material.HSN,
                        //Barcde = "",
                        Qty = Pinvi.Qty.ToString(),
                        FreeQty = "0",
                        Unit = "NOS",
                        UnitPrice = Math.Round(Pinvi.Value, 2).ToString(),
                        TotAmt = Math.Round(Pinvi.TaxableValue, 2).ToString(),
                        Discount = Convert.ToString(Pinvi.DiscountValue),
                        PreTaxVal = "0",
                        AssAmt = Math.Round(Pinvi.TaxableValue, 2).ToString(),
                        GstRt = Convert.ToString(Math.Round(Pinvi.CGST + Pinvi.IGST + Pinvi.SGST, 2)),
                        IgstAmt = Convert.ToString(Math.Round(Pinvi.IGSTValue, 2)),
                        CgstAmt = Convert.ToString(Math.Round(Pinvi.CGSTValue, 2)),
                        SgstAmt = Convert.ToString(Math.Round(Pinvi.SGSTValue, 2)),
                        CesRt = "",
                        CesAmt = "0",
                        CesNonAdvlAmt = "",
                        StateCesRt = "",
                        StateCesAmt = "",
                        StateCesNonAdvlAmt = "",
                        OthChrg = "",
                        TotItemVal = Convert.ToString(Math.Round(Pinvi.TaxableValue + Pinvi.SGSTValue + Pinvi.IGSTValue + Pinvi.CGSTValue + 0, 2)),
                        OrdLineRef = Convert.ToString(TOTALLINEITEMS),
                        OrgCntry = "IN",
                        PrdSlNo = Convert.ToString(TOTALLINEITEMS), 
                    }); 
                }
                AccumulatedOtherCharges = AccumulatedOtherCharges + Pinv.TCSValue;
                AccumulatedTotItemVal = AccumulatedTotItemVal + AccumulatedOtherCharges;
                EInvoice.ValDtls = new PValDtls()
                {
                    AssVal = Convert.ToString(Math.Round(AccumulatedTotalAmount, 2)),
                    CgstVal = Convert.ToString(Math.Round(AccumulatedCgstVal, 2)),
                    SgstVal = Convert.ToString(Math.Round(AccumulatedSgstVal, 2)),
                    IgstVal = Convert.ToString(Math.Round(AccumulatedIgstVal, 2)),
                    CesVal = Convert.ToString(Math.Round(AccumulatedCesVal, 2)),
                    StCesVal = "0",
                    Discount = "0",
                    OthChrg = Convert.ToString(AccumulatedOtherCharges),
                    RndOffAmt = Convert.ToString(Math.Round(Math.Round(AccumulatedTotItemVal, 2) - AccumulatedTotItemVal, 2)),
                    TotInvVal = Convert.ToString(Math.Round(AccumulatedTotItemVal)),
                    TotInvValFc = Convert.ToString(Math.Round(AccumulatedTotItemVal, 2))
 
                }; 
                EInvoice.PayDtls = new PPayDtls() { };
                EInvoice.RefDtls = new PRefDtls() { };
                EInvoice.AddlDocDtls = new List<PAddlDocDtls>();
                EInvoice.ExpDtls = new PExpDtls()
                {

                };
                EInvoice.EwbDtls = new PEwbDtls() { };
                string SS = JsonConvert.SerializeObject(EInvoice);
            }
            catch (Exception ex)
            {
            }
            return EInvoice;


        }
        public PEInvoice ConvertSaleReturnCreditNote(PSaleOrderDelivery Pinv)
        {
            PEInvoice EInvoice = new PEInvoice();
            try
            {
                int TOTALLINEITEMS = 0;
                EInvoice.TranDtls = new PTranDtls()
                {
                    IgstOnIntra = "N"
                };
                EInvoice.DocDtls = new PDocDtls()
                {
                    Typ = "CRN",
                    No = Pinv.InvoiceNumber,
                    Dt = ((DateTime)Pinv.InvoiceDate).ToShortDateString()
                };
                EInvoice.SellerDtls = new PSellerDtls()
                {

                    Gstin = Pinv.InvoiceDetails.SupplierGSTIN.Trim(),
                    LglNm = Pinv.SaleOrder.Dealer.DealerName,
                    Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                    Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
                };
                EInvoice.BuyerDtls = new PBuyerDtls()
                {
                    Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                    LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    Pos = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                    Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                    Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                };
                EInvoice.DispDtls = new PDispDtls()
                {
                    Nm = Pinv.SaleOrder.Dealer.DealerName,
                    Addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                    Pin = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.SupplierStateCode.Trim(),
                };

                EInvoice.ShipDtls = new PShipDtls()
                {
                    Gstin = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                    LglNm = Pinv.InvoiceDetails.BuyerName.Trim(),
                    Addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                    Loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                    Pin = Pinv.InvoiceDetails.BuyerPincode.Trim(),
                    Stcd = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                };

                EInvoice.ItemList = new List<PItemList>();
                decimal AccumulatedTotalAmount = 0, AccumulatedAssTotalAmount = 0, AccumulatedSgstVal = 0, AccumulatedIgstVal = 0, AccumulatedCgstVal = 0,
                AccumulatedCesVal = 0, AccumulatedOtherCharges = 0, AccumulatedTotItemVal = 0;
                TOTALLINEITEMS = 0;

                foreach (PSaleOrderDeliveryItem Pinvi in Pinv.SaleOrderDeliveryItems)
                {
                    TOTALLINEITEMS = TOTALLINEITEMS + 1;

                    AccumulatedTotalAmount = AccumulatedTotalAmount + Pinvi.TaxableValue;
                    AccumulatedAssTotalAmount = AccumulatedAssTotalAmount + Pinvi.TaxableValue;
                    AccumulatedSgstVal = AccumulatedSgstVal + Pinvi.SGSTValue;
                    AccumulatedIgstVal = AccumulatedIgstVal + Pinvi.IGSTValue;
                    AccumulatedCgstVal = AccumulatedCgstVal + Pinvi.CGSTValue;
                    AccumulatedCesVal = AccumulatedCesVal + 0;
                    AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;

                    EInvoice.ItemList.Add(new PItemList()
                    {
                        SlNo = Convert.ToString(TOTALLINEITEMS),
                        PrdDesc = Pinvi.Material.MaterialDescription,
                        IsServc = Pinvi.Material.HSN == "998719" ? "Y" : "N",
                        HsnCd = Pinvi.Material.HSN,
                        //Barcde = "",
                        Qty = Pinvi.Qty.ToString(),
                        FreeQty = "0",
                        Unit = "NOS",
                        UnitPrice = Math.Round(Pinvi.Value, 2).ToString(),
                        TotAmt = Math.Round(Pinvi.TaxableValue, 2).ToString(),
                        Discount = Convert.ToString(Pinvi.DiscountValue),
                        PreTaxVal = "0",
                        AssAmt = Math.Round(Pinvi.TaxableValue, 2).ToString(),
                        GstRt = Convert.ToString(Math.Round(Pinvi.CGST + Pinvi.IGST + Pinvi.SGST, 2)),
                        IgstAmt = Convert.ToString(Math.Round(Pinvi.IGSTValue, 2)),
                        CgstAmt = Convert.ToString(Math.Round(Pinvi.CGSTValue, 2)),
                        SgstAmt = Convert.ToString(Math.Round(Pinvi.SGSTValue, 2)),
                        CesRt = "",
                        CesAmt = "0",
                        CesNonAdvlAmt = "",
                        StateCesRt = "",
                        StateCesAmt = "",
                        StateCesNonAdvlAmt = "",
                        OthChrg = "",
                        TotItemVal = Convert.ToString(Math.Round(Pinvi.TaxableValue + Pinvi.SGSTValue + Pinvi.IGSTValue + Pinvi.CGSTValue + 0, 2)),
                        OrdLineRef = Convert.ToString(TOTALLINEITEMS),
                        OrgCntry = "IN",
                        PrdSlNo = Convert.ToString(TOTALLINEITEMS),
                    });
                }

                EInvoice.ValDtls = new PValDtls()
                {
                    AssVal = Convert.ToString(Math.Round(AccumulatedTotalAmount, 2)),
                    CgstVal = Convert.ToString(Math.Round(AccumulatedCgstVal, 2)),
                    SgstVal = Convert.ToString(Math.Round(AccumulatedSgstVal, 2)),
                    IgstVal = Convert.ToString(Math.Round(AccumulatedIgstVal, 2)),
                    CesVal = Convert.ToString(Math.Round(AccumulatedCesVal, 2)),
                    StCesVal = "0",
                    Discount = "0",
                    OthChrg = Convert.ToString(AccumulatedOtherCharges),
                    RndOffAmt = Convert.ToString(Math.Round(Math.Round(AccumulatedTotItemVal, 2) - AccumulatedTotItemVal, 2)),
                    TotInvVal = Convert.ToString(Math.Round(AccumulatedTotItemVal)),
                    TotInvValFc = Convert.ToString(Math.Round(AccumulatedTotItemVal, 2))

                };
                EInvoice.PayDtls = new PPayDtls() { };
                EInvoice.RefDtls = new PRefDtls() { };
                EInvoice.AddlDocDtls = new List<PAddlDocDtls>();
                EInvoice.ExpDtls = new PExpDtls()
                {

                };
                EInvoice.EwbDtls = new PEwbDtls() { };
                string SS = JsonConvert.SerializeObject(EInvoice);
            }
            catch (Exception ex)
            {
            }
            return EInvoice;


        }
        public string GeneratEInvoice(string InvoiceNumber, string InvType)
        {
            PEInvoice EInvoice = new PEInvoice();
            PDealer Dealer = new PDealer();

            string IRN = "";
            if (InvType == "PAY")
            {
                PDMS_PaidServiceInvoice Pinv = GetPaidServiceInvoiceForRequestEInvoice(InvoiceNumber, null, null, null, "")[0];
                Dealer = new BDealer().GetDealerByID(null, Pinv.ICTicket.Dealer.DealerCode);
                EInvoice = ConvertPaidServiceInvoice(Pinv);
                IRN = Pinv.IRN;
            }
            else if (InvType == "ATY")
            {
                PDMS_WarrantyClaimInvoice Pinv = getActivityInvoiceForRequestEInvoice(InvoiceNumber, null, null, null)[0];
                Dealer = new BDealer().GetDealerByID(null, Pinv.Dealer.DealerCode);
                EInvoice = ConvertActivityInvoice(Pinv);
                IRN = Pinv.IRN;
            }
            else if (InvType == "WARR")
            {
                PDMS_WarrantyClaimInvoice Pinv = getWarrantyClaimInvoiceForRequestEInvoice(InvoiceNumber, null, null, null,null)[0];
                Dealer = new BDealer().GetDealerByID(null, Pinv.Dealer.DealerCode);
                EInvoice = ConvertWarrantyInvoice(Pinv);
                IRN = Pinv.IRN;
            }
            else if (InvType == "SalesCom")
            {
                PSalesCommissionClaimInvoice Pinv = GetSalesCommissionClaimInvoiceForRequestEInvoice(null,InvoiceNumber, null, null, null)[0];
                Dealer = new BDealer().GetDealerByID(null, Pinv.Dealer.DealerCode);
                EInvoice = ConvertSalesCommissionClaimInvoice(Pinv);
                IRN = Pinv.IRN;
            }
            else if (InvType == "SalesInv")
            {
                PSaleOrderDelivery Pinv = GetSaleInvoiceForRequestEInvoice(InvoiceNumber, null, null, null, "")[0];
                Dealer = new BDealer().GetDealerByID(null, Pinv.SaleOrder.Dealer.DealerCode);
                EInvoice = ConvertSaleInvoice(Pinv);
                IRN = Pinv.IRN;
            }
            else if (InvType == "SalesReCre")
            {
                PSaleOrderDelivery Pinv = GetSaleReturnCreditNoteForRequestEInvoice(InvoiceNumber, null, null, null, "")[0];
                Dealer = new BDealer().GetDealerByID(null, Pinv.SaleOrder.Dealer.DealerCode);
                EInvoice = ConvertSaleReturnCreditNote(Pinv);
                IRN = Pinv.IRN;
            }
            string Message = ValidationEInvoice(EInvoice);
            if (!string.IsNullOrEmpty(Message))
            {
                return Message;
            }
            if ((!Dealer.EInvAPI))
            {
                return "This Dealer not under E Invoice";
            }
            if (!string.IsNullOrEmpty(IRN))
            {
                return "IRN already created";
            }
            try
            {
                PApiHeader Header = null;
                //PApplicationSettings Asetting = new BApplicationSettings().getAppSetting((short)ApplicationSettings.EInvoiceToken)[0];
                //if (Convert.ToDateTime(Asetting.Value3).AddHours(6) >= DateTime.Now)
                //{
                //    Header = new PApiHeader();
                //    Header.Data = new PHeaderData();
                //    Header.Data.token = Asetting.Value1;
                //    Header.Data.associatedOrgs = new List<PHeaderDataAssociated>();
                //    Header.Data.associatedOrgs.Add(new PHeaderDataAssociated() { organisation = new PHeaderDataAssociatedOrg() { id = Asetting.Value2 } });
                //}
                //else
                //{
                //    PApiEInv ul = new PApiEInv();
                //    ul.handle = Dealer.EInvUserAPI.Handle;
                //    ul.handleType = Dealer.EInvUserAPI.HandleType;
                //    ul.password = Dealer.EInvUserAPI.Password;
                //    Header = JsonConvert.DeserializeObject<PApiHeader>(new BApiEInv().GetAccessToken(ul));
                //    int SettingID = (short)ApplicationSettings.EInvoiceToken;
                //    string Value1 = Header.Data.token;
                //    string Value2 = Header.Data.associatedOrgs[0].organisation.id;
                //    string Value3 = DateTime.Now.ToString();
                //    new BApplicationSettings().UpdateApplicationSetting(SettingID, Value1, Value2, Value3);

                //}

                //if (Header == null)
                //{
                //    return "Token not generated";
                //}

                PApiEInvHandle Handle = new PApiEInvHandle();
                Handle.handle = Dealer.EInvUserAPI.Handle;
                Handle.handleType = Dealer.EInvUserAPI.HandleType;
                Handle.password = Dealer.EInvUserAPI.Password;
               // return JsonConvert.DeserializeObject<List<PDMS_Dealer>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
                PApiResult ResultToken= JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPutWithOutToken("EInvoice/GetEInvoiceToken", Handle));
                if (ResultToken.Status == PApplication.Failure)
                {
                    return "Token not generated";
                }
                Header= JsonConvert.DeserializeObject<PApiHeader>(JsonConvert.SerializeObject(ResultToken.Data));
                PResultEInv Results = new BApiEInv().ApiPut(Header, Dealer, EInvoice);
                // PResultEInv Results = new PResultEInv();
                // PSuccessEInv PSuccess = JsonConvert.DeserializeObject<PSuccessEInv>("{\"data\": {\"AckNo\": 162210030870114,\"AckDt\": \"2022-01-10 12:21:00\",\"Irn\": \"Irn0158eb6a8b\",\"SignedInvoice\": \"SignedInvoiceuMMJAeuQ\",\"SignedQRCode\": \"SignedQRCodeFyA\",\"Status\": \"ACT\",\"EwbNo\": null,\"EwbDt\": null,\"EwbValidTill\": null,\"Remarks\": null }}");
                if (Results.Status == PApplication.Success)
                {
                    //PResultEInvData data = JsonConvert.DeserializeObject<PResultEInvData>(JsonConvert.SerializeObject(Results.data));
                    PSuccessEInvData data = (PSuccessEInvData)Results.data;
                    IntegrationEInvoive(InvoiceNumber, data.Irn, data.AckDt, data.SignedQRCode, data.SignedInvoice, "", InvType);
                }
                else
                {
                    string data = (string)Results.data;
                    IntegrationEInvoive(InvoiceNumber, null, null, null, null, data, InvType);
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }
        public string ValidationEInvoice(PEInvoice Inv)
        {
            try
            {  
                if (string.IsNullOrEmpty(Inv.BuyerDtls.Gstin))
                {
                    return "Please update Buyer GST Number";
                }
                String regexS = "^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexS);
                if (Inv.BuyerDtls.Gstin == "URD")
                {
                    return "Customer  GST Number is URD";
                }
                else if (regex.Match(Inv.BuyerDtls.Gstin).Success)
                {
                    if (Inv.BuyerDtls.Gstin.Trim().Substring(0, 2) != Inv.BuyerDtls.Stcd.Trim())
                    {
                        return "Please update Buyer State Code";
                    }
                }
                else
                {
                    return "Please update correct GST Number";
                }
                if (string.IsNullOrEmpty(Inv.BuyerDtls.Addr1.Trim()))
                {
                    return "Please update Buyer Address";
                }
                if (!new BDMS_EInvoice().ValidatePincode(Inv.BuyerDtls.Pin.Substring(0, 2), Inv.BuyerDtls.Stcd))
                {
                    return "Please check Buyer Pincode and Statecode";
                }

                if (string.IsNullOrEmpty(Inv.SellerDtls.Gstin) || string.IsNullOrEmpty(Inv.SellerDtls.Loc) || string.IsNullOrEmpty(Inv.SellerDtls.Pin) || string.IsNullOrEmpty(Inv.SellerDtls.Stcd))
                {
                    return "Please check the supplier details of Invoice (" + Inv.DocDtls.No + ")";
                }
                if (string.IsNullOrEmpty(Inv.BuyerDtls.Gstin) || string.IsNullOrEmpty(Inv.BuyerDtls.Loc) || string.IsNullOrEmpty(Inv.BuyerDtls.Pin) || string.IsNullOrEmpty(Inv.BuyerDtls.Stcd))
                {
                    return "Please check the Buyer details of Invoice (" + Inv.DocDtls.No + ")";
                } 
            }
            catch (Exception e)
            { }
            return "";
        }



        public void StartGeneratEInvoice()
        {
            Dictionary<string, string> Invs = GetInvoiceForEInvoiceRequest();
            foreach (KeyValuePair<string, string>  inv in Invs)
            {
                GeneratEInvoice(inv.Key, inv.Value);
            }
        }

        public Dictionary<string, string> GetInvoiceForEInvoiceRequest()
        {
           // List<string> Inv = new List<string>(); 
            Dictionary<string,string> Inv = new Dictionary<string, string>(); 
            try
            { 
                
                using (DataSet DataSet = provider.Select("GetInvoiceForEInvoiceRequest"))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Inv.Add(Convert.ToString(dr["InvoiceNumber"]), Convert.ToString(dr["InvType"]));
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Inv;
        }
        public PDMS_EInvoiceSigned GetSaleInvoiceESigned(long InvoiceID)
        {
            PDMS_EInvoiceSigned InvoiceE = new PDMS_EInvoiceSigned();
            try
            {
                DbParameter InvoiceIDP = provider.CreateParameter("InvoiceID", InvoiceID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { InvoiceIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetPaidServiceInvoiceESigned", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            InvoiceE.RefInvoiceID = Convert.ToInt64(dr["RefInvoiceID"]);
                            InvoiceE.IRN = Convert.ToString(dr["IRN"]);
                            InvoiceE.SignedQRCode = Convert.ToString(dr["SignedQRCode"]);
                            InvoiceE.SignedInvoice = Convert.ToString(dr["SignedInvoice"]);
                            InvoiceE.Comments = Convert.ToString(dr["Comments"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return InvoiceE;
        }

        public string GetInvoiceError(string InvoiceNumber, string InvTyp)
        { 
            try
            {
                string Quer = "";
                if (InvTyp == "PAY")
                {
                    Quer = "select E.Comments from ZDMS_TServiceInvoice H left join ZDMS_TServiceInvoiceE  E on E.ServiceInvoiceID = H.ServiceInvoiceID where  H.InvoiceNumber ='"+ InvoiceNumber + "'";
                }
                else if(InvTyp == "ATY")
                {
                    Quer = "select E.Comments from YDMS_ActivityInvoiceHdr H left join YDMS_ActivityInvoiceHdrE_Z  E on E.AIH_PkHdrID = H.AIH_PkHdrID where  H.AIH_InvoiceNo ='" + InvoiceNumber + "'";
                } 
                else if (InvTyp == "WARR")
                {
                    Quer = "select E.Comments from ZDMS_WarrantyClaimInvoiceHeader H left join ZDMS_WarrantyClaimInvoiceE  E on E.WarrantyClaimInvoiceID = H.WarrantyClaimInvoiceID where  H.InvoiceNumber ='"+ InvoiceNumber + "'";
                }
                else if (InvTyp == "SalesCom")
                {
                    Quer = "select E.Comments from TSalesCommissionClaimInvoice H left join TSalesCommissionClaimInvoiceE  E on E.SalesCommissionClaimInvoiceID = H.SalesCommissionClaimInvoiceID where  H.InvoiceNumber ='" + InvoiceNumber + "'";
                }
                else if (InvTyp == "SalesInv")
                {
                    Quer = "select E.Comments from TSaleOrderDelivery H left join TSaleOrderDeliveryE  E on E.SaleOrderDeliveryID = H.SaleOrderDeliveryID where  H.InvoiceNumber ='" + InvoiceNumber + "'";

                }
                else if (InvTyp == "SalesReCre")
                {
                    Quer = "select E.Comments from TSaleOrderReturn H left join TSaleOrderReturnE  E on E.SaleOrderReturnID = H.SaleOrderReturnID where  H.CreditNoteNumber ='" + InvoiceNumber + "'";

                }

                using (DataSet DataSet = provider.SelectUsingQuery(Quer))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            return Convert.ToString(dr["Comments"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return "";
        }
    }
}