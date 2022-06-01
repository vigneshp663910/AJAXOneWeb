using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewChangeHistory
{
    public partial class LeadChangeHistory : System.Web.UI.Page
    {
        public DataTable LeadCH
        {
            get
            {
                if (Session["LeadCH"] == null)
                {
                    Session["LeadCH"] = new DataTable();
                }
                return (DataTable)Session["LeadCH"];
            }
            set
            {
                Session["LeadCH"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Report » Lead Change History');</script>");
            if (!IsPostBack)
            {
                LeadCH = new BChangeHistory().GetChangeHistoryLeadFields(null, null);
                new DDLBind(ddlLeadField, LeadCH, "Description", "ChangeHistoryFieldID");
                txtDateFrom.Text = DateTime.Now.AddDays(1+(-1* DateTime.Now.Day)).ToString("yyyy-MM-dd");
                txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSearchLeadChgHst_Click(object sender, EventArgs e)
        {
            DateTime? DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());
            int? LeadField = ddlLeadField.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlLeadField.SelectedValue);
            LeadCH = new BChangeHistory().GetLeadChangeHistory(txtLeadNumber.Text.Trim(), LeadField, DateFrom, DateTo);
            gvLeadChgHst.DataSource = LeadCH;
            gvLeadChgHst.DataBind();
            lblRowCountLeadChgHst.Text = (((gvLeadChgHst.PageIndex) * gvLeadChgHst.PageSize) + 1) + " - " + (((gvLeadChgHst.PageIndex) * gvLeadChgHst.PageSize) + gvLeadChgHst.Rows.Count) + " of " + LeadCH.Rows.Count;
        }

        protected void btnExportExcelLeadChgHst_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(LeadCH, "Lead Change History");
        }

        protected void gvLeadChgHst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLeadChgHst.PageIndex = e.NewPageIndex;
            gvLeadChgHst.DataSource = LeadCH;
            gvLeadChgHst.DataBind();
        }

        protected void ibtnLeadChgHstArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLeadChgHst.PageIndex > 0)
            {
                gvLeadChgHst.PageIndex = gvLeadChgHst.PageIndex - 1;
                LeadChangeHistorytBind(gvLeadChgHst, lblRowCountLeadChgHst, LeadCH);
            }

        }
        protected void ibtnLeadChgHstArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLeadChgHst.PageCount > gvLeadChgHst.PageIndex)
            {
                gvLeadChgHst.PageIndex = gvLeadChgHst.PageIndex + 1;
                LeadChangeHistorytBind(gvLeadChgHst, lblRowCountLeadChgHst, LeadCH);
            }
        }


        void LeadChangeHistorytBind(GridView gv, Label lbl, DataTable LeadCH)
        {
            gv.DataSource = LeadCH;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + LeadCH.Rows.Count;
        }
    }
}