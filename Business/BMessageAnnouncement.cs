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

namespace Business
{
    public class BMessageAnnouncement
    {
        private IDataAccess provider;
        private IDataAccess providerReport;
        public BMessageAnnouncement()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PMessageAnnouncement> GetMessageAnnouncement(long? MessageAnnouncementId, int? DealerID, int? DealerDepartmentID, int? DealerDesignationID, int? DealerEmployeeID)
        {
            List<PMessageAnnouncement> PMAs = new List<PMessageAnnouncement>();
            PMessageAnnouncement PMA = null;
            DateTime traceStartTime = DateTime.Now;
            DataTable usersDataTable = new DataTable();
            try
            {
                DbParameter MessageAnnouncementIdP = provider.CreateParameter("MessageAnnouncementId", MessageAnnouncementId, DbType.Int64);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DealerDepartmentIDP = provider.CreateParameter("DealerDepartmentID", DealerDepartmentID, DbType.Int32);
                DbParameter DealerDesignationIDP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int32);
                DbParameter DealerEmployeeIDP = provider.CreateParameter("DealerEmployeeID", DealerEmployeeID, DbType.Int32);

                DbParameter[] userParams = new DbParameter[5] { MessageAnnouncementIdP, DealerIDP, DealerDepartmentIDP, DealerDesignationIDP, DealerEmployeeIDP };

                using (DataSet usersDataSet = provider.Select("GetMessageAnnouncement", userParams))
                {
                    if (usersDataSet != null)
                        foreach (DataRow dr in usersDataSet.Tables[0].Rows)
                        {
                            PMA = new PMessageAnnouncement();
                            PMA.MessageAnnouncementId = Convert.ToInt64(dr["MessageAnnouncementId"]);
                            PMA.AssignTo = new PUser() { UserID = Convert.ToInt32(dr["AssignTo"]), ContactName = Convert.ToString(dr["AssignToName"]) };
                            PMA.Message = Convert.ToString(dr["Message"]);
                            PMA.CreatedBy = new PUser() { UserID = Convert.ToInt32(dr["CreatedBy"]), ContactName = Convert.ToString(dr["CreatedByName"]) };
                            PMA.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                        }
                }
                TraceLogger.Log(traceStartTime);
                return PMAs;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
    }
}
