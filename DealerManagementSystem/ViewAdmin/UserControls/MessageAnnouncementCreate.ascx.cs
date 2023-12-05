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
        private List<PMessageAnnouncement> DealerUserDetails
        {
            get
            {
                if (ViewState["DealerUserDetails"] == null)
                {
                    ViewState["DealerUserDetails"] = new List<PMessageAnnouncement>();
                }
                return (List<PMessageAnnouncement>)ViewState["DealerUserDetails"];
            }
            set
            {
                ViewState["DealerUserDetails"] = value;
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
        protected void gvEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        protected void gvEmp_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<PMessageAnnouncement> pMessageAnnouncement = ViewState["DealerUserDetailsSort"] as List<PMessageAnnouncement>;
            var MsgAnnouncement = pMessageAnnouncement;
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;
            if (Sortdir == "ASC")
            {
                MsgAnnouncement = Sort<PMessageAnnouncement>(DealerUserDetails, SortExp, SortDirection.Ascending);
            }
            else
            {
                MsgAnnouncement = Sort<PMessageAnnouncement>(DealerUserDetails, SortExp, SortDirection.Descending);
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
        public List<PMessageAnnouncement> Sort<TKey>(List<PMessageAnnouncement> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<PMessageAnnouncement>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<PMessageAnnouncement>();
            }
        }
        protected void ChkMail_CheckedChanged(object sender, EventArgs e)
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

        protected void ChkMailH_CheckedChanged(object sender, EventArgs e)
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
        protected void lbActions_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;

                if (string.IsNullOrEmpty(txtMessage.Text))
                {
                    lblMessage.Text = "Please Enter Message";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                long success = 0;
                long NotificationNumber = 0;
                NotificationNumber = new BMessageAnnouncement().GetMaxNoMessageAnnouncement();
                foreach (PMessageAnnouncement ss in DealerUserDetails)
                {
                    if (ss.AssignTo.Department.DealerDepartment != "Top Management")
                    {
                        if (ss.MailResponce == true)
                        {
                            ss.Message = txtMessage.Text;
                            ss.NotificationNumber = NotificationNumber;
                            success = new BMessageAnnouncement().InsertMessageAnnouncement(ss, Convert.ToInt64(PSession.User.UserID));

                            if (success == 0)
                            {
                                lblMessage.Text = "Mail Not Send...!";
                                lblMessage.ForeColor = Color.Red;
                                lblMessage.Visible = true;
                            }
                            else
                            {
                                string messageBody = new EmailManager().GetFileContent(ConfigurationManager.AppSettings["BasePath"] + "/MailFormat/MessageAnnouncement.htm"); ;
                                messageBody = messageBody.Replace("@@Message", txtMessage.Text);
                                messageBody = messageBody.Replace("\r", "&nbsp");                                
                                messageBody = messageBody.Replace("\n", "<br />");
                                messageBody = messageBody.Replace("@@Dealer", (ddlDealer.SelectedValue=="0")?"ALL": ddlDealer.SelectedItem.Text);
                                messageBody = messageBody.Replace("@@Department", (ddlDepartment.SelectedValue == "0") ? "ALL" : ddlDepartment.SelectedItem.Text);
                                messageBody = messageBody.Replace("@@Designation", (ddlDesignation.SelectedValue == "0") ? "ALL" : ddlDesignation.SelectedItem.Text);
                                messageBody = messageBody.Replace("@@Employee", (ddlDealerEmployee.SelectedValue == "0") ? "ALL" : ddlDealerEmployee.SelectedItem.Text);
                                messageBody = messageBody.Replace("@@NotificationNo", NotificationNumber.ToString());
                                messageBody = messageBody.Replace("@@NotificationDate", DateTime.Now.ToString());
                                messageBody = messageBody.Replace("@@fromName", "Team AJAXOne");
                                messageBody = messageBody.Replace("@@URL", ConfigurationManager.AppSettings["URL"]);
                                new EmailManager().MailSend(ss.AssignTo.Mail, "AJAXOne - Message [Notification No. "+ NotificationNumber + "  ]", messageBody, Convert.ToInt64(PSession.User.UserID));
                                lblMessage.Text = "Mail Send Successfully...!";
                                lblMessage.ForeColor = Color.Green;
                                lblMessage.Visible = true;
                            }
                        }
                    }
                }
                lbSendMessage.Visible = false;
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