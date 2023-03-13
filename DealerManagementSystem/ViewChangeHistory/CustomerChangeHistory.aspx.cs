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
    public partial class CustomerChangeHistory : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewChangeHistory_CustomerChangeHistory; } }
        public DataTable CustomerCH
        {
            get
            {
                if (Session["CustomerCH"] == null)
                {
                    Session["CustomerCH"] = new DataTable();
                }
                return (DataTable)Session["CustomerCH"];
            }
            set
            {
                Session["CustomerCH"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Report » Customer Change History');</script>");
            if (!IsPostBack)
            {
                CustomerCH = new BChangeHistory().GetChangeHistoryCustomerFields(null, null);
                new DDLBind(ddlCustomerField, CustomerCH, "Description", "ChangeHistoryFieldID");
                txtDateFrom.Text = DateTime.Now.AddDays(1+(-1* DateTime.Now.Day)).ToString("yyyy-MM-dd");
                txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSearchCustChgHst_Click(object sender, EventArgs e)
        {
            DateTime? DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());
            int? CustomerField =  ddlCustomerField.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCustomerField.SelectedValue);
            CustomerCH = new BChangeHistory().GetCustomerChangeHistory(txtCustomerCode.Text.Trim(), CustomerField, DateFrom, DateTo);
            gvCustChgHst.DataSource = CustomerCH;
            gvCustChgHst.DataBind();
            lblRowCountCustChgHst.Text = (((gvCustChgHst.PageIndex) * gvCustChgHst.PageSize) + 1) + " - " + (((gvCustChgHst.PageIndex) * gvCustChgHst.PageSize) + gvCustChgHst.Rows.Count) + " of " + CustomerCH.Rows.Count;
        }

        protected void btnExportExcelCustChgHst_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(CustomerCH, "Customer Change History");
        }

        protected void gvCustChgHst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustChgHst.PageIndex = e.NewPageIndex;
            gvCustChgHst.DataSource = CustomerCH;
            gvCustChgHst.DataBind();
        }

        protected void ibtnCustChgHstArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvCustChgHst.PageIndex > 0)
            {
                gvCustChgHst.PageIndex = gvCustChgHst.PageIndex - 1;
                CustomerChangeHistorytBind(gvCustChgHst, lblRowCountCustChgHst, CustomerCH);
            }
        }
        protected void ibtnCustChgHstArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvCustChgHst.PageCount > gvCustChgHst.PageIndex)
            {
                gvCustChgHst.PageIndex = gvCustChgHst.PageIndex + 1;
                CustomerChangeHistorytBind(gvCustChgHst, lblRowCountCustChgHst, CustomerCH);
            }
        }


        void CustomerChangeHistorytBind(GridView gv, Label lbl, DataTable CustomerCH)
        {
            gv.DataSource = CustomerCH;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + CustomerCH.Rows.Count;
        }
    }
}