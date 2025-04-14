using DataAccess;
using Properties;
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
    public class BFeedback
    {
        private IDataAccess provider;
        public BFeedback()
        {
            provider = new ProviderFactory().GetProvider();
        } 
        public long coTg_Insert_AppsFeedBack(PComment Comment)
        {
            long success = 0;
            try
            {
                DbParameter ModuleNo = provider.CreateParameter("ModuleNo", Comment.ModuleNo, DbType.Int64);
                DbParameter UserID = provider.CreateParameter("UserID", Comment.UserID, DbType.Int64);
                DbParameter Comments = provider.CreateParameter("Comments", Comment.Comments, DbType.String);
                DbParameter Stars = provider.CreateParameter("Ratings", Comment.Ratings, DbType.Int64);
                DbParameter[] Params = new DbParameter[4] { ModuleNo, UserID, Comments, Stars };


                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("coTg_Insert_AppsFeedBack", Params);
                    scope.Complete();
                }

            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BFeedback", "coTg_Insert_AppsFeedBack", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BHome", "coTg_Insert_AppsFeedBack", ex);
                throw ex;
            }
            return success;
        }
    }
}
