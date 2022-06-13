using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    public class PActivityType
    {
        public Int32 ActivityTypeID { get; set; }
        public string ActivityTypeCode { get; set; }
        public string ActivityTypeName { get; set; }
        public Boolean IsActive { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public PUser ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    } 
    [Serializable]
    public class PActivity
    {
        public long ActivityID { get; set; }
        public List<PLeadSalesEngineer> SalesEngineer { get; set; }
        public PActivityType ActivityType { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string Location { get; set; }
        public PDMS_Customer Customer { get; set; }
        public PDMS_Equipment Equipment { get; set; }
        public string Remark { get; set; }
        public decimal ActivityStartLatitude { get; set; }
        public decimal ActivityStartLongitude { get; set; }
        public decimal ActivityEndLatitude { get; set; }
        public decimal ActivityEndLongitude { get; set; }
        public decimal Amount { get; set; }
        public PUser CreatedBy { get; set; }
        public string DateTime { get; set; }
        public PUser ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public PActivityReference ActivityReference { get; set; }

    }
    [Serializable]
    public class PActivityReference
    {
        public long ActivityReferenceTableID { get; set; }
        public string ReferenceTable { get; set; }
    }
    [Serializable]
    public class PActivityReferenceType
    {
        public int ActivityReferenceTypeID { get; set; }
        public string ActivityReferenceType { get; set; }
        public Boolean IsActive { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public PUser ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

