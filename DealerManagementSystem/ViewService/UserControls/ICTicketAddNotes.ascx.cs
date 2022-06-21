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
    public partial class ICTicketAddNotes : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster()
        { 
            ddlNoteType.DataTextField = "NoteType";
            ddlNoteType.DataValueField = "NoteTypeID";
            ddlNoteType.DataSource = new BDMS_Service().GetNoteType(null, null);
            ddlNoteType.DataBind();
        }

        void Clear()
        {


        }
        public string Read()
        {
            return "&NoteTypeID=" + ddlNoteType.SelectedValue + "&Comments=" + txtComments.Text;
        }
        public string Validation()
        {
            string Message = "";
            if (ddlNoteType.SelectedValue == "0")
            {
                return "Please select the Note Type"; 
            }
            if (string.IsNullOrEmpty(txtComments.Text))
            {
                return "Please enter the Comments"; 
            }
            return Message;
        }
    }
}