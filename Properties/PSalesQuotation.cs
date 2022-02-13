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
        public string QuotationNo { get; set; }
        public DateTime QuotationDate { get; set; }
        public DateTime? RequestedDeliveryDate { get; set; }

        public PSaleQuotationType Type { get; set; }
        public PSaleQuotationStatus Status { get; set; }
        public PSaleQuotationRejectionReason RejectionReason { get; set; }

        public DateTime? ValidDate { get; set; }
        public DateTime? ValidTo { get; set; }

        public decimal NetValue { get; set; }
        public DateTime? PriceDate { get; set; }
        public PPriceGroup PriceGroup { get; set; }
        //Ajax product  
        public PSaleQuotationNote Note { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }
        public PDMS_Customer BillTo { get; set; }
        public PDMS_Customer ShipTo { get; set; }

        //Foc
        public string SpecialRequirements { get; set; }
        public string FocServiceKit { get; set; }
        public string FocWheelAssy { get; set; }
        public string FocExtensionChutes { get; set; }
        public string FocOthers { get; set; }
        public PSalesQuotationFinancier Financier { get; set; }

        public PSalesQuotationItem QuotationItem { get; set; }
        public List<PSalesQuotationItem> QuotationItems { get; set; }


        public string SalesOrderNumber { get; set; }
        public DateTime? SalesOrderDate { get; set; }
        public PDMS_PrimaryPurchaseOrder PrimaryPurchaseOrder { get; set; }
        public PDMS_PrimaryInvoice PrimaryInvoice { get; set; }
        public Decimal? InvoiceValue { get; set; }


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

    }

    [Serializable]
    public class PSalesQuotationItem
    {
        public long WebQuotationItemID { get; set; }
        public long WebQuotationID { get; set; }
        public PDMS_Material Material { get; set; }
        public int Qty { get; set; }
        public Decimal BasicPrice { get; set; }
        public Decimal? Discount1 { get; set; }
        public Decimal? Discount2 { get; set; }
        public Decimal? Discount3 { get; set; }
    }

    [Serializable]
    public class PSalesQuotationFinancier
    {
        public long QuotationFinancierID { get; set; }
        public long QuotationID { get; set; }
        //Financier 
        public PDMS_Financier Financier { get; set; }
        public PDMS_IncoTerm IncoTerm { get; set; }
        public PDMS_PaymentTerm CreditDays { get; set; }
        public string DoNumber { get; set; }
        public DateTime? DoDate { get; set; }

        //public Decimal? DoAmount { get; set; }
        //public Decimal? MarginMoney { get; set; }

        public Decimal? AdvanceAmount { get; set; }
        public Decimal? FinancierAmount { get; set; }

        public PMake Competitor { get; set; }
        public PProduct CompetitorProduct { get; set; }
        public PProductType CompetitorProductType { get; set; }

        //  public string BenificiaryOfDO { get; set; }
        //  public Decimal? SubventionAmount { get; set; }
        //  public string BackToBackDoEndorsedToAjax { get; set; }
        //  public string TransportationAndInsurance { get; set; }
    }
    public class PSaleQuotationStatus
    {
        public Int16 SaleQuotationStatusID { get; set; }
        public string Status { get; set; }
    }


    public class PSaleQuotationRejectionReason
    {
    }
    public class PSaleQuotationUserStatus
    {
    }
    public class PSaleQuotationType
    {
    }
    public class PSaleQuotationNote
    {
    }
    public class PPriceGroup
    {
    }
}
