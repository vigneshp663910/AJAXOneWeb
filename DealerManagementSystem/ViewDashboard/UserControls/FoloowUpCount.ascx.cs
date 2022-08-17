using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDashboard.UserControls
{
    public partial class FoloowUpCount : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblTodaysFollowUpCount.Text = "0";
            lblFuture7DaysFollowUpCount.Text = "0";
            int? DealerID = (int?)Session["SerDealerID"];
            //DateTime? DateFrom = (DateTime?)Session["SerDateFrom"];
            //DateTime? DateTo = (DateTime?)Session["SerDateTo"];
            List<int> LeadFollowUpCount = new BLead().GetLeadFollowUpCount(DealerID, PSession.User.UserID);

            lblTodaysFollowUpCount.Text = LeadFollowUpCount[0].ToString();
            lblFuture7DaysFollowUpCount.Text = LeadFollowUpCount[1].ToString();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            //Session["leadStatusID"] = 1;
            //Session["leadDateFrom"] = (DateTime?)Session["SerDateFrom"];
            Response.Redirect("ViewPreSale/ManageLeadFoloowUps.aspx");
        }
        //protected void lbActions_Click(object sender, EventArgs e)
        //{

        //    Session["leadStatusID"] = 3;
        //    Session["leadDateFrom"] = (DateTime?)Session["SerDateFrom"];
        //    Response.Redirect("ViewPreSale/lead.aspx");
        //}
    }
}