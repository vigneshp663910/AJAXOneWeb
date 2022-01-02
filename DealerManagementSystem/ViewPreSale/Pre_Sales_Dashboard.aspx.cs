using System;
using System.Web.UI;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Pre_Sales_Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales Dashboard');</script>");
        }
    }
}