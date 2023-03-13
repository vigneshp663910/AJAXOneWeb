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
    public partial class SaleOrderInvoiceReport : BasePage
    {
      //  public override SubModule SubModuleName { get { return SubModule.ViewSales_SaleOrderInvoiceReport; } }
        public List<PDMS_SalesInvoice> SDMS_SalesInvoice
        {
            get
            {
                if (Session["DMS_SalesInvoice"] == null)
                {
                    Session["DMS_SalesInvoice"] = new List<PDMS_SalesInvoice>();
                }
                return (List<PDMS_SalesInvoice>)Session["DMS_SalesInvoice"];
            }
            set
            {
                Session["DMS_SalesInvoice"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » M/C Invoice ');</script>");
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
                txtInvoiceDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtInvoiceDateTo.Text = DateTime.Now.ToShortDateString();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillSalesInvoice();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillSalesInvoice()
        {
            int? DealerID = null;
            string CustomerCode = "";
            string InvoiceNumber = "";
            DateTime? InvoiceDateF = null;
            DateTime? InvoiceDateT = null;
            string SaleOrderInvoiceStatusID = "";
            try
            {
                TraceLogger.Log(DateTime.Now);
                //int PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
                //int PageNo = Convert.ToInt32(ddlPageNo.SelectedValue);
                string Fillter = "";

                if (!string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
                {
                    Fillter = Fillter + " and inv.f_customer_id = '" + txtCustomerCode.Text.Trim() + "'";
                    //Fillter = Fillter + "and t.f_ic_ticket_id = " + txtICServiceTicket.Text.Trim();
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



                if (!string.IsNullOrEmpty(txtMaterial.Text.Trim()))
                    Fillter = Fillter + " and invi.f_material_id  = '" + txtMaterial.Text.Trim().ToUpper() + "'";

                Fillter = Fillter + new ZF().GetStatus(lbStatus, " and inv.s_status");

                //if (ddlSOStatus.SelectedValue != "0")
                //{
                //    Fillter = Fillter + " and s_status  = '" + ddlSOStatus.SelectedItem.Text + "'";
                //}

                if (ddlSalesType.SelectedValue == "0")
                    Fillter = Fillter + " and inv.s_object_type = 101 and inv.f_division='SP' "; //and so.r_ext_id is null
                else if (ddlSalesType.SelectedValue == "1")
                    Fillter = Fillter + " and     in (201,208)";
                else if (ddlSalesType.SelectedValue == "2")
                    Fillter = Fillter + " and inv.s_object_type = 208 ";
                else if (ddlSalesType.SelectedValue == "3")
                    Fillter = Fillter + " and inv.s_object_type = 201 ";
                else if (ddlSalesType.SelectedValue == "4")
                    Fillter = Fillter + " and inv.s_object_type in (104,108)";
                else if (ddlSalesType.SelectedValue == "5")
                    Fillter = Fillter + " and inv.s_object_type = 108 ";
                else if (ddlSalesType.SelectedValue == "6")
                    Fillter = Fillter + " and inv.s_object_type = 104 ";


                List<PDMS_SalesInvoice> SOIs = null;

                GridView gv = null;
                gvICTickets.Visible = false;
                gvDM.Visible = false;
                gvDCM.Visible = false;
                if (rbDetail.Checked)
                {
                    DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                    CustomerCode = txtCustomerCode.Text.Trim();
                    InvoiceNumber = txtInvoiceNumber.Text.Trim();
                    InvoiceDateF = string.IsNullOrEmpty(txtInvoiceDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtInvoiceDateFrom.Text.Trim());
                    InvoiceDateT = string.IsNullOrEmpty(txtInvoiceDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtInvoiceDateTo.Text.Trim());
                    SaleOrderInvoiceStatusID = new ZF().GetStatus(lbStatus, "");
                    SOIs = new BDMS_SalesOrder().GetSalesInvoiceDetails(DealerID, CustomerCode, InvoiceNumber, InvoiceDateF, InvoiceDateT, SaleOrderInvoiceStatusID, "");
                    gv = gvICTickets;
                }

                if (ddlDealerCode.SelectedValue != "0")
                {
                    Fillter = Fillter + " and   inv.s_tenant_id = '" + ddlDealerCode.SelectedValue + "'";
                }
                if (rbDealerMaterial.Checked)
                {

                    SOIs = new BDMS_SalesOrder().GetSalesInvoiceDealerAndMaterialWise(Fillter);
                    gv = gvDM;
                }
                if (rbDealerCustomerMaterial.Checked)
                {
                    SOIs = new BDMS_SalesOrder().GetSalesInvoiceDealerCustomerAndMaterialWise(Fillter);
                    gv = gvDCM;
                }
                gv.Visible = true;
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
                SDMS_SalesInvoice = SOIs;


                gv.PageIndex = 0;
                gv.DataSource = SOIs;
                gv.DataBind();
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
                    lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_SalesInvoice.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_SalesOrderItems", "fillSalesOrderItems", e1);
                throw e1;
            }
        }
        //void fillSalesInvoice()
        //{
        //    try
        //    {
        //        TraceLogger.Log(DateTime.Now);
        //        //int PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        //        //int PageNo = Convert.ToInt32(ddlPageNo.SelectedValue);
        //        string Fillter = "";

        //        if (!string.IsNullOrEmpty(txtCustomer.Text.Trim()))
        //        {
        //            Fillter = Fillter + " and inv.f_customer_id = '" + txtCustomer.Text.Trim() + "'";
        //            //Fillter = Fillter + "and t.f_ic_ticket_id = " + txtICServiceTicket.Text.Trim();
        //        }




        //        if (!string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
        //            Fillter = Fillter + " and inv.p_inv_id = '" + txtInvoiceNumber.Text.Trim() + "'";


        //        if (!string.IsNullOrEmpty(txtInvoiceDateFrom.Text.Trim()))
        //        {
        //            Fillter = Fillter + " and inv.r_inv_date >= '" + txtInvoiceDateFrom.Text.Trim().Split('/')[1] + "/" + txtInvoiceDateFrom.Text.Trim().Split('/')[0] + "/" + txtInvoiceDateFrom.Text.Trim().Split('/')[2] + "'";
        //        }

        //        if (!string.IsNullOrEmpty(txtInvoiceDateTo.Text.Trim()))
        //        {
        //            Fillter = Fillter + " and inv.r_inv_date <= '" + txtInvoiceDateTo.Text.Trim().Split('/')[1] + "/" + txtInvoiceDateTo.Text.Trim().Split('/')[0] + "/" + txtInvoiceDateTo.Text.Trim().Split('/')[2] + "'";
        //        }



        //        if (!string.IsNullOrEmpty(txtMaterial.Text.Trim()))
        //            Fillter = Fillter + " and invi.f_material_id  = '" + txtMaterial.Text.Trim().ToUpper() + "'";

        //        Fillter = Fillter + new ZF().GetStatus(lbStatus, " and inv.s_status");

        //        //if (ddlSOStatus.SelectedValue != "0")
        //        //{
        //        //    Fillter = Fillter + " and s_status  = '" + ddlSOStatus.SelectedItem.Text + "'";
        //        //}

        //        if (ddlSalesType.SelectedValue == "0")
        //            Fillter = Fillter + " and inv.s_object_type = 101 and inv.f_division='SP' "; //and so.r_ext_id is null
        //        else if (ddlSalesType.SelectedValue == "1")
        //            Fillter = Fillter + " and inv.s_object_type  in (201,208)";
        //        else if (ddlSalesType.SelectedValue == "2")
        //            Fillter = Fillter + " and inv.s_object_type = 208 ";
        //        else if (ddlSalesType.SelectedValue == "3")
        //            Fillter = Fillter + " and inv.s_object_type = 201 ";
        //        else if (ddlSalesType.SelectedValue == "4")
        //            Fillter = Fillter + " and inv.s_object_type in (104,108)";
        //        else if (ddlSalesType.SelectedValue == "5")
        //            Fillter = Fillter + " and inv.s_object_type = 108 ";
        //        else if (ddlSalesType.SelectedValue == "6")
        //            Fillter = Fillter + " and inv.s_object_type = 104 ";


        //        List<PDMS_SalesInvoice> SOIs = null;

        //        GridView gv = null;
        //        gvICTickets.Visible = false;
        //        gvDM.Visible = false;
        //        gvDCM.Visible = false;
        //        if (rbDetail.Checked)
        //        {
        //            Fillter = Fillter + " order by inv.f_customer_id, so.p_so_Id desc";
        //            SOIs = new BDMS_SalesOrder().GetSalesInvoiceDetails(Fillter, ddlDealerCode.SelectedValue == "0" ? "" : ddlDealerCode.SelectedValue);
        //            gv = gvICTickets;                   
        //        }

        //        if (ddlDealerCode.SelectedValue != "0")
        //        {
        //            Fillter = Fillter + " and   inv.s_tenant_id = '" + ddlDealerCode.SelectedValue + "'";
        //        }
        //        if (rbDealerMaterial.Checked)
        //        {

        //            SOIs = new BDMS_SalesOrder().GetSalesInvoiceDealerAndMaterialWise(Fillter);
        //            gv = gvDM;
        //        }
        //        if (rbDealerCustomerMaterial.Checked)
        //        {
        //            SOIs = new BDMS_SalesOrder().GetSalesInvoiceDealerCustomerAndMaterialWise(Fillter);
        //            gv = gvDCM;
        //        }
        //        gv.Visible = true;
        //        if (ddlDealerCode.SelectedValue == "0")
        //        {
        //            var SOIs1 = (from S in SOIs
        //                         join D in PSession.User.Dealer on S.Dealer.DealerCode equals D.UserName
        //                         select new
        //                         {
        //                             S
        //                         }).ToList();
        //            SOIs.Clear();
        //            foreach (var w in SOIs1)
        //            {
        //                SOIs.Add(w.S);
        //            }
        //        }
        //        SDMS_SalesInvoice = SOIs;


        //        gv.PageIndex = 0;
        //        gv.DataSource = SOIs;
        //        gv.DataBind();
        //        if (SOIs.Count == 0)
        //        {
        //            lblRowCount.Visible = false;
        //            ibtnArrowLeft.Visible = false;
        //            ibtnArrowRight.Visible = false;
        //        }
        //        else
        //        {
        //            lblRowCount.Visible = true;
        //            ibtnArrowLeft.Visible = true;
        //            ibtnArrowRight.Visible = true;
        //            lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_SalesInvoice.Count;
        //        }
        //        TraceLogger.Log(DateTime.Now);
        //    }
        //    catch (Exception e1)
        //    {
        //        new FileLogger().LogMessage("DMS_SalesOrderItems", "fillSalesOrderItems", e1);
        //        throw e1;
        //    }
        //}
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            GridView gv = null;
            if (rbDetail.Checked)
            {
                gv = gvICTickets;
            }
            if (rbDealerMaterial.Checked)
            {
                gv = gvDM;
            }
            if (rbDealerCustomerMaterial.Checked)
            {
                gv = gvDCM;
            }
            if (gv.PageIndex > 0)
            {
                gv.PageIndex = gv.PageIndex - 1;
                gv.DataSource = SDMS_SalesInvoice;
                gv.DataBind();
                lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_SalesInvoice.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            GridView gv = null;
            if (rbDetail.Checked)
            {
                gv = gvICTickets;
            }
            if (rbDealerMaterial.Checked)
            {
                gv = gvDM;
            }
            if (rbDealerCustomerMaterial.Checked)
            {
                gv = gvDCM;
            }
            if (gv.PageCount > gv.PageIndex)
            {

                gv.PageIndex = gv.PageIndex + 1;
                gv.DataSource = SDMS_SalesInvoice;
                gv.DataBind();
                lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_SalesInvoice.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

            if (rbDetail.Checked)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Dealer Code");
                dt.Columns.Add("Dealer Name");
                dt.Columns.Add("Customer");
                dt.Columns.Add("CustomerName");
                dt.Columns.Add("GSTNo");
                dt.Columns.Add("Status");
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
                foreach (PDMS_SalesInvoice M in SDMS_SalesInvoice)
                {
                    dt.Rows.Add(
                        M.Dealer.DealerCode
                        , M.Dealer.DealerName
                        , M.Customer.CustomerCode
                        , M.Customer.CustomerName
                        , M.GSTNo
                        , M.Status
                        , M.InvoiceNumber
                        , M.InvoiceDate == null ? "" : ((DateTime)M.InvoiceDate).ToShortDateString()
                        , "'" + M.InvoiceItem.Material.MaterialCode
                        , M.InvoiceItem.Material.MaterialDescription
                        , M.InvoiceItem.Material.HSN
                        , decimal.Round(M.InvoiceItem.UnitBasicPrice, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.Qty, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.Value, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.Discount, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.DiscountedPrice, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.FreightAmount, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.TaxableAmount, 2, MidpointRounding.AwayFromZero)
                        , M.InvoiceItem.SGST == null ? (decimal?)null : decimal.Round((decimal)M.InvoiceItem.SGST, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.SGSTAmt, 2, MidpointRounding.AwayFromZero)
                        , M.InvoiceItem.CGST == null ? (decimal?)null : decimal.Round((decimal)M.InvoiceItem.CGST, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.CGSTAmt, 2, MidpointRounding.AwayFromZero)
                        , M.InvoiceItem.IGST == null ? (decimal?)null : decimal.Round((decimal)M.InvoiceItem.IGST, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.IGSTAmt, 2, MidpointRounding.AwayFromZero)
                         , decimal.Round(M.InvoiceItem.Tax, 2, MidpointRounding.AwayFromZero)
                       , decimal.Round(M.InvoiceItem.TotalAmt, 2, MidpointRounding.AwayFromZero)
                        , M.InvoiceItem.Material.MaterialType
                        , M.Division
                        , M.Location);
                }
                new BXcel().ExporttoExcel(dt, "Sales Invoice");
            }
            if (rbDealerMaterial.Checked)
            {
                DataTable dtDM = new DataTable();
                dtDM.Columns.Add("Dealer Code");
                dtDM.Columns.Add("Dealer Name");
                dtDM.Columns.Add("PartNumber");
                dtDM.Columns.Add("Description");
                dtDM.Columns.Add("Qty");
                dtDM.Columns.Add("Net Amount");
                dtDM.Columns.Add("Gross Amount");
                //  dtDM.Columns.Add("Header Count");
                dtDM.Columns.Add("Count");

                foreach (PDMS_SalesInvoice M in SDMS_SalesInvoice)
                {
                    dtDM.Rows.Add(
                        M.Dealer.DealerCode
                        , M.Dealer.DealerName
                        , "'" + M.InvoiceItem.Material.MaterialCode
                        , M.InvoiceItem.Material.MaterialDescription
                        , decimal.Round(M.InvoiceItem.Qty, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.NetAmount, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.GrossAmount, 2, MidpointRounding.AwayFromZero)
                        // , M.HeaderCount
                        , M.InvoiceItem.ItemCount
                       );
                }
                new BXcel().ExporttoExcel(dtDM, "Sales Invoice Dealer And Material Wise");
            }
            if (rbDealerCustomerMaterial.Checked)
            {
                DataTable dtDCM = new DataTable();
                dtDCM.Columns.Add("Dealer");
                dtDCM.Columns.Add("Dealer Name");
                dtDCM.Columns.Add("Customer");
                dtDCM.Columns.Add("Customer Name");
                dtDCM.Columns.Add("PartNumber");
                dtDCM.Columns.Add("Description");
                dtDCM.Columns.Add("Qty");
                dtDCM.Columns.Add("Net Amount");
                dtDCM.Columns.Add("Gross Amount");
                dtDCM.Columns.Add("Count");

                foreach (PDMS_SalesInvoice M in SDMS_SalesInvoice)
                {
                    dtDCM.Rows.Add(
                        M.Dealer.DealerCode
                        , M.Dealer.DealerName
                        , M.Customer.CustomerCode
                        , M.Customer.CustomerName
                        , "'" + M.InvoiceItem.Material.MaterialCode
                        , M.InvoiceItem.Material.MaterialDescription
                        , decimal.Round(M.InvoiceItem.Qty, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.NetAmount, 2, MidpointRounding.AwayFromZero)
                        , decimal.Round(M.InvoiceItem.GrossAmount, 2, MidpointRounding.AwayFromZero)
                        //  , M.HeaderCount
                        , M.InvoiceItem.ItemCount
                       );
                }
                new BXcel().ExporttoExcel(dtDCM, "Sales Invoice Dealer-Customer-Material Wise");
            }

        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataSource = SDMS_SalesInvoice;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_SalesInvoice.Count;

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
            //    if (ddlSalesType.SelectedValue == "0")
            //    {
            //        ddlSOStatus.Items.Insert(0, new ListItem("Customer Order", "0"));
            //    }
            //    else
            //    {
            //        ddlSOStatus.Items.Insert(0, new ListItem("All", "0"));
            //        ddlSOStatus.Items.Insert(1, new ListItem("Machine Quotation", "1"));
            //        ddlSOStatus.Items.Insert(1, new ListItem("Machine Order", "1"));
            //    }

        }

        protected void gvDM_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDM.PageIndex = e.NewPageIndex;
            gvDM.DataSource = SDMS_SalesInvoice;
            gvDM.DataBind();
            lblRowCount.Text = (((gvDM.PageIndex) * gvDM.PageSize) + 1) + " - " + (((gvDM.PageIndex) * gvDM.PageSize) + gvDM.Rows.Count) + " of " + SDMS_SalesInvoice.Count;

        }

        protected void gvDCM_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDCM.PageIndex = e.NewPageIndex;
            gvDCM.DataSource = SDMS_SalesInvoice;
            gvDCM.DataBind();
            lblRowCount.Text = (((gvDCM.PageIndex) * gvDCM.PageSize) + 1) + " - " + (((gvDCM.PageIndex) * gvDCM.PageSize) + gvDCM.Rows.Count) + " of " + SDMS_SalesInvoice.Count;

        }
    }
}