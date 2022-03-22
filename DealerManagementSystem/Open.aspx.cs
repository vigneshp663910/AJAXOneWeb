using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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

        protected void BtnCreateCustomer_Click(object sender, EventArgs e)
        {
            //List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerFromSQL(Convert.ToInt32(txtCustomerId.Text), null);
            //string CustomerCode = Customer[0].CustomerCode;
            //if (string.IsNullOrEmpty(CustomerCode))
            //{
            //    CustomerCode = new SapIntegration.SCustomer().CreateCustomerInSAP(Customer);
            //    if (!string.IsNullOrEmpty(CustomerCode))
            //    {
                   // new BDMS_Customer().UpdateCustomerCodeFromSapToSql(Convert.ToInt64(txtCustomerId.Text));
            //    }
            //}
        }

        protected void BtnCreateQuotation_Click(object sender, EventArgs e)
        {
            //List<> Customer = new BDMS_Customer().GetCustomerFromSQL(Convert.ToInt32(txtCustomerId.Text), null);
            //string CustomerCode = Customer[0].CustomerCode;
            //if (string.IsNullOrEmpty(CustomerCode))
            //{
            //CustomerCode = new SapIntegration.SCustomer().CreateCustomerInSAP(Customer);
            //string QuotationNo=new SapIntegration.SQuotation().getQuotationIntegration();
            //if (!string.IsNullOrEmpty(CustomerCode))
            //{
            //    new BDMS_Customer().UpdateCustomerCodeFromSapToSql(Convert.ToInt32(txtCustomerId.Text), CustomerCode);
            //}
            //}
        }

        protected void btnUpdateAddressFromSapToSql_Click(object sender, EventArgs e)
        {
            new BDMS_Customer().UpdateCustomerAddressFromSapToSql();
        }
    }
}