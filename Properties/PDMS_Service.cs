using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    public class PDMS_Service
    {
    }
    [Serializable]
    public class PDMS_Category1
    {
        public int Category1ID { get; set; }
        public string Category1 { get; set; }
        public string Category1Code { get; set; }
        public string Category1_Category1Code { get; set; }
    }
    [Serializable]
    public class PDMS_Category2
    {
        public int Category1ID { get; set; }
        public int Category2ID { get; set; }
        public string Category2 { get; set; }
        public string Category2Code { get; set; }
        public string Category2_Category2Code { get; set; }
    }
    [Serializable]
    public class PDMS_Category3
    {
        public int Category2ID { get; set; }
        public int Category3ID { get; set; }
        public string Category3 { get; set; }
        public string Category3Code { get; set; }
        public string Category3_Category3Code { get; set; }
    }
    [Serializable]
    public class PDMS_Category4
    {
        public int Category3ID { get; set; }
        public int Category4ID { get; set; }
        public string Category4 { get; set; }
        public string Category4Code { get; set; }
        public string Category4_Category4Code { get; set; }
    }
    [Serializable]
    public class PDMS_Category5
    {
        public int Category4ID { get; set; }
        public int Category5ID { get; set; }
        public string Category5 { get; set; }
        public string Category5Code { get; set; }
        public string Category5_Category5Code { get; set; }
    }
    [Serializable]
    public class PDMS_MainApplication
    {
        public int MainApplicationID { get; set; }
        public string MainApplication { get; set; }
        public bool IsActive { get; set; }        
    }
    [Serializable]
    public class PDMS_SubApplication
    {
        public int SubApplicationID { get; set; }
        public int MainApplicationID { get; set; }
        public string SubApplication { get; set; }
        public PDMS_MainApplication MainApplication { get; set; }
    }
    [Serializable]
    public class PDMS_ServiceCharge
    {
        public long ServiceChargeID { get; set; }
        public long ICTicketID { get; set; }
        public int Item { get; set; }
        public PDMS_Material Material { get; set; }
        public PDMS_ICTicketTSIR TSIR { get; set; }
        public DateTime? Date { get; set; }
        public decimal WorkedHours { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Discount { get; set; }
        public int SGST { get; set; }
        public int IGST { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }
        public decimal Total { get; set; }
        public Boolean IsClaimOrInvRequested { get; set; }
        public Boolean IsClaimOrInvRequested_N
        {
            get
            {
                if (IsClaimOrInvRequested == true)
                {
                    return false;
                }
                return true;
            }
        }
        public string ClaimNumber { get; set; }
        public string QuotationNumber { get; set; }
        public string ProformaInvoiceNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public Boolean IsDeleted { get; set; }

        public int? CountOverall { get; set; }
        public int? CountBasedMC { get; set; }
    }
    [Serializable]
    public class PDMS_ServiceMaterial
    {
        public long ServiceMaterialID { get; set; }
        public long ICTicketID { get; set; }
        public PDMS_ICTicket ICTicket { get; set; }
        public int Item { get; set; }
        public PDMS_Material Material { get; set; }
        public PDMS_Material DefectiveMaterial { get; set; }
        public PDMS_MaterialSource MaterialSource { get; set; }
        public int Qty { get; set; }
        public int AvailableQty { get; set; }
     //   public Boolean IsCustomerStock { get; set; }
        public Boolean IsFaultyPart { get; set; }
        public string ReceivingStatus { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxablePrice { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }
        public Boolean IsClaimOrQtnRequested { get; set; }
        public Boolean IsClaimOrQtnRequested_N
        {
            get
            {
                if (IsClaimOrQtnRequested == true)
                {
                    return false;
                }
                return true;
            }
        }
        public string QuotationNumber { get; set; }
        public DateTime? QuotationDate { get; set; }
        public string DeliveryNumber { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string SaleOrderNumber { get; set; }
        public string ClaimNumber { get; set; }
        public Boolean IsDeleted { get; set; }
        public string PONumber { get; set; }
        public PDMS_ICTicketTSIR TSIR { get; set; }
        public Boolean IsRecomenedParts { get; set; }
        public Boolean IsQuotationParts { get; set; }
        public decimal AvailablePercentage { get; set; }

        public string WarrantyMaterialReturnStatus { get; set; }
        public string OldInvoice { get; set; }
        
    }
    [Serializable]
    public class PDMS_NoteType
    {
        public int NoteTypeID { get; set; }
        public string NoteType { get; set; }
        public string NoteCode { get; set; }
    }
    [Serializable]
    public class PDMS_ServiceNote
    {
        public long ServiceNoteID { get; set; }
        public long ICTicketID { get; set; }
        public PDMS_NoteType NoteType { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedOn { get; set; }
        public PUser CreatedBy { get; set; }
    }
    [Serializable]
    public class PDMS_ServiceTechnician
    {
        public long ServiceTechnicianID { get; set; }
        public int UserID { get; set; }
        public String UserName { get; set; }
        public String ContactName { get; set; }
        public string UserName_ContactName { get; set; }
        public List<PDMS_ServiceTechnicianWorkedDate> ServiceTechnicianWorkedDate { get; set; }

        public DateTime AssignedOn { get; set; }
        public PUser AssignedBy { get; set; }
    }
    [Serializable]
    public class PDMS_CustomerSatisfactionLevel
    {
        public int CustomerSatisfactionLevelID { get; set; }
        public string CustomerSatisfactionLevel { get; set; }
    }
    [Serializable]
    public class PDMS_ServiceTechnicianWorkedDate
    {
        public long ServiceTechnicianWorkDateID { get; set; }
        public string UserName_ContactName { get; set; }
        public int UserID { get; set; }
        public DateTime WorkedDate { get; set; }
        public Decimal WorkedHours { get; set; }
    }
    [Serializable]
    public class PDMS_MaterialSource
    {
        public int MaterialSourceID { get; set; }
        public string MaterialSource { get; set; }
    }
}
