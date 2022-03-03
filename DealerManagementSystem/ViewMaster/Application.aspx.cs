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
    public partial class Application : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new DDLBind(ddlMainApplication, new BDMS_Service().GetMainApplication(null, null), "MainApplication", "MainApplicationID", true, "All Application");
                SearchMainApplication();
                SearchSubApplication();
            }
        }

        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            SearchMainApplication();
            SearchSubApplication();
        }
        void SearchSubApplication()
        {
            int? SubApplicationID = null;
            string SubApplicationName = null;
            int? MainApplicationID = ddlMainApplication.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMainApplication.SelectedValue);

            List<PDMS_SubApplication> subApplications = new BDMS_Service().GetSubApplication(MainApplicationID, SubApplicationID, SubApplicationName);

            gvSubApplication.DataSource = subApplications;
            gvSubApplication.DataBind();
        }
        void SearchMainApplication()
        {
            int? MainApplicationID = ddlMainApplication.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMainApplication.SelectedValue);
            string MainApplicationName = ddlMainApplication.SelectedValue == "0" ? (string)null : ddlMainApplication.SelectedItem.Text.Trim();

            List<PDMS_MainApplication> mainApplications = new BDMS_Service().GetMainApplication(MainApplicationID, MainApplicationName);

            gvMainApplication.DataSource = mainApplications;
            gvMainApplication.DataBind();
        }

        protected void BtnAddMainApplication_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            int success = 0;
            Button BtnAddMainApplication = (Button)gvMainApplication.FooterRow.FindControl("BtnAddMainApplication");
            
            string MainApplication = ((TextBox)gvMainApplication.FooterRow.FindControl("txtMainApplication")).Text.Trim();
            if (string.IsNullOrEmpty(MainApplication))
            {
                lblMessage.Text = "Please Enter the Main Application Name";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            if (BtnAddMainApplication.Text == "Add")
            {
                success = new BDMS_Service().InsertOrUpdateMainApplication(null, MainApplication, true, PSession.User.UserID,"Add");
                if (success == 1)
                {
                    SearchMainApplication();
                    lblMessage.Text = "Main Application Created Successfully...!";
                    lblMessage.ForeColor = Color.Green;
                    return;
                }
                else if (success == 2)
                {
                    lblMessage.Text = "Main Application Name Already Found";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    lblMessage.Text = "Main Application Not Created Successfully...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
            else
            {
                success = new BDMS_Service().InsertOrUpdateMainApplication(Convert.ToInt32(HiddenID.Value), MainApplication, true, PSession.User.UserID, "Update");
                if (success == 1)
                {
                    HiddenID.Value = null;
                    SearchMainApplication();
                    lblMessage.Text = "Main Application Updated Successfully...!";
                    lblMessage.ForeColor = Color.Green;
                    return;
                }
                else if (success == 2)
                {
                    lblMessage.Text = "Main Application Name Already Found";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    lblMessage.Text = "Main Application Not Updated Successfully...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
        }

        protected void lblMainApplicationDelete_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            int success = 0;
            LinkButton lblMainApplicationDelete = (LinkButton)sender;
            long MainApplicationID = Convert.ToInt32(lblMainApplicationDelete.CommandArgument);
            GridViewRow row = (GridViewRow)(lblMainApplicationDelete.NamingContainer);
            string MainApplication = ((Label)row.FindControl("lblMainApplication")).Text.Trim();
            success = new BDMS_Service().InsertOrUpdateMainApplication(MainApplicationID, MainApplication, false, PSession.User.UserID, "Delete");
            if (success == 1)
            {
                SearchMainApplication();
                lblMessage.Text = "MainApplication was Deleted successfully";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "MainApplication was not Deleted successfully";
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }

        protected void gvMainApplication_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchMainApplication();
            gvMainApplication.PageIndex = e.NewPageIndex;
            gvMainApplication.DataBind();
        }

        protected void gvSubApplication_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchSubApplication();
            gvSubApplication.PageIndex = e.NewPageIndex;
            gvSubApplication.DataBind();
        }

        protected void lblSubApplicationDelete_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            int success = 0;
            LinkButton lblSubApplicationDelete = (LinkButton)sender;
            long SubApplicationID = Convert.ToInt32(lblSubApplicationDelete.CommandArgument);
            GridViewRow row = (GridViewRow)(lblSubApplicationDelete.NamingContainer);
            int MainApplicationID = Convert.ToInt32(((Label)row.FindControl("lblMainApplicationID")).Text.Trim());
            string SubApplication = ((Label)row.FindControl("lblSubApplication")).Text.Trim();

            success = new BDMS_Service().InsertOrUpdateSubApplication(SubApplicationID, MainApplicationID, SubApplication, false, PSession.User.UserID,"Delete");
            if (success == 1)
            {
                SearchSubApplication();
                lblMessage.Text = "SubApplication was Deleted successfully";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "SubApplication was not Deleted successfully";
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }

        protected void BtnAddSubApplication_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            int success = 0;
            Button BtnAddSubApplication = (Button)gvSubApplication.FooterRow.FindControl("BtnAddSubApplication");
            DropDownList ddlGMainApplication = (DropDownList)gvSubApplication.FooterRow.FindControl("ddlGMainApplication");
            if (ddlGMainApplication.SelectedValue == "0")
            {
                lblMessage.Text = "Please Select the Main Application Name";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            int MainApplicationID = Convert.ToInt32(ddlGMainApplication.SelectedValue);
            string SubApplication = ((TextBox)gvSubApplication.FooterRow.FindControl("txtSubApplication")).Text.Trim();
            if (string.IsNullOrEmpty(SubApplication))
            {
                lblMessage.Text = "Please Enter the Sub Application Name";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (BtnAddSubApplication.Text == "Add")
            {
                success = new BDMS_Service().InsertOrUpdateSubApplication(null, MainApplicationID, SubApplication, true, Convert.ToInt32(PSession.User.UserID),"Add");
                if (success == 1)
                {
                    SearchSubApplication();
                    lblMessage.Text = "Sub Application Created Successfully...!";
                    lblMessage.ForeColor = Color.Green;
                    return;
                }
                else if (success == 2)
                {
                    lblMessage.Text = "Sub Application Name Already Found";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    lblMessage.Text = "Sub Application Not Created Successfully...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
            else
            {
                success = new BDMS_Service().InsertOrUpdateSubApplication(Convert.ToInt32(HiddenID.Value), MainApplicationID, SubApplication, true, Convert.ToInt32(PSession.User.UserID),"Update");
                if (success == 1)
                {
                    HiddenID.Value = null;
                    SearchSubApplication();
                    lblMessage.Text = "Sub Application Updated Successfully...!";
                    lblMessage.ForeColor = Color.Green;
                    return;
                }
                else if (success == 2)
                {
                    lblMessage.Text = "Sub Application Name Already Found";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    lblMessage.Text = "Sub Application Not Updated Successfully...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
        }

        protected void gvSubApplication_DataBound(object sender, EventArgs e)
        {
            DropDownList ddlGMainApplication = gvSubApplication.FooterRow.FindControl("ddlGMainApplication") as DropDownList;
            new DDLBind(ddlGMainApplication, new BDMS_Service().GetMainApplication(null, null), "MainApplication", "MainApplicationID", true, "All Application");
        }

        protected void lblMainApplicationEdit_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            LinkButton lblMainApplicationEdit = (LinkButton)sender;
            TextBox txtMainApplication = (TextBox)gvMainApplication.FooterRow.FindControl("txtMainApplication");
            Button BtnAddMainApplication = (Button)gvMainApplication.FooterRow.FindControl("BtnAddMainApplication");
            GridViewRow row = (GridViewRow)(lblMainApplicationEdit.NamingContainer);
            string MainApplication = ((Label)row.FindControl("lblMainApplication")).Text.Trim();
            txtMainApplication.Text = MainApplication;
            HiddenID.Value = Convert.ToString(lblMainApplicationEdit.CommandArgument);
            BtnAddMainApplication.Text = "Update";
        }

        protected void lblSubApplicationEdit_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            LinkButton lblSubApplicationEdit = (LinkButton)sender;
            DropDownList ddlGMainApplication = (DropDownList)gvSubApplication.FooterRow.FindControl("ddlGMainApplication");
            TextBox txtSubApplication = (TextBox)gvSubApplication.FooterRow.FindControl("txtSubApplication");
            Button BtnAddSubApplication = (Button)gvSubApplication.FooterRow.FindControl("BtnAddSubApplication");
            GridViewRow row = (GridViewRow)(lblSubApplicationEdit.NamingContainer);
            Label lblMainApplicationID = (Label)row.FindControl("lblMainApplicationID");
            ddlGMainApplication.SelectedValue = lblMainApplicationID.Text;
            string SubApplication = ((Label)row.FindControl("lblSubApplication")).Text.Trim();
            txtSubApplication.Text = SubApplication;
            HiddenID.Value = Convert.ToString(lblSubApplicationEdit.CommandArgument);
            BtnAddSubApplication.Text = "Update";
        }
    }
}