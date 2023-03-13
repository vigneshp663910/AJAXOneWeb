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
    public partial class SalesCommissionClaimInvoice : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSales_SalesCommissionClaimInvoice; } }
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
            Session["previousUrl"] = "SalesCommissionClaimInvoice.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
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

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
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
                PageIndex = 1;
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
                bool? IsVerified = null;
                if (ddlIsVerified.SelectedValue == "1") { IsVerified = true; } else if (ddlIsVerified.SelectedValue == "2") { IsVerified = false; }

                int RowCount = 0;
                PApiResult Result = new BSalesCommissionClaim().GetSalesCommissionClaimInvoice(null, DealerID, InvoiceNumber, InvoiceDateFrom, InvoiceDateTo, IsVerified, PageIndex, gvClaimInvoice.PageSize);
                Invoices = JsonConvert.DeserializeObject<List<PSalesCommissionClaimInvoice>>(JsonConvert.SerializeObject(Result.Data));
                RowCount = Result.RowCount;
                //gvClaimInvoice.PageIndex = 0;
                //gvClaimInvoice.DataSource = Invoices;
                //gvClaimInvoice.DataBind();

                //if (Invoices.Count == 0)
                //{
                //    lblRowCount.Visible = false;
                //    ibtnArrowLeft.Visible = false;
                //    ibtnArrowRight.Visible = false;
                //}
                //else
                //{
                //    lblRowCount.Visible = true;
                //    ibtnArrowLeft.Visible = true;
                //    ibtnArrowRight.Visible = true;
                //    lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + Invoices.Count;
                //}
                if (RowCount == 0)
                {
                    gvClaimInvoice.DataSource = null;
                    gvClaimInvoice.DataBind();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    gvClaimInvoice.DataSource = Invoices;
                    gvClaimInvoice.DataBind();
                    PageCount = (RowCount + gvClaimInvoice.PageSize - 1) / gvClaimInvoice.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvClaimInvoice.PageSize) + 1) + " - " + (((PageIndex - 1) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + RowCount;
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
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillWarrantyInvoice();
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillWarrantyInvoice();
            }
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClaimInvoice.PageIndex = e.NewPageIndex;
            fillWarrantyInvoice();
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

        protected void ibPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label lblSalesCommissionClaimInvoiceID = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblSalesCommissionClaimInvoiceID");

                PSalesCommissionClaimInvoice SOIs = new BSalesCommissionClaim().GetSalesCommissionClaimInvoiceByID(Convert.ToInt64(lblSalesCommissionClaimInvoiceID.Text));


                //if (string.IsNullOrEmpty(SOIs.InvoiceItem.Material.HSN))
                //{
                //    lblMessage.Text = "HSN Code Missed. Please contact admin";
                //    lblMessage.ForeColor = Color.Red;
                //    lblMessage.Visible = true;
                //    return;
                //}
                if (SOIs.InvoiceItem.CGSTValue + SOIs.InvoiceItem.SGSTValue + SOIs.InvoiceItem.IGSTValue == 0)
                {
                    lblMessage.Text = "GST Value Missed. Please contact admin";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                    return;
                }


                if ((SOIs.Dealer.IsEInvoice) && (SOIs.Dealer.EInvoiceDate <= SOIs.InvoiceDate))
                {
                    if (string.IsNullOrEmpty(SOIs.IRN))
                    {
                        PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getSalesCommissionClaimInvoiceESigned(SOIs.SalesCommissionClaimInvoiceID);
                        if (!string.IsNullOrEmpty(EInvoiceSigned.Comments))
                        {
                            lblMessage.Text = EInvoiceSigned.Comments;
                        }
                        else
                        {
                            lblMessage.Text = "E Invoice Not generated.";
                        }
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;
                        return;
                    }
                }


                PAttachedFile UploadedFile = new BSalesCommissionClaim().GetSalesCommissionClaimInvoiceFile(Convert.ToInt64(lblSalesCommissionClaimInvoiceID.Text));
                if (UploadedFile == null)
                {
                    new BSalesCommissionClaim().InsertSalesCommissionClaimInvoiceFile(Convert.ToInt64(lblSalesCommissionClaimInvoiceID.Text), SalesCommissionClaimInvoice1(Convert.ToInt32(lblSalesCommissionClaimInvoiceID.Text)));
                    UploadedFile = new BSalesCommissionClaim().GetSalesCommissionClaimInvoiceFile(Convert.ToInt64(lblSalesCommissionClaimInvoiceID.Text));
                }

                //Response.Buffer = true;
                //Response.Clear();
                //Response.ContentType = UploadedFile.FileType;
                //Response.AddHeader("content-disposition", "attachment; filename=" + UploadedFile.FileName+".pdf");
                //Response.BinaryWrite(UploadedFile.AttachedFile); // create the file
                //Response.Flush(); // send it to the client to download
                //var uploadPath = Server.MapPath("~/Backup");
                //var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(UploadedFile.FileName + ".pdf"));
                //File.WriteAllBytes(tempfilenameandlocation, UploadedFile.AttachedFile);
                Response.Redirect("../PDF.aspx?FileName=" + UploadedFile.FileName + ".pdf" + "&Title=Sales » Sales Commision » Claim Invoice", false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        public PAttachedFile SalesCommissionClaimInvoice1(long SalesCommissionClaimInvoiceID)
        {
            try
            {
                PSalesCommissionClaimInvoice SalesCommissionClaimInvoice = new BSalesCommissionClaim().GetSalesCommissionClaimInvoiceByID(SalesCommissionClaimInvoiceID);

                PDMS_Customer Dealer = new SCustomer().getCustomerAddress(SalesCommissionClaimInvoice.Dealer.DealerCode);
                string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

                PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
                string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : "," + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : "," + Ajax.Address3)).Trim(',', ' ');
                string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : "," + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');

                //PSalesCommissionClaim salesCommissionClaim = GetSalesCommissionClaimByID(SalesCommissionClaimInvoice.SalesCommissionClaimID);

                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                Random r = new Random();
                string FileName = "Inv - " + SalesCommissionClaimInvoice.InvoiceNumber + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;
                ReportParameter[] P = null;

                if ((SalesCommissionClaimInvoice.Dealer.IsEInvoice) && (SalesCommissionClaimInvoice.Dealer.EInvoiceDate <= SalesCommissionClaimInvoice.InvoiceDate))
                {
                    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getSalesCommissionClaimInvoiceESigned(SalesCommissionClaimInvoiceID);
                    P = new ReportParameter[42];
                    P[40] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, SalesCommissionClaimInvoice.InvoiceNumber), false);
                    P[41] = new ReportParameter("IRNNo", "IRN : " + SalesCommissionClaimInvoice.IRN, false);
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/SalesCommisionTaxQuotationQRCode.rdlc");
                }
                else
                {
                    P = new ReportParameter[40];
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/SalesCommisionTaxQuotation.rdlc");
                }

                string StateCode = Dealer.State.StateCode;
                decimal GrandTotal = 0;
                PSalesCommissionClaimInvoiceItem item = SalesCommissionClaimInvoice.InvoiceItem;
                if (item.SGST != 0)
                {
                    P[23] = new ReportParameter("Amount", (item.Qty * item.Rate).ToString(), false);
                    P[24] = new ReportParameter("SGSTValue", item.SGSTValue.ToString(), false);
                    P[25] = new ReportParameter("CGSTValue", item.CGSTValue.ToString(), false);
                    P[38] = new ReportParameter("SGST", "SGST @ " + item.SGST, false);
                    P[39] = new ReportParameter("CGST", "CGST @ " + item.CGST, false);
                    //CommissionDT.Rows.Add(1, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, (item.Qty * item.Rate), item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, (item.Qty * item.Rate) + item.CGSTValue + item.SGSTValue);
                    GrandTotal = (item.Qty * item.Rate) + item.CGSTValue + item.SGSTValue;
                    P[26] = new ReportParameter("GrandTotal", GrandTotal.ToString(), false);
                    P[27] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                }
                else
                {
                    P[23] = new ReportParameter("Amount", (item.Qty * item.Rate).ToString(), false);
                    P[24] = new ReportParameter("SGSTValue", "", false);
                    P[25] = new ReportParameter("CGSTValue", item.IGSTValue.ToString(), false);
                    P[38] = new ReportParameter("SGST", "", false);
                    P[39] = new ReportParameter("CGST", "IGST @ " + item.IGST.ToString(), false);
                    //CommissionDT.Rows.Add(1, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, (item.Qty * item.Rate), item.IGST, null, item.IGSTValue, null, (item.Qty * item.Rate) + item.IGSTValue);
                    GrandTotal = (item.Qty * item.Rate) + item.IGSTValue;
                    P[26] = new ReportParameter("GrandTotal", GrandTotal.ToString(), false);
                    P[27] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                }

                // P[0] = new ReportParameter("CompanyName", Dealer.CustomerName + " " + Dealer.CustomerName2, false);
                P[0] = new ReportParameter("CompanyName", Dealer.CustomerName, false);
                P[1] = new ReportParameter("CompanyAddress1", DealerAddress1, false);
                P[2] = new ReportParameter("CompanyAddress2", DealerAddress2, false);
                P[3] = new ReportParameter("QuotationType", "TAX INVOICE", false);
                P[4] = new ReportParameter("InvoiceNo", SalesCommissionClaimInvoice.InvoiceNumber, false);
                P[5] = new ReportParameter("InvoiceDate", SalesCommissionClaimInvoice.InvoiceDate.ToString(), false);
                P[6] = new ReportParameter("IncomeTaxPAN", Dealer.PAN, false);
                P[7] = new ReportParameter("ITGST", Dealer.GSTIN, false);
                P[8] = new ReportParameter("ITGSTStateCode", Dealer.State.StateCode, false);
                P[9] = new ReportParameter("ITGSTState", Dealer.State.State, false);
                P[10] = new ReportParameter("CustomerStateCode", Ajax.State.StateCode, false);
                P[11] = new ReportParameter("AFPAN", Ajax.PAN, false);
                P[12] = new ReportParameter("AFGSTN", Ajax.GSTIN, false);
                P[13] = new ReportParameter("Nameofservice", "Sales Commission", false);
                P[14] = new ReportParameter("ServiceCategory", "Services provided for a fee/commission or contract basis on retail trade", false);
                P[15] = new ReportParameter("HSNCode", "996211", false);
                P[16] = new ReportParameter("Placeofsupply", "Karnataka", false);
                P[17] = new ReportParameter("Model", (SalesCommissionClaimInvoice.Quotation.Model.ModelCode == null) ? "" : SalesCommissionClaimInvoice.Quotation.Model.ModelCode + " - " + SalesCommissionClaimInvoice.Quotation.Model.Division.DivisionDescription, false);
                P[18] = new ReportParameter("SerialNo", (item.Material.MaterialSerialNumber == null) ? "" : item.Material.MaterialSerialNumber, false);
                P[19] = new ReportParameter("MInvoiceNo", SalesCommissionClaimInvoice.Quotation.SalesInvoiceNumber, false);
                P[20] = new ReportParameter("MInvoiceDate", SalesCommissionClaimInvoice.Quotation.SalesInvoiceDate.ToString(), false);
                // P[21] = new ReportParameter("CustomerName", (SalesCommissionClaimInvoice.Quotation == null) ? "" : SalesCommissionClaimInvoice.Quotation.Lead.Customer.CustomerName + " " + SalesCommissionClaimInvoice.Quotation.Lead.Customer.CustomerName2, false);
                P[21] = new ReportParameter("CustomerName", (SalesCommissionClaimInvoice.Quotation == null) ? "" : SalesCommissionClaimInvoice.Quotation.Lead.Customer.CustomerName, false);
                P[22] = new ReportParameter("CustomerCode", (SalesCommissionClaimInvoice.Quotation == null) ? "" : SalesCommissionClaimInvoice.Quotation.Lead.Customer.CustomerCode, false);
                P[28] = new ReportParameter("ClaimNo", SalesCommissionClaimInvoice.Claim.ClaimNumber, false);
                P[29] = new ReportParameter("ClaimDate", SalesCommissionClaimInvoice.Claim.ClaimDate.ToString(), false);
                P[30] = new ReportParameter("AccDocNo", SalesCommissionClaimInvoice.Quotation.AccountNumber, false);
                P[31] = new ReportParameter("AccYear", (SalesCommissionClaimInvoice.Quotation.AccountDate == null) ? "" : Convert.ToDateTime(SalesCommissionClaimInvoice.Quotation.AccountDate).ToString("yyyy"), false);
                P[32] = new ReportParameter("AjaxName", Ajax.CustomerFullName, false);
                P[33] = new ReportParameter("AjaxAddress1", AjaxCustomerAddress1, false);
                P[34] = new ReportParameter("AjaxAddress2", AjaxCustomerAddress2, false);
                P[35] = new ReportParameter("AjaxCINandGST", "CIN:" + Ajax.PAN + ",GST:" + Ajax.GSTIN, false);
                P[36] = new ReportParameter("AjaxPAN", "PAN:" + Ajax.PAN, false);
                P[37] = new ReportParameter("AjaxTelephoneandEmail", "T:" + Ajax.Mobile + ",Email:" + Ajax.Email, false);

                report.SetParameters(P);

                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                PAttachedFile InvF = new PAttachedFile();

                var uploadPath = Server.MapPath("~/Backup");
                var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                File.WriteAllBytes(tempfilenameandlocation, mybytes);

                InvF.FileType = mimeType;
                InvF.AttachedFile = mybytes;
                InvF.AttachedFileID = 0;
                InvF.FileName = FileName;
                return InvF;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
            return null;
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

        protected void btnExportExcelForSAP_Click(object sender, EventArgs e)
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

                    dt.Rows.Add(
                         string.Format("80{0}", M.Dealer.DealerCode.Substring(2))
                        , ((DateTime)M.InvoiceDate).ToShortDateString()
                        , M.InvoiceNumber
                        , "'" + M.InvoiceItem.Material.MaterialCode
                        , M.InvoiceItem.TaxableValue
                        , M.InvoiceItem.TaxableValue + M.InvoiceItem.CGSTValue + M.InvoiceItem.SGSTValue + M.InvoiceItem.IGSTValue
                        , M.Quotation.Lead.Customer.CustomerCode
                        , M.Quotation.Lead.Customer.CustomerName
                        , ""
                        , M.Quotation.EquipmentSerialNo
                        , M.InvoiceItem.Material.HSN
                        , ""
                        , M.InvoiceItem.Material.Model
                        , ""
                        , ""
                        , M.Claim.ClaimNumber
                        , ""
                        , Convert.ToString(M.TCSValue)
                        , M.IRN
                        , M.IRNDate == null ? "" : ((DateTime)M.IRNDate).ToShortDateString()
                        , M.InvoiceItem.CGST
                        , M.InvoiceItem.CGSTValue
                        , M.InvoiceItem.SGST
                        , M.InvoiceItem.SGSTValue
                        , M.InvoiceItem.IGST
                        , M.InvoiceItem.IGSTValue
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