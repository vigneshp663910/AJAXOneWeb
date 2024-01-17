using Business;
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

namespace DealerManagementSystem.ViewSales
{
    public partial class SaleOrder : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSales_SaleOrder; } }
        DateTime? DateFrom = null;
        DateTime? DateTo = null;
        string SaleOrderNo = null;
        int? DealerID = null;
        string CustomerCode = null;
        int? SaleOrderStatusID = null;
        public List<PSaleOrder> SalesOrder
        {
            get
            {
                if (ViewState["PDMS_SalesOrder"] == null)
                {
                    ViewState["PDMS_SalesOrder"] = new List<PSaleOrder>();
                }
                return (List<PSaleOrder>)ViewState["PDMS_SalesOrder"];
            }
            set
            {
                ViewState["PDMS_SalesOrder"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Sales Order');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year; ;
                txtDateTo.Text = DateTime.Now.ToShortDateString();

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
                new DDLBind(ddlSOStatus, new BDMS_SalesOrder().GetSaleOrderStatus(null, null), "Status", "StatusID");
                fillSalesOrder();
            }
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillSalesOrder();
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
            DateFrom = null;
            DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());
            SaleOrderNo = txtSONumber.Text.Trim();
            DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            SaleOrderStatusID = ddlSOStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSOStatus.SelectedValue);
            CustomerCode = string.IsNullOrEmpty(txtCustomer.Text.Trim()) ? null : txtCustomer.Text.Trim();
        }
        void fillSalesOrder()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                long? SaleOrderID = null;
                PApiResult Result = new BDMS_SalesOrder().GetSaleOrderHeader(SaleOrderID, DateFrom.ToString(), DateTo.ToString(), SaleOrderNo, DealerID, CustomerCode, SaleOrderStatusID, PageIndex, gvSaleOrder.PageSize);
                SalesOrder = JsonConvert.DeserializeObject<List<PSaleOrder>>(JsonConvert.SerializeObject(Result.Data));

                gvSaleOrder.PageIndex = 0;
                gvSaleOrder.DataSource = SalesOrder;
                gvSaleOrder.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvSaleOrder.PageSize - 1) / gvSaleOrder.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvSaleOrder.PageSize) + 1) + " - " + (((PageIndex - 1) * gvSaleOrder.PageSize) + gvSaleOrder.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("SaleOrder", "fillSalesOrder", e1);
                throw e1;
            }
        }
        protected void btnCreateSO_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            divDetailsView.Visible = false;
            divSaleOrderCreate.Visible = true;
            UC_SalesOrderCreate.FillMaster();
        }        
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillSalesOrder();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillSalesOrder();
            }
        }
        protected void gvSaleOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSaleOrder.PageIndex = e.NewPageIndex;
            gvSaleOrder.DataSource = SalesOrder;
            gvSaleOrder.DataBind();
            lblRowCount.Text = (((gvSaleOrder.PageIndex) * gvSaleOrder.PageSize) + 1) + " - " + (((gvSaleOrder.PageIndex) * gvSaleOrder.PageSize) + gvSaleOrder.Rows.Count) + " of " + SalesOrder.Count;
        }
        protected void btnViewSO_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblSaleOrderID = (Label)gvRow.FindControl("lblSaleOrderID");
            divList.Visible = false;
            divSaleOrderCreate.Visible = false;
            divDetailsView.Visible = true;
            UC_SalesOrderView.fillViewSO(Convert.ToInt64(lblSaleOrderID.Text));
        }
        protected void btnSaleOrderViewBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divSaleOrderCreate.Visible = false;
            divDetailsView.Visible = false;
        }
        protected void btnSaleOrderCreateBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divSaleOrderCreate.Visible = false;
            divDetailsView.Visible = false;
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            TraceLogger.Log(DateTime.Now);
            Search();
            long? SaleOrderID = null;
            DataTable dt = new DataTable();
            PApiResult Result = new BDMS_SalesOrder().GetSaleOrderReport(SaleOrderID, DateFrom.ToString(), DateTo.ToString(), SaleOrderNo, DealerID, CustomerCode, SaleOrderStatusID);
            dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));

            new BXcel().ExporttoExcel(dt, "Sales Order Report");
        }
        [WebMethod]
        public static string GetCustomer(string CustS)
        {
            List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerAutocomplete(CustS, 0);
            return JsonConvert.SerializeObject(Customer);
        }
        [WebMethod]
        public static string GetMaterial(string Material, string MaterialType, string Division)
        {
            List<PDMS_Material> Materials = new BDMS_Material().GetMaterialAutocompleteN(Material, MaterialType, null);
            return JsonConvert.SerializeObject(Materials);
        }
    }
}