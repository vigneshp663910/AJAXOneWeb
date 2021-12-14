using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_PaidServiceHeader
    {
        public long PaidServiceHeaderID { get; set; }
        public string ICTicketNumber { get; set; }
        public DateTime ICTicketDate { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }
        public PDMS_ServiceStatus ServiceStatus { get; set; }
        public PDMS_ServiceType ServiceType { get; set; }
        public string Model { get; set; }
        public DateTime? Date { get; set; }
        public string Peirod { get; set; }
        public PUser Technician { get; set; }
        public DateTime? RestoreDate { get; set; }
        public PDMS_PaidServiceItem ServiceItem { get; set; }
        public List<PDMS_PaidServiceItem> ServiceItems { get; set; }
    }
    [Serializable]
    public class PDMS_PaidServiceItem
    {
        public long PaidServiceItemID { get; set; }
        public long PaidServiceHeaderID { get; set; }
        public string Material { get; set; }
        public string MaterialDesc { get; set; }
        public string UnitOM { get; set; }
        public decimal? ValueBeforeTax { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Total { get; set; }
        public string QuotationNumber { get; set; }
        public string ProformaInvoiceNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
    }

    [Serializable]
    public class PDMS_PaidServiceInvoice
    {
        public long PaidServiceInvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int GrandTotal { get; set; }
        public decimal TCSValue { get; set; }
        public decimal TCSTax { get; set; }
       // public string Remarks { get; set; }
        public string Through { get; set; }
        public string LRNumber { get; set; }
        public PDMS_ICTicket ICTicket { get; set; }
        public PDMS_PaidServiceInvoiceItem InvoiceItem { get; set; }
        public List<PDMS_PaidServiceInvoiceItem> InvoiceItems { get; set; }
        public string ProformaInvoiceNumber { get; set; }
        public DateTime ProformaInvoiceDate { get; set; }
        public Boolean IsDeletionAllowed { get; set; }
        public Boolean IsActiveInvoice { get; set; }
        public PDMS_PaidServiceInvoiceDetails InvoiceDetails { get; set; }
        public string IRN { get; set; }
    }
    [Serializable]
    public class PDMS_PaidServiceInvoiceItem
    {
        public long PaidServiceInvoiceItemID { get; set; }
        public long PaidServiceInvoiceID { get; set; }
        public string DeliveryNumber { get; set; }
        public string Item { get; set; }
        public PDMS_Material Material { get; set; }
        public string UnitOM { get; set; }        
        public decimal? Qty { get; set; }
        public decimal? Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }
        public decimal CessValue { get; set; } 
    }
     [Serializable]
    public class PDMS_PaidServiceInvoiceDetails
    {
        public long InvoiceDetailsID { get; set; }
        public long PaidServiceInvoiceID { get; set; }
        public string SupplierGSTIN { get; set; }
        public string Supplier_addr1 { get; set; }
        public string SupplierLocation { get; set; }
        public string SupplierPincode { get; set; }
        public string SupplierStateCode { get; set; }

        public string BuyerGSTIN { get; set; }
        public string BuyerName { get; set; }         
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
