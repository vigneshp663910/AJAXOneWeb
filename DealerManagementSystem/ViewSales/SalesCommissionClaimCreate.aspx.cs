using Business;
using DealerManagementSystem.ViewPreSale.UserControls;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSales
{
    public partial class SalesCommissionClaimCreate : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Sales Commission Claim Create');</script>"); 
            lblMessage.Text = ""; 
            if (!IsPostBack)
            {  
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillQuotation();
        }

        public List<PSalesQuotation> Quote
        {
            get
            {
                if (Session["SalesCommissionClaimCreate"] == null)
                {
                    Session["SalesCommissionClaimCreate"] = new List<PSalesQuotation>();
                }
                return (List<PSalesQuotation>)Session["SalesCommissionClaimCreate"];
            }
            set
            {
                Session["SalesCommissionClaimCreate"] = value;
            }
        }

        protected void ibtnQuoteArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvQuotation.PageIndex > 0)
            {
                gvQuotation.PageIndex = gvQuotation.PageIndex - 1;
                QuoteBind(gvQuotation, lblRowCount, Quote);
            }
        } 
        protected void ibtnQuoteArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvQuotation.PageCount > gvQuotation.PageIndex)
            {
                gvQuotation.PageIndex = gvQuotation.PageIndex + 1;
                QuoteBind(gvQuotation, lblRowCount, Quote);
            }
        }

        void QuoteBind(GridView gv, Label lbl, List<PSalesQuotation> Quote)
        {
            gv.DataSource = Quote;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Quote.Count;
        }


        void FillQuotation()
        {
            Quote = new BSalesCommissionClaim().GetSalesQuotationForClaimCreate(txtQuotation.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());
            gvQuotation.DataSource = Quote;
            gvQuotation.DataBind();
            if (Quote.Count == 0)
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
                lblRowCount.Text = (((gvQuotation.PageIndex) * gvQuotation.PageSize) + 1) + " - " + (((gvQuotation.PageIndex) * gvQuotation.PageSize) + gvQuotation.Rows.Count) + " of " + Quote.Count;
            }
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
            FillQuotation();
        }

        protected void btnViewQuotation_Click(object sender, EventArgs e)
        {
            divColdVisitView.Visible = true;
            btnBackToList.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblQuotationID = (Label)gvRow.FindControl("lblQuotationID");
            UC_QuotationView.fillViewQuotation(Convert.ToInt64(lblQuotationID.Text));
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            divColdVisitView.Visible = true;
            btnBackToList.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblQuotationID = (Label)gvRow.FindControl("lblQuotationID");

            lblMessage.Visible = true;

            string endPoint = "SalesCommission/InsertSalesCommissionClaim?SalesQuotationID=" + lblQuotationID.Text;

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = "Updated Successfully";
            lblMessage.ForeColor = Color.Green;
        }
    }
}