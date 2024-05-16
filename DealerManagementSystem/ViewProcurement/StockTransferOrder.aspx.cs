using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement
{
    public partial class StockTransferOrder : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewProcurement_StockTransferOrder; } }
        int? DealerID = null;
        int? OfficeID = null;
        string StockTransferOrderNo = "";
            string DateFrom = null; 
        string DateTo = null;
        int? StatusID = null; 
         

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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Procurement » Stock Transfer Orders');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtPoDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtPoDateTo.Text = DateTime.Now.ToShortDateString();

                fillDealer(); 
                new DDLBind(ddlPOStatus, new BDMS_Master().GetAjaxOneStatus((short)AjaxOneStatusHeader.StockTransferOrder), "Status", "StatusID");
                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderCreate).Count() == 0)
                {
                    btnCreatePO.Visible = false;
                }
                if (Session["StockTransferOrderID"] != null)
                {
                    divList.Visible = false;
                    divDetailsView.Visible = true;
                    UC_StockTransferOrderView.fillViewPO(Convert.ToInt64(Session["StockTransferOrderID"]));
                    Session["StockTransferOrderID"] = null;
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
                fillPurchaseOrder();
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
            OfficeID = ddlDealerOffice.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerOffice.SelectedValue);
            DateFrom =  txtPoDateFrom.Text.Trim();
            DateTo = txtPoDateTo.Text.Trim();
            StatusID = ddlPOStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlPOStatus.SelectedValue);
            StockTransferOrderNo = txtPoNumber.Text.Trim();
        }
        void fillPurchaseOrder()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BStockTransferOrder().GetStockTransferOrderHeader(DealerID, OfficeID, StockTransferOrderNo, DateFrom, DateTo, StatusID, PageIndex, gvICTickets.PageSize);
                List<PStockTransferOrder> PO = JsonConvert.DeserializeObject<List<PStockTransferOrder>>(JsonConvert.SerializeObject(Result.Data));
                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = PO;
                gvICTickets.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvICTickets.PageSize - 1) / gvICTickets.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvICTickets.PageSize) + 1) + " - " + (((PageIndex - 1) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("PurchaseOrder", "fillPurchaseOrder", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillPurchaseOrder();
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillPurchaseOrder();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("PO Number");
            //dt.Columns.Add("PO Item");
            //dt.Columns.Add("PO Date");
            //dt.Columns.Add("PO Type");
            //dt.Columns.Add("Dealer Code");
            //dt.Columns.Add("Dealer Name");
            //dt.Columns.Add("Location");
            //dt.Columns.Add("Currency");
            //dt.Columns.Add("Vendor Code");
            //dt.Columns.Add("PO Status");
            //dt.Columns.Add("Division");
            //dt.Columns.Add("Material");
            //dt.Columns.Add("HSN");
            //dt.Columns.Add("Material Desc");
            //dt.Columns.Add("Order Qty");
            //dt.Columns.Add("Ship. Qty");
            //dt.Columns.Add("Apr Qty");
            //dt.Columns.Add("UOM");
            //dt.Columns.Add("Net Amt");
            //dt.Columns.Add("Discount");
            //dt.Columns.Add("Unit Price");
            //dt.Columns.Add("Freight");
            //dt.Columns.Add("Insurance");
            //dt.Columns.Add("Packing");
            //dt.Columns.Add("SGST");
            //dt.Columns.Add("CGST");
            //dt.Columns.Add("IGST");
            //dt.Columns.Add("Gross Amt");

            //Search();
            //PApiResult Result = new BStockTransferOrder().GetStockTransferOrderHeader(DealerID, StockTransferOrderNo, DateFrom, DateTo, StatusID, PageIndex, gvICTickets.PageSize);
            //List<PStockTransferOrder> PO = JsonConvert.DeserializeObject<List<PStockTransferOrder>>(JsonConvert.SerializeObject(Result.Data));

            //foreach (PStockTransferOrder M in PO)
            //{
            //    dt.Rows.Add(
            //        M.PurchaseOrderID, M.PurchaseOrderItem.POItem, M.PurchaseOrderDate.ToShortDateString()
            //       , M.POType
            //        , M.Dealer.DealerCode, M.Dealer.DealerName
            //        , M.Location
            //        , M.Currency
            //        , M.BillTo
            //        , M.POStatus
            //        , M.Division
            //        , "'" + M.PurchaseOrderItem.Material.MaterialCode
            //         , M.PurchaseOrderItem.Material.HSN
            //          , M.PurchaseOrderItem.Material.MaterialDescription
            //        , decimal.Round(M.PurchaseOrderItem.OrderQuantity, 2, MidpointRounding.AwayFromZero)
            //        , decimal.Round(M.PurchaseOrderItem.ShipedQuantity, 2, MidpointRounding.AwayFromZero)
            //        , decimal.Round(M.PurchaseOrderItem.ApprovedQuantity, 2, MidpointRounding.AwayFromZero)
            //        , M.PurchaseOrderItem.UOM
            //        , decimal.Round(M.PurchaseOrderItem.NetAmount, 2, MidpointRounding.AwayFromZero)
            //        , decimal.Round(M.PurchaseOrderItem.DiscountAmount, 2, MidpointRounding.AwayFromZero)
            //        , decimal.Round(M.PurchaseOrderItem.UnitPrice, 2, MidpointRounding.AwayFromZero)
            //        , decimal.Round(M.PurchaseOrderItem.Fright, 2, MidpointRounding.AwayFromZero)
            //        , decimal.Round(M.PurchaseOrderItem.Insurance, 2, MidpointRounding.AwayFromZero)
            //        , decimal.Round(M.PurchaseOrderItem.PackingAndForwarding, 2, MidpointRounding.AwayFromZero)
            //        , decimal.Round(M.PurchaseOrderItem.SGST, 2, MidpointRounding.AwayFromZero)
            //        , decimal.Round(M.PurchaseOrderItem.CGST, 2, MidpointRounding.AwayFromZero)
            //        , decimal.Round(M.PurchaseOrderItem.IGST, 2, MidpointRounding.AwayFromZero)
            //        , decimal.Round(M.PurchaseOrderItem.GrossAmount, 2, MidpointRounding.AwayFromZero));
            //}
            //new BXcel().ExporttoExcel(dt, "PurchaseOrder Report");
        }

        
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
            FillGetDealerOffice();
        }
       
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
            divPurchaseOrderCreate.Visible = false;
        }

        protected void btnPurchaseOrderCreateBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divPurchaseOrderCreate.Visible = false;
        }

        protected void btnPurchaseOrderViewBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
        }

        protected void btnCreatePO_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            divPurchaseOrderCreate.Visible = true;
            lblMessage.Text = "";
            Button BtnView = (Button)sender;
            UC_StockTransferOrderCreate.FillMaster();
        }

        [WebMethod]
        public static string GetMaterial(string Material, string MaterialType)
        {
            List<PDMS_Material> Materials = new BDMS_Material().GetMaterialAutocompleteN(Material, MaterialType, null, "false");
            return JsonConvert.SerializeObject(Materials);
        }

        protected void btnViewPO_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblStockTransferOrderID = (Label)gvRow.FindControl("lblStockTransferOrderID");
            divList.Visible = false;
            divDetailsView.Visible = true;
            UC_StockTransferOrderView.fillViewPO(Convert.ToInt64(lblStockTransferOrderID.Text));
        }
        protected void ddlDealerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGetDealerOffice();
        }
        private void FillGetDealerOffice()
        { 
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealerCode.SelectedValue), null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}