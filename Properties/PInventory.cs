using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{ 
    public class PInventory
    { 
    }
    [Serializable]
    public class PInitialStock
    {
        public int InitialStockID { get; set; }
        public PDealer Dealer { get; set; }
        public PDMS_Material Material { get; set; }
        public Decimal Quantity { get; set; }
        public PUser PostedBy { get; set; }
        public DateTime PostedOn { get; set; }
    }
    [Serializable]
    public class PInitialStock_Post
    {
        public string DealerCode { get; set; }
        public string MaterialCode { get; set; }
        public Decimal Quantity { get; set; } 
    }
}
