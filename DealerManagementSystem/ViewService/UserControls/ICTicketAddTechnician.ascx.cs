using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService.UserControls
{
    public partial class ICTicketAddTechnician : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster(int DealerID)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null,DealerID, true, null, null, 4); 
            new DDLBind(ddlDealerSalesEngineer, DealerUser, "ContactName", "UserID");  
        }
        public int ReadAssignSE()
        { 
            return Convert.ToInt32(ddlDealerSalesEngineer.SelectedValue);
        }
        public string ValidationAssignSE()
        {
            string Message = "";
            ddlDealerSalesEngineer.BorderColor = Color.Silver;
            if (ddlDealerSalesEngineer.SelectedValue == "0") 
            {
                Message = Message + "Please select the Sales Engineer";
                ddlDealerSalesEngineer.BorderColor = Color.Red;
            }
            return Message;
        }
    }
}