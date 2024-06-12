using Business;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSales.UserControls
{
    public partial class SalesOrderView : System.Web.UI.UserControl
    {
        public PSaleOrder SOrder
        {
            get
            {
                if (ViewState["SOrder"] == null)
                {
                    ViewState["SOrder"] = new PSaleOrder();
                }
                return (PSaleOrder)ViewState["SOrder"];
            }
            set
            {
                ViewState["SOrder"] = value;
            }
        }
        public List<PSaleOrderItem_Insert> SOItem_Insert
        {
            get
            {
                if (ViewState["SOItem_Insert"] == null)
                {
                    ViewState["SOItem_Insert"] = new List<PSaleOrderItem_Insert>();
                }
                return (List<PSaleOrderItem_Insert>)ViewState["SOItem_Insert"];
            }
            set
            {
                ViewState["SOItem_Insert"] = value;
            }
        }
        public PSaleOrder_Insert SO_Insert
        {
            get
            {
                if (ViewState["SO_Insert"] == null)
                {
                    ViewState["SO_Insert"] = new PSaleOrder_Insert();
                }
                return (PSaleOrder_Insert)ViewState["SO_Insert"];
            }
            set
            {
                ViewState["SO_Insert"] = value;
            }
        }
        public List<PSaleOrderDeliveryItem_Insert> SODelivery_Insert
        {
            get
            {
                if (ViewState["SaleOrderDelivery_Insert"] == null)
                {
                    ViewState["SaleOrderDelivery_Insert"] = new List<PSaleOrderDeliveryItem_Insert>();
                }
                return (List<PSaleOrderDeliveryItem_Insert>)ViewState["SaleOrderDelivery_Insert"];
            }
            set
            {
                ViewState["SaleOrderDelivery_Insert"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessageAddSOItem.Text = "";
            lblMessageSOEdit.Text = "";
            lblMessageCreateSODelivery.Text = "";

            if (!IsPostBack)
            {
            }
        }
        public void fillViewSO(long SaleOrderID)
        {
            SOrder = new BDMS_SalesOrder().GetSaleOrderByID(SaleOrderID);

            lblQuotationNumber.Text = SOrder.QuotationNumber;
            lblQuotationDate.Text = SOrder.QuotationDate.ToString("dd/MM/yyyy");
            lblSaleOrderNumber.Text = SOrder.SaleOrderNumber;
            lblSaleOrderDate.Text = (SOrder.SaleOrderDate == null) ? null : Convert.ToDateTime(SOrder.SaleOrderDate).ToString("dd/MM/yyyy");
            lblProformaInvoiceNumber.Text = SOrder.ProformaInvoiceNumber;
            lblProformaInvoiceDate.Text = (SOrder.ProformaInvoiceDate == null) ? null : Convert.ToDateTime(SOrder.ProformaInvoiceDate).ToString("dd/MM/yyyy");
            lblDealerOffice.Text = SOrder.DealerOffice.OfficeName;
            //lblContactPerson.Text = SOrder.ContactPerson;
            lblRemarks.Text = SOrder.Remarks;
            lblFrieghtPaidBy.Text = SOrder.FrieghtPaidBy;
            lblTaxType.Text = SOrder.TaxType;
            //lblCustomer.Text = SOrder.Customer.CustomerCode + " " + SOrder.Customer.CustomerName;
            lblContactPersonNumber.Text = SOrder.ContactPersonNumber;
            lblExpectedDeliveryDate.Text = SOrder.ExpectedDeliveryDate.ToString("dd/MM/yyyy");
            lblAttn.Text = SOrder.Attn;
            lblSODealer.Text = SOrder.Dealer.DealerCode + " " + SOrder.Dealer.DealerName;
            lblStatus.Text = SOrder.SaleOrderStatus.Status;
            lblDivision.Text = SOrder.Division.DivisionCode;
            lblProduct.Text = SOrder.Product == null ? "" : SOrder.Product.Product;
            lblInsurancePaidBy.Text = SOrder.InsurancePaidBy;
            lblEquipmentSerialNo.Text = SOrder.Equipment.EquipmentSerialNo;
            lblSaleOrderType.Text = SOrder.SaleOrderType.SaleOrderType;
            lblSalesEngnieer.Text = SOrder.SalesEngineer == null ? "" : SOrder.SalesEngineer.ContactName;
            lblHeaderDiscount.Text = SOrder.HeaderDiscountPercentage.ToString();

            lblRefNumber.Text = SOrder.RefNumber;
            lblRefDate.Text = SOrder.RefDate == null ? "" : ((DateTime)SOrder.RefDate).ToString("dd/MM/yyyy");

            lblSoCreatedBy.Text = SOrder.CreatedBy.ContactName;
            lblSalesType.Text = SOrder.SalesType == null ? "" : SOrder.SalesType.Status;

            decimal Discount = 0, TaxableValue = 0, TaxValue = 0, NetAmount = 0;
            foreach (PSaleOrderItem SaleOrderItem in SOrder.SaleOrderItems)
            {
                Discount = Discount + SaleOrderItem.DiscountValue;
                TaxableValue = TaxableValue + SaleOrderItem.TaxableValue;
                TaxValue = TaxValue + SaleOrderItem.Material.CGSTValue + SaleOrderItem.Material.SGSTValue + SaleOrderItem.Material.IGSTValue;
                NetAmount = NetAmount + SaleOrderItem.TaxableValue + SaleOrderItem.Material.CGSTValue + SaleOrderItem.Material.SGSTValue + SaleOrderItem.Material.IGSTValue;
                SaleOrderItem.NetAmount = SaleOrderItem.TaxableValue + SaleOrderItem.Material.CGSTValue + SaleOrderItem.Material.SGSTValue + SaleOrderItem.Material.IGSTValue;
            }

            lblDiscount.Text = Discount.ToString();
            lblTaxableValue.Text = TaxableValue.ToString();
            lblTaxValue.Text = TaxValue.ToString();
            lblNetAmount.Text = NetAmount.ToString();

            gvSOItem.DataSource = SOrder.SaleOrderItems;
            gvSOItem.DataBind();
            gvSODelivery.DataSource = SOrder.Deliverys;
            gvSODelivery.DataBind();


            SOrder.Customer = new BDMS_Customer().GetCustomerByID(SOrder.Customer.CustomerID);
            UC_CustomerView.fillCustomer(SOrder.Customer);
            ActionControlMange();
        }
        void ActionControlMange()
        {
            lbEditSaleOrder.Visible = true;
            lbCancelSaleOrder.Visible = true;
            lbAddSaleOrderItem.Visible = true;
            // lbGenerateQuotation.Visible = true;
            lbGenerateProformaInvoice.Visible = true;
            lbReleaseSaleOrder.Visible = true;
            lbDelivery.Visible = true;

            int StatusID = SOrder.SaleOrderStatus.StatusID;

            if (StatusID == (short)AjaxOneStatus.SaleOrder_Quotation) // Draft
            {
                lbDelivery.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.SaleOrder_ProformaInvoice) // Draft
            {
                lbDelivery.Visible = false;
                lbGenerateProformaInvoice.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.SaleOrder_OrderPlaced) //Order Placed
            {
                lbEditSaleOrder.Visible = false;
                lbCancelSaleOrder.Visible = false;
                lbAddSaleOrderItem.Visible = false;
                lbReleaseSaleOrder.Visible = false;
                lbGenerateProformaInvoice.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.SaleOrder_Cancelled) //Cancelled
            {
                lbEditSaleOrder.Visible = false;
                lbCancelSaleOrder.Visible = false;
                lbAddSaleOrderItem.Visible = false;
                lbReleaseSaleOrder.Visible = false;
                //lbGenerateQuotation.Visible = false;
                lbGenerateProformaInvoice.Visible = false;
                lbDelivery.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.SaleOrder_Delivered) //Delivered
            {
                lbEditSaleOrder.Visible = false;
                lbCancelSaleOrder.Visible = false;
                lbAddSaleOrderItem.Visible = false;
                lbReleaseSaleOrder.Visible = false;
                // lbGenerateQuotation.Visible = false;
                lbGenerateProformaInvoice.Visible = false;
                lbDelivery.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.SaleOrder_PartiallyDelivered) //Partially Delivered
            {
                lbEditSaleOrder.Visible = false;
                //lbCancelSaleOrder.Visible = false;
                lbAddSaleOrderItem.Visible = false;
                lbReleaseSaleOrder.Visible = false;
                //lbGenerateQuotation.Visible = false;
                lbGenerateProformaInvoice.Visible = false;
            }

            if (SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.IntraDealerOrder
                || SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.MachineOrder
                || SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.WarrantyOrder
                )
            {
                lbAddSaleOrderItem.Visible = false;
                txtRefDate.Enabled = false;
                txtRefNumber.Enabled = false;
            }
            else
            {
                txtRefDate.Enabled = true;
                txtRefNumber.Enabled = true;
            }
            DisableSOItemEditDelete();
        }
        private void DisableSOItemEditDelete()
        {
            int StatusID = SOrder.SaleOrderStatus.StatusID;
            if (StatusID == (short)AjaxOneStatus.SaleOrder_OrderPlaced
                || StatusID == (short)AjaxOneStatus.SaleOrder_Delivered
                || StatusID == (short)AjaxOneStatus.SaleOrder_Cancelled
                || SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.MachineOrder
                || SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.WarrantyOrder
                )
            {
                for (int i = 0; i < gvSOItem.Rows.Count; i++)
                {
                    ((LinkButton)gvSOItem.Rows[i].FindControl("lnkBtnEdit")).Visible = false;
                    ((LinkButton)gvSOItem.Rows[i].FindControl("lnkBtnDelete")).Visible = false;
                }
            }
            if (SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.IntraDealerOrder)
            {
                for (int i = 0; i < gvSOItem.Rows.Count; i++)
                {
                    ((LinkButton)gvSOItem.Rows[i].FindControl("lnkBtnDelete")).Visible = false;
                }
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbCancelSaleOrder")
            {
                //PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/CancelSaleOrder?SaleOrderID=" + SOrder.SaleOrderID));
                // PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/UpdateSaleOrderStatus?SaleOrderID=" + SOrder.SaleOrderID + "&StatusID=" + 23));
                PApiResult Results = new BDMS_SalesOrder().UpdateSaleOrderStatus(SOrder.SaleOrderID, (short)AjaxOneStatus.SaleOrder_Cancelled);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SOrder.SaleOrderID);
            }
            else if (lbActions.ID == "lbEditSaleOrder")
            {
                Edit();
                MPE_SaleOrderEdit.Show();
            }
            else if (lbActions.ID == "lbAddSaleOrderItem")
            {
                txtMaterial.Text = "";
                hdfMaterialID.Value = "0";
                txtQty.Text = "";
                MPE_SaleOrderItemAdd.Show();
            }
            else if (lbActions.ID == "lbReleaseSaleOrder")
            {
                //PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/ReleaseSaleOrder?SaleOrderID=" + SOrder.SaleOrderID));
                // PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/UpdateSaleOrderStatus?SaleOrderID=" + SOrder.SaleOrderID + "&StatusID=" + 13));
                PApiResult Results = new BDMS_SalesOrder().UpdateSaleOrderStatus(SOrder.SaleOrderID, (short)AjaxOneStatus.SaleOrder_OrderPlaced);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SOrder.SaleOrderID);
            }
            else if (lbActions.ID == "lbGenerateProformaInvoice")
            {
                PApiResult Results = new BDMS_SalesOrder().UpdateSaleOrderStatus(SOrder.SaleOrderID, (short)AjaxOneStatus.SaleOrder_ProformaInvoice);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SOrder.SaleOrderID);
            }
            else if (lbActions.ID == "lbDelivery")
            {
                MPE_Delivery.Show();
                List<PDMS_EquipmentHeader> EQs = new BDMS_Equipment().GetEquipmentForCreateICTicket(Convert.ToInt64(SOrder.Dealer.DealerID), null, null);
                new DDLBind(ddlEquipment, EQs, "EquipmentSerialNo", "EquipmentHeaderID", true, "Select");
                new DDLBind(ddlPaymentMode, new BDMS_Master().GetAjaxOneStatus((short)AjaxOneStatusHeader.PaymentMode), "Status", "StatusID", true, "Select");
                //cxDispatchDate.StartDate = DateTime.Now;
                //txtBoxDispatchDate.Text = DateTime.Now.ToShortDateString();

                //cxCourierDate.StartDate = DateTime.Now;
                //txtBoxCourierDate.Text = DateTime.Now.ToShortDateString();

                //cxPickupDate.StartDate = DateTime.Now;
                //txtBoxPickupDate.Text = DateTime.Now.ToShortDateString();

                if (SOrder.SaleOrderType.SaleOrderTypeID == 4)
                {
                    divEquipment.Visible = true;
                }

                SODelivery_Insert = new List<PSaleOrderDeliveryItem_Insert>();
                foreach (PSaleOrderItem Item in SOrder.SaleOrderItems)
                {
                    if (Item.Quantity != Item.DeliveredQuantity)
                    {
                         
                        PDealerStock Stock = new BInventory().GetDealerStockCountByID(SOrder.Dealer.DealerID, SOrder.DealerOffice.OfficeID, Item.Material.MaterialID); 
                        SODelivery_Insert.Add(new PSaleOrderDeliveryItem_Insert()
                        {
                            SaleOrderID = SOrder.SaleOrderID,
                            SaleOrderItemID = Item.SaleOrderItemID,
                            MaterialID = Item.Material.MaterialID,
                            MaterialCode = Item.Material.MaterialCode,
                            MaterialDescription = Item.Material.MaterialDescription,
                            UOM = Item.Material.BaseUnit,
                            Quantity = Item.Quantity - Item.DeliveredQuantity,
                            DeliveryQuantity = Item.Quantity - Item.DeliveredQuantity,
                            Value = Item.Value,
                            TaxableValue = Item.TaxableValue,
                            CGST = Item.Material.CGST,
                            CGSTValue = Item.Material.CGSTValue,
                            SGST = Item.Material.SGST,
                            SGSTValue = Item.Material.SGSTValue,
                            IGST = Item.Material.IGST,
                            IGSTValue = Item.Material.IGSTValue,
                            StockAvailable = Stock == null ? 0 : Stock.UnrestrictedQty
                        }); ;
                    }
                }

                List<PDMS_CustomerShipTo> ShipTos = new BDMS_Customer().GetCustomerShopTo(null, SOrder.Customer.CustomerID);
                foreach (PDMS_CustomerShipTo ShipTo in ShipTos)
                {
                    ShipTo.Address1 = ShipTo.Address1 + "," + ShipTo.Address2 + "," + ShipTo.Address3 + "," + ShipTo.District.District + "," + ShipTo.State.State;
                }
                new DDLBind(ddlShiftTo, ShipTos, "Address1", "CustomerShipToID", true, "Select");

                lblBillingAddress.Text = SOrder.Customer.Address1 + ","
                    + SOrder.Customer.Address2 + ","
                    + SOrder.Customer.Address3 + ","
                    + SOrder.Customer.District.District + ","
                    + SOrder.Customer.State.State;

                txtShippingAddress.Text = SOrder.Customer.Address1 + ","
                   + SOrder.Customer.Address2 + ","
                   + SOrder.Customer.Address3 + " ,"
                   + SOrder.Customer.Pincode + " ,"
                   + SOrder.Customer.District.District + ","
                   + SOrder.Customer.State.State;

                // lblDeliveryAddress.Text = lblBillingAddress.Text;

                gvDelivery.DataSource = SODelivery_Insert;
                gvDelivery.DataBind();
                if (SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.MachineOrder || SOrder.Division.DivisionID == (short)SaleOrderType.WarrantyOrder)
                {
                    gvDelivery.Columns[4].Visible = false;
                }
                else
                {
                    gvDelivery.Columns[4].Visible = true;
                }
            }
            else if (lbActions.ID == "lbPreviewQuotation")
            {
                ViewSalesPartsQuotation();
            }
            else if (lbActions.ID == "lbDownloadQuotation")
            {
                DownloadSalesPartsQuotation();
            }
            else if (lbActions.ID == "lbPreviewProformaInvoice")
            {
                ViewSalesProformaInvoice();
            }
            else if (lbActions.ID == "lbDownloadProformaInvoice")
            {
                DownloadSalesProformaInvoice();
            }
        }
        void DownloadSalesPartsQuotation()
        {
            try
            {
                string contentType = string.Empty;
                contentType = "application/pdf";
                string FileName = SOrder.QuotationNumber + ".pdf";
                string mimeType;
                Byte[] mybytes = SalesPartsQuotationRdlc(out mimeType);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(mybytes); // create the file
                new BXcel().PdfDowload();
                Response.Flush(); // send it to the client to download
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void ViewSalesPartsQuotation()
        {
            try
            {
                string mimeType = string.Empty;
                Byte[] mybytes = SalesPartsQuotationRdlc(out mimeType);
                string FileName = SOrder.QuotationNumber + ".pdf";
                var uploadPath = Server.MapPath("~/Backup");
                var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                File.WriteAllBytes(tempfilenameandlocation, mybytes);
                Context.Response.Write("<script language='javascript'>window.open('../PDF.aspx?FileName=" + FileName + "&Title=Sales » Sales Order','_newtab');</script>");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        Byte[] SalesPartsQuotationRdlc(out string mimeType)
        {
            var CC = CultureInfo.CurrentCulture;
            Random r = new Random();
            string extension;
            string encoding;
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;

           PDMS_Dealer Dealer = new BDealer().GetDealerAddress(SOrder.Dealer.DealerID)[0];
            //string DealerCustomerAddress1 = (Dealer.Address.Address1 + (string.IsNullOrEmpty(Dealer.Address.Address2) ? "" : "," + Dealer.Address.Address2) + (string.IsNullOrEmpty(Dealer.Address.Address3) ? "" : "," + Dealer.Address.Address3)).Trim(',', ' ');
            //string DealerCustomerAddress2 = (Dealer.Address.City + (string.IsNullOrEmpty(Dealer.Address.State.State) ? "" : "," + Dealer.Address.State.State) + (string.IsNullOrEmpty(Dealer.Address.Pincode) ? "" : "-" + Dealer.Address.Pincode)).Trim(',', ' ');

            PDMS_DealerOffice DealerOffice = new BDMS_Dealer().GetDealerOffice(null, SOrder.DealerOffice.OfficeID, null)[0];
            string DealerCustomerAddress1 = (DealerOffice.Address1 + (string.IsNullOrEmpty(DealerOffice.Address2) ? "" : "," + DealerOffice.Address2) + (string.IsNullOrEmpty(DealerOffice.Address3) ? "" : "," + DealerOffice.Address3)).Trim(',', ' ');
            string DealerCustomerAddress2 = (DealerOffice.City + (string.IsNullOrEmpty(DealerOffice.State) ? "" : "," + DealerOffice.State) + (string.IsNullOrEmpty(DealerOffice.Pincode) ? "" : "-" + DealerOffice.Pincode)).Trim(',', ' ');

            string CustomerAddress1 = "", CustomerAddress2 = "", StateCode = "", GSTIN = "", PAN = "", CustomerCode = "", CustomerName = "";

            if (SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.IntraDealerOrder)
            {
                PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderHeader(null, null, SOrder.RefNumber, null, null, null, null, null, null, null, null);
                long PurchaseOrderID = JsonConvert.DeserializeObject<List<PPurchaseOrder>>(JsonConvert.SerializeObject(Result.Data))[0].PurchaseOrderID;
                PPurchaseOrder PurchaseOrder = new BDMS_PurchaseOrder().GetPurchaseOrderByID(PurchaseOrderID);
                PDMS_DealerOffice Customer = new BDMS_Dealer().GetDealerOffice(PurchaseOrder.Dealer.DealerID, PurchaseOrder.Location.OfficeID, null)[0];
                CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State) ? "" : "," + Customer.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');
                PDMS_Dealer DealerAddress = new BDealer().GetDealerAddress(PurchaseOrder.Dealer.DealerID)[0];
                StateCode = DealerAddress.Address.State.StateCode;
                GSTIN = DealerAddress.Address.GSTIN;
                PAN = DealerAddress.Address.PAN;
                CustomerName = PurchaseOrder.Dealer.DealerName;
                CustomerCode = PurchaseOrder.Dealer.DealerCode;
            }
            else
            {
                PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(SOrder.Customer.CustomerID);
                CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');
                StateCode = Customer.State.StateCode;
                GSTIN = Customer.GSTIN;
                PAN = Customer.PAN;
                CustomerName = Customer.CustomerName;
                CustomerCode = Customer.CustomerCode;
            }

            List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, SOrder.Dealer.DealerCode, null);

            ReportParameter[] P = new ReportParameter[36];
            P[0] = new ReportParameter("CompanyName", Dealer.DealerName.ToUpper(), false);
            P[1] = new ReportParameter("CompanyAddress1", DealerCustomerAddress1, false);
            P[2] = new ReportParameter("CompanyAddress2", DealerCustomerAddress2, false);
            P[3] = new ReportParameter("CompanyCINandGST", "GSTIN No : " + Dealer.Address.GSTIN);
            P[4] = new ReportParameter("CompanyPAN", "PAN:" + Dealer.Address.PAN);
            P[5] = new ReportParameter("CompanyTelephoneandEmail", "T:" + DealerOffice.Mobile + ",Email:" + DealerOffice.Email);
            P[6] = new ReportParameter("QuotationNo", SOrder.QuotationNumber, false);
            P[7] = new ReportParameter("QuotationDate", SOrder.QuotationDate.ToString(), false);
            P[8] = new ReportParameter("CustomerCode", CustomerCode, false);
            P[9] = new ReportParameter("CustomerName", CustomerName, false);
            P[10] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
            P[11] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
            P[12] = new ReportParameter("CustomerStateCode", StateCode, false);
            P[13] = new ReportParameter("ReceiverGSTINUINNo", GSTIN, false);
            P[14] = new ReportParameter("CustomerPAN", PAN, false);
            P[15] = new ReportParameter("KindAtten", SOrder.Attn, false);
            P[16] = new ReportParameter("ContactNo", SOrder.ContactPersonNumber, false);
            P[17] = new ReportParameter("RefNo", SOrder.RefNumber, false);
            P[18] = new ReportParameter("RefDate", SOrder.RefDate.ToString(), false);
            P[19] = new ReportParameter("Model", (SOrder.Product == null) ? "" : SOrder.Product.Product, false);
            P[20] = new ReportParameter("MCSerNo", SOrder.Equipment.EquipmentSerialNo, false);

            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("ItemNo");
            dtItem.Columns.Add("PartNo");
            dtItem.Columns.Add("Description");
            dtItem.Columns.Add("HSN");
            dtItem.Columns.Add("Qty");
            dtItem.Columns.Add("UOM");
            dtItem.Columns.Add("UnitPrice");
            dtItem.Columns.Add("Discount");
            dtItem.Columns.Add("Taxable");
            dtItem.Columns.Add("CGSTPer");
            dtItem.Columns.Add("CGSTVal");
            dtItem.Columns.Add("SGSTPer");
            dtItem.Columns.Add("SGSTVal");

            decimal SubTotal = 0, GrandTotal = 0, TotalDiscount = 0, TotalTaxable = 0, TotalSGSTVal = 0, TotalCGSTVal = 0;

            int sno = 0;
            foreach (PSaleOrderItem Item in SOrder.SaleOrderItems)
            {
                if (SOrder.TaxType.Contains("SGST & CGST"))
                {
                    dtItem.Rows.Add(sno += 1, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Material.HSN, Item.Quantity.ToString("0")
                        , Item.Material.BaseUnit, String.Format("{0:n}", Item.Value), String.Format("{0:n}", Item.DiscountValue), String.Format("{0:n}", Item.TaxableValue)
                        , String.Format("{0:n}", Item.Material.CGST), String.Format("{0:n}", Item.Material.CGSTValue), String.Format("{0:n}", Item.Material.SGST), String.Format("{0:n}", Item.Material.SGSTValue));
                    SubTotal += (Item.TaxableValue + Item.Material.CGSTValue + Item.Material.SGSTValue);
                    TotalDiscount += Item.DiscountValue;
                    TotalTaxable += Item.TaxableValue;
                    TotalCGSTVal += Item.Material.CGSTValue;
                    TotalSGSTVal += Item.Material.SGSTValue;

                    P[21] = new ReportParameter("CGST_Header", "%", false);
                    P[22] = new ReportParameter("CGSTVal_Header", "CGST", false);
                    P[23] = new ReportParameter("SGST_Header", "%", false);
                    P[24] = new ReportParameter("SGSTVal_Header", "SGST", false);
                }
                else
                {
                    dtItem.Rows.Add(sno += 1, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Material.HSN, Item.Quantity.ToString("0")
                        , Item.Material.BaseUnit, String.Format("{0:n}", Item.Value), String.Format("{0:n}", Item.DiscountValue), String.Format("{0:n}", Item.TaxableValue)
                        , String.Format("{0:n}", 0), String.Format("{0:n}", 0), String.Format("{0:n}", Item.Material.IGST), String.Format("{0:n}", Item.Material.IGSTValue));
                    SubTotal += (Item.TaxableValue + Item.Material.IGSTValue + Item.Material.IGSTValue);
                    TotalDiscount += Item.DiscountValue;
                    TotalTaxable += Item.TaxableValue;
                    TotalSGSTVal += Item.Material.IGSTValue;

                    P[21] = new ReportParameter("CGST_Header", "", false);
                    P[22] = new ReportParameter("CGSTVal_Header", "", false);
                    P[23] = new ReportParameter("SGST_Header", "%", false);
                    P[24] = new ReportParameter("SGSTVal_Header", "IGST", false);
                }
            }
            GrandTotal = Math.Round(SubTotal);

            P[21] = new ReportParameter("CGST_Header", "%", false);
            P[22] = new ReportParameter("CGSTVal_Header", "CGST", false);
            P[23] = new ReportParameter("SGST_Header", "%", false);
            P[24] = new ReportParameter("SGSTVal_Header", "SGST", false);
            P[25] = new ReportParameter("SubTotal", SubTotal.ToString(), false);
            P[26] = new ReportParameter("GrandTotal", String.Format("{0:n}", GrandTotal), false);
            P[27] = new ReportParameter("GrandTotalInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)).ToUpper(), false);
            P[28] = new ReportParameter("DealerAccNo", DealerBank[0].DealerBank.AcNumber, false);
            P[29] = new ReportParameter("DealerBank", DealerBank[0].DealerBank.BankName, false);
            P[30] = new ReportParameter("DealerBranch", DealerBank[0].DealerBank.Branch, false);
            P[31] = new ReportParameter("DealerIfscCode", DealerBank[0].DealerBank.IfscCode, false);
            P[32] = new ReportParameter("TotalDiscount", String.Format("{0:n}", TotalDiscount), false);
            P[33] = new ReportParameter("TotalTaxable", String.Format("{0:n}", TotalTaxable), false);
            P[34] = new ReportParameter("TotalCGSTVal", String.Format("{0:n}", TotalCGSTVal), false);
            P[35] = new ReportParameter("TotalSGSTVal", String.Format("{0:n}", TotalSGSTVal), false);

            report.ReportPath = Server.MapPath("~/Print/SalesPartsQuotation.rdlc");
            report.SetParameters(P);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "SalesPartsQuotation";//This refers to the dataset name in the RDLC file  
            rds.Value = dtItem;
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

            return mybytes;
        }
        void DownloadSalesProformaInvoice()
        {
            try
            {
                string contentType = string.Empty;
                contentType = "application/pdf";
                string FileName = SOrder.ProformaInvoiceNumber + ".pdf";
                string mimeType;
                Byte[] mybytes = SalesProformaInvoiceRdlc(out mimeType);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(mybytes); // create the file
                new BXcel().PdfDowload();
                Response.Flush(); // send it to the client to download
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void ViewSalesProformaInvoice()
        {
            try
            {
                string mimeType = string.Empty;
                Byte[] mybytes = SalesProformaInvoiceRdlc(out mimeType);
                string FileName = SOrder.ProformaInvoiceNumber + ".pdf";
                var uploadPath = Server.MapPath("~/Backup");
                var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                File.WriteAllBytes(tempfilenameandlocation, mybytes);
                Context.Response.Write("<script language='javascript'>window.open('../PDF.aspx?FileName=" + FileName + "&Title=Sales » Sales Order','_newtab');</script>");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        Byte[] SalesProformaInvoiceRdlc(out string mimeType)
        {
            var CC = CultureInfo.CurrentCulture;
            Random r = new Random();
            string extension;
            string encoding;
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;

            PDMS_Dealer Dealer = new BDealer().GetDealerAddress(SOrder.Dealer.DealerID)[0];
            string DealerCustomerAddress1 = (Dealer.Address.Address1 + (string.IsNullOrEmpty(Dealer.Address.Address2) ? "" : "," + Dealer.Address.Address2) + (string.IsNullOrEmpty(Dealer.Address.Address3) ? "" : "," + Dealer.Address.Address3)).Trim(',', ' ');
            string DealerCustomerAddress2 = (Dealer.Address.City + (string.IsNullOrEmpty(Dealer.Address.State.State) ? "" : "," + Dealer.Address.State.State) + (string.IsNullOrEmpty(Dealer.Address.Pincode) ? "" : "-" + Dealer.Address.Pincode)).Trim(',', ' ');

            string CustomerAddress1 = "", CustomerAddress2 = "", StateCode = "", GSTIN = "", PAN = "", CustomerCode = "", CustomerName = "";

            if (SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.IntraDealerOrder)
            {
                PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderHeader(null, null, SOrder.RefNumber, null, null, null, null, null, null, null, null);
                long PurchaseOrderID = JsonConvert.DeserializeObject<List<PPurchaseOrder>>(JsonConvert.SerializeObject(Result.Data))[0].PurchaseOrderID;
                PPurchaseOrder PurchaseOrder = new BDMS_PurchaseOrder().GetPurchaseOrderByID(PurchaseOrderID);
                PDMS_DealerOffice Customer = new BDMS_Dealer().GetDealerOffice(PurchaseOrder.Dealer.DealerID, PurchaseOrder.Location.OfficeID, null)[0];
                CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State) ? "" : "," + Customer.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');
                PDMS_Dealer DealerAddress = new BDealer().GetDealerAddress(PurchaseOrder.Dealer.DealerID)[0];
                StateCode = DealerAddress.Address.State.StateCode;
                GSTIN = DealerAddress.Address.GSTIN;
                PAN = DealerAddress.Address.PAN;
                CustomerName = PurchaseOrder.Dealer.DealerName;
                CustomerCode = PurchaseOrder.Dealer.DealerCode;
            }
            else
            {
                PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(SOrder.Customer.CustomerID);
                CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');
                StateCode = Customer.State.StateCode;
                GSTIN = Customer.GSTIN;
                PAN = Customer.PAN;
                CustomerName = Customer.CustomerName;
                CustomerCode = Customer.CustomerCode;
            }

            List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(SOrder.Dealer.DealerID, null, null);

            ReportParameter[] P = new ReportParameter[34];
            P[0] = new ReportParameter("CompanyName", Dealer.DealerName.ToUpper(), false);
            P[1] = new ReportParameter("CompanyAddress1", DealerCustomerAddress1, false);
            P[2] = new ReportParameter("CompanyAddress2", DealerCustomerAddress2, false);
            P[3] = new ReportParameter("CompanyCINandGST", "CIN:" + Dealer.Address.PAN + ",GST:" + Dealer.Address.GSTIN);
            P[4] = new ReportParameter("CompanyTelephoneandEmail", "T:" + Dealer.Address.Mobile + ",Email:" + Dealer.Address.Email);
            P[5] = new ReportParameter("InvoiceNo", SOrder.ProformaInvoiceNumber, false);
            P[6] = new ReportParameter("InvoiceDate", SOrder.ProformaInvoiceDate.ToString(), false);
            P[7] = new ReportParameter("CustomerCode", CustomerCode, false);
            P[8] = new ReportParameter("CustomerName", CustomerName, false);
            P[9] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
            P[10] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
            P[11] = new ReportParameter("CustomerStateCode", StateCode, false);
            P[12] = new ReportParameter("ReceiverGSTINUINNo", GSTIN, false);
            P[13] = new ReportParameter("CustomerPAN", PAN, false);
            P[14] = new ReportParameter("KindAtten", SOrder.Attn, false);
            P[15] = new ReportParameter("RefNo", SOrder.RefNumber, false);
            P[16] = new ReportParameter("RefDate", SOrder.RefDate.ToString(), false);
            P[17] = new ReportParameter("Model", (SOrder.Product == null) ? "" : SOrder.Product.Product, false);
            P[18] = new ReportParameter("MCSerNo", SOrder.Equipment.EquipmentSerialNo, false);

            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("ItemNo");
            dtItem.Columns.Add("PartNo");
            dtItem.Columns.Add("Description");
            dtItem.Columns.Add("HSN");
            dtItem.Columns.Add("Qty");
            dtItem.Columns.Add("UOM");
            dtItem.Columns.Add("UnitPrice");
            dtItem.Columns.Add("Discount");
            dtItem.Columns.Add("Taxable");
            dtItem.Columns.Add("CGSTPer");
            dtItem.Columns.Add("CGSTVal");
            dtItem.Columns.Add("SGSTPer");
            dtItem.Columns.Add("SGSTVal");

            decimal SubTotal = 0, GrandTotal = 0, TotalDiscount = 0, TotalTaxable = 0, TotalSGSTVal = 0, TotalCGSTVal = 0;

            int sno = 0;
            foreach (PSaleOrderItem Item in SOrder.SaleOrderItems)
            {
                if (SOrder.TaxType.Contains("SGST & CGST"))
                {
                    dtItem.Rows.Add(sno += 1, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Material.HSN, Item.Quantity.ToString("0")
                        , Item.Material.BaseUnit, String.Format("{0:n}", Item.Value), String.Format("{0:n}", Item.DiscountValue), String.Format("{0:n}", Item.TaxableValue)
                        , String.Format("{0:n}", Item.Material.CGST), String.Format("{0:n}", Item.Material.CGSTValue), String.Format("{0:n}", Item.Material.SGST), String.Format("{0:n}", Item.Material.SGSTValue));
                    SubTotal += (Item.TaxableValue + Item.Material.CGSTValue + Item.Material.SGSTValue);
                    TotalDiscount += Item.DiscountValue;
                    TotalTaxable += Item.TaxableValue;
                    TotalCGSTVal += Item.Material.CGSTValue;
                    TotalSGSTVal += Item.Material.SGSTValue;

                    P[19] = new ReportParameter("CGST_Header", "%", false);
                    P[20] = new ReportParameter("CGSTVal_Header", "CGST", false);
                    P[21] = new ReportParameter("SGST_Header", "%", false);
                    P[22] = new ReportParameter("SGSTVal_Header", "SGST", false);
                }
                else
                {
                    dtItem.Rows.Add(sno += 1, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Material.HSN, Item.Quantity.ToString("0")
                        , Item.Material.BaseUnit, String.Format("{0:n}", Item.Value), String.Format("{0:n}", Item.DiscountValue), String.Format("{0:n}", Item.TaxableValue)
                        , String.Format("{0:n}", 0), String.Format("{0:n}", 0), String.Format("{0:n}", Item.Material.IGST), String.Format("{0:n}", Item.Material.IGSTValue));
                    SubTotal += (Item.TaxableValue + Item.Material.IGSTValue + Item.Material.IGSTValue);
                    TotalDiscount += Item.DiscountValue;
                    TotalTaxable += Item.TaxableValue;
                    TotalSGSTVal += Item.Material.IGSTValue;

                    P[19] = new ReportParameter("CGST_Header", "", false);
                    P[20] = new ReportParameter("CGSTVal_Header", "", false);
                    P[21] = new ReportParameter("SGST_Header", "%", false);
                    P[22] = new ReportParameter("SGSTVal_Header", "IGST", false);
                }
            }
            GrandTotal = Math.Round(SubTotal);
            P[23] = new ReportParameter("SubTotal", SubTotal.ToString(), false);
            P[24] = new ReportParameter("GrandTotal", String.Format("{0:n}", GrandTotal), false);
            P[25] = new ReportParameter("GrandTotalInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)).ToUpper(), false);
            P[26] = new ReportParameter("DealerAccNo", DealerBank[0].DealerBank.AcNumber, false);
            P[27] = new ReportParameter("DealerBank", DealerBank[0].DealerBank.BankName, false);
            P[28] = new ReportParameter("DealerBranch", DealerBank[0].DealerBank.Branch, false);
            P[29] = new ReportParameter("DealerIfscCode", DealerBank[0].DealerBank.IfscCode, false);
            P[30] = new ReportParameter("TotalDiscount", String.Format("{0:n}", TotalDiscount), false);
            P[31] = new ReportParameter("TotalTaxable", String.Format("{0:n}", TotalTaxable), false);
            P[32] = new ReportParameter("TotalCGSTVal", String.Format("{0:n}", TotalCGSTVal), false);
            P[33] = new ReportParameter("TotalSGSTVal", String.Format("{0:n}", TotalSGSTVal), false);

            report.ReportPath = Server.MapPath("~/Print/SalesProformaInvoice.rdlc");
            report.SetParameters(P);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "SalesProformaInvoice";//This refers to the dataset name in the RDLC file  
            rds.Value = dtItem;
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

            return mybytes;
        }
        public void Edit()
        {
            cxExpectedDeliveryDate.StartDate = DateTime.Now;
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(SOrder.Dealer.DealerID, null, null), "OfficeName", "OfficeID", true, "Select");
            new DDLBind(ddlSalesType, new BDMS_Master().GetAjaxOneStatus((short)AjaxOneStatusHeader.SalesType), "Status", "StatusID", true, "Select");
            ddlOfficeName.SelectedValue = SOrder.DealerOffice.OfficeID.ToString();

            ddlProduct.BorderColor = Color.Silver;
            txtExpectedDeliveryDate.BorderColor = Color.Silver;

            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, SOrder.Dealer.DealerID, true, null, null, null);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID");
            ddlSalesEngineer.SelectedValue = SOrder.SalesEngineer == null ? "0" : SOrder.SalesEngineer.UserID.ToString();

            txtContactPersonNumber.Text = SOrder.ContactPersonNumber;
            txtRemarks.Text = SOrder.Remarks;
            txtExpectedDeliveryDate.Text = SOrder.ExpectedDeliveryDate.ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(SOrder.InsurancePaidBy))
                ddlInsurancePaidBy.SelectedValue = ddlInsurancePaidBy.Items.FindByText(SOrder.InsurancePaidBy).Value;
            if (!string.IsNullOrEmpty(SOrder.FrieghtPaidBy))
                ddlFrieghtPaidBy.SelectedValue = ddlFrieghtPaidBy.Items.FindByText(SOrder.FrieghtPaidBy).Value;
            txtAttn.Text = SOrder.Attn;
            if (SOrder.Product != null)
                ddlProduct.SelectedValue = SOrder.Product.ProductID.ToString();
            ddlTaxType.SelectedValue = ddlTaxType.Items.FindByText(SOrder.TaxType).Value;
            txtBoxHeaderDiscountPercent.Text = SOrder.HeaderDiscountPercentage.ToString();

            txtRefNumber.Text = SOrder.RefNumber;
            txtRefDate.Text = SOrder.RefDate == null ? "" : ((DateTime)SOrder.RefDate).ToString("dd/MM/yyyy");

            if (SOrder.SalesType != null)
                ddlSalesType.SelectedValue = SOrder.SalesType.StatusID.ToString(); 

        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
            MPE_SaleOrderEdit.Show();
        }
        protected void btnUpdateSO_Click(object sender, EventArgs e)
        {
            lblMessageSOEdit.ForeColor = Color.Red;
            lblMessageSOEdit.Text = "";
            try
            {
                string Message = Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessageSOEdit.Text = Message;
                    MPE_SaleOrderEdit.Show();
                    return;
                }

                SO_Insert = Read();
                //SO_Insert.SaleOrderItems = new List<PSaleOrderItem_Insert>();
                //string result = new BAPI().ApiPut("SaleOrder", SO_Insert);
                string result = new BAPI().ApiPut("SaleOrder/UpdateSaleOrder", SO_Insert);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessageSOEdit.Text = Result.Message;
                    MPE_SaleOrderEdit.Show();
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SOrder.SaleOrderID);
            }
            catch (Exception ex)
            {
                lblMessageSOEdit.Text = ex.Message.ToString();
                MPE_SaleOrderEdit.Show();
            }
        }
        protected void btnSaveSOItem_Click(object sender, EventArgs e)
        {
            lblMessageAddSOItem.ForeColor = Color.Red;
            lblMessageAddSOItem.Text = "";
            try
            {
                string Message = ValidationItem();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessageAddSOItem.Text = Message;
                    MPE_SaleOrderItemAdd.Show();
                    return;
                }
                PDMS_Material m = new BDMS_Material().GetMaterialListSQL(Convert.ToInt32(hdfMaterialID.Value), null, null, null, null)[0];
                PSaleOrderItem_Insert pSaleOrderItem = new BDMS_SalesOrder().ReadItem(m, SOrder.Dealer.DealerID, SOrder.DealerOffice.OfficeID
                    , Convert.ToInt32(txtQty.Text.Trim()), SOrder.Customer.CustomerCode
                    , SOrder.Dealer.DealerCode, SOrder.HeaderDiscountPercentage, 0,0, SOrder.TaxType);

                pSaleOrderItem.SaleOrderID = SOrder.SaleOrderID;
                // pSaleOrderItem = ReadItem();
                string result = new BAPI().ApiPut("SaleOrder/UpdateSaleOrderItem", pSaleOrderItem);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessageAddSOItem.Text = Result.Message;
                    MPE_SaleOrderItemAdd.Show();
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SOrder.SaleOrderID);

            }
            catch (Exception ex)
            {
                lblMessageAddSOItem.Text = ex.Message.ToString();
                MPE_SaleOrderItemAdd.Show();
            }
        }
        protected void lblMaterialRemove_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            //if (SOrder.SaleOrderStatus.StatusID == 23)
            //{
            //    lblMessage.Text = "Sale Order is Cancelled.";
            //    return;
            //}
            //if (SOrder.SaleOrderStatus.StatusID == 15)
            //{
            //    lblMessage.Text = "Sale Order is delivered. Item not allowed edit.";
            //    return;
            //}
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label lblSaleOrderItemID = (Label)gvRow.FindControl("lblSaleOrderItemID");
                PSaleOrderItem Item = (PSaleOrderItem)SOrder.SaleOrderItems.Where(M => M.SaleOrderItemID == Convert.ToInt64(lblSaleOrderItemID.Text)).ToList()[0];

                PSaleOrderItem_Insert SoI = new PSaleOrderItem_Insert();
                SoI.SaleOrderItemID = Item.SaleOrderItemID;
                SoI.MaterialID = Item.Material.MaterialID;
                SoI.MaterialCode = Item.Material.MaterialCode;
                SoI.Quantity = Item.Quantity;
                SoI.PerRate = Item.PerRate;
                SoI.Value = Item.Value;
                // SoI.ItemDiscountPercentage = Item.ItemDiscountPercentage;
                SoI.TaxableValue = Item.TaxableValue;
                SoI.SGST = Item.Material.SGST;
                SoI.SGSTValue = Item.Material.SGSTValue;
                SoI.CGST = Item.Material.CGST;
                SoI.CGSTValue = Item.Material.CGSTValue;
                SoI.IGST = Item.Material.IGST;
                SoI.IGSTValue = Item.Material.IGSTValue;
                SoI.StatusID = (short)AjaxOneStatus.SaleOrderItem_Cancelled;

                PSaleOrder_Insert SO = new PSaleOrder_Insert();
                SO.SaleOrderID = Convert.ToInt64(SOrder.SaleOrderID);
                SO.DealerID = Convert.ToInt32(SOrder.Dealer.DealerID);
                SO.CustomerID = Convert.ToInt32(SOrder.Customer.CustomerID);
                SO.StatusID = 23;
                SO.OfficeID = Convert.ToInt32(SOrder.DealerOffice.OfficeID);
                SO.ContactPersonNumber = SOrder.ContactPersonNumber.Trim();
                SO.DivisionID = Convert.ToInt32(SOrder.Division.DivisionID);
                SO.Remarks = SOrder.Remarks.Trim();
                SO.ExpectedDeliveryDate = SOrder.ExpectedDeliveryDate;
                SO.InsurancePaidBy = SOrder.InsurancePaidBy.Trim();
                SO.FrieghtPaidBy = SOrder.FrieghtPaidBy.Trim();
                SO.Attn = SOrder.Attn.Trim();
                SO.ProductID = Convert.ToInt32(SOrder.Product.ProductID);
                SO.TaxType = SOrder.TaxType.Trim();
                SO.SaleOrderTypeID = SOrder.SaleOrderType.SaleOrderTypeID;
                SO.SalesEngineerID = SOrder.SalesEngineer.UserID;
                SO.HeaderDiscountPercentage = SOrder.HeaderDiscountPercentage;

                SO.SaleOrderItems = new List<PSaleOrderItem_Insert>();
                SO.SaleOrderItems.Add(SoI);

                string result = new BAPI().ApiPut("SaleOrder", SO);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SOrder.SaleOrderID);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        public PSaleOrder_Insert Read()
        {
            PSaleOrder_Insert SO = new PSaleOrder_Insert();
            SO.SaleOrderID = SOrder.SaleOrderID;
            SO.DealerID = SOrder.Dealer.DealerID;
            // SO.CustomerID = Convert.ToInt32(hdfCustomerId.Value);
            SO.StatusID = 11;
            SO.OfficeID = Convert.ToInt32(ddlOfficeName.SelectedValue);
            SO.ContactPersonNumber = txtContactPersonNumber.Text.Trim();
            //SO.DivisionID = Convert.ToInt32(ddlDivision.SelectedValue); 
            SO.Remarks = txtRemarks.Text.Trim();
            SO.ExpectedDeliveryDate = Convert.ToDateTime(txtExpectedDeliveryDate.Text.Trim());
            SO.InsurancePaidBy = ddlInsurancePaidBy.SelectedItem.Text;
            SO.FrieghtPaidBy = ddlFrieghtPaidBy.SelectedItem.Text;
            SO.Attn = txtAttn.Text.Trim();
            SO.ProductID = Convert.ToInt32(ddlProduct.SelectedValue);
            SO.TaxType = ddlTaxType.SelectedItem.Text;
            SO.SaleOrderTypeID = SOrder.SaleOrderType.SaleOrderTypeID;
            SO.SalesEngineerID = Convert.ToInt32(ddlSalesEngineer.SelectedValue);
            SO.HeaderDiscountPercentage = Convert.ToDecimal(txtBoxHeaderDiscountPercent.Text.Trim());
            if (SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.IntraDealerOrder
                || SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.MachineOrder
                || SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.WarrantyOrder
                )
            {
                SO.RefNumber = SOrder.RefNumber;
                SO.RefDate = SOrder.RefDate;
            }
            else
            {
                SO.RefNumber = txtRefNumber.Text.Trim();
                SO.RefDate = string.IsNullOrEmpty(txtRefDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRefDate.Text.Trim());
            }
            SO.SalesTypeID = ddlSalesType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesType.SelectedValue);
            return SO;
        }
        public PSaleOrderItem_Insert ReadItem()
        {
            PSaleOrderItem_Insert SoI = new PSaleOrderItem_Insert();
            SoI.SaleOrderID = SOrder.SaleOrderID;
            SoI.MaterialID = Convert.ToInt32(hdfMaterialID.Value);
            SoI.MaterialCode = hdfMaterialCode.Value;
            SoI.Quantity = Convert.ToInt32(txtQty.Text.Trim());

            PDMS_Material m = new BDMS_Material().GetMaterialListSQL(Convert.ToInt32(SoI.MaterialID), null, null, null, null)[0];
            if (string.IsNullOrEmpty(m.HSN))
            {
                throw new Exception("HSN Code is not updated for this Material. Please contact Parts Admin.");
            }
            List<PMaterial_Api> Material_SapS = new List<PMaterial_Api>();
            Material_SapS.Add(new PMaterial_Api()
            {
                MaterialCode = SoI.MaterialCode,
                Quantity = SoI.Quantity,
                Item = (Material_SapS.Count + 1) * 10
            });

            PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
            MaterialPrice.Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt32(SOrder.Customer.CustomerID)).CustomerCode;
            MaterialPrice.Vendor = new BDealer().GetDealerByID(Convert.ToInt32(SOrder.Dealer.DealerID), "").DealerCode;
            MaterialPrice.OrderType = "101_DSSOR_SALES_ORDER_HDR";

            MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
            MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
            {
                ItemNo = "10",
                Material = SoI.MaterialCode,
                Quantity = SoI.Quantity
            });
            List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
            PMaterial Mat = Mats[0];

            if (Mat.CurrentPrice <= 0)
            {
                throw new Exception("Please maintain the Price for Material " + SoI.MaterialCode + " in SAP.");
            }
            if (Mat.SGST <= 0 && Mat.IGST <= 0)
            {
                throw new Exception("Please maintain the Tax for Material " + SoI.MaterialCode + " in SAP.");
            }
            if (Mat.SGSTValue <= 0 && Mat.IGSTValue <= 0)
            {
                throw new Exception("GST Tax value not found this Material " + SoI.MaterialCode + " in SAP.");
            }

            SoI.PerRate = Mat.CurrentPrice / SoI.Quantity;
            SoI.Value = Mat.CurrentPrice;

            SoI.DiscountValue = SOrder.HeaderDiscountPercentage > 0 ? (Mat.CurrentPrice * (SOrder.HeaderDiscountPercentage / 100)) : Mat.Discount;

            SoI.TaxableValue = SOrder.HeaderDiscountPercentage > 0 ? (Mat.CurrentPrice - (Mat.CurrentPrice * (SOrder.HeaderDiscountPercentage / 100))) : Mat.TaxablePrice;

            if (SOrder.TaxType == "SGST & CGST")
            {
                SoI.SGST = (Mat.SGST + Mat.CGST + Mat.IGST) / 2;
                SoI.SGSTValue = SOrder.HeaderDiscountPercentage > 0 ? (SoI.TaxableValue * (SoI.SGST / 100)) : (Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue) / 2;
                SoI.CGST = (Mat.SGST + Mat.CGST + Mat.IGST) / 2;
                SoI.CGSTValue = SOrder.HeaderDiscountPercentage > 0 ? (SoI.TaxableValue * (SoI.CGST / 100)) : (Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue) / 2;
                SoI.IGST = 0;
                SoI.IGSTValue = 0;
            }
            else
            {
                SoI.SGST = 0;
                SoI.SGSTValue = 0;
                SoI.CGST = 0;
                SoI.CGSTValue = 0;
                SoI.IGST = Mat.SGST + Mat.CGST + Mat.IGST;
                SoI.IGSTValue = SOrder.HeaderDiscountPercentage > 0 ? (SoI.TaxableValue * (SoI.IGST / 100)) : (Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue);
            }

            SoI.NetAmount = SoI.TaxableValue + SoI.SGSTValue + SoI.CGSTValue + SoI.IGSTValue;
            SoI.MaterialDescription = m.MaterialDescription;
            SoI.HSN = m.HSN;
            SoI.UOM = m.BaseUnit;
            return SoI;
        }
        public string Validation()
        {
            ddlOfficeName.BorderColor = Color.Silver;
            //txtCustomer.BorderColor = Color.Silver;
            txtContactPersonNumber.BorderColor = Color.Silver;
            //ddlDivision.BorderColor = Color.Silver;
            ddlProduct.BorderColor = Color.Silver;
            txtExpectedDeliveryDate.BorderColor = Color.Silver;
            ddlTaxType.BorderColor = Color.Silver;
            txtBoxHeaderDiscountPercent.BorderColor = Color.Silver;
            string Message = "";
            if (ddlOfficeName.SelectedValue == "0")
            {
                ddlOfficeName.BorderColor = Color.Red;
                return "Please select the Dealer Office.";
            }
            //if (string.IsNullOrEmpty(hdfCustomerId.Value))
            //{
            //    txtCustomer.BorderColor = Color.Red;
            //    return "Please enter Customer.";
            //}
            if (!string.IsNullOrEmpty(txtContactPersonNumber.Text.Trim()))
            {
                long longCheck;
                if (!long.TryParse(txtContactPersonNumber.Text.Trim(), out longCheck))
                {
                    txtContactPersonNumber.BorderColor = Color.Red;
                    return "Mobile should be 10 Digit.";
                }
            }
            //if (ddlDivision.SelectedValue == "0")
            //{
            //    ddlDivision.BorderColor = Color.Red;
            //    return "Please select the Division.";
            //}
            if (ddlProduct.SelectedValue == "0")
            {
                ddlProduct.BorderColor = Color.Red;
                return "Please select the Product.";
            }
            if (string.IsNullOrEmpty(txtExpectedDeliveryDate.Text))
            {
                txtExpectedDeliveryDate.BorderColor = Color.Red;
                return "Please enter the Expected Delivery Date.";
            }
            if (ddlTaxType.SelectedValue == "0")
            {
                ddlTaxType.BorderColor = Color.Red;
                return "Please select Tax.";
            }
            decimal value;
            if (!decimal.TryParse(txtBoxHeaderDiscountPercent.Text, out value))
            {
                txtBoxHeaderDiscountPercent.BackColor = Color.Red;
                return "Please enter correct format in Header Discount Percent.";
            }
            if (Convert.ToDecimal(txtBoxHeaderDiscountPercent.Text) >= 100)
            {
                txtBoxHeaderDiscountPercent.BackColor = Color.Red;
                return "Discount Percentage cannot exceed 100.";
            }
            return Message;
        }
        public string ValidationItem()
        {
            if (string.IsNullOrEmpty(hdfMaterialID.Value))
            {
                return "Please select the Material.";
            }

            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                return "Please enter the Qty.";
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) < 1)
            {
                return "Quantity cannot be less than 1."; 
            }

            foreach (PSaleOrderItem Item in SOrder.SaleOrderItems)
            {
                if (Item.Material.MaterialID == Convert.ToInt32(hdfMaterialID.Value))
                {
                    return "Duplicate Material.";
                }
            }

            decimal value;
            if (!decimal.TryParse(txtQty.Text, out value))
            {
                return "Please enter correct format in Qty.";
            }
            return "";
        }
        protected void lnkBtnItemAction_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            //if (SOrder.SaleOrderStatus.StatusID == 23)
            //{
            //    lblMessage.Text = "Sale Order is Cancelled.";
            //    return;
            //}
            //if (SOrder.SaleOrderStatus.StatusID == 15)
            //{
            //    lblMessage.Text = "Sale Order is Delivered. Item not allowed edit.";
            //    return;
            //}

            LinkButton lbActions = ((LinkButton)sender);
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;


            LinkButton lnkBtnEdit = (LinkButton)gvRow.FindControl("lnkBtnEdit");
            LinkButton lnkBtnUpdate = (LinkButton)gvRow.FindControl("lnkBtnupdate");
            LinkButton lnkBtnCancel = (LinkButton)gvRow.FindControl("lnkBtnCancel");
            LinkButton lnkBtnDelete = (LinkButton)gvRow.FindControl("lnkBtnDelete");
            //LinkButton lblMaterialRemove = (LinkButton)gvRow.FindControl("lblMaterialRemove");

            TextBox txtBoxQuantity = (TextBox)gvRow.FindControl("txtBoxQuantity");
            Label lblQuantity = (Label)gvRow.FindControl("lblQuantity");
            //TextBox txtItemDiscountValue = (TextBox)gvRow.FindControl("txtItemDiscountValue");
            Panel pnlItemDiscount = (Panel)gvRow.FindControl("pnlItemDiscount");
            
            Label lblItemDiscountValue = (Label)gvRow.FindControl("lblItemDiscountValue");

            if (lbActions.ID == "lnkBtnEdit")
            {
                lnkBtnEdit.Visible = false;
                lnkBtnUpdate.Visible = true;
                lnkBtnCancel.Visible = true;
                lnkBtnDelete.Visible = false;
                //lblMaterialRemove.Visible = false;

                txtBoxQuantity.Visible = true;
                lblQuantity.Visible = false;
                pnlItemDiscount.Visible = true;
                lblItemDiscountValue.Visible = false;
            }
            else if (lbActions.ID == "lnkBtnUpdate")
            {
                Label lblSaleOrderItemID = (Label)gvRow.FindControl("lblSaleOrderItemID");
                Label lblMaterialID = (Label)gvRow.FindControl("lblMaterialID");

                

                TextBox txtItemDiscountPercentage = (TextBox)gvRow.FindControl("txtItemDiscountPercentage");
                decimal IDiscountValue = 0;
                decimal IDiscountPercentage = 0;

                decimal value;
                if (!decimal.TryParse(txtBoxQuantity.Text.Trim(), out value))
                {
                    lblMessage.Text = "Please enter correct format in Qty.";
                    return;
                }
                if (Convert.ToDecimal(txtBoxQuantity.Text.Trim()) < 1)
                {
                    lblMessage.Text = "Quantity cannot be less than 1.";
                    return;
                }

                if (!string.IsNullOrEmpty(txtItemDiscountPercentage.Text.Trim()))
                { 
                    if (!decimal.TryParse(txtItemDiscountPercentage.Text.Trim(), out value))
                    {
                        lblMessage.Text = "Please enter correct format in Discount Percent.";
                        return;
                    }
                    IDiscountPercentage = Convert.ToDecimal(txtItemDiscountPercentage.Text.Trim()) ;
                    if (0 > IDiscountPercentage || IDiscountPercentage > 100)
                    {
                        lblMessage.Text = "Discount Percentage cannot be less than 0 or  exceed 100.";
                        return;
                    }
                }
                else
                {
                    TextBox txtItemDiscountValue = (TextBox)gvRow.FindControl("txtItemDiscountValue");
                    if (!decimal.TryParse(txtItemDiscountValue.Text.Trim(), out value))
                    {
                        lblMessage.Text = "Please enter correct format in Discount Percent.";
                        return;
                    }
                    IDiscountValue = Convert.ToDecimal(txtItemDiscountValue.Text.Trim());
                    if (IDiscountValue < 0)
                    {
                        lblMessage.Text = "Discount value cannot be less than 0.";
                        return;
                    }
                }

                //PSaleOrderItem_Insert item_Insert = new PSaleOrderItem_Insert();
                //item_Insert.SaleOrderItemID = Convert.ToInt64(lblSaleOrderItemID.Text);
                //item_Insert.Quantity = Convert.ToDecimal(txtBoxQuantity.Text); 
                try
                {
                    PDMS_Material m = new BDMS_Material().GetMaterialListSQL(Convert.ToInt32(lblMaterialID.Text), null, null, null, null)[0];
                    int Quantity = Convert.ToInt32(Convert.ToDecimal(txtBoxQuantity.Text.Trim()));
                    PSaleOrderItem_Insert item_Insert = new BDMS_SalesOrder().ReadItem(m, SOrder.Dealer.DealerID, SOrder.Dealer.DealerOffice.OfficeID, Quantity, SOrder.Customer.CustomerCode
                        , SOrder.Dealer.DealerCode, SOrder.HeaderDiscountPercentage, IDiscountValue, IDiscountPercentage, SOrder.TaxType);
                    item_Insert.SaleOrderItemID = Convert.ToInt64(lblSaleOrderItemID.Text);
                    item_Insert.StatusID = (short)AjaxOneStatus.SaleOrderItem_Created;
                    string result = new BAPI().ApiPut("SaleOrder/UpdateSaleOrderItem", item_Insert);
                    PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                    if (Result.Status == PApplication.Failure)
                    {
                        lblMessage.Text = Result.Message;
                        return;
                    }
                    lblMessage.Text = Result.Message;
                    lblMessage.ForeColor = Color.Green;
                    fillViewSO(SOrder.SaleOrderID);
                    lnkBtnEdit.Visible = true;
                    lnkBtnUpdate.Visible = false;
                    lnkBtnCancel.Visible = false;
                    lnkBtnDelete.Visible = true;
                    //lblMaterialRemove.Visible = true; 
                    txtBoxQuantity.Visible = false;
                    lblQuantity.Visible = true;
                }
                catch (Exception e1)
                {
                    lblMessage.Text = e1.Message;
                    return;
                }
            }
            else if (lbActions.ID == "lnkBtnCancel")
            {
                lnkBtnEdit.Visible = true;
                lnkBtnUpdate.Visible = false;
                lnkBtnCancel.Visible = false;
                lnkBtnDelete.Visible = true;
                //lblMaterialRemove.Visible = true;
                txtBoxQuantity.Visible = false;
                lblQuantity.Visible = true;
                pnlItemDiscount.Visible = false;
                lblItemDiscountValue.Visible = true;
            }
            else if (lbActions.ID == "lnkBtnDelete")
            {
                Label lblSaleOrderItemID = (Label)gvRow.FindControl("lblSaleOrderItemID");
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/CancelSaleOrderItem?SaleOrderItemID=" + Convert.ToInt64(lblSaleOrderItemID.Text)));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SOrder.SaleOrderID);
            }
            else if (lbActions.ID == "lnkBtnStockAvailability")
            {
                Label lblMaterialID = (Label)gvRow.FindControl("lblMaterialID");
                PDealerStock s = new BInventory().GetDealerStockCountByID(SOrder.Dealer.DealerID, SOrder.DealerOffice.OfficeID, Convert.ToInt32(lblMaterialID.Text));

                if (s != null)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "On Order Qty : " + s.OnOrderQty.ToString() + ", Transit Qty : " + s.TransitQty.ToString() + ", Unrestricted Qty : " + s.UnrestrictedQty.ToString();
                }
                else
                {
                    lblMessage.Text = "Stock is not available";
                }
            }
        }
        protected void btnSaveDelivery_Click(object sender, EventArgs e)
        {
            lblMessageCreateSODelivery.ForeColor = Color.Red;
            MPE_Delivery.Show();
            try
            {
                if (SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.MachineOrder)
                {
                    if (ddlEquipment.SelectedValue == "0")
                    {
                        lblMessageCreateSODelivery.Text = "Please select the Equipment.";
                        return;
                    }
                }

                readSaleOrderDelivery();
                foreach (PSaleOrderDeliveryItem_Insert T in SODelivery_Insert)
                {
                    T.ShiftToID = ddlShiftTo.SelectedValue == "0" ? (long?)null : Convert.ToInt64(ddlShiftTo.SelectedValue);
                    T.EquipmentHeaderID = ddlEquipment.SelectedValue == "0" ? (long?)null : Convert.ToInt64(ddlEquipment.SelectedValue);
                    T.PaymentModeID = ddlPaymentMode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlPaymentMode.SelectedValue);
                    T.ShippingAddress = txtShippingAddress.Text; 
                }
                SODelivery_Insert.RemoveAll(r => r.DeliveryQuantity == 0);

                PApiResult Result = new BDMS_SalesOrder().InsertSaleOrderDelivery(SODelivery_Insert);
                if (Result.Status == PApplication.Failure)
                {
                    lblMessageCreateSODelivery.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SOrder.SaleOrderID);
                MPE_Delivery.Hide();
            }
            catch (Exception e1)
            {
                lblMessageCreateSODelivery.Text = e1.Message;
            }
        }
        void readSaleOrderDelivery()
        {
            foreach (GridViewRow row in gvDelivery.Rows)
            {
                Label lblSaleOrderItemID = (Label)row.FindControl("lblSaleOrderItemID");
                Label lblOrderQty = (Label)row.FindControl("lblOrderQty");
                TextBox txtDeliveryQuantity = (TextBox)row.FindControl("txtDeliveryQuantity");

                decimal OrderQty = Convert.ToDecimal(lblOrderQty.Text);
                decimal DeliveryQuantity = 0;

                if (SOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.MachineOrder || SOrder.Division.DivisionID == (short)SaleOrderType.WarrantyOrder)
                {
                    DeliveryQuantity = OrderQty;
                } 
                else
                {
                    if (string.IsNullOrEmpty(txtDeliveryQuantity.Text))
                    {
                        throw new Exception("Please enter the Delivery Quantity.");
                    }
                    DeliveryQuantity = Convert.ToDecimal(txtDeliveryQuantity.Text);
                }
              
                if (DeliveryQuantity > OrderQty || DeliveryQuantity < 0)
                {
                    throw new Exception("Please check the Delivery Quantity.");
                }
                Label lblMaterialID = (Label)row.FindControl("lblMaterialID");
                PDealerStock Stock = new BInventory().GetDealerStockCountByID(SOrder.Dealer.DealerID, SOrder.DealerOffice.OfficeID, Convert.ToInt32(lblMaterialID.Text));


                Label lblMaterialCode = (Label)row.FindControl("lblMaterialCode");

                if (DeliveryQuantity > Stock.UnrestrictedQty)
                {
                    throw new Exception("Please check the Stock for material : " + lblMaterialCode.Text);
                }


                foreach (PSaleOrderDeliveryItem_Insert Item in SODelivery_Insert)
                {
                    if (Convert.ToInt64(lblSaleOrderItemID.Text) == Item.SaleOrderItemID)
                    {
                        Item.DeliveryQuantity = DeliveryQuantity;
                        break;
                    }
                }
            }
        }
        protected void gvDelivery_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                MPE_Delivery.Show();
                readSaleOrderDelivery();
                gvDelivery.DataSource = SODelivery_Insert;
                gvDelivery.PageIndex = e.NewPageIndex;
                gvDelivery.DataBind();

            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message;
            }
        }
        //protected void ddlShiftTo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MPE_Delivery.Show();
        //    if (ddlShiftTo.SelectedValue == "0")
        //    {
        //        lblDeliveryAddress.Text = lblBillingAddress.Text;
        //    }
        //    else
        //    {
        //        PDMS_CustomerShipTo ShiftTo = new BDMS_Customer().GetCustomerShopTo(Convert.ToInt64(ddlShiftTo.SelectedValue), SOrder.Customer.CustomerID)[0];
        //        lblDeliveryAddress.Text = ShiftTo.Address1 + "," + ShiftTo.Address2 + "," + ShiftTo.Address3 + "," + ShiftTo.District.District + "," + ShiftTo.State.State;
        //    }
        //}
        protected void ddlShiftTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPE_Delivery.Show(); 
            if (ddlShiftTo.SelectedValue == "0")
            { 
                txtShippingAddress.Text = SOrder.Customer.Address1 + ","
                    + SOrder.Customer.Address2 + ","
                    + SOrder.Customer.Address3 + " ,"
                    + SOrder.Customer.Pincode + " ,"
                    + SOrder.Customer.District.District + ","
                    + SOrder.Customer.State.State;                
            }
            else
            {
                PDMS_CustomerShipTo ShiftTo = new BDMS_Customer().GetCustomerShopTo(Convert.ToInt64(ddlShiftTo.SelectedValue), SOrder.Customer.CustomerID)[0];                
                txtShippingAddress.Text = ShiftTo.Address1 + ","
                    + ShiftTo.Address2 + ","
                    + ShiftTo.Address3 + " ,"
                    + ShiftTo.Pincode
                     + ShiftTo.District.District + ","
                    + ShiftTo.State.State;                
            }
        }
    }
}