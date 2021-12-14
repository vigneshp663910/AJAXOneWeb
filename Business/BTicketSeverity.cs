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
    public  class BTicketSeverity
    {
        private IDataAccess provider;
        public BTicketSeverity()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PSeverity> getTicketSeverity(int? TicketSeverityID, string TicketSeverity)
        {
            List<PSeverity> BTicketSeverityList = new List<PSeverity>();
            PSeverity pTicketSeverity;

            DbParameter TicketSeverityIDParam;
            DbParameter TicketSeverityParam;

            if (TicketSeverityID != null)
                TicketSeverityIDParam = provider.CreateParameter("SeverityID", TicketSeverityID, DbType.Int32);
            else
                TicketSeverityIDParam = provider.CreateParameter("SeverityID", DBNull.Value, DbType.Int32);

            if (!string.IsNullOrEmpty(TicketSeverity))
                TicketSeverityParam = provider.CreateParameter("Severity", TicketSeverity, DbType.String);
            else
                TicketSeverityParam = provider.CreateParameter("Severity", DBNull.Value, DbType.String);



            DbParameter[] TicketResolutionTypeParams = new DbParameter[2] { TicketSeverityIDParam, TicketSeverityParam };

            try
            {
                using (DataSet TicketTypeDataSet = provider.Select("GetSeverity", TicketResolutionTypeParams))
                {
                    if (TicketTypeDataSet != null)
                    {
                        foreach (DataRow TicketTypeRow in TicketTypeDataSet.Tables[0].Rows)
                        {

                            pTicketSeverity = new PSeverity
                            {
                                SeverityID = Convert.ToInt32(TicketTypeRow["SeverityID"]),
                                Severity = Convert.ToString(TicketTypeRow["Severity"])
                               
                            };
                            BTicketSeverityList.Add(pTicketSeverity);
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            return BTicketSeverityList;
        }
    }
}
