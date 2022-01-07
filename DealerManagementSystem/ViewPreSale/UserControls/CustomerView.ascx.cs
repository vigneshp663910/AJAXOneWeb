using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class CustomerView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }
        public void fillCustomer(long CustomerID)
        {

            PDMS_Customer Customer = new PDMS_Customer();
            Customer = new BDMS_Customer().GetCustomerProspect(CustomerID, "", "", null, null, null, null)[0];

            lblCustomer.Text = (Customer.CustomerCode + " " + Customer.CustomerName).Trim();
            lblContactPerson.Text = Customer.ContactPerson;
            lblMobile.Text = Customer.Mobile;
            lblAlternativeMobile.Text = Customer.AlternativeMobile;
            lblEmail.Text = Customer.Email;
            lblGSTIN.Text = Customer.GSTIN;
            lblPAN.Text = Customer.PAN;

            string Location = Customer.Address1 + ", " + Customer.Address2 + ", " + Customer.District.District + ", " + Customer.State.State;
            lblLocation.Text = Location;

        }
    }
}