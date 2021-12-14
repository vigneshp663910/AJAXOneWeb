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
    public class BDMS_SiteContactPersonDesignation
    {
        private IDataAccess provider;
        public BDMS_SiteContactPersonDesignation()
        {
            provider = new ProviderFactory().GetProvider();
        }

        public List<PDMS_SiteContactPersonDesignation> GetSiteContactPersonDesignation(int? DesignationID, string Designation)
        {
            List<PDMS_SiteContactPersonDesignation> DesignationList = new List<PDMS_SiteContactPersonDesignation>();
            try
            {
                DbParameter DesignationIDP = provider.CreateParameter("DesignationID", DesignationID, DbType.Int32);

                DbParameter DesignationP;
                if (!string.IsNullOrEmpty(Designation))
                    DesignationP = provider.CreateParameter("Designation", Designation, DbType.String);
                else
                    DesignationP = provider.CreateParameter("Designation", null, DbType.String);

                DbParameter[] Params = new DbParameter[2] { DesignationIDP, DesignationP };

                using (DataSet DataSet = provider.Select("ZDMS_GetSiteContactPersonDesignation", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            DesignationList.Add(new PDMS_SiteContactPersonDesignation()
                            {
                                DesignationID = Convert.ToInt32(dr["DesignationID"]),
                                Designation = Convert.ToString(dr["Designation"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return DesignationList;
        }

        public void GetSiteContactPersonDesignationDDL(DropDownList ddl, int? DesignationID, string Designation)
        {
            List<PDMS_SiteContactPersonDesignation> DesignationList = GetSiteContactPersonDesignation(DesignationID, Designation);

            ddl.DataValueField = "DesignationID";
            ddl.DataTextField = "Designation";
            ddl.DataSource = DesignationList;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}

