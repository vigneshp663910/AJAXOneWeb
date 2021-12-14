using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    static public class PQuery
    {
        public const string GetSalesOrder = " select  t.description  as Dealer_Name ,bp.p_bp_id ,bp.r_org_name ,bps.r_value as GSTNO , so.p_so_Id"
+ " ,so.r_order_date ,so.s_status So_status ,inv.p_inv_id ,inv.r_inv_date ,soi.f_material_id ,mm.r_description ,mm.r_hsn_id ,soi.r_unit_price"
+ " ,soi.r_order_qty,soi.r_gross_amt, soi.r_discount_amt  ,ZFRH.r_cond_amt  as freight ,SGST.r_cond_amt as SGSTAmt , SGST.f_percentage as SGST"
+ ",CGST.r_cond_amt as CGSTAmt ,CGST.f_percentage as CGST ,IGST.r_cond_amt  as IGSTAmt ,IGST.f_percentage  as IGST  ,mm.f_Mat_Type "
+ ",so.f_Division ,so.f_location  ,so.r_contact_prsn ,so.r_contact_no ,so.s_tenant_id as Dealer_Code"
+ " from  dssor_sales_order_hdr so "
+ " inner join dssor_sales_order_item soi on so.p_so_Id = soi.p_so_Id"
+ " inner join m_tenant t on t.tenantid = so.s_tenant_id"
+ " inner join doohr_bp bp on bp.p_bp_id = so.f_customer_id  and bp.s_tenant_id <> 20   and bp.s_tenant_id = so.s_tenant_id"
+ " left join af_m_materials mm on mm.p_material = soi.f_material_id  "
+ " left Join dsinr_inv_hdr inv on inv.f_so_id = so.p_so_Id	"
+ " left Join  doohr_bp_statutory bps on bps.p_bp_id =bp.p_bp_id and bps.r_statutory_type='GSTIN'  and bps.s_tenant_id <> 20  and  bps.s_tenant_id = so.s_tenant_id"
+ " left join dssor_sales_order_cond SGST on SGST.p_so_id = soi.p_so_Id and SGST.p_so_item = soi.p_so_item and SGST.p_condition_type = 'ZOSG'"
+ " left join dssor_sales_order_cond CGST on CGST.p_so_id = soi.p_so_Id and CGST.p_so_item = soi.p_so_item and CGST.p_condition_type = 'ZOCG'"
+ "left join dssor_sales_order_cond IGST on IGST.p_so_id = soi.p_so_Id and IGST.p_so_item = soi.p_so_item and IGST.p_condition_type = 'ZICG'"
+ " left join dssor_sales_order_cond ZFRH on ZFRH.p_so_id = soi.p_so_Id and ZFRH.p_so_item = soi.p_so_item and ( ZFRH.p_condition_type = 'ZFRH' or (ZFRH.p_condition_type = 'ZFRD'))"
+ " where	1 =1 ";


        public const string GetWarrantyClaimIntegration = " select claim.p_doc_id as ClaimID"
       + " ,claim.r_doc_date as ClaimDate ,ten.tenantid Dealer_code,ten.description  Dealer_Name,p_inv_id  reference, r_inv_date   Invoice_date, "
       + " invi.p_inv_item ,f_material_id   material ,(case when   d_material_desc is null then  m.r_description else   d_material_desc end)  material_desc "
       + " , psc.r_equipment_ser_no          Machine_Serial_No, f_uom uom,r_qty qty   ,r_hsn_id as HSN_CODE,psc.f_cust_id                   BP_Code, "
       + " psc.f_ic_ticket_id              IC_Ticket, psc.f_ic_ticket_date ,invi.r_gross_amt                Base_Value ,case when  invi.r_gross_amt = invi.r_net_amt then invi.r_net_amt * 1.18 else invi.r_net_amt end   as Total"
       + " ,claim.r_levelone_apr,claim.r_levelone_date,claimi.r_levelone_amt,claim.r_leveltwo_apr,claim.r_leveltwo_date	,claimi.r_leveltwo_amt"
       + " ,claim.s_status as status ,case when psc.r_goodwill_warranty is null then null when psc.r_goodwill_warranty is true then 1 else 0 end  as r_goodwill_warranty  	 	"
       + " ,doc.r_remarks,p_sc_id,psc.r_tsir_no,psc.r_closure_date as Restore_Date, psc.r_application as application,psc.r_location_id as location , psc.r_fsr_no_date"

       + " FROM   dsinr_inv_item invi"
              + " inner JOIN dsinr_inv_hdr inv ON ( invi.k_id = inv.p_id AND invi.s_tenant_id = inv.s_tenant_id and inv.s_status <> 'CANCELLED' and 	d_inv_type_desc = 'Warranty Invoice'  )  "
              + " inner JOIN dsprr_psc_hdr psc ON ( psc.p_sc_id = inv.r_ext_id AND psc.s_tenant_id = inv.s_tenant_id ) "
            //   + " inner JOIN dsclr_claim_item claimi ON ( claimi.f_ref_doc = psc.p_sc_id    and claimi.p_doc_item= invi.p_inv_item    and claimi.f_material = invi.f_material_id ) "
             + " inner JOIN dsclr_claim_item claimi ON ( claimi.f_ref_doc = psc.p_sc_id  and claimi.f_material = invi.f_material_id ) "
              + " inner JOIN dsclr_claim_hdr claim  ON ( claim.p_doc_id = claimi.k_doc_id )  "
              + " inner join dpdcr_doc_hdr doc on doc.r_ref_doc = claim.p_doc_id"
              + " INNER  JOIN m_tenant ten ON ( ten.tenantid = invi.s_tenant_id  ) "
              + " left join af_m_materials m on m.p_material = invi.f_material_id "
              + " order by claim.p_doc_id";

        public const string GetSalesInvoiceDetails = "select  t.description  as Dealer_Name ,inv.f_customer_id ,inv.d_customer_name ,bps.r_value as GSTNO   "
  + " ,inv.p_inv_id ,inv.r_inv_date ,inv.s_status "
  + " ,invi.f_material_id ,mm.r_description ,mm.r_hsn_id "
  + " ,soi.r_unit_price "
  + " ,invi.r_qty ,invi.r_gross_amt, invi.r_discount_amt  "
  + " ,ZFRH.r_amt  as freight ,SGST.r_amt as SGSTAmt , SGST.r_percentage as SGST "
  + " ,SGST.r_amt as CGSTAmt ,SGST.r_percentage as CGST ,IGST.r_amt  as IGSTAmt ,IGST.r_percentage  as IGST  ,mm.f_Mat_Type  "
  + " ,inv.f_Division ,inv.f_location   "
  + " ,so.r_contact_prsn ,so.r_contact_no  "
  + " ,inv.s_tenant_id as Dealer_Code "
  + " from  dsinr_inv_hdr inv  "
  + " inner join dsinr_inv_item invi on invi.k_id = inv.p_id "
  + " inner join dssor_sales_order_item soi on  soi.p_so_Id = inv.f_so_id 	and soi.f_material_id = invi.f_material_id "
  + " inner join dssor_sales_order_hdr so on    so.p_so_Id = soi.p_so_Id "
  + " inner join m_tenant t on t.tenantid = inv.s_tenant_id  "
  + " left join af_m_materials mm on mm.p_material = invi.f_material_id     "
  + " left Join  doohr_bp_statutory bps on bps.p_bp_id =inv.f_customer_id and bps.r_statutory_type='GSTIN'  and bps.s_tenant_id <> 20  and  bps.s_tenant_id = inv.s_tenant_id "
  + " left join dsinr_inv_cond SGST on SGST.k_inv_id = invi.k_inv_id and SGST.k_inv_item = invi.p_inv_item and SGST.k_cond_type = 'ZOSG' "
            // + " left join dsinr_inv_cond CGST on CGST.k_inv_id = invi.k_inv_id and CGST.k_inv_item = invi.p_inv_item and CGST.k_cond_type = 'ZOCG' "
  + " left join dsinr_inv_cond IGST on IGST.k_inv_id = invi.k_inv_id and IGST.k_inv_item = invi.p_inv_item and IGST.k_cond_type = 'ZICG' "
  + " left join dsinr_inv_cond ZFRH on ZFRH.k_inv_id = invi.k_inv_id and ZFRH.k_inv_item = invi.p_inv_item and (ZFRH.k_cond_type = 'ZFRH' or (ZFRH.k_cond_type = 'ZFRD')) "
  + " where	1 =1   ";

         

        public const string GetSalesInvoiceDealerAndMaterialWise = " select Dealer,Dealer_Name,material,material_description,sum(qty) as qty,sum(net_amt) as net_amt ,sum(gross_amt) as gross_amt "
+ " ,sum(item_count) as item_count,count(*) as Header_count from ( select  	inv.s_tenant_id as Dealer	,t.description  as Dealer_Name ,invi.f_material_id as  material "
    + " ,mm.r_description   as material_description ,sum(invi.r_qty) as qty ,sum(invi.r_net_amt) as net_amt	,sum(invi.r_gross_amt) as gross_amt ,count(*) as item_count "
+ " from  dsinr_inv_hdr inv   inner join dsinr_inv_item invi on invi.k_id = inv.p_id  inner join m_tenant t on t.tenantid = inv.s_tenant_id   "
+ " left join af_m_materials mm on mm.p_material = invi.f_material_id  where	1 = 1     @@Filter "
 + " group by  inv.s_tenant_id  ,t.description  ,invi.f_material_id ,mm.r_description  ,inv.p_inv_id) t1	group by Dealer,Dealer_Name,material,material_description  ";

        public const string GetSalesInvoiceDealerCustomerAndMaterialWise = " select Dealer,Dealer_Name,customer,customer_name,material,material_description,sum(qty) as qty,sum(net_amt) as net_amt ,sum(gross_amt) as gross_amt "
  + " ,sum(item_count) as item_count,count(*) as Header_count from ( select  	inv.s_tenant_id as Dealer	,t.description  as Dealer_Name, inv.f_customer_id as customer,inv.d_customer_name as customer_name ,invi.f_material_id as  material "
      + " ,mm.r_description   as material_description ,sum(invi.r_qty) as qty ,sum(invi.r_net_amt) as net_amt	,sum(invi.r_gross_amt) as gross_amt ,count(*) as item_count "
  + " from  dsinr_inv_hdr inv   inner join dsinr_inv_item invi on invi.k_id = inv.p_id  inner join m_tenant t on t.tenantid = inv.s_tenant_id   "
  + " left join af_m_materials mm on mm.p_material = invi.f_material_id  where	1 = 1     @@Filter "
   + " group by  inv.s_tenant_id  ,t.description,inv.f_customer_id,inv.d_customer_name  ,invi.f_material_id ,mm.r_description  ,inv.p_inv_id) t1	group by Dealer,Dealer_Name,customer,customer_name,material,material_description  ";



        public const string GetWarrantyInvoiceIntegration = "select m.tax_percentage , inv.p_inv_id as Invoice_number,inv.r_inv_date Invoice_date,psc.f_ic_ticket_id IC_Ticket, psc.f_ic_ticket_date,inv.s_status as Invoice_status ,f_del_Id as Delivery_ID "
                  + " ,ten.tenantid Dealer_code,ten.description  Dealer_Name,invi.p_inv_item ,f_material_id   material ,(case when   d_material_desc is null then  m.r_description else   d_material_desc end)  material_desc "
                  + " , psc.r_equipment_ser_no Machine_Serial_No, f_uom uom,r_qty qty   ,r_hsn_id as HSN_CODE,psc.f_cust_id BP_Code, invi.r_gross_amt Base_Value "
                  + " ,case when  invi.r_gross_amt = invi.r_net_amt then invi.r_net_amt * 1.18 else invi.r_net_amt end   as Total "
                  + " ,case when psc.r_goodwill_warranty is null then null when psc.r_goodwill_warranty is true then 1 else 0 end  as r_goodwill_warranty "
                  + " ,p_sc_id,psc.r_tsir_no,psc.r_closure_date as Restore_Date, psc.r_application as application,psc.r_location_id as location , psc.r_fsr_no_date "
                  + " FROM   dsinr_inv_item invi inner JOIN dsinr_inv_hdr inv ON ( invi.k_id = inv.p_id AND invi.s_tenant_id = inv.s_tenant_id and  d_inv_type_desc = 'Warranty Invoice'  )  "
                  + " inner JOIN dsprr_psc_hdr psc ON ( psc.p_sc_id = inv.r_ext_id AND psc.s_tenant_id = inv.s_tenant_id ) INNER  JOIN m_tenant ten ON ( ten.tenantid = invi.s_tenant_id  )  "
                  + " left join af_m_materials m on m.p_material = invi.f_material_id  @@Fillter    order by inv.p_inv_id,psc.f_ic_ticket_id  ";
    }
}