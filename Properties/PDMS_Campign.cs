using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_Campign
    {
        public int CampignID { get; set; }
        public string CampignName { get; set; } 
    }
    [Serializable]
    public class PDMS_CampignTicket
    {
        public long CampignTicketID { get; set; }
        public PDMS_Campign Campign { get; set; }
        public Boolean IsCracked { get; set; }
        public DateTime?  WeldingDate { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
        public PDMS_ICTicket ICTicket { get; set; }
    }
}
