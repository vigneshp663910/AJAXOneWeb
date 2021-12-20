using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Properties;
using System.Data;
using System.Drawing;

namespace DealerManagementSystem.ViewService
{
    public partial class PaidServiceReport : System.Web.UI.Page
    {
        public List<PDMS_PaidServiceHeader> SDMS_PurchaseOrder
        {
            get
            {
                if (Session["DMS_PaidServiceReport"] == null)
                {
                    Session["DMS_PaidServiceReport"] = new List<PDMS_PaidServiceHeader>();
                }
                return (List<PDMS_PaidServiceHeader>)Session["DMS_PaidServiceReport"];
            }
            set
            {
                Session["DMS_PaidServiceReport"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_PaidServiceReport.aspx";
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
                // fillMTTR();
                // FillPageNo(1);
                txtPoDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year; ;
                txtPoDateTo.Text = DateTime.Now.ToShortDateString();

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
                fillPaidService();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillPaidService()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                string Fillter = "Where 1=1";
                if (!string.IsNullOrEmpty(txtPoNumber.Text.Trim()))
                {
                    Fillter = "'" + txtPoNumber.Text.Trim() + "'";
                }
                else
                {
                    Fillter = "null";
                }
                if (!string.IsNullOrEmpty(txtPoDateFrom.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtPoDateFrom.Text.Trim().Split('/')[1] + "/" + txtPoDateFrom.Text.Trim().Split('/')[0] + "/" + txtPoDateFrom.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + "," + "null";
                }
                if (!string.IsNullOrEmpty(txtPoDateTo.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtPoDateTo.Text.Trim().Split('/')[1] + "/" + txtPoDateTo.Text.Trim().Split('/')[0] + "/" + txtPoDateTo.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + "," + "null";
                }
                if (ddlDealerCode.SelectedValue != "0")
                {
                    Fillter = Fillter + ",'" + ddlDealerCode.SelectedValue + "'";
                }
                else
                {
                    Fillter = Fillter + "," + "null";
                }

                if (ddlPOStatus.SelectedValue != "0")
                {
                    Fillter = Fillter + ",'" + ddlPOStatus.SelectedItem.Text + "'";
                }
                else
                {
                    Fillter = Fillter + "," + "null";
                }

                List<PDMS_PurchaseOrder> PurchaseOrder = new BDMS_PurchaseOrder().GetPurchaseOrder(Fillter);


                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in PurchaseOrder
                                 join D in PSession.User.Dealer on S.Dealer.DealerCode equals D.UserName
                                 select new
                                 {
                                     S
                                 }).ToList();
                    PurchaseOrder.Clear();
                    foreach (var w in SOIs1)
                    {
                        PurchaseOrder.Add(w.S);
                    }
                }

                //   SDMS_PurchaseOrder = PurchaseOrder;

                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = PurchaseOrder;
                gvICTickets.DataBind();
                if (PurchaseOrder.Count == 0)
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
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_PurchaseOrder.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_PaidServiceReport", "fillPaidService", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;
                gvICTickets.DataSource = SDMS_PurchaseOrder;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_PurchaseOrder.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataSource = SDMS_PurchaseOrder;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_PurchaseOrder.Count;
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


            //foreach (PDMS_PurchaseOrder M in SDMS_PurchaseOrder)
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
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
    }
}