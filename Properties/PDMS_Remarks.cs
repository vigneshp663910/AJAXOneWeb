using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_Remarks
    {
        public int RemarksMainID { get; set; }
        public int RemarksSubID { get; set; }
        public string Remarks { get; set; } 
    }
}
