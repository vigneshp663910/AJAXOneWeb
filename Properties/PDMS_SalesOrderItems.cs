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
        //public decimal? SoQty { get; set; }

        //public string PartNumber { get; set; }
        //public string Description { get; set; }
        //public string MatType { get; set; }
        //public string Division { get; set; }

        //public decimal Qty { get; set; }
        //public decimal Basic { get; set; }
        //public decimal Discount { get; set; }
        //public decimal BasicAfterDisc { get; set; }
        //public decimal Tax { get; set; }
        //public decimal FreightInsurance { get; set; }
        //public decimal TotalAmt { get; set; }
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
    public class PDMS_SalesInvoiceReturn
    {
        public long InvoiceReturnID { get; set; }
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
        public string SaleOrderNumber { get; set; }
        public DateTime? SaleOrderDate { get; set; }
        public PDMS_SalesInvoiceReturnItem InvoiceItem { get; set; }
        public List<PDMS_SalesInvoiceReturnItem> InvoiceItems { get; set; }

        public string Reference { get; set; }
        public DateTime? ReferenceDate { get; set; }
        public string McMode { get; set; }
        public string MachineSlNo { get; set; }
        public string PaymentTerms { get; set; }
        public string CreditDays { get; set; }
        public string Remarks { get; set; }

    }
    [Serializable]
    public class PDMS_SalesInvoiceReturnItem
    {
        public long InvoiceReturnItemID { get; set; }
        public long ItemCount { get; set; }
        public int ItemNo { get; set; }
        public PDMS_Material Material { get; set; }
        public decimal UnitBasicPrice { get; set; }
        public decimal Qty { get; set; }
        public decimal ReturnQty { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal ApprovedQty { get; set; }
        public decimal TotalQty { get; set; } 
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
    }

    [Serializable]
    public class PDMS_SalesType
    {
        public int SalesTypeID { get; set; }
        public string SalesType { get; set; }
        public int SalesTypeCode { get; set; }
            
    }

    [Serializable]
    public class PDMS_SalesOrderJSON
    {
        public string bo_id { get; set; }
        public string msg_id { get; set; }
        public string msg_type { get; set; }
        public string establishment { get; set; }
        public string fromentityname { get; set; }
        public string tenant { get; set; }
        public string flavor { get; set; }
        public string is_master_data { get; set; }
        public IEnumerable<PDMS_SalesOrderResultsJSON> results { get; set; }
    }
    [Serializable]
    public class PDMS_SalesOrderResultsJSON
    {
        public string p_so_id { get; set; }
        public string f_po_id { get; set; }
        public string r_ext_id { get; set; }
        public string f_customer_id { get; set; }
        public string f_currency { get; set; }
        public string f_location { get; set; }
        public string r_insurance_p { get; set; }
        public string r_tax_amt { get; set; }
        public string r_net_amt { get; set; }
        public string r_req_del_date { get; set; }
        public string f_ship_to { get; set; }
        public string r_doc_flow_id { get; set; }
        public string r_description { get; set; }
        public string r_contact_no { get; set; }
        public string r_gross_amt { get; set; }
        public string r_contact_prsn { get; set; }
        public string r_discount_amt { get; set; }
        public string f_division { get; set; }
        public string f_office { get; set; }
        public string f_order_type { get; set; }
        public string r_exp_del_date { get; set; }
        public string r_remarks { get; set; }
        public string r_frieght_p { get; set; }
        public string r_order_date { get; set; }
        public string r_discount_amt_additional { get; set; }
        public string f_sales_office { get; set; }
        public string t_ship_to_addr { get; set; }
        public string t_delivery_id { get; set; }
        public string s_modified_by { get; set; }
        public string t_rem_amt { get; set; }
        public string s_tenant_id { get; set; }
        public string t_activity_id { get; set; }
        public string t_tenant_id { get; set; }
        public string t_invoice_id { get; set; }
        public string s_status { get; set; }
        public string s_created_by { get; set; }
        public string s_establishment { get; set; }
        public string s_modified_on { get; set; }
        public string t_action { get; set; }
        public string s_object_type { get; set; }
        public string t_org_name { get; set; }
        public string t_org_nick_name { get; set; }
        public string t_location_desc { get; set; }
        public string r_model { get; set; }
        public string r_model_no { get; set; }
        public string s_created_on { get; set; }
        public string channel { get; set; }
        public IEnumerable<PDMS_dssor_sales_order_hdr_itemsJSON> dssor_sales_order_hdr_items { get; set; }
    }
    [Serializable]
    public class PDMS_dssor_sales_order_hdr_itemsJSON
    {
        public string r_item_type { get; set; } 
        public string r_hgl_item { get; set; }
        public string d_material_desc { get; set; }
        public string f_po_id { get; set; }
        public string f_material_id { get; set; }
        public string r_order_qty { get; set; }
        public string r_approved_qty { get; set; }
        public string r_gross_amt { get; set; }
        public string r_pending_qty { get; set; }
        public string r_cancel_qty { get; set; }
        public string r_net_amt { get; set; }
        public string f_oem_id { get; set; }
        public string r_tax_amt { get; set; }
        public string f_uom { get; set; }
        public string f_location { get; set; }
        public string r_shiped_qty { get; set; }
        public string f_office { get; set; }
        public string p_so_item { get; set; }
        public string r_resvered_qty { get; set; }
        public string r_doc_flow_id { get; set; }
        public string r_discount_amt { get; set; }
        public string r_add_discount_amt { get; set; }
        public string r_exp_del_date { get; set; }
        public string r_unit_price { get; set; }
        public string f_po_item { get; set; }
        public string f_mat_division { get; set; }
        public string s_modified_by { get; set; }
        public string p_so_id { get; set; }   
        public string s_tenant_id { get; set; }
        public string s_created_on { get; set; }
        public string r_indicator { get; set; }
        public string t_check_avail_count { get; set; }
        public string l_status_icon { get; set; }
        public string s_status { get; set; }
        public string t_check_avail_count_css { get; set; }
        public string t_batch { get; set; }
        public string s_created_by { get; set; }
        public string t_existing_css { get; set; }   
        public string s_establishment { get; set; }
        public string s_modified_on { get; set; }
        public string t_action { get; set; }
        public string s_object_type { get; set; }
        public string l_status { get; set; }
        public string t_scheme_item_edit { get; set; }
        public string t_item_quantity_is_edit { get; set; }
        public string t_free_goods { get; set; }
        public string channel { get; set; }
        public IEnumerable<PDMS_dssor_sales_order_item_condsJSON> dssor_sales_order_item_conds { get; set; }
    }
    [Serializable]
    public class PDMS_dssor_sales_order_item_condsJSON
    {
        public string f_po_id { get; set; }
        public string f_currency { get; set; }
        public string r_cond_amt { get; set; }
        public string r_order_qty { get; set; }
        public string r_base_amt { get; set; }
        public string s_modified_by { get; set; }
        public string p_so_id { get; set; }
        public string s_tenant_id { get; set; }
        public string r_pric_date { get; set; }
        public string s_created_by { get; set; }
        public string s_created_on { get; set; }
        public string s_establishment { get; set; }
        public string s_modified_on { get; set; }
        public string r_cond_grp { get; set; }
        public string r_cond_cls { get; set; }
        public string d_cond_desc { get; set; }
        public string f_percentage { get; set; }
        public string p_so_item { get; set; }
        public string p_condition_type { get; set; }
        public string channel { get; set; } 
    }

    [Serializable]
    public class PDMS_PrimaryPurchaseOrder
    {
        public string PrimaryPurchaseOrderID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }
      
    }
    [Serializable]
    public class PDMS_WebQuotation
    {
        public long WebQuotationID { get; set; }
        public string WebQuotationNumber { get; set; }
        public DateTime WebQuotationDate { get; set; }
        public string SalesOrderNumber { get; set; }
        public DateTime? SalesOrderDate { get; set; }
        public PDMS_PrimaryPurchaseOrder PrimaryPurchaseOrder { get; set; }
        public PDMS_PrimaryInvoice PrimaryInvoice { get; set; }

        public PDMS_WebQuotationStatus WebQuotationStatus { get; set; }

        public PDMS_Dealer Dealer { get; set; }

        public PDMS_Customer Customer { get; set; }
        public string Office { get; set; }
        public PDMS_Customer BillTo { get; set; }
        public PDMS_Customer ShipTo { get; set; }

        public PDMS_Address InvoiceAddress { get; set; }
        public PDMS_Address ShipToAddress { get; set; }

        public string SalesOrderStatus { get; set; }

        public PDMS_MainApplication Usage { get; set; }
        public string RetailCustomer { get; set; }
        // public string Hypothecation { get; set; }  
        public PDMS_SourceOfEnquiry SourceOfEnquiry { get; set; }
        public string ReasonForOrderConversion { get; set; }
        public string CustomerType { get; set; }
        public string Profile { get; set; }
        public string Size { get; set; }
        public string OwnershipPattern { get; set; }
        public string NameOfTheProject { get; set; }
        public PDMS_DiscountType ModeOfBilling { get; set; }


        //Financier 
        public PDMS_Financier Financier { get; set; }
        public Decimal? InvoiceValue { get; set; }
        public string DoNumber { get; set; }
        public DateTime? DoDate { get; set; }
        public PDMS_PaymentTerm CreditDays { get; set; }
        public Decimal? DoAmount { get; set; }
        public Decimal? MarginMoney { get; set; }
        public PDMS_IncoTerm IncoTerm { get; set; }
        public Decimal? AdvanceAmount { get; set; }
        //public Decimal? FinancierAmount { get; set; }
        public string BenificiaryOfDO { get; set; }
        public Decimal? SubventionAmount { get; set; }
        public string BackToBackDoEndorsedToAjax { get; set; }
        public string TransportationAndInsurance { get; set; }

        //Foc
        public string SpecialRequirements { get; set; }
        public string FocServiceKit { get; set; }
        public string FocWheelAssy { get; set; }
        public string FocExtensionChutes { get; set; }
        public string FocOthers { get; set; }

        //Sales
        public PDMS_EquipmentHeader Equipment { get; set; }
        public Decimal? DiscountSales { get; set; }
        public Decimal? FreightValue { get; set; }
        public Decimal? InsuranceValue { get; set; }
        public DateTime? TRDate { get; set; }
        public Boolean ConsolidationInvoicePrint { get; set; }
        public Decimal? FreightAmount { get; set; }
        public string Billing { get; set; }
        public PDMS_WebQuotationItem WebQuotationItem { get; set; }
        public List<PDMS_WebQuotationItem> WebQuotationItems { get; set; }

        public PUser Approver { get; set; }
        public Boolean SendToSAP { get; set; }
        public Boolean IsSuccess { get; set; }
        public string SapStatus
        {
            get
            {
                return SendToSAP ? (IsSuccess ? "Success" : "Not Success") : "Not Send To SAP";
            }
        }
    }

    [Serializable]
    public class PDMS_WebQuotationItem
    {
        public long WebQuotationItemID { get; set; }
        public long WebQuotationID { get; set; }
        public PDMS_Material Material { get; set; }
        public int Qty { get; set; }
        public Decimal BasicPrice { get; set; }
        public Decimal? Discount1 { get; set; }
        public Decimal? Discount2 { get; set; }
        public Decimal? Discount3 { get; set; }
    }

    [Serializable]
    public class PDMS_PrimaryInvoice
    {
        public string PrimaryInvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }

        public string SalesOrderNumber { get; set; }
        public DateTime? SalesOrderDate { get; set; }
        public PDMS_PrimaryPurchaseOrder PrimaryPurchaseOrder { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }
      
        public string SalesOrderStatus { get; set; }

       // public PDMS_ModeOfBilling ModeOfBilling { get; set; }
        public PDMS_Address ShipToPartyAddress { get; set; }
        public string Plant { get; set; }
        public Decimal Usage { get; set; }
        public Decimal Discount { get; set; }       
    }
   
    public class PDMS_ModeOfBilling1
    {
        public int ModeOfBillingID { get; set; }
        public string ModeOfBilling { get; set; }
        public int ModeOfBillingCode { get; set; }

    }
    public class PDMS_TermsOfPayment
    {
        public int TermsOfPaymentID { get; set; }
        public string TermsOfPayment { get; set; }
        public int TermsOfPaymentCode { get; set; }

    }
    public class PDMS_WebQuotationStatus
    {
        public Int16 WebQuotationStatusID { get; set; }
        public string WebQuotationStatus { get; set; }  
    }
}
