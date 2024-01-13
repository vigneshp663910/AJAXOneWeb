using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewFinance.Reports
{
    public partial class DealerBalanceConfirmationReport : BasePage
    {
        private DataTable DealerBalanceConfirmationRpt
        {
            get
            {
                if (ViewState["DealerBalanceConfirmationReport"] == null)
                {
                    ViewState["DealerBalanceConfirmationReport"] = new DataTable();
                }
                return (DataTable)ViewState["DealerBalanceConfirmationReport"];
            }
            set
            {
                ViewState["DealerBalanceConfirmationReport"] = value;
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
            lblMessage.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Finanace » Reports » Dealer Balance Confirmation Report');</script>");
            if (!IsPostBack)
            {
                DealerBalanceConfirmationRpt = null;
                //new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithDisplayName", "DID", true, "All Dealer");
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                new DDLBind(ddlBalanceConfirmationStatus, new BDealer().GetDealerBalanceConfirmationStatus(null, null), "Status", "StatusID");
            }
            DealerBalanceConfirmationRptBind(gvDealerBalanceConfirmationRpt, lblRowCountDealerBalConFirm, DealerBalanceConfirmationRpt);
        }
        void DealerBalanceConfirmationRptBind(GridView gv, Label lbl, DataTable DealerBalanceConfirmationReport)
        {
            gv.DataSource = DealerBalanceConfirmationReport;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + DealerBalanceConfirmationReport.Rows.Count;
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillDealerBalanceConfirmation();
        }
        void FillDealerBalanceConfirmation()
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            string DateFrom = txtFromDate.Text.Trim();
            string DateTo = txtToDate.Text.Trim();
            int? BalanceConfirmationStatusID = ddlBalanceConfirmationStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlBalanceConfirmationStatus.SelectedValue);

            DealerBalanceConfirmationRpt = new BDealer().GetDealerBalanceConfirmationReport(DealerID, BalanceConfirmationStatusID, DateFrom, DateTo);

            if (DealerBalanceConfirmationRpt.Rows.Count == 0)
            {
                lblRowCountDealerBalConFirm.Visible = false;
                ibtnDealerBalConFirmArrowLeft.Visible = false;
                ibtnDealerBalConFirmArrowRight.Visible = false;
                gvDealerBalanceConfirmationRpt.DataSource = DealerBalanceConfirmationRpt;
                gvDealerBalanceConfirmationRpt.DataBind();
            }
            else
            {
                lblRowCountDealerBalConFirm.Visible = true;
                ibtnDealerBalConFirmArrowLeft.Visible = true;
                ibtnDealerBalConFirmArrowRight.Visible = true;
                DealerBalanceConfirmationRptBind(gvDealerBalanceConfirmationRpt, lblRowCountDealerBalConFirm, DealerBalanceConfirmationRpt);
            }
        }
        protected void ibtnDealerBalConFirmArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerBalanceConfirmationRpt.PageIndex > 0)
            {
                gvDealerBalanceConfirmationRpt.PageIndex = gvDealerBalanceConfirmationRpt.PageIndex - 1;
                DealerBalanceConfirmationRptBind(gvDealerBalanceConfirmationRpt, lblRowCountDealerBalConFirm, DealerBalanceConfirmationRpt);
            }
        }
        protected void ibtnDealerBalConFirmArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerBalanceConfirmationRpt.PageCount > gvDealerBalanceConfirmationRpt.PageIndex)
            {
                gvDealerBalanceConfirmationRpt.PageIndex = gvDealerBalanceConfirmationRpt.PageIndex + 1;
                DealerBalanceConfirmationRptBind(gvDealerBalanceConfirmationRpt, lblRowCountDealerBalConFirm, DealerBalanceConfirmationRpt);
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(DealerBalanceConfirmationRpt, "Dealer Balance Confirmation Report");
        }
    }
}