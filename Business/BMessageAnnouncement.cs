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
        public PApiResult GetMessageAnnouncementHeader(long? MessageAnnouncementHeaderID, string Subject, string SentFrom, string SentTo, int? SentBy, bool? ReadStatus, string LoginEntryDate, int? PageIndex, int? PageSize)
        {
            string endPoint = "MessageNotification/GetMessageAnnouncementHeader?MessageAnnouncementHeaderID=" + MessageAnnouncementHeaderID + "&Subject=" + Subject + "&SentFrom=" + SentFrom + "&SentTo=" + SentTo + "&SentBy=" + SentBy + "&ReadStatus=" + ReadStatus + "&LoginEntryDate=" + LoginEntryDate + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetMessageAnnouncementHeaderAllNotification(long? MessageAnnouncementHeaderID, string Subject, string SentFrom, string SentTo, int? SentBy, bool? ReadStatus, string LoginEntryDate, int? PageIndex, int? PageSize)
        {
            string endPoint = "MessageNotification/GetMessageAnnouncementHeaderAllNotification?MessageAnnouncementHeaderID=" + MessageAnnouncementHeaderID + "&Subject=" + Subject + "&SentFrom=" + SentFrom + "&SentTo=" + SentTo + "&SentBy=" + SentBy + "&ReadStatus=" + ReadStatus + "&LoginEntryDate=" + LoginEntryDate + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetMessageAnnouncementHeaderByID(long? MessageAnnouncementHeaderID, bool? ReadStatus, int? PageIndex, int? PageSize)
        {
            string endPoint = "MessageNotification/GetMessageAnnouncementHeaderByID?MessageAnnouncementHeaderID=" + MessageAnnouncementHeaderID + "&ReadStatus="+ ReadStatus;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public List<PMessageAnnouncementItem> GetUsersForMessageAnnouncement(long? UserID, string UserName, int? UserTypeID, string ExternalReferenceID, int? DealerID, bool? IsEnabled, string ContactName, int? DealerDepartmentID, int? DealerDesignationID)
        {
            string endPoint = "MessageNotification/GetUsersForMessageAnnouncement?UserID=" + UserID + "&UserName=" + UserName + "&UserTypeID=" + UserTypeID + "&ExternalReferenceID=" + ExternalReferenceID + "&DealerID=" + DealerID + "&IsEnabled=" + IsEnabled + "&ContactName=" + ContactName + "&DealerDepartmentID=" + DealerDepartmentID + "&DealerDesignationID=" + DealerDesignationID;
            return JsonConvert.DeserializeObject<List<PMessageAnnouncementItem>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PUser> GetMessageAnnouncementAccess()
        {
            string endPoint = "MessageNotification/GetMessageAnnouncementAccess";
            return JsonConvert.DeserializeObject<List<PUser>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}