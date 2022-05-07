using Business;
using Properties;
using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class ClosedSupportTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Close');</script>");

            if (!IsPostBack)
            {
                FillCategory();

                FillTicketSeverity();
                FillTicketType();
                //FillStatus();
                //FillResolutionType(); 
                FillTickets();
            }
        }

        void FillCategory()
        {
            ddlCategory.DataTextField = "Category";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataSource = new BTicketCategory().getTicketCategory(null, null, null);
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

        //void FillStatus()
        //{
        //    ddlStatus.DataTextField = "TicketStatus";
        //    ddlStatus.DataValueField = "TicketStatusID";
        //    ddlStatus.DataSource = new BTicketStatus().getTicketStatus(null, null);
        //    ddlStatus.DataBind();
        //    ddlStatus.Items.Insert(0, new ListItem("Select", "0"));
        //}

        //void FillResolutionType()
        //{
        //    ddlResolutionType.DataTextField = "TicketResolutionType";
        //    ddlResolutionType.DataValueField = "TicketResolutionTypeID";
        //    ddlResolutionType.DataSource = new BTicketResolutionType().getTicketResolutionType(null, null);
        //    ddlResolutionType.DataBind();
        //    ddlResolutionType.Items.Insert(0, new ListItem("Select", "0"));
        //}

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
            int? TicketType = ddlTicketType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTicketType.SelectedValue);
             

              gvTickets.DataSource = new BTickets().GetTicketToClose(null, TicketCategoryID, TicketSubCategoryID, TicketType, PSession.User.UserID, null);
            gvTickets.DataBind();
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
            string url = "AssignTicket.aspx?TicketNo=" + ((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text;
            Response.Redirect(url);
        }
        protected void gvTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillTickets();
            gvTickets.PageIndex = e.NewPageIndex;
            gvTickets.DataBind();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            PTicketHeader H = new BTickets().GetTicketDetails(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text), null, null, null, null, null, null, null, null, null)[0];

            new BTickets().UpdateTicketClosedStatus(H.HeaderID);

            FillTickets();
            lblMessage.Text = "Ticket is  successfully updated.";
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;
        }
    }
}