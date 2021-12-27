using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Location');</script>");

            if (!IsPostBack)
            {
                try
                {
                    Label lblProjectTitle = (Label)Master.FindControl("lblProjectTitle");
                    lblProjectTitle.Text = "Location";
                    LCountry = new BDMS_Address().GetCountry(null, null);
                    LRegion = new BDMS_Address().GetRegion(null, null, null);
                    LState = new BDMS_Address().GetState(null, null, null, null);
                    LDistrict = new BDMS_Address().GetDistrict(null, null, null, null, null);
                    FillCountry();
                    FillRegion();
                    FillState();
                    FillDistrict();
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
                FillCountryDLL(ddlSSCountry);
                FillCountryDLL(ddlRCountry);
                FillCountryDLL(ddlSRCountry);
                FillCountryDLL(ddlSCountry);
                FillCountryDLL(ddlDCountry);
                FillCountryDLL(ddlSDCountry);
                FillCountryDLL(ddlCityCountry);
                FillCountryDLL(ddlSCityCountry);
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
                LDistrict = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID, District);
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
        private void FillRegion()
        {
            try
            {
                int? CountryID = null;
                if (ddlSSCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSSCountry.SelectedValue);
                }
                FillRegionDLL(ddlSSRegion, CountryID, null, null);
                CountryID = null;
                if (ddlSCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSCountry.SelectedValue);
                }
                FillRegionDLL(ddlSRegion, CountryID, null, null);
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
                if (ddlDCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlDCountry.SelectedValue);
                }
                FillStateDLL(ddlDState, CountryID, null, null, null);
                CountryID = null; RegionID = null;
                if (ddlSDCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSDCountry.SelectedValue);
                }
                FillStateDLL(ddlSDState, CountryID, null, null, null);
                CountryID = null; RegionID = null;
                if (ddlCityCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlCityCountry.SelectedValue);
                }
                FillStateDLL(ddlCityState, CountryID, null, null, null);
                CountryID = null; RegionID = null;
                if (ddlSCityCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSCityCountry.SelectedValue);
                }
                FillStateDLL(ddlSCityState, CountryID, null, null, null);
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
                int? CountryID = null, RegionID = null, StateID = null, DistrictID = null;
                string district = null;
                if (ddlCityCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlCityCountry.SelectedValue);
                }
                if (ddlCityState.SelectedValue != "0")
                {
                    StateID = Convert.ToInt32(ddlCityState.SelectedValue);
                }
                FillDistrictDLL(ddlCityDistrict, CountryID, RegionID, StateID, DistrictID, district);
                CountryID = null; RegionID = null; StateID = null; DistrictID = null; district = null;
                if (ddlSCityCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSCityCountry.SelectedValue);
                }
                if (ddlSCityState.SelectedValue != "0")
                {
                    StateID = Convert.ToInt32(ddlSCityState.SelectedValue);
                }
                FillDistrictDLL(ddlSCityDistrict, CountryID, RegionID, StateID, DistrictID, district);
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillGridState()
        {
            try
            {
                int? CountryID = null, RegionID = null, StateID = null;
                string State = null;
                if (ddlSSCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSSCountry.SelectedValue);
                }
                if (ddlSSRegion.SelectedValue != "0")
                {
                    RegionID = Convert.ToInt32(ddlSSRegion.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txtSSState.Text))
                {
                    State = txtSRRegion.Text.Trim();
                }
                List<PDMS_State> MML = new BDMS_Address().GetState(CountryID, RegionID, null, State);
                gvState.DataSource = MML;
                gvState.DataBind();
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillGridDistrict()
        {
            try
            {
                int? CountryID = null, RegionID = null, StateID = null, DistrictID = null;
                string District = null;
                if (ddlSDCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSDCountry.SelectedValue);
                }
                if (ddlSDState.SelectedValue != "0")
                {
                    StateID = Convert.ToInt32(ddlSDState.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txtSDDistrict.Text))
                {
                    District = txtSDDistrict.Text.Trim();
                }
                List<PDMS_District> MML = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID, District);
                gvDistrict.DataSource = MML;
                gvDistrict.DataBind();
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillGridCountry()
        {
            try
            {
                int? CountryID = null;
                string country = null;
                if (!string.IsNullOrEmpty(txtSCountry.Text))
                {
                    country = txtSCountry.Text.Trim();
                }
                List<PDMS_Country> MML = new BDMS_Address().GetCountry(CountryID, country);
                gvCountry.DataSource = MML;
                gvCountry.DataBind();
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillGridRegion()
        {
            try
            {
                int? CountryID = null, RegionID = null;
                string Region = null;
                if (ddlSRCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSRCountry.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txtSRRegion.Text))
                {
                    Region = txtSRRegion.Text.Trim();
                }
                List<PDMS_Region> MML = new BDMS_Address().GetRegion(CountryID, RegionID, Region);
                gvRegion.DataSource = MML;
                gvRegion.DataBind();
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void BtnSaveCountry_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                string Message = "";

                if (string.IsNullOrEmpty(txtCountry.Text.Trim()))
                {
                    Message = Message + "<br/> Please Enter the Country";
                    Success = false;
                }
                lblMessage.Text = Message;
                if (Success == false)
                {
                    return;
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressCountry(null, txtDistrict.Text.Trim(), null, true, PSession.User.UserID);
                    if (Success == true)
                    {
                        lblMessage.Text = "Country is Added successfully";
                        lblMessage.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Country is not Added successfully";
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
        protected void BtnSearchCountry_Click(object sender, EventArgs e)
        {
            FillGridCountry();
        }
        protected void ImageCEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton ImageCEdit = (ImageButton)sender;
                long id = Convert.ToInt32(ImageCEdit.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageCEdit.NamingContainer);
                ImageButton ImageCUpdate = (ImageButton)row.FindControl("ImageCUpdate");
                TextBox txtGCCountry = (TextBox)row.FindControl("txtGCCountry");
                txtGCCountry.Enabled = true;
                ImageCUpdate.Visible = true;
                ImageCEdit.Visible = false;
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ImageCUpdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                string Message = "";
                Boolean Success = true;
                ImageButton ImageCUpdate = (ImageButton)sender;
                long id = Convert.ToInt32(ImageCUpdate.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageCUpdate.NamingContainer);
                ImageButton ImageCEdit = (ImageButton)row.FindControl("ImageCEdit");
                TextBox txtGCCountry = (TextBox)row.FindControl("txtGCCountry");
                if (string.IsNullOrEmpty(txtGCCountry.Text.Trim()))
                {
                    Message = Message + "<br/> Please Enter the Country";
                    Success = false;
                }
                lblMessage.Text = Message;
                if (Success == false)
                {
                    return;
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressCountry(Convert.ToInt32(id), txtGCCountry.Text.Trim(), null, true, PSession.User.UserID);
                    if (Success == false)
                    {
                        lblMessage.Text = "Country is not successfully updated";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;

                    }
                    else
                    {
                        lblMessage.Text = "Country was successfully updated.";
                        lblMessage.ForeColor = Color.Green;
                        lblMessage.Visible = true;
                        txtGCCountry.Enabled = false;
                        ImageCUpdate.Visible = false;
                        ImageCEdit.Visible = true;
                        FillGridCountry();
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
        protected void ImageCDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                ImageButton ImageCDelete = (ImageButton)sender;
                long id = Convert.ToInt32(ImageCDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageCDelete.NamingContainer);
                TextBox txtGCCountry = (TextBox)row.FindControl("txtGCCountry");

                Success = new BDMS_Address().InsertOrUpdateAddressCountry(Convert.ToInt32(id), txtGCCountry.Text.Trim(), null, false, PSession.User.UserID);
                if (Success == false)
                {
                    lblMessage.Text = "Country is not successfully Deleted";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;

                }
                else
                {
                    lblMessage.Text = "Country was successfully Deleted.";
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Visible = true;
                    FillGridCountry();
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void BtnSaveRegion_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                string Message = "";

                if (ddlRCountry.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Select Country";
                    Success = false;
                }
                if (string.IsNullOrEmpty(txtRRegion.Text.Trim()))
                {
                    Message = Message + "<br/> Please Enter the Region";
                    Success = false;
                }
                lblMessage.Text = Message;
                if (Success == false)
                {
                    return;
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressRegion(null, txtRRegion.Text.Trim(), Convert.ToInt32(ddlRCountry.SelectedValue), true, PSession.User.UserID);
                    if (Success == true)
                    {
                        lblMessage.Text = "Region is Added successfully";
                        lblMessage.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Region is not Added successfully";
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
                    DropDownList ddlGRCountry = (e.Row.FindControl("ddlGRCountry") as DropDownList);

                    FillCountryDLL(ddlGRCountry);
                    string CountryID = !string.IsNullOrEmpty((e.Row.FindControl("lblCountry") as Label).Text) ? (e.Row.FindControl("lblCountry") as Label).Text : "0";
                    ddlGRCountry.SelectedValue = CountryID;
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ImageREdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                ImageButton ImageREdit = (ImageButton)sender;
                long id = Convert.ToInt32(ImageREdit.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageREdit.NamingContainer);
                ImageButton ImageRUpdate = (ImageButton)row.FindControl("ImageRUpdate");
                DropDownList ddlGRCountry = (DropDownList)row.FindControl("ddlGRCountry");
                TextBox txtGRRegion = (TextBox)row.FindControl("txtGRRegion");
                ddlGRCountry.Enabled = true;
                txtGRRegion.Enabled = true;
                ImageRUpdate.Visible = true;
                ImageREdit.Visible = false;
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ImageRUpdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                string Message = "";
                Boolean Success = true;
                ImageButton ImageRUpdate = (ImageButton)sender;
                long id = Convert.ToInt32(ImageRUpdate.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageRUpdate.NamingContainer);
                ImageButton ImageREdit = (ImageButton)row.FindControl("ImageREdit");
                DropDownList ddlGRCountry = (DropDownList)row.FindControl("ddlGRCountry");
                TextBox txtGRRegion = (TextBox)row.FindControl("txtGRRegion");
                if (ddlGRCountry.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Select Country";
                    Success = false;
                }
                if (string.IsNullOrEmpty(txtGRRegion.Text.Trim()))
                {
                    Message = Message + "<br/> Please Enter the Region";
                    Success = false;
                }
                lblMessage.Text = Message;
                if (Success == false)
                {
                    return;
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressRegion(Convert.ToInt32(id), txtGRRegion.Text.Trim(), Convert.ToInt32(ddlGRCountry.SelectedValue), true, PSession.User.UserID);
                    if (Success == false)
                    {
                        lblMessage.Text = "Region is not successfully updated";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;

                    }
                    else
                    {
                        lblMessage.Text = "Region was successfully updated.";
                        lblMessage.ForeColor = Color.Green;
                        lblMessage.Visible = true;
                        ddlGRCountry.Enabled = false;
                        txtGRRegion.Enabled = false;
                        ImageRUpdate.Visible = false;
                        ImageREdit.Visible = true;
                        FillGridRegion();
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
        protected void ImageRDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                ImageButton ImageRDelete = (ImageButton)sender;
                long id = Convert.ToInt32(ImageRDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageRDelete.NamingContainer);
                DropDownList ddlGRCountry = (DropDownList)row.FindControl("ddlGRCountry");
                TextBox txtGRRegion = (TextBox)row.FindControl("txtGRRegion");

                Success = new BDMS_Address().InsertOrUpdateAddressRegion(Convert.ToInt32(id), txtGRRegion.Text.Trim(), Convert.ToInt32(ddlGRCountry.SelectedValue), false, PSession.User.UserID);
                if (Success == false)
                {
                    lblMessage.Text = "Region is not successfully Deleted";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;

                }
                else
                {
                    lblMessage.Text = "Region was successfully Deleted.";
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Visible = true;
                    FillGridRegion();
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void BtnSaveState_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                string Message = "";

                if (ddlSCountry.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Select Country";
                    Success = false;
                }
                if (ddlSRegion.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Select Region";
                    Success = false;
                }
                if (string.IsNullOrEmpty(txtState.Text.Trim()))
                {
                    Message = Message + "<br/> Please Enter the State";
                    Success = false;
                }
                lblMessage.Text = Message;
                if (Success == false)
                {
                    return;
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressState(null, txtState.Text.Trim(), txtStateCode.Text.Trim(), null, Convert.ToInt32(ddlSCountry.SelectedValue), true, Convert.ToInt32(ddlSRegion.SelectedValue), PSession.User.UserID);
                    if (Success == true)
                    {
                        lblMessage.Text = "State is Added successfully";
                        lblMessage.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "State is not Added successfully";
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
        protected void BtnSearchState_Click(object sender, EventArgs e)
        {
            FillGridState();
        }

        protected void gvState_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlGSCountry = (e.Row.FindControl("ddlGSCountry") as DropDownList);
                    DropDownList ddlGSRegion = (e.Row.FindControl("ddlGSRegion") as DropDownList);

                    FillCountryDLL(ddlGSCountry);
                    string CountryID = !string.IsNullOrEmpty((e.Row.FindControl("lblCountry") as Label).Text) ? (e.Row.FindControl("lblCountry") as Label).Text : "0";
                    ddlGSCountry.SelectedValue = CountryID;

                    FillRegionDLL(ddlGSRegion, Convert.ToInt32(CountryID), null, null);
                    string RegionID = !string.IsNullOrEmpty((e.Row.FindControl("lblRegion") as Label).Text) ? (e.Row.FindControl("lblRegion") as Label).Text : "0";
                    ddlGSRegion.SelectedValue = RegionID;
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ImageSEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                ImageButton ImageSEdit = (ImageButton)sender;
                long id = Convert.ToInt32(ImageSEdit.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageSEdit.NamingContainer);
                ImageButton ImageSUpdate = (ImageButton)row.FindControl("ImageSUpdate");
                DropDownList ddlGSCountry = (DropDownList)row.FindControl("ddlGSCountry");
                DropDownList ddlGSRegion = (DropDownList)row.FindControl("ddlGSRegion");
                TextBox txtGRState = (TextBox)row.FindControl("txtGRState");
                TextBox txtGRStateCode = (TextBox)row.FindControl("txtGRStateCode");
                ddlGSCountry.Enabled = true;
                ddlGSRegion.Enabled = true;
                txtGRState.Enabled = true;
                txtGRStateCode.Enabled = true;
                ImageSUpdate.Visible = true;
                ImageSEdit.Visible = false;
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ImageSUpdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                string Message = "";
                Boolean Success = true;
                ImageButton ImageSUpdate = (ImageButton)sender;
                long id = Convert.ToInt32(ImageSUpdate.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageSUpdate.NamingContainer);
                ImageButton ImageSEdit = (ImageButton)row.FindControl("ImageSEdit");
                DropDownList ddlGSCountry = (DropDownList)row.FindControl("ddlGSCountry");
                DropDownList ddlGSRegion = (DropDownList)row.FindControl("ddlGSRegion");
                TextBox txtGRState = (TextBox)row.FindControl("txtGRState");
                TextBox txtGRStateCode = (TextBox)row.FindControl("txtGRStateCode");
                if (ddlGSCountry.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Select Country";
                    Success = false;
                }
                if (ddlGSRegion.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Select Region";
                    Success = false;
                }
                if (string.IsNullOrEmpty(txtGRState.Text.Trim()))
                {
                    Message = Message + "<br/> Please Enter the State";
                    Success = false;
                }
                lblMessage.Text = Message;
                if (Success == false)
                {
                    return;
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressState(Convert.ToInt32(id), txtGRState.Text.Trim(), txtGRStateCode.Text.Trim(), null, Convert.ToInt32(ddlGSCountry.SelectedValue), true, Convert.ToInt32(ddlGSRegion.SelectedValue), PSession.User.UserID);
                    if (Success == false)
                    {
                        lblMessage.Text = "State is not successfully updated";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;

                    }
                    else
                    {
                        lblMessage.Text = "State was successfully updated.";
                        lblMessage.ForeColor = Color.Green;
                        lblMessage.Visible = true;
                        ddlGSCountry.Enabled = false;
                        ddlGSRegion.Enabled = false;
                        txtGRState.Enabled = false;
                        txtGRStateCode.Enabled = false;
                        ImageSUpdate.Visible = false;
                        ImageSEdit.Visible = true;
                        FillGridState();
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
        protected void ImageSDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                ImageButton ImageSDelete = (ImageButton)sender;
                long id = Convert.ToInt32(ImageSDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageSDelete.NamingContainer);
                DropDownList ddlGSCountry = (DropDownList)row.FindControl("ddlGSCountry");
                DropDownList ddlGSRegion = (DropDownList)row.FindControl("ddlGSRegion");
                TextBox txtGRState = (TextBox)row.FindControl("txtGRState");
                TextBox txtGRStateCode = (TextBox)row.FindControl("txtGRStateCode");

                Success = new BDMS_Address().InsertOrUpdateAddressState(Convert.ToInt32(id), txtGRState.Text.Trim(), txtGRStateCode.Text.Trim(), null, Convert.ToInt32(ddlGSCountry.SelectedValue), false, Convert.ToInt32(ddlGSRegion.SelectedValue), PSession.User.UserID);
                if (Success == false)
                {
                    lblMessage.Text = "State is not successfully Deleted";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;

                }
                else
                {
                    lblMessage.Text = "State was successfully Deleted.";
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Visible = true;
                    FillGridState();
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void BtnSaveDistrict_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                string Message = "";
                if (ddlDCountry.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Select Country";
                    Success = false;
                }
                if (ddlDState.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Select State";
                    Success = false;
                }
                if (string.IsNullOrEmpty(txtDistrict.Text.Trim()))
                {
                    Message = Message + "<br/> Please Enter the District";
                    Success = false;
                }

                lblMessage.Text = Message;
                if (Success == false)
                {
                    return;
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressDistrict(null, Convert.ToInt32(ddlDState.SelectedValue), txtDistrict.Text.Trim(), null, true, PSession.User.UserID);
                    if (Success == true)
                    {
                        lblMessage.Text = "District is updated successfully";
                        lblMessage.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "District is not updated successfully";
                        lblMessage.ForeColor = Color.Red;
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
        protected void BtnSearchDistrict_Click(object sender, EventArgs e)
        {
            FillGridDistrict();
        }
        protected void gvDistrict_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlGDCountry = (e.Row.FindControl("ddlGDCountry") as DropDownList);
                    DropDownList ddlGDState = (e.Row.FindControl("ddlGDState") as DropDownList);

                    FillCountryDLL(ddlGDCountry);
                    string CountryID = !string.IsNullOrEmpty((e.Row.FindControl("lblCountry") as Label).Text) ? (e.Row.FindControl("lblCountry") as Label).Text : "0";
                    ddlGDCountry.SelectedValue = CountryID;

                    FillStateDLL(ddlGDState, Convert.ToInt32(CountryID), null, null, null);
                    string StateID = !string.IsNullOrEmpty((e.Row.FindControl("lblState") as Label).Text) ? (e.Row.FindControl("lblState") as Label).Text : "0";
                    ddlGDState.SelectedValue = StateID;
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ImageDEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                ImageButton ImageDEdit = (ImageButton)sender;
                long id = Convert.ToInt32(ImageDEdit.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageDEdit.NamingContainer);
                ImageButton ImageDUpdate = (ImageButton)row.FindControl("ImageDUpdate");
                DropDownList ddlGDCountry = (DropDownList)row.FindControl("ddlGDCountry");
                DropDownList ddlGDState = (DropDownList)row.FindControl("ddlGDState");
                TextBox txtGDDistrict = (TextBox)row.FindControl("txtGDDistrict");
                ddlGDCountry.Enabled = true;
                ddlGDState.Enabled = true;
                txtGDDistrict.Enabled = true;
                ImageDUpdate.Visible = true;
                ImageDEdit.Visible = false;
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ImageDUpdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                string Message = "";
                Boolean Success = true;
                ImageButton ImageDUpdate = (ImageButton)sender;
                long id = Convert.ToInt32(ImageDUpdate.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageDUpdate.NamingContainer);
                ImageButton ImageDEdit = (ImageButton)row.FindControl("ImageDEdit");
                DropDownList ddlGDCountry = (DropDownList)row.FindControl("ddlGDCountry");
                DropDownList ddlGDState = (DropDownList)row.FindControl("ddlGDState");
                TextBox txtGDDistrict = (TextBox)row.FindControl("txtGDDistrict");
                if (string.IsNullOrEmpty(txtGDDistrict.Text.Trim()))
                {
                    Message = Message + "<br/> Please Enter the District";
                    Success = false;
                }
                if (ddlGDState.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Enter the District";
                    Success = false;
                }
                lblMessage.Text = Message;
                if (Success == false)
                {
                    return;
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressDistrict(Convert.ToInt32(id), Convert.ToInt32(ddlGDState.SelectedValue), txtGDDistrict.Text.Trim(), null, true, PSession.User.UserID);
                    if (Success == false)
                    {
                        lblMessage.Text = "District is not successfully updated";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;

                    }
                    else
                    {
                        lblMessage.Text = "District was successfully updated.";
                        lblMessage.ForeColor = Color.Green;
                        lblMessage.Visible = true;
                        ddlGDCountry.Enabled = false;
                        ddlGDState.Enabled = false;
                        txtGDDistrict.Enabled = false;
                        ImageDUpdate.Visible = false;
                        ImageDEdit.Visible = true;
                        FillGridDistrict();
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
        protected void ImageDDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                ImageButton ImageDDelete = (ImageButton)sender;
                long id = Convert.ToInt32(ImageDDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageDDelete.NamingContainer);
                DropDownList ddlGDCountry = (DropDownList)row.FindControl("ddlGDCountry");
                DropDownList ddlGDState = (DropDownList)row.FindControl("ddlGDState");
                TextBox txtGDDistrict = (TextBox)row.FindControl("txtGDDistrict");
                Success = new BDMS_Address().InsertOrUpdateAddressDistrict(Convert.ToInt32(id), Convert.ToInt32(ddlGDState.SelectedValue), txtGDDistrict.Text.Trim(), null, false, PSession.User.UserID);
                if (Success == false)
                {
                    lblMessage.Text = "District is not successfully Deleted";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;

                }
                else
                {
                    lblMessage.Text = "District was successfully Deleted.";
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Visible = true;
                    FillGridDistrict();
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void BtnSaveCity_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                string Message = "";
                if (ddlCityCountry.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Select Country";
                    Success = false;
                }
                if (ddlCityState.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Select State";
                    Success = false;
                }
                if (ddlCityDistrict.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Select District";
                    Success = false;
                }
                if (string.IsNullOrEmpty(txtCity.Text.Trim()))
                {
                    Message = Message + "<br/> Please Enter the City";
                    Success = false;
                }

                lblMessage.Text = Message;
                if (Success == false)
                {
                    return;
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressTehsil(null, Convert.ToInt32(ddlCityDistrict.SelectedValue), txtCity.Text.Trim(), true, PSession.User.UserID);
                    if (Success == true)
                    {
                        lblMessage.Text = "City is Added successfully";
                        lblMessage.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "City is not Added successfully";
                        lblMessage.ForeColor = Color.Red;
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

        protected void BtnSearchCity_Click(object sender, EventArgs e)
        {
            FillGridTehsil();
        }
        private void FillGridTehsil()
        {
            try
            {
                int? CountryID = null, StateID = null, DistrictID = null;
                string Tehsil = null;
                if (ddlSCityCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSCityCountry.SelectedValue);
                }
                if (ddlSCityState.SelectedValue != "0")
                {
                    StateID = Convert.ToInt32(ddlSCityState.SelectedValue);
                }
                if (ddlSCityDistrict.SelectedValue != "0")
                {
                    DistrictID = Convert.ToInt32(ddlSCityDistrict.SelectedValue);
                }
                if (!string.IsNullOrEmpty(ddlSCCity.Text))
                {
                    Tehsil = ddlSCCity.Text.Trim();
                }
                List<PDMS_Tehsil> MML = new BDMS_Address().GetTehsil(CountryID, StateID, DistrictID, Tehsil);
                gvCity.DataSource = MML;
                gvCity.DataBind();
                //throw new NotImplementedException();
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void gvCity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlGCityCountry = (e.Row.FindControl("ddlGCityCountry") as DropDownList);
                    DropDownList ddlGCityState = (e.Row.FindControl("ddlGCityState") as DropDownList);
                    DropDownList ddlGCityDistrict = (e.Row.FindControl("ddlGCityDistrict") as DropDownList);

                    FillCountryDLL(ddlGCityCountry);
                    string CountryID = !string.IsNullOrEmpty((e.Row.FindControl("lblCountry") as Label).Text) ? (e.Row.FindControl("lblCountry") as Label).Text : "0";
                    ddlGCityCountry.SelectedValue = CountryID;

                    FillStateDLL(ddlGCityState, Convert.ToInt32(CountryID), null, null, null);
                    string StateID = !string.IsNullOrEmpty((e.Row.FindControl("lblState") as Label).Text) ? (e.Row.FindControl("lblState") as Label).Text : "0";
                    ddlGCityState.SelectedValue = StateID;

                    FillDistrictDLL(ddlGCityDistrict, Convert.ToInt32(CountryID), null, Convert.ToInt32(StateID), null, null);
                    string DistrictID = (e.Row.FindControl("lblDistrict") as Label).Text;
                    ddlGCityDistrict.SelectedValue = DistrictID;
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ImageCityEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                ImageButton ImageCityEdit = (ImageButton)sender;
                long id = Convert.ToInt32(ImageCityEdit.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageCityEdit.NamingContainer);
                ImageButton ImageCityUpdate = (ImageButton)row.FindControl("ImageCityUpdate");
                DropDownList ddlGCityCountry = (DropDownList)row.FindControl("ddlGCityCountry");
                DropDownList ddlGCityState = (DropDownList)row.FindControl("ddlGCityState");
                DropDownList ddlGCityDistrict = (DropDownList)row.FindControl("ddlGCityDistrict");
                TextBox txtGCity = (TextBox)row.FindControl("txtGCity");
                ddlGCityCountry.Enabled = true;
                ddlGCityState.Enabled = true;
                ddlGCityDistrict.Enabled = true;
                txtGCity.Enabled = true;
                ImageCityUpdate.Visible = true;
                ImageCityEdit.Visible = false;
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ImageCityUpdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                string Message = "";
                Boolean Success = true;
                ImageButton ImageCityUpdate = (ImageButton)sender;
                long id = Convert.ToInt32(ImageCityUpdate.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageCityUpdate.NamingContainer);
                ImageButton ImageCityEdit = (ImageButton)row.FindControl("ImageCityEdit");
                DropDownList ddlGCityCountry = (DropDownList)row.FindControl("ddlGCityCountry");
                DropDownList ddlGCityState = (DropDownList)row.FindControl("ddlGCityState");
                DropDownList ddlGCityDistrict = (DropDownList)row.FindControl("ddlGCityDistrict");
                TextBox txtGCity = (TextBox)row.FindControl("txtGCity");
                if (ddlGCityCountry.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Enter the Country";
                    Success = false;
                }
                if (ddlGCityState.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Enter the State";
                    Success = false;
                }
                if (ddlGCityDistrict.SelectedValue == "0")
                {
                    Message = Message + "<br/> Please Enter the District";
                    Success = false;
                }
                if (string.IsNullOrEmpty(txtGCity.Text.Trim()))
                {
                    Message = Message + "<br/> Please Enter the City";
                    Success = false;
                }

                lblMessage.Text = Message;
                if (Success == false)
                {
                    return;
                }
                else
                {
                    Success = new BDMS_Address().InsertOrUpdateAddressTehsil(null, Convert.ToInt32(ddlGCityDistrict.SelectedValue), txtGCity.Text.Trim(), true, PSession.User.UserID);
                    if (Success == false)
                    {
                        lblMessage.Text = "City is not successfully updated";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;

                    }
                    else
                    {
                        lblMessage.Text = "City was successfully updated.";
                        lblMessage.ForeColor = Color.Green;
                        lblMessage.Visible = true;
                        ddlGCityCountry.Enabled = false;
                        ddlGCityState.Enabled = false;
                        ddlGCityDistrict.Enabled = false;
                        txtGCity.Enabled = false;
                        ImageCityUpdate.Visible = false;
                        ImageCityEdit.Visible = true;
                        FillGridTehsil();
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
        protected void ImageCityDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                ImageButton ImageCityDelete = (ImageButton)sender;
                long id = Convert.ToInt32(ImageCityDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(ImageCityDelete.NamingContainer);
                DropDownList ddlGCityCountry = (DropDownList)row.FindControl("ddlGCityCountry");
                DropDownList ddlGCityState = (DropDownList)row.FindControl("ddlGCityState");
                DropDownList ddlGCityDistrict = (DropDownList)row.FindControl("ddlGCityDistrict");
                TextBox txtGCity = (TextBox)row.FindControl("txtGCity");
                Success = new BDMS_Address().InsertOrUpdateAddressTehsil(null, Convert.ToInt32(ddlGCityDistrict.SelectedValue), txtGCity.Text.Trim(), false, PSession.User.UserID);
                if (Success == false)
                {
                    lblMessage.Text = "City is not successfully Deleted";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;

                }
                else
                {
                    lblMessage.Text = "City was successfully Deleted.";
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Visible = true;
                    FillGridTehsil();
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}