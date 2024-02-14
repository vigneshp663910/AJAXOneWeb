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
    public partial class PurchaseOrder : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewProcurement_PurchaseOrder; } }
        int? DealerID = null;
        int? DealerOfficeID = null;
        string VendorID = null;
        string PurchaseOrderNo = null;
        DateTime? PurchaseOrderDateF = null;
        DateTime? PurchaseOrderDateT = null;
        int? PurchaseOrderStatusID = null;
        int? PurchaseOrderTypeID = null;
        int? DivisionID = null;

        public List<PDMS_PurchaseOrder> SDMS_PurchaseOrder
        {
            get
            {
                if (ViewState["PDMS_PurchaseOrder"] == null)
                {
                    ViewState["PDMS_PurchaseOrder"] = new List<PDMS_PurchaseOrder>();
                }
                return (List<PDMS_PurchaseOrder>)ViewState["PDMS_PurchaseOrder"];
            }
            set
            {
                ViewState["PDMS_PurchaseOrder"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Procurement » Purchase Orders');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtPoDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtPoDateTo.Text = DateTime.Now.ToShortDateString();

                fillDealer();
                fillProcurementStatus();
                FillGetDealerOffice();
                fillPurchaseOrderType();
                ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderCreate).Count() == 0)
                {
                    btnCreatePO.Visible = false;
                }
                if (Session["PurchaseOrderID"] != null)
                {
                    divList.Visible = false;
                    divDetailsView.Visible = true;
                    UC_PurchaseOrderView.fillViewPO(Convert.ToInt64(Session["PurchaseOrderID"]));
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
            DealerOfficeID = ddlDealerOffice.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerOffice.SelectedValue);
            DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
            PurchaseOrderTypeID = ddlPurchaseOrderType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlPurchaseOrderType.SelectedValue);
            //   VendorID = null; 
            PurchaseOrderDateF = null;
            PurchaseOrderDateF = string.IsNullOrEmpty(txtPoDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtPoDateFrom.Text.Trim());
            PurchaseOrderDateT = string.IsNullOrEmpty(txtPoDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtPoDateTo.Text.Trim());

            PurchaseOrderStatusID = ddlPOStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlPOStatus.SelectedValue);
            PurchaseOrderNo = txtPoNumber.Text.Trim();
        }
        void fillPurchaseOrder()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderHeader(DealerID, VendorID, PurchaseOrderNo, PurchaseOrderDateF
                    , PurchaseOrderDateT, PurchaseOrderStatusID, PurchaseOrderTypeID, DivisionID, DealerOfficeID, PageIndex, gvICTickets.PageSize);
                List<PPurchaseOrder> PO = JsonConvert.DeserializeObject<List<PPurchaseOrder>>(JsonConvert.SerializeObject(Result.Data));
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
            DataTable dt = new DataTable();
            dt.Columns.Add("PO Number");
            dt.Columns.Add("PO Item");
            dt.Columns.Add("PO Date");
            dt.Columns.Add("PO Type");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Location");
            dt.Columns.Add("Currency");
            dt.Columns.Add("Vendor Code");
            dt.Columns.Add("PO Status");
            dt.Columns.Add("Division");
            dt.Columns.Add("Material");
            dt.Columns.Add("HSN");
            dt.Columns.Add("Material Desc");
            dt.Columns.Add("Order Qty");
            dt.Columns.Add("Ship. Qty");
            dt.Columns.Add("Apr Qty");
            dt.Columns.Add("UOM");
            dt.Columns.Add("Net Amt");
            dt.Columns.Add("Discount");
            dt.Columns.Add("Unit Price");
            dt.Columns.Add("Freight");
            dt.Columns.Add("Insurance");
            dt.Columns.Add("Packing");
            dt.Columns.Add("SGST");
            dt.Columns.Add("CGST");
            dt.Columns.Add("IGST");
            dt.Columns.Add("Gross Amt");


            foreach (PDMS_PurchaseOrder M in SDMS_PurchaseOrder)
            {
                dt.Rows.Add(
                    M.PurchaseOrderID, M.PurchaseOrderItem.POItem, M.PurchaseOrderDate.ToShortDateString()
                   , M.POType
                    , M.Dealer.DealerCode, M.Dealer.DealerName
                    , M.Location
                    , M.Currency
                    , M.BillTo
                    , M.POStatus
                    , M.Division
                    , "'" + M.PurchaseOrderItem.Material.MaterialCode
                     , M.PurchaseOrderItem.Material.HSN
                      , M.PurchaseOrderItem.Material.MaterialDescription
                    , decimal.Round(M.PurchaseOrderItem.OrderQuantity, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.PurchaseOrderItem.ShipedQuantity, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.PurchaseOrderItem.ApprovedQuantity, 2, MidpointRounding.AwayFromZero)
                    , M.PurchaseOrderItem.UOM
                    , decimal.Round(M.PurchaseOrderItem.NetAmount, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.PurchaseOrderItem.DiscountAmount, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.PurchaseOrderItem.UnitPrice, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.PurchaseOrderItem.Fright, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.PurchaseOrderItem.Insurance, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.PurchaseOrderItem.PackingAndForwarding, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.PurchaseOrderItem.SGST, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.PurchaseOrderItem.CGST, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.PurchaseOrderItem.IGST, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.PurchaseOrderItem.GrossAmount, 2, MidpointRounding.AwayFromZero));
            }
            new BXcel().ExporttoExcel(dt, "PurchaseOrder Report");
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataSource = SDMS_PurchaseOrder;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_PurchaseOrder.Count;

        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        void fillProcurementStatus()
        {
            ddlPOStatus.DataTextField = "ProcurementStatus";
            ddlPOStatus.DataValueField = "ProcurementStatusID";
            ddlPOStatus.DataSource = new BDMS_PurchaseOrder().GetProcurementStatus(1);
            ddlPOStatus.DataBind();
            ddlPOStatus.Items.Insert(0, new ListItem("Select", "0"));
        }

        //protected void BtnView_Click(object sender, EventArgs e)
        //{
        //    divList.Visible = false;
        //    divDetailsView.Visible = true; 
        //    lblMessage.Text = "";
        //    Button BtnView = (Button)sender;
        //    ViewState["EnquiryID"] = Convert.ToInt64(BtnView.CommandArgument);
        //    UC_PurchaseOrderView.fillViewEnquiry(Convert.ToInt64(BtnView.CommandArgument));
        //}
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
            UC_PurchaseOrderCreate.FillMaster();
        }

        [WebMethod]
        public static string GetMaterial(string Material, string MaterialType)
        {
            List<PDMS_Material> Materials = new BDMS_Material().GetMaterialAutocompleteN(Material, MaterialType, null);
            return JsonConvert.SerializeObject(Materials);
        }

        protected void btnViewPO_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblPurchaseOrderID = (Label)gvRow.FindControl("lblPurchaseOrderID");
            divList.Visible = false;
            divDetailsView.Visible = true;
            UC_PurchaseOrderView.fillViewPO(Convert.ToInt64(lblPurchaseOrderID.Text));
        }

        protected void ddlPurchaseOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDivision.Items.Clear();
            ddlDivision.DataTextField = "DivisionDescription";
            ddlDivision.DataValueField = "DivisionID";
            ddlDivision.Items.Insert(0, new ListItem("Select", "0"));

            string OrderType = ddlPurchaseOrderType.SelectedValue;

            if ((OrderType == "1") || (OrderType == "2") || (OrderType == "7"))
            {
                ddlDivision.Items.Insert(1, new ListItem("Parts", "15"));
            }
            else if (ddlPurchaseOrderType.SelectedValue == "5")
            {
                ddlDivision.Items.Insert(1, new ListItem("Batching Plant", "1"));
                ddlDivision.Items.Insert(2, new ListItem("Concrete Mixer", "2"));
                ddlDivision.Items.Insert(3, new ListItem("Concrete Pump", "3"));
                ddlDivision.Items.Insert(4, new ListItem("Dumper", "4"));
                ddlDivision.Items.Insert(5, new ListItem("Transit Mixer", "11"));
                ddlDivision.Items.Insert(6, new ListItem("Mobile Concrete Equipment", "14"));
                ddlDivision.Items.Insert(7, new ListItem("Placing Equipment", "19"));
            }
            else if (ddlPurchaseOrderType.SelectedValue == "6")
            {
                ddlDivision.Items.Insert(1, new ListItem("Parts", "15"));
                ddlDivision.Items.Insert(2, new ListItem("Batching Plant", "1"));
                ddlDivision.Items.Insert(3, new ListItem("Concrete Mixer", "2"));
                ddlDivision.Items.Insert(4, new ListItem("Concrete Pump", "3"));
                ddlDivision.Items.Insert(5, new ListItem("Dumper", "4"));
                ddlDivision.Items.Insert(6, new ListItem("Transit Mixer", "11"));
                ddlDivision.Items.Insert(7, new ListItem("Mobile Concrete Equipment", "14"));
                ddlDivision.Items.Insert(8, new ListItem("Placing Equipment", "19"));
            }
        }

        protected void ddlDealerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGetDealerOffice();
        }
        private void FillGetDealerOffice()
        {
            DealerID = (ddlDealerCode.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(DealerID, null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }
        void fillPurchaseOrderType()
        {
            ddlPurchaseOrderType.Items.Clear();
            ddlPurchaseOrderType.DataTextField = "PurchaseOrderType";
            ddlPurchaseOrderType.DataValueField = "PurchaseOrderTypeID";
            ddlPurchaseOrderType.Items.Insert(0, new ListItem("Select", "0"));

            ddlPurchaseOrderType.Items.Insert(1, new ListItem("Stock Order-Within 15 Days", "1"));
            ddlPurchaseOrderType.Items.Insert(2, new ListItem("Emergency Order-Within 3 Days", "2"));
            ddlPurchaseOrderType.Items.Insert(3, new ListItem("Break Down Order-Within 3 Days", "7"));
            ddlPurchaseOrderType.Items.Insert(4, new ListItem("Machine Order-Within 3 Days", "5"));
            ddlPurchaseOrderType.Items.Insert(5, new ListItem("Intra-Dealer Order-Within 3 Days", "6"));
        }
    }
}