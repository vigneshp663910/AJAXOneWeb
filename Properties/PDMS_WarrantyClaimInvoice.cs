using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_WarrantyClaimInvoice
    {

        public long WarrantyClaimInvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public PDMS_Dealer Dealer { get; set; } 
        public int GrandTotal { get; set; }
        public int TempTcsMatCount { get; set; }
        public decimal TCSValue { get; set; }
        public decimal TCSTax { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int MonthRange { get; set; }
        public string AnnexureNumber { get; set; }
        public DateTime? PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }
        public string Through { get; set; }
        public string LRNumber { get; set; }
        public string IRN { get; set; }
        public DateTime? IRNDate { get; set; }
        public string SAPDoc { get; set; }
        public DateTime? SAPPostingDate { get; set; }
        public decimal? SAPInvoiceValue { get; set; }
        public string SAPClearingDocument { get; set; }
        public DateTime? SAPClearingDate { get; set; }
        public decimal? SAPInvoiceTDSValue { get; set; }

        public List<PDMS_WarrantyClaimInvoiceItem> InvoiceItems { get; set; }

        public PDMS_WarrantyInvoiceType InvoiceType { get; set; }
        public PDMS_WarrantyClaimInvoiceDetails InvoiceDetails { get; set; }
    }
    [Serializable]
    public class PDMS_WarrantyClaimInvoiceItem
    {

        public long WarrantyClaimInvoiceItemID { get; set; }
        public long WarrantyClaimInvoiceID { get; set; }
        public string Category { get; set; }
        public string Material { get; set; }
        public string MaterialDesc { get; set; }
        public string HSNCode { get; set; }
        public string UOM { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal ApprovedValue { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal DepitValue { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }
        public string DeliveryChallan { get; set; }
        public long WarrantyClaimAnnexureItemID { get; set; }

        public string Remark { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] AttachedByte { get; set; }
        public int FileSize { get; set; } 

    }

    [Serializable]
    public class PDMS_WarrantyInvoiceType
    {
        public int InvoiceTypeID { get; set; }
        public string InvoiceType { get; set; }
    }
    [Serializable]
    public class PDMS_WarrantyClaimInvoiceDetails
    {
        public long InvoiceDetailsID { get; set; }
        public long PaidServiceInvoiceID { get; set; }

        public string SupplierGSTIN { get; set; }
       
        public string Supplier_addr1 { get; set; }
        public string SupplierLocation { get; set; }
        public string SupplierPincode { get; set; }
        public string SupplierStateCode { get; set; }
        public string BuyerName { get; set; }
        public string BuyerGSTIN { get; set; }
        public string BuyerStateCode { get; set; }
        public string Buyer_addr1 { get; set; }
        public string Buyer_loc { get; set; }
        public string BuyerPincode { get; set; }
        public string disp_sup_trade_Name { get; set; }
        public string disp_sup_addr1 { get; set; }
        public string disp_sup_loc { get; set; }
        public string disp_sup_pin { get; set; }
        public string disp_sup_stcd { get; set; }
    }
}
