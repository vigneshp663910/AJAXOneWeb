using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSales
{
    public partial class SalesCommissionClaimPerformanceReport : System.Web.UI.Page
    {
        public DataTable Claim
        {
            get
            {
                if (Session["SalesCommissionClaimPerformanceReport"] == null)
                {
                    Session["SalesCommissionClaimPerformanceReport"] = new DataTable();
                }
                return (DataTable)Session["SalesCommissionClaimPerformanceReport"];
            }
            set
            {
                Session["SalesCommissionClaimPerformanceReport"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["PageName"] = "Claim Performance Report";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Commission » Claim Performance Report');</script>");

            lblMessage.Text = "";
            if (!IsPostBack)
            { 
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
            }
        }

        void fillEquipmentPopulationReport()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                long? SalesCommissionClaimID = null;
                long? SalesQuotationID = null;

                string ClaimNumber = txtClaimNumber.Text.Trim();
                string ClaimDateFrom = txtDateFrom.Text.Trim();
                string ClaimDateTo = txtDateTo.Text.Trim();
                int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                Claim = new BSalesCommissionClaim().GetSalesCommissionClaimPerformanceReport(SalesCommissionClaimID, SalesQuotationID, ClaimNumber, ClaimDateFrom, ClaimDateTo, StatusID, DealerID).Tables[0];

                gvEquipment.PageIndex = 0;
                gvEquipment.DataSource = Claim;
                gvEquipment.DataBind();
                if (Claim.Rows.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + Claim.Rows.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaim", "fillClaim", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipment.PageIndex > 0)
            {
                gvEquipment.DataSource = Claim;
                gvEquipment.PageIndex = gvEquipment.PageIndex - 1;

                gvEquipment.DataBind();
                lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + Claim.Rows.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipment.PageCount > gvEquipment.PageIndex)
            {
                gvEquipment.DataSource = Claim;
                gvEquipment.PageIndex = gvEquipment.PageIndex + 1;
                gvEquipment.DataBind();
                lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + Claim.Rows.Count;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(Claim, "Claim Performance Report");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquipment.DataSource = Claim;
            gvEquipment.PageIndex = e.NewPageIndex;
            gvEquipment.DataBind();
            lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + Claim.Rows.Count;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillEquipmentPopulationReport();
        }
    }
}