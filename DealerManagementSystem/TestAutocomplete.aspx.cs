using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public partial class TestAutocomplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //grd.DataSource = new BDMS_Address().GetCountry(null, null);
                //grd.DataBind();

                //gvRelation.DataSource = new BDMS_Address().GetCountry(null, null);
                //gvRelation.DataBind();
            }
        }

        [WebMethod]
        public static List<string> GetEmpNames(string empName)
        {
            List<string> Emp = new List<string>();

            List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerAutocomplete(empName);
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


        //[WebMethod]
        //public static string GetEmpNames(string empName)
        //{
        //    string Emp = "";

        //    for (int i = 1; i <= 5; i++)
        //    {
        //        string div = "<div id='div" + i + "'><label id='lblCustomerID" + i + "'>John</label><table><tr><td><label id='lblCustomerName" + i + "'>John</label></td><td>Prospect</td></tr >"
        //   + "<tr><td>Peter</td><td>900000000</td></tr></ table > </ div >";
        //        Emp = Emp + div;
        //    }
        //    return Emp;
        //}

        protected void grd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ss = txtEmpName.Text;
        }
    }
}