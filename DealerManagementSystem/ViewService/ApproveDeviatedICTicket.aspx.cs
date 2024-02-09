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
    public partial class ApproveDeviatedICTicket : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ApproveDeviatedICTicket; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_DeviatedICTicketApprove.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_ICTicket> ICTicket
        {
            get
            {
                if (Session["DMS_DeviatedICTicketApprove"] == null)
                {
                    Session["DMS_DeviatedICTicketApprove"] = new List<PDMS_ICTicket>();
                }
                return (List<PDMS_ICTicket>)Session["DMS_DeviatedICTicketApprove"];
            }
            set
            {
                Session["DMS_DeviatedICTicketApprove"] = value;
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
                int? DealerCode = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                DateTime? ICTicketDateF = string.IsNullOrEmpty(txtRequestedDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDateFrom.Text.Trim());
                DateTime? ICTicketDateT = string.IsNullOrEmpty(txtRequestedDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDateTo.Text.Trim());
                List<PDMS_ICTicket> SOIs = null;


                SOIs = new BDMS_ICTicket().GetDeviatedICTicketForApproval(DealerCode, txtICTicketNumber.Text.Trim(), Convert.ToInt32(ddlICTicketDeviationType.SelectedValue), ICTicketDateF, ICTicketDateT);

                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs
                                 join D in PSession.User.Dealer on S.Dealer.DealerCode equals D.UserName
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
                ICTicket = SOIs;

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
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
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
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;

                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
            }
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.DataSource = ICTicket;
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
        }

        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }



        //protected void lbView_Click(object sender, EventArgs e)
        //{
        //    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //    int index = gvRow.RowIndex;
        //    string url = "DMS_ICTicketView.aspx?TicketID=" + gvICTickets.DataKeys[index].Value.ToString();
        //    Response.Redirect(url);
        //}
        protected void lbApprove_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;


            if (new BDMS_ICTicket().ApproveOrRejectDeviatedICTicketRequest(Convert.ToInt64(gvICTickets.DataKeys[index].Value), true, null, PSession.User.UserID))
            {
                lblMessage.Text = "ICTicket  Approved successfully";
                lblMessage.ForeColor = Color.Green;
                fillICTicket();
            }
            else
            {
                lblMessage.Text = "ICTicket is not Approved successfully";
                lblMessage.ForeColor = Color.Red;
            }

        }
        protected void lbReject_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            if (new BDMS_ICTicket().ApproveOrRejectDeviatedICTicketRequest(Convert.ToInt64(gvICTickets.DataKeys[index].Value), null, true, PSession.User.UserID))
            {
                lblMessage.Text = "ICTicket  Rejected successfully";
                lblMessage.ForeColor = Color.Green;
                fillICTicket();
            }
            else
            {
                lblMessage.Text = "Invoice is not Rejected successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }

    }
}