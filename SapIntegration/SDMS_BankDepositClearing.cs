using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class SDMS_BankDepositClearing
    {
        public PDMS_BankDepositClearing UpdateBankDepositClearingToSAP(PDMS_BankDepositClearing Bank)
        {
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZFI_F_29_BAPI");

            Bank.SapPostedOn = DateTime.Now;

            string GL_ACCOUNT = Bank.BankAccount.Replace("GL-", "");
            tagListBapi.SetValue("DOC_DATE", Bank.TransactionDate);
            tagListBapi.SetValue("PSTNG_DATE", Bank.SapPostedOn);
            tagListBapi.SetValue("HEADER_TXT", Bank.BankDescription.Substring(25, 25));
           // tagListBapi.SetValue("HEADER_TXT", Bank.HeaderText);
            tagListBapi.SetValue("REF_DOC_NO", Bank.DepositFor);
            tagListBapi.SetValue("GL_ACCOUNT", GL_ACCOUNT.Substring(0, 6));
            tagListBapi.SetValue("CUSTOMER", Bank.Customer.CustomerCode);
            tagListBapi.SetValue("AMT_DOCCUR", Bank.Amount);
           // tagListBapi.SetValue("CURRENCY", "NR");
            tagListBapi.SetValue("ITEM_TEXT", Bank.InvoiceNumber + "  " + Bank.PONumber);
            tagListBapi.Invoke(SAP.RfcDes());

            Bank.SapAccountNo = tagListBapi.GetString("DOC_NO");


            if (string.IsNullOrEmpty(Bank.SapAccountNo))
            {
                IRfcTable ERROR = tagListBapi.GetTable("ERROR");


                new SSapErrorMail().ErrorMail("Update Bank Deposit Clearing To SAP", ERROR.ToString());
            }

          
            try
            {
              
              //Bank.SapAccountNo = RESULT.CurrentRow.GetString("EMPLOYEE_DEPARTMENT"); 
              //Bank.SapClearedOn = Convert.ToDateTime(RESULT.CurrentRow.GetString("EMAIL_ID"));
            }
            catch (Exception ex)
           { }

            return Bank;
        }
    }
}
