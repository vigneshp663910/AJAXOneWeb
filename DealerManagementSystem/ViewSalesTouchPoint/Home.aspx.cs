using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSalesTouchPoint
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession_STP.SalesTouchPointUser == null)
            {
                Response.Redirect(UIHelper.RedirectToSalesTouchPointLogin);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}