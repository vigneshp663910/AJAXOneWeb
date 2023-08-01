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
    public partial class PurchaseOrderPerformance : BasePage
    {
        public List<PDMS_PurchaseOrder> SDMS_PurchaseOrder
        {
            get
            {
                if (Session["DMS_PurchaseOrderPerformance"] == null)
                {
                    Session["DMS_PurchaseOrderPerformance"] = new List<PDMS_PurchaseOrder>();
                }
                return (List<PDMS_PurchaseOrder>)Session["DMS_PurchaseOrderPerformance"];
            }
            set
            {
                Session["DMS_PurchaseOrderPerformance"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_PurchaseOrderPerformance.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Procurement » Purchase Order Perforamance');</script>");
            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                // fillMTTR();
                // FillPageNo(1);
                txtPoDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtPoDateTo.Text = DateTime.Now.ToShortDateString();

                 
                    fillDealer(); 
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

        void fillPurchaseOrder()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                //int PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
                //int PageNo = Convert.ToInt32(ddlPageNo.SelectedValue);




                string Fillter1 = "";
                string Fillter2 = "";
                if (ddlDealerCode.SelectedValue != "0")
                {
                    Fillter1 = Fillter1 + " and po.s_tenant_id = '" + ddlDealerCode.SelectedValue + "'";
                }

                if (ddlOrderType.SelectedValue != "0")
                {
                    Fillter1 = Fillter1 + " and  o.s_object_type  = " + ddlOrderType.SelectedValue;
                }
                if (ddlLocation.SelectedValue != "0")
                {
                    Fillter1 = Fillter1 + " and po.f_location  = '" + ddlLocation.SelectedItem.Text + "'";
                }

                if (!string.IsNullOrEmpty(txtPoNumber.Text.Trim()))
                {
                    Fillter1 = Fillter1 + " and  po.p_po_id  = '" + txtPoNumber.Text.Trim() + "'";
                }

                if (!string.IsNullOrEmpty(txtMaterial.Text.Trim()))
                {
                    Fillter1 = Fillter1 + " and poi.f_material_id  = '" + txtMaterial.Text.Trim().ToUpper() + "'";
                }

                if (ddlStatus.SelectedValue != "0")
                {
                    Fillter1 = Fillter1 + " and po.s_status  = '" + ddlStatus.SelectedItem.Text + "'";
                }

                if (!string.IsNullOrEmpty(txtPoDateFrom.Text.Trim()))
                {
                    Fillter1 = Fillter1 + " and po.s_created_on >= '" + txtPoDateFrom.Text.Trim().Split('/')[1] + "/" + txtPoDateFrom.Text.Trim().Split('/')[0] + "/" + txtPoDateFrom.Text.Trim().Split('/')[2] + "'";
                }

                if (!string.IsNullOrEmpty(txtPoDateTo.Text.Trim()))
                {
                    Fillter1 = Fillter1 + " and po.s_created_on <= '" + txtPoDateTo.Text.Trim().Split('/')[1] + "/" + txtPoDateTo.Text.Trim().Split('/')[0] + "/" + txtPoDateTo.Text.Trim().Split('/')[2] + "'";
                }

                Fillter2 = Fillter1;
                if (!string.IsNullOrEmpty(txtGRDateFrom.Text.Trim()))
                {
                    Fillter1 = Fillter1 + "and gr.r_gr_date >=  '" + txtGRDateFrom.Text.Trim().Split('/')[1] + "/" + txtGRDateFrom.Text.Trim().Split('/')[0] + "/" + txtGRDateFrom.Text.Trim().Split('/')[2] + "'";
                }

                if (!string.IsNullOrEmpty(txtGRDateTo.Text.Trim()))
                {
                    Fillter1 = Fillter1 + "and gr.r_gr_date <='" + txtGRDateTo.Text.Trim().Split('/')[1] + "/" + txtGRDateTo.Text.Trim().Split('/')[0] + "/" + txtGRDateTo.Text.Trim().Split('/')[2] + "'";
                }





                string Fillter = "Where 1=1";

                if (ddlDealerCode.SelectedValue != "0")
                {
                    Fillter = "'" + ddlDealerCode.SelectedValue + "'";
                }
                else
                {
                    Fillter = "null";
                }

                if (ddlOrderType.SelectedValue != "0")
                {
                    Fillter = Fillter + ",'" + ddlOrderType.SelectedItem.Text + "'";
                    //Fillter = Fillter + "and t.f_ic_ticket_id = " + txtICServiceTicket.Text.Trim();
                }
                else
                {
                    Fillter = Fillter + ",null";
                }
                if (ddlLocation.SelectedValue != "0")
                {
                    Fillter = Fillter + ",'" + ddlLocation.SelectedItem.Text + "'";
                    //Fillter = Fillter + "and t.f_ic_ticket_id = " + txtICServiceTicket.Text.Trim();
                }
                else
                {
                    Fillter = Fillter + ",null";
                }

                if (!string.IsNullOrEmpty(txtPoNumber.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtPoNumber.Text.Trim() + "'";
                    //Fillter = Fillter + "and t.f_ic_ticket_id = " + txtICServiceTicket.Text.Trim();
                }
                else
                {
                    Fillter = Fillter + ",null";
                }
                if (!string.IsNullOrEmpty(txtMaterial.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtMaterial.Text.Trim().ToUpper() + "'";
                    //Fillter = Fillter + "and t.f_ic_ticket_id = " + txtICServiceTicket.Text.Trim();
                }
                else
                {
                    Fillter = Fillter + ",null";
                }

                if (ddlStatus.SelectedValue != "0")
                {
                    Fillter = Fillter + ",'" + ddlStatus.SelectedItem.Text + "'";
                    //Fillter = Fillter + "and t.f_ic_ticket_id = " + txtICServiceTicket.Text.Trim();
                }
                else
                {
                    Fillter = Fillter + ",null";
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

                if (!string.IsNullOrEmpty(txtGRDateFrom.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtGRDateFrom.Text.Trim().Split('/')[1] + "/" + txtGRDateFrom.Text.Trim().Split('/')[0] + "/" + txtGRDateFrom.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + "," + "null";
                }
                if (!string.IsNullOrEmpty(txtGRDateTo.Text.Trim()))
                {
                    Fillter = Fillter + ",'" + txtGRDateTo.Text.Trim().Split('/')[1] + "/" + txtGRDateTo.Text.Trim().Split('/')[0] + "/" + txtGRDateTo.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    Fillter = Fillter + "," + "null";
                }



                // List<PDMS_PurchaseOrder> PurchaseOrder = new BDMS_PurchaseOrder().GetPurchaseOrderPerformance(Fillter1);
                List<PDMS_PurchaseOrder> PurchaseOrder = new BDMS_PurchaseOrder().GetPurchaseOrderPerformanceLinq(Fillter1, Fillter2);

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
                SDMS_PurchaseOrder = PurchaseOrder;

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
                new FileLogger().LogMessage("DMS_MTTR_Report", "fillMTTR", e1);
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
            dt.Columns.Add("GR Date");
            dt.Columns.Add("GR Number");
            dt.Columns.Add("Order Type");
            dt.Columns.Add("Material");
            dt.Columns.Add("Material Desc");
            dt.Columns.Add("HSN");
            dt.Columns.Add("Order Qty");
            dt.Columns.Add("Unit Price");
            dt.Columns.Add("Net Amt");
            dt.Columns.Add("Discount");
            dt.Columns.Add("Freight");
            dt.Columns.Add("Insurance");
            dt.Columns.Add("Packing");
            dt.Columns.Add("Taxable Amount");
            dt.Columns.Add("SGST");
            dt.Columns.Add("CGST");
            dt.Columns.Add("IGST");
            dt.Columns.Add("Gross Amt");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Location");
            dt.Columns.Add("PO Status");
            dt.Columns.Add("GR Status");
            dt.Columns.Add("PO Number");
            dt.Columns.Add("PO Item");
            dt.Columns.Add("PO Date");
            dt.Columns.Add("PO Month");
            dt.Columns.Add("UOM");
            dt.Columns.Add("ASN Qty");
            dt.Columns.Add("ASN Date");
            dt.Columns.Add("GR Qty");
            dt.Columns.Add("Missing Qty");
            dt.Columns.Add("Damaged Qty");
            dt.Columns.Add("Wrong Supply Qty");
            dt.Columns.Add("PO - ASN Qty");
            dt.Columns.Add("ASN Dt - PO Dt");
            dt.Columns.Add("PO - GR Qty");
            dt.Columns.Add("GR Dt - PO Dt");
            dt.Columns.Add("Cum. Asn Qty");
            dt.Columns.Add("Latest GR Dt");
            dt.Columns.Add("Cum. Gr Qty");


            foreach (PDMS_PurchaseOrder M in SDMS_PurchaseOrder)
            {
                dt.Rows.Add(
                    M.PurchaseOrderItem.GRDate
                    , M.PurchaseOrderItem.GRNumber
                    , M.POType
                    , M.PurchaseOrderItem.Material.MaterialCode
                    , M.PurchaseOrderItem.Material.MaterialDescription
                    , M.PurchaseOrderItem.Material.HSN
                    , M.PurchaseOrderItem.OrderQuantity
                    , M.PurchaseOrderItem.UnitPrice
                    , M.PurchaseOrderItem.NetAmount
                    , M.PurchaseOrderItem.DiscountAmount
                    , M.PurchaseOrderItem.Fright
                    , M.PurchaseOrderItem.Insurance
                    , M.PurchaseOrderItem.PackingAndForwarding
                    , M.PurchaseOrderItem.TaxableAmount
                    , M.PurchaseOrderItem.SGST
                    , M.PurchaseOrderItem.CGST
                    , M.PurchaseOrderItem.IGST
                    , M.PurchaseOrderItem.GrossAmount
                    , M.Dealer.DealerCode
                    , M.Dealer.DealerName
                    , M.Location
                    , M.POStatus
                    , M.PurchaseOrderItem.GRStatus
                    , M.PurchaseOrderID
                    , M.PurchaseOrderItem.POItem
                    , M.PurchaseOrderDate
                    , M.POMonth
                    , M.PurchaseOrderItem.UOM
                    , M.PurchaseOrderItem.ASNQuantity
                    , M.PurchaseOrderItem.ASNDate
                    , M.PurchaseOrderItem.GRQuantity
                    , M.PurchaseOrderItem.MissingQuantity
                    , M.PurchaseOrderItem.DamagedQuantity
                    , M.PurchaseOrderItem.WrongSupplyQuantity
                    , M.PurchaseOrderItem.POMinusAsnQuantity
                    , M.PurchaseOrderItem.AsnMinusPODate
                    , M.PurchaseOrderItem.POMinusGrQuantity
                    , M.PurchaseOrderItem.GrMinusPODate
                    , M.PurchaseOrderItem.CumulativeAsnQuantity
                    , M.PurchaseOrderItem.LatestGrDate
                    , M.PurchaseOrderItem.CumulativeGrQuantity
                    );
            }
            new BXcel().ExporttoExcel(dt, "Purchase Order Performance Report");
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