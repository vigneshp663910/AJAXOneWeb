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
    public partial class QuotationChangeHistory : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewChangeHistory_QuotationChangeHistory; } }
        public DataTable QuotationCH
        {
            get
            {
                if (Session["QuotationCH"] == null)
                {
                    Session["QuotationCH"] = new DataTable();
                }
                return (DataTable)Session["QuotationCH"];
            }
            set
            {
                Session["QuotationCH"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Report » Quotation Change History');</script>");
            if (!IsPostBack)
            {
                QuotationCH = new BChangeHistory().GetChangeHistoryQuotationFields(null, null);
                new DDLBind(ddlQuotationField, QuotationCH, "Description", "ChangeHistoryFieldID");
                txtDateFrom.Text = DateTime.Now.AddDays(1+(-1* DateTime.Now.Day)).ToString("yyyy-MM-dd");
                txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSearchQuotationChgHst_Click(object sender, EventArgs e)
        {
            DateTime? DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());
            int? QuotationField = ddlQuotationField.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlQuotationField.SelectedValue);
            QuotationCH = new BChangeHistory().GetQuotationChangeHistory((string.IsNullOrEmpty(txtRefQuotationNo.Text.Trim()) ? (Int64?)null : Convert.ToInt64(txtRefQuotationNo.Text.Trim())), QuotationField, DateFrom, DateTo);
            gvQuotationChgHst.DataSource = QuotationCH;
            gvQuotationChgHst.DataBind();
            lblRowCountQuotationChgHst.Text = (((gvQuotationChgHst.PageIndex) * gvQuotationChgHst.PageSize) + 1) + " - " + (((gvQuotationChgHst.PageIndex) * gvQuotationChgHst.PageSize) + gvQuotationChgHst.Rows.Count) + " of " + QuotationCH.Rows.Count;
        }

        protected void btnExportExcelQuotationChgHst_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(QuotationCH, "Quotation Change History");
        }

        protected void gvQuotationChgHst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvQuotationChgHst.PageIndex = e.NewPageIndex;
            gvQuotationChgHst.DataSource = QuotationCH;
            gvQuotationChgHst.DataBind();
        }

        protected void ibtnQuotationChgHstArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvQuotationChgHst.PageIndex > 0)
            {
                gvQuotationChgHst.PageIndex = gvQuotationChgHst.PageIndex - 1;
                QuotationChangeHistorytBind(gvQuotationChgHst, lblRowCountQuotationChgHst, QuotationCH);
            }

        }
        protected void ibtnQuotationChgHstArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvQuotationChgHst.PageCount > gvQuotationChgHst.PageIndex)
            {
                gvQuotationChgHst.PageIndex = gvQuotationChgHst.PageIndex + 1;
                QuotationChangeHistorytBind(gvQuotationChgHst, lblRowCountQuotationChgHst, QuotationCH);
            }
        }


        void QuotationChangeHistorytBind(GridView gv, Label lbl, DataTable QuotationCH)
        {
            gv.DataSource = QuotationCH;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + QuotationCH.Rows.Count;
        }
    }
}