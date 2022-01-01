using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{    
    [Serializable]
    public class PLead
    {
        public long LeadID { get; set; }
        public DateTime LeadDate { get; set; }

        public string LeadNumber { get; set; }
        public string Name { get; set; }
        public PLeadCategory Category { get; set; }
        public PLeadProgressStatus ProgressStatus { get; set; }
        public PLeadQualification Qualification { get; set; }
        public PLeadSource Source { get; set; }
        public PLeadStatus Status { get; set; }
        public PLeadType Type { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }

        public string Remarks { get; set; }
        public string PersonName { get; set; }
        public string PersonContactNumber { get; set; }
        public string PersonMail { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public PDMS_Country Country { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_District District { get; set; }
        public PDMS_Tehsil Tehsil { get; set; }

        public PUser CreatedBy { get; set; }
        public string CreatedOn { get; set; }
    }

    [Serializable]
    public class PLeadindiaMartJson
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

        public long ENQ_MESSAGE { get; set; }
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
    }

    [Serializable]
    public class PLeadCategory
    {
        public int CategoryID { get; set; }
        public string Category { get; set; }
    }

    [Serializable]
    public class PLeadProgressStatus
    {
        public int ProgressStatusID { get; set; }
        public string ProgressStatus { get; set; }
    }
    
    [Serializable]
    public class PLeadQualification
    {
        public int QualificationID { get; set; }
        public string Qualification { get; set; }
    }
    
    [Serializable]
    public class PLeadSource
    {
        public int SourceID { get; set; }
        public string Source { get; set; }
    }
    
    [Serializable]
    public class PLeadStatus
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }
    [Serializable]
    public class PLeadType
    {
        public int TypeID { get; set; }
        public string Type { get; set; }
    }
}
