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
   public class BDMS_Model
    { 
        private IDataAccess provider;
        public BDMS_Model()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public void GetTypeOfWarrantyDDL(DropDownList ddl, int? ModelID, string Model, int? DivisionID)
         {

             ddl.DataValueField = "ModelID";
             ddl.DataTextField = "Model";
             ddl.DataSource = GetModel(ModelID, Model, DivisionID);
             ddl.DataBind();
             ddl.Items.Insert(0, new ListItem("Select", "0"));
         }

        public List<PDMS_Model> GetModel(int? ModelID, string Model, int? DivisionID)
         {
             List<PDMS_Model> MML = new List<PDMS_Model>();
             try
             {
                 DbParameter ModelIDP = provider.CreateParameter("ModelID", ModelID, DbType.Int32);
                 DbParameter ModelP = provider.CreateParameter("Model", string.IsNullOrEmpty(Model) ? null : Model, DbType.String);
                 DbParameter DivisionIDP = provider.CreateParameter("DivisionID", DivisionID, DbType.Int32);

                DbParameter[] Params = new DbParameter[3] { ModelIDP, ModelP, DivisionIDP };
                 using (DataSet DataSet = provider.Select("ZDMS_GetModel", Params))
                 {
                     if (DataSet != null)
                     {
                         foreach (DataRow dr in DataSet.Tables[0].Rows)
                         {
                             MML.Add(new PDMS_Model()
                             {
                                 ModelID = Convert.ToInt32(dr["ModelID"]),
                                 ModelDescription= Convert.ToString(dr["ModelDescription"]),
                                 ModelCodeModelDescription = Convert.ToString(dr["ModelCode"]) + " - " + Convert.ToString(dr["Model"]) + " - " + Convert.ToString(dr["ModelDescription"]),
                                 Model = Convert.ToString(dr["Model"]),
                                 ModelCode= Convert.ToString(dr["ModelCode"]),
                                 Division = new PDMS_Division() { DivisionCode = Convert.ToString(dr["DivisionCode"]) , DivisionDescription= Convert.ToString(dr["DivisionDescription"]) }
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
