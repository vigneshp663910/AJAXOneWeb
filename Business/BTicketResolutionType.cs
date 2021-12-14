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
    public class BTicketResolutionType
    {
         private IDataAccess provider;
         public BTicketResolutionType()
        {
            provider = new ProviderFactory().GetProvider();
        }
         public List<PResolutionType> getTicketResolutionType(int? TicketResolutionTypeID, string TicketResolutionType)
        {
            List<PResolutionType> TicketResolutionList = new List<PResolutionType>();
            PResolutionType pTicketResolutionType;

            DbParameter TicketResolutionTypeIDParam;
            DbParameter TicketResolutionTypeParam;

            if (TicketResolutionTypeID != null)
                TicketResolutionTypeIDParam = provider.CreateParameter("ResolutionTypeID", TicketResolutionTypeID, DbType.Int32);
            else
                TicketResolutionTypeIDParam = provider.CreateParameter("ResolutionTypeID", DBNull.Value, DbType.Int32);

            if (!string.IsNullOrEmpty(TicketResolutionType))
                TicketResolutionTypeParam = provider.CreateParameter("ResolutionType", TicketResolutionType, DbType.String);
            else
                TicketResolutionTypeParam = provider.CreateParameter("ResolutionType", DBNull.Value, DbType.String);



            DbParameter[] TicketResolutionTypeParams = new DbParameter[2] { TicketResolutionTypeIDParam, TicketResolutionTypeParam };

            try
            {
                using (DataSet TicketTypeDataSet = provider.Select("GetResolutionType", TicketResolutionTypeParams))
                {
                    if (TicketTypeDataSet != null)
                    {
                        foreach (DataRow TicketTypeRow in TicketTypeDataSet.Tables[0].Rows)
                        {
                            pTicketResolutionType = new PResolutionType
                            {
                                ResolutionTypeID = Convert.ToInt32(TicketTypeRow["ResolutionTypeID"]),
                                ResolutionType = Convert.ToString(TicketTypeRow["ResolutionType"])
                            };
                            TicketResolutionList.Add(pTicketResolutionType);
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
            return TicketResolutionList;
        }
    }
}
