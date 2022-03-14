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
    public class BTestSN
    {
        private IDataAccess provider;
        public BTestSN()
        {
            provider = new ProviderFactory().GetProvider();
        }
        //public int InsertOrUpdateMake(int? MakeID, string Make, Boolean IsActive, int UserID)
        //{
        //    TraceLogger.Log(DateTime.Now);
        //    int success = 0;
        //    DbParameter MakeIDP = provider.CreateParameter("MakeID", MakeID, DbType.Int32);
        //    DbParameter MakeP = provider.CreateParameter("Make", Make, DbType.String);
        //    DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
        //    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //    DbParameter[] Params = new DbParameter[4] { MakeIDP, MakeP, IsActiveP, UserIDP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            success = provider.Insert("InsertUpdateMake", Params);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BTestSN", "InsertUpdateMake", sqlEx);
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BTestSN", "InsertUpdateMake", ex);
        //        throw ex;
        //    }
        //    return success;
        //}

        //public int InsertOrUpdateProductType(int? ProductTypeID, string ProductType, Boolean IsActive, int UserID)
        //{
        //    TraceLogger.Log(DateTime.Now);
        //    int success = 0;
        //    DbParameter ProductTypeIDP = provider.CreateParameter("ProductTypeID", ProductTypeID, DbType.Int32);
        //    DbParameter ProductTypeP = provider.CreateParameter("ProductType", ProductType, DbType.String);
        //    DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
        //    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //    DbParameter[] Params = new DbParameter[4] { ProductTypeIDP, ProductTypeP, IsActiveP, UserIDP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            success = provider.Insert("InsertUpdateProductType", Params);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BTestSN", "InsertUpdateProductType", sqlEx);
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BTestSN", "InsertUpdateProductType", ex);
        //        throw ex;
        //    }
        //    return success;
        //}

        //public int InsertOrUpdateProduct(int? ProductID, string Product, Boolean IsActive, int UserID)
        //{
        //    TraceLogger.Log(DateTime.Now);
        //    int success = 0;
        //    DbParameter ProductIDP = provider.CreateParameter("ProductID", ProductID, DbType.Int32);
        //    DbParameter ProductP = provider.CreateParameter("Product", Product, DbType.String);
        //    DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
        //    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //    DbParameter[] Params = new DbParameter[4] { ProductIDP, ProductP, IsActiveP, UserIDP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            success = provider.Insert("InsertUpdateProduct", Params);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BTestSN", "InsertUpdateProduct", sqlEx);
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BTestSN", "InsertUpdateProduct", ex);
        //        throw ex;
        //    }
        //    return success;
        //}
    }
}
