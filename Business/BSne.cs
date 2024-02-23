using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BSne
    {
        private IDataAccess provider;
        public BSne()
        {
            try
            {
                provider = new ProviderFactory().GetProvider();
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BSaleOrderDelivery", "provider : " + e1.Message, null);
            }
        }
        public PApiResult GetSaleOrderDeliveryHeader(long? SaleOrderDeliveryID, string DateFrom, string DateTo, string DeliveryNumber, int? DealerID, int? OfficeCodeID, int? DivisionID, string CustomerCode, int? SaleOrderTypeID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "Sne/GetSaleOrderDeliveryHeader?SaleOrderDeliveryID=" + SaleOrderDeliveryID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&DeliveryNumber=" + DeliveryNumber + "&DealerID=" + DealerID + "&OfficeCodeID=" + OfficeCodeID + "&DivisionID=" + DivisionID
                + "&CustomerCode=" + CustomerCode + "&SaleOrderTypeID=" + SaleOrderTypeID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetSaleOrderDeliveryReport(long? SaleOrderDeliveryID, string DateFrom, string DateTo, string DeliveryNumber, int? DealerID, int? OfficeCodeID, int? DivisionID, string CustomerCode, int? SaleOrderTypeID)
        {
            string endPoint = "Sne/GetSaleOrderDeliveryReport?SaleOrderDeliveryID=" + SaleOrderDeliveryID + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&DeliveryNumber=" + DeliveryNumber + "&DealerID=" + DealerID + "&OfficeCodeID=" + OfficeCodeID + "&DivisionID=" + DivisionID
                + "&CustomerCode=" + CustomerCode + "&SaleOrderTypeID=" + SaleOrderTypeID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PSaleOrderDelivery GetSaleOrderDeliveryByID(long SaleOrderDeliveryID)
        {
            string endPoint = "Sne/SaleOrderDeliveryByID?SaleOrderDeliveryID=" + SaleOrderDeliveryID;
            return JsonConvert.DeserializeObject<PSaleOrderDelivery>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PSaleOrderItem> GetSaleOrderItemByDeliveryID(long SaleOrderDeliveryID)
        {
            string endPoint = "Sne/GetSaleOrderItemByDeliveryID?SaleOrderDeliveryID=" + SaleOrderDeliveryID;
            return JsonConvert.DeserializeObject<List<PSaleOrderItem>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}
