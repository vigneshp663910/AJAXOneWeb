using DataAccess;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Transactions;

namespace Business
{
    public class BSmsManager
    {
        private IDataAccess provider;
        public BSmsManager()
        {
            provider = new ProviderFactory().GetProvider();
        }

        public int Start()
        {
            int Total = 0; 
            List<PSmsManager> Sms = new List<PSmsManager>(); 
            try
            { 
                using (DataSet ds = provider.Select("GetSMSSendInfo"))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Sms.Add(new PSmsManager()
                            {
                                SmsID = Convert.ToInt64(dr["SmsID"]),
                                PhoneNumber = Convert.ToString(dr["PhoneNumber"]),
                                Message = Convert.ToString(dr["Message"]),
                            }); 
                        }
                    }
                }
                foreach(PSmsManager s in Sms)
                {
                    if (SendSMS(s.PhoneNumber, s.Message))
                    {
                        int success = 0;
                        DbParameter SmsID = provider.CreateParameter("SmsID", s.SmsID, DbType.Int32);
                        DbParameter PhoneNumber = provider.CreateParameter("PhoneNumber", s.PhoneNumber, DbType.String);
                        DbParameter Message = provider.CreateParameter("Message", s.Message, DbType.String);
                        DbParameter[] Params = new DbParameter[3] { SmsID, PhoneNumber, Message };

                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            success = provider.Insert("InsertSMSSendInfo", Params);
                            scope.Complete();
                        }
                    }
                }
            }
            catch (Exception exp)
            { 
            }
            return Total;
        }
        public Boolean SendSMS(string sendToPhoneNumber, string Message)
        {
            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            Boolean success = false;
            try
            {
                // String sendToPhoneNumber = "8861002594";
                String userid = ConfigurationManager.AppSettings["UserIdSMS"]; //"2000138608";
                String passwd = ConfigurationManager.AppSettings["PasswdSMS"]; //"Gupsms@123";
                String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?method=sendMessage&send_to=" + sendToPhoneNumber + "&msg=" + Message + "&userid=" + userid + "&password=" + passwd + "&v=1.1 & msg_type=TEXT&auth_scheme=PLAIN";
                request = WebRequest.Create(url);
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new System.IO.StreamReader(stream, ec);
                result = reader.ReadToEnd();
                Console.WriteLine(result);
                reader.Close();
                stream.Close();
                success = true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
            return success;
        }
    }
    class PSmsManager
    {
        public long SmsID { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
