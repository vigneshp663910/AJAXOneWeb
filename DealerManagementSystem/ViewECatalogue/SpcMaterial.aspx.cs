using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewECatalogue
{
    public partial class SpcMaterial : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewECatalogue_SpcMaterial; } }
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
            lblMessage.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('E Catalogue » Spc Material');</script>");
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                try
                {
                    FillModel();
                    
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
            }
        }
        void FillModel()
        {
            PApiResult Result = new BECatalogue().GetSpcMaterial(txtMaterial.Text.Trim(), PageIndex, gvModel.PageSize);
            gvModel.PageIndex = 0;
            gvModel.DataSource = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            gvModel.DataBind();

            if (Result.RowCount == 0)
            {
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
            else
            {
                PageCount = (Result.RowCount + gvModel.PageSize - 1) / gvModel.PageSize;
                lblRowCount.Visible = true;
                ibtnArrowLeft.Visible = true;
                ibtnArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvModel.PageSize) + 1) + " - " + (((PageIndex - 1) * gvModel.PageSize) + gvModel.Rows.Count) + " of " + Result.RowCount;
            } 
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillModel();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillModel();
            }
        } 
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            FillModel();
        }        
        protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
        {

            PApiResult Result = new BECatalogue().GetSpcMaterial(txtMaterial.Text.Trim(), null, null);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            new BXcel().ExporttoExcel(dt, "Model List");
        }
    }
}