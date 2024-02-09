using DealerManagementSystem.Dashboard;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketEscalationOnBreakdown :  BasePage
    {
        private BasePage _page;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SerDealerID"] != null)
                {
                    ddlDealer.SelectedValue = Convert.ToString((int)Session["SerDealerID"]);
                }

                if (Session["SerDateFrom"] != null)
                {
                    txtDateFrom.Text = Convert.ToString(Session["SerDateFrom"]);
                }
                if (Session["SerDateTo"] != null)
                {
                    txtDateTo.Text = Convert.ToString(Session["SerDateTo"]);
                } 
                fillDealer();
            }

            fill(this);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {


            //  fill(this);
        }

        void fill(BasePage page)
        {

            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            DateTime? DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());

            Session["SerDealerID"] = DealerID;
            Session["SerDateFrom"] = DateFrom;
            Session["SerDateTo"] = DateTo;

            _page = page;
            int seq = 1;
            PlaceHolder phDashboard = (PlaceHolder)tblDashboard.FindControl("ph_usercontrols_" + seq++.ToString());

            ICTicketEscalationOnBreakdownLevel ucICTicketEscalationOnBreakdownLevel1 = (ICTicketEscalationOnBreakdownLevel)_page.LoadControl("~/Dashboard/ICTicketEscalationOnBreakdownLevel.ascx");
            ucICTicketEscalationOnBreakdownLevel1.ID = "ucICTicketEscalationOnBreakdownLevel1";
            ucICTicketEscalationOnBreakdownLevel1.DashboardControlID = 1;
            phDashboard.Controls.Add(ucICTicketEscalationOnBreakdownLevel1);

            seq = 2;
            phDashboard = (PlaceHolder)tblDashboard.FindControl("ph_usercontrols_" + seq++.ToString());
            ICTicketEscalationOnBreakdownLevel ucICTicketEscalationOnBreakdownLevel2 = (ICTicketEscalationOnBreakdownLevel)_page.LoadControl("~/Dashboard/ICTicketEscalationOnBreakdownLevel.ascx");
            ucICTicketEscalationOnBreakdownLevel2.ID = "ucICTicketEscalationOnBreakdownLevel2";
            ucICTicketEscalationOnBreakdownLevel2.DashboardControlID = 2;
            phDashboard.Controls.Add(ucICTicketEscalationOnBreakdownLevel2);

            seq = 3;
            phDashboard = (PlaceHolder)tblDashboard.FindControl("ph_usercontrols_" + seq++.ToString());
            ICTicketEscalationOnBreakdownLevel ucICTicketEscalationOnBreakdownLevel3 = (ICTicketEscalationOnBreakdownLevel)_page.LoadControl("~/Dashboard/ICTicketEscalationOnBreakdownLevel.ascx");
            ucICTicketEscalationOnBreakdownLevel3.ID = "ucICTicketEscalationOnBreakdownLevel3";
            ucICTicketEscalationOnBreakdownLevel3.DashboardControlID = 3;
            phDashboard.Controls.Add(ucICTicketEscalationOnBreakdownLevel3);

            seq = 4;
            phDashboard = (PlaceHolder)tblDashboard.FindControl("ph_usercontrols_" + seq++.ToString());
            ICTicketEscalationOnBreakdownLevel ucICTicketEscalationOnBreakdownLevel4 = (ICTicketEscalationOnBreakdownLevel)_page.LoadControl("~/Dashboard/ICTicketEscalationOnBreakdownLevel.ascx");
            ucICTicketEscalationOnBreakdownLevel4.ID = "ucICTicketEscalationOnBreakdownLevel4";
            ucICTicketEscalationOnBreakdownLevel4.DashboardControlID = 4;
            phDashboard.Controls.Add(ucICTicketEscalationOnBreakdownLevel4);
        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();

            ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        }
    }
}