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
    public partial class DealerSalesConfiguration : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_DealerSalesConfiguration; } }
        public List<PDMS_District> DealerSalesDistrict
        {
            get
            {
                if (ViewState["PDMS_District_Sales"] == null)
                {
                    ViewState["PDMS_District_Sales"] = new List<PDMS_District>();
                }
                return (List<PDMS_District>)ViewState["PDMS_District_Sales"];
            }
            set
            {
                ViewState["PDMS_District_Sales"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DealerSalesConfiguration.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Dealer Sales Configuration');</script>");
            if (!IsPostBack)
            {
                try
                {
                    FillCountryDLL(ddlDSalesCountry);
                    FillDealerDLL(ddlDSalesDealer);
                    DealerSalesDistrict = new BDMS_Address().GetDistrict(null, null, null, null, null, null, "true");
                }
                catch (Exception Ex)
                {
                    lblMessageDealerSalesDistrict.Visible = true;
                    lblMessageDealerSalesDistrict.Text = Ex.ToString();
                    lblMessageDealerSalesDistrict.ForeColor = Color.Red;
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
                lblMessageDealerSalesDistrict.Visible = true;
                lblMessageDealerSalesDistrict.Text = Ex.ToString();
                lblMessageDealerSalesDistrict.ForeColor = Color.Red;
            }
        }
        protected void ddlDSalesCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //new DDLBind(ddlDSalesRegion, new BDMS_Address().GetRegion(Convert.ToInt32(ddlDSalesCountry.SelectedValue), null, null), "Region", "RegionID", true, "Select Region");
            FillRegionDLL(ddlDSalesRegion, Convert.ToInt32(ddlDSalesCountry.SelectedValue), null, null);
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
                lblMessageDealerSalesDistrict.Visible = true;
                lblMessageDealerSalesDistrict.Text = Ex.ToString();
                lblMessageDealerSalesDistrict.ForeColor = Color.Red;
            }
        }
        protected void ddlDSalesRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //new DDLBind(ddlDSalesState, new BDMS_Address().GetState(null, Convert.ToInt32(ddlDSalesCountry.SelectedValue), Convert.ToInt32(ddlDSalesRegion.SelectedValue), null, null), "State", "StateID", true, "Select State");
            FillStateDLL(ddlDSalesState, null, Convert.ToInt32(ddlDSalesCountry.SelectedValue), Convert.ToInt32(ddlDSalesRegion.SelectedValue), null, null);
            ddlDSalesDistrict.Items.Clear();
            ddlDSalesDistrict.SelectedValue = "0";
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
                lblMessageDealerSalesDistrict.Visible = true;
                lblMessageDealerSalesDistrict.Text = Ex.ToString();
                lblMessageDealerSalesDistrict.ForeColor = Color.Red;
            }
        }
        protected void ddlDSalesState_SelectedIndexChanged(object sender, EventArgs e)
        {
            //new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(ddlRegion.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), null, null, null, "true"), "District", "DistrictID", true, "Select District");            
            FillDistrictDLL(ddlDSalesDistrict, Convert.ToInt32(ddlDSalesCountry.SelectedValue), Convert.ToInt32(ddlDSalesRegion.SelectedValue), Convert.ToInt32(ddlDSalesState.SelectedValue), null, null);
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
                lblMessageDealerSalesDistrict.Visible = true;
                lblMessageDealerSalesDistrict.Text = Ex.ToString();
                lblMessageDealerSalesDistrict.ForeColor = Color.Red;
            }
        }
        protected void ddlDSalesDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            //new DDLBind(ddlDSalesDealer, new BDMS_Dealer().GetDealer(null, null, PSession.User.UserID, Convert.ToInt32(ddlDSalesRegion.SelectedValue)), "DealerCode", "DealerID", true, "Select Dealer");
            //FillDealerDLL(ddlDSalesDealer, null, null, PSession.User.UserID, Convert.ToInt32(ddlDSalesRegion.SelectedValue));
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
                lblMessageDealerSalesDistrict.Visible = true;
                lblMessageDealerSalesDistrict.Text = Ex.ToString();
                lblMessageDealerSalesDistrict.ForeColor = Color.Red;
            }
        }
        protected void ibtnDealerSalesDistrictArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerSalesDistrict.PageIndex > 0)
            {
                gvDealerSalesDistrict.PageIndex = gvDealerSalesDistrict.PageIndex - 1;
                DealerSalesDistrictBind(gvDealerSalesDistrict, lblRowCountDealerSalesDistrict, DealerSalesDistrict);
            }
        }
        private void FillGridDealerSalesDistrict()
        {
            try
            {
                int? CountryID = null, RegionID = null, StateID = null, DistrictID = null, SalesDealerID = null;

                if (ddlDSalesCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlDSalesCountry.SelectedValue);
                }
                if (ddlDSalesRegion.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDSalesRegion.SelectedValue))
                {
                    RegionID = Convert.ToInt32(ddlDSalesRegion.SelectedValue);
                }
                if (ddlDSalesState.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDSalesState.SelectedValue))
                {
                    StateID = Convert.ToInt32(ddlDSalesState.SelectedValue);
                }
                if (ddlDSalesDistrict.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDSalesDistrict.SelectedValue))
                {
                    DistrictID = Convert.ToInt32(ddlDSalesDistrict.SelectedValue);
                }
                if (ddlDSalesDealer.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDSalesDealer.SelectedValue))
                {
                    SalesDealerID = Convert.ToInt32(ddlDSalesDealer.SelectedValue);
                }

                DealerSalesDistrict = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID, null, SalesDealerID, "true");
                
                if (DealerSalesDistrict.Count == 0)
                {
                    DealerSalesDistrict.Add(new PDMS_District());
                }
                ViewState["gvDealerSalesDistrict"] = DealerSalesDistrict;
                gvDealerSalesDistrict.DataSource = DealerSalesDistrict;
                gvDealerSalesDistrict.DataBind();

                if (DealerSalesDistrict.Count == 0)
                {
                    lblRowCountDealerSalesDistrict.Visible = false;
                    ibtnDealerSalesDistrictArrowLeft.Visible = false;
                    ibtnDealerSalesDistrictArrowRight.Visible = false;
                }
                else
                {
                    lblRowCountDealerSalesDistrict.Visible = true;
                    ibtnDealerSalesDistrictArrowLeft.Visible = true;
                    ibtnDealerSalesDistrictArrowRight.Visible = true;
                    lblRowCountDealerSalesDistrict.Text = (((gvDealerSalesDistrict.PageIndex) * gvDealerSalesDistrict.PageSize) + 1) + " - " + (((gvDealerSalesDistrict.PageIndex) * gvDealerSalesDistrict.PageSize) + gvDealerSalesDistrict.Rows.Count) + " of " + DealerSalesDistrict.Count;
                }

                //DropDownList ddlGDSalesCountry = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesCountry") as DropDownList;
                //new DDLBind(ddlGDSalesCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");

                //DropDownList ddlGDSalesRegion = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesRegion") as DropDownList;
                //new DDLBind(ddlGDSalesRegion, new BDMS_Address().GetRegion(null, null, null), "Region", "RegionID", true, "Select Region");

                //DropDownList ddlGDSalesState = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesState") as DropDownList;
                //new DDLBind(ddlGDSalesState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID", true, "Select State");

                //DropDownList ddlGDSalesDistrict = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesDistrict") as DropDownList;
                //new DDLBind(ddlGDSalesDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null, "true"), "District", "DistrictID", true, "Select District");

                //DropDownList ddlGDSalesOffice = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesOffice") as DropDownList;
                //new DDLBind(ddlGDSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");

                //DropDownList ddlGDSalesDealer = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesDealer") as DropDownList;
                //new DDLBind(ddlGDSalesDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");
            }
            catch (Exception Ex)
            {
                lblMessageDealerSalesDistrict.Visible = true;
                lblMessageDealerSalesDistrict.Text = Ex.ToString();
                lblMessageDealerSalesDistrict.ForeColor = Color.Red;
            }
        }
        protected void ibtnDealerSalesDistrictArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerSalesDistrict.PageCount > gvDealerSalesDistrict.PageIndex)
            {
                gvDealerSalesDistrict.PageIndex = gvDealerSalesDistrict.PageIndex + 1;
                DealerSalesDistrictBind(gvDealerSalesDistrict, lblRowCountDealerSalesDistrict, DealerSalesDistrict);
            }
        }
        void DealerSalesDistrictBind(GridView gv, Label lbl, List<PDMS_District> LDistrict)
        {
            gv.DataSource = LDistrict;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + LDistrict.Count;

            //DropDownList ddlGDSalesCountry = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesCountry") as DropDownList;
            //new DDLBind(ddlGDSalesCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");

            //DropDownList ddlGDSalesRegion = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesRegion") as DropDownList;
            //new DDLBind(ddlGDSalesRegion, new BDMS_Address().GetRegion(null, null, null), "Region", "RegionID", true, "Select Region");

            //DropDownList ddlGDSalesState = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesState") as DropDownList;
            //new DDLBind(ddlGDSalesState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID", true, "Select State");

            //DropDownList ddlGDSalesDistrict = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesDistrict") as DropDownList;
            //new DDLBind(ddlGDSalesDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null, "true"), "District", "DistrictID", true, "Select District");

            //DropDownList ddlGDSalesOffice = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesOffice") as DropDownList;
            //new DDLBind(ddlGDSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");

            //DropDownList ddlGDSalesDealer = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesDealer") as DropDownList;
            //new DDLBind(ddlGDSalesDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");

            //DropDownList ddlSalesEngineer = gvDealerSalesDistrict.FooterRow.FindControl("ddlSalesEngineer") as DropDownList;
            //List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, null, true, null, null, 4);
            //new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
        }
        protected void lnkBtnDealerSalesDistrictEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageDealerSalesDistrict.Text = string.Empty;
                lblMessageDealerSalesDistrict.ForeColor = Color.Red;
                lblMessageDealerSalesDistrict.Visible = true;

                LinkButton lnkBtnDistrictEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnDistrictEdit.NamingContainer);

                Label lblGDSalesCountry = (Label)row.FindControl("lblGDSalesCountry");
                //Label lblGDSalesCountryID = (Label)row.FindControl("lblGDSalesCountryID");
                TextBox txtSalesCountry = (TextBox)gvDealerSalesDistrict.FooterRow.FindControl("txtSalesCountry");
                txtSalesCountry.Text = lblGDSalesCountry.Text;
                txtSalesCountry.Visible = true;

                //Label lblGDSalesRegion = (Label)row.FindControl("lblGDSalesRegion");
                ////Label lblGDSalesRegionID = (Label)row.FindControl("lblGDSalesRegionID");
                //TextBox txtSalesRegion = (TextBox)gvDealerSalesDistrict.FooterRow.FindControl("txtSalesRegion");
                //txtSalesRegion.Text = lblGDSalesRegion.Text;
                //txtSalesRegion.Visible = true;

                Label lblGDSalesState = (Label)row.FindControl("lblGDSalesState");
                //Label lblGDSalesStateID = (Label)row.FindControl("lblGDSalesStateID");
                TextBox txtSalesState = (TextBox)gvDealerSalesDistrict.FooterRow.FindControl("txtSalesState");
                txtSalesState.Text = lblGDSalesState.Text;
                txtSalesState.Visible = true;

                Label lblGDSalesDistrict = (Label)row.FindControl("lblGDSalesDistrict");
                //Label lblGDSalesDistrictID = (Label)row.FindControl("lblGDSalesDistrictID");
                TextBox txtSalesDistrict = (TextBox)gvDealerSalesDistrict.FooterRow.FindControl("txtSalesDistrict");
                txtSalesDistrict.Text = lblGDSalesDistrict.Text;
                txtSalesDistrict.Visible = true;

                Label lblGDSalesOffice = (Label)row.FindControl("lblGDSalesOffice");
                Label lblGDSalesOfficeID = (Label)row.FindControl("lblGDSalesOfficeID");
                DropDownList ddlGDSalesOffice = (DropDownList)gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesOffice");
                new DDLBind(ddlGDSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");
                ddlGDSalesOffice.SelectedValue = (string.IsNullOrEmpty(lblGDSalesOfficeID.Text)) ? "0" : lblGDSalesOfficeID.Text;
                ddlGDSalesOffice.Visible = true;

                Label lblGDSalesDealer = (Label)row.FindControl("lblGDSalesDealer");
                Label lblGDSalesDealerID = (Label)row.FindControl("lblGDSalesDealerID");
                DropDownList ddlGDSalesDealer = (DropDownList)gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesDealer");
                new DDLBind(ddlGDSalesDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");
                ddlGDSalesDealer.SelectedValue = (string.IsNullOrEmpty(lblGDSalesDealerID.Text)) ? "0" : lblGDSalesDealerID.Text;
                ddlGDSalesDealer.Visible = true;

                Label lblGDSalesEngineer = (Label)row.FindControl("lblGDSalesEngineer");
                Label lblGDSalesEngineerUserID = (Label)row.FindControl("lblGDSalesEngineerUserID");
                DropDownList ddlSalesEngineer = (DropDownList)gvDealerSalesDistrict.FooterRow.FindControl("ddlSalesEngineer");
                List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, Convert.ToInt32(ddlGDSalesDealer.SelectedValue), true, null, (short)DealerDepartment.Sales, null);
                new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
                try
                {
                    ddlSalesEngineer.SelectedValue = (string.IsNullOrEmpty(lblGDSalesEngineerUserID.Text)) ? "0" : lblGDSalesEngineerUserID.Text;
                }
                catch { }
                ddlSalesEngineer.Visible = true;

                HiddenID.Value = Convert.ToString(lnkBtnDistrictEdit.CommandArgument);
                Button BtnUpdateDealerSalesDistrict = (Button)gvDealerSalesDistrict.FooterRow.FindControl("BtnUpdateDealerSalesDistrict");
                BtnUpdateDealerSalesDistrict.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessageDealerSalesDistrict.Text = ex.Message.ToString();
                lblMessageDealerSalesDistrict.ForeColor = Color.Red;
                lblMessageDealerSalesDistrict.Visible = true;
            }
        }
        protected void BtnUpdateDealerSalesDistrict_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageDealerSalesDistrict.Text = string.Empty;
                lblMessageDealerSalesDistrict.ForeColor = Color.Red;
                lblMessageDealerSalesDistrict.Visible = true;
                Boolean Success = true;
                Button BtnUpdateDistrict = (Button)gvDealerSalesDistrict.FooterRow.FindControl("BtnUpdateDealerSalesDistrict");

                DropDownList ddlGDSalesOffice = (DropDownList)gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesOffice");
                if (ddlGDSalesOffice.SelectedValue == "0")
                {
                    lblMessageDealerSalesDistrict.Text = "Please select SalesOffice.";
                    lblMessageDealerSalesDistrict.ForeColor = Color.Red;
                    return;
                }
                DropDownList ddlGDSalesDealer = (DropDownList)gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesDealer");
                if (ddlGDSalesDealer.SelectedValue == "0")
                {
                    lblMessageDealerSalesDistrict.Text = "Please select Dealer.";
                    lblMessageDealerSalesDistrict.ForeColor = Color.Red;
                    return;
                }
                
                DropDownList ddlSalesEngineer = (DropDownList)gvDealerSalesDistrict.FooterRow.FindControl("ddlSalesEngineer");
                //if (ddlSalesEngineer.SelectedValue == "0")
                //{
                //    lblMessageDealerSalesDistrict.Text = "Please select Sales Engineer.";
                //    lblMessageDealerSalesDistrict.ForeColor = Color.Red;
                //    return;
                //}
                int? SalesEngineerUserID = ddlSalesEngineer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesEngineer.SelectedValue);
                Success = new BDMS_Address().UpdateDealerSalesDistrict(Convert.ToInt32(HiddenID.Value), Convert.ToInt32(ddlGDSalesDealer.SelectedValue), Convert.ToInt32(ddlGDSalesOffice.SelectedValue), SalesEngineerUserID, PSession.User.UserID);
                if (Success == true)
                {
                    HiddenID.Value = null;
                    FillGridDealerSalesDistrict();
                    lblMessageDealerSalesDistrict.Text = "District successfully updated.";
                    lblMessageDealerSalesDistrict.ForeColor = Color.Green;
                    return;
                }
                else if (Success == false)
                {
                    lblMessageDealerSalesDistrict.Text = "District already found";
                    lblMessageDealerSalesDistrict.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    lblMessageDealerSalesDistrict.Text = "District not updated successfully...!";
                    lblMessageDealerSalesDistrict.ForeColor = Color.Red;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageDealerSalesDistrict.Text = ex.Message.ToString();
                lblMessageDealerSalesDistrict.ForeColor = Color.Red;
                lblMessageDealerSalesDistrict.Visible = true;
            }
        }
        protected void BtnSearchDealerSalesDistrict_Click(object sender, EventArgs e)
        {
            FillGridDealerSalesDistrict();
        }
        protected void gvDealerSalesDistrict_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerSalesDistrict.PageIndex = e.NewPageIndex;
            FillGridDealerSalesDistrict();
            gvDealerSalesDistrict.DataBind();
            //DropDownList ddlGDSalesCountry = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesCountry") as DropDownList;
            //new DDLBind(ddlGDSalesCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");
            //DropDownList ddlGDSalesRegion = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesRegion") as DropDownList;
            //new DDLBind(ddlGDSalesRegion, new BDMS_Address().GetRegion(null, null, null), "Region", "RegionID", true, "Select Region");
            //DropDownList ddlGDSalesState = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesState") as DropDownList;
            //new DDLBind(ddlGDSalesState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID", true, "Select State");
            //DropDownList ddlGDSalesDistrict = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesDistrict") as DropDownList;
            //new DDLBind(ddlGDSalesDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null, "true"), "District", "DistrictID", true, "Select District");
            //DropDownList ddlGDSalesDealer = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesDealer") as DropDownList;
            //new DDLBind(ddlGDSalesDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");
            //DropDownList ddlGDSalesOffice = gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesOffice") as DropDownList;
            //new DDLBind(ddlGDSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");
        }
        protected void ddlGDSalesDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlGDSalesDealer = (DropDownList)gvDealerSalesDistrict.FooterRow.FindControl("ddlGDSalesDealer");
            DropDownList ddlSalesEngineer = (DropDownList)gvDealerSalesDistrict.FooterRow.FindControl("ddlSalesEngineer");
            List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, Convert.ToInt32(ddlGDSalesDealer.SelectedValue), true, null, null, 4);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
        }
    }
}