using Business;
using DealerManagementSystem.ViewPreSale.UserControls;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class EnquiryTimeLineReport : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Enquiry Time Line');</script>");

            lblMessage.Text = "";

            if (!IsPostBack)
            { 
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillEnquiryTimeLine();
        }

        public DataTable SalesReport
        {
            get
            {
                if (Session["PreSalesReport"] == null)
                {
                    Session["PreSalesReport"] = new DataTable();
                }
                return (DataTable)Session["PreSalesReport"];
            }
            set
            {
                Session["PreSalesReport"] = value;
            }
        }

        protected void ibtnLeadArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLead.PageIndex > 0)
            {
                gvLead.PageIndex = gvLead.PageIndex - 1;
                LeadBind(gvLead, lblRowCount);
            }
        }
        protected void ibtnLeadArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLead.PageCount > gvLead.PageIndex)
            {
                gvLead.PageIndex = gvLead.PageIndex + 1;
                LeadBind(gvLead, lblRowCount);
            }
        }

        void LeadBind(GridView gv, Label lbl)
        {
            gv.DataSource = SalesReport;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SalesReport.Rows.Count;
        }

        void FillEnquiryTimeLine()
        {
            lblMessage.Text = string.Empty;
            PApiResult result = new PApiResult();
            try
            {
                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);

                result = new BEnquiry().GetEnquiryTimeLineReport(txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), DealerID);
                if (result.Status == "Failed")
                {
                    lblMessage.Text = result.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                SalesReport = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(result.Data));
                gvLead.DataSource = SalesReport;
                gvLead.DataBind();

                if (SalesReport.Rows.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnLeadArrowLeft.Visible = false;
                    ibtnLeadArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnLeadArrowLeft.Visible = true;
                    ibtnLeadArrowRight.Visible = true;
                    lblRowCount.Text = (((gvLead.PageIndex) * gvLead.PageSize) + 1) + " - " + (((gvLead.PageIndex) * gvLead.PageSize) + gvLead.Rows.Count) + " of " + SalesReport.Rows.Count;
                }
            }
            catch (Exception e)
            {
                lblMessage.Text = e.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void gvLead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLead.PageIndex = e.NewPageIndex;
            FillEnquiryTimeLine();
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(SalesReport, "Enquiry Time Line");
        }
    }
}