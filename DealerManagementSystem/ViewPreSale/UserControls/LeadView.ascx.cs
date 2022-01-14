using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class LeadView : System.Web.UI.UserControl
    {
        public long LeadID
        {
            get
            {
                if (Session["LeadID"] == null)
                {
                    Session["LeadID"] = 0;
                }
                return Convert.ToInt64(Session["LeadID"]);
            }
            set
            {
                Session["LeadID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       public void fillViewLead(long LeadID)
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

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Assign")
            {
                MP_AssignSE.Show();
                fillAssignSalesEngineer(LeadID);
            }
            else if (lbActions.Text == "Add Follow-up")
            {
                MPE_FollowUp.Show();
                fillFollowUp(LeadID);
            }
            else if (lbActions.Text == "Customer Convocation")
            {
                MPE_Convocation.Show();
                fillConvocation(LeadID);
            }
            else if (lbActions.Text == "Edit Financial Info")
            {
                MPE_Financial.Show();
                fillFinancial(LeadID);
            }
            else if (lbActions.Text == "Add Effort")
            {
                MPE_Effort.Show();
                fillEffort(LeadID);
            }
            else if (lbActions.Text == "Add Expense")
            {
                MPE_Expense.Show();
                fillExpense(LeadID);
            }
        }

        protected void lblSalesEngineerAdd_Click(object sender, EventArgs e)
        {
            DropDownList ddlSalesEngineer = (DropDownList)gvSalesEngineer.FooterRow.FindControl("ddlSalesEngineer");
            PLeadSalesEngineer Lead = new PLeadSalesEngineer();
            Lead.LeadSalesEngineerID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            Lead.SalesEngineer = new PUser { UserID = Convert.ToInt32(ddlSalesEngineer.SelectedValue) };
            Lead.AssignedBy = new PUser { UserID = PSession.User.UserID };
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
            Lead.ProgressStatus = new PLeadProgressStatus() { ProgressStatusID = Convert.ToInt32(ddlProgressStatusF.SelectedValue) };
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
    }
}