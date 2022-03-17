using System;

namespace DealerManagementSystem.ProcessFlow
{
    public partial class Pre_Sales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Process Flow');</script>");
        

        }
    }
}