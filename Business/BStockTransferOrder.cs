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
    public class BStockTransferOrder
    {
        public PApiResult GetStockTransferOrderHeader(int? DealerID,int? OfficeID, string StockTransferOrderNo, string DateFrom, string DateTo, int? StatusID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "StockTransferOrder/GetStockTransferOrderHeader?DealerID=" + DealerID + "&OfficeID=" + OfficeID + "&StockTransferOrderNo=" + StockTransferOrderNo
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
        public PApiResult InsertStockTransferOrderDelivery(object obj)
        {
            string endPoint = "StockTransferOrder/InsertStockTransferOrderDelivery";
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut(endPoint, obj));
        }
        public PApiResult GetStockTransferOrderDeliveryByID(long? StockTransferOrderID, long? DeliveryID)
        {
            string endPoint = "StockTransferOrder/GetStockTransferOrderDeliveryByID?StockTransferOrderID=" + StockTransferOrderID + "&DeliveryID=" + DeliveryID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetStockTransferOrderDeliveryHeader(int? DealerID, string DeliveryNumber, string StockTransferOrderNo, string DateFrom, string DateTo, int? StatusID, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "StockTransferOrder/GetStockTransferOrderDeliveryHeader?DealerID=" + DealerID + "&DeliveryNumber=" + DeliveryNumber + "&StockTransferOrderNo=" + StockTransferOrderNo
              + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&StatusID=" + StatusID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult UpdateStockTransferOrderGr(object obj)
        {
            string endPoint = "StockTransferOrder/UpdateStockTransferOrderGr";
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut(endPoint, obj));
        }
        public PStockTransferOrderItem_Insert GetMaterialPriceForStockTransferOrder(int DealerID, PStockTransferOrderItem_Insert PoI)
        { 
            PDealer Dealer= new BDealer().GetDealerByID(DealerID,"");
            PSapMatPrice_Input InPut = new PSapMatPrice_Input();
            InPut.Customer = Dealer.DealerCode;
            //InPut.Vendor = Dealer.DealerCode;
            InPut.OrderType = "DEFAULT_SEC_AUART";
            //InPut.PriceDate = DateTime.Now;

            //PMaterial Mat = new BDMS_Material().MaterialPriceFromSap(Dealer.DealerCode, Dealer.DealerCode, "DEFAULT_SEC_AUART", 1, PoI.MaterialCode, PoI.Quantity, "", "", "false");
            List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(InPut);
            PMaterial Mat = Mats[0];
            PoI.TaxableValue = Mat.CurrentPrice;
            PoI.SGST = Mat.SGST;
            PoI.SGSTValue = Mat.SGST == 0 ? 0 : Mat.CurrentPrice * Mat.SGST / 100;
            PoI.CGST = Mat.SGST;
            PoI.CGSTValue = Mat.SGST == 0 ? 0 : Mat.CurrentPrice * Mat.SGST / 100;
            PoI.IGST = Mat.IGST;
            PoI.IGSTValue = Mat.IGST == 0 ? 0 : Mat.CurrentPrice * Mat.IGST / 100; 

            return PoI;
        }
    }
}
