using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster.UserControls
{
    public partial class LocationDistrict : System.Web.UI.UserControl
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
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            if (!IsPostBack)
            {
                try
                { 
                    FillCountryDLL(ddlDCountry);  
                    LDistrict = new BDMS_Address().GetDistrict(null, null, null, null, null, null, "true");
                    LSalesOffice = new BDMS_Address().GetSalesOffice(null, null);
                }
                catch (Exception Ex)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = Ex.ToString();
                    lblMessage.ForeColor = Color.Red;
                }
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

                if (!string.IsNullOrEmpty(txtDistrict.Text))
                {
                    District = txtDistrict.Text.Trim();
                } 

                LDistrict = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID, District, DealerID, "true");
                if (LDistrict.Count == 0)
                {
                    LDistrict.Add(new PDMS_District());
                } 
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


                DropDownList ddlfGvCountry = gvDistrict.FooterRow.FindControl("ddlfGvCountry") as DropDownList;
                new DDLBind(ddlfGvCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");

                DropDownList ddlfGvState = gvDistrict.FooterRow.FindControl("ddlfGvState") as DropDownList;
                new DDLBind(ddlfGvState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID", true, "Select State");

                DropDownList ddlfGvSalesOffice = gvDistrict.FooterRow.FindControl("ddlfGvSalesOffice") as DropDownList;
                new DDLBind(ddlfGvSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice"); 

                ActionControlMange();
            }
            catch (Exception Ex)
            { 
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        
        void DistrictBind(GridView gv, Label lbl, List<PDMS_District> LDistrict)
        {
            gv.DataSource = LDistrict;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + LDistrict.Count;

            DropDownList ddlfGvCountry = gvDistrict.FooterRow.FindControl("ddlfGvCountry") as DropDownList;
            new DDLBind(ddlfGvCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID", true, "Select Country");

            DropDownList ddlfGvState = gvDistrict.FooterRow.FindControl("ddlfGvState") as DropDownList;
            new DDLBind(ddlfGvState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID", true, "Select State");

            DropDownList ddlfGvSalesOffice = gvDistrict.FooterRow.FindControl("ddlfGvSalesOffice") as DropDownList;
            new DDLBind(ddlfGvSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");
            ActionControlMange();
        }
        protected void lnkBtnDistrictEdit_Click(object sender, EventArgs e)
        {
            try
            {  
                LinkButton lnkBtnDistrictEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnDistrictEdit.NamingContainer);

                DropDownList ddlfGvCountry = (DropDownList)gvDistrict.FooterRow.FindControl("ddlfGvCountry");
                ddlfGvCountry.Enabled = false;
                DropDownList ddlfGvState = (DropDownList)gvDistrict.FooterRow.FindControl("ddlfGvState");
                ddlfGvState.Enabled = false;
                DropDownList ddlfGvSalesOffice = (DropDownList)gvDistrict.FooterRow.FindControl("ddlfGvSalesOffice");  
                 
                TextBox txtGDDistrict = (TextBox)gvDistrict.FooterRow.FindControl("txtfGvDistrict");

                Button BtnAddOrUpdateDistrict = (Button)gvDistrict.FooterRow.FindControl("BtnAddOrUpdateDistrict");

                Label lblGDCountry = (Label)row.FindControl("lblGvCountry");
                Label lblGDCountryID = (Label)row.FindControl("lblGvCountryID");
                ddlfGvCountry.SelectedValue = (string.IsNullOrEmpty(lblGDCountryID.Text)) ? "0" : lblGDCountryID.Text;

                Label lblGDState = (Label)row.FindControl("lblGvState");
                Label lblGDStateID = (Label)row.FindControl("lblGvStateID");
                ddlfGvState.SelectedValue = (string.IsNullOrEmpty(lblGDStateID.Text)) ? "0" : lblGDStateID.Text;

                Label lblGDSalesOffice = (Label)row.FindControl("lblGvSalesOffice");
                Label lblGDSalesOfficeID = (Label)row.FindControl("lblGvSalesOfficeID");
                ddlfGvSalesOffice.SelectedValue = (string.IsNullOrEmpty(lblGDSalesOfficeID.Text)) ? "0" : lblGDSalesOfficeID.Text;  

                Label lblGDDistrict = (Label)row.FindControl("lblGvDistrict");
                txtGDDistrict.Text = lblGDDistrict.Text;

                ((CheckBox)gvDistrict.FooterRow.FindControl("cbfGvHilly")).Checked = ((CheckBox)row.FindControl("cbGvHilly")).Checked;

                HiddenID.Value = Convert.ToString(lnkBtnDistrictEdit.CommandArgument);
                BtnAddOrUpdateDistrict.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red; 
            }
        }
        protected void BtnAddOrUpdateDistrict_Click(object sender, EventArgs e)
        {
            try
            { 
                lblMessage.ForeColor = Color.Red;  

                Button BtnAddOrUpdateDistrict = (Button)gvDistrict.FooterRow.FindControl("BtnAddOrUpdateDistrict"); 
                DropDownList ddlGDCountry = (DropDownList)gvDistrict.FooterRow.FindControl("ddlfGvCountry");
                if (ddlGDCountry.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Country."; 
                    return;
                }
                DropDownList ddlGDState = (DropDownList)gvDistrict.FooterRow.FindControl("ddlfGvState");
                if (ddlGDState.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select State."; 
                    return;
                }
                DropDownList ddlGDSalesOffice = (DropDownList)gvDistrict.FooterRow.FindControl("ddlfGvSalesOffice");
                if (ddlGDSalesOffice.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select SalesOffice."; 
                    return;
                } 
                string District = ((TextBox)gvDistrict.FooterRow.FindControl("txtfGvDistrict")).Text.Trim();
                if (string.IsNullOrEmpty(District))
                {
                    lblMessage.Text = "Please enter District."; 
                    return;
                }
                Boolean Hilly = ((CheckBox)gvDistrict.FooterRow.FindControl("cbfGvHilly")).Checked;
                if (BtnAddOrUpdateDistrict.Text == "Add")
                {
                    PApiResult Result =  new BDMS_Address().InsertOrUpdateAddressDistrict(Convert.ToInt32(ddlGDCountry.SelectedValue), Convert.ToInt32(ddlGDState.SelectedValue),null, District, Convert.ToInt32(ddlGDSalesOffice.SelectedValue), Hilly, true);
                    if (Result.Status == PApplication.Success)
                    {
                        FillGridDistrict();
                        lblMessage.Text = "District is added successfully.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    } 
                    else
                    {
                        lblMessage.Text = Result.Message; 
                        return;
                    }
                }
                else
                {
                    PApiResult Result =   new BDMS_Address().InsertOrUpdateAddressDistrict( Convert.ToInt32(ddlGDCountry.SelectedValue), Convert.ToInt32(ddlGDState.SelectedValue),Convert.ToInt32(HiddenID.Value), District, Convert.ToInt32(ddlGDSalesOffice.SelectedValue), Hilly, true);

                    if (Result.Status == PApplication.Success)
                    {
                        HiddenID.Value = null;
                        FillGridDistrict();
                        lblMessage.Text = "District successfully updated.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    } 
                    else
                    {
                        lblMessage.Text = Result.Message;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString(); 
            }
        }
        protected void BtnSearchDistrict_Click(object sender, EventArgs e)
        {
            FillGridDistrict(); 
        } 
        protected void lnkBtnDistrictDelete_Click(object sender, EventArgs e)
        {
            try
            { 
                lblMessage.ForeColor = Color.Red; 
                Boolean success = true;
                LinkButton lnkBtnDistrictDelete = (LinkButton)sender;
                int DistrictID = Convert.ToInt32(lnkBtnDistrictDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnDistrictDelete.NamingContainer);

                int CountryID = Convert.ToInt32(((Label)row.FindControl("lblGvCountryID")).Text.Trim());
                int StateID = Convert.ToInt32(((Label)row.FindControl("lblGvStateID")).Text.Trim());
                Label lblGDSalesOfficeID = (Label)row.FindControl("lblGvSalesOfficeID"); 
                int? SalesOfficeID = string.IsNullOrEmpty(lblGDSalesOfficeID.Text) ? (int?)null : Convert.ToInt32(lblGDSalesOfficeID.Text);   
               
                string District = ((Label)row.FindControl("lblGvDistrict")).Text.Trim();

                Boolean Hilly = ((CheckBox)row.FindControl("cbGvHilly")).Checked;
                PApiResult Result = new BDMS_Address().InsertOrUpdateAddressDistrict(CountryID, StateID, DistrictID, District, SalesOfficeID, Hilly, false);               
                if (Result.Status == PApplication.Success) 
                {
                    HiddenID.Value = null;
                    FillGridDistrict();
                    lblMessage.Text = "District deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else if (success == false)
                {
                    lblMessage.Text = "District not deleted successfully"; 
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString(); 
            }
        }
        protected void ddlDCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStateDLL(ddlDState,null, Convert.ToInt32(ddlDCountry.SelectedValue), null, null, null);
        }
        private void FillStateDLL(DropDownList ddl, int? DealerID, int? CountryID, int? RegionID, int? StateID, string State)
        {
            try
            {  
                ddl.DataValueField = "StateID";
                ddl.DataTextField = "State";
                ddl.DataSource = new BDMS_Address().GetState(DealerID, CountryID, RegionID, StateID, State);
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

        protected void ddlGDCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlGDCountry = (DropDownList)sender;
            GridViewRow row = (GridViewRow)(ddlGDCountry.NamingContainer);
            DropDownList ddlGDState = (DropDownList)row.FindControl("ddlfGvState");
            DropDownList ddlGDSalesOffice = (DropDownList)row.FindControl("ddlfGvSalesOffice"); 
            FillStateDLL(ddlGDState, null, Convert.ToInt32(ddlGDCountry.SelectedValue), null, null, null);
            FillSalesOfficeDLL(ddlGDSalesOffice, null, null); 
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
         
        public void ActionControlMange()
        {  
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.AddEditLocation).Count() == 0)
            {
                for (int i = 0; i < gvDistrict.Rows.Count; i++)
                {
                    ((LinkButton)gvDistrict.Rows[i].FindControl("lnkBtnDistrictEdit")).Visible = false;
                    ((LinkButton)gvDistrict.Rows[i].FindControl("lnkBtnDistrictDelete")).Visible = false;                    
                }
                ((Button)gvDistrict.FooterRow.FindControl("BtnAddOrUpdateDistrict")).Visible = false;
                gvDistrict.ShowFooter = false;
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
        protected void gvDistrict_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDistrict.PageIndex = e.NewPageIndex;
            DistrictBind(gvDistrict, lblRowCountD, LDistrict);
        }
    }
}