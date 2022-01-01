using System;
using System.Web.UI;

namespace DealerManagementSystem.ProcessFlow
{
    public partial class Process_Flow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Process Flow');</script>");
        }
    }
}