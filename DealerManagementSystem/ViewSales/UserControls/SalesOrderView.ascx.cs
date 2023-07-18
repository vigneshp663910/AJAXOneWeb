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
    public partial class SalesOrderView : System.Web.UI.UserControl
    {
        public PSaleOrder SaleOrderByID
        {
            get
            {
                if (ViewState["SaleOrderByID"] == null)
                {
                    ViewState["SaleOrderByID"] = new PSaleOrder();
                }
                return (PSaleOrder)ViewState["SaleOrderByID"];
            }
            set
            {
                ViewState["SaleOrderByID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void fillViewSO(long SaleOrderID)
        {
            SaleOrderByID = new BDMS_SalesOrder().GetSaleOrderByID(SaleOrderID);

            lblSaleOrderNumber.Text = SaleOrderByID.SaleOrderNumber;
            lblDealerOffice.Text = SaleOrderByID.Dealer.DealerOffice.OfficeName;
            lblContactPerson.Text = SaleOrderByID.ContactPerson;
            lblRemarks.Text = SaleOrderByID.Remarks;
            lblRefNumber.Text = SaleOrderByID.RefNumber;
            lblFrieghtPaidBy.Text = SaleOrderByID.FrieghtPaidBy;
            lblSelectTax.Text = SaleOrderByID.SelectTax;
            lblSaleOrderDate.Text = SaleOrderByID.SaleOrderDate.ToString();
            lblCustomer.Text = SaleOrderByID.Customer.CustomerCode + " " + SaleOrderByID.Customer.CustomerName;
            lblContactPersonNumber.Text = SaleOrderByID.ContactPersonNumber;
            lblExpectedDeliveryDate.Text = SaleOrderByID.ExpectedDeliveryDate.ToString();
            lblRefDate.Text = SaleOrderByID.RefDate.ToString();
            lblAttn.Text = SaleOrderByID.Attn;
            lblSODealer.Text = SaleOrderByID.Dealer.DealerCode + " " + SaleOrderByID.Dealer.DealerName;
            lblStatus.Text = SaleOrderByID.SaleOrderStatus.Status;
            lblDivision.Text = SaleOrderByID.Division.DivisionCode;
            lblProduct.Text = SaleOrderByID.Product.Product;
            lblInsurancePaidBy.Text = SaleOrderByID.InsurancePaidBy;
            lblEquipmentSerialNo.Text = SaleOrderByID.EquipmentSerialNo;

            gvSOItem.DataSource = SaleOrderByID.SaleOrderItems;
            gvSOItem.DataBind();
            ActionControlMange();
        }
        void ActionControlMange()
        {
            //lbReleasePO.Visible = true;
            //lbEditPO.Visible = true;
            //lbCancelPO.Visible = true;

            //int StatusID = PurchaseOrder.PurchaseOrderStatus.PurchaseOrderStatusID;
            //if ((StatusID == 2) || (StatusID == 3))
            //{
            //    lbReleasePO.Visible = false;
            //    lbEditPO.Visible = false;
            //}
            //else if ((StatusID == 4) || (StatusID == 5) || (StatusID == 6))
            //{
            //    lbReleasePO.Visible = false;
            //    lbEditPO.Visible = false;
            //    lbCancelPO.Visible = false;
            //}
        }
    }
}