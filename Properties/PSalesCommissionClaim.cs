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
}
