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
        public List<PUser> GetMessageAnnouncementAccess()
        {
            string endPoint = "MessageNotification/GetMessageAnnouncementAccess";
            return JsonConvert.DeserializeObject<List<PUser>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}