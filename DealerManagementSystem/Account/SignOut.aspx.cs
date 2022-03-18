using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.Account
{
    public partial class SignOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogOutYes_Click(object sender, EventArgs e)
        {
            //Session.Abandon();
            //Request.Cookies.Clear();
            //Session.Clear();
            //Session.RemoveAll();

            PSession.User = null;
            Response.Redirect("/Account/SignOut.htm");
            
        }

        protected void btnLogOutNo_Click(object sender, EventArgs e)
        {
        
            Response.Redirect("../Home.aspx");
        }
    }
}