using DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BChangeLogs
    {
        private IDataAccess provider;
        public BChangeLogs()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public DataSet GetDealerChangeLogs(string DealerCode, string DateFrom, string DateTo)
        {
            string endPoint = "ChangeLogs/GetDealerChangeLogs?DealerCode=" + DealerCode + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo;
            return JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataSet GetCustomerChangeLogs(string CustomerCode, string DateFrom, string DateTo, int? DealerID)
        {
            string endPoint = "ChangeLogs/GetCustomerChangeLogs?CustomerCode=" + CustomerCode + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataSet GetLeadChangeLogs(string LeadNumber, string DateFrom, string DateTo, int? DealerID)
        {
            string endPoint = "ChangeLogs/GetLeadChangeLogs?LeadNumber=" + LeadNumber + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataSet GetEnquiryChangeLogs(string EnquiryNumber, string DateFrom, string DateTo, int? DealerID)
        {
            string endPoint = "ChangeLogs/GetEnquiryChangeLogs?EnquiryNumber=" + EnquiryNumber + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetSpcAssemblyChangeLogs(string AssemblyCode, int? DivisionID, int? ProductModelID, string DateFrom, string DateTo, bool Excel, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "ChangeLogs/GetSpcAssemblyChangeLogs?AssemblyCode=" + AssemblyCode + "&DivisionID=" + DivisionID + "&ProductModelID=" + ProductModelID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Excel=" + Excel + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetSpcAssemblyPartsCoOrdinateChangeLogs(string Material, int? DivisionID, int? ProductModelID, int? SpcAssemblyID, string DateFrom, string DateTo, bool Excel, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "ChangeLogs/GetSpcAssemblyPartsCoOrdinateChangeLogs?Material=" + Material + "&DivisionID=" + DivisionID + "&ProductModelID=" + ProductModelID + "&SpcAssemblyID=" + SpcAssemblyID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Excel=" + Excel + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetSpcModelChangeLogs(string SpcModelCode, string DateFrom, string DateTo, bool Excel, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "ChangeLogs/GetSpcModelChangeLogs?SpcModelCode=" + SpcModelCode + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Excel=" + Excel + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
}
