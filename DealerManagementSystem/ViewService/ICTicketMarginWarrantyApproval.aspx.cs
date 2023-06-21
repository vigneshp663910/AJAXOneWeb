using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketMarginWarrantyApproval : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ICTicketMarginWarrantyApproval; } }
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Margin Warranty Approval');</script>");
            lblMessageMarginWarrantyApproval.Visible = false;
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if(!IsPostBack)
            {
                txtRequestedDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtRequestedDateTo.Text = DateTime.Now.ToShortDateString();
                
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID, new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0].DID.ToString()));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }
                lblRowCountMarginWarrantyChangeReq.Visible = false;
                ibtnMarginWarrantyChangeReqArrowLeft.Visible = false;
                ibtnMarginWarrantyChangeReqArrowRight.Visible = false;
            }
        }
        private void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillMarginWarrantyChangeReq();
            }
            catch (Exception e1)
            {
                lblMessageMarginWarrantyApproval.Text = e1.ToString();
                lblMessageMarginWarrantyApproval.ForeColor = Color.Red;
                lblMessageMarginWarrantyApproval.Visible = true;
            }
        }

        private void fillMarginWarrantyChangeReq()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                string ICTicketNumber = txtICTicket.Text.Trim();
                DateTime? MarginWarrantyRequestedDateFrom = string.IsNullOrEmpty(txtRequestedDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDateFrom.Text.Trim());
                DateTime? MarginWarrantyRequestedDateTo = string.IsNullOrEmpty(txtRequestedDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDateTo.Text.Trim());
                int RowCount = 0;
                
                DataTable MarginWarrantyChangeReq = new BDMS_ICTicket().GetMarginWarrantyChangeForApproval(DealerID, null, null, ICTicketNumber, PSession.User.UserID, PageIndex, gvMarginWarrantyChangeReq.PageSize, out RowCount);

                gvMarginWarrantyChangeReq.PageIndex = 0;
                gvMarginWarrantyChangeReq.DataSource = MarginWarrantyChangeReq;
                gvMarginWarrantyChangeReq.DataBind();
                
                if (RowCount == 0)
                {
                    lblRowCountMarginWarrantyChangeReq.Visible = false;
                    ibtnMarginWarrantyChangeReqArrowLeft.Visible = false;
                    ibtnMarginWarrantyChangeReqArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (RowCount + gvMarginWarrantyChangeReq.PageSize - 1) / gvMarginWarrantyChangeReq.PageSize;
                    lblRowCountMarginWarrantyChangeReq.Visible = true;
                    ibtnMarginWarrantyChangeReqArrowLeft.Visible = true;
                    ibtnMarginWarrantyChangeReqArrowRight.Visible = true;
                    lblRowCountMarginWarrantyChangeReq.Text = (((PageIndex - 1) * gvMarginWarrantyChangeReq.PageSize) + 1) + " - " + (((PageIndex - 1) * gvMarginWarrantyChangeReq.PageSize) + gvMarginWarrantyChangeReq.Rows.Count) + " of " + RowCount;
                }
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("ICTicketMarginWarrantyApproval", "fillMarginWarrantyChangeReq", e1);
                throw e1;
            }
        }
        protected void ibtnMarginWarrantyChangeReqArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillMarginWarrantyChangeReq();
            }
        }
        protected void ibtnMarginWarrantyChangeReqArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillMarginWarrantyChangeReq();
            }
        }
        protected void lbView_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            ViewState["ICTicketID"] = gvMarginWarrantyChangeReq.DataKeys[index].Value.ToString();
            divList.Visible = false;
            divMarginWarrantyApprovalView.Visible = true;
            UC_ICTicketView.FillICTicket(Convert.ToInt64(ViewState["ICTicketID"]));
            Label lblMarginWarrantyChangeID = (Label)gvRow.FindControl("lblMarginWarrantyChangeID");
            Session["MarginWarrantyChangeID"] = lblMarginWarrantyChangeID.Text;
            //UC_ICTicketBasicInformation.FillBasicInformation();
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divMarginWarrantyApprovalView.Visible = false;
            fillMarginWarrantyChangeReq();
        }
    }
}