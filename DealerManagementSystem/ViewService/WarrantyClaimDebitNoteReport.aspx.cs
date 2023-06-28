using Business;
using Microsoft.Reporting.WebForms;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyClaimDebitNoteReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_WarrantyClaimDebitNoteReport; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_WarrantyClaimDebitNoteReport.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_WarrantyClaimDebitNote> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_WarrantyClaimDebitNoteReport"] == null)
                {
                    Session["DMS_WarrantyClaimDebitNoteReport"] = new List<PDMS_WarrantyClaimDebitNote>();
                }
                return (List<PDMS_WarrantyClaimDebitNote>)Session["DMS_WarrantyClaimDebitNoteReport"];
            }
            set
            {
                Session["DMS_WarrantyClaimDebitNoteReport"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Failed Material » Warranty Debit Note Report');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {


                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    PDealer Dealer = new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0];
                    ddlDealer.Items.Add(new ListItem(PSession.User.ExternalReferenceID, Dealer.DID.ToString()));
                    ddlDealer.Enabled = false;
                }
                else
                {
                    ddlDealer.Enabled = true;
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
                fillWarrantyInvoice();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
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
                string DebitNoteNumber = txtDebitNoteNumber.Text.Trim();
                DateTime? DebitNoteDateF = string.IsNullOrEmpty(txtDebitNoteDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDebitNoteDateF.Text.Trim());
                DateTime? DebitNoteDateT = string.IsNullOrEmpty(txtDebitNoteDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDebitNoteDateT.Text.Trim());
                string InvoiceNumber = txtInvoiceNumber.Text.Trim();
                int UserID = PSession.User.UserID;
                List<PDMS_WarrantyClaimDebitNote> SOIs = new BDMS_WarrantyClaimDebitNote().getWarrantyClaimDebitNoteReport(null, DealerID, DebitNoteNumber, DebitNoteDateF, DebitNoteDateT, InvoiceNumber, UserID);
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
                new FileLogger().LogMessage("WarrantyClaimDebitNoteReport", "fillWarrantyInvoice", e1);
                throw e1;
            }
        }

        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
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
                    Label lblInvoiceTypeID = (Label)e.Row.FindControl("lblInvoiceTypeID");

                    GridView gvClaimInvoiceItem = (GridView)e.Row.FindControl("gvClaimInvoiceItem");
                    List<PDMS_WarrantyClaimDebitNoteItem> Lines = new List<PDMS_WarrantyClaimDebitNoteItem>();
                    Lines = SDMS_WarrantyClaimHeader.Find(s => s.WarrantyClaimDebitNoteID == Convert.ToInt64(supplierPOID)).DebitNoteItems;

                    gvClaimInvoiceItem.DataSource = Lines;
                    gvClaimInvoiceItem.DataBind();
                    //for (int i = 0; i < gvClaimInvoiceItem.Rows.Count; i++)
                    //{
                    //    TextBox txtDebitQty = (TextBox)gvClaimInvoiceItem.Rows[i].FindControl("txtDebitQty");
                    //    txtDebitQty.Visible = false;
                    //}

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
                Label lblWarrantyClaimDebitNoteID = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblWarrantyClaimDebitNoteID");


                PDMS_WarrantyClaimDebitNote DebitNote = new BDMS_WarrantyClaimDebitNote().getWarrantyClaimDebitNoteReport(Convert.ToInt64(lblWarrantyClaimDebitNoteID.Text), null, null, null, null, null, PSession.User.UserID)[0];
                PDMS_WarrantyClaimInvoice OriginalInvoice = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(null, "", null, null, null, null, DebitNote.InvoiceNumber)[0];

                //if ((OriginalInvoice.Dealer.IsEInvoice) && (PDMS_EInvoice.EInvoiveDate <= DebitNote.DebitNoteDate))
                //{
                //    if (string.IsNullOrEmpty(DebitNote.IRN))
                //    {
                //        lblMessage.Text = "E Invoice Not generated.";
                //        lblMessage.ForeColor = Color.Red;
                //        lblMessage.Visible = true;
                //        return;
                //    }
                //}

                PAttachedFile UploadedFile = DebitFile(DebitNote, OriginalInvoice);

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
        private PAttachedFile DebitFile(PDMS_WarrantyClaimDebitNote DebitNote, PDMS_WarrantyClaimInvoice OriginalInvoice)
        {
            try
            {
                //  PDMS_WarrantyClaimInvoice ClaimInvoice = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(WarrantyClaimInvoiceID, "", null, null, null, 4, "")[0];


                PDMS_Customer Dealer = new BDMS_Customer().getCustomerAddressFromSAP(DebitNote.Dealer.DealerCode);
                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("Rate");
                CommissionDT.Columns.Add("Value", typeof(decimal));
                CommissionDT.Columns.Add("CGST");
                CommissionDT.Columns.Add("SGST");
                CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                //  decimal GrandTotal = 0;
                //    string StateCode = DealerOffice.StateCode;
                string GST_Header = "";
                decimal TCSTotalValue = 0;

                //PDMS_WarrantyClaimAnnexureHeader AnnexureH = new PDMS_WarrantyClaimAnnexureHeader();
                //AnnexureH = SDMS_WarrantyClaimHeader;
                int i = 0;
                foreach (PDMS_WarrantyClaimDebitNoteItem item in DebitNote.DebitNoteItems)
                {
                    item.ApprovedValue = item.ApprovedValue * -1;
                    item.CGSTValue = item.CGSTValue * -1;
                    item.SGSTValue = item.SGSTValue * -1;
                    item.IGSTValue = item.IGSTValue * -1;
                    item.Qty = item.Qty * -1;

                    TCSTotalValue = TCSTotalValue + item.ApprovedValue + item.CGSTValue + item.SGSTValue + item.IGSTValue;
                }


                foreach (PDMS_WarrantyClaimDebitNoteItem item in DebitNote.DebitNoteItems)
                {

                    i = i + 1;
                    item.Qty = item.Qty * -1;
                    item.ApprovedValue = item.ApprovedValue * -1;

                    if (item.SGST != 0)
                    {
                        GST_Header = "CGST & SGST";
                        item.CGSTValue = item.CGSTValue * -1;
                        item.SGSTValue = item.SGSTValue * -1;
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.ApprovedValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.ApprovedValue + item.CGSTValue + item.SGSTValue);
                    }
                    else
                    {
                        GST_Header = "IGST";
                        item.IGSTValue = item.IGSTValue * -1;
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.ApprovedValue, item.IGST, null, item.IGSTValue, null, item.ApprovedValue + item.IGSTValue);
                    }
                }

                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;

                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;

                ReportParameter[] P = null;
                //if (PDMS_EInvoice.EInvoiveDate <= DebitNote.DebitNoteDate)
                //{
                //    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getWarrantyClaimDebitNoteESigned(DebitNote.WarrantyClaimDebitNoteID);
                //    P = new ReportParameter[21];
                //    P[19] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, DebitNote.DebitNoteNumber), false);
                //    P[20] = new ReportParameter("IRN", "IRN : " + DebitNote.IRN, false);
                //    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_ClaimDebitNoteQRCode.rdlc");
                //}
                //else
                //{
                    P = new ReportParameter[19];
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_ClaimDebitNote.rdlc");
                //}

                //   ViewState["Month"] = ddlMonth.SelectedValue;
                P[0] = new ReportParameter("DealerCode", DebitNote.Dealer.DealerCode, false);
                P[1] = new ReportParameter("Annexure", DebitNote.InvoiceNumber, false);
                P[2] = new ReportParameter("DateOfClaim", DebitNote.DebitNoteDate.ToShortDateString(), false);
                P[3] = new ReportParameter("DealerName", DebitNote.Dealer.DealerName, false);
                P[4] = new ReportParameter("Address1", Dealer.Address1, false);
                P[5] = new ReportParameter("Address2", Dealer.Address2, false);
                P[6] = new ReportParameter("Contact", "Contact", false);
                P[7] = new ReportParameter("GSTIN", Dealer.GSTIN, false);
                P[8] = new ReportParameter("GST_Header", GST_Header, false);
                P[9] = new ReportParameter("GrandTotal", (DebitNote.GrandTotal).ToString(), false);
                P[10] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(DebitNote.GrandTotal)), false);
                P[11] = new ReportParameter("InvoiceNumber", DebitNote.DebitNoteNumber, false);
                P[12] = new ReportParameter("PeriodFrom", OriginalInvoice.InvoiceDate.ToShortDateString(), false);
                P[13] = new ReportParameter("PeriodTo", "", false);
                P[14] = new ReportParameter("PAN", Dealer.PAN, false);
                DateTime NewLogoDate = Convert.ToDateTime(ConfigurationManager.AppSettings["NewLogoDate"]);
                string NewLogo = "0";
                if (NewLogoDate <= DebitNote.DebitNoteDate)
                {
                    NewLogo = "1";
                }
                P[15] = new ReportParameter("NewLogo", NewLogo, false);
                P[16] = new ReportParameter("TCSValue", Convert.ToString(DebitNote.TCSValue), false);
                P[17] = new ReportParameter("TCSSubTotal", Convert.ToString((TCSTotalValue * -1) + DebitNote.TCSValue), false);
                P[18] = new ReportParameter("TCSTax", Convert.ToString(DebitNote.TCSTax), false);

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);
                report.SetParameters(P);
                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                PAttachedFile InvF = new PAttachedFile();

                InvF.FileType = mimeType;
                InvF.AttachedFile = mybytes;
                InvF.AttachedFileID = 0;
                InvF.FileName = "DebitNote " + DebitNote.DebitNoteNumber;
                return InvF;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnkButton = sender as LinkButton;
                //child gridview row clicked
                GridViewRow childRow = lnkButton.NamingContainer as GridViewRow;
                //child grid clicked
                GridView childGrid = childRow.NamingContainer as GridView;
                //parent gridviewrow containing the child grid
                GridViewRow parentRow = (childGrid.NamingContainer as GridViewRow);
                //Id is the datakeyname of my gridview
                // GridView gvClaimInvoiceItem = (GridView)gvClaimInvoice.Rows[parentRow.RowIndex].FindControl("gvClaimInvoiceItem");

                //  GridView gvClaimInvoiceItem = (GridView)gvClaimInvoice.Rows[parentRow.RowIndex].FindControl("gvClaimInvoiceItem");


                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                PDMS_WarrantyClaimDebitNoteItem UploadedFile = new BDMS_WarrantyClaimDebitNote().getWarrantyClaimDebitNoteItemAttachment(Convert.ToInt64(lnkButton.CommandName));

                Response.AddHeader("Content-type", UploadedFile.ContentType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedByte);
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
            dt.Columns.Add("Dealer_Code");
            dt.Columns.Add("Debit_Note_Date");
            dt.Columns.Add("Debit_Note_No");
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
            dt.Columns.Add("Reference No");
            dt.Columns.Add("Period");

            dt.Columns.Add("TCS Amt.");
            dt.Columns.Add("IRN No.");
            dt.Columns.Add("IRN DATE");

            dt.Columns.Add("SAP Doc");
            dt.Columns.Add("AE Inv. Accounted Date");
            dt.Columns.Add("Payment Voucher. No");
            dt.Columns.Add("Payment Date");
            dt.Columns.Add("Payment Value");
            dt.Columns.Add("TDS Value");

            foreach (PDMS_WarrantyClaimDebitNote M in SDMS_WarrantyClaimHeader)
            {
                foreach (PDMS_WarrantyClaimDebitNoteItem Item in M.DebitNoteItems)
                {
                    //  PDMS_WarrantyInvoiceHeader SOIs = new BDMS_WarrantyClaim().GetWarrantyClaimReport(txtICServiceTicket.Text.Trim(), ICTicketDateF, ICTicketDateT, txtClaimNumber.Text.Trim(), ClaimDateF, ClaimDateT, DealerCode, StatusID, ApprovedDateF, ApprovedDateT, txtTSIRNumber.Text.Trim(), false, PSession.User.UserID)[0];
                    PDMS_WarrantyClaimAnnexureItem AnnexureItem = new PDMS_WarrantyClaimAnnexureItem();
                    List<PDMS_WarrantyClaimInvoice> SOIs = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(null, "", null, null, null, null, M.InvoiceNumber);

                    if (Item.WarrantyClaimAnnexureItemID != 0)
                    {
                        AnnexureItem = new BDMS_WarrantyClaimAnnexure().GetWarrantyClaimAnnexureReport(null, "", null, null, null, null, "", Item.WarrantyClaimAnnexureItemID, true)[0].AnnexureItems[0];
                    }

                    dt.Rows.Add(
                         string.Format("80{0}", M.Dealer.DealerCode.Substring(2))
                        , ((DateTime)M.DebitNoteDate).ToShortDateString()
                        , M.DebitNoteNumber
                        , "'" + Item.Material
                        , Item.TaxableValue
                        , Item.TaxableValue + Item.CGSTValue + Item.SGSTValue + Item.IGSTValue
                        , AnnexureItem.CustomerCode
                        , AnnexureItem.CustomerName
                        , AnnexureItem.ICTicketID
                        , AnnexureItem.MachineSerialNumber
                        , Item.HSNCode
                        , ""
                        , AnnexureItem.Model
                        , AnnexureItem.HMR
                        , ""
                        , M.InvoiceNumber
                        , ((DateTime)M.PeriodFrom).ToShortDateString() + " - " + ((DateTime)M.PeriodTo).ToShortDateString()
                        , M.TCSValue
                        , M.IRN
                        , M.IRNDate == null ? "" : ((DateTime)M.IRNDate).ToShortDateString()
                        , M.SAPDoc
                        , M.SAPPostingDate == null ? "" : ((DateTime)M.SAPPostingDate).ToShortDateString()
                          , M.SAPClearingDocument
                           , M.SAPClearingDate == null ? "" : ((DateTime)M.SAPClearingDate).ToShortDateString()
                            , M.SAPInvoiceValue
                            , M.SAPInvoiceTDSValue
                    );
                }
            }
            new BXcel().ExporttoExcel(dt, "Claim For SAP Upload");
        }
    }
}