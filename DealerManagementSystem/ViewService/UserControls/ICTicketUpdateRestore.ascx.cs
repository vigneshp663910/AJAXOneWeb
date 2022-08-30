using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService.UserControls
{
    public partial class ICTicketUpdateRestore : System.Web.UI.UserControl
    {
        public PDMS_ICTicket SDMS_ICTicket
        {
            get
            {
                if (Session["SDMS_ICTicket"] == null)
                {
                    Session["SDMS_ICTicket"] = new PDMS_ICTicket();
                }
                return (PDMS_ICTicket)Session["SDMS_ICTicket"];
            }
            set
            {
                Session["SDMS_ICTicket"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster(PDMS_ICTicket SDMS_ICTicket, PDMS_ICTicketFSR ICTicketFSR)
        {
            EnableOrDesableBasedOnServiceCharges();
            FillCustomerSatisfactionLevel();

            txtRestoreDate.Text = SDMS_ICTicket.RestoreDate == null ? "" : ((DateTime)SDMS_ICTicket.RestoreDate).ToShortDateString();
            ddlRestoreHH.SelectedValue = SDMS_ICTicket.RestoreDate == null ? "-1" : ((DateTime)SDMS_ICTicket.RestoreDate).Hour.ToString();
            if (SDMS_ICTicket.RestoreDate != null)
            {
                int RestoreMMMinute = ((DateTime)SDMS_ICTicket.RestoreDate).Minute;
                int adjustment = RestoreMMMinute % 5;
                if (adjustment != 0)
                {
                    RestoreMMMinute = (RestoreMMMinute - adjustment) + 5;
                }
                ddlRestoreMM.SelectedValue = RestoreMMMinute.ToString().PadLeft(2, '0');
            }
            else
            {
                ddlRestoreMM.SelectedValue = "0";
            }
            txtArrivalBackDate.Text = SDMS_ICTicket.ArrivalBack == null ? "" : ((DateTime)SDMS_ICTicket.ArrivalBack).ToShortDateString();
            ddlArrivalBackHH.SelectedValue = SDMS_ICTicket.ArrivalBack == null ? "-1" : ((DateTime)SDMS_ICTicket.ArrivalBack).Hour.ToString();
            if (SDMS_ICTicket.ArrivalBack != null)
            {
                int ArrivalBackMMMinute = ((DateTime)SDMS_ICTicket.ArrivalBack).Minute;
                int adjustment = ArrivalBackMMMinute % 5;
                if (adjustment != 0)
                {
                    ArrivalBackMMMinute = (ArrivalBackMMMinute - adjustment) + 5;
                }
                ddlArrivalBackMM.SelectedValue = ArrivalBackMMMinute.ToString().PadLeft(2, '0');
            }
            else
            {
                ddlArrivalBackMM.SelectedValue = "0";
            }

            if (SDMS_ICTicket.CustomerSatisfactionLevel != null)
                ddlCustomerSatisfactionLevel.SelectedValue = SDMS_ICTicket.CustomerSatisfactionLevel.CustomerSatisfactionLevelID.ToString();

            txtCustomerRemarks.Text = ICTicketFSR.CustomerRemarks;

            if (SDMS_ICTicket.CustomerSatisfactionLevel != null)
                ddlComplaintStatus.SelectedValue = ICTicketFSR.ComplaintStatus;
        }
        public void EnableOrDesableBasedOnServiceCharges()
        {
            if (SDMS_ICTicket.ServiceCharges.Count != 0)
            {
                txtRestoreDate.Enabled = true;
                ddlRestoreHH.Enabled = true;
                ddlRestoreMM.Enabled = true;
            }
            else
            {
                txtRestoreDate.Enabled = false;
                ddlRestoreHH.Enabled = false;
                ddlRestoreMM.Enabled = false;
            }
        }
        void Clear()
        {


        }
        public string Read()
        {  

            DateTime? RestoreDate = string.IsNullOrEmpty(txtRestoreDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRestoreDate.Text.Trim() + " " + ddlRestoreHH.SelectedValue + ":" + ddlRestoreMM.SelectedValue);
            DateTime? ArrivalBack = string.IsNullOrEmpty(txtArrivalBackDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtArrivalBackDate.Text.Trim() + " " + ddlArrivalBackHH.SelectedValue + ":" + ddlArrivalBackMM.SelectedValue);

           
            return "&RestoreDate=" + RestoreDate + "&ArrivalBack=" + ArrivalBack + "&CustomerSatisfactionLevelID=" + ddlCustomerSatisfactionLevel.SelectedValue
                + "&CustomerRemarks=" + txtCustomerRemarks.Text.Trim() + "&ComplaintStatus=" + ddlComplaintStatus.SelectedValue; 
        }
        public string Validation(PDMS_ICTicket SDMS_ICTicket)
        {
            string Message = "";
            DateTime? RestoreDate = string.IsNullOrEmpty(txtRestoreDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRestoreDate.Text.Trim() + " " + ddlRestoreHH.SelectedValue + ":" + ddlRestoreMM.SelectedValue);

            if (SDMS_ICTicket.ReachedDate > RestoreDate)
            {
                
                return "Restore date should not be less than Reached date.";
                
            }
            return Message;
        }

        protected void ddlComplaintStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlComplaintStatus.SelectedValue == "Close")
            {
                txtRestoreDate.Enabled = true;
                ddlRestoreHH.Enabled = true;
                ddlRestoreMM.Enabled = true;
            }
            else
            {
                txtRestoreDate.Enabled = false;
                ddlRestoreHH.Enabled = false;
                ddlRestoreMM.Enabled = false;
                txtRestoreDate.Text = "";
            }
        }
        private void FillCustomerSatisfactionLevel()
        {
            ddlCustomerSatisfactionLevel.DataTextField = "CustomerSatisfactionLevel";
            ddlCustomerSatisfactionLevel.DataValueField = "CustomerSatisfactionLevelID";
            ddlCustomerSatisfactionLevel.DataSource = new BDMS_Service().GetCustomerSatisfactionLevel(null, null);
            ddlCustomerSatisfactionLevel.DataBind();
            ddlCustomerSatisfactionLevel.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}