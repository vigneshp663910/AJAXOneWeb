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
    public partial class Application : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMaster_Application; } }
        public List<PDMS_SubApplication> subApp
        {
            get
            {
                if (Session["PDMS_SubApplication"] == null)
                {
                    Session["PDMS_SubApplication"] = new List<PDMS_SubApplication>();
                }
                return (List<PDMS_SubApplication>)Session["PDMS_SubApplication"];
            }
            set
            {
                Session["PDMS_SubApplication"] = value;
            }
        }

        public List<PDMS_MainApplication> mainApplications
        {
            get
            {
                if (Session["PDMS_MainApplication"] == null)
                {
                    Session["PDMS_MainApplication"] = new List<PDMS_MainApplication>();
                }
                return (List<PDMS_MainApplication>)Session["PDMS_MainApplication"];
            }
            set
            {
                Session["PDMS_MainApplication"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Application');</script>");
            if (!IsPostBack)
            {
                try
                {
                    new DDLBind(ddlMainApplication, new BDMS_Service().GetMainApplication(null, null), "MainApplication", "MainApplicationID", true, "Select");
                    SearchMainApplication();
                    SearchSubApplication();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
            }
        }



        protected void ibtnSubAppArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvSubApplication.PageIndex > 0)
            {
                gvSubApplication.PageIndex = gvSubApplication.PageIndex - 1;
                SubAppBind(gvSubApplication, lblRowCount, subApp);
            }
        }
        protected void ibtnSubAppArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvSubApplication.PageCount > gvSubApplication.PageIndex)
            {
                gvSubApplication.PageIndex = gvSubApplication.PageIndex + 1;
                SubAppBind(gvSubApplication, lblRowCount, subApp);
            }
        }

        void SubAppBind(GridView gv, Label lbl, List<PDMS_SubApplication> subApp)
        {
            gv.DataSource = subApp;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + subApp.Count;
        }


        void SearchSubApplication()
        {
            int? SubApplicationID = null;
            string SubApplicationName = null;
            int? MainApplicationID = ddlMainApplication.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMainApplication.SelectedValue);

            subApp = new BDMS_Service().GetSubApplication(MainApplicationID, SubApplicationID, SubApplicationName);

            gvSubApplication.DataSource = subApp;
            gvSubApplication.DataBind();
            if (subApp.Count == 0)
            {
                subApp.Add(new PDMS_SubApplication() { });
                gvSubApplication.DataSource = subApp;
                gvSubApplication.DataBind();
                lblRowCount.Visible = false;
                ibtnSubAppArrowLeft.Visible = false;
                ibtnSubAppArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnSubAppArrowLeft.Visible = true;
                ibtnSubAppArrowRight.Visible = true;
                lblRowCount.Text = (((gvSubApplication.PageIndex) * gvSubApplication.PageSize) + 1) + " - " + (((gvSubApplication.PageIndex) * gvSubApplication.PageSize) + gvSubApplication.Rows.Count) + " of " + subApp.Count;
            }

            Boolean EditAlloved = false;

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;

            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EmployeeEdit).Count() != 0)
            {
                EditAlloved = true;
            }
            for (int i = 0; i < gvSubApplication.Rows.Count; i++)
            {
                LinkButton lblSubApplicationEdit = (LinkButton)gvSubApplication.Rows[i].FindControl("lblSubApplicationEdit");
                if (EditAlloved == true)
                {
                    lblSubApplicationEdit.Visible = true;
                }
                else
                {
                    lblSubApplicationEdit.Visible = false;
                }
            }

        }


        void SearchMainApplication()
        {
            //int? MainApplicationID = ddlMainApplication.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMainApplication.SelectedValue);
            //string MainApplicationName = ddlMainApplication.SelectedValue == "0" ? (string)null : ddlMainApplication.SelectedItem.Text.Trim();
            int? MainApplicationID = (int?)null;
            string MainApplicationName = (string)null;

            //List<PDMS_MainApplication> mainApplications = new BDMS_Service().GetMainApplication(MainApplicationID, MainApplicationName);
            mainApplications = new BDMS_Service().GetMainApplication(MainApplicationID, MainApplicationName);

            gvMainApplication.DataSource = mainApplications;
            gvMainApplication.DataBind();
            lblRowCountMainApp.Text = (((gvMainApplication.PageIndex) * gvMainApplication.PageSize) + 1) + " - " + (((gvMainApplication.PageIndex) * gvMainApplication.PageSize) + gvMainApplication.Rows.Count) + " of " + mainApplications.Count;
            if (mainApplications.Count == 0)
            {
                PDMS_MainApplication pDMS_MainApplication = new PDMS_MainApplication();
                mainApplications.Add(pDMS_MainApplication);
                gvMainApplication.DataSource = mainApplications;
                gvMainApplication.DataBind();
            }
            Boolean EditAlloved = false;

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;

            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EmployeeEdit).Count() != 0)
            {
                EditAlloved = true;
            }
            for (int i = 0; i < gvMainApplication.Rows.Count; i++)
            {
                LinkButton lblMainApplicationEdit = (LinkButton)gvMainApplication.Rows[i].FindControl("lblMainApplicationEdit");
                if (EditAlloved == true)
                {
                    lblMainApplicationEdit.Visible = true;
                }
                else
                {
                    lblMainApplicationEdit.Visible = false;
                }
            }
        }

        protected void BtnAddMainApplication_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddMainApplication = (Button)gvMainApplication.FooterRow.FindControl("BtnAddMainApplication");

                string MainApplication = ((TextBox)gvMainApplication.FooterRow.FindControl("txtMainApplication")).Text.Trim();
                if (string.IsNullOrEmpty(MainApplication))
                {
                    lblMessage.Text = "Please Enter Main Application";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddMainApplication.Text == "Add")
                {
                    success = new BDMS_Service().InsertOrUpdateMainApplication(null, MainApplication, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        SearchMainApplication();
                        lblMessage.Text = "Main Application Created Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Main Application Already Found";
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
                    success = new BDMS_Service().InsertOrUpdateMainApplication(Convert.ToInt32(HiddenID.Value), MainApplication, true, PSession.User.UserID);
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lblMainApplicationDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                LinkButton lblMainApplicationDelete = (LinkButton)sender;
                long MainApplicationID = Convert.ToInt32(lblMainApplicationDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lblMainApplicationDelete.NamingContainer);
                string MainApplication = ((Label)row.FindControl("lblMainApplication")).Text.Trim();
                success = new BDMS_Service().InsertOrUpdateMainApplication(MainApplicationID, MainApplication, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
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

            gvSubApplication.PageIndex = e.NewPageIndex;
            SearchSubApplication();
            gvSubApplication.DataBind();
        }

        protected void lblSubApplicationDelete_Click(object sender, EventArgs e)
        {
            try
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

                success = new BDMS_Service().InsertOrUpdateSubApplication(SubApplicationID, MainApplicationID, SubApplication, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void BtnAddSubApplication_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddSubApplication = (Button)gvSubApplication.FooterRow.FindControl("BtnAddSubApplication");
                DropDownList ddlGMainApplication = (DropDownList)gvSubApplication.FooterRow.FindControl("ddlGMainApplication");
                if (ddlGMainApplication.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select Main Application";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                int MainApplicationID = Convert.ToInt32(ddlGMainApplication.SelectedValue);
                string SubApplication = ((TextBox)gvSubApplication.FooterRow.FindControl("txtSubApplication")).Text.Trim();
                if (string.IsNullOrEmpty(SubApplication))
                {
                    lblMessage.Text = "Please Enter Sub Application";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (BtnAddSubApplication.Text == "Add")
                {
                    success = new BDMS_Service().InsertOrUpdateSubApplication(null, MainApplicationID, SubApplication, true, Convert.ToInt32(PSession.User.UserID));
                    if (success == 1)
                    {
                        SearchSubApplication();
                        lblMessage.Text = "Sub Application Created Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Sub Application Already Found";
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
                    success = new BDMS_Service().InsertOrUpdateSubApplication(Convert.ToInt32(HiddenID.Value), MainApplicationID, SubApplication, true, Convert.ToInt32(PSession.User.UserID));
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
                        lblMessage.Text = "Sub Application Already Found";
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void gvSubApplication_DataBound(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlGMainApplication = gvSubApplication.FooterRow.FindControl("ddlGMainApplication") as DropDownList;
                new DDLBind(ddlGMainApplication, new BDMS_Service().GetMainApplication(null, null), "MainApplication", "MainApplicationID", true, "Select");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lblMainApplicationEdit_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lblSubApplicationEdit_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void ddlMainApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SearchSubApplication();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void ibtnMainAppArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMainApplication.PageIndex > 0)
            {
                gvMainApplication.PageIndex = gvMainApplication.PageIndex - 1;
                MainAppBind(gvMainApplication, lblRowCountMainApp, mainApplications);
            }
        }
        protected void ibtnMainAppArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMainApplication.PageCount > gvMainApplication.PageIndex)
            {
                gvMainApplication.PageIndex = gvMainApplication.PageIndex + 1;
                MainAppBind(gvMainApplication, lblRowCountMainApp, mainApplications);
            }
        }

        void MainAppBind(GridView gv, Label lbl, List<PDMS_MainApplication> MainApp)
        {
            gv.DataSource = MainApp;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + mainApplications.Count;
        }
    }
}