using System;

namespace DealerManagementSystem.ProcessFlow
{
    public partial class DMM_Process : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealer Manpower Management Process Flow');</script>");
        }
    }
}