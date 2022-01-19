using Business;
using DealerManagementSystem.ViewPreSale.UserControls;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class ColdVisits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Cold Visit');</script>");

            lblMessage.Text = "";
            lblMessageColdVisit.Text = "";
            lblMessageEffort.Text = "";
            lblMessageExpense.Text = "";
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
            FillClodVisit();
        }
        void FillClodVisit()
        {
            DateTime? ColdVisitDateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? ColdVisitDateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());

            long? CustomerID = null;
            string CustomerCode = null;
            string CustomerName = txtCustomer.Text.Trim();
            string Mobile = txtMobile.Text.Trim();

            int? CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);
            int? StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
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
            MPE_Customer.Show();
            PColdVisit ColdVisitList = new PColdVisit();
            lblMessageColdVisit.ForeColor = Color.Red;
            lblMessageColdVisit.Visible = true;
            string Message = "";
           // TextBox txtCustomerID = (TextBox)UC_Customer.FindControl("txtCustomerID");
            if (!string.IsNullOrEmpty(txtCustomerID.Text.Trim()))
            {
                ColdVisitList.Customer = new PDMS_Customer();
                ColdVisitList.Customer.CustomerID = Convert.ToInt64(txtCustomerID.Text.Trim());
            }
            else
            {
                Message = UC_Customer.ValidationCustomer();               
               
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessageColdVisit.Text = Message;
                    return;
                }
                ColdVisitList.Customer = UC_Customer.ReadCustomer();
            }

            Message = ValidationColdVisit();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageColdVisit.Text = Message;
                return;
            } 
           
            ColdVisitList.ColdVisitDate = Convert.ToDateTime(txtColdVisitDate.Text.Trim());
            ColdVisitList.ActionType = new PActionType() { ActionTypeID = Convert.ToInt32(ddlActionType.SelectedValue) };
            ColdVisitList.Remark = txtRemark.Text.Trim();
           
            ColdVisitList.CreatedBy = new PUser { UserID = PSession.User.UserID };
               string result = new BAPI().ApiPut("ColdVisit", ColdVisitList);

            result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data);
            if (result == "0")
            {
                MPE_Customer.Show();
                lblMessageColdVisit.Text = "Customer is not updated successfully ";
                return;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Customer is updated successfully ";
            }
            List<PColdVisit> Leads = new BColdVisit().GetColdVisit(Convert.ToInt64(result), null, null, null, null, null, null, null, null, null);
            gvLead.DataSource = Leads;
            gvLead.DataBind();
            UC_Customer.FillClean();
            MPE_Customer.Hide();
        }
        public string ValidationColdVisit()
        {
            string Message = ""; 
            txtColdVisitDate.BorderColor = Color.Silver;
            txtRemark.BorderColor = Color.Silver;
            ddlActionType.BorderColor = Color.Silver;
            if (string.IsNullOrEmpty(txtColdVisitDate.Text.Trim()))
            {
                Message = "Please enter the Cold Visit Date"; 
                txtColdVisitDate.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Remark"; 
                txtRemark.BorderColor = Color.Red;
            }
            
            if (ddlActionType.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Action Type"; 
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
            lblMessageEffort.ForeColor = Color.Red;
            lblMessageEffort.Visible = true;
            MPE_Effort.Show();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageEffort.Text = Message;
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
            lblMessageExpense.ForeColor = Color.Red;
            lblMessageExpense.Visible = true;
            MPE_Expense.Show();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageExpense.Text = Message;
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
                    +"<table><tr><td>"
                    +"<label id='lblCustomerName" + i + "'>" + cust.CustomerName + "</label></td><td>Prospect</td></tr >"   + "<tr><td>"
                    +"<label id='lblContactPerson" + i + "'>" + cust.ContactPerson + "</label></td><td>"
                    + "<label id='lblMobile" + i + "'>" + cust.Mobile + " </td></tr></ table >";
                Emp.Add(div);
            }
            return Emp;
        }



        protected void gvLead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        { 
            gvLead.PageIndex = e.NewPageIndex;
            FillClodVisit();
        }
    }
}