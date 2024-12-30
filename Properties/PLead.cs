using System;
using System.Collections.Generic;
using System.Globalization;
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
        public PDMS_MainApplication Application { get; set; }
        public PLeadQualification Qualification { get; set; }
        public PLeadSource Source { get; set; }
        public DateTime? ExpectedDateOfSale { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
        public PLeadStatus Status { get; set; }
        public PProject Project { get; set; }
        //public PLeadUrgency Urgency { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }
        public string CustomerFeedback { get; set; }
        public string Remarks { get; set; }

        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }


        public PUser AssignedTo { get; set; }
        public List<PLeadSalesEngineer> SalesEngineer { get; set; }
        public List<PLeadConversation> Conversation { get; set; }
        public PLeadFinancial Financial { get; set; }
        public List<PLeadFollowUp> FollowUp { get; set; }
        public decimal TotalEffort { get; set; }
        public decimal TotalExpense { get; set; }
        public PProductType ProductType { get; set; }
        public List<PLeadProduct> LeadProduct { get; set; }
        public PPreSalesMasterItem SalesChannelType { get; set; }
    }
    [Serializable]
    public class PLead_Insert
    {
        public long LeadID { get; set; }
        public long? EnquiryID { get; set; } 
        public int ProductTypeID { get; set; }
        public int? SourceID { get; set; }
        public DateTime ExpectedDateOfSale { get; set; }
        public DateTime NextFollowUpDate { get; set; }
        public long? ProjectID { get; set; } 
        public int? MainApplicationID { get; set; }
        public string CustomerFeedback { get; set; }
        public string Remarks { get; set; }
        public PDMS_Customer_Insert Customer { get; set; }
        public int? SalesChannelTypeID { get; set; }
    }
    [Serializable]
    public class PLeadDeviation_Insert
    {
        public long CustomerID { get; set; }
        public int DealerID { get; set; }
        public int ProductTypeID { get; set; }
        public int? SourceID { get; set; }
        public DateTime ExpectedDateOfSale { get; set; }
        public DateTime NextFollowUpDate { get; set; } 
        public int? MainApplicationID { get; set; }
        public string CustomerFeedback { get; set; }
        public string Remarks { get; set; }
        public int? SalesChannelTypeID { get; set; }
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
        public string Active
        {
            get
            {
                return IsActive ? "Active" : "Not Active";
            }
        }
    }
    [Serializable]
    public class PLeadConversation
    {
        public long LeadConversationID { get; set; }
        public long LeadID { get; set; }
       // public PLeadProgressStatus ProgressStatus { get; set; }
        public string Conversation { get; set; }
        public DateTime ConversationDate { get; set; }
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
        public PLead Lead { get; set; }
        public PPreSaleStatus Status { get; set; }
        public PDMS_Customer Customer { get; set; }
        public string Remark { get; set; }
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


    [Serializable]
    public class PLeadProduct
    {
        public long LeadProductID { get; set; }
        public long LeadID { get; set; }
        public PProductType ProductType { get; set; }
        public PProduct Product { get; set; }
        public decimal Quantity { get; set; }
        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
        public PSalesQuotation SalesQuotation { get; set; }
        
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
        public int Count { get; set; }
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
        public int? ProductTypeID { get; set; } 
        public int? QualificationID { get; set; }
        public int? SourceID { get; set; } 
        public int? DealerID { get; set; }
        public long? SalesEngineerID { get; set; }
        public long? CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public int? CountryID { get; set; }
        public int? RegionID { get; set; }
        public int? StateID { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
    [Serializable]
    public class PLeadQuestionaries
    {
        public long LeadQuestionariesID { get; set; }
        public long LeadID { get; set; }
        public PLeadQuestionariesMain QuestionariesMain { get; set; }
        public PLeadQuestionariesSub QuestionariesSub { get; set; }
        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
    }
    [Serializable]
    public class PLeadQuestionariesMain
    {
        public int LeadQuestionariesMainID { get; set; }
        public string LeadQuestionariesMain { get; set; }
    }
    [Serializable]
    public class PLeadQuestionariesSub
    {
        public int LeadQuestionariesSubID { get; set; }
        public PLeadQuestionariesMain QuestionariesMain { get; set; }
        public string LeadQuestionariesSub { get; set; }
    }

    [Serializable]
    public class PLeadUrgency
    {
        public int UrgencyID { get; set; }
        public string Urgency { get; set; }
    }


    public class PDealerMissionPlanning
    {
        public long DealerMissionPlanningID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName
        {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month).Substring(0, 3);
            }
        }
        public PDMS_Dealer Dealer { get; set; }
        public PProductType ProductType { get; set; }
        public int? BillingPlan { get; set; }
        public int? BillingRevenuePlan { get; set; }
        public int? RetailPlan { get; set; }
        public int? LeadPlan { get; set; }
        public int? LeadConversionPlan { get; set; }
        public int? QuotationPlan { get; set; }
        public int? QuotationConversionPlan { get; set; }
        public int? PartsQuotationPlan { get; set; }
        public int? PartsQuotationConversionPlan { get; set; }
        public int? PartsRetailPlan { get; set; }
        public int? PartsBillingPlan { get; set; }

        //public int BillingActual { get; set; }
        //public int BillingRevenueActual { get; set; }
        //public int RetailActual { get; set; }
        //public int LeadActual { get; set; }
        //public int QuotationActual { get; set; }
        //public int PartsQuotationActual { get; set; }
        //public int PartsRetailActual { get; set; }
        //public int PartsBillingActual { get; set; }

        public PUser CreatedBy { get; set; }
    }


   
}
