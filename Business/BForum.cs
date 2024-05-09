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
    public class BForum
    {
        private IDataAccess provider;
        public BForum()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public int GetMessageViewStatusCound(long HeaderID, int? UserID)
        {
            DbParameter HeaderIDP = provider.CreateParameter("HeaderID", HeaderID, DbType.Int64);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            DbParameter[] Params = new DbParameter[2] { HeaderIDP, UserIDP };
            try
            {
                using (DataSet DS = provider.Select("GetMessageViewStatusCound", Params))
                {
                    if (DS != null)
                    { 
                        return Convert.ToInt32(DS.Tables[0].Rows[0][0]); 
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return 0;
        }

        public void UdateMessageViewStatus(long HeaderID, int UserID, long LastMessageID)
        {
            DbParameter HeaderIDP = provider.CreateParameter("HeaderID", HeaderID, DbType.Int64);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter LastMessageIDP = provider.CreateParameter("LastMessageID", LastMessageID, DbType.Int64);
            DbParameter[] Params = new DbParameter[3] { HeaderIDP, UserIDP, LastMessageIDP };
            try
            {
                using (DataSet DS = provider.Select("UdateMessageViewStatus", Params))
                {
                    
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { } 
        }
    }
}
