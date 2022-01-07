using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    public class PColdVisit
    {
        public long ColdVisitID { get; set; }
        public string ColdVisitNumber { get; set; }
        public DateTime ColdVisitDate { get; set; }
        public PActionType ActionType { get; set; }
        public PDMS_Customer Customer { get; set; }
        public string Remark { get; set; }
        public PUser CreatedBy { get; set; }
    }
    public class PActionType
    {
        public int ActionTypeID { get; set; }
        public string ActionType { get; set; }
    }
}
