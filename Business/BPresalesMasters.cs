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
    public class BPresalesMasters
    {
        private IDataAccess provider;
        public BPresalesMasters()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PLeadSource> GetLeadSource(int? SourceID, string Source)
        {
            List<PLeadSource> pLeadSources = new List<PLeadSource>();
            try
            {
                DbParameter SourceIDP = provider.CreateParameter("SourceID", SourceID, DbType.Int32);
                DbParameter SourceP = provider.CreateParameter("Source", Source, DbType.String);
                DbParameter[] Params = new DbParameter[2] { SourceIDP, SourceP };
                using (DataSet DataSet = provider.Select("GetLeadSource", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            pLeadSources.Add(new PLeadSource()
                            {
                                SourceID = Convert.ToInt32(dr["LeadSourceID"]),
                                Source = Convert.ToString(dr["LeadSource"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return pLeadSources;
        }
    }
}
