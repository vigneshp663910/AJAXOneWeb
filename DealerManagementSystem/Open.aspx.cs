using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public partial class Open : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new BTest().getSalesByYearAndMonth();
        }

        protected void btnAPITest_Click(object sender, EventArgs e)
        {
          //  new BAPI().GetServicePriority1();
            new BAPI().Main1();
        }

        [WebMethod]
        public static List<object> GetChartData(string country)
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "ShipCity", "TotalOrders", "tttt" });
            chartData.Add(new object[] { 2013, 1000, 522 });
            chartData.Add(new object[] { 2014, 1170, 866 });
            chartData.Add(new object[] { 2015, 660, 88 });
            chartData.Add(new object[] { 2016, 1030, 866 });
            return chartData;
        }
    }
}