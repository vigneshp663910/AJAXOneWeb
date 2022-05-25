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
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
                new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
                loadYearAndMonth();
            }
        }
        [WebMethod]
        public static List<object> GetChartData2(string country)
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "General", "Open", "Assign", "Quotation", "Won" });
            chartData.Add(new object[] { "9001", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9002", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9004", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9005", 10, 24, 32, 18  });

            chartData.Add(new object[] { "9011", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9012", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9014", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9015", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9016", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9017", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9018", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9019", 10, 24, 32, 18  });

            chartData.Add(new object[] { "9021", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9022", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9024", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9025", 10, 24, 32, 18  });

            chartData.Add(new object[] { "9031", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9032", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9034", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9035", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9036", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9037", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9038", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9039", 10, 24, 32, 18  });

            chartData.Add(new object[] { "9041", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9042", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9044", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9045", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9046", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9047", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9048", 10, 24, 32, 18  });
            chartData.Add(new object[] { "9049", 10, 24, 32, 18  });

            return chartData;


            //  var data = google.visualization.arrayToDataTable([
            //    ['Genre', 'Fantasy & Sci Fi', 'Romance', 'Mystery Crime', 'General', 'Western', 'Literature', { role: 'annotation' }],
            //    ['2010', 10, 24, 20, 32, 18, 5, ''],
            //    ['2020', 16, 22, 23, 30, 16, 9, ''],
            //    ['2030', 28, 19, 29, 30, 12, 13, '']
            //]);
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlRegion, new BDMS_Address().GetRegion(ddlCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCountry.SelectedValue), null, null), "Region", "RegionID");

            ClientScript.RegisterStartupScript(GetType(), "hwa", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(drawChart); ", true);
        }
        void loadYearAndMonth()
        {
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            for (int i = 2018; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Insert(ddlYear.Items.Count, new ListItem(i.ToString(), i.ToString()));
            }
            ddlMonth.Items.Insert(0, new ListItem("Select", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Insert(ddlMonth.Items.Count, new ListItem(i.ToString(), i.ToString()));
            }
        }
    }
}