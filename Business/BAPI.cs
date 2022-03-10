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
    public class PApiResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class UL
    {
        public string UserName { get; set; }
        public string LoginPassword { get; set; }
    }
    //public class Token
    //{
    //    [JsonProperty("access_token")]
    //    public string AccessToken { get; set; }
    //    public string Error { get; set; }
    //    public string Name { get; set; }
    //    public string Role { get; set; }
    //    public string Parameter1 { get; set; }
    //    public string Parameter2 { get; set; }
    //}
    public class BAPI
    {
        private static string ApiBaseAddress { get; set; }
        public BAPI()
        {
            ApiBaseAddress = Convert.ToString(ConfigurationManager.AppSettings["ApiBaseAddress"]);
        }
        //private HttpClient HeadersForAccessTokenGenerate(string baseUrl,string apikey,string clientId,string clientSecret)
        //{
        //    HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
        //    HttpClient client = new HttpClient(handler);
        //    try
        //    {
        //        client.BaseAddress = new Uri(baseUrl);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        //        client.DefaultRequestHeaders.Add("apikey", apikey);
        //        client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{clientId}:{clientSecret}")));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return client;
        //}
        //public async Task<PUser> EmployeeRegistration(string accessToken)
        //{
        //    PUser employeeRegisterationResponse = null;
        //    try
        //    {
        //        string createEndPointURL = "https://www.c-sharpcorner/registerUsers";
        //        string username = "KhajaMoiz", password = "Password", firstname = "Khaja", lastname = "Moizuddin", email = "Khaja.Moizuddin@gmail.com";
        //        HttpClient client = Method_Headers(accessToken, createEndPointURL);
        //        string registerUserJson = RegisterUserJson(username, password, firstname, lastname, email);
        //        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
        //        request.Content = new StringContent(registerUserJson, Encoding.UTF8, "application/json");
        //        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        HttpResponseMessage tokenResponse = client.PostAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), request.Content).Result;
        //        if (tokenResponse.IsSuccessStatusCode)
        //        {
        //            employeeRegisterationResponse = await tokenResponse.Content.ReadAsAsync<PUser>(new[] { new JsonMediaTypeFormatter() });
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {

        //    }
        //    return employeeRegisterationResponse;
        //}

        private static string Username = string.Empty;
        private static string Password = string.Empty;

        public void Main1()
        {
            string token1 = "";
            UL user = new UL();
            user.UserName = "IT.OFFICER4";
            user.LoginPassword = "abc@123";

            token1 = GetAccessToken(user);

            if (!string.IsNullOrEmpty(token1))
            {
                CallAPIResource(token1);
            }
            else
            {
                Console.WriteLine(token1);
            }
            Console.ReadLine();
        }

        public string GetAccessToken(UL user)
        {
            string token = "";
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            //var RequestBody = new Dictionary<string, string>
            //    {
            //   // {"grant_type", "password"},
            //    {"username", username},
            //    {"password", password},
            //    };
            // var tokenResponse = client.PostAsync(baseAddress + "User", new FormUrlEncodedContent(RequestBody)).Result;
            //var tokenResponse = client.PostAsync(baseAddress + "User", new StringContent(JsonConvert.SerializeObject(user))).Result;

            //  baseAddress = "https://localhost:44302/";

            var tokenResponse = client.PostAsync(ApiBaseAddress + "User", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
                token = JsonContent;
            }
            else
            {
                token = "";
            }
            return token;
        }
        private static void CallAPIResource(string AccessToken)
        {
            string url = "https://localhost:44302/api/Master/GetServiceType";
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            //var RequestBody = new Dictionary<string, string>
            //    {
            //    {"Parameter1", "value1"},
            //    {"Parameter2", "vakue2"},
            //    };
            //var APIResponse = client.PostAsync(url, new FormUrlEncodedContent(RequestBody)).Result;


            var APIResponse = client.GetAsync(ApiBaseAddress + "Master/GetServiceType").Result;


            if (APIResponse.IsSuccessStatusCode)
            {
                var JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                //Token Message = JsonConvert.DeserializeObject<Token>(JsonContent);  
                Console.WriteLine("APIResponse : " + JsonContent.ToString());
            }
            else
            {
                Console.WriteLine("APIResponse, Error : " + APIResponse.StatusCode);
            }
        }


        public String ApiGet(string Filter)
        {
            // JsonResult JsonContent = new JsonResult();
            string AccessToken = PSession.AccessToken;

            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            //var RequestBody = new Dictionary<string, string>
            //    {
            //    {"Parameter1", "value1"},
            //    {"Parameter2", "vakue2"},
            //    };
            //var APIResponse = client.PostAsync(url, new FormUrlEncodedContent(RequestBody)).Result;


            var APIResponse = client.GetAsync(ApiBaseAddress + Filter).Result;
            if (APIResponse.IsSuccessStatusCode)
            {
                var JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                return APIResponse.Content.ReadAsStringAsync().Result;

            }
            else
            {
                Console.WriteLine("APIResponse, Error : " + APIResponse.StatusCode);
            }
            return "";
        }


        public String ApiPut(string EndPoint, object obj)
        {
            // JsonResult JsonContent = new JsonResult();
            string AccessToken = PSession.AccessToken;
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            //var RequestBody = new Dictionary<string, string>
            //    {
            //    {"Parameter1", "value1"},
            //    {"Parameter2", "vakue2"},
            //    };
            //var APIResponse = client.PostAsync(url, new FormUrlEncodedContent(RequestBody)).Result;

            var APIResponse = client.PostAsync(ApiBaseAddress + EndPoint, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")).Result;


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
}
