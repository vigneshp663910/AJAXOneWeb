using DataAccess;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business
{
    public class BMessageAnnouncement
    {
        private IDataAccess provider;
        public BMessageAnnouncement()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PMessageAnnouncement> GetMessageAnnouncement(long? MessageAnnouncementId, int? DealerID, int? DealerDepartmentID, int? DealerDesignationID, int? DealerEmployeeID)
        {
            List<PMessageAnnouncement> PMAs = new List<PMessageAnnouncement>();
            PMessageAnnouncement PMA = null;
            DateTime traceStartTime = DateTime.Now;
            try
            {
                DbParameter MessageAnnouncementIdP = provider.CreateParameter("MessageAnnouncementId", MessageAnnouncementId, DbType.Int64);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DealerDepartmentIDP = provider.CreateParameter("DealerDepartmentID", DealerDepartmentID, DbType.Int32);
                DbParameter DealerDesignationIDP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int32);
                DbParameter DealerEmployeeIDP = provider.CreateParameter("DealerEmployeeID", DealerEmployeeID, DbType.Int32);

                DbParameter[] userParams = new DbParameter[5] { MessageAnnouncementIdP, DealerIDP, DealerDepartmentIDP, DealerDesignationIDP, DealerEmployeeIDP };

                using (DataSet usersDataSet = provider.Select("GetMessageAnnouncement", userParams))
                {
                    if (usersDataSet != null)
                        foreach (DataRow dr in usersDataSet.Tables[0].Rows)
                        {
                            PMA = new PMessageAnnouncement();
                            PMA.MessageAnnouncementId = Convert.ToInt64(dr["MessageAnnouncementId"]);
                            PMA.AssignTo = new PUser() { UserID = Convert.ToInt32(dr["AssignTo"]), ContactName = Convert.ToString(dr["AssignToName"]) };
                            PMA.Message = Convert.ToString(dr["Message"]);
                            PMA.CreatedBy = new PUser() { UserID = Convert.ToInt32(dr["CreatedBy"]), ContactName = Convert.ToString(dr["CreatedByName"]) };
                            PMA.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                            PMA.NotificationNumber = Convert.ToInt64(dr["NotificationNumber"]);
                            PMAs.Add(PMA);
                        }
                }
                TraceLogger.Log(traceStartTime);
                return PMAs;
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

        public List<PMessageAnnouncement> GetUsersForMessageAnnouncement(long? UserID, string UserName, int? UserTypeID, string ExternalReferenceID, int? DealerID, bool? IsEnabled, string ContactName, int? DealerDepartmentID, int? DealerDesignationID)
        {
            List<PMessageAnnouncement> users = new List<PMessageAnnouncement>();
            DateTime traceStartTime = DateTime.Now;
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
                DbParameter ContactNameP = provider.CreateParameter("ContactName", ContactName, DbType.String);

                DbParameter DealerDepartmentIDP = provider.CreateParameter("DealerDepartmentID", DealerDepartmentID, DbType.Int32);
                DbParameter DealerDesignationIDP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int32);

                DbParameter[] userParams = new DbParameter[9] { UserIDP, UserNameP, UserTypeIDP, ExternalReferenceIDP, DealerIDP, IsEnabledP, ContactNameP, DealerDepartmentIDP, DealerDesignationIDP };

                using (DataSet usersDataSet = provider.Select("GetUsersForMessageAnnouncement", userParams))
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
        private PMessageAnnouncement ConvertToUserVO(DataRow userRow)
        {
            return new PMessageAnnouncement()
            {
                AssignTo = new PUser()
                {
                    UserID = Convert.ToInt32(userRow["UserID"]),
                    ContactName = Convert.ToString(userRow["ContactName"]),
                    ExternalReferenceID = userRow["ExternalReferenceID"] != DBNull.Value ? Convert.ToString(userRow["ExternalReferenceID"]) : string.Empty,
                    Department = new PDMS_DealerDepartment() { DealerDepartmentID = Convert.ToInt32(userRow["DealerDepartmentID"]), DealerDepartment = Convert.ToString(userRow["DealerDepartment"]) },
                    Designation = new PDMS_DealerDesignation() { DealerDesignationID = Convert.ToInt32(userRow["DealerDesignationID"]), DealerDesignation = Convert.ToString(userRow["DealerDesignation"]) },
                    Mail = Convert.ToString(userRow["Mail"]),
                },
                Dealer = new PDealer()
                {
                    DID = Convert.ToInt32(userRow["DealerID"]),
                    DealerCode = Convert.ToString(userRow["DealerCode"]),
                    CodeWithDisplayName = Convert.ToString(userRow["DealerCode"]) + " " + Convert.ToString(userRow["DisplayName"]),
                },
            };
        }
        public long InsertMessageAnnouncement(PMessageAnnouncement PMA, Int64 UserID)
        {
            TraceLogger.Log(DateTime.Now);
            long success = 0;
            try
            {
                DbParameter AssignToP = provider.CreateParameter("AssignTo", PMA.AssignTo.UserID, DbType.Int64);
                DbParameter MessageP = provider.CreateParameter("Message", PMA.Message, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                DbParameter NotificationNumberP = provider.CreateParameter("NotificationNumber", PMA.NotificationNumber, DbType.Int64);
                DbParameter[] Params = new DbParameter[4] { AssignToP, MessageP, UserIDP, NotificationNumberP };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("InsertMessageAnnouncement", Params);
                    scope.Complete();
                }
                success = 1;
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("BMessageAnnouncement", "InsertMessageAnnouncement", e1);
                throw e1;
            }
            TraceLogger.Log(DateTime.Now);
            return success;
        }
        public long GetMaxNoMessageAnnouncement()
        {
            TraceLogger.Log(DateTime.Now);
            long Success = 0;
            long NotificationNumber = 0;
            try
            {
                DbParameter OutValue = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                DbParameter[] Params = new DbParameter[1] { OutValue };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    Success = provider.Insert("GetMaxNoMessageAnnouncement", Params);
                    if (Success != 0)
                    {
                        NotificationNumber = Convert.ToInt64(OutValue.Value);
                    }
                    scope.Complete();
                }
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("BMessageAnnouncement", "GetMaxNoMessageAnnouncement", e1);
                throw e1;
            }
            TraceLogger.Log(DateTime.Now);
            return NotificationNumber;
        }
    }
}
