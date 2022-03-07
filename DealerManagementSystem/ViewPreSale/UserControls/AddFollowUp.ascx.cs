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
    public partial class AddFollowUp : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
               
            }
        }
        public void FillMaster()
        {
            cxFollowUpDate.StartDate = DateTime.Now;
            //List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID, true);
            //List<PUser> U = new List<PUser>();
            //foreach (PLeadSalesEngineer SE in SalesEngineer)
            //{
            //    U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            //}
            //new DDLBind(ddlSalesEngineer, U, "ContactName", "UserID");
        }
        //protected void lblFollowUpAdd_Click(object sender, EventArgs e)
        //{
        //    PLeadFollowUp Lead = new PLeadFollowUp();
        //    Lead.LeadFollowUpID = 0;
        //    Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
        //    Lead.FollowUpDate = Convert.ToDateTime(txtFollowUpDate.Text.Trim());
        //    Lead.FollowUpNote = txtFollowUpNote.Text.Trim();
        //    Lead.SalesEngineer = new PUser { UserID = Convert.ToInt32(ddlSalesEngineer.SelectedValue) };
        //    Lead.CreatedBy = new PUser { UserID = PSession.User.UserID }; 
        //} 
        public PLeadFollowUp ReadFollowUp()
        {
            PLeadFollowUp Lead = new PLeadFollowUp();
            Lead.LeadFollowUpID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            Lead.FollowUpDate = Convert.ToDateTime(txtFollowUpDate.Text.Trim());
            Lead.FollowUpNote = txtFollowUpNote.Text.Trim();
            Lead.SalesEngineer = new PUser { UserID = Convert.ToInt32(ddlSalesEngineer.SelectedValue) };
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };
            return Lead;
        }

        public string ValidationFollowUp()
        {
            string Message = "";
            ddlSalesEngineer.BorderColor = Color.Silver;
            txtFollowUpDate.BorderColor = Color.Silver;
            txtFollowUpNote.BorderColor = Color.Silver; 

            if (ddlSalesEngineer.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Sales Engineer";
                ddlSalesEngineer.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtFollowUpDate.Text.Trim()))
            {
                Message = "Please enter the Follow Up Date";
                txtFollowUpDate.BorderColor = Color.Red;
            } 
            if (string.IsNullOrEmpty(txtFollowUpNote.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Remark";
                txtFollowUpNote.BorderColor = Color.Red;
            }
            return Message;
        }
    }
}