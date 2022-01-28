using Business;
using System;
using System.Collections.Generic;
using System.Web.Services;

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

        protected void BtnMaterial_Click(object sender, EventArgs e)
        {
            new BDMS_Material().IntegrationMaterial();
        }

        protected void BtnMaterialSupersede_Click(object sender, EventArgs e)
        {
            new BDMS_Material().IntegrationMaterialSupersede();
        }
        protected void btnEnquiryIndiamart_Click(object sender, EventArgs e)
        {
            new BEnquiryIndiamart().InsertOrUpdateEnquiryIndiamart();
        }
    }
}