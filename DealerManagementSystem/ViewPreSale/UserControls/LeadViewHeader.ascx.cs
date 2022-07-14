using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class LeadViewHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void fillViewLead(PLead Lead)
        {
            if (Lead != null)
            {
                lblLeadNumber.Text = Lead.LeadNumber;
                lblLeadDate.Text = Lead.LeadDate.ToLongDateString();
                // lblCategory.Text = Lead.Category.Category;
                lblUrgency.Text= Lead.Urgency == null ? "" : Lead.Urgency.Urgency;
                lblApplication.Text = Lead.Application == null ? "" : Lead.Application.MainApplication;
                lblQualification.Text = Lead.Qualification == null ? "" : Lead.Qualification.Qualification;
                lblSource.Text = Lead.Source == null ? "" : Lead.Source.Source;
                lblStatus.Text = Lead.Status == null ? "" : Lead.Status.Status;
                lblProject.Text = Lead.Project == null ? "" : Lead.Project.ProjectName;
                lblDealer.Text = Lead.Dealer.DealerCode;
                lblContactPerson.Text = Lead.Customer.ContactPerson;
                lblRemarks.Text = Lead.Remarks;
                lblCustomer.Text = Lead.Customer.CustomerFullName;
                lblContactPerson.Text = Lead.Customer.ContactPerson;
                lblMobile.Text = "<a href='tel:" + Lead.Customer.Mobile + "'>" + Lead.Customer.Mobile + "</a>";
                lblEmail.Text = "<a href='mailto:" + Lead.Customer.Email + "'>" + Lead.Customer.Email + "</a>";

                string Location = Lead.Customer.Address1 + ", " + Lead.Customer.Address2 + ", " + Lead.Customer.District.District + ", " + Lead.Customer.State.State;
                lblLocation.Text = Location;
                lblCustomerFeedback.Text = Lead.CustomerFeedback;
            }
        }
        public void Clear()
        {

            lblLeadNumber.Text = "";
            lblLeadDate.Text = "";
            lblApplication.Text = "";
            lblQualification.Text = "";
            lblSource.Text = "";
            lblStatus.Text = "";
            lblProject.Text = "";
            lblDealer.Text = "";
            lblContactPerson.Text = "";
            lblRemarks.Text = "";
            lblCustomer.Text = "";
            lblContactPerson.Text = "";
            lblMobile.Text = "";
            lblEmail.Text = ""; 
            lblLocation.Text = "";
        }
    }
}