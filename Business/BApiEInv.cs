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
            EInvoiceAPI = Convert.ToString(ConfigurationManager.AppSettings["ApiBaseAddress"]);
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
        public String ApiPut(string AccessToken, object obj = null)
        { 
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken); 
            var APIResponse = client.PostAsync(EInvoiceAPI, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")).Result; 
            if (APIResponse.IsSuccessStatusCode)
            {
                return APIResponse.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("APIResponse, Error : " + APIResponse.StatusCode);
            }
            return "";
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
}
