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
    public partial class EquipmentChangeApproval : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewEquipment_EquipmentChangeApproval; } }
        public DataTable EquipChgReq
        {
            get
            {
                if (Session["EquipChgReq"] == null)
                {
                    Session["EquipChgReq"] = new DataTable();
                }
                return (DataTable)Session["EquipChgReq"];
            }
            set
            {
                Session["EquipChgReq"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Equipment Change Request');</script>");
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            } 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false; 
            if (!IsPostBack)
            {
                txtReqDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtReqDateTo.Text = DateTime.Now.ToShortDateString();

                lblRowCountEquipChgReq.Visible = false;
                ibtnArrowLeftEquipChgReq.Visible = false;
                ibtnArrowRightEquipChgReq.Visible = false;
                Session["ChangeID"] = null;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillEquipmentChangeReq();
        }
        private void fillEquipmentChangeReq()
        {
            DateTime DateFrom = Convert.ToDateTime(txtReqDateFrom.Text.Trim());
            DateTime DateTo = Convert.ToDateTime(txtReqDateTo.Text.Trim());
            string EquipmentSerialNo = txtEquipmentSerialNo.Text.Trim();
            EquipChgReq = new BDMS_Equipment().GetEquipmentChangeForApproval(DateFrom, DateTo, EquipmentSerialNo);

            gvEquipChgReq.DataSource = EquipChgReq;
            gvEquipChgReq.DataBind();

            if (EquipChgReq.Rows.Count == 0)
            {
                lblRowCountEquipChgReq.Visible = false;
                ibtnArrowLeftEquipChgReq.Visible = false;
                ibtnArrowRightEquipChgReq.Visible = false;
            }
            else
            {
                lblRowCountEquipChgReq.Visible = true;
                ibtnArrowLeftEquipChgReq.Visible = true;
                ibtnArrowRightEquipChgReq.Visible = true;
                lblRowCountEquipChgReq.Text = (((gvEquipChgReq.PageIndex) * gvEquipChgReq.PageSize) + 1) + " - " + (((gvEquipChgReq.PageIndex) * gvEquipChgReq.PageSize) + gvEquipChgReq.Rows.Count) + " of " + EquipChgReq.Rows.Count;
            }

        }
        protected void ibtnArrowLeftEquipChgReq_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipChgReq.PageIndex > 0)
            {
                gvEquipChgReq.PageIndex = gvEquipChgReq.PageIndex - 1;
                EquipChgReqBind(gvEquipChgReq, lblRowCountEquipChgReq, EquipChgReq);
            }
        }
        protected void ibtnArrowRightEquipChgReq_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipChgReq.PageCount > gvEquipChgReq.PageIndex)
            {
                gvEquipChgReq.PageIndex = gvEquipChgReq.PageIndex + 1;
                EquipChgReqBind(gvEquipChgReq, lblRowCountEquipChgReq, EquipChgReq);
            }
        }

        protected void gvEquipChgReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquipChgReq.PageIndex = e.NewPageIndex;
            fillEquipmentChangeReq();
        }
        void EquipChgReqBind(GridView gv, Label lbl, DataTable EquipChgReq)
        {
            gv.DataSource = EquipChgReq;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + EquipChgReq.Rows.Count;
        }
        protected void btnViewEquipmentChange_Click(object sender, EventArgs e)
        {
            divEquipmentChangeView.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblChangeID = (Label)gvRow.FindControl("lblChangeID");
            Label lblEquipmentHeaderID = (Label)gvRow.FindControl("lblEquipmentHeaderID"); 
            Session["ChangeID"] = lblChangeID.Text;
            UC_EquipmentView.fillEquipment(Convert.ToInt64(lblEquipmentHeaderID.Text));
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divEquipmentChangeView.Visible = false;
            divList.Visible = true;
        }
    }
}
