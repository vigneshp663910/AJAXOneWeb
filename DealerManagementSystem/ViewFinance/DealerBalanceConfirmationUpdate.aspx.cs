using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewFinance
{
    public partial class DealerBalanceConfirmationUpdate : BasePage
    {
        private DataTable DealerBalanceConfirmationToUpdate
        {
            get
            {
                if (ViewState["DealerBalanceConfirmationToUpdate"] == null)
                {
                    ViewState["DealerBalanceConfirmationToUpdate"] = new DataTable();
                }
                return (DataTable)ViewState["DealerBalanceConfirmationToUpdate"];
            }
            set
            {
                ViewState["DealerBalanceConfirmationToUpdate"] = value;
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
            lblMessage.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Finance » Dealer Balance Confirmation Update');</script>");
            if (!IsPostBack)
            {
                //new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithDisplayName", "DID", true, "All Dealer");
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                new DDLBind(ddlBalanceConfirmationStatus, new BDealer().GetDealerBalanceConfirmationStatus(null, null), "Status", "StatusID");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillDealerBalanceConfirmationToUpdate();
        }
        void FillDealerBalanceConfirmationToUpdate()
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            string DateFrom = txtFromDate.Text.Trim();
            string DateTo = txtToDate.Text.Trim(); 
            int? BalanceConfirmationStatusID = ddlBalanceConfirmationStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlBalanceConfirmationStatus.SelectedValue);

            DealerBalanceConfirmationToUpdate = new BDealer().GetDealerBalanceConfirmationToUpdate(DealerID, BalanceConfirmationStatusID, DateFrom, DateTo);
            gvDealerBalanceConfirmation.DataSource = DealerBalanceConfirmationToUpdate;
            gvDealerBalanceConfirmation.DataBind();

            if (DealerBalanceConfirmationToUpdate.Rows.Count == 0)
            {
                lblRowCountDealerBalConfirm.Visible = false;
                ibtnDealerBalConfirmArrowLeft.Visible = false;
                ibtnDealerBalConfirmArrowRight.Visible = false;
            }
            else
            {
                lblRowCountDealerBalConfirm.Visible = true;
                ibtnDealerBalConfirmArrowLeft.Visible = true;
                ibtnDealerBalConfirmArrowRight.Visible = true;
                DealerBalanceConfirmationToUpdateBind(gvDealerBalanceConfirmation, lblRowCountDealerBalConfirm, DealerBalanceConfirmationToUpdate);
            }
        }
        void DealerBalanceConfirmationToUpdateBind(GridView gv, Label lbl, DataTable DealerBC)
        {
            gv.DataSource = DealerBC;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + DealerBC.Rows.Count;
        }
        protected void gvDealerBalanceConfirmation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerBalanceConfirmation.PageIndex = e.NewPageIndex;
            FillDealerBalanceConfirmationToUpdate();
            gvDealerBalanceConfirmation.DataBind();
        }
        protected void ibtnDealerBalConfirmArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerBalanceConfirmation.PageIndex > 0)
            {
                gvDealerBalanceConfirmation.PageIndex = gvDealerBalanceConfirmation.PageIndex - 1;
                DealerBalanceConfirmationToUpdateBind(gvDealerBalanceConfirmation, lblRowCountDealerBalConfirm, DealerBalanceConfirmationToUpdate);
            }
        }
        protected void ibtnDealerBalConfirmArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerBalanceConfirmation.PageCount > gvDealerBalanceConfirmation.PageIndex)
            {
                gvDealerBalanceConfirmation.PageIndex = gvDealerBalanceConfirmation.PageIndex + 1;
                DealerBalanceConfirmationToUpdateBind(gvDealerBalanceConfirmation, lblRowCountDealerBalConfirm, DealerBalanceConfirmationToUpdate);
            }
        }
        protected void lnkBtnBalanceConfirmationEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtnBalanceConfirmationEdit = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(lnkBtnBalanceConfirmationEdit.NamingContainer);

            Label lblTotalOutstandingAsPerDealer = (Label)row.FindControl("lblTotalOutstandingAsPerDealer");
            Label lblBalanceConfirmationStatusG = (Label)row.FindControl("lblBalanceConfirmationStatusG");
            Label lblBalanceConfirmationStatusIDG = (Label)row.FindControl("lblBalanceConfirmationStatusIDG");
            
            TextBox txtTotalOutstandingAsPerDealer = (TextBox)row.FindControl("txtTotalOutstandingAsPerDealer");
            DropDownList ddlBalanceConfirmationStatusG = (DropDownList)row.FindControl("ddlBalanceConfirmationStatusG");

            lblTotalOutstandingAsPerDealer.Visible = false;
            lblBalanceConfirmationStatusG.Visible = false;

            txtTotalOutstandingAsPerDealer.Text = lblTotalOutstandingAsPerDealer.Text;
            txtTotalOutstandingAsPerDealer.Visible = true;
            new DDLBind(ddlBalanceConfirmationStatusG, new BDealer().GetDealerBalanceConfirmationStatus(null, null), "Status", "StatusID");
            ddlBalanceConfirmationStatusG.SelectedValue = (string.IsNullOrEmpty(lblBalanceConfirmationStatusIDG.Text)) ? "0" : lblBalanceConfirmationStatusIDG.Text;
            ddlBalanceConfirmationStatusG.Visible = true;

            Button btnUpdateBalanceConfirmation = (Button)row.FindControl("btnUpdateBalanceConfirmation");
            Button btnBack = (Button)row.FindControl("btnBack");
            btnUpdateBalanceConfirmation.Visible = true;
            btnBack.Visible = true;
            lnkBtnBalanceConfirmationEdit.Visible = false;
        }
        protected void btnUpdateBalanceConfirmation_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)(btn.NamingContainer);

            Label lblTotalOutstandingAsPerDealer = (Label)row.FindControl("lblTotalOutstandingAsPerDealer");
            Label lblBalanceConfirmationStatusG = (Label)row.FindControl("lblBalanceConfirmationStatusG");

            TextBox txtTotalOutstandingAsPerDealer = (TextBox)row.FindControl("txtTotalOutstandingAsPerDealer");
            DropDownList ddlBalanceConfirmationStatusG = (DropDownList)row.FindControl("ddlBalanceConfirmationStatusG");

            if (btn.ID == "btnBack")
            {
                lblTotalOutstandingAsPerDealer.Visible = true;
                lblBalanceConfirmationStatusG.Visible = true;

                txtTotalOutstandingAsPerDealer.Visible = false;
                ddlBalanceConfirmationStatusG.Visible = false;

                Button btnUpdateBalanceConfirmation = (Button)row.FindControl("btnUpdateBalanceConfirmation");
                Button btnBack = (Button)row.FindControl("btnBack");
                LinkButton lnkBtnBalanceConfirmationEdit = (LinkButton)row.FindControl("lnkBtnBalanceConfirmationEdit");
                btnUpdateBalanceConfirmation.Visible = false;
                btnBack.Visible = false;
                lnkBtnBalanceConfirmationEdit.Visible = true;
            }
            else
            {
                if(string.IsNullOrEmpty(txtTotalOutstandingAsPerDealer.Text.Trim()))
                {
                    lblMessage.Text = "Please enter the Total Outstanding Amount.";
                    txtTotalOutstandingAsPerDealer.BorderColor = Color.Red;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (ddlBalanceConfirmationStatusG.SelectedValue == "0")
                {
                    lblMessage.Text = "Please enter the Total Outstanding Amount.";
                    txtTotalOutstandingAsPerDealer.BorderColor = Color.Red;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                Label lblDealerBalanceConfirmationID = (Label)row.FindControl("lblDealerBalanceConfirmationID");

                string endPoint = "Dealer/UpdateDealerBalanceConfirmation?DealerBalanceConfirmationID=" + lblDealerBalanceConfirmationID.Text + "&TotalOutstandingAsPerDealer=" + txtTotalOutstandingAsPerDealer.Text + "&BalanceConfirmationStatusID=" + ddlBalanceConfirmationStatusG.SelectedValue;
                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));

                if (result.Status == PApplication.Failure)
                {
                    lblMessage.Text = "Dealer Balance Confirmation is not updated successfully.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Dealer Balance Confirmation is updated successfully.";

                    lblTotalOutstandingAsPerDealer.Visible = true;
                    lblBalanceConfirmationStatusG.Visible = true;

                    txtTotalOutstandingAsPerDealer.Visible = false;
                    ddlBalanceConfirmationStatusG.Visible = false;

                    lblTotalOutstandingAsPerDealer.Text = txtTotalOutstandingAsPerDealer.Text;
                    lblBalanceConfirmationStatusG.Text = ddlBalanceConfirmationStatusG.SelectedValue;

                    Button btnUpdateBalanceConfirmation = (Button)row.FindControl("btnUpdateBalanceConfirmation");
                    Button btnBack = (Button)row.FindControl("btnBack");
                    LinkButton lnkBtnBalanceConfirmationEdit = (LinkButton)row.FindControl("lnkBtnBalanceConfirmationEdit");
                    btnUpdateBalanceConfirmation.Visible = false;
                    btnBack.Visible = false;
                    lnkBtnBalanceConfirmationEdit.Visible = true;
                }
            }
        }
    }
}