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
    public partial class InProgressSupportTicket : System.Web.UI.Page
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            // if (Membership.GetUser() == null) 
            this.Page.MasterPageFile = "~/Dealer.master";
            //  else
            //    this.Page.MasterPageFile = "~/myMaster.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » InProgress');</script>");

            if (!IsPostBack)
            {
                FillCategory();
                FillTicketSeverity();
                FillTicketType();
                FillResolutionType();

                FillTickets(null);
                //if (PSession.User.UserTypeID == (short)UserTypes.Manager || PSession.User.UserTypeID == (short)UserTypes.Admin)
                //{
                //    gvTickets.Columns[17].Visible = true;
                //}
                //else if (PSession.User.UserTypeID == (short)UserTypes.Associate)
                //{
                //    gvTickets.Columns[17].Visible = false;
                //}
            }
            if (IsPostBack && fu.PostedFile != null)
            {
                if (fu.PostedFile.FileName.Length > 0)
                {
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
            ddlTicketType.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillResolutionType()
        {
            ddlResolutionType.DataTextField = "ResolutionType";
            ddlResolutionType.DataValueField = "ResolutionTypeID";
            ddlResolutionType.DataSource = new BTicketResolutionType().getTicketResolutionType(null, null);
            ddlResolutionType.DataBind();
            ddlResolutionType.Items.Insert(0, new ListItem("Select", "0"));
        }


        void FillTickets(int? ItemId)
        {

            int? HeaderId = string.IsNullOrEmpty(txtTicketNo.Text.Trim()) ? (int?)null : Convert.ToInt32(txtTicketNo.Text.Trim());
            int? TicketCategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            int? TicketSubCategoryID = null;
            if (ddlSubcategory.Items.Count > 0)
            {
                TicketSubCategoryID = ddlSubcategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSubcategory.SelectedValue);
            }
            int? TicketSeverity = ddlSeverity.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSeverity.SelectedValue);
            int? TicketType = ddlTicketType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTicketType.SelectedValue);




            string AssignedTo = PSession.User.UserName;

            gvTickets.DataSource = new BTickets().GetInProgressTickets(HeaderId, ItemId, TicketCategoryID, TicketSubCategoryID, TicketSeverity, TicketType, null, AssignedTo, null);
            gvTickets.DataBind();
            for (int i = 0; i < gvTickets.Rows.Count; i++)
            {
                Label lblTicketID = (Label)gvTickets.Rows[i].FindControl("lblHeaderId");
                ImageButton ibMessage = (ImageButton)gvTickets.Rows[i].FindControl("ibMessage");

                int count = new BForum().GetMessageViewStatusCound(Convert.ToInt32(lblTicketID.Text), PSession.User.UserID);
                if (count == 0)
                {
                    ibMessage.ImageUrl = "~/Images/Message.jpg";
                }
                else
                {
                    ibMessage.ImageUrl = "~/Images/MessageNew.jpg";
                }
            }
            if (PSession.User.UserTypeID == (short)UserTypes.Manager || PSession.User.UserTypeID == (short)UserTypes.Admin)
                gvTickets.Columns[15].Visible = true;
            else if (PSession.User.UserTypeID == (short)UserTypes.Associate)
                gvTickets.Columns[15].Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillTickets(null);
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubCategory();
        }
        protected void lbTicketNo_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            string url = "TicketView.aspx?TicketNo=" + ((Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblHeaderId")).Text;
            Response.Redirect(url);
        }
        protected void gvTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillTickets(null);
            gvTickets.PageIndex = e.NewPageIndex;
            gvTickets.DataBind();
        }

        protected void btnResolve_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label ItemId = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblItemID");
            FillTickets(Convert.ToInt32(ItemId.Text));
            pnSearch.Visible = false;
            pnResolve.Visible = true;
            gvTickets.Columns[15].Visible = false;
            gvTickets.Columns[16].Visible = false;
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

            gvNewFileAttached.DataSource = AttchedFile;
            gvNewFileAttached.DataBind();
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            string fileName = (sender as LinkButton).Text;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.WriteFile(filePath);
            Response.End();
        }
        void fillAttachedFiles()
        {
            Dictionary<string, int> AttachedFiles = new BTickets().getAttachedFiles(1);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!validatetion())
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                return;
            }


            //        int ItemId, decimal Effort, int? ResolutionType, string Resolution, string SupportType, long UserId, Boolean NewTR, PTR TR, List< string > AttchedFile

            PTaskItem_Insert TaskItem = new PTaskItem_Insert();
            TaskItem.ItemID = Convert.ToInt32(((Label)gvTickets.Rows[0].FindControl("lblItemID")).Text);
            TaskItem.HeaderID = Convert.ToInt32(((Label)gvTickets.Rows[0].FindControl("lblHeaderId")).Text);
            TaskItem.SubCategoryID = Convert.ToInt32(((Label)gvTickets.Rows[0].FindControl("lblSubCategoryID")).Text);




            TaskItem.Effort = Convert.ToDecimal("0" + txtEffort.Text);
            TaskItem.ResolutionType = Convert.ToInt32(ddlResolutionType.SelectedValue) == 0 ? (int?)null : Convert.ToInt32(ddlResolutionType.SelectedValue);
            TaskItem.Resolution = txtResolution.Text;
            TaskItem.SupportType = ddlSupportType.SelectedValue;
           
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
            string messageBody = "";

            PTicketHeader Tickets = new BTickets().GetTicketDetails(TaskItem.HeaderID, TaskItem.ItemID, null, null, null, null, null, null, null, null)[0];
            messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/TicketResolved.htm");
            messageBody = messageBody.Replace("@@TicketNo", TaskItem.HeaderID.ToString());
            messageBody = messageBody.Replace("@@RequestedOn", Tickets.CreatedOn.ToString());
            messageBody = messageBody.Replace("@@Category", Tickets.Category.Category);
            messageBody = messageBody.Replace("@@Resolution", TaskItem.Resolution);
            messageBody = messageBody.Replace("@@fromName", PSession.User.ContactName);

            PUser user = new BUser().GetUserDetails(Tickets.CreatedBy.UserID);
            new EmailManager().MailSend(user.Mail, "Ticketing System", messageBody, TaskItem.HeaderID);
            ClearField();
            btnSave.Visible = false;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnSearch.Visible = true;
            pnResolve.Visible = false;
            FillTickets(null);
            gvTickets.Columns[15].Visible = true;
            gvTickets.Columns[16].Visible = true;
        }

        Boolean validatetion()
        {
            decimal parsedValue;
            if (!decimal.TryParse(txtEffort.Text, out parsedValue))
            {
                lblMessage.Text = "Please Enter number in Effort !";
                txtEffort.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtResolution.Text.Trim()))
            {
                lblMessage.Text = "Please enter the resolution";
                txtResolution.Focus();
                return false;
            }
            return true;
        }
        void ClearField()
        {
            txtEffort.Text = string.Empty;
            txtResolution.Text = string.Empty; 
            ddlResolutionType.SelectedValue = "0";
            gvNewFileAttached.DataSource = null;
            gvNewFileAttached.DataBind();
        }
        protected void ibMessage_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            string url = "SupportTicketView.aspx?TicketNo=" + ((Label)gvTickets.Rows[index].FindControl("lblHeaderId")).Text;
            Response.Redirect(url);
        }

        protected void btnReassign_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label TicketNo = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblHeaderId");
            Label ItemNo = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblItemID");
            Response.Redirect("ReassignSupportTicket.aspx?TicketNo=" + TicketNo.Text + "&ItemNo=" + ItemNo.Text);
        }
    }
}