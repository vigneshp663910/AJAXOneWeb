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
            //txtLeadDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); 
           // txtLeadDate.TextMode = TextBoxMode.Date;

            List<PLeadQualification> Qualification = new BLead().GetLeadQualification(null, null);
            //new DDLBind(ddlQualification, Qualification, "Qualification", "QualificationID");
            //ddlQualification.SelectedValue = "1";

            //List<PLeadType> LeadType = new BLead().GetLeadType(null, null);
            //new DDLBind(ddlLeadType, LeadType, "Type", "TypeID");
             
            new DDLBind(ddlSource, new BLead().GetLeadSource(null, null), "Source", "SourceID");
            new DDLBind(ddlSalesChannelType, new BPreSale().GetPreSalesMasterItem((short)PreSalesMasterHeader.SalesChannelType), "ItemText", "MasterItemID");
            //List<PLeadCategory> Category = new BLead().GetLeadCategory(null, null);
            //new DDLBind(ddlCategory, Category, "Category", "CategoryID");


            //List<PProductType> ProductType = new BDMS_Master().GetProductType(null, null);
            //new DDLBind(ddlProductType, ProductType, "ProductType", "ProductTypeID");
            List<PProductType> PTypes = new BDMS_Master().GetProductType(null, null);
            ddlProductType.Items.Clear();
            if (PSession.User.DealerTypeID == (short)DealerType.Retailer)
            {
                foreach (PProductType PType in PTypes)
                {
                    if (PType.ProductTypeID == (short)ProductType.Udaan)
                        ddlProductType.Items.Insert(0, new ListItem(PType.ProductType, PType.ProductTypeID.ToString()));
                }
            }
            else if (PSession.User.DealerTypeID == (short)DealerType.Dealer)
            {
                foreach (PProductType PType in PTypes)
                {
                    if (PType.ProductTypeID != (short)ProductType.Udaan)
                        ddlProductType.Items.Insert(0, new ListItem(PType.ProductType, PType.ProductTypeID.ToString()));
                }
            }
            else
            {
                new DDLBind(ddlProductType, PTypes, "ProductType", "ProductTypeID");
            }

            new DDLBind(ddlApplication, new BDMS_Service().GetMainApplication(null, null), "MainApplication", "MainApplicationID");
          //  new DDLBind(ddlProject, new BProject().GetProject(null, null,null,null,null,null,null), "ProjectName_state", "ProjectID");

            cxNextFollowUpDate.StartDate = DateTime.Now;
            cxExpectedDateOfSale.StartDate = DateTime.Now;
        }

        void Clear()
        { 
        }
        public void fillLead(PLead Lead)
        {
            //txtLeadDate.Text = Lead.LeadDate.ToString("yyyy-MM-dd");
            List<PLeadProduct> LeadProduct = new BLead().GetLeadProduct(Lead.LeadID, PSession.User.UserID);
            if(LeadProduct.Count !=0)
            {
                ddlProductType.Enabled = false;
            }
            ddlProductType.SelectedValue = Convert.ToString(Lead.ProductType.ProductTypeID); 
            ddlSource.SelectedValue = Lead.Source==null?"0": Convert.ToString(Lead.Source.SourceID);
            ddlSalesChannelType.SelectedValue = Lead.SalesChannelType == null ? "0" : Convert.ToString(Lead.SalesChannelType.MasterItemID);
            // ddlProject.SelectedValue = Lead.Project == null ? "0" : Convert.ToString(Lead.Project.ProjectID);
            hdfProjectID.Value = Lead.Project == null ? "" : Convert.ToString(Lead.Project.ProjectID);
            txtProject.Text = Lead.Project == null ? "" : Convert.ToString(Lead.Project.ProjectName);
            txtProject.Enabled = Lead.Project == null ? true : false;
            //ddlUrgency.SelectedValue = Lead.Urgency == null ? "0" : Convert.ToString(Lead.Urgency.UrgencyID);
            txtExpectedDateOfSale.Text = Lead.ExpectedDateOfSale== null?"": Convert.ToString(Lead.ExpectedDateOfSale);
            ddlApplication.SelectedValue = Lead.Application == null ? "0" : Convert.ToString(Lead.Application.MainApplicationID);
            txtCustomerFeedback.Text = Lead.CustomerFeedback;
            txtRemarks.Text = Lead.Remarks;
            txtNextFollowUpDate.Text = Lead.NextFollowUpDate == null ? "" : Convert.ToString(Lead.NextFollowUpDate);
             
        }
        public PLead_Insert Read()
        {
            PLead_Insert Lead = new PLead_Insert();
           // Lead.LeadDate = Convert.ToDateTime(txtLeadDate.Text.Trim());
            Lead.ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue);

            //Lead.Category = ddlCategory.SelectedValue == "" ? null : new PLeadCategory() { CategoryID = Convert.ToInt32(ddlCategory.SelectedValue) };
            //Lead.QualificationID = ddlQualification.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlQualification.SelectedValue);
            Lead.SourceID = ddlSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSource.SelectedValue);
            Lead.SalesChannelTypeID = ddlSalesChannelType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesChannelType.SelectedValue);
            // Lead.ProjectID = ddlProject.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProject.SelectedValue);
            Lead.ProjectID = hdfProjectID.Value == "" ? (int?)null : Convert.ToInt32(hdfProjectID.Value);
            // Lead.UrgencyID = ddlUrgency.SelectedValue == "0" ? (int?)null :    Convert.ToInt32(ddlUrgency.SelectedValue);
            Lead.ExpectedDateOfSale = Convert.ToDateTime(txtExpectedDateOfSale.Text.Trim());
            Lead.MainApplicationID = ddlApplication.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlApplication.SelectedValue);
            Lead.CustomerFeedback = txtCustomerFeedback.Text.Trim();
            Lead.Remarks = txtRemarks.Text.Trim();
            Lead.NextFollowUpDate = Convert.ToDateTime(txtNextFollowUpDate.Text.Trim());
            Lead.SalesChannelTypeID = ddlSalesChannelType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesChannelType.SelectedValue);
            return Lead;
        }
        public string Validation()
        {
            string Message = "";
             txtNextFollowUpDate.BorderColor = Color.Silver;
            ddlProductType.BorderColor = Color.Silver;
            txtExpectedDateOfSale.BorderColor = Color.Silver;
            //ddlApplication.BorderColor = Color.Silver;
            //ddlCategory.BorderColor = Color.Silver;
            //ddlQualification.BorderColor = Color.Silver;
            ddlSource.BorderColor = Color.Silver;
            //ddlLeadType.BorderColor = Color.Silver;
            //txtRemarks.BorderColor = Color.Silver;


            if (string.IsNullOrEmpty(txtNextFollowUpDate.Text.Trim()))
            {
                txtNextFollowUpDate.BorderColor = Color.Red;
                return "Please enter the Next FollowUp Date.";

            }
            //else 
            if (ddlProductType.SelectedValue == "0")
            {
                ddlProductType.BorderColor = Color.Red;
                return "Please select the Product Type";
            }
            if (string.IsNullOrEmpty(txtExpectedDateOfSale.Text.Trim()))
            {
                txtExpectedDateOfSale.BorderColor = Color.Red;
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

            if (ddlSource.SelectedValue == "0")
            {
                ddlSource.BorderColor = Color.Red;
                return "Please select the Source";

            }
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