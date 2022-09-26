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
    public partial class WarrantyClaimApprovalLevel1 : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_ClaimApprovalList1.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_WarrantyInvoiceHeader> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_ClaimApprovalList1"] == null)
                {
                    Session["DMS_ClaimApprovalList1"] = new List<PDMS_WarrantyInvoiceHeader>();
                }
                return (List<PDMS_WarrantyInvoiceHeader>)Session["DMS_ClaimApprovalList1"];
            }
            set
            {
                Session["DMS_ClaimApprovalList1"] = value;
            }
        }
        public List<PDMS_Remarks> MStatusRemarks
        {
            get
            {
                if (Session["Remarks1"] == null)
                {
                    Session["Remarks1"] = new List<PDMS_Remarks>();
                }
                return (List<PDMS_Remarks>)Session["Remarks1"];
            }
            set
            {
                Session["Remarks1"] = value;
            }
        }
        public List<PDMS_Remarks> Remarks
        {
            get
            {
                if (Session["Remarks2"] == null)
                {
                    Session["Remarks2"] = new List<PDMS_Remarks>();
                }
                return (List<PDMS_Remarks>)Session["Remarks2"];
            }
            set
            {
                Session["Remarks2"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Warranty » Claim Approval');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                //   new BDMS_WarrantyClaim().insertWarrantyClaim();
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
                //FillStatus();
                //FillCategory();
                MStatusRemarks = new BDMS_Master().GetRemarks(1, null);
                Remarks = new BDMS_Master().GetRemarks(2, null);

                //string[] ClaimApprove1 = ConfigurationManager.AppSettings["ClaimApprove1"].Split(',');
                //string[] ClaimApprove2 = ConfigurationManager.AppSettings["ClaimApprove2"].Split(',');
                //string[] ClaimApprove3 = ConfigurationManager.AppSettings["ClaimApprove3"].Split(',');

                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ClaimApproval1).Count() == 1)
                {
                    lblStatus.Text = "L1 Approve";
                }
                else if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ClaimApproval2).Count() == 1)
                {
                    lblStatus.Text = "L2 Approve";
                }
                else if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ClaimApproval3).Count() == 1)
                {
                    lblStatus.Text = "L3 Approve";
                }
                else
                {
                    lblStatus.Text = "You have no permission to approve";
                    btnSearch.Visible = false;
                    btnExportExcel.Visible = false;
                }
                //if (ClaimApprove1.Contains(PSession.User.UserID.ToString()))
                //{ 
                //    lblStatus.Text = "L1 Approve";
                //}
                //else if (ClaimApprove2.Contains(PSession.User.UserID.ToString()))
                //{ 
                //    lblStatus.Text = "L2 Approve";
                //}
                //else if (ClaimApprove3.Contains(PSession.User.UserID.ToString()))
                //{ 
                //    lblStatus.Text = "L3 Approve";
                //}
                //else
                //{
                //    lblStatus.Text = "You have no permission to approve";
                //    btnSearch.Visible = false;
                //    btnExportExcel.Visible = false;
                //}
                new BDMS_Division().GetDivisionForSerchGroped(ddlDivision);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillClaimApproval();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillClaimApproval()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);

                DateTime? ICTicketDateF = null;
                DateTime? ICTicketDateT = null;

                DateTime? ClaimDateF = null;
                DateTime? ClaimDateT = null;
                int? StatusID = null;
                string DealerCode = "";

                if (ddlDealerCode.SelectedValue != "0")
                {
                    DealerCode = ddlDealerCode.SelectedValue;
                }
                if (!string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()))
                {
                    ICTicketDateF = Convert.ToDateTime(txtICLoginDateFrom.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()))
                {
                    ICTicketDateT = Convert.ToDateTime(txtICLoginDateTo.Text.Trim());
                }

                if (!string.IsNullOrEmpty(txtClaimDateF.Text.Trim()))
                {
                    ClaimDateF = Convert.ToDateTime(txtClaimDateF.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtClaimDateT.Text.Trim()))
                {
                    ClaimDateT = Convert.ToDateTime(txtClaimDateT.Text.Trim());
                }

                ////  StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);




                //string[] ClaimApprove1 = ConfigurationManager.AppSettings["ClaimApprove1"].Split(',');
                //string[] ClaimApprove2 = ConfigurationManager.AppSettings["ClaimApprove2"].Split(',');
                //string[] ClaimApprove3 = ConfigurationManager.AppSettings["ClaimApprove3"].Split(',');
                //if (ClaimApprove1.Contains(PSession.User.UserID.ToString()))
                //{
                //    StatusID = 1;
                //}
                //else if (ClaimApprove2.Contains(PSession.User.UserID.ToString()))
                //{
                //    StatusID = 2;
                //}
                //else if (ClaimApprove3.Contains(PSession.User.UserID.ToString()))
                //{
                //    StatusID = 3;
                //}

                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ClaimApproval1).Count() == 1)
                {
                    StatusID = 1;
                }
                else if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ClaimApproval2).Count() == 1)
                {
                    StatusID = 2;
                }
                else if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ClaimApproval3).Count() == 1)
                {
                    StatusID = 3;
                }

                string DivisionID = ddlDivision.SelectedValue == "0" ? null : ddlDivision.SelectedValue;
                List<PDMS_WarrantyInvoiceHeader> SOIs = new BDMS_WarrantyClaim().GetWarrantyClaimApproval(txtICServiceTicket.Text.Trim(), ICTicketDateF, ICTicketDateT, txtClaimID.Text.Trim(), ClaimDateF, ClaimDateT, DealerCode, StatusID, txtTSIRNumber.Text.Trim(), DivisionID, PSession.User.UserID);
                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs
                                 join D in PSession.User.Dealer on S.DealerCode equals D.UserName
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

                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = SOIs;
                gvICTickets.DataBind();
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
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_ClaimApprovalList1", "fillClaimApproval", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;
                gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IC Ticket ID");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Cust. Code");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Margin Warranty");
            dt.Columns.Add("Machine Serial Number");
            dt.Columns.Add("Status");
            dt.Columns.Add("Apr.1 By");
            dt.Columns.Add("Apr 1 On");
            dt.Columns.Add("Apr.2 By");
            dt.Columns.Add("Apr 2 On");

            dt.Columns.Add("Claim Number");
            dt.Columns.Add("Claim Date");

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
            dt.Columns.Add("Apr. 1 Amt");
            dt.Columns.Add("Apr. 1 Remarks");
            dt.Columns.Add("Failure Mat Remarks 2");
            dt.Columns.Add("Apr. 2 Amt");
            dt.Columns.Add("Apr. 2 Remarks");

            foreach (PDMS_WarrantyInvoiceHeader M in SDMS_WarrantyClaimHeader)
            {
                foreach (PDMS_WarrantyInvoiceItem Item in M.InvoiceItems)
                {
                    dt.Rows.Add(
                          M.ICTicketID
                        , M.ICTicketDate == null ? "" : ((DateTime)M.ICTicketDate).ToShortDateString()
                        , M.CustomerCode
                        , M.CustomerName
                        , M.DealerCode
                        , M.DealerName
                        , M.HMR
                        , M.MarginWarranty
                        , M.MachineSerialNumber
                        , M.ClaimStatus
                        , M.Approved1By.ContactName
                        , M.Approved1On
                       , M.Approved2By.ContactName
                        , M.Approved2On
                        , M.InvoiceNumber
                        , ((DateTime)M.InvoiceDate).ToShortDateString()
                        , M.TSIRNumber
                        , M.Model
                        , Item.HSNCode
                        , "'" + Item.Material
                        , Item.MaterialDesc
                        , Item.Category
                        , Item.Qty
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
                        );
                }
            }
            new BXcel().ExporttoExcel(dt, "Warranty Claim");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

        }
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string supplierPOID = Convert.ToString(gvICTickets.DataKeys[e.Row.RowIndex].Value);
                    GridView supplierPOLinesGrid = (GridView)e.Row.FindControl("gvICTicketItems");

                    Label lblPscID = (Label)e.Row.FindControl("lblPscID");
                    if (!string.IsNullOrEmpty(lblPscID.Text))
                    {
                        GridView gvFileAttached = (GridView)e.Row.FindControl("gvFileAttached");
                        gvFileAttached.DataSource = new BDMS_WarrantyClaim().GetAttachment("'" + lblPscID.Text.Trim() + "'");
                        gvFileAttached.DataBind();
                    }
                    Label lblICTicketID = (Label)e.Row.FindControl("lblICTicketID");

                    List<PDMS_ICTicket> SOIs = new BDMS_ICTicket().GetICTicket(null, "", lblICTicketID.Text, null, null, null, null);
                    if (SOIs.Count == 1)
                    {
                        List<PAttachedFile> UploadedFile = new BDMS_ICTicket().GetICTicketAttachedFile(SOIs[0].ICTicketID, null);

                        GridView gvFileAttachedAF = (GridView)e.Row.FindControl("gvFileAttachedAF");
                        gvFileAttachedAF.DataSource = UploadedFile;
                        gvFileAttachedAF.DataBind();

                        List<PDMS_FSRAttachedFile> UploadedFileFSR = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileDetails(SOIs[0].ICTicketID, null);
                        GridView gvFileAttachedFSR = (GridView)e.Row.FindControl("gvFileAttachedFSR");
                        gvFileAttachedFSR.DataSource = UploadedFileFSR;
                        gvFileAttachedFSR.DataBind();


                        List<PDMS_TSIRAttachedFile> UploadedFileTSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(SOIs[0].ICTicketID, null, null);
                        GridView gvFileAttachedTSIR = (GridView)e.Row.FindControl("gvFileAttachedTSIR");
                        gvFileAttachedTSIR.DataSource = UploadedFileTSIR;
                        gvFileAttachedTSIR.DataBind();

                    }

                    List<PDMS_WarrantyInvoiceItem> supplierPurchaseOrderLines = new List<PDMS_WarrantyInvoiceItem>();
                    supplierPurchaseOrderLines = SDMS_WarrantyClaimHeader.Find(s => s.InvoiceNumber == supplierPOID).InvoiceItems;

                    supplierPOLinesGrid.DataSource = supplierPurchaseOrderLines;
                    supplierPOLinesGrid.DataBind();

                    //string[] ClaimApprove1 = ConfigurationManager.AppSettings["ClaimApprove1"].Split(',');
                   // string[] ClaimApprove2 = ConfigurationManager.AppSettings["ClaimApprove2"].Split(',');
                   // string[] ClaimApprove3 = ConfigurationManager.AppSettings["ClaimApprove3"].Split(',');

                    List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
 
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    if (lblStatus.Text == "REQUESTED")
                    {
                        if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ClaimApproval1).Count() == 1)
                        {
                            Button btnApproved1By = (Button)e.Row.FindControl("btnApproved1By");
                            Label lblApproved1By = (Label)e.Row.FindControl("lblApproved1By");
                            btnApproved1By.Visible = true;
                            lblApproved1By.Visible = false;
                            for (int i = 0; i < supplierPOLinesGrid.Rows.Count; i++)
                            {
                                //Label lblMaterialStatus = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterialStatus");
                                Label lblAmount = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblAmount");



                                //DropDownList ddlMaterialStatus = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlMaterialStatus");                               
                                TextBox txtApproved1Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved1Amount");
                                txtApproved1Amount.Text = lblAmount.Text;
                                //ddlMaterialStatus.SelectedValue = lblMaterialStatus.Text.Trim() == "" ? "0" : lblMaterialStatus.Text.Trim();
                                //lblMaterialStatus.Visible = false;
                                //ddlMaterialStatus.Visible = true;                             
                                txtApproved1Amount.Enabled = true;


                                Label lblMaterialStatusRemarks1 = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterialStatusRemarks1");
                                DropDownList ddlMaterialStatusRemarks1 = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlMaterialStatusRemarks1");
                                ddlMaterialStatusRemarks1.Visible = true;
                                lblMaterialStatusRemarks1.Visible = false;
                                ddlMaterialStatusRemarks1.DataTextField = "Remarks";
                                ddlMaterialStatusRemarks1.DataValueField = "RemarksSubID";
                                ddlMaterialStatusRemarks1.DataSource = MStatusRemarks;
                                ddlMaterialStatusRemarks1.DataBind();
                                ddlMaterialStatusRemarks1.Items.Insert(0, new ListItem("Select", "0"));

                                DropDownList ddlApproved1Remarks = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlApproved1Remarks");
                                Label lblApproved1Remarks = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblApproved1Remarks");
                                lblApproved1Remarks.Visible = false;
                                ddlApproved1Remarks.Visible = true;
                                ddlApproved1Remarks.DataTextField = "Remarks";
                                ddlApproved1Remarks.DataValueField = "RemarksSubID";
                                ddlApproved1Remarks.DataSource = Remarks;
                                ddlApproved1Remarks.DataBind();
                                ddlApproved1Remarks.Items.Insert(0, new ListItem("Select", "0"));
                                Label lblCategory = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblCategory");
                                if (lblCategory.Text != "Warranty.")
                                {
                                    ddlMaterialStatusRemarks1.Visible = false;
                                }
                            }
                        }

                        if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ClaimApproval2).Count() == 1)
                        {
                            Button btnApproved2By = (Button)e.Row.FindControl("btnApproved2By");
                            Label lblApproved2By = (Label)e.Row.FindControl("lblApproved2By");
                            btnApproved2By.Visible = true;
                            lblApproved2By.Visible = false;
                            for (int i = 0; i < supplierPOLinesGrid.Rows.Count; i++)
                            {

                                TextBox txtApproved1Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved1Amount");
                                TextBox txtApproved2Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved2Amount");
                                txtApproved2Amount.Text = txtApproved1Amount.Text;
                                txtApproved2Amount.Enabled = true;


                                Label lblMaterialStatusRemarks2 = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterialStatusRemarks2");
                                DropDownList ddlMaterialStatusRemarks2 = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlMaterialStatusRemarks2");

                                lblMaterialStatusRemarks2.Visible = false;
                                ddlMaterialStatusRemarks2.Visible = true;
                                ddlMaterialStatusRemarks2.DataTextField = "Remarks";
                                ddlMaterialStatusRemarks2.DataValueField = "RemarksSubID";
                                ddlMaterialStatusRemarks2.DataSource = MStatusRemarks;
                                ddlMaterialStatusRemarks2.DataBind();
                                ddlMaterialStatusRemarks2.Items.Insert(0, new ListItem("Select", "0"));

                                DropDownList ddlApproved2Remarks = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlApproved2Remarks");
                                Label lblApproved2Remarks = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblApproved2Remarks");
                                lblApproved2Remarks.Visible = false;
                                ddlApproved2Remarks.Visible = true;
                                ddlApproved2Remarks.DataTextField = "Remarks";
                                ddlApproved2Remarks.DataValueField = "RemarksSubID";
                                ddlApproved2Remarks.DataSource = Remarks;
                                ddlApproved2Remarks.DataBind();
                                ddlApproved2Remarks.Items.Insert(0, new ListItem("Select", "0"));
                                Label lblCategory = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblCategory");
                                if (lblCategory.Text != "Warranty.")
                                {
                                    ddlMaterialStatusRemarks2.Visible = false;
                                }
                            }
                        }
                    }
                    if (lblStatus.Text == "APPROVED L1")
                    {
                        if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ClaimApproval2).Count() == 1)
                        {
                            Button btnApproved2By = (Button)e.Row.FindControl("btnApproved2By");
                            Label lblApproved2By = (Label)e.Row.FindControl("lblApproved2By");
                            btnApproved2By.Visible = true;
                            lblApproved2By.Visible = false;
                            for (int i = 0; i < supplierPOLinesGrid.Rows.Count; i++)
                            {
                                // Label lblMaterialStatus = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterialStatus");
                                //  DropDownList ddlMaterialStatus = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlMaterialStatus");
                                //ddlMaterialStatus.Visible = true;
                                //ddlMaterialStatus.SelectedValue = lblMaterialStatus.Text.Trim() == "" ? "0" : lblMaterialStatus.Text.Trim();
                                //lblMaterialStatus.Visible = false;

                                TextBox txtApproved1Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved1Amount");
                                TextBox txtApproved2Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved2Amount");
                                txtApproved2Amount.Text = txtApproved1Amount.Text;
                                txtApproved2Amount.Enabled = true;


                                Label lblMaterialStatusRemarks2 = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterialStatusRemarks2");
                                DropDownList ddlMaterialStatusRemarks2 = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlMaterialStatusRemarks2");

                                lblMaterialStatusRemarks2.Visible = false;
                                ddlMaterialStatusRemarks2.Visible = true;
                                ddlMaterialStatusRemarks2.DataTextField = "Remarks";
                                ddlMaterialStatusRemarks2.DataValueField = "RemarksSubID";
                                ddlMaterialStatusRemarks2.DataSource = MStatusRemarks;
                                ddlMaterialStatusRemarks2.DataBind();
                                ddlMaterialStatusRemarks2.Items.Insert(0, new ListItem("Select", "0"));

                                DropDownList ddlApproved2Remarks = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlApproved2Remarks");
                                Label lblApproved2Remarks = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblApproved2Remarks");
                                lblApproved2Remarks.Visible = false;
                                ddlApproved2Remarks.Visible = true;
                                ddlApproved2Remarks.DataTextField = "Remarks";
                                ddlApproved2Remarks.DataValueField = "RemarksSubID";
                                ddlApproved2Remarks.DataSource = Remarks;
                                ddlApproved2Remarks.DataBind();
                                ddlApproved2Remarks.Items.Insert(0, new ListItem("Select", "0"));
                                Label lblCategory = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblCategory");
                                if (lblCategory.Text != "Warranty.")
                                {
                                    ddlMaterialStatusRemarks2.Visible = false;
                                }
                            }
                        }
                    }
                    if (lblStatus.Text == "APPROVED L2")
                    {
                        if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ClaimApproval3).Count() == 1)
                        {
                            Button btnApproved3By = (Button)e.Row.FindControl("btnApproved3By");
                            Label lblApproved3By = (Label)e.Row.FindControl("lblApproved3By");
                            btnApproved3By.Visible = true;
                            lblApproved3By.Visible = false;
                            for (int i = 0; i < supplierPOLinesGrid.Rows.Count; i++)
                            {
                                TextBox txtApproved1Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved1Amount");
                                TextBox txtApproved2Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved2Amount");
                                TextBox txtApproved3Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved3Amount");
                                txtApproved3Amount.Text = txtApproved2Amount.Text;
                                txtApproved3Amount.Enabled = true;


                                //Label lblMaterialStatusRemarks2 = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterialStatusRemarks2");
                                //DropDownList ddlMaterialStatusRemarks2 = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlMaterialStatusRemarks2");

                                //lblMaterialStatusRemarks2.Visible = false;
                                //ddlMaterialStatusRemarks2.Visible = true;
                                //ddlMaterialStatusRemarks2.DataTextField = "Remarks";
                                //ddlMaterialStatusRemarks2.DataValueField = "RemarksSubID";
                                //ddlMaterialStatusRemarks2.DataSource = MStatusRemarks;
                                //ddlMaterialStatusRemarks2.DataBind();
                                //ddlMaterialStatusRemarks2.Items.Insert(0, new ListItem("Select", "0"));

                                DropDownList ddlApproved3Remarks = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlApproved3Remarks");
                                Label lblApproved3Remarks = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblApproved3Remarks");
                                lblApproved3Remarks.Visible = false;
                                ddlApproved3Remarks.Visible = true;
                                ddlApproved3Remarks.DataTextField = "Remarks";
                                ddlApproved3Remarks.DataValueField = "RemarksSubID";
                                ddlApproved3Remarks.DataSource = Remarks;
                                ddlApproved3Remarks.DataBind();
                                ddlApproved3Remarks.Items.Insert(0, new ListItem("Select", "0"));
                                //Label lblCategory = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblCategory");
                                //if (lblCategory.Text != "Warranty.")
                                //{
                                //    ddlMaterialStatusRemarks2.Visible = false;
                                //}
                            }
                        }
                    }
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_MTTR_Report", "fillMTTR", ex);
                throw ex;
            }
        }

        //void FillStatus()
        //{
        //    List<PDMS_WarrantyStatus> Status = new BDMS_WarrantyClaim().GetWarrantyClaimStatus();
        //    ddlStatus.DataTextField = "Status";
        //    ddlStatus.DataValueField = "StatusID";
        //    ddlStatus.DataSource = Status;
        //    ddlStatus.DataBind();
        //    ddlStatus.Items.Insert(0, new ListItem("All", "0"));

        //}

        //void FillCategory()
        //{
        //    List<PDMS_WarrantyClaimCategory> Status = new BDMS_Master().GetCategory(null,"");
        //    ddlCategory.DataTextField = "Category";
        //    ddlCategory.DataValueField = "CategoryID";
        //    ddlCategory.DataSource = Status;
        //    ddlCategory.DataBind();
        //    ddlCategory.Items.Insert(0, new ListItem("All", "0"));

        //}
        protected void btnApproved1By_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            GridView supplierPOLinesGrid = (GridView)gvICTickets.Rows[gvRow.RowIndex].FindControl("gvICTicketItems");
            Label lblWarrantyInvoiceHeaderID = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblWarrantyInvoiceHeaderID");
            //  Label lblClaimID = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblClaimID");
            Label lblInvoiceNumber = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceNumber");

            Label lblInvoiceDate = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceDate");
            if (ControlBaseOn60Days(Convert.ToDateTime(lblInvoiceDate.Text), lblInvoiceNumber.Text))
            {
                lblMessage.Text = "This claim crossed the date. Please get approval.";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }

            List<PDMS_WarrantyInvoiceItem> Claims = new List<PDMS_WarrantyInvoiceItem>();
            for (int i = 0; i < supplierPOLinesGrid.Rows.Count; i++)
            {
                Label lblWarrantyInvoiceItemID = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblWarrantyInvoiceItemID");
                DropDownList ddlMaterialStatusRemarks1 = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlMaterialStatusRemarks1");

                TextBox txtApproved1Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved1Amount");

                DropDownList ddlApproved1Remarks = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlApproved1Remarks");
                Label lblAmount = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblAmount");


                decimal Amount = Convert.ToDecimal(lblAmount.Text);
                decimal parsedValue;
                if (!decimal.TryParse(txtApproved1Amount.Text, out parsedValue))
                {
                    lblMessage.Text = "Please Enter decimal value in approve amount !";
                    txtApproved1Amount.Focus();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (Amount < Convert.ToDecimal(txtApproved1Amount.Text))
                {
                    lblMessage.Text = "Please enter approve amount less than or equal of claim amount";
                    txtApproved1Amount.Focus();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                Label lblCategory = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblCategory");
                Label lblMaterial = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterial");

                PDMS_Material Mat = new BDMS_Material().GetMaterialListSQL(null, lblMaterial.Text, null, null, null)[0];
                if (Mat.MaterialGroup == "887" || Mat.IsMainServiceMaterial == true)
                { }
                else if (lblCategory.Text == "Warranty.")
                {
                    Label lblDeliveryNumber = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblDeliveryNumber");

                    if (string.IsNullOrEmpty(lblDeliveryNumber.Text))
                    {
                        lblMessage.Text = "Material " + lblMaterial.Text + " is not Delivered. So that you cannot approve this claim  !";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    if (ddlMaterialStatusRemarks1.SelectedValue == "0")
                    {
                        lblMessage.Text = "Please select the material status remarks 1 !";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }

                Claims.Add(new PDMS_WarrantyInvoiceItem()
                {
                    WarrantyInvoiceItemID = Convert.ToInt64(lblWarrantyInvoiceItemID.Text),
                    WarrantyInvoiceHeaderID = Convert.ToInt64(lblWarrantyInvoiceHeaderID.Text),
                    MaterialStatusRemarks1 = ddlMaterialStatusRemarks1.SelectedValue == "0" ? "" : ddlMaterialStatusRemarks1.SelectedItem.Text,
                    Approved1Amount = Convert.ToDecimal(txtApproved1Amount.Text),
                    Approved1Remarks = ddlApproved1Remarks.SelectedValue == "0" ? "" : ddlApproved1Remarks.SelectedItem.Text,
                });
            }
            if (new BDMS_WarrantyClaim().ApproveWarrantyClaims1(Claims, PSession.User.UserID, 1))
            {
                lblMessage.Text = "Claime number " + lblInvoiceNumber.Text + " is approved";
                lblMessage.ForeColor = Color.Green;
                //  fillClaimApproval();
                SDMS_WarrantyClaimHeader.RemoveAll(m => m.InvoiceNumber == lblInvoiceNumber.Text);
                gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

            }
            else
            {
                lblMessage.Text = "Claime number " + lblInvoiceNumber.Text + " is not approved";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true;
        }

        protected void btnApproved2By_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            GridView supplierPOLinesGrid = (GridView)gvICTickets.Rows[gvRow.RowIndex].FindControl("gvICTicketItems");
            Label lblWarrantyInvoiceHeaderID = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblWarrantyInvoiceHeaderID");
            //  Label lblClaimID = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblClaimID");
            Label lblInvoiceNumber = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceNumber");
            Label lblInvoiceDate = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceDate");
            if (ControlBaseOn60Days(Convert.ToDateTime(lblInvoiceDate.Text), lblInvoiceNumber.Text))
            {
                lblMessage.Text = "This claim crossed the date. Please get approval.";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            List<PDMS_WarrantyInvoiceItem> Claims = new List<PDMS_WarrantyInvoiceItem>();
            for (int i = 0; i < supplierPOLinesGrid.Rows.Count; i++)
            {
                Label lblWarrantyInvoiceItemID = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblWarrantyInvoiceItemID");
                DropDownList ddlMaterialStatusRemarks2 = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlMaterialStatusRemarks2");
                TextBox txtApproved2Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved2Amount");
                DropDownList ddlApproved2Remarks = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlApproved2Remarks");
                Label lblAmount = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblAmount");
                decimal parsedValue;
                if (!decimal.TryParse(txtApproved2Amount.Text, out parsedValue))
                {
                    lblMessage.Text = "Please Enter decimal value in approve amount !";
                    txtApproved2Amount.Focus();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                decimal Amount = Convert.ToDecimal(lblAmount.Text);
                if (Amount < Convert.ToDecimal(txtApproved2Amount.Text))
                {
                    lblMessage.Text = "Please enter approve amount less than or equal of claim amount";
                    txtApproved2Amount.Focus();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                Label lblCategory = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblCategory");

                Label lblMaterial = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterial");
                PDMS_Material Mat = new BDMS_Material().GetMaterialListSQL(null, lblMaterial.Text, null, null, null)[0];
                if (Mat.MaterialGroup == "887")
                { }
                else if (lblCategory.Text == "Warranty.")
                {
                    Label lblDeliveryNumber = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblDeliveryNumber");
                    //Label lblMaterial = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterial");
                    if (string.IsNullOrEmpty(lblDeliveryNumber.Text))
                    {
                        lblMessage.Text = "Material " + lblMaterial.Text + " is not Delivered. So that you cannot approve this claim  !";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }

                    if (ddlMaterialStatusRemarks2.SelectedValue == "0")
                    {
                        lblMessage.Text = "Please select the material status remarks 2 !";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                Claims.Add(new PDMS_WarrantyInvoiceItem()
                {
                    WarrantyInvoiceItemID = Convert.ToInt64(lblWarrantyInvoiceItemID.Text),
                    WarrantyInvoiceHeaderID = Convert.ToInt64(lblWarrantyInvoiceHeaderID.Text),
                    //   MaterialStatus = ddlMaterialStatus.SelectedValue == "0" ? "" : ddlMaterialStatus.SelectedValue,
                    MaterialStatusRemarks1 = ddlMaterialStatusRemarks2.SelectedValue == "0" ? "" : ddlMaterialStatusRemarks2.SelectedItem.Text,
                    Approved1Amount = Convert.ToDecimal(txtApproved2Amount.Text),
                    Approved1Remarks = ddlApproved2Remarks.SelectedValue == "0" ? "" : ddlApproved2Remarks.SelectedItem.Text,
                });
            }

            if (new BDMS_WarrantyClaim().ApproveWarrantyClaims1(Claims, PSession.User.UserID, 2))
            {
                lblMessage.Text = "Invoice number " + lblInvoiceNumber.Text + " is approved";
                lblMessage.ForeColor = Color.Green;
                //  fillClaimApproval();
                SDMS_WarrantyClaimHeader.RemoveAll(m => m.InvoiceNumber == lblInvoiceNumber.Text);
                gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

            }
            else
            {
                lblMessage.Text = "Invoice number " + lblInvoiceNumber.Text + " is not approved";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true;
        }

        protected void btnApproved3By_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            GridView supplierPOLinesGrid = (GridView)gvICTickets.Rows[gvRow.RowIndex].FindControl("gvICTicketItems");
            Label lblWarrantyInvoiceHeaderID = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblWarrantyInvoiceHeaderID");
            //  Label lblClaimID = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblClaimID");
            Label lblInvoiceNumber = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceNumber");
            Label lblInvoiceDate = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceDate");
            if (ControlBaseOn60Days(Convert.ToDateTime(lblInvoiceDate.Text), lblInvoiceNumber.Text))
            {
                lblMessage.Text = "This claim crossed the date. Please get approval.";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            List<PDMS_WarrantyInvoiceItem> Claims = new List<PDMS_WarrantyInvoiceItem>();
            for (int i = 0; i < supplierPOLinesGrid.Rows.Count; i++)
            {
                Label lblWarrantyInvoiceItemID = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblWarrantyInvoiceItemID");
                DropDownList ddlMaterialStatusRemarks3 = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlMaterialStatusRemarks3");
                TextBox txtApproved3Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved3Amount");
                DropDownList ddlApproved3Remarks = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlApproved3Remarks");
                Label lblAmount = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblAmount");
                decimal parsedValue;
                if (!decimal.TryParse(txtApproved3Amount.Text, out parsedValue))
                {
                    lblMessage.Text = "Please Enter decimal value in approve amount !";
                    txtApproved3Amount.Focus();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                decimal Amount = Convert.ToDecimal(lblAmount.Text);
                if (Amount < Convert.ToDecimal(txtApproved3Amount.Text))
                {
                    lblMessage.Text = "Please enter approve amount less than or equal of claim amount";
                    txtApproved3Amount.Focus();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                Label lblCategory = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblCategory");
                Label lblMaterial = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterial");
                PDMS_Material Mat = new BDMS_Material().GetMaterialListSQL(null, lblMaterial.Text, null, null, null)[0];
                if (Mat.MaterialGroup == "887")
                { }
                else if (lblCategory.Text == "Warranty.")
                {
                    Label lblDeliveryNumber = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblDeliveryNumber");

                    if (string.IsNullOrEmpty(lblDeliveryNumber.Text))
                    {
                        lblMessage.Text = "Material " + lblMaterial.Text + " is not Delivered. So that you cannot approve this claim  !";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }

                    //if (ddlMaterialStatusRemarks3.SelectedValue == "0")
                    //{
                    //    lblMessage.Text = "Please select the material status remarks 2 !";
                    //    lblMessage.Visible = true;
                    //    lblMessage.ForeColor = Color.Red;
                    //    return;
                    //}
                }
                Claims.Add(new PDMS_WarrantyInvoiceItem()
                {
                    WarrantyInvoiceItemID = Convert.ToInt64(lblWarrantyInvoiceItemID.Text),
                    WarrantyInvoiceHeaderID = Convert.ToInt64(lblWarrantyInvoiceHeaderID.Text),
                    //   MaterialStatus = ddlMaterialStatus.SelectedValue == "0" ? "" : ddlMaterialStatus.SelectedValue,
                    //  MaterialStatusRemarks1 = ddlMaterialStatusRemarks3.SelectedValue == "0" ? "" : ddlMaterialStatusRemarks3.SelectedItem.Text,
                    Approved1Amount = Convert.ToDecimal(txtApproved3Amount.Text),
                    Approved1Remarks = ddlApproved3Remarks.SelectedValue == "0" ? "" : ddlApproved3Remarks.SelectedItem.Text,
                });
            }

            if (new BDMS_WarrantyClaim().ApproveWarrantyClaims1(Claims, PSession.User.UserID, 3))
            {
                lblMessage.Text = "Invoice number " + lblInvoiceNumber.Text + " is approved";
                lblMessage.ForeColor = Color.Green;
                //  fillClaimApproval();
                SDMS_WarrantyClaimHeader.RemoveAll(m => m.InvoiceNumber == lblInvoiceNumber.Text);
                gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

            }
            else
            {
                lblMessage.Text = "Invoice number " + lblInvoiceNumber.Text + " is not approved";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true;
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
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
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName);
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
            LinkButton lnkEquipmentSerialNo = (LinkButton)sender;
            Session["SerEquipmentSerialNo"] = lnkEquipmentSerialNo.Text;
            PlaceHolder phDashboard = (PlaceHolder)tblDashboard.FindControl("ph_usercontrols_1");
            EquipmentView ucDMS_EquipmentView = (EquipmentView)this.LoadControl("~/UserControls/DMS_EquipmentView.ascx");
            ucDMS_EquipmentView.ID = "ucDMS_EquipmentView";
            phDashboard.Controls.Add(ucDMS_EquipmentView);
            mp1.Show();
        }
        protected void lnkFSRDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkFSRDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkFSRDownload.Parent.Parent;
                Label lblAttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");

                long AttachedFileID = Convert.ToInt64(lblAttachedFileID.Text);
                PDMS_FSRAttachedFile UploadedFileFSR = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileByID(AttachedFileID);

                Response.AddHeader("Content-type", UploadedFileFSR.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFileFSR.FileName);
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
                PDMS_TSIRAttachedFile UploadedFileTSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileByID(AttachedFileID);
                Response.AddHeader("Content-type", UploadedFileTSIR.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFileTSIR.FileName);
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
        protected void lnkTSIR_Click(object sender, EventArgs e)
        {
            LinkButton lnkTSIR = (LinkButton)sender;
            Session["TSIRNumber"] = lnkTSIR.Text;
            PlaceHolder phDashboard = (PlaceHolder)tblDashboard.FindControl("ph_usercontrols_2");
            ICTicketTSIRView ucDMS_ICTicketTSIRView = (ICTicketTSIRView)this.LoadControl("~/UserControls/DMS_ICTicketTSIRView.ascx");
            ucDMS_ICTicketTSIRView.ID = "ucDMS_ICTicketTSIRView";
            phDashboard.Controls.Add(ucDMS_ICTicketTSIRView);
            mpTSIR.Show();
        }

        Boolean ControlBaseOn60Days(DateTime InvoiceDate, string InvoiceNumber)
        {
            Boolean ch = false;
            try
            {
                int Days = Convert.ToInt32(ConfigurationManager.AppSettings["ClaimLockDate"]);

                if (InvoiceDate.AddDays(Days) < DateTime.Now)
                {
                    ch = true;
                    List<PDMS_WarrantyInvoiceHeader> ICTicketDT = new BDMS_WarrantyClaim().GetDeviatedClaimReport(null, InvoiceNumber, null, null, PSession.User.UserID);
                    if (ICTicketDT.Count == 1)
                    {
                        if (ICTicketDT[0].DeviatedIsApproved == true)
                        {
                            ch = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ch = true;
            }
            return ch;
        }
    }
}