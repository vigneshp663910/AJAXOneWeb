using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BDealerBusinessExcellence
    {
        public List<PDealerBusinessExcellenceHeader> GetDealerBusinessExcellence(int? Year, int? Month, int? DealerID, int? RegionID, int? StatusID)
        {
            string endPoint = "DealerBusinessExcellence/GetDealerBusinessExcellence?Year=" + Year + "&Month=" + Month + "&DealerID=" + DealerID + "&RegionID=" + RegionID + "&StatusID=" + StatusID;
            return JsonConvert.DeserializeObject<List<PDealerBusinessExcellenceHeader>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}
