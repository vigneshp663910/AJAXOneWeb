using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PSAPDocumentNumber
    {
        public string InvoiceNumber { get; set; }
        public string SAPDoc { get; set; }
        public DateTime SAPPostingDate { get; set; }
        public string SAPClearingDocument { get; set; }
        public DateTime? SAPClearingDate { get; set; }
        public decimal SAPInvoiceValue { get; set; }
        public decimal? SAPInvoiceTDSValue { get; set; }
        public int Dealer { get; set; }
    }
}
