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
    public partial class InProgressSupportTicket : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSupportTicket_InProgressSupportTicket; } }
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
        //private List<PAttachedFile> AttchedFile
        //{
        //    get
        //    {
        //        if (ViewState["NewAttchedFile"] == null)
        //        {
        //            ViewState["NewAttchedFile"] = new List<PAttachedFile>();
        //        }
        //        return (List<PAttachedFile>)ViewState["NewAttchedFile"];
        //    }
        //}
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » InProgress');</script>");

            if (!IsPostBack)
            {
                FillCategory();
                FillTicketSeverity();
                FillTicketType();
                FillTickets();
            }            
        }
        void FillCategory()
        {
            ddlCategory.DataTextField = "Category";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataSource = JsonConvert.DeserializeObject<List<PCategory>>(JsonConvert.SerializeObject(new BTickets().getTicketCategory(null, null).Data));
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillSubCategory()
        {
            ddlSubcategory.DataTextField = "SubCategory";
            ddlSubcategory.DataValueField = "SubCategoryID";
            ddlSubcategory.DataSource = JsonConvert.DeserializeObject<List<PSubCategory>>(JsonConvert.SerializeObject(new BTickets().getTicketSubCategory(null, null, Convert.ToInt32(ddlCategory.SelectedValue)).Data));
            ddlSubcategory.DataBind();
            ddlSubcategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillTicketSeverity()
        {
            ddlSeverity.DataTextField = "Severity";
            ddlSeverity.DataValueField = "SeverityID";
            ddlSeverity.DataSource = JsonConvert.DeserializeObject<List<PSeverity>>(JsonConvert.SerializeObject(new BTickets().getTicketSeverity(null, null).Data));
            ddlSeverity.DataBind();
            ddlSeverity.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillTicketType()
        {
            ddlTicketType.DataTextField = "Type";
            ddlTicketType.DataValueField = "TypeID";
            ddlTicketType.DataSource = JsonConvert.DeserializeObject<List<PType>>(JsonConvert.SerializeObject(new BTickets().getTicketType(null, null).Data));
            ddlTicketType.DataBind();
            ddlTicketType.Items.Insert(0, new ListItem("Select", "0"));
        }
        void FillTickets()
        {
            long? HeaderId = string.IsNullOrEmpty(txtTicketNo.Text.Trim()) ? (int?)null : Convert.ToInt32(txtTicketNo.Text.Trim());
            int? TicketCategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            int? TicketSubCategoryID = null;
            if (ddlSubcategory.Items.Count > 0)
            {
                TicketSubCategoryID = ddlSubcategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSubcategory.SelectedValue);
            }
            int? TicketSeverity = ddlSeverity.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSeverity.SelectedValue);
            int? TicketType = ddlTicketType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTicketType.SelectedValue);
            int UserID = PSession.User.UserID;
            string AssignedTo = PSession.User.UserName;
            int RowCount = 0;
            List<PTicketHeader> TicketHeader = new List<PTicketHeader>();
            PApiResult Result = new BTickets().GetInProgressTickets(HeaderId, TicketCategoryID, TicketSubCategoryID, TicketSeverity, PageIndex, gvTickets.PageSize);
            TicketHeader = JsonConvert.DeserializeObject<List<PTicketHeader>>(JsonConvert.SerializeObject(Result.Data));
            //TicketHeader = new BTickets().GetInProgressTickets(HeaderId, TicketCategoryID, TicketSubCategoryID, TicketSeverity, UserID, PageIndex, gvTickets.PageSize, out RowCount);

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
                Label lblTicketID = (Label)gvTickets.Rows[i].FindControl("lblHeaderId");
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
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
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
            gvTickets.PageIndex = e.NewPageIndex;
            FillTickets();
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
        protected void ibMessage_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            divSupportTicketView.Visible = true;
            btnBackToList.Visible = true;
            pnSearch.Visible = false;
            divGrid.Visible = false;
            UC_SupportTicketView.FillTickets(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblHeaderId")).Text));
            UC_SupportTicketView.FillChat(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblHeaderId")).Text));
            UC_SupportTicketView.FillChatTemp(Convert.ToInt32(((Label)gvTickets.Rows[index].FindControl("lblHeaderId")).Text));
        }

        //protected void btnReassign_Click(object sender, EventArgs e)
        //{
        //    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //    Label TicketNo = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblHeaderId");
        //    Label ItemNo = (Label)gvTickets.Rows[gvRow.RowIndex].FindControl("lblItemID");
        //    Response.Redirect("ReassignSupportTicket.aspx?TicketNo=" + TicketNo.Text + "&ItemNo=" + ItemNo.Text);
        //}
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divSupportTicketView.Visible = false;
            btnBackToList.Visible = false;
            pnSearch.Visible = true;
            divGrid.Visible = true;
            //pnResolve.Visible = false;
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
    }
}