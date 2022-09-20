using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class AddLead : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster()
        {
            txtLeadDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); 
            txtLeadDate.TextMode = TextBoxMode.Date;

            List<PLeadQualification> Qualification = new BLead().GetLeadQualification(null, null);
            //new DDLBind(ddlQualification, Qualification, "Qualification", "QualificationID");
            //ddlQualification.SelectedValue = "1";

            //List<PLeadType> LeadType = new BLead().GetLeadType(null, null);
            //new DDLBind(ddlLeadType, LeadType, "Type", "TypeID");

            List<PLeadSource> Source = new BLead().GetLeadSource(null, null);
            new DDLBind(ddlSource, Source, "Source", "SourceID");

            //List<PLeadCategory> Category = new BLead().GetLeadCategory(null, null);
            //new DDLBind(ddlCategory, Category, "Category", "CategoryID");


            List<PProductType> ProductType = new BDMS_Master().GetProductType(null, null);
            new DDLBind(ddlProductType, ProductType, "ProductType", "ProductTypeID");

            
            new DDLBind(ddlApplication, new BDMS_Service().GetMainApplication(null, null), "MainApplication", "MainApplicationID");
            new DDLBind(ddlProject, new BProject().GetProject(null, null,null,null,null,null,null), "ProjectName_state", "ProjectID");
        }

        void Clear()
        {


        }
        public void fillLead(PLead Lead)
        {
            txtLeadDate.Text = Lead.LeadDate.ToString("yyyy-MM-dd");  
            
            ddlProductType.SelectedValue = Convert.ToString(Lead.ProductType.ProductTypeID);
            //ddlCategory.SelectedValue = Convert.ToString(Lead.Category.CategoryID);
           // ddlQualification.SelectedValue = Lead.Qualification == null ? "0" : Convert.ToString(Lead.Qualification.QualificationID);
            ddlSource.SelectedValue = Lead.Source==null?"0": Convert.ToString(Lead.Source.SourceID);
            ddlProject.SelectedValue = Lead.Project == null ? "0" : Convert.ToString(Lead.Project.ProjectID);
            //ddlUrgency.SelectedValue = Lead.Urgency == null ? "0" : Convert.ToString(Lead.Urgency.UrgencyID);
            txtExpectedDateOfSale.Text = Lead.ExpectedDateOfSale== null?"": Convert.ToString(Lead.ExpectedDateOfSale);
            ddlApplication.SelectedValue = Lead.Application == null ? "0" : Convert.ToString(Lead.Application.MainApplicationID);
            txtCustomerFeedback.Text = Lead.CustomerFeedback;
            txtRemarks.Text = Lead.Remarks;
        }
        public PLead_Insert Read()
        {
            PLead_Insert Lead = new PLead_Insert();
            Lead.LeadDate = Convert.ToDateTime(txtLeadDate.Text.Trim());
            Lead.ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue);

            //Lead.Category = ddlCategory.SelectedValue == "" ? null : new PLeadCategory() { CategoryID = Convert.ToInt32(ddlCategory.SelectedValue) };
          //  Lead.QualificationID = ddlQualification.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlQualification.SelectedValue);
            Lead.SourceID = ddlSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSource.SelectedValue);
            Lead.ProjectID = ddlProject.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProject.SelectedValue);
            // Lead.UrgencyID = ddlUrgency.SelectedValue == "0" ? (int?)null :    Convert.ToInt32(ddlUrgency.SelectedValue);
            Lead.ExpectedDateOfSale = Convert.ToDateTime(txtExpectedDateOfSale.Text.Trim());
            Lead.MainApplicationID = ddlApplication.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlApplication.SelectedValue);
            Lead.CustomerFeedback = txtCustomerFeedback.Text.Trim();
            Lead.Remarks = txtRemarks.Text.Trim();
            return Lead;
        }
        public string Validation()
        {
            string Message = "";
            txtLeadDate.BorderColor = Color.Silver;
            ddlProductType.BorderColor = Color.Silver;
             txtExpectedDateOfSale.BorderColor = Color.Silver;
            //ddlApplication.BorderColor = Color.Silver;
            //ddlCategory.BorderColor = Color.Silver;
            //ddlQualification.BorderColor = Color.Silver;
            //ddlSource.BorderColor = Color.Silver;
            //ddlLeadType.BorderColor = Color.Silver;
            //txtRemarks.BorderColor = Color.Silver;
            if (string.IsNullOrEmpty(txtLeadDate.Text.Trim()))
            {
                txtLeadDate.BorderColor = Color.Red;
                return "Please enter the Lead Date";

            }
            else if (ddlProductType.SelectedValue == "0")
            {
                ddlProductType.BorderColor = Color.Red;
                return "Please select the Product Type";
            }
            else if(string.IsNullOrEmpty(txtExpectedDateOfSale.Text.Trim()))
            {
                txtLeadDate.BorderColor = Color.Red;
                return "Please select the Expected Date of Sale";

            }
            //else if (ddlCategory.SelectedValue == "0")
            //{
            //    ddlCategory.BorderColor = Color.Red;
            //    return "Please select the Category";

            //}
            //else if (ddlQualification.SelectedValue == "0")
            //{
            //    ddlQualification.BorderColor = Color.Red;
            //    return "Please select the Qualification"; 
            //}
            //else if (ddlSource.SelectedValue == "0")
            //{
            //    ddlSource.BorderColor = Color.Red;
            //    return "Please select the Source";

            //}
            //else if (ddlLeadType.SelectedValue == "0")
            //{
            //    ddlLeadType.BorderColor = Color.Red;
            //    return "Please select the LeadType";

            //} 
            //else if (ddlUrgency.SelectedValue == "0")
            //{
            //    ddlUrgency.BorderColor = Color.Red;
            //    return "Please select the Urgency"; 
            //}
            //else if (ddlApplication.SelectedValue == "0")
            //{
            //    ddlApplication.BorderColor = Color.Red;
            //    return "Please select the Application"; 
            //}
            //else if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
            //{
            //    txtRemarks.BorderColor = Color.Red;
            //    Message = Message + "<br/>Please enter the Remark"; 
            //} 
            return Message;
        }
    }
}