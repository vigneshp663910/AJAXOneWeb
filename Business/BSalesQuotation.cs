using DataAccess;
using Newtonsoft.Json;
using Properties; 
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

        public PApiResult GetSalesQuotationBasic(long? SalesQuotationID, string QuotationNo, string QuotationDateFrom, string QuotationDateTo
            , long? LeadID,string LeadNumber, int? StatusID, int? UserStatusID,int? ProductTypeID,int? ProductID, int? DealerID, int? SalesEngineerID, string CustomerCode, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "SalesQuotation/SalesQuotationBasic?SalesQuotationID=" + SalesQuotationID + "&QuotationNo=" + QuotationNo + "&QuotationDateFrom=" + QuotationDateFrom  + "&QuotationDateTo=" + QuotationDateTo 
                + "&LeadID=" + LeadID + "&LeadNumber=" + LeadNumber
                + "&StatusID=" + StatusID + "&UserStatusID=" + UserStatusID + "&ProductTypeID=" + ProductTypeID + "&ProductID=" + ProductID
                + "&DealerID=" + DealerID + "&SalesEngineerID=" + SalesEngineerID
                + "&CustomerCode=" + CustomerCode + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));

        }
        public PApiResult GetSalesQuotationExcel(long? SalesQuotationID, string QuotationNo, string QuotationDateFrom, string QuotationDateTo
           , long? LeadID, string LeadNumber, int? StatusID, int? UserStatusID, int? ProductTypeID, int? ProductID, int? DealerID, int? SalesEngineerID, string CustomerCode)
        {
            string endPoint = "SalesQuotation/SalesQuotationExcel?SalesQuotationID=" + SalesQuotationID + "&QuotationNo=" + QuotationNo + "&QuotationDateFrom=" + QuotationDateFrom + "&QuotationDateTo=" + QuotationDateTo
                + "&LeadID=" + LeadID + "&LeadNumber=" + LeadNumber
                + "&StatusID=" + StatusID + "&UserStatusID=" + UserStatusID + "&ProductTypeID=" + ProductTypeID + "&ProductID=" + ProductID
                + "&DealerID=" + DealerID + "&SalesEngineerID=" + SalesEngineerID            + "&CustomerCode=" + CustomerCode ;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));

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

        public PApiResult CreateQuotationInSap(long SalesQuotationID)
        {

            string endPoint = "SalesQuotation/CreateQuotationInSap?SalesQuotationID=" + SalesQuotationID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
             
        }
        public PApiResult CreateQuotationInPartsPortal(long SalesQuotationID)
        {
            string endPoint = "SalesQuotation/CreateQuotationInPartsPortal?SalesQuotationID=" + SalesQuotationID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public Boolean InsertSalesQuotationRevision(PSalesQuotation Quotation, string SoldToAddress1, string SoldToAddress2, string ShipToAddress1, string ShipToAddress2, string KindAttention,
            string Hypothecation, string Reference, string TermsOfPayment, string Delivery, string QNote, string Validity,decimal GrandTotal)
        {
            int success = 0;
            DbParameter SalesQuotationIDP = provider.CreateParameter("SalesQuotationID", Quotation.QuotationID, DbType.Int32);
            DbParameter SoldToAddress1P = provider.CreateParameter("SoldToAddress1", SoldToAddress1, DbType.String);
            DbParameter SoldToAddress2P = provider.CreateParameter("SoldToAddress2", SoldToAddress2, DbType.String);
            DbParameter ShipToAddress1P = provider.CreateParameter("ShipToAddress1", ShipToAddress1, DbType.String);
            DbParameter ShipToAddress2P = provider.CreateParameter("ShipToAddress2", ShipToAddress2, DbType.String);
            DbParameter KindAttentionP = provider.CreateParameter("KindAttention", KindAttention, DbType.String);
            DbParameter HypothecationP = provider.CreateParameter("Hypothecation", Hypothecation, DbType.String);
            DbParameter ReferenceP = provider.CreateParameter("Reference", Reference, DbType.String);
            DbParameter TermsOfPaymentP = provider.CreateParameter("TermsOfPayment", TermsOfPayment, DbType.String);
            DbParameter DeliveryP = provider.CreateParameter("Delivery", Delivery, DbType.String);
            DbParameter QNoteP = provider.CreateParameter("QNote", QNote, DbType.String);
            DbParameter ValidityP = provider.CreateParameter("Validity", Validity, DbType.String);
            DbParameter GrandTotalP = provider.CreateParameter("GrandTotal", GrandTotal.ToString("0.000"), DbType.Decimal);
            DbParameter OutValue = provider.CreateParameter("OutValue", 0, DbType.Int32, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[14] { SalesQuotationIDP, SoldToAddress1P, SoldToAddress2P, ShipToAddress1P, ShipToAddress2P, 
                KindAttentionP,HypothecationP,ReferenceP,TermsOfPaymentP,DeliveryP,QNoteP,ValidityP,GrandTotalP,OutValue};
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("InsertSalesQuotationRevision", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BSalesQuotation", "InsertSalesQuotationRevision", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BSalesQuotation", " InsertSalesQuotationRevision", ex);
                return false;
            }
            return true;
        }
        //public int GetSalesQuotationFlow()
        //{
        //    TraceLogger.Log(DateTime.Now);
        //    List<PSalesQuotationDocumentDetails> SalesQuotationDocumentDetails = new List<PSalesQuotationDocumentDetails>();
        //    try
        //    {
        //        SalesQuotationDocumentDetails = new SQuotation().GetSalesQuotationFlow();

        //        foreach (PSalesQuotationDocumentDetails SalesQuotationDocumentDetail in SalesQuotationDocumentDetails)
        //        {
        //            DbParameter QuotationNo = provider.CreateParameter("QuotationNo", SalesQuotationDocumentDetail.QuotationNo, DbType.String);                                        
        //            DbParameter DocumentNumber = provider.CreateParameter("DocumentNumber", SalesQuotationDocumentDetail.DocumentNumber, DbType.String);
        //            DbParameter DocumentCode = provider.CreateParameter("DocumentCode", SalesQuotationDocumentDetail.DocumentCode, DbType.String);
        //            DbParameter DocumentName = provider.CreateParameter("DocumentName", SalesQuotationDocumentDetail.DocumentName, DbType.String);
        //            DbParameter DocumentDate = provider.CreateParameter("DocumentDate", SalesQuotationDocumentDetail.DocumentDate, DbType.DateTime);
        //            DbParameter Material = provider.CreateParameter("Material", SalesQuotationDocumentDetail.Material, DbType.String);                    
        //            DbParameter Item = provider.CreateParameter("Item", SalesQuotationDocumentDetail.Item, DbType.String);
        //            DbParameter SubSequentItem = provider.CreateParameter("SubSequentItem", SalesQuotationDocumentDetail.SubSequentItem, DbType.String);
        //            DbParameter MachineSerialNumber = provider.CreateParameter("MachineSerialNumber", SalesQuotationDocumentDetail.MachineSerialNumber, DbType.String);

        //            DbParameter[] Params = new DbParameter[9] { QuotationNo, DocumentNumber, DocumentCode, DocumentName, DocumentDate, Material, Item, SubSequentItem, MachineSerialNumber };
        //            try
        //            {
        //                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //                {
        //                    provider.Insert("InsertOrUpdateSalesQuotationDocumentDetails", Params);
        //                    scope.Complete();
        //                    new SQuotation().UpdateStatusSalesQuotationFlow(SalesQuotationDocumentDetail);
        //                }

        //            }
        //            catch (SqlException sqlEx)
        //            {
        //                new FileLogger().LogMessage("BSalesQuotation", "InsertOrUpdateSalesQuotationDocumentDetails", sqlEx);

        //                throw;
        //            }
        //            catch (Exception ex)
        //            {
        //                new FileLogger().LogMessage("BSalesQuotation", " InsertOrUpdateSalesQuotationDocumentDetails", ex);
        //                throw;
        //            }
        //        }

        //        TraceLogger.Log(DateTime.Now);
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BSalesQuotation", "InsertOrUpdateSalesQuotationDocumentDetails", ex);
        //    }
        //    return SalesQuotationDocumentDetails.Count();
        //}



        public PSalesQuotationItem getMaterialTaxForQuotation(PSalesQuotation Quotation, string MaterialCode, Boolean IsWarrenty, decimal qty)
        {
            PSalesQuotation_sap_MaterialTax Tax = new PSalesQuotation_sap_MaterialTax();
            Tax.CustomerCode = Quotation.Lead.Customer.CustomerCode;
            Tax.CustomerStateCode = Quotation.Lead.Customer.State.StateCode;
            Tax.DealerStateCode = Quotation.Lead.Dealer.StateCode;
            Tax.MaterialCode = MaterialCode;
            Tax.IsWarrenty = IsWarrenty;
            Tax.qty = qty;
            Tax.QuotationItems = new List<PSalesQuotationItem_sap_MaterialTax>();
            foreach (PSalesQuotationItem item in Quotation.QuotationItems)
            {
                Tax.QuotationItems.Add(new PSalesQuotationItem_sap_MaterialTax()
                {
                    MaterialCode = item.Material.MaterialCode,
                    Qty = item.Qty
                });
            }
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/getMaterialTaxForQuotation", Tax));
            if (Results.Status == PApplication.Failure)
            {
                throw new Exception( Results.Message); 
            }
            return JsonConvert.DeserializeObject<PSalesQuotationItem>(JsonConvert.SerializeObject(Results.Data));
        }
        public DataTable getMaterialTextForQuotation(string MaterialCode)
        {
            string endPoint = "SalesQuotation/getMaterialTextForQuotation?MaterialCode=" + MaterialCode;

            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            
        }


        public PSalesQuotationCustomerSinged GetSalesQuotationCustomerSinged(long SalesQuotationID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "SalesQuotation/GetSalesQuotationCustomerSinged?SalesQuotationID=" + SalesQuotationID ;
            return JsonConvert.DeserializeObject<PSalesQuotationCustomerSinged>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);
        }
        public PAttachedFile AttachedFileSalesQuotationCustomerSingedForDownload(string DocumentName)
        {
            string endPoint = "SalesQuotation/AttachedFileSalesQuotationCustomerSingedForDownload?DocumentName=" + DocumentName;
            return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
        }
        public List<PQuotationSpecification> GetQuotationSpecification(long? SalesQuotationIDP)
        {
            string endPoint = "SalesQuotation/GetQuotationSpecification?SalesQuotationIDP=" + SalesQuotationIDP;
            return JsonConvert.DeserializeObject<List<PQuotationSpecification>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
    [Serializable]
    public class PSalesQuotation_sap_MaterialTax
    {
        public List<PSalesQuotationItem_sap_MaterialTax> QuotationItems { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerStateCode { get; set; }
        public string DealerStateCode { get; set; }

        public string MaterialCode { get; set; }
        public Boolean IsWarrenty { get; set; }
        public decimal qty { get; set; }
    }
    [Serializable]
    public class PSalesQuotationItem_sap_MaterialTax
    {
        public string MaterialCode { get; set; }
        public int Qty { get; set; }
    }
}
