using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
   public class BPreSale
    {
        public List<PActionType> GetActionType(int? ActionTypeID, string ActionType)
        {
            string endPoint = "PreSale/ActionType?ActionTypeID=" + ActionTypeID + "&ActionType=" + ActionType;
            return JsonConvert.DeserializeObject<List<PActionType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}
