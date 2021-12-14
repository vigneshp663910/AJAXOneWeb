using DataAccess;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Business
{
    public class BDMS_DiscountType
    {
        private IDataAccess provider;
        public BDMS_DiscountType()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PDMS_DiscountType> GetDiscountType(int? DiscountTypeID, string DiscountType)
        {
            List<PDMS_DiscountType> DiscountTypes = new List<PDMS_DiscountType>();
            DbParameter DiscountTypeIDP = provider.CreateParameter("DiscountTypeID", DiscountTypeID, DbType.Int32);
            DbParameter DiscountTypeP = provider.CreateParameter("DiscountType", string.IsNullOrEmpty(DiscountType) ? null : DiscountType, DbType.String);
            DbParameter[] Params = new DbParameter[2] { DiscountTypeIDP, DiscountTypeP };
            try
            {
                using (DataSet ds = provider.Select("ZDMS_GetDiscountType", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DiscountTypes.Add(new PDMS_DiscountType()
                            {
                                DiscountTypeID = Convert.ToInt32(dr["DiscountTypeID"]),
                                DiscountType = Convert.ToString(dr["DiscountType"]),
                                DiscountTypeCode = Convert.ToString(dr["DiscountTypeCode"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
            }
            catch (Exception ex)
            { }
            return DiscountTypes;
        }
        public void GetDiscountTypeDDL(DropDownList ddl, int? DiscountTypeID, string DiscountType)
        {
            try
            { 
                ddl.DataValueField = "DiscountTypeID";
                ddl.DataTextField = "DiscountType";
                ddl.DataSource = GetDiscountType(DiscountTypeID, DiscountType); 
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
        }
    }
}
