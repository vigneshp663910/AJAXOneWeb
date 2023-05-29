using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BInventory
    {
        public DataTable GetInitialStock(int? DealerID, int? DivisionID, int? ModelID, string MaterialCode)
        {
            string endPoint = "Inventory/GetInitialStock?DealerID=" + DealerID + "&DivisionID=" + DivisionID + "&ModelID=" + ModelID + "&MaterialCode=" + MaterialCode;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult InsertInitialStock(List<PInitialStock_Post> Stock)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Inventory/InsertInitialStock", Stock));
        }
    }
}
