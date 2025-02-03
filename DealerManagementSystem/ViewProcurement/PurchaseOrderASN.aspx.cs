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
        int? DealerOfficeID = null;
        string VendorID = null;
        string AsnNumber = null;
        string PurchaseOrderNo = null;
        string invoiceNo = null;
        string SaleOrderNo = null;
        DateTime? AsnDateF = null;
        DateTime? AsnDateT = null;
        int? AsnStatusID = null;
        int? PurchaseOrderTypeID = null;
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
            Session["previousUrl"] = "PurchaseOrderASN.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Procurement » Advanced Shipping Notice');</script>");
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
                fillDealer();
                FillGetDealerOffice();
                new DDLBind(ddlPurchaseOrderType, new BProcurementMasters().GetPurchaseOrderType(null, null), "PurchaseOrderType", "PurchaseOrderTypeID");
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
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
            DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            DealerOfficeID = ddlDealerOffice.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerOffice.SelectedValue);
            //   VendorID = null; 
            AsnDateF = null;
            AsnDateF = string.IsNullOrEmpty(txtAsnDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtAsnDateFrom.Text.Trim());
            AsnDateT = string.IsNullOrEmpty(txtAsnDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtAsnDateTo.Text.Trim());
            AsnStatusID = ddlAsnStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlAsnStatus.SelectedValue);
            AsnNumber = txtAsnNumber.Text.Trim();
            PurchaseOrderNo = txtPoNumber.Text.Trim();
            invoiceNo=txtInvoiceNo.Text.Trim();
            SaleOrderNo = txtSoNumber.Text.Trim();
            DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
            PurchaseOrderTypeID = ddlPurchaseOrderType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlPurchaseOrderType.SelectedValue);
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
        void fillPurchaseOrderASN()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderAsnHeader(DealerID, DealerOfficeID, VendorID, AsnNumber, AsnDateF, AsnDateT, AsnStatusID, PurchaseOrderNo, SaleOrderNo, invoiceNo, PurchaseOrderTypeID, DivisionID, PageIndex, gvPAsn.PageSize);
                List<PAsn> PAsnHeader = JsonConvert.DeserializeObject<List<PAsn>>(JsonConvert.SerializeObject(Result.Data));
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
        
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();
            ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void ddlDealerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGetDealerOffice();
        }
        private void FillGetDealerOffice()
        {
            DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(DealerID, null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
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
            Search();
            DataTable Result = new BDMS_PurchaseOrder().GetPurchaseOrderAsnExcel(DealerID, DealerOfficeID, VendorID, AsnNumber, AsnDateF, AsnDateT, AsnStatusID, PurchaseOrderNo, SaleOrderNo, invoiceNo, PurchaseOrderTypeID, DivisionID, 0);
            new BXcel().ExporttoExcel(Result, "Asn Report"); 
        } 

        protected void btnExportExcelDetails_Click(object sender, EventArgs e)
        {
            Search();
            DataTable Result = new BDMS_PurchaseOrder().GetPurchaseOrderAsnExcel(DealerID, DealerOfficeID, VendorID, AsnNumber, AsnDateF, AsnDateT, AsnStatusID, PurchaseOrderNo, SaleOrderNo,invoiceNo, PurchaseOrderTypeID, DivisionID, 1);
            new BXcel().ExporttoExcel(Result, "Asn Report");
        }

        protected void btnMissingASN_Click(object sender, EventArgs e)
        {
            MPE_MissingASN.Show();
        }

        protected void btnMissingAsnSave_Click(object sender, EventArgs e)
        {
            new BAPI().ApiGet("Sap/GetASNFromSAP?InvoiceNo=" + txtInvoiceNumber.Text);
            fillPurchaseOrderASN();
        }


        protected void gvPAsn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            f_Return_Product_Type_Image rStr1 = new f_Return_Product_Type_Image();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#b3ecff';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Click On View Icon for More Details... ";

                Label lblStatus = e.Row.FindControl("lblAsnStatus") as Label;
                string lStatus = lblStatus.Text;

                if (lStatus == "GR Pending") { e.Row.Cells[0].Attributes["style"] = "background-color: red"; }
                else if (lStatus == "GR Done") { e.Row.Cells[0].Attributes["style"] = "background-color: #009900"; }
                else if (lStatus == "Cancelled") { e.Row.Cells[0].Attributes["style"] = "background-color: #52527a"; }


                Label lblDivision = e.Row.FindControl("lblDivision") as Label;
                string lDivision = lblDivision.Text;

                ImageButton imgButton = e.Row.FindControl("imgDivision") as ImageButton;
                rStr1.as_ProductType = lDivision;
                imgButton.ImageUrl = rStr1.GetProductTypeImage();

            }
        }
    }
}