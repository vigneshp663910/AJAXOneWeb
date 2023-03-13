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
    public partial class SalesOrderItems : BasePage
    {
   //     public override SubModule SubModuleName { get { return SubModule.ViewSales_SalesOrderItems; } }
        public List<PDMS_SalesOrderItems> SDMS_SalesOrderItems
        {
            get
            {
                if (Session["PDMS_SalesOrderItems"] == null)
                {
                    Session["PDMS_SalesOrderItems"] = new List<PDMS_SalesOrderItems>();
                }
                return (List<PDMS_SalesOrderItems>)Session["PDMS_SalesOrderItems"];
            }
            set
            {
                Session["PDMS_SalesOrderItems"] = value;
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
                string Fillter = "";

                if (!string.IsNullOrEmpty(txtCustomer.Text.Trim()))
                {
                    Fillter = Fillter + " and so.f_customer_id = '" + txtCustomer.Text.Trim() + "'";
                    //Fillter = Fillter + "and t.f_ic_ticket_id = " + txtICServiceTicket.Text.Trim();
                }

                if (ddlDealerCode.SelectedValue != "0")
                {
                    Fillter = Fillter + " and so.s_tenant_id = '" + ddlDealerCode.SelectedValue + "'";
                }


                if (!string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                    Fillter = Fillter + " and inv.p_inv_id = '" + txtInvoiceNumber.Text.Trim() + "'";


                if (!string.IsNullOrEmpty(txtInvoiceDateFrom.Text.Trim()))
                {
                    Fillter = Fillter + " and inv.r_inv_date >= '" + txtInvoiceDateFrom.Text.Trim().Split('/')[1] + "/" + txtInvoiceDateFrom.Text.Trim().Split('/')[0] + "/" + txtInvoiceDateFrom.Text.Trim().Split('/')[2] + "'";
                }

                if (!string.IsNullOrEmpty(txtInvoiceDateTo.Text.Trim()))
                {
                    Fillter = Fillter + " and inv.r_inv_date <= '" + txtInvoiceDateTo.Text.Trim().Split('/')[1] + "/" + txtInvoiceDateTo.Text.Trim().Split('/')[0] + "/" + txtInvoiceDateTo.Text.Trim().Split('/')[2] + "'";
                }



                if (!string.IsNullOrEmpty(txtSONumber.Text.Trim()))
                    Fillter = Fillter + " and so.p_so_Id = '" + txtSONumber.Text.Trim() + "'";


                if (!string.IsNullOrEmpty(txtSODateFrom.Text.Trim()))
                {
                    Fillter = Fillter + " and so.r_order_date >= '" + txtSODateFrom.Text.Trim().Split('/')[1] + "/" + txtSODateFrom.Text.Trim().Split('/')[0] + "/" + txtSODateFrom.Text.Trim().Split('/')[2] + "'";
                }

                if (!string.IsNullOrEmpty(txtSODateTo.Text.Trim()))
                {
                    Fillter = Fillter + " and so.r_order_date <= '" + txtSODateTo.Text.Trim().Split('/')[1] + "/" + txtSODateTo.Text.Trim().Split('/')[0] + "/" + txtSODateTo.Text.Trim().Split('/')[2] + "'";
                }


                if (!string.IsNullOrEmpty(txtMaterial.Text.Trim()))
                    Fillter = Fillter + " and soi.f_material_id  = '" + txtMaterial.Text.Trim().ToUpper() + "'";

                if (ddlSOStatus.SelectedValue != "0")
                {
                    Fillter = Fillter + " and so.s_status  = '" + ddlSOStatus.SelectedItem.Text + "'";
                }

                if (ddlSalesType.SelectedValue == "0")
                    Fillter = Fillter + " and soi.s_object_type = 101 and so.f_division='SP' and so.r_ext_id is null";
                else if (ddlSalesType.SelectedValue == "1")
                    Fillter = Fillter + " and soi.s_object_type  in (201,208)";
                else if (ddlSalesType.SelectedValue == "2")
                    Fillter = Fillter + " and soi.s_object_type = 208 ";
                else if (ddlSalesType.SelectedValue == "3")
                    Fillter = Fillter + " and soi.s_object_type = 201 ";
                else if (ddlSalesType.SelectedValue == "4")
                    Fillter = Fillter + " and soi.s_object_type in (104,108)";
                else if (ddlSalesType.SelectedValue == "5")
                    Fillter = Fillter + " and soi.s_object_type = 108 ";
                else if (ddlSalesType.SelectedValue == "6")
                    Fillter = Fillter + " and soi.s_object_type = 104 ";

                Fillter = Fillter + " order by inv.f_customer_id, so.p_so_Id desc";
                List<PDMS_SalesOrderItems> SOIs = new BDMS_SalesOrder().GetSalesOrderItems(Fillter);
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
                SDMS_SalesOrderItems = SOIs;


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
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_SalesOrderItems.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_SalesOrderItems", "fillSalesOrderItems", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;
                gvICTickets.DataSource = SDMS_SalesOrderItems;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_SalesOrderItems.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataSource = SDMS_SalesOrderItems;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_SalesOrderItems.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Customer");
            dt.Columns.Add("CustomerName");
            dt.Columns.Add("GSTNo");
            dt.Columns.Add("SO No");
            dt.Columns.Add("SO Date");
            dt.Columns.Add("SO Status");
            dt.Columns.Add("InvoiceNumber");
            dt.Columns.Add("InvoiceDate");
            dt.Columns.Add("PartNumber");
            dt.Columns.Add("Description");
            dt.Columns.Add("HSNCode");
            dt.Columns.Add("UnitBasicPrice");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Value");
            dt.Columns.Add("Discount");
            dt.Columns.Add("DiscountedPrice");
            dt.Columns.Add("FreightAmount");
            dt.Columns.Add("TaxableAmount");
            dt.Columns.Add("SGST");
            dt.Columns.Add("SGSTAmt");
            dt.Columns.Add("CGST");
            dt.Columns.Add("CGSTAmt");
            dt.Columns.Add("IGST");
            dt.Columns.Add("IGSTAmt");
            dt.Columns.Add("Tax");
            dt.Columns.Add("TotalAmt");
            dt.Columns.Add("MatType");
            dt.Columns.Add("Division");
            dt.Columns.Add("Location");
            foreach (PDMS_SalesOrderItems M in SDMS_SalesOrderItems)
            {
                dt.Rows.Add(
                    M.Dealer.DealerCode
                    , M.Dealer.DealerName
                    , M.Customer
                    , M.CustomerName
                    , M.GSTNo
                    , M.SONumber
                    , M.SODate.ToShortDateString()
                    , M.SOStatus
                    , M.InvoiceNumber
                    , M.InvoiceDate == null ? "" : ((DateTime)M.InvoiceDate).ToShortDateString()
                    , "'" + M.Material.MaterialCode
                    , M.Material.MaterialDescription
                    , M.Material.HSN
                    , decimal.Round(M.UnitBasicPrice, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.Qty, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.Value, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.Discount, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.DiscountedPrice, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.FreightAmount, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.TaxableAmount, 2, MidpointRounding.AwayFromZero)
                    , M.SGST == null ? (decimal?)null : decimal.Round((decimal)M.SGST, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.SGSTAmt, 2, MidpointRounding.AwayFromZero)
                    , M.CGST == null ? (decimal?)null : decimal.Round((decimal)M.CGST, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.CGSTAmt, 2, MidpointRounding.AwayFromZero)
                    , M.IGST == null ? (decimal?)null : decimal.Round((decimal)M.IGST, 2, MidpointRounding.AwayFromZero)
                    , decimal.Round(M.IGSTAmt, 2, MidpointRounding.AwayFromZero)
                     , decimal.Round(M.Tax, 2, MidpointRounding.AwayFromZero)
                   , decimal.Round(M.TotalAmt, 2, MidpointRounding.AwayFromZero)
                    , M.MatType
                    , M.Division
                    , M.Location);
            }
            new BXcel().ExporttoExcel(dt, "Sales Order Report");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataSource = SDMS_SalesOrderItems;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_SalesOrderItems.Count;

        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void ddlSalesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSalesType.SelectedValue == "0")
            {
                ddlSOStatus.Items.Insert(0, new ListItem("Customer Order", "0"));
            }
            else
            {
                ddlSOStatus.Items.Insert(0, new ListItem("All", "0"));
                ddlSOStatus.Items.Insert(1, new ListItem("Machine Quotation", "1"));
                ddlSOStatus.Items.Insert(1, new ListItem("Machine Order", "1"));
            }

        }
    }
}