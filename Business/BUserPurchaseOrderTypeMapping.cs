using DataAccess;
using Newtonsoft.Json;

namespace Business
{
    public class BUserPurchaseOrderTypeMapping
    {
        private IDataAccess provider;
        public BUserPurchaseOrderTypeMapping()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public PApiResult GetUserPurchaseOrderTypeMapping(int? DealerID, int? PurchaseOrderTypeID, int? DepartmentID, int? DesignationID, bool? IsActive, int? PageIndex, int? PageSize)
        {
            string endPoint = "UserPurchaseOrderTypeMapping/GetUserPurchaseOrderTypeMapping?DealerID=" + DealerID + "&PurchaseOrderTypeID=" + PurchaseOrderTypeID + "&DepartmentID=" + DepartmentID + "&DesignationID=" + DesignationID + "&IsActive=" + IsActive + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetUserPurchaseOrderTypeMappingReport(int? DealerID, int? DealerEmployeeID, int? PurchaseOrderTypeID, int? DepartmentID, int? DesignationID, int? PageIndex, int? PageSize)
        {
            string endPoint = "UserPurchaseOrderTypeMapping/GetUserPurchaseOrderTypeMappingReport?DealerID=" + DealerID + "&DealerEmployeeID=" + DealerEmployeeID + "&PurchaseOrderTypeID=" + PurchaseOrderTypeID + "&DepartmentID=" + DepartmentID + "&DesignationID=" + DesignationID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetUserPurchaseOrderTypeMappingReport_Excel(int? DealerID, int? DealerEmployeeID, int? PurchaseOrderTypeID, int? DepartmentID, int? DesignationID)
        {
            string endPoint = "UserPurchaseOrderTypeMapping/GetUserPurchaseOrderTypeMappingReport_Excel?DealerID=" + DealerID + "&DealerEmployeeID=" + DealerEmployeeID + "&PurchaseOrderTypeID=" + PurchaseOrderTypeID + "&DepartmentID=" + DepartmentID + "&DesignationID=" + DesignationID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
}
