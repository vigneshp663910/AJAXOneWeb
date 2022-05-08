using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDashboard
{
    public partial class LeadStatusOpen : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblOpen.Text = "0";
            int? DealerID = (int?)Session["SerDealerID"];
            DateTime? DateFrom = (DateTime?)Session["SerDateFrom"];
            DateTime? DateTo = (DateTime?)Session["SerDateTo"];
            List<PLeadStatus> Status = new BLead().GetLeadCountByStatus(DateFrom, DateTo, DealerID, PSession.User.UserID);
            if ((Status.Where(m => m.StatusID == 1).Count() != 0))
            {
                var ss = Status.Where(m => m.StatusID == 1).ToList();
                lblOpen.Text = ss[0].Count.ToString();
            }
        }

        protected void lbActions_Click(object sender, EventArgs e)
        { 
            Session["leadStatusID"] = 1;
            Session["leadDateFrom"] = (DateTime?)Session["SerDateFrom"];
            Response.Redirect("ViewPreSale/lead.aspx");
        }
    }
}