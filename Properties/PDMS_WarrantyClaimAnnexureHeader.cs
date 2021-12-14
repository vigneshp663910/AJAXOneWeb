using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_WarrantyClaimAnnexureHeader
    {
        public long WarrantyClaimAnnexureHeaderID { get; set; }
        public string AnnexureNumber { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Contact { get; set; }
        public string GSTIN { get; set; }

        public int MonthRange { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public PDMS_Customer Customer  { get; set; }
        public PDMS_WarrantyClaimAnnexureItem AnnexureItem { get; set; }
        public List<PDMS_WarrantyClaimAnnexureItem> AnnexureItems { get; set; }
    }
    [Serializable]
    public class PDMS_WarrantyClaimAnnexureItem
    {
        public long WarrantyClaimAnnexureItemID { get; set; }
        public long WarrantyClaimAnnexureHeaderID { get; set; }
        public int SLID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public PDMS_ICTicket ICTicket { get; set; }
        public string ICTicketID { get; set; }
        public DateTime? ICTicketDate { get; set; }
        public DateTime? RestoreDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Material { get; set; }
        public string MaterialDesc { get; set; }
        public string HSNCode { get; set; }
        public string HMR { get; set; }

        public string MachineSerialNumber { get; set; }
        public string Model { get; set; }

     
        public decimal ClaimAmount { get; set; }
        public decimal ApprovedAmount { get; set; }


        public string Item { get; set; }
        public decimal? Qty { get; set; }
        public string UnitOM { get; set; }

        public decimal? BaseTax { get; set; }

        public string Category { get; set; }
        public string BriefDescriptionOfJob { get; set; }
        public string DeliveryChallan { get; set; }

        public string SAPDoc { get; set; }
        public DateTime? SAPPostingDate { get; set; }
        public decimal? SAPInvoiceValue { get; set; }
        public decimal? TaxPercentage { get; set; }
    }
}
