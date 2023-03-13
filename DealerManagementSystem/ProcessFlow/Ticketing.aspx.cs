using Properties;
using System;

namespace DealerManagementSystem.ProcessFlow
{
    public partial class Ticketing : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ProcessFlow_Ticketing; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Process Flow');</script>");
        

        }
    }
}