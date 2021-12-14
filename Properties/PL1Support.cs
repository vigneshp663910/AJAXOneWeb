using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PL1SupportMapping
    {
        public int? L1SupportUserMappingID { get; set; }
        public int? DealerID { get; set; }
        public int CategoryID { get; set; }
        public String Category { get; set; }
        public int? UserId { get; set; }
        public Boolean IsActive { get; set; }
    }
}