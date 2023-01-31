using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
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
      //  public PProduct ProductList { get; set; }
        public string Product { get; set; }
        public string Remarks { get; set; }
        public string RejectReason { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? LeadID { get; set; }
        public PUser STM { get; set; }
    }

    [Serializable]

    public class PEnquiryIndiamart
    {
        public string RN { get; set; }
        public string QUERY_ID { get; set; }
        public string QTYPE { get; set; }
        public string SENDERNAME { get; set; }
        public string SENDEREMAIL { get; set; }
        public string MOB { get; set; }
        public string GLUSR_USR_COMPANYNAME { get; set; }
        public string ENQ_ADDRESS { get; set; }
        public string ENQ_CITY { get; set; }
        public string ENQ_STATE { get; set; }
        public string COUNTRY_ISO { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string ENQ_MESSAGE { get; set; }
        public string DATE_RE { get; set; }
        public string DATE_R { get; set; }
        public string DATE_TIME_RE { get; set; }
        public string LOG_TIME { get; set; }
        public string QUERY_MODID { get; set; }
        public string ENQ_CALL_DURATION { get; set; }
        public string ENQ_RECEIVER_MOB { get; set; }
        public string EMAIL_ALT { get; set; }
        public string MOBILE_ALT { get; set; }
        public string TOTAL_COUNT { get; set; }
        public int PreSaleStatusID { get; set; }
        public PUser RejectedBy { get; set; }
        public DateTime RejectedOn { get; set; }
        public string RejectedReason { get; set; }

    }

    [Serializable]
    public class PEnquiryRemark
    {
        public long EnquiryRemarkID { get; set; }
        public string Remark { get; set; }
    }
}
