using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_SalesOrder
    {
        public string SalesOrderID { get; set; }
        public string SalesOrderNumber { get; set; }
        public DateTime? SalesOrderDate { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }   
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string SalesOrderStatus { get; set; }  
        public PDMS_SalesOrderItems SalesOrderItem { get; set; }
        public List<PDMS_SalesOrderItems> SalesOrderItems { get; set; } 
    }
    [Serializable]
    public class PDMS_SalesOrderItems
    {
        public string SalesOrderItemID { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public string Customer { get; set; }
        public string CustomerName { get; set; }

        public string GSTNo { get; set; }
        public string SONumber { get; set; }
        public DateTime SODate { get; set; }
        public string SOStatus { get; set; }
       // public string QuotationNo { get; set; }
       // public DateTime? QuotationDate { get; set; }
      
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public PDMS_Material Material { get; set; } 
        public decimal UnitBasicPrice { get; set; }
        public decimal Qty { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal FreightAmount { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal? SGST { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal? CGST { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal? IGST { get; set; }
        public decimal IGSTAmt { get; set; }      
        public decimal Tax { get; set; }
        public decimal TotalAmt { get; set; }
        public string MatType { get; set; }
        public string Division { get; set; }
        public string Location { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }   
    }

    [Serializable]
    public class PDMS_SalesOrder1
    {
        public long HID { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; } 

        public string QuotationNumber { get; set; }
        public DateTime QuotationDate { get; set; }
        public string QuotationStatus { get; set; }

        public string SalesOrderNumber { get; set; }
        public DateTime? SalesOrderDate { get; set; }
        public string SalesOrderStatus { get; set; }

        public string DeliveryNumber { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }

        public string Location { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
          
        public string Division { get; set; }
 
        public decimal Basic { get; set; }
        public decimal Discount { get; set; }
        public decimal BasicAfterDisc { get; set; }
        public decimal Tax { get; set; }
        public decimal FreightInsurance { get; set; }
        public decimal TotalAmt { get; set; }

        public PDMS_SalesOrderItems1 SalesOrderItems { get; set; }
        public List<PDMS_SalesOrderItems1> SalesOrderItemsS { get; set; }

        public int? DeliveryMinusSalesOrderDays { get; set; }
        public int? InvoiceMinusSalesOrderDays { get; set; }

        public string PaymntTerms { get; set; }
        public string Transprt { get; set; } 
    }

    [Serializable]
    public class PDMS_SalesOrderItems1
    {
        public long IID { get; set; }
        public string SalesOrderItemID { get; set; }
       
        public string GSTNo { get; set; }

        public PDMS_Material Material { get; set; }

        public decimal QuotationQuantity { get; set; }
        public decimal SalesOrderQuantity { get; set; }
        public decimal DeliveryQuantity { get; set; }
        public decimal InvoiceQuantity { get; set; }

        public decimal QuotationMinusInvoiceQuantity { get; set; }
        public decimal SalesOrderMinusDeliveryQuantity { get; set; }
        public decimal SalesOrderMinusInvoiceQuantity { get; set; }

       


        public decimal UnitBasicPrice { get; set; } 
         
        public decimal Discount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal FreightAmount { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal? SGST { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal? CGST { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal? IGST { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmt { get; set; }
        public string MatType { get; set; }
        public string Division { get; set; }
        
    }

    [Serializable]
    public class PDMS_SalesInvoice
    { 
        public long InvoiceID { get; set; }
        public long HeaderCount { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; } 
        public string GSTNo { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Status { get; set; }
        public PDMS_SalesType SalesType { get; set; }

        public string Division { get; set; }
        public string Location { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string SalesPerson { get; set; }
        public string SaleOrderNumber { get; set; }
        public DateTime? SaleOrderDate { get; set; }
        public PDMS_SalesInvoiceItem InvoiceItem { get; set; }
        public List<PDMS_SalesInvoiceItem> InvoiceItems { get; set; }

        public string Reference { get; set; }
        public DateTime? ReferenceDate { get; set; }
        public string McMode { get; set; }
        public string MachineSlNo { get; set; }
        public string PaymentTerms { get; set; }
        public string CreditDays { get; set; }
        public string Remarks { get; set; }

    }
    [Serializable]
    public class PDMS_SalesInvoiceItem
    {
        public long InvoiceItemID { get; set; }
        public long ItemCount { get; set; }
        public int ItemNo { get; set; }
        public PDMS_Material Material { get; set; } 
        public decimal UnitBasicPrice { get; set; }
        public decimal Qty { get; set; }
        public decimal ReturnQty { get; set; } 
        //public decimal ReceivedQty { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal FreightAmount { get; set; }
        public decimal TaxableAmount { get; set; }

        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }

        public decimal? SGST { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal? CGST { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal? IGST { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxP { get; set; }
        public decimal TotalAmt { get; set; }
        public string CalType { get; set; }   
    }

   
   

    [Serializable]
    public class PDMS_SalesType
    {
        public int SalesTypeID { get; set; }
        public string SalesType { get; set; }
        public int SalesTypeCode { get; set; }
            
    }



    [Serializable]
    public class PSaleOrder
    {
        public long SaleOrderID { get; set; }
        public string QuotationNumber { get; set; }
        public DateTime QuotationDate { get; set; }
        public string SaleOrderNumber { get; set; }
        public DateTime? SaleOrderDate { get; set; }        
        public string ProformaInvoiceNumber { get; set; }
        public DateTime? ProformaInvoiceDate { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_DealerOffice DealerOffice { get; set; }
        public PDMS_Customer Customer { get; set; }
        public PDMS_CustomerShipTo ShipTo { get; set; }
        //public PSaleOrderStatus SaleOrderStatus { get; set; }
        public PAjaxOneStatus SaleOrderStatus { get; set; }
        public PSaleOrderItem SaleOrderItem { get; set; }
        public List<PSaleOrderItem> SaleOrderItems { get; set; }
      //  public string ContactPerson { get; set; }
        public string ContactPersonNumber { get; set; }
        public PDMS_Division Division { get; set; }
        public string Remarks { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public string RefNumber { get; set; }
        public DateTime? RefDate { get; set; }
        public string InsurancePaidBy { get; set; }
        public string FrieghtPaidBy { get; set; }
        public string Attn { get; set; }
        public PProduct Product { get; set; }
        public PDMS_EquipmentHeader Equipment { get; set; }
        public string TaxType { get; set; }
        public decimal GrossAmount { get; set; }
        public PSaleOrderType SaleOrderType { get; set; }
        public PUser SalesEngineer { get; set; }
        public decimal HeaderDiscountPercentage { get; set; }
        public PUser CreatedBy { get; set; }
        public PUser CancelledBy { get; set; }
        public DateTime CancelledOn { get; set; }
        public PUser ForceClosedBy { get; set; }
        public DateTime ForceClosedOn { get; set; }
        public List<PSaleOrderDelivery> Deliverys { get; set; }

    }
    [Serializable]
    public class PSaleOrderItem
    {
        public long SaleOrderItemID { get; set; }
        public PDMS_Material Material { get; set; }
        public decimal Quantity { get; set; }
        public decimal PerRate { get; set; }
        public decimal Value { get; set; }
        public decimal ItemDiscountValue { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal Tax { get; set; }
        //public decimal TotalAmt { get; set; }
        public decimal NetAmount { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public PUser CancelledBy { get; set; }
        public DateTime CancelledOn { get; set; }
        public decimal DeliveredQuantity { get; set; }
    }
    [Serializable]
    public class PSaleOrderDelivery
    {
        public long SaleOrderDeliveryID { get; set; }
        public string DeliveryNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public PAjaxOneStatus Status { get; set; }
        public PSaleOrder SaleOrder { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public PSaleOrderDeliveryItem SaleOrderDeliveryItem { get; set; }
        public List<PSaleOrderDeliveryItem> SaleOrderDeliveryItems { get; set; }
        public PDMS_EquipmentHeader Equipment { get; set; }
        public int GrandTotal { get; set; }
        public PDMS_WarrantyInvoiceType InvoiceType { get; set; }
        public PInvoiceDetails InvoiceDetails { get; set; }
        public string IRN { get; set; }
        public int TempTcsMatCount { get; set; }
        public decimal TCSValue { get; set; }
        public decimal TCSTax { get; set; }
    }
    [Serializable]
    public class PInvoiceDetails
    {
        public long InvoiceDetailsID { get; set; }
        public long PaidServiceInvoiceID { get; set; }
        public string SupplierGSTIN { get; set; }
        public string Supplier_addr1 { get; set; }
        public string SupplierLocation { get; set; }
        public string SupplierPincode { get; set; }
        public string SupplierStateCode { get; set; }

        public string BuyerGSTIN { get; set; }
        public string BuyerName { get; set; }
        public string BuyerStateCode { get; set; }
        public string Buyer_addr1 { get; set; }
        public string Buyer_loc { get; set; }
        public string BuyerPincode { get; set; }
        public string disp_sup_trade_Name { get; set; }
        public string disp_sup_addr1 { get; set; }
        public string disp_sup_loc { get; set; }
        public string disp_sup_pin { get; set; }
        public string disp_sup_stcd { get; set; }
    }
    [Serializable]
    public class PSaleOrderDeliveryItem
    {
        public long SaleOrderDeliveryItemID { get; set; }
        public PDMS_Material Material { get; set; }
        public PSaleOrderItem SaleOrderItem { get; set; } 
        public decimal Qty { get; set; }
        public decimal Value { get; set; } 
        public decimal DiscountValue { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }

        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }
        public decimal CessValue { get; set; }

    }
    [Serializable]
    public class PSaleOrderReturn
    {
        public long SaleOrderReturnID { get; set; }
        public string SaleOrderReturnNumber { get; set; }
        public DateTime SaleOrderReturnDate { get; set; }
        public string CreditNoteNumber { get; set; }
        public DateTime? CreditNoteDate { get; set; }
        public PUser ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public PUser CancelledBy { get; set; }
        public DateTime? CancelledOn { get; set; }
        public PSaleOrderDelivery SaleOrderDelivery { get; set; } 
        public PSaleOrderReturnItem SaleOrderReturnItem { get; set; }
        public List<PSaleOrderReturnItem> SaleOrderReturnItems { get; set; }
        public PAjaxOneStatus ReturnStatus { get; set; }
    }
    [Serializable]
    public class PSaleOrderReturnItem
    {
        public long SaleOrderReturnItemID { get; set; }
        public PSaleOrderDeliveryItem SaleOrderDeliveryItem { get; set; }
        public decimal Qty { get; set; }

    }
    [Serializable]
    public class PSaleOrderStatus
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }
    
    [Serializable]
    public class PSaleOrder_Insert
    {
        public long SaleOrderID { get; set; } 
        public int DealerID { get; set; }
        public int OfficeID { get; set; }
        public long CustomerID { get; set; }
        
        public int StatusID { get; set; } 
        public string ContactPersonNumber { get; set; }
        public int DivisionID { get; set; }
        public string Remarks { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; } 
        public string InsurancePaidBy { get; set; }
        public string FrieghtPaidBy { get; set; }
        public string Attn { get; set; }
        public int? ProductID { get; set; }
        public long? EquipmentID { get; set; }
        public string TaxType { get; set; } 
        public int SaleOrderTypeID { get; set; }
        public int? SalesEngineerID { get; set; }
        public decimal HeaderDiscountPercentage { get; set; }
        public string RefNumber { get; set; }
        public DateTime? RefDate { get; set; }
        public List<PSaleOrderItem_Insert> SaleOrderItems { get; set; }

    }
    [Serializable]
    public class PSaleOrderItem_Insert
    {
        public long SaleOrderID { get; set; }
        public long SaleOrderItemID { get; set; }
        public int MaterialID { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string HSN { get; set; }
        public string UOM { get; set; }
        public decimal Quantity { get; set; }
        public decimal PerRate { get; set; }
        public decimal Value { get; set; }
        public decimal ItemDiscountValue { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal FreightValue { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }
        public int StatusID { get; set; }
        public decimal Tcs { get; set; }
        public decimal NetAmount { get; set; }
        public Decimal OnOrderQty { get; set; }
        public Decimal TransitQty { get; set; }
        public Decimal UnrestrictedQty { get; set; }
    }
    [Serializable]
    public class PSaleOrderReturnItem_Insert
    {
        public long SaleOrderDeliveryID { get; set; }
        public long SaleOrderDeliveryItemID { get; set; }
        public int MaterialID { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string UOM { get; set; }
        public decimal Qty { get; set; }
        public string Remarks { get; set; }
    }
    [Serializable]
    public class PSaleOrderType
    {
        public int SaleOrderTypeID { get; set; }
        public string SaleOrderType { get; set; }
        public Boolean IsActive { get; set; }
    }
    [Serializable]
    public class PSaleOrderDeliveryItem_Insert
    {
        public long SaleOrderID { get; set; }
        public long? ShiftToID { get; set; }
        public long SaleOrderItemID { get; set; }
        public int MaterialID { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string UOM { get; set; }
        public decimal Quantity { get; set; }
        public decimal DeliveryQuantity { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; } 
        public long? EquipmentHeaderID { get; set; } 
    }
    [Serializable]
    public class PSaleOrderDeliveryShipping
    {
        public long SaleOrderDeliveryID { get; set; }
        public decimal NetWeight { get; set; }
        public DateTime DispatchDate { get; set; }
        public string CourierID { get; set; }
        public DateTime CourierDate { get; set; }
        public string CourierCompanyName { get; set; }
        public string CourierPerson { get; set; }
        public string LRNo { get; set; }
        public string PackingDescription { get; set; }
        public string PackingRemarks { get; set; }
        public string TransportDetails { get; set; }
        public string TransportMode { get; set; }
        public DateTime PickupDate { get; set; }
        public string Remarks { get; set; }
    }
}
