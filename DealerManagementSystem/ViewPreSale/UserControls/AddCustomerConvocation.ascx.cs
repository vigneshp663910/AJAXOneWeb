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
    public partial class AddCustomerConvocation : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
            }
        }
        public void FillMaster(long LeadID)
        {
            List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID);
            List<PUser> U = new List<PUser>();
            foreach (PLeadSalesEngineer SE in SalesEngineer)
            {
                U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            }
            new DDLBind(ddlSalesEngineer, U, "ContactName", "UserID");
            new DDLBind( ddlProgressStatus , new BLead().GetLeadProgressStatus(null, null), "ProgressStatus", "ProgressStatusID"); 
        } 
        public PLeadConvocation ReadConvocation()
        {
            PLeadConvocation Lead = new PLeadConvocation();
            Lead.LeadConvocationID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            Lead.SalesEngineer = new PUser { UserID = Convert.ToInt32(ddlSalesEngineer.SelectedValue) };
            Lead.ProgressStatus = new PLeadProgressStatus() { ProgressStatusID = Convert.ToInt32(ddlProgressStatus.SelectedValue) };
            Lead.ConvocationDate = Convert.ToDateTime(txtConvocationDate.Text.Trim());
            Lead.Convocation = txtConvocation.Text.Trim(); 
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };

            return Lead;
        }

        public string ValidationConvocation()
        {
            string Message = "";
            ddlSalesEngineer.BorderColor = Color.Silver;
            ddlProgressStatus.BorderColor = Color.Silver; 
            txtConvocationDate.BorderColor = Color.Silver;
            txtConvocation.BorderColor = Color.Silver;

            if (ddlSalesEngineer.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Sales Engineer";
                ddlSalesEngineer.BorderColor = Color.Red;
            } 
            if (ddlProgressStatus.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Progress Status";
                ddlProgressStatus.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtConvocationDate.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Date";
                txtConvocationDate.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtConvocation.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Convocation";
                txtConvocation.BorderColor = Color.Red;
            }
            return Message;
        }
    }
}