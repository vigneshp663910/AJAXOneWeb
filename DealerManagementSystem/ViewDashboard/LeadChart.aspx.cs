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
            StringBuilder str = new StringBuilder();

            str.Append(@" <script type='text/javascript'>
                      google.load('visualization', '1', {packages: ['corechart']});
                    </script>
                    <script type='text/javascript'>
                    function drawVisualization() 
                    var data = google.visualization.arrayToDataTable([
                    ['Month', 'Bolivia', 'Ecuador', 'Madagascar', 'Average'],");

            DataTable dt = new DataTable();

            int count = dt.Rows.Count - 1;
            for (int i = 0; i <= count; i++)
            {
                if (count == i)
                {
                    str.Append("['" + dt.Rows[i]["Month"].ToString() + "'," + dt.Rows[i]["Bolivia"].ToString() + "," + dt.Rows[i]["Ecuador"].ToString() + "," + dt.Rows[i]["Madagascar"].ToString() + "," + dt.Rows[i]["Average"].ToString() + "]]);");
                }
                else
                {
                    str.Append("['" + dt.Rows[i]["Month"].ToString() + "'," + dt.Rows[i]["Bolivia"].ToString() + "," + dt.Rows[i]["Ecuador"].ToString() + "," + dt.Rows[i]["Madagascar"].ToString() + "," + dt.Rows[i]["Average"].ToString() + "],");
                }
            } 
            str.Append("var options = { title : 'Monthly Coffee Production by Country', vAxis: {title: 'Cups'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
            str.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
            str.Append(" </script>");
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static List<GraphData> GetChartData1(string country)
        {
            // Initialization.  
            List<GraphData> result = new List<GraphData>();

            try
            {

            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return info.  
            return result;
        }

       


        [WebMethod]
        public static List<object> GetChartData(string country)
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "ShipCity", "TotalOrders" });
            chartData.Add(new object[] { 2013, 1000 });
            chartData.Add(new object[] { 2014, 1170 });
            chartData.Add(new object[] { 2015, 660 });
            chartData.Add(new object[] { 2016, 1030 });
            return chartData;
        }

        [WebMethod]
        public static List<object> GetChartData2(string country)
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "General", "Open", "Assign", "Quotation", "Invoice",  "Western" });
            chartData.Add(new object[] { "9001", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9002", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9004", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9005", 10, 24, 32, 18, 5 });

            chartData.Add(new object[] { "9011", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9012", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9014", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9015", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9016", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9017", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9018", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9019", 10, 24, 32, 18, 5 });

            chartData.Add(new object[] { "9021", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9022", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9024", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9025", 10, 24, 32, 18, 5 });

            chartData.Add(new object[] { "9031", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9032", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9034", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9035", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9036", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9037", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9038", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9039", 10, 24, 32, 18, 5 });

            chartData.Add(new object[] { "9041", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9042", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9044", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9045", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9046", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9047", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9048", 10, 24, 32, 18, 5 });
            chartData.Add(new object[] { "9049", 10, 24, 32, 18, 5 });

            return chartData;


            //  var data = google.visualization.arrayToDataTable([
            //    ['Genre', 'Fantasy & Sci Fi', 'Romance', 'Mystery Crime', 'General', 'Western', 'Literature', { role: 'annotation' }],
            //    ['2010', 10, 24, 20, 32, 18, 5, ''],
            //    ['2020', 16, 22, 23, 30, 16, 9, ''],
            //    ['2030', 28, 19, 29, 30, 12, 13, '']
            //]);
        }
    }
}