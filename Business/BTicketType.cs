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
    public class BTicketType
    {
        private IDataAccess provider;
        public BTicketType()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PType> getTicketType(int? TicketTypeID, string TicketType)
        {
            List<PType> TicketTypeList=new List<PType>();
            PType pTicketType;

            DbParameter TicketTypeIDParam;
            DbParameter TicketTypeParam;
           
            if (TicketTypeID != null)
                TicketTypeIDParam = provider.CreateParameter("TypeID", TicketTypeID, DbType.Int32);
            else
                TicketTypeIDParam = provider.CreateParameter("TypeID", DBNull.Value, DbType.Int32);

            if (!string.IsNullOrEmpty(TicketType))
                TicketTypeParam = provider.CreateParameter("Type", TicketType, DbType.String);
            else
                TicketTypeParam = provider.CreateParameter("Type", DBNull.Value, DbType.String);



            DbParameter[] TicketTypeParams = new DbParameter[2] { TicketTypeIDParam, TicketTypeParam };

            try
            {
                using (DataSet TicketTypeDataSet = provider.Select("GetType", TicketTypeParams))
                {
                    if (TicketTypeDataSet != null)
                    {
                        foreach (DataRow TicketTypeRow in TicketTypeDataSet.Tables[0].Rows)
                        {
                            pTicketType = new PType {
                                TypeID = Convert.ToInt32(TicketTypeRow["TypeID"]),
                                Type = Convert.ToString(TicketTypeRow["Type"])
                            };
                            TicketTypeList.Add(pTicketType);
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
            return TicketTypeList;
        }
    }
}
