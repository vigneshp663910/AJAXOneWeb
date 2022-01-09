using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class CustomerSearch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            gvCustomer.DataSource = new BDMS_Customer().GetCustomerProspect(null, "", txtCustomer.Text.Trim(), "", null, null, null);
            gvCustomer.DataBind();
        }

        [WebMethod]
        public static List<string> GetSearch(string prefixText)
        { 
             
            return new List<string>();
        }
    }
}