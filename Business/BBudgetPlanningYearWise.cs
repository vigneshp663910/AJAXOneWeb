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
    }
}