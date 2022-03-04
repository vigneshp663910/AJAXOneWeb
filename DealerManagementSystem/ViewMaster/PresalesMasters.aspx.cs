using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class PresalesMasters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    List<PLeadSource> Source = new BPresalesMasters().GetLeadSource(null, null);
                    new DDLBind(ddlLeadSource, Source, "Source", "SourceID");
                    new DDLBind(ddlLeadSource, Source, "Source", "SourceID");
                    SearchLeadSource();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.ToString();
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
            }
        }
        void SearchLeadSource()
        {
            int? LeadSourceID = ddlLeadSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlLeadSource.SelectedValue);
            string LeadSource = ddlLeadSource.SelectedValue == "0" ? (string)null : ddlLeadSource.SelectedItem.Text.Trim();

            List<PLeadSource> leadSources = new BPresalesMasters().GetLeadSource(LeadSourceID, LeadSource);

            gvLeadSource.DataSource = leadSources;
            gvLeadSource.DataBind();
        }

        protected void btnSearchLeadSource_Click(object sender, EventArgs e)
        {
            try
            {
                SearchLeadSource();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void gvLeadSource_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchLeadSource();
            gvLeadSource.PageIndex = e.NewPageIndex;
            gvLeadSource.DataBind();
        }

        protected void BtnAddLeadSource_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddLeadSource = (Button)gvLeadSource.FooterRow.FindControl("BtnAddLeadSource");

                string LeadSource = ((TextBox)gvLeadSource.FooterRow.FindControl("txtLeadSource")).Text.Trim();
                if (string.IsNullOrEmpty(LeadSource))
                {
                    lblMessage.Text = "Please Enter the Main Application Name";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddLeadSource.Text == "Add")
                {
                    success = new BDMS_Service().InsertOrUpdateMainApplication(null, LeadSource, true, PSession.User.UserID, "Add");
                    if (success == 1)
                    {
                        SearchLeadSource();
                        lblMessage.Text = "Lead Source Created Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Lead Source Name Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Lead Source Not Created Successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BDMS_Service().InsertOrUpdateMainApplication(Convert.ToInt32(HiddenID.Value), LeadSource, true, PSession.User.UserID, "Update");
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        SearchLeadSource();
                        lblMessage.Text = "Lead Source Updated Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Lead Source Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Lead Source Not Updated Successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lblLeadSourceEdit_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lblLeadSourceDelete_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
    }
}