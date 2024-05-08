using Business;
using DealerManagementSystem.ViewAdmin.UserControls;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class MessageAnnouncement : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_MessageAnnouncement; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
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
        public List<PMessageAnnouncementHeader> Message
        {
            get
            {
                if (ViewState["PMessageAnnouncement"] == null)
                {
                    ViewState["PMessageAnnouncement"] = new List<PMessageAnnouncementHeader>();
                }
                return (List<PMessageAnnouncementHeader>)ViewState["PMessageAnnouncement"];
            }
            set
            {
                ViewState["PMessageAnnouncement"] = value;
            }
        }
        long? MessageAnnouncementHeaderID = null;
        string Subject = null;
        string SentFrom = null;
        string SentTo = null;
        int? SentBy = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Notification');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                List<PUser> user = new BUser().GetUsers(null, null, null, null, null, true, null, null, null);
                new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
                SentBy = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);

                List<PUser> User = new BMessageAnnouncement().GetMessageAnnouncementAccess();

                if (User.Count() == 0)
                {
                    ChkGetAllMessage.Visible = false;
                    divChkIT.Visible = false;
                    btnMessage.Visible = false;
                }
                if (Session["MessageAnnouncementId"] != null)
                {
                    divMessageAnnouncementView.Visible = true;
                    divList.Visible = false;
                    UC_MessageAnnouncementView.fillViewMessage(Convert.ToInt64(Session["MessageAnnouncementId"]));
                    Session["MessageAnnouncementId"] = null;
                }
            }
        }
        void Search()
        {
            MessageAnnouncementHeaderID = string.IsNullOrEmpty(txtNotificationNo.Text) ? (long?)null: Convert.ToInt32(txtNotificationNo.Text.Trim());
            Subject = txtSubject.Text.Trim();
            SentFrom = txtDateFrom.Text.Trim();
            SentTo = txtDateTo.Text.Trim();
            SentBy = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
        }
        void Fill()
        {
            Search();

            PApiResult Result = new PApiResult();



            if (ChkGetAllMessage.Checked)
            {
                Result = new BMessageAnnouncement().GetMessageAnnouncementHeaderAllNotification(MessageAnnouncementHeaderID, Subject, SentFrom, SentTo, SentBy, null, null, PageIndex, gvMessageAnnouncement.PageSize);
                Message = JsonConvert.DeserializeObject<List<PMessageAnnouncementHeader>>(JsonConvert.SerializeObject(Result.Data));
            }
            else
            {
                Result = new BMessageAnnouncement().GetMessageAnnouncementHeader(MessageAnnouncementHeaderID, Subject, SentFrom, SentTo, SentBy, null, DateTime.Now.ToString("yyyy-MM-dd"), PageIndex, gvMessageAnnouncement.PageSize);
                Message = JsonConvert.DeserializeObject<List<PMessageAnnouncementHeader>>(JsonConvert.SerializeObject(Result.Data));
            }
            gvMessageAnnouncement.DataSource = Message;
            gvMessageAnnouncement.DataBind();


            if (Result.RowCount == 0)
            {
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
            else
            {
                PageCount = (Result.RowCount + gvMessageAnnouncement.PageSize - 1) / gvMessageAnnouncement.PageSize;
                lblRowCount.Visible = true;
                ibtnArrowLeft.Visible = true;
                ibtnArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvMessageAnnouncement.PageSize) + 1) + " - " + (((PageIndex - 1) * gvMessageAnnouncement.PageSize) + gvMessageAnnouncement.Rows.Count) + " of " + Result.RowCount;
            }
            foreach (GridViewRow row in gvMessageAnnouncement.Rows)
            {
                LinkButton LnkForwardMessage = (LinkButton)row.FindControl("LnkForwardMessage");
                LinkButton LnkDraftEdit = (LinkButton)row.FindControl("LnkDraftEdit");
                Label lblStatus = (Label)row.FindControl("lblStatus");
                if (lblStatus.Text == "Sent")
                {
                    LnkForwardMessage.Visible = true;
                    LnkDraftEdit.Visible = false;
                }
                else
                {
                    LnkForwardMessage.Visible = false;
                    LnkDraftEdit.Visible = true;
                }
                List<PUser> User = new BMessageAnnouncement().GetMessageAnnouncementAccess();

                if (User.Count() == 0)
                {
                    LnkForwardMessage.Visible = false;
                    LnkDraftEdit.Visible = false;
                }
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                Fill();
            }
        }

        protected void btnMessage_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            divMessageAnnouncementCreate.Visible = true;
            lblMessage.Text = "";
            Button BtnView = (Button)sender;
            UC_MessageAnnouncementCreate.FillMaster();
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divMessageAnnouncementCreate.Visible = false;
            divMessageAnnouncementView.Visible = false;
        }

        protected void btnViewMessage_Click(object sender, EventArgs e)
        {
            divMessageAnnouncementView.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblMessageAnnouncementId = (Label)gvRow.FindControl("lblNotificationNo");
            UC_MessageAnnouncementView.fillViewMessage(Convert.ToInt64(lblMessageAnnouncementId.Text));
        }
        protected void btnViewBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divMessageAnnouncementCreate.Visible = false;
            divMessageAnnouncementView.Visible = false;
        }

        protected void LnkForwardMessage_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            divMessageAnnouncementCreate.Visible = true;
            lblMessage.Text = "";
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblMessageAnnouncementHeaderId = (Label)gvRow.FindControl("lblNotificationNo");
            UC_MessageAnnouncementCreate.FillMasterEdit(Convert.ToInt32(lblMessageAnnouncementHeaderId.Text), "");
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                Fill();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PageCount = 0;
            PageIndex = 1;
            Fill();
        }
        protected void LnkDraftEdit_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            divMessageAnnouncementCreate.Visible = true;
            lblMessage.Text = "";
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblMessageAnnouncementHeaderId = (Label)gvRow.FindControl("lblNotificationNo");
            UC_MessageAnnouncementCreate.FillMasterEdit(Convert.ToInt32(lblMessageAnnouncementHeaderId.Text), "Draft");
        }
        protected void ChkIT_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkIT.Checked)
            {
                List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, null, true, null, (short)DealerDepartment.BusinessSystem, null);
                new DDLBind(ddlDealerEmployee, DealerUser, "ContactName", "UserID");
            }
        }
    }
}