using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SapIntegration
{
   public class SHDFCBankPaymentIntegration
    {
       public string UpdateICTicketRequestedDateToSAP(string[] Payment, string ITEM_TEXT)
       {
           IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZFI_F_29_BAPI");

           //  tagListBapi.SetValue("COMP_CODE", "AF");
           //  tagListBapi.SetValue("DOC_DATE", Convert.ToDateTime(Payment[0]));

           tagListBapi.SetValue("PSTNG_DATE", Convert.ToDateTime(Payment[0]));
           tagListBapi.SetValue("HEADER_TXT", Payment[10]);
           tagListBapi.SetValue("REF_DOC_NO", Payment[4]);

           //  tagListBapi.SetValue("GL_ACCOUNT", "167130");

           tagListBapi.SetValue("CUSTOMER", Payment[2].Substring(Payment[2].Count() - 4, 4));
           tagListBapi.SetValue("AMT_DOCCUR", Payment[5]);

           //  tagListBapi.SetValue("CURRENCY", "NR");

           tagListBapi.SetValue("ITEM_TEXT", Payment[1] + "" + ITEM_TEXT);
           tagListBapi.Invoke(SAP.RfcDes());
           return tagListBapi.GetValue("RESULT").ToString();
       }
    }
}
