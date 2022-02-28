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
    public partial class Effort : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }

        public void FillMaster(List<PUser> User)
        {
            new DDLBind(ddlEffortType, new BDMS_Master().GetEffortType(null, null), "EffortType", "EffortTypeID");
            new DDLBind(ddlSalesEngineer, User, "ContactName", "UserID");
        }

        protected void lblEffortAdd_Click(object sender, EventArgs e)
        {  
            PLeadEffort Lead = new PLeadEffort();
            Lead.LeadEffortID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            Lead.SalesEngineer = new PUser { UserID = Convert.ToInt32(ddlSalesEngineer.SelectedValue) };
          
            Lead.EffortDate = Convert.ToDateTime(txtEffortDate.Text.Trim()); 
            Lead.EffortStartTime = Convert.ToDecimal(txtEffortStartTime.Text.Trim().Replace(':', '.'));
            Lead.EffortEndTime = Convert.ToDecimal(txtEffortEndTime.Text.Trim().Replace(':', '.'));

            Lead.EffortType = new PEffortType { EffortTypeID = Convert.ToInt32(ddlEffortType.SelectedValue) };
            Lead.Remark = txtRemark.Text;
            long EffortStartTime = (Convert.ToInt32(Lead.EffortStartTime) * 60) + (int)(((decimal)Lead.EffortStartTime % 1) * 100);
            long EffortEndTime = (Convert.ToInt32(Lead.EffortEndTime) * 60) + (int)(((decimal)Lead.EffortEndTime % 1) * 100);
            long Effort = EffortEndTime - EffortStartTime;
            Lead.Effort = Convert.ToInt32(Effort / 60) + Convert.ToDecimal(((Effort % 60) / 100.00));

            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };  
        }

        

        public PLeadEffort ReadEffort()
        {
            PLeadEffort Lead = new PLeadEffort(); 
            Lead.SalesEngineer = new PUser { UserID = Convert.ToInt32(ddlSalesEngineer.SelectedValue) };

            Lead.EffortDate = Convert.ToDateTime(txtEffortDate.Text.Trim());
            Lead.EffortStartTime = Convert.ToDecimal(txtEffortStartTime.Text.Trim().Replace(':', '.'));
            Lead.EffortEndTime = Convert.ToDecimal(txtEffortEndTime.Text.Trim().Replace(':', '.'));

            Lead.EffortType = new PEffortType { EffortTypeID = Convert.ToInt32(ddlEffortType.SelectedValue) };
            Lead.Remark = txtRemark.Text;
            long EffortStartTime = (Convert.ToInt32(Lead.EffortStartTime) * 60) + (int)(((decimal)Lead.EffortStartTime % 1) * 100);
            long EffortEndTime = (Convert.ToInt32(Lead.EffortEndTime) * 60) + (int)(((decimal)Lead.EffortEndTime % 1) * 100);
            long Effort = EffortEndTime - EffortStartTime;
            Lead.Effort = Convert.ToInt32(Effort / 60) + Convert.ToDecimal(((Effort % 60) / 100.00));

            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };
            return Lead;
        }

        public string ValidationEffort()
        {
            string Message = "";  
            ddlSalesEngineer.BorderColor = Color.Silver;
            txtEffortDate.BorderColor = Color.Silver;
            txtEffortStartTime.BorderColor = Color.Silver;
            txtEffortEndTime.BorderColor = Color.Silver;
            ddlEffortType.BorderColor = Color.Silver;
            txtRemark.BorderColor = Color.Silver; 

            if (ddlSalesEngineer.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Sales Engineer"; 
                ddlSalesEngineer.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtEffortDate.Text.Trim()))
            {
                Message = "Please enter the Effort Date"; 
                txtEffortDate.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtEffortStartTime.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Effort Start Time"; 
                txtEffortStartTime.BorderColor = Color.Red;
            } 
            if (string.IsNullOrEmpty(txtEffortEndTime.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Effort End Time"; 
                txtEffortEndTime.BorderColor = Color.Red;
            } 
            if (ddlEffortType.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Effort Type"; 
                ddlEffortType.BorderColor = Color.Red;
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