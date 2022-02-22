using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Pre_Sales_Dashboard : System.Web.UI.Page
    {
        DateTime? From = null;
        DateTime? To = DateTime.Now;
        DateTime? FromF = null;
        DateTime? ToF = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales Dashboard');</script>");
            // lblConvertToProspect.InnerText = "Convert To  Prospect: 25000";
            From = DateTime.Now;
            FromF = DateTime.Now.AddDays(-7);
            FillStatusCount();
            FillFunnel();
        }

        void FillStatusCount()
        { 
            int? DealerID = null;
            if (rbToday.Checked)
            {
                From = DateTime.Now;
            }
            else if (rbWeek.Checked)
            {
                From = DateTime.Now.AddDays(-7);
            }
            else if (rbMonth.Checked)
            {
                From = DateTime.Now.AddMonths(-1);
            }
            else if (rbYear.Checked)
            {
                From = DateTime.Now.AddYears(-1);
            }
            List<PLeadStatus> Status = new BLead().GetLeadCountByStatus(From, To, DealerID, PSession.User.UserID);
            if ((Status.Where(m => m.StatusID == 1).Count() != 0))
            {
                var ss = Status.Where(m => m.StatusID == 1).ToList();
                lblNewlyCreated.Text = ss[0].Count.ToString();
            }

            if ((Status.Where(m => m.StatusID == 2).Count() != 0))
            {
                var ss = Status.Where(m => m.StatusID == 2).ToList();
                lblAssigned.Text = ss[0].Count.ToString();
            }
            if ((Status.Where(m => m.StatusID == 3).Count() != 0))
            {
                var ss = Status.Where(m => m.StatusID == 3).ToList();
                lblProspect.Text = ss[0].Count.ToString();
            }
            if ((Status.Where(m => m.StatusID == 4).Count() != 0))
            {
                var ss = Status.Where(m => m.StatusID == 4).ToList();
                lblWon.Text = ss[0].Count.ToString();
            }
            if ((Status.Where(m => m.StatusID == 5).Count() != 0))
            {
                var ss = Status.Where(m => m.StatusID == 5).ToList();
                lblLost.Text = ss[0].Count.ToString();
            }           
        }
        void FillFunnel()
        { 
            if (rbWeekF.Checked)
            {
                FromF = DateTime.Now.AddDays(-7);
            }
            else if (rbMonthF.Checked)
            {
                FromF = DateTime.Now.AddMonths(-1);
            }
            else if (rbYearF.Checked)
            {
                FromF = DateTime.Now.AddYears(-1);
            }

            List<PLeadStatus> StatusF = new BLead().GetLeadCountByStatus(FromF, ToF, null, PSession.User.UserID);
            int NewlyCreated = 0;
            if ((StatusF.Where(m => m.StatusID == 1).Count() != 0))
            {
                var ss = StatusF.Where(m => m.StatusID == 1).ToList();
                NewlyCreated = ss[0].Count;
            }
            if ((StatusF.Where(m => m.StatusID == 2).Count() != 0))
            {
                var ss = StatusF.Where(m => m.StatusID == 2).ToList();
                NewlyCreated = NewlyCreated + ss[0].Count;
            }
            lblNewlyCreatedF.InnerText = "Newly Created: " + NewlyCreated.ToString();
            if ((StatusF.Where(m => m.StatusID == 3).Count() != 0))
            {
                var ss = StatusF.Where(m => m.StatusID == 3).ToList();
                lblConvertToProspectF.InnerText = ss[0].Count.ToString();
            }
            if ((StatusF.Where(m => m.StatusID == 4).Count() != 0))
            {
                var ss = StatusF.Where(m => m.StatusID == 4).ToList();
                lblWonF.InnerText = "Won: " + ss[0].Count.ToString();
            }
        }

        protected void rbStatus_CheckedChanged(object sender, EventArgs e)
        {
            FillStatusCount();
        }
        protected void rbStatusF_CheckedChanged(object sender, EventArgs e)
        {
            FillFunnel();
        }
    }
}