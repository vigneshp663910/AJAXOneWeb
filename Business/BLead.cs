using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
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
            string endPoint = "Lead?"+ JsonConvert.SerializeObject(Lead);
            return JsonConvert.DeserializeObject<List<PLead>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/GetLead", Lead)).Data));
        }

        public List<PLeadSalesEngineer> GetLeadSalesEngineer(long LeadID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/SalesEngineer?LeadID=" + LeadID + "&UserID=" + UserID;
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
        public List<PLeadFollowUp> GetLeadFollowUp(long? LeadID, int? SalesEngineerUserID, DateTime? From, DateTime? To, int? UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/FollowUp?LeadID=" + LeadID + "&SalesEngineerUserID=" + SalesEngineerUserID 
                + "&From=" + From + "&To=" + To + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PLeadFollowUp>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PLeadConvocation> GetLeadConvocation(long LeadID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Lead/Convocation?LeadID=" + LeadID + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PLeadConvocation>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
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
    }
}
