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
    public partial class Expense : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new DDLBind(ddlExpenseType, new BDMS_Master().GetExpenseType(null, null), "ExpenseType", "ExpenseTypeID");
                new DDLBind(ddlSalesEngineer, new BUser().GetUsers(null, "", null, ""), "ContactName", "UserID");
            }
        }
        protected void lblEffortAdd_Click(object sender, EventArgs e)
        {
            PLeadEffort Lead = new PLeadEffort(); 
            Lead.SalesEngineer = new PUser { UserID = Convert.ToInt32(ddlSalesEngineer.SelectedValue) };

            Lead.EffortDate = Convert.ToDateTime(txtExpenseDate.Text.Trim());
            Lead.EffortStartTime = Convert.ToDecimal(txtAmount.Text.Trim()); 

            Lead.EffortType = new PEffortType { EffortTypeID = Convert.ToInt32(ddlExpenseType.SelectedValue) };
            Lead.Remark = txtRemark.Text; 
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };
        }

        

        public PLeadExpense ReadExpense()
        {
            PLeadExpense Lead = new PLeadExpense();
            Lead.SalesEngineer = new PUser { UserID = Convert.ToInt32(ddlSalesEngineer.SelectedValue) }; 
            Lead.ExpenseDate = Convert.ToDateTime(txtExpenseDate.Text.Trim());
            Lead.Amount = Convert.ToDecimal(txtAmount.Text.Trim()); 
            Lead.ExpenseType = new PExpenseType { ExpenseTypeID = Convert.ToInt32(ddlExpenseType.SelectedValue) };
            Lead.Remark = txtRemark.Text;
            
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };
            return Lead;
        }

        public string ValidationExpense()
        {
            string Message = ""; 
            ddlSalesEngineer.BorderColor = Color.Silver;
            txtExpenseDate.BorderColor = Color.Silver;
            ddlExpenseType.BorderColor = Color.Silver;
            txtAmount.BorderColor = Color.Silver;  
            txtRemark.BorderColor = Color.Silver;

            if (ddlSalesEngineer.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Sales Engineer"; 
                ddlSalesEngineer.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtExpenseDate.Text.Trim()))
            {
                Message = "Please enter the Expense Date"; 
                txtExpenseDate.BorderColor = Color.Red;
            }
            if (ddlExpenseType.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Expense Type"; 
                ddlExpenseType.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtAmount.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Amount"; 
                txtAmount.BorderColor = Color.Red;
            } 
            if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Remark"; 
                txtRemark.BorderColor = Color.Red;
            }
            return Message;
        }
    }
}