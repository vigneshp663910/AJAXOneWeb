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
    public class BOrganization
    {
        private IDataAccess provider;
        private IDataAccess providerReport;
        public BOrganization()
        {
            provider = new ProviderFactory().GetProvider();
            providerReport = new ProviderFactory().GetProvider(true);
        }
        public List<PDealerEmployee> GetOrganization( int UserID)
        {
            List<PDealerEmployee> Ws = new List<PDealerEmployee>();
            PDealerEmployee W = null;
            try
            { 
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

                DbParameter[] Params = new DbParameter[1] {  UserIDP };
                using (DataSet DataSet = provider.Select("GetOrganizationLevel", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDealerEmployee();
                            Ws.Add(W); 
                            W.EmpId = Convert.ToInt32(dr["UserID"]);
                            W.EmployeeName = Convert.ToString(dr["Name"]);
                             
                           
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
