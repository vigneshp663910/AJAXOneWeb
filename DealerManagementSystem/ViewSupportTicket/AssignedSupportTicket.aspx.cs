using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class AssignedSupportTicket : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Assigned');</script>");


            if (!IsPostBack)
            {
                FillCategory();
                FillTicketSeverity();
                FillTickets();

                //if (PSession.User.UserTypeID == (short)UserTypes.Manager || PSession.User.UserTypeID == (short)UserTypes.Admin)
                //    gvTickets.Columns[15].Visible = true;
                //else if (PSession.User.UserTypeID == (short)UserTypes.Associate)
                //    gvTickets.Columns[15].Visible = false;
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
        void FillTickets()
        {
            string TicketNO = txtTicketNo.Text.Trim();
            int? TicketCategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            int? TicketSubCategoryID = null;
            if (ddlSubcategory.Items.Count > 0)
            {
                TicketSubCategoryID = ddlSubcategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSubcategory.SelectedValue);
            }
            int? TicketSeverity = ddlSeverity.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSeverity.SelectedValue);
            
            gvTickets.DataSource = new BTickets().GetAssignedTickets(TicketNO, TicketCategoryID, TicketSubCategoryID, TicketSeverity,PSession.User.UserID);
            gvTickets.DataBind();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillTickets();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubCategory();
        }
        protected void lbTicketNo_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            string status = ((Label)gvTickets.Rows[index].FindControl("lblTicketStatus")).Text;
            string url;

            if (status != "Resolved")
            {
                url = "AssignTicket.aspx?TicketNo=" + ((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text;
            }
            else
            {
                url = "TicketView.aspx?TicketNo=" + ((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text;
            }


            //  url = "AssignTicket.aspx?TicketNo=" + ((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text;
            Response.Redirect(url);
        }
        protected void gvTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillTickets();
            gvTickets.PageIndex = e.NewPageIndex;
            gvTickets.DataBind();
        }

        protected void btnInProgress_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label ItemId = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblItemID");
            new BTickets().UpdateTicketStatus(Convert.ToInt32(ItemId.Text), 3);
            FillTickets();
        }

        protected void ibMessage_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            //string url = "SupportTicketView.aspx?TicketNo=" + ((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text;
            //Response.Redirect(url);
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
            Label TicketNo = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblTicketID");
            Label ItemNo = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblItemID");

            Response.Redirect("ReassignSupportTicket.aspx?TicketNo=" + TicketNo.Text + "&ItemNo=" + ItemNo.Text);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label TicketNo = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblTicketID");
            Label ItemNo = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblItemID");

            //Response.Redirect("ReassignSupportTicket.aspx?TicketNo=" + TicketNo.Text + "&ItemNo=" + ItemNo.Text);

            new BTickets().UpdateTicketCancelStatus(Convert.ToInt32(TicketNo.Text), Convert.ToInt32(ItemNo.Text), PSession.User.UserID);
            FillTickets();
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divSupportTicketView.Visible = false;
            btnBackToList.Visible = false;
            divList.Visible = true;
        }
    }
}