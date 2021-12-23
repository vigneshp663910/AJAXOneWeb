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
                FillCountry();
                FillRegion();
                FillState();
                FillDistrict();
                FillGridCountry();
                FillGridDistrict();
                FillGridState();
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

                ddlSCCountry.DataValueField = "CountryID";
                ddlSCCountry.DataTextField = "Country";
                ddlSCCountry.DataSource = MML;
                ddlSCCountry.DataBind();
                ddlSCCountry.Items.Insert(0, new ListItem("Select", "0"));
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
                List<PDMS_Region> MML = new BDMS_Address().GetRegion(null, null);
                ddlSSRegion.DataValueField = "RegionID";
                ddlSSRegion.DataTextField = "Region";
                ddlSSRegion.DataSource = MML;
                ddlSSRegion.DataBind();
                ddlSSRegion.Items.Insert(0, new ListItem("Select", "0"));
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
                List<PDMS_State> MML = new BDMS_Address().GetState(null, null);

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
            List<PDMS_District> MML = new BDMS_Address().GetDistrict(null, null, null);
            gvDistrict.DataSource = MML;
            gvDistrict.DataBind();
            //throw new NotImplementedException();
        }
        private void FillGridDistrict()
        {
            int CountryID = 0;
            if (ddlSCCountry.SelectedValue != "0")
            {
                CountryID = Convert.ToInt32(ddlSCCountry.SelectedValue);
            }
            List<PDMS_District> MML = new BDMS_Address().GetDistrict(null, null, null);
            gvDistrict.DataSource = MML;
            gvDistrict.DataBind();
            //throw new NotImplementedException();
        }
        private void FillGridCountry()
        {
            int CountryID=0;
            if (ddlSCCountry.SelectedValue != "0")
            {
                CountryID = Convert.ToInt32(ddlSCCountry.SelectedValue);
            }
            List<PDMS_Country> MML = new BDMS_Address().GetCountry(CountryID, ddlSCCountry.SelectedItem.Text.Trim());
            gvCountry.DataSource = MML;
            gvCountry.DataBind();
        }

        protected void BtnSaveDistrict_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                return;
            }
            int StateID = 0;
            if (ddlDState.SelectedValue != "0")
            {
                StateID = Convert.ToInt32(ddlDState.SelectedValue);
            }
            Boolean Success = new BDMS_Address().InsertOrUpdateAddressDistrict(null, StateID, txtDistrict.Text.Trim(), null, PSession.User.UserID);
            if (Success == true )
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
    }
}