using Business;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketReachedDateChange : System.Web.UI.Page
    {
        public PDMS_ICTicket ICTicket
        {
            get
            {
                if (Session["ICTicket"] == null)
                {
                    Session["ICTicket"] = new PDMS_ICTicket();
                }
                return (PDMS_ICTicket)Session["ICTicket"];
            }
            set
            {
                Session["ICTicket"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » IC Ticket » Reached Date Change');</script>");

            if (!IsPostBack)
            {
                ICTicket = null;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<PDMS_ICTicket> ICTickets = new BDMS_ICTicket().GetICTicket(null, "", txtICTicket.Text.Trim(), null, null, null, null);
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            if (string.IsNullOrEmpty(txtICTicket.Text.Trim()))
            {
                lblMessage.Text = "Please enter IC Ticket";
                return;
            }
            if (ICTickets.Count != 1)
            {
                lblMessage.Text = "Please check the IC Ticket";
                return;
            }
            //  ceRequestedDate.StartDate
            ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTickets[0].ICTicketID);
            //if (ICTicket.ReachedDate != null)
            //{
            //    lblMessage.Text = "Please check the IC Ticket";
            //    return;
            //}
            UC_BasicInformation.SDMS_ICTicket = ICTicket;
            UC_BasicInformation.FillBasicInformation();
            txtRequestedDate.Enabled = true;
            lblMessage.Visible = false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            if (string.IsNullOrEmpty(txtRequestedDate.Text.Trim()))
            {
                lblMessage.Text = "Please enter Requested Date";
                return;
            }

            if (ddlRequestedHH.SelectedValue == "-1")
            {
                lblMessage.Text = "Please select the Requested Hour";
                return;
            }

            if (ddlRequestedMM.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Requested Minute";
                return;
            }
            DateTime RequestedDate = Convert.ToDateTime(txtRequestedDate.Text.Trim() + " " + ddlRequestedHH.SelectedValue + ":" + ddlRequestedMM.SelectedValue);

            if (new BDMS_ICTicket().ChangeICTicketRequestedDate(ICTicket.ICTicketID, RequestedDate, PSession.User.UserID))
            {
                lblMessage.Text = " Requested date and time changed";
                lblMessage.ForeColor = Color.Green;
                txtRequestedDate.Enabled = false;

                ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicket.ICTicketID);
                UC_BasicInformation.SDMS_ICTicket = ICTicket;
                UC_BasicInformation.FillBasicInformation();
               // new SDMS_ICTicket().UpdateICTicketRequestedDateToSAP(ICTicket.ICTicketNumber, RequestedDate);
            }
            else
            {
                lblMessage.Text = " Requested date and time is not changed";
            }

        }
    }
}