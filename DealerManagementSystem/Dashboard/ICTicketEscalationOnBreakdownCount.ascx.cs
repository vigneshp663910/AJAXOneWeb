using Business;
using Properties;
using System;
using System.Data;

namespace DealerManagementSystem.Dashboard
{
    public partial class ICTicketEscalationOnBreakdownCount : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int? DealerID = (int?)Session["SerDealerID"];
            DateTime? DateFrom = (DateTime?)Session["SerDateFrom"];
            DateTime? DateTo = (DateTime?)Session["SerDateTo"];


            DataSet ds = new BDMS_MTTR().GetMTTREscalationOnBreakdownCount(DealerID, DateFrom, DateTo, PSession.User.UserID);
            lblBreakDown8.Text = ds.Tables[0].Rows[0][0].ToString();
            lblBreakDown24.Text = ds.Tables[1].Rows[0][0].ToString();
            lblBreakDown48.Text = ds.Tables[2].Rows[0][0].ToString();
            lblBreakDown72.Text = ds.Tables[3].Rows[0][0].ToString();
        }

        protected void lbBreakDownMoreThan8_Click(object sender, EventArgs e)
        {
            Server.TransferRequest("Dashboard/DMS_ICTicketEscalationOnBreakdownLevel1.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}