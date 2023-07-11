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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster()
        {  
            FillCustomerSatisfactionLevel(); 
        } 
        public string Read()
        {  

          //  DateTime? RestoreDate = string.IsNullOrEmpty(txtRestoreDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRestoreDate.Text.Trim() + " " + ddlRestoreHH.SelectedValue + ":" + ddlRestoreMM.SelectedValue);
            

            // return "&RestoreDate=" + RestoreDate + "&ArrivalBack=" + ArrivalBack + "&ComplaintStatus=" + ddlComplaintStatus.SelectedValue; 
            return "&ComplaintStatus=Close"
                + "&CustomerSatisfactionLevelID=" + ddlCustomerSatisfactionLevel.SelectedValue + "&CustomerRemarks=" + txtCustomerRemarks.Text.Trim();
        }
        public string Validation(PDMS_ICTicket SDMS_ICTicket)
        {
            string Message = ""; 
            if (ddlCustomerSatisfactionLevel.SelectedValue =="0")
            {
                return "Please enter the Customer Satisfaction Level";
            }
            if (string.IsNullOrEmpty(txtCustomerRemarks.Text))
            {
                return "Please enter the Customer Remarks";
            } 
            return Message;
        }

        protected void ddlComplaintStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlComplaintStatus.SelectedValue == "Close")
            //{
            //    txtRestoreDate.Enabled = true;
            //    ddlRestoreHH.Enabled = true;
            //    ddlRestoreMM.Enabled = true;
            //}
            //else
            //{
            //    txtRestoreDate.Enabled = false;
            //    ddlRestoreHH.Enabled = false;
            //    ddlRestoreMM.Enabled = false;
            //    txtRestoreDate.Text = "";
            //}
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