using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement
{
    public partial class MonthlyProcurement : System.Web.UI.Page
    {
        public List<PDMS_PurchaseOrder> SDMS_SalesOrder
        {
            get
            {
                if (Session["DMS_MonthlyProcurement"] == null)
                {
                    Session["DMS_MonthlyProcurement"] = new List<PDMS_PurchaseOrder>();
                }
                return (List<PDMS_PurchaseOrder>)Session["DMS_MonthlyProcurement"];
            }
            set
            {
                Session["DMS_MonthlyProcurement"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {

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
                txtPoDateFrom.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                txtPoDateTo.Text = DateTime.Now.ToShortDateString();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillSalesOrderItems();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillSalesOrderItems()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                string Fillter = "";

                if (ddlDealerCode.SelectedValue != "0")
                    Fillter = Fillter + " and po.s_tenant_id = '" + ddlDealerCode.SelectedValue + "'";

                if (ddlOrderType.SelectedValue != "0")
                    Fillter = Fillter + " and po.s_object_type = " + ddlOrderType.SelectedValue;

                if (ddlStatus.SelectedValue != "0")
                    Fillter = Fillter + " and po.s_status = '" + ddlStatus.SelectedItem.Text + "'";

                if (!string.IsNullOrEmpty(txtMaterial.Text.Trim()))
                    Fillter = Fillter + " and poi.f_material_id = '" + txtMaterial.Text.Trim() + "'";


                if (!string.IsNullOrEmpty(txtPoDateFrom.Text.Trim()))
                {
                    Fillter = Fillter + "  and po.r_order_date >= '" + txtPoDateFrom.Text.Trim().Split('/')[1] + "/" + txtPoDateFrom.Text.Trim().Split('/')[0] + "/" + txtPoDateFrom.Text.Trim().Split('/')[2] + "'";
                }

                if (!string.IsNullOrEmpty(txtPoDateTo.Text.Trim()))
                {
                    Fillter = Fillter + "  and po.r_order_date <= '" + txtPoDateTo.Text.Trim().Split('/')[1] + "/" + txtPoDateTo.Text.Trim().Split('/')[0] + "/" + txtPoDateTo.Text.Trim().Split('/')[2] + "'";
                }


                List<PDMS_PurchaseOrder> SOIs = null;


                SOIs = new BDMS_PurchaseOrder().GetPurchaseOrderMonthily(Fillter);


                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs
                                 join D in PSession.User.Dealer on S.Dealer.DealerCode equals D.UserName
                                 select new
                                 {
                                     S
                                 }).ToList();
                    SOIs.Clear();
                    foreach (var w in SOIs1)
                    {
                        SOIs.Add(w.S);
                    }
                }

                SDMS_SalesOrder = SOIs;


                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = SOIs;
                gvICTickets.DataBind();
                if (SOIs.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_SalesOrder.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_SalesOrderPerformance", "fillSalesOrderItems", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;
                gvICTickets.DataSource = SDMS_SalesOrder;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_SalesOrder.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataSource = SDMS_SalesOrder;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_SalesOrder.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SNo");
            dt.Columns.Add("Dealer");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Type of Order");
            dt.Columns.Add("Period");
            dt.Columns.Add("No. of Orders");
            dt.Columns.Add("No. of Items");
            dt.Columns.Add("Total Qty");

            dt.Columns.Add("Value(NDP)");
            int i = 0;
            foreach (PDMS_PurchaseOrder P in SDMS_SalesOrder)
            {
                i = i + 1;
                dt.Rows.Add(
                    i
                    , P.Dealer.DealerCode
                    , P.Dealer.DealerName
                    , P.POType
                    , P.POMonth
                    , P.HeaderCount
                    , P.PurchaseOrderItem.ItemCount
                    , P.PurchaseOrderItem.OrderQuantity
                    , P.PurchaseOrderItem.GrossAmount

                    );
            }
            new BXcel().ExporttoExcel(dt, "Monthly Procurement");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataSource = SDMS_SalesOrder;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_SalesOrder.Count;

        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
    }
}