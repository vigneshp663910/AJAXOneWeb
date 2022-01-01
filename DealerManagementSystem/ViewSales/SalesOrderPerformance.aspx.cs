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
    public partial class SalesOrderPerformance : System.Web.UI.Page
    {
        public List<PDMS_SalesOrder1> SDMS_SalesOrder
        {
            get
            {
                if (Session["DMS_SalesOrderPerformance"] == null)
                {
                    Session["DMS_SalesOrderPerformance"] = new List<PDMS_SalesOrder1>();
                }
                return (List<PDMS_SalesOrder1>)Session["DMS_SalesOrderPerformance"];
            }
            set
            {
                Session["DMS_SalesOrderPerformance"] = value;
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
                txtSODateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtSODateTo.Text = DateTime.Now.ToShortDateString();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillSalesOrderPerformance();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillSalesOrderPerformance()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);


                string Fillter1 = "";




                Fillter1 = Fillter1 + (string.IsNullOrEmpty(txtCustomer.Text.Trim()) ? "" : " and so.f_customer_id = '" + txtCustomer.Text.Trim() + "'");
                Fillter1 = Fillter1 + (string.IsNullOrEmpty(txtQuotetionNumber.Text.Trim()) ? "" : " and so.p_so_Id = '" + txtQuotetionNumber.Text.Trim() + "'");
                Fillter1 = Fillter1 + (string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()) ? "" : " and inv.p_inv_id = '" + txtInvoiceNumber.Text.Trim() + "'");
                Fillter1 = Fillter1 + (string.IsNullOrEmpty(txtSONumber.Text.Trim()) ? "" : " and so.p_so_Id = '" + txtSONumber.Text.Trim() + "'");
                Fillter1 = Fillter1 + (string.IsNullOrEmpty(txtMaterial.Text.Trim()) ? "" : " and soi.f_material_id = '" + txtMaterial.Text.Trim() + "'");

                Fillter1 = Fillter1 + (ddlSOStatus.SelectedValue == "0" ? "" : " and so.s_status = '" + ddlSOStatus.SelectedValue + "'");

                Fillter1 = Fillter1 + (string.IsNullOrEmpty(txtSODateFrom.Text.Trim()) ? "" : " and so.r_order_date >= '" + txtSODateFrom.Text.Trim().Split('/')[1] + "/" + txtSODateFrom.Text.Trim().Split('/')[0] + "/" + txtSODateFrom.Text.Trim().Split('/')[2] + "'");
                Fillter1 = Fillter1 + (string.IsNullOrEmpty(txtSODateTo.Text.Trim()) ? "" : " and so.r_order_date <= '" + txtSODateTo.Text.Trim().Split('/')[1] + "/" + txtSODateTo.Text.Trim().Split('/')[0] + "/" + txtSODateTo.Text.Trim().Split('/')[2] + "'");

                Fillter1 = Fillter1 + (string.IsNullOrEmpty(txtDeliveryDateFrom.Text.Trim()) ? "" : " and d.r_del_date >= '" + txtDeliveryDateFrom.Text.Trim().Split('/')[1] + "/" + txtDeliveryDateFrom.Text.Trim().Split('/')[0] + "/" + txtDeliveryDateFrom.Text.Trim().Split('/')[2] + "'");
                Fillter1 = Fillter1 + (string.IsNullOrEmpty(txtDeliveryDateTo.Text.Trim()) ? "" : " and d.r_del_date <= '" + txtDeliveryDateTo.Text.Trim().Split('/')[1] + "/" + txtDeliveryDateTo.Text.Trim().Split('/')[0] + "/" + txtDeliveryDateTo.Text.Trim().Split('/')[2] + "'");

                Fillter1 = Fillter1 + (string.IsNullOrEmpty(txtOrderDateFrom.Text.Trim()) ? "" : " and inv.r_inv_date >= '" + txtOrderDateFrom.Text.Trim().Split('/')[1] + "/" + txtOrderDateFrom.Text.Trim().Split('/')[0] + "/" + txtOrderDateFrom.Text.Trim().Split('/')[2] + "'");
                Fillter1 = Fillter1 + (string.IsNullOrEmpty(txtOrderDateTo.Text.Trim()) ? "" : " and inv.r_inv_date <= '" + txtOrderDateTo.Text.Trim().Split('/')[1] + "/" + txtOrderDateTo.Text.Trim().Split('/')[0] + "/" + txtOrderDateTo.Text.Trim().Split('/')[2] + "'");


                string Fillter = (ddlDealerCode.SelectedValue == "0" ? "null" : "" + ddlDealerCode.SelectedValue + "");
                Fillter = Fillter + (string.IsNullOrEmpty(txtCustomer.Text.Trim()) ? ",null" : ",'" + txtCustomer.Text.Trim() + "'");
                Fillter = Fillter + (string.IsNullOrEmpty(txtQuotetionNumber.Text.Trim()) ? ",null" : ",'" + txtQuotetionNumber.Text.Trim() + "'");
                Fillter = Fillter + (string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()) ? ",null" : ",'" + txtInvoiceNumber.Text.Trim() + "'");
                Fillter = Fillter + (string.IsNullOrEmpty(txtSONumber.Text.Trim()) ? ",null" : ",'" + txtSONumber.Text.Trim() + "'");
                Fillter = Fillter + (string.IsNullOrEmpty(txtMaterial.Text.Trim()) ? ",null" : ",'" + txtMaterial.Text.Trim().ToUpper() + "'");
                Fillter = Fillter + (ddlSOStatus.SelectedValue == "0" ? ",null" : ",'" + ddlSOStatus.SelectedItem.Text + "'");

                if (!string.IsNullOrEmpty(txtSODateFrom.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtSODateFrom.Text.Trim().Split('/')[1] + "/" + txtSODateFrom.Text.Trim().Split('/')[0] + "/" + txtSODateFrom.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + ",null";
                }
                if (!string.IsNullOrEmpty(txtSODateTo.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtSODateTo.Text.Trim().Split('/')[1] + "/" + txtSODateTo.Text.Trim().Split('/')[0] + "/" + txtSODateTo.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + ",null";
                }


                if (!string.IsNullOrEmpty(txtDeliveryDateFrom.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtDeliveryDateFrom.Text.Trim().Split('/')[1] + "/" + txtDeliveryDateFrom.Text.Trim().Split('/')[0] + "/" + txtDeliveryDateFrom.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + ",null";
                }

                if (!string.IsNullOrEmpty(txtDeliveryDateTo.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtDeliveryDateTo.Text.Trim().Split('/')[1] + "/" + txtDeliveryDateTo.Text.Trim().Split('/')[0] + "/" + txtDeliveryDateTo.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + ",null";
                }



                if (!string.IsNullOrEmpty(txtOrderDateFrom.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtOrderDateFrom.Text.Trim().Split('/')[1] + "/" + txtOrderDateFrom.Text.Trim().Split('/')[0] + "/" + txtOrderDateFrom.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + ",null";
                }

                if (!string.IsNullOrEmpty(txtOrderDateTo.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtOrderDateTo.Text.Trim().Split('/')[1] + "/" + txtOrderDateTo.Text.Trim().Split('/')[0] + "/" + txtOrderDateTo.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + ",null";
                }
                List<PDMS_SalesOrder1> SOIs = null;

                if (ddlSalesType.SelectedValue == "0")
                {
                    Fillter1 = Fillter1 + (ddlDealerCode.SelectedValue == "0" ? "" : " and so.s_tenant_id = '" + ddlDealerCode.SelectedValue + "'");
                    SOIs = new BDMS_SalesOrder().GetSalesOrderPerfomanceParts(Fillter1);
                    gvICTickets.Columns[3].Visible = false;
                    gvICTickets.Columns[11].Visible = false;
                    gvICTickets.Columns[12].Visible = false;
                    gvICTickets.Columns[13].Visible = false;
                    gvICTickets.Columns[22].Visible = false;
                }
                else
                {
                    Fillter1 = Fillter1 + (ddlDealerCode.SelectedValue == "0" ? "" : " and Qt.s_tenant_id = '" + ddlDealerCode.SelectedValue + "'");
                    SOIs = new BDMS_SalesOrder().GetSalesOrderPerfomance(Fillter1);
                    gvICTickets.Columns[3].Visible = true;
                    gvICTickets.Columns[11].Visible = true;
                    gvICTickets.Columns[12].Visible = true;
                    gvICTickets.Columns[13].Visible = true;
                    gvICTickets.Columns[22].Visible = true;
                }

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
                new FileLogger().LogMessage("DMS_SalesOrderPerformance", "fillSalesOrderPerformance", e1);
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
            dt.Columns.Add("Dealer");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Location");
            dt.Columns.Add("SO Status");
            dt.Columns.Add("Customer");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Contact Person");
            dt.Columns.Add("Contact Number");

            dt.Columns.Add("Material");
            dt.Columns.Add("Mat Desc");
            dt.Columns.Add("SO No");
            dt.Columns.Add("SO Date");
            dt.Columns.Add("SO Quantity");
            dt.Columns.Add("Delivery No");
            dt.Columns.Add("Delivery Date");
            dt.Columns.Add("Delivery Quantity");
            dt.Columns.Add("Invoice No");
            dt.Columns.Add("Invoice Date");
            dt.Columns.Add("Quote - Inv Qty");
            dt.Columns.Add("SO - Del Qty");
            dt.Columns.Add("SO - Inv Qty");
            dt.Columns.Add("Del- SO Days");
            dt.Columns.Add("Inv- SO Days");
            dt.Columns.Add("Paymnt Terms");
            dt.Columns.Add("Transprt");

            foreach (PDMS_SalesOrder1 M in SDMS_SalesOrder)
            {

                dt.Rows.Add(
                    M.Dealer.DealerCode,
                    M.Dealer.DealerName
                    , M.Location
                    , M.SalesOrderStatus
                     , M.Customer.CustomerCode
                    , M.Customer.CustomerName
                    , M.ContactPerson
                   , M.ContactNumber
                   , M.SalesOrderItems.Material.MaterialCode
                   , M.SalesOrderItems.Material.MaterialDescription
                    , M.SalesOrderNumber
                    , M.SalesOrderDate
                    , decimal.Round(M.SalesOrderItems.SalesOrderQuantity, 2, MidpointRounding.AwayFromZero)
                   , M.DeliveryNumber
                    , M.DeliveryDate
                    , decimal.Round(M.SalesOrderItems.DeliveryQuantity, 2, MidpointRounding.AwayFromZero)

                    , M.InvoiceNumber
                    , M.InvoiceDate
                    // , decimal.Round(M.SalesOrderItems.InvoiceQuantity, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.SalesOrderItems.QuotationMinusInvoiceQuantity, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.SalesOrderItems.SalesOrderMinusDeliveryQuantity, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.SalesOrderItems.SalesOrderMinusInvoiceQuantity, 2, MidpointRounding.AwayFromZero)
                    , M.DeliveryMinusSalesOrderDays
                    , M.InvoiceMinusSalesOrderDays
                    , M.PaymntTerms
                    , M.Transprt

                    );
            }
            new BXcel().ExporttoExcel(dt, "Sales Order Performance Report");
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