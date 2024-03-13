using Business;
using Newtonsoft.Json;
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
        public override SubModule SubModuleName { get { return SubModule.ViewFinance_Reports_DealerBalanceConfirmationReport; } }
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
                PageCount = 0;
                PageIndex = 1;
                DealerBalanceConfirmationRpt = null;
                new DDLBind().FillDealerAndEngneer(ddlDealer, null); 
                new DDLBind(ddlDealerMail, PSession.User.Dealer, "CodeWithDisplayName", "DealerCode");
                new DDLBind(ddlBalanceConfirmationStatus, new BDMS_Master().GetAjaxOneStatus(2), "Status", "StatusID");
            }
            //DealerBalanceConfirmationRptBind(gvDealerBalanceConfirmationRpt, lblRowCountDealerBalConFirm, DealerBalanceConfirmationRpt);
        }
        void DealerBalanceConfirmationRptBind(GridView gv, Label lbl, DataTable DealerBalanceConfirmationReport,int RowCount)
        {
            gv.DataSource = DealerBalanceConfirmationReport;
            gv.DataBind();
            //lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + RowCount;
            lbl.Text = (((PageIndex - 1) * gv.PageSize) + 1) + " - " + (((PageIndex - 1) * gv.PageSize) + gv.Rows.Count) + " of " + RowCount;
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

        //    DataTable DealerBalanceConfirmationRpt = new BDealerBusiness().GetDealerBalanceConfirmationReport(DealerID, BalanceConfirmationStatusID, DateFrom, DateTo);
            PApiResult Result =   new BDealerBusiness().GetDealerBalanceConfirmationReport(DealerID, BalanceConfirmationStatusID, DateFrom, DateTo, PageIndex, gvDealerBalanceConfirmationRpt.PageSize);
            DataTable DealerBalanceConfirmationRpt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            if (Result.RowCount == 0)
            {
                lblRowCountDealerBalConFirm.Visible = false;
                ibtnDealerBalConFirmArrowLeft.Visible = false;
                ibtnDealerBalConFirmArrowRight.Visible = false;
                gvDealerBalanceConfirmationRpt.DataSource = DealerBalanceConfirmationRpt;
                gvDealerBalanceConfirmationRpt.DataBind();
            }
            else
            {
                PageCount = (Result.RowCount + gvDealerBalanceConfirmationRpt.PageSize - 1) / gvDealerBalanceConfirmationRpt.PageSize;
                lblRowCountDealerBalConFirm.Visible = true;
                ibtnDealerBalConFirmArrowLeft.Visible = true;
                ibtnDealerBalConFirmArrowRight.Visible = true;
                DealerBalanceConfirmationRptBind(gvDealerBalanceConfirmationRpt, lblRowCountDealerBalConFirm, DealerBalanceConfirmationRpt, Result.RowCount);
            }
        }
        protected void ibtnDealerBalConFirmArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillDealerBalanceConfirmation();
            } 
        }
        protected void ibtnDealerBalConFirmArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillDealerBalanceConfirmation();
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            string DateFrom = txtFromDate.Text.Trim();
            string DateTo = txtToDate.Text.Trim();
            int? BalanceConfirmationStatusID = ddlBalanceConfirmationStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlBalanceConfirmationStatus.SelectedValue);

            DataTable dt = new BDealerBusiness().GetDealerBalanceConfirmationReportExcel(DealerID, BalanceConfirmationStatusID, DateFrom, DateTo);

            new BXcel().ExporttoExcel(dt, "Dealer Balance Confirmation Report");
        }

          
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkFSRDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkFSRDownload.Parent.Parent;
                Label lblAttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");

                long AttachedFileID = Convert.ToInt64(lblAttachedFileID.Text); 
                PAttachedFile UploadedFile = new BDealerBusiness().AttachmentsForDownload(AttachedFileID);
                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            { }
        }

        protected void gvDealerBalanceConfirmationRpt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblDealerBalanceConfirmationID = (Label)e.Row.FindControl("lblDealerBalanceConfirmationID"); 
                    GridView gvFileAttached = (GridView)e.Row.FindControl("gvFileAttached");
                    gvFileAttached.DataSource = new BDealerBusiness().GetDealerBalanceConfirmationAttachment(Convert.ToInt64(lblDealerBalanceConfirmationID.Text));  
                    gvFileAttached.DataBind(); 
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("Warranty Claim Approval", "gvICTickets_RowDataBound", ex);
                throw ex;
            }
        }

        protected void btnRequestForMail_Click(object sender, EventArgs e)
        {
            MPE_MailRequest.Show();
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            MPE_MailRequest.Show(); 
            new BDealerBusiness().SendMailtoDealerSatament(ddlDealerMail.SelectedValue, txtOpenDate.Text, txtPostDateFrom.Text, txtMail.Text.Trim());
            MPE_MailRequest.Hide();
        }
    }
}