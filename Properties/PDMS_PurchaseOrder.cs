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
        public decimal PackingAndForwarding  { get; set; }

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
    public class PDMS_PurchaseOrderN
    {
        public long PurchaseOrderID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public string SoNumber { get; set; }
        public string OrderTo { get; set; }
        public string OrderFor { get; set; }
        public PDMS_PurchaseOrderType PurchaseOrderType { get; set; }
        public PDMS_PurchaseOrderStatus Status { get; set; }
        public PDMS_DealerOffice Location { get; set; }
        public string Currency { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Vendor { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string Remarks { get; set; }
        
        public PDMS_PurchaseOrderItemN PurchaseOrderItem { get; set; }
        public List<PDMS_PurchaseOrderItemN> PurchaseOrderItemS { get; set; }       
    }
    [Serializable]
    public class PDMS_PurchaseOrderItemN
    {
        public string PurchaseOrderID { get; set; }
        public int Item { get; set; }
        public PDMS_Material Material { get; set; }
        public PDMS_PurchaseOrderStatus ItemStatus { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal TaxValue { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Discount { get; set; }

        public decimal TaxableValue { get; set; }
        public decimal ApprovedQuantity { get; set; }
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
    public class PDMS_ASN
    {
        public long AsnID { get; set; }
        public string AsnNumber { get; set; }
        public DateTime AsnDate { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public string DeliveryNumber { get; set; }
        public string SapDeliveryNumber { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public DateTime? PickupDate { get; set; }
        public DateTime? LoadingDate { get; set; }
        public decimal? NetWeight { get; set; } 
        public string TrackID { get; set; }
        public string CourierID { get; set; }
        public DateTime? CourierDate { get; set; }
        public string Status { get; set; }
        public string ShipingAddress { get; set; }
        public decimal? CourierCharge { get; set; }
        public string LRNo { get; set; }
        public string Remarks { get; set; }
        public PDMS_AsnItem AsnItem { get; set; }
        public List<PDMS_AsnItem> AsnItemS { get; set; }

        public string GrNumber { get; set; }
        public DateTime GrDate { get; set; }
        public string GrStatus { get; set; }

    }

    [Serializable]
    public class PDMS_AsnItem
    {
        public string AsnItemID { get; set; }
        public long AsnID { get; set; }
        public int AsnItem { get; set; }        
        public string SoNumber { get; set; }
        public int SoItem { get; set; } 
        public string SapSoNumber { get; set; } 

        public PDMS_Material Material { get; set; }
        public decimal Qty { get; set; }
        public decimal NetWeight { get; set; }
        public string UomWeight { get; set; }
        public decimal PackCount { get; set; }
        public string UomPackCount { get; set; }
        public string StockType { get; set; }
        public Boolean IsChangedpart { get; set; }
        public string Remarks { get; set; } 
        public PDMS_PurchaseOrderN PO { get; set; }

        public decimal DeliveredQty { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal DamagedQty { get; set; }
        public decimal ReturnedQty { get; set; }
        public string GrRemarks { get; set; } 
    }


    [Serializable]
    public class PDMS_GRN
    {
        public long GrID { get; set; }
        public string GrNumber { get; set; }
        public DateTime GrDate { get; set; }
        public PDMS_ASN ASN { get; set; }

        public PDMS_GrItem GrItem { get; set; }
        public List<PDMS_GrItem> GrItemS { get; set; }
    }
    [Serializable]
    public class PDMS_GrItem
    {
        public string GRID { get; set; }
        public int GRItem { get; set; }
        public PDMS_Material Material { get; set; }
        public decimal Qty { get; set; }
        public decimal ReceivedQty { get; set; } 
        public decimal DamagedQuantity { get; set; }
        public string Remark { get; set; }
    }



    [Serializable]
    public class PDMS_PurchaseOrderInvoice
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

        public PDMS_PurchaseOrderInvoiceItem InvoiceItem { get; set; }
        public List<PDMS_PurchaseOrderInvoiceItem> InvoiceItemS { get; set; }
    }
    [Serializable]
    public class PDMS_PurchaseOrderInvoiceItem
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
        public decimal Packing  { get; set; }

        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
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
        public long StatusID { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
    }

    [Serializable]
    public class PPurchaseOrder
    {
        public string PurchaseOrderID { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public PPurchaseOrderType POType { get; set; }
        public PPurchaseOrderStatus Status { get; set; }
        public string Location { get; set; }
        public string Currency { get; set; }
        public string BillTo { get; set; }
        public string Insurance { get; set; }
        public decimal TaxAmount { get; set; }
        public string SoldTo { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public PDMS_Division Division { get; set; }
        public PPurchaseOrderItem PurchaseOrderItem { get; set; }
        public List<PPurchaseOrderItem> PurchaseOrderItems { get; set; }
        public PDMS_Dealer Dealer { get; set; }
    }

    [Serializable]
    public class PPurchaseOrderItem
    {
        public string PurchaseOrderID { get; set; }
        public int Item { get; set; }
        public PDMS_Material Material { get; set; }
        public decimal Quantity { get; set; }
        public string UOM { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxableAmount { get; set; }

        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }
        public decimal TaxAmount { get; set; }       
        public decimal NetAmount { get; set; } 
        public decimal ShipedQuantity { get; set; } 

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
        public int OrderToID { get; set; }
        public int VendorID { get; set; }
        public int PurchaseOrderTypeID { get; set; }
        public int DivisionID { get; set; }
        
        public string ReferenceNo { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string Remarks { get; set; }
        
        
        public List<PPurchaseOrderItem_Insert> PurchaseOrderItems { get; set; }
        
    }

    [Serializable]
    public class PPurchaseOrderItem_Insert
    {
        public string PurchaseOrderID { get; set; }
        public int Item { get; set; }
        public int MaterialID { get; set; }
        public string MaterialCode { get; set; }
        public decimal Quantity { get; set; }
        public string UOM { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxableAmount { get; set; }

        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal ShipedQuantity { get; set; }  
    }
    [Serializable]
    public class PAsn
    {
        public long AsnID { get; set; }
        public string AsnNumber { get; set; }
        public DateTime AsnDate { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public string DeliveryNumber { get; set; }
        public string SapDeliveryNumber { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public DateTime? PickupDate { get; set; }
        public DateTime? LoadingDate { get; set; }
        public decimal? NetWeight { get; set; }
        public string TrackID { get; set; }
        public string CourierID { get; set; }
        public DateTime? CourierDate { get; set; }
        public string Status { get; set; }
        public string ShipingAddress { get; set; }
        public decimal? CourierCharge { get; set; }
        public string LRNo { get; set; }
        public string Remarks { get; set; }
        public PAsnItem AsnItem { get; set; }
        public List<PAsnItem> AsnItemS { get; set; }

        public string GrNumber { get; set; }
        public DateTime GrDate { get; set; }
        public string GrStatus { get; set; }

    }

    [Serializable]
    public class PAsnItem
    {
        public string AsnItemID { get; set; }
        public long AsnID { get; set; }
        public int AsnItem { get; set; }
        public string SoNumber { get; set; }
        public int SoItem { get; set; }
        public string SapSoNumber { get; set; }

        public PDMS_Material Material { get; set; }
        public decimal Qty { get; set; }
        public decimal NetWeight { get; set; }
        public string UomWeight { get; set; }
        public decimal PackCount { get; set; }
        public string UomPackCount { get; set; }
        public string StockType { get; set; }
        public Boolean IsChangedpart { get; set; }
        public string Remarks { get; set; }
        public PDMS_PurchaseOrderN PO { get; set; }

        public decimal DeliveredQty { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal DamagedQty { get; set; }
        public decimal ReturnedQty { get; set; }
        public string GrRemarks { get; set; }
    }

}
