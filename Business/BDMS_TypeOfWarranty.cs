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
using System.Web.UI.WebControls;

namespace Business
{
    public class BDMS_TypeOfWarranty
    {
         private IDataAccess provider;
         public BDMS_TypeOfWarranty()
        {
            provider = new ProviderFactory().GetProvider();
        }
         public void GetTypeOfWarrantyDDL(DropDownList ddl, int? TypeOfWarrantyID, string TypeOfWarranty)
         {

             ddl.DataValueField = "TypeOfWarrantyID";
             ddl.DataTextField = "TypeOfWarranty";
             ddl.DataSource = GetTypeOfWarranty(TypeOfWarrantyID, TypeOfWarranty);
             ddl.DataBind();
             ddl.Items.Insert(0, new ListItem("Select", "0"));
         }

         public List<PDMS_TypeOfWarranty> GetTypeOfWarranty(int? TypeOfWarrantyID, string TypeOfWarranty)
         {
             List<PDMS_TypeOfWarranty> MML = new List<PDMS_TypeOfWarranty>();
             try
             {

                 DbParameter TypeOfWarrantyIDP = provider.CreateParameter("TypeOfWarrantyID", TypeOfWarrantyID, DbType.Int32);
                 DbParameter TypeOfWarrantyP = provider.CreateParameter("TypeOfWarranty", string.IsNullOrEmpty(TypeOfWarranty) ? null : TypeOfWarranty, DbType.String);

                 DbParameter[] Params = new DbParameter[2] { TypeOfWarrantyIDP, TypeOfWarrantyP };
                 using (DataSet DataSet = provider.Select("ZDMS_GetTypeOfWarranty", Params))
                 {
                     if (DataSet != null)
                     {
                         foreach (DataRow dr in DataSet.Tables[0].Rows)
                         {
                             MML.Add(new PDMS_TypeOfWarranty()
                             {
                                 TypeOfWarrantyID = Convert.ToInt32(dr["TypeOfWarrantyID"]),
                                 TypeOfWarranty = Convert.ToString(dr["TypeOfWarranty"])
                             });
                         }
                     }
                 }
             }
             catch (SqlException sqlEx)
             { }
             catch (Exception ex)
             { }
             return MML;
         }
    }
}
