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
    public class PSalesOrder
    {
        public long SalesOrderID { get; set; }
        public string SalesOrderNumber { get; set; }
        public DateTime SalesOrderDate { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string SalesOrderStatus { get; set; }
        public PDMS_SalesOrderItems SalesOrderItem { get; set; }
        public List<PDMS_SalesOrderItems> SalesOrderItems { get; set; }
    }
    [Serializable]
    public class PSalesOrderItems
    {
        public long SalesOrderItemID { get; set; }  
        public PDMS_Material Material { get; set; } 
        public decimal Qty { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal FreightAmount { get; set; }
        public decimal TaxableAmount { get; set; } 
        public decimal Tax { get; set; }
        public decimal TotalAmt { get; set; } 
    }

    //[Serializable]
    //public class PDMS_SalesInvoiceReturn
    //{
    //    public long InvoiceReturnID { get; set; }
    //    public long HeaderCount { get; set; }
    //    public PDMS_Dealer Dealer { get; set; }
    //    public PDMS_Customer Customer { get; set; }
    //    public string GSTNo { get; set; }
    //    public string InvoiceNumber { get; set; }
    //    public DateTime? InvoiceDate { get; set; }
    //    public string Status { get; set; }
    //    public PDMS_SalesType SalesType { get; set; }

    //    public string Division { get; set; }
    //    public string Location { get; set; }
    //    public string ContactPerson { get; set; }
    //    public string ContactNumber { get; set; }
    //    public string SaleOrderNumber { get; set; }
    //    public DateTime? SaleOrderDate { get; set; }
    //    public PDMS_SalesInvoiceReturnItem InvoiceItem { get; set; }
    //    public List<PDMS_SalesInvoiceReturnItem> InvoiceItems { get; set; }

    //    public string Reference { get; set; }
    //    public DateTime? ReferenceDate { get; set; }
    //    public string McMode { get; set; }
    //    public string MachineSlNo { get; set; }
    //    public string PaymentTerms { get; set; }
    //    public string CreditDays { get; set; }
    //    public string Remarks { get; set; }

    //}
    //[Serializable]
    //public class PDMS_SalesInvoiceReturnItem
    //{
    //    public long InvoiceReturnItemID { get; set; }
    //    public long ItemCount { get; set; }
    //    public int ItemNo { get; set; }
    //    public PDMS_Material Material { get; set; }
    //    public decimal UnitBasicPrice { get; set; }
    //    public decimal Qty { get; set; }
    //    public decimal ReturnQty { get; set; }
    //    public decimal ReceivedQty { get; set; }
    //    public decimal ApprovedQty { get; set; }
    //    public decimal TotalQty { get; set; }
    //    public decimal Value { get; set; }
    //    public decimal Discount { get; set; }
    //    public decimal DiscountedPrice { get; set; }
    //    public decimal FreightAmount { get; set; }
    //    public decimal TaxableAmount { get; set; }

    //    public decimal NetAmount { get; set; }
    //    public decimal GrossAmount { get; set; }

    //    public decimal? SGST { get; set; }
    //    public decimal SGSTAmt { get; set; }
    //    public decimal? CGST { get; set; }
    //    public decimal CGSTAmt { get; set; }
    //    public decimal? IGST { get; set; }
    //    public decimal IGSTAmt { get; set; }
    //    public decimal Tax { get; set; }
    //    public decimal TaxP { get; set; }
    //    public decimal TotalAmt { get; set; }
    //}
}
