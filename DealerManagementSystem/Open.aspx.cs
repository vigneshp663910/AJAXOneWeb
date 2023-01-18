using Business;
using Properties;
using SapIntegration;
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
            new BDMS_Material().IntegrationMaterialMaster();
        }

        protected void BtnMaterialSupersede_Click(object sender, EventArgs e)
        {
            new BDMS_Material().IntegrationMaterialSupersede();
        }
        protected void btnEnquiryIndiamart_Click(object sender, EventArgs e)
        {
            new BAPI().ApiGetWithOutToken("Equipment/IntegrationEquipmentFromSAP_new");
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

        protected void btnSMS_Click(object sender, EventArgs e)
        {
            new BSmsManager().Start();
        }

        protected void btnMail_Click(object sender, EventArgs e)
        {
            new EmailManager().Start();
        }

        protected void BtnSalesQuotationDetails_Click(object sender, EventArgs e)
        {
            new BAPI().ApiGet("SalesQuotation/GetSalesQuotationFlow");
           // new BSalesQuotation().GetSalesQuotationFlow();
        }

        protected void BtnEnquiryDetails_Click(object sender, EventArgs e)
        {
            try
            {
               // DateTime From = Convert.ToDateTime(txtFromDate.Text.Trim());
               // DateTime To = Convert.ToDateTime(txtToDate.Text.Trim());
               // new BEnquiry().EnquirySync(From, To,txtEnquiryNo.Text.Trim(),txtDelaerCode.Text.Trim(),txtCustomerCode.Text.Trim());               
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnCustomerMiss_Click(object sender, EventArgs e)
        {
            new BAPI().ApiGetWithOutToken("Customer/CustomerMiss?CustomerCode=" + txtCustomerCodeMiss.Text.Trim());
        }

        protected void btnAPIEInvoice_Click(object sender, EventArgs e)
        {
            new BDMS_EInvoice().StartGeneratEInvoice();
        }

        protected void btnDealerAddress_Click(object sender, EventArgs e)
        {
            List<string> Dealer = new List<string>();
            Dealer.Add("9001");
            Dealer.Add("9004");
            Dealer.Add("9005");
            Dealer.Add("9007");
            Dealer.Add("9008");
            Dealer.Add("9009");
            Dealer.Add("9010");
            Dealer.Add("9012");
            Dealer.Add("9013");
            Dealer.Add("9014");
            Dealer.Add("9015");
            Dealer.Add("9017");
            Dealer.Add("9018");
            Dealer.Add("9019");
            Dealer.Add("9020");
            Dealer.Add("9021"); 
            Dealer.Add("9023");
            Dealer.Add("9024");
            Dealer.Add("9026");
            Dealer.Add("9027");
            Dealer.Add("9028");
            Dealer.Add("9029");
            Dealer.Add("9030");
            Dealer.Add("9031");
            Dealer.Add("9032");
            Dealer.Add("9034");
            Dealer.Add("9033");
            Dealer.Add("9036");
            Dealer.Add("9038");
            Dealer.Add("9040");
            Dealer.Add("9041");
            Dealer.Add("9042");
            Dealer.Add("9043");
            Dealer.Add("9044");
            Dealer.Add("9045");
            Dealer.Add("9046");
            Dealer.Add("9049");
            Dealer.Add("9050");
            Dealer.Add("9051");
            Dealer.Add("9052");
            Dealer.Add("9053");
            Dealer.Add("9054");
            Dealer.Add("9055");  
            foreach (string DealerCode in Dealer)
            {
                new BDealer().InsertOrUpdateDealerAddress(DealerCode);
            }
        }

        protected void btnIntegrationWarrantyClaimAnnexureToSAP_Click(object sender, EventArgs e)
        {
            new BDMS_WarrantyClaimAnnexure().IntegrationWarrantyClaimAnnexureToSAP();
        }

        protected void btnIntegrationEquipmentFromSAP_Click(object sender, EventArgs e)
        {
            new BDMS_Equipment().IntegrationEquipmentFromSAP();
        }

        protected void btnIntegrationEquipmentFromSAP_New_Click(object sender, EventArgs e)
        {
            new BAPI().ApiGetWithOutToken("Equipment/IntegrationEquipmentFromSAP_new");
        }
    }
}