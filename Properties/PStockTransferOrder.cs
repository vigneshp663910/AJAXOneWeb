using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    public class PStockTransferOrder
    {
        public long StockTransferOrderID { get; set; }
        public string StockTransferOrderNumber { get; set; }
        public DateTime StockTransferOrderDate { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_DealerOffice DestinationOffice { get; set; }
        public PDMS_DealerOffice SourceOffice { get; set; }
        public PAjaxOneStatus Status { get; set; }
        public PDMS_Division Division { get; set; }
        public string Remarks { get; set; }
        public PStockTransferOrderItem Item { get; set; }
        public List<PStockTransferOrderItem> Items { get; set; }
    }
    [Serializable]
    public class PStockTransferOrderItem
    {
        public long StockTransferOrderID { get; set; }
        public long StockTransferOrderItemID { get; set; }
        public int ItemNo { get; set; }
        public PMaterial Material { get; set; }
        public decimal Quantity { get; set; }
        public decimal TransitQuantity { get; set; }
        public decimal DeliveredQuantity { get; set; }
    }
    [Serializable]
    public class PStockTransferOrder_Insert
    {
        public long StockTransferOrderID { get; set; }
        public int DealerID { get; set; }
        public int DestinationOfficeID { get; set; }
        public int SourceOfficeID { get; set; }
        public int DivisionID { get; set; }
        public string Remarks { get; set; }
        public List<PStockTransferOrderItem_Insert> Items { get; set; }
    }
    [Serializable]
    public class PStockTransferOrderItem_Insert
    {
        public long StockTransferOrderID { get; set; }
        public long StockTransferOrderItemID { get; set; }
        public int Item { get; set; }
        public int MaterialID { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public decimal Quantity { get; set; }
        public string UOM { get; set; }
    }
    [Serializable]
    public class PStockTransferOrderItemDelivery_Insert
    {
        public long StockTransferOrderID { get; set; }
        public long StockTransferOrderItemID { get; set; }
        public int Item { get; set; }
        public long MaterialID { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal BalanceQuantity { get; set; }
        public decimal DeliveryQuantity { get; set; }
    }
    [Serializable]
    public class PStockTransferOrderDelivery
    {
        public long DeliveryID { get; set; }
        public string DeliveryNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string GrNumber { get; set; }
        public DateTime? GrDate { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PStockTransferOrder StockTransferOrder { get; set; }
        public PStockTransferOrderDeliveryItem Item { get; set; }
        public List<PStockTransferOrderDeliveryItem> Items { get; set; }
    }
    [Serializable]
    public class PStockTransferOrderDeliveryItem
    {
        public long DeliveryID { get; set; }
        public long DeliveryItemID { get; set; }
        public PStockTransferOrderItem StockTransferOrderItem { get; set; }

        public int ItemNo { get; set; }
        public PDMS_Material Material { get; set; }
        public decimal DeliveryQuantity { get; set; }
        public decimal? GrQuantity { get; set; }
        public decimal? UnrestrictedQuantity { get; set; }
        public decimal? RestrictedQuantity { get; set; }
    }
}
