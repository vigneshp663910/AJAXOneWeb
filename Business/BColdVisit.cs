using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BColdVisit
    {
        private IDataAccess provider;
        private IDataAccess providerReport;
        public BColdVisit()
        {
            provider = new ProviderFactory().GetProvider();
            providerReport = new ProviderFactory().GetProvider(true);
        }
        public List<PColdVisit> GetColdVisit(long? ColdVisitID, DateTime? ColdVisitDateFrom, DateTime? ColdVisitDateTo, long? CustomerID, string CustomerCode, string CustomerName, string Mobile, int? CountryID, int? StateID, int? ReferenceTableID, long? ReferenceID, int? DealerID, int? SalesEngineerID,int? ActionTypeID)
        {
            string endPoint = "ColdVisit?ColdVisitID=" + ColdVisitID + "&ColdVisitDateFrom=" + ColdVisitDateFrom + "&ColdVisitDateTo=" + ColdVisitDateTo + "&CustomerID=" + CustomerID
                + "&CustomerCode=" + CustomerCode + "&CustomerName=" + CustomerName + "&Mobile=" + Mobile + "&CountryID=" + CountryID + "&StateID=" + StateID + "&ReferenceTableID=" + ReferenceTableID + "&ReferenceID=" + ReferenceID 
                + "&DealerID=" + DealerID + "&SalesEngineerID=" + SalesEngineerID + "&ActionTypeID=" + ActionTypeID;
            return JsonConvert.DeserializeObject<List<PColdVisit>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PLeadEffort> GetColdVisitEffort(long LeadID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "ColdVisit/Effort?LeadID=" + LeadID + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PLeadEffort>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }
        public List<PLeadExpense> GetColdVisitExpense(long LeadID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "ColdVisit/Expense?LeadID=" + LeadID + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PLeadExpense>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);

        }

        public List<PVisitTarget> GetVisitTarget(int? Year, int? Month,int? DealerID, int? DepartmentID, int? DealerEmployeeID, int? UserID,int? EmployeeUserId)
         {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "ColdVisit/VisitTarget?Year=" + Year + "&Month=" + Month + "&DealerID=" + DealerID + "&DepartmentID=" + DepartmentID + "&DealerEmployeeID=" + DealerEmployeeID + "&UserID=" + UserID+ "&EmployeeUserId="+ EmployeeUserId;
            return JsonConvert.DeserializeObject<List<PVisitTarget>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);
        }
        public List<PAttachedFile> GetAttachedFileColdVisit(long? ColdVisitID)
        {
            string endPoint = "ColdVisit/AttachedFile?ColdVisitID=" + ColdVisitID;
            return JsonConvert.DeserializeObject<List<PAttachedFile>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PAttachedFile GetAttachedFileColdVisitForDownload(string DocumentName)
        {
            string endPoint = "ColdVisit/AttachedFileForDownload?DocumentName=" + DocumentName;
            return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
        }
        public DataTable GetVisitReport(int? DealerID, int? DealerEmployeeID, string FromDate, string ToDate, int? UserID)
        {
            TraceLogger.Log(DateTime.Now);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter DealerEmployeeIDP = provider.CreateParameter("DealerEmployeeID", DealerEmployeeID, DbType.Int32);
            DbParameter LeadDateFromP = provider.CreateParameter("FromDate", FromDate, DbType.DateTime);
            DbParameter LeadDateToP = provider.CreateParameter("ToDate", ToDate, DbType.DateTime);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[5] { DealerIDP, DealerEmployeeIDP, LeadDateFromP, LeadDateToP, UserIDP };
            try
            {
                using (DataSet DataSet = provider.Select("GetVisitReport", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BColdVisit", "GetVisitReport", ex);
                throw;
            }
            return null;
            //  TraceLogger.Log(DateTime.Now);
        }
    }
}
