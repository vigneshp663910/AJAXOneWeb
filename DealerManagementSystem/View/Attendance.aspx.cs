using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.View
{
    public partial class Attendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Activity » Attendance');</script>");

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnPunch_Click(object sender, EventArgs e)
        {
            new BAttendance().InsertOrUpdateAttendance(PSession.User.UserID);
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void gvAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}