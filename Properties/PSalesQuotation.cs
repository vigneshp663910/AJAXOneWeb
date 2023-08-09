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
        public string RevisionNumber { get; set; }
        public string SapQuotationNo { get; set; }
        public DateTime? SapQuotationDate { get; set; } 
        public string PgQuotationNo { get; set; }
        public DateTime? PgQuotationDate { get; set; } 
        public DateTime? RequestedDeliveryDate { get; set; }
        public PSalesQuotationType QuotationType { get; set; }
        public PSalesQuotationStatus Status { get; set; }
        public PSaleQuotationRejectionReason RejectionReason { get; set; }
        public PSaleQuotationRejectionReason UserStatusRemarks { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? PricingDate { get; set; }
        public Boolean CommissionAgent { get; set; }
        public PPriceGroup PriceGroup { get; set; }
        public PSalesQuotationUserStatus UserStatus { get; set; }
        public decimal? NetValue { get; set; }

        //Ajax product    
        public PDMS_CustomerShipTo ShipTo { get; set; }
        public PSalesQuotationFinancier Financier { get; set; }
        public PSalesQuotationItem QuotationItem { get; set; }
        public PDMS_Model Model { get; set; }
        public List<PSalesQuotationItem> QuotationItems { get; set; }
        public List<PSalesQuotationCompetitor> Competitor { get; set; }
        public PLead Lead { get; set; }
        public  PLeadProduct  LeadProduct { get; set; }
        public List<PSalesQuotationNote> Notes { get; set; }
        public PDMS_Material Material { get; set; }
        public string SaleOrderNumber { get; set; }
        public DateTime? SaleOrderDate { get; set; }
        public string DeliveryNumber { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string SalesInvoiceNumber { get; set; }
        public DateTime? SalesInvoiceDate { get; set; }
        public string AccountNumber { get; set; }
        public DateTime? AccountDate { get; set; }
        public string EquipmentSerialNo { get; set; }
        public PUser CreatedBy { get; set; }

        public decimal GrossValue { get; set; }
        public decimal TotalEffort { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal LifeTimeTax { get; set; }

        public Boolean IsStandard { get; set; }
        public PSalesQuotationApprovalStatus ApprovalStatus { get; set; }

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
    public class PSalesQuotation_Insert
    {
        public long QuotationID { get; set; }
        public long LeadID { get; set; }
        public long LeadProductID { get; set; }
        public int? SalesQuotationUserStatusID { get; set; }
        public DateTime? RequestedDeliveryDate { get; set; }
        public long? CustomerShipToID { get; set; }
        public int? SalesQuotationRejectionReasonID { get; set; }

        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? PricingDate { get; set; }
        public Boolean CommissionAgent { get; set; }
        public PPriceGroup PriceGroup { get; set; }  
        public decimal LifeTimeTax { get; set; }
        public Boolean IsStandard { get; set; }

    }

    [Serializable]
    public class PSalesQuotationItem
    {
        public long SalesQuotationItemID { get; set; }
        public long SalesQuotationID { get; set; }
        public int Item { get; set; }
        public PDMS_Material Material { get; set; }
        public int Qty { get; set; }
        public Decimal AspPrice { get; set; }
        public Decimal Rate { get; set; }
        public Decimal? Discount { get; set; }
        public Decimal TaxableValue { get; set; }

        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }
        public decimal TCSValue { get; set; }
        public decimal TCSTax { get; set; }
        public decimal LifeTimeValue { get; set; }
        public decimal LifeTimeTax { get; set; }
        //public Decimal TaxPersent { get; set; }
        //public Decimal TaxValue { get; set; }
        public Decimal NetValue { get; set; }
      //  public PPlant Plant { get; set; }
        public PSaleQuotationRejectionReason RejectionReason { get; set; }
        public PUser CreatedBy { get; set; }
        public string SapFlag { get; set; }
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
    [Serializable]
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

    [Serializable]
    public class PSalesQuotationFollowUp
    {
        public long SalesQuotationFollowUpID { get; set; }
        public long SalesQuotationID { get; set; }
        public string FollowUpNote { get; set; }
        public DateTime FollowUpDate { get; set; }
        public PUser SalesEngineer { get; set; }
        public PUser CreatedBy { get; set; }

        public PSalesQuotation SalesQuotation { get; set; }
        public PPreSaleStatus Status { get; set; }
        public PDMS_Customer Customer { get; set; }
        public string Remark { get; set; }
    }
    [Serializable]
    public class PSalesQuotationEffort
    {
        public long SalesQuotationEffortID { get; set; }
        public long SalesQuotationID { get; set; }
        public PUser SalesEngineer { get; set; }
        public DateTime EffortDate { get; set; }
        public decimal EffortStartTime { get; set; }
        public decimal EffortEndTime { get; set; }
        public decimal Effort { get; set; }
        public PEffortType EffortType { get; set; }
        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
    }
    [Serializable]
    public class PSalesQuotationExpense
    {
        public long SalesQuotationExpenseID { get; set; }
        public long SalesQuotationID { get; set; }
        public PUser SalesEngineer { get; set; }
        public DateTime ExpenseDate { get; set; }
        public PExpenseType ExpenseType { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
    }

    [Serializable]
    public class PSalesQuotationDocumentDetails
    {
        public long SalesQuotationDocumentDetailsID { get; set; }        
        public string QuotationNo { get; set; }
        public int Item { get; set; }
        public int SubSequentItem { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentName { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string Material { get; set; }
        public string MachineSerialNumber { get; set; }  
    }
    [Serializable]
    public class PSalesQuotationApprovalStatus
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }
    [Serializable]
    public class PSalesQuotationApprovedBy
    {
        public int ApprovedByID { get; set; }
        public PUser ApprovedBy { get; set; }
        public PSalesQuotationApprovalStatus ApprovalLevel { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string Remark { get; set; }
    }
}