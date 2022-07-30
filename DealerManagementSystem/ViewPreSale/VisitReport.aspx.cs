using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class VisitReport : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        public DataTable GVVisitReport
        {
            get
            {
                if (Session["GVVisitReport"] == null)
                {
                    Session["GVVisitReport"] = new DataTable();
                }
                return (DataTable)Session["GVVisitReport"];
            }
            set
            {
                Session["GVVisitReport"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Report » Visit Report');</script>");

                lblMessage.Text = "";

                if (!IsPostBack)
                {
                    new BDealer().FillDealerAndEngneer(ddlDealer, ddlDealerEmployee);
                    new DDLBind().Year(ddlYear,2022, false);
                    new DDLBind().Month(ddlMonth, false);
                    //List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, null, true, null, null, null);
                    //new DDLBind(ddlDealerEmployee, DealerUser, "ContactName", "UserID");
                    //new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
                }
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try {
                new BXcel().ExporttoExcel(GVVisitReport, "Visit Report");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void ibtnVisitArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (gvVisitReport.PageIndex > 0)
                {
                    gvVisitReport.PageIndex = gvVisitReport.PageIndex - 1;
                    VisitReportBind(gvVisitReport, lblRowCount);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void ibtnVisitArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (gvVisitReport.PageCount > gvVisitReport.PageIndex)
                {
                    gvVisitReport.PageIndex = gvVisitReport.PageIndex + 1;
                    VisitReportBind(gvVisitReport, lblRowCount);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void gvVisitReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvVisitReport.PageIndex = e.NewPageIndex;
                FillVisitReport();
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
                FillVisitReport();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void FillVisitReport()
        {
            lblMessage.Text = string.Empty;
            try
            {
                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                int? DealerEmployeeID = ddlDealerEmployee.SelectedValue == "0" || ddlDealerEmployee.SelectedValue == "" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
                int Year =   Convert.ToInt32(ddlYear.SelectedValue);
                int Month = Convert.ToInt32(ddlMonth.SelectedValue); ;

                GVVisitReport = new BColdVisit().GetVisitReport(DealerID, DealerEmployeeID, Year, Month, PSession.User.UserID);
                gvVisitReport.DataSource = GVVisitReport;
                gvVisitReport.DataBind();

                if (GVVisitReport.Rows.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnVisitArrowLeft.Visible = false;
                    ibtnVisitArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnVisitArrowLeft.Visible = true;
                    ibtnVisitArrowRight.Visible = true;
                    lblRowCount.Text = (((gvVisitReport.PageIndex) * gvVisitReport.PageSize) + 1) + " - " + (((gvVisitReport.PageIndex) * gvVisitReport.PageSize) + gvVisitReport.Rows.Count) + " of " + GVVisitReport.Rows.Count;
                }
            }
            catch (Exception e)
            {
                lblMessage.Text = e.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void VisitReportBind(GridView gv, Label lbl)
        {
            gv.DataSource = GVVisitReport;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + GVVisitReport.Rows.Count;
        }

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        { 
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlDealerEmployee, DealerUser, "ContactName", "UserID");
        }
    }
}