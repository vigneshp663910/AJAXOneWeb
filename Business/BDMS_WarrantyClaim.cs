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
    public class BDMS_WarrantyClaim
    {
        private IDataAccess provider; 
        public BDMS_WarrantyClaim()
        {
            provider = new ProviderFactory().GetProvider(); 
        } 
        //public List<PDMS_WarrantyInvoiceHeader> GetWarrantyClaimReport(string ICTicketID, DateTime? ICTicketDateF, DateTime? ICTicketDateT, string InvoiceNumber,
        //DateTime? InvoiceDateF, DateTime? InvoiceDateT, string DealerCode, int? StatusID,
        //  DateTime? AnnexureF, DateTime? AnnexureT, string TSIRNumber,string CustomerCode,string MachineSerialNumber, Boolean IsAbove50K, int? UserID)
        //{
        //    List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
        //    PDMS_WarrantyInvoiceHeader W = null;
        //    DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", string.IsNullOrEmpty(ICTicketID) ? null : ICTicketID, DbType.String);
        //    DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
        //    DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

        //    DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
        //    DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
        //    DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);

        //    DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
        //    DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);


        //    //DbParameter Approved1DateFP = provider.CreateParameter("Approved1DateF", Approved1DateF, DbType.DateTime);
        //    //DbParameter Approved1DateTP = provider.CreateParameter("Approved1DateT", Approved1DateT, DbType.DateTime);
        //    //DbParameter Approved2DateFP = provider.CreateParameter("Approved2DateF", Approved2DateF, DbType.DateTime);
        //    //DbParameter Approved2DateTP = provider.CreateParameter("Approved2DateT", Approved2DateT, DbType.DateTime);


        //    DbParameter AnnexureFP = provider.CreateParameter("AnnexureF", AnnexureF, DbType.DateTime);
        //    DbParameter AnnexureTP = provider.CreateParameter("AnnexureT", AnnexureT, DbType.DateTime);



        //    DbParameter TSIRNumberP = provider.CreateParameter("TSIRNumber", string.IsNullOrEmpty(TSIRNumber) ? null : TSIRNumber, DbType.String);

        //    DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
        //    DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", string.IsNullOrEmpty(MachineSerialNumber) ? null : MachineSerialNumber, DbType.String);
        //    DbParameter UserIDP = provider.CreateParameter("IsAbove50K", IsAbove50K, DbType.Boolean);
        //    DbParameter IsAbove50KP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //    DbParameter[] Params = new DbParameter[15] { ICTicketIDP, ICTicketDateFP, ICTicketDateTP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerCodeP, StatusIDP,
        //       AnnexureFP,AnnexureTP , TSIRNumberP,CustomerCodeP,MachineSerialNumberP,IsAbove50KP, UserIDP };
        //    try
        //    {
        //        using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimReport", Params, (20) * 60))
        //        {
        //            if (EmployeeDataSet != null)
        //            {
        //                long HeaderID = -1;
        //                foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
        //                {

        //                    if (HeaderID != Convert.ToInt64(dr["WarrantyInvoiceHeaderID"]))
        //                    {
        //                        W = new PDMS_WarrantyInvoiceHeader();
        //                        Ws.Add(W);
        //                        W.WarrantyInvoiceHeaderID = Convert.ToInt64(dr["WarrantyInvoiceHeaderID"]);
        //                        W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
        //                        W.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
        //                        W.ICTicketID = Convert.ToString(dr["ICTicketID"]);
        //                        W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
        //                        W.CustomerCode = Convert.ToString(dr["CustomerCode"]);
        //                        W.CustomerName = Convert.ToString(dr["CustomerName"]);
        //                        W.DealerCode = Convert.ToString(dr["DealerCode"]);
        //                        W.DealerName = Convert.ToString(dr["DealerName"]);
        //                        W.ICTicket = new PDMS_ICTicket()
        //                        {
        //                            ServiceType = new PDMS_ServiceType() { ServiceType = Convert.ToString(dr["ServiceType"]) },
        //                            ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]),
        //                            Equipment = new PDMS_EquipmentHeader() { CommissioningOn = DBNull.Value == dr["CommissioningOn"] ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]) },

        //                        };

        //                        W.Approved1By = new PUser();
        //                        if (dr["Approved1By"] != DBNull.Value)
        //                        {
        //                            W.Approved1By.ContactName = Convert.ToString(dr["Approved1By"]);
        //                        }
        //                        // W.Approved1By = new PUser() { UserID = DBNull.Value == dr["Approved1By"] ? (int?)null : Convert.ToInt32(dr["Approved1By"]) };
        //                        W.Approved1On = DBNull.Value == dr["Approved1On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved1On"]);

        //                        //  W.Approved2By = DBNull.Value == dr["Approved2By"] ? (int?)null : Convert.ToInt32(dr["Approved2By"]);

        //                        W.Approved2By = new PUser();
        //                        if (dr["Approved2By"] != DBNull.Value)
        //                        {
        //                            W.Approved2By.ContactName = Convert.ToString(dr["Approved2By"]);
        //                        }
        //                        W.Approved2On = DBNull.Value == dr["Approved2On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved2On"]);

        //                        W.Approved3By = new PUser();
        //                        if (dr["Approved3By"] != DBNull.Value)
        //                        {
        //                            W.Approved3By.ContactName = Convert.ToString(dr["Approved3By"]);
        //                        }
        //                        W.Approved3On = DBNull.Value == dr["Approved3On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved3On"]);

        //                        W.ClaimStatus = Convert.ToString(dr["Status"]).Trim();
        //                        W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
        //                        W.MarginWarranty = DBNull.Value == dr["MarginWarranty"] ? (Boolean?)null : Convert.ToBoolean(dr["MarginWarranty"]);
        //                        W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
        //                        W.Model = Convert.ToString(dr["Model"]);
        //                        W.PscID = Convert.ToString(dr["PscID"]);
        //                      //  W.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
        //                        W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
        //                        W.AnnexureNumber = Convert.ToString(dr["AnnexureNumber"]);
        //                        W.AnnexureDate = DBNull.Value == dr["AnnexureDate"] ? (DateTime?)null : Convert.ToDateTime(dr["AnnexureDate"]);
        //                        W.AcInvoiceNumber = Convert.ToString(dr["AcInvoiceNumber"]);
        //                        W.AcInvoiceDate = DBNull.Value == dr["AcInvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["AcInvoiceDate"]);
        //                        HeaderID = W.WarrantyInvoiceHeaderID;

        //                        W.InvoiceItems = new List<PDMS_WarrantyInvoiceItem>();
        //                    }
        //                    PDMS_WarrantyInvoiceItem item = new PDMS_WarrantyInvoiceItem();
        //                    item.WarrantyInvoiceItemID = Convert.ToInt64(dr["WarrantyInvoiceItemID"]);

        //                    item.Item = Convert.ToString(dr["Item"]);
        //                    item.RefDocID = Convert.ToString(dr["RefDocID"]);
        //                    item.Material = Convert.ToString(dr["Material"]);
        //                    item.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);
        //                    item.DeliveryNumber = Convert.ToString(dr["DeliveryNumber"]);
        //                    item.Category = Convert.ToString(dr["Category"]);

        //                    item.HSNCode = Convert.ToString(dr["HSNCode"]);
        //                    item.Qty = Convert.ToDecimal(dr["Qty"]);
        //                    item.Per = DBNull.Value == dr["Per"] ? (decimal?)null : Convert.ToDecimal(dr["Per"]);
        //                    item.UnitOM = Convert.ToString(dr["UnitOM"]);
        //                    item.MaterialStatus = Convert.ToString(dr["MaterialStatus"]);
        //                    item.MaterialStatusRemarks1 = Convert.ToString(dr["MaterialStatusRemarks1"]);
        //                    item.MaterialStatusRemarks2 = Convert.ToString(dr["MaterialStatusRemarks2"]);
        //                    item.Amount = DBNull.Value == dr["Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Amount"]);
        //                    item.BaseTax = DBNull.Value == dr["BaseTax"] ? (decimal?)null : Convert.ToDecimal(dr["BaseTax"]);
        //                    item.Approved1Amount = DBNull.Value == dr["Approved1Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Approved1Amount"]);
        //                    item.Approved1Remarks = Convert.ToString(dr["Approved1Remarks"]);

        //                    item.Approved2Amount = DBNull.Value == dr["Approved2Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Approved2Amount"]);
        //                    item.Approved2Remarks = Convert.ToString(dr["Approved2Remarks"]);
        //                    item.Approved3Amount = DBNull.Value == dr["Approved3Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Approved3Amount"]);
        //                    item.Approved3Remarks = Convert.ToString(dr["Approved3Remarks"]);


        //                    //   item.InvoiceNumberNew = Convert.ToString(dr["InvoiceNumberNew"]);                           

        //                    item.SAPDoc = Convert.ToString(dr["SAPDoc"]);
        //                    item.SAPPostingDate = DBNull.Value == dr["SAPPostingDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SAPPostingDate"]);
        //                    item.SAPInvoiceValue = DBNull.Value == dr["SAPInvoiceValue"] ? (decimal?)null : Convert.ToDecimal(dr["SAPInvoiceValue"]);
        //                    item.SAPClearingDocument = Convert.ToString(dr["SAPClearingDocument"]);
        //                    item.DeliveryNumber = Convert.ToString(dr["DeliveryNumber"]);
        //                    item.AnnexureNumber = Convert.ToString(dr["AnnexureNumber"]);
        //                    item.WarrantyMaterialReturnStatus = new PDMS_WarrantyMaterialReturnStatus();
        //                    if (DBNull.Value != dr["WarrantyMaterialReturnStatusID"])
        //                    {
        //                        item.WarrantyMaterialReturnStatus.WarrantyMaterialReturnStatusID = Convert.ToInt32(dr["WarrantyMaterialReturnStatusID"]);
        //                        item.WarrantyMaterialReturnStatus.WarrantyMaterialReturnStatus = Convert.ToString(dr["WarrantyMaterialReturnStatus"]);
        //                    }
        //                    item.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
        //                  //  item.TSIRDate = DBNull.Value == dr["TSIRDate"] ? (DateTime?)null : Convert.ToDateTime(dr["TSIRDate"]);
        //                    W.InvoiceItems.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return Ws;
        //}
         
        public List<PDMS_WarrantyInvoiceHeader_New> GetWarrantyClaimReport_New1(PWarrantyClaim_Filter Filter)
        { 
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Warranty/GetWarrantyClaimReport", Filter));
            if (Results.Status == PApplication.Failure)
            {
                throw new Exception(Results.Message);
            } 
            return JsonConvert.DeserializeObject<List<PDMS_WarrantyInvoiceHeader_New>>(JsonConvert.SerializeObject(Results.Data)); 
        }
        public List<PDMS_WarrantyInvoiceHeader_1> GetWarrantyClaimHeader(long? ICTicketID,long? WarrantyInvoiceHeaderID, string InvoiceNumber   )
        {
            List<PDMS_WarrantyInvoiceHeader_1> Ws = new List<PDMS_WarrantyInvoiceHeader_1>();
            PDMS_WarrantyInvoiceHeader_1 W = null;
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
            DbParameter WarrantyInvoiceHeaderIDP = provider.CreateParameter("WarrantyInvoiceHeaderID", WarrantyInvoiceHeaderID, DbType.Int64);
            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String); 
            DbParameter[] Params = new DbParameter[3] { ICTicketIDP, WarrantyInvoiceHeaderIDP, InvoiceNumberP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimHeader", Params))
                {
                    if (EmployeeDataSet != null)
                    { 
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        { 
                            W = new PDMS_WarrantyInvoiceHeader_1();
                            Ws.Add(W);
                            //W.WarrantyInvoiceHeaderID = Convert.ToInt64(dr["WarrantyInvoiceHeaderID"]);
                            //W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            //W.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]); 
                            W.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                            W.CustomerName = Convert.ToString(dr["CustomerName"]);
                            //W.DealerCode = Convert.ToString(dr["DealerCode"]);
                            //W.DealerName = Convert.ToString(dr["DealerName"]);
                            W.ICTicket = new PDMS_ICTicket()
                            {
                                ICTicketID = Convert.ToInt64(dr["ICTicketID"]),
                                ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]),
                                ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]),
                                Customer = new PDMS_Customer() { CustomerID = Convert.ToInt64(dr["CustomerID"]) },
                                //ServiceType = new PDMS_ServiceType() { ServiceType = Convert.ToString(dr["ServiceType"]) },
                                //Equipment = new PDMS_EquipmentHeader() { CommissioningOn = DBNull.Value == dr["CommissioningOn"] ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]) }
                            };
                            W.Approved1By = new PUser();
                            if (dr["Approved1By"] != DBNull.Value)
                            {
                                W.Approved1By.ContactName = Convert.ToString(dr["Approved1By"]);
                            }
                            //W.Approved1On = DBNull.Value == dr["Approved1On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved1On"]);
                            W.Approved2By = new PUser();
                            if (dr["Approved2By"] != DBNull.Value)
                            {
                                W.Approved2By.ContactName = Convert.ToString(dr["Approved2By"]);
                            }
                           // W.Approved2On = DBNull.Value == dr["Approved2On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved2On"]);
                            //W.Approved3By = new PUser();
                            //if (dr["Approved3By"] != DBNull.Value)
                            //{
                            //    W.Approved3By.ContactName = Convert.ToString(dr["Approved3By"]);
                            //}
                            //W.Approved3On = DBNull.Value == dr["Approved3On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved3On"]);


                            //W.ClaimStatus = Convert.ToString(dr["Status"]).Trim();
                            W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                            //W.MarginWarranty = DBNull.Value == dr["MarginWarranty"] ? (Boolean?)null : Convert.ToBoolean(dr["MarginWarranty"]);
                            W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                            W.Model = Convert.ToString(dr["Model"]);
                            //W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                            //W.AnnexureNumber = Convert.ToString(dr["AnnexureNumber"]);
                            //W.AnnexureDate = DBNull.Value == dr["AnnexureDate"] ? (DateTime?)null : Convert.ToDateTime(dr["AnnexureDate"]);
                            //W.AcInvoiceNumber = Convert.ToString(dr["AcInvoiceNumber"]);
                            //W.AcInvoiceDate = DBNull.Value == dr["AcInvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["AcInvoiceDate"]); 
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
        public DataTable GetWarrantyClaimReporExcel(PWarrantyClaim_Filter Filter)
        {
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Warranty/GetWarrantyClaimReportExcel", Filter));
            if (Results.Status == PApplication.Failure)
            {
                throw new Exception(Results.Message);
            }
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Results.Data));
        }
         
        public List<PDMS_WarrantyInvoiceHeader_New> GetWarrantyClaimApproval(string ICTicketID, DateTime? ICTicketDateF, DateTime? ICTicketDateT, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID, int? StatusID, string TSIRNumber,string DivisionID, int UserID)
        {
            List<PDMS_WarrantyInvoiceHeader_New> Ws = new List<PDMS_WarrantyInvoiceHeader_New>();
            PDMS_WarrantyInvoiceHeader_New W = null;
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", string.IsNullOrEmpty(ICTicketID) ? null : ICTicketID, DbType.String);
            DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
            DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
            DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
            DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);

            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
            DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
            DbParameter TSIRNumberP = provider.CreateParameter("TSIRNumber", string.IsNullOrEmpty(TSIRNumber) ? null : TSIRNumber, DbType.String);
            DbParameter DivisionIDP = provider.CreateParameter("DivisionID", DivisionID, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[11] { ICTicketIDP, ICTicketDateFP, ICTicketDateTP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerIDP, StatusIDP, TSIRNumberP, DivisionIDP, UserIDP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimApprovalAPI", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        long HeaderID = -1;
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {

                            if (HeaderID != Convert.ToInt64(dr["WarrantyInvoiceHeaderID"]))
                            {
                                W = new PDMS_WarrantyInvoiceHeader_New();
                                Ws.Add(W);
                                W.WarrantyInvoiceHeaderID = Convert.ToInt64(dr["WarrantyInvoiceHeaderID"]);
                                W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                W.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                                W.ICTicket = new PDMS_ICTicket() { ServiceType = new PDMS_ServiceType() { ServiceType = Convert.ToString(dr["ServiceType"]) } };

                                W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                                W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
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

                                //W.Approved3By = new PUser();
                                //if (dr["Approved3By"] != DBNull.Value)
                                //{
                                //    W.Approved3By.ContactName = Convert.ToString(dr["Approved3By"]);
                                //}
                                //W.Approved3On = DBNull.Value == dr["Approved3On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved3On"]);


                                W.ClaimStatus = Convert.ToString(dr["ClaimStatus"]).Trim();
                                W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                                W.MarginWarranty = DBNull.Value == dr["MarginWarranty"] ? (Boolean?)null : Convert.ToBoolean(dr["MarginWarranty"]);
                                W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                                W.EquipmentHeaderID = DBNull.Value == dr["EquipmentHeaderID"] ? (long?)null : Convert.ToInt64(dr["EquipmentHeaderID"]);
                                W.Model = Convert.ToString(dr["Model"]);
                                W.PscID = Convert.ToString(dr["PscID"]);
                                
                                W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                                HeaderID = W.WarrantyInvoiceHeaderID;

                                W.InvoiceItems = new List<PDMS_WarrantyInvoiceItem>();
                            }
                            PDMS_WarrantyInvoiceItem item = new PDMS_WarrantyInvoiceItem();
                            item.WarrantyInvoiceItemID = Convert.ToInt64(dr["WarrantyInvoiceItemID"]);

                            item.Item = Convert.ToString(dr["Item"]);
                            item.RefDocID = Convert.ToString(dr["RefDocID"]);
                            item.Material = Convert.ToString(dr["Material"]);
                           // item.MaterialGroup = Convert.ToString(dr["MaterialGroup"]);
                            item.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);
                            item.DeliveryNumber = Convert.ToString(dr["DeliveryNumber"]);
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

                            //item.Approved3Amount = DBNull.Value == dr["Approved3Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Approved3Amount"]);
                            //item.Approved3Remarks = Convert.ToString(dr["Approved3Remarks"]);


                            item.WarrantyMaterialReturnStatus = new PDMS_WarrantyMaterialReturnStatus();
                            if (DBNull.Value != dr["WarrantyMaterialReturnStatusID"])
                            {
                                item.WarrantyMaterialReturnStatus.WarrantyMaterialReturnStatusID = Convert.ToInt32(dr["WarrantyMaterialReturnStatusID"]);
                                item.WarrantyMaterialReturnStatus.WarrantyMaterialReturnStatus = Convert.ToString(dr["WarrantyMaterialReturnStatus"]);
                            }
                            item.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
                            item.TsirID = Convert.ToString(dr["TsirID"]);
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
        public Boolean ApproveWarrantyClaims1(List<PDMS_WarrantyInvoiceItem> Claims, int ApprovedBy, int ApproveLevel)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (PDMS_WarrantyInvoiceItem Claim in Claims)
                    {
                        ApproveWarrantyClaim1(Claim, ApprovedBy, ApproveLevel);
                    }
                    scope.Complete();
                }
                return true;
            }
            catch
            { }
            return false;
        }
        private void ApproveWarrantyClaim1(PDMS_WarrantyInvoiceItem Claim, int ApprovedBy, int ApproveLevel)
        {

            DbParameter WarrantyInvoiceHeaderIDP = provider.CreateParameter("WarrantyInvoiceHeaderID", Claim.WarrantyInvoiceHeaderID, DbType.Int64);
            DbParameter WarrantyInvoiceItemIDP = provider.CreateParameter("WarrantyInvoiceItemID", Claim.WarrantyInvoiceItemID, DbType.Int64);
            DbParameter ApprovedByP = provider.CreateParameter("ApprovedBy", ApprovedBy, DbType.Int32);
            DbParameter ApproveLevelP = provider.CreateParameter("ApproveLevel", ApproveLevel, DbType.Int32);
            DbParameter MaterialStatus = provider.CreateParameter("MaterialStatus", Claim.MaterialStatus, DbType.String);
            DbParameter Approved1Amount = provider.CreateParameter("ApprovedAmount", Claim.Approved1Amount, DbType.Decimal);
            DbParameter MaterialStatusRemarks1 = provider.CreateParameter("MaterialStatusRemarks", Claim.MaterialStatusRemarks1, DbType.String);
            DbParameter Approved1Remarks = provider.CreateParameter("ApprovedRemarks", Claim.Approved1Remarks, DbType.String);

            DbParameter[] Params = new DbParameter[8] { WarrantyInvoiceHeaderIDP, WarrantyInvoiceItemIDP, ApprovedByP, ApproveLevelP, MaterialStatus, Approved1Amount, MaterialStatusRemarks1, Approved1Remarks };
            try
            {

                provider.Insert("ZDMS_ApproveWarrantyClaim1", Params);

            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaim", "ZDMS_ApproveWarrantyClaim", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaim", " ZDMS_ApproveWarrantyClaim", ex);
            }

        }
          
        public List<PDMS_WarrantyStatus> GetWarrantyClaimStatus()
        {
            List<PDMS_WarrantyStatus> Status = new List<PDMS_WarrantyStatus>();
            try
            {
                using (DataSet ds = provider.Select("ZDMS_GetWarrantyClaimStatus"))
                {
                    if (ds != null)
                    {

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Status.Add(new PDMS_WarrantyStatus()
                            {
                                Status = Convert.ToString(dr["Status"]).Trim(),
                                StatusID = Convert.ToInt32(dr["StatusID"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Status;
        }
       
        public List<PDMS_WarrantyInvoiceHeader> GetWarrantyClaimForGenerateInvoiceAbove50k(string DealerCode, int? Year, int? Month, string ClaimNumber)
        {
            List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
            PDMS_WarrantyInvoiceHeader W = null;

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", DealerCode, DbType.String);
            DbParameter YearP = provider.CreateParameter("Year", Year, DbType.Int16);
            DbParameter MonthP = provider.CreateParameter("Month", Month, DbType.Int16);
            DbParameter ClaimNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(ClaimNumber) ? null : ClaimNumber, DbType.String);
            DbParameter[] Params = new DbParameter[4] { DealerCodeP, YearP, MonthP, ClaimNumberP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimForGenerateInvoiceAbove50k", Params))
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
                                W.ClaimStatus = Convert.ToString(dr["Status"]).Trim();
                                W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                                W.MarginWarranty = DBNull.Value == dr["MarginWarranty"] ? (Boolean?)null : Convert.ToBoolean(dr["MarginWarranty"]);
                                W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                                W.Model = Convert.ToString(dr["Model"]);
                                W.PscID = Convert.ToString(dr["PscID"]);
                                W.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
                                W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                                W.AnnexureNumber = Convert.ToString(dr["AnnexureNumber"]);
                                HeaderID = W.WarrantyInvoiceHeaderID;
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
                            item.Approved2Amount = DBNull.Value == dr["Approved2Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Approved2Amount"]);

                            item.Approved1Remarks = Convert.ToString(dr["Approved1Remarks"]);
                            item.Approved2Remarks = Convert.ToString(dr["Approved2Remarks"]);
                            item.SAPDoc = Convert.ToString(dr["SAPDoc"]); 
                            item.SAPInvoiceValue = DBNull.Value == dr["SAPInvoiceValue"] ? (decimal?)null : Convert.ToDecimal(dr["SAPInvoiceValue"]);
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

        public Boolean CancelWarrantyClaims(string InvoiceNumber, int CanceledBy)
        {
            try
            { 
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", InvoiceNumber, DbType.String);
                    DbParameter CanceledByP = provider.CreateParameter("CanceledBy", CanceledBy, DbType.Int32);
                    DbParameter CancelT = provider.CreateParameter("CancelT", true, DbType.Boolean);
                    DbParameter[] Params = new DbParameter[3] { InvoiceNumberP, CanceledByP, CancelT };
                    provider.Insert("ZDMS_CancelWarrantyClaim", Params);
                    scope.Complete();
                } 
                return true;
            }
            catch (Exception d)
            {
            }
            return false;
        }
      
        public PSAPDocumentNumber getSAPDocumentNumber(string InvoiceNumber)
        {
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("Warranty/getSAPDocumentNumber?InvoiceNumber=" + InvoiceNumber));
            if (Results.Status == PApplication.Failure)
            {
                throw new Exception(Results.Message);
            }
            return JsonConvert.DeserializeObject<PSAPDocumentNumber>(JsonConvert.SerializeObject(Results.Data));
        }

        public List<PClaimDeviation> GetDeviatedClaimReport(int? DealerID, string ClaimNumber, string RequestedDateF, string RequestedDateT, int DeviationTypeID)
        { 
            string endPoint = "Warranty/GetDeviatedClaimReport?DealerID=" + DealerID + "&ClaimNumber=" + ClaimNumber
                + "&RequestedDateF=" + RequestedDateF + "&RequestedDateT=" + RequestedDateT + "&DeviationTypeID=" + DeviationTypeID;
            return JsonConvert.DeserializeObject<List<PClaimDeviation>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public PApiResult InsertDeviatedClaimRequestForApproval(string ClaimNumber, int DeviationTypeID)
        {
            string endPoint = "Warranty/InsertDeviatedClaimRequestForApproval?ClaimNumber=" + ClaimNumber + "&DeviationTypeID=" + DeviationTypeID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));

        }
        public List<PClaimDeviation> GetDeviatedClaimForApproval(int? DealerID, string IcTicketNumber, string ClaimNumber, string RequestedDateF, string RequestedDateT, int DeviationTypeID)
        {
            string endPoint = "Warranty/GetDeviatedClaimForApproval?DealerID=" + DealerID + "&IcTicketNumber=" + IcTicketNumber + "&ClaimNumber=" + ClaimNumber 
                + "&RequestedDateF=" + RequestedDateF + "&RequestedDateT=" + RequestedDateT + "&DeviationTypeID=" + DeviationTypeID ;

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));

            if (Results.Status == PApplication.Failure)
            {
                throw new Exception(Results.Message); 
            }
            return JsonConvert.DeserializeObject<List<PClaimDeviation>>(JsonConvert.SerializeObject(Results.Data));

        }
        public PApiResult ApproveOrRejectDeviatedClaimRequest(long ClaimDeviationID, Boolean? IsApproved, Boolean? IsRejected)
        {
            string endPoint = "Warranty/ApproveOrRejectDeviatedClaimRequest?ClaimDeviationID=" + ClaimDeviationID + "&IsApproved=" + IsApproved + "&IsRejected=" + IsRejected;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public Boolean GetDeviatedClaimToVerify(string ClaimNumber, int DeviationTypeID)
        {
            string endPoint = "Warranty/GetDeviatedClaimToVerify?ClaimNumber=" + ClaimNumber + "&DeviationTypeID=" + DeviationTypeID;
            return JsonConvert.DeserializeObject<Boolean>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataTable ZYA_GetModel( string Division)
        {
            string endPoint = "Warranty/ZYA_GetModel?Division=" + Division;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataSet ZYA_GetWarrantyCostPerMachine(string ManufacturingDateF, string ManufacturingDateT, string AsOnDate, string Dealer, string Region, string ServiceType, string Division, string Model, string Gragh, int Excel = 0)
        {
            string endPoint = "Warranty/ZYA_GetWarrantyCostPerMachine?ManufacturingDateF=" + ManufacturingDateF + "&ManufacturingDateT=" + ManufacturingDateT + "&AsOnDate=" + AsOnDate
                + "&Dealer=" + Dealer + "&Region=" + Region + "&ServiceType=" + ServiceType + "&Division=" + Division + "&Model=" + Model + "&Gragh=" + Gragh + "&Excel=" + Excel;
            return JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataSet ZYA_GetIncidentPer100Machine(string ManufacturingDateF, string ManufacturingDateT, string AsOnDate, string Dealer, string Region, string ServiceType, string Division, string Model, string Gragh, int Excel = 0)
        {
            string endPoint = "Warranty/ZYA_GetIncidentPer100Machine?ManufacturingDateF=" + ManufacturingDateF + "&ManufacturingDateT=" + ManufacturingDateT + "&AsOnDate=" + AsOnDate
                + "&Dealer=" + Dealer + "&Region=" + Region + "&ServiceType=" + ServiceType + "&Division=" + Division + "&Model=" + Model + "&Gragh=" + Gragh + "&Excel=" + Excel;
            return JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}