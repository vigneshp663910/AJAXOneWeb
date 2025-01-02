using Business;
using Newtonsoft.Json;
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
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
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
                PageCount = 0;
                PageIndex = 1;
                try
                {
                    //FillCountryDLL(ddlCountry);
                    ddlCountry_SelectedIndexChanged(null, null);
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
                ddlRegion.DataSource = new BDMS_Address().GetRegion(1, null, null);
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
                ddlState.DataSource = new BDMS_Address().GetState(null, 1, Convert.ToInt32(ddlRegion.SelectedValue), null, null);
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
                ddlDistrict.DataSource = new BDMS_Address().GetDistrict(1, Convert.ToInt32(ddlRegion.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), null, null, null, "true");
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
                fillDealer(ddlSalesDealer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == (short)DealerType.Dealer || m.DealerType.DealerTypeID == (short)DealerType.OEM), "Select Dealer");
                fillDealer(ddlServiceDealer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == (short)DealerType.Dealer || m.DealerType.DealerTypeID == (short)DealerType.OEM), "Select Dealer");
                fillDealer(ddlSalesRetailer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == (short)DealerType.Retailer || m.DealerType.DealerTypeID == (short)DealerType.OEM), "Select Retailer");
                fillDealer(ddlServiceRetailer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == (short)DealerType.Retailer || m.DealerType.DealerTypeID == (short)DealerType.OEM), "Select Retailer"); 
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
               // int? CountryID = ddlCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCountry.SelectedValue);
                int? RegionID = ddlRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);
                int? StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
                int? DistrictID = ddlDistrict.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDistrict.SelectedValue);

                int? SalesDealerID = ddlSalesDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesDealer.SelectedValue);
                int? ServiceDealerID = ddlServiceDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServiceDealer.SelectedValue);
                int? SalesRetailerID = ddlSalesRetailer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesRetailer.SelectedValue);
                int? ServiceRetailerID = ddlServiceRetailer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServiceRetailer.SelectedValue);
               
               // List<PDMS_District> District
                      PApiResult Result = new BDMS_Address().GetSalesAndServiceDistrict(1, RegionID, StateID, DistrictID, SalesDealerID, ServiceDealerID, SalesRetailerID, ServiceRetailerID, PageIndex, gvDealerSales.PageSize);
                List<PDMS_District> dd = JsonConvert.DeserializeObject<List<PDMS_District>>(JsonConvert.SerializeObject(Result.Data));
                gvDealerSales.DataSource = dd;
                gvDealerSales.DataBind();
                gvDealerService.DataSource = dd;
                gvDealerService.DataBind();

                gvRetailerSales.DataSource = dd;
                gvRetailerSales.DataBind();

                gvRetailerService.DataSource = dd;
                gvRetailerService.DataBind();

                gvList.DataSource = dd;
                gvList.DataBind();

                if (Result.RowCount == 0)
                {
                    lblRowCountDealerSales.Visible = false;
                    ibtnDealerSalesLeft.Visible = false;
                    ibtnDealerSalesRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvDealerSales.PageSize - 1) / gvDealerSales.PageSize;
                    lblRowCountDealerSales.Visible = true;
                    ibtnDealerSalesLeft.Visible = true;
                    ibtnDealerSalesRight.Visible = true;
                    lblRowCountDealerSales.Text = (((PageIndex - 1) * gvDealerSales.PageSize) + 1) + " - " + (((PageIndex - 1) * gvDealerSales.PageSize) + gvDealerSales.Rows.Count) + " of " + Result.RowCount;
                }
                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EditAllSalesAndServiceConfiguration).Count() == 0)
                {
                    for (int i = 0; i < gvDealerSales.Rows.Count; i++)
                    { 
                        ((LinkButton)gvDealerService.Rows[i].FindControl("lnkBtnDealerServiceEdit")).Visible = false; 
                        ((LinkButton)gvRetailerService.Rows[i].FindControl("lnkBtnRetailerServiceEdit")).Visible = false;
                    }
                }
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EditAllSalesAndServiceConfiguration
                || A.SubModuleChildID == (short)SubModuleChildMaster.EditDealerSalesEngineer 
                ).Count() == 0)
                {
                    for (int i = 0; i < gvDealerSales.Rows.Count; i++)
                    {
                        ((LinkButton)gvDealerSales.Rows[i].FindControl("lnkBtnDealerSalesEdit")).Visible = false;  
                    }
                }

                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EditAllSalesAndServiceConfiguration 
                || A.SubModuleChildID == (short)SubModuleChildMaster.EditRetailerSalesEngineer
                ).Count() == 0)
                {
                    for (int i = 0; i < gvDealerSales.Rows.Count; i++)
                    { 
                        ((LinkButton)gvRetailerSales.Rows[i].FindControl("lnkBtnRetailerSalesEdit")).Visible = false;
                    }
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
            ImageButton lbActions = ((ImageButton)sender);
            if (lbActions.ID == "ibtnDealerSalesLeft")
            {
                if (PageIndex > 1)
                {
                    PageIndex = PageIndex - 1;
                    FillSalesAndServiceDistrict();
                }
            }
            else if (lbActions.ID == "ibtnDealerSalesRight")
            {
                if (PageCount > PageIndex)
                {
                    PageIndex = PageIndex + 1;
                    FillSalesAndServiceDistrict();
                }
            } 
        }      
       
        protected void lnkBtnDealerSalesEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;

                LinkButton lnkBtnDistrictEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnDistrictEdit.NamingContainer);
                Label lblGvDistrictID = (Label)row.FindControl("lblGvDistrictID");
                PApiResult Result = new BDMS_Address().GetSalesAndServiceDistrict(null, null, null, Convert.ToInt32(lblGvDistrictID.Text), null, null, null, null,1,1);
                List<PDMS_District> District = JsonConvert.DeserializeObject<List<PDMS_District>>(JsonConvert.SerializeObject(Result.Data));
               
                
                DropDownList ddlGvSalesOffice = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGvSalesOffice");
                new DDLBind(ddlGvSalesOffice, new BDMS_Address().GetSalesOffice(null, null), "SalesOffice", "SalesOfficeID", true, "Select SalesOffice");
                ddlGvSalesOffice.SelectedValue = District[0].SalesOffice == null ? "0" : District[0].SalesOffice.SalesOfficeID.ToString();
                //ddlGvSalesOffice.Visible = true;
                Label lblFGvSalesOffice = (Label)gvDealerSales.FooterRow.FindControl("lblFGvSalesOffice");
                lblFGvSalesOffice.Text = District[0].SalesOffice == null ? "" : District[0].SalesOffice.SalesOffice;


                DropDownList ddlGvSalesDealer = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGvSalesDealer");
                fillDealer(ddlGvSalesDealer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == (short)DealerType.Dealer || m.DealerType.DealerTypeID == (short)DealerType.OEM), "Select Retailer"); 
                ddlGvSalesDealer.SelectedValue = District[0].Dealer == null ? "0" : District[0].Dealer.DealerID.ToString();
                //ddlGvSalesDealer.Visible = true;

                Label lblFGvSalesDealer = (Label)gvDealerSales.FooterRow.FindControl("lblFGvSalesDealer");
                lblFGvSalesDealer.Text = District[0].Dealer == null ? "" : District[0].Dealer.DealerCode;
                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EditAllSalesAndServiceConfiguration).Count() == 1)
                {
                    lblFGvSalesDealer.Visible = false;
                    lblFGvSalesOffice.Visible = false;
                    ddlGvSalesDealer.Visible = true;
                    ddlGvSalesOffice.Visible = true;
                }
                else if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EditDealerSalesEngineer).Count() == 1)
                {
                    lblFGvSalesDealer.Visible = true;
                    lblFGvSalesOffice.Visible = true;
                    ddlGvSalesDealer.Visible = false;
                    ddlGvSalesOffice.Visible = false;
                }


                DropDownList ddlGvDealerSalesEngineer = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGvDealerSalesEngineer");
                List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, Convert.ToInt32(ddlGvSalesDealer.SelectedValue), true, null, (short)DealerDepartment.Sales, null);
                new DDLBind(ddlGvDealerSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
                ddlGvDealerSalesEngineer.SelectedValue = District[0].SalesDealerEngineer == null ? "0" : District[0].SalesDealerEngineer.UserID.ToString();
                ddlGvDealerSalesEngineer.Visible = true;

                ((Label)gvDealerSales.FooterRow.FindControl("lblfGvDistrictID")).Text = lblGvDistrictID.Text; 
                ((Button)gvDealerSales.FooterRow.FindControl("BtnDealerSalesUpdate")).Visible = true;
                ((Label)gvDealerSales.FooterRow.FindControl("lblfRegion")).Text = District[0].State.Region.Region;
                ((Label)gvDealerSales.FooterRow.FindControl("lblfRegion")).Visible = true;
                ((Label)gvDealerSales.FooterRow.FindControl("lblfState")).Text = District[0].State.State;
                ((Label)gvDealerSales.FooterRow.FindControl("lblfState")).Visible = true;
                ((Label)gvDealerSales.FooterRow.FindControl("lblfDistrict")).Text = District[0].District;
                ((Label)gvDealerSales.FooterRow.FindControl("lblfDistrict")).Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected void BtnDealerSalesUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                Boolean Success = true;
                DropDownList ddlGvSOffice = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGvSalesOffice");
                if (ddlGvSOffice.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Sales Office.";
                    return;
                }
                DropDownList ddlGvSalesDealer = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGvSalesDealer");
                if (ddlGvSalesDealer.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Dealer.";
                    return;
                }

                DropDownList ddlEngineer = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGvDealerSalesEngineer");
                int? EngineerUserID = ddlEngineer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlEngineer.SelectedValue);

                Label lblfGvDistrictID = (Label)gvDealerSales.FooterRow.FindControl("lblfGvDistrictID");

                PApiResult Result = new BDMS_Address().UpdateSalesAndServiceDistrict(Convert.ToInt32(lblfGvDistrictID.Text), Convert.ToInt32(ddlGvSalesDealer.SelectedValue), Convert.ToInt32(ddlGvSOffice.SelectedValue), EngineerUserID, 1);
                if (Result.Status == PApplication.Success)
                {
                    lblfGvDistrictID.Text = "";
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
            DropDownList ddlGvSalesDealer = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGvSalesDealer");
            DropDownList ddlGvDealerSalesEngineer = (DropDownList)gvDealerSales.FooterRow.FindControl("ddlGvDealerSalesEngineer");
            List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, Convert.ToInt32(ddlGvSalesDealer.SelectedValue), true, null, null, 4);
            new DDLBind(ddlGvDealerSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
        }


        protected void lnkBtnDealerServiceEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;

                LinkButton lnkBtnDistrictEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnDistrictEdit.NamingContainer);

                Label lblGvDistrictID = (Label)row.FindControl("lblGvDistrictID");
                PApiResult Result = new BDMS_Address().GetSalesAndServiceDistrict(null, null, null, Convert.ToInt32(lblGvDistrictID.Text), null, null, null, null, 1, 1);
                List<PDMS_District> District = JsonConvert.DeserializeObject<List<PDMS_District>>(JsonConvert.SerializeObject(Result.Data));

                 

                DropDownList ddlGvServiceDealer = (DropDownList)gvDealerService.FooterRow.FindControl("ddlGvServiceDealer");
                fillDealer(ddlGvServiceDealer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == (short)DealerType.Dealer || m.DealerType.DealerTypeID == (short)DealerType.OEM), "Select Retailer");  
                ddlGvServiceDealer.SelectedValue = District[0].ServiceDealer == null ? "0" : District[0].ServiceDealer.DealerID.ToString();
                ddlGvServiceDealer.Visible = true;

                ((Label)gvDealerService.FooterRow.FindControl("lblfGvDistrictID")).Text = lblGvDistrictID.Text; 
                ((Button)gvDealerService.FooterRow.FindControl("BtnDealerServiceUpdate")).Visible = true;
                ((Label)gvDealerService.FooterRow.FindControl("lblfRegion")).Text = District[0].State.Region.Region;
                ((Label)gvDealerService.FooterRow.FindControl("lblfRegion")).Visible = true;
                ((Label)gvDealerService.FooterRow.FindControl("lblfState")).Text = District[0].State.State;
                ((Label)gvDealerService.FooterRow.FindControl("lblfState")).Visible = true;
                ((Label)gvDealerService.FooterRow.FindControl("lblfDistrict")).Text = District[0].District;
                ((Label)gvDealerService.FooterRow.FindControl("lblfDistrict")).Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected void BtnDealerServiceUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                Boolean Success = true;

                DropDownList ddlGvServiceDealer = (DropDownList)gvDealerService.FooterRow.FindControl("ddlGvServiceDealer");
                if (ddlGvServiceDealer.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Dealer.";
                    return;
                }
                Label lblfGvDistrictID = (Label)gvDealerService.FooterRow.FindControl("lblfGvDistrictID");

                PApiResult Result = new BDMS_Address().UpdateSalesAndServiceDistrict(Convert.ToInt32(lblfGvDistrictID.Text), Convert.ToInt32(ddlGvServiceDealer.SelectedValue), null, null, 2);
                if (Result.Status == PApplication.Success)
                {
                    lblfGvDistrictID.Text = "";
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

        protected void lnkBtnRetailerSalesEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;

                LinkButton lnkBtnDistrictEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnDistrictEdit.NamingContainer);
                Label lblGvDistrictID = (Label)row.FindControl("lblGvDistrictID");
                PApiResult Result = new BDMS_Address().GetSalesAndServiceDistrict(null, null, null, Convert.ToInt32(lblGvDistrictID.Text), null, null, null, null, 1, 1);
                List<PDMS_District> District = JsonConvert.DeserializeObject<List<PDMS_District>>(JsonConvert.SerializeObject(Result.Data));

                 

                DropDownList ddlGvSalesRetailer = (DropDownList)gvRetailerSales.FooterRow.FindControl("ddlGvSalesRetailer");
                fillDealer(ddlGvSalesRetailer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == (short)DealerType.Retailer || m.DealerType.DealerTypeID == (short)DealerType.OEM), "Select Retailer");  
                ddlGvSalesRetailer.SelectedValue = District[0].SalesRetailer == null ? "0" : District[0].SalesRetailer.DealerID.ToString();
                //ddlGvSalesRetailer.Visible = true;

                Label lblFGvSalesRetailer = (Label)gvRetailerSales.FooterRow.FindControl("lblFGvSalesRetailer");
                lblFGvSalesRetailer.Text = District[0].SalesRetailer == null ? "" : District[0].SalesRetailer.DealerCode;
                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EditAllSalesAndServiceConfiguration).Count() == 1)
                {
                    lblFGvSalesRetailer.Visible = false;
                    ddlGvSalesRetailer.Visible = true;
                }
                else if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EditRetailerSalesEngineer).Count() == 1)
                {
                    lblFGvSalesRetailer.Visible = true;
                    ddlGvSalesRetailer.Visible = false;
                }

                DropDownList ddlGvRetailerSalesEngineer = (DropDownList)gvRetailerSales.FooterRow.FindControl("ddlGvRetailerSalesEngineer");
                List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, Convert.ToInt32(ddlGvSalesRetailer.SelectedValue), true, null, (short)DealerDepartment.Sales, null);
                new DDLBind(ddlGvRetailerSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
                ddlGvRetailerSalesEngineer.SelectedValue = District[0].SalesRetailerEngineer == null ? "0" : District[0].SalesRetailerEngineer.UserID.ToString();
               
                ddlGvRetailerSalesEngineer.Visible = true;



                ((Label)gvRetailerSales.FooterRow.FindControl("lblfGvDistrictID")).Text = lblGvDistrictID.Text;
                ((Button)gvRetailerSales.FooterRow.FindControl("BtnRetailerSalesUpdate")).Visible = true;
                ((Label)gvRetailerSales.FooterRow.FindControl("lblfRegion")).Text = District[0].State.Region.Region;
                ((Label)gvRetailerSales.FooterRow.FindControl("lblfRegion")).Visible = true;
                ((Label)gvRetailerSales.FooterRow.FindControl("lblfState")).Text = District[0].State.State;
                ((Label)gvRetailerSales.FooterRow.FindControl("lblfState")).Visible = true;
                ((Label)gvRetailerSales.FooterRow.FindControl("lblfDistrict")).Text = District[0].District;
                ((Label)gvRetailerSales.FooterRow.FindControl("lblfDistrict")).Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected void BtnRetailerSalesUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                Boolean Success = true;
                 
                DropDownList ddlGvSalesRetailer = (DropDownList)gvRetailerSales.FooterRow.FindControl("ddlGvSalesRetailer");
                if (ddlGvSalesRetailer.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Retailer.";
                    return;
                }

                DropDownList ddlEngineer = (DropDownList)gvRetailerSales.FooterRow.FindControl("ddlGvRetailerSalesEngineer");
                int? EngineerUserID = ddlEngineer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlEngineer.SelectedValue);

                Label lblfGvDistrictID = (Label)gvRetailerSales.FooterRow.FindControl("lblfGvDistrictID");

                PApiResult Result = new BDMS_Address().UpdateSalesAndServiceDistrict(Convert.ToInt32(lblfGvDistrictID.Text), Convert.ToInt32(ddlGvSalesRetailer.SelectedValue), null, EngineerUserID,3);
                if (Result.Status == PApplication.Success)
                {
                    lblfGvDistrictID.Text = "";
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
        protected void ddlgvSalesRetailer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlGvSalesRetailer = (DropDownList)gvRetailerSales.FooterRow.FindControl("ddlGvSalesRetailer");
            DropDownList ddlGvRetailerSalesEngineer = (DropDownList)gvRetailerSales.FooterRow.FindControl("ddlGvRetailerSalesEngineer");
            List<PUser> DealerUser = new BUser().GetUsers(null, null, 7, null, Convert.ToInt32(ddlGvSalesRetailer.SelectedValue), true, null, null, 4);
            new DDLBind(ddlGvRetailerSalesEngineer, DealerUser, "ContactName", "UserID", true, "Select Sales Engineer");
        }

        protected void lnkBtnRetailerServiceEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;

                LinkButton lnkBtnDistrictEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnDistrictEdit.NamingContainer);

                Label lblGvDistrictID = (Label)row.FindControl("lblGvDistrictID");
                PApiResult Result = new BDMS_Address().GetSalesAndServiceDistrict(null, null, null, Convert.ToInt32(lblGvDistrictID.Text), null, null, null, null, 1, 1);
                List<PDMS_District> District = JsonConvert.DeserializeObject<List<PDMS_District>>(JsonConvert.SerializeObject(Result.Data));
                                  
                DropDownList ddlGvServiceRetailer = (DropDownList)gvRetailerService.FooterRow.FindControl("ddlGvServiceRetailer");
                fillDealer(ddlGvServiceRetailer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == (short)DealerType.Retailer || m.DealerType.DealerTypeID == (short)DealerType.OEM), "Select Retailer");
                ddlGvServiceRetailer.Visible = true;
                ddlGvServiceRetailer.SelectedValue = District[0].ServiceRetailer == null ? "0" : District[0].ServiceRetailer.DealerID.ToString();



                ((Label)gvRetailerService.FooterRow.FindControl("lblfGvDistrictID")).Text = lblGvDistrictID.Text;
                ((Button)gvRetailerService.FooterRow.FindControl("BtnRetailerServiceUpdate")).Visible = true;
                ((Label)gvRetailerService.FooterRow.FindControl("lblfRegion")).Text = District[0].State.Region.Region;
                ((Label)gvRetailerService.FooterRow.FindControl("lblfRegion")).Visible = true;
                ((Label)gvRetailerService.FooterRow.FindControl("lblfState")).Text = District[0].State.State;
                ((Label)gvRetailerService.FooterRow.FindControl("lblfState")).Visible = true;
                ((Label)gvRetailerService.FooterRow.FindControl("lblfDistrict")).Text = District[0].District;
                ((Label)gvRetailerService.FooterRow.FindControl("lblfDistrict")).Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected void BtnRetailerServiceUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                Boolean Success = true;

                DropDownList ddlGvServiceRetailer = (DropDownList)gvRetailerService.FooterRow.FindControl("ddlGvServiceRetailer");
                if (ddlGvServiceRetailer.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Retailer.";
                    return;
                }

                Label lblfGvDistrictID = (Label)gvRetailerService.FooterRow.FindControl("lblfGvDistrictID");
                PApiResult Result = new BDMS_Address().UpdateSalesAndServiceDistrict(Convert.ToInt32(lblfGvDistrictID.Text), Convert.ToInt32(ddlGvServiceRetailer.SelectedValue), null, null,4);
                if (Result.Status == PApplication.Success)
                {
                    lblfGvDistrictID.Text = "";
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

        void fillDealer(DropDownList dll, object Data,string select)
        {
            dll.DataTextField = "CodeWithName";
            dll.DataValueField = "DealerID";
            dll.DataSource = Data;
            dll.DataBind();
            dll.Items.Insert(0, new ListItem(select, "0"));
        }

    }
}