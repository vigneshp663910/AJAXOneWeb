using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketMarginWarrantyChange : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ICTicketMarginWarrantyChange; } }
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » IC Ticket » Margin Warranty Change');</script>");

            if (!IsPostBack)
            {
                ICTicket = null;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            if (string.IsNullOrEmpty(txtICTicket.Text.Trim()))
            {
                lblMessage.Text = "Please Enter IC Ticket";
                return;
            }
            List<PDMS_ICTicket> ICTickets = new BDMS_ICTicket().GetICTicket(null, "", txtICTicket.Text.Trim(), null, null, null, null);
            if (ICTickets.Count != 1)
            {
                lblMessage.Text = "Please Check The IC Ticket";
                return;
            }
            //  ceRequestedDate.StartDate
            ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTickets[0].ICTicketID);
            if (ICTicket.CurrentHMRValue == null)
            {
                lblMessage.Text = "Please Fill The HMR Value ";
                return;
            }
            //  if (ICTicket.Equipment.WarrantyExpiryDate < DateTime.Now.AddDays(-1).AddDays(-30))
            if (ICTicket.Equipment.WarrantyExpiryDate < ICTicket.ICTicketDate.AddDays(-1).AddDays(-30))
            {
                lblMessage.Text = "Please Check Warranty Expiry Date";
                return;
            }
            if (ICTicket.Equipment.EquipmentModel.Division.UOM != "Cum")
            {
                if (ICTicket.CurrentHMRValue > 2100)
                {
                    lblMessage.Text = "HMR Value Crossed 2100";
                    return;
                }
            }
            //if (ICTicket.ReachedDate != null)
            //{
            //    lblMessage.Text = "Please check the IC Ticket";
            //    return;
            //}
            UC_BasicInformation.SDMS_ICTicket = ICTicket;
            UC_BasicInformation.FillBasicInformation();

            cbIsMarginWarranty.Enabled = true;
            lblMessage.Visible = false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            if (cbIsMarginWarranty.Checked == ICTicket.IsMarginWarranty)
            {
                lblMessage.Text = "Please change Margin Warranty ";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (new BDMS_ICTicket().UpdateICTicketMarginWarranty(ICTicket.ICTicketID, cbIsMarginWarranty.Checked, txtMarginRemark.Text.Trim(), PSession.User.UserID))
            {
                lblMessage.Text = "Margin Warranty Changed";
                lblMessage.ForeColor = Color.Green;
                cbIsMarginWarranty.Enabled = false;

                ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicket.ICTicketID);
                UC_BasicInformation.SDMS_ICTicket = ICTicket;
                UC_BasicInformation.FillBasicInformation();
            }
            else
            {
                lblMessage.Text = "Margin Warranty is not Changed";
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}