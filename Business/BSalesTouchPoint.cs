using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business
{
    public class BSalesTouchPoint
    {
        private IDataAccess provider;
        public BSalesTouchPoint()
        {
            provider = new ProviderFactory().GetProvider();
        }
        //User
        public PApiResult Login(UserAuthentication UserA)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesTouchPointUser/GetToken", UserA));
        }
        public PApiResult GetSalesTouchPointUserList(string ContactNumber, string EmailID, string Name, int? PageIndex, int? PageSize)
        {
            string endPoint = "SalesTouchPointUser/GetSalesTouchPointUserList?ContactNumber=" + ContactNumber + "&EmailID=" + EmailID + "&Name=" + Name + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            //return JsonConvert.DeserializeObject<List<PSalesTouchPointUser>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PSalesTouchPointUser GetSalesTouchPointUser(string ContactNumber, string Password)
        {
            string endPoint = "SalesTouchPointUser/GetSalesTouchPointUser?ContactNumber=" + ContactNumber + "&Password=" + Password;
            return JsonConvert.DeserializeObject<PSalesTouchPointUser>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PSalesTouchPointUser GetSalesTouchPointUserByID(int? SalesTouchPointUserID)
        {
            string endPoint = "SalesTouchPointUser/GetSalesTouchPointUserByID?SalesTouchPointUserID=" + SalesTouchPointUserID;
            return JsonConvert.DeserializeObject<PSalesTouchPointUser>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public bool ValidateChangePassword(PSalesTouchPointUser_Insert user, string oldPwd, string newPwd, string cnfmNewPwd, string PasswordType)
        {
            bool success = false;
            if (PasswordType == "Reset")
            {
                if (user.OTPExpiry >= DateTime.Now)
                {
                    if (!LMSHelper.DecodeString(Convert.ToString(user.OTP)).Equals(oldPwd))
                        throw new LMSFunctionalException(FunctionalErrorCode.InvalidOTP);
                    else if (!newPwd.Equals(cnfmNewPwd))
                        throw new LMSFunctionalException(FunctionalErrorCode.ChangePwdNewAndConfirmPwdNotMatching);
                    else if (!CheckPasswordStandard(newPwd))
                        throw new LMSFunctionalException(FunctionalErrorCode.ChangePwdStdNotMet);
                }
                else
                {
                    throw new LMSFunctionalException(FunctionalErrorCode.OTPTimeExpired);
                }
            }
            else
            {
                if (!LMSHelper.DecodeString(user.Password).Equals(oldPwd))
                    throw new LMSFunctionalException(FunctionalErrorCode.ChangePwdOldPwdIncorrect);
                else if (!newPwd.Equals(cnfmNewPwd))
                    throw new LMSFunctionalException(FunctionalErrorCode.ChangePwdNewAndConfirmPwdNotMatching);
                else if (!CheckPasswordStandard(newPwd))
                    throw new LMSFunctionalException(FunctionalErrorCode.ChangePwdStdNotMet);
            }
            success = true;
            return success;
        }
        private bool CheckPasswordStandard(string newPwd)
        {
            return Regex.IsMatch(newPwd, @"(?=.*[a-z])(?=.*[@#$%^&+=])(?=.*[0-9]).*$");
        }


        //Enquiry
        public PApiResult GetSalesTouchPointEnquiry(long? SalesTouchPointEnquiryID, string SalesTouchPointEnquiryNumber, DateTime? DateFrom, DateTime? DateTo, int? CountryID, int? StateID, int? DistrictID
            , string CustomerName, int? PageIndex, int? PageSize, int? UserId)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "SalesTouchPointEnquiry/GetSalesTouchPointEnquiry?SalesTouchPointEnquiryID=" + SalesTouchPointEnquiryID + "&SalesTouchPointEnquiryNumber=" + SalesTouchPointEnquiryNumber + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&CountryID=" + CountryID + "&StateID=" + StateID + "&DistrictID=" + DistrictID
                + "&CustomerName=" + CustomerName  + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize + "&UserId=" + UserId;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PSalesTouchPointEnquiry GetSalesTouchPointEnquiryByID(long SalesTouchPointEnquiryID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "SalesTouchPointEnquiry/GetSalesTouchPointEnquiryByID?SalesTouchPointEnquiryID=" + SalesTouchPointEnquiryID;
            return JsonConvert.DeserializeObject<PSalesTouchPointEnquiry>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult UpdateSalesTouchPointEnquiryReject(long SalesTouchPointEnquiryID, string Reason)
        {
            string endPoint = "SalesTouchPointEnquiry/UpdateSalesTouchPointEnquiryReject?SalesTouchPointEnquiryID=" + SalesTouchPointEnquiryID + "&Reason=" + Reason;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult UpdateSalesTouchPointEnquiryFromPreSalesEnquiry(long SalesTouchPointEnquiryID, long EnquiryID)
        {
            string endPoint = "SalesTouchPointEnquiry/UpdateSalesTouchPointEnquiryFromPreSalesEnquiry?SalesTouchPointEnquiryID=" + SalesTouchPointEnquiryID + "&EnquiryID=" + EnquiryID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetSalesTouchPointEnquiryStatusReportPresale(long? SalesTouchPointEnquiryID, string SalesTouchPointEnquiryNumber, DateTime? DateFrom, DateTime? DateTo, int? CountryID, int? StateID, int? DistrictID
            , string CustomerName, int? PageIndex, int? PageSize)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "SalesTouchPointEnquiry/GetSalesTouchPointEnquiryStatusReportPresale?SalesTouchPointEnquiryID=" + SalesTouchPointEnquiryID + "&SalesTouchPointEnquiryNumber=" + SalesTouchPointEnquiryNumber + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&CountryID=" + CountryID + "&StateID=" + StateID + "&DistrictID=" + DistrictID
                + "&CustomerName=" + CustomerName + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetSalesTouchPointEnquiryStatusReport(long? SalesTouchPointEnquiryID, string SalesTouchPointEnquiryNumber, DateTime? DateFrom, DateTime? DateTo, int? CountryID, int? StateID, int? DistrictID
            , string CustomerName, int? PageIndex, int? PageSize, int? UserId)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "SalesTouchPointEnquiry/GetSalesTouchPointEnquiryStatusReport?SalesTouchPointEnquiryID=" + SalesTouchPointEnquiryID + "&SalesTouchPointEnquiryNumber=" + SalesTouchPointEnquiryNumber + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&CountryID=" + CountryID + "&StateID=" + StateID + "&DistrictID=" + DistrictID
                + "&CustomerName=" + CustomerName + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize + "&UserId=" + UserId;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
}
