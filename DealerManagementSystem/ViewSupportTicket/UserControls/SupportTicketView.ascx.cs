using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                if (ViewState["TaskReportView"] == null)
                {
                    ViewState["TaskReportView"] = new List<PTicketHeader>();
                }
                return (List<PTicketHeader>)ViewState["TaskReportView"];
            }
            set
            {
                ViewState["TaskReportView"] = value;
            }
        }
        public List<PUser> AjaxEmployee
        {
            get
            {
                if (ViewState["AjaxEmployee"] == null)
                {
                    ViewState["AjaxEmployee"] = new List<PUser>();
                }
                return (List<PUser>)ViewState["AjaxEmployee"];
            }
            set
            {
                ViewState["AjaxEmployee"] = value;
            }
        }
        private string Action;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["TicketNo"])))
                {
                    long TicketNo = Convert.ToInt64(ViewState["TicketNo"]);
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
            ddlCategory.DataSource = JsonConvert.DeserializeObject<List<PCategory>>(JsonConvert.SerializeObject(new BTickets().getTicketCategory(null, null).Data));
            ddlCategory.DataBind();

            ddlEditCategory.DataTextField = "Category";
            ddlEditCategory.DataValueField = "CategoryID";
            ddlEditCategory.DataSource = JsonConvert.DeserializeObject<List<PCategory>>(JsonConvert.SerializeObject(new BTickets().getTicketCategory(null, null).Data));
            ddlEditCategory.DataBind();
        }
        void FillSubCategory()
        {
            ddlSubcategory.DataTextField = "SubCategory";
            ddlSubcategory.DataValueField = "SubCategoryID";
            ddlSubcategory.DataSource = JsonConvert.DeserializeObject<List<PSubCategory>>(JsonConvert.SerializeObject(new BTickets().getTicketSubCategory(null, null, Convert.ToInt32(ddlCategory.SelectedValue)).Data));
            ddlSubcategory.DataBind();
            ddlSubcategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillEditSubCategory()
        {
            ddlEditSubCategory.DataTextField = "SubCategory";
            ddlEditSubCategory.DataValueField = "SubCategoryID";
            ddlEditSubCategory.DataSource = JsonConvert.DeserializeObject<List<PSubCategory>>(JsonConvert.SerializeObject(new BTickets().getTicketSubCategory(null, null, (ddlEditCategory.SelectedValue == "0") ? (Int32?)null : Convert.ToInt32(ddlEditCategory.SelectedValue)).Data));
            ddlEditSubCategory.DataBind();
            ddlEditSubCategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillTicketType()
        {
            ddlEditTicketType.DataTextField = "Type";
            ddlEditTicketType.DataValueField = "TypeID";
            ddlEditTicketType.DataSource = JsonConvert.DeserializeObject<List<PType>>(JsonConvert.SerializeObject(new BTickets().getTicketType(null, null).Data));
            ddlEditTicketType.DataBind();
            ddlEditTicketType.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillTicketSeverity()
        {
            ddlSeverity.DataTextField = "Severity";
            ddlSeverity.DataValueField = "SeverityID";
            ddlSeverity.DataSource = JsonConvert.DeserializeObject<List<PSeverity>>(JsonConvert.SerializeObject(new BTickets().getTicketSeverity(null, null).Data));
            ddlSeverity.DataBind();
            ddlSeverity.Items.Insert(0, new ListItem("Select", "0"));
            ddlSeverity.SelectedValue = "3";

            ddlEditSeverity.DataTextField = "Severity";
            ddlEditSeverity.DataValueField = "SeverityID";
            ddlEditSeverity.DataSource = JsonConvert.DeserializeObject<List<PSeverity>>(JsonConvert.SerializeObject(new BTickets().getTicketSeverity(null, null).Data));
            ddlEditSeverity.DataBind();
            ddlEditSeverity.Items.Insert(0, new ListItem("Select", "0"));
            ddlEditSeverity.SelectedValue = "3";
        }
        void FillAssignTo()
        {
            new DDLBind().FillDealerAndEngneer(ddlAssignDealer, null);
            PDealer dealer = new BDealer().GetDealerByID(null, PSession.User.ExternalReferenceID);
            ddlAssignDealer.SelectedValue = dealer.DID.ToString();
            new BDMS_Dealer().GetDealerDepartmentDDL(ddlAssignDepartment, null, null);
            ddlAssignDepartment.SelectedValue = PSession.User.Department.DealerDepartmentID.ToString();
            //Changes Requested from John Sir
            List<PUser> user = new BUser().GetUsers(null, null, null, null, null, true, null, null, null);
            new DDLBind(ddlAssignedTo, user, "ContactName", "UserID");
            ddlAssignDealer.Enabled = false;
            ddlAssignDepartment.Enabled = false;
            ddlAssignDepartment_SelectedIndexChanged(null, null);
        }
        void FillApproval()
        {
            List<PUser> user = new BUser().GetUsers(null, null, null, null, null, true, null, null, null);
            new DDLBind(ddlapprovar, user, "ContactName", "UserID");
        }
        void FillAllFields(long TicketNo)
        {
            ddlCategory.SelectedValue = Convert.ToString(Ticket[0].Category.CategoryID);
            FillSubCategory();
            if (Ticket[0].SubCategory != null)
            {
                ddlSubcategory.SelectedValue = Convert.ToString(Ticket[0].SubCategory.SubCategoryID);
            }
            if (Ticket[0].Severity != null)
            {
                ddlSeverity.SelectedValue = Convert.ToString(Ticket[0].Severity.SeverityID);
            }
        }
        void ClearField()
        {
            txtAssignerRemark.Text = string.Empty;
        }
        Boolean validatetion()
        {
            decimal parsedValue;
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
            if (ddlAssignDealer.SelectedValue == "0")
            {
                lblMessageAssignTo.Text = "Please Select The Dealer";
                return false;
            }
            if (ddlAssignDepartment.SelectedValue == "0")
            {
                lblMessageAssignTo.Text = "Please Select The Department";
                return false;
            }
            if (ddlAssignedTo.SelectedValue == "0")
            {
                lblMessageAssignTo.Text = "Please Select The Assigned To";
                return false;
            }
            return true;
        }
        Boolean validationHeaderEditInfo()
        {
            if (ddlEditCategory.SelectedValue == "0")
            {
                lblMessageHeaderEdit.Text = "Please Select The Category";
                return false;
            }
            if (ddlEditSubCategory.SelectedValue == "0")
            {
                lblMessageHeaderEdit.Text = "Please Select The Subcategory";
                return false;
            }
            if (ddlEditTicketType.SelectedValue == "0")
            {
                lblMessageHeaderEdit.Text = "Please Select The TicketType";
                return false;
            }
            if (ddlEditSeverity.SelectedValue == "0")
            {
                lblMessageHeaderEdit.Text = "Please Select The Severity";
                return false;
            }
            if (string.IsNullOrEmpty(txtEditSubject.Text))
            {
                lblMessageHeaderEdit.Text = "Please Enter The Subject";
                return false;
            }
            if (string.IsNullOrEmpty(txtEditDescription.Text))
            {
                lblMessageHeaderEdit.Text = "Please Enter The Description";
                return false;
            }
            return true;
        }
        public void FillTickets(long? TicketNO)
        {
            ViewState["TicketNo"] = TicketNO;
            PApiResult Result = new BTickets().GetTicketByID(TicketNO);
            Ticket = JsonConvert.DeserializeObject<List<PTicketHeader>>(JsonConvert.SerializeObject(Result.Data));
            //Ticket = new BTickets().GetTicketByID(TicketNO);

            ddlMailNotification.Items.Clear();
            ddlMailNotification.Items.Insert(ddlMailNotification.Items.Count, new ListItem("Select", "0"));
            ddlMailNotification.Items.Insert(ddlMailNotification.Items.Count, new ListItem("All", "-1"));
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
                if (ddlMailNotification.Items.FindByValue(A.RequestedBy.UserID.ToString()) == null)
                {
                    ddlMailNotification.Items.Insert(ddlMailNotification.Items.Count, new ListItem(A.RequestedBy.ContactName, A.RequestedBy.UserID.ToString()));
                }
            }

            lblTicketID.Text = Ticket[0].HeaderID.ToString();
            lblDealer.Text = Ticket[0].Dealer.DealerCode + " " + Ticket[0].Dealer.DealerName;
            lblSubject.Text = Ticket[0].Subject;

            lblCategory.Text = Ticket[0].Category.Category;
            lblSubCategory.Text = (Ticket[0].SubCategory == null) ? "" : Ticket[0].SubCategory.SubCategory;
            lblTicketType.Text = Ticket[0].Type.Type;

            lblSeverity.Text = (Ticket[0].Severity == null) ? "" : Ticket[0].Severity.Severity;
            lblAge.Text = Ticket[0].age.ToString() + " " + Ticket[0].SLA.ToString();
            lblStatus.Text = Ticket[0].Status.Status;

            lblCreatedBy.Text = Ticket[0].CreatedBy.ContactName;
            lblCreatedByContactNumber.Text = Ticket[0].CreatedBy.ContactNumber;
            lblCreatedOn.Text = Ticket[0].CreatedOn.ToString();

            lblUpdatedBy.Text = (Ticket[0].UpdatedBy == null) ? "" : Ticket[0].UpdatedBy.ContactName;
            lblUpdatedByContactNumber.Text = (Ticket[0].UpdatedBy == null) ? "" : Ticket[0].UpdatedBy.ContactNumber;
            lblUpdatedOn.Text = (Ticket[0].UpdatedBy == null) ? "" : Ticket[0].CreatedOn.ToString();

            lblDescription.Text = Ticket[0].Description;
            lblClosedBy.Text = (Ticket[0].ClosedBy == null) ? "" : Ticket[0].ClosedBy.ContactName;
            lblClosedOn.Text = (Ticket[0].ClosedOn == null) ? "" : Ticket[0].ClosedOn.ToString();

            lblFeedback.Text = Ticket[0].Feedback;
            Rating1.CurrentRating = (Ticket[0].Rating == null) ? 0 : Convert.ToInt32(Ticket[0].Rating);            
            //gvTickets.DataSource = Ticket;
            //gvTickets.DataBind();

            gvTicketItem.DataSource = Ticket[0].TicketItems;
            gvTicketItem.DataBind();

            gvApprover.DataSource = Ticket[0].ApprovalDetails;
            gvApprover.DataBind();

            ActionControlMange();
        }
        public void FillChat(long TicketNO)
        {
            List<PMessage> PMessages = new List<PMessage>();
            PMessage Message = null;

            long LastMessageID = 0;
            //List<PForum> Forums = new BTickets().GetForumDetails(TicketNO);
            PApiResult Result = new BTickets().GetForumDetails(TicketNO);
            List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(Result.Data));
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
            tbpTaskView.ActiveTabIndex = 0;
            if (LastMessageID != 0)
                new BForum().UdateMessageViewStatus(TicketNO, PSession.User.UserID, LastMessageID);
        }
        public void FillChatTemp(long TicketNO)
        {
            List<PMessageTemp> PMessages = new List<PMessageTemp>();
            PMessageTemp Message = null;

            long LastMessageID = 0;
            //List<PForum> Forums = new BTickets().GetForumDetails(TicketNO);
            PApiResult Result = new BTickets().GetForumDetails(TicketNO);
            List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(Result.Data));
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
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Edit Header Info")
            {
                FillCategory();
                FillEditSubCategory();
                FillTicketType();
                FillTicketSeverity();
                FillEditInfo(Convert.ToInt64(ViewState["TicketNo"]));
                btnUpdateHeaderInfo.Visible = true;
                MPE_EditHeaderInfo.Show();
            }
            else if (lbActions.Text == "Message")
            {
                txtMessage.Text = "";
                txtMessage.Visible = true;
                divMailNotification.Visible = true;
                ddlMailNotification.SelectedValue = "-1";
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
                ddlMailNotification.SelectedValue = "-1";
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
                btnAssign.Text = "Assign";
                FillCategory();
                FillTicketSeverity();
                FillAssignTo();
                FillAllFields(Convert.ToInt64(ViewState["TicketNo"]));
                MPE_AssignTo.Show();
            }
            else if (lbActions.Text == "Reassign")
            {
                btnAssign.Visible = true;
                btnAssign.Text = "Reassign";
                FillCategory();
                FillTicketSeverity();
                FillAssignTo();
                FillAllFields(Convert.ToInt64(ViewState["TicketNo"]));
                MPE_AssignTo.Show();
            }
            else if (lbActions.Text == "In Progress")
            {
                InProgress();
            }
            else if (lbActions.Text == "Resolve")
            {
                FillResolutionType();
                FillAjaxEmployee();
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
            else if (lbActions.Text == "Reopen")
            {
                Reopen();
            }
            else if (lbActions.Text == "Reject")
            {
                txtMessage.Text = "";
                txtMessage.Visible = true;
                FileUpload.Visible = false;
                divMailNotification.Visible = false;
                btnSend.Text = "Send";
                btnSend.Focus();
                MPE_Conversation.Show();
                Label2.Text = "Reject";
            }
            else if (lbActions.Text == "Resend Approval")
            {
                FillApproval();
                btnSendForApproval.Visible = true;
                MPE_SendApproval.Show();
            }
            tpnlTicketHistory.Visible = true;
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            PForum_Insert Forum = new PForum_Insert();
            Forum.HeaderID = Convert.ToInt64(ViewState["TicketNo"]);

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
                result = new BAPI().ApiPut("Task/UpdateTicketForceClosedStatus", Forum);
            }
            if (Label2.Text == "Approve")
            {
                result = new BAPI().ApiPut("Task/UpdateTicketStatusApprove", Forum);
            }
            if (Label2.Text == "Reject")
            {
                result = new BAPI().ApiPut("Task/UpdateTicketRejectStatus", Forum);
            }

            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

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
                    string CC = "";
                    if (ddlMailNotification.SelectedValue == "-1")
                    {
                        PApiResult ResultTicketByID = new BTickets().GetTicketByID(Forum.HeaderID);
                        List<PTicketHeader> TicketLts = JsonConvert.DeserializeObject<List<PTicketHeader>>(JsonConvert.SerializeObject(ResultTicketByID.Data));
                        //List<PTicketHeader> TicketLts = new BTickets().GetTicketByID(Convert.ToInt32(Forum.HeaderID));
                        List<string> EmailList = new List<string>();
                        var items = ddlMailNotification.Items;
                        PUser userCreatedBy = new BUser().GetUserDetails(TicketLts[0].CreatedBy.UserID);
                        PDealer dealer = new BDealer().GetDealerByID(null, userCreatedBy.ExternalReferenceID);
                        foreach (var item in items.Cast<ListItem>().Where(x => x.Value != "-1" && x.Value != "0"))
                        {
                            PUser TicketUsers = new BUser().GetUserDetails(Convert.ToInt32(item.Value));
                            if (!EmailList.Contains(TicketUsers.Mail))
                            {
                                if (userCreatedBy.Mail != TicketUsers.Mail)
                                {
                                    EmailList.Add(TicketUsers.Mail);
                                    CC = (CC == "") ? TicketUsers.Mail : "," + TicketUsers.Mail;
                                }
                            }
                        }
                        string messageBody = messageBody = new EmailManager().GetMailNotificationTemplate("TicketMessage.htm");
                        messageBody = messageBody.Replace("@@RequestedOn", TicketLts[0].CreatedOn.ToString());
                        messageBody = messageBody.Replace("@@DealerName", dealer.DealerCode + " - " + dealer.ContactName);
                        messageBody = messageBody.Replace("@@Description", TicketLts[0].Description);
                        messageBody = messageBody.Replace("@@ContactName", TicketLts[0].ContactName);
                        messageBody = messageBody.Replace("@@MobileNo", TicketLts[0].MobileNo);
                        messageBody = messageBody.Replace("@@Category", TicketLts[0].Category.Category);
                        messageBody = messageBody.Replace("@@Subcategory", TicketLts[0].SubCategory.SubCategory);
                        messageBody = messageBody.Replace("@@Subject", TicketLts[0].Subject);
                        messageBody = messageBody.Replace("@@Message", Forum.Message);
                        messageBody = messageBody.Replace("@@TicketNo", Forum.HeaderID.ToString());
                        messageBody = messageBody.Replace("@@ToName", userCreatedBy.ContactName);
                        messageBody = messageBody.Replace("@@Status", TicketLts[0].Status.Status);
                        messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
                        messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);

                        List<PMessage> PMessages = new List<PMessage>();
                        PMessage Message = null;

                        long LastMessageID = 0;
                        string Msg = "";
                        //List<PForum> Forums = new BTickets().GetForumDetails(Forum.HeaderID);
                        PApiResult ResultForums = new BTickets().GetForumDetails(Forum.HeaderID);
                        List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(ResultForums.Data));
                        foreach (PForum F in Forums)
                        {
                            Message = new PMessage();
                            Message.Message = "<tr><td style='background-color: white;width:150px;'>" + F.FromUser.ContactName + "</td><td style='background-color: white;width:145px;'>" + F.CreatedOn + "</td><td style='background-color: white;color:blue'>" + F.Message + "</td></tr>";
                            Msg += Message.Message;

                            PMessages.Add(Message);
                            LastMessageID = F.ID;
                        }
                        messageBody = messageBody.Replace("@@Msg", "<table border='1' cellspacing='0' width='100%'><tr><th style='background-color: #696767;color: white;'>From</th><th style='background-color: #696767;color: white;'>Date</th><th style='background-color: #696767;color: white;'>Response</th style='background-color: #696767;color: white;'></tr>" + Msg + "</table>");
                        new EmailManager().MailSend(userCreatedBy.Mail, "", ConfigurationManager.AppSettings["TaskMailBcc"] + "," + CC, "AJAXOne-[Ticket No: " + Forum.HeaderID.ToString() + "] Message", messageBody, Forum.HeaderID);
                    }
                    else
                    {
                        PApiResult ResultTicketByID = new BTickets().GetTicketByID(Forum.HeaderID);
                        List<PTicketHeader> TicketLts = JsonConvert.DeserializeObject<List<PTicketHeader>>(JsonConvert.SerializeObject(ResultTicketByID.Data));
                        //List<PTicketHeader> TicketLts = new BTickets().GetTicketByID(Convert.ToInt32(Forum.HeaderID));
                        PUser UserCreatedBy = new BUser().GetUserDetails(TicketLts[0].CreatedBy.UserID);
                        PDealer dealer = new BDealer().GetDealerByID(null, UserCreatedBy.ExternalReferenceID);
                        PUser TicketSelectedUsers = new BUser().GetUserDetails(Convert.ToInt32(ddlMailNotification.SelectedValue));
                        if (UserCreatedBy.Mail != TicketSelectedUsers.Mail)
                        {
                            CC = TicketSelectedUsers.Mail;
                        }
                        if ((UserCreatedBy.Mail != PSession.User.Mail) && TicketSelectedUsers.Mail != PSession.User.Mail && ConfigurationManager.AppSettings["TaskMailBcc"] != PSession.User.Mail)
                        {
                            CC += (CC == "") ? PSession.User.Mail : "," + PSession.User.Mail;
                        }
                        string messageBody = messageBody = new EmailManager().GetMailNotificationTemplate("TicketMessage.htm");
                        messageBody = messageBody.Replace("@@RequestedOn", TicketLts[0].CreatedOn.ToString());
                        messageBody = messageBody.Replace("@@DealerName", dealer.DealerCode + " - " + dealer.ContactName);
                        messageBody = messageBody.Replace("@@Description", TicketLts[0].Description);
                        messageBody = messageBody.Replace("@@ContactName", TicketLts[0].ContactName);
                        messageBody = messageBody.Replace("@@MobileNo", TicketLts[0].MobileNo);
                        messageBody = messageBody.Replace("@@Category", TicketLts[0].Category.Category);
                        messageBody = messageBody.Replace("@@Subcategory", TicketLts[0].SubCategory.SubCategory);
                        messageBody = messageBody.Replace("@@Subject", TicketLts[0].Subject);
                        messageBody = messageBody.Replace("@@Message", Forum.Message);
                        messageBody = messageBody.Replace("@@TicketNo", Forum.HeaderID.ToString());
                        messageBody = messageBody.Replace("@@ToName", UserCreatedBy.ContactName);
                        messageBody = messageBody.Replace("@@Status", TicketLts[0].Status.Status);
                        messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
                        messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);

                        List<PMessage> PMessages = new List<PMessage>();
                        PMessage Message = null;

                        long LastMessageID = 0;
                        string Msg = "";
                        //List<PForum> Forums = new BTickets().GetForumDetails(Forum.HeaderID));
                        PApiResult ResultForum = new BTickets().GetForumDetails(Forum.HeaderID);
                        List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(ResultForum.Data));
                        foreach (PForum F in Forums)
                        {
                            Message = new PMessage();
                            Message.Message = "<tr><td style='background-color: white;width:150px;'>" + F.FromUser.ContactName + "</td><td style='background-color: white;width:145px;'>" + F.CreatedOn + "</td><td style='background-color: white;color:blue'>" + F.Message + "</td></tr>";
                            Msg += Message.Message;

                            PMessages.Add(Message);
                            LastMessageID = F.ID;
                        }
                        messageBody = messageBody.Replace("@@Msg", "<table border='1' cellspacing='0' width='100%'><tr><th style='background-color: #696767;color: white;'>From</th><th style='background-color: #696767;color: white;'>Date</th><th style='background-color: #696767;color: white;'>Response</th style='background-color: #696767;color: white;'></tr>" + Msg + "</table>");
                        new EmailManager().MailSend(UserCreatedBy.Mail, "", ConfigurationManager.AppSettings["TaskMailBcc"] + "," + CC, "AJAXOne-[Ticket No: " + Forum.HeaderID.ToString() + "] Message", messageBody, Forum.HeaderID);
                    }
                }
            }
            else if (Label2.Text == "Approve")
            {
                int? RequestedBy = null, Approver = null, CreatedBy = null;
                DateTime? RequestedOn = null;
                PApiResult ResultTicketByID = new BTickets().GetTicketByID(Forum.HeaderID);
                List<PTicketHeader> TicketLts = JsonConvert.DeserializeObject<List<PTicketHeader>>(JsonConvert.SerializeObject(ResultTicketByID.Data));
                //List<PTicketHeader> TicketLts = new BTickets().GetTicketByID(Convert.ToInt32(Forum.HeaderID));
                foreach (PTicketsApprovalDetails Item in TicketLts[0].ApprovalDetails)
                {
                    if (PSession.User.UserID == Item.Approver.UserID && Item.InActive == false)
                    {
                        RequestedBy = Item.RequestedBy.UserID;
                        RequestedOn = Item.RequestedOn;
                        Approver = Item.Approver.UserID;
                    }
                }
                CreatedBy = TicketLts[0].CreatedBy.UserID;

                string messageBody = "";
                PUser UserApprovar = new BUser().GetUserDetails(Convert.ToInt64(Approver));
                PUser userCreatedBy = new BUser().GetUserDetails(Convert.ToInt64(CreatedBy));
                PUser userRequester = new BUser().GetUserDetails(Convert.ToInt64(RequestedBy));
                PDealer dealer = new BDealer().GetDealerByID(null, userCreatedBy.ExternalReferenceID);
                messageBody = new EmailManager().GetMailNotificationTemplate("TicketApproved.htm");
                messageBody = messageBody.Replace("@@TicketNo", TicketLts[0].HeaderID.ToString());
                messageBody = messageBody.Replace("@@ApproverName", UserApprovar.ContactName);
                messageBody = messageBody.Replace("@@RequestedByName", userRequester.ContactName);
                messageBody = messageBody.Replace("@@CreatedByName", userCreatedBy.ContactName);
                messageBody = messageBody.Replace("@@RequestedOn", TicketLts[0].CreatedOn.ToString());
                messageBody = messageBody.Replace("@@DealerName", dealer.DealerCode + " - " + dealer.ContactName);
                messageBody = messageBody.Replace("@@TicketType", TicketLts[0].Type.Type);
                messageBody = messageBody.Replace("@@Category", TicketLts[0].Category.Category);
                messageBody = messageBody.Replace("@@Subcategory", TicketLts[0].SubCategory.SubCategory);
                messageBody = messageBody.Replace("@@Subject", TicketLts[0].Subject);
                messageBody = messageBody.Replace("@@Description", TicketLts[0].Description);
                messageBody = messageBody.Replace("@@ContactName", TicketLts[0].ContactName);
                messageBody = messageBody.Replace("@@MobileNo", TicketLts[0].MobileNo);
                messageBody = messageBody.Replace("@@ApprovalRequestedDate", RequestedOn.ToString());
                messageBody = messageBody.Replace("@@ApprovedDate", DateTime.Now.ToString());
                messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
                messageBody = messageBody.Replace("@@Status", TicketLts[0].Status.Status);
                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);

                List<PMessage> PMessages = new List<PMessage>();
                PMessage Message = null;

                long LastMessageID = 0;
                string Msg = "";
                //List<PForum> Forums = new BTickets().GetForumDetails(Convert.ToInt32(TicketLts[0].HeaderID.ToString()));
                PApiResult ResultForum = new BTickets().GetForumDetails(TicketLts[0].HeaderID);
                List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(ResultForum.Data));
                foreach (PForum F in Forums)
                {
                    Message = new PMessage();
                    Message.Message = "<tr><td style='background-color: white;width:150px;'>" + F.FromUser.ContactName + "</td><td style='background-color: white;width:145px;'>" + F.CreatedOn + "</td><td style='background-color: white;color:blue'>" + F.Message + "</td></tr>";
                    Msg += Message.Message;

                    PMessages.Add(Message);
                    LastMessageID = F.ID;
                }
                messageBody = messageBody.Replace("@@Msg", "<table border='1' cellspacing='0' width='100%'><tr><th style='background-color: #696767;color: white;'>From</th><th style='background-color: #696767;color: white;'>Date</th><th style='background-color: #696767;color: white;'>Response</th style='background-color: #696767;color: white;'></tr>" + Msg + "</table>");
                new EmailManager().MailSend(userRequester.Mail, "", ConfigurationManager.AppSettings["TaskMailBcc"] + "," + userCreatedBy.Mail + "," + UserApprovar.Mail, "AJAXOne-[Ticket No: " + TicketLts[0].HeaderID.ToString() + "] Approved", messageBody, Convert.ToInt64(TicketLts[0].HeaderID.ToString()));
            }
            else if (Label2.Text == "Reject")
            {
                int? RequestedBy = null, Approver = null, CreatedBy = null;
                DateTime? RequestedOn = null;
                PApiResult ResultTicketByID = new BTickets().GetTicketByID(Forum.HeaderID);
                List<PTicketHeader> TicketLts = JsonConvert.DeserializeObject<List<PTicketHeader>>(JsonConvert.SerializeObject(ResultTicketByID.Data));
                //List<PTicketHeader> TicketLts = new BTickets().GetTicketByID(Convert.ToInt32(Forum.HeaderID));
                foreach (PTicketsApprovalDetails Item in TicketLts[0].ApprovalDetails)
                {
                    if (PSession.User.UserID == Item.Approver.UserID && Item.InActive == false)
                    {
                        RequestedBy = Item.RequestedBy.UserID;
                        RequestedOn = Item.RequestedOn;
                        Approver = Item.Approver.UserID;
                    }
                }
                CreatedBy = TicketLts[0].CreatedBy.UserID;

                string messageBody = "";
                PUser UserApprovar = new BUser().GetUserDetails(Convert.ToInt64(Approver));
                PUser userCreatedBy = new BUser().GetUserDetails(Convert.ToInt64(CreatedBy));
                PUser userRequester = new BUser().GetUserDetails(Convert.ToInt64(RequestedBy));
                PDealer dealer = new BDealer().GetDealerByID(null, userCreatedBy.ExternalReferenceID);
                messageBody = new EmailManager().GetMailNotificationTemplate("TicketRejected.htm");
                messageBody = messageBody.Replace("@@TicketNo", TicketLts[0].HeaderID.ToString());
                messageBody = messageBody.Replace("@@ApproverName", UserApprovar.ContactName);
                messageBody = messageBody.Replace("@@RequestedByName", userRequester.ContactName);
                messageBody = messageBody.Replace("@@CreatedByName", userCreatedBy.ContactName);
                messageBody = messageBody.Replace("@@RequestedOn", TicketLts[0].CreatedOn.ToString());
                messageBody = messageBody.Replace("@@DealerName", dealer.DealerCode + " - " + dealer.ContactName);
                messageBody = messageBody.Replace("@@TicketType", TicketLts[0].Type.Type);
                messageBody = messageBody.Replace("@@Category", TicketLts[0].Category.Category);
                messageBody = messageBody.Replace("@@Subcategory", TicketLts[0].SubCategory.SubCategory);
                messageBody = messageBody.Replace("@@Subject", TicketLts[0].Subject);
                messageBody = messageBody.Replace("@@Description", TicketLts[0].Description);
                messageBody = messageBody.Replace("@@ContactName", TicketLts[0].ContactName);
                messageBody = messageBody.Replace("@@MobileNo", TicketLts[0].MobileNo);
                messageBody = messageBody.Replace("@@ApprovalRequestedDate", RequestedOn.ToString());
                messageBody = messageBody.Replace("@@ApprovedDate", DateTime.Now.ToString());
                messageBody = messageBody.Replace("@@ReasonForRejection", Forum.Message);
                messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
                messageBody = messageBody.Replace("@@Status", TicketLts[0].Status.Status);
                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);

                List<PMessage> PMessages = new List<PMessage>();
                PMessage Message = null;

                long LastMessageID = 0;
                string Msg = "";
                //List<PForum> Forums = new BTickets().GetForumDetails(Convert.ToInt32(TicketLts[0].HeaderID.ToString()));
                PApiResult ResultForum = new BTickets().GetForumDetails(TicketLts[0].HeaderID);
                List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(ResultForum.Data));
                foreach (PForum F in Forums)
                {
                    Message = new PMessage();
                    Message.Message = "<tr><td style='background-color: white;width:150px;'>" + F.FromUser.ContactName + "</td><td style='background-color: white;width:145px;'>" + F.CreatedOn + "</td><td style='background-color: white;color:blue'>" + F.Message + "</td></tr>";
                    Msg += Message.Message;

                    PMessages.Add(Message);
                    LastMessageID = F.ID;
                }
                messageBody = messageBody.Replace("@@Msg", "<table border='1' cellspacing='0' width='100%'><tr><th style='background-color: #696767;color: white;'>From</th><th style='background-color: #696767;color: white;'>Date</th><th style='background-color: #696767;color: white;'>Response</th style='background-color: #696767;color: white;'></tr>" + Msg + "</table>");
                new EmailManager().MailSend(userRequester.Mail, "", ConfigurationManager.AppSettings["TaskMailBcc"] + "," + userCreatedBy.Mail + "," + UserApprovar.Mail, "AJAXOne-[Ticket No: " + TicketLts[0].HeaderID.ToString() + "] Rejected", messageBody, Convert.ToInt64(TicketLts[0].HeaderID.ToString()));
            }
            else if (Label2.Text == "Force Close")
            {
                string messageBody = "";
                PApiResult ResultTicketByID = new BTickets().GetTicketByID(Forum.HeaderID);
                List<PTicketHeader> TicketLts = JsonConvert.DeserializeObject<List<PTicketHeader>>(JsonConvert.SerializeObject(ResultTicketByID.Data));
                //List<PTicketHeader> TicketLts = new BTickets().GetTicketByID(Convert.ToInt32(Forum.HeaderID));
                PUser userCreatedBy = new BUser().GetUserDetails(TicketLts[0].CreatedBy.UserID);
                PUser userForceclosedBy = new BUser().GetUserDetails(Convert.ToInt64(PSession.User.UserID));
                PDealer dealer = new BDealer().GetDealerByID(null, userCreatedBy.ExternalReferenceID);

                string CC = "";
                foreach (PTicketItem TktDet in TicketLts[0].TicketItems)
                {
                    PUser TicketUsersAssignedTo = new BUser().GetUserDetails(Convert.ToInt32(TktDet.AssignedTo.UserID));
                    if (!CC.Contains(TicketUsersAssignedTo.Mail))
                    {
                        CC = (CC == "") ? TicketUsersAssignedTo.Mail : "," + TicketUsersAssignedTo.Mail;
                    }
                    PUser TicketUsersAssignedBy = new BUser().GetUserDetails(Convert.ToInt32(TktDet.AssignedBy.UserID));
                    if (!CC.Contains(TicketUsersAssignedBy.Mail))
                    {
                        CC = (CC == "") ? TicketUsersAssignedBy.Mail : "," + TicketUsersAssignedBy.Mail;
                    }
                }


                messageBody = new EmailManager().GetMailNotificationTemplate("TicketForceclose.htm");
                messageBody = messageBody.Replace("@@TicketNo", TicketLts[0].HeaderID.ToString());
                messageBody = messageBody.Replace("@@RequestedOn", TicketLts[0].CreatedOn.ToString());
                messageBody = messageBody.Replace("@@DealerName", dealer.DealerCode + " - " + dealer.ContactName);
                messageBody = messageBody.Replace("@@TicketType", TicketLts[0].Type.Type);
                messageBody = messageBody.Replace("@@Category", TicketLts[0].Category.Category);
                messageBody = messageBody.Replace("@@Subcategory", TicketLts[0].SubCategory.SubCategory);
                messageBody = messageBody.Replace("@@Subject", TicketLts[0].Subject);
                messageBody = messageBody.Replace("@@Description", TicketLts[0].Description);
                messageBody = messageBody.Replace("@@ContactName", TicketLts[0].ContactName);
                messageBody = messageBody.Replace("@@MobileNo", TicketLts[0].MobileNo);
                messageBody = messageBody.Replace("@@ToName", userForceclosedBy.ContactName);
                messageBody = messageBody.Replace("@@Message", Forum.Message);
                messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
                messageBody = messageBody.Replace("@@Status", TicketLts[0].Status.Status);
                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);

                List<PMessage> PMessages = new List<PMessage>();
                PMessage Message = null;

                long LastMessageID = 0;
                string Msg = "";
                //List<PForum> Forums = new BTickets().GetForumDetails(Convert.ToInt32(TicketLts[0].HeaderID.ToString()));
                PApiResult ResultForum = new BTickets().GetForumDetails(TicketLts[0].HeaderID);
                List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(ResultForum.Data));
                foreach (PForum F in Forums)
                {
                    Message = new PMessage();
                    Message.Message = "<tr><td style='background-color: white;width:150px;'>" + F.FromUser.ContactName + "</td><td style='background-color: white;width:145px;'>" + F.CreatedOn + "</td><td style='background-color: white;color:blue'>" + F.Message + "</td></tr>";
                    Msg += Message.Message;

                    PMessages.Add(Message);
                    LastMessageID = F.ID;
                }
                messageBody = messageBody.Replace("@@Msg", "<table border='1' cellspacing='0' width='100%'><tr><th style='background-color: #696767;color: white;'>From</th><th style='background-color: #696767;color: white;'>Date</th><th style='background-color: #696767;color: white;'>Response</th style='background-color: #696767;color: white;'></tr>" + Msg + "</table>");
                new EmailManager().MailSend(userForceclosedBy.Mail, "", ConfigurationManager.AppSettings["TaskMailBcc"] + "," + CC, "AJAXOne-[Ticket No: " + TicketLts[0].HeaderID.ToString() + "] ForceClosed", messageBody, Convert.ToInt64(TicketLts[0].HeaderID.ToString()));
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
            TaskItem.HeaderID = Convert.ToInt64(ViewState["TicketNo"]);
            TaskItem.SubCategoryID = Convert.ToInt32(ddlSubcategory.SelectedValue);
            TaskItem.SeverityID = Convert.ToInt32(ddlSeverity.SelectedValue);
            TaskItem.AssignerRemark = txtAssignerRemark.Text.Trim();
            TaskItem.AssignedTo = Convert.ToInt32(ddlAssignedTo.SelectedValue);

            TaskItem.ActualDuration = 0;
            TaskItem.SupportType = ddlSupportType.SelectedValue;
            TaskItem.AttchedFile = AttchedFile;

            bool containsItem = Ticket[0].TicketItems.Any(item => item.HeaderID == TaskItem.HeaderID && item.AssignedTo.UserID == Convert.ToInt32(ddlAssignedTo.SelectedValue) && ((item.ItemStatus.StatusID == 2) || (item.ItemStatus.StatusID == 3) || (item.ItemStatus.StatusID == 4) || (item.ItemStatus.StatusID == 6) || (item.ItemStatus.StatusID == 8) || (item.ItemStatus.StatusID == 11)) && item.InActive == false);
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

            long TicketNo = Convert.ToInt64(ViewState["TicketNo"]);
            FillTickets(TicketNo);
            FillChat(TicketNo);
            FillChatTemp(TicketNo);

            string messageBody = "";
            PUser userCreatedBy = new BUser().GetUserDetails(Ticket[0].CreatedBy.UserID);
            PUser userAssignTo = new BUser().GetUserDetails(Convert.ToInt32(ddlAssignedTo.SelectedValue));
            PDealer dealer = new BDealer().GetDealerByID(null, userCreatedBy.ExternalReferenceID);
            messageBody = new EmailManager().GetMailNotificationTemplate("TicketAssign.htm");
            messageBody = messageBody.Replace("@@TicketNo", TaskItem.HeaderID.ToString());
            messageBody = messageBody.Replace("@@RequestedOn", Ticket[0].CreatedOn.ToString());
            messageBody = messageBody.Replace("@@DealerName", dealer.DealerCode + " - " + dealer.ContactName);
            messageBody = messageBody.Replace("@@TicketType", Ticket[0].Type.Type);
            messageBody = messageBody.Replace("@@Category", Ticket[0].Category.Category);
            messageBody = messageBody.Replace("@@Subcategory", Ticket[0].SubCategory.SubCategory);
            messageBody = messageBody.Replace("@@Subject", Ticket[0].Subject);
            messageBody = messageBody.Replace("@@Description", Ticket[0].Description);
            messageBody = messageBody.Replace("@@ContactName", Ticket[0].ContactName);
            messageBody = messageBody.Replace("@@MobileNo", Ticket[0].MobileNo);
            messageBody = messageBody.Replace("@@ToName", userCreatedBy.ContactName);
            messageBody = messageBody.Replace("@@AssignedTo", userAssignTo.ContactName);
            messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
            messageBody = messageBody.Replace("@@Status", Ticket[0].Status.Status);
            messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);

            List<PMessage> PMessages = new List<PMessage>();
            PMessage Message = null;

            long LastMessageID = 0;
            string Msg = "";
            //List<PForum> Forums = new BTickets().GetForumDetails(Convert.ToInt32(Ticket[0].HeaderID.ToString()));
            PApiResult ResultForum = new BTickets().GetForumDetails(Ticket[0].HeaderID);
            List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(ResultForum.Data));
            foreach (PForum F in Forums)
            {
                Message = new PMessage();
                Message.Message = "<tr><td style='background-color: white;width:150px;'>" + F.FromUser.ContactName + "</td><td style='background-color: white;width:145px;'>" + F.CreatedOn + "</td><td style='background-color: white;color:blue'>" + F.Message + "</td></tr>";
                Msg += Message.Message;

                PMessages.Add(Message);
                LastMessageID = F.ID;
            }
            messageBody = messageBody.Replace("@@Msg", "<table border='1' cellspacing='0' width='100%'><tr><th style='background-color: #696767;color: white;'>From</th><th style='background-color: #696767;color: white;'>Date</th><th style='background-color: #696767;color: white;'>Response</th style='background-color: #696767;color: white;'></tr>" + Msg + "</table>");
            new EmailManager().MailSend(userCreatedBy.Mail, "", ConfigurationManager.AppSettings["TaskMailBcc"] + "," + PSession.User.Mail + "," + userAssignTo.Mail, "AJAXOne-[Ticket No: " + TaskItem.HeaderID.ToString() + "] Assigned", messageBody, Convert.ToInt64(TaskItem.HeaderID.ToString()));
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
            PTaskItem_Insert Item = new PTaskItem_Insert();
            Item.HeaderID = Convert.ToInt64(ViewState["TicketNo"]);
            Item.AssignedTo = Approver;

            //int success = new BTickets().insertTicketApprovalDetails(PSession.User.UserID, Convert.ToInt32(ViewState["TicketNo"]), Approver);
            string result = new BAPI().ApiPut("Task/insertTicketApprovalDetails", Item);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblSendApproval.Text = "Ticket No " + Convert.ToInt64(ViewState["TicketNo"]) + "  is not successfully updated.";
                lblSendApproval.ForeColor = Color.Red;
                lblSendApproval.Visible = true;
                MPE_SendApproval.Show();
                return;
            }
            else
            {
                lblMessage.Text = "Ticket No " + Convert.ToInt64(ViewState["TicketNo"]) + " is successfully updated.";
                btnSendForApproval.Visible = false;
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
                long TicketNo = Convert.ToInt64(ViewState["TicketNo"]);
                FillTickets(TicketNo);
                FillChat(TicketNo);
                FillChatTemp(TicketNo);

                string messageBody = "";
                PUser UserApprovar = new BUser().GetUserDetails(Convert.ToInt32(ddlapprovar.SelectedValue));
                PUser userCreatedBy = new BUser().GetUserDetails(Ticket[0].CreatedBy.UserID);
                PUser userRequester = new BUser().GetUserDetails(PSession.User.UserID);
                PDealer dealer = new BDealer().GetDealerByID(null, userCreatedBy.ExternalReferenceID);
                messageBody = new EmailManager().GetMailNotificationTemplate("TicketApproval.htm");
                messageBody = messageBody.Replace("@@TicketNo", Ticket[0].HeaderID.ToString());
                messageBody = messageBody.Replace("@@ApproverName", UserApprovar.ContactName);
                messageBody = messageBody.Replace("@@CreatedByName", userCreatedBy.ContactName);
                messageBody = messageBody.Replace("@@RequestedOn", Ticket[0].CreatedOn.ToString());
                messageBody = messageBody.Replace("@@DealerName", dealer.DealerCode + " - " + dealer.ContactName);
                messageBody = messageBody.Replace("@@TicketType", Ticket[0].Type.Type);
                messageBody = messageBody.Replace("@@Category", Ticket[0].Category.Category);
                messageBody = messageBody.Replace("@@Subcategory", Ticket[0].SubCategory.SubCategory);
                messageBody = messageBody.Replace("@@Subject", Ticket[0].Subject);
                messageBody = messageBody.Replace("@@Description", Ticket[0].Description);
                messageBody = messageBody.Replace("@@ContactName", Ticket[0].ContactName);
                messageBody = messageBody.Replace("@@MobileNo", Ticket[0].MobileNo);
                messageBody = messageBody.Replace("@@ApprovalRequester", userRequester.ContactName);
                messageBody = messageBody.Replace("@@ApprovalDueDate", DateTime.Now.AddDays(10).ToString());
                messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
                messageBody = messageBody.Replace("@@Status", Ticket[0].Status.Status);
                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);

                List<PMessage> PMessages = new List<PMessage>();
                PMessage Message = null;

                long LastMessageID = 0;
                string Msg = "";
                //List<PForum> Forums = new BTickets().GetForumDetails(Convert.ToInt32(Ticket[0].HeaderID.ToString()));
                PApiResult ResultForum = new BTickets().GetForumDetails(Ticket[0].HeaderID);
                List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(ResultForum.Data));
                foreach (PForum F in Forums)
                {
                    Message = new PMessage();
                    Message.Message = "<tr><td style='background-color: white;width:150px;'>" + F.FromUser.ContactName + "</td><td style='background-color: white;width:145px;'>" + F.CreatedOn + "</td><td style='background-color: white;color:blue'>" + F.Message + "</td></tr>";
                    Msg += Message.Message;

                    PMessages.Add(Message);
                    LastMessageID = F.ID;
                }
                messageBody = messageBody.Replace("@@Msg", "<table border='1' cellspacing='0' width='100%'><tr><th style='background-color: #696767;color: white;'>From</th><th style='background-color: #696767;color: white;'>Date</th><th style='background-color: #696767;color: white;'>Response</th style='background-color: #696767;color: white;'></tr>" + Msg + "</table>");
                new EmailManager().MailSend(UserApprovar.Mail, "", ConfigurationManager.AppSettings["TaskMailBcc"] + "," + PSession.User.Mail + "," + userCreatedBy.Mail, "AJAXOne-[Ticket No: " + Ticket[0].HeaderID.ToString() + "] Request for Approval / Reject", messageBody, Convert.ToInt64(Ticket[0].HeaderID.ToString()));
            }
        }
        void InProgress()
        {
            PTaskItem_Insert TaskItem = new PTaskItem_Insert();

            foreach (PTicketItem ticketItem in Ticket[0].TicketItems)
            {
                if (ticketItem.ItemStatus.Status == "Assigned" && ticketItem.AssignedTo.UserID == PSession.User.UserID && ticketItem.InActive == false)
                {
                    TaskItem.HeaderID = ticketItem.HeaderID;
                    TaskItem.ItemID = ticketItem.ItemID;
                    TaskItem.StatusId = 3;
                }
            }

            //int Success = new BTickets().UpdateTicketStatus(TaskItem.ItemID, 3);

            string result = new BAPI().ApiPut("Task/UpdateTicketStatus", TaskItem);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Success)
            {
                lblMessage.Text = "Ticket No " + TaskItem.HeaderID + " is successfully updated.";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
                long TicketNo = Convert.ToInt64(ViewState["TicketNo"]);
                FillTickets(TicketNo);
                FillChat(TicketNo);
                FillChatTemp(TicketNo);

                string messageBody = "";
                PUser userCreatedBy = new BUser().GetUserDetails(Ticket[0].CreatedBy.UserID);
                PUser userAssignTo = new BUser().GetUserDetails(Convert.ToInt64(PSession.User.UserID));
                PDealer dealer = new BDealer().GetDealerByID(null, userCreatedBy.ExternalReferenceID);
                messageBody = new EmailManager().GetMailNotificationTemplate("TicketInprogress.htm");
                messageBody = messageBody.Replace("@@TicketNo", TaskItem.HeaderID.ToString());
                messageBody = messageBody.Replace("@@RequestedOn", Ticket[0].CreatedOn.ToString());
                messageBody = messageBody.Replace("@@DealerName", dealer.DealerCode + " - " + dealer.ContactName);
                messageBody = messageBody.Replace("@@TicketType", Ticket[0].Type.Type);
                messageBody = messageBody.Replace("@@Category", Ticket[0].Category.Category);
                messageBody = messageBody.Replace("@@Subcategory", Ticket[0].SubCategory.SubCategory);
                messageBody = messageBody.Replace("@@Subject", Ticket[0].Subject);
                messageBody = messageBody.Replace("@@Description", Ticket[0].Description);
                messageBody = messageBody.Replace("@@ContactName", Ticket[0].ContactName);
                messageBody = messageBody.Replace("@@MobileNo", Ticket[0].MobileNo);
                messageBody = messageBody.Replace("@@ToName", userCreatedBy.ContactName);
                messageBody = messageBody.Replace("@@AssignedTo", userAssignTo.ContactName);
                messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
                messageBody = messageBody.Replace("@@Status", Ticket[0].Status.Status);
                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);

                List<PMessage> PMessages = new List<PMessage>();
                PMessage Message = null;

                long LastMessageID = 0;
                string Msg = "";
                //List<PForum> Forums = new BTickets().GetForumDetails(Convert.ToInt32(Ticket[0].HeaderID.ToString()));
                PApiResult ResultForum = new BTickets().GetForumDetails(Ticket[0].HeaderID);
                List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(ResultForum.Data));
                foreach (PForum F in Forums)
                {
                    Message = new PMessage();
                    Message.Message = "<tr><td style='background-color: white;width:150px;'>" + F.FromUser.ContactName + "</td><td style='background-color: white;width:145px;'>" + F.CreatedOn + "</td><td style='background-color: white;color:blue'>" + F.Message + "</td></tr>";
                    Msg += Message.Message;

                    PMessages.Add(Message);
                    LastMessageID = F.ID;
                }
                messageBody = messageBody.Replace("@@Msg", "<table border='1' cellspacing='0' width='100%'><tr><th style='background-color: #696767;color: white;'>From</th><th style='background-color: #696767;color: white;'>Date</th><th style='background-color: #696767;color: white;'>Response</th style='background-color: #696767;color: white;'></tr>" + Msg + "</table>");
                new EmailManager().MailSend(userCreatedBy.Mail, "", ConfigurationManager.AppSettings["TaskMailBcc"] + "," + userAssignTo.Mail, "AJAXOne-[Ticket No: " + TaskItem.HeaderID.ToString() + "] is InProgress", messageBody, Convert.ToInt64(TaskItem.HeaderID.ToString()));
            }
        }
        void Cancel()
        {
            PTaskItem_Insert TaskItem = new PTaskItem_Insert();
            foreach (PTicketItem ticketItem in Ticket[0].TicketItems)
            {
                if ((ticketItem.ItemStatus.Status == "Assigned" || ticketItem.ItemStatus.Status == "In Progress") && ticketItem.AssignedTo.UserID == PSession.User.UserID && ticketItem.InActive == false)
                {
                    TaskItem.HeaderID = ticketItem.HeaderID;
                    TaskItem.ItemID = ticketItem.ItemID;
                }
            }
            string result = new BAPI().ApiPut("Task/UpdateTicketCancelStatus", TaskItem);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);
            if (Result.Status == PApplication.Success)
            {
                lblMessage.Text = "Ticket No " + TaskItem.HeaderID + " is successfully updated.";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
                long TicketNo = Convert.ToInt64(ViewState["TicketNo"]);
                FillTickets(TicketNo);
                FillChat(TicketNo);
                FillChatTemp(TicketNo);

                string messageBody = "";
                PUser userCreatedBy = new BUser().GetUserDetails(Ticket[0].CreatedBy.UserID);
                PUser userCancelledBy = new BUser().GetUserDetails(Convert.ToInt64(PSession.User.UserID));
                PDealer dealer = new BDealer().GetDealerByID(null, userCreatedBy.ExternalReferenceID);

                messageBody = new EmailManager().GetMailNotificationTemplate("TicketCancel.htm");
                messageBody = messageBody.Replace("@@TicketNo", Ticket[0].HeaderID.ToString());
                messageBody = messageBody.Replace("@@RequestedOn", Ticket[0].CreatedOn.ToString());
                messageBody = messageBody.Replace("@@DealerName", dealer.DealerCode + " - " + dealer.ContactName);
                messageBody = messageBody.Replace("@@TicketType", Ticket[0].Type.Type);
                messageBody = messageBody.Replace("@@Category", Ticket[0].Category.Category);
                messageBody = messageBody.Replace("@@Subcategory", Ticket[0].SubCategory.SubCategory);
                messageBody = messageBody.Replace("@@Subject", Ticket[0].Subject);
                messageBody = messageBody.Replace("@@Description", Ticket[0].Description);
                messageBody = messageBody.Replace("@@ContactName", Ticket[0].ContactName);
                messageBody = messageBody.Replace("@@MobileNo", Ticket[0].MobileNo);
                messageBody = messageBody.Replace("@@ToName", userCreatedBy.ContactName);
                messageBody = messageBody.Replace("@@Message", "This ticket to be Cancelled");
                messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
                messageBody = messageBody.Replace("@@Status", Ticket[0].Status.Status);
                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);

                List<PMessage> PMessages = new List<PMessage>();
                PMessage Message = null;

                long LastMessageID = 0;
                string Msg = "";
                //List<PForum> Forums = new BTickets().GetForumDetails(Convert.ToInt32(Ticket[0].HeaderID.ToString()));
                PApiResult ResultForum = new BTickets().GetForumDetails(Ticket[0].HeaderID);
                List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(ResultForum.Data));
                foreach (PForum F in Forums)
                {
                    Message = new PMessage();
                    Message.Message = "<tr><td style='background-color: white;width:150px;'>" + F.FromUser.ContactName + "</td><td style='background-color: white;width:145px;'>" + F.CreatedOn + "</td><td style='background-color: white;color:blue'>" + F.Message + "</td></tr>";
                    Msg += Message.Message;

                    PMessages.Add(Message);
                    LastMessageID = F.ID;
                }
                messageBody = messageBody.Replace("@@Msg", "<table border='1' cellspacing='0' width='100%'><tr><th style='background-color: #696767;color: white;'>From</th><th style='background-color: #696767;color: white;'>Date</th><th style='background-color: #696767;color: white;'>Response</th style='background-color: #696767;color: white;'></tr>" + Msg + "</table>");
                new EmailManager().MailSend(userCreatedBy.Mail, "", ConfigurationManager.AppSettings["TaskMailBcc"] + "," + userCancelledBy.Mail, "AJAXOne-[Ticket No: " + Ticket[0].HeaderID.ToString() + "] Cancelled", messageBody, Convert.ToInt64(Ticket[0].HeaderID.ToString()));
            }
            else
            {
                lblMessage.Text = Result.Message.ToString();
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
                return;
            }
        }
        void Close()
        {
            MPE_Close.Show();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (Ticket[0].Status.StatusID == 4)
            {
                PTask_Insert Task = new PTask_Insert();
                Task.HeaderID = Ticket[0].HeaderID;
                Task.Feedback = txtFeedBack.Text;
                Task.Rating = Convert.ToInt32(Rating.CurrentRating);
                string result = new BAPI().ApiPut("Task/UpdateTicketClosedStatus", Task);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Success)
                {
                    lblMessage.Text = "Ticket No " + Ticket[0].HeaderID + " is successfully updated.";
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Visible = true;
                    long TicketNo = Convert.ToInt64(ViewState["TicketNo"]);
                    FillTickets(TicketNo);
                    FillChat(TicketNo);
                    FillChatTemp(TicketNo);
                }
            }
        }
        protected void Rating_Click(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            HidRating.Value = Evaluate(int.Parse(e.Value), Rating.MaxRating, 0, 5);
            Rating.CurrentRating = Convert.ToInt32(Math.Round(Convert.ToDecimal(HidRating.Value)));
            MPE_Close.Show();
        }
        public static string Evaluate(int value, int maximalValue, int minimumRange, int maximumRange)
        {
            int stepDelta = (minimumRange == 0) ? 1 : 0;
            double delta = (double)(maximumRange - minimumRange) / (maximalValue - 1);
            double result = (delta * value - delta * stepDelta);
            return String.Format("{0:g}", value);
        }
        void Reopen()
        {
            if (Ticket[0].Status.StatusID == 4)
            {
                //int Success = new BTickets().UpdateTicketReopenStatus(Ticket[0].HeaderID, PSession.User.UserID);
                string result = new BAPI().ApiPut("Task/UpdateTicketReopenStatus", Ticket[0].HeaderID);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Success)
                {
                    lblMessage.Text = "Ticket No " + Ticket[0].HeaderID + " is successfully updated.";
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Visible = true;
                    long TicketNo = Ticket[0].HeaderID;
                    FillTickets(TicketNo);
                    FillChat(TicketNo);
                    FillChatTemp(TicketNo);

                    string CC = "";
                    List<string> EmailList = new List<string>();
                    var items = ddlMailNotification.Items;
                    PUser userCreatedBy = new BUser().GetUserDetails(Ticket[0].CreatedBy.UserID);
                    int? AssignTo = null;
                    foreach (PTicketItem Item in Ticket[0].TicketItems)
                    {
                        if (Item.InActive == false && Item.ItemStatus.StatusID == 2)
                        {
                            AssignTo = Item.AssignedTo.UserID;
                        }
                    }
                    PUser userAssignTo = new BUser().GetUserDetails(Convert.ToInt64(AssignTo));
                    foreach (var item in items.Cast<ListItem>().Where(x => x.Value != "-1" && x.Value != "0"))
                    {
                        PUser TicketUsers = new BUser().GetUserDetails(Convert.ToInt32(item.Value));
                        if (!EmailList.Contains(TicketUsers.Mail))
                        {
                            if (userCreatedBy.Mail != TicketUsers.Mail && PSession.User.Mail != TicketUsers.Mail && userAssignTo.Mail != TicketUsers.Mail)
                            {
                                EmailList.Add(TicketUsers.Mail);
                                CC = (CC == "") ? TicketUsers.Mail : "," + TicketUsers.Mail;
                            }
                        }
                    }
                    string messageBody = "";
                    PDealer dealer = new BDealer().GetDealerByID(null, userCreatedBy.ExternalReferenceID);
                    messageBody = new EmailManager().GetMailNotificationTemplate("TicketReopen.htm");
                    messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);
                    messageBody = messageBody.Replace("@@TicketNo", Ticket[0].HeaderID.ToString());
                    messageBody = messageBody.Replace("@@RequestedOn", Ticket[0].CreatedOn.ToString());
                    messageBody = messageBody.Replace("@@DealerName", dealer.DealerCode + " - " + dealer.ContactName);
                    messageBody = messageBody.Replace("@@TicketType", Ticket[0].Type.Type);
                    messageBody = messageBody.Replace("@@Category", Ticket[0].Category.Category);
                    messageBody = messageBody.Replace("@@Subcategory", Ticket[0].SubCategory.SubCategory);
                    messageBody = messageBody.Replace("@@Subject", Ticket[0].Subject);
                    messageBody = messageBody.Replace("@@Description", Ticket[0].Description);
                    messageBody = messageBody.Replace("@@ContactName", Ticket[0].ContactName);
                    messageBody = messageBody.Replace("@@MobileNo", Ticket[0].MobileNo);
                    messageBody = messageBody.Replace("@@ReopenDate", DateTime.Now.ToString());
                    messageBody = messageBody.Replace("@@AssignedTo", userAssignTo.ContactName);
                    messageBody = messageBody.Replace("@@fromName", PSession.User.ContactName);
                    messageBody = messageBody.Replace("@@Status", Ticket[0].Status.Status);

                    List<PMessage> PMessages = new List<PMessage>();
                    PMessage Message = null;

                    long LastMessageID = 0;
                    string Msg = "";
                    //List<PForum> Forums = new BTickets().GetForumDetails(Convert.ToInt32(Ticket[0].HeaderID.ToString()));
                    PApiResult ResultForum = new BTickets().GetForumDetails(Ticket[0].HeaderID);
                    List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(ResultForum.Data));
                    foreach (PForum F in Forums)
                    {
                        Message = new PMessage();
                        Message.Message = "<tr><td style='background-color: white;width:150px;'>" + F.FromUser.ContactName + "</td><td style='background-color: white;width:145px;'>" + F.CreatedOn + "</td><td style='background-color: white;color:blue'>" + F.Message + "</td></tr>";
                        Msg += Message.Message;

                        PMessages.Add(Message);
                        LastMessageID = F.ID;
                    }
                    messageBody = messageBody.Replace("@@Msg", "<table border='1' cellspacing='0' width='100%'><tr><th style='background-color: #696767;color: white;'>From</th><th style='background-color: #696767;color: white;'>Date</th><th style='background-color: #696767;color: white;'>Response</th style='background-color: #696767;color: white;'></tr>" + Msg + "</table>");
                    new EmailManager().MailSend(userCreatedBy.Mail + "," + userAssignTo.Mail, "", ConfigurationManager.AppSettings["TaskMailBcc"] + "," + PSession.User.Mail + "," + CC, "AJAXOne-[Ticket No: " + Ticket[0].HeaderID.ToString() + "] Re-Opened", messageBody, Ticket[0].HeaderID);
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
            else
            {
                if (fuResolve.PostedFile != null)
                {
                    if (AttchedFile.Count == 1)
                    {
                        PForum_Insert Forum = new PForum_Insert();
                        Forum.HeaderID = Convert.ToInt64(ViewState["TicketNo"]);
                        Forum.Message = AttchedFile[0].FileName;
                        Forum.AttchedFile = AttchedFile[0];
                        new BAPI().ApiPut("Task/Forum", Forum);
                        PApiResult Result1 = JsonConvert.DeserializeObject<PApiResult>(result);
                    }
                }
            }

            lblMessage.Text = "Ticket No " + TaskItem.HeaderID + " is successfully updated.";
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;

            long TicketNo = Convert.ToInt64(ViewState["TicketNo"]);
            FillTickets(TicketNo);
            FillChat(TicketNo);
            FillChatTemp(TicketNo);

            if (Ticket[0].Status.StatusID == 4)
            {
                string messageBody = "";
                PUser userCreatedBy = new BUser().GetUserDetails(Ticket[0].CreatedBy.UserID);
                PUser userAssignTo = new BUser().GetUserDetails(PSession.User.UserID);
                PDealer dealer = new BDealer().GetDealerByID(null, userCreatedBy.ExternalReferenceID);
                messageBody = new EmailManager().GetMailNotificationTemplate("TicketResolved.htm");
                messageBody = messageBody.Replace("@@TicketNo", TaskItem.HeaderID.ToString());
                messageBody = messageBody.Replace("@@RequestedOn", Ticket[0].CreatedOn.ToString());
                messageBody = messageBody.Replace("@@DealerName", dealer.DealerCode + " - " + dealer.ContactName);
                messageBody = messageBody.Replace("@@TicketType", Ticket[0].Type.Type);
                messageBody = messageBody.Replace("@@Category", Ticket[0].Category.Category);
                messageBody = messageBody.Replace("@@Subcategory", Ticket[0].SubCategory.SubCategory);
                messageBody = messageBody.Replace("@@Subject", Ticket[0].Subject);
                messageBody = messageBody.Replace("@@Description", Ticket[0].Description);
                messageBody = messageBody.Replace("@@ContactName", Ticket[0].ContactName);
                messageBody = messageBody.Replace("@@MobileNo", Ticket[0].MobileNo);
                messageBody = messageBody.Replace("@@ToName", userCreatedBy.ContactName);
                messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
                messageBody = messageBody.Replace("@@Status", Ticket[0].Status.Status);
                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);

                string CC = string.Empty;
                int mailindex = 0;
                foreach (PUser user in AjaxEmployee)
                {
                    CC += (mailindex == 0) ? user.Mail : "," + user.Mail;
                    mailindex += 1;
                }

                List<PMessage> PMessages = new List<PMessage>();
                PMessage Message = null;

                long LastMessageID = 0;
                string Msg = "";
                //List<PForum> Forums = new BTickets().GetForumDetails(Convert.ToInt32(Ticket[0].HeaderID.ToString()));
                PApiResult ResultForum = new BTickets().GetForumDetails(Ticket[0].HeaderID);
                List<PForum> Forums = JsonConvert.DeserializeObject<List<PForum>>(JsonConvert.SerializeObject(ResultForum.Data));
                foreach (PForum F in Forums)
                {
                    Message = new PMessage();
                    Message.Message = "<tr><td style='background-color: white;width:150px;'>" + F.FromUser.ContactName + "</td><td style='background-color: white;width:145px;'>" + F.CreatedOn + "</td><td style='background-color: white;color:blue'>" + F.Message + "</td></tr>";
                    Msg += Message.Message;

                    PMessages.Add(Message);
                    LastMessageID = F.ID;
                }
                messageBody = messageBody.Replace("@@Msg", "<table border='1' cellspacing='0' width='100%'><tr><th style='background-color: #696767;color: white;'>From</th><th style='background-color: #696767;color: white;'>Date</th><th style='background-color: #696767;color: white;'>Response</th style='background-color: #696767;color: white;'></tr>" + Msg + "</table>");
                new EmailManager().MailSend(userCreatedBy.Mail, CC, ConfigurationManager.AppSettings["TaskMailBcc"] + "," + userAssignTo.Mail, "AJAXOne-[Ticket No: " + TaskItem.HeaderID.ToString() + "] Resolved", messageBody, Convert.ToInt64(TaskItem.HeaderID.ToString()));
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
            ddlAjaxEmployee.SelectedValue = "0";
            gvResolveNewFileAttached.DataSource = null;
            gvResolveNewFileAttached.DataBind();
            gvAjaxEmployee.DataSource = null;
            gvAjaxEmployee.DataBind();
            AjaxEmployee = null;
            if (AttchedFile.Count > 0)
                AttchedFile.RemoveAt(0);
        }
        void FillResolutionType()
        {
            ddlResolutionType.DataTextField = "ResolutionType";
            ddlResolutionType.DataValueField = "ResolutionTypeID";
            ddlResolutionType.DataSource = JsonConvert.DeserializeObject<List<PResolutionType>>(JsonConvert.SerializeObject(new BTickets().getTicketResolutionType(null, null).Data));
            ddlResolutionType.DataBind();
            ddlResolutionType.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillAjaxEmployee()
        {
            List<PUser> user = new BUser().GetUsers(null, null, null, null, 53, true, null, null, null);
            new DDLBind(ddlAjaxEmployee, user, "ContactName", "UserID");
            gvAjaxEmployee.DataSource = null;
            gvAjaxEmployee.DataBind();
            AjaxEmployee = new List<PUser>();
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
            lbtnEditHeaderInfo.Visible = true;
            lbtnMessage.Visible = true;
            lbtnUploadFile.Visible = true;
            lbtnSendApproval.Visible = true;
            lbtnResendApproval.Visible = true;
            lbtnApprove.Visible = true;
            lbtnAssignTo.Visible = true;
            lbtnReassign.Visible = true;
            lbtnInProgress.Visible = true;
            lbtnResolve.Visible = true;
            lbtnCancel.Visible = true;
            lbtnForceclose.Visible = true;
            lbtnClose.Visible = true;
            lbtnReopen.Visible = true;
            lbtnReject.Visible = true;

            if (PSession.User.Department.DealerDepartmentID != 7)
            {
                lbtnEditHeaderInfo.Visible = false;
            }

            if (Ticket[0].Status.StatusID == 1)
            {
                lbtnClose.Visible = false;
                lbtnCancel.Visible = false;
                lbtnResolve.Visible = false;
                lbtnReassign.Visible = false;
                lbtnInProgress.Visible = false;
                lbtnApprove.Visible = false;
                lbtnReopen.Visible = false;
                lbtnReject.Visible = false;
                lbtnResendApproval.Visible = false;
            }
            if ((Ticket[0].Status.StatusID == 2) || (Ticket[0].Status.StatusID == 8))
            {
                if (!Ticket[0].ApprovalDetails.Any(item => item.Approver.ContactName == PSession.User.ContactName && item.InActive == false && item.RejectedBy == null && item.IsAppoved == null))
                {
                    lbtnApprove.Visible = false;
                }
                if (Ticket[0].TicketItems.Any(item => item.ItemStatus.StatusID == 2 && item.AssignedTo.UserID == PSession.User.UserID && item.InActive == false))
                {
                    lbtnClose.Visible = false;
                    //lbtnCancel.Visible = false;
                    lbtnResolve.Visible = false;
                    //lbtnInProgress.Visible = false;
                    lbtnReopen.Visible = false;
                }
                else if (Ticket[0].TicketItems.Any(item => item.ItemStatus.StatusID == 3 && item.AssignedTo.UserID == PSession.User.UserID && item.InActive == false))
                {
                    lbtnClose.Visible = false;
                    lbtnCancel.Visible = false;
                    //lbtnResolve.Visible = false;
                    lbtnReassign.Visible = false;
                    lbtnInProgress.Visible = false;
                    lbtnReopen.Visible = false;
                }
                else
                {
                    lbtnClose.Visible = false;
                    lbtnCancel.Visible = false;
                    lbtnResolve.Visible = false;
                    lbtnReassign.Visible = false;
                    lbtnInProgress.Visible = false;
                    lbtnReopen.Visible = false;
                }
            }
            if (Ticket[0].Status.StatusID == 3)
            {
                lbtnApprove.Visible = false;
                if (Ticket[0].TicketItems.Any(item => item.ItemStatus.StatusID == 3 && item.AssignedTo.UserID == PSession.User.UserID && item.InActive == false))
                {
                    lbtnClose.Visible = false;
                    //lbtnCancel.Visible = false;
                    //lbtnResolve.Visible = false;
                    lbtnReassign.Visible = false;
                    lbtnInProgress.Visible = false;
                    lbtnReopen.Visible = false;
                }
                else
                {
                    lbtnClose.Visible = false;
                    lbtnCancel.Visible = false;
                    lbtnResolve.Visible = false;
                    lbtnReassign.Visible = false;
                    lbtnInProgress.Visible = false;
                    lbtnReopen.Visible = false;
                }
            }
            if (Ticket[0].Status.StatusID == 4)
            {
                lbtnApprove.Visible = false;
                lbtnCancel.Visible = false;
                lbtnResolve.Visible = false;
                lbtnReassign.Visible = false;
                lbtnInProgress.Visible = false;
                lbtnForceclose.Visible = false;
                lbtnSendApproval.Visible = false;
                if (Ticket[0].CreatedBy.UserID != PSession.User.UserID)
                {
                    lbtnClose.Visible = false;
                    lbtnReopen.Visible = false;
                }
                lbtnReject.Visible = false;
                lbtnResendApproval.Visible = false;
            }

            if (Ticket[0].Status.StatusID == 6)
            {
                lbtnAssignTo.Visible = false;
                lbtnCancel.Visible = false;
                lbtnResolve.Visible = false;
                lbtnReassign.Visible = false;
                lbtnInProgress.Visible = false;
                lbtnClose.Visible = false;

                if (!Ticket[0].ApprovalDetails.Any(item => item.IsAppoved != true && item.Approver.ContactName == PSession.User.ContactName && item.InActive == false))
                {
                    lbtnApprove.Visible = false;
                    lbtnReject.Visible = false;
                    lbtnResendApproval.Visible = false;
                }
                lbtnReopen.Visible = false;
            }
            bool containsItemHideAll = Ticket.Any(item => item.Status.StatusID == 5 || item.Status.StatusID == 11);
            if (containsItemHideAll)
            {
                lbtnSendApproval.Visible = false;
                lbtnApprove.Visible = false;
                lbtnAssignTo.Visible = false;
                lbtnReassign.Visible = false;
                lbtnInProgress.Visible = false;
                lbtnResolve.Visible = false;
                lbtnCancel.Visible = false;
                lbtnForceclose.Visible = false;
                lbtnClose.Visible = false;
                lbtnReopen.Visible = false;
                lbtnReject.Visible = false;
                lbtnResendApproval.Visible = false;
            }
            if ((Ticket[0].Status.StatusID != 6) || (Ticket[0].ApprovalDetails.Any(item => item.Approver.ContactName != PSession.User.ContactName && item.InActive == false) && Ticket[0].ApprovalDetails.Any(item => item.IsAppoved == true && (item.RejectedBy != null) ? (item.RejectedBy.ContactName == PSession.User.ContactName) : (item.Approver.ContactName == PSession.User.ContactName))))
            {
                if (!Ticket[0].ApprovalDetails.Any(item => item.Approver.ContactName == PSession.User.ContactName && item.InActive == false && item.RejectedBy == null && item.IsAppoved == null))
                {
                    lbtnReject.Visible = false;
                    lbtnApprove.Visible = false;
                    lbtnResendApproval.Visible = false;
                }
            }
            if (Ticket[0].Status.StatusID == 12)
            {
                lbtnSendApproval.Visible = false;
                lbtnApprove.Visible = false;
                lbtnAssignTo.Visible = false;
                lbtnReassign.Visible = false;
                lbtnInProgress.Visible = false;
                lbtnResolve.Visible = false;
                lbtnCancel.Visible = false;
                lbtnForceclose.Visible = false;
                lbtnClose.Visible = false;
                lbtnReopen.Visible = false;
                lbtnReject.Visible = false;
                lbtnResendApproval.Visible = false;
            }
            if (Ticket[0].TicketItems.Count > 0 && Ticket[0].Status.StatusID != 1)
            {
                lbtnAssignTo.Visible = false;
            }
            if (Ticket[0].ApprovalDetails.Count > 0 && Ticket[0].Status.StatusID != 1)
            {
                lbtnSendApproval.Visible = false;
            }

            if (PSession.User.UserID == 1 || PSession.User.UserID == 382 || PSession.User.UserID == 491
       || PSession.User.UserID == 383 || PSession.User.UserID == 2954)
            {

            }
            else
            {
                lbtnAssignTo.Visible = false;
                lbtnSendApproval.Visible = false;
            }
            if (PSession.User.UserID == 1)
            {

            }
            else
            {
                lbtnForceclose.Visible = false;
            }
            if (Ticket[0].Status.StatusID != 1)
            {
                lbtnSendApproval.Visible = false;
                lbtnResendApproval.Visible = false;
            }
        }

        protected void btnUpdateHeaderInfo_Click(object sender, EventArgs e)
        {
            if (!validationHeaderEditInfo())
            {
                lblMessageHeaderEdit.ForeColor = Color.Red;
                lblMessageHeaderEdit.Visible = true;
                MPE_EditHeaderInfo.Show();
                return;
            }
            PTask_Insert TaskHeader = new PTask_Insert();
            TaskHeader.HeaderID = Convert.ToInt64(ViewState["TicketNo"]);
            TaskHeader.CategoryID = Convert.ToInt32(ddlEditCategory.SelectedValue);
            TaskHeader.SubCategoryID = Convert.ToInt32(ddlEditSubCategory.SelectedValue);
            TaskHeader.TicketTypeID = Convert.ToInt32(ddlEditTicketType.SelectedValue);
            TaskHeader.SeverityID = Convert.ToInt32(ddlEditSeverity.SelectedValue);
            TaskHeader.Subject = txtEditSubject.Text.Trim();
            TaskHeader.Description = txtEditDescription.Text.Trim();
            string result = new BAPI().ApiPut("Task/UpdateHeaderInfo", TaskHeader);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessageHeaderEdit.Text = "Ticket Not Updated successfully.";
                lblMessageHeaderEdit.ForeColor = Color.Red;
                lblMessageHeaderEdit.Visible = true;
                MPE_EditHeaderInfo.Show();
                return;
            }
            else
            {
                lblMessage.Text = "Ticket No " + Convert.ToInt64(ViewState["TicketNo"]) + " is successfully updated.";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
                long TicketNo = Convert.ToInt64(ViewState["TicketNo"]);
                FillTickets(TicketNo);
                FillChat(TicketNo);
                FillChatTemp(TicketNo);
            }
        }
        protected void ddlEditCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEditSubCategory();
            MPE_EditHeaderInfo.Show();
        }
        void FillEditInfo(long TicketNo)
        {
            ddlEditCategory.SelectedValue = Convert.ToString(Ticket[0].Category.CategoryID);
            FillEditSubCategory();
            if (Ticket[0].SubCategory != null)
            {
                ddlEditSubCategory.SelectedValue = Convert.ToString(Ticket[0].SubCategory.SubCategoryID);
            }
            if (Ticket[0].Type != null)
            {
                ddlEditTicketType.SelectedValue = Convert.ToString(Ticket[0].Type.TypeID);
            }
            if (Ticket[0].Severity != null)
            {
                ddlEditSeverity.SelectedValue = Convert.ToString(Ticket[0].Severity.SeverityID);
            }
            txtEditSubject.Text = Ticket[0].Subject.ToString();
            txtEditDescription.Text = Ticket[0].Description.ToString();
        }

        protected void ddlAssignDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? DealerID = null;
            DealerID = (ddlAssignDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlAssignDealer.SelectedValue);
            new BDMS_Dealer().GetDealerDepartmentDDL(ddlAssignDepartment, null, null);
            ddlAssignDepartment.SelectedValue = PSession.User.Department.DealerDepartmentID.ToString();
            List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, null, null);
            new DDLBind(ddlAssignedTo, user, "ContactName", "UserID");
            ddlAssignDepartment_SelectedIndexChanged(null, null);
            MPE_AssignTo.Show();
        }

        protected void ddlAssignDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? DealerID = null, DepartmentID = null;
            DealerID = (ddlAssignDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlAssignDealer.SelectedValue);
            DepartmentID = (ddlAssignDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlAssignDepartment.SelectedValue);
            List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DepartmentID, null);
            new DDLBind(ddlAssignedTo, user, "ContactName", "UserID");
            MPE_AssignTo.Show();
        }

        protected void BtnAddAjaxEmployee_Click(object sender, EventArgs e)
        {
            lblResolve.ForeColor = Color.Red;
            lblResolve.Visible = true;
            if (ddlAjaxEmployee.SelectedValue == "0")
            {
                lblResolve.Text = "Please Add Ajax Employee...!";
                MPE_Resolve.Show();
                return;
            }
            if (AjaxEmployee.Any(item => item.UserID == Convert.ToInt32(ddlAjaxEmployee.SelectedValue)))
            {
                lblResolve.Text = "Ajax Employee Already Exist...!";
                MPE_Resolve.Show();
                return;
            }
            PUser User = new BUser().GetUserDetails(Convert.ToInt32(ddlAjaxEmployee.SelectedValue));
            if (string.IsNullOrEmpty(User.Mail))
            {
                lblResolve.Text = "Mail ID Not Available...!";
                MPE_Resolve.Show();
                return;
            }
            AjaxEmployee.Add(User);
            gvAjaxEmployee.DataSource = AjaxEmployee;
            gvAjaxEmployee.DataBind();
            MPE_Resolve.Show();
        }

        protected void lbDelete_Click(object sender, EventArgs e)
        {
            gvAjaxEmployee.DataSource = null;
            gvAjaxEmployee.DataBind();

            LinkButton lbDelete = (LinkButton)sender;
            GridViewRow Grow = (GridViewRow)lbDelete.NamingContainer;
            Label lblAjaxUserID = (Label)Grow.FindControl("lblAjaxUserID");
            f:
            int Index = 0;
            foreach (PUser user in AjaxEmployee)
            {
                if (user.UserID.ToString() == lblAjaxUserID.Text)
                {
                    AjaxEmployee.RemoveAt(Index);
                    goto f;
                }
                Index = Index + 1;
            }
            gvAjaxEmployee.DataSource = AjaxEmployee;
            gvAjaxEmployee.DataBind();
            MPE_Resolve.Show();
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