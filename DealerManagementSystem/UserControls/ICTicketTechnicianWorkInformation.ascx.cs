using AjaxControlToolkit;
using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class ICTicketTechnicianWorkInformation : System.Web.UI.UserControl
    {
        public List<PDMS_ServiceTechnician> SDMS_Technicians
        {
            get
            {
                if (Session["DMS_ICTicketTechnicianAssign"] == null)
                {
                    Session["DMS_ICTicketTechnicianAssign"] = new List<PDMS_ServiceTechnician>();
                }
                return (List<PDMS_ServiceTechnician>)Session["DMS_ICTicketTechnicianAssign"];
            }
            set
            {
                Session["DMS_ICTicketTechnicianAssign"] = value;
            }
        }
        public PDMS_ICTicket SDMS_ICTicket
        {
            //get
            //{
            //    if (Session["DMS_ICTicket"] == null)
            //    {
            //        Session["DMS_ICTicket"] = new PDMS_ICTicket();
            //    }
            //    return (PDMS_ICTicket)Session["DMS_ICTicket"];
            //}
            //set
            //{
            //    Session["DMS_ICTicket"] = value;
            //}
            get;
            set;
        }
        public List<PDMS_ServiceTechnician> SDMS_TechniciansWD
        {
            get
            {
                if (Session["DMS_ICTicketTechnicianAssignWD"] == null)
                {
                    Session["DMS_ICTicketTechnicianAssignWD"] = new List<PDMS_ServiceTechnician>();
                }
                return (List<PDMS_ServiceTechnician>)Session["DMS_ICTicketTechnicianAssignWD"];
            }
            set
            {
                Session["DMS_ICTicketTechnicianAssignWD"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                //if (!string.IsNullOrEmpty(Request.QueryString["TicketID"]))
                //{
                //    long ICTicketID = Convert.ToInt64(Request.QueryString["TicketID"]);
                // SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);
                SDMS_Technicians = new BDMS_Service().GetTechniciansByDealerID(SDMS_ICTicket.Dealer.DealerID);
                FillTechnicians();
                FillTechniciansByTicketID();
                //}
            }
        }
        private void FillTechniciansByTicketID()
        {
            SDMS_TechniciansWD = new BDMS_Service().GetTechniciansByTicketID(SDMS_ICTicket.ICTicketID);


            List<PDMS_ServiceTechnicianWorkedDate> WorkedDate = new List<PDMS_ServiceTechnicianWorkedDate>();
            foreach (PDMS_ServiceTechnician t in SDMS_TechniciansWD)
            {
                foreach (PDMS_ServiceTechnicianWorkedDate W in t.ServiceTechnicianWorkedDate)
                {
                    WorkedDate.Add(W);
                }
            }
            if (WorkedDate.Count == 0)
            {
                PDMS_ServiceTechnicianWorkedDate c = new PDMS_ServiceTechnicianWorkedDate();
                WorkedDate.Add(c);
            }

            gvTechnicianWorkDays.DataSource = WorkedDate;
            gvTechnicianWorkDays.DataBind();
        }
        private void FillTechnicians()
        {
            //FillTechniciansByDealerID();
            //if (SDMS_ICTicket.Technician.UserID == 0)
            //{
            //    btnSave.Visible = true;
            //    ddlTechnician.Visible = true;
            //    lblTechnician.Visible = false;
            //    FillTechniciansByDealerID();
            //}
            //else
            //{
            //    //  
            //    //  ddlTechnician.SelectedValue = SDMS_ICTicket.Technician.UserID.ToString();
            //    lblTechnician.Text = SDMS_ICTicket.Technician.ContactName;
            //    btnSave.Visible = false;
            //    ddlTechnician.Visible = false;
            //    lblTechnician.Visible = true;
            //}
            //if (SDMS_ICTicket.ReachedDate == null)
            //    gvTechnicianWorkDays.Visible = false;
        }
        protected void lbWorkedDayAdd_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            DropDownList gvddlTechnician = (DropDownList)gvTechnicianWorkDays.FooterRow.FindControl("gvddlTechnician");
            TextBox txtWorkedDate = (TextBox)gvTechnicianWorkDays.FooterRow.FindControl("txtWorkedDate");
            TextBox txtWorkedHours = (TextBox)gvTechnicianWorkDays.FooterRow.FindControl("txtWorkedHours");

            //   gvddlTechnician.Focus();
            if (gvddlTechnician.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Technician";
                return;
            }
            if (string.IsNullOrEmpty(txtWorkedDate.Text))
            {
                lblMessage.Text = "Please select the Worked Date";
                return;
            }
            if (string.IsNullOrEmpty(txtWorkedHours.Text))
            {
                lblMessage.Text = "Please enter the Worked Hours";
                return;
            }

            if (Convert.ToDecimal(txtWorkedHours.Text) > 24)
            {
                lblMessage.Text = "Worked hours should not be greater than 24 hours";
                return;
            }
            for (int i = 0; i < gvTechnicianWorkDays.Rows.Count; i++)
            {
                Label lblUserID = (Label)gvTechnicianWorkDays.Rows[i].FindControl("lblUserID");
                Label lblWorkedDay = (Label)gvTechnicianWorkDays.Rows[i].FindControl("lblWorkedDay");

                if ((lblUserID.Text == gvddlTechnician.SelectedValue) && (lblWorkedDay.Text == txtWorkedDate.Text))
                {
                    lblMessage.Text = "Already this date added for this ticket. Please check it";
                    return;
                }
            }

            if (new BDMS_ICTicket().InsertOrUpdateTechnicianWorkedDateAddOrRemoveICTicket(0, SDMS_ICTicket.ICTicketID, Convert.ToInt32(gvddlTechnician.SelectedValue), Convert.ToDateTime(txtWorkedDate.Text), Convert.ToDecimal(txtWorkedHours.Text), false, PSession.User.UserID))
            {
                lblMessage.Text = "Technician  Worked Date Added for this ticket";
                lblMessage.ForeColor = Color.Green;
                SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(SDMS_ICTicket.ICTicketID);

                FillTechniciansByTicketID();
            }
            else
            {
                lblMessage.Text = "Technician Worked is not Date Added";

            }
        }
        protected void lbWorkedDayRemove_Click(object sender, EventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            long ServiceTechnicianWorkDateID = Convert.ToInt64(((Label)gvRow.FindControl("lblServiceTechnicianWorkDateID")).Text);
            Label lblWorkedHours = (Label)gvRow.FindControl("lblWorkedHours");
            new BDMS_ICTicket().InsertOrUpdateTechnicianWorkedDateAddOrRemoveICTicket(ServiceTechnicianWorkDateID, SDMS_ICTicket.ICTicketID, null, null, Convert.ToDecimal(lblWorkedHours.Text), true, PSession.User.UserID);
            lblMessage.Text = "Technician Worked Date Removed";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            FillTechniciansByTicketID();
            DropDownList gvddlTechnician = (DropDownList)gvTechnicianWorkDays.FooterRow.FindControl("gvddlTechnician");
            //  gvddlTechnician.Focus();
        }
        protected void gvTechnicianWorkDays_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList gvddlTechnician = (DropDownList)e.Row.FindControl("gvddlTechnician");
                gvddlTechnician.DataTextField = "ContactName";
                gvddlTechnician.DataValueField = "UserID";
                gvddlTechnician.DataSource = SDMS_TechniciansWD;
                gvddlTechnician.DataBind();
                gvddlTechnician.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        protected void gvddlTechnician_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList gvddlTechnician = (DropDownList)gvTechnicianWorkDays.FooterRow.FindControl("gvddlTechnician");
            TextBox txtWorkedDate = (TextBox)gvTechnicianWorkDays.FooterRow.FindControl("txtWorkedDate");
            //  gvddlTechnician.Focus();
            if (gvddlTechnician.SelectedValue == "0")
            {
                return;
            }
            txtWorkedDate.Text = "";

            CalendarExtender ceWorkedDate = (CalendarExtender)gvTechnicianWorkDays.FooterRow.FindControl("ceWorkedDate");
            if (SDMS_ICTicket.ReachedDate != null)
                ceWorkedDate.StartDate = SDMS_ICTicket.ReachedDate;
            if (SDMS_ICTicket.RestoreDate != null)
                ceWorkedDate.EndDate = SDMS_ICTicket.RestoreDate;

        }
    }
}