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
    public partial class SalesAndServiceConfiguration : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_DealerSalesConfiguration; } }
       
        protected void Page_PreInit(object sender, EventArgs e)
        { 
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            } 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Dealer Sales Configuration');</script>");
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                try
                {
                    FillCountryDLL(ddlCountry);
                    FillDealer(); 
                }
                catch (Exception Ex)
                {
                    lblMessage.Text = Ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }       
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlRegion.DataSource = new BDMS_Address().GetRegion(Convert.ToInt32(ddlCountry.SelectedValue), null, null);
                ddlRegion.DataValueField = "RegionID";
                ddlRegion.DataTextField = "Region";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, new ListItem("Select Region", "0"));
                ddlRegion_SelectedIndexChanged(null, null);
            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            } 
        }
        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        { 
            try
            {
                ddlState.DataSource = new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(ddlRegion.SelectedValue), null, null);
                ddlState.DataValueField = "StateID";
                ddlState.DataTextField = "State";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Select State", "0"));
                ddlState_SelectedIndexChanged(null, null);
            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        { 
            try
            {
                ddlDistrict.DataSource = new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(ddlRegion.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), null, null, null, "true");
                ddlDistrict.DataValueField = "DistrictID";
                ddlDistrict.DataTextField = "District";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select District", "0"));
            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            } 
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillSalesAndServiceDistrict();
        }
        private void FillDealer()
        {
            try
            {
                FillDealerDLL(ddlSalesDealer);
                FillDealerDLL(ddlServiceDealer);
                FillDealerDLL(ddlSalesRetailer);
                FillDealerDLL(ddlServiceRetailer); 
            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillDealerDLL(DropDownList dll)
        {
            try
            {
                dll.DataValueField = "DID";
                dll.DataTextField = "CodeWithDisplayName";
                dll.DataSource = PSession.User.Dealer;
                dll.DataBind();
                dll.Items.Insert(0, new ListItem("Select Dealer", "0"));
                 

            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
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
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }         
        private void FillSalesAndServiceDistrict()
        {
            try
            {  
                int? CountryID = ddlCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCountry.SelectedValue);
                int? RegionID = ddlRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);
                int? StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
                int? DistrictID = ddlDistrict.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDistrict.SelectedValue);

                int? SalesDealerID = ddlSalesDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesDealer.SelectedValue);
                int? ServiceDealerID = ddlServiceDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServiceDealer.SelectedValue);
                int? SalesRetailerID = ddlSalesRetailer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesRetailer.SelectedValue);
                int? ServiceRetailerID = ddlServiceRetailer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServiceRetailer.SelectedValue);

                List<PDMS_District> District = new BDMS_Address().GetSalesAndServiceDistrict(CountryID, RegionID, StateID, DistrictID, SalesDealerID, ServiceDealerID, SalesRetailerID, ServiceRetailerID);                 
                gvDealerSales.DataSource = District;
                gvDealerSales.DataBind();
                if (District.Count == 0)
                {
                    lblRowCountDealerSales.Visible = false;
                    ibtnDealerSalesLeft.Visible = false;
                    ibtnDealerSalesRight.Visible = false;
                }
                else
                {
                    lblRowCountDealerSales.Visible = true;
                    ibtnDealerSalesLeft.Visible = true;
                    ibtnDealerSalesRight.Visible = true;
                    lblRowCountDealerSales.Text = (((gvDealerSales.PageIndex) * gvDealerSales.PageSize) + 1) + " - " + (((gvDealerSales.PageIndex) * gvDealerSales.PageSize) + gvDealerSales.Rows.Count) + " of " + District.Count;
                }
            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ibtnArrow_Click(object sender, ImageClickEventArgs e)
        { 
        }      
       
        protected void lnkBtnDealerSalesDistrictEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;

                LinkButton lnkBtnDistrictEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnDistrictEdit.NamingContainer);
                Label lblGvDistrictID = (Label)row.FindControl("lblGvDistrictID");
                List<PDMS_District> District = new BDMS_Address().GetSalesAndServiceDistrict(null, null, null, Convert.ToInt32(lblGvDistrictID.Text), null, null, null, null);

                DropDownList ddlGDSalesOffice = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGDSalesOffice");
                new DDLBind(ddlGDSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");
                ddlGDSalesOffice.SelectedValue = District[0].SalesOffice == null ? "0" : District[0].SalesOffice.SalesOfficeID.ToString();
                ddlGDSalesOffice.Visible = true;

                DropDownList ddlGvSalesDealer = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGvSalesDealer");
                new DDLBind(ddlGvSalesDealer, new BDMS_Dealer().GetDealer(null, null, null, null), "DealerCode", "DealerID", true, "Select Dealer");
                ddlGvSalesDealer.SelectedValue = District[0].Dealer == null ? "0" : District[0].Dealer.DealerID.ToString();
                ddlGvSalesDealer.Visible = true;

                Label lblGDSalesEngineerUserID = (Label)row.FindControl("lblGDSalesEngineerUserID");
                DropDownList ddlSalesEngineer = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlSalesEngineer");
                List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, Convert.ToInt32(ddlGvSalesDealer.SelectedValue), true, null, (short)DealerDepartment.Sales, null);
                new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
                ddlSalesEngineer.SelectedValue = District[0].SalesDealerEngineer == null ? "0" : District[0].SalesDealerEngineer.UserID.ToString();
                ddlSalesEngineer.Visible = true;

                HiddenID.Value = lblGvDistrictID.Text;
                ((Button)gvDealerSales.FooterRow.FindControl("BtnUpdateDealerSalesDistrict")).Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected void BtnUpdateDealerSalesDistrict_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red; 
                Boolean Success = true;
                Button BtnUpdateDistrict = (Button)gvDealerSales.FooterRow.FindControl("BtnUpdateDealerSalesDistrict");

                DropDownList ddlGDSalesOffice = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGDSalesOffice");
                if (ddlGDSalesOffice.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select SalesOffice."; 
                    return;
                }
                DropDownList ddlGDSalesDealer = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGDSalesDealer");
                if (ddlGDSalesDealer.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Dealer."; 
                    return;
                }

                DropDownList ddlSalesEngineer = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlSalesEngineer"); 
                int? SalesEngineerUserID = ddlSalesEngineer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesEngineer.SelectedValue);
                Success = new BDMS_Address().UpdateDealerSalesDistrict(Convert.ToInt32(HiddenID.Value), Convert.ToInt32(ddlGDSalesDealer.SelectedValue), Convert.ToInt32(ddlGDSalesOffice.SelectedValue), SalesEngineerUserID, PSession.User.UserID);
                if (Success == true)
                {
                    HiddenID.Value = null;
                    FillSalesAndServiceDistrict();
                    lblMessage.Text = "District successfully updated.";
                    lblMessage.ForeColor = Color.Green;
                    return;
                }
                else if (Success == false)
                {
                    lblMessage.Text = "District already found"; 
                    return;
                }
                else
                {
                    lblMessage.Text = "District not updated successfully...!"; 
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString(); 
            }
        }       
        protected void ddlgvSalesDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlGDSalesDealer = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGDSalesDealer");
            DropDownList ddlSalesEngineer = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlSalesEngineer");
            List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, Convert.ToInt32(ddlGDSalesDealer.SelectedValue), true, null, null, 4);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
        }
    }
}