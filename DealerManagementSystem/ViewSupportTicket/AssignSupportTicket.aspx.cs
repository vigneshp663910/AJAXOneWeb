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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class AssignSupportTicket : System.Web.UI.Page
    {
        private List<PAttachedFile> AttchedFile
        {
            get
            {
                if (ViewState["NewAttchedFile"] == null)
                {
                    ViewState["NewAttchedFile"] = new List<PAttachedFile>();
                }
                return (List<PAttachedFile>)ViewState["NewAttchedFile"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Assigned');</script>");

            if (!IsPostBack)
            {

                FillCategory();
                FillTicketSeverity();
                FillTicketType();
                FillStatus();
                FillAssignTo();

                if (!string.IsNullOrEmpty(Request.QueryString["TicketNo"]))
                {
                    int TicketNo = Convert.ToInt32(Request.QueryString["TicketNo"]);
                    FillAllFields(TicketNo);
                    txtTicketNo.Text = TicketNo.ToString();
                    int status = Convert.ToInt32(ddlStatus.SelectedValue);
                    if (status == 5)
                    {

                        btnSave.Visible = false;
                    }
                    FillTickets(TicketNo);
                }
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

        void FillStatus()
        {
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataSource = new BTicketStatus().getTicketStatus(null, null);
            ddlStatus.DataBind();
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
        protected void txtTicketNo_TextChanged(object sender, EventArgs e)
        {
            FillAllFields(Convert.ToInt32(txtTicketNo.Text.Trim()));
        }
        void FillAllFields(int TicketNo)
        {
            //if (string.IsNullOrEmpty(TicketNo))
            //{
            //    TicketNo = txtTicketNo.Text.Trim();
            //}
            PTicketHeader Tickets = new PTicketHeader();
            // Tickets = new BTickets().GeTicketsByTicketNo(TicketNo);
            Tickets = new BTickets().GetOpenTickets(TicketNo, null, null, null, null, null, PSession.User.UserID)[0];

            txtRequestedOn.Text = Convert.ToString(Tickets.CreatedOn);
            ddlCategory.SelectedValue = Convert.ToString(Tickets.Category.CategoryID);
            FillSubCategory();

            if (Tickets.SubCategory != null)
            {
                ddlSubcategory.SelectedValue = Convert.ToString(Tickets.SubCategory.SubCategoryID);
            }

            if (Tickets.Severity != null)
            {
                ddlCategory.Enabled = false;
                ddlSubcategory.Enabled = false;
                ddlSeverity.Enabled = false;
            }

            if (Tickets.Severity != null)
            {
                ddlSeverity.SelectedValue = Convert.ToString(Tickets.Severity.SeverityID);
            }
            txtRequestedBy.Text = Tickets.CreatedBy.ContactName + " " + txtRequestedOn.Text;
            ddlTicketType.SelectedValue = Convert.ToString(Tickets.Type.TypeID);
            //if (Tickets.AssignedTo != null)
            //{
            //    ddlAssignedTo.SelectedValue = Convert.ToString(Tickets.AssignedTo.EID);
            //}

            ddlStatus.SelectedValue = Convert.ToString(Tickets.Status.StatusID);
            txtRequesterRemark.Text = Tickets.Description;

            //if (Tickets.ActualDuration != null)
            //{
            //    txtActualDuration.Text = Convert.ToString(Tickets.ActualDuration);
            //}

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
            ViewState["RequestedBy"] = Tickets.CreatedBy.UserName;

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!validatetion())
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                return;
            }

            PTaskItem_Insert TaskItem = new PTaskItem_Insert();
            TaskItem.HeaderID = Convert.ToInt32(txtTicketNo.Text.Trim());
            TaskItem.SubCategoryID = Convert.ToInt32(ddlSubcategory.SelectedValue);
            TaskItem.SeverityID = Convert.ToInt32(ddlSeverity.SelectedValue);
            TaskItem.AssignerRemark = txtAssignerRemark.Text.Trim();
            TaskItem.AssignedTo = Convert.ToInt32(ddlAssignedTo.SelectedValue);

            TaskItem.ActualDuration = 0;
            TaskItem.SupportType = ddlSupportType.SelectedValue;
            TaskItem.AttchedFile = AttchedFile;

            //  int status = new BTickets().InsertTicketItem( ,, 0,   file, ddlSupportType.SelectedValue);
            string result = new BAPI().ApiPut("Task/TaskItem", TaskItem);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);
            //result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data);

            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            } 

            lblMessage.Text = "Ticket No " + TaskItem.HeaderID + " is successfully updated.";
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;
             
            string messageBody = "";
            string Subject = "New Ticket " + TaskItem.HeaderID;
             
            PUser userAssignedTo = new BUser().GetUserDetails(Convert.ToInt32(ddlAssignedTo.SelectedValue));

            messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/TicketAssign.htm");
            if (userAssignedTo.SystemCategoryID == (short)SystemCategory.AF)
            {
                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"] + "AssignedTickets.aspx?TicketNo=" + TaskItem.HeaderID);
            }
            else
            {
                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URLDealer"] + "Login.aspx");
            }
            messageBody = messageBody.Replace("@@TicketNo", TaskItem.HeaderID.ToString());
            messageBody = messageBody.Replace("@@ToName", userAssignedTo.ContactName);
            messageBody = messageBody.Replace("@@RequestedOn", txtRequestedOn.Text);
            messageBody = messageBody.Replace("@@Category", ddlCategory.SelectedItem.Text);
            messageBody = messageBody.Replace("@@Subcategory", ddlSubcategory.SelectedItem.Text);
            messageBody = messageBody.Replace("@@Severity", ddlSeverity.SelectedItem.Text);
            messageBody = messageBody.Replace("@@TicketType", ddlTicketType.SelectedItem.Text);
            messageBody = messageBody.Replace("@@Description", txtRequesterRemark.Text);
            messageBody = messageBody.Replace("@@Justification", txtAssignerRemark.Text);
            //  messageBody = messageBody.Replace("@@ActualDuration", Convert.ToString(ActualDuration));
            messageBody = messageBody.Replace("@@fromName", PSession.User.ContactName);
            new EmailManager().MailSend(userAssignedTo.Mail, Subject, messageBody, TaskItem.HeaderID);
            ClearField();
            btnSave.Visible = false;
        }
        void ClearField()
        {

            //  txtDepartment.Text = string.Empty;
            //  ddlTicketType.SelectedValue = "1";
            // ddlCategory.SelectedValue = "1";
            txtAssignerRemark.Text = string.Empty;
            //  BulletedList1.Items.Clear();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubCategory();
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

        Boolean validatetion()
        {
            decimal parsedValue;
            if (btnSave.Text == "Assign")
            {
                if (ddlCategory.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select The Category";
                    return false;
                }
                if (ddlSubcategory.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select The Subcategory";
                    return false;
                }
                if (ddlSeverity.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select The Severity";
                    return false;
                }
                if (ddlAssignedTo.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select The Assigned To";
                    return false;
                }
                //if (ddlResolutionType.SelectedValue == "0")
                //{
                //    lblMessage.Text = "Please Select The Resolution Type";
                //    return false;
                //}


                //if (!decimal.TryParse(txtActualDuration.Text, out parsedValue))
                //{
                //    lblMessage.Text = "Please Enter number in Actual Duration !";
                //    txtActualDuration.Focus();
                //    return false;
                //}
            }

            return true;
        }

        protected void Remove_Click(object sender, EventArgs e)
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
            gvFileAttached.DataSource = AttchedFile;
            gvFileAttached.DataBind();
        }

        protected void cbAllEmp_CheckedChanged(object sender, EventArgs e)
        {
            FillAssignTo();
        }

        void FillTickets(int? TicketNO)
        {
            //List<PTicketHeader> Ticket = new BTickets().GetTicketDetails(TicketNO, null, null, null, null, null, null, null, null, null);
            List<PTicketHeader> Ticket = new BTickets().GetTicketByID(TicketNO);
            gvTickets.DataSource = Ticket;
            gvTickets.DataBind();
        }
    }
}