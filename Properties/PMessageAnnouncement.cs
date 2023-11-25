using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class PMessageAnnouncement
    {
        public long MessageAnnouncementId { get; set; }
        public PUser AssignTo { get; set; }
        public string Message { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Boolean MailResponce { get; set; }
        public PDealer Dealer { get; set; }
        public long NotificationNumber{get;set;}
    }

}