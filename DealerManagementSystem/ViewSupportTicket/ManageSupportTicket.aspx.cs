using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class ManageSupportTicket : System.Web.UI.Page
    {
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
                PageCount = 0;
                PageIndex = 1;
                new FillDropDownt().Category(ddlCategory, null, null);
                ddlCategory_SelectedIndexChanged(null, null);
                FillTicketSeverity();
                //  new FillDropDownt().SubCategory(ddlSubcategory, null, null, null);
                new FillDropDownt().Type(ddlTicketType, null, null);
                FillStatus();
                //  new FillDropDownt().Employee(ddlAssignedTo, null, null, "", "", "");
                //  new FillDropDownt().Employee(ddlCreatedBy, null, null, "", "", "");
                //  new FillDropDownt().Department(ddlDepartment); 
                //FillTickets();
                List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, null, true, null, null, null);
                new DDLBind(ddlEmployee, DealerUser, "ContactName", "UserID");                
                if (Session["DashboardTaskStatus"] != null)
                {                    
                    foreach (ListItem li in lbStatus.Items)
                    {
                        if(Session["DashboardTaskStatus"].ToString().Contains(li.Text))
                        {
                            li.Selected = true;
                        }
                    }
                }
                if (Session["DashboardTaskUserID"] != null)
                {
                    ddlEmployee.SelectedValue = Session["DashboardTaskUserID"].ToString();
                }
                PageIndex = 1;
                FillTickets();
            }
        }
        void FillStatus()
        {
            lbStatus.DataTextField = "Status";
            lbStatus.DataValueField = "StatusID";
            lbStatus.DataSource = new BTicketStatus().getTicketStatus(null, null);
            lbStatus.DataBind();
            lbStatus.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillTicketSeverity()
        {
            ddlSeverity.DataTextField = "Severity";
            ddlSeverity.DataValueField = "SeverityID";
            ddlSeverity.DataSource = new BTicketSeverity().getTicketSeverity(null, null);
            ddlSeverity.DataBind();
            ddlSeverity.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillTickets()
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
            PUser User = PSession.User;
            List<PTicketHeader> TicketHeader = new List<PTicketHeader>();
            int UserID = (ddlEmployee.SelectedValue == "0") ? PSession.User.UserID : Convert.ToInt32(ddlEmployee.SelectedValue);
            int RowCount = 0;
            TicketHeader = new BTickets().GetTicketDetailsSupport(TicketNO, null, CategoryID, SubCategoryID, SeverityID, TypeId, null, null, UserID, TicketStatus, TicketFrom, TicketTo, PageIndex, gvTickets.PageSize, out RowCount);

            if (RowCount == 0)
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
                PageCount = (RowCount + gvTickets.PageSize - 1) / gvTickets.PageSize;
                lblRowCount.Visible = true;
                ibtnArrowLeft.Visible = true;
                ibtnArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvTickets.PageSize) + 1) + " - " + (((PageIndex - 1) * gvTickets.PageSize) + gvTickets.Rows.Count) + " of " + RowCount;
            }

            for (int i = 0; i < gvTickets.Rows.Count; i++)
            {
                Label lblTicketID = (Label)gvTickets.Rows[i].FindControl("lblTicketID");
                ImageButton ibMessage = (ImageButton)gvTickets.Rows[i].FindControl("ibMessage");

                int count = new BForum().GetMessageViewStatusCound(Convert.ToInt32(lblTicketID.Text), UserID);
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
            Session["DashboardTaskStatus"] = null;
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
            int? SubCategoryID = null;
            int? TypeId = ddlTicketType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTicketType.SelectedValue);
            int UserID = (ddlEmployee.SelectedValue == "0") ? PSession.User.UserID : Convert.ToInt32(ddlEmployee.SelectedValue);
            string TicketStatus = "";
            DataTable dt = new DataTable(); ;
            foreach (ListItem li in lbStatus.Items)
            {
                if (li.Selected)
                {
                    TicketStatus = TicketStatus + "," + li.Text;
                }
            }
            int Index = 0;
            int Rowcount = 100;
            int CRowcount = Rowcount;
            while (Rowcount == CRowcount)
            {
                Index = Index + 1;
                DataTable dtTickets = new BTickets().GetTicketDetails_DT(TicketNO, null, CategoryID, SubCategoryID, null, TypeId, null, null, UserID, TicketStatus, Index, Rowcount);
                CRowcount = 0;
                dt.Merge(dtTickets);
                CRowcount = dtTickets.Rows.Count;
            }
            
            new BXcel().ExporttoExcel(dt, "TicketReport");

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            int UserID = (ddlEmployee.SelectedValue == "0") ? PSession.User.UserID : Convert.ToInt32(ddlEmployee.SelectedValue);
            int RowCount = 0;
            PTicketHeader H = new BTickets().GetTicketDetails(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text), null, null, null, null, null, null, null, null, null, null, null, PageIndex, gvTickets.PageSize, out RowCount)[0];

            new BTickets().UpdateTicketClosedStatus(H.HeaderID, UserID);

            FillTickets();
            lblMessage.Text = "Ticket is  successfully updated.";
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;
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
        protected void btnReassign_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblTicketID = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblTicketID");
            int RowCount = 0;
            PTicketHeader TH = new BTickets().GetTicketDetails(Convert.ToInt32(lblTicketID.Text), null, null, null, null, null, null, null, null, "", null, null, PageIndex, gvTickets.PageSize, out RowCount)[0];
            int ItemNo = 0;
            foreach (PTicketItem i in TH.TicketItems)
            {
                if (ItemNo < i.ItemID)
                {
                    ItemNo = i.ItemID;
                }
            }
            if (ItemNo == 0)
            {
                string url = "AssignSupportTicket.aspx?TicketNo=" + lblTicketID.Text;
                Response.Redirect(url);
            }
            Response.Redirect("ReassignSupportTicket.aspx?TicketNo=" + lblTicketID.Text + "&ItemNo=" + ItemNo.ToString());
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
            PageIndex = 1;
            FillTickets();
        }
    }
}