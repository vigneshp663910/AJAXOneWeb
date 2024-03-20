using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.Planning
{
    public partial class DealerBusinessExcellenceCategory3Update : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_Planning_DealerBusinessExcellenceCategory3Update; } } 
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Business Excellence » Category 3 Update');</script>");
        }
    }
}