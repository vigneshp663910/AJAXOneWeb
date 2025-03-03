using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class POnboardEmployee_Insert
    {
        public int OnboardEmployeeID { get; set; }
        public string EmpCode { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public DateTime? DOB { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string EmailID { get; set; }
        public int? EducationalQualificationID { get; set; }
        public decimal? TotalExperience { get; set; }
        public string Address { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int? TehsilID { get; set; }
        public string Village { get; set; }
        public string Location { get; set; }
        public string EmergencyContactNumber { get; set; }
        public int? BloodGroupID { get; set; }
        public int DealerID { get; set; }
        public int OfficeCodeID { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public int DealerDepartmentID { get; set; }
        public int DealerDesignationID { get; set; }
        public int ReportingTo { get; set; }
        public string ModulePermission { get; set; }
        public string ApprovedRemark { get; set; }
        public bool? IsApproved { get; set; }
        public int StatusId { get; set; }
    }
    [Serializable]
    public class POnboardEmployee
    {
        public int OnboardEmployeeID { get; set; }
        public string EmpCode { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public DateTime? DOB { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string EmailID { get; set; }
        public PDMS_EqucationalQualification EducationalQualification { get; set; }
        public decimal? TotalExperience { get; set; }
        public string Address { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_District District { get; set; }
        public PDMS_Tehsil Tehsil { get; set; }
        public string Village { get; set; }
        public string Location { get; set; }
        public string EmergencyContactNumber { get; set; }
        public PDMS_BloodGroup BloodGroup { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_DealerOffice DealerOffice { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public PDMS_DealerDepartment DealerDepartment { get; set; }
        public PDMS_DealerDesignation DealerDesignation { get; set; }
        public PDMS_DealerEmployee ReportingTo { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public PUser Approver { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public bool? IsApproved { get; set; }
        public string ModulePermission { get; set; }
        public string ApprovedRemark { get; set; }
        public POnboardEmployeeStatus Status { get; set; }
        public List<PDealer> DealerList { get; set; }
        public PUser User { get; set; }
    }
    [Serializable]
    public class POnboardEmployeeStatus
    {
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
    [Serializable]
    public class POnboardEmployeeDealer_Insert
    {
        public int OnboardEmployeeID { get; set; }
        public int DealerID { get; set; }
        public bool IsActive { get; set; }
    }
    [Serializable]
    public class PAjaxEmployee_Insert
    {
        public int AjaxEmployeeID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public DateTime? DOB { get; set; }
        public string ContactNumber { get; set; }
        public string ContactNumber1 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int? TehsilID { get; set; }
        public string Village { get; set; }
        public string Location { get; set; }
        public string AadhaarCardNo { get; set; }
        public int? EducationalQualificationID { get; set; }
        public decimal? TotalExperience { get; set; }        
        public int OfficeCodeID { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public int DealerDepartmentID { get; set; }
        public int DealerDesignationID { get; set; }
        public int ReportingTo { get; set; }
        public string EmergencyContactNumber { get; set; }
        public int? BloodGroupID { get; set; }
        public string ModulePermission { get; set; }
        public string ApprovedRemark { get; set; }
        public int StatusId { get; set; }
    }
    [Serializable]
    public class PAjaxEmployee
    {
        public int AjaxEmployeeID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public DateTime? DOB { get; set; }
        public string ContactNumber { get; set; }
        public string ContactNumber1 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_District District { get; set; }
        public PDMS_Tehsil Tehsil { get; set; }
        public string Village { get; set; }
        public string Location { get; set; }
        public string AadhaarCardNo { get; set; }
        public PDMS_EqucationalQualification EqucationalQualification { get; set; }
        public decimal? TotalExperience { get; set; }
        public PDMS_DealerEmployeeRole DealerEmployeeRole { get; set; }
        public string EmergencyContactNumber { get; set; }
        public PDMS_BloodGroup BloodGroup { get; set; }
        public string ModulePermission { get; set; }
        public string ApprovedRemark { get; set; }
        public List<PDealer> DealerPermissionList { get; set; }
    }
}
