using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDashboard
{
    public partial class Task_Dashboard : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        DateTime? From = null;
        DateTime? To = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Dashboard');</script>");
            if (!IsPostBack)
            {
                new FillDropDownt().Category(ddlCategory, null, null);
                ddlCategory_SelectedIndexChanged(null, null);
                FillStatusCount();
            }
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            new FillDropDownt().SubCategory(ddlSubcategory, null, null, CategoryID);
        }        
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Open")
            {
                Session["TaskStatusID"] = 1;
            }
            else if (lbActions.Text == "Assigned")
            {
                Session["TaskStatusID"] = 2;
            }
            else if (lbActions.Text == "Quotation")
            {
                Session["TaskStatusID"] = 3;
            }
            else if (lbActions.Text == "Won")
            {
                Session["TaskStatusID"] = 4;
            }
            else if (lbActions.Text == "Lost")
            {
                Session["TaskStatusID"] = 5;
            }
            else if (lbActions.Text == "Cancelled")
            {
                Session["TaskStatusID"] = 6;
            }
            //Response.Redirect("lead.aspx");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillStatusCount();
        }
        void FillStatusCount()
        {
            lblCreated.Text = "0";
            lblOpen.Text = "0";
            lblWaitingForApproval.Text = "0";
            lblAssigned.Text = "0";
            lblInProgress.Text = "0";
            lblResolved.Text = "0";
            lblClosed.Text = "0";
            DateTime TodayDate = DateTime.Now.Date;

            if (!string.IsNullOrEmpty(txtTicketFrom.Text))
            {
                From = Convert.ToDateTime(txtTicketFrom.Text);
            }
            if (!string.IsNullOrEmpty(txtTicketTo.Text))
            {
                To = Convert.ToDateTime(txtTicketTo.Text).AddDays(1);
            }
            int? CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            int? SubCategoryID = ddlSubcategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSubcategory.SelectedValue);
            DataSet ds = new BTickets().GetTicketDetailsCountByStatus(CategoryID, SubCategoryID, PSession.User.UserID, From, To);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblCreated.Text = ds.Tables[0].Compute("Sum(TotalCreated)", "").ToString();
                lblOpen.Text = ds.Tables[0].Compute("Sum(Opened)", "").ToString();
                lblWaitingForApproval.Text = ds.Tables[0].Compute("Sum(WaitingForApproval)", "").ToString();
                lblAssigned.Text = ds.Tables[0].Compute("Sum(Assigned)", "").ToString();
                lblInProgress.Text = ds.Tables[0].Compute("Sum(InProgress)", "").ToString();
                lblResolved.Text = ds.Tables[0].Compute("Sum(Resolved)", "").ToString();
                lblClosed.Text = ds.Tables[0].Compute("Sum(Closed)", "").ToString();

                gvTickets.DataSource = ds.Tables[0];
                gvTickets.DataBind();
            }
            ClientScript.RegisterStartupScript(GetType(), "hwa1", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(TaskStatusChart); ", true);
        }
        [WebMethod]
        public static List<object> TaskStatusChart(string Category, string Subcategory, string DateFrom, string DateTo)
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "Year&Month", "Created","Progress", "Closed" });
            int? CategoryID = Category == "0" ? (int?)null : Convert.ToInt32(Category);
            int? SubcategoryID = Subcategory == "0" ? (int?)null : Convert.ToInt32(Subcategory);

            DateTime? From = string.IsNullOrEmpty(DateFrom)? (DateTime?)null : Convert.ToDateTime(DateFrom);
            DateTime? To = string.IsNullOrEmpty(DateTo) ? (DateTime?)null : Convert.ToDateTime(DateTo);

            DataSet ds = new BTickets().GetTicketDetailsCountByStatusForChart(CategoryID, SubcategoryID, PSession.User.UserID, From, To);

            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    chartData.Add(new object[] { Convert.ToString(dr["Year"])+"-"+Convert.ToString(dr["Month"]), Convert.ToInt32(dr["TotalCreated"]), Convert.ToInt32(dr["InProgress"]), Convert.ToInt32(dr["Closed"]) });
                }
            }
            return chartData;
        }
    }
}