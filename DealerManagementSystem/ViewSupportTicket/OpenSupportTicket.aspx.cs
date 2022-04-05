using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class OpenSupportTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Request.QueryString["SendForApproval"]))
                {
                    FillAllFields(Convert.ToInt32(Request.QueryString["SendForApproval"]));
                    pnList.Visible = false;
                    pnView.Visible = true;
                }
                else
                {
                    FillCategory();
                    //   FillStatus();
                    FillCreatedBy();
                    FillTickets();
                }
                FillApproval();

                //  txtRequestedDateFrom.Text = d.DataDespesa.ToString("yyyy-MM-dd");
            }
        }
        void FillCreatedBy()
        {
            ddlCreatedBy.DataTextField = "ContactName";
            ddlCreatedBy.DataValueField = "UserID";
            ddlCreatedBy.DataSource = new BUser().GetAllUsers();
            ddlCreatedBy.DataBind();
            ddlCreatedBy.Items.Insert(0, new ListItem("Select", "0"));
        }

        void FillCategory()
        {
            ddlCategory.DataTextField = "Category";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataSource = new BTicketCategory().getTicketCategory(null, null, null);
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        //void FillStatus()
        //{
        //    ddlStatus.DataTextField = "Status";
        //    ddlStatus.DataValueField = "StatusID";
        //    ddlStatus.DataSource = new BTicketStatus().getTicketStatus(null, null);
        //    ddlStatus.DataBind();
        //    ddlStatus.Items.Insert(0, new ListItem("Select", "0"));
        //    ddlStatus.SelectedValue = "1";
        //}

        void FillTickets()
        {
            try
            {
                int? HeaderId = null;
                int? TicketCategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);

                //  int? StatusId = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);

                long? CreatedBy = ddlCreatedBy.SelectedValue == "0" ? (long?)null : Convert.ToInt64(ddlCreatedBy.SelectedValue);
                DateTime? RequestedDateFrom = null;
                if (!string.IsNullOrEmpty(txtRequestedDateFrom.Text))
                {
                    RequestedDateFrom = Convert.ToDateTime(txtRequestedDateFrom.Text);
                }
                DateTime? RequestedDateTo = null;
                if (!string.IsNullOrEmpty(txtRequestedDateTo.Text))
                {
                    RequestedDateTo = Convert.ToDateTime(txtRequestedDateTo.Text).AddDays(1);
                }
                if (!string.IsNullOrEmpty(txtTicketId.Text.Trim()))
                {

                    HeaderId = Convert.ToInt32(txtTicketId.Text);
                }

                gvTickets.DataSource = new BTickets().GetOpenTickets(HeaderId, TicketCategoryID, null, CreatedBy, RequestedDateFrom, RequestedDateTo, PSession.User.UserID);
                gvTickets.DataBind();


                //else if (PSession.User.L1Support == true)
                //{
                //    List<PTicketHeader> Header = new BTickets().GetTicketDetails(HeaderId, null, TicketCategoryID, null, null, null, null, PSession.User.UserID, null, "2,3,4");

                //    gvTickets.DataSource = Header;
                //}

                FillMessageStatus();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillTickets();
        }

        protected void lbTicketNo_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            if (rbAssign.Checked)
            {
                string url = "AssignSupportTicket.aspx?TicketNo=" + ((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text;
                Response.Redirect(url);
            }
            else if (rbSendForApproval.Checked)
            {
                FillAllFields(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text));
                pnList.Visible = false;
                pnView.Visible = true;
            }
            //else if (rbResolve.Checked)
            //{
            //    string url = "ResolveSupportTicket.aspx?TicketNo=" + ((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text;
            //    Response.Redirect(url);
            //}
            else if (rbReject.Checked)
            {
                pnList.Visible = false;
                pnlReject.Visible = true;
                int HeaderId = Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text);
                PTicketHeader TH = new BTickets().GetTicketDetails(HeaderId, null, null, null, null, null, null, null, null, null)[0];
                txtTicketNoReject.Text = TH.HeaderID.ToString();
                txtTicketNoRejectRemark.Text = "";
            }
        }

        protected void gvTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillTickets();
            gvTickets.PageIndex = e.NewPageIndex;
            gvTickets.DataBind();
            FillMessageStatus();
        }
        void FillAllFields(int HeaderId)
        {
            //if (string.IsNullOrEmpty(TicketNo))
            //{
            //    TicketNo = txtTicketNo.Text.Trim();
            //}
            // PTickets Tickets = new PTickets();
            // Tickets = new BTickets().GeTicketsByTicketNo(TicketNo);

            //    PTicketHeader TH = new BTickets().GetOpenTickets(HeaderId, null, null, null, null, null, PSession.User.UserID)[0];
            PTicketHeader TH = new BTickets().GetTicketDetails(HeaderId, null, null, null, null, null, null, null, null, null)[0];
            txtTicketNo.Text = TH.HeaderID.ToString();
            txtCategory.Text = TH.Category.Category;
            txtRequestedBy.Text = TH.CreatedBy.ContactName + " " + Convert.ToString(TH.CreatedOn);
            txtTicketType.Text = TH.Type.Type;
            txtTicketDescription.Text = Convert.ToString(TH.Description);
            // txtJustification.Text = Convert.ToString(Tickets.Justification);


            txtStatus.Text = Convert.ToString(TH.Status.StatusID);

            Dictionary<string, int> AttachedFiles = new BTickets().getAttachedFiles(HeaderId);
            List<ListItem> files = new List<ListItem>();
            foreach (KeyValuePair<string, Int32> Files in AttachedFiles)
            {
                files.Add(new ListItem(Files.Key, ConfigurationManager.AppSettings["BasePath"] + "/AttachedFile/" + TH.CreatedOn.Month.ToString("00") + "" + TH.CreatedOn.Year.ToString() + "/" + Files.Value + "." + Files.Key.Split('.')[Files.Key.Split('.').Count() - 1]));// Files.Key.Split('.')[1])));
            }

        }

        protected void btnSendForApproval_Click(object sender, EventArgs e)
        {
            //PEmployee AssignedTo = new BEmployees().GetEmployeeListJohn(Convert.ToInt32(ddlapprovar.SelectedValue), null, "", "", "")[0];
            //PUser AssignedTo_User = new BUser().GetUserDetails(AssignedTo.EmployeeUserID);
            //if (string.IsNullOrEmpty(AssignedTo_User.UserName))
            //{
            //    PUser user = new PUser();
            //    user.ContactName = AssignedTo.EmployeeName;
            //    user.ExternalReferenceID = AssignedTo.EmpId.ToString();
            //    user.UserName = AssignedTo.EmployeeUserID;
            //    user.UserTypeID = 1;
            //    user.CreatedBy = AssignedTo.EmpId;
            //    user.CreatedOn = DateTime.Now;
            //    user.UpdatedBy = AssignedTo.EmpId;
            //    user.UpdatedOn = DateTime.Now;
            //    user.UserID = 0;
            //    user.IsLocked = false;
            //    user.IsEnabled = true;
            //    user.PassWord = "";
            //    user.IsFirstTimeLogin = true;
            //    user.Mail = AssignedTo.Mail1;
            //    user.Phone = "";
            //    new BUser().UpdateUser(user);
            //    AssignedTo_User = new BUser().GetUserDetails(AssignedTo.EmployeeUserID);
            //}

            int success = new BTickets().insertTicketApprovalDetails(PSession.User.UserID, Convert.ToInt32(txtTicketNo.Text), Convert.ToInt32(ddlapprovar.SelectedValue));
            //int success = new BTickets().insertTicketApprovalDetails(PSession.User.UserID, Convert.ToInt32(txtTicketNo.Text), AssignedTo_User.UserID);
            if (success == 0)
            {
                lblMessage.Text = "Ticket No " + txtTicketNo.Text + "  is not successfully updated.";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Ticket No " + txtTicketNo.Text + " is successfully updated.";
                btnSendForApproval.Visible = false;
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
                PUser UserApproval = new BUser().GetUsers(Convert.ToInt32(ddlapprovar.SelectedValue), "", null, "", null,null)[0];
                //  PEmployee EmployeeApproval = new BEmployees().GetEmployeeListJohn(Convert.ToInt32(ddlapprovar.SelectedValue), null, "", "", "")[0];

                string messageBody = messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/TicketRequestForApproval.htm");

                messageBody = messageBody.Replace("@@RequestedBy", txtRequestedBy.Text);
                messageBody = messageBody.Replace("@@TicketNo", txtTicketNo.Text);
                messageBody = messageBody.Replace("@@ToName", UserApproval.ContactName);
                messageBody = messageBody.Replace("@@TicketType", txtTicketType.Text);
                messageBody = messageBody.Replace("@@Category", txtCategory.Text);
                messageBody = messageBody.Replace("@@Description", txtTicketDescription.Text);
                messageBody = messageBody.Replace("@@fromName", PSession.User.ContactName);

                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"] + "TicketApproval.aspx?TicketNo=" + txtTicketNo.Text);
                new EmailManager().MailSend(UserApproval.Mail, "New Ticket " + txtTicketNo.Text, messageBody, Convert.ToInt32(txtTicketNo.Text));
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnList.Visible = true;
            pnView.Visible = false;
            FillTickets();
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            string fileName = (sender as LinkButton).Text;
            Response.ContentType = ContentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.WriteFile(filePath);
            Response.End();
        }
        void FillApproval()
        {
            //List<PEmployee> eList = null;
            //if (Session["eList"] == null)
            //{
            //    eList = new BEmployees().GetEmployeeListJohn(null, null, "", "", "");
            //    Session["eList"] = eList;
            //}
            //else
            //{
            //    eList = (List<PEmployee>)Session["eList"];
            //}
            //ddlapprovar.DataTextField = "EmployeeUserID";
            //ddlapprovar.DataValueField = "EID";
            //ddlapprovar.DataSource = eList;
            //ddlapprovar.DataBind();
            //ddlapprovar.Items.Insert(0, new ListItem("Select", "0"));

            List<PUser> user = new BUser().GetAllUsers();
            user.Where(M => M.SystemCategoryID == (short)SystemCategory.AF || M.SystemCategoryID == (short)SystemCategory.Support).ToList();
            //   List<PUser> UserList = new BUser().GetUsers(null, "", 0, "");

            ddlapprovar.DataTextField = "ContactName";
            ddlapprovar.DataValueField = "UserID";
            ddlapprovar.DataSource = user.Where(M => M.SystemCategoryID == (short)SystemCategory.AF).ToList();
            ddlapprovar.DataBind();
            ddlapprovar.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void ibMessage_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            string url = "SupportTicketView.aspx?TicketNo=" + ((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text;
            Response.Redirect(url);
        }

        void FillMessageStatus()
        {
            for (int i = 0; i < gvTickets.Rows.Count; i++)
            {
                Label lblTicketID = (Label)gvTickets.Rows[i].FindControl("lblTicketID");
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
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            int success = new BTickets().UpdateTicketReject(PSession.User.UserID, Convert.ToInt32(txtTicketNoReject.Text), txtTicketNoRejectRemark.Text.Trim());
            if (success == 0)
            {
                lblMessage.Text = "Ticket No " + txtTicketNo.Text + "  is not successfully updated.";
                lblMessage.ForeColor = Color.Red;
            }
            else
            {
                btnReject.Visible = false;
                lblMessage.Text = "Ticket No " + txtTicketNo.Text + "  is successfully updated.";
                lblMessage.ForeColor = Color.Red;
            }

            //else
            //{
            //    lblMessage.Text = "Ticket No " + txtTicketNo.Text + " is successfully updated.";
            //    btnSendForApproval.Visible = false;
            //    lblMessage.ForeColor = Color.Green;
            //    lblMessage.Visible = true;
            //    PUser UserApproval = new BUser().GetUsers(Convert.ToInt32(ddlapprovar.SelectedValue), "", null, "")[0];

            //    //  PEmployee EmployeeApproval = new BEmployees().GetEmployeeListJohn(Convert.ToInt32(ddlapprovar.SelectedValue), null, "", "", "")[0];

            //    string messageBody = messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/TicketRequestForApproval.htm");
            //    messageBody = messageBody.Replace("@@RequestedBy", txtRequestedBy.Text);
            //    messageBody = messageBody.Replace("@@TicketNo", txtTicketNo.Text);
            //    messageBody = messageBody.Replace("@@ToName", UserApproval.ContactName);
            //    messageBody = messageBody.Replace("@@TicketType", txtTicketType.Text);
            //    messageBody = messageBody.Replace("@@Category", txtCategory.Text);
            //    messageBody = messageBody.Replace("@@Description", txtTicketDescription.Text);
            //    messageBody = messageBody.Replace("@@fromName", PSession.User.ContactName);

            //    messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"] + "TicketApproval.aspx?TicketNo=" + txtTicketNo.Text);
            //    new EmailManager().MailSend(UserApproval.Mail, "New Ticket " + txtTicketNo.Text, messageBody, Convert.ToInt32(txtTicketNo.Text));
            //}
        }

        protected void btnRejectBack_Click(object sender, EventArgs e)
        {
            pnList.Visible = true;
            pnlReject.Visible = false;
            FillTickets();
        }

    }
}