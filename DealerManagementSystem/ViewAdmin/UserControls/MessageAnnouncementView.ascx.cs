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
            MessageAnnouncementHeaderByID = new BMessageAnnouncement().GetMessageAnnouncementHeaderByID(MessageAnnouncementHeaderID);


            lblNotificationNumber.Text = MessageAnnouncementHeaderByID.MessageAnnouncementHeaderID.ToString();
            lblDate.Text = MessageAnnouncementHeaderByID.CreatedOn.ToString();
            lblValidFrom.Text = MessageAnnouncementHeaderByID.ValidFrom.ToString();
            lblValidTo.Text = MessageAnnouncementHeaderByID.ValidTo.ToString();
            lblCreatedBy.Text = MessageAnnouncementHeaderByID.CreatedBy.ContactName;
            lblMailResponce.Text = (MessageAnnouncementHeaderByID.MailResponce == true) ? "Read" : (MessageAnnouncementHeaderByID.MailResponce == false) ? "Pending" : "Partial";
            lblMsg.Text = MessageAnnouncementHeaderByID.Message;

            gvMessageTo.DataSource = MessageAnnouncementHeaderByID.Item;
            gvMessageTo.DataBind();
            //ActionControlMange();
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            //if (lbActions.Text == "Release PO")
            //{
            //    lblMessage.Visible = true;
            //    PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("PurchaseOrder/ReleasePurchaseOrder?PurchaseOrderID=" + PurchaseOrder.PurchaseOrderID.ToString()));
            //    if (Results.Status == PApplication.Failure)
            //    {
            //        lblMessage.Text = Results.Message;
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //    lblMessage.Text = "Updated Successfully";
            //    lblMessage.ForeColor = Color.Green;
            //    fillViewPO(PurchaseOrder.PurchaseOrderID);
            //}
        }
    }
}