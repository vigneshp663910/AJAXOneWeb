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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Commission Claim » Create');</script>"); 
            lblMessage.Text = ""; 
            if (!IsPostBack)
            {
                //new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillQuotation();
        }

        public List<PSalesCommissionClaim> Quote
        {
            get
            {
                if (Session["SalesCommissionClaimCreate"] == null)
                {
                    Session["SalesCommissionClaimCreate"] = new List<PSalesCommissionClaim>();
                }
                return (List<PSalesCommissionClaim>)Session["SalesCommissionClaimCreate"];
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
                QuoteBind();
            }
        } 
        protected void ibtnQuoteArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvQuotation.PageCount > gvQuotation.PageIndex)
            {
                gvQuotation.PageIndex = gvQuotation.PageIndex + 1;
                QuoteBind();
            }
        }

        void QuoteBind()
        {
            gvQuotation.DataSource = Quote;
            gvQuotation.DataBind();
            lblRowCount.Text = (((gvQuotation.PageIndex) * gvQuotation.PageSize) + 1) + " - " + (((gvQuotation.PageIndex) * gvQuotation.PageSize) + gvQuotation.Rows.Count) + " of " + Quote.Count;
        }


        void FillQuotation()
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            Quote = new BSalesCommissionClaim().GetSalesQuotationForClaimCreate(DealerID,txtQuotation.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());
            gvQuotation.DataSource = Quote;
            gvQuotation.DataBind();

            foreach(GridViewRow r in gvQuotation.Rows)
            {
                TextBox txtAmount = (TextBox)r.FindControl("txtAmount");
                TextBox txtPercentage = (TextBox)r.FindControl("txtPercentage");
                if(Convert.ToDecimal("0"+txtAmount.Text) == 0)
                {
                    txtAmount.Enabled = false;
                }
                if (Convert.ToDecimal("0" + txtPercentage.Text) == 0)
                {
                    txtPercentage.Enabled = false;
                }
            }

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
            //divColdVisitView.Visible = true;
            //btnBackToList.Visible = true;
            //divList.Visible = false;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblQuotationID = (Label)gvRow.FindControl("lblQuotationID");

            

            int IsAmount = 0;
            decimal Value = 0;
            TextBox txtAmount = (TextBox)gvRow.FindControl("txtAmount");
            TextBox txtPercentage = (TextBox)gvRow.FindControl("txtPercentage");
            
            
            if (Convert.ToDecimal("0" + txtAmount.Text) != 0)
            {
                IsAmount = 1;
                Value = Convert.ToDecimal("0" + txtAmount.Text);
                Label lblAmount = (Label)gvRow.FindControl("lblAmount");
                if (Convert.ToDecimal(lblAmount.Text) < Value)
                {
                    lblMessage.Text = "Amount is not grater then " + lblAmount.Text;
                    return;
                }
            }
            else
            {
                IsAmount = 2;
                Value = Convert.ToDecimal("0" + txtPercentage.Text);
                Label lblPercentage = (Label)gvRow.FindControl("lblPercentage");
                if (Convert.ToDecimal(lblPercentage.Text) < Value)
                {
                    lblMessage.Text = "Percentage is not grater then "+ lblPercentage.Text;
                    return;
                }
            }


            string endPoint = "SalesCommission/InsertSalesCommissionClaim?SalesQuotationID=" + lblQuotationID.Text+ "&IsAmount="+ IsAmount + "&Value=" + Value;

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message; 
                return;
            }
            lblMessage.Text = "Updated Successfully";
            lblMessage.ForeColor = Color.Green;
            FillQuotation();
        }
    }
}