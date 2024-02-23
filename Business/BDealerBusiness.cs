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


        public List<PDealerBusinessExcellence> GetDealerBusinessExcellenceToUpdate(int? Year, int? Month, int? DealerID, int? Category1ID, int? Category2ID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "DealerBusinessExcellence/GetDealerBusinessExcellenceToUpdate?Year=" + Year + "&Month=" + Month + "&DealerID=" + DealerID + "&Category1ID=" + Category1ID + "&Category2ID=" + Category2ID;
            return JsonConvert.DeserializeObject<List<PDealerBusinessExcellence>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            //  TraceLogger.Log(DateTime.Now);
        }
        public DataTable GetDealerBusinessExcellenceReport(int? Year, int? Month, int? DealerID, int? Category1ID)
        {
            string endPoint = "DealerBusinessExcellence/GetDealerBusinessExcellenceReport?Year=" + Year + "&Month=" + Month + "&DealerID=" + DealerID + "&Category1ID=" + Category1ID;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PDealerBusinessExcellenceCategory1> GetDealerBusinessExcellenceFunctionArea(int? Category1ID)
        {
            string endPoint = "DealerBusinessExcellence/GetDealerBusinessExcellenceFunctionArea?Category1ID=" + Category1ID;
            return JsonConvert.DeserializeObject<List<PDealerBusinessExcellenceCategory1>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PDealerBusinessExcellenceCategory2> GetDealerBusinessExcellenceFunctionSubArea(int? Category1ID, int? Category2ID)
        {
            string endPoint = "DealerBusinessExcellence/GetDealerBusinessExcellenceFunctionSubArea?Category1ID=" + Category1ID + "&Category2ID=" + Category2ID;
            return JsonConvert.DeserializeObject<List<PDealerBusinessExcellenceCategory2>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetDealerBusinessExcellence(int? Year, int? Month, int? DealerID, int? RegionID, int? StatusID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "DealerBusinessExcellence/GetDealerBusinessExcellence?Year=" + Year + "&Month=" + Month + "&DealerID=" + DealerID + "&RegionID=" + RegionID + "&StatusID=" + StatusID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            // return JsonConvert.DeserializeObject<List<PDealerBusinessExcellenceHeader>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PDealerBusinessExcellenceHeader GetDealerBusinessExcellenceByID(long DealerBusinessExcellenceID)
        {
            string endPoint = "DealerBusinessExcellence/GetDealerBusinessExcellenceByID?DealerBusinessExcellenceID=" + DealerBusinessExcellenceID;
            return JsonConvert.DeserializeObject<PDealerBusinessExcellenceHeader>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetDealerBusinessExcellenceQuarterWise(int Year, int? Quarter, int? Month, int? DealerID, int? RegionID)
        {
            string endPoint = "DealerBusinessExcellence/GetDealerBusinessExcellenceQuarterWise?Year=" + Year + "&Quarter=" + Quarter + "&Month=" + Month + "&DealerID=" + DealerID + "&RegionID=" + RegionID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
}
