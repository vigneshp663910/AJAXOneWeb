using DataAccess;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Script.Serialization;

namespace Business
{
    public class BDMS_PurchaseOrder
    {
        private IDataAccess provider;
        public BDMS_PurchaseOrder()
        {
            try
            {
                provider = new ProviderFactory().GetProvider();
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_ICTicket", "provider : " + e1.Message, null);
            }
        }
        public List<PDMS_PurchaseOrder> GetPurchaseOrder(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_PurchaseOrder> POs = new List<PDMS_PurchaseOrder>();
            try
            {
                string Query = "select po.p_po_id po,po.s_created_on po_date,po.s_tenant_id,ten.description tenant_name,po.f_location,po.f_bill_to,po.f_currency,po.r_insurance_p"
                    + ",po.r_tax_amt r_tax_amtH,po.f_sold_to,po.s_status,po.r_net_amt  r_net_amtH,po.r_gross_amt  r_gross_amtH,po.r_discount_amt  r_discount_amtH,po.f_division,poi.p_po_item "
         + ",poi.f_material_id,mmh.r_hsn_id,poi.r_order_qty,poi.r_tax_amt,poi.f_uom,poi.r_net_amt,poi.d_material_desc,poi.r_gross_amt,poi.r_shiped_qty,poi.r_discount_amt"
           + ",poi.r_unit_price,poi.r_approved_qty,SGST.r_cond_amt SGST,CGST.r_cond_amt CGST,IGST.r_cond_amt IGST,F.r_cond_amt ZFRH,I.r_cond_amt ZINS,B.r_cond_amt  ZPKF,o.r_desc  PO_Type"
               + " from dppor_purc_order_hdr po "
+ " inner join dppor_purc_order_item poi on poi.k_po_id = po.p_po_id  "
+ " inner join m_tenant ten on ten.tenantid = po.s_tenant_id  "
+ " left join maf_object_type o on o.s_object_type = po.s_object_type and o.k_entityname='dppor_purc_order_hdr' and o.is_deleted is null "
+ " left join dmmmr_mmaster mmh on mmh.p_material = poi.f_material_id and mmh.s_tenant_id = po.s_tenant_id "

+ " left Join dppor_purc_order_cond SGST  on SGST.k_po_id = poi.k_po_id and SGST.k_po_item =poi.p_po_item and SGST.p_condition_type ='JOSG' "
+ " left Join dppor_purc_order_cond CGST  on CGST.k_po_id = poi.k_po_id and CGST.k_po_item =poi.p_po_item and CGST.p_condition_type ='JOCG' "
+ " left Join dppor_purc_order_cond IGST  on IGST.k_po_id = poi.k_po_id and IGST.k_po_item =poi.p_po_item and IGST.p_condition_type ='JOIG' "

+ " left Join dppor_purc_order_cond F  on F.k_po_id = poi.k_po_id and F.k_po_item =poi.p_po_item and F.p_condition_type ='ZFRH' "
+ " left Join dppor_purc_order_cond I  on I.k_po_id = poi.k_po_id and I.k_po_item =poi.p_po_item and I.p_condition_type ='ZINS' "
+ " left Join dppor_purc_order_cond B  on B.k_po_id = poi.k_po_id and B.k_po_item =poi.p_po_item and B.p_condition_type ='ZPKF' where 1=1 "
+ filter;

                // string Query = "SELECT  * from pr_get_purchase_order(" + filter + ")";
                DataTable dt = new NpgsqlServer().ExecuteReader(Query);
                PDMS_PurchaseOrder PO = new PDMS_PurchaseOrder();
                foreach (DataRow dr in dt.Rows)
                {
                    PO = new PDMS_PurchaseOrder();
                    PO.PurchaseOrderID = Convert.ToString(dr["po"]);
                    PO.PurchaseOrderDate = Convert.ToDateTime(dr["po_date"]);
                    PO.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]), DealerName = Convert.ToString(dr["tenant_name"]) };
                    PO.Location = Convert.ToString(dr["f_location"]);
                    PO.Currency = Convert.ToString(dr["f_currency"]);
                    PO.BillTo = Convert.ToString(dr["f_bill_to"]);

                    PO.Insurance = Convert.ToString(dr["r_insurance_p"]);
                    PO.TaxAmount = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    PO.SoldTo = Convert.ToString(dr["f_sold_to"]);
                    PO.POStatus = Convert.ToString(dr["s_status"]);
                    PO.NetAmount = DBNull.Value == dr["r_net_amt"] ? 0 : Convert.ToDecimal(dr["r_net_amt"]);
                    PO.GrossAmount = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);
                    PO.DiscountAmount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    PO.Division = Convert.ToString(dr["f_division"]);
                    PO.POType = Convert.ToString(dr["PO_Type"]);

                    PO.PurchaseOrderItem = new PDMS_PurchaseOrderItem();
                    PO.PurchaseOrderItem.POItem = Convert.ToInt32(dr["p_po_item"]);
                    PO.PurchaseOrderItem.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["f_material_id"]),
                        MaterialDescription = Convert.ToString(dr["d_material_desc"]),
                        HSN = Convert.ToString(dr["r_hsn_id"]),
                        // PO.PurchaseOrderItem.Material.MaterialType = Convert.ToString(dr["f_material_id"]);
                    };

                    PO.PurchaseOrderItem.OrderQuantity = DBNull.Value == dr["r_order_qty"] ? 0 : Convert.ToDecimal(dr["r_order_qty"]);
                    PO.PurchaseOrderItem.TaxAmount = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    PO.PurchaseOrderItem.UOM = Convert.ToString(dr["f_uom"]);
                    PO.PurchaseOrderItem.NetAmount = DBNull.Value == dr["r_net_amt"] ? 0 : Convert.ToDecimal(dr["r_net_amt"]);

                    PO.PurchaseOrderItem.GrossAmount = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);
                    PO.PurchaseOrderItem.ShipedQuantity = DBNull.Value == dr["r_shiped_qty"] ? 0 : Convert.ToDecimal(dr["r_shiped_qty"]);
                    PO.PurchaseOrderItem.DiscountAmount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    PO.PurchaseOrderItem.UnitPrice = DBNull.Value == dr["r_unit_price"] ? 0 : Convert.ToDecimal(dr["r_unit_price"]);
                    PO.PurchaseOrderItem.ApprovedQuantity = DBNull.Value == dr["r_approved_qty"] ? 0 : Convert.ToDecimal(dr["r_approved_qty"]);

                    PO.PurchaseOrderItem.Fright = DBNull.Value == dr["ZFRH"] ? 0 : Convert.ToDecimal(dr["ZFRH"]);
                    PO.PurchaseOrderItem.Insurance = DBNull.Value == dr["ZINS"] ? 0 : Convert.ToDecimal(dr["ZINS"]);
                    PO.PurchaseOrderItem.PackingAndForwarding = DBNull.Value == dr["ZPKF"] ? 0 : Convert.ToDecimal(dr["ZPKF"]);
                    PO.PurchaseOrderItem.SGST = DBNull.Value == dr["SGST"] ? 0 : Convert.ToDecimal(dr["SGST"]);
                    PO.PurchaseOrderItem.CGST = DBNull.Value == dr["CGST"] ? 0 : Convert.ToDecimal(dr["CGST"]);
                    PO.PurchaseOrderItem.IGST = DBNull.Value == dr["IGST"] ? 0 : Convert.ToDecimal(dr["IGST"]);

                    POs.Add(PO);
                }
                return POs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_PurchaseOrder", "GetPurchaseOrder", ex);
                throw ex;
            }
            return POs;
        }
        public List<PDMS_PurchaseOrder> GetPurchaseOrderPerformance(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_PurchaseOrder> POs = new List<PDMS_PurchaseOrder>();
            try
            {
                string Query = "select po.p_po_id  PO_No,po.r_order_date  PO_Date,po.f_location as Location "
+ " ,po.s_status PO_Status,poi.p_po_item PO_Item,poi.f_material_id Material_Number,poi.d_material_desc Material_Description,poi.r_order_qty Qty "
+ " ,poi.r_unit_price  Unit_Price,poi.r_discount_amt Discount,poi.f_uom UOM,poi.r_gross_amt Net_Amount ,poi.r_net_amt Gross_Amount,poi.r_tax_amt  Tax_Amount,o.r_desc  Order_Type,mmh.r_hsn_id HSN_Code "
+ " ,ten.tenantid Dealer_Code,ten.description Dealer_Name,SGST.r_cond_amt SGST,CGST.r_cond_amt CGST,IGST.r_cond_amt IGST,F.r_cond_amt ZFRH,I.r_cond_amt ZINS,B.r_cond_amt  ZPKF "
+ " ,asni.r_delivery_qty  ASN_Qty,asn.r_asn_date ASN_Date,gri.k_gr_id  GR_Number,gr.r_gr_date GR_Date,gri.r_delivery_qty GR_Qty,(gri.r_delivery_qty - gri.r_received_qty)   Missing_Qty "
+ " ,gri.r_damaged_qty Damaged_Qty,case when gri.r_is_wrongparts  is null then 0 when gri.r_is_wrongparts  is true then 1 else 0 end  as Wrong_Supply,gr.s_status GR_Status"
+ " FROM   dppor_purc_order_hdr po  "
+ "  inner JOIN dppor_purc_order_item poi ON  poi.k_po_id = po.p_po_id AND poi.s_tenant_id = po.s_tenant_id  and  po.s_tenant_id <> 20 "
+ " left join maf_object_type o on o.s_object_type = po.s_object_type and o.k_entityname='dppor_purc_order_hdr' and o.is_deleted is null "
+ " left join dmmmr_mmaster mmh on mmh.p_material = poi.f_material_id and mmh.s_tenant_id = po.s_tenant_id "
+ " inner join m_tenant ten on ten.tenantid = po.s_tenant_id  "
+ " left Join dppor_purc_order_cond SGST  on SGST.k_po_id = poi.k_po_id and SGST.k_po_item =poi.p_po_item and SGST.p_condition_type ='JOSG' "
+ " left Join dppor_purc_order_cond CGST  on CGST.k_po_id = poi.k_po_id and CGST.k_po_item =poi.p_po_item and CGST.p_condition_type ='JOCG' "
+ " left Join dppor_purc_order_cond IGST  on IGST.k_po_id = poi.k_po_id and IGST.k_po_item =poi.p_po_item and IGST.p_condition_type ='JOIG' "
+ " left Join dppor_purc_order_cond F  on F.k_po_id = poi.k_po_id and F.k_po_item =poi.p_po_item and F.p_condition_type ='ZFRH' "
+ " left Join dppor_purc_order_cond I  on I.k_po_id = poi.k_po_id and I.k_po_item =poi.p_po_item and I.p_condition_type ='ZINS' "
+ " left Join dppor_purc_order_cond B  on B.k_po_id = poi.k_po_id and B.k_po_item =poi.p_po_item and B.p_condition_type ='ZPKF' "
+ " left join dpasr_asn_item asni on asni.f_po_id = po.p_po_id and asni.f_po_item = poi.p_po_item "
+ " left join dpasr_asn_hdr asn on asn.p_asn_id = asni.k_asn_id   "
+ " left join dpgrr_gr_hdr gr on  gr.f_asn_id = asni.k_asn_id   "
+ " left join dpgrr_gr_item gri on gri.f_po_id = po.p_po_id and gri.f_po_item = poi.p_po_item   and gr.p_gr_id = gri.k_gr_id Where 1 = 1 " + filter;


                //          Query = "SELECT  * from pr_get_po_performance(" + filter + ")";
                DataTable dt = new NpgsqlServer().ExecuteReader(Query);
                PDMS_PurchaseOrder PO = new PDMS_PurchaseOrder();
                foreach (DataRow dr in dt.Rows)
                {
                    PO = new PDMS_PurchaseOrder();
                    PO.PurchaseOrderID = Convert.ToString(dr["po_no"]);
                    PO.PurchaseOrderDate = Convert.ToDateTime(dr["po_date"]);
                    PO.POMonth = PO.PurchaseOrderDate.ToString("MMMM");

                    PO.Location = Convert.ToString(dr["location"]);
                    PO.POStatus = Convert.ToString(dr["po_status"]);
                    PO.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["dealer_code"]), DealerName = Convert.ToString(dr["dealer_name"]) };

                    //  PO.Currency = Convert.ToString(dr["f_currency"]);
                    //   PO.BillTo = Convert.ToString(dr["f_bill_to"]);

                    //    PO.Insurance = Convert.ToString(dr["r_insurance_p"]);
                    //   PO.TaxAmount = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    // PO.SoldTo = Convert.ToString(dr["f_sold_to"]);

                    //   PO.NetAmount = DBNull.Value == dr["r_net_amt"] ? 0 : Convert.ToDecimal(dr["r_net_amt"]);
                    //    PO.GrossAmount = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);
                    //  PO.DiscountAmount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    //  PO.Division = Convert.ToString(dr["f_division"]);
                    PO.POType = Convert.ToString(dr["order_type"]);

                    PO.PurchaseOrderItem = new PDMS_PurchaseOrderItem();
                    PO.PurchaseOrderItem.POItem = Convert.ToInt32(dr["po_item"]);
                    PO.PurchaseOrderItem.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["material_number"]),
                        MaterialDescription = Convert.ToString(dr["material_description"]),
                        HSN = Convert.ToString(dr["hsn_code"]),
                        // PO.PurchaseOrderItem.Material.MaterialType = Convert.ToString(dr["f_material_id"]);
                    };

                    PO.PurchaseOrderItem.OrderQuantity = DBNull.Value == dr["qty"] ? 0 : Convert.ToDecimal(dr["qty"]);
                    PO.PurchaseOrderItem.UnitPrice = DBNull.Value == dr["unit_price"] ? 0 : Convert.ToDecimal(dr["unit_price"]);
                    PO.PurchaseOrderItem.DiscountAmount = DBNull.Value == dr["discount"] ? 0 : Convert.ToDecimal(dr["discount"]);
                    PO.PurchaseOrderItem.UOM = Convert.ToString(dr["uom"]);
                    PO.PurchaseOrderItem.NetAmount = DBNull.Value == dr["Net_Amount"] ? 0 : Convert.ToDecimal(dr["Net_Amount"]);
                    PO.PurchaseOrderItem.GrossAmount = DBNull.Value == dr["Gross_Amount"] ? 0 : Convert.ToDecimal(dr["Gross_Amount"]);

                    PO.PurchaseOrderItem.TaxAmount = DBNull.Value == dr["Tax_Amount"] ? 0 : Convert.ToDecimal(dr["Tax_Amount"]);

                    PO.PurchaseOrderItem.Fright = DBNull.Value == dr["ZFRH"] ? 0 : Convert.ToDecimal(dr["ZFRH"]);
                    PO.PurchaseOrderItem.Insurance = DBNull.Value == dr["ZINS"] ? 0 : Convert.ToDecimal(dr["ZINS"]);
                    PO.PurchaseOrderItem.PackingAndForwarding = DBNull.Value == dr["ZPKF"] ? 0 : Convert.ToDecimal(dr["ZPKF"]);


                    PO.PurchaseOrderItem.TaxableAmount = PO.PurchaseOrderItem.NetAmount + PO.PurchaseOrderItem.DiscountAmount + PO.PurchaseOrderItem.Fright + PO.PurchaseOrderItem.Insurance + PO.PurchaseOrderItem.PackingAndForwarding;

                    PO.PurchaseOrderItem.SGST = DBNull.Value == dr["SGST"] ? 0 : Convert.ToDecimal(dr["SGST"]);
                    PO.PurchaseOrderItem.CGST = DBNull.Value == dr["CGST"] ? 0 : Convert.ToDecimal(dr["CGST"]);
                    PO.PurchaseOrderItem.IGST = DBNull.Value == dr["IGST"] ? 0 : Convert.ToDecimal(dr["IGST"]);

                    PO.PurchaseOrderItem.ASNDate = DBNull.Value == dr["asn_date"] ? (DateTime?)null : Convert.ToDateTime(dr["asn_date"]);
                    PO.PurchaseOrderItem.ASNQuantity = DBNull.Value == dr["asn_qty"] ? 0 : Convert.ToDecimal(dr["asn_qty"]);
                    PO.PurchaseOrderItem.GRNumber = Convert.ToString(dr["gr_number"]);
                    PO.PurchaseOrderItem.GRDate = DBNull.Value == dr["gr_date"] ? (DateTime?)null : Convert.ToDateTime(dr["gr_date"]);
                    PO.PurchaseOrderItem.GRQuantity = DBNull.Value == dr["gr_qty"] ? 0 : Convert.ToDecimal(dr["gr_qty"]);
                    PO.PurchaseOrderItem.MissingQuantity = DBNull.Value == dr["missing_qty"] ? 0 : Convert.ToDecimal(dr["missing_qty"]);
                    PO.PurchaseOrderItem.DamagedQuantity = DBNull.Value == dr["damaged_qty"] ? 0 : Convert.ToDecimal(dr["damaged_qty"]);
                    PO.PurchaseOrderItem.GRStatus = Convert.ToString(dr["gr_status"]);
                    PO.PurchaseOrderItem.ISWrongSupplied = Convert.ToBoolean(dr["wrong_supply"]);
                    if (PO.PurchaseOrderItem.ISWrongSupplied)
                        PO.PurchaseOrderItem.WrongSupplyQuantity = DBNull.Value == dr["asn_qty"] ? 0 : Convert.ToDecimal(dr["asn_qty"]);

                    PO.PurchaseOrderItem.AsnMinusPODate = DBNull.Value == dr["asn_date"] ? (int?)null : (int)(((DateTime)PO.PurchaseOrderItem.ASNDate - PO.PurchaseOrderDate).TotalDays);
                    PO.PurchaseOrderItem.POMinusAsnQuantity = DBNull.Value == dr["asn_qty"] ? (int?)null : (int)(PO.PurchaseOrderItem.OrderQuantity - PO.PurchaseOrderItem.ASNQuantity);

                    PO.PurchaseOrderItem.GrMinusPODate = DBNull.Value == dr["gr_date"] ? (int?)null : (int)(((DateTime)PO.PurchaseOrderItem.GRDate - PO.PurchaseOrderDate).TotalDays);
                    PO.PurchaseOrderItem.POMinusGrQuantity = DBNull.Value == dr["gr_qty"] ? (int?)null : (int)(PO.PurchaseOrderItem.OrderQuantity - PO.PurchaseOrderItem.GRQuantity);
                    POs.Add(PO);

                }
                foreach (PDMS_PurchaseOrder P in POs)
                {
                    P.PurchaseOrderItem.CumulativeGrQuantity = POs.Where(item => item.PurchaseOrderID == P.PurchaseOrderID && item.PurchaseOrderItem.POItem == P.PurchaseOrderItem.POItem && item.PurchaseOrderItem.Material.MaterialCode == P.PurchaseOrderItem.Material.MaterialCode).Sum(item => (item.PurchaseOrderItem.GRQuantity == null ? 0 : (decimal)item.PurchaseOrderItem.GRQuantity));
                    P.PurchaseOrderItem.CumulativeAsnQuantity = POs.Where(item => item.PurchaseOrderID == P.PurchaseOrderID && item.PurchaseOrderItem.POItem == P.PurchaseOrderItem.POItem && item.PurchaseOrderItem.Material.MaterialCode == P.PurchaseOrderItem.Material.MaterialCode).Sum(item => (item.PurchaseOrderItem.GRQuantity == null ? 0 : (decimal)item.PurchaseOrderItem.ASNQuantity));
                    P.PurchaseOrderItem.LatestGrDate = POs.Where(item => item.PurchaseOrderID == P.PurchaseOrderID && item.PurchaseOrderItem.POItem == P.PurchaseOrderItem.POItem && item.PurchaseOrderItem.Material.MaterialCode == P.PurchaseOrderItem.Material.MaterialCode).Max(item => (item.PurchaseOrderItem.GRDate));
                }

                return POs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_PurchaseOrder", "GetPurchaseOrderPerformance", ex);
                throw ex;
            }
            return POs;
        }
        public List<PDMS_PurchaseOrder> GetPurchaseOrderPerformanceLinq(string filter1, string filter2)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_PurchaseOrder> POs = new List<PDMS_PurchaseOrder>();
            try
            {
                string Query1 = "select  po.p_po_id  PO_No,po.r_order_date PO_Date, po.f_location as Location,po.s_status PO_Status,poi.p_po_item PO_Item,poi.f_material_id Material_Number,poi.d_material_desc Material_Description,poi.r_order_qty Qty"
+ " ,poi.r_unit_price  Unit_Price,poi.r_discount_amt Discount,poi.f_uom UOM,poi.r_gross_amt Net_Amount ,poi.r_net_amt Gross_Amount,poi.r_tax_amt  Tax_Amount,o.r_desc  Order_Type,mmh.r_hsn_id HSN_Code"
+ " ,ten.tenantid Dealer_Code,ten.description Dealer_Name,asni.r_delivery_qty  ASN_Qty,asn.r_asn_date ASN_Date,gri.k_gr_id  GR_Number,gr.r_gr_date GR_Date,gri.r_delivery_qty GR_Qty"
+ " ,(gri.r_delivery_qty - gri.r_received_qty)   Missing_Qty,gri.r_damaged_qty Damaged_Qty"
+ " ,case when gri.r_is_wrongparts  is null then 0 when gri.r_is_wrongparts  is true then 1 else 0 end  as Wrong_Supply,gr.s_status GR_Status"
+ " FROM   dppor_purc_order_hdr po  "
+ "  inner JOIN dppor_purc_order_item poi ON  poi.k_po_id = po.p_po_id AND poi.s_tenant_id = po.s_tenant_id  and  po.s_tenant_id <> 20 "
+ " left join maf_object_type o on o.s_object_type = po.s_object_type and o.k_entityname='dppor_purc_order_hdr' and o.is_deleted is null "
+ " left join dmmmr_mmaster mmh on mmh.p_material = poi.f_material_id and mmh.s_tenant_id = po.s_tenant_id "
+ " inner join m_tenant ten on ten.tenantid = po.s_tenant_id  "
+ " left join dpasr_asn_item asni on asni.f_po_id = po.p_po_id and asni.f_po_item = poi.p_po_item "
+ " left join dpasr_asn_hdr asn on asn.p_asn_id = asni.k_asn_id   "
+ " left join dpgrr_gr_hdr gr on  gr.f_asn_id = asni.k_asn_id   "
+ " left join dpgrr_gr_item gri on gri.f_po_id = po.p_po_id and gri.f_po_item = poi.p_po_item   and gr.p_gr_id = gri.k_gr_id Where 1 = 1 " + filter1;
                string Query2 = "select  po.p_po_id  PO_No,poi.p_po_item PO_Item,poi.s_tenant_id , T.p_condition_type,T.r_cond_amt  "
                    + " from  dppor_purc_order_hdr po "
                    + " inner JOIN dppor_purc_order_item poi ON  poi.k_po_id = po.p_po_id AND poi.s_tenant_id = po.s_tenant_id  and  po.s_tenant_id <> 20 "
                    + " left join maf_object_type o on o.s_object_type = po.s_object_type and o.k_entityname='dppor_purc_order_hdr' and o.is_deleted is null "
                    + " left Join dppor_purc_order_cond T  on  T.k_po_id = poi.k_po_id and  T.k_po_item =poi.p_po_item  Where 1 = 1 " + filter2;
                //          Query = "SELECT  * from pr_get_po_performance(" + filter + ")";
                DataTable dt1 = new NpgsqlServer().ExecuteReader(Query1);
                DataTable dt2 = new NpgsqlServer().ExecuteReader(Query2);

                PDMS_PurchaseOrder PO = new PDMS_PurchaseOrder();
                foreach (DataRow dr in dt1.Rows)
                {
                    PO = new PDMS_PurchaseOrder();
                    PO.PurchaseOrderID = Convert.ToString(dr["po_no"]);
                    PO.PurchaseOrderDate = Convert.ToDateTime(dr["po_date"]);
                    PO.POMonth = PO.PurchaseOrderDate.ToString("MMMM");

                    PO.Location = Convert.ToString(dr["location"]);
                    PO.POStatus = Convert.ToString(dr["po_status"]);
                    PO.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["dealer_code"]), DealerName = Convert.ToString(dr["dealer_name"]) };

                    //  PO.Currency = Convert.ToString(dr["f_currency"]);
                    //   PO.BillTo = Convert.ToString(dr["f_bill_to"]);

                    //    PO.Insurance = Convert.ToString(dr["r_insurance_p"]);
                    //   PO.TaxAmount = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    // PO.SoldTo = Convert.ToString(dr["f_sold_to"]);

                    //   PO.NetAmount = DBNull.Value == dr["r_net_amt"] ? 0 : Convert.ToDecimal(dr["r_net_amt"]);
                    //    PO.GrossAmount = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);
                    //  PO.DiscountAmount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    //  PO.Division = Convert.ToString(dr["f_division"]);
                    PO.POType = Convert.ToString(dr["order_type"]);

                    PO.PurchaseOrderItem = new PDMS_PurchaseOrderItem();
                    PO.PurchaseOrderItem.POItem = Convert.ToInt32(dr["po_item"]);
                    PO.PurchaseOrderItem.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["material_number"]),
                        MaterialDescription = Convert.ToString(dr["material_description"]),
                        HSN = Convert.ToString(dr["hsn_code"]),
                        // PO.PurchaseOrderItem.Material.MaterialType = Convert.ToString(dr["f_material_id"]);
                    };

                    PO.PurchaseOrderItem.OrderQuantity = DBNull.Value == dr["qty"] ? 0 : Convert.ToDecimal(dr["qty"]);
                    PO.PurchaseOrderItem.UnitPrice = DBNull.Value == dr["unit_price"] ? 0 : Convert.ToDecimal(dr["unit_price"]);
                    PO.PurchaseOrderItem.DiscountAmount = DBNull.Value == dr["discount"] ? 0 : Convert.ToDecimal(dr["discount"]);
                    PO.PurchaseOrderItem.UOM = Convert.ToString(dr["uom"]);
                    PO.PurchaseOrderItem.NetAmount = DBNull.Value == dr["Net_Amount"] ? 0 : Convert.ToDecimal(dr["Net_Amount"]);
                    PO.PurchaseOrderItem.GrossAmount = DBNull.Value == dr["Gross_Amount"] ? 0 : Convert.ToDecimal(dr["Gross_Amount"]);

                    PO.PurchaseOrderItem.TaxAmount = DBNull.Value == dr["Tax_Amount"] ? 0 : Convert.ToDecimal(dr["Tax_Amount"]);



                    PO.PurchaseOrderItem.ASNDate = DBNull.Value == dr["asn_date"] ? (DateTime?)null : Convert.ToDateTime(dr["asn_date"]);
                    PO.PurchaseOrderItem.ASNQuantity = DBNull.Value == dr["asn_qty"] ? 0 : Convert.ToDecimal(dr["asn_qty"]);
                    PO.PurchaseOrderItem.GRNumber = Convert.ToString(dr["gr_number"]);
                    PO.PurchaseOrderItem.GRDate = DBNull.Value == dr["gr_date"] ? (DateTime?)null : Convert.ToDateTime(dr["gr_date"]);
                    PO.PurchaseOrderItem.GRQuantity = DBNull.Value == dr["gr_qty"] ? 0 : Convert.ToDecimal(dr["gr_qty"]);
                    PO.PurchaseOrderItem.MissingQuantity = DBNull.Value == dr["missing_qty"] ? 0 : Convert.ToDecimal(dr["missing_qty"]);
                    PO.PurchaseOrderItem.DamagedQuantity = DBNull.Value == dr["damaged_qty"] ? 0 : Convert.ToDecimal(dr["damaged_qty"]);
                    PO.PurchaseOrderItem.GRStatus = Convert.ToString(dr["gr_status"]);
                    PO.PurchaseOrderItem.ISWrongSupplied = Convert.ToBoolean(dr["wrong_supply"]);
                    if (PO.PurchaseOrderItem.ISWrongSupplied)
                        PO.PurchaseOrderItem.WrongSupplyQuantity = DBNull.Value == dr["asn_qty"] ? 0 : Convert.ToDecimal(dr["asn_qty"]);

                    PO.PurchaseOrderItem.AsnMinusPODate = DBNull.Value == dr["asn_date"] ? (int?)null : (int)(((DateTime)PO.PurchaseOrderItem.ASNDate - PO.PurchaseOrderDate).TotalDays);
                    PO.PurchaseOrderItem.POMinusAsnQuantity = DBNull.Value == dr["asn_qty"] ? (int?)null : (int)(PO.PurchaseOrderItem.OrderQuantity - PO.PurchaseOrderItem.ASNQuantity);

                    PO.PurchaseOrderItem.GrMinusPODate = DBNull.Value == dr["gr_date"] ? (int?)null : (int)(((DateTime)PO.PurchaseOrderItem.GRDate - PO.PurchaseOrderDate).TotalDays);
                    PO.PurchaseOrderItem.POMinusGrQuantity = DBNull.Value == dr["gr_qty"] ? (int?)null : (int)(PO.PurchaseOrderItem.OrderQuantity - PO.PurchaseOrderItem.GRQuantity);

                    PO.PurchaseOrderItem.Fright = 0;
                    PO.PurchaseOrderItem.Insurance = 0;
                    PO.PurchaseOrderItem.PackingAndForwarding = 0;
                    PO.PurchaseOrderItem.SGST = 0;
                    PO.PurchaseOrderItem.CGST = 0;
                    PO.PurchaseOrderItem.IGST = 0;

                    DataRow[] ZFRH = dt2.Select("PO_No = '" + PO.PurchaseOrderID + "' AND PO_Item = '" + PO.PurchaseOrderItem.POItem + "' AND s_tenant_id = '" + PO.Dealer.DealerCode + "' and p_condition_type ='ZFRH' ");
                    if (ZFRH.Count() == 1)
                    {
                        PO.PurchaseOrderItem.Fright = DBNull.Value == ZFRH[0]["r_cond_amt"] ? 0 : Convert.ToDecimal(ZFRH[0]["r_cond_amt"]);
                    }
                    DataRow[] ZINS = dt2.Select("PO_No = '" + PO.PurchaseOrderID + "' AND PO_Item = '" + PO.PurchaseOrderItem.POItem + "' AND s_tenant_id = '" + PO.Dealer.DealerCode + "' and p_condition_type ='ZINS' ");
                    if (ZINS.Count() == 1)
                    {
                        PO.PurchaseOrderItem.Insurance = DBNull.Value == ZINS[0]["r_cond_amt"] ? 0 : Convert.ToDecimal(ZINS[0]["r_cond_amt"]);
                    }
                    DataRow[] ZPKF = dt2.Select("PO_No = '" + PO.PurchaseOrderID + "' AND PO_Item = '" + PO.PurchaseOrderItem.POItem + "' AND s_tenant_id = '" + PO.Dealer.DealerCode + "' and p_condition_type ='ZPKF' ");
                    if (ZPKF.Count() == 1)
                    {
                        PO.PurchaseOrderItem.PackingAndForwarding = DBNull.Value == ZPKF[0]["r_cond_amt"] ? 0 : Convert.ToDecimal(ZPKF[0]["r_cond_amt"]);
                    }
                    DataRow[] JOSG = dt2.Select("PO_No = '" + PO.PurchaseOrderID + "' AND PO_Item = '" + PO.PurchaseOrderItem.POItem + "' AND s_tenant_id = '" + PO.Dealer.DealerCode + "' and p_condition_type ='JOSG' ");
                    if (JOSG.Count() == 1)
                    {
                        PO.PurchaseOrderItem.SGST = DBNull.Value == JOSG[0]["r_cond_amt"] ? 0 : Convert.ToDecimal(JOSG[0]["r_cond_amt"]);
                    }
                    DataRow[] JOCG = dt2.Select("PO_No = '" + PO.PurchaseOrderID + "' AND PO_Item = '" + PO.PurchaseOrderItem.POItem + "' AND s_tenant_id = '" + PO.Dealer.DealerCode + "' and p_condition_type ='JOCG' ");
                    if (JOCG.Count() == 1)
                    {
                        PO.PurchaseOrderItem.CGST = DBNull.Value == JOCG[0]["r_cond_amt"] ? 0 : Convert.ToDecimal(JOCG[0]["r_cond_amt"]);
                    }
                    DataRow[] JOIG = dt2.Select("PO_No = '" + PO.PurchaseOrderID + "' AND PO_Item = '" + PO.PurchaseOrderItem.POItem + "' AND s_tenant_id = '" + PO.Dealer.DealerCode + "' and p_condition_type ='JOIG' ");
                    if (JOIG.Count() == 1)
                    {
                        PO.PurchaseOrderItem.IGST = DBNull.Value == JOIG[0]["r_cond_amt"] ? 0 : Convert.ToDecimal(JOIG[0]["r_cond_amt"]);
                    }


                    PO.PurchaseOrderItem.TaxableAmount = PO.PurchaseOrderItem.NetAmount + PO.PurchaseOrderItem.DiscountAmount + PO.PurchaseOrderItem.Fright + PO.PurchaseOrderItem.Insurance + PO.PurchaseOrderItem.PackingAndForwarding;


                    POs.Add(PO);
                }
                foreach (PDMS_PurchaseOrder P in POs)
                {
                    P.PurchaseOrderItem.CumulativeGrQuantity = POs.Where(item => item.PurchaseOrderID == P.PurchaseOrderID && item.PurchaseOrderItem.POItem == P.PurchaseOrderItem.POItem && item.PurchaseOrderItem.Material.MaterialCode == P.PurchaseOrderItem.Material.MaterialCode).Sum(item => (item.PurchaseOrderItem.GRQuantity == null ? 0 : (decimal)item.PurchaseOrderItem.GRQuantity));
                    P.PurchaseOrderItem.CumulativeAsnQuantity = POs.Where(item => item.PurchaseOrderID == P.PurchaseOrderID && item.PurchaseOrderItem.POItem == P.PurchaseOrderItem.POItem && item.PurchaseOrderItem.Material.MaterialCode == P.PurchaseOrderItem.Material.MaterialCode).Sum(item => (item.PurchaseOrderItem.GRQuantity == null ? 0 : (decimal)item.PurchaseOrderItem.ASNQuantity));
                    P.PurchaseOrderItem.LatestGrDate = POs.Where(item => item.PurchaseOrderID == P.PurchaseOrderID && item.PurchaseOrderItem.POItem == P.PurchaseOrderItem.POItem && item.PurchaseOrderItem.Material.MaterialCode == P.PurchaseOrderItem.Material.MaterialCode).Max(item => (item.PurchaseOrderItem.GRDate));
                }

                return POs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_PurchaseOrder", "GetPurchaseOrderPerformance", ex);
                throw ex;
            }
            return POs;
        }

        public List<PDMS_PurchaseOrder> GetPurchaseOrderMonthily(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_PurchaseOrder> POs = new List<PDMS_PurchaseOrder>();
            try
            {

                string Query = "select tenant_id, tenant_name,PO_Type , sum( gross_amt)  as gross_amt, sum(order_qty) as order_qty ,count(*) Header_count,sum( item_count) as item_count , (to_char(to_timestamp (month::text, 'MM'), 'TMmon')) || ' '   ||year  as Period "
+ " from ( select   po.s_tenant_id as tenant_id  ,ten.description tenant_name  ,o.r_desc  PO_Type ,sum( poi.r_gross_amt)  as gross_amt  ,sum( poi.r_order_qty)  as order_qty  ,count(*) item_count "
      + " , EXTRACT(MONTH FROM r_order_date ) as month ,EXTRACT(Year FROM r_order_date ) as year from  dppor_purc_order_hdr po "
      + " inner join dppor_purc_order_item poi on poi.k_po_id = po.p_po_id "
      + " inner join m_tenant ten on ten.tenantid = po.s_tenant_id "
      + " left join maf_object_type o on o.s_object_type = po.s_object_type and o.k_entityname='dppor_purc_order_hdr' and o.is_deleted is null 	   where  1 =1"
+ filter
  + " group by po.s_tenant_id ,ten.description   ,o.r_desc    ,po.p_po_id ,EXTRACT(MONTH FROM r_order_date ) ,EXTRACT(Year FROM r_order_date ) ) t  group by  tenant_id, tenant_name,PO_Type,month,year order by year,month";
                DataTable dt = new NpgsqlServer().ExecuteReader(Query);
                PDMS_PurchaseOrder PO = new PDMS_PurchaseOrder();
                foreach (DataRow dr in dt.Rows)
                {
                    PO = new PDMS_PurchaseOrder();

                    PO.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["tenant_id"]), DealerName = Convert.ToString(dr["tenant_name"]) };
                    PO.HeaderCount = Convert.ToInt64(dr["Header_count"]);
                    PO.POMonth = Convert.ToString(dr["Period"]);
                    PO.POType = Convert.ToString(dr["PO_Type"]);
                    PO.PurchaseOrderItem = new PDMS_PurchaseOrderItem();
                    PO.PurchaseOrderItem.OrderQuantity = DBNull.Value == dr["order_qty"] ? 0 : Convert.ToDecimal(dr["order_qty"]);
                    PO.PurchaseOrderItem.GrossAmount = DBNull.Value == dr["gross_amt"] ? 0 : Convert.ToDecimal(dr["gross_amt"]);
                    PO.PurchaseOrderItem.ItemCount = Convert.ToInt64(dr["item_count"]);
                    POs.Add(PO);
                }
                return POs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_PurchaseOrder", "GetPurchaseOrderMonthily", ex);
                throw ex;
            }
            return POs;
        }



        public void GetPurchaseOrderIntegration(string filter)
        {
            GetPurchaseOrderIntegration1(filter);
            GetPurchaseOrderStoIntegration(filter);
            GetAsnIntegration();
            GetGrIntegration();
            GetSapDeliveryNumberIntegration();
            GetPurchaseOrderInvoiceIntegration();


            //   GetPurchaseOrderHIntegration(filter);
            //   GetPurchaseOrderIIntegration(filter);
            //   GetPurchaseOrderStoHIntegration(filter);
            //   GetPurchaseOrderStoIIntegration(filter);
            //   GetAsnHIntegration(filter);
            //   GetAsnIIntegration(filter);
        }


        public void GetPurchaseOrderIntegration1(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_PurchaseOrderN> POs = new List<PDMS_PurchaseOrderN>();
            try
            {

                DateTime PoDate = DateTime.Now;
                string POdateString = "";

                using (DataSet DataSet = provider.Select("ZDMS_GetPurchaseOrderLastDate"))
                {
                    if (DataSet != null)
                    {
                        PoDate = Convert.ToDateTime(DataSet.Tables[0].Rows[0]["PurchaseOrderDate"]);
                        PoDate = PoDate.AddMinutes(-1);
                        POdateString = PoDate.Year.ToString() + "-" + PoDate.Month.ToString("00") + "-" + PoDate.Day.ToString("00") + " " + PoDate.ToShortTimeString();
                    }
                }

                string Query1 = "select p_po_id po, po.s_created_on po_date, po.s_tenant_id, po.f_office, f_vendor_id, f_currency , po.s_object_type, po.s_status,po.f_so_id, "
+ " po.p_po_id po,poi.p_po_item,po.s_tenant_id,poi.f_material_id, poi.r_order_qty,poi.r_tax_amt, poi.r_net_amt,poi.r_gross_amt,poi.r_shiped_qty,poi.r_discount_amt,poi.r_unit_price , poi.s_status as s_statusItem "
+" from dppor_purc_order_hdr po inner join dppor_purc_order_item poi on poi.k_po_id = po.p_po_id and poi.s_tenant_id =  po.s_tenant_id "
+ " where  po.s_created_on > '2018-07-01' and po.s_tenant_id <> 20  and  (po.s_created_on >='" + POdateString + "' or  po.s_modified_on>='" + POdateString + "') order by po.s_created_on , p_po_id";

                

                DataTable dtH = new NpgsqlServer().ExecuteReader(Query1);
                PDMS_PurchaseOrderN PO = null;
                PDMS_PurchaseOrderItemN POI = null;
                long PoID = 0;
                string PoNumber = "";
                foreach (DataRow dr in dtH.Rows)
                {                   
                    if (PoNumber != Convert.ToString(dr["po"]))
                    {
                        PO = new PDMS_PurchaseOrderN();
                        POs.Add(PO);
                        PoNumber = Convert.ToString(dr["po"]);
                        PO.PurchaseOrderNo = Convert.ToString(dr["po"]);
                        PO.PurchaseOrderDate = Convert.ToDateTime(dr["po_date"]);
                        PO.SoNumber = Convert.ToString(dr["f_so_id"]);
                        PO.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) };
                        PO.Location = new PDMS_DealerOffice();
                        PO.Location.OfficeCode = Convert.ToString(dr["f_office"]);
                        PO.Currency = Convert.ToString(dr["f_currency"]);
                        PO.Vendor = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["f_vendor_id"]) }; 
                        PO.Status = new PDMS_PurchaseOrderStatus() { Code = Convert.ToString(dr["s_status"]) }; 
                        PO.PurchaseOrderType = new PDMS_PurchaseOrderType() { Code = Convert.ToString(dr["s_object_type"]) };
                        PO.PurchaseOrderItemS = new List<PDMS_PurchaseOrderItemN>();
                    }
                    POI = new PDMS_PurchaseOrderItemN();
                    PO.PurchaseOrderItemS.Add(POI);
                    POI.Item = Convert.ToInt32(dr["p_po_item"]);
                    POI.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]) };
                    POI.Qty = DBNull.Value == dr["r_order_qty"] ? 0 : Convert.ToDecimal(dr["r_order_qty"]);
                    POI.Rate = DBNull.Value == dr["r_unit_price"] ? 0 : Convert.ToDecimal(dr["r_unit_price"]);
                    POI.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    POI.TaxableValue = (POI.Qty * POI.Rate) + POI.Discount;
                    POI.TaxValue = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    POI.ItemStatus = new PDMS_PurchaseOrderStatus() { Code = Convert.ToString(dr["s_statusItem"]) };
                }

                int c = 0;
                foreach (PDMS_PurchaseOrderN PO1 in POs)
                {
                    c = c + 1;
                    DbParameter PurchaseOrderNo = provider.CreateParameter("PurchaseOrderNo", PO1.PurchaseOrderNo, DbType.String);
                    DbParameter PurchaseOrderDate = provider.CreateParameter("PurchaseOrderDate", PO1.PurchaseOrderDate, DbType.DateTime);
                    DbParameter SoNumber = provider.CreateParameter("SoNumber", PO1.SoNumber, DbType.String);
                    DbParameter Dealer = provider.CreateParameter("Dealer", PO1.Dealer.DealerCode, DbType.String);
                    DbParameter Vendor = provider.CreateParameter("Vendor", PO1.Vendor.CustomerCode, DbType.String);
                    DbParameter OrderTo = provider.CreateParameter("OrderTo", PO1.OrderTo, DbType.String);
                    DbParameter OrderFor = provider.CreateParameter("OrderFor", PO1.OrderFor, DbType.String);
                    DbParameter PurchaseOrderTypeCode = provider.CreateParameter("PurchaseOrderTypeCode", PO1.PurchaseOrderType.Code, DbType.String);
                    DbParameter Location = provider.CreateParameter("Location", PO1.Location.OfficeCode, DbType.String);
                    DbParameter ExpectedDeliveryDate = provider.CreateParameter("ExpectedDeliveryDate", PO1.ExpectedDeliveryDate, DbType.DateTime);
                    DbParameter StatusCode = provider.CreateParameter("StatusCode", PO1.Status.Code, DbType.String);
                    DbParameter Remarks = provider.CreateParameter("Remarks", PO1.Remarks, DbType.String);
                    DbParameter Currency = provider.CreateParameter("Currency", PO1.Currency, DbType.String);
                    DbParameter OutP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                    DbParameter[] Params = new DbParameter[14] { PurchaseOrderNo, PurchaseOrderDate, SoNumber, Dealer, Vendor, OrderTo, OrderFor, PurchaseOrderTypeCode, Location, ExpectedDeliveryDate, StatusCode, Remarks, Currency, OutP };

                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        provider.Insert("ZDMS_InsertOrUpdatePurchaseOrder", Params);
                        PoID = Convert.ToInt64(OutP.Value);
                        foreach (PDMS_PurchaseOrderItemN POi in PO1.PurchaseOrderItemS)
                        {
                            DbParameter PurchaseOrderID = provider.CreateParameter("PurchaseOrderID", PoID, DbType.String);
                            DbParameter Item = provider.CreateParameter("Item", POi.Item, DbType.Int32);
                            DbParameter MaterialCode = provider.CreateParameter("MaterialCode ", POi.Material.MaterialCode, DbType.String);
                            DbParameter Qty = provider.CreateParameter("Qty", POi.Qty, DbType.Decimal);
                            DbParameter Rate = provider.CreateParameter("Rate", POi.Rate, DbType.Decimal);
                            DbParameter Discount = provider.CreateParameter("Discount", POi.Discount, DbType.Decimal);
                            DbParameter TaxableValue = provider.CreateParameter("TaxableValue", POi.TaxableValue, DbType.Decimal);
                            DbParameter TaxValue = provider.CreateParameter("TaxValue", POi.TaxValue, DbType.Decimal);
                            DbParameter StatusCodeI = provider.CreateParameter("StatusCode ", POi.ItemStatus.Code, DbType.String);
                            DbParameter[] ParamsI = new DbParameter[9] { PurchaseOrderID, Item, MaterialCode, Qty, Rate, Discount, TaxableValue, TaxValue, StatusCodeI };
                            provider.Insert("ZDMS_InsertOrUpdatePurchaseOrderItem", ParamsI);
                        }
                        scope.Complete();
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_PurchaseOrder", "GetPurchaseOrder", ex);
            }
        }
        public void GetPurchaseOrderHIntegration(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_PurchaseOrderN> POs = new List<PDMS_PurchaseOrderN>();
            try
            {

                DateTime PoDate = DateTime.Now;
                string POdateString = "";

                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceLastDate"))
                {
                    if (DataSet != null)
                    {
                        PoDate = Convert.ToDateTime(DataSet.Tables[0].Rows[0]["InvoiceDate"]);
                        PoDate = PoDate.AddMinutes(-1);
                        POdateString = PoDate.Year.ToString() + "-" + PoDate.Month.ToString("00") + "-" + PoDate.Day.ToString("00") + " " + PoDate.ToShortTimeString();
                    }
                }

                string Query1 = "select  p_po_id po, s_created_on po_date, s_tenant_id, f_office, f_bill_to, f_currency , s_object_type, s_status,f_so_id from dppor_purc_order_hdr where  s_created_on > '2018-07-01' and s_tenant_id <> 20"
                + " and  (s_created_on >='" + POdateString + "' or  s_modified_on>='" + POdateString + "') order by   s_created_on";

                if (!string.IsNullOrEmpty(filter))
                {
                    Query1 = "select  p_po_id po, s_created_on po_date, s_tenant_id, f_office, f_bill_to, f_currency , s_object_type, s_status,f_so_id from dppor_purc_order_hdr where  s_created_on > '2018-07-01' and s_tenant_id <> 20 " + filter;
                }

                DataTable dtH = new NpgsqlServer().ExecuteReader(Query1);
                PDMS_PurchaseOrderN PO = new PDMS_PurchaseOrderN();
                foreach (DataRow dr in dtH.Rows)
                {
                    PO = new PDMS_PurchaseOrderN();
                    PO.PurchaseOrderNo = Convert.ToString(dr["po"]);
                    PO.PurchaseOrderDate = Convert.ToDateTime(dr["po_date"]);
                    PO.SoNumber = Convert.ToString(dr["f_so_id"]);
                    PO.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) };
                    PO.Location = new PDMS_DealerOffice();
                    PO.Location.OfficeCode = Convert.ToString(dr["f_office"]);
                    PO.Currency = Convert.ToString(dr["f_currency"]);
                    PO.Vendor = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["f_bill_to"]) };
                    //   PO.Insurance = Convert.ToString(dr["r_insurance_p"]);
                    //   PO.TaxAmount = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    //   PO.SoldTo = Convert.ToString(dr["f_sold_to"]);
                    PO.Status = new PDMS_PurchaseOrderStatus() { Code = Convert.ToString(dr["s_status"]) };
                    //    PO.NetAmount = DBNull.Value == dr["r_net_amt"] ? 0 : Convert.ToDecimal(dr["r_net_amt"]);
                    //   PO.GrossAmount = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);
                    //  PO.DiscountAmount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    PO.PurchaseOrderType = new PDMS_PurchaseOrderType() { Code = Convert.ToString(dr["s_object_type"]) };
                    POs.Add(PO);
                }
                int c = 0;
                foreach (PDMS_PurchaseOrderN PO1 in POs)
                {
                    c = c + 1;
                    DbParameter PurchaseOrderNo = provider.CreateParameter("PurchaseOrderNo", PO1.PurchaseOrderNo, DbType.String);
                    DbParameter PurchaseOrderDate = provider.CreateParameter("PurchaseOrderDate", PO1.PurchaseOrderDate, DbType.DateTime);
                    DbParameter SoNumber = provider.CreateParameter("SoNumber", PO1.SoNumber, DbType.String);
                    DbParameter Dealer = provider.CreateParameter("Dealer ", PO1.Dealer.DealerCode, DbType.String);
                    DbParameter Vendor = provider.CreateParameter("Vendor", PO1.Vendor.CustomerCode, DbType.String);
                    DbParameter OrderTo = provider.CreateParameter("OrderTo", PO1.OrderTo, DbType.String);
                    DbParameter OrderFor = provider.CreateParameter("OrderFor", PO1.OrderFor, DbType.String);
                    DbParameter PurchaseOrderTypeCode = provider.CreateParameter("PurchaseOrderTypeCode", PO1.PurchaseOrderType.Code, DbType.String);
                    DbParameter Location = provider.CreateParameter("Location", PO1.Location.OfficeCode, DbType.String);
                    DbParameter ExpectedDeliveryDate = provider.CreateParameter("ExpectedDeliveryDate", PO1.ExpectedDeliveryDate, DbType.DateTime);
                    DbParameter StatusCode = provider.CreateParameter("StatusCode ", PO1.Status.Code, DbType.String);
                    DbParameter Remarks = provider.CreateParameter("Remarks", PO1.Remarks, DbType.String);
                    DbParameter Currency = provider.CreateParameter("Currency", PO1.Currency, DbType.String);

                    DbParameter[] Params = new DbParameter[13] { PurchaseOrderNo, PurchaseOrderDate, SoNumber, Dealer, Vendor, OrderTo, OrderFor, PurchaseOrderTypeCode, Location, ExpectedDeliveryDate, StatusCode, Remarks, Currency };

                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        provider.Insert("ZDMS_InsertOrUpdatePurchaseOrder", Params);
                        scope.Complete();
                    }
                }
                TraceLogger.Log(DateTime.Now);

            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_PurchaseOrder", "GetPurchaseOrder", ex);
            }
        }
        public void GetPurchaseOrderIIntegration(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_PurchaseOrderN> POs = new List<PDMS_PurchaseOrderN>();
            try
            {
                string Query2 = "select po.p_po_id po,poi.p_po_item,po.s_tenant_id,poi.f_material_id, poi.r_order_qty,poi.r_tax_amt, poi.r_net_amt,poi.r_gross_amt,poi.r_shiped_qty,poi.r_discount_amt,poi.r_unit_price , poi.s_status "
                    + " from dppor_purc_order_hdr po inner join dppor_purc_order_item poi on poi.k_po_id = po.p_po_id where  po.s_created_on > '2018-07-01' and po.s_tenant_id <> 20 and   po.p_po_id ='3031002885'   ";

                if (!string.IsNullOrEmpty(filter))
                {
                    Query2 = "select po.p_po_id po,poi.p_po_item,po.s_tenant_id,poi.f_material_id, poi.r_order_qty,poi.r_tax_amt, poi.r_net_amt,poi.r_gross_amt,poi.r_discount_amt,poi.r_unit_price , poi.s_status "
                        + " from dppor_purc_order_hdr po inner join dppor_purc_order_item poi on poi.k_po_id = po.p_po_id where  po.s_created_on > '2018-07-01' and po.s_tenant_id <> 20  and  EXTRACT(YEAR FROM po.s_created_on ) = 2019  " + filter;
                }

                POs = new List<PDMS_PurchaseOrderN>();
                PDMS_PurchaseOrderItemN POI = null;
                DataTable dtI = new NpgsqlServer().ExecuteReader(Query2);
                foreach (DataRow dr in dtI.Rows)
                {
                    POI = new PDMS_PurchaseOrderItemN();
                    POI.Item = Convert.ToInt32(dr["p_po_item"]);
                    POI.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]) };
                    POI.Qty = DBNull.Value == dr["r_order_qty"] ? 0 : Convert.ToDecimal(dr["r_order_qty"]);
                    POI.Rate = DBNull.Value == dr["r_unit_price"] ? 0 : Convert.ToDecimal(dr["r_unit_price"]);
                    POI.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);

                    POI.TaxableValue = (POI.Qty * POI.Rate) + POI.Discount;

                    // POI.TaxableValue = DBNull.Value == dr["r_net_amt"] ? 0 : Convert.ToDecimal(dr["r_net_amt"]);
                    // POI.GrossAmount = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);
                    // POI.ShipedQuantity = DBNull.Value == dr["r_shiped_qty"] ? 0 : Convert.ToDecimal(dr["r_shiped_qty"]);

                    POI.TaxValue = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);

                    POI.ItemStatus = new PDMS_PurchaseOrderStatus() { Code = Convert.ToString(dr["s_status"]) };
                    POs.Add(new PDMS_PurchaseOrderN() { PurchaseOrderItem = POI, PurchaseOrderNo = Convert.ToString(dr["po"]), Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) } });
                }
                int c = 0;
                foreach (PDMS_PurchaseOrderN PO1 in POs)
                {
                    c = c + 1;
                    DbParameter PurchaseOrderNo = provider.CreateParameter("PurchaseOrderNo", PO1.PurchaseOrderNo, DbType.String);
                    DbParameter Item = provider.CreateParameter("Item", PO1.PurchaseOrderItem.Item, DbType.Int32);
                    DbParameter Dealer = provider.CreateParameter("Dealer ", PO1.Dealer.DealerCode, DbType.String);
                    DbParameter MaterialCode = provider.CreateParameter("MaterialCode ", PO1.PurchaseOrderItem.Material.MaterialCode, DbType.String);
                    DbParameter Qty = provider.CreateParameter("Qty", PO1.PurchaseOrderItem.Qty, DbType.Decimal);
                    DbParameter Rate = provider.CreateParameter("Rate", PO1.PurchaseOrderItem.Rate, DbType.Decimal);
                    DbParameter Discount = provider.CreateParameter("Discount", PO1.PurchaseOrderItem.Discount, DbType.Decimal);
                    DbParameter TaxableValue = provider.CreateParameter("TaxableValue", PO1.PurchaseOrderItem.TaxableValue, DbType.Decimal);
                    DbParameter TaxValue = provider.CreateParameter("TaxValue", PO1.PurchaseOrderItem.TaxValue, DbType.Decimal);
                    DbParameter StatusCode = provider.CreateParameter("StatusCode ", PO1.PurchaseOrderItem.ItemStatus.Code, DbType.String);
                    DbParameter[] Params = new DbParameter[10] { PurchaseOrderNo, Item, Dealer, MaterialCode, Qty, Rate, Discount, TaxableValue, TaxValue, StatusCode };

                    try
                    {
                        //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        //{
                            provider.Insert("ZDMS_InsertOrUpdatePurchaseOrderItem", Params);
                        //    scope.Complete();
                        //}
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessageService("BDMS_PurchaseOrder", "GetPurchaseOrder", ex);
                    }
                }
                TraceLogger.Log(DateTime.Now);

            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_PurchaseOrder", "GetPurchaseOrder", ex);
            }
        }

        public void GetPurchaseOrderStoIntegration(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_PurchaseOrderN> POs = new List<PDMS_PurchaseOrderN>();
            try
            {

                DateTime PoDate = DateTime.Now;
                string POdateString = "";

                using (DataSet DataSet = provider.Select("ZDMS_GetPurchaseOrderStoLastDate"))
                {
                    if (DataSet != null)
                    {
                        PoDate = Convert.ToDateTime(DataSet.Tables[0].Rows[0]["PurchaseOrderDate"]);
                        PoDate = PoDate.AddMinutes(-1);
                        POdateString = PoDate.Year.ToString() + "-" + PoDate.Month.ToString("00") + "-" + PoDate.Day.ToString("00") + " " + PoDate.ToShortTimeString();
                    }
                }

                string Query1 = "select po.p_sto_id po, po.s_created_on po_date, po.s_tenant_id,r_to_office f_office,f_customer_id f_bill_to,po.f_from_office,f_currency , po.s_object_type "
                    + " , po.s_status, '' f_so_id, p_sto_item p_po_item, poi.f_material_id, poi.r_order_qty,poi.r_tax_amt, poi.r_net_amt,poi.r_gross_amt, poi.r_shiped_qty,0 r_discount_amt "
                    + " ,poi.r_unit_price , poi.s_status as s_statusItem from dsstor_sto_hdr po inner join dsstor_sto_item poi on poi.k_sto_id = po.p_sto_id"
+ " where  po.s_created_on > '2018-07-01' and po.s_tenant_id <> 20  and  (po.s_created_on >='" + POdateString + "' or  po.s_modified_on>='" + POdateString + "') order by po.s_created_on , po.p_sto_id ";



                DataTable dtH = new NpgsqlServer().ExecuteReader(Query1);
                PDMS_PurchaseOrderN PO = null;
                PDMS_PurchaseOrderItemN POI = null;
                long PoID = 0;
                string PoNumber = "";
                foreach (DataRow dr in dtH.Rows)
                {
                   
                    if (PoNumber != Convert.ToString(dr["po"]))
                    {
                        PO = new PDMS_PurchaseOrderN();
                        POs.Add(PO);
                        PoNumber = Convert.ToString(dr["po"]);
                        PO.PurchaseOrderNo = Convert.ToString(dr["po"]);
                        PO.PurchaseOrderDate = Convert.ToDateTime(dr["po_date"]);
                        PO.SoNumber = Convert.ToString(dr["f_so_id"]);
                        PO.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) };
                        PO.Location = new PDMS_DealerOffice();
                        PO.Location.OfficeCode = Convert.ToString(dr["f_office"]);
                        PO.Currency = Convert.ToString(dr["f_currency"]);
                        PO.Vendor = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["f_bill_to"]) };
                        PO.Status = new PDMS_PurchaseOrderStatus() { Code = Convert.ToString(dr["s_status"]) };
                        PO.PurchaseOrderType = new PDMS_PurchaseOrderType() { Code = Convert.ToString(dr["s_object_type"]) };
                        PO.PurchaseOrderItemS = new List<PDMS_PurchaseOrderItemN>();
                    }
                    POI = new PDMS_PurchaseOrderItemN();
                    PO.PurchaseOrderItemS.Add(POI);
                    POI.Item = Convert.ToInt32(dr["p_po_item"]);
                    POI.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]) };
                    POI.Qty = DBNull.Value == dr["r_order_qty"] ? 0 : Convert.ToDecimal(dr["r_order_qty"]);
                    POI.Rate = DBNull.Value == dr["r_unit_price"] ? 0 : Convert.ToDecimal(dr["r_unit_price"]);
                    POI.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    POI.TaxableValue = (POI.Qty * POI.Rate) + POI.Discount;
                    POI.TaxValue = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    POI.ItemStatus = new PDMS_PurchaseOrderStatus() { Code = Convert.ToString(dr["s_statusItem"]) };
                }

                int c = 0;
                foreach (PDMS_PurchaseOrderN PO1 in POs)
                {
                    c = c + 1;
                    DbParameter PurchaseOrderNo = provider.CreateParameter("PurchaseOrderNo", PO1.PurchaseOrderNo, DbType.String);
                    DbParameter PurchaseOrderDate = provider.CreateParameter("PurchaseOrderDate", PO1.PurchaseOrderDate, DbType.DateTime);
                    DbParameter SoNumber = provider.CreateParameter("SoNumber", PO1.SoNumber, DbType.String);
                    DbParameter Dealer = provider.CreateParameter("Dealer ", PO1.Dealer.DealerCode, DbType.String);
                    DbParameter Vendor = provider.CreateParameter("Vendor", PO1.Vendor.CustomerCode, DbType.String);
                    DbParameter OrderTo = provider.CreateParameter("OrderTo", PO1.OrderTo, DbType.String);
                    DbParameter OrderFor = provider.CreateParameter("OrderFor", PO1.OrderFor, DbType.String);
                    DbParameter PurchaseOrderTypeCode = provider.CreateParameter("PurchaseOrderTypeCode", PO1.PurchaseOrderType.Code, DbType.String);
                    DbParameter Location = provider.CreateParameter("Location", PO1.Location.OfficeCode, DbType.String);
                    DbParameter ExpectedDeliveryDate = provider.CreateParameter("ExpectedDeliveryDate", PO1.ExpectedDeliveryDate, DbType.DateTime);
                    DbParameter StatusCode = provider.CreateParameter("StatusCode ", PO1.Status.Code, DbType.String);
                    DbParameter Remarks = provider.CreateParameter("Remarks", PO1.Remarks, DbType.String);
                    DbParameter Currency = provider.CreateParameter("Currency", PO1.Currency, DbType.String);
                    DbParameter OutP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                    DbParameter[] Params = new DbParameter[14] { PurchaseOrderNo, PurchaseOrderDate, SoNumber, Dealer, Vendor, OrderTo, OrderFor, PurchaseOrderTypeCode, Location, ExpectedDeliveryDate, StatusCode, Remarks, Currency, OutP };

                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        provider.Insert("ZDMS_InsertOrUpdatePurchaseOrder", Params);
                        PoID = Convert.ToInt64(OutP.Value);
                        foreach (PDMS_PurchaseOrderItemN POi in PO1.PurchaseOrderItemS)
                        {
                            DbParameter PurchaseOrderID = provider.CreateParameter("PurchaseOrderID", PoID, DbType.String);
                            DbParameter Item = provider.CreateParameter("Item", POi.Item, DbType.Int32);
                            DbParameter MaterialCode = provider.CreateParameter("MaterialCode ", POi.Material.MaterialCode, DbType.String);
                            DbParameter Qty = provider.CreateParameter("Qty", POi.Qty, DbType.Decimal);
                            DbParameter Rate = provider.CreateParameter("Rate", POi.Rate, DbType.Decimal);
                            DbParameter Discount = provider.CreateParameter("Discount", POi.Discount, DbType.Decimal);
                            DbParameter TaxableValue = provider.CreateParameter("TaxableValue", POi.TaxableValue, DbType.Decimal);
                            DbParameter TaxValue = provider.CreateParameter("TaxValue", POi.TaxValue, DbType.Decimal);
                            DbParameter StatusCodeI = provider.CreateParameter("StatusCode ", POi.ItemStatus.Code, DbType.String);
                            DbParameter[] ParamsI = new DbParameter[9] { PurchaseOrderID, Item, MaterialCode, Qty, Rate, Discount, TaxableValue, TaxValue, StatusCodeI };
                            provider.Insert("ZDMS_InsertOrUpdatePurchaseOrderItem", ParamsI);
                        }
                        scope.Complete();
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_PurchaseOrder", "GetPurchaseOrder", ex);
            }
        }
        public void GetPurchaseOrderStoHIntegration(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_PurchaseOrderN> POs = new List<PDMS_PurchaseOrderN>();
            try
            {
                string Query1 = "select  p_sto_id po, s_created_on po_date, s_tenant_id,r_to_office f_office,f_customer_id f_bill_to,f_from_office, f_currency , s_object_type, s_status, '' f_so_id from dsstor_sto_hdr where  s_created_on > '2021-07-01' and s_tenant_id <> 20";

                if (!string.IsNullOrEmpty(filter))
                {
                    Query1 = "select  p_sto_id po, s_created_on po_date, s_tenant_id,r_to_office f_office,f_customer_id f_bill_to,f_from_office, f_currency , s_object_type, s_status, '' f_so_id from dsstor_sto_hdr where  s_created_on > '2021-07-01' and s_tenant_id <> 20 " + filter;
                }

                DataTable dtH = new NpgsqlServer().ExecuteReader(Query1);
                PDMS_PurchaseOrderN PO = new PDMS_PurchaseOrderN();
                foreach (DataRow dr in dtH.Rows)
                {
                    PO = new PDMS_PurchaseOrderN();
                    PO.PurchaseOrderNo = Convert.ToString(dr["po"]);
                    PO.PurchaseOrderDate = Convert.ToDateTime(dr["po_date"]);
                    PO.SoNumber = Convert.ToString(dr["f_so_id"]);
                    PO.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) };
                    PO.Location = new PDMS_DealerOffice();
                    PO.Location.OfficeCode = Convert.ToString(dr["f_office"]);
                    PO.Currency = Convert.ToString(dr["f_currency"]);
                    PO.Vendor = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["f_bill_to"]) };
                    //   PO.Insurance = Convert.ToString(dr["r_insurance_p"]);
                    //   PO.TaxAmount = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    //   PO.SoldTo = Convert.ToString(dr["f_sold_to"]);
                    PO.Status = new PDMS_PurchaseOrderStatus() { Code = Convert.ToString(dr["s_status"]) };
                    //    PO.NetAmount = DBNull.Value == dr["r_net_amt"] ? 0 : Convert.ToDecimal(dr["r_net_amt"]);
                    //   PO.GrossAmount = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);
                    //  PO.DiscountAmount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    PO.PurchaseOrderType = new PDMS_PurchaseOrderType() { Code = Convert.ToString(dr["s_object_type"]) };
                    POs.Add(PO);
                }
                foreach (PDMS_PurchaseOrderN PO1 in POs)
                {
                    DbParameter PurchaseOrderNo = provider.CreateParameter("PurchaseOrderNo", PO1.PurchaseOrderNo, DbType.String);
                    DbParameter PurchaseOrderDate = provider.CreateParameter("PurchaseOrderDate", PO1.PurchaseOrderDate, DbType.DateTime);
                    DbParameter SoNumber = provider.CreateParameter("SoNumber", PO1.SoNumber, DbType.String);
                    DbParameter Dealer = provider.CreateParameter("Dealer ", PO1.Dealer.DealerCode, DbType.String);
                    DbParameter Vendor = provider.CreateParameter("Vendor", PO1.Vendor.CustomerCode, DbType.String);
                    DbParameter OrderTo = provider.CreateParameter("OrderTo", PO1.OrderTo, DbType.String);
                    DbParameter OrderFor = provider.CreateParameter("OrderFor", PO1.OrderFor, DbType.String);
                    DbParameter PurchaseOrderTypeCode = provider.CreateParameter("PurchaseOrderTypeCode", PO1.PurchaseOrderType.Code, DbType.String);
                    DbParameter Location = provider.CreateParameter("Location", PO1.Location.OfficeCode, DbType.String);
                    DbParameter ExpectedDeliveryDate = provider.CreateParameter("ExpectedDeliveryDate", PO1.ExpectedDeliveryDate, DbType.DateTime);
                    DbParameter StatusCode = provider.CreateParameter("StatusCode ", PO1.Status.Code, DbType.String);
                    DbParameter Remarks = provider.CreateParameter("Remarks", PO1.Remarks, DbType.String);
                    DbParameter Currency = provider.CreateParameter("Currency", PO1.Currency, DbType.String);

                    DbParameter[] Params = new DbParameter[13] { PurchaseOrderNo, PurchaseOrderDate, SoNumber, Dealer, Vendor, OrderTo, OrderFor, PurchaseOrderTypeCode, Location, ExpectedDeliveryDate, StatusCode, Remarks, Currency };

                    //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    //{
                        provider.Insert("ZDMS_InsertOrUpdatePurchaseOrder", Params);
                    //    scope.Complete();
                    //}
                }
                TraceLogger.Log(DateTime.Now);

            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_PurchaseOrder", "GetPurchaseOrder", ex);
            }
        }
        public void GetPurchaseOrderStoIIntegration(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_PurchaseOrderN> POs = new List<PDMS_PurchaseOrderN>();
            try
            {
                string Query2 = "select po.p_sto_id po,p_sto_item p_po_item,po.s_tenant_id,poi.f_material_id, poi.r_order_qty,poi.r_tax_amt, poi.r_net_amt,poi.r_gross_amt, poi.r_shiped_qty,0 r_discount_amt,poi.r_unit_price , poi.s_status "
                    + " from dsstor_sto_hdr po inner join dsstor_sto_item poi on poi.k_sto_id = po.p_sto_id where  po.s_created_on > '2021-07-01' and po.s_tenant_id <> 20 ";

                if (!string.IsNullOrEmpty(filter))
                {
                    Query2 = "select po.p_sto_id po,p_sto_item p_po_item,po.s_tenant_id,poi.f_material_id, poi.r_order_qty,poi.r_tax_amt, poi.r_net_amt,poi.r_gross_amt, poi.r_shiped_qty,0 r_discount_amt,poi.r_unit_price , poi.s_status "
                        + " from dsstor_sto_hdr po inner join dsstor_sto_item poi on poi.k_sto_id = po.p_sto_id where  po.s_created_on > '2021-07-01' and po.s_tenant_id <> 20 " + filter;
                }

                POs = new List<PDMS_PurchaseOrderN>();
                PDMS_PurchaseOrderItemN POI = null;
                DataTable dtI = new NpgsqlServer().ExecuteReader(Query2);
                foreach (DataRow dr in dtI.Rows)
                {
                    POI = new PDMS_PurchaseOrderItemN();
                    POI.Item = Convert.ToInt32(dr["p_po_item"]);
                    POI.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]) };
                    POI.Qty = DBNull.Value == dr["r_order_qty"] ? 0 : Convert.ToDecimal(dr["r_order_qty"]);
                    POI.Rate = DBNull.Value == dr["r_unit_price"] ? 0 : Convert.ToDecimal(dr["r_unit_price"]);
                    POI.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);

                    POI.TaxableValue = (POI.Qty * POI.Rate) + POI.Discount;

                    // POI.TaxableValue = DBNull.Value == dr["r_net_amt"] ? 0 : Convert.ToDecimal(dr["r_net_amt"]);
                    // POI.GrossAmount = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);
                    // POI.ShipedQuantity = DBNull.Value == dr["r_shiped_qty"] ? 0 : Convert.ToDecimal(dr["r_shiped_qty"]);

                    POI.TaxValue = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);

                    POI.ItemStatus = new PDMS_PurchaseOrderStatus() { Code = Convert.ToString(dr["s_status"]) };
                    POs.Add(new PDMS_PurchaseOrderN() { PurchaseOrderItem = POI, PurchaseOrderNo = Convert.ToString(dr["po"]), Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) } });
                }
                foreach (PDMS_PurchaseOrderN PO1 in POs)
                {
                    DbParameter PurchaseOrderNo = provider.CreateParameter("PurchaseOrderNo", PO1.PurchaseOrderNo, DbType.String);
                    DbParameter Item = provider.CreateParameter("Item", PO1.PurchaseOrderItem.Item, DbType.Int32);
                    DbParameter Dealer = provider.CreateParameter("Dealer ", PO1.Dealer.DealerCode, DbType.String);
                    DbParameter MaterialCode = provider.CreateParameter("MaterialCode ", PO1.PurchaseOrderItem.Material.MaterialCode, DbType.String);
                    DbParameter Qty = provider.CreateParameter("Qty", PO1.PurchaseOrderItem.Qty, DbType.Decimal);
                    DbParameter Rate = provider.CreateParameter("Rate", PO1.PurchaseOrderItem.Rate, DbType.Decimal);
                    DbParameter Discount = provider.CreateParameter("Discount", PO1.PurchaseOrderItem.Discount, DbType.Decimal);
                    DbParameter TaxableValue = provider.CreateParameter("TaxableValue", PO1.PurchaseOrderItem.TaxableValue, DbType.Decimal);
                    DbParameter TaxValue = provider.CreateParameter("TaxValue", PO1.PurchaseOrderItem.TaxValue, DbType.Decimal);
                    DbParameter StatusCode = provider.CreateParameter("StatusCode ", PO1.PurchaseOrderItem.ItemStatus.Code, DbType.String);
                    DbParameter[] Params = new DbParameter[10] { PurchaseOrderNo, Item, Dealer, MaterialCode, Qty, Rate, Discount, TaxableValue, TaxValue, StatusCode };

                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            provider.Insert("ZDMS_InsertOrUpdatePurchaseOrderItem", Params);
                            scope.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessageService("BDMS_PurchaseOrder", "GetPurchaseOrder", ex);
                    }
                }
                TraceLogger.Log(DateTime.Now);

            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_PurchaseOrder", "GetPurchaseOrder", ex);
            }
        }


        public void GetAsnIntegration()
        {
            DateTime AsnDate1 = DateTime.Now;
            string AsnDateString = "";

            TraceLogger.Log(DateTime.Now);
            List<PDMS_ASN> AsnList = new List<PDMS_ASN>();
            try
            {
                using (DataSet DataSet = provider.Select("ZDMS_GetPurchaseOrderAsnLastDate"))
                {
                    if (DataSet != null)
                    {
                        AsnDate1 = Convert.ToDateTime(DataSet.Tables[0].Rows[0]["AsnDate"]);
                        AsnDate1 = AsnDate1.AddMinutes(-1);
                        AsnDateString = AsnDate1.Year.ToString() + "-" + AsnDate1.Month.ToString("00") + "-" + AsnDate1.Day.ToString("00") + " " + AsnDate1.ToShortTimeString();
                    }
                }

                string Query1 = "select p_asn_id, Asn.r_asn_date,Asn.s_tenant_id, Asn.f_delivery_id, Asn.r_nt_weight, Asn.s_status, r_remark, f_courier_id , "
+ " p_asn_item, f_po_id, f_po_item,AsnI.s_tenant_id, f_material_id, r_delivery_qty, AsnI.r_nt_weight, AsnI.r_uom_weight, AsnI.r_pack_count,  "
+ " AsnI.r_pack_count_uom, r_stock_type, r_is_changedpart , r_remarks from  dpasr_asn_hdr Asn "
+ " inner join dpasr_asn_item AsnI on AsnI.k_asn_id = Asn.p_asn_id and AsnI.s_tenant_id = Asn.s_tenant_id "
+ " where Asn.r_asn_date > '2021-07-01' and Asn.s_tenant_id <> 20 and  (Asn.s_created_on >='" + AsnDateString + "' or  Asn.s_modified_on >='" + AsnDateString + "') order by Asn.s_created_on , Asn.p_asn_id ";

                

                PDMS_ASN Asn = null;
                PDMS_AsnItem AsnItem = null;
                DataTable dtI = new NpgsqlServer().ExecuteReader(Query1);

                string AsnN = "";
                foreach (DataRow dr in dtI.Rows)
                {

                    if (AsnN != Convert.ToString(dr["p_asn_id"]))
                    {
                        Asn = new PDMS_ASN();
                        AsnList.Add(Asn);
                        AsnN = Convert.ToString(dr["p_asn_id"]);
                        Asn.AsnNumber = Convert.ToString(dr["p_asn_id"]);
                        Asn.AsnDate = Convert.ToDateTime(dr["r_asn_date"]);
                        Asn.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) };
                        Asn.DeliveryNumber = Convert.ToString(dr["f_delivery_id"]);
                        Asn.NetWeight = DBNull.Value == dr["r_nt_weight"] ? (decimal?)null : Convert.ToDecimal(dr["r_nt_weight"]);
                        Asn.CourierID = Convert.ToString(dr["f_courier_id"]);
                        Asn.Status = Convert.ToString(dr["s_status"]);
                        Asn.Remarks = Convert.ToString(dr["r_remark"]);
                        Asn.AsnItemS = new List<PDMS_AsnItem>();
                    }
                    AsnItem = new PDMS_AsnItem();
                    Asn.AsnItemS.Add(AsnItem);
                    AsnItem.AsnItem = Convert.ToInt32(dr["p_asn_item"]);
                    AsnItem.PO = new PDMS_PurchaseOrderN() { PurchaseOrderNo = Convert.ToString(dr["f_po_id"]), PurchaseOrderItem = new PDMS_PurchaseOrderItemN() { Item = DBNull.Value == dr["f_po_item"] ? 0 : Convert.ToInt32(dr["f_po_item"]) } };
                    AsnItem.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]) };
                    AsnItem.Qty = DBNull.Value == dr["r_delivery_qty"] ? 0 : Convert.ToDecimal(dr["r_delivery_qty"]);
                    AsnItem.NetWeight = DBNull.Value == dr["r_nt_weight"] ? 0 : Convert.ToDecimal(dr["r_nt_weight"]);
                    AsnItem.UomWeight = Convert.ToString(dr["r_uom_weight"]);
                    AsnItem.PackCount = DBNull.Value == dr["r_pack_count"] ? 0 : Convert.ToDecimal(dr["r_pack_count"]);
                    AsnItem.UomPackCount = Convert.ToString(dr["r_pack_count_uom"]);
                    AsnItem.StockType = Convert.ToString(dr["r_stock_type"]);
                    AsnItem.Remarks = Convert.ToString(dr["r_remarks"]);
                    AsnItem.IsChangedpart = DBNull.Value == dr["r_is_changedpart"] ? false : Convert.ToBoolean(Convert.ToInt32(dr["r_is_changedpart"])); 
                }
               

                long AsnID = 0;

                foreach (PDMS_ASN A in AsnList)
                {
                    DbParameter AsnNumber = provider.CreateParameter("AsnNumber", A.AsnNumber, DbType.String);
                    DbParameter AsnDate = provider.CreateParameter("AsnDate", A.AsnDate, DbType.DateTime);
                    DbParameter Dealer = provider.CreateParameter("Dealer ", A.Dealer.DealerCode, DbType.String);
                    DbParameter DeliveryNumber = provider.CreateParameter("DeliveryNumber ", A.DeliveryNumber, DbType.String);
                    DbParameter NetWeight = provider.CreateParameter("NetWeight", A.NetWeight, DbType.Decimal);
                    DbParameter CourierID = provider.CreateParameter("CourierID ", A.CourierID, DbType.String);
                    DbParameter Status = provider.CreateParameter("Status", A.Status, DbType.String);
                    DbParameter Remarks = provider.CreateParameter("Remarks", A.Remarks, DbType.String);
                    DbParameter OutP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                    DbParameter[] Params = new DbParameter[9] { AsnNumber, AsnDate, Dealer, DeliveryNumber, NetWeight, CourierID, Status, Remarks, OutP };
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            provider.Insert("ZDMS_InsertOrUpdatePurchaseOrderAsn", Params);
                            AsnID = Convert.ToInt64(OutP.Value);
                            foreach (PDMS_AsnItem AsnI in A.AsnItemS)
                            {
                                DbParameter AsnIDP = provider.CreateParameter("AsnID", AsnID, DbType.String);
                                DbParameter AsnItemP = provider.CreateParameter("AsnItem", AsnI.AsnItem, DbType.Int32);
                                DbParameter PurchaseOrderNo = provider.CreateParameter("PurchaseOrderNo", AsnI.PO.PurchaseOrderNo, DbType.String);
                                DbParameter PoItem = provider.CreateParameter("PoItem", AsnI.PO.PurchaseOrderItem.Item, DbType.Int32);
                                DbParameter IDealer = provider.CreateParameter("Dealer ", A.Dealer.DealerCode, DbType.String);
                                DbParameter MaterialCode = provider.CreateParameter("MaterialCode ", AsnI.Material.MaterialCode, DbType.String);
                                DbParameter Qty = provider.CreateParameter("Qty", AsnI.Qty, DbType.Decimal);

                                DbParameter INetWeight = provider.CreateParameter("NetWeight", AsnI.NetWeight, DbType.Decimal);
                                DbParameter UomWeight = provider.CreateParameter("UomWeight ", AsnI.UomWeight, DbType.String);
                                DbParameter PackCount = provider.CreateParameter("PackCount", AsnI.PackCount, DbType.Decimal);
                                DbParameter UomPackCount = provider.CreateParameter("UomPackCount ", AsnI.UomPackCount, DbType.String);
                                DbParameter StockType = provider.CreateParameter("StockType ", AsnI.StockType, DbType.String);
                                DbParameter IRemarks = provider.CreateParameter("Remarks ", AsnI.Remarks, DbType.String);
                                DbParameter IsChangedpart = provider.CreateParameter("IsChangedpart ", AsnI.IsChangedpart, DbType.Boolean);
                                DbParameter[] IParams = new DbParameter[14] { AsnIDP, AsnItemP, PurchaseOrderNo, PoItem, IDealer, MaterialCode, Qty, INetWeight, UomWeight, PackCount, UomPackCount, StockType, IRemarks, IsChangedpart };

                                provider.Insert("ZDMS_InsertOrUpdatePurchaseOrderAsnItem", IParams);
                            }
                            scope.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessageService("BDMS_PurchaseOrder", "ZDMS_InsertOrUpdatePurchaseOrderAsn", ex);
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_PurchaseOrder", "ZDMS_InsertOrUpdatePurchaseOrderAsn", ex);
            }
        }
        public void GetAsnHIntegration(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_ASN> POs = new List<PDMS_ASN>();
            try
            {
                string Query1 = "select p_asn_id, r_asn_date,s_tenant_id, f_delivery_id, r_nt_weight, s_status, r_remark, f_courier_id from  dpasr_asn_hdr where r_asn_date > '2021-07-01' and s_tenant_id <> 20";

                if (!string.IsNullOrEmpty(filter))
                {
                    Query1 = "select p_asn_id, r_asn_date,s_tenant_id, f_delivery_id, r_nt_weight, s_status, r_remark, f_courier_id from  dpasr_asn_hdr where r_asn_date > '2021-07-01' and s_tenant_id <> 20 " + filter;
                }

                PDMS_ASN POI = null;
                DataTable dtI = new NpgsqlServer().ExecuteReader(Query1);
                foreach (DataRow dr in dtI.Rows)
                {
                    POI = new PDMS_ASN();
                    POI.AsnNumber = Convert.ToString(dr["p_asn_id"]);
                    POI.AsnDate = Convert.ToDateTime(dr["r_asn_date"]);
                    POI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) };
                    POI.DeliveryNumber = Convert.ToString(dr["f_delivery_id"]);
                    //  POI.PickupDate = DBNull.Value == dr["r_order_qty"] ? (DateTime?)null : Convert.ToDateTime(dr["p_po_item"]);
                    //  POI.LoadingDate = DBNull.Value == dr["r_order_qty"] ? (DateTime?)null : Convert.ToDateTime(dr["p_po_item"]);
                    POI.NetWeight = DBNull.Value == dr["r_nt_weight"] ? (decimal?)null : Convert.ToDecimal(dr["r_nt_weight"]);
                    //  POI.TrackID = Convert.ToString(dr["p_po_item"]);
                    POI.CourierID = Convert.ToString(dr["f_courier_id"]);
                    // POI.CourierDate = DBNull.Value == dr["r_order_qty"] ? (DateTime?)null : Convert.ToDateTime(dr["p_po_item"]);
                    POI.Status = Convert.ToString(dr["s_status"]);
                    // POI.ShipingAddress = Convert.ToString(dr["p_po_item"]);
                    // POI.CourierCharge = DBNull.Value == dr["r_order_qty"] ? (decimal?)null : Convert.ToDecimal(dr["p_po_item"]);
                    // POI.LRNo = Convert.ToString(dr["p_po_item"]);
                    POI.Remarks = Convert.ToString(dr["r_remark"]);
                    POs.Add(POI);
                }
                foreach (PDMS_ASN PO1 in POs)
                {
                    DbParameter AsnNumber = provider.CreateParameter("AsnNumber", PO1.AsnNumber, DbType.String);
                    DbParameter AsnDate = provider.CreateParameter("AsnDate", PO1.AsnDate, DbType.DateTime);
                    DbParameter Dealer = provider.CreateParameter("Dealer ", PO1.Dealer.DealerCode, DbType.String);
                    DbParameter DeliveryNumber = provider.CreateParameter("DeliveryNumber ", PO1.DeliveryNumber, DbType.String);
                    //  DbParameter PickupDate = provider.CreateParameter("PickupDate", PO1.PickupDate, DbType.DateTime);
                    //    DbParameter LoadingDate = provider.CreateParameter("LoadingDate", PO1.LoadingDate, DbType.DateTime);
                    DbParameter NetWeight = provider.CreateParameter("NetWeight", PO1.NetWeight, DbType.Decimal);
                    //  DbParameter TrackID = provider.CreateParameter("TrackID", PO1.TrackID, DbType.String);
                    DbParameter CourierID = provider.CreateParameter("CourierID ", PO1.CourierID, DbType.String);

                    //   DbParameter CourierDate = provider.CreateParameter("CourierDate", PO1.CourierDate, DbType.DateTime);
                    DbParameter Status = provider.CreateParameter("Status", PO1.Status, DbType.String);
                    //  DbParameter ShipingAddress = provider.CreateParameter("ShipingAddress", PO1.ShipingAddress, DbType.String);
                    //   DbParameter CourierCharge = provider.CreateParameter("CourierCharge", PO1.CourierCharge, DbType.Decimal);
                    //   DbParameter LRNo = provider.CreateParameter("LRNo", PO1.LRNo, DbType.String);
                    DbParameter Remarks = provider.CreateParameter("Remarks", PO1.Remarks, DbType.String);

                    DbParameter[] Params = new DbParameter[8] { AsnNumber, AsnDate, Dealer, DeliveryNumber, NetWeight, CourierID, Status, Remarks };
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            provider.Insert("ZDMS_InsertOrUpdatePurchaseOrderAsnOld", Params);
                            scope.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessageService("BDMS_PurchaseOrder", "ZDMS_InsertOrUpdatePurchaseOrderAsn", ex);
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_PurchaseOrder", "ZDMS_InsertOrUpdatePurchaseOrderAsn", ex);
            }
        }
        public void GetAsnIIntegration(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_ASN> POs = new List<PDMS_ASN>();
            try
            {
                string Query1 = "select k_asn_id, p_asn_item, f_po_id, f_po_item,s_tenant_id, f_material_id, r_delivery_qty, r_nt_weight, r_uom_weight, r_pack_count, r_pack_count_uom, r_stock_type, r_is_changedpart , r_remarks from dpasr_asn_item  where s_created_on > '2021-07-01' and s_tenant_id <> 20";

                if (!string.IsNullOrEmpty(filter))
                {
                    Query1 = "select k_asn_id, p_asn_item, f_po_id, f_po_item,s_tenant_id, f_material_id, r_delivery_qty, r_nt_weight, r_uom_weight, r_pack_count, r_pack_count_uom, r_stock_type, r_is_changedpart , r_remarks from dpasr_asn_item  where s_created_on > '2021-07-01' and s_tenant_id <> 20  " + filter;
                }

                PDMS_AsnItem POI = null;
                DataTable dtI = new NpgsqlServer().ExecuteReader(Query1);
                foreach (DataRow dr in dtI.Rows)
                {
                    POI = new PDMS_AsnItem();
                    POI.AsnItem = Convert.ToInt32(dr["p_asn_item"]);
                    POI.PO = new PDMS_PurchaseOrderN() { PurchaseOrderNo = Convert.ToString(dr["f_po_id"]), PurchaseOrderItem = new PDMS_PurchaseOrderItemN() { Item = DBNull.Value == dr["f_po_item"] ? 0 : Convert.ToInt32(dr["f_po_item"]) } };
                    //  POI.SoNumber = Convert.ToString(dr["p_asn_item"]);
                    // POI.SoItem = Convert.ToInt32(dr["p_po_item"]);
                    // POI.SapSoNumber = Convert.ToString(dr["p_po_item"]);

                    POI.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]) };
                    POI.Qty = DBNull.Value == dr["r_delivery_qty"] ? 0 : Convert.ToDecimal(dr["r_delivery_qty"]);

                    POI.NetWeight = DBNull.Value == dr["r_nt_weight"] ? 0 : Convert.ToDecimal(dr["r_nt_weight"]);
                    POI.UomWeight = Convert.ToString(dr["r_uom_weight"]);
                    POI.PackCount = DBNull.Value == dr["r_pack_count"] ? 0 : Convert.ToDecimal(dr["r_pack_count"]);
                    POI.UomPackCount = Convert.ToString(dr["r_pack_count_uom"]);
                    POI.StockType = Convert.ToString(dr["r_stock_type"]);
                    POI.Remarks = Convert.ToString(dr["r_remarks"]);
                    POI.IsChangedpart = DBNull.Value == dr["r_is_changedpart"] ? false : Convert.ToBoolean(Convert.ToInt32(dr["r_is_changedpart"]));

                    POs.Add(new PDMS_ASN() { AsnItem = POI, AsnNumber = Convert.ToString(dr["k_asn_id"]), Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) } });
                }
                foreach (PDMS_ASN PO1 in POs)
                {
                    DbParameter AsnNumber = provider.CreateParameter("AsnNumber", PO1.AsnNumber, DbType.String);
                    DbParameter AsnItem = provider.CreateParameter("AsnItem", PO1.AsnItem.AsnItem, DbType.Int32);
                    DbParameter PurchaseOrderNo = provider.CreateParameter("PurchaseOrderNo", PO1.AsnItem.PO.PurchaseOrderNo, DbType.String);
                    DbParameter PoItem = provider.CreateParameter("PoItem", PO1.AsnItem.PO.PurchaseOrderItem.Item, DbType.Int32);
                    DbParameter Dealer = provider.CreateParameter("Dealer", PO1.Dealer.DealerCode, DbType.String);
                    //  DbParameter SapSoNumber = provider.CreateParameter("SapSoNumber", PO1.AsnItem.SapSoNumber, DbType.String);
                    DbParameter MaterialCode = provider.CreateParameter("MaterialCode ", PO1.AsnItem.Material.MaterialCode, DbType.String);
                    DbParameter Qty = provider.CreateParameter("Qty", PO1.AsnItem.Qty, DbType.Decimal);

                    DbParameter NetWeight = provider.CreateParameter("NetWeight", PO1.NetWeight, DbType.Decimal);
                    DbParameter UomWeight = provider.CreateParameter("UomWeight ", PO1.AsnItem.UomWeight, DbType.String);
                    DbParameter PackCount = provider.CreateParameter("PackCount", PO1.AsnItem.PackCount, DbType.Decimal);
                    DbParameter UomPackCount = provider.CreateParameter("UomPackCount ", PO1.AsnItem.UomPackCount, DbType.String);
                    DbParameter StockType = provider.CreateParameter("StockType ", PO1.AsnItem.StockType, DbType.String);
                    DbParameter Remarks = provider.CreateParameter("Remarks ", PO1.AsnItem.Remarks, DbType.String);
                    DbParameter IsChangedpart = provider.CreateParameter("IsChangedpart ", PO1.AsnItem.IsChangedpart, DbType.Boolean);
                    DbParameter[] Params = new DbParameter[13] { AsnNumber, AsnItem, PurchaseOrderNo, PoItem, Dealer, MaterialCode, Qty, NetWeight, UomWeight, PackCount, UomPackCount, StockType, Remarks };
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            provider.Insert("ZDMS_InsertOrUpdatePurchaseOrderAsnItemOld", Params);
                            scope.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessageService("BDMS_PurchaseOrder", "ZDMS_InsertOrUpdatePurchaseOrderAsnItem", ex);
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_PurchaseOrder", "ZDMS_InsertOrUpdatePurchaseOrderAsnItem", ex);
            }
        }


        public void GetGrIntegration()
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_ASN> POs = new List<PDMS_ASN>();
            try
            {
                DateTime GrDate1 = DateTime.Now;
                string GrDateString = "";
                using (DataSet DataSet = provider.Select("ZDMS_GetPurchaseOrderGrLastDate"))
                {
                    if (DataSet != null)
                    {
                        GrDate1 = Convert.ToDateTime(DataSet.Tables[0].Rows[0]["GrDate"]);
                        GrDate1 = GrDate1.AddMinutes(-1);
                        GrDateString = GrDate1.Year.ToString() + "-" + GrDate1.Month.ToString("00") + "-" + GrDate1.Day.ToString("00") + " " + GrDate1.ToShortTimeString();
                    }
                }

                //string Query1 = "select f_asn_id, h.r_gr_date, k_gr_id, i.s_tenant_id, p_gr_item, f_material_id, r_received_qty, r_qty, r_damaged_qty, r_returned_qty,h.s_status,i.r_remarks "
                //                + " from dpgrr_gr_hdr h inner join  dpgrr_gr_item i on i.k_gr_id = h.p_gr_id and i.s_tenant_id = h.s_tenant_id where  h.r_gr_date > '2021-07-01' and h.s_tenant_id <> 20 ";
                string Query1 = " select f_asn_id, h.r_gr_date, k_gr_id, i.s_tenant_id, p_gr_item, f_material_id, r_received_qty, r_qty, r_damaged_qty, r_returned_qty,h.s_status,i.r_remarks "
   + " from dpgrr_gr_hdr h inner join  dpgrr_gr_item i on i.k_gr_id = h.p_gr_id and i.s_tenant_id = h.s_tenant_id "
  + " where  h.r_gr_date > '2021-07-01' and h.s_tenant_id <> 20 and (h.s_created_on >='" + GrDateString + "' or  h.s_modified_on >='" + GrDateString + "' )  order by h.s_created_on ";
                

                PDMS_AsnItem POI = null;
                DataTable dtI = new NpgsqlServer().ExecuteReader(Query1);
                foreach (DataRow dr in dtI.Rows)
                {
                    POI = new PDMS_AsnItem();
                    POI.AsnItem = Convert.ToInt32(dr["p_gr_item"]);
                    POI.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]) };

                    POI.DeliveredQty = DBNull.Value == dr["r_qty"] ? 0 : Convert.ToDecimal(dr["r_qty"]);
                    POI.ReceivedQty = DBNull.Value == dr["r_received_qty"] ? 0 : Convert.ToDecimal(dr["r_received_qty"]);
                    POI.DamagedQty = DBNull.Value == dr["r_damaged_qty"] ? 0 : Convert.ToDecimal(dr["r_damaged_qty"]);
                    POI.ReturnedQty = DBNull.Value == dr["r_returned_qty"] ? 0 : Convert.ToDecimal(dr["r_returned_qty"]);
                    POI.GrRemarks = Convert.ToString(dr["r_remarks"]);

                    POs.Add(new PDMS_ASN()
                    {
                        AsnItem = POI,
                        AsnNumber = Convert.ToString(dr["f_asn_id"]),
                        GrNumber = Convert.ToString(dr["k_gr_id"]),
                        GrDate = Convert.ToDateTime(dr["r_gr_date"]),
                        GrStatus = Convert.ToString(dr["s_status"]),
                        Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) }
                    });
                }
                foreach (PDMS_ASN PO1 in POs)
                {
                    DbParameter AsnNumber = provider.CreateParameter("AsnNumber", PO1.AsnNumber, DbType.String);
                    DbParameter AsnItem = provider.CreateParameter("AsnItem", PO1.AsnItem.AsnItem, DbType.Int32);
                    DbParameter GrNumber = provider.CreateParameter("GrNumber", PO1.GrNumber, DbType.String);
                    DbParameter GrDate = provider.CreateParameter("GrDate", PO1.GrDate, DbType.DateTime);
                    DbParameter GrStatus = provider.CreateParameter("GrStatus", PO1.GrStatus, DbType.String);
                    DbParameter Dealer = provider.CreateParameter("Dealer", PO1.Dealer.DealerCode, DbType.String);

                    DbParameter MaterialCode = provider.CreateParameter("MaterialCode ", PO1.AsnItem.Material.MaterialCode, DbType.String);
                    DbParameter DeliveredQty = provider.CreateParameter("DeliveredQty", PO1.AsnItem.DeliveredQty, DbType.Decimal);
                    DbParameter ReceivedQty = provider.CreateParameter("ReceivedQty", PO1.AsnItem.ReceivedQty, DbType.Decimal);
                    DbParameter DamagedQty = provider.CreateParameter("DamagedQty", PO1.AsnItem.DamagedQty, DbType.Decimal);
                    DbParameter ReturnedQty = provider.CreateParameter("ReturnedQty", PO1.AsnItem.ReturnedQty, DbType.Decimal);
                    DbParameter GrRemarks = provider.CreateParameter("GrRemarks ", PO1.AsnItem.GrRemarks, DbType.String);
                    DbParameter[] Params = new DbParameter[12] { AsnNumber, AsnItem, GrNumber, GrDate, GrStatus, Dealer, MaterialCode, DeliveredQty, ReceivedQty, DamagedQty, ReturnedQty, GrRemarks };
                    try
                    {
                        //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        //{
                        provider.Insert("ZDMS_InsertOrUpdatePurchaseOrderGR", Params);
                        //    scope.Complete();
                        //}
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessageService("BDMS_PurchaseOrder", "ZDMS_InsertOrUpdatePurchaseOrderAsnItem", ex);
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_PurchaseOrder", "ZDMS_InsertOrUpdatePurchaseOrderAsnItem", ex);
            }
        }

        public void GetSapDeliveryNumberIntegration()
        {
            int c = 0;
            TraceLogger.Log(DateTime.Now);
            List<PDMS_ASN> POs = new List<PDMS_ASN>();
            try
            {
               // string Query1 = "select p_del_id,f_customer_id,f_asn_id,r_ext_id, r_del_date from dsder_delv_hdr where  r_del_date > '2018-07-01' and  s_tenant_id = 20 ";

                DateTime DDate = DateTime.Now;
                string DDateString = "";
                using (DataSet DataSet = provider.Select("ZDMS_GetPurchaseOrderDeliveryLastDate"))
                {
                    if (DataSet != null)
                    {
                        DDate = Convert.ToDateTime(DataSet.Tables[0].Rows[0]["DeliveryDate"]);
                        DDate = DDate.AddMinutes(-1);
                        DDateString = DDate.Year.ToString() + "-" + DDate.Month.ToString("00") + "-" + DDate.Day.ToString("00") + " " + DDate.ToShortTimeString();
                    }
                }
                string Query1 = "select p_del_id,f_customer_id,f_asn_id,r_ext_id, r_del_date from dsder_delv_hdr where  r_del_date > '2018-07-01' and  s_tenant_id = 20 "
                    + " and (r_del_date >='" + DDateString + "' or  s_modified_on >='" + DDateString + "' )  order by r_del_date ";

                


                DataTable dtI = new NpgsqlServer().ExecuteReader(Query1);
                foreach (DataRow dr in dtI.Rows)
                {
                    POs.Add(new PDMS_ASN()
                    {
                        AsnNumber = Convert.ToString(dr["f_asn_id"]),
                        DeliveryNumber = Convert.ToString(dr["p_del_id"]),
                        SapDeliveryNumber = Convert.ToString(dr["r_ext_id"]),
                        DeliveryDate = Convert.ToDateTime(dr["r_del_date"]),
                        Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["f_customer_id"]) }
                    });
                }
              
                foreach (PDMS_ASN PO1 in POs)
                {
                    c = c + 1;
                    DbParameter AsnNumber = provider.CreateParameter("AsnNumber", PO1.AsnNumber, DbType.String);
                    DbParameter DeliveryNumber = provider.CreateParameter("DeliveryNumber", PO1.DeliveryNumber, DbType.String);
                    DbParameter SapDeliveryNumber = provider.CreateParameter("SapDeliveryNumber", PO1.SapDeliveryNumber, DbType.String);
                    DbParameter Dealer = provider.CreateParameter("Dealer", PO1.Dealer.DealerCode, DbType.String);
                    DbParameter DeliveryDate = provider.CreateParameter("DeliveryDate", PO1.DeliveryDate, DbType.DateTime);
                    DbParameter[] Params = new DbParameter[5] { AsnNumber, DeliveryNumber, SapDeliveryNumber, Dealer, DeliveryDate };
                    try
                    {
                        //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        //{
                        provider.Insert("ZDMS_InsertOrUpdatePurchaseOrderAsnSapDeliveryNumber", Params);
                        //    scope.Complete();
                        //}
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessageService("BDMS_PurchaseOrder", "GetSapDeliveryNumberIntegration", ex);
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_PurchaseOrder", "GetSapDeliveryNumberIntegration", ex);
            }
        }

        public void GetPurchaseOrderInvoiceIntegration()
        {
            int c = 0;
            TraceLogger.Log(DateTime.Now);
           // string DeliveryNumber = "80181437";

            List<string> SapDeliveryNumbers = GetPurchaseOrderInvoicePendingASN();
            foreach (string SapDeliveryNumber in SapDeliveryNumbers)
            {
                c = c + 1;
                PDMS_PurchaseOrderInvoice Inv = new SDMS_PurchaseOrder().getPurchaseOrderInvoiceFromSAP(SapDeliveryNumber);

                if(string.IsNullOrEmpty( Inv.Invoice))
                {
                    continue;
                }
                try
                {
                    long InvoiceID = 0;

                    DbParameter Invoice = provider.CreateParameter("Invoice", Inv.Invoice, DbType.String);
                    DbParameter InvoiceDate = provider.CreateParameter("InvoiceDate", Inv.InvoiceDate, DbType.DateTime );
                    DbParameter Currency = provider.CreateParameter("Currency ", Inv.Currency, DbType.String);
                    DbParameter DeliveryNumberP = provider.CreateParameter("SapDeliveryNumber ", SapDeliveryNumber, DbType.String);
                    DbParameter TotalValue = provider.CreateParameter("TotalValue", Inv.TotalValue, DbType.Decimal);
                    DbParameter TotalTCSValue = provider.CreateParameter("TotalTCSValue", Inv.TotalTCSValue, DbType.Decimal);

                    DbParameter OutP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                    DbParameter[] Params = new DbParameter[7] { Invoice, InvoiceDate, Currency, DeliveryNumberP, TotalValue, TotalTCSValue, OutP };

                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        provider.Insert("ZDMS_InsertOrUpdatePurchaseOrderInvoice", Params);
                        InvoiceID = Convert.ToInt64(OutP.Value);
                        foreach (PDMS_PurchaseOrderInvoiceItem Items in Inv.InvoiceItemS)
                        {
                            DbParameter InvoiceIDP = provider.CreateParameter("InvoiceID", InvoiceID, DbType.String);
                            DbParameter Item = provider.CreateParameter("Item", Items.Item, DbType.Int32);
                            DbParameter MaterialCode = provider.CreateParameter("MaterialCode ", Items.Material.MaterialCode, DbType.String);
                            DbParameter Qty = provider.CreateParameter("Qty", Items.Qty, DbType.Decimal);
                            DbParameter Rate = provider.CreateParameter("Rate", Items.Rate, DbType.Decimal);
                            DbParameter Discount = provider.CreateParameter("Discount", Items.Discount, DbType.Decimal);
                            DbParameter TaxableValue = provider.CreateParameter("TaxableValue", Items.TaxableValue, DbType.Decimal);
                            DbParameter SGST = provider.CreateParameter("SGST", Items.SGST, DbType.Decimal);
                            DbParameter IGST = provider.CreateParameter("IGST", Items.IGST, DbType.Decimal);
                            DbParameter SGSTValue = provider.CreateParameter("SGSTValue", Items.SGSTValue, DbType.Decimal);
                            DbParameter IGSTValue = provider.CreateParameter("IGSTValue", Items.IGSTValue, DbType.Decimal);
                            DbParameter TCS = provider.CreateParameter("TCS", Items.TCS, DbType.Decimal);
                            DbParameter TCSValue = provider.CreateParameter("TCSValue", Items.TCSValue, DbType.Decimal);
                            DbParameter Freight = provider.CreateParameter("Freight", Items.Freight, DbType.Decimal);
                            DbParameter Insururance = provider.CreateParameter("Insururance", Items.Insururance, DbType.Decimal);
                            DbParameter Packing = provider.CreateParameter("Packing", Items.Packing, DbType.Decimal);
                            DbParameter[] ParamIs = new DbParameter[16] { InvoiceIDP, Item, MaterialCode, Qty, Rate, Discount, TaxableValue, SGST, IGST, SGSTValue, IGSTValue, TCS, TCSValue, Freight, Insururance, Packing };
                            provider.Insert("ZDMS_InsertOrUpdatePurchaseOrderInvoiceItem", ParamIs);
                        }
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    new FileLogger().LogMessageService("BDMS_PurchaseOrder", "GetPurchaseOrder", ex);
                }
            }
        }

        public List<string> GetPurchaseOrderInvoicePendingASN()
        {
            TraceLogger.Log(DateTime.Now);
            List<string> SapDeliveryNumbers = new List<string>();
            try
            {
                using (DataSet DS = provider.Select("ZDMS_GetPurchaseOrderInvoicePendingASN"))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            SapDeliveryNumbers.Add(Convert.ToString(dr["SapDeliveryNumber"])); 
                        }
                    }
                }
                return SapDeliveryNumbers; 
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_PurchaseOrder", "GetPurchaseOrderInvoicePendingASN", ex); 
            }
            return SapDeliveryNumbers;
        }
        
        public DataTable GetPurchaseOrderInvoice()
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                using (DataSet DS = provider.Select("ZDMS_GetPurchaseOrderInvoicePendingASN"))
                {
                    if (DS != null)
                    {
                        return DS.Tables[0];
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_PurchaseOrder", "GetPurchaseOrderPerformance", ex);
                throw ex;
            }
            return null;
        }


        public DataTable GetPurchaseOrderReport(int? DealerID, string CustomerCode, string PurchaseOrderNo, DateTime? PurchaseOrderDateF, DateTime? PurchaseOrderDateT, string PurchaseOrderStatusID, string MaterialCode, int? PurchaseOrderTypeID,long UserID)
        {
            TraceLogger.Log(DateTime.Now);           
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter PurchaseOrderNoP = provider.CreateParameter("PurchaseOrderNo", string.IsNullOrEmpty(PurchaseOrderNo) ? null : PurchaseOrderNo, DbType.String);
                DbParameter PurchaseOrderDateFP = provider.CreateParameter("PurchaseOrderDateF", PurchaseOrderDateF, DbType.DateTime);
                DbParameter PurchaseOrderDateTP = provider.CreateParameter("PurchaseOrderDateT", PurchaseOrderDateT, DbType.DateTime);
                DbParameter PurchaseOrderStatusIDP = provider.CreateParameter("PurchaseOrderStatusID", string.IsNullOrEmpty(PurchaseOrderStatusID) ? null : PurchaseOrderStatusID, DbType.String);
                DbParameter PurchaseOrderTypeIDP = provider.CreateParameter("PurchaseOrderTypeID", PurchaseOrderTypeID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                DbParameter[] Params = new DbParameter[9] { DealerIDP, CustomerCodeP, PurchaseOrderNoP, PurchaseOrderDateFP, PurchaseOrderDateTP, PurchaseOrderStatusIDP, MaterialCodeP, PurchaseOrderTypeIDP, UserIDP };


                PDMS_PurchaseOrderN SOI = new PDMS_PurchaseOrderN();
                using (DataSet DataSet = provider.Select("ZDMS_GetPurchaseOrderReport", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
                return null;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return null;
        }

        public DataTable GetPurchaseOrderAsnReport(int? DealerID, string CustomerCode, string AsnNumber, DateTime? AsnDateF, DateTime? AsnDateT, int? PurchaseOrderAsnStatusID, string PurchaseOrderNo, DateTime? PurchaseOrderDateF, DateTime? PurchaseOrderDateT, int? PurchaseOrderTypeID, string MaterialCode, long UserID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);

                DbParameter AsnNumberP = provider.CreateParameter("AsnNumber", string.IsNullOrEmpty(AsnNumber) ? null : AsnNumber, DbType.String);
                DbParameter AsnDateFP = provider.CreateParameter("AsnDateF", AsnDateF, DbType.DateTime);
                DbParameter AsnDateTP = provider.CreateParameter("AsnDateT", AsnDateT, DbType.DateTime);
                DbParameter PurchaseOrderAsnStatusIDP = provider.CreateParameter("PurchaseOrderAsnStatusID", PurchaseOrderAsnStatusID, DbType.Int32);

                DbParameter PurchaseOrderNoP = provider.CreateParameter("PurchaseOrderNo", string.IsNullOrEmpty(PurchaseOrderNo) ? null : PurchaseOrderNo, DbType.String);
                DbParameter PurchaseOrderDateFP = provider.CreateParameter("PurchaseOrderDateF", PurchaseOrderDateF, DbType.DateTime);
                DbParameter PurchaseOrderDateTP = provider.CreateParameter("PurchaseOrderDateT", PurchaseOrderDateT, DbType.DateTime);

                DbParameter PurchaseOrderTypeIDP = provider.CreateParameter("PurchaseOrderTypeID", PurchaseOrderTypeID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                DbParameter[] Params = new DbParameter[12] { DealerIDP, CustomerCodeP, MaterialCodeP, AsnNumberP, AsnDateFP, AsnDateTP, PurchaseOrderAsnStatusIDP
                    , PurchaseOrderNoP, PurchaseOrderDateFP, PurchaseOrderDateTP, PurchaseOrderTypeIDP,UserIDP };


                PDMS_PurchaseOrderN SOI = new PDMS_PurchaseOrderN();
                using (DataSet DataSet = provider.Select("ZDMS_GetPurchaseOrderAsnReport", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
                return null;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return null;
        }

        public DataTable GetPurchaseOrderInvoiceReport(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, string PurchaseOrderNo, DateTime? PurchaseOrderDateF, DateTime? PurchaseOrderDateT, int? PurchaseOrderTypeID, string MaterialCode, long UserID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);

                DbParameter PurchaseOrderNoP = provider.CreateParameter("PurchaseOrderNo", string.IsNullOrEmpty(PurchaseOrderNo) ? null : PurchaseOrderNo, DbType.String);
                DbParameter PurchaseOrderDateFP = provider.CreateParameter("PurchaseOrderDateF", PurchaseOrderDateF, DbType.DateTime);
                DbParameter PurchaseOrderDateTP = provider.CreateParameter("PurchaseOrderDateT", PurchaseOrderDateT, DbType.DateTime);

                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);

                DbParameter PurchaseOrderTypeIDP = provider.CreateParameter("PurchaseOrderTypeID", PurchaseOrderTypeID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                DbParameter[] Params = new DbParameter[11] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, PurchaseOrderNoP, PurchaseOrderDateFP, PurchaseOrderDateTP, PurchaseOrderTypeIDP, MaterialCodeP, UserIDP };


                PDMS_PurchaseOrderN SOI = new PDMS_PurchaseOrderN();
                using (DataSet DataSet = provider.Select("ZDMS_GetPurchaseOrderInvoiceReport", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
                return null;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return null;
        }
       
    }
}
