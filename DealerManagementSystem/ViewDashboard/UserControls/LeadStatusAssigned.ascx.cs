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
    public partial class LeadStatusAssigned : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAssigned.Text = "0";
            int? DealerID = (int?)Session["SerDealerID"];
            DateTime? DateFrom = (DateTime?)Session["SerDateFrom"];
            DateTime? DateTo = (DateTime?)Session["SerDateTo"];
            List<PLeadStatus> Status = new BLead().GetLeadCountByStatus(DateFrom, DateTo, DealerID, PSession.User.UserID);
            if (Status != null)
            {
                if ((Status.Where(m => m.StatusID == 2).Count() != 0))
                {
                    var ss = Status.Where(m => m.StatusID == 2).ToList();
                    lblAssigned.Text = ss[0].Count.ToString();
                }
            }
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {  
            Session["leadStatusID"] = 2;
            Session["leadDateFrom"] = (DateTime?)Session["SerDateFrom"];
            Response.Redirect("ViewPreSale/lead.aspx");
        }
    }
}