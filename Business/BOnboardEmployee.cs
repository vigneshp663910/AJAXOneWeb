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
            string endPoint = "OnboardEmployee/State?DealerID=" + DealerID + "&CountryID=" + CountryID + "&RegionID=" + RegionID + "&StateID=" + StateID + "&State=" + State;
            return JsonConvert.DeserializeObject<List<PDMS_State>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PDMS_District> GetDistrict(int? CountryID, int? RegionID, int? StateID, int? DistrictID, string District, int? DealerID)
        {
            string endPoint = "OnboardEmployee/District?CountryID=" + CountryID + "&RegionID=" + RegionID + "&StateID=" + StateID
                + "&DistrictID=" + DistrictID + "&District=" + District + "&DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<List<PDMS_District>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetOnboardEmployee(string EmpCode, string Name, int? DealerDepartmentID, int? DealerDesignationID, int? StatusId, int? PageIndex, int? PageSize)
        {
            string endPoint = "OnboardEmployee/GetOnboardEmployee?EmpCode=" + EmpCode + "&Name=" + Name + "&DealerDepartmentID=" + DealerDepartmentID + "&DealerDesignationID=" + DealerDesignationID + "&StatusId=" + StatusId
                + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public POnboardEmployee GetOnboardEmployeeByID(int OnboardEmployeeID)
        {
            string endPoint = "OnboardEmployee/GetOnboardEmployeeByID?OnboardEmployeeID=" + OnboardEmployeeID;
            return JsonConvert.DeserializeObject<POnboardEmployee>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetOnboardEmployeePendingApproval(string EmpCode, string Name, int? DealerDepartmentID, int? DealerDesignationID, int? StatusId, int? PageIndex, int? PageSize)
        {
            string endPoint = "OnboardEmployee/GetOnboardEmployeePendingApproval?EmpCode=" + EmpCode + "&Name=" + Name + "&DealerDepartmentID=" + DealerDepartmentID
                + "&DealerDesignationID=" + DealerDesignationID + "&StatusId=" + StatusId + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public void GetDealerEmployeeDDL(DropDownList ddl, int? DealerID, int? DepartmentID)
        {
            List<PDMS_DealerEmployee> Employee = new BDMS_Dealer().GetDealerEmployeeManage(DealerID, null, null, null, null, null, true, DepartmentID, null);
            ddl.DataValueField = "DealerEmployeeID";
            ddl.DataTextField = "Name";
            ddl.DataSource = Employee;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public List<POnboardEmployeeStatus> GetOnboardEmployeeStatus(int? StatusId, string Status)
        {
            string endPoint = "OnboardEmployee/GetOnboardEmployeeStatus?StatusId=" + StatusId + "&Status=" + Status;
            return JsonConvert.DeserializeObject<List<POnboardEmployeeStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PAjaxEmployee GetOnboardEmployeeByGenerateAjaxEmployee(int OnboardEmployeeID)
        {
            string endPoint = "OnboardEmployee/GetOnboardEmployeeByGenerateAjaxEmployee?OnboardEmployeeID=" + OnboardEmployeeID;
            return JsonConvert.DeserializeObject<PAjaxEmployee>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PAjaxEmployee GetDealerEmployeeByDealerEmployeeID(int DealerEmployeeID)
        {
            string endPoint = "OnboardEmployee/GetDealerEmployeeByDealerEmployeeID?DealerEmployeeID=" + DealerEmployeeID;
            return JsonConvert.DeserializeObject<PAjaxEmployee>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}
