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
    public partial class SalesCommissionClaimInvoiceView : System.Web.UI.UserControl
    { 
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
         
        public void fillViewSalesCommissionClaimInvoice(long SalesCommissionClaimInvoiceID)
        {
            

            PSalesCommissionClaimInvoice Invoice = new BSalesCommissionClaim().GetSalesCommissionClaimInvoiceByID(SalesCommissionClaimInvoiceID);
            if (Invoice.SalesCommissionClaimInvoiceID == 0)
            {
                lblMessage.Text = "Please Contact Administrator...!";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }

            PApiResult Result = new BSalesCommissionClaim().GetSalesCommissionClaimInvoice(SalesCommissionClaimInvoiceID, null, Invoice.InvoiceNumber, null, null, null, 1, gvClaimInvoice.PageSize);
            List<PSalesCommissionClaimInvoice> Invoices = JsonConvert.DeserializeObject<List<PSalesCommissionClaimInvoice>>(JsonConvert.SerializeObject(Result.Data));

            gvClaimInvoice.DataSource = Invoices;
            gvClaimInvoice.DataBind();
            gvClaimInvoiceItem.DataSource = Invoices;
            gvClaimInvoiceItem.DataBind();

            PSalesCommissionClaim Claim = new BSalesCommissionClaim().GetSalesCommissionClaimByID(Invoice.Claim.SalesCommissionClaimID);
            if (Claim.SalesCommissionClaimID == 0)
            {
                lblMessage.Text = "Please Contact Administrator...!";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }

            fillViewSalesCommission(Claim);

            UC_LeadView.fillViewLead(Claim.Quotation.Lead);
            UC_SalesQuotationView.fillViewQuotation(Claim.Quotation); 
        }
         
        public void fillViewSalesCommission(PSalesCommissionClaim Claim)
        {             
            lblClaimNumber.Text = Claim.ClaimNumber;
            lblClaimDate.Text = Claim.ClaimDate.ToLongDateString();

            lblRequestedBy.Text = Claim.CreatedBy.ContactName;
            lblDealer.Text = Claim.Dealer == null ? "" : Claim.Dealer.DealerName;
            lblApproved1By.Text = Claim.Approved1By == null ? "" : Claim.Approved1By.ContactName;
            lblApproved1On.Text = Claim.Approved1On == null ? "" : ((DateTime)Claim.Approved1On).ToLongDateString();
            lblApproved2By.Text = Claim.Approved2By == null ? "" : Claim.Approved2By.ContactName;
            lblApproved2On.Text = Claim.Approved2On == null ? "" : ((DateTime)Claim.Approved2On).ToLongDateString();
          
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
        }
         
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
         
    }
}