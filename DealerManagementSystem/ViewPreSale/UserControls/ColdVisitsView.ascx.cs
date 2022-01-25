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
    public partial class ColdVisitsView : System.Web.UI.UserControl
    {
        public long ColdVisitID
        {
            get
            {
                if (Session["ColdVisitID"] == null)
                {
                    Session["ColdVisitID"] = 0;
                }
                return Convert.ToInt64(Session["ColdVisitID"]);
            }
            set
            {
                Session["ColdVisitID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageEffort.Text = "";
            lblMessageExpense.Text = "";
        }
        public void fillViewColdVisit(long ColdVisitID)
        {
            this.ColdVisitID = ColdVisitID;
             
            PColdVisit Lead = new BColdVisit().GetColdVisit(ColdVisitID,null, null, null, null, null, null, null, null, null)[0];
            lblLeadNumber.Text = Lead.ColdVisitNumber;
            lblLeadDate.Text = Convert.ToString(Lead.ColdVisitDate); 
          //  lblRemarks.Text = Lead.Remarks;
            string Customer = Lead.Customer.CustomerCode + " " + Lead.Customer.CustomerName;
            lblCustomer.Text = Customer;
            lblContactPerson.Text = Lead.Customer.ContactPerson;
            lblMobile.Text = Lead.Customer.Mobile;
            lblEmail.Text = Lead.Customer.Email;

            string Location = Lead.Customer.Address1 + ", " + Lead.Customer.Address2 + ", " + Lead.Customer.District.District + ", " + Lead.Customer.State.State;
            lblLocation.Text = Location;
            fillEffort();
            fillExpense();
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);

            if (lbActions.Text == "Add Effort")
            {
                MPE_Effort.Show(); 
            }
            else if (lbActions.Text == "Add Expense")
            {
                MPE_Expense.Show(); 
            }
        }

        protected void btnSaveEffort_Click(object sender, EventArgs e)
        {
            string Message = UC_Effort.ValidationEffort();
            lblMessageEffort.ForeColor = Color.Red;
            lblMessageEffort.Visible = true; 
            if (!string.IsNullOrEmpty(Message))
            {
                MPE_Effort.Show();
                lblMessageEffort.Text = Message;
                return;
            }
            PLeadEffort Effort = new PLeadEffort();
            Effort = UC_Effort.ReadEffort();
            Effort.LeadID = ColdVisitID;
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit/Effort", Effort)).Data);
            fillEffort(); 
        }

        protected void btnSaveExpense_Click(object sender, EventArgs e)
        {
            string Message = UC_Expense.ValidationExpense();
            lblMessageExpense.ForeColor = Color.Red;
            lblMessageExpense.Visible = true;
            
            if (!string.IsNullOrEmpty(Message))
            {
                MPE_Expense.Show();
                lblMessageExpense.Text = Message;
                return;
            }
            PLeadExpense Expense = new PLeadExpense();
            Expense = UC_Expense.ReadExpense();
            Expense.LeadID = ColdVisitID;
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit/Expense", Expense)).Data);
            fillExpense();
        }
        void fillEffort()
        {
            gvEffort.DataSource = new BColdVisit().GetColdVisitEffort(ColdVisitID, PSession.User.UserID);
            gvEffort.DataBind();

        }
        void fillExpense()
        {

            gvExpense.DataSource = new BColdVisit().GetColdVisitExpense(ColdVisitID, PSession.User.UserID);
            gvExpense.DataBind();

        } 
    }
}