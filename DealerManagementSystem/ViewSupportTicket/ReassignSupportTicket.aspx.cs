using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class ReassignSupportTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAssignTo();
                if (!string.IsNullOrEmpty(Request.QueryString["TicketNo"]))
                {
                    int TicketNo = Convert.ToInt32(Request.QueryString["TicketNo"]);
                    ViewState["TicketNo"] = TicketNo;
                    int ItemNo = Convert.ToInt32(Request.QueryString["ItemNo"]);
                    ViewState["ItemNo"] = ItemNo;
                    int RowCount = 0;
                    List<PTicketHeader> Ticket = new BTickets().GetTicketDetails(TicketNo, ItemNo, null, null, null, null, null, null, null, null,null,null, 1, 10000, out RowCount);
                    txtActualDuration.Text = "0.00";

                    if (Ticket[0].TicketItems[0].ItemStatus.StatusID == (short)TicketStatus.Assigned)
                    {
                        txtActualDuration.Enabled = false;
                    }
                    else if (Ticket[0].TicketItems[0].ItemStatus.StatusID == (short)TicketStatus.InProgress)
                    {
                        txtActualDuration.Enabled = true;
                    }
                    FillTickets(TicketNo);
                }
            }
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

        void FillTickets(int TicketNO)
        {
            int RowCount = 0;
            List<PTicketHeader> Ticket = new BTickets().GetTicketDetails(TicketNO, null, null, null, null, null, null, null, null, null,null,null, 1, 10000, out RowCount);
            gvTickets.DataSource = Ticket;
            gvTickets.DataBind();

            gvTicketItem.DataSource = Ticket[0].TicketItems;
            gvTicketItem.DataBind();
        }

        protected void cbAllEmp_CheckedChanged(object sender, EventArgs e)
        {
            FillAssignTo();
        }

        protected void btnReassign_Click(object sender, EventArgs e)
        {
            if (!validatetion())
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                return;
            }

            decimal ActualDuration = Convert.ToDecimal("0" + txtActualDuration.Text);


            int status = new BTickets().UpdateReassignTicket((int)ViewState["TicketNo"], (int)ViewState["ItemNo"], txtAssignerRemark.Text.Trim(), Convert.ToInt32(ddlAssignedTo.SelectedValue), ActualDuration, PSession.User.UserID, ddlSupportType.SelectedValue);

            if (status == 0)
            {
                lblMessage.Text = "Ticket is not successfully updated.";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Ticket No " + (int)ViewState["TicketNo"] + " is successfully updated.";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;

                string messageBody = "";
                string Subject = "New Ticket " + (int)ViewState["TicketNo"];
                int RowCount = 0;
                PTicketHeader TH = new BTickets().GetTicketDetails((int)ViewState["TicketNo"], null, null, null, null, null, null, null, null, "",null,null, 1, 10000, out RowCount)[0];
                PUser userAssignedTo = new BUser().GetUserDetails(Convert.ToInt32(ddlAssignedTo.SelectedValue));

                messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/TicketAssign.htm");

                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URLDealer"] + "Login.aspx");

                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"] + "AssignedTickets.aspx?TicketNo=" + (int)ViewState["TicketNo"]);
                messageBody = messageBody.Replace("@@TicketNo", ViewState["TicketNo"].ToString());
                messageBody = messageBody.Replace("@@ToName", userAssignedTo.ContactName);
                messageBody = messageBody.Replace("@@RequestedOn", TH.CreatedOn.ToShortDateString());
                messageBody = messageBody.Replace("@@Category", TH.Category.Category);
                messageBody = messageBody.Replace("@@Subcategory", TH.SubCategory.SubCategory);
                messageBody = messageBody.Replace("@@Severity", TH.Severity.Severity);
                messageBody = messageBody.Replace("@@TicketType", TH.Type.Type);
                messageBody = messageBody.Replace("@@Description", TH.Description);
                messageBody = messageBody.Replace("@@Justification", txtAssignerRemark.Text);
                messageBody = messageBody.Replace("@@ActualDuration", Convert.ToString(ActualDuration));
                messageBody = messageBody.Replace("@@fromName", PSession.User.ContactName);
                new EmailManager().MailSend(userAssignedTo.Mail, Subject, messageBody, (int)ViewState["TicketNo"]);

                string url = "TicketView.aspx?TicketNo=" + (int)ViewState["TicketNo"];
                Response.Redirect(url);
            }
        }
        Boolean validatetion()
        {
            decimal parsedValue;
            if (ddlAssignedTo.SelectedValue == "0")
            {
                lblMessage.Text = "Please Select The Assigned To";
                return false;
            }
            if (!decimal.TryParse(txtActualDuration.Text, out parsedValue))
            {
                lblMessage.Text = "Please Enter number in Worked Duration !";
                txtActualDuration.Focus();
                return false;
            }
            return true;
        }
    }
}