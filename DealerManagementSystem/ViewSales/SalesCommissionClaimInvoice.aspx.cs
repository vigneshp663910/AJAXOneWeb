using Business;
using Microsoft.Reporting.WebForms;
using Properties;
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
    public partial class SalesCommissionClaimInvoice : System.Web.UI.Page
    {
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

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
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
                string InvoiceNumber = txtClaimNumber.Text.Trim();
                string InvoiceDateFrom = txtDateFrom.Text.Trim();
                string InvoiceDateTo = txtDateTo.Text.Trim();

                Invoices = new BSalesCommissionClaim().GetSalesCommissionClaimInvoice(null,DealerID, InvoiceNumber, InvoiceDateFrom, InvoiceDateTo);
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
            }
        }

        protected void ibPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label lblSalesCommissionClaimInvoiceID = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblSalesCommissionClaimInvoiceID");

                PSalesCommissionClaimInvoice SOIs = new BSalesCommissionClaim().GetSalesCommissionClaimInvoice(Convert.ToInt64(lblSalesCommissionClaimInvoiceID.Text), null, null, null, null)[0];


                if (string.IsNullOrEmpty(SOIs.InvoiceItem.Material.HSN))
                {
                    lblMessage.Text = "HSN Code Missed. Please contact admin";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                    return;
                }
                else if (SOIs.InvoiceItem.CGSTValue + SOIs.InvoiceItem.SGSTValue + SOIs.InvoiceItem.IGSTValue == 0)
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
                    UploadedFile = new BSalesCommissionClaim().GetSalesCommissionClaimInvoiceFile(Convert.ToInt64(lblSalesCommissionClaimInvoiceID.Text));
                }

                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName + "." + UploadedFile.FileType);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();




                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName + "." + UploadedFile.FileType);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();

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

                CommissionDT.Rows.Add(1, "Material", "Desc", 9876, 1, 10, 100, 18, 18, 18, 180, 100 + 18 + 18);


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

                P = new ReportParameter[21];
                P[19] = new ReportParameter("QRCodeImg", "");
                P[20] = new ReportParameter("IRN", "IRN : ", false);
                report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_SalesClaimInvoice.rdlc");


                //   ViewState["Month"] = ddlMonth.SelectedValue;
                P[0] = new ReportParameter("DealerCode", "", false);
                P[1] = new ReportParameter("Annexure", "", false);
                P[2] = new ReportParameter("DateOfClaim", "", false);
                P[3] = new ReportParameter("DealerName", "", false);
                P[4] = new ReportParameter("Address1", "", false);
                P[5] = new ReportParameter("Address2", "", false);
                P[6] = new ReportParameter("Contact", "Contact", false);
                P[7] = new ReportParameter("GSTIN", "", false);
                P[8] = new ReportParameter("GST_Header", "", false);
                P[9] = new ReportParameter("GrandTotal", "", false);
                P[10] = new ReportParameter("AmountInWord", "", false);
                P[11] = new ReportParameter("InvoiceNumber", "", false);
                P[12] = new ReportParameter("PeriodFrom", "", false);
                P[13] = new ReportParameter("PeriodTo", "", false);
                P[14] = new ReportParameter("PAN", "", false);
                //DateTime NewLogoDate = Convert.ToDateTime(ConfigurationManager.AppSettings["NewLogoDate"]);
                //string NewLogo = "0";
                //if (NewLogoDate <= ClaimInvoice.InvoiceDate)
                //{
                //    NewLogo = "1";
                //}
                P[15] = new ReportParameter("NewLogo", "", false);
                P[16] = new ReportParameter("TCSValue", "", false);
                P[17] = new ReportParameter("TCSSubTotal", "", false);
                P[18] = new ReportParameter("TCSTax", "", false);




                ReportDataSource rds = new ReportDataSource();
                rds.Name = "SalesCommisionInvoice";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);
                report.SetParameters(P);
                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                PAttachedFile InvF = new PAttachedFile();

                InvF.FileType = mimeType;
                InvF.AttachedFile = mybytes;
                InvF.AttachedFileID = 0;

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(mybytes); // create the file
                Response.Flush(); // send it to the client to download
                //var uploadPath = Server.MapPath("~/Backup");
                //var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                //File.WriteAllBytes(tempfilenameandlocation, mybytes);
                //Response.Redirect("PDF.aspx?FileName=" + FileName, false);
            }
            catch (Exception ex)
            {
            }
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
                //foreach (PDMS_WarrantyClaimInvoice M in Invoices)
                //{
                //    int i = 0;
                //    List<PDMS_WarrantyClaimAnnexureItem> AnnexureItemS = new List<PDMS_WarrantyClaimAnnexureItem>();
                //    if (!string.IsNullOrEmpty(M.AnnexureNumber))
                //    {
                //        AnnexureItemS = new BDMS_WarrantyClaimAnnexure().GetWarrantyClaimAnnexureByID(null, null, M.AnnexureNumber)[0].AnnexureItems;
                //    }

                //    foreach (PDMS_WarrantyClaimInvoiceItem Item in M.InvoiceItems)
                //    {
                //        i = 1 + i;
                //        //  PDMS_WarrantyInvoiceHeader SOIs = new BDMS_WarrantyClaim().GetWarrantyClaimReport(txtICServiceTicket.Text.Trim(), ICTicketDateF, ICTicketDateT, txtClaimNumber.Text.Trim(), ClaimDateF, ClaimDateT, DealerCode, StatusID, ApprovedDateF, ApprovedDateT, txtTSIRNumber.Text.Trim(), false, PSession.User.UserID)[0];
                //        PDMS_WarrantyClaimAnnexureItem AnnexureItem = new PDMS_WarrantyClaimAnnexureItem();
                //        if ((Item.WarrantyClaimAnnexureItemID != 0) && (AnnexureItemS.Count > 0))
                //        {
                //            //  AnnexureItem = new BDMS_WarrantyClaimAnnexure().GetWarrantyClaimAnnexureByID(null, Item.WarrantyClaimAnnexureItemID)[0].AnnexureItems[0];
                //            //   AnnexureItem = (PDMS_WarrantyClaimAnnexureItem)AnnexureItemS.Where(m => m.WarrantyClaimAnnexureItemID == Item.WarrantyClaimAnnexureItemID);
                //            //  var SOIs1 = (from S in AnnexureItemS where (S.WarrantyClaimAnnexureItemID == Item.WarrantyClaimAnnexureItemID) select new { S }).ToList();
                //            //       AnnexureItem = (PDMS_WarrantyClaimAnnexureItem) SOIs1;

                //            var M1 = (from m in AnnexureItemS where m.WarrantyClaimAnnexureItemID == Item.WarrantyClaimAnnexureItemID select m);
                //            if (M1.Count() == 1)
                //            {
                //                AnnexureItem.CustomerCode = M1.ToList()[0].CustomerCode;
                //                AnnexureItem.CustomerName = M1.ToList()[0].CustomerName;
                //                AnnexureItem.ICTicketID = M1.ToList()[0].ICTicketID;
                //                AnnexureItem.MachineSerialNumber = M1.ToList()[0].MachineSerialNumber;
                //                AnnexureItem.Model = M1.ToList()[0].Model;
                //                AnnexureItem.HMR = M1.ToList()[0].HMR;
                //                AnnexureItem.ICTicket = M1.ToList()[0].ICTicket;
                //            }
                //        }

                //        dt.Rows.Add(
                //             string.Format("80{0}", M.Dealer.DealerCode.Substring(2))
                //            , ((DateTime)M.InvoiceDate).ToShortDateString()
                //            , M.InvoiceNumber
                //            , "'" + Item.Material
                //            , Item.TaxableValue
                //            , Item.TaxableValue + Item.CGSTValue + Item.SGSTValue + Item.IGSTValue
                //            , AnnexureItem.CustomerCode
                //            , AnnexureItem.CustomerName
                //            , AnnexureItem.ICTicketID
                //            , AnnexureItem.MachineSerialNumber
                //            , Item.HSNCode
                //            , ""
                //            , AnnexureItem.Model
                //            , AnnexureItem.HMR
                //            , AnnexureItem.ICTicket == null ? "" : AnnexureItem.ICTicket.ServiceType.ServiceType
                //            , M.AnnexureNumber
                //            , ((DateTime)M.PeriodFrom).ToShortDateString() + " - " + ((DateTime)M.PeriodTo).ToShortDateString()
                //            , i == 1 ? Convert.ToString(M.TCSValue) : ""
                //            , M.IRN
                //            , M.IRNDate == null ? "" : ((DateTime)M.IRNDate).ToShortDateString()
                //            , Item.CGST
                //            , Item.CGSTValue
                //            , Item.SGST
                //            , Item.SGSTValue
                //            , Item.IGST
                //            , Item.IGSTValue
                //            , M.SAPDoc
                //            , M.SAPPostingDate == null ? "" : ((DateTime)M.SAPPostingDate).ToShortDateString()
                //            , M.SAPClearingDocument
                //            , M.SAPClearingDate == null ? "" : ((DateTime)M.SAPClearingDate).ToShortDateString()
                //            , M.SAPInvoiceValue
                //            , M.SAPInvoiceTDSValue);
                //    };
                //}
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaimInvoiceReport", "btnExportExcelForSAP_Click", ex);
            }
            new BXcel().ExporttoExcel(dt, "Claim Invoice Report");
        }
    }
}