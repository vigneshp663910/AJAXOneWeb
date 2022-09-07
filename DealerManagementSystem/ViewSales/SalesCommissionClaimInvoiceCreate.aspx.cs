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

namespace DealerManagementSystem.ViewSales
{
    public partial class SalesCommissionClaimInvoiceCreate : System.Web.UI.Page
    {
        public List<PSalesCommissionClaim> Claim
        {
            get
            {
                if (Session["SalesCommissionClaimInvoiceCreate"] == null)
                {
                    Session["SalesCommissionClaimInvoiceCreate"] = new List<PSalesCommissionClaim>();
                }
                return (List<PSalesCommissionClaim>)Session["SalesCommissionClaimInvoiceCreate"];
            }
            set
            {
                Session["SalesCommissionClaimInvoiceCreate"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Commission Claim » Invoice Create');</script>");

            lblMessage.Text = "";
            if (!IsPostBack)
            {

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
            string ClaimNumber = txtClaimNumber.Text.Trim();
            string ClaimDateFrom = txtDateFrom.Text.Trim();
            string ClaimDateTo = txtDateTo.Text.Trim(); 
            int? DealerID = null;
            Claim = new BSalesCommissionClaim().GetSalesCommissionClaimForInvoiceCreate(DealerID, ClaimNumber, ClaimDateFrom, ClaimDateTo);
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

        protected void btnCreateInvoice_Click(object sender, EventArgs e)
        {   
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblSalesCommissionClaimID = (Label)gvRow.FindControl("lblSalesCommissionClaimID");

            lblMessage.Visible = true;

            string endPoint = "SalesCommission/CreateClaimInvoice?SalesCommissionClaimID=" + lblSalesCommissionClaimID.Text;

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
        
              new BDMS_EInvoice().GeneratEInvoiceForSalesCommissionClaimInvoice(Convert.ToInt64(Results.Data));

            lblMessage.Text = "Updated Successfully";
            lblMessage.ForeColor = Color.Green;
            FillClaim();
        }
    }
}