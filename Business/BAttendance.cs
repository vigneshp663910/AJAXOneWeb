using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business
{
    public class BAttendance
    {
        private IDataAccess provider;
        public BAttendance()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public DataTable GetAttendance(DateTime? AttendanceDateFrom, DateTime? AttendanceDateTo, int? DealerID, int UserID)
        {
            DbParameter AttendanceDateFromP = provider.CreateParameter("AttendanceDateFrom", AttendanceDateFrom, DbType.DateTime);
            DbParameter AttendanceDateToP = provider.CreateParameter("AttendanceDateTo", AttendanceDateTo, DbType.DateTime);
            DbParameter DIDP = provider.CreateParameter("DID", DealerID, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { AttendanceDateFromP, AttendanceDateToP, DIDP, UserIDP };
            try
            {
                using (DataSet ds = provider.Select("GetAttendance", Params))
                {
                    if (ds != null)
                    {
                        return ds.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }

        public Boolean InsertOrUpdateAttendance( int UserID)
        {
            int success = 0; 
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32); 
            DbParameter[] Params = new DbParameter[1] {  UserIDP  };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("InsertOrUpdateAttendance", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BAttendance", "InsertOrUpdateAttendance", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BAttendance", " InsertOrUpdateAttendance", ex);
                return false;
            }
            return true;
        }

    }
}
