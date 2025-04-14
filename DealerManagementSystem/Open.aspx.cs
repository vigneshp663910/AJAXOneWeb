using Business;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties; 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Services;

namespace DealerManagementSystem
{
    public partial class Open : System.Web.UI.Page
    {
        public class PDealerBalanceConfirmation_Insert
        {
            public string DealerCode { get; set; }
            public DateTime Date { get; set; }
            public decimal VendorBalance { get; set; }
            public decimal CustomerBalance { get; set; }
            public decimal TotalOutstandingAsPerAjax { get; set; }
            public string Currency { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
             new BTest().getSalesByYearAndMonth(); 
            new FileLogger().WriteLog("Web Test" + DateTime.Now.ToString());
        } 

        protected void btnAPITest_Click(object sender, EventArgs e)
        {
            //  new BAPI().GetServicePriority1();
            new BAPI().Main1();
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



        protected void btnCustomerMiss_Click(object sender, EventArgs e)
        {
            new BAPI().ApiGetWithOutToken("Customer/CustomerMiss?CustomerCode=" + txtCustomerCodeMiss.Text.Trim());
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
            new BAPI().ApiGetWithOutToken("Warranty/CreateWarrantyClaimAnnexureToSAP");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            new BAPI().ApiGetWithOutToken("SalesQuotation/GetSalesQuotationFlow");
        }
 
        protected void btnMttrEscalation_Click(object sender, EventArgs e)
        {
            new BDMS_MTTR().SendMailMttrEscalationMatrix();
        } 

        protected void btnInsertDealerStockAgeing_Click(object sender, EventArgs e)
        {
            new BAPI().ApiGetWithOutToken("SqlJob/InsertDealerStockAgeing");
        }
    }
    [Serializable]
    public class PICTicketFSRSignature_Insert
    {
        public long FsrID { get; set; }
        public string FileType { get; set; }
        public byte[] TPhoto { get; set; }
        public byte[] TSignature { get; set; }
        public byte[] CPhoto { get; set; }
        public byte[] CSignature { get; set; }
        public string CName { get; set; }
        public DateTime SignatureOn { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}