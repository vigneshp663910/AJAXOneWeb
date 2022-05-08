using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Business
{
    public class BLead
    {
        public List<PLeadCategory> GetLeadCategory(int? LeadCategoryID, string LeadCategory)
        {
            string endPoint = "Lead/GetLeadCategory?LeadCategoryID=" + LeadCategoryID + "&LeadCategory=" + LeadCategory;
            return JsonConvert.DeserializeObject<List<PLeadCategory>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PLeadProgressStatus> GetLeadProgressStatus(int? ProgressStatusID, string ProgressStatus)
        {
            string endPoint = "Lead/GetLeadProgressStatus?ProgressStatusID=" + ProgressStatusID + "&ProgressStatus=" + ProgressStatus;
            return JsonConvert.DeserializeObject<List<PLeadProgressStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PLeadQualification> GetLeadQualification(int? QualificationID, string Qualification)
        {
            string endPoint = "Lead/GetLeadQualification?QualificationID=" + QualificationID + "&Qualification=" + Qualification;
            return JsonConvert.DeserializeObject<List<PLeadQualification>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PLeadSource> GetLeadSource(int? SourceID, string Source)
        {
            string endPoint = "Lead/GetLeadSource?SourceID=" + SourceID + "&Source=" + Source;
            return JsonConvert.DeserializeObject<List<PLeadSource>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PLeadStatus> GetLeadStatus(int? StatusID, string Status)
        {
            string endPoint = "Lead/GetLeadStatus?StatusID=" + StatusID + "&Status=" + Status;
            return JsonConvert.DeserializeObject<List<PLeadStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PLeadType> GetLeadType(int? TypeID, string Type)
        {
            string endPoint = "Lead/GetLeadType?TypeID=" + TypeID + "&Type=" + Type;
            return JsonConvert.DeserializeObject<List<PLeadType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PLead> GetLead(PLeadSearch Lead)
        {
            string endPoint = "Lead?" + JsonConvert.SerializeObject(Lead);
            return JsonConvert.DeserializeObject<List<PLead>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/GetLead", Lead)).Data));
        }
        public PLead GetLeadByID(long LeadID)
        {
            string endPoint = "Lead/LeadByID?LeadID=" + LeadID;
            return JsonConvert.DeserializeObject<PLead>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PLeadSalesEngineer> GetLeadSalesEngineer(long LeadID, int UserID, Boolean? IsActive)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/SalesEngineer?LeadID=" + LeadID + "&UserID=" + UserID + "&IsActive=" + IsActive;
            return JsonConvert.DeserializeObject<List<PLeadSalesEngineer>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PLeadFollowUp> GetLeadFollowUpByID(long LeadID, long? LeadFollowUpID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/FollowUpByID?LeadID=" + LeadID + "&LeadFollowUpID=" + LeadFollowUpID;
            return JsonConvert.DeserializeObject<List<PLeadFollowUp>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PLeadFollowUp> GetLeadFollowUp(long? LeadID, int? SalesEngineerUserID, string From, string To,int? DealerID,string Customer)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/FollowUp?LeadID=" + LeadID + "&SalesEngineerUserID=" + SalesEngineerUserID
                + "&From=" + From + "&To=" + To + "&DealerID=" + DealerID + "&Customer=" + Customer;
            return JsonConvert.DeserializeObject<List<PLeadFollowUp>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PLeadConversation> GetLeadConversation(long LeadID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/Conversation?LeadID=" + LeadID + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List< PLeadConversation >> (JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PLeadFinancial> GetLeadFinancial(long LeadID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/Financial?LeadID=" + LeadID + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PLeadFinancial>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PLeadEffort> GetLeadEffort(long LeadID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/Effort?LeadID=" + LeadID + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PLeadEffort>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PLeadExpense> GetLeadExpense(long LeadID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/Expense?LeadID=" + LeadID + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PLeadExpense>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PLeadProduct> GetLeadProduct(long LeadID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/Product?LeadID=" + LeadID + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PLeadProduct>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }

        public List<PAttachedFile> GetAttachedFileLead(long? LeadID)
        {
            string endPoint = "Lead/AttachedFile?LeadID=" + LeadID;
            return JsonConvert.DeserializeObject<List<PAttachedFile>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PAttachedFile GetAttachedFileLeadForDownload(string DocumentName)
        {
            string endPoint = "Lead/AttachedFileForDownload?DocumentName=" + DocumentName;
            return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
        }
        public List<PLeadStatus> GetLeadCountByStatus(DateTime? From, DateTime? To, int? DealerID, int? UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/LeadCountByStatus?From=" + From + "&To=" + To + "&DealerID=" + DealerID + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PLeadStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PLeadQuestionariesMain> GetLeadQuestionariesMain(int? LeadQuestionariesMainID, string LeadQuestionariesMain)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/QuestionariesMain?LeadQuestionariesMainID=" + LeadQuestionariesMainID + "&LeadQuestionariesMain=" + LeadQuestionariesMain;
            return JsonConvert.DeserializeObject<List<PLeadQuestionariesMain>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PLeadQuestionariesSub> GetLeadQuestionariesSub(int? LeadQuestionariesSubID, int? LeadQuestionariesMainID, string LeadQuestionariesSub)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/QuestionariesSub?LeadQuestionariesSubID=" + LeadQuestionariesSubID + "&LeadQuestionariesMainID=" + LeadQuestionariesMainID + "&LeadQuestionariesSub=" + LeadQuestionariesSub;
            return JsonConvert.DeserializeObject<List<PLeadQuestionariesSub>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PLeadQuestionaries> GetLeadQuestionaries(long LeadID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/Questionaries?LeadID=" + LeadID;
            return JsonConvert.DeserializeObject<List<PLeadQuestionaries>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }

        public DataTable GetPreSaleReport(string Lead,string LeadDateFrom, string LeadDateTo,string Quotation, string QuotationDateFrom, string QuotationDateTo
            , string Invoice, string InvoiceDateFrom, string InvoiceDateTo, string CustomerCode, string CustomerName, int? CountryID, int? StateID, int? DealerID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/PreSaleReport?Lead=" + Lead + "&LeadDateFrom=" + LeadDateFrom + "&LeadDateTo=" + LeadDateTo + "&Quotation=" + Quotation
                 + "&QuotationDateFrom=" + QuotationDateFrom + "&QuotationDateTo=" + QuotationDateTo + "&Invoice=" + Invoice 
                 + "&InvoiceDateFrom=" + InvoiceDateFrom + "&InvoiceDateTo=" + InvoiceDateTo + "&CustomerCode=" + CustomerCode + "&CustomerName=" + CustomerName
                 + "&CountryID=" + CountryID + "&StateID=" + StateID + "&DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
    }
}
