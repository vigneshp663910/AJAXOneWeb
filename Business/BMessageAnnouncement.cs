using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public List<PMessageAnnouncementHeader> GetMessageAnnouncementHeader(long? MessageAnnouncementHeaderID, int? DealerID, int? DealerDepartmentID, int? DealerDesignationID, int? DealerEmployeeID, bool? ReadStatus, string LoginEntryDate)
        {
            string endPoint = "MessageNotification/GetMessageAnnouncementHeader?MessageAnnouncementHeaderID=" + MessageAnnouncementHeaderID + "&DealerID=" + DealerID + "&DealerDepartmentID=" + DealerDepartmentID + "&DealerDesignationID=" + DealerDesignationID + "&DealerEmployeeID=" + DealerEmployeeID + "&ReadStatus=" + ReadStatus + "&LoginEntryDate=" + LoginEntryDate;
            return JsonConvert.DeserializeObject<List<PMessageAnnouncementHeader>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PMessageAnnouncementHeader GetMessageAnnouncementHeaderByID(long? MessageAnnouncementHeaderID)
        {
            string endPoint = "MessageNotification/GetMessageAnnouncementHeaderByID?MessageAnnouncementHeaderID=" + MessageAnnouncementHeaderID;
            return JsonConvert.DeserializeObject<PMessageAnnouncementHeader>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PMessageAnnouncementItem> GetUsersForMessageAnnouncement(long? UserID, string UserName, int? UserTypeID, string ExternalReferenceID, int? DealerID, bool? IsEnabled, string ContactName, int? DealerDepartmentID, int? DealerDesignationID)
        {
            string endPoint = "MessageNotification/GetUsersForMessageAnnouncement?UserID=" + UserID + "&UserName=" + UserName + "&UserTypeID=" + UserTypeID + "&ExternalReferenceID=" + ExternalReferenceID + "&DealerID=" + DealerID + "&IsEnabled=" + IsEnabled + "&ContactName=" + ContactName + "&DealerDepartmentID=" + DealerDepartmentID + "&DealerDesignationID=" + DealerDesignationID;
            return JsonConvert.DeserializeObject<List<PMessageAnnouncementItem>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public long InsertMessageAnnouncement(PMessageAnnouncementHeader Msg)
        {
            TraceLogger.Log(DateTime.Now);
            long success = 0;
            long MessageAnnouncementHeaderID = 0;
            try
            {
                DbParameter MessageP = provider.CreateParameter("Message", Msg.Message, DbType.String);
                DbParameter ValidFromP = provider.CreateParameter("ValidFrom", Msg.ValidFrom, DbType.DateTime);
                DbParameter ValidToP = provider.CreateParameter("ValidTo", Msg.ValidTo, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", Convert.ToInt64(Msg.CreatedBy.UserID), DbType.Int64);
                DbParameter TicketIDParam = provider.CreateParameter("OutValue", "", DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                DbParameter[] Params = new DbParameter[5] { MessageP, ValidFromP, ValidToP, UserIDP, TicketIDParam };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("InsertMessageAnnouncementHeader", Params);

                    if (success != 0)
                    {
                        MessageAnnouncementHeaderID = Convert.ToInt64(TicketIDParam.Value);
                        foreach (PMessageAnnouncementItem ss in Msg.Item)
                        {
                            if (ss.AssignTo.Department.DealerDepartment != "Top Management")
                            {
                                if (ss.MailResponce == true)
                                {
                                    DbParameter MessageAnnouncementHeaderIDP = provider.CreateParameter("MessageAnnouncementHeaderID", MessageAnnouncementHeaderID, DbType.Int64);
                                    DbParameter AssignToP = provider.CreateParameter("AssignTo", ss.AssignTo.UserID, DbType.Int64);
                                    DbParameter ReadStatusP = provider.CreateParameter("ReadStatus", false, DbType.Boolean);
                                    DbParameter[] ParamsItem = new DbParameter[3] { MessageAnnouncementHeaderIDP, AssignToP, ReadStatusP };
                                    success = provider.Insert("InsertMessageAnnouncementItem", ParamsItem);
                                }
                            }
                        }
                    }
                    scope.Complete();
                }
                success = (success != 0) ? MessageAnnouncementHeaderID : success;
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("BMessageAnnouncement", "InsertMessageAnnouncement", e1);
                throw e1;
            }
            TraceLogger.Log(DateTime.Now);
            return success;
        }
        public long UpdateMessageAnnouncementItemReadStatus(Int64 AssignTo,bool ReadStatus)
        {
            TraceLogger.Log(DateTime.Now);
            long success = 0;
            try
            {
                DbParameter AssignToP = provider.CreateParameter("AssignTo", AssignTo, DbType.Int64);
                DbParameter ReadStatusP = provider.CreateParameter("ReadStatus", ReadStatus, DbType.Boolean);
                DbParameter[] Params = new DbParameter[2] { AssignToP, ReadStatusP };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateMessageAnnouncementItemReadStatus", Params);
                    scope.Complete();
                }
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("BMessageAnnouncement", "UpdateMessageAnnouncementItemReadStatus", e1);
                throw e1;
            }
            TraceLogger.Log(DateTime.Now);
            return success;
        }
    }
}