using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDashboard
{
    public partial class LeadChart : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        public class GraphData
        {
            public int Year { get; set; }
            public int Sales { get; set; }
            public int Expenses { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new DDLBind(ddlYDealer, PSession.User.Dealer, "CodeWithName", "DID");
                new DDLBind(ddlYCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
                new DDLBind(ddlMDealer, PSession.User.Dealer, "CodeWithName", "DID");
                new DDLBind(ddlMCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
                loadYearAndMonth();
            }
        }
        void loadYearAndMonth()
        {
            //ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            //for (int i = 2018; i <= DateTime.Now.Year; i++)
            //{
            //    ddlYear.Items.Insert(ddlYear.Items.Count, new ListItem(i.ToString(), i.ToString()));
            //}
            //ddlMonth.Items.Insert(0, new ListItem("Select", "0"));
            //for (int i = 1; i <= 12; i++)
            //{
            //    ddlMonth.Items.Insert(ddlMonth.Items.Count, new ListItem(i.ToString(), i.ToString()));
            //}
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "hwa", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(drawChart); ", true);
            ClientScript.RegisterStartupScript(GetType(), "hwa1", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(RegionWiseLeadStatusChart); ", true);
        }

        [WebMethod]
        public static List<object> GetChartData2(string country)
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "General", "Open", "Assign", "Quotation", "Won" });
            chartData.Add(new object[] { "9001", 10, 24, 32, 18 });
            chartData.Add(new object[] { "9002", 52, 20, 32, 18 });
            chartData.Add(new object[] { "9004", 10, 70, 32, 18 });
            chartData.Add(new object[] { "9005", 96, 50, 32, 18 });

            chartData.Add(new object[] { "9011", 40, 40, 32, 18 });
            chartData.Add(new object[] { "9012", 10, 35, 32, 18 });
            chartData.Add(new object[] { "9014", 38, 25, 32, 18 });
            chartData.Add(new object[] { "9015", 10, 24, 32, 18 });
            chartData.Add(new object[] { "9016", 74, 90, 32, 18 });
            chartData.Add(new object[] { "9017", 36, 70, 32, 18 });
            chartData.Add(new object[] { "9018", 10, 24, 32, 18 });
            chartData.Add(new object[] { "9019", 98, 60, 32, 18 });

            chartData.Add(new object[] { "9021", 10, 24, 32, 18 });
            chartData.Add(new object[] { "9022", 40, 24, 32, 18 });
            chartData.Add(new object[] { "9024", 10, 50, 32, 18 });
            chartData.Add(new object[] { "9025", 96, 24, 32, 18 });

            chartData.Add(new object[] { "9031", 10, 64, 32, 18 });
            chartData.Add(new object[] { "9032", 74, 37, 32, 18 });
            chartData.Add(new object[] { "9034", 10, 24, 32, 18 });
            chartData.Add(new object[] { "9035", 63, 70, 32, 18 });
            chartData.Add(new object[] { "9036", 10, 24, 32, 18 });
            chartData.Add(new object[] { "9037", 5, 24, 32, 18 });
            chartData.Add(new object[] { "9038", 10, 24, 32, 18 });
            chartData.Add(new object[] { "9039", 75, 24, 32, 18 });

            chartData.Add(new object[] { "9041", 10, 24, 32, 18 });
            chartData.Add(new object[] { "9042", 32, 24, 32, 18 });
            chartData.Add(new object[] { "9044", 10, 24, 32, 18 });
            chartData.Add(new object[] { "9045", 74, 24, 32, 18 });
            chartData.Add(new object[] { "9046", 65, 24, 32, 18 });
            chartData.Add(new object[] { "9047", 85, 24, 32, 18 });
            chartData.Add(new object[] { "9048", 10, 24, 32, 18 });
            chartData.Add(new object[] { "9049", 5, 24, 32, 18 });

            return chartData;


            //  var data = google.visualization.arrayToDataTable([
            //    ['Genre', 'Fantasy & Sci Fi', 'Romance', 'Mystery Crime', 'General', 'Western', 'Literature', { role: 'annotation' }],
            //    ['2010', 10, 24, 20, 32, 18, 5, ''],
            //    ['2020', 16, 22, 23, 30, 16, 9, ''],
            //    ['2030', 28, 19, 29, 30, 12, 13, '']
            //]);
        }

        [WebMethod]
        public static List<object> GetRegionWiseLeadStatus(string country)
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "General", "Open", "Assign", "Quotation", "Won" });
            chartData.Add(new object[] { "Western", 10, 24, 32, 18 });
            chartData.Add(new object[] { "Central", 52, 20, 32, 18 });
            chartData.Add(new object[] { "East", 10, 70, 32, 18 });
            chartData.Add(new object[] { "North", 96, 50, 32, 18 });

            chartData.Add(new object[] { "South1", 40, 40, 32, 18 });
            chartData.Add(new object[] { "South2", 10, 35, 32, 18 });

            return chartData;
            //  var data = google.visualization.arrayToDataTable([
            //    ['Genre', 'Fantasy & Sci Fi', 'Romance', 'Mystery Crime', 'General', 'Western', 'Literature', { role: 'annotation' }],
            //    ['2010', 10, 24, 20, 32, 18, 5, ''],
            //    ['2020', 16, 22, 23, 30, 16, 9, ''],
            //    ['2030', 28, 19, 29, 30, 12, 13, '']
            //]);
        }

        protected void ddlMCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlMRegion, new BDMS_Address().GetRegion(ddlMCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMCountry.SelectedValue), null, null), "Region", "RegionID");
        }

        protected void ddlYCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlYRegion, new BDMS_Address().GetRegion(ddlYCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlYCountry.SelectedValue), null, null), "Region", "RegionID");
        }
    }
}