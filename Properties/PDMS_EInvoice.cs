using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_EInvoice
    {
        public static DateTime EInvoiveDate
        {
            get
            {
                return Convert.ToDateTime(ConfigurationManager.AppSettings["EInvoiveDate"]);
            }
        }
        public static DateTime TcsDate
        {
            get
            {
                return Convert.ToDateTime(ConfigurationManager.AppSettings["TcsDate"]);
            }
        }
        public static decimal TcsTax
        {
            get
            {
                return Convert.ToDecimal(ConfigurationManager.AppSettings["TcsTax"]);
            }
        }

        public static String EInvoivePath
        {
            get
            {
                string _EInvoivePath = Convert.ToString(ConfigurationManager.AppSettings["EInvoivePath"]);
                if (!Directory.Exists(_EInvoivePath))
                {
                    Directory.CreateDirectory(_EInvoivePath);
                    Directory.CreateDirectory(_EInvoivePath + "Export");
                    Directory.CreateDirectory(_EInvoivePath + "Export/Success");
                    Directory.CreateDirectory(_EInvoivePath + "Export/Fail");

                    Directory.CreateDirectory(_EInvoivePath + "Import");
                    Directory.CreateDirectory(_EInvoivePath + "Import/Success");
                    Directory.CreateDirectory(_EInvoivePath + "Import/Fail");
                }
                return _EInvoivePath;
            }
        }
        public static String EInvoivePathExport
        {
            get
            {
                return PDMS_EInvoice.EInvoivePath + "/Export/";
            }
        }
        public static String EInvoivePathExportSuccess
        {
            get
            {
                return PDMS_EInvoice.EInvoivePath + "/Export/Success/";
            }
        }
        public static String EInvoivePathExportFail
        {
            get
            {
                return PDMS_EInvoice.EInvoivePath + "/Export/Fail/";
            }
        }
        public static String EInvoivePathImport
        {
            get
            {
                return PDMS_EInvoice.EInvoivePath + "Import";
            }
        }
        public static String EInvoivePathImportFail
        {
            get
            {
                return PDMS_EInvoice.EInvoivePath + "/Import/Fail";
            }
        }
        public static String EInvoivePathImportSuccess
        {
            get
            {
                return PDMS_EInvoice.EInvoivePath + "Import/Success";
            }
        }


        public long EInvoiceID { get; set; }
        public string Tax_Scheme { get; set; }
        public string DocumentCategory { get; set; }
        public string DocumentType { get; set; }
        public string BillingDocument { get; set; }
        public string InvoiceDate { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierGSTIN { get; set; }
        public string SupplierTrade_Name { get; set; }
        public string Supplier_addr1 { get; set; }
        public string SupplierLocation { get; set; }
        public string SupplierPincode { get; set; }
        public string SupplierStateCode { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerGSTIN { get; set; }
        public string BuyerName { get; set; }
        public string BuyerStateCode { get; set; }
        public string Buyer_addr1 { get; set; }
        public string Buyer_loc { get; set; }
        public string BuyerPincode { get; set; }
        //public string disp_sup_trade_Name { get; set; }
        //public string disp_sup_addr1 { get; set; }
        //public string disp_sup_loc { get; set; }
        //public string disp_sup_pin { get; set; }
        //public string disp_sup_stcd { get; set; }
        public string TOTALLINEITEMS { get; set; }


        public string AccumulatedTotalAmount { get; set; }
        public string AccumulatedAssTotalAmount { get; set; }
        public string AccumulatedSgstVal { get; set; }
        public string AccumulatedIgstVal { get; set; }
        public string AccumulatedCgstVal { get; set; }
        public string AccumulatedCesVal { get; set; }
        public string AccumulatedOtherCharges { get; set; }

        public string AccumulatedTotItemVal { get; set; }
        public string IRN { get; set; }
        public string ReasonForCancellation { get; set; }
        public string CancellationComment { get; set; }
        public string Type { get; set; }


        public string FileSubName { get; set; }
        public string EInvoiceFTPPath { get; set; }
        public string EInvoiceFTPUserID { get; set; }
        public string EInvoiceFTPPassword { get; set; }

        public PDMS_EInvoiceItem EInvoiceItem { get; set; }
        public List<PDMS_EInvoiceItem> EInvoiceItems { get; set; }
        public Boolean Checked { get; set; }
    }
     [Serializable]
    public class PDMS_EInvoiceItem
    {
        public int EInvoiceID { get; set; }
        public long InvoiceItemID { get; set; }
        public string BillingDocument { get; set; }
        public string SlNo { get; set; }
        public string PrdDesc { get; set; }
        public string IsServc { get; set; }
        public string HSNCode { get; set; }
        public string Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public string UnitPrice { get; set; }
        public string TotalAmount { get; set; }
        public string AssesseebleAmount { get; set; }
        public string TaxRate { get; set; }
        public string SGSTAmount { get; set; }
        public string IGSTAmount { get; set; }
        public string CGSTAmount { get; set; }
        public string CESSRate { get; set; }
        public string CESSAmount { get; set; }
        public string OtherCharges { get; set; }
        public string TotalItemValue { get; set; }
    }

    [Serializable]
    public class PDMS_EInvoiceSigned
    {
        public long EInvoiceID { get; set; }
        public long RefInvoiceID { get; set; }
        public string IRN { get; set; }
        public string SignedQRCode { get; set; }
        public string SignedInvoice { get; set; }
        public string Comments { get; set; }
    }
}
