using Properties;
using System;
using System.Net;
using System.Web.UI;

namespace DealerManagementSystem.ProcessFlow
{
    public partial class Service : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ProcessFlow_Service; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service Process Flow');</script>");
        }

        //protected void btnSOP_Click(object sender, EventArgs e)
        //{
        //}
    }
}