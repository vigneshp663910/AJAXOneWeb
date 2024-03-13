using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ApproveDeviatedCliamApprove : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ApproveDeviatedCliamApprove; } }
        protected void Page_PreInit(object sender, EventArgs e)
        { 
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            } 
        }
        public List<PClaimDeviation> Claim
        {
            get
            {
                if (Session["DMS_DeviatedCliamApprove"] == null)
                {
                    Session["DMS_DeviatedCliamApprove"] = new List<PClaimDeviation>();
                }
                return (List<PClaimDeviation>)Session["DMS_DeviatedCliamApprove"];
            }
            set
            {
                Session["DMS_DeviatedCliamApprove"] = value;
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
                txtRequestedDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtRequestedDateTo.Text = DateTime.Now.ToShortDateString();

                fillDealer();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillICTicket();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillICTicket()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                DateTime? ICTicketDateF = string.IsNullOrEmpty(txtRequestedDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDateFrom.Text.Trim());
                DateTime? ICTicketDateT = string.IsNullOrEmpty(txtRequestedDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDateTo.Text.Trim());
                 
                Claim = new BDMS_WarrantyClaim().GetDeviatedClaimForApproval(DealerID, txtICTicketNumber.Text.Trim(), txtClaimNumber.Text.Trim()
                    , txtRequestedDateFrom.Text.Trim(), txtRequestedDateTo.Text.Trim(), Convert.ToInt32(ddlDeviationType.SelectedValue)); 

                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = Claim;
                gvICTickets.DataBind();
                if (Claim.Count == 0)
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
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + Claim.Count;
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_DeviatedICTicketApprove", "fillICTicket", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.DataSource = Claim;
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;

                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + Claim.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.DataSource = Claim;
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + Claim.Count;
            }
        }



        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void lbApprove_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            Label lblClaimDeviationID = (Label)gvRow.FindControl("lblClaimDeviationID");

            PApiResult Results = new BDMS_WarrantyClaim().ApproveOrRejectDeviatedClaimRequest(Convert.ToInt64(lblClaimDeviationID.Text), true, null);

            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                return;
            }
            lblMessage.Text = "ICTicket  Approved successfully";
            lblMessage.ForeColor = Color.Green;
            fillICTicket();


            //if (new BDMS_WarrantyClaim().ApproveOrRejectDeviatedClaimRequest(Convert.ToInt64(gvICTickets.DataKeys[index].Value), true, null, PSession.User.UserID))
            //{
            //    lblMessage.Text = "ICTicket  Approved successfully";
            //    lblMessage.ForeColor = Color.Green;
            //    fillICTicket();
            //}
            //else
            //{
            //    lblMessage.Text = "ICTicket is not Approved successfully";
            //    lblMessage.ForeColor = Color.Red;
            //}

        }
        protected void lbReject_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            PApiResult Results = new BDMS_WarrantyClaim().ApproveOrRejectDeviatedClaimRequest(Convert.ToInt64(gvICTickets.DataKeys[index].Value), null, true);

            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                return;
            }
            lblMessage.Text = "ICTicket  Rejected successfully";
            lblMessage.ForeColor = Color.Green;
            fillICTicket();

            //if (new BDMS_WarrantyClaim().ApproveOrRejectDeviatedClaimRequest(Convert.ToInt64(gvICTickets.DataKeys[index].Value), null, true, PSession.User.UserID))
            //{
            //    lblMessage.Text = "ICTicket  Rejected successfully";
            //    lblMessage.ForeColor = Color.Green;
            //    fillICTicket();
            //}
            //else
            //{
            //    lblMessage.Text = "Invoice is not Rejected successfully";
            //    lblMessage.ForeColor = Color.Red;
            //}
        }

        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    long WarrantyInvoiceHeaderID = Convert.ToInt64(gvICTickets.DataKeys[e.Row.RowIndex].Value);
                    GridView gvICTicketItems = (GridView)e.Row.FindControl("gvICTicketItems");
                    List<PDMS_WarrantyInvoiceItem> ClaimItem = new List<PDMS_WarrantyInvoiceItem>();
                    ClaimItem = Claim.Find(s => s.WarrantyInvoiceHeaderID == WarrantyInvoiceHeaderID).InvoiceItems;

                    gvICTicketItems.DataSource = ClaimItem;
                    gvICTicketItems.DataBind();
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_DeviatedCliamApprove", "gvICTickets_RowDataBound", ex);
                throw ex;
            }
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.DataSource = Claim;
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + Claim.Count;
        }

    }
}