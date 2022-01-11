using Business;
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
                grd.DataSource = new BDMS_Address().GetCountry(null, null);
                grd.DataBind();

                gvRelation.DataSource = new BDMS_Address().GetCountry(null, null);
                gvRelation.DataBind();
            }
        }

        [WebMethod]
        public static List<string> GetEmpNames(string empName)
        {
            List<string> Emp = new List<string>();
            Emp.Add("abc");
            Emp.Add("abcd");
            Emp.Add("abcde");
            Emp.Add("abtf");
            return Emp;
        }

        protected void grd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}