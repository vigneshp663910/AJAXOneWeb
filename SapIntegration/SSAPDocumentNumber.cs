using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SapIntegration
{
   public class SSAPDocumentNumber
    {
       public PSAPDocumentNumber getSAPDocumentNumber(string InvoiceNumber)
       {
           PSAPDocumentNumber Cust = new PSAPDocumentNumber();

           IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZFM_SAP_FI_PAYMENT");
           tagListBapi.SetValue("P_XBLNR", InvoiceNumber);
           tagListBapi.Invoke(SAP.RfcDes());
         
           IRfcTable IT_PAYDET = tagListBapi.GetTable("IT_PAYDET");
           for (int i = 0; i < IT_PAYDET.RowCount; i++)
           {
               IT_PAYDET.CurrentIndex = i;
               Cust.InvoiceNumber = InvoiceNumber;
               Cust.SAPDoc = IT_PAYDET.CurrentRow.GetString("BELNR");
               Cust.SAPPostingDate = Convert.ToDateTime(IT_PAYDET.CurrentRow.GetString("BUDAT"));

               Cust.SAPClearingDocument = IT_PAYDET.CurrentRow.GetString("AUGBL");
               Cust.SAPClearingDate = string.IsNullOrEmpty(Cust.SAPClearingDocument) ? (DateTime?)null : Convert.ToDateTime(IT_PAYDET.CurrentRow.GetString("AUGDT"));

               //Cust.SAPInvoiceValue = Convert.ToDecimal(IT_PAYDET.CurrentRow.GetString("TOTAL"));
               Cust.SAPInvoiceValue = Convert.ToDecimal(IT_PAYDET.CurrentRow.GetString("CUSTOMER"));
               Cust.SAPInvoiceTDSValue = Convert.ToDecimal(IT_PAYDET.CurrentRow.GetString("WITHHOLDING"));
           }
           return Cust;
       }
    }
}
