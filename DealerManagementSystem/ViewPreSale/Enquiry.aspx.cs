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

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Enquiry : System.Web.UI.Page
    {
        public List<PEnquiry> PEnquiry
        {
            get
            {
                if (Session["Enquiry"] == null)
                {
                    Session["Enquiry"] = new List<PEnquiry>();
                }
                return (List<PEnquiry>)Session["Enquiry"];
            }
            set
            {
                Session["Enquiry"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ClearField();
                    //FillGrid(null);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillGrid(long? EnquiryID)
        {
            int? CountryID = null, StateID = null, DistrictID = null;
            string CustomerName = null;
            if (!string.IsNullOrEmpty(txtSCustomerName.Text))
            {
                CustomerName = txtSCustomerName.Text.Trim();
            }
            if (ddlSCountry.SelectedValue != "0")
            {
                CountryID = Convert.ToInt32(ddlSCountry.SelectedValue);
            }
            if (ddlSState.SelectedValue != "0")
            {
                StateID = Convert.ToInt32(ddlSState.SelectedValue);
            }
            if (ddlSDistrict.SelectedValue != "0")
            {
                DistrictID = Convert.ToInt32(ddlSDistrict.SelectedValue);
            }
            PEnquiry= new BEnquiry().GetEnquiry(null, CustomerName, CountryID, StateID, DistrictID);
            gvEnquiry.DataSource = PEnquiry;
            gvEnquiry.DataBind();

            if (PEnquiry.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnEnqArrowLeft.Visible = false;
                ibtnEnqArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnEnqArrowLeft.Visible = true;
                ibtnEnqArrowRight.Visible = true;
                lblRowCount.Text = (((gvEnquiry.PageIndex) * gvEnquiry.PageSize) + 1) + " - " + (((gvEnquiry.PageIndex) * gvEnquiry.PageSize) + gvEnquiry.Rows.Count) + " of " + PEnquiry.Count;
            }

            if (EnquiryID != null && EnquiryID != 0)
            {
                PEnquiry Enquiry = new BEnquiry().GetEnquiry(Convert.ToInt32(EnquiryID), null, null, null, null)[0];
                lblCustomerName.Text = Enquiry.CustomerName;
                lblPersonName.Text = Enquiry.PersonName;
                lblEnquiryDate.Text = Enquiry.EnquiryDate.ToString("dd/MM/yyyy");
                lblSource.Text = Enquiry.Source.Source;
                lblStatus.Text = Enquiry.Status.Status;
                lblProduct.Text = Enquiry.Product;
                lblCountry.Text = Enquiry.Country.Country.ToString();
                lblState.Text = Enquiry.State.State.ToString();
                lblDistrict.Text = Enquiry.District.District.ToString();
                lblAddress.Text = Enquiry.Address.ToString();
                lblMail.Text = Enquiry.Mail;
                lblMobile.Text = Enquiry.Mobile;
                lblRemarks.Text = Enquiry.Remarks;
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillGrid(null);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            MPE_Enquiry.Show();
            HiddenEnquiryID.Value = "";
        }

        protected void gvEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
                FillGrid(null);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            divEnquiryView.Visible = true;
            divEnquiryList.Visible = false;
            lblAddEnquiryMessage.Text = "";
            lblMessage.Text = "";
            Button BtnView = (Button)sender;
            int? EnquiryID = Convert.ToInt32(BtnView.CommandArgument);
            HiddenEnquiryID.Value = EnquiryID.ToString();
            FillGrid(EnquiryID);
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPE_Enquiry.Show();
            new DDLBind(ddlState, new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPE_Enquiry.Show();
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_Enquiry.Show();
                if (!Validation())
                {
                    return;
                }
                PEnquiry enquiry = new PEnquiry();
                if (!string.IsNullOrEmpty(HiddenEnquiryID.Value))
                {
                    enquiry.EnquiryID = Convert.ToInt32(HiddenEnquiryID.Value);
                }
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
                enquiry.CreatedBy.CreatedBy = PSession.User.UserID;
                if (new BEnquiry().InsertOrUpdateEnquiry(enquiry))
                {
                    lblMessage.Text = "Enquiry Was Saved Successfully...";
                    lblMessage.ForeColor = Color.Green;
                    FillGrid(enquiry.EnquiryID);
                    ClearField();
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
        Boolean Validation()
        {
            lblAddEnquiryMessage.ForeColor = Color.Red;
            Boolean Ret = true;
            string Message = "";
            if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Customer Name...!";
                Ret = false;
                txtCustomerName.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtEnquiryDate.Text.Trim()))
            {
                Message = Message + "<br/>Please select the Enquiry Date...!";
                Ret = false;
                txtEnquiryDate.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtMobile.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the Mobile...!";
                Ret = false;
                txtMobile.BorderColor = Color.Red;
            }
            if (ddlSource.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Source";
                Ret = false;
                ddlSource.BorderColor = Color.Red;
            }
            if (ddlCountry.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Country";
                Ret = false;
                ddlCountry.BorderColor = Color.Red;
            }
            if (ddlState.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the State";
                Ret = false;
                ddlState.BorderColor = Color.Red;
            }
            if (ddlDistrict.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the District";
                Ret = false;
                ddlDistrict.BorderColor = Color.Red;
            }
            lblAddEnquiryMessage.Text = Message;
            return Ret;
        }
        void ClearField()
        {
            txtCustomerName.Text = string.Empty;
            txtEnquiryDate.Text = string.Empty;
            txtPersonName.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtMail.Text = string.Empty;
            ddlSource.Items.Clear();
            ddlCountry.Items.Clear();
            ddlState.Items.Clear();
            ddlDistrict.Items.Clear();
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            new DDLBind(ddlSCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            new DDLBind(ddlSState, new BDMS_Address().GetState(null, null, null, null), "State", "StateID");
            new DDLBind(ddlSDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null), "District", "DistrictID");
            new DDLBind(ddlSource, new BPresalesMasters().GetLeadSource(null, null), "Source", "SourceID");
            txtAddress.Text = string.Empty;
            txtProduct.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            int? EnquiryID = Convert.ToInt32(HiddenEnquiryID.Value);
            PEnquiry enquiry = new BEnquiry().GetEnquiry(EnquiryID, null, null, null, null)[0];
            if (lbActions.Text == "Edit Enquiry")
            {
                MPE_Enquiry.Show();
                lblMessage.Text = "";
                lblAddEnquiryMessage.Text = "";                
                txtCustomerName.Text = enquiry.CustomerName;
                txtEnquiryDate.Text = enquiry.EnquiryDate.ToString("dd/MM/yyyy");
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
            if (lbActions.Text == "ConvertToLead")
            {

            }
            if (lbActions.Text == "Reject")
            {
                MPE_FoloowUpStatus.Show();
                ViewState["Status"] = 2;
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            ClearField();
            MPE_Enquiry.Hide();
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divEnquiryView.Visible = false;
            divEnquiryList.Visible = true;
            ClearField();
        }

        protected void btnFoloowUpStatus_Click(object sender, EventArgs e)
        {
            int? EnquiryID = Convert.ToInt32(HiddenEnquiryID.Value);
            PEnquiry enquiry = new BEnquiry().GetEnquiry(EnquiryID, null, null, null, null)[0];

            enquiry.Remarks = txtRemark.Text.Trim();
            enquiry.Status = new PPreSaleStatus();
            enquiry.Status.StatusID = Convert.ToInt32(ViewState["Status"]);
            enquiry.CreatedBy = new PUser();
            enquiry.CreatedBy.CreatedBy = PSession.User.UserID;

            if (new BEnquiry().InsertOrUpdateEnquiry(enquiry))
            {
                lblMessage.Text = "Enquiry PreSales Status Was Updated Successfully...";
                lblMessage.ForeColor = Color.Green;
                FillGrid(enquiry.EnquiryID);
                ClearField();
                MPE_Enquiry.Hide();
            }
            else
            {
                lblAddEnquiryMessage.Text = "Enquiry Not Saved Successfully...!";
                lblAddEnquiryMessage.ForeColor = Color.Red;
            }
        }

        protected void ibtnEnqArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEnquiry.PageIndex > 0)
            {
                gvEnquiry.PageIndex = gvEnquiry.PageIndex - 1;
                EnquiryBind(gvEnquiry, lblRowCount, PEnquiry);
            }
        }

        protected void ibtnEnqArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEnquiry.PageCount > gvEnquiry.PageIndex)
            {
                gvEnquiry.PageIndex = gvEnquiry.PageIndex + 1;
                EnquiryBind(gvEnquiry, lblRowCount, PEnquiry);
            }
        }
        void EnquiryBind(GridView gv, Label lbl, List<PEnquiry> PEnquiry)
        {
            gv.DataSource = PEnquiry;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + PEnquiry.Count;
        }
    }
}