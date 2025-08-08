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

namespace DealerManagementSystem.ViewECatalogue
{
    public partial class SpcCart : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewProcurement_PurchaseOrder; } }
        int? DealerID = null;
        int? OfficeID = null; 
        string OrderNo = null;
        string DateF = null;
        string DateT = null; 
        int? ModelID = null;
        int? DivisionID = null;

        public List<PspcCart> Cart
        {
            get
            {
                if (ViewState["SpcCart"] == null)
                {
                    ViewState["SpcCart"] = new List<PspcCart>();
                }
                return (List<PspcCart>)ViewState["SpcCart"];
            }
            set
            {
                ViewState["SpcCart"] = value;
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
                
                //FillGetDealerOffice();
                ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
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
                fillCart();
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
            DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
            ModelID = ddlPurchaseOrderType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlPurchaseOrderType.SelectedValue);
            DateF = txtPoDateFrom.Text.Trim();
            DateT = txtPoDateTo.Text.Trim(); 
            OrderNo = txtPoNumber.Text.Trim();
        }
        void fillCart()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BECatalogue().GetSpcCart(null, DealerID, OfficeID, OrderNo, DateF, DateT, DivisionID, ModelID, PageIndex, gvCart.PageSize); 
                Cart = JsonConvert.DeserializeObject<List<PspcCart>>(JsonConvert.SerializeObject(Result.Data));

                gvCart.PageIndex = 0;
                gvCart.DataSource = Cart;
                gvCart.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvCart.PageSize - 1) / gvCart.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvCart.PageSize) + 1) + " - " + (((PageIndex - 1) * gvCart.PageSize) + gvCart.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("Cart", "fillCart", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillCart();
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillCart();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            TraceLogger.Log(DateTime.Now);
            Search();
            PApiResult Result = new BECatalogue().GetSpcCart(null, DealerID, OfficeID, OrderNo, DateF, DateT, DivisionID, ModelID);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data)); 
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
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false; 
        }       

        protected void btnPurchaseOrderViewBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
        }  
        protected void btnViewPO_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblspcCartID = (Label)gvRow.FindControl("lblspcCartID");
            divList.Visible = false;
            divDetailsView.Visible = true;
            UC_SpcCartView.fillViewPO(Convert.ToInt64(lblspcCartID.Text));
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
       
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                  //  string supplierPOID = Convert.ToString(gvClaimByClaimID.DataKeys[e.Row.RowIndex].Value);
                    GridView gvCartItems = (GridView)e.Row.FindControl("gvCartItems");
                    Label lblspcCartID = (Label)e.Row.FindControl("lblspcCartID");
                    gvCartItems.DataSource = Cart.Find(s => s.spcCartID == Convert.ToInt32(lblspcCartID.Text)).CartItem;
                    gvCartItems.DataBind();  
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
