using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMarketing
{
    public partial class ClaimApproval : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                List<PDealer> Dealer = new BDMS_Activity().GetDealerByUserID(PSession.User.UserID);

                ddlDealerSearch.DataTextField = "CodeWithName"; ddlDealerSearch.DataValueField = "DID"; ddlDealerSearch.DataSource = Dealer; ddlDealerSearch.DataBind();
                if (ddlDealerSearch.Items.Count > 1) ddlDealerSearch.Items.Insert(0, new ListItem("Select", "0"));

                divStatus.Visible = false;
                divSearch.Visible = true;
                if (Request.QueryString.Count == 0)
                {
                    divSearch.Visible = false;
                }
                Aes myAes = Aes.Create();

                BDMS_Activity oAct = new BDMS_Activity();
                try
                {
                    if (Request.QueryString["PAGE"] != null)
                    {
                        string sAppLevel = Convert.ToString(Request.QueryString["PAGE"]);
                        string sArea = Convert.ToString(Request.QueryString["FA"]);
                        string AppLevelEnc = oAct.Encrypt(sAppLevel);
                        string AreaLevelEnc = oAct.Encrypt(sArea);
                        Response.Redirect("ClaimApproval.aspx?PID=" + HttpUtility.UrlEncode(AppLevelEnc) + "&FAID=" + AreaLevelEnc);

                    }
                    else if (Request.QueryString["PID"] != null)
                    {
                        string sAppLevelEnc = HttpUtility.UrlDecode(Convert.ToString(Request.QueryString["PID"]));
                        string AreaLevelEnc = HttpUtility.UrlDecode(Convert.ToString(Request.QueryString["FAID"]));
                        string sAppLevel = oAct.Decrypt(sAppLevelEnc);
                        string sFA = oAct.Decrypt(AreaLevelEnc);
                        ViewState["ApprovalLevel"] = sAppLevel;
                        ViewState["FAID"] = sFA.Split('.')[0];
                        lbllevel.Text = "- Level " + sAppLevel;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogError("Claim Approval page loading", ex);
                    divSearch.Visible = false;
                }


            }

        }


        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Search_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            oActivity.BindActivityActualDataForApproval(gvData, Convert.ToInt32(ddlDealerSearch.SelectedValue), txtFromDateSearch.Text, txtToDateSearch.Text, Convert.ToInt32(ddlAppStatus.SelectedValue), PSession.User.UserID, Convert.ToInt32(ViewState["ApprovalLevel"]), Convert.ToInt32(ViewState["FAID"]));
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {

            DataTable dtDocs = Session["AADocs"] as DataTable;
            int iSno = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            DataRow dr = dtDocs.Select("AD_PkDocID= " + iSno).GetValue(0) as DataRow;
            if (dr != null)
            {
                Response.Clear();
                Response.ContentType = dr["AD_ContentType"].ToString();
                Response.AddHeader("content-disposition", "attachment;filename=\"" + dr["AD_FileName"].ToString() + "\"");
                Response.BinaryWrite((byte[])dr["AD_AttachedFile"]);
                Response.End();
            }

        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            System.Data.DataTable dt = oActivity.GetActivityActualDataForApproval_ForExcel(Convert.ToInt32(ddlDealerSearch.SelectedValue), txtFromDateSearch.Text, txtToDateSearch.Text, Convert.ToInt32(ddlAppStatus.SelectedValue), PSession.User.UserID, Convert.ToInt32(ViewState["ApprovalLevel"]), Convert.ToInt32(ViewState["FAID"]));

            new BXcel().ExporttoExcel(dt, "Activity Data");
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int AA_PKActualID = Convert.ToInt32(((LinkButton)sender).CommandArgument);


                BDMS_Activity oActivity = new BDMS_Activity();
                DataSet ds = oActivity.GetActivityActualDataByActualID(AA_PKActualID);

                DataTable dtHdr = ds.Tables[0];
                if (dtHdr.Rows.Count > 0)
                {
                    DataRow dr = dtHdr.Rows[0];
                    txtDealer.Text = dr["Dealer"].ToString();
                    txtPlannedActivity.Text = dr["Activity"].ToString() + "[" + dr["AP_ControlNo"].ToString() + "]";
                    lblPeriod.Text = dr["Plan_Period"].ToString();
                    lblUnitsPlanned.Text = dr["AP_NoofUnits"].ToString();
                    lblBudgetPerUnit.Text = dr["AI_Budget"].ToString();
                    lblExpectedBudget.Text = dr["TotalPlanBudget"].ToString();
                    txtAjaxSharing.Text = dr["Plan_AjaxSharingAmount"].ToString();
                    txtDealerSharing.Text = dr["Plan_DealerSharingAmount"].ToString();
                    lblPlanLocation.Text = dr["AP_Location"].ToString();
                    txtUnits.Text = dr["AA_NoofUnits"].ToString();
                    txtFromDate.Text = dr["AA_FromDate"].ToString();
                    txtToDate.Text = dr["AA_ToDate"].ToString();
                    txtExpBudget.Text = dr["AA_Expenses"].ToString();
                    txtAjaxSharingA.Text = dr["Actual_AjaxSharingAmount"].ToString();
                    txtDealerSharingA.Text = dr["Actual_DealerSharingAmount"].ToString();
                    txtLocation.Text = dr["AA_Location"].ToString();
                    txtRemarks.Text = dr["AA_Remarks"].ToString();
                    ddlAppStatus1.SelectedValue = Convert.ToInt32(dr["AppStatus1"].ToString()).ToString();
                    txtApp1Remarks.Text = dr["AppRemarks1"].ToString();
                    txtApp1Amount.Text = dr["App1Amount"].ToString();
                    ddlAppStatus2.SelectedValue = Convert.ToInt32(dr["AppStatus2"].ToString()).ToString();
                    txtApp2Remarks.Text = dr["AppRemarks2"].ToString();
                    txtApp2Amount.Text = dr["App2Amount"].ToString();
                    ViewState["PKActualID"] = dr["PKActualID"].ToString();
                    if (Convert.ToInt32(ViewState["ApprovalLevel"]) == 1)
                    {
                        divApp2.Visible = false;
                    }
                    else
                    {
                        ddlAppStatus1.Enabled = false;
                        txtApp1Amount.Enabled = true;
                        txtApp1Remarks.Enabled = true;
                    }
                    if (Convert.ToInt32(ViewState["FAID"]) == 3)
                    {
                        divApp1.Style.Add("display", "none");
                    }
                    string blnCheck = lblPeriod.Text == "" ? "true" : "false";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key123", "HidePlan(" + blnCheck + ");GetActivityData('" + dr["AP_FKActivityID"].ToString() + "');SetActualDates('" + txtFromDate.Text + "','" + txtToDate.Text + "')", true);
                }
                Session["AADocs"] = ds.Tables[1];
                lstImages.DataSource = ds.Tables[1];
                lstImages.DataBind();
                lstImages.Visible = true;

                divStatus.Visible = true;
                divSearch.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError("Claim Approval lnk_edit", ex);
            }
        }
        protected void lstImages_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkDownload = e.Item.FindControl("lnkDownload") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkDownload);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BDMS_Activity objAct = new BDMS_Activity();
                int ActualID = Convert.ToInt32(ViewState["PKActualID"]);
                int ApprovalLevel = Convert.ToInt32(ViewState["ApprovalLevel"]);
                long UpdatedBy = PSession.User.UserID;
                int Status = ApprovalLevel == 1 ? Convert.ToInt32(ddlAppStatus1.SelectedValue) : Convert.ToInt32(ddlAppStatus2.SelectedValue);
                string strStatus = ApprovalLevel == 1 ? (ddlAppStatus1.SelectedItem.Text) : (ddlAppStatus2.SelectedItem.Text);
                string Remarks = ApprovalLevel == 1 ? txtApp1Remarks.Text : txtApp2Remarks.Text;
                string sAmount = ApprovalLevel == 1 ? txtApp1Amount.Text : txtApp2Amount.Text;
                double dblAmount = Convert.ToDouble(sAmount == "" ? "0" : sAmount);
                if (Status == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('Please select status!');", true);
                    (ApprovalLevel == 1 ? ddlAppStatus1 : ddlAppStatus2).Focus();
                    return;
                }
                if (dblAmount == 0 && !strStatus.ToUpper().Contains("REJECT"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('Please enter approved amount!');", true);
                    (ApprovalLevel == 1 ? txtApp1Amount : txtApp2Amount).Focus();
                    return;
                }
                if (Status == 9 && txtApp1Remarks.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('Please enter remarks!');", true);
                    txtApp1Remarks.Focus();
                    return;
                }
                if (Status == 11 && txtApp2Remarks.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('Please enter remarks!');", true);
                    txtApp2Remarks.Focus();
                    return;
                }
                objAct.SaveActivityApproval(ActualID, ApprovalLevel, UpdatedBy, Status, dblAmount, Remarks);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('" + (strStatus.ToUpper().Contains("REJECT") ? "Rejected" : "Approved Successfully!") + "');", true);
                Search_Click(Search, null);
                divSearch.Visible = true;
                divStatus.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert(\"Error:" + ex.Message + " \")", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divSearch.Visible = true;
            divStatus.Visible = false;
        }
    }
}