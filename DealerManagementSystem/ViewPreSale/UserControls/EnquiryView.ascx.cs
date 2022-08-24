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
        public PDMS_Customer Customer
        {
            get
            {
                if (Session["CustomerEnquiry"] == null)
                {
                    Session["CustomerEnquiry"] = new PDMS_Customer();
                }
                return (PDMS_Customer)Session["CustomerEnquiry"];
            }
            set
            {
                Session["CustomerEnquiry"] = value;
            }
        }
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
        public PLead Lead
        {
            get
            {
                if (Session["EnquiryViewPLead"] == null)
                {
                    Session["EnquiryViewPLead"] = new PLead();
                }
                return (PLead)Session["EnquiryViewPLead"];
            }
            set
            {
                Session["EnquiryViewPLead"] = value;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblAddEnquiryMessage.Text = "";

            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["EnquiryID"])))
            {
                long EnquiryID = Convert.ToInt64(Convert.ToString(ViewState["EnquiryID"]));
                if (EnquiryID != Enquiry.EnquiryID)
                {
                    Enquiry = new BEnquiry().GetEnquiry(EnquiryID, null, null, null, null, null, null, null, null, null, null,PSession.User.UserID)[0];
                }
            }
        }
        public void fillViewEnquiry(long EnquiryID)
        {
            ViewState["EnquiryID"]= EnquiryID;
            Enquiry = new BEnquiry().GetEnquiry(EnquiryID,null, null, null, null, null, null, null, null, null, null, PSession.User.UserID)[0];
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
            lblAddress2.Text = Enquiry.Address2.ToString();
            lblAddress3.Text = Enquiry.Address3.ToString();
            lblMobile.Text = "<a href='tel:" + Enquiry.Mobile + "'>" + Enquiry.Mobile + "</a>";
            lblMail.Text = "<a href='mailto:" + Enquiry.Mail + "'>" + Enquiry.Mail + "</a>";
            lblRemarks.Text = Enquiry.Remarks;

            if (Enquiry.LeadID != null)
            {
                Lead = new BLead().GetLeadByID((long)Enquiry.LeadID);
                CustomerViewSoldTo.fillCustomer(Lead.Customer);
                UC_LeadView.fillViewLead(Lead);
            }
            else
            {
                CustomerViewSoldTo.Clear();
                UC_LeadView.Clear();
            }
            ActionControlMange();
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
                    MPE_Enquiry.Hide();
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
            if (lbActions.Text == "Convert To Lead")
            {
                MPE_CustomerSelect.Show();  
                gvCustomer.DataSource = new BDMS_Customer().GetCustomerForEnquiryToLead(Enquiry.CustomerName, Enquiry.Mobile, Enquiry.State.StateID);
                gvCustomer.DataBind();
            }
            if (lbActions.Text == "Reject")
            {
                MPE_EnquiryReject.Show(); 
            }
        }

        protected void btnEnquiryStatus_Click(object sender, EventArgs e)
        { 
            if (new BEnquiry().UpdateEnquiryReject(Enquiry.EnquiryID, txtRemark.Text.Trim(), PSession.User.UserID))
            {
                lblMessage.Text = "Enquiry Rejected Successfully...";
                lblMessage.ForeColor = Color.Green;
                fillViewEnquiry(Enquiry.EnquiryID);
            }
            else
            {
                lblAddEnquiryMessage.Text = "Enquiry is Not Rejected Successfully...!";
                lblAddEnquiryMessage.ForeColor = Color.Red;
            }
        }

        protected void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            MPE_Lead.Show();           
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblCustomerID = (Label)gvRow.FindControl("lblCustomerID");
            Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt64(lblCustomerID.Text));
            pnlCustomerOld.Enabled = false;
            UC_AddLead.FillMaster();
            UC_CustomerCreate.FillMaster();
            UC_CustomerCreate.FillCustomer(Customer);
            txtCustomerID.Text = Convert.ToString(Customer.CustomerID);
            DropDownList ddlSource = (DropDownList)UC_AddLead.FindControl("ddlSource");
            ddlSource.SelectedValue = Convert.ToString(Enquiry.Source.SourceID);
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
            DropDownList ddlTehsil = (DropDownList)UC_CustomerCreate.FindControl("ddlTehsil");


            TextBox txtCustomerName = (TextBox)UC_CustomerCreate.FindControl("txtCustomerName");
            //  TextBox txtCustomerName2 = (TextBox)UC_Customer.FindControl("txtCustomerName2");
            TextBox txtContactPerson = (TextBox)UC_CustomerCreate.FindControl("txtContactPerson");
            TextBox txtMobile = (TextBox)UC_CustomerCreate.FindControl("txtMobile");
            TextBox txtEmail = (TextBox)UC_CustomerCreate.FindControl("txtEmail");
            TextBox txtAddress1 = (TextBox)UC_CustomerCreate.FindControl("txtAddress1");
            TextBox txtAddress2 = (TextBox)UC_CustomerCreate.FindControl("txtAddress2");
            TextBox txtAddress3 = (TextBox)UC_CustomerCreate.FindControl("txtAddress3");

            ddlSource.SelectedValue = Convert.ToString(Enquiry.Source.SourceID);
            ddlCountry.SelectedValue = Convert.ToString(Enquiry.Country.CountryID); 
            new DDLBind(ddlDistrict, new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
            ddlState.SelectedValue = Convert.ToString(Enquiry.State.StateID);

            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID");
            ddlDistrict.SelectedValue = Convert.ToString(Enquiry.District.DistrictID);

            List<PDMS_Tehsil> Tehsil = new BDMS_Address().GetTehsil(null, null, Convert.ToInt32(ddlDistrict.SelectedValue), null); 
            new DDLBind(ddlTehsil, Tehsil, "Tehsil", "TehsilID");

            txtContactPerson.Text = Enquiry.PersonName;
            txtMobile.Text = Enquiry.Mobile;
            txtEmail.Text = Enquiry.Mail;

            txtCustomerName.Text = Enquiry.CustomerName;
            txtAddress1.Text = Enquiry.Address;
            txtAddress2.Text = Enquiry.Address2;
            txtAddress3.Text = Enquiry.Address3;
        }

        protected void gvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomer.PageIndex = e.NewPageIndex;
            MPE_CustomerSelect.Show();
            gvCustomer.DataSource = new BDMS_Customer().GetCustomerForEnquiryToLead(Enquiry.CustomerName, Enquiry.Mobile, Enquiry.State.StateID);
            gvCustomer.DataBind();

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
            Lead.EnquiryID = Enquiry.EnquiryID;
            string result = new BAPI().ApiPut("Lead", Lead);
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(result);
            if (Results.Status == PApplication.Failure)
            {
                lblMessageLead.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_Lead.Hide();
            fillViewEnquiry(Enquiry.EnquiryID);

           // PLeadSearch S = new PLeadSearch();
           // S.LeadID = Convert.ToInt64(Results.Data);

            // gvLead.DataSource = new BLead().GetLead(S);
            // gvLead.DataBind();
            // UC_CustomerCreate.FillClean(); 
        }
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }

        void ActionControlMange()
        {
            lbEditEnquiry.Visible = true;
            lbInActive.Visible = true;
            lbReject.Visible = true;

            if ((Enquiry.Status.StatusID == (short)PreSaleStatus.ConvertedToLead) || (Enquiry.Status.StatusID == (short)PreSaleStatus.Rejected))
            {
                lbEditEnquiry.Visible = false;
                lbInActive.Visible = false;
                lbReject.Visible = false;
            }
        }
    }
}