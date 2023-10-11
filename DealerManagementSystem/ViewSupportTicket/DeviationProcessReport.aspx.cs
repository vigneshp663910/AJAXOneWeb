using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class DeviationProcessReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSupportTicket_DeviationProcessReport; } }
        public List<PDeviationProcess> DeviationProcess
        {
            get
            {
                if (ViewState["DeviationProcess"] == null)
                {
                    ViewState["DeviationProcess"] = new List<PDeviationProcess>();
                }
                return (List<PDeviationProcess>)ViewState["DeviationProcess"];
            }
            set
            {
                ViewState["DeviationProcess"] = value;
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Deviation Process Report');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
                fillDeviationProcess();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillDeviationProcess();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillDeviationProcess()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                PApiResult Result = new BDeviationProcess().GetDeviationProcess(null, txtFileName.Text.Trim(), txtFileType.Text.Trim(), txtSubject.Text.Trim(), true, PageIndex, gvDeviationProcess.PageSize);
                DeviationProcess = JsonConvert.DeserializeObject<List<PDeviationProcess>>(JsonConvert.SerializeObject(Result.Data));
                gvDeviationProcess.PageIndex = 0;

                if (Result.RowCount == 0)
                {
                    gvDeviationProcess.DataSource = null;
                    gvDeviationProcess.DataBind();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    gvDeviationProcess.DataSource = DeviationProcess;
                    gvDeviationProcess.DataBind();

                    PageCount = (Result.RowCount + gvDeviationProcess.PageSize - 1) / gvDeviationProcess.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvDeviationProcess.PageSize) + 1) + " - " + (((PageIndex - 1) * gvDeviationProcess.PageSize) + gvDeviationProcess.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DeviationProcess", "fillDeviationProcess", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillDeviationProcess();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillDeviationProcess();
            }
        }
        protected void gvDeviationProcess_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDeviationProcess.PageIndex = e.NewPageIndex;
            fillDeviationProcess();
        }
        protected void lblDeviationProcessDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lblDeviationProcessDelete = (LinkButton)sender;
                long DeviationProcessID = Convert.ToInt64(lblDeviationProcessDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lblDeviationProcessDelete.NamingContainer);
                string lblFileName = ((Label)row.FindControl("lblFileName")).Text.Trim();
                string lblFileType = ((Label)row.FindControl("lblFileType")).Text.Trim();
                string lblSubject = ((Label)row.FindControl("lblSubject")).Text.Trim();
                string lblRemarks = ((Label)row.FindControl("lblRemarks")).Text.Trim();
                string lblRequestedByUserID = ((Label)row.FindControl("lblRequestedByUserID")).Text.Trim();
                string lblApprovedByUserID = ((Label)row.FindControl("lblApprovedByUserID")).Text.Trim();
                PDeviationProcess_Insert DP = new PDeviationProcess_Insert();
                DP.DeviationProcessID = DeviationProcessID;
                DP.RequestedBy = Convert.ToInt32(lblRequestedByUserID);
                DP.RequestedOn = DateTime.Now;
                DP.ApprovedBy = Convert.ToInt32(lblApprovedByUserID);
                DP.ApprovedOn = DateTime.Now;
                DP.FileName = lblFileName;
                DP.FileType = lblFileType;
                DP.IsActive = false;
                DP.Subject = lblSubject;
                DP.Remarks = lblRemarks;
                DP.UserID = PSession.User.UserID;

                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Deviation", DP));

                if (result.Status == PApplication.Failure)
                {
                    lblMessage.Text = result.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                lblMessage.Text = result.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                fillDeviationProcess();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            DivList.Visible = false;
            DivCreate.Visible = true;
            Clear();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            try
            {
                string Message = Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                PDeviationProcess_Insert DP = new PDeviationProcess_Insert();
                DP.RequestedBy = Convert.ToInt32(ddlRequestedBy.SelectedValue);
                DP.RequestedOn = Convert.ToDateTime(txtRequestedOn.Text.Trim());
                DP.ApprovedBy = Convert.ToInt32(ddlApprovedBy.SelectedValue);
                DP.ApprovedOn = Convert.ToDateTime(txtApprovedOn.Text.Trim());

                HttpPostedFile file = fuFileUpload.PostedFile;
                string name = file.FileName;
                int position = name.LastIndexOf("\\");
                name = name.Substring(position + 1);
                DP.FileName = name;

                int size = file.ContentLength;
                byte[] fileData = new byte[size];
                file.InputStream.Read(fileData, 0, size);
                DP.AttachedFile = fileData;
                DP.FileType = file.ContentType;
                DP.IsActive = true;
                DP.Subject = txtISubject.Text.Trim();
                DP.Remarks = txtRemarks.Text.Trim();
                DP.UserID = PSession.User.UserID;

                string result = new BAPI().ApiPut("Deviation", DP);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                fillDeviationProcess();
                Clear();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            DivList.Visible = true;
            DivCreate.Visible = false;
        }
        public string Validation()
        {
            ddlRequestedByDealer.BorderColor = Color.Silver;
            ddlApprovedByDealer.BorderColor = Color.Silver;
            ddlRequestedBy.BorderColor = Color.Silver;
            txtRequestedOn.BorderColor = Color.Silver;
            ddlApprovedBy.BorderColor = Color.Silver;
            txtApprovedOn.BorderColor = Color.Silver;
            txtISubject.BorderColor = Color.Silver;
            fuFileUpload.BorderColor = Color.Silver;
            txtRemarks.BorderColor = Color.Silver;

            if (ddlRequestedByDealer.SelectedValue == "0")
            {
                ddlRequestedByDealer.BorderColor = Color.Red;
                return "Please select the Requested By Dealer...!";
            }
            if (ddlApprovedByDealer.SelectedValue == "0")
            {
                ddlApprovedByDealer.BorderColor = Color.Red;
                return "Please select the Approved By Dealer...!";
            }
            if (ddlRequestedBy.SelectedValue == "0")
            {
                ddlRequestedBy.BorderColor = Color.Red;
                return "Please select the Requested By...!";
            }
            if (string.IsNullOrEmpty(txtRequestedOn.Text))
            {
                txtRequestedOn.BorderColor = Color.Red;
                return "Please select the Requested On...!";
            }
            if (ddlApprovedBy.SelectedValue == "0")
            {
                ddlApprovedBy.BorderColor = Color.Red;
                return "Please select the Approved By...!";
            }
            if (string.IsNullOrEmpty(txtApprovedOn.Text))
            {
                txtRequestedOn.BorderColor = Color.Red;
                return "Please select the Approved On...!";
            }
            if (string.IsNullOrEmpty(txtISubject.Text))
            {
                txtISubject.BorderColor = Color.Red;
                return "Please Enter the Subject...!";
            }
            if (fuFileUpload.PostedFile == null)
            {
                fuFileUpload.BorderColor = Color.Red;
                return "Please select the Attachment...!";
            }
            if (fuFileUpload.PostedFile.FileName.Length == 0)
            {
                fuFileUpload.BorderColor = Color.Red;
                return "Please select the Attachment...!";
            }
            return "";
        }
        void Clear()
        {
            fillDealer();
            ddlRequestedByDealer.BorderColor = Color.Silver;
            ddlApprovedByDealer.BorderColor = Color.Silver;
            ddlRequestedBy.BorderColor = Color.Silver;
            txtRequestedOn.BorderColor = Color.Silver;
            ddlApprovedBy.BorderColor = Color.Silver;
            txtApprovedOn.BorderColor = Color.Silver;
            txtISubject.BorderColor = Color.Silver;
            fuFileUpload.BorderColor = Color.Silver;
            txtRemarks.BorderColor = Color.Silver;
            ddlRequestedBy.SelectedValue = "0";
            txtRequestedOn.Text = "";
            ddlApprovedBy.SelectedValue = "0";
            txtApprovedOn.Text = "";
            txtISubject.Text = "";
            fuFileUpload = null;
            txtRemarks.Text = "";
        }
        void fillDealer()
        {
            List<PDealer> Dealer = new BDealer().GetDealerList(null, null, null);
            new DDLBind(ddlRequestedByDealer, Dealer, "DealerCode", "DID");
            new DDLBind(ddlApprovedByDealer, Dealer, "DealerCode", "DID");
            ddlRequestedByDealer_SelectedIndexChanged(null, null);
            ddlApprovedByDealer_SelectedIndexChanged(null, null);
        }
        protected void ddlRequestedByDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlRequestedByDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlRequestedBy, DealerUser, "ContactName", "UserID");
        }
        protected void ddlApprovedByDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlApprovedByDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlApprovedBy, DealerUser, "ContactName", "UserID");
        }
        protected void lnkFileName_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkFileName = (LinkButton)sender;
                long DeviationProcessID = Convert.ToInt64(lnkFileName.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkFileName.NamingContainer);
                string lblFileName = ((Label)row.FindControl("lblFileName")).Text.Trim();
                string lblFileType = ((Label)row.FindControl("lblFileType")).Text.Trim();
                PDeviationProcess_Insert DP = new PDeviationProcess_Insert();
                DP.DeviationProcessID = DeviationProcessID;
                DP.FileName = lblFileName;
                DP.FileType = lblFileType;
                FileDownload(DP);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void FileDownload(PDeviationProcess_Insert DP)
        {
            try
            {
                Response.AddHeader("Content-type", DP.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + DP.FileName);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                PAttachedFile Files = new BDeviationProcess().GetAttachedFileDownload(DP.DeviationProcessID + Path.GetExtension(DP.FileName));
                Response.BinaryWrite(Files.AttachedFile);
                // Append cookie
                HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                cookie.Value = "Flag";
                cookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Current.Response.AppendCookie(cookie);
                // end 
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

            }
        }
    }
}