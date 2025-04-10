using DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BChangeLogs
    {
        private IDataAccess provider;
        public BChangeLogs()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public DataSet GetDealerChangeLogs(string DealerCode, string DateFrom, string DateTo)
        {
            string endPoint = "ChangeLogs/GetDealerChangeLogs?DealerCode=" + DealerCode + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo;
            return JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataSet GetCustomerChangeLogs(string CustomerCode, string DateFrom, string DateTo)
        {
            string endPoint = "ChangeLogs/GetCustomerChangeLogs?CustomerCode=" + CustomerCode + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo;
            return JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}
