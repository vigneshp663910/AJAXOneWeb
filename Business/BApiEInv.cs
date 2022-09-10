using Newtonsoft.Json;
using Properties;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
//using System.Net.Http;
//using System.Net.Http.Headers;

using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Business
{
    public class PApiEInv
    {
        public string handle { get; set; }
        public string handleType { get; set; }
        public string password { get; set; }
        public int tokenDurationInMins { get { return 360; } }
    }
    public class BApiEInv
    {
        private static string EInvoiceAPI { get; set; }
        private static string EInvoiceTokensAPI { get; set; }
        public BApiEInv()
        {
            EInvoiceAPI = Convert.ToString(ConfigurationManager.AppSettings["EInvoiceAPI"]);
            EInvoiceTokensAPI = Convert.ToString(ConfigurationManager.AppSettings["EInvoiceTokensAPI"]);
        }
        public void Main1()
        {
        }

        public string GetAccessToken(PApiEInv user)
        {
            string token = "";
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            try
            {
                string SS = JsonConvert.SerializeObject(user);

                var tokenResponse = client.PostAsync(EInvoiceTokensAPI, new StringContent(SS, Encoding.UTF8, "application/json")).Result;

                if (tokenResponse.IsSuccessStatusCode)
                {
                    var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
                    token = JsonContent;
                }
                else
                {
                    token = "";
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return token;
        }
        public PResultEInv ApiPut(string AccessToken, object obj = null)
        {
            PResultEInv Data = new PResultEInv();

            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            var APIResponse = client.PostAsync(EInvoiceAPI, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")).Result;
           
            if (APIResponse.IsSuccessStatusCode)
            { 
                Data.Status = PApplication.Success;
                Data.data = JsonConvert.DeserializeObject<PResultEInvData>(APIResponse.Content.ReadAsStringAsync().Result);
               
            }
            else
            { 
                Data.Status = PApplication.Failure;
                Data.data = JsonConvert.DeserializeObject<PResultEInvError>(APIResponse.Content.ReadAsStringAsync().Result); 
            } 
            return Data;
        }
    }

    public class PToken
    {
        public PData Data { get; set; }
    }
    public class PData
    {
        public string token { get; set; }
    }

    public class PResultEInv
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object data { get; set; }
    }

    public class PResultEInvError
    {
        public string message { get; set; }
        public PResultEInvErrorArgs args { get; set; }
    }

    public class PResultEInvData
    {
        public string AckNo { get; set; }
        public DateTime AckDt { get; set; }
        public string Irn { get; set; }
        public string SignedInvoice { get; set; }
        public string SignedQRCode { get; set; }
        public string Status { get; set; }
        public string EwbNo { get; set; }
        public DateTime EwbDt { get; set; }
        public DateTime EwbValidTill { get; set; }
    }


    public class PResultEInvErrorArgs
    {
        public List<PResultEInvErrorArgsDetails> details { get; set; }
        public string data { get; set; }
    }
    public class PResultEInvErrorArgsDetails
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

}

 