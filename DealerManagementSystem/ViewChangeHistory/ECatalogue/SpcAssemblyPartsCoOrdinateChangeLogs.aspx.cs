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
    public partial class SpcAssemblyPartsCoOrdinateChangeLogs : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewChangeHistory_SpcAssemblyPartsCoOrdinateChangeLogs; } }
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Report » SpcAssembly Parts CoOrdinate Change Logs');</script>");
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                try
                {
                    txtDateFrom.Text = DateTime.Now.AddDays(1 + (-1 * DateTime.Now.Day)).ToString("yyyy-MM-dd");
                    txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    new BECatalogue().FillDivision(ddlDivision);
                    new DDLBind(ddlProductModel, new BECatalogue().GetSpcModel(null, null, null, true, null), "SpcModel", "SpcModelID", true, "Select Model");
                    new DDLBind(ddlAssembly, JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(new BECatalogue().GetSpcAssembly(null, null, null, null, true, 0).Data)), "AssemblyCode", "SpcAssemblyID", true, "Select Assembly");
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
                fillSpcAssemblyPartsCoOrdinateChangeLogs();
            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void fillSpcAssemblyPartsCoOrdinateChangeLogs()
        {
            try
            {
                string Material = null;
                Material = string.IsNullOrEmpty(txtMaterial.Text) ? null : txtMaterial.Text.Trim();
                int? DivisionID = null, ProductModelID = null, SpcAssemblyID = null;
                DivisionID = (ddlDivision.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
                ProductModelID = (ddlProductModel.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlProductModel.SelectedValue);
                SpcAssemblyID = (ddlAssembly.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlAssembly.SelectedValue);
                PApiResult Result = new BChangeLogs().GetSpcAssemblyPartsCoOrdinateChangeLogs(Material, DivisionID, ProductModelID, SpcAssemblyID, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false, PageIndex, gvSpcAssemblyPartsCoOrdinateChangeLogs.PageSize);
                DataSet GetSpcAssemblyPartsCoOrdinateChangeLogs = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));

                int RowCount = Result.RowCount;

                GetSpcAssemblyPartsCoOrdinateChangeLogs.Tables[0].Columns.Remove(GetSpcAssemblyPartsCoOrdinateChangeLogs.Tables[0].Columns["RowCount"]);

                gvSpcAssemblyPartsCoOrdinateChangeLogs.DataSource = GetSpcAssemblyPartsCoOrdinateChangeLogs.Tables[0];
                gvSpcAssemblyPartsCoOrdinateChangeLogs.DataBind();

                if (RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                    imgBtnExportExcel.Visible = false;
                }
                else
                {
                    PageCount = (RowCount + gvSpcAssemblyPartsCoOrdinateChangeLogs.PageSize - 1) / gvSpcAssemblyPartsCoOrdinateChangeLogs.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    imgBtnExportExcel.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvSpcAssemblyPartsCoOrdinateChangeLogs.PageSize) + 1) + " - " + (((PageIndex - 1) * gvSpcAssemblyPartsCoOrdinateChangeLogs.PageSize) + gvSpcAssemblyPartsCoOrdinateChangeLogs.Rows.Count) + " of " + RowCount;
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
                fillSpcAssemblyPartsCoOrdinateChangeLogs();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillSpcAssemblyPartsCoOrdinateChangeLogs();
            }
        }
        protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string Material = null;
                Material = string.IsNullOrEmpty(txtMaterial.Text) ? null : txtMaterial.Text.Trim();
                int? DivisionID = null, ProductModelID = null, SpcAssemblyID = null;
                DivisionID = (ddlDivision.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
                ProductModelID = (ddlProductModel.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlProductModel.SelectedValue);
                SpcAssemblyID = (ddlAssembly.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlAssembly.SelectedValue);

                PApiResult Result = new BChangeLogs().GetSpcAssemblyPartsCoOrdinateChangeLogs(Material, DivisionID, ProductModelID, SpcAssemblyID, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
                DataSet GetSpcAssemblyPartsCoOrdinateChangeLogs = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));

                new BXcel().ExporttoExcel(GetSpcAssemblyPartsCoOrdinateChangeLogs.Tables[0], "Assembly Parts CoOrdinate Change Logs Report");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ddlProductModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlAssembly, JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(new BECatalogue().GetSpcAssembly(Convert.ToInt32(ddlDivision.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue), null, null, true, 0).Data)), "AssemblyCode", "SpcAssemblyID", true, "Select Assembly");
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlProductModel, new BECatalogue().GetSpcModel(null, Convert.ToInt32(ddlDivision.SelectedValue), null, true, null), "SpcModel", "SpcModelID", true, "Select Model");
            new DDLBind(ddlAssembly, JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(new BECatalogue().GetSpcAssembly(Convert.ToInt32(ddlDivision.SelectedValue), Convert.ToInt32(ddlDivision.SelectedValue), null, null, true, 0).Data)), "AssemblyCode", "SpcAssemblyID", true, "Select Assembly");
        }
    }
}