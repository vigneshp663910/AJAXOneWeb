using Business;
using ClosedXML.Excel;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
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
        public List<PDealerBinLocation_Insert> InsertBinLocationUpload
        {
            get
            {
                if (ViewState["BinLocationUpload"] == null)
                {
                    ViewState["BinLocationUpload"] = new List<PDealerBinLocation_Insert>();
                }
                return (List<PDealerBinLocation_Insert>)ViewState["BinLocationUpload"];
            }
            set
            {
                ViewState["BinLocationUpload"] = value;
            }
        }
        public List<PDealerBinLocation_Insert> InsertBinLocationConfigUpload
        {
            get
            {
                if (ViewState["BinLocationConfigUpload"] == null)
                {
                    ViewState["BinLocationConfigUpload"] = new List<PDealerBinLocation_Insert>();
                }
                return (List<PDealerBinLocation_Insert>)ViewState["BinLocationConfigUpload"];
            }
            set
            {
                ViewState["BinLocationConfigUpload"] = value;
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
            lblMessage.Text = "";

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                try
                {
                    LoadDropdown();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                    fillDealerBinLocation();
                    fillDealerBinLocationConfig();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                }
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
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

                PApiResult Result = new BDMS_Dealer().GetDealerBinLocation(DealerID, OfficeCodeID, PageIndex, gvDealerBinLocation.PageSize);
                PDealerBinLocationHeader = JsonConvert.DeserializeObject<List<PDealerBinLocation>>(JsonConvert.SerializeObject(Result.Data));
                RowCount = Result.RowCount;

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
                PDealerBinLocation_Insert pDealerBin = new PDealerBinLocation_Insert();
                if (btnSave.Text == "Update")
                {
                    pDealerBin.DealerBinLocationID = Convert.ToInt32(HidDealerBinLocationID.Value);
                }
                pDealerBin.BinName = txtBinName.Text.Trim();
                pDealerBin.DealerID = Convert.ToInt32(ddlCDealerCode.SelectedValue);
                pDealerBin.OfficeID = Convert.ToInt32(ddlCOfficeName.SelectedValue);
                pDealerBin.IsActive = true;
                List<PDealerBinLocation_Insert> BinLst = new List<PDealerBinLocation_Insert>();
                BinLst.Add(pDealerBin);
                PApiResult Results = InsertOrUpdateDealerBinLocation(BinLst);
                //PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Dealer/InsertOrUpdateDealerBinLocation", BinLst));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessageDealerBinLocation.ForeColor = Color.Red;
                    lblMessageDealerBinLocation.Text = "Bin Location is Not Created Successfully.";
                    MPE_DealerBinLocationCreate.Show();
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Bin Location is Created Successfully.";
                fillDealerBinLocation();
            }
            catch (Exception ex)
            {
                lblMessageDealerBinLocation.ForeColor = Color.Red;
                lblMessageDealerBinLocation.Text = ex.Message.ToString();
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
                Result = true;
            }
            if (ddlCOfficeName.SelectedValue == "0")
            {
                lblMessageDealerBinLocation.ForeColor = Color.Red;
                lblMessageDealerBinLocation.Text = "Please Select Office..!";
                Result = true;
            }
            if (string.IsNullOrEmpty(txtBinName.Text))
            {
                lblMessageDealerBinLocation.ForeColor = Color.Red;
                lblMessageDealerBinLocation.Text = "Please Enter Bin Name...!";
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
            try
            {
                LinkButton lnkDeleteDealerBinLocation = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkDeleteDealerBinLocation.NamingContainer);
                Label lblBinName = (Label)row.FindControl("lblBinName");
                Label lblDealerID = (Label)row.FindControl("lblDealerID");
                Label lblOfficeCodeID = (Label)row.FindControl("lblOfficeCodeID");
                Label lblDealerBinLocationID = (Label)row.FindControl("lblDealerBinLocationID");
                HidDealerBinLocationID.Value = lblDealerBinLocationID.Text;

                PDealerBinLocation_Insert pDealerBin = new PDealerBinLocation_Insert();
                pDealerBin.DealerBinLocationID = Convert.ToInt32(HidDealerBinLocationID.Value);
                pDealerBin.BinName = lblBinName.Text;
                pDealerBin.DealerID = Convert.ToInt32(lblDealerID.Text);
                pDealerBin.OfficeID = Convert.ToInt32(lblOfficeCodeID.Text);
                pDealerBin.IsActive = false;

                if (PDealerBinLocationConfigHeader.Any(item => item.DealerBinLocationID == pDealerBin.DealerBinLocationID))
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Bin Location Reference is Available in Material Mapping...!";
                    return;
                }
                List<PDealerBinLocation_Insert> BinLst = new List<PDealerBinLocation_Insert>();
                BinLst.Add(pDealerBin);
                PApiResult Results = InsertOrUpdateDealerBinLocation(BinLst);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Bin Location is Not Deleted Successfully.";
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Bin Location is Deleted Successfully.";
                fillDealerBinLocation();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
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
            }
        }
        void fillDealerBinLocationConfig()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                CSearch();
                int RowCount = 0;

                PApiResult Result = new BDMS_Dealer().GetDealerBinLocationMaterialMappingHeader(CDealerID, COfficeCodeID, CDealerBinLocationID, MaterialCode, PageIndex, gvDealerBinLocation.PageSize);
                PDealerBinLocationConfigHeader = JsonConvert.DeserializeObject<List<PDealerBinLocation>>(JsonConvert.SerializeObject(Result.Data));
                RowCount = Result.RowCount;

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
            lblMessageDealerBinLocationConfig.Text = "";
            try
            {
                Boolean Result = ValidationConfig();
                if (Result)
                {
                    MPE_DealerBinLocationConfigCreate.Show();
                    return;
                }
                PDealerBinLocation_Insert pDealerBin = new PDealerBinLocation_Insert();
                if (btnSaveConfig.Text == "Update")
                {
                    pDealerBin.DealerBinLocationMaterialMappingID = Convert.ToInt32(HidDealerBinLocationMaterialMappingID.Value);
                }
                pDealerBin.DealerBinLocationID = Convert.ToInt32(ddlCDealerBinConfig.SelectedValue);
                pDealerBin.DealerID = Convert.ToInt32(ddlCDealerConfig.SelectedValue);
                pDealerBin.OfficeID = Convert.ToInt32(ddlCDealerOfficeConfig.SelectedValue);
                string Material = txtMaterial.Text.Trim();
                Material = Material.Split(' ')[0];
                PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, Material, null, null, null)[0];
                pDealerBin.MaterialID = MM.MaterialID;
                pDealerBin.IsActive = true;
                List<PDealerBinLocation_Insert> BinLst = new List<PDealerBinLocation_Insert>();
                BinLst.Add(pDealerBin);
                PApiResult Results = InsertOrUpdateDealerBinLocationMaterialMapping(BinLst);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessageDealerBinLocationConfig.ForeColor = Color.Red;
                    lblMessageDealerBinLocationConfig.Text = "Bin Location Configuration is Not Created Successfully.";
                    MPE_DealerBinLocationConfigCreate.Show();
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Bin Location Configuration is Created Successfully.";
                fillDealerBinLocationConfig();
            }
            catch (Exception ex)
            {
                lblMessageDealerBinLocationConfig.ForeColor = Color.Red;
                lblMessageDealerBinLocationConfig.Text = ex.Message.ToString();
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
                Result = true;
            }
            if (ddlCDealerOfficeConfig.SelectedValue == "0")
            {
                lblMessageDealerBinLocationConfig.ForeColor = Color.Red;
                lblMessageDealerBinLocationConfig.Text = "Please Select Office..!";
                Result = true;
            }
            if (ddlCDealerBinConfig.SelectedValue == "0")
            {
                lblMessageDealerBinLocationConfig.ForeColor = Color.Red;
                lblMessageDealerBinLocationConfig.Text = "Please Select Bin..!";
                Result = true;
            }
            string Material = txtMaterial.Text.Trim();
            Material = Material.Split(' ')[0];
            PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, Material, null, null, null)[0];
            if (MM == null)
            {
                lblMessageDealerBinLocationConfig.ForeColor = Color.Red;
                lblMessageDealerBinLocationConfig.Text = "Please Select Valid Material..!";
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
            lblMessage.ForeColor = Color.Red;
            try
            {
                LinkButton lnkDeleteDealerBinLocationConfig = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkDeleteDealerBinLocationConfig.NamingContainer);
                Label lblDealerID = (Label)row.FindControl("lblDealerID");
                Label lblOfficeCodeID = (Label)row.FindControl("lblOfficeCodeID");
                Label lblDealerBinLocationID = (Label)row.FindControl("lblDealerBinLocationID");
                Label lblMaterialID = (Label)row.FindControl("lblMaterialID");
                Label lblDealerBinLocationMaterialMappingID = (Label)row.FindControl("lblDealerBinLocationMaterialMappingID");
                HidDealerBinLocationMaterialMappingID.Value = lblDealerBinLocationMaterialMappingID.Text;

                PDealerBinLocation_Insert pDealerBin = new PDealerBinLocation_Insert();
                pDealerBin.DealerBinLocationMaterialMappingID = Convert.ToInt32(HidDealerBinLocationMaterialMappingID.Value);
                pDealerBin.DealerBinLocationID = Convert.ToInt32(lblDealerBinLocationID.Text);
                pDealerBin.DealerID = Convert.ToInt32(lblDealerID.Text);
                pDealerBin.OfficeID = Convert.ToInt32(lblOfficeCodeID.Text);
                pDealerBin.MaterialID = Convert.ToInt32(lblMaterialID.Text);
                pDealerBin.IsActive = false;

                List<PDealerBinLocation_Insert> BinLst = new List<PDealerBinLocation_Insert>();
                BinLst.Add(pDealerBin);
                PApiResult Results = InsertOrUpdateDealerBinLocationMaterialMapping(BinLst);
                //PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Dealer/InsertOrUpdateDealerBinLocationMaterialMapping", pDealerBin));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Bin Location Configuration is Not Deleted Successfully.";
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Bin Location Configuration is Deleted Successfully.";
                fillDealerBinLocationConfig();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ddlCDealerOfficeConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CrDealerConfigID = (ddlCDealerConfig.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlCDealerConfig.SelectedValue);
            int? CrCDealerOfficeConfigID = (ddlCDealerOfficeConfig.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlCDealerOfficeConfig.SelectedValue);
            new DDLBind(ddlCDealerBinConfig, new BDMS_Dealer().GetDealerBin(CrDealerConfigID, CrCDealerOfficeConfigID), "BinName", "DealerBinLocationID");
            MPE_DealerBinLocationConfigCreate.Show();
        }
        protected void BtnUploadBinLocation_Click(object sender, EventArgs e)
        {
            ddlBinLocationDealer.DataTextField = "CodeWithName";
            ddlBinLocationDealer.DataValueField = "DID";
            ddlBinLocationDealer.DataSource = PSession.User.Dealer;
            ddlBinLocationDealer.DataBind();
            ddlBinLocationDealer.Items.Insert(0, new ListItem("Select", "0"));
            new DDLBind(ddlBinLocationOffice, new BDMS_Dealer().GetDealerOffice(0, null, null), "OfficeName", "OfficeID");
            GVUploadBinLocation.DataSource = null;
            GVUploadBinLocation.DataBind();
            BtnSaveBinLocation.Visible = false;
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerBinLocationCreateUpdateDeleteUpload).Count() > 0)
            {
                btnViewBinLocation.Visible = true;
            }
            MPE_DealerBinLocationUpload.Show();
        }
        protected void btnDownloadBinLocation_Click(object sender, EventArgs e)
        {
            string Path = Server.MapPath("~/Templates\\BinLocation.xlsx");
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"BinLocation.xlsx\"");
            byte[] data = req.DownloadData(Path);
            response.BinaryWrite(data);
            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end
            response.End();
        }

        protected void btnViewBinLocation_Click(object sender, EventArgs e)
        {
            lblMsg_DealerBinLocationUpload.ForeColor = Color.Red;
            try
            {
                if (ddlBinLocationDealer.SelectedValue == "0")
                {
                    lblMsg_DealerBinLocationUpload.Text = "Please select Dealer.";
                    lblMsg_DealerBinLocationUpload.ForeColor = Color.Red;
                    MPE_DealerBinLocationUpload.Show();
                    return;
                }
                if (ddlBinLocationOffice.SelectedValue == "0")
                {
                    lblMsg_DealerBinLocationUpload.Text = "Please select Office.";
                    lblMsg_DealerBinLocationUpload.ForeColor = Color.Red;
                    MPE_DealerBinLocationUpload.Show();
                    return;
                }

                List<PDealerBinLocation> BinLocationUpload = new List<PDealerBinLocation>();
                GVUploadBinLocation.DataSource = BinLocationUpload;
                GVUploadBinLocation.DataBind();
                if (IsPostBack && fileUploadBinLocation.PostedFile != null)
                {
                    if (fileUploadBinLocation.PostedFile.FileName.Length > 0)
                    {
                        FillUploadBinLocation();
                    }
                }
                MPE_DealerBinLocationUpload.Show();
            }
            catch (Exception ex)
            {
                lblMsg_DealerBinLocationUpload.Text = ex.Message.ToString();
                MPE_DealerBinLocationUpload.Show();
                return;
            }
        }

        protected void BtnSaveBinLocation_Click(object sender, EventArgs e)
        {
            lblMsg_DealerBinLocationUpload.ForeColor = Color.Red;
            try
            {
                PApiResult Results = InsertOrUpdateDealerBinLocation(InsertBinLocationUpload);
                if (Results.Status == PApplication.Failure)
                {
                    lblMsg_DealerBinLocationUpload.ForeColor = Color.Red;
                    lblMsg_DealerBinLocationUpload.Text = "Bin Location is Not Created Successfully.";
                    MPE_DealerBinLocationUpload.Show();
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                fillDealerBinLocation();
                BtnSaveBinLocation.Visible = false;
            }
            catch (Exception ex)
            {
                lblMsg_DealerBinLocationUpload.Text = ex.Message.ToString();
                MPE_DealerBinLocationUpload.Show();
                return;
            }
        }
        private Boolean FillUploadBinLocation()
        {
            BtnSaveBinLocation.Visible = false;
            Boolean Success = true;
            if (fileUploadBinLocation.HasFile == true)
            {
                using (XLWorkbook workBook = new XLWorkbook(fileUploadBinLocation.PostedFile.InputStream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    List<PDealerBinLocation> BinLocationUpload = new List<PDealerBinLocation>();

                    //Loop through the Worksheet rows.
                    int sno = 0;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        sno += 1;
                        if (sno > 1)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                if (!string.IsNullOrEmpty(cell.Value.ToString()))
                                {
                                    PDealerBinLocation db = new PDealerBinLocation();
                                    db.Dealer = new PDealer { DealerID = Convert.ToInt32(ddlBinLocationDealer.SelectedValue), DealerName = ddlBinLocationDealer.SelectedItem.Text };
                                    db.DealerOffice = new PDMS_DealerOffice { OfficeID = Convert.ToInt32(ddlBinLocationOffice.SelectedValue), OfficeName = ddlBinLocationOffice.SelectedItem.Text };
                                    db.BinName = cell.Value.ToString().Trim();
                                    BinLocationUpload.Add(db);
                                }
                            }
                        }
                    }

                    List<PDealerBinLocation> BinLocationUploadIsExist = JsonConvert.DeserializeObject<List<PDealerBinLocation>>(JsonConvert.SerializeObject(new BDMS_Dealer().GetDealerBinLocation(Convert.ToInt32(ddlBinLocationDealer.SelectedValue), Convert.ToInt32(ddlBinLocationOffice.SelectedValue), null, null).Data));
                    InsertBinLocationUpload = new List<PDealerBinLocation_Insert>();
                    foreach (PDealerBinLocation bin in BinLocationUploadIsExist)
                    {
                        bool containsItem = BinLocationUploadIsExist.Any(item => item.BinName == bin.BinName);
                        if (containsItem)
                        {
                            lblMsg_DealerBinLocationUpload.Text = "Bin Location already available : " + bin.BinName + ".";
                            lblMsg_DealerBinLocationUpload.ForeColor = Color.Red;
                            Success = false;
                            return Success;
                        }
                        PDealerBinLocation_Insert Bin = new PDealerBinLocation_Insert();
                        Bin.DealerBinLocationID = bin.DealerBinLocationID;
                        Bin.BinName = bin.BinName;
                        Bin.OfficeID = bin.DealerOffice.OfficeID;
                        Bin.IsActive = true;
                        InsertBinLocationUpload.Add(Bin);
                    }
                    if (BinLocationUpload.Count > 0)
                    {
                        GVUploadBinLocation.DataSource = BinLocationUpload;
                        GVUploadBinLocation.DataBind();
                        List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                        if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerBinLocationCreateUpdateDeleteUpload).Count() > 0)
                        {
                            BtnSaveBinLocation.Visible = true;
                        }
                        btnViewBinLocation.Visible = false;
                    }
                }
            }
            else
            {
                lblMessage.Text = "Please Upload the File...!";
                lblMessage.ForeColor = Color.Red;
                Success = false;
                return Success;
            }
            return Success;
        }

        protected void ddlBinLocationDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlBinLocationOffice, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlBinLocationDealer.SelectedValue), null, null), "OfficeName", "OfficeID");
            MPE_DealerBinLocationUpload.Show();
        }
        protected void BtnUploadBinLocationConfig_Click(object sender, EventArgs e)
        {
            ddlBinLocationConfigDealer.DataTextField = "CodeWithName";
            ddlBinLocationConfigDealer.DataValueField = "DID";
            ddlBinLocationConfigDealer.DataSource = PSession.User.Dealer;
            ddlBinLocationConfigDealer.DataBind();
            ddlBinLocationConfigDealer.Items.Insert(0, new ListItem("Select", "0"));
            new DDLBind(ddlBinLocationConfigOffice, new BDMS_Dealer().GetDealerOffice(0, null, null), "OfficeName", "OfficeID");
            GVUploadBinLocationConfig.DataSource = null;
            GVUploadBinLocationConfig.DataBind();
            BtnSaveBinLocationConfig.Visible = false;
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerBinLocationCreateUpdateDeleteUpload).Count() > 0)
            {
                btnViewBinLocationConfig.Visible = true;
            }
            MPE_DealerBinLocationConfigUpload.Show();
        }
        protected void btnDownloadBinLocationConfig_Click(object sender, EventArgs e)
        {
            string Path = Server.MapPath("~/Templates\\BinLocationMaterialConfig.xlsx");
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"BinLocationMaterialConfig.xlsx\"");
            byte[] data = req.DownloadData(Path);
            response.BinaryWrite(data);
            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end
            response.End();
        }
        protected void ddlBinLocationConfigDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlBinLocationConfigOffice, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlBinLocationConfigDealer.SelectedValue), null, null), "OfficeName", "OfficeID");
            MPE_DealerBinLocationConfigUpload.Show();
        }
        protected void btnViewBinLocationConfig_Click(object sender, EventArgs e)
        {
            if (ddlBinLocationConfigDealer.SelectedValue == "0")
            {
                lblMsg_DealerBinLocationConfigUpload.Text = "Please select Dealer.";
                lblMsg_DealerBinLocationConfigUpload.ForeColor = Color.Red;
                MPE_DealerBinLocationConfigUpload.Show();
                return;
            }
            if (ddlBinLocationConfigOffice.SelectedValue == "0")
            {
                lblMsg_DealerBinLocationConfigUpload.Text = "Please select Office.";
                lblMsg_DealerBinLocationConfigUpload.ForeColor = Color.Red;
                MPE_DealerBinLocationConfigUpload.Show();
                return;
            }

            List<PDealerBinLocation> BinLocationConfigUpload = new List<PDealerBinLocation>();
            GVUploadBinLocation.DataSource = BinLocationConfigUpload;
            GVUploadBinLocation.DataBind();
            if (IsPostBack && fileUploadBinLocationConfig.PostedFile != null)
            {
                if (fileUploadBinLocationConfig.PostedFile.FileName.Length > 0)
                {
                    FillUploadBinLocationConfig();
                }
            }
            MPE_DealerBinLocationConfigUpload.Show();
        }
        private Boolean FillUploadBinLocationConfig()
        {
            BtnSaveBinLocationConfig.Visible = false;
            Boolean Success = true;
            if (fileUploadBinLocationConfig.HasFile == true)
            {
                using (XLWorkbook workBook = new XLWorkbook(fileUploadBinLocationConfig.PostedFile.InputStream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    List<PDealerBinLocation> BinLocationConfigUpload = new List<PDealerBinLocation>();

                    //Loop through the Worksheet rows.
                    int sno = 0;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        sno += 1;
                        if (sno > 1)
                        {
                            PDealerBinLocation db = new PDealerBinLocation();
                            db.Dealer = new PDealer { DealerID = Convert.ToInt32(ddlBinLocationConfigDealer.SelectedValue), DealerName = ddlBinLocationConfigDealer.SelectedItem.Text };
                            db.DealerOffice = new PDMS_DealerOffice { OfficeID = Convert.ToInt32(ddlBinLocationConfigOffice.SelectedValue), OfficeName = ddlBinLocationConfigOffice.SelectedItem.Text };
                            int i = 0;
                            foreach (IXLCell cell in row.Cells())
                            {
                                if (!string.IsNullOrEmpty(cell.Value.ToString().Trim()))
                                {
                                    if (i == 0)
                                    {
                                        List<PDealerBinLocation> BinLocationConfigUploadIsExist = JsonConvert.DeserializeObject<List<PDealerBinLocation>>(JsonConvert.SerializeObject(new BDMS_Dealer().GetDealerBinLocation(Convert.ToInt32(ddlBinLocationConfigDealer.SelectedValue), Convert.ToInt32(ddlBinLocationConfigOffice.SelectedValue), null, null).Data));
                                        bool containsItem = BinLocationConfigUploadIsExist.Any(item => item.BinName == cell.Value.ToString().Trim() && item.Dealer.DealerID == Convert.ToInt32(ddlBinLocationConfigDealer.SelectedValue) && item.DealerOffice.OfficeID == Convert.ToInt32(ddlBinLocationConfigOffice.SelectedValue));
                                        if (containsItem)
                                        {
                                            foreach (PDealerBinLocation BinLocation in BinLocationConfigUploadIsExist)
                                            {
                                                if (BinLocation.BinName == cell.Value.ToString().Trim())
                                                {
                                                    db.DealerBinLocationID = BinLocation.DealerBinLocationID;
                                                    db.BinName = cell.Value.ToString();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            lblMsg_DealerBinLocationConfigUpload.Text = "BinName not available : " + cell.Value.ToString().Trim() + ".";
                                            lblMsg_DealerBinLocationConfigUpload.ForeColor = Color.Red;
                                            Success = false;
                                            return Success;
                                        }

                                    }
                                    if (i == 1)
                                    {
                                        PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, cell.Value.ToString().Trim(), null, null, null)[0];
                                        db.Material = new PDMS_Material() { MaterialID = MM.MaterialID, MaterialCode = cell.Value.ToString().Trim() };
                                    }
                                    i++;
                                }
                            }
                            if (db.BinName != null)
                                BinLocationConfigUpload.Add(db);
                        }
                    }


                    List<PDealerBinLocation> BinLocationUploadIsExist = JsonConvert.DeserializeObject<List<PDealerBinLocation>>(JsonConvert.SerializeObject(new BDMS_Dealer().GetDealerBinLocationMaterialMappingHeader(Convert.ToInt32(ddlBinLocationConfigDealer.SelectedValue), Convert.ToInt32(ddlBinLocationConfigOffice.SelectedValue), null, null, null, null).Data));
                    InsertBinLocationConfigUpload = new List<PDealerBinLocation_Insert>();
                    foreach (PDealerBinLocation bin in BinLocationConfigUpload)
                    {
                        if (bin.DealerBinLocationID == 0)
                        {
                            lblMsg_DealerBinLocationConfigUpload.Text = "BinName not available : " + bin.BinName + ".";
                            lblMsg_DealerBinLocationConfigUpload.ForeColor = Color.Red;
                            Success = false;
                            return Success;
                        }
                        bool containsItem = BinLocationUploadIsExist.Any(item => item.Material.MaterialID == bin.Material.MaterialID && item.DealerBinLocationID == bin.DealerBinLocationID && item.Dealer.DealerID == bin.Dealer.DealerID && item.DealerOffice.OfficeID == bin.DealerOffice.OfficeID);
                        if (containsItem)
                        {
                            lblMsg_DealerBinLocationConfigUpload.Text = "Material already available : " + bin.Material.MaterialCode + ".";
                            lblMsg_DealerBinLocationConfigUpload.ForeColor = Color.Red;
                            Success = false;
                            return Success;
                        }
                        PDealerBinLocation_Insert Bin = new PDealerBinLocation_Insert();
                        Bin.DealerBinLocationMaterialMappingID = bin.DealerBinLocationMaterialMappingID;
                        Bin.DealerBinLocationID = bin.DealerBinLocationID;
                        Bin.OfficeID = bin.DealerOffice.OfficeID;
                        Bin.MaterialID = bin.Material.MaterialID;
                        Bin.IsActive = true;
                        InsertBinLocationConfigUpload.Add(Bin);
                    }
                    if (BinLocationConfigUpload.Count > 0)
                    {
                        GVUploadBinLocationConfig.DataSource = BinLocationConfigUpload;
                        GVUploadBinLocationConfig.DataBind();
                        List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                        if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerBinLocationCreateUpdateDeleteUpload).Count() > 0)
                        {
                            BtnSaveBinLocationConfig.Visible = true;
                        }
                        btnViewBinLocationConfig.Visible = false;
                    }
                }
            }
            else
            {
                lblMessage.Text = "Please Upload the File...!";
                lblMessage.ForeColor = Color.Red;
                Success = false;
                return Success;
            }
            return Success;
        }
        protected void BtnSaveBinLocationConfig_Click(object sender, EventArgs e)
        {
            lblMsg_DealerBinLocationConfigUpload.ForeColor = Color.Red;
            try
            {
                PApiResult Results = InsertOrUpdateDealerBinLocationMaterialMapping(InsertBinLocationConfigUpload);
                if (Results.Status == PApplication.Failure)
                {
                    lblMsg_DealerBinLocationConfigUpload.ForeColor = Color.Red;
                    lblMsg_DealerBinLocationConfigUpload.Text = "Bin Location Configuration is Not Created Successfully..!";
                    MPE_DealerBinLocationConfigUpload.Show();
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                fillDealerBinLocationConfig();
                BtnSaveBinLocationConfig.Visible = false;
            }
            catch (Exception ex)
            {
                lblMsg_DealerBinLocationConfigUpload.Text = ex.Message.ToString();
                MPE_DealerBinLocationConfigUpload.Show();
                return;
            }
        }
        protected void imgBtnExportExcelDealerBinLocation_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Search();
                PApiResult Result = new BDMS_Dealer().GetDealerBinLocation(DealerID, OfficeCodeID, null, null);
                List<PDealerBinLocation> l1 = JsonConvert.DeserializeObject<List<PDealerBinLocation>>(JsonConvert.SerializeObject(Result.Data));
                try
                {
                    DataTable BinLocation = new DataTable();
                    BinLocation.Columns.Add("Dealer Code");
                    BinLocation.Columns.Add("Dealer Name");
                    BinLocation.Columns.Add("Office Code");
                    BinLocation.Columns.Add("Office Name");
                    BinLocation.Columns.Add("Bin Name");
                    foreach (PDealerBinLocation bin in l1)
                    {
                        BinLocation.Rows.Add(bin.Dealer.DealerCode, bin.Dealer.DealerName, bin.DealerOffice.OfficeCode, bin.DealerOffice.OfficeName, bin.BinName);
                    }
                    new BXcel().ExporttoExcel(BinLocation, "Bin Location Report");
                }
                catch(Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void imgBtnExportExcelDealerBinLocationMatMapping_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CSearch();
                PApiResult Result = new BDMS_Dealer().GetDealerBinLocationMaterialMappingHeader(CDealerID, COfficeCodeID, CDealerBinLocationID, MaterialCode, null, null);
                List<PDealerBinLocation> l1 = JsonConvert.DeserializeObject<List<PDealerBinLocation>>(JsonConvert.SerializeObject(Result.Data));
                try
                {
                    DataTable BinLocation = new DataTable();
                    BinLocation.Columns.Add("Dealer Code");
                    BinLocation.Columns.Add("Dealer Name");
                    BinLocation.Columns.Add("Office Code");
                    BinLocation.Columns.Add("Office Name");
                    BinLocation.Columns.Add("Bin Name");
                    BinLocation.Columns.Add("Material Code");
                    BinLocation.Columns.Add("Material Name");
                    foreach (PDealerBinLocation bin in l1)
                    {
                        BinLocation.Rows.Add(bin.Dealer.DealerCode, bin.Dealer.DealerName, bin.DealerOffice.OfficeCode, bin.DealerOffice.OfficeName, bin.BinName, bin.Material.MaterialCode, bin.Material.MaterialDescription);
                    }
                    new BXcel().ExporttoExcel(BinLocation, "Bin Location Material Report");
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        PApiResult InsertOrUpdateDealerBinLocationMaterialMapping(List<PDealerBinLocation_Insert> InsertBinLocationConfigUpload_)
        {
          return   JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Dealer/InsertOrUpdateDealerBinLocationMaterialMapping", InsertBinLocationConfigUpload_));
        }
        PApiResult InsertOrUpdateDealerBinLocation(List<PDealerBinLocation_Insert> InsertBinLocationUpload_)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Dealer/InsertOrUpdateDealerBinLocation", InsertBinLocationUpload_));
        }
    }
}