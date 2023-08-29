using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class DealerBinLocation : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMaster_DealerBinLocation; } }
        int? DealerID = null;
        int? OfficeCodeID = null;

        int? CDealerID = null;
        int? COfficeCodeID = null;
        int? CDealerBinLocationID = null;
        int? CMaterialID = null;
        string MaterialCode = null;
        public List<PDealerBinLocation> PDealerBinLocationHeader
        {
            get
            {
                if (ViewState["PDealerBinLocationHeader"] == null)
                {
                    ViewState["PDealerBinLocationHeader"] = new List<PDealerBinLocation>();
                }
                return (List<PDealerBinLocation>)ViewState["PDealerBinLocationHeader"];
            }
            set
            {
                ViewState["PDealerBinLocationHeader"] = value;
            }
        }
        public List<PDealerBinLocation> PDealerBinLocationConfigHeader
        {
            get
            {
                if (ViewState["PDealerBinLocationConfigHeader"] == null)
                {
                    ViewState["PDealerBinLocationConfigHeader"] = new List<PDealerBinLocation>();
                }
                return (List<PDealerBinLocation>)ViewState["PDealerBinLocationConfigHeader"];
            }
            set
            {
                ViewState["PDealerBinLocationConfigHeader"] = value;
            }
        }
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
            Session["previousUrl"] = "DealerBinLocation.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Dealer Bin Location');</script>");
            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                LoadDropdown();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
                fillDealerBinLocation();
                fillDealerBinLocationConfig();
            }
        }
        void LoadDropdown()
        {
            fillDealer();

            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(0, null, null), "OfficeName", "OfficeID");
            new DDLBind(ddlCOfficeName, new BDMS_Dealer().GetDealerOffice(0, null, null), "OfficeName", "OfficeID");
            new DDLBind(ddlCOffice, new BDMS_Dealer().GetDealerOffice(0, null, null), "OfficeName", "OfficeID");
            new DDLBind(ddlCDealerOfficeConfig, new BDMS_Dealer().GetDealerOffice(0, null, null), "OfficeName", "OfficeID");

            new DDLBind(ddlCBin, new BDMS_Dealer().GetDealerBin(0, null), "BinName", "DealerBinLocationID");
            new DDLBind(ddlCDealerBinConfig, new BDMS_Dealer().GetDealerBin(0, null), "BinName", "DealerBinLocationID");
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("Select", "0"));

            ddlCDealerCode.DataTextField = "CodeWithName";
            ddlCDealerCode.DataValueField = "DID";
            ddlCDealerCode.DataSource = PSession.User.Dealer;
            ddlCDealerCode.DataBind();
            ddlCDealerCode.Items.Insert(0, new ListItem("Select", "0"));

            ddlCDealer.DataTextField = "CodeWithName";
            ddlCDealer.DataValueField = "DID";
            ddlCDealer.DataSource = PSession.User.Dealer;
            ddlCDealer.DataBind();
            ddlCDealer.Items.Insert(0, new ListItem("Select", "0"));

            ddlCDealerConfig.DataTextField = "CodeWithName";
            ddlCDealerConfig.DataValueField = "DID";
            ddlCDealerConfig.DataSource = PSession.User.Dealer;
            ddlCDealerConfig.DataBind();
            ddlCDealerConfig.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillDealerBinLocation();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void Search()
        {
            DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            OfficeCodeID = ddlOfficeName.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlOfficeName.SelectedValue);
        }
        void fillDealerBinLocation()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                int RowCount = 0;
                PDealerBinLocationHeader = new BDMS_Dealer().GetDealerBinLocation(DealerID, OfficeCodeID, Convert.ToInt32(PSession.User.UserID), PageIndex, gvDealerBinLocation.PageSize, out RowCount);
                gvDealerBinLocation.PageIndex = 0;
                gvDealerBinLocation.DataSource = PDealerBinLocationHeader;
                gvDealerBinLocation.DataBind();

                if (RowCount == 0)
                {
                    gvDealerBinLocation.DataSource = null;
                    gvDealerBinLocation.DataBind();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    gvDealerBinLocation.DataSource = PDealerBinLocationHeader;
                    gvDealerBinLocation.DataBind();
                    PageCount = (RowCount + gvDealerBinLocation.PageSize - 1) / gvDealerBinLocation.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvDealerBinLocation.PageSize) + 1) + " - " + (((PageIndex - 1) * gvDealerBinLocation.PageSize) + gvDealerBinLocation.Rows.Count) + " of " + RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DealerBinLocation", "fillDealerBinLocation", e1);
                throw e1;
            }
        }
        protected void gvDealerBinLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerBinLocation.PageIndex = e.NewPageIndex;
            fillDealerBinLocation();
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillDealerBinLocation();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillDealerBinLocation();
            }
        }
        protected void btnCreateBinLocation_Click(object sender, EventArgs e)
        {
            ddlCDealerCode.Enabled = true;
            ddlCOfficeName.Enabled = true;
            btnSave.Text = "Save";
            HidDealerBinLocationID.Value = "";
            LoadDropdown();
            txtBinName.Text = "";
            lblMessageDealerBinLocation.Text = "";
            lblMessage.Text = "";
            MPE_DealerBinLocationCreate.Show();
        }
        protected void ddlDealerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerID = (ddlDealerCode.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(DealerID, null, null), "OfficeName", "OfficeID");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean Result = Validation();
                if (Result)
                {
                    MPE_DealerBinLocationCreate.Show();
                    return;
                }
                PDealerBinLocation pDealerBin = new PDealerBinLocation();
                if (btnSave.Text == "Update")
                {
                    pDealerBin.DealerBinLocationID = Convert.ToInt32(HidDealerBinLocationID.Value);
                }
                pDealerBin.BinName = txtBinName.Text.Trim();
                pDealerBin.Dealer = new PDealer() { DealerID = Convert.ToInt32(ddlCDealerCode.SelectedValue) };
                pDealerBin.DealerOffice = new PDMS_DealerOffice() { OfficeID = Convert.ToInt32(ddlCOfficeName.SelectedValue) };

                Boolean success = new BDMS_Dealer().InsertOrUpdateDealerBinLocation(pDealerBin, true, PSession.User.UserID);
                if (success)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Bin Location is Created Successfully..";
                    lblMessage.Visible = true;
                    fillDealerBinLocation();
                }
                else
                {
                    lblMessageDealerBinLocation.ForeColor = Color.Red;
                    lblMessageDealerBinLocation.Text = "Bin Location is Not Created Successfully..!";
                    lblMessageDealerBinLocation.Visible = true;
                    MPE_DealerBinLocationCreate.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageDealerBinLocation.ForeColor = Color.Red;
                lblMessageDealerBinLocation.Text = ex.Message.ToString();
                lblMessageDealerBinLocation.Visible = true;
                MPE_DealerBinLocationCreate.Show();
                return;
            }
        }
        protected void ddlCDealerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CrDealerID = (ddlCDealerCode.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlCDealerCode.SelectedValue);
            new DDLBind(ddlCOfficeName, new BDMS_Dealer().GetDealerOffice(CrDealerID, null, null), "OfficeName", "OfficeID");
            MPE_DealerBinLocationCreate.Show();
        }
        public Boolean Validation()
        {
            Boolean Result = false;
            if (ddlCDealerCode.SelectedValue == "0")
            {
                lblMessageDealerBinLocation.ForeColor = Color.Red;
                lblMessageDealerBinLocation.Text = "Please Select Dealer..!";
                lblMessageDealerBinLocation.Visible = true;
                Result = true;
            }
            if (ddlCOfficeName.SelectedValue == "0")
            {
                lblMessageDealerBinLocation.ForeColor = Color.Red;
                lblMessageDealerBinLocation.Text = "Please Select Office..!";
                lblMessageDealerBinLocation.Visible = true;
                Result = true;
            }
            if (string.IsNullOrEmpty(txtBinName.Text))
            {
                lblMessageDealerBinLocation.ForeColor = Color.Red;
                lblMessageDealerBinLocation.Text = "Please Enter Bin Name...!";
                lblMessageDealerBinLocation.Visible = true;
                Result = true;
            }
            return Result;
        }
        protected void lnkEditDealerBinLocation_Click(object sender, EventArgs e)
        {
            LinkButton lnkEditDealerBinLocation = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(lnkEditDealerBinLocation.NamingContainer);
            Label lblBinName = (Label)row.FindControl("lblBinName");
            Label lblDealerID = (Label)row.FindControl("lblDealerID");
            Label lblOfficeCodeID = (Label)row.FindControl("lblOfficeCodeID");
            Label lblDealerBinLocationID = (Label)row.FindControl("lblDealerBinLocationID");
            txtBinName.Text = lblBinName.Text;
            ddlCDealerCode.SelectedValue = lblDealerID.Text;
            ddlCDealerCode.Enabled = false;
            new DDLBind(ddlCOfficeName, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(lblDealerID.Text), null, null), "OfficeName", "OfficeID");
            ddlCOfficeName.SelectedValue = lblOfficeCodeID.Text;
            ddlCOfficeName.Enabled = false;
            HidDealerBinLocationID.Value = lblDealerBinLocationID.Text;
            lblMessageDealerBinLocation.Text = "";
            lblMessage.Text = "";
            btnSave.Text = "Update";
            MPE_DealerBinLocationCreate.Show();
        }
        protected void lnkDeleteDealerBinLocation_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            LinkButton lnkDeleteDealerBinLocation = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(lnkDeleteDealerBinLocation.NamingContainer);
            Label lblBinName = (Label)row.FindControl("lblBinName");
            Label lblDealerID = (Label)row.FindControl("lblDealerID");
            Label lblOfficeCodeID = (Label)row.FindControl("lblOfficeCodeID");
            Label lblDealerBinLocationID = (Label)row.FindControl("lblDealerBinLocationID");
            HidDealerBinLocationID.Value = lblDealerBinLocationID.Text;

            PDealerBinLocation pDealerBin = new PDealerBinLocation();
            pDealerBin.DealerBinLocationID = Convert.ToInt32(HidDealerBinLocationID.Value);
            pDealerBin.BinName = lblBinName.Text;
            pDealerBin.Dealer = new PDealer() { DealerID = Convert.ToInt32(lblDealerID.Text) };
            pDealerBin.DealerOffice = new PDMS_DealerOffice() { OfficeID = Convert.ToInt32(lblOfficeCodeID.Text) };


            if (PDealerBinLocationConfigHeader.Any(item => item.DealerBinLocationID == pDealerBin.DealerBinLocationID))
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Bin Location Reference is Available in Material Mapping...!";
                lblMessage.Visible = true;
                return;
            }

            Boolean success = new BDMS_Dealer().InsertOrUpdateDealerBinLocation(pDealerBin, false, PSession.User.UserID);
            if (success)
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Bin Location is Deleted Successfully..";
                lblMessage.Visible = true;
                fillDealerBinLocation();
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Bin Location is Not Deleted Successfully..!";
                lblMessage.Visible = true;
                return;
            }
        }
        protected void ddlCDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CDealerID = (ddlCDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlCDealer.SelectedValue);
            new DDLBind(ddlCOffice, new BDMS_Dealer().GetDealerOffice(CDealerID, null, null), "OfficeName", "OfficeID");
        }
        protected void btnCSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillDealerBinLocationConfig();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillDealerBinLocationConfig()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                CSearch();
                int RowCount = 0;
                PDealerBinLocationConfigHeader = new BDMS_Dealer().GetDealerBinLocationMaterialMappingHeader(CDealerID, COfficeCodeID, CDealerBinLocationID, MaterialCode, Convert.ToInt32(PSession.User.UserID), PageIndex, gvDealerBinLocationConfig.PageSize, out RowCount);
                gvDealerBinLocationConfig.PageIndex = 0;
                gvDealerBinLocationConfig.DataSource = PDealerBinLocationConfigHeader;
                gvDealerBinLocationConfig.DataBind();

                if (RowCount == 0)
                {
                    gvDealerBinLocationConfig.DataSource = null;
                    gvDealerBinLocationConfig.DataBind();
                    lblCRowCount.Visible = false;
                    ibtnCArrowLeft.Visible = false;
                    ibtnCArrowRight.Visible = false;
                }
                else
                {
                    gvDealerBinLocationConfig.DataSource = PDealerBinLocationConfigHeader;
                    gvDealerBinLocationConfig.DataBind();
                    PageCount = (RowCount + gvDealerBinLocationConfig.PageSize - 1) / gvDealerBinLocationConfig.PageSize;
                    lblCRowCount.Visible = true;
                    ibtnCArrowLeft.Visible = true;
                    ibtnCArrowRight.Visible = true;
                    lblCRowCount.Text = (((PageIndex - 1) * gvDealerBinLocationConfig.PageSize) + 1) + " - " + (((PageIndex - 1) * gvDealerBinLocationConfig.PageSize) + gvDealerBinLocationConfig.Rows.Count) + " of " + RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DealerBinLocation", "fillDealerBinLocationConfig", e1);
                throw e1;
            }
        }
        void CSearch()
        {
            CDealerID = ddlCDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCDealer.SelectedValue);
            COfficeCodeID = ddlCOffice.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCOffice.SelectedValue);
            CDealerBinLocationID = ddlCBin.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCBin.SelectedValue);
            CMaterialID = (int?)null;
            MaterialCode = txtSMaterial.Text.Trim();
        }
        protected void btnCreateBinConfiguration_Click(object sender, EventArgs e)
        {
            ddlCDealerConfig.Enabled = true;
            ddlCDealerOfficeConfig.Enabled = true;
            ddlCDealerBinConfig.Enabled = true;
            lblMessageDealerBinLocationConfig.Text = "";
            lblMessage.Text = "";
            txtMaterial.Text = "";
            btnSaveConfig.Text = "Save";
            HidDealerBinLocationMaterialMappingID.Value = "";
            LoadDropdown();
            MPE_DealerBinLocationConfigCreate.Show();
        }
        protected void ibtnCArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillDealerBinLocationConfig();
            }
        }
        protected void ibtnCArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillDealerBinLocationConfig();
            }
        }
        protected void gvDealerBinLocationConfig_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerBinLocationConfig.PageIndex = e.NewPageIndex;
            fillDealerBinLocationConfig();
        }
        protected void ddlCOffice_SelectedIndexChanged(object sender, EventArgs e)
        {
            CDealerID = (ddlCDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlCDealer.SelectedValue);
            COfficeCodeID = (ddlCOffice.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlCOffice.SelectedValue);
            new DDLBind(ddlCBin, new BDMS_Dealer().GetDealerBin(CDealerID, COfficeCodeID), "BinName", "DealerBinLocationID");
        }
        protected void ddlCDealerConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CrDealerConfigID = (ddlCDealerConfig.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlCDealerConfig.SelectedValue);
            new DDLBind(ddlCDealerOfficeConfig, new BDMS_Dealer().GetDealerOffice(CrDealerConfigID, null, null), "OfficeName", "OfficeID");
            new DDLBind(ddlCDealerBinConfig, new BDMS_Dealer().GetDealerBin(CrDealerConfigID, null), "BinName", "DealerBinLocationID");
            MPE_DealerBinLocationConfigCreate.Show();
        }
        protected void btnSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean Result = ValidationConfig();
                if (Result)
                {
                    MPE_DealerBinLocationConfigCreate.Show();
                    return;
                }
                PDealerBinLocation pDealerBin = new PDealerBinLocation();
                if (btnSaveConfig.Text == "Update")
                {
                    pDealerBin.DealerBinLocationMaterialMappingID = Convert.ToInt32(HidDealerBinLocationMaterialMappingID.Value);
                }
                pDealerBin.DealerBinLocationID = Convert.ToInt32(ddlCDealerBinConfig.SelectedValue);
                pDealerBin.Dealer = new PDealer() { DealerID = Convert.ToInt32(ddlCDealerConfig.SelectedValue) };
                pDealerBin.DealerOffice = new PDMS_DealerOffice() { OfficeID = Convert.ToInt32(ddlCDealerOfficeConfig.SelectedValue) };
                string Material = txtMaterial.Text.Trim();
                Material = Material.Split(' ')[0];
                PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, Material, null, null, null)[0];
                pDealerBin.Material = new PDMS_Material() { MaterialID = MM.MaterialID };
                Boolean success = new BDMS_Dealer().InsertOrUpdateDealerBinLocationMaterialMapping(pDealerBin, true, PSession.User.UserID);
                if (success)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Bin Location Configuration is Created Successfully..";
                    lblMessage.Visible = true;
                    fillDealerBinLocationConfig();
                }
                else
                {
                    lblMessageDealerBinLocationConfig.ForeColor = Color.Red;
                    lblMessageDealerBinLocationConfig.Text = "Bin Location Configuration is Not Created Successfully..!";
                    lblMessageDealerBinLocationConfig.Visible = true;
                    MPE_DealerBinLocationConfigCreate.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageDealerBinLocationConfig.ForeColor = Color.Red;
                lblMessageDealerBinLocationConfig.Text = ex.Message.ToString();
                lblMessageDealerBinLocationConfig.Visible = true;
                MPE_DealerBinLocationConfigCreate.Show();
                return;
            }
        }
        public Boolean ValidationConfig()
        {
            Boolean Result = false;
            if (ddlCDealerConfig.SelectedValue == "0")
            {
                lblMessageDealerBinLocationConfig.ForeColor = Color.Red;
                lblMessageDealerBinLocationConfig.Text = "Please Select Dealer..!";
                lblMessageDealerBinLocationConfig.Visible = true;
                Result = true;
            }
            if (ddlCDealerOfficeConfig.SelectedValue == "0")
            {
                lblMessageDealerBinLocationConfig.ForeColor = Color.Red;
                lblMessageDealerBinLocationConfig.Text = "Please Select Office..!";
                lblMessageDealerBinLocationConfig.Visible = true;
                Result = true;
            }
            if (ddlCDealerBinConfig.SelectedValue == "0")
            {
                lblMessageDealerBinLocationConfig.ForeColor = Color.Red;
                lblMessageDealerBinLocationConfig.Text = "Please Select Bin..!";
                lblMessageDealerBinLocationConfig.Visible = true;
                Result = true;
            }
            string Material = txtMaterial.Text.Trim();
            Material = Material.Split(' ')[0];
            PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, Material, null, null, null)[0];
            if (MM == null)
            {
                lblMessageDealerBinLocationConfig.ForeColor = Color.Red;
                lblMessageDealerBinLocationConfig.Text = "Please Select Valid Material..!";
                lblMessageDealerBinLocationConfig.Visible = true;
                Result = true;
            }
            return Result;
        }
        [WebMethod]
        public static List<string> SearchSMaterial(string input)
        {
            List<string> Materials = new BDMS_Material().GetMaterialAutocomplete(input, "", null);
            return Materials.FindAll(item => item.ToLower().Contains(input.ToLower()));
        }
        protected void lnkEditDealerBinLocationConfig_Click(object sender, EventArgs e)
        {
            LinkButton lnkEditDealerBinLocationConfig = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(lnkEditDealerBinLocationConfig.NamingContainer);
            Label lblDealerID = (Label)row.FindControl("lblDealerID");
            Label lblOfficeCodeID = (Label)row.FindControl("lblOfficeCodeID");
            Label lblDealerBinLocationID = (Label)row.FindControl("lblDealerBinLocationID");
            Label lblMaterialDesc = (Label)row.FindControl("lblMaterialDesc");
            Label lblMaterialCode = (Label)row.FindControl("lblMaterialCode");
            Label lblDealerBinLocationMaterialMappingID = (Label)row.FindControl("lblDealerBinLocationMaterialMappingID");
            ddlCDealerConfig.SelectedValue = lblDealerID.Text;
            ddlCDealerConfig.Enabled = false;
            new DDLBind(ddlCDealerOfficeConfig, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(lblDealerID.Text), null, null), "OfficeName", "OfficeID");
            ddlCDealerOfficeConfig.SelectedValue = lblOfficeCodeID.Text;
            ddlCDealerOfficeConfig.Enabled = false;
            new DDLBind(ddlCDealerBinConfig, new BDMS_Dealer().GetDealerBin(Convert.ToInt32(lblDealerID.Text), Convert.ToInt32(lblOfficeCodeID.Text)), "BinName", "DealerBinLocationID");
            ddlCDealerBinConfig.SelectedValue = lblDealerBinLocationID.Text;
            ddlCDealerBinConfig.Enabled = false;
            txtMaterial.Text = lblMaterialCode.Text + " " + lblMaterialDesc.Text;
            HidDealerBinLocationMaterialMappingID.Value = lblDealerBinLocationMaterialMappingID.Text;
            btnSaveConfig.Text = "Update";
            lblMessageDealerBinLocationConfig.Text = "";
            lblMessage.Text = "";
            MPE_DealerBinLocationConfigCreate.Show();
        }
        protected void lnkDeleteDealerBinLocationConfig_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            LinkButton lnkDeleteDealerBinLocationConfig = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(lnkDeleteDealerBinLocationConfig.NamingContainer);
            Label lblDealerID = (Label)row.FindControl("lblDealerID");
            Label lblOfficeCodeID = (Label)row.FindControl("lblOfficeCodeID");
            Label lblDealerBinLocationID = (Label)row.FindControl("lblDealerBinLocationID");
            Label lblMaterialID = (Label)row.FindControl("lblMaterialID");
            Label lblDealerBinLocationMaterialMappingID = (Label)row.FindControl("lblDealerBinLocationMaterialMappingID");
            HidDealerBinLocationMaterialMappingID.Value = lblDealerBinLocationMaterialMappingID.Text;

            PDealerBinLocation pDealerBin = new PDealerBinLocation();
            pDealerBin.DealerBinLocationMaterialMappingID = Convert.ToInt32(HidDealerBinLocationMaterialMappingID.Value);
            pDealerBin.DealerBinLocationID = Convert.ToInt32(lblDealerBinLocationID.Text);
            pDealerBin.Dealer = new PDealer() { DealerID = Convert.ToInt32(lblDealerID.Text) };
            pDealerBin.DealerOffice = new PDMS_DealerOffice() { OfficeID = Convert.ToInt32(lblOfficeCodeID.Text) };
            pDealerBin.Material = new PDMS_Material() { MaterialID = Convert.ToInt32(lblMaterialID.Text) };

            Boolean success = new BDMS_Dealer().InsertOrUpdateDealerBinLocationMaterialMapping(pDealerBin, false, PSession.User.UserID);
            if (success)
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Bin Location Configuration is Deleted Successfully..";
                lblMessage.Visible = true;
                fillDealerBinLocationConfig();
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Bin Location Configuration is Not Created Successfully..!";
                lblMessage.Visible = true;
                return;
            }
        }
        protected void ddlCDealerOfficeConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CrDealerConfigID = (ddlCDealerConfig.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlCDealerConfig.SelectedValue);
            int? CrCDealerOfficeConfigID = (ddlCDealerOfficeConfig.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlCDealerOfficeConfig.SelectedValue);
            new DDLBind(ddlCDealerBinConfig, new BDMS_Dealer().GetDealerBin(CrDealerConfigID, CrCDealerOfficeConfigID), "BinName", "DealerBinLocationID");
            MPE_DealerBinLocationConfigCreate.Show();
        }
    }
}