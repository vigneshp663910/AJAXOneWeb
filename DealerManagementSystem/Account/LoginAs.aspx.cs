using System;
using System.Web.UI;

namespace DealerManagementSystem.Account
{
    public partial class LoginAs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Login As');</script>");
        }
    }
}