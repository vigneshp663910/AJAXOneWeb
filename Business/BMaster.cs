using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
   public class BMaster
    {
        public List<PStatusItem> GetStatusItem(int StatusHeaderID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Master/GetStatusItem?StatusHeaderID=" + StatusHeaderID;
            return JsonConvert.DeserializeObject<List<PStatusItem>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));          
        }   
    }
}
