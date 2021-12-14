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
    public class BTicketSubCategory
    {
        private IDataAccess provider;
        public BTicketSubCategory()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PSubCategory> getTicketSubCategory(int? TicketSubCategoryID, string TicketSubCategory, int? TicketCategoryId)
        {
            List<PSubCategory> TicketSubCategoryList = new List<PSubCategory>();
            PSubCategory pTicketSubCategory;

            DbParameter TicketSubCategoryIDParam;
            DbParameter TicketSubCategoryParam;
            DbParameter TicketCategoryIdParam;

            if (TicketSubCategoryID != null)
                TicketSubCategoryIDParam = provider.CreateParameter("SubCategoryID", TicketSubCategoryID, DbType.Int32);
            else
                TicketSubCategoryIDParam = provider.CreateParameter("SubCategoryID", DBNull.Value, DbType.Int32);

            if (!string.IsNullOrEmpty(TicketSubCategory))
                TicketSubCategoryParam = provider.CreateParameter("SubCategory", TicketSubCategory, DbType.String);
            else
                TicketSubCategoryParam = provider.CreateParameter("SubCategory", DBNull.Value, DbType.String);

            if (TicketCategoryId != null)
                TicketCategoryIdParam = provider.CreateParameter("CategoryId", TicketCategoryId, DbType.Int32);
            else
                TicketCategoryIdParam = provider.CreateParameter("CategoryId", DBNull.Value, DbType.Int32);


            DbParameter[] TicketResolutionTypeParams = new DbParameter[3] { TicketSubCategoryIDParam, TicketSubCategoryParam, TicketCategoryIdParam };
            try
            {
                using (DataSet TicketTypeDataSet = provider.Select("GetSubCategory", TicketResolutionTypeParams))
                {
                    if (TicketTypeDataSet != null)
                    {
                        foreach (DataRow TicketTypeRow in TicketTypeDataSet.Tables[0].Rows)
                        {

                            pTicketSubCategory = new PSubCategory
                            {
                                SubCategoryID = Convert.ToInt32(TicketTypeRow["SubCategoryID"]),
                                CategoryId = Convert.ToInt32(TicketTypeRow["CategoryId"]),
                                SubCategory = Convert.ToString(TicketTypeRow["SubCategory"]),
                                SeverityID = Convert.ToInt32(TicketTypeRow["SeverityID"])
                            };
                            TicketSubCategoryList.Add(pTicketSubCategory);
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return TicketSubCategoryList;
        }
    }
}
