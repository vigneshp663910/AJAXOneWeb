using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Lead.Dealer.DealerID, true, null);

            new DDLBind(ddlDealerSalesEngineer, DealerUser, "ContactName", "UserID");

            int AjaxDealerID = Convert.ToInt32(ConfigurationManager.AppSettings["AjaxDealerID"]);
            List<PUser> AjaxUser = new BUser().GetUsers(null, null, null, null, AjaxDealerID, true, null);

            new DDLBind(ddlAjaxSalesEngineer, AjaxUser, "ContactName", "UserID");
        }
        public PLeadSalesEngineer ReadAssignSE()
        {
            PLeadSalesEngineer Lead = new PLeadSalesEngineer();
            Lead.SalesEngineer = new PUser { UserID = ddlDealerSalesEngineer.SelectedValue == "0" ? Convert.ToInt32(ddlAjaxSalesEngineer.SelectedValue) :Convert.ToInt32(ddlDealerSalesEngineer.SelectedValue) };
            Lead.AssignedBy = new PUser { UserID = PSession.User.UserID };
            Lead.IsActive = true; 
            return Lead;
        } 
        public string ValidationAssignSE()
        {
            string Message = "";
            ddlDealerSalesEngineer.BorderColor = Color.Silver;
            if ((ddlDealerSalesEngineer.SelectedValue == "0") && (ddlAjaxSalesEngineer.SelectedValue == "0"))
            {
                Message = Message + "<br/>Please select the Sales Engineer";
                ddlDealerSalesEngineer.BorderColor = Color.Red;
            }
            return Message;
        }
    }
}