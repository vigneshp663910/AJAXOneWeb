using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business
{
    public class BSalesIncentive
    {
        private IDataAccess provider;
        public BSalesIncentive()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public Boolean InsertOrUpdateTSalesIncentive_ForExcelUpload(DataTable dtSalesIncentive)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (DataRow dr in dtSalesIncentive.Rows)
                    {
                        if (!String.IsNullOrEmpty(dr[4].ToString()))
                        {
                            DbParameter SalesIncentiveIDP = provider.CreateParameter("SalesIncentiveID", DBNull.Value, DbType.Int32);
                            DbParameter DateP = provider.CreateParameter("Date", "01-" + dr[0].ToString(), DbType.String);
                            DbParameter InvoiceNoP = provider.CreateParameter("InvoiceNo", dr[2].ToString(), DbType.String);
                            DbParameter InvoiceDateP = provider.CreateParameter("InvoiceDate", Convert.ToDateTime(dr[3].ToString()), DbType.DateTime);
                            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", dr[4].ToString(), DbType.String);
                            DbParameter AadhaarNoP = provider.CreateParameter("AadhaarNo", dr[7].ToString(), DbType.String);
                            DbParameter SalesLevelP = provider.CreateParameter("SalesLevel", dr[6].ToString(), DbType.Int32);
                            DbParameter ModelP = provider.CreateParameter("Model", dr[10].ToString(), DbType.String);
                            DbParameter IncentiveAmountP = provider.CreateParameter("IncentiveAmount", Convert.ToDecimal(dr[11].ToString()), DbType.Decimal);

                            DbParameter[] Params = new DbParameter[9] { SalesIncentiveIDP, DateP, InvoiceNoP, InvoiceDateP, DealerCodeP, AadhaarNoP, SalesLevelP, ModelP, IncentiveAmountP };
                            provider.Insert("InsertOrUpdateTSalesIncentive_ForExcelUpload", Params);
                        }
                    }
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BSalesIncentive", "InsertOrUpdateTSalesIncentive_ForExcelUpload", ex);
                return false;
            }
            return true;
        }
        public DataTable GetSalesIncentive(int? SalesIncentiveID, int? Year, int? Month, int? DealerID, int? SalesEngineerID, string InvoiceNo, DateTime? DateFrom, DateTime? DateTo, int? UserID, int? PageIndex = null, int? PageSize = null)
        {
            DataTable dtSalesIncentive = new DataTable();
            try
            {
                DbParameter SalesIncentiveIDP = provider.CreateParameter("SalesIncentiveID", SalesIncentiveID, DbType.Int32);
                DbParameter YearP = provider.CreateParameter("Year", Year, DbType.Int32);
                DbParameter MonthP = provider.CreateParameter("Month", Month, DbType.Int32);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter SalesEngineerIDP = provider.CreateParameter("SalesEngineerID", SalesEngineerID, DbType.Int32);
                DbParameter InvoiceNoP = provider.CreateParameter("InvoiceNo", InvoiceNo, DbType.String);
                DbParameter DateFromP = provider.CreateParameter("DateFrom", DateFrom, DbType.DateTime);
                DbParameter DateToP = provider.CreateParameter("DateTo", DateTo, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);
                DbParameter[] Params = new DbParameter[11] { SalesIncentiveIDP, YearP, MonthP, DealerIDP, SalesEngineerIDP, InvoiceNoP, DateFromP, DateToP, UserIDP, PageIndexP, PageSizeP };

                using (DataSet DataSet = provider.Select("GetSalesIncentive", Params))
                {
                    if (DataSet != null)
                    {
                        dtSalesIncentive = DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtSalesIncentive;
        }
    }
}