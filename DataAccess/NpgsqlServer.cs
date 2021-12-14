using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class NpgsqlServer : IDataAccessNP
    {
        #region Class Variables
        private String connectionString;
        #endregion

        #region Constructor
        public NpgsqlServer()
        { 
            connectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["DSNConnectionString"]);
        }
        #endregion

        #region Public Methods
               
        /// <returns>DataSet</returns>
        public DataTable ExecuteReader(String Query)
        {
            DateTime traceStartTime = DateTime.Now;
            DataTable dt = new DataTable();
            OdbcDataReader reader = null;
            OdbcConnection connection = null;
            try
            {
                connection = new OdbcConnection(connectionString);
                OdbcCommand command = new OdbcCommand(Query, connection);
                connection.Open();
                command.CommandTimeout = 6000;
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);

               dt.Clear();
            
                dt.Load(reader);
                reader.Close();
                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
                connection.Close();
            }
        }
        public int ExecuteNonQuery(String Query)
        {
            DateTime traceStartTime = DateTime.Now; 
            OdbcConnection connection = new OdbcConnection(connectionString);
            try
            {
               
                OdbcCommand command = new OdbcCommand(Query, connection);
                connection.Open();
                int reader = command.ExecuteNonQuery();
                connection.Close(); 
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                connection.Close();
            }
        }
        public string ExecuteScalar(String Query)
        {
            DateTime traceStartTime = DateTime.Now; 
            OdbcConnection connection = new OdbcConnection(connectionString);
            try
            {
               
                OdbcCommand command = new OdbcCommand(Query, connection);
                connection.Open();
                object reader = command.ExecuteScalar();
                connection.Close();
                return Convert.ToString(reader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public Boolean UpdateTransactions(List<string> Querys)
        {
            DateTime traceStartTime = DateTime.Now;
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand();
                OdbcTransaction transaction = null;
                command.Connection = connection;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    command.Connection = connection;
                    command.Transaction = transaction;
                    foreach (string Query in Querys)
                    {
                        command.CommandText = Query;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    { }
                }
            }
            return false;
        }
        public string QuotationCreationUpdateTransactions(List<string> Querys, string DealerCode, List<string> Quotation)
        {
            DateTime traceStartTime = DateTime.Now;
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand();
                OdbcTransaction transaction = null;
                command.Connection = connection;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();




                    string QuotationNumber = "";
                    Boolean isOldQuotationNumber = false;
                    foreach (string Quo in Quotation)
                    {
                        string Status = ExecuteScalar("select s_status from dssor_sales_order_hdr where s_tenant_id =" + DealerCode + " and 	p_so_id ='" + Quo + "'");
                        if ((Status == "DRAFT") || (Status == "QUOTATION"))
                        {
                            QuotationNumber = Quo;
                            isOldQuotationNumber = true;
                            break;
                        }
                    }
                    if (isOldQuotationNumber == false)
                    {
                        QuotationNumber = ExecuteScalar("select r_currentnumber from m_idseries_interval where s_tenant_id =" + DealerCode + " and 	s_series_id ='SOStand'");
                        QuotationNumber = Convert.ToString(Convert.ToInt64(QuotationNumber) + 1);
                    }
                    command.Connection = connection;
                    command.Transaction = transaction;
                    int QueryCount = 0;
                    foreach (string Query in Querys)
                    {
                        QueryCount = QueryCount + 1;
                        if (isOldQuotationNumber == true)
                        {
                            if (QueryCount != 1)
                            {
                                command.CommandText = Query.Replace("@@QuotationNumber", QuotationNumber);
                                command.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            command.CommandText = Query.Replace("@@QuotationNumber", QuotationNumber);
                            command.ExecuteNonQuery();
                        }
                    }

                    if (isOldQuotationNumber == false)
                    {
                        long QuotationNumberN = Convert.ToInt64(QuotationNumber);
                        string query = " Update m_idseries_interval set  r_currentnumber = '" + QuotationNumberN + "'    where s_tenant_id =" + DealerCode + " and 	s_series_id ='SOStand'";
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return QuotationNumber;
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    { }
                }
            }
            return "";
        }

        public string WarrantyPOQuotationCreationUpdateTransactions(List<string> Querys, string DealerCode,string ICTicketNo, List<string> Quotation)
        {
            DateTime traceStartTime = DateTime.Now;
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand();
                OdbcTransaction transaction = null;
                command.Connection = connection;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    string QuotationNumber = "";
                    Boolean isOldQuotationNumber = false;
                    foreach (string Quo in Quotation)
                    {
                        string Status = ExecuteScalar("select s_status from dppor_purc_order_hdr where s_tenant_id =" + DealerCode + " and 	p_po_id ='" + Quo + "'");
                        if (Status == "DRAFT")
                        {
                            QuotationNumber = Quo;
                            isOldQuotationNumber = true;
                            break;
                        }
                    }
                    if (isOldQuotationNumber == false)
                    {
                        QuotationNumber = ExecuteScalar("select r_currentnumber from m_idseries_interval where s_tenant_id =" + DealerCode + " and 	s_series_id ='PO'");
                        QuotationNumber = Convert.ToString(Convert.ToInt64(QuotationNumber) + 1);
                    }
                    int ICCount = Convert.ToInt32(ExecuteScalar("select count(*) from af_ic_tickets_purc where f_ic_ticket_id = " + ICTicketNo + " and s_tenant_id =" + DealerCode + " and 	p_po_id ='" + QuotationNumber + "'"));
                    
                    command.Connection = connection;
                    command.Transaction = transaction;

                    if (ICCount == 0)
                    {
                        command.CommandText = "INSERT INTO public.af_ic_tickets_purc(f_ic_ticket_id, s_tenant_id, p_po_id)VALUES (" + ICTicketNo + "," + DealerCode + ",'" + QuotationNumber + "')"; //Query.Replace("@@QuotationNumber", QuotationNumber);
                        command.ExecuteNonQuery();
                    }

                    int QueryCount = 0;
                    foreach (string Query in Querys)
                    {
                        QueryCount = QueryCount + 1;
                        if (isOldQuotationNumber == true)
                        {
                            if (QueryCount != 1)
                            {
                                command.CommandText = Query.Replace("@@QuotationNumber", QuotationNumber);
                                command.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            command.CommandText = Query.Replace("@@QuotationNumber", QuotationNumber);
                            command.ExecuteNonQuery();
                        }
                    }

                    if (isOldQuotationNumber == false)
                    {
                        long QuotationNumberN = Convert.ToInt64(QuotationNumber);
                        string query = " Update m_idseries_interval set  r_currentnumber = '" + QuotationNumberN + "'    where s_tenant_id =" + DealerCode + " and 	s_series_id ='PO'";
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return QuotationNumber;
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    { }
                }
            }
            return "";
        }

        public Boolean UpdateTransactionsJSNQuotation(List<string> Querys, string DealerCode)
        {
            DateTime traceStartTime = DateTime.Now;
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                OdbcCommand command = new OdbcCommand();
                OdbcTransaction transaction = null;
                command.Connection = connection;
                int QueryCount = 0;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    string QuotationNumber = "";
                    QuotationNumber = ExecuteScalar("select r_currentnumber from m_idseries_interval where s_tenant_id =" + DealerCode + " and 	s_series_id ='SOStand'");
                    QuotationNumber = Convert.ToString(Convert.ToInt64(QuotationNumber) + 1);

                    command.Connection = connection;
                    command.Transaction = transaction;
                   
                    foreach (string Query in Querys)
                    {
                        QueryCount = QueryCount + 1;

                        command.CommandText = Query.Replace("@@QuotationNumber", QuotationNumber);
                        command.ExecuteNonQuery();
                    }

                    long QuotationNumberN = Convert.ToInt64(QuotationNumber);
                    string query = " Update m_idseries_interval set  r_currentnumber = '" + QuotationNumberN + "'    where s_tenant_id =" + DealerCode + " and 	s_series_id ='SOStand'";
                    command.CommandText = query;
                    command.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    { } 
                }
            }
            return false;
        }

        #endregion 
    }
}
