using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement
{
    public partial class PurchaseOrderAsnGR : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewProcurement_PurchaseOrderAsnGR; } }
        int? DealerID = null;
        string VendorID = null;
        string GrNumber = null;
        DateTime? GrDateF = null;
        DateTime? GrDateT = null;
        int? GrStatusID = null;
        public List<PGr> GrHeader
        {
            get
            {
                if (ViewState["GrHeader"] == null)
                {
                    ViewState["GrHeader"] = new List<PGr>();
                }
                return (List<PGr>)ViewState["GrHeader"];
            }
            set
            {
                ViewState["GrHeader"] = value;
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
        protected void Page_PreInit(object sender, EventArgs e)
        { 
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Procurement » Purchase Order ASN GR');</script>");
            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtGrDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year; ;
                txtGrDateTo.Text = DateTime.Now.ToShortDateString();
                new DDLBind(ddlGrStatus, new BDMS_PurchaseOrder().GetProcurementStatus(1), "ProcurementStatus", "ProcurementStatusID");
                 
                    fillDealer(); 
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillPurchaseOrderASNGr();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void Search()
        {
            DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            //   VendorID = null; 
            GrDateF = null;
            GrDateF = string.IsNullOrEmpty(txtGrDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtGrDateFrom.Text.Trim());
            GrDateT = string.IsNullOrEmpty(txtGrDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtGrDateTo.Text.Trim());
            GrStatusID = ddlGrStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlGrStatus.SelectedValue);
            GrNumber = string.IsNullOrEmpty(txtGrNumber.Text) ? null : txtGrNumber.Text.Trim();
        }
        void fillPurchaseOrderASNGr()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderAsnGrHeader(DealerID, VendorID, GrNumber, GrDateF, GrDateT, GrStatusID, PageIndex, gvAsnGr.PageSize);
                GrHeader = JsonConvert.DeserializeObject<List<PGr>>(JsonConvert.SerializeObject(Result.Data));
                gvAsnGr.PageIndex = 0;
                gvAsnGr.DataSource = GrHeader;
                gvAsnGr.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvAsnGr.PageSize - 1) / gvAsnGr.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvAsnGr.PageSize) + 1) + " - " + (((PageIndex - 1) * gvAsnGr.PageSize) + gvAsnGr.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("PurchaseOrderAsn", "fillPurchaseOrderASNGr", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillPurchaseOrderASNGr();
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillPurchaseOrderASNGr();
            }
        }
        protected void gvAsnGr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAsnGr.PageIndex = e.NewPageIndex;
            gvAsnGr.DataSource = GrHeader;
            gvAsnGr.DataBind();
            lblRowCount.Text = (((gvAsnGr.PageIndex) * gvAsnGr.PageSize) + 1) + " - " + (((gvAsnGr.PageIndex) * gvAsnGr.PageSize) + gvAsnGr.Rows.Count) + " of " + GrHeader.Count;
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void btnPurchaseOrderASNGrViewBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
        }
        protected void btnViewPO_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblGrID = (Label)gvRow.FindControl("lblGrID");
            divList.Visible = false;
            divDetailsView.Visible = true;
            UC_PurchaseOrderASNGrView.fillViewPOAsn(Convert.ToInt64(lblGrID.Text));
        }
    }
}