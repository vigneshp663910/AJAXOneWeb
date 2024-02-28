using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSales.UserControls
{
    public partial class SalesOrderDeliveryView : System.Web.UI.UserControl
    {
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
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageSODeliveryView.Text = "";
            if (!IsPostBack)
            {
            }
        }
        public void fillViewSODelivery(long SaleOrderDeliveryID)
        {
            SaleOrderDeliveryByID = new BSne().GetSaleOrderDeliveryByID(SaleOrderDeliveryID);

            lblDeliveryNumber.Text = SaleOrderDeliveryByID.DeliveryNumber;
            lblDeliveryDate.Text = SaleOrderDeliveryByID.DeliveryDate.ToString("dd/MM/yyyy");
            lblInvoiceNumber.Text = SaleOrderDeliveryByID.InvoiceNumber;
            lblInvoiceDate.Text = SaleOrderDeliveryByID.InvoiceDate.ToString();
            lblDealer.Text = SaleOrderDeliveryByID.SaleOrder.Dealer.DealerCode + " " + SaleOrderDeliveryByID.SaleOrder.Dealer.DealerName;
            lblDealerOffice.Text = SaleOrderDeliveryByID.SaleOrder.Dealer.DealerOffice.OfficeName;
            lblDivision.Text = SaleOrderDeliveryByID.SaleOrder.Division.DivisionCode;
            lblCustomer.Text = SaleOrderDeliveryByID.SaleOrder.Customer.CustomerCode + " " + SaleOrderDeliveryByID.SaleOrder.Customer.CustomerName;
            lblSaleOrderType.Text = SaleOrderDeliveryByID.SaleOrder.SaleOrderType.SaleOrderType;
            
            decimal Value = 0, TaxableValue = 0, TaxValue = 0, NetAmount = 0;
            foreach (PSaleOrderDeliveryItem DeliveryItem in SaleOrderDeliveryByID.SaleOrderDeliveryItems)
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

            gvSODeliveryItem.DataSource = SaleOrderDeliveryByID.SaleOrderDeliveryItems;
            gvSODeliveryItem.DataBind();

            lblQuotationNumber.Text = SaleOrderDeliveryByID.SaleOrder.QuotationNumber;
            lblQuotationDate.Text = SaleOrderDeliveryByID.SaleOrder.QuotationDate.ToString("dd/MM/yyyy");
            lblSaleOrderNumber.Text = SaleOrderDeliveryByID.SaleOrder.SaleOrderNumber;
            lblSaleOrderDate.Text = (SaleOrderDeliveryByID.SaleOrder.SaleOrderDate == null) ? null : Convert.ToDateTime(SaleOrderDeliveryByID.SaleOrder.SaleOrderDate).ToString("dd/MM/yyyy");
            lblProformaInvoiceNumber.Text = SaleOrderDeliveryByID.SaleOrder.ProformaInvoiceNumber;
            lblProformaInvoiceDate.Text = (SaleOrderDeliveryByID.SaleOrder.ProformaInvoiceDate == null) ? null : Convert.ToDateTime(SaleOrderDeliveryByID.SaleOrder.ProformaInvoiceDate).ToString("dd/MM/yyy");
            lblSOExpectedDeliveryDate.Text = SaleOrderDeliveryByID.SaleOrder.ExpectedDeliveryDate.ToString("dd/MM/yyyy");
            lblRefNumber.Text = SaleOrderDeliveryByID.SaleOrder.RefNumber;
            lblRefDate.Text = Convert.ToDateTime(SaleOrderDeliveryByID.SaleOrder.RefDate).ToString("dd/MM/yyyy");
            lblContactPersonNumber.Text = SaleOrderDeliveryByID.SaleOrder.ContactPersonNumber;
            lblProduct.Text = SaleOrderDeliveryByID.SaleOrder.Product.Product;
            lblEquipmentSerialNo.Text = SaleOrderDeliveryByID.SaleOrder.Equipment.EquipmentSerialNo;
            lblFrieghtPaidBy.Text = SaleOrderDeliveryByID.SaleOrder.FrieghtPaidBy;
            lblInsurancePaidBy.Text = SaleOrderDeliveryByID.SaleOrder.InsurancePaidBy;
            lblRemarks.Text = SaleOrderDeliveryByID.SaleOrder.Remarks;
            lblAttn.Text = SaleOrderDeliveryByID.SaleOrder.Attn;
            lblSalesEngnieer.Text = SaleOrderDeliveryByID.SaleOrder.SalesEngineer.ContactName;
            lblTaxType.Text = SaleOrderDeliveryByID.SaleOrder.TaxType;
            lblHeaderDiscountPercent.Text = SaleOrderDeliveryByID.SaleOrder.HeaderDiscountPercentage.ToString();
            lblGrossAmount.Text = SaleOrderDeliveryByID.SaleOrder.GrossAmount.ToString();

            gvSOItem.DataSource = null;
            gvSOItem.DataBind();
            List<PSaleOrderItem> saleOrderItems = new BSne().GetSaleOrderItemByDeliveryID(SaleOrderDeliveryByID.SaleOrderDeliveryID);
            gvSOItem.DataSource = saleOrderItems;
            gvSOItem.DataBind();

            SaleOrderDeliveryByID.SaleOrder.Customer = new BDMS_Customer().GetCustomerByID(SaleOrderDeliveryByID.SaleOrder.Customer.CustomerID);
            UC_CustomerView.fillCustomer(SaleOrderDeliveryByID.SaleOrder.Customer);

            //gvShipTo.DataSource = null;
            //gvShipTo.DataBind();
            //gvShipTo.DataSource = new BDMS_Customer().GetCustomerShopTo(SaleOrderDeliveryByID.SaleOrder.ShipTo.CustomerShipToID, SaleOrderDeliveryByID.SaleOrder.Customer.CustomerID);
            //gvShipTo.DataBind();

            List<PDMS_CustomerShipTo> ShipTos = new BDMS_Customer().GetCustomerShopTo(SaleOrderDeliveryByID.SaleOrder.ShipTo.CustomerShipToID, SaleOrderDeliveryByID.SaleOrder.Customer.CustomerID);
            
            lblBillingAddress.Text = SaleOrderDeliveryByID.SaleOrder.Customer.Address1 + "," 
                + SaleOrderDeliveryByID.SaleOrder.Customer.Address2 + ","
                + SaleOrderDeliveryByID.SaleOrder.Customer.Address3 + ","
                + SaleOrderDeliveryByID.SaleOrder.Customer.District.District + ","
                + SaleOrderDeliveryByID.SaleOrder.Customer.State.State + ","
                + SaleOrderDeliveryByID.SaleOrder.Customer.Pincode;

            if (SaleOrderDeliveryByID.SaleOrder.ShipTo.CustomerShipToID == 0)
            {
                lblDeliveryAddress.Text = lblBillingAddress.Text;
            }
            else
            {
                lblDeliveryAddress.Text = ShipTos[0].Address1 + ","
                + ShipTos[0].Address2 + ","
                + ShipTos[0].Address3 + ","
                + ShipTos[0].District.District + ","
                + ShipTos[0].State.State + ","
                + ShipTos[0].Pincode;
            }
        }
    }
}