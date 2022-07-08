using Business;
using Newtonsoft.Json;
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
                gvCustomer.DataSource = new BDMS_Customer().GetCustomerAutocomplete(CustomerName, 0);
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
            MPE_Lead.Show();
            UC_AddLead.FillMaster();
            UC_CustomerCreate.FillMaster();
            DropDownList ddlSource = (DropDownList)UC_AddLead.FindControl("ddlSource");
            ddlSource.SelectedValue = Convert.ToString(Enquiry.EnquiryID);
        }

        protected void btnNewCustomer_Click(object sender, EventArgs e)
        {
            MPE_Lead.Show();
            UC_AddLead.FillMaster();
            UC_CustomerCreate.FillMaster();

            DropDownList ddlSource = (DropDownList)UC_AddLead.FindControl("ddlSource");
            DropDownList ddlCountry = (DropDownList)UC_CustomerCreate.FindControl("ddlCountry");
            DropDownList ddlState = (DropDownList)UC_CustomerCreate.FindControl("ddlState");
            
            DropDownList ddlDistrict = (DropDownList)UC_CustomerCreate.FindControl("ddlDistrict");
            
            

            TextBox txtCustomerName = (TextBox)UC_CustomerCreate.FindControl("txtCustomerName");
            //  TextBox txtCustomerName2 = (TextBox)UC_Customer.FindControl("txtCustomerName2");
            TextBox txtContactPerson = (TextBox)UC_CustomerCreate.FindControl("txtContactPerson");
            TextBox txtMobile = (TextBox)UC_CustomerCreate.FindControl("txtMobile");
            TextBox txtEmail = (TextBox)UC_CustomerCreate.FindControl("txtEmail");
            TextBox txtAddress1 = (TextBox)UC_CustomerCreate.FindControl("txtAddress1");
            // TextBox txtAddress2 = (TextBox)UC_Customer.FindControl("txtAddress2");
            // TextBox txtAddress3 = (TextBox)UC_Customer.FindControl("txtAddress3");

            ddlSource.SelectedValue = Convert.ToString(Enquiry.EnquiryID);
            ddlCountry.SelectedValue = Convert.ToString(Enquiry.Country.CountryID); 
            new DDLBind(ddlDistrict, new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
            ddlState.SelectedValue = Convert.ToString(Enquiry.State.StateID);

            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID");
            ddlDistrict.SelectedValue = Convert.ToString(Enquiry.District.DistrictID);

            txtContactPerson.Text = Enquiry.PersonName;
            txtMobile.Text = Enquiry.Mobile;
            txtEmail.Text = Enquiry.Mail;

            txtCustomerName.Text = Enquiry.CustomerName;
            txtAddress1.Text = Enquiry.Address;
        }

        protected void gvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void btnSaveLead_Click(object sender, EventArgs e)
        {
            MPE_Lead.Show();
            PLead_Insert Lead = new PLead_Insert();
            lblMessageLead.ForeColor = Color.Red;
            lblMessageLead.Visible = true;
            string Message = UC_AddLead.Validation();

            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageLead.Text = Message;
                return;
            }
            Lead = UC_AddLead.Read(); 
            if (!string.IsNullOrEmpty(txtCustomerID.Text.Trim()))
            {
                Lead.Customer = new PDMS_Customer();
                Lead.Customer.CustomerID = Convert.ToInt64(txtCustomerID.Text.Trim());
            }
            else
            {
                Message = UC_CustomerCreate.ValidationCustomer();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessageLead.Text = Message;
                    return;
                }
                Lead.Customer = UC_CustomerCreate.ReadCustomer();
            } 

            string result = new BAPI().ApiPut("Lead", Lead);
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(result);
            if (Results.Status == PApplication.Failure)
            {
                lblMessageLead.Text = Results.Message;
                return;
            }
            ShowMessage(Results);

            PLeadSearch S = new PLeadSearch();
            S.LeadID = Convert.ToInt64(result);

            // gvLead.DataSource = new BLead().GetLead(S);
            // gvLead.DataBind();
            UC_CustomerCreate.FillClean(); 
        }
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
    }
}