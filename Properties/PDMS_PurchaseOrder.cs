using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_PurchaseOrder
    {
        public string PurchaseOrderID { get; set; }
        public long HeaderCount { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public string POMonth { get; set; }
        public string POType { get; set; }
        public string POStatus { get; set; }
        public string Location { get; set; }
        public string Currency { get; set; }
        public string BillTo { get; set; }
        public string Insurance { get; set; }
        public decimal TaxAmount { get; set; }

        public string SoldTo { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Division { get; set; }
        public PDMS_PurchaseOrderItem PurchaseOrderItem { get; set; }
        public PDMS_Dealer Dealer { get; set; }
    }

    [Serializable]
    public class PDMS_PurchaseOrderItem
    {
        public string PurchaseOrderID { get; set; }
        public int POItem { get; set; }
        public long ItemCount { get; set; }
        public PDMS_Material Material { get; set; }
        public decimal OrderQuantity { get; set; }
        public decimal TaxAmount { get; set; }
        public string UOM { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal ShipedQuantity { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal ApprovedQuantity { get; set; }
        public decimal Fright { get; set; }
        public decimal Insurance { get; set; }
        public decimal PackingAndForwarding { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public DateTime? ASNDate { get; set; }
        public decimal? ASNQuantity { get; set; }
        public string GRNumber { get; set; }
        public DateTime? GRDate { get; set; }
        public decimal? GRQuantity { get; set; }
        public decimal? MissingQuantity { get; set; }
        public decimal? DamagedQuantity { get; set; }
        public Boolean ISWrongSupplied { get; set; }
        public decimal? WrongSupplyQuantity { get; set; }
        public string GRStatus { get; set; }
        public decimal? POMinusAsnQuantity { get; set; }
        public int? AsnMinusPODate { get; set; }
        public decimal? POMinusGrQuantity { get; set; }
        public int? GrMinusPODate { get; set; }
        public decimal CumulativeAsnQuantity { get; set; }
        public decimal CumulativeGrQuantity { get; set; }
        public DateTime? LatestGrDate { get; set; }
    }

    [Serializable]
    public class PDMS_PurchaseOrderType
    {
        public long PurchaseOrderTypeID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
    [Serializable]
    public class PDMS_PurchaseOrderStatus
    {
        public long StatusID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    [Serializable]
    public class PPurchaseOrderType
    {
        public int PurchaseOrderTypeID { get; set; }
        public string Code { get; set; }
        public string PurchaseOrderType { get; set; }
        public string SapOrderType { get; set; }
    }
    [Serializable]
    public class PPurchaseOrderStatus
    {
        public int PurchaseOrderStatusID { get; set; }
        public string Code { get; set; }
        public string PurchaseOrderStatus { get; set; }
    }
    [Serializable]
    public class PPurchaseOrderAsnStatus
    {
        public int AsnStatusID { get; set; }
        public string AsnStatus { get; set; }
    }
    [Serializable]
    public class PPurchaseOrderAsnGrStatus
    {
        public int GrStatusID { get; set; }
        public string GrStatus { get; set; }
    }
    [Serializable]
    public class PPurchaseOrderTo
    {
        public int PurchaseOrderToID { get; set; }
        public string PurchaseOrderTo { get; set; }
    }
    [Serializable]
    public class PPurchaseOrder
    {
        public long PurchaseOrderID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_DealerOffice Location { get; set; }
        public PPurchaseOrderTo PurchaseOrderTo { get; set; }
        public PDMS_Dealer Vendor { get; set; }
        public PPurchaseOrderType PurchaseOrderType { get; set; }
        public PPurchaseOrderStatus PurchaseOrderStatus { get; set; }
        public PDMS_Division Division { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }
        public string ReferenceNo { get; set; }
        public string Remarks { get; set; }

        public string Insurance { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public PPurchaseOrderItem PurchaseOrderItem { get; set; }
        public List<PPurchaseOrderItem> PurchaseOrderItems { get; set; }

        public string SaleOrderNumber { get; set; }
    }

    [Serializable]
    public class PPurchaseOrderItem
    {
        public long PurchaseOrderID { get; set; }
        public long PurchaseOrderItemID { get; set; }
        public int POItem { get; set; }
        public PMaterial Material { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxableValue { get; set; }
        public PPurchaseOrderStatus PurchaseOrderStatus { get; set; }
        public decimal Tax { get { return Material.CGST + Material.SGST + Material.IGST; } }
        public decimal TaxValue { get { return Material.CGSTValue + Material.SGSTValue + Material.IGSTValue; } }
        //public decimal NetAmount { get; set; }
        //public decimal GrossAmount { get; set; }
        //public decimal ShipedQuantity { get; set; } 
        //public decimal ApprovedQuantity { get; set; }
        //public decimal Fright { get; set; }
        //public decimal Insurance { get; set; }
        //public decimal PackingAndForwarding { get; set; } 
    }

    [Serializable]
    public class PPurchaseOrder_Insert
    {
        public long PurchaseOrderID { get; set; }
        public int DealerID { get; set; }
        public int DealerOfficeID { get; set; }
        public int PurchaseOrderToID { get; set; }
        public int VendorID { get; set; }
        public int PurchaseOrderTypeID { get; set; }
        public int DivisionID { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string ReferenceNo { get; set; }
        public string Remarks { get; set; }
        public List<PPurchaseOrderItem_Insert> PurchaseOrderItems { get; set; }

    }

    [Serializable]
    public class PPurchaseOrderItem_Insert
    {
        public long PurchaseOrderID { get; set; }
        public long PurchaseOrderItemID { get; set; }
        public int Item { get; set; }
        public int MaterialID { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public decimal Quantity { get; set; }
        public string UOM { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxableAmount { get; set; }

        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }
    }

    [Serializable]
    public class PAsn
    {
        public long AsnID { get; set; }
        public string AsnNumber { get; set; }
        public PPurchaseOrder PurchaseOrder { get; set; }
        public PGr Gr { get; set; }
        public DateTime AsnDate { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public string SoNumber { get; set; }
        public DateTime SoDate { get; set; }
        public string DeliveryNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? LoadingDate { get; set; }
        public decimal? NetWeight { get; set; }
        public string TrackID { get; set; }
        public string CourierID { get; set; }
        public DateTime? CourierDate { get; set; }
        public PPurchaseOrderAsnStatus AsnStatus { get; set; }
        public string ShipingAddress { get; set; }
        public decimal? CourierCharge { get; set; }
        public string LRNo { get; set; }
        public string Remarks { get; set; }
        public PAsnItem AsnItem { get; set; }
        public List<PAsnItem> AsnItemS { get; set; }

    }

    [Serializable]
    public class PAsnItem
    {
        public long AsnItemID { get; set; }
        public int AsnItem { get; set; }
        public long AsnID { get; set; }
        public PPurchaseOrderItem PurchaseOrderItem { get; set; }
        public PGrItem GrItem { get; set; }
        public PDMS_Material Material { get; set; }
        public decimal Qty { get; set; }
        public decimal NetWeight { get; set; }
        public string UomWeight { get; set; }
        public decimal PackCount { get; set; }
        public string UomPackCount { get; set; }
        public string StockType { get; set; }
        public Boolean IsChangedpart { get; set; }
        public string Remarks { get; set; }
        public string GrRemarks { get; set; }
    }

    [Serializable]
    public class PGr
    {
        public long GrID { get; set; }
        public string GrNumber { get; set; }
        public DateTime GrDate { get; set; }
        public PAsn ASN { get; set; }
        public List<PGrItem> GrItemS { get; set; }
        public PGrItem GrItem { get; set; }
        public PPurchaseOrderAsnGrStatus Status { get; set; }
        public string Remarks { get; set; }
        public PUser PostedBy { get; set; }
        public DateTime? PostedOn { get; set; }
        public PUser CancelledBy { get; set; }
        public DateTime? CancelledOn { get; set; }
        public string CancelledReason { get; set; }
    }
    [Serializable]
    public class PGrItem
    {
        public long GrID { get; set; }
        public long GrItemID { get; set; }
        public PAsnItem AsnItem { get; set; }
        public decimal DeliveredQty { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal DamagedQty { get; set; }
        public decimal ReturnedQty { get; set; }
        public decimal MissingQty { get; set; }
        public string Remark { get; set; }
    }

    [Serializable]
    public class PPurchaseOrderInvoice
    {
        public long PurchaseOrderInvoiceID { get; set; }
        public string Invoice { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Currency { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal FreightAmount { get; set; }

        public decimal TotalValue { get; set; }
        public decimal TotalTCSValue { get; set; }


        public PDMS_PurchaseOrderType PurchaseOrderType { get; set; }
        public PDMS_PurchaseOrderStatus Status { get; set; }
        public PDMS_DealerOffice Location { get; set; }

        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Vendor { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string Remarks { get; set; }

    }
    [Serializable]
    public class PPurchaseOrderInvoiceItem
    {
        public long PurchaseOrderInvoiceItemID { get; set; }
        public long PurchaseOrderInvoiceID { get; set; }
        public int Item { get; set; }
        public PDMS_Material Material { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal CGSTValue { get; set; }
        public decimal IGSTValue { get; set; }

        public decimal TCS { get; set; }
        public decimal TCSValue { get; set; }


        public decimal Freight { get; set; }
        public decimal Insururance { get; set; }
        public decimal Packing { get; set; }

        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
    }
    [Serializable]
    public class PGr_Insert
    {
        public string AsnItemID { get; set; }
        public long AsnID { get; set; }
        public decimal DeliveredQty { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal DamagedQty { get; set; }
        public decimal MissingQty { get; set; }
        public string GrRemarks { get; set; }
        public string ItemRemarks { get; set; }
    }



    [Serializable]
    public class PPurchaseOrderReturnItem_Insert
    {
        public long GrID { get; set; }
        public long GrItemID { get; set; }
        public string Remarks { get; set; }
    }
    [Serializable]    
    public class PPurchaseOrderReturnStatus
    {
        public int PurchaseOrderReturnStatusID { get; set; }
        public string PurchaseOrderReturnStatusCode { get; set; }
        public string PurchaseOrderReturnStatusDescription { get; set; }
    }
    [Serializable]
    public class PPurchaseOrderReturn
    {
        public long PurchaseOrderReturnID { get; set; }
        public string PurchaseOrderReturnNumber { get; set; }
        public DateTime PurchaseOrderReturnDate { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_DealerOffice Location { get; set; }
        public PDMS_Dealer Vendor { get; set; }
        public PPurchaseOrderReturnStatus PurchaseOrderReturnStatus { get; set; }
        public string Remarks { get; set; }
        public PPurchaseOrderReturnItem PurchaseOrderReturnItem { get; set; }
        public List<PPurchaseOrderReturnItem> PurchaseOrderReturnItems { get; set; }
    }
    [Serializable]
    public class PPurchaseOrderReturnItem
    {
        public long PurchaseOrderReturnID { get; set; }
        public long PurchaseOrderReturnItemID { get; set; }
        public PPurchaseOrder PurchaseOrder { get; set; }
        public int Item { get; set; }
        public PGrItem GrItem { get; set; }
        //  public PAsnItem AsnItem { get; set; }
        public PGr Gr { get; set; }
        public PAsn Asn { get; set; }

        public PMaterial Material { get; set; }
        public decimal Quantity { get; set; }
        public decimal DeliveredQty { get; set; }
    }
    [Serializable]
    public class PPurchaseOrderReturnDelivery
    {
        public long PoReturnDeliveryID { get; set; }
        public PPurchaseOrderReturn PurchaseOrderReturn { get; set; }
        public string DeliveryNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal? NetWeight { get; set; }
        public string TrackID { get; set; }
        public string CourierID { get; set; }
        public DateTime? CourierDate { get; set; }
        public string LRNo { get; set; }
        public string Remarks { get; set; }
        public PPurchaseOrderReturnDeliveryItem PurchaseOrderReturnDeliveryItem { get; set; }
        public List<PPurchaseOrderReturnDeliveryItem> PurchaseOrderReturnDeliveryItemS { get; set; }
    }
    [Serializable]
    public class PPurchaseOrderReturnDeliveryItem
    {
        public long PoReturnDeliveryItemID { get; set; }
        public long PoReturnDeliveryID { get; set; }
        public PPurchaseOrderReturnItem PurchaseOrderReturnItem { get; set; }
        public decimal DeliveryQty { get; set; }
        public decimal NetWeight { get; set; }
        public string UomWeight { get; set; }
        public decimal PackCount { get; set; }
        public string UomPackCount { get; set; }
        public string Remarks { get; set; }
    }
    [Serializable]
    public class PPurchaseOrderReturnDeliveryItem_Insert
    {
        public long PurchaseOrderReturnID { get; set; }
        public long PurchaseOrderReturnItemID { get; set; }
        public decimal DeliveryQty { get; set; }
        public string Remarks { get; set; }
    }
}
