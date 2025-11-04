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
    public partial class SpcAssemblyPartsReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewECatalogue_SpcAssemblyPartsReport; } }
        int? SpcProductGroupID = null;
        int? SpcModelID = null;
        int? SpcAssemblyID = null;
        string Material = null;
         
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('E Catalogue » Assembly Parts Report');</script>");
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                
                new DDLBind(ddlProductGroup, new BECatalogue().GetSpcProductGroup(null, null, true), "PGSCodePGDescription", "SpcProductGroupID", true, " All");
                ddlProductGroup_SelectedIndexChanged(null, null);

                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
       
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillPartsReport();
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillPartsReport();
            }
        }
         
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillPartsReport();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red; 
            }
        }
        void fillPartsReport()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BECatalogue().GetAssemblyPartsReport(SpcProductGroupID, SpcModelID, SpcAssemblyID, Material, 0, PageIndex, gvSOInvoice.PageSize);
                DataTable SalesOrderInvoiceReport = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));

                gvSOInvoice.PageIndex = 0;
                gvSOInvoice.DataSource = SalesOrderInvoiceReport;
                gvSOInvoice.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvSOInvoice.PageSize - 1) / gvSOInvoice.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvSOInvoice.PageSize) + 1) + " - " + (((PageIndex - 1) * gvSOInvoice.PageSize) + gvSOInvoice.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("SpcAssemblyPartsReport", "fillPartsReport", e1);
                throw e1;
            }
        }
        void Search()
        {
            
            SpcProductGroupID = ddlProductGroup.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductGroup.SelectedValue);
            SpcModelID = ddlModel.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlModel.SelectedValue);
            SpcAssemblyID = ddlAssembly.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlAssembly.SelectedValue);
            Material = string.IsNullOrEmpty(txtMaterial.Text.Trim()) ? null : txtMaterial.Text.Trim();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Search();
            PApiResult Result = new BECatalogue().GetAssemblyPartsReport(SpcProductGroupID, SpcModelID, SpcAssemblyID, Material, 1, null, null);
            DataTable Report = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            new BXcel().ExporttoExcel(Report, "Assembly Parts List");
        }
        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpcProductGroupID = ddlProductGroup.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductGroup.SelectedValue);
            SpcModelID = ddlModel.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlModel.SelectedValue);
            PApiResult Result = new BECatalogue().GetSpcAssembly(SpcProductGroupID, SpcModelID, null, null, true, 0, null, null);
            List<PSpcAssembly> Assembly = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data));
            new DDLBind(ddlAssembly, Assembly, "AssemblyDescription", "SpcAssemblyID", true, "All");

        }
         
        protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpcProductGroupID = ddlProductGroup.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductGroup.SelectedValue);
            new DDLBind(ddlModel, new BECatalogue().GetSpcModel(null, SpcProductGroupID, null, true, null), "SpcModelCodeWithDescription", "SpcModelID", true, "All");
            ddlModel_SelectedIndexChanged(null, null);
        }
    }
}