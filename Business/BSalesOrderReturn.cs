using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BSalesOrderReturn
    {
        private IDataAccess provider;
        public BSalesOrderReturn()
        {
            try
            {
                provider = new ProviderFactory().GetProvider();
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BSalesOrderReturn", "provider : " + e1.Message, null);
            }
        } 
        //public PApiResult GetSaleOrderDeliveryForSoReturnCreation(int DealerID, int DealerOfficeID, long CustomerCode, int? DivisionID, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT)
        //{
        //    string endPoint = "SaleOrderReturn/SaleOrderDeliveryForSoReturnCreation?DealerID=" + DealerID + "&CustomerCode=" + CustomerCode
        //        + "&DealerOfficeID=" + DealerOfficeID + "&DivisionID=" + DivisionID + "&InvoiceNumber=" + InvoiceNumber
        //         + "&InvoiceDateF=" + InvoiceDateF + "&InvoiceDateT=" + InvoiceDateT;
        //    return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        //}
        public PApiResult GetSaleOrderDeliveryForSoReturnCreation(string InvoiceNumber, string DeliveryNumber)
        {
            string endPoint = "SaleOrderReturn/SaleOrderDeliveryForSoReturnCreation?InvoiceNumber=" + InvoiceNumber + "&DeliveryNumber=" + DeliveryNumber;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public DataTable GetSaleOrderDeliveryItemForSoReturnCreation(long SaleOrderDeliveryItemID)
        {
            string endPoint = "SaleOrderReturn/SaleOrderDeliveryItemForSoReturnCreation?SaleOrderDeliveryItemID=" + SaleOrderDeliveryItemID;
            //return JsonConvert.DeserializeObject<List<PSaleOrderDelivery>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetSaleOrderReturnHeader(int? DealerID, int? OfficeCodeID, int? DivisionID, string CustomerCode, long? SaleOrderReturnID, string SaleOrderReturnNo, string SaleOrderReturnDateF, string SaleOrderReturnDateT, int? SaleOrderReturnStatusID
            ,string CreditNoteNumber , string CreditNotenDateF, string CreditNotenDateT, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "SaleOrderReturn/SaleOrderReturnHeader?DealerID=" + DealerID + "&OfficeCodeID=" + OfficeCodeID + "&DivisionID=" + DivisionID + "&CustomerCode=" + CustomerCode + "&SaleOrderReturnID=" + SaleOrderReturnID + "&SaleOrderReturnNo=" + SaleOrderReturnNo
                 + "&SaleOrderReturnDateF=" + SaleOrderReturnDateF + "&SaleOrderReturnDateT=" + SaleOrderReturnDateT + "&SaleOrderReturnStatusID=" + SaleOrderReturnStatusID
                 + "&CreditNoteNumber=" + CreditNoteNumber + "&CreditNotenDateF=" + CreditNotenDateF + "&CreditNotenDateT=" + CreditNotenDateT + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PSaleOrderReturn GetSaleOrderReturnByID(long SaleOrderReturnID)
        {
            string endPoint = "SaleOrderReturn/SaleOrderReturnByID?SaleOrderReturnID=" + SaleOrderReturnID;
            return JsonConvert.DeserializeObject<PSaleOrderReturn>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public PApiResult UpdateSaleOrderReturnStatus(long SaleOrderReturnID,int StatusID)
        {
            string endPoint = "SaleOrderReturn/UpdateSaleOrderReturnStatus?SaleOrderReturnID=" + SaleOrderReturnID + "&StatusID=" + StatusID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public SalesReturnCreditFileDetails GetSaleOrderReturnCreditNoteFileDetails(long SaleOrderReturnID)
        {
            string endPoint = "SaleOrderReturn/GetSaleOrderReturnCreditNoteFileDetails?SaleOrderReturnID=" + SaleOrderReturnID;
            return JsonConvert.DeserializeObject<SalesReturnCreditFileDetails>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}
