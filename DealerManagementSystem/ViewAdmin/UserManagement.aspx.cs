using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class UserManagement : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_UserManagement; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        public List<PModuleAccess> ModuleAccess
        {
            get
            {
                if (ViewState["PModuleAccess"] == null)
                {
                    ViewState["PModuleAccess"] = new List<PModuleAccess>();
                }
                return (List<PModuleAccess>)ViewState["PModuleAccess"];
            }
            set
            {
                ViewState["PModuleAccess"] = value;
            }
        }

        public List<PSubModuleChild> SubModuleChileByUserID
        {
            get
            {
                if (ViewState["SubModuleChile"] == null)
                {
                    ViewState["SubModuleChile"] = new List<PSubModuleChild>();
                }
                return (List<PSubModuleChild>)ViewState["SubModuleChile"];
            }
            set
            {
                ViewState["SubModuleChile"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » User');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
                FillUser();                             
            }
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }
        public List<PUser> UserLst
        {
            get
            {
                if (ViewState["PUser"] == null)
                {
                    ViewState["PUser"] = new List<PUser>();
                }
                return (List<PUser>)ViewState["PUser"];
            }
            set
            {
                ViewState["PUser"] = value;
            }
        }
        protected void ibtnUserArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvUser.PageIndex > 0)
            {
                gvUser.PageIndex = gvUser.PageIndex - 1;
                UserBind(gvUser, lblRowCount, UserLst);
            }
        }
        protected void ibtnUserArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvUser.PageCount > gvUser.PageIndex)
            {
                gvUser.PageIndex = gvUser.PageIndex + 1;
                UserBind(gvUser, lblRowCount, UserLst);
            }
        }
        void UserBind(GridView gv, Label lbl, List<PUser> UserLst)
        {
            gv.DataSource = UserLst;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + UserLst.Count;
        }
        void FillUser()
        {
            //int? EmpId = null;
            //if (!string.IsNullOrEmpty(txtEmp.Text))
            //{
            //    EmpId = Convert.ToInt32(txtEmp.Text);
            //}
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            string ContactName = null; if (!string.IsNullOrEmpty(txtContactName.Text)) { ContactName = txtContactName.Text; }

            bool? IsLocked = null;
            if (ddlIsLocked.SelectedValue == "1") { IsLocked = true; } else if (ddlIsLocked.SelectedValue == "2") { IsLocked = false; }

            bool? IsEnabled = null;
            if (ddlIsEnabled.SelectedValue == "1") { IsEnabled = true; } else if (ddlIsEnabled.SelectedValue == "2") { IsEnabled = false; }
              

            int?  DepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            int? DesignationID = ddlDesignation.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);

            // List<PUser> u = new BUser().GetUsers(null, txtEmp.Text, null, "", DealerID, IsEnabled, ContactName, DepartmentID, DesignationID);
            //u = u.FindAll(m => m.SystemCategoryID == (short)SystemCategory.Dealer && m.ContactName.ToLower().Contains(txtContactName.Text.Trim().ToLower()));
            //u = u.FindAll(m => m.ContactName.ToLower().Contains(txtContactName.Text.Trim().ToLower()));
        
            UserLst = new BUser().GetUsers(null, txtEmp.Text, null, "", DealerID, IsEnabled, ContactName, DepartmentID, DesignationID);

            UserLst = UserLst.FindAll(m => m.ContactName.ToLower().Contains(txtContactName.Text.Trim().ToLower()) && ((m.IsLocked == IsLocked) || (IsLocked == null)));
            gvUser.DataSource = UserLst;

            //gvUser.DataSource = u;
            gvUser.DataBind();

            if (UserLst.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnUserArrowLeft.Visible = false;
                ibtnUserArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnUserArrowLeft.Visible = true;
                ibtnUserArrowRight.Visible = true;
                lblRowCount.Text = (((gvUser.PageIndex) * gvUser.PageSize) + 1) + " - " + (((gvUser.PageIndex) * gvUser.PageSize) + gvUser.Rows.Count) + " of " + UserLst.Count;
            } 
        }
        protected void lbEmpId_Click(object sender, EventArgs e)
        {
            fillDashboard();

            divList.Visible = false;
            pnlUser.Visible = false;
            pnlModule.Visible = true;
            pnlDealer.Visible = false;
            //btnUpdate.Visible = true;
            btnBack.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            int UserID = Convert.ToInt32(((Label)gvUser.Rows[index].FindControl("lblUserID")).Text);
            SubModuleChileByUserID = new BUser().GetSubModuleChileByUserID(UserID);
            ViewState["EId"] = UserID;

            List<PModuleAccess> AccessModule = new BUser().GetDMSModuleAll();
            ModuleAccess = AccessModule;
            gvModule.DataSource = AccessModule;
            gvModule.DataBind();
            fillDealer();

            List<PModuleAccess> EAccessModule = new BUser().GetDMSModuleByUser(UserID, null, null);
            if (EAccessModule.Count() != 0)
            {
                for (int j = 0; j < gvModule.Rows.Count; j++)
                {
                    int ModuleMasterID = Convert.ToInt32(gvModule.DataKeys[j].Value);
                    DataList dlModule = (DataList)gvModule.Rows[j].FindControl("dlModule");
                    if (EAccessModule.Find(s => s.ModuleMasterID == ModuleMasterID) == null)
                    {
                        continue;
                    }
                    List<PSubModuleAccess> SubModule = new List<PSubModuleAccess>();
                    SubModule = EAccessModule.Find(s => s.ModuleMasterID == ModuleMasterID).SubModuleAccess;

                    for (int i = 0; i < dlModule.Items.Count; i++)
                    {
                        int dlModuleID = Convert.ToInt32(dlModule.DataKeys[i].ToString());
                        if (SubModule.Where(A => A.SubModuleMasterID == dlModuleID).Count() != 0)
                        {
                            CheckBox cbSMId = (CheckBox)dlModule.Items[i].FindControl("cbSMId");
                            cbSMId.Checked = true;
                        }
                    }
                }
            }
            PUser u = new BUser().GetUserDetails(UserID);
            lblUserID.Text = u.UserName.ToString();
            lblUserName.Text = u.ContactName;
            lblDepartment.Text = u.Department.DealerDepartment;
            lblDesignation.Text = u.Designation.DealerDesignation;
            //if (u.UserTypeID != (short)UserTypes.Dealer)
            //{
            pnlDealer.Visible = true;
            List<PDealer> Dealer = new BDealer().GetDealerByUserID(UserID);

            for (int i = 0; i < dlDealer.Items.Count; i++)
            {
                int DealerID = Convert.ToInt32(dlDealer.DataKeys[i].ToString());
                if (Dealer.Where(A => A.DID == DealerID).Count() != 0)
                {
                    CheckBox cbSMId = (CheckBox)dlDealer.Items[i].FindControl("cbSMId");
                    cbSMId.Checked = true;
                }
            }
            //}

            List<PDMS_Dashboard> Dashboard = new BDMS_Dashboard().GetDashboardByUserID(UserID);

            for (int i = 0; i < dlDashboard.Items.Count; i++)
            {
                int DashboardID = Convert.ToInt32(dlDashboard.DataKeys[i].ToString());
                if (Dashboard.Where(A => A.DashboardID == DashboardID).Count() != 0)
                {
                    CheckBox cbSMId = (CheckBox)dlDashboard.Items[i].FindControl("cbSMId");
                    cbSMId.Checked = true;
                }
            }

            List<PUserMobileFeature> UserMobileFeature = new BUser().GetUserMobileFeatureAccessByUserID(UserID);

            for (int i = 0; i < dlMobileFeatureAccess.Items.Count; i++)
            {
                int UserMobileFeatureID = Convert.ToInt32(dlMobileFeatureAccess.DataKeys[i].ToString());
                if (UserMobileFeature.Where(A => A.UserMobileFeatureID == UserMobileFeatureID).Count() != 0)
                {
                    CheckBox cbSMId = (CheckBox)dlMobileFeatureAccess.Items[i].FindControl("cbSMId");
                    cbSMId.Checked = true;
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            List<int> AccessM = new List<int>();
            List<int> AccessSCM = new List<int>();
            for (int j = 0; j < gvModule.Rows.Count; j++)
            {
                DataList dlModule = (DataList)gvModule.Rows[j].FindControl("dlModule");
                for (int i = 0; i < dlModule.Items.Count; i++)
                {
                    CheckBox cbSMId = (CheckBox)dlModule.Items[i].FindControl("cbSMId");
                    int dlModuleID = Convert.ToInt32(dlModule.DataKeys[i].ToString());
                    if (cbSMId.Checked)
                    {
                        AccessM.Add(dlModuleID);
                    }
                    DataList dlChildModule = (DataList)dlModule.Items[i].FindControl("dlChildModule");
                    for (int k = 0; k < dlChildModule.Items.Count; k++)
                    {
                        int SubModuleChildID = Convert.ToInt32(dlChildModule.DataKeys[k].ToString());
                        CheckBox cbChildId = (CheckBox)dlChildModule.Items[k].FindControl("cbChildId");
                        if (cbChildId.Checked)
                        {
                            AccessSCM.Add(SubModuleChildID);
                        }
                    }
                }
            }
            List<int> AccessD = new List<int>();
            for (int i = 0; i < dlDealer.Items.Count; i++)
            {
                CheckBox cbSMId = (CheckBox)dlDealer.Items[i].FindControl("cbSMId");
                int dlModuleID = Convert.ToInt32(dlDealer.DataKeys[i].ToString());
                if (cbSMId.Checked)
                {
                    AccessD.Add(dlModuleID);
                }
            }
            List<int> AccessDB = new List<int>();
            for (int i = 0; i < dlDashboard.Items.Count; i++)
            {
                CheckBox cbSMId = (CheckBox)dlDashboard.Items[i].FindControl("cbSMId");
                int dlDashboardID = Convert.ToInt32(dlDashboard.DataKeys[i].ToString());
                if (cbSMId.Checked)
                {
                    AccessDB.Add(dlDashboardID);
                }
            }

            List<int> MobileFeature = new List<int>();
            for (int i = 0; i < dlMobileFeatureAccess.Items.Count; i++)
            {
                CheckBox cbSMId = (CheckBox)dlMobileFeatureAccess.Items[i].FindControl("cbSMId");
                int UserMobileFeatureID = Convert.ToInt32(dlMobileFeatureAccess.DataKeys[i].ToString());
                if (cbSMId.Checked)
                {
                    MobileFeature.Add(UserMobileFeatureID);
                }
            }

            if (new BUser().UpdateUserPermition(Convert.ToInt64(ViewState["EId"]), AccessM, AccessSCM, AccessD, AccessDB, MobileFeature, PSession.User.UserID))
            {
                //List<PModuleAccess> AccessModule = new BUser().GetDMSModuleAll();
                //ModuleAccess = AccessModule;
                //gvModule.DataSource = AccessModule;
                //gvModule.DataBind();
                //fillDealer();
                //btnUpdate.Visible = false;
                btnBack.Visible = false;
                pnlDealer.Visible = false;
                pnlModule.Visible = false;
                pnlUser.Visible = true;

                lblMessage.Text = "It is updated successfully";
                lblMessage.ForeColor = Color.Green;
                divList.Visible = true;
            }
            else
            {
                lblMessage.Text = "It is not updated successfully ";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true;

        }
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int supplierPOID = Convert.ToInt32(gvModule.DataKeys[e.Row.RowIndex].Value);
                    DataList dlModule = (DataList)e.Row.FindControl("dlModule");

                    Label lblPscID = (Label)e.Row.FindControl("lblPscID");
                    GridView gvFileAttached = (GridView)e.Row.FindControl("gvFileAttached");

                    List<PSubModuleAccess> supplierPurchaseOrderLines = new List<PSubModuleAccess>();
                    supplierPurchaseOrderLines = ModuleAccess.Find(s => s.ModuleMasterID == supplierPOID).SubModuleAccess;

                    dlModule.DataSource = supplierPurchaseOrderLines;
                    dlModule.DataBind();
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_MTTR_Report", "fillMTTR", ex);
                throw ex;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillUser();
        }
        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            FillUser();
        }
        void fillDealer()
        {
            List<PDealer> dealer = new BDealer().GetDealerList(null, "", "");
            dlDealer.DataSource = dealer;
            dlDealer.DataBind();
        }

        //void fillDealerDLL()
        //{ 
        //    ddlDealer.DataTextField = "CodeWithName";
        //    ddlDealer.DataValueField = "DID";
        //    ddlDealer.DataSource = PSession.User.Dealer;
        //    ddlDealer.DataBind();
        //    ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        //}
        protected void cbAllDealer_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dlDealer.Items.Count; i++)
            {
                CheckBox cbSMId = (CheckBox)dlDealer.Items[i].FindControl("cbSMId");
                if (cbAllDealer.Checked)
                    cbSMId.Checked = true;
                else
                    cbSMId.Checked = false;
            }
        }
        protected void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            DataList dl = (DataList)gvModule.Rows[gvRow.RowIndex].FindControl("dlModule");
            CheckBox cbAll = (CheckBox)gvModule.Rows[gvRow.RowIndex].FindControl("cbAll");
            for (int i = 0; i < dl.Items.Count; i++)
            {
                CheckBox cbSMId = (CheckBox)dl.Items[i].FindControl("cbSMId");
                if (cbAll.Checked)
                    cbSMId.Checked = true;
                else
                    cbSMId.Checked = false;
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            //btnUpdate.Visible = false;
            btnBack.Visible = false;
            pnlDealer.Visible = false;
            pnlModule.Visible = false;
            pnlUser.Visible = true;

        }
        protected void cbAllDashboard_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dlDashboard.Items.Count; i++)
            {
                CheckBox cbSMId = (CheckBox)dlDashboard.Items[i].FindControl("cbSMId");
                if (cbAllDashboard.Checked)
                    cbSMId.Checked = true;
                else
                    cbSMId.Checked = false;
            }
        }
        void fillDashboard()
        {
            List<PDMS_Dashboard> Dashboard = new BDMS_Dashboard().GetDashboardAll(null);
            dlDashboard.DataSource = Dashboard;
            dlDashboard.DataBind();
             
            dlMobileFeatureAccess.DataSource = new BUser().GetUserMobileFeature();
            dlMobileFeatureAccess.DataBind();
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Button btnEdit = (Button)gvUser.Rows[index].FindControl("btnEdit");
            Button btnUpdate = (Button)gvUser.Rows[index].FindControl("btnUpdate");
            Button btnCancel = (Button)gvUser.Rows[index].FindControl("btnCancel");
            btnEdit.Visible = false;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;

            Label lblPassWord = (Label)gvUser.Rows[index].FindControl("lblPassWord");
            Label lblUserName = (Label)gvUser.Rows[index].FindControl("lblUserName");
            Label lblContactName = (Label)gvUser.Rows[index].FindControl("lblContactName");
            Label lblMail = (Label)gvUser.Rows[index].FindControl("lblMail");
            Label lblContactNumber = (Label)gvUser.Rows[index].FindControl("lblContactNumber");
            Label lblExternalReferenceID = (Label)gvUser.Rows[index].FindControl("lblExternalReferenceID");

            // TextBox txtPassWord = (TextBox)gvUser.Rows[index].FindControl("txtPassWord");
            TextBox txtUserName = (TextBox)gvUser.Rows[index].FindControl("txtUserName");
            TextBox txtContactName = (TextBox)gvUser.Rows[index].FindControl("txtContactName");
            TextBox txtMail = (TextBox)gvUser.Rows[index].FindControl("txtMail");
            TextBox txtContactNumber = (TextBox)gvUser.Rows[index].FindControl("txtContactNumber");
            TextBox txtExternalReferenceID = (TextBox)gvUser.Rows[index].FindControl("txtExternalReferenceID");

            CheckBox cbIsTechnician = (CheckBox)gvUser.Rows[index].FindControl("cbIsTechnician");
            CheckBox cbIsLocked = (CheckBox)gvUser.Rows[index].FindControl("cbIsLocked");
            CheckBox cbIsEnabled = (CheckBox)gvUser.Rows[index].FindControl("cbIsEnabled"); 

            //lblPassWord.Visible = false;
            lblUserName.Visible = false;
            lblContactName.Visible = false;
            lblMail.Visible = false;
            lblContactNumber.Visible = false;
            lblExternalReferenceID.Visible = false;

            //txtPassWord.Visible = true;
            txtUserName.Visible = true;
            txtContactName.Visible = true;
            txtMail.Visible = true;
            txtContactNumber.Visible = true;
            txtExternalReferenceID.Visible = true;

            cbIsTechnician.Enabled = true;
            cbIsLocked.Enabled = true;
            cbIsEnabled.Enabled = true; 
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Button btnEdit = (Button)gvUser.Rows[index].FindControl("btnEdit");
            Button btnUpdate = (Button)gvUser.Rows[index].FindControl("btnUpdate");
            Button btnCancel = (Button)gvUser.Rows[index].FindControl("btnCancel");
            btnEdit.Visible = true;
            btnUpdate.Visible = false;
            btnCancel.Visible = false;

            Label lblPassWord = (Label)gvUser.Rows[index].FindControl("lblPassWord");
            Label lblUserName = (Label)gvUser.Rows[index].FindControl("lblUserName");
            Label lblContactName = (Label)gvUser.Rows[index].FindControl("lblContactName");
            Label lblMail = (Label)gvUser.Rows[index].FindControl("lblMail");
            Label lblContactNumber = (Label)gvUser.Rows[index].FindControl("lblContactNumber");
            Label lblExternalReferenceID = (Label)gvUser.Rows[index].FindControl("lblExternalReferenceID");

            //TextBox txtPassWord = (TextBox)gvUser.Rows[index].FindControl("txtPassWord");
            TextBox txtUserName = (TextBox)gvUser.Rows[index].FindControl("txtUserName");
            TextBox txtContactName = (TextBox)gvUser.Rows[index].FindControl("txtContactName");
            TextBox txtMail = (TextBox)gvUser.Rows[index].FindControl("txtMail");
            TextBox txtContactNumber = (TextBox)gvUser.Rows[index].FindControl("txtContactNumber");
            TextBox txtExternalReferenceID = (TextBox)gvUser.Rows[index].FindControl("txtExternalReferenceID");

            CheckBox cbIsTechnician = (CheckBox)gvUser.Rows[index].FindControl("cbIsTechnician");
            CheckBox cbIsLocked = (CheckBox)gvUser.Rows[index].FindControl("cbIsLocked");
            CheckBox cbIsEnabled = (CheckBox)gvUser.Rows[index].FindControl("cbIsEnabled"); 

            //lblPassWord.Visible = true;
            lblUserName.Visible = true;
            lblContactName.Visible = true;
            lblMail.Visible = true;
            lblContactNumber.Visible = true;
            lblExternalReferenceID.Visible = true;

            //txtPassWord.Visible = false;
            txtUserName.Visible = false;
            txtContactName.Visible = false;
            txtMail.Visible = false;
            txtContactNumber.Visible = false;
            txtExternalReferenceID.Visible = false;

            cbIsTechnician.Enabled = false;
            cbIsLocked.Enabled = false;
            cbIsEnabled.Enabled = false; 
        }
        protected void GvbtnUpdate_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Label lblUserID = (Label)gvUser.Rows[index].FindControl("lblUserID");

            Button btnEdit = (Button)gvUser.Rows[index].FindControl("btnEdit");
            Button btnUpdate = (Button)gvUser.Rows[index].FindControl("btnUpdate");
            Button btnCancel = (Button)gvUser.Rows[index].FindControl("btnCancel");
            btnEdit.Visible = true;
            btnUpdate.Visible = false;
            btnCancel.Visible = false;

            //Label lblPassWord = (Label)gvUser.Rows[index].FindControl("lblPassWord");
            Label lblUserName = (Label)gvUser.Rows[index].FindControl("lblUserName");
            Label lblContactName = (Label)gvUser.Rows[index].FindControl("lblContactName");
            Label lblMail = (Label)gvUser.Rows[index].FindControl("lblMail");
            Label lblContactNumber = (Label)gvUser.Rows[index].FindControl("lblContactNumber");
            Label lblExternalReferenceID = (Label)gvUser.Rows[index].FindControl("lblExternalReferenceID");

            //TextBox txtPassWord = (TextBox)gvUser.Rows[index].FindControl("txtPassWord");
            TextBox txtUserName = (TextBox)gvUser.Rows[index].FindControl("txtUserName");
            TextBox txtContactName = (TextBox)gvUser.Rows[index].FindControl("txtContactName");
            TextBox txtMail = (TextBox)gvUser.Rows[index].FindControl("txtMail");
            TextBox txtContactNumber = (TextBox)gvUser.Rows[index].FindControl("txtContactNumber");
            TextBox txtExternalReferenceID = (TextBox)gvUser.Rows[index].FindControl("txtExternalReferenceID");

            CheckBox cbIsTechnician = (CheckBox)gvUser.Rows[index].FindControl("cbIsTechnician");
            CheckBox cbIsLocked = (CheckBox)gvUser.Rows[index].FindControl("cbIsLocked");
            CheckBox cbIsEnabled = (CheckBox)gvUser.Rows[index].FindControl("cbIsEnabled"); 

            //if (string.IsNullOrEmpty(txtState.Text.Trim()))
            //{
            //    lblMessage.Text = "Please Enter the State";
            //    lblMessage.ForeColor = Color.Red;
            //    return;
            //}

            PUser userDAO = new BUser().GetUserDetails(Convert.ToInt32(lblUserID.Text));
            userDAO.UserName = txtUserName.Text.Trim();
            // userDAO.PassWord = txtPassWord.Text.Trim();
            userDAO.ContactName = txtContactName.Text.Trim();
            userDAO.Mail = txtMail.Text.Trim();
            userDAO.ContactNumber = txtContactNumber.Text.Trim();
            userDAO.ExternalReferenceID = txtExternalReferenceID.Text.Trim();

            userDAO.IsTechnician = cbIsTechnician.Checked;
            userDAO.IsLocked = cbIsLocked.Checked;
            userDAO.IsEnabled = cbIsEnabled.Checked; 
            userDAO.UpdatedBy = PSession.User.UserID;
            userDAO.UpdatedOn = DateTime.Now;

            if (new BUser().InsertOrUpdateUser(userDAO))
            {
                lblMessage.Text = "User Updated Successfully";
                lblMessage.ForeColor = Color.Green;

                btnEdit.Visible = true;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;

              //  lblPassWord.Visible = true;
                lblUserName.Visible = true;
                lblContactName.Visible = true;
                lblMail.Visible = true;
                lblContactNumber.Visible = true;
                lblExternalReferenceID.Visible = true;

               // txtPassWord.Visible = false;
                txtUserName.Visible = false;
                txtContactName.Visible = false;
                txtMail.Visible = false;
                txtContactNumber.Visible = false;
                txtExternalReferenceID.Visible = false;

                cbIsTechnician.Enabled = false;
                cbIsLocked.Enabled = false;
                cbIsEnabled.Enabled = false; 
                //lblPassWord.Text = txtPassWord.Text.Trim();
                lblContactName.Text = txtContactName.Text.Trim();
                lblMail.Text = txtMail.Text.Trim();
                lblContactNumber.Text = txtContactNumber.Text.Trim();
            }
            else
            {
                lblMessage.Text = "State is not Updated Successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }         
        protected void gvSubModuleChild_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void dlModule_DataBinding(object sender, EventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {                
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_MTTR_Report", "fillMTTR", ex);
                throw ex;
            }
        }
        protected void dlModule_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblSubModuleMasterID = (Label)e.Item.FindControl("lblSubModuleMasterID");
                DataList dlChildModule = (DataList)e.Item.FindControl("dlChildModule");

                List<PSubModuleChild> supplierPurchaseOrderLines = new List<PSubModuleChild>();
                supplierPurchaseOrderLines = new BUser().GetSubModuleChileAll(Convert.ToInt32(lblSubModuleMasterID.Text));
                dlChildModule.DataSource = supplierPurchaseOrderLines;
                dlChildModule.DataBind();

                for (int i = 0; i < dlChildModule.Items.Count; i++)
                {
                    int SubModuleChildID = Convert.ToInt32(dlChildModule.DataKeys[i].ToString());
                    if (SubModuleChileByUserID.Where(A => A.SubModuleChildID == SubModuleChildID).Count() != 0)
                    {
                        CheckBox cbSMId = (CheckBox)dlChildModule.Items[i].FindControl("cbChildId");
                        cbSMId.Checked = true;
                    }
                }
            }
        }
        protected void btnUserResetPassword_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Label lblUserID = (Label)gvUser.Rows[index].FindControl("lblUserID");

            if (new BUser().UpdateUserResetPasswordByAdmin(Convert.ToInt64(lblUserID.Text), PSession.User.UserID))
            {
                lblMessage.Text = "User reset successfully.";
                lblMessage.ForeColor = Color.Green;
                PUser userDetails = new BUser().GetUserDetails(Convert.ToInt64(lblUserID.Text));
                if (!string.IsNullOrEmpty(userDetails.UserName))
                {
                    string messageBody = messageBody = new EmailManager().GetMailNotificationTemplate("UserResetPassword.htm");
                    messageBody = messageBody.Replace("@@Addresse", userDetails.ContactName);
                    messageBody = messageBody.Replace("@@UserName", userDetails.UserName);
                    messageBody = messageBody.Replace("@@Password", "ajax@123");
                    messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);
                    new EmailManager().MailSend(userDetails.Mail, "", "Password Reset Request", messageBody);

                    //messageBody = "Dear User, Your Password for login is ajax@123. From AJAX ENGG";
                    //new EmailManager().SendSMS(userDetails.Employee.ContactNumber, messageBody);
                }
                else
                {
                    lblMessage.Text = "Invalid UserName...!";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
            else
            {
                lblMessage.Text = "User not reset successfully.";
                lblMessage.ForeColor = Color.Red;
            }

        }
    }
}