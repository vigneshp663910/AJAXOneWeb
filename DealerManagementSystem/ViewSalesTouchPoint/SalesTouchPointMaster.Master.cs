using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSalesTouchPoint
{
    public partial class SalesTouchPointMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PSession_STP.SalesTouchPointUser != null)
                {
                    lblusername.Text = PSession_STP.SalesTouchPointUser.Name;
                }
            }
        }

        protected void LogOut_ServerClick(object sender, EventArgs e)
        {
            PSession_STP.SalesTouchPointUser = null;
            Response.Redirect(UIHelper.RedirectToSalesTouchPointLogin);
        }
    }
}