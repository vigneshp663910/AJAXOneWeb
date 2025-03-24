using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSalesTouchPoint.UserControls
{
    public partial class AddEnquiry : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster()
        {
            Clear();
        }
        void Clear()
        {
            txtCustomerName.Text = string.Empty;
            //  txtEnquiryDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            txtPersonName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtMail.Text = string.Empty;
            ddlCountry.Items.Clear();
            ddlState.Items.Clear();
            ddlDistrict.Items.Clear();
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            ddlCountry.SelectedValue = "1";
            new DDLBind(ddlState, new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null, null, null), "District", "DistrictID");
            
            txtAddress.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtAddress3.Text = string.Empty;
            txtPincode.Text = string.Empty;
            txtRemarks.Text = string.Empty;

            txtCustomerName.Enabled = true;
            txtMobile.Enabled = true;
            ddlCountry.Enabled = true;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        public PSalesTouchPointEnquiry_Insert Read()
        {
            PSalesTouchPointEnquiry_Insert enquiry = new PSalesTouchPointEnquiry_Insert();
            enquiry.CustomerName = txtCustomerName.Text.Trim();
            enquiry.PersonName = txtPersonName.Text.Trim();
            enquiry.Mobile = txtMobile.Text.Trim();
            enquiry.Mail = txtMail.Text.Trim();
            enquiry.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            enquiry.StateID = Convert.ToInt32(ddlState.SelectedValue);
            enquiry.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            enquiry.Address = txtAddress.Text.Trim();
            enquiry.Address2 = txtAddress2.Text.Trim();
            enquiry.Address3 = txtAddress3.Text.Trim();
            enquiry.Pincode = txtPincode.Text.Trim();
            enquiry.Remarks = txtRemarks.Text.Trim();            
            return enquiry;
        }

        public void Write(PSalesTouchPointEnquiry enquiry)
        {
            txtCustomerName.Text = enquiry.CustomerName;
            txtCustomerName.Enabled = false;
            // txtEnquiryDate.Text = enquiry.EnquiryDate.ToString("dd/MM/yyyy HH:mm:ss");
            txtPersonName.Text = enquiry.PersonName;
            txtMobile.Text = enquiry.Mobile;
            txtMobile.Enabled = false;
            txtMail.Text = enquiry.Mail;
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");            
            new DDLBind(ddlState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID");
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null), "District", "DistrictID");
            ddlCountry.SelectedValue = enquiry.Country.CountryID.ToString();
            ddlCountry.Enabled = false;
            ddlState.SelectedValue = enquiry.State.StateID.ToString();
            ddlState.Enabled = false;
            ddlDistrict.SelectedValue = enquiry.District.DistrictID.ToString();
            ddlDistrict.Enabled = false;
            txtAddress.Text = enquiry.Address.ToString();
            txtAddress2.Text = enquiry.Address2.ToString();
            txtAddress3.Text = enquiry.Address3.ToString();
            txtPincode.Text = enquiry.Pincode.ToString();
            txtRemarks.Text = enquiry.Remarks;
        }
        public string Validation()
        {
            txtCustomerName.BorderColor = Color.Silver;
            txtPersonName.BorderColor = Color.Silver;
            txtMobile.BorderColor = Color.Silver;
            ddlCountry.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;
            ddlDistrict.BorderColor = Color.Silver;
            txtAddress.BorderColor = Color.Silver;
            txtPincode.BorderColor = Color.Silver;
            string Message = "";
            if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            {
                txtCustomerName.BorderColor = Color.Red;
                return "Please enter the customer name...!";
            }
            if (string.IsNullOrEmpty(txtPersonName.Text.Trim()))
            {
                txtPersonName.BorderColor = Color.Red;
                return "Please enter the contact person...!";
            }
            if (string.IsNullOrEmpty(txtMobile.Text.Trim()))
            {
                txtMobile.BorderColor = Color.Red;
                return "Please enter the mobile...!";
            }
            if (txtMobile.Text.Trim().Length != 10)
            {
                txtMobile.BorderColor = Color.Red;
                return "Mobile Length should be 10 digit";
            }
            if (ddlCountry.SelectedValue == "0")
            {
                ddlCountry.BorderColor = Color.Red;
                return "Please select the Country";
            }
            if (ddlState.SelectedValue == "0")
            {
                ddlState.BorderColor = Color.Red;
                return "Please select the State";
            }
            if (ddlDistrict.SelectedValue == "0")
            {
                ddlDistrict.BorderColor = Color.Red;
                return "Please select the District";
            }
            if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                txtAddress.BorderColor = Color.Red;
                return "Please enter the address...!";
            }
            if (string.IsNullOrEmpty(txtPincode.Text.Trim()))
            {
                txtPincode.BorderColor = Color.Red;
                return "Please enter the pincode...!";
            }
            return Message;
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlState, new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(null, null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID");
        }
    }
}