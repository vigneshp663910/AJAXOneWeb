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
    public partial class SaleOrderDelivery : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSales_SaleOrderDelivery; } }
        
        DateTime? DateFrom = null;
        DateTime? DateTo = null;
        string DeliveryNo = null;
        int? DealerID = null;
        int? OfficeCodeID = null;
        int? DivisionID = null;
        string CustomerCode = null;
        int? SaleOrderTypeID = null;
        public List<PSaleOrderDelivery> SalesOrderDelivery
        {
            get
            {
                if (ViewState["SalesOrderDelivery"] == null)
                {
                    ViewState["SalesOrderDelivery"] = new List<PSaleOrderDelivery>();
                }
                return (List<PSaleOrderDelivery>)ViewState["SalesOrderDelivery"];
            }
            set
            {
                ViewState["SalesOrderDelivery"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Sales Order Delivery');</script>");
            lblMessageSODelivery.Visible = false;
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year; ;
                txtDateTo.Text = DateTime.Now.ToShortDateString();

                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    ddlDealer.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
                    ddlDealer.Enabled = false;
                }
                else
                {
                    ddlDealer.Enabled = true;
                    fillDealer();
                }
                
                int? CDealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(CDealerID, null, null), "OfficeName", "OfficeID", true, "Select");
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
                new DDLBind(ddlSaleOrderType, new BDMS_SalesOrder().GetSaleOrderType(null, null), "SaleOrderType", "SaleOrderTypeID");

                if (Session["SaleOrderDeliveryID"] != null)
                {
                    divSODeliveryList.Visible = false;
                    divSODeliveryDetailsView.Visible = true;
                    UC_SalesOrderDeliveryView.fillViewSODelivery(Convert.ToInt64(Session["SaleOrderDeliveryID"]));
                }
                lblRowCountSODelivery.Visible = false;
                ibtnArrowLeftSODelivery.Visible = false;
                ibtnArrowRightSODelivery.Visible = false;
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
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CDealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(CDealerID, null, null), "OfficeName", "OfficeID", true, "Select");
        }
        protected void btnSearchSODelivery_Click(object sender, EventArgs e)
        {
            try
            {
                fillSalesOrderDelivery();
            }
            catch (Exception e1)
            {
                lblMessageSODelivery.Text = e1.ToString();
                lblMessageSODelivery.ForeColor = Color.Red;
                lblMessageSODelivery.Visible = true;
            }
        }
        void fillSalesOrderDelivery()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                long? SaleOrderDeliveryID = null;
                PApiResult Result = new BSne().GetSaleOrderDeliveryHeader(SaleOrderDeliveryID, DateFrom.ToString(), DateTo.ToString(), DeliveryNo, DealerID, OfficeCodeID, DivisionID, CustomerCode, SaleOrderTypeID, PageIndex, gvSODelivery.PageSize);
                SalesOrderDelivery = JsonConvert.DeserializeObject<List<PSaleOrderDelivery>>(JsonConvert.SerializeObject(Result.Data));

                gvSODelivery.PageIndex = 0;
                gvSODelivery.DataSource = SalesOrderDelivery;
                gvSODelivery.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCountSODelivery.Visible = false;
                    ibtnArrowLeftSODelivery.Visible = false;
                    ibtnArrowRightSODelivery.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvSODelivery.PageSize - 1) / gvSODelivery.PageSize;
                    lblRowCountSODelivery.Visible = true;
                    ibtnArrowLeftSODelivery.Visible = true;
                    ibtnArrowRightSODelivery.Visible = true;
                    lblRowCountSODelivery.Text = (((PageIndex - 1) * gvSODelivery.PageSize) + 1) + " - " + (((PageIndex - 1) * gvSODelivery.PageSize) + gvSODelivery.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("SaleOrderDelivery", "fillSalesOrderDelivery", e1);
                throw e1;
            }
        }
        void Search()
        {
            DateFrom = null;
            DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());
            DeliveryNo = txtDeliveryNumber.Text.Trim();
            DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            OfficeCodeID = ddlOfficeName.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlOfficeName.SelectedValue);
            DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
            CustomerCode = string.IsNullOrEmpty(txtCustomer.Text.Trim()) ? null : txtCustomer.Text.Trim();
            SaleOrderTypeID = ddlSaleOrderType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSaleOrderType.SelectedValue);
        }
        protected void ibtnArrowLeftSODelivery_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillSalesOrderDelivery();
            }
        }
        protected void ibtnArrowRightSODelivery_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillSalesOrderDelivery();
            }
        }
        protected void gvSODelivery_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSODelivery.PageIndex = e.NewPageIndex;
            gvSODelivery.DataSource = SalesOrderDelivery;
            gvSODelivery.DataBind();
            lblRowCountSODelivery.Text = (((gvSODelivery.PageIndex) * gvSODelivery.PageSize) + 1) + " - " + (((gvSODelivery.PageIndex) * gvSODelivery.PageSize) + gvSODelivery.Rows.Count) + " of " + SalesOrderDelivery.Count;
        }
        protected void btnViewSODelivery_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblSaleOrderDeliveryID = (Label)gvRow.FindControl("lblSaleOrderDeliveryID");
            divSODeliveryList.Visible = false;
            divSODeliveryDetailsView.Visible = true;
            UC_SalesOrderDeliveryView.fillViewSODelivery(Convert.ToInt64(lblSaleOrderDeliveryID.Text));
        }
        protected void btnViewSODeliveryBack_Click(object sender, EventArgs e)
        {
            divSODeliveryList.Visible = true;
            divSODeliveryDetailsView.Visible = false;
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            TraceLogger.Log(DateTime.Now);
            Search();
            long? SaleOrderDeliveryID = null;
            DataTable dt = new DataTable();
            PApiResult Result = new BSne().GetSaleOrderDeliveryReport(SaleOrderDeliveryID, DateFrom.ToString(), DateTo.ToString(), DeliveryNo, DealerID, OfficeCodeID, DivisionID, CustomerCode, SaleOrderTypeID);
            dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));

            new BXcel().ExporttoExcel(dt, "Sales Order Delivery Report");
        }
    }
}