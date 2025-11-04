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
    public partial class SpcModelChangeLogs : BasePage
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Report » SpcModel Change Logs');</script>");
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;                
                txtDateFrom.Text = DateTime.Now.AddDays(1 + (-1 * DateTime.Now.Day)).ToString("yyyy-MM-dd");
                txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillSpcModelChangeLogs();
            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void fillSpcModelChangeLogs()
        {
            try
            {
                string ModelCode = null;
                ModelCode = string.IsNullOrEmpty(txtSpcModelCode.Text) ? null : txtSpcModelCode.Text.Trim();
                PApiResult Result = new BChangeLogs().GetSpcModelChangeLogs(ModelCode, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false, PageIndex, gvSpcModelChangeLogs.PageSize);
                DataSet GetSpcModelChangeLogs = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));

                int RowCount = Result.RowCount;

                GetSpcModelChangeLogs.Tables[0].Columns.Remove(GetSpcModelChangeLogs.Tables[0].Columns["RowCount"]);

                gvSpcModelChangeLogs.DataSource = GetSpcModelChangeLogs.Tables[0];
                gvSpcModelChangeLogs.DataBind();

                if (RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                    imgBtnExportExcel.Visible = false;
                }
                else
                {
                    PageCount = (RowCount + gvSpcModelChangeLogs.PageSize - 1) / gvSpcModelChangeLogs.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    imgBtnExportExcel.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvSpcModelChangeLogs.PageSize) + 1) + " - " + (((PageIndex - 1) * gvSpcModelChangeLogs.PageSize) + gvSpcModelChangeLogs.Rows.Count) + " of " + RowCount;
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
                fillSpcModelChangeLogs();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillSpcModelChangeLogs();
            }
        }
        protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string ModelCode = null;
                ModelCode = string.IsNullOrEmpty(txtSpcModelCode.Text) ? null : txtSpcModelCode.Text.Trim();
                PApiResult Result = new BChangeLogs().GetSpcModelChangeLogs(ModelCode, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
                DataSet GetSpcModelChangeLogs = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));

                new BXcel().ExporttoExcel(GetSpcModelChangeLogs.Tables[0], "Model Change Logs Report");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}