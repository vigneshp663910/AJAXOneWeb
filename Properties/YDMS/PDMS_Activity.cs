using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    
    public class PDMS_ActivityMaster
    {
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityCode { get; set; }
         


    }
    public class PDMS_ActivityInfo
    {
        public int ActivityID { get; set; }
        public int FunctionalAreaID { get; set; }
        public string FunctionalArea { get; set; }
        public int Unit { get; set; }
        public string UnitDesc { get; set; }
        public double Budget { get; set; }
        public double AjaxSharing { get; set; }
        public double DealerSharing { get; set; }
        public double GST { get; set; }
        public string SAC { get; set; }
        public string ActivityType { get; set; }

    }
     
    public class PDMS_FunctionalArea
    {
        public string FunctionalArea { get; set; }
        public int FunctionalAreaID { get; set; }
    }
    public class PDMS_CommonMaster
    {
        public string _Description { get; set; }
        public int _MasterID { get; set; }
    }
    public class PDMS_ActivityPlan
    {
        public int AP_PKPlanID { get; set; }
        public int AP_FKDealerID { get; set; }
        public int AP_FKActivityID { get; set; }
        public int AP_NoofUnits { get; set; }
        public double AP_BudgetPerUnit { get; set; }
        public double AP_ExpBudget { get; set; }
        public DateTime AP_FromDate { get; set; }
        public DateTime AP_ToDate { get; set; }
        public string AP_Location { get; set; }
        public string AP_Remarks { get; set; }
        public int AP_CreatedBy { get; set; }
        public double AP_AjaxSharing { get; set; }
        public double AI_AjaxSharing { get; set; }
        public string AP_Unit { get; set; }
    }
    public class PDMS_PlannedActivity
    {
        public int PkPlanID { get; set; }
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
    }
    public class PDMS_ActivityDocs
    {
        public int AD_FKActualID { get; set; }
        public int AD_Sno { get; set; }

        public string AD_Description { get; set; }
        public string AD_ContentType { get; set; }
        public string AD_FileName { get; set; }
        public byte[] AD_AttachedFile { get; set; }
        public long AD_FileSize { get; set; }

    }
    public class PDMS_DDLList
    {
        public string _Text { get; set; }
        public string _Value { get; set; }
        public PDMS_DDLList(string Text,string Value)
        {
            _Text = Text;
            _Value = Value;
        }
    }
}
