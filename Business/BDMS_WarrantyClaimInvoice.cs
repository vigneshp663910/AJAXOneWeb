using DataAccess;
using Properties; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Web;
using System.Web.UI;
using System.Configuration;
using Newtonsoft.Json;

namespace Business
{
    public class BDMS_WarrantyClaimInvoice
    {
        private IDataAccess provider;
        public BDMS_WarrantyClaimInvoice()
        {
            provider = new ProviderFactory().GetProvider();
        }
        //public long insertWarrantyClaimInvoice(PDMS_WarrantyClaimInvoice inv, int MonthRange, int UserID)
        //{

        //    int success = 0;
        //    long WarrantyClaimInvoiceID = 0;

        //    DbParameter DealerCodeP = provider.CreateParameter("DealerCode", inv.Dealer.DealerCode, DbType.String);
        //    DbParameter MonthRangeP = provider.CreateParameter("MonthRange", MonthRange, DbType.Int32);
        //    DbParameter Month = provider.CreateParameter("Month", inv.Month, DbType.Int32);
        //    DbParameter Year = provider.CreateParameter("Year", inv.Year, DbType.Int32);
        //    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

        //    DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
        //    DbParameter[] Params = new DbParameter[6] { DealerCodeP, MonthRangeP, Month, Year, WarrantyClaimInvoiceIDP, UserIDP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            success = provider.Insert("ZDMS_InsertWarrantyClaimInvoice", Params);
        //            WarrantyClaimInvoiceID = Convert.ToInt64(WarrantyClaimInvoiceIDP.Value);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", "insertWarrantyClaimInvoice", sqlEx);

        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", " insertWarrantyClaimInvoice", ex);

        //    }
        //    return WarrantyClaimInvoiceID;
        //}

        public long InsertWarrantyClaimInvoiceNEPI_Commission(PDMS_WarrantyClaimInvoice inv, int MonthRange, int UserID, PDMS_Customer Dealer, PDMS_Customer Customer)
        {

            int success = 0;
            long WarrantyClaimInvoiceID = 0;

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", inv.Dealer.DealerCode, DbType.String);
            DbParameter MonthRangeP = provider.CreateParameter("MonthRange", MonthRange, DbType.Int32);
            DbParameter Month = provider.CreateParameter("Month", inv.Month, DbType.Int32);
            DbParameter Year = provider.CreateParameter("Year", inv.Year, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            DbParameter SGSTIN = provider.CreateParameter("SupplierGSTIN", Dealer.GSTIN, DbType.String);
            DbParameter SAddr1 = provider.CreateParameter("Supplier_addr1", (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2)).Trim(','), DbType.String);
            DbParameter SAddr2 = provider.CreateParameter("Supplier_addr2", Dealer.Address3, DbType.String);
            DbParameter SLocation = provider.CreateParameter("SupplierLocation", Dealer.City, DbType.String);
            DbParameter SPincode = provider.CreateParameter("SupplierPincode", Dealer.Pincode, DbType.String);
            DbParameter SStateCode = provider.CreateParameter("SupplierStateCode", Dealer.State.StateCode, DbType.String);

            DbParameter BuyerName = provider.CreateParameter("BuyerName", Customer.CustomerName, DbType.String);
            DbParameter BGSTIN = provider.CreateParameter("BuyerGSTIN", Customer.GSTIN, DbType.String);
            DbParameter BStateCode = provider.CreateParameter("BuyerStateCode", Customer.State.StateCode, DbType.String);
            DbParameter BAddr1 = provider.CreateParameter("Buyer_addr1", Customer.Address1, DbType.String);
            DbParameter BAddr2 = provider.CreateParameter("Buyer_addr2", Customer.Address2, DbType.String);
            DbParameter BLoc = provider.CreateParameter("Buyer_loc", Customer.City, DbType.String);
            DbParameter BPincode = provider.CreateParameter("BuyerPincode", Customer.Pincode, DbType.String);


            DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[19] { DealerCodeP, MonthRangeP, Month, Year, WarrantyClaimInvoiceIDP, UserIDP, SGSTIN, SAddr1, SAddr2, SLocation, SPincode, SStateCode, BuyerName, BGSTIN, BStateCode, BAddr1, BAddr2, BLoc, BPincode };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertWarrantyClaimInvoiceNEPI_Commission", Params);
                    WarrantyClaimInvoiceID = Convert.ToInt64(WarrantyClaimInvoiceIDP.Value);
                    scope.Complete();
                }
                //   new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, InvNEPI_CommissionFile(WarrantyClaimInvoiceID));
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", "InsertWarrantyClaimInvoiceNEPI_Commission", sqlEx);

            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", " InsertWarrantyClaimInvoiceNEPI_Commission", ex);

            }
            return WarrantyClaimInvoiceID;
        }
        public long InsertWarrantyClaimInvoiceWarranty_Service(PDMS_WarrantyClaimInvoice inv, int MonthRange, int UserID, PDMS_Customer Dealer, PDMS_Customer Customer)
        {

            int success = 0;
            long WarrantyClaimInvoiceID = 0;

            decimal? TCSTax = null;
            if (PDMS_EInvoice.EInvoiveDate <= DateTime.Now) 
            {
                TCSTax = PDMS_EInvoice.TcsTax;

            }

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", inv.Dealer.DealerCode, DbType.String);
            DbParameter MonthRangeP = provider.CreateParameter("MonthRange", MonthRange, DbType.Int32);
            DbParameter Month = provider.CreateParameter("Month", inv.Month, DbType.Int32);
            DbParameter Year = provider.CreateParameter("Year", inv.Year, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            DbParameter TCSTaxP = provider.CreateParameter("TCSTax", TCSTax, DbType.Decimal);

            DbParameter SGSTIN = provider.CreateParameter("SupplierGSTIN", Dealer.GSTIN, DbType.String);
            DbParameter SAddr1 = provider.CreateParameter("Supplier_addr1", (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2)).Trim(','), DbType.String);
            DbParameter SAddr2 = provider.CreateParameter("Supplier_addr2", Dealer.Address3, DbType.String);
            DbParameter SLocation = provider.CreateParameter("SupplierLocation", Dealer.City, DbType.String);
            DbParameter SPincode = provider.CreateParameter("SupplierPincode", Dealer.Pincode, DbType.String);
            DbParameter SStateCode = provider.CreateParameter("SupplierStateCode", Dealer.State.StateCode, DbType.String);

            DbParameter BuyerName = provider.CreateParameter("BuyerName", Customer.CustomerName, DbType.String);
            DbParameter BGSTIN = provider.CreateParameter("BuyerGSTIN", Customer.GSTIN, DbType.String);
            DbParameter BStateCode = provider.CreateParameter("BuyerStateCode", Customer.State.StateCode, DbType.String);
            DbParameter BAddr1 = provider.CreateParameter("Buyer_addr1", (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2)).Trim(','), DbType.String);
            DbParameter BAddr2 = provider.CreateParameter("Buyer_addr2", Customer.Address3, DbType.String);
            DbParameter BLoc = provider.CreateParameter("Buyer_loc", Customer.City, DbType.String);
            DbParameter BPincode = provider.CreateParameter("BuyerPincode", Customer.Pincode, DbType.String);


            DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[20] { DealerCodeP, MonthRangeP, Month, Year, WarrantyClaimInvoiceIDP, UserIDP, TCSTaxP, SGSTIN, SAddr1, SAddr2, SLocation, SPincode, SStateCode, BuyerName, BGSTIN, BStateCode, BAddr1, BAddr2, BLoc, BPincode };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertWarrantyClaimInvoiceWarranty_Service", Params);
                    WarrantyClaimInvoiceID = Convert.ToInt64(WarrantyClaimInvoiceIDP.Value);
                    scope.Complete();
                }
                //   new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, InvWarranty_Service(WarrantyClaimInvoiceID));
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", "InsertWarrantyClaimInvoiceWarranty_Service", sqlEx);

            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", " InsertWarrantyClaimInvoiceWarranty_Service", ex);

            }
            return WarrantyClaimInvoiceID;
        }
        public long InsertWarrantyClaimInvoiceWarranty_ServicePartial(PDMS_WarrantyClaimInvoice inv, int MonthRange, int UserID, PDMS_Customer Dealer, PDMS_Customer Customer)
        {

            int success = 0;
            long WarrantyClaimInvoiceID = 0;

            //decimal? TCSTax = null;
            //if (PDMS_EInvoice.EInvoiveDate <= DateTime.Now)
            //{
            //    TCSTax = PDMS_EInvoice.TcsTax;

            //}

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", inv.Dealer.DealerCode, DbType.String);
            DbParameter MonthRangeP = provider.CreateParameter("MonthRange", MonthRange, DbType.Int32);
            DbParameter Month = provider.CreateParameter("Month", inv.Month, DbType.Int32);
            DbParameter Year = provider.CreateParameter("Year", inv.Year, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

          //  DbParameter TCSTaxP = provider.CreateParameter("TCSTax", TCSTax, DbType.Decimal);

            DbParameter SGSTIN = provider.CreateParameter("SupplierGSTIN", Dealer.GSTIN, DbType.String);
            DbParameter SAddr1 = provider.CreateParameter("Supplier_addr1", (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2)).Trim(','), DbType.String);
            DbParameter SAddr2 = provider.CreateParameter("Supplier_addr2", Dealer.Address3, DbType.String);
            DbParameter SLocation = provider.CreateParameter("SupplierLocation", Dealer.City, DbType.String);
            DbParameter SPincode = provider.CreateParameter("SupplierPincode", Dealer.Pincode, DbType.String);
            DbParameter SStateCode = provider.CreateParameter("SupplierStateCode", Dealer.State.StateCode, DbType.String);

            DbParameter BuyerName = provider.CreateParameter("BuyerName", Customer.CustomerName, DbType.String);
            DbParameter BGSTIN = provider.CreateParameter("BuyerGSTIN", Customer.GSTIN, DbType.String);
            DbParameter BStateCode = provider.CreateParameter("BuyerStateCode", Customer.State.StateCode, DbType.String);
            DbParameter BAddr1 = provider.CreateParameter("Buyer_addr1", (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2)).Trim(','), DbType.String);
            DbParameter BAddr2 = provider.CreateParameter("Buyer_addr2", Customer.Address3, DbType.String);
            DbParameter BLoc = provider.CreateParameter("Buyer_loc", Customer.City, DbType.String);
            DbParameter BPincode = provider.CreateParameter("BuyerPincode", Customer.Pincode, DbType.String);

            DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[19] { DealerCodeP, MonthRangeP, Month, Year, WarrantyClaimInvoiceIDP, UserIDP,  SGSTIN, SAddr1, SAddr2, SLocation, SPincode, SStateCode, BuyerName, BGSTIN, BStateCode, BAddr1, BAddr2, BLoc, BPincode };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertWarrantyClaimInvoiceWarranty_ServicePartial", Params);
                    WarrantyClaimInvoiceID = Convert.ToInt64(WarrantyClaimInvoiceIDP.Value);
                    scope.Complete();
                }
                //   new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, InvWarranty_Service(WarrantyClaimInvoiceID));
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", "InsertWarrantyClaimInvoiceWarranty_Service", sqlEx);

            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", " InsertWarrantyClaimInvoiceWarranty_Service", ex);

            }
            return WarrantyClaimInvoiceID;
        }

        public long InsertWarrantyClaimInvoiceAbove5K(string DealerCode, string ClaimNumber, int UserID, string reportPath, string Through, string LRNumber, PDMS_Customer Dealer, PDMS_Customer Customer, Boolean IsPartial)
        {
            int success = 0;
            long WarrantyClaimInvoiceID = 0;

            decimal? TCSTax = null;
            if (PDMS_EInvoice.EInvoiveDate <= DateTime.Now) 
            {
                TCSTax = PDMS_EInvoice.TcsTax;
            }

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", DealerCode, DbType.String);
            DbParameter ClaimNumberP = provider.CreateParameter("ClaimNumber", ClaimNumber, DbType.String);
            DbParameter ThroughP = provider.CreateParameter("Through", Through, DbType.String);
            DbParameter LRNumberP = provider.CreateParameter("LRNumber", LRNumber, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter TCSTaxP = provider.CreateParameter("TCSTax", TCSTax, DbType.Decimal);
            DbParameter SGSTIN = provider.CreateParameter("SupplierGSTIN", Dealer.GSTIN, DbType.String);
            DbParameter SAddr1 = provider.CreateParameter("Supplier_addr1", (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2)).Trim(','), DbType.String);
            DbParameter SAddr2 = provider.CreateParameter("Supplier_addr2", Dealer.Address3, DbType.String);
            DbParameter SLocation = provider.CreateParameter("SupplierLocation", Dealer.City, DbType.String);
            DbParameter SPincode = provider.CreateParameter("SupplierPincode", Dealer.Pincode, DbType.String);
            DbParameter SStateCode = provider.CreateParameter("SupplierStateCode", Dealer.State.StateCode, DbType.String);

            DbParameter BuyerName = provider.CreateParameter("BuyerName", Customer.CustomerName, DbType.String);
            DbParameter BGSTIN = provider.CreateParameter("BuyerGSTIN", Customer.GSTIN, DbType.String);
            DbParameter BStateCode = provider.CreateParameter("BuyerStateCode", Customer.State.StateCode, DbType.String);
            DbParameter BAddr1 = provider.CreateParameter("Buyer_addr1", Customer.Address1, DbType.String);
            DbParameter BAddr2 = provider.CreateParameter("Buyer_addr2", Customer.Address2, DbType.String);
            DbParameter BLoc = provider.CreateParameter("Buyer_loc", Customer.City, DbType.String);
            DbParameter BPincode = provider.CreateParameter("BuyerPincode", Customer.Pincode, DbType.String);



            DbParameter[] Params = new DbParameter[20] { DealerCodeP, ClaimNumberP, WarrantyClaimInvoiceIDP, ThroughP, LRNumberP, UserIDP, TCSTaxP, SGSTIN, SAddr1, SAddr2, SLocation, SPincode, SStateCode,BuyerName, BGSTIN, BStateCode, BAddr1, BAddr2, BLoc, BPincode };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //  if ((ServiceTypeID == (short)DMS_ServiceType.Warranty) || (ServiceTypeID == (short)DMS_ServiceType.PartsWarranty) || (ServiceTypeID == (short)DMS_ServiceType.GoodwillWarranty))
                    if (IsPartial)
                    {
                        success = provider.Insert("ZDMS_InsertWarrantyClaimInvoiceAbove5KPartial", Params);
                    }
                    else
                    {
                        success = provider.Insert("ZDMS_InsertWarrantyClaimInvoiceAbove5K", Params);
                    }

                    WarrantyClaimInvoiceID = Convert.ToInt64(WarrantyClaimInvoiceIDP.Value);
                    scope.Complete();
                }
                // new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, InvoiceAbove50K(WarrantyClaimInvoiceID));
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", "InsertWarrantyClaimInvoiceAbove5K", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", " InsertWarrantyClaimInvoiceAbove5K", ex);
            }
            return WarrantyClaimInvoiceID;
        }

        public List<PDMS_WarrantyClaimInvoice> getWarrantyClaimInvoice(long? WarrantyClaimInvoiceID, string DealerCode, int? Year, int? Month, int? MonthRange, int? InvoiceTypeID, string InvoiceNumber, Boolean? NotAccounted = null, Boolean? EInvoiceGenerated = null)
        {
            List<PDMS_WarrantyClaimInvoice> Ws = new List<PDMS_WarrantyClaimInvoice>();
            PDMS_WarrantyClaimInvoice W = null;

            DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("WarrantyClaimInvoiceID", WarrantyClaimInvoiceID, DbType.Int64);
            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter YearP = provider.CreateParameter("Year", Year, DbType.Int16);
            DbParameter MonthP = provider.CreateParameter("Month", Month, DbType.Int16);
            DbParameter MonthRangeP = provider.CreateParameter("MonthRange", MonthRange, DbType.Int16);
            DbParameter InvoiceTypeIDP = provider.CreateParameter("InvoiceTypeID", InvoiceTypeID, DbType.Int32);
            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
            DbParameter[] Params = new DbParameter[7] { WarrantyClaimInvoiceIDP, DealerCodeP, YearP, MonthP, MonthRangeP, InvoiceTypeIDP, InvoiceNumberP };
            try
            {
                long InvoiceID = 0;
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimInvoice", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["WarrantyClaimInvoiceID"]))
                            {
                                W = new PDMS_WarrantyClaimInvoice();
                                W.IRN = Convert.ToString(dr["IRN"]);
                                W.SAPDoc = Convert.ToString(dr["SAPDoc"]);
                                if (NotAccounted == true)
                                {
                                    if (!string.IsNullOrEmpty(W.SAPDoc))
                                    {
                                        continue;
                                    }
                                }

                                if (EInvoiceGenerated == true)
                                {
                                    if (string.IsNullOrEmpty(W.IRN))
                                    {
                                        continue;
                                    }
                                }
                                Ws.Add(W);
                                W.WarrantyClaimInvoiceID = Convert.ToInt64(dr["WarrantyClaimInvoiceID"]);
                                W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                W.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                                W.Dealer = new PDMS_Dealer() 
                                { 
                                    DealerCode = Convert.ToString(dr["UserName"]), 
                                    DealerName = Convert.ToString(dr["ContactName"]),
                                    IsEInvoice = DBNull.Value == dr["IsEInvoice"] ? false : Convert.ToBoolean(dr["IsEInvoice"]),
                                    EInvoiceDate= DBNull.Value == dr["EInvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["EInvoiceDate"]) ,
                                    //EInvAPI = DBNull.Value == dr["EInvAPI"] ? false : Convert.ToBoolean(dr["EInvAPI"]),
                                    //EInvUserAPI = new PEInvUserAPI()
                                    //{
                                    //    Handle = Convert.ToString(dr["EInvHandle"]),
                                    //    HandleType = Convert.ToString(dr["EInvHandleType"]),
                                    //    Password = Convert.ToString(dr["EInvPassword"]),
                                    //}
                                };
                                W.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);

                                W.TCSValue = DBNull.Value == dr["TCSValue"] ? 0 : Convert.ToDecimal(dr["TCSValue"]);
                                W.TCSTax = DBNull.Value == dr["TCSTax"] ? 0 : Convert.ToDecimal(dr["TCSTax"]);
                                W.Year = Convert.ToInt32(dr["Year"]);
                                W.Month = Convert.ToInt32(dr["Month"]);
                                W.MonthRange = Convert.ToInt32(dr["MonthRange"]);
                                W.AnnexureNumber = Convert.ToString(dr["AnnexureNumber"]);
                                W.PeriodFrom = DBNull.Value == dr["PeriodFrom"] ? (DateTime?)null : Convert.ToDateTime(dr["PeriodFrom"]);
                                W.PeriodTo = DBNull.Value == dr["PeriodTo"] ? (DateTime?)null : Convert.ToDateTime(dr["PeriodTo"]);
                                W.Through = Convert.ToString(dr["Through"]);
                                W.LRNumber = Convert.ToString(dr["LRNumber"]);
                              
                                W.IRNDate = DBNull.Value == dr["IRNDate"] ? (DateTime?)null : Convert.ToDateTime(dr["IRNDate"]);
                                W.InvoiceItems = new List<PDMS_WarrantyClaimInvoiceItem>();
                                InvoiceID = W.WarrantyClaimInvoiceID;
                               
                                W.SAPPostingDate = DBNull.Value == dr["SAPPostingDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SAPPostingDate"]);
                                W.SAPClearingDocument = Convert.ToString(dr["SAPClearingDocument"]);
                                W.SAPClearingDate = DBNull.Value == dr["SAPClearingDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SAPClearingDate"]);
                                W.SAPInvoiceValue = DBNull.Value == dr["SAPInvoiceValue"] ? (decimal?)null : Convert.ToDecimal(dr["SAPInvoiceValue"]);
                                W.SAPInvoiceTDSValue = DBNull.Value == dr["SAPInvoiceTDSValue"] ? (decimal?)null : Convert.ToDecimal(dr["SAPInvoiceTDSValue"]);
                                W.InvoiceType = new PDMS_WarrantyInvoiceType() { InvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"]), InvoiceType = Convert.ToString(dr["InvoiceType"]) };
                            }
                            W.InvoiceItems.Add(new PDMS_WarrantyClaimInvoiceItem()
                                {
                                    Material = Convert.ToString(dr["Material"]),
                                    MaterialDesc = Convert.ToString(dr["MaterialDesc"]),
                                    HSNCode = Convert.ToString(dr["HSNCode"]),
                                    Qty = Convert.ToInt32(dr["Qty"]),
                                    Rate = Convert.ToDecimal(dr["Rate"]),
                                    Category = Convert.ToString(dr["Category"]),
                                    ApprovedValue = Convert.ToDecimal(dr["ApprovedValue"]),
                                    Discount = Convert.ToDecimal(dr["Discount"]),
                                    TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                    CGST = Convert.ToDecimal(dr["CGST"]),
                                    SGST = Convert.ToDecimal(dr["SGST"]),
                                    IGST = Convert.ToDecimal(dr["IGST"]),
                                    CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                    SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                    IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                                    WarrantyClaimAnnexureItemID = Convert.ToInt64(dr["WarrantyClaimAnnexureItemID"])

                                });
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

        public void insertWarrantyClaimInvoiceFile(long WarrantyClaimInvoiceID, PAttachedFile InvFile)
        {
            DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("WarrantyClaimInvoiceID", WarrantyClaimInvoiceID, DbType.String);
            DbParameter InvFileP = provider.CreateParameter("InvFile", InvFile.AttachedFile, DbType.Binary);
            DbParameter FileTypeP = provider.CreateParameter("FileType", InvFile.FileType, DbType.String);
            DbParameter[] Params = new DbParameter[3] { WarrantyClaimInvoiceIDP, InvFileP, FileTypeP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertWarrantyClaimInvoiceFile", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", "insertWarrantyClaimInvoiceFile", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", " insertWarrantyClaimInvoiceFile", ex);
            }
        }
        public PAttachedFile GetWarrantyClaimInvoiceFile(long WarrantyClaimInvoiceID)
        {
            DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("WarrantyClaimInvoiceID", WarrantyClaimInvoiceID, DbType.Int64);
            PAttachedFile Files = null;
            try
            {
                string endPoint = "Warranty/GetWarrantyClaimInvoiceFile?WarrantyClaimInvoiceID=" + WarrantyClaimInvoiceID;
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                Files = JsonConvert.DeserializeObject<PAttachedFile>(JsonConvert.SerializeObject(Result.Data));

               
                if (Files == null)
                {
                    PDMS_WarrantyClaimInvoice SOIs = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(WarrantyClaimInvoiceID, "", null, null, null, null, "")[0];
                    if (SOIs.InvoiceType.InvoiceTypeID == 1)
                    {
                        new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, InvNEPI_CommissionFile(WarrantyClaimInvoiceID));
                    }
                    else if (SOIs.InvoiceType.InvoiceTypeID == 2)
                    {
                        new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, InvWarranty_Service(WarrantyClaimInvoiceID));
                    }
                    else if (SOIs.InvoiceType.InvoiceTypeID == 3)
                    {
                        new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, InvoiceAbove50K(WarrantyClaimInvoiceID));
                    }
                    else if (SOIs.InvoiceType.InvoiceTypeID == (short)DMS_InvoiceType.Warranty_ServicePartial)
                    {
                        new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, InvWarranty_ServicePartial(WarrantyClaimInvoiceID));
                    }
                    else if (SOIs.InvoiceType.InvoiceTypeID == (short)DMS_InvoiceType.Above50kPartial)
                    {
                        new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, InvoiceAbove50kPartial(WarrantyClaimInvoiceID));
                    }
                    //else if (SOIs.InvoiceType.InvoiceTypeID == 4)
                    //{
                    //    new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, DebitFile(WarrantyClaimInvoiceID));
                    //}
                }
                return Files;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", "GetWarrantyClaimInvoiceFile", ex);
                return null;
            }

        }

        public List<PDMS_WarrantyInvoiceType> GetWarrantyInvoiceType()
        {
            List<PDMS_WarrantyInvoiceType> Ws = new List<PDMS_WarrantyInvoiceType>();

            try
            {

                using (DataSet ataSet = provider.Select("ZDMS_GetWarrantyInvoiceType"))
                {
                    if (ataSet != null)
                    {
                        foreach (DataRow dr in ataSet.Tables[0].Rows)
                        {
                            Ws.Add(new PDMS_WarrantyInvoiceType()
                            {
                                InvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"]),
                                InvoiceType = Convert.ToString(dr["InvoiceType"])
                            });
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

        private PAttachedFile InvoiceAbove50K(long WarrantyClaimInvoiceID)
        {
            try
            {
                PDMS_WarrantyClaimInvoice ClaimInvoice = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(WarrantyClaimInvoiceID, "", null, null, null, 3, "")[0];
                //PDMS_Customer Dealer = new SCustomer().getCustomerAddress(ClaimInvoice.Dealer.DealerCode);
                PDMS_Customer Dealer = new BDMS_Customer().getCustomerAddressFromSAP(ClaimInvoice.Dealer.DealerCode);
                string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

                //   PDMS_WarrantyInvoiceHeader WarrantyInvoiceHeader = new BDMS_WarrantyClaim().GetWarrantyClaimReport("", null, null, ClaimInvoice.AnnexureNumber, null, null, "", null, null, null, "","","", false, 1)[0];

                PDMS_WarrantyInvoiceHeader_1 WarrantyInvoiceHeader = new BDMS_WarrantyClaim().GetWarrantyClaimHeader(null, null, ClaimInvoice.AnnexureNumber)[0];
               // string CustomerCode = "", CustomerName="", ICTicketID="", Model="", HMR="", MachineSerialNumber="";
               // DateTime ICTicketDate = DateTime.Now;

                //PDMS_Customer Customer = new SCustomer().getCustomerAddress(WarrantyInvoiceHeader.CustomerCode);

                // PDMS_Customer Customer = new BDMS_Customer().getCustomerAddressFromSAP(WarrantyInvoiceHeader.CustomerCode);
                PDMS_Customer Customer = new BDMS_Customer().getCustomerAddressFromSAP(WarrantyInvoiceHeader.CustomerCode);
                string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("Rate");
                CommissionDT.Columns.Add("Value", typeof(decimal));
                CommissionDT.Columns.Add("CGST");
                CommissionDT.Columns.Add("SGST");
                CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                //  decimal GrandTotal = 0;
                string StateCode = Dealer.State.StateCode;
                string GST_Header = "";
                int i = 0;
                decimal TCSSubTotal = 0;
                foreach (PDMS_WarrantyClaimInvoiceItem item in ClaimInvoice.InvoiceItems)
                {

                    i = i + 1;
                    if (item.SGST != 0)
                    {
                        GST_Header = "CGST & SGST";
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.ApprovedValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.ApprovedValue + item.CGSTValue + item.SGSTValue);
                        TCSSubTotal = TCSSubTotal + item.ApprovedValue + item.CGSTValue + item.SGSTValue;
                    }
                    else
                    {
                        GST_Header = "IGST";
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.ApprovedValue, item.IGST, null, item.IGSTValue, null, item.ApprovedValue + item.IGSTValue);
                        TCSSubTotal = TCSSubTotal + item.ApprovedValue + item.IGSTValue;
                    }
                }

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

                ReportParameter[] P = null;
                if ((ClaimInvoice.Dealer.IsEInvoice) && (ClaimInvoice.Dealer.EInvoiceDate <= ClaimInvoice.InvoiceDate))
                {
                    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getWarrantyClaimInvoiceESigned(WarrantyClaimInvoiceID); 
                    P = new ReportParameter[33];
                    P[31] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, ClaimInvoice.InvoiceNumber), false);
                    P[32] = new ReportParameter("IRN", "IRN : " + ClaimInvoice.IRN, false);
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_ClaimInvoice50KQRCode.rdlc");
                }
                else
                {
                    P = new ReportParameter[31];
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_ClaimInvoice50K.rdlc");
                }


                //   ViewState["Month"] = ddlMonth.SelectedValue;
                P[0] = new ReportParameter("DealerCode", ClaimInvoice.Dealer.DealerCode, false);
                P[1] = new ReportParameter("DealerName", ClaimInvoice.Dealer.DealerName, false);
                P[2] = new ReportParameter("Address1", DealerAddress1, false);
                P[3] = new ReportParameter("Address2", DealerAddress2, false);
                P[4] = new ReportParameter("Contact", "Contact", false);
                P[5] = new ReportParameter("GSTIN", Dealer.GSTIN, false);
                P[6] = new ReportParameter("GST_Header", GST_Header, false);
                P[7] = new ReportParameter("GrandTotal", (ClaimInvoice.GrandTotal).ToString(), false);
                P[8] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(ClaimInvoice.GrandTotal)), false);
                P[9] = new ReportParameter("InvoiceNumber", ClaimInvoice.InvoiceNumber, false);

                P[10] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                //P[11] = new ReportParameter("CustomerName", WarrantyInvoiceHeader.CustomerName, false);
                P[11] = new ReportParameter("CustomerName", WarrantyInvoiceHeader.CustomerName, false);
                P[12] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                P[13] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                P[14] = new ReportParameter("CustomerMail", Customer.Email, false);
                P[15] = new ReportParameter("CustomerStateCode", Customer.GSTIN.Length > 2 ? Customer.GSTIN.Substring(0, 2) : "", false);
                P[16] = new ReportParameter("CustomerGST", Customer.GSTIN, false);
                // P[17] = new ReportParameter("ICTicketNo", WarrantyInvoiceHeader.ICTicketID, false);
                P[17] = new ReportParameter("ICTicketNo", WarrantyInvoiceHeader.ICTicket.ICTicketNumber, false);
                //  P[18] = new ReportParameter("Model", WarrantyInvoiceHeader.Model, false);
                P[18] = new ReportParameter("Model", WarrantyInvoiceHeader.Model, false);
                P[19] = new ReportParameter("DateOfComm", "", false);
                 P[20] = new ReportParameter("HMR", Convert.ToString(WarrantyInvoiceHeader.HMR), false); 
                P[21] = new ReportParameter("Through", ClaimInvoice.Through, false);
                P[22] = new ReportParameter("LR", ClaimInvoice.LRNumber, false);
               // P[23] = new ReportParameter("TSIR", WarrantyInvoiceHeader.TSIRNumber, false);
                P[23] = new ReportParameter("TSIR", "", false);
                //P[24] = new ReportParameter("MachineSerialNo", WarrantyInvoiceHeader.MachineSerialNumber, false);
                P[24] = new ReportParameter("MachineSerialNo", WarrantyInvoiceHeader.MachineSerialNumber, false);
               // P[25] = new ReportParameter("DateOfFailure", ((DateTime)WarrantyInvoiceHeader.ICTicketDate).ToShortDateString(), false);
                P[25] = new ReportParameter("DateOfFailure", WarrantyInvoiceHeader.ICTicket.ICTicketDate.ToShortDateString(), false);
                P[26] = new ReportParameter("InvDate", ((DateTime)ClaimInvoice.InvoiceDate).ToShortDateString(), false);
                DateTime NewLogoDate = Convert.ToDateTime(ConfigurationManager.AppSettings["NewLogoDate"]);
                string NewLogo = "0";
                if (NewLogoDate <= ClaimInvoice.InvoiceDate)
                {
                    NewLogo = "1";
                }
                P[27] = new ReportParameter("NewLogo", NewLogo, false);

                P[28] = new ReportParameter("TCSValue", Convert.ToString(ClaimInvoice.TCSValue), false);
                P[29] = new ReportParameter("TCSSubTotal", Convert.ToString(TCSSubTotal + ClaimInvoice.TCSValue), false);
                P[30] = new ReportParameter("TCSTax", Convert.ToString(ClaimInvoice.TCSTax), false);



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
                return InvF;
            }
            catch (Exception ex)
            {
                //  lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                //   lblMessage.ForeColor = Color.Red;
                //  lblMessage.Visible = true;
            }
            return null;
        }
        private PAttachedFile InvNEPI_CommissionFile(long WarrantyClaimInvoiceID)
        {
            try
            {

                PDMS_WarrantyClaimInvoice ClaimInvoice = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(WarrantyClaimInvoiceID, "", null, null, null, 1, "")[0];

                //      List<PDMS_WarrantyClaimInvoice> ClaimInvoice = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(null, DealerCode, Convert.ToInt32(Year), Convert.ToInt32(Month), Convert.ToInt32(MonthRange), Convert.ToInt32(InvoiceTypeID), "");              

                PDMS_Customer Dealer = new BDMS_Customer().getCustomerAddressFromSAP(ClaimInvoice.Dealer.DealerCode);
                string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');


                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Category");
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                CommissionDT.Columns.Add("CGST");
                CommissionDT.Columns.Add("SGST");
                CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("NoOfCalls");
                string GST_Header = "";

                //  PDMS_WarrantyClaimAnnexureHeader AnnexureH = new BDMS_WarrantyClaimAnnexure().GetWarrantyClaimAnnexureReport(null, null, null, null, null, null, "")[0];


                foreach (PDMS_WarrantyClaimInvoiceItem item in ClaimInvoice.InvoiceItems)
                {
                    if (item.Category == "Commissioning")
                    {
                        if (item.SGST != 0)
                        {
                            GST_Header = "CGST & SGST";
                            CommissionDT.Rows.Add(1, item.Category, item.ApprovedValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.Qty);
                        }
                        else
                        {
                            GST_Header = "IGST";
                            CommissionDT.Rows.Add(1, item.Category, item.ApprovedValue, item.IGST, null, item.IGSTValue, null, item.Qty);
                        }
                    }
                }
                foreach (PDMS_WarrantyClaimInvoiceItem item in ClaimInvoice.InvoiceItems)
                {
                    if (item.Category == "NEPI")
                    {
                        if (item.SGST != 0)
                        {
                            GST_Header = "CGST & SGST";
                            CommissionDT.Rows.Add(CommissionDT.Rows.Count + 1, item.Category, item.ApprovedValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.Qty);
                        }
                        else
                        {
                            GST_Header = "IGST";
                            CommissionDT.Rows.Add(CommissionDT.Rows.Count + 1, item.Category, item.ApprovedValue, item.IGST, null, item.IGSTValue, null, item.Qty);
                        }
                    }
                }

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
                
                ReportParameter[] P = null;
                if ((ClaimInvoice.Dealer.IsEInvoice) && (ClaimInvoice.Dealer.EInvoiceDate <= ClaimInvoice.InvoiceDate))
                {
                    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getWarrantyClaimInvoiceESigned(WarrantyClaimInvoiceID);
                    P = new ReportParameter[16];
                    P[14] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, ClaimInvoice.InvoiceNumber), false);
                    P[15] = new ReportParameter("IRN", "IRN : " + ClaimInvoice.IRN, false);
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_ClaimInvoiceNCQRCode.rdlc");
                }
                else
                {
                    P = new ReportParameter[14];
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_ClaimInvoiceNC.rdlc");
                }


                P[0] = new ReportParameter("DealerCode", ClaimInvoice.Dealer.DealerCode, false);
                P[1] = new ReportParameter("Annexure", ClaimInvoice.AnnexureNumber, false);
                P[2] = new ReportParameter("DateOfClaim", ClaimInvoice.InvoiceDate.ToShortDateString(), false);
                P[3] = new ReportParameter("DealerName", ClaimInvoice.Dealer.DealerName, false);
                P[4] = new ReportParameter("Address1", DealerAddress1, false);
                P[5] = new ReportParameter("Address2", DealerAddress2, false);
                P[6] = new ReportParameter("Contact", "Contact", false);
                P[7] = new ReportParameter("GSTIN", Dealer.GSTIN, false);
                P[8] = new ReportParameter("GST_Header", GST_Header, false);
                P[9] = new ReportParameter("GrandTotal", (ClaimInvoice.GrandTotal).ToString(), false);
                P[10] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(ClaimInvoice.GrandTotal), false);
                P[11] = new ReportParameter("InvoiceNumber", ClaimInvoice.InvoiceNumber, false);
                P[12] = new ReportParameter("PAN", Dealer.PAN, false);

                DateTime NewLogoDate = Convert.ToDateTime(ConfigurationManager.AppSettings["NewLogoDate"]);
                string NewLogo = "0";
                if (NewLogoDate <= ClaimInvoice.InvoiceDate)
                {
                    NewLogo = "1";
                }
                P[13] = new ReportParameter("NewLogo", NewLogo, false);
                 
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
                return InvF;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        private PAttachedFile InvWarranty_Service(long WarrantyClaimInvoiceID)
        {
            try
            {
                PDMS_WarrantyClaimInvoice ClaimInvoice = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(WarrantyClaimInvoiceID, "", null, null, null, 2, "")[0];

                PDMS_Customer Dealer = new BDMS_Customer().getCustomerAddressFromSAP(ClaimInvoice.Dealer.DealerCode);
                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("Rate");
                CommissionDT.Columns.Add("Value", typeof(decimal));
                CommissionDT.Columns.Add("CGST");
                CommissionDT.Columns.Add("SGST");
                CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                //  decimal GrandTotal = 0;
                //    string StateCode = DealerOffice.StateCode;
                string GST_Header = "";


                //PDMS_WarrantyClaimAnnexureHeader AnnexureH = new PDMS_WarrantyClaimAnnexureHeader();
                //AnnexureH = SDMS_WarrantyClaimHeader;
                int i = 0;
                decimal TCSSubTotal = 0;
                foreach (PDMS_WarrantyClaimInvoiceItem item in ClaimInvoice.InvoiceItems)
                {
                    if (item.Category == "Warranty Material")
                    {
                        i = i + 1;
                        if (item.SGST != 0)
                        {

                            GST_Header = "CGST & SGST";
                            CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.ApprovedValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.ApprovedValue + item.CGSTValue + item.SGSTValue);

                            TCSSubTotal = TCSSubTotal + item.ApprovedValue + item.CGSTValue + item.SGSTValue;
                        }
                        else
                        {
                            GST_Header = "IGST";
                            CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.ApprovedValue, item.IGST, null, item.IGSTValue, null, item.ApprovedValue + item.IGSTValue);
                            TCSSubTotal = TCSSubTotal + item.ApprovedValue + item.IGSTValue;
                        }
                    }
                }
                foreach (PDMS_WarrantyClaimInvoiceItem item in ClaimInvoice.InvoiceItems)
                {
                   // if (item.Category == "Labour Charges")
                    if (item.Category != "Warranty Material")
                    {
                        i = i + 1;
                        if (item.SGST != 0)
                        {

                            GST_Header = "CGST & SGST";
                            CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, "", item.ApprovedValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.ApprovedValue + item.CGSTValue + item.SGSTValue);
                            TCSSubTotal = TCSSubTotal + item.ApprovedValue + item.CGSTValue + item.SGSTValue;
                        }
                        else
                        {
                            GST_Header = "IGST";
                            CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, "", item.ApprovedValue, item.IGST, null, item.IGSTValue, null, item.ApprovedValue + item.IGSTValue);
                            TCSSubTotal = TCSSubTotal + item.ApprovedValue + item.IGSTValue;
                        }
                    }
                }


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
                 
                ReportParameter[] P = null;
                if ((ClaimInvoice.Dealer.IsEInvoice) && (ClaimInvoice.Dealer.EInvoiceDate <= ClaimInvoice.InvoiceDate))
                {
                    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getWarrantyClaimInvoiceESigned(WarrantyClaimInvoiceID);
                    P = new ReportParameter[21];
                    P[19] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, ClaimInvoice.InvoiceNumber), false);
                    P[20] = new ReportParameter("IRN", "IRN : " + ClaimInvoice.IRN, false);
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_ClaimInvoiceWSQRCode.rdlc");
                }
                else
                {
                    P = new ReportParameter[19];
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_ClaimInvoiceWS.rdlc");
                }

                //   ViewState["Month"] = ddlMonth.SelectedValue;
                P[0] = new ReportParameter("DealerCode", ClaimInvoice.Dealer.DealerCode, false);
                P[1] = new ReportParameter("Annexure", ClaimInvoice.AnnexureNumber, false);
                P[2] = new ReportParameter("DateOfClaim", ClaimInvoice.InvoiceDate.ToShortDateString(), false);
                P[3] = new ReportParameter("DealerName", ClaimInvoice.Dealer.DealerName, false);
                P[4] = new ReportParameter("Address1", Dealer.Address1, false);
                P[5] = new ReportParameter("Address2", Dealer.Address2, false);
                P[6] = new ReportParameter("Contact", "Contact", false);
                P[7] = new ReportParameter("GSTIN", Dealer.GSTIN, false);
                P[8] = new ReportParameter("GST_Header", GST_Header, false);
                P[9] = new ReportParameter("GrandTotal", (ClaimInvoice.GrandTotal).ToString(), false);
                P[10] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(ClaimInvoice.GrandTotal)), false);
                P[11] = new ReportParameter("InvoiceNumber", ClaimInvoice.InvoiceNumber, false);
                P[12] = new ReportParameter("PeriodFrom", ((DateTime)ClaimInvoice.PeriodFrom).ToShortDateString(), false);
                P[13] = new ReportParameter("PeriodTo", ((DateTime)ClaimInvoice.PeriodTo).ToShortDateString(), false);
                P[14] = new ReportParameter("PAN", Dealer.PAN, false);
                DateTime NewLogoDate = Convert.ToDateTime(ConfigurationManager.AppSettings["NewLogoDate"]);
                string NewLogo = "0";
                if (NewLogoDate <= ClaimInvoice.InvoiceDate)
                {
                    NewLogo = "1";
                }
                P[15] = new ReportParameter("NewLogo", NewLogo, false);
                P[16] = new ReportParameter("TCSValue",Convert.ToString( ClaimInvoice.TCSValue), false);
                P[17] = new ReportParameter("TCSSubTotal", Convert.ToString(TCSSubTotal + ClaimInvoice.TCSValue), false);
                P[18] = new ReportParameter("TCSTax", Convert.ToString(ClaimInvoice.TCSTax), false); 

                


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
                return InvF;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        private PAttachedFile InvWarranty_ServicePartial(long WarrantyClaimInvoiceID)
        {
            try
            {
                PDMS_WarrantyClaimInvoice ClaimInvoice = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(WarrantyClaimInvoiceID, "", null, null, null, 5, "")[0];

                PDMS_Customer Dealer = new BDMS_Customer().getCustomerAddressFromSAP(ClaimInvoice.Dealer.DealerCode);
                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("Rate");
                CommissionDT.Columns.Add("Value", typeof(decimal));
                CommissionDT.Columns.Add("CGST");
                CommissionDT.Columns.Add("SGST");
                CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                //  decimal GrandTotal = 0;
                //    string StateCode = DealerOffice.StateCode;
                string GST_Header = "";


                //PDMS_WarrantyClaimAnnexureHeader AnnexureH = new PDMS_WarrantyClaimAnnexureHeader();
                //AnnexureH = SDMS_WarrantyClaimHeader;
                int i = 0;
                decimal TCSSubTotal = 0;
                foreach (PDMS_WarrantyClaimInvoiceItem item in ClaimInvoice.InvoiceItems)
                {
                    i = i + 1;
                    if (item.SGST != 0)
                    {

                        GST_Header = "CGST & SGST";
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.ApprovedValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.ApprovedValue + item.CGSTValue + item.SGSTValue);

                        TCSSubTotal = TCSSubTotal + item.ApprovedValue + item.CGSTValue + item.SGSTValue;
                    }
                    else
                    {
                        GST_Header = "IGST";
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.ApprovedValue, item.IGST, null, item.IGSTValue, null, item.ApprovedValue + item.IGSTValue);
                        TCSSubTotal = TCSSubTotal + item.ApprovedValue + item.IGSTValue;
                    }
                }          


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

                ReportParameter[] P = null;
                if ((ClaimInvoice.Dealer.IsEInvoice) && (ClaimInvoice.Dealer.EInvoiceDate <= ClaimInvoice.InvoiceDate))
                {
                    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getWarrantyClaimInvoiceESigned(WarrantyClaimInvoiceID);
                    P = new ReportParameter[21];
                    P[19] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, ClaimInvoice.InvoiceNumber), false);
                    P[20] = new ReportParameter("IRN", "IRN : " + ClaimInvoice.IRN, false);
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_ClaimInvoiceWSQRCode.rdlc");
                }
                else
                {
                    P = new ReportParameter[19];
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_ClaimInvoiceWS.rdlc");
                }

                //   ViewState["Month"] = ddlMonth.SelectedValue;
                P[0] = new ReportParameter("DealerCode", ClaimInvoice.Dealer.DealerCode, false);
                P[1] = new ReportParameter("Annexure", ClaimInvoice.AnnexureNumber, false);
                P[2] = new ReportParameter("DateOfClaim", ClaimInvoice.InvoiceDate.ToShortDateString(), false);
                P[3] = new ReportParameter("DealerName", ClaimInvoice.Dealer.DealerName, false);
                P[4] = new ReportParameter("Address1", Dealer.Address1, false);
                P[5] = new ReportParameter("Address2", Dealer.Address2, false);
                P[6] = new ReportParameter("Contact", "Contact", false);
                P[7] = new ReportParameter("GSTIN", Dealer.GSTIN, false);
                P[8] = new ReportParameter("GST_Header", GST_Header, false);
                P[9] = new ReportParameter("GrandTotal", (ClaimInvoice.GrandTotal).ToString(), false);
                P[10] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(ClaimInvoice.GrandTotal)), false);
                P[11] = new ReportParameter("InvoiceNumber", ClaimInvoice.InvoiceNumber, false);
                P[12] = new ReportParameter("PeriodFrom", ((DateTime)ClaimInvoice.PeriodFrom).ToShortDateString(), false);
                P[13] = new ReportParameter("PeriodTo", ((DateTime)ClaimInvoice.PeriodTo).ToShortDateString(), false);
                P[14] = new ReportParameter("PAN", Dealer.PAN, false);
                DateTime NewLogoDate = Convert.ToDateTime(ConfigurationManager.AppSettings["NewLogoDate"]);
                string NewLogo = "0";
                if (NewLogoDate <= ClaimInvoice.InvoiceDate)
                {
                    NewLogo = "1";
                }
                P[15] = new ReportParameter("NewLogo", NewLogo, false);
                P[16] = new ReportParameter("TCSValue", Convert.ToString(ClaimInvoice.TCSValue), false);
                P[17] = new ReportParameter("TCSSubTotal", Convert.ToString(TCSSubTotal + ClaimInvoice.TCSValue), false);
                P[18] = new ReportParameter("TCSTax", Convert.ToString(ClaimInvoice.TCSTax), false);




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
                return InvF;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        private PAttachedFile InvoiceAbove50kPartial(long WarrantyClaimInvoiceID)
        {
            try
            {
                PDMS_WarrantyClaimInvoice ClaimInvoice = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(WarrantyClaimInvoiceID, "", null, null, null, 6, "")[0];
                PDMS_Customer Dealer = new BDMS_Customer().getCustomerAddressFromSAP(ClaimInvoice.Dealer.DealerCode);
                string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

                //  PDMS_WarrantyInvoiceHeader WarrantyInvoiceHeader = new BDMS_WarrantyClaim().GetWarrantyClaimReport("", null, null, ClaimInvoice.AnnexureNumber, null, null, "", null, null, null, "", "", "", false, 1)[0];
                PDMS_WarrantyInvoiceHeader_1 WarrantyInvoiceHeader = new BDMS_WarrantyClaim().GetWarrantyClaimHeader(null, null, ClaimInvoice.AnnexureNumber)[0];
                PDMS_Customer Customer = new BDMS_Customer().getCustomerAddressFromSAP(WarrantyInvoiceHeader.CustomerCode);
                string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("Rate");
                CommissionDT.Columns.Add("Value", typeof(decimal));
                CommissionDT.Columns.Add("CGST");
                CommissionDT.Columns.Add("SGST");
                CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                //  decimal GrandTotal = 0;
                string StateCode = Dealer.State.StateCode;
                string GST_Header = "";
                int i = 0;
                decimal TCSSubTotal = 0;
                foreach (PDMS_WarrantyClaimInvoiceItem item in ClaimInvoice.InvoiceItems)
                {

                    i = i + 1;
                    if (item.SGST != 0)
                    {
                        GST_Header = "CGST & SGST";
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.ApprovedValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.ApprovedValue + item.CGSTValue + item.SGSTValue);
                        TCSSubTotal = TCSSubTotal + item.ApprovedValue + item.CGSTValue + item.SGSTValue;
                    }
                    else
                    {
                        GST_Header = "IGST";
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.ApprovedValue, item.IGST, null, item.IGSTValue, null, item.ApprovedValue + item.IGSTValue);
                        TCSSubTotal = TCSSubTotal + item.ApprovedValue + item.IGSTValue;
                    }
                }

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

                ReportParameter[] P = null;
                if ((ClaimInvoice.Dealer.IsEInvoice) && (ClaimInvoice.Dealer.EInvoiceDate <= ClaimInvoice.InvoiceDate))
                {
                    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getWarrantyClaimInvoiceESigned(WarrantyClaimInvoiceID);
                    P = new ReportParameter[33];
                    P[31] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, ClaimInvoice.InvoiceNumber), false);
                    P[32] = new ReportParameter("IRN", "IRN : " + ClaimInvoice.IRN, false);
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_ClaimInvoice50KQRCode.rdlc");
                }
                else
                {
                    P = new ReportParameter[31];
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_ClaimInvoice50K.rdlc");
                }


                //   ViewState["Month"] = ddlMonth.SelectedValue;
                P[0] = new ReportParameter("DealerCode", ClaimInvoice.Dealer.DealerCode, false);
                P[1] = new ReportParameter("DealerName", ClaimInvoice.Dealer.DealerName, false);
                P[2] = new ReportParameter("Address1", DealerAddress1, false);
                P[3] = new ReportParameter("Address2", DealerAddress2, false);
                P[4] = new ReportParameter("Contact", "Contact", false);
                P[5] = new ReportParameter("GSTIN", Dealer.GSTIN, false);
                P[6] = new ReportParameter("GST_Header", GST_Header, false);
                P[7] = new ReportParameter("GrandTotal", (ClaimInvoice.GrandTotal).ToString(), false);
                P[8] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(ClaimInvoice.GrandTotal)), false);
                P[9] = new ReportParameter("InvoiceNumber", ClaimInvoice.InvoiceNumber, false);

                P[10] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                P[11] = new ReportParameter("CustomerName", WarrantyInvoiceHeader.CustomerName, false);
                P[12] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                P[13] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                P[14] = new ReportParameter("CustomerMail", Customer.Email, false);
                P[15] = new ReportParameter("CustomerStateCode", Customer.GSTIN.Length > 2 ? Customer.GSTIN.Substring(0, 2) : "", false);
                P[16] = new ReportParameter("CustomerGST", Customer.GSTIN, false);
                P[17] = new ReportParameter("ICTicketNo", WarrantyInvoiceHeader.ICTicket.ICTicketNumber, false);
                P[18] = new ReportParameter("Model", WarrantyInvoiceHeader.Model, false);
                P[19] = new ReportParameter("DateOfComm", "", false);
                P[20] = new ReportParameter("HMR", Convert.ToString(WarrantyInvoiceHeader.HMR), false);
                P[21] = new ReportParameter("Through", ClaimInvoice.Through, false);
                P[22] = new ReportParameter("LR", ClaimInvoice.LRNumber, false);
                //  P[23] = new ReportParameter("TSIR", WarrantyInvoiceHeader.TSIRNumber, false);
                P[23] = new ReportParameter("TSIR", "", false);
                P[24] = new ReportParameter("MachineSerialNo", WarrantyInvoiceHeader.MachineSerialNumber, false);
                P[25] = new ReportParameter("DateOfFailure", WarrantyInvoiceHeader.ICTicket.ICTicketDate.ToShortDateString(), false);
                P[26] = new ReportParameter("InvDate", ((DateTime)ClaimInvoice.InvoiceDate).ToShortDateString(), false);
                DateTime NewLogoDate = Convert.ToDateTime(ConfigurationManager.AppSettings["NewLogoDate"]);
                string NewLogo = "0";
                if (NewLogoDate <= ClaimInvoice.InvoiceDate)
                {
                    NewLogo = "1";
                }
                P[27] = new ReportParameter("NewLogo", NewLogo, false);

                P[28] = new ReportParameter("TCSValue", Convert.ToString(ClaimInvoice.TCSValue), false);
                P[29] = new ReportParameter("TCSSubTotal", Convert.ToString(TCSSubTotal + ClaimInvoice.TCSValue), false);
                P[30] = new ReportParameter("TCSTax", Convert.ToString(ClaimInvoice.TCSTax), false);



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
                return InvF;
            }
            catch (Exception ex)
            {
                //  lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                //   lblMessage.ForeColor = Color.Red;
                //  lblMessage.Visible = true;
            }
            return null;
        }
 

        public List<PDMS_WarrantyClaimInvoice> GetWarrantyClaimInvoiceByMonthAndMonthRange(String DealerCode, int? Year, int? Month, int? MonthRange, int? InvoiceTypeID)
        {
            List<PDMS_WarrantyClaimInvoice> Ws = new List<PDMS_WarrantyClaimInvoice>();
            PDMS_WarrantyClaimInvoice W = null;

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter YearP = provider.CreateParameter("Year", Year, DbType.Int16);
            DbParameter MonthP = provider.CreateParameter("Month", Month, DbType.Int16);
            DbParameter MonthRangeP = provider.CreateParameter("MonthRange", MonthRange, DbType.Int16);
            DbParameter InvoiceTypeIDP = provider.CreateParameter("InvoiceTypeID", InvoiceTypeID, DbType.Int32);
            DbParameter[] Params = new DbParameter[5] { DealerCodeP, YearP, MonthP, MonthRangeP, InvoiceTypeIDP };
            try
            {
                long InvoiceID = 0;
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimInvoiceByMonthAndMonthRange", Params))
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
                                W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["UserName"]), DealerName = Convert.ToString(dr["ContactName"]) };
                                W.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                W.Year = Convert.ToInt32(dr["Year"]);
                                W.Month = Convert.ToInt32(dr["Month"]);
                                W.MonthRange = Convert.ToInt32(dr["MonthRange"]);
                                W.AnnexureNumber = Convert.ToString(dr["AnnexureNumber"]);
                                W.PeriodFrom = DBNull.Value == dr["PeriodFrom"] ? (DateTime?)null : Convert.ToDateTime(dr["PeriodFrom"]);
                                W.PeriodTo = DBNull.Value == dr["PeriodTo"] ? (DateTime?)null : Convert.ToDateTime(dr["PeriodTo"]);
                                W.Through = Convert.ToString(dr["Through"]);
                                W.LRNumber = Convert.ToString(dr["LRNumber"]);
                                W.InvoiceItems = new List<PDMS_WarrantyClaimInvoiceItem>();
                                InvoiceID = W.WarrantyClaimInvoiceID;
                                W.SAPDoc = Convert.ToString(dr["SAPDoc"]);
                                W.SAPPostingDate = DBNull.Value == dr["SAPPostingDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SAPPostingDate"]);
                                W.SAPClearingDocument = Convert.ToString(dr["SAPClearingDocument"]);
                                W.SAPClearingDate = DBNull.Value == dr["SAPClearingDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SAPClearingDate"]);
                                W.SAPInvoiceValue = DBNull.Value == dr["SAPInvoiceValue"] ? (decimal?)null : Convert.ToDecimal(dr["SAPInvoiceValue"]);
                                W.SAPInvoiceTDSValue = DBNull.Value == dr["SAPInvoiceTDSValue"] ? (decimal?)null : Convert.ToDecimal(dr["SAPInvoiceTDSValue"]);
                                W.InvoiceType = new PDMS_WarrantyInvoiceType() { InvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"]), InvoiceType = Convert.ToString(dr["InvoiceType"]) };
                            }
                            W.InvoiceItems.Add(new PDMS_WarrantyClaimInvoiceItem()
                            {
                                Material = Convert.ToString(dr["Material"]),
                                MaterialDesc = Convert.ToString(dr["MaterialDesc"]),
                                HSNCode = Convert.ToString(dr["HSNCode"]),
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Rate = Convert.ToDecimal(dr["Rate"]),
                                Category = Convert.ToString(dr["Category"]),
                                ApprovedValue = Convert.ToDecimal(dr["ApprovedValue"]),
                                Discount = Convert.ToDecimal(dr["Discount"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToInt32(dr["CGST"]),
                                SGST = Convert.ToInt32(dr["SGST"]),
                                IGST = Convert.ToInt32(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                                WarrantyClaimAnnexureItemID = Convert.ToInt64(dr["WarrantyClaimAnnexureItemID"])

                            });
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
    }
}