using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class SupportTicketView : System.Web.UI.Page
    {
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            } 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
             
            if (!IsPostBack)
            {
                //new BEmployees().CheckPermition(0);
                if (!string.IsNullOrEmpty(Request.QueryString["TicketNo"]))
                {
                    int TicketNo = Convert.ToInt32(Request.QueryString["TicketNo"]);
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
        }
        void FillTickets(int? TicketNO)
        {
            List<PTicketHeader> Ticket = new BTickets().GetTicketDetails(TicketNO, null, null, null, null, null, null, null, null, null);
            gvTickets.DataSource = Ticket;
            gvTickets.DataBind();

            gvTicketItem.DataSource = Ticket[0].TicketItems;
            gvTicketItem.DataBind();

            gvApprover.DataSource = Ticket[0].ApprovalDetails;
            gvApprover.DataBind();
        }

        void FillChat(int TicketNO)
        {
            List<PMessage> PMessages = new List<PMessage>();
            PMessage Message = null;

            // dt.Rows.Add("<p style = 'text-align: right'><U>" + F.FromUser.ContactName + "</U> </br> <p style = 'padding-left : 10px; text-align: right'>" + F.Message + "</p></p>","Right");
            //  dt.Rows.Add("<U>" + F.FromUser.ContactName + "</U> </br> <p style = 'padding-left : 10px'>" + F.Message + "</p>","Left");
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
        void FillChatTemp(int TicketNO)
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
            GridView1.DataSource = PMessages;
            GridView1.DataBind();
            if (LastMessageID != 0)
                new BForum().UdateMessageViewStatus(TicketNO, PSession.User.UserID, LastMessageID);
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {

            PForum_Insert Forum = new PForum_Insert();
            Forum.HeaderID = Convert.ToInt32(Request.QueryString["TicketNo"]);

            if (txtMessage.Visible == true)
            {
                if (string.IsNullOrEmpty(txtMessage.Text.Trim()))
                {
                    //lblMessage.Text = "Please enter the message.";
                    //lblMessage.ForeColor = Color.Red;
                    //lblMessage.Visible = true;
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
            // new BTickets().insertForum(TicketNo, PSession.User.UserID, Message, FileName);

            string result = new BAPI().ApiPut("Task/Forum", Forum);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);
            //result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data);

            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }

            FillChat(Forum.HeaderID);
            AttchedFile.RemoveAt(0);
        }

        protected void rbMessage_CheckedChanged(object sender, EventArgs e)
        {
            txtMessage.Text = "";
            if (rbMessage.Checked)
            {
                txtMessage.Visible = true;
                FileUpload.Visible = false;
                btnSend.Text = "Send";
            }
            else
            {
                txtMessage.Visible = false;
                FileUpload.Visible = true;
                btnSend.Text = "Upload File";
            }
            btnSend.Focus();
        }


        void Download(int Index)
        {
            LinkButton lnkDownload = (LinkButton)gvchar.Rows[Index].FindControl("lnkDownload");
            Label lblID = (Label)gvchar.Rows[Index].FindControl("lblID");
            string Formate = lnkDownload.Text.Split('.')[lnkDownload.Text.Split('.').Count() - 1];

            //Response.ContentType = ContentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + lnkDownload.Text);
            //Response.WriteFile(ConfigurationManager.AppSettings["BasePath"] + "/AttachedFile/" + lblID.Text + "." + Formate);
            //Response.End();


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

        protected void ibFileUpload_Click(object sender, ImageClickEventArgs e)
        {
            txtMessage.Text = "";

            txtMessage.Visible = false;
            FileUpload.Visible = true;
            btnSend.Text = "Upload File";
            btnSend.Visible = true;
            btnSend.Focus();
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