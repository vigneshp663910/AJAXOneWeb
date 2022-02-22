using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
     [Serializable]
    public class PUser
    {
        #region Properties
        public Int32 Status { get; set; }
        public int UserID { get; set; }
        public int DealerEmployeeID { get; set; }
        public PDMS_DealerEmployee Employee { get; set; }
        public PDMS_DealerDepartment Department { get; set; }
        public PDMS_DealerDesignation Designation { get; set; }
        public PDMS_DealerEmployee ReportingTo { get; set; }
        public Int32 PlantID { get; set; } 
        public String UserName { get; set; }
        public String PassWord { get; set; }
        public Boolean IsLocked { get; set; }
        public Int16 UserTypeID { get; set; }
        public Boolean IsEnabled { get; set; }
        public String ContactName { get; set; } 

        public Boolean IsFirstTimeLogin { get; set; }
        public String ExternalReferenceID { get; set; }
        public DateTime PasswordExpiryDate { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public PContactDetail ContactDetail { get; set; }
         public List<PUserModuleAccess> AccessModules { get; set; }
        public List<PModuleAccess> DMSModules { get; set; }
        public List<PSubModuleChile> SubModuleChile { get; set; }
        public PUserType UserType { get; set; }
        public string EnableDisableReason { get; set; }
        public bool IsUpdate { get; set; }
        public List<PPlant> UserPlants { get; set; }
        public bool IsNatesanGroupUser { get; set; }
        public string Mail { get; set; }
        public string ContactNumber { get; set; }
        public Boolean L1Support { get; set; }
        public Int16 SystemCategoryID { get; set; }
        public List<PDealer> Dealer { get; set; }
        public Boolean IsTechnician { get; set; }
        public List<PDashboard> Dashboard { get; set; }

        public  PUsersDesignation UsersDesignation { get; set; }

        #endregion
    }
     [Serializable]
    public class PUserType
    {

        public Int16 UserTypeID { get; set; }
        public Int16 SystemID { get; set; }
        public String UserTypeCode { get; set; }
        public String CodeDescription { get; set; }
        public Boolean IsActive { get; set; }

    }
    public class PUserAudit
    {
        public int UserId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime LogoutDate { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public string SesionId { get; set; }
        public bool IsSessionExpired { get; set; }
    }
     [Serializable]
    public class PContactDetail
    {
        public Int64 ContactDetailsID { get; set; }
        public Int64 UserID { get; set; }
        public string EmailID { get; set; }
        public string Phone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public Int64 CreatedBy { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
     [Serializable]
    public class PUserModuleAccess
    {
        #region Properties
        public int UserModuleAccessID { get; set; }
        public int ModuleAccessID { get; set; }
        public int SubModuleAccessId { get; set; }
        public string Name { get; set; } 
        public PModuleAccess ModuleAccess { get; set; }
        public PSubModuleAccess SubModuleAccess { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsActive { get; set; }
        #endregion
    }
    public class PPlant
    {
        #region Properties
        public String City { get; set; }
        public String State { get; set; }
        public Int32 PlantID { get; set; }
        public String Country { get; set; }
        public String Postcode { get; set; }
        public String PlantCode { get; set; }
        public String PlantName { get; set; }
        public Boolean IsActive { get; set; }
        public String AddressLine1 { get; set; }
        public String AddressLine2 { get; set; }
        public String AddressLine3 { get; set; }
        public bool IsUpdate { get; set; }
        #endregion
    }
     [Serializable]
    public class PModuleAccess
    {
        #region Properties
        public int ModuleMasterID { get; set; }
        public string ModuleName { get; set; }
        public string ModulAwesomeIco { get; set; }
        public List<PSubModuleAccess> SubModuleAccess { get; set; }

        public int UserModuleAccessID { get; set; }
        public long UserID { get; set; }
        public int ModuleAccessID { get; set; }
        public int SubModuleAccessID { get; set; } 
      
        public bool IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string ModuleAwesomeIco { get; set; }


        #endregion
    }
     [Serializable]
    public class PSubModuleAccess
    {
        #region Properties
        public int SubModuleMasterID { get; set; }
        public int ModuleMasterID { get; set; }
        public string SubModuleName { get; set; }
        public string ParentMenu { get; set; }
        public string ModuleAction { get; set; }
        public string DisplayName1 { get; set; }

        #endregion
    }

    [Serializable]
    public class PSubModuleChile
    {
        #region Properties
        public int SubModuleChildID { get; set; }
        public PSubModuleAccess SubModule { get; set; }
        public string ChildName { get; set; } 
        #endregion
    }


    [Serializable]
    public class PAccount
    {
        public long AccountID { get; set; }
        public string StaffName { get; set; }
        public string UserName { get; set; }
        public string LoginPassword { get; set; }
        public Int16 UserTypeID { get; set; }
        public string ExternalReferenceID { get; set; }
        public string EmailID { get; set; }
        public long ContactID { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public Int16 SystemID { get; set; }
        public bool IsAuthorized { get; set; }
        public string DeclineReason { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<int> ManagerID { get; set; }
        public List<PPlant> Plants { get; set; }
    }
     [Serializable]
    public class PUserPlantMapping
    {

        #region Properties
        public int PlantID { get; set; }
        public long UserID { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        #endregion
    }
    public class PL1SupportUserMapping
    {
        #region Properties
        public int DealerID { get; set; }
        public long CategoryID { get; set; }
        public long UserID { get; set; }
        public bool IsActive { get; set; } 
        #endregion
    }
    public class PUserMobile
    {
        public int UserMobileID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string IMEI { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public Boolean IsActive { get; set; }
         
    }
     [Serializable]
    public class PDashboard
    {
        #region Properties
        public int DashboardID { get; set; }
        public string Dashboard { get; set; }      
        public long UserID { get; set; } 
        public bool IsActive { get; set; } 
        #endregion
    }


     [Serializable]
     public class PUsersDesignation
     {
         #region Properties
         public int UsersDesignationID { get; set; }
         public string UsersDesignation { get; set; } 
         #endregion
     }
}
