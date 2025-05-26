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

namespace DealerManagementSystem.ViewSales
{
    public partial class QuotationTracker : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSales_QuotationTracker; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                 Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
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
        DateTime? DateFrom = null;
        DateTime? DateTo = null;
        string QuotationNumber = null;
        int? DealerID = null;
        int? DealerTypeID = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sale Order » Report » Quotation Tracker');</script>");
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                try
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                    imgBtnExportExcel.Visible = false;
                    new DDLBind(ddlDealerType, new BDMS_Dealer().GetDealerType(null, null), "DealerType", "DealerTypeID");
                    fillDealer(ddlDealer, PSession.User.Dealer, "Select");
                    txtDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year; ;
                    txtDateTo.Text = DateTime.Now.ToShortDateString();
                    fillQuotationTracker();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }
        void fillDealer(DropDownList dll, object Data, string select)
        {
            dll.DataTextField = "CodeWithName";
            dll.DataValueField = "DealerID";
            dll.DataSource = Data;
            dll.DataBind();
            dll.Items.Insert(0, new ListItem(select, "0"));
        }
        protected void ddlDealerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDealerType.SelectedValue == "0")
            {
                fillDealer(ddlDealer, PSession.User.Dealer, "Select");
            }
            else
            {
                fillDealer(ddlDealer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == Convert.ToInt32(ddlDealerType.SelectedValue)), "Select");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillQuotationTracker();
            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void Search()
        {
            DateFrom = null;
            DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());
            QuotationNumber = txtQuotationNumber.Text.Trim();
            DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            DealerTypeID = ddlDealerType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerType.SelectedValue);
        }
        void fillQuotationTracker()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int RowCount = 0;
                Search();
                PApiResult Result = new BDMS_SalesOrder().GetPartsQuotationTracker(DateFrom.ToString(), DateTo.ToString(), QuotationNumber, DealerID, DealerTypeID, PageIndex, gvQuotationTracker.PageSize);
                DataTable QuotationTrackerReport = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
                RowCount = Result.RowCount;

                gvQuotationTracker.PageIndex = 0;
                gvQuotationTracker.DataSource = QuotationTrackerReport;
                gvQuotationTracker.DataBind();

                if (RowCount == 0)
                {
                    gvQuotationTracker.DataSource = null;
                    gvQuotationTracker.DataBind();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                    imgBtnExportExcel.Visible = false;
                }
                else
                {
                    gvQuotationTracker.DataSource = QuotationTrackerReport;
                    gvQuotationTracker.DataBind();
                    PageCount = (RowCount + gvQuotationTracker.PageSize - 1) / gvQuotationTracker.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    imgBtnExportExcel.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvQuotationTracker.PageSize) + 1) + " - " + (((PageIndex - 1) * gvQuotationTracker.PageSize) + gvQuotationTracker.Rows.Count) + " of " + RowCount;
                }
                //ActionControlMange();
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillQuotationTracker();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillQuotationTracker();
            }
        }
        protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Search();
                PApiResult Result = new BDMS_SalesOrder().GetPartsQuotationTracker(DateFrom.ToString(), DateTo.ToString(), QuotationNumber, DealerID, DealerTypeID, null, null);
                DataTable QuotationTrackerReport = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));

                new BXcel().ExporttoExcel(QuotationTrackerReport, "Quotation Tracker Report");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}