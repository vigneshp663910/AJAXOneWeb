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
    public class BDMS_Division
    {
        private IDataAccess provider;
        public BDMS_Division()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public void GetDivisionForSerchGroped(DropDownList ddl)
        {
            try
            {

                ddl.DataTextField = "DivisionCode";
                ddl.DataValueField = "DivisionID";

                ddl.Items.Insert(0, new ListItem("All", "0"));
                ddl.Items.Insert(1, new ListItem("CM", "2"));
                ddl.Items.Insert(2, new ListItem("BP", "1"));
                ddl.Items.Insert(3, new ListItem("CP", "3"));
                ddl.Items.Insert(4, new ListItem("BP,TM", "1,11"));
                ddl.Items.Insert(5, new ListItem("CP,BP,TM,PS,DP", "3,1,11,14,4"));


                //ddl.Items.Insert(3, new ListItem("SP", "15")); 
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
        }
        //public void GetDivision(DropDownList ddl, int? DivisionID, string DivisionCode)
        //{
        //    try
        //    {

        //        ddl.DataTextField = "DivisionCode";
        //        ddl.DataValueField = "DivisionID";
        //        ddl.DataSource = GetDivision(DivisionID, DivisionCode);
        //        ddl.DataBind();
        //        ddl.Items.Insert(0, new ListItem("All", "0"));

        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //}
        //public List<PDMS_Division> GetDivision(int? DivisionID, string DivisionCode)
        //{


        //    List<PDMS_Division> Division = new List<PDMS_Division>();
        //    try
        //    {

        //        DbParameter DivisionIDP = provider.CreateParameter("DivisionID", DivisionID, DbType.Int32);
        //        DbParameter DivisionCodeP = provider.CreateParameter("ServiceStatus", string.IsNullOrEmpty(DivisionCode) ? null : DivisionCode, DbType.String);
        //        DbParameter[] Params = new DbParameter[2] { DivisionIDP, DivisionCodeP };
        //        using (DataSet DataSet = provider.Select("ZDMS_GetDivision", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    Division.Add(new PDMS_Division() { DivisionID = Convert.ToInt32(dr["DivisionID"]), DivisionCode = Convert.ToString(dr["DivisionCode"]) });
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return Division;
        //}
    }
}