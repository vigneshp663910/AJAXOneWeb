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

namespace Business
{
    class BDMS_PrimaryMCSalesOrder
    {
        private IDataAccess provider;
        public BDMS_PrimaryMCSalesOrder()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public PDMS_PrimaryMCSalesOrder GetPrimaryMCSalesOrderByPrimaryMCSalesOrderID(long PrimaryMCSalesOrderID)
        {
            PDMS_PrimaryMCSalesOrder SO = new PDMS_PrimaryMCSalesOrder();
            DbParameter PrimaryMCSalesOrderIDP = provider.CreateParameter("PrimaryMCSalesOrderID", PrimaryMCSalesOrderID, DbType.Int64);
            DbParameter[] Params = new DbParameter[1] { PrimaryMCSalesOrderIDP };
            try
            {
                using (DataSet DS = provider.Select("ZDMS_GetDealer", Params))
                {
                    if (DS != null)
                    {
                        DataRow Dr = DS.Tables[0].Rows[0];

                        SO.PrimaryMCSalesOrderID = Convert.ToInt64(Dr["PrimaryMCSalesOrderID"]);
                        SO.Dealer = new PDMS_Dealer()
                        {
                            DealerID = Convert.ToInt32(Dr["DealerID"]), 
                            DealerCode = Convert.ToString(Dr["DealerCode"]),
                            DealerName = Convert.ToString(Dr["DealerName"]) 
                        };
                        SO.Customer = new PDMS_Customer() 
                        {
                            CustomerID = Convert.ToInt32(Dr["CustomerID"]),
                            CustomerCode = Convert.ToString(Dr["CustomerCode"]),
                            CustomerName = Convert.ToString(Dr["CustomerName"]) 
                        };
                        SO.Equipment = new PDMS_EquipmentHeader()
                        {
                            EquipmentHeaderID = Convert.ToInt32(Dr["EquipmentHeaderID"]),
                            EquipmentSerialNo = Convert.ToString(Dr["EquipmentSerialNo"]),
                            EngineSerialNo = Convert.ToString(Dr["EngineSerialNo"]),
                            TypeOfWheelAssembly = Convert.ToString(Dr["TypeOfWheelAssembly"]),
                            ChassisSlNo = Convert.ToString(Dr["ChassisSlNo"]),
                            ESN = Convert.ToString(Dr["ESN"]),
                            Plant = Convert.ToString(Dr["Plant"]),
                            Dispatch = Convert.ToString(Dr["Dispatch"]),
                            ManufacturingDate = Convert.ToDateTime(Dr["ManufacturingDate"]),
                            EquipmentModel = new PDMS_Model()
                            {
                                ModelID = Convert.ToInt32(Dr["ModelID"]),
                                Model = Convert.ToString(Dr["ModelCode"]),
                                ModelDescription = Convert.ToString(Dr["ModelDescription"]),
                            },
                            Material = new PDMS_Material()
                            {
                                MaterialID = Convert.ToInt32(Dr["ModelID"]),
                                MaterialCode = Convert.ToString(Dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(Dr["MaterialDescription"]),
                            }
                        };

                        SO.PurchaseOrder = Convert.ToString(Dr["PurchaseOrder"]);
                        SO.PurchaseOrderDate = Convert.ToDateTime(Dr["PurchaseOrderDate"]);
                        SO.ShipTo = new PDMS_Address()
                        {
                            State = new PDMS_State() { State = Convert.ToString(Dr["ShipToState"]) },
                            District = new PDMS_District() { District = Convert.ToString(Dr["ShipToDistrict"]) },
                            City = Convert.ToString(Dr["ShipToCity"]),
                            Address1 = Convert.ToString(Dr["ShipToAddress1"]),
                            Address2 = Convert.ToString(Dr["ShipToAddress2"]),
                            PostalCode = Convert.ToString(Dr["ShipToPostalCode"])
                        };
                        SO.FinacierCode = Convert.ToString(Dr["FinacierCode"]);
                        SO.MachineQuantity = Convert.ToInt32(Dr["MachineQuantity"]);
                        SO.BasicPrice = Convert.ToDecimal(Dr["BasicPrice"]);
                        SO.Discount = Convert.ToDecimal(Dr["Discount"]);
                        SO.InvoiceValue = Convert.ToDecimal(Dr["InvoiceValue"]);
                        SO.DoNumber = Convert.ToString(Dr["DoNumber"]);
                        SO.DoDate = Convert.ToDateTime(Dr["DoDate"]);
                        SO.DoAmount = Convert.ToDecimal(Dr["DoAmount"]);
                        SO.TermsOfPayment = new PDMS_PaymentTerm()
                        {
                            PaymentTermID = Convert.ToInt32(Dr["PaymentTermID"]),
                            PaymentTerm = Convert.ToString(Dr["PaymentTerm"]),
                            Description =  Convert.ToString(Dr["PaymentTermDescription"])
                        };
                        SO.MarginMoney = Convert.ToDecimal(Dr["MarginMoney"]);
                        SO.DiscountType = new PDMS_DiscountType()
                        {
                            DiscountTypeID = Convert.ToInt32(Dr["DiscountTypeID"]),
                            DiscountType = Convert.ToString(Dr["DiscountType"]),
                            DiscountTypeCode = Convert.ToString(Dr["DiscountTypeCode"])
                        };
                        SO.IncoTerms = new PIncoTerms()
                        {
                            IncoTermsID = Convert.ToInt32(Dr["IncoTermID"]),
                            IncoTerms = Convert.ToString(Dr["IncoTerm"]),
                            Description = Convert.ToString(Dr["IncoTermDescription"])
                        };
                        SO.AdvanceAmount = Convert.ToDecimal(Dr["AdvanceAmount"]);
                        SO.FinancierAmount = Convert.ToDecimal(Dr["FinancierAmount"]);
                        SO.FreightAmount = Convert.ToDecimal(Dr["FreightAmount"]);
                        SO.BenificiaryOfDO = Convert.ToString(Dr["BenificiaryOfDO"]);
                        SO.SubventionAmount = Convert.ToDecimal(Dr["SubventionAmount"]);
                        SO.Usage = new PDMS_MainApplication()
                        {
                            MainApplicationID = Convert.ToInt32(Dr["MainApplicationID"]),
                            MainApplication = Convert.ToString(Dr["MainApplication"])
                        };

                        SO.TRDate = Convert.ToDateTime(Dr["TRDate"]);
                        SO.HorsePower = Convert.ToString(Dr["HorsePower"]);
                        SO.RetailCustomer = Convert.ToString(Dr["RetailCustomer"]);
                        SO.ConsolidationInvoicePrint = Convert.ToString(Dr["ConsolidationInvoicePrint"]);
                        SO.Hypothecation = Convert.ToString(Dr["Hypothecation"]);
                        SO.BackToBackDoEndorsedToAjax = Convert.ToString(Dr["BackToBackDoEndorsedToAjax"]);
                        SO.SpecialRequirements = Convert.ToString(Dr["SpecialRequirements"]);
                        SO.FocServiceKit = Convert.ToString(Dr["FocServiceKit"]);
                        SO.FocWheelAssy = Convert.ToString(Dr["FocWheelAssy"]);
                        SO.FocExtensionChutes = Convert.ToString(Dr["FocExtensionChutes"]);
                        SO.FocOthers = Convert.ToString(Dr["FocOthers"]);
                        SO.SourceOfEnquiry = new PDMS_SourceOfEnquiry() { SourceOfEnquiryID = Convert.ToInt32(Dr["SourceOfEnquiryID"]), SourceOfEnquiry = Convert.ToString(Dr["SourceOfEnquiry"]) };

                        SO.ReasonForOrderConversion = Convert.ToString(Dr["ReasonForOrderConversion"]);
                        SO.CustomerType = Convert.ToString(Dr["CustomerType"]);
                        SO.Profile = Convert.ToString(Dr["Profile"]);
                        SO.Size = Convert.ToString(Dr["Size"]);
                        SO.OwnershipPattern = Convert.ToString(Dr["OwnershipPattern"]);
                        SO.NameOfTheProject = Convert.ToString(Dr["NameOfTheProject"]);
                        SO.TransportationAndInsurance = Convert.ToString(Dr["TransportationAndInsurance"]);
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return SO;
        }

        public int InsertOrUpdatePrimaryMCSalesOrder(PDMS_PrimaryMCSalesOrder SO, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int EmployeeID = 0;
            try
            {
                DbParameter PrimaryMCSalesOrderID = provider.CreateParameter("PrimaryMCSalesOrderID", SO.PrimaryMCSalesOrderID, DbType.Int32);
                DbParameter DealerID = provider.CreateParameter("DealerID", SO.Dealer.DealerID, DbType.Int32);
                DbParameter CustomerID = provider.CreateParameter("CustomerID", SO.Customer.CustomerID, DbType.Int32);
                DbParameter EquipmentHeaderID = provider.CreateParameter("EquipmentHeaderID", SO.Equipment.EquipmentHeaderID, DbType.Int32);
                DbParameter PurchaseOrder = provider.CreateParameter("PurchaseOrder", SO.PurchaseOrder, DbType.String);
                DbParameter PurchaseOrderDate = provider.CreateParameter("PurchaseOrderDate", SO.PurchaseOrderDate, DbType.DateTime);

                DbParameter ShipToStateID = provider.CreateParameter("ShipToStateID", SO.ShipTo.State.StateID, DbType.Int32);
                DbParameter ShipToDistrictID = provider.CreateParameter("ShipToDistrictID", SO.ShipTo.District.DistrictID, DbType.Int32);
                DbParameter ShipToCity = provider.CreateParameter("ShipToCity", SO.ShipTo.City, DbType.String);
                DbParameter ShipToAddress1 = provider.CreateParameter("ShipToAddress1", SO.ShipTo.Address1, DbType.String);
                DbParameter ShipToAddress2 = provider.CreateParameter("ShipToAddress2", SO.ShipTo.Address2, DbType.String);
                DbParameter ShipToPostalCode = provider.CreateParameter("ShipToPostalCode", SO.ShipTo.PostalCode, DbType.String);

                DbParameter FinacierCode = provider.CreateParameter("FinacierCode", SO.FinacierCode, DbType.String);
                DbParameter MachineQuantity = provider.CreateParameter("MachineQuantity", SO.MachineQuantity, DbType.Int32);
                DbParameter BasicPrice = provider.CreateParameter("BasicPrice", SO.BasicPrice, DbType.Decimal);
                DbParameter Discount = provider.CreateParameter("Discount", SO.Discount, DbType.Decimal);
                DbParameter DoNumber = provider.CreateParameter("DoNumber", SO.DoNumber, DbType.String);
                DbParameter DoDate = provider.CreateParameter("DoDate", SO.DoDate, DbType.DateTime);
                DbParameter DoAmount = provider.CreateParameter("DoAmount", SO.DoAmount, DbType.Decimal);
                DbParameter TermsOfPaymentID = provider.CreateParameter("TermsOfPaymentID", SO.TermsOfPayment.PaymentTermID, DbType.Int32);
                DbParameter MarginMoney = provider.CreateParameter("MarginMoney", SO.MarginMoney, DbType.Decimal);

                DbParameter DiscountTypeID = provider.CreateParameter("DiscountTypeID", SO.DiscountType.DiscountTypeID, DbType.Int32);
                DbParameter IncotermsID = provider.CreateParameter("IncotermsID", SO.IncoTerms.IncoTermsID, DbType.Int32);
                DbParameter FinancierAmount = provider.CreateParameter("FinancierAmount", SO.FinancierAmount, DbType.Decimal);
                DbParameter FreightAmount = provider.CreateParameter("FreightAmount", SO.FreightAmount, DbType.Decimal);
                DbParameter BenificiaryOfDO = provider.CreateParameter("BenificiaryOfDO", SO.BenificiaryOfDO, DbType.String);
                DbParameter SubventionAmount = provider.CreateParameter("SubventionAmount", SO.SubventionAmount, DbType.Decimal);

                DbParameter UsageID = provider.CreateParameter("UsageID", SO.Usage.MainApplicationID, DbType.Int32);
                DbParameter TRDate = provider.CreateParameter("TRDate", SO.TRDate, DbType.DateTime);
                DbParameter HorsePower = provider.CreateParameter("HorsePower", SO.HorsePower, DbType.String);
                DbParameter RetailCustomer = provider.CreateParameter("RetailCustomer", SO.RetailCustomer, DbType.String);
                DbParameter ConsolidationInvoicePrint = provider.CreateParameter("ConsolidationInvoicePrint", SO.ConsolidationInvoicePrint, DbType.String);
                DbParameter Hypothecation = provider.CreateParameter("Hypothecation", SO.Hypothecation, DbType.String);
                DbParameter BackToBackDoEndorsedToAjax = provider.CreateParameter("BackToBackDoEndorsedToAjax", SO.BackToBackDoEndorsedToAjax, DbType.String);
                DbParameter SpecialRequirements = provider.CreateParameter("SpecialRequirements", SO.SpecialRequirements, DbType.String);
                DbParameter FocServiceKit = provider.CreateParameter("FocServiceKit", SO.FocServiceKit, DbType.String);

                DbParameter FocWheelAssy = provider.CreateParameter("FocWheelAssy", SO.FocWheelAssy, DbType.String);
                DbParameter FocExtensionChutes = provider.CreateParameter("FocExtensionChutes", SO.FocExtensionChutes, DbType.String);
                DbParameter FocOthers = provider.CreateParameter("FocOthers", SO.FocOthers, DbType.String);
                DbParameter SourceOfEnquiry = provider.CreateParameter("SourceOfEnquiry", SO.SourceOfEnquiry.SourceOfEnquiryID, DbType.String);
                DbParameter ReasonForOrderConversion = provider.CreateParameter("ReasonForOrderConversion", SO.ReasonForOrderConversion, DbType.String);
                DbParameter CustomerType = provider.CreateParameter("CustomerType", SO.CustomerType, DbType.String);
                DbParameter OwnershipPattern = provider.CreateParameter("OwnershipPattern", SO.OwnershipPattern, DbType.String);
                DbParameter Profile = provider.CreateParameter("Profile", SO.Profile, DbType.String);
                DbParameter NameOfTheProject = provider.CreateParameter("NameOfTheProject", SO.NameOfTheProject, DbType.String);
                DbParameter TransportationAndInsurance = provider.CreateParameter("TransportationAndInsurance", SO.TransportationAndInsurance, DbType.String);
                  
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32); 
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    DbParameter[] Params = new DbParameter[47] { PrimaryMCSalesOrderID, DealerID, CustomerID, EquipmentHeaderID, PurchaseOrder, PurchaseOrderDate,
                        ShipToStateID, ShipToDistrictID, ShipToCity, ShipToAddress1, ShipToAddress2, ShipToPostalCode,
                        FinacierCode,MachineQuantity,BasicPrice
                     ,Discount,DoNumber,DoDate,DoAmount,TermsOfPaymentID,MarginMoney,DiscountTypeID,IncotermsID
                     ,FinancierAmount,FreightAmount,BenificiaryOfDO,SubventionAmount,UsageID,TRDate,HorsePower,RetailCustomer,ConsolidationInvoicePrint,Hypothecation
                     ,BackToBackDoEndorsedToAjax,SpecialRequirements,FocServiceKit,FocWheelAssy,FocExtensionChutes,FocOthers,SourceOfEnquiry,ReasonForOrderConversion,CustomerType
                     ,OwnershipPattern,Profile,NameOfTheProject,TransportationAndInsurance,UserIDP};

                    provider.Insert("ZDMS_InsertOrUpdateDealerEmployee", Params);
                    scope.Complete(); 
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Dealer", "InsertOrUpdateDealerEmployee", ex);
                return 0;
            }
            return EmployeeID;
        }
    }
}
