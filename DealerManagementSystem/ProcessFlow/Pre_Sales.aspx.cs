using Properties;
using System;

namespace DealerManagementSystem.ProcessFlow
{
    public partial class Pre_Sales : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ProcessFlow_Pre_Sales; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Process Flow');</script>");
        

        }

        protected void btnSOP_Click(object sender, EventArgs e)
        {

        }
    }
}