using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMarketing
{
    public partial class ActivityPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<PDealer> Dealer = new BDMS_Activity().GetDealerByUserID(PSession.UserId);
                ddlDealer.DataTextField = "CodeWithName"; ddlDealer.DataValueField = "DID"; ddlDealer.DataSource = Dealer; ddlDealer.DataBind();
                if (ddlDealer.Items.Count > 1) ddlDealer.Items.Insert(0, new ListItem("Select", "0"));
                ddlDealerSearch.DataTextField = "CodeWithName"; ddlDealerSearch.DataValueField = "DID"; ddlDealerSearch.DataSource = Dealer; ddlDealerSearch.DataBind();
                if (ddlDealerSearch.Items.Count > 1)
                {
                    ddlDealerSearch.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlDealer.Enabled = false; ddlDealerSearch.Enabled = false;
                }
                BDMS_Activity oActivity = new BDMS_Activity();
                oActivity.GetActivity(ddlActivity, "FA");
                oActivity.GetActivity(ddlActivitySearch, "FA");
                txtUnits.Attributes.Add("type", "number");
                txtFromDate.Attributes.Add("type", "date");
                txtToDate.Attributes.Add("type", "date");
                //txtFromDateSearch.Attributes.Add("type", "date");
                //txtToDateSearch.Attributes.Add("type", "date");
                txtFromDateSearch.Text = DateTime.Now.AddDays(-1 * DateTime.Now.Day + 1).ToString("dd-MMM-yyyy");
                txtToDateSearch.Text = DateTime.Now.AddDays(-1 * DateTime.Now.Day + 1).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                //ddlActivity.Attributes.Add("onchange", "GetActivityData(this.value)");
                txtUnits.Attributes.Add("onkeyup", "SetExpBudget()");
                txtFromDate.Attributes.Add("onblur", "document.getElementById('" + txtToDate.ClientID + "').min=this.value;");
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            oActivity.BindActivityPlanData(gvData, Convert.ToInt32(ddlDealerSearch.SelectedValue), Convert.ToInt32(ddlActivitySearch.SelectedValue), txtFromDateSearch.Text, txtToDateSearch.Text, PSession.UserId, Convert.ToInt32(ddlStatus.SelectedValue));
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            System.Data.DataTable dt = oActivity.GetActivityPlanData(Convert.ToInt32(ddlDealerSearch.SelectedValue), Convert.ToInt32(ddlActivitySearch.SelectedValue), txtFromDateSearch.Text, txtToDateSearch.Text, Convert.ToInt32(ddlStatus.SelectedValue));
            dt.Columns.Remove("PKPlanID");
            dt.Columns.Remove("IsUpdated");
            dt.Columns.Remove("AP_Status");
            GridView gvExcel = new GridView();
            gvExcel.DataSource = dt;
            gvExcel.DataBind();
            gvExcel.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            foreach (GridViewRow gvRow in gvExcel.Rows)
            {
                for (int i = 1; i < 9; i++)
                {
                    gvRow.Cells[i].HorizontalAlign = i == 6 ? HorizontalAlign.Right : HorizontalAlign.Center;
                    if (gvRow.Cells[i].Text.Length < 13)
                    {
                        gvRow.Cells[i].Style.Add("width", "150");
                    }
                    if (i == 6 && gvRow.Cells[i].Text != "")
                    {
                        gvRow.Cells[i].Text = Convert.ToDouble(gvRow.Cells[i].Text).ToString("#,#");
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
            Response.AppendHeader("content-disposition", "attachment;filename=\"Activity Plan Data.xls\"");

            Response.Write(sw.ToString());
            Response.End();
            //new BXcel().ExporttoExcel(dt, "Activity Plan Data");
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                int iPkPlanID = Convert.ToInt32(lnkEdit.CommandArgument);
                //lnkEdit.Attributes.Add("onclick", "return GetActivityPlanData(" + iPkPlanID + ")");
            }
        }
        [System.Web.Services.WebMethod]
        public static string SaveActivityPlan(int PKPlanID, int DealerID, int ActivityID, int Units, string FromDate, string ToDate, string Location, string Remarks)
        {
            string sReturn = "";
            try
            {
                BDMS_Activity oActivity = new BDMS_Activity();
                sReturn = oActivity.SaveActivityPlan(PKPlanID, ActivityID, DealerID, Units, FromDate, ToDate, Location, Remarks, PSession.UserId);
            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }
            return sReturn;
        }
        [System.Web.Services.WebMethod]
        public static string GetActivityPlanData(int PKPlanID)
        {
            string sReturn = "";
            try
            {
                BDMS_Activity oActivity = new BDMS_Activity();
                sReturn = JsonConvert.SerializeObject(oActivity.GetActivityPlanDataByID(PKPlanID));

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

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            int PKPlanID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            List<PDMS_ActivityPlan> data = oActivity.GetActivityPlanDataByID(PKPlanID);
            if (data.Count > 0)
            {
                hdnPkPlanID.Value = data[0].AP_PKPlanID.ToString();
                ddlDealer.SelectedValue = data[0].AP_FKDealerID.ToString();
                ddlActivity.SelectedValue = data[0].AP_FKActivityID.ToString();
                txtUnits.Value = data[0].AP_NoofUnits.ToString();
                txtFromDate.Value = data[0].AP_FromDate.ToString("dd-MMM-yyyy");
                txtToDate.Value = data[0].AP_ToDate.ToString("dd-MMM-yyyy");
                txtLocation.Value = data[0].AP_Location;
                txtRemarks.Value = data[0].AP_Remarks;
                txtBudget.Value = data[0].AP_BudgetPerUnit.ToString();
                txtExpBudget.Value = data[0].AP_ExpBudget.ToString();
                lblAjaxSharing.InnerText = "(" + data[0].AI_AjaxSharing.ToString() + "%)";
                lblDealerSharing.InnerText = "(" + (100 - Convert.ToDouble("0" + data[0].AI_AjaxSharing.ToString())).ToString() + "%)";

                var TotalBudget = data[0].AP_ExpBudget;
                var AjaxSharing = data[0].AI_AjaxSharing;
                txtAjaxSharing.Value = AjaxSharing.ToString();
                txtDealerSharing.Value = (TotalBudget - AjaxSharing).ToString();
            }

        }

        protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ActivityID = Convert.ToInt32(ddlActivity.SelectedValue);
            List<PDMS_ActivityInfo> actinfoList = new BDMS_Activity().GetActivityInfoDataByID(ActivityID);
            if (actinfoList.Count > 0 && ActivityID > 0)
            {
                txtFA.Value = actinfoList[0].FunctionalArea.ToString();
                txtBudget.Value = actinfoList[0].Budget.ToString();
                txtExpBudget.Value = "";
                lblAjaxSharing.InnerText = " (" + actinfoList[0].AjaxSharing.ToString() + ")";
                lblDealerSharing.InnerText = " (" + (100 - Convert.ToDouble(actinfoList[0].AjaxSharing.ToString())).ToString() + ")";
                hdnAjaxSharing.Value = actinfoList[0].AjaxSharing.ToString(); //actinfoList[0].Budget* actinfoList[0].AjaxSharing/100*Convert.ToDouble("0"+txtUnits.Value);
                txtAjaxSharing.Value = "";
                txtDealerSharing.Value = "";
                lblUnits.InnerText = "No of Units(" + actinfoList[0].UnitDesc + ")";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "SetExpBudget();", true);
            }
            else
            {
                txtFA.Value = "";
                txtBudget.Value = "";
                txtExpBudget.Value = "";
                lblAjaxSharing.InnerText = "";
                lblDealerSharing.InnerText = "";
                txtAjaxSharing.Value = "";
                hdnAjaxSharing.Value = "";
                txtDealerSharing.Value = "";
                lblUnits.InnerText = "No of Units";
            }
        }
    }
}