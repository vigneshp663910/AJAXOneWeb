using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class Location : System.Web.UI.Page
    {
        public List<PDMS_Country> LCountry
        {
            get
            {
                if (Session["PDMS_Country"] == null)
                {
                    Session["PDMS_Country"] = new List<PDMS_Country>();
                }
                return (List<PDMS_Country>)Session["PDMS_Country"];
            }
            set
            {
                Session["PDMS_Country"] = value;
            }
        }
        public List<PDMS_Region> LRegion
        {
            get
            {
                if (Session["PDMS_Region"] == null)
                {
                    Session["PDMS_Region"] = new List<PDMS_Region>();
                }
                return (List<PDMS_Region>)Session["PDMS_Region"];
            }
            set
            {
                Session["PDMS_Region"] = value;
            }
        }
        public List<PDMS_State> LState
        {
            get
            {
                if (Session["PDMS_State"] == null)
                {
                    Session["PDMS_State"] = new List<PDMS_State>();
                }
                return (List<PDMS_State>)Session["PDMS_State"];
            }
            set
            {
                Session["PDMS_State"] = value;
            }
        }

        public List<PDMS_District> LDistrict
        {
            get
            {
                if (Session["PDMS_District"] == null)
                {
                    Session["PDMS_District"] = new List<PDMS_District>();
                }
                return (List<PDMS_District>)Session["PDMS_District"];
            }
            set
            {
                Session["PDMS_District"] = value;
            }
        }

       

        public List<PSalesOffice> LSalesOffice
        {
            get
            {
                if (Session["PSalesOffice"] == null)
                {
                    Session["PSalesOffice"] = new List<PSalesOffice>();
                }
                return (List<PSalesOffice>)Session["PSalesOffice"];
            }
            set
            {
                Session["PSalesOffice"] = value;
            }
        }
        //public List<PDMS_Dealer> LDealer
        //{
        //    get
        //    {
        //        if (Session["PDMS_Dealer"] == null)
        //        {
        //            Session["PDMS_Dealer"] = new List<PDMS_Dealer>();
        //        }
        //        return (List<PDMS_Dealer>)Session["PDMS_Dealer"];
        //    }
        //    set
        //    {
        //        Session["PDMS_Dealer"] = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Location');</script>");
            lblMessage.Text = "";

            if (!IsPostBack)
            {
                try
                {
                    Label lblProjectTitle = (Label)Master.FindControl("lblProjectTitle");
                    lblProjectTitle.Text = "Location";
                    FillCountry();
                    LRegion = new BDMS_Address().GetRegion(null, null, null);
                    LState = new BDMS_Address().GetState(null, null, null, null);
                    LSalesOffice = new BDMS_Address().GetSalesOffice(null, null);
                    LDistrict = new BDMS_Address().GetDistrict(null, null, null, null, null, null, "true");
                   // LDealer = new BDMS_Dealer().GetDealer(null, null, null, null);
                    FillCountry();
                    FillRegion();
                    FillState();
                    //FillDistrict();
                    FillDealer();
                    FillDealerDLL(ddlDDealer);
                }
                catch (Exception Ex)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = Ex.ToString();
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }

        private void FillCountry()
        {
            try
            {

                LCountry = new BDMS_Address().GetCountry(null, null);
                FillCountryDLL(ddlRCountry);
                FillCountryDLL(ddlSCountry);
                FillCountryDLL(ddlDCountry);
               // FillCountryDLL(ddlCityCountry);
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillCountryDLL(DropDownList ddl)
        {
            try
            {
                ddl.DataValueField = "CountryID";
                ddl.DataTextField = "Country";
                ddl.DataSource = LCountry;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillRegionDLL(DropDownList ddl, int? CountryID, int? RegionID, string Region)
        {
            try
            {
                LRegion = new BDMS_Address().GetRegion(CountryID, RegionID, Region);
                ddl.DataValueField = "RegionID";
                ddl.DataTextField = "Region";
                ddl.DataSource = LRegion;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillStateDLL(DropDownList ddl, int? CountryID, int? RegionID, int? StateID, string State)
        {
            try
            {
                LState = new BDMS_Address().GetState(CountryID, RegionID, StateID, State);
                ddl.DataValueField = "StateID";
                ddl.DataTextField = "State";
                ddl.DataSource = LState;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillDistrictDLL(DropDownList ddl, int? CountryID, int? RegionID, int? StateID, int? DistrictID, string District)
        {
            try
            {
                LDistrict = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID, null, null, "true");
                ddl.DataValueField = "DistrictID";
                ddl.DataTextField = "District";
                ddl.DataSource = LDistrict;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillSalesOfficeDLL(DropDownList ddl, int? SalesOfficeID, string SalesOffice)
        {
            try
            {
                LSalesOffice = new BDMS_Address().GetSalesOffice(SalesOfficeID, SalesOffice);
                ddl.DataValueField = "SalesOfficeID";
                ddl.DataTextField = "SalesOffice";
                ddl.DataSource = LSalesOffice;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillDealerDLL(DropDownList ddl)
        {
            try
            {
                // LDealer = new BDMS_Dealer().GetDealer(DealerID, DealerCode, null, null);
                ddl.DataValueField = "DID";
                ddl.DataTextField = "CodeWithName";
                ddl.DataSource = PSession.User.Dealer;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillRegion()
        {
            try
            {
                int? CountryID = null;

                if (ddlRCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlRCountry.SelectedValue);
                }
                //FillRegionDLL(ddlRRegion, CountryID, null, null);
                CountryID = null;
                if (ddlSCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSCountry.SelectedValue);
                }
                //FillRegionDLL(ddlSRegion, CountryID, null, null);
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillState()
        {
            try
            {
                int? CountryID = null, RegionID = null;
                if (ddlDCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlDCountry.SelectedValue);
                }
                //if (ddlDCountry.SelectedValue != "0")
                //{
                //    CountryID = Convert.ToInt32(ddlDCountry.SelectedValue);
                //}
                //FillStateDLL(ddlDState, CountryID, null, null, null);

                //CountryID = null; RegionID = null;
                //if (ddlCityCountry.SelectedValue != "0")
                //{
                //    CountryID = Convert.ToInt32(ddlCityCountry.SelectedValue);
                //}
                //FillStateDLL(ddlCityState, CountryID, null, null, null);
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
       
        private void FillDealer()
        {
            try
            {
                int? DealerID = null;
                string DealerCode = null;

                //FillDealerDLL(ddlDDealer, DealerID, DealerCode);
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void ibtnStateArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvState.PageIndex > 0)
            {
                gvState.PageIndex = gvState.PageIndex - 1;
                StateBind(gvState, lblRowCountS, LState);
            }
        }
        protected void ibtnStateArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvState.PageCount > gvState.PageIndex)
            {
                gvState.PageIndex = gvState.PageIndex + 1;
                StateBind(gvState, lblRowCountS, LState);
            }
        }

        void StateBind(GridView gv, Label lbl, List<PDMS_State> LState)
        {
            gv.DataSource = LState;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + LState.Count;
        }

        private void FillGridState()
        {
            try
            {
                int? CountryID = null, RegionID = null, StateID = null;
                string State = null, StateCode = null;
                if (ddlSCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSCountry.SelectedValue);

                    if (ddlSRegion.SelectedValue != "0")
                    {
                        RegionID = Convert.ToInt32(ddlSRegion.SelectedValue);
                    }
                }
                if (!string.IsNullOrEmpty(txtState.Text))
                {
                    State = txtState.Text.Trim();
                }
                if (!string.IsNullOrEmpty(txtStateCode.Text))
                {
                    StateCode = txtStateCode.Text.Trim();
                }

                //List<PDMS_State> MML = new BDMS_Address().GetState(CountryID, RegionID, null, State);
                LState = new BDMS_Address().GetState(CountryID, RegionID, null, State);

                if (LState.Count == 0)
                {
                    LState.Add(new PDMS_State());
                }
                gvState.DataSource = LState;
                gvState.DataBind();

                if (LState.Count == 0)
                {
                    lblRowCountS.Visible = false;
                    ibtnStateArrowLeft.Visible = false;
                    ibtnStateArrowRight.Visible = false;
                }
                else
                {
                    lblRowCountS.Visible = true;
                    ibtnStateArrowLeft.Visible = true;
                    ibtnStateArrowRight.Visible = true;
                    lblRowCountS.Text = (((gvState.PageIndex) * gvState.PageSize) + 1) + " - " + (((gvState.PageIndex) * gvState.PageSize) + gvState.Rows.Count) + " of " + LState.Count;
                }



                DropDownList ddlGSCountry = gvState.FooterRow.FindControl("ddlGSCountry") as DropDownList;
                new DDLBind(ddlGSCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");

                DropDownList ddlGSRegion = gvState.FooterRow.FindControl("ddlGSRegion") as DropDownList;
                new DDLBind(ddlGSRegion, new BDMS_Address().GetRegion(null, null, null), "Region", "RegionID", true, "Select Region");
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void ibtnDistrictArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDistrict.PageIndex > 0)
            {
                gvDistrict.PageIndex = gvDistrict.PageIndex - 1;
                DistrictBind(gvDistrict, lblRowCountD, LDistrict);
            }
        }
        protected void ibtnDistrictArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDistrict.PageCount > gvDistrict.PageIndex)
            {
                gvDistrict.PageIndex = gvDistrict.PageIndex + 1;
                DistrictBind(gvDistrict, lblRowCountD, LDistrict);
            }
        }

        void DistrictBind(GridView gv, Label lbl, List<PDMS_District> LDistrict)
        {
            gv.DataSource = LDistrict;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + LDistrict.Count;


            DropDownList ddlGDCountry = gvDistrict.FooterRow.FindControl("ddlGDCountry") as DropDownList;
            new DDLBind(ddlGDCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");

            DropDownList ddlGDState = gvDistrict.FooterRow.FindControl("ddlGDState") as DropDownList;
            new DDLBind(ddlGDState, new BDMS_Address().GetState(null, null, null, null), "State", "StateID", true, "Select State");

            DropDownList ddlGDSalesOffice = gvDistrict.FooterRow.FindControl("ddlGDSalesOffice") as DropDownList;
            new DDLBind(ddlGDSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");

            DropDownList ddlGDDealer = gvDistrict.FooterRow.FindControl("ddlGDDealer") as DropDownList;
            new DDLBind(ddlGDDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");

            DropDownList ddlSalesEngineer = gvDistrict.FooterRow.FindControl("ddlSalesEngineer") as DropDownList;
            //new DDLBind(ddlSalesEngineer, new BUser().GetUsers(null, null, 7, null, null, true, null, null, null), "ContactName", "UserID", true, "Select Engineer");
            List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, null, true, null, null, 4);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
        }

        private void FillGridDistrict()
        {
            try
            {
                int? CountryID = null, RegionID = null, StateID = null, DistrictID = null, DealerID = null;
                string District = null;
                if (ddlDCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlDCountry.SelectedValue);
                }
                if (ddlDState.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDState.SelectedValue))
                {
                    StateID = Convert.ToInt32(ddlDState.SelectedValue);
                }
                if (ddlDDealer.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDDealer.SelectedValue))
                {
                    DealerID = Convert.ToInt32(ddlDDealer.SelectedValue);
                }

                if (!string.IsNullOrEmpty(txtDistrict.Text))
                {
                    District = txtDistrict.Text.Trim();
                }

                //List<PDMS_District> MML = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID,  District, null);

                LDistrict = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID, District, DealerID,"true");
                if (LDistrict.Count == 0)
                {
                    LDistrict.Add(new PDMS_District());
                }
                ViewState["gvDistrict"] = LDistrict;
                gvDistrict.DataSource = LDistrict;
                gvDistrict.DataBind();


                if (LDistrict.Count == 0)
                {
                    lblRowCountD.Visible = false;
                    ibtnDistrictArrowLeft.Visible = false;
                    ibtnDistrictArrowRight.Visible = false;
                }
                else
                {
                    lblRowCountD.Visible = true;
                    ibtnDistrictArrowLeft.Visible = true;
                    ibtnDistrictArrowRight.Visible = true;
                    lblRowCountD.Text = (((gvDistrict.PageIndex) * gvDistrict.PageSize) + 1) + " - " + (((gvDistrict.PageIndex) * gvDistrict.PageSize) + gvDistrict.Rows.Count) + " of " + LDistrict.Count;
                }


                DropDownList ddlGDCountry = gvDistrict.FooterRow.FindControl("ddlGDCountry") as DropDownList;
                new DDLBind(ddlGDCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");

                DropDownList ddlGDState = gvDistrict.FooterRow.FindControl("ddlGDState") as DropDownList;
                new DDLBind(ddlGDState, new BDMS_Address().GetState(null, null, null, null), "State", "StateID", true, "Select State");

                DropDownList ddlGDSalesOffice = gvDistrict.FooterRow.FindControl("ddlGDSalesOffice") as DropDownList;
                new DDLBind(ddlGDSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");

                DropDownList ddlGDDealer = gvDistrict.FooterRow.FindControl("ddlGDDealer") as DropDownList;
                new DDLBind(ddlGDDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");

                DropDownList ddlSalesEngineer = gvDistrict.FooterRow.FindControl("ddlSalesEngineer") as DropDownList;
                //new DDLBind(ddlSalesEngineer, new BUser().GetUsers(null, null, 7, null, null, true, null, null, null), "ContactName", "UserID", true, "Select Engineer");
                List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, null, true, null, null, 4);
                new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void ibtnCountryArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvCountry.PageIndex > 0)
            {
                gvCountry.PageIndex = gvCountry.PageIndex - 1;
                CountryBind(gvCountry, lblRowCountN, LCountry);
            }
        }
        protected void ibtnCountryArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvCountry.PageCount > gvCountry.PageIndex)
            {
                gvCountry.PageIndex = gvCountry.PageIndex + 1;
                CountryBind(gvCountry, lblRowCountN, LCountry);
            }
        }

        void CountryBind(GridView gv, Label lbl, List<PDMS_Country> LCountry)
        {
            gv.DataSource = LCountry;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + LCountry.Count;
        }

        private void FillGridCountry()
        {
            try
            {
                int? CountryID = null;
                string country = null;
                if (!string.IsNullOrEmpty(txtCountry.Text))
                {
                    country = txtCountry.Text.Trim();
                }
                //List<PDMS_Country> MML = new BDMS_Address().GetCountry(CountryID, country);
                LCountry = new BDMS_Address().GetCountry(CountryID, country);
                if (LCountry.Count == 0)
                {
                    LCountry.Add(new PDMS_Country());
                }
                ViewState["gvCountry"] = LCountry;
                gvCountry.DataSource = LCountry;
                gvCountry.DataBind();

                if (LCountry.Count == 0)
                {
                    lblRowCountN.Visible = false;
                    ibtnCountryArrowLeft.Visible = false;
                    ibtnCountryArrowRight.Visible = false;
                }
                else
                {
                    lblRowCountN.Visible = true;
                    ibtnCountryArrowLeft.Visible = true;
                    ibtnCountryArrowRight.Visible = true;
                    lblRowCountN.Text = (((gvCountry.PageIndex) * gvCountry.PageSize) + 1) + " - " + (((gvCountry.PageIndex) * gvCountry.PageSize) + gvCountry.Rows.Count) + " of " + LCountry.Count;
                }

                DropDownList ddlGCCountryCurrency = gvCountry.FooterRow.FindControl("ddlGCCountryCurrency") as DropDownList;
                new DDLBind(ddlGCCountryCurrency, new BDMS_Address().GetCurrency(null, null), "Currency", "CurrencyID", true, "Select Country Currency");

                DropDownList ddlGCSalesOrganization = gvCountry.FooterRow.FindControl("ddlGCSalesOrganization") as DropDownList;
                //new DDLBind(ddlGCSalesOrganization, new BDMS_Address().GetCountry(null, null), "SalesOrganization", "SalesOrganization", true, "Select Sales Organization");

                //new BDMS_Address().GetSalesOrganization(ddlGCSalesOrganization, null, null);
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void ibtnRegionArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvRegion.PageIndex > 0)
            {
                gvRegion.PageIndex = gvRegion.PageIndex - 1;
                RegionBind(gvRegion, lblRowCountR, LRegion);
            }
        }
        protected void ibtnRegionArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvRegion.PageCount > gvRegion.PageIndex)
            {
                gvRegion.PageIndex = gvRegion.PageIndex + 1;
                RegionBind(gvRegion, lblRowCountR, LRegion);
            }
        }

        void RegionBind(GridView gv, Label lbl, List<PDMS_Region> LRegion)
        {
            gv.DataSource = LRegion;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + LRegion.Count;
        }

        private void FillGridRegion()
        {
            try
            {
                int? CountryID = null, RegionID = null;
                string Region = null;
                if (ddlRCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlRCountry.SelectedValue);
                    if (ddlRRegion.SelectedValue != "0") //&& !string.IsNullOrEmpty(ddlRRegion.SelectedValue)
                    {
                        RegionID = Convert.ToInt32(ddlRRegion.SelectedValue);
                    }
                }
                //if (!string.IsNullOrEmpty(ddlRRegion.Text))

                //List<PDMS_Region> MML = new BDMS_Address().GetRegion(CountryID, RegionID, Region);

                LRegion = new BDMS_Address().GetRegion(CountryID, RegionID, Region);

                if (LRegion.Count == 0)
                {
                    LRegion.Add(new PDMS_Region());
                }
                gvRegion.DataSource = LRegion;
                gvRegion.DataBind();


                if (LRegion.Count == 0)
                {
                    lblRowCountR.Visible = false;
                    ibtnRegionArrowLeft.Visible = false;
                    ibtnRegionArrowRight.Visible = false;
                }
                else
                {
                    lblRowCountR.Visible = true;
                    ibtnRegionArrowLeft.Visible = true;
                    ibtnRegionArrowRight.Visible = true;
                    lblRowCountR.Text = (((gvRegion.PageIndex) * gvRegion.PageSize) + 1) + " - " + (((gvRegion.PageIndex) * gvRegion.PageSize) + gvRegion.Rows.Count) + " of " + LRegion.Count;
                }

                DropDownList ddlGRCountry = gvRegion.FooterRow.FindControl("ddlGRCountry") as DropDownList;
                new DDLBind(ddlGRCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void BtnAddOrUpdateCountry_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                Button BtnAddOrUpdateCountry = (Button)gvCountry.FooterRow.FindControl("BtnAddOrUpdateCountry");

                string Country = ((TextBox)gvCountry.FooterRow.FindControl("txtGCCountry")).Text.Trim();
                if (string.IsNullOrEmpty(Country))
                {
                    lblMessage.Text = "Please enter Country.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                string CountryCode = ((TextBox)gvCountry.FooterRow.FindControl("txtGCCountryCode")).Text.Trim();
                if (string.IsNullOrEmpty(Country))
                {
                    lblMessage.Text = "Please enter Country Code.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                DropDownList ddlGCCountryCurrency = (DropDownList)gvCountry.FooterRow.FindControl("ddlGCCountryCurrency");
                if (ddlGCCountryCurrency.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Country Currency.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                //string SalesOrganization = ((TextBox)gvCountry.FooterRow.FindControl("txtGCSalesOrganization")).Text.Trim();
                DropDownList ddlGCSalesOrganization = (DropDownList)gvCountry.FooterRow.FindControl("ddlGCSalesOrganization");
                if (ddlGCSalesOrganization.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Sales Organization.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                int CountryCurrencyID = Convert.ToInt32(ddlGCCountryCurrency.SelectedValue);
                int? CountryID = null;

                if (BtnAddOrUpdateCountry.Text == "Add")
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressCountry(CountryID, Country, CountryCode, CountryCurrencyID, ddlGCSalesOrganization.SelectedValue, true, PSession.User.UserID);
                    if (Success == true)
                    {
                        FillGridCountry();
                        lblMessage.Text = "Country is added successfully.";
                        lblMessage.ForeColor = Color.Green;
                        FillCountry();
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "Country is already found.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Country not created successfully.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    CountryID = Convert.ToInt32(HiddenID.Value);
                    Success = new BDMS_Address().InsertOrUpdateAddressCountry(CountryID, Country, CountryCode, CountryCurrencyID, ddlGCSalesOrganization.SelectedValue, true, PSession.User.UserID);
                    if (Success == true)
                    {
                        FillGridCountry();
                        lblMessage.Text = "Country is updated successfully.";
                        lblMessage.ForeColor = Color.Green;
                        FillCountry();
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "Country is already found.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Country not created successfully.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void BtnSearchCountry_Click(object sender, EventArgs e)
        {
            FillGridCountry();
            lblMessage.Text = "";
        }

        protected void lnkBtnCountryEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnCountryEdit = (LinkButton)sender;
                TextBox txtGCCountry = (TextBox)gvCountry.FooterRow.FindControl("txtGCCountry");
                TextBox txtGCCountryCode = (TextBox)gvCountry.FooterRow.FindControl("txtGCCountryCode");
                DropDownList ddlGCCountryCurrency = (DropDownList)gvCountry.FooterRow.FindControl("ddlGCCountryCurrency");
                //TextBox txtGCSalesOrganization = (TextBox)gvCountry.FooterRow.FindControl("txtGCSalesOrganization");
                DropDownList ddlGCSalesOrganization = (DropDownList)gvCountry.FooterRow.FindControl("ddlGCSalesOrganization");
                Button BtnAddOrUpdateCountry = (Button)gvCountry.FooterRow.FindControl("BtnAddOrUpdateCountry");
                GridViewRow row = (GridViewRow)(lnkBtnCountryEdit.NamingContainer);
                Label lblGCCountry = (Label)row.FindControl("lblGCCountry");
                txtGCCountry.Text = lblGCCountry.Text;
                Label lblGCCountryCode = (Label)row.FindControl("lblGCCountryCode");
                txtGCCountryCode.Text = lblGCCountryCode.Text;
                Label lblGCCountryCurrencyID = (Label)row.FindControl("lblGCCountryCurrencyID");
                ddlGCCountryCurrency.SelectedValue = lblGCCountryCurrencyID.Text;
                Label lblGCSalesOrganization = (Label)row.FindControl("lblGCSalesOrganization");
                //txtGCSalesOrganization.Text = lblGCSalesOrganization.Text;
                ddlGCSalesOrganization.SelectedValue = lblGCSalesOrganization.Text;
                HiddenID.Value = Convert.ToString(lnkBtnCountryEdit.CommandArgument);
                BtnAddOrUpdateCountry.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnCountryDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean success = true;
                LinkButton lnkBtnCountryDelete = (LinkButton)sender;
                int CountryID = Convert.ToInt32(lnkBtnCountryDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnCountryDelete.NamingContainer);
                string Country = ((Label)row.FindControl("lblGCCountry")).Text.Trim();
                string CountryCode = ((Label)row.FindControl("lblGCCountryCode")).Text.Trim();
                int CountryCurrencyID = Convert.ToInt32(((Label)row.FindControl("lblGCCountryCurrencyID")).Text.Trim());
                string SalesOrganization = ((Label)row.FindControl("lblGCSalesOrganization")).Text.Trim();

                success = new BDMS_Address().InsertOrUpdateAddressCountry(CountryID, Country, CountryCode, CountryCurrencyID, SalesOrganization, false, PSession.User.UserID);
                if (success == true)
                {
                    HiddenID.Value = null;
                    FillGridCountry();
                    FillCountry();
                    lblMessage.Text = "Country deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else if (success == false)
                {
                    lblMessage.Text = "Country not deleted successfully";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnRegionEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnRegionEdit = (LinkButton)sender;
                DropDownList ddlGRCountry = (DropDownList)gvRegion.FooterRow.FindControl("ddlGRCountry");
                ddlGRCountry.Enabled = false;
                TextBox txtGRRegion = (TextBox)gvRegion.FooterRow.FindControl("txtGRRegion");
                Button BtnAddOrUpdateRegion = (Button)gvRegion.FooterRow.FindControl("BtnAddOrUpdateRegion");
                GridViewRow row = (GridViewRow)(lnkBtnRegionEdit.NamingContainer);
                Label lblGRCountryID = (Label)row.FindControl("lblGRCountryID");
                ddlGRCountry.SelectedValue = (string.IsNullOrEmpty(lblGRCountryID.Text)) ? "0" : lblGRCountryID.Text;
                Label lblGRRegion = (Label)row.FindControl("lblGRRegion");
                txtGRRegion.Text = lblGRRegion.Text;
                HiddenID.Value = Convert.ToString(lnkBtnRegionEdit.CommandArgument);
                BtnAddOrUpdateRegion.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnRegionDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean success = true;
                LinkButton lnkBtnRegionDelete = (LinkButton)sender;
                int RegionID = Convert.ToInt32(lnkBtnRegionDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnRegionDelete.NamingContainer);
                int CountryID = Convert.ToInt32(((Label)row.FindControl("lblGRCountryID")).Text.Trim());
                string Region = ((Label)row.FindControl("lblGRRegion")).Text.Trim();

                success = new BDMS_Address().InsertOrUpdateAddressRegion(RegionID, Region, CountryID, false, PSession.User.UserID);
                if (success == true)
                {
                    HiddenID.Value = null;
                    FillGridRegion();
                    FillRegionDLL(ddlRRegion, Convert.ToInt32(ddlRCountry.SelectedValue), null, null);
                    lblMessage.Text = "Region deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else if (success == false)
                {
                    lblMessage.Text = "Region not deleted successfully";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void BtnAddOrUpdateRegion_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                Button BtnAddOrUpdateRegion = (Button)gvRegion.FooterRow.FindControl("BtnAddOrUpdateRegion");

                DropDownList ddlGRCountry = (DropDownList)gvRegion.FooterRow.FindControl("ddlGRCountry");
                if (ddlGRCountry.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Country.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                string Region = ((TextBox)gvRegion.FooterRow.FindControl("txtGRRegion")).Text.Trim();
                if (string.IsNullOrEmpty(Region))
                {
                    lblMessage.Text = "Please enter Region.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddOrUpdateRegion.Text == "Add")
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressRegion(null, Region, Convert.ToInt32(ddlGRCountry.SelectedValue), true, PSession.User.UserID);
                    if (Success == true)
                    {
                        FillGridRegion();
                        FillRegionDLL(ddlRRegion, Convert.ToInt32(ddlRCountry.SelectedValue), null, null);
                        lblMessage.Text = "Region is added successfully.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "Region is already found.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Region not created successfully.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressRegion(Convert.ToInt32(HiddenID.Value), Region, Convert.ToInt32(ddlGRCountry.SelectedValue), true, PSession.User.UserID);
                    if (Success == true)
                    {
                        HiddenID.Value = null;
                        FillGridRegion();
                        FillRegionDLL(ddlRRegion, Convert.ToInt32(ddlRCountry.SelectedValue), null, null);
                        lblMessage.Text = "Region successfully updated.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "Region already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Region not updated successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void BtnSearchRegion_Click(object sender, EventArgs e)
        {
            FillGridRegion();
        }

        protected void gvRegion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void BtnSearchState_Click(object sender, EventArgs e)
        {
            FillGridState();
        }

        protected void BtnAddOrUpdateState_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                Button BtnAddOrUpdateRegion = (Button)gvState.FooterRow.FindControl("BtnAddOrUpdateState");

                DropDownList ddlGSCountry = (DropDownList)gvState.FooterRow.FindControl("ddlGSCountry");
                if (ddlGSCountry.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Country.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                DropDownList ddlGSRegion = (DropDownList)gvState.FooterRow.FindControl("ddlGSRegion");
                if (ddlGSRegion.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Region.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                string State = ((TextBox)gvState.FooterRow.FindControl("txtGSState")).Text.Trim();
                if (string.IsNullOrEmpty(State))
                {
                    lblMessage.Text = "Please enter State.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                string StateCode = ((TextBox)gvState.FooterRow.FindControl("txtGSStateCode")).Text.Trim();
                if (string.IsNullOrEmpty(StateCode))
                {
                    lblMessage.Text = "Please enter State Code.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddOrUpdateRegion.Text == "Add")
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressState(null, State, StateCode, null, Convert.ToInt32(ddlGSCountry.SelectedValue), true, Convert.ToInt32(ddlGSRegion.SelectedValue), PSession.User.UserID);
                    if (Success == true)
                    {
                        FillGridState();
                        lblMessage.Text = "State is added successfully.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "State is already found.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "State not created successfully.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressState(Convert.ToInt32(HiddenID.Value), State, StateCode, null, Convert.ToInt32(ddlGSCountry.SelectedValue), true, Convert.ToInt32(ddlGSRegion.SelectedValue), PSession.User.UserID);

                    if (Success == true)
                    {
                        HiddenID.Value = null;
                        FillGridState();
                        lblMessage.Text = "State successfully updated.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "State already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "State not updated successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void gvState_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //DropDownList ddlGSCountry = (e.Row.FindControl("ddlGSCountry") as DropDownList);
                    //DropDownList ddlGSRegion = (e.Row.FindControl("ddlGSRegion") as DropDownList);

                    //FillCountryDLL(ddlGSCountry);
                    //string CountryID = !string.IsNullOrEmpty((e.Row.FindControl("lblCountry") as Label).Text) ? (e.Row.FindControl("lblCountry") as Label).Text : "0";
                    //ddlGSCountry.SelectedValue = CountryID;

                    //FillRegionDLL(ddlGSRegion, Convert.ToInt32(CountryID), null, null);
                    //string RegionID = !string.IsNullOrEmpty((e.Row.FindControl("lblRegion") as Label).Text) ? (e.Row.FindControl("lblRegion") as Label).Text : "0";
                    //ddlGSRegion.SelectedValue = RegionID;
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void lnkBtnStateEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnStateEdit = (LinkButton)sender;
                DropDownList ddlGSCountry = (DropDownList)gvState.FooterRow.FindControl("ddlGSCountry");
                ddlGSCountry.Enabled = false;
                DropDownList ddlGSRegion = (DropDownList)gvState.FooterRow.FindControl("ddlGSRegion");
                ddlGSRegion.Enabled = false;
                TextBox txtGSState = (TextBox)gvState.FooterRow.FindControl("txtGSState");
                TextBox txtGSStateCode = (TextBox)gvState.FooterRow.FindControl("txtGSStateCode");
                Button BtnAddOrUpdateState = (Button)gvState.FooterRow.FindControl("BtnAddOrUpdateState");
                GridViewRow row = (GridViewRow)(lnkBtnStateEdit.NamingContainer);
                Label lblGSCountry = (Label)row.FindControl("lblGSCountry");
                Label lblGSCountryID = (Label)row.FindControl("lblGSCountryID");
                ddlGSCountry.SelectedValue = (string.IsNullOrEmpty(lblGSCountryID.Text)) ? "0" : lblGSCountryID.Text;
                Label lblGSRegion = (Label)row.FindControl("lblGSRegion");
                Label lblGSRegionID = (Label)row.FindControl("lblGSRegionID");
                ddlGSRegion.SelectedValue = (string.IsNullOrEmpty(lblGSRegionID.Text)) ? "0" : lblGSRegionID.Text;
                Label lblGSState = (Label)row.FindControl("lblGSState");
                txtGSState.Text = lblGSState.Text;
                Label lblGSStateCode = (Label)row.FindControl("lblGSStateCode");
                txtGSStateCode.Text = lblGSStateCode.Text;
                HiddenID.Value = Convert.ToString(lnkBtnStateEdit.CommandArgument);
                BtnAddOrUpdateState.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnStateDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean success = true;
                LinkButton lnkBtnStateDelete = (LinkButton)sender;
                int StateID = Convert.ToInt32(lnkBtnStateDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnStateDelete.NamingContainer);
                int CountryID = Convert.ToInt32(((Label)row.FindControl("lblGSCountryID")).Text.Trim());
                int RegionID = Convert.ToInt32(((Label)row.FindControl("lblGSRegionID")).Text.Trim());
                string State = ((Label)row.FindControl("lblGSState")).Text.Trim();
                string StateCode = ((Label)row.FindControl("lblGSStateCode")).Text.Trim();

                success = new BDMS_Address().InsertOrUpdateAddressState(StateID, State, StateCode, null, CountryID, false, RegionID, PSession.User.UserID);
                if (success == true)
                {
                    HiddenID.Value = null;
                    FillGridState();
                    lblMessage.Text = "State deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else if (success == false)
                {
                    lblMessage.Text = "State not deleted successfully";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void BtnSearchDistrict_Click(object sender, EventArgs e)
        {
            FillGridDistrict();
        }

        protected void lnkBtnDistrictEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnDistrictEdit = (LinkButton)sender;
                DropDownList ddlGDCountry = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDCountry");
                ddlGDCountry.Enabled = false;
                DropDownList ddlGDState = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDState");
                ddlGDState.Enabled = false;
                DropDownList ddlGDSalesOffice = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDSalesOffice");
                DropDownList ddlGDDealer = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDDealer");
                DropDownList ddlSalesEngineer = (DropDownList)gvDistrict.FooterRow.FindControl("ddlSalesEngineer");
                TextBox txtGDDistrict = (TextBox)gvDistrict.FooterRow.FindControl("txtGDDistrict");
                Button BtnAddOrUpdateDistrict = (Button)gvDistrict.FooterRow.FindControl("BtnAddOrUpdateDistrict");
                GridViewRow row = (GridViewRow)(lnkBtnDistrictEdit.NamingContainer);
                Label lblGDCountry = (Label)row.FindControl("lblGDCountry");
                Label lblGDCountryID = (Label)row.FindControl("lblGDCountryID");
                ddlGDCountry.SelectedValue = (string.IsNullOrEmpty(lblGDCountryID.Text)) ? "0" : lblGDCountryID.Text;
                Label lblGDState = (Label)row.FindControl("lblGDState");
                Label lblGDStateID = (Label)row.FindControl("lblGDStateID");
                ddlGDState.SelectedValue = (string.IsNullOrEmpty(lblGDStateID.Text)) ? "0" : lblGDStateID.Text;
                Label lblGDSalesOffice = (Label)row.FindControl("lblGDSalesOffice");
                Label lblGDSalesOfficeID = (Label)row.FindControl("lblGDSalesOfficeID");
                ddlGDSalesOffice.SelectedValue = (string.IsNullOrEmpty(lblGDSalesOfficeID.Text)) ? "0" : lblGDSalesOfficeID.Text;
                Label lblGDSalesEngineer = (Label)row.FindControl("lblGDSalesEngineer");
                Label lblGDSalesEngineerUserID = (Label)row.FindControl("lblGDSalesEngineerUserID");
                ddlSalesEngineer.SelectedValue = (string.IsNullOrEmpty(lblGDSalesEngineerUserID.Text)) ? "0" : lblGDSalesEngineerUserID.Text;
                Label lblGDDealer = (Label)row.FindControl("lblGDDealer");
                Label lblGDDealerID = (Label)row.FindControl("lblGDDealerID");
                ddlGDDealer.SelectedValue = (string.IsNullOrEmpty(lblGDDealerID.Text)) ? "0" : lblGDDealerID.Text;
                Label lblGDDistrict = (Label)row.FindControl("lblGDDistrict");
                txtGDDistrict.Text = lblGDDistrict.Text;
                HiddenID.Value = Convert.ToString(lnkBtnDistrictEdit.CommandArgument);
                BtnAddOrUpdateDistrict.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnDistrictDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean success = true;
                LinkButton lnkBtnDistrictDelete = (LinkButton)sender;
                int DistrictID = Convert.ToInt32(lnkBtnDistrictDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnDistrictDelete.NamingContainer);
                int CountryID = Convert.ToInt32(((Label)row.FindControl("lblGDCountryID")).Text.Trim());
                int StateID = Convert.ToInt32(((Label)row.FindControl("lblGDStateID")).Text.Trim());
                int SalesOfficeID = Convert.ToInt32(((Label)row.FindControl("lblGDSalesOfficeID")).Text.Trim());
                int DealerID = Convert.ToInt32(((Label)row.FindControl("lblGDDealerID")).Text.Trim());
                int SalesEngineerUserID = Convert.ToInt32(((Label)row.FindControl("lblGDSalesEngineerUserID")).Text.Trim());
                string District = ((Label)row.FindControl("lblGDDistrict")).Text.Trim();

                success = new BDMS_Address().InsertOrUpdateAddressDistrict(DistrictID, CountryID, StateID, SalesOfficeID, DealerID, SalesEngineerUserID, District, null, false, PSession.User.UserID);
                if (success == true)
                {
                    HiddenID.Value = null;
                    FillGridDistrict();
                    lblMessage.Text = "District deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else if (success == false)
                {
                    lblMessage.Text = "District not deleted successfully";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void BtnAddOrUpdateDistrict_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                Button BtnAddOrUpdateDistrict = (Button)gvDistrict.FooterRow.FindControl("BtnAddOrUpdateDistrict");

                DropDownList ddlGDCountry = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDCountry");
                if (ddlGDCountry.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Country.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                DropDownList ddlGDState = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDState");
                if (ddlGDState.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select State.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                DropDownList ddlGDSalesOffice = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDSalesOffice");
                if (ddlGDSalesOffice.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select SalesOffice.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                DropDownList ddlGDDealer = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDDealer");
                if (ddlGDDealer.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Dealer.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                DropDownList ddlSalesEngineer = (DropDownList)gvDistrict.FooterRow.FindControl("ddlSalesEngineer");
                if (ddlSalesEngineer.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Sales Engineer.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                string District = ((TextBox)gvDistrict.FooterRow.FindControl("txtGDDistrict")).Text.Trim();
                if (string.IsNullOrEmpty(District))
                {
                    lblMessage.Text = "Please enter District.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddOrUpdateDistrict.Text == "Add")
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressDistrict(null, Convert.ToInt32(ddlGDCountry.SelectedValue), Convert.ToInt32(ddlGDState.SelectedValue), Convert.ToInt32(ddlGDSalesOffice.SelectedValue), Convert.ToInt32(ddlGDDealer.SelectedValue), Convert.ToInt32(ddlSalesEngineer.SelectedValue), District, null, true, PSession.User.UserID);
                    if (Success == true)
                    {
                        FillGridDistrict();
                        lblMessage.Text = "District is added successfully.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "District is already found.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "District not created successfully.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressDistrict(Convert.ToInt32(HiddenID.Value), Convert.ToInt32(ddlGDCountry.SelectedValue), Convert.ToInt32(ddlGDState.SelectedValue), Convert.ToInt32(ddlGDSalesOffice.SelectedValue), Convert.ToInt32(ddlGDDealer.SelectedValue), Convert.ToInt32(ddlSalesEngineer.SelectedValue), District, null, true, PSession.User.UserID);

                    if (Success == true)
                    {
                        HiddenID.Value = null;
                        FillGridDistrict();
                        lblMessage.Text = "District successfully updated.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "District already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "District not updated successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void gvDistrict_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //DropDownList ddlGDCountry = (e.Row.FindControl("ddlGDCountry") as DropDownList);
                    //DropDownList ddlGDState = (e.Row.FindControl("ddlGDState") as DropDownList);
                    //DropDownList ddlGDDealer = (e.Row.FindControl("ddlGDDealer") as DropDownList);

                    //FillCountryDLL(ddlGDCountry);
                    //string CountryID = !string.IsNullOrEmpty((e.Row.FindControl("lblCountry") as Label).Text) ? (e.Row.FindControl("lblCountry") as Label).Text : "0";
                    //ddlGDCountry.SelectedValue = CountryID;

                    //FillStateDLL(ddlGDState, Convert.ToInt32(CountryID), null, null, null);
                    //string StateID = !string.IsNullOrEmpty((e.Row.FindControl("lblState") as Label).Text) ? (e.Row.FindControl("lblState") as Label).Text : "0";
                    //ddlGDState.SelectedValue = StateID;

                    //FillDealerDLL(ddlGDDealer, null, null);
                    //string DealerID = !string.IsNullOrEmpty((e.Row.FindControl("lblDealer") as Label).Text) ? (e.Row.FindControl("lblDealer") as Label).Text : "0";
                    //ddlGDDealer.SelectedValue = DealerID;
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
       

        void CityBind(GridView gv, Label lbl, List<PDMS_Tehsil> LTehsil)
        {
            gv.DataSource = LTehsil;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + LTehsil.Count;
        }

       

        protected void gvCity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //DropDownList ddlGCityCountry = (e.Row.FindControl("ddlGCityCountry") as DropDownList);
                    //DropDownList ddlGCityState = (e.Row.FindControl("ddlGCityState") as DropDownList);
                    //DropDownList ddlGCityDistrict = (e.Row.FindControl("ddlGCityDistrict") as DropDownList);

                    //FillCountryDLL(ddlGCityCountry);
                    //string CountryID = !string.IsNullOrEmpty((e.Row.FindControl("lblCountry") as Label).Text) ? (e.Row.FindControl("lblCountry") as Label).Text : "0";
                    //ddlGCityCountry.SelectedValue = CountryID;

                    //FillStateDLL(ddlGCityState, Convert.ToInt32(CountryID), null, null, null);
                    //string StateID = !string.IsNullOrEmpty((e.Row.FindControl("lblState") as Label).Text) ? (e.Row.FindControl("lblState") as Label).Text : "0";
                    //ddlGCityState.SelectedValue = StateID;

                    //FillDistrictDLL(ddlGCityDistrict, Convert.ToInt32(CountryID), null, Convert.ToInt32(StateID), null, null);
                    //string DistrictID = (e.Row.FindControl("lblDistrict") as Label).Text;
                    //ddlGCityDistrict.SelectedValue = DistrictID;
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

       

        protected void ddlSCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRegionDLL(ddlSRegion, Convert.ToInt32(ddlSCountry.SelectedValue), null, null);
        }
        protected void ddlRCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRegionDLL(ddlRRegion, Convert.ToInt32(ddlRCountry.SelectedValue), null, null);
        }

        protected void ddlDCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStateDLL(ddlDState, Convert.ToInt32(ddlDCountry.SelectedValue), null, null, null);
        }

        protected void ddlDState_SelectedIndexChanged(object sender, EventArgs e)
        { 
        }

       
        protected void ddlGSCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlGSCountry = (DropDownList)sender;
            GridViewRow row = (GridViewRow)(ddlGSCountry.NamingContainer);
            DropDownList ddlGSRegion = (DropDownList)row.FindControl("ddlGSRegion");
            FillRegionDLL(ddlGSRegion, Convert.ToInt32(ddlGSCountry.SelectedValue), null, null);
        }

        protected void ddlGDCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlGDCountry = (DropDownList)sender;
            GridViewRow row = (GridViewRow)(ddlGDCountry.NamingContainer);
            DropDownList ddlGDState = (DropDownList)row.FindControl("ddlGDState");
            DropDownList ddlGDSalesOffice = (DropDownList)row.FindControl("ddlGDSalesOffice");
            DropDownList ddlGDDealer = (DropDownList)row.FindControl("ddlGDDealer");
            FillStateDLL(ddlGDState, Convert.ToInt32(ddlGDCountry.SelectedValue), null, null, null);
            FillSalesOfficeDLL(ddlGDSalesOffice, null, null);
            FillDealerDLL(ddlGDDealer);
        }

        protected void ddlGCityCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlGCityCountry = (DropDownList)sender;
            GridViewRow row = (GridViewRow)(ddlGCityCountry.NamingContainer);
            DropDownList ddlGCityState = (DropDownList)row.FindControl("ddlGCityState");
            FillStateDLL(ddlGCityState, Convert.ToInt32(ddlGCityCountry.SelectedValue), null, null, null);
        }

        protected void ddlGCityState_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlGCityState = (DropDownList)sender;
            GridViewRow row = (GridViewRow)(ddlGCityState.NamingContainer);
            DropDownList ddlGCityCountry = (DropDownList)row.FindControl("ddlGCityCountry");
            DropDownList ddlGCityDistrict = (DropDownList)row.FindControl("ddlGCityDistrict");
            FillDistrictDLL(ddlGCityDistrict, Convert.ToInt32(ddlGCityCountry.SelectedValue), null, Convert.ToInt32(ddlGCityState.SelectedValue), null, null);
        }

        protected void gvDistrict_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //FillGridDistrict();
            gvDistrict.PageIndex = e.NewPageIndex;
            FillGridDistrict();
            gvDistrict.DataBind();
            DropDownList ddlGDCountry = gvDistrict.FooterRow.FindControl("ddlGDCountry") as DropDownList;
            new DDLBind(ddlGDCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");
            DropDownList ddlGDState = gvDistrict.FooterRow.FindControl("ddlGDState") as DropDownList;
            new DDLBind(ddlGDState, new BDMS_Address().GetState(null, null, null, null), "State", "StateID", true, "Select State");
            DropDownList ddlGDSalesOffice = gvDistrict.FooterRow.FindControl("ddlGDSalesOffice") as DropDownList;
            new DDLBind(ddlGDSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");
            DropDownList ddlGDDealer = gvDistrict.FooterRow.FindControl("ddlGDDealer") as DropDownList;
            new DDLBind(ddlGDDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");
            DropDownList ddlSalesEngineer = gvDistrict.FooterRow.FindControl("ddlSalesEngineer") as DropDownList;
            //new DDLBind(ddlSalesEngineer, new BUser().GetUsers(null, null, 7, null, null, true, null, null, null), "ContactName", "UserID", true, "Select Engineer");
            List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, null, true, null, null, 4);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID");
        }

       

        protected void gvState_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //FillGridState();
            gvState.PageIndex = e.NewPageIndex;
            FillGridState();
            gvState.DataBind();

            DropDownList ddlGSCountry = gvState.FooterRow.FindControl("ddlGSCountry") as DropDownList;
            new DDLBind(ddlGSCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");
            DropDownList ddlGSRegion = gvState.FooterRow.FindControl("ddlGSRegion") as DropDownList;
            new DDLBind(ddlGSRegion, new BDMS_Address().GetRegion(null, null, null), "Region", "RegionID", true, "Select Region");
        }

        protected void gvRegion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //FillGridRegion();
            gvRegion.PageIndex = e.NewPageIndex;
            FillGridRegion();
            gvRegion.DataBind();
            DropDownList ddlGRCountry = gvRegion.FooterRow.FindControl("ddlGRCountry") as DropDownList;
            new DDLBind(ddlGRCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");
        }

        protected void gvCountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //FillGridCountry();
            gvCountry.PageIndex = e.NewPageIndex;
            FillGridCountry();
            gvCountry.DataBind();
            DropDownList ddlGCCountryCurrency = gvCountry.FooterRow.FindControl("ddlGCCountryCurrency") as DropDownList;
            new DDLBind(ddlGCCountryCurrency, new BDMS_Address().GetCurrency(null, null), "Currency", "CurrencyID", true, "Select Country Currency");
        }


        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ActionControlMange();
        }
        void ActionControlMange()
        {         
            gvCountry.Columns[5].Visible = false;
            gvRegion.Columns[3].Visible = false;
            gvState.Columns[5].Visible = false;
            gvDistrict.Columns[7].Visible = false;
            gvCountry.ShowFooter = false;
            gvRegion.ShowFooter = false;
            gvState.ShowFooter = false;
            gvDistrict.ShowFooter = false;
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;            
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.AddEditLocation).Count() == 1)
            {
                gvCountry.Columns[5].Visible = true;
                gvRegion.Columns[3].Visible = true;
                gvState.Columns[5].Visible = true;
                gvDistrict.Columns[7].Visible = true;
                gvCountry.ShowFooter = true;
                gvRegion.ShowFooter = true;
                gvState.ShowFooter = true;
                gvDistrict.ShowFooter = true;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EidtDistrictSalesEngineer).Count() == 1)
            {                 
                gvDistrict.Columns[7].Visible = true; 
                gvDistrict.ShowFooter = true;

                foreach (GridViewRow gv in gvDistrict.Rows)
                {
                    //LinkButton lnkBtnDistrictEdit = (LinkButton)gv.FindControl("lnkBtnDistrictEdit");
                    LinkButton lnkBtnDistrictDelete = (LinkButton)gv.FindControl("lnkBtnDistrictDelete");
                    lnkBtnDistrictDelete.Visible = false;
                }
                if (gvDistrict.Rows.Count >= 1)
                {
                    DropDownList ddlGDCountry = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDCountry");
                    DropDownList ddlGDState = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDState");
                    DropDownList ddlGDSalesOffice = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDSalesOffice");
                    DropDownList ddlGDDealer = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDDealer");
                    TextBox txtGDDistrict = (TextBox)gvDistrict.FooterRow.FindControl("txtGDDistrict");

                    ddlGDCountry.Enabled = false;
                    ddlGDState.Enabled = false;
                    ddlGDSalesOffice.Enabled = false;
                    ddlGDDealer.Enabled = false;
                    txtGDDistrict.Enabled = false;
                }
            }             
        }
        //public List<PDMS_Tehsil> LTehsil
        //{
        //    get
        //    {
        //        if (Session["PDMS_Tehsil"] == null)
        //        {
        //            Session["PDMS_Tehsil"] = new List<PDMS_Tehsil>();
        //        }
        //        return (List<PDMS_Tehsil>)Session["PDMS_Tehsil"];
        //    }
        //    set
        //    {
        //        Session["PDMS_Tehsil"] = value;
        //    }
        //}
        //private void FillDistrict()
        //{
        //    try
        //    {
        //        int? CountryID = null, RegionID = null, StateID = null, DistrictID = null, DealerID = null;
        //        string district = null;
        //        if (ddlCityCountry.SelectedValue != "0")
        //        {
        //            CountryID = Convert.ToInt32(ddlCityCountry.SelectedValue);
        //        }
        //        if (ddlCityState.SelectedValue != "0" && !string.IsNullOrEmpty(ddlCityState.SelectedValue))
        //        {
        //            StateID = Convert.ToInt32(ddlCityState.SelectedValue);
        //        } 
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void gvCity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    //FillGridTehsil();
        //    gvCity.PageIndex = e.NewPageIndex;
        //    FillGridTehsil();
        //    gvCity.DataBind();
        //    DropDownList ddlGCityCountry = gvCity.FooterRow.FindControl("ddlGCityCountry") as DropDownList;
        //    new DDLBind(ddlGCityCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");
        //    DropDownList ddlGCityState = gvCity.FooterRow.FindControl("ddlGCityState") as DropDownList;
        //    new DDLBind(ddlGCityState, new BDMS_Address().GetState(null, null, null, null), "State", "StateID", true, "Select State");
        //    DropDownList ddlGCityDistrict = gvCity.FooterRow.FindControl("ddlGCityDistrict") as DropDownList;
        //    new DDLBind(ddlGCityDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null), "District", "DistrictID", true, "Select District");
        //}

        //protected void ddlCityCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillStateDLL(ddlCityState, Convert.ToInt32(ddlCityCountry.SelectedValue), null, null, null);
        //}

        //protected void ddlCityState_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillDistrictDLL(ddlCityDistrict, Convert.ToInt32(ddlCityCountry.SelectedValue), null, Convert.ToInt32(ddlCityState.SelectedValue), null, null);
        //}

        //protected void lnkBtnCityEdit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.Text = string.Empty;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        LinkButton lnkBtnCityEdit = (LinkButton)sender;
        //        DropDownList ddlGCityCountry = (DropDownList)gvCity.FooterRow.FindControl("ddlGCityCountry");
        //        DropDownList ddlGCityState = (DropDownList)gvCity.FooterRow.FindControl("ddlGCityState");
        //        DropDownList ddlGCityDistrict = (DropDownList)gvCity.FooterRow.FindControl("ddlGCityDistrict");
        //        TextBox txtGCity = (TextBox)gvCity.FooterRow.FindControl("txtGCity");
        //        Button BtnAddOrUpdateCity = (Button)gvCity.FooterRow.FindControl("BtnAddOrUpdateCity");
        //        GridViewRow row = (GridViewRow)(lnkBtnCityEdit.NamingContainer);
        //        Label lblGCityCountry = (Label)row.FindControl("lblGCityCountry");
        //        Label lblGCityCountryID = (Label)row.FindControl("lblGCityCountryID");
        //        ddlGCityCountry.SelectedValue = (string.IsNullOrEmpty(lblGCityCountryID.Text)) ? "0" : lblGCityCountryID.Text;
        //        Label lblGCityState = (Label)row.FindControl("lblGCityState");
        //        Label lblGCityStateID = (Label)row.FindControl("lblGCityStateID");
        //        ddlGCityState.SelectedValue = (string.IsNullOrEmpty(lblGCityStateID.Text)) ? "0" : lblGCityStateID.Text;
        //        Label lblGCityDistrict = (Label)row.FindControl("lblGCityDistrict");
        //        Label lblGCityDistrictID = (Label)row.FindControl("lblGCityDistrictID");
        //        ddlGCityDistrict.SelectedValue = (string.IsNullOrEmpty(lblGCityDistrictID.Text)) ? "0" : lblGCityDistrictID.Text;
        //        Label lblGCity = (Label)row.FindControl("lblGCity");
        //        txtGCity.Text = lblGCity.Text;
        //        HiddenID.Value = Convert.ToString(lnkBtnCityEdit.CommandArgument);
        //        BtnAddOrUpdateCity.Text = "Update";
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}

        //protected void lnkBtnCityDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.Text = string.Empty;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        Boolean success = true;
        //        LinkButton lnkBtnCityDelete = (LinkButton)sender;
        //        int TehsilID = Convert.ToInt32(lnkBtnCityDelete.CommandArgument);
        //        GridViewRow row = (GridViewRow)(lnkBtnCityDelete.NamingContainer);
        //        int CountryID = Convert.ToInt32(((Label)row.FindControl("lblGCityCountryID")).Text.Trim());
        //        int StateID = Convert.ToInt32(((Label)row.FindControl("lblGCityStateID")).Text.Trim());
        //        int DistrictID = Convert.ToInt32(((Label)row.FindControl("lblGCityDistrictID")).Text.Trim());
        //        string City = ((Label)row.FindControl("lblGCity")).Text.Trim();

        //        success = new BDMS_Address().InsertOrUpdateAddressTehsil(TehsilID, DistrictID, City, false, PSession.User.UserID);
        //        if (success == true)
        //        {
        //            HiddenID.Value = null;
        //            FillGridTehsil();
        //            lblMessage.Text = "City deleted successfully";
        //            lblMessage.ForeColor = Color.Green;
        //        }
        //        else if (success == false)
        //        {
        //            lblMessage.Text = "City not deleted successfully";
        //            lblMessage.ForeColor = Color.Red;
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}

        //protected void BtnSearchCity_Click(object sender, EventArgs e)
        //{
        //    FillGridTehsil();
        //}

        //protected void ibtnCityArrowLeft_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (gvCity.PageIndex > 0)
        //    {
        //        gvCity.PageIndex = gvCity.PageIndex - 1;
        //        CityBind(gvCity, lblRowCountC, LTehsil);
        //    }
        //}
        //protected void ibtnCityArrowRight_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (gvCity.PageCount > gvCity.PageIndex)
        //    {
        //        gvCity.PageIndex = gvCity.PageIndex + 1;
        //        CityBind(gvCity, lblRowCountC, LTehsil);
        //    }
        //}

        //private void FillGridTehsil()
        //{
        //    try
        //    {
        //        int? CountryID = null, StateID = null, DistrictID = null;
        //        string Tehsil = null;
        //        if (ddlCityCountry.SelectedValue != "0")
        //        {
        //            CountryID = Convert.ToInt32(ddlCityCountry.SelectedValue);
        //        }
        //        if (ddlCityState.SelectedValue != "0" && !string.IsNullOrEmpty(ddlCityState.SelectedValue))
        //        {
        //            StateID = Convert.ToInt32(ddlCityState.SelectedValue);
        //        }
        //        if (ddlCityDistrict.SelectedValue != "0" && !string.IsNullOrEmpty(ddlCityDistrict.SelectedValue))
        //        {
        //            DistrictID = Convert.ToInt32(ddlCityDistrict.SelectedValue);
        //        }
        //        if (!string.IsNullOrEmpty(txtCity.Text))
        //        {
        //            Tehsil = txtCity.Text.Trim();
        //        }

        //        //List<PDMS_Tehsil> MML = new BDMS_Address().GetTehsil(CountryID, StateID, DistrictID, Tehsil);

        //        LTehsil = new BDMS_Address().GetTehsil(CountryID, StateID, DistrictID, Tehsil);
        //        if (LTehsil.Count == 0)
        //        {
        //            LTehsil.Add(new PDMS_Tehsil());
        //        }
        //        gvCity.DataSource = LTehsil;
        //        gvCity.DataBind();
        //        //throw new NotImplementedException();

        //        if (LTehsil.Count == 0)
        //        {
        //            lblRowCountC.Visible = false;
        //            ibtnCityArrowLeft.Visible = false;
        //            ibtnCityArrowRight.Visible = false;
        //        }
        //        else
        //        {
        //            lblRowCountC.Visible = true;
        //            ibtnCityArrowLeft.Visible = true;
        //            ibtnCityArrowRight.Visible = true;
        //            lblRowCountC.Text = (((gvCity.PageIndex) * gvCity.PageSize) + 1) + " - " + (((gvCity.PageIndex) * gvCity.PageSize) + gvCity.Rows.Count) + " of " + LTehsil.Count;
        //        }


        //        DropDownList ddlGCityCountry = gvCity.FooterRow.FindControl("ddlGCityCountry") as DropDownList;
        //        new DDLBind(ddlGCityCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");

        //        DropDownList ddlGCityState = gvCity.FooterRow.FindControl("ddlGCityState") as DropDownList;
        //        new DDLBind(ddlGCityState, new BDMS_Address().GetState(null, null, null, null), "State", "StateID", true, "Select State");

        //        DropDownList ddlGCityDistrict = gvCity.FooterRow.FindControl("ddlGCityDistrict") as DropDownList;
        //        new DDLBind(ddlGCityDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null), "District", "DistrictID", true, "Select District");

        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void BtnAddOrUpdateCity_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.Text = string.Empty;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        Boolean Success = true;
        //        Button BtnAddOrUpdateCity = (Button)gvCity.FooterRow.FindControl("BtnAddOrUpdateCity");

        //        DropDownList ddlGCityCountry = (DropDownList)gvCity.FooterRow.FindControl("ddlGCityCountry");
        //        if (ddlGCityCountry.SelectedValue == "0")
        //        {
        //            lblMessage.Text = "Please select Country.";
        //            lblMessage.ForeColor = Color.Red;
        //            return;
        //        }
        //        DropDownList ddlGCityState = (DropDownList)gvCity.FooterRow.FindControl("ddlGCityState");
        //        if (ddlGCityState.SelectedValue == "0")
        //        {
        //            lblMessage.Text = "Please select State.";
        //            lblMessage.ForeColor = Color.Red;
        //            return;
        //        }
        //        DropDownList ddlGCityDistrict = (DropDownList)gvCity.FooterRow.FindControl("ddlGCityDistrict");
        //        if (ddlGCityDistrict.SelectedValue == "0")
        //        {
        //            lblMessage.Text = "Please select District.";
        //            lblMessage.ForeColor = Color.Red;
        //            return;
        //        }
        //        string City = ((TextBox)gvCity.FooterRow.FindControl("txtGCity")).Text.Trim();
        //        if (string.IsNullOrEmpty(City))
        //        {
        //            lblMessage.Text = "Please enter City.";
        //            lblMessage.ForeColor = Color.Red;
        //            return;
        //        }

        //        if (BtnAddOrUpdateCity.Text == "Add")
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressTehsil(null, Convert.ToInt32(ddlGCityDistrict.SelectedValue), City, true, PSession.User.UserID);
        //            if (Success == true)
        //            {
        //                FillGridTehsil();
        //                lblMessage.Text = "City is added successfully.";
        //                lblMessage.ForeColor = Color.Green;
        //                return;
        //            }
        //            else if (Success == false)
        //            {
        //                lblMessage.Text = "City is already found.";
        //                lblMessage.ForeColor = Color.Red;
        //                return;
        //            }
        //            else
        //            {
        //                lblMessage.Text = "City not created successfully.";
        //                lblMessage.ForeColor = Color.Red;
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressTehsil(Convert.ToInt32(HiddenID.Value), Convert.ToInt32(ddlGCityDistrict.SelectedValue), City, true, PSession.User.UserID);

        //            if (Success == true)
        //            {
        //                HiddenID.Value = null;
        //                FillGridTehsil();
        //                lblMessage.Text = "City successfully updated.";
        //                lblMessage.ForeColor = Color.Green;
        //                return;
        //            }
        //            else if (Success == false)
        //            {
        //                lblMessage.Text = "City already found";
        //                lblMessage.ForeColor = Color.Red;
        //                return;
        //            }
        //            else
        //            {
        //                lblMessage.Text = "City not updated successfully...!";
        //                lblMessage.ForeColor = Color.Red;
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}

    }
}