using Business;
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
    public partial class ModulePermissionList : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_ModulePermissionList; } }
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Modulewise Permission List');</script>");
            try
            {
                if (!IsPostBack)
                {
                    new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                    new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                    new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
                    GetMainModule(ddlMainModule, null, null);
                    GetSubModule(ddlSubModule, null, null, null);
                    GetSubModuleChildMaster(ddlChildModule, null, null, null);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        public List<PDealerUserPermission> UserwiseDealerList
        {
            get
            {
                if (ViewState["UserwiseDealerList"] == null)
                {
                    ViewState["UserwiseDealerList"] = new List<PDealerUserPermission>();
                }
                return (List<PDealerUserPermission>)ViewState["UserwiseDealerList"];
            }
            set
            {
                ViewState["UserwiseDealerList"] = value;
            }
        }
        private void GetMainModule(DropDownList ddl, int? ModuleMasterID, string ModuleName)
        {
            List<PModuleAccess> MainModule = new BUser().GetModuleMaster(ModuleMasterID, ModuleName);
            ddl.DataValueField = "ModuleMasterID";
            ddl.DataTextField = "ModuleName";
            ddl.DataSource = MainModule;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void GetSubModule(DropDownList ddl, int? ModuleMasterID, int? SubModuleMasterID, string SubModuleName)
        {
            List<PSubModuleAccess> MainModule = new BUser().GetSubModuleMaster(ModuleMasterID, SubModuleMasterID, SubModuleName);
            ddl.DataValueField = "SubModuleMasterID";
            ddl.DataTextField = "SubModuleName";
            ddl.DataSource = MainModule;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void GetSubModuleChildMaster(DropDownList ddl, int? SubModuleMasterID, int? SubModuleChildID, string ChildName)
        {
            List<PSubModuleChild> MainModule = new BUser().GetSubModuleChildMaster(SubModuleMasterID, SubModuleChildID, ChildName);
            ddl.DataValueField = "SubModuleChildID";
            ddl.DataTextField = "ChildName";
            ddl.DataSource = MainModule;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void FillGrid()
        {
            try
            {
                if(ddlSubModule.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select SubModule...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                int? DepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
                int? DesignationID = ddlDesignation.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
                int? ModuleMasterID = (ddlMainModule.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlMainModule.SelectedValue);
                int? SubModuleMasterID = (ddlSubModule.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlSubModule.SelectedValue);
                int? SubModuleChildID = (ddlChildModule.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlChildModule.SelectedValue);
                UserwiseDealerList = new BUser().GetUserByModulePermissions(DealerID, DepartmentID, DesignationID, ModuleMasterID, SubModuleMasterID, SubModuleChildID);

                List<PDealerUserPermission> Dealers = new List<PDealerUserPermission>();
                foreach (PDealerUserPermission pDealerUserPermission in UserwiseDealerList)
                {
                    PDealerUserPermission User = new PDealerUserPermission();
                    User.UserName = pDealerUserPermission.UserName;
                    User.ContactName = pDealerUserPermission.ContactName;
                    User.MailID = pDealerUserPermission.MailID;
                    User.DealerDesignation = pDealerUserPermission.DealerDesignation;
                    User.DealerDepartment = pDealerUserPermission.DealerDepartment;
                    User.ModuleName = pDealerUserPermission.ModuleName;
                    User.SubModuleName = pDealerUserPermission.SubModuleName;
                    User.ChildName = pDealerUserPermission.ChildName;
                    Dealers.Add(User);
                }
                UserwiseDealerList = Dealers.ToList();


                if (UserwiseDealerList.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                    gvDealerUsers.DataSource = null;
                    gvDealerUsers.DataBind();
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((gvDealerUsers.PageIndex) * gvDealerUsers.PageSize) + 1) + " - " + (((gvDealerUsers.PageIndex) * gvDealerUsers.PageSize) + gvDealerUsers.Rows.Count) + " of " + UserwiseDealerList.Count;
                    gvDealerUsers.DataSource = UserwiseDealerList;
                    gvDealerUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PageIndex = 1;
                FillGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void gvDealerUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerUsers.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerUsers.PageIndex > 0)
            {
                gvDealerUsers.PageIndex = gvDealerUsers.PageIndex - 1;
                UserBind(gvDealerUsers, lblRowCount, UserwiseDealerList);
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerUsers.PageCount > gvDealerUsers.PageIndex)
            {
                gvDealerUsers.PageIndex = gvDealerUsers.PageIndex + 1;
                UserBind(gvDealerUsers, lblRowCount, UserwiseDealerList);
            }
        }
        void UserBind(GridView gv, Label lbl, List<PDealerUserPermission> DealerList)
        {
            gv.DataSource = DealerList;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + DealerList.Count;
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }
        protected void ddlMainModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? ModuleMasterID = (ddlMainModule.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlMainModule.SelectedValue);
            GetSubModule(ddlSubModule, ModuleMasterID, null, null);
            GetSubModuleChildMaster(ddlChildModule, null, null, null);
        }
        protected void ddlSubModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? SubModuleMasterID = (ddlSubModule.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlSubModule.SelectedValue);
            GetSubModuleChildMaster(ddlChildModule, SubModuleMasterID, null, null);
        }
    }
}