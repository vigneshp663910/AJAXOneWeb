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
    public partial class DealerServiceConfiguration : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_DealerServiceConfiguration; } }
        public List<PDMS_District> DealerServiceDistrict
        {
            get
            {
                if (ViewState["PDMS_District_Service"] == null)
                {
                    ViewState["PDMS_District_Service"] = new List<PDMS_District>();
                }
                return (List<PDMS_District>)ViewState["PDMS_District_Service"];
            }
            set
            {
                ViewState["PDMS_District_Service"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Dealer Service Configuration');</script>");

            if (!IsPostBack)
            {
                try
                {
                    FillCountryDLL(ddlDServiceCountry);
                    FillDealerDLL(ddlDServiceDealer);
                    DealerServiceDistrict = new BDMS_Address().GetDistrict(null, null, null, null, null, null, "true");
                }
                catch (Exception Ex)
                {
                    lblMessageDealerServiceDistrict.Visible = true;
                    lblMessageDealerServiceDistrict.Text = Ex.ToString();
                    lblMessageDealerServiceDistrict.ForeColor = Color.Red;
                }
            }
        }
        private void FillCountryDLL(DropDownList ddl)
        {
            try
            {
                ddl.DataSource = new BDMS_Address().GetCountry(null, null);
                ddl.DataValueField = "CountryID";
                ddl.DataTextField = "Country";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select Country", "0"));
            }
            catch (Exception Ex)
            {
                lblMessageDealerServiceDistrict.Visible = true;
                lblMessageDealerServiceDistrict.Text = Ex.ToString();
                lblMessageDealerServiceDistrict.ForeColor = Color.Red;
            }
        }
        protected void ddlDServiceCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
              FillRegionDLL(ddlDServiceRegion, Convert.ToInt32(ddlDServiceCountry.SelectedValue), null, null);
        }
        private void FillRegionDLL(DropDownList ddl, int? CountryID, int? RegionID, string Region)
        {
            try
            {
                ddl.DataSource = new BDMS_Address().GetRegion(CountryID, RegionID, Region);
                ddl.DataValueField = "RegionID";
                ddl.DataTextField = "Region";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select Region", "0"));
            }
            catch (Exception Ex)
            {
                lblMessageDealerServiceDistrict.Visible = true;
                lblMessageDealerServiceDistrict.Text = Ex.ToString();
                lblMessageDealerServiceDistrict.ForeColor = Color.Red;
            }
        }
        protected void ddlDServiceRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
             FillStateDLL(ddlDServiceState, null, Convert.ToInt32(ddlDServiceCountry.SelectedValue), Convert.ToInt32(ddlDServiceRegion.SelectedValue), null, null);
            ddlDServiceDistrict.Items.Clear();
            ddlDServiceDistrict.SelectedValue = "0";
        }
        private void FillStateDLL(DropDownList ddl, int? DealerID, int? CountryID, int? RegionID, int? StateID, string State)
        {
            try
            {
                ddl.DataSource = new BDMS_Address().GetState(DealerID, CountryID, RegionID, StateID, State);
                ddl.DataValueField = "StateID";
                ddl.DataTextField = "State";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select State", "0"));
            }
            catch (Exception Ex)
            {
                lblMessageDealerServiceDistrict.Visible = true;
                lblMessageDealerServiceDistrict.Text = Ex.ToString();
                lblMessageDealerServiceDistrict.ForeColor = Color.Red;
            }
        }
        protected void ddlDServiceState_SelectedIndexChanged(object sender, EventArgs e)
        {
             FillDistrictDLL(ddlDServiceDistrict, Convert.ToInt32(ddlDServiceCountry.SelectedValue), Convert.ToInt32(ddlDServiceRegion.SelectedValue), Convert.ToInt32(ddlDServiceState.SelectedValue), null, null);
        }
        private void FillDistrictDLL(DropDownList ddl, int? CountryID, int? RegionID, int? StateID, int? DistrictID, string District)
        {
            try
            {
                ddl.DataSource = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID, null, null, "true");
                ddl.DataValueField = "DistrictID";
                ddl.DataTextField = "District";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select District", "0"));
            }
            catch (Exception Ex)
            {
                lblMessageDealerServiceDistrict.Visible = true;
                lblMessageDealerServiceDistrict.Text = Ex.ToString();
                lblMessageDealerServiceDistrict.ForeColor = Color.Red;
            }
        }
        
         private void FillDealerDLL(DropDownList ddl)
        {
            try
            {
                ddl.DataValueField = "DID";
                ddl.DataTextField = "CodeWithDisplayName";
                ddl.DataSource = PSession.User.Dealer;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select Dealer", "0"));
            }
            catch (Exception Ex)
            {
                lblMessageDealerServiceDistrict.Visible = true;
                lblMessageDealerServiceDistrict.Text = Ex.ToString();
                lblMessageDealerServiceDistrict.ForeColor = Color.Red;
            }
        }
        protected void BtnSearchDealerServiceDistrict_Click(object sender, EventArgs e)
        {
            FillGridDealerServiceDistrict();
        }
        private void FillGridDealerServiceDistrict()
        {
            try
            {
                int? CountryID = null, RegionID = null, StateID = null, DistrictID = null, ServiceDealerID = null;
                if (ddlDServiceCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlDServiceCountry.SelectedValue);
                }
                if (ddlDServiceRegion.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDServiceRegion.SelectedValue))
                {
                    RegionID = Convert.ToInt32(ddlDServiceRegion.SelectedValue);
                }
                if (ddlDServiceState.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDServiceState.SelectedValue))
                {
                    StateID = Convert.ToInt32(ddlDServiceState.SelectedValue);
                }
                if (ddlDServiceDistrict.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDServiceDistrict.SelectedValue))
                {
                    DistrictID = Convert.ToInt32(ddlDServiceDistrict.SelectedValue);
                }
                if (ddlDServiceDealer.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDServiceDealer.SelectedValue))
                {
                    ServiceDealerID = Convert.ToInt32(ddlDServiceDealer.SelectedValue);
                }
                DealerServiceDistrict = new BDMS_Address().GetDealerServiceDistrict(CountryID, RegionID, StateID, DistrictID, ServiceDealerID);
                if (DealerServiceDistrict.Count == 0)
                {
                    DealerServiceDistrict.Add(new PDMS_District());
                }
                gvDealerServiceDistrict.DataSource = DealerServiceDistrict;
                gvDealerServiceDistrict.DataBind();

                if (DealerServiceDistrict.Count == 0)
                {
                    lblRowCountDealerServiceDistrict.Visible = false;
                    ibtnDealerServiceDistrictArrowLeft.Visible = false;
                    ibtnDealerServiceDistrictArrowRight.Visible = false;
                }
                else
                {
                    lblRowCountDealerServiceDistrict.Visible = true;
                    ibtnDealerServiceDistrictArrowLeft.Visible = true;
                    ibtnDealerServiceDistrictArrowRight.Visible = true;
                    lblRowCountDealerServiceDistrict.Text = (((gvDealerServiceDistrict.PageIndex) * gvDealerServiceDistrict.PageSize) + 1) + " - " + (((gvDealerServiceDistrict.PageIndex) * gvDealerServiceDistrict.PageSize) + gvDealerServiceDistrict.Rows.Count) + " of " + DealerServiceDistrict.Count;
                }

            }
            catch (Exception Ex)
            {
                lblMessageDealerServiceDistrict.Visible = true;
                lblMessageDealerServiceDistrict.Text = Ex.ToString();
                lblMessageDealerServiceDistrict.ForeColor = Color.Red;
            }
        }
        protected void gvDealerServiceDistrict_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerServiceDistrict.PageIndex = e.NewPageIndex;
            FillGridDealerServiceDistrict();
            gvDealerServiceDistrict.DataBind();

            }
        protected void ibtnDealerServiceDistrictArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerServiceDistrict.PageIndex > 0)
            {
                gvDealerServiceDistrict.PageIndex = gvDealerServiceDistrict.PageIndex - 1;
                DealerServiceDistrictBind(gvDealerServiceDistrict, lblRowCountDealerServiceDistrict, DealerServiceDistrict);
            }
        }       
        protected void ibtnDealerServiceDistrictArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerServiceDistrict.PageCount > gvDealerServiceDistrict.PageIndex)
            {
                gvDealerServiceDistrict.PageIndex = gvDealerServiceDistrict.PageIndex + 1;
                DealerServiceDistrictBind(gvDealerServiceDistrict, lblRowCountDealerServiceDistrict, DealerServiceDistrict);
            }
        }
        void DealerServiceDistrictBind(GridView gv, Label lbl, List<PDMS_District> ServiceDealerDistrict)
        {
            gv.DataSource = ServiceDealerDistrict;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + ServiceDealerDistrict.Count;

        }
        protected void lnkBtnDealerServiceDistrictEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageDealerServiceDistrict.Text = string.Empty;
                lblMessageDealerServiceDistrict.ForeColor = Color.Red;
                lblMessageDealerServiceDistrict.Visible = true;
                
                LinkButton lnkBtnServiceDealerDistrictEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnServiceDealerDistrictEdit.NamingContainer);

                Label lblGDServiceCountry = (Label)row.FindControl("lblGDServiceCountry");
                //Label lblGDServiceCountryID = (Label)row.FindControl("lblGDServiceCountryID");
                TextBox txtServiceCountry = (TextBox)gvDealerServiceDistrict.FooterRow.FindControl("txtServiceCountry");
                txtServiceCountry.Text = lblGDServiceCountry.Text;
                txtServiceCountry.Visible = true;

                Label lblGDServiceRegion = (Label)row.FindControl("lblGDServiceRegion");
                  TextBox txtServiceRegion = (TextBox)gvDealerServiceDistrict.FooterRow.FindControl("txtServiceRegion");
                txtServiceRegion.Text = lblGDServiceRegion.Text;
                txtServiceRegion.Visible = true;

                Label lblGDServiceState = (Label)row.FindControl("lblGDServiceState");
                 TextBox txtServiceState = (TextBox)gvDealerServiceDistrict.FooterRow.FindControl("txtServiceState");
                txtServiceState.Text = lblGDServiceState.Text;
                txtServiceState.Visible = true;

                Label lblGDServiceDistrict = (Label)row.FindControl("lblGDServiceDistrict");
                 TextBox txtServiceDistrict = (TextBox)gvDealerServiceDistrict.FooterRow.FindControl("txtServiceDistrict");
                txtServiceDistrict.Text = lblGDServiceDistrict.Text;
                txtServiceDistrict.Visible = true;

                DropDownList ddlGDServiceDealer = (DropDownList)gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceDealer");
                new DDLBind(ddlGDServiceDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");
                Label lblGDServiceDealerID = (Label)row.FindControl("lblGDServiceDealerID");
                ddlGDServiceDealer.SelectedValue = (string.IsNullOrEmpty(lblGDServiceDealerID.Text)) ? "0" : lblGDServiceDealerID.Text;
                ddlGDServiceDealer.Visible = true;

                Button BtnUpdateDealerServiceDistrict = (Button)gvDealerServiceDistrict.FooterRow.FindControl("BtnUpdateDealerServiceDistrict");
                BtnUpdateDealerServiceDistrict.Visible = true;

                HiddenID.Value = Convert.ToString(lnkBtnServiceDealerDistrictEdit.CommandArgument);
            }
            catch (Exception ex)
            {
                lblMessageDealerServiceDistrict.Text = ex.Message.ToString();
                lblMessageDealerServiceDistrict.ForeColor = Color.Red;
                lblMessageDealerServiceDistrict.Visible = true;
            }
        }
        protected void BtnUpdateDealerServiceDistrict_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageDealerServiceDistrict.Text = string.Empty;
                lblMessageDealerServiceDistrict.ForeColor = Color.Red;
                lblMessageDealerServiceDistrict.Visible = true;
                Boolean Success = true;
                Button BtnUpdateDealerServiceDistrict = (Button)gvDealerServiceDistrict.FooterRow.FindControl("BtnUpdateDealerServiceDistrict");

                DropDownList ddlGDServiceDealer = (DropDownList)gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceDealer");
                if (ddlGDServiceDealer.SelectedValue == "0")
                {
                    lblMessageDealerServiceDistrict.Text = "Please select Dealer.";
                    lblMessageDealerServiceDistrict.ForeColor = Color.Red;
                    return;
                }
                
                Success = new BDMS_Address().UpdateDealerServiceDistrict(Convert.ToInt32(HiddenID.Value), Convert.ToInt32(ddlGDServiceDealer.SelectedValue), PSession.User.UserID);

                if (Success == true)
                {
                    HiddenID.Value = null;
                    FillGridDealerServiceDistrict();
                    lblMessageDealerServiceDistrict.Text = "District successfully updated.";
                    lblMessageDealerServiceDistrict.ForeColor = Color.Green;
                    return;
                }
                else if (Success == false)
                {
                    lblMessageDealerServiceDistrict.Text = "District already found";
                    lblMessageDealerServiceDistrict.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    lblMessageDealerServiceDistrict.Text = "District not updated successfully...!";
                    lblMessageDealerServiceDistrict.ForeColor = Color.Red;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageDealerServiceDistrict.Text = ex.Message.ToString();
                lblMessageDealerServiceDistrict.ForeColor = Color.Red;
                lblMessageDealerServiceDistrict.Visible = true;
            }
        }
       
    }
}