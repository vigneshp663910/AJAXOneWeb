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
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
//using System.Net.Http;
//using System.Net.Http.Headers;

using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Business
{
    public class PApiEInvHandle
    {
        public string handle { get; set; }
        public string handleType { get; set; }
        public string password { get; set; }
        public int tokenDurationInMins { get { return 360; } }
    }
    public class PApiEinvEHeader
    {
        public string TOKEN { get; set; }
        public string OrgID { get; set; }
        public string GspCODE { get; set; }
        public string Gstin { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }

    }
    public class BApiEInv
    {
        private static string EInvoiceAPI { get; set; }
        public BApiEInv()
        {
            EInvoiceAPI = Convert.ToString(ConfigurationManager.AppSettings["EInvoiceAPI"]);
        }
        public void Main1()
        {
        }
         
        public class PSuccess
        {
            public string data { get; set; }
        }
        public PResultEInv ApiPut(PApiHeader Header, PDealer Dealer, object obj = null)
        { 
            string PWD = "";
            PResultEInv Data = new PResultEInv();

            HttpClientHandler handler = new HttpClientHandler(); 
            HttpClient client = new HttpClient(handler);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-FLYNN-N-USER-TOKEN", Header.Data.token);
            client.DefaultRequestHeaders.Add("X-FLYNN-N-ORG-ID", Header.Data.associatedOrgs[0].organisation.id);
            client.DefaultRequestHeaders.Add("X-FLYNN-N-IRP-GSP-CODE", Dealer.GspCode);
            client.DefaultRequestHeaders.Add("X-FLYNN-N-IRP-GSTIN", Dealer.Gstin);
            client.DefaultRequestHeaders.Add("X-FLYNN-N-IRP-USERNAME", Dealer.ApiUserName);
            client.DefaultRequestHeaders.Add("X-FLYNN-N-IRP-PWD", Dealer.ApiPassword); 

            var APIResponse = client.PostAsync(EInvoiceAPI, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")).Result;
           
            if (APIResponse.IsSuccessStatusCode)
            { 
                Data.Status = PApplication.Success;
                PSuccess PSuccess = JsonConvert.DeserializeObject<PSuccess>(APIResponse.Content.ReadAsStringAsync().Result);
                Data.data = JsonConvert.DeserializeObject<PSuccessEInvData>(PSuccess.data);
               
            }
            else
            { 
                Data.Status = PApplication.Failure;
                Data.data = Convert.ToString( APIResponse.Content.ReadAsStringAsync().Result); 
            } 
            return Data;
        }
    }

    public class PApiHeader
    {
        public PHeaderData Data { get; set; }
    }
    public class PHeaderData
    {
        public string token { get; set; }
        public List< PHeaderDataAssociated> associatedOrgs { get; set; }
    }
    public class PHeaderDataAssociated
    { 
        public PHeaderDataAssociatedOrg organisation { get; set; } 
        
    }
    public class PHeaderDataAssociatedOrg
    { 
        public string id { get; set; }  
    }
    public class PResultEInv
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object data { get; set; }
    }

    public class PFailedEInv
    {
        public PFailedEInvError error { get; set; }
        
    }
    public class PFailedEInvError
    {
        public string message { get; set; }
        public string type { get; set; }
    }

    public class PSuccessEInv
    {
        public PSuccessEInvData data { get; set; }
    }
    public class PSuccessEInvData
    {
        public string AckNo { get; set; }
        public DateTime AckDt { get; set; }
        public string Irn { get; set; }
        public string SignedInvoice { get; set; }
        public string SignedQRCode { get; set; }
        public string Status { get; set; }
        public string EwbNo { get; set; }
        public DateTime? EwbDt { get; set; }
        public DateTime? EwbValidTill { get; set; }
    }


  
    public class PResultEInvErrorArgsDetails
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

}

 