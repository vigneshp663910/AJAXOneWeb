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

namespace DealerManagementSystem.ViewAdmin
{
    public partial class UserAccessBySubModule : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_UserAccessBySubModule; } }
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
        int? ModuleMasterID = null;
        int? SubModuleMasterID = null;
        int? DepartmentID = null;
        int? DesignationID = null;
        bool? IsEnabled = null;
        bool? IsActive = null;
        public List<PUserAccessBySubModule> GetUserAccessBySubModule
        {
            get
            {
                if (ViewState["GetUserAccessBySubModule"] == null)
                {
                    ViewState["GetUserAccessBySubModule"] = new List<PUserAccessBySubModule>();
                }
                return (List<PUserAccessBySubModule>)ViewState["GetUserAccessBySubModule"];
            }
            set
            {
                ViewState["GetUserAccessBySubModule"] = value;
            }
        }
        public List<PUserAccessBySubModule> GetUserAccessBySubModuleUpdated
        {
            get
            {
                if (ViewState["GetUserAccessBySubModuleUpdated"] == null)
                {
                    ViewState["GetUserAccessBySubModuleUpdated"] = new List<PUserAccessBySubModule>();
                }
                return (List<PUserAccessBySubModule>)ViewState["GetUserAccessBySubModuleUpdated"];
            }
            set
            {
                ViewState["GetUserAccessBySubModuleUpdated"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » User Access By Sub Module');</script>");
            lblMessage.Text = "";
            try
            {
                if (!IsPostBack)
                {
                    new DDLBind(ddlMainModule, new BUser().GetModuleMaster(null, null), "ModuleName", "ModuleMasterID", true, "Select");
                    ddlSubModule.Items.Insert(0, new ListItem("Select", "0"));
                    new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                    new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);

                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }
        protected void ddlMainModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModuleMasterID = (ddlMainModule.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlMainModule.SelectedValue);
            new DDLBind(ddlSubModule, new BUser().GetSubModuleMaster(ModuleMasterID, null, null), "SubModuleName", "SubModuleMasterID", true, "Select");
        }
        private void FillGrid()
        {            
            try
            {
                if (ddlMainModule.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select Main Module...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (ddlSubModule.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select Sub Module...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                SubModuleMasterID = (ddlSubModule.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlSubModule.SelectedValue);
                DepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
                DesignationID = ddlDesignation.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
                if (ddlIsActive.SelectedValue == "1") { IsActive = true; } else if (ddlIsActive.SelectedValue == "2") { IsActive = false; }
                if (ddlIsEnabled.SelectedValue == "1") { IsEnabled = true; } else if (ddlIsEnabled.SelectedValue == "2") { IsEnabled = false; }

                PApiResult Result = new BUser().GetUserAccessBySubModule(SubModuleMasterID, DepartmentID, DesignationID, IsActive, IsEnabled, PageIndex, gvUsers.PageSize);
                GetUserAccessBySubModule = JsonConvert.DeserializeObject<List<PUserAccessBySubModule>>(JsonConvert.SerializeObject(Result.Data));
                GetUserAccessBySubModuleUpdated = null;
                gvUsers.DataSource = GetUserAccessBySubModule;
                gvUsers.DataBind();

                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvUsers.PageSize - 1) / gvUsers.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvUsers.PageSize) + 1) + " - " + (((PageIndex - 1) * gvUsers.PageSize) + gvUsers.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                PageCount = 0;
                PageIndex = 1;
                FillGrid();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillGrid();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillGrid();
            }
        }
        protected void ChkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label lblUserID = (Label)gvRow.FindControl("lblUserID");
                CheckBox ChkIsActive = (CheckBox)gvRow.FindControl("ChkIsActive");
                if ((GetUserAccessBySubModuleUpdated.Any(z => z.User.UserID == Convert.ToInt32(lblUserID.Text))))
                {
                    GetUserAccessBySubModuleUpdated.RemoveAll(p => p.User.UserID == Convert.ToInt32(lblUserID.Text));
                }
                if (!GetUserAccessBySubModule.Any(z => z.User.UserID == Convert.ToInt32(lblUserID.Text) && z.IsActive == ChkIsActive.Checked))
                {
                    PUserAccessBySubModule item = new PUserAccessBySubModule();
                    item.User = new PUser { UserID = Convert.ToInt32(lblUserID.Text) };
                    item.IsActive = ChkIsActive.Checked;
                    GetUserAccessBySubModuleUpdated.Add(item);
                }
                BtnSave.Visible = true;
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
            }
        }
        protected void cbIsActiveH_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbIsActiveH = (CheckBox)sender;
            CheckBox chckheader = (CheckBox)gvUsers.HeaderRow.FindControl("cbIsActiveH");
            chckheader.Checked = cbIsActiveH.Checked;

            foreach (GridViewRow Row in gvUsers.Rows)
            {
                Label lblUserID = (Label)Row.FindControl("lblUserID");
                CheckBox ChkIsActive = (CheckBox)Row.FindControl("ChkIsActive");
                ChkIsActive.Checked = cbIsActiveH.Checked;

                if ((GetUserAccessBySubModuleUpdated.Any(z => z.User.UserID == Convert.ToInt32(lblUserID.Text))))
                {
                    GetUserAccessBySubModuleUpdated.RemoveAll(p => p.User.UserID == Convert.ToInt32(lblUserID.Text));
                }
                if (!GetUserAccessBySubModule.Any(z => z.User.UserID == Convert.ToInt32(lblUserID.Text) && z.IsActive == ChkIsActive.Checked))
                {
                    PUserAccessBySubModule item = new PUserAccessBySubModule();
                    item.User = new PUser { UserID = Convert.ToInt32(lblUserID.Text) };
                    item.IsActive = cbIsActiveH.Checked;
                    GetUserAccessBySubModuleUpdated.Add(item);
                }
            }
            GetUserAccessBySubModuleUpdated.RemoveAll(p => GetUserAccessBySubModule.Any(z => z.User.UserID == p.User.UserID && z.IsActive == p.IsActive));
            BtnSave.Visible = true;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (PUserAccessBySubModule user in GetUserAccessBySubModuleUpdated)
                {
                    PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("User/InsertOrUpdateUserAccessBySubModule?UserID=" + Convert.ToInt32(user.User.UserID) + "&SubModuleAccessID=" + Convert.ToInt32(ddlSubModule.SelectedValue) + "&IsActive=" + user.IsActive));
                    if (result.Status == PApplication.Failure)
                    {
                        lblMessage.Text = result.Message;
                        return;
                    }
                    lblMessage.Text = result.Message;
                    lblMessage.ForeColor = Color.Green;
                }
                FillGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
    }
}