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
    public partial class AssignSE : System.Web.UI.UserControl
    {
        public void FillMaster(PLead Lead)
        {
            List<PUser> User = new BUser().GetUsers(null, null, null, null, Lead.Dealer.DealerID, true, null);
            new DDLBind(ddlSalesEngineer, User, "ContactName", "UserID");
        }
        public PLeadSalesEngineer ReadAssignSE()
        {
            PLeadSalesEngineer Lead = new PLeadSalesEngineer();
            Lead.SalesEngineer = new PUser { UserID = Convert.ToInt32(ddlSalesEngineer.SelectedValue) };
            Lead.AssignedBy = new PUser { UserID = PSession.User.UserID };
            Lead.IsActive = true;

            return Lead;
        }

        public string ValidationAssignSE()
        {
            string Message = "";
            ddlSalesEngineer.BorderColor = Color.Silver;
            if (ddlSalesEngineer.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Sales Engineer";
                ddlSalesEngineer.BorderColor = Color.Red;
            }

            return Message;
        }
    }
}