using Business;
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
                Value = Value + DeliveryItem.SaleOrderItem.Value;
                TaxableValue = TaxableValue + DeliveryItem.SaleOrderItem.TaxableValue;
                TaxValue = TaxValue + DeliveryItem.SaleOrderItem.Material.CGSTValue + DeliveryItem.SaleOrderItem.Material.SGSTValue + DeliveryItem.SaleOrderItem.Material.IGSTValue;
                NetAmount = NetAmount + DeliveryItem.SaleOrderItem.TaxableValue + DeliveryItem.SaleOrderItem.Material.CGSTValue + DeliveryItem.SaleOrderItem.Material.SGSTValue + DeliveryItem.SaleOrderItem.Material.IGSTValue;
                DeliveryItem.SaleOrderItem.NetAmount = DeliveryItem.SaleOrderItem.TaxableValue + DeliveryItem.SaleOrderItem.Material.CGSTValue + DeliveryItem.SaleOrderItem.Material.SGSTValue + DeliveryItem.SaleOrderItem.Material.IGSTValue;
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
            lblProduct.Text = SaleOrderDelivery.SaleOrder.Product.Product;
            lblEquipmentSerialNo.Text = SaleOrderDelivery.Equipment.EquipmentSerialNo;
            lblFrieghtPaidBy.Text = SaleOrderDelivery.SaleOrder.FrieghtPaidBy;
            lblInsurancePaidBy.Text = SaleOrderDelivery.SaleOrder.InsurancePaidBy;
            lblRemarks.Text = SaleOrderDelivery.SaleOrder.Remarks;
            lblAttn.Text = SaleOrderDelivery.SaleOrder.Attn;
            lblSalesEngnieer.Text = SaleOrderDelivery.SaleOrder.SalesEngineer.ContactName;
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
            if (!string.IsNullOrEmpty(SaleOrderDelivery.InvoiceNumber))
            {
                lbGenerateInvoice.Visible = false;
            }
        }
      
    }
}