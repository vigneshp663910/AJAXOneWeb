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

        public List<PDMS_Tehsil> LTehsil
        {
            get
            {
                if (Session["PDMS_Tehsil"] == null)
                {
                    Session["PDMS_Tehsil"] = new List<PDMS_Tehsil>();
                }
                return (List<PDMS_Tehsil>)Session["PDMS_Tehsil"];
            }
            set
            {
                Session["PDMS_Tehsil"] = value;
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
        public List<PDMS_Dealer> LDealer
        {
            get
            {
                if (Session["PDMS_Dealer"] == null)
                {
                    Session["PDMS_Dealer"] = new List<PDMS_Dealer>();
                }
                return (List<PDMS_Dealer>)Session["PDMS_Dealer"];
            }
            set
            {
                Session["PDMS_Dealer"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Location');</script>");

            if (!IsPostBack)
            {
                try
                {
                    Label lblProjectTitle = (Label)Master.FindControl("lblProjectTitle");
                    lblProjectTitle.Text = "Location";
                    LCountry = new BDMS_Address().GetCountry(null, null);
                    LRegion = new BDMS_Address().GetRegion(null, null, null);
                    LState = new BDMS_Address().GetState(null, null, null, null);
                    LSalesOffice = new BDMS_Address().GetSalesOffice(null, null);
                    LDistrict = new BDMS_Address().GetDistrict(null, null, null, null, null, null);
                    LDealer = new BDMS_Dealer().GetDealer(null, null);
                    FillCountry();
                    FillRegion();
                    FillState();
                    FillDistrict();
                    FillDealer();
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
                FillCountryDLL(ddlRCountry);
                FillCountryDLL(ddlSCountry);
                FillCountryDLL(ddlDCountry);
                FillCountryDLL(ddlCityCountry);
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
                LDistrict = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID, null, null);
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
        private void FillDealerDLL(DropDownList ddl, int? DealerID, string DealerCode)
        {
            try
            {
                LDealer = new BDMS_Dealer().GetDealer(DealerID, DealerCode);
                ddl.DataValueField = "DealerID";
                ddl.DataTextField = "DealerCode";
                ddl.DataSource = LDealer;
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
                    FillRegionDLL(ddlRRegion, CountryID, null, null);
                }
                if (ddlSCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSCountry.SelectedValue);
                }
                if (ddlSCountry.SelectedValue != "0")
                {
                    FillRegionDLL(ddlSRegion, CountryID, null, null);
                }
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
                FillStateDLL(ddlDState, CountryID, null, null, null);

                CountryID = null; RegionID = null;
                if (ddlCityCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlCityCountry.SelectedValue);
                }
                FillStateDLL(ddlCityState, CountryID, null, null, null);
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillDistrict()
        {
            try
            {
                int? CountryID = null, RegionID = null, StateID = null, DistrictID = null, DealerID = null;
                string district = null;
                if (ddlCityCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlCityCountry.SelectedValue);
                }
                if (ddlCityState.SelectedValue != "0")
                {
                    StateID = Convert.ToInt32(ddlCityState.SelectedValue);
                }
                FillDistrictDLL(ddlCityDistrict, CountryID, RegionID, StateID, DistrictID,  district);
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
                
                FillDealerDLL(ddlDDealer, DealerID, DealerCode);
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
                if (ddlDState.SelectedValue != "0")
                {
                    StateID = Convert.ToInt32(ddlDState.SelectedValue);
                }
                if (ddlDDealer.SelectedValue != "0")
                {
                    DealerID = Convert.ToInt32(ddlDDealer.SelectedValue);
                }

                if (!string.IsNullOrEmpty(txtDistrict.Text))
                {
                    District = txtDistrict.Text.Trim();
                }
                
                //List<PDMS_District> MML = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID,  District, null);

                LDistrict = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID, District, null);
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
                new DDLBind(ddlGDDealer, new BDMS_Dealer().GetDealer(null, null), "DealerName", "DealerID", true, "Select Dealer");
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

        //protected void BtnAddCountry_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        lblMessage.Text = string.Empty;
        //        Boolean Success = true;
        //        string Message = "";

        //        if (string.IsNullOrEmpty(txtCountry.Text.Trim()))
        //        {
        //            Message = Message + "<br/> Please Enter the Country...!";
        //            Success = false;
        //        }
        //        List<PDMS_Country> MML = new BDMS_Address().GetCountry(null, txtCountry.Text.Trim());
        //        if (MML.Count > 0)
        //        {
        //            Message = Message + "<br/> Country is already found...!";
        //            Success = false;
        //        }
        //        lblMessage.Text = Message;
        //        if (Success == false)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressCountry(null, txtCountry.Text.Trim(), null, true, PSession.User.UserID);
        //            if (Success == true)
        //            {
        //                lblMessage.Text = "Country is Added successfully";
        //                lblMessage.ForeColor = Color.Green;
        //                FillGridCountry();
        //            }
        //            else
        //            {
        //                lblMessage.Text = "Country is not Added successfully";
        //                lblMessage.ForeColor = Color.Red;
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

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
                if (BtnAddOrUpdateCountry.Text == "Add")
                {
                    //Success = new BDMS_Address().InsertOrUpdateAddressCountry(null, Country, CountryCode, CountryCurrencyID, SalesOrganization, true, PSession.User.UserID);
                    Success = new BDMS_Address().InsertOrUpdateAddressCountry(null, Country, CountryCode, CountryCurrencyID, ddlGCSalesOrganization.SelectedValue, true, PSession.User.UserID);
                    if (Success == true)
                    {
                        FillGridCountry();
                        FillCountryDLL();
                        lblMessage.Text = "Country is added successfully.";
                        lblMessage.ForeColor = Color.Green;
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
                    //Success = new BDMS_Address().InsertOrUpdateAddressCountry(Convert.ToInt32(HiddenID.Value), Country, CountryCode, CountryCurrencyID, SalesOrganization, true, PSession.User.UserID);
                    Success = new BDMS_Address().InsertOrUpdateAddressCountry(Convert.ToInt32(HiddenID.Value), Country, CountryCode, CountryCurrencyID, ddlGCSalesOrganization.SelectedValue, true, PSession.User.UserID);
                    if (Success == true)
                    {
                        HiddenID.Value = null;
                        FillGridCountry();
                        lblMessage.Text = "Country successfully updated.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "Country already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Country not updated successfully...!";
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

        //protected void BtnAddCountry_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        lblMessage.Text = string.Empty;
        //        Boolean Success = true;
        //        string Message = "";

        //        if (string.IsNullOrEmpty(txtCountry.Text.Trim()))
        //        {
        //            Message = Message + "<br/> Please Enter the Country...!";
        //            Success = false;
        //        }
        //        List<PDMS_Country> MML = new BDMS_Address().GetCountry(null, txtCountry.Text.Trim());
        //        if (MML.Count > 0)
        //        {
        //            Message = Message + "<br/> Country is already found...!";
        //            Success = false;
        //        }
        //        lblMessage.Text = Message;
        //        if (Success == false)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressCountry(null, txtCountry.Text.Trim(), null, true, PSession.User.UserID);
        //            if (Success == true)
        //            {
        //                lblMessage.Text = "Country is Added successfully";
        //                lblMessage.ForeColor = Color.Green;
        //                FillGridCountry();
        //            }
        //            else
        //            {
        //                lblMessage.Text = "Country is not Added successfully";
        //                lblMessage.ForeColor = Color.Red;
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        protected void BtnSearchCountry_Click(object sender, EventArgs e)
        {
            FillGridCountry();
            lblMessage.Text = "";
        }


        //protected void ImageCEdit_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        ImageButton ImageCEdit = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageCEdit.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageCEdit.NamingContainer);
        //        ImageButton ImageCUpdate = (ImageButton)row.FindControl("ImageCUpdate");
        //        Label lblGCCountry = (Label)row.FindControl("lblGCCountry");
        //        TextBox txtGCCountry = (TextBox)row.FindControl("txtGCCountry");
        //        lblGCCountry.Visible = false;
        //        txtGCCountry.Visible = true;
        //        ImageCUpdate.Visible = true;
        //        ImageCEdit.Visible = false;
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void ImageCUpdate_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        string Message = "";
        //        Boolean Success = true;
        //        ImageButton ImageCUpdate = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageCUpdate.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageCUpdate.NamingContainer);
        //        ImageButton ImageCEdit = (ImageButton)row.FindControl("ImageCEdit");
        //        TextBox txtGCCountry = (TextBox)row.FindControl("txtGCCountry");
        //        if (string.IsNullOrEmpty(txtGCCountry.Text.Trim()))
        //        {
        //            Message = Message + "<br/> Please Enter the Country";
        //            Success = false;
        //        }
        //        List<PDMS_Country> MML = new BDMS_Address().GetCountry(null, txtGCCountry.Text.Trim());
        //        if (MML.Count > 0)
        //        {
        //            Message = Message + "<br/> Country is already found...!";
        //            Success = false;
        //        }
        //        lblMessage.Text = Message;
        //        if (Success == false)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressCountry(Convert.ToInt32(id), txtGCCountry.Text.Trim(), null, true, PSession.User.UserID);
        //            if (Success == false)
        //            {
        //                lblMessage.Text = "Country is not successfully updated";
        //                lblMessage.ForeColor = Color.Red;
        //                lblMessage.Visible = true;

        //            }
        //            else
        //            {
        //                lblMessage.Text = "Country was successfully updated.";
        //                lblMessage.ForeColor = Color.Green;
        //                lblMessage.Visible = true;
        //                txtGCCountry.Enabled = false;
        //                ImageCUpdate.Visible = false;
        //                ImageCEdit.Visible = true;
        //                FillGridCountry();
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}


        //protected void ImageCDelete_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.Text = "";
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        Boolean Success = true;
        //        ImageButton ImageCDelete = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageCDelete.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageCDelete.NamingContainer);
        //        TextBox txtGCCountry = (TextBox)row.FindControl("txtGCCountry");

        //        Success = new BDMS_Address().InsertOrUpdateAddressCountry(Convert.ToInt32(id), txtGCCountry.Text.Trim(), null, false, PSession.User.UserID);
        //        if (Success == false)
        //        {
        //            lblMessage.Text = "Country is not successfully Deleted";
        //            lblMessage.ForeColor = Color.Red;
        //            lblMessage.Visible = true;

        //        }
        //        else
        //        {
        //            lblMessage.Text = "Country was successfully Deleted.";
        //            lblMessage.ForeColor = Color.Green;
        //            lblMessage.Visible = true;
        //            FillGridCountry();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

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

        //protected void BtnAddRegion_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        lblMessage.Text = string.Empty;
        //        Boolean Success = true;
        //        string Message = "";

        //        if (ddlRCountry.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select Country";
        //            Success = false;
        //        }
        //        if (string.IsNullOrEmpty(txtRRegion.Text.Trim()))
        //        {
        //            Message = Message + "<br/> Please Enter the Region";
        //            Success = false;
        //        }
        //        List<PDMS_Region> MML = new BDMS_Address().GetRegion(Convert.ToInt32(ddlRCountry.SelectedValue), null, txtRRegion.Text.Trim());
        //        if (MML.Count > 0)
        //        {
        //            Message = Message + "<br/> Region is already found...!";
        //            Success = false;
        //        }
        //        lblMessage.Text = Message;
        //        if (Success == false)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressRegion(null, txtRRegion.Text.Trim(), Convert.ToInt32(ddlRCountry.SelectedValue), true, PSession.User.UserID);
        //            if (Success == true)
        //            {
        //                lblMessage.Text = "Region is Added successfully";
        //                lblMessage.ForeColor = Color.Green;
        //                FillGridRegion();
        //            }
        //            else
        //            {
        //                lblMessage.Text = "Region is not Added successfully";
        //                lblMessage.ForeColor = Color.Red;
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}


        protected void lnkBtnRegionEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnRegionEdit = (LinkButton)sender;
                DropDownList ddlGRCountry = (DropDownList)gvRegion.FooterRow.FindControl("ddlGRCountry");
                TextBox txtGRRegion = (TextBox)gvRegion.FooterRow.FindControl("txtGRRegion");
                Button BtnAddOrUpdateRegion = (Button)gvRegion.FooterRow.FindControl("BtnAddOrUpdateRegion");
                GridViewRow row = (GridViewRow)(lnkBtnRegionEdit.NamingContainer);
                Label lblGRCountryID = (Label)row.FindControl("lblGRCountryID");
                ddlGRCountry.SelectedValue = lblGRCountryID.Text;
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
                    Success = new BDMS_Address().InsertOrUpdateAddressRegion(null, Region, Convert.ToInt32(ddlRCountry.SelectedValue), true, PSession.User.UserID);
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

        //protected void ImageREdit_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.Text = "";
        //        ImageButton ImageREdit = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageREdit.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageREdit.NamingContainer);
        //        ImageButton ImageRUpdate = (ImageButton)row.FindControl("ImageRUpdate");
        //        DropDownList ddlGRCountry = (DropDownList)row.FindControl("ddlGRCountry");
        //        TextBox txtGRRegion = (TextBox)row.FindControl("txtGRRegion");
        //        ddlGRCountry.Enabled = true;
        //        txtGRRegion.Enabled = true;
        //        ImageRUpdate.Visible = true;
        //        ImageREdit.Visible = false;
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void ImageRUpdate_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        string Message = "";
        //        Boolean Success = true;
        //        ImageButton ImageRUpdate = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageRUpdate.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageRUpdate.NamingContainer);
        //        ImageButton ImageREdit = (ImageButton)row.FindControl("ImageREdit");
        //        DropDownList ddlGRCountry = (DropDownList)row.FindControl("ddlGRCountry");
        //        TextBox txtGRRegion = (TextBox)row.FindControl("txtGRRegion");
        //        if (ddlGRCountry.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select Country";
        //            Success = false;
        //        }
        //        if (string.IsNullOrEmpty(txtGRRegion.Text.Trim()))
        //        {
        //            Message = Message + "<br/> Please Enter the Region";
        //            Success = false;
        //        }
        //        List<PDMS_Region> MML = new BDMS_Address().GetRegion(Convert.ToInt32(ddlGRCountry.SelectedValue), null, txtGRRegion.Text.Trim());
        //        if (MML.Count > 0)
        //        {
        //            Message = Message + "<br/> Region is already found...!";
        //            Success = false;
        //        }
        //        lblMessage.Text = Message;
        //        if (Success == false)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressRegion(Convert.ToInt32(id), txtGRRegion.Text.Trim(), Convert.ToInt32(ddlGRCountry.SelectedValue), true, PSession.User.UserID);
        //            if (Success == false)
        //            {
        //                lblMessage.Text = "Region is not successfully updated";
        //                lblMessage.ForeColor = Color.Red;
        //                lblMessage.Visible = true;

        //            }
        //            else
        //            {
        //                lblMessage.Text = "Region was successfully updated.";
        //                lblMessage.ForeColor = Color.Green;
        //                lblMessage.Visible = true;
        //                ddlGRCountry.Enabled = false;
        //                txtGRRegion.Enabled = false;
        //                ImageRUpdate.Visible = false;
        //                ImageREdit.Visible = true;
        //                FillGridRegion();
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}


        //protected void ImageRDelete_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.Text = "";
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        Boolean Success = true;
        //        ImageButton ImageRDelete = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageRDelete.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageRDelete.NamingContainer);
        //        DropDownList ddlGRCountry = (DropDownList)row.FindControl("ddlGRCountry");
        //        TextBox txtGRRegion = (TextBox)row.FindControl("txtGRRegion");

        //        Success = new BDMS_Address().InsertOrUpdateAddressRegion(Convert.ToInt32(id), txtGRRegion.Text.Trim(), Convert.ToInt32(ddlGRCountry.SelectedValue), false, PSession.User.UserID);
        //        if (Success == false)
        //        {
        //            lblMessage.Text = "Region is not successfully Deleted";
        //            lblMessage.ForeColor = Color.Red;
        //            lblMessage.Visible = true;

        //        }
        //        else
        //        {
        //            lblMessage.Text = "Region was successfully Deleted.";
        //            lblMessage.ForeColor = Color.Green;
        //            lblMessage.Visible = true;
        //            FillGridRegion();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}
        //protected void BtnAddState_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        lblMessage.Text = string.Empty;
        //        Boolean Success = true;
        //        string Message = "";

        //        if (ddlSCountry.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select Country";
        //            Success = false;
        //        }
        //        if (ddlSRegion.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select Region";
        //            Success = false;
        //        }
        //        if (string.IsNullOrEmpty(txtState.Text.Trim()))
        //        {
        //            Message = Message + "<br/> Please Enter the State";
        //            Success = false;
        //        }
        //        if (string.IsNullOrEmpty(txtStateCode.Text.Trim()))
        //        {
        //            Message = Message + "<br/> Please Enter the StateCode";
        //            Success = false;
        //        }
        //        List<PDMS_State> MML = new BDMS_Address().GetState(Convert.ToInt32(ddlSCountry.SelectedValue), Convert.ToInt32(ddlSRegion.SelectedValue), null, txtStateCode.Text.Trim());
        //        if (MML.Count > 0)
        //        {
        //            Message = Message + "<br/> State is already found...!";
        //            Success = false;
        //        }
        //        lblMessage.Text = Message;
        //        if (Success == false)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressState(null, txtState.Text.Trim(), txtStateCode.Text.Trim(), null, Convert.ToInt32(ddlSCountry.SelectedValue), true, Convert.ToInt32(ddlSRegion.SelectedValue), PSession.User.UserID);
        //            if (Success == true)
        //            {
        //                lblMessage.Text = "State is Added successfully";
        //                lblMessage.ForeColor = Color.Green;
        //                FillGridState();
        //            }
        //            else
        //            {
        //                lblMessage.Text = "State is not Added successfully";
        //                lblMessage.ForeColor = Color.Red;
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}
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
                if (string.IsNullOrEmpty(State))
                {
                    lblMessage.Text = "Please enter State Code.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddOrUpdateRegion.Text == "Add")
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressState(null, State, StateCode, null, Convert.ToInt32(ddlSCountry.SelectedValue), true, Convert.ToInt32(ddlSRegion.SelectedValue), PSession.User.UserID);
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

        //protected void ImageSEdit_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.Text = "";
        //        ImageButton ImageSEdit = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageSEdit.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageSEdit.NamingContainer);
        //        ImageButton ImageSUpdate = (ImageButton)row.FindControl("ImageSUpdate");
        //        DropDownList ddlGSCountry = (DropDownList)row.FindControl("ddlGSCountry");
        //        DropDownList ddlGSRegion = (DropDownList)row.FindControl("ddlGSRegion");
        //        TextBox txtGRState = (TextBox)row.FindControl("txtGRState");
        //        TextBox txtGRStateCode = (TextBox)row.FindControl("txtGRStateCode");
        //        ddlGSCountry.Enabled = true;
        //        ddlGSRegion.Enabled = true;
        //        txtGRState.Enabled = true;
        //        txtGRStateCode.Enabled = true;
        //        ImageSUpdate.Visible = true;
        //        ImageSEdit.Visible = false;
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}


        //protected void ImageSUpdate_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        string Message = "";
        //        Boolean Success = true;
        //        ImageButton ImageSUpdate = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageSUpdate.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageSUpdate.NamingContainer);
        //        ImageButton ImageSEdit = (ImageButton)row.FindControl("ImageSEdit");
        //        DropDownList ddlGSCountry = (DropDownList)row.FindControl("ddlGSCountry");
        //        DropDownList ddlGSRegion = (DropDownList)row.FindControl("ddlGSRegion");
        //        TextBox txtGRState = (TextBox)row.FindControl("txtGRState");
        //        TextBox txtGRStateCode = (TextBox)row.FindControl("txtGRStateCode");
        //        if (ddlGSCountry.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select Country";
        //            Success = false;
        //        }
        //        if (ddlGSRegion.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select Region";
        //            Success = false;
        //        }
        //        if (string.IsNullOrEmpty(txtGRState.Text.Trim()))
        //        {
        //            Message = Message + "<br/> Please Enter the State";
        //            Success = false;
        //        }
        //        List<PDMS_State> MML = new BDMS_Address().GetState(Convert.ToInt32(ddlGSCountry.SelectedValue), Convert.ToInt32(ddlGSRegion.SelectedValue), null, txtGRState.Text.Trim());
        //        if (MML.Count > 0)
        //        {
        //            Message = Message + "<br/> State is already found...!";
        //            Success = false;
        //        }
        //        lblMessage.Text = Message;
        //        if (Success == false)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressState(Convert.ToInt32(id), txtGRState.Text.Trim(), txtGRStateCode.Text.Trim(), null, Convert.ToInt32(ddlGSCountry.SelectedValue), true, Convert.ToInt32(ddlGSRegion.SelectedValue), PSession.User.UserID);
        //            if (Success == false)
        //            {
        //                lblMessage.Text = "State is not successfully updated";
        //                lblMessage.ForeColor = Color.Red;
        //                lblMessage.Visible = true;

        //            }
        //            else
        //            {
        //                lblMessage.Text = "State was successfully updated.";
        //                lblMessage.ForeColor = Color.Green;
        //                lblMessage.Visible = true;
        //                ddlGSCountry.Enabled = false;
        //                ddlGSRegion.Enabled = false;
        //                txtGRState.Enabled = false;
        //                txtGRStateCode.Enabled = false;
        //                ImageSUpdate.Visible = false;
        //                ImageSEdit.Visible = true;
        //                FillGridState();
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void ImageSDelete_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.Text = "";
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        Boolean Success = true;
        //        ImageButton ImageSDelete = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageSDelete.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageSDelete.NamingContainer);
        //        DropDownList ddlGSCountry = (DropDownList)row.FindControl("ddlGSCountry");
        //        DropDownList ddlGSRegion = (DropDownList)row.FindControl("ddlGSRegion");
        //        TextBox txtGRState = (TextBox)row.FindControl("txtGRState");
        //        TextBox txtGRStateCode = (TextBox)row.FindControl("txtGRStateCode");

        //        Success = new BDMS_Address().InsertOrUpdateAddressState(Convert.ToInt32(id), txtGRState.Text.Trim(), txtGRStateCode.Text.Trim(), null, Convert.ToInt32(ddlGSCountry.SelectedValue), false, Convert.ToInt32(ddlGSRegion.SelectedValue), PSession.User.UserID);
        //        if (Success == false)
        //        {
        //            lblMessage.Text = "State is not successfully Deleted";
        //            lblMessage.ForeColor = Color.Red;
        //            lblMessage.Visible = true;

        //        }
        //        else
        //        {
        //            lblMessage.Text = "State was successfully Deleted.";
        //            lblMessage.ForeColor = Color.Green;
        //            lblMessage.Visible = true;
        //            FillGridState();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        protected void lnkBtnStateEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnStateEdit = (LinkButton)sender;
                DropDownList ddlGSCountry = (DropDownList)gvState.FooterRow.FindControl("ddlGSCountry");
                DropDownList ddlGSRegion = (DropDownList)gvState.FooterRow.FindControl("ddlGSRegion");
                TextBox txtGSState = (TextBox)gvState.FooterRow.FindControl("txtGSState");
                TextBox txtGSStateCode = (TextBox)gvState.FooterRow.FindControl("txtGSStateCode");
                Button BtnAddOrUpdateState = (Button)gvState.FooterRow.FindControl("BtnAddOrUpdateState");
                GridViewRow row = (GridViewRow)(lnkBtnStateEdit.NamingContainer);
                Label lblGSCountry = (Label)row.FindControl("lblGSCountry");
                ddlGSCountry.SelectedValue = lblGSCountry.Text;
                Label lblGSRegion = (Label)row.FindControl("lblGSRegion");
                ddlGSRegion.SelectedValue = lblGSRegion.Text;
                Label lblGSState = (Label)row.FindControl("lblGSState");
                txtGSState.Text = lblGSState.Text;
                Label lblGSStateCode = (Label)row.FindControl("lblGSStateCode");
                txtGSState.Text = lblGSStateCode.Text;
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

                success = new BDMS_Address().InsertOrUpdateAddressState(CountryID, State, StateCode, null, CountryID, false, RegionID, PSession.User.UserID);
                if (success == true)
                {
                    HiddenID.Value = null;
                    FillGridCountry();
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

        //protected void BtnAddDistrict_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        lblMessage.Text = string.Empty;
        //        Boolean Success = true;
        //        string Message = "";
        //        if (ddlDCountry.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select Country";
        //            Success = false;
        //        }
        //        if (ddlDState.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select State";
        //            Success = false;
        //        }
        //        if (ddlDDealer.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select DealerCode";
        //            Success = false;
        //        }
        //        if (string.IsNullOrEmpty(txtDistrict.Text.Trim()))
        //        {
        //            Message = Message + "<br/> Please Enter the District";
        //            Success = false;
        //        }
        //        List<PDMS_District> MML = new BDMS_Address().GetDistrict(Convert.ToInt32(ddlDCountry.SelectedValue), null, Convert.ToInt32(ddlDState.SelectedValue), null, txtDistrict.Text.Trim());
        //        if (MML.Count > 0)
        //        {
        //            Message = Message + "<br/> District is already found...!";
        //            Success = false;
        //        }
        //        lblMessage.Text = Message;
        //        if (Success == false)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressDistrict(null, Convert.ToInt32(ddlDState.SelectedValue), Convert.ToInt32(ddlDDealer.SelectedValue), txtDistrict.Text.Trim(), null, true, PSession.User.UserID);
        //            if (Success == true)
        //            {
        //                lblMessage.Text = "District is Added successfully";
        //                lblMessage.ForeColor = Color.Green;
        //                FillGridDistrict();
        //            }
        //            else
        //            {
        //                lblMessage.Text = "District is not Added successfully";
        //                lblMessage.ForeColor = Color.Red;
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}
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
                DropDownList ddlGDState = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDState");
                DropDownList ddlGDDealer = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDDealer");
                TextBox txtGDDistrict = (TextBox)gvDistrict.FooterRow.FindControl("txtGDDistrict");
                Button BtnAddOrUpdateDistrict = (Button)gvDistrict.FooterRow.FindControl("BtnAddOrUpdateDistrict");
                GridViewRow row = (GridViewRow)(lnkBtnDistrictEdit.NamingContainer);
                Label lblGDCountry = (Label)row.FindControl("lblGDCountry");
                ddlGDCountry.SelectedValue = lblGDCountry.Text;
                Label lblGDState = (Label)row.FindControl("lblGDState");
                ddlGDState.SelectedValue = lblGDState.Text;
                Label lblGDDealer = (Label)row.FindControl("lblGDDealer");
                ddlGDDealer.Text = lblGDDealer.Text;
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
                int DealerID = Convert.ToInt32(((Label)row.FindControl("lblGDDealerID")).Text.Trim());
                int SalesOfficeID = Convert.ToInt32(((Label)row.FindControl("lblGDSalesOfficeID")).Text.Trim());
                string District = ((Label)row.FindControl("lblGDDistrict")).Text.Trim();

                success = new BDMS_Address().InsertOrUpdateAddressDistrict(DistrictID, CountryID, StateID, DealerID, SalesOfficeID, District, null, false, PSession.User.UserID);
                if (success == true)
                {
                    HiddenID.Value = null;
                    FillGridCountry();
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
                string District = ((TextBox)gvDistrict.FooterRow.FindControl("txtGDDistrict")).Text.Trim();
                if (string.IsNullOrEmpty(District))
                {
                    lblMessage.Text = "Please enter District.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddOrUpdateDistrict.Text == "Add")
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressDistrict(null, Convert.ToInt32(ddlGDCountry.SelectedValue), Convert.ToInt32(ddlGDState.SelectedValue), Convert.ToInt32(ddlGDDealer.SelectedValue), Convert.ToInt32(ddlGDSalesOffice.SelectedValue), District, null, true, PSession.User.UserID);
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
                    Success = new BDMS_Address().InsertOrUpdateAddressDistrict(Convert.ToInt32(HiddenID.Value), Convert.ToInt32(ddlGDCountry.SelectedValue), Convert.ToInt32(ddlGDState.SelectedValue), Convert.ToInt32(ddlGDDealer.SelectedValue), Convert.ToInt32(ddlGDSalesOffice.SelectedValue), District, null, true, PSession.User.UserID);

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

        //protected void ImageDEdit_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.Text = "";
        //        ImageButton ImageDEdit = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageDEdit.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageDEdit.NamingContainer);
        //        ImageButton ImageDUpdate = (ImageButton)row.FindControl("ImageDUpdate");
        //        DropDownList ddlGDCountry = (DropDownList)row.FindControl("ddlGDCountry");
        //        DropDownList ddlGDState = (DropDownList)row.FindControl("ddlGDState");
        //        DropDownList ddlGDDealer = (DropDownList)row.FindControl("ddlGDDealer");
        //        TextBox txtGDDistrict = (TextBox)row.FindControl("txtGDDistrict");
        //        ddlGDCountry.Enabled = true;
        //        ddlGDState.Enabled = true;
        //        ddlGDDealer.Enabled = true;
        //        txtGDDistrict.Enabled = true;
        //        ImageDUpdate.Visible = true;
        //        ImageDEdit.Visible = false;
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void ImageDUpdate_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        string Message = "";
        //        Boolean Success = true;
        //        ImageButton ImageDUpdate = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageDUpdate.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageDUpdate.NamingContainer);
        //        ImageButton ImageDEdit = (ImageButton)row.FindControl("ImageDEdit");
        //        DropDownList ddlGDCountry = (DropDownList)row.FindControl("ddlGDCountry");
        //        DropDownList ddlGDState = (DropDownList)row.FindControl("ddlGDState");
        //        DropDownList ddlGDDealer = (DropDownList)row.FindControl("ddlGDDealer");
        //        TextBox txtGDDistrict = (TextBox)row.FindControl("txtGDDistrict");

        //        if (ddlGDCountry.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select the Country";
        //            Success = false;
        //        }
        //        if (ddlGDState.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select the State";
        //            Success = false;
        //        }
        //        if (ddlGDDealer.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select the DealerCode";
        //            Success = false;
        //        }
        //        if (string.IsNullOrEmpty(txtGDDistrict.Text.Trim()))
        //        {
        //            Message = Message + "<br/> Please Enter the District";
        //            Success = false;
        //        }
        //        List<PDMS_District> MML = new BDMS_Address().GetDistrict(Convert.ToInt32(ddlGDCountry.SelectedValue), null, Convert.ToInt32(ddlGDState.SelectedValue), null, txtGDDistrict.Text.Trim());
        //        if (MML.Count > 0)
        //        {
        //            Message = Message + "<br/> District is already found...!";
        //            Success = false;
        //        }
        //        lblMessage.Text = Message;
        //        if (Success == false)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressDistrict(Convert.ToInt32(id), Convert.ToInt32(ddlGDState.SelectedValue), Convert.ToInt32(ddlGDDealer.SelectedValue), txtGDDistrict.Text.Trim(), null, true, PSession.User.UserID);
        //            if (Success == false)
        //            {
        //                lblMessage.Text = "District is not successfully updated";
        //                lblMessage.ForeColor = Color.Red;
        //                lblMessage.Visible = true;

        //            }
        //            else
        //            {
        //                lblMessage.Text = "District was successfully updated.";
        //                lblMessage.ForeColor = Color.Green;
        //                lblMessage.Visible = true;
        //                ddlGDCountry.Enabled = false;
        //                ddlGDState.Enabled = false;
        //                ddlGDDealer.Enabled = false;
        //                txtGDDistrict.Enabled = false;
        //                ImageDUpdate.Visible = false;
        //                ImageDEdit.Visible = true;
        //                FillGridDistrict();
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void ImageDDelete_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.Text = "";
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        Boolean Success = true;
        //        ImageButton ImageDDelete = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageDDelete.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageDDelete.NamingContainer);
        //        DropDownList ddlGDCountry = (DropDownList)row.FindControl("ddlGDCountry");
        //        DropDownList ddlGDState = (DropDownList)row.FindControl("ddlGDState");
        //        DropDownList ddlGDDealer = (DropDownList)row.FindControl("ddlGDDealer");
        //        TextBox txtGDDistrict = (TextBox)row.FindControl("txtGDDistrict");
        //        Success = new BDMS_Address().InsertOrUpdateAddressDistrict(Convert.ToInt32(id), Convert.ToInt32(ddlGDState.SelectedValue), Convert.ToInt32(ddlGDDealer.SelectedValue), txtGDDistrict.Text.Trim(), null, false, PSession.User.UserID);
        //        if (Success == false)
        //        {
        //            lblMessage.Text = "District is not successfully Deleted";
        //            lblMessage.ForeColor = Color.Red;
        //            lblMessage.Visible = true;

        //        }
        //        else
        //        {
        //            lblMessage.Text = "District was successfully Deleted.";
        //            lblMessage.ForeColor = Color.Green;
        //            lblMessage.Visible = true;
        //            FillGridDistrict();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}



        //protected void BtnAddCity_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        lblMessage.Text = string.Empty;
        //        Boolean Success = true;
        //        string Message = "";
        //        if (ddlCityCountry.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select Country";
        //            Success = false;
        //        }
        //        if (ddlCityState.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select State";
        //            Success = false;
        //        }
        //        if (ddlCityDistrict.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Select District";
        //            Success = false;
        //        }
        //        if (string.IsNullOrEmpty(txtCity.Text.Trim()))
        //        {
        //            Message = Message + "<br/> Please Enter the City";
        //            Success = false;
        //        }
        //        List<PDMS_Tehsil> MML = new BDMS_Address().GetTehsil(Convert.ToInt32(ddlCityCountry.SelectedValue), Convert.ToInt32(ddlCityState.SelectedValue), Convert.ToInt32(ddlCityDistrict.SelectedValue), txtCity.Text.Trim());
        //        if (MML.Count > 0)
        //        {
        //            Message = Message + "<br/> City is already found...!";
        //            Success = false;
        //        }
        //        lblMessage.Text = Message;
        //        if (Success == false)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressTehsil(null, Convert.ToInt32(ddlCityDistrict.SelectedValue), txtCity.Text.Trim(), true, PSession.User.UserID);
        //            if (Success == true)
        //            {
        //                lblMessage.Text = "City is Added successfully";
        //                lblMessage.ForeColor = Color.Green;
        //                FillGridTehsil();
        //            }
        //            else
        //            {
        //                lblMessage.Text = "City is not Added successfully";
        //                lblMessage.ForeColor = Color.Red;
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        protected void BtnSearchCity_Click(object sender, EventArgs e)
        {
            FillGridTehsil();
        }


        protected void ibtnCityArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvCity.PageIndex > 0)
            {
                gvCity.PageIndex = gvCity.PageIndex - 1;
                CityBind(gvCity, lblRowCountC, LTehsil);
            }
        }
        protected void ibtnCityArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvCity.PageCount > gvCity.PageIndex)
            {
                gvCity.PageIndex = gvCity.PageIndex + 1;
                CityBind(gvCity, lblRowCountC, LTehsil);
            }
        }

        void CityBind(GridView gv, Label lbl, List<PDMS_Tehsil> LTehsil)
        {
            gv.DataSource = LTehsil;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + LTehsil.Count;
        }


        private void FillGridTehsil()
        {
            try
            {
                int? CountryID = null, StateID = null, DistrictID = null;
                string Tehsil = null;
                if (ddlCityCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlCityCountry.SelectedValue);

                    if (ddlCityState.SelectedValue != "0")
                    {
                        StateID = Convert.ToInt32(ddlCityState.SelectedValue);

                        if (ddlCityDistrict.SelectedValue != "0")
                        {
                            DistrictID = Convert.ToInt32(ddlCityDistrict.SelectedValue);
                        }
                    }
                }
                
                if (!string.IsNullOrEmpty(txtCity.Text))
                {
                    Tehsil = txtCity.Text.Trim();
                }
                
                //List<PDMS_Tehsil> MML = new BDMS_Address().GetTehsil(CountryID, StateID, DistrictID, Tehsil);

                LTehsil = new BDMS_Address().GetTehsil(CountryID, StateID, DistrictID, Tehsil);
                if (LTehsil.Count ==0)
                {
                    LTehsil.Add(new PDMS_Tehsil());
                }
                gvCity.DataSource = LTehsil;
                gvCity.DataBind();
                //throw new NotImplementedException();

                if (LTehsil.Count == 0)
                {
                    lblRowCountC.Visible = false;
                    ibtnCityArrowLeft.Visible = false;
                    ibtnCityArrowRight.Visible = false;
                }
                else
                {
                    lblRowCountC.Visible = true;
                    ibtnCityArrowLeft.Visible = true;
                    ibtnCityArrowRight.Visible = true;
                    lblRowCountC.Text = (((gvCity.PageIndex) * gvCity.PageSize) + 1) + " - " + (((gvCity.PageIndex) * gvCity.PageSize) + gvCity.Rows.Count) + " of " + LTehsil.Count;
                }


                DropDownList ddlGCityCountry = gvCity.FooterRow.FindControl("ddlGCityCountry") as DropDownList;
                new DDLBind(ddlGCityCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");

                DropDownList ddlGCityState = gvCity.FooterRow.FindControl("ddlGCityState") as DropDownList;
                new DDLBind(ddlGCityState, new BDMS_Address().GetState(null, null, null, null), "State", "StateID", true, "Select State");

                DropDownList ddlGCityDistrict = gvCity.FooterRow.FindControl("ddlGCityDistrict") as DropDownList;
                new DDLBind(ddlGCityDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null), "District", "DistrictID", true, "Select District");

            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void BtnAddOrUpdateCity_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                Button BtnAddOrUpdateCity = (Button)gvCity.FooterRow.FindControl("BtnAddOrUpdateCity");

                DropDownList ddlGCityCountry = (DropDownList)gvCity.FooterRow.FindControl("ddlGCityCountry");
                if (ddlGCityCountry.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Country.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                DropDownList ddlGCityState = (DropDownList)gvCity.FooterRow.FindControl("ddlGCityState");
                if (ddlGCityState.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select State.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                DropDownList ddlGCityDistrict = (DropDownList)gvCity.FooterRow.FindControl("ddlGCityDistrict");
                if (ddlGCityDistrict.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select District.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                string City = ((TextBox)gvCity.FooterRow.FindControl("txtGCity")).Text.Trim();
                if (string.IsNullOrEmpty(City))
                {
                    lblMessage.Text = "Please enter City.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddOrUpdateCity.Text == "Add")
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressTehsil(null, Convert.ToInt32(ddlGCityDistrict.SelectedValue), City, true, PSession.User.UserID);
                    if (Success == true)
                    {
                        FillGridTehsil();
                        lblMessage.Text = "City is added successfully.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "City is already found.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "City not created successfully.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressTehsil(Convert.ToInt32(HiddenID.Value), Convert.ToInt32(ddlGCityDistrict.SelectedValue), City, true, PSession.User.UserID);

                    if (Success == true)
                    {
                        HiddenID.Value = null;
                        FillGridTehsil();
                        lblMessage.Text = "City successfully updated.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "City already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "City not updated successfully...!";
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

        //protected void ImageCityEdit_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.Text = "";
        //        ImageButton ImageCityEdit = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageCityEdit.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageCityEdit.NamingContainer);
        //        ImageButton ImageCityUpdate = (ImageButton)row.FindControl("ImageCityUpdate");
        //        DropDownList ddlGCityCountry = (DropDownList)row.FindControl("ddlGCityCountry");
        //        DropDownList ddlGCityState = (DropDownList)row.FindControl("ddlGCityState");
        //        DropDownList ddlGCityDistrict = (DropDownList)row.FindControl("ddlGCityDistrict");
        //        TextBox txtGCity = (TextBox)row.FindControl("txtGCity");
        //        ddlGCityCountry.Enabled = true;
        //        ddlGCityState.Enabled = true;
        //        ddlGCityDistrict.Enabled = true;
        //        txtGCity.Enabled = true;
        //        ImageCityUpdate.Visible = true;
        //        ImageCityEdit.Visible = false;
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}


        //protected void ImageCityUpdate_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        string Message = "";
        //        Boolean Success = true;
        //        ImageButton ImageCityUpdate = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageCityUpdate.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageCityUpdate.NamingContainer);
        //        ImageButton ImageCityEdit = (ImageButton)row.FindControl("ImageCityEdit");
        //        DropDownList ddlGCityCountry = (DropDownList)row.FindControl("ddlGCityCountry");
        //        DropDownList ddlGCityState = (DropDownList)row.FindControl("ddlGCityState");
        //        DropDownList ddlGCityDistrict = (DropDownList)row.FindControl("ddlGCityDistrict");
        //        TextBox txtGCity = (TextBox)row.FindControl("txtGCity");
        //        if (ddlGCityCountry.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Enter the Country";
        //            Success = false;
        //        }
        //        if (ddlGCityState.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Enter the State";
        //            Success = false;
        //        }
        //        if (ddlGCityDistrict.SelectedValue == "0")
        //        {
        //            Message = Message + "<br/> Please Enter the District";
        //            Success = false;
        //        }
        //        if (string.IsNullOrEmpty(txtGCity.Text.Trim()))
        //        {
        //            Message = Message + "<br/> Please Enter the City";
        //            Success = false;
        //        }
        //        List<PDMS_Tehsil> MML = new BDMS_Address().GetTehsil(Convert.ToInt32(ddlGCityCountry.SelectedValue), Convert.ToInt32(ddlGCityState.SelectedValue), Convert.ToInt32(ddlGCityDistrict.SelectedValue), txtGCity.Text.Trim());
        //        if (MML.Count > 0)
        //        {
        //            Message = Message + "<br/> City is already found...!";
        //            Success = false;
        //        }
        //        lblMessage.Text = Message;
        //        if (Success == false)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Success = new BDMS_Address().InsertOrUpdateAddressTehsil(null, Convert.ToInt32(ddlGCityDistrict.SelectedValue), txtGCity.Text.Trim(), true, PSession.User.UserID);
        //            if (Success == false)
        //            {
        //                lblMessage.Text = "City is not successfully updated";
        //                lblMessage.ForeColor = Color.Red;
        //                lblMessage.Visible = true;

        //            }
        //            else
        //            {
        //                lblMessage.Text = "City was successfully updated.";
        //                lblMessage.ForeColor = Color.Green;
        //                lblMessage.Visible = true;
        //                ddlGCityCountry.Enabled = false;
        //                ddlGCityState.Enabled = false;
        //                ddlGCityDistrict.Enabled = false;
        //                txtGCity.Enabled = false;
        //                ImageCityUpdate.Visible = false;
        //                ImageCityEdit.Visible = true;
        //                FillGridTehsil();
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void ImageCityDelete_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        lblMessage.Text = "";
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //        Boolean Success = true;
        //        ImageButton ImageCityDelete = (ImageButton)sender;
        //        long id = Convert.ToInt32(ImageCityDelete.CommandArgument);
        //        GridViewRow row = (GridViewRow)(ImageCityDelete.NamingContainer);
        //        DropDownList ddlGCityCountry = (DropDownList)row.FindControl("ddlGCityCountry");
        //        DropDownList ddlGCityState = (DropDownList)row.FindControl("ddlGCityState");
        //        DropDownList ddlGCityDistrict = (DropDownList)row.FindControl("ddlGCityDistrict");
        //        TextBox txtGCity = (TextBox)row.FindControl("txtGCity");
        //        Success = new BDMS_Address().InsertOrUpdateAddressTehsil(null, Convert.ToInt32(ddlGCityDistrict.SelectedValue), txtGCity.Text.Trim(), false, PSession.User.UserID);
        //        if (Success == false)
        //        {
        //            lblMessage.Text = "City is not successfully Deleted";
        //            lblMessage.ForeColor = Color.Red;
        //            lblMessage.Visible = true;

        //        }
        //        else
        //        {
        //            lblMessage.Text = "City was successfully Deleted.";
        //            lblMessage.ForeColor = Color.Green;
        //            lblMessage.Visible = true;
        //            FillGridTehsil();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.Text = Ex.ToString();
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}


        protected void lnkBtnCityEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnCityEdit = (LinkButton)sender;
                DropDownList ddlGCityCountry = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGCityCountry");
                DropDownList ddlGCityState = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGCityState");
                DropDownList ddlGCityDistrict = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGCityDistrict");
                TextBox txtGCity = (TextBox)gvDistrict.FooterRow.FindControl("txtGCity");
                Button BtnAddOrUpdateCity = (Button)gvDistrict.FooterRow.FindControl("BtnAddOrUpdateCity");
                GridViewRow row = (GridViewRow)(lnkBtnCityEdit.NamingContainer);
                Label lblGCityCountry = (Label)row.FindControl("lblGCityCountry");
                ddlGCityCountry.SelectedValue = lblGCityCountry.Text;
                Label lblGCityState = (Label)row.FindControl("lblGCityState");
                ddlGCityState.SelectedValue = lblGCityState.Text;
                Label lblGCityDistrict = (Label)row.FindControl("lblGCityDistrict");
                ddlGCityDistrict.Text = lblGCityDistrict.Text;
                Label lblGCity = (Label)row.FindControl("lblGCity");
                txtGCity.Text = lblGCity.Text;
                HiddenID.Value = Convert.ToString(lnkBtnCityEdit.CommandArgument);
                BtnAddOrUpdateCity.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnCityDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean success = true;
                LinkButton lnkBtnCityDelete = (LinkButton)sender;
                int TehsilID = Convert.ToInt32(lnkBtnCityDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnCityDelete.NamingContainer);
                int CountryID = Convert.ToInt32(((Label)row.FindControl("lblGCityCountryID")).Text.Trim());
                int StateID = Convert.ToInt32(((Label)row.FindControl("lblGCityStateID")).Text.Trim());
                int DistrictID = Convert.ToInt32(((Label)row.FindControl("lblGCityDistrictID")).Text.Trim());
                string City = ((Label)row.FindControl("lblGCity")).Text.Trim();

                success = new BDMS_Address().InsertOrUpdateAddressTehsil(TehsilID, DistrictID, City, false, PSession.User.UserID);
                if (success == true)
                {
                    HiddenID.Value = null;
                    FillGridCountry();
                    lblMessage.Text = "City deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else if (success == false)
                {
                    lblMessage.Text = "City not deleted successfully";
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
            FillDealerDLL(ddlDDealer, null, null);
        }

        protected void ddlCityCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
                FillStateDLL(ddlCityState, Convert.ToInt32(ddlCityCountry.SelectedValue), null, null, null);
        }

        protected void ddlCityState_SelectedIndexChanged(object sender, EventArgs e)
        {
                FillDistrictDLL(ddlCityDistrict, Convert.ToInt32(ddlCityCountry.SelectedValue), null, Convert.ToInt32(ddlCityState.SelectedValue),  null,null);
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
            FillDealerDLL(ddlGDDealer, null, null);
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
            FillDistrictDLL(ddlGCityDistrict, Convert.ToInt32(ddlGCityCountry.SelectedValue), null, Convert.ToInt32(ddlGCityState.SelectedValue), null,  null);
        }

        protected void gvDistrict_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //FillGridDistrict();
            gvDistrict.PageIndex = e.NewPageIndex;
            FillGridDistrict();
            gvDistrict.DataBind();
        }

        protected void gvCity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //FillGridTehsil();
            gvCity.PageIndex = e.NewPageIndex;
            FillGridTehsil();
            gvCity.DataBind();
        }

        protected void gvState_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //FillGridState();
            gvState.PageIndex = e.NewPageIndex;
            FillGridState();
            gvState.DataBind();
        }

        protected void gvRegion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //FillGridRegion();
            gvRegion.PageIndex = e.NewPageIndex;
            FillGridRegion();
            gvRegion.DataBind();
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
    }
}