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

namespace DealerManagementSystem.ViewSales.Report
{
    public partial class SalesReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSales_SalesInvoiceReport; } }

        int? DealerID = null;
        int? OfficeCodeID = null;
        string CustomerCode = null;
        string InvoiceNo = null;
        DateTime? InvoiceDateFrom = null;
        DateTime? InvoiceDateTo = null;
        int? SaleOrderTypeID = null;
        int? DivisionID = null;

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
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Sales Report');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtInvoiceDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year; ;
                txtInvoiceDateTo.Text = DateTime.Now.ToShortDateString();

                fillDealer();
                new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null), "OfficeName", "OfficeID", true, "Select");
                new DDLBind(ddlSaleOrderType, new BDMS_SalesOrder().GetSaleOrderType(null, null), "SaleOrderType", "SaleOrderTypeID");
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();
            ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillSalesOrderInvoiceReport();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillSalesOrderInvoiceReport();
            }
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null), "OfficeName", "OfficeID", true, "Select");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillSalesOrderInvoiceReport();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillSalesOrderInvoiceReport()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BDMS_SalesOrder().GetSalesReport(DealerID, OfficeCodeID, CustomerCode, InvoiceNo, InvoiceDateFrom.ToString(), InvoiceDateTo.ToString(), SaleOrderTypeID, DivisionID, PageIndex, gvSOInvoice.PageSize);
                DataTable SalesOrderInvoiceReport = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));

                gvSOInvoice.PageIndex = 0;
                gvSOInvoice.DataSource = SalesOrderInvoiceReport;
                gvSOInvoice.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvSOInvoice.PageSize - 1) / gvSOInvoice.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvSOInvoice.PageSize) + 1) + " - " + (((PageIndex - 1) * gvSOInvoice.PageSize) + gvSOInvoice.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("SaleInvoiceReport", "fillSalesOrderDelivery", e1);
                throw e1;
            }
        }
        void Search()
        {
            InvoiceDateFrom = string.IsNullOrEmpty(txtInvoiceDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtInvoiceDateFrom.Text.Trim());
            InvoiceDateTo = string.IsNullOrEmpty(txtInvoiceDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtInvoiceDateTo.Text.Trim());
            InvoiceNo = txtInvoiceNumber.Text.Trim();
            DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            OfficeCodeID = ddlOfficeName.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlOfficeName.SelectedValue);
            CustomerCode = string.IsNullOrEmpty(txtCustomer.Text.Trim()) ? null : txtCustomer.Text.Trim();
            SaleOrderTypeID = ddlSaleOrderType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSaleOrderType.SelectedValue);
            DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Search();
            PApiResult Result = new BDMS_SalesOrder().GetSalesReport(DealerID, OfficeCodeID, CustomerCode, InvoiceNo, InvoiceDateFrom.ToString(), InvoiceDateTo.ToString(), SaleOrderTypeID, DivisionID, null, null);
            DataTable SalesOrderInvoiceReport = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            new BXcel().ExporttoExcel(SalesOrderInvoiceReport, "SalesReport");
        }
    }
}