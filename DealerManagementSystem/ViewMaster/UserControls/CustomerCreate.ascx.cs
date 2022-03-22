using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster.UserControls
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
            cxDOAnniversary.EndDate = DateTime.Now;
            cxDOB.EndDate = DateTime.Now;

            new DDLBind(ddlTitle, new BDMS_Customer().GetCustomerTitle(null, null), "Title", "TitleID",false);
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID"); 
            ddlCountry.SelectedValue = "1";  
            new DDLBind(ddlState, new BDMS_Address().GetState(1, null, null, null), "State", "StateID");  
           // new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(1, null, null, null,null), "District", "DistrictID");
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
            Customer.Title = new PCustomerTitle() { TitleID = Convert.ToInt32(ddlTitle.SelectedValue) };
            Customer.CustomerName = txtCustomerName.Text.Trim();
            Customer.CustomerName2 = txtCustomerName2.Text.Trim();
            Customer.GSTIN = txtGSTIN.Text.Trim().ToUpper();
            Customer.PAN = txtPAN.Text.Trim().ToUpper();
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
            Customer.IsShipTo = cbShipTo.Checked;
            Customer.CreatedBy = new PUser { UserID = PSession.User.UserID };
            return Customer;
        }

        public void FillCustomer(PDMS_Customer Customer)
        {
            txtCustomerName.Text = Customer.CustomerName;
            txtCustomerName2.Text = Customer.CustomerName2;
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
            cbShipTo.Checked = Customer.IsShipTo;
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
            long longCheck;
            

            string Message = ""; 
            txtCustomerName.BorderColor = Color.Silver;
            txtGSTIN.BorderColor = Color.Silver;
            txtPAN.BorderColor = Color.Silver;
            txtContactPerson.BorderColor = Color.Silver;
            txtMobile.BorderColor = Color.Silver;
            txtAlternativeMobile.BorderColor = Color.Silver;
            txtAddress1.BorderColor = Color.Silver;
            txtPincode.BorderColor = Color.Silver;

            ddlCountry.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;
            ddlDistrict.BorderColor = Color.Silver;

            Regex regex = new Regex(@"^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[a-zA-Z0-9]{3}$");

            if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            {
                Message = "Please enter the Customer Name";
                txtCustomerName.BorderColor = Color.Red;
                return Message;
            }
            else if ((!regex.Match(txtGSTIN.Text.Trim()).Success) && (txtGSTIN.Text.Trim() != "URD"))
            {
                Message = " GST Number " + txtGSTIN.Text.Trim() + " is not correct";
                txtGSTIN.BorderColor = Color.Red;
                return Message;
            } 

            if ((txtGSTIN.Text.Trim() != "URD") && (!string.IsNullOrEmpty(txtGSTIN.Text.Trim())))
            {
                string gst = txtGSTIN.Text.Trim().Remove(0, 2).Substring(0, 10);
                if (txtPAN.Text.Trim().ToUpper() != gst.ToUpper())
                {
                    Message = Message + "<br/>PAN and GSTIN are not matching .";
                    txtPAN.BorderColor = Color.Red;
                    return Message;
                }
            } 
            
            if (string.IsNullOrEmpty(txtContactPerson.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Contact Person";
                txtContactPerson.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtMobile.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Mobile";
                txtMobile.BorderColor = Color.Red;
            }
            else if (txtMobile.Text.Trim().Length != 10)
            {
                Message = Message + "<br/>Mobile Length should be 10 digit";
                txtMobile.BorderColor = Color.Red;
            }
            else if (!long.TryParse(txtMobile.Text.Trim(), out longCheck))
            {
                Message = Message + "<br/>Mobile should be 10 digit";
                txtMobile.BorderColor = Color.Red;
            }


            if (!string.IsNullOrEmpty(txtAlternativeMobile.Text.Trim()))
            {
                if (txtAlternativeMobile.Text.Trim().Length != 10)
                {
                    Message = Message + "<br/>Alternative Mobile Length should be 10 digit";
                    txtAlternativeMobile.BorderColor = Color.Red;
                }
                else if (!long.TryParse(txtAlternativeMobile.Text.Trim(), out longCheck))
                {
                    Message = Message + "<br/>Alternative Mobile should be 10 digit";
                    txtAlternativeMobile.BorderColor = Color.Red;
                }
            }

            if (!string.IsNullOrEmpty(Message))
            {
                return Message;
            }
              if (string.IsNullOrEmpty(txtAddress1.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Address1";
                txtAddress1.BorderColor = Color.Red;
            }

            else if (ddlCountry.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Country";
                ddlCountry.BorderColor = Color.Red;
            }
            else if (ddlState.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the State";
                ddlState.BorderColor = Color.Red;
            }
            else if (ddlDistrict.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the District";
                ddlDistrict.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtPincode.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Postal";
                txtPincode.BorderColor = Color.Red;
            }
            else if (!long.TryParse(txtPincode.Text.Trim(), out longCheck))
            {
                Message = Message + "<br/>Pincode should be in digit";
                txtPincode.BorderColor = Color.Red;
            }  
            return Message;
        }

       
    }
}