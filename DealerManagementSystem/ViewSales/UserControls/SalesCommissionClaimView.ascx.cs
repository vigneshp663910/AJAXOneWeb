using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSales.UserControls
{
    public partial class SalesCommissionClaimView : System.Web.UI.UserControl
    { 
        public PSalesCommissionClaim Claim
        {
            get
            {
                if (Session["SalesCommissionClaimView"] == null)
                {
                    Session["SalesCommissionClaimView"] = new PSalesQuotation();
                }
                return (PSalesCommissionClaim)Session["SalesCommissionClaimView"];
            }
            set
            {
                Session["SalesCommissionClaimView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
        }
        public void FillMaster()
        {
            //FillGetDealerOffice();
            //new BDMS_IncoTerm().GetIncoTermDDL(ddlIncoterms, null, null);
            //new BDMS_Financier().GetFinancierDDL(ddlBankName, null, null); 

            
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Edit Quotation Basic Info")
            {

            }
        } 
      
        public void fillViewSalesCommission(long SalesCommissionClaimID)
        {
            Claim = new BSalesCommissionClaim().GetSalesCommissionClaimByID(SalesCommissionClaimID);
            if (Claim.SalesCommissionClaimID == 0)
            {
                lblMessage.Text = "Please Contact Administrator...!";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblClaimNumber.Text = Claim.ClaimNumber;
            lblClaimDate.Text = Claim.ClaimDate.ToLongDateString();

            lblRequestedBy.Text = Claim.CreatedBy.ContactName;
            lblDealer.Text = Claim.Dealer == null ? "" : Claim.Dealer.DealerName;
            lblApproved1By.Text = Claim.Approved1By == null ? "" : Claim.Approved1By.ContactName;
            lblApproved1On.Text = Claim.Approved1On == null ? "" : ((DateTime)Claim.Approved1On).ToLongDateString();
            lblApproved2By.Text = Claim.Approved2By == null ? "" : Claim.Approved2By.ContactName;
            lblApproved2On.Text = Claim.Approved2On == null ? "" : ((DateTime)Claim.Approved2On).ToLongDateString();
            //lblApproved3By.Text = Claim.FinanceVerifiedBy == null ? "" : Claim.FinanceVerifiedBy.ContactName;
            //lblApproved3On.Text = Claim.FinanceVerifiedOn == null ? "" : ((DateTime)Claim.FinanceVerifiedOn).ToLongDateString();
            //lblApproved3Remarks.Text = Claim.FinanceRemarks;
            lblInvoiceNumber.Text = Claim.Quotation.SalesInvoiceNumber;
            lblInvoiceDate.Text = ((DateTime)Claim.Quotation.SalesInvoiceDate).ToLongDateString();

            lblMaterial.Text = Claim.ClaimItem.Material.MaterialCode;
            lblMaterialDescription.Text = Claim.ClaimItem.Material.MaterialDescription;
            lblQty.Text = Convert.ToString(Claim.ClaimItem.Qty);
            lblAmount.Text = Convert.ToString(Claim.ClaimItem.Amount);
            lblBaseTax.Text = Convert.ToString(Claim.ClaimItem.BaseTax);
            lblApproved1Amount.Text = Convert.ToString(Claim.ClaimItem.Approved1Amount);
            lblApproved2Amount.Text = Convert.ToString(Claim.ClaimItem.Approved2Amount); 
            lblApproved1Remarks.Text = Claim.ClaimItem.Approved1Remarks;
            lblApproved2Remarks.Text = Claim.ClaimItem.Approved2Remarks;
            
            //fillCompetitor();

            //ActionControlMange();

            UC_LeadView.fillViewLead(Claim.Quotation.Lead);
            UC_SalesQuotationView.fillViewQuotation(Claim.Quotation);
            //fillSalesCommissionClaim();
        }
        
        public void fillProduct()
        {
            //gvProduct.DataSource = Quotation.QuotationItems;
            //gvProduct.DataBind();
        }
        public void fillCompetitor()
        {
            //gvCompetitor.DataSource = Quotation.Competitor;
            //gvCompetitor.DataBind();
        }
         
       
        void ActionControlMange()
        {
            
 

        }
       
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }

        
        void fillSalesCommissionClaim()
        {
             //List<PSalesCommissionClaim> claim = new BSalesCommissionClaim().GetSalesCommissionClaim(null, Quotation.QuotationID, null, null, null, null, null);
            //gvSalesCommission.DataSource = claim;
            //gvSalesCommission.DataBind();
            //gvSalesCommissionItem.DataSource = claim;
            //gvSalesCommissionItem.DataBind();

        }
    }
}