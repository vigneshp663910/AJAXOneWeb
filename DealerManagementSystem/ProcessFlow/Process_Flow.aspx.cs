using Properties;
using System;
using System.Web.UI;

namespace DealerManagementSystem.ProcessFlow
{
    public partial class Process_Flow : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ProcessFlow_Process_Flow; } }
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Process Flow');</script>");
        }
    }
}