using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class PSalesQuotation
    {
        public long QuotationID { get; set; }
        public string RefQuotationNo { get; set; }
        public DateTime RefQuotationDate { get; set; }
        public string QuotationNo { get; set; }
        public DateTime? QuotationDate { get; set; }
        public DateTime? RequestedDeliveryDate { get; set; }
        public PSalesQuotationType QuotationType { get; set; }
        public PSalesQuotationStatus Status { get; set; }
        public PSaleQuotationRejectionReason RejectionReason { get; set; }
        public PSaleQuotationRejectionReason UserStatusRemarks { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? PricingDate { get; set; }
        public PPriceGroup PriceGroup { get; set; }
        public PSalesQuotationUserStatus UserStatus { get; set; }
        public decimal? NetValue { get; set; }
        //Ajax product   
        public PDMS_Customer BillTo { get; set; }
        public PDMS_Customer ShipTo { get; set; }
        public PSalesQuotationFinancier Financier { get; set; }
        public PSalesQuotationItem QuotationItem { get; set; }
        public List<PSalesQuotationItem> QuotationItems { get; set; }
        public List<PSalesQuotationCompetitor> Competitor { get; set; }
        public PLead Lead { get; set; }
        public List<PSalesQuotationNote> Notes { get; set; }
        public string SalesOrderNo { get; set; }
        public DateTime? SalesOrderDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public PUser CreatedBy { get; set; }

        //public Decimal? DiscountSales { get; set; }
        //public Decimal? FreightValue { get; set; }
        //public Decimal? InsuranceValue { get; set; }
        //public DateTime? TRDate { get; set; }
        //public Boolean ConsolidationInvoicePrint { get; set; }
        //public Decimal? FreightAmount { get; set; }
        //public string Billing { get; set; }
        //public PDMS_EquipmentHeader Equipment { get; set; }
        //public PDMS_Address InvoiceAddress { get; set; }
        //public PDMS_Address ShipToAddress { get; set; }
        //public string SalesOrderStatus { get; set; }
        //  public string Office { get; set; }
        //public PDMS_MainApplication MainApplication { get; set; }
        //public string RetailCustomer { get; set; }
        // public string Hypothecation { get; set; }  
        //public PDMS_SourceOfEnquiry SourceOfEnquiry { get; set; }
        // public string ReasonForOrderConversion { get; set; }
        // public string CustomerType { get; set; }
        // public string Profile { get; set; }
        // public string Size { get; set; }
        // public string OwnershipPattern { get; set; }
        // public string NameOfTheProject { get; set; }
        // public PDMS_DiscountType ModeOfBilling { get; set; } 
        //public PUser Approver { get; set; }
        //public Boolean SendToSAP { get; set; }
        //public Boolean IsSuccess { get; set; }
        //public string SapStatus
        //{
        //    get
        //    {
        //        return SendToSAP ? (IsSuccess ? "Success" : "Not Success") : "Not Send To SAP";
        //    }
        //}
        //Foc
        //public string SpecialRequirements { get; set; }
        //public string FocServiceKit { get; set; }
        //public string FocWheelAssy { get; set; }
        //public string FocExtensionChutes { get; set; }
        //public string FocOthers { get; set; }
        //public PDMS_PrimaryInvoice PrimaryInvoice { get; set; }
        //public PDMS_PrimaryPurchaseOrder PrimaryPurchaseOrder { get; set; }
    }

    [Serializable]
    public class PSalesQuotationItem
    {
        public long SalesQuotationItemID { get; set; }
        public long SalesQuotationID { get; set; }
        public int Item { get; set; }
        public PDMS_Material Material { get; set; }
        public int Qty { get; set; }
        public Decimal Rate { get; set; }
        public Decimal? Discount { get; set; }
        public Decimal TaxableValue { get; set; }

        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }

        //public Decimal TaxPersent { get; set; }
        //public Decimal TaxValue { get; set; }
        public Decimal NetValue { get; set; }
        public PPlant Plant { get; set; }
        public PSaleQuotationRejectionReason RejectionReason { get; set; }
        public PUser CreatedBy { get; set; }
    }

    [Serializable]
    public class PSalesQuotationFinancier
    {
        public long SalesQuotationFinancierID { get; set; }
        public long QuotationID { get; set; }
        //Financier 
        public PBankName BankName { get; set; }
        public PIncoTerms IncoTerms { get; set; }
        public PPaymentTerms PaymentTerms { get; set; }
        public string DoNumber { get; set; }
        public DateTime? DoDate { get; set; }

        //public Decimal? DoAmount { get; set; }
        //public Decimal? MarginMoney { get; set; }

        public Decimal? AdvanceAmount { get; set; }
        public Decimal? FinancierAmount { get; set; }
        public PUser CreatedBy { get; set; }


        //  public string BenificiaryOfDO { get; set; }
        //  public Decimal? SubventionAmount { get; set; }
        //  public string BackToBackDoEndorsedToAjax { get; set; }
        //  public string TransportationAndInsurance { get; set; }
    }

    [Serializable]
    public class PSalesQuotationCompetitor
    {
        public long SalesQuotationCompetitorID { get; set; }
        public long SalesQuotationID { get; set; }
        public PMake Make { get; set; }
        public PProductType ProductType { get; set; }
        public PProduct Product { get; set; }

        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
    }

    [Serializable]
    public class PSalesQuotationNote
    {
        public long SalesQuotationNoteID { get; set; }
        public long SalesQuotationID { get; set; }
        public PSalesQuotationNoteList Note { get; set; }
        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
    }

    [Serializable]
    public class PSalesQuotationType
    {
        public Int32 QuotationTypeID { get; set; }
        public string QuotationType { get; set; }
    }
    public class PSalesQuotationStatus
    {
        public Int32 SalesQuotationStatusID { get; set; }
        public string SalesQuotationStatus { get; set; }
    }
    [Serializable]
    public class PSalesQuotationUserStatus
    {
        public Int32 SalesQuotationUserStatusID { get; set; }
        public string SalesQuotationUserStatus { get; set; }
    }

    [Serializable]
    public class PSaleQuotationRejectionReason
    {
        public Int32 SalesQuotationRejectionReasonID { get; set; }
        public string Reason { get; set; }
    }

    [Serializable]
    public class PSalesQuotationNoteList
    {
        public Int32 SalesQuotationNoteListID { get; set; }
        public string Note { get; set; }
    }









}
