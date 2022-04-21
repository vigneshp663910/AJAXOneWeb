using DataAccess;
using Newtonsoft.Json;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Business
{
  public  class BSalesQuotation
    {
        private IDataAccess provider;
        public BSalesQuotation()
        {
            try
            {
                provider = new ProviderFactory().GetProvider();
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BSalesbQuotation", "provider : " + e1.Message, null);
            }
        }
        public long InsertOrUpdateSalesQuotationBasicInformation(PSalesQuotation SalesOrder)
        {
            return 2;
            //int success = 0;

            //DbParameter WebQuotationID = provider.CreateParameter("WebQuotationID", SalesOrder.WebQuotationID, DbType.Int64);
             
            //DbParameter DealerID = provider.CreateParameter("DealerID", SalesOrder.Dealer.DealerID, DbType.Int32);
            //DbParameter CustomerID = provider.CreateParameter("CustomerID", SalesOrder.Customer.CustomerID, DbType.Int32);

            //DbParameter BillTo = provider.CreateParameter("BillTo", SalesOrder.BillTo.CustomerCode, DbType.String);
            //DbParameter ShipTo = provider.CreateParameter("ShipTo", SalesOrder.ShipTo.CustomerCode, DbType.String);
            //DbParameter Office = provider.CreateParameter("Office", SalesOrder.Office, DbType.String);

            //DbParameter StateID = provider.CreateParameter("ShipToStateID", SalesOrder.ShipToAddress.State.StateID, DbType.Int32);
            //DbParameter DistrictID = provider.CreateParameter("ShipToDistrictID", SalesOrder.ShipToAddress.District.DistrictID, DbType.Int32);
            //DbParameter Address1 = provider.CreateParameter("ShipToAddress1", SalesOrder.ShipToAddress.Address1, DbType.String);
            //DbParameter Address2 = provider.CreateParameter("ShipToAddress2", SalesOrder.ShipToAddress.Address2, DbType.String);
            //DbParameter City = provider.CreateParameter("ShipToCity", SalesOrder.ShipToAddress.City, DbType.String);
            //DbParameter PostalCode = provider.CreateParameter("ShipToPostalCode", SalesOrder.ShipToAddress.PostalCode, DbType.String);

            //DbParameter MainApplicationID = provider.CreateParameter("UsageID", SalesOrder.Usage == null ? (int?)null : SalesOrder.Usage.MainApplicationID, DbType.Int32);
            //DbParameter RetailCustomer = provider.CreateParameter("RetailCustomer", SalesOrder.RetailCustomer, DbType.String);
            //// DbParameter Hypothecation = provider.CreateParameter("Hypothecation", SalesOrder.Hypothecation, DbType.String);


            //DbParameter SourceOfEnquiry = provider.CreateParameter("SourceOfEnquiry", SalesOrder.SourceOfEnquiry == null ? (int?)null : SalesOrder.SourceOfEnquiry.SourceOfEnquiryID, DbType.Int32);
            //DbParameter ReasonForOrderConversion = provider.CreateParameter("ReasonForOrderConversion", SalesOrder.ReasonForOrderConversion, DbType.String);

            //DbParameter CustomerType = provider.CreateParameter("CustomerType", SalesOrder.CustomerType, DbType.String);
            //DbParameter Profile = provider.CreateParameter("Profile", SalesOrder.Profile, DbType.String);
            //DbParameter Size = provider.CreateParameter("Size", SalesOrder.Size, DbType.String);
            //DbParameter OwnershipPattern = provider.CreateParameter("OwnershipPattern", SalesOrder.OwnershipPattern, DbType.String);
            //DbParameter NameOfTheProject = provider.CreateParameter("NameOfTheProject", SalesOrder.NameOfTheProject, DbType.String);
            //DbParameter DiscountTypeID = provider.CreateParameter("DiscountTypeID", SalesOrder.ModeOfBilling == null ? (int?)null : SalesOrder.ModeOfBilling.DiscountTypeID, DbType.Int32);

            //DbParameter SpecialRequirements = provider.CreateParameter("SpecialRequirements", SalesOrder.SpecialRequirements, DbType.String);
            //DbParameter FocServiceKit = provider.CreateParameter("FocServiceKit", SalesOrder.FocServiceKit, DbType.String);
            //DbParameter FocWheelAssy = provider.CreateParameter("FocWheelAssy", SalesOrder.FocWheelAssy, DbType.String);
            //DbParameter FocExtensionChutes = provider.CreateParameter("FocExtensionChutes", SalesOrder.FocExtensionChutes, DbType.String);
            //DbParameter FocOthers = provider.CreateParameter("FocOthers", SalesOrder.FocOthers, DbType.String);


            //DbParameter OutValueDParam = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));

            //DbParameter[] Params = new DbParameter[28] { WebQuotationID, 
            //    DealerID,CustomerID,BillTo,ShipTo,Office
            //   ,StateID,DistrictID,Address1,Address2,City,PostalCode ,MainApplicationID,RetailCustomer,SourceOfEnquiry,ReasonForOrderConversion
            //,CustomerType,Profile,Size,OwnershipPattern,NameOfTheProject,DiscountTypeID
            //, SpecialRequirements, FocServiceKit, FocWheelAssy, FocExtensionChutes, FocOthers ,OutValueDParam };
            //long SalesOrderID = 0;
            //try
            //{
            //    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //    {
            //        success = provider.Insert("ZDMS_InsertOrUpdateWebQuotationBasicInformation", Params);
            //        SalesOrderID = Convert.ToInt64(OutValueDParam.Value);
            //        foreach (PDMS_WebQuotationItem Item in SalesOrder.WebQuotationItems)
            //        {
            //            Item.WebQuotationID = SalesOrderID;
            //            InsertOrUpdateWebQuotationItem(Item);
            //        }
            //        scope.Complete();
            //    }

            //}
            //catch (SqlException sqlEx)
            //{
            //    new FileLogger().LogMessage("BDMS_WebQuotation", "InsertOrUpdatePrimarySalesOrderDealer", sqlEx);
            //    return 0;
            //}
            //catch (Exception ex)
            //{
            //    new FileLogger().LogMessage("BDMS_WebQuotation", " InsertOrUpdatePrimarySalesOrderDealer", ex);
            //    return 0;
            //}
            //return Convert.ToInt64(OutValueDParam.Value);
        }
        public Boolean InsertOrUpdateSalesQuotationFocInformation(PSalesQuotation SalesOrder)
        {
            //int success = 0;
            //DbParameter WebQuotationID = provider.CreateParameter("WebQuotationID", SalesOrder.QuotationID, DbType.Int64);

            //DbParameter SpecialRequirements = provider.CreateParameter("SpecialRequirements", SalesOrder.SpecialRequirements, DbType.String);
            //DbParameter FocServiceKit = provider.CreateParameter("FocServiceKit", SalesOrder.FocServiceKit, DbType.String);
            //DbParameter FocWheelAssy = provider.CreateParameter("FocWheelAssy", SalesOrder.FocWheelAssy, DbType.String);
            //DbParameter FocExtensionChutes = provider.CreateParameter("FocExtensionChutes", SalesOrder.FocExtensionChutes, DbType.String);
            //DbParameter FocOthers = provider.CreateParameter("FocOthers", SalesOrder.FocOthers, DbType.String);

            //DbParameter[] Params = new DbParameter[6] { WebQuotationID, SpecialRequirements, FocServiceKit, FocWheelAssy, FocExtensionChutes, FocOthers };
            //try
            //{
            //    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //    {
            //        success = provider.Insert("ZDMS_InsertOrUpdateWebQuotationFocInformation", Params);
            //        scope.Complete();
            //    }
            //}
            //catch (SqlException sqlEx)
            //{
            //    new FileLogger().LogMessage("BDMS_WebQuotation", "InsertOrUpdatePrimarySalesOrderFocInformation", sqlEx);
            //    return false;
            //}
            //catch (Exception ex)
            //{
            //    new FileLogger().LogMessage("BDMS_WebQuotation", " InsertOrUpdatePrimarySalesOrderFocInformation", ex);
            //    return false;
            //}
            return true;
        }
        public Boolean InsertOrUpdateSalesQuotationFinanceInformation(PSalesQuotation SalesOrder)
        {
            int success = 0;
            //DbParameter WebQuotationID = provider.CreateParameter("WebQuotationID", SalesOrder.QuotationID, DbType.Int64);

            //DbParameter FinancierCode = provider.CreateParameter("FinancierID", SalesOrder.Financier == null ? (int?)null : SalesOrder.Financier.FinancierID, DbType.String);
            //DbParameter InvoiceValue = provider.CreateParameter("InvoiceValue", SalesOrder.InvoiceValue, DbType.Decimal);
            //DbParameter DoNumber = provider.CreateParameter("DoNumber", SalesOrder.DoNumber, DbType.String);
            //DbParameter DoDate = provider.CreateParameter("DoDate", SalesOrder.DoDate, DbType.DateTime);
            //DbParameter PaymentTermID = provider.CreateParameter("TermsOfPaymentID", SalesOrder.CreditDays == null ? (int?)null : SalesOrder.CreditDays.PaymentTermID, DbType.Int32);

            //DbParameter DoAmount = provider.CreateParameter("DoAmount", SalesOrder.DoAmount, DbType.Decimal);
            //DbParameter MarginMoney = provider.CreateParameter("MarginMoney", SalesOrder.MarginMoney, DbType.Decimal);

            //DbParameter IncoTermID = provider.CreateParameter("IncoTermID", SalesOrder.IncoTerm == null ? (int?)null : SalesOrder.IncoTerm.IncoTermID, DbType.Int32);
            //DbParameter AdvanceAmount = provider.CreateParameter("AdvanceAmount", SalesOrder.AdvanceAmount, DbType.Decimal);
            ////  DbParameter FinancierAmount = provider.CreateParameter("FinancierAmount", SalesOrder.FinancierAmount, DbType.Decimal);
            //DbParameter BenificiaryOfDO = provider.CreateParameter("BenificiaryOfDO", SalesOrder.BenificiaryOfDO, DbType.String);
            //DbParameter SubventionAmount = provider.CreateParameter("SubventionAmount", SalesOrder.SubventionAmount, DbType.Decimal);
            //DbParameter BackToBackDoEndorsedToAjax = provider.CreateParameter("BackToBackDoEndorsedToAjax", SalesOrder.BackToBackDoEndorsedToAjax, DbType.String);
            //DbParameter TransportationAndInsurance = provider.CreateParameter("TransportationAndInsurance", SalesOrder.TransportationAndInsurance, DbType.String);
            //DbParameter[] Params = new DbParameter[14] { WebQuotationID,FinancierCode,InvoiceValue,DoNumber,DoDate,PaymentTermID
            //    ,DoAmount,MarginMoney,IncoTermID,AdvanceAmount,BenificiaryOfDO,SubventionAmount,BackToBackDoEndorsedToAjax,TransportationAndInsurance };
            //try
            //{
            //    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //    {
            //        success = provider.Insert("ZDMS_InsertOrUpdateWebQuotationFinanceInformation", Params);
            //        scope.Complete();
            //    }
            //}
            //catch (SqlException sqlEx)
            //{
            //    new FileLogger().LogMessage("BDMS_WebQuotation", "InsertOrUpdatePrimarySalesOrderFinanceInformation", sqlEx);
            //    return false;
            //}
            //catch (Exception ex)
            //{
            //    new FileLogger().LogMessage("BDMS_WebQuotation", " InsertOrUpdatePrimarySalesOrderFinanceInformation", ex);
            //    return false;
            //}
            return true;
        }
        //public Boolean InsertOrUpdateSalesQuotationItem(PSalesQuotationItem Item)
        //{
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            DbParameter QuotationID = provider.CreateParameter("QuotationID", Item.QuotationID, DbType.Int64);
        //            DbParameter QuotationItemID = provider.CreateParameter("WebQuotationItemID", Item.QuotationItemID, DbType.Int64);
        //            DbParameter MaterialCode = provider.CreateParameter("MaterialCode", Item.Material == null ? "" : Item.Material.MaterialCode, DbType.String);
        //            DbParameter MaterialDescription = provider.CreateParameter("MaterialDescription", Item.Material == null ? "" : Item.Material.MaterialDescription, DbType.String);
                   
        //            DbParameter Qty = provider.CreateParameter("Qty", Item.Qty, DbType.Int32);
        //            DbParameter Rate = provider.CreateParameter("Rate", Item.Rate, DbType.Decimal);
        //            DbParameter TaxableValue = provider.CreateParameter("TaxableValue", Item.TaxableValue, DbType.Decimal);
        //            DbParameter TaxPersent = provider.CreateParameter("TaxPersent", Item.TaxPersent, DbType.Decimal);
        //            DbParameter TaxValue = provider.CreateParameter("TaxValue", Item.TaxValue, DbType.Decimal);
        //            DbParameter NetValue = provider.CreateParameter("NetValue", Item.NetValue, DbType.Decimal);

        //            DbParameter[] ItemParams = new DbParameter[10] { QuotationID, QuotationItemID, MaterialCode, MaterialDescription,  Qty, Rate, TaxableValue, TaxPersent, TaxValue, NetValue };
        //            provider.Insert("ZDMS_InsertOrUpdateSalesQuotationItem", ItemParams);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BDMS_WebQuotation", "InsertOrUpdatePrimarySalesOrderDealer", sqlEx);
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BDMS_WebQuotation", " InsertOrUpdatePrimarySalesOrderDealer", ex);
        //        return false;
        //    }
        //    return true;
        //}
        public Boolean InsertOrUpdateSalesQuotationSalesInformation(PSalesQuotation SalesOrder)
        {
            //int success = 0;
            //DbParameter WebQuotationID = provider.CreateParameter("WebQuotationID", SalesOrder.QuotationID, DbType.Int64);
            //DbParameter EquipmentHeaderID = provider.CreateParameter("EquipmentHeaderID", SalesOrder.Equipment.EquipmentHeaderID, DbType.Int64);
            //DbParameter DiscountSales = provider.CreateParameter("DiscountSales", SalesOrder.DiscountSales, DbType.Decimal);
            //DbParameter FreightValue = provider.CreateParameter("FreightValue", SalesOrder.FreightValue, DbType.Decimal);
            //DbParameter InsuranceValue = provider.CreateParameter("InsuranceValue", SalesOrder.InsuranceValue, DbType.Decimal);
            //DbParameter TRDate = provider.CreateParameter("TRDate", SalesOrder.TRDate, DbType.DateTime);
            //DbParameter ConsolidationInvoicePrint = provider.CreateParameter("ConsolidationInvoicePrint", SalesOrder.ConsolidationInvoicePrint, DbType.Boolean);
            //DbParameter FreightAmount = provider.CreateParameter("FreightAmount", SalesOrder.FreightAmount, DbType.Decimal);
            //DbParameter Billing = provider.CreateParameter("Billing", SalesOrder.Billing, DbType.String);

            //DbParameter[] Params = new DbParameter[9] { WebQuotationID, EquipmentHeaderID, DiscountSales, FreightValue, InsuranceValue, TRDate, ConsolidationInvoicePrint, FreightAmount, Billing };
            //try
            //{
            //    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //    {
            //        success = provider.Insert("ZDMS_InsertOrUpdateWebQuotationSalesInformation", Params);
            //        scope.Complete();
            //    }
            //}
            //catch (SqlException sqlEx)
            //{
            //    new FileLogger().LogMessage("BDMS_WebQuotation", "InsertOrUpdatePrimarySalesOrder", sqlEx);
            //    return false;
            //}
            //catch (Exception ex)
            //{
            //    new FileLogger().LogMessage("BDMS_WebQuotation", " InsertOrUpdatePrimarySalesOrder", ex);
            //    return false;
            //}
            return true;
        }
        public Boolean UpdateSalesQuotationSapStatus(long WebQuotationID, Boolean IsSuccess)
        {
            int success = 0;
            DbParameter WebQuotationIDP = provider.CreateParameter("WebQuotationID", WebQuotationID, DbType.Int64);
            DbParameter IsSuccessP = provider.CreateParameter("IsSuccess", IsSuccess, DbType.Boolean);
            DbParameter[] Params = new DbParameter[2] { WebQuotationIDP, IsSuccessP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_UpdateWebQuotationOrderSapStatus", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WebQuotation", "UpdateWebQuotationSapStatus", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WebQuotation", " UpdateWebQuotationSapStatus", ex);
                return false;
            }
            return true;
        }


        public Boolean ApproveSalesQuotation(long WebQuotationID, Int16 StatusID, int UserID)
        {
            int success = 0;
            DbParameter WebQuotationIDP = provider.CreateParameter("WebQuotationID", WebQuotationID, DbType.Int64);
            DbParameter StatusIDP = provider.CreateParameter("WebQuotationStatusID", StatusID, DbType.Int16);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            DbParameter[] Params = new DbParameter[3] { WebQuotationIDP, StatusIDP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_ApproveWebQuotation", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WebQuotation", "ApproveWebQuotation", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WebQuotation", " ApproveWebQuotation", ex);
                return false;
            }
            return true;
        }
        public List<PSalesQuotation> GetWebQuotationByID(long? WebQuotationID, string PurchaseOrderNumber)
        {
            List<PSalesQuotation> Ws = new List<PSalesQuotation>();
            //PDMS_WebQuotation W = null;
            //try
            //{
            //    DbParameter WebQuotationIDP = provider.CreateParameter("WebQuotationID", WebQuotationID, DbType.Int64);
            //    DbParameter PurchaseOrderNumberP = provider.CreateParameter("PurchaseOrder", string.IsNullOrEmpty(PurchaseOrderNumber) ? null : PurchaseOrderNumber, DbType.String);

            //    DbParameter[] Params = new DbParameter[2] { WebQuotationIDP, PurchaseOrderNumberP };
            //    using (DataSet DataSet = provider.Select("ZDMS_GetWebQuotationByWebQuotationID", Params))
            //    {
            //        if (DataSet != null)
            //        {
            //            foreach (DataRow dr in DataSet.Tables[0].Rows)
            //            {
            //                W = new PDMS_WebQuotation();
            //                Ws.Add(W);

            //                W.WebQuotationID = Convert.ToInt64(dr["WebQuotationID"]);
            //                W.SalesOrderNumber = Convert.ToString(dr["SalesOrderNumber"]);
            //                W.SalesOrderDate = dr["SalesOrderDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["SalesOrderDate"]);
            //                W.PrimaryPurchaseOrder = new PDMS_PrimaryPurchaseOrder() { PurchaseOrderNumber = Convert.ToString(dr["PurchaseOrder"]), PurchaseOrderDate = Convert.ToDateTime(dr["PurchaseOrderDate"]) };
            //                //  public PDMS_PrimaryPurchaseOrder PrimaryPurchaseOrder { get; set; }
            //                //  public PDMS_PrimaryInvoice PrimaryInvoice { get; set; }

            //                W.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(dr["DealerID"]), DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
            //                W.Customer = new PDMS_Customer() { CustomerID = Convert.ToInt32(dr["CustomerID"]), CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
            //                // public string SalesOrderStatus { get; set; }
            //                //  public PDMS_Address InvoiceAddress { get; set; }
            //                W.BillTo = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["BillTo"]) };
            //                W.ShipTo = new PDMS_Customer()
            //                {
            //                    CustomerCode = Convert.ToString(dr["ShipTo"]),
            //                    State = new PDMS_State() { StateID = Convert.ToInt32(dr["ShipToStateID"]), State = Convert.ToString(dr["State"]) },
            //                    District = dr["ShipToDistrictID"] == DBNull.Value ? null : new PDMS_District() { DistrictID = Convert.ToInt32(dr["ShipToDistrictID"]), District = Convert.ToString(dr["District"]) },
            //                    City = Convert.ToString(dr["ShipToCity"]),
            //                    Address1 = Convert.ToString(dr["ShipToAddress1"]),
            //                    Address2 = Convert.ToString(dr["ShipToAddress2"]),
            //                    Pincode = Convert.ToString(dr["ShipToPostalCode"])
            //                };
            //                W.WebQuotationStatus = new PDMS_WebQuotationStatus() { WebQuotationStatusID = Convert.ToInt16(dr["WebQuotationStatusID"]), WebQuotationStatus = Convert.ToString(dr["WebQuotationStatus"]) };
            //                W.Office = Convert.ToString(dr["Office"]);

            //                W.ShipToAddress = new PDMS_Address();
            //                W.ShipToAddress.State = new PDMS_State() { StateID = Convert.ToInt32(dr["ShipToStateID"]), State = Convert.ToString(dr["State"]) };
            //                W.ShipToAddress.District = new PDMS_District() { DistrictID = Convert.ToInt32(dr["ShipToDistrictID"]), District = Convert.ToString(dr["District"]) };
            //                W.ShipToAddress.City = Convert.ToString(dr["ShipToCity"]);
            //                W.ShipToAddress.Address1 = Convert.ToString(dr["ShipToAddress1"]);
            //                W.ShipToAddress.Address2 = Convert.ToString(dr["ShipToAddress2"]);
            //                W.ShipToAddress.PostalCode = Convert.ToString(dr["ShipToPostalCode"]);

            //                //Finance

            //                W.Financier = dr["FinancierID"] == DBNull.Value ? null : new PDMS_Financier() { FinancierID = Convert.ToInt32(dr["FinancierID"]), FinancierCode = Convert.ToString(dr["FinancierCode"]), FinancierName = Convert.ToString(dr["FinancierName"]) };
            //                W.InvoiceValue = dr["InvoiceValue"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["InvoiceValue"]);
            //                W.DoNumber = Convert.ToString(dr["DoNumber"]);
            //                W.DoDate = dr["DoDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DoDate"]);
            //                W.CreditDays = dr["TermsOfPaymentID"] == DBNull.Value ? null : new PDMS_PaymentTerm() { PaymentTermID = Convert.ToInt32(dr["TermsOfPaymentID"]), Description = Convert.ToString(dr["PaymentTermDescription"]) };
            //                W.DoAmount = dr["DoAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["DoAmount"]);
            //                W.MarginMoney = dr["MarginMoney"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["MarginMoney"]);
            //                W.ModeOfBilling = dr["DiscountTypeID"] == DBNull.Value ? null : new PDMS_DiscountType() { DiscountTypeID = Convert.ToInt32(dr["DiscountTypeID"]), DiscountType = Convert.ToString(dr["DiscountType"]) };
            //                W.IncoTerm = dr["IncoTermID"] == DBNull.Value ? null : new PDMS_IncoTerm() { IncoTermID = Convert.ToInt32(dr["IncoTermID"]), Description = Convert.ToString(dr["IncoTermDescription"]) };
            //                W.AdvanceAmount = dr["AdvanceAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["AdvanceAmount"]);
            //                //    W.FinancierAmount = dr["FinancierAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["FinancierAmount"]);
            //                W.BenificiaryOfDO = Convert.ToString(dr["BenificiaryOfDO"]);
            //                W.SubventionAmount = dr["SubventionAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["SubventionAmount"]);



            //                W.Usage = dr["UsageID"] == DBNull.Value ? null : new PDMS_MainApplication() { MainApplicationID = Convert.ToInt32(dr["UsageID"]), MainApplication = Convert.ToString(dr["MainApplication"]) };
            //                W.RetailCustomer = Convert.ToString(dr["RetailCustomer"]);
            //                //   W.Hypothecation = Convert.ToString(dr["Hypothecation"]);
            //                W.BackToBackDoEndorsedToAjax = Convert.ToString(dr["BackToBackDoEndorsedToAjax"]);
            //                W.SpecialRequirements = Convert.ToString(dr["SpecialRequirements"]);
            //                W.FocServiceKit = Convert.ToString(dr["FocServiceKit"]);
            //                W.FocWheelAssy = Convert.ToString(dr["FocWheelAssy"]);
            //                W.FocExtensionChutes = Convert.ToString(dr["FocExtensionChutes"]);
            //                W.FocOthers = Convert.ToString(dr["FocOthers"]);
            //                W.SourceOfEnquiry = dr["SourceOfEnquiryID"] == DBNull.Value ? null : new PDMS_SourceOfEnquiry() { SourceOfEnquiryID = Convert.ToInt32(dr["SourceOfEnquiryID"]), SourceOfEnquiry = Convert.ToString(dr["SourceOfEnquiry"]) };
            //                W.ReasonForOrderConversion = Convert.ToString(dr["ReasonForOrderConversion"]);
            //                W.CustomerType = Convert.ToString(dr["CustomerType"]);
            //                W.Profile = Convert.ToString(dr["Profile"]);
            //                W.Size = Convert.ToString(dr["Size"]);
            //                W.OwnershipPattern = Convert.ToString(dr["OwnershipPattern"]);

            //                W.NameOfTheProject = Convert.ToString(dr["NameOfTheProject"]);
            //                W.TransportationAndInsurance = Convert.ToString(dr["TransportationAndInsurance"]);

            //                //  public PDMS_Equipment Equipment { get; set; } 
            //                W.DiscountSales = dr["DiscountSales"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["DiscountSales"]);
            //                W.FreightValue = dr["FreightValue"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["FreightValue"]);
            //                W.InsuranceValue = dr["InsuranceValue"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["InsuranceValue"]);
            //                W.ConsolidationInvoicePrint = dr["ConsolidationInvoicePrint"] == DBNull.Value ? false : Convert.ToBoolean(dr["ConsolidationInvoicePrint"]);
            //                W.TRDate = dr["TRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["TRDate"]);
            //                W.FreightAmount = dr["FreightAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["FreightAmount"]);
            //                W.Billing = Convert.ToString(dr["Billing"]);

            //                if (dr["EquipmentHeaderID"] == DBNull.Value)
            //                {
            //                    W.Equipment = null;
            //                }
            //                else
            //                {
            //                    W.Equipment = new PDMS_EquipmentHeader();

            //                    W.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
            //                    W.Equipment.EquipmentModel = new PDMS_Model()
            //                    {
            //                        ModelID = Convert.ToInt32(dr["ModelID"]),
            //                        ModelCode = Convert.ToString(dr["ModelCode"]),
            //                        Model = Convert.ToString(dr["Model"]),
            //                        ModelDescription = Convert.ToString(dr["ModelDescription"]),
            //                        // Division = new PDMS_Division() { DivisionID = Convert.ToInt32(dr["DivisionID"]), DivisionCode = Convert.ToString(dr["DivisionCode"]), DivisionDescription = Convert.ToString(dr["DivisionDescription"]) }
            //                    };

            //                    W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
            //                    W.Equipment.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);

            //                    W.Equipment.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["MaterialCode"]) };
            //                    W.Equipment.TypeOfWheelAssembly = Convert.ToString(dr["TypeOfWheelAssembly"]);
            //                    W.Equipment.ChassisSlNo = Convert.ToString(dr["ChassisSlNo"]);
            //                    W.Equipment.ESN = Convert.ToString(dr["ESN"]);
            //                    W.Equipment.Plant = Convert.ToString(dr["Plant"]);

            //                    W.Equipment.Dispatch = Convert.ToString(dr["Dispatch"]);
            //                    W.Equipment.ManufacturingDate = dr["ManufacturingDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ManufacturingDate"]);

            //                }
            //                W.SendToSAP = dr["SendToSAP"] == DBNull.Value ? false : Convert.ToBoolean(dr["SendToSAP"]);
            //                W.IsSuccess = dr["IsSuccess"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsSuccess"]);
            //                W.Approver = new PUser() { ContactName = Convert.ToString(dr["ApproverContactName"]) };
            //                W.WebQuotationItems = GetWebQuotationItem(W.WebQuotationID, null);
            //            }
            //        }
            //    }
            //}
            //catch (SqlException sqlEx)
            //{

            //}
            //catch (Exception ex)
            //{

            //}
            return Ws;
        }
        public List<PSalesQuotationItem> GetSalesQuotationItem(long? WebQuotationID, long? WebQuotationItemID)
        {
            List<PSalesQuotationItem> Ws = new List<PSalesQuotationItem>();
            //PDMS_WebQuotationItem W = null;
            //try
            //{
            //    DbParameter WebQuotationIDP = provider.CreateParameter("WebQuotationID", WebQuotationID, DbType.Int64);
            //    DbParameter WebQuotationItemIDP = provider.CreateParameter("WebQuotationItemID", WebQuotationItemID, DbType.String);

            //    DbParameter[] Params = new DbParameter[2] { WebQuotationIDP, WebQuotationItemIDP };
            //    using (DataSet DataSet = provider.Select("ZDMS_GetWebQuotationItem", Params))
            //    {
            //        if (DataSet != null)
            //        {
            //            foreach (DataRow dr in DataSet.Tables[0].Rows)
            //            {
            //                W = new PDMS_WebQuotationItem();
            //                Ws.Add(W);
            //                W.WebQuotationID = Convert.ToInt64(dr["WebQuotationID"]);
            //                W.WebQuotationItemID = Convert.ToInt64(dr["WebQuotationItemID"]);
            //                W.Material = new PDMS_Material()
            //                {
            //                    MaterialID = Convert.ToInt32(dr["MaterialID"]),
            //                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
            //                    MaterialDescription = Convert.ToString(dr["MaterialDescription"])
            //                };
            //                W.Qty = Convert.ToInt32(dr["Qty"]);
            //                W.BasicPrice = Convert.ToDecimal(dr["BasicPrice"]);
            //                W.Discount1 = dr["Discount1"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Discount1"]);
            //                W.Discount2 = dr["Discount2"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Discount2"]);
            //                W.Discount3 = dr["Discount3"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Discount3"]);
            //            }
            //        }
            //    }
            //}
            //catch (SqlException sqlEx)
            //{

            //}
            //catch (Exception ex)
            //{

            //}
            return Ws;
        }

        public List<PSalesQuotation> GetSalesQuotation(string PONumber, DateTime? PODateFrom, DateTime? PODateTo, string SONumber, DateTime? SODateFrom, DateTime? SODateTo
            , int? DealerID, string CustomerCode, Int16? WebQuotationStatusID)
        {
            List<PSalesQuotation> Ws = new List<PSalesQuotation>();
            //PSalesQuotation W = null;
            //try
            //{
            //    DbParameter PONumberP = provider.CreateParameter("PONumber", string.IsNullOrEmpty(PONumber) ? null : PONumber, DbType.Int64);
            //    DbParameter PODateFromP = provider.CreateParameter("PODateFrom", PODateFrom, DbType.DateTime);
            //    DbParameter PODateToP = provider.CreateParameter("PODateTo", PODateTo, DbType.DateTime);

            //    DbParameter SONumberP = provider.CreateParameter("SONumber", string.IsNullOrEmpty(SONumber) ? null : SONumber, DbType.Int64);
            //    DbParameter SODateFromP = provider.CreateParameter("SODateFrom", SODateFrom, DbType.DateTime);
            //    DbParameter SODateToP = provider.CreateParameter("SODateTo", SODateTo, DbType.DateTime);

            //    DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            //    DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.Int64);
            //    DbParameter WebQuotationStatusIDP = provider.CreateParameter("WebQuotationStatusID", WebQuotationStatusID, DbType.Int16);


            //    DbParameter[] Params = new DbParameter[9] { PONumberP, PODateFromP, PODateToP, SONumberP, SODateFromP, SODateToP, DealerIDP, CustomerCodeP, WebQuotationStatusIDP };
            //    using (DataSet DataSet = provider.Select("ZDMS_GetWebQuotation", Params))
            //    {
            //        if (DataSet != null)
            //        {
            //            foreach (DataRow dr in DataSet.Tables[0].Rows)
            //            {
            //                W = new PDMS_WebQuotation();
            //                Ws.Add(W);

            //                W.WebQuotationID = Convert.ToInt64(dr["WebQuotationID"]);
            //                W.SalesOrderNumber = Convert.ToString(dr["SalesOrderNumber"]);
            //                W.SalesOrderDate = dr["SalesOrderDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["SalesOrderDate"]);
            //                W.PrimaryPurchaseOrder = new PDMS_PrimaryPurchaseOrder() { PurchaseOrderNumber = Convert.ToString(dr["PurchaseOrder"]) };
            //                W.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(dr["DealerID"]), DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
            //                W.Customer = new PDMS_Customer() { CustomerID = Convert.ToInt32(dr["CustomerID"]), CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
            //                W.BillTo = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["BillTo"]) };
            //                W.ShipTo = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["ShipTo"]) };
            //                W.Office = Convert.ToString(dr["Office"]);
            //                W.WebQuotationStatus = new PDMS_WebQuotationStatus() { WebQuotationStatusID = Convert.ToInt16(dr["WebQuotationStatusID"]), WebQuotationStatus = Convert.ToString(dr["WebQuotationStatus"]) };

            //                W.Financier = dr["FinancierID"] == DBNull.Value ? null : new PDMS_Financier() { FinancierID = Convert.ToInt32(dr["FinancierID"]), FinancierCode = Convert.ToString(dr["FinancierCode"]), FinancierName = Convert.ToString(dr["FinancierName"]) };

            //                W.InvoiceValue = dr["InvoiceValue"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["InvoiceValue"]);
            //                W.DoAmount = dr["DoAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["DoAmount"]);
            //                W.MarginMoney = dr["MarginMoney"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["MarginMoney"]);

            //                //      W.Hypothecation = Convert.ToString(dr["Hypothecation"]);
            //                W.SourceOfEnquiry = dr["SourceOfEnquiryID"] == DBNull.Value ? null : new PDMS_SourceOfEnquiry() { SourceOfEnquiryID = Convert.ToInt32(dr["SourceOfEnquiryID"]), SourceOfEnquiry = Convert.ToString(dr["SourceOfEnquiry"]) };
            //                W.CustomerType = Convert.ToString(dr["CustomerType"]);

            //                W.NameOfTheProject = Convert.ToString(dr["NameOfTheProject"]);
            //                W.TransportationAndInsurance = Convert.ToString(dr["TransportationAndInsurance"]);
            //                W.DiscountSales = dr["DiscountSales"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["DiscountSales"]);
            //                W.FreightValue = dr["FreightValue"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["FreightValue"]);
            //                W.InsuranceValue = dr["InsuranceValue"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["InsuranceValue"]);
            //                W.ConsolidationInvoicePrint = dr["ConsolidationInvoicePrint"] == DBNull.Value ? false : Convert.ToBoolean(dr["ConsolidationInvoicePrint"]);
            //                W.FreightAmount = dr["FreightAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["FreightAmount"]);

            //                if (dr["EquipmentHeaderID"] == DBNull.Value)
            //                {
            //                    W.Equipment = null;
            //                }
            //                else
            //                {
            //                    W.Equipment = new PDMS_EquipmentHeader();
            //                    W.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
            //                    W.Equipment.EquipmentModel = new PDMS_Model() { ModelID = Convert.ToInt32(dr["ModelID"]), Model = Convert.ToString(dr["Model"]), };
            //                    W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
            //                    W.Equipment.Dispatch = Convert.ToString(dr["Dispatch"]);
            //                }
            //                W.WebQuotationItems = GetSalesQuotationItem(W.QuotationID, null);
            //            }
            //        }
            //    }
            //}
            //catch (SqlException sqlEx)
            //{

            //}
            //catch (Exception ex)
            //{

            //}
            return Ws;
        }
        public List<PSalesQuotation> GetSalesQuotationForApproval()
        {
            List<PSalesQuotation> Ws = new List<PSalesQuotation>();
            //PSalesQuotation W = null;
            //try
            //{
            //    using (DataSet DataSet = provider.Select("GetWebQuotationForApproval"))
            //    {
            //        if (DataSet != null)
            //        {
            //            foreach (DataRow dr in DataSet.Tables[0].Rows)
            //            {
            //                W = new PSalesQuotation();
            //                Ws.Add(W);
            //                W.QuotationID = Convert.ToInt64(dr["WebQuotationID"]);
            //                W.SalesOrderNumber = Convert.ToString(dr["SalesOrderNumber"]);
            //                W.SalesOrderDate = dr["SalesOrderDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["SalesOrderDate"]);
            //                W.PrimaryPurchaseOrder = new PDMS_PrimaryPurchaseOrder() { PurchaseOrderNumber = Convert.ToString(dr["PurchaseOrder"]), PurchaseOrderDate = Convert.ToDateTime(dr["PurchaseOrderDate"]) };
            //                W.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(dr["DealerID"]), DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
            //                W.Customer = new PDMS_Customer() { CustomerID = Convert.ToInt32(dr["CustomerID"]), CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
            //                W.BillTo = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["BillTo"]) };
            //                W.ShipTo = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["ShipTo"]) };
            //                W.Office = Convert.ToString(dr["Office"]);
            //                W.WebQuotationStatus = new PDMS_WebQuotationStatus() { WebQuotationStatusID = Convert.ToInt16(dr["WebQuotationStatusID"]), WebQuotationStatus = Convert.ToString(dr["WebQuotationStatus"]) };
            //                W.Financier = dr["FinancierID"] == DBNull.Value ? null : new PDMS_Financier() { FinancierID = Convert.ToInt32(dr["FinancierID"]), FinancierCode = Convert.ToString(dr["FinancierCode"]), FinancierName = Convert.ToString(dr["FinancierName"]) };
            //                W.InvoiceValue = dr["InvoiceValue"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["InvoiceValue"]);
            //                W.DoAmount = dr["DoAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["DoAmount"]);
            //                W.MarginMoney = dr["MarginMoney"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["MarginMoney"]);
            //                //  W.Hypothecation = Convert.ToString(dr["Hypothecation"]);
            //                W.SourceOfEnquiry = dr["SourceOfEnquiryID"] == DBNull.Value ? null : new PDMS_SourceOfEnquiry() { SourceOfEnquiryID = Convert.ToInt32(dr["SourceOfEnquiryID"]), SourceOfEnquiry = Convert.ToString(dr["SourceOfEnquiry"]) };
            //                W.CustomerType = Convert.ToString(dr["CustomerType"]);
            //                W.NameOfTheProject = Convert.ToString(dr["NameOfTheProject"]);
            //                W.TransportationAndInsurance = Convert.ToString(dr["TransportationAndInsurance"]);
            //                W.DiscountSales = dr["DiscountSales"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["DiscountSales"]);
            //                W.FreightValue = dr["FreightValue"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["FreightValue"]);
            //                W.InsuranceValue = dr["InsuranceValue"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["InsuranceValue"]);
            //                W.ConsolidationInvoicePrint = dr["ConsolidationInvoicePrint"] == DBNull.Value ? false : Convert.ToBoolean(dr["ConsolidationInvoicePrint"]);
            //                W.FreightAmount = dr["FreightAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["FreightAmount"]);
                             
            //                W.QuotationItems = GetSalesQuotationItem(W.QuotationID, null);
            //            }
            //        }
            //    }
            //}
            //catch (SqlException sqlEx)
            //{

            //}
            //catch (Exception ex)
            //{

            //}
            return Ws;
        }

        public void IntegrationSalesQuotation(long? WebQuotationID = null)
        {
            //List<PSalesQuotation> Quots = GetSalesQuotationForSAP(WebQuotationID);
            //foreach (PSalesQuotation Quot in Quots)
            //{
            //    UpdateSalesQuotationSapStatus(Quot.QuotationID, new SSalesQuotation().UpdateICTicketRequestedDateToSAP(Quot));
            //}
        }
        public List<PSalesQuotation> GetWebQuotationForSAP(long? WebQuotationID = null)
        {
            List<PSalesQuotation> Ws = new List<PSalesQuotation>();
            //PSalesQuotation W = null;
            //try
            //{
            //    DbParameter WebQuotationIDP = provider.CreateParameter("WebQuotationID", WebQuotationID, DbType.Int64);
            //    DbParameter[] Params = new DbParameter[1] { WebQuotationIDP };
            //    using (DataSet DataSet = provider.Select("ZDMS_GetWebQuotationFoSAP", Params))
            //    {
            //        if (DataSet != null)
            //        {
            //            foreach (DataRow dr in DataSet.Tables[0].Rows)
            //            {
            //                W = new PSalesQuotation();
            //                Ws.Add(W);

            //                W.WebQuotationID = Convert.ToInt64(dr["WebQuotationID"]);
            //                W.SalesOrderNumber = Convert.ToString(dr["SalesOrderNumber"]);
            //                W.SalesOrderDate = dr["SalesOrderDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["SalesOrderDate"]);
            //                W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]) };

            //                W.PrimaryPurchaseOrder = new PDMS_PrimaryPurchaseOrder() { PurchaseOrderNumber = Convert.ToString(dr["PurchaseOrder"]), PurchaseOrderDate = Convert.ToDateTime(dr["PurchaseOrderDate"]) };
            //                //  W.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(dr["DealerID"]), DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };

            //                W.DoNumber = Convert.ToString(dr["DoNumber"]);
            //                W.DoDate = dr["DoDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DoDate"]);
            //                W.DoAmount = dr["DoAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["DoAmount"]);
            //                W.CreditDays = new PDMS_PaymentTerm() { PaymentTerm = Convert.ToString(dr["PaymentTermDescription"]) };
            //                W.AdvanceAmount = dr["AdvanceAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["AdvanceAmount"]);
            //                W.FreightAmount = dr["FreightAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["FreightAmount"]);
            //                W.InvoiceValue = dr["InvoiceValue"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["InvoiceValue"]);
            //                W.Equipment = new PDMS_EquipmentHeader()
            //                {
            //                    HorsePower = Convert.ToString(dr["HorsePower"]),
            //                    ManufacturingDate = dr["ManufacturingDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ManufacturingDate"]),
            //                    EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]),
            //                    ChassisSlNo = Convert.ToString(dr["ChassisSlNo"]),
            //                    EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]),
            //                    Plant = Convert.ToString(dr["Plant"])
            //                };

            //                W.Usage = new PDMS_MainApplication() { MainApplication = Convert.ToString(dr["MainApplication"]) };
            //                W.ShipTo = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["ShipTo"]) };
            //                W.IncoTerm = new PDMS_IncoTerm() { Description = Convert.ToString(dr["IncoTermDescription"]) };
            //                W.Financier = new PDMS_Financier() { FinancierCode = Convert.ToString(dr["FinancierCode"]) };
            //                W.BenificiaryOfDO = Convert.ToString(dr["BenificiaryOfDO"]);
            //                W.SubventionAmount = dr["SubventionAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["SubventionAmount"]);
            //                W.FocServiceKit = Convert.ToString(dr["FocServiceKit"]);
            //                W.RetailCustomer = Convert.ToString(dr["RetailCustomer"]);
            //                W.TRDate = dr["TRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["TRDate"]);

            //                W.ConsolidationInvoicePrint = dr["ConsolidationInvoicePrint"] == DBNull.Value ? false : Convert.ToBoolean(dr["ConsolidationInvoicePrint"]);

            //                W.ModeOfBilling = new PDMS_DiscountType() { DiscountTypeCode = Convert.ToString(dr["DiscountTypeCode"]) };
            //                W.WebQuotationItems = GetWebQuotationItem(W.WebQuotationID, null);


            //            }
            //        }
            //    }
            //}
            //catch (SqlException sqlEx)
            //{

            //}
            //catch (Exception ex)
            //{

            //}
            return Ws;
        }

        public List<PSalesQuotation> GetSalesQuotationBasic(long? SalesQuotationID, long? RefQuotationID, long? LeadID, DateTime? RefQuotationDate
          , string QuotationNo, DateTime? QuotationDateFrom, DateTime? QuotationDateTo, int? QuotationTypeID, int? StatusID, int? DealerID, string CustomerCode)
        {
            string endPoint = "SalesQuotation/SalesQuotationBasic?SalesQuotationID=" + SalesQuotationID + "&RefQuotationID=" + RefQuotationID + "&LeadID=" + LeadID 
                + "&RefQuotationDate=" + RefQuotationDate                + "&QuotationNo=" + QuotationNo + "&QuotationDateFrom=" + QuotationDateFrom 
                + "&QuotationDateTo=" + QuotationDateTo + "&QuotationTypeID=" + QuotationTypeID + "&StatusID=" + StatusID
                + "&DealerID=" + DealerID + "&CustomerCode=" + CustomerCode;
            return JsonConvert.DeserializeObject<List<PSalesQuotation>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }
        public PSalesQuotation GetSalesQuotationByID(long SalesQuotationID)
        {
            string endPoint = "SalesQuotation/SalesQuotationByID?SalesQuotationID=" + SalesQuotationID;
            return JsonConvert.DeserializeObject<PSalesQuotation>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }


        public List<PSalesQuotationType> GetSalesQuotationType(int? QuotationTypeID, string QuotationType)
        {
            string endPoint = "SalesQuotation/Type?QuotationTypeID=" + QuotationTypeID + "&QuotationType=" + QuotationType;
            return JsonConvert.DeserializeObject<List<PSalesQuotationType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PSalesQuotationStatus> GetSalesQuotationStatus(int? SaleQuotationStatusID, string Status)
        {
            string endPoint = "SalesQuotation/Status?SaleQuotationStatusID=" + SaleQuotationStatusID + "&Status=" + Status;
            return JsonConvert.DeserializeObject<List<PSalesQuotationStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PSalesQuotationUserStatus> GetSalesQuotationUserStatus(int? SalesQuotationUserStatusID, string Status)
        {
            string endPoint = "SalesQuotation/UserStatus?SalesQuotationUserStatusID=" + SalesQuotationUserStatusID + "&Status=" + Status;
            return JsonConvert.DeserializeObject<List<PSalesQuotationUserStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PSaleQuotationRejectionReason> GetSaleQuotationRejectionReason(int? SaleQuotationRejectionReasonID, string Reason)
        {
            string endPoint = "SalesQuotation/RejectionReason?SaleQuotationRejectionReasonID=" + SaleQuotationRejectionReasonID + "&Reason=" + Reason;
            return JsonConvert.DeserializeObject<List<PSaleQuotationRejectionReason>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PSalesQuotationNoteList> GetSaleQuotationNoteList(int? SalesQuotationNoteListID, string Note)
        {
            string endPoint = "SalesQuotation/NoteList?SalesQuotationNoteListID=" + SalesQuotationNoteListID + "&Note=" + Note;
            return JsonConvert.DeserializeObject<List<PSalesQuotationNoteList>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PSalesQuotationFollowUp> GetSalesQuotationFollowUpByID(long SalesQuotationID, long? SalesQuotationFollowUpID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "SalesQuotation/FollowUpByID?SalesQuotationID=" + SalesQuotationID + "&SalesQuotationFollowUpID=" + SalesQuotationFollowUpID;
            return JsonConvert.DeserializeObject<List<PSalesQuotationFollowUp>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PSalesQuotationFollowUp> GetSalesQuotationFollowUp(long? SalesQuotationID, int? SalesEngineerUserID, DateTime? From, DateTime? To, int? UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "SalesQuotation/FollowUp?SalesQuotationID=" + SalesQuotationID + "&SalesEngineerUserID=" + SalesEngineerUserID
                + "&From=" + From + "&To=" + To + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PSalesQuotationFollowUp>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PSalesQuotationEffort> GetSalesQuotationEffort(long SalesQuotationID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "SalesQuotation/Effort?SalesQuotationID=" + SalesQuotationID + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PSalesQuotationEffort>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PSalesQuotationExpense> GetSalesQuotationExpense(long SalesQuotationID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "SalesQuotation/Expense?SalesQuotationID=" + SalesQuotationID + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PSalesQuotationExpense>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }

        public PApiResult CreateQuotationInPartsPortal(long SalesQuotationID)
        {

            string endPoint = "SalesQuotation/CreateQuotationInPartsPortal?SalesQuotationID=" + SalesQuotationID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));


            //PSalesQuotation SQ = GetSalesQuotationByID(SalesQuotationID);
            ////PDMS_SalesOrderJSON SalesOrder = new PDMS_SalesOrderJSON(); 
            //try
            //{
            //    List<string> Query = new List<string>();
            //    DataTable dtLocation = new NpgsqlServer().ExecuteReader("SELECT  p_office, p_location FROM  dmmer_location where r_default = true and   s_tenant_id = " + SQ.Lead.Dealer.DealerCode + " limit 1");

            //    string Location = "";
            //    string Office = "";

            //    if (dtLocation.Rows.Count == 1)
            //    {
            //        Location = Convert.ToString(dtLocation.Rows[0]["p_location"]);
            //        Office = Convert.ToString(dtLocation.Rows[0]["p_office"]);
            //    }
            //    DataTable QuotationNumberCheck = new NpgsqlServer().ExecuteReader("SELECT count(*) FROM  dssor_sales_order_hdr where r_ext_id = '" + SQ.RefQuotationNo + "' and   s_tenant_id = " + SQ.Lead.Dealer.DealerCode + " limit 1");

            //    if (Convert.ToInt32(QuotationNumberCheck.Rows[0][0]) != 0)
            //    {
            //        return;
            //    }
            //    Query = QuotationForPartsPortal(SQ, Location, Office);

            //    if (new NpgsqlServer().UpdateTransactionsJSNQuotation(Query, SQ.Lead.Dealer.DealerCode))
            //    {
            //    }
            //}
            //catch (Exception ex)
            //{
            //    new FileLogger().LogMessageService("BDMS_SalesOrder", "CreateQuotationForJSN", ex);
            //    throw ex;
            //}
        }

        // List<string> QuotationForPartsPortal(PSalesQuotation SQ, string Location, string Office)
        //{

        //    // -f_po_id,f_bill_to,s_modified_by
        //    List<string> Query = new List<string>();

        //    string Query1S = "insert into dssor_sales_order_hdr (s_establishment,p_so_id,r_order_date";
        //    string Query1V = " VALUES (1000,'@@QuotationNumber'," + SQ.RefQuotationDate;

        //    Query1S = Query1S + ",s_tenant_id";
        //    Query1V = Query1V + "," + SQ.Lead.Dealer.DealerCode;

        //    Query1S = Query1S + ",f_customer_id";
        //    Query1V = Query1V + ",'" + SQ.Lead.Customer.CustomerCode + "'";

        //    Query1S = Query1S + ",f_ship_to";
        //    Query1V = Query1V + ",'" + SQ.Lead.Customer.CustomerCode + "01_1" + "'";

        //    Query1S = Query1S + ",f_location";
        //    Query1V = Query1V + ",'" + Location + "'";

        //    Query1S = Query1S + ",f_currency";
        //    Query1V = Query1V + ",'" + "INR" + "'";

        //    Query1S = Query1S + ",f_office";
        //    Query1V = Query1V + ",'" + Office + "'";


        //    Query1S = Query1S + ",r_ext_id";
        //    Query1V = Query1V + ",'" + SQ.RefQuotationNo + "'";


        //    //Query1S = Query1S + ",r_tax_amt";
        //    //Query1V = Query1V + "," + SalesOrderResults.r_tax_amt;

        //    Query1S = Query1S + ",r_discount_amt_additional";
        //    Query1V = Query1V + ",0";

        //    //if (!string.IsNullOrEmpty(SalesOrderResults.s_created_on))
        //    //{
        //    //    Query1S = Query1S + ",s_created_on";
        //    //    Query1V = Query1V + ",'" + SalesOrderResults.s_created_on + "'";
        //    //}

        //    Query1S = Query1S + ",s_status";
        //    Query1V = Query1V + ",'QUOTATION'";

        //    Query1S = Query1S + ",r_net_amt";
        //    Query1V = Query1V + ",0";

        //    Query1S = Query1S + ",r_discount_amt";
        //    Query1V = Query1V + ",0";

        //    Query1S = Query1S + ",f_order_type";
        //    Query1V = Query1V + ",208";
        //    Query1S = Query1S + ",s_object_type";
        //    Query1V = Query1V + ",208";


        //    Query1S = Query1S + ",r_exp_del_date";
        //    Query1V = Query1V + ",'" + SQ.RequestedDeliveryDate + "'";


        //    Query1S = Query1S + ",f_sales_office";
        //    Query1V = Query1V + ",'KA10'";

        //    Query1S = Query1S + ",channel";
        //    Query1V = Query1V + ",'LG'";


        //    Query1S = Query1S + ",f_division";
        //    Query1V = Query1V + ",'" + SQ.QuotationItems[0].Material.Model.Division.DivisionCode + "'";

        //    Query1S = Query1S + ",r_activity_id";
        //    Query1V = Query1V + ",1";



        //    Query1S = Query1S + ",r_req_del_date";
        //    Query1V = Query1V + ",'" + SQ.RequestedDeliveryDate + "'";



        //    //if (!string.IsNullOrEmpty(SalesOrderResults.f_ship_to))
        //    //{
        //    //    Query1S = Query1S + ",f_ship_to";
        //    //    Query1V = Query1V + ",'" + SalesOrderResults.f_ship_to + "'";
        //    //}



        //    Query1S = Query1S + ",r_gross_amt";
        //    Query1V = Query1V + "," + SQ.GrossValue;

        //    //Query1S = Query1S + ",r_tcs_amt";
        //    //Query1V = Query1V + "," + SQ.TCSValue;

        //    Query1S = Query1S + ",r_cess_amt";
        //    Query1V = Query1V + ",0";


        //    Query1S = Query1S + " )";
        //    Query1V = Query1V + " )";
        //    Query.Add(Query1S + Query1V);

        //    foreach (PSalesQuotationItem Item in SQ.QuotationItems)
        //    {
        //        string Query1SItem = "insert into dssor_sales_order_item (s_establishment,p_so_id";
        //        string Query1VItem = " VALUES (1000,'@@QuotationNumber'";

        //        Query1SItem = Query1SItem + ",s_tenant_id";
        //        Query1VItem = Query1VItem + "," + SQ.Lead.Dealer.DealerCode;


        //        Query1SItem = Query1SItem + ",p_so_item";
        //        Query1VItem = Query1VItem + "," + Item.Item;


        //        Query1SItem = Query1SItem + ",f_location";
        //        Query1VItem = Query1VItem + ",'" + Location + "'";

        //        //if (!string.IsNullOrEmpty(SalesOrderItems.s_created_on))
        //        //{
        //        //    Query1SItem = Query1SItem + ",s_created_on";
        //        //    Query1VItem = Query1VItem + ",'" + SalesOrderItems.s_created_on + "'";
        //        //}

        //        //if (!string.IsNullOrEmpty(SalesOrderItems.s_created_by))
        //        //{
        //        //    Query1SItem = Query1SItem + ",s_created_by";
        //        //    Query1VItem = Query1VItem + ",'" + SalesOrderItems.s_created_by + "'";
        //        //}


        //        Query1SItem = Query1SItem + ",f_uom";
        //        Query1VItem = Query1VItem + ",'" + Item.Material.BaseUnit + "'";


        //        Query1SItem = Query1SItem + ",r_tax_amt";
        //        Query1VItem = Query1VItem + "," + Item.CGSTValue + Item.SGSTValue + Item.IGSTValue;

        //        Query1SItem = Query1SItem + ",s_status";
        //        Query1VItem = Query1VItem + ",'QUOTATION'";

        //        Query1SItem = Query1SItem + ",r_hgl_item";
        //        Query1VItem = Query1VItem + ",0";

        //        Query1SItem = Query1SItem + ",f_office";
        //        Query1VItem = Query1VItem + ",'" + Office + "'";

        //        Query1SItem = Query1SItem + ",r_exp_del_date";
        //        Query1VItem = Query1VItem + ",'" + SQ.RequestedDeliveryDate + "'";

        //        Query1SItem = Query1SItem + ",f_oem_id";
        //        Query1VItem = Query1VItem + ",'AF'";

        //        Query1SItem = Query1SItem + ",f_material_id";
        //        Query1VItem = Query1VItem + ",'" + Item.Material.MaterialCode + "'";

        //        Query1SItem = Query1SItem + ",r_order_qty";
        //        Query1VItem = Query1VItem + "," + Item.Qty;

        //        Query1SItem = Query1SItem + ",r_item_type";
        //        Query1VItem = Query1VItem + ",0";

        //        Query1SItem = Query1SItem + ",r_add_discount_amt";
        //        Query1VItem = Query1VItem + ",0";

        //        Query1SItem = Query1SItem + ",r_net_amt";
        //        Query1VItem = Query1VItem + ",0";

        //        Query1SItem = Query1SItem + ",f_mat_division";
        //        Query1VItem = Query1VItem + ",'" + Item.Material.Model.Division.DivisionCode + "'";

        //        Query1SItem = Query1SItem + ",d_material_desc";
        //        Query1VItem = Query1VItem + ",'" + Item.Material.MaterialDescription + "'";

        //        // is_ack r_delv_qty 

        //        Query1SItem = Query1SItem + ",r_pending_qty";
        //        Query1VItem = Query1VItem + ",0";

        //        Query1SItem = Query1SItem + ",r_gross_amt";
        //        Query1VItem = Query1VItem + "," + Item.CGSTValue + Item.SGSTValue + Item.IGSTValue + Item.TaxableValue;

        //        Query1SItem = Query1SItem + ",r_discount_amt";
        //        Query1VItem = Query1VItem + ",0";

        //        Query1SItem = Query1SItem + ",s_object_type";
        //        Query1VItem = Query1VItem + ",208";

        //        Query1SItem = Query1SItem + ",channel";
        //        Query1VItem = Query1VItem + ",'LG'";

        //        //  r_tcs_amt   r_cess_amt r_delv_amt  r_stock_qqty r_stock_oqty 

        //        Query1SItem = Query1SItem + " )";
        //        Query1VItem = Query1VItem + " )";
        //        Query.Add(Query1SItem + Query1VItem);

        //        //foreach (PDMS_dssor_sales_order_item_condsJSON SOIC in SalesOrderItems.dssor_sales_order_item_conds)
        //        //{

        //        //    string Query1SItemC = "insert into dssor_sales_order_cond (s_establishment,p_so_id";
        //        //    string Query1VItemC = " VALUES (1000,'@@QuotationNumber'";

        //        //    if (!string.IsNullOrEmpty(SOIC.p_so_item))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",p_so_item";
        //        //        Query1VItemC = Query1VItemC + "," + SOIC.p_so_item;
        //        //    }

        //        //    Query1SItemC = Query1SItemC + ",s_tenant_id";
        //        //    Query1VItemC = Query1VItemC + "," + DealerCode;

        //        //    if (!string.IsNullOrEmpty(SOIC.p_condition_type))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",p_condition_type";
        //        //        Query1VItemC = Query1VItemC + ",'" + SOIC.p_condition_type + "'";
        //        //    }

        //        //    if (!string.IsNullOrEmpty(SOIC.f_currency))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",f_currency";
        //        //        Query1VItemC = Query1VItemC + ",'" + SOIC.f_currency + "'";
        //        //    }

        //        //    if (!string.IsNullOrEmpty(SOIC.r_cond_amt))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",r_cond_amt";
        //        //        if (SOIC.r_cond_amt.Contains('-'))
        //        //        {
        //        //            Query1VItemC = Query1VItemC + ",-" + SOIC.r_cond_amt.Replace("-", "");
        //        //        }
        //        //        else
        //        //        {
        //        //            Query1VItemC = Query1VItemC + "," + SOIC.r_cond_amt;
        //        //        }
        //        //    }

        //        //    if (!string.IsNullOrEmpty(SOIC.r_base_amt))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",r_base_amt";
        //        //        Query1VItemC = Query1VItemC + "," + SOIC.r_base_amt;
        //        //    }
        //        //    if (!string.IsNullOrEmpty(SOIC.r_order_qty))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",r_order_qty";
        //        //        Query1VItemC = Query1VItemC + "," + SOIC.r_order_qty;
        //        //    }

        //        //    if (!string.IsNullOrEmpty(SOIC.r_pric_date))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",r_pric_date";
        //        //        Query1VItemC = Query1VItemC + ",'" + SOIC.r_pric_date + "'";
        //        //    }
        //        //    if (!string.IsNullOrEmpty(SOIC.s_created_by))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",s_created_by";
        //        //        Query1VItemC = Query1VItemC + ",'" + SOIC.s_created_by + "'";
        //        //    }
        //        //    if (!string.IsNullOrEmpty(SOIC.s_created_on))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",s_created_on";
        //        //        Query1VItemC = Query1VItemC + ",'" + SOIC.s_created_on + "'";
        //        //    }
        //        //    if (!string.IsNullOrEmpty(SOIC.r_cond_grp))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",r_cond_grp";
        //        //        Query1VItemC = Query1VItemC + ",'" + SOIC.r_cond_grp + "'";
        //        //    }
        //        //    if (!string.IsNullOrEmpty(SOIC.d_cond_desc))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",d_cond_desc";
        //        //        Query1VItemC = Query1VItemC + ",'" + SOIC.d_cond_desc + "'";
        //        //    }
        //        //    if (!string.IsNullOrEmpty(SOIC.r_cond_cls))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",r_cond_cls";
        //        //        Query1VItemC = Query1VItemC + ",'" + SOIC.r_cond_cls + "'";
        //        //    }
        //        //    if (!string.IsNullOrEmpty(SOIC.f_percentage))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",f_percentage";
        //        //        Query1VItemC = Query1VItemC + "," + SOIC.f_percentage;
        //        //    }

        //        //    //  s_sync_status s_action     

        //        //    if (!string.IsNullOrEmpty(SOIC.channel))
        //        //    {
        //        //        Query1SItemC = Query1SItemC + ",channel";
        //        //        Query1VItemC = Query1VItemC + ",'" + SOIC.channel + "'";
        //        //    }


        //        //    Query1SItemC = Query1SItemC + " )";
        //        //    Query1VItemC = Query1VItemC + " )";

        //        //    Query.Add(Query1SItemC + Query1VItemC);
        //        //}
        //    }
        //    return Query;
        //}
    }
}
