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
            if (!IsPostBack)
            {
                try
                {
                    FillDealerDLL(ddlDDealer);
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
        private void FillDealerDLL(DropDownList ddl)
        {
            try
            { 
                ddl.DataValueField = "DID";
                ddl.DataTextField = "CodeWithDisplayName";
                ddl.DataSource = PSession.User.Dealer;
                ddl.DataBind();
                // ddl.Items.Insert(0, new ListItem("Select", "0"));
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
        protected void ibtnDistrictArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDistrict.PageIndex > 0)
            {
                gvDistrict.PageIndex = gvDistrict.PageIndex - 1;
                DistrictBind(gvDistrict, lblRowCountD, LDistrict);
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
                if (ddlDDealer.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDDealer.SelectedValue))
                {
                    DealerID = Convert.ToInt32(ddlDDealer.SelectedValue);
                }

                if (!string.IsNullOrEmpty(txtDistrict.Text))
                {
                    District = txtDistrict.Text.Trim();
                }

                //List<PDMS_District> MML = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID,  District, null);

                LDistrict = new BDMS_Address().GetDistrict(CountryID, RegionID, StateID, DistrictID, District, DealerID, "true");
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
                new DDLBind(ddlGDState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID", true, "Select State");

                DropDownList ddlGDSalesOffice = gvDistrict.FooterRow.FindControl("ddlGDSalesOffice") as DropDownList;
                new DDLBind(ddlGDSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");

                DropDownList ddlGDDealer = gvDistrict.FooterRow.FindControl("ddlGDDealer") as DropDownList;
                new DDLBind(ddlGDDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");

                //DropDownList ddlSalesEngineer = gvDistrict.FooterRow.FindControl("ddlSalesEngineer") as DropDownList;
                ////new DDLBind(ddlSalesEngineer, new BUser().GetUsers(null, null, 7, null, null, true, null, null, null), "ContactName", "UserID", true, "Select Engineer");
                //List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, null, true, null, null, 4);
                //new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
            }
            catch (Exception Ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = Ex.ToString();
                lblMessage.ForeColor = Color.Red;
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
            new DDLBind(ddlGDState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID", true, "Select State");

            DropDownList ddlGDSalesOffice = gvDistrict.FooterRow.FindControl("ddlGDSalesOffice") as DropDownList;
            new DDLBind(ddlGDSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");

            DropDownList ddlGDDealer = gvDistrict.FooterRow.FindControl("ddlGDDealer") as DropDownList;
            new DDLBind(ddlGDDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");

            DropDownList ddlSalesEngineer = gvDistrict.FooterRow.FindControl("ddlSalesEngineer") as DropDownList;
            //new DDLBind(ddlSalesEngineer, new BUser().GetUsers(null, null, 7, null, null, true, null, null, null), "ContactName", "UserID", true, "Select Engineer");
            List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, null, true, null, null, 4);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
        }
        protected void lnkBtnDistrictEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnDistrictEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnDistrictEdit.NamingContainer);

                DropDownList ddlGDCountry = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDCountry");
                ddlGDCountry.Enabled = false;
                DropDownList ddlGDState = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDState");
                ddlGDState.Enabled = false;
                DropDownList ddlGDSalesOffice = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDSalesOffice");
                DropDownList ddlGDDealer = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDDealer");
                Label lblGDDealerID = (Label)row.FindControl("lblGDDealerID");
                ddlGDDealer.SelectedValue = (string.IsNullOrEmpty(lblGDDealerID.Text)) ? "0" : lblGDDealerID.Text;

                DropDownList ddlSalesEngineer = (DropDownList)gvDistrict.FooterRow.FindControl("ddlSalesEngineer");

                List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, Convert.ToInt32(ddlGDDealer.SelectedValue), true, null,(short)DealerDepartment.Sales,null);
                new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");

                TextBox txtGDDistrict = (TextBox)gvDistrict.FooterRow.FindControl("txtGDDistrict");
                Button BtnAddOrUpdateDistrict = (Button)gvDistrict.FooterRow.FindControl("BtnAddOrUpdateDistrict");

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
                //if (ddlSalesEngineer.SelectedValue == "0")
                //{
                //    lblMessage.Text = "Please select Sales Engineer.";
                //    lblMessage.ForeColor = Color.Red;
                //    return;
                //}
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
                    int? SalesEngineerUserID = ddlSalesEngineer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesEngineer.SelectedValue);
                    Success = new BDMS_Address().InsertOrUpdateAddressDistrict(Convert.ToInt32(HiddenID.Value), Convert.ToInt32(ddlGDCountry.SelectedValue), Convert.ToInt32(ddlGDState.SelectedValue), Convert.ToInt32(ddlGDSalesOffice.SelectedValue), Convert.ToInt32(ddlGDDealer.SelectedValue), SalesEngineerUserID, District, null, true, PSession.User.UserID);

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
        protected void BtnSearchDistrict_Click(object sender, EventArgs e)
        {
            FillGridDistrict(); 
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
            new DDLBind(ddlGDState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID", true, "Select State");
            DropDownList ddlGDSalesOffice = gvDistrict.FooterRow.FindControl("ddlGDSalesOffice") as DropDownList;
            new DDLBind(ddlGDSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");
            DropDownList ddlGDDealer = gvDistrict.FooterRow.FindControl("ddlGDDealer") as DropDownList;
            new DDLBind(ddlGDDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");
            //DropDownList ddlSalesEngineer = gvDistrict.FooterRow.FindControl("ddlSalesEngineer") as DropDownList;
            ////new DDLBind(ddlSalesEngineer, new BUser().GetUsers(null, null, 7, null, null, true, null, null, null), "ContactName", "UserID", true, "Select Engineer");
            //List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, null, true, null, null, 4);
            //new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID");
        }

        public void  ActionControlMange()
        {
            
            gvDistrict.Columns[7].Visible = false;
            
            gvDistrict.ShowFooter = false;
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.AddEditLocation).Count() == 1)
            {
                
                gvDistrict.Columns[7].Visible = true; 
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
        protected void ddlGDDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlGDDealer = (DropDownList)gvDistrict.FooterRow.FindControl("ddlGDDealer");
            DropDownList ddlSalesEngineer = (DropDownList)gvDistrict.FooterRow.FindControl("ddlSalesEngineer");
            List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, Convert.ToInt32(ddlGDDealer.SelectedValue), true, null, null, 4);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
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
                Label lblGDSalesOfficeID = (Label)row.FindControl("lblGDSalesOfficeID");
                Label lblGDDealerID = (Label)row.FindControl("lblGDDealerID");
                Label lblGDSalesEngineerUserID =(Label)row.FindControl("lblGDSalesEngineerUserID");
                int? SalesOfficeID = string.IsNullOrEmpty(lblGDSalesOfficeID.Text) ? (int?)null : Convert.ToInt32(lblGDSalesOfficeID.Text);
                int? DealerID = string.IsNullOrEmpty(lblGDDealerID.Text) ? (int?)null : Convert.ToInt32(lblGDDealerID.Text); 
                int? SalesEngineerUserID = string.IsNullOrEmpty(lblGDSalesEngineerUserID.Text) ? (int?)null : Convert.ToInt32(lblGDSalesEngineerUserID.Text);  
               
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
        protected void ddlDCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStateDLL(ddlDState, Convert.ToInt32(ddlDDealer.SelectedValue), Convert.ToInt32(ddlDCountry.SelectedValue), null, null, null);
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
            DropDownList ddlGDState = (DropDownList)row.FindControl("ddlGDState");
            DropDownList ddlGDSalesOffice = (DropDownList)row.FindControl("ddlGDSalesOffice");
            DropDownList ddlGDDealer = (DropDownList)row.FindControl("ddlGDDealer");
            FillStateDLL(ddlGDState, null, Convert.ToInt32(ddlGDCountry.SelectedValue), null, null, null);
            FillSalesOfficeDLL(ddlGDSalesOffice, null, null);
            FillDealerDLL(ddlGDDealer);
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
        protected void ddlDState_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
       
    }
}