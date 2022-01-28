using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class ManageLeadFoloowUps : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillFollowUps();
        }
        void FillFollowUps()
        {
            List<PLeadFollowUp> FollowUp = new BLead().GetLeadFollowUp(null, null, null, null, PSession.User.UserID);
            gvFollowUp.DataSource = FollowUp;
            gvFollowUp.DataBind();
        }
    }
}