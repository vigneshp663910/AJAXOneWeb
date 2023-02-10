using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
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
        public string UserName { get; set; }
        public string CodeWithName { get; set; }
        public string MailID1 { get; set; }
        public string Phone { get; set; }
        public int UserTypeID { get; set; }        
        public Boolean IsActive { get; set; }
        public string HeadOfficeID { get; set; }
        public string StateCode { get; set; }

        public Boolean IsEInvoice { get; set; }
        public string EInvoiveFTPPath { get; set; }
        public string EInvoiveFTPUserID { get; set; }
        public string EInvoiveFTPPassword { get; set; }
        public string EInvoiveDate { get; set; }

        public PDMS_Country Country { get; set; }
        public PDMS_State State { get; set; }


        public Boolean EInvAPI { get; set; }
        public string GspCode { get; set; }
        public string Gstin { get; set; }
        public string ApiUserName { get; set; }
        public string ApiPassword { get; set; }

        public PEInvUserAPI EInvUserAPI { get; set; }
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
}
