using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Helpers;
using System.Web.Services;

namespace DealerManagementSystem
{
    public partial class Test : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //var myChart = new Chart(width: 600, height: 400)
            //   .AddTitle("Employees")
            //   .AddSeries(chartType: "column",
            //      xValue: new[] { "Peter", "Andrew", "Julie", "Mary", "Dave" },
            //      yValues: new[] { "2", "6", "4", "5", "3" })
            //   .Write();

            //var myChart1 = new Chart(width: 600, height: 400)
            //   .AddTitle("Employees")
            //   .AddSeries(chartType: "Pie",
            //      xValue: new[] { "Peter", "Andrew", "Julie", "Mary", "Dave" },
            //      yValues: new[] { "2", "6", "4", "5", "3" })
            //   .AddLegend("Employee Info", "PieChart")
            //   .Write();
            BindChart();

        }
        private void BindChart()
        {
            DataTable dsChartData = new DataTable();
            StringBuilder strScript = new StringBuilder();

            try
            {
                dsChartData = GetChartData();

                strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Year', 'WebScripting', 'BrowserHijacker','ResidentVirus','FileInfectorVirus'],");

                foreach (DataRow row in dsChartData.Rows)
                {
                    strScript.Append("['" + row["Year"] + "'," + row["WebScripting"] + "," + row["BrowserHijacker"] + "," + row["ResidentVirus"] + "," + row["FileInfectorVirus"] + "],");
                }
                strScript.Remove(strScript.Length - 1, 1);
                strScript.Append("]);");

                strScript.Append("var options = { title : 'Year Wise Virus Count', vAxis: {title: 'Cups'},  hAxis: {title: 'Types of Virus'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
                strScript.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
                strScript.Append(" </script>");

                ltScripts.Text = strScript.ToString();
            }
            catch
            {
            }
            finally
            {
                dsChartData.Dispose();
                strScript.Clear();
            }
        }
        private DataTable GetChartData()
        {
            DataSet dsData = new DataSet();
            try
            {
                DataTable dt = new DataTable();

                try
                {
                    dt = new DataTable();
                    dt.Columns.Add("Year", typeof(string));
                    dt.Columns.Add("WebScripting", typeof(Int16));
                    dt.Columns.Add("BrowserHijacker", typeof(Int16));
                    dt.Columns.Add("ResidentVirus", typeof(Int16));
                    dt.Columns.Add("FileInfectorVirus", typeof(Int16));
                    dt.Rows.Add("2015", 34, 22, 34, 54);
                    dt.Rows.Add("2016", 26, 33, 12, 37);
                    dt.Rows.Add("2017", 72, 55, 23, 56);
                }
                catch (Exception ex)
                {

                }

                dsData = new DataSet();
                dsData.Tables.Add(dt);
            }
            catch
            {
                throw;
            }
            return dsData.Tables[0];
        }
        [WebMethod]
        public static List<object> GetChartData1()
        {
            List<object> chartData = new List<object>();

            chartData.Add(new object[]

            {
           "DateTime", "Bugs"
             });

            chartData.Add(new object[]
                    {
                  "2011", "12",
                  "2014","45",
                  "2015","40",
                  "2016","98"
                    });

            return chartData;
        }
        [System.Web.Services.WebMethod]
        public static string GetData()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = new DataTable();
                dt.Columns.Add("State", typeof(string));
                dt.Columns.Add("Count", typeof(Int16));
                dt.Rows.Add("A", 55);
                dt.Rows.Add("B", 20);
                dt.Rows.Add("C", 34);
                dt.Rows.Add("D", 40);
            }
            catch (Exception ex)
            {

            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            return ds.GetXml();
        }
    }
}