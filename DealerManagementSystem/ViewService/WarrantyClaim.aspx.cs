using Business;
using DealerManagementSystem.UserControls;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyClaim : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_WarrantyClaim; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "WarrantyClaim1.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_WarrantyInvoiceHeader_New> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_WarrantyClaim1"] == null)
                {
                    Session["DMS_WarrantyClaim1"] = new List<PDMS_WarrantyInvoiceHeader_New>();
                }
                return (List<PDMS_WarrantyInvoiceHeader_New>)Session["DMS_WarrantyClaim1"];
            }
            set
            {
                Session["DMS_WarrantyClaim1"] = value;
            }
        }

        public List<PDMS_WarrantyTicket> SDMS_WarrantyClaimHeaderByTicket
        {
            get
            {
                if (Session["DMS_WarrantyClaimByTicket1"] == null)
                {
                    Session["DMS_WarrantyClaimByTicket1"] = new List<PDMS_WarrantyTicket>();
                }
                return (List<PDMS_WarrantyTicket>)Session["DMS_WarrantyClaimByTicket1"];
            }
            set
            {
                Session["DMS_WarrantyClaimByTicket1"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Warranty » Claim Report');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                //new BDMS_WarrantyClaim().insertWarrantyClaim();
               
                fillDealer();
                txtClaimDateF.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtClaimDateT.Text = DateTime.Now.ToShortDateString();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
                FillStatus();
                //FillCategory();

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
                

                //int? DealerCode = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                //DateTime? ICTicketDateF = string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateFrom.Text.Trim());
                //DateTime? ICTicketDateT = string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateTo.Text.Trim()); ;

                //DateTime? ClaimDateF = string.IsNullOrEmpty(txtClaimDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtClaimDateF.Text.Trim());
                //DateTime? ClaimDateT = string.IsNullOrEmpty(txtClaimDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtClaimDateT.Text.Trim());

                //DateTime? ApprovedDateF = string.IsNullOrEmpty(txtApprovedDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtApprovedDateF.Text.Trim());
                //DateTime? ApprovedDateT = string.IsNullOrEmpty(txtApprovedDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtApprovedDateT.Text.Trim());


                //int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);


                PWarrantyClaim_Filter Filter = new PWarrantyClaim_Filter();

                Filter.DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                Filter.ICTicketDateF = string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateFrom.Text.Trim());
                Filter.ICTicketDateT = string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateTo.Text.Trim()); ;

                Filter.ClaimDateF = string.IsNullOrEmpty(txtClaimDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtClaimDateF.Text.Trim());
                Filter.ClaimDateT = string.IsNullOrEmpty(txtClaimDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtClaimDateT.Text.Trim());

                Filter.ApprovedDateF = string.IsNullOrEmpty(txtApprovedDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtApprovedDateF.Text.Trim());
                Filter.ApprovedDateT = string.IsNullOrEmpty(txtApprovedDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtApprovedDateT.Text.Trim()); 
                Filter.StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue); 
                Filter.ICTicketNumber = txtICServiceTicket.Text.Trim();
                Filter.ClaimNumber = txtClaimNumber.Text.Trim();
                Filter.TSIRNumber = txtTSIRNumber.Text.Trim();
                Filter.CustomerCode = txtCustomerCode.Text.Trim();
                Filter.MachineSerialNumber = txtMachineSerialNumber.Text.Trim();
                Filter.IsAbove50K = cbIsAbove50K.Checked;
                SDMS_WarrantyClaimHeader = new BDMS_WarrantyClaim().GetWarrantyClaimReport_New1(Filter);

                GridView gv = null;
                //if (ddlReoprt.SelectedValue == "0")
                //{
                //SDMS_WarrantyClaimHeader = new BDMS_WarrantyClaim().GetWarrantyClaimReport_New(txtICServiceTicket.Text.Trim(), ICTicketDateF, ICTicketDateT, txtClaimNumber.Text.Trim(), ClaimDateF, ClaimDateT, DealerCode, StatusID, ApprovedDateF, ApprovedDateT, txtTSIRNumber.Text.Trim(), txtCustomerCode.Text.Trim(), txtMachineSerialNumber.Text.Trim(), cbIsAbove50K.Checked, PSession.User.UserID);

                gv = gvClaimByClaimID;
                gvClaimByClaimID.Visible = true;
                gvClaimByTicket.Visible = false;
                gv.PageIndex = 0;
                gv.DataSource = SDMS_WarrantyClaimHeader;
                gv.DataBind();
                if (SDMS_WarrantyClaimHeader.Count == 0)
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
                    lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
                } 
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("WarrantyClaim", "fillClaim", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            GridView gv = null;
            //if (ddlReoprt.SelectedValue == "0")
            //{
                gv = gvClaimByClaimID;
                gv.DataSource = SDMS_WarrantyClaimHeader;
            //}
            //else
            //{
            //    gv = gvClaimByTicket;
            //    gv.DataSource = SDMS_WarrantyClaimHeaderByTicket;
            //}

            if (gv.PageIndex > 0)
            {
                gv.PageIndex = gv.PageIndex - 1;

                gv.DataBind();
                lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            GridView gv = null;
            //if (ddlReoprt.SelectedValue == "0")
            //{
                gv = gvClaimByClaimID;
                gv.DataSource = SDMS_WarrantyClaimHeader;
            //}
            //else
            //{
            //    gv = gvClaimByTicket;
            //    gv.DataSource = SDMS_WarrantyClaimHeaderByTicket;
            //}

            if (gv.PageCount > gv.PageIndex)
            {
                gv.PageIndex = gv.PageIndex + 1;
                gv.DataBind();
                lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IC Ticket");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Restore Date");
            dt.Columns.Add("Service Type");
            dt.Columns.Add("Cust. Code");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Margin Warranty");
            dt.Columns.Add("Machine Serial Number");
            dt.Columns.Add("Commissioning On");
            dt.Columns.Add("Nature of Failure");
            dt.Columns.Add("Status");
            dt.Columns.Add("Apr.1 By");
            dt.Columns.Add("Apr.1 On");
            dt.Columns.Add("Apr.2 By");
            dt.Columns.Add("Apr.2 On");

            dt.Columns.Add("Apr.3 By");
            dt.Columns.Add("Apr.3 On");

            dt.Columns.Add("Claim Number");
            dt.Columns.Add("Claim Date");

            dt.Columns.Add("Model");
            dt.Columns.Add("SAC / HSN Code");
            dt.Columns.Add("Material");
            dt.Columns.Add("Material Desc");
            dt.Columns.Add("Category");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Per");
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

            dt.Columns.Add("Apr.3 Amt");
            dt.Columns.Add("Apr.3 Remarks");

            dt.Columns.Add("TSIR Number");
            dt.Columns.Add("SAP Doc");
            dt.Columns.Add("SAP Posting Date");

            dt.Columns.Add("SAP Invoice Value");

            dt.Columns.Add("Invoice Number");
            dt.Columns.Add("Invoice Date");
            dt.Columns.Add("Annexure Number");
            dt.Columns.Add("Annexure Date");

            foreach (PDMS_WarrantyInvoiceHeader_New M in SDMS_WarrantyClaimHeader)
            {
                foreach (PDMS_WarrantyInvoiceItem Item in M.InvoiceItems)
                {
                    dt.Rows.Add(

                          M.ICTicketNumber
                        , M.ICTicketDate == null ? "" : ((DateTime)M.ICTicketDate).ToShortDateString()
                        , M.RestoreDate == null ? "" : ((DateTime)M.RestoreDate).ToShortDateString()
                        , M.ICTicket.ServiceType.ServiceType
                        , M.CustomerCode
                        , M.CustomerName
                        , M.DealerCode
                        , M.DealerName
                        , M.HMR
                        , M.MarginWarranty
                        , M.MachineSerialNumber
                        , M.ICTicket.Equipment.CommissioningOn
                         , M.ICTicket.ComplaintDescription
                        , M.ClaimStatus
                        , M.Approved1By.ContactName
                        , M.Approved1On
                       , M.Approved2By.ContactName
                        , M.Approved2On
                        , M.Approved3By.ContactName
                        , M.Approved3On
                        , M.InvoiceNumber
                        , ((DateTime)M.InvoiceDate).ToShortDateString()
                        //, M.TSIRNumber
                        , M.Model
                        , Item.HSNCode
                        , "'" + Item.Material
                        , Item.MaterialDesc
                        , Item.Category
                        , Item.Qty
                        , Item.Per
                        , Item.UnitOM
                        , Item.Amount
                        , Item.BaseTax
                        // , Item.MaterialStatus
                        , Item.MaterialStatusRemarks1
                        , Item.Approved1Amount
                        , Item.Approved1Remarks
                        , Item.MaterialStatusRemarks2
                        , Item.Approved2Amount
                        , Item.Approved2Remarks
                         , Item.Approved3Amount
                        , Item.Approved3Remarks
                        , Item.TSIRNumber
                        , Item.SAPDoc
                        , Item.SAPPostingDate
                         , Item.SAPInvoiceValue
                          , M.AcInvoiceNumber

                           , M.AcInvoiceDate == null ? "" : ((DateTime)M.AcInvoiceDate).ToShortDateString()
                            , Item.AnnexureNumber
                        , M.AnnexureDate == null ? "" : ((DateTime)M.AnnexureDate).ToShortDateString()

                       );
                }
            }
            new BXcel().ExporttoExcel(dt, "Warranty Claim");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = null;
            //if (ddlReoprt.SelectedValue == "0")
            //{
                gv = gvClaimByClaimID;
                gv.DataSource = SDMS_WarrantyClaimHeader;
            //}
            //else
            //{
            //    gv = gvClaimByTicket;
            //    gv.DataSource = SDMS_WarrantyClaimHeaderByTicket;
            //}

            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
            lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

        }
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string supplierPOID = Convert.ToString(gvClaimByClaimID.DataKeys[e.Row.RowIndex].Value);
                    GridView supplierPOLinesGrid = (GridView)e.Row.FindControl("gvICTicketItems");
                    //Label lblPscID = (Label)e.Row.FindControl("lblPscID");
                    //if (!string.IsNullOrEmpty(lblPscID.Text))
                    //{
                    //    GridView gvFileAttached = (GridView)e.Row.FindControl("gvFileAttached");
                    //    gvFileAttached.DataSource = new BDMS_WarrantyClaim().GetAttachment("'" + lblPscID.Text.Trim() + "'");
                    //    gvFileAttached.DataBind();
                    //}
                    Label lblICTicketID = (Label)e.Row.FindControl("lblICTicketID");
                    //List<PDMS_ICTicket> SOIs = new BDMS_ICTicket().GetICTicket(null, "", lblICTicketID.Text, null, null, null, null);
                    //if (SOIs.Count == 1)
                    //{
                        List<PAttachedFile> UploadedFile = new BDMS_ICTicket().GetICTicketAttachedFile(Convert.ToInt64(lblICTicketID.Text), null);
                        GridView gvFileAttachedAF = (GridView)e.Row.FindControl("gvFileAttachedAF");
                        gvFileAttachedAF.DataSource = UploadedFile;
                        gvFileAttachedAF.DataBind();

                        List<PDMS_FSRAttachedFile> UploadedFileFSR = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileDetails(Convert.ToInt64(lblICTicketID.Text), null);
                        GridView gvFileAttachedFSR = (GridView)e.Row.FindControl("gvFileAttachedFSR");
                        gvFileAttachedFSR.DataSource = UploadedFileFSR;
                        gvFileAttachedFSR.DataBind();

                        List<PDMS_TSIRAttachedFile> UploadedFileTSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(Convert.ToInt64(lblICTicketID.Text), null, null);
                        GridView gvFileAttachedTSIR = (GridView)e.Row.FindControl("gvFileAttachedTSIR");
                        gvFileAttachedTSIR.DataSource = UploadedFileTSIR;
                        gvFileAttachedTSIR.DataBind();

                   // }

                  
                    List<PDMS_WarrantyInvoiceItem> supplierPurchaseOrderLines = new List<PDMS_WarrantyInvoiceItem>();
                    supplierPurchaseOrderLines = SDMS_WarrantyClaimHeader.Find(s => s.InvoiceNumber == supplierPOID).InvoiceItems;

                    supplierPOLinesGrid.DataSource = supplierPurchaseOrderLines;
                    supplierPOLinesGrid.DataBind();
                   // string[] ClaimCancel = ConfigurationManager.AppSettings["ClaimCancel"].Split(',');

                    List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                     
                    if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ClaimCancel).Count() == 1)
                    {

                        Label lblAnnexureNumber = (Label)e.Row.FindControl("lblAnnexureNumber");
                        Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                        if ((string.IsNullOrEmpty(lblAnnexureNumber.Text)) && (lblStatus.Text != "CANCELED"))
                        {
                            Button btnCancel = (Button)e.Row.FindControl("btnCancel");
                            btnCancel.Visible = true;
                        }
                    }
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
            }
        }

        void FillStatus()
        {
            List<PDMS_WarrantyStatus> Status = new BDMS_WarrantyClaim().GetWarrantyClaimStatus();
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataSource = Status;
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("All", "0"));

        }
        protected void btnExportExcelForSAP_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("vendor_code");
            dt.Columns.Add("invoice_date");
            dt.Columns.Add("reference");
            dt.Columns.Add("material");
            dt.Columns.Add("base_value");
            dt.Columns.Add("total");
            dt.Columns.Add("bp_code");
            dt.Columns.Add("bp_name");
            dt.Columns.Add("ic_ticket");
            dt.Columns.Add("machine_serial_no");
            dt.Columns.Add("hsn_code");
            dt.Columns.Add("Created by");
            dt.Columns.Add("model");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Apr.1 Amt");
            dt.Columns.Add("Apr.1 By");
            dt.Columns.Add("Apr.1 On");
            dt.Columns.Add("Apr.2 Amt");
            dt.Columns.Add("Apr.2 By");
            dt.Columns.Add("Apr.2 On");

            foreach (PDMS_WarrantyInvoiceHeader_New M in SDMS_WarrantyClaimHeader)
            {
                foreach (PDMS_WarrantyInvoiceItem Item in M.InvoiceItems)
                {
                    dt.Rows.Add(
                         string.Format("80{0}", M.DealerCode.Substring(2))
                        , ((DateTime)M.InvoiceDate).ToShortDateString()
                        , M.InvoiceNumber
                        , "'" + Item.Material
                        , Item.Amount
                        , Item.BaseTax
                        , M.CustomerCode
                        , M.CustomerName
                        , M.ICTicketNumber
                        , M.MachineSerialNumber
                        , Item.HSNCode
                        , ""
                        , M.Model
                        , M.HMR
                          , Item.Approved1Amount
                          , M.Approved1By.ContactName
                        , M.Approved1On
                         , Item.Approved2Amount
                       , M.Approved2By.ContactName
                        , M.Approved2On
                       );
                }
            }
            new BXcel().ExporttoExcel(dt, "Claim For SAP Upload");
        }

        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void gvClaimByTicket_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string supplierPOID = Convert.ToString(gvClaimByTicket.DataKeys[e.Row.RowIndex].Value);
                    GridView supplierPOLinesGrid = (GridView)e.Row.FindControl("gvICTicketItems");

                    //Label lblPscID = (Label)e.Row.FindControl("lblPscID");
                    //GridView gvFileAttached = (GridView)e.Row.FindControl("gvFileAttached");
                    //gvFileAttached.DataSource = new BDMS_WarrantyClaim().GetAttachment("'" + lblPscID.Text.Trim() + "'");
                    //gvFileAttached.DataBind();


                    List<PDMS_WarrantyInvoiceHeader> supplierPurchaseOrderLines = new List<PDMS_WarrantyInvoiceHeader>();
                    supplierPurchaseOrderLines = SDMS_WarrantyClaimHeaderByTicket.Find(s => s.ICTicketID == supplierPOID).Invoice;

                    supplierPOLinesGrid.DataSource = supplierPurchaseOrderLines;
                    supplierPOLinesGrid.DataBind();


                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblInvoiceNumber = (Label)gvClaimByClaimID.Rows[gvRow.RowIndex].FindControl("lblInvoiceNumber");
            if (new BDMS_WarrantyClaim().CancelWarrantyClaims(lblInvoiceNumber.Text, PSession.User.UserID))
            {

                lblMessage.Text = "Claime number " + lblInvoiceNumber.Text + " is canceled";
                lblMessage.ForeColor = Color.Green;
                //  fillClaimApproval();
                SDMS_WarrantyClaimHeader.RemoveAll(m => m.InvoiceNumber == lblInvoiceNumber.Text);
                gvClaimByClaimID.DataSource = SDMS_WarrantyClaimHeader;
                gvClaimByClaimID.DataBind();
                lblRowCount.Text = (((gvClaimByClaimID.PageIndex) * gvClaimByClaimID.PageSize) + 1) + " - " + (((gvClaimByClaimID.PageIndex) * gvClaimByClaimID.PageSize) + gvClaimByClaimID.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
            else
            {
                lblMessage.Text = "Claime number " + lblInvoiceNumber.Text + " is not canceled";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true;
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {


                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.Parent.Parent;
                Label lblAttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");

                long AttachedFileID = Convert.ToInt64(lblAttachedFileID.Text);
                PAttachedFile UploadedFile = new BDMS_ICTicket().GetICTicketAttachedFile(null, AttachedFileID)[0];

                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName.Replace(",", " "));
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkMachineSerialNumber_Click(object sender, EventArgs e)
        {
            divEquipmentView.Visible = true;
            divClaimList.Visible = false;

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblEquipmentHeaderID = (Label)gvRow.FindControl("lblEquipmentHeaderID");
            if (!string.IsNullOrEmpty(lblEquipmentHeaderID.Text))
            {
                UC_EquipmentView.fillEquipment(Convert.ToInt64(lblEquipmentHeaderID.Text));
            }
            //LinkButton lnkEquipmentSerialNo = (LinkButton)sender;
            //Session["SerEquipmentSerialNo"] = lnkEquipmentSerialNo.Text;
            //PlaceHolder phDashboard = (PlaceHolder)tblDashboard.FindControl("ph_usercontrols_1");
            //EquipmentView ucEquipmentView = (EquipmentView)this.LoadControl("~/UserControls/EquipmentView.ascx");
            //ucEquipmentView.ID = "ucEquipmentView";
            //phDashboard.Controls.Add(ucEquipmentView);
            //mp1.Show();
        }
        protected void lnkTSIR_Click(object sender, EventArgs e)
        {
            divTSIRView.Visible = true;
            divClaimList.Visible = false;

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblTsirID = (Label)gvRow.FindControl("lblTsirID");
            if (!string.IsNullOrEmpty(lblTsirID.Text))
            {
                UC_TSIRView.FillTsir(Convert.ToInt64(lblTsirID.Text));
            }

            //LinkButton lnkTSIR = (LinkButton)sender;
            //Session["TSIRNumber"] = lnkTSIR.Text;
            //PlaceHolder phDashboard = (PlaceHolder)tblDashboard.FindControl("ph_usercontrols_2");
            //ICTicketTSIRView ucICTicketTSIRView = (ICTicketTSIRView)this.LoadControl("~/UserControls/ICTicketTSIRView.ascx");
            //ucICTicketTSIRView.ID = "ucICTicketTSIRView";
            //phDashboard.Controls.Add(ucICTicketTSIRView);
            //mpTSIR.Show();
        }
        protected void lnkFSRDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkFSRDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkFSRDownload.Parent.Parent;
                Label lblAttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");

                long AttachedFileID = Convert.ToInt64(lblAttachedFileID.Text);
                //PDMS_FSRAttachedFile UploadedFileFSR = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileByID(AttachedFileID);
                PAttachedFile UploadedFileFSR = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileForDownload(AttachedFileID);

                Response.AddHeader("Content-type", UploadedFileFSR.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFileFSR.FileName.Replace(",", " "));
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFileFSR.AttachedFile);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            { }
        }

        protected void lnkTSIRDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.Parent.Parent;
                Label lblAttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");

                long AttachedFileID = Convert.ToInt64(lblAttachedFileID.Text);
                // PDMS_TSIRAttachedFile UploadedFileTSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileByID(AttachedFileID);
                PAttachedFile UploadedFileTSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileForDownload(AttachedFileID);
                Response.AddHeader("Content-type", UploadedFileTSIR.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFileTSIR.FileName.Replace(",", " "));
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFileTSIR.AttachedFile);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnTSIRViewBack_Click(object sender, EventArgs e)
        {
            divClaimList.Visible = true;
            divTSIRView.Visible = false;
        }

        protected void btnEquipmentViewBack_Click(object sender, EventArgs e)
        {
            divClaimList.Visible = true;
            divEquipmentView.Visible = false;
        }
    }
}