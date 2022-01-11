using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class ModelTemplateDesign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillGridCountry();
        }
        private void FillGridCountry()
        {
            try
            {
                List<PDMS_Country> MML = new BDMS_Address().GetCountry(null, null);
                gvCountry.DataSource = MML;
                gvCountry.DataBind();
            }
            catch (Exception Ex)
            {
            }
        }
    }
}