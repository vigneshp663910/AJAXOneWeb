using Properties;
using System;

namespace DealerManagementSystem.ProcessFlow
{
    public partial class DMM_Process : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ProcessFlow_DMM_Process; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealer Employee » Process Flow');</script>");
        }
    }
}
