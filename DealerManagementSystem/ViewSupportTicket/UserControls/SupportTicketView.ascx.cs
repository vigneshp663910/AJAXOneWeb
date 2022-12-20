using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SapIntegration;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.IO;
using System.Configuration;

namespace DealerManagementSystem.ViewSupportTicket.UserControls
{
    public partial class SupportTicketView : System.Web.UI.UserControl
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        private List<PAttachedFile> AttchedFile
        {
            get
            {
                if (ViewState["AttchedFile"] == null)
                {
                    ViewState["AttchedFile"] = new List<PAttachedFile>();
                }
                return (List<PAttachedFile>)ViewState["AttchedFile"];
            }
        }
        public List<PTicketHeader> Ticket
        {
            get
            {
                if (Session["TaskReportView"] == null)
                {
                    Session["TaskReportView"] = new List<PTicketHeader>();
                }
                return (List<PTicketHeader>)Session["TaskReportView"];
            }
            set
            {
                Session["TaskReportView"] = value;
            }
        }
        private string Action;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Task Report View');</script>");
            if (!IsPostBack)
            {
                //new BEmployees().CheckPermition(0);
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["TicketNo"])))
                {
                    int TicketNo = Convert.ToInt32(ViewState["TicketNo"]);
                    FillTickets(TicketNo);
                    FillChat(TicketNo);
                    FillChatTemp(TicketNo);
                }

                btnSend.Focus();
            }

            if (IsPostBack && FileUpload.PostedFile != null)
            {
                if (FileUpload.PostedFile.FileName.Length > 0)
                {
                    HttpPostedFile file = FileUpload.PostedFile;
                    PAttachedFile F = new PAttachedFile();
                    int size = file.ContentLength;
                    string name = file.FileName;
                    int position = name.LastIndexOf("\\");
                    name = name.Substring(position + 1);

                    byte[] fileData = new byte[size];
                    file.InputStream.Read(fileData, 0, size);

                    F.FileName = name;
                    F.AttachedFile = fileData;
                    F.FileType = file.ContentType;
                    F.FileSize = size;
                    F.AttachedFileID = 0;
                    AttchedFile.Add(F);
                }
                btnSend.Focus();
            }
            if (IsPostBack && fu.PostedFile != null)
            {
                if (fu.PostedFile.FileName.Length > 0)
                {
                    if (fu.PostedFile.FileName.Length == 0)
                    {
                        lblMessage.Text = "Please select the file";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    foreach (PAttachedFile file1 in AttchedFile)
                    {
                        if (file1.FileName == fu.PostedFile.FileName)
                        {
                            lblMessage.Text = "File Name already available";
                            lblMessage.ForeColor = Color.Red;
                            return;
                        }
                    }

                    HttpPostedFile file = fu.PostedFile;
                    PAttachedFile F = new PAttachedFile();
                    int size = file.ContentLength;
                    string name = file.FileName;
                    int position = name.LastIndexOf("\\");
                    name = name.Substring(position + 1);

                    byte[] fileData = new byte[size];
                    file.InputStream.Read(fileData, 0, size);

                    F.FileName = name;
                    F.AttachedFile = fileData;
                    F.FileType = file.ContentType;
                    F.FileSize = size;
                    F.AttachedFileID = 0;
                    AttchedFile.Add(F);
                    gvNewFileAttached.DataSource = AttchedFile;
                    gvNewFileAttached.DataBind();
                    MPE_AssignTo.Show();
                }
            }
            if (IsPostBack && fuResolve.PostedFile != null)
            {
                if (fuResolve.PostedFile.FileName.Length > 0)
                {
                    foreach (PAttachedFile file1 in AttchedFile)
                    {
                        if (file1.FileName == fuResolve.PostedFile.FileName)
                        {
                            lblMessage.Text = "File Name already available";
                            lblMessage.ForeColor = Color.Red;
                            return;
                        }
                    }
                    HttpPostedFile file = fuResolve.PostedFile;
                    PAttachedFile F = new PAttachedFile();
                    int size = file.ContentLength;
                    string name = file.FileName;
                    int position = name.LastIndexOf("\\");
                    name = name.Substring(position + 1);

                    byte[] fileData = new byte[size];
                    file.InputStream.Read(fileData, 0, size);

                    F.FileName = name;
                    F.AttachedFile = fileData;
                    F.FileType = file.ContentType;
                    F.FileSize = size;
                    F.AttachedFileID = 0;

                    AttchedFile.Add(F);

                    gvResolveNewFileAttached.DataSource = AttchedFile;
                    gvResolveNewFileAttached.DataBind();
                    MPE_Resolve.Show();
                }
            }
        }
        void FillCategory()
        {
            ddlCategory.DataTextField = "Category";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataSource = new BTicketCategory().getTicketCategory(null, null);
            ddlCategory.DataBind();
        }
        void FillSubCategory()
        {
            ddlSubcategory.DataTextField = "SubCategory";
            ddlSubcategory.DataValueField = "SubCategoryID";
            ddlSubcategory.DataSource = new BTicketSubCategory().getTicketSubCategory(null, null, Convert.ToInt32(ddlCategory.SelectedValue));
            ddlSubcategory.DataBind();
            ddlSubcategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillTicketSeverity()
        {
            ddlSeverity.DataTextField = "Severity";
            ddlSeverity.DataValueField = "SeverityID";
            ddlSeverity.DataSource = new BTicketSeverity().getTicketSeverity(null, null);
            ddlSeverity.DataBind();
            ddlSeverity.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillTicketType()
        {
            ddlTicketType.DataTextField = "Type";
            ddlTicketType.DataValueField = "TypeID";
            ddlTicketType.DataSource = new BTicketType().getTicketType(null, null);
            ddlTicketType.DataBind();
        }
        void FillAssignTo()
        {
            ddlAssignedTo.DataTextField = "ContactName";
            ddlAssignedTo.DataValueField = "UserID";
            List<PUser> user = new BUser().GetAllUsers();
            ddlAssignedTo.DataSource = user;
            ddlAssignedTo.DataBind();
            ddlAssignedTo.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillApproval()
        {
            List<PUser> user = new BUser().GetAllUsers(null, null);
            //user.Where(M => M.SystemCategoryID == (short)SystemCategory.AF || M.SystemCategoryID == (short)SystemCategory.Support).ToList();
            ddlapprovar.DataTextField = "ContactName";
            ddlapprovar.DataValueField = "UserID";
            ddlapprovar.DataSource = user;//.Where(M => M.SystemCategoryID == (short)SystemCategory.AF).ToList();
            ddlapprovar.DataBind();
            ddlapprovar.Items.Insert(0, new ListItem("Select", "0"));
        }
        void ClearField()
        {

            //  txtDepartment.Text = string.Empty;
            //  ddlTicketType.SelectedValue = "1";
            // ddlCategory.SelectedValue = "1";
            txtAssignerRemark.Text = string.Empty;
            //  BulletedList1.Items.Clear();
        }
        Boolean validatetion()
        {
            decimal parsedValue;
            if (btnAssign.Text == "Assign")
            {
                if (ddlCategory.SelectedValue == "0")
                {
                    lblMessageAssignTo.Text = "Please Select The Category";
                    return false;
                }
                if (ddlSubcategory.SelectedValue == "0")
                {
                    lblMessageAssignTo.Text = "Please Select The Subcategory";
                    return false;
                }
                if (ddlSeverity.SelectedValue == "0")
                {
                    lblMessageAssignTo.Text = "Please Select The Severity";
                    return false;
                }
                if (ddlAssignedTo.SelectedValue == "0")
                {
                    lblMessageAssignTo.Text = "Please Select The Assigned To";
                    return false;
                }
            }
            return true;
        }
        public void FillTickets(int? TicketNO)
        {
            ViewState["TicketNo"] = TicketNO;
            Ticket = new BTickets().GetTicketByID(TicketNO);

            ddlMailNotification.Items.Clear();
            ddlMailNotification.Items.Insert(ddlMailNotification.Items.Count, new ListItem("Select", "0"));
            ddlMailNotification.Items.Insert(ddlMailNotification.Items.Count, new ListItem("Select All", "-1"));
            ddlMailNotification.Items.Insert(ddlMailNotification.Items.Count, new ListItem(Ticket[0].CreatedBy.ContactName, Ticket[0].CreatedBy.UserID.ToString()));


            foreach (PTicketItem T in Ticket[0].TicketItems)
            {
                if (ddlMailNotification.Items.FindByValue(T.AssignedTo.UserID.ToString()) == null)
                {
                    ddlMailNotification.Items.Insert(ddlMailNotification.Items.Count, new ListItem(T.AssignedTo.ContactName, T.AssignedTo.UserID.ToString()));
                }
                if (ddlMailNotification.Items.FindByValue(T.AssignedBy.UserID.ToString()) == null)
                {
                    ddlMailNotification.Items.Insert(ddlMailNotification.Items.Count, new ListItem(T.AssignedBy.ContactName, T.AssignedBy.UserID.ToString()));
                }
            }

            foreach (PTicketsApprovalDetails A in Ticket[0].ApprovalDetails)
            {
                if (ddlMailNotification.Items.FindByValue(A.Approver.UserID.ToString()) == null)
                {
                    ddlMailNotification.Items.Insert(ddlMailNotification.Items.Count, new ListItem(A.Approver.ContactName, A.Approver.UserID.ToString()));
                }
            }

            lblTicketID.Text = Ticket[0].HeaderID.ToString();
            lblCategory.Text = Ticket[0].Category.Category;
            lblSubCategory.Text = (Ticket[0].SubCategory == null) ? "" : Ticket[0].SubCategory.SubCategory;
            lblSeverity.Text = (Ticket[0].Severity == null) ? "" : Ticket[0].Severity.Severity;
            lblTicketType.Text = Ticket[0].Type.Type;
            lblDescription.Text = Ticket[0].Description;
            lblStatus.Text = Ticket[0].Status.Status;
            lblCreatedBy.Text = Ticket[0].CreatedBy.ContactName;
            lblCreatedByContactNumber.Text = Ticket[0].CreatedBy.ContactNumber;
            lblCreatedOn.Text = Ticket[0].CreatedOn.ToString();
            lblAge.Text = Ticket[0].age.ToString();
            lblClosedOn.Text = Ticket[0].ClosedOn.ToString();
            //gvTickets.DataSource = Ticket;
            //gvTickets.DataBind();

            gvTicketItem.DataSource = Ticket[0].TicketItems;
            gvTicketItem.DataBind();

            gvApprover.DataSource = Ticket[0].ApprovalDetails;
            gvApprover.DataBind();

            ActionControlMange();
        }
        public void FillChat(int TicketNO)
        {
            List<PMessage> PMessages = new List<PMessage>();
            PMessage Message = null;

            long LastMessageID = 0;
            List<PForum> Forums = new BTickets().GetForumDetails(TicketNO);
            foreach (PForum F in Forums)
            {
                Message = new PMessage();
                Message.MessageVisible = false;
                Message.FileTypeVisible = true;
                Message.ID = F.ID;

                if (F.FromUser.UserID == PSession.User.UserID)
                {
                    Message.Name = "<p style = 'text-align: right'><U>" + F.FromUser.ContactName + " </U>  (" + F.CreatedOn + ")</p>";
                    Message.Message = "<p style = 'padding-left : 10px; text-align: right'>" + F.Message + "</p>";
                    Message.CssClass = "Right";

                }
                else
                {
                    Message.Name = "<p><U>" + F.FromUser.ContactName + " </U> (" + F.CreatedOn + ")</p>";
                    Message.Message = "<p style = 'padding-left : 10px'>" + F.Message + "</p>";
                    Message.CssClass = "Left";
                }
                if (F.FileTypeID != (short)FileType.Message)
                {
                    Message.Message = F.Message;
                }

                if (F.FileTypeID == (short)FileType.Unknown)
                {
                    Message.FileType = "~/FileFormat/Unknown_Icon.jpg";
                }
                else if (F.FileTypeID == (short)FileType.Message)
                {
                    Message.MessageVisible = true;
                    Message.FileTypeVisible = false;
                }
                else if (F.FileTypeID == (short)FileType.Word)
                {
                    Message.FileType = "~/FileFormat/Word_Icon.jpg";
                }
                else if (F.FileTypeID == (short)FileType.RAR)
                {
                    Message.FileType = "~/FileFormat/RAR_Icon.jpg";
                }

                else if (F.FileTypeID == (short)FileType.Excel)
                {
                    Message.FileType = "~/FileFormat/Excel_Icon.jpg";
                }
                else if (F.FileTypeID == (short)FileType.MSG)
                {
                    Message.FileType = "~/FileFormat/MSG_Icon.jpg";
                }
                else if (F.FileTypeID == (short)FileType.Pdf)
                {
                    Message.FileType = "~/FileFormat/Pdf_Icon.jpg";
                }
                else if (F.FileTypeID == (short)FileType.XML)
                {
                    Message.FileType = "~/FileFormat/XML_Icon.jpg";
                }
                else if (F.FileTypeID == (short)FileType.zipped)
                {
                    Message.FileType = "~/FileFormat/zipped_Icon.jpg";
                }
                else if (F.FileTypeID == (short)FileType.Jpeg)
                {
                    Message.FileType = "~/FileFormat/Jpeg_Icon.jpg";
                }
                else if (F.FileTypeID == (short)FileType.Png)
                {
                    Message.FileType = "~/FileFormat/Png_Icon.jpg";
                }

                PMessages.Add(Message);
                LastMessageID = F.ID;
            }
            gvchar.DataSource = PMessages;
            gvchar.DataBind();
            if (LastMessageID != 0)
                new BForum().UdateMessageViewStatus(TicketNO, PSession.User.UserID, LastMessageID);
        }
        public void FillChatTemp(int TicketNO)
        {
            List<PMessageTemp> PMessages = new List<PMessageTemp>();
            PMessageTemp Message = null;

            long LastMessageID = 0;
            List<PForum> Forums = new BTickets().GetForumDetails(TicketNO);
            foreach (PForum F in Forums)
            {
                Message = new PMessageTemp();

                Message.FileTypeVisible = true;
                Message.ID = F.ID;

                if (F.FromUser.UserID == PSession.User.UserID)
                {
                    Message.Receiver = F.FromUser.ContactName;
                    Message.ReceiverTime = Convert.ToString(F.CreatedOn);

                    Message.CssClass = "Right";
                }
                else
                {
                    Message.Sender = F.FromUser.ContactName;
                    Message.SenderTime = Convert.ToString(F.CreatedOn);

                    Message.CssClass = "Left";
                }

                if (F.FileTypeID == (short)FileType.Message)
                {
                    Message.MessageVisible = false;
                    Message.Message = F.Message;
                }
                else
                {
                    Message.FileName = F.Message;
                    Message.FileType = "~/FileFormat/Unknown_Icon.jpg";
                    Message.MessageVisible = true;
                }
                PMessages.Add(Message);
                LastMessageID = F.ID;
            }
            //GridView1.DataSource = PMessages;
            //GridView1.DataBind();
            if (LastMessageID != 0)
                new BForum().UdateMessageViewStatus(TicketNO, PSession.User.UserID, LastMessageID);
        }

        void FillAllFields(int TicketNo)
        {
            txtRequestedOn.Text = Convert.ToString(Ticket[0].CreatedOn);
            ddlCategory.SelectedValue = Convert.ToString(Ticket[0].Category.CategoryID);
            FillSubCategory();

            if (Ticket[0].SubCategory != null)
            {
                ddlSubcategory.SelectedValue = Convert.ToString(Ticket[0].SubCategory.SubCategoryID);
            }

            if (Ticket[0].Severity != null)
            {
                ddlCategory.Enabled = false;
                ddlSubcategory.Enabled = false;
                ddlSeverity.Enabled = false;
            }

            if (Ticket[0].Severity != null)
            {
                ddlSeverity.SelectedValue = Convert.ToString(Ticket[0].Severity.SeverityID);
            }
            txtRequestedBy.Text = Ticket[0].CreatedBy.ContactName + " " + txtRequestedOn.Text;
            ddlTicketType.SelectedValue = Convert.ToString(Ticket[0].Type.TypeID);
            
            ddlStatus.SelectedValue = Convert.ToString(Ticket[0].Status.StatusID);
            txtRequesterRemark.Text = Ticket[0].Description;

            Dictionary<string, long> AttachedFiles = new Dictionary<string, long>(); // new BTickets().getAttachedFiles(TicketNo);
            List<PForum> Forums = new BTickets().GetForumDetails(TicketNo);
            foreach (PForum F in Forums)
            {
                if (F.FileTypeID != (short)FileType.Message)
                {
                    AttachedFiles.Add(F.Message, F.ID);
                }
            }
            List<ListItem> files = new List<ListItem>();
            foreach (KeyValuePair<string, long> Files in AttachedFiles)
            {
                files.Add(new ListItem(Files.Key, Files.Value + "." + Files.Key.Split('.')[Files.Key.Split('.').Count() - 1]));
            }
            gvFileAttached.DataSource = files;
            gvFileAttached.DataBind();
            ViewState["RequestedBy"] = Ticket[0].CreatedBy.UserName;
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Message")
            {
                txtMessage.Text = "";
                txtMessage.Visible = true;
                divMailNotification.Visible = true;
                FileUpload.Visible = false;
                btnSend.Text = "Send";
                btnSend.Visible = true;
                btnSend.Focus();

                MPE_Conversation.Show();
                Label2.Text = "Message";
            }
            else if (lbActions.Text == "Upload File")
            {
                txtMessage.Text = "";
                txtMessage.Visible = false;
                FileUpload.Visible = true;
                divMailNotification.Visible = true;
                btnSend.Text = "Upload File";
                btnSend.Visible = true;
                btnSend.Focus();

                MPE_Conversation.Show();
                Label2.Text = "Upload";
            }
            else if (lbActions.Text == "Request For Approval")
            {
                FillApproval();
                btnSendForApproval.Visible = true;
                MPE_SendApproval.Show();
            }
            else if (lbActions.Text == "Approve")
            {
                txtMessage.Text = "";
                txtMessage.Visible = true;
                FileUpload.Visible = false;
                divMailNotification.Visible = false;
                btnSend.Text = "Send";
                btnSend.Focus();
                MPE_Conversation.Show();
                Label2.Text = "Approve";
            }
            else if (lbActions.Text == "Assign To")
            {
                btnAssign.Visible = true;
                FillCategory();
                FillSubCategory();
                FillTicketSeverity();
                FillTicketType();
                FillAssignTo();
                FillAllFields(Convert.ToInt32(ViewState["TicketNo"]));
                MPE_AssignTo.Show();
            }
            else if (lbActions.Text == "In Progress")
            {
                InProgress();
            }
            else if (lbActions.Text == "Resolve")
            {
                FillResolutionType();
                MPE_Resolve.Show();
            }
            else if (lbActions.Text == "Cancel")
            {
                Cancel();
            }
            else if (lbActions.Text == "Close")
            {
                Close();
            }
            else if (lbActions.Text == "Force Close")
            {
                txtMessage.Text = "";
                txtMessage.Visible = true;
                FileUpload.Visible = false;
                divMailNotification.Visible = false;
                btnSend.Text = "Send";
                btnSend.Focus();
                MPE_Conversation.Show();
                Label2.Text = "Force Close";
            }
            tpnlTicketHistory.Visible = true;
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            PForum_Insert Forum = new PForum_Insert();
            Forum.HeaderID = Convert.ToInt32(ViewState["TicketNo"]);

            if (txtMessage.Visible == true)
            {
                if (string.IsNullOrEmpty(txtMessage.Text.Trim()))
                {
                    lblMessageConversation.Text = "Please enter the message.";
                    lblMessageConversation.ForeColor = Color.Red;
                    lblMessageConversation.Visible = true;
                    MPE_Conversation.Show();
                    return;
                }
                Forum.Message = txtMessage.Text;
                txtMessage.Text = string.Empty;
            }
            else
            {
                if (AttchedFile.Count == 1)
                {
                    Forum.Message = AttchedFile[0].FileName;
                    Forum.AttchedFile = AttchedFile[0];
                }
                else
                {
                    return;
                }
            }

            btnSend.Focus();

            string result = new BAPI().ApiPut("Task/Forum", Forum);
            if (Label2.Text == "Force Close")
            {
                new BTickets().UpdateTicketForceClosedStatus(Forum.HeaderID, PSession.User.UserID);
            }
            if (Label2.Text == "Approve")
            {
                new BTickets().UpdateTicketStatusApprove(Convert.ToInt32(ViewState["TicketNo"]), PSession.User.UserID, Forum.Message);
            }

            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);
            //result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data);

            if (Result.Status == PApplication.Failure)
            {
                lblMessageConversation.Text = Result.Message;
                lblMessageConversation.Visible = true;
                lblMessageConversation.ForeColor = Color.Red;
                MPE_Conversation.Show();
                return;
            }
            if (Label2.Text == "Message" || Label2.Text == "Upload")
            {
                if (ddlMailNotification.SelectedValue != "0")
                {
                    if (ddlMailNotification.SelectedValue == "-1")
                    {
                        var items = ddlMailNotification.Items;
                        foreach (var item in items.Cast<ListItem>().Where(x => x.Value != "-1" && x.Value != "0"))
                        {
                            PUser UserApproval = new BUser().GetUserDetails(Convert.ToInt32(item.Value));
                            string messageBody = messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/Forum.htm");

                            messageBody = messageBody.Replace("@@TicketNo", Forum.HeaderID.ToString());
                            messageBody = messageBody.Replace("@@ToName", UserApproval.ContactName);
                            messageBody = messageBody.Replace("@@fromName", PSession.User.ContactName);
                            messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"] + "ViewSupportTicket/ManageSupportTicket.aspx");
                            new EmailManager().MailSend(UserApproval.Mail, "New Ticket " + Forum.HeaderID.ToString(), messageBody, Forum.HeaderID);
                        }
                    }
                    else
                    {
                        PUser UserApproval = new BUser().GetUserDetails(Convert.ToInt32(ddlMailNotification.SelectedValue));
                        string messageBody = messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/Forum.htm");

                        messageBody = messageBody.Replace("@@TicketNo", Forum.HeaderID.ToString());
                        messageBody = messageBody.Replace("@@ToName", UserApproval.ContactName);
                        messageBody = messageBody.Replace("@@fromName", PSession.User.ContactName);
                        messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"] + "ViewSupportTicket/ManageSupportTicket.aspx");
                        new EmailManager().MailSend(UserApproval.Mail, "New Ticket " + Forum.HeaderID.ToString(), messageBody, Forum.HeaderID);
                    }
                }
            }

            FillTickets(Forum.HeaderID);
            FillChatTemp(Forum.HeaderID);
            FillChat(Forum.HeaderID);
            
            if (AttchedFile.Count > 0)
                AttchedFile.RemoveAt(0);
        }
        void Download(int Index)
        {
            LinkButton lnkDownload = (LinkButton)gvchar.Rows[Index].FindControl("lnkDownload");
            Label lblID = (Label)gvchar.Rows[Index].FindControl("lblID");
            string Formate = lnkDownload.Text.Split('.')[lnkDownload.Text.Split('.').Count() - 1];
            PAttachedFile UploadedFile = new BTickets().GetAttachedFileTaskForDownload(lblID.Text + "." + Formate);
            Response.AddHeader("Content-type", Formate);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lnkDownload.Text);
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            Response.BinaryWrite(UploadedFile.AttachedFile);
            Response.Flush();
            Response.End();
        }
        protected void ibDownload_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton g = (ImageButton)sender;

            Download(((GridViewRow)g.NamingContainer).RowIndex);
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            LinkButton g = (LinkButton)sender;
            Download(((GridViewRow)g.NamingContainer).RowIndex);
        }
        protected void ibReply_Click(object sender, ImageClickEventArgs e)
        {
            txtMessage.Text = "";

            txtMessage.Visible = true;
            FileUpload.Visible = false;
            btnSend.Text = "Send";
            btnSend.Focus();
            btnSend.Visible = true;
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubCategory();
            MPE_AssignTo.Show();
        }
        protected void DownloadFile(object sender, EventArgs e)
        {

            try
            {
                // LinkButton lnkDownload = (LinkButton)sender;
                //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

                string filePath = (sender as LinkButton).CommandArgument;
                string fileName = (sender as LinkButton).Text;
                PAttachedFile UploadedFile = new BTickets().GetAttachedFileTaskForDownload(filePath);

                Response.AddHeader("Content-type", filePath.Split('.')[1]);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Response.End();
            }

            //Response.ContentType = ContentType;
            ////Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            //Response.WriteFile(filePath);
            //Response.End();
        }
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            if (!validatetion())
            {
                lblMessageAssignTo.ForeColor = Color.Red;
                lblMessageAssignTo.Visible = true;
                MPE_AssignTo.Show();
                return;
            }

            PTaskItem_Insert TaskItem = new PTaskItem_Insert();
            TaskItem.HeaderID = Convert.ToInt32(ViewState["TicketNo"]);
            TaskItem.SubCategoryID = Convert.ToInt32(ddlSubcategory.SelectedValue);
            TaskItem.SeverityID = Convert.ToInt32(ddlSeverity.SelectedValue);
            TaskItem.AssignerRemark = txtAssignerRemark.Text.Trim();
            TaskItem.AssignedTo = Convert.ToInt32(ddlAssignedTo.SelectedValue);

            TaskItem.ActualDuration = 0;
            TaskItem.SupportType = ddlSupportType.SelectedValue;
            TaskItem.AttchedFile = AttchedFile;

            bool containsItem = Ticket[0].TicketItems.Any(item => item.HeaderID == TaskItem.HeaderID && item.AssignedTo.UserID == Convert.ToInt32(ddlAssignedTo.SelectedValue) && ((item.ItemStatus.StatusID == 2) || (item.ItemStatus.StatusID == 3) || (item.ItemStatus.StatusID == 4) || (item.ItemStatus.StatusID == 6) || (item.ItemStatus.StatusID == 8) || (item.ItemStatus.StatusID == 11)));
            if (containsItem)
            {
                lblMessageAssignTo.Text = "This Ticket is already found To " + ddlAssignedTo.SelectedItem.Text + "...!";
                lblMessageAssignTo.ForeColor = Color.Red;
                lblMessageAssignTo.Visible = true;
                MPE_AssignTo.Show();
                return;
            }
            string result = new BAPI().ApiPut("Task/TaskItem", TaskItem);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessageAssignTo.Text = Result.Message;
                lblMessageAssignTo.ForeColor = Color.Red;
                lblMessageAssignTo.Visible = true;
                MPE_AssignTo.Show();
                return;
            }

            lblMessage.Text = "Ticket No " + TaskItem.HeaderID + " is successfully updated.";
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;

            int TicketNo = Convert.ToInt32(ViewState["TicketNo"]);
            FillTickets(TicketNo);
            FillChat(TicketNo);
            FillChatTemp(TicketNo);

            string messageBody = "";
            string Subject = "New Ticket " + TaskItem.HeaderID;

            PUser userAssignedTo = new BUser().GetUserDetails(Convert.ToInt32(ddlAssignedTo.SelectedValue));

            messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/TicketAssign.htm");
            messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"] + "ViewSupportTicket/AssignedSupportTicket.aspx");
            messageBody = messageBody.Replace("@@TicketNo", TaskItem.HeaderID.ToString());
            messageBody = messageBody.Replace("@@ToName", userAssignedTo.ContactName);
            messageBody = messageBody.Replace("@@RequestedOn", txtRequestedOn.Text);
            messageBody = messageBody.Replace("@@Category", ddlCategory.SelectedItem.Text);
            messageBody = messageBody.Replace("@@Subcategory", ddlSubcategory.SelectedItem.Text);
            messageBody = messageBody.Replace("@@Severity", ddlSeverity.SelectedItem.Text);
            messageBody = messageBody.Replace("@@TicketType", ddlTicketType.SelectedItem.Text);
            messageBody = messageBody.Replace("@@Description", txtRequesterRemark.Text);
            messageBody = messageBody.Replace("@@Justification", txtAssignerRemark.Text);
            messageBody = messageBody.Replace("@@fromName", PSession.User.ContactName);
            new EmailManager().MailSend(userAssignedTo.Mail, Subject, messageBody, TaskItem.HeaderID);
            ClearField();
            btnAssign.Visible = false;
            MPE_AssignTo.Hide();
        }
        protected void Remove_Click(object sender, EventArgs e)
        {
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
            Label file = (Label)Grow.FindControl("lbltest");
            string fileName = file.Text;

            AttchedFile.RemoveAll(x => x.FileName == file.Text);

            gvNewFileAttached.DataSource = AttchedFile;
            gvNewFileAttached.DataBind();
            MPE_AssignTo.Show();
        }
        protected void btnSendForApproval_Click(object sender, EventArgs e)
        {
            if (ddlapprovar.SelectedValue == "0")
            {
                lblSendApproval.Text = "Please Select Approver.";
                lblSendApproval.ForeColor = Color.Red;
                lblSendApproval.Visible = true;
                MPE_SendApproval.Show();
                return;
            }
            bool containsItem = Ticket[0].ApprovalDetails.Any(item => item.Approver.ContactName == ddlapprovar.SelectedItem.Text.Trim());
            if (containsItem)
            {
                lblSendApproval.Text = "This Ticket is already sent for Approval To " + ddlapprovar.SelectedItem.Text + "...!";
                lblSendApproval.ForeColor = Color.Red;
                lblSendApproval.Visible = true;
                MPE_SendApproval.Show();
                return;
            }

            int? Approver = null;
            if (ddlapprovar.SelectedValue != "0")
            {
                Approver = Convert.ToInt32(ddlapprovar.SelectedValue);
            }
            int success = new BTickets().insertTicketApprovalDetails(PSession.User.UserID, Convert.ToInt32(ViewState["TicketNo"]), Approver);
            if (success == 0)
            {
                lblSendApproval.Text = "Ticket No " + Convert.ToInt32(ViewState["TicketNo"]) + "  is not successfully updated.";
                lblSendApproval.ForeColor = Color.Red;
                lblSendApproval.Visible = true;
                MPE_SendApproval.Show();
                return;
            }
            else
            {
                lblMessage.Text = "Ticket No " + Convert.ToInt32(ViewState["TicketNo"]) + " is successfully updated.";
                btnSendForApproval.Visible = false;
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
                int TicketNo = Convert.ToInt32(ViewState["TicketNo"]);
                FillTickets(TicketNo);
                FillChat(TicketNo);
                FillChatTemp(TicketNo);
                PUser UserApproval = new BUser().GetUsers(Convert.ToInt32(ddlapprovar.SelectedValue), "", null, "", null, null, null, null, null)[0];
                //  PEmployee EmployeeApproval = new BEmployees().GetEmployeeListJohn(Convert.ToInt32(ddlapprovar.SelectedValue), null, "", "", "")[0];

                string messageBody = messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/TicketRequestForApproval.htm");

                messageBody = messageBody.Replace("@@RequestedBy", PSession.User.ContactName);
                messageBody = messageBody.Replace("@@TicketNo", Convert.ToInt32(ViewState["TicketNo"]).ToString());
                messageBody = messageBody.Replace("@@ToName", UserApproval.ContactName);
                messageBody = messageBody.Replace("@@TicketType", Ticket[0].Type.Type);
                messageBody = messageBody.Replace("@@Category", Ticket[0].Category.Category);
                messageBody = messageBody.Replace("@@Description", Ticket[0].Description);
                messageBody = messageBody.Replace("@@fromName", PSession.User.ContactName);

                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"] + "ViewSupportTicket/ManageSupportTicket.aspx");
                new EmailManager().MailSend(UserApproval.Mail, "New Ticket " + Convert.ToInt32(ViewState["TicketNo"]).ToString(), messageBody, Convert.ToInt32(ViewState["TicketNo"]));
            }
        }
        void InProgress()
        {
            PTaskItem_Insert TaskItem = new PTaskItem_Insert();

            foreach (PTicketItem ticketItem in Ticket[0].TicketItems)
            {
                if (ticketItem.ItemStatus.Status == "Assigned" && ticketItem.AssignedTo.UserID == PSession.User.UserID)
                {
                    TaskItem.HeaderID = ticketItem.HeaderID;
                    TaskItem.ItemID = ticketItem.ItemID;
                }
            }
            int Success = new BTickets().UpdateTicketStatus(TaskItem.ItemID, 3);

            if (Success != 0)
            {
                lblMessage.Text = "Ticket No " + TaskItem.HeaderID + " is successfully updated.";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
                int TicketNo = Convert.ToInt32(ViewState["TicketNo"]);
                FillTickets(TicketNo);
                FillChat(TicketNo);
                FillChatTemp(TicketNo);
            }
        }
        void Cancel()
        {
            PTaskItem_Insert TaskItem = new PTaskItem_Insert();
            foreach (PTicketItem ticketItem in Ticket[0].TicketItems)
            {
                if (ticketItem.ItemStatus.Status == "Assigned" && ticketItem.AssignedTo.UserID == PSession.User.UserID)
                {
                    TaskItem.HeaderID = ticketItem.HeaderID;
                    TaskItem.ItemID = ticketItem.ItemID;
                }
            }
            int Success = new BTickets().UpdateTicketCancelStatus(TaskItem.HeaderID, TaskItem.ItemID, PSession.User.UserID);
            if (Success != 0)
            {
                lblMessage.Text = "Ticket No " + TaskItem.HeaderID + " is successfully updated.";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
                int TicketNo = Convert.ToInt32(ViewState["TicketNo"]);
                FillTickets(TicketNo);
                FillChat(TicketNo);
                FillChatTemp(TicketNo);
            }
        }
        void Close()
        {
            if (Ticket[0].Status.StatusID == 4)
            {
                int Success = new BTickets().UpdateTicketClosedStatus(Ticket[0].HeaderID, PSession.User.UserID);
                if (Success != 0)
                {
                    lblMessage.Text = "Ticket No " + Ticket[0].HeaderID + " is successfully updated.";
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Visible = true;
                    int TicketNo = Convert.ToInt32(ViewState["TicketNo"]);
                    FillTickets(TicketNo);
                    FillChat(TicketNo);
                    FillChatTemp(TicketNo);
                }
            }
        }
        protected void btnResolve_Click(object sender, EventArgs e)
        {
            if (!validationResolve())
            {
                lblResolve.ForeColor = Color.Red;
                lblResolve.Visible = true;
                return;
            }
            PTaskItem_Insert TaskItem = new PTaskItem_Insert();
            foreach (PTicketItem ticketItem in Ticket[0].TicketItems)
            {
                if (ticketItem.ItemStatus.Status == "In Progress" && ticketItem.AssignedTo.UserID == PSession.User.UserID)
                {
                    TaskItem.ItemID = ticketItem.ItemID;
                    TaskItem.HeaderID = ticketItem.HeaderID;
                    TaskItem.SubCategoryID = Ticket[0].SubCategory.SubCategoryID;
                }
            }
            TaskItem.Effort = Convert.ToDecimal("0" + txtEffort.Text);
            TaskItem.ResolutionType = Convert.ToInt32(ddlResolutionType.SelectedValue) == 0 ? (int?)null : Convert.ToInt32(ddlResolutionType.SelectedValue);
            TaskItem.Resolution = txtResolution.Text;
            TaskItem.SupportType = ddlResSupportType.SelectedValue;

            string result = new BAPI().ApiPut("Task/UpdateTicketResolvedStatus", TaskItem);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }


            lblMessage.Text = "Ticket No " + TaskItem.HeaderID + " is successfully updated.";
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;

            int TicketNo = Convert.ToInt32(ViewState["TicketNo"]);
            FillTickets(TicketNo);
            FillChat(TicketNo);
            FillChatTemp(TicketNo);

            if (Ticket[0].Status.StatusID == 4)
            {
                string messageBody = "";
                PTicketHeader Tickets = new BTickets().GetTicketByID(TaskItem.HeaderID)[0];
                messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/TicketResolved.htm");
                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"] + "ViewSupportTicket/ClosedSupportTicket.aspx");
                messageBody = messageBody.Replace("@@TicketNo", TaskItem.HeaderID.ToString());
                messageBody = messageBody.Replace("@@RequestedOn", Tickets.CreatedOn.ToString());
                messageBody = messageBody.Replace("@@Category", Tickets.Category.Category);
                messageBody = messageBody.Replace("@@Resolution", TaskItem.Resolution);
                messageBody = messageBody.Replace("@@fromName", PSession.User.ContactName);
                PUser user = new BUser().GetUserDetails(Tickets.CreatedBy.UserID);
                new EmailManager().MailSend(user.Mail, "Ticketing System", messageBody, TaskItem.HeaderID);
            }
            ClearFieldResolve();
        }
        Boolean validationResolve()
        {
            decimal parsedValue;
            if (!decimal.TryParse(txtEffort.Text, out parsedValue))
            {
                lblResolve.Text = "Please Enter number in Effort...!";
                txtEffort.Focus();
                MPE_Resolve.Show();
                return false;
            }
            if (ddlResolutionType.SelectedValue == "0")
            {
                lblResolve.Text = "Please select Resolution Type...!";
                txtEffort.Focus();
                MPE_Resolve.Show();
                return false;
            }
            if (string.IsNullOrEmpty(txtResolution.Text.Trim()))
            {
                lblResolve.Text = "Please enter the resolution...!";
                txtResolution.Focus();
                MPE_Resolve.Show();
                return false;
            }
            return true;
        }
        void ClearFieldResolve()
        {
            txtEffort.Text = string.Empty;
            txtResolution.Text = string.Empty;
            ddlResolutionType.SelectedValue = "0";
            gvResolveNewFileAttached.DataSource = null;
            gvResolveNewFileAttached.DataBind();
        }
        void FillResolutionType()
        {
            ddlResolutionType.DataTextField = "ResolutionType";
            ddlResolutionType.DataValueField = "ResolutionTypeID";
            ddlResolutionType.DataSource = new BTicketResolutionType().getTicketResolutionType(null, null);
            ddlResolutionType.DataBind();
            ddlResolutionType.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void ResRemove_Click(object sender, EventArgs e)
        {
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
            Label file = (Label)Grow.FindControl("lbltest");
            int fileIndex = 0;
            foreach (PAttachedFile file1 in AttchedFile)
            {
                if (file1.FileName == file.Text)
                {
                    AttchedFile.RemoveAt(fileIndex);
                }
                fileIndex = fileIndex + 1;
            }

            gvResolveNewFileAttached.DataSource = AttchedFile;
            gvResolveNewFileAttached.DataBind();
        }
        void ActionControlMange()
        {
            lbtnMessage.Visible = true;
            lbtnUploadFile.Visible = true;

            lbtnSendApproval.Visible = true;
            lbtnApprove.Visible = true;
            lbtnAssignTo.Visible = true;
            lbtnInProgress.Visible = true;
            lbtnResolve.Visible = true;
            lbtnCancel.Visible = true;
            lbtnForceclose.Visible = true;
            lbtnClose.Visible = true;

            if (Ticket[0].Status.StatusID == 1)
            {
                lbtnClose.Visible = false;
                lbtnCancel.Visible = false;
                lbtnResolve.Visible = false;
                lbtnInProgress.Visible = false;
                lbtnApprove.Visible = false;
            }

            if ((Ticket[0].Status.StatusID == 2) || (Ticket[0].Status.StatusID == 8))
            {
                lbtnApprove.Visible = false;
                if (Ticket[0].TicketItems.Any(item => item.ItemStatus.StatusID == 2 && item.AssignedTo.UserID == PSession.User.UserID))
                {
                    lbtnClose.Visible = false;
                    //lbtnCancel.Visible = false;
                    lbtnResolve.Visible = false;
                    //lbtnInProgress.Visible = false;
                }
                else if (Ticket[0].TicketItems.Any(item => item.ItemStatus.StatusID == 3 && item.AssignedTo.UserID == PSession.User.UserID))
                {
                    lbtnClose.Visible = false;
                    lbtnCancel.Visible = false;
                    //lbtnResolve.Visible = false;
                    lbtnInProgress.Visible = false;
                }
                else
                {
                    lbtnClose.Visible = false;
                    lbtnCancel.Visible = false;
                    lbtnResolve.Visible = false;
                    lbtnInProgress.Visible = false;
                }
            }
            if (Ticket[0].Status.StatusID == 3)
            {
                lbtnApprove.Visible = false;
                if (Ticket[0].TicketItems.Any(item => item.ItemStatus.StatusID == 3 && item.AssignedTo.UserID == PSession.User.UserID))
                {
                    lbtnClose.Visible = false;
                    lbtnCancel.Visible = false;
                    //lbtnResolve.Visible = false;
                    lbtnInProgress.Visible = false;
                }
                else
                {
                    lbtnClose.Visible = false;
                    lbtnCancel.Visible = false;
                    lbtnResolve.Visible = false;
                    lbtnInProgress.Visible = false;
                }
            }
            if (Ticket[0].Status.StatusID == 4)
            {
                lbtnApprove.Visible = false;
                lbtnCancel.Visible = false;
                lbtnResolve.Visible = false;
                lbtnInProgress.Visible = false;
                lbtnForceclose.Visible = false;
                lbtnSendApproval.Visible = false;
                if (Ticket[0].CreatedBy.UserID != PSession.User.UserID)
                {
                    lbtnClose.Visible = false;
                }
            }

            if (Ticket[0].Status.StatusID == 6)
            {
                lbtnAssignTo.Visible = false;
                lbtnCancel.Visible = false;
                lbtnResolve.Visible = false;
                lbtnInProgress.Visible = false;
                lbtnClose.Visible = false;

                if (!Ticket[0].ApprovalDetails.Any(item => item.IsAppoved != true && item.Approver.ContactName == PSession.User.ContactName))
                {
                    lbtnApprove.Visible = false;
                }
            }
            bool containsItemHideAll = Ticket.Any(item => item.Status.StatusID == 5 || item.Status.StatusID == 11);
            if (containsItemHideAll)
            {
                lbtnSendApproval.Visible = false;
                lbtnApprove.Visible = false;
                lbtnAssignTo.Visible = false;
                lbtnInProgress.Visible = false;
                lbtnResolve.Visible = false;
                lbtnCancel.Visible = false;
                lbtnForceclose.Visible = false;
                lbtnClose.Visible = false;
            }
            if (PSession.User.UserID != 1)
            {
                lbtnAssignTo.Visible = false;
                lbtnSendApproval.Visible = false;
                lbtnForceclose.Visible = false;
            }
        }
    }
    class PMessage
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public Boolean MessageVisible { get; set; }
        public string CssClass { get; set; }
        public string FileType { get; set; }
        public Boolean FileTypeVisible { get; set; }
    }
    class PMessageTemp
    {
        public long ID { get; set; }
        public string Sender { get; set; }
        public string SenderTime { get; set; }
        public string Message { get; set; }
        public string AttachedFile { get; set; }
        public string Receiver { get; set; }
        public string ReceiverTime { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public Boolean FileTypeVisible { get; set; }
        public Boolean MessageVisible { get; set; }
        public string CssClass { get; set; }
    }
}