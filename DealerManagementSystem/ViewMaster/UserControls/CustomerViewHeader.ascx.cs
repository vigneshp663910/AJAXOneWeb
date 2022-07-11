using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster.UserControls
{
    public partial class CustomerViewHeader : System.Web.UI.UserControl
    { 
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void fillCustomer(PDMS_Customer Customer)
        {
            if (Customer != null)
            {
                lblCustomer.Text = Customer.CustomerFullName;
                lblContactPerson.Text = Customer.ContactPerson;
                lblMobile.Text = "<a href='tel:" + Customer.Mobile + "'>" + Customer.Mobile + "</a>";
                lblAlternativeMobile.Text = "<a href='tel:" + Customer.AlternativeMobile + "'>" + Customer.AlternativeMobile + "</a>";
                lblEmail.Text = "<a href='mailto:" + Customer.Email + "'>" + Customer.Email + "</a>";
                lblGSTIN.Text = Customer.GSTIN;
                lblPAN.Text = Customer.PAN;

                string Address = Customer.Address1 + ", " + Customer.Address2 + ", " + Customer.District.District + ", " + Customer.State.State;
                lblAddress.Text = Address;

                cbVerified.Checked = Customer.IsVerified;
                cbIsActive.Checked = Customer.IsActive;
                cbOrderBlock.Checked = Customer.OrderBlock;
                cbDeliveryBlock.Checked = Customer.DeliveryBlock;
                cbBillingBlock.Checked = Customer.BillingBlock;
            }
        }
        public void Clear()
        {

            lblCustomer.Text = "";
            lblContactPerson.Text = "";
            lblMobile.Text = "";
            lblAlternativeMobile.Text = "";
            lblEmail.Text = "";
            lblGSTIN.Text = "";
            lblPAN.Text = "";

            lblAddress.Text = "";

            cbVerified.Checked = false;
            cbIsActive.Checked = false;
            cbOrderBlock.Checked = false;
            cbDeliveryBlock.Checked = false;
            cbBillingBlock.Checked = false;
        }
    }
}