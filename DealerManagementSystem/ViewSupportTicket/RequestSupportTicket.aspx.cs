using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class RequestSupportTicket : System.Web.UI.Page
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
            // if (Membership.GetUser() == null) 
            this.Page.MasterPageFile = "~/Dealer.master";
            //  else
            //    this.Page.MasterPageFile = "~/myMaster.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » New');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
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
                    if (!AttchedFile.Contains(fu.PostedFile.FileName))
                    {
                        string path = ConfigurationManager.AppSettings["BasePath"] + "/File/" + PSession.User.UserName + "/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        fu.SaveAs(path + fu.PostedFile.FileName.Split('\\')[fu.PostedFile.FileName.Split('\\').Count() - 1]);
                        AttchedFile.Add(fu.PostedFile.FileName.Split('\\')[fu.PostedFile.FileName.Split('\\').Count() - 1]);
                        gvFileAttached.DataSource = AttchedFile.Select(l => new { test = l });
                        gvFileAttached.DataBind();
                    }
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

            List<string> file = new List<string>();

            foreach (string f in AttchedFile)
            {
                file.Add(f);
            }

            int TicketTypeID = Convert.ToInt32(ddlTicketType.SelectedValue);
            int CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            int? SubCategoryID = Convert.ToInt32(ddlSubcategory.SelectedValue) == 0 ? (int?)null : Convert.ToInt32(ddlSubcategory.SelectedValue);
            Boolean Repeat = false;

            long TicketId;

            TicketId = new BTickets().insertTicketHeader(TicketTypeID, CategoryID, SubCategoryID, PSession.User, txtSubject.Text, txtTicketDescription.Text, txtMobileNo.Text, txtContactName.Text, Repeat, file, PSession.User.UserID, 1, null);

            if (TicketId == 0)
            {
                lblMessage.Text = "Ticket is not successfully generated.";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Ticket No " + TicketId + " is successfully generated.";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
                int L1SupportUser = new BTickets().GetL1SupportUserMapping(PSession.User.UserID, CategoryID);
                string Subject = "New Ticket " + TicketId.ToString();
                string messageBody = "";
                if (L1SupportUser == 0)
                {

                    //List<PCategory> pTicketCategory = new BTicketCategory().getTicketCategory(Convert.ToInt32(ddlCategory.SelectedValue), null, null);
                    //List<PEmployee> EmployeeRe = new BEmployees().GetEmployeeListJohn(null, pTicketCategory[0].EmpId, "", "", "");
                    //messageBody = messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/TicketCreate.htm");
                    //messageBody = messageBody.Replace("@@requested", PSession.User.ContactName);
                    //messageBody = messageBody.Replace("@@TicketNo ", TicketId.ToString());
                    //messageBody = messageBody.Replace("@@ToName", EmployeeRe[0].EmployeeName);
                    //messageBody = messageBody.Replace("@@TicketType", ddlTicketType.SelectedItem.Text);
                    //messageBody = messageBody.Replace("@@Category", ddlCategory.SelectedItem.Text);
                    //messageBody = messageBody.Replace("@@Description", txtTicketDescription.Text);
                    //messageBody = messageBody.Replace("@@fromName", PSession.User.ContactName);
                    //messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"] + "AssignTicket.aspx?TicketNo=" + TicketId.ToString());
                    //new EmailManager().MailSend(EmployeeRe[0].Mail1, Subject, messageBody, TicketId);
                }
                else
                {
                    //PUser AssignedUser = new BUser().GetUserDetails(L1SupportUser);
                    //List<PEmployee> pAssignedTo = null;
                    //pAssignedTo = new BEmployees().GetEmployeeListJohn(null, Convert.ToInt32(AssignedUser.ExternalReferenceID), "", "", "");
                    //messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/TicketAssign.htm");
                    //messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URLAF"] + "AssignedTickets.aspx?TicketNo=" + TicketId);
                    //messageBody = messageBody.Replace("@@TicketNo", TicketId.ToString());
                    //messageBody = messageBody.Replace("@@ToName", pAssignedTo[0].EmployeeName);
                    //messageBody = messageBody.Replace("@@RequestedOn", DateTime.Now.ToString());
                    //messageBody = messageBody.Replace("@@Category", ddlCategory.SelectedItem.Text);
                    //messageBody = messageBody.Replace("@@Subcategory", ddlSubcategory.SelectedItem.Text);
                    //messageBody = messageBody.Replace("@@Severity", "");
                    //messageBody = messageBody.Replace("@@TicketType", ddlTicketType.SelectedItem.Text);
                    //messageBody = messageBody.Replace("@@Description", "");
                    //messageBody = messageBody.Replace("@@Justification", "");
                    //messageBody = messageBody.Replace("@@ActualDuration", "2");
                    //messageBody = messageBody.Replace("@@fromName", "");
                    //new EmailManager().MailSend(pAssignedTo[0].Mail1, Subject, messageBody, TicketId);
                }
                ClearField();
            }
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
            AttchedFile.Remove(fileName);
            string path = ConfigurationManager.AppSettings["BasePath"] + "/File/" + PSession.User.UserName + "/";
            if (File.Exists(path + fileName))
            {
                File.Delete(path + fileName);
            }
            gvFileAttached.DataSource = AttchedFile.Select(l => new { test = l });
            gvFileAttached.DataBind();
        }

        void FillTickets()
        {

            gvTickets.DataSource = new BTickets().GetTicketDetails(null, null, null, null, null, null, null, null, PSession.User.UserID, "Open,Assigned");
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