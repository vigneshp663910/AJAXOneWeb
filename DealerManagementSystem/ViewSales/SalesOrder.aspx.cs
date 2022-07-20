using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSales
{
    public partial class SalesOrder : System.Web.UI.Page
    {
        public List<PDMS_SalesOrder> SDMS_SalesOrder
        {
            get
            {
                if (Session["PDMS_SalesOrder"] == null)
                {
                    Session["PDMS_SalesOrder"] = new List<PDMS_SalesOrder>();
                }
                return (List<PDMS_SalesOrder>)Session["PDMS_SalesOrder"];
            }
            set
            {
                Session["PDMS_SalesOrder"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            string ss = Context.Request.QueryString["previousUrl"];
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Sale Order');</script>");
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
                //int PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
                //int PageNo = Convert.ToInt32(ddlPageNo.SelectedValue);
                string Fillter = "Where 1=1";

                if (!string.IsNullOrEmpty(txtCustomer.Text.Trim()))
                {
                    Fillter = "'" + txtCustomer.Text.Trim() + "'";
                    //Fillter = Fillter + "and t.f_ic_ticket_id = " + txtICServiceTicket.Text.Trim();
                }
                else
                {
                    Fillter = "null";
                }
                if (ddlDealerCode.SelectedValue != "0")
                {
                    Fillter = Fillter + ",'" + ddlDealerCode.SelectedItem.Text + "'";
                }
                else
                {
                    Fillter = Fillter + "," + "null";
                }

                if (!string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                    Fillter = Fillter + ",'" + txtInvoiceNumber.Text.Trim() + "'";
                else
                    Fillter = Fillter + "," + "null";

                if (!string.IsNullOrEmpty(txtOrderDateFrom.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtOrderDateFrom.Text.Trim().Split('/')[1] + "/" + txtOrderDateFrom.Text.Trim().Split('/')[0] + "/" + txtOrderDateFrom.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + "," + "null";
                }
                if (!string.IsNullOrEmpty(txtOrderDateTo.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtOrderDateTo.Text.Trim().Split('/')[1] + "/" + txtOrderDateTo.Text.Trim().Split('/')[0] + "/" + txtOrderDateTo.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + "," + "null";
                }


                if (!string.IsNullOrEmpty(txtSONumber.Text.Trim()))
                    Fillter = Fillter + ",'" + txtSONumber.Text.Trim() + "'";
                else
                    Fillter = Fillter + "," + "null";

                if (!string.IsNullOrEmpty(txtSODateFrom.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtSODateFrom.Text.Trim().Split('/')[1] + "/" + txtSODateFrom.Text.Trim().Split('/')[0] + "/" + txtSODateFrom.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + "," + "null";
                }
                if (!string.IsNullOrEmpty(txtSODateTo.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtSODateTo.Text.Trim().Split('/')[1] + "/" + txtSODateTo.Text.Trim().Split('/')[0] + "/" + txtSODateTo.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + "," + "null";
                }

                if (!string.IsNullOrEmpty(txtMaterial.Text.Trim()))
                    Fillter = Fillter + ",'%" + txtMaterial.Text.Trim() + "%'";
                else
                    Fillter = Fillter + "," + "null";


                Fillter = Fillter + "," + "10000000";
                Fillter = Fillter + "," + "0";
                // int RowCount = new BDMS_MTTR().GetMttrCount(Fillter);
                // int t = (RowCount + PageSize - 1) / PageSize;
                //  FillPageNo(t);
                //   ddlPageNo.SelectedValue = PageNo.ToString();
                //  Fillter = Fillter + "," + ddlPageSize.SelectedValue;

                // Fillter = Fillter + "," + ((PageNo - 1) * PageSize).ToString();


                List<PDMS_SalesOrder> SOIs = new BDMS_SalesOrder().GetSalesOrder(Fillter);

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
                new FileLogger().LogMessage("DMS_MTTR_Report", "fillMTTR", e1);
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
            dt.Columns.Add("Customer");
            dt.Columns.Add("CustomerName");
            dt.Columns.Add("InvoiceNumber");
            dt.Columns.Add("InvoiceDate");
            dt.Columns.Add("PartNumber");
            dt.Columns.Add("Description");
            dt.Columns.Add("MatType");
            dt.Columns.Add("Division");

            dt.Columns.Add("Qty");
            dt.Columns.Add("Basic");
            dt.Columns.Add("Discount");
            dt.Columns.Add("BasicAfterDisc");
            dt.Columns.Add("Tax");
            dt.Columns.Add("FreightInsurance");
            dt.Columns.Add("TotalAmt");
            foreach (PDMS_SalesOrder M in SDMS_SalesOrder)
            {
                dt.Rows.Add(
                    M.Customer
                    , M.Customer.CustomerName
                    , M.InvoiceNumber
                    , M.InvoiceDate
                    , "'" + M.SalesOrderItem.Material.MaterialCode
                    , M.SalesOrderItem.Material.MaterialDescription
                    , M.SalesOrderItem.Material.MaterialType
                    , M.SalesOrderItem.Material.MaterialDivision
                    , decimal.Round(M.SalesOrderItem.Qty, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.SalesOrderItem.Value, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.SalesOrderItem.Discount, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.SalesOrderItem.DiscountedPrice, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.SalesOrderItem.Tax, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.SalesOrderItem.FreightAmount, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.SalesOrderItem.TotalAmt, 2, MidpointRounding.AwayFromZero)
                    );
            }
            new BXcel().ExporttoExcel(dt, "Sales Order Report");
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
            ddlDealerCode.DataTextField = "UserName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
    }
}