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

        public List<PPurchaseOrder> PurchaseOrderList
        {
            get
            {
                if (ViewState["PurchaseOrderList"] == null)
                {
                    ViewState["PurchaseOrderList"] = new List<PPurchaseOrder>();
                }
                return (List<PPurchaseOrder>)ViewState["PurchaseOrderList"];
            }
            set
            {
                ViewState["PurchaseOrderList"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Procurement » Purchase Order');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtPoDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtPoDateTo.Text = DateTime.Now.ToShortDateString();

                fillDealer();
                new DDLBind(ddlPOStatus, new BDMS_PurchaseOrder().GetProcurementStatus((short)ProcurementStatusHeader.PurchaseOrder), "ProcurementStatus", "ProcurementStatusID");

                //FillGetDealerOffice();
                ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
                new DDLBind(ddlPurchaseOrderType, new BProcurementMasters().GetPurchaseOrderType(null, null), "PurchaseOrderType", "PurchaseOrderTypeID");
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
                //ddlDivision.Items.Insert(0, new ListItem("Select", "0"));
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
                    Session["PurchaseOrderID"] = null;
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
                PurchaseOrderList = JsonConvert.DeserializeObject<List<PPurchaseOrder>>(JsonConvert.SerializeObject(Result.Data));
                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = PurchaseOrderList;
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
            TraceLogger.Log(DateTime.Now);
            Search();
            PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderExportToExcel(DealerID, VendorID, PurchaseOrderNo, PurchaseOrderDateF
                    , PurchaseOrderDateT, PurchaseOrderStatusID, PurchaseOrderTypeID, DivisionID, DealerOfficeID, 0);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            //DataTable dt = new DataTable();
            //dt.Columns.Add("PO Number");
            //dt.Columns.Add("PO Date");
            //dt.Columns.Add("Dealer");
            //dt.Columns.Add("Receiving Location");
            //dt.Columns.Add("Vendor");
            //dt.Columns.Add("PO Order Type");
            //dt.Columns.Add("Division");            
            //dt.Columns.Add("PO Status");
            //dt.Columns.Add("SaleOrder Number");
            //dt.Columns.Add("Net Value");
            //dt.Columns.Add("Created");

            //foreach (PPurchaseOrder M in PurchaseOrderList)
            //{
            //    dt.Rows.Add(
            //        M.PurchaseOrderNumber
            //        , M.PurchaseOrderDate.ToShortDateString()
            //        , M.Dealer.DealerCode + Environment.NewLine + M.Dealer.DealerName
            //        , M.Location.OfficeName
            //        , M.Vendor.DealerCode + Environment.NewLine + M.Vendor.DealerName
            //        , M.PurchaseOrderType.PurchaseOrderType
            //        , M.Division.DivisionCode
            //        , M.PurchaseOrderStatus.ProcurementStatus
            //        , M.SaleOrderNumber
            //        , M.NetAmount
            //        , M.Created.ContactName
            //        );
            //}
            new BXcel().ExporttoExcel(dt, "Purchase Order Report");
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
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
        public static string GetMaterial(string Material, string MaterialType, string DivisionID, int ItemCount)
        {
            List<PDMS_Material> Materials = null;
            if (ItemCount == 0)
            {
                Materials = new BDMS_Material().GetMaterialAutocompleteN(Material, MaterialType, Convert.ToInt32(DivisionID), "false");
            }
            else
            {
                Materials = new BDMS_Material().GetMaterialAutocompleteN(Material, MaterialType, (short)Division.Parts, "false");
            }

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
        protected void ddlDealerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGetDealerOffice();
        }
        private void FillGetDealerOffice()
        {
            DealerID = Convert.ToInt32(ddlDealerCode.SelectedValue);
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(DealerID, null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
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
            else
            {
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
            }
        }
        protected void btnExportExcelDetails_Click(object sender, EventArgs e)
        {
            Search();
            PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderExportToExcel(DealerID, VendorID, PurchaseOrderNo, PurchaseOrderDateF
                    , PurchaseOrderDateT, PurchaseOrderStatusID, PurchaseOrderTypeID, DivisionID, DealerOfficeID, 1);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            new BXcel().ExporttoExcel(dt, "Purchase Order Report");
        }

        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            f_Return_Product_Type_Image rStr1 = new f_Return_Product_Type_Image();

            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#b3ecff';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Click On View Icon for More Details... ";

                Label lblStatus = e.Row.FindControl("lblPurchaseOrderStatus") as Label;
                string lStatus = lblStatus.Text;

                if (lStatus == "Draft") { e.Row.Cells[0].Attributes["style"] = "background-color: #3399ff"; e.Row.Cells[8].Attributes["style"] = "color: #3399ff; font-weight: bold; font-size :12px;"; }
                else if (lStatus == "Released") { e.Row.Cells[0].Attributes["style"] = "background-color: #40bf80"; e.Row.Cells[8].Attributes["style"] = "color: #40bf80; font-weight: bold; font-size :12px;"; }
                else if (lStatus == "Partial Received") { e.Row.Cells[0].Attributes["style"] = "background-color: #00e600"; e.Row.Cells[8].Attributes["style"] = "color: #00e600; font-weight: bold; font-size :12px;"; }
                else if (lStatus == "Completed") { e.Row.Cells[0].Attributes["style"] = "background-color: #009900"; e.Row.Cells[8].Attributes["style"] = "color: #009900; font-weight: bold; font-size :12px;"; }
                else if (lStatus == "Force Closed") { e.Row.Cells[0].Attributes["style"] = "background-color: #cc6600"; e.Row.Cells[8].Attributes["style"] = "color: #cc6600; font-weight: bold; font-size :12px;"; }
                else if (lStatus == "Cancelled") { e.Row.Cells[0].Attributes["style"] = "background-color: #52527a"; e.Row.Cells[8].Attributes["style"] = "color: #52527a; font-weight: bold; font-size :12px;"; }
                else if (lStatus == "Waiting for Release Approval") { e.Row.Cells[0].Attributes["style"] = "background-color: #cccc00"; e.Row.Cells[8].Attributes["style"] = "color: #cccc00; font-weight: bold; font-size :12px;"; }
                else if (lStatus == "Waiting for Cancel Approval") { e.Row.Cells[0].Attributes["style"] = "background-color: #ffb366"; e.Row.Cells[8].Attributes["style"] = "color: #ffb366; font-weight: bold; font-size :12px;"; }

                Label lblDivision = e.Row.FindControl("lblf_division") as Label;
                string lDivision = lblDivision.Text;

                ImageButton imgButton = e.Row.FindControl("imgDivision") as ImageButton;
                rStr1.as_ProductType = lDivision;
                imgButton.ImageUrl = rStr1.GetProductTypeImage();
            }
        }
    }
}
