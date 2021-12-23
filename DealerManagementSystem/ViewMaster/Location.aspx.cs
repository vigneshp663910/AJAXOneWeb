using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                FillState();
                FillDistrict();
                FillGridDistrict();
                FillGridState();
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
            List<PDMS_District> MML = new BDMS_Address().GetDistrict(null, null, null);
            gvDistrict.DataSource = MML;
            gvDistrict.DataBind();
            //throw new NotImplementedException();
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
    }
}