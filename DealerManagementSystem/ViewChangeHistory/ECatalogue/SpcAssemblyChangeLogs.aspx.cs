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

namespace DealerManagementSystem.ViewChangeHistory.ECatalogue
{
    public partial class SpcAssemblyChangeLogs : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewChangeHistory_SpcAssemblyChangeLogs; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Report » SpcAssembly Change Logs');</script>");
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                try
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                    imgBtnExportExcel.Visible = false;
                    txtDateFrom.Text = DateTime.Now.AddDays(1 + (-1 * DateTime.Now.Day)).ToString("yyyy-MM-dd");
                    txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    new BECatalogue().FillDivision(ddlDivision);
                    new DDLBind(ddlProductModel, new BECatalogue().GetSpcModel(null, null, null, true, null), "SpcModel", "SpcModelID", true, "Select Model");
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillSpcAssemblyChangeLogs();
            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void fillSpcAssemblyChangeLogs()
        {
            try
            {
                string AssemblyCode = null;
                AssemblyCode = string.IsNullOrEmpty(txtAssemblyCode.Text) ? null : txtAssemblyCode.Text.Trim();
                int? DivisionID = null, ProductModelID = null;
                DivisionID = (ddlDivision.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
                ProductModelID = (ddlProductModel.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlProductModel.SelectedValue);
                PApiResult Result = new BChangeLogs().GetSpcAssemblyChangeLogs(AssemblyCode, DivisionID, ProductModelID, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false, PageIndex, gvSpcAssemblyLogs.PageSize);
                DataSet GetSpcAssemblyChangeLogs = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));

                int RowCount = Result.RowCount;

                GetSpcAssemblyChangeLogs.Tables[0].Columns.Remove(GetSpcAssemblyChangeLogs.Tables[0].Columns["RowCount"]);

                gvSpcAssemblyLogs.DataSource = GetSpcAssemblyChangeLogs.Tables[0];
                gvSpcAssemblyLogs.DataBind();

                if (RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                    imgBtnExportExcel.Visible = false;
                }
                else
                {
                    PageCount = (RowCount + gvSpcAssemblyLogs.PageSize - 1) / gvSpcAssemblyLogs.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    imgBtnExportExcel.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvSpcAssemblyLogs.PageSize) + 1) + " - " + (((PageIndex - 1) * gvSpcAssemblyLogs.PageSize) + gvSpcAssemblyLogs.Rows.Count) + " of " + RowCount;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillSpcAssemblyChangeLogs();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillSpcAssemblyChangeLogs();
            }
        }
        protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string AssemblyCode = null;
                AssemblyCode = string.IsNullOrEmpty(txtAssemblyCode.Text) ? null : txtAssemblyCode.Text.Trim();
                int? DivisionID = null, ProductModelID = null;
                DivisionID = (ddlDivision.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
                ProductModelID = (ddlProductModel.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlProductModel.SelectedValue);
                PApiResult Result = new BChangeLogs().GetSpcAssemblyChangeLogs(AssemblyCode, DivisionID, ProductModelID, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
                DataSet GetSpcAssemblyChangeLogs = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));

                new BXcel().ExporttoExcel(GetSpcAssemblyChangeLogs.Tables[0], "Assembly Change Logs Report");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlProductModel, new BECatalogue().GetSpcModel(null, Convert.ToInt32(ddlDivision.SelectedValue), null, true, null), "SpcModel", "SpcModelID", true, "Select Model");
        }
    }
}