using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.View
{
    public partial class Appointment : BasePage
    {
        public List<PLeadFollowUp> FollowUp
        {
            get
            {
                if (Session["Appointment"] == null)
                {
                    Session["Appointment"] = new List<PLeadFollowUp>();
                }
                return (List<PLeadFollowUp>)Session["Appointment"];
            }
            set
            {
                Session["Appointment"] = value;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string From = DateTime.Now.AddDays(-10).ToShortDateString();
                FillFollowUps(From);
            } 
        }

        protected void caAppointment_DayRender(object sender, DayRenderEventArgs e)
        {
            string From = e.Day.Date.ToShortDateString();
            List<PLeadFollowUp> FUp = new BLead().GetLeadFollowUp(null, PSession.User.UserID, From, From, null, null);
             
            string div = "";
            foreach (PLeadFollowUp F in FUp)
            {
                //Customer = F.Customer.CustomerName;
                //div = "<div><table><tr><td>"
                //  + "<label >" + F.Customer.CustomerName + "</label></td><td>Prospect</td></tr >" + "<tr><td>"
                //  + "<label >" + F.Customer.ContactPerson + "</label></td><td>"
                //  + "<label >" + F.Customer.Mobile + " </td></tr></table></div> ";
                
                div = div + "<div class=\"dropdown1\">FollowUp<div class=\"dropdown1-content\"> " + F.Customer.CustomerName + "<br /> " + F.Customer.ContactPerson + "<br /> " + "<a href = 'tel:" + F.Customer.Mobile + "' > " + F.Customer.Mobile + "</a>" + "</div> </div>";
            }
            div = e.Day.DayNumberText+ "<br />" + div;
            e.Cell.Text = "<a href=" + e.SelectUrl + " style=\"color: Black\" >" + div + "</a>";
        }

        protected void caAppointment_SelectionChanged(object sender, EventArgs e)
        {

        }

        protected void caAppointment_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            string From = e.NewDate.AddDays(-10).ToShortDateString();
            FillFollowUps(From);
        }
        void FillFollowUps(string From)
        { 
            FollowUp = new BLead().GetLeadFollowUp(null, PSession.User.UserID, From, null, null, null); 
        }
    }
}