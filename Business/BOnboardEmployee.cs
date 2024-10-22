using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Business
{
    public class BOnboardEmployee
    {
        private IDataAccess provider;
        public BOnboardEmployee()
        {
            provider = new ProviderFactory().GetProvider();
        }        
        public List<PDMS_State> GetState(int? DealerID, int? CountryID, int? RegionID, int? StateID, string State)
        {
            //string endPoint = "OnboardEmployee/State?DealerID=" + DealerID + "&CountryID=" + CountryID + "&RegionID=" + RegionID + "&StateID=" + StateID + "&State=" + State;
            //PApiResult ResultToken = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGetWithOutToken(endPoint));
            //return (List<PDMS_State>)ResultToken.Data;
            string endPoint = "OnboardEmployee/State?DealerID=" + DealerID + "&CountryID=" + CountryID + "&RegionID=" + RegionID + "&StateID=" + StateID + "&State=" + State;
            return JsonConvert.DeserializeObject<List<PDMS_State>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PDMS_District> GetDistrict(int? CountryID, int? RegionID, int? StateID, int? DistrictID, string District, int? DealerID)
        {
            string endPoint = "OnboardEmployee/District?CountryID=" + CountryID + "&RegionID=" + RegionID + "&StateID=" + StateID
                + "&DistrictID=" + DistrictID + "&District=" + District + "&DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<List<PDMS_District>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetOnboardEmployee(string EmpCode, string Name, int? DealerDepartmentID, int? DealerDesignationID, int? PageIndex, int? PageSize)
        {
            string endPoint = "OnboardEmployee/GetOnboardEmployee?EmpCode=" + EmpCode + "&Name=" + Name + "&DealerDepartmentID=" + DealerDepartmentID + "&DealerDesignationID=" + DealerDesignationID
                + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public POnboardEmployee GetOnboardEmployeeByID(int OnboardEmployeeID)
        {
            string endPoint = "OnboardEmployee/GetOnboardEmployeeByID?OnboardEmployeeID=" + OnboardEmployeeID;
            return JsonConvert.DeserializeObject<POnboardEmployee>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetOnboardEmployeePendingApproval(string EmpCode, string Name, int? DealerDepartmentID, int? DealerDesignationID, int? PageIndex, int? PageSize)
        {
            string endPoint = "OnboardEmployee/GetOnboardEmployeePendingApproval?EmpCode=" + EmpCode + "&Name=" + Name + "&DealerDepartmentID=" + DealerDepartmentID
                + "&DealerDesignationID=" + DealerDesignationID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public bool ApproveOnboardEmployee(int OnboardEmployeeID, string ModulePermission, string DealerPermission, string ApprovedRemark, bool IsApproved)
        {
            string endPoint = "OnboardEmployee/ApproveOnboardEmployee?OnboardEmployeeID=" + OnboardEmployeeID + "&ModulePermission=" + ModulePermission + "&DealerPermission=" + DealerPermission + "&ApprovedRemark=" + ApprovedRemark + "&IsApproved=" + IsApproved;
            return JsonConvert.DeserializeObject<bool>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}
