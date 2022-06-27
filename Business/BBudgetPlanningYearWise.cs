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
    public class BBudgetPlanningYearWise
    {
        private IDataAccess provider;
        public BBudgetPlanningYearWise()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PBudgetPlanningYearWise> GetBudgetPlanningYearWise(int? DealerID, int? ModelID, int? Year)
        {
            List<PBudgetPlanningYearWise> Budget = new List<PBudgetPlanningYearWise>();
           

            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32); 
            DbParameter ModelIDP = provider.CreateParameter("ModelID", ModelID, DbType.Int32);
            DbParameter YearP = provider.CreateParameter("Year", Year, DbType.Int32);

            DbParameter[] Params = new DbParameter[3] { DealerIDP, ModelIDP, YearP };
            try
            {
                using (DataSet ds = provider.Select("GetBudgetPlanningYearWise", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Budget.Add(new PBudgetPlanningYearWise()
                            {
                                Dealer = new PDMS_Dealer()
                                {
                                    DealerID = Convert.ToInt32(dr["DealerID"]),
                                    DealerCode = Convert.ToString(dr["DealerCode"]),
                                    DealerName = Convert.ToString(dr["DealerName"]),
                                },
                                Model = new PDMS_Model()
                                {
                                    ModelID = Convert.ToInt32(dr["ModelID"]),
                                    Model = Convert.ToString(dr["Model"]),
                                },
                                Year = Convert.ToInt32(dr["Year"]),
                                Budget = dr["Budget"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Budget"]),
                                Actual = dr["Actual"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Actual"]),
                                Freezed = dr["Freezed"] == DBNull.Value ? false : Convert.ToBoolean(dr["Freezed"]),
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Budget;
        }

        public Boolean insertOrUpdateBudgetPlanningYearWise(List<PBudgetPlanningYearWise> Budgets, int UserId)
        {
           
           
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (PBudgetPlanningYearWise B in Budgets)
                    {
                        DbParameter DealerID = provider.CreateParameter("DealerID", B.Dealer.DealerID, DbType.Int32);
                        DbParameter ModelID = provider.CreateParameter("ModelID", B.Model.ModelID, DbType.Int32);
                        DbParameter Year = provider.CreateParameter("Year", B.Year, DbType.Int32);
                        DbParameter Budget = provider.CreateParameter("Budget", B.Budget, DbType.Int32); 
                        DbParameter UserIdP = provider.CreateParameter("UserId", UserId, DbType.Int32);
                        DbParameter[] Params = new DbParameter[5] { DealerID, ModelID, Year, Budget, UserIdP }; 
                        provider.Insert("InsertOrUpdateBudgetPlanningYearWise", Params);
                    }
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            { return false; }
            catch (Exception ex)
            { return false; }
            return true;
        }
        public List<PBudgetPlanningMonthWise> GetBudgetPlanningMonthWise(int? DealerID, int? ModelID, int? Year,int Month)
        {
            List<PBudgetPlanningMonthWise> Budget = new List<PBudgetPlanningMonthWise>();


            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter ModelIDP = provider.CreateParameter("ModelID", ModelID, DbType.Int32);
            DbParameter YearP = provider.CreateParameter("Year", Year, DbType.Int32);
            DbParameter MonthP = provider.CreateParameter("Month", Month, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { DealerIDP, ModelIDP, YearP, MonthP };
            try
            {
                using (DataSet ds = provider.Select("GetBudgetPlanningMonthWise", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            Budget.Add(new PBudgetPlanningMonthWise()
                            {
                                BudgetPlanningYear = new PBudgetPlanningYearWise()
                                {
                                    BudgetPYWiseID = Convert.ToInt64(dr["BudgetPYWiseID"]),
                                    Dealer = new PDMS_Dealer()
                                    {
                                        DealerID = Convert.ToInt32(dr["DealerID"]),
                                        DealerCode = Convert.ToString(dr["DealerCode"]),
                                        DealerName = Convert.ToString(dr["DealerName"]),
                                    },
                                    Model = new PDMS_Model()
                                    {
                                        ModelID = Convert.ToInt32(dr["ModelID"]),
                                        Model = Convert.ToString(dr["Model"]),
                                    },
                                    Year = Convert.ToInt32(dr["Year"]),
                                    Budget = dr["Budget"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Budget"]),
                                    Actual = dr["Actual"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Actual"]),
                                    Freezed = dr["Freezed"] == DBNull.Value ? false : Convert.ToBoolean(dr["Freezed"]),
                                },
                                Planed = dr["Planed"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Planed"]),
                                SystemPlaned = dr["SystemPlaned"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SystemPlaned"]),
                                Actual = dr["Actual"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Actual"]),
                                Month = Convert.ToInt32(dr["Month"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Budget;
        }

        public Boolean insertOrUpdateBudgetPlanningMonthWise(List<PBudgetPlanningMonthWise> Budgets, int UserId)
        { 
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (PBudgetPlanningMonthWise B in Budgets)
                    {
                        DbParameter DealerID = provider.CreateParameter("BudgetPYWiseID", B.BudgetPlanningYear.BudgetPYWiseID, DbType.Int32); 
                        DbParameter Month = provider.CreateParameter("Month", B.Month, DbType.Int32);
                        DbParameter Planed = provider.CreateParameter("Planed", B.Planed, DbType.Int32);
                        DbParameter UserIdP = provider.CreateParameter("UserId", UserId, DbType.Int32);
                        DbParameter[] Params = new DbParameter[4] { DealerID, Month, Planed, UserIdP };
                        provider.Insert("InsertOrUpdateBudgetPlanningMonthWise", Params);
                    }
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            { return false; }
            catch (Exception ex)
            { return false; }
            return true;
        }
    }
}