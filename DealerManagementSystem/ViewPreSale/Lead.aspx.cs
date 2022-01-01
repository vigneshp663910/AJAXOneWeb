using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Lead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            { 
                new DDLBind(ddlCategory, new BLead().GetLeadCategory(null, null), "Category", "CategoryID");
                new DDLBind(ddlQualification, new BLead().GetLeadQualification(null, null), "Qualification", "QualificationID");
                new DDLBind(ddlSource, new BLead().GetLeadSource(null, null), "Source", "SourceID");
                new DDLBind(ddlLeadType, new BLead().GetLeadType(null, null), "Type", "TypeID");

                new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
                ddlCountry.SelectedValue = "1";
                new DDLBind(ddlState, new BDMS_Address().GetState(1, null, null, null), "State", "StateID");
                
                //ddlProgressStatus.DataSource = new BLead().GetLeadProgressStatus(null, null);
                //ddlProgressStatus.DataBind();
                //ddlProgressStatus.Items.Insert(0, new ListItem("Select", "0"));                 

                //ddlStatus.DataSource = new BLead().GetLeadStatus(null, null);
                //ddlStatus.DataBind();
                //ddlStatus.Items.Insert(0, new ListItem("Select", "0"));                
                 
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
            PLead l= JsonConvert.DeserializeObject<PLead>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead", Lead)).Data));
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
            List<PLead> Leads = new BLead().GetLead(S);
            gvLead.DataSource = Leads;
            gvLead.DataBind();
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
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
    }
}