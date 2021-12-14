using SapIntegration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Business
{
    public class B_HDFCBankPaymentIntegration
    {
        public void IntegrationICTicket()
        {
            string folderPath = ConfigurationManager.AppSettings["HDFCBankPath"];
            string[] InvFiles = Directory.GetFiles(folderPath, "*");
            // new FileManager().DownloadAllFilesToBeImported(ConfigurationManager.AppSettings["EInvoiveFTPPathAE"], ConfigurationManager.AppSettings["EInvoiveFTPUserIDAE"], ConfigurationManager.AppSettings["EInvoiveFTPPasswordAE"], PDMS_EInvoice.EInvoivePathImport, "DMSS_INV_*");
            TraceLogger.Log(DateTime.Now);
            try
            {
                foreach (string file in InvFiles)
                {
                    string[] fileT = File.ReadAllText(file).Split(';');
                    string[] Inv = File.ReadAllText(file).Trim().Split('\n')[1].Split('|');
                    string FileName = file.Replace(folderPath, "");
                    try
                    {
                        string Result = new SHDFCBankPaymentIntegration().UpdateICTicketRequestedDateToSAP(Inv, FileName);
                        if (string.IsNullOrEmpty(Result))
                        {
                            File.Move(file, folderPath + "/Fail/" + FileName);
                        }
                        else
                        {
                            File.Move(file, folderPath + "/Success/" + FileName);
                        }
                    }
                    catch (Exception e1)
                    {
                        File.Move(file, folderPath + "/Fail/" + FileName);
                        new FileLogger().LogMessageService("BDMS_EInvoice", "IntegrationEInvoive", e1);
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_EInvoice", "IntegrationEInvoive", ex);
            }
        }
    }
}