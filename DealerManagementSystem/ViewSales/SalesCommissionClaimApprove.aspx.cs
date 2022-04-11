using Business;
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

namespace DealerManagementSystem.ViewSales
{
    public partial class SalesCommissionClaimApprove : System.Web.UI.Page
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
        public List<PSalesCommissionClaim> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_ClaimApprovalList1"] == null)
                {
                    Session["DMS_ClaimApprovalList1"] = new List<PSalesCommissionClaim>();
                }
                return (List<PSalesCommissionClaim>)Session["DMS_ClaimApprovalList1"];
            }
            set
            {
                Session["DMS_ClaimApprovalList1"] = value;
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

                string[] ClaimApprove1 = ConfigurationManager.AppSettings["ClaimApprove1"].Split(',');
                string[] ClaimApprove2 = ConfigurationManager.AppSettings["ClaimApprove2"].Split(',');
                string[] ClaimApprove3 = ConfigurationManager.AppSettings["ClaimApprove3"].Split(',');
                if (ClaimApprove1.Contains(PSession.User.UserID.ToString()))
                {
                    // ddlStatus.SelectedValue = "1";
                    lblStatus.Text = "L1 Approve";
                }
                else if (ClaimApprove2.Contains(PSession.User.UserID.ToString()))
                {
                    // ddlStatus.SelectedValue = "3";
                    lblStatus.Text = "L2 Approve";
                }
                else if (ClaimApprove3.Contains(PSession.User.UserID.ToString()))
                {
                    // ddlStatus.SelectedValue = "3";
                    lblStatus.Text = "L3 Approve";
                }
                else
                {
                    lblStatus.Text = "You have no permission to approve";
                    btnSearch.Visible = false;
                    btnExportExcel.Visible = false;
                }
                new BDMS_Division().GetDivisionForSerchGroped(ddlDivision);
                fillClaimApproval();
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

                long? SalesCommissionClaimID = null;
                long? SalesQuotationID = null;
                int? DealerID = null;
                string ClaimDateFrom = null;
                string ClaimDateTo = null;
                string InvoiceNumber = null;
                string InvoiceDateF = null;
                string InvoiceDateT = null;
                int? StatusID = null;

                string DealerCode = "";

                if (ddlDealerCode.SelectedValue != "0")
                {
                    DealerCode = ddlDealerCode.SelectedValue;
                }

                ClaimDateFrom = Convert.ToString(txtICLoginDateFrom.Text.Trim()); 
                ClaimDateTo = Convert.ToString(txtICLoginDateTo.Text.Trim()); 
                InvoiceDateF = Convert.ToString(txtClaimDateF.Text.Trim()); 
                InvoiceDateT = Convert.ToString(txtClaimDateT.Text.Trim());
                

                //  StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
                string[] ClaimApprove1 = ConfigurationManager.AppSettings["ClaimApprove1"].Split(',');
                string[] ClaimApprove2 = ConfigurationManager.AppSettings["ClaimApprove2"].Split(',');
                string[] ClaimApprove3 = ConfigurationManager.AppSettings["ClaimApprove3"].Split(',');
                if (ClaimApprove1.Contains(PSession.User.UserID.ToString()))
                {
                    StatusID = 1;
                }
                else if (ClaimApprove2.Contains(PSession.User.UserID.ToString()))
                {
                    StatusID = 2;
                }
                else if (ClaimApprove3.Contains(PSession.User.UserID.ToString()))
                {
                    StatusID = 3;
                }
                string DivisionID = ddlDivision.SelectedValue == "0" ? null : ddlDivision.SelectedValue;




                SDMS_WarrantyClaimHeader = new BSalesCommissionClaim().GetSalesCommissionClaimApproval(SalesCommissionClaimID, SalesQuotationID, DealerID,
                    ClaimDateFrom, ClaimDateTo, InvoiceNumber, InvoiceDateF, InvoiceDateT, StatusID);
                 
                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
                gvICTickets.DataBind();
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

            foreach (PSalesCommissionClaim M in SDMS_WarrantyClaimHeader)
            {
                //foreach (PDMS_WarrantyInvoiceItem Item in M.InvoiceItems)
                //{
                    //dt.Rows.Add(
                    //      M.ICTicketID
                    //    , M.ICTicketDate == null ? "" : ((DateTime)M.ICTicketDate).ToShortDateString()
                    //    , M.CustomerCode
                    //    , M.CustomerName
                    //    , M.DealerCode
                    //    , M.DealerName
                    //    , M.HMR
                    //    , M.MarginWarranty
                    //    , M.MachineSerialNumber
                    //    , M.ClaimStatus
                    //    , M.Approved1By.ContactName
                    //    , M.Approved1On
                    //   , M.Approved2By.ContactName
                    //    , M.Approved2On
                    //    , M.InvoiceNumber
                    //    , ((DateTime)M.InvoiceDate).ToShortDateString()
                    //    , M.TSIRNumber
                    //    , M.Model
                    //    , Item.HSNCode
                    //    , "'" + Item.Material
                    //    , Item.MaterialDesc
                    //    , Item.Category
                    //    , Item.Qty
                    //    , Item.UnitOM
                    //    , Item.Amount
                    //    , Item.BaseTax
                    //    // , Item.MaterialStatus
                    //    , Item.MaterialStatusRemarks1
                    //    , Item.Approved1Amount
                    //    , Item.Approved1Remarks
                    //    , Item.MaterialStatusRemarks2
                    //    , Item.Approved2Amount
                    //    , Item.Approved2Remarks
                    //    );
                //}
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
                    long SalesCommissionClaimID = Convert.ToInt64(gvICTickets.DataKeys[e.Row.RowIndex].Value);
                    GridView supplierPOLinesGrid = (GridView)e.Row.FindControl("gvICTicketItems");
                    Label lblICTicketID = (Label)e.Row.FindControl("lblICTicketID");

                    List<PSalesCommissionClaim> supplierPurchaseOrderLines = new List<PSalesCommissionClaim>();
                    supplierPurchaseOrderLines = (List<PSalesCommissionClaim>)SDMS_WarrantyClaimHeader.Where(s => s.SalesCommissionClaimID == SalesCommissionClaimID);

                    supplierPOLinesGrid.DataSource = supplierPurchaseOrderLines;
                    supplierPOLinesGrid.DataBind();

                    string[] ClaimApprove1 = ConfigurationManager.AppSettings["ClaimApprove1"].Split(',');
                    string[] ClaimApprove2 = ConfigurationManager.AppSettings["ClaimApprove2"].Split(',');
                    string[] ClaimApprove3 = ConfigurationManager.AppSettings["ClaimApprove3"].Split(',');
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    if (lblStatus.Text == "REQUESTED")
                    {
                        if (ClaimApprove1.Contains(PSession.User.UserID.ToString()))
                        {
                            Button btnApproved1By = (Button)e.Row.FindControl("btnApproved1By");
                            Label lblApproved1By = (Label)e.Row.FindControl("lblApproved1By");
                            btnApproved1By.Visible = true;
                            lblApproved1By.Visible = false;
                            for (int i = 0; i < supplierPOLinesGrid.Rows.Count; i++)
                            {
                                Label lblAmount = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblAmount"); 
                                TextBox txtApproved1Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved1Amount");
                                txtApproved1Amount.Text = lblAmount.Text;
                                txtApproved1Amount.Enabled = true;  
                            }
                        } 
                    }
                    else if (lblStatus.Text == "APPROVED L1")
                    {
                        if (ClaimApprove2.Contains(PSession.User.UserID.ToString()))
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
                            }
                        }
                    }
                    else if(lblStatus.Text == "APPROVED L2")
                    {
                        if (ClaimApprove3.Contains(PSession.User.UserID.ToString()))
                        {
                            Button btnApproved3By = (Button)e.Row.FindControl("btnApproved3By");
                            Label lblApproved3By = (Label)e.Row.FindControl("lblApproved3By");
                            btnApproved3By.Visible = true;
                            lblApproved3By.Visible = false;
                            for (int i = 0; i < supplierPOLinesGrid.Rows.Count; i++)
                            { 
                                TextBox txtApproved2Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved2Amount");
                                TextBox txtApproved3Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved3Amount");
                                txtApproved3Amount.Text = txtApproved2Amount.Text;
                                txtApproved3Amount.Enabled = true; 
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


        protected void btnApproved1By_Click(object sender, EventArgs e)
        {
            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //GridView supplierPOLinesGrid = (GridView)gvICTickets.Rows[gvRow.RowIndex].FindControl("gvICTicketItems");
            //Label lblWarrantyInvoiceHeaderID = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblWarrantyInvoiceHeaderID");
            //Label lblInvoiceNumber = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceNumber");

            //Label lblInvoiceDate = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceDate");

            //List<PDMS_WarrantyInvoiceItem> Claims = new List<PDMS_WarrantyInvoiceItem>();
            //for (int i = 0; i < supplierPOLinesGrid.Rows.Count; i++)
            //{
            //    Label lblWarrantyInvoiceItemID = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblWarrantyInvoiceItemID");
            //    DropDownList ddlMaterialStatusRemarks1 = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlMaterialStatusRemarks1");

            //    TextBox txtApproved1Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved1Amount");

            //    DropDownList ddlApproved1Remarks = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlApproved1Remarks");
            //    Label lblAmount = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblAmount"); 
            //    decimal Amount = Convert.ToDecimal(lblAmount.Text);
            //    decimal parsedValue;
            //    if (!decimal.TryParse(txtApproved1Amount.Text, out parsedValue))
            //    {
            //        lblMessage.Text = "Please Enter decimal value in approve amount !";
            //        txtApproved1Amount.Focus();
            //        lblMessage.Visible = true;
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //    if (Amount < Convert.ToDecimal(txtApproved1Amount.Text))
            //    {
            //        lblMessage.Text = "Please enter approve amount less than or equal of claim amount";
            //        txtApproved1Amount.Focus();
            //        lblMessage.Visible = true;
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //    Label lblCategory = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblCategory");
            //    Label lblMaterial = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterial"); 
            //    Claims.Add(new PDMS_WarrantyInvoiceItem()
            //    {
            //        WarrantyInvoiceItemID = Convert.ToInt64(lblWarrantyInvoiceItemID.Text),
            //        WarrantyInvoiceHeaderID = Convert.ToInt64(lblWarrantyInvoiceHeaderID.Text),
            //        MaterialStatusRemarks1 = ddlMaterialStatusRemarks1.SelectedValue == "0" ? "" : ddlMaterialStatusRemarks1.SelectedItem.Text,
            //        Approved1Amount = Convert.ToDecimal(txtApproved1Amount.Text),
            //        Approved1Remarks = ddlApproved1Remarks.SelectedValue == "0" ? "" : ddlApproved1Remarks.SelectedItem.Text,
            //    });
            //}
            //if (new BDMS_WarrantyClaim().ApproveWarrantyClaims1(Claims, PSession.User.UserID, 1))
            //{
            //    lblMessage.Text = "Claime number " + lblInvoiceNumber.Text + " is approved";
            //    lblMessage.ForeColor = Color.Green;
            //    SDMS_WarrantyClaimHeader.RemoveAll(m => m.InvoiceNumber == lblInvoiceNumber.Text);
            //    gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
            //    gvICTickets.DataBind();
            //    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            //}
            //else
            //{
            //    lblMessage.Text = "Claime number " + lblInvoiceNumber.Text + " is not approved";
            //    lblMessage.ForeColor = Color.Red;
            //}
            //lblMessage.Visible = true;
        }

        protected void btnApproved2By_Click(object sender, EventArgs e)
        {
            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //GridView supplierPOLinesGrid = (GridView)gvICTickets.Rows[gvRow.RowIndex].FindControl("gvICTicketItems");
            //Label lblWarrantyInvoiceHeaderID = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblWarrantyInvoiceHeaderID"); 
            //Label lblInvoiceNumber = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceNumber");
            //Label lblInvoiceDate = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceDate");
            
            //List<PDMS_WarrantyInvoiceItem> Claims = new List<PDMS_WarrantyInvoiceItem>();
            //for (int i = 0; i < supplierPOLinesGrid.Rows.Count; i++)
            //{
            //    Label lblWarrantyInvoiceItemID = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblWarrantyInvoiceItemID");
            //    DropDownList ddlMaterialStatusRemarks2 = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlMaterialStatusRemarks2");
            //    TextBox txtApproved2Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved2Amount");
            //    DropDownList ddlApproved2Remarks = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlApproved2Remarks");
            //    Label lblAmount = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblAmount");
            //    decimal parsedValue;
            //    if (!decimal.TryParse(txtApproved2Amount.Text, out parsedValue))
            //    {
            //        lblMessage.Text = "Please Enter decimal value in approve amount !";
            //        txtApproved2Amount.Focus();
            //        lblMessage.Visible = true;
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //    decimal Amount = Convert.ToDecimal(lblAmount.Text);
            //    if (Amount < Convert.ToDecimal(txtApproved2Amount.Text))
            //    {
            //        lblMessage.Text = "Please enter approve amount less than or equal of claim amount";
            //        txtApproved2Amount.Focus();
            //        lblMessage.Visible = true;
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //    Label lblCategory = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblCategory");

            //    Label lblMaterial = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterial");
                 
            //    Claims.Add(new PDMS_WarrantyInvoiceItem()
            //    {
            //        WarrantyInvoiceItemID = Convert.ToInt64(lblWarrantyInvoiceItemID.Text),
            //        WarrantyInvoiceHeaderID = Convert.ToInt64(lblWarrantyInvoiceHeaderID.Text), 
            //        MaterialStatusRemarks1 = ddlMaterialStatusRemarks2.SelectedValue == "0" ? "" : ddlMaterialStatusRemarks2.SelectedItem.Text,
            //        Approved1Amount = Convert.ToDecimal(txtApproved2Amount.Text),
            //        Approved1Remarks = ddlApproved2Remarks.SelectedValue == "0" ? "" : ddlApproved2Remarks.SelectedItem.Text,
            //    });
            //}

            //if (new BDMS_WarrantyClaim().ApproveWarrantyClaims1(Claims, PSession.User.UserID, 2))
            //{
            //    lblMessage.Text = "Invoice number " + lblInvoiceNumber.Text + " is approved";
            //    lblMessage.ForeColor = Color.Green; 
            //    SDMS_WarrantyClaimHeader.RemoveAll(m => m.InvoiceNumber == lblInvoiceNumber.Text);
            //    gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
            //    gvICTickets.DataBind();
            //    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

            //}
            //else
            //{
            //    lblMessage.Text = "Invoice number " + lblInvoiceNumber.Text + " is not approved";
            //    lblMessage.ForeColor = Color.Red;
            //}
            //lblMessage.Visible = true;
        }

        protected void btnApproved3By_Click(object sender, EventArgs e)
        {
            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //GridView supplierPOLinesGrid = (GridView)gvICTickets.Rows[gvRow.RowIndex].FindControl("gvICTicketItems");
            //Label lblWarrantyInvoiceHeaderID = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblWarrantyInvoiceHeaderID"); 
            //Label lblInvoiceNumber = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceNumber");
            //Label lblInvoiceDate = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceDate");
             
            //List<PDMS_WarrantyInvoiceItem> Claims = new List<PDMS_WarrantyInvoiceItem>();
            //for (int i = 0; i < supplierPOLinesGrid.Rows.Count; i++)
            //{
            //    Label lblWarrantyInvoiceItemID = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblWarrantyInvoiceItemID");
            //    DropDownList ddlMaterialStatusRemarks3 = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlMaterialStatusRemarks3");
            //    TextBox txtApproved3Amount = (TextBox)supplierPOLinesGrid.Rows[i].FindControl("txtApproved3Amount");
            //    DropDownList ddlApproved3Remarks = (DropDownList)supplierPOLinesGrid.Rows[i].FindControl("ddlApproved3Remarks");
            //    Label lblAmount = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblAmount");
            //    decimal parsedValue;
            //    if (!decimal.TryParse(txtApproved3Amount.Text, out parsedValue))
            //    {
            //        lblMessage.Text = "Please Enter decimal value in approve amount !";
            //        txtApproved3Amount.Focus();
            //        lblMessage.Visible = true;
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //    decimal Amount = Convert.ToDecimal(lblAmount.Text);
            //    if (Amount < Convert.ToDecimal(txtApproved3Amount.Text))
            //    {
            //        lblMessage.Text = "Please enter approve amount less than or equal of claim amount";
            //        txtApproved3Amount.Focus();
            //        lblMessage.Visible = true;
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //    Label lblCategory = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblCategory");
            //    Label lblMaterial = (Label)supplierPOLinesGrid.Rows[i].FindControl("lblMaterial");
               
                
            //    Claims.Add(new PDMS_WarrantyInvoiceItem()
            //    {
            //        WarrantyInvoiceItemID = Convert.ToInt64(lblWarrantyInvoiceItemID.Text),
            //        WarrantyInvoiceHeaderID = Convert.ToInt64(lblWarrantyInvoiceHeaderID.Text), 
            //        Approved1Amount = Convert.ToDecimal(txtApproved3Amount.Text),
            //        Approved1Remarks = ddlApproved3Remarks.SelectedValue == "0" ? "" : ddlApproved3Remarks.SelectedItem.Text,
            //    });
            //}

            //if (new BDMS_WarrantyClaim().ApproveWarrantyClaims1(Claims, PSession.User.UserID, 3))
            //{
            //    lblMessage.Text = "Invoice number " + lblInvoiceNumber.Text + " is approved";
            //    lblMessage.ForeColor = Color.Green; 
            //    SDMS_WarrantyClaimHeader.RemoveAll(m => m.InvoiceNumber == lblInvoiceNumber.Text);
            //    gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
            //    gvICTickets.DataBind();
            //    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

            //}
            //else
            //{
            //    lblMessage.Text = "Invoice number " + lblInvoiceNumber.Text + " is not approved";
            //    lblMessage.ForeColor = Color.Red;
            //}
            //lblMessage.Visible = true;
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
        
    }
}