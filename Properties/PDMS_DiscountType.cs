using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_DiscountType
    {
        public int DiscountTypeID { get; set; }
        public string DiscountType { get; set; }
        public string DiscountTypeCode { get; set; }
    }
}
