using DataAccess;
using Properties;
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
    public class BTicketCategory
    {
       private IDataAccess provider;
       public BTicketCategory()
        {
            provider = new ProviderFactory().GetProvider();
        }
       public List<PCategory> getTicketCategory(int? TicketCategoryID, string TicketCategory, int? UserTypeID)
        {
            List<PCategory> TicketCategoryList = new List<PCategory>();
            PCategory pTicketCategory;

            DbParameter TicketTypeIDParam;
            DbParameter TicketTypeParam;
            DbParameter UserTypeIDP;

            if (TicketCategoryID != null)
                TicketTypeIDParam = provider.CreateParameter("CategoryID", TicketCategoryID, DbType.Int32);
            else
                TicketTypeIDParam = provider.CreateParameter("CategoryID", DBNull.Value, DbType.Int32);

            if (!string.IsNullOrEmpty(TicketCategory))
                TicketTypeParam = provider.CreateParameter("Category", TicketCategory, DbType.String);
            else
                TicketTypeParam = provider.CreateParameter("Category", DBNull.Value, DbType.String);

            if (UserTypeID != null)
                UserTypeIDP = provider.CreateParameter("UserTypeID", UserTypeID, DbType.String);
            else
                UserTypeIDP = provider.CreateParameter("UserTypeID", DBNull.Value, DbType.String);


            DbParameter[] TicketTypeParams = new DbParameter[3] { TicketTypeIDParam, TicketTypeParam, UserTypeIDP };

            try
            {
                using (DataSet TicketTypeDataSet = provider.Select("GetCategory", TicketTypeParams))
                {
                    if (TicketTypeDataSet != null)
                    {
                        foreach (DataRow TicketTypeRow in TicketTypeDataSet.Tables[0].Rows)
                        {
                            pTicketCategory = new PCategory
                            {
                                CategoryID = Convert.ToInt32(TicketTypeRow["CategoryID"]),
                                Category = Convert.ToString(TicketTypeRow["Category"]),
                                EmpId = Convert.ToInt32(TicketTypeRow["EmpId"])
                            };
                            TicketCategoryList.Add(pTicketCategory);
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
            return TicketCategoryList;
        }
    }
}
