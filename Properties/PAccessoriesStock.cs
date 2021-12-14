using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    public class PAccessoriesStock
    {
        public int AccessoriesStockID { get; set; }
        public PAccessoriesType AccessoriesType { get; set; }
        public decimal OpeningStock { get; set; }
        public decimal PurchasedStock { get; set; }
        public decimal AssignedStock { get; set; }
        public decimal ScrapStock { get; set; }
        public int Year { get; set; } 
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
    }
}
