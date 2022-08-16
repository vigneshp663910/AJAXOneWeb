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
    public partial class LeadStatusDashboard : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblOpen.Text = "0";
            lblAssigned.Text = "0";
            lblQuotation.Text = "0";

            int? DealerID = (int?)Session["SerDealerID"];
            DateTime? DateFrom = (DateTime?)Session["SerDateFrom"];
            DateTime? DateTo = (DateTime?)Session["SerDateTo"];
            List<PLeadStatus> Status = new BLead().GetLeadCountByStatus(DateFrom, DateTo, DealerID, PSession.User.UserID);
           
            if (Status != null)
            {
                if ((Status.Where(m => m.StatusID == 1).Count() != 0))
                {
                    var ss = Status.Where(m => m.StatusID == 1).ToList();
                    lblOpen.Text = ss[0].Count.ToString();
                }

                if ((Status.Where(m => m.StatusID == 2).Count() != 0))
                {
                    var ss = Status.Where(m => m.StatusID == 2).ToList();
                    lblAssigned.Text = ss[0].Count.ToString();
                }

                if ((Status.Where(m => m.StatusID == 3).Count() != 0))
                {
                    var ss = Status.Where(m => m.StatusID == 3).ToList();
                    lblQuotation.Text = ss[0].Count.ToString();
                }
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbtnLeadStatusOpen")
            {
                Session["leadStatusID"] = 1;
            }
            else if (lbActions.ID == "lbtnLeadStatusAssigned")
            {
                Session["leadStatusID"] = 2;
            }
            else if(lbActions.ID == "lbtnLeadStatusQuotation")
            {
                Session["leadStatusID"] = 3;
            } 
            Session["leadDateFrom"] = (DateTime?)Session["SerDateFrom"];
            Response.Redirect("ViewPreSale/lead.aspx"); 
        }
    }
}