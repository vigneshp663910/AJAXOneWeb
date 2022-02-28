using DataAccess;
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
            UpdateJobsStatus((short)Jobs.ICTicketIntegrationFromCRM, Joblist((Jobs.ICTicketIntegrationFromCRM)));
            return;
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
            DataTable jobsDataTable = new DataTable();
            try
            {
                using (DataTable dt = provider.Select("GetActiveJobsForNextRun").Tables[0])
                {
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                            activeJobs.Add(Convert.ToInt32(dr["JobID"]));
                    }
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
        public Boolean Joblist(Jobs JobName)
        {
            Boolean i = false;
            int C = 0;
            try
            {
                switch (JobName)
                {
                    //case Jobs.WarrantyClaimFromPostgre:
                    //    new FileLogger().LogMessageService("Started", "WarrantyClaimFromPostgre", null);
                    //    C = new BDMS_WarrantyClaim().InsertWarrantyInvoice("", false);
                    //    new FileLogger().LogMessageService("Ended", "Warranty Claim from Postgre Total Record " + C.ToString(), null);
                    //    break;
                    //case Jobs.WarrantyClaimAbove50KFromPostgre:
                    //    new FileLogger().LogMessageService("Started", "WarrantyClaimAbove50KFromPostgre", null);
                    //    string Filter = " where inv.p_inv_id in ( select InvoiceNumber  from  ( select   invg.p_inv_id as  InvoiceNumber , sum(invig.r_gross_amt) Sum_Value FROM   dsinr_inv_item invig  inner JOIN dsinr_inv_hdr invg ON ( invig.k_id = invg.p_id AND invig.s_tenant_id = invg.s_tenant_id and  d_inv_type_desc = 'Warranty Invoice'  )  where 1 = 1  ";
                    //    Filter = Filter + " and inv.r_inv_date  = '" + DateTime.Now.ToShortDateString().Split('/')[1] + "/" + DateTime.Now.ToShortDateString().Split('/')[0] + "/" + DateTime.Now.ToShortDateString().Split('/')[2] + "'";
                    //    Filter = Filter + "  group by invg.p_inv_id  ,invg.s_tenant_id )	t where Sum_Value >= 50000) ";
                    //    C = new BDMS_WarrantyClaim().InsertWarrantyInvoice(Filter, false);
                    //    new FileLogger().LogMessageService("Ended", "Warranty Claim from Postgre Above 50K Total Record " + C.ToString(), null);
                    //    break;
                    case Jobs.ICTicketIntegrationFromCRM:
                        new FileLogger().LogMessageService("Started", "ICTicketIntegrationFromCRM", null);
                        C = new BDMS_ICTicket().IntegrationICTicket();
                        C = new BDMS_ICTicket().IntegrationICTicketByBapi();
                        new FileLogger().LogMessageService("Ended", "IC Ticket Integration From CRM Total Record " + C.ToString(), null);
                        break;
                    case Jobs.MaterialIntegrationFromPostgre:
                        new FileLogger().LogMessageService("Started", "MaterialIntegrationFromPostgre", null);
                        C = new BDMS_Material().IntegrationMaterial();
                        new FileLogger().LogMessageService("Ended", "Material Integration From Postgre Total Record " + C.ToString(), null);

                        break;
                    case Jobs.SAPDocumentForWarrantyInvoiceFromSAP:
                        new FileLogger().LogMessageService("Started", "SAP Document For Warranty Invoice From SAP", null);
                        new BDMS_WarrantyClaimInvoice().UpdateSAPDocumentNumberOld();
                        new BDMS_WarrantyClaimInvoice().UpdateSAPDocumentNumber();
                        new FileLogger().LogMessageService("Ended", "SAP Document For Warranty Invoice From SAP", null);
                        break;
                    case Jobs.SaleOrderNumberForSrviceQuatationFromSAP:
                        new FileLogger().LogMessageService("Started", "SaleOrder Number For Srvice Quatation From SAP", null);
                        new BDMS_Service().UpdateSaleOrderNumberFromPostgres();
                        new FileLogger().LogMessageService("Ended", "Sale Order Number For Srvice Quatation From SAP Total Record" + C.ToString(), null);
                        break;
                    case Jobs.TechnicianIntegrationFromSAP:
                        new FileLogger().LogMessageService("Started", "Technician Integration From SAP", null);
                        new BEmployees().InsertOrUpdateTechnicianUserFromSAP();
                        new FileLogger().LogMessageService("Ended", "Technician Integration From SAP", null);
                        break;
                    case Jobs.UpdateICTicketToSAP:
                        new FileLogger().LogMessageService("Started", "Update ICTicket To SAP", null);
                        new BDMS_ICTicket().UpdateICTicketToSAP();
                        new FileLogger().LogMessageService("Ended", "Update ICTicket To SAP", null);
                        break;
                    case Jobs.ModelForClaim:
                        new FileLogger().LogMessageService("Started", "Model For Claim", null);
                        new BDMS_WarrantyClaim().UpdateWarrantyClaimMachineSerialNumberForModel();
                        new FileLogger().LogMessageService("Ended", "Model For Claim", null);
                        break;
                    case Jobs.Category:
                        new FileLogger().LogMessageService("Started", "Category", null);
                        new BDMS_Category().IntegrationCategory();
                        new FileLogger().LogMessageService("Ended", "Category", null);
                        break;
                    case Jobs.ICTicketIntegrationVerification:
                        break;
                    case Jobs.QuotationForJSN:
                        new FileLogger().LogMessageService("Started", "QuotationForJSN", null);
                        new BDMS_SalesOrder().CreateQuotationForJSN();
                        new FileLogger().LogMessageService("Ended", "QuotationForJSN", null);
                        break;
                    case Jobs.IntegrationSalesOrder:
                        new FileLogger().LogMessageService("Started", "", null);

                        new FileLogger().LogMessageService("Ended", "", null);
                        break;
                    case Jobs.IntegrationSalesOrderInvoice:
                        new FileLogger().LogMessageService("Started", "IntegrationSalesOrderInvoice", null);
                        new BDMS_SalesOrder().IntegrationSalesOrderInvoice();
                        new FileLogger().LogMessageService("Ended", "IntegrationSalesOrderInvoice", null);
                        break;
                    case Jobs.IntegrationClaimAnnexure:
                        new FileLogger().LogMessageService("Started", "IntegrationWarrantyClaimAnnexureToSAP", null);
                        new BDMS_WarrantyClaimAnnexure().IntegrationWarrantyClaimAnnexureToSAP();
                        new FileLogger().LogMessageService("Ended", "IntegrationWarrantyClaimAnnexureToSAP", null);
                        break;
                    case Jobs.EInvoice:
                        new FileLogger().LogMessageService("Started", "BDMS_EInvoice", null);
                        new BDMS_EInvoice().IntegrationEInvoive();
                        new FileLogger().LogMessageService("Ended", "BDMS_EInvoice", null);
                        break;
                    case Jobs.SendMailMttrEscalationMatrix:
                        new FileLogger().LogMessageService("Started", "EscalationMoreThan72Hrs", null);
                        new BDMS_MTTR().SendMailMttrEscalationMatrix();
                        //new BDMS_MTTR().MailEscalationMoreThan72Hrs();
                        //new BDMS_MTTR().MailEscalationMoreThan48Hrs();
                        //new BDMS_MTTR().MailEscalationMoreThan24Hrs();
                        new FileLogger().LogMessageService("Ended", "EscalationMoreThan72Hrs", null);
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
    }
}