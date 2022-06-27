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
            lblLeadNumber.Text = Lead.LeadNumber;
            lblLeadDate.Text = Lead.LeadDate.ToLongDateString();
            lblCategory.Text = Lead.Category.Category;
            lblApplication.Text = Lead.Application.MainApplication;
            lblQualification.Text = Lead.Qualification.Qualification;
            lblQualification.Text = Lead.Qualification.Qualification;
            lblSource.Text = Lead.Source.Source;
            lblStatus.Text = Lead.Status.Status;
            lblProject.Text = Lead.Project.ProjectName;
            lblDealer.Text = Lead.Dealer.DealerCode;
            lblContactPerson.Text = Lead.Customer.ContactPerson;
            lblRemarks.Text = Lead.Remarks;
            lblCustomer.Text = Lead.Customer.CustomerFullName;
            lblContactPerson.Text = Lead.Customer.ContactPerson;
            lblMobile.Text = "<a href='tel:" + Lead.Customer.Mobile + "'>" + Lead.Customer.Mobile + "</a>";
            lblEmail.Text = "<a href='mailto:" + Lead.Customer.Email + "'>" + Lead.Customer.Email + "</a>";

            string Location = Lead.Customer.Address1 + ", " + Lead.Customer.Address2 + ", " + Lead.Customer.District.District + ", " + Lead.Customer.State.State;
            lblLocation.Text = Location; 
        }
    }
}