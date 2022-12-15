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

                new FillDropDownt().Category(ddlCategory, null,null);
                //  new FillDropDownt().SubCategory(ddlSubcategory, null, null, null);
                new FillDropDownt().Type(ddlTicketType, null, null);
                FillStatus();
                //  new FillDropDownt().Employee(ddlAssignedTo, null, null, "", "", "");
                //  new FillDropDownt().Employee(ddlCreatedBy, null, null, "", "", "");
                //  new FillDropDownt().Department(ddlDepartment); 
                //FillTickets();
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

        void FillTickets()
        {
            int? TicketNO = string.IsNullOrEmpty(txtTicketNo.Text.Trim()) ? (int?)null : Convert.ToInt32(txtTicketNo.Text.Trim());
            int? CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            int? SubCategoryID = null;
            //if (ddlSubcategory.Items.Count > 0)
            //{
            //    SubCategoryID = ddlSubcategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSubcategory.SelectedValue);
            //}

            // int? CreatedBy = ddlCreatedBy.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCreatedBy.SelectedValue);
            int? TypeId = ddlTicketType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTicketType.SelectedValue);
            string TicketStatus = "";// lbStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(lbStatus.SelectedValue);
            foreach (ListItem li in lbStatus.Items)
            {
                if (li.Selected)
                {
                    TicketStatus = TicketStatus + "," + li.Text;
                }
            }
            PUser User = PSession.User;

            //  gvTickets.DataSource = new BTickets().GetManageTickets(TicketNO, TicketStatus, TicketCategoryID, TicketSubCategoryID, TicketType, CreatedBy, DepartmentID, AssignedTo, usreID);
            List<PTicketHeader> TicketHeader = new List<PTicketHeader>();
            //if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer)
            //{ 
            TicketHeader = new BTickets().GetTicketDetails(TicketNO, null, CategoryID, SubCategoryID, null, TypeId, null, null, PSession.User.UserID, TicketStatus);

            // if (PSession.User.UserName.Contains("IT."))
            if ((User.Designation.DealerDesignationID == (short)DealerDesignation.BusinessSystemHead) || (User.Designation.DealerDesignationID == (short)DealerDesignation.BusinessSystemManager))
            {
                // TicketHeader = new BTickets().GetTicketDetails(TicketNO, null, CategoryID, SubCategoryID, null, TypeId, null, null, null, TicketStatus);
                gvTickets.Columns[10].Visible = true;
            }
            else
            {
                // TicketHeader = new BTickets().GetTicketDetails(TicketNO, null, CategoryID, SubCategoryID, null, TypeId, null, null, PSession.User.UserID, TicketStatus);
                gvTickets.Columns[10].Visible = false;
                //gvTickets.Columns[11].Visible = false;
            }
            // }

            gvTickets.DataSource = TicketHeader;
            gvTickets.DataBind();
            for (int i = 0; i < gvTickets.Rows.Count; i++)
            {
                Label lblTicketID = (Label)gvTickets.Rows[i].FindControl("lblTicketID");
                Label lblTicketStatus = (Label)gvTickets.Rows[i].FindControl("lblTicketStatus");
                Button btnClose = (Button)gvTickets.Rows[i].FindControl("btnClose");
                //Button btnReassign = (Button)gvTickets.Rows[i].FindControl("btnReassign");
                ImageButton ibMessage = (ImageButton)gvTickets.Rows[i].FindControl("ibMessage");
                if (lblTicketStatus.Text == "Resolved")
                {
                    btnClose.Visible = true;
                }
                else
                {
                    btnClose.Visible = false;
                }

                //if (lblTicketStatus.Text == "Resolved" || lblTicketStatus.Text == "Closed")
                //{
                //    btnReassign.Visible = false;
                //}
                //else
                //{
                //    btnReassign.Visible = true;
                //}
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FillTickets();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            // new FillDropDownt().SubCategory(ddlSubcategory, null, null, CategoryID);
        }

        //protected void lbTicketNo_Click(object sender, EventArgs e)
        //{

        //}

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
            string TicketStatus = "";
            foreach (ListItem li in lbStatus.Items)
            {
                if (li.Selected)
                {
                    TicketStatus = TicketStatus + "," + li.Text;
                }
            }
            string usreID = PSession.User.UserName;

            DataTable dt = new BTickets().GetTicketDetails_DT(TicketNO, null, CategoryID, SubCategoryID, null, TypeId, null, null, PSession.User.UserID, TicketStatus);

            new BXcel().ExporttoExcel(dt, "TicketReport");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            PTicketHeader H = new BTickets().GetTicketDetails(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblTicketID")).Text), null, null, null, null, null, null, null, null, null)[0];

            new BTickets().UpdateTicketClosedStatus(H.HeaderID, PSession.User.UserID);

            FillTickets();
            lblMessage.Text = "Ticket is  successfully updated.";
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;
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
            Label lblTicketID = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblTicketID");
            PTicketHeader TH = new BTickets().GetTicketDetails(Convert.ToInt32(lblTicketID.Text), null, null, null, null, null, null, null, null, "")[0];
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
    }
}