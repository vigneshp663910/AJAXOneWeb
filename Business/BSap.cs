using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BSap
    {
        public PApiResult GetPurchaseOrderAsnInvoiceNumber(string InvoiceNumber)
        {
            string endPoint = "Sap/GetPurchaseOrderAsnInvoiceNumber?InvoiceNumber=" + InvoiceNumber;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public decimal GetEdfsCashBalance(string dealercode)
        {
            string endPoint = "Sap/GetEdfsCashBalance?dealercode=" + dealercode;
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (string.IsNullOrEmpty(Convert.ToString(Result.Data)))
            {
                //throw new Exception("EDFS Cash Balance is not maintained."); 
                return 0;
            }
            return Convert.ToDecimal(Result.Data); 
        }
        public decimal GetMaterialStock(string materialcode)
        {
            string endPoint = "Sap/GetMaterialStock?materialcode=" + materialcode;
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (string.IsNullOrEmpty(Convert.ToString(Result.Data)))
            {
                throw new Exception(Result.Message);
            }
            return Convert.ToDecimal(Result.Data);
        }
    }
}
