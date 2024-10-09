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
    public partial class SalesOrderDeliveryView : System.Web.UI.UserControl
    {
        public long SODeliveryID
        {
            get
            {
                if (ViewState["SaleOrderDeliveryID"] == null)
                {
                    ViewState["SaleOrderDeliveryID"] = 0;
                }
                return (long)ViewState["SaleOrderDeliveryID"];
            }
            set
            {
                ViewState["SaleOrderDeliveryID"] = value;
            }
        }
        public PSaleOrderDelivery SaleOrderDeliveryByID
        {
            get
            {
                if (ViewState["SaleOrderDeliveryByID"] == null)
                {
                    ViewState["SaleOrderDeliveryByID"] = new PSaleOrderDelivery();
                }
                return (PSaleOrderDelivery)ViewState["SaleOrderDeliveryByID"];
            }
            set
            {
                ViewState["SaleOrderDeliveryByID"] = value;
            }
        }
        public PSaleOrderDeliveryShipping Shipping_Insert
        {
            get
            {
                if (ViewState["SaleOrderDelivery_Insert"] == null)
                {
                    ViewState["SaleOrderDelivery_Insert"] = new PSaleOrderDeliveryShipping();
                }
                return (PSaleOrderDeliveryShipping)ViewState["SaleOrderDelivery_Insert"];
            }
            set
            {
                ViewState["SaleOrderDelivery_Insert"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbPreviewDC")
            {
                ViewSalesDeliveryChallan();
            }
            else if (lbActions.ID == "lbDowloadDC")
            {
                DownloadSalesDeliveryChallan();
            }
            else if (lbActions.ID == "lbGenerateInvoice")
            {
                PDMS_Dealer Dealer = new BDMS_Dealer().GetDealer(SaleOrderDeliveryByID.SaleOrder.Dealer.DealerID, null, null, null)[0];
                if (Dealer.IsEInvoice && SaleOrderDeliveryByID.SaleOrder.Customer.GSTIN != "URD")
                {
                    if (string.IsNullOrEmpty(SaleOrderDeliveryByID.SaleOrder.Customer.Address1))
                    {
                        lblMessage.Text = "Please update Customer Address.";
                        return;
                    }
                    if (string.IsNullOrEmpty(SaleOrderDeliveryByID.SaleOrder.Customer.City))
                    {
                        lblMessage.Text = "Please update Customer City.";
                        return;
                    }
                    if (string.IsNullOrEmpty(SaleOrderDeliveryByID.SaleOrder.Customer.Pincode))
                    {
                        lblMessage.Text = "Please update Customer Pincode.";
                        return;
                    }
                    string Message = ValidationEInvoice(SaleOrderDeliveryByID.SaleOrder.Customer.GSTIN, SaleOrderDeliveryByID.SaleOrder.Customer.Pincode, SaleOrderDeliveryByID.SaleOrder.Customer.State.StateCode);

                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        return;
                    }
                }

                PApiResult Results = new BDMS_SalesOrder().GenerateSaleInvoice(SODeliveryID);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Green;
                fillViewSODelivery(SODeliveryID);
                // PDMS_Dealer Dealer = new BDMS_Dealer().GetDealer(SaleOrderDeliveryByID.SaleOrder.Dealer.DealerID, null, null, null)[0];
                 
                if (Dealer.IsEInvoice && SaleOrderDeliveryByID.SaleOrder.Customer.GSTIN != "URD" && (Dealer.ServicePaidEInvoice))
                {
                    new BDMS_EInvoice().GeneratEInvoice(SaleOrderDeliveryByID.InvoiceNumber, "SalesInv");
                } 
            }
            else if (lbActions.ID == "lbDowloadInvoice")
            {
                try
                {
                    if (SaleOrderDeliveryByID.SaleOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.MachineOrder)
                    {
                        DownloadSalesMachineInvoice();
                    }
                    else
                    {
                        ibPDF_Click();
                    }
                }
                catch (Exception e1)
                {
                    lblMessage.Text = e1.Message;
                    lblMessage.ForeColor = Color.Red;
                }
            }
            else if (lbActions.ID == "lbUpdateShippingDetails")
            {
                MPE_Delivery.Show();
                cxDispatchDate.StartDate = DateTime.Now;
                txtBoxDispatchDate.Text = DateTime.Now.ToShortDateString();

                cxCourierDate.StartDate = DateTime.Now;
                txtBoxCourierDate.Text = DateTime.Now.ToShortDateString();

                cxPickupDate.StartDate = DateTime.Now;
                txtBoxPickupDate.Text = DateTime.Now.ToShortDateString();
            }
            else if (lbActions.ID == "lbPreviewInvoice")
            {
                try
                {
                    // string FileName = PSession.User.UserID + DateTime.Now.ToShortDateString().Replace("/", "") + DateTime.Now.ToLongTimeString().Replace(":", "") + ".pdf";
                    string FileName = SaleOrderDeliveryByID.InvoiceNumber + ".pdf";
                    var uploadPath = Server.MapPath("~/Backup");
                    var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                    if (SaleOrderDeliveryByID.SaleOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.MachineOrder)
                    {
                        string mimeType = string.Empty;
                        Byte[] mybytes = SalesMachineInvoiceRdlc(out mimeType);
                        File.WriteAllBytes(tempfilenameandlocation, mybytes);
                    }
                    else
                    {
                        PAttachedFile UploadedFile = new BDMS_SalesOrder().GetPartInvoiceFile(SODeliveryID);
                        // UploadedFile.FileName = "A.pdf";  
                        File.WriteAllBytes(tempfilenameandlocation, UploadedFile.AttachedFile);
                        //  Response.Redirect("../PDF.aspx?FileName=" + FileName + "&Title=Pre-Sales » Quotation", false);
                    }
                    Context.Response.Write("<script language='javascript'>window.open('../PDF.aspx?FileName=" + FileName + "&Title=Sales » Sales Order Delivery','_newtab');</script>");
                }
                catch (Exception e1)
                {
                    lblMessage.Text = e1.Message;
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }
        public string ValidationEInvoice(string Gstin,string Pin,string Stcd)
        {
            try
            {
                if (string.IsNullOrEmpty(Gstin))
                {
                    return "Please update Buyer GST Number";
                }
                String regexS = "^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexS);
                if (regex.Match(Gstin).Success)
                {
                    if (Gstin.Trim().Substring(0, 2) != Stcd.Trim())
                    {
                        return "Please verify the GST number with State";
                    }
                }
                else
                {
                    return "Please update correct GST Number";
                } 
                if (!new BDMS_EInvoice().ValidatePincode(Pin.Substring(0, 2), Stcd))
                {
                    return "Please check Buyer Pincode and Statecode";
                } 
            }
            catch (Exception e)
            { }
            return "";
        }
        void ViewSalesDeliveryChallan()
        {
            try
            {
                string mimeType = string.Empty;
                Byte[] mybytes = SalesDeliveryChallanRdlc(out mimeType);
                string FileName = SaleOrderDeliveryByID.DeliveryNumber + ".pdf";
                var uploadPath = Server.MapPath("~/Backup");
                var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                File.WriteAllBytes(tempfilenameandlocation, mybytes);
                Context.Response.Write("<script language='javascript'>window.open('../PDF.aspx?FileName=" + FileName + "&Title=Sales » Sales Order Delivery','_newtab');</script>");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        Byte[] SalesDeliveryChallanRdlc(out string mimeType)
        {
            string extension;
            string encoding;
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;

            PDMS_Dealer Dealer = new BDealer().GetDealerAddress(SaleOrderDeliveryByID.SaleOrder.Dealer.DealerID)[0];
            //string DealerCustomerAddress1 = (Dealer.Address.Address1 + (string.IsNullOrEmpty(Dealer.Address.Address2) ? "" : "," + Dealer.Address.Address2) + (string.IsNullOrEmpty(Dealer.Address.Address3) ? "" : "," + Dealer.Address.Address3)).Trim(',', ' ');
            //string DealerCustomerAddress2 = (Dealer.Address.City + (string.IsNullOrEmpty(Dealer.Address.State.State) ? "" : "," + Dealer.Address.State.State) + (string.IsNullOrEmpty(Dealer.Address.Pincode) ? "" : "-" + Dealer.Address.Pincode)).Trim(',', ' ');


            PDMS_DealerOffice DealerOffice = new BDMS_Dealer().GetDealerOffice(null, SaleOrderDeliveryByID.SaleOrder.Dealer.DealerOffice.OfficeID, null)[0];

            string DealerCustomerAddress1 = (DealerOffice.Address1 + (string.IsNullOrEmpty(DealerOffice.Address2) ? "" : "," + DealerOffice.Address2) + (string.IsNullOrEmpty(DealerOffice.Address3) ? "" : "," + DealerOffice.Address3)).Trim(',', ' ');
            string DealerCustomerAddress2 = (DealerOffice.City + (string.IsNullOrEmpty(DealerOffice.State) ? "" : "," + DealerOffice.State) + (string.IsNullOrEmpty(DealerOffice.Pincode) ? "" : "-" + DealerOffice.Pincode)).Trim(',', ' ');

            PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(SaleOrderDeliveryByID.SaleOrder.Customer.CustomerID);
            string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
            string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

            ReportParameter[] P = new ReportParameter[12];
            P[0] = new ReportParameter("CompanyName", Dealer.DealerName.ToUpper(), false);
            P[1] = new ReportParameter("CompanyAddress1", DealerCustomerAddress1, false);
            P[2] = new ReportParameter("CompanyAddress2", DealerCustomerAddress2, false);
            P[3] = new ReportParameter("DeliveryLocation", DealerOffice.State, false);
            P[4] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
            P[5] = new ReportParameter("CustomerName", Customer.CustomerName, false);
            P[6] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
            P[7] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
            P[8] = new ReportParameter("DeliveryNo", SaleOrderDeliveryByID.DeliveryNumber, false);
            P[9] = new ReportParameter("DeliveryDate", SaleOrderDeliveryByID.DeliveryDate.ToString(), false);
            P[11] = new ReportParameter("DeliveryAddress", SaleOrderDeliveryByID.ShippingAddress, false);
            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("ItemNo");
            dtItem.Columns.Add("PartNo");
            dtItem.Columns.Add("Description");
            dtItem.Columns.Add("HSN");
            dtItem.Columns.Add("Qty");
            dtItem.Columns.Add("UOM");
            dtItem.Columns.Add("UnitPrice");
            dtItem.Columns.Add("Tax");
            dtItem.Columns.Add("Value");

            decimal TotalValue = 0;

            int sno = 0;
            foreach (PSaleOrderDeliveryItem Item in SaleOrderDeliveryByID.SaleOrderDeliveryItems)
            {
                dtItem.Rows.Add(sno += 1, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Material.HSN, Item.Qty.ToString("0"), Item.Material.BaseUnit, String.Format("{0:n}", Item.Value), String.Format("{0:n}", Item.SGST + Item.CGST + Item.IGST), String.Format("{0:n}", (Item.TaxableValue + (Item.SGSTValue + Item.CGSTValue + Item.IGSTValue))));
                TotalValue += (Item.TaxableValue + (Item.SGSTValue + Item.CGSTValue + Item.IGSTValue));
            }
            if (SaleOrderDeliveryByID.Freight != 0)
            {
                decimal TaxValue = SaleOrderDeliveryByID.Freight * 18 / 100;
                dtItem.Rows.Add(sno += 1, "Freight", "Freight Charges", "998719", "", "LE", String.Format("{0:n}", SaleOrderDeliveryByID.Freight)
                    , String.Format("{0:n}", 18), String.Format("{0:n}", TaxValue), SaleOrderDeliveryByID.Freight + TaxValue);
                TotalValue += SaleOrderDeliveryByID.Freight + TaxValue;
            }
            if (SaleOrderDeliveryByID.PackingAndForward != 0)
            {
                decimal TaxValue = SaleOrderDeliveryByID.Freight * 18 / 100;
                dtItem.Rows.Add(sno += 1, "Packing", "Packing Charges", "998719", "", "LE", String.Format("{0:n}", SaleOrderDeliveryByID.Freight)
                    , String.Format("{0:n}", 18), String.Format("{0:n}", TaxValue), SaleOrderDeliveryByID.Freight + TaxValue);
                TotalValue += SaleOrderDeliveryByID.Freight + TaxValue; 
            }

            P[10] = new ReportParameter("TotalValue", String.Format("{0:n}", TotalValue), false);
            report.ReportPath = Server.MapPath("~/Print/SalesDeliveryChallan.rdlc");
            report.SetParameters(P);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "SalesDeliveryChallan";//This refers to the dataset name in the RDLC file  
            rds.Value = dtItem;
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

            return mybytes;
        }
        void DownloadSalesDeliveryChallan()
        {
            try
            {
                string contentType = string.Empty;
                contentType = "application/pdf";
                string FileName = SaleOrderDeliveryByID.DeliveryNumber + ".pdf";
                string mimeType;
                Byte[] mybytes = SalesDeliveryChallanRdlc(out mimeType);
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
        public void fillViewSODelivery(long SaleOrderDeliveryID)
        {
            SaleOrderDeliveryByID = new BDMS_SalesOrder().GetSaleOrderDeliveryByID(SaleOrderDeliveryID);
            SODeliveryID = SaleOrderDeliveryID;
            lblDeliveryNumber.Text = SaleOrderDeliveryByID.DeliveryNumber;
            lblDeliveryDate.Text = SaleOrderDeliveryByID.DeliveryDate.ToShortDateString();
            lblInvoiceNumber.Text = SaleOrderDeliveryByID.InvoiceNumber;
            lblInvoiceDate.Text = SaleOrderDeliveryByID.InvoiceDate == null ? "" : ((DateTime)SaleOrderDeliveryByID.InvoiceDate).ToShortDateString();
            lblDealer.Text = SaleOrderDeliveryByID.SaleOrder.Dealer.DealerCode + " " + SaleOrderDeliveryByID.SaleOrder.Dealer.DealerName;
            lblDealerOffice.Text = SaleOrderDeliveryByID.SaleOrder.Dealer.DealerOffice.OfficeName;
            lblDivision.Text = SaleOrderDeliveryByID.SaleOrder.Division.DivisionCode;
            lblSaleOrderType.Text = SaleOrderDeliveryByID.SaleOrder.SaleOrderType.SaleOrderType;
            lblDeliveryOrderStatus.Text = SaleOrderDeliveryByID.Status.Status;
            lblCustomer.Text = SaleOrderDeliveryByID.SaleOrder.Customer.CustomerCode + " " + SaleOrderDeliveryByID.SaleOrder.Customer.CustomerName;
            lblEquipment.Text = SaleOrderDeliveryByID.Equipment.EquipmentSerialNo;
            lblPaymentMode.Text = SaleOrderDeliveryByID.PaymentMode == null ? "" : SaleOrderDeliveryByID.PaymentMode.Status;
            lblTcsTax.Text = Convert.ToString(SaleOrderDeliveryByID.TCSTax);
            lblTcsValue.Text = Convert.ToString(SaleOrderDeliveryByID.TCSValue);
            decimal Discount = 0, TaxableValue = 0, TaxValue = 0, NetAmount = 0;
            foreach (PSaleOrderDeliveryItem DeliveryItem in SaleOrderDeliveryByID.SaleOrderDeliveryItems)
            {
                Discount = Discount + + DeliveryItem.DiscountValue;
                TaxableValue = TaxableValue + DeliveryItem.TaxableValue;
                TaxValue = TaxValue + DeliveryItem.CGSTValue + DeliveryItem.SGSTValue + DeliveryItem.IGSTValue;
                NetAmount = NetAmount + DeliveryItem.TaxableValue + DeliveryItem.CGSTValue + DeliveryItem.SGSTValue + DeliveryItem.IGSTValue;
                DeliveryItem.SaleOrderItem.NetAmount = DeliveryItem.TaxableValue + DeliveryItem.CGSTValue + DeliveryItem.SGSTValue + DeliveryItem.IGSTValue;
            }

            lblDiscount.Text = Discount.ToString();
            lblTaxableValue.Text = TaxableValue.ToString();
            lblTaxValue.Text = TaxValue.ToString();
            lblNetAmount.Text = NetAmount.ToString();
            lblNetAmountWithTCS.Text = (NetAmount + SaleOrderDeliveryByID.TCSValue).ToString();

            gvSODeliveryItem.DataSource = SaleOrderDeliveryByID.SaleOrderDeliveryItems;
            gvSODeliveryItem.DataBind();

            lblQuotationNumber.Text = SaleOrderDeliveryByID.SaleOrder.QuotationNumber;
            lblQuotationDate.Text = SaleOrderDeliveryByID.SaleOrder.QuotationDate.ToShortDateString();
            lblSaleOrderNumber.Text = SaleOrderDeliveryByID.SaleOrder.SaleOrderNumber;
            lblSaleOrderDate.Text = (SaleOrderDeliveryByID.SaleOrder.SaleOrderDate == null) ? null : ((DateTime)SaleOrderDeliveryByID.SaleOrder.SaleOrderDate).ToShortDateString();
            lblProformaInvoiceNumber.Text = SaleOrderDeliveryByID.SaleOrder.ProformaInvoiceNumber;
            lblProformaInvoiceDate.Text = (SaleOrderDeliveryByID.SaleOrder.ProformaInvoiceDate == null) ? null : ((DateTime)SaleOrderDeliveryByID.SaleOrder.ProformaInvoiceDate).ToShortDateString();
            lblSOExpectedDeliveryDate.Text = SaleOrderDeliveryByID.SaleOrder.ExpectedDeliveryDate.ToShortDateString();
            lblRefNumber.Text = SaleOrderDeliveryByID.SaleOrder.RefNumber;
            lblRefDate.Text = (SaleOrderDeliveryByID.SaleOrder.RefDate == null) ? "" : ((DateTime)SaleOrderDeliveryByID.SaleOrder.RefDate).ToShortDateString();
            lblContactPersonNumber.Text = SaleOrderDeliveryByID.SaleOrder.ContactPersonNumber;
            lblProduct.Text = SaleOrderDeliveryByID.SaleOrder.Product == null ? "" : SaleOrderDeliveryByID.SaleOrder.Product.Product;
            lblEquipmentSerialNo.Text = SaleOrderDeliveryByID.Equipment.EquipmentSerialNo;
            lblFrieghtPaidBy.Text = SaleOrderDeliveryByID.SaleOrder.FrieghtPaidBy;
            lblInsurancePaidBy.Text = SaleOrderDeliveryByID.SaleOrder.InsurancePaidBy;
            lblRemarks.Text = SaleOrderDeliveryByID.SaleOrder.Remarks;
            lblAttn.Text = SaleOrderDeliveryByID.SaleOrder.Attn;
            lblSalesEngnieer.Text = (SaleOrderDeliveryByID.SaleOrder.SalesEngineer == null) ? "" : SaleOrderDeliveryByID.SaleOrder.SalesEngineer.ContactName;
            lblTaxType.Text = SaleOrderDeliveryByID.SaleOrder.TaxType;
            lblHeaderDiscountPercent.Text = SaleOrderDeliveryByID.SaleOrder.HeaderDiscountPercentage.ToString();
            lblGrossAmount.Text = SaleOrderDeliveryByID.SaleOrder.GrossAmount.ToString();

            gvSOItem.DataSource = null;
            gvSOItem.DataBind();
            List<PSaleOrderItem> saleOrderItems = new BDMS_SalesOrder().GetSaleOrderItemByDeliveryID(SaleOrderDeliveryByID.SaleOrderDeliveryID);
            gvSOItem.DataSource = saleOrderItems;
            gvSOItem.DataBind();

            SaleOrderDeliveryByID.SaleOrder.Customer = new BDMS_Customer().GetCustomerByID(SaleOrderDeliveryByID.SaleOrder.Customer.CustomerID);
            UC_CustomerView.fillCustomer(SaleOrderDeliveryByID.SaleOrder.Customer);

            lblBillingAddress.Text = SaleOrderDeliveryByID.SaleOrder.Customer.Address1 + ","
             + SaleOrderDeliveryByID.SaleOrder.Customer.Address2 + ","
             + SaleOrderDeliveryByID.SaleOrder.Customer.Address3 + ","
             + SaleOrderDeliveryByID.SaleOrder.Customer.District.District + ","
             + SaleOrderDeliveryByID.SaleOrder.Customer.State.State + ","
             + SaleOrderDeliveryByID.SaleOrder.Customer.Pincode;

            lblDeliveryAddress.Text = SaleOrderDeliveryByID.ShippingAddress;

            lblShippingRemarks.Text = SaleOrderDeliveryByID.Remarks.ToString();
            //if (SaleOrderDeliveryByID.SaleOrder.ShipTo != null)
            //{
            //    List<PDMS_CustomerShipTo> ShipTos = new BDMS_Customer().GetCustomerShopTo(SaleOrderDeliveryByID.SaleOrder.ShipTo.CustomerShipToID, SaleOrderDeliveryByID.SaleOrder.Customer.CustomerID);
            //    lblDeliveryAddress.Text = ShipTos[0].Address1 + ","
            //        + ShipTos[0].Address2 + ","
            //        + ShipTos[0].Address3 + ","
            //        + ShipTos[0].District.District + ","
            //        + ShipTos[0].State.State + ","
            //        + ShipTos[0].Pincode;
            //}
            //else
            //{
            //    lblDeliveryAddress.Text = lblBillingAddress.Text;
            //}

            if (SaleOrderDeliveryByID.Shipping != null)
            {
                lblNetWeight.Text = SaleOrderDeliveryByID.Shipping.NetWeight.ToString();
                lblDispatchDate.Text = SaleOrderDeliveryByID.Shipping.DispatchDate.ToShortDateString();
                lblCourierID.Text = SaleOrderDeliveryByID.Shipping.CourierID.ToString();
                lblCourierDate.Text = SaleOrderDeliveryByID.Shipping.CourierDate.ToShortDateString();
                lblCourierCompanyName.Text = SaleOrderDeliveryByID.Shipping.CourierCompanyName.ToString();
                lblCourierPerson.Text = SaleOrderDeliveryByID.Shipping.CourierPerson.ToString();
                lblLRNo.Text = SaleOrderDeliveryByID.Shipping.LRNo.ToString();
                lblPackingDescription.Text = SaleOrderDeliveryByID.Shipping.PackingDescription.ToString();
                lblPackingRemarks.Text = SaleOrderDeliveryByID.Shipping.PackingRemarks.ToString();
                lblTransportDetails.Text = SaleOrderDeliveryByID.Shipping.TransportDetails.ToString();
                lblTransportMode.Text = SaleOrderDeliveryByID.Shipping.TransportMode.ToString();
                lblPickupDate.Text = SaleOrderDeliveryByID.Shipping.PickupDate.ToShortDateString(); 
            }
            ActionControlMange(SaleOrderDeliveryByID);
        }
        void ActionControlMange(PSaleOrderDelivery SaleOrderDeliveryByID)
        {
            lbGenerateInvoice.Visible = true;
            lbUpdateShippingDetails.Visible = true;
            lbPreviewInvoice.Visible = true;
            lbDowloadInvoice.Visible = true;
            if (SaleOrderDeliveryByID.SaleOrder.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.WarrantyOrder)
            {
                lbGenerateInvoice.Visible = false;
                lbPreviewInvoice.Visible = false;
                lbDowloadInvoice.Visible = false;
            }

            if (SaleOrderDeliveryByID.Status.StatusID == (short)AjaxOneStatus.SaleOrderDelivery_InvoicePending)
            {
                if (SaleOrderDeliveryByID.SaleOrder.SaleOrderType.SaleOrderTypeID != (short)SaleOrderType.WarrantyOrder)
                {
                    lbUpdateShippingDetails.Visible = false;
                }
                lbPreviewInvoice.Visible = false;
                lbDowloadInvoice.Visible = false;
            }
            else if (SaleOrderDeliveryByID.Status.StatusID == (short)AjaxOneStatus.SaleOrderDelivery_Invoiced)
            {
                lbGenerateInvoice.Visible = false;
            }
            else if (SaleOrderDeliveryByID.Status.StatusID == (short)AjaxOneStatus.SaleOrderDelivery_Shipped)
            {
                lbGenerateInvoice.Visible = false;
                lbUpdateShippingDetails.Visible = false;
            }

        }
        void ibPDF_Click()
        {
            try
            {
                PAttachedFile UploadedFile = new BDMS_SalesOrder().GetPartInvoiceFile(SODeliveryID);
                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lblInvoiceNumber.Text + ".pdf");
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                new BXcel().PdfDowload();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnSaveShipping_Click(object sender, EventArgs e)
        {
            lblMessageCreateSODelivery.ForeColor = Color.Red;
            MPE_Delivery.Show();
            try
            {
                Shipping_Insert.SaleOrderDeliveryID = SODeliveryID;
                Shipping_Insert.NetWeight = Convert.ToDecimal("0" + txtBoxNetWeight.Text);
                
                Shipping_Insert.DispatchDate = Convert.ToDateTime(txtBoxDispatchDate.Text.Trim());
                Shipping_Insert.CourierID = txtBoxCourierId.Text.Trim();
                Shipping_Insert.CourierDate = Convert.ToDateTime(txtBoxCourierDate.Text.Trim());
                Shipping_Insert.CourierCompanyName = txtBoxCourierCompanyName.Text.Trim();
                Shipping_Insert.CourierPerson = txtBoxCourierPerson.Text.Trim();
                Shipping_Insert.LRNo = txtBoxLRNo.Text.Trim();
                Shipping_Insert.PackingDescription = txtBoxPackingDescription.Text.Trim();
                Shipping_Insert.PackingRemarks = txtBoxPackingRemarks.Text.Trim();
                Shipping_Insert.TransportDetails = txtBoxTransportDetails.Text.Trim();
                Shipping_Insert.TransportMode = ddlTransportMode.SelectedItem.Text;
                Shipping_Insert.PickupDate = Convert.ToDateTime(txtBoxPickupDate.Text.Trim());

                PApiResult Result = new BDMS_SalesOrder().InsertSaleOrderDeliveryShipping(Shipping_Insert);
                if (Result.Status == PApplication.Failure)
                {
                    lblMessageCreateSODelivery.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;

                MPE_Delivery.Hide();
                fillViewSODelivery(SODeliveryID);
            }
            catch (Exception e1)
            {
                lblMessageCreateSODelivery.Text = e1.Message;
            }
        }
        Byte[] SalesMachineInvoiceRdlc(out string mimeType)
        {
            var CC = CultureInfo.CurrentCulture;
            Random r = new Random();
            string extension;
            string encoding;
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;

            PDMS_Dealer Dealer = new BDealer().GetDealerAddress(SaleOrderDeliveryByID.SaleOrder.Dealer.DealerID)[0];

            PDMS_DealerOffice DealerOffice = new BDMS_Dealer().GetDealerOffice(null, SaleOrderDeliveryByID.SaleOrder.Dealer.DealerOffice.OfficeID, null)[0];

            string DealerAddress1 = (DealerOffice.Address1 + (string.IsNullOrEmpty(DealerOffice.Address2) ? "" : "," + DealerOffice.Address2) + (string.IsNullOrEmpty(DealerOffice.Address3) ? "" : "," + DealerOffice.Address3)).Trim(',', ' ');
            string DealerAddress2 = (DealerOffice.City + (string.IsNullOrEmpty(DealerOffice.State) ? "" : "," + DealerOffice.State) + (string.IsNullOrEmpty(DealerOffice.Pincode) ? "" : "-" + DealerOffice.Pincode)).Trim(',', ' ');

            PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(SaleOrderDeliveryByID.SaleOrder.Customer.CustomerID);
            string CustomerAddress = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
              CustomerAddress = CustomerAddress+"," + (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

          //  string  CustomerShipToAddress = SaleOrderDeliveryByID.ShippingAddress;
            string ShippingAddress = string.IsNullOrEmpty(SaleOrderDeliveryByID.ShippingAddress.Trim()) ? CustomerAddress : SaleOrderDeliveryByID.ShippingAddress.Trim();

            //if (SaleOrderDeliveryByID.SaleOrder.ShipTo != null)
            //{
            //    List<PDMS_CustomerShipTo> CustomerShipTo = new BDMS_Customer().GetCustomerShopTo(SaleOrderDeliveryByID.SaleOrder.ShipTo.CustomerShipToID, SaleOrderDeliveryByID.SaleOrder.Customer.CustomerID);
            //    CustomerShipToCode = CustomerShipTo[0].CustomerCode;
            //    CustomerShipToAddress1 = (CustomerShipTo[0].Address1 + (string.IsNullOrEmpty(CustomerShipTo[0].Address2) ? "" : "," + CustomerShipTo[0].Address2) + (string.IsNullOrEmpty(CustomerShipTo[0].Address3) ? "" : "," + CustomerShipTo[0].Address3)).Trim(',', ' ');
            //    CustomerShipToAddress2 = (CustomerShipTo[0].City + (string.IsNullOrEmpty(CustomerShipTo[0].State.State) ? "" : "," + CustomerShipTo[0].State.State) + (string.IsNullOrEmpty(CustomerShipTo[0].Pincode) ? "" : "-" + CustomerShipTo[0].Pincode)).Trim(',', ' ');
            //}
            //else
            //{
            //    CustomerShipToCode = Customer.CustomerCode;
            //    CustomerShipToAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address1) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
            //    CustomerShipToAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');
            //}

            PApiResult Result = new BSalesQuotation().GetSalesQuotationBasic(null, SaleOrderDeliveryByID.SaleOrder.QuotationNumber, null, null
               , null, null, null, null, null, null, null, null, null, null, null);

            List<PSalesQuotation> QL = JsonConvert.DeserializeObject<List<PSalesQuotation>>(JsonConvert.SerializeObject(Result.Data));

            PSalesQuotation Q = new BSalesQuotation().GetSalesQuotationByID(QL[0].QuotationID);
            string KindAttention = "", Hypothecation = "", PhoneNumber = "";

            foreach (PSalesQuotationNote Note in Q.Notes)
            {
                if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.KindAttention) { KindAttention = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Hypothecation) { Hypothecation = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.PhoneNumber) { PhoneNumber = Note.Remark; }
            }
            //PDMS_EquipmentHeader E = new BDMS_Equipment().GetEquipmentHeaderByID(Convert.ToInt32(SaleOrderDeliveryByID.Equipment.EquipmentHeaderID));
            ReportParameter[] P = new ReportParameter[43];
            P[0] = new ReportParameter("CompanyName", Dealer.DealerName.ToUpper(), false);
            P[1] = new ReportParameter("CompanyAddress1", DealerAddress1, false);
            P[2] = new ReportParameter("CompanyAddress2", DealerAddress2, false);
            P[3] = new ReportParameter("CompanyTelephoneandEmail", "T:" + Dealer.Address.Mobile + "," + Environment.NewLine + "Email:" + Dealer.Address.Email);
            P[4] = new ReportParameter("CompanyPAN", Dealer.Address.PAN, false);
            P[5] = new ReportParameter("CompanyGSTIN", Dealer.Address.GSTIN, false);
            P[6] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
            P[7] = new ReportParameter("CustomerName", Customer.CustomerName, false);
            P[8] = new ReportParameter("CustomerAddress", CustomerAddress, false);
            P[9] = new ReportParameter("CustomerAddress2", "", false);
            P[10] = new ReportParameter("Hypothecation", Hypothecation, false);
            P[11] = new ReportParameter("CustomerShipToCode", Customer.CustomerCode, false);
            P[12] = new ReportParameter("CustomerShipToName", Customer.CustomerName, false);
            P[13] = new ReportParameter("ShippingAddress", ShippingAddress, false);
            P[14] = new ReportParameter("CustomerShipToAddress2", "", false);
            P[15] = new ReportParameter("CustomerShipToPAN", Customer.PAN, false);
            P[16] = new ReportParameter("CustomerShipToGSTIN", Customer.GSTIN, false);
            P[17] = new ReportParameter("InvoiceNo", SaleOrderDeliveryByID.InvoiceNumber, false);
            P[18] = new ReportParameter("InvoiceDate", (SaleOrderDeliveryByID.InvoiceDate == null) ? "" : Convert.ToDateTime(SaleOrderDeliveryByID.InvoiceDate).ToShortDateString(), false);
            P[19] = new ReportParameter("Attn", Customer.ContactPerson, false);
            P[20] = new ReportParameter("Mobile", Customer.Mobile, false);
            P[21] = new ReportParameter("Ref", SaleOrderDeliveryByID.SaleOrder.RefNumber, false);
            P[22] = new ReportParameter("CGST_Header", "", false);
            P[23] = new ReportParameter("CGSTVal_Header", "", false);
            P[24] = new ReportParameter("SGST_Header", "", false);
            P[25] = new ReportParameter("SGSTVal_Header", "", false);
            P[26] = new ReportParameter("DateOfPreparationOfInvoice", (SaleOrderDeliveryByID.InvoiceDate == null) ? "" : Convert.ToDateTime(SaleOrderDeliveryByID.InvoiceDate).ToShortDateString(), false);
            P[27] = new ReportParameter("DateOfRemovalOfGoods", (SaleOrderDeliveryByID.InvoiceDate == null) ? "" : Convert.ToDateTime(SaleOrderDeliveryByID.InvoiceDate).ToShortDateString(), false);
            //P[28] = new ReportParameter("MannerOfTransport", SaleOrderDeliveryByID.Shipping.TransportDetails, false);
            P[28] = new ReportParameter("MannerOfTransport", "", false);
            P[29] = new ReportParameter("Destination", SaleOrderDeliveryByID.SaleOrder.Dealer.DealerOffice.OfficeName, false);
            P[30] = new ReportParameter("SubTotal", "", false);
            P[31] = new ReportParameter("TCSTotal", "", false);
            P[32] = new ReportParameter("GrandTotal", "", false);
            P[33] = new ReportParameter("GrandTotalInwords", "", false);
            P[34] = new ReportParameter("Model", Q.LeadProduct.Product.Product, false);
            P[35] = new ReportParameter("MachineSlno", SaleOrderDeliveryByID.Equipment.EquipmentSerialNo, false);
            P[36] = new ReportParameter("EngineNo", SaleOrderDeliveryByID.Equipment.EngineSerialNo, false);
            P[37] = new ReportParameter("ChassisNo", SaleOrderDeliveryByID.Equipment.ChassisSlNo, false);
            P[38] = new ReportParameter("Remarks", "", false);
            P[39] = new ReportParameter("DeliveryNo", SaleOrderDeliveryByID.DeliveryNumber, false);
            P[40] = new ReportParameter("IRNo", "", false);
            P[41] = new ReportParameter("TCSTaxPer", "", false);
            P[42] = new ReportParameter("QRCodeImg", "", false);

            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("Sn");
            dtItem.Columns.Add("PartNo");
            dtItem.Columns.Add("Description");
            dtItem.Columns.Add("HSN");
            dtItem.Columns.Add("Qty");
            dtItem.Columns.Add("UOM");
            dtItem.Columns.Add("UnitPrice");
            dtItem.Columns.Add("TotalValue");
            dtItem.Columns.Add("Discount");
            dtItem.Columns.Add("Taxable");
            dtItem.Columns.Add("CGSTPer");
            dtItem.Columns.Add("CGSTVal");
            dtItem.Columns.Add("SGSTPer");
            dtItem.Columns.Add("SGSTVal");

            int sno = 0;
            decimal SubTotal = 0, GrandTotal = 0;
            foreach (PSaleOrderDeliveryItem Item in SaleOrderDeliveryByID.SaleOrderDeliveryItems)
            {
                if (Item.IGST == 0)
                {
                    dtItem.Rows.Add(sno += 1, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Material.HSN, Item.Qty.ToString("0"), Item.Material.BaseUnit, String.Format("{0:n}", Item.Value / Item.Qty), String.Format("{0:n}", Item.Value), String.Format("{0:n}", Item.DiscountValue), String.Format("{0:n}", Item.TaxableValue), String.Format("{0:n}", Item.CGST), String.Format("{0:n}", Item.CGSTValue), String.Format("{0:n}", Item.SGST), String.Format("{0:n}", Item.SGSTValue));
                    SubTotal += (Item.TaxableValue + Item.CGSTValue + Item.SGSTValue);
                    P[22] = new ReportParameter("CGST_Header", "%", false);
                    P[23] = new ReportParameter("CGSTVal_Header", "CGST", false);
                    P[24] = new ReportParameter("SGST_Header", "%", false);
                    P[25] = new ReportParameter("SGSTVal_Header", "SGST", false);
                }
                else
                {
                    dtItem.Rows.Add(sno += 1, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Material.HSN, Item.Qty.ToString("0"), Item.Material.BaseUnit, String.Format("{0:n}", Item.Value / Item.Qty), String.Format("{0:n}", Item.Value), String.Format("{0:n}", Item.DiscountValue), String.Format("{0:n}", Item.TaxableValue), "", "", String.Format("{0:n}", Item.IGST), String.Format("{0:n}", Item.IGSTValue));
                    SubTotal += (Item.TaxableValue + Item.IGSTValue);
                    P[22] = new ReportParameter("CGST_Header", "", false);
                    P[23] = new ReportParameter("CGSTVal_Header", "", false);
                    P[24] = new ReportParameter("SGST_Header", "%", false);
                    P[25] = new ReportParameter("SGSTVal_Header", "IGST", false);
                }
            }
            GrandTotal = Math.Round(SubTotal + SaleOrderDeliveryByID.TCSValue);
            P[30] = new ReportParameter("SubTotal", String.Format("{0:n}", SubTotal), false);
            P[31] = new ReportParameter("TCSTotal", String.Format("{0:n}", SaleOrderDeliveryByID.TCSValue), false);
            P[32] = new ReportParameter("GrandTotal", String.Format("{0:n}", GrandTotal), false);
            P[33] = new ReportParameter("GrandTotalInwords", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
            P[41] = new ReportParameter("TCSTaxPer", String.Format("{0:n}", SaleOrderDeliveryByID.TCSTax), false);
            PDMS_Dealer DealerN = new BDMS_Dealer().GetDealer(SaleOrderDeliveryByID.SaleOrder.Dealer.DealerID, null, null, null)[0];
            if ((DealerN.ServicePaidEInvoice) && (DealerN.EInvoiceDate <= SaleOrderDeliveryByID.InvoiceDate) && (Customer.GSTIN != "URD"))
            {
                PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().GetSaleOrderDeliveryInvoiceESigned(SaleOrderDeliveryByID.SaleOrderDeliveryID);
                if (EInvoiceSigned != null)
                {
                    if (string.IsNullOrEmpty(EInvoiceSigned.SignedQRCode))
                    {
                        throw new Exception("E Invoice not generated.: " + EInvoiceSigned.Comments);
                    }
                }
                if (string.IsNullOrEmpty(SaleOrderDeliveryByID.IRN))
                {
                    throw new Exception("E Invoice not generated. Please contact IT Team.");
                } 
                else
                {
                    P[40] = new ReportParameter("IRNo", "IRN : " + SaleOrderDeliveryByID.IRN, false);
                    P[42] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, SaleOrderDeliveryByID.InvoiceNumber), false); 
                }

            }
            else
            {
                P[40] = new ReportParameter("IRNo", "", false);
                P[42] = new ReportParameter("QRCodeImg", "", false);
            }


            report.ReportPath = Server.MapPath("~/Print/SalesMachineInvoice.rdlc");
            report.SetParameters(P);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "SalesMachineInvoice";//This refers to the dataset name in the RDLC file  
            rds.Value = dtItem;
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF 
            return mybytes;
        }
        void DownloadSalesMachineInvoice()
        {
            try
            {
                string contentType = string.Empty;
                contentType = "application/pdf";
                string FileName = SaleOrderDeliveryByID.InvoiceNumber + ".pdf";
                string mimeType;
                Byte[] mybytes = SalesMachineInvoiceRdlc(out mimeType);
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
    }
}