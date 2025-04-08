using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService.UserControls
{
    public partial class OnlineServiceTicketView : System.Web.UI.UserControl
    {
        public POnlineServiceTicket  Ticket
        {
            get
            {
                if (ViewState["OnlineServiceTicketView"] == null)
                {
                    ViewState["OnlineServiceTicketView"] = new POnlineServiceTicket();
                }
                return (POnlineServiceTicket)ViewState["OnlineServiceTicketView"];
            }
            set
            {
                ViewState["OnlineServiceTicketView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageRestore.Text = "";
            lblEscalatedL1Message.Text = "";
            lblCustomerSatisfactionLevelMessage.Text = "";
            lblMessage.Text = "";
        }

        protected void btnPopup_Click(object sender, EventArgs e)
        {
            Button lbActions = ((Button)sender);
            if (lbActions.ID == "btnUpdateRestore")
            {
                MPE_UpdateRestore.Show();
                PApiResult Results = new BDMS_ICTicket().UpdateOnlineServiceTicketStatus(Ticket.OnlineServiceTicketID, (short)StatusItem.OnlineServiceTicketStatus_Restored, txtRestoreRemarks.Text.Trim(), null);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessageRestore.Text = Results.Message;
                    lblMessageRestore.ForeColor = Color.Red;
                    return;
                }
                lblMessage.Text = "Updated successfully";
                lblMessage.ForeColor = Color.Red;
                FillOnlineServiceTicket(Ticket.OnlineServiceTicketID);
                MPE_UpdateRestore.Hide();
            }
            else if (lbActions.ID == "btnUpdateEscalatedL1")
            {
                MPE_EscalatedL1.Show();
                PApiResult Results = new BDMS_ICTicket().UpdateOnlineServiceTicketStatus(Ticket.OnlineServiceTicketID, (short)StatusItem.OnlineServiceTicketStatus_EscalatedL1, txtEscalatedL1Remark.Text.Trim(), ddlAjaxEmployee.SelectedValue);
                if (Results.Status == PApplication.Failure)
                {
                    lblEscalatedL1Message.Text = Results.Message;
                    lblEscalatedL1Message.ForeColor = Color.Red;
                    return;
                }
                lblMessage.Text = "Updated successfully";
                lblMessage.ForeColor = Color.Red;
                FillOnlineServiceTicket(Ticket.OnlineServiceTicketID);
                MPE_EscalatedL1.Hide();
            }
            else if (lbActions.ID == "btnUpdateCustomerSatisfactionLevel")
            {
                MPE_CustomerSatisfactionLevelk.Show();
                PApiResult Results = new BDMS_ICTicket().UpdateOnlineServiceTickeCustomerSatisfactionLevel(Ticket.OnlineServiceTicketID,ddlCustomerSatisfactionLevel.SelectedValue);
                if (Results.Status == PApplication.Failure)
                {
                    lblCustomerSatisfactionLevelMessage.Text = Results.Message;
                    lblCustomerSatisfactionLevelMessage.ForeColor = Color.Red;
                    return;
                }
                lblMessage.Text = "Updated successfully";
                lblMessage.ForeColor = Color.Red;
                FillOnlineServiceTicket(Ticket.OnlineServiceTicketID);
                MPE_CustomerSatisfactionLevelk.Hide();
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {

            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbtnRestore")
            {
                MPE_UpdateRestore.Show();
            }
            else if (lbActions.ID == "lbtnEscalatedL1")
            {
                ddlAjaxEmployee.DataTextField = "ContactName";
                ddlAjaxEmployee.DataValueField = "UserID";
                ddlAjaxEmployee.DataSource = JsonConvert.DeserializeObject<List<PUser>>(JsonConvert.SerializeObject(new BDMS_ICTicket().GetUserForOnlineServiceTicketSupport().Data));;
                ddlAjaxEmployee.DataBind(); 

                MPE_EscalatedL1.Show();
            }
            else if (lbActions.ID == "lbtnEscalatedDealer")
            {
                PApiResult Results = new BDMS_ICTicket().UpdateOnlineServiceTicketStatus(Ticket.OnlineServiceTicketID,(short)StatusItem.OnlineServiceTicketStatus_EscalatedDealer, null, null);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                lblMessage.Text = "Updated successfully";
                lblMessage.ForeColor = Color.Red;
                FillOnlineServiceTicket(Ticket.OnlineServiceTicketID);
            }
            else if (lbActions.ID == "lbtnUpdateCustomerSatisfactionLevel")
            {
                FillCustomerSatisfactionLevel();
                MPE_CustomerSatisfactionLevelk.Show();
            }
        }
        public void FillOnlineServiceTicket(long ICTicketID)
        {
            PApiResult Result = new BDMS_ICTicket().GetOnlineServiceTicket(ICTicketID, null, null, null, null, null, null, 0);
            Ticket = JsonConvert.DeserializeObject<List<POnlineServiceTicket>>(JsonConvert.SerializeObject(Result.Data))[0];
            lblTicket.Text = Ticket.OnlineTicketNumber;

            ActionControlMange();
        }
        void ActionControlMange()
        {
            //int ServiceTypeID = SDMS_ICTicket.ServiceType.ServiceTypeID;

            lbtnRestore.Visible = true;
            lbtnEscalatedL1.Visible = true;
            lbtnEscalatedDealer.Visible = true;
            lbtnUpdateCustomerSatisfactionLevel.Visible = true;

            if ((Ticket.Status.StatusItemID == (short)StatusItem.OnlineServiceTicketStatus_Requested))
            {
                if (Ticket.RegisteredBy.UserID != PSession.User.UserID)
                {
                    lbtnRestore.Visible = false;
                    lbtnEscalatedL1.Visible = false;
                    lbtnEscalatedDealer.Visible = false; 
                }
                lbtnUpdateCustomerSatisfactionLevel.Visible = false;
            }
            else if ((Ticket.Status.StatusItemID == (short)StatusItem.OnlineServiceTicketStatus_EscalatedL1))
            {
                lbtnEscalatedL1.Visible = false;
                lbtnUpdateCustomerSatisfactionLevel.Visible = false;
            }
            else if ((Ticket.Status.StatusItemID == (short)StatusItem.OnlineServiceTicketStatus_EscalatedDealer))
            {
                lbtnRestore.Visible = false;
                lbtnEscalatedL1.Visible = false;
                lbtnEscalatedDealer.Visible = false;
                lbtnUpdateCustomerSatisfactionLevel.Visible = false;
            }
            else if((Ticket.Status.StatusItemID == (short)StatusItem.OnlineServiceTicketStatus_Restored))
            {
                lbtnRestore.Visible = false;
                lbtnEscalatedL1.Visible = false;
                lbtnEscalatedDealer.Visible = false; 
            }

            if (!string.IsNullOrEmpty( Ticket.CustomerSatisfactionLevel.CustomerSatisfactionLevel))
            {
                lbtnUpdateCustomerSatisfactionLevel.Visible = false; 
            }

            //List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            //if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.RequestForDecline).Count() == 0)
            //{
            //    lbtnRequestForDecline.Visible = false;
            //}
            //if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.MarginWarrantyRequest).Count() == 0)
            //{
            //    lbtnMarginWarrantyRequest.Visible = false;
            //}

        }

        private void FillCustomerSatisfactionLevel()
        {
            ddlCustomerSatisfactionLevel.DataTextField = "CustomerSatisfactionLevel";
            ddlCustomerSatisfactionLevel.DataValueField = "CustomerSatisfactionLevelID";
            ddlCustomerSatisfactionLevel.DataSource = new BDMS_Service().GetCustomerSatisfactionLevel(null, null);
            ddlCustomerSatisfactionLevel.DataBind(); 
        }
    }
}