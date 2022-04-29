using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Pre_Sales_Dashboard : System.Web.UI.Page
    {
        DateTime? From = null;
        DateTime? To = DateTime.Now;
        DateTime? FromF = null;
        DateTime? ToF = DateTime.Now;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
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
            lblOpen.Text = "0";
            lblAssigned.Text = "0";
            lblQuotation.Text = "0";
            lblWon.Text = "0";
            lblLost.Text = "0"; 
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
            if ((Status.Where(m => m.StatusID == 6).Count() != 0))
            {
                var ss = Status.Where(m => m.StatusID == 6).ToList();
                lblCancelled.Text = ss[0].Count.ToString();
            }
        }
        void FillFunnel()
        {

            lblNewlyCreatedF.InnerText = "Newly Created: 0" ;
            lblConvertToProspectF.InnerText = "0";
            lblWonF.InnerText = "0";

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

            int Open = 0, Assigned = 0, Quotation = 0, Won = 0, Lost = 0, Cancel = 0; 
            if ((StatusF.Where(m => m.StatusID == 1).Count() != 0))
            {
                var ss = StatusF.Where(m => m.StatusID == 1).ToList();
                Open = ss[0].Count;
            }
            if ((StatusF.Where(m => m.StatusID == 2).Count() != 0))
            {
                var ss = StatusF.Where(m => m.StatusID == 2).ToList();
                Assigned = ss[0].Count;
            }
            
            if ((StatusF.Where(m => m.StatusID == 3).Count() != 0))
            {
                var ss = StatusF.Where(m => m.StatusID == 3).ToList();
                Quotation = ss[0].Count; 
            }
            if ((StatusF.Where(m => m.StatusID == 4).Count() != 0))
            {
                var ss = StatusF.Where(m => m.StatusID == 4).ToList();
               
                Won = ss[0].Count;
            }
            if ((StatusF.Where(m => m.StatusID == 5).Count() != 0))
            {
                var ss = StatusF.Where(m => m.StatusID == 5).ToList();
                Lost = ss[0].Count;
            }
            if ((StatusF.Where(m => m.StatusID == 6).Count() != 0))
            {
                var ss = StatusF.Where(m => m.StatusID == 6).ToList();
                Cancel = ss[0].Count;
            }
            lblNewlyCreatedF.InnerText = "Newly Created: " + (Open + Assigned + Quotation + Won + Lost + Cancel).ToString();
            lblConvertToProspectF.InnerText = (Quotation + Won).ToString();
            lblWonF.InnerText = "Won: " + Won.ToString();
        }

        protected void rbStatus_CheckedChanged(object sender, EventArgs e)
        {
            FillStatusCount();
        }
        protected void rbStatusF_CheckedChanged(object sender, EventArgs e)
        {
            FillFunnel();
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Open")
            {
                Session["leadStatusID"] = 1;
            }
            else if (lbActions.Text == "Assigned")
            {
                Session["leadStatusID"] = 2;
            }
            else if (lbActions.Text == "Quotation")
            {
                Session["leadStatusID"] = 3;
            }
            else if (lbActions.Text == "Won")
            {
                Session["leadStatusID"] = 5;
            }
            else if (lbActions.Text == "Lost")
            {
                Session["leadStatusID"] = 6;
            }
            else if (lbActions.Text == "Cancelled")
            {
                Session["leadStatusID"] = 7;
            }
            if(rbToday.Checked)
            {
                Session["leadDateFrom"] = DateTime.Now; 
            }
            else if (rbWeek.Checked)
            {
                Session["leadDateFrom"] = DateTime.Now.AddDays(-7);
            }
            else if (rbMonth.Checked)
            {
                Session["leadDateFrom"] = DateTime.Now.AddMonths(-1);
            }
            else
            {
                Session["leadDateFrom"] = DateTime.Now.AddYears(-1);
            }
            Response.Redirect("lead.aspx");
        }
    }
}