using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BColdVisit
    {
        public List<PColdVisit> GetColdVisit(long? ColdVisitID, DateTime? ColdVisitDateFrom, DateTime? ColdVisitDateTo, long? CustomerID, string CustomerCode, string CustomerName, string Mobile, int? CountryID, int? StateID, int? DistrictID)
        {
            string endPoint = "ColdVisit?ColdVisitID=" + ColdVisitID + "&ColdVisitDateFrom=" + ColdVisitDateFrom + "&ColdVisitDateTo=" + ColdVisitDateTo + "&CustomerID=" + CustomerID
                + "&CustomerCode=" + CustomerCode + "&CustomerName=" + CustomerName + "&Mobile=" + Mobile + "&CountryID=" + CountryID + "&StateID=" + StateID + "&DistrictID=" + DistrictID;
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

    }
}
