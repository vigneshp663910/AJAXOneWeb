using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class MessageAnnouncement : System.Web.UI.Page
    {
        public int? DealerID, DealerDepartmentID, DealerDesignationID;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Message Announcement');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                DealerDepartmentID = (ddlDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
                new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, DealerDepartmentID, null, null);
                DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
                List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
                new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
                Fill();
            }
        }
        void Fill()
        {
            //DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            //DealerDepartmentID = (ddlDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            //DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);

            //UserLst = new BUser().GetUsers(null, txtEmp.Text, null, "", DealerID, IsEnabled, ContactName, DepartmentID, DesignationID);

            //UserLst = UserLst.FindAll(m => m.ContactName.ToLower().Contains(txtContactName.Text.Trim().ToLower()) && ((m.ajaxOne == ajaxOne) || (ajaxOne == null)) && ((m.IsLocked == IsLocked) || (IsLocked == null)));
            //gvUser.DataSource = UserLst;

            ////gvUser.DataSource = u;
            //gvUser.DataBind();

            //if (UserLst.Count == 0)
            //{
            //    lblRowCount.Visible = false;
            //    ibtnUserArrowLeft.Visible = false;
            //    ibtnUserArrowRight.Visible = false;
            //}
            //else
            //{
            //    lblRowCount.Visible = true;
            //    ibtnUserArrowLeft.Visible = true;
            //    ibtnUserArrowRight.Visible = true;
            //    lblRowCount.Text = (((gvUser.PageIndex) * gvUser.PageSize) + 1) + " - " + (((gvUser.PageIndex) * gvUser.PageSize) + gvUser.Rows.Count) + " of " + UserLst.Count;
            //}
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
            DealerDepartmentID = (ddlDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, DealerDepartmentID, null, null);
            DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
            new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerDepartmentID = (ddlDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, DealerDepartmentID, null, null);
            DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
            new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
            new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}