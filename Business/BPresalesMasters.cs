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
            DbParameter[] Params = new DbParameter[4] { ActionTypeIDP, ActionTypeP, IsActiveP, UserIDP };
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
        public List<PCustomerAttributeMain> GetCustomerAttributeMain(int? AttributeMainID, string AttributeMain)
        {
            List<PCustomerAttributeMain> pCustomerAttributes = new List<PCustomerAttributeMain>();
            try
            {
                DbParameter AttributeMainIDP = provider.CreateParameter("AttributeMainID", AttributeMainID, DbType.Int32);
                DbParameter AttributeMainP = provider.CreateParameter("AttributeMain", AttributeMain, DbType.String);
                DbParameter[] Params = new DbParameter[2] { AttributeMainIDP, AttributeMainP };
                using (DataSet DataSet = provider.Select("GetCustomerAttributeMain", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            pCustomerAttributes.Add(new PCustomerAttributeMain()
                            {
                                AttributeMainID = Convert.ToInt32(dr["AttributeMainID"]),
                                AttributeMain = Convert.ToString(dr["AttributeMain"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BPresalesMasters", "GetCustomerAttributeMain", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BPresalesMasters", "GetCustomerAttributeMain", ex);
                throw ex;
            }
            return pCustomerAttributes;
        }
        public int InsertOrUpdateCustomerAttributeMain(long? CustomerAttributeMainID, string CustomerAttributeMain, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            DbParameter CustomerAttributeMainIDP = provider.CreateParameter("AttributeMainID", CustomerAttributeMainID, DbType.Int32);
            DbParameter CustomerAttributeMainP = provider.CreateParameter("AttributeMain", CustomerAttributeMain, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { CustomerAttributeMainIDP, CustomerAttributeMainP, IsActiveP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateCustomerAttributeMain", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BPresalesMasters", "ZDMS_InsertOrUpdateCustomerAttributeMain", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BPresalesMasters", " ZDMS_InsertOrUpdateCustomerAttributeMain", ex);
                throw ex;
            }
            return success;
        }
        public List<PCustomerAttributeSub> GetCustomerAttributeSub(int? AttributeSubID, int? AttributeMainID, string AttributeSub)
        {
            List<PCustomerAttributeSub> pCustomerAttributeSubs = new List<PCustomerAttributeSub>();
            try
            {
                DbParameter AttributeSubIDP = provider.CreateParameter("AttributeSubID", AttributeSubID, DbType.Int32);
                DbParameter AttributeMainIDP = provider.CreateParameter("AttributeMainID", AttributeMainID, DbType.Int32);
                DbParameter AttributeSubP = provider.CreateParameter("AttributeSub", AttributeSub, DbType.String);
                DbParameter[] Params = new DbParameter[3] { AttributeSubIDP, AttributeMainIDP, AttributeSubP };
                using (DataSet DataSet = provider.Select("GetCustomerAttributeSub", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            pCustomerAttributeSubs.Add(new PCustomerAttributeSub()
                            {
                                AttributeMainID = Convert.ToInt32(dr["AttributeMainID"]),
                                AttributeSubID = Convert.ToInt32(dr["AttributeSubID"]),
                                AttributeSub = Convert.ToString(dr["AttributeSub"]),
                                AttributeMain = new PCustomerAttributeMain()
                                {
                                    AttributeMainID = Convert.ToInt32(dr["AttributeMainID"]),
                                    AttributeMain = Convert.ToString(dr["AttributeMain"])
                                }
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BPresalesMasters", "GetCustomerAttributeSub", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BPresalesMasters", "GetCustomerAttributeSub", ex);
                throw ex;
            }
            return pCustomerAttributeSubs;
        }
        public int InsertOrUpdateCustomerAttributeSub(long? AttributeSubID,Int32 AttributeMainID, string AttributeSub, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            DbParameter AttributeSubIDP = provider.CreateParameter("AttributeSubID", AttributeSubID, DbType.Int32);
            DbParameter AttributeMainIDP = provider.CreateParameter("AttributeMainID", AttributeMainID, DbType.Int32);
            DbParameter AttributeSubP = provider.CreateParameter("AttributeSub", AttributeSub, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[5] { AttributeSubIDP, AttributeMainIDP, AttributeSubP, IsActiveP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateCustomerAttributeSub", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BPresalesMasters", "ZDMS_InsertOrUpdateCustomerAttributeSub", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BPresalesMasters", " ZDMS_InsertOrUpdateCustomerAttributeSub", ex);
                throw ex;
            }
            return success;
        }
        public List<PEffortType> GetEffortType(int? EffortTypeID, string EffortType)
        {
            List<PEffortType> pEffortTypes = new List<PEffortType>();
            try
            {
                DbParameter EffortTypeIDP = provider.CreateParameter("EffortTypeID", EffortTypeID, DbType.Int32);
                DbParameter EffortTypeP = provider.CreateParameter("EffortType", EffortType, DbType.String);
                DbParameter[] Params = new DbParameter[2] { EffortTypeIDP, EffortTypeP };
                using (DataSet DataSet = provider.Select("GetEffortType", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            pEffortTypes.Add(new PEffortType()
                            {
                                EffortTypeID = Convert.ToInt32(dr["EffortTypeID"]),
                                EffortType = Convert.ToString(dr["EffortType"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BPresalesMasters", "GetEffortType", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BPresalesMasters", "GetEffortType", ex);
                throw ex;
            }
            return pEffortTypes;
        }
        public int InsertOrUpdateEffortType(long? EffortTypeID, string EffortType, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            DbParameter EffortTypeIDP = provider.CreateParameter("EffortTypeID", EffortTypeID, DbType.Int32);
            DbParameter EffortTypeP = provider.CreateParameter("EffortType", EffortType, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { EffortTypeIDP, EffortTypeP, IsActiveP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateEffortType", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BPresalesMasters", "ZDMS_InsertOrUpdateEffortType", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BPresalesMasters", " ZDMS_InsertOrUpdateEffortType", ex);
                throw ex;
            }
            return success;
        }
        public List<PExpenseType> GetExpenseType(int? ExpenseTypeID, string ExpenseType)
        {
            List<PExpenseType> pExpenseTypes = new List<PExpenseType>();
            try
            {
                DbParameter ExpenseTypeIDP = provider.CreateParameter("ExpenseTypeID", ExpenseTypeID, DbType.Int32);
                DbParameter ExpenseTypeP = provider.CreateParameter("ExpenseType", ExpenseType, DbType.String);
                DbParameter[] Params = new DbParameter[2] { ExpenseTypeIDP, ExpenseTypeP };
                using (DataSet DataSet = provider.Select("GetExpenseType", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            pExpenseTypes.Add(new PExpenseType()
                            {
                                ExpenseTypeID = Convert.ToInt32(dr["ExpenseTypeID"]),
                                ExpenseType = Convert.ToString(dr["ExpenseType"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BPresalesMasters", "GetExpenseType", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BPresalesMasters", "GetExpenseType", ex);
                throw ex;
            }
            return pExpenseTypes;
        }
        public int InsertOrUpdateExpenseType(long? ExpenseTypeID, string ExpenseType, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            DbParameter ExpenseTypeIDP = provider.CreateParameter("ExpenseTypeID", ExpenseTypeID, DbType.Int32);
            DbParameter ExpenseTypeP = provider.CreateParameter("ExpenseType", ExpenseType, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { ExpenseTypeIDP, ExpenseTypeP, IsActiveP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateExpenseType", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BPresalesMasters", "ZDMS_InsertOrUpdateExpenseType", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BPresalesMasters", " ZDMS_InsertOrUpdateExpenseType", ex);
                throw ex;
            }
            return success;
        }

        public int InsertOrUpdateMake(int? MakeID, string Make, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            DbParameter MakeIDP = provider.CreateParameter("MakeID", MakeID, DbType.Int32);
            DbParameter MakeP = provider.CreateParameter("Make", Make, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { MakeIDP, MakeP, IsActiveP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("InsertUpdateMake", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTestSN", "InsertUpdateMake", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTestSN", "InsertUpdateMake", ex);
                throw ex;
            }
            return success;
        }

        public int InsertOrUpdateProductType(int? ProductTypeID, string ProductType, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            DbParameter ProductTypeIDP = provider.CreateParameter("ProductTypeID", ProductTypeID, DbType.Int32);
            DbParameter ProductTypeP = provider.CreateParameter("ProductType", ProductType, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { ProductTypeIDP, ProductTypeP, IsActiveP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("InsertUpdateProductType", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTestSN", "InsertUpdateProductType", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTestSN", "InsertUpdateProductType", ex);
                throw ex;
            }
            return success;
        }

        public int InsertOrUpdateProduct(int? ProductID, string Product, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            DbParameter ProductIDP = provider.CreateParameter("ProductID", ProductID, DbType.Int32);
            DbParameter ProductP = provider.CreateParameter("Product", Product, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { ProductIDP, ProductP, IsActiveP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("InsertUpdateProduct", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTestSN", "InsertUpdateProduct", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTestSN", "InsertUpdateProduct", ex);
                throw ex;
            }
            return success;
        }
    }
}
