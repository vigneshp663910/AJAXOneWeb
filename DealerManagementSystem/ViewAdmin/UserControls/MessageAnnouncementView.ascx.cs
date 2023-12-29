using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin.UserControls
{
    public partial class MessageAnnouncementView : System.Web.UI.UserControl
    {
        public PMessageAnnouncementHeader MessageAnnouncementHeaderByID
        {
            get
            {
                if (ViewState["PMessageAnnouncementHeaderByID"] == null)
                {
                    ViewState["PMessageAnnouncementHeaderByID"] = new PMessageAnnouncementHeader();
                }
                return (PMessageAnnouncementHeader)ViewState["PMessageAnnouncementHeaderByID"];
            }
            set
            {
                ViewState["PMessageAnnouncementHeaderByID"] = value;
            }
        }
        private int PageCountView
        {
            get
            {
                if (ViewState["PageCountView"] == null)
                {
                    ViewState["PageCountView"] = 0;
                }
                return (int)ViewState["PageCountView"];
            }
            set
            {
                ViewState["PageCountView"] = value;
            }
        }
        private int PageIndexView
        {
            get
            {
                if (ViewState["PageIndexView"] == null)
                {
                    ViewState["PageIndexView"] = 1;
                }
                return (int)ViewState["PageIndexView"];
            }
            set
            {
                ViewState["PageIndexView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                PageCountView = 0;
                PageIndexView = 1;
            }
        }
        public void fillViewMessage(long MessageAnnouncementHeaderID)
        {
            PMessageAnnouncementItem Item = new PMessageAnnouncementItem();
            Item.MessageAnnouncementHeaderID = MessageAnnouncementHeaderID;
            Item.ReadStatus = true;
            new BAPI().ApiPut("MessageNotification/UpdateMessageReadStatus", Item);

            PApiResult Result = new PApiResult();
            Result = new BMessageAnnouncement().GetMessageAnnouncementHeaderByID(MessageAnnouncementHeaderID, PageIndexView, gvMessageTo.PageSize);
            MessageAnnouncementHeaderByID = JsonConvert.DeserializeObject<PMessageAnnouncementHeader>(JsonConvert.SerializeObject(Result.Data));

            lblNotificationNumber.Text = MessageAnnouncementHeaderByID.MessageAnnouncementHeaderID.ToString();
            lblDate.Text = MessageAnnouncementHeaderByID.CreatedOn.ToString();
            lblValidFrom.Text = MessageAnnouncementHeaderByID.ValidFrom.ToString();
            lblValidTo.Text = MessageAnnouncementHeaderByID.ValidTo.ToString();
            lblCreatedBy.Text = MessageAnnouncementHeaderByID.CreatedBy.ContactName;
            lblSubject.Text = MessageAnnouncementHeaderByID.Subject;
            lblMailResponce.Text = (MessageAnnouncementHeaderByID.MailResponce == true) ? "Read" : (MessageAnnouncementHeaderByID.MailResponce == false) ? "Pending" : "Partial";
            lblMsg.Text = MessageAnnouncementHeaderByID.Message;

            gvMessageTo.DataSource = MessageAnnouncementHeaderByID.Item;
            gvMessageTo.DataBind();
            if (Result.RowCount == 0)
            {
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
            else
            {
                PageCountView = (Result.RowCount + gvMessageTo.PageSize - 1) / gvMessageTo.PageSize;
                lblRowCount.Visible = true;
                ibtnArrowLeft.Visible = true;
                ibtnArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndexView - 1) * gvMessageTo.PageSize) + 1) + " - " + (((PageIndexView - 1) * gvMessageTo.PageSize) + gvMessageTo.Rows.Count) + " of " + Result.RowCount;
            }
            //ActionControlMange();
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndexView > 1)
            {
                PageIndexView = PageIndexView - 1;
                fillViewMessage(MessageAnnouncementHeaderByID.MessageAnnouncementHeaderID);
                gvMessageTo.PageSize = gvMessageTo.PageIndex - 1;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCountView > PageIndexView)
            {
                PageIndexView = PageIndexView + 1;
                fillViewMessage(MessageAnnouncementHeaderByID.MessageAnnouncementHeaderID);
                gvMessageTo.PageSize = gvMessageTo.PageIndex + 1;
            }
        }

        protected void gvMessageTo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMessageTo.PageIndex = e.NewPageIndex;
            PageIndexView = gvMessageTo.PageIndex+1;
            fillViewMessage(MessageAnnouncementHeaderByID.MessageAnnouncementHeaderID);
        }
    }
}