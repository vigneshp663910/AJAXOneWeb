using System;
using System.Web.UI;

namespace DealerManagementSystem.ProcessFlow
{
    public partial class OrgChart : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Organisation Chart » Model');</script>");
        }

    }

}