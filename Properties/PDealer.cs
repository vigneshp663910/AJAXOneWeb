using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_Dealer
    {
        public int DID { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string ContactName { get; set; }
        public string DisplayName { get; set; }
        public string CodeWithDisplayName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public PDMS_State StateN { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public PDMS_DealerOffice DealerOffice { get; set; }
        public PDealerBankDetails DealerBank { get; set; }
        public PUser TL { get; set; }
        public PUser SM { get; set; }
        public Boolean ServicePaidEInvoice { get; set; }
        public Boolean IsEInvoice { get; set; } 
        public DateTime? EInvoiceDate { get; set; }
        public string AuthorityName { get; set; }
        public string AuthorityDesignation { get; set; }
        public string AuthorityMobile { get; set; }
        public List<PDMS_Customer> Customer { get; set; }
        public Boolean IsActive { get; set; }
        public PDMS_Region Region { get; set; } 
        public PAddress Address { get; set; }
        public PDealerType DealerType { get; set; }
        public int CountryID { get; set; }
        public string ContactPerson { get; set; }
        public string ApiUserName { get; set; }
        public string ApiPassword { get; set; }
        public string HeadOfficeID { get; set; }
        public PUser SalesResponsibleID { get; set; }
    }

    [Serializable]
    public class PDMS_DealerOffice
    {
        public int OfficeID { get; set; }
        public int DealerID { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string OfficeName_OfficeCode { get; set; }
        public string SapLocationCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public PDMS_Country Country { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsHeadOffice { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public PDMS_District District { get; set; }
        public PDMS_State StateN { get; set; }
        public PAudit Audit { get; set; }
    }

    [Serializable]

    public class PDMS_DealerEmployee
    {
        public int DealerEmployeeID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public PDMS_DealerEmployeeAttachedFile Photo { get; set; }
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
        public PDMS_DealerEmployeeAttachedFile AdhaarCardCopyFrontSide { get; set; }
        public PDMS_DealerEmployeeAttachedFile AdhaarCardCopyBackSide { get; set; }
        public PDMS_EqucationalQualification EqucationalQualification { get; set; }

        public decimal? TotalExperience { get; set; }
        public string PANNo { get; set; }
        public PDMS_DealerEmployeeAttachedFile PANCardCopy { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }

        public PDMS_DealerEmployeeAttachedFile ChequeCopy { get; set; }
        public Boolean IsAjaxHPApproved { get; set; }
        public PUser AjaxHPApprovedBy { get; set; }
        public DateTime AjaxHPApprovedON { get; set; }
        public PDMS_DealerEmployeeRole DealerEmployeeRole { get; set; }
        public PUser CreatedBy { get; set; }

        public string EmergencyContactNumber { get; set; }
        public PDMS_BloodGroup BloodGroup { get; set; }
        public object Country { get; set; }
    }
    [Serializable]
    public class PDMS_DealerEmployeeAttachedFile
    {
        public long AttachedFileID { get; set; }
        public int DealerEmployeeID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long TicketID { get; set; }
        public byte[] AttachedFile { get; set; }
        public long FileSize { get; set; }
        public Boolean IsDeleted { get; set; }
    }
    [Serializable]
    public class PDMS_EqucationalQualification
    {
        public int EqucationalQualificationID { get; set; }
        public string EqucationalQualification { get; set; }
    }
    [Serializable]
    public class PDMS_DealerDepartment
    {
        public int DealerDepartmentID { get; set; }
        public string DealerDepartment { get; set; }
    }
    [Serializable]
    public class PDMS_DealerDesignation
    {
        public int DealerDesignationID { get; set; }
        public string DealerDesignation { get; set; }
        public PDMS_DealerDepartment Department { get; set; }
        public int SalesColdCustomerVisitTarget { get; set; }
        public int SalesProspecCustomertVisitTarget { get; set; }
        public int SalesExistCustomerVisitTarget { get; set; }
        public PUser ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    [Serializable]
    public class PDMS_DealerEmployeeRole
    {
        public string LoginUserName { get; set; }
        public long DealerEmployeeRoleID { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PUser User { get; set; }
        public PDMS_DealerOffice DealerOffice { get; set; }
        public int DealerEmployeeID { get; set; }
        public PDMS_DealerEmployee DealerEmployee { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public PDMS_DealerDepartment DealerDepartment { get; set; }
        public PDMS_DealerDesignation DealerDesignation { get; set; }
        public PDMS_DealerEmployee ReportingTo { get; set; }

        public DateTime? DateOfLeaving { get; set; }
        public Boolean? WishToLeave { get; set; }

        public Boolean IsActive { get; set; }
        public string IsActiveString
        {
            get
            {
                if (IsActive)
                    return "Active";
                else
                    return "InActive";
            }
        }
        public string SAPEmpCode { get; set; }
    }
    [Serializable]
    public class PDMS_BloodGroup
    {
        public int BloodGroupID { get; set; }
        public string BloodGroup { get; set; }
    }
    [Serializable]    
    public class PDealer
    {
        public int DID { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string ContactName { get; set; }
        public string DisplayName { get; set; }
        public string CodeWithDisplayName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public PDMS_State StateN { get; set; }
        public PDMS_State State { get; set; }
        public string StateCode { get; set; }
        public PDMS_Country Country { get; set; }
        
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public PDMS_DealerOffice DealerOffice { get; set; }
        public PDealerBankDetails DealerBank { get; set; }
        public string UserName { get; set; }
        public string CodeWithName { get; set; }
        public string MailID1 { get; set; }
        public string Phone { get; set; }
        public int UserTypeID { get; set; }        
        public Boolean IsActive { get; set; }
        public string HeadOfficeID { get; set; }
       

        public Boolean IsEInvoice { get; set; } 
        public string EInvoiveDate { get; set; }

       


        public Boolean EInvAPI { get; set; }
        public string GspCode { get; set; }
        public string Gstin { get; set; }
        public string ApiUserName { get; set; }
        public string ApiPassword { get; set; }

        public PEInvUserAPI EInvUserAPI { get; set; }
        public PDealerType DealerType { get; set; } 
    }
    [Serializable] 
    public class PDealerStateMappingID
    {
        public int DealerStateMappingID { get; set; }
        public PDealer Dealer { get; set; }
        public PDMS_State State { get; set; } 
    }
    [Serializable]
    public class PDealerAddress
    {
        public int DealerID { get; set; }
        public string Gstin { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public PDMS_State State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string Pan { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public string ContactPerson { get; set; }  
    }

    [Serializable]
    public class PDealerBankDetails
    {
        public int DealerBankID { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string AcNumber { get; set; }
        public string IfscCode { get; set; }
        public Boolean IsActive { get; set; }
        public int DealerID { get; set; }
    }
    [Serializable]
    public class PDealerNotification
    {
        public int DealerNotificationID { get; set; }
        public PDealerNotificationModule Module { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PUser User { get; set; }
        public Boolean IsSMS { get; set; }
        public Boolean IsMail { get; set; }
        public Boolean IsActive { get; set; }
    }

    [Serializable]
    public class PDealerNotificationModule
    {
        public int DealerNotificationModuleID { get; set; }
        public string ModuleName { get; set; }
    }
    [Serializable]
    public class PEInvUserAPI
    {
        public string Handle { get; set; }
        public string HandleType { get; set; }
        public string Password { get; set; }
    }
    [Serializable]
    public class PDealerUserPermission
    {
        public int DealerID { get; set; }
        public string UserName { get; set; }
        public string DealerCode { get; set; }
        public string ContactName { get; set; }
        public string CodeWithName { get; set; }
        public string MailID { get; set; }
        public string DealerDesignation { get; set; }
        public string DealerDepartment { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string ChildName { get; set; }
    }

    [Serializable]
    public class PDealerBinLocation
    {
        public int DealerBinLocationMaterialMappingID { get; set; }
        public int DealerBinLocationID { get; set; }
        public string BinName { get; set; }
        public PDealer Dealer { get; set; }
        public PDMS_DealerOffice DealerOffice { get; set; }
        public PDMS_Material Material { get; set; }
    }
    [Serializable]
    public class PDealerType
    {
        public int DealerTypeID { get; set; } 
        public string DealerType { get; set; } 
    }

    [Serializable]
    public class PDealerBusinessExcellenceCategory1
    {
        public int DealerBusinessExcellenceCategory1 { get; set; }
        public string FunctionArea { get; set; }
        public int MaxScore { get; set; }
    }
    [Serializable]
    public class PDealerBusinessExcellenceCategory2
    {
        public int DealerBusinessExcellenceCategory2 { get; set; }
        public string FunctionSubArea { get; set; }
        public PDealerBusinessExcellenceCategory1 Category1 { get; set; }
    }
    [Serializable]
    public class PDealerBusinessExcellenceCategory3
    {
        public int DealerBusinessExcellenceCategory3ID { get; set; }
        public string Parameter { get; set; }
        public int ParameterMaxScore { get; set; }
        public int MinimumQualifying { get; set; }
        public PDealerBusinessExcellenceCategory2 Category2 { get; set; }
    }
    [Serializable]
    public class PDealerBusinessExcellence
    {
       // public long DealerBusinessExcellenceID { get; set; }
        public int DealerBusinessExcellenceCategory3ID { get; set; }
        public string FunctionArea { get; set; }
        public string Category2 { get; set; }
        public string Parameter { get; set; }  
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName
        {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month).Substring(0, 3);
            }
        }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }

        public decimal Target { get; set; }
        public decimal Actual { get; set; }
        public string Remarks { get; set; } 
        public Boolean IsSubmitted { get; set; }
    }
    [Serializable]
    public class PDealerBusinessExcellenceHeader
    {
        public long DealerBusinessExcellenceID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName
        {
            get
            {
                return Month == 0 ? "" : CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month).Substring(0, 3);
            }
        } 
        public PDMS_Dealer Dealer { get; set; }  
        public PAjaxOneStatus Status { get; set; }
        public PUser RequestedBy { get; set; }
        public DateTime RequestedOn { get; set; }
        public PUser SubmittedBy { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public PUser ApprovalL1By { get; set; }
        public DateTime? ApprovalL1On { get; set; }
        public PUser ApprovalL2By { get; set; }
        public DateTime? ApprovalL2On { get; set; }
        public PUser ApprovalL3By { get; set; }
        public DateTime? ApprovalL3On { get; set; }
        public PUser ApprovalL4By { get; set; }
        public DateTime? ApprovalL4On { get; set; }
        public List<PDealerBusinessExcellenceItem> Items { get; set; }
    }
    [Serializable]
    public class PDealerBusinessExcellenceItem
    {
        public long DealerBusinessExcellenceItemID { get; set; }
        public int DealerBusinessExcellenceCategory3ID { get; set; }
        public PDealerBusinessExcellenceCategory1 FunctionArea { get; set; }
        public PDealerBusinessExcellenceCategory2 FunctionSubArea { get; set; }
        public PDealerBusinessExcellenceCategory3 Parameter { get; set; }  
        public decimal Target { get; set; }
        public decimal Actual { get; set; }
        public string Remarks { get; set; } 
    }
    [Serializable]
    public class PAddress
    {
        public int AddressID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public PDMS_District District { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_Country Country { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public string ContactPerson { get; set; }
    }
    [Serializable]
    public class PDMS_DealerOffice_Insert
    {
        public int OfficeID { get; set; }
        public int DealerID { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string SapLocationCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public int DistrictID { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public string Pincode { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsHeadOffice { get; set; }
        public int UserID { get; set; }
    }

    [Serializable]
    public class PAudit
    {
        public PUser CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public PUser MoidifiedBy { get; set; } 
        public DateTime? MoidifiedOn { get; set; }
    }

    [Serializable]
    public class PDealer_Insert
    { 
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string DealerShortName { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int DealerTypeID { get; set; }    
        public Boolean? IsActive { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public int DistrictID { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public string Pincode { get; set; }              
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string AcNumber { get; set; }
        public string IfscCode { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string SapLocationCode { get; set; }
        public Boolean? IsEInvoice { get; set; }
        public DateTime? EInvoiceDate { get; set; }
        public string APIUsername { get; set; }
        public string APIPassword { get; set; }
        public Boolean? IsServicePaidEInvoice { get; set; }       
    }
    [Serializable]
    public class PDealer_Update
    {
        public int DealerID { get; set; }
        public int DealerCode { get; set; }
        public string DealerName { get; set; }
        public string DealerShortName { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int DealerTypeID { get; set; }
        public Boolean? IsActive { get; set; }    
        public Boolean? IsEInvoice { get; set; }
        public DateTime? EInvoiceDate { get; set; }
        public string APIUsername { get; set; }
        public string APIPassword { get; set; }
        public Boolean? IsServicePaidEInvoice { get; set; }
    }
}
