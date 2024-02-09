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
    public class BDMS_OpenTable
    {
        public DataTable GetDataFromTable(string TableName)
        {
            DataTable dt = null;
            TraceLogger.Log(DateTime.Now);
       
            try
            {
                string query = "SELECT " + GetDataFieldTable(TableName) + " from " + TableName + " Group by " + GetDataFieldTable(TableName);
                dt = new BPG().OutputDataTable(query);
              
                return dt;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_MTTR", "GetMttr", ex);
                throw ex;
            }
            return dt;
        }

        public string GetDataFieldTable(string TableName)
        {
            DataTable dt = null;
            TraceLogger.Log(DateTime.Now);
            string field = "";
          
            try
            {
                string query = "select column_name   from information_schema.columns  where table_name = '" + TableName + "' and data_type  not in ( 'date','Boolean')  ;";
                dt = new BPG().OutputDataTable(query);
                foreach (DataRow dr in dt.Rows)
                {
                    field = field + ", " + Convert.ToString(dr["column_name"]);
                }
                field = field.Trim(',');
                return field;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_MTTR", "GetMttr", ex);
                throw ex;
            }
            return field;
        }
    }
}
