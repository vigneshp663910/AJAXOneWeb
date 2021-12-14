using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_PaymentTerm
    { 
        public int PaymentTermID { get; set; }
        public string PaymentTerm { get; set; }
        public string Description { get; set; }
        public string PaymentTerm_Description
        {
            get
            {
                return PaymentTerm + " " + Description;
            }
        }

    }
}
