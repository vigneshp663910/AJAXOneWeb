using DataAccess;
using Newtonsoft.Json;
using Properties;
using QRCoder;
using SapIntegration;
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


        public void IntegrationEInvoive()
        {
            //List<PDealer> Dealers = new BDealer().GetDealerList(null, null, null);
            //foreach (PDealer Dealer in Dealers)
            //{
            //    new FileManager().DownloadAllFilesToBeImported(Dealer.EInvoiveFTPPath, Dealer.EInvoiveFTPUserID, Dealer.EInvoiveFTPPassword, PDMS_EInvoice.EInvoivePathImport, "DMSS_INV_*");
            //}
            List<PDMS_EInvoice> EInvoices = GetEInvoiceBillingDocumentforFtpProcess();

            foreach (PDMS_EInvoice EInvoice in EInvoices)
            {
               // new FileManager().DownloadAllFilesToBeImported(ConfigurationManager.AppSettings["EInvoiveFTPPathAE"], ConfigurationManager.AppSettings["EInvoiveFTPUserIDAE"], ConfigurationManager.AppSettings["EInvoiveFTPPasswordAE"], PDMS_EInvoice.EInvoivePathImport, "DMSS_INV_*");
                DownloadAllFilesToBeImported(EInvoice.EInvoiceFTPPath, EInvoice.EInvoiceFTPUserID, EInvoice.EInvoiceFTPPassword, PDMS_EInvoice.EInvoivePathImport, EInvoice.BillingDocument);
                string[] InvFiles = Directory.GetFiles(PDMS_EInvoice.EInvoivePathImport, "*" + EInvoice.BillingDocument + "*");
                TraceLogger.Log(DateTime.Now);
                PDMS_ICTicketJSON Customers = new PDMS_ICTicketJSON();
                try
                {
                    foreach (string file in InvFiles)
                    {
                        string[] fileT = File.ReadAllText(file).Split(';');
                        string[] Inv = fileT[1].Split('!');
                        try
                        {
                            DateTime? IRNDate = string.IsNullOrEmpty(Inv[44]) ? (DateTime?)null : Convert.ToDateTime(Inv[44].Trim());

                            DbParameter InvoiceNumber = provider.CreateParameter("InvoiceNumber", Inv[4], DbType.String);
                            DbParameter IRN = provider.CreateParameter("IRN", string.IsNullOrEmpty(Inv[52]) ? null : Inv[52], DbType.String);
                            DbParameter IRNDateP = provider.CreateParameter("IRNDate", IRNDate, DbType.DateTime);
                            DbParameter SignedQRCode = provider.CreateParameter("SignedQRCode", string.IsNullOrEmpty(Inv[51]) ? null : Inv[51], DbType.String);
                            DbParameter SignedInvoice = provider.CreateParameter("SignedInvoice", string.IsNullOrEmpty(Inv[50]) ? null : Inv[50], DbType.String);
                            DbParameter Comments = provider.CreateParameter("Comments", string.IsNullOrEmpty(Inv[53]) ? null : Inv[53], DbType.String);
                            DbParameter InvName = provider.CreateParameter("InvName", EInvoice.FileSubName, DbType.String);
                            DbParameter[] Params = new DbParameter[7] { InvoiceNumber, IRN, IRNDateP, SignedQRCode, SignedInvoice, Comments, InvName };

                            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                            {
                                provider.Insert("ZDMS_InsertOrUpdateEInvoice", Params); 
                                scope.Complete();
                            }
                            File.Move(file, file.Replace("Import", "Import/Success"));
                        }
                        catch (Exception e1)
                        {
                            File.Move(file, file.Replace("Import", " Import/Fail"));
                            new FileLogger().LogMessageService("BDMS_EInvoice", "IntegrationEInvoive", e1); 
                        }
                    }
                    TraceLogger.Log(DateTime.Now);
                }
                catch (Exception ex)
                {                    
                    new FileLogger().LogMessageService("BDMS_EInvoice", "IntegrationEInvoive", ex); 
                }
            }
        }

      
        public List<PDMS_EInvoice> GetInvoiceForRequestEInvoice(string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID, string CustomerCode)
        {
            List<PDMS_EInvoice> EInvoiceS = new List<PDMS_EInvoice>();
            PDMS_EInvoice EInvoice = new PDMS_EInvoice();
            int TOTALLINEITEMS = 0;
            List<PDMS_PaidServiceInvoice> PaidServiceInvoice = GetPaidServiceInvoiceForRequestEInvoice(InvoiceNumber, InvoiceDateF, InvoiceDateT, DealerID, CustomerCode);
            int i = 0;
            foreach (PDMS_PaidServiceInvoice Pinv in PaidServiceInvoice)
            {
                i = i + 1;
                EInvoice = new PDMS_EInvoice()
                 {
                     EInvoiceID = i,
                     Tax_Scheme = "GST",
                     DocumentCategory = "B2B",
                     DocumentType = "INV",
                     BillingDocument = Pinv.InvoiceNumber,
                     InvoiceDate = Pinv.InvoiceDate,


                     SupplierCode = Pinv.ICTicket.Dealer.DealerCode,
                     SupplierGSTIN = Pinv.InvoiceDetails.SupplierGSTIN.Trim(),
                     SupplierTrade_Name = Pinv.ICTicket.Dealer.DealerName,
                     Supplier_addr1 = Pinv.InvoiceDetails.Supplier_addr1.Trim(),
                     SupplierLocation = Pinv.InvoiceDetails.SupplierLocation.Trim(),
                     SupplierPincode = Pinv.InvoiceDetails.SupplierPincode.Trim(),
                     SupplierStateCode = Pinv.InvoiceDetails.SupplierStateCode.Trim(),

                     // BuyerCode = Pinv.ICTicket.Customer.CustomerCode,
                     BuyerGSTIN = Pinv.InvoiceDetails.BuyerGSTIN.Trim(),
                     BuyerName = Pinv.InvoiceDetails.BuyerName.Trim(),
                     BuyerStateCode = Pinv.InvoiceDetails.BuyerStateCode.Trim(),
                     Buyer_addr1 = Pinv.InvoiceDetails.Buyer_addr1.Trim(),
                     Buyer_loc = Pinv.InvoiceDetails.Buyer_loc.Trim(),
                     BuyerPincode = Pinv.InvoiceDetails.BuyerPincode.Trim(),

                     EInvoiceFTPPath = Pinv.ICTicket.Dealer.EInvoiceFTPPath,
                     EInvoiceFTPUserID = Pinv.ICTicket.Dealer.EInvoiceFTPUserID,
                     EInvoiceFTPPassword = Pinv.ICTicket.Dealer.EInvoiceFTPPassword,

                     //public string IRN { get; set; }
                     //public string ReasonForCancellation { get; set; }
                     //public string CancellationComment { get; set; }
                     Type = "U",
                     FileSubName = "PAY",
                     EInvoiceItems = new List<PDMS_EInvoiceItem>()
                 };
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
                    AccumulatedCesVal = AccumulatedCesVal + Pinvi.CessValue;
                    //     AccumulatedOtherCharges = AccumulatedOtherCharges + Pinvi.t;
                    AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;
                    EInvoice.EInvoiceItems.Add(new PDMS_EInvoiceItem()
                    {

                        SlNo = Convert.ToString(TOTALLINEITEMS),
                        InvoiceItemID = Pinvi.PaidServiceInvoiceItemID,
                        BillingDocument = Pinv.InvoiceNumber,

                        PrdDesc = Pinvi.Material.MaterialDescription,
                        IsServc = "Y",
                        HSNCode = Pinvi.Material.HSN,
                        Quantity =(decimal) Pinvi.Qty ,
                        UnitOfMeasure = "NOS",//Pinvi.Material.BaseUnit,
                        UnitPrice = (decimal)Pinvi.Rate,
                        TotalAmount = Pinvi.TaxableValue,
                        AssesseebleAmount = Pinvi.TaxableValue,
                        TaxRate = Convert.ToString(Pinvi.CGST + Pinvi.IGST + Pinvi.SGST),
                        SGSTAmount =  Pinvi.SGSTValue,
                        IGSTAmount =  Pinvi.IGSTValue ,
                        CGSTAmount =  Pinvi.CGSTValue ,
                        // CESSRate = "",
                        CESSAmount = Pinvi.CessValue ,
                        TotalItemValue =  Pinvi.TaxableValue + Pinvi.SGSTValue + Pinvi.IGSTValue + Pinvi.CGSTValue + Pinvi.CessValue

                    });
                }

                EInvoice.TOTALLINEITEMS = Convert.ToString(TOTALLINEITEMS);
                EInvoice.AccumulatedTotalAmount = Convert.ToString(AccumulatedTotalAmount);
                EInvoice.AccumulatedAssTotalAmount = Convert.ToString(AccumulatedAssTotalAmount);
                EInvoice.AccumulatedSgstVal = Convert.ToString(AccumulatedSgstVal);
                EInvoice.AccumulatedIgstVal = Convert.ToString(AccumulatedIgstVal);
                EInvoice.AccumulatedCgstVal = Convert.ToString(AccumulatedCgstVal);
                EInvoice.AccumulatedCesVal = Convert.ToString(AccumulatedCesVal);
              //  EInvoice.AccumulatedOtherCharges = Convert.ToString(AccumulatedOtherCharges);
                EInvoice.AccumulatedTotItemVal = Convert.ToString(AccumulatedTotItemVal);

                EInvoiceS.Add(EInvoice);
            }

            List<PDMS_WarrantyClaimInvoice> WarrantyClaimInvoice = getWarrantyClaimInvoiceForRequestEInvoice(InvoiceNumber, InvoiceDateF, InvoiceDateT, DealerID, CustomerCode);
            foreach (PDMS_WarrantyClaimInvoice Pinv in WarrantyClaimInvoice)
            {
                i = i + 1;
                EInvoice = new PDMS_EInvoice()
                {
                    EInvoiceID = i,
                    Tax_Scheme = "GST",
                    DocumentCategory = "B2B",
                    DocumentType = "INV",
                    BillingDocument = Pinv.InvoiceNumber,
                    // InvoiceDate = Pinv.InvoiceDate.ToShortDateString(),
                    InvoiceDate = Pinv.InvoiceDate,

                    SupplierCode = Pinv.Dealer.DealerCode,
                    SupplierGSTIN = Pinv.InvoiceDetails.SupplierGSTIN,
                    SupplierTrade_Name = Pinv.Dealer.DealerName,
                    Supplier_addr1 = Pinv.InvoiceDetails.Supplier_addr1,
                    SupplierLocation = Pinv.InvoiceDetails.SupplierLocation,
                    SupplierPincode = Pinv.InvoiceDetails.SupplierPincode,
                    SupplierStateCode = Pinv.InvoiceDetails.SupplierStateCode,

                    //BuyerCode = Pinv.InvoiceDetails.BuyerCode,

                    BuyerGSTIN = Pinv.InvoiceDetails.BuyerGSTIN,
                    BuyerName = Pinv.InvoiceDetails.BuyerName,
                    BuyerStateCode = Pinv.InvoiceDetails.BuyerStateCode,
                    Buyer_addr1 = Pinv.InvoiceDetails.Buyer_addr1,
                    Buyer_loc = Pinv.InvoiceDetails.Buyer_loc,
                    BuyerPincode = Pinv.InvoiceDetails.BuyerPincode,

                    EInvoiceFTPPath = Pinv.Dealer.EInvoiceFTPPath,
                    EInvoiceFTPUserID = Pinv.Dealer.EInvoiceFTPUserID,
                    EInvoiceFTPPassword = Pinv.Dealer.EInvoiceFTPPassword,

                    Type = "U",
                    FileSubName = "WARR",

                    EInvoiceItems = new List<PDMS_EInvoiceItem>()

                };
                decimal AccumulatedTotalAmount = 0, AccumulatedAssTotalAmount = 0, AccumulatedSgstVal = 0, AccumulatedIgstVal = 0, AccumulatedCgstVal = 0,
                    AccumulatedCesVal = 0, AccumulatedTotItemVal = 0;
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

                    AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;

                    decimal OtherCharges = Pinvi.HSNCode == "998719" ? 0 : Convert.ToDecimal(Pinv.TCSValue) / Convert.ToDecimal(Pinv.TempTcsMatCount);
                    EInvoice.EInvoiceItems.Add(new PDMS_EInvoiceItem()
                    {

                        SlNo = Convert.ToString(TOTALLINEITEMS),
                        InvoiceItemID = Pinvi.WarrantyClaimInvoiceItemID,
                        BillingDocument = Pinv.InvoiceNumber,

                        PrdDesc = Pinvi.MaterialDesc,
                        IsServc = Pinvi.HSNCode == "998719" ? "Y" : "N",
                        HSNCode = Pinvi.HSNCode,
                        Quantity =(decimal) Pinvi.Qty ,
                        UnitOfMeasure = Pinvi.UOM,
                        UnitPrice = (decimal)Pinvi.Rate,
                        TotalAmount =   Pinvi.TaxableValue ,
                        AssesseebleAmount =   Pinvi.TaxableValue ,
                        TaxRate = String.Format("{0:.#####}", Pinvi.CGST + Pinvi.IGST + Pinvi.SGST),
                        SGSTAmount =  Pinvi.SGSTValue,
                        IGSTAmount =   Pinvi.IGSTValue,
                        CGSTAmount =   Pinvi.CGSTValue,
                        // CESSRate = "",
                        // CESSAmount = ""
                        CESSAmount = 0,
                        //   OtherCharges = Pinvi.HSNCode == "998719" ? "0" : String.Format("{0:.#####}", Convert.ToDecimal(Pinv.TCSValue) / Convert.ToDecimal(Pinv.TempTcsMatCount)),
                        OtherCharges =   OtherCharges,
                        TotalItemValue =   Pinvi.TaxableValue + Pinvi.SGSTValue + Pinvi.IGSTValue + Pinvi.CGSTValue + OtherCharges
                    });



                    EInvoice.TOTALLINEITEMS = String.Format("{0:.#####}", TOTALLINEITEMS);
                    EInvoice.AccumulatedTotalAmount = String.Format("{0:.#####}", AccumulatedTotalAmount);
                    EInvoice.AccumulatedAssTotalAmount = String.Format("{0:.#####}", AccumulatedAssTotalAmount);
                    EInvoice.AccumulatedSgstVal = String.Format("{0:.#####}", AccumulatedSgstVal);
                    EInvoice.AccumulatedIgstVal = String.Format("{0:.#####}", AccumulatedIgstVal);
                    EInvoice.AccumulatedCgstVal = String.Format("{0:.#####}", AccumulatedCgstVal);
                    EInvoice.AccumulatedCesVal = String.Format("{0:.#####}", AccumulatedCesVal);
                    EInvoice.AccumulatedOtherCharges = String.Format("{0:.#####}", Pinv.TCSValue);
                    EInvoice.AccumulatedTotItemVal = String.Format("{0:.#####}", AccumulatedTotItemVal + Pinv.TCSValue);
                }
                EInvoiceS.Add(EInvoice);
            }


            List<PDMS_WarrantyClaimInvoice> ActivityInvoice = getActivityInvoiceForRequestEInvoice(InvoiceNumber, InvoiceDateF, InvoiceDateT, DealerID);
            foreach (PDMS_WarrantyClaimInvoice Pinv in ActivityInvoice)
            {
                i = i + 1;
                EInvoice = new PDMS_EInvoice()
                {
                    EInvoiceID = i,
                    Tax_Scheme = "GST",
                    DocumentCategory = "B2B",
                    DocumentType = "INV",
                    BillingDocument = Pinv.InvoiceNumber,
                    // InvoiceDate = Pinv.InvoiceDate.ToShortDateString(),
                    InvoiceDate = Pinv.InvoiceDate,

                    SupplierCode = Pinv.Dealer.DealerCode,
                    SupplierGSTIN = Pinv.InvoiceDetails.SupplierGSTIN,
                    SupplierTrade_Name = Pinv.Dealer.DealerName,
                    Supplier_addr1 = Pinv.InvoiceDetails.Supplier_addr1,
                    SupplierLocation = Pinv.InvoiceDetails.SupplierLocation,
                    SupplierPincode = Pinv.InvoiceDetails.SupplierPincode,
                    SupplierStateCode = Pinv.InvoiceDetails.SupplierStateCode,

                    //BuyerCode = Pinv.InvoiceDetails.BuyerCode,

                    BuyerGSTIN = Pinv.InvoiceDetails.BuyerGSTIN,
                    BuyerName = Pinv.InvoiceDetails.BuyerName,
                    BuyerStateCode = Pinv.InvoiceDetails.BuyerStateCode,
                    Buyer_addr1 = Pinv.InvoiceDetails.Buyer_addr1,
                    Buyer_loc = Pinv.InvoiceDetails.Buyer_loc,
                    BuyerPincode = Pinv.InvoiceDetails.BuyerPincode,

                    EInvoiceFTPPath = Pinv.Dealer.EInvoiceFTPPath,
                    EInvoiceFTPUserID = Pinv.Dealer.EInvoiceFTPUserID,
                    EInvoiceFTPPassword = Pinv.Dealer.EInvoiceFTPPassword,

                    Type = "U",
                    FileSubName = "ATY",

                    EInvoiceItems = new List<PDMS_EInvoiceItem>()

                };
                decimal AccumulatedTotalAmount = 0, AccumulatedAssTotalAmount = 0, AccumulatedSgstVal = 0, AccumulatedIgstVal = 0, AccumulatedCgstVal = 0,
                    AccumulatedCesVal = 0, AccumulatedTotItemVal = 0;
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

                    AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;

                    decimal OtherCharges = Pinvi.HSNCode == "998719" ? 0 : Convert.ToDecimal(Pinv.TCSValue) / Convert.ToDecimal(Pinv.TempTcsMatCount);
                    EInvoice.EInvoiceItems.Add(new PDMS_EInvoiceItem()
                    {

                        SlNo = Convert.ToString(TOTALLINEITEMS),
                        InvoiceItemID = Pinvi.WarrantyClaimInvoiceItemID,
                        BillingDocument = Pinv.InvoiceNumber,

                        PrdDesc = Pinvi.MaterialDesc,
                        IsServc =   "Y",
                        HSNCode = Pinvi.HSNCode,
                        Quantity =  Pinvi.Qty ,
                        UnitOfMeasure = Pinvi.UOM,
                        UnitPrice =   Pinvi.Rate,
                        TotalAmount =   Pinvi.TaxableValue ,
                        AssesseebleAmount =   Pinvi.TaxableValue ,
                        TaxRate = String.Format("{0:.#####}", Pinvi.CGST + Pinvi.IGST + Pinvi.SGST),
                        SGSTAmount =   Pinvi.SGSTValue ,
                        IGSTAmount = Pinvi.IGSTValue ,
                        CGSTAmount =  Pinvi.CGSTValue ,
                        // CESSRate = "",
                        // CESSAmount = ""
                        CESSAmount = 0,
                        //   OtherCharges = Pinvi.HSNCode == "998719" ? "0" : String.Format("{0:.#####}", Convert.ToDecimal(Pinv.TCSValue) / Convert.ToDecimal(Pinv.TempTcsMatCount)),
                        OtherCharges =   OtherCharges,
                        TotalItemValue =  Pinvi.TaxableValue + Pinvi.SGSTValue + Pinvi.IGSTValue + Pinvi.CGSTValue + OtherCharges
                    });



                    EInvoice.TOTALLINEITEMS = String.Format("{0:.#####}", TOTALLINEITEMS);
                    EInvoice.AccumulatedTotalAmount = String.Format("{0:.#####}", AccumulatedTotalAmount);
                    EInvoice.AccumulatedAssTotalAmount = String.Format("{0:.#####}", AccumulatedAssTotalAmount);
                    EInvoice.AccumulatedSgstVal = String.Format("{0:.#####}", AccumulatedSgstVal);
                    EInvoice.AccumulatedIgstVal = String.Format("{0:.#####}", AccumulatedIgstVal);
                    EInvoice.AccumulatedCgstVal = String.Format("{0:.#####}", AccumulatedCgstVal);
                    EInvoice.AccumulatedCesVal = String.Format("{0:.#####}", AccumulatedCesVal);
                    EInvoice.AccumulatedOtherCharges = String.Format("{0:.#####}", Pinv.TCSValue);
                    EInvoice.AccumulatedTotItemVal = String.Format("{0:.#####}", AccumulatedTotItemVal + Pinv.TCSValue);
                }
                EInvoiceS.Add(EInvoice);
            }
            return EInvoiceS;
        }

        private List<PDMS_PaidServiceInvoice> GetPaidServiceInvoiceForRequestEInvoice(string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID, string CustomerCode)
        {
            List<PDMS_PaidServiceInvoice> Services = new List<PDMS_PaidServiceInvoice>();
            try
            {
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);

                DbParameter[] Params = new DbParameter[5] { InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerIDP, CustomerCodeP };
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
                                Service.ICTicket.Dealer = new PDMS_Dealer();
                                Service.ICTicket.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                                Service.ICTicket.Dealer.DealerName = Convert.ToString(dr["ContactName"]);

                                Service.ICTicket.Dealer.EInvoiceFTPPath = Convert.ToString(dr["EInvoiceFTPPath"]);
                                Service.ICTicket.Dealer.EInvoiceFTPUserID = Convert.ToString(dr["EInvoiceFTPUserID"]);
                                Service.ICTicket.Dealer.EInvoiceFTPPassword = Convert.ToString(dr["EInvoiceFTPPassword"]);


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
        private List<PDMS_WarrantyClaimInvoice> getWarrantyClaimInvoiceForRequestEInvoice(string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID, string CustomerCode)
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
                                    EInvoiceDate= DBNull.Value == dr["EInvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["EInvoiceDate"]) ,
                                    EInvoiceFTPPath = Convert.ToString(dr["EInvoiceFTPPath"]),
                                    EInvoiceFTPUserID = Convert.ToString(dr["EInvoiceFTPUserID"]),
                                    EInvoiceFTPPassword = Convert.ToString(dr["EInvoiceFTPPassword"])
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
        private List<PDMS_WarrantyClaimInvoice> getActivityInvoiceForRequestEInvoice(string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID)
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
                                    EInvoiceFTPPath = Convert.ToString(dr["EInvoiceFTPPath"]),
                                    EInvoiceFTPUserID = Convert.ToString(dr["EInvoiceFTPUserID"]),
                                    EInvoiceFTPPassword = Convert.ToString(dr["EInvoiceFTPPassword"])
                                };
                                W.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                W.InvoiceItems = new List<PDMS_WarrantyClaimInvoiceItem>();
                                InvoiceID = W.WarrantyClaimInvoiceID;
                               // W.InvoiceType = new PDMS_WarrantyInvoiceType() { InvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"]), InvoiceType = Convert.ToString(dr["InvoiceType"]) };

                                W.InvoiceDetails = new PDMS_WarrantyClaimInvoiceDetails();

                                PDMS_Customer Dealer = new SCustomer().getCustomerAddress(W.Dealer.DealerCode);
                              
                               // string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                               // string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.StateN.State) ? "" : "," + Dealer.StateN.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');


                                W.InvoiceDetails.SupplierGSTIN =Dealer.GSTIN;
                                W.InvoiceDetails.Supplier_addr1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2)).Trim(',');
                                W.InvoiceDetails.SupplierLocation = Dealer.City;
                                W.InvoiceDetails.SupplierPincode = Dealer.Pincode;
                                W.InvoiceDetails.SupplierStateCode = Dealer.State.StateCode;

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

        public List<PDMS_EInvoice> GetDebitNoteForRequestEInvoice(string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID, string CustomerCode)
        {
            List<PDMS_EInvoice> EInvoiceS = new List<PDMS_EInvoice>();
            PDMS_EInvoice EInvoice = new PDMS_EInvoice();
            int TOTALLINEITEMS = 0;
            List<PDMS_WarrantyClaimDebitNote> Invoice = getWarrantyClaimDebitNoteForRequestEInvoice(InvoiceNumber, InvoiceDateF, InvoiceDateT, DealerID, CustomerCode);
            int i = 0;
            foreach (PDMS_WarrantyClaimDebitNote Pinv in Invoice)
            {
                i = i + 1;
                EInvoice = new PDMS_EInvoice()
                {
                    EInvoiceID = i,
                     Tax_Scheme = "GST",
                    DocumentCategory = "B2B",
                    DocumentType = "DBN",
                    BillingDocument = Pinv.DebitNoteNumber,
                    InvoiceDate = Pinv.DebitNoteDate,

                  // SupplierCode = Pinv.Dealer.DealerCode,
                    SupplierTrade_Name = "Ajax Engineering Private Limited",
                    SupplierGSTIN = "29AABCA2035K1ZT",                   
                    Supplier_addr1 = Pinv.DebitNoteDetails.Supplier_addr1.Trim(),
                    SupplierLocation = Pinv.DebitNoteDetails.SupplierLocation.Trim(),
                    SupplierPincode = Pinv.DebitNoteDetails.SupplierPincode.Trim(),
                    SupplierStateCode = "29",


                    BuyerGSTIN = Pinv.DebitNoteDetails.BuyerGSTIN.Trim(),
                    BuyerName = Pinv.DebitNoteDetails.BuyerName.Trim(),
                    BuyerStateCode = Pinv.DebitNoteDetails.BuyerStateCode.Trim(),
                    Buyer_addr1 = Pinv.DebitNoteDetails.Buyer_addr1.Trim(),
                    Buyer_loc = Pinv.DebitNoteDetails.Buyer_loc.Trim(),
                    BuyerPincode = Pinv.DebitNoteDetails.BuyerPincode.Trim(),

                    EInvoiceFTPPath = Convert.ToString(ConfigurationManager.AppSettings["EInvoiveFTPPathAE"]),
                    EInvoiceFTPUserID = Convert.ToString(ConfigurationManager.AppSettings["EInvoiveFTPUserIDAE"]),
                    EInvoiceFTPPassword = Convert.ToString(ConfigurationManager.AppSettings["EInvoiveFTPPasswordAE"]),

                    Type = "U",
                    FileSubName = "DEBIT",
                    EInvoiceItems = new List<PDMS_EInvoiceItem>()

                };
                decimal AccumulatedTotalAmount = 0, AccumulatedAssTotalAmount = 0, AccumulatedSgstVal = 0, AccumulatedIgstVal = 0, AccumulatedCgstVal = 0,
                    AccumulatedCesVal = 0, AccumulatedOtherCharges = 0, AccumulatedTotItemVal = 0;
                TOTALLINEITEMS = 0;
                foreach (PDMS_WarrantyClaimDebitNoteItem Pinvi in Pinv.DebitNoteItems)
                {
                    TOTALLINEITEMS = TOTALLINEITEMS + 1;
                    AccumulatedTotalAmount = AccumulatedTotalAmount + Pinvi.TaxableValue;
                    AccumulatedAssTotalAmount = AccumulatedAssTotalAmount + Pinvi.TaxableValue;
                    AccumulatedSgstVal = AccumulatedSgstVal + Pinvi.SGSTValue;
                    AccumulatedIgstVal = AccumulatedIgstVal + Pinvi.IGSTValue;
                    AccumulatedCgstVal = AccumulatedCgstVal + Pinvi.CGSTValue;
                    AccumulatedCesVal = AccumulatedCesVal + 0;
                    AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;

                    decimal OtherCharges = Pinvi.HSNCode == "998719" ? 0 : Convert.ToDecimal(Pinv.TCSValue) / Convert.ToDecimal(Pinv.TempTcsMatCount);


                    EInvoice.EInvoiceItems.Add(new PDMS_EInvoiceItem()
                    {

                        SlNo = Convert.ToString(TOTALLINEITEMS),
                        PrdDesc = Pinvi.MaterialDesc,
                        IsServc = Pinvi.HSNCode == "998719" ? "Y" : "N",
                        HSNCode = Pinvi.HSNCode,
                        Quantity = Pinvi.Qty ,
                        
                        UnitOfMeasure = Pinvi.UOM,   
                        UnitPrice =   Pinvi.Rate ,
                        TotalAmount =   Pinvi.TaxableValue ,
                        AssesseebleAmount =   Pinvi.TaxableValue ,
                        TaxRate = String.Format("{0:.#####}", Pinvi.CGST + Pinvi.IGST + Pinvi.SGST),
                        SGSTAmount =   Pinvi.SGSTValue ,
                        IGSTAmount =   Pinvi.IGSTValue ,
                        CGSTAmount =   Pinvi.CGSTValue ,
                        // CESSRate = "",
                        CESSAmount = 0,
                        OtherCharges =   OtherCharges ,
                        TotalItemValue =   Pinvi.TaxableValue + Pinvi.SGSTValue + Pinvi.IGSTValue + Pinvi.CGSTValue + OtherCharges

                       

                    });
                }
                EInvoice.TOTALLINEITEMS = String.Format("{0:.#####}", TOTALLINEITEMS);
                EInvoice.AccumulatedTotalAmount = String.Format("{0:.#####}", AccumulatedTotalAmount);
                EInvoice.AccumulatedAssTotalAmount = String.Format("{0:.#####}", AccumulatedAssTotalAmount);
                EInvoice.AccumulatedSgstVal = String.Format("{0:.#####}", AccumulatedSgstVal);
                EInvoice.AccumulatedIgstVal = String.Format("{0:.#####}", AccumulatedIgstVal);
                EInvoice.AccumulatedCgstVal = String.Format("{0:.#####}", AccumulatedCgstVal);
                EInvoice.AccumulatedCesVal = String.Format("{0:.#####}", AccumulatedCesVal);
                EInvoice.AccumulatedOtherCharges = String.Format("{0:.#####}", Pinv.TCSValue);
                EInvoice.AccumulatedTotItemVal = String.Format("{0:.#####}", AccumulatedTotItemVal + Pinv.TCSValue);
                EInvoice.AccumulatedTotItemVal = Convert.ToString(AccumulatedTotItemVal);

                EInvoiceS.Add(EInvoice);
            }
            return EInvoiceS;
        }
        public List<PDMS_WarrantyClaimDebitNote> getWarrantyClaimDebitNoteForRequestEInvoice(string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID, string CustomerCode)
        {
            List<PDMS_WarrantyClaimDebitNote> Ws = new List<PDMS_WarrantyClaimDebitNote>();
            PDMS_WarrantyClaimDebitNote W = null;
            DbParameter InvoiceNumberP = provider.CreateParameter("DebitNoteNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
            DbParameter InvoiceDateFP = provider.CreateParameter("DebitNoteDateF", InvoiceDateF, DbType.DateTime);
            DbParameter InvoiceDateTP = provider.CreateParameter("DebitNoteDateT", InvoiceDateT, DbType.DateTime);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            //    DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);

            DbParameter[] Params = new DbParameter[4] { InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerIDP };
            try
            {
                long InvoiceID = 0;
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_getWarrantyClaimDebitNoteForRequestEInvoice", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["WarrantyClaimDebitNoteID"]))
                            {
                                W = new PDMS_WarrantyClaimDebitNote();
                                Ws.Add(W);
                                W.WarrantyClaimDebitNoteID = Convert.ToInt64(dr["WarrantyClaimDebitNoteID"]);
                                W.DebitNoteNumber = Convert.ToString(dr["DebitNoteNumber"]);
                                W.DebitNoteDate = Convert.ToDateTime(dr["DebitNoteDate"]);
                                W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["ContactName"]) };
                                W.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                W.PeriodFrom = DBNull.Value == dr["PeriodFrom"] ? (DateTime?)null : Convert.ToDateTime(dr["PeriodFrom"]);
                                W.PeriodTo = DBNull.Value == dr["PeriodTo"] ? (DateTime?)null : Convert.ToDateTime(dr["PeriodTo"]);
                                W.DebitNoteItems = new List<PDMS_WarrantyClaimDebitNoteItem>();
                                InvoiceID = W.WarrantyClaimDebitNoteID;
                                W.TCSValue = DBNull.Value == dr["TCSValue"] ? 0 : Convert.ToDecimal(dr["TCSValue"]);
                                W.TCSTax = DBNull.Value == dr["TCSTax"] ? 0 : Convert.ToDecimal(dr["TCSTax"]);
                                W.DebitNoteDetails = new PDMS_WarrantyClaimDebitNoteDetails();

                                W.DebitNoteDetails.SupplierGSTIN = Convert.ToString(dr["SupplierGSTIN"]);
                                W.DebitNoteDetails.Supplier_addr1 = Convert.ToString(dr["Supplier_addr1"]);
                                W.DebitNoteDetails.SupplierLocation = Convert.ToString(dr["SupplierLocation"]);
                                W.DebitNoteDetails.SupplierPincode = Convert.ToString(dr["SupplierPincode"]);
                                W.DebitNoteDetails.SupplierStateCode = Convert.ToString(dr["SupplierStateCode"]);

                                W.DebitNoteDetails.BuyerGSTIN = Convert.ToString(dr["BuyerGSTIN"]);
                                W.DebitNoteDetails.BuyerName = Convert.ToString(dr["CustomerName"]);
                                W.DebitNoteDetails.BuyerStateCode = Convert.ToString(dr["BuyerStateCode"]);
                                W.DebitNoteDetails.Buyer_addr1 = Convert.ToString(dr["Buyer_addr1"]);
                                W.DebitNoteDetails.Buyer_loc = Convert.ToString(dr["Buyer_loc"]);
                                W.DebitNoteDetails.BuyerPincode = Convert.ToString(dr["BuyerPincode"]);

                            }
                            W.DebitNoteItems.Add(new PDMS_WarrantyClaimDebitNoteItem()
                            {
                                WarrantyClaimDebitNoteItemID = Convert.ToInt64(dr["WarrantyClaimDebitNoteItemID"]),
                                WarrantyClaimAnnexureItemID = Convert.ToInt64(dr["WarrantyClaimAnnexureItemID"]),
                                Material = Convert.ToString(dr["Material"]),
                                MaterialDesc = Convert.ToString(dr["MaterialDesc"]),
                                HSNCode = Convert.ToString(dr["HSNCode"]),
                                UOM = Convert.ToString(dr["UnitCode"]),
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Rate = Convert.ToDecimal(dr["Rate"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToInt32(dr["CGST"]),
                                SGST = Convert.ToInt32(dr["SGST"]),
                                IGST = Convert.ToInt32(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                            });
                            W.TempTcsMatCount = W.TempTcsMatCount + Convert.ToString(dr["HSNCode"]) == "998719" ? 0 : 1;
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

        public List<PDMS_EInvoice> GetEInvoiceBillingDocumentforFtpProcess()
        {
            List<PDMS_EInvoice> InvoiceE = new List<PDMS_EInvoice>();
            try
            {
                using (DataSet DataSet = provider.Select("ZDMS_GetEInvoiceBillingDocumentforFtpProcess"))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            InvoiceE.Add(new PDMS_EInvoice()
                            {
                                BillingDocument = Convert.ToString(dr["BillingDocument"]),
                                EInvoiceFTPPath = Convert.ToString(dr["EInvoiceFTPPath"]),
                                EInvoiceFTPUserID = Convert.ToString(dr["EInvoiceFTPUserID"]),
                                EInvoiceFTPPassword = Convert.ToString(dr["EInvoiceFTPPassword"]),
                                FileSubName = Convert.ToString(dr["FileSubName"])
                            });
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
        
        /// FTP
        /// 
        public void DownloadAllFilesToBeImported(string FTPPath, string FTPUserName, string FTPPassword, string DestinationPath, string FTPFileName)
        {
            FTPManager ftp = new FTPManager();
            List<string> fileNames = new List<string>();
            FTPDetails ftpDetails = GetFTPDetails(FTPPath, FTPUserName, FTPPassword);
            //try
            //{
            fileNames = GetFilesOnFilter(ftpDetails, FTPFileName);
            ftpDetails.DestinationPath = DestinationPath;
            if (fileNames == null)
                fileNames = new List<string>();
            //foreach (string file in fileNames)
            //{
                try
                {
                    ftpDetails.FTPFileName = fileNames[fileNames.Count()-1];
                    ftpDetails.FTPFileName = ftpDetails.FTPFileName.Substring(39, ftpDetails.FTPFileName.Length - 39);
                    ftp.Download(ftpDetails);
                    //try
                    //{
                    //    ftp.Delete(ftpDetails);
                    //}
                    //catch (Exception ex)
                    //{
                    //    ExceptionLogger.LogError(string.Format("Unable to delete the file {0} from ftp", ftpDetails.FTPFileName), ex);
                    //}
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogError(string.Format("Unable to download the file {0} from ftp", ftpDetails.FTPFileName), ex);
                }
          //  }
            //}
            //catch (VPSException vpsEx)
            //{
            //    throw vpsEx;
            //}
            //catch (Exception ex)
            //{
            //    throw new VPSException(ErrorCode.GENE, ex);
            //}
        }
        public void DownloadAndDeleteFtp(string FTPPath, string FTPUserName, string FTPPassword, string DestinationPath, string FTPFileName)
        {
            FTPManager ftp = new FTPManager();
            List<string> fileNames = new List<string>();
            FTPDetails ftpDetails = GetFTPDetails(FTPPath, FTPUserName, FTPPassword);           
            fileNames = GetFilesOnFilter(ftpDetails, FTPFileName);
            ftpDetails.DestinationPath = DestinationPath; 
            try
            {
                if (fileNames.Count != 0)
                {
                    ftpDetails.FTPFileName = fileNames[fileNames.Count() - 1];
                    ftpDetails.FTPFileName = ftpDetails.FTPFileName.Substring(39, ftpDetails.FTPFileName.Length - 39);
                    ftp.Download(ftpDetails);
                    try
                    {
                        ftp.Delete(ftpDetails);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.LogError(string.Format("Unable to delete the file {0} from ftp", ftpDetails.FTPFileName), ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError(string.Format("Unable to download the file {0} from ftp", ftpDetails.FTPFileName), ex);
            }            
        }
        public static FTPDetails GetFTPDetails(string FTPPath, string FTPUserName, string FTPPassword)
        {
            FTPCredentials credential = new FTPCredentials()
            {
                UserName = FTPUserName,
                Password = FTPPassword
            };
            return new FTPDetails()
            {
                Credentials = credential,
                FTPPath = FTPPath
            };
        }

        public List<string> GetFilesOnFilter(FTPDetails ftpDetails, string ftpFilter)
        {
            try
            {
                string[] ftpFileList = GetAllFiles(ftpDetails);
                if (ftpFileList != null)
                {
                    var filteredFileList = from list in ftpFileList where list.Contains(ftpFilter) orderby list select list;
                    return filteredFileList.ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                //1   throw new VPSException(ErrorCode.FTPERROR, ex);
            }
            return null;
        }
        public string[] GetAllFiles(FTPDetails ftpDetails)
        {
            string[] downloadFiles = null;
            StringBuilder result = new StringBuilder();
            FtpWebRequest ftpRequest = null;
            StreamReader reader = null;
            WebResponse response = null;
            try
            {
                string uri = ftpDetails.FTPPath;
                ftpRequest = Login(uri, ftpDetails.Credentials);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftpRequest.Proxy = null;
              //  ftpRequest.UsePassive = true;
                response = ftpRequest.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.ASCII);
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }

                if (result.ToString().Trim().Length > 1)
                {
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                    downloadFiles = result.ToString().Split('\n'); 
                }
                return downloadFiles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (response != null)
                    response.Close();

            }
        }

        public FtpWebRequest Login(string uri, FTPCredentials credential)
        {
            try
            {
                FtpWebRequest ftpRequest;
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                ftpRequest.Credentials = new NetworkCredential(credential.UserName, credential.Password);
                ftpRequest.KeepAlive = false; 
                return ftpRequest;
            }
            catch (Exception ex)
            {
                //  throw new VPSException(ErrorCode.FTPERROR, ex);
            }
            return null;
        }

        void hh(string UserName, string Password)
        {
            WebClient r = new WebClient();
            r.Credentials = new NetworkCredential(UserName, Password);
            byte[] fdata = r.DownloadData("");
        }

         
        public string GeneratEInvoice(string InvoiceNumber)
        {
            try
            {
                string Message = "";

                PDMS_EInvoice Inv = new BDMS_EInvoice().GetInvoiceForRequestEInvoice(InvoiceNumber, null, null, null, null)[0];

                Inv.BuyerGSTIN = Inv.BuyerGSTIN.Trim();
                if (string.IsNullOrEmpty(Inv.BuyerGSTIN))
                {
                    return "Please update Buyer GST Number"; 
                }
                String regexS = "^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexS);
                if (Inv.BuyerGSTIN == "URD")
                {
                    return "Customer  GST Number is URD";
                }
                else if (regex.Match(Inv.BuyerGSTIN).Success)
                {
                    if (Inv.BuyerGSTIN.Trim().Substring(0, 2) != Inv.BuyerStateCode.Trim())
                    {
                        return "Please update Buyer State Code"; 
                    }
                }
                else
                {
                    return "Please update correct GST Number"; 
                }
                if (string.IsNullOrEmpty(Inv.Buyer_addr1.Trim()))
                {
                    return "Please update Buyer Address"; 
                }
                if (!new BDMS_EInvoice().ValidatePincode(Inv.BuyerPincode.Substring(0, 2), Inv.BuyerStateCode))
                {
                    return "Please check Buyer Pincode and Statecode"; 
                }

                if (string.IsNullOrEmpty(Inv.SupplierGSTIN) || string.IsNullOrEmpty(Inv.SupplierLocation) || string.IsNullOrEmpty(Inv.SupplierPincode) || string.IsNullOrEmpty(Inv.SupplierStateCode))
                {
                    return "</n> Please check the supplier details of Invoice (" + Inv.BillingDocument + ")"; 
                }
                if (string.IsNullOrEmpty(Inv.BuyerGSTIN) || string.IsNullOrEmpty(Inv.Buyer_loc) || string.IsNullOrEmpty(Inv.BuyerPincode) || string.IsNullOrEmpty(Inv.BuyerStateCode))
                {
                    return "</n> Please check the Buyer details of Invoice (" + Inv.BillingDocument + ")"; 
                }
                string FTPFileName = "DMSS_INV_" + Inv.FileSubName + "_" + Inv.BillingDocument + ".txt";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(PDMS_EInvoice.EInvoivePathExport + FTPFileName))
                {
                    file.WriteLine("Tax_Scheme! Document Category! Document Type! Billing Document! Invoice Date! Supplier GSTIN! Supplier trade_Name! Supplier_addr1! Supplier Location! Supplier Pincode! Supplier State Code! Buyer GSTIN! Buyer Name! Buyer State Code! Buyer_addr1! Buyer_loc! Buyer Pincode! disp_sup_trade_Name! disp_sup_addr1! disp_sup_loc! disp_sup_pin! disp_sup_stcd! TOTAL LINE ITEMS! Sl No! PrdDesc! IsServc! HSN Code! Quantity! Unit of measure! Unit Price! Total Amount(Tax Base)! Assesseeble Amount! Tax Rate! SGST Amount! IGST Amount! CGST Amount! CESS Rate! CESS Amount! Other Charges! Total Item Value! Accumulated Total Amount! Accumulated AssTotal Amount! Accumulated SgstVal! Accumulated IgstVal! Accumulated CgstVal! Accumulated CesVal!  Accumulated  Other Charges! Accumulated Tot Item Val! IRN! Reason for Cancellation! Cancellation comment! Type;");
                    int i = 0;
                    foreach (PDMS_EInvoiceItem Invi in Inv.EInvoiceItems)
                    {
                        if (i == 0)
                        {
                            string line = Inv.Tax_Scheme + "!" + Inv.DocumentCategory + "!" + Inv.DocumentType + "!" + Inv.BillingDocument + "!" + Inv.InvoiceDate + "!" + Inv.SupplierGSTIN + "!" + Inv.SupplierTrade_Name + "!" + Inv.Supplier_addr1 + "!" + Inv.SupplierLocation + "!" + Inv.SupplierPincode + "!" + Inv.SupplierStateCode + "!" + Inv.BuyerGSTIN + "!" + Inv.BuyerName + "!" + Inv.BuyerStateCode + "!" + Inv.Buyer_addr1 + "!" + Inv.Buyer_loc + "!" + Inv.BuyerPincode + "!" + Inv.SupplierTrade_Name + "!" + Inv.Supplier_addr1 + "!" + Inv.SupplierLocation + "!" + Inv.SupplierPincode + "!" + Inv.SupplierStateCode + "!" + Inv.TOTALLINEITEMS + "!!!!!!!!!!!!!!!!!!" + Inv.AccumulatedTotalAmount + "!" + Inv.AccumulatedAssTotalAmount + "!" + Inv.AccumulatedSgstVal + "!" + Inv.AccumulatedIgstVal + "!" + Inv.AccumulatedCgstVal + "!" + Inv.AccumulatedCesVal + "!" + Inv.AccumulatedOtherCharges + "!" + Inv.AccumulatedTotItemVal + "!" + Inv.IRN + "!" + Inv.ReasonForCancellation + "!" + Inv.CancellationComment + "!" + Inv.Type + ";";
                            file.WriteLine(line);
                            line = Inv.Tax_Scheme + "!" + Inv.DocumentCategory + "!" + Inv.DocumentType + "!" + Inv.BillingDocument + "!!!!!!!!!!!!!!!!!!!!" + Invi.SlNo + "!" + Invi.PrdDesc + "!" + Invi.IsServc + "!" + Invi.HSNCode + "!" + Invi.Quantity + "!" + Invi.UnitOfMeasure + "!" + Invi.UnitPrice + "!" + Invi.TotalAmount + "!" + Invi.AssesseebleAmount + "!" + Invi.TaxRate + "!" + Invi.SGSTAmount + "!" + Invi.IGSTAmount + "!" + Invi.CGSTAmount + "!" + Invi.CESSRate + "!" + Invi.CESSAmount + "!" + Invi.OtherCharges + "!" + Invi.TotalItemValue + "!" + "!" + "!" + "!" + "!" + "!" + "!" + "!" + "!" + "!!!" + Inv.Type + ";";
                            file.WriteLine(line);
                        }
                        else
                        {
                            string line = Inv.Tax_Scheme + "!" + Inv.DocumentCategory + "!" + Inv.DocumentType + "!" + Inv.BillingDocument + "!!!!!!!!!!!!!!!!!!!!" + Invi.SlNo + "!" + Invi.PrdDesc + "!" + Invi.IsServc + "!" + Invi.HSNCode + "!" + Invi.Quantity + "!" + Invi.UnitOfMeasure + "!" + Invi.UnitPrice + "!" + Invi.TotalAmount + "!" + Invi.AssesseebleAmount + "!" + Invi.TaxRate + "!" + Invi.SGSTAmount + "!" + Invi.IGSTAmount + "!" + Invi.CGSTAmount + "!" + Invi.CESSRate + "!" + Invi.CESSAmount + "!" + Invi.OtherCharges + "!" + Invi.TotalItemValue + "!" + "!" + "!" + "!" + "!" + "!" + "!" + "!" + "!" + "!!!" + Inv.Type + ";";
                            file.WriteLine(line);
                        }
                        if (i == Inv.EInvoiceItems.Count() - 1)
                        {
                            string line = Inv.Tax_Scheme + "!" + Inv.DocumentCategory + "!" + Inv.DocumentType + "!" + Inv.BillingDocument + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + Inv.AccumulatedTotalAmount + "!" + Inv.AccumulatedAssTotalAmount + "!" + Inv.AccumulatedSgstVal + "!" + Inv.AccumulatedIgstVal + "!" + Inv.AccumulatedCgstVal + "!" + Inv.AccumulatedCesVal + "!" + Inv.AccumulatedOtherCharges + "!" + Inv.AccumulatedTotItemVal + "!" + Inv.IRN + "!" + Inv.ReasonForCancellation + "!" + Inv.CancellationComment + "!" + Inv.Type + ";";
                            file.WriteLine(line);
                        }
                        i = i + 1;
                    }
                    file.Close();
                }
                if (new BDMS_EInvoice().UpdateEInvoiveStatus(Inv.BillingDocument, 1, Inv.FileSubName))
                {
                    if (new FileManager().UploadFile(Inv.EInvoiceFTPPath + "input_files/", Inv.EInvoiceFTPUserID, Inv.EInvoiceFTPPassword, PDMS_EInvoice.EInvoivePathExport + FTPFileName, FTPFileName))
                    {
                        new FileManager().MoveFile(new FileInfo(PDMS_EInvoice.EInvoivePathExport + FTPFileName), PDMS_EInvoice.EInvoivePathExportSuccess);
                    }
                }
            }
            catch(Exception e)
            { }
            return "";
        }
        public void GeneratEDebitNote(string InvoiceNumber)
        {
            try
            {
                string Message = "";

                PDMS_EInvoice Inv = new BDMS_EInvoice().GetDebitNoteForRequestEInvoice(InvoiceNumber, null, null, null, null)[0]; 

                Inv.BuyerGSTIN = Inv.BuyerGSTIN.Trim();
                if (string.IsNullOrEmpty(Inv.BuyerGSTIN))
                {
                    Message = "Please update Buyer GST Number";
                    return;
                }  
                String regexS = "^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexS);
                if (Inv.BuyerGSTIN == "URD")
                {
                    return;
                }
                else if (regex.Match(Inv.BuyerGSTIN).Success)
                {
                    if (Inv.BuyerGSTIN.Trim().Substring(0, 2) != Inv.BuyerStateCode.Trim())
                    {
                        Message = "Please update Buyer State Code";
                        return;
                    }
                }
                else
                {
                    Message = "Please update correct GST Number";
                    return;
                }
                if (string.IsNullOrEmpty(Inv.Buyer_addr1.Trim()))
                {
                    Message = "Please update Buyer Address";
                    return;
                }
                if (!new BDMS_EInvoice().ValidatePincode(Inv.BuyerPincode.Substring(0, 2), Inv.BuyerStateCode))
                {
                    Message = "Please check Buyer Pincode and Statecode";
                    return;
                }

                if (string.IsNullOrEmpty(Inv.SupplierGSTIN) || string.IsNullOrEmpty(Inv.SupplierLocation) || string.IsNullOrEmpty(Inv.SupplierPincode) || string.IsNullOrEmpty(Inv.SupplierStateCode))
                {
                    Message = Message + "</n> Please check the supplier details of Invoice (" + Inv.BillingDocument + ")";
                    // continue;
                }
                if (string.IsNullOrEmpty(Inv.BuyerGSTIN) || string.IsNullOrEmpty(Inv.Buyer_loc) || string.IsNullOrEmpty(Inv.BuyerPincode) || string.IsNullOrEmpty(Inv.BuyerStateCode))
                {
                    Message = Message + "</n> Please check the Buyer details of Invoice (" + Inv.BillingDocument + ")";
                    //  continue;
                }
                string FTPFileName = "DMSS_INV_" + Inv.FileSubName + "_" + Inv.BillingDocument + ".txt";
                 
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(PDMS_EInvoice.EInvoivePathExport + FTPFileName))
                {
                    file.WriteLine("Tax_Scheme! Document Category! Document Type! Billing Document! Invoice Date! Supplier GSTIN! Supplier trade_Name! Supplier_addr1! Supplier Location! Supplier Pincode! Supplier State Code! Buyer GSTIN! Buyer Name! Buyer State Code! Buyer_addr1! Buyer_loc! Buyer Pincode! disp_sup_trade_Name! disp_sup_addr1! disp_sup_loc! disp_sup_pin! disp_sup_stcd! TOTAL LINE ITEMS! Sl No! PrdDesc! IsServc! HSN Code! Quantity! Unit of measure! Unit Price! Total Amount(Tax Base)! Assesseeble Amount! Tax Rate! SGST Amount! IGST Amount! CGST Amount! CESS Rate! CESS Amount! Other Charges! Total Item Value! Accumulated Total Amount! Accumulated AssTotal Amount! Accumulated SgstVal! Accumulated IgstVal! Accumulated CgstVal! Accumulated CesVal!  Accumulated  Other Charges! Accumulated Tot Item Val! IRN! Reason for Cancellation! Cancellation comment! Type;");
                    int i = 0;
                    foreach (PDMS_EInvoiceItem Invi in Inv.EInvoiceItems)
                    {
                        if (i == 0)
                        {
                            string line = Inv.Tax_Scheme + "!" + Inv.DocumentCategory + "!" + Inv.DocumentType + "!" + Inv.BillingDocument + "!" + Inv.InvoiceDate + "!" + Inv.SupplierGSTIN + "!" + Inv.SupplierTrade_Name + "!" + Inv.Supplier_addr1 + "!" + Inv.SupplierLocation + "!" + Inv.SupplierPincode + "!" + Inv.SupplierStateCode + "!" + Inv.BuyerGSTIN + "!" + Inv.BuyerName + "!" + Inv.BuyerStateCode + "!" + Inv.Buyer_addr1 + "!" + Inv.Buyer_loc + "!" + Inv.BuyerPincode + "!" + Inv.SupplierTrade_Name + "!" + Inv.Supplier_addr1 + "!" + Inv.SupplierLocation + "!" + Inv.SupplierPincode + "!" + Inv.SupplierStateCode + "!" + Inv.TOTALLINEITEMS + "!!!!!!!!!!!!!!!!!!" + Inv.AccumulatedTotalAmount + "!" + Inv.AccumulatedAssTotalAmount + "!" + Inv.AccumulatedSgstVal + "!" + Inv.AccumulatedIgstVal + "!" + Inv.AccumulatedCgstVal + "!" + Inv.AccumulatedCesVal + "!" + Inv.AccumulatedOtherCharges + "!" + Inv.AccumulatedTotItemVal + "!" + Inv.IRN + "!" + Inv.ReasonForCancellation + "!" + Inv.CancellationComment + "!" + Inv.Type + ";";
                            file.WriteLine(line);
                            line = Inv.Tax_Scheme + "!" + Inv.DocumentCategory + "!" + Inv.DocumentType + "!" + Inv.BillingDocument + "!!!!!!!!!!!!!!!!!!!!" + Invi.SlNo + "!" + Invi.PrdDesc + "!" + Invi.IsServc + "!" + Invi.HSNCode + "!" + Invi.Quantity + "!" + Invi.UnitOfMeasure + "!" + Invi.UnitPrice + "!" + Invi.TotalAmount + "!" + Invi.AssesseebleAmount + "!" + Invi.TaxRate + "!" + Invi.SGSTAmount + "!" + Invi.IGSTAmount + "!" + Invi.CGSTAmount + "!" + Invi.CESSRate + "!" + Invi.CESSAmount + "!" + Invi.OtherCharges + "!" + Invi.TotalItemValue + "!" + "!" + "!" + "!" + "!" + "!" + "!" + "!" + "!" + "!!!" + Inv.Type + ";";
                            file.WriteLine(line);
                        }
                        else
                        {
                            string line = Inv.Tax_Scheme + "!" + Inv.DocumentCategory + "!" + Inv.DocumentType + "!" + Inv.BillingDocument + "!!!!!!!!!!!!!!!!!!!!" + Invi.SlNo + "!" + Invi.PrdDesc + "!" + Invi.IsServc + "!" + Invi.HSNCode + "!" + Invi.Quantity + "!" + Invi.UnitOfMeasure + "!" + Invi.UnitPrice + "!" + Invi.TotalAmount + "!" + Invi.AssesseebleAmount + "!" + Invi.TaxRate + "!" + Invi.SGSTAmount + "!" + Invi.IGSTAmount + "!" + Invi.CGSTAmount + "!" + Invi.CESSRate + "!" + Invi.CESSAmount + "!" + Invi.OtherCharges + "!" + Invi.TotalItemValue + "!" + "!" + "!" + "!" + "!" + "!" + "!" + "!" + "!" + "!!!" + Inv.Type + ";";
                            file.WriteLine(line);
                        }
                        if (i == Inv.EInvoiceItems.Count() - 1)
                        {
                            string line = Inv.Tax_Scheme + "!" + Inv.DocumentCategory + "!" + Inv.DocumentType + "!" + Inv.BillingDocument + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + Inv.AccumulatedTotalAmount + "!" + Inv.AccumulatedAssTotalAmount + "!" + Inv.AccumulatedSgstVal + "!" + Inv.AccumulatedIgstVal + "!" + Inv.AccumulatedCgstVal + "!" + Inv.AccumulatedCesVal + "!" + Inv.AccumulatedOtherCharges + "!" + Inv.AccumulatedTotItemVal + "!" + Inv.IRN + "!" + Inv.ReasonForCancellation + "!" + Inv.CancellationComment + "!" + Inv.Type + ";";
                            file.WriteLine(line);
                        }
                        i = i + 1;
                    }
                    file.Close();
                }
                if (new BDMS_EInvoice().UpdateEInvoiveStatus(Inv.BillingDocument, 1, Inv.FileSubName))
                {
                    if (new FileManager().UploadFile(Inv.EInvoiceFTPPath + "input_files/", Inv.EInvoiceFTPUserID, Inv.EInvoiceFTPPassword, PDMS_EInvoice.EInvoivePathExport + FTPFileName, FTPFileName))
                    {
                        new FileManager().MoveFile(new FileInfo(PDMS_EInvoice.EInvoivePathExport + FTPFileName), PDMS_EInvoice.EInvoivePathExportSuccess);
                    }
                }
            }
            catch (Exception e)
            { }
        }

        public Boolean UpdateEInvoiveBuyerDetails(string InvoiceNumber, string BuyerGSTIN, string BuyerStateCode, string Buyer_addr1, string Buyer_loc, string BuyerPincode,Int32 UserID)
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
        public Boolean UpdateEInvoiveHSNCode(string InvoiceNumber, long InvoiceItemID, string HSNCode, Int32 UserID)
        {
            try
            {
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", InvoiceNumber, DbType.String);
                DbParameter InvoiceItemIDP = provider.CreateParameter("InvoiceItemID", InvoiceItemID, DbType.String);
                DbParameter HSNCodeP = provider.CreateParameter("HSNCode", HSNCode, DbType.String); 
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.String);
                DbParameter[] Params = new DbParameter[4] { InvoiceNumberP, InvoiceItemIDP, HSNCodeP, UserIDP };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateEInvoiceHSNCode", Params); ;
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

        public void GeneratEInvoiceForSalesCommissionClaimInvoice1(string InvoiceNumber)
        {
            PDMS_EInvoice Inv = new BDMS_EInvoice().GetInvoiceForRequestEInvoice(InvoiceNumber, null, null, null, null)[0];


            try
            {
                PApiEInv ul = new PApiEInv();
                ul.handle = Inv.Dealer.EInvUserAPI.Handle;
                ul.handleType = Inv.Dealer.EInvUserAPI.HandleType;
                ul.password = Inv.Dealer.EInvUserAPI.Password;
                //  AccessToken = JsonConvert.DeserializeObject<PData>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PToken>(new BApiEInv().GetAccessToken(ul)).Data)).token;


                PEInvoice EInvoice = new PEInvoice();
                int TOTALLINEITEMS = 0;

                EInvoice.TranDtls = new PTranDtls() { };
                EInvoice.DocDtls = new PDocDtls()
                {
                    Typ = "INV",
                    No = Inv.BillingDocument,
                    Dt = Inv.InvoiceDate.Year.ToString() + Inv.InvoiceDate.Month.ToString("00") + Inv.InvoiceDate.Day.ToString("00"),
                };
                EInvoice.SellerDtls = new PSellerDtls()
                {

                    Gstin = Inv.SupplierGSTIN.Trim(),
                    LglNm = Inv.Dealer.DealerName,
                    TrdNm = "",
                    Addr1 = Inv.Supplier_addr1.Trim(),
                    Addr2 = "",
                    Loc = Inv.SupplierLocation.Trim(),
                    Pin = Inv.SupplierPincode.Trim(),
                    Stcd = Inv.SupplierStateCode.Trim(),
                    Ph = "",
                    Em = "",
                };

                //SupplierCode = Pinv.ICTicket.Dealer.DealerCode, 

                EInvoice.BuyerDtls = new PBuyerDtls()
                {
                    Gstin = Inv.BuyerGSTIN.Trim(),
                    LglNm = Inv.BuyerName.Trim(),
                    TrdNm = "",
                    Pos = "",
                    Addr1 = Inv.Buyer_addr1.Trim(),
                    Addr2 = "",
                    Loc = Inv.Buyer_loc.Trim(),
                    Pin = Inv.BuyerPincode.Trim(),
                    Stcd = Inv.BuyerStateCode.Trim(),
                    Ph = "",
                    Em = ""
                };
                EInvoice.DispDtls = new PDispDtls() { };
                EInvoice.ShipDtls = new PShipDtls() { };
                EInvoice.ItemList = new List<PItemList>();

                decimal AccumulatedTotalAmount = 0, AccumulatedAssTotalAmount = 0, AccumulatedSgstVal = 0, AccumulatedIgstVal = 0, AccumulatedCgstVal = 0,
                  AccumulatedCesVal = 0, AccumulatedOtherCharges = 0, AccumulatedTotItemVal = 0;
                TOTALLINEITEMS = 0;


                PDMS_EInvoiceItem Pinvi = Inv.EInvoiceItem;

                TOTALLINEITEMS = TOTALLINEITEMS + 1;

                AccumulatedTotalAmount = AccumulatedTotalAmount + Pinvi.TotalAmount;
                AccumulatedAssTotalAmount = AccumulatedAssTotalAmount + Pinvi.TotalAmount;
                AccumulatedSgstVal = AccumulatedSgstVal + Pinvi.SGSTAmount;
                AccumulatedIgstVal = AccumulatedIgstVal + Pinvi.IGSTAmount;
                AccumulatedCgstVal = AccumulatedCgstVal + Pinvi.CGSTAmount;
                AccumulatedCesVal = AccumulatedCesVal + 0;
                //     AccumulatedOtherCharges = AccumulatedOtherCharges + Pinvi.t;
                AccumulatedTotItemVal = AccumulatedAssTotalAmount + AccumulatedSgstVal + AccumulatedIgstVal + AccumulatedCgstVal + AccumulatedCesVal;

                EInvoice.ItemList.Add(new PItemList()
                {

                    SlNo = Convert.ToString(TOTALLINEITEMS),
                    PrdDesc = Pinvi.PrdDesc,
                    IsServc = "Y",
                    HsnCd = Pinvi.HSNCode,
                    Barcde = "",
                    Qty = Pinvi.Quantity.ToString(),
                    FreeQty = "0",
                    Unit = "NOS",
                    UnitPrice = Pinvi.UnitPrice.ToString(),
                    TotAmt = Pinvi.TotalAmount.ToString(),
                    Discount = "0",
                    PreTaxVal = "0",
                    AssAmt = Pinvi.TotalAmount.ToString(),
                    GstRt =   Pinvi.TaxRate,
                    IgstAmt = Convert.ToString(Pinvi.IGSTAmount),
                    CgstAmt = Convert.ToString(Pinvi.CGSTAmount),
                    SgstAmt = Convert.ToString(Pinvi.SGSTAmount),
                    CesRt = "",
                    CesAmt = "0",
                    CesNonAdvlAmt = "",
                    StateCesRt = "",
                    StateCesAmt = "",
                    StateCesNonAdvlAmt = "",
                    OthChrg = "",
                    TotItemVal = Convert.ToString(Pinvi.TotalAmount + Pinvi.SGSTAmount + Pinvi.IGSTAmount + Pinvi.CGSTAmount + Pinvi.CESSAmount),
                    OrdLineRef = "",
                    OrgCntry = "",
                    PrdSlNo = "",

                    // public PBchDtls BchDtls { get; set; }
                    // public List<PAttribDtls> AttribDtls { get; set; }
                });
                // InvoiceItemID = Pinvi.PaidServiceInvoiceItemID,
                // BillingDocument = Pinv.InvoiceNumber,   
                // CESSRate = "", 



                EInvoice.ValDtls = new PValDtls()
                {
                    AssVal = "",
                    CgstVal = Convert.ToString(AccumulatedCgstVal),
                    SgstVal = Convert.ToString(AccumulatedSgstVal),
                    IgstVal = Convert.ToString(AccumulatedIgstVal),
                    CesVal = Convert.ToString(AccumulatedCesVal),
                    StCesVal = "0",
                    Discount = "0",
                    OthChrg = Convert.ToString(AccumulatedOtherCharges),
                    RndOffAmt = "",
                    TotInvVal = Convert.ToString(AccumulatedTotItemVal),
                    TotInvValFc = Convert.ToString(AccumulatedTotItemVal)

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
                EInvoice.ExpDtls = new PExpDtls() { };
                EInvoice.EwbDtls = new PEwbDtls() { };

                // BuyerCode = Pinv.ICTicket.Customer.CustomerCode,


                //Type = "U",
                //FileSubName = "PAY"  
            }
            catch (Exception ex)
            {

            }
        }
      
    }
}
