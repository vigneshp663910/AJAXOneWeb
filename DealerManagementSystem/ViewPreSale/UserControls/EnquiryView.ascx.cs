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
    public partial class EnquiryView : System.Web.UI.UserControl
    {
        public PEnquiry Enquiry
        {
            get
            {
                if (Session["EnquiryView"] == null)
                {
                    Session["EnquiryView"] = new PLead();
                }
                return (PEnquiry)Session["EnquiryView"];
            }
            set
            {
                Session["EnquiryView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblAddEnquiryMessage.Text = "";
        }
        public void fillViewEnquiry(long? EnquiryID)
        {
            Enquiry = new BEnquiry().GetEnquiry(Convert.ToInt32(EnquiryID), null, null, null, null, null, null, null)[0];
            lblEnquiryNumber.Text = Enquiry.EnquiryNumber;
            lblCustomerName.Text = Enquiry.CustomerName;
            lblPersonName.Text = Enquiry.PersonName;
            lblEnquiryDate.Text = Enquiry.EnquiryDate.ToString("dd/MM/yyyy HH:mm:ss");
            lblSource.Text = Enquiry.Source.Source;
            lblStatus.Text = Enquiry.Status.Status;
            lblProduct.Text = Enquiry.Product;
            lblCountry.Text = Enquiry.Country.Country.ToString();
            lblState.Text = Enquiry.State.State.ToString();
            lblDistrict.Text = Enquiry.District.District.ToString();
            lblAddress.Text = Enquiry.Address.ToString();
            lblMobile.Text = "<a href='tel:" + Enquiry.Mobile + "'>" + Enquiry.Mobile + "</a>";
            lblMail.Text = "<a href='mailto:" + Enquiry.Mail + "'>" + Enquiry.Mail + "</a>";
            lblRemarks.Text = Enquiry.Remarks;
            CustomerViewSoldTo.fillCustomer(null);
            UC_LeadView.fillViewLead(null);
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_Enquiry.Show();
                string Message = UC_AddEnquiry.Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                PEnquiry enquiryAdd = UC_AddEnquiry.Read();

                enquiryAdd.EnquiryID = Enquiry.EnquiryID; 
                if (new BEnquiry().InsertOrUpdateEnquiry(enquiryAdd, PSession.User.UserID))
                {
                    lblMessage.Text = "Enquiry Was Saved Successfully...";
                    lblMessage.ForeColor = Color.Green;
                    fillViewEnquiry(Enquiry.EnquiryID); 
                }
                else
                {
                    lblAddEnquiryMessage.Text = "Enquiry Not Saved Successfully...!";
                    lblAddEnquiryMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblAddEnquiryMessage.Text = ex.Message.ToString();
                lblAddEnquiryMessage.ForeColor = Color.Red;
            }
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);  
            if (lbActions.Text == "Edit Enquiry")
            { 
                MPE_Enquiry.Show();
                UC_AddEnquiry.FillMaster();
                UC_AddEnquiry.Write(Enquiry); 
            }
            if (lbActions.Text == "ConvertToLead")
            {
                MPE_CustomerSelect.Show(); 
                string CustomerName = Enquiry.CustomerName;
                string Mobile = Enquiry.Mobile; 
                gvCustomer.DataSource = new BDMS_Customer().GetCustomer(null, null, CustomerName, Mobile, null, null, null);
                gvCustomer.DataBind();
            }
            if (lbActions.Text == "Reject")
            {
                MPE_EnquiryReject.Show(); 
            }
        }

        protected void btnEnquiryStatus_Click(object sender, EventArgs e)
        { 
            if (new BEnquiry().UpdateEnquiryStatus(Enquiry.EnquiryID, txtRemark.Text.Trim(),2, PSession.User.UserID))
            {
                lblMessage.Text = "Enquiry PreSales Status Was Updated Successfully...";
                lblMessage.ForeColor = Color.Green;
                fillViewEnquiry(Enquiry.EnquiryID);
            }
            else
            {
                lblAddEnquiryMessage.Text = "Enquiry Not Saved Successfully...!";
                lblAddEnquiryMessage.ForeColor = Color.Red;
            }
        }

        protected void btnSelectCustomer_Click(object sender, EventArgs e)
        {

        }

        protected void btnNewCustomer_Click(object sender, EventArgs e)
        {

        }

        protected void gvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}