 
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data; 
using System.Linq; 

namespace Business
{
    public class BProcurementMasters
    {
        public List<PPurchaseOrderType> GetPurchaseOrderType(int? PurchaseOrderTypeID, string PurchaseOrderType,int? DealerTypeID =null,int? OrderToID = null)
        {
            string endPoint = "ProcurementMasters/PurchaseOrderType?PurchaseOrderTypeID=" + PurchaseOrderTypeID + "&PurchaseOrderType=" + PurchaseOrderType
                + "&DealerTypeID=" + DealerTypeID + "&OrderToID=" + OrderToID;
            return JsonConvert.DeserializeObject<List<PPurchaseOrderType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}
