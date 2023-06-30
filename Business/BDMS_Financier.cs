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
    public class BDMS_Financier
    {
        private IDataAccess provider;
        public BDMS_Financier()
        {
            provider = new ProviderFactory().GetProvider();
        }

        public List<PDMS_Financier> GetFinancier(int? FinancierID, string FinancierCode)
        {
            List<PDMS_Financier> DiscountTypes = new List<PDMS_Financier>();
            DbParameter FinancierIDP = provider.CreateParameter("FinancierID", FinancierID, DbType.Int32);
            DbParameter FinancierCodeP = provider.CreateParameter("FinancierCode", string.IsNullOrEmpty(FinancierCode) ? null : FinancierCode, DbType.String);
            DbParameter[] Params = new DbParameter[2] { FinancierIDP, FinancierCodeP };
            try
            {
                using (DataSet ds = provider.Select("ZDMS_GetFinancier", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DiscountTypes.Add(new PDMS_Financier()
                            {
                                FinancierID = Convert.ToInt32(dr["FinancierID"]),
                                FinancierCode = Convert.ToString(dr["FinancierCode"]),
                                FinancierName = Convert.ToString(dr["FinancierName"])
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
        public void GetFinancierDDL(DropDownList ddl, int? FinancierID, string FinancierCode)
        {
            try
            {
                ddl.DataValueField = "FinancierID";
                ddl.DataTextField = "FinancierName_Code";
                ddl.DataSource = GetFinancier(FinancierID, FinancierCode);
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
