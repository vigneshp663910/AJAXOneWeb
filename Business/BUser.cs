using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;

namespace Business
{
    public class BUser
    {
        private IDataAccess provider;
        public BUser()
        {
            provider = new ProviderFactory().GetProvider();
        }
        #region Public Methods
        
        public DataTable AuthenticateUser(string userName)
        {
            try
            {
                DateTime traceStartTime = DateTime.Now;
                DataTable userDataTable = new DataTable();

                DbParameter userIDParams = provider.CreateParameter("UserName", userName, DbType.String);
                DbParameter DMSP = provider.CreateParameter("DMS", 2, DbType.Int32);
                DbParameter[] userParams = new DbParameter[2] { userIDParams, DMSP };

                using (DataSet userDataSet = provider.Select("AuthenticateUser", userParams))
                {
                    if (userDataSet != null)
                        userDataTable = userDataSet.Tables[0];
                }

                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);

                return userDataTable;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        private PUser ConvertToUser(DataRow userRow)
        {
            return new PUser()
            {
                PassWord = Convert.ToString(userRow["LoginPassword"]),
                UserID = Convert.ToInt32(userRow["UserID"]),
                UserName = Convert.ToString(userRow["UserName"]),
                ContactName = Convert.ToString(userRow["ContactName"]),
                UserTypeID = Convert.ToInt16(userRow["UserTypeID"]),
                ExternalReferenceID = userRow["ExternalReferenceID"] != DBNull.Value ? Convert.ToString(userRow["ExternalReferenceID"]) : string.Empty,
                IsFirstTimeLogin = Convert.ToBoolean(userRow["IsFirstTime"]),
                IsLocked = Convert.ToBoolean(userRow["IsLocked"]),
                IsEnabled = Convert.ToBoolean(userRow["IsEnabled"]),
                PasswordExpiryDate = Convert.ToDateTime(userRow["PasswordExpirationDate"]),
                CreatedBy = Convert.ToInt32(userRow["CreatedBy"]),
                CreatedOn = Convert.ToDateTime(userRow["CreatedOn"]),
                L1Support = userRow["L1Support"] != DBNull.Value ? Convert.ToBoolean(userRow["L1Support"]) : false,
                IsTechnician = userRow["IsTechnician"] != DBNull.Value ? Convert.ToBoolean(userRow["IsTechnician"]) : false,
                Mail = Convert.ToString(userRow["Mail"]),
                SystemCategoryID = Convert.ToInt16(userRow["SystemCategoryID"]),
                ContactNumber = Convert.ToString(userRow["ContactNumber"]),
                OTP = Convert.ToString(userRow["OTP"]),
                OTPExpiry = Convert.ToDateTime(userRow["OTPExpiry"]),

                DealerEmployeeID = Convert.ToInt32(userRow["DealerEmployeeID"]),
                Department = new PDMS_DealerDepartment()
                {
                    DealerDepartmentID = Convert.ToInt32(userRow["DealerDepartmentID"]),
                    DealerDepartment = Convert.ToString(userRow["DealerDepartment"])
                },
                Designation = new PDMS_DealerDesignation()
                {
                    DealerDesignationID = Convert.ToInt32(userRow["DealerDepartmentID"]),
                    DealerDesignation = Convert.ToString(userRow["DealerDesignation"])
                },
                Employee = new PDMS_DealerEmployee()
                {
                    Name = Convert.ToString(userRow["Name"]),
                    DealerEmployeeID = Convert.ToInt32(userRow["DealerEmployeeID"]),
                    ContactNumber = Convert.ToString(userRow["DealerContactNumber"]),
                    Email = Convert.ToString(userRow["DealerEmailID"])
                }
            };
        }
        public PUser GetUserDetails(string userName)
        {
            PUser userAuthDetails = new PUser();
            try
            {
                DateTime tracerStart = DateTime.Now;
                if (!string.IsNullOrEmpty(userName))
                {
                    using (DataTable userData = AuthenticateUser(userName))
                    {
                        if (userData.Rows.Count > 0)
                            userAuthDetails = ConvertToUser(userData.Rows[0]);
                    }
                }
                TraceLogger.Log(tracerStart);
                return userAuthDetails;
            }
            catch (LMSException lmsEx)
            {
                throw lmsEx;
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public PUser GetUserDetails(long userID)
        {
            PUser userAuthDetails = new PUser();
            try
            {
                DateTime traceStart = DateTime.Now;
                using (DataTable userData = GetLoginUserDetails(userID))
                {
                    if (userData.Rows.Count > 0)
                        userAuthDetails = ConvertToUser(userData.Rows[0]);
                    TraceLogger.Log(traceStart);
                    return userAuthDetails;
                }
            }
            catch (LMSException lmsEx)
            {
                throw lmsEx;
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public DataTable GetLoginUserDetails(long userID)
        {
            try
            {
                DateTime traceStartTime = DateTime.Now;
                DataTable userDataTable = new DataTable();

                DbParameter userIDParams = provider.CreateParameter("UserID", userID, DbType.Int64);
                DbParameter DMSP = provider.CreateParameter("DMS", 2, DbType.Int32);
                DbParameter[] userParams = new DbParameter[2] { userIDParams, DMSP };

                using (DataSet userDataSet = provider.Select("GetUserDetails", userParams))
                {
                    if (userDataSet != null)
                        userDataTable = userDataSet.Tables[0];
                }

                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);

                return userDataTable;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }

        public List<PUser> GetUsersActivityTracking(long? UserID, string UserName, int? UserTypeID, string ExternalReferenceID, int? DealerID, bool? IsEnabled, string ContactName, int? DealerDepartmentID, int? DealerDesignationID, bool? IsLocked, bool? ajaxOne, int? PageIndex, int? PageSize, out int RowCount)
        {
            List<PUser> users = new List<PUser>();
            PUser user = null;
            DateTime traceStartTime = DateTime.Now;
            DataTable usersDataTable = new DataTable();
            RowCount = 0;
            try
            {
                DbParameter UserNameP, ExternalReferenceIDP;

                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);

                if (!string.IsNullOrEmpty(UserName))
                    UserNameP = provider.CreateParameter("UserName", UserName, DbType.String);
                else
                    UserNameP = provider.CreateParameter("UserName", DBNull.Value, DbType.String);

                DbParameter UserTypeIDP = provider.CreateParameter("UserTypeID", UserTypeID, DbType.Int32);

                if (!string.IsNullOrEmpty(ExternalReferenceID))
                    ExternalReferenceIDP = provider.CreateParameter("ExternalReferenceID", ExternalReferenceID, DbType.String);
                else
                    ExternalReferenceIDP = provider.CreateParameter("ExternalReferenceID", DBNull.Value, DbType.String);

                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

                DbParameter IsEnabledP = provider.CreateParameter("IsEnabled", IsEnabled, DbType.Boolean);
                DbParameter IsLockedP = provider.CreateParameter("IsLocked", IsLocked, DbType.Boolean);
                DbParameter ajaxOneP = provider.CreateParameter("ajaxOne", ajaxOne, DbType.Boolean);
                DbParameter ContactNameP = provider.CreateParameter("ContactName", ContactName, DbType.String);

                DbParameter DealerDepartmentIDP = provider.CreateParameter("DealerDepartmentID", DealerDepartmentID, DbType.Int32);
                DbParameter DealerDesignationIDP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int32);
                DbParameter DMS = provider.CreateParameter("DMS", 2, DbType.Int32);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);

                DbParameter[] userParams = new DbParameter[14] { UserIDP, UserNameP, UserTypeIDP, ExternalReferenceIDP, DealerIDP, IsEnabledP, ContactNameP, DealerDepartmentIDP, DealerDesignationIDP, DMS, IsLockedP, ajaxOneP, PageIndexP, PageSizeP };

                using (DataSet usersDataSet = provider.Select("GetUsersActivityTracking", userParams))
                {
                    if (usersDataSet != null)
                        foreach (DataRow userRow in usersDataSet.Tables[0].Rows)
                        {
                            user = new PUser();
                            user.PassWord = Convert.ToString(userRow["LoginPassword"]);
                            user.UserID = Convert.ToInt32(userRow["UserID"]);
                            user.UserName = Convert.ToString(userRow["UserName"]);
                            user.ContactName = Convert.ToString(userRow["ContactName"]);
                            user.UserTypeID = Convert.ToInt16(userRow["UserTypeID"]);
                            user.ExternalReferenceID = userRow["ExternalReferenceID"] != DBNull.Value ? Convert.ToString(userRow["ExternalReferenceID"]) : string.Empty;
                            user.IsFirstTimeLogin = Convert.ToBoolean(userRow["IsFirstTime"]);
                            user.IsLocked = Convert.ToBoolean(userRow["IsLocked"]);
                            user.IsEnabled = Convert.ToBoolean(userRow["IsEnabled"]);
                            user.PasswordExpiryDate = Convert.ToDateTime(userRow["PasswordExpirationDate"]);
                            user.CreatedBy = Convert.ToInt32(userRow["CreatedBy"]);
                            user.CreatedOn = Convert.ToDateTime(userRow["CreatedOn"]);
                            user.LastLoginDate = userRow["LastLoginDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(userRow["LastLoginDate"]);
                            user.CreatedByName = Convert.ToString(userRow["UserName"]);
                            user.SystemCategoryID = Convert.ToInt16(userRow["SystemCategoryID"]);
                            user.Mail = Convert.ToString(userRow["Mail"]);
                            user.ContactNumber = Convert.ToString(userRow["ContactNumber"]);
                            user.IsTechnician = userRow["IsTechnician"] == DBNull.Value ? false : Convert.ToBoolean(userRow["IsTechnician"]);
                            user.DaysSince = userRow["DaysSince"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(userRow["DaysSince"]);
                            user.LoginCount = userRow["LoginCount"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(userRow["LoginCount"]);
                            users.Add(user);
                            RowCount = Convert.ToInt32(userRow["RowCount"]);
                        }
                }
                TraceLogger.Log(traceStartTime);
                return users;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         public List<PUser> GetUsers(long? UserID, string UserName, int? UserTypeID, string ExternalReferenceID, int? DealerID, bool? IsEnabled, string ContactName, int? DealerDepartmentID, int? DealerDesignationID)      
        {
            string endPoint = "User/GetUsers?UserID=" + UserID + "&UserName=" + UserName + "&UserTypeID=" + UserTypeID + "&ExternalReferenceID=" + ExternalReferenceID + "&DealerID=" + DealerID
                 + "&IsEnabled=" + IsEnabled + "&ContactName=" + ContactName + "&DealerDepartmentID=" + DealerDepartmentID + "&DealerDesignationID=" + DealerDesignationID;
            return JsonConvert.DeserializeObject<List<PUser>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public void LockUserAccount(PUser user)
        {
            try
            {
                DateTime tracerStartTime = DateTime.Now;
                UpdateUser(user);
                string emailId = GetContactDetailByUser(user.UserID).EmailID;
                if (!user.IsLocked)
                    new LMSMessaging().SendUnLockUserMail(user, emailId);
                TraceLogger.Log(tracerStartTime);
            }
            catch (LMSException lmsEx)
            {
                throw lmsEx;
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public void UpdateUser(PUser userDAO)
        {
            DateTime traceStartTime = DateTime.Now;
            DbParameter enableDisableReasonParam;
            try
            {
                DbParameter contactNameParam = provider.CreateParameter("ContactName", userDAO.ContactName, DbType.String);
                DbParameter userNameParam = provider.CreateParameter("UserName", userDAO.UserName, DbType.String);
                DbParameter loginPasswordParam = provider.CreateParameter("LoginPassword", userDAO.PassWord, DbType.String);
                DbParameter userTypeIDParam = provider.CreateParameter("UserTypeID", userDAO.UserTypeID, DbType.Int16);
                DbParameter externalReferenceIDParam = provider.CreateParameter("ExternalReferenceID", userDAO.ExternalReferenceID, DbType.String);
                DbParameter isFirstTimeParam = provider.CreateParameter("IsFirstTime", userDAO.IsFirstTimeLogin, DbType.Boolean);
                DbParameter isLockedParam = provider.CreateParameter("IsLocked", userDAO.IsLocked, DbType.Boolean);
                DbParameter isEnabledParam = provider.CreateParameter("IsEnabled", userDAO.IsEnabled, DbType.Boolean);
                DbParameter passwordExpirationDateParam = provider.CreateParameter("PasswordExpirationDate", userDAO.PasswordExpiryDate, DbType.DateTime);
                if (userDAO.EnableDisableReason != null)
                    enableDisableReasonParam = provider.CreateParameter("EnableDisableReason", userDAO.EnableDisableReason, DbType.String);
                else
                    enableDisableReasonParam = provider.CreateParameter("EnableDisableReason", DBNull.Value, DbType.String);
                DbParameter createdByParam = provider.CreateParameter("CreatedBy", userDAO.CreatedBy, DbType.Int64);
                DbParameter updatedByParam = provider.CreateParameter("UpdatedBy", userDAO.UpdatedBy, DbType.Int64);
                DbParameter createdOnParam = provider.CreateParameter("CreatedOn", userDAO.CreatedOn, DbType.DateTime);

                DbParameter updatedOnParam = provider.CreateParameter("UpdatedOn", userDAO.UpdatedOn, DbType.DateTime);
                DbParameter userIDParam = provider.CreateParameter("UserID", userDAO.UserID, DbType.Int64);
                DbParameter MailP = provider.CreateParameter("Mail", userDAO.Mail, DbType.String);

                DbParameter[] userParams = new DbParameter[16] { userIDParam, contactNameParam, userNameParam, loginPasswordParam, userTypeIDParam, externalReferenceIDParam,
                                                                     isFirstTimeParam, isLockedParam, isEnabledParam, passwordExpirationDateParam,
                                                                    enableDisableReasonParam, updatedByParam,updatedOnParam, createdByParam, createdOnParam,MailP};

                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);

                provider.Update("UpdateUser", userParams);
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }

        }

        public void UpdateResetPassword(string UserName, string Password)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                DbParameter UserNameP = provider.CreateParameter("UserName", UserName, DbType.String);
                DbParameter PasswordP = provider.CreateParameter("Password", Password, DbType.String);

                DbParameter[] userParams = new DbParameter[2] { UserNameP, PasswordP };

                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);

                provider.Update("UpdateResetPassword", userParams);
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }

        public PContactDetail GetContactDetailByUser(Int64 userID)
        {
            PContactDetail contactDetail = new PContactDetail();
            try
            {
                DateTime traceStartTime = DateTime.Now;
                using (DataTable contactDetailData = GetContactDetailsByUser(userID))
                {
                    if (contactDetailData.Rows.Count > 0)
                        contactDetail = ConvertToContactDetail(contactDetailData.Rows[0]);
                    TraceLogger.Log(traceStartTime);
                    return contactDetail;
                }
            }
            catch (LMSException lmsEx)
            {
                throw lmsEx;
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public DataTable GetContactDetailsByUser(Int64 UserId)
        {
            DateTime traceStartTime = DateTime.Now;
            DataTable contactDetailDataTable = new DataTable();
            try
            {
                DbParameter userIdParam = provider.CreateParameter("UserId", UserId, DbType.Int64);
                DbParameter[] contactDetailParams = new DbParameter[1] { userIdParam };

                using (DataSet contactDetailsDataSet = provider.Select("GetContactDetailsByUser", contactDetailParams))
                {
                    if (contactDetailsDataSet != null)
                        contactDetailDataTable = contactDetailsDataSet.Tables[0];
                }
                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);
                return contactDetailDataTable;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        private PContactDetail ConvertToContactDetail(DataRow contactDetailRow)
        {
            return new PContactDetail()
            {
                AddressLine1 = Convert.ToString(contactDetailRow["AddressLine1"]),
                AddressLine2 = Convert.ToString(contactDetailRow["AddressLine2"]),
                AddressLine3 = Convert.ToString(contactDetailRow["AddressLine3"]),
                UserID = Convert.ToInt64(contactDetailRow["UserID"]),
                ContactDetailsID = Convert.ToInt64(contactDetailRow["ContactDetailsID"]),
                City = Convert.ToString(contactDetailRow["City"]),
                Country = Convert.ToString(contactDetailRow["Country"]),
                EmailID = Convert.ToString(contactDetailRow["EmailID"]),
                Phone = Convert.ToString(contactDetailRow["Phone"]),
                Postcode = Convert.ToString(contactDetailRow["Postcode"]),
                State = Convert.ToString(contactDetailRow["State"])
            };
        }

        public Boolean InsertOrUpdateUser(PUser userDAO)
        {
            Boolean Success = false;
            int userId = 0;
            DateTime traceStartTime = DateTime.Now;
            DbParameter enableDisableReasonParam;
            try
            {
                DbParameter contactNameParam = provider.CreateParameter("ContactName", userDAO.ContactName, DbType.String);
                DbParameter userNameParam = provider.CreateParameter("UserName", userDAO.UserName, DbType.String);
                DbParameter loginPasswordParam = provider.CreateParameter("LoginPassword", userDAO.PassWord, DbType.String);
                DbParameter userTypeIDParam = provider.CreateParameter("UserTypeID", userDAO.UserTypeID, DbType.Int16);
                DbParameter externalReferenceIDParam = provider.CreateParameter("ExternalReferenceID", userDAO.ExternalReferenceID, DbType.String);
                DbParameter isFirstTimeParam = provider.CreateParameter("IsFirstTime", userDAO.IsFirstTimeLogin, DbType.Boolean);
                DbParameter isLockedParam = provider.CreateParameter("IsLocked", userDAO.IsLocked, DbType.Boolean);
                DbParameter isEnabledParam = provider.CreateParameter("IsEnabled", userDAO.IsEnabled, DbType.Boolean);
                DbParameter ajaxOne = provider.CreateParameter("ajaxOne", userDAO.ajaxOne, DbType.Boolean);
                DbParameter IsDisabledService = provider.CreateParameter("IsDisabledService", userDAO.IsDisabledService, DbType.Boolean);
                DbParameter passwordExpirationDateParam = provider.CreateParameter("PasswordExpirationDate", userDAO.PasswordExpiryDate, DbType.DateTime);
                if (userDAO.EnableDisableReason != null)
                    enableDisableReasonParam = provider.CreateParameter("EnableDisableReason", userDAO.EnableDisableReason, DbType.String);
                else
                    enableDisableReasonParam = provider.CreateParameter("EnableDisableReason", DBNull.Value, DbType.String);
                DbParameter createdByParam = provider.CreateParameter("CreatedBy", userDAO.CreatedBy, DbType.Int64);
                DbParameter updatedByParam = provider.CreateParameter("UpdatedBy", userDAO.UpdatedBy, DbType.Int64);
                DbParameter createdOnParam = provider.CreateParameter("CreatedOn", userDAO.CreatedOn, DbType.DateTime);
                DbParameter updatedOnParam = provider.CreateParameter("UpdatedOn", userDAO.UpdatedOn, DbType.DateTime);

                DbParameter MailP = provider.CreateParameter("Mail", userDAO.Mail, DbType.String);
                DbParameter PhoneP = provider.CreateParameter("Phone", userDAO.ContactNumber, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", userDAO.UserID, DbType.String);

                DbParameter IsTechnician = provider.CreateParameter("IsTechnician", userDAO.IsTechnician, DbType.Boolean);


                DbParameter OutValueP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt16(ParameterDirections.Output));
                DbParameter[] userParams = new DbParameter[21] { contactNameParam, userNameParam, loginPasswordParam, userTypeIDParam, externalReferenceIDParam,
                                                                     isFirstTimeParam, isLockedParam, isEnabledParam,ajaxOne,IsDisabledService, passwordExpirationDateParam,
                                                                    enableDisableReasonParam, createdByParam,updatedByParam,createdOnParam,updatedOnParam,MailP,PhoneP,UserIDP,IsTechnician,OutValueP };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    userId = provider.Insert("InsertOrUpdateUser", userParams, true);
                    scope.Complete();
                    Success = true;
                }
                TraceLogger.Log(traceStartTime);
                return Success;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }

        }

        public void InsertUpdateUserAudit(PUserAudit userAudit)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                DbParameter UserId = provider.CreateParameter("UserId", userAudit.UserId, DbType.Int64);
                DbParameter LoginDate = provider.CreateParameter("LoginDate", userAudit.LoginDate, DbType.DateTime);
                DbParameter LogoutDate = provider.CreateParameter("LogoutDate", userAudit.LogoutDate, DbType.DateTime);
                DbParameter IPAddress = provider.CreateParameter("IPAddress", userAudit.IPAddress, DbType.String);
                DbParameter Browser = provider.CreateParameter("Browser", userAudit.Browser, DbType.String);
                DbParameter SesionId = provider.CreateParameter("SesionId", userAudit.SesionId, DbType.String);
                DbParameter IsSessionExpired = provider.CreateParameter("IsSessionExpired", userAudit.IsSessionExpired, DbType.Boolean);
                DbParameter Latitude = provider.CreateParameter("Latitude", string.IsNullOrEmpty( userAudit.Latitude)?null: userAudit.Latitude, DbType.String);
                DbParameter Longitude = provider.CreateParameter("Longitude", string.IsNullOrEmpty(userAudit.Longitude) ? null : userAudit.Longitude, DbType.String);
                DbParameter[] userParams = new DbParameter[9] { UserId, LoginDate, LogoutDate, IPAddress, Browser, SesionId, IsSessionExpired, Latitude, Longitude };
                provider.Insert("InsertUpdateUserAuditDetails", userParams, false);
                TraceLogger.Log(traceStartTime);
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }


        /// <summary>
        /// When user hanges the password, this method does the basic validations and update the user record
        /// </summary>
        /// <param name="userId">long</param>
        /// <param name="oldPwd">string</param>
        /// <param name="newPwd">string</param>
        /// <param name="cnfmNewPwd">string</param>
        public int ChangePassword(int userId, string oldPwd, string newPwd, string cnfmNewPwd, string PasswordType)
        {
            try
            {
                DateTime tracerStartTime = DateTime.Now;
                PUser user = GetUserDetails(userId);
                ValidateChangePassword(user, oldPwd, newPwd, cnfmNewPwd, PasswordType);
                user.IsFirstTimeLogin = false;
                user.IsLocked = false;
                user.IsEnabled = true;
                user.PassWord = LMSHelper.EncodeString(newPwd);
                user.PasswordExpiryDate = DateTime.Now.AddMonths(3);
                user.UpdatedBy = user.UserID;
                user.UpdatedOn = DateTime.Now;
                UpdateUser(user);
                TraceLogger.Log(tracerStartTime);
                return 1;
            }
            catch (LMSException lmsEx)
            {
                throw lmsEx;
            }
            catch (LMSFunctionalException lmsfExe)
            {
                throw lmsfExe;
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method is used to validate user authentication
        /// </summary>
        /// <param name="userAuthDetails"></param>
        /// <param name="password"></param>
        private void ValidateAuthenication(PUser userAuthDetails, string password)
        {
            if (userAuthDetails == null || string.IsNullOrEmpty(userAuthDetails.UserName))
                throw new LMSFunctionalException(FunctionalErrorCode.InvalidUserName);
            else if (!password.Equals(LMSHelper.DecodeString(userAuthDetails.PassWord.Trim())))
                throw new LMSFunctionalException(FunctionalErrorCode.InvalidPassword);
            else if (userAuthDetails.IsLocked)
                throw new LMSFunctionalException(FunctionalErrorCode.AccountLocked);
            else if (!userAuthDetails.IsEnabled)
                throw new LMSFunctionalException(FunctionalErrorCode.AccountDisabled);

        }
        private void ValidateChangePassword(PUser user, string oldPwd, string newPwd, string cnfmNewPwd, string PasswordType)
        {
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
                if (!LMSHelper.DecodeString(user.PassWord).Equals(oldPwd))
                    throw new LMSFunctionalException(FunctionalErrorCode.ChangePwdOldPwdIncorrect);
                else if (!newPwd.Equals(cnfmNewPwd))
                    throw new LMSFunctionalException(FunctionalErrorCode.ChangePwdNewAndConfirmPwdNotMatching);
                else if (!CheckPasswordStandard(newPwd))
                    throw new LMSFunctionalException(FunctionalErrorCode.ChangePwdStdNotMet);
            }
        }
        private bool CheckPasswordStandard(string newPwd)
        {
            return Regex.IsMatch(newPwd, @"(?=.*[a-z])(?=.*[@#$%^&+=])(?=.*[0-9]).*$");
        }

        /// <summary>
        /// This method is used to convert user datarow to UserVO.
        /// </summary>
        /// <param name="userRow"></param>
        /// <returns></returns>
        private PUser ConvertToUserVO(DataRow userRow)
        {
            return new PUser()
            {
                PassWord = Convert.ToString(userRow["LoginPassword"]),
                UserID = Convert.ToInt32(userRow["UserID"]),
                UserName = Convert.ToString(userRow["UserName"]),
                ContactName = Convert.ToString(userRow["ContactName"]),
                UserTypeID = Convert.ToInt16(userRow["UserTypeID"]),
                ExternalReferenceID = userRow["ExternalReferenceID"] != DBNull.Value ? Convert.ToString(userRow["ExternalReferenceID"]) : string.Empty,

                IsFirstTimeLogin = Convert.ToBoolean(userRow["IsFirstTime"]),
                IsLocked = Convert.ToBoolean(userRow["IsLocked"]),
                IsEnabled = Convert.ToBoolean(userRow["IsEnabled"]),
                PasswordExpiryDate = Convert.ToDateTime(userRow["PasswordExpirationDate"]),
                CreatedBy = Convert.ToInt32(userRow["CreatedBy"]),
                CreatedOn = Convert.ToDateTime(userRow["CreatedOn"]),
                CreatedByName = Convert.ToString(userRow["UserName"]),
                SystemCategoryID = Convert.ToInt16(userRow["SystemCategoryID"]),
                //  IsNatesanGroupUser = userRow.Table.Columns.Contains("SisterCompanyId") ? (userRow["SisterCompanyId"] != DBNull.Value ? true : false) : true,
                Mail = Convert.ToString(userRow["Mail"]),
                ContactNumber = Convert.ToString(userRow["ContactNumber"]),
                IsTechnician = userRow["IsTechnician"] == DBNull.Value ? false : Convert.ToBoolean(userRow["IsTechnician"]),
            };
        }

        /// <summary>
        /// This method is used to convert contact datarow to Contact detail VO.
        /// </summary>
        /// <param name="contactDetailRow"></param>
        /// <returns></returns>
        private PContactDetail ConvertToContactDetailVO(DataRow contactDetailRow)
        {
            return new PContactDetail()
            {
                AddressLine1 = Convert.ToString(contactDetailRow["AddressLine1"]),
                AddressLine2 = Convert.ToString(contactDetailRow["AddressLine2"]),
                AddressLine3 = Convert.ToString(contactDetailRow["AddressLine3"]),
                UserID = Convert.ToInt64(contactDetailRow["UserID"]),
                ContactDetailsID = Convert.ToInt64(contactDetailRow["ContactDetailsID"]),
                City = Convert.ToString(contactDetailRow["City"]),
                Country = Convert.ToString(contactDetailRow["Country"]),
                EmailID = Convert.ToString(contactDetailRow["EmailID"]),
                Phone = Convert.ToString(contactDetailRow["Phone"]),
                Postcode = Convert.ToString(contactDetailRow["Postcode"]),
                State = Convert.ToString(contactDetailRow["State"])
            };
        }

        /// <summary>
        /// This method is used to convert UserVO to UserDAO.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>



        /// <summary>
        /// This method is used to convert UserModuleAccessVO,UserVO to ModuleAccess DAO
        /// </summary>
        /// <param name="moduleAccess">UserModuleAccessVO</param>
        /// <param name="user">UserVO</param>
        /// <returns>ModuleAccessDAO</returns>


        private PUserAudit ConvertToUserAuditVO(DataRow userRow)
        {
            return new PUserAudit()
            {
                UserId = Convert.ToInt32(userRow["UserId"]),
                SesionId = Convert.ToString(userRow["SesionId"]),
                LogoutDate = Convert.ToDateTime(userRow["LogoutDate"]),
                LoginDate = Convert.ToDateTime(userRow["LoginDate"]),
                Browser = Convert.ToString(userRow["Browser"]),
                IPAddress = Convert.ToString(userRow["IPAddress"]),
                IsSessionExpired = Convert.ToBoolean(userRow["IsSessionExpired"]),
            };
        }
        #endregion


        public void UserAudit(PUserAudit audit)
        {
            try
            {
                InsertUpdateUserAudit(audit);
            }
            catch (LMSException vpEx)
            {
                ExceptionLogger.LogError("Failure during user audit trials", vpEx);
                throw vpEx;
            }
        }

        public void LockUserAccount(string userName)
        {
            try
            {
                DateTime tracerStart = DateTime.Now;
                PUser userVO = GetUserDetails(userName);
                userVO.IsLocked = true;
                userVO.UpdatedBy = userVO.UserID;
                userVO.UpdatedOn = DateTime.Now;
                LockUserAccount(userVO);
                TraceLogger.Log(tracerStart);
            }
            catch (LMSException ex)
            {
                ExceptionLogger.LogError("LockUserAccount", ex);
                throw ex;
            }
        }
        public List<PModuleAccess> GetDMSModuleByUser(Int64 UserId, int? ModuleMasterID, int? SubModuleMasterID,Boolean All = true)
        {
            DateTime traceStartTime = DateTime.Now;
            List<PModuleAccess> MAs = new List<PModuleAccess>();
            int ID = 0;
            PModuleAccess MA = null;
            try
            {
                DbParameter userIdP = provider.CreateParameter("UserId", UserId, DbType.Int64);
                DbParameter ModuleMasterIDP = provider.CreateParameter("ModuleMasterID", ModuleMasterID, DbType.Int32);
                DbParameter SubModuleMasterIDP = provider.CreateParameter("SubModuleMasterID", SubModuleMasterID, DbType.Int32);
                DbParameter DMSP = provider.CreateParameter("DMS", 2, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { userIdP, ModuleMasterIDP, SubModuleMasterIDP, DMSP };

                using (DataSet ds = provider.Select("GetDMSModuleByUserID", Params))
                {
                    if (ds != null)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (ID != Convert.ToInt32(dr["ModuleMasterID"]))
                            {
                                MA = new PModuleAccess();
                                MAs.Add(MA);
                                MA.ModuleMasterID = Convert.ToInt32(dr["ModuleMasterID"]);
                                MA.ModuleName = Convert.ToString(dr["ModuleName"]);
                                MA.ModuleAwesomeIco = Convert.ToString(dr["ModuleAwesomeIco"]);
                                MA.SubModuleAccess = new List<PSubModuleAccess>();
                                ID = Convert.ToInt32(dr["ModuleMasterID"]);
                                MA.SubModuleAccessID = Convert.ToInt32(dr["SubModuleMasterID"]);
                            }
                            MA.SubModuleAccess.Add(new PSubModuleAccess()
                            {
                                SubModuleMasterID = Convert.ToInt32(dr["SubModuleMasterID"]),
                                SubModuleName = Convert.ToString(dr["SubModuleName"]),
                                ParentMenu = Convert.ToString(dr["ParentMenu"]),
                                ModuleAction = Convert.ToString(dr["ModuleAction"]),
                                DisplayName1 = Convert.ToString(dr["DisplayName1"])
                            });
                        }
                }
                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);
                return MAs;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public List<PModuleAccess> GetDMSModuleAll()
        {
            DateTime traceStartTime = DateTime.Now;
            List<PModuleAccess> MAs = new List<PModuleAccess>();
            int ID = 0;
            PModuleAccess MA = null;
            try
            {
                using (DataSet ds = provider.Select("GetDMSModuleAll"))
                {
                    if (ds != null)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (ID != Convert.ToInt32(dr["ModuleMasterID"]))
                            {
                                MA = new PModuleAccess();
                                MAs.Add(MA);
                                MA.ModuleMasterID = Convert.ToInt32(dr["ModuleMasterID"]);
                                MA.ModuleName = Convert.ToString(dr["ModuleName"]);
                                ID = Convert.ToInt32(dr["ModuleMasterID"]);
                                MA.SubModuleAccess = new List<PSubModuleAccess>();
                            }
                            MA.SubModuleAccess.Add(new PSubModuleAccess()
                            {
                                SubModuleMasterID = Convert.ToInt32(dr["SubModuleMasterID"]),
                                SubModuleName = Convert.ToString(dr["SubModuleName"])
                            });
                        }
                }
                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);
                return MAs;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public Boolean UpdateUserPermition(long UserID, List<int> AccessModule, List<int> AccessModuleC, List<int> AccessDealer, List<int> Dashboard, List<int> MobileFeature, long CreatedBy)
        {
            List<PUser> users = new List<PUser>();
            DateTime traceStartTime = DateTime.Now;
            DataTable usersDataTable = new DataTable();
            try
            {
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                DbParameter CreatedByP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64);
                DbParameter DMSP = provider.CreateParameter("DMS", 2, DbType.Int32);

                DbParameter[] userParams = new DbParameter[3] { UserIDP, CreatedByP, DMSP };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("DeactivateUserAccess", userParams, false);

                    foreach (int SubModuleAccessID in AccessModule)
                    {
                        DbParameter UserIDMP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                        DbParameter SubModuleAccessIDP = provider.CreateParameter("SubModuleAccessID", SubModuleAccessID, DbType.Int32);
                        DbParameter CreatedByMP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64);
                        DbParameter[] MParams = new DbParameter[3] { UserIDMP, SubModuleAccessIDP, CreatedByMP };
                        provider.Insert("InsertOrUpdateUserModuleAccess", MParams, false);
                    }
                    foreach (int SubModuleChildID in AccessModuleC)
                    {
                        DbParameter UserIDMP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                        DbParameter SubModuleAccessIDP = provider.CreateParameter("SubModuleChildID", SubModuleChildID, DbType.Int32);
                        DbParameter CreatedByMP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64);
                        DbParameter[] MParams = new DbParameter[3] { UserIDMP, SubModuleAccessIDP, CreatedByMP };
                        provider.Insert("InsertOrUpdateUserSubModuleChildAccess", MParams, false);
                    }
                    foreach (int DealerID in AccessDealer)
                    {
                        DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                        DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int64);
                        DbParameter CreatedByDP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64);
                        DbParameter[] DParams = new DbParameter[3] { UserIDDP, DealerIDP, CreatedByDP };
                        provider.Insert("InsertOrUpdateUserDealerAccess", DParams, false);
                    }

                    foreach (int DashboardID in Dashboard)
                    {
                        DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                        DbParameter DashboardIDP = provider.CreateParameter("DashboardID", DashboardID, DbType.Int32);
                        DbParameter CreatedByDP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64);
                        DbParameter[] DParams = new DbParameter[3] { UserIDDP, DashboardIDP, CreatedByDP };
                        provider.Insert("InsertOrUpdateUserDashboardAccess", DParams, false);
                    }

                    foreach (int DashboardID in Dashboard)
                    {
                        DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                        DbParameter DashboardIDP = provider.CreateParameter("DashboardID", DashboardID, DbType.Int32);
                        DbParameter CreatedByDP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64);
                        DbParameter[] DParams = new DbParameter[3] { UserIDDP, DashboardIDP, CreatedByDP };
                        provider.Insert("InsertOrUpdateUserDashboardAccess", DParams, false);
                    }

                    foreach (int UserMobileFeatureID in MobileFeature)
                    {
                        DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                        DbParameter UserMobileFeatureIDP = provider.CreateParameter("UserMobileFeatureID", UserMobileFeatureID, DbType.Int32);
                        DbParameter CreatedByDP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64);
                        DbParameter[] DParams = new DbParameter[3] { UserIDDP, UserMobileFeatureIDP, CreatedByDP };
                        provider.Insert("InsertOrUpdateMUserMobileFeatureAccessAccess", DParams, false);
                    }

                    scope.Complete();
                    // This call is for track the status and logged into the trace logeer
                    TraceLogger.Log(traceStartTime);
                }
                return true;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
            return false;
        }

       
        public List<PSubModuleChild> GetSubModuleChileAll(int? SubModuleMasterID)
        {
            string endPoint = "User/SubModuleChileAll?SubModuleMasterID=" + SubModuleMasterID;
            return JsonConvert.DeserializeObject<List<PSubModuleChild>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PSubModuleChild> GetSubModuleChileByUserID(Int64 UserId)
        {
            string endPoint = "User/SubModuleChileByUserID?UserId=" + UserId;
            return JsonConvert.DeserializeObject<List<PSubModuleChild>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public PApiResult Login(UserAuthentication UserA)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("User/GetToken", UserA));
        }
        //public PUser GetUserByToken()
        //{
        //    UserAuthentication UserA = new UserAuthentication();
        //    string endPoint = "User/UserByToken";
        //    return JsonConvert.DeserializeObject<PUser>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut(endPoint, UserA)).Data));
        //}
        public PApiResult GetUserByToken()
        {
            UserAuthentication UserA = new UserAuthentication();
            string endPoint = "User/UserByToken";
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut(endPoint, UserA));
        }

        public PApiResult GetTokenByID(int userID)
        {
            string endPoint = "User/GetTokenByID?userID=" + userID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
 
        public List<PUserMobile> GetUserMobileForApproval()
        {
            string endPoint = "User/UserMobileForApproval";
            return JsonConvert.DeserializeObject<List<PUserMobile>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }
        public PApiResult ApproveUserMobile(int UserMobileID)
        {
            string endPoint = "User/ApproveUserMobile?UserMobileID=" + UserMobileID;
            return  JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public Boolean RejectUserMobile(int UserMobileID, string Remarks)
        {
            Boolean UserMobile = false;
            try
            {
                DateTime tracerStart = DateTime.Now;
                DbParameter UserMobileIDP = provider.CreateParameter("UserMobileID", UserMobileID, DbType.Int32);
                DbParameter RemarksP = provider.CreateParameter("Remarks", Remarks, DbType.String);
                DbParameter[] userParams = new DbParameter[2] { UserMobileIDP, RemarksP };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Select("RejectUserMobile", userParams);
                    UserMobile = true;
                    scope.Complete();
                }
                TraceLogger.Log(tracerStart);
                return UserMobile;
            }
            catch (LMSException lmsEx)
            {
                throw lmsEx;
            }
            catch (LMSFunctionalException lmsfExe)
            {
                throw lmsfExe;
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public List<PUserMobile> GetUserMobileManage(int? DealerID,string  FromDate, string ToDate,Boolean isActive, int? DealerDepartmentID, int? DealerDesignationID, int? EngineerUserID)
        {
            string endPoint = "User/UserMobileManage?DealerID=" + DealerID+ "&FromDate="+ FromDate + "&ToDate=" + ToDate + "&isActive=" + isActive + "&DealerDepartmentID=" + DealerDepartmentID + "&DealerDesignationID=" + DealerDesignationID + "&EngineerUserID=" + EngineerUserID;
            return JsonConvert.DeserializeObject<List<PUserMobile>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PModuleAccess> GetDMSModuleByByDealerDesignationID(Int32 DealerDesignationID)
        {
            DateTime traceStartTime = DateTime.Now;
            List<PModuleAccess> MAs = new List<PModuleAccess>();
            int ID = 0;
            PModuleAccess MA = null;
            try
            {
                DbParameter DealerDesignationIDP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int64); 
                DbParameter[] Params = new DbParameter[1] { DealerDesignationIDP };

                using (DataSet ds = provider.Select("GetDMSModuleByDealerDesignationID", Params))
                {
                    if (ds != null)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (ID != Convert.ToInt32(dr["ModuleMasterID"]))
                            {
                                MA = new PModuleAccess();
                                MAs.Add(MA);
                                MA.ModuleMasterID = Convert.ToInt32(dr["ModuleMasterID"]);
                                MA.ModuleName = Convert.ToString(dr["ModuleName"]);
                                MA.ModuleAwesomeIco = Convert.ToString(dr["ModuleAwesomeIco"]);
                                MA.SubModuleAccess = new List<PSubModuleAccess>();
                                ID = Convert.ToInt32(dr["ModuleMasterID"]);
                                MA.SubModuleAccessID = Convert.ToInt32(dr["SubModuleMasterID"]);
                            }
                            MA.SubModuleAccess.Add(new PSubModuleAccess()
                            {
                                SubModuleMasterID = Convert.ToInt32(dr["SubModuleMasterID"]),
                                SubModuleName = Convert.ToString(dr["SubModuleName"]),
                                ParentMenu = Convert.ToString(dr["ParentMenu"]),
                                ModuleAction = Convert.ToString(dr["ModuleAction"]),
                                DisplayName1 = Convert.ToString(dr["DisplayName1"])
                            });
                        }
                }
                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);
                return MAs;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public List<PSubModuleChild> GetSubModuleChileByDealerDesignationID(Int32 DealerDesignationID)
        {
            string endPoint = "User/SubModuleChileByDealerDesignationID?DealerDesignationID=" + DealerDesignationID;
            return JsonConvert.DeserializeObject<List<PSubModuleChild>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public Boolean InsertOrUpdateDefaultUserPermition(int DealerDesignationID, List<int> AccessModule, List<int> AccessModuleC, List<int> Dashboard, List<int> MobileFeature, long CreatedBy)
        {
            List<PUser> users = new List<PUser>();
            DateTime traceStartTime = DateTime.Now;
            DataTable usersDataTable = new DataTable();
            try
            {
                DbParameter DealerDesignationIDP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int64);
                DbParameter CreatedByP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64); 

                DbParameter[] userParams = new DbParameter[2] { DealerDesignationIDP, CreatedByP };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("DeactivateDefaultUserAccess", userParams, false);

                    foreach (int SubModuleAccessID in AccessModule)
                    {
                        DbParameter DealerDesignationIDMP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int64);
                        DbParameter SubModuleAccessIDP = provider.CreateParameter("SubModuleAccessID", SubModuleAccessID, DbType.Int32);
                        DbParameter CreatedByMP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64);
                        DbParameter[] MParams = new DbParameter[3] { DealerDesignationIDMP, SubModuleAccessIDP, CreatedByMP };
                        provider.Insert("InsertOrUpdateDefaultUserModuleAccess", MParams, false);
                    }
                    foreach (int SubModuleChildID in AccessModuleC)
                    {
                        DbParameter DealerDesignationIDMP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int64);
                        DbParameter SubModuleAccessIDP = provider.CreateParameter("SubModuleChildID", SubModuleChildID, DbType.Int32);
                        DbParameter CreatedByMP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64);
                        DbParameter[] MParams = new DbParameter[3] { DealerDesignationIDMP, SubModuleAccessIDP, CreatedByMP };
                        provider.Insert("InsertOrUpdateDefaultUserSubModuleChildAccess", MParams, false);
                    }

                    foreach (int DashboardID in Dashboard)
                    {
                        DbParameter DealerDesignationIDDP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int64);
                        DbParameter DashboardIDP = provider.CreateParameter("DashboardID", DashboardID, DbType.Int32);
                        DbParameter CreatedByDP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64);
                        DbParameter[] DParams = new DbParameter[3] { DealerDesignationIDDP, DashboardIDP, CreatedByDP };
                        provider.Insert("InsertOrUpdateDefaultUserDashboardAccess", DParams, false);
                    }

                    foreach (int ID in MobileFeature)
                    {
                        DbParameter DealerDesignationIDDP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int64);
                        DbParameter UserMobileFeatureIDP = provider.CreateParameter("UserMobileFeatureID", ID, DbType.Int32);
                        DbParameter CreatedByDP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64);
                        DbParameter[] DParams = new DbParameter[3] { DealerDesignationIDDP, UserMobileFeatureIDP, CreatedByDP };
                        provider.Insert("InsertOrUpdateDefaultUserMobileAccess", DParams, false);
                    }

                    scope.Complete();
                    // This call is for track the status and logged into the trace logeer
                    TraceLogger.Log(traceStartTime);
                }
                return true;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
            return false;
        }

        public DataTable GetUserLocationCurrent(int? DealerID, int? UserID, int? DealerDepartmentID, int? LoginUserID)
        {
            try
            {

                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter DealerDepartmentIDP = provider.CreateParameter("DealerDepartmentID", DealerDepartmentID, DbType.Int32);
                DbParameter LoginUserIDP = provider.CreateParameter("LoginUserID", LoginUserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, UserIDP, DealerDepartmentIDP, LoginUserIDP };

                using (DataSet ds = provider.Select("GetUserLocationCurrent", Params))
                {
                    if (ds != null)
                        return ds.Tables[0];
                }
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
            return null;
        }
        public DataTable GetUserLocationTrack(int UserID )
        {
            try
            {
                 
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32); 
                DbParameter[] Params = new DbParameter[1] {  UserIDP  };

                using (DataSet ds = provider.Select("GetUserLocationTrack", Params))
                {
                    if (ds != null)
                        return ds.Tables[0];
                }
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
            return null;
        }

        public List<PUserMobileFeature> GetUserMobileFeatureAccessByUserID(int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "User/GetUserMobileFeatureAccessByUserID?UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PUserMobileFeature>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }
        public List<PUserMobileFeature> GetUserMobileFeature()
        {
            DateTime traceStartTime = DateTime.Now;
            List<PUserMobileFeature> MAs = new List<PUserMobileFeature>();
            PUserMobileFeature MA = null;
            try
            { 

                using (DataSet ds = provider.Select("GetUserMobileFeature"))
                {
                    if (ds != null)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            MA = new PUserMobileFeature();
                            MAs.Add(MA);
                            MA.UserMobileFeatureID = Convert.ToInt32(dr["UserMobileFeatureID"]);
                            MA.FeatureName = Convert.ToString(dr["FeatureName"]);
                        }
                }
                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);
                return MAs;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public List<PSubModuleChild> GetSubModuleChildMaster(int? SubModuleMasterID,int? SubModuleChildID,string ChildName)
        {
            DateTime traceStartTime = DateTime.Now;
            List<PSubModuleChild> MAs = new List<PSubModuleChild>();
            PSubModuleChild MA = null;
            try
            {
                DbParameter SubModuleMasterIDP = provider.CreateParameter("SubModuleMasterID", SubModuleMasterID, DbType.Int32);
                DbParameter SubModuleChildIDP = provider.CreateParameter("SubModuleChildID", SubModuleChildID, DbType.Int32);
                DbParameter ChildNameP = provider.CreateParameter("ChildName", ChildName, DbType.String);
                DbParameter[] Params = new DbParameter[3] { SubModuleMasterIDP, SubModuleChildIDP, ChildNameP };

                using (DataSet ds = provider.Select("GetSubModuleChildMaster", Params))
                {
                    if (ds != null)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            MA = new PSubModuleChild();
                            MAs.Add(MA);
                            MA.SubModuleChildID = Convert.ToInt32(dr["SubModuleChildID"]);
                            MA.ChildName = Convert.ToString(dr["ChildName"]);
                        }
                }
                TraceLogger.Log(traceStartTime);
                return MAs;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public List<PSubModuleAccess> GetSubModuleMaster(int? ModuleMasterID, int? SubModuleMasterID, string SubModuleName)
        {
            DateTime traceStartTime = DateTime.Now;
            List<PSubModuleAccess> MAs = new List<PSubModuleAccess>();
            PSubModuleAccess MA = null;
            try
            {
                DbParameter ModuleMasterIDP = provider.CreateParameter("ModuleMasterID", ModuleMasterID, DbType.Int32);
                DbParameter SubModuleMasterIDP = provider.CreateParameter("SubModuleMasterID", SubModuleMasterID, DbType.Int32);
                DbParameter SubModuleNameP = provider.CreateParameter("SubModuleName", SubModuleName, DbType.String);
                DbParameter[] Params = new DbParameter[3] { ModuleMasterIDP, SubModuleMasterIDP, SubModuleNameP };

                using (DataSet ds = provider.Select("GetSubModuleMaster", Params))
                {
                    if (ds != null)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            MA = new PSubModuleAccess();
                            MAs.Add(MA);
                            MA.SubModuleMasterID = Convert.ToInt32(dr["SubModuleMasterID"]);
                            MA.SubModuleName = Convert.ToString(dr["SubModuleName"]);
                        }
                }
                TraceLogger.Log(traceStartTime);
                return MAs;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public List<PModuleAccess> GetModuleMaster(int? ModuleMasterID, string ModuleName)
        {
            DateTime traceStartTime = DateTime.Now;
            List<PModuleAccess> MAs = new List<PModuleAccess>();
            PModuleAccess MA = null;
            try
            {
                DbParameter ModuleMasterIDP = provider.CreateParameter("ModuleMasterID", ModuleMasterID, DbType.Int32);
                DbParameter ModuleNameP = provider.CreateParameter("ModuleName", ModuleName, DbType.String);
                DbParameter[] Params = new DbParameter[2] { ModuleMasterIDP, ModuleNameP };
                using (DataSet ds = provider.Select("GetModuleMaster", Params))
                {
                    if (ds != null)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            MA = new PModuleAccess();
                            MAs.Add(MA);
                            MA.ModuleMasterID = Convert.ToInt32(dr["ModuleMasterID"]);
                            MA.ModuleName = Convert.ToString(dr["ModuleName"]);
                        }
                }
                TraceLogger.Log(traceStartTime);
                return MAs;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public List<PDealerUserPermission> GetUserByDealerIDs(string DealerID, int? DepartmentID, int? DesignationID)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDealerUserPermission> Users = new List<PDealerUserPermission>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter DepartmentIDP = provider.CreateParameter("DepartmentID", DepartmentID, DbType.Int32);
                DbParameter DesignationIDP = provider.CreateParameter("DesignationID", DesignationID, DbType.Int32);
                DbParameter[] Params = new DbParameter[3] { DealerIDP, DepartmentIDP, DesignationIDP };
                using (DataSet ds = provider.Select("GetUserByDealerIDs", Params))
                {
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables[0];

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PDealerUserPermission User = new PDealerUserPermission();
                            User.DealerID = Convert.ToInt32(dr["DID"]);
                            User.UserName = Convert.ToString(dr["UserName"]);
                            User.DealerCode = Convert.ToString(dr["DealerCode"]);
                            User.ContactName = Convert.ToString(dr["ContactName"]);
                            User.CodeWithName = Convert.ToString(dr["DealerCode"]) + "-" + Convert.ToString(dr["DisplayName"]);
                            User.MailID = Convert.ToString(dr["MailID"]);
                            User.DealerDesignation = Convert.ToString(dr["DealerDesignation"]);
                            User.DealerDepartment = Convert.ToString(dr["DealerDepartment"]);
                            Users.Add(User);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDealer", "GetUserByDealerIDs", ex);
                throw ex;
            }
            return Users;
        }
        public List<PDealerUserPermission> GetUserByModulePermissions(int? DealerID, int? DepartmentID, int? DesignationID, int? ModuleMasterID, int? SubModuleMasterID, int? SubModuleChildID)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDealerUserPermission> Users = new List<PDealerUserPermission>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DepartmentIDP = provider.CreateParameter("DepartmentID", DepartmentID, DbType.Int32);
                DbParameter DesignationIDP = provider.CreateParameter("DesignationID", DesignationID, DbType.Int32);
                DbParameter ModuleMasterIDP = provider.CreateParameter("ModuleMasterID", ModuleMasterID, DbType.Int32);
                DbParameter SubModuleMasterIDP = provider.CreateParameter("SubModuleMasterID", SubModuleMasterID, DbType.Int32);
                DbParameter SubModuleChildIDP = provider.CreateParameter("SubModuleChildID", SubModuleChildID, DbType.Int32);


                DbParameter[] Params = new DbParameter[6] { DealerIDP, DepartmentIDP, DesignationIDP, ModuleMasterIDP, SubModuleMasterIDP, SubModuleChildIDP };
                using (DataSet ds = provider.Select("GetUserByModulePermission", Params))
                {
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables[0];

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PDealerUserPermission User = new PDealerUserPermission();
                            User.DealerID = Convert.ToInt32(dr["DID"]);
                            User.UserName = Convert.ToString(dr["UserName"]);
                            User.DealerCode = Convert.ToString(dr["DealerCode"]);
                            User.ContactName = Convert.ToString(dr["ContactName"]);
                            User.CodeWithName = Convert.ToString(dr["DealerCode"]) + "-" + Convert.ToString(dr["DisplayName"]);
                            User.MailID = Convert.ToString(dr["MailID"]);
                            User.DealerDesignation = Convert.ToString(dr["DealerDesignation"]);
                            User.DealerDepartment = Convert.ToString(dr["DealerDepartment"]);
                            User.ModuleName = Convert.ToString(dr["ModuleName"]);
                            User.SubModuleName = Convert.ToString(dr["SubModuleName"]);
                            User.ChildName = Convert.ToString(dr["ChildName"]);
                            Users.Add(User);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDealer", "GetUserByDealerIDs", ex);
                throw ex;
            }
            return Users;
        }

        public Boolean UpdateUserResetPasswordByAdmin(long UserID, long UpdatedBy)
        {
            List<PUser> users = new List<PUser>();
            DateTime traceStartTime = DateTime.Now;
            DataTable usersDataTable = new DataTable();
            try
            {
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                DbParameter UpdatedByP = provider.CreateParameter("UpdatedBy", UpdatedBy, DbType.Int64);

                DbParameter[] userParams = new DbParameter[2] { UserIDP , UpdatedByP };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("UpdateUserResetPasswordByAdmin", userParams, false);
                    scope.Complete();
                    // This call is for track the status and logged into the trace logeer
                    TraceLogger.Log(traceStartTime);
                }
                return true;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
            return false;
        }

        public List<PUserMobileFeature> GetMobileFeatureByDealerDesignationID(Int32 DealerDesignationID)
        {
            DateTime traceStartTime = DateTime.Now;
            List<PUserMobileFeature> MAs = new List<PUserMobileFeature>();
            int ID = 0;
            PUserMobileFeature MA = null;
            try
            {
                DbParameter DealerDesignationIDP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { DealerDesignationIDP };

                using (DataSet ds = provider.Select("GetMobileFeatureByDealerDesignationID", Params))
                {
                    if (ds != null)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            MA = new PUserMobileFeature();
                            MAs.Add(MA);
                            MA.UserMobileFeatureID = Convert.ToInt32(dr["UserMobileFeatureID"]);
                            MA.FeatureName = Convert.ToString(dr["FeatureName"]);
                        }
                }
                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);
                return MAs;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
    }
}