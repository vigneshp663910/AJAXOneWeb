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
                //FillResolutionType();

                FillTickets();
                //if (PSession.User.UserTypeID == (short)UserTypes.Manager || PSession.User.UserTypeID == (short)UserTypes.Admin)
                //{
                //    gvTickets.Columns[17].Visible = true;
                //}
                //else if (PSession.User.UserTypeID == (short)UserTypes.Associate)
                //{
                //    gvTickets.Columns[17].Visible = false;
                //}
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
        


        void FillTickets()
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

            gvTickets.DataSource = new BTickets().GetInProgressTickets(HeaderId, TicketCategoryID, TicketSubCategoryID, TicketSeverity);
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
            //if (PSession.User.UserTypeID == (short)UserTypes.Manager || PSession.User.UserTypeID == (short)UserTypes.Admin)
            //    gvTickets.Columns[15].Visible = true;
            //else if (PSession.User.UserTypeID == (short)UserTypes.Associate)
            //    gvTickets.Columns[15].Visible = false;
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
            string url = "TicketView.aspx?TicketNo=" + ((Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblHeaderId")).Text;
            Response.Redirect(url);
        }
        protected void gvTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillTickets();
            gvTickets.PageIndex = e.NewPageIndex;
            gvTickets.DataBind();
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
        protected void ibMessage_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            //string url = "SupportTicketView.aspx?TicketNo=" + ((Label)gvTickets.Rows[index].FindControl("lblHeaderId")).Text;
            //Response.Redirect(url);
            divSupportTicketView.Visible = true;
            btnBackToList.Visible = true;
            pnSearch.Visible = false;
            //pnResolve.Visible = false;
            divGrid.Visible = false;
            UC_SupportTicketView.FillTickets(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblHeaderId")).Text));
            UC_SupportTicketView.FillChat(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblHeaderId")).Text));
            UC_SupportTicketView.FillChatTemp(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblHeaderId")).Text));
        }

        protected void btnReassign_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label TicketNo = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblHeaderId");
            Label ItemNo = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblItemID");
            Response.Redirect("ReassignSupportTicket.aspx?TicketNo=" + TicketNo.Text + "&ItemNo=" + ItemNo.Text);
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divSupportTicketView.Visible = false;
            btnBackToList.Visible = false;
            pnSearch.Visible = true;
            divGrid.Visible = true;
            //pnResolve.Visible = false;
        }
    }
}