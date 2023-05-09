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
    public class BProject
    {
        private IDataAccess provider;
        public BProject()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public Boolean InsertOrUpdateProject(PProject project)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter ProjectID = provider.CreateParameter("ProjectID", project.ProjectID, DbType.Int32);
                DbParameter ProjectNumber = provider.CreateParameter("ProjectNumber", project.ProjectNumber, DbType.String);
                DbParameter EmailDate = provider.CreateParameter("EmailDate", project.EmailDate, DbType.DateTime);
                DbParameter TenderNumber = provider.CreateParameter("TenderNumber", project.TenderNumber, DbType.String);
                DbParameter StateID = provider.CreateParameter("StateID", project.State.StateID, DbType.Int32);
                DbParameter DistrictID = provider.CreateParameter("DistrictID", project.District.DistrictID, DbType.Int32);
                DbParameter Value = provider.CreateParameter("Value", project.Value, DbType.Decimal);
                DbParameter L1ContractorName = provider.CreateParameter("L1ContractorName", project.L1ContractorName, DbType.String);
                DbParameter L1ContractorAddress = provider.CreateParameter("L1ContractorAddress", project.L1ContractorAddress, DbType.String);
                DbParameter L1ContractorAddress2 = provider.CreateParameter("L1ContractorAddress2", project.L1ContractorAddress2, DbType.String);
                DbParameter L2Bidder = provider.CreateParameter("L2Bidder", project.L2Bidder, DbType.String);
                DbParameter L3Bidder = provider.CreateParameter("L3Bidder", project.L3Bidder, DbType.String);
                DbParameter ContractAwardDate = provider.CreateParameter("ContractAwardDate", project.ContractAwardDate, DbType.DateTime);
                DbParameter ContractEndDate = provider.CreateParameter("ContractEndDate", project.ContractEndDate, DbType.DateTime);
                DbParameter Remarks = provider.CreateParameter("Remarks", project.Remarks, DbType.String);
                DbParameter ProjectName = provider.CreateParameter("ProjectName", project.ProjectName, DbType.String);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter[] Params = new DbParameter[16] { ProjectID, ProjectNumber, EmailDate, TenderNumber, StateID, DistrictID, Value, L1ContractorName, L1ContractorAddress, L1ContractorAddress2, L2Bidder, L3Bidder, ContractAwardDate, ContractEndDate, Remarks, ProjectName };
                    provider.Insert("ZDMS_InsertOrUpdateProject", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BProject", "ZDMS_InsertOrUpdateProject", ex);
                return false;
            }
            return true;
        }
        public List<PProject> GetProject(int? ProjectID, int? StateID, int? DistrictID, DateTime? DateFrom, DateTime? DateTo, string ProjectName, string ProjectNumber)
        {
            List<PProject> projects = new List<PProject>();
            try
            {
                DbParameter ProjectIDP = provider.CreateParameter("ProjectID", ProjectID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter DistrictIDP = provider.CreateParameter("DistrictID", DistrictID, DbType.Int32);
                DbParameter DateFromP = provider.CreateParameter("DateFrom", DateFrom, DbType.DateTime);
                DbParameter DateToP = provider.CreateParameter("DateTo", DateTo, DbType.DateTime);
                DbParameter ProjectNameP = provider.CreateParameter("ProjectName", ProjectName, DbType.String);
                DbParameter ProjectNumberP = provider.CreateParameter("ProjectNumber", ProjectNumber, DbType.String);
                DbParameter[] Params = new DbParameter[7] { ProjectIDP, StateIDP, DistrictIDP, DateFromP, DateToP, ProjectNameP, ProjectNumberP };

                using (DataSet DataSet = provider.Select("GetProject", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            projects.Add(new PProject()
                            {
                                ProjectID = Convert.ToInt64(dr["ProjectID"]),
                                ProjectName = Convert.ToString(dr["ProjectName"]),
                                ProjectNumber = Convert.ToString(dr["ProjectNumber"]),
                                EmailDate = Convert.ToDateTime(dr["EmailDate"]),
                                TenderNumber = Convert.ToString(dr["TenderNumber"]),
                                State = DBNull.Value == dr["StateID"] ? null : new PDMS_State()
                                {
                                    State = Convert.ToString(dr["State"]),
                                    StateID = Convert.ToInt32(dr["StateID"]),
                                },
                                District = DBNull.Value == dr["DistrictID"] ? null : new PDMS_District()
                                {
                                    District = Convert.ToString(dr["District"]),
                                    DistrictID = Convert.ToInt32(dr["DistrictID"]),
                                },
                                Value = DBNull.Value == dr["Value"] ? 0 : Convert.ToDecimal(dr["Value"]),
                                L1ContractorName = Convert.ToString(dr["L1ContractorName"]),
                                L1ContractorAddress = Convert.ToString(dr["L1ContractorAddress"]),
                                L1ContractorAddress2 = Convert.ToString(dr["L1ContractorAddress2"]),
                                L2Bidder = Convert.ToString(dr["L2Bidder"]),
                                L3Bidder = Convert.ToString(dr["L3Bidder"]),
                                ContractAwardDate = Convert.ToDateTime(dr["ContractAwardDate"]),
                                ContractEndDate = Convert.ToDateTime(dr["ContractEndDate"]),
                                Remarks = Convert.ToString(dr["Remarks"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return projects;
        }
        public Boolean InsertOrUpdateProject_ForExcelUpload(PProject project, string EmailDateP, string ContractAwardDateP, string ContractEndDateP)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter ProjectID = provider.CreateParameter("ProjectID", project.ProjectID, DbType.Int32);
                DbParameter EmailDate = provider.CreateParameter("EmailDate", string.IsNullOrEmpty(EmailDateP) ? (DateTime?)null : Convert.ToDateTime(EmailDateP), DbType.DateTime);
                DbParameter TenderNumber = provider.CreateParameter("TenderNumber", project.TenderNumber, DbType.String);
                DbParameter StateID = provider.CreateParameter("StateID", (project.State == null) ? (int?)null : project.State.StateID, DbType.Int32);
                DbParameter DistrictID = provider.CreateParameter("DistrictID", project.District == null ? (int?)null : project.District.DistrictID, DbType.Int32);
                DbParameter Value = provider.CreateParameter("Value", project.Value, DbType.Decimal);
                DbParameter L1ContractorName = provider.CreateParameter("L1ContractorName", project.L1ContractorName, DbType.String);
                DbParameter L1ContractorAddress = provider.CreateParameter("L1ContractorAddress", project.L1ContractorAddress, DbType.String);
                DbParameter L2Bidder = provider.CreateParameter("L2Bidder", project.L2Bidder, DbType.String);
                DbParameter L3Bidder = provider.CreateParameter("L3Bidder", project.L3Bidder, DbType.String);
                DbParameter ContractAwardDate = provider.CreateParameter("ContractAwardDate", string.IsNullOrEmpty(ContractAwardDateP) ? (DateTime?)null : Convert.ToDateTime(ContractAwardDateP), DbType.DateTime);
                DbParameter ContractEndDate = provider.CreateParameter("ContractEndDate", string.IsNullOrEmpty(ContractEndDateP) ? (DateTime?)null : Convert.ToDateTime(ContractEndDateP), DbType.DateTime);
                DbParameter Remarks = provider.CreateParameter("Remarks", project.Remarks, DbType.String);
                DbParameter ProjectName = provider.CreateParameter("ProjectName", project.ProjectName, DbType.String);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter[] Params = new DbParameter[14] { ProjectID, EmailDate, TenderNumber, StateID, DistrictID, Value, L1ContractorName, L1ContractorAddress, L2Bidder, L3Bidder, ContractAwardDate, ContractEndDate, Remarks, ProjectName };
                    provider.Insert("ZDMS_InsertOrUpdateProject_ForExcelUpload", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BProject", "ZDMS_InsertOrUpdateProject_ForExcelUpload", ex);
                return false;
            }
            return true;
        }
        public List<PProject> GetProjectAutocomplete(string Project)
        {
            List<PProject> projects = new List<PProject>();
            try
            {
                
                DbParameter ProjectP = provider.CreateParameter("Project", Project, DbType.String); 
                DbParameter[] Params = new DbParameter[1] { ProjectP };

                using (DataSet DataSet = provider.Select("GetProjectAutocomplete", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            projects.Add(new PProject()
                            {
                                ProjectID = Convert.ToInt64(dr["ProjectID"]),
                                ProjectName = Convert.ToString(dr["ProjectName"]),
                                ProjectNumber = Convert.ToString(dr["ProjectNumber"]),
                                EmailDate = Convert.ToDateTime(dr["EmailDate"]),
                                TenderNumber = Convert.ToString(dr["TenderNumber"]),
                                State = new PDMS_State() { State = Convert.ToString(dr["State"]) },
                                District = new PDMS_District() { District = Convert.ToString(dr["District"])},
                                Value = DBNull.Value == dr["Value"] ? 0 : Convert.ToDecimal(dr["Value"]),
                                L1ContractorName = Convert.ToString(dr["L1ContractorName"]),
                                L2Bidder = Convert.ToString(dr["L2Bidder"]),
                                L3Bidder = Convert.ToString(dr["L3Bidder"]),
                                ContractAwardDate = Convert.ToDateTime(dr["ContractAwardDate"]),
                                ContractEndDate = Convert.ToDateTime(dr["ContractEndDate"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return projects;
        }

    }
}
