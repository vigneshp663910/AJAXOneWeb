using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class ManageSupportTicket : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSupportTicket_ManageSupportTicket; } }
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Report');</script>");
            if (!IsPostBack)
            {
                try
                {
                    PageCount = 0;
                    PageIndex = 1;
                    new FillDropDownt().Category(ddlCategory, null, null);
                    ddlCategory_SelectedIndexChanged(null, null);
                    FillTicketSeverity();
                    new FillDropDownt().Type(ddlTicketType, null, null);
                    FillStatus();
                    new DDLBind().FillDealerAndEngneer(ddlDealer, ddlCreatedBy);
                    new DDLBind().FillDealerAndEngneer(ddlDealer, ddlAssignedTo);
                    new DDLBind().FillDealerAndEngneer(ddlDealer, ddlApprovalTo);
                    string Status = string.Empty;
                    if (Session["DashboardTaskStatus"] != null && Session["DashboardTaskStatus"] != "Created")
                    {
                        foreach (ListItem li in lbStatus.Items)
                        {
                            if (Session["DashboardTaskStatus"].ToString().Contains(li.Text))
                            {
                                li.Selected = true;
                                Status = li.Text;
                            }
                        }
                    }
                    if (Session["DashboardTaskUserID"] != null && Session["DashboardTaskDealerID"] != null)
                    {
                        ddlDealer.SelectedValue = Session["DashboardTaskDealerID"].ToString();
                        ddlDealer_SelectedIndexChanged(null, null);
                        if (Status == "In Progress" || Status == "Assigned" || Status == "Resolved" || Status == "Closed" || Status == "Cancel" || Status == "Foreclose")
                        {
                            ddlAssignedTo.SelectedValue = Session["DashboardTaskUserID"].ToString();
                        }
                        if (Status == "Waiting for Approval" || Status == "Approved" || Status == "Reject")
                        {
                            ddlApprovalTo.SelectedValue = Session["DashboardTaskUserID"].ToString();
                        }
                        if (Status == "Open")
                        {
                            ddlCreatedBy.SelectedValue = Session["DashboardTaskUserID"].ToString();
                        }
                        if (Status == "")
                        {
                            ddlCreatedBy.SelectedValue = Session["DashboardTaskUserID"].ToString();
                        }
                    }

                    PageIndex = 1;
                    FillTickets();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.ToString();
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
            }
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlAssignedTo, DealerUser, "ContactName", "UserID");
            new DDLBind(ddlApprovalTo, DealerUser, "ContactName", "UserID");
            new DDLBind(ddlCreatedBy, DealerUser, "ContactName", "UserID");
        }
        void FillStatus()
        {
            lbStatus.DataTextField = "Status";
            lbStatus.DataValueField = "StatusID";
            lbStatus.DataSource = JsonConvert.DeserializeObject<List<PStatus>>(JsonConvert.SerializeObject(new BTickets().getTicketStatus(null, null).Data));
            lbStatus.DataBind();
            lbStatus.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillTicketSeverity()
        {
            ddlSeverity.DataTextField = "Severity";
            ddlSeverity.DataValueField = "SeverityID";
            ddlSeverity.DataSource = JsonConvert.DeserializeObject<List<PSeverity>>(JsonConvert.SerializeObject(new BTickets().getTicketSeverity(null, null).Data));
            ddlSeverity.DataBind();
            ddlSeverity.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillTickets()
        {
            try
            {
                int? TicketNO = string.IsNullOrEmpty(txtTicketNo.Text.Trim()) ? (int?)null : Convert.ToInt32(txtTicketNo.Text.Trim());
                int? CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
                int? SubCategoryID = ddlSubcategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSubcategory.SelectedValue);
                int? SeverityID = ddlSeverity.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSeverity.SelectedValue);
                int? TypeId = ddlTicketType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTicketType.SelectedValue);
                string TicketStatus = "";
                foreach (ListItem li in lbStatus.Items)
                {
                    if (li.Selected)
                    {
                        TicketStatus = TicketStatus + "," + li.Text;
                    }
                }
                TicketStatus = TicketStatus.Replace(",Select", "");
                DateTime? TicketFrom = null;
                if (!string.IsNullOrEmpty(txtTicketFrom.Text))
                {
                    TicketFrom = Convert.ToDateTime(txtTicketFrom.Text);
                }
                DateTime? TicketTo = null;
                if (!string.IsNullOrEmpty(txtTicketTo.Text))
                {
                    TicketTo = Convert.ToDateTime(txtTicketTo.Text).AddDays(1);
                }
                List<PTicketHeader> TicketHeader = new List<PTicketHeader>();
                int? UserID = null;
                if (Session["DashboardTaskUserID"] == null)
                {
                    UserID = PSession.User.UserID;
                }
                int? CreatedBy = null, AssignedTo = null, ApprovalTo = null, DealerID = null;
                CreatedBy = (ddlCreatedBy.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlCreatedBy.SelectedValue);
                AssignedTo = (ddlAssignedTo.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlAssignedTo.SelectedValue);
                ApprovalTo = (ddlApprovalTo.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlApprovalTo.SelectedValue);
                DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);

                PApiResult Result = new BTickets().GetTicketDetailsSupport(DealerID, TicketNO, CategoryID, SubCategoryID, SeverityID, TypeId, CreatedBy, AssignedTo, ApprovalTo, UserID, TicketStatus, txtTicketFrom.Text, txtTicketTo.Text, PageIndex, gvTickets.PageSize);
                TicketHeader = JsonConvert.DeserializeObject<List<PTicketHeader>>(JsonConvert.SerializeObject(Result.Data));

                if (Result.RowCount == 0)
                {
                    gvTickets.DataSource = null;
                    gvTickets.DataBind();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    gvTickets.DataSource = TicketHeader;
                    gvTickets.DataBind();
                    PageCount = (Result.RowCount + gvTickets.PageSize - 1) / gvTickets.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvTickets.PageSize) + 1) + " - " + (((PageIndex - 1) * gvTickets.PageSize) + gvTickets.Rows.Count) + " of " + Result.RowCount;
                }

                for (int i = 0; i < gvTickets.Rows.Count; i++)
                {
                    Label lblTicketID = (Label)gvTickets.Rows[i].FindControl("lblTicketID");
                    ImageButton ibMessage = (ImageButton)gvTickets.Rows[i].FindControl("ibMessage");
                    int count = new BForum().GetMessageViewStatusCound(Convert.ToInt64(lblTicketID.Text), (UserID == null) ? Convert.ToInt32(Session["DashboardTaskUserID"]) : UserID);
                    if (count == 0)
                    {
                        ibMessage.ImageUrl = "~/Images/Message.jpg";
                    }
                    else
                    {
                        ibMessage.ImageUrl = "~/Images/MessageNew.jpg";
                    }
                }
                Session["DashboardTaskUserID"] = null;
                Session["DashboardTaskDealerID"] = null;
                Session["DashboardTaskStatus"] = null;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            new FillDropDownt().SubCategory(ddlSubcategory, null, null, CategoryID);
        }
        protected void gvTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTickets.PageIndex = e.NewPageIndex;
            FillTickets();
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            int? TicketNO = string.IsNullOrEmpty(txtTicketNo.Text.Trim()) ? (int?)null : Convert.ToInt32(txtTicketNo.Text.Trim());
            int? CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            int? SubCategoryID = ddlSubcategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSubcategory.SelectedValue);
            int? SeverityID = ddlSeverity.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSeverity.SelectedValue);
            int? TypeId = ddlTicketType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTicketType.SelectedValue);
            string TicketStatus = "";
            foreach (ListItem li in lbStatus.Items)
            {
                if (li.Selected)
                {
                    TicketStatus = TicketStatus + "," + li.Text;
                }
            }
            TicketStatus = TicketStatus.Replace(",Select", "");
            int? UserID = null;
            if (Session["DashboardTaskUserID"] == null)
            {
                UserID = PSession.User.UserID;
            }
            int? CreatedBy = null, AssignedTo = null, ApprovalTo = null, DealerID = null;
            CreatedBy = (ddlCreatedBy.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlCreatedBy.SelectedValue);
            AssignedTo = (ddlAssignedTo.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlAssignedTo.SelectedValue);
            ApprovalTo = (ddlApprovalTo.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlApprovalTo.SelectedValue);
            DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            PApiResult Result = new BTickets().GetTicketDetailsByExcel(DealerID, TicketNO, CategoryID, SubCategoryID, SeverityID, TypeId, CreatedBy, AssignedTo, ApprovalTo, UserID, TicketStatus, txtTicketFrom.Text, txtTicketTo.Text);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            new BXcel().ExporttoExcel(dt, "TicketReport");
        }
        protected void ibMessage_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            divSupportTicketView.Visible = true;
            btnBackToList.Visible = true;
            divList.Visible = false;
            UC_SupportTicketView.FillTickets(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text));
            UC_SupportTicketView.FillChat(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text));
            UC_SupportTicketView.FillChatTemp(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text));
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divSupportTicketView.Visible = false;
            btnBackToList.Visible = false;
            divList.Visible = true;
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillTickets();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillTickets();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PageIndex = 1;
                FillTickets();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
    }
}