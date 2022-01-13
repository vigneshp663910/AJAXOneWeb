using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
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
            }
        }
        public void FillMaster()
        {  
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID"); 
            ddlCountry.SelectedValue = "1";  
            new DDLBind(ddlState, new BDMS_Address().GetState(1, null, null, null), "State", "StateID");  
          //  new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(1, null, null, null,null), "District", "DistrictID");
           // new DDLBind(ddlTehsil, new BDMS_Address().GetTehsil(1, null, null, null), "Tehsil", "TehsilID");
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
            //List<PDMS_District> District   new BDMS_Address().GetDistrict(1, null, null, null, null);
            List<PDMS_Tehsil> Tehsil = new BDMS_Address().GetTehsil(null, null, Convert.ToInt32(ddlDistrict.SelectedValue), null);
            //ddlCountry.SelectedValue = Convert.ToString(Tehsil[0].Country.CountryID);
            //ddlState.SelectedValue = Convert.ToString(Customer.State.StateID);
            //ddlDistrict.SelectedValue = Convert.ToString(Customer.District.DistrictID);

            new DDLBind(ddlTehsil, Tehsil, "Tehsil", "TehsilID");
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

            Customer.DOB = string.IsNullOrEmpty(txtDOB.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDOB.Text.Trim());
            Customer.DOAnniversary = string.IsNullOrEmpty(txtDOAnniversary.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDOAnniversary.Text.Trim());
            Customer.SendSMS = cbSendSMS.Checked;
            Customer.SendEmail = cbSendEmail.Checked;
            Customer.CreatedBy = new PUser { UserID = PSession.User.UserID };
            return Customer;
        }

        public void FillCustomer(PDMS_Customer Customer)
        {
            txtCustomerName.Text = Customer.CustomerName;
            txtGSTIN.Text = Customer.GSTIN;
            txtPAN.Text = Customer.PAN;
            txtContactPerson.Text = Customer.ContactPerson;
            txtMobile.Text = Customer.Mobile;
            txtAlternativeMobile.Text = Customer.AlternativeMobile;
            txtEmail.Text = Customer.Email;
            txtAddress1.Text = Customer.Address1;
            txtAddress2.Text = Customer.Address2;
            txtAddress3.Text = Customer.Address3;
            txtCity.Text = Customer.City;
            txtPincode.Text = Customer.Pincode;

            ddlCountry.SelectedValue = Convert.ToString(Customer.Country.CountryID);
            new DDLBind(ddlState, new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");

            ddlState.SelectedValue = Convert.ToString(Customer.State.StateID);
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null), "District", "DistrictID");

            ddlDistrict.SelectedValue = Convert.ToString(Customer.District.DistrictID);
            List<PDMS_Tehsil> Tehsil = new BDMS_Address().GetTehsil(null, null, Convert.ToInt32(ddlDistrict.SelectedValue), null);
            new DDLBind(ddlTehsil, Tehsil, "Tehsil", "TehsilID");
            if (Customer.Tehsil != null)
            {
                ddlTehsil.SelectedValue = Convert.ToString(Customer.Tehsil.TehsilID);
            }

            txtDOB.Text = Customer.DOB == null ? "" : ((DateTime)Customer.DOB).ToString("yyyy-MM-dd");
            txtDOAnniversary.Text = Customer.DOAnniversary == null ? "" : ((DateTime)Customer.DOAnniversary).ToString("yyyy-MM-dd");
            cbSendSMS.Checked = Customer.SendSMS;
            cbSendEmail.Checked = Customer.SendEmail;
        }

        public void FillClean()
        {
            txtCustomerName.Text = "";
            txtGSTIN.Text = "";
            txtPAN.Text = "";
            txtContactPerson.Text = "";
            txtMobile.Text = "";
            txtAlternativeMobile.Text = "";
            txtEmail.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtCity.Text = "";
            txtPincode.Text = "";
            txtDOB.Text = "";
            txtDOAnniversary.Text = "";
            cbSendSMS.Checked = false;
            cbSendEmail.Checked = false;
            FillMaster();
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