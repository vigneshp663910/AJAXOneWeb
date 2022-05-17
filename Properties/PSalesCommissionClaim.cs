using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class PSalesCommissionClaim
    {
        public long SalesCommissionClaimID { get; set; }
        public string ClaimNumber { get; set; }
        public DateTime ClaimDate { get; set; }
        public PDMS_Customer Customer { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PUser CreatedBy { get; set; }
        public PUser Approved1By { get; set; }
        public DateTime? Approved1On { get; set; }
        public PUser Approved2By { get; set; }
        public DateTime? Approved2On { get; set; }
        public PUser Approved3By { get; set; }
        public DateTime? Approved3On { get; set; }
        public PSalesCommissionClaimStatus Status { get; set; }
        public PSalesQuotation Quotation { get; set; }
        public PSalesCommissionClaimItem ClaimItem { get; set; }
    }
    [Serializable]
    public class PSalesCommissionClaimItem
    {
        public long SalesCommissionClaimItemID { get; set; }
        public long SalesCommissionClaimID { get; set; }
        public string Item { get; set; }
        public PDMS_Material Material { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal BaseTax { get; set; }
        public decimal? Approved1Amount { get; set; }
        public string Approved1Remarks { get; set; }
        public decimal? Approved2Amount { get; set; }
        public string Approved2Remarks { get; set; }
        public decimal? Approved3Amount { get; set; }
        public string Approved3Remarks { get; set; }

    }
    [Serializable]
    public class PSalesCommissionClaimStatus
    {
        public long StatusID { get; set; }
        public string Status { get; set; }
    }

    [Serializable]
    public class PSalesCommissionClaimInvoice
    {
        public long SalesCommissionClaimInvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }

        public PSalesCommissionClaim Claim { get; set; }
        public PSalesQuotation Quotation { get; set; }
        public PDMS_Dealer Dealer { get; set; }

        public int GrandTotal { get; set; }
        public decimal TCSValue { get; set; }
        public decimal TCSTax { get; set; }

        public PUser CreatedBy { get; set; }


        public string IRN { get; set; }
        public DateTime? IRNDate { get; set; }
        public string SAPDoc { get; set; }
        public DateTime? SAPPostingDate { get; set; }
        public decimal? SAPInvoiceValue { get; set; }
        public string SAPClearingDocument { get; set; }
        public DateTime? SAPClearingDate { get; set; }
        public decimal? SAPInvoiceTDSValue { get; set; }


        public PSalesCommissionClaimInvoiceItem InvoiceItem { get; set; }
        public PSalesCommissionClaimInvoiceDetails InvoiceDetails { get; set; }
    }
    [Serializable]
    public class PSalesCommissionClaimInvoiceItem
    {
        public long SalesCommissionClaimInvoiceItemID { get; set; }
        public long SalesCommissionClaimInvoiceID { get; set; }
        public string Item { get; set; }
        public PDMS_Material Material { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }
    }
    [Serializable]
    public class PSalesCommissionClaimInvoiceDetails
    {
        public long SalesCommissionClaimInvoiceDetailsID { get; set; } 
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
