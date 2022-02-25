using DataAccess;
using Properties;
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
    public class BDMS_SalesOrder
    {

        private IDataAccess provider;
        public BDMS_SalesOrder()
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

        public List<object> getSalesByYearAndMonth()
        {
            List<object> chartData = new List<object>();
            string ICT = "";
            try
            {
                using (DataSet DataSet = provider.Select("DB_SalesByYearAndMonth"))
                {
                    if (DataSet != null)
                    {
                        int cCount = DataSet.Tables[0].Columns.Count;
                        int rCount = DataSet.Tables[0].Rows.Count;


                        for (int i = 0; i < rCount; i++)
                        {
                            object[] obj = new object[cCount];
                            if (chartData.Count == 0)
                            {
                                for (int j = 0; j < cCount; j++)
                                {
                                    obj[j] = Convert.ToString(DataSet.Tables[0].Columns[j].ColumnName);
                                }
                                chartData.Add(obj);
                                obj = new object[cCount];
                            }
                            for (int j = 0; j < cCount; j++)
                            {
                                obj[j] = DataSet.Tables[0].Rows[i][j];
                            }
                            chartData.Add(obj);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return chartData;
        }
        public void IntegrationSalesOrder(string filter = "")
        {
            try
            {

                TraceLogger.Log(DateTime.Now);
                List<PDMS_SalesOrderItems> SOIs = new List<PDMS_SalesOrderItems>();

                //  string query   = "SELECT  * from pr_getsalesorderitems(" + filter + ")";

                string query = "select so.f_customer_id ,so.p_so_Id"
+ " ,so.r_order_date ,so.s_status So_status ,inv.p_inv_id ,inv.r_inv_date ,soi.f_material_id ,soi.r_unit_price  ,soi.r_order_qty,soi.r_gross_amt, soi.r_discount_amt  "
+ " ,ZFRH.r_cond_amt  as freight ,SGST.r_cond_amt as SGSTAmt , SGST.f_percentage as SGST ,CGST.r_cond_amt as CGSTAmt ,CGST.f_percentage as CGST ,IGST.r_cond_amt  as IGSTAmt "
+ " ,IGST.f_percentage  as IGST  ,so.f_Division ,so.f_location  ,so.r_contact_prsn ,so.r_contact_no ,so.s_tenant_id "
 + " from  dssor_sales_order_hdr so  "
 + " inner join dssor_sales_order_item soi on so.p_so_Id = soi.p_so_Id   "
 + " left Join dsinr_inv_hdr inv on inv.f_so_id = so.p_so_Id	 "
 + " left join dssor_sales_order_cond SGST on SGST.p_so_id = soi.p_so_Id and SGST.p_so_item = soi.p_so_item and SGST.p_condition_type = 'ZOSG' "
 + " left join dssor_sales_order_cond CGST on CGST.p_so_id = soi.p_so_Id and CGST.p_so_item = soi.p_so_item and CGST.p_condition_type = 'ZOCG' "
 + " left join dssor_sales_order_cond IGST on IGST.p_so_id = soi.p_so_Id and IGST.p_so_item = soi.p_so_item and IGST.p_condition_type = 'ZICG' "
 + " left join dssor_sales_order_cond ZFRH on ZFRH.p_so_id = soi.p_so_Id and ZFRH.p_so_item = soi.p_so_item and ( ZFRH.p_condition_type = 'ZFRH' or (ZFRH.p_condition_type = 'ZFRD')) "
 + " where	1 =1 ";
                query = query + filter;

                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_SalesOrderItems SOI = new PDMS_SalesOrderItems();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_SalesOrderItems();
                    SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) };
                    SOI.Customer = Convert.ToString(dr["f_customer_id"]);
                    SOI.SONumber = Convert.ToString(dr["p_so_Id"]);
                    SOI.SODate = Convert.ToDateTime(dr["r_order_date"]);
                    SOI.SOStatus = Convert.ToString(dr["So_status"]);
                    SOI.InvoiceNumber = Convert.ToString(dr["p_inv_id"]);
                    SOI.InvoiceDate = DBNull.Value == dr["r_inv_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_inv_date"]);
                    SOI.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]) };
                    SOI.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["r_unit_price"]));
                    SOI.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["r_order_qty"]));
                    SOI.Value = SOI.UnitBasicPrice * SOI.Qty;
                    SOI.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    SOI.DiscountedPrice = SOI.Value + SOI.Discount;
                    SOI.FreightAmount = DBNull.Value == dr["freight"] ? 0 : Convert.ToDecimal(dr["freight"]);
                    SOI.TaxableAmount = SOI.Discount + SOI.Value + SOI.FreightAmount;
                    SOI.SGST = DBNull.Value == dr["SGST"] ? (decimal?)null : Convert.ToDecimal(dr["SGST"]);
                    SOI.SGSTAmt = DBNull.Value == dr["SGSTAmt"] ? 0 : Convert.ToDecimal(dr["SGSTAmt"]);
                    SOI.CGST = DBNull.Value == dr["CGST"] ? (decimal?)null : Convert.ToDecimal(dr["CGST"]);
                    SOI.CGSTAmt = DBNull.Value == dr["CGSTAmt"] ? 0 : Convert.ToDecimal(dr["CGSTAmt"]);
                    SOI.IGST = DBNull.Value == dr["IGST"] ? (decimal?)null : Convert.ToDecimal(dr["IGST"]);
                    SOI.IGSTAmt = DBNull.Value == dr["IGSTAmt"] ? 0 : Convert.ToDecimal(dr["IGSTAmt"]);
                    SOI.Tax = SOI.SGSTAmt + SOI.CGSTAmt + SOI.IGSTAmt;
                    SOI.TotalAmt = SOI.Tax + SOI.TaxableAmount;
                    SOI.Location = Convert.ToString(dr["f_location"]);
                    SOI.ContactPerson = Convert.ToString(dr["r_contact_prsn"]);
                    SOI.ContactNumber = Convert.ToString(dr["r_contact_no"]);
                    SOIs.Add(SOI);
                }

                foreach (PDMS_SalesOrderItems SO in SOIs)
                {
                    try
                    {
                        DbParameter SaleOrderNumber = provider.CreateParameter("SaleOrderNumber", SO.SONumber, DbType.String);
                        DbParameter SaleOrderDate = provider.CreateParameter("SaleOrderDate", SO.SODate, DbType.DateTime);
                        DbParameter DealerCode = provider.CreateParameter("DealerCode ", SO.Dealer.DealerCode, DbType.String);
                        DbParameter CustomerCode = provider.CreateParameter("CustomerCode", SO.Customer, DbType.String);
                        DbParameter SaleOrderStatus = provider.CreateParameter("SaleOrderStatus", SO.SOStatus, DbType.String);
                        DbParameter InvoiceNumber = provider.CreateParameter("InvoiceNumber", SO.InvoiceNumber, DbType.String);
                        DbParameter InvoiceDate = provider.CreateParameter("InvoiceDate ", SO.InvoiceDate, DbType.DateTime);
                        DbParameter Location = provider.CreateParameter("Location", "", DbType.String);
                        DbParameter ContactPerson = provider.CreateParameter("ContactPerson", "SO.ContactPerson", DbType.String);
                        DbParameter ContactPersonNumber = provider.CreateParameter("ContactPersonNumber ", "", DbType.String);

                        DbParameter MaterialCode = provider.CreateParameter("MaterialCode ", SO.Material.MaterialCode, DbType.String);
                        DbParameter UnitPrice = provider.CreateParameter("UnitPrice", SO.UnitBasicPrice, DbType.Decimal);
                        DbParameter Qty = provider.CreateParameter("Qty", SO.Qty, DbType.Decimal);
                        DbParameter Value = provider.CreateParameter("Value", SO.Value, DbType.Decimal);
                        DbParameter Discount = provider.CreateParameter("Discount ", SO.Discount, DbType.Decimal);
                        DbParameter FreightAmt = provider.CreateParameter("FreightAmt", SO.FreightAmount, DbType.Decimal);

                        DbParameter TaxableAmt = provider.CreateParameter("TaxableAmt ", SO.TotalAmt, DbType.Decimal);
                        DbParameter SGST = provider.CreateParameter("SGST", SO.SGST, DbType.Decimal);
                        DbParameter SGSTAmt = provider.CreateParameter("SGSTAmt", SO.SGSTAmt, DbType.Decimal);
                        DbParameter CGST = provider.CreateParameter("CGST", SO.CGST, DbType.Decimal);
                        DbParameter CGSTAmt = provider.CreateParameter("CGSTAmt", SO.CGSTAmt, DbType.Decimal);
                        DbParameter IGST = provider.CreateParameter("IGST", SO.IGST, DbType.Decimal);
                        DbParameter IGSTAmt = provider.CreateParameter("IGSTAmt", SO.IGSTAmt, DbType.Decimal);

                        //DbParameter Qty = provider.CreateParameter("Qty", SO.Qty, DbType.String);
                        //DbParameter Value = provider.CreateParameter("Value", SO.Basic, DbType.String);
                        //DbParameter Discount = provider.CreateParameter("Discount ", SO.Discount, DbType.String);
                        //DbParameter FreightAmt = provider.CreateParameter("FreightAmt", SO.FreightInsurance, DbType.String);

                        DbParameter[] Params = new DbParameter[23] { SaleOrderNumber,SaleOrderDate,DealerCode,CustomerCode,SaleOrderStatus, InvoiceNumber,InvoiceDate,
                        Location,ContactPerson,ContactPersonNumber , MaterialCode,UnitPrice,Qty,Value,Discount, FreightAmt,TaxableAmt,SGST,SGSTAmt,CGST
                        ,CGSTAmt,IGST,IGSTAmt};

                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {

                            provider.Insert("ZDMS_InsertOrUpdateSaleOrder", Params); ;
                            scope.Complete();

                        }

                    }
                    catch (Exception e1)
                    {
                        new FileLogger().LogMessageService("BDMS_ICTicket", "IntegrationICTicket", e1);
                        //  throw e1;
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Material", "IntegrationMaterial", ex);
                //   throw ex;
            }

        }
        public string IntegrationSalesOrderInvoice(string filter = "")
        {
            string Re = "";
            try
            {

                TraceLogger.Log(DateTime.Now);
                List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();


                DateTime r_inv_date = DateTime.Now;
                string r_inv_dateString = "";

                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceLastDate"))
                {
                    if (DataSet != null)
                    {
                        r_inv_date = Convert.ToDateTime(DataSet.Tables[0].Rows[0]["InvoiceDate"]);
                        r_inv_date = r_inv_date.AddMinutes(-1);
                        r_inv_dateString = r_inv_date.Year.ToString() + "-" + r_inv_date.Month.ToString("00") + "-" + r_inv_date.Day.ToString("00") + " " + r_inv_date.ToShortTimeString();
                    }
                }
                string query = "";
                //                  query = "select   inv.f_customer_id ,inv.p_inv_id,inv.s_created_on  as r_inv_date,inv.r_price_grp ,inv.s_status,inv.s_object_type,invi.p_inv_item ,invi.f_material_id   "
                //+ " ,invi.r_unit_price ,invi.r_qty,invi.r_received_qty,invi.r_return_qty ,invi.r_gross_amt,invi.r_net_amt , invi.r_discount_amt,invi.r_tax_amt, ZFRH.r_amt  as freight  "
                //+ " ,inv.f_division, inv.f_location,so.r_contact_prsn ,so.r_contact_no, so.r_our_ref_id ,inv.s_tenant_id ,so.p_so_Id,so.r_order_date,so.r_our_ref_id,so.r_ref_date,so.r_model,so.r_model_no, so.r_remarks  "
                //+ " from  dsinr_inv_hdr inv   "
                //+ " inner join dsinr_inv_item invi on invi.k_id = inv.p_id  and invi.s_tenant_id = inv.s_tenant_id "
                //+ " inner join dssor_sales_order_hdr so on   so.p_so_Id = inv.f_so_id    and so.s_tenant_id = inv.s_tenant_id"
                //+ " left join dsinr_inv_cond ZFRH on ZFRH.k_inv_id = invi.k_inv_id and ZFRH.k_inv_item = invi.p_inv_item and (ZFRH.k_cond_type = 'ZFRH' or (ZFRH.k_cond_type = 'ZFRD')) "
                //+ " where	invi.r_gross_amt <> 0 and (inv.s_created_on>='" + r_inv_dateString + "' or inv.s_modified_on>='" + r_inv_dateString + "') order by  inv.s_created_on";

                query = "select   inv.f_customer_id ,inv.p_inv_id, invi.k_id ,inv.s_created_on  as r_inv_date,inv.r_price_grp ,inv.s_status,inv.s_object_type,invi.p_inv_item ,invi.f_material_id   "
                + " ,invi.r_unit_price ,invi.r_qty,invi.r_received_qty,invi.r_return_qty ,invi.r_gross_amt,invi.r_net_amt , invi.r_discount_amt,invi.r_tax_amt, ZFRH.r_amt  as freight  "
                + " ,inv.f_division, inv.f_location,so.r_contact_prsn ,so.r_contact_no, so.r_our_ref_id ,inv.s_tenant_id ,so.p_so_Id,so.r_order_date,so.r_our_ref_id,so.r_ref_date,so.r_model,so.r_model_no, so.r_remarks  "
                + " from  dsinr_inv_hdr inv   "
                + " inner join dsinr_inv_item invi on invi.k_id = inv.p_id  and invi.s_tenant_id = inv.s_tenant_id "
                + " inner join dssor_sales_order_hdr so on   so.p_so_Id = inv.f_so_id    and so.s_tenant_id = inv.s_tenant_id"
                + " left join dsinr_inv_cond ZFRH on ZFRH.k_inv_id = invi.k_inv_id and ZFRH.k_inv_item = invi.p_inv_item and (ZFRH.k_cond_type = 'ZFRH' or (ZFRH.k_cond_type = 'ZFRD')) "
                + " where invi.r_gross_amt <> 0  and EXTRACT(YEAR FROM  inv.s_created_on) >=  2021  and invi.s_action is null and  inv.s_status ='RELEASED'  and so.s_tenant_id <> 20   limit 1000";

                // and EXTRACT(MONTH FROM  inv.s_created_on) >=  9

                if (!string.IsNullOrEmpty(filter))
                {
                    query = "select   inv.f_customer_id ,inv.p_inv_id, ,invi.k_id ,inv.s_created_on  as r_inv_date,inv.r_price_grp ,inv.s_status,inv.s_object_type,invi.p_inv_item ,invi.f_material_id   "
+ " ,invi.r_unit_price ,invi.r_qty,invi.r_received_qty,invi.r_return_qty ,invi.r_gross_amt,invi.r_net_amt , invi.r_discount_amt,invi.r_tax_amt, ZFRH.r_amt  as freight  "
+ " ,inv.f_division, inv.f_location,so.r_contact_prsn ,so.r_contact_no, so.r_our_ref_id ,inv.s_tenant_id ,so.p_so_Id,so.r_order_date,so.r_our_ref_id,so.r_ref_date,so.r_model,so.r_model_no, so.r_remarks  "
+ " from  dsinr_inv_hdr inv   "
+ " inner join dsinr_inv_item invi on invi.k_id = inv.p_id    and invi.s_tenant_id = inv.s_tenant_id  "
+ " inner join dssor_sales_order_hdr so on   so.p_so_Id = inv.f_so_id    and so.s_tenant_id = inv.s_tenant_id"
+ " left join dsinr_inv_cond ZFRH on ZFRH.k_inv_id = invi.k_inv_id and ZFRH.k_inv_item = invi.p_inv_item and (ZFRH.k_cond_type = 'ZFRH' or (ZFRH.k_cond_type = 'ZFRD')) "
+ " where	invi.r_gross_amt <> 0  " + filter;

                }
                //  query = query + filter;

                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_SalesInvoice();
                    i = i + 1;
                    SOI.InvoiceID = i;
                    SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) };
                    SOI.Dealer.DealerCode = SOI.Dealer.DealerCode == "23" ? "9004" : SOI.Dealer.DealerCode;
                    SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["f_customer_id"]) };
                    SOI.InvoiceNumber = Convert.ToString(dr["p_inv_id"]);

                    SOI.k_id = Convert.ToString(dr["k_id"]);

                    SOI.InvoiceDate = DBNull.Value == dr["r_inv_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_inv_date"]);
                    SOI.Status = Convert.ToString(dr["s_status"]);
                    SOI.SalesType = new PDMS_SalesType() { SalesTypeCode = Convert.ToInt32(dr["s_object_type"]) };
                    SOI.Division = Convert.ToString(dr["f_division"]);
                    SOI.Location = Convert.ToString(dr["f_location"]);
                    SOI.ContactPerson = Convert.ToString(dr["r_contact_prsn"]);
                    SOI.ContactNumber = Convert.ToString(dr["r_contact_no"]);
                    SOI.SalesPerson = Convert.ToString(dr["r_our_ref_id"]);

                    SOI.SaleOrderNumber = Convert.ToString(dr["p_so_Id"]);
                    SOI.SaleOrderDate = DBNull.Value == dr["r_order_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_order_date"]);

                    SOI.Reference = Convert.ToString(dr["r_our_ref_id"]);
                    SOI.ReferenceDate = DBNull.Value == dr["r_ref_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_ref_date"]);
                    SOI.McMode = Convert.ToString(dr["r_model"]);
                    SOI.MachineSlNo = Convert.ToString(dr["r_model_no"]);
                    SOI.Remarks = Convert.ToString(dr["r_remarks"]);

                    SOI.InvoiceItem = new PDMS_SalesInvoiceItem();

                    SOI.InvoiceItem.ItemNo = Convert.ToInt32("0" + Convert.ToString(dr["p_inv_item"]));
                    SOI.InvoiceItem.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]) };

                    SOI.InvoiceItem.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["r_unit_price"]));
                    SOI.InvoiceItem.TaxableAmount = DBNull.Value == dr["r_net_amt"] ? 0 : Convert.ToDecimal(dr["r_net_amt"]);
                    SOI.InvoiceItem.Value = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);

                    decimal t = 0;
                    if ((SOI.InvoiceItem.Value > SOI.InvoiceItem.TaxableAmount) && (SOI.InvoiceItem.TaxableAmount != 0))
                    {
                        t = SOI.InvoiceItem.Value;
                        SOI.InvoiceItem.Value = SOI.InvoiceItem.TaxableAmount;
                        SOI.InvoiceItem.TaxableAmount = t;
                    }
                    // SOI.InvoiceItem.ReceivedQty = Convert.ToDecimal("0" + Convert.ToString(dr["r_received_qty"]));
                    SOI.InvoiceItem.ReturnQty = Convert.ToDecimal("0" + Convert.ToString(dr["r_return_qty"]));

                    SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["r_qty"]));
                    SOI.InvoiceItem.Qty = SOI.InvoiceItem.Qty == 0 ? 1 : SOI.InvoiceItem.Qty;
                    SOI.InvoiceItem.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    SOI.InvoiceItem.Tax = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    if (SOI.InvoiceItem.UnitBasicPrice != 0)
                    {
                        SOI.InvoiceItem.Value = SOI.InvoiceItem.UnitBasicPrice * SOI.InvoiceItem.Qty;
                        SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;
                        SOI.InvoiceItem.CalType = "UnitBasicPrice";
                    }
                    else if (SOI.InvoiceItem.Value != 0)
                    {
                        SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;
                        SOI.InvoiceItem.UnitBasicPrice = SOI.InvoiceItem.Value / SOI.InvoiceItem.Qty;
                        SOI.InvoiceItem.CalType = "Value";
                    }
                    else if (SOI.InvoiceItem.TaxableAmount != 0)
                    {
                        SOI.InvoiceItem.Value = SOI.InvoiceItem.TaxableAmount - SOI.InvoiceItem.Discount;
                        SOI.InvoiceItem.UnitBasicPrice = SOI.InvoiceItem.Value / SOI.InvoiceItem.Qty;
                        SOI.InvoiceItem.CalType = "TaxableAmount";
                    }

                    SOI.InvoiceItem.FreightAmount = DBNull.Value == dr["freight"] ? 0 : Convert.ToDecimal(dr["freight"]);
                    SOIs.Add(SOI);

                }
                Re = Re + " " + SOIs.Count();
                foreach (PDMS_SalesInvoice SO in SOIs)
                {
                    try
                    {
                        DbParameter InvoiceNumber = provider.CreateParameter("InvoiceNumber", SO.InvoiceNumber, DbType.String);
                        DbParameter InvoiceDate = provider.CreateParameter("InvoiceDate", SO.InvoiceDate, DbType.DateTime);
                        DbParameter DealerCode = provider.CreateParameter("DealerCode ", SO.Dealer.DealerCode, DbType.String);
                        DbParameter CustomerCode = provider.CreateParameter("CustomerCode", SO.Customer.CustomerCode, DbType.String);
                        DbParameter SaleOrderInvoiceStatus = provider.CreateParameter("SaleOrderInvoiceStatus", SO.Status, DbType.String);
                        DbParameter SalesTypeCode = provider.CreateParameter("SalesTypeCode", SO.SalesType.SalesTypeCode, DbType.Int32);
                        DbParameter SaleOrderNumber = provider.CreateParameter("SaleOrderNumber", SO.SaleOrderNumber, DbType.String);
                        DbParameter SaleOrderDate = provider.CreateParameter("SaleOrderDate", SO.SaleOrderDate, DbType.DateTime);
                        DbParameter Division = provider.CreateParameter("Division", SO.Division, DbType.String);
                        DbParameter Location = provider.CreateParameter("Location", SO.Location, DbType.String);
                        DbParameter ContactPerson = provider.CreateParameter("ContactPerson", SO.ContactPerson, DbType.String);
                        DbParameter ContactPersonNumber = provider.CreateParameter("ContactPersonNumber ", SO.ContactNumber, DbType.String);
                        DbParameter SalesPerson = provider.CreateParameter("SalesPerson", SO.SalesPerson, DbType.String);

                        DbParameter Reference = provider.CreateParameter("Reference", SO.Reference, DbType.String);
                        DbParameter ReferenceDate = provider.CreateParameter("ReferenceDate", SO.ReferenceDate, DbType.DateTime);
                        DbParameter McMode = provider.CreateParameter("McMode", SO.McMode, DbType.String);
                        DbParameter MachineSlNo = provider.CreateParameter("MachineSlNo", SO.MachineSlNo, DbType.String);
                        DbParameter Remarks = provider.CreateParameter("Remarks ", SO.Remarks, DbType.String);



                        DbParameter ItemNo = provider.CreateParameter("ItemNo ", SO.InvoiceItem.ItemNo, DbType.Int32);
                        DbParameter MaterialCode = provider.CreateParameter("MaterialCode", SO.InvoiceItem.Material.MaterialCode, DbType.String);
                        DbParameter UnitPrice = provider.CreateParameter("UnitPrice", SO.InvoiceItem.UnitBasicPrice, DbType.Decimal);
                        DbParameter Qty = provider.CreateParameter("Qty", SO.InvoiceItem.Qty, DbType.Decimal);
                        //  DbParameter ReceivedQty = provider.CreateParameter("ReceivedQty", SO.InvoiceItem.ReceivedQty, DbType.Decimal);
                        DbParameter ReceivedQty = provider.CreateParameter("ReturnQty", SO.InvoiceItem.ReturnQty, DbType.Decimal);
                        DbParameter Value = provider.CreateParameter("Value", SO.InvoiceItem.Value, DbType.Decimal);
                        DbParameter Discount = provider.CreateParameter("Discount", SO.InvoiceItem.Discount, DbType.Decimal);
                        DbParameter FreightAmt = provider.CreateParameter("FreightAmt", SO.InvoiceItem.FreightAmount, DbType.Decimal);

                        DbParameter TaxableAmt = provider.CreateParameter("TaxableAmt", SO.InvoiceItem.TaxableAmount, DbType.Decimal);
                        DbParameter Tax = provider.CreateParameter("Tax", SO.InvoiceItem.Tax, DbType.Decimal);

                        //  DbParameter SGST = provider.CreateParameter("SGST", SO.InvoiceItem.SGST, DbType.Decimal);
                        //  DbParameter SGSTAmt = provider.CreateParameter("SGSTAmt", SO.InvoiceItem.SGSTAmt, DbType.Decimal);
                        //  DbParameter IGST = provider.CreateParameter("IGST", SO.InvoiceItem.IGST, DbType.Decimal);
                        //  DbParameter IGSTAmt = provider.CreateParameter("IGSTAmt", SO.InvoiceItem.IGSTAmt, DbType.Decimal);
                        DbParameter CalType = provider.CreateParameter("CalType", string.IsNullOrEmpty(SO.InvoiceItem.CalType) ? null : SO.InvoiceItem.CalType, DbType.String);

                        DbParameter[] Params = new DbParameter[29] { InvoiceNumber,InvoiceDate,DealerCode,CustomerCode,SaleOrderInvoiceStatus,SalesTypeCode, SaleOrderNumber,SaleOrderDate,Division,
                        Location,ContactPerson,ContactPersonNumber,SalesPerson,Reference,ReferenceDate,McMode,MachineSlNo,Remarks,ItemNo , MaterialCode,UnitPrice,Qty,ReceivedQty,Value,Discount, FreightAmt,TaxableAmt,Tax,CalType};

                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            provider.Insert("ZDMS_InsertOrUpdateSaleOrderInvoice", Params); ;
                            scope.Complete();
                        }

                        new NpgsqlServer().ExecuteReader("update dsinr_inv_item set s_action = '0' where k_id = '" + SO.k_id + "' and f_material_id = '" + SO.InvoiceItem.Material.MaterialCode + "' and p_inv_item = " + SO.InvoiceItem.ItemNo + " and s_tenant_id = " + SO.Dealer.DealerCode);
                    }
                    catch (Exception e1)
                    {
                        Re = Re + " " + e1.Message + SO.InvoiceNumber;
                        new FileLogger().LogMessageService("BDMS_ICTicket", "IntegrationSalesOrderInvoice", e1);
                        //  throw e1;
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Material", "IntegrationSalesOrderInvoice", ex);
                Re = Re + " " + ex.Message;
                //   throw ex;
            }
            return Re;
        }
        public string IntegrationSalesOrderInvoiceReturn()
        {
            string Re = "";
            try
            {

                TraceLogger.Log(DateTime.Now);
                List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
                DateTime r_inv_date = DateTime.Now;
                string r_inv_dateString = "";

                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceReturnLastDate"))
                {
                    if (DataSet != null)
                    {
                        r_inv_date = Convert.ToDateTime(DataSet.Tables[0].Rows[0]["InvoiceDate"]);
                        r_inv_date = r_inv_date.AddMinutes(-1);
                        r_inv_dateString = r_inv_date.Year.ToString() + "-" + r_inv_date.Month.ToString("00") + "-" + r_inv_date.Day.ToString("00") + " " + r_inv_date.ToShortTimeString();
                    }
                }
                string query = " select h.p_sr_id,h.s_created_on,h.f_customer_id,h.r_gross_amt,h.f_order_type,h.s_status,h.s_tenant_id,h.s_object_type "
  + " ,i.p_sr_item,i.r_return_qty,i.f_material_id,i.r_net_amt,i.r_tax_amt,i.r_unit_price,i.f_uom,i.r_approved_qty    "
  + " from dssor_sales_return_hdr h  inner join dssor_sales_return_item i on i.p_sr_id = h.p_sr_id where     "

+ " where r_unit_price is not null  and  (h.s_created_on>='" + r_inv_dateString + "' or h.s_modified_on>='" + r_inv_dateString + "') order by  h.s_created_on";



                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_SalesInvoice();
                    i = i + 1;
                    SOI.InvoiceID = i;
                    SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) };
                    SOI.Dealer.DealerCode = SOI.Dealer.DealerCode == "23" ? "9004" : SOI.Dealer.DealerCode;
                    SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["f_customer_id"]) };
                    SOI.InvoiceNumber = Convert.ToString(dr["p_inv_id"]);
                    SOI.InvoiceDate = DBNull.Value == dr["r_inv_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_inv_date"]);
                    SOI.Status = Convert.ToString(dr["s_status"]);
                    SOI.SalesType = new PDMS_SalesType() { SalesTypeCode = Convert.ToInt32(dr["s_object_type"]) };
                    SOI.Division = Convert.ToString(dr["f_division"]);
                    SOI.Location = Convert.ToString(dr["f_location"]);
                    SOI.ContactPerson = Convert.ToString(dr["r_contact_prsn"]);
                    SOI.ContactNumber = Convert.ToString(dr["r_contact_no"]);

                    SOI.SaleOrderNumber = Convert.ToString(dr["p_so_Id"]);
                    SOI.SaleOrderDate = DBNull.Value == dr["r_order_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_order_date"]);

                    SOI.Reference = Convert.ToString(dr["r_our_ref_id"]);
                    SOI.ReferenceDate = DBNull.Value == dr["r_ref_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_ref_date"]);
                    SOI.McMode = Convert.ToString(dr["r_model"]);
                    SOI.MachineSlNo = Convert.ToString(dr["r_model_no"]);
                    SOI.Remarks = Convert.ToString(dr["r_remarks"]);

                    SOI.InvoiceItem = new PDMS_SalesInvoiceItem();

                    SOI.InvoiceItem.ItemNo = Convert.ToInt32("0" + Convert.ToString(dr["p_inv_item"]));
                    SOI.InvoiceItem.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]) };

                    SOI.InvoiceItem.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["r_unit_price"]));
                    SOI.InvoiceItem.TaxableAmount = DBNull.Value == dr["r_net_amt"] ? 0 : Convert.ToDecimal(dr["r_net_amt"]);
                    SOI.InvoiceItem.Value = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);

                    decimal t = 0;
                    if ((SOI.InvoiceItem.Value > SOI.InvoiceItem.TaxableAmount) && (SOI.InvoiceItem.TaxableAmount != 0))
                    {
                        t = SOI.InvoiceItem.Value;
                        SOI.InvoiceItem.Value = SOI.InvoiceItem.TaxableAmount;
                        SOI.InvoiceItem.TaxableAmount = t;
                    }

                    SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["r_qty"]));
                    SOI.InvoiceItem.Qty = SOI.InvoiceItem.Qty == 0 ? 1 : SOI.InvoiceItem.Qty;
                    SOI.InvoiceItem.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    SOI.InvoiceItem.Tax = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    if (SOI.InvoiceItem.UnitBasicPrice != 0)
                    {
                        SOI.InvoiceItem.Value = SOI.InvoiceItem.UnitBasicPrice * SOI.InvoiceItem.Qty;
                        SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;
                        SOI.InvoiceItem.CalType = "UnitBasicPrice";
                    }
                    else if (SOI.InvoiceItem.Value != 0)
                    {
                        SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;
                        SOI.InvoiceItem.UnitBasicPrice = SOI.InvoiceItem.Value / SOI.InvoiceItem.Qty;
                        SOI.InvoiceItem.CalType = "Value";
                    }
                    else if (SOI.InvoiceItem.TaxableAmount != 0)
                    {
                        SOI.InvoiceItem.Value = SOI.InvoiceItem.TaxableAmount - SOI.InvoiceItem.Discount;
                        SOI.InvoiceItem.UnitBasicPrice = SOI.InvoiceItem.Value / SOI.InvoiceItem.Qty;
                        SOI.InvoiceItem.CalType = "TaxableAmount";
                    }

                    SOI.InvoiceItem.FreightAmount = DBNull.Value == dr["freight"] ? 0 : Convert.ToDecimal(dr["freight"]);
                    SOIs.Add(SOI);
                    //  SOI.InvoiceItem.TaxableAmount =   SOI.InvoiceItem.Value ;

                    //if (SOI.InvoiceItem.Tax == 0)
                    //{
                    //    SOI.InvoiceItem.SGST = (decimal?)null;
                    //    SOI.InvoiceItem.SGSTAmt = 0;
                    //    SOI.InvoiceItem.IGST = (decimal?)null;
                    //    SOI.InvoiceItem.IGSTAmt = 0;
                    //}
                    //else
                    //{
                    //    if (SOI.InvoiceItem.Value == 0)
                    //    {
                    //        SOI.InvoiceItem.SGST = (decimal?)null;
                    //        SOI.InvoiceItem.SGSTAmt = 0;
                    //        SOI.InvoiceItem.IGST = (decimal?)null;
                    //        SOI.InvoiceItem.IGSTAmt = 0;

                    //    }
                    //    else  if (Convert.ToString(dr["r_price_grp"]) == "09")
                    //    {
                    //        SOI.InvoiceItem.SGST = SOI.InvoiceItem.Tax * 50 / SOI.InvoiceItem.TaxableAmount;
                    //        SOI.InvoiceItem.SGSTAmt = SOI.InvoiceItem.Tax / 2;

                    //        SOI.InvoiceItem.IGST =  (decimal?)null;
                    //        SOI.InvoiceItem.IGSTAmt = 0;

                    //    }
                    //    else if (Convert.ToString(dr["r_price_grp"]) == "07")
                    //    {
                    //        SOI.InvoiceItem.SGST = (decimal?)null;
                    //        SOI.InvoiceItem.SGSTAmt = 0;

                    //        SOI.InvoiceItem.IGST = SOI.InvoiceItem.Tax * 100 / SOI.InvoiceItem.TaxableAmount;
                    //        SOI.InvoiceItem.IGSTAmt = SOI.InvoiceItem.Tax;
                    //    }
                    //    else
                    //    {
                    //        SOI.InvoiceItem.SGST = (decimal?)null;
                    //        SOI.InvoiceItem.SGSTAmt = 0;
                    //        SOI.InvoiceItem.IGST = (decimal?)null;
                    //        SOI.InvoiceItem.IGSTAmt = 0;
                    //    }
                    //}
                    // SOI.InvoiceItem.TotalAmt = SOI.InvoiceItem.Tax + SOI.InvoiceItem.TaxableAmount; 
                }
                Re = Re + " " + SOIs.Count();
                foreach (PDMS_SalesInvoice SO in SOIs)
                {
                    try
                    {
                        DbParameter InvoiceNumber = provider.CreateParameter("InvoiceNumber", SO.InvoiceNumber, DbType.String);
                        DbParameter InvoiceDate = provider.CreateParameter("InvoiceDate", SO.InvoiceDate, DbType.DateTime);
                        DbParameter DealerCode = provider.CreateParameter("DealerCode ", SO.Dealer.DealerCode, DbType.String);
                        DbParameter CustomerCode = provider.CreateParameter("CustomerCode", SO.Customer.CustomerCode, DbType.String);
                        DbParameter SaleOrderInvoiceStatus = provider.CreateParameter("SaleOrderInvoiceStatus", SO.Status, DbType.String);
                        DbParameter SalesTypeCode = provider.CreateParameter("SalesTypeCode", SO.SalesType.SalesTypeCode, DbType.Int32);
                        DbParameter SaleOrderNumber = provider.CreateParameter("SaleOrderNumber", SO.SaleOrderNumber, DbType.String);
                        DbParameter SaleOrderDate = provider.CreateParameter("SaleOrderDate", SO.SaleOrderDate, DbType.DateTime);
                        DbParameter Division = provider.CreateParameter("Division", SO.Division, DbType.String);
                        DbParameter Location = provider.CreateParameter("Location", SO.Location, DbType.String);
                        DbParameter ContactPerson = provider.CreateParameter("ContactPerson", SO.ContactPerson, DbType.String);
                        DbParameter ContactPersonNumber = provider.CreateParameter("ContactPersonNumber ", SO.ContactNumber, DbType.String);

                        DbParameter Reference = provider.CreateParameter("Reference", SO.Reference, DbType.String);
                        DbParameter ReferenceDate = provider.CreateParameter("ReferenceDate", SO.ReferenceDate, DbType.DateTime);
                        DbParameter McMode = provider.CreateParameter("McMode", SO.McMode, DbType.String);
                        DbParameter MachineSlNo = provider.CreateParameter("MachineSlNo", SO.MachineSlNo, DbType.String);
                        DbParameter Remarks = provider.CreateParameter("Remarks ", SO.Remarks, DbType.String);



                        DbParameter ItemNo = provider.CreateParameter("ItemNo ", SO.InvoiceItem.ItemNo, DbType.Int32);
                        DbParameter MaterialCode = provider.CreateParameter("MaterialCode", SO.InvoiceItem.Material.MaterialCode, DbType.String);
                        DbParameter UnitPrice = provider.CreateParameter("UnitPrice", SO.InvoiceItem.UnitBasicPrice, DbType.Decimal);
                        DbParameter Qty = provider.CreateParameter("Qty", SO.InvoiceItem.Qty, DbType.Decimal);

                        DbParameter Value = provider.CreateParameter("Value", SO.InvoiceItem.Value, DbType.Decimal);
                        DbParameter Discount = provider.CreateParameter("Discount", SO.InvoiceItem.Discount, DbType.Decimal);
                        DbParameter FreightAmt = provider.CreateParameter("FreightAmt", SO.InvoiceItem.FreightAmount, DbType.Decimal);

                        DbParameter TaxableAmt = provider.CreateParameter("TaxableAmt", SO.InvoiceItem.TaxableAmount, DbType.Decimal);
                        DbParameter Tax = provider.CreateParameter("Tax", SO.InvoiceItem.Tax, DbType.Decimal);

                        //   DbParameter SGST = provider.CreateParameter("SGST", SO.InvoiceItem.SGST, DbType.Decimal);
                        //  DbParameter SGSTAmt = provider.CreateParameter("SGSTAmt", SO.InvoiceItem.SGSTAmt, DbType.Decimal);
                        //  DbParameter IGST = provider.CreateParameter("IGST", SO.InvoiceItem.IGST, DbType.Decimal);
                        //  DbParameter IGSTAmt = provider.CreateParameter("IGSTAmt", SO.InvoiceItem.IGSTAmt, DbType.Decimal);
                        DbParameter CalType = provider.CreateParameter("CalType", string.IsNullOrEmpty(SO.InvoiceItem.CalType) ? null : SO.InvoiceItem.CalType, DbType.String);

                        DbParameter[] Params = new DbParameter[27] { InvoiceNumber,InvoiceDate,DealerCode,CustomerCode,SaleOrderInvoiceStatus,SalesTypeCode, SaleOrderNumber,SaleOrderDate,Division,
                        Location,ContactPerson,ContactPersonNumber,Reference,ReferenceDate,McMode,MachineSlNo,Remarks,ItemNo , MaterialCode,UnitPrice,Qty,Value,Discount, FreightAmt,TaxableAmt,Tax,CalType};

                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {

                            provider.Insert("ZDMS_InsertOrUpdateSaleOrderInvoice", Params); ;
                            scope.Complete();

                        }
                    }
                    catch (Exception e1)
                    {
                        Re = Re + " " + e1.Message + SO.InvoiceNumber;
                        new FileLogger().LogMessageService("BDMS_ICTicket", "IntegrationSalesOrderInvoice", e1);
                        //  throw e1;
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Material", "IntegrationSalesOrderInvoice", ex);
                Re = Re + " " + ex.Message;
                //   throw ex;
            }
            return Re;
        }
        public List<PDMS_SalesOrder> GetSalesOrder(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesOrder> SOIs = new List<PDMS_SalesOrder>();
            try
            {
                string query = "SELECT  * from pr_getsalesorderBasedOnInvoice(" + filter + ")";

                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_SalesOrder SOI = new PDMS_SalesOrder();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_SalesOrder();
                    SOI.SalesOrderID = Convert.ToString(dr["SalesOrderID"]);
                    SOI.SalesOrderNumber = Convert.ToString(dr["SalesOrderID"]);
                    SOI.SalesOrderDate = DBNull.Value == dr["so_date"] ? (DateTime?)null : Convert.ToDateTime(dr["so_date"]);
                    SOI.SalesOrderStatus = Convert.ToString(dr["SalesOrderStatus"]);
                    SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["Dealer_Code"]) };
                    SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                    SOI.InvoiceNumber = Convert.ToString(dr["p_inv_id"]);
                    SOI.InvoiceDate = DBNull.Value == dr["r_inv_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_inv_date"]);
                    SOI.SalesOrderItem = new PDMS_SalesOrderItems();
                    SOI.SalesOrderItem.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["MaterialCode"]),
                        MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                        HSN = Convert.ToString(dr["HSN"]),
                        MaterialType = Convert.ToString(dr["MaterialType"]),
                        MaterialDivision = Convert.ToString(dr["MaterialDivision"])
                    };
                    SOI.SalesOrderItem.UnitBasicPrice = DBNull.Value == dr["UnitBasicPrice"] ? 0 : Convert.ToDecimal(dr["UnitBasicPrice"]);
                    SOI.SalesOrderItem.Qty = DBNull.Value == dr["r_qty"] ? 0 : Convert.ToDecimal(dr["r_qty"]);
                    SOI.SalesOrderItem.Value = Convert.ToDecimal(dr["r_qty"]);
                    SOI.SalesOrderItem.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    SOI.SalesOrderItem.DiscountedPrice = SOI.SalesOrderItem.Value - SOI.SalesOrderItem.Discount;
                    SOI.SalesOrderItem.Tax = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    SOI.SalesOrderItem.FreightAmount = DBNull.Value == dr["ZFRH"] ? 0 : Convert.ToDecimal(dr["ZFRH"]);
                    SOI.SalesOrderItem.TotalAmt = SOI.SalesOrderItem.DiscountedPrice + SOI.SalesOrderItem.Tax + SOI.SalesOrderItem.FreightAmount;

                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrder", ex);
                throw ex;
            }
            return SOIs;
        }

        public List<PDMS_SalesInvoice> GetSaleOrderInvoicePartsReport(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };


                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoicePartsReport", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]), State = Convert.ToString(dr["DealerState"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]), State = new PDMS_State() { State = Convert.ToString(dr["CustomerState"]) } };
                            SOI.GSTNo = Convert.ToString(dr["GSTNO"]);
                            SOI.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            SOI.InvoiceDate = DBNull.Value == dr["InvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["InvoiceDate"]);

                            SOI.SaleOrderNumber = Convert.ToString(dr["SaleOrderNumber"]);
                            SOI.SaleOrderDate = DBNull.Value == dr["SaleOrderDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SaleOrderDate"]);

                            SOI.Status = Convert.ToString(dr["SaleOrderInvoiceStatus"]);
                            SOI.Location = Convert.ToString(dr["Location"]);
                            SOI.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            SOI.ContactNumber = Convert.ToString(dr["ContactNumber"]);
                            SOI.SalesPerson = Convert.ToString(dr["SalesPerson"]);
                            SOI.Reference = Convert.ToString(dr["ContactPerson"]);
                            SOI.ReferenceDate = DBNull.Value == dr["ReferenceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["ReferenceDate"]);
                            SOI.McMode = Convert.ToString(dr["McMode"]);
                            SOI.MachineSlNo = Convert.ToString(dr["MachineSlNo"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();

                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                HSN = Convert.ToString(dr["HSNCode"]),
                                MaterialType = Convert.ToString(dr["MaterialType"]),
                                Product = Convert.ToString(dr["Product"]),
                                ProductGroup = Convert.ToString(dr["ProductGroup"])
                            };
                            SOI.InvoiceItem.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["UnitBasicPrice"]));
                            SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["Qty"]));
                            SOI.InvoiceItem.ReturnQty = Convert.ToDecimal("0" + Convert.ToString(dr["ReturnQty"]));
                            SOI.InvoiceItem.Value = SOI.InvoiceItem.UnitBasicPrice * SOI.InvoiceItem.Qty;

                            SOI.InvoiceItem.Discount = Convert.ToDecimal(dr["Discount"]);

                            SOI.InvoiceItem.DiscountedPrice = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;

                            SOI.InvoiceItem.FreightAmount = DBNull.Value == dr["FreightAmount"] ? 0 : Convert.ToDecimal(dr["FreightAmount"]);

                            SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Discount + SOI.InvoiceItem.Value + SOI.InvoiceItem.FreightAmount;
                            SOI.InvoiceItem.SGST = DBNull.Value == dr["SGST"] ? (decimal?)null : Convert.ToDecimal(dr["SGST"]);
                            SOI.InvoiceItem.SGSTAmt = Convert.ToDecimal(dr["SGSTAmt"]);
                            SOI.InvoiceItem.CGST = DBNull.Value == dr["CGST"] ? (decimal?)null : Convert.ToDecimal(dr["CGST"]);
                            SOI.InvoiceItem.CGSTAmt = Convert.ToDecimal(dr["CGSTAmt"]);

                            SOI.InvoiceItem.IGST = DBNull.Value == dr["IGST"] ? (decimal?)null : Convert.ToDecimal(dr["IGST"]);
                            SOI.InvoiceItem.IGSTAmt = Convert.ToDecimal(dr["IGSTAmt"]);
                            SOI.InvoiceItem.Tax = SOI.InvoiceItem.SGSTAmt + SOI.InvoiceItem.CGSTAmt + SOI.InvoiceItem.IGSTAmt;
                            SOI.InvoiceItem.TaxP = (SOI.InvoiceItem.SGST == null ? 0 : (decimal)SOI.InvoiceItem.SGST)
                                + (SOI.InvoiceItem.CGST == null ? 0 : (decimal)SOI.InvoiceItem.CGST)
                                + (SOI.InvoiceItem.IGST == null ? 0 : (decimal)SOI.InvoiceItem.IGST);
                            SOI.InvoiceItem.TotalAmt = SOI.InvoiceItem.Tax + SOI.InvoiceItem.TaxableAmount;


                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSaleOrderInvoicePartsDealerAndMaterialWise(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };

                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoicePartsDealerAndMaterialWise", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.HeaderCount = Convert.ToInt64(dr["HeaderCount"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                            };
                            SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                            SOI.InvoiceItem.NetAmount = DBNull.Value == dr["NetAmount"] ? 0 : Convert.ToDecimal(dr["NetAmount"]);
                            SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["GrossAmount"] ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
                            SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["ItemCount"]);

                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSaleOrderInvoicePartsDealerCustomerAndMaterialWise(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };

                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoicePartsDealerCustomerAndMaterialWise", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerCode"]) };
                            SOI.HeaderCount = Convert.ToInt64(dr["HeaderCount"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                            };
                            SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                            SOI.InvoiceItem.NetAmount = DBNull.Value == dr["NetAmount"] ? 0 : Convert.ToDecimal(dr["NetAmount"]);
                            SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["GrossAmount"] ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
                            SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["ItemCount"]);

                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }

        public List<PDMS_SalesInvoice> GetSaleOrderInvoiceMcReport(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };


                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceMcReport", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            SOI.GSTNo = Convert.ToString(dr["GSTNO"]);
                            SOI.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            SOI.InvoiceDate = DBNull.Value == dr["InvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["InvoiceDate"]);
                            SOI.Status = Convert.ToString(dr["SaleOrderInvoiceStatus"]);
                            SOI.Location = Convert.ToString(dr["Location"]);
                            SOI.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            SOI.ContactNumber = Convert.ToString(dr["ContactNumber"]);

                            SOI.Reference = Convert.ToString(dr["ContactPerson"]);
                            SOI.ReferenceDate = DBNull.Value == dr["ReferenceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["ReferenceDate"]);
                            SOI.McMode = Convert.ToString(dr["McMode"]);
                            SOI.MachineSlNo = Convert.ToString(dr["MachineSlNo"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();

                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                HSN = Convert.ToString(dr["HSNCode"]),
                                MaterialType = Convert.ToString(dr["MaterialType"]),
                                Product = Convert.ToString(dr["Product"]),
                                ProductGroup = Convert.ToString(dr["ProductGroup"])
                            };
                            SOI.InvoiceItem.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["UnitBasicPrice"]));
                            SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["Qty"]));
                            SOI.InvoiceItem.ReturnQty = Convert.ToDecimal("0" + Convert.ToString(dr["ReturnQty"]));
                            SOI.InvoiceItem.Value = SOI.InvoiceItem.UnitBasicPrice * SOI.InvoiceItem.Qty;

                            SOI.InvoiceItem.Discount = Convert.ToDecimal(dr["Discount"]);

                            SOI.InvoiceItem.DiscountedPrice = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;

                            SOI.InvoiceItem.FreightAmount = DBNull.Value == dr["FreightAmount"] ? 0 : Convert.ToDecimal(dr["FreightAmount"]);

                            SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Discount + SOI.InvoiceItem.Value + SOI.InvoiceItem.FreightAmount;
                            SOI.InvoiceItem.SGST = DBNull.Value == dr["SGST"] ? (decimal?)null : Convert.ToDecimal(dr["SGST"]);
                            SOI.InvoiceItem.SGSTAmt = Convert.ToDecimal(dr["SGSTAmt"]);
                            SOI.InvoiceItem.CGST = DBNull.Value == dr["CGST"] ? (decimal?)null : Convert.ToDecimal(dr["CGST"]);
                            SOI.InvoiceItem.CGSTAmt = Convert.ToDecimal(dr["CGSTAmt"]);

                            SOI.InvoiceItem.IGST = DBNull.Value == dr["IGST"] ? (decimal?)null : Convert.ToDecimal(dr["IGST"]);
                            SOI.InvoiceItem.IGSTAmt = Convert.ToDecimal(dr["IGSTAmt"]);
                            SOI.InvoiceItem.Tax = SOI.InvoiceItem.SGSTAmt + SOI.InvoiceItem.CGSTAmt + SOI.InvoiceItem.IGSTAmt;
                            SOI.InvoiceItem.TaxP = (SOI.InvoiceItem.SGST == null ? 0 : (decimal)SOI.InvoiceItem.SGST)
                               + (SOI.InvoiceItem.CGST == null ? 0 : (decimal)SOI.InvoiceItem.CGST)
                               + (SOI.InvoiceItem.IGST == null ? 0 : (decimal)SOI.InvoiceItem.IGST);
                            SOI.InvoiceItem.TotalAmt = SOI.InvoiceItem.Tax + SOI.InvoiceItem.TaxableAmount;


                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSaleOrderInvoiceMcDealerAndMaterialWise(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };

                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceMcDealerAndMaterialWise", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.HeaderCount = Convert.ToInt64(dr["HeaderCount"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                            };
                            SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                            SOI.InvoiceItem.NetAmount = DBNull.Value == dr["NetAmount"] ? 0 : Convert.ToDecimal(dr["NetAmount"]);
                            SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["GrossAmount"] ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
                            SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["ItemCount"]);

                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSaleOrderInvoiceMcDealerCustomerAndMaterialWise(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };

                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceMcDealerCustomerAndMaterialWise", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerCode"]) };
                            SOI.HeaderCount = Convert.ToInt64(dr["HeaderCount"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                            };
                            SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                            SOI.InvoiceItem.NetAmount = DBNull.Value == dr["NetAmount"] ? 0 : Convert.ToDecimal(dr["NetAmount"]);
                            SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["GrossAmount"] ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
                            SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["ItemCount"]);

                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }


        public List<PDMS_SalesInvoice> GetSaleOrderInvoiceWarrantyReport(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };


                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceWarrantyReport", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            SOI.GSTNo = Convert.ToString(dr["GSTNO"]);
                            SOI.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            SOI.InvoiceDate = DBNull.Value == dr["InvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["InvoiceDate"]);
                            SOI.Status = Convert.ToString(dr["SaleOrderInvoiceStatus"]);
                            SOI.Location = Convert.ToString(dr["Location"]);
                            SOI.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            SOI.ContactNumber = Convert.ToString(dr["ContactNumber"]);
                            SOI.Reference = Convert.ToString(dr["ContactPerson"]);
                            SOI.ReferenceDate = DBNull.Value == dr["ReferenceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["ReferenceDate"]);
                            SOI.McMode = Convert.ToString(dr["McMode"]);
                            SOI.MachineSlNo = Convert.ToString(dr["MachineSlNo"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();

                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                HSN = Convert.ToString(dr["HSNCode"]),
                                MaterialType = Convert.ToString(dr["MaterialType"]),
                                Product = Convert.ToString(dr["Product"]),
                                ProductGroup = Convert.ToString(dr["ProductGroup"])
                            };
                            SOI.InvoiceItem.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["UnitBasicPrice"]));
                            SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["Qty"]));
                            SOI.InvoiceItem.ReturnQty = Convert.ToDecimal("0" + Convert.ToString(dr["ReturnQty"]));
                            SOI.InvoiceItem.Value = SOI.InvoiceItem.UnitBasicPrice * SOI.InvoiceItem.Qty;

                            SOI.InvoiceItem.Discount = Convert.ToDecimal(dr["Discount"]);

                            SOI.InvoiceItem.DiscountedPrice = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;

                            SOI.InvoiceItem.FreightAmount = DBNull.Value == dr["FreightAmount"] ? 0 : Convert.ToDecimal(dr["FreightAmount"]);

                            SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Discount + SOI.InvoiceItem.Value + SOI.InvoiceItem.FreightAmount;
                            SOI.InvoiceItem.SGST = DBNull.Value == dr["SGST"] ? (decimal?)null : Convert.ToDecimal(dr["SGST"]);
                            SOI.InvoiceItem.SGSTAmt = Convert.ToDecimal(dr["SGSTAmt"]);
                            SOI.InvoiceItem.CGST = DBNull.Value == dr["CGST"] ? (decimal?)null : Convert.ToDecimal(dr["CGST"]);
                            SOI.InvoiceItem.CGSTAmt = Convert.ToDecimal(dr["CGSTAmt"]);

                            SOI.InvoiceItem.IGST = DBNull.Value == dr["IGST"] ? (decimal?)null : Convert.ToDecimal(dr["IGST"]);
                            SOI.InvoiceItem.IGSTAmt = Convert.ToDecimal(dr["IGSTAmt"]);
                            SOI.InvoiceItem.Tax = SOI.InvoiceItem.SGSTAmt + SOI.InvoiceItem.CGSTAmt + SOI.InvoiceItem.IGSTAmt;
                            SOI.InvoiceItem.TaxP = (SOI.InvoiceItem.SGST == null ? 0 : (decimal)SOI.InvoiceItem.SGST)
                               + (SOI.InvoiceItem.CGST == null ? 0 : (decimal)SOI.InvoiceItem.CGST)
                               + (SOI.InvoiceItem.IGST == null ? 0 : (decimal)SOI.InvoiceItem.IGST);
                            SOI.InvoiceItem.TotalAmt = SOI.InvoiceItem.Tax + SOI.InvoiceItem.TaxableAmount;


                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSaleOrderInvoiceWarrantyDealerAndMaterialWise(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };

                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceWarrantyDealerAndMaterialWise", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.HeaderCount = Convert.ToInt64(dr["HeaderCount"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                            };
                            SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                            SOI.InvoiceItem.NetAmount = DBNull.Value == dr["NetAmount"] ? 0 : Convert.ToDecimal(dr["NetAmount"]);
                            SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["GrossAmount"] ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
                            SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["ItemCount"]);

                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSaleOrderInvoiceWarrantyDealerCustomerAndMaterialWise(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderWarrantyStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, MaterialCodeP };

                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoicePartsDealerCustomerAndMaterialWise", Params))
                {
                    if (DataSet != null)
                    {
                        int i = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            SOI = new PDMS_SalesInvoice();
                            i = i + 1;
                            SOI.InvoiceID = i;
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerCode"]) };
                            SOI.HeaderCount = Convert.ToInt64(dr["HeaderCount"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                            };
                            SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                            SOI.InvoiceItem.NetAmount = DBNull.Value == dr["NetAmount"] ? 0 : Convert.ToDecimal(dr["NetAmount"]);
                            SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["GrossAmount"] ? 0 : Convert.ToDecimal(dr["GrossAmount"]);
                            SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["ItemCount"]);

                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }

        public List<PDMS_SalesInvoice> GetSalesInvoiceDetails(int? DealerID, string CustomerCode, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, String SaleOrderInvoiceStatusID, string SalesTypeID)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter SaleOrderInvoiceStatusIDP = provider.CreateParameter("SaleOrderInvoiceStatusID", string.IsNullOrEmpty(SaleOrderInvoiceStatusID) ? null : SaleOrderInvoiceStatusID, DbType.String);
                DbParameter SalesTypeIDP = provider.CreateParameter("SalesTypeID", string.IsNullOrEmpty(SalesTypeID) ? null : SalesTypeID, DbType.String);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, SaleOrderInvoiceStatusIDP, SalesTypeIDP };


                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                using (DataSet DataSet = provider.Select("ZDMS_GetSaleOrderInvoiceReport", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            SOI = new PDMS_SalesInvoice();
                            SOI.InvoiceID = Convert.ToInt64(dr["SaleOrderInvoiceID"]);
                            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            SOI.GSTNo = Convert.ToString(dr["GSTNO"]);
                            SOI.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            SOI.InvoiceDate = DBNull.Value == dr["InvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["InvoiceDate"]);
                            SOI.Status = Convert.ToString(dr["SaleOrderInvoiceStatus"]);
                            SOI.Location = Convert.ToString(dr["Location"]);
                            SOI.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            SOI.ContactNumber = Convert.ToString(dr["ContactNumber"]);
                            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();

                            SOI.InvoiceItem.Material = new PDMS_Material()
                            {
                                MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                HSN = Convert.ToString(dr["HSNCode"]),
                                MaterialType = Convert.ToString(dr["MaterialType"]),
                                Product = Convert.ToString(dr["Product"]),
                                ProductGroup = Convert.ToString(dr["ProductGroup"])
                            };
                            SOI.InvoiceItem.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["UnitBasicPrice"]));
                            SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["Qty"]));
                            SOI.InvoiceItem.Value = SOI.InvoiceItem.UnitBasicPrice * SOI.InvoiceItem.Qty;

                            SOI.InvoiceItem.Discount = Convert.ToDecimal(dr["Discount"]);

                            SOI.InvoiceItem.DiscountedPrice = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;

                            SOI.InvoiceItem.FreightAmount = DBNull.Value == dr["FreightAmount"] ? 0 : Convert.ToDecimal(dr["FreightAmount"]);

                            SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Discount + SOI.InvoiceItem.Value + SOI.InvoiceItem.FreightAmount;
                            SOI.InvoiceItem.SGST = DBNull.Value == dr["SGST"] ? (decimal?)null : Convert.ToDecimal(dr["SGST"]);
                            SOI.InvoiceItem.SGSTAmt = Convert.ToDecimal(dr["SGSTAmt"]);
                            SOI.InvoiceItem.CGST = DBNull.Value == dr["CGST"] ? (decimal?)null : Convert.ToDecimal(dr["CGST"]);
                            SOI.InvoiceItem.CGSTAmt = Convert.ToDecimal(dr["CGSTAmt"]);

                            SOI.InvoiceItem.IGST = DBNull.Value == dr["IGST"] ? (decimal?)null : Convert.ToDecimal(dr["IGST"]);
                            SOI.InvoiceItem.IGSTAmt = Convert.ToDecimal(dr["IGSTAmt"]);
                            SOI.InvoiceItem.Tax = SOI.InvoiceItem.SGSTAmt + SOI.InvoiceItem.CGSTAmt + SOI.InvoiceItem.IGSTAmt;
                            SOI.InvoiceItem.TotalAmt = SOI.InvoiceItem.Tax + SOI.InvoiceItem.TaxableAmount;


                            SOIs.Add(SOI);
                        }
                    }
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }

        //public List<PDMS_SalesOrder> GetSalesOrderOld(string filter)
        //{
        //    TraceLogger.Log(DateTime.Now);
        //    List<PDMS_SalesOrder> SOIs = new List<PDMS_SalesOrder>();
        //    try
        //    {

        //        string query = "SELECT  * from pr_getsalesorderBasedOnInvoice(" + filter + ")";

        //        DataTable dt = new NpgsqlServer().ExecuteReader(query);
        //        PDMS_SalesOrder SOI = new PDMS_SalesOrder();
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            SOI = new PDMS_SalesOrder();
        //            SOI.Customer = Convert.ToString(dr["p_bp_id"]);
        //            SOI.CustomerName = Convert.ToString(dr["r_org_name"]);
        //            SOI.InvoiceNumber = Convert.ToString(dr["p_inv_id"]);
        //            SOI.InvoiceDate = DBNull.Value == dr["r_inv_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_inv_date"]);
        //            SOI.PartNumber = Convert.ToString(dr["f_material_id"]);
        //            SOI.Description = Convert.ToString(dr["r_description"]);
        //            SOI.MatType = Convert.ToString(dr["f_Mat_Type"]);
        //            SOI.Division = Convert.ToString(dr["f_Division"]);
        //            SOI.Qty = DBNull.Value == dr["r_qty"] ? 0 : Convert.ToDecimal(dr["r_qty"]);

        //            SOI.Basic = DBNull.Value == dr["ZPRP"] ? 0 : Convert.ToDecimal(dr["ZPRP"]);

        //            SOI.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
        //            SOI.BasicAfterDisc = SOI.Basic - SOI.Discount;
        //            SOI.Tax = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
        //            SOI.FreightInsurance = DBNull.Value == dr["ZFRH"] ? 0 : Convert.ToDecimal(dr["ZFRH"]);// + (DBNull.Value == dr["ZINS"] ? 0 : Convert.ToDecimal(dr["ZINS"]));
        //            SOI.TotalAmt = SOI.BasicAfterDisc + SOI.Tax + SOI.FreightInsurance;
        //            SOI.SoNumber = Convert.ToString(dr["so_number"]);
        //            SOI.SoDate = DBNull.Value == dr["so_date"] ? (DateTime?)null : Convert.ToDateTime(dr["so_date"]);
        //            SOI.SoQty = DBNull.Value == dr["so_qty"] ? 0 : Convert.ToDecimal(dr["so_qty"]);
        //            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["Dealer_Code"]) };
        //            SOIs.Add(SOI);
        //        }
        //        return SOIs;
        //        TraceLogger.Log(DateTime.Now);
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrder", ex);
        //        throw ex;
        //    }
        //    return SOIs;
        //}
        public List<PDMS_SalesOrderItems> GetSalesOrderItems(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesOrderItems> SOIs = new List<PDMS_SalesOrderItems>();
            try
            {
                //  string query   = "SELECT  * from pr_getsalesorderitems(" + filter + ")";

                string query = PQuery.GetSalesOrder + filter;

                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_SalesOrderItems SOI = new PDMS_SalesOrderItems();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_SalesOrderItems();
                    SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["Dealer_Code"]), DealerName = Convert.ToString(dr["Dealer_Name"]) };
                    SOI.Customer = Convert.ToString(dr["p_bp_id"]);
                    SOI.CustomerName = Convert.ToString(dr["r_org_name"]);
                    SOI.GSTNo = Convert.ToString(dr["GSTNO"]);
                    SOI.SONumber = Convert.ToString(dr["p_so_Id"]);
                    SOI.SODate = Convert.ToDateTime(dr["r_order_date"]);
                    SOI.SOStatus = Convert.ToString(dr["So_status"]);
                    SOI.InvoiceNumber = Convert.ToString(dr["p_inv_id"]);
                    SOI.InvoiceDate = DBNull.Value == dr["r_inv_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_inv_date"]);

                    SOI.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]), MaterialDescription = Convert.ToString(dr["r_description"]), HSN = Convert.ToString(dr["r_hsn_id"]) };
                    //SOI.PartNumber = Convert.ToString(dr["f_material_id"]);
                    //SOI.Description = Convert.ToString(dr["r_description"]);

                    //SOI.HSNCode = Convert.ToString(dr["r_hsn_id"]);
                    SOI.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["r_unit_price"]));
                    SOI.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["r_order_qty"]));
                    // SOI.Value = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);
                    SOI.Value = SOI.UnitBasicPrice * SOI.Qty;

                    SOI.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);

                    SOI.DiscountedPrice = SOI.Value + SOI.Discount;

                    SOI.FreightAmount = DBNull.Value == dr["freight"] ? 0 : Convert.ToDecimal(dr["freight"]);

                    SOI.TaxableAmount = SOI.Discount + SOI.Value + SOI.FreightAmount;
                    SOI.SGST = DBNull.Value == dr["SGST"] ? (decimal?)null : Convert.ToDecimal(dr["SGST"]);
                    SOI.SGSTAmt = DBNull.Value == dr["SGSTAmt"] ? 0 : Convert.ToDecimal(dr["SGSTAmt"]);
                    SOI.CGST = DBNull.Value == dr["CGST"] ? (decimal?)null : Convert.ToDecimal(dr["CGST"]);
                    SOI.CGSTAmt = DBNull.Value == dr["CGSTAmt"] ? 0 : Convert.ToDecimal(dr["CGSTAmt"]);

                    SOI.IGST = DBNull.Value == dr["IGST"] ? (decimal?)null : Convert.ToDecimal(dr["IGST"]);
                    SOI.IGSTAmt = DBNull.Value == dr["IGSTAmt"] ? 0 : Convert.ToDecimal(dr["IGSTAmt"]);
                    SOI.Tax = SOI.SGSTAmt + SOI.CGSTAmt + SOI.IGSTAmt;
                    SOI.TotalAmt = SOI.Tax + SOI.TaxableAmount;
                    SOI.MatType = Convert.ToString(dr["f_Mat_Type"]);
                    SOI.Division = Convert.ToString(dr["f_Division"]);
                    SOI.Location = Convert.ToString(dr["f_location"]);
                    SOI.ContactPerson = Convert.ToString(dr["r_contact_prsn"]);
                    SOI.ContactNumber = Convert.ToString(dr["r_contact_no"]);
                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesOrder1> GetSalesOrderPerfomance(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesOrder1> SOIs = new List<PDMS_SalesOrder1>();
            try
            {
                string query = "select  Qt.s_tenant_id as Dealer_Code  ,t.description  as Dealer_Name ,Qt.f_location  as location  ,Qt.s_status as  Quotation_Status  ,so.s_status as  Sales_Order_Status"
 + "  ,bp.p_bp_id as Customer_Code  ,bp.r_org_name as Customer_Name ,Qt.r_contact_prsn as contact_person ,Qt.r_contact_no  as contact_No ,Qti.f_material_id as Material_Code"
 + " ,mm.r_description  as Material_Description ,mm.r_hsn_id as hsn , Qt.p_so_Id as Quotation_Number ,Qt.r_order_date as Quotation_Date ,Qti.r_order_qty as Quotation_Quantity , so.p_so_Id as Sales_Order_Number"
 + " ,so.r_order_date as Sales_Order_Date ,soi.r_order_qty as Sales_Order_Quantity ,d.p_del_id as Delivery_Number ,d.r_del_date as Delivery_Date ,di.r_pick_qty as Delivery_Quantity ,inv.p_inv_id as Invoice_Number"
 + " ,inv.r_inv_date as Invoice_Date , invi.r_qty as Invoice_Quantity ,soi.r_unit_price as Unit_Price,  soi.r_gross_amt as Gross_Amount, soi.r_discount_amt as discount_Amount "
+ " from dssor_sales_order_hdr Qt "
+ " inner join dssor_sales_order_item Qti on Qti.p_so_Id = Qt.p_so_Id   "
+ " inner join m_tenant t on t.tenantid = Qt.s_tenant_id"
+ "  inner join doohr_bp bp on bp.p_bp_id = Qt.f_customer_id  and bp.s_tenant_id <> 20   and bp.s_tenant_id = Qt.s_tenant_id"

+ "  left join dssor_sales_order_hdr so on so.r_ext_id = Qt.p_so_Id"
+ "  left join dssor_sales_order_item soi on so.p_so_Id = soi.p_so_Id and  soi.f_material_id =  Qti.f_material_id  "

 + " left join dsder_delv_item di on di.f_so_id = soi.p_so_Id  and di.f_material_id = soi.f_material_id  "
 + " left join dsder_delv_hdr d on d.p_del_id = di.p_del_id"

 + " left Join dsinr_inv_hdr inv on inv.f_so_id = so.p_so_Id "
 + " left Join dsinr_inv_item invi on invi.k_id = inv.p_id and invi.f_material_id  = di.f_material_id"
 + " left join af_m_materials mm on mm.p_material = Qti.f_material_id where Qt.s_object_type = 208 " + filter + " order by inv.f_customer_id, so.p_so_Id desc";

                //  string query ="SELECT  * from pr_get_sales_order_perfomance(" + filter + ")";
                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_SalesOrder1 SO = new PDMS_SalesOrder1();
                PDMS_SalesOrderItems1 SOi = null;
                foreach (DataRow dr in dt.Rows)
                {

                    SO = new PDMS_SalesOrder1();
                    SOIs.Add(SO);
                    SO.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["Dealer_Code"]), DealerName = Convert.ToString(dr["Dealer_Name"]) };
                    SO.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["Customer_Code"]), CustomerName = Convert.ToString(dr["Customer_Name"]) };
                    SO.Location = Convert.ToString(dr["Location"]);
                    SO.ContactPerson = Convert.ToString(dr["Contact_Person"]);
                    SO.ContactNumber = Convert.ToString(dr["Contact_Number"]);


                    SO.QuotationNumber = Convert.ToString(dr["Quotation_Number"]);
                    SO.QuotationDate = Convert.ToDateTime(dr["Quotation_Date"]);
                    SO.QuotationStatus = Convert.ToString(dr["Quotation_Status"]);

                    SO.SalesOrderNumber = Convert.ToString(dr["Sales_Order_Number"]);
                    SO.SalesOrderDate = DBNull.Value == dr["Sales_Order_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Sales_Order_Date"]);
                    SO.SalesOrderStatus = Convert.ToString(dr["Sales_Order_Status"]);

                    SO.DeliveryNumber = Convert.ToString(dr["Delivery_Number"]);
                    SO.DeliveryDate = DBNull.Value == dr["Delivery_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Delivery_Date"]);

                    SO.InvoiceNumber = Convert.ToString(dr["Invoice_Number"]);
                    SO.InvoiceDate = DBNull.Value == dr["Invoice_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Invoice_Date"]);

                    SO.DeliveryMinusSalesOrderDays = DBNull.Value == dr["Delivery_Date"] ? (int?)null : (int)(((DateTime)SO.DeliveryDate - (DateTime)SO.SalesOrderDate).TotalDays);
                    SO.InvoiceMinusSalesOrderDays = DBNull.Value == dr["Invoice_Date"] ? (int?)null : (int)(((DateTime)SO.InvoiceDate - (DateTime)SO.SalesOrderDate).TotalDays);

                    SO.SalesOrderItems = new PDMS_SalesOrderItems1();

                    SOi = new PDMS_SalesOrderItems1();
                    SO.SalesOrderItems = SOi;

                    SOi.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["Material_Code"]),
                        MaterialDescription = Convert.ToString(dr["Material_Description"]),
                        HSN = Convert.ToString(dr["hsn"]),
                        //  MaterialType =
                    };


                    SOi.QuotationQuantity = DBNull.Value == dr["Quotation_Quantity"] ? 0 : Convert.ToDecimal(dr["Quotation_Quantity"]);
                    SOi.SalesOrderQuantity = DBNull.Value == dr["Sales_Order_Quantity"] ? 0 : Convert.ToDecimal(dr["Sales_Order_Quantity"]);
                    SOi.DeliveryQuantity = DBNull.Value == dr["Delivery_Quantity"] ? 0 : Convert.ToDecimal(dr["Delivery_Quantity"]);
                    SOi.InvoiceQuantity = DBNull.Value == dr["Invoice_Quantity"] ? 0 : Convert.ToDecimal(dr["Invoice_Quantity"]);

                    SOi.QuotationMinusInvoiceQuantity = SOi.QuotationQuantity - SOi.InvoiceQuantity;
                    SOi.SalesOrderMinusDeliveryQuantity = SOi.SalesOrderQuantity - SOi.DeliveryQuantity;
                    SOi.SalesOrderMinusInvoiceQuantity = SOi.SalesOrderQuantity - SOi.InvoiceQuantity;



                    //public decimal? DeliveryMinusSalesOrderDays { get; set; }
                    //public decimal? InvoiceMinusSalesOrderDays { get; set; }


                    // SOI.Basic = DBNull.Value == dr["ZPRP"] ? 0 : Convert.ToDecimal(dr["ZPRP"]);

                    //  SOI.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    //  SOI.BasicAfterDisc = SOI.Basic - SOI.Discount;
                    //   SOI.Tax = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    //  SOI.FreightInsurance = DBNull.Value == dr["ZFRH"] ? 0 : Convert.ToDecimal(dr["ZFRH"]);// + (DBNull.Value == dr["ZINS"] ? 0 : Convert.ToDecimal(dr["ZINS"]));
                    //   SOI.TotalAmt = SOI.BasicAfterDisc + SOI.Tax + SOI.FreightInsurance;

                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderPerfomance", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesOrder1> GetSalesOrderPerfomanceParts(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesOrder1> SOIs = new List<PDMS_SalesOrder1>();
            try
            {

                string query = " select so.s_tenant_id as Dealer_Code,t.description  as Dealer_Name,so.f_location  as location,so.s_status as  Sales_Order_Status"
  + " ,bp.p_bp_id as Customer_Code,bp.r_org_name as Customer_Name,so.r_contact_prsn as contact_person,so.r_contact_no  as Contact_Number,soi.f_material_id as Material_Code"
  + " ,mm.r_description  as Material_Description,mm.r_hsn_id as hsn, so.p_so_Id as Sales_Order_Number"
 + " ,so.r_order_date as Sales_Order_Date,soi.r_order_qty as Sales_Order_Quantity,d.p_del_id as Delivery_Number,d.r_del_date as Delivery_Date,di.r_pick_qty as Delivery_Quantity,inv.p_inv_id as Invoice_Number"
+ " ,inv.r_inv_date as Invoice_Date, invi.r_qty as Invoice_Quantity,soi.r_unit_price as Unit_Price,soi.r_gross_amt as Gross_Amount,soi.r_discount_amt as discount_Amount"

+ " from   dssor_sales_order_hdr so "
+ "  inner join dssor_sales_order_item soi on so.p_so_Id = soi.p_so_Id  and so.s_object_type =101 and so.f_division = 'SP'   and so.r_ext_id is null "
+ " inner join m_tenant t on t.tenantid = so.s_tenant_id "
+ " inner join doohr_bp bp on bp.p_bp_id = so.f_customer_id  and bp.s_tenant_id <> 20   and bp.s_tenant_id = so.s_tenant_id "
+ " left join dsder_delv_item di on di.f_so_id = soi.p_so_Id and di.f_material_id = soi.f_material_id   "
+ " left join dsder_delv_hdr d on d.p_del_id = di.p_del_id "
+ " left Join dsinr_inv_hdr inv on inv.f_so_id = so.p_so_Id  "
+ " left Join dsinr_inv_item invi on invi.k_id = inv.p_id and invi.f_material_id  = di.f_material_id   "
+ " left join af_m_materials mm on mm.p_material = soi.f_material_id  where 1 = 1  " + filter;

                //+ " from dssor_sales_order_hdr Qt"
                //+ " inner join dssor_sales_order_item Qti on Qti.p_so_Id = Qt.p_so_Id   "
                //+ " inner join m_tenant t on t.tenantid = Qt.s_tenant_id "
                //+ " inner join doohr_bp bp on bp.p_bp_id = Qt.f_customer_id  and bp.s_tenant_id <> 20   and bp.s_tenant_id = Qt.s_tenant_id "
                //+ " left join dssor_sales_order_hdr so on so.r_ext_id = Qt.p_so_Id "
                //+ " left join dssor_sales_order_item soi on so.p_so_Id = soi.p_so_Id and  soi.f_material_id =  Qti.f_material_id  "
                //+ " left join dsder_delv_item di on di.f_so_id = soi.p_so_Id  and di.f_material_id = soi.f_material_id   "
                //+ " left join dsder_delv_hdr d on d.p_del_id = di.p_del_id "
                //+ " left Join dsinr_inv_hdr inv on inv.f_so_id = so.p_so_Id  "
                //+ " left Join dsinr_inv_item invi on invi.k_id = inv.p_id and invi.f_material_id  = di.f_material_id    "
                //+ " left join af_m_materials mm on mm.p_material = Qti.f_material_id where Qt.s_object_type = 208  " + filter;

                //   string query = "SELECT  * from pr_get_sales_order_perfomance_parts(" + filter + ")";

                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_SalesOrder1 SO = new PDMS_SalesOrder1();
                PDMS_SalesOrderItems1 SOi = null;
                foreach (DataRow dr in dt.Rows)
                {

                    SO = new PDMS_SalesOrder1();
                    SOIs.Add(SO);
                    SO.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["Dealer_Code"]), DealerName = Convert.ToString(dr["Dealer_Name"]) };
                    SO.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["Customer_Code"]), CustomerName = Convert.ToString(dr["Customer_Name"]) };
                    SO.Location = Convert.ToString(dr["Location"]);
                    SO.ContactPerson = Convert.ToString(dr["Contact_Person"]);
                    SO.ContactNumber = Convert.ToString(dr["Contact_Number"]);


                    //  SO.QuotationNumber = Convert.ToString(dr["Quotation_Number"]);
                    //  SO.QuotationDate = Convert.ToDateTime(dr["Quotation_Date"]);
                    //  SO.QuotationStatus = Convert.ToString(dr["Quotation_Status"]);

                    SO.SalesOrderNumber = Convert.ToString(dr["Sales_Order_Number"]);
                    SO.SalesOrderDate = DBNull.Value == dr["Sales_Order_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Sales_Order_Date"]);
                    SO.SalesOrderStatus = Convert.ToString(dr["Sales_Order_Status"]);

                    SO.DeliveryNumber = Convert.ToString(dr["Delivery_Number"]);
                    SO.DeliveryDate = DBNull.Value == dr["Delivery_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Delivery_Date"]);

                    SO.InvoiceNumber = Convert.ToString(dr["Invoice_Number"]);
                    SO.InvoiceDate = DBNull.Value == dr["Invoice_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Invoice_Date"]);

                    SO.DeliveryMinusSalesOrderDays = DBNull.Value == dr["Delivery_Date"] ? (int?)null : (int)(((DateTime)SO.DeliveryDate - (DateTime)SO.SalesOrderDate).TotalDays);
                    SO.InvoiceMinusSalesOrderDays = DBNull.Value == dr["Invoice_Date"] ? (int?)null : (int)(((DateTime)SO.InvoiceDate - (DateTime)SO.SalesOrderDate).TotalDays);

                    SO.SalesOrderItems = new PDMS_SalesOrderItems1();

                    SOi = new PDMS_SalesOrderItems1();
                    SO.SalesOrderItems = SOi;

                    SOi.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["Material_Code"]),
                        MaterialDescription = Convert.ToString(dr["Material_Description"]),
                        HSN = Convert.ToString(dr["hsn"]),
                        //  MaterialType =
                    };


                    //  SOi.QuotationQuantity = DBNull.Value == dr["Quotation_Quantity"] ? 0 : Convert.ToDecimal(dr["Quotation_Quantity"]);
                    SOi.SalesOrderQuantity = DBNull.Value == dr["Sales_Order_Quantity"] ? 0 : Convert.ToDecimal(dr["Sales_Order_Quantity"]);
                    SOi.DeliveryQuantity = DBNull.Value == dr["Delivery_Quantity"] ? 0 : Convert.ToDecimal(dr["Delivery_Quantity"]);
                    SOi.InvoiceQuantity = DBNull.Value == dr["Invoice_Quantity"] ? 0 : Convert.ToDecimal(dr["Invoice_Quantity"]);

                    //    SOi.QuotationMinusInvoiceQuantity = SOi.QuotationQuantity - SOi.InvoiceQuantity;
                    SOi.SalesOrderMinusDeliveryQuantity = SOi.SalesOrderQuantity - SOi.DeliveryQuantity;
                    SOi.SalesOrderMinusInvoiceQuantity = SOi.SalesOrderQuantity - SOi.InvoiceQuantity;



                    //public decimal? DeliveryMinusSalesOrderDays { get; set; }
                    //public decimal? InvoiceMinusSalesOrderDays { get; set; }


                    // SOI.Basic = DBNull.Value == dr["ZPRP"] ? 0 : Convert.ToDecimal(dr["ZPRP"]);

                    //  SOI.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);
                    //  SOI.BasicAfterDisc = SOI.Basic - SOI.Discount;
                    //   SOI.Tax = DBNull.Value == dr["r_tax_amt"] ? 0 : Convert.ToDecimal(dr["r_tax_amt"]);
                    //  SOI.FreightInsurance = DBNull.Value == dr["ZFRH"] ? 0 : Convert.ToDecimal(dr["ZFRH"]);// + (DBNull.Value == dr["ZINS"] ? 0 : Convert.ToDecimal(dr["ZINS"]));
                    //   SOI.TotalAmt = SOI.BasicAfterDisc + SOI.Tax + SOI.FreightInsurance;

                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderPerfomance", ex);
                throw ex;
            }
            return SOIs;
        }

        //public List<PDMS_SalesInvoice> GetSalesInvoiceDetails(string filter, string DealerCode)
        //{
        //    TraceLogger.Log(DateTime.Now);
        //    List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
        //    try
        //    {
        //        //  string query   = "SELECT  * from pr_getsalesorderitems(" + filter + ")";
        //        string query = "";

        //        query = PQuery.GetSalesInvoiceDetails + filter;

        //        DataTable dt = new NpgsqlServer().ExecuteReader(query);
        //        PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
        //        int i = 0;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            if (!string.IsNullOrEmpty(DealerCode))
        //            {
        //                if (DealerCode != Convert.ToString(dr["Dealer_Code"]))
        //                {
        //                    continue;
        //                }
        //            }
        //            SOI = new PDMS_SalesInvoice();
        //            i = i + 1;
        //            SOI.InvoiceID = i;
        //            SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["Dealer_Code"]), DealerName = Convert.ToString(dr["Dealer_Name"]) };
        //            SOI.Dealer.DealerCode = SOI.Dealer.DealerCode == "23" ? "9004" : SOI.Dealer.DealerCode;
        //            SOI.Customer = Convert.ToString(dr["f_customer_id"]);
        //            SOI.CustomerName = Convert.ToString(dr["d_customer_name"]);
        //            SOI.GSTNo = Convert.ToString(dr["GSTNO"]);
        //            SOI.InvoiceNumber = Convert.ToString(dr["p_inv_id"]);
        //            SOI.InvoiceDate = DBNull.Value == dr["r_inv_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_inv_date"]);
        //            SOI.Status = Convert.ToString(dr["s_status"]);
        //            SOI.Division = Convert.ToString(dr["f_Division"]);
        //            SOI.Location = Convert.ToString(dr["f_location"]);
        //            SOI.ContactPerson = Convert.ToString(dr["r_contact_prsn"]);
        //            SOI.ContactNumber = Convert.ToString(dr["r_contact_no"]);
        //            SOI.InvoiceItem = new PDMS_SalesInvoiceItem();

        //            SOI.InvoiceItem.Material = new PDMS_Material()
        //            {
        //                MaterialCode = Convert.ToString(dr["f_material_id"]),
        //                MaterialDescription = Convert.ToString(dr["r_description"])
        //                ,
        //                HSN = Convert.ToString(dr["r_hsn_id"]),
        //                MaterialType = Convert.ToString(dr["f_Mat_Type"])
        //            };
        //            SOI.InvoiceItem.UnitBasicPrice = Convert.ToDecimal("0" + Convert.ToString(dr["r_unit_price"]));
        //            SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["r_qty"]));
        //            // SOI.Value = DBNull.Value == dr["r_gross_amt"] ? 0 : Convert.ToDecimal(dr["r_gross_amt"]);
        //            SOI.InvoiceItem.Value = SOI.InvoiceItem.UnitBasicPrice * SOI.InvoiceItem.Qty;

        //            SOI.InvoiceItem.Discount = DBNull.Value == dr["r_discount_amt"] ? 0 : Convert.ToDecimal(dr["r_discount_amt"]);

        //            SOI.InvoiceItem.DiscountedPrice = SOI.InvoiceItem.Value + SOI.InvoiceItem.Discount;

        //            SOI.InvoiceItem.FreightAmount = DBNull.Value == dr["freight"] ? 0 : Convert.ToDecimal(dr["freight"]);

        //            SOI.InvoiceItem.TaxableAmount = SOI.InvoiceItem.Discount + SOI.InvoiceItem.Value + SOI.InvoiceItem.FreightAmount;
        //            SOI.InvoiceItem.SGST = DBNull.Value == dr["SGST"] ? (decimal?)null : Convert.ToDecimal(dr["SGST"]);
        //            SOI.InvoiceItem.SGSTAmt = DBNull.Value == dr["SGSTAmt"] ? 0 : Convert.ToDecimal(dr["SGSTAmt"]);
        //            SOI.InvoiceItem.CGST = DBNull.Value == dr["CGST"] ? (decimal?)null : Convert.ToDecimal(dr["CGST"]);
        //            SOI.InvoiceItem.CGSTAmt = DBNull.Value == dr["CGSTAmt"] ? 0 : Convert.ToDecimal(dr["CGSTAmt"]);

        //            SOI.InvoiceItem.IGST = DBNull.Value == dr["IGST"] ? (decimal?)null : Convert.ToDecimal(dr["IGST"]);
        //            SOI.InvoiceItem.IGSTAmt = DBNull.Value == dr["IGSTAmt"] ? 0 : Convert.ToDecimal(dr["IGSTAmt"]);
        //            SOI.InvoiceItem.Tax = SOI.InvoiceItem.SGSTAmt + SOI.InvoiceItem.CGSTAmt + SOI.InvoiceItem.IGSTAmt;
        //            SOI.InvoiceItem.TotalAmt = SOI.InvoiceItem.Tax + SOI.InvoiceItem.TaxableAmount;


        //            SOIs.Add(SOI);
        //        }
        //        return SOIs;
        //        TraceLogger.Log(DateTime.Now);
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
        //        throw ex;
        //    }
        //    return SOIs;
        //}

        public List<PDMS_SalesInvoice> GetSalesInvoiceDealerAndMaterialWise(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                //  string query   = "SELECT  * from pr_getsalesorderitems(" + filter + ")";

                string query = PQuery.GetSalesInvoiceDealerAndMaterialWise.Replace("@@Filter", filter);

                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_SalesInvoice();
                    i = i + 1;
                    SOI.InvoiceID = i;
                    SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["Dealer"]), DealerName = Convert.ToString(dr["Dealer_Name"]) };
                    SOI.HeaderCount = Convert.ToInt64(dr["Header_count"]);
                    SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                    SOI.InvoiceItem.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["material"]),
                        MaterialDescription = Convert.ToString(dr["material_description"])
                    };
                    SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["qty"]));
                    SOI.InvoiceItem.NetAmount = DBNull.Value == dr["net_amt"] ? 0 : Convert.ToDecimal(dr["net_amt"]);
                    SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["gross_amt"] ? 0 : Convert.ToDecimal(dr["gross_amt"]);
                    SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["item_count"]);

                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_SalesInvoice> GetSalesInvoiceDealerCustomerAndMaterialWise(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_SalesInvoice> SOIs = new List<PDMS_SalesInvoice>();
            try
            {
                //  string query   = "SELECT  * from pr_getsalesorderitems(" + filter + ")";

                string query = PQuery.GetSalesInvoiceDealerCustomerAndMaterialWise.Replace("@@Filter", filter);

                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_SalesInvoice SOI = new PDMS_SalesInvoice();
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {

                    SOI = new PDMS_SalesInvoice();
                    i = i + 1;
                    SOI.InvoiceID = i;
                    SOI.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };

                    SOI.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                    SOI.HeaderCount = Convert.ToInt64(dr["Header_count"]);
                    SOI.InvoiceItem = new PDMS_SalesInvoiceItem();
                    SOI.InvoiceItem.Material = new PDMS_Material()
                    {
                        MaterialCode = Convert.ToString(dr["material"]),
                        MaterialDescription = Convert.ToString(dr["material_description"])
                    };
                    SOI.InvoiceItem.Qty = Convert.ToDecimal("0" + Convert.ToString(dr["qty"]));
                    SOI.InvoiceItem.NetAmount = DBNull.Value == dr["net_amt"] ? 0 : Convert.ToDecimal(dr["net_amt"]);
                    SOI.InvoiceItem.GrossAmount = DBNull.Value == dr["gross_amt"] ? 0 : Convert.ToDecimal(dr["gross_amt"]);
                    SOI.InvoiceItem.ItemCount = Convert.ToInt64(dr["item_count"]);

                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_SalesOrder", "GetSalesOrderItems", ex);
                throw ex;
            }
            return SOIs;
        }
        public string dssor_sales_order_hdr = " insert into dssor_sales_order_hdr ( s_establishment,p_so_id,s_tenant_id,f_customer_id,f_location,f_currency,f_bill_to,s_modified_by "
                 + ",r_insurance_p,r_discount_amt_additional,s_status,r_tax_amt,s_created_on,r_net_amt,f_ship_to"
                 + ",r_contact_no,r_gross_amt,r_contact_prsn,r_discount_amt,s_created_by,s_modified_on,f_office,f_order_type,s_object_type,r_remarks,r_exp_del_date"
                 + ",r_frieght_p,r_order_date,channel,f_division,r_auto,r_ref_obj_name,r_ref_obj_type,r_price_grp,r_model,r_model_no,s_last_request_index,r_insurance_amt,r_packing_chrgs,r_freight_amt ) values ";
        public string dssor_sales_order_item = "insert into dssor_sales_order_item (s_establishment,p_so_item, p_so_id,s_tenant_id,f_location,s_modified_by,f_uom,r_tax_amt,f_division"
    + ",s_status,f_office,r_exp_del_date,f_material_id,s_last_request_index,r_order_qty,r_add_discount_amt"
    + ",s_created_on,r_net_amt,d_material_desc,r_resvered_qty,r_gross_amt,r_cancel_qty"
    + ",r_shiped_qty,r_discount_amt,s_created_by,r_unit_price,r_pending_qty,s_modified_on"
    + ",s_object_type,r_approved_qty,s_channel) values ";
        public string dssor_sales_order_cond = "insert into dssor_sales_order_cond (s_establishment,p_so_item,p_so_id,s_tenant_id,p_condition_type,f_currency,"
       + "r_cond_amt,r_order_qty,r_pric_date,s_created_by,s_created_on,d_cond_desc,r_cond_cls,f_percentage,channel) values ";
        public string CreateQuotationForMaterial(PDMS_ICTicket ICTicket, PUser User, List<string> Quotation)
        {
            //         s_modified_on  

            string f_bill_to = "";
            string r_insurance_p = "Seller";
            string f_order_type = "101";
            string s_object_type = "101";
            string r_remarks = "";
            string r_frieght_p = "Seller";
            string r_auto = "false";
            string r_ref_obj_name = "";
            string r_ref_obj_type = "null";
            string r_price_grp = "07";
            string r_model = ICTicket.Equipment.EquipmentModel.Model;
            string r_model_no = ICTicket.Equipment.EquipmentSerialNo;

            if (ICTicket.IsWarranty)
            {
                f_bill_to = "B001";
                r_insurance_p = "";
                f_order_type = "108";
                s_object_type = "108";
                r_remarks = "In Warranty Quotation";
                r_frieght_p = "";
                r_auto = "null";
                r_ref_obj_name = "dsprr_psc_hdr";
                r_ref_obj_type = "101";
                r_price_grp = "";
            }


            int success = 0;
            string QuotationNumber = "@@QuotationNumber";
            List<string> querys = new List<string>();
            Boolean HeaderCheck = false;
            string query = "";
            List<PDMS_ServiceMaterial> ServiceMaterials = new BDMS_Service().GetServiceMaterials(ICTicket.ICTicketID, null, null, "", false, "");

            try
            {
                foreach (PDMS_ServiceMaterial ServiceMaterial in ServiceMaterials)
                {
                    //if ((string.IsNullOrEmpty(ServiceMaterial.QuotationNumber)) && (ServiceMaterial.IsCustomerStock == false))
                    if ((string.IsNullOrEmpty(ServiceMaterial.QuotationNumber)) && (ServiceMaterial.MaterialSource.MaterialSourceID != (short)MaterialSource.Customer))
                    {

                        decimal r_tax_amt = ServiceMaterial.IGSTValue + ServiceMaterial.SGSTValue + ServiceMaterial.SGSTValue;
                        decimal r_net_amt = ServiceMaterial.BasePrice + r_tax_amt - ServiceMaterial.Discount;
                        if (HeaderCheck == false)
                        {
                            query = dssor_sales_order_hdr + "(1000,'" + QuotationNumber + "','" + ICTicket.Dealer.DealerCode + "','" + ICTicket.Customer.CustomerCode
               + "','" + ICTicket.DealerOffice.OfficeName_OfficeCode + "','INR','" + f_bill_to + "','" + User.UserName + "','" + r_insurance_p + "',0,'DRAFT'"
               + "," + r_tax_amt + ",now()," + r_net_amt + ",'" + ICTicket.Customer.CustomerCode + "','" + ICTicket.PresentContactNumber.Substring(0, 10) + "'," + ServiceMaterial.BasePrice
               + ",'" + ICTicket.ContactPerson + "'," + ServiceMaterial.Discount + ",'" + User.UserName + "',now(),'" + ICTicket.DealerOffice.OfficeCode + "'," + f_order_type + "," + s_object_type + ",'" + r_remarks + "',now(),'" + r_frieght_p + "',now(),'UI','SP',"
               + r_auto + ",'" + r_ref_obj_name + "'," + r_ref_obj_type + ",'" + r_price_grp + "','" + r_model + "','" + r_model_no + "',0,0,0,0)";

                            querys.Add(query);
                            HeaderCheck = true;
                        }
                        query = dssor_sales_order_item

                            + "(1000," + ServiceMaterial.Item + ", '" + QuotationNumber + "'," + ICTicket.Dealer.DealerCode + ",'" + ICTicket.DealerOffice.OfficeName_OfficeCode + "','" + User.UserName
                            + "','EA'," + r_tax_amt + ",'SP','DRAFT','" + ICTicket.DealerOffice.OfficeCode + "',now(),'" + ServiceMaterial.Material.MaterialCode + "',0," + ServiceMaterial.Qty
                            + ",0,now()," + r_net_amt + ",'" + ServiceMaterial.Material.MaterialDescription + "',0," + ServiceMaterial.BasePrice
            + ",0,0," + ServiceMaterial.Discount + ",'" + User.UserName + "'," + ServiceMaterial.Material.CurrentPrice + "," + ServiceMaterial.Qty + ",now(),101," + ServiceMaterial.Qty + ",'UI')";

                        querys.Add(query);

                        query = dssor_sales_order_cond
        + "(1000," + ServiceMaterial.Item + ",'" + QuotationNumber + "'," + ICTicket.Dealer.DealerCode + ",'ZPRP','INR'," + ServiceMaterial.BasePrice
        + "," + ServiceMaterial.Qty + ",now(),'" + User.UserName + "',now(),'Price-Ajax Parts','B',  null  ,'UI')";
                        querys.Add(query);
                        if (ServiceMaterial.Discount != 0)
                        {
                            query = dssor_sales_order_cond
            + "(1000," + ServiceMaterial.Item + ",'" + QuotationNumber + "'," + ICTicket.Dealer.DealerCode + ",'ZCD2','INR'," + ServiceMaterial.Discount
            + "," + ServiceMaterial.Qty + ",now(),'" + User.UserName + "',now(),'Customer Discount','A'," + ServiceMaterial.Material.TaxPercentage + ",'UI')";
                            querys.Add(query);
                        }
                        if (ServiceMaterial.SGST != 0)
                        {
                            query = dssor_sales_order_cond
            + "(1000," + ServiceMaterial.Item + ",'" + QuotationNumber + "'," + ICTicket.Dealer.DealerCode + ",'ZOCG','INR'," + ServiceMaterial.SGSTValue
            + "," + ServiceMaterial.Qty + ",now(),'" + User.UserName + "',now(),'DLR Central GST OP','D'," + ServiceMaterial.SGST + ",'UI')";
                            querys.Add(query);
                            query = dssor_sales_order_cond
            + "(1000," + ServiceMaterial.Item + ",'" + QuotationNumber + "'," + ICTicket.Dealer.DealerCode + ",'ZOSG','INR'," + ServiceMaterial.SGSTValue
            + "," + ServiceMaterial.Qty + ",now(),'" + User.UserName + "',now(),'DLR Central GST OP','D'," + ServiceMaterial.SGST + ",'UI')";
                            querys.Add(query);
                        }
                        else
                        {
                            query = dssor_sales_order_cond
            + "(1000," + ServiceMaterial.Item + ",'" + QuotationNumber + "'," + ICTicket.Dealer.DealerCode + ",'ZOIG','INR'," + ServiceMaterial.IGSTValue
            + "," + ServiceMaterial.Qty + ",now(),'" + User.UserName + "',now(),'DLR Central GST OP','D'," + ServiceMaterial.IGST + ",'UI')";
                            querys.Add(query);
                        }
                    }

                    //             query = "INSERT INTO public.dppor_purc_order_hdr("
                    //+ " s_establishment, s_tenant_id,                     p_po_id, f_location                                           , f_currency, f_bill_to, s_modified_by          , r_insurance_p, r_tax_amt, s_created_on, f_sold_to, s_status, r_net_amt, r_req_del_date, r_gross_amt, s_created_by           , s_modified_on, f_order_type, f_office                                  , s_object_type, r_exp_del_date, r_remarks          , r_frieght_p,r_order_date,channel, f_division,  r_auto,f_vendor_id,s_channel, s_status_custom)"
                    //+ " VALUES (1000," + ICTicket.Dealer.DealerCode + " , 100100 , '" + ICTicket.DealerOffice.OfficeName_OfficeCode + "', 'INR'     , '100235' , '" + User.UserName + "', 'SELLER'     , r_tax_amt, now()       , '100235' , 'DRAFT' , r_net_amt,  now()        , r_gross_amt, '" + User.UserName + "', now()        , 104         , '" + ICTicket.DealerOffice.OfficeCode + "', 104          , now()         , 'In Warranty Order', 'SELLER'   , now()      , 'UI'  , 'SP'      , true   , '100235'  , 'MI'    , 'DRAFT')";
                    //             querys.Add(query);


                }
                QuotationNumber = "";

                QuotationNumber = new NpgsqlServer().QuotationCreationUpdateTransactions(querys, ICTicket.Dealer.DealerCode, Quotation);

                if (!string.IsNullOrEmpty(QuotationNumber))
                {
                    foreach (PDMS_ServiceMaterial ServiceMaterial in ServiceMaterials)
                    {
                        //  if ((string.IsNullOrEmpty(ServiceMaterial.QuotationNumber)) && (ServiceMaterial.IsCustomerStock == false))
                        if ((string.IsNullOrEmpty(ServiceMaterial.QuotationNumber)) && (ServiceMaterial.MaterialSource.MaterialSourceID != (short)MaterialSource.Customer))
                        {
                            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                            {
                                DbParameter ServiceMaterialIDPP = provider.CreateParameter("ServiceMaterialID", ServiceMaterial.ServiceMaterialID, DbType.Int64);
                                DbParameter QuotationNumberP = provider.CreateParameter("QuotationNumber", QuotationNumber, DbType.String);
                                DbParameter[] Paramss = new DbParameter[2] { ServiceMaterialIDPP, QuotationNumberP };
                                provider.Insert("ZDMS_UpdateICTicketMaterialQuotationNumber", Paramss);
                                scope.Complete();
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateMaterialAddOrRemoveICTicket", sqlEx);
                return "";
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertOrUpdateMaterialAddOrRemoveICTicket", ex);
                return "";
            }
            return QuotationNumber;
        }

        public void CreateQuotationForJSN()
        {
            string folderPath = ConfigurationManager.AppSettings["ICTicketPath"];
            string[] Files = Directory.GetFiles(folderPath, "ZMQT_9*");
            PDMS_SalesOrderJSON SalesOrder = new PDMS_SalesOrderJSON();
            foreach (string file in Files)
            {
                try
                {
                    string json = File.ReadAllText(file).Replace("'", "''");
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    SalesOrder = ser.Deserialize<PDMS_SalesOrderJSON>(json);
                    List<string> Query = new List<string>();
                    string DealerCode = file.Replace(folderPath, "").Substring(6, 4); //ZMQT_9001_20190615043951522

                    DataTable dtLocation = new NpgsqlServer().ExecuteReader("SELECT  p_office, p_location FROM  dmmer_location where r_default = true and   s_tenant_id = " + DealerCode + " limit 1");

                    string Location = "";
                    string Office = "";

                    if (dtLocation.Rows.Count == 1)
                    {
                        Location = Convert.ToString(dtLocation.Rows[0]["p_location"]);
                        Office = Convert.ToString(dtLocation.Rows[0]["p_office"]);
                    }

                    foreach (PDMS_SalesOrderResultsJSON SalesOrderResults in SalesOrder.results)
                    {

                        DataTable QuotationNumberCheck = new NpgsqlServer().ExecuteReader("SELECT count(*) FROM  dssor_sales_order_hdr where r_ext_id = '" + SalesOrderResults.r_ext_id + "' and   s_tenant_id = " + DealerCode + " limit 1");

                        if (Convert.ToInt32(QuotationNumberCheck.Rows[0][0]) != 0)
                        {
                            continue;
                        }

                        Query = QuotationForJSN(SalesOrderResults, Location, Office, DealerCode);
                    }
                    if (new NpgsqlServer().UpdateTransactionsJSNQuotation(Query, DealerCode))
                    {
                        File.Move(file, file.Replace("DCONNECT", "DCONNECT\\Processed"));
                    }
                    else
                    {
                        File.Move(file, file.Replace("DCONNECT", "DCONNECT\\FAILED"));
                    }
                }
                catch (Exception ex)
                {
                    File.Move(file, file.Replace("DCONNECT", "DCONNECT\\FAILED"));
                    new FileLogger().LogMessageService("BDMS_SalesOrder", "CreateQuotationForJSN", ex);
                }
            }
        }

        List<string> QuotationForJSN(PDMS_SalesOrderResultsJSON SalesOrderResults, string Location, string Office, string DealerCode)
        {
            List<string> Query = new List<string>();

            string Query1S = "insert into dssor_sales_order_hdr (s_establishment,p_so_id,r_order_date";
            string Query1V = " VALUES (1000,'@@QuotationNumber',now()";
            if (!string.IsNullOrEmpty(SalesOrderResults.f_po_id))
            {
                Query1S = Query1S + ",f_po_id";
                Query1V = Query1V + ",'" + SalesOrderResults.f_po_id + "'";
            }

            if (!string.IsNullOrEmpty(SalesOrderResults.r_ext_id))
            {
                Query1S = Query1S + ",r_ext_id";
                Query1V = Query1V + ",'" + SalesOrderResults.r_ext_id + "'";
            }

            if (!string.IsNullOrEmpty(SalesOrderResults.f_customer_id))
            {
                Query1S = Query1S + ",f_customer_id";
                Query1V = Query1V + ",'" + SalesOrderResults.f_customer_id + "'";

                Query1S = Query1S + ",f_ship_to";
                Query1V = Query1V + ",'" + SalesOrderResults.f_customer_id + "01_1" + "'";
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.f_currency))
            {
                Query1S = Query1S + ",f_currency";
                Query1V = Query1V + ",'" + SalesOrderResults.f_currency + "'";
            }

            Query1S = Query1S + ",f_location";
            Query1V = Query1V + ",'" + Location + "'";

            Query1S = Query1S + ",f_office";
            Query1V = Query1V + ",'" + Office + "'";


            if (!string.IsNullOrEmpty(SalesOrderResults.r_insurance_p))
            {
                Query1S = Query1S + ",r_insurance_p";
                Query1V = Query1V + ",'" + SalesOrderResults.r_insurance_p + "'";
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.r_tax_amt))
            {
                Query1S = Query1S + ",r_tax_amt";
                Query1V = Query1V + "," + SalesOrderResults.r_tax_amt;
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.r_net_amt))
            {
                Query1S = Query1S + ",r_net_amt";
                Query1V = Query1V + "," + SalesOrderResults.r_net_amt;
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.r_req_del_date))
            {
                Query1S = Query1S + ",r_req_del_date";
                Query1V = Query1V + ",'" + SalesOrderResults.r_req_del_date + "'";
            }
            //if (!string.IsNullOrEmpty(SalesOrderResults.f_ship_to))
            //{
            //    Query1S = Query1S + ",f_ship_to";
            //    Query1V = Query1V + ",'" + SalesOrderResults.f_ship_to + "'";
            //}
            if (!string.IsNullOrEmpty(SalesOrderResults.r_doc_flow_id))
            {
                Query1S = Query1S + ",r_doc_flow_id";
                Query1V = Query1V + ",'" + SalesOrderResults.r_doc_flow_id + "'";
            }

            if (!string.IsNullOrEmpty(SalesOrderResults.r_description))
            {
                Query1S = Query1S + ",r_description";
                Query1V = Query1V + ",'" + SalesOrderResults.r_description + "'";
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.r_contact_no))
            {
                Query1S = Query1S + ",r_contact_no";
                Query1V = Query1V + ",'" + SalesOrderResults.r_contact_no + "'";
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.r_gross_amt))
            {
                Query1S = Query1S + ",r_gross_amt";
                Query1V = Query1V + "," + SalesOrderResults.r_gross_amt;
            }

            if (!string.IsNullOrEmpty(SalesOrderResults.r_contact_prsn))
            {
                Query1S = Query1S + ",r_contact_prsn";
                Query1V = Query1V + ",'" + SalesOrderResults.r_contact_prsn + "'";
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.r_discount_amt))
            {
                Query1S = Query1S + ",r_discount_amt";
                Query1V = Query1V + "," + SalesOrderResults.r_discount_amt;
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.f_division))
            {
                Query1S = Query1S + ",f_division";
                Query1V = Query1V + ",'" + SalesOrderResults.f_division + "'";
            }


            if (!string.IsNullOrEmpty(SalesOrderResults.f_order_type))
            {
                Query1S = Query1S + ",f_order_type";
                Query1V = Query1V + "," + SalesOrderResults.f_order_type;
            }

            if (!string.IsNullOrEmpty(SalesOrderResults.r_exp_del_date))
            {
                Query1S = Query1S + ",r_exp_del_date";
                Query1V = Query1V + ",'" + SalesOrderResults.r_exp_del_date + "'";
            }

            if (!string.IsNullOrEmpty(SalesOrderResults.r_remarks))
            {
                Query1S = Query1S + ",r_remarks";
                Query1V = Query1V + ",'" + SalesOrderResults.r_remarks + "'";
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.r_frieght_p))
            {
                Query1S = Query1S + ",r_frieght_p";
                Query1V = Query1V + ",'" + SalesOrderResults.r_frieght_p + "'";
            }
            //if (!string.IsNullOrEmpty(SalesOrderResults.r_order_date))
            //{
            //    Query1S = Query1S + ",r_order_date";
            //    Query1V = Query1V + SalesOrderResults.r_order_date;
            //}
            if (!string.IsNullOrEmpty(SalesOrderResults.r_discount_amt_additional))
            {
                Query1S = Query1S + ",r_discount_amt_additional";
                Query1V = Query1V + "," + SalesOrderResults.r_discount_amt_additional;
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.f_sales_office))
            {
                Query1S = Query1S + ",f_sales_office";
                Query1V = Query1V + ",'" + SalesOrderResults.f_sales_office + "'";
            }

            if (!string.IsNullOrEmpty(SalesOrderResults.s_modified_by))
            {
                Query1S = Query1S + ",s_modified_by";
                Query1V = Query1V + ",'" + SalesOrderResults.s_modified_by + "'";
            }

            Query1S = Query1S + ",s_tenant_id";
            Query1V = Query1V + "," + DealerCode;

            if (!string.IsNullOrEmpty(SalesOrderResults.s_status))
            {
                Query1S = Query1S + ",s_status";
                Query1V = Query1V + ",'" + SalesOrderResults.s_status + "'";
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.s_created_by))
            {
                Query1S = Query1S + ",s_created_by";
                Query1V = Query1V + ",'" + SalesOrderResults.s_created_by + "'";
            }

            if (!string.IsNullOrEmpty(SalesOrderResults.s_modified_on))
            {
                Query1S = Query1S + ",s_modified_on";
                Query1V = Query1V + ",'" + SalesOrderResults.s_modified_on + "'";
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.s_object_type))
            {
                Query1S = Query1S + ",s_object_type";
                Query1V = Query1V + "," + SalesOrderResults.s_object_type;
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.r_model))
            {
                Query1S = Query1S + ",r_model";
                Query1V = Query1V + ",'" + SalesOrderResults.r_model + "'";
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.r_model_no))
            {
                Query1S = Query1S + ",r_model_no";
                Query1V = Query1V + ",'" + SalesOrderResults.r_model_no + "'";
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.s_created_on))
            {
                Query1S = Query1S + ",s_created_on";
                Query1V = Query1V + ",'" + SalesOrderResults.s_created_on + "'";
            }
            if (!string.IsNullOrEmpty(SalesOrderResults.channel))
            {
                Query1S = Query1S + ",channel";
                Query1V = Query1V + ",'" + SalesOrderResults.channel + "'";
            }

            Query1S = Query1S + " )";
            Query1V = Query1V + " )";
            Query.Add(Query1S + Query1V);

            //   f_bill_to,r_sales_person, r_sales_prsn_id, s_sync_status , s_action, , , is_ack, r_auto, 
            // r_ref_obj_name, r_ref_obj_type, r_price_grp, , , r_our_ref_id, r_ref_date, r_activity_id, r_activity_item_id, r_vp_id
            //, s_last_request_id, s_last_request_index, r_ext_ref, r_responsible_tenant, r_special_rate, r_insurance_amt
            //, r_packing_chrgs, r_freight_amt, r_reason, r_ext_quote_id, r_hypothecation, r_ref_invoice_id, s_channel, s_status_custom
            //, r_tcs_amt

            // public string t_ship_to_addr { get; set; }
            // public string t_delivery_id { get; set; }
            // public string t_rem_amt { get; set; }
            // public string t_activity_id { get; set; }
            // public string t_tenant_id { get; set; }
            // public string t_invoice_id { get; set; }
            // public string t_action { get; set; }
            // public string t_org_name { get; set; }
            // public string t_org_nick_name { get; set; }
            // public string t_location_desc { get; set; }



            foreach (PDMS_dssor_sales_order_hdr_itemsJSON SalesOrderItems in SalesOrderResults.dssor_sales_order_hdr_items)
            {
                string Query1SItem = "insert into dssor_sales_order_item (s_establishment,p_so_id";
                string Query1VItem = " VALUES (1000,'@@QuotationNumber'";
                if (!string.IsNullOrEmpty(SalesOrderItems.r_item_type))
                {
                    Query1SItem = Query1SItem + ",r_item_type";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_item_type;
                }

                Query1SItem = Query1SItem + ",f_office";
                Query1VItem = Query1VItem + ",'" + Office + "'";

                if (!string.IsNullOrEmpty(SalesOrderItems.r_hgl_item))
                {
                    Query1SItem = Query1SItem + ",r_hgl_item";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_hgl_item;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.d_material_desc))
                {
                    Query1SItem = Query1SItem + ",d_material_desc";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.d_material_desc + "'";
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.f_po_id))
                {
                    Query1SItem = Query1SItem + ",f_po_id";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.f_po_id + "'";
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.f_material_id))
                {
                    Query1SItem = Query1SItem + ",f_material_id";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.f_material_id + "'";
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_order_qty))
                {
                    Query1SItem = Query1SItem + ",r_order_qty";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_order_qty;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_approved_qty))
                {
                    Query1SItem = Query1SItem + ",r_approved_qty";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_approved_qty;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_gross_amt))
                {
                    Query1SItem = Query1SItem + ",r_gross_amt";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_gross_amt;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_pending_qty))
                {
                    Query1SItem = Query1SItem + ",r_pending_qty";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_pending_qty;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_cancel_qty))
                {
                    Query1SItem = Query1SItem + ",r_cancel_qty";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_cancel_qty;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_net_amt))
                {
                    Query1SItem = Query1SItem + ",r_net_amt";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_net_amt;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.f_oem_id))
                {
                    Query1SItem = Query1SItem + ",f_oem_id";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.f_oem_id + "'";
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_tax_amt))
                {
                    Query1SItem = Query1SItem + ",r_tax_amt";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_tax_amt;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.f_uom))
                {
                    Query1SItem = Query1SItem + ",f_uom";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.f_uom + "'";
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.f_location))
                {
                    Query1SItem = Query1SItem + ",f_location";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.f_location + "'";
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_shiped_qty))
                {
                    Query1SItem = Query1SItem + ",r_shiped_qty";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_shiped_qty;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.p_so_item))
                {
                    Query1SItem = Query1SItem + ",p_so_item";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.p_so_item;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_resvered_qty))
                {
                    Query1SItem = Query1SItem + ",r_resvered_qty";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_resvered_qty;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_doc_flow_id))
                {
                    Query1SItem = Query1SItem + ",r_doc_flow_id";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.r_doc_flow_id + "'";
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_discount_amt))
                {
                    Query1SItem = Query1SItem + ",r_discount_amt";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_discount_amt;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_add_discount_amt))
                {
                    Query1SItem = Query1SItem + ",r_add_discount_amt";
                    Query1VItem = Query1VItem + ",-" + SalesOrderItems.r_add_discount_amt.Replace("-", "");
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_exp_del_date))
                {
                    Query1SItem = Query1SItem + ",r_exp_del_date";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.r_exp_del_date + "'";
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_unit_price))
                {
                    Query1SItem = Query1SItem + ",r_unit_price";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.r_unit_price;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.f_po_item))
                {
                    Query1SItem = Query1SItem + ",f_po_item";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.f_po_item;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.f_mat_division))
                {
                    Query1SItem = Query1SItem + ",f_mat_division";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.f_mat_division + "'";
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.s_modified_by))
                {
                    Query1SItem = Query1SItem + ",s_modified_by";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.s_modified_by + "'";
                }
                //if (!string.IsNullOrEmpty(SalesOrderItems.p_so_id))
                //{
                //    Query1SItem = Query1SItem + ",p_so_id";
                //    Query1VItem = Query1VItem + "," + SalesOrderItems.p_so_id;
                //}

                Query1SItem = Query1SItem + ",s_tenant_id";
                Query1VItem = Query1VItem + "," + DealerCode;

                if (!string.IsNullOrEmpty(SalesOrderItems.s_created_on))
                {
                    Query1SItem = Query1SItem + ",s_created_on";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.s_created_on + "'";
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.r_indicator))
                {
                    Query1SItem = Query1SItem + ",r_indicator";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.r_indicator + "'";
                }
                //if (!string.IsNullOrEmpty(SalesOrderItems.s_status))
                //{
                Query1SItem = Query1SItem + ",s_status";
                // Query1VItem = Query1VItem + ",'" + SalesOrderItems.s_status + "'";
                Query1VItem = Query1VItem + ",'QUOTATION'";
                //}
                if (!string.IsNullOrEmpty(SalesOrderItems.s_created_by))
                {
                    Query1SItem = Query1SItem + ",s_created_by";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.s_created_by + "'";
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.s_modified_on))
                {
                    Query1SItem = Query1SItem + ",s_modified_on";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.s_modified_on + "'";
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.s_object_type))
                {
                    Query1SItem = Query1SItem + ",s_object_type";
                    Query1VItem = Query1VItem + "," + SalesOrderItems.s_object_type;
                }
                if (!string.IsNullOrEmpty(SalesOrderItems.channel))
                {
                    Query1SItem = Query1SItem + ",channel";
                    Query1VItem = Query1VItem + ",'" + SalesOrderItems.channel + "'";
                }

                Query1SItem = Query1SItem + " )";
                Query1VItem = Query1VItem + " )";
                Query.Add(Query1SItem + Query1VItem);
                //  s_action, f_division, r_scheme_code, , , , , r_item_category, r_ref_obj_name, s_sync_status,s_last_request_index, is_ack  
                //,s_last_request_id, , r_ref_obj_type, r_batch, r_exp_date, s_channel, s_status_custom, r_delv_qty, r_tcs_amt) 



                foreach (PDMS_dssor_sales_order_item_condsJSON SOIC in SalesOrderItems.dssor_sales_order_item_conds)
                {

                    string Query1SItemC = "insert into dssor_sales_order_cond (s_establishment,p_so_id";
                    string Query1VItemC = " VALUES (1000,'@@QuotationNumber'";

                    if (!string.IsNullOrEmpty(SOIC.f_po_id))
                    {
                        Query1SItemC = Query1SItemC + ",f_po_id";
                        Query1VItemC = Query1VItemC + ",'" + SOIC.f_po_id + "'";
                    }
                    if (!string.IsNullOrEmpty(SOIC.f_currency))
                    {
                        Query1SItemC = Query1SItemC + ",f_currency";
                        Query1VItemC = Query1VItemC + ",'" + SOIC.f_currency + "'";
                    }
                    if (!string.IsNullOrEmpty(SOIC.r_cond_amt))
                    {
                        Query1SItemC = Query1SItemC + ",r_cond_amt";
                        if (SOIC.r_cond_amt.Contains('-'))
                        {
                            Query1VItemC = Query1VItemC + ",-" + SOIC.r_cond_amt.Replace("-", "");
                        }
                        else
                        {
                            Query1VItemC = Query1VItemC + "," + SOIC.r_cond_amt;
                        }
                    }
                    if (!string.IsNullOrEmpty(SOIC.r_order_qty))
                    {
                        Query1SItemC = Query1SItemC + ",r_order_qty";
                        Query1VItemC = Query1VItemC + "," + SOIC.r_order_qty;
                    }
                    if (!string.IsNullOrEmpty(SOIC.r_base_amt))
                    {
                        Query1SItemC = Query1SItemC + ",r_base_amt";
                        Query1VItemC = Query1VItemC + "," + SOIC.r_base_amt;
                    }
                    if (!string.IsNullOrEmpty(SOIC.s_modified_by))
                    {
                        Query1SItemC = Query1SItemC + ",s_modified_by";
                        Query1VItemC = Query1VItemC + ",'" + SOIC.s_modified_by + "'";
                    }
                    //if (!string.IsNullOrEmpty(SOIC.p_so_id))
                    //{
                    //    Query1SItemC = Query1SItemC + ",p_so_id";
                    //    Query1VItemC = Query1VItemC + "," + SOIC.p_so_id;
                    //}

                    Query1SItemC = Query1SItemC + ",s_tenant_id";
                    Query1VItemC = Query1VItemC + "," + DealerCode;

                    if (!string.IsNullOrEmpty(SOIC.r_pric_date))
                    {
                        Query1SItemC = Query1SItemC + ",r_pric_date";
                        Query1VItemC = Query1VItemC + ",'" + SOIC.r_pric_date + "'";
                    }
                    if (!string.IsNullOrEmpty(SOIC.s_created_by))
                    {
                        Query1SItemC = Query1SItemC + ",s_created_by";
                        Query1VItemC = Query1VItemC + ",'" + SOIC.s_created_by + "'";
                    }
                    if (!string.IsNullOrEmpty(SOIC.s_created_on))
                    {
                        Query1SItemC = Query1SItemC + ",s_created_on";
                        Query1VItemC = Query1VItemC + ",'" + SOIC.s_created_on + "'";
                    }
                    if (!string.IsNullOrEmpty(SOIC.s_modified_on))
                    {
                        Query1SItemC = Query1SItemC + ",s_modified_on";
                        Query1VItemC = Query1VItemC + ",'" + SOIC.s_modified_on + "'";
                    }
                    if (!string.IsNullOrEmpty(SOIC.r_cond_grp))
                    {
                        Query1SItemC = Query1SItemC + ",r_cond_grp";
                        Query1VItemC = Query1VItemC + ",'" + SOIC.r_cond_grp + "'";
                    }
                    if (!string.IsNullOrEmpty(SOIC.r_cond_cls))
                    {
                        Query1SItemC = Query1SItemC + ",r_cond_cls";
                        Query1VItemC = Query1VItemC + ",'" + SOIC.r_cond_cls + "'";
                    }
                    if (!string.IsNullOrEmpty(SOIC.d_cond_desc))
                    {
                        Query1SItemC = Query1SItemC + ",d_cond_desc";
                        Query1VItemC = Query1VItemC + ",'" + SOIC.d_cond_desc + "'";
                    }
                    if (!string.IsNullOrEmpty(SOIC.f_percentage))
                    {
                        Query1SItemC = Query1SItemC + ",f_percentage";
                        Query1VItemC = Query1VItemC + "," + SOIC.f_percentage;
                    }
                    if (!string.IsNullOrEmpty(SOIC.p_so_item))
                    {
                        Query1SItemC = Query1SItemC + ",p_so_item";
                        Query1VItemC = Query1VItemC + "," + SOIC.p_so_item;
                    }
                    if (!string.IsNullOrEmpty(SOIC.p_condition_type))
                    {
                        Query1SItemC = Query1SItemC + ",p_condition_type";
                        Query1VItemC = Query1VItemC + ",'" + SOIC.p_condition_type + "'";
                    }
                    if (!string.IsNullOrEmpty(SOIC.channel))
                    {
                        Query1SItemC = Query1SItemC + ",channel";
                        Query1VItemC = Query1VItemC + ",'" + SOIC.channel + "'";
                    }

                    Query1SItemC = Query1SItemC + " )";
                    Query1VItemC = Query1VItemC + " )";

                    Query.Add(Query1SItemC + Query1VItemC);
                }
            }
            return Query;
        }
    }
}
