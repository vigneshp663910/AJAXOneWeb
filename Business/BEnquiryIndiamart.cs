using DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Script.Serialization;

namespace Business
{
    public class BEnquiryIndiamart
    {
        private const string URL = "https://mapi.indiamart.com/wservce/enquiry/listing/"; 

        private IDataAccess provider;
        public BEnquiryIndiamart()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public DataTable GetEnquiryIndiamart(DateTime? DateFrom, DateTime? DateTo)
        {
            DbParameter DateFromP = provider.CreateParameter("DateFrom", DateFrom, DbType.DateTime);
            DbParameter DateToP = provider.CreateParameter("DateTo", DateTo, DbType.DateTime);
            try
            {
                using (DataSet DS = provider.Select("GetEnquiryIndiamart", new DbParameter[2] { DateFromP, DateToP }))
                {
                    if (DS != null)
                    {
                        return DS.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public Boolean InsertOrUpdateEnquiryIndiamart()
        {
            try
            {

                provider = new ProviderFactory().GetProvider();

                List<PEnquiryIndiamart> Enquirys = new List<PEnquiryIndiamart>();

                JavaScriptSerializer ser = new JavaScriptSerializer();
                Enquirys = ser.Deserialize<List<PEnquiryIndiamart>>(ApiGet());

                foreach (PEnquiryIndiamart Enquiry in Enquirys)
                {
                    DbParameter QUERY_ID = provider.CreateParameter("QUERY_ID", Enquiry.QUERY_ID, DbType.String);
                    DbParameter QTYPE = provider.CreateParameter("QTYPE", Enquiry.QTYPE, DbType.String);
                    DbParameter SENDERNAME = provider.CreateParameter("SENDERNAME", Enquiry.SENDERNAME, DbType.String);
                    DbParameter SENDEREMAIL = provider.CreateParameter("SENDEREMAIL", Enquiry.SENDEREMAIL, DbType.String);
                    DbParameter MOB = provider.CreateParameter("MOB", Enquiry.MOB, DbType.String);
                    DbParameter GLUSR_USR_COMPANYNAME = provider.CreateParameter("GLUSR_USR_COMPANYNAME", Enquiry.GLUSR_USR_COMPANYNAME, DbType.String);
                    DbParameter ENQ_ADDRESS = provider.CreateParameter("ENQ_ADDRESS", Enquiry.ENQ_ADDRESS, DbType.String);
                    DbParameter ENQ_CITY = provider.CreateParameter("ENQ_CITY", Enquiry.ENQ_CITY, DbType.String);
                    DbParameter ENQ_STATE = provider.CreateParameter("ENQ_STATE", Enquiry.ENQ_STATE, DbType.String);
                    DbParameter COUNTRY_ISO = provider.CreateParameter("COUNTRY_ISO", Enquiry.COUNTRY_ISO, DbType.String);
                    DbParameter PRODUCT_NAME = provider.CreateParameter("PRODUCT_NAME", Enquiry.PRODUCT_NAME, DbType.String);
                    DbParameter ENQ_MESSAGE = provider.CreateParameter("ENQ_MESSAGE", Enquiry.ENQ_MESSAGE, DbType.String);
                    DbParameter DATE_RE = provider.CreateParameter("DATE_RE", Enquiry.DATE_RE, DbType.String);

                    DbParameter DATE_TIME_RE = provider.CreateParameter("DATE_TIME_RE", Enquiry.DATE_TIME_RE, DbType.DateTime);
                    DbParameter QUERY_MODID = provider.CreateParameter("QUERY_MODID", Enquiry.QUERY_MODID, DbType.String);
                    DbParameter ENQ_CALL_DURATION = provider.CreateParameter("ENQ_CALL_DURATION", Enquiry.ENQ_CALL_DURATION, DbType.String);
                    DbParameter ENQ_RECEIVER_MOB = provider.CreateParameter("ENQ_RECEIVER_MOB", Enquiry.ENQ_RECEIVER_MOB, DbType.String);
                    DbParameter EMAIL_ALT = provider.CreateParameter("EMAIL_ALT", Enquiry.EMAIL_ALT, DbType.String);
                    DbParameter MOBILE_ALT = provider.CreateParameter("MOBILE_ALT", Enquiry.MOBILE_ALT, DbType.String);
                    DbParameter TOTAL_COUNT = provider.CreateParameter("TOTAL_COUNT", Enquiry.TOTAL_COUNT, DbType.String);
                    DbParameter[] Params = new DbParameter[20] {   QUERY_ID, QTYPE, SENDERNAME, SENDEREMAIL, MOB, GLUSR_USR_COMPANYNAME, ENQ_ADDRESS ,ENQ_CITY,ENQ_STATE,COUNTRY_ISO
                   ,PRODUCT_NAME,ENQ_MESSAGE,DATE_RE,DATE_TIME_RE,QUERY_MODID,ENQ_CALL_DURATION,ENQ_RECEIVER_MOB,EMAIL_ALT,MOBILE_ALT,TOTAL_COUNT};
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        provider.Insert("InsertOrUpdateEnquiryIndiamart", Params);
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public String ApiGet()
        {
            //string Start_Time = "Start_Time/21-JAN-2022/";
            //string End_Time = "End_Time/27-JAN-2022/";

            string Start_Time = "Start_Time/" + DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy") + "/";
            string End_Time = "End_Time/" + DateTime.Now.ToString("dd-MMM-yyyy") + "/";

            string GLUSR_MOBILE_KEY = "GLUSR_MOBILE_KEY/MTY0MzI3OTkzNy4yNjAxIzE3MzU2MTg=/";
            string GLUSR_MOBILE = "GLUSR_MOBILE/6366426080/";
            string dd = "" + GLUSR_MOBILE + GLUSR_MOBILE_KEY + Start_Time + End_Time;

            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);  
            var APIResponse = client.GetAsync(URL + GLUSR_MOBILE + GLUSR_MOBILE_KEY + Start_Time + End_Time).Result;

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
    }
    [Serializable]

    public class PEnquiryIndiamart
    {
        public string RN { get; set; }
        public string QUERY_ID { get; set; }
        public string QTYPE { get; set; }
        public string SENDERNAME { get; set; }
        public string SENDEREMAIL { get; set; }
        public string MOB { get; set; }
        public string GLUSR_USR_COMPANYNAME { get; set; }
        public string ENQ_ADDRESS { get; set; }
        public string ENQ_CITY { get; set; }
        public string ENQ_STATE { get; set; }
        public string COUNTRY_ISO { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string ENQ_MESSAGE { get; set; }
        public string DATE_RE { get; set; }
        public string DATE_R { get; set; }
        public string DATE_TIME_RE { get; set; }
        public string LOG_TIME { get; set; }
        public string QUERY_MODID { get; set; }
        public string ENQ_CALL_DURATION { get; set; }
        public string ENQ_RECEIVER_MOB { get; set; }
        public string EMAIL_ALT { get; set; }
        public string MOBILE_ALT { get; set; }
        public string TOTAL_COUNT { get; set; }



    }
}
