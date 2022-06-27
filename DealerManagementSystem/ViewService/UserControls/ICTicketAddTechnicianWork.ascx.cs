using AjaxControlToolkit;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService.UserControls
{
    public partial class ICTicketAddTechnicianWork : System.Web.UI.UserControl
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
        public void FillMaster(List<PDMS_ServiceTechnician> SDMS_TechniciansWD)
        { 
            ddlTechnician.DataTextField = "ContactName";
            ddlTechnician.DataValueField = "UserID";
            ddlTechnician.DataSource = SDMS_TechniciansWD;
            ddlTechnician.DataBind();
            ddlTechnician.Items.Insert(0, new ListItem("Select", "0"));
        } 
        void Clear()
        {


        }
        public string Read()
        {   
            return "&TechnicianID=" + ddlTechnician.SelectedValue+ "&WorkedDay="+ txtWorkedDate.Text + "&WorkedHours=" + txtWorkedHours.Text;
        }
        public string Validation(List<PDMS_ServiceTechnician> SDMS_TechniciansWD)
        {
            string Message = "";

            foreach (PDMS_ServiceTechnician t in SDMS_TechniciansWD)
            {
                foreach (PDMS_ServiceTechnicianWorkedDate W in t.ServiceTechnicianWorkedDate)
                {
                    if ((t.UserID.ToString() == ddlTechnician.SelectedValue) && (W.WorkedDate.ToString() == txtWorkedDate.Text))
                    {
                        return "Already this date added for this ticket. Please check it";
                    }
                }
            }
            
            if (ddlTechnician.SelectedValue == "0")
            {
                return "Please select the Technician";
            }
            if (string.IsNullOrEmpty(txtWorkedDate.Text))
            {
                return "Please select the Worked Date";
            }
            if (string.IsNullOrEmpty(txtWorkedHours.Text))
            {
                return "Please enter the Worked Hours";
            }
            if (Convert.ToDecimal(txtWorkedHours.Text) > 24)
            {
                return "Worked hours should not be greater than 24 hours";
            }
            return Message;
        }

        protected void ddlTechnician_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlTechnician.SelectedValue == "0")
            {
                return;
            }
            txtWorkedDate.Text = "";

            if (SDMS_ICTicket.ReachedDate != null)
                ceWorkedDate.StartDate = SDMS_ICTicket.ReachedDate;
            if (SDMS_ICTicket.RestoreDate != null)
                ceWorkedDate.EndDate = SDMS_ICTicket.RestoreDate;
        }
    }
}