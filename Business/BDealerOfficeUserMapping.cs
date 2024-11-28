using DataAccess;
using Newtonsoft.Json;

namespace Business
{
    public class BDealerOfficeUserMapping
    {
        private IDataAccess provider;
        public BDealerOfficeUserMapping()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public PApiResult GetDealerOfficeUserMapping(int? DealerID, int? OfficeID, int? DepartmentID, int? DesignationID, bool? IsActive, int? PageIndex, int? PageSize)
        {
            string endPoint = "DealerOfficeUserMapping/GetDealerOfficeUserMapping?DealerID=" + DealerID + "&OfficeID=" + OfficeID + "&DepartmentID=" + DepartmentID + "&DesignationID=" + DesignationID + "&IsActive=" + IsActive + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetDealerOfficeUserMappingReport(int? DealerID, int? DealerEmployeeID, int? OfficeID, int? DepartmentID, int? DesignationID, int? PageIndex, int? PageSize)
        {
            string endPoint = "DealerOfficeUserMapping/GetDealerOfficeUserMappingReport?DealerID=" + DealerID + "&DealerEmployeeID=" + DealerEmployeeID + "&OfficeID=" + OfficeID + "&DepartmentID=" + DepartmentID + "&DesignationID=" + DesignationID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetDealerOfficeUserMappingReport_Excel(int? DealerID, int? DealerEmployeeID, int? OfficeID, int? DepartmentID, int? DesignationID)
        {
            string endPoint = "DealerOfficeUserMapping/GetDealerOfficeUserMappingReport_Excel?DealerID=" + DealerID + "&DealerEmployeeID=" + DealerEmployeeID + "&OfficeID=" + OfficeID + "&DepartmentID=" + DepartmentID + "&DesignationID=" + DesignationID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
}
