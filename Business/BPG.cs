using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BPG
    { 
        //public void OutputNo(string Query)
        //{
            
        //}

         
        public string outputSingle(string Query)
        {
            string endPoint = "PG/OutputSingle?Query=" + Query;
            return JsonConvert.DeserializeObject<string>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }

        public List<string> outputDouble(string Query)
        {
            string endPoint = "PG/OutputDouble?Query=" + Query;
            return JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }

        public DataTable OutputDataTable(string Query)
        {
            string endPoint = "PG/OutputDataTable?Query=" + Query;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public Boolean UpdateTransactions(List<string> Querys)
        {
            string result = new BAPI().ApiPut("PG//UpdateTransactions", Querys);
            return JsonConvert.DeserializeObject<Boolean>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data));
        }
    }
}
