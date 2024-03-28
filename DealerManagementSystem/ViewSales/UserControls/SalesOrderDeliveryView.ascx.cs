using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            if (lbActions.ID == "lbGenerateInvoice")
            {
                PApiResult Results = new BDMS_SalesOrder().GenerateSaleInvoice(SODeliveryID);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Green;
                fillViewSODelivery(SODeliveryID);
            }
            else if (lbActions.ID == "lbDowloadInvoice")
            {
                ibPDF_Click();
            }
            else if (lbActions.ID == "lbDelivery")
            {
                MPE_Delivery.Show(); 
                cxDispatchDate.StartDate = DateTime.Now;
                txtBoxDispatchDate.Text = DateTime.Now.ToShortDateString();

                cxCourierDate.StartDate = DateTime.Now;
                txtBoxCourierDate.Text = DateTime.Now.ToShortDateString();

                cxPickupDate.StartDate = DateTime.Now;
                txtBoxPickupDate.Text = DateTime.Now.ToShortDateString();

            }
        }
        public void fillViewSODelivery(long SaleOrderDeliveryID)
        {
            PSaleOrderDelivery SaleOrderDelivery = new BDMS_SalesOrder().GetSaleOrderDeliveryByID(SaleOrderDeliveryID);
            SODeliveryID = SaleOrderDeliveryID;
            lblDeliveryNumber.Text = SaleOrderDelivery.DeliveryNumber;
            lblDeliveryDate.Text = SaleOrderDelivery.DeliveryDate.ToString("dd/MM/yyyy");
            lblInvoiceNumber.Text = SaleOrderDelivery.InvoiceNumber;
            lblInvoiceDate.Text = SaleOrderDelivery.InvoiceDate.ToString();
            lblDealer.Text = SaleOrderDelivery.SaleOrder.Dealer.DealerCode + " " + SaleOrderDelivery.SaleOrder.Dealer.DealerName;
            lblDealerOffice.Text = SaleOrderDelivery.SaleOrder.Dealer.DealerOffice.OfficeName;
            lblDivision.Text = SaleOrderDelivery.SaleOrder.Division.DivisionCode;
            lblCustomer.Text = SaleOrderDelivery.SaleOrder.Customer.CustomerCode + " " + SaleOrderDelivery.SaleOrder.Customer.CustomerName;
            lblSaleOrderType.Text = SaleOrderDelivery.SaleOrder.SaleOrderType.SaleOrderType;
            lblEquipment.Text = SaleOrderDelivery.Equipment.EquipmentSerialNo;
            decimal Value = 0, TaxableValue = 0, TaxValue = 0, NetAmount = 0;
            foreach (PSaleOrderDeliveryItem DeliveryItem in SaleOrderDelivery.SaleOrderDeliveryItems)
            {
                Value = Value + DeliveryItem.Value;
                TaxableValue = TaxableValue + DeliveryItem.TaxableValue;
                TaxValue = TaxValue + DeliveryItem.CGSTValue + DeliveryItem.SGSTValue + DeliveryItem.IGSTValue;
                NetAmount = NetAmount + DeliveryItem.TaxableValue + DeliveryItem.CGSTValue + DeliveryItem.SGSTValue + DeliveryItem.IGSTValue;
                DeliveryItem.SaleOrderItem.NetAmount = DeliveryItem.TaxableValue + DeliveryItem.CGSTValue + DeliveryItem.SGSTValue + DeliveryItem.IGSTValue;
            }

            lblValue.Text = Value.ToString();
            lblTaxableValue.Text = TaxableValue.ToString();
            lblTaxValue.Text = TaxValue.ToString();
            lblNetAmount.Text = NetAmount.ToString();

            gvSODeliveryItem.DataSource = SaleOrderDelivery.SaleOrderDeliveryItems;
            gvSODeliveryItem.DataBind();

            lblQuotationNumber.Text = SaleOrderDelivery.SaleOrder.QuotationNumber;
            lblQuotationDate.Text = SaleOrderDelivery.SaleOrder.QuotationDate.ToString("dd/MM/yyyy");
            lblSaleOrderNumber.Text = SaleOrderDelivery.SaleOrder.SaleOrderNumber;
            lblSaleOrderDate.Text = (SaleOrderDelivery.SaleOrder.SaleOrderDate == null) ? null : Convert.ToDateTime(SaleOrderDelivery.SaleOrder.SaleOrderDate).ToString("dd/MM/yyyy");
            lblProformaInvoiceNumber.Text = SaleOrderDelivery.SaleOrder.ProformaInvoiceNumber;
            lblProformaInvoiceDate.Text = (SaleOrderDelivery.SaleOrder.ProformaInvoiceDate == null) ? null : Convert.ToDateTime(SaleOrderDelivery.SaleOrder.ProformaInvoiceDate).ToString("dd/MM/yyy");
            lblSOExpectedDeliveryDate.Text = SaleOrderDelivery.SaleOrder.ExpectedDeliveryDate.ToString("dd/MM/yyyy");
            lblRefNumber.Text = SaleOrderDelivery.SaleOrder.RefNumber;
            lblRefDate.Text = SaleOrderDelivery.SaleOrder.RefDate == null ? "" : Convert.ToDateTime(SaleOrderDelivery.SaleOrder.RefDate).ToString("dd/MM/yyyy");
            lblContactPersonNumber.Text = SaleOrderDelivery.SaleOrder.ContactPersonNumber;
            lblProduct.Text = SaleOrderDelivery.SaleOrder.Product == null ? "" : SaleOrderDelivery.SaleOrder.Product.Product;
            lblEquipmentSerialNo.Text = SaleOrderDelivery.Equipment.EquipmentSerialNo;
            lblFrieghtPaidBy.Text = SaleOrderDelivery.SaleOrder.FrieghtPaidBy;
            lblInsurancePaidBy.Text = SaleOrderDelivery.SaleOrder.InsurancePaidBy;
            lblRemarks.Text = SaleOrderDelivery.SaleOrder.Remarks;
            lblAttn.Text = SaleOrderDelivery.SaleOrder.Attn;
            lblSalesEngnieer.Text = SaleOrderDelivery.SaleOrder.SalesEngineer == null?"": SaleOrderDelivery.SaleOrder.SalesEngineer.ContactName;
            lblTaxType.Text = SaleOrderDelivery.SaleOrder.TaxType;
            lblHeaderDiscountPercent.Text = SaleOrderDelivery.SaleOrder.HeaderDiscountPercentage.ToString();
            lblGrossAmount.Text = SaleOrderDelivery.SaleOrder.GrossAmount.ToString();

            gvSOItem.DataSource = null;
            gvSOItem.DataBind();
            List<PSaleOrderItem> saleOrderItems = new BDMS_SalesOrder().GetSaleOrderItemByDeliveryID(SaleOrderDelivery.SaleOrderDeliveryID);
            gvSOItem.DataSource = saleOrderItems;
            gvSOItem.DataBind();

            SaleOrderDelivery.SaleOrder.Customer = new BDMS_Customer().GetCustomerByID(SaleOrderDelivery.SaleOrder.Customer.CustomerID);
            UC_CustomerView.fillCustomer(SaleOrderDelivery.SaleOrder.Customer);

            //gvShipTo.DataSource = null;
            //gvShipTo.DataBind();
            //gvShipTo.DataSource = new BDMS_Customer().GetCustomerShopTo(SaleOrderDeliveryByID.SaleOrder.ShipTo.CustomerShipToID, SaleOrderDeliveryByID.SaleOrder.Customer.CustomerID);
            //gvShipTo.DataBind();

            lblBillingAddress.Text = SaleOrderDelivery.SaleOrder.Customer.Address1 + ","
             + SaleOrderDelivery.SaleOrder.Customer.Address2 + ","
             + SaleOrderDelivery.SaleOrder.Customer.Address3 + ","
             + SaleOrderDelivery.SaleOrder.Customer.District.District + ","
             + SaleOrderDelivery.SaleOrder.Customer.State.State + ","
             + SaleOrderDelivery.SaleOrder.Customer.Pincode;

            if (SaleOrderDelivery.SaleOrder.ShipTo != null)
            {
                List<PDMS_CustomerShipTo> ShipTos = new BDMS_Customer().GetCustomerShopTo(SaleOrderDelivery.SaleOrder.ShipTo.CustomerShipToID, SaleOrderDelivery.SaleOrder.Customer.CustomerID);
                lblDeliveryAddress.Text = ShipTos[0].Address1 + ","
                    + ShipTos[0].Address2 + ","
                    + ShipTos[0].Address3 + ","
                    + ShipTos[0].District.District + ","
                    + ShipTos[0].State.State + ","
                    + ShipTos[0].Pincode;
            }
            else
            {
                lblDeliveryAddress.Text = lblBillingAddress.Text;
            }

            ActionControlMange(SaleOrderDelivery);
        }

        void ActionControlMange(PSaleOrderDelivery SaleOrderDelivery)
        {
            lbGenerateInvoice.Visible = true;
            lbUpdateShippingDetails.Visible = true;
            lbPreviewInvoiceDC.Visible = true;
            lbDowloadInvoice.Visible = true;
            if (SaleOrderDelivery.Status.StatusID == (short)AjaxOneStatus.SaleOrderDelivery_InvoicePending)
            {
                if (SaleOrderDelivery.SaleOrder.SaleOrderType.SaleOrderTypeID != (short)SaleOrderType.WarrantyOrder)
                {
                    lbUpdateShippingDetails.Visible = false;
                } 
                else
                {
                    lbGenerateInvoice.Visible = false;
                }
                lbPreviewInvoiceDC.Visible = false;
                lbDowloadInvoice.Visible = false;
            }
            else if (SaleOrderDelivery.Status.StatusID == (short)AjaxOneStatus.SaleOrderDelivery_Invoiced)
            {
                lbGenerateInvoice.Visible = false;
            }
            else if (SaleOrderDelivery.Status.StatusID == (short)AjaxOneStatus.SaleOrderDelivery_Shipped)
            {
                lbGenerateInvoice.Visible = false;
                lbUpdateShippingDetails.Visible = false;
            } 

        }
        void ibPDF_Click()
        {
            try
            { 
                //if ((SoDelivery.SaleOrder.Dealer.IsEInvoice) && (SoDelivery.SaleOrder.Dealer.EInvoiceDate <= SoDelivery.InvoiceDate))
                //{
                //    if (SoDelivery.InvoiceDetails.BuyerGSTIN.Trim() == "URD")
                //    {

                //    }
                //    else if (string.IsNullOrEmpty(SoDelivery.IRN))
                //    {
                //        PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().GetSaleInvoiceESigned(SoDelivery.SaleOrderDeliveryID);
                //        if (!string.IsNullOrEmpty(EInvoiceSigned.Comments))
                //        {
                //            lblMessage.Text = EInvoiceSigned.Comments;
                //        }
                //        else
                //        {
                //            lblMessage.Text = "E Invoice Not generated.";
                //        }

                //        lblMessage.ForeColor = Color.Red;
                //        lblMessage.Visible = true;
                //        return;
                //    }
                //}

                PAttachedFile UploadedFile = new BDMS_SalesOrder().GetServiceInvoiceFile(SODeliveryID); 
                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lblInvoiceNumber.Text + ".pdf" );
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                new BXcel().PdfDowload();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSaveShipping_Click(object sender, EventArgs e)
        {
            lblMessageCreateSODelivery.ForeColor = Color.Red;
            MPE_Delivery.Show();
            try
            {
                Shipping_Insert.NetWeight = Convert.ToDecimal("0"+txtBoxNetWeight.Text);
                Shipping_Insert.Remarks = txtBoxRemarks.Text.Trim();
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
            }
            catch (Exception e1)
            {
                lblMessageCreateSODelivery.Text = e1.Message;
            }
        }

    }
}