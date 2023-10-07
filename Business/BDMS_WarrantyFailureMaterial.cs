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
   public class BDMS_WarrantyFailureMaterial
    {
         private IDataAccess provider;
         public BDMS_WarrantyFailureMaterial()
        {
            provider = new ProviderFactory().GetProvider();
        }

         public List<PDMS_WarrantyMaterialReturnStatus> GetWarrantyMaterialReturnStatus(int? WarrantyMaterialReturnStatusID, string WarrantyMaterialReturnStatus)
         {
             List<PDMS_WarrantyMaterialReturnStatus> ReturnStatus = new List<PDMS_WarrantyMaterialReturnStatus>();
             PDMS_WarrantyMaterialReturnStatus Status = null;
             DbParameter WarrantyMaterialReturnStatusIDP = provider.CreateParameter("WarrantyMaterialReturnStatusID", WarrantyMaterialReturnStatusID, DbType.Int32);
             DbParameter WarrantyMaterialReturnStatusP = provider.CreateParameter("WarrantyMaterialReturnStatus", string.IsNullOrEmpty(WarrantyMaterialReturnStatus) ? null : WarrantyMaterialReturnStatus, DbType.String);

             DbParameter[] Params = new DbParameter[1] { WarrantyMaterialReturnStatusIDP };
             try
             {
                 using (DataSet ds = provider.Select("ZDMS_GetWarrantyMaterialReturnStatus", Params))
                 {
                     if (ds != null)
                     {

                         foreach (DataRow dr in ds.Tables[0].Rows)
                         {

                             Status = new PDMS_WarrantyMaterialReturnStatus();
                             Status.WarrantyMaterialReturnStatusID = Convert.ToInt32(dr["WarrantyMaterialReturnStatusID"]);
                             Status.WarrantyMaterialReturnStatus = Convert.ToString(dr["WarrantyMaterialReturnStatus"]);
                             ReturnStatus.Add(Status);
                         }
                     }
                 }
             }
             catch (SqlException sqlEx)
             { }
             catch (Exception ex)
             { }
             return ReturnStatus;
         }
       public List<PDMS_WarrantyInvoiceHeader> GetFailedMaterialReturn(string ICTicketID, DateTime? ICTicketDateF, DateTime? ICTicketDateT, string ClaimID,
       DateTime? ClaimDateF, DateTime? ClaimDateT, string DealerCode, int? StatusID, string DCNumber, DateTime? DCDateF, DateTime? DCDateT, int UserID)
       {
           List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
           PDMS_WarrantyInvoiceHeader W = null;
           DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", string.IsNullOrEmpty(ICTicketID) ? null : ICTicketID, DbType.String);
           DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
           DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

           DbParameter ClaimIDP = provider.CreateParameter("ClaimID", string.IsNullOrEmpty(ClaimID) ? null : ClaimID, DbType.String);
           DbParameter ClaimDateFP = provider.CreateParameter("ClaimDateF", ClaimDateF, DbType.DateTime);
           DbParameter ClaimDateTP = provider.CreateParameter("ClaimDateT", ClaimDateT, DbType.DateTime);

           DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
           DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);

           DbParameter DCNumberP = provider.CreateParameter("DCNumber", string.IsNullOrEmpty(DCNumber) ? null : DCNumber, DbType.String);
           DbParameter DCDateFP = provider.CreateParameter("DCDateF", DCDateF, DbType.DateTime);
           DbParameter DCDateTP = provider.CreateParameter("DCDateT", DCDateT, DbType.DateTime);

           DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
           DbParameter[] Params = new DbParameter[12] { ICTicketIDP, ICTicketDateFP, ICTicketDateTP, ClaimIDP, ClaimDateFP, ClaimDateTP, DealerCodeP, StatusIDP
               , DCNumberP,DCDateFP,DCDateTP, UserIDP };
           try
           {
               using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetFailedMaterialReturn", Params))
               {
                   if (EmployeeDataSet != null)
                   {
                       long HeaderID = -1;
                       foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                       {

                           if (HeaderID != Convert.ToInt64(dr["WarrantyInvoiceHeaderID"]))
                           {
                               W = new PDMS_WarrantyInvoiceHeader();
                               Ws.Add(W);
                               W.WarrantyInvoiceHeaderID = Convert.ToInt64(dr["WarrantyInvoiceHeaderID"]);

                               W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                               W.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);

                               W.ICTicketID = Convert.ToString(dr["ICTicketID"]);
                               W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                               W.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                               W.CustomerName = Convert.ToString(dr["CustomerName"]);
                               W.DealerCode = Convert.ToString(dr["DealerCode"]);
                               W.DealerName = Convert.ToString(dr["DealerName"]);
                               W.Approved1By = new PUser();
                               if (dr["Approved1By"] != DBNull.Value)
                               {
                                   W.Approved1By.ContactName = Convert.ToString(dr["Approved1By"]);
                               }
                               W.Approved1On = DBNull.Value == dr["Approved1On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved1On"]);


                               W.Approved2By = new PUser();
                               if (dr["Approved2By"] != DBNull.Value)
                               {
                                   W.Approved2By.ContactName = Convert.ToString(dr["Approved2By"]);
                               }

                               W.Approved2On = DBNull.Value == dr["Approved2On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved2On"]);
                               //   W.Status = Convert.ToString(dr["Status"]).Trim();
                               W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                               W.MarginWarranty = DBNull.Value == dr["MarginWarranty"] ? (Boolean?)null : Convert.ToBoolean(dr["MarginWarranty"]);
                               W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                               W.Model = Convert.ToString(dr["Model"]);
                               W.PscID = Convert.ToString(dr["PscID"]);
                               W.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
                               W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                               W.Location = Convert.ToString(dr["Location"]);
                               W.Application = Convert.ToString(dr["Application"]);
                               W.WarrantyEndDate = DBNull.Value == dr["WarrantyEndDate"] ? (DateTime?)null : Convert.ToDateTime(dr["WarrantyEndDate"]);
                               HeaderID = W.WarrantyInvoiceHeaderID;
                               W.DateOfCommissioning = DBNull.Value == dr["DateOfCommissioning"] ? (DateTime?)null : Convert.ToDateTime(dr["DateOfCommissioning"]);
                               W.FSRNumber = Convert.ToString(dr["FSRNumber"]);
                               W.ReasonForFailure = Convert.ToString(dr["ReasonForFailure"]);
                               W.AcInvoiceNumber = Convert.ToString(dr["AcInvoiceNumber"]);
                               W.ClaimStatus = Convert.ToString(dr["Status"]);
                               W.InvoiceItems = new List<PDMS_WarrantyInvoiceItem>();
                           }
                           PDMS_WarrantyInvoiceItem item = new PDMS_WarrantyInvoiceItem();
                           item.WarrantyInvoiceItemID = Convert.ToInt64(dr["WarrantyInvoiceItemID"]);

                           item.Item = Convert.ToString(dr["Item"]);
                           item.RefDocID = Convert.ToString(dr["RefDocID"]);
                           item.Material = Convert.ToString(dr["Material"]);
                           item.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);
                           item.Category = Convert.ToString(dr["Category"]);

                           item.HSNCode = Convert.ToString(dr["HSNCode"]);
                           item.Qty = Convert.ToDecimal(dr["Qty"]);
                           item.UnitOM = Convert.ToString(dr["UnitOM"]);
                           item.MaterialStatus = Convert.ToString(dr["MaterialStatus"]);
                           item.MaterialStatusRemarks1 = Convert.ToString(dr["MaterialStatusRemarks1"]);
                           item.MaterialStatusRemarks2 = Convert.ToString(dr["MaterialStatusRemarks2"]);
                           item.Amount = DBNull.Value == dr["Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Amount"]);
                           item.BaseTax = DBNull.Value == dr["BaseTax"] ? (decimal?)null : Convert.ToDecimal(dr["BaseTax"]);
                           item.Approved1Amount = DBNull.Value == dr["Approved1Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Approved1Amount"]);
                           item.Approved1Remarks = Convert.ToString(dr["Approved1Remarks"]);

                           item.Approved2Amount = DBNull.Value == dr["Approved2Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Approved2Amount"]);
                           item.Approved2Remarks = Convert.ToString(dr["Approved2Remarks"]);
                           item.WarrantyMaterialReturnStatus = new PDMS_WarrantyMaterialReturnStatus();
                           if (DBNull.Value != dr["WarrantyMaterialReturnStatusID"])
                           {
                               item.WarrantyMaterialReturnStatus.WarrantyMaterialReturnStatusID = Convert.ToInt32(dr["WarrantyMaterialReturnStatusID"]);
                               item.WarrantyMaterialReturnStatus.WarrantyMaterialReturnStatus = Convert.ToString(dr["WarrantyMaterialReturnStatus"]);
                           }
                           item.AnnexureNumber = Convert.ToString(dr["AnnexureNumber"]);
                           item.SAPDoc = Convert.ToString(dr["SAPDoc"]);
                           item.SAPInvoiceValue = DBNull.Value == dr["SAPInvoiceValue"] ? (decimal?)null : Convert.ToDecimal(dr["SAPInvoiceValue"]);
                           item.FailureMaterial = new PDMS_WarrantyFailureMaterial();

                           item.FailureMaterial.DeliveryChallanNumber = Convert.ToString(dr["DeliveryChallanNumber"]);
                           item.FailureMaterial.DeliveryChallanDate = DBNull.Value == dr["DeliveryChallanDate"] ? (DateTime?)null : Convert.ToDateTime(dr["DeliveryChallanDate"]);
                           item.FailureMaterial.TransporterName = Convert.ToString(dr["TransporterName"]);
                           item.FailureMaterial.DocketDetails = Convert.ToString(dr["DocketDetails"]);
                           
                           item.FailureMaterial.PackingDetails = Convert.ToString(dr["PackingDetails"]);
                           item.FailureMaterial.FailureMaterialItem = new PDMS_WarrantyFailureMaterialItem();
                           item.FailureMaterial.FailureMaterialItem.AcknowledgedOn = DBNull.Value == dr["AcknowledgedOn"] ? (DateTime?)null : Convert.ToDateTime(dr["AcknowledgedOn"]);
                           item.FailureMaterial.FailureMaterialItem.IsCanceled = DBNull.Value == dr["IsCanceled"] ? false : Convert.ToBoolean(dr["IsCanceled"]);
                           W.InvoiceItems.Add(item);
                       }
                   }
               }
           }
           catch (SqlException sqlEx)
           { }
           catch (Exception ex)
           { }
           return Ws;
       }

       public List<PDMS_WarrantyInvoiceHeader> GetFailedMaterialDCTemplateCreation(string ICTicketID, DateTime? ICTicketDateF, DateTime? ICTicketDateT, string ClaimID,
      DateTime? ClaimDateF, DateTime? ClaimDateT, string DealerCode, int? StatusID, string TSIRNumber, int UserID)
       {
           List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
           PDMS_WarrantyInvoiceHeader W = null;
           DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", string.IsNullOrEmpty(ICTicketID) ? null : ICTicketID, DbType.String);
           DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
           DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

           DbParameter ClaimIDP = provider.CreateParameter("ClaimID", string.IsNullOrEmpty(ClaimID) ? null : ClaimID, DbType.String);
           DbParameter ClaimDateFP = provider.CreateParameter("ClaimDateF", ClaimDateF, DbType.DateTime);
           DbParameter ClaimDateTP = provider.CreateParameter("ClaimDateT", ClaimDateT, DbType.DateTime);

           DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
           DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
           DbParameter TSIRNumberP = provider.CreateParameter("TSIRNumber", string.IsNullOrEmpty(TSIRNumber) ? null : TSIRNumber, DbType.String);
           DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
           DbParameter[] Params = new DbParameter[10] { ICTicketIDP, ICTicketDateFP, ICTicketDateTP, ClaimIDP, ClaimDateFP, ClaimDateTP, DealerCodeP, StatusIDP, TSIRNumberP, UserIDP };
           try
           {
               using (DataSet EmployeeDataSet = provider.Select("GetWarrantyFailedMaterialDCTemplateCreation", Params))
               {
                   if (EmployeeDataSet != null)
                   {
                       long HeaderID = -1;
                       foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                       {

                          // if (HeaderID != Convert.ToInt64(dr["WarrantyInvoiceHeaderID"]))
                          // {
                               W = new PDMS_WarrantyInvoiceHeader();
                               Ws.Add(W);
                               W.WarrantyInvoiceHeaderID = Convert.ToInt64(dr["WarrantyInvoiceHeaderID"]);

                               W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                               W.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);

                               W.ICTicketID = Convert.ToString(dr["ICTicketID"]);
                               W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                               W.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                               W.CustomerName = Convert.ToString(dr["CustomerName"]);
                               W.DealerCode = Convert.ToString(dr["DealerCode"]);
                               W.DealerName = Convert.ToString(dr["DealerName"]);
                               W.Approved1By = new PUser();
                               if (dr["Approved1By"] != DBNull.Value)
                               {
                                   W.Approved1By.ContactName = Convert.ToString(dr["Approved1By"]);
                               }
                               W.Approved1On = DBNull.Value == dr["Approved1On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved1On"]);


                               W.Approved2By = new PUser();
                               if (dr["Approved2By"] != DBNull.Value)
                               {
                                   W.Approved2By.ContactName = Convert.ToString(dr["Approved2By"]);
                               }

                               W.Approved2On = DBNull.Value == dr["Approved2On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved2On"]);
                               //   W.Status = Convert.ToString(dr["Status"]).Trim();
                               W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                               W.MarginWarranty = DBNull.Value == dr["MarginWarranty"] ? (Boolean?)null : Convert.ToBoolean(dr["MarginWarranty"]);
                               W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                               W.Model = Convert.ToString(dr["Model"]);
                               W.PscID = Convert.ToString(dr["PscID"]);
                               W.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
                               W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                               W.Location = Convert.ToString(dr["Location"]);
                               W.Application = Convert.ToString(dr["Application"]);
                               W.WarrantyEndDate = DBNull.Value == dr["WarrantyEndDate"] ? (DateTime?)null : Convert.ToDateTime(dr["WarrantyEndDate"]);
                            //   HeaderID = W.WarrantyInvoiceHeaderID;
                               W.DateOfCommissioning = DBNull.Value == dr["DateOfCommissioning"] ? (DateTime?)null : Convert.ToDateTime(dr["DateOfCommissioning"]);
                               W.FSRNumber = Convert.ToString(dr["FSRNumber"]);
                               W.ReasonForFailure = Convert.ToString(dr["ReasonForFailure"]);
                               W.InvoiceItem = new PDMS_WarrantyInvoiceItem();

                               PDMS_WarrantyInvoiceItem item = new PDMS_WarrantyInvoiceItem();
                               item.WarrantyInvoiceItemID = Convert.ToInt64(dr["WarrantyInvoiceItemID"]);

                               item.Item = Convert.ToString(dr["Item"]);
                               item.RefDocID = Convert.ToString(dr["RefDocID"]);
                               item.Material = Convert.ToString(dr["Material"]);
                               item.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);
                               item.Category = Convert.ToString(dr["Category"]);

                               item.HSNCode = Convert.ToString(dr["HSNCode"]);
                               item.Qty = Convert.ToDecimal(dr["Qty"]);
                               item.UnitOM = Convert.ToString(dr["UnitOM"]);
                               item.MaterialStatus = Convert.ToString(dr["MaterialStatus"]);
                               item.MaterialStatusRemarks1 = Convert.ToString(dr["MaterialStatusRemarks1"]);
                               item.MaterialStatusRemarks2 = Convert.ToString(dr["MaterialStatusRemarks2"]);
                               item.Amount = DBNull.Value == dr["Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Amount"]);
                               item.BaseTax = DBNull.Value == dr["BaseTax"] ? (decimal?)null : Convert.ToDecimal(dr["BaseTax"]);
                               item.Approved1Amount = DBNull.Value == dr["Approved1Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Approved1Amount"]);
                               item.Approved1Remarks = Convert.ToString(dr["Approved1Remarks"]);

                               item.Approved2Amount = DBNull.Value == dr["Approved2Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Approved2Amount"]);
                               item.Approved2Remarks = Convert.ToString(dr["Approved2Remarks"]);

                               W.InvoiceItem = (item);
                          // }
                       }
                   }
               }
           }
           catch (SqlException sqlEx)
           { }
           catch (Exception ex)
           { }
           return Ws;
       }


       public List<PDMS_WarrantyFailureMaterial> GetDCTemplateNameActive(int? DealerID	)
       {
           List<PDMS_WarrantyFailureMaterial> Ws = new List<PDMS_WarrantyFailureMaterial>();        
           try
           {

               DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
               DbParameter[] Params = new DbParameter[1] { DealerIDP };

               using (DataSet DataSet = provider.Select("ZDMS_GetDCTemplateNameActive", Params))
               {
                   if (DataSet != null)
                   {
                       foreach (DataRow dr in DataSet.Tables[0].Rows)
                       {
                           Ws.Add(new PDMS_WarrantyFailureMaterial()
                               {
                                   DCTemplateID = Convert.ToInt64(dr["DCTemplateID"]),
                                   DCTemplateName = Convert.ToString(dr["DCTemplateName"])
                               });
                       }
                   }
               }
           }
           catch (SqlException sqlEx)
           { }
           catch (Exception ex)
           { }
           return Ws;
       }

       public long InsertWarrantyFailureMaterialDCTemplate(long DCTemplateID, string TemplateName, List<long> WarrantyInvoiceItemIDs)
       { 
           int success = 0;
           long DCTemplateIDT = 0;
           DbParameter DCTemplateIDP = provider.CreateParameter("DCTemplateID", DCTemplateID, DbType.Int64);
           DbParameter TemplateNameP = provider.CreateParameter("TemplateName", TemplateName, DbType.String);

           DbParameter WarrantyClaimHeader = provider.CreateParameter("OutValue", DCTemplateIDT, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
           DbParameter[] Params = new DbParameter[3] {DCTemplateIDP, TemplateNameP, WarrantyClaimHeader };
           try
           {
               using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
               {
                   success = provider.Insert("ZDMS_InsertWarrantyFailureMaterialDCTemplate", Params);
                   if (success != 0)
                   {
                       DCTemplateID = Convert.ToInt64(WarrantyClaimHeader.Value);
                       foreach (long WarrantyInvoiceItemID in WarrantyInvoiceItemIDs)
                       {
                           InsertWarrantyFailureMaterialDCTemplateItem(DCTemplateID, WarrantyInvoiceItemID);
                       }
                   }
                   scope.Complete();
               }
           }
           catch (SqlException sqlEx)
           {
               new FileLogger().LogMessage("BDMS_WarrantyFailureMaterial", "insertWarrantyInvoiceHeader", sqlEx);
               DCTemplateID = 0;
               throw;
           }
           catch (Exception ex)
           {
               new FileLogger().LogMessage("BDMS_WarrantyFailureMaterial", " insertWarrantyInvoiceHeader", ex);
               DCTemplateID = 0;
               throw;
           }
           return DCTemplateID;
       }
       public void InsertWarrantyFailureMaterialDCTemplateItem(long DCTemplateID, long WarrantyInvoiceItemID)
       {
            
           DbParameter DCTemplateIDP = provider.CreateParameter("DCTemplateID", DCTemplateID, DbType.Int64);
           DbParameter WarrantyInvoiceItemIDP = provider.CreateParameter("WarrantyInvoiceItemID", WarrantyInvoiceItemID, DbType.Int32);


           DbParameter[] Params = new DbParameter[2] { DCTemplateIDP, WarrantyInvoiceItemIDP };
           try
           {
               provider.Insert("ZDMS_InsertWarrantyFailureMaterialDCTemplateItem", Params);
           }
           catch (SqlException sqlEx)
           {
               new FileLogger().LogMessage("BDMS_WarrantyFailureMaterial", "InsertWarrantyFailureMaterialDCTemplateItem", sqlEx);
               throw;
           }
           catch (Exception ex)
           {
               new FileLogger().LogMessage("BDMS_WarrantyFailureMaterial", " InsertWarrantyFailureMaterialDCTemplateItem", ex);
               throw;
           }
       }

       public List<PDMS_WarrantyFailureMaterial> GetFailedMaterialDCTemplateToCreateDC(long? DCTemplateID, int? DealerID)
       {
           List<PDMS_WarrantyFailureMaterial> Ws = new List<PDMS_WarrantyFailureMaterial>();
           PDMS_WarrantyFailureMaterial W = null;
           DbParameter DCTemplateIDP = provider.CreateParameter("DCTemplateID", DCTemplateID, DbType.Int64);
           DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
           DbParameter[] Params = new DbParameter[2] { DCTemplateIDP, DealerIDP };
           try
           {
               using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyFailedMaterialDCTemplateToCreateDC", Params))
               {
                   if (EmployeeDataSet != null)
                   {
                       long HeaderID = -1;
                       foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                       {
                           if (HeaderID != Convert.ToInt64(dr["DCTemplateID"]))
                           {
                               W = new PDMS_WarrantyFailureMaterial();
                               Ws.Add(W);
                               W.DCTemplateID = Convert.ToInt64(dr["DCTemplateID"]);
                               W.DCTemplateName = Convert.ToString(dr["TemplateName"]);                              
                               HeaderID = W.DCTemplateID;  
                               W.FailureMaterialItems = new List<PDMS_WarrantyFailureMaterialItem>();
                           }
                           PDMS_WarrantyFailureMaterialItem item = new PDMS_WarrantyFailureMaterialItem();

                           item.Invoice = new PDMS_WarrantyInvoiceHeader();
                           item.Invoice.WarrantyInvoiceHeaderID = Convert.ToInt64(dr["WarrantyInvoiceHeaderID"]);

                           item.Invoice.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                           item.Invoice.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);

                           item.Invoice.ICTicketID = Convert.ToString(dr["ICTicketID"]);
                           item.Invoice.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                           item.Invoice.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                           item.Invoice.CustomerName = Convert.ToString(dr["CustomerName"]);
                           item.Invoice.DealerCode = Convert.ToString(dr["DealerCode"]);
                           item.Invoice.DealerName = Convert.ToString(dr["DealerName"]);

                           item.Invoice.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                           item.Invoice.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                           item.Invoice.Model = Convert.ToString(dr["Model"]);

                           item.Invoice.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
                           item.Invoice.FSRNumber = Convert.ToString(dr["FSRNumber"]);

                           item.Invoice.InvoiceItem = new PDMS_WarrantyInvoiceItem();
                           item.Invoice.InvoiceItem.WarrantyInvoiceItemID = Convert.ToInt64(dr["WarrantyInvoiceItemID"]);
                           item.Invoice.InvoiceItem.Material = Convert.ToString(dr["Material"]);
                           item.Invoice.InvoiceItem.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);

                           item.Invoice.InvoiceItem.HSNCode = Convert.ToString(dr["HSNCode"]);
                           item.Invoice.InvoiceItem.Qty = Convert.ToDecimal(dr["Qty"]);
                           item.Invoice.InvoiceItem.UnitOM = Convert.ToString(dr["UnitOM"]);

                           W.FailureMaterialItems.Add(item);

                       }
                   }
               }
           }
           catch (SqlException sqlEx)
           { }
           catch (Exception ex)
           { }
           return Ws;
       }

       public long InsertWarrantyFailureMaterialDC(long DeliveryChallanID, string DeliveryTo, string TransporterName, string DocketDetails, string PackingDetails, List<long> WarrantyInvoiceItemIDs, long UserID, long DealerID)
       {
           int success = 0;
           long DCID = 0;
           DbParameter DeliveryChallanIDP = provider.CreateParameter("DeliveryChallanID", DeliveryChallanID, DbType.Int64);
           DbParameter DeliveryToP = provider.CreateParameter("DeliveryTo", DeliveryTo, DbType.String);
           DbParameter TransporterNameP = provider.CreateParameter("TransporterName", TransporterName, DbType.String);
           DbParameter DocketDetailsP = provider.CreateParameter("DocketDetails", DocketDetails, DbType.String);
           DbParameter PackingDetailsP = provider.CreateParameter("PackingDetails", PackingDetails, DbType.String);

           DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
           DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int64);

           DbParameter WarrantyClaimHeader = provider.CreateParameter("OutValue", DCID, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
           DbParameter[] Params = new DbParameter[8] { DeliveryChallanIDP,DeliveryToP, TransporterNameP, DocketDetailsP, PackingDetailsP, WarrantyClaimHeader, UserIDP, DealerIDP };
           try
           {
               using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
               {
                   success = provider.Insert("ZDMS_InsertWarrantyFailureMaterialDC", Params);
                   if (success != 0)
                   {
                       DCID = Convert.ToInt64(WarrantyClaimHeader.Value);
                       if (DeliveryChallanID == 0)
                       {
                           foreach (long WarrantyInvoiceItemID in WarrantyInvoiceItemIDs)
                           {
                               InsertWarrantyFailureMaterialDCItem(DCID, WarrantyInvoiceItemID);
                           }
                       }
                   }
                   scope.Complete();
               }
           }
           catch (SqlException sqlEx)
           {
               new FileLogger().LogMessage("BDMS_WarrantyFailureMaterial", "insertWarrantyInvoiceHeader", sqlEx);
               DCID = 0;
               throw;
           }
           catch (Exception ex)
           {
               new FileLogger().LogMessage("BDMS_WarrantyFailureMaterial", " insertWarrantyInvoiceHeader", ex);
               DCID = 0;
               throw;
           }
           return DCID;
       }
       public void InsertWarrantyFailureMaterialDCItem(long DeliveryChallanID, long WarrantyInvoiceItemID)
       {
           DbParameter DeliveryChallanItemIDP = provider.CreateParameter("DeliveryChallanID", DeliveryChallanID, DbType.Int64);
           DbParameter WarrantyInvoiceItemIDP = provider.CreateParameter("WarrantyInvoiceItemID", WarrantyInvoiceItemID, DbType.Int32);
           DbParameter[] Params = new DbParameter[2] { DeliveryChallanItemIDP, WarrantyInvoiceItemIDP };
           try
           {                
               provider.Insert("ZDMS_InsertWarrantyFailureMaterialDCItem", Params);            
           }
           catch (SqlException sqlEx)
           {
               new FileLogger().LogMessage("BDMS_WarrantyFailureMaterial", "InsertWarrantyFailureMaterialDCTemplateItem", sqlEx);
               throw;
           }
           catch (Exception ex)
           {
               new FileLogger().LogMessage("BDMS_WarrantyFailureMaterial", " InsertWarrantyFailureMaterialDCTemplateItem", ex);
               throw;
           }
       }
       public List<PDMS_WarrantyFailureMaterial> GetWarrantyFailedMaterialDeliveryChallan(long? DeliveryChallanID, String DCNumber, DateTime? DCDateF, DateTime? DCDateT, String ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? DealerID)
       {
           List<PDMS_WarrantyFailureMaterial> Ws = new List<PDMS_WarrantyFailureMaterial>();
           PDMS_WarrantyFailureMaterial W = null;
           DbParameter DeliveryChallanIDP = provider.CreateParameter("DeliveryChallanID", DeliveryChallanID, DbType.Int64);
           DbParameter DCNumberP = provider.CreateParameter("DCNumber", string.IsNullOrEmpty(DCNumber) ? null : DCNumber, DbType.String);
           DbParameter DCDateFP = provider.CreateParameter("DCDateF", DCDateF, DbType.DateTime);
           DbParameter DCDateTP = provider.CreateParameter("DCDateT", DCDateT, DbType.DateTime);

           DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
           DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
           DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);


           DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
           DbParameter[] Params = new DbParameter[8] { DeliveryChallanIDP, DCNumberP, DCDateFP, DCDateTP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, DealerIDP };
           try
           {
               using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyFailedMaterialDeliveryChallan", Params))
               {
                   if (EmployeeDataSet != null)
                   {
                       long HeaderID = -1;
                       foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                       {

                           if (HeaderID != Convert.ToInt64(dr["DeliveryChallanID"]))
                           {
                               W = new PDMS_WarrantyFailureMaterial();
                               Ws.Add(W);
                               W.DeliveryChallanID = Convert.ToInt64(dr["DeliveryChallanID"]);
                               W.DeliveryChallanNumber = Convert.ToString(dr["DeliveryChallanNumber"]);
                               W.DeliveryChallanDate = Convert.ToDateTime(dr["CreatedOn"]);

                               W.DeliveryTo = Convert.ToString(dr["DeliveryTo"]);
                               W.TransporterName = Convert.ToString(dr["TransporterName"]);
                               W.DocketDetails = Convert.ToString(dr["DocketDetails"]);
                               W.PackingDetails = Convert.ToString(dr["PackingDetails"]);
                               HeaderID = W.DeliveryChallanID;
                               W.Dealer = new PDMS_Dealer();
                               W.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                               W.Dealer.DealerName = Convert.ToString(dr["DealerName"]);
                               W.CreatedBy = new PUser();
                               W.CreatedBy.ContactName = Convert.ToString(dr["ContactName"]);
                               W.FailureMaterialItems = new List<PDMS_WarrantyFailureMaterialItem>();
                           }
                           PDMS_WarrantyFailureMaterialItem item = new PDMS_WarrantyFailureMaterialItem();

                           item.Invoice = new PDMS_WarrantyInvoiceHeader();
                           item.Invoice.WarrantyInvoiceHeaderID = Convert.ToInt64(dr["WarrantyInvoiceHeaderID"]);

                           item.Invoice.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                           item.Invoice.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);

                           item.Invoice.ICTicketID = Convert.ToString(dr["ICTicketID"]);
                           item.Invoice.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                           item.Invoice.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                           item.Invoice.CustomerName = Convert.ToString(dr["CustomerName"]);


                           item.Invoice.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                           item.Invoice.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                           item.Invoice.Model = Convert.ToString(dr["Model"]);

                           item.Invoice.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
                           item.Invoice.FSRNumber = Convert.ToString(dr["FSRNumber"]);

                           item.Invoice.InvoiceItem = new PDMS_WarrantyInvoiceItem();
                           item.Invoice.InvoiceItem.WarrantyInvoiceItemID = Convert.ToInt64(dr["WarrantyInvoiceItemID"]);
                           item.Invoice.InvoiceItem.Material = Convert.ToString(dr["Material"]);
                           item.Invoice.InvoiceItem.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);

                           item.Invoice.InvoiceItem.HSNCode = Convert.ToString(dr["HSNCode"]);
                           item.Invoice.InvoiceItem.Qty = Convert.ToDecimal(dr["Qty"]);
                           item.Invoice.InvoiceItem.Amount = Convert.ToDecimal(dr["Amount"]);

                           item.Invoice.InvoiceItem.UnitOM = Convert.ToString(dr["UnitOM"]);
                           item.IsAcknowledged = DBNull.Value == dr["IsAcknowledged"] ? false : Convert.ToBoolean(dr["IsAcknowledged"]);
                           item.IsCanceled = DBNull.Value == dr["IsCanceled"] ? false : Convert.ToBoolean(dr["IsCanceled"]);
                           item.DeliveryChallanItemID = Convert.ToInt64(dr["DeliveryChallanItemID"]);
                           W.FailureMaterialItems.Add(item);
                       }
                   }
               }
           }
           catch (SqlException sqlEx)
           { }
           catch (Exception ex)
           { }
           return Ws;
       }
       public Boolean AcknowledgeWarrantyFailureMaterialDCItem(long DeliveryChallanItemID, int UserID)
       {

           DbParameter DeliveryChallanItemIDP = provider.CreateParameter("DeliveryChallanItemID", DeliveryChallanItemID, DbType.Int64);
           DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);


           DbParameter[] Params = new DbParameter[2] { DeliveryChallanItemIDP, UserIDP };
           try
           {
               using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
               {
                   provider.Insert("ZDMS_AcknowledgeWarrantyFailureMaterialDCItem", Params);
                   scope.Complete();
               }

           }
           catch (SqlException sqlEx)
           {
               new FileLogger().LogMessage("BDMS_WarrantyFailureMaterial", "AcknowledgeWarrantyFailureMaterialDCItem", sqlEx);
               return false;
           }
           catch (Exception ex)
           {
               new FileLogger().LogMessage("BDMS_WarrantyFailureMaterial", " AcknowledgeWarrantyFailureMaterialDCItem", ex);
               return false;
           }
           return true;
       }
       public Boolean CancelWarrantyFailureMaterialDCItem(long DeliveryChallanItemID, int UserID)
       {

           DbParameter DeliveryChallanItemIDP = provider.CreateParameter("DeliveryChallanItemID", DeliveryChallanItemID, DbType.Int64);
           DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);


           DbParameter[] Params = new DbParameter[2] { DeliveryChallanItemIDP, UserIDP };
           try
           {
               using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
               {
                   provider.Insert("ZDMS_CancelWarrantyFailureMaterialDCItem", Params);
                   scope.Complete();
               }

           }
           catch (SqlException sqlEx)
           {
               new FileLogger().LogMessage("BDMS_WarrantyFailureMaterial", "CancelWarrantyFailureMaterialDCItem", sqlEx);
               return false;
           }
           catch (Exception ex)
           {
               new FileLogger().LogMessage("BDMS_WarrantyFailureMaterial", " CancelWarrantyFailureMaterialDCItem", ex);
               return false;
           }
           return true;
       }
   }
}
