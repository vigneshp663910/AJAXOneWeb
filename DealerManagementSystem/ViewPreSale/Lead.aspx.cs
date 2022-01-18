using AjaxControlToolkit;
using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Lead : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Lead');</script>");

            if (!IsPostBack)
            {
                List<PLeadCategory> Category = new BLead().GetLeadCategory(null, null);
                new DDLBind(ddlCategory, Category, "Category", "CategoryID");
                new DDLBind(ddlSCategory, Category, "Category", "CategoryID");

                List <PLeadQualification > Qualification = new BLead().GetLeadQualification(null, null);
                new DDLBind(ddlQualification, Qualification, "Qualification", "QualificationID");
                new DDLBind(ddlSQualification, Qualification, "Qualification", "QualificationID");

                List<PLeadSource> Source = new BLead().GetLeadSource(null, null);
                new DDLBind(ddlSource, Source, "Source", "SourceID");
                new DDLBind(ddlSSource, Source, "Source", "SourceID");

                List<PLeadType> LeadType = new BLead().GetLeadType(null, null);
                new DDLBind(ddlLeadType, LeadType, "Type", "TypeID");
                 new DDLBind(ddlSType, LeadType, "Type", "TypeID");

                List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
             //   new DDLBind(ddlCountry, Country, "Country", "CountryID");
                new DDLBind(ddlSCountry, Country, "Country", "CountryID");
              //  new DDLBind(ddlCCountry, Country, "Country", "CountryID");

               
           
               // ddlCountry.SelectedValue = "1";

                List < PDMS_State > State= new BDMS_Address().GetState(1, null, null, null);
              //  new DDLBind(ddlState, State, "State", "StateID");
                new DDLBind(ddlSState, State, "State", "StateID");
               // new DDLBind(ddlCState, State, "State", "StateID");

                List<PLeadProgressStatus > ProgressStatus = new BLead().GetLeadProgressStatus(null, null);
                new DDLBind(ddlSProgressStatus, ProgressStatus, "ProgressStatus", "ProgressStatusID");
                new DDLBind(ddlProgressStatus, ProgressStatus, "ProgressStatus", "ProgressStatusID");

                List<PLeadStatus> Status = new BLead().GetLeadStatus(null, null);
                new DDLBind(ddlSStatus, Status, "Status", "StatusID");
                new DDLBind(ddlStatus, Status, "Status", "StatusID");

                ddlProgressStatus.SelectedValue = "1";
                ddlStatus.SelectedValue = "1";
                txtLeadDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
 
                txtLeadDate.TextMode = TextBoxMode.Date;
                //cbCustomers.DataTextField = "State";
                //cbCustomers.DataValueField = "StateID";
                //cbCustomers.DataSource = State;
                //cbCustomers.DataBind();

                //cbCustomers.Items.Insert(0, new ListItem("Select", "0"));

            }
        }

        void fillLead()
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        { 
            PLead Lead = new PLead(); 
            string Message = ""; 
            if (!string.IsNullOrEmpty(txtCustomerID.Text.Trim()))
            {
                Lead.Customer = new PDMS_Customer();
                Lead.Customer.CustomerID = Convert.ToInt64(txtCustomerID.Text.Trim());
            }
            else
            {
                Message = UC_Customer.ValidationCustomer();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                MPE_Customer.Show();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                Lead.Customer = UC_Customer.ReadCustomer();
            }

            Lead.LeadDate = Convert.ToDateTime(txtLeadDate.Text.Trim());

            Lead.Status = new PLeadStatus() { StatusID = Convert.ToInt32(ddlStatus.SelectedValue) };
            Lead.ProgressStatus = new PLeadProgressStatus() { ProgressStatusID = Convert.ToInt32(ddlProgressStatus.SelectedValue) };

            Lead.Category = new PLeadCategory() { CategoryID = Convert.ToInt32(ddlCategory.SelectedValue) };
            Lead.Qualification = new PLeadQualification() { QualificationID = Convert.ToInt32(ddlQualification.SelectedValue) };
            Lead.Source = new PLeadSource() { SourceID = Convert.ToInt32(ddlSource.SelectedValue) };
            Lead.Type = new PLeadType() { TypeID = Convert.ToInt32(ddlLeadType.SelectedValue) }; 
            Lead.Remarks = txtRemarks.Text.Trim(); 
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };

            PLead l = JsonConvert.DeserializeObject<PLead>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead", Lead)).Data));
        }

        //protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    new DDLBind(ddlState, new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
        //}

        //protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null), "District", "DistrictID");
        //}

        //protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    new DDLBind(ddlTehsil, new BDMS_Address().GetTehsil(Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), null), "Tehsil", "TehsilID");
        //}

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            PLeadSearch S = new PLeadSearch();
            S.LeadNumber = txtLeadNumber.Text.Trim();
            S.StateID = ddlSState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSState.SelectedValue);
            S.ProgressStatusID = ddlSProgressStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSProgressStatus.SelectedValue);
            S.CategoryID = ddlSCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCategory.SelectedValue);
            S.QualificationID = ddlSQualification.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSQualification.SelectedValue);
            S.SourceID = ddlSSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSSource.SelectedValue);
            S.TypeID = ddlSType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSType.SelectedValue);
            S.CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);
            S.StatusID = ddlSStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSStatus.SelectedValue); 

            S.CustomerCode = txtCustomer.Text.Trim();
            S.LeadDateFrom = string.IsNullOrEmpty(txtLeadDateFrom.Text.Trim())?(DateTime?) null: Convert.ToDateTime( txtLeadDateFrom.Text.Trim());
            S.LeadDateTo = string.IsNullOrEmpty(txtLeadDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateTo.Text.Trim());

            List<PLead> Leads = new BLead().GetLead(S);
            gvLead.DataSource = Leads;
            gvLead.DataBind();
        }

        //protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //    DropDownList ddlAction = (DropDownList)gvRow.FindControl("ddlAction");
        //    Label lblLeadID = (Label)gvRow.FindControl("lblLeadID");
        //    ViewState["LeadID"] = lblLeadID.Text;
             
        //    if (ddlAction.Text == "View Lead")
        //    {
        //        divList.Visible = false;
        //        divDetailsView.Visible = true;
        //        UC_LeadView.fillViewLead(Convert.ToInt64(lblLeadID.Text));
        //    }
            //else if (ddlAction.Text == "Assign")
            //{
            //    MP_AssignSE.Show();
            //    fillAssignSalesEngineer(Convert.ToInt64(lblLeadID.Text)); 
            //}
            //else if (ddlAction.Text == "Add Follow-up")
            //{
            //    MPE_FollowUp.Show();
            //    fillFollowUp(Convert.ToInt64(lblLeadID.Text));
            //}
            //else if (ddlAction.Text == "Customer Convocation")
            //{
            //    MPE_Convocation.Show();
            //    fillConvocation(Convert.ToInt64(lblLeadID.Text)); 
            //}            
            //else if (ddlAction.Text == "Edit Financial Info")
            //{
            //    MPE_Financial.Show();
            //    fillFinancial(Convert.ToInt64(lblLeadID.Text));
            //}
            //else if (ddlAction.Text == "Add Effort")
            //{
            //    MPE_Effort.Show();
            //    fillEffort(Convert.ToInt64(lblLeadID.Text));
            //}
            //else if (ddlAction.Text == "Add Expense")
            //{
            //    MPE_Expense.Show();
            //    fillExpense(Convert.ToInt64(lblLeadID.Text));
            //}
        //}
        protected void ddlSCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlSState, new BDMS_Address().GetState(Convert.ToInt32(ddlSCountry.SelectedValue), null, null, null), "State", "StateID");
        }
 
      
        //protected void cbNewCustomer_CheckedChanged(object sender, EventArgs e)
        //{
        //    if(cbNewCustomer.Checked)
        //    {
        //        pnlNewCustomerName.Visible = true;
        //        pnlOldCustomerName.Visible = false;
        //    }
        //    else
        //    {
        //        pnlNewCustomerName.Visible = false;
        //        pnlOldCustomerName.Visible = true;
        //    }
        //}
  
         
        protected void btnAddLead_Click(object sender, EventArgs e)
        {
            MPE_Customer.Show();
            UC_Customer.FillMaster();
        }
        [WebMethod]
        public static List<string> GetCustomer(string CustS)
        {
            List<string> Emp = new List<string>();
            List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerProspectAutocomplete(CustS);
            int i = 0;
            foreach (PDMS_Customer cust in Customer)
            {
                i = i + 1;
                string div = "<label id='lblCustomerID" + i + "' style='display: none'>" + cust.CustomerID + "</label>"
                    + "<table><tr><td>"
                    + "<label id='lblCustomerName" + i + "'>" + cust.CustomerName + "</label></td><td>Prospect</td></tr >" + "<tr><td>"
                    + "<label id='lblContactPerson" + i + "'>" + cust.ContactPerson + "</label></td><td>"
                    + "<label id='lblMobile" + i + "'>" + cust.Mobile + " </td></tr></ table >";
                Emp.Add(div);
            }
            return Emp;
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
        }

        protected void btnViewLead_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblLeadID = (Label)gvRow.FindControl("lblLeadID");
            ViewState["LeadID"] = lblLeadID.Text;

            divList.Visible = false;
            divDetailsView.Visible = true;
            UC_LeadView.fillViewLead(Convert.ToInt64(lblLeadID.Text));
        }
    }
}