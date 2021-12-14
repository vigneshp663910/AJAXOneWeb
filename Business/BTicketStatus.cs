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
    public class BTicketStatus
    {
         private IDataAccess provider;
         public BTicketStatus()
        {
            provider = new ProviderFactory().GetProvider();
        }
         public List<PStatus> getTicketStatus(int? TicketStatusID, string TicketStatus)
        {
            List<PStatus> TicketStatusList = new List<PStatus>();
            PStatus pTicketStatus;

            DbParameter TicketStatusIDParam;
            DbParameter TicketStatusParam;

            if (TicketStatusID != null)
                TicketStatusIDParam = provider.CreateParameter("StatusID", TicketStatusID, DbType.Int32);
            else
                TicketStatusIDParam = provider.CreateParameter("StatusID", DBNull.Value, DbType.Int32);

            if (!string.IsNullOrEmpty(TicketStatus))
                TicketStatusParam = provider.CreateParameter("Status", TicketStatus, DbType.String);
            else
                TicketStatusParam = provider.CreateParameter("Status", DBNull.Value, DbType.String);



            DbParameter[] TicketResolutionTypeParams = new DbParameter[2] { TicketStatusIDParam, TicketStatusParam };

            try
            {
                using (DataSet TicketTypeDataSet = provider.Select("GetStatus", TicketResolutionTypeParams))
                {
                    if (TicketTypeDataSet != null)
                    {
                        foreach (DataRow TicketTypeRow in TicketTypeDataSet.Tables[0].Rows)
                        {

                            pTicketStatus = new PStatus
                            {

                                StatusID = Convert.ToInt32(TicketTypeRow["StatusID"]),
                                Status = Convert.ToString(TicketTypeRow["Status"])
                               
                            };
                            TicketStatusList.Add(pTicketStatus);
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
            return TicketStatusList;
        }
    }
}
