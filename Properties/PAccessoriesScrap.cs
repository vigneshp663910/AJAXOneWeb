using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    public class PAccessoriesScrap
    {
        public int AccessoriesScrapID { get; set; }
        public PAccessoriesType AccessoriesType { get; set; }
        public decimal Quantity { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
    }
}
