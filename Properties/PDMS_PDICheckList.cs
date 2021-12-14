using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{


    [Serializable]
    public class PDMS_PDICheckList
    {
        public int PDICheckListID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    } 
    [Serializable]
    public class PDMS_ICTicketPDICheckList
    {
        public long ICTicketPDICheckListID { get; set; }
        public long ICTicketID { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public PUser InspectionDoneBy { get; set; }
        public DateTime InspectionOn { get; set; }

        public PDMS_ICTicketPDICheckListItem PDICheckListItem { get; set; }
        public List<PDMS_ICTicketPDICheckListItem> PDICheckListItems { get; set; }
    }

    [Serializable]
    public class PDMS_ICTicketPDICheckListItem
    {
        public long ICTicketPDICheckListItemID { get; set; }
        public long ICTicketPDICheckListID { get; set; }
        public PDMS_PDICheckList PDICheckList { get; set; }
        public string ObservationValue { get; set; }
        public string ObservationJudgement { get; set; }
        public PAttachedFile Picture { get; set; }
    }
}
