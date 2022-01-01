using DataAccess;
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
    public class BTest
    {
        private IDataAccess provider;
        public BTest()
        {
            try
            {
                provider = new ProviderFactory().GetProvider();
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_ICTicket", "provider : " + e1.Message, null);
            }
        }
        public List<object> getSalesByYearAndMonth()
        {
            List<object> chartData = new List<object>();
            string ICT = "";
            try
            {
                using (DataSet DataSet = provider.Select("DB_SalesByYearAndMonth"))
                {
                    if (DataSet != null)
                    {
                        int cCount = DataSet.Tables[0].Columns.Count;
                        int rCount = DataSet.Tables[0].Rows.Count;

                       
                        for (int i = 0; i < rCount; i++)
                        {
                            object[] obj = new object[cCount];
                            if(chartData.Count==0)
                            {
                                for (int j = 0; j < cCount; j++)
                                {
                                    obj[j] = Convert.ToString(DataSet.Tables[0].Columns[j].ColumnName);
                                }
                                chartData.Add(obj);
                                obj = new object[cCount];
                            }
                            for (int j = 0; j < cCount; j++)
                            {
                                obj[j] = Convert.ToString( DataSet.Tables[0].Rows[i][j]);
                            } 
                            chartData.Add(obj);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return chartData;
        }
    }
}
