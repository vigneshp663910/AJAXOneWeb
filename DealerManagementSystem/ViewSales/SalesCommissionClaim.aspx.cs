using Business;
using DealerManagementSystem.ViewPreSale.UserControls;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSales
{
    public partial class SalesCommissionClaim : System.Web.UI.Page
    {
        public List<PSalesCommissionClaim> Claim
        {
            get
            {
                if (Session["SalesCommissionClaim"] == null)
                {
                    Session["SalesCommissionClaim"] = new List<PSalesCommissionClaim>();
                }
                return (List<PSalesCommissionClaim>)Session["SalesCommissionClaim"];
            }
            set
            {
                Session["SalesCommissionClaim"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Commission » Claim');</script>");

            lblMessage.Text = "";
            if (!IsPostBack)
            {
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillClaim();
        }



        protected void ibtnQuoteArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvQuotation.PageIndex > 0)
            {
                gvQuotation.PageIndex = gvQuotation.PageIndex - 1;
                QuoteBind(gvQuotation, lblRowCount);
            }
        }


        protected void ibtnQuoteArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvQuotation.PageCount > gvQuotation.PageIndex)
            {
                gvQuotation.PageIndex = gvQuotation.PageIndex + 1;
                QuoteBind(gvQuotation, lblRowCount);
            }
        }
        void QuoteBind(GridView gv, Label lbl)
        {
            gv.DataSource = Claim;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Claim.Count;
        }
        void FillClaim()
        {
            long? SalesCommissionClaimID = null;
            long? SalesQuotationID = null;

            string ClaimNumber = txtClaimNumber.Text.Trim();
            string ClaimDateFrom = txtDateFrom.Text.Trim();
            string ClaimDateTo = txtDateTo.Text.Trim();
            int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue); ;
            Claim = new BSalesCommissionClaim().GetSalesCommissionClaim(SalesCommissionClaimID, SalesQuotationID, ClaimNumber, ClaimDateFrom, ClaimDateTo, StatusID, DealerID);
            gvQuotation.DataSource = Claim;
            gvQuotation.DataBind();

            if (Claim.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnQuoteArrowLeft.Visible = false;
                ibtnQuoteArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnQuoteArrowLeft.Visible = true;
                ibtnQuoteArrowRight.Visible = true;
                lblRowCount.Text = (((gvQuotation.PageIndex) * gvQuotation.PageSize) + 1) + " - " + (((gvQuotation.PageIndex) * gvQuotation.PageSize) + gvQuotation.Rows.Count) + " of " + Claim.Count;
            }
        }


        protected void lbViewCustomer_Click(object sender, EventArgs e)
        {
            //divCustomerView.Visible = true;
            //divColdVisitView.Visible = false;
            //btnBackToList.Visible = true;
            //divList.Visible = false;

            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //DropDownList ddlAction = (DropDownList)gvRow.FindControl("ddlAction");
            //Label lblCustomerID = (Label)gvRow.FindControl("lblCustomerID");
            //UC_CustomerView.fillCustomer(Convert.ToInt64(lblCustomerID.Text));
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divColdVisitView.Visible = false;
            btnBackToList.Visible = false;
            divList.Visible = true;
        }


        protected void gvLead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvQuotation.PageIndex = e.NewPageIndex;
            FillClaim();
        }

        protected void btnViewQuotation_Click(object sender, EventArgs e)
        {
            divColdVisitView.Visible = true;
            btnBackToList.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblSalesCommissionClaimID = (Label)gvRow.FindControl("lblSalesCommissionClaimID");
            UC_ClaimView.fillViewSalesCommission(Convert.ToInt64(lblSalesCommissionClaimID.Text));
        }

        protected void btnAddQuotation_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewPreSale/Lead.aspx");
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SalesCommisionClaimExportExcel(Claim, "Sales Commision Claim Report");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void SalesCommisionClaimExportExcel(List<PSalesCommissionClaim> SalesCommissionClaim, String Name)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Claim Number");
            dt.Columns.Add("Claim Date");
            dt.Columns.Add("Dealer");
            dt.Columns.Add("Material");
            dt.Columns.Add("Material Description");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Amount");
            dt.Columns.Add("BaseTax");
            dt.Columns.Add("Approved 1 Amount");
            dt.Columns.Add("Approved 1 By");
            dt.Columns.Add("Approved 1 On");
            dt.Columns.Add("Approved 1 Remark");
            dt.Columns.Add("Approved 2 Amount");
            dt.Columns.Add("Approved 2 By");
            dt.Columns.Add("Approved 2 On");
            dt.Columns.Add("Approved 2 Remark");
            dt.Columns.Add("Approved 3 Amount");
            dt.Columns.Add("Approved 3 By");
            dt.Columns.Add("Approved 3 On");
            dt.Columns.Add("Approved 3 Remark");
            dt.Columns.Add("Status");
            dt.Columns.Add("Invoice Number");
            dt.Columns.Add("Invoice Date");
            dt.Columns.Add("Created By");
            dt.Columns.Add("Created On");
            dt.Columns.Add("Modified By");
            dt.Columns.Add("Modified On");
            foreach (PSalesCommissionClaim CommissionClaim in SalesCommissionClaim)
            {
                dt.Rows.Add(
                    "'" + CommissionClaim.ClaimNumber
                    , CommissionClaim.ClaimDate
                    , (CommissionClaim.Dealer == null) ? "" : CommissionClaim.Dealer.DealerCode
                    , "'" + CommissionClaim.ClaimItem.Material.MaterialCode
                    , CommissionClaim.ClaimItem.Material.MaterialDescription
                    , CommissionClaim.ClaimItem.Qty
                    , CommissionClaim.ClaimItem.Amount
                    , CommissionClaim.ClaimItem.BaseTax
                    , CommissionClaim.ClaimItem.Approved1Amount
                    , CommissionClaim.Approved1By.ContactName
                    , CommissionClaim.Approved1On == null ? "" : CommissionClaim.Approved1On.ToString()
                    , CommissionClaim.ClaimItem.Approved1Remarks
                    , CommissionClaim.ClaimItem.Approved2Amount
                    , CommissionClaim.Approved2By.ContactName
                    , CommissionClaim.Approved2On == null ? "" : CommissionClaim.Approved2On.ToString()
                    , CommissionClaim.ClaimItem.Approved2Remarks
                    , CommissionClaim.ClaimItem.Approved3Amount
                    , CommissionClaim.Approved3By.ContactName
                    , CommissionClaim.Approved3On == null ? "" : CommissionClaim.Approved3On.ToString()
                    , CommissionClaim.ClaimItem.Approved3Remarks
                    , CommissionClaim.Status.Status
                    , (CommissionClaim.Quotation.SalesInvoiceNumber == null) ? "" : CommissionClaim.Quotation.SalesInvoiceNumber
                    , (CommissionClaim.Quotation.SalesInvoiceDate == null) ? "" : CommissionClaim.Quotation.SalesInvoiceDate.ToString()
                    , (CommissionClaim.CreatedBy == null) ? "" : CommissionClaim.CreatedBy.ContactName
                    //, CommissionClaim.CreatedOn
                    //, (CommissionClaim.ModifiedBy == null) ? "" : CommissionClaim.ModifiedBy.ContactName
                    //, CommissionClaim.ModifiedOn
                    , ""
                    , ""
                    , ""
                    );
            }
            try
            {
                new BXcel().ExporttoExcel(dt, Name);
            }
            catch
            {

            }
            finally
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>HideProgress();</script>");
            }
        }
    }
}