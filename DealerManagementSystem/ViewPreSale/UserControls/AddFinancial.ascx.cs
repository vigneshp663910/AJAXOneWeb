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
    public partial class AddFinancial : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
        public void FillMaster()
        {
            new DDLBind(ddlBankName, new BDMS_Master().GetBankName(null, null), "BankName", "BankNameID");
        }
        public PLeadFinancial ReadFinancial()
        { 
            PLeadFinancial Lead = new PLeadFinancial();
            Lead.LeadFinancialID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            Lead.BankName = new PBankName { BankNameID = Convert.ToInt32(ddlBankName.SelectedValue) };
            Lead.FinancePercentage = Convert.ToDecimal(txtFinancePercentage.Text.Trim());
            Lead.Remark = txtRemark.Text;
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };

            return Lead;
        }

        public string ValidationFinancial()
        {
            string Message = "";
            ddlBankName.BorderColor = Color.Silver;
            txtFinancePercentage.BorderColor = Color.Silver;
            txtRemark.BorderColor = Color.Silver; 

            if (ddlBankName.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Bank Name";
                ddlBankName.BorderColor = Color.Red;
            }           
            else if (string.IsNullOrEmpty(txtFinancePercentage.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Finance Percentage";
                txtFinancePercentage.BorderColor = Color.Red;
            }
            else if(string.IsNullOrEmpty(txtRemark.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Remark";
                txtRemark.BorderColor = Color.Red;
            }
            return Message;
        }
    }
}