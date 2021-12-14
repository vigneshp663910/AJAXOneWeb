using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    public class PUserPermission
    {
        public Boolean TRMove { get; set; }
        public Boolean TRReport { get; set; }   
        public Boolean TRApprove { get; set; }
        public Boolean TRApproveReport { get; set; }
        public Boolean OpenTicket { get; set; }
    }
}
