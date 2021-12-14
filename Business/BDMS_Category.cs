using DataAccess;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Business
{
    public class BDMS_Category
    {
        private IDataAccess provider;
        public BDMS_Category()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public int IntegrationCategory()
        {
            TraceLogger.Log(DateTime.Now);

            string Type = "";
            string CategoryCode = "";
            string Category = "";
            string Category1Code = "";
            string Category2Code = "";
            string Category3Code = "";
            string Category4Code = ""; 
            DataTable dt = new SDMS_Category().getCategoryFromSAP();
           
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Type = null;
                    CategoryCode = null;
                    Category = null;
                    Category1Code = null;
                    Category2Code = null;
                    Category3Code = null;
                    Category4Code = null;
                   
                    Type = Convert.ToString(dr["TYPE"]);
                    Category = Convert.ToString(dr["DESCRIPTION"]);
                    CategoryCode = Convert.ToString(dr["CAT_ID"]); 
                    
                    if (Type == "C2")
                    {
                        Category1Code = Convert.ToString(dr["CATEGORY_1"]);
                    }
                    else if (Type == "C3")
                    {
                        Category1Code = Convert.ToString(dr["CATEGORY_1"]);
                        Category2Code = Convert.ToString(dr["CATEGORY_2"]); 
                    }
                    else if (Type == "C4")
                    {
                        Category1Code = Convert.ToString(dr["CATEGORY_1"]);
                        Category2Code = Convert.ToString(dr["CATEGORY_2"]);
                        Category3Code = Convert.ToString(dr["CATEGORY_3"]); 
                    }
                    else if (Type == "C5")
                    {
                        Category1Code = Convert.ToString(dr["CATEGORY_1"]);
                        Category2Code = Convert.ToString(dr["CATEGORY_2"]);
                        Category3Code = Convert.ToString(dr["CATEGORY_3"]);
                        Category4Code = Convert.ToString(dr["CATEGORY_4"]);
                    }
                    

                    DbParameter TypeP = provider.CreateParameter("Type", Type, DbType.String);
                    DbParameter CategoryP = provider.CreateParameter("Category", Category, DbType.String);
                    DbParameter CategoryCodeP = provider.CreateParameter("CategoryCode", CategoryCode, DbType.String);
                    DbParameter Category1CodeP = provider.CreateParameter("Category1Code", Category1Code, DbType.String);
                    DbParameter Category2CodeP = provider.CreateParameter("Category2Code", Category2Code, DbType.String);
                    DbParameter Category3CodeP = provider.CreateParameter("Category3Code", Category3Code, DbType.String);
                    DbParameter Category4CodeP = provider.CreateParameter("Category4Code", Category4Code, DbType.String);

                    DbParameter[] Params = new DbParameter[7] { TypeP, CategoryP, CategoryCodeP,Category1CodeP, Category2CodeP, Category3CodeP,  Category4CodeP };
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            provider.Insert("ZDMS_InsertOrUpdateCategory", Params);
                            scope.Complete();
                        }
                    }

                    catch (SqlException sqlEx)
                    {
                        new FileLogger().LogMessageService("BDMS_Equipment", "InsertOrUpdateEquipment", sqlEx);

                        throw;
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessageService("BDMS_Equipment", " InsertOrUpdateEquipment", ex);
                        throw;
                    }
                }
            }
            return 0;
        }
    }
}
