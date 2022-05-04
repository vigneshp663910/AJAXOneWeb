using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class AddCustomerConversation : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
            }
        }
        public void FillMaster(long LeadID)
        {
            List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID, true);
            List<PUser> U = new List<PUser>();
            foreach (PLeadSalesEngineer SE in SalesEngineer)
            {
                U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            }
            new DDLBind(ddlSalesEngineer, U, "ContactName", "UserID");
            new DDLBind( ddlProgressStatus , new BLead().GetLeadProgressStatus(null, null), "ProgressStatus", "ProgressStatusID"); 
        } 
        public PLeadConversation ReadConversation()
        {
            PLeadConversation Lead = new PLeadConversation();
            Lead.LeadConversationID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            Lead.SalesEngineer = new PUser { UserID = Convert.ToInt32(ddlSalesEngineer.SelectedValue) };
            Lead.ProgressStatus = new PLeadProgressStatus() { ProgressStatusID = Convert.ToInt32(ddlProgressStatus.SelectedValue) };
            Lead.ConversationDate = Convert.ToDateTime(txtConversationDate.Text.Trim());
            Lead.Conversation = txtConversation.Text.Trim(); 
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };

            return Lead;
        }

        public string ValidationConversation()
        {
            string Message = "";
            ddlSalesEngineer.BorderColor = Color.Silver;
            ddlProgressStatus.BorderColor = Color.Silver; 
            txtConversationDate.BorderColor = Color.Silver;
            txtConversation.BorderColor = Color.Silver;

            if (ddlSalesEngineer.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Sales Engineer";
                ddlSalesEngineer.BorderColor = Color.Red;
            } 
            if (ddlProgressStatus.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Progress Status";
                ddlProgressStatus.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtConversationDate.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Date";
                txtConversationDate.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtConversation.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Conversation";
                txtConversation.BorderColor = Color.Red;
            }
            return Message;
        }
    }
}