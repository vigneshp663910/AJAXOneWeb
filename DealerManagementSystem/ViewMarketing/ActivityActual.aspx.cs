using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Newtonsoft.Json;
using Properties;

namespace DealerManagementSystem.ViewMarketing
{
    public partial class ActivityActual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<PDealer> Dealer = new BDMS_Activity().GetDealerByUserID(PSession.User.UserID);
                ddlDealer.DataTextField = "CodeWithName"; ddlDealer.DataValueField = "DID"; ddlDealer.DataSource = Dealer; ddlDealer.DataBind();
                if (ddlDealer.Items.Count > 1)
                {
                    ddlDealer.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlDealer_SelectedIndexChanged(ddlDealer, null);
                    ddlDealer.Enabled = false;
                    ddlDealerSearch.Enabled = false;
                }
                ddlDealerSearch.DataTextField = "CodeWithName"; ddlDealerSearch.DataValueField = "DID"; ddlDealerSearch.DataSource = Dealer; ddlDealerSearch.DataBind();
                if (ddlDealerSearch.Items.Count > 1)
                {
                    ddlDealerSearch.Items.Insert(0, new ListItem("Select", "0"));
                }


                txtUnits.Attributes.Add("type", "number");
                txtFromDate.Attributes.Add("type", "date");
                txtToDate.Attributes.Add("type", "date");

                txtFromDateSearch.Text = DateTime.Now.AddDays(-1 * DateTime.Now.Day + 1).ToString("dd-MMM-yyyy");
                txtToDateSearch.Text = DateTime.Now.AddDays(-1 * DateTime.Now.Day + 1).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                txtFromDate.Attributes.Add("onblur", "document.getElementById('" + txtToDate.ClientID + "').min=this.value;");
                txtFromDate.Attributes.Add("max", "new Date()");
                txtToDate.Attributes.Add("max", "new Date()");
                //ddlPlannedActivity.Attributes.Add("onchange", "GetActivityPlanData(this.value)");
            }
        }

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            BDMS_Activity oAct = new BDMS_Activity();
            oAct.GetPlannedActivity(ddlPlannedActivity, Convert.ToInt32(ddlDealer.SelectedValue));
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            oActivity.BindActivityActualData(gvData, Convert.ToInt32(ddlDealerSearch.SelectedValue), ddlDateOn.SelectedValue, txtFromDateSearch.Text, txtToDateSearch.Text, PSession.User.UserID);
            foreach (GridViewRow gvRow in gvData.Rows)
            {
                LinkButton lnkEdit = gvRow.FindControl("lnkEdit") as LinkButton;
                HiddenField hdnApprovalLevel = gvRow.FindControl("hdnApprovalLevel") as HiddenField;
                LinkButton lnkGenerateInvoice = gvRow.FindControl("lnkGenerateInvoice") as LinkButton;
                HiddenField hdnApprovalStatus = gvRow.FindControl("hdnApprovalStatus") as HiddenField;
                HiddenField hdnInvID = gvRow.FindControl("hdnInvID") as HiddenField;
                HiddenField hdnActualID = gvRow.FindControl("hdnActualID") as HiddenField;
                if (PSession.User.UserTypeID == 7)
                {
                    lnkEdit.Text = "View";
                }
                if (hdnApprovalLevel.Value == "2" && hdnApprovalStatus.Value == "10")
                {
                    lnkGenerateInvoice.Visible = true;
                    lnkEdit.Text = "View";
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkGenerateInvoice);
                }
                if (hdnInvID.Value != "0")
                {
                    lnkGenerateInvoice.Text = "Print Invoice";
                    lnkGenerateInvoice.Attributes.Add("onclick", "window.open('YDMS_ActivityInvoice.aspx?AID=" + oActivity.Encrypt(hdnActualID.Value) + "', 'newwindow', 'toolbar=no,location=no,menubar=no,width=1000,height=600,titlebar=no, fullscreen=no,resizable=yes,scrollbars=yes,top=60,left=60');return false;");
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "keHide", "$('#divEntry').toggle(1000);", true);
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            System.Data.DataTable dt = oActivity.GetActivityActualDataForExcel(Convert.ToInt32(ddlDealerSearch.SelectedValue), ddlDateOn.SelectedValue, txtFromDateSearch.Text, txtToDateSearch.Text, PSession.User.UserID);

            new BXcel().ExporttoExcel(dt, "Activity Actual Data");
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                int iPkPlanID = Convert.ToInt32(lnkEdit.CommandArgument);
                lnkEdit.Attributes.Add("onclick", "return GetActivityActualData(" + iPkPlanID + ")");
            }
        }
        [System.Web.Services.WebMethod]
        public static string SaveActivityActual(int PKPlanID, int Units, string FromDate, string ToDate, string Location, string Remarks, int Status, string NDRemarks, double dblExpenses)
        {
            string sReturn = "";
            try
            {

                BDMS_Activity oActivity = new BDMS_Activity();
                sReturn = oActivity.SaveActivityActual(PKPlanID, Units, FromDate, ToDate, Location, Remarks, PSession.User.UserID, Status, NDRemarks, dblExpenses);


            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }
            return sReturn;
        }
        [System.Web.Services.WebMethod]
        public static string GetActivityActualData(int PKPlanID)
        {
            string sReturn = "";
            try
            {
                BDMS_Activity oActivity = new BDMS_Activity();
                sReturn = JsonConvert.SerializeObject(oActivity.GetActivityActualDataByID(PKPlanID));

            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }
            return sReturn;
        }
        //[System.Web.Services.WebMethod]


        protected void flUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            //HttpPostedFile postedFile = flUpload.PostedFile;
            //byte[] fileData = null;
            //using (var binaryReader = new BinaryReader(postedFile.InputStream))
            //{
            //    fileData = binaryReader.ReadBytes(postedFile.ContentLength);
            //}
            //List<PDMS_ActivityDocs> lstDocs=Session["ActDocs"] as List<PDMS_ActivityDocs>;
            //if(lstDocs==null)
            //{
            //    lstDocs = new List<PDMS_ActivityDocs>();
            //}
            //PDMS_ActivityDocs objDocs = new PDMS_ActivityDocs();
            //objDocs.AD_Sno = (lstDocs == null) ? 1 : lstDocs.Count + 1;
            //objDocs.AD_Description = txtFileDesc.Value;
            //objDocs.AD_FileSize = postedFile.ContentLength;
            //objDocs.AD_FileName = postedFile.FileName;
            //objDocs.AD_ContentType = postedFile.ContentType;
            //objDocs.AD_AttachedFile = fileData;
            //lstDocs.Add(objDocs);
            //Session["ActDocs"] = lstDocs;

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int PkPlanID = Convert.ToInt32(ddlPlannedActivity.SelectedValue);
                int Units = Convert.ToInt32(txtUnits.Value);
                string FromDate = txtFromDate.Value;
                string ToDate = txtToDate.Value;
                string Location = txtLocation.Value;
                string Remarks = txtRemarks.Value;
                int Status = Convert.ToInt32(ddlStatus.SelectedValue);
                string NDRemarks = txtNotDoneRemarks.Value;
                double dblExpenses = Convert.ToDouble(txtExpBudget.Value);
                Session["ActDocs"] = null;
                SaveFile(flUpload1, "Image 1");
                SaveFile(flUpload2, "Image 2");
                SaveFile(flUpload3, "Image 3");
                SaveFile(flUpload4, "Image 4");

                BDMS_Activity oActivity = new BDMS_Activity();
                oActivity.SaveActivityActual(PkPlanID, Units, FromDate, ToDate, Location, Remarks, PSession.User.UserID, Status, NDRemarks, dblExpenses);
                List<PDMS_ActivityDocs> lstDocs = Session["ActDocs"] as List<PDMS_ActivityDocs>;
                if (lstDocs != null)
                {
                    oActivity.SaveActivityAttachments(Convert.ToInt32(ddlPlannedActivity.SelectedValue), lstDocs);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('Saved Successfully!');Clear();", true);
                btnCancel_Click(btnCancel, null);
                Search_Click(Search, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert(\"Error:" + ex.Message + " \")", true);
            }
        }
        protected void SaveFile(FileUpload flUpload, string ImageDesc)
        {
            if (flUpload.HasFile)
            {
                HttpPostedFile postedFile = flUpload.PostedFile;

                if (postedFile != null)
                {
                    string sFileName = DateTime.Now.ToString("ddmmyhhmmsstt") + "_" + PSession.User.UserID.ToString() + "_" + postedFile.FileName;
                    string pathaname = HttpContext.Current.Server.MapPath("~/YDMS/Temp/") + @"\" + sFileName;
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/YDMS/Temp/"))) Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/YDMS/Temp/"));
                    if (postedFile.ContentType.ToUpper().Contains("IMAGE"))
                    {
                        BDMS_ImageCompress imgCompress = BDMS_ImageCompress.GetImageCompressObject;
                        imgCompress.GetImage = new System.Drawing.Bitmap(postedFile.InputStream);
                        imgCompress.Height = 800;
                        imgCompress.Width = 1160;
                        //imgCompress.Save(httpPostedFile.FileName, @"C:\Documents and Settings\Rasmi\My Documents\Visual Studio2008\WebSites\compressImageFile\Logo");
                        imgCompress.Save(sFileName, HttpContext.Current.Server.MapPath("~/YDMS/Temp/"), 100);
                        if (!File.Exists(pathaname)) postedFile.SaveAs(pathaname);
                    }
                    else
                    {
                        flUpload.SaveAs(pathaname);
                    }

                    byte[] fileData = File.ReadAllBytes(pathaname);
                    FileInfo fInfo = new FileInfo(pathaname);
                    List<PDMS_ActivityDocs> lstDocs = Session["ActDocs"] as List<PDMS_ActivityDocs>;
                    if (lstDocs == null)
                    {
                        lstDocs = new List<PDMS_ActivityDocs>();
                    }
                    PDMS_ActivityDocs objDocs = new PDMS_ActivityDocs();

                    objDocs.AD_Sno = (lstDocs.Count == 0) ? 1 : (lstDocs.Max(d => d.AD_Sno) + 1);
                    objDocs.AD_Description = ImageDesc;
                    objDocs.AD_FileSize = fInfo.Length;
                    objDocs.AD_FileName = postedFile.FileName;
                    objDocs.AD_ContentType = postedFile.ContentType;
                    objDocs.AD_AttachedFile = fileData;
                    lstDocs.Add(objDocs);
                    Session["ActDocs"] = lstDocs;
                    try
                    {
                        File.Delete(pathaname);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkEdit = sender as LinkButton;
            int iPlanID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            BDMS_Activity oActivity = new BDMS_Activity();
            DataSet ds = oActivity.GetActualDataByID(iPlanID);

            DataTable dtHdr = ds.Tables[0];
            if (dtHdr.Rows.Count > 0)
            {
                DataRow dr = dtHdr.Rows[0];
                ddlDealer.SelectedValue = dr["AP_FKDealerID"].ToString();

                ddlPlannedActivity.Items.Clear();
                ddlPlannedActivity.Items.Add(new ListItem(dr["Activity"].ToString() + "[" + dr["AP_ControlNo"].ToString() + "]", iPlanID.ToString()));
                lblPeriod.Text = dr["Plan_Period"].ToString();
                lblUnitsPlanned.Text = dr["AP_NoofUnits"].ToString();
                lblBudgetPerUnit.Text = dr["AI_Budget"].ToString();
                lblExpectedBudget.Text = dr["TotalPlanBudget"].ToString();
                txtAjaxSharing.Text = dr["Plan_AjaxSharingAmount"].ToString();
                txtDealerSharing.Text = dr["Plan_DealerSharingAmount"].ToString();
                lblPlanLocation.Text = dr["AP_Location"].ToString();

                ddlStatus.SelectedValue = dr["AP_Status"].ToString();
                txtNotDoneRemarks.Value = dr["AP_NotDoneRemarks"].ToString();
                txtUnits.Value = dr["AA_NoofUnits"].ToString();
                txtFromDate.Value = dr["AA_FromDate"].ToString();
                txtToDate.Value = dr["AA_ToDate"].ToString();
                txtExpBudget.Value = dr["AA_Expenses"].ToString();
                txtAjaxSharingA.Value = dr["Actual_AjaxSharingAmount"].ToString();
                txtDealerSharingA.Value = dr["Actual_DealerSharingAmount"].ToString();
                txtLocation.Value = dr["AA_Location"].ToString();
                txtRemarks.Value = dr["AA_Remarks"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key123", "GetActivityData('" + dr["AP_FKActivityID"].ToString() + "');CheckStatus(" + dr["AP_Status"].ToString() + ");SetActualDates('" + txtFromDate.Value + "','" + txtToDate.Value + "')", true);
            }
            Session["AADocs"] = ds.Tables[1];
            lstImages.DataSource = ds.Tables[1];
            lstImages.DataBind();
            lstImages.Visible = true;
            ddlDealer.Enabled = false;
            if (PSession.User.UserTypeID == 7 || lnkEdit.Text.Equals("View"))
            {
                btnSubmit.Visible = false;
                divAttach.Style.Add("display", "none");
                ddlStatus.Enabled = false;
                txtUnits.Disabled = true;
                txtFromDate.Disabled = true;
                txtToDate.Disabled = true;
                txtExpBudget.Disabled = true;
                txtLocation.Disabled = true;
                txtRemarks.Disabled = true;
                txtNotDoneRemarks.Disabled = true;

            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            int iDocID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            DataTable dtDocs = Session["AADocs"] as DataTable;

            if (dtDocs != null)
            {
                if (dtDocs.Select("AD_PKDocID='" + iDocID + "'").Length > 0)
                {
                    DataRow dr = (DataRow)dtDocs.Select("AD_PKDocID='" + iDocID + "'").GetValue(0);

                    byte[] bytData = ((byte[])dr["AD_AttachedFile"]);
                    string FileNme = dr["AD_FileName"].ToString();
                    string ContentType = dr["AD_ContentType"].ToString();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("Content-Disposition", "attachment;filename=\"" + FileNme + "\"");
                    Response.ContentType = ContentType;
                    Response.BinaryWrite(bytData);
                    Response.End();
                    lstImages.DataSource = dtDocs;
                    lstImages.DataBind();

                }
            }


        }
        //protected void btnAddMore_Click(object sender, EventArgs e)
        //{
        //    string sReturn = "";
        //    try
        //    {
        //        HttpPostedFile postedFile = flUpload.PostedFile;
        //        byte[] fileData = null;
        //        using (var binaryReader = new BinaryReader(postedFile.InputStream))
        //        {
        //            fileData = binaryReader.ReadBytes(postedFile.ContentLength);
        //        }
        //        List<PDMS_ActivityDocs> lstDocs = Session["ActDocs"] as List<PDMS_ActivityDocs>;
        //        if (lstDocs == null)
        //        {
        //            lstDocs = new List<PDMS_ActivityDocs>();
        //        }
        //        PDMS_ActivityDocs objDocs = new PDMS_ActivityDocs();

        //        objDocs.AD_Sno = (lstDocs.Count == 0) ? 1 : (lstDocs.Max(d => d.AD_Sno) + 1);
        //        objDocs.AD_Description = txtFileDesc.Value;
        //        objDocs.AD_FileSize = postedFile.ContentLength;
        //        objDocs.AD_FileName = postedFile.FileName;
        //        objDocs.AD_ContentType = postedFile.ContentType;
        //        objDocs.AD_AttachedFile = fileData;
        //        lstDocs.Add(objDocs);
        //        Session["ActDocs"] = lstDocs;
        //        gvAttachments.DataSource = lstDocs;
        //        gvAttachments.DataBind();
        //        BDMS_Activity oAct = new BDMS_Activity();
        //        oAct.SaveActivityAttachments(Convert.ToInt32(ddlPlannedActivity.SelectedValue), lstDocs);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "keyUp", "FileUpladed();", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "keyUp", "FileError(\"" + ex.Message + "\");", true);

        //    }
        //}

        protected void lstImages_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkDownload = e.Item.FindControl("lnkDownload") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkDownload);
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lstImages.DataSource = null;
            lstImages.DataBind();
            btnSubmit.Visible = true;
            BDMS_Activity oAct = new BDMS_Activity();
            oAct.GetPlannedActivity(ddlPlannedActivity, Convert.ToInt32(ddlDealer.SelectedValue));
            divAttach.Style.Add("display", "");
            ddlStatus.Enabled = true;
            txtUnits.Disabled = false;
            txtFromDate.Disabled = false;
            txtToDate.Disabled = false;
            txtExpBudget.Disabled = false;
            txtLocation.Disabled = false;
            txtRemarks.Disabled = false;
            txtNotDoneRemarks.Disabled = false;
            ddlDealer.Enabled = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key123", "Clear();", true);
        }

        protected void lnkGenerateInvoice_Click(object sender, EventArgs e)
        {
            LinkButton lnkEdit = sender as LinkButton;
            int PKActualID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            BDMS_Activity oActivity = new BDMS_Activity();
            string InvoiceNo = oActivity.GenerateInvoice(PKActualID, PSession.User.UserID);

            if (!InvoiceNo.Contains("Error"))
            {
                Search_Click(Search, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "PrintInvoice(\"" + oActivity.Encrypt(PKActualID.ToString()) + "\");", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "keyError", "alert(\"" + InvoiceNo + "\");", true);
            }
        }

        protected void ddlPlannedActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            int PKPlanID = Convert.ToInt32(ddlPlannedActivity.SelectedValue);
            List<PDMS_ActivityPlan> lstActPlan = oActivity.GetActivityPlanDataByID(PKPlanID);
            if (lstActPlan.Count > 0)
            {
                lblPeriod.Text = lstActPlan[0].AP_FromDate.ToString("dd.MM.yyyy") + " to " + lstActPlan[0].AP_ToDate.ToString("dd.MM.yyyy");
                lblPlanLocation.Text = lstActPlan[0].AP_Location;
                lblExpectedBudget.Text = lstActPlan[0].AP_ExpBudget.ToString("#0");
                lblBudgetPerUnit.Text = lstActPlan[0].AP_BudgetPerUnit.ToString("#0");
                lblUnitsPlanned.Text = lstActPlan[0].AP_NoofUnits.ToString() + " " + lstActPlan[0].AP_Unit;
                lblActUnits.InnerText = "Actual No of units(" + lstActPlan[0].AP_Unit + ")";
                var TotalBudget = Convert.ToDouble(lblExpectedBudget.Text);
                var AjaxSharing = (TotalBudget * lstActPlan[0].AP_AjaxSharing / 100);
                lblAjaxSharing.InnerText = " (" + lstActPlan[0].AP_AjaxSharing + "%)";
                lblDealerSharing.InnerText = " (" + (100 - lstActPlan[0].AP_AjaxSharing) + "%)";
                lblAjaxSharingA.InnerText = " (" + lstActPlan[0].AP_AjaxSharing + "%)";
                lblDealerSharingA.InnerText = " (" + (100 - lstActPlan[0].AP_AjaxSharing) + "%)";
                txtAjaxSharing.Text = AjaxSharing.ToString("#0");
                txtDealerSharing.Text = (TotalBudget - AjaxSharing).ToString("#0");
                hdnAjaxSharing.Value = lstActPlan[0].AP_AjaxSharing.ToString("#0");
            }
        }
    }
}