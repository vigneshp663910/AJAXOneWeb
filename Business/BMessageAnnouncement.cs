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
        public List<PMessageAnnouncement> GetAllUsers(string contactName = "", string userName = "")
        {
            List<PMessageAnnouncement> users = new List<PMessageAnnouncement>();
            DateTime traceStartTime = DateTime.Now;
            DataTable usersDataTable = new DataTable();
            try
            {
                DbParameter ContactNameParams, UserNameParams;
                ContactNameParams = provider.CreateParameter("ContactName", contactName, DbType.String);
                UserNameParams = provider.CreateParameter("UserName", userName, DbType.String);
                DbParameter[] userParams = new DbParameter[2] { ContactNameParams, UserNameParams };

                using (DataSet usersDataSet = provider.Select("GetAllUsers", userParams))
                {
                    if (usersDataSet != null)
                        foreach (DataRow usersRow in usersDataSet.Tables[0].Rows)
                        {

                        }
                }
                TraceLogger.Log(traceStartTime);
                return users;
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
