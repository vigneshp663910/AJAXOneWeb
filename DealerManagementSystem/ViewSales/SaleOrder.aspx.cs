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
        //string SaleOrderNo = null;
        string QuotationNo = null;
        string SaleOrderNumber = null;
        int? DealerID = null;
        int? OfficeCodeID = null;
        int? DivisionID = null;
        string CustomerCode = null;
        int? SaleOrderStatusID = null;
        int? SaleOrderTypeID = null;
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

                fillDealer(); 
                new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealerCode.SelectedValue), null, null), "OfficeName", "OfficeID", true, "Select");
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
                new DDLBind(ddlSOStatus, new BDMS_Master().GetAjaxOneStatus(3), "Status", "StatusID");
                new DDLBind(ddlSOType, new BDMS_SalesOrder().GetSaleOrderType(null, null), "SaleOrderType", "SaleOrderTypeID");
                //fillSalesOrder();
                if (Session["SaleOrderID"] != null)
                {
                    divList.Visible = false;
                    divDetailsView.Visible = true;
                    UC_SalesOrderView.fillViewSO(Convert.ToInt64(Session["SaleOrderID"]));
                    Session["SaleOrderID"] = null;
                }
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
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
        protected void ddlDealerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CDealerID = (ddlDealerCode.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(CDealerID, null, null), "OfficeName", "OfficeID", true, "Select");
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
            //SaleOrderNo = txtSONumber.Text.Trim();
            QuotationNo = txtQuotationNumber.Text.Trim();
            SaleOrderNumber = txtSaleOrderNumber.Text.Trim();
            DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            OfficeCodeID = ddlOfficeName.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlOfficeName.SelectedValue);
            DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
            SaleOrderStatusID = ddlSOStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSOStatus.SelectedValue);
            CustomerCode = string.IsNullOrEmpty(txtCustomer.Text.Trim()) ? null : txtCustomer.Text.Trim();
            SaleOrderTypeID = ddlSOType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSOType.SelectedValue);
        }
        void fillSalesOrder()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                long? SaleOrderID = null;
                PApiResult Result = new BDMS_SalesOrder().GetSaleOrderHeader(SaleOrderID, DateFrom.ToString(), DateTo.ToString(), QuotationNo, SaleOrderNumber, DealerID, OfficeCodeID, DivisionID, CustomerCode, SaleOrderStatusID, SaleOrderTypeID, PageIndex, gvSaleOrder.PageSize);
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
            PApiResult Result = new BDMS_SalesOrder().GetSaleOrderReport(SaleOrderID, DateFrom.ToString(), DateTo.ToString(), QuotationNo, DealerID, OfficeCodeID, DivisionID, CustomerCode, SaleOrderStatusID, SaleOrderTypeID);
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
        public static string GetMaterial(string Material, string MaterialType, string DivisionID)
        {

            List<PDMS_Material> Materials = new BDMS_Material().GetMaterialAutocompleteN(Material, MaterialType, Convert.ToInt32(DivisionID), "false");
            return JsonConvert.SerializeObject(Materials);
        }
        [WebMethod]
        public static List<DetailsAutoFill> GetEquipment(string custID)
        {
            DetailsAutoFill studentDetails = null;
            List<PDMS_EquipmentHeader> Relations = new BDMS_Equipment().GetEquipmentForCreateICTicket(Convert.ToInt64(custID), null, null);
            List<DetailsAutoFill> lstStudentDetails = new List<DetailsAutoFill>();
            foreach (PDMS_EquipmentHeader Relation in Relations)
            {
                studentDetails = new DetailsAutoFill();
                studentDetails.Id = Relation.EquipmentHeaderID;
                studentDetails.Name = Relation.EquipmentSerialNo;
                lstStudentDetails.Add(studentDetails);
            }
            return lstStudentDetails;
        }
        public class DetailsAutoFill
        {
            public long Id { get; set; }
            public string Name { get; set; }
        }
        [WebMethod]
        public static List<DetailsAutoFill> GetShiftTo(string custID)
        {
            DetailsAutoFill studentDetails = null;
            List<PDMS_CustomerShipTo> Relations = new BDMS_Customer().GetCustomerShopTo(null, Convert.ToInt64(custID));
            List<DetailsAutoFill> lstStudentDetails = new List<DetailsAutoFill>();
            foreach (PDMS_CustomerShipTo Relation in Relations)
            {
                studentDetails = new DetailsAutoFill();
                studentDetails.Id = Relation.CustomerShipToID;
                studentDetails.Name = Relation.Address1+","+ Relation.Address2 + "," + Relation.Address2 + "," + Relation.District.District + "," + Relation.State.State;
                lstStudentDetails.Add(studentDetails);
            }
            return lstStudentDetails;
        }
    }
}