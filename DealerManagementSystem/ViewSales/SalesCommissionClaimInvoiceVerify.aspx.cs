using Business;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSales
{
    public partial class SalesCommissionClaimInvoiceVerify : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        { 
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            } 
        }
        public List<PSalesCommissionClaimInvoice> Invoices
        {
            get
            {
                if (Session["SalesCommissionClaimInvoice"] == null)
                {
                    Session["SalesCommissionClaimInvoice"] = new List<PSalesCommissionClaimInvoice>();
                }
                return (List<PSalesCommissionClaimInvoice>)Session["SalesCommissionClaimInvoice"];
            }
            set
            {
                Session["SalesCommissionClaimInvoice"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Commision » Claim Invoice');</script>");
             
            if (!IsPostBack)
            {
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
                fillWarrantyInvoice();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillWarrantyInvoice()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);

                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                string InvoiceNumber = txtClaimNumber.Text.Trim();
                string InvoiceDateFrom = txtDateFrom.Text.Trim();
                string InvoiceDateTo = txtDateTo.Text.Trim();
                Boolean IsVerified = false;               

                int RowCount = 0;
                PApiResult Result = new BSalesCommissionClaim().GetSalesCommissionClaimInvoice(null, DealerID, InvoiceNumber, InvoiceDateFrom, InvoiceDateTo, IsVerified,null,null);
                Invoices = JsonConvert.DeserializeObject<List<PSalesCommissionClaimInvoice>>(JsonConvert.SerializeObject(Result.Data));
                RowCount = Result.RowCount;
                gvClaimInvoice.PageIndex = 0;
                gvClaimInvoice.DataSource = Invoices;
                gvClaimInvoice.DataBind();

                if (Invoices.Count == 0)
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
                    lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + Invoices.Count;
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaimInvoiceReport", "fillWarrantyInvoice", e1);
                throw e1;
            }
        }

        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithDisplayName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();
            ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvClaimInvoice.PageIndex > 0)
            {
                gvClaimInvoice.PageIndex = gvClaimInvoice.PageIndex - 1;
                gvClaimInvoice.DataSource = Invoices;
                gvClaimInvoice.DataBind();
                lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + Invoices.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvClaimInvoice.PageCount > gvClaimInvoice.PageIndex)
            {
                gvClaimInvoice.PageIndex = gvClaimInvoice.PageIndex + 1;
                gvClaimInvoice.DataSource = Invoices;
                gvClaimInvoice.DataBind();
                lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + Invoices.Count;
            }
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClaimInvoice.PageIndex = e.NewPageIndex;
            gvClaimInvoice.DataSource = Invoices;
            gvClaimInvoice.DataBind();
            lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + Invoices.Count;

        }
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string supplierPOID = Convert.ToString(gvClaimInvoice.DataKeys[e.Row.RowIndex].Value);
                    GridView gvClaimInvoiceItem = (GridView)e.Row.FindControl("gvClaimInvoiceItem");
                    List<PSalesCommissionClaimInvoice> Lines = new List<PSalesCommissionClaimInvoice>();
                    Lines = Invoices.Where(s => s.SalesCommissionClaimInvoiceID == Convert.ToInt64(supplierPOID)).ToList();

                    gvClaimInvoiceItem.DataSource = Lines;

                    gvClaimInvoiceItem.DataBind();
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
          
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Dealer_Code");
            dt.Columns.Add("Invoice_Date");
            dt.Columns.Add("Invoice_Number");
            dt.Columns.Add("Material");
            dt.Columns.Add("Taxable_Value");
            dt.Columns.Add("Total");

            dt.Columns.Add("BP_Code");
            dt.Columns.Add("BP_Name");
            dt.Columns.Add("IC_Ticket");
            dt.Columns.Add("Machine_Serial_No");
            dt.Columns.Add("HSN_Code");
            dt.Columns.Add("Created By");
            dt.Columns.Add("Model");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Remark");
            dt.Columns.Add("Annexure No");
            dt.Columns.Add("Period");
            dt.Columns.Add("TCS Amt.");
            dt.Columns.Add("IRN No.");
            dt.Columns.Add("IRN DATE");

            dt.Columns.Add("CGST %");
            dt.Columns.Add("CGST Value");
            dt.Columns.Add("SGST %");
            dt.Columns.Add("SGST Value");
            dt.Columns.Add("IGST %");
            dt.Columns.Add("IGST Value");

            dt.Columns.Add("SAP Doc");
            dt.Columns.Add("AE Inv. Accounted Date");
            dt.Columns.Add("Payment Voucher. No");
            dt.Columns.Add("Payment Date");
            dt.Columns.Add("Payment Value");
            dt.Columns.Add("TDS Value");
            try
            {
                foreach (PSalesCommissionClaimInvoice M in Invoices)
                {
                    PSalesCommissionClaimInvoiceItem Item = M.InvoiceItem;

                    dt.Rows.Add(
                         string.Format("80{0}", M.Dealer.DealerCode.Substring(2))
                        , ((DateTime)M.InvoiceDate).ToShortDateString()
                        , M.InvoiceNumber
                        , "'" + Item.Material
                        , Item.TaxableValue
                        , Item.TaxableValue + Item.CGSTValue + Item.SGSTValue + Item.IGSTValue
                        , M.Claim.Customer.CustomerCode
                        , M.Claim.Customer.CustomerName
                        , "" // AnnexureItem.ICTicketID
                        , "" // AnnexureItem.MachineSerialNumber
                        , Item.Material.HSN
                        , ""
                        , "" // AnnexureItem.Model
                        , "" // AnnexureItem.HMR
                        , "" // AnnexureItem.ICTicket == null ? "" : AnnexureItem.ICTicket.ServiceType.ServiceType
                        , M.InvoiceNumber
                        , "" // ((DateTime)M.PeriodFrom).ToShortDateString() + " - " + ((DateTime)M.PeriodTo).ToShortDateString()
                        , Convert.ToString(M.TCSValue)
                        , M.IRN
                        , M.IRNDate == null ? "" : ((DateTime)M.IRNDate).ToShortDateString()
                        , Item.CGST
                        , Item.CGSTValue
                        , Item.SGST
                        , Item.SGSTValue
                        , Item.IGST
                        , Item.IGSTValue
                        , M.SAPDoc
                        , M.SAPPostingDate == null ? "" : ((DateTime)M.SAPPostingDate).ToShortDateString()
                        , M.SAPClearingDocument
                        , M.SAPClearingDate == null ? "" : ((DateTime)M.SAPClearingDate).ToShortDateString()
                        , M.SAPInvoiceValue
                        , M.SAPInvoiceTDSValue);
                }
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaimInvoiceReport", "btnExportExcelForSAP_Click", ex);
            }
            new BXcel().ExporttoExcel(dt, "Claim Invoice Report");
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblSalesCommissionClaimInvoiceID = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblSalesCommissionClaimInvoiceID");
            TextBox txtRemarks = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtRemarks");
            string endPoint = "SalesCommission/VerifyInvoice?SalesCommissionClaimInvoiceID=" + lblSalesCommissionClaimInvoiceID.Text + "&Remarks=" + txtRemarks.Text.Trim();
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = Results.Message;
                return;
            }
            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = Results.Message;
            fillWarrantyInvoice();
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divClaimInvoiceView.Visible = false;
            btnBackToList.Visible = false;
            divList.Visible = true;
        }
        protected void btnViewClaimInvoice_Click(object sender, EventArgs e)
        {
            divClaimInvoiceView.Visible = true;
            btnBackToList.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblSalesCommissionClaimInvoiceID = (Label)gvRow.FindControl("lblSalesCommissionClaimInvoiceID");
            UC_ClaimInvoiceView.fillViewSalesCommissionClaimInvoice(Convert.ToInt64(lblSalesCommissionClaimInvoiceID.Text));
        }
    }
}