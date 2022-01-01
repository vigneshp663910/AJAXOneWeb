using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class ICTicketTechnicianAssign : System.Web.UI.UserControl
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
                    SDMS_Technicians = new BDMS_Service().GetTechniciansByDealerID(SDMS_ICTicket.Dealer.DealerID);
                    FillTechnicians();
                    FillTechniciansByTicketID();
                }
            }
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
        private void FillTechniciansByTicketID()
        {
            SDMS_TechniciansWD = new BDMS_Service().GetTechniciansByTicketID(SDMS_ICTicket.ICTicketID);
            int rowC = SDMS_TechniciansWD.Count;
            if (SDMS_TechniciansWD.Count == 0)
            {
                SDMS_TechniciansWD.Add(new PDMS_ServiceTechnician());
            }
            gvTechnician.DataSource = SDMS_TechniciansWD;
            gvTechnician.DataBind();
            if (rowC == 1)
            {
                gvTechnician.Columns[2].Visible = false;
            }
            else
            {
                gvTechnician.Columns[2].Visible = true;
            }


        }
        private void FillTechniciansByDealerID()
        {
            //SDMS_Technicians = new BDMS_Service().GetTechniciansByDealerID(SDMS_ICTicket.Dealer.DealerID);
            //ddlTechnician.DataTextField = "ContactName";
            //ddlTechnician.DataValueField = "UserID";
            //ddlTechnician.DataSource = SDMS_Technicians;
            //ddlTechnician.DataBind();
            //ddlTechnician.Items.Insert(0, new ListItem("Select", "0"));

        }

        protected void gvTechnician_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbTechnicianRemove = (LinkButton)e.Row.FindControl("lbTechnicianRemove");
                Label lblUserID = (Label)e.Row.FindControl("lblUserID");
                //PDMS_ServiceTechnician WorkedDate = new PDMS_ServiceTechnician();
                //WorkedDate = SDMS_TechniciansWD.Find(s => s.UserID == Convert.ToInt32(lblUserID.Text));

                //if (WorkedDate.ServiceTechnicianWorkedDate.Count != 0)
                //{
                //    lbTechnicianRemove.Visible = false;
                //}
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
            gvddlTechnician.Focus();
        }

        protected void lbTechnicianRemove_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Label lblUserIDR = (Label)gvRow.FindControl("lblUserID");
            new BDMS_ICTicket().InsertOrUpdateTechnicianAddOrRemoveICTicket(SDMS_ICTicket.ICTicketID, Convert.ToInt32(lblUserIDR.Text), true, PSession.User.UserID);
            lblMessage.Text = "Technician Removed";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            FillTechniciansByDealerID();
            FillTechniciansByTicketID();

            DropDownList gvddlTechnician = (DropDownList)gvTechnician.FooterRow.FindControl("gvddlTechnician");
            gvddlTechnician.Focus();
        }

    }
}