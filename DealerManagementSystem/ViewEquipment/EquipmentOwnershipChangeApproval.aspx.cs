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

namespace DealerManagementSystem.ViewEquipment
{
    public partial class EquipmentOwnershipChangeApproval : System.Web.UI.Page
    {
        public List<PEquipmentOwnershipChangeApproval> EquipOwnershipChgReq
        {
            get
            {
                if (Session["EquipOwnershipChgReq"] == null)
                {
                    Session["EquipOwnershipChgReq"] = new List<PEquipmentOwnershipChangeApproval>();
                }
                return (List<PEquipmentOwnershipChangeApproval>)Session["EquipOwnershipChgReq"];
            }
            set
            {
                Session["EquipOwnershipChgReq"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Equipment Ownership Change Request');</script>");
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageEquipOwnershipChgReq.Visible = false;
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                txtOwnershipChgReqDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtOwnershipChgReqDateTo.Text = DateTime.Now.ToShortDateString();

                lblRowCountEquipOwnershipChgReq.Visible = false;
                ibtnArrowLeftEquipOwnershipChgReq.Visible = false;
                ibtnArrowRightEquipOwnershipChgReq.Visible = false;
            }
        }

        protected void btnSearchEquipOwnershipChgReq_Click(object sender, EventArgs e)
        {
            FillEquipmentOwnershipChangeReq();
        }

        //protected void btnViewEquipOwnershipChgReq(object sender, EventArgs e)
        //{
        //    FillEquipmentOwnershipChangeReq();
        //}
        private void FillEquipmentOwnershipChangeReq()
        {
            DateTime DateFrom = Convert.ToDateTime(txtOwnershipChgReqDateFrom.Text.Trim());
            DateTime DateTo = Convert.ToDateTime(txtOwnershipChgReqDateTo.Text.Trim());
            string EquipmentSerialNo = txtEquipmentSerialNoEquipOwnershipChgReq.Text.Trim();
            EquipOwnershipChgReq = new BDMS_Equipment().GetEquipmentOwnershipChangeForApproval(DateFrom, DateTo, EquipmentSerialNo);

            gvEquipOwnershipChgReq.DataSource = EquipOwnershipChgReq;
            gvEquipOwnershipChgReq.DataBind();

            //if (EquipOwnershipChgReq.Count == 0)
            //{
            //    lblRowCountEquipOwnershipChgReq.Visible = false;
            //    ibtnArrowLeftEquipOwnershipChgReq.Visible = false;
            //    ibtnArrowRightEquipOwnershipChgReq.Visible = false;
            //}
            //else
            //{
                lblRowCountEquipOwnershipChgReq.Visible = true;
                ibtnArrowLeftEquipOwnershipChgReq.Visible = true;
                ibtnArrowRightEquipOwnershipChgReq.Visible = true;
                lblRowCountEquipOwnershipChgReq.Text = (((gvEquipOwnershipChgReq.PageIndex) * gvEquipOwnershipChgReq.PageSize) + 1) + " - " + (((gvEquipOwnershipChgReq.PageIndex) * gvEquipOwnershipChgReq.PageSize) + gvEquipOwnershipChgReq.Rows.Count) + " of " + EquipOwnershipChgReq.Count;
            //}

        }
        protected void ibtnArrowLeftEquipOwnershipChgReq_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipOwnershipChgReq.PageIndex > 0)
            {
                gvEquipOwnershipChgReq.PageIndex = gvEquipOwnershipChgReq.PageIndex - 1;
                EquipOwnershipChgReqBind(gvEquipOwnershipChgReq, lblRowCountEquipOwnershipChgReq, EquipOwnershipChgReq);
            }
        }
        protected void ibtnArrowRightEquipOwnershipChgReq_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipOwnershipChgReq.PageCount > gvEquipOwnershipChgReq.PageIndex)
            {
                gvEquipOwnershipChgReq.PageIndex = gvEquipOwnershipChgReq.PageIndex + 1;
                EquipOwnershipChgReqBind(gvEquipOwnershipChgReq, lblRowCountEquipOwnershipChgReq, EquipOwnershipChgReq);
            }
        }

        protected void gvEquipOwnershipChgReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquipOwnershipChgReq.PageIndex = e.NewPageIndex;
            FillEquipmentOwnershipChangeReq();
        }
        void EquipOwnershipChgReqBind(GridView gv, Label lbl, List<PEquipmentOwnershipChangeApproval> EquipOwnershipChgReq)
        {
            gv.DataSource = EquipOwnershipChgReq;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + EquipOwnershipChgReq.Count;
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblOwnershipChangeID = (Label)gvRow.FindControl("lblOwnershipChangeID");
            Label lblEquipmentHeaderID = (Label)gvRow.FindControl("lblEquipmentHeaderID");
            Boolean iSApprove = false;

            if (lbActions.Text == "Approve")
            {
                iSApprove = true;
            }
            if (new BDMS_Equipment().ApproveOrRejectEquipmentOwnershipChange(Convert.ToInt64(lblOwnershipChangeID.Text), Convert.ToInt64(lblEquipmentHeaderID.Text), PSession.User.UserID, iSApprove))
            {
                lblMessageEquipOwnershipChgReq.Text = "Equipment Ownership Change approved succesfully.";
                lblMessageEquipOwnershipChgReq.ForeColor = Color.Green;
                lblMessageEquipOwnershipChgReq.Visible = true;
            }
            else
            {
                lblMessageEquipOwnershipChgReq.Text = "Equipment Ownership Change rejected.";
                lblMessageEquipOwnershipChgReq.ForeColor = Color.Red;
                lblMessageEquipOwnershipChgReq.Visible = true;
            }
            FillEquipmentOwnershipChangeReq();
        }
        protected void btnViewEquipOwnershipChgReq_Click(object sender, EventArgs e)
        {
            divEquipOwnershipChgReqView.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblOwnershipChangeID = (Label)gvRow.FindControl("lblOwnershipChangeID");
            Label lblEquipmentHeaderID = (Label)gvRow.FindControl("lblEquipmentHeaderID");
            Label lblOwnershipChgReqCustomerID = (Label)gvRow.FindControl("lblOwnershipChgReqCustomerID");
            Session["OwnershipChangeID"] = lblOwnershipChangeID.Text;
            Session["EquipmentHeaderID"] = lblEquipmentHeaderID.Text;
            Session["OwnershipChgReqCustomerID"] = lblOwnershipChgReqCustomerID.Text;

            UC_EquipmentView.fillEquipment(Convert.ToInt64(lblEquipmentHeaderID.Text));
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divEquipOwnershipChgReqView.Visible = false;
            divList.Visible = true;
        }
    }
}