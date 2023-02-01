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
    public class BDealerOperatorDetails
    {
        private IDataAccess provider;
        public BDealerOperatorDetails()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public Boolean InsertOrUpdateTDealerOperatorDetails_ForExcelUpload(DataTable dtDealerOperatorDetails)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (DataRow dr in dtDealerOperatorDetails.Rows)
                    {
                        if (!String.IsNullOrEmpty(dr[3].ToString()))
                        {
                            DbParameter DealerOperatorDetailsIDP = provider.CreateParameter("DealerOperatorDetailsID", DBNull.Value, DbType.Int32);
                            DbParameter DealerNameP = provider.CreateParameter("DealerCode", dr[3].ToString(), DbType.String);
                            DbParameter StateP = provider.CreateParameter("State", dr[1].ToString(), DbType.String);
                            DbParameter OperatorNameP = provider.CreateParameter("OperatorName", dr[4].ToString(), DbType.String);
                            DbParameter ContactNoP = provider.CreateParameter("ContactNo", dr[5].ToString(), DbType.String);
                            DbParameter EmailIDP = provider.CreateParameter("EmailID", dr[6].ToString(), DbType.String);
                            DbParameter YearsOfExperienceP = provider.CreateParameter("YearsOfExperience", string.IsNullOrEmpty(dr[7].ToString())?(decimal?)null:Convert.ToDecimal(dr[7].ToString()), DbType.Decimal);

                            DbParameter[] Params = new DbParameter[7] { DealerOperatorDetailsIDP, DealerNameP, StateP, OperatorNameP, ContactNoP, EmailIDP, YearsOfExperienceP };
                            provider.Insert("InsertOrUpdateTDealerOperatorDetails_ForExcelUpload", Params);
                        }
                    }
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDealerOperatorDetails", "InsertOrUpdateTDealerOperatorDetails_ForExcelUpload", ex);
                return false;
            }
            return true;
        }
        public DataTable GetDealerOperatorDetails(int? DealerOperatorDetailsID, int? DealerID, int? StateID, int? RegionID, string OperatorName, int? UserID, int? PageIndex = null, int? PageSize = null)
        {
            DataTable dtDealerOperatorDetails = new DataTable();
            try
            {
                DbParameter DealerOperatorDetailsIDP = provider.CreateParameter("DealerOperatorDetailsID", DealerOperatorDetailsID, DbType.Int32);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter RegionIDP = provider.CreateParameter("RegionID", RegionID, DbType.Int32);
                DbParameter OperatorNameP = provider.CreateParameter("OperatorName", OperatorName, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);
                DbParameter[] Params = new DbParameter[8] { DealerOperatorDetailsIDP, DealerIDP, StateIDP, RegionIDP, OperatorNameP, UserIDP, PageIndexP, PageSizeP };

                using (DataSet DataSet = provider.Select("GetDealerOperatorDetails", Params))
                {
                    if (DataSet != null)
                    {
                        dtDealerOperatorDetails = DataSet.Tables[0];
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
            return dtDealerOperatorDetails;
        }
    }
}
