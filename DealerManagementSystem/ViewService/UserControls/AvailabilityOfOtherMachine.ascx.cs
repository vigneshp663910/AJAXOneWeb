using Business;
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
    public partial class AvailabilityOfOtherMachine : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            lblMessage.Text = "";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                //    SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);
                //   FillAvailabilityOfOtherMachine();
            }
        }

        protected void gvNotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlTypeOfMachine = (DropDownList)e.Row.FindControl("ddlTypeOfMachine");
                new BDMS_AvailabilityOfOtherMachine().GetTypeOfMachine(ddlTypeOfMachine, null, null);
                DropDownList ddlMake = (DropDownList)e.Row.FindControl("ddlMake");
                new BDMS_AvailabilityOfOtherMachine().GetMake(ddlMake, null, null);
            }
        }
        protected void lblNoteAdd_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            DropDownList ddlTypeOfMachine = (DropDownList)gvAvailabilityOfOtherMachine.FooterRow.FindControl("ddlTypeOfMachine");
            DropDownList ddlMake = (DropDownList)gvAvailabilityOfOtherMachine.FooterRow.FindControl("ddlMake");
            //  ddlTypeOfMachine.Focus();
            lblFocus.Focus();
            TextBox txtQuantity = (TextBox)gvAvailabilityOfOtherMachine.FooterRow.FindControl("txtQuantity");
            if (ddlTypeOfMachine.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Note Type";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (ddlMake.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Make";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                lblMessage.Text = "Please enter the Quantity";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            else
            {

            }
            long ICTicketID = (long)ViewState["ICTicketID"];
            int TypeOfMachineID = Convert.ToInt32(ddlTypeOfMachine.SelectedValue);
            int Quantity = Convert.ToInt32(txtQuantity.Text.Trim());
            int MakeID = Convert.ToInt32(ddlMake.SelectedValue);
            if (new BDMS_AvailabilityOfOtherMachine().InsertOrUpdateAvailabilityOfOtherMachineAddOrRemoveICTicket(0, ICTicketID, TypeOfMachineID, Quantity, MakeID, false, PSession.User.UserID))
            {
                lblMessage.Text = "New line is added for this FSR";
                lblMessage.ForeColor = Color.Green;
                FillAvailabilityOfOtherMachine(ICTicketID);

            }
            else
            {
                lblMessage.Text = "New line is not added for this FSR";
                lblMessage.ForeColor = Color.Red;
            }
            //ddlTypeOfMachine.Focus();
            lblFocus.Focus();
        }
        protected void lblNoteRemove_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long AvailabilityOfOtherMachineID = Convert.ToInt64(gvAvailabilityOfOtherMachine.DataKeys[gvRow.RowIndex].Value);
            new BDMS_AvailabilityOfOtherMachine().InsertOrUpdateAvailabilityOfOtherMachineAddOrRemoveICTicket(AvailabilityOfOtherMachineID, 0, 0, 0, 0, true, PSession.User.UserID);
            lblMessage.Text = "Note is removed from this ticket";
            lblMessage.ForeColor = Color.Green;
            long ICTicketID = (long)ViewState["ICTicketID"];
            FillAvailabilityOfOtherMachine(ICTicketID);
            DropDownList ddlTypeOfMachine = (DropDownList)gvAvailabilityOfOtherMachine.FooterRow.FindControl("ddlTypeOfMachine");
            // ddlTypeOfMachine.Focus();
            lblFocus.Focus();
        }

        public void FillAvailabilityOfOtherMachine(long ICTicketID)
        {
            ViewState["ICTicketID"] = ICTicketID;
            List<PDMS_AvailabilityOfOtherMachine> Note = new BDMS_AvailabilityOfOtherMachine().GetAvailabilityOfOtherMachine(ICTicketID, null, null, null);
            if (Note.Count == 0)
            {
                PDMS_AvailabilityOfOtherMachine N = new PDMS_AvailabilityOfOtherMachine();
                Note.Add(N);
            }
            gvAvailabilityOfOtherMachine.DataSource = Note;
            gvAvailabilityOfOtherMachine.DataBind();
        }
    }
}