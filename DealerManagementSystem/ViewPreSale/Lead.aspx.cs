using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Lead : System.Web.UI.Page
    { 

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales ~ Lead');</script>");

            if (!IsPostBack)
            {
                List<PLeadCategory> Category = new BLead().GetLeadCategory(null, null);
                new DDLBind(ddlCategory, Category, "Category", "CategoryID");
                new DDLBind(ddlSCategory, Category, "Category", "CategoryID");

                List <PLeadQualification > Qualification = new BLead().GetLeadQualification(null, null);
                new DDLBind(ddlQualification, Qualification, "Qualification", "QualificationID");
                new DDLBind(ddlSQualification, Qualification, "Qualification", "QualificationID");

                List<PLeadSource> Source = new BLead().GetLeadSource(null, null);
                new DDLBind(ddlSource, Source, "Source", "SourceID");
                new DDLBind(ddlSSource, Source, "Source", "SourceID");

                List<PLeadType> LeadType = new BLead().GetLeadType(null, null);
                new DDLBind(ddlLeadType, LeadType, "Type", "TypeID");
                 new DDLBind(ddlSType, LeadType, "Type", "TypeID");

                List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                new DDLBind(ddlCountry, Country, "Country", "CountryID");
                new DDLBind(ddlSCountry, Country, "Country", "CountryID");
                ddlCountry.SelectedValue = "1";

                List < PDMS_State > State= new BDMS_Address().GetState(1, null, null, null);
                new DDLBind(ddlState, State, "State", "StateID");
                new DDLBind(ddlSState, State, "State", "StateID");

                List<PLeadProgressStatus > ProgressStatus = new BLead().GetLeadProgressStatus(null, null);
                new DDLBind(ddlSProgressStatus, ProgressStatus, "ProgressStatus", "ProgressStatusID");

                List<PLeadStatus> Status = new BLead().GetLeadStatus(null, null);
                new DDLBind(ddlSStatus, Status, "Status", "StatusID"); 
            }
        }

        void fillLead()
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            PLead Lead = new PLead();
            Lead.LeadDate = Convert.ToDateTime(txtLeadDate.Text.Trim());

            Lead.Category = new PLeadCategory() { CategoryID = Convert.ToInt32(ddlCategory.SelectedValue) };
            Lead.Qualification = new PLeadQualification() { QualificationID = Convert.ToInt32(ddlQualification.SelectedValue) };
            Lead.Source = new PLeadSource() { SourceID = Convert.ToInt32(ddlSource.SelectedValue) };
            Lead.Type = new PLeadType() { TypeID = Convert.ToInt32(ddlLeadType.SelectedValue) };
            Lead.Customer = new PDMS_Customer() { CustomerCode = txtCustomerCode.Text.Trim() };
            Lead.Remarks = txtRemarks.Text.Trim();

            Lead.Customer.CustomerName = txtCustomerName.Text.Trim();

            Lead.Customer.ContactPerson = txtContactPerson.Text.Trim();

            Lead.Customer.Mobile = txtMobile.Text.Trim();
            Lead.Customer.Email = txtEmail.Text.Trim();
            Lead.Customer.Address1 = txtAddress1.Text.Trim();
            Lead.Customer.Address2 = txtAddress2.Text.Trim();
            Lead.Customer.Country = new PDMS_Country() { CountryID = Convert.ToInt32(ddlCountry.SelectedValue) };
            Lead.Customer.State = new PDMS_State() { StateID = Convert.ToInt32(ddlState.SelectedValue) };
            Lead.Customer.District = new PDMS_District() { DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue) };
            Lead.Customer.Tehsil = new PDMS_Tehsil() { TehsilID = Convert.ToInt32(ddlTehsil.SelectedValue) };
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };
            PLead l = JsonConvert.DeserializeObject<PLead>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead", Lead)).Data));
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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            PLeadSearch S = new PLeadSearch();
            S.LeadNumber = txtLeadNumber.Text.Trim();
            S.StateID = ddlSState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSState.SelectedValue);
            S.ProgressStatusID = ddlSProgressStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSProgressStatus.SelectedValue);
            S.CategoryID = ddlSCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCategory.SelectedValue);
            S.QualificationID = ddlSQualification.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSQualification.SelectedValue);
            S.SourceID = ddlSSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSSource.SelectedValue);
            S.TypeID = ddlSType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSType.SelectedValue);
            S.CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);
            S.StatusID = ddlSStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSStatus.SelectedValue); 

            S.CustomerCode = txtCustomerCode.Text.Trim();
            S.LeadDateFrom = string.IsNullOrEmpty(txtLeadDateFrom.Text.Trim())?(DateTime?) null: Convert.ToDateTime( txtLeadDateFrom.Text.Trim());
            S.LeadDateTo = string.IsNullOrEmpty(txtLeadDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateTo.Text.Trim());

            List<PLead> Leads = new BLead().GetLead(S);
            gvLead.DataSource = Leads;
            gvLead.DataBind();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            DropDownList ddlAction = (DropDownList)gvRow.FindControl("ddlAction");

            if (ddlAction.Text == "View Lead")
            {
                Label lblLeadNumber1 = (Label)gvRow.FindControl("lblLeadNumber");
                PLeadSearch S = new PLeadSearch();
                S.LeadNumber = lblLeadNumber1.Text;
                PLead Lead = new BLead().GetLead(S)[0];
                lblLeadNumber.Text = Lead.LeadNumber;
                lblLeadDate.Text = Convert.ToString(Lead.LeadDate);
                lblCategory.Text = Lead.Category.Category;
                lblProgressStatus.Text = Lead.ProgressStatus.ProgressStatus;
                lblQualification.Text = Lead.Qualification.Qualification;
                lblSource.Text = Lead.Source.Source;
                lblStatus.Text = Lead.Status.Status;
                lblType.Text = Lead.Type.Type;
                lblDealer.Text = Lead.Dealer.DealerCode;
                lblRemarks.Text = Lead.Remarks;
                string Customer = Lead.Customer.CustomerCode + " " + Lead.Customer.CustomerName;
                lblCustomer.Text = Customer;
                lblContactPerson.Text = Lead.Customer.ContactPerson;
                lblMobile.Text = Lead.Customer.Mobile;
                lblEmail.Text = Lead.Customer.Email;

                string Location = Lead.Customer.Address1 + ", " + Lead.Customer.Address2 + ", " + Lead.Customer.District.District + ", " + Lead.Customer.State.State;
                lblLocation.Text = Location;
            }
            else if (ddlAction.Text == "Assign")
            {
                MP_AssignSalesEngineer.Show();
            }
        }
        protected void ddlSCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlSState, new BDMS_Address().GetState(Convert.ToInt32(ddlSCountry.SelectedValue), null, null, null), "State", "StateID");
        }

        protected void btnAssignSalesEngineer_Click(object sender, EventArgs e)
        {

        }
    }
}