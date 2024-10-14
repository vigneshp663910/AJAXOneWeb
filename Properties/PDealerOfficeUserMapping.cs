using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class PDealerOfficeUserMapping
    {
        public PUser User { get; set; }
        public PDealer Dealer { get; set; }
        public PDMS_DealerOffice DealerOffice { get; set; }
        public Boolean IsActive { get; set; }
        public PUser ActionGivenBy { get; set; }
        public DateTime? ActionGivenOn { get; set; }
        public PUser ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}