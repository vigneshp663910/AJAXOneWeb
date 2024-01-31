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
        public DataTable GetStockMovementType(int? StockMovementTypeID,string IsManual)
        {
            string endPoint = "Inventory/GetStockMovementType?StockMovementTypeID=" + StockMovementTypeID + "&IsManual=" + IsManual;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataTable GetInitialStock(int? DealerID, int? DivisionID, int? ModelID, string MaterialCode)
        {
            string endPoint = "Inventory/GetInitialStock?DealerID=" + DealerID + "&DivisionID=" + DivisionID + "&ModelID=" + ModelID + "&MaterialCode=" + MaterialCode;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult InsertUpdateInitialStock(List<PInitialStock_Post> Stock)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Inventory/InsertUpdateInitialStock", Stock));
        }
        public PApiResult GetDealerStock(int? DealerID, int? OfficeID, int? DivisionID, int? ModelID, string MaterialCode, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "Inventory/GetDealerStock?DealerID=" + DealerID + "&OfficeID=" + OfficeID + "&DivisionID=" + DivisionID + "&ModelID=" + ModelID
            + "&MaterialCode=" + MaterialCode + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetDealerStockMovement(int DealerID, int OfficeID, string MaterialCode, string FromDate ,string ToDate, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "Inventory/GetDealerStockMovement?DealerID=" + DealerID + "&OfficeID=" + OfficeID + "&MaterialCode=" + MaterialCode
            + "&FromDate=" + FromDate + "&ToDate=" + ToDate + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetDealerPhysicalInventoryPosting(int? DealerID, int? OfficeID, string FromDate, string ToDate, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "Inventory/GetDealerPhysicalInventoryPosting?DealerID=" + DealerID + "&OfficeID=" + OfficeID + "&FromDate=" + FromDate + "&ToDate=" + ToDate
             + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PPhysicalInventoryPosting GetDealerPhysicalInventoryPostingByID(long PhysicalInventoryPostingID)
        {
            string endPoint = "Inventory/GetDealerPhysicalInventoryPostingByID?PhysicalInventoryPostingID=" + PhysicalInventoryPostingID;
            return JsonConvert.DeserializeObject<PPhysicalInventoryPosting>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data)); 
        }
        public PApiResult InsertDealerPhysicalInventoryPosting(List<PPhysicalInventoryPosting_Post> Stock)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Inventory/InsertDealerPhysicalInventoryPosting", Stock));
        }
        public PApiResult GetDealerStockAgeing(int? DealerID, int? OfficeID, int? DivisionID, int? ModelID, string MaterialCode, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "Inventory/GetDealerStockAgeing?DealerID=" + DealerID + "&OfficeID=" + OfficeID + "&DivisionID=" + DivisionID + "&ModelID=" + ModelID
            + "&MaterialCode=" + MaterialCode + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
}
