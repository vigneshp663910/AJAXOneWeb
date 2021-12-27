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
