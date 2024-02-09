using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    class PDealerBusiness
    {
    }
    [Serializable]
    public class PDealerBalanceConfirmation_Post
    {
        public long DealerBalanceConfirmationID { get; set; }
        public decimal VendorBalanceAsPerDealer { get; set; }
        public decimal CustomerBalanceAsPerDealer { get; set; }
        public decimal TotalOutstandingAsPerDealer { get; set; }
        public int BalanceConfirmationStatusID { get; set; }
        public List<PAttachedFile_Azure> AttachedFile { get; set; }
    }
}
