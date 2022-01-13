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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales ~ Lead');</script>");

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

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            DropDownList ddlAction = (DropDownList)gvRow.FindControl("ddlAction");
            Label lblLeadID = (Label)gvRow.FindControl("lblLeadID");
            ViewState["LeadID"] = lblLeadID.Text;
             
            if (ddlAction.Text == "View Lead")
            { 
                fillViewLead(Convert.ToInt64(lblLeadID.Text));
            }
            else if (ddlAction.Text == "Assign")
            {
                MP_AssignSE.Show();
                fillAssignSalesEngineer(Convert.ToInt64(lblLeadID.Text)); 
            }
            else if (ddlAction.Text == "Add Follow-up")
            {
                MPE_FollowUp.Show();
                fillFollowUp(Convert.ToInt64(lblLeadID.Text));
            }
            else if (ddlAction.Text == "Customer Convocation")
            {
                MPE_Convocation.Show();
                fillConvocation(Convert.ToInt64(lblLeadID.Text)); 
            }            
            else if (ddlAction.Text == "Edit Financial Info")
            {
                MPE_Financial.Show();
                fillFinancial(Convert.ToInt64(lblLeadID.Text));
            }
            else if (ddlAction.Text == "Add Effort")
            {
                MPE_Effort.Show();
                fillEffort(Convert.ToInt64(lblLeadID.Text));
            }
            else if (ddlAction.Text == "Add Expense")
            {
                MPE_Expense.Show();
                fillExpense(Convert.ToInt64(lblLeadID.Text));
            }
        }
        protected void ddlSCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlSState, new BDMS_Address().GetState(Convert.ToInt32(ddlSCountry.SelectedValue), null, null, null), "State", "StateID");
        }
 
        protected void lblSalesEngineerAdd_Click(object sender, EventArgs e)
        {
            DropDownList ddlSalesEngineer = (DropDownList)gvSalesEngineer.FooterRow.FindControl("ddlSalesEngineer");
            PLeadSalesEngineer Lead = new PLeadSalesEngineer();
            Lead.LeadSalesEngineerID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            Lead.SalesEngineer =  new PUser { UserID = Convert.ToInt32(ddlSalesEngineer.SelectedValue) };
            Lead.AssignedBy  = new PUser { UserID = PSession.User.UserID };
            Lead.IsActive = true;

            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/SalesEngineer", Lead)).Data);
            MP_AssignSE.Show();
            fillAssignSalesEngineer(Lead.LeadID);
        }
        protected void lblConvocationAdd_Click(object sender, EventArgs e)
        { 

            DropDownList ddlSalesEngineerF = (DropDownList)gvConvocation.FooterRow.FindControl("ddlSalesEngineerF");
            DropDownList ddlProgressStatusF = (DropDownList)gvConvocation.FooterRow.FindControl("ddlProgressStatusF");
            TextBox txtConvocationDateF = (TextBox)gvConvocation.FooterRow.FindControl("txtConvocationDateF");
            TextBox txtConvocationF = (TextBox)gvConvocation.FooterRow.FindControl("txtConvocationF");

            PLeadConvocation Lead = new PLeadConvocation();
            Lead.LeadConvocationID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            Lead.ProgressStatus = new PLeadProgressStatus() { ProgressStatusID =Convert.ToInt32(ddlProgressStatusF.SelectedValue) };
            Lead.ConvocationDate = Convert.ToDateTime(txtConvocationDateF.Text.Trim());
            Lead.Convocation = txtConvocationF.Text.Trim();
            Lead.SalesEngineer = new PUser { UserID = Convert.ToInt32(ddlSalesEngineerF.SelectedValue) };
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Convocation", Lead)).Data);


        }
        protected void lblFollowUpAdd_Click(object sender, EventArgs e)
        {

        DropDownList ddlSalesEngineer = (DropDownList)gvFollowUp.FooterRow.FindControl("ddlSalesEngineer");
        TextBox txtFollowUpDateF = (TextBox)gvFollowUp.FooterRow.FindControl("txtFollowUpDateF");
        TextBox txtFollowUpNoteF = (TextBox)gvFollowUp.FooterRow.FindControl("txtFollowUpNoteF");

        PLeadFollowUp Lead = new PLeadFollowUp();
            Lead.LeadFollowUpID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            Lead.FollowUpDate = Convert.ToDateTime(txtFollowUpDateF.Text.Trim());
            Lead.FollowUpNote = txtFollowUpNoteF.Text.Trim();
            Lead.SalesEngineer = new PUser { UserID = Convert.ToInt32(ddlSalesEngineer.SelectedValue) };
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID }; 

            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/FollowUp", Lead)).Data);
            MPE_FollowUp.Show();
            fillFollowUp(Lead.LeadID);
        }
      
        protected void btnSaveEffort_Click(object sender, EventArgs e)
        {
            MPE_Effort.Show();

            string Message = UC_Effort.ValidationEffort();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true; 
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            PLeadEffort Lead = new PLeadEffort();
            Lead = UC_Effort.ReadEffort();
            Lead.LeadEffortID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Effort", Lead)).Data); 
            fillEffort(Lead.LeadID);
        }
        protected void btnSaveExpense_Click(object sender, EventArgs e)
        {
            MPE_Expense.Show();
            string Message = UC_Expense.ValidationExpense();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            MPE_Expense.Show();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            PLeadExpense Lead = new PLeadExpense();
            Lead = UC_Expense.ReadExpense();
            Lead.LeadExpenseID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Expense", Lead)).Data);
            fillExpense(Lead.LeadID);
        }
        protected void lblFinancialAdd_Click(object sender, EventArgs e)
        {

            DropDownList ddlBankNameF = (DropDownList)gvFinancial.FooterRow.FindControl("ddlBankNameF");
            TextBox txtFinancePercentageF = (TextBox)gvFinancial.FooterRow.FindControl("txtFinancePercentageF");
            TextBox txtRemarkF = (TextBox)gvFinancial.FooterRow.FindControl("txtRemarkF");
            PLeadFinancial Lead = new PLeadFinancial();
            Lead.LeadFinancialID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            Lead.BankName = new PBankName { BankNameID = Convert.ToInt32(ddlBankNameF.SelectedValue) };
            Lead.FinancePercentage = Convert.ToDecimal(txtFinancePercentageF.Text.Trim());
            Lead.Remark = txtRemarkF.Text;
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID }; 

            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Financial", Lead)).Data);
            MPE_Financial.Show();
            fillFinancial(Lead.LeadID);
        }

        void fillViewLead(long LeadID)
        {
            
            PLeadSearch S = new PLeadSearch();
            S.LeadID = LeadID;
            PLead Lead = new BLead().GetLead(S)[0];
            lblLeadNumber.Text = Lead.LeadNumber;
            lblLeadDate.Text = Convert.ToString(Lead.LeadDate);
            lblCategory.Text = Lead.Category.Category;
            lblProgressStatus.Text = Lead.ProgressStatus.ProgressStatus;
            lblQualification.Text = Lead.Qualification.Qualification;
            lblSource.Text = Lead.Source.Source;
            lblStatus.Text = Lead.Status.Status;
            lblType.Text = Lead.Type.Type;
            lblDealer.Text = Lead.Dealer.DealerCode;
            lblRemarks.Text = Lead.Remarks;
            string Customer = Lead.Customer.CustomerCode + " " + Lead.Customer.CustomerName;
            lblCustomer.Text = Customer;
            lblContactPerson.Text = Lead.Customer.ContactPerson;
            lblMobile.Text = Lead.Customer.Mobile;
            lblEmail.Text = Lead.Customer.Email;

            string Location = Lead.Customer.Address1 + ", " + Lead.Customer.Address2 + ", " + Lead.Customer.District.District + ", " + Lead.Customer.State.State;
            lblLocation.Text = Location;
        }
        void fillAssignSalesEngineer(long LeadID)
        {
            List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID);

            if (SalesEngineer.Count == 0)
            {
                SalesEngineer.Add(new PLeadSalesEngineer());
            }

            gvSalesEngineer.DataSource = SalesEngineer;
            gvSalesEngineer.DataBind();

            //DropDownList ddlSalesEngineer = (DropDownList)gvSalesEngineer.FooterRow.FindControl("ddlSalesEngineer");

            //ddlSalesEngineer.DataTextField = "ContactName";
            //ddlSalesEngineer.DataValueField = "UserID";
            //ddlSalesEngineer.DataSource = new BUser().GetAllUsers();
            //ddlSalesEngineer.DataBind();
            new DDLBind((DropDownList)gvSalesEngineer.FooterRow.FindControl("ddlSalesEngineer"), new BUser().GetAllUsers(), "ContactName", "UserID");
        }
        void fillFollowUp(long LeadID)
        {
            List<PLeadFollowUp> FollowUp = new BLead().GetLeadFollowUp(LeadID, PSession.User.UserID);

            if (FollowUp.Count == 0)
            {
                FollowUp.Add(new PLeadFollowUp());
            }

            gvFollowUp.DataSource = FollowUp;
            gvFollowUp.DataBind();

            //DropDownList ddlSalesEngineer = (DropDownList)gvFollowUp.FooterRow.FindControl("ddlSalesEngineer");

            List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID);
            List<PUser> U = new List<PUser>();
            foreach (PLeadSalesEngineer SE in SalesEngineer)
            {
                U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            }
            //ddlSalesEngineer.DataTextField = "ContactName";
            //ddlSalesEngineer.DataValueField = "UserID";
            //ddlSalesEngineer.DataSource = U;
            //ddlSalesEngineer.DataBind();
            new DDLBind((DropDownList)gvFollowUp.FooterRow.FindControl("ddlSalesEngineer"), U, "ContactName", "UserID");
        }
        void fillConvocation(long LeadID)
        {
            List<PLeadConvocation> FollowUp = new BLead().GetLeadConvocation(LeadID, PSession.User.UserID);

            if (FollowUp.Count == 0)
            {
                FollowUp.Add(new PLeadConvocation());
            }

            gvConvocation.DataSource = FollowUp;
            gvConvocation.DataBind();
            List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID);
            List<PUser> U = new List<PUser>();
            foreach (PLeadSalesEngineer SE in SalesEngineer)
            {
                U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            }
            new DDLBind((DropDownList)gvConvocation.FooterRow.FindControl("ddlSalesEngineerF"), U, "ContactName", "UserID");
            new DDLBind((DropDownList)gvConvocation.FooterRow.FindControl("ddlProgressStatusF"), new BLead().GetLeadProgressStatus(null, null), "ProgressStatus", "ProgressStatusID");
        }
        void fillFinancial(long LeadID)
        {
            List<PLeadFinancial> FollowUp = new BLead().GetLeadFinancial(LeadID, PSession.User.UserID);

            if (FollowUp.Count == 0)
            {
                FollowUp.Add(new PLeadFinancial());
            }

            gvFinancial.DataSource = FollowUp;
            gvFinancial.DataBind();
            new DDLBind((DropDownList)gvFinancial.FooterRow.FindControl("ddlBankNameF"), new BDMS_Master().GetBankName(null, null), "BankName", "BankNameID");
        }
        void fillEffort(long LeadID)
        { 
            gvEffort.DataSource = new BLead().GetLeadEffort(LeadID, PSession.User.UserID);
            gvEffort.DataBind();
             

            List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID);
            List<PUser> U = new List<PUser>();
            foreach (PLeadSalesEngineer SE in SalesEngineer)
            {
                U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            }
            new DDLBind((DropDownList)UC_Effort.FindControl("ddlSalesEngineer"), U, "ContactName", "UserID");
        }
        void fillExpense(long LeadID)
        { 
            gvExpense.DataSource = new BLead().GetLeadExpense(LeadID, PSession.User.UserID);
            gvExpense.DataBind(); 

            List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID);
            List<PUser> U = new List<PUser>();
            foreach (PLeadSalesEngineer SE in SalesEngineer)
            {
                U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            } 
            new DDLBind((DropDownList)UC_Expense.FindControl("ddlSalesEngineer"), U, "ContactName", "UserID");
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
    }
}