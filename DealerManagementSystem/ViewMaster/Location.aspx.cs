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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label lblProjectTitle = (Label)Master.FindControl("lblProjectTitle");
                lblProjectTitle.Text = "Location";
                LCountry = new BDMS_Address().GetCountry(null, null);
                FillCountry();
                FillRegion();
                FillState();
                FillDistrict();
                //FillGridCountry();
                //   FillGridDistrict();
                //FillGridState();
                //FillGridRegion();
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

            }
            catch (SqlException sqlEx)
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
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
            catch (SqlException sqlEx)
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
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
                List<PDMS_Region> MML = new BDMS_Address().GetRegion(CountryID, null, null);
                ddlSSRegion.DataValueField = "RegionID";
                ddlSSRegion.DataTextField = "Region";
                ddlSSRegion.DataSource = MML;
                ddlSSRegion.DataBind();
                ddlSSRegion.Items.Insert(0, new ListItem("Select", "0"));

                CountryID = null;
                if (ddlSCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSCountry.SelectedValue);
                }
                MML = new BDMS_Address().GetRegion(CountryID, null, null);
                ddlSRegion.DataValueField = "RegionID";
                ddlSRegion.DataTextField = "Region";
                ddlSRegion.DataSource = MML;
                ddlSRegion.DataBind();
                ddlSRegion.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (SqlException sqlEx)
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
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
                List<PDMS_State> MML = new BDMS_Address().GetState(CountryID, RegionID, null, null);
                ddlDState.DataValueField = "StateID";
                ddlDState.DataTextField = "State";
                ddlDState.DataSource = MML;
                ddlDState.DataBind();
                ddlDState.Items.Insert(0, new ListItem("Select", "0"));

                CountryID = null; RegionID = null;
                if (ddlSDCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(ddlSDCountry.SelectedValue);
                }
                MML = new BDMS_Address().GetState(CountryID, RegionID, null, null);
                ddlSDState.DataValueField = "StateID";
                ddlSDState.DataTextField = "State";
                ddlSDState.DataSource = MML;
                ddlSDState.DataBind();
                ddlSDState.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (SqlException sqlEx)
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
        private void FillDistrict()
        {
            try
            {
                //int? CountryID = null, RegionID = null;
                //if (ddlDCountry.SelectedValue != "0")
                //{
                //    CountryID = Convert.ToInt32(ddlDCountry.SelectedValue);
                //}
                //if (ddlDRegion.SelectedValue != "0")
                //{
                //    RegionID = Convert.ToInt32(ddlDRegion.SelectedValue);
                //}
                List<PDMS_District> MML = new BDMS_Address().GetDistrict(null, null, null, null, null);

                ddlCDistrict.DataValueField = "DistrictID";
                ddlCDistrict.DataTextField = "District";
                ddlCDistrict.DataSource = MML;
                ddlCDistrict.DataBind();
                ddlCDistrict.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (SqlException sqlEx)
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
        private void FillGridState()
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
            //throw new NotImplementedException();
        }
        private void FillGridDistrict()
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
            List<PDMS_District> MML = new BDMS_Address().GetDistrict(CountryID, RegionID, DistrictID, StateID, District);
            gvDistrict.DataSource = MML;
            gvDistrict.DataBind();
            //throw new NotImplementedException();
        }
        private void FillGridCountry()
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
        private void FillGridRegion()
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






        protected void ImageCityEdit_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ImageCityUpdate_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ImageCityDelete_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void BtnSaveCountry_Click(object sender, EventArgs e)
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
        protected void BtnSearchCountry_Click(object sender, EventArgs e)
        {
            FillGridCountry();
        }
        protected void ImageCEdit_Click(object sender, ImageClickEventArgs e)
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
        protected void ImageCUpdate_Click(object sender, ImageClickEventArgs e)
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
        protected void ImageCDelete_Click(object sender, ImageClickEventArgs e)
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
        protected void BtnSaveRegion_Click(object sender, EventArgs e)
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
        protected void BtnSearchRegion_Click(object sender, EventArgs e)
        {
            FillGridRegion();
        }
        protected void gvRegion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ddlGRCountry = (e.Row.FindControl("ddlGRCountry") as DropDownList);

                List<PDMS_Country> MML = new BDMS_Address().GetCountry(null, null);
                ddlGRCountry.DataValueField = "CountryID";
                ddlGRCountry.DataTextField = "Country";
                ddlGRCountry.DataSource = MML;
                ddlGRCountry.DataBind();
                ddlGRCountry.Items.Insert(0, new ListItem("Select", "0"));

                //Select the Country of Customer in DropDownList
                string CountryID = (e.Row.FindControl("lblCountry") as Label).Text;
                if (MML.Count > 0)
                    ddlGRCountry.Items.FindByValue(CountryID).Selected = true;
            }
        }
        protected void ImageREdit_Click(object sender, ImageClickEventArgs e)
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
        protected void ImageRUpdate_Click(object sender, ImageClickEventArgs e)
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
        protected void ImageRDelete_Click(object sender, ImageClickEventArgs e)
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
        protected void BtnSaveState_Click(object sender, EventArgs e)
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
            if (string.IsNullOrEmpty(txtStateCode.Text.Trim()))
            {
                Message = Message + "<br/> Please Enter the StateCode";
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
        protected void BtnSearchState_Click(object sender, EventArgs e)
        {
            FillGridState();
        }

        protected void gvState_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ddlGSCountry = (e.Row.FindControl("ddlGSCountry") as DropDownList);
                DropDownList ddlGSRegion = (e.Row.FindControl("ddlGSRegion") as DropDownList);

                List<PDMS_Country> MML = new BDMS_Address().GetCountry(null, null);
                ddlGSCountry.DataValueField = "CountryID";
                ddlGSCountry.DataTextField = "Country";
                ddlGSCountry.DataSource = MML;
                ddlGSCountry.DataBind();
                ddlGSCountry.Items.Insert(0, new ListItem("Select", "0"));

                //Select the Country of Customer in DropDownList
                string CountryID = (e.Row.FindControl("lblCountry") as Label).Text;
                if (MML.Count > 0)
                    ddlGSCountry.Items.FindByValue(CountryID).Selected = true;

                List<PDMS_Region> MML1 = new BDMS_Address().GetRegion(Convert.ToInt32(ddlGSCountry.SelectedValue), null, null);
                ddlGSRegion.DataValueField = "RegionID";
                ddlGSRegion.DataTextField = "Region";
                ddlGSRegion.DataSource = MML1;
                ddlGSRegion.DataBind();
                ddlGSRegion.Items.Insert(0, new ListItem("Select", "0"));

                //Select the Country of Customer in DropDownList
                string RegionID = (e.Row.FindControl("lblRegion") as Label).Text;
                if (MML1.Count > 0)
                    ddlGSRegion.Items.FindByValue(RegionID).Selected = true;
            }
        }
        protected void ImageSEdit_Click(object sender, ImageClickEventArgs e)
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
        protected void ImageSUpdate_Click(object sender, ImageClickEventArgs e)
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
            if (string.IsNullOrEmpty(txtGRStateCode.Text.Trim()))
            {
                Message = Message + "<br/> Please Enter the StateCode";
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
                    lblMessage.Text = "Region is not successfully updated";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;

                }
                else
                {
                    lblMessage.Text = "Region was successfully updated.";
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
        protected void ImageSDelete_Click(object sender, ImageClickEventArgs e)
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
        protected void BtnSaveDistrict_Click(object sender, EventArgs e)
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
        protected void BtnSearchDistrict_Click(object sender, EventArgs e)
        {
            FillGridDistrict();
        }
        protected void gvDistrict_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ddlGDCountry = (e.Row.FindControl("ddlGDCountry") as DropDownList);
                DropDownList ddlGDState = (e.Row.FindControl("ddlGDState") as DropDownList);

                FillCountryDLL(ddlGDCountry);

                //Select the Country of Customer in DropDownList
                string CountryID = (e.Row.FindControl("lblCountry") as Label).Text;

                ddlGDCountry.SelectedValue = CountryID;

                List<PDMS_State> MML1 = new BDMS_Address().GetState(Convert.ToInt32(CountryID), null, null, null);
                ddlGDState.DataValueField = "StateID";
                ddlGDState.DataTextField = "State";
                ddlGDState.DataSource = MML1;
                ddlGDState.DataBind();
                ddlGDState.Items.Insert(0, new ListItem("Select", "0"));

                //Select the State of Customer in DropDownList
                string StateID = (e.Row.FindControl("lblState") as Label).Text;

                ddlGDState.SelectedValue = StateID;
            }
        }
        protected void ImageDEdit_Click(object sender, ImageClickEventArgs e)
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
        protected void ImageDUpdate_Click(object sender, ImageClickEventArgs e)
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
        protected void ImageDDelete_Click(object sender, ImageClickEventArgs e)
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
    }
}