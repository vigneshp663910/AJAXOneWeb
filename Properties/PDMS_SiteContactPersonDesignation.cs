using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_SiteContactPersonDesignation
    {
        public int DesignationID { get; set; }
        public string Designation { get; set; }
        public Boolean IsActive { get; set; }
    }
}
