using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BStockTransferOrder
    {
        public PApiResult GetStockTransferOrderHeader(int? DealerID, string StockTransferOrderNo, string DateFrom, string DateTo, int? StatusID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "StockTransferOrder/GetStockTransferOrderHeader?DealerID=" + DealerID + "&StockTransferOrderNo=" + StockTransferOrderNo
              + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&StatusID=" + StatusID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult InsertStockTransferOrder(object obj)
        {
            string endPoint = "StockTransferOrder/InsertStockTransferOrder";
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut(endPoint, obj));
        }
        public PApiResult InsertOrUpdateStockTransferOrderItem(object obj)
        {
            string endPoint = "StockTransferOrder/InsertOrUpdateStockTransferOrderItem";
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut(endPoint, obj));
        }
        public PApiResult GetStockTransferOrderByID(long StockTransferOrderID)
        {
            string endPoint = "StockTransferOrder/GetStockTransferOrderByID?StockTransferOrderID=" + StockTransferOrderID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult UpdateStockTransferOrderStatus(long StockTransferOrderID,int StatusID)
        {
            string endPoint = "StockTransferOrder/UpdateStockTransferOrderStatus?StockTransferOrderID=" + StockTransferOrderID + "&StatusID=" + StatusID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
}
