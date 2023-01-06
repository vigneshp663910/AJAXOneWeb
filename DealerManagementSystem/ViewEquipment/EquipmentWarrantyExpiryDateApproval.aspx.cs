using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewEquipment
{
    public partial class EquipmentWarrantyExpiryDateApproval : System.Web.UI.Page
    {
        public List<PWarrantyExpiryDateChangeApproval> EquipWarrantyExpiryDateChgReq
        {
            get
            {
                if (Session["EquipWarrantyExpiryDateChgReq"] == null)
                {
                    Session["EquipWarrantyExpiryDateChgReq"] = new List<PWarrantyExpiryDateChangeApproval>();
                }
                return (List<PWarrantyExpiryDateChangeApproval>)Session["EquipWarrantyExpiryDateChgReq"];
            }
            set
            {
                Session["EquipWarrantyExpiryDateChgReq"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Equipment Warranty Expiry Date Change Request');</script>");
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageEquipWarrantyExpiryDateChgReq.Visible = false;
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                txtWarrantyExpiryDateChgReqDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtWarrantyExpiryDateChgReqDateTo.Text = DateTime.Now.ToShortDateString();

                lblRowCountEquipWarrantyExpiryDateChgReq.Visible = false;
                ibtnArrowLeftEquipWarrantyExpiryDateChgReq.Visible = false;
                ibtnArrowRightEquipWarrantyExpiryDateChgReq.Visible = false;
            }
        }
        protected void btnSearchEquipWarrantyExpiryDateChgReq_Click(object sender, EventArgs e)
        {
            FillEquipmentWarrantyExpiryDateChangeReq();
        }
        //protected void btnViewEquipWarrantyExpiryDateChgReq(object sender, EventArgs e)
        //{
        //    FillEquipmentWarrantyExpiryDateChangeReq();
        //}
        private void FillEquipmentWarrantyExpiryDateChangeReq()
        {
            DateTime DateFrom = Convert.ToDateTime(txtWarrantyExpiryDateChgReqDateFrom.Text.Trim());
            DateTime DateTo = Convert.ToDateTime(txtWarrantyExpiryDateChgReqDateTo.Text.Trim());
            string EquipmentSerialNo = txtEquipmentSerialNoEquipWarrantyExpiryDateChgReq.Text.Trim();
            EquipWarrantyExpiryDateChgReq = new BDMS_Equipment().GetEquipmentWarrantyExpiryDateChangeForApproval(DateFrom, DateTo, EquipmentSerialNo);

            gvEquipWarrantyExpiryDateChgReq.DataSource = EquipWarrantyExpiryDateChgReq;
            gvEquipWarrantyExpiryDateChgReq.DataBind();

            if (EquipWarrantyExpiryDateChgReq.Count == 0)
            {
                lblRowCountEquipWarrantyExpiryDateChgReq.Visible = false;
                ibtnArrowLeftEquipWarrantyExpiryDateChgReq.Visible = false;
                ibtnArrowRightEquipWarrantyExpiryDateChgReq.Visible = false;
            }
            else
            {
                lblRowCountEquipWarrantyExpiryDateChgReq.Visible = true;
                ibtnArrowLeftEquipWarrantyExpiryDateChgReq.Visible = true;
                ibtnArrowRightEquipWarrantyExpiryDateChgReq.Visible = true;
                lblRowCountEquipWarrantyExpiryDateChgReq.Text = (((gvEquipWarrantyExpiryDateChgReq.PageIndex) * gvEquipWarrantyExpiryDateChgReq.PageSize) + 1) + " - " + (((gvEquipWarrantyExpiryDateChgReq.PageIndex) * gvEquipWarrantyExpiryDateChgReq.PageSize) + gvEquipWarrantyExpiryDateChgReq.Rows.Count) + " of " + EquipWarrantyExpiryDateChgReq.Count;
            }

        }
        protected void ibtnArrowLeftEquipWarrantyExpiryDateChgReq_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipWarrantyExpiryDateChgReq.PageIndex > 0)
            {
                gvEquipWarrantyExpiryDateChgReq.PageIndex = gvEquipWarrantyExpiryDateChgReq.PageIndex - 1;
                EquipWarrantyExpiryDateChgReqBind(gvEquipWarrantyExpiryDateChgReq, lblRowCountEquipWarrantyExpiryDateChgReq, EquipWarrantyExpiryDateChgReq);
            }
        }
        protected void ibtnArrowRightEquipWarrantyExpiryDateChgReq_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipWarrantyExpiryDateChgReq.PageCount > gvEquipWarrantyExpiryDateChgReq.PageIndex)
            {
                gvEquipWarrantyExpiryDateChgReq.PageIndex = gvEquipWarrantyExpiryDateChgReq.PageIndex + 1;
                EquipWarrantyExpiryDateChgReqBind(gvEquipWarrantyExpiryDateChgReq, lblRowCountEquipWarrantyExpiryDateChgReq, EquipWarrantyExpiryDateChgReq);
            }
        }

        protected void gvEquipWarrantyExpiryDateChgReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquipWarrantyExpiryDateChgReq.PageIndex = e.NewPageIndex;
            FillEquipmentWarrantyExpiryDateChangeReq();
        }
        void EquipWarrantyExpiryDateChgReqBind(GridView gv, Label lbl, List<PWarrantyExpiryDateChangeApproval> EquipWarrantyExpiryDateChgReq)
        {
            gv.DataSource = EquipWarrantyExpiryDateChgReq;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + EquipWarrantyExpiryDateChgReq.Count;
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblWarrantyExpiryDateChangeID = (Label)gvRow.FindControl("lblWarrantyExpiryDateChangeID");
            Label lblEquipmentHeaderID = (Label)gvRow.FindControl("lblEquipmentHeaderID");
            Boolean iSApprove = false;

            if (lbActions.Text == "Approve")
            {
                iSApprove = true;
            }
            if (new BDMS_Equipment().ApproveOrRejectEquipmentWarrantyExpiryDateChange(Convert.ToInt64(lblWarrantyExpiryDateChangeID.Text), Convert.ToInt64(lblEquipmentHeaderID.Text), PSession.User.UserID, iSApprove))
            {
                lblMessageEquipWarrantyExpiryDateChgReq.Text = "Equipment Warranty Type Change approved.";
                lblMessageEquipWarrantyExpiryDateChgReq.ForeColor = Color.Green;
                lblMessageEquipWarrantyExpiryDateChgReq.Visible = true;
            }
            else
            {
                lblMessageEquipWarrantyExpiryDateChgReq.Text = "Equipment Warranty Type Change rejected.";
                lblMessageEquipWarrantyExpiryDateChgReq.ForeColor = Color.Red;
                lblMessageEquipWarrantyExpiryDateChgReq.Visible = true;
            }
            FillEquipmentWarrantyExpiryDateChangeReq();
        }
        protected void btnViewEquipWarrantyExpiryDateChgReq_Click(object sender, EventArgs e)
        {
            divEquipWarrantyExpiryDateChgReqView.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblWarrantyExpiryDateChangeID = (Label)gvRow.FindControl("lblWarrantyExpiryDateChangeID");
            Label lblEquipmentHeaderID = (Label)gvRow.FindControl("lblEquipmentHeaderID");
            Label lblWarrantyExpiryDate = (Label)gvRow.FindControl("lblWarrantyExpiryDate");
            Session["WarrantyExpiryDateChangeID"] = lblWarrantyExpiryDateChangeID.Text;
            Session["EquipmentHeaderID"] = lblEquipmentHeaderID.Text;
            Session["NewWarrantyExpiryDate"] = lblWarrantyExpiryDate.Text;

            UC_EquipmentView.fillEquipment(Convert.ToInt64(lblEquipmentHeaderID.Text));
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divEquipWarrantyExpiryDateChgReqView.Visible = false;
            divList.Visible = true;
        }
    }
}