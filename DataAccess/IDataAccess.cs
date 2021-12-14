using System;
using System.Data;
using System.Data.Common;

namespace DataAccess
{
    public interface IDataAccess
    {
        DbParameter CreateParameter(String paramName, object paramValue, DbType dbType, Int32 paramterDirection = 1);
        DataSet Select(String storedProcedureName, DbParameter[] parameters);
        DataSet Select(String storedProcedureName, DbParameter[] parameters, Int32 timeOut);
        DataSet Select(String storedProcedureName);
        DataSet SelectUsingQuery(String query);
        Int32 InsertUsingQuery(String query, Boolean outputValueRequired = false);
        //Object InsertUsingQuery(string query);
        Int32 Insert(String storedProcedureName, DbParameter[] parameters, Boolean outputValueRequired = false);
        //Object Insert(string storedProcedureName, DbParameter[] parameters);
        Int32 UpdateUsingQuery(String query, Boolean outputValueRequired = false);
        Int32 Update(String storedProcedureName, DbParameter[] parameters, Boolean outputValueRequired = false);
        Int32 DeleteUsingQuery(String query, Boolean outputValueRequired = false);
        Int32 Delete(String storedProcedureName, DbParameter[] parameters, Boolean outputValueRequired = false);
        Object GetScalar(String storedProcedureName, DbParameter[] parameters);
        Object GetScalar(String storedProcedureName);
    }
    public interface IDataAccessNP
    {
        DataTable ExecuteReader(String storedProcedureName);
    }
}
