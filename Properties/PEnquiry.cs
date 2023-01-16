using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    public class PEnquiry
    {
        public long EnquiryID { get; set; }
        public DateTime EnquiryDate { get; set; }
        public string EnquiryNumber { get; set; }
        public string CustomerName { get; set; }
        public string PersonName { get; set; }
        public string Mail { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public PLeadSource Source { get; set; }
        public PPreSaleStatus Status { get; set; }
        public PDMS_Country Country { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_District District { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PProductType ProductType { get; set; }
        public PProduct ProductList { get; set; }
        public string Product { get; set; }
        public string Remarks { get; set; }
        public string RejectReason { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LeadID { get; set; }
        public PUser STM { get; set; }
    }
}
