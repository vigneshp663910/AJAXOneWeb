using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class CustomerAutocomplete : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
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
                string div = "<label id='lblCustomerID" + i + "' style='display: none'>" + cust.CustomerID + "</label><table><tr><td><label id='lblCustomerName" + i + "'>" + cust.CustomerName + "</label></td><td>Prospect</td></tr >"
           + "<tr><td>" + cust.ContactPerson + "</td><td>" + cust.Mobile + "</td></tr></ table >";
                Emp.Add(div);
            }
            return Emp;
        }
    }
}