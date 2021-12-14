using DataAccess;
using Microsoft.Reporting.WebForms;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;

namespace Business
{
    public class BDMS_WarrantyClaimDebitNote
    {
        private IDataAccess provider;
        public BDMS_WarrantyClaimDebitNote()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PDMS_WarrantyClaimInvoice> getWarrantyClaimInvoiceForCreateDebitNote(long? WarrantyClaimInvoiceID, int? DealerID, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, string ClaimNumber, string ICTicketNumber, int? InvoiceTypeID)
        {
            List<PDMS_WarrantyClaimInvoice> Ws = new List<PDMS_WarrantyClaimInvoice>();
            PDMS_WarrantyClaimInvoice W = null;

            DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("WarrantyClaimInvoiceID", WarrantyClaimInvoiceID, DbType.Int64);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
            DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
            DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
            DbParameter ClaimNumberP = provider.CreateParameter("ClaimNumber", string.IsNullOrEmpty(ClaimNumber) ? null : ClaimNumber, DbType.String);
            DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
            DbParameter InvoiceTypeIDP = provider.CreateParameter("InvoiceTypeID", InvoiceTypeID, DbType.Int32);
            DbParameter[] Params = new DbParameter[8] { WarrantyClaimInvoiceIDP, DealerIDP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, ClaimNumberP, ICTicketNumberP, InvoiceTypeIDP };
            try
            {
                long InvoiceID = 0;
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimInvoiceForCreateDebitNote", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["WarrantyClaimInvoiceID"]))
                            {
                                W = new PDMS_WarrantyClaimInvoice();
                                Ws.Add(W);
                                W.WarrantyClaimInvoiceID = Convert.ToInt64(dr["WarrantyClaimInvoiceID"]);
                                W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                W.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                                W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["UserName"]), DealerName = Convert.ToString(dr["ContactName"]) };
                                W.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                W.Year = Convert.ToInt32(dr["Year"]);
                                W.Month = Convert.ToInt32(dr["Month"]);
                                W.MonthRange = Convert.ToInt32(dr["MonthRange"]);
                                W.AnnexureNumber = Convert.ToString(dr["AnnexureNumber"]);
                                W.PeriodFrom = DBNull.Value == dr["PeriodFrom"] ? (DateTime?)null : Convert.ToDateTime(dr["PeriodFrom"]);
                                W.PeriodTo = DBNull.Value == dr["PeriodTo"] ? (DateTime?)null : Convert.ToDateTime(dr["PeriodTo"]);
                                W.Through = Convert.ToString(dr["Through"]);
                                W.LRNumber = Convert.ToString(dr["LRNumber"]);
                                W.InvoiceItems = new List<PDMS_WarrantyClaimInvoiceItem>();
                                InvoiceID = W.WarrantyClaimInvoiceID;
                                W.InvoiceType = new PDMS_WarrantyInvoiceType() { InvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"]), InvoiceType = Convert.ToString(dr["InvoiceType"]) };
                            }
                            W.InvoiceItems.Add(new PDMS_WarrantyClaimInvoiceItem()
                            {
                                WarrantyClaimInvoiceItemID = Convert.ToInt64(dr["WarrantyClaimInvoiceItemID"]),
                                Material = Convert.ToString(dr["Material"]),
                                MaterialDesc = Convert.ToString(dr["MaterialDesc"]),
                                HSNCode = Convert.ToString(dr["HSNCode"]),
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Rate = Convert.ToDecimal(dr["Rate"]),
                                Category = Convert.ToString(dr["Category"]),
                                ApprovedValue = Convert.ToDecimal(dr["ApprovedValue"]),
                                Discount = Convert.ToDecimal(dr["Discount"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                //   DepitValue = Convert.ToDecimal(dr["TaxableValue"]) * Convert.ToDecimal(".06"),
                                DepitValue = 0,
                                CGST = Convert.ToInt32(dr["CGST"]),
                                SGST = Convert.ToInt32(dr["SGST"]),
                                IGST = Convert.ToInt32(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"])

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
        public List<PDMS_WarrantyClaimDebitNote> getWarrantyClaimDebitNoteAcknowledge(long? WarrantyClaimDebitNoteID, int? DealerID, string DebitNoteNumber, DateTime? DebitNoteDateF, DateTime? DebitNoteDateT, string InvoiceNumber, int UserID)
        {
            List<PDMS_WarrantyClaimDebitNote> Ws = new List<PDMS_WarrantyClaimDebitNote>();
            PDMS_WarrantyClaimDebitNote W = null;

            DbParameter WarrantyClaimDebitNoteIDP = provider.CreateParameter("WarrantyClaimDebitNoteID", WarrantyClaimDebitNoteID, DbType.Int64);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter DebitNoteNumberP = provider.CreateParameter("DebitNoteNumber", DebitNoteNumber, DbType.String);
            DbParameter DebitNoteDateFP = provider.CreateParameter("DebitNoteDateF", DebitNoteDateF, DbType.DateTime);
            DbParameter DebitNoteDateTP = provider.CreateParameter("DebitNoteDateT", DebitNoteDateT, DbType.DateTime);
            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", InvoiceNumber, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[7] { WarrantyClaimDebitNoteIDP, DealerIDP, DebitNoteNumberP, DebitNoteDateFP, DebitNoteDateTP, InvoiceNumberP, UserIDP };
            try
            {
                long InvoiceID = 0;
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimDebitNoteForAcknowledge", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["WarrantyClaimDebitNoteID"]))
                            {
                                W = new PDMS_WarrantyClaimDebitNote();
                                Ws.Add(W);
                                W.WarrantyClaimDebitNoteID = Convert.ToInt64(dr["WarrantyClaimDebitNoteID"]);
                                W.DebitNoteNumber = Convert.ToString(dr["DebitNoteNumber"]);
                                W.DebitNoteDate = Convert.ToDateTime(dr["DebitNoteDate"]);
                                W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["ContactName"]) };
                                W.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                //W.Year = Convert.ToInt32(dr["Year"]);
                                //W.Month = Convert.ToInt32(dr["Month"]);
                                //W.MonthRange = Convert.ToInt32(dr["MonthRange"]);
                                W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                //W.PeriodFrom = DBNull.Value == dr["PeriodFrom"] ? (DateTime?)null : Convert.ToDateTime(dr["PeriodFrom"]);
                                //W.PeriodTo = DBNull.Value == dr["PeriodTo"] ? (DateTime?)null : Convert.ToDateTime(dr["PeriodTo"]);
                                //W.Through = Convert.ToString(dr["Through"]);
                                //W.LRNumber = Convert.ToString(dr["LRNumber"]);
                                W.DebitNoteItems = new List<PDMS_WarrantyClaimDebitNoteItem>();
                                InvoiceID = W.WarrantyClaimDebitNoteID;
                            }
                            W.DebitNoteItems.Add(new PDMS_WarrantyClaimDebitNoteItem()
                            {
                                WarrantyClaimDebitNoteItemID = Convert.ToInt64(dr["WarrantyClaimDebitNoteItemID"]),
                                Material = Convert.ToString(dr["Material"]),
                                MaterialDesc = Convert.ToString(dr["MaterialDesc"]),
                                HSNCode = Convert.ToString(dr["HSNCode"]),
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Rate = Convert.ToDecimal(dr["Rate"]),
                                ApprovedValue = Convert.ToDecimal(dr["TaxableValue"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToInt32(dr["CGST"]),
                                SGST = Convert.ToInt32(dr["SGST"]),
                                IGST = Convert.ToInt32(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                                Remark = Convert.ToString(dr["Remark"]),
                                FileName = Convert.ToString(dr["FileName"]),
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
        public Boolean AcknowledgeWarrantyClaimDebitNote(long WarrantyClaimDebitNoteID, int UserID)
        {

            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter WarrantyClaimDebitNoteIDP = provider.CreateParameter("WarrantyClaimDebitNoteID", WarrantyClaimDebitNoteID, DbType.Int64);
            DbParameter[] Params = new DbParameter[2] { WarrantyClaimDebitNoteIDP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_AcknowledgeWarrantyClaimDebitNoter", Params);
                    scope.Complete();
                }
                return true;
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimDebitNote", "AcknowledgeWarrantyClaimDebitNote", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimDebitNote", " AcknowledgeWarrantyClaimDebitNote", ex);
            }
            return false;
        }
        public List<PDMS_WarrantyClaimDebitNote> getWarrantyClaimDebitNoteReport(long? WarrantyClaimDebitNoteID, int? DealerID, string DebitNoteNumber, DateTime? DebitNoteDateF, DateTime? DebitNoteDateT, string InvoiceNumber, int? UserID)
        {
            List<PDMS_WarrantyClaimDebitNote> Ws = new List<PDMS_WarrantyClaimDebitNote>();
            PDMS_WarrantyClaimDebitNote W = null;
            DbParameter WarrantyClaimDebitNoteIDP = provider.CreateParameter("WarrantyClaimDebitNoteID", WarrantyClaimDebitNoteID, DbType.Int64);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter DebitNoteNumberP = provider.CreateParameter("DebitNoteNumber", string.IsNullOrEmpty(DebitNoteNumber) ? null : DebitNoteNumber, DbType.String);
            DbParameter DebitNoteDateFP = provider.CreateParameter("DebitNoteDateF", DebitNoteDateF, DbType.DateTime);
            DbParameter DebitNoteDateTP = provider.CreateParameter("DebitNoteDateT", DebitNoteDateT, DbType.DateTime);
            DbParameter InvoiceNumberP = provider.CreateParameter("InvoiceNumber", string.IsNullOrEmpty(InvoiceNumber) ? null : InvoiceNumber, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[7] { WarrantyClaimDebitNoteIDP, DealerIDP, DebitNoteNumberP, DebitNoteDateFP, DebitNoteDateTP, InvoiceNumberP, UserIDP };
            try
            {
                long InvoiceID = 0;
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_getWarrantyClaimDebitNoteReport", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["WarrantyClaimDebitNoteID"]))
                            {
                                W = new PDMS_WarrantyClaimDebitNote();
                                Ws.Add(W);
                                W.WarrantyClaimDebitNoteID = Convert.ToInt64(dr["WarrantyClaimDebitNoteID"]);
                                W.DebitNoteNumber = Convert.ToString(dr["DebitNoteNumber"]);
                                W.DebitNoteDate = Convert.ToDateTime(dr["DebitNoteDate"]);
                                W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["ContactName"]) };
                                W.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                //W.Year = Convert.ToInt32(dr["Year"]);
                                //W.Month = Convert.ToInt32(dr["Month"]);
                                //W.MonthRange = Convert.ToInt32(dr["MonthRange"]);
                                W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                W.PeriodFrom = DBNull.Value == dr["PeriodFrom"] ? (DateTime?)null : Convert.ToDateTime(dr["PeriodFrom"]);
                                W.PeriodTo = DBNull.Value == dr["PeriodTo"] ? (DateTime?)null : Convert.ToDateTime(dr["PeriodTo"]);
                                //W.Through = Convert.ToString(dr["Through"]);
                                //W.LRNumber = Convert.ToString(dr["LRNumber"]);
                                W.DebitNoteItems = new List<PDMS_WarrantyClaimDebitNoteItem>();
                                InvoiceID = W.WarrantyClaimDebitNoteID;
                                W.IsAcknowledged = DBNull.Value == dr["IsAcknowledged"] ? false : Convert.ToBoolean(dr["IsAcknowledged"]);
                                W.SAPDoc = Convert.ToString(dr["SAPDoc"]);
                                W.SAPPostingDate = DBNull.Value == dr["SAPPostingDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SAPPostingDate"]);
                                W.SAPClearingDocument = Convert.ToString(dr["SAPClearingDocument"]);
                                W.SAPClearingDate = DBNull.Value == dr["SAPClearingDate"] ? (DateTime?)null : Convert.ToDateTime(dr["SAPClearingDate"]);
                                W.SAPInvoiceValue = DBNull.Value == dr["SAPDebitNoteValue"] ? (Decimal?)null : Convert.ToDecimal(dr["SAPDebitNoteValue"]);
                                W.SAPInvoiceTDSValue = DBNull.Value == dr["SAPDebitNoteTDSValue"] ? (Decimal?)null : Convert.ToDecimal(dr["SAPDebitNoteTDSValue"]);

                                W.TCSValue = DBNull.Value == dr["TCSValue"] ? 0 : Convert.ToDecimal(dr["TCSValue"]);
                                W.TCSTax = DBNull.Value == dr["TCSTax"] ? 0 : Convert.ToDecimal(dr["TCSTax"]);
                                W.IRN = Convert.ToString(dr["IRN"]);
                                W.IRNDate = DBNull.Value == dr["IRNDate"] ? (DateTime?)null : Convert.ToDateTime(dr["IRNDate"]);
                            }
                            W.DebitNoteItems.Add(new PDMS_WarrantyClaimDebitNoteItem()
                            {
                                WarrantyClaimDebitNoteItemID = Convert.ToInt64(dr["WarrantyClaimDebitNoteItemID"]),
                                WarrantyClaimAnnexureItemID = Convert.ToInt64(dr["WarrantyClaimAnnexureItemID"]),
                                Material = Convert.ToString(dr["Material"]),
                                MaterialDesc = Convert.ToString(dr["MaterialDesc"]),
                                HSNCode = Convert.ToString(dr["HSNCode"]),
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Rate = Convert.ToDecimal(dr["Rate"]),
                                ApprovedValue = Convert.ToDecimal(dr["TaxableValue"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToInt32(dr["CGST"]),
                                SGST = Convert.ToInt32(dr["SGST"]),
                                IGST = Convert.ToInt32(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                                Remark = Convert.ToString(dr["Remark"]),
                                FileName = Convert.ToString(dr["FileName"]),
                                //  ContentType = Convert.ToString(dr["Material"]),
                                // FileSize = Convert.ToInt32(dr["Material"]),
                                //   AttachedByte = (Byte[])(dr["AttachedByte"]),
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
        public PDMS_WarrantyClaimDebitNoteItem getWarrantyClaimDebitNoteItemAttachment(long WarrantyClaimDebitNoteItemID)
        {
            PDMS_WarrantyClaimDebitNoteItem Ws = new PDMS_WarrantyClaimDebitNoteItem();
            DbParameter WarrantyClaimDebitNoteItemIDP = provider.CreateParameter("WarrantyClaimDebitNoteItemID", WarrantyClaimDebitNoteItemID, DbType.Int64);
            DbParameter[] Params = new DbParameter[1] { WarrantyClaimDebitNoteItemIDP };
            try
            {

                using (DataSet EmployeeDataSet = provider.Select("ZDMS_getWarrantyClaimDebitNoteItemAttachment", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            Ws.FileName = Convert.ToString(dr["FileName"]);
                            Ws.ContentType = Convert.ToString(dr["ContentType"]);
                            Ws.FileSize = Convert.ToInt32(dr["FileSize"]);
                            Ws.AttachedByte = (Byte[])(dr["AttachedByte"]);
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
        public long InsertWarrantyClaimDebitNote(List<PDMS_WarrantyClaimInvoiceItem> invs, int UserID, decimal? GrandTotal, decimal? TCSValue, decimal? TCSTax, PDMS_Customer Dealer, PDMS_Customer Customer)
        {
            int success = 0;
            long ClaimDebitID = 0;

            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("WarrantyClaimInvoiceID", invs[0].WarrantyClaimInvoiceID, DbType.Int64);
            DbParameter GrandTotalP = provider.CreateParameter("GrandTotal", GrandTotal, DbType.Decimal);
            DbParameter TCSValueP = provider.CreateParameter("TCSValue", TCSValue, DbType.Decimal);
            DbParameter TCSTaxP = provider.CreateParameter("TCSTax", TCSTax, DbType.Decimal);
            DbParameter ClaimDebitIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter SGSTIN = provider.CreateParameter("SupplierGSTIN", Dealer.GSTIN, DbType.String);
            DbParameter SAddr1 = provider.CreateParameter("Supplier_addr1", Dealer.Address1, DbType.String);
            DbParameter SAddr2 = provider.CreateParameter("Supplier_addr2", Dealer.Address2, DbType.String);
            DbParameter SLocation = provider.CreateParameter("SupplierLocation", Dealer.City, DbType.String);
            DbParameter SPincode = provider.CreateParameter("SupplierPincode", Dealer.Pincode, DbType.String);
            DbParameter SStateCode = provider.CreateParameter("SupplierStateCode", Dealer.StateCode, DbType.String);

            DbParameter BGSTIN = provider.CreateParameter("BuyerGSTIN", Customer.GSTIN, DbType.String);
            DbParameter BStateCode = provider.CreateParameter("BuyerStateCode", Customer.StateCode, DbType.String);
            DbParameter BAddr1 = provider.CreateParameter("Buyer_addr1", Customer.Address1, DbType.String);
            DbParameter BAddr2 = provider.CreateParameter("Buyer_addr2", Customer.Address2, DbType.String);
            DbParameter BLoc = provider.CreateParameter("Buyer_loc", Customer.City, DbType.String);
            DbParameter BPincode = provider.CreateParameter("BuyerPincode", Customer.Pincode, DbType.String);

            DbParameter[] Params = new DbParameter[18] { WarrantyClaimInvoiceIDP, UserIDP, ClaimDebitIDP, GrandTotalP, TCSValueP, TCSTaxP, SGSTIN, SAddr1, SAddr2, SLocation, SPincode, SStateCode, BGSTIN, BStateCode, BAddr1, BAddr2, BLoc, BPincode };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertWarrantyClaimDebitNoteHeader", Params);
                    ClaimDebitID = Convert.ToInt64(ClaimDebitIDP.Value);
                    foreach (PDMS_WarrantyClaimInvoiceItem inv in invs)
                    {
                        DbParameter WarrantyClaimInvoiceItemIDP = provider.CreateParameter("WarrantyClaimInvoiceItemID", inv.WarrantyClaimInvoiceItemID, DbType.Int64);
                        DbParameter ApprovedValueP = provider.CreateParameter("Value", inv.ApprovedValue, DbType.Decimal);
                        //  DbParameter QtyP = provider.CreateParameter("Qty", inv.Qty, DbType.Int32);
                        DbParameter ClaimDebitIDIDP = provider.CreateParameter("WarrantyClaimInvoiceID", ClaimDebitID, DbType.Int64);

                        DbParameter RemarkP = provider.CreateParameter("Remark", inv.Remark, DbType.String);
                        DbParameter FileNameP = provider.CreateParameter("FileName", inv.FileName, DbType.String);
                        DbParameter ContentTypeP = provider.CreateParameter("ContentType", inv.ContentType, DbType.String);
                        DbParameter FileSizeP = provider.CreateParameter("FileSize", inv.FileSize, DbType.Int32);
                        DbParameter AttachedByteP = provider.CreateParameter("AttachedByte", inv.AttachedByte, DbType.Binary);


                        DbParameter[] Ps = new DbParameter[8] { WarrantyClaimInvoiceItemIDP, ApprovedValueP, ClaimDebitIDIDP, RemarkP, FileNameP, ContentTypeP, FileSizeP, AttachedByteP };
                        provider.Insert("ZDMS_InsertWarrantyClaimDebitNoteItem", Ps);
                    }
                    scope.Complete();
                }
                //List<string> querys = new List<string>();
                //foreach (PDMS_WarrantyClaimInvoiceItem Item in invs)
                //{
                //    if (Item.WarrantyClaimAnnexureItemID != 0)
                //    {
                //        PDMS_WarrantyClaimAnnexureItem AnnexureItem = new PDMS_WarrantyClaimAnnexureItem();
                //        AnnexureItem = new BDMS_WarrantyClaimAnnexure().GetWarrantyClaimAnnexureReport(null, "", null, null, null, null, "", Item.WarrantyClaimAnnexureItemID, true)[0].AnnexureItems[0];
                //        PDMS_WarrantyInvoiceHeader SOIs = new BDMS_WarrantyClaim().GetWarrantyClaimReport("", null, null, AnnexureItem.InvoiceNumber, null, null, "", null, null, null, "", false, PSession.User.UserID)[0];
                //        string DeliveryNumber = SOIs.InvoiceItems.Where(M => M.Material == Item.Material).ToList()[0].DeliveryNumber;

                //        if (!string.IsNullOrEmpty(DeliveryNumber))
                //        {
                //            string f_office = new NpgsqlServer(true).ExecuteScalar("select  f_office from dsder_delv_item  where p_del_id ='" + DeliveryNumber.Trim() + "' and f_material_id='" + Item.Material + "' limit 1");
                //            querys.Add("update dmstr_stock set r_total_qty=r_total_qty+" + Item.Qty + ",r_available_qty=r_available_qty+" + Item.Qty + "  where s_tenant_id = " + SOIs.DealerCode + " and p_material = '" + Item.Material + "' and p_office='" + f_office + "' and p_stock_type='SALE'");
                //            querys.Add("update dmstr_stock_batch set r_total_qty=r_total_qty+" + Item.Qty + ",r_available_qty=r_available_qty+" + Item.Qty + "  where s_tenant_id = " + SOIs.DealerCode + " and p_material = '" + Item.Material + "' and p_office='" + f_office + "' and p_stock_type='SALE'");
                //            querys.Add("update dmmr_stock_summary set r_total_qty=r_total_qty+" + Item.Qty + "  where s_tenant_id = " + SOIs.DealerCode + " and p_material_id = '" + Item.Material + "' and p_office_id='" + f_office + "' and p_stock_type='SALE' and p_month = '" + DateTime.Now.Month + "' and p_year = '" + DateTime.Now.Year + "'");
                //        }
                //        Boolean StockStatus = new NpgsqlServer(true).UpdateTransactions(querys);
                //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                //        {
                //            DbParameter WarrantyClaimInvoiceItemIDP = provider.CreateParameter("WarrantyClaimInvoiceItemID", Item.WarrantyClaimInvoiceItemID, DbType.Int64);
                //            DbParameter StockStatusP = provider.CreateParameter("StockStatus", StockStatus, DbType.Boolean);
                //            DbParameter[] P = new DbParameter[2] { WarrantyClaimInvoiceItemIDP, StockStatusP };
                //            provider.Insert("ZDMS_UpdateWarrantyClaimDebitNoteStockStatus", P);
                //            scope.Complete();
                //        }
                //    }
                //}

                //new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(ClaimDebitID, DebitFile(ClaimDebitID));
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimDebitNote", "InsertWarrantyClaimDebitNote", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimDebitNote", " InsertWarrantyClaimDebitNote", ex);
            }
            return ClaimDebitID;
        }
    }
}
