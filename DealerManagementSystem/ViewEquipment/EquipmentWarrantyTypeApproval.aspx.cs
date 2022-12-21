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
    public partial class EquipmentWarrantyTypeApproval : System.Web.UI.Page
    {
        public List<PEquipmentWarrantyTypeApproval> EquipWarrantyTypeChgReq
        {
            get
            {
                if (Session["EquipWarrantyTypeChgReq"] == null)
                {
                    Session["EquipWarrantyTypeChgReq"] = new List<PEquipmentWarrantyTypeApproval>();
                }
                return (List<PEquipmentWarrantyTypeApproval>)Session["EquipWarrantyTypeChgReq"];
            }
            set
            {
                Session["EquipWarrantyTypeChgReq"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Equipment');</script>");
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
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
                txtReqDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtReqDateTo.Text = DateTime.Now.ToShortDateString();

                lblRowCountEquipWarrantyTypeChgReq.Visible = false;
                ibtnArrowLeftEquipWarrantyTypeChgReq.Visible = false;
                ibtnArrowRightEquipWarrantyTypeChgReq.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillEquipmentWarrantTypeChangeReq();
        }
        private void FillEquipmentWarrantTypeChangeReq()
        {
            DateTime DateFrom = Convert.ToDateTime(txtReqDateFrom.Text.Trim());
            DateTime DateTo = Convert.ToDateTime(txtReqDateTo.Text.Trim());
            string EquipmentSerialNo = txtEquipmentSerialNo.Text.Trim();
            EquipWarrantyTypeChgReq = new BDMS_Equipment().GetEquipmentWarrantTypeChangeForApproval(DateFrom, DateTo, EquipmentSerialNo);

            gvEquipWarrantyTypeChgReq.DataSource = EquipWarrantyTypeChgReq;
            gvEquipWarrantyTypeChgReq.DataBind();

            if (EquipWarrantyTypeChgReq.Count == 0)
            {
                lblRowCountEquipWarrantyTypeChgReq.Visible = false;
                ibtnArrowLeftEquipWarrantyTypeChgReq.Visible = false;
                ibtnArrowRightEquipWarrantyTypeChgReq.Visible = false;
            }
            else
            {
                lblRowCountEquipWarrantyTypeChgReq.Visible = true;
                ibtnArrowLeftEquipWarrantyTypeChgReq.Visible = true;
                ibtnArrowRightEquipWarrantyTypeChgReq.Visible = true;
                lblRowCountEquipWarrantyTypeChgReq.Text = (((gvEquipWarrantyTypeChgReq.PageIndex) * gvEquipWarrantyTypeChgReq.PageSize) + 1) + " - " + (((gvEquipWarrantyTypeChgReq.PageIndex) * gvEquipWarrantyTypeChgReq.PageSize) + gvEquipWarrantyTypeChgReq.Rows.Count) + " of " + EquipWarrantyTypeChgReq.Count;
            }

        }
        protected void ibtnArrowLeftEquipWarrantyTypeChgReq_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipWarrantyTypeChgReq.PageIndex > 0)
            {
                gvEquipWarrantyTypeChgReq.PageIndex = gvEquipWarrantyTypeChgReq.PageIndex - 1;
                EquipWarrantyTypeChgReqBind(gvEquipWarrantyTypeChgReq, lblRowCountEquipWarrantyTypeChgReq, EquipWarrantyTypeChgReq);
            }
        }
        protected void ibtnArrowRightEquipWarrantyTypeChgReq_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipWarrantyTypeChgReq.PageCount > gvEquipWarrantyTypeChgReq.PageIndex)
            {
                gvEquipWarrantyTypeChgReq.PageIndex = gvEquipWarrantyTypeChgReq.PageIndex + 1;
                EquipWarrantyTypeChgReqBind(gvEquipWarrantyTypeChgReq, lblRowCountEquipWarrantyTypeChgReq, EquipWarrantyTypeChgReq);
            }
        }

        protected void gvEquipWarrantyTypeChgReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquipWarrantyTypeChgReq.PageIndex = e.NewPageIndex;
            FillEquipmentWarrantTypeChangeReq();
        }
        void EquipWarrantyTypeChgReqBind(GridView gv, Label lbl, List<PEquipmentWarrantyTypeApproval> Activity1)
        {
            gv.DataSource = EquipWarrantyTypeChgReq;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + EquipWarrantyTypeChgReq.Count;
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblWarrantyTypeChangeID = (Label)gvRow.FindControl("lblWarrantyTypeChangeID");
            Label lblEquipmentHeaderID = (Label)gvRow.FindControl("lblEquipmentHeaderID");
            Label lblEquipmentWarrantyTypeID = (Label)gvRow.FindControl("lblEquipmentWarrantyTypeID");
            Boolean iSApprove = false;

            if (lbActions.Text == "Approve")
            {
                iSApprove = true;
            }
            if (new BDMS_Equipment().ApproveOrRejectEquipmentWarrrantyTypeChange(Convert.ToInt64(lblWarrantyTypeChangeID.Text), Convert.ToInt64(lblEquipmentHeaderID.Text), Convert.ToInt64(lblEquipmentWarrantyTypeID.Text), PSession.User.UserID, iSApprove))
            {
                lblMessage.Text = "Equipment Warrranty Type Change approved succesfully.";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Equipment Warrranty Type Change rejected.";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
            FillEquipmentWarrantTypeChangeReq();
        }
        protected void btnViewEquipmentWarrantyTypeChange_Click(object sender, EventArgs e)
        {
            divEquipmentWarrantyTypeChangeView.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblWarrantyTypeChangeID = (Label)gvRow.FindControl("lblWarrantyTypeChangeID");
            Label lblEquipmentHeaderID = (Label)gvRow.FindControl("lblEquipmentHeaderID");
            Label lblEquipmentWarrantyTypeID = (Label)gvRow.FindControl("lblEquipmentWarrantyTypeID");
            Session["WarrantyTypeChangeID"] = lblWarrantyTypeChangeID.Text;
            Session["EquipmentHeaderID"] = lblEquipmentHeaderID.Text;
            Session["EquipmentWarrantyTypeID"] = lblEquipmentWarrantyTypeID.Text;
            
            UC_EquipmentView.fillEquipment(Convert.ToInt64(lblEquipmentHeaderID.Text));
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divEquipmentWarrantyTypeChangeView.Visible = false;
            divList.Visible = true;
        }
    }
}