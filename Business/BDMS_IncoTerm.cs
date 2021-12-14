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
    public class BDMS_IncoTerm
    {
         private IDataAccess provider;
         public BDMS_IncoTerm()
        {
            provider = new ProviderFactory().GetProvider();
        }
         public List<PDMS_IncoTerm> GetIncoTerm(int? IncoTermID, string IncoTerm)
        {
            List<PDMS_IncoTerm> IncoTermS = new List<PDMS_IncoTerm>();
            DbParameter DiscountTypeIDP = provider.CreateParameter("IncoTermID", IncoTermID, DbType.Int32);
            DbParameter DiscountTypeP = provider.CreateParameter("IncoTerm", string.IsNullOrEmpty(IncoTerm) ? null : IncoTerm, DbType.String);
            DbParameter[] Params = new DbParameter[2] { DiscountTypeIDP, DiscountTypeP };
            try
            {
                using (DataSet ds = provider.Select("ZDMS_GetIncoTerm", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            IncoTermS.Add(new PDMS_IncoTerm()
                            {
                                IncoTermID = Convert.ToInt32(dr["IncoTermID"]),
                                IncoTerm = Convert.ToString(dr["IncoTerm"]),
                                Description = Convert.ToString(dr["Description"])
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
            return IncoTermS;
        }
         public void GetIncoTermDDL(DropDownList ddl, int? IncoTermID, string IncoTerm)
        {
            try
            {
                ddl.DataValueField = "IncoTermID";
                ddl.DataTextField = "IncoTerm_Description";
                ddl.DataSource = GetIncoTerm(IncoTermID, IncoTerm); 
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
