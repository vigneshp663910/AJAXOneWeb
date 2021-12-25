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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label lblProjectTitle = (Label)Master.FindControl("lblProjectTitle");
                lblProjectTitle.Text = "Location";
                FillCountry();
                FillRegion();
                FillState();
                FillDistrict();
                //FillGridCountry();
                //FillGridDistrict();
                //FillGridState();
                //FillGridRegion();
            }
        }

        private void FillCountry()
        {
            try
            {
                List<PDMS_Country> MML = new BDMS_Address().GetCountry(null, null);
                ddlSSCountry.DataValueField = "CountryID";
                ddlSSCountry.DataTextField = "Country";
                ddlSSCountry.DataSource = MML;
                ddlSSCountry.DataBind();
                ddlSSCountry.Items.Insert(0, new ListItem("Select", "0"));

                ddlRCountry.DataValueField = "CountryID";
                ddlRCountry.DataTextField = "Country";
                ddlRCountry.DataSource = MML;
                ddlRCountry.DataBind();
                ddlRCountry.Items.Insert(0, new ListItem("Select", "0"));

                ddlSRCountry.DataValueField = "CountryID";
                ddlSRCountry.DataTextField = "Country";
                ddlSRCountry.DataSource = MML;
                ddlSRCountry.DataBind();
                ddlSRCountry.Items.Insert(0, new ListItem("Select", "0"));

                ddlSCountry.DataValueField = "CountryID";
                ddlSCountry.DataTextField = "Country";
                ddlSCountry.DataSource = MML;
                ddlSCountry.DataBind();
                ddlSCountry.Items.Insert(0, new ListItem("Select", "0"));
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
                List<PDMS_Region> MML = new BDMS_Address().GetRegion(null, null, null);
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
                List<PDMS_State> MML = new BDMS_Address().GetState(null,null,null, null);

                ddlDState.DataValueField = "StateID";
                ddlDState.DataTextField = "State";
                ddlDState.DataSource = MML;
                ddlDState.DataBind();
                ddlDState.Items.Insert(0, new ListItem("Select", "0"));

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
                List<PDMS_District> MML = new BDMS_Address().GetDistrict(null, null, null);
                ddlSDDistrict.DataValueField = "DistrictID";
                ddlSDDistrict.DataTextField = "District";
                ddlSDDistrict.DataSource = MML;
                ddlSDDistrict.DataBind();
                ddlSDDistrict.Items.Insert(0, new ListItem("Select", "0"));

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
            int? CountryID = null, RegionID = null;
            string Region = null;
            if (ddlSSCountry.SelectedValue != "0")
            {
                CountryID = Convert.ToInt32(ddlSSCountry.SelectedValue);
            }
            if (ddlSSRegion.SelectedValue != "0")
            {
                RegionID = Convert.ToInt32(ddlSSRegion.SelectedValue);
                Region = ddlSSRegion.SelectedItem.Text.Trim();
            }
            List<PDMS_State> MML = new BDMS_Address().GetState(null,null,null, null);
            gvState.DataSource = MML;
            gvState.DataBind();
            //throw new NotImplementedException();
        }
        private void FillGridDistrict()
        {
            int? StateID = null, DistrictID = null;
            string District = null;
            if (ddlSDState.SelectedValue != "0")
            {
                StateID = Convert.ToInt32(ddlSDState.SelectedValue);
            }
            if (ddlSDDistrict.SelectedValue != "0")
            {
                DistrictID = Convert.ToInt32(ddlSDDistrict.SelectedValue);
                District = ddlSDDistrict.SelectedItem.Text.Trim();
            }
            List<PDMS_District> MML = new BDMS_Address().GetDistrict(DistrictID, StateID, District);
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

        protected void BtnSaveDistrict_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                return;
            }
            Boolean Success = new BDMS_Address().InsertOrUpdateAddressDistrict(null, Convert.ToInt32(ddlDState.SelectedValue), txtDistrict.Text.Trim(), null, PSession.User.UserID);
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
        Boolean Validation()
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            Boolean Ret = true;
            string Message = "";

            if (string.IsNullOrEmpty(txtDistrict.Text.Trim()))
            {
                Message = Message + "<br/> Please Enter the District";
                Ret = false;
            }
            if (ddlDState.SelectedValue == "0")
            {
                Message = Message + "<br/> Please Select State";
                Ret = false;
            }
            lblMessage.Text = Message;
            return Ret;
        }

        protected void gvDistrict_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ddlState = (e.Row.FindControl("ddlGDState") as DropDownList);

                List<PDMS_State> MML = new BDMS_Address().GetState(null,null,null, null);
                ddlState.DataValueField = "StateID";
                ddlState.DataTextField = "State";
                ddlState.DataSource = MML;
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Select", "0"));

                //Select the Country of Customer in DropDownList
                string StateID = (e.Row.FindControl("lblState") as Label).Text;
                ddlState.Items.FindByValue(StateID).Selected = true;
            }
        }

        protected void BtnSearchDistrict_Click(object sender, EventArgs e)
        {
            FillGridDistrict();
        }

        protected void ImageEdit_Click(object sender, ImageClickEventArgs e)
        {
            lblMessage.Text = "";
            ImageButton ImageEdit = (ImageButton)sender;
            long id = Convert.ToInt32(ImageEdit.CommandArgument);
            GridViewRow row = (GridViewRow)(ImageEdit.NamingContainer);
            ImageButton ImageUpdate = (ImageButton)row.FindControl("ImageUpdate");
            TextBox txtGDDistrict = (TextBox)row.FindControl("txtGDDistrict");
            DropDownList ddlGDState = (DropDownList)row.FindControl("ddlGDState");
            txtGDDistrict.Enabled = true;
            ddlGDState.Enabled = true;
            ImageUpdate.Visible = true;
            ImageEdit.Visible = false;
        }

        protected void ImageUpdate_Click(object sender, ImageClickEventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            string Message = "";
            Boolean Success = true;
            ImageButton ImageUpdate = (ImageButton)sender;
            long id = Convert.ToInt32(ImageUpdate.CommandArgument);
            GridViewRow row = (GridViewRow)(ImageUpdate.NamingContainer);
            ImageButton ImageEdit = (ImageButton)row.FindControl("ImageEdit");
            TextBox txtGDDistrict = (TextBox)row.FindControl("txtGDDistrict");
            DropDownList ddlGDState = (DropDownList)row.FindControl("ddlGDState");
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
                Success = new BDMS_Address().InsertOrUpdateAddressDistrict(Convert.ToInt32(id), Convert.ToInt32(ddlGDState.SelectedValue), txtGDDistrict.Text.Trim(), null, PSession.User.UserID);
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
                    txtGDDistrict.Enabled = false;
                    ddlGDState.Enabled = false;
                    ImageUpdate.Visible = false;
                    ImageEdit.Visible = true;
                    FillGridDistrict();
                }
            }
        }
        protected void ImageDelete_Click(object sender, ImageClickEventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            string Message = "";
            Boolean Success = true;
            ImageButton ImageDelete = (ImageButton)sender;
            long id = Convert.ToInt32(ImageDelete.CommandArgument);
            GridViewRow row = (GridViewRow)(ImageDelete.NamingContainer);
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

            Success = new BDMS_Address().DeleteAddressCountry(Convert.ToInt32(id), PSession.User.UserID);
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

            Success = new BDMS_Address().DeleteAddressRegion(Convert.ToInt32(id), PSession.User.UserID);
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
                if(MML1.Count>0)
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

            Success = new BDMS_Address().DeleteAddressState(Convert.ToInt32(id), PSession.User.UserID);
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
    }
}