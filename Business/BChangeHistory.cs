using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Business
{
    public class BChangeHistory
    {
        private IDataAccess provider;
        public BChangeHistory()
        {
            provider = new ProviderFactory().GetProvider();
        }
        
        public DataTable GetChangeHistoryCustomerFields(int? ChangeHistoryFieldID, string Description)
        {
           
            DbParameter ChangeHistoryFieldIDP = provider.CreateParameter("ChangeHistoryFieldID", ChangeHistoryFieldID, DbType.Int32);
            DbParameter DescriptionP = provider.CreateParameter("Description", Description, DbType.String);
            DbParameter[] Params = new DbParameter[2] { ChangeHistoryFieldIDP, DescriptionP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("GetChangeHistoryCustomerFields", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        return EmployeeDataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }

        public DataTable GetChangeHistoryLeadFields(int? ChangeHistoryFieldID, string Description)
        {

            DbParameter ChangeHistoryFieldIDP = provider.CreateParameter("ChangeHistoryFieldID", ChangeHistoryFieldID, DbType.Int32);
            DbParameter DescriptionP = provider.CreateParameter("Description", Description, DbType.String);
            DbParameter[] Params = new DbParameter[2] { ChangeHistoryFieldIDP, DescriptionP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("GetChangeHistoryLeadFields", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        return EmployeeDataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }

        public DataTable GetChangeHistoryQuotationFields(int? ChangeHistoryFieldID, string Description)
        {

            DbParameter ChangeHistoryFieldIDP = provider.CreateParameter("ChangeHistoryFieldID", ChangeHistoryFieldID, DbType.Int32);
            DbParameter DescriptionP = provider.CreateParameter("Description", Description, DbType.String);
            DbParameter[] Params = new DbParameter[2] { ChangeHistoryFieldIDP, DescriptionP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("GetChangeHistoryQuotationFields", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        return EmployeeDataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }

        public DataTable GetCustomerChangeHistory(string CustomerCode, int? ChangeHistoryFieldID, DateTime? DateFrom, DateTime? DateTo)
        {
            DbParameter CustomerIDP = provider.CreateParameter("CustomerCode",string.IsNullOrEmpty(CustomerCode)?null: CustomerCode, DbType.String);
            DbParameter ChangeHistoryFieldP = provider.CreateParameter("ChangeHistoryFieldID",  ChangeHistoryFieldID , DbType.Int32);
            DbParameter DateFromP = provider.CreateParameter("DateFrom", DateFrom, DbType.DateTime);
            DbParameter DateToP = provider.CreateParameter("DateTo", DateTo, DbType.DateTime);
            try
            {
                using (DataSet DS = provider.Select("GetCustomerChangeHistory", new DbParameter[4] { CustomerIDP, ChangeHistoryFieldP, DateFromP, DateToP }))
                {
                    if (DS != null)
                    {
                        return DS.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }

        public DataTable GetLeadChangeHistory(string LeadNumber, int? ChangeHistoryFieldID, DateTime? DateFrom, DateTime? DateTo)
        {
            DbParameter LeadNumberP = provider.CreateParameter("LeadNumber", string.IsNullOrEmpty(LeadNumber) ? null : LeadNumber, DbType.String);
            DbParameter ChangeHistoryFieldP = provider.CreateParameter("ChangeHistoryFieldID", ChangeHistoryFieldID, DbType.Int32);
            DbParameter DateFromP = provider.CreateParameter("DateFrom", DateFrom, DbType.DateTime);
            DbParameter DateToP = provider.CreateParameter("DateTo", DateTo, DbType.DateTime);
            try
            {
                using (DataSet DS = provider.Select("GetLeadChangeHistory", new DbParameter[4] { LeadNumberP, ChangeHistoryFieldP, DateFromP, DateToP }))
                {
                    if (DS != null)
                    {
                        return DS.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }

        public DataTable GetQuotationChangeHistory(Int64? RefQuotationID, int? ChangeHistoryFieldID, DateTime? DateFrom, DateTime? DateTo)
        {
            DbParameter RefQuotationIDP = provider.CreateParameter("RefQuotationID", RefQuotationID, DbType.Int64);
            DbParameter ChangeHistoryFieldP = provider.CreateParameter("ChangeHistoryFieldID", ChangeHistoryFieldID, DbType.Int32);
            DbParameter DateFromP = provider.CreateParameter("DateFrom", DateFrom, DbType.DateTime);
            DbParameter DateToP = provider.CreateParameter("DateTo", DateTo, DbType.DateTime);
            try
            {
                using (DataSet DS = provider.Select("GetQuotationChangeHistory", new DbParameter[4] { RefQuotationIDP, ChangeHistoryFieldP, DateFromP, DateToP }))
                {
                    if (DS != null)
                    {
                        return DS.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
    }
}
