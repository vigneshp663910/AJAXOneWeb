using DataAccess;
using Newtonsoft.Json;
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
    public class BDMS_Dashboard
    {
        private IDataAccess provider;
        public BDMS_Dashboard()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PDMS_Dashboard> GetDashboardAll(int? DashboardID)
        {
            List<PDMS_Dashboard> Dashboard = new List<PDMS_Dashboard>();       
            DbParameter DIDP  = provider.CreateParameter("DashboardID", DashboardID, DbType.Int32); 
            DbParameter[] EmployeeParams = new DbParameter[1] { DIDP  };
            try
            {
                using (DataSet ds = provider.Select("ZDMS_GetDashboardAll", EmployeeParams))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Dashboard.Add(new PDMS_Dashboard()
                            {
                                DashboardID = Convert.ToInt32(dr["DashboardID"]),
                                DashboardName = Convert.ToString(dr["DashboardName"])
                            }); 
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Dashboard;
        }
        public List<PDMS_Dashboard> GetDashboardByUserID(int UserID)
        {
            List<PDMS_Dashboard> Dashboard = new List<PDMS_Dashboard>();
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[1] { UserIDP };
            try
            {
                using (DataSet ds = provider.Select("ZDMS_GetDashboardByUserID", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Dashboard.Add(new PDMS_Dashboard()
                            {
                                DashboardID = Convert.ToInt32(dr["DashboardID"]),
                                DashboardName = Convert.ToString(dr["DashboardName"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Dashboard;
        }
        public List<PDMS_Dashboard> GetDashboardByDealerDesignationID(int DealerDesignationID)
        {
            List<PDMS_Dashboard> Dashboard = new List<PDMS_Dashboard>();
            DbParameter DealerDesignationIDP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int32);
            DbParameter[] Params = new DbParameter[1] { DealerDesignationIDP };
            try
            {
                using (DataSet ds = provider.Select("GetDashboardByDealerDesignationID", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Dashboard.Add(new PDMS_Dashboard()
                            {
                                DashboardID = Convert.ToInt32(dr["DashboardID"]),
                                DashboardName = Convert.ToString(dr["DashboardName"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Dashboard;
        }

        public List<PDMS_ICTicket> GetDashboardTransactionStatistics(int? DealerID, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int UserID)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter DealerCodeP  = provider.CreateParameter("DealerID", DealerID, DbType.Int32);             

                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerCodeP, ICTicketDateFP, ICTicketDateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetDashboardTransactionStatistics", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["count"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                           
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }

        
    }
}
