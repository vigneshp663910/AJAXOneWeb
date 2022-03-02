using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class Application : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new DDLBind(ddlMainModule, new BDMS_Service().GetMainApplication(null, null), "MainApplication", "MainApplicationID", true, "All Application");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void btnRetrieve_Click(object sender, EventArgs e)
        {

        }
    }
}