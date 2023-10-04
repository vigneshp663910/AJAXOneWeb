using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class RequestSupportTicket : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSupportTicket_RequestSupportTicket; } }
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » New');</script>");
             
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                FillTicketType();
                FillCategory();
                FillTickets();
            }
            if (IsPostBack && fu.PostedFile != null)
            {
                if (fu.PostedFile.FileName.Length > 0)
                { 
                    lblMessage.Visible = true;
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
                    gvFileAttached.DataSource = AttchedFile;
                    gvFileAttached.DataBind();
                }
            }
        }
        void FillTicketType()
        {
            ddlTicketType.DataTextField = "Type";
            ddlTicketType.DataValueField = "TypeID";
            ddlTicketType.DataSource = new BTicketType().getTicketType(null, null);
            ddlTicketType.DataBind();
            ddlTicketType.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillCategory()
        {
            ddlCategory.DataTextField = "Category";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataSource = new BTicketCategory().getTicketCategory(null, null);
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillSubCategory()
        {
            ddlSubcategory.DataTextField = "SubCategory";
            ddlSubcategory.DataValueField = "SubCategoryID";
            ddlSubcategory.DataSource = new BTicketSubCategory().getTicketSubCategory(null, null, Convert.ToInt32(ddlCategory.SelectedValue));
            ddlSubcategory.DataBind();
            ddlSubcategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubCategory();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!validatetion())
            {
                return;
            } 
            long TicketId;
            PTask_Insert Task = new PTask_Insert();
            Task.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            Task.TicketTypeID = Convert.ToInt32(ddlTicketType.SelectedValue);
            Task.SubCategoryID = Convert.ToInt32(ddlSubcategory.SelectedValue) == 0 ? (int?)null : Convert.ToInt32(ddlSubcategory.SelectedValue);
            Task.Repeat = false;
            Task.Subject = txtSubject.Text;
            Task.Description = txtTicketDescription.Text;
            Task.MobileNo = txtMobileNo.Text;
            Task.ContactName = txtContactName.Text;
            Task.ActualCreater = PSession.User.UserID;
            Task.PriorityLevel = 1;
            Task.AttchedFile = AttchedFile;
            string result = new BAPI().ApiPut("Task", Task);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }
            lblMessage.Text = Result.Message+" Ticket No : "+ Result.Data;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            string CC = "";
            string messageBody = "";
            PDealer dealer = new BDealer().GetDealerByID(null, PSession.User.ExternalReferenceID);
            messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/TicketCreate.htm");
            messageBody = messageBody.Replace("@@TicketNo", Result.Data.ToString());
            messageBody = messageBody.Replace("@@RequestedOn", DateTime.Now.ToString());
            messageBody = messageBody.Replace("@@DealerName", dealer.DealerCode + " - " + dealer.ContactName);
            messageBody = messageBody.Replace("@@TicketType", ddlTicketType.SelectedItem.Text);
            messageBody = messageBody.Replace("@@Category", ddlCategory.SelectedItem.Text);            
            messageBody = messageBody.Replace("@@Subcategory", ddlSubcategory.SelectedItem.Text);
            messageBody = messageBody.Replace("@@Subject", txtSubject.Text);
            messageBody = messageBody.Replace("@@Description", txtTicketDescription.Text);
            messageBody = messageBody.Replace("@@ContactName", txtContactName.Text);
            messageBody = messageBody.Replace("@@MobileNo", txtMobileNo.Text);
            messageBody = messageBody.Replace("@@ToName", PSession.User.ContactName);
            messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
            messageBody = messageBody.Replace("@@Status", "Open");
            messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);

            List<PMessage> PMessages = new List<PMessage>();
            PMessage Message = null;
            long LastMessageID = 0;
            string Msg = "";
            List<PForum> Forums = new BTickets().GetForumDetails(Convert.ToInt32(Result.Data.ToString()));
            foreach (PForum F in Forums)
            {
                Message = new PMessage();
                Message.Message = "<tr><td style='background-color: white;width:150px;'>" + F.FromUser.ContactName + "</td><td style='background-color: white;width:145px;'>" + F.CreatedOn + "</td><td style='background-color: white;'>" + F.Message + "</td></tr>";
                Msg += Message.Message;

                PMessages.Add(Message);
                LastMessageID = F.ID;
            }
            messageBody = messageBody.Replace("@@Msg", "<table border='1' cellspacing='0' width='100%'><tr><th style='background-color: #696767;color: white;'>From</th><th style='background-color: #696767;color: white;'>Date</th><th style='background-color: #696767;color: white;'>Response</th style='background-color: #696767;color: white;'></tr>" + Msg + "</table>");
            new EmailManager().MailSend(PSession.User.Mail, CC, ConfigurationManager.AppSettings["TaskMailBcc"], "AJAXOne-[Ticket No: " + Result.Data + "] Created", messageBody, Convert.ToInt64(Result.Data));

            ClearField();            
        }

        void ClearField()
        {
            ddlCategory.SelectedValue = "0";
            FillSubCategory();
            ddlTicketType.SelectedValue = "0";
            txtSubject.Text = string.Empty;
            txtTicketDescription.Text = string.Empty;
            txtContactName.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            // txtPriorityLevel.Text = "1";
            AttchedFile.Clear();
            gvFileAttached.DataSource = AttchedFile;
            gvFileAttached.DataBind();
        }
        protected void Remove_Click(object sender, EventArgs e)
        {
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
            Label file = (Label)Grow.FindControl("lbltest");
            string fileName = file.Text;

            int fileIndex = 0;
            foreach (PAttachedFile file1 in AttchedFile)
            {
                if (file1.FileName == file.Text)
                {
                    AttchedFile.RemoveAt(fileIndex); 
                    if(AttchedFile.Count==0)
                    {
                        break;
                    }
                }
                fileIndex = fileIndex + 1;
            }  
            gvFileAttached.DataSource = AttchedFile;
            gvFileAttached.DataBind();
        }

        void FillTickets()
        {
            int RowCount = 0;
            gvTickets.DataSource = new BTickets().GetTicketDetails(null, null, null, null, null, null, null, null, PSession.User.UserID, "Open,Assigned",null,null, 1, 10000, out RowCount);
            gvTickets.DataBind();
        }

        Boolean validatetion()
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            if (ddlCategory.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the category";
                return false;
            }
            if (ddlSubcategory.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Sub Category";
                return false;
            }
            if (ddlTicketType.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the ticket type";
                return false;
            }

            if (string.IsNullOrEmpty(txtSubject.Text.Trim()))
            {
                lblMessage.Text = "Please enter the subject";
                return false;
            }
            if (string.IsNullOrEmpty(txtTicketDescription.Text.Trim()))
            {
                lblMessage.Text = "Please enter the ticket note";
                return false;
            }
            //int value;
            //if (!int.TryParse("0" + txtPriorityLevel.Text, out value))
            //{
            //    lblMessage.Text = "Please enter integer in priority level";
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtContactName.Text.Trim()))
            {
                lblMessage.Text = "Please enter the contact name";
                return false;
            }
            if (string.IsNullOrEmpty(txtMobileNo.Text.Trim()))
            {
                lblMessage.Text = "Please enter 10 digit mobile number";
                return false;
            }
            if (txtMobileNo.Text.Trim().Length != 10)
            {
                lblMessage.Text = "Please enter 10 digit mobile number";
                return false;
            }
            long value;
            if (!long.TryParse("0" + txtMobileNo.Text, out value))
            {
                lblMessage.Text = "Please enter integer in mobile number";
                return false;
            }

            lblMessage.Visible = false;
            return true;

        }
    }
}