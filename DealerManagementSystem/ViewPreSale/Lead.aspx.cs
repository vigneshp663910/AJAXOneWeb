using Business;
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
            
            ddlCategory.DataSource = new BLead().GetLeadCategory(null, null);
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select", "0"));

            ddlProgressStatus.DataSource = new BLead().GetLeadProgressStatus(null, null);
            ddlProgressStatus.DataBind();
            ddlProgressStatus.Items.Insert(0, new ListItem("Select", "0"));

            ddlQualification.DataSource = new BLead().GetLeadQualification(null, null);
            ddlQualification.DataBind();
            ddlQualification.Items.Insert(0, new ListItem("Select", "0"));


            ddlSource.DataSource = new BLead().GetLeadSource(null, null);
            ddlSource.DataBind();
            ddlSource.Items.Insert(0, new ListItem("Select", "0"));

            ddlStatus.DataSource = new BLead().GetLeadStatus(null, null);
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Select", "0"));

            ddlCountry.DataSource = new BDMS_Address().GetCountry(null, null);
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("Select", "0"));
            ddlCountry.SelectedValue = "1";

            ddlState.DataSource = new BDMS_Address().GetState(1, null, null, null);
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("Select", "0"));

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlState.DataSource = new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null);
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDistrict.DataSource = new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null);
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlTehsil.DataSource = new BDMS_Address().GetTehsil(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null);
            //ddlTehsil.DataBind();
            //ddlTehsil.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}