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
    public class BDealerBusiness
    {
        public PApiResult GetDealerBalanceConfirmationReport(int? DealerID, int? BalanceConfirmationStatusID, string DateFrom, string DateTo, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "DealerBusiness/GetDealerBalanceConfirmationReport?DealerID=" + DealerID + "&BalanceConfirmationStatusID=" 
                + BalanceConfirmationStatusID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)) ;
        }
        public DataTable GetDealerBalanceConfirmationToUpdate(int? DealerID, int? BalanceConfirmationStatusID, string DateFrom, string DateTo)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "DealerBusiness/GetDealerBalanceConfirmationToUpdate?DealerID=" + DealerID + "&BalanceConfirmationStatusID=" + BalanceConfirmationStatusID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataTable GetDealerBalanceConfirmationReportExcel(int? DealerID, int? BalanceConfirmationStatusID, string DateFrom, string DateTo)
        {
            string endPoint = "DealerBusiness/GetDealerBalanceConfirmationReportExcel?DealerID=" + DealerID + "&BalanceConfirmationStatusID=" + BalanceConfirmationStatusID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public DataTable GetDealerBalanceConfirmationAttachment(long? DealerBalanceConfirmationID)
        {
            string endPoint = "DealerBusiness/GetDealerBalanceConfirmationAttachment?DealerBalanceConfirmationID=" + DealerBalanceConfirmationID;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PAttachedFile AttachmentsForDownload(long? AttachedFileID)
        {
            string endPoint = "DealerBusiness/AttachmentsForDownload?AttachedFileID=" + AttachedFileID;
            return JsonConvert.DeserializeObject<PAttachedFile>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult SendMailtoDealerSatament(string customer, string open_dt, string pos_dt_from, string email)
        {
            string endPoint = "DealerBusiness/SendMailtoDealerSatament?customer=" + customer + "&open_dt=" + open_dt + "&pos_dt_from=" + pos_dt_from + "&email=" + email;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
}
