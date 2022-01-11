using Business;
using DealerManagementSystem.ViewPreSale.UserControls;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class ColdVisits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales ~ Cold Visit');</script>");

            lblMessage.Text = "";
            if (!IsPostBack)
            { 
                List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                new DDLBind(ddlSCountry, Country, "Country", "CountryID");
                ddlSCountry.SelectedValue = "1";
                List<PDMS_State> State = new BDMS_Address().GetState(1, null, null, null);
                new DDLBind(ddlState, State, "State", "StateID"); 
                new DDLBind(ddlActionType, new BPreSale().GetActionType( null, null), "ActionType", "ActionTypeID");
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        { 
            DateTime? ColdVisitDateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? ColdVisitDateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());
          
            long? CustomerID = null;
            string CustomerCode = null;
            string CustomerName = txtCustomer.Text.Trim();
            string Mobile = txtMobile.Text.Trim(); 

            int? CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);
            int?   StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
            int? DistrictID = null;


            List<PColdVisit> Leads = new BColdVisit().GetColdVisit(null, ColdVisitDateFrom, ColdVisitDateTo, CustomerID, CustomerCode, CustomerName, Mobile, CountryID, StateID, DistrictID);
            gvLead.DataSource = Leads;
            gvLead.DataBind();
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlState, new BDMS_Address().GetState(Convert.ToInt32(ddlSCountry.SelectedValue), null, null, null), "State", "StateID");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string Message = UC_Customer.ValidationCustomer();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            MPE_Customer.Show();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }

            Message = ValidationColdVisit();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }

            PColdVisit ColdVisitList = new PColdVisit();
            ColdVisitList.ColdVisitDate = Convert.ToDateTime(txtColdVisitDate.Text.Trim());
            ColdVisitList.ActionType = new PActionType() { ActionTypeID = Convert.ToInt32(ddlActionType.SelectedValue) };
            ColdVisitList.Remark = txtRemark.Text.Trim();
            ColdVisitList.Customer = UC_Customer.ReadCustomer(); 
            ColdVisitList.CreatedBy = new PUser { UserID = PSession.User.UserID };
               string result = new BAPI().ApiPut("ColdVisit", ColdVisitList);
        }
        public string ValidationColdVisit()
        {
            string Message = "";
            Boolean Ret = true;

            txtColdVisitDate.BorderColor = Color.Silver;
            txtRemark.BorderColor = Color.Silver;
            ddlActionType.BorderColor = Color.Silver;
            if (string.IsNullOrEmpty(txtColdVisitDate.Text.Trim()))
            {
                Message = "Please enter the Cold Visi tDate";
                Ret = false;
                txtColdVisitDate.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Remark";
                Ret = false;
                txtRemark.BorderColor = Color.Red;
            }
            
            if (ddlActionType.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Action Type";
                Ret = false;
                ddlActionType.BorderColor = Color.Red;
            }
            

            return Message;
        }
        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            DropDownList ddlAction = (DropDownList)gvRow.FindControl("ddlAction");
            Label lblColdVisitID = (Label)gvRow.FindControl("lblColdVisitID");
            ViewState["ColdVisitID"] = lblColdVisitID.Text;

            if (ddlAction.Text == "Add Effort")
            {
                MPE_Effort.Show();
                fillEffort(Convert.ToInt64(lblColdVisitID.Text)); 
            }
            else if (ddlAction.Text == "Add Expense")
            {
                 MPE_Expense.Show();
                fillExpense(Convert.ToInt64(lblColdVisitID.Text));
            }
        }
 
        protected void btnSaveEffort_Click(object sender, EventArgs e)
        {
            string Message = UC_Effort.ValidationEffort();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            MPE_Effort.Show();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            } 
            PLeadEffort Effort = new PLeadEffort(); 
            Effort = UC_Effort.ReadEffort();
            Effort.LeadID = Convert.ToInt64(ViewState["ColdVisitID"]);
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit/Effort", Effort)).Data);
            fillEffort(Effort.LeadID);
        }

        protected void btnSaveExpense_Click(object sender, EventArgs e)
        {
            string Message = UC_Expense.ValidationExpense();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            MPE_Expense.Show();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            } 
            PLeadExpense Expense = new PLeadExpense();
            Expense = UC_Expense.ReadExpense();
            Expense.LeadID = Convert.ToInt64(ViewState["ColdVisitID"]);
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit/Expense", Expense)).Data);
            fillExpense(Expense.LeadID);
        }
        void fillEffort(long ColdVisitID)
        {   
            gvEffort.DataSource = new BColdVisit().GetColdVisitEffort(ColdVisitID, PSession.User.UserID); 
            gvEffort.DataBind();
             
        }
        void fillExpense(long ColdVisitID)
        { 
            
            gvExpense.DataSource = new BColdVisit().GetColdVisitExpense(ColdVisitID, PSession.User.UserID); 
            gvExpense.DataBind();
            
        }

        protected void lbViewCustomer_Click(object sender, EventArgs e)
        {
            divCustomerView.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            DropDownList ddlAction = (DropDownList)gvRow.FindControl("ddlAction");
            Label lblCustomerID = (Label)gvRow.FindControl("lblCustomerID");

            UC_CustomerView.fillCustomer(Convert.ToInt64(lblCustomerID.Text));
            //CustomerView ucCustomerView = (CustomerView)LoadControl("~/ViewPreSale/UserControls/CustomerView.ascx");
            //ucCustomerView.ID = "ucCustomerView";
          
            //PlaceHolder phDashboard = (PlaceHolder)tblDashboard.FindControl("ph_usercontrols_1");
            //phDashboard.Controls.Add(ucCustomerView);
            //ucCustomerView.fillCustomer(Convert.ToInt64(lblCustomerID.Text)); 
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divCustomerView.Visible = false;
            divList.Visible = true;
        }

        protected void btnAddColdVisit_Click(object sender, EventArgs e)
        {
            MPE_Customer.Show(); 
        }
    }
}