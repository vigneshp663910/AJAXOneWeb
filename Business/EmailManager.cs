using DataAccess;
using Properties;
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
using System.Transactions;

namespace Business
{
    public class EmailManager : System.Web.UI.Page
    {

        private IDataAccess provider;
        public EmailManager()
        {
            provider = new ProviderFactory().GetProvider();
        }


        public void MailSend(string mailTo, string Subject, string messageBody, long TicketID)
        {
            try
            {
                DbParameter TicketIDP = provider.CreateParameter("TicketID", TicketID, DbType.Int64);
                DbParameter UserIDP = provider.CreateParameter("UserID", PSession.User.UserID, DbType.Int64);
                DbParameter mailToP = provider.CreateParameter("MailTo", mailTo, DbType.String);
                DbParameter SubjectP = provider.CreateParameter("Subject", Subject, DbType.String);
                DbParameter messageBodyP = provider.CreateParameter("MessageBody", messageBody, DbType.String);
               
                DbParameter[] Params = new DbParameter[5] { TicketIDP, UserIDP, mailToP, SubjectP, messageBodyP };
                try
                {
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        provider.Insert("insertMailSendInfo", Params); 
                        scope.Complete(); 
                    } 
                }
                catch (SqlException sqlEx)
                {
                    new FileLogger().LogMessage("EmailManager", "MailSend", sqlEx);

                }
                catch (Exception ex)
                {
                    new FileLogger().LogMessage("EmailManager", " MailSend", ex);

                }

                ////   new FileLogger().LogTrack("MailSend", "Start");
                //MailMessage mailMessage = new MailMessage();
                //string fromMail = Convert.ToString(ConfigurationManager.AppSettings["MailFrom"]);

                //string mailPwd = Convert.ToString(ConfigurationManager.AppSettings["Mailpwd"]);
                //string smtpHost = Convert.ToString(ConfigurationManager.AppSettings["SmtpHost"]);

                //NetworkCredential networkCredential = new NetworkCredential(fromMail, mailPwd);
                //mailMessage.From = new MailAddress(fromMail);
                //mailMessage.Subject = Subject;
                //mailMessage.To.Add(mailTo);
                //mailMessage.Body = messageBody;
                //SmtpClient smtpClient = new SmtpClient();
                //smtpClient.Host = smtpHost;
                //smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.Credentials = networkCredential;
                //mailMessage.IsBodyHtml = true;
                ////      smtpClient.EnableSsl = true;
                //smtpClient.Send(mailMessage);

                //// new FileLogger().LogTrack("MailSend", "End");
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("EmailManager", "MailSend", ex);
            }
        }
        public string GetFileContent(string FilePath)
        {
            string msg = string.Empty;
            try
            {
                if (File.Exists(FilePath))
                {
                    FileStream fStream = new FileStream(FilePath, FileMode.Open);
                    StreamReader sReader = new StreamReader(fStream);
                    msg = sReader.ReadToEnd();
                    fStream.Close();
                }
                else
                    throw new FileNotFoundException();
            }
            catch (FileNotFoundException fex)
            {
                new FileLogger().LogMessage("EmailManager", "GetFileContent", fex); 
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("EmailManager", "GetFileContent", ex); 
            }
            return msg;
        }
        private string GetMessageBody(string messagePath, int MailModuleId)
        {
          string messageBody = new EmailManager().GetFileContent(Server.MapPath(messagePath));

          if (MailModuleId ==1)
          {

          }
          else if (MailModuleId == 2)
          {

          }
          else if (MailModuleId == 3)
          {

          }
          else if (MailModuleId == 4)
          {

          }

          return messageBody;

        }


        private void MailSendDelegate(string mailTo, string Subject, string messageBody, string ss)
        {
            try
            {
                //  new FileLogger().LogTrack("MailSend", "Start");
                MailMessage mailMessage = new MailMessage();
                string fromMail = Convert.ToString(ConfigurationManager.AppSettings["MailFrom"]);
                string mailPwd = Convert.ToString(ConfigurationManager.AppSettings["Mailpwd"]);
                string smtpHost = Convert.ToString(ConfigurationManager.AppSettings["SmtpHost"]);

                NetworkCredential networkCredential = new NetworkCredential(fromMail, mailPwd);
                mailMessage.From = new MailAddress(fromMail);
                mailMessage.Subject = Subject;
                mailMessage.To.Add(mailTo);
                mailMessage.Body = messageBody;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = smtpHost;
                smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                mailMessage.IsBodyHtml = true;
                //      smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                //  new FileLogger().LogTrack("MailSend", "End");
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("EmailManager", "MailSendDelegate", ex); 
            }
        }

        public Boolean MailSendTSIR(string mailTo, string Subject, string messageBody, Byte[] mybytes,string FileName)
        {
            try
            {
                //   new FileLogger().LogTrack("MailSend", "Start");
                MemoryStream memoryStream = new MemoryStream(mybytes);
                memoryStream.Seek(0, SeekOrigin.Begin);

                MailMessage mailMessage = new MailMessage();
                string fromMail = Convert.ToString(ConfigurationManager.AppSettings["MailFrom"]);

                string mailPwd = Convert.ToString(ConfigurationManager.AppSettings["Mailpwd"]);
                string smtpHost = Convert.ToString(ConfigurationManager.AppSettings["SmtpHost"]);

                NetworkCredential networkCredential = new NetworkCredential(fromMail, mailPwd);
                mailMessage.From = new MailAddress(fromMail);
                mailMessage.Subject = Subject;
                mailMessage.To.Add(mailTo);
                mailMessage.Body = messageBody;
                
                Attachment attachment = new Attachment(memoryStream, FileName);
                mailMessage.Attachments.Add(attachment);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = smtpHost;
                smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                mailMessage.IsBodyHtml = true;
                      smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                return true;
                // new FileLogger().LogTrack("MailSend", "End");
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("EmailManager", "MailSend", ex);
                return false;
            }
        }

        public void MailSendByService(List<string> MailToS, string Subject, string messageBody, List<string> MailCcS = null, List<string> MailBccS = null)
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

                foreach (string MailTo in MailToS)
                {
                    mailMessage.To.Add(MailTo);
                }
                if (MailCcS != null)
                {
                    foreach (string MailCC in MailCcS)
                    {
                        if (!string.IsNullOrEmpty(MailCC))
                            mailMessage.CC.Add(MailCC);
                    }
                }
                if (MailBccS != null)
                {
                    foreach (string MailBcc in MailBccS)
                    {
                        if (!string.IsNullOrEmpty(MailBcc))
                            mailMessage.Bcc.Add(MailBcc);
                    }
                }

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
                new FileLogger().LogMessageService("EmailManager", "MailSend", ex);
            }
        }

        public Boolean MailSendPRF(List<string> MailToS, string Subject, string messageBody, Byte[] mybytes, string FileName)
        {
            try
            {
                //   new FileLogger().LogTrack("MailSend", "Start");
                MemoryStream memoryStream = new MemoryStream(mybytes);
                memoryStream.Seek(0, SeekOrigin.Begin);

                MailMessage mailMessage = new MailMessage();
                string fromMail = Convert.ToString(ConfigurationManager.AppSettings["MailFrom"]);

                string mailPwd = Convert.ToString(ConfigurationManager.AppSettings["Mailpwd"]);
                string smtpHost = Convert.ToString(ConfigurationManager.AppSettings["SmtpHost"]);

                NetworkCredential networkCredential = new NetworkCredential(fromMail, mailPwd);
                mailMessage.From = new MailAddress(fromMail);
                mailMessage.Subject = Subject;

                foreach (string MailTo in MailToS)
                {
                    mailMessage.To.Add(MailTo);
                }

                mailMessage.Body = messageBody;

                Attachment attachment = new Attachment(memoryStream, FileName);
                mailMessage.Attachments.Add(attachment);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = smtpHost;
                smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                mailMessage.IsBodyHtml = true;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                return true;
                // new FileLogger().LogTrack("MailSend", "End");
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("EmailManager", "MailSend", ex);
                return false;
            }
        }

        public void MailSend(string mailTo, string Subject, string messageBody)
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
                mailMessage.To.Add(mailTo);
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
                new FileLogger().LogMessage("EmailManager", "MailSend", ex); 
            }
        }
    
    }
}
