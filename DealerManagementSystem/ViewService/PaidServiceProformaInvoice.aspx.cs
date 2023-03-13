using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class PaidServiceProformaInvoice : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_PaidServiceProformaInvoice; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_PaidServiceProformaInvoice.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_PaidServiceInvoice> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_PaidServiceProformaInvoice"] == null)
                {
                    Session["DMS_PaidServiceProformaInvoice"] = new List<PDMS_PaidServiceInvoice>();
                }
                return (List<PDMS_PaidServiceInvoice>)Session["DMS_PaidServiceProformaInvoice"];
            }
            set
            {
                Session["DMS_PaidServiceProformaInvoice"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » PaidService » Proforma Invoice');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    PDealer Dealer = new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0];
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID, Dealer.DID.ToString()));
                    //  ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
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
                fillWarrantyQuotation();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillWarrantyQuotation()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                long? ICTicketID = 0;
                if (!string.IsNullOrEmpty(txtICTicketNumber.Text.Trim()))
                {
                    List<PDMS_ICTicket> IC = new BDMS_ICTicket().GetICTicket(null, "", txtICTicketNumber.Text.Trim(), null, null, null, null);
                    foreach (PDMS_ICTicket t in IC)
                    {
                        if (t.ServiceStatus.ServiceStatusID != (short)DMS_ServiceStatus.Declined)
                        {
                            ICTicketID = t.ICTicketID;
                        }
                    }
                }
                else
                {
                    ICTicketID = null;
                }
                string InvoiceNumber = txtInvoiceNumber.Text.Trim();
                DateTime? InvoiceDateF = string.IsNullOrEmpty(txtInvoiceDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtInvoiceDateFrom.Text.Trim()); ;
                DateTime? InvoiceDateT = string.IsNullOrEmpty(txtInvoiceDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtInvoiceDateTo.Text.Trim()); ;
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                string CustomerCode = txtCustomerCode.Text.Trim();

                List<PDMS_PaidServiceInvoice> SOIs = new BDMS_Service().GetPaidServiceProformaInvoice(null, ICTicketID, InvoiceNumber, InvoiceDateF, InvoiceDateT, DealerID, CustomerCode);

                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs
                                 join D in PSession.User.Dealer on S.ICTicket.Dealer.DealerCode equals D.UserName
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

                SDMS_WarrantyClaimHeader = SOIs;

                gvClaimInvoice.PageIndex = 0;
                gvClaimInvoice.DataSource = SOIs;
                gvClaimInvoice.DataBind();
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
                    lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
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
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvClaimInvoice.PageIndex > 0)
            {
                gvClaimInvoice.PageIndex = gvClaimInvoice.PageIndex - 1;
                gvClaimInvoice.DataSource = SDMS_WarrantyClaimHeader;
                gvClaimInvoice.DataBind();
                lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvClaimInvoice.PageCount > gvClaimInvoice.PageIndex)
            {
                gvClaimInvoice.PageIndex = gvClaimInvoice.PageIndex + 1;
                gvClaimInvoice.DataSource = SDMS_WarrantyClaimHeader;
                gvClaimInvoice.DataBind();
                lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClaimInvoice.PageIndex = e.NewPageIndex;
            gvClaimInvoice.DataSource = SDMS_WarrantyClaimHeader;
            gvClaimInvoice.DataBind();
            lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

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

                    List<PDMS_PaidServiceInvoiceItem> Lines = new List<PDMS_PaidServiceInvoiceItem>();
                    Lines = SDMS_WarrantyClaimHeader.Find(s => s.PaidServiceInvoiceID == Convert.ToInt64(supplierPOID)).InvoiceItems;

                    gvClaimInvoiceItem.DataSource = Lines;

                    gvClaimInvoiceItem.DataBind();

                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }

        protected void ibPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                string PaidServiceInvoiceID = gvClaimInvoice.DataKeys[gvRow.RowIndex].Value.ToString();
                PAttachedFile UploadedFile = new BDMS_Service().ServiceProformaInvoicefile(Convert.ToInt64(PaidServiceInvoiceID));
                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName + "." + UploadedFile.FileType);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                new BXcel().PdfDowload();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnExportExcelForSAP_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("vendor_code");
            dt.Columns.Add("invoice_date");
            dt.Columns.Add("reference");
            dt.Columns.Add("material");
            dt.Columns.Add("Taxable_value");
            dt.Columns.Add("total");

            dt.Columns.Add("bp_code");
            dt.Columns.Add("bp_name");
            dt.Columns.Add("ic_ticket");
            dt.Columns.Add("machine_serial_no");
            dt.Columns.Add("hsn_code");
            dt.Columns.Add("Created by");
            dt.Columns.Add("model");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Remark");
            dt.Columns.Add("Annexure No");
            dt.Columns.Add("Period");
            foreach (PDMS_PaidServiceInvoice M in SDMS_WarrantyClaimHeader)
            {
                foreach (PDMS_PaidServiceInvoiceItem Item in M.InvoiceItems)
                {

                }
            }
            new BXcel().ExporttoExcel(dt, "Claim For SAP Upload");
        }
        protected void lbQuotationCancel_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Visible = true;
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                string PaidServiceInvoiceID = gvClaimInvoice.DataKeys[gvRow.RowIndex].Value.ToString();
                if (new BDMS_Service().CancelServiceQuotationOrProformaOrInvoice(Convert.ToInt64(PaidServiceInvoiceID), PSession.User.UserID, 2))
                {
                    lblMessage.Text = "Proforma Invoice is canceled successfully";
                    lblMessage.ForeColor = Color.Green;
                    fillWarrantyQuotation();
                }
                else
                {
                    lblMessage.Text = "Proforma Invoice is not canceled successfully";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}