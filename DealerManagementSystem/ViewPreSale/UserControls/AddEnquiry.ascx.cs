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
    public partial class AddEnquiry : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster()
        {
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            ddlCountry.SelectedValue = "1";
            new DDLBind(ddlState, new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
            new DDLBind(ddlSource, new BPresalesMasters().GetLeadSource(null, null), "Source", "SourceID");
            Clear();
        } 
        void Clear()
        {
            txtCustomerName.Text = string.Empty;
            txtEnquiryDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            txtPersonName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtMail.Text = string.Empty;
            ddlSource.Items.Clear();
            ddlCountry.Items.Clear();
            ddlState.Items.Clear();
            ddlDistrict.Items.Clear();
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            ddlCountry.SelectedValue = "1";
            new DDLBind(ddlState, new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null, null, null), "District", "DistrictID");
            
            new DDLBind(ddlSource, new BPresalesMasters().GetLeadSource(null, null), "Source", "SourceID");
            txtAddress.Text = string.Empty;
            txtProduct.Text = string.Empty;
            txtRemarks.Text = string.Empty;
           

        }
        public PEnquiry Read()
        {
            PEnquiry enquiry = new PEnquiry();
            enquiry.CustomerName = txtCustomerName.Text.Trim();
            enquiry.EnquiryDate = Convert.ToDateTime(txtEnquiryDate.Text.Trim());
            enquiry.PersonName = txtPersonName.Text.Trim();
            enquiry.Mobile = txtMobile.Text.Trim();
            enquiry.Mail = txtMail.Text.Trim();
            enquiry.Source = new PLeadSource();
            enquiry.Source.SourceID = Convert.ToInt32(ddlSource.SelectedValue);
            enquiry.Status = new PPreSaleStatus();
            enquiry.Status.StatusID = 1;
            enquiry.Country = new PDMS_Country();
            enquiry.Country.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            enquiry.State = new PDMS_State();
            enquiry.State.StateID = Convert.ToInt32(ddlState.SelectedValue);
            enquiry.District = new PDMS_District();
            enquiry.District.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            enquiry.Address = txtAddress.Text.Trim();
            enquiry.Product = txtProduct.Text.Trim();
            enquiry.Remarks = txtRemarks.Text.Trim();
            enquiry.CreatedBy = new PUser(); 
            return enquiry;
        }

        public void Write(PEnquiry enquiry)
        {
            txtCustomerName.Text = enquiry.CustomerName;
            txtEnquiryDate.Text = enquiry.EnquiryDate.ToString("dd/MM/yyyy HH:mm:ss");
            txtPersonName.Text = enquiry.PersonName;
            txtMobile.Text = enquiry.Mobile;
            txtMail.Text = enquiry.Mail;
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            new DDLBind(ddlState, new BDMS_Address().GetState(null, null, null, null), "State", "StateID");
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null), "District", "DistrictID");
            new DDLBind(ddlSource, new BPresalesMasters().GetLeadSource(null, null), "Source", "SourceID");
            ddlCountry.SelectedValue = enquiry.Country.CountryID.ToString();
            ddlState.SelectedValue = enquiry.State.StateID.ToString();
            ddlDistrict.SelectedValue = enquiry.District.DistrictID.ToString();
            ddlSource.SelectedValue = enquiry.Source.SourceID.ToString();
            txtAddress.Text = enquiry.Address.ToString();
            txtProduct.Text = enquiry.Product;
            txtRemarks.Text = enquiry.Remarks;
        }
        public string Validation()
        {
            string Message = "";
            if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            {
                txtCustomerName.BorderColor = Color.Red;
                return "Please enter the Customer Name...!";
            }
            if (string.IsNullOrEmpty(txtEnquiryDate.Text.Trim()))
            {
                txtEnquiryDate.BorderColor = Color.Red;
                return "Please select the Enquiry Date...!";
            }
            if (string.IsNullOrEmpty(txtMobile.Text.Trim()))
            {
                txtMobile.BorderColor = Color.Red;
                return "Please Enter the Mobile...!";
            }
            else if (txtMobile.Text.Trim().Length != 10)
            {
                Message = Message + "<br/>Mobile Length should be 10 digit";
                txtMobile.BorderColor = Color.Red;
            }

            if (ddlSource.SelectedValue == "0")
            {
                ddlSource.BorderColor = Color.Red;
                return "Please select the Source";
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
            return Message;
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        { 
            new DDLBind(ddlState, new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        { 
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID");
        }
    }
}