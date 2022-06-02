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
    public partial class Appointment : System.Web.UI.Page
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

            string Customer = "";
            string div = "";
            foreach (PLeadFollowUp F in FUp)
            {
                Customer = F.Customer.CustomerName;
                  div = "<div><table><tr><td>"
                    + "<label >" + F.Customer.CustomerName + "</label></td><td>Prospect</td></tr >" + "<tr><td>"
                    + "<label >" + F.Customer.ContactPerson + "</label></td><td>"
                    + "<label >" + F.Customer.Mobile + " </td></tr></table></div> ";
            }
            e.Cell.Text = "<a href=" + e.SelectUrl + " style=\"color: Black\" title=\" <div> 1 June  fsbtgsrtrttsrh <br/>1fsbtgsrtrttsrh <br/>1fsbtgsrtrttsrh <br/> </div> \">" + e.Day.DayNumberText +   div + "</a>";
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