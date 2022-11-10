using DataAccess;
using Properties;
using SapIntegration;
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
        private IDataAccess providerReport;
        public BDMS_WarrantyClaim()
        {
            provider = new ProviderFactory().GetProvider();
            providerReport = new ProviderFactory().GetProvider(true);
        }
 
        public List<PDMS_WarrantySAPInvoice> GetWarrantySAPInvoice()
        {
            List<PDMS_WarrantySAPInvoice> Ws = new List<PDMS_WarrantySAPInvoice>();
            PDMS_WarrantySAPInvoice W = null;

            try
            {
                using (DataSet DataSet = provider.Select("ZDMS_GetWarrantySAPInvoice"))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_WarrantySAPInvoice();
                            Ws.Add(W);
                            W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            W.SAPDoc = Convert.ToString(dr["SAPDoc"]);

                            W.SAPInvoiceValue = DBNull.Value == dr["SAPInvoiceValue"] ? (decimal?)null : Convert.ToDecimal(dr["SAPInvoiceValue"]);
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
        public List<PDMS_ClaimInvoiceHeader> GetClaimInvoice(string ICTicketID, DateTime? ICTicketDateF, DateTime? ICTicketDateT, string ClaimID, DateTime? ClaimDateF, DateTime? ClaimDateT, string DealerCode, int? StatusID, string TSIRNumber, int UserID)
        {
            List<PDMS_ClaimInvoiceHeader> Ws = new List<PDMS_ClaimInvoiceHeader>();
            PDMS_ClaimInvoiceHeader W = null;
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
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetClaimInvoicePrint", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        string Invoice = "";
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {

                            if (Invoice.Trim() != Convert.ToString(dr["InvoiceNumber"]).Trim())
                            {
                                W = new PDMS_ClaimInvoiceHeader();
                                Ws.Add(W);
                                W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                W.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                                Invoice = W.InvoiceNumber;

                                W.WarrantyClaimHeaderID = Convert.ToInt64(dr["WarrantyInvoiceHeaderID"]);
                                //    W.ClaimID = Convert.ToString(dr["ClaimID"]).Trim();
                                //   W.ClaimDate = Convert.ToDateTime(dr["ClaimDate"]);
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
                                // W.Approved1By = new PUser() { UserID = DBNull.Value == dr["Approved1By"] ? (int?)null : Convert.ToInt32(dr["Approved1By"]) };
                                W.Approved1On = DBNull.Value == dr["Approved1On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved1On"]);

                                // W.Approved2By = DBNull.Value == dr["Approved2By"] ? (int?)null : Convert.ToInt32(dr["Approved2By"]);

                                W.Approved2By = new PUser();
                                if (dr["Approved2By"] != DBNull.Value)
                                {
                                    W.Approved2By.ContactName = Convert.ToString(dr["Approved2By"]);
                                }

                                W.Approved2On = DBNull.Value == dr["Approved2On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved2On"]);
                                W.Status = Convert.ToString(dr["Status"]).Trim();
                                W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                                W.MarginWarranty = DBNull.Value == dr["MarginWarranty"] ? (Boolean?)null : Convert.ToBoolean(dr["MarginWarranty"]);
                                W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                                W.PscID = Convert.ToString(dr["PscID"]);
                                W.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
                                W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                                W.DateOfCommissioning = DBNull.Value == dr["DateOfCommissioning"] ? (DateTime?)null : Convert.ToDateTime(dr["DateOfCommissioning"]);
                                W.ReasonForFailure = Convert.ToString(dr["ReasonForFailure"]);
                                W.ClaimItems = new List<PDMS_ClaimInvoiceItem>();
                            }
                            PDMS_ClaimInvoiceItem item = new PDMS_ClaimInvoiceItem();
                            item.WarrantyClaimItemID = Convert.ToInt64(dr["WarrantyInvoiceItemID"]);

                            item.Item = Convert.ToString(dr["Item"]);
                            item.RefDocID = Convert.ToString(dr["RefDocID"]);
                            item.Material = Convert.ToString(dr["Material"]);
                            item.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);
                            item.Category = Convert.ToString(dr["Category"]);
                            item.Model = Convert.ToString(dr["Model"]);
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

                            W.ClaimItems.Add(item);
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

        public int InsertWarrantyInvoice(string Fillter)
        {
            int count = 0;
            try
            {
                //  DateTime dtF = DateTime.Now.AddMonths(-2);
                DateTime dtF = DateTime.Now.AddDays(-100);
                DateTime dtT = DateTime.Now;


                List<PDMS_WarrantyInvoiceHeader> WisDms = GetWarrantyInvoiceIntegration(Fillter);

                WisDms.RemoveAll(m => (DateTime)m.InvoiceDate < dtF);
                List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
                PDMS_WarrantyInvoiceHeader W = null;
                PDMS_WarrantyInvoiceItem item = null;
                string InvoiceNumber = "";
                string ICTicketID = "";
                List<PDMS_WarrantyInvoiceHeader> HMRs = GetHMR();
                List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerName();
                List<PDMS_WarrantyInvoiceHeader> Model_WarrantyEndDate = GetModel_WarrantyEndDate();
                foreach (PDMS_WarrantyInvoiceHeader WiDms in WisDms)
                {
                    if ((InvoiceNumber != WiDms.InvoiceNumber) || (ICTicketID != WiDms.ICTicketID))
                    {
                        W = new PDMS_WarrantyInvoiceHeader();
                        Ws.Add(W);
                        InvoiceNumber = WiDms.InvoiceNumber;
                        ICTicketID = WiDms.ICTicketID;
                        W.InvoiceNumber = WiDms.InvoiceNumber;
                        W.InvoiceDate = WiDms.InvoiceDate;
                        W.ICTicketID = WiDms.ICTicketID;
                        W.ICTicketDate = WiDms.ICTicketDate;

                        W.CustomerCode = WiDms.CustomerCode;

                        W.CustomerName = (Customer.Where(m => m.CustomerCode == WiDms.CustomerCode)).ToList()[0].CustomerName;
                        W.DealerCode = WiDms.DealerCode;
                        W.DealerName = WiDms.DealerName;
                        W.InvoiceStatus = WiDms.InvoiceStatus;

                        W.PscID = WiDms.PscID;

                        W.HMR = 0;
                        if ((HMRs.Where(m => m.PscID == W.PscID).Count() != 0))
                        {
                            W.HMR = (HMRs.Where(m => m.PscID == W.PscID)).ToList()[0].HMR;
                        }
                        W.MarginWarranty = WiDms.MarginWarranty;
                        W.MachineSerialNumber = WiDms.MachineSerialNumber;

                        W.FSRNumber = WiDms.FSRNumber;
                        W.TSIRNumber = WiDms.TSIRNumber;
                        W.RestoreDate = WiDms.RestoreDate;
                        W.Location = WiDms.Location;
                        W.Application = WiDms.Application;
                        if ((Model_WarrantyEndDate.Where(m => m.PscID == W.PscID).Count() != 0))
                        {
                            var psc = (Model_WarrantyEndDate.Where(m => m.PscID == W.PscID)).ToList()[0];
                            W.WarrantyEndDate = psc.WarrantyEndDate;
                            W.Model = psc.Model;
                            W.DateOfCommissioning = psc.DateOfCommissioning;
                            W.ReasonForFailure = psc.ReasonForFailure;
                        }
                        W.InvoiceItems = new List<PDMS_WarrantyInvoiceItem>();
                    }
                    item = new PDMS_WarrantyInvoiceItem();
                    item.DeliveryNumber = WiDms.InvoiceItem.DeliveryNumber;
                    item.Item = WiDms.InvoiceItem.Item;
                    item.RefDocID = WiDms.InvoiceItem.RefDocID;
                    item.Material = WiDms.InvoiceItem.Material;
                    if (!string.IsNullOrEmpty(WiDms.InvoiceItem.MaterialDesc))
                    {
                        item.MaterialDesc = WiDms.InvoiceItem.MaterialDesc;
                    }
                    else
                    {
                        var M = (from m in new SMaterial().getMaterial(WiDms.InvoiceItem.Material) where m.MaterialCode == WiDms.InvoiceItem.Material select m);
                        if (M.Count() == 1)
                        {
                            item.MaterialDesc = M.ToList()[0].MaterialDescription;
                        }
                    }
                    item.Qty = WiDms.InvoiceItem.Qty;
                    item.UnitOM = WiDms.InvoiceItem.UnitOM;
                    item.Amount = WiDms.InvoiceItem.Amount;
                    item.BaseTax = WiDms.InvoiceItem.BaseTax;
                    item.HSNCode = WiDms.InvoiceItem.HSNCode;
                    item.TaxPercentage = WiDms.InvoiceItem.TaxPercentage;

                    item.BriefDescriptionOfJob = WiDms.InvoiceItem.BriefDescriptionOfJob;
                    item.Item = WiDms.InvoiceItem.Item;
                    W.InvoiceItems.Add(item);
                }

                foreach (PDMS_WarrantyInvoiceHeader ch in Ws)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {

                            long WarrantyClaimHeaderID = insertWarrantyInvoiceHeader(ch);
                            if (WarrantyClaimHeaderID != 0)
                            {
                                foreach (PDMS_WarrantyInvoiceItem ci in ch.InvoiceItems)
                                {
                                    ci.WarrantyInvoiceHeaderID = WarrantyClaimHeaderID;
                                    insertWarrantyInvoiceItem(ci);
                                    count = count + 1;
                                }
                            }
                            scope.Complete();
                        }
                    }
                    catch (Exception e2)
                    {
                        new FileLogger().LogMessage("BDMS_WarrantyClaim", "InsertWarrantyInvoice : " + ch.InvoiceNumber, e2);
                    }
                }
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_WarrantyClaim", "InsertWarrantyInvoice : ", e1);
            }
            return count;
        }

        public List<PDMS_WarrantyInvoiceHeader> GetWarrantyInvoiceIntegration(string Fillter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_WarrantyInvoiceHeader> SOIs = new List<PDMS_WarrantyInvoiceHeader>();
            try
            {

                // string query = "SELECT  * from pr_get_warranty_claim_Integration(" + filter + ")";
                string query = PQuery.GetWarrantyInvoiceIntegration;
                query = query.Replace("@@Fillter", Fillter);
                new FileLogger().LogMessageService("BDMS_WarrantyClaim", query, null);
                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                new FileLogger().LogMessageService("BDMS_WarrantyClaim", dt.Rows.Count.ToString(), null);
                PDMS_WarrantyInvoiceHeader SOI = new PDMS_WarrantyInvoiceHeader();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_WarrantyInvoiceHeader();

                    SOI.InvoiceNumber = Convert.ToString(dr["Invoice_Number"]);
                    SOI.InvoiceDate = DBNull.Value == dr["Invoice_date"] ? (DateTime?)null : Convert.ToDateTime(dr["Invoice_date"]);
                    SOI.ICTicketID = Convert.ToString(dr["IC_Ticket"]);
                    SOI.ICTicketDate = DBNull.Value == dr["f_ic_ticket_date"] ? (DateTime?)null : Convert.ToDateTime(dr["f_ic_ticket_date"]);

                    SOI.DealerCode = Convert.ToString(dr["Dealer_code"]);
                    SOI.DealerName = Convert.ToString(dr["Dealer_Name"]);
                    SOI.CustomerCode = Convert.ToString(dr["BP_Code"]);
                    SOI.MachineSerialNumber = Convert.ToString(dr["Machine_Serial_No"]);
                    SOI.MarginWarranty = DBNull.Value == dr["r_goodwill_warranty"] ? (Boolean?)null : Convert.ToBoolean(dr["r_goodwill_warranty"]);
                    SOI.InvoiceStatus = Convert.ToString(dr["Invoice_Status"]);
                    SOI.PscID = Convert.ToString(dr["p_sc_id"]);
                    SOI.FSRNumber = Convert.ToString(dr["r_fsr_no_date"]);
                    SOI.TSIRNumber = Convert.ToString(dr["r_tsir_no"]);
                    SOI.RestoreDate = DBNull.Value == dr["Restore_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Restore_Date"]);
                    SOI.Location = Convert.ToString(dr["location"]);
                    SOI.Application = Convert.ToString(dr["application"]);
                    SOI.InvoiceItem = new PDMS_WarrantyInvoiceItem();
                    SOI.InvoiceItem.DeliveryNumber = Convert.ToString(dr["Delivery_ID"]);
                    SOI.InvoiceItem.Material = Convert.ToString(dr["material"]);
                    SOI.InvoiceItem.MaterialDesc = Convert.ToString(dr["material_desc"]);
                    SOI.InvoiceItem.UnitOM = Convert.ToString(dr["uom"]);

                    SOI.InvoiceItem.Qty = DBNull.Value == dr["qty"] ? 1 : Convert.ToDecimal(dr["qty"]);


                    SOI.InvoiceItem.Amount = DBNull.Value == dr["Base_Value"] ? (decimal?)null : Convert.ToDecimal(dr["Base_Value"]);
                    SOI.InvoiceItem.BaseTax = DBNull.Value == dr["Total"] ? (decimal?)null : Convert.ToDecimal(dr["Total"]);
                    SOI.InvoiceItem.HSNCode = Convert.ToString(dr["HSN_CODE"]);
                    SOI.InvoiceItem.Item = Convert.ToString(dr["p_inv_item"]);
                    SOI.InvoiceItem.TaxPercentage = DBNull.Value == dr["Tax_Percentage"] ? (decimal?)null : Convert.ToDecimal(dr["Tax_Percentage"]);
                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_WarrantyClaim", "GetWarrantyInvoiceIntegration : ", ex);
                throw ex;
            }
            return SOIs;
        }
        public long insertWarrantyInvoiceHeader(PDMS_WarrantyInvoiceHeader ClaimHeader)
        {


            int success = 0;
            long WarrantyInvoiceHeaderID = 0;

            DbParameter InvoiceNumber = provider.CreateParameter("InvoiceNumber", ClaimHeader.InvoiceNumber, DbType.String);
            DbParameter InvoiceDate = provider.CreateParameter("InvoiceDate", ClaimHeader.InvoiceDate, DbType.DateTime);

            DbParameter ICTicketID = provider.CreateParameter("ICTicketID", ClaimHeader.ICTicketID, DbType.String);
            DbParameter ICTicketDate = provider.CreateParameter("ICTicketDate", ClaimHeader.ICTicketDate, DbType.DateTime);
            DbParameter CustomerCode = provider.CreateParameter("CustomerCode", ClaimHeader.CustomerCode, DbType.String);
            DbParameter CustomerName = provider.CreateParameter("CustomerName", ClaimHeader.CustomerName, DbType.String);
            DbParameter DealerCode = provider.CreateParameter("DealerCode", ClaimHeader.DealerCode, DbType.String);

            DbParameter HMR = provider.CreateParameter("HMR", ClaimHeader.HMR, DbType.Int32);
            DbParameter Margin = provider.CreateParameter("MarginWarranty", ClaimHeader.MarginWarranty, DbType.Boolean);
            DbParameter InvoiceStatus = provider.CreateParameter("InvoiceStatus", ClaimHeader.InvoiceStatus, DbType.String);
            DbParameter MachineSerialNumber = provider.CreateParameter("MachineSerialNumber", ClaimHeader.MachineSerialNumber, DbType.String);
            DbParameter Model = provider.CreateParameter("Model", ClaimHeader.Model, DbType.String);
            DbParameter PscID = provider.CreateParameter("PscID", ClaimHeader.PscID, DbType.String);
            DbParameter TSIRNumber = provider.CreateParameter("TSIRNumber", ClaimHeader.TSIRNumber, DbType.String);
            DbParameter RestoreDate = provider.CreateParameter("RestoreDate", ClaimHeader.RestoreDate, DbType.DateTime);

            DbParameter Location = provider.CreateParameter("Location", ClaimHeader.Location, DbType.String);
            DbParameter Application = provider.CreateParameter("Application", ClaimHeader.Application, DbType.String);
            DbParameter WarrantyEndDate = provider.CreateParameter("WarrantyEndDate", ClaimHeader.WarrantyEndDate, DbType.DateTime);

            DbParameter ReasonForFailure = provider.CreateParameter("ReasonForFailure", ClaimHeader.ReasonForFailure, DbType.String);
            DbParameter DateOfCommissioning = provider.CreateParameter("DateOfCommissioning", ClaimHeader.DateOfCommissioning, DbType.DateTime);
            DbParameter FSRNumber = provider.CreateParameter("FSRNumber", ClaimHeader.FSRNumber, DbType.String);


            DbParameter WarrantyClaimHeader = provider.CreateParameter("OutValue", WarrantyInvoiceHeaderID, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[22] { InvoiceNumber, InvoiceDate, ICTicketID,ICTicketDate, CustomerCode, CustomerName, DealerCode, WarrantyClaimHeader,
                HMR, Margin, InvoiceStatus, MachineSerialNumber,Model,PscID,TSIRNumber,RestoreDate
                ,Location,Application,WarrantyEndDate,ReasonForFailure,DateOfCommissioning,FSRNumber};
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertWarrantyInvoiceHeader", Params);
                    if (success != 0)
                    {
                        WarrantyInvoiceHeaderID = Convert.ToInt64(WarrantyClaimHeader.Value);
                    }
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaim", "insertWarrantyInvoiceHeader", sqlEx);
                WarrantyInvoiceHeaderID = 0;
                throw;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaim", " insertWarrantyInvoiceHeader", ex);
                WarrantyInvoiceHeaderID = 0;
                throw;
            }
            return WarrantyInvoiceHeaderID;
        }
        public void insertWarrantyInvoiceItem(PDMS_WarrantyInvoiceItem ClaimItem)
        {


            DbParameter DeliveryNumber;
            DbParameter WarrantyInvoiceHeaderID = provider.CreateParameter("WarrantyInvoiceHeaderID", ClaimItem.WarrantyInvoiceHeaderID, DbType.Int64);
            DbParameter Item = provider.CreateParameter("Item", ClaimItem.Item, DbType.Int32);
            DbParameter RefDocID = provider.CreateParameter("RefDocID", ClaimItem.RefDocID, DbType.String);
            DbParameter Material = provider.CreateParameter("Material", ClaimItem.Material, DbType.String);
            DbParameter MaterialDesc = provider.CreateParameter("MaterialDesc", ClaimItem.MaterialDesc, DbType.String);
            DbParameter Qty = provider.CreateParameter("Qty", ClaimItem.Qty, DbType.Decimal);
            DbParameter UnitOM = provider.CreateParameter("UnitOM", ClaimItem.UnitOM, DbType.String);
            DbParameter Amount = provider.CreateParameter("Amount", ClaimItem.Amount, DbType.Decimal);
            DbParameter BaseTax = provider.CreateParameter("BaseTax", ClaimItem.BaseTax, DbType.Decimal);
            DbParameter HSNCode = provider.CreateParameter("HSNCode", ClaimItem.HSNCode, DbType.String);
            DbParameter TaxPercentage = provider.CreateParameter("TaxPercentage", ClaimItem.TaxPercentage, DbType.String);
            DbParameter BriefDescriptionOfJob = provider.CreateParameter("BriefDescriptionOfJob", ClaimItem.BriefDescriptionOfJob, DbType.String);
            if (!string.IsNullOrEmpty(ClaimItem.DeliveryNumber))
                DeliveryNumber = provider.CreateParameter("DeliveryNumber", ClaimItem.DeliveryNumber, DbType.String);
            else
                DeliveryNumber = provider.CreateParameter("DeliveryNumber", DBNull.Value, DbType.String);

            DbParameter[] Params = new DbParameter[13] { WarrantyInvoiceHeaderID, Item, RefDocID, Material, MaterialDesc, Qty, UnitOM, Amount, BaseTax, HSNCode, TaxPercentage, BriefDescriptionOfJob, DeliveryNumber };
            try
            {
                provider.Insert("ZDMS_InsertWarrantyInvoiceItem", Params);
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaim", "insertWarrantyInvoiceItem", sqlEx);
                throw;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaim", " insertWarrantyInvoiceItem", ex);
                throw;
            }
        }
        public List<PDMS_WarrantyInvoiceHeader> GetWarrantyClaimReport(string ICTicketID, DateTime? ICTicketDateF, DateTime? ICTicketDateT, string InvoiceNumber,
        DateTime? InvoiceDateF, DateTime? InvoiceDateT, string DealerCode, int? StatusID,
          DateTime? AnnexureF, DateTime? AnnexureT, string TSIRNumber,string CustomerCode,string MachineSerialNumber, Boolean IsAbove50K, int? UserID)
        {
            List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
            PDMS_WarrantyInvoiceHeader W = null;
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", string.IsNullOrEmpty(ICTicketID) ? null : ICTicketID, DbType.String);
            DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
            DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
            DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
            DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);


            //DbParameter Approved1DateFP = provider.CreateParameter("Approved1DateF", Approved1DateF, DbType.DateTime);
            //DbParameter Approved1DateTP = provider.CreateParameter("Approved1DateT", Approved1DateT, DbType.DateTime);
            //DbParameter Approved2DateFP = provider.CreateParameter("Approved2DateF", Approved2DateF, DbType.DateTime);
            //DbParameter Approved2DateTP = provider.CreateParameter("Approved2DateT", Approved2DateT, DbType.DateTime);


            DbParameter AnnexureFP = provider.CreateParameter("AnnexureF", AnnexureF, DbType.DateTime);
            DbParameter AnnexureTP = provider.CreateParameter("AnnexureT", AnnexureT, DbType.DateTime);



            DbParameter TSIRNumberP = provider.CreateParameter("TSIRNumber", string.IsNullOrEmpty(TSIRNumber) ? null : TSIRNumber, DbType.String);

            DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
            DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", string.IsNullOrEmpty(MachineSerialNumber) ? null : MachineSerialNumber, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("IsAbove50K", IsAbove50K, DbType.Boolean);
            DbParameter IsAbove50KP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[15] { ICTicketIDP, ICTicketDateFP, ICTicketDateTP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerCodeP, StatusIDP,
               AnnexureFP,AnnexureTP , TSIRNumberP,CustomerCodeP,MachineSerialNumberP,IsAbove50KP, UserIDP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimReport", Params, (20) * 60))
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
                                W.ICTicket = new PDMS_ICTicket()
                                {
                                    ServiceType = new PDMS_ServiceType() { ServiceType = Convert.ToString(dr["ServiceType"]) },
                                    ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]),
                                    Equipment = new PDMS_EquipmentHeader() { CommissioningOn = DBNull.Value == dr["CommissioningOn"] ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]) },

                                };

                                W.Approved1By = new PUser();
                                if (dr["Approved1By"] != DBNull.Value)
                                {
                                    W.Approved1By.ContactName = Convert.ToString(dr["Approved1By"]);
                                }
                                // W.Approved1By = new PUser() { UserID = DBNull.Value == dr["Approved1By"] ? (int?)null : Convert.ToInt32(dr["Approved1By"]) };
                                W.Approved1On = DBNull.Value == dr["Approved1On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved1On"]);

                                //  W.Approved2By = DBNull.Value == dr["Approved2By"] ? (int?)null : Convert.ToInt32(dr["Approved2By"]);

                                W.Approved2By = new PUser();
                                if (dr["Approved2By"] != DBNull.Value)
                                {
                                    W.Approved2By.ContactName = Convert.ToString(dr["Approved2By"]);
                                }
                                W.Approved2On = DBNull.Value == dr["Approved2On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved2On"]);

                                W.Approved3By = new PUser();
                                if (dr["Approved3By"] != DBNull.Value)
                                {
                                    W.Approved3By.ContactName = Convert.ToString(dr["Approved3By"]);
                                }
                                W.Approved3On = DBNull.Value == dr["Approved3On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved3On"]);

                                W.ClaimStatus = Convert.ToString(dr["Status"]).Trim();
                                W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                                W.MarginWarranty = DBNull.Value == dr["MarginWarranty"] ? (Boolean?)null : Convert.ToBoolean(dr["MarginWarranty"]);
                                W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                                W.Model = Convert.ToString(dr["Model"]);
                                W.PscID = Convert.ToString(dr["PscID"]);
                              //  W.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
                                W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                                W.AnnexureNumber = Convert.ToString(dr["AnnexureNumber"]);
                                W.AnnexureDate = DBNull.Value == dr["AnnexureDate"] ? (DateTime?)null : Convert.ToDateTime(dr["AnnexureDate"]);
                                W.AcInvoiceNumber = Convert.ToString(dr["AcInvoiceNumber"]);
                                W.AcInvoiceDate = DBNull.Value == dr["AcInvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["AcInvoiceDate"]);
                                HeaderID = W.WarrantyInvoiceHeaderID;

                                W.InvoiceItems = new List<PDMS_WarrantyInvoiceItem>();
                            }
                            PDMS_WarrantyInvoiceItem item = new PDMS_WarrantyInvoiceItem();
                            item.WarrantyInvoiceItemID = Convert.ToInt64(dr["WarrantyInvoiceItemID"]);

                            item.Item = Convert.ToString(dr["Item"]);
                            item.RefDocID = Convert.ToString(dr["RefDocID"]);
                            item.Material = Convert.ToString(dr["Material"]);
                            item.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);
                            item.DeliveryNumber = Convert.ToString(dr["DeliveryNumber"]);
                            item.Category = Convert.ToString(dr["Category"]);

                            item.HSNCode = Convert.ToString(dr["HSNCode"]);
                            item.Qty = Convert.ToDecimal(dr["Qty"]);
                            item.Per = DBNull.Value == dr["Per"] ? (decimal?)null : Convert.ToDecimal(dr["Per"]);
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
                            item.Approved3Amount = DBNull.Value == dr["Approved3Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Approved3Amount"]);
                            item.Approved3Remarks = Convert.ToString(dr["Approved3Remarks"]);


                            //   item.InvoiceNumberNew = Convert.ToString(dr["InvoiceNumberNew"]);                           

                            item.SAPDoc = Convert.ToString(dr["SAPDoc"]);
                            item.SAPPostingDate = DBNull.Value == dr["SAPPostingDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SAPPostingDate"]);
                            item.SAPInvoiceValue = DBNull.Value == dr["SAPInvoiceValue"] ? (decimal?)null : Convert.ToDecimal(dr["SAPInvoiceValue"]);
                            item.SAPClearingDocument = Convert.ToString(dr["SAPClearingDocument"]);
                            item.DeliveryNumber = Convert.ToString(dr["DeliveryNumber"]);
                            item.AnnexureNumber = Convert.ToString(dr["AnnexureNumber"]);
                            item.WarrantyMaterialReturnStatus = new PDMS_WarrantyMaterialReturnStatus();
                            if (DBNull.Value != dr["WarrantyMaterialReturnStatusID"])
                            {
                                item.WarrantyMaterialReturnStatus.WarrantyMaterialReturnStatusID = Convert.ToInt32(dr["WarrantyMaterialReturnStatusID"]);
                                item.WarrantyMaterialReturnStatus.WarrantyMaterialReturnStatus = Convert.ToString(dr["WarrantyMaterialReturnStatus"]);
                            }
                            item.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
                          //  item.TSIRDate = DBNull.Value == dr["TSIRDate"] ? (DateTime?)null : Convert.ToDateTime(dr["TSIRDate"]);
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
        public List<PDMS_WarrantyTicket> GetWarrantyClaimReportByICTicket1(string ICTicketID, DateTime? ICTicketDateF, DateTime? ICTicketDateT, string InvoiceNumber,
        DateTime? InvoiceDateF, DateTime? InvoiceDateT, string DealerCode, int? StatusID, string TSIRNumber, int UserID)
        {
            List<PDMS_WarrantyTicket> Ws = new List<PDMS_WarrantyTicket>();
            PDMS_WarrantyTicket W = null;
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", string.IsNullOrEmpty(ICTicketID) ? null : ICTicketID, DbType.String);
            DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
            DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
            DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
            DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
            DbParameter TSIRNumberP = provider.CreateParameter("TSIRNumber", string.IsNullOrEmpty(TSIRNumber) ? null : TSIRNumber, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[10] { ICTicketIDP, ICTicketDateFP, ICTicketDateTP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerCodeP, StatusIDP, TSIRNumberP, UserIDP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimReportByICTicketID1", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        string HeaderID = "";
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {

                            if (HeaderID != Convert.ToString(dr["ICTicketID"]))
                            {
                                W = new PDMS_WarrantyTicket();
                                Ws.Add(W);

                                W.ICTicketID = Convert.ToString(dr["ICTicketID"]);
                                W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);

                                W.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                                W.CustomerName = Convert.ToString(dr["CustomerName"]);
                                W.DealerCode = Convert.ToString(dr["DealerCode"]);
                                W.DealerName = Convert.ToString(dr["DealerName"]);

                                //  W.Approved2By = DBNull.Value == dr["Approved2By"] ? (int?)null : Convert.ToInt32(dr["Approved2By"]);

                                W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                                W.MarginWarranty = DBNull.Value == dr["MarginWarranty"] ? (Boolean?)null : Convert.ToBoolean(dr["MarginWarranty"]);
                                W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                                W.Model = Convert.ToString(dr["Model"]);
                                W.PscID = Convert.ToString(dr["PscID"]);
                                W.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
                                W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                                HeaderID = W.ICTicketID;

                                W.Invoice = new List<PDMS_WarrantyInvoiceHeader>();
                            }
                            PDMS_WarrantyInvoiceHeader item = new PDMS_WarrantyInvoiceHeader();
                            item.Approved1By = new PUser();
                            if (dr["Approved1By"] != DBNull.Value)
                            {
                                item.Approved1By.ContactName = Convert.ToString(dr["Approved1By"]);
                            }

                            item.Approved1On = DBNull.Value == dr["Approved1On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved1On"]);


                            item.Approved2By = new PUser();
                            if (dr["Approved2By"] != DBNull.Value)
                            {
                                item.Approved2By.ContactName = Convert.ToString(dr["Approved2By"]);
                            }

                            item.Approved2On = DBNull.Value == dr["Approved2On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved2On"]);
                            item.ClaimStatus = Convert.ToString(dr["Status"]).Trim();

                            item.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            item.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                            item.InvoiceItem = new PDMS_WarrantyInvoiceItem();

                            item.InvoiceItem.Item = Convert.ToString(dr["Item"]);
                            item.InvoiceItem.RefDocID = Convert.ToString(dr["RefDocID"]);
                            item.InvoiceItem.Material = Convert.ToString(dr["Material"]);
                            item.InvoiceItem.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);
                            item.InvoiceItem.Category = Convert.ToString(dr["Category"]);

                            item.InvoiceItem.HSNCode = Convert.ToString(dr["HSNCode"]);
                            item.InvoiceItem.Qty = Convert.ToDecimal(dr["Qty"]);
                            item.InvoiceItem.UnitOM = Convert.ToString(dr["UnitOM"]);
                            item.InvoiceItem.MaterialStatus = Convert.ToString(dr["MaterialStatus"]);
                            item.InvoiceItem.MaterialStatusRemarks1 = Convert.ToString(dr["MaterialStatusRemarks1"]);
                            item.InvoiceItem.MaterialStatusRemarks2 = Convert.ToString(dr["MaterialStatusRemarks2"]);
                            item.InvoiceItem.Amount = DBNull.Value == dr["Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Amount"]);
                            item.InvoiceItem.BaseTax = DBNull.Value == dr["BaseTax"] ? (decimal?)null : Convert.ToDecimal(dr["BaseTax"]);
                            item.InvoiceItem.Approved1Amount = DBNull.Value == dr["Approved1Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Approved1Amount"]);
                            item.InvoiceItem.Approved1Remarks = Convert.ToString(dr["Approved1Remarks"]);

                            item.InvoiceItem.Approved2Amount = DBNull.Value == dr["Approved2Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Approved2Amount"]);
                            item.InvoiceItem.Approved2Remarks = Convert.ToString(dr["Approved2Remarks"]);
                            item.InvoiceItem.SAPDoc = Convert.ToString(dr["SAPDoc"]);
                            item.InvoiceItem.SAPPostingDate = DBNull.Value == dr["SAPPostingDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SAPPostingDate"]);
                            item.InvoiceItem.SAPInvoiceValue = DBNull.Value == dr["SAPInvoiceValue"] ? (decimal?)null : Convert.ToDecimal(dr["SAPInvoiceValue"]);

                            W.Invoice.Add(item);
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
        public List<PDMS_WarrantyInvoiceHeader> GetWarrantyClaimApproval(string ICTicketID, DateTime? ICTicketDateF, DateTime? ICTicketDateT, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, string DealerCode, int? StatusID, string TSIRNumber,string DivisionID, int UserID)
        {
            List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
            PDMS_WarrantyInvoiceHeader W = null;
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", string.IsNullOrEmpty(ICTicketID) ? null : ICTicketID, DbType.String);
            DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
            DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
            DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
            DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
            DbParameter TSIRNumberP = provider.CreateParameter("TSIRNumber", string.IsNullOrEmpty(TSIRNumber) ? null : TSIRNumber, DbType.String);
            DbParameter DivisionIDP = provider.CreateParameter("DivisionID", DivisionID, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[11] { ICTicketIDP, ICTicketDateFP, ICTicketDateTP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerCodeP, StatusIDP, TSIRNumberP, DivisionIDP, UserIDP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimApproval", Params))
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
                                W.ICTicket = new PDMS_ICTicket() { ServiceType = new PDMS_ServiceType() { ServiceType = Convert.ToString(dr["ServiceType"]) } };

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

        public List<PDMS_WarrantyInvoiceHeader> GetClaimConsolidationAnnexure1(string DealerCode, int Year, int Month, int MonthRange)
        {
            List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
            PDMS_WarrantyInvoiceHeader W = null;

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter YearP = provider.CreateParameter("Year", Year, DbType.Int16);
            DbParameter MonthP = provider.CreateParameter("Month", Month, DbType.Int16);
            DbParameter MonthRangeP = provider.CreateParameter("MonthRange", MonthRange, DbType.Int16);
            long i = 0;
            DbParameter[] Params = new DbParameter[4] { DealerCodeP, YearP, MonthP, MonthRangeP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetClaimConsolidationAnnexure1", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {

                            W = new PDMS_WarrantyInvoiceHeader();
                            Ws.Add(W);
                            i = i + 1;
                            W.InvoiceNumber = i.ToString();
                            W.ICTicketID = Convert.ToString(dr["ICTicketID"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                            W.CustomerName = Convert.ToString(dr["CustomerName"]);
                            W.DealerCode = Convert.ToString(dr["DealerCode"]);
                            W.DealerName = Convert.ToString(dr["DealerName"]);
                            W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                            W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                            W.Model = Convert.ToString(dr["Model"]);
                            W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                            W.InvoiceItem = new PDMS_WarrantyInvoiceItem();

                            W.InvoiceItem.Material = Convert.ToString(dr["Material"]);
                            W.InvoiceItem.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);

                            W.InvoiceItem.HSNCode = Convert.ToString(dr["HSNCode"]);
                            W.InvoiceItem.Amount = DBNull.Value == dr["Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Amount"]);
                            W.InvoiceItem.Approved1Amount = Convert.ToDecimal(dr["ApprovedAmount"]);
                            W.InvoiceItem.Category = Convert.ToString(dr["Category"]);
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
        public List<PDMS_WarrantyInvoiceHeader> GetClaimConsolidationAnnexureManuallyInSAP1(string DealerCode, int Year, int Month, int MonthRange)
        {
            List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
            PDMS_WarrantyInvoiceHeader W = null;

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter YearP = provider.CreateParameter("Year", Year, DbType.Int16);
            DbParameter MonthP = provider.CreateParameter("Month", Month, DbType.Int16);
            DbParameter MonthRangeP = provider.CreateParameter("MonthRange", MonthRange, DbType.Int16);
            long i = 0;
            DbParameter[] Params = new DbParameter[4] { DealerCodeP, YearP, MonthP, MonthRangeP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetClaimConsolidationAnnexureManuallyInSAP1", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {

                            W = new PDMS_WarrantyInvoiceHeader();
                            Ws.Add(W);
                            i = i + 1;
                            W.InvoiceNumber = i.ToString();
                            W.ICTicketID = Convert.ToString(dr["ICTicketID"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                            W.CustomerName = Convert.ToString(dr["CustomerName"]);
                            W.DealerCode = Convert.ToString(dr["DealerCode"]);
                            W.DealerName = Convert.ToString(dr["DealerName"]);
                            W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                            W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                            W.Model = Convert.ToString(dr["Model"]);
                            W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                            W.InvoiceItem = new PDMS_WarrantyInvoiceItem();

                            W.InvoiceItem.Material = Convert.ToString(dr["Material"]);
                            W.InvoiceItem.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);

                            W.InvoiceItem.HSNCode = Convert.ToString(dr["HSNCode"]);
                            W.InvoiceItem.Amount = DBNull.Value == dr["Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Amount"]);
                            W.InvoiceItem.Approved1Amount = Convert.ToDecimal(dr["ApprovedAmount"]);
                            W.InvoiceItem.Category = Convert.ToString(dr["Category"]);
                            W.InvoiceItem.SAPDoc = Convert.ToString(dr["SAPDoc"]);
                            W.InvoiceItem.SAPPostingDate = DBNull.Value == dr["SAPPostingDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SAPPostingDate"]);

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

        public List<PDMS_WarrantyInvoiceHeader> GetClaimConsolidationAnnexureAll1(string DealerCode, int Year, int? Month, int? MonthRange)
        {
            List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
            PDMS_WarrantyInvoiceHeader W = null;

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter YearP = provider.CreateParameter("Year", Year, DbType.Int16);
            DbParameter MonthP = provider.CreateParameter("Month", Month, DbType.Int16);
            DbParameter MonthRangeP = provider.CreateParameter("MonthRange", MonthRange, DbType.Int16);
            long i = 0;
            DbParameter[] Params = new DbParameter[4] { DealerCodeP, YearP, MonthP, MonthRangeP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetClaimConsolidationAnnexureAll1", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {

                            W = new PDMS_WarrantyInvoiceHeader();
                            Ws.Add(W);
                            i = i + 1;
                            W.InvoiceNumber = i.ToString();
                            W.ICTicketID = Convert.ToString(dr["ICTicketID"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                            W.CustomerName = Convert.ToString(dr["CustomerName"]);
                            W.DealerCode = Convert.ToString(dr["DealerCode"]);
                            W.DealerName = Convert.ToString(dr["DealerName"]);
                            W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                            W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                            W.Model = Convert.ToString(dr["Model"]);
                            W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                            W.InvoiceItem = new PDMS_WarrantyInvoiceItem();

                            W.InvoiceItem.Material = Convert.ToString(dr["Material"]);
                            W.InvoiceItem.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);

                            W.InvoiceItem.HSNCode = Convert.ToString(dr["HSNCode"]);
                            W.InvoiceItem.Amount = DBNull.Value == dr["Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Amount"]);
                            W.InvoiceItem.Approved1Amount = Convert.ToDecimal(dr["ApprovedAmount"]);
                            W.InvoiceItem.Category = Convert.ToString(dr["Category"]);
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
        public List<PDMS_WarrantyInvoiceHeader> GetClaimConsolidationAnnexureAllManuallyInSAP1(string DealerCode, int Year, int? Month, int? MonthRange)
        {
            List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
            PDMS_WarrantyInvoiceHeader W = null;

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter YearP = provider.CreateParameter("Year", Year, DbType.Int16);
            DbParameter MonthP = provider.CreateParameter("Month", Month, DbType.Int16);
            DbParameter MonthRangeP = provider.CreateParameter("MonthRange", MonthRange, DbType.Int16);

            DbParameter[] Params = new DbParameter[4] { DealerCodeP, YearP, MonthP, MonthRangeP };
            long i = 0;
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetClaimConsolidationAnnexureAllManuallyInSAP1", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {

                            W = new PDMS_WarrantyInvoiceHeader();
                            Ws.Add(W);
                            i = i + 1;
                            W.InvoiceNumber = i.ToString();
                            W.ICTicketID = Convert.ToString(dr["ICTicketID"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                            W.CustomerName = Convert.ToString(dr["CustomerName"]);
                            W.DealerCode = Convert.ToString(dr["DealerCode"]);
                            W.DealerName = Convert.ToString(dr["DealerName"]);
                            W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                            W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                            W.Model = Convert.ToString(dr["Model"]);
                            W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                            W.InvoiceItem = new PDMS_WarrantyInvoiceItem();

                            W.InvoiceItem.Material = Convert.ToString(dr["Material"]);
                            W.InvoiceItem.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);

                            W.InvoiceItem.HSNCode = Convert.ToString(dr["HSNCode"]);
                            W.InvoiceItem.Amount = DBNull.Value == dr["Amount"] ? (decimal?)null : Convert.ToDecimal(dr["Amount"]);
                            W.InvoiceItem.Approved1Amount = Convert.ToDecimal(dr["ApprovedAmount"]);
                            W.InvoiceItem.Category = Convert.ToString(dr["Category"]);
                            W.InvoiceItem.SAPDoc = Convert.ToString(dr["SAPDoc"]);
                            W.InvoiceItem.SAPPostingDate = DBNull.Value == dr["SAPPostingDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SAPPostingDate"]);

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


        public List<PDMS_WarrantyInvoiceHeader> GetHMR()
        {

            TraceLogger.Log(DateTime.Now);
            List<PDMS_WarrantyInvoiceHeader> SOIs = new List<PDMS_WarrantyInvoiceHeader>();
            try
            {
                //   string query = "SELECT  * from pr_get_hmrs()";
                string query = "select f_counter_ref_id  as PscID , Max(r_counter_end) as HMR   from dsprr_psc_counter	 where s_tenant_id <> 20 group by f_counter_ref_id";
                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_WarrantyInvoiceHeader SOI = new PDMS_WarrantyInvoiceHeader();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_WarrantyInvoiceHeader();

                    SOI.PscID = Convert.ToString(dr["PscID"]);
                    SOI.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_MTTR", "GetMttr", ex);
                throw ex;
            }
            return SOIs;

        }
        public List<PDMS_WarrantyInvoiceHeader> GetModel_WarrantyEndDate()
        {

            TraceLogger.Log(DateTime.Now);
            List<PDMS_WarrantyInvoiceHeader> SOIs = new List<PDMS_WarrantyInvoiceHeader>();
            try
            {
                string query = "select  psc.p_sc_id as PscID  ,ceq.r_description as Model ,eq.r_end_date as Warranty_End_Date , ceq.r_installation_date, t.problem_reported "
                                + " FROM  dsinr_inv_hdr inv        inner JOIN dsprr_psc_hdr psc	ON ( psc.p_sc_id = inv.r_ext_id AND psc.s_tenant_id = inv.s_tenant_id )  "
                                + " left JOIN dohr_cust_equip_detail ceq ON( ceq.k_equipment_id = psc.f_equipment_id  AND ceq.s_tenant_id = psc.s_tenant_id )   "

                                + " left join dohr_cust_equip_warranty eq on eq.K_equipment_id = psc.f_equipment_id  and eq.s_tenant_id = psc.s_tenant_id  "
                                + " left join af_ic_tickets t on t.f_ic_ticket_id = psc.f_ic_ticket_id "
                                + " where 	 inv.s_status <> 'CANCELLED' and 	d_inv_type_desc = 'Warranty Invoice' ";
                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_WarrantyInvoiceHeader SOI = new PDMS_WarrantyInvoiceHeader();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_WarrantyInvoiceHeader();

                    SOI.PscID = Convert.ToString(dr["PscID"]);
                    SOI.Model = Convert.ToString(dr["Model"]).Replace("(MTS Production)", "");
                    SOI.WarrantyEndDate = DBNull.Value == dr["warranty_end_date"] ? (DateTime?)null : Convert.ToDateTime(dr["warranty_end_date"]);
                    SOI.DateOfCommissioning = DBNull.Value == dr["r_installation_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_installation_date"]);
                    SOI.ReasonForFailure = Convert.ToString(dr["problem_reported"]);
                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_MTTR", "GetModel_WarrantyEndDate", ex);
                throw ex;
            }
            return SOIs;

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
        
        public List<PDMS_WarrantyAttachment> GetAttachment(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_WarrantyAttachment> Attachment = new List<PDMS_WarrantyAttachment>();
            try
            {
                //string query = "SELECT  * from pr_get_attachment(" + filter + ")";
                string query = "select  r_description,r_url,p_object_key,p_filename FROM  dfatr_attachment WHERE p_object_key like concat(" + filter + ",'%')   and s_tenant_id <> 20 group by r_description,r_url,p_object_key,p_filename";
                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                foreach (DataRow dr in dt.Rows)
                {
                    Attachment.Add(new PDMS_WarrantyAttachment()
                    {
                        AttachmentID = 1,
                        fileName = Convert.ToString(dr["r_description"]),
                        PscID = Convert.ToString(dr["p_object_key"]),
                        Url = "OpenInNewTab('" + Convert.ToString(dr["r_url"]) + "'); return false;"
                    });
                }
                return Attachment;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_MTTR", "GetMttr", ex);
                throw ex;
            }
            return Attachment;
        }

        public void insertWarrantyInvoice5K(Boolean live)
        {
            try
            {
                DateTime dtF = DateTime.Now.AddMonths(-2);
                DateTime dtT = DateTime.Now;
                List<PDMS_WarrantyInvoiceHeader> WisDms = new List<PDMS_WarrantyInvoiceHeader>();
                string query = PQuery.GetWarrantyInvoiceIntegration;
                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_WarrantyInvoiceHeader SOI = new PDMS_WarrantyInvoiceHeader();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_WarrantyInvoiceHeader();

                    SOI.InvoiceNumber = Convert.ToString(dr["Invoice_Number"]);
                    SOI.InvoiceDate = DBNull.Value == dr["Invoice_date"] ? (DateTime?)null : Convert.ToDateTime(dr["Invoice_date"]);
                    SOI.ICTicketID = Convert.ToString(dr["IC_Ticket"]);
                    SOI.ICTicketDate = DBNull.Value == dr["f_ic_ticket_date"] ? (DateTime?)null : Convert.ToDateTime(dr["f_ic_ticket_date"]);

                    SOI.DealerCode = Convert.ToString(dr["Dealer_code"]);
                    SOI.DealerName = Convert.ToString(dr["Dealer_Name"]);
                    SOI.CustomerCode = Convert.ToString(dr["BP_Code"]);
                    SOI.MachineSerialNumber = Convert.ToString(dr["Machine_Serial_No"]);
                    SOI.MarginWarranty = DBNull.Value == dr["r_goodwill_warranty"] ? (Boolean?)null : Convert.ToBoolean(dr["r_goodwill_warranty"]);
                    SOI.InvoiceStatus = Convert.ToString(dr["Invoice_Status"]);
                    SOI.PscID = Convert.ToString(dr["p_sc_id"]);
                    SOI.FSRNumber = Convert.ToString(dr["r_fsr_no_date"]);
                    SOI.TSIRNumber = Convert.ToString(dr["r_tsir_no"]);
                    SOI.RestoreDate = DBNull.Value == dr["Restore_Date"] ? (DateTime?)null : Convert.ToDateTime(dr["Restore_Date"]);
                    SOI.Location = Convert.ToString(dr["location"]);
                    SOI.Application = Convert.ToString(dr["application"]);
                    SOI.InvoiceItem = new PDMS_WarrantyInvoiceItem();
                    SOI.InvoiceItem.DeliveryNumber = Convert.ToString(dr["Delivery_ID"]);
                    SOI.InvoiceItem.Material = Convert.ToString(dr["material"]);
                    SOI.InvoiceItem.MaterialDesc = Convert.ToString(dr["material_desc"]);
                    SOI.InvoiceItem.UnitOM = Convert.ToString(dr["uom"]);
                    SOI.InvoiceItem.Qty = Convert.ToDecimal(dr["qty"]);
                    SOI.InvoiceItem.Amount = DBNull.Value == dr["Base_Value"] ? (decimal?)null : Convert.ToDecimal(dr["Base_Value"]);
                    SOI.InvoiceItem.BaseTax = DBNull.Value == dr["Total"] ? (decimal?)null : Convert.ToDecimal(dr["Total"]);
                    SOI.InvoiceItem.HSNCode = Convert.ToString(dr["HSN_CODE"]);
                    SOI.InvoiceItem.Item = Convert.ToString(dr["p_inv_item"]);
                    WisDms.Add(SOI);
                }
                //-------------------------------------------------------
                //   dd.RemoveAll(m => (DateTime)m.ICLoginDate < dtF);
                List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
                PDMS_WarrantyInvoiceHeader W = null;
                PDMS_WarrantyInvoiceItem item = null;
                string InvoiceNumber = "";
                string ICTicketID = "";
                List<PDMS_WarrantyInvoiceHeader> HMRs = GetHMR();
                List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerName();
                List<PDMS_WarrantyInvoiceHeader> Model_WarrantyEndDate = GetModel_WarrantyEndDate();
                foreach (PDMS_WarrantyInvoiceHeader WiDms in WisDms)
                {
                    if ((InvoiceNumber != WiDms.InvoiceNumber) || (ICTicketID != WiDms.ICTicketID))
                    {
                        W = new PDMS_WarrantyInvoiceHeader();
                        Ws.Add(W);
                        InvoiceNumber = WiDms.InvoiceNumber;
                        ICTicketID = WiDms.ICTicketID;
                        W.InvoiceNumber = WiDms.InvoiceNumber;
                        W.InvoiceDate = WiDms.InvoiceDate;
                        W.ICTicketID = WiDms.ICTicketID;
                        W.ICTicketDate = WiDms.ICTicketDate;

                        W.CustomerCode = WiDms.CustomerCode;

                        W.CustomerName = (Customer.Where(m => m.CustomerCode == WiDms.CustomerCode)).ToList()[0].CustomerName;
                        W.DealerCode = WiDms.DealerCode;
                        W.DealerName = WiDms.DealerName;
                        W.InvoiceStatus = WiDms.InvoiceStatus;

                        W.PscID = WiDms.PscID;

                        W.HMR = 0;
                        if ((HMRs.Where(m => m.PscID == W.PscID).Count() != 0))
                        {
                            W.HMR = (HMRs.Where(m => m.PscID == W.PscID)).ToList()[0].HMR;
                        }
                        W.MarginWarranty = WiDms.MarginWarranty;
                        W.MachineSerialNumber = WiDms.MachineSerialNumber;

                        W.FSRNumber = WiDms.FSRNumber;
                        W.TSIRNumber = WiDms.TSIRNumber;
                        W.RestoreDate = WiDms.RestoreDate;
                        W.Location = WiDms.Location;
                        W.Application = WiDms.Application;
                        if ((Model_WarrantyEndDate.Where(m => m.PscID == W.PscID).Count() != 0))
                        {
                            var psc = (Model_WarrantyEndDate.Where(m => m.PscID == W.PscID)).ToList()[0];
                            W.WarrantyEndDate = psc.WarrantyEndDate;
                            W.Model = psc.Model;
                            W.DateOfCommissioning = psc.DateOfCommissioning;
                            W.ReasonForFailure = psc.ReasonForFailure;
                        }
                        W.InvoiceItems = new List<PDMS_WarrantyInvoiceItem>();
                    }
                    item = new PDMS_WarrantyInvoiceItem();
                    item.DeliveryNumber = item.DeliveryNumber;
                    item.Item = WiDms.InvoiceItem.Item;
                    item.RefDocID = WiDms.InvoiceItem.RefDocID;
                    item.Material = WiDms.InvoiceItem.Material;
                    if (!string.IsNullOrEmpty(WiDms.InvoiceItem.MaterialDesc))
                    {
                        item.MaterialDesc = WiDms.InvoiceItem.MaterialDesc;
                    }
                    else
                    {
                        var M = (from m in new SMaterial().getMaterial(WiDms.InvoiceItem.Material) where m.MaterialCode == WiDms.InvoiceItem.Material select m);
                        if (M.Count() == 1)
                        {
                            item.MaterialDesc = M.ToList()[0].MaterialDescription;
                        }
                    }
                    item.Qty = WiDms.InvoiceItem.Qty;
                    item.UnitOM = WiDms.InvoiceItem.UnitOM;
                    item.Amount = WiDms.InvoiceItem.Amount;
                    item.BaseTax = WiDms.InvoiceItem.BaseTax;
                    item.HSNCode = WiDms.InvoiceItem.HSNCode;


                    item.BriefDescriptionOfJob = WiDms.InvoiceItem.BriefDescriptionOfJob;
                    item.Item = WiDms.InvoiceItem.Item;
                    W.InvoiceItems.Add(item);
                }

                foreach (PDMS_WarrantyInvoiceHeader ch in Ws)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {

                            long WarrantyClaimHeaderID = insertWarrantyInvoiceHeader(ch);
                            if (WarrantyClaimHeaderID != 0)
                            {
                                foreach (PDMS_WarrantyInvoiceItem ci in ch.InvoiceItems)
                                {
                                    ci.WarrantyInvoiceHeaderID = WarrantyClaimHeaderID;
                                    insertWarrantyInvoiceItem(ci);

                                }
                            }
                            scope.Complete();
                        }
                    }
                    catch (Exception e2)
                    {
                        new FileLogger().LogMessage("BDMS_MTTR", "insertWarrantyInvoice : " + ch.InvoiceNumber, e2);
                    }
                }

            }
            catch (Exception e1)
            {

            }
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

        public List<PDMS_WarrantyInvoiceHeader> GetWarrantyInvoiceFromPostGres(string Fillter)
        {
            List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();

            try
            {
                DateTime dtF = DateTime.Now.AddMonths(-2);
                DateTime dtT = DateTime.Now;

                List<PDMS_WarrantyInvoiceHeader> WisDms = GetWarrantyInvoiceIntegration(Fillter);

                //   dd.RemoveAll(m => (DateTime)m.ICLoginDate < dtF);
                PDMS_WarrantyInvoiceHeader W = null;
                PDMS_WarrantyInvoiceItem item = null;
                string InvoiceNumber = "";
                string ICTicketID = "";

                List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerName();
                foreach (PDMS_WarrantyInvoiceHeader WiDms in WisDms)
                {
                    if ((InvoiceNumber != WiDms.InvoiceNumber) || (ICTicketID != WiDms.ICTicketID))
                    {
                        W = new PDMS_WarrantyInvoiceHeader();
                        Ws.Add(W);
                        InvoiceNumber = WiDms.InvoiceNumber;
                        ICTicketID = WiDms.ICTicketID;
                        W.InvoiceNumber = WiDms.InvoiceNumber;
                        W.InvoiceDate = WiDms.InvoiceDate;
                        W.ICTicketID = WiDms.ICTicketID;
                        W.ICTicketDate = WiDms.ICTicketDate;

                        W.CustomerCode = WiDms.CustomerCode;

                        W.CustomerName = (Customer.Where(m => m.CustomerCode == WiDms.CustomerCode)).ToList()[0].CustomerName;
                        W.DealerCode = WiDms.DealerCode;
                        W.DealerName = WiDms.DealerName;
                        W.InvoiceStatus = WiDms.InvoiceStatus;

                        W.PscID = WiDms.PscID;

                        W.HMR = 0;

                        string queryHMR = "select f_counter_ref_id  as PscID , Max(r_counter_end) as HMR   from dsprr_psc_counter	 where s_tenant_id <> 20  and f_counter_ref_id = '" + WiDms.PscID + "' group by f_counter_ref_id";
                        DataTable dtHMR = new NpgsqlServer().ExecuteReader(queryHMR);
                        foreach (DataRow dr in dtHMR.Rows)
                        {
                            W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                        }
                        W.MarginWarranty = WiDms.MarginWarranty;
                        W.MachineSerialNumber = WiDms.MachineSerialNumber;

                        W.FSRNumber = WiDms.FSRNumber;
                        W.TSIRNumber = WiDms.TSIRNumber;
                        W.RestoreDate = WiDms.RestoreDate;
                        W.Location = WiDms.Location;
                        W.Application = WiDms.Application;

                        string queryModel = "select  psc.p_sc_id as PscID  ,ceq.r_description as Model ,eq.r_end_date as Warranty_End_Date , ceq.r_installation_date, t.problem_reported "
                              + " FROM  dsinr_inv_hdr inv        inner JOIN dsprr_psc_hdr psc	ON ( psc.p_sc_id = inv.r_ext_id AND psc.s_tenant_id = inv.s_tenant_id )  "
                              + " left JOIN dohr_cust_equip_detail ceq ON( ceq.k_equipment_id = psc.f_equipment_id  AND ceq.s_tenant_id = psc.s_tenant_id )   "

                              + " left join dohr_cust_equip_warranty eq on eq.K_equipment_id = psc.f_equipment_id  and eq.s_tenant_id = psc.s_tenant_id  "
                              + " left join af_ic_tickets t on t.f_ic_ticket_id = psc.f_ic_ticket_id "
                              + " where 	 inv.s_status <> 'CANCELLED' and 	d_inv_type_desc = 'Warranty Invoice' and psc.p_sc_id  = '" + W.PscID + "' ";
                        DataTable dtModel = new NpgsqlServer().ExecuteReader(queryModel);

                        foreach (DataRow dr in dtModel.Rows)
                        {
                            W.Model = Convert.ToString(dr["Model"]).Replace("(MTS Production)", "");
                            W.WarrantyEndDate = DBNull.Value == dr["warranty_end_date"] ? (DateTime?)null : Convert.ToDateTime(dr["warranty_end_date"]);
                            W.DateOfCommissioning = DBNull.Value == dr["r_installation_date"] ? (DateTime?)null : Convert.ToDateTime(dr["r_installation_date"]);
                            W.ReasonForFailure = Convert.ToString(dr["problem_reported"]);
                        }

                        W.InvoiceItems = new List<PDMS_WarrantyInvoiceItem>();
                    }
                    item = new PDMS_WarrantyInvoiceItem();
                    item.DeliveryNumber = WiDms.InvoiceItem.DeliveryNumber;
                    item.Item = WiDms.InvoiceItem.Item;
                    item.RefDocID = WiDms.InvoiceItem.RefDocID;
                    item.Material = WiDms.InvoiceItem.Material;
                    if (!string.IsNullOrEmpty(WiDms.InvoiceItem.MaterialDesc))
                    {
                        item.MaterialDesc = WiDms.InvoiceItem.MaterialDesc;
                    }
                    else
                    {
                        var M = (from m in new SMaterial().getMaterial(WiDms.InvoiceItem.Material) where m.MaterialCode == WiDms.InvoiceItem.Material select m);
                        if (M.Count() == 1)
                        {
                            item.MaterialDesc = M.ToList()[0].MaterialDescription;
                        }
                    }
                    item.Qty = WiDms.InvoiceItem.Qty;
                    item.UnitOM = WiDms.InvoiceItem.UnitOM;
                    item.Amount = WiDms.InvoiceItem.Amount;
                    item.BaseTax = WiDms.InvoiceItem.BaseTax;
                    item.HSNCode = WiDms.InvoiceItem.HSNCode;
                    item.TaxPercentage = WiDms.InvoiceItem.TaxPercentage;

                    item.BriefDescriptionOfJob = WiDms.InvoiceItem.BriefDescriptionOfJob;
                    item.Item = WiDms.InvoiceItem.Item;
                    W.InvoiceItems.Add(item);
                }
                foreach (PDMS_WarrantyInvoiceHeader ch in Ws)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {

                            long WarrantyClaimHeaderID = insertWarrantyInvoiceHeader(ch);
                            if (WarrantyClaimHeaderID != 0)
                            {
                                foreach (PDMS_WarrantyInvoiceItem ci in ch.InvoiceItems)
                                {
                                    ci.WarrantyInvoiceHeaderID = WarrantyClaimHeaderID;
                                    insertWarrantyInvoiceItem(ci);
                                }
                            }
                            scope.Complete();
                        }
                    }
                    catch (Exception e2)
                    {
                        new FileLogger().LogMessage("BDMS_WarrantyClaim", "InsertWarrantyInvoice : " + ch.InvoiceNumber, e2);
                    }
                }

            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_WarrantyClaim", "InsertWarrantyInvoice : ", e1);
            }
            return Ws;
        }
 
        public Boolean CancelWarrantyClaims(string InvoiceNumber, int CanceledBy)
        {
            try
            {
                PDMS_WarrantyInvoiceHeader SOIs = new BDMS_WarrantyClaim().GetWarrantyClaimReport("", null, null, InvoiceNumber, null, null, "", null, null, null, "", "", "", false, PSession.User.UserID)[0];

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", InvoiceNumber, DbType.String);
                    DbParameter CanceledByP = provider.CreateParameter("CanceledBy", CanceledBy, DbType.Int32);
                    DbParameter CancelT = provider.CreateParameter("CancelT", true, DbType.Boolean);
                    DbParameter[] Params = new DbParameter[3] { InvoiceNumberP, CanceledByP, CancelT };
                    provider.Insert("ZDMS_CancelWarrantyClaim", Params);
                    scope.Complete();
                }
                List<string> querys = new List<string>();
                //querys.Add("update dsinr_inv_hdr set s_status = 'CANCELLED'  where p_inv_id = '" + InvoiceNumber.Trim() + "'");
                //foreach (PDMS_WarrantyInvoiceItem Item in SOIs.InvoiceItems)
                //{
                //    if (!string.IsNullOrEmpty(Item.DeliveryNumber))
                //    {
                //        string f_office = new NpgsqlServer(true).ExecuteScalar("select  f_office from dsder_delv_item  where p_del_id ='" + Item.DeliveryNumber.Trim() + "' and f_material_id='" + Item.Material + "' limit 1");
                //        querys.Add("update dmstr_stock set r_total_qty=r_total_qty+" + Item.Qty + ",r_available_qty=r_available_qty+" + Item.Qty + "  where s_tenant_id = " + SOIs.DealerCode + " and p_material = '" + Item.Material + "' and p_office='" + f_office + "' and p_stock_type='SALE'");
                //        querys.Add("update dmstr_stock_batch set r_total_qty=r_total_qty+" + Item.Qty + ",r_available_qty=r_available_qty+" + Item.Qty + "  where s_tenant_id = " + SOIs.DealerCode + " and p_material = '" + Item.Material + "' and p_office='" + f_office + "' and p_stock_type='SALE'");
                //        querys.Add("update dmmr_stock_summary set r_total_qty=r_total_qty+" + Item.Qty + "  where s_tenant_id = " + SOIs.DealerCode + " and p_material_id = '" + Item.Material + "' and p_office_id='" + f_office + "' and p_stock_type='SALE' and p_month = '" + DateTime.Now.Month + "' and p_year = '" + DateTime.Now.Year + "'");
                //    }                    
                //}
                foreach (PDMS_WarrantyInvoiceItem Item in SOIs.InvoiceItems)
                {
                    if (!string.IsNullOrEmpty(Item.DeliveryNumber))
                    {
                        string f_office = new NpgsqlServer().ExecuteScalar("select  f_office from dsder_delv_item  where p_del_id ='" + Item.DeliveryNumber.Trim() + "' and f_material_id='" + Item.Material + "' limit 1");
                        string p_location = new NpgsqlServer().ExecuteScalar("select  f_location from dsder_delv_item  where p_del_id ='" + Item.DeliveryNumber.Trim() + "' and f_material_id='" + Item.Material + "' limit 1");

                        querys.Add("INSERT INTO public.af_stock_ledger_icticket(" +
                          "s_establishment, s_tenant_id, p_location, p_office, p_material, p_stock_type,  p_batch, r_document_type, r_document_id, r_posting_date, f_ref_id1, r_opening_qty, r_inward_qty, r_outward_qty, r_closing_qty, r_current_stock, nes_flag, s_status, created_by, created_on)"
            + "VALUES (1000, " + SOIs.DealerCode + ", '" + p_location + "','" + f_office + "', '" + Item.Material + "', 'SALE', 'B1', 'DLV','" + Item.DeliveryNumber.Trim() + "', now(),'" + SOIs.ICTicketID + "', 0,+" + Item.Qty + ", 0, 0, 0, 'N','Created','sa',now() )");
                    }
                }

                if (!new NpgsqlServer().UpdateTransactions(querys))
                {
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", InvoiceNumber, DbType.String);
                        DbParameter CanceledByP = provider.CreateParameter("CanceledBy", CanceledBy, DbType.Int32);
                        DbParameter CancelT = provider.CreateParameter("CancelT", false, DbType.Boolean);
                        DbParameter[] Params = new DbParameter[3] { InvoiceNumberP, CanceledByP, CancelT };
                        provider.Insert("ZDMS_CancelWarrantyClaim", Params);
                        scope.Complete();
                    }
                }
                return true;
            }
            catch (Exception d)
            {
            }
            return false;
        }
        public void UpdateWarrantyClaimMachineSerialNumberForModel()
        {
            List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
            PDMS_WarrantyInvoiceHeader W = null;
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimMachineSerialNumberForModel"))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            W = new PDMS_WarrantyInvoiceHeader();
                            Ws.Add(W);
                            W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                        }
                    }
                }
                foreach (PDMS_WarrantyInvoiceHeader item in Ws)
                {

                    List<string> Model = new SDMS_ICTicket().getModelByProductID(item.MachineSerialNumber);
                    if (!string.IsNullOrEmpty(Model[0]))
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", item.MachineSerialNumber, DbType.String);
                            DbParameter ModelP = provider.CreateParameter("Model", Model[0], DbType.String);
                            DbParameter DivisionP = provider.CreateParameter("Division", Model[1], DbType.String);
                            DbParameter[] Params = new DbParameter[3] { MachineSerialNumberP, ModelP, DivisionP };
                            provider.Insert("ZDMS_UpdateWarrantyClaimMachineSerialNumberForModel", Params);
                            scope.Complete();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
        }
         
        public Boolean InsertDeviatedClaimRequestForApproval(long WarrantyInvoiceHeaderID, int UserID)
        {

            DbParameter WarrantyInvoiceHeaderIDP = provider.CreateParameter("WarrantyInvoiceHeaderID", WarrantyInvoiceHeaderID, DbType.Int64);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[2] { WarrantyInvoiceHeaderIDP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertDeviatedClaimRequestForApproval", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaim", "InsertDeviatedClaimRequestForApproval", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaim", " InsertDeviatedClaimRequestForApproval", ex);
                return false;
            }
            return true;
        }
        public Boolean ApproveOrRejectDeviatedClaimRequest(long WarrantyInvoiceHeaderID, Boolean? IsApproved, Boolean? IsRejected, int UserID)
        {
            DbParameter WarrantyInvoiceHeaderIDP = provider.CreateParameter("WarrantyInvoiceHeaderID", WarrantyInvoiceHeaderID, DbType.Int64);
            DbParameter IsApprovedP = provider.CreateParameter("IsApproved", IsApproved, DbType.Int32);
            DbParameter IsRejectedP = provider.CreateParameter("IsRejected", IsRejected, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { WarrantyInvoiceHeaderIDP, IsApprovedP, IsRejectedP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_ApproveOrRejectDeviatedClaimRequest", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaim", "ApproveOrRejectDeviatedClaimRequest", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaim", " ApproveOrRejectDeviatedClaimRequest", ex);
                return false;
            }
            return true;
        }
        public List<PDMS_WarrantyInvoiceHeader> GetDeviatedClaimForApproval(int? DealerID, string ClaimNumber, DateTime? RequestedDateF, DateTime? RequestedDateT)
        {
            List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
            PDMS_WarrantyInvoiceHeader W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter ClaimNumberP = provider.CreateParameter("ClaimNumber", string.IsNullOrEmpty(ClaimNumber) ? null : ClaimNumber, DbType.String);
                DbParameter RequestedDateFP = provider.CreateParameter("RequestedDateF", RequestedDateF, DbType.DateTime);
                DbParameter RequestedDateTP = provider.CreateParameter("RequestedDateT", RequestedDateT, DbType.DateTime);

                DbParameter[] Params = new DbParameter[4] { DealerIDP, ClaimNumberP, RequestedDateFP, RequestedDateTP };
                using (DataSet DataSet = provider.Select("ZDMS_GetDeviatedClaimForApproval", Params))
                {

                    if (DataSet != null)
                    {
                        long HeaderID = -1;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
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
                                // W.Approved1By = new PUser() { UserID = DBNull.Value == dr["Approved1By"] ? (int?)null : Convert.ToInt32(dr["Approved1By"]) };
                                W.Approved1On = DBNull.Value == dr["Approved1On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved1On"]);

                                // W.Approved2By = DBNull.Value == dr["Approved2By"] ? (int?)null : Convert.ToInt32(dr["Approved2By"]);

                                W.Approved2By = new PUser();
                                if (dr["Approved2By"] != DBNull.Value)
                                {
                                    W.Approved2By.ContactName = Convert.ToString(dr["Approved2By"]);
                                }

                                W.Approved2On = DBNull.Value == dr["Approved2On"] ? (DateTime?)null : Convert.ToDateTime(dr["Approved2On"]);
                                W.ClaimStatus = Convert.ToString(dr["ClaimStatus"]).Trim();
                                W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                                W.MarginWarranty = DBNull.Value == dr["MarginWarranty"] ? (Boolean?)null : Convert.ToBoolean(dr["MarginWarranty"]);
                                W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
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
                            item.WarrantyMaterialReturnStatus = new PDMS_WarrantyMaterialReturnStatus();
                            if (DBNull.Value != dr["WarrantyMaterialReturnStatusID"])
                            {
                                item.WarrantyMaterialReturnStatus.WarrantyMaterialReturnStatusID = Convert.ToInt32(dr["WarrantyMaterialReturnStatusID"]);
                                item.WarrantyMaterialReturnStatus.WarrantyMaterialReturnStatus = Convert.ToString(dr["WarrantyMaterialReturnStatus"]);
                            }
                            item.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
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
        public List<PDMS_WarrantyInvoiceHeader> GetDeviatedClaimReport(int? DealerID, string ClaimNumber, DateTime? RequestedDateF, DateTime? RequestedDateT, int UserID)
        {
            List<PDMS_WarrantyInvoiceHeader> Ws = new List<PDMS_WarrantyInvoiceHeader>();
            PDMS_WarrantyInvoiceHeader W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter ClaimNumberP = provider.CreateParameter("ClaimNumber", string.IsNullOrEmpty(ClaimNumber) ? null : ClaimNumber, DbType.String);
                DbParameter RequestedDateFP = provider.CreateParameter("RequestedDateF", RequestedDateF, DbType.DateTime);
                DbParameter RequestedDateTP = provider.CreateParameter("RequestedDateT", RequestedDateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[5] { DealerIDP, ClaimNumberP, RequestedDateFP, RequestedDateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetDeviatedClaimReport", Params))
                {
                    if (DataSet != null)
                    {
                        long HeaderID = -1;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
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

                                W.ClaimStatus = Convert.ToString(dr["ClaimStatus"]).Trim();
                                W.HMR = DBNull.Value == dr["HMR"] ? (int?)null : Convert.ToInt32(dr["HMR"]);
                                W.MarginWarranty = DBNull.Value == dr["MarginWarranty"] ? (Boolean?)null : Convert.ToBoolean(dr["MarginWarranty"]);
                                W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                                W.Model = Convert.ToString(dr["Model"]); 

                                W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);

                                W.DeviatedIsApproved = DBNull.Value == dr["IsApproved"] ? (Boolean?)null : Convert.ToBoolean(dr["IsApproved"]);
                                W.DeviatedIsRejected = DBNull.Value == dr["IsRejected"] ? (Boolean?)null : Convert.ToBoolean(dr["IsRejected"]);

                                HeaderID = W.WarrantyInvoiceHeaderID;
                                W.InvoiceItems = new List<PDMS_WarrantyInvoiceItem>();
                            }
                            PDMS_WarrantyInvoiceItem item = new PDMS_WarrantyInvoiceItem();
                            item.WarrantyInvoiceItemID = Convert.ToInt64(dr["WarrantyInvoiceItemID"]);

                            item.Item = Convert.ToString(dr["Item"]);
                            item.RefDocID = Convert.ToString(dr["RefDocID"]);
                            item.Material = Convert.ToString(dr["Material"]);
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
                          
                           
                            item.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
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
    }
}