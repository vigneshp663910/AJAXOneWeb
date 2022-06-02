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
        public List<PDealerEmployee> GetOrganization( int? DealerEmployeeID, int? DealerID,int? DealerDepartmentID)
        {
            List<PDealerEmployee> Ws = new List<PDealerEmployee>();
            PDealerEmployee W = null;
            try
            { 
                DbParameter UserIDP = provider.CreateParameter("DealerEmployeeID", DealerEmployeeID, DbType.Int32);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DealerDepartmentIDP = provider.CreateParameter("DealerDepartmentID", DealerDepartmentID, DbType.Int32);

                DbParameter[] Params = new DbParameter[3] {  UserIDP , DealerIDP, DealerDepartmentIDP };
                using (DataSet DataSet = provider.Select("GetOrganizationLevel", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDealerEmployee();
                            Ws.Add(W); 
                            W.EmpId = Convert.ToInt32(dr["DealerEmployeeID"]);
                            W.EmployeeName = Convert.ToString(dr["Name"]) + " - "
                                //+ Convert.ToString(dr["DealerDepartment"]) + " " 
                                + Convert.ToString(dr["DealerDesignation"]); 
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
