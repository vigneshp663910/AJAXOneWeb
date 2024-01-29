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

namespace DealerManagementSystem.ViewProcurement
{
    public partial class PurchaseOrderASN : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewProcurement_PurchaseOrderASN; } }
        int? DealerID = null;
        string VendorID = null;
        string AsnNumber = null;
        DateTime? AsnDateF = null;
        DateTime? AsnDateT = null;
        int? AsnStatusID = null;
        public List<PAsn> PAsnHeader
        {
            get
            {
                if (ViewState["PAsnHeader"] == null)
                {
                    ViewState["PAsnHeader"] = new List<PAsn>();
                }
                return (List<PAsn>)ViewState["PAsnHeader"];
            }
            set
            {
                ViewState["PAsnHeader"] = value;
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
            Session["previousUrl"] = "PurchaseOrderASN.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Procurement » Purchase Order ASN');</script>");
            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtAsnDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year; ;
                txtAsnDateTo.Text = DateTime.Now.ToShortDateString();
                new DDLBind(ddlAsnStatus, new BDMS_PurchaseOrder().GetProcurementStatus(2), "ProcurementStatus", "ProcurementStatusID");

                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillPurchaseOrderASN();
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
            AsnDateF = null;
            AsnDateF = string.IsNullOrEmpty(txtAsnDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtAsnDateFrom.Text.Trim());
            AsnDateT = string.IsNullOrEmpty(txtAsnDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtAsnDateTo.Text.Trim());
            AsnStatusID = ddlAsnStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlAsnStatus.SelectedValue);
            AsnNumber = string.IsNullOrEmpty(txtAsnNumber.Text)?null:txtAsnNumber.Text.Trim();
        }
        void fillPurchaseOrderASN()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderAsnHeader(DealerID, VendorID, AsnNumber, AsnDateF, AsnDateT, AsnStatusID, PageIndex, gvPAsn.PageSize);
                PAsnHeader = JsonConvert.DeserializeObject<List<PAsn>>(JsonConvert.SerializeObject(Result.Data));
                gvPAsn.PageIndex = 0;
                gvPAsn.DataSource = PAsnHeader;
                gvPAsn.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvPAsn.PageSize - 1) / gvPAsn.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvPAsn.PageSize) + 1) + " - " + (((PageIndex - 1) * gvPAsn.PageSize) + gvPAsn.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("PurchaseOrderAsn", "fillPurchaseOrderASN", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillPurchaseOrderASN();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillPurchaseOrderASN();
            }
        }
        protected void gvPAsn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPAsn.PageIndex = e.NewPageIndex;
            gvPAsn.DataSource = PAsnHeader;
            gvPAsn.DataBind();
            lblRowCount.Text = (((gvPAsn.PageIndex) * gvPAsn.PageSize) + 1) + " - " + (((gvPAsn.PageIndex) * gvPAsn.PageSize) + gvPAsn.Rows.Count) + " of " + PAsnHeader.Count;
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void btnPurchaseOrderViewBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
        }
        protected void btnViewPO_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblAsnID = (Label)gvRow.FindControl("lblAsnID");
            divList.Visible = false;
            divDetailsView.Visible = true;
            UC_PurchaseOrderASNView.fillViewPOAsn(Convert.ToInt64(lblAsnID.Text));
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportExcel(PAsnHeader, "Asn Report");
        }
        void ExportExcel(List<PAsn> AsnList, String Name)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Sno");
            dt.Columns.Add("Asn Number");
            dt.Columns.Add("Asn Date");
            dt.Columns.Add("PO Number");
            dt.Columns.Add("PO Date");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Vendor Code");
            dt.Columns.Add("Asn Status");
            dt.Columns.Add("Delivery Number");
            dt.Columns.Add("Delivery Date");
            dt.Columns.Add("Net Weight");            
            dt.Columns.Add("Track ID");
            dt.Columns.Add("Courier ID");
            dt.Columns.Add("Courier Date");
            dt.Columns.Add("LR Number");
            dt.Columns.Add("Asn Remarks");
            dt.Columns.Add("Gr Number");
            dt.Columns.Add("Gr Date");
            dt.Columns.Add("Gr Status");
            int sno = 0;
            foreach (PAsn Asn in AsnList)
            {
                sno += 1;
                dt.Rows.Add(
                    sno
                    , Asn.AsnNumber
                    , Asn.AsnDate
                    , Asn.PurchaseOrder.PurchaseOrderNumber
                    , Asn.PurchaseOrder.PurchaseOrderDate
                    , Asn.PurchaseOrder.Dealer.DealerCode
                    , Asn.PurchaseOrder.Vendor.DealerCode
                    , Asn.AsnStatus.ProcurementStatus
                    , Asn.DeliveryNumber
                    , Asn.DeliveryDate
                    , Asn.NetWeight
                    , Asn.TrackID
                    , Asn.CourierID
                    , Asn.CourierDate
                    , Asn.LRNo
                    , Asn.Remarks
                    , (Asn.Gr != null) ? Asn.Gr.GrNumber : ""
                    , (Asn.Gr != null) ? Asn.Gr.GrDate.ToString() : ""
                    , (Asn.Gr != null) ? Asn.Gr.Status.ProcurementStatus : ""
                    );
            }
            try
            {
                new BXcel().ExporttoExcel(dt, Name);
            }
            catch
            {

            }
            finally
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>HideProgress();</script>");
            }
        }
    }
}