using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class CustomerCreate : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {  
                List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                new DDLBind(ddlCountry, Country, "Country", "CountryID");  

                ddlCountry.SelectedValue = "1";

                List<PDMS_State> State = new BDMS_Address().GetState(1, null, null, null);
                new DDLBind(ddlState, State, "State", "StateID");  
            }
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlState, new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null), "District", "DistrictID");
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlTehsil, new BDMS_Address().GetTehsil(Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), null), "Tehsil", "TehsilID");
        }
        public PDMS_Customer ReadCustomer()
        {
            PDMS_Customer Customer = new PDMS_Customer();
            Customer.CustomerName = txtCustomerName.Text.Trim();
            Customer.GSTIN = txtGSTIN.Text.Trim();
            Customer.PAN = txtPAN.Text.Trim();
            Customer.ContactPerson = txtContactPerson.Text.Trim();
            Customer.Mobile = txtMobile.Text.Trim();
            Customer.AlternativeMobile = txtAlternativeMobile.Text.Trim();
            Customer.Email = txtEmail.Text.Trim();
            Customer.Address1 = txtAddress1.Text.Trim();
            Customer.Address2 = txtAddress2.Text.Trim();
            Customer.Address3 = txtAddress3.Text.Trim();
            Customer.City = txtCity.Text.Trim();
            Customer.Pincode = txtPincode.Text.Trim();

            Customer.Country = new PDMS_Country() { CountryID = Convert.ToInt32(ddlCountry.SelectedValue) };
            Customer.State = new PDMS_State() { StateID = Convert.ToInt32(ddlState.SelectedValue) };
            Customer.District = new PDMS_District() { DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue) };
            if (ddlTehsil.SelectedValue != "0")
            {
                Customer.Tehsil = new PDMS_Tehsil() { TehsilID = Convert.ToInt32(ddlTehsil.SelectedValue) };
            } 
            return Customer;
        }
        public string ValidationCustomer()
        {
            string Message = "";
            Boolean Ret = true;

            txtCustomerName.BorderColor = Color.Silver;
            txtContactPerson.BorderColor = Color.Silver;
            txtMobile.BorderColor = Color.Silver;
            txtAddress1.BorderColor = Color.Silver;
            txtPincode.BorderColor = Color.Silver;

            ddlCountry.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;
            ddlDistrict.BorderColor = Color.Silver;

            if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            {
                Message = "Please enter the Customer Name";
                Ret = false;
                txtCustomerName.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtContactPerson.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Contact Person";
                Ret = false;
                txtContactPerson.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtMobile.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Mobile";
                Ret = false;
                txtMobile.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtAddress1.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Address1";
                Ret = false;
                txtAddress1.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtPincode.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Postal";
                Ret = false;
                txtPincode.BorderColor = Color.Red;
            }
            if (ddlCountry.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Country";
                Ret = false;
                ddlCountry.BorderColor = Color.Red;
            }
            else if (ddlState.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the State";
                Ret = false;
                ddlState.BorderColor = Color.Red;
            }

            else if (ddlDistrict.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the District";
                Ret = false;
                ddlDistrict.BorderColor = Color.Red;
            }


            return Message;
        }
    }
}