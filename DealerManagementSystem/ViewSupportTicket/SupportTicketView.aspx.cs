using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class SupportTicketView : System.Web.UI.Page
    {
        private List<string> AttchedFile
        {
            get
            {
                if (ViewState["AttchedFile"] == null)
                {
                    ViewState["AttchedFile"] = new List<string>();
                }
                return (List<string>)ViewState["AttchedFile"];
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = new BDealer().DealerMaster();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
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

                    string path = ConfigurationManager.AppSettings["BasePath"] + "/File/" + PSession.User.UserName + "/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileUpload.SaveAs(path + FileUpload.PostedFile.FileName.Split('\\')[FileUpload.PostedFile.FileName.Split('\\').Count() - 1]);
                    AttchedFile.Add(FileUpload.PostedFile.FileName.Split('\\')[FileUpload.PostedFile.FileName.Split('\\').Count() - 1]);

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

            int TicketNo = Convert.ToInt32(Request.QueryString["TicketNo"]);
            string FileName = "";
            string Message = "";
            if (txtMessage.Visible == true)
            {
                if (string.IsNullOrEmpty(txtMessage.Text.Trim()))
                {
                    //lblMessage.Text = "Please enter the message.";
                    //lblMessage.ForeColor = Color.Red;
                    //lblMessage.Visible = true;
                    return;
                }
                Message = txtMessage.Text;
                txtMessage.Text = string.Empty;
            }
            else
            {
                if (AttchedFile.Count == 1)
                {
                    Message = AttchedFile[0];
                    FileName = AttchedFile[0];
                }
                else
                {
                    return;
                }
            }

            btnSend.Focus();
            new BTickets().insertForum(TicketNo, PSession.User.UserID, Message, FileName);
            FillChat(TicketNo);


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

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + lnkDownload.Text);
            Response.WriteFile(ConfigurationManager.AppSettings["BasePath"] + "/AttachedFile/" + lblID.Text + "." + Formate);
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