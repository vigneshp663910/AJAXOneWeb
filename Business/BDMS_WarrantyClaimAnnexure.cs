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
    public class BDMS_WarrantyClaimAnnexure
    {
        private IDataAccess provider;
        public BDMS_WarrantyClaimAnnexure()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public PDMS_WarrantyClaimAnnexureHeader GetWarrantyClaimAnnexureToGenerate(string DealerCode, int Year, int Month, int MonthRange, int InvoiceTypeID, string DeliveryChallan)
        {
            PDMS_WarrantyClaimAnnexureHeader Ws = new PDMS_WarrantyClaimAnnexureHeader();
            PDMS_WarrantyClaimAnnexureItem W = null;

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter YearP = provider.CreateParameter("Year", Year, DbType.Int16);
            DbParameter MonthP = provider.CreateParameter("Month", Month, DbType.Int16);
            DbParameter MonthRangeP = provider.CreateParameter("MonthRange", MonthRange, DbType.Int16);
            DbParameter InvoiceTypeIDP = provider.CreateParameter("InvoiceTypeID", InvoiceTypeID, DbType.Int32);
            DbParameter DeliveryChallanP = provider.CreateParameter("DeliveryChallan", string.IsNullOrEmpty(DeliveryChallan) ? null : DeliveryChallan, DbType.String);


            int RowNo = 0;
            DbParameter[] Params = new DbParameter[6] { DealerCodeP, YearP, MonthP, MonthRangeP, InvoiceTypeIDP, DeliveryChallanP };
            try
            {
                using (DataSet ds = provider.Select("ZDMS_GetWarrantyClaimAnnexureToGenerate", Params))
                {
                    if (ds != null)
                    {
                        // Ws.WarrantyClaimAnnexureHeaderID = Convert.ToInt64(ds.Tables[0].Rows[0]["WarrantyClaimAnnexureHeaderID"]);
                        Ws.Dealer = new PDMS_Dealer();
                        if (ds.Tables[0].Rows.Count != 0)
                        {

                            Ws.Dealer.DealerCode = Convert.ToString(ds.Tables[0].Rows[0]["DealerCode"]);
                            Ws.Dealer.DealerName = Convert.ToString(ds.Tables[0].Rows[0]["DealerName"]);
                        }
                        Ws.AnnexureItems = new List<PDMS_WarrantyClaimAnnexureItem>();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            W = new PDMS_WarrantyClaimAnnexureItem();
                            Ws.AnnexureItems.Add(W);
                            RowNo = RowNo + 1;
                            W.SLID = RowNo;
                            W.ICTicketID = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                            W.CustomerName = Convert.ToString(dr["CustomerName"]);

                            W.HMR = Convert.ToString(dr["HMR"]);
                            W.MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]);
                            W.Model = Convert.ToString(dr["Model"]);
                            W.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                            W.ApprovedDate = Convert.ToDateTime(dr["ApprovedDate"]);
                            W.Material = Convert.ToString(dr["Material"]);
                            W.MaterialDesc = Convert.ToString(dr["MaterialDesc"]);

                            W.HSNCode = Convert.ToString(dr["HSNCode"]);
                            W.ClaimAmount = Convert.ToDecimal(dr["Amount"]);
                            W.ApprovedAmount = Convert.ToDecimal(dr["ApprovedAmount"]);
                            W.Category = Convert.ToString(dr["Category"]);
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
        public Boolean InsertWarrantyClaimAnnexureHeader(PDMS_WarrantyClaimAnnexureHeader Annexure, int InvoiceTypeID, string ClaimNumber, int UserID)
        {

            int success = 0;
            long WarrantyClaimInvoiceID = 0;

            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", Annexure.Dealer.DealerCode, DbType.String);
            DbParameter Address1P = provider.CreateParameter("Address1", Annexure.Address1, DbType.String);
            DbParameter Address2P = provider.CreateParameter("Address2", Annexure.Address2, DbType.String);
            DbParameter ContactP = provider.CreateParameter("Contact", Annexure.Contact, DbType.String);
            DbParameter GSTINP = provider.CreateParameter("GSTIN", Annexure.GSTIN, DbType.String);
            DbParameter Month = provider.CreateParameter("Month", Annexure.Month, DbType.Int32);
            DbParameter Year = provider.CreateParameter("Year", Annexure.Year, DbType.Int32);
            DbParameter MonthRangeP = provider.CreateParameter("MonthRange", Annexure.MonthRange, DbType.Int32);
            DbParameter PeriodFromP = provider.CreateParameter("PeriodFrom", Annexure.PeriodFrom, DbType.DateTime);
            DbParameter PeriodToP = provider.CreateParameter("PeriodTo", Annexure.PeriodTo, DbType.DateTime);
            DbParameter AnnexureNumberP = provider.CreateParameter("AnnexureNumber", Annexure.AnnexureNumber, DbType.String);
            DbParameter InvoiceTypeIDP = provider.CreateParameter("InvoiceTypeID", InvoiceTypeID, DbType.Int32);
            DbParameter ClaimNumberP = provider.CreateParameter("ClaimNumber", string.IsNullOrEmpty(ClaimNumber) ? null : ClaimNumber, DbType.String);

            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            //  DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[14] { DealerCodeP, Address1P, Address2P, ContactP, GSTINP, Month, Year, MonthRangeP, PeriodFromP, PeriodToP, AnnexureNumberP, UserIDP, ClaimNumberP, InvoiceTypeIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertWarrantyClaimAnnexureHeader", Params);

                    if (success != 0)
                    {
                        //   WarrantyClaimInvoiceID = Convert.ToInt64(WarrantyClaimInvoiceIDP.Value);
                    }
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimAnnexure", "InsertWarrantyClaimAnnexureHeader", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimAnnexure", " InsertWarrantyClaimAnnexureHeader", ex);
                return false;
            }
            return true;
        }

        public List<PDMS_WarrantyClaimAnnexureHeader> GetWarrantyClaimAnnexureReport(long? WarrantyClaimAnnexureHeaderID, string DealerCode, int? Year, int? Month, int? MonthRange, int? InvoiceTypeID, string ClaimNumber, long? WarrantyClaimAnnexureItemID, Boolean ALL = false)
        {
            List<PDMS_WarrantyClaimAnnexureHeader> Ws = new List<PDMS_WarrantyClaimAnnexureHeader>();
            PDMS_WarrantyClaimAnnexureHeader W = null;
            DbParameter WarrantyClaimAnnexureHeaderIDP = provider.CreateParameter("WarrantyClaimAnnexureHeaderID", WarrantyClaimAnnexureHeaderID, DbType.Int64);
            DbParameter WarrantyClaimAnnexureItemIDP = provider.CreateParameter("WarrantyClaimAnnexureItemID", WarrantyClaimAnnexureItemID, DbType.Int64);
            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter YearP = provider.CreateParameter("Year", Year, DbType.Int16);
            DbParameter MonthP = provider.CreateParameter("Month", Month, DbType.Int16);
            DbParameter MonthRangeP = provider.CreateParameter("MonthRange", MonthRange, DbType.Int16);
            DbParameter InvoiceTypeIDP = provider.CreateParameter("InvoiceTypeID", InvoiceTypeID, DbType.Int32);
            DbParameter ClaimNumberP = provider.CreateParameter("ClaimNumber", string.IsNullOrEmpty(ClaimNumber) ? null : ClaimNumber, DbType.String);
            DbParameter ALLP = provider.CreateParameter("ALL", ALL, DbType.Boolean);
            long i = 0;
            DbParameter[] Params = new DbParameter[9] { WarrantyClaimAnnexureHeaderIDP, WarrantyClaimAnnexureItemIDP, DealerCodeP, YearP, MonthP, MonthRangeP, InvoiceTypeIDP, ClaimNumberP, ALLP };
            try
            {
                using (DataSet DS = provider.Select("ZDMS_GetWarrantyClaimAnnexureReport", Params))
                {
                    if (DS != null)
                    {
                        long HeaderID = 0;
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            if (HeaderID != Convert.ToInt64(dr["WarrantyClaimAnnexureHeaderID"]))
                            {
                                W = new PDMS_WarrantyClaimAnnexureHeader();
                                Ws.Add(W);
                                i = i + 1;
                                HeaderID = Convert.ToInt64(dr["WarrantyClaimAnnexureHeaderID"]);
                                W.WarrantyClaimAnnexureHeaderID = Convert.ToInt64(dr["WarrantyClaimAnnexureHeaderID"]);
                                W.Dealer = new PDMS_Dealer();
                                W.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                                W.Dealer.DealerName = Convert.ToString(dr["DealerName"]);
                                W.Address1 = Convert.ToString(dr["Address1"]);
                                W.Address2 = Convert.ToString(dr["Address2"]);
                                W.Contact = Convert.ToString(dr["Contact"]);
                                W.GSTIN = Convert.ToString(dr["GSTIN"]);
                                W.Month = Convert.ToInt32(dr["Month"]);
                                W.MonthName = new DateTime(1900, W.Month, 01).ToString("MMM");

                                W.Year = Convert.ToInt32(dr["Year"]);
                                W.InvoiceNumber = Convert.ToString(dr["NEPI_INV"]);
                                W.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                                W.PeriodFrom = Convert.ToDateTime(dr["PeriodFrom"]);
                                W.PeriodTo = Convert.ToDateTime(dr["PeriodTo"]);
                                W.AnnexureNumber = Convert.ToString(dr["AnnexureNumber"]);
                                W.AnnexureItems = new List<PDMS_WarrantyClaimAnnexureItem>();
                            }
                            W.AnnexureItems.Add(new PDMS_WarrantyClaimAnnexureItem()
                            {
                                WarrantyClaimAnnexureItemID = Convert.ToInt64(dr["WarrantyClaimAnnexureItemID"]),
                                SLID = Convert.ToInt32(dr["SL"]),
                                ICTicketID = Convert.ToString(dr["ICTicket"]),
                                ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]),
                                CustomerCode = Convert.ToString(dr["CustomerCode"]),
                                CustomerName = Convert.ToString(dr["CustomerName"]),
                                HMR = Convert.ToString(dr["HMR"]),
                                MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]),
                                Model = Convert.ToString(dr["Model"]),
                                RestoreDate = dr["RestoreDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]),
                                ApprovedDate = dr["ApprovedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ApprovedDate"]),
                                Material = Convert.ToString(dr["Material"]),
                                MaterialDesc = Convert.ToString(dr["MaterialDesc"]),
                                HSNCode = Convert.ToString(dr["HSNCode"]),
                                ClaimAmount = Convert.ToDecimal(dr["Amount"]),
                                ApprovedAmount = Convert.ToDecimal(dr["ApprovedAmount"]),
                                Category = Convert.ToString(dr["Category"]),
                                Qty = Convert.ToInt32(dr["Qty"]),
                                TaxPercentage = dr["TaxPercentage"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["TaxPercentage"])
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
        public List<PDMS_WarrantyClaimAnnexureHeader> GetWarrantyClaimAnnexureByID(long? WarrantyClaimAnnexureHeaderID, long? WarrantyClaimAnnexureItemID, string AnnexureNumber)
        {
            List<PDMS_WarrantyClaimAnnexureHeader> Ws = new List<PDMS_WarrantyClaimAnnexureHeader>();
            PDMS_WarrantyClaimAnnexureHeader W = null;
            DbParameter WarrantyClaimAnnexureHeaderIDP = provider.CreateParameter("WarrantyClaimAnnexureHeaderID", WarrantyClaimAnnexureHeaderID, DbType.Int64);
            DbParameter WarrantyClaimAnnexureItemIDP = provider.CreateParameter("WarrantyClaimAnnexureItemID", WarrantyClaimAnnexureItemID, DbType.Int64);
            DbParameter AnnexureNumberP = provider.CreateParameter("AnnexureNumber", AnnexureNumber, DbType.String);

            long i = 0;
            DbParameter[] Params = new DbParameter[3] { WarrantyClaimAnnexureHeaderIDP, WarrantyClaimAnnexureItemIDP, AnnexureNumberP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetWarrantyClaimAnnexureByID", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        long HeaderID = 0;
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            if (HeaderID != Convert.ToInt64(dr["WarrantyClaimAnnexureHeaderID"]))
                            {
                                W = new PDMS_WarrantyClaimAnnexureHeader();
                                Ws.Add(W);
                                i = i + 1;
                                HeaderID = Convert.ToInt64(dr["WarrantyClaimAnnexureHeaderID"]);
                                W.WarrantyClaimAnnexureHeaderID = Convert.ToInt64(dr["WarrantyClaimAnnexureHeaderID"]);
                                // W.Dealer = new PDMS_Dealer();
                                //W.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]); 
                                //W.Address1 = Convert.ToString(dr["Address1"]);
                                //W.Address2 = Convert.ToString(dr["Address2"]);
                                //W.Contact = Convert.ToString(dr["Contact"]);
                                //W.GSTIN = Convert.ToString(dr["GSTIN"]);
                                //W.Month = Convert.ToInt32(dr["Month"]);
                                //W.MonthName = new DateTime(1900, W.Month, 01).ToString("MMM");

                                //W.Year = Convert.ToInt32(dr["Year"]);
                                //W.InvoiceNumber = Convert.ToString(dr["NEPI_INV"]);
                                //W.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                                //W.PeriodFrom = Convert.ToDateTime(dr["PeriodFrom"]);
                                //W.PeriodTo = Convert.ToDateTime(dr["PeriodTo"]);
                                //W.AnnexureNumber = Convert.ToString(dr["AnnexureNumber"]);
                                W.AnnexureItems = new List<PDMS_WarrantyClaimAnnexureItem>();
                            }
                            W.AnnexureItems.Add(new PDMS_WarrantyClaimAnnexureItem()
                            {
                                WarrantyClaimAnnexureItemID = Convert.ToInt64(dr["WarrantyClaimAnnexureItemID"]),
                                //SLID = Convert.ToInt32(dr["SL"]),
                                ICTicketID = Convert.ToString(dr["ICTicket"]),
                                //ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]),
                                CustomerCode = Convert.ToString(dr["CustomerCode"]),
                                CustomerName = Convert.ToString(dr["CustomerName"]),
                                HMR = Convert.ToString(dr["HMR"]),
                                MachineSerialNumber = Convert.ToString(dr["MachineSerialNumber"]),
                                Model = Convert.ToString(dr["Model"]),
                                ICTicket = new PDMS_ICTicket() { ServiceType = new PDMS_ServiceType() { ServiceType = Convert.ToString(dr["ServiceType"]) } }
                                //  RestoreDate = dr["RestoreDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]),
                                // ApprovedDate = dr["ApprovedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ApprovedDate"]),
                                //  Material = Convert.ToString(dr["Material"]),
                                //  MaterialDesc = Convert.ToString(dr["MaterialDesc"]),
                                //  HSNCode = Convert.ToString(dr["HSNCode"]),
                                //  ClaimAmount = Convert.ToDecimal(dr["Amount"]),
                                //  ApprovedAmount = Convert.ToDecimal(dr["ApprovedAmount"]),
                                //  Category = Convert.ToString(dr["Category"]),
                                // Qty = Convert.ToInt32(dr["Qty"]),
                                //    TaxPercentage = dr["TaxPercentage"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["TaxPercentage"])
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
    }
}
