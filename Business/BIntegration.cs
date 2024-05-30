using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Business
{
    public class BIntegration
    {
        private IDataAccess provider;
        public BIntegration()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public void Start()
        {
            //UpdateJobsStatus((short)Jobs.EInvoice, Joblist((Jobs.EInvoice)));
            //return;
            List<int> activeJobs = GetActiveJobsForNextRun();
            foreach (int Job in activeJobs)
            {
                UpdateJobsStatus(Job, Joblist((Jobs)Job));
            }

        }
        //public void DailyJob()
        //{
        //    new FileLogger().LogMessageService("Start", "Main", null);
        //    int C = new BDMS_WarrantyClaim().InsertWarrantyInvoice("", false);
        //    new FileLogger().LogMessageService("WarrantyInvoice Total Record", C.ToString(), null);
        //    new FileLogger().LogMessageService("End", "Main", null);

        //}

        public List<int> GetActiveJobsForNextRun()
        {
            List<int> activeJobs = new List<int>(); 
            try
            {
                DbParameter AjaxOne = provider.CreateParameter("AjaxOne", true, DbType.Boolean);
                DbParameter[] Params = new DbParameter[1] { AjaxOne };
                using (DataTable dt = provider.Select("GetActiveJobsForNextRun", Params).Tables[0])
                {
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                            activeJobs.Add(Convert.ToInt32(dr["JobID"]));
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return activeJobs;
        }
        public Boolean Joblist(Jobs JobName)
        {
            Boolean i = false;
            int C = 0;
            try
            {
                switch (JobName)
                {
                    case Jobs.SendSMS:
                        new FileLogger().LogMessageService("Started", "SendSMS", null);
                        C = new BSmsManager().Start();
                        new FileLogger().LogMessageService("Ended", "Send SMS Total Record " + C.ToString(), null);
                        break;
                    case Jobs.SendMail:
                        new FileLogger().LogMessageService("Started", "SendSMS", null);
                        C = new EmailManager().Start();
                        new FileLogger().LogMessageService("Ended", "Send Mail Total Record " + C.ToString(), null);
                        break;
                    case Jobs.MaterialIntegrationFromSAP: 
                        new BAPI().ApiGetWithOutToken("Material/MaterialIntegrationFromSap"); 
                        break;
                    //case Jobs.CustomerIntegration:
                    //    new FileLogger().LogMessageService("Started", "CustomerIntegration", null);
                    //    new BAPI().ApiGetWithOutToken("Customer/CustomerMiss");
                    //    new BAPI().ApiGetWithOutToken("Customer/CustomerShipToMiss");
                    //    new FileLogger().LogMessageService("Ended", "CustomerIntegration ", null);
                    //    break;

                    //case Jobs.UpdateSalesQuotationDeliveryDetails:
                    //    new FileLogger().LogMessageService("Started", "CustomerIntegration", null);
                    //    new BAPI().ApiGetWithOutToken("SalesQuotation/InsertOrUpdateSalesQuotationDeliveryDetails");
                    //    new FileLogger().LogMessageService("Ended", "CustomerIntegration ", null);
                    //    break;

                    case Jobs.SalesQuotationFlowFromSap: 
                        new BAPI().ApiGetWithOutToken("SalesQuotation/GetSalesQuotationFlow"); 
                        break;
                    //case Jobs.EnquiryFromCRM:
                    //    new FileLogger().LogMessageService("Started", "EnquiryFromCRM", null);
                    //    new BAPI().ApiGetWithOutToken("Enquiry/getEnquiryFromSAP");
                    //    new FileLogger().LogMessageService("Ended", "EnquiryFromCRM ", null);
                    //    break;
                    case Jobs.EInvoice: 
                        new BDMS_EInvoice().StartGeneratEInvoice(); 
                        break;
                    case Jobs.LeadQualificationByExpectedDateOfSale: 
                        new BAPI().ApiGetWithOutToken("Lead/UpdateLeadQualificationByExpectedDateOfSale"); 
                        break;

                    case Jobs.IntegrationClaimAnnexure: 
                        new BAPI().ApiGetWithOutToken("Warranty/CreateWarrantyClaimAnnexureToSAP"); 
                        break;


                    //case Jobs.MaterialIntegrationFromPostgre:
                    //    new FileLogger().LogMessageService("Started", "MaterialIntegrationFromPostgre", null);
                    //    C = new BDMS_Material().IntegrationMaterial();
                    //    new FileLogger().LogMessageService("Ended", "Material Integration From Postgre Total Record " + C.ToString(), null);
                    //    break;

                    case Jobs.SAPDocumentForWarrantyInvoiceFromSAP: 
                        //new BDMS_WarrantyClaimInvoice().UpdateSAPDocumentNumberOld();
                        //new BDMS_WarrantyClaimInvoice().UpdateSAPDocumentNumber();
                        new BAPI().ApiGetWithOutToken("Warranty/UpdateSAPDocumentNumber"); 
                        break;
                    case Jobs.SaleOrderNumberForSrviceQuatationFromSAP:
                        new FileLogger().LogMessageService("Started", "SaleOrder Number For Srvice Quatation From SAP", null);
                        UpdateSaleOrderNumberFromPostgres();
                        new FileLogger().LogMessageService("Ended", "Sale Order Number For Srvice Quatation From SAP Total Record" + C.ToString(), null);
                        break;

                    //case Jobs.TechnicianIntegrationFromSAP:
                    //    new FileLogger().LogMessageService("Started", "Technician Integration From SAP", null);
                    //    new BEmployees().InsertOrUpdateTechnicianUserFromSAP();
                    //    new FileLogger().LogMessageService("Ended", "Technician Integration From SAP", null);
                    //    break;
                    //case Jobs.UpdateICTicketToSAP:
                    //    new FileLogger().LogMessageService("Started", "Update ICTicket To SAP", null);
                    //    new BDMS_ICTicket().UpdateICTicketToSAP();
                    //    new FileLogger().LogMessageService("Ended", "Update ICTicket To SAP", null);
                    //    break;

                    //case Jobs.ModelForClaim:
                    //    new FileLogger().LogMessageService("Started", "Model For Claim", null);
                    //    new BDMS_WarrantyClaim().UpdateWarrantyClaimMachineSerialNumberForModel();
                    //    new FileLogger().LogMessageService("Ended", "Model For Claim", null);
                    //    break;
                    //case Jobs.Category:
                    //    new FileLogger().LogMessageService("Started", "Category", null);
                    //    new BDMS_Category().IntegrationCategory();
                    //    new FileLogger().LogMessageService("Ended", "Category", null);
                    //    break;

                    //case Jobs.ICTicketIntegrationVerification:
                    //    break;
                    //case Jobs.QuotationForJSN:
                    //    new FileLogger().LogMessageService("Started", "QuotationForJSN", null);
                    //    new BDMS_SalesOrder().CreateQuotationForJSN();
                    //    new FileLogger().LogMessageService("Ended", "QuotationForJSN", null);
                    //    break;
                    //case Jobs.IntegrationSalesOrder:
                    //    new FileLogger().LogMessageService("Started", "", null);

                    //    new FileLogger().LogMessageService("Ended", "", null);
                    //    break;
                    //case Jobs.IntegrationSalesOrderInvoice:
                    //    new FileLogger().LogMessageService("Started", "IntegrationSalesOrderInvoice", null);
                    //    new BDMS_SalesOrder().IntegrationSalesOrderInvoice();
                    //    new FileLogger().LogMessageService("Ended", "IntegrationSalesOrderInvoice", null);
                    //    break;
                    //case Jobs.IntegrationClaimAnnexure:
                    //    new FileLogger().LogMessageService("Started", "IntegrationWarrantyClaimAnnexureToSAP", null);
                    //    new BDMS_WarrantyClaimAnnexure().IntegrationWarrantyClaimAnnexureToSAP();
                    //    new FileLogger().LogMessageService("Ended", "IntegrationWarrantyClaimAnnexureToSAP", null);
                    //    break;
                    //case Jobs.EInvoice:
                    //    new FileLogger().LogMessageService("Started", "BDMS_EInvoice", null);
                    //    new BDMS_EInvoice().IntegrationEInvoive();
                    //    new FileLogger().LogMessageService("Ended", "BDMS_EInvoice", null);
                    //    break;
                    case Jobs.SendMailMttrEscalationMatrix: 
                        new BDMS_MTTR().SendMailMttrEscalationMatrix();
                        //new BDMS_MTTR().MailEscalationMoreThan72Hrs();
                        //new BDMS_MTTR().MailEscalationMoreThan48Hrs();
                        //new BDMS_MTTR().MailEscalationMoreThan24Hrs(); 
                        break;

                    //case Jobs.IntegrationEquipmentFromSAP:
                    //    new FileLogger().LogMessageService("Started", "IntegrationEquipmentFromSAP", null);
                    //    new BDMS_Equipment().IntegrationEquipmentFromSAP();
                    //    new FileLogger().LogMessageService("Ended", "IntegrationEquipmentFromSAP", null);
                    //    break;
                    case Jobs.IntegrationEquipmentFromSAP_New: 
                        new BAPI().ApiGetWithOutToken("Equipment/IntegrationEquipmentFromSAP_new"); 
                        break;

                    case Jobs.EnquiryIndiamart: 
                        new BAPI().ApiGetWithOutToken("Enquiry/IntegrationEnquiryIndiamart"); 
                        break;
                    case Jobs.TaskTicketResolvedAutoClose: 
                        new BAPI().ApiGetWithOutToken("Task/TaskTicketResolvedAutoClose"); 
                        break;
                    case Jobs.RebuildIndex: 
                        new BAPI().ApiGetWithOutToken("Application/RebuildIndex"); 
                        break;
                }
                i = true;
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BIntegration", "BIntegration : " + e1.Message, null);
            }
            return i;
        }
        public List<int> UpdateJobsStatus(int JobID, Boolean LastStatus)
        {
            List<int> activeJobs = new List<int>();
            DataTable jobsDataTable = new DataTable();
            try
            {
                DbParameter JobIDParam = provider.CreateParameter("JobID", JobID, DbType.Int32);
                DbParameter LastStatusParam = provider.CreateParameter("LastStatus", LastStatus, DbType.Boolean);
                DbParameter[] Params = new DbParameter[2] { JobIDParam, LastStatusParam };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("UpdateJobsStatus", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            return activeJobs;
        }

        public void UpdateSaleOrderNumberFromPostgres()
        { 
            new BAPI().ApiGetWithOutToken("ICTicket/UpdateSaleOrderNumberFromPostgres"); 
        }
    }
}