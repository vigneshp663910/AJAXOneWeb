using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class ICTicketNote : System.Web.UI.UserControl
    {
        public PDMS_ICTicket SDMS_ICTicket
        {
            get;
            set;
        }
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
                FillServiceNotes();
            }
        }


        protected void gvNotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlNoteType = (DropDownList)e.Row.FindControl("ddlNoteType");
                ddlNoteType.DataTextField = "NoteType";
                ddlNoteType.DataValueField = "NoteTypeID";
                ddlNoteType.DataSource = new BDMS_Service().GetNoteType(null, null);
                ddlNoteType.DataBind();
            }
        }
        protected void lblNoteAdd_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            DropDownList ddlNoteType = (DropDownList)gvNotes.FooterRow.FindControl("ddlNoteType");

            //  ddlNoteType.Focus();
            TextBox txtComments = (TextBox)gvNotes.FooterRow.FindControl("txtComments");
            if (ddlNoteType.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Note Type";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(txtComments.Text))
            {
                lblMessage.Text = "Please enter the Comments";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (new BDMS_ICTicket().InsertOrUpdateNoteAddOrRemoveICTicket(0, SDMS_ICTicket.ICTicketID, Convert.ToInt32(ddlNoteType.SelectedValue), txtComments.Text, false, PSession.User.UserID))
            {
                lblMessage.Text = "New note is added for this ticket";
                lblMessage.ForeColor = Color.Green;
                FillServiceNotes();

            }
            else
            {
                lblMessage.Text = "New note is not added for this ticket";
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void lblNoteRemove_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long ServiceNoteID = Convert.ToInt64(gvNotes.DataKeys[gvRow.RowIndex].Value);
            new BDMS_ICTicket().InsertOrUpdateNoteAddOrRemoveICTicket(ServiceNoteID, 0, 0, "", true, PSession.User.UserID);
            lblMessage.Text = "Note is removed from this ticket";
            lblMessage.ForeColor = Color.Green;

            FillServiceNotes();
            DropDownList ddlNoteType = (DropDownList)gvNotes.FooterRow.FindControl("ddlNoteType");
            //  ddlNoteType.Focus();
        }



        private void FillServiceNotes()
        {
            List<PDMS_ServiceNote> Note = new BDMS_Service().GetServiceNote(SDMS_ICTicket.ICTicketID, null, null, "");
            if (Note.Count == 0)
            {
                PDMS_ServiceNote N = new PDMS_ServiceNote();
                Note.Add(N);
            }
            gvNotes.DataSource = Note;
            gvNotes.DataBind();
        }
    }

}