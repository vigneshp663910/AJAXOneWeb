using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin.UserControls
{
    public partial class MessageAnnouncementCreate : System.Web.UI.UserControl
    {
        private List<PMessageAnnouncementItem> DealerUserDetails
        {
            get
            {
                if (ViewState["DealerUserDetails"] == null)
                {
                    ViewState["DealerUserDetails"] = new List<PMessageAnnouncementItem>();
                }
                return (List<PMessageAnnouncementItem>)ViewState["DealerUserDetails"];
            }
            set
            {
                ViewState["DealerUserDetails"] = value;
            }
        }
        public PMessageAnnouncementHeader MessageByID
        {
            get
            {
                if (ViewState["MessageByID"] == null)
                {
                    ViewState["MessageByID"] = new PMessageAnnouncementHeader();
                }
                return (PMessageAnnouncementHeader)ViewState["MessageByID"];
            }
            set
            {
                ViewState["MessageByID"] = value;
            }
        }
        private int UnChecked
        {
            get
            {
                if (ViewState["UnChecked"] == null)
                {
                    ViewState["UnChecked"] = 0;
                }
                return (int)ViewState["UnChecked"];
            }
            set
            {
                ViewState["UnChecked"] = value;
            }
        }
        public int? DealerID, DealerDepartmentID, DealerDesignationID, DealerUserID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, DealerDepartmentID, null, null);
                List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
                new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
            }
        }
        public void FillMaster()
        {
            new DDLBind().FillDealerAndEngneer(ddlDealer, null);
            DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
            DealerDepartmentID = (ddlDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, DealerDepartmentID, null, null);
            DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
            new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
            DealerUserID = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            FillGrid();
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
            DealerDepartmentID = (ddlDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, DealerDepartmentID, null, null);
            DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
            new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
            DealerUserID = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            FillGrid();
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerDepartmentID = (ddlDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, DealerDepartmentID, null, null);
            DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
            new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
            DealerUserID = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            FillGrid();
        }
        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
            new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
            DealerUserID = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            FillGrid();
        }
        protected void ddlDealerEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerUserID = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            FillGrid();
        }
        public void FillGrid()
        {
            try
            {
                DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                DealerDepartmentID = (ddlDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
                DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
                DealerUserID = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
                DealerUserDetails = new BMessageAnnouncement().GetUsersForMessageAnnouncement(DealerUserID, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
                ViewState["DealerUserDetailsSort"] = DealerUserDetails;
                gvEmp.DataSource = DealerUserDetails;
                gvEmp.DataBind();

                CheckBox ChkMailH = (CheckBox)gvEmp.HeaderRow.FindControl("ChkMailH");
                foreach (var ss in DealerUserDetails)
                {
                    ChkMailH.Checked = true;
                    if (ss.MailResponce == false)
                    {
                        ChkMailH.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                return;
            }
        }
        protected void gvEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                gvEmp.DataSource = DealerUserDetails;
                gvEmp.PageIndex = e.NewPageIndex;
                gvEmp.DataBind();
                CheckBox ChkMailHH = (CheckBox)gvEmp.HeaderRow.FindControl("ChkMailH");

                if (UnChecked == DealerUserDetails.Count)
                { ChkMailHH.Checked = true; }
                else { ChkMailHH.Checked = false; }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                return;
            }
        }
        protected void gvEmp_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                List<PMessageAnnouncementItem> pMessageAnnouncement = ViewState["DealerUserDetailsSort"] as List<PMessageAnnouncementItem>;
                var MsgAnnouncement = pMessageAnnouncement;
                string Sortdir = GetSortDirection(e.SortExpression);
                string SortExp = e.SortExpression;
                if (Sortdir == "ASC")
                {
                    MsgAnnouncement = Sort<PMessageAnnouncementItem>(DealerUserDetails, SortExp, SortDirection.Ascending);
                }
                else
                {
                    MsgAnnouncement = Sort<PMessageAnnouncementItem>(DealerUserDetails, SortExp, SortDirection.Descending);
                }
                gvEmp.DataSource = MsgAnnouncement;
                gvEmp.DataBind();
                CheckBox ChkMailH = (CheckBox)gvEmp.HeaderRow.FindControl("ChkMailH");
                foreach (var ss in pMessageAnnouncement)
                {
                    ChkMailH.Checked = true;
                    if (ss.MailResponce == false)
                    {
                        ChkMailH.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                return;
            }
        }
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<PMessageAnnouncementItem> Sort<TKey>(List<PMessageAnnouncementItem> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<PMessageAnnouncementItem>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<PMessageAnnouncementItem>();
            }
        }
        protected void ChkMail_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                CheckBox ChkMailE = (CheckBox)sender;
                GridViewRow rowindex = (GridViewRow)ChkMailE.NamingContainer;
                Label lblDealerEmployeeID = (Label)rowindex.FindControl("lblDealerEmployeeID");
                int UserID = Convert.ToInt32(lblDealerEmployeeID.Text);
                CheckBox ChkMailH = (CheckBox)gvEmp.HeaderRow.FindControl("ChkMailH");

                foreach (var ss in DealerUserDetails)
                {
                    if (ss.AssignTo.UserID == UserID)
                    {
                        ss.MailResponce = ChkMailE.Checked;
                        if (ChkMailE.Checked)
                        {
                            lbSendMessage.Visible = true;
                            UnChecked = UnChecked + 1;
                            break;
                        }
                        else
                        {
                            UnChecked = UnChecked - 1;
                            break;
                        }
                    }
                }
                if (UnChecked == DealerUserDetails.Count)
                { ChkMailH.Checked = true; }
                else { ChkMailH.Checked = false; }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                return;
            }
        }
        protected void ChkMailH_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                CheckBox ChkMailH = (CheckBox)gvEmp.HeaderRow.FindControl("ChkMailH");
                if (ChkMailH.Checked == true)
                {
                    lbSendMessage.Visible = true;

                    foreach (var ss in DealerUserDetails)
                    {
                        ss.MailResponce = true;
                    }
                    gvEmp.DataSource = DealerUserDetails;
                    gvEmp.DataBind();
                    CheckBox ChkMailH_T = (CheckBox)gvEmp.HeaderRow.FindControl("ChkMailH");
                    ChkMailH_T.Checked = true;
                    UnChecked = DealerUserDetails.Count;
                }
                else
                {
                    lbSendMessage.Visible = false;
                    foreach (var ss in DealerUserDetails)
                    {
                        ss.MailResponce = false;
                    }
                    gvEmp.DataSource = DealerUserDetails;
                    gvEmp.DataBind();
                    CheckBox ChkMailH_T = (CheckBox)gvEmp.HeaderRow.FindControl("ChkMailH");
                    ChkMailH_T.Checked = false;
                    UnChecked = 0;
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                return;
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;

                LinkButton lbActions = (LinkButton)sender;
                if (lbActions.Text == "Send Message")
                {
                    SendMessage();
                    lbSendMessage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                return;
            }
        }
        public void ValidationMessage()
        {
            if (string.IsNullOrEmpty(FreeTextMessage.Text))
            {
                lblMessage.Text = "Please Enter Message...!";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(txtValidFrom.Text))
            {
                lblMessage.Text = "Please select Valid From...!";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(txtValidTo.Text))
            {
                lblMessage.Text = "Please select Valid To...!";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(txtSubject.Text))
            {
                lblMessage.Text = "Please Enter Subject...!";
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }
        public void SendMessage()
        {
            ValidationMessage();
            PMessageAnnouncementHeader Msg = new PMessageAnnouncementHeader();
            if(MessageByID.MessageAnnouncementHeaderID != null)
            {
                Msg.MessageAnnouncementHeaderID = MessageByID.MessageAnnouncementHeaderID;
            }
            Msg.Message = FreeTextMessage.Text;
            Msg.ValidFrom = Convert.ToDateTime(txtValidFrom.Text);
            Msg.ValidTo = Convert.ToDateTime(txtValidTo.Text);
            Msg.Subject = Convert.ToString(txtSubject.Text);
            Msg.Status = "Sent";
            Msg.Item = DealerUserDetails;
            string result = new BAPI().ApiPut("MessageNotification", Msg);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);
            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                return;
            }
            else
            {
                foreach (PMessageAnnouncementItem ss in DealerUserDetails)
                {
                    if (ss.AssignTo.Department.DealerDepartment != "Top Management")
                    {
                        if (ss.MailResponce == true)
                        {
                            if (!string.IsNullOrEmpty(ss.AssignTo.Mail))
                            {
                                string messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/MessageAnnouncement.htm"); ;
                                messageBody = messageBody.Replace("@@Message", FreeTextMessage.Text);
                                messageBody = messageBody.Replace("\r", "&nbsp");
                                messageBody = messageBody.Replace("\n", "<br />");
                                messageBody = messageBody.Replace("@@Dealer", (ddlDealer.SelectedValue == "0") ? "ALL" : ddlDealer.SelectedItem.Text);
                                messageBody = messageBody.Replace("@@Department", (ddlDepartment.SelectedValue == "0") ? "ALL" : ddlDepartment.SelectedItem.Text);
                                messageBody = messageBody.Replace("@@Designation", (ddlDesignation.SelectedValue == "0") ? "ALL" : ddlDesignation.SelectedItem.Text);
                                messageBody = messageBody.Replace("@@Employee", (ddlDealerEmployee.SelectedValue == "0") ? "ALL" : ddlDealerEmployee.SelectedItem.Text);
                                messageBody = messageBody.Replace("@@NotificationNo", Result.Data.ToString());
                                messageBody = messageBody.Replace("@@NotificationDate", DateTime.Now.ToString());
                                messageBody = messageBody.Replace("@@Subject", txtSubject.Text);
                                messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
                                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);
                                new EmailManager().MailSend(ss.AssignTo.Mail, "AJAXOne - Message [Notification No. " + Result.Data + "]", messageBody, Convert.ToInt64(PSession.User.UserID));
                            }
                        }
                    }
                }
                lblMessage.Text = "Message Sent Successfully...";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
            }
        }
        public void FillMasterEdit(long? MessageAnnouncementHeaderID,string IsDraft)
        {
            PApiResult Result = new PApiResult();
            Result = new BMessageAnnouncement().GetMessageAnnouncementHeaderByID(Convert.ToInt64(MessageAnnouncementHeaderID), null, null);
            MessageByID = JsonConvert.DeserializeObject<PMessageAnnouncementHeader>(JsonConvert.SerializeObject(Result.Data));
            FreeTextMessage.Text = MessageByID.Message; 
            txtValidFrom.Text = MessageByID.ValidFrom.ToString("yyyy-MM-dd"); 
            txtValidTo.Text = MessageByID.ValidTo.ToString("yyyy-MM-dd"); 
            txtSubject.Text = MessageByID.Subject; 
            btnSaveAsDraft.Visible = false;
            if(IsDraft!="Draft")
            {
                btnSaveAsDraft.Visible = true;
            }
        }
        protected void btnSaveAsDraft_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;

                PMessageAnnouncementHeader Msg = new PMessageAnnouncementHeader();
                Msg.Message = FreeTextMessage.Text;
                Msg.ValidFrom = Convert.ToDateTime(txtValidFrom.Text);
                Msg.ValidTo = Convert.ToDateTime(txtValidTo.Text);
                Msg.Subject = Convert.ToString(txtSubject.Text);
                Msg.Item = new List<PMessageAnnouncementItem>();
                Msg.Status = "Draft";
                string result = new BAPI().ApiPut("MessageNotification", Msg);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);
                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.Text = Result.Message;
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                    return;
                }
                else
                {
                    lblMessage.Text = "Message Sent to Draft Successfully...";
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                return;
            }
        }
    }
}