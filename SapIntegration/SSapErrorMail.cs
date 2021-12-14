using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks; 

namespace SapIntegration
{
    public class SSapErrorMail
    { 
        public void ErrorMail( string Subject, string messageBody)
        {
            try
            {
                //   new FileLogger().LogTrack("MailSend", "Start");
                MailMessage mailMessage = new MailMessage();
                string fromMail = Convert.ToString(ConfigurationManager.AppSettings["MailFrom"]);
                string mailPwd = Convert.ToString(ConfigurationManager.AppSettings["Mailpwd"]);
                string smtpHost = Convert.ToString(ConfigurationManager.AppSettings["SmtpHost"]);

                NetworkCredential networkCredential = new NetworkCredential(fromMail, mailPwd);
                mailMessage.From = new MailAddress(fromMail);
                mailMessage.Subject = Subject;


                mailMessage.To.Add("venkatramesh.v@ajax-engg.com");
                mailMessage.CC.Add("krishna.p@ajax-engg.com");
                mailMessage.CC.Add("john.peter@ajax-engg.com");
                mailMessage.Body = messageBody;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = smtpHost;
                smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                mailMessage.IsBodyHtml = true;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);

                // new FileLogger().LogTrack("MailSend", "End");
            }
            catch (Exception ex)
            {
                //   new FileLogger().LogMessageService("EmailManager", "ErrorMail", ex);
            }
        }
    }
}
