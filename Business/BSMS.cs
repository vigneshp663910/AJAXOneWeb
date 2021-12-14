using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Business
{
   public class BSMS
    {
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
}
