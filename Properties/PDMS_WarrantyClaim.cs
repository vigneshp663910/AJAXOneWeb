using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_WarrantyClaim_Old
    {
        public string ClaimID { get; set; }
        public DateTime? ClaimDate { get; set; }

        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Reference { get; set; }
        public string PartNumber { get; set; }
        public string MachineSerialNo { get; set; }
        public decimal? Base { get; set; }
        public decimal? Base_Tax1 { get; set; }
        public string ICLicketID { get; set; }
        public DateTime? ICLoginDate { get; set; }
        public string HsnCode { get; set; }
        public decimal? LevelOneApprovedAmt { get; set; }
        public decimal? LevelTwoApprovedAmt { get; set; }
        public string LevelOneApprover { get; set; }
        public string LevelTwoApprover { get; set; }
        public DateTime? LevelOneApproverDate { get; set; }
        public DateTime? LevelTwoApproverDate { get; set; }
       // public string Remarks { get; set; }
        public string Model { get; set; }
        public int? HMR { get; set; }
        public Boolean? MarginWarranty { get; set; }
        public string Status { get; set; }
        public string Item { get; set; }
        public string RefDocID { get; set; }
        public string MaterialDesc { get; set; }
        public decimal? Qty { get; set; }
        public string UnitOM { get; set; }
        public string PscID { get; set; }
          public string ReasonForFailure { get; set; }
        
        public DateTime? DateOfCommissioning { get; set; }
        public string FSRNumber { get; set; }
        public string TSIRNumber { get; set; }
        public DateTime? RestoreDate { get; set; }
        public string BriefDescriptionOfJob { get; set; }

        public string Location { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string Application { get; set; }

    }
    [Serializable]
    public class PDMS_WarrantyClaimHeader_Old
    {
        public string ClaimID { get; set; }
        public DateTime? ClaimDate { get; set; }
        public string ICTicketID { get; set; }
        public DateTime? ICTicketDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }

        public PUser Approved1By { get; set; } 
        public DateTime? Approved1On { get; set; }
        public PUser Approved2By { get; set; } 
        public DateTime? Approved2On { get; set; }

        public int? HMR { get; set; }
        public Boolean? MarginWarranty { get; set; }
        public string MachineSerialNumber { get; set; }
        public string Status { get; set; }
        public string PscID { get; set; }
        public string ReasonForFailure { get; set; }
      public DateTime?  DateOfCommissioning { get; set; }
        public string FSRNumber { get; set; }
        public string TSIRNumber { get; set; }
        public DateTime? RestoreDate { get; set; }
        public PDMS_WarrantyClaimItem_Old ClaimItem { get; set; }
        public List<PDMS_WarrantyClaimItem_Old> ClaimItems { get; set; } public long WarrantyClaimHeaderID { get; set; }

        public string OrderType = "ZDIP";
        public string SalesOrg = "AJF";
        public string DisChannel = "DS";
        public string Division = "SP";
        public string Location { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string Application { get; set; }
        //public string PricingDate { get; set; }

        public string PaymentTerm = "C000";
        public string IncoTerms = "EXW";
        public string OrderReason = "215";
        public string Plant { get; set; }
        public string Partner { get; set; }
      //  public string PricingDate { get; set; }
       
    }
    [Serializable]
    public class PDMS_WarrantyClaimItem_Old
    {
        public long WarrantyClaimItemID { get; set; }
        public long WarrantyClaimHeaderID { get; set; }
        public string ClaimID { get; set; }
        public string Item { get; set; }
        public string RefDocID { get; set; }
        public string Material { get; set; }
        public string MaterialDesc { get; set; }
      
        public decimal? Qty { get; set; }
        public string UnitOM { get; set; }
        public decimal? Amount { get; set; }
        public decimal? BaseTax { get; set; }
        public string MaterialStatus { get; set; }
        public string MaterialStatusRemarks1 { get; set; }
        public string MaterialStatusRemarks2 { get; set; } 
        public decimal? Approved1Amount { get; set; }
        public string Approved1Remarks { get; set; } 
        public decimal? Approved2Amount { get; set; }
        public string Approved2Remarks { get; set; } 
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }      
        public string Model { get; set; }
        public string Category { get; set; }
        public string HSNCode { get; set; }
        public string BriefDescriptionOfJob { get; set; }

        public string SAPDoc { get; set; }
        public DateTime? SAPPostingDate { get; set; }
        public decimal? SAPInvoiceValue { get; set; }
       
    }
    [Serializable]
    public class PDMS_WarrantyStatus
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }
    [Serializable]
    public class PDMS_WarrantyClaimCategory
    {
        public int CategoryID { get; set; }
        public string Category { get; set; } 
    }
    [Serializable]
    public class PDMS_WarrantyAttachment
    {
        public int AttachmentID { get; set; }
        public string PscID { get; set; }
        public string fileName { get; set; }
        public string Url { get; set; }
    }

    [Serializable]
    public class PDMS_WarrantySAPInvoice
    { 
        public string InvoiceNumber { get; set; } 
        public string SAPDoc { get; set; }
        public decimal? SAPInvoiceValue { get; set; }
        
    }

    [Serializable]
    public class PDMS_ClaimInvoiceHeader
    {
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }

        public string ClaimID { get; set; }
        public DateTime? ClaimDate { get; set; }
        public string ICTicketID { get; set; }
        public DateTime? ICTicketDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }

        public PUser Approved1By { get; set; }
        public DateTime? Approved1On { get; set; }
        public PUser Approved2By { get; set; }
        public DateTime? Approved2On { get; set; }

        public int? HMR { get; set; }
        public Boolean? MarginWarranty { get; set; }
        public string MachineSerialNumber { get; set; }
        public string Status { get; set; }
        public string PscID { get; set; }
        public string TSIRNumber { get; set; }
        public DateTime? RestoreDate { get; set; }
        public PDMS_ClaimInvoiceItem ClaimItem { get; set; }
        public List<PDMS_ClaimInvoiceItem> ClaimItems { get; set; } 
        public long WarrantyClaimHeaderID { get; set; }

        public string OrderType = "ZDIP";
        public string SalesOrg = "AJF";
        public string DisChannel = "DS";
        public string Division = "SP";
        public string Location { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string Application { get; set; }
        //public string PricingDate { get; set; }

        public string PaymentTerm = "C000";
        public string IncoTerms = "EXW";
        public string OrderReason = "215";
        public string Plant { get; set; }
        public string Partner { get; set; }

        public string ReasonForFailure { get; set; }
      public DateTime?  DateOfCommissioning { get; set; }
        public string FSRNumber { get; set; }
        //  public string PricingDate { get; set; }

    }
    [Serializable]
    public class PDMS_ClaimInvoiceItem
    {
        public long WarrantyClaimItemID { get; set; }
        public long WarrantyClaimHeaderID { get; set; }
        public string ClaimID { get; set; }
        public string Item { get; set; }
        public string RefDocID { get; set; }
        public string Material { get; set; }
        public string MaterialDesc { get; set; }

        public decimal? Qty { get; set; }
        public string UnitOM { get; set; }
        public decimal? Amount { get; set; }
        public decimal? BaseTax { get; set; }
        public string MaterialStatus { get; set; }
        public string MaterialStatusRemarks1 { get; set; }
        public string MaterialStatusRemarks2 { get; set; }
        public decimal? Approved1Amount { get; set; }
        public string Approved1Remarks { get; set; }
        public decimal? Approved2Amount { get; set; }
        public string Approved2Remarks { get; set; }
       
        public string Model { get; set; }
        public string Category { get; set; }
        public string HSNCode { get; set; }
        public string BriefDescriptionOfJob { get; set; }

        public string SAPDoc { get; set; }
        public decimal? SAPInvoiceValue { get; set; }
       
    }

    [Serializable]
    public class PDMS_WarrantyTicket
    {
        
        public string ICTicketID { get; set; }
        public DateTime? ICTicketDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }      

        public int? HMR { get; set; }
        public Boolean? MarginWarranty { get; set; }
        public string MachineSerialNumber { get; set; }
        public string Model { get; set; }        
        public string PscID { get; set; }
        public string ReasonForFailure { get; set; }
        public DateTime? DateOfCommissioning { get; set; }
        public string FSRNumber { get; set; }
        public string TSIRNumber { get; set; }
        public DateTime? RestoreDate { get; set; }
        public List<PDMS_WarrantyInvoiceHeader> Invoice { get; set; }
      
    }

    [Serializable]
    public class PDMS_WarrantyInvoiceHeader_New
    {
        public long WarrantyInvoiceHeaderID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }

        public long ICTicketID { get; set; }
        public string ICTicketNumber { get; set; }
        public DateTime? ICTicketDate { get; set; }

        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }

        public PDMS_ICTicket ICTicket { get; set; }


        public PUser Approved1By { get; set; }
        public DateTime? Approved1On { get; set; }
        public PUser Approved2By { get; set; }
        public DateTime? Approved2On { get; set; }

        public PUser Approved3By { get; set; }
        public DateTime? Approved3On { get; set; }


        public int? HMR { get; set; }
        public Boolean? MarginWarranty { get; set; }
        public string MachineSerialNumber { get; set; }
        public long? EquipmentHeaderID { get; set; }
        public string Model { get; set; }
        public string InvoiceStatus { get; set; }
        public string ClaimStatus { get; set; }
        public string PscID { get; set; }
        public string ReasonForFailure { get; set; }
        public DateTime? DateOfCommissioning { get; set; }
        public string FSRNumber { get; set; }
        public string TSIRNumber { get; set; }
        public DateTime? RestoreDate { get; set; }
        public PDMS_WarrantyInvoiceItem InvoiceItem { get; set; }
        public List<PDMS_WarrantyInvoiceItem> InvoiceItems { get; set; }
        public int? DaysSinceClaimCreation { get; set; }

        public string OrderType = "ZDIP";
        public string SalesOrg = "AJF";
        public string DisChannel = "DS";
        public string Division = "SP";
        public string Location { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string Application { get; set; }
        //public string PricingDate { get; set; }

        public string PaymentTerm = "C000";
        public string IncoTerms = "EXW";
        public string OrderReason = "215";
        public string Plant { get; set; }
        public string Partner { get; set; }
        public string AcInvoiceNumber { get; set; }
        public DateTime? AcInvoiceDate { get; set; }
        public string AnnexureNumber { get; set; }
        public DateTime? AnnexureDate { get; set; }
        //  public string PricingDate { get; set; }
        //  public string InvoiceNumberNew { get; set; }

        public Boolean? DeviatedIsApproved { get; set; }
        public Boolean? DeviatedIsRejected { get; set; }
    }

    [Serializable]
    public class PDMS_WarrantyInvoiceHeader
    {
        public long WarrantyInvoiceHeaderID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }

        public string ICTicketID { get; set; } 
        public DateTime? ICTicketDate { get; set; }

        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }

        public PDMS_ICTicket ICTicket { get; set; }


        public PUser Approved1By { get; set; }
        public DateTime? Approved1On { get; set; }
        public PUser Approved2By { get; set; }
        public DateTime? Approved2On { get; set; }

        public PUser Approved3By { get; set; }
        public DateTime? Approved3On { get; set; }


        public int? HMR { get; set; }
        public Boolean? MarginWarranty { get; set; } 
        public string MachineSerialNumber { get; set; }
        public string Model { get; set; }
        public string InvoiceStatus { get; set; }
        public string ClaimStatus { get; set; }
        public string PscID { get; set; }
        public string ReasonForFailure { get; set; }
        public DateTime? DateOfCommissioning { get; set; }
        public string FSRNumber { get; set; }
        public string TSIRNumber { get; set; }
        public DateTime? RestoreDate { get; set; }
        public PDMS_WarrantyInvoiceItem InvoiceItem { get; set; }
        public List<PDMS_WarrantyInvoiceItem> InvoiceItems { get; set; }
        public int? DaysSinceClaimCreation { get; set; }

        public string OrderType = "ZDIP";
        public string SalesOrg = "AJF";
        public string DisChannel = "DS";
        public string Division = "SP";
        public string Location { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string Application { get; set; }
        //public string PricingDate { get; set; }

        public string PaymentTerm = "C000";
        public string IncoTerms = "EXW";
        public string OrderReason = "215";
        public string Plant { get; set; }
        public string Partner { get; set; }
        public string AcInvoiceNumber { get; set; }
        public DateTime? AcInvoiceDate { get; set; }
        public string AnnexureNumber { get; set; }
        public DateTime? AnnexureDate { get; set; }
        //  public string PricingDate { get; set; }
        //  public string InvoiceNumberNew { get; set; }

        public Boolean? DeviatedIsApproved { get; set; }
        public Boolean? DeviatedIsRejected { get; set; }
    }
    [Serializable]
    public class PDMS_WarrantyInvoiceItem
    {
        public long WarrantyInvoiceItemID { get; set; }
        public long WarrantyInvoiceHeaderID { get; set; }
        public string DeliveryNumber { get; set; } 
        public string Item { get; set; }
        public string RefDocID { get; set; }
        public string Material { get; set; }
        public string MaterialGroup { get; set; }
        public string MaterialDesc { get; set; }

        public decimal? Qty { get; set; }
        public decimal? Per { get; set; }
        public string UnitOM { get; set; }
        public decimal? Amount { get; set; }
        public decimal? BaseTax { get; set; }
        public string MaterialStatus { get; set; }
        public string MaterialStatusRemarks1 { get; set; }
        public string MaterialStatusRemarks2 { get; set; }
        public decimal? Approved1Amount { get; set; }
        public string Approved1Remarks { get; set; }
        public decimal? Approved2Amount { get; set; }
        public string Approved2Remarks { get; set; }

        public decimal? Approved3Amount { get; set; }
        public string Approved3Remarks { get; set; }

        public string Category { get; set; }
        public string HSNCode { get; set; }
        public string BriefDescriptionOfJob { get; set; }
        public decimal? TaxPercentage { get; set; }
      

        public string SAPDoc { get; set; }
        public DateTime? SAPPostingDate { get; set; }
        public decimal? SAPInvoiceValue { get; set; }
        public string SAPClearingDocument { get; set; }
        public string AnnexureNumber { get; set; }
        public PDMS_WarrantyMaterialReturnStatus WarrantyMaterialReturnStatus { get; set; }
        public PDMS_WarrantyFailureMaterial FailureMaterial { get; set; }
        public string TSIRNumber { get; set; }
        public string TsirID { get; set; }
    }

    [Serializable]
    public class PDMS_WarrantyMaterialReturnStatus
    {
        public int WarrantyMaterialReturnStatusID { get; set; }
        public string WarrantyMaterialReturnStatus { get; set; } 
    }

    [Serializable]
    public class PWarrantyClaim_Filter
    {
        public string ICTicketNumber { get; set; }
        public DateTime? ICTicketDateF { get; set; }
        public DateTime? ICTicketDateT { get; set; }
        public string ClaimNumber { get; set; }
        public DateTime? ClaimDateF { get; set; }
        public DateTime? ClaimDateT { get; set; }
        public int? DealerID { get; set; }
        public int? StatusID { get; set; }
        public DateTime? ApprovedDateF { get; set; }
        public DateTime? ApprovedDateT { get; set; }
        public string TSIRNumber { get; set; }
        public string CustomerCode { get; set; }
        public string MachineSerialNumber { get; set; }
        public Boolean IsAbove50K { get; set; }
    }
    [Serializable]
    public class PClaimDeviation
    {
        public long ClaimDeviationID { get; set; }
        public long WarrantyInvoiceHeaderID { get; set; } 
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; } 
        public string ICTicketID { get; set; }
        public DateTime? ICTicketDate { get; set; }

        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }

        public int? HMR { get; set; }
        public Boolean? MarginWarranty { get; set; }
        public string MachineSerialNumber { get; set; }
        public string Model { get; set; }
        public string ClaimStatus { get; set; }
        public DateTime? RestoreDate { get; set; }
        public List<PDMS_WarrantyInvoiceItem> InvoiceItems { get; set; }

        public Boolean? DeviatedIsApproved { get; set; }
        public Boolean? DeviatedIsRejected { get; set; }
    }
}
