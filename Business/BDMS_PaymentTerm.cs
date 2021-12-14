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
    public class BDMS_PaymentTerm
    {
     private IDataAccess provider;
         public BDMS_PaymentTerm()
        {
            provider = new ProviderFactory().GetProvider();
        }
         public List<PDMS_PaymentTerm> GetPaymentTerm(int? PaymentTermID, string PaymentTerm)
        {
            List<PDMS_PaymentTerm> PaymentTermS = new List<PDMS_PaymentTerm>();
            DbParameter DiscountTypeIDP = provider.CreateParameter("PaymentTermID", PaymentTermID, DbType.Int32);
            DbParameter DiscountTypeP = provider.CreateParameter("PaymentTerm", string.IsNullOrEmpty(PaymentTerm) ? null : PaymentTerm, DbType.String);
            DbParameter[] Params = new DbParameter[2] { DiscountTypeIDP, DiscountTypeP };
            try
            {
                using (DataSet ds = provider.Select("ZDMS_GetPaymentTerm", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PaymentTermS.Add(new PDMS_PaymentTerm()
                            {
                                PaymentTermID = Convert.ToInt32(dr["PaymentTermID"]),
                                PaymentTerm = Convert.ToString(dr["PaymentTerm"]),
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
            return PaymentTermS;
        }
         public void GetPaymentTermDDL(DropDownList ddl, int? PaymentTermID, string PaymentTerm)
        {
            try
            {
                ddl.DataValueField = "PaymentTermID";
                ddl.DataTextField = "PaymentTerm_Description";
                ddl.DataSource = GetPaymentTerm(PaymentTermID, PaymentTerm); 
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
