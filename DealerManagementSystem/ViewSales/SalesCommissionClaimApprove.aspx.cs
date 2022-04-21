using Business;
using Newtonsoft.Json;
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

                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SalesCommClaimAproval1).Count() != 0)
                { 
                    lblStatus.Text = "L1 Approve";
                }
                else if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SalesCommClaimAproval2).Count() != 0)
                {
                    lblStatus.Text = "L2 Approve";
                }
                else if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SalesCommClaimAproval3).Count() != 0)
                {
                    lblStatus.Text = "L3 Approve";
                } 
                else
                {
                    lblStatus.Text = "You have no permission to approve";
                    btnSearch.Visible = false;
                    btnExportExcel.Visible = false;
                } 

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
                int? DealerID = null; 
                string ClaimNumber = null; 
                string ClaimDateFrom = Convert.ToString(txtClaimDateF.Text.Trim());
                string ClaimDateTo = Convert.ToString(txtClaimDateT.Text.Trim());
                int StatusID = 0; 
                if (ddlDealerCode.SelectedValue != "0")
                {
                    DealerID = Convert.ToInt32( ddlDealerCode.SelectedValue);
                }

                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;

                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SalesCommClaimAproval1).Count() != 0)
                {
                    StatusID = 1;
                }
                else if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SalesCommClaimAproval2).Count() != 0)
                {
                    StatusID = 2;
                }
                else if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SalesCommClaimAproval3).Count() != 0)
                {
                    StatusID = 3;
                }

                SDMS_WarrantyClaimHeader = new BSalesCommissionClaim().GetSalesCommissionClaimApproval(DealerID, ClaimNumber, ClaimDateFrom, ClaimDateTo, StatusID); 
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

                foreach (GridViewRow grv in gvICTickets.Rows)
                {
                    for (int i = 0; i < gvICTickets.Rows.Count; i++)
                    {
                        if (StatusID == 1)
                        { 
                            Button btnApproved1By = (Button)gvICTickets.Rows[i].FindControl("btnApproved1By");
                            Label lblApproved1By = (Label)gvICTickets.Rows[i].FindControl("lblApproved1By");
                            btnApproved1By.Visible = true;
                            lblApproved1By.Visible = false;

                            Label lblAmount = (Label)gvICTickets.Rows[i].FindControl("lblAmount");
                            TextBox txtApproved1Amount = (TextBox)gvICTickets.Rows[i].FindControl("txtApproved1Amount");
                            txtApproved1Amount.Text = lblAmount.Text;
                            txtApproved1Amount.Enabled = true;

                            TextBox txtApproved1Remarks = (TextBox)gvICTickets.Rows[i].FindControl("txtApproved1Remarks");
                            txtApproved1Remarks.Enabled = true;
                        }
                        else if (StatusID == 2)
                        {
                            Button btnApproved2By = (Button)gvICTickets.Rows[i].FindControl("btnApproved2By");
                            Label lblApproved2By = (Label)gvICTickets.Rows[i].FindControl("lblApproved2By");
                            btnApproved2By.Visible = true;
                            lblApproved2By.Visible = false; 
                            TextBox txtApproved1Amount = (TextBox)gvICTickets.Rows[i].FindControl("txtApproved1Amount");
                            TextBox txtApproved2Amount = (TextBox)gvICTickets.Rows[i].FindControl("txtApproved2Amount");
                            txtApproved2Amount.Text = txtApproved1Amount.Text;
                            txtApproved2Amount.Enabled = true;

                            TextBox txtApproved2Remarks = (TextBox)gvICTickets.Rows[i].FindControl("txtApproved2Remarks");
                            txtApproved2Remarks.Enabled = true;
                        }
                        else if (StatusID == 3)
                        {
                            Button btnApproved3By = (Button)gvICTickets.Rows[i].FindControl("btnApproved3By");
                            Label lblApproved3By = (Label)gvICTickets.Rows[i].FindControl("lblApproved3By");
                            btnApproved3By.Visible = true;
                            lblApproved3By.Visible = false;
                            TextBox txtApproved2Amount = (TextBox)gvICTickets.Rows[i].FindControl("txtApproved2Amount");
                            TextBox txtApproved3Amount = (TextBox)gvICTickets.Rows[i].FindControl("txtApproved3Amount");
                            txtApproved3Amount.Text = txtApproved2Amount.Text;
                            txtApproved3Amount.Enabled = true; 

                            TextBox txtApproved3Remarks = (TextBox)gvICTickets.Rows[i].FindControl("txtApproved3Remarks");
                            txtApproved3Remarks.Enabled = true;
                        }
                    }
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
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblClaimNumber = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblClaimNumber");
            Label lblSalesCommissionClaimID = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblSalesCommissionClaimID");
            TextBox txtApproved1Amount = (TextBox)gvICTickets.Rows[gvRow.RowIndex].FindControl("txtApproved1Amount");
            TextBox txtApproved1Remarks = (TextBox)gvICTickets.Rows[gvRow.RowIndex].FindControl("txtApproved1Remarks");
            Label lblAmount = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblAmount");
            decimal Amount = Convert.ToDecimal(lblAmount.Text);
            decimal parsedValue;
            if (!decimal.TryParse(txtApproved1Amount.Text, out parsedValue))
            {
                lblMessage.Text = "Please Enter decimal value in approve amount !";
                txtApproved1Amount.Focus();
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (Amount < Convert.ToDecimal(txtApproved1Amount.Text))
            {
                lblMessage.Text = "Please enter approve amount less than or equal of claim amount";
                txtApproved1Amount.Focus();
                lblMessage.ForeColor = Color.Red;
                return;
            }
            string endPoint = "SalesCommission/ApproveClaim?SalesCommissionClaimID=" + Convert.ToInt64(lblSalesCommissionClaimID.Text) + "&ApproveLevel=1" + "&ApprovedAmount=" + Convert.ToDecimal(txtApproved1Amount.Text)
                + "&ApprovedRemarks=" + txtApproved1Remarks.Text;
            Approve(endPoint, lblClaimNumber.Text, Convert.ToInt64(lblSalesCommissionClaimID.Text));
        }

        void Approve(string endPoint, string  ClaimNumber,long SalesCommissionClaimID)
        {
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = "Claime number " + ClaimNumber + " is not approved";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = "Claime number " + ClaimNumber + " is approved";
            lblMessage.ForeColor = Color.Green;
            SDMS_WarrantyClaimHeader.RemoveAll(m => m.SalesCommissionClaimID == SalesCommissionClaimID);
            gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

        }
        protected void btnApproved2By_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblClaimNumber = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblClaimNumber");
            Label lblSalesCommissionClaimID = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblSalesCommissionClaimID");
            TextBox txtApproved2Amount = (TextBox)gvICTickets.Rows[gvRow.RowIndex].FindControl("txtApproved2Amount");
            TextBox txtApproved2Remarks = (TextBox)gvICTickets.Rows[gvRow.RowIndex].FindControl("txtApproved2Remarks");
            Label lblAmount = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblAmount");
            decimal parsedValue;
            if (!decimal.TryParse(txtApproved2Amount.Text, out parsedValue))
            {
                lblMessage.Text = "Please Enter decimal value in approve amount !";
                txtApproved2Amount.Focus(); 
                lblMessage.ForeColor = Color.Red;
                return;
            }
            decimal Amount = Convert.ToDecimal(lblAmount.Text);
            if (Amount < Convert.ToDecimal(txtApproved2Amount.Text))
            {
                lblMessage.Text = "Please enter approve amount less than or equal of claim amount";
                txtApproved2Amount.Focus(); 
                lblMessage.ForeColor = Color.Red;
                return;
            }
            string endPoint = "SalesCommission/ApproveClaim?SalesCommissionClaimID=" + Convert.ToInt64(lblSalesCommissionClaimID.Text) + "&ApproveLevel=2" + "&ApprovedAmount=" + Convert.ToDecimal(txtApproved2Amount.Text)
                + "&ApprovedRemarks=" + txtApproved2Remarks.Text;
            Approve(endPoint, lblClaimNumber.Text, Convert.ToInt64(lblSalesCommissionClaimID.Text));
        }
        protected void btnApproved3By_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblClaimNumber = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblClaimNumber");
            Label lblSalesCommissionClaimID = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblSalesCommissionClaimID");

            TextBox txtApproved3Amount = (TextBox)gvICTickets.Rows[gvRow.RowIndex].FindControl("txtApproved3Amount");
            TextBox txtApproved3Remarks = (TextBox)gvICTickets.Rows[gvRow.RowIndex].FindControl("txtApproved3Remarks");
            Label lblAmount = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblAmount");
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
            string endPoint = "SalesCommission/ApproveClaim?SalesCommissionClaimID=" + Convert.ToInt64(lblSalesCommissionClaimID.Text) + "&ApproveLevel=3" + "&ApprovedAmount=" + Convert.ToDecimal(txtApproved3Amount.Text)
           + "&ApprovedRemarks=" + txtApproved3Remarks.Text;
            Approve(endPoint, lblClaimNumber.Text, Convert.ToInt64(lblSalesCommissionClaimID.Text));
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        
    }
}