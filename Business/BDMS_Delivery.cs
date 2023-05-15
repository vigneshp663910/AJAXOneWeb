using DataAccess;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Web;
using System.Web.UI;
using System.Configuration;
using Newtonsoft.Json;

namespace Business
{
    public class BDMS_Delivery
    {
        private IDataAccess provider;
        public BDMS_Delivery()
        {
            provider = new ProviderFactory().GetProvider();
        }
        //public List<PDMS_DeliveryHeader> getDelivery(string DealerCode, string DeliveryNumber, DateTime? DeliveryDateFrom, DateTime? DeliveryDateTo, int? DeliveryTypeID, string DealerStateCode)
        //{
        //     List<PDMS_DeliveryHeader> Ws = new List<PDMS_DeliveryHeader>();
        //    PDMS_DeliveryHeader W = null;

        //    string Query = " select  f_so_id,del.p_del_id,r_del_date,del.s_tenant_id,f_customer_id,d_org_nick_name,deli.f_material_id,deli.d_material_desc,m.r_hsn_id,m.tax_percentage,r_order_qty,r_unit_price,r_discount_amt ,deli.f_office, del.f_address_id "
        //        + " from dsder_delv_hdr del inner join dsder_delv_item deli on deli.p_del_id = del.p_del_id  inner join dssor_sales_order_item sali on sali.p_so_item = deli.f_so_item and sali.p_so_id=  deli.f_so_id "
        //        + "left join af_m_materials m on m.p_material = deli.f_material_id where 1 =1 ";

        //    try
        //    {

        //        Query = string.IsNullOrEmpty(DealerCode) ? Query : Query + " and del.s_tenant_id = " + DealerCode;
        //        Query = string.IsNullOrEmpty(DeliveryNumber) ? Query : Query + " and del.p_del_id = '" + DeliveryNumber + "'";
        //        if (DeliveryDateFrom != null)
        //        {
        //            string dateFrom = ((DateTime)DeliveryDateFrom).ToShortDateString();
        //            Query = Query + " and del.r_del_date >= '" + dateFrom.Split('/')[1] + "/" + dateFrom.Split('/')[0] + "/" + dateFrom.Split('/')[2] + "'";               
        //        }
        //        if (DeliveryDateTo != null)
        //        {
        //            string dateTo = ((DateTime)DeliveryDateTo).ToShortDateString();
        //            Query = Query + " and del.r_del_date <= '" + dateTo.Split('/')[1] + "/" + dateTo.Split('/')[0] + "/" + dateTo.Split('/')[2] + "'";
        //        }

        //        Query = Query + " order by del.p_del_id";

        //        DataTable dt = new NpgsqlServer().ExecuteReader(Query);
        //        PDMS_WarrantyInvoiceHeader SOI = new PDMS_WarrantyInvoiceHeader();

        //        string ID = "";
        //        foreach (DataRow dr in dt.Rows)
        //        {

        //            if (ID != Convert.ToString(dr["p_del_id"]))
        //            {
        //                W = new PDMS_DeliveryHeader();
        //                Ws.Add(W);

        //                W.DeliveryNumber = Convert.ToString(dr["p_del_id"]);
        //                W.DeliveryDate = Convert.ToDateTime(dr["r_del_date"]);
        //                W.SoNumber = Convert.ToString(dr["f_so_id"]);
        //                W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) };
        //                W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["f_customer_id"]), CustomerName = Convert.ToString(dr["d_org_nick_name"]) };
        //                W.DeliveryItems = new List<PDMS_DeliveryItem>();
        //                W.Dealer.DealerOffice = new PDMS_DealerOffice() { OfficeCode = Convert.ToString(dr["f_office"]) };
        //                W.AddressID = Convert.ToString(dr["f_address_id"]);
        //                ID = W.DeliveryNumber;
        //            }
        //            W.DeliveryItems.Add(new PDMS_DeliveryItem()
        //            {

        //                Material = Convert.ToString(dr["f_material_id"]),
        //                MaterialDesc = Convert.ToString(dr["d_material_desc"]),
        //                HSNCode = Convert.ToString(dr["r_hsn_id"]),
        //                Qty = Convert.ToInt32(dr["r_order_qty"]),
        //                Rate = Math.Round(Convert.ToDecimal(dr["r_unit_price"]),2),
        //                Discount =Math.Round(Convert.ToDecimal(dr["r_discount_amt"]),2) ,
        //                TaxPercentage = DBNull.Value==dr["tax_percentage"]?0: Math.Round(Convert.ToDecimal(dr["tax_percentage"]),2)
        //            });
        //        }
        //        decimal GrandTotal = 0;
        //        foreach (PDMS_DeliveryHeader Header in Ws)
        //        {
        //            foreach (PDMS_DeliveryItem Item in Header.DeliveryItems)
        //            {
        //                Item.Value = Item.Qty * Item.Rate;
        //                Item.TaxableValue = Item.Value - Item.Discount;
        //                if (DealerStateCode == "29")
        //                {
        //                    Item.CGST = Convert.ToInt32(Item.TaxPercentage);
        //                    Item.CGSTValue = Math.Round(Item.CGST * Item.TaxableValue / 100,2);
        //                    Item.SGST = Item.CGST;
        //                    Item.SGSTValue = Item.CGSTValue;
        //                    GrandTotal = GrandTotal + Item.TaxableValue + Item.CGSTValue + Item.SGSTValue;
        //                }
        //                else
        //                {
        //                    Item.IGST = Convert.ToInt32(Item.TaxPercentage) * 2;
        //                    Item.IGSTValue =Math.Round(Item.IGST * Item.TaxableValue / 100, 2) ;
        //                    GrandTotal = GrandTotal + Item.TaxableValue + Item.IGSTValue;
        //                }
        //            }
        //            Header.GrandTotal = (int)Math.Round(GrandTotal);
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return Ws;
        //}


        //public List<PDMS_DeliveryHeader> getDelivery(string DealerCode, string DeliveryNumber, string DeliveryDateFrom, string DeliveryDateTo, int? DeliveryTypeID, string DealerStateCode)
        //{
        //    string endPoint = "ICTicket/getDelivery?DealerCode=" + DealerCode + "&DeliveryNumber=" + DeliveryNumber + "&DeliveryDateFrom=" + DeliveryDateFrom + "&DeliveryDateTo="
        //      + DeliveryDateTo + "&DeliveryTypeID=" + DeliveryTypeID + "&DealerStateCode=" + DealerStateCode;
        //    return JsonConvert.DeserializeObject<List<PDMS_DeliveryHeader>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        //}

        public List<PDMS_DeliveryHeader> getDelivery(string DealerCode, string DeliveryNumber, string DeliveryDateFrom, string DeliveryDateTo, int? DeliveryTypeID, string DealerStateCode)
        {
            List<PDMS_DeliveryHeader> Ws = new List<PDMS_DeliveryHeader>();
            PDMS_DeliveryHeader W = null;

            string Query = " select  f_so_id,del.p_del_id,r_del_date,del.s_tenant_id,f_customer_id,d_org_nick_name,deli.f_material_id,deli.d_material_desc,m.r_hsn_id,m.tax_percentage,r_order_qty,r_unit_price,r_discount_amt ,deli.f_office, del.f_address_id "
                + " from dsder_delv_hdr del inner join dsder_delv_item deli on deli.p_del_id = del.p_del_id  inner join dssor_sales_order_item sali on sali.p_so_item = deli.f_so_item and sali.p_so_id=  deli.f_so_id "
                + "left join af_m_materials m on m.p_material = deli.f_material_id where 1 =1 ";

            try
            {

                Query = string.IsNullOrEmpty(DealerCode) ? Query : Query + " and del.s_tenant_id = " + DealerCode;
                Query = string.IsNullOrEmpty(DeliveryNumber) ? Query : Query + " and del.p_del_id = '" + DeliveryNumber + "'";
                if (!string.IsNullOrEmpty(DeliveryDateFrom))
                {
                    Query = Query + " and del.r_del_date >= '" + DeliveryDateFrom.Split('/')[1] + "/" + DeliveryDateFrom.Split('/')[0] + "/" + DeliveryDateFrom.Split('/')[2] + "'";
                }
                if (!string.IsNullOrEmpty(DeliveryDateTo))
                {
                    Query = Query + " and del.r_del_date <= '" + DeliveryDateTo.Split('/')[1] + "/" + DeliveryDateTo.Split('/')[0] + "/" + DeliveryDateTo.Split('/')[2] + "'";
                }

                Query = Query + " order by del.p_del_id";

                //  DataTable dt = new NpgsqlServer().ExecuteReader(Query);
                DataTable dt = new BPG().OutputDataTable(Query);


                PDMS_WarrantyInvoiceHeader SOI = new PDMS_WarrantyInvoiceHeader();

                string ID = "";
                foreach (DataRow dr in dt.Rows)
                {

                    if (ID != Convert.ToString(dr["p_del_id"]))
                    {
                        W = new PDMS_DeliveryHeader();
                        Ws.Add(W);

                        W.DeliveryNumber = Convert.ToString(dr["p_del_id"]);
                        W.DeliveryDate = Convert.ToDateTime(dr["r_del_date"]);
                        W.SoNumber = Convert.ToString(dr["f_so_id"]);
                        W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]) };
                        W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["f_customer_id"]), CustomerName = Convert.ToString(dr["d_org_nick_name"]) };
                        W.DeliveryItems = new List<PDMS_DeliveryItem>();
                        W.Dealer.DealerOffice = new PDMS_DealerOffice() { OfficeCode = Convert.ToString(dr["f_office"]) };
                        W.AddressID = Convert.ToString(dr["f_address_id"]);
                        ID = W.DeliveryNumber;
                    }
                    W.DeliveryItems.Add(new PDMS_DeliveryItem()
                    {

                        Material = Convert.ToString(dr["f_material_id"]),
                        MaterialDesc = Convert.ToString(dr["d_material_desc"]),
                        HSNCode = Convert.ToString(dr["r_hsn_id"]),
                        Qty = Convert.ToInt32(dr["r_order_qty"]),
                        Rate = Math.Round(Convert.ToDecimal(dr["r_unit_price"]), 2),
                        Discount = Math.Round(Convert.ToDecimal(dr["r_discount_amt"]), 2),
                        TaxPercentage = DBNull.Value == dr["tax_percentage"] ? 0 : Math.Round(Convert.ToDecimal(dr["tax_percentage"]), 2)
                    });
                }
                decimal GrandTotal = 0;
                foreach (PDMS_DeliveryHeader Header in Ws)
                {
                    foreach (PDMS_DeliveryItem Item in Header.DeliveryItems)
                    {
                        Item.Value = Item.Qty * Item.Rate;
                        Item.TaxableValue = Item.Value - Item.Discount;
                        if (DealerStateCode == "29")
                        {
                            Item.CGST = Convert.ToInt32(Item.TaxPercentage);
                            Item.CGSTValue = Math.Round(Item.CGST * Item.TaxableValue / 100, 2);
                            Item.SGST = Item.CGST;
                            Item.SGSTValue = Item.CGSTValue;
                            GrandTotal = GrandTotal + Item.TaxableValue + Item.CGSTValue + Item.SGSTValue;
                        }
                        else
                        {
                            Item.IGST = Convert.ToInt32(Item.TaxPercentage) * 2;
                            Item.IGSTValue = Math.Round(Item.IGST * Item.TaxableValue / 100, 2);
                            GrandTotal = GrandTotal + Item.TaxableValue + Item.IGSTValue;
                        }
                    }
                    Header.GrandTotal = (int)Math.Round(GrandTotal);
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }

        public List<PDMS_DeliveryHeader> GetDeliveryTransportationDetails(string DeliveryNumber, DateTime? DeliveryDateFrom, DateTime? DeliveryDateTo)
        {
            List<PDMS_DeliveryHeader> Transportations = new List<PDMS_DeliveryHeader>(); 
            PDMS_DeliveryHeader W = null; 
            DbParameter DeliveryNumberP = provider.CreateParameter("DeliveryNumber", DeliveryNumber, DbType.String);
            DbParameter DeliveryDateFromP = provider.CreateParameter("DeliveryDateFrom", DeliveryDateFrom, DbType.DateTime);
            DbParameter DeliveryDateToP = provider.CreateParameter("DeliveryDateTo", DeliveryDateTo, DbType.DateTime);

            DbParameter[] Params = new DbParameter[3] { DeliveryNumberP, DeliveryDateFromP, DeliveryDateToP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetDeliveryTransportationDetails", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            W = new PDMS_DeliveryHeader();
                            Transportations.Add(W);
                            W.DeliveryID = Convert.ToInt64(dr["DeliveryID"]);
                            W.DeliveryNumber = Convert.ToString(dr["DeliveryNumber"]);
                            W.TransportationThrough = Convert.ToString(dr["TransportationThrough"]);
                            W.TransportationDate = DBNull.Value == dr["TransportationDate"] ? (DateTime?)null : Convert.ToDateTime(dr["TransportationDate"]);
                            W.VehicleNumber = Convert.ToString(dr["VehicleNumber"]);
                        }
                    }
                }
               
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Transportations;
        }

        public PAttachedFile DeliveryChellanBelow50K_Print(string DeliveryNumber, string DealerCode)
        {
            try
            {

                   PDMS_Customer Dealer = new SCustomer().getCustomerAddress(DealerCode);
                PDMS_DeliveryHeader Delivery = getDelivery("", DeliveryNumber, null, null, null, Dealer.State.StateCode)[0];


                 PDMS_DealerOffice DealerOffice = new BDMS_Dealer().GetDealerOffice(null, null, Delivery.Dealer.DealerOffice.OfficeCode)[0];
                PDMS_Customer DealerAD = new SCustomer().getCustomerAddress(DealerOffice.SapLocationCode.Trim());
                string DealerAddress1 = (DealerAD.Address1 + (string.IsNullOrEmpty(DealerAD.Address2) ? "" : "," + DealerAD.Address2) + (string.IsNullOrEmpty(DealerAD.Address3) ? "" : "," + DealerAD.Address3)).Trim(',', ' ');
                string DealerAddress2 = (DealerAD.City + (string.IsNullOrEmpty(DealerAD.State.State) ? "" : "," + DealerAD.State.State) + (string.IsNullOrEmpty(DealerAD.Pincode) ? "" : "-" + DealerAD.Pincode)).Trim(',', ' ');

                PDMS_Customer Customer = null;

                if (!string.IsNullOrEmpty(Delivery.AddressID))
                {
                    Customer = new BDMS_Customer().GetCustomerAddressFromPG_p_office_id(DealerCode, Delivery.Customer.CustomerCode, Delivery.AddressID);
                }
                else
                {
                    Customer = new BDMS_Customer().GetCustomerAddressFromPG(DealerCode, Delivery.Customer.CustomerCode);
                }

               // PDMS_Customer Customer = new SCustomer().getCustomerAddress(Delivery.Customer.CustomerCode);
                string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("Rate");
                CommissionDT.Columns.Add("Value", typeof(decimal));
                CommissionDT.Columns.Add("CGST");
                CommissionDT.Columns.Add("SGST");
                CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("Amount", typeof(decimal)); 

                string StateCode = DealerAD.State.StateCode;
                string GST_Header = "";
                int i = 0;
                foreach (PDMS_DeliveryItem item in Delivery.DeliveryItems)
                {
                    i = i + 1;
                    if (item.SGST != 0)
                    {
                        GST_Header = "CGST & SGST";
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.Value, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.Value + item.CGSTValue + item.SGSTValue);
                    }
                    else
                    {
                        GST_Header = "IGST";
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.Value, item.IGST, null, item.IGSTValue, null, item.Value + item.IGSTValue);
                    }
                }

                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;

                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;

                ReportParameter[] P = new ReportParameter[29];

                List <PDMS_ServiceMaterial> ServiceMaterial = new BDMS_Service().GetServiceMaterials(null, null, null, "", null, Delivery.SoNumber);
                string DateOfComm = "";
                if (ServiceMaterial.Count != 0)
                {
                    PDMS_ICTicket IC = new BDMS_ICTicket().GetICTicketByICTIcketID(ServiceMaterial[0].ICTicketID);
                    //    PDMS_ICTicketTSIR TSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(ServiceMaterial[0].TSIR.TsirID, null);

                    Delivery.ICTicketID = IC.ICTicketNumber;
                    Delivery.ICTicketDate = IC.ICTicketDate;
                    Delivery.Model = IC.Equipment.EquipmentModel.Model;
                    Delivery.HMR = IC.CurrentHMRValue;
                    Delivery.TSIRNumber = ServiceMaterial[0].TSIR == null ? "" : ServiceMaterial[0].TSIR.TsirNumber;
                    Delivery.MachineSerialNumber = IC.Equipment.EquipmentSerialNo;

                    DateOfComm = IC.Equipment.CommissioningOn == null ? "" : ((DateTime)IC.Equipment.CommissioningOn).ToShortDateString();
                }
                else
                {
                    string Query = "select  psc.f_ic_ticket_id, psc.f_ic_ticket_date,psc.r_equipment_ser_no,psc.r_tsir_no,ceq.r_description as Model,Max(r_counter_end) HMR	"
                    + " FROM   dsinr_inv_item invi inner JOIN dsinr_inv_hdr inv ON ( invi.k_id = inv.p_id AND invi.s_tenant_id = inv.s_tenant_id ) 	"
                     + " inner JOIN dsprr_psc_hdr psc ON ( psc.p_sc_id = inv.r_ext_id AND psc.s_tenant_id = inv.s_tenant_id ) 	"
                    + " left join dsprr_psc_counter Hm on Hm.f_counter_ref_id = p_sc_id   and hm. s_tenant_id <> 20		"
                    + " left JOIN dohr_cust_equip_detail ceq ON( ceq.k_equipment_id = psc.f_equipment_id  AND ceq.s_tenant_id = psc.s_tenant_id )  	"
                    + "  where invi.f_del_Id = '" + Delivery.DeliveryNumber + "'"
                    + "  group by  psc.f_ic_ticket_id, psc.f_ic_ticket_date,f_del_Id, psc.r_equipment_ser_no,p_sc_id,psc.r_tsir_no, psc.r_fsr_no_date,ceq.r_description";

                    DataTable dt = new BPG().OutputDataTable(Query);

                    //DataTable dt = new NpgsqlServer().ExecuteReader(Query);

                    foreach (DataRow dr in dt.Rows)
                    {
                        Delivery.ICTicketID = Convert.ToString(dr["f_ic_ticket_id"]);
                        Delivery.ICTicketDate = Convert.ToDateTime(dr["f_ic_ticket_date"]);
                        Delivery.Model = Convert.ToString(dr["Model"]);
                        Delivery.HMR = dr["HMR"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["HMR"]);
                        Delivery.TSIRNumber = Convert.ToString(dr["r_tsir_no"]);
                        Delivery.MachineSerialNumber = Convert.ToString(dr["r_equipment_ser_no"]);
                    }
                }
                PDMS_DeliveryHeader Transportation = GetDeliveryTransportationDetails(Delivery.DeliveryNumber, null, null)[0];
                //   ViewState["Month"] = ddlMonth.SelectedValue;
                P[0] = new ReportParameter("DealerCode", Delivery.Dealer.DealerCode, false);
                P[1] = new ReportParameter("DealerName", DealerAD.CustomerName, false);
                P[2] = new ReportParameter("Address1", DealerAddress1, false);
                P[3] = new ReportParameter("Address2", DealerAddress2, false);
                P[4] = new ReportParameter("Contact", "Contact", false);
                P[5] = new ReportParameter("GSTIN", DealerAD.GSTIN, false);
                P[6] = new ReportParameter("GST_Header", GST_Header, false);
                P[7] = new ReportParameter("GrandTotal", (Delivery.GrandTotal).ToString(), false);
                P[8] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(Delivery.GrandTotal)), false);
                P[9] = new ReportParameter("InvoiceNumber", Delivery.DeliveryNumber, false);

                P[10] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                P[11] = new ReportParameter("CustomerName", Customer.CustomerName, false);
                P[12] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                P[13] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                P[14] = new ReportParameter("CustomerMail", Customer.Email, false);
                P[15] = new ReportParameter("CustomerStateCode", Customer.GSTIN.Length > 2 ? Customer.GSTIN.Substring(0, 2) : "", false);
                P[16] = new ReportParameter("CustomerGST", Customer.GSTIN, false);
                P[17] = new ReportParameter("ICTicketNo", Delivery.ICTicketID, false);
                P[18] = new ReportParameter("Model", Delivery.Model, false);
                P[19] = new ReportParameter("DateOfComm", DateOfComm, false);
                P[20] = new ReportParameter("HMR", Convert.ToString(Delivery.HMR), false);
                P[21] = new ReportParameter("Through", Transportation.TransportationThrough, false);
                P[22] = new ReportParameter("LR", Transportation.VehicleNumber, false);
                P[23] = new ReportParameter("TSIR", Delivery.TSIRNumber, false);
                P[24] = new ReportParameter("MachineSerialNo", Delivery.MachineSerialNumber, false);
                P[25] = new ReportParameter("DateOfFailure", Delivery.ICTicketDate == null ? "" : ((DateTime)Delivery.ICTicketDate).ToShortDateString(), false);
                P[26] = new ReportParameter("InvDate", Delivery.DeliveryDate.ToShortDateString(), false);
                P[27] = new ReportParameter("TransportationDate", Transportation.TransportationDate == null ? "" : ((DateTime)Transportation.TransportationDate).ToShortDateString(), false);
                DateTime NewLogoDate = Convert.ToDateTime(ConfigurationManager.AppSettings["NewLogoDate"]);
                string NewLogo = "0";
                if (NewLogoDate <= Delivery.DeliveryDate)
                {
                    NewLogo = "1";
                }
                P[28] = new ReportParameter("NewLogo", NewLogo, false);
                report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_DeliveryChellan.rdlc");

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);
                report.SetParameters(P);
                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                PAttachedFile InvF = new PAttachedFile();

                InvF.FileType = mimeType;
                InvF.AttachedFile = mybytes;
                InvF.AttachedFileID = 0;
                return InvF;
            }
            catch (Exception ex)
            {
                 //  lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                 //  lblMessage.ForeColor = Color.Red;
                 //  lblMessage.Visible = true;
            }
            return null;
        }
        
        public void InsertOrUpdateDeliveryTransportationDetails(string DeliveryNumber, DateTime DeliveryDate, string TransportationThrough, DateTime? TransportationDate, string VehicleNumber)
        {
            
            DbParameter DeliveryNumberP = provider.CreateParameter("DeliveryNumber", DeliveryNumber, DbType.String);
            DbParameter DeliveryDateP = provider.CreateParameter("DeliveryDate", DeliveryDate, DbType.DateTime);
            DbParameter TransportationThroughP = provider.CreateParameter("TransportationThrough", TransportationThrough, DbType.String);
            DbParameter TransportationDateP = provider.CreateParameter("TransportationDate", TransportationDate, DbType.DateTime);
            DbParameter VehicleNumberP = provider.CreateParameter("VehicleNumber", VehicleNumber, DbType.String);
            DbParameter[] Params = new DbParameter[5] { DeliveryNumberP, DeliveryDateP, TransportationThroughP, TransportationDateP, VehicleNumberP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateDeliveryTransportation", Params);                    
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
           
        }
    }
}
