using AjaxControlToolkit;
using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketTechnicianAssign : System.Web.UI.Page
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
            get
            {
                if (Session["DMS_ICTicket"] == null)
                {
                    Session["DMS_ICTicket"] = new PDMS_ICTicket();
                }
                return (PDMS_ICTicket)Session["DMS_ICTicket"];
            }
            set
            {
                Session["DMS_ICTicket"] = value;
            }
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
                if (!string.IsNullOrEmpty(Request.QueryString["TicketID"]))
                {

                    long ICTicketID = Convert.ToInt64(Request.QueryString["TicketID"]);
                    SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);
                    ICTicketButton.SDMS_ICTicket = SDMS_ICTicket;
                    ICTicketButton.PageName = "DMS_ICTicketTechnicianAssign";
                    //UC_BasicInformation.SDMS_ICTicket = SDMS_ICTicket; 

                    FillBasicInformation();
                    FillTechnicians();


                    FillTechniciansByTicketID();

                    if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                    {

                    }
                    else
                    {

                    }
                }
            }
        }
        private void FillBasicInformation()
        {
            lblICTicket.Text = SDMS_ICTicket.ICTicketNumber + " - " + SDMS_ICTicket.ICTicketDate.ToShortDateString();
            lblCustomer.Text = SDMS_ICTicket.Customer.CustomerCode + " - " + SDMS_ICTicket.Customer.CustomerName;
            lblStatus.Text = SDMS_ICTicket.ServiceStatus.ServiceStatus;
            lblRequestedDate.Text = SDMS_ICTicket.RequestedDate == null ? "" : ((DateTime)SDMS_ICTicket.RequestedDate).ToShortDateString();
            cbIsWarranty.Checked = SDMS_ICTicket.IsWarranty;
            lblWarrantyExpiry.Text = ((DateTime)SDMS_ICTicket.Equipment.WarrantyExpiryDate).ToShortDateString();
            lblDistrict.Text = SDMS_ICTicket.Address.District.District + " - " + SDMS_ICTicket.Address.State.State;
            lblContactPerson.Text = SDMS_ICTicket.ContactPerson;
            lblPresentContactNumber.Text = SDMS_ICTicket.PresentContactNumber;
            lblComplaintDescription.Text = SDMS_ICTicket.ComplaintDescription;
            lblInformation.Text = SDMS_ICTicket.Information;
            lblOldICTicketNumber.Text = SDMS_ICTicket.OldICTicketNumber;
            lblTechnician.Text = SDMS_ICTicket.Technician.ContactName;

            lblLastHMRValue.Text = Convert.ToString(SDMS_ICTicket.LastHMRValue);
            lblLastHMRDate.Text = SDMS_ICTicket.LastHMRDate == null ? "" : ((DateTime)SDMS_ICTicket.LastHMRDate).ToShortDateString();
            lblNewHMRValue.Text = Convert.ToString(SDMS_ICTicket.CurrentHMRValue);
            lblNewHMRDate.Text = SDMS_ICTicket.CurrentHMRDate == null ? "" : ((DateTime)SDMS_ICTicket.CurrentHMRDate).ToShortDateString();
        }

        private void FillTechnicians()
        {
            FillTechniciansByDealerID();
            if (SDMS_ICTicket.Technician.UserID == 0)
            {
                btnSave.Visible = true;
                ddlTechnician.Visible = true;
                lblTechnician.Visible = false;
                FillTechniciansByDealerID();
            }
            else
            {
                //  
                //  ddlTechnician.SelectedValue = SDMS_ICTicket.Technician.UserID.ToString();
                lblTechnician.Text = SDMS_ICTicket.Technician.ContactName;
                btnSave.Visible = false;
                ddlTechnician.Visible = false;
                lblTechnician.Visible = true;
            }
            if (SDMS_ICTicket.ReachedDate == null)
                gvTechnicianWorkDays.Visible = false;
        }



        private void FillTechniciansByTicketID()
        {
            SDMS_TechniciansWD = new BDMS_Service().GetTechniciansByTicketID(SDMS_ICTicket.ICTicketID);
            gvTechnician.DataSource = SDMS_TechniciansWD;
            gvTechnician.DataBind();
            if (gvTechnician.Rows.Count == 1)
            {
                gvTechnician.Columns[2].Visible = false;
            }
            else
            {
                gvTechnician.Columns[2].Visible = true;
            }

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
        private void FillTechniciansByDealerID()
        {
            SDMS_Technicians = new BDMS_Service().GetTechniciansByDealerID(SDMS_ICTicket.Dealer.DealerID);
            ddlTechnician.DataTextField = "ContactName";
            ddlTechnician.DataValueField = "UserID";
            ddlTechnician.DataSource = SDMS_Technicians;
            ddlTechnician.DataBind();
            ddlTechnician.Items.Insert(0, new ListItem("Select", "0"));

        }


        protected void gvTechnician_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbTechnicianRemove = (LinkButton)e.Row.FindControl("lbTechnicianRemove");
                Label lblUserID = (Label)e.Row.FindControl("lblUserID");
                // SDMS_Technicians.RemoveAll(m => m.UserID == Convert.ToInt32(lblUserID.Text));
                PDMS_ServiceTechnician WorkedDate = new PDMS_ServiceTechnician();
                WorkedDate = SDMS_TechniciansWD.Find(s => s.UserID == Convert.ToInt32(lblUserID.Text));

                if (WorkedDate.ServiceTechnicianWorkedDate.Count != 0)
                {
                    //  LinkButton lbTechnicianRemove = (LinkButton)e.Row.FindControl("lbTechnicianRemove");
                    lbTechnicianRemove.Visible = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList gvddlTechnician = (DropDownList)e.Row.FindControl("gvddlTechnician");
                gvddlTechnician.DataTextField = "ContactName";
                gvddlTechnician.DataValueField = "UserID";
                gvddlTechnician.DataSource = SDMS_Technicians;
                gvddlTechnician.DataBind();
                gvddlTechnician.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        protected void lbTechnicianAdd_Click(object sender, EventArgs e)
        {

            //List<PUser> Tech = new List<PUser>();
            //for (int i = 0; i < gvTechnician.Rows.Count; i++)
            //{
            //    Label lblUserName = (Label)gvTechnician.Rows[i].FindControl("lblUserName");
            //    Label lblContactName = (Label)gvTechnician.Rows[i].FindControl("lblContactName");
            //    Label lblUserID = (Label)gvTechnician.Rows[i].FindControl("lblUserID");
            //    Tech.Add(new PUser() { UserID = Convert.ToInt32(lblUserID.Text), UserName = lblUserName.Text, ContactName = lblContactName.Text });
            //}
            //DropDownList gvddlTechnician = (DropDownList)gvTechnician.FooterRow.FindControl("gvddlTechnician");
            //var t = SDMS_Technicians.Where(m => m.UserID == Convert.ToInt32(gvddlTechnician.SelectedValue)).ToList();
            //Tech.Add(new PUser() { UserID = t[0].UserID, UserName = t[0].UserName, ContactName = t[0].ContactName });
            //SDMS_Technicians.RemoveAll(m => m.UserID == Convert.ToInt32(gvddlTechnician.SelectedValue));
            //gvTechnician.DataSource = Tech;
            //gvTechnician.DataBind();

            DropDownList gvddlTechnician = (DropDownList)gvTechnician.FooterRow.FindControl("gvddlTechnician");
            if (gvddlTechnician.SelectedValue == "0")
            {
                return;
            }
            new BDMS_ICTicket().InsertOrUpdateTechnicianAddOrRemoveICTicket(SDMS_ICTicket.ICTicketID, Convert.ToInt32(gvddlTechnician.SelectedValue), false, PSession.User.UserID);
            lblMessage.Text = "New Technician Assigned";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            FillTechniciansByDealerID();
            FillTechniciansByTicketID();

        }

        protected void lbTechnicianRemove_Click(object sender, EventArgs e)
        {
            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //int index = gvRow.RowIndex;
            //List<PDMS_ServiceTechnician> Tech = new List<PDMS_ServiceTechnician>();
            //for (int i = 0; i < gvTechnician.Rows.Count; i++)
            //{
            //    Label lblUserName = (Label)gvTechnician.Rows[i].FindControl("lblUserName");
            //    Label lblContactName = (Label)gvTechnician.Rows[i].FindControl("lblContactName");
            //    Label lblUserID = (Label)gvTechnician.Rows[i].FindControl("lblUserID");
            //    Tech.Add(new PDMS_ServiceTechnician() { UserID = Convert.ToInt32(lblUserID.Text), UserName = lblUserName.Text, ContactName = lblContactName.Text });
            //}
            //Label lblUserIDR = (Label)gvRow.FindControl("lblUserID");
            //var t = Tech.Where(m => m.UserID == Convert.ToInt32(lblUserIDR.Text)).ToList();

            //SDMS_Technicians.Add(new PDMS_ServiceTechnician() { UserID = t[0].UserID, UserName = t[0].UserName, ContactName = t[0].ContactName });
            //Tech.RemoveAll(m => m.UserID == Convert.ToInt32(lblUserIDR.Text));
            //gvTechnician.DataSource = Tech;
            //gvTechnician.DataBind();

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Label lblUserIDR = (Label)gvRow.FindControl("lblUserID");
            new BDMS_ICTicket().InsertOrUpdateTechnicianAddOrRemoveICTicket(SDMS_ICTicket.ICTicketID, Convert.ToInt32(lblUserIDR.Text), true, PSession.User.UserID);
            lblMessage.Text = "Technician Removed";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            FillTechniciansByDealerID();
            FillTechniciansByTicketID();
            FillBasicInformation();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlTechnician.SelectedValue == "0")
            {
                return;
            }
            new BDMS_ICTicket().InsertOrUpdateTechnicianForICTicket(SDMS_ICTicket.ICTicketID, Convert.ToInt32(ddlTechnician.SelectedValue), PSession.User.UserID);
            lblMessage.Text = "Technician Assigned";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(SDMS_ICTicket.ICTicketID);
            FillBasicInformation();

            FillTechniciansByDealerID();
            FillTechniciansByTicketID();
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
            FillTechniciansByDealerID();
            FillTechniciansByTicketID();
            FillBasicInformation();
        }

        protected void lbWorkedDayAdd_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            DropDownList gvddlTechnician = (DropDownList)gvTechnicianWorkDays.FooterRow.FindControl("gvddlTechnician");
            TextBox txtWorkedDate = (TextBox)gvTechnicianWorkDays.FooterRow.FindControl("txtWorkedDate");
            TextBox txtWorkedHours = (TextBox)gvTechnicianWorkDays.FooterRow.FindControl("txtWorkedHours");
            if (gvddlTechnician.SelectedValue == "0")
            {
                lblMessage.Text = "Technician  Worked Date Added";
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
            for (int i = 0; i < gvTechnicianWorkDays.Rows.Count; i++)
            {
                Label lblUserID = (Label)gvTechnicianWorkDays.Rows[i].FindControl("lblUserID");
                Label lblWorkedHours = (Label)gvTechnicianWorkDays.Rows[i].FindControl("lblWorkedHours");

                if ((lblUserID.Text == gvddlTechnician.SelectedValue) && (lblWorkedHours.Text == txtWorkedDate.Text))
                {
                    lblMessage.Text = "Already this date add in this ticket. Please check it";
                    return;
                }
            }
            //   PDMS_ServiceTechnician WorkedDate = new PDMS_ServiceTechnician();
            //   WorkedDate = SDMS_TechniciansWD.Find(s => s.UserID == Convert.ToInt32(gvddlTechnician.SelectedValue));


            new BDMS_ICTicket().InsertOrUpdateTechnicianWorkedDateAddOrRemoveICTicket(0, SDMS_ICTicket.ICTicketID, Convert.ToInt32(gvddlTechnician.SelectedValue), Convert.ToDateTime(txtWorkedDate.Text), Convert.ToDecimal(txtWorkedHours.Text), false, PSession.User.UserID);
            lblMessage.Text = "Technician  Worked Date Added";

            lblMessage.ForeColor = Color.Green;
            SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(SDMS_ICTicket.ICTicketID);
            FillBasicInformation();

            FillTechniciansByDealerID();
            FillTechniciansByTicketID();
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
            if (gvddlTechnician.SelectedValue == "0")
            {
                return;
            }
            txtWorkedDate.Text = "";
            //  PDMS_ServiceTechnician WorkedDate = new PDMS_ServiceTechnician();
            //  WorkedDate = SDMS_TechniciansWD.Find(s => s.UserID == Convert.ToInt32(gvddlTechnician.SelectedValue));

            CalendarExtender ceWorkedDate = (CalendarExtender)gvTechnicianWorkDays.FooterRow.FindControl("ceWorkedDate");
            if (SDMS_ICTicket.ReachedDate != null)
                ceWorkedDate.StartDate = SDMS_ICTicket.ReachedDate;
            if (SDMS_ICTicket.RestoreDate != null)
                ceWorkedDate.EndDate = SDMS_ICTicket.RestoreDate;
        }
    }
}