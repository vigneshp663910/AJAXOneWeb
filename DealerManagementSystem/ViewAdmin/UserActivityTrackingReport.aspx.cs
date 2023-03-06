using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class UserActivityTrackingReport : System.Web.UI.Page
    {
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
        public List<PUser> UserActivityTracking
        {
            get
            {
                if (Session["UserActivityTracking"] == null)
                {
                    Session["UserActivityTracking"] = new List<PUser>();
                }
                return (List<PUser>)Session["UserActivityTracking"];
            }
            set
            {
                Session["UserActivityTracking"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » User Activity Tracking');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
                FillUsersActivityTracking();
            }
        }
        void FillUsersActivityTracking()
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            string ContactName = null; if (!string.IsNullOrEmpty(txtContactName.Text)) { ContactName = txtContactName.Text; }

            bool? IsLocked = null;
            if (ddlIsLocked.SelectedValue == "1") { IsLocked = true; } else if (ddlIsLocked.SelectedValue == "2") { IsLocked = false; }

            bool? IsEnabled = null;
            if (ddlIsEnabled.SelectedValue == "1") { IsEnabled = true; } else if (ddlIsEnabled.SelectedValue == "2") { IsEnabled = false; }

            bool? ajaxOne = null;
            if (ddlAJAXOne.SelectedValue == "1") { ajaxOne = true; } else if (ddlAJAXOne.SelectedValue == "2") { ajaxOne = false; }

            int? DepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            int? DesignationID = ddlDesignation.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);

            int RowCount = 0;
            UserActivityTracking = new BUser().GetUsersActivityTracking(null, txtEmp.Text, null, "", DealerID, IsEnabled, ContactName, DepartmentID, DesignationID, IsLocked, ajaxOne, PageIndex, gvUser.PageSize, out RowCount);

            gvUser.DataSource = null;
            gvUser.DataBind();

            if (RowCount == 0)
            {
                gvUser.DataSource = null;
                gvUser.DataBind();
                lblRowCount.Visible = false;
                ibtnUserArrowLeft.Visible = false;
                ibtnUserArrowRight.Visible = false;
            }
            else
            {
                gvUser.DataSource = UserActivityTracking;
                gvUser.DataBind();
                PageCount = (RowCount + gvUser.PageSize - 1) / gvUser.PageSize;
                lblRowCount.Visible = true;
                ibtnUserArrowLeft.Visible = true;
                ibtnUserArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvUser.PageSize) + 1) + " - " + (((PageIndex - 1) * gvUser.PageSize) + gvUser.Rows.Count) + " of " + RowCount;
            }
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }
        protected void ibtnUserArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillUsersActivityTracking();
            }
        }
        protected void ibtnUserArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillUsersActivityTracking();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            FillUsersActivityTracking();
        }
        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            FillUsersActivityTracking();
        }
        protected void gvUser_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<PUser> Users = UserActivityTracking;
            //var HoliDay = pHoliDay;
            var User = Users;
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;
            if (Sortdir == "ASC")
            {
                User = Sort<PUser>(User, SortExp, SortDirection.Ascending);
            }
            else
            {
                User = Sort<PUser>(User, SortExp, SortDirection.Descending);
            }
            this.gvUser.DataSource = User;
            this.gvUser.DataBind();
        }
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<PUser> Sort<TKey>(List<PUser> Users, string sortBy, SortDirection direction)
        {
            PropertyInfo property = Users.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return Users.OrderBy(e => property.GetValue(e, null)).ToList<PUser>();
            }
            else
            {
                return Users.OrderByDescending(e => property.GetValue(e, null)).ToList<PUser>();
            }
        }
    }
}