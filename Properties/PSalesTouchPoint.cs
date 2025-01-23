using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    //User
    [Serializable]
    public class PSalesTouchPointUser
    {
        #region Properties
        public int SalesTouchPointUserID { get; set; }
        public String AadharNumber { get; set; }
        public String Name { get; set; }
        public string ContactNumber { get; set; }
        public string EmailID { get; set; }
        public String Password { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_District District { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsLocked { get; set; }
        public Boolean IsEnabled { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? LatitudeLongitudeDate { get; set; }
        public int? LoginCount { get; set; }
        public DateTime? ManualLockDate { get; set; }
        public string OTP { get; set; }
        public DateTime? OTPExpiry { get; set; }
        #endregion
    }
    [Serializable]
    public class PSalesTouchPointUser_Insert
    {
        #region Properties
        public int SalesTouchPointUserID { get; set; }
        public String AadharNumber { get; set; }
        public String Name { get; set; }
        public string ContactNumber { get; set; }
        public string EmailID { get; set; }
        public String Password { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Boolean IsLocked { get; set; }
        public Boolean IsEnabled { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? LatitudeLongitudeDate { get; set; }
        public int? LoginCount { get; set; }
        public DateTime? ManualLockDate { get; set; }
        public string OTP { get; set; }
        public DateTime? OTPExpiry { get; set; }
        #endregion
    }
    [Serializable]
    public class PUserAudit_STP
    {
        #region Properties
        public int UserId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime LogoutDate { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public string SesionId { get; set; }
        public bool IsSessionExpired { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        #endregion
    }
    //Enquiry
    [Serializable]
    public class PSalesTouchPointEnquiry
    {
        public long SalesTouchPointEnquiryID { get; set; }        
        public string SalesTouchPointEnquiryNumber { get; set; }
        public DateTime SalesTouchPointEnquiryDate { get; set; }
        public string CustomerName { get; set; }
        public string PersonName { get; set; }
        public string Mail { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Pincode { get; set; }
        public PDMS_Country Country { get; set; }
        public PDMS_State State { get; set; }
        public PDMS_District District { get; set; }
        public string Remarks { get; set; }
        public PSalesTouchPointUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string RejectReason { get; set; }
        public long? EnquiryId { get; set; }
        public string EnquiryNumber { get; set; }
        public long? LeadID { get; set; }
        public string LeadNumber { get; set; }
        public string QuotationNumber { get; set; }
        public DateTime? QuotationDate { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public PSalesTouchPointEnquiryStatus Status { get; set; }
        public PLeadStatus LeadStatus { get; set; }
    }
    [Serializable]
    public class PSalesTouchPointEnquiry_Insert
    {
        public long SalesTouchPointEnquiryID { get; set; }
        public string CustomerName { get; set; }
        public string PersonName { get; set; }
        public string Mail { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Pincode { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public string Remarks { get; set; }
    }
    [Serializable]
    public class PSalesTouchPointEnquiryStatus
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}
