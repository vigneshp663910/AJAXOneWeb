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
        public long Lead { get; set; }
        public string LeadNumber { get; set; }
        public string Name { get; set; }
        public PLeadCategory LeadCategory { get; set; }
        public PLeadProgressStatus LeadProgressStatus { get; set; }
        public PLeadQualification LeadQualification { get; set; }
        public PLeadSource LeadSource { get; set; }
        public PLeadStatus LeadStatus { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }
        public int CreatedBy { get; set; }
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
 
}
