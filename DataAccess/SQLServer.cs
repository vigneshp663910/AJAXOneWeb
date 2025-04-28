using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace DataAccess
{
    /// <summary>
    /// This class contains methods to create sql parameter, select, insert, update and delete methods.
    /// </summary>
    public class SQLServer:IDataAccess
    {
        #region Class Variables
        private String connectionString;
        #endregion

        #region Constructor
        public SQLServer()
        {
             
                connectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["ConnectionString"]);
            
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method accepts parameter name, parameter value, parameter data
        /// type and paramater direction as input and creates db parameter of
        /// type SqlParameter.
        /// </summary>
        /// <param name="paramName">String</param>
        /// <param name="paramValue">object</param>
        /// <param name="dbType">DbType</param>
        /// <param name="parameterDirection">Int32</param>
        /// <returns>DbParameter</returns>
        public DbParameter CreateParameter(String paramName, Object paramValue,
            DbType dbType, Int32 parameterDirection = 1)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                SqlParameter parameters = new SqlParameter();
                parameters.DbType = dbType;
                parameters.ParameterName = String.Concat("@", paramName);
                parameters.Value = paramValue;

                if (parameterDirection == Convert.ToInt32(ParameterDirection.Output))
                    parameters.Direction = ParameterDirection.Output;

               // TraceLogger.Log(traceStartTime);
                return parameters;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This methods accepts stored procedure name and an array of parameters
        /// as input and calls ExecuteDataSet method by passing stored procedure
        /// name, command type and an array of paramteres. This method returns a
        /// data set.
        /// </summary>
        /// <param name="storedProcedureName">String</param>
        /// <param name="parameters">DbParameter[]</param>        
        /// <returns>DataSet</returns>
        public DataSet Select(String storedProcedureName,
            DbParameter[] parameters)
        {
            DateTime traceStartTime = DateTime.Now;
            DataSet selectedData = new DataSet();
            try
            {
                selectedData = ExecuteDataset(storedProcedureName,
                    CommandType.StoredProcedure, parameters);

               // TraceLogger.Log(traceStartTime);
                return selectedData;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This methods accepts stored procedure name and an array of parameters
        /// as input and calls ExecuteDataSet method by passing stored procedure
        /// name, command type, an array of paramteres and time out value. This
        /// method returns a data set.
        /// </summary>
        /// <param name="storedProcedureName">String</param>
        /// <param name="parameters">DbParameter[]</param>
        /// <param name="timeOut">Int32</param>        
        /// <returns>DataSet</returns>
        //public DataSet Select(String storedProcedureName,            DbParameter[] parameters, Int32 timeOut)
        //{
        //    DateTime traceStartTime = DateTime.Now;
        //    DataSet selectedData = new DataSet();
        //    try
        //    {
        //        selectedData = ExecuteDataset(storedProcedureName,
        //            CommandType.StoredProcedure, parameters, null, timeOut);

        //        //TraceLogger.Log(traceStartTime);
        //        return selectedData;
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        
        /// <summary>
        /// This methods accepts stored procedure name and parameters as input
        /// and calls ExecuteDataSet method by passing stored procedure name,
        /// command type, an array of paramteres and time out value. This
        /// method returns a data set.
        /// </summary>
        /// <param name="storedProcedureName">String</param>        
        /// <returns>DataSet</returns>
        public DataSet Select(String storedProcedureName)
        {
            DateTime traceStartTime = DateTime.Now;
            DataSet selectedData = new DataSet();
            try
            {
                selectedData = ExecuteDataset(storedProcedureName,
                    CommandType.StoredProcedure);

               // TraceLogger.Log(traceStartTime);
                return selectedData;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataSet SelectUsingQuery(string query)
        {
            try
            {
                return ExecuteDataset(query);
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public int Insert(string storedProcedureName, DbParameter[] parameters, Boolean outputValueRequired = false)
        {
            try
            {
                return ExecuteNonQuery(storedProcedureName, outputValueRequired,
                    CommandType.StoredProcedure, parameters);
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public int Update(string storedProcedureName, DbParameter[] parameters, Boolean outputValueRequired = false)
        {
            try
            {
                return ExecuteNonQuery(storedProcedureName, outputValueRequired,
                    CommandType.StoredProcedure, parameters);
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        //public int Delete(string storedProcedureName, DbParameter[] parameters, Boolean outputValueRequired = false)
        //{
        //    try
        //    {
        //        return ExecuteNonQuery(storedProcedureName, outputValueRequired,
        //            CommandType.StoredProcedure, parameters);
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        //public object GetScalar(string storedProcedureName, DbParameter[] parameters)
        //{
        //    try
        //    {
        //        return ExecuteScalar(storedProcedureName,
        //            CommandType.StoredProcedure, parameters);
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
         
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        private DataSet ExecuteDataset(string commandText,
            CommandType commandType = CommandType.Text,
            DbParameter[] parameters = null,
            SqlTransaction sqlTransaction = null, int commandTimeout = 600)
        {            
            try
            {
                Database vendorPortalDb = new SqlDatabase(connectionString);
                
                DbCommand command = vendorPortalDb.
                    GetSqlStringCommand(commandText);
                
                if (parameters != null)
                {
                    foreach (DbParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                command.CommandType = commandType;

                if (sqlTransaction != null)
                    command.Transaction = sqlTransaction;

                if (commandTimeout > 0)
                    command.CommandTimeout = commandTimeout;
                
                DataSet result = vendorPortalDb.ExecuteDataSet(command);

                return result;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        private int ExecuteNonQuery(string commandText,
            Boolean outputValueRequired = false,
            CommandType commandType = CommandType.Text,
            DbParameter[] parameters = null,
            SqlTransaction sqlTransaction = null, int commandTimeout = 600
            )
        {
            try
            {
                Int32 outputValue = 0;
                Database vendorPortalDb = new SqlDatabase(connectionString);

                DbCommand command = vendorPortalDb.
                    GetSqlStringCommand(commandText);

                if (parameters != null)
                {
                    foreach (DbParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                command.CommandType = commandType;

                if (sqlTransaction != null)
                    command.Transaction = sqlTransaction;

                if (commandTimeout > 0)
                    command.CommandTimeout = commandTimeout;
                else
                    command.CommandTimeout = 300;

                outputValue = vendorPortalDb.ExecuteNonQuery(command);

                if (outputValueRequired)
                    return Convert.ToInt32(vendorPortalDb.GetParameterValue(command, "@OutValue"));
                else
                    return outputValue;
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        //private object ExecuteScalar(string commandText,
        //    CommandType commandType = CommandType.Text,
        //    DbParameter[] parameters = null,
        //    SqlTransaction sqlTransaction = null, int commandTimeout = 600)
        //{
        //    try
        //    {
        //        Database vendorPortalDb = new SqlDatabase(connectionString);

        //        DbCommand command = vendorPortalDb.
        //            GetSqlStringCommand(commandText);
                
        //        if (parameters != null)
        //        {
        //            foreach (DbParameter parameter in parameters)
        //            {
        //                command.Parameters.Add(parameter);
        //            }
        //        }

        //        command.CommandType = commandType;

        //        if (sqlTransaction != null)
        //            command.Transaction = sqlTransaction;

        //        if (commandTimeout > 0)
        //            command.CommandTimeout = commandTimeout;

        //        return vendorPortalDb.ExecuteScalar(command);
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
