using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDashboard.UserControls
{
    public partial class CustomerSatisfactionInAfterSalesSupport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int? DealerID = (int?)Session["SerDealerID"];
            DateTime? DateFrom = (DateTime?)Session["SerDateFrom"];
            DateTime? DateTo = (DateTime?)Session["SerDateTo"];


            DataSet ds = new BDMS_MTTR().GetDashCustomerSatisfactionInAfterSalesSupport(DealerID, DateFrom, DateTo, PSession.User.UserID);
            lblMTTR1.Text = ds.Tables[0].Rows[0][0].ToString();
            lblMTTR2.Text = ds.Tables[1].Rows[0][0].ToString();
            lblFTRWS.Text = ds.Tables[2].Rows[0][0].ToString();
            lblWPAD.Text = ds.Tables[3].Rows[0][0].ToString();
        }
    }
}