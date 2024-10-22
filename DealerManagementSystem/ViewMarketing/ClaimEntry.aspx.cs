using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Newtonsoft.Json;
using Properties;

namespace DealerManagementSystem.ViewMarketing
{
    public partial class ClaimEntry : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMarketing_ClaimEntry; } }
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
                btnNew.Attributes.Add("onclick", "return confirm('Do you want ro enter a new claim?');");
                divEntry.Visible = true;
                divSearch.Visible = false;
                List<PDealer> Dealer = new BDMS_Activity().GetDealerByUserID(PSession.User.UserID);
                ddlDealer.DataTextField = "CodeWithName"; ddlDealer.DataValueField = "DID"; ddlDealer.DataSource = Dealer; ddlDealer.DataBind();
                if (ddlDealer.Items.Count > 1)
                {
                    ddlDealer.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    //ddlDealer_SelectedIndexChanged(ddlDealer, null);
                    ddlDealer.Enabled = false;
                    ddlDealerSearch.Enabled = false;
                }
                ddlDealerSearch.DataTextField = "CodeWithName"; ddlDealerSearch.DataValueField = "DID"; ddlDealerSearch.DataSource = Dealer; ddlDealerSearch.DataBind();
                if (ddlDealerSearch.Items.Count > 1)
                {
                    ddlDealerSearch.Items.Insert(0, new ListItem("Select", "0"));
                }

                BDMS_Activity oActivity = new BDMS_Activity();
                oActivity.GetActivity(ddlActivity, "IA");

                txtUnits.Attributes.Add("type", "number");
                txtFromDate.Attributes.Add("type", "date");
                txtToDate.Attributes.Add("type", "date");

                txtFromDateSearch.Text = DateTime.Now.AddDays(-1 * DateTime.Now.Day + 1).ToString("dd-MMM-yyyy");
                txtToDateSearch.Text = DateTime.Now.AddDays(-1 * DateTime.Now.Day + 1).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                txtFromDate.Attributes.Add("onblur", "document.getElementById('" + txtToDate.ClientID + "').min=this.value;");
                txtFromDate.Attributes.Add("max", "new Date()");
                txtToDate.Attributes.Add("max", "new Date()");
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            DataTable dt = oActivity.GetActivityClaimData_WOPlan(Convert.ToInt32(ddlDealerSearch.SelectedValue), txtFromDateSearch.Text, txtToDateSearch.Text, PSession.User.UserID);
            dt.Columns.Remove("AAP_ApprovalLevel");
            dt.Columns.Remove("ApprovalStatusID");
            dt.Columns.Remove("PKActualID");
            dt.Columns.Remove("AIH_PkHdrID");

            new BXcel().ExporttoExcel(dt, "Activity Data");
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();

            DataTable dt = oActivity.GetActivityClaimData_WOPlan(Convert.ToInt32(ddlDealerSearch.SelectedValue), txtFromDateSearch.Text, txtToDateSearch.Text, PSession.User.UserID);
            gvData.DataSource = dt;
            gvData.DataBind();
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
                    lnkGenerateInvoice.Attributes.Add("onclick", "window.open('ActivityInvoice.aspx?AID=" + oActivity.Encrypt(hdnActualID.Value) + "', 'newwindow', 'toolbar=no,location=no,menubar=no,width=1000,height=600,titlebar=no, fullscreen=no,resizable=yes,scrollbars=yes,top=60,left=60');return false;");
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "keHide", "$('#divEntry').toggle(1000);", true);
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
                    string FileNme = dr["AD_FileName"].ToString();
                    // byte[] bytData = ((byte[])dr["AD_AttachedFile"]);
                    byte[] bytData = null;
                    if (DBNull.Value == dr["AD_AttachedFile"])
                    {
                         bytData = new BDMS_Activity().DowloadActivityDocs(iDocID + Path.GetExtension(FileNme)).AttachedFile;
                    }
                    else
                    {
                          bytData = ((byte[])dr["AD_AttachedFile"]);
                    }
                   
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
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkEdit = sender as LinkButton;
            int ActualID = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            BDMS_Activity oActivity = new BDMS_Activity();
            DataSet ds = oActivity.GetActivityActualDataByActualID(ActualID);
            hdnActualID.Value = ActualID.ToString();
            DataTable dtHdr = ds.Tables[0];
            if (dtHdr.Rows.Count > 0)
            {
                DataRow dr = dtHdr.Rows[0];
                ddlDealer.SelectedValue = dr["AA_FKDealerID"].ToString();
                ddlActivity.SelectedValue = dr["AA_FKActivityID"].ToString();
                txtUnits.Text = dr["AA_NoofUnits"].ToString();
                txtFromDate.Text = dr["AA_FromDate"].ToString();
                txtToDate.Text = dr["AA_ToDate"].ToString();
                txtExpenses.Text = dr["AA_Expenses"].ToString();
                txtAjaxSharingA.Text = dr["Actual_AjaxSharingAmount"].ToString();
                txtDealerSharingA.Text = dr["Actual_DealerSharingAmount"].ToString();
                txtLocation.Text = dr["AA_Location"].ToString();
                txtRemarks.Text = dr["AA_Remarks"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key123", "GetActivityData('" + dr["AA_FKActivityID"].ToString() + "');", true);
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

                txtUnits.Enabled = true;
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;

                txtLocation.Enabled = true;
                txtRemarks.Enabled = true;
            }
            divEntry.Visible = true;
            divSearch.Visible = false;
        }
        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                int PKActualID = Convert.ToInt32(lnkEdit.CommandArgument);

            }
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int PKActualID = 0;
                int DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
                int ActivityID = Convert.ToInt32(ddlActivity.SelectedValue);
                int Units = Convert.ToInt32(txtUnits.Text);
                string FromDate = txtFromDate.Text;
                string ToDate = txtToDate.Text;
                string Location = txtLocation.Text;
                string Remarks = txtRemarks.Text;
                double dblExpenses = Convert.ToDouble(txtExpenses.Text);
                if (Convert.ToDateTime(ToDate) < DateTime.Now.AddMonths(-1))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('Please check ToDate!');Clear();", true);
                    return;
                }
                Session["ActDocs"] = null;

                BDMS_Activity oActivity = new BDMS_Activity();
                PKActualID = Convert.ToInt32(oActivity.SaveActivityClaim(PKActualID, DealerID, ActivityID, Units, FromDate, ToDate, Location, Remarks, PSession.User.UserID, dblExpenses));
                SaveFile(flUpload1, "Image 1");
                SaveFile(flUpload2, "Image 2");
                SaveFile(flUpload3, "Image 3");
                SaveFile(flUpload4, "Image 4");

                List<PDMS_ActivityDocs> lstDocs = Session["ActDocs"] as List<PDMS_ActivityDocs>;
                if (lstDocs != null)
                {
                    oActivity.SaveActivityAttachments_ActualID(PKActualID, lstDocs);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('Saved Successfully!');Clear();", true);
                btnExisting_Click(btnExisting, null);
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
                    if(!Directory.Exists(HttpContext.Current.Server.MapPath("~/YDMS/Temp/")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/YDMS/Temp/"));
                    }
                    if (postedFile.ContentType.ToUpper().Contains("IMAGE"))
                    {
                        BDMS_ImageCompress imgCompress = BDMS_ImageCompress.GetImageCompressObject;
                        imgCompress.GetImage = new System.Drawing.Bitmap(postedFile.InputStream);
                        imgCompress.Height = 800;
                        imgCompress.Width = 1160;
                        //imgCompress.Save(httpPostedFile.FileName, @"C:\Documents and Settings\Rasmi\My Documents\Visual Studio2008\WebSites\compressImageFile\Logo");
                        imgCompress.Save(sFileName, HttpContext.Current.Server.MapPath("~/YDMS/Temp/"), 100);
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


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lstImages.DataSource = null;
            lstImages.DataBind();
            btnSubmit.Visible = true;
            BDMS_Activity oAct = new BDMS_Activity();
            divAttach.Style.Add("display", "");
            ddlActivity.ClearSelection();
            txtUnits.Text = "";
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
            txtExpenses.Text = "";
            txtLocation.Text = "";
            txtRemarks.Text = "";
            ddlDealer.Enabled = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key123", "Clear();", true);
        }
        protected void lstImages_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkDownload = e.Item.FindControl("lnkDownload") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkDownload);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            divEntry.Visible = true;
            divSearch.Visible = false;
        }

        protected void btnExisting_Click(object sender, EventArgs e)
        {
            divEntry.Visible = false;
            divSearch.Visible = true;
        }

        protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ActivityID = Convert.ToInt32(ddlActivity.SelectedValue);
            List<PDMS_ActivityInfo> actinfoList = new BDMS_Activity().GetActivityInfoDataByID(ActivityID);
            if (actinfoList.Count > 0 && ActivityID > 0)
            {
                lblAjaxSharingA.InnerText = " (" + actinfoList[0].AjaxSharing.ToString() + ")";
                lblDealerSharingA.InnerText = " (" + (100 - Convert.ToDouble(actinfoList[0].AjaxSharing.ToString())).ToString() + ")";
                hdnAjaxSharing.Value = actinfoList[0].AjaxSharing.ToString(); //actinfoList[0].Budget* actinfoList[0].AjaxSharing/100*Convert.ToDouble("0"+txtUnits.Value);
                lblActUnits.InnerText = "No of Units(" + actinfoList[0].UnitDesc + ")";
            }
            else
            {
                lblAjaxSharingA.InnerText = "";
                lblDealerSharingA.InnerText = "";
                hdnAjaxSharing.Value = "";
                lblActUnits.InnerText = "No of Units";
            }
        }
    }
}