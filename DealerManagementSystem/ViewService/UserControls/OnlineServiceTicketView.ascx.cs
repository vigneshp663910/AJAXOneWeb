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
                lblMessageRestore.ForeColor = Color.Red;
                if (string.IsNullOrEmpty(txtRestoreRemarks.Text.Trim()))
                {
                    lblMessageRestore.Text = "Please enter the Remark";
                    return;
                }
                PApiResult Results = new BDMS_ICTicket().UpdateOnlineServiceTicketStatus(Ticket.OnlineServiceTicketID, (short)StatusItem.OnlineServiceTicketStatus_Restored, txtRestoreRemarks.Text.Trim(), null);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessageRestore.Text = Results.Message; 
                    return;
                } 
                MPE_UpdateRestore.Hide();
            }
            else if (lbActions.ID == "btnUpdateEscalatedL1")
            {
                MPE_EscalatedL1.Show(); 
                lblEscalatedL1Message.ForeColor = Color.Red;
                if (string.IsNullOrEmpty(txtEscalatedL1Remark.Text.Trim()))
                {
                    lblEscalatedL1Message.Text = "Please enter the Remark";
                    return;
                }
                PApiResult Results = new BDMS_ICTicket().UpdateOnlineServiceTicketStatus(Ticket.OnlineServiceTicketID, (short)StatusItem.OnlineServiceTicketStatus_EscalatedL1, txtEscalatedL1Remark.Text.Trim(), ddlAjaxEmployee.SelectedValue);
                if (Results.Status == PApplication.Failure)
                {
                    lblEscalatedL1Message.Text = Results.Message;
                    return;
                } 
                MPE_EscalatedL1.Hide();
            }
            else if (lbActions.ID == "btnUpdateEscalatedDeale")
            {
                MPE_EscalatedDealer.Show();
                lblEscalatedDealerMessage.ForeColor = Color.Red;
                if (string.IsNullOrEmpty(lblEscalatedDealerRemarks.Text.Trim()))
                {
                    lblEscalatedDealerMessage.Text = "Please enter the Remark";
                    return;
                }
                PApiResult Results = new BDMS_ICTicket().UpdateOnlineServiceTicketStatus(Ticket.OnlineServiceTicketID, (short)StatusItem.OnlineServiceTicketStatus_EscalatedDealer, lblEscalatedDealerRemarks.Text.Trim(), null);
                if (Results.Status == PApplication.Failure)
                {
                    lblEscalatedDealerMessage.Text = Results.Message; 
                    return;
                }
                MPE_EscalatedDealer.Hide();
            }
            else if (lbActions.ID == "btnUpdateCustomerSatisfactionLevel")
            {
                MPE_CustomerSatisfactionLevelk.Show();
                PApiResult Results = new BDMS_ICTicket().UpdateOnlineServiceTickeCustomerSatisfactionLevel(Ticket.OnlineServiceTicketID, ddlCustomerSatisfactionLevel.SelectedValue);
                if (Results.Status == PApplication.Failure)
                {
                    lblCustomerSatisfactionLevelMessage.Text = Results.Message;
                    lblCustomerSatisfactionLevelMessage.ForeColor = Color.Red;
                    return;
                } 
                MPE_CustomerSatisfactionLevelk.Hide();
            }

            lblMessage.Text = "Updated successfully";
            lblMessage.ForeColor = Color.Green;
            FillOnlineServiceTicket(Ticket.OnlineServiceTicketID);
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
                MPE_EscalatedDealer.Show();
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
            lblDistrict.Text = Ticket.Address.District.District;
            lblLocation.Text = Ticket.Location;
            lblComplaintDescription.Text = Ticket.ComplaintDescription;
            lblStatus.Text = Ticket.Status.StatusItem;
            lblCustomer.Text = Ticket.Customer.CustomerName + " - " + Ticket.Customer.CustomerCode;
            lblContactPerson.Text = Ticket.ContactPerson + " & " + Ticket.ContactNumber;
            cbIsWarranty.Checked = Ticket.IsWarranty;
            lblEquipment.Text = Ticket.Equipment.EquipmentSerialNo;
            lblModel.Text = Ticket.Equipment.EquipmentModel.Model;

            lblSatisfactionLevel.Text = Ticket.CustomerSatisfactionLevel.CustomerSatisfactionLevel;

            lblRestoreBy.Text =  Ticket.RestoredBy.ContactName;
            lblRestoreDate.Text = Convert.ToString(Ticket.RestoreDate);
            lblRestoreRemarks.Text = Ticket.RestoreRemarks;

            lblRegisteredBy.Text = Ticket.RegisteredBy.ContactName;
            lblEscalatedL1.Text = Ticket.EscalatedL1 == null ? "" : Ticket.EscalatedL1.ContactName;
            lblEscalatedL1Date.Text = Convert.ToString(Ticket.EscalatedL1On);
            
            //  lblCallCategory.Text = "";
            lblPriority.Text = Ticket.ICPriority.ServicePriority;
            lblState.Text = Ticket.Address.State.State;

            lblEscalatedL1Remarks.Text = Ticket.EscalatedRemarks;
            lblEscalatedDealerRemarks.Text = Ticket.EscalatedDealerRemarks;

            //  CustomerViewSoldTo.fillCustomer(Ticket.Customer.CustomerID);

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
                if (Ticket.EscalatedL1.UserID != PSession.User.UserID)
                {
                    lbtnRestore.Visible = false; 
                    lbtnEscalatedDealer.Visible = false;
                } 
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