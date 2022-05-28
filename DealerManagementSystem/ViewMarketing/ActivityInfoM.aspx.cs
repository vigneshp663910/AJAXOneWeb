using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMarketing
{
    public partial class ActivityInfoM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BDMS_Activity oActivity = new BDMS_Activity();
                oActivity.GetActivity(ddlActivity);
                oActivity.GetActivity(ddlActivitySearch);
                oActivity.GetCommonMaster(ddlFunctionalArea, "ACT_FA", 0);

                oActivity.GetCommonMaster(ddlUnit, "ACT_UNIT", 0);
                txtBudget.Attributes.Add("type", "number");
                txtAjaxSharing.Attributes.Add("type", "number");
                txtDealerSharing.Attributes.Add("type", "number");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "keyin", "Initialize()", true);

            }
        }
        [System.Web.Services.WebMethod]

        public static string SaveActivityInfo(int ActivityID, int FunctionaAreaID, int UnitID, double dblBudget, double dblAjaxSharing, double dblDealerSharing, string SAC, double GST, string ActivityType)
        {
            string sReturn = "";
            try
            {
                BDMS_Activity oActivity = new BDMS_Activity();
                sReturn = oActivity.SaveActivityInfo(ActivityID, FunctionaAreaID, UnitID, dblBudget, dblAjaxSharing, dblDealerSharing, SAC, GST, PSession.User.UserID, ActivityType);
            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }
            return sReturn;

        }
        [System.Web.Services.WebMethod]

        public static string GetActivityInfo(int ActivityID)
        {
            string sReturn = "";
            try
            {
                BDMS_Activity oActivity = new BDMS_Activity();
                sReturn = JsonConvert.SerializeObject(oActivity.GetActivityInfoDataByID(ActivityID));


            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }
            return sReturn;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            oActivity.GetActivityInfoData(gvData, Convert.ToInt32(ddlActivitySearch.SelectedValue));
            if (hdnSWidth.Value == "") hdnSWidth.Value = "1000";

            int width = Convert.ToInt32(hdnSWidth.Value);
            gvData.Columns[3].Visible = width > 500;
            gvData.Columns[4].Visible = width > 500;
            gvData.Columns[5].Visible = width > 768;
            gvData.Columns[6].Visible = width > 768;
            gvData.Columns[7].Visible = width > 768;
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                int iActivityID = Convert.ToInt32(lnkEdit.CommandArgument);
                //lnkEdit.Attributes.Add("onclick", "return GetActivityData(" + iActivityID + ")");
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            System.Data.DataTable dt = oActivity.GetActivityInfoData(Convert.ToInt32(ddlActivitySearch.SelectedValue));
            GridView gvExcel = new GridView();
            gvExcel.DataSource = dt;
            gvExcel.DataBind();
            gvExcel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            foreach (GridViewRow gvRow in gvExcel.Rows)
            {
                for (int i = 1; i < 8; i++)
                {
                    gvRow.Cells[i].HorizontalAlign = i == 5 ? HorizontalAlign.Right : HorizontalAlign.Center;
                    if (gvRow.Cells[i].Text.Length < 13)
                    {
                        gvRow.Cells[i].Style.Add("width", "150");
                    }
                }
            }
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvExcel.RenderControl(hw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";
            Response.AppendHeader("content-disposition", "attachment;filename=\"Activity Info Master.xls\"");

            Response.Write(sw.ToString());
            Response.End();
            //new BXcel().ExporttoExcel(dt, "Activity Info Master");
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = ((LinkButton)sender).NamingContainer as GridViewRow;
                int ActivityID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
                List<PDMS_ActivityInfo> actinfoList = new BDMS_Activity().GetActivityInfoDataByID(ActivityID);
                if (actinfoList.Count > 0)
                {
                    ddlActivity.SelectedValue = actinfoList[0].ActivityID.ToString();
                    ddlFunctionalArea.SelectedValue = actinfoList[0].FunctionalAreaID.ToString();
                    ddlUnit.SelectedValue = actinfoList[0].Unit.ToString();
                    txtBudget.Value = actinfoList[0].Budget.ToString();
                    txtAjaxSharing.Value = actinfoList[0].AjaxSharing.ToString();
                    txtDealerSharing.Value = actinfoList[0].DealerSharing.ToString();
                    ddlGST.SelectedValue = actinfoList[0].GST.ToString();
                    txtSAC.Value = actinfoList[0].SAC;
                    ddlActivityType.SelectedValue = actinfoList[0].ActivityType.ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError("Activity Info Master lnk_Edit", ex);
            }
        }
        int PageIndex = 0;
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PageIndex = e.NewPageIndex;
        }

        protected void gvData_PageIndexChanged(object sender, EventArgs e)
        {
            gvData.PageIndex = PageIndex;
            btnSearch_Click(null, null);
        }
    }
}