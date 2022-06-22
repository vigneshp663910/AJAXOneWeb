using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    public class PActivityType
    {
        public int ActivityTypeID { get; set; }
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
        public PUser SalesEngineer { get; set; }
        public PActivityType ActivityType { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string Location { get; set; }
        public PDMS_Customer Customer { get; set; }
        public PDMS_EquipmentHeader Equipment { get; set; }
        public string Remark { get; set; }
        public decimal ActivityStartLatitude { get; set; }
        public decimal ActivityStartLongitude { get; set; }
        public decimal ActivityEndLatitude { get; set; }
        public decimal ActivityEndLongitude { get; set; }
        public decimal? Amount { get; set; }
        public PUser CreatedBy { get; set; }
        public string DateTime { get; set; }
        public PUser ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public PActivityReferenceType ActivityReference { get; set; }
        public string ReferenceNumber { get; set; }
        public long? ReferenceNumberID { get; set; }
    }   

    [Serializable]
    public class PActivityReferenceType
    {
        public int  ActivityReferenceTableID { get; set; }
        public string ReferenceTable { get; set; }
        public Boolean IsActive { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public PUser ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    [Serializable]
    public class PActivitySearch
    {
        public long? ActivityID { get; set; }
        public int? ActivityTypeID { get; set; }
        public DateTime? ActivityDateFrom { get; set; }
        public DateTime? ActivityDateTo{ get; set; }
        public string CustomerCode { get; set; }
        public string EquipmentSerialNo { get; set; }
        public int? ActivityReferenceTableID { get; set; }
        public string ReferenceNumber { get; set; }
    }
}

