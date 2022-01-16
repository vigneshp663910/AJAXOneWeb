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
        public PLeadCategory Category { get; set; }
        public PLeadProgressStatus ProgressStatus { get; set; }
        public PLeadQualification Qualification { get; set; }
        public PLeadSource Source { get; set; }
        public PLeadStatus Status { get; set; }
        public PLeadType Type { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }

        public string Remarks { get; set; }
        //public string PersonName { get; set; }
        //public string PersonContactNumber { get; set; }
        //public string PersonMail { get; set; }
        //public string Address1 { get; set; }
        //public string Address2 { get; set; }
        //public  PDMS_Country Country { get; set; }
        //public PDMS_State State { get; set; }
        //public PDMS_District District { get; set; }
        //public PDMS_Tehsil Tehsil { get; set; }

        public PUser CreatedBy { get; set; }
        public string CreatedOn { get; set; }


        public PUser AssignedTo { get; set; }
        public List<PLeadSalesEngineer> SalesEngineer { get; set; }
        public List<PLeadConvocation> Convocation { get; set; }
        public PLeadFinancial Financial { get; set; }
        public List<PLeadFollowUp> FollowUp { get; set; }
    }
    [Serializable]
    public class PLeadSalesEngineer
    {
        public long LeadSalesEngineerID { get; set; }
        public long LeadID { get; set; }
        public PUser SalesEngineer { get; set; }
        public PUser AssignedBy { get; set; }
        public DateTime AssignedOn { get; set; }
        public Boolean IsActive { get; set; }
    }
    [Serializable]
    public class PLeadConvocation
    {
        public long LeadConvocationID { get; set; }
        public long LeadID { get; set; }
        public PLeadProgressStatus ProgressStatus { get; set; }
        public string Convocation { get; set; }
        public DateTime ConvocationDate { get; set; }
        public PUser SalesEngineer { get; set; }
        public PUser CreatedBy { get; set; }
    }
    [Serializable]
    public class PLeadFinancial
    {
        public long LeadFinancialID { get; set; }
        public long LeadID { get; set; }
        public PBankName BankName { get; set; }
        public decimal FinancePercentage { get; set; }
        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
    }
    [Serializable]
    public class PLeadFollowUp
    {
        public long LeadFollowUpID { get; set; }
        public long LeadID { get; set; }
        public string FollowUpNote { get; set; }
        public DateTime FollowUpDate { get; set; }
        public PUser SalesEngineer { get; set; }
        public PUser CreatedBy { get; set; }
    }
    [Serializable]
    public class PLeadEffort
    {
        public long LeadEffortID { get; set; }
        public long LeadID { get; set; }
        public PUser SalesEngineer { get; set; }
        public DateTime EffortDate { get; set; }
        public decimal EffortStartTime { get; set; }
        public decimal EffortEndTime { get; set; }
        public decimal Effort { get; set; }
        public PEffortType EffortType { get; set; }
        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
    }
    [Serializable]
    public class PLeadExpense
    {
        public long LeadExpenseID { get; set; }
        public long LeadID { get; set; }
        public PUser SalesEngineer { get; set; }
        public DateTime ExpenseDate { get; set; }
        public PExpenseType ExpenseType { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
    }

    public class PLeadProduct
    {
        public long LeadProductID { get; set; }
        public long LeadID { get; set; }
        public PProductType ProductType { get; set; }
        public PProduct Product { get; set; }
        public decimal Quantity { get; set; }
        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
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

    [Serializable]
    public class PLeadSearch
    {
        public long? LeadID { get; set; }
        public string LeadNumber { get; set; }
        public DateTime? LeadDateFrom { get; set; }
        public DateTime? LeadDateTo { get; set; }
        public int? StatusID { get; set; }
        public int? ProgressStatusID { get; set; }
        public int? CategoryID { get; set; }
        public int? QualificationID { get; set; }
        public int? SourceID { get; set; }
        public int? TypeID { get; set; }
        public int? DealerID { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public int? CountryID { get; set; }
        public int? StateID { get; set; }
        public string Name { get; set; }
    }
}
