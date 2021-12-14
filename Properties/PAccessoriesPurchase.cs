using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    public class PAccessoriesPurchase
    {
        public int AccessoriesPurchaseID { get; set; }
        public PAccessoriesType AccessoriesType { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string VendorName { get; set; }
        public decimal Quantity { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; } 
    }
}
