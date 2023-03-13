using System;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class tab_AdminModulePages : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Module');</script>");
        }
    }
}