using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class DefaultUserModule : System.Web.UI.Page
    {
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
                if (Session["PModuleAccess"] == null)
                {
                    Session["PModuleAccess"] = new List<PModuleAccess>();
                }
                return (List<PModuleAccess>)Session["PModuleAccess"];
            }
            set
            {
                Session["PModuleAccess"] = value;
            }
        }

        public List<PSubModuleChild> SubModuleChileByUserID
        {
            get
            {
                if (Session["SubModuleChile"] == null)
                {
                    Session["SubModuleChile"] = new List<PSubModuleChild>();
                }
                return (List<PSubModuleChild>)Session["SubModuleChile"];
            }
            set
            {
                Session["SubModuleChile"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealer Employee » Roles');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                FillDealerDesignation();
                fillDashboard(); 
            }
        }

        public List<PDMS_DealerDesignation> DealerDesignation
        {
            get
            {
                if (Session["PDMS_DealerDesignation"] == null)
                {
                    Session["PDMS_DealerDesignation"] = new List<PDMS_DealerDesignation>();
                }
                return (List<PDMS_DealerDesignation>)Session["PDMS_DealerDesignation"];
            }
            set
            {
                Session["PDMS_DealerDesignation"] = value;
            }
        }

        protected void ibtnUserArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerDesignation.PageIndex > 0)
            {
                gvDealerDesignation.PageIndex = gvDealerDesignation.PageIndex - 1;
                UserBind(gvDealerDesignation, lblRowCount);
            }
        }
        protected void ibtnUserArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerDesignation.PageCount > gvDealerDesignation.PageIndex)
            {
                gvDealerDesignation.PageIndex = gvDealerDesignation.PageIndex + 1;
                UserBind(gvDealerDesignation, lblRowCount);
            }
        }

        void UserBind(GridView gv, Label lbl)
        {
            gv.DataSource = DealerDesignation;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + DealerDesignation;
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDealerDesignation();
            new BDMS_Dealer().GetDealerDesignation(Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }

        void FillDealerDesignation()
        {
            int? DealerDepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            DealerDesignation = new BDMS_Dealer().GetDealerDesignation(DealerDepartmentID, null, null);
             
            gvDealerDesignation.DataSource = DealerDesignation;
            gvDealerDesignation.DataBind();

            if (DealerDesignation.Count == 0)
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
                lblRowCount.Text = (((gvDealerDesignation.PageIndex) * gvDealerDesignation.PageSize) + 1) + " - " + (((gvDealerDesignation.PageIndex) * gvDealerDesignation.PageSize) + gvDealerDesignation.Rows.Count) + " of " + DealerDesignation.Count;
            }
        }
        protected void lbRole_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            pnlUser.Visible = false;
            pnlModule.Visible = true; 
            btnUpdate.Visible = true;
            btnBack.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            int DealerDesignationID = Convert.ToInt32(((Label)gvDealerDesignation.Rows[index].FindControl("lblDealerDesignationID")).Text);
            ViewState["DealerDesignationID"] = DealerDesignationID;

            List<PModuleAccess> AccessModule = new BUser().GetDMSModuleAll();
            ModuleAccess = AccessModule;
            gvModule.DataSource = AccessModule;
            gvModule.DataBind(); 
            SubModuleChileByUserID = new BUser().GetSubModuleChileByDealerDesignationID(DealerDesignationID);
            List<PModuleAccess> EAccessModule = new BUser().GetDMSModuleByByDealerDesignationID(DealerDesignationID);

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
            //PUser u = new BUser().GetUserDetails(UserID);
            //lblUserID.Text = u.UserName.ToString();
            //lblUserName.Text = u.ContactName;
          
             

            //List<PDMS_Dashboard> Dashboard = new BDMS_Dashboard().GetDashboardByUserID(UserID);

            //for (int i = 0; i < dlDashboard.Items.Count; i++)
            //{
            //    int DashboardID = Convert.ToInt32(dlDashboard.DataKeys[i].ToString());
            //    if (Dashboard.Where(A => A.DashboardID == DashboardID).Count() != 0)
            //    {
            //        CheckBox cbSMId = (CheckBox)dlDashboard.Items[i].FindControl("cbSMId");
            //        cbSMId.Checked = true;
            //    }
            //}


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
            if (new BUser().InsertOrUpdateDefaultUserPermition(Convert.ToInt32(ViewState["EId"]), AccessM, AccessSCM, AccessDB, PSession.User.UserID))
            { 
                btnUpdate.Visible = false;
                btnBack.Visible = false;
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
 
        protected void gvDealerDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerDesignation.PageIndex = e.NewPageIndex;
            FillDealerDesignation(); 
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
            btnUpdate.Visible = false;
            btnBack.Visible = false; 
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
    }
}