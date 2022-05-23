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
        /// <summary>
        /// This method  will call the user login authenticate details from datbase and validate the user.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns>It returns the valid integer value</returns>
        public PUser AuthenticateUser(string userName, string password)
        {
            PUser userAuthDetails = new PUser();
            try
            {
                DateTime tracerStart = DateTime.Now;
                using (DataTable userData = AuthenticateUserByUserNameOrMobileOrEmail(userName, password))
                {
                    if (userData.Rows.Count > 0)
                        userAuthDetails = ConvertToUser(userData.Rows[0]);
                }
                TraceLogger.Log(tracerStart);
                ValidateAuthenication(userAuthDetails, password);
                return userAuthDetails;
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

        public DataTable AuthenticateUserByUserNameOrMobileOrEmail(string userName, String password)
        {
            try
            {
                DateTime traceStartTime = DateTime.Now;
                DataTable userDataTable = new DataTable();

                DbParameter userIDParams = provider.CreateParameter("UserName", userName, DbType.String);
                DbParameter passwordP = provider.CreateParameter("Password", LMSHelper.EncodeString(password), DbType.String);
                DbParameter[] userParams = new DbParameter[2] { userIDParams, passwordP };

                using (DataSet userDataSet = provider.Select("AuthenticateUserByUserNameOrMobileOrEmail", userParams))
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
        /// <summary>
        /// This method will call the GetLoginUserDetails method in DAO layer for getting user details
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>it returns userdetails list</returns>
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

        /// <summary>
        ///  This method will call the GetAllUsers method in DAO layer for getting all users.
        /// </summary>
        /// <returns></returns>
        //public List<PUser> GetAllUsers(string contactName = "", string userName = "")
        //{
        //    List<PUser> users = new List<PUser>();
        //    try
        //    {
        //        DateTime traceStartTime = DateTime.Now;
        //        using (DataTable userData = GetAllUsers1(contactName, userName))
        //        {
        //            foreach (DataRow usersRow in userData.Rows)
        //                users.Add(ConvertToUserVO(usersRow));
        //        }
        //        TraceLogger.Log(traceStartTime);
        //        return users;
        //    }
        //    catch (LMSException lmsEx)
        //    {
        //        throw lmsEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new LMSException(ErrorCode.GENE, ex);
        //    }
        //}
        public List<PUser> GetAllUsers(string contactName = "", string userName = "")
        {
            List<PUser> users = new List<PUser>();
            DateTime traceStartTime = DateTime.Now;
            DataTable usersDataTable = new DataTable();
            try
            {
                DbParameter ContactNameParams, UserNameParams;
                ContactNameParams = provider.CreateParameter("ContactName", contactName, DbType.String);
                UserNameParams = provider.CreateParameter("UserName", userName, DbType.String);
                DbParameter[] userParams = new DbParameter[2] { ContactNameParams, UserNameParams };

                using (DataSet usersDataSet = provider.Select("GetAllUsers", userParams))
                {
                    if (usersDataSet != null)
                        foreach (DataRow usersRow in usersDataSet.Tables[0].Rows)
                            users.Add(ConvertToUserVO(usersRow));
                }
                // This call is for track the status and logged into the trace logeer
                TraceLogger.Log(traceStartTime);
                return users;
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
        public List<PUser> GetUsers(long? UserID, string UserName, int? UserTypeID, string ExternalReferenceID,int? DealerID,bool? IsEnabled,string ContactName, int? DealerDepartmentID, int? DealerDesignationID)
        {
            string endPoint = "User/GetUsers?UserID=" + UserID + "&UserName=" + UserName + "&UserTypeID=" + UserTypeID + "&ExternalReferenceID=" + ExternalReferenceID + "&DealerID=" + DealerID
                 + "&IsEnabled=" + IsEnabled + "&ContactName=" + ContactName + "&DealerDepartmentID=" + DealerDepartmentID + "&DealerDesignationID=" + DealerDesignationID;
            return JsonConvert.DeserializeObject<List<PUser>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        /// <summary>
        /// This method is used to call UpdateUser method from DAO layer 
        /// to update lock for the selected user.
        /// </summary>
        /// <param name="user"></param>
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

        /// <summary>
        /// This method is used to call UpdateUser method from DAO layer
        /// to update enable/disable and enable/disable reason for the selected user.
        /// </summary>
        /// <param name="user"></param>
        public void EnableDisableUser(PUser user)
        {
            try
            {
                DateTime tracerStartTime = DateTime.Now;
                UpdateUser(user);
                string emailId = GetContactDetailByUser(user.UserID).EmailID;
                if (user.IsEnabled)
                {
                    new LMSMessaging().SendEnableUserMail(user, emailId);
                }
                else
                {
                    new LMSMessaging().SendDisableUserMail(user, emailId);
                }
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

        /// <summary>
        ///  This method is used to call GetContactDetailsByUser method in DAO layer for getting contact details
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public long AuthorizeUser(PUser user, PAccount account)
        {
            int userId = 0;
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    // userId = CreateUser(user);
                    user.UserID = userId;
                    //foreach (PPlant plant in user.UserPlants)
                    //{
                    //    CreateUserPlantMapping(PlantVOtoUserPlantMappingDAO(plant, user));
                    //}
                    //foreach (PUserModuleAccess module in user.AccessModules)
                    //{
                    //    CreateuserModuleAccess(ModuleAccessVOtoModuleAccessDAO(module, user));
                    //}
                    user.ContactDetail.UserID = userId;
                    CreateContactDetails(user.ContactDetail);
                    UpdateAccount(account);
                    scope.Complete();
                }
                //new lmsMessaging().SendAuthorizationMail(account);
                return userId;
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
        public void UpdateAccount(PAccount account)
        {
            try
            {
                UpdateAccountDetails(account);
            }
            catch (LMSException vpEx)
            {
                throw vpEx;
            }
        }
        public long UpdateAccountDetails(PAccount accountDAO)
        {
            DateTime traceStartTime = DateTime.Now;
            long accountId = 0;
            try
            {
                DbParameter accountIDParam = provider.CreateParameter("AccountID", accountDAO.AccountID, DbType.Int64);
                DbParameter stateParam = provider.CreateParameter("State", accountDAO.State, DbType.String);
                DbParameter cityParam = provider.CreateParameter("City", accountDAO.City, DbType.String);
                DbParameter countryParam = provider.CreateParameter("Country", accountDAO.Country, DbType.String);
                DbParameter emailIDParam = provider.CreateParameter("EmailID", accountDAO.EmailID, DbType.String);
                DbParameter systemIDParam = provider.CreateParameter("SystemID", accountDAO.SystemID, DbType.Int16);
                DbParameter postcodeParam = provider.CreateParameter("Postcode", accountDAO.Postcode, DbType.String);
                DbParameter userNameParam = provider.CreateParameter("UserName", accountDAO.UserName, DbType.String);
                DbParameter createdByParam = provider.CreateParameter("CreatedBy", accountDAO.CreatedBy, DbType.Int64);
                DbParameter staffNameParam = provider.CreateParameter("StaffName", accountDAO.StaffName, DbType.String);
                DbParameter userTypeIDParam = provider.CreateParameter("UserTypeID", accountDAO.UserTypeID, DbType.Byte);
                DbParameter contactNameParam = provider.CreateParameter("ContactName", accountDAO.ContactName, DbType.String);
                DbParameter loginPasswordParam = provider.CreateParameter("LoginPassword", accountDAO.LoginPassword, DbType.String);
                DbParameter contactPhoneParam = provider.CreateParameter("ContactPhone", accountDAO.ContactPhone, DbType.String);
                DbParameter addressLine1Param = provider.CreateParameter("AddressLine1", accountDAO.AddressLine1, DbType.String);
                DbParameter addressLine2Param = provider.CreateParameter("AddressLine2", accountDAO.AddressLine2, DbType.String);
                DbParameter addressLine3Param = provider.CreateParameter("AddressLine3", accountDAO.AddressLine3, DbType.String);
                DbParameter IsAuthorizedParam = provider.CreateParameter("IsAuthorized", accountDAO.IsAuthorized, DbType.Boolean);
                DbParameter DeclineReasonParam;
                if (!string.IsNullOrEmpty(accountDAO.DeclineReason))
                    DeclineReasonParam = provider.CreateParameter("DeclineReason", accountDAO.DeclineReason, DbType.String);
                else
                    DeclineReasonParam = provider.CreateParameter("DeclineReason", DBNull.Value, DbType.String);
                DbParameter UpdatedByParam = provider.CreateParameter("UpdatedBy", accountDAO.UpdatedBy, DbType.Int64);
                DbParameter CreatedOnParam = provider.CreateParameter("CreatedOn", accountDAO.CreatedOn, DbType.DateTime);
                DbParameter UpdatedOnParam = provider.CreateParameter("UpdatedOn", accountDAO.UpdatedOn, DbType.DateTime);
                DbParameter externalReferenceIDParam = provider.CreateParameter("ExternalReferenceID", accountDAO.ExternalReferenceID, DbType.String);
                DbParameter[] accountParams = new DbParameter[23] { accountIDParam,staffNameParam, userNameParam, loginPasswordParam, userTypeIDParam,
                                                           externalReferenceIDParam,emailIDParam,contactNameParam,
                                                           contactPhoneParam,addressLine1Param,addressLine2Param,addressLine3Param,IsAuthorizedParam,DeclineReasonParam,
                                                           cityParam,stateParam,postcodeParam,countryParam,systemIDParam,createdByParam,UpdatedByParam,CreatedOnParam,UpdatedOnParam};
                accountId = provider.Update("UpdateAccountDetails", accountParams);
                TraceLogger.Log(traceStartTime);
                return accountId;
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
        public void CreateContactDetails(PContactDetail contactDetailsDAO)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                DbParameter userIDParam = provider.CreateParameter("UserID", contactDetailsDAO.UserID, DbType.Int64);
                DbParameter emailIDParam = provider.CreateParameter("EmailID", contactDetailsDAO.EmailID, DbType.String);
                DbParameter phoneParam = provider.CreateParameter("Phone", contactDetailsDAO.Phone, DbType.String);
                DbParameter addressLine1Param = provider.CreateParameter("AddressLine1", contactDetailsDAO.AddressLine1, DbType.String);
                DbParameter addressLine2Param = provider.CreateParameter("AddressLine2", contactDetailsDAO.AddressLine2, DbType.String);
                DbParameter addressLine3Param = provider.CreateParameter("AddressLine3", contactDetailsDAO.AddressLine3, DbType.String);
                DbParameter cityParam = provider.CreateParameter("City", contactDetailsDAO.City, DbType.String);
                DbParameter stateParam = provider.CreateParameter("State", contactDetailsDAO.State, DbType.String);
                DbParameter postcodeParam = provider.CreateParameter("Postcode", contactDetailsDAO.Postcode, DbType.String);
                DbParameter countryParam = provider.CreateParameter("Country", contactDetailsDAO.Country, DbType.String);
                DbParameter[] contactDetailParams = new DbParameter[10] { userIDParam, emailIDParam, phoneParam, addressLine1Param, addressLine2Param, addressLine3Param,
                                                                    cityParam, stateParam, postcodeParam, countryParam};
                provider.Insert("CreateContactDetails", contactDetailParams);
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

        private PModuleAccess ModuleAccessVOtoModuleAccessDAO(PUserModuleAccess moduleAccess, PUser user)
        {
            return new PModuleAccess()
            {

                UserID = user.UserID,
                SubModuleAccessID = moduleAccess.SubModuleAccessId,
                ModuleAccessID = moduleAccess.ModuleAccessID,
                IsActive = moduleAccess.IsActive,
                CreatedBy = user.CreatedBy,
                CreatedOn = user.CreatedOn,
                UpdatedBy = user.UpdatedBy,
                UpdatedOn = user.UpdatedOn
            };
        }
        public void CreateuserModuleAccess(PModuleAccess moduleAccess)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                DbParameter userIDParam = provider.CreateParameter("UserID", moduleAccess.UserID, DbType.Int64);
                DbParameter moduleAccessIDParam = provider.CreateParameter("ModuleAccessID", moduleAccess.ModuleAccessID, DbType.Int32);
                DbParameter subModuleAccessIDParam = provider.CreateParameter("SubModuleAccessID", moduleAccess.SubModuleAccessID, DbType.Int32);
                DbParameter isActiveParam = provider.CreateParameter("IsActive", moduleAccess.IsActive, DbType.Boolean);
                DbParameter createdByParam = provider.CreateParameter("CreatedBy", moduleAccess.CreatedBy, DbType.Int64);
                DbParameter updatedByParam = provider.CreateParameter("UpdatedBy", moduleAccess.UpdatedBy, DbType.Int64);
                DbParameter createdOnParam = provider.CreateParameter("CreatedOn", moduleAccess.CreatedOn, DbType.DateTime);
                DbParameter updatedOnParam = provider.CreateParameter("UpdatedOn", moduleAccess.UpdatedOn, DbType.DateTime);
                DbParameter[] contactDetailParams = new DbParameter[8] { userIDParam, moduleAccessIDParam, subModuleAccessIDParam, isActiveParam,
                                                    createdByParam, updatedByParam, createdOnParam,updatedOnParam };
                provider.Insert("CreateUpdateUserModuleAccess", contactDetailParams);
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
        private PUserPlantMapping PlantVOtoUserPlantMappingDAO(PPlant plant, PUser user)
        {
            return new PUserPlantMapping()
            {
                PlantID = plant.PlantID,
                UserID = user.UserID,
                IsActive = plant.IsActive,
                CreatedBy = user.CreatedBy,
                CreatedOn = user.CreatedOn,
                UpdatedBy = user.UpdatedBy,
                UpdatedOn = user.UpdatedOn
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
                DbParameter[] userParams = new DbParameter[20] { contactNameParam, userNameParam, loginPasswordParam, userTypeIDParam, externalReferenceIDParam,
                                                                     isFirstTimeParam, isLockedParam, isEnabledParam,ajaxOne, passwordExpirationDateParam,
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

        public void CreateUserPlantMapping(PUserPlantMapping mapping)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                DbParameter UserId = provider.CreateParameter("UserId", mapping.UserID, DbType.Int64);
                DbParameter PlantId = provider.CreateParameter("PlantId", mapping.PlantID, DbType.Int16);
                DbParameter IsActive = provider.CreateParameter("IsActive", mapping.IsActive, DbType.Boolean);
                DbParameter CreatedBy = provider.CreateParameter("CreatedBy", mapping.CreatedBy, DbType.Int64);
                DbParameter CreatedOn = provider.CreateParameter("CreatedOn", mapping.CreatedOn, DbType.DateTime);
                DbParameter UpdatedBy = provider.CreateParameter("UpdatedBy", mapping.UpdatedBy, DbType.Int64);
                DbParameter UpdatedOn = provider.CreateParameter("UpdatedOn", mapping.UpdatedOn, DbType.DateTime);
                DbParameter[] accountParams = new DbParameter[7] { UserId, PlantId, IsActive, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn };
                provider.Insert("CreateUpdateUserPlantMappings", accountParams);
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
        /// This method is used to call UpdateUserPlantMapping method to activate the existing plants for the selected user
        /// call CreateUserPlantMapping method to add new plants for the user.
        /// call UpdateUserModuleAccess method to update access rights  to the user for selected modules 
        /// call CreateuserModuleAccess method to add new modules for the user.
        /// </summary>
        /// <param name="userVO"></param>
        public void UpdateUserDetail(PUser userVO)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //foreach (PPlant plant in userVO.UserPlants)
                    //{
                    //    CreateUserPlantMapping(PlantVOtoUserPlantMapping(plant, userVO));
                    //}
                    //foreach (PUserModuleAccess moduleAccess in userVO.AccessModules)
                    //{

                    //    CreateuserModuleAccess(ModuleAccessVOtoModuleAccessDAO(moduleAccess, userVO));

                    //}

                    scope.Complete();
                }
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
        private PUserPlantMapping PlantVOtoUserPlantMapping(PPlant plant, PUser user)
        {
            return new PUserPlantMapping()
            {
                PlantID = plant.PlantID,
                UserID = user.UserID,
                IsActive = plant.IsActive,
                CreatedBy = user.CreatedBy,
                CreatedOn = user.CreatedOn,
                UpdatedBy = user.UpdatedBy,
                UpdatedOn = user.UpdatedOn
            };
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
        public List<PModuleAccess> GetDMSModuleByUser(Int64 UserId, int? ModuleMasterID, int? SubModuleMasterID)
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
        public Boolean UpdateUserPermition(long UserID, List<int> AccessModule, List<int> AccessModuleC, List<int> AccessDealer, List<int> Dashboard, long CreatedBy)
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

        public List<PUser> GetDMSUserBySubModuleAccessID(Int64 SubModuleAccessID)
        {
            DateTime traceStartTime = DateTime.Now;
            List<PUser> users = new List<PUser>();

            try
            {
                DbParameter SubModuleAccessIDP = provider.CreateParameter("SubModuleAccessID", SubModuleAccessID, DbType.Int64);
                DbParameter[] contactDetailParams = new DbParameter[1] { SubModuleAccessIDP };
                using (DataSet ds = provider.Select("GetDMSUserBySubModuleAccessID", contactDetailParams))
                {
                    if (ds != null)
                        foreach (DataRow usersRow in ds.Tables[0].Rows)
                            users.Add(ConvertToUserVO(usersRow));
                }
                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);
                return users;
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
        public PUser GetUserByToken()
        {
            UserAuthentication UserA = new UserAuthentication();
            string endPoint = "User/UserByToken";
            return JsonConvert.DeserializeObject<PUser>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut(endPoint, UserA)).Data));
        }

        public PApiResult GetTokenByID(int userID)
        {
            string endPoint = "User/GetTokenByID?userID=" + userID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }


        public PUserMobile GetUserIDByIMEI(string IMEI)
        {
            PUserMobile UserMobile = null;
            try
            {
                DateTime tracerStart = DateTime.Now;
                DbParameter IMEIP = provider.CreateParameter("IMEI", IMEI, DbType.String);
                DbParameter[] userParams = new DbParameter[1] { IMEIP };

                using (DataSet userDataSet = provider.Select("GetUserIDByIMEI", userParams))
                {
                    if (userDataSet != null)
                        foreach (DataRow dr in userDataSet.Tables[0].Rows)
                        {
                            UserMobile = new PUserMobile();
                            UserMobile.UserMobileID = Convert.ToInt32(dr["UserMobileID"]);
                            UserMobile.UserID = Convert.ToInt32(dr["UserID"]);
                            //UserMobile.IMEI = Convert.ToString(dr["IMEI"]);
                            UserMobile.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                            UserMobile.ApprovedBy = DBNull.Value == dr["ApprovedBy"] ? (int?)null : Convert.ToInt32(dr["ApprovedBy"]);
                            UserMobile.ApprovedOn = DBNull.Value == dr["ApprovedOn"] ? (DateTime?)null : Convert.ToDateTime(dr["ApprovedOn"]);
                            UserMobile.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        }
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
        //public Boolean InserUserMobileIMEI(int UserID, string IMEI)
        //{
        //    Boolean UserMobile = false;
        //    try
        //    {
        //        DateTime tracerStart = DateTime.Now;
        //        DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //        DbParameter IMEIP = provider.CreateParameter("IMEI", IMEI, DbType.String);
        //        DbParameter[] userParams = new DbParameter[2] { UserIDP, IMEIP };
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            provider.Select("InserUserMobileIMEI", userParams);
        //            UserMobile = true;
        //            scope.Complete();
        //        }

        //        TraceLogger.Log(tracerStart);
        //        return UserMobile;
        //    }
        //    catch (LMSException lmsEx)
        //    {
        //        throw lmsEx;
        //    }
        //    catch (LMSFunctionalException lmsfExe)
        //    {
        //        throw lmsfExe;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new LMSException(ErrorCode.GENE, ex);
        //    }
        //}
        public List<PUserMobile> GetUserMobileManage(int? DealerID,string  FromDate, string ToDate)
        {
            string endPoint = "User/UserMobileManage?DealerID=" + DealerID+ "&FromDate="+ FromDate + "&ToDate=" + ToDate;
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
        public Boolean InsertOrUpdateDefaultUserPermition(int DealerDesignationID, List<int> AccessModule, List<int> AccessModuleC, List<int> Dashboard, long CreatedBy)
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
    }
}
