using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_TypeOfWarranty
    {
        public int TypeOfWarrantyID { get; set; }
        public string TypeOfWarranty { get; set; }
        public Boolean IsActive { get; set; }
    }
}
