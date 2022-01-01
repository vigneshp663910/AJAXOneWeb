using Business;
using Newtonsoft.Json;
using Properties;
using System;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Lead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales ~ Lead');</script>");

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

            Lead.Name = txtName.Text.Trim();

            Lead.PersonName = txtContactPersonName.Text.Trim();

            Lead.PersonContactNumber = txtContactNumber.Text.Trim();
            Lead.PersonMail = txtPersonMail.Text.Trim();
            Lead.Address1 = txtAddress1.Text.Trim();
            Lead.Address2 = txtAddress2.Text.Trim();
            Lead.Country = new PDMS_Country() { CountryID = Convert.ToInt32(ddlCountry.SelectedValue) };
            Lead.State = new PDMS_State() { StateID = Convert.ToInt32(ddlState.SelectedValue) };
            Lead.District = new PDMS_District() { DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue) };
            Lead.Tehsil = new PDMS_Tehsil() { TehsilID = Convert.ToInt32(ddlTehsil.SelectedValue) };
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
    }
}