using Properties;
using System;
using System.Collections.Generic;


namespace DealerManagementSystem.ViewProcurement
{
    public partial class PurchaseOrderN : BasePage
    { 
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}