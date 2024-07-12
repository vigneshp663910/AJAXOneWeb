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
   public class BPreSale
    {
        public List<PActionType> GetActionType(int? ActionTypeID, string ActionType)
        {
            string endPoint = "PreSale/ActionType?ActionTypeID=" + ActionTypeID + "&ActionType=" + ActionType;
            return JsonConvert.DeserializeObject<List<PActionType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetPreSalesSummeryReport(int? CountryID, int? RegionID, int? StateID, int? DealerID, string DateFrom, string DateTo)
        {
            string endPoint = "PreSale/GetPreSalesSummeryReport?CountryID=" + CountryID + "&RegionID=" + RegionID + "&StateID=" + StateID
                + "&DealerID=" + DealerID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));

        }
        public PApiResult GetVisitCoverageReport(int? CountryID, int? RegionID, int? StateID, int? DealerID,string LeadDateFrom, string LeadDateTo, string VisitDateFrom, string VisitDateTo)
        {
            string endPoint = "PreSale/GetVisitCoverageReport?CountryID=" + CountryID + "&RegionID=" + RegionID + "&StateID=" + StateID
                + "&DealerID=" + DealerID + "&LeadDateFrom=" + LeadDateFrom + "&LeadDateTo=" + LeadDateTo + "&VisitDateFrom=" + VisitDateFrom + "&VisitDateTo=" + VisitDateTo;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));

        }
        public List<PPreSalesMasterItem> GetPreSalesMasterItem(int MasterHeaderID)
        {
            string endPoint = "PreSale/GetPreSalesMasterItem?MasterHeaderID=" + MasterHeaderID;
            return JsonConvert.DeserializeObject<List<PPreSalesMasterItem>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}
