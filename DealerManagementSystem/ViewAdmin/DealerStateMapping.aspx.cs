using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class DealerStateMapping : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_DealerStateMapping; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Relate Dealer & State');</script>");

            if (!IsPostBack)
            {
                FillDealer(ddlDealerS);
                FillDealer(ddlDealerM);
                FillCountry(ddlCountryS);
                FillCountry(ddlCountryM);
                FillState(ddlCountryS, ddlStateS);
                FillState(ddlCountryM, ddlStateM);
            }
        }
        void FillDealer(DropDownList ddlDealer)
        {
            List<PDMS_Dealer> Dealer = new List<PDMS_Dealer>();
            new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithDisplayName", "DID");
        }
        void FillCountry(DropDownList ddlCountry)
        {
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");
            ddlCountry.SelectedValue = "1";
        }
        void FillState(DropDownList ddlCountry, DropDownList ddlState)
        {
            new DDLBind(ddlState, new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID", true, "Select State");
        }
        protected void ddlCountryS_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillState(ddlCountryS, ddlStateS);
        }
        protected void ddlCountryM_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillState(ddlCountryM, ddlStateM);
        }
        public List<PDealerStateMappingID> DealerStateMapping1
        {
            get
            {
                if (Session["PDealerStateMappingID"] == null)
                {
                    Session["PDealerStateMappingID"] = new List<PDealerStateMappingID>();
                }
                return (List<PDealerStateMappingID>)Session["PDealerStateMappingID"];
            }
            set
            {
                Session["PDealerStateMappingID"] = value;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillDealerState();
        }
        void FillDealerState()
        {
            int? DealerID = null, CountryID = null, StateID = null;

            if (ddlDealerS.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDealerS.SelectedValue))
            {
                DealerID = Convert.ToInt32(ddlDealerS.SelectedValue);
            }
            if (ddlCountryS.SelectedValue != "0")
            {
                CountryID = Convert.ToInt32(ddlCountryS.SelectedValue);
            }
            if (ddlStateS.SelectedValue != "0" && !string.IsNullOrEmpty(ddlStateS.SelectedValue))
            {
                StateID = Convert.ToInt32(ddlStateS.SelectedValue);
            }

            DealerStateMapping1 = new BDealer().GetDealerStateMapping(DealerID, CountryID, StateID);

            gvDealerState.DataSource = DealerStateMapping1;
            gvDealerState.DataBind();

            if (DealerStateMapping1.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnDSArrowLeft.Visible = false;
                ibtnDSArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnDSArrowLeft.Visible = true;
                ibtnDSArrowRight.Visible = true;
                lblRowCount.Text = (((gvDealerState.PageIndex) * gvDealerState.PageSize) + 1) + " - " + (((gvDealerState.PageIndex) * gvDealerState.PageSize) + gvDealerState.Rows.Count) + " of " + DealerStateMapping1.Count;
            }
        }
        protected void ibtnDSArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerState.PageIndex > 0)
            {
                gvDealerState.PageIndex = gvDealerState.PageIndex - 1;
                DSBind(gvDealerState, lblRowCount, DealerStateMapping1);
            }
        }
        protected void ibtnDSArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerState.PageCount > gvDealerState.PageIndex)
            {
                gvDealerState.PageIndex = gvDealerState.PageIndex + 1;
                DSBind(gvDealerState, lblRowCount, DealerStateMapping1);
            }
        }
        void DSBind(GridView gv, Label lbl, List<PDealerStateMappingID> DealerStateMapping)
        {
            gv.DataSource = DealerStateMapping1;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + DealerStateMapping1.Count;
        }
        protected void gvDealerState_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerState.PageIndex = e.NewPageIndex;
            FillDealerState();
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                lblMessage.Text = string.Empty;
                Boolean Success = true;
                int Result = 0;
                string Message = "";

                if (ddlDealerM.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please select the Dealer...!";
                    Success = false;
                }
                if (ddlCountryM.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please select the Country...!";
                    Success = false;
                }
                if (ddlStateM.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please select the State...!";
                    Success = false;
                }
                lblMessage.Text = Message;
                if (Success == false)
                {
                    return;
                }
                else
                {
                    Result = new BDealer().InsertOrUpdateDealerStateMapping(null, Convert.ToInt32(ddlDealerM.SelectedValue), Convert.ToInt32(ddlCountryM.SelectedValue), Convert.ToInt32(ddlStateM.SelectedValue), PSession.User.UserID, true);
                    if (Result == 1)
                    {
                        lblMessage.Text = "State mapped to Dealer successfully.";
                        lblMessage.ForeColor = Color.Green;
                        FillDealerState();
                    }
                    else if (Result == 2)
                    {
                        lblMessage.Text = "State already mapped to Dealer.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "State not mapped to Dealer successfully.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void lblDealerStateDelete_Click(object sender, EventArgs e)
        {
            int Result = 0;
            LinkButton lblDealerStateDelete = (LinkButton)sender;
            string DealerStateMappingID = lblDealerStateDelete.CommandArgument;
            Result = new BDealer().InsertOrUpdateDealerStateMapping(Convert.ToInt32(DealerStateMappingID), null, null, null, PSession.User.UserID, false);
            if (Result == 1)
            {
                lblMessage.Text = "Dealaer State mapping was deleted successfully.";
                lblMessage.ForeColor = Color.Green;
                FillDealerState();
            }
            else
            {
                lblMessage.Text = "Dealaer State mapping was not deleted successfully.";
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }
    }
}