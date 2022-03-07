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
    public class BPresalesMasters
    {
        private IDataAccess provider;
        public BPresalesMasters()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PLeadSource> GetLeadSource(int? SourceID, string Source)
        {
            List<PLeadSource> pLeadSources = new List<PLeadSource>();
            try
            {
                DbParameter SourceIDP = provider.CreateParameter("SourceID", SourceID, DbType.Int32);
                DbParameter SourceP = provider.CreateParameter("Source", Source, DbType.String);
                DbParameter[] Params = new DbParameter[2] { SourceIDP, SourceP };
                using (DataSet DataSet = provider.Select("GetLeadSource", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            pLeadSources.Add(new PLeadSource()
                            {
                                SourceID = Convert.ToInt32(dr["LeadSourceID"]),
                                Source = Convert.ToString(dr["LeadSource"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BPresalesMasters", "GetLeadSource", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BPresalesMasters", "GetLeadSource", ex);
                throw ex;
            }
            return pLeadSources;
        }
        public int InsertOrUpdateLeadSource(long? LeadSourceID, string LeadSource, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            DbParameter LeadSourceIDP = provider.CreateParameter("LeadSourceID", LeadSourceID, DbType.Int32);
            DbParameter LeadSourceP = provider.CreateParameter("LeadSource", LeadSource, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { LeadSourceIDP, LeadSourceP, IsActiveP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateLeadSource", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BPresalesMasters", "ZDMS_InsertOrUpdateLeadSource", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BPresalesMasters", " ZDMS_InsertOrUpdateLeadSource", ex);
                throw ex;
            }
            return success;
        }
        public List<PActionType> GetActionType(int? ActionTypeID, string ActionType)
        {
            List<PActionType> pActionTypes = new List<PActionType>();
            try
            {
                DbParameter ActionTypeIDP = provider.CreateParameter("ActionTypeID", ActionTypeID, DbType.Int32);
                DbParameter ActionTypeP = provider.CreateParameter("ActionType", ActionType, DbType.String);
                DbParameter[] Params = new DbParameter[2] { ActionTypeIDP, ActionTypeP };
                using (DataSet DataSet = provider.Select("GetActionType", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            pActionTypes.Add(new PActionType()
                            {
                                ActionTypeID = Convert.ToInt32(dr["ActionTypeID"]),
                                ActionType = Convert.ToString(dr["ActionType"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BPresalesMasters", "GetActionType", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BPresalesMasters", "GetActionType", ex);
                throw ex;
            }
            return pActionTypes;
        }
        public int InsertOrUpdateActionType(long? ActionTypeID, string ActionType, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            DbParameter ActionTypeIDP = provider.CreateParameter("ActionTypeID", ActionTypeID, DbType.Int32);
            DbParameter ActionTypeP = provider.CreateParameter("ActionType", ActionType, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { ActionTypeIDP, ActionTypeP, IsActiveP, UserIDP};
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateActionType", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BPresalesMasters", "ZDMS_InsertOrUpdateActionType", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BPresalesMasters", " ZDMS_InsertOrUpdateActionType", ex);
                throw ex;
            }
            return success;
        }
    }
}
