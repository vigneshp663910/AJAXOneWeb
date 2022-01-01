using Business;
using Microsoft.Reporting.WebForms;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyFailureMaterialDCReport : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_WarrantyFailureMaterialDCReport.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }

        public List<PDMS_WarrantyFailureMaterial> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_WarrantyClaim"] == null)
                {
                    Session["DMS_WarrantyClaim"] = new List<PDMS_WarrantyFailureMaterial>();
                }
                return (List<PDMS_WarrantyFailureMaterial>)Session["DMS_WarrantyClaim"];
            }
            set
            {
                Session["DMS_WarrantyClaim"] = value;
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
                //new BDMS_WarrantyClaim().insertWarrantyClaim();
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    PDealer Dealer = new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0];
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID, Dealer.DID.ToString()));
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
                fillClaim();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillClaim()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? DealerID = null;

                if (ddlDealerCode.SelectedValue != "0")
                {
                    DealerID = Convert.ToInt32(ddlDealerCode.SelectedValue);
                }
                //else
                //{
                //    lblMessage.Text = "Please select dealer";
                //    lblMessage.Visible = true;
                //    lblMessage.ForeColor = Color.Red;
                //    return;
                //}

                String DCNumber = txtDeliveryChallanNumber.Text.Trim();
                DateTime? DCDateF = string.IsNullOrEmpty(txtDCDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDCDateF.Text.Trim());
                DateTime? DCDateT = string.IsNullOrEmpty(txtDCDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDCDateT.Text.Trim());

                String ICTicketNumber = txtICTicketNumber.Text.Trim();
                DateTime? ICTicketDateF = string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateFrom.Text.Trim());
                DateTime? ICTicketDateT = string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateTo.Text.Trim());

                List<PDMS_WarrantyFailureMaterial> SOIs = new BDMS_WarrantyFailureMaterial().GetWarrantyFailedMaterialDeliveryChallan(null, DCNumber, DCDateF, DCDateT, ICTicketNumber, ICTicketDateF, ICTicketDateT, DealerID);

                //if (ddlDealerCode.SelectedValue == "0")
                //{
                //    var SOIs1 = (from S in SOIs
                //                 join D in PSession.User.Dealer on S.FailureMaterialItem.Invoice.DealerCode equals D.UserName
                //                 select new
                //                 {
                //                     S
                //                 }).ToList();
                //    SOIs.Clear();
                //    foreach (var w in SOIs1)
                //    {
                //        SOIs.Add(w.S);
                //    }
                //}


                SDMS_WarrantyClaimHeader = SOIs;
                gvDCTemplate.PageIndex = 0;
                gvDCTemplate.DataSource = SOIs;

                gvDCTemplate.DataBind();

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
                    lblRowCount.Text = (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + 1) + " - " + (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + gvDCTemplate.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
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
            if (gvDCTemplate.PageIndex > 0)
            {
                gvDCTemplate.PageIndex = gvDCTemplate.PageIndex - 1;
                gvDCTemplate.DataSource = SDMS_WarrantyClaimHeader;
                gvDCTemplate.DataBind();
                lblRowCount.Text = (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + 1) + " - " + (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + gvDCTemplate.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDCTemplate.PageCount > gvDCTemplate.PageIndex)
            {
                gvDCTemplate.PageIndex = gvDCTemplate.PageIndex + 1;
                gvDCTemplate.DataSource = SDMS_WarrantyClaimHeader;
                gvDCTemplate.DataBind();
                lblRowCount.Text = (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + 1) + " - " + (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + gvDCTemplate.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Order Type");
            dt.Columns.Add("Sales Org");
            dt.Columns.Add("Dis Channel");
            dt.Columns.Add("Division");
            dt.Columns.Add("Sold To Party");
            dt.Columns.Add("Ship To Party");
            dt.Columns.Add("Pricing Date");
            dt.Columns.Add("Payment Term");
            dt.Columns.Add("Inco Terms");
            dt.Columns.Add("Order Reason");
            dt.Columns.Add("Material");
            dt.Columns.Add("Status");
            dt.Columns.Add("Apr.1 By");
            dt.Columns.Add("Apr.1 On");
            dt.Columns.Add("Apr.2 By");
            dt.Columns.Add("Apr.2 On");

            dt.Columns.Add("Invoice Number");
            dt.Columns.Add("Invoice Date");
            dt.Columns.Add("TSIR Number");
            dt.Columns.Add("Model");
            dt.Columns.Add("SAC / HSN Code");
            dt.Columns.Add("Material");
            dt.Columns.Add("Material Desc");
            dt.Columns.Add("Category");
            dt.Columns.Add("Qty");
            dt.Columns.Add("UOM");
            dt.Columns.Add("Amount");
            dt.Columns.Add("BaseTax");
            // dt.Columns.Add("Material Status");
            dt.Columns.Add("Failure Mat Remarks 1");
            dt.Columns.Add("Apr.1 Amt");
            dt.Columns.Add("Apr.1 Remarks");
            dt.Columns.Add("Failure Mat Remarks 2");
            dt.Columns.Add("Apr.2 Amt");
            dt.Columns.Add("Apr.2 Remarks");

            //foreach (PDMS_WarrantyInvoiceHeader M in SDMS_WarrantyClaimHeader)
            //{
            //    foreach (PDMS_WarrantyInvoiceItem Item in M.InvoiceItems)
            //    {
            //        dt.Rows.Add(
            //            // M.ClaimID
            //            // , M.ClaimDate == null ? "" : ((DateTime)M.ClaimDate).ToShortDateString()
            //              M.ICTicketID
            //            , M.ICTicketDate == null ? "" : ((DateTime)M.ICTicketDate).ToShortDateString()
            //            , M.CustomerCode
            //            , M.CustomerName
            //            , M.DealerCode
            //            , M.DealerName
            //            , M.HMR
            //            , M.MarginWarranty
            //            , M.MachineSerialNumber
            //            //  , M.Status
            //            , M.Approved1By.ContactName
            //            , M.Approved1On
            //           , M.Approved2By.ContactName
            //            , M.Approved2On
            //            , M.InvoiceNumber
            //            , M.InvoiceDate == null ? "" : ((DateTime)M.InvoiceDate).ToShortDateString()
            //            , M.TSIRNumber
            //            , M.Model
            //            , Item.HSNCode
            //            , "'" + Item.Material
            //            , Item.MaterialDesc
            //            , Item.Category
            //            , Item.Qty
            //            , Item.UnitOM
            //            , Item.Amount
            //            , Item.BaseTax
            //             , Item.MaterialStatusRemarks1
            //            , Item.Approved1Amount
            //            , Item.Approved1Remarks
            //            , Item.MaterialStatusRemarks2
            //            , Item.Approved2Amount
            //            , Item.Approved2Remarks
            //            );
            //    }
            //}
            new BXcel().ExporttoExcel(dt, "Warranty Claim");
        }

        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{

                //    if (ddlDCTemplate.SelectedValue != "0")
                //    {
                //        Label lblWarrantyInvoiceItemID = (Label)e.Row.FindControl("lblWarrantyInvoiceItemID");
                //        var MaterialItem = Ws.FailureMaterialItems.Find(s => s.InvoiceItem.WarrantyInvoiceItemID == Convert.ToInt64(lblWarrantyInvoiceItemID.Text));
                //        if (MaterialItem != null)
                //        {
                //            if (Ws.FailureMaterialItems.Find(s => s.InvoiceItem.WarrantyInvoiceItemID == Convert.ToInt64(lblWarrantyInvoiceItemID.Text)).InvoiceItem.WarrantyInvoiceItemID != null)
                //            {
                //                GridSelected[e.Row.RowIndex, gvICTickets.PageIndex] = Convert.ToInt64(lblWarrantyInvoiceItemID.Text);
                //            }
                //        }
                //    }

                //    CheckBox cbSelectedMaterial = (CheckBox)e.Row.FindControl("cbSelectedMaterial");
                //    if (GridSelected[e.Row.RowIndex, gvICTickets.PageIndex] == 0)
                //    {
                //        cbSelectedMaterial.Checked = false;
                //    }
                //    else
                //    {
                //        cbSelectedMaterial.Checked = true;
                //    }
                //}

                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }


        protected void btnExportExcelForSAP_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Order Type");
            dt.Columns.Add("Sales Org");
            dt.Columns.Add("Dis Channel");
            dt.Columns.Add("Division");
            dt.Columns.Add("Sold To Party");
            dt.Columns.Add("Ship To Party");
            dt.Columns.Add("Pricing Date");
            dt.Columns.Add("Payment Term");
            dt.Columns.Add("Inco Terms");
            dt.Columns.Add("Order Reason");
            dt.Columns.Add("Material");
            dt.Columns.Add("Order Qty");
            dt.Columns.Add("Plant");
            dt.Columns.Add("Partner");
            dt.Columns.Add("Text- Eqip.Model");
            dt.Columns.Add("Text-Equip Sr No");
            dt.Columns.Add("FSR No");
            dt.Columns.Add("TR Date");
            dt.Columns.Add("Approved1 By");
            dt.Columns.Add("Approved2 By");
            dt.Columns.Add("Site Location");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Warranty Expiry");
            dt.Columns.Add("Date Of Comm");
            dt.Columns.Add("Kind Attention");
            dt.Columns.Add("Reason for Failure");
            dt.Columns.Add("Warr Claim Date");
            dt.Columns.Add("Number of Days");
            dt.Columns.Add("Application");
            dt.Columns.Add("TSIR");
            //foreach (PDMS_WarrantyInvoiceHeader M in SDMS_WarrantyClaimHeader)
            //{
            //    foreach (PDMS_WarrantyInvoiceItem Item in M.InvoiceItems)
            //    {
            //        dt.Rows.Add(
            //              M.OrderType, M.SalesOrg, M.DisChannel, M.Division
            //            , M.DealerCode, M.DealerCode
            //            , M.InvoiceDate == null ? "" : ((DateTime)M.InvoiceDate).ToShortDateString()
            //            , M.PaymentTerm, M.IncoTerms, M.OrderReason
            //            , "'" + Item.Material + ".FL", Item.Qty
            //            , "T001", M.CustomerCode
            //            , M.Model
            //            , M.MachineSerialNumber
            //            , M.FSRNumber
            //            , ""
            //            , M.Approved1By == null ? "" : M.Approved1By.ContactName
            //            , M.Approved2By == null ? "" : M.Approved2By.ContactName
            //             , M.Location
            //            , M.HMR
            //            , M.WarrantyEndDate == null ? "" : ((DateTime)M.WarrantyEndDate).ToShortDateString()
            //            , M.DateOfCommissioning == null ? "" : ((DateTime)M.DateOfCommissioning).ToShortDateString()
            //            , ""
            //            , M.ReasonForFailure
            //            , ""// M.ClaimDate == null ? "" : ((DateTime)M.ClaimDate).ToShortDateString()
            //            , ""
            //            , M.Application
            //            , M.TSIRNumber
            //           );
            //    }
            //}
            new BXcel().ExporttoExcel(dt, "Claim For SAP Upload");
        }

        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void gvDCTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    long DeliveryChallanID = Convert.ToInt64(gvDCTemplate.DataKeys[e.Row.RowIndex].Value);

                    GridView gvDCItem = (GridView)e.Row.FindControl("gvDCItem");

                    List<PDMS_WarrantyFailureMaterialItem> MaterialItem = new List<PDMS_WarrantyFailureMaterialItem>();
                    MaterialItem = SDMS_WarrantyClaimHeader.Find(s => s.DeliveryChallanID == DeliveryChallanID).FailureMaterialItems;
                    gvDCItem.DataSource = MaterialItem;
                    gvDCItem.DataBind();
                }

                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvDCTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDCTemplate.PageIndex = e.NewPageIndex;
            gvDCTemplate.DataSource = SDMS_WarrantyClaimHeader;
            gvDCTemplate.DataBind();

            lblRowCount.Text = (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + 1) + " - " + (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + gvDCTemplate.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

        }
        protected void ibPDF_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long DeliveryChallanID = Convert.ToInt64(gvDCTemplate.DataKeys[gvRow.RowIndex].Value);

            PAttachedFile UploadedFile = DeliveryChellanPrint(DeliveryChallanID);

            string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            Response.AddHeader("Content-type", UploadedFile.FileType);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "." + UploadedFile.FileType);
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            Response.BinaryWrite(UploadedFile.AttachedFile);
            Response.Flush();
            Response.End();
        }

        public PAttachedFile DeliveryChellanPrint(long DeliveryChallanID)
        {
            try
            {
                PDMS_WarrantyFailureMaterial FM = new BDMS_WarrantyFailureMaterial().GetWarrantyFailedMaterialDeliveryChallan(DeliveryChallanID, "", null, null, "", null, null, null)[0];

                PDMS_Customer DealerAD = new SCustomer().getCustomerAddress(FM.Dealer.DealerCode);
                string DealerAddress1 = (DealerAD.Address1 + (string.IsNullOrEmpty(DealerAD.Address2) ? "" : "," + DealerAD.Address2) + (string.IsNullOrEmpty(DealerAD.Address3) ? "" : "," + DealerAD.Address3)).Trim(',', ' ');
                string DealerAddress2 = (DealerAD.City + (string.IsNullOrEmpty(DealerAD.State.State) ? "" : "," + DealerAD.State.State) + (string.IsNullOrEmpty(DealerAD.Pincode) ? "" : "-" + DealerAD.Pincode)).Trim(',', ' ');


                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("MaterialDesc");
                CommissionDT.Columns.Add("ClaimNumber");
                CommissionDT.Columns.Add("TSIRNumber");
                CommissionDT.Columns.Add("MachineSerialNumber");
                CommissionDT.Columns.Add("Model");
                CommissionDT.Columns.Add("CustomerCode");
                CommissionDT.Columns.Add("CustomerName");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("HSNCode");

                decimal ApproximateValue = 0;
                //  decimal GrandTotal = 0;
                string StateCode = DealerAD.State.StateCode;
                string GST_Header = "";
                int i = 0;

                foreach (PDMS_WarrantyFailureMaterialItem Item in FM.FailureMaterialItems)
                {
                    i = i + 1;
                    CommissionDT.Rows.Add(i
                        , Item.Invoice.InvoiceItem.Material + ".FL"
                        , Item.Invoice.InvoiceItem.MaterialDesc
                        , Item.Invoice.InvoiceNumber
                        , Item.Invoice.TSIRNumber
                        , Item.Invoice.MachineSerialNumber
                        , Item.Invoice.Model
                        , Item.Invoice.CustomerCode
                        , Item.Invoice.CustomerName
                        , Item.Invoice.InvoiceItem.Qty
                         , Item.Invoice.InvoiceItem.HSNCode
                        );
                    ApproximateValue = Math.Round(ApproximateValue + (((decimal)Item.Invoice.InvoiceItem.Amount) * (decimal)0.05));
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

                ReportParameter[] P = new ReportParameter[15];

                P[0] = new ReportParameter("DealerCode", FM.Dealer.DealerCode, false);
                P[1] = new ReportParameter("DealerName", DealerAD.CustomerName, false);
                P[2] = new ReportParameter("Address1", DealerAddress1, false);
                P[3] = new ReportParameter("Address2", DealerAddress2, false);
                P[4] = new ReportParameter("Contact", "Contact", false);
                P[5] = new ReportParameter("GSTIN", DealerAD.GSTIN, false);
                P[6] = new ReportParameter("GST_Header", GST_Header, false);
                P[7] = new ReportParameter("InvoiceNumber", FM.DeliveryChallanNumber, false);
                P[8] = new ReportParameter("Through", FM.TransporterName, false);
                P[9] = new ReportParameter("LR", FM.DocketDetails, false);
                P[10] = new ReportParameter("InvDate", ((DateTime)FM.DeliveryChallanDate).ToShortDateString(), false);
                P[11] = new ReportParameter("DeliveryTo", FM.DeliveryTo, false);
                P[12] = new ReportParameter("PreparedBy", FM.CreatedBy.ContactName, false);
                P[13] = new ReportParameter("PackingDetails", FM.PackingDetails, false);
                P[14] = new ReportParameter("ApproximateValue", Convert.ToString(ApproximateValue), false);

                report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_WarrantyFailedMaterialDeliveryChellan.rdlc");
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet2";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);
                report.SetParameters(P);
                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                PAttachedFile InvF = new PAttachedFile();

                InvF.FileType = mimeType;
                InvF.AttachedFile = mybytes;
                InvF.AttachedFileID = 0;
                return InvF;
            }
            catch (Exception ex)
            {
                //  lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                //   lblMessage.ForeColor = Color.Red;
                //    lblMessage.Visible = true;
            }
            return null;
        }

        protected void lbEditOrUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long DeliveryChallanID = Convert.ToInt64(gvDCTemplate.DataKeys[gvRow.RowIndex].Value);
            LinkButton lbEditOrUpdate = (LinkButton)gvDCTemplate.Rows[gvRow.RowIndex].FindControl("lbEditOrUpdate");

            Label lblTransporterName = (Label)gvDCTemplate.Rows[gvRow.RowIndex].FindControl("lblTransporterName");
            Label lblDocketDetails = (Label)gvDCTemplate.Rows[gvRow.RowIndex].FindControl("lblDocketDetails");
            Label lblPackingDetails = (Label)gvDCTemplate.Rows[gvRow.RowIndex].FindControl("lblPackingDetails");
            TextBox txtTransporterName = (TextBox)gvDCTemplate.Rows[gvRow.RowIndex].FindControl("txtTransporterName");
            TextBox txtDocketDetails = (TextBox)gvDCTemplate.Rows[gvRow.RowIndex].FindControl("txtDocketDetails");
            TextBox txtPackingDetails = (TextBox)gvDCTemplate.Rows[gvRow.RowIndex].FindControl("txtPackingDetails");

            if (lbEditOrUpdate.Text == "Edit")
            {
                lblTransporterName.Visible = false;
                lblDocketDetails.Visible = false;
                lblPackingDetails.Visible = false;

                txtTransporterName.Visible = true;
                txtDocketDetails.Visible = true;
                txtPackingDetails.Visible = true;
                txtTransporterName.Text = lblTransporterName.Text;
                txtDocketDetails.Text = lblDocketDetails.Text;
                txtPackingDetails.Text = lblPackingDetails.Text;

                lbEditOrUpdate.Text = "Update";
            }
            else
            {
                long st = new BDMS_WarrantyFailureMaterial().InsertWarrantyFailureMaterialDC(DeliveryChallanID, "", txtTransporterName.Text.Trim(), txtDocketDetails.Text.Trim(), txtPackingDetails.Text.Trim(), null, PSession.User.UserID, 0);
                if (st != 0)
                {
                    List<PDMS_WarrantyFailureMaterial> SOIs = new BDMS_WarrantyFailureMaterial().GetWarrantyFailedMaterialDeliveryChallan(st, "", null, null, "", null, null, null);
                    lblMessage.Text = "Delivery Challan Number " + SOIs[0].DeliveryChallanNumber;
                    lblMessage.ForeColor = Color.Green;
                    fillClaim();

                }
                else
                {
                    lblMessage.Text = "Delivery Challan Number is not created";
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }
    }
}