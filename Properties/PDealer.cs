using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
     [Serializable]
    
    public class PDealer
    {
        public int DID { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string ContactName { get; set; }
        public string DisplayName { get; set; }
        public string CodeWithDisplayName { get; set; }
        public string UserName { get; set; }
        public string CodeWithName { get; set; }
        public string MailID1 { get; set; }
        public string Phone { get; set; }
        public int UserTypeID { get; set; }        
        public Boolean IsActive { get; set; }
        public string HeadOfficeID { get; set; }
        public string StateCode { get; set; }

        public Boolean IsEInvoice { get; set; }
        public string EInvoiveFTPPath { get; set; }
        public string EInvoiveFTPUserID { get; set; }
        public string EInvoiveFTPPassword { get; set; }
        public string EInvoiveDate { get; set; }

        public PDMS_Country Country { get; set; }
        public PDMS_State State { get; set; }
    }
}
