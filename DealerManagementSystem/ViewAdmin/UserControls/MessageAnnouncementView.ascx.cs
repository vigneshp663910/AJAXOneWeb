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
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }
        public void fillViewMessage(long MessageAnnouncementHeaderID)
        {
            PMessageAnnouncementItem Item = new PMessageAnnouncementItem();
            Item.MessageAnnouncementHeaderID = MessageAnnouncementHeaderID;
            Item.ReadStatus = true;
            new BAPI().ApiPut("MessageNotification/UpdateMessageReadStatus", Item);

            MessageAnnouncementHeaderByID = new BMessageAnnouncement().GetMessageAnnouncementHeaderByID(MessageAnnouncementHeaderID);

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
            //ActionControlMange();
        }
    }
}