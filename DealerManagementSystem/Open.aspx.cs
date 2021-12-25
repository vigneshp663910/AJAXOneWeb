using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public partial class Open : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAPITest_Click(object sender, EventArgs e)
        {
          //  new BAPI().GetServicePriority1();
            new BAPI().Main1();
        }
    }
}