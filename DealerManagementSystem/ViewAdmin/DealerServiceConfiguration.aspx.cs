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
                if (Session["PDMS_District_Service"] == null)
                {
                    Session["PDMS_District_Service"] = new List<PDMS_District>();
                }
                return (List<PDMS_District>)Session["PDMS_District_Service"];
            }
            set
            {
                Session["PDMS_District_Service"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DealerServiceConfiguration.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Dealer Service Configuration');</script>");

            if (!IsPostBack)
            {
                try
                {
                    //new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");
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
            //new DDLBind(ddlDServiceRegion, new BDMS_Address().GetRegion(Convert.ToInt32(ddlDServiceCountry.SelectedValue), null, null), "Region", "RegionID", true, "Select Region");
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
            //new DDLBind(ddlState, new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(ddlRegion.SelectedValue), null, null), "State", "StateID", true, "Select State");
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
            //new DDLBind(ddlDServiceDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlDServiceCountry.SelectedValue), Convert.ToInt32(ddlDServiceRegion.SelectedValue), Convert.ToInt32(ddlDServiceState.SelectedValue), null, null, null, "true"), "District", "DistrictID", true, "Select District");            
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
        protected void ddlDServiceDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            //new DDLBind(ddlDServiceDealer, new BDMS_Dealer().GetDealer(null, null, PSession.User.UserID, Convert.ToInt32(ddlDServiceRegion.SelectedValue)), "DealerCode", "DealerID", true, "Select Dealer");
            //FillDealerDLL(ddlDServiceDealer, null, null, PSession.User.UserID, Convert.ToInt32(ddlDServiceRegion.SelectedValue));
        }
        //private void FillDealerDLL(DropDownList ddl, int? DealerID, string DealerCode, int? UserID, int? RegionID)
        //{
        //    try
        //    {
        //        ddl.DataSource = new BDMS_Dealer().GetDealer(DealerID, DealerCode, UserID, RegionID);
        //        ddl.DataValueField = "DealerID";
        //        ddl.DataTextField = "DealerCode";
        //        ddl.DataBind();
        //        // ddl.Items.Insert(0, new ListItem("Select", "0"));
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessageServiceDealerDistrict.Visible = true;
        //        lblMessageServiceDealerDistrict.Text = Ex.ToString();
        //        lblMessageServiceDealerDistrict.ForeColor = Color.Red;
        //    }
        //}
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

                ViewState["gvDealerServiceDistrict"] = DealerServiceDistrict;
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

                //DropDownList ddlGDServiceCountry = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceCountry") as DropDownList;
                //new DDLBind(ddlGDServiceCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");

                //DropDownList ddlGDServiceRegion = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceRegion") as DropDownList;
                //new DDLBind(ddlGDServiceRegion, new BDMS_Address().GetRegion(null, null, null), "Region", "RegionID", true, "Select Region");

                //DropDownList ddlGDServiceState = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceState") as DropDownList;
                //new DDLBind(ddlGDServiceState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID", true, "Select State");

                //DropDownList ddlGDServiceDistrict = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceDistrict") as DropDownList;
                //new DDLBind(ddlGDServiceDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null, "true"), "District", "DistrictID", true, "Select District");

                //DropDownList ddlGDServiceDealer = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceDealer") as DropDownList;
                //new DDLBind(ddlGDServiceDealer, new BDMS_Dealer().GetDealer(null, null, PSession.User.UserID, null), "DealerCode", "DealerID", true, "Select Dealer");
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

            //DropDownList ddlGDServiceCountry = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceCountry") as DropDownList;
            //new DDLBind(ddlGDServiceCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");

            //DropDownList ddlGDServiceRegion = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceRegion") as DropDownList;
            //new DDLBind(ddlGDServiceRegion, new BDMS_Address().GetRegion(null, null, null), "Region", "RegionID", true, "Select Region");

            //DropDownList ddlGDServiceState = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceState") as DropDownList;
            //new DDLBind(ddlGDServiceState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID", true, "Select State");

            //DropDownList ddlGDServiceDistrict = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceDistrict") as DropDownList;
            //new DDLBind(ddlGDServiceDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null, "true"), "District", "DistrictID", true, "Select District");

            //DropDownList ddlGDServiceDealer = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceDealer") as DropDownList;
            //new DDLBind(ddlGDServiceDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");
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

            //DropDownList ddlGDServiceCountry = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceCountry") as DropDownList;
            //new DDLBind(ddlGDServiceCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");

            //DropDownList ddlGDServiceRegion = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceRegion") as DropDownList;
            //new DDLBind(ddlGDServiceRegion, new BDMS_Address().GetRegion(null, null, null), "Region", "RegionID", true, "Select Region");

            //DropDownList ddlGDServiceState = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceState") as DropDownList;
            //new DDLBind(ddlGDServiceState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID", true, "Select State");

            //DropDownList ddlGDServiceDistrict = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceDistrict") as DropDownList;
            //new DDLBind(ddlGDServiceDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null, "true"), "District", "DistrictID", true, "Select District");
            
            //DropDownList ddlGDServiceDealer = gvDealerServiceDistrict.FooterRow.FindControl("ddlGDServiceDealer") as DropDownList;
            //new DDLBind(ddlGDServiceDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");
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
                //Label lblGDServiceRegionID = (Label)row.FindControl("lblGDServiceRegionID");
                TextBox txtServiceRegion = (TextBox)gvDealerServiceDistrict.FooterRow.FindControl("txtServiceRegion");
                txtServiceRegion.Text = lblGDServiceRegion.Text;
                txtServiceRegion.Visible = true;

                Label lblGDServiceState = (Label)row.FindControl("lblGDServiceState");
                //Label lblGDServiceStateID = (Label)row.FindControl("lblGDServiceStateID");
                TextBox txtServiceState = (TextBox)gvDealerServiceDistrict.FooterRow.FindControl("txtServiceState");
                txtServiceState.Text = lblGDServiceState.Text;
                txtServiceState.Visible = true;

                Label lblGDServiceDistrict = (Label)row.FindControl("lblGDServiceDistrict");
                //Label lblGDServiceDistrictID = (Label)row.FindControl("lblGDServiceDistrictID");
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
        protected void gvDealerServiceDistrict_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //DropDownList ddlGDServiceDealer = (e.Row.FindControl("ddlGDServiceDealer") as DropDownList);
                    ////new DDLBind(ddlGDServiceDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");
                    //FillDealerDLL(ddlGDServiceDealer);
                    //string ServiceDealerID = !string.IsNullOrEmpty((e.Row.FindControl("lblGDServiceDealerID") as Label).Text) ? (e.Row.FindControl("lblGDServiceDealerID") as Label).Text : "0";
                    //ddlGDServiceDealer.SelectedValue = ServiceDealerID;
                }
            }
            catch (Exception Ex)
            {
                lblMessageDealerServiceDistrict.Visible = true;
                lblMessageDealerServiceDistrict.Text = Ex.ToString();
                lblMessageDealerServiceDistrict.ForeColor = Color.Red;
            }
        }
    }
}