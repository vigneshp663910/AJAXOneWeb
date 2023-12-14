using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class PMessageAnnouncementHeader
    {
        public long MessageAnnouncementHeaderID { get; set; }
        public string Message { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Boolean? MailResponce { get; set; }
        public List<PMessageAnnouncementItem> Item { get; set; }
    }
    [Serializable]
    public class PMessageAnnouncementItem
    {
        public long? MessageAnnouncementItemID { get; set; }
        public long MessageAnnouncementHeaderID { get; set; }
        public PUser AssignTo { get; set; }
        public Boolean MailResponce { get; set; }
        public Boolean ReadStatus { get; set; }
        public PDealer Dealer { get; set; }
    }

}