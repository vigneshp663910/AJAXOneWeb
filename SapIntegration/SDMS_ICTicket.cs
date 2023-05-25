using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class SDMS_ICTicket
    { 
        public List<string> getModelByProductID(string ProductID)
        {
            long n;
            if (long.TryParse(ProductID, out n))
            {
                ProductID = ProductID.PadLeft(18, '0');
            }
            List<string> Model = new List<string>();
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZBAPI_GET_MODEL_BY_PRODUCT");
            tagListBapi.SetValue("PRODUCT", ProductID);
            tagListBapi.Invoke(SAP.RfcDes());
            Model.Add(Convert.ToString(tagListBapi.GetValue("P_MATKL")));
            Model.Add(Convert.ToString(tagListBapi.GetValue("P_SPART")));
            Model.Add(Convert.ToString(tagListBapi.GetValue("P_MAKTX")));
            Model.Add(Convert.ToString(tagListBapi.GetValue("P_SERNR")));
            return Model;
        }
         
    }
}
