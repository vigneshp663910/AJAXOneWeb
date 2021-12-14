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
    public class BDMS_SourceOfEnquiry
    {
          private IDataAccess provider;
        public BDMS_SourceOfEnquiry()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PDMS_SourceOfEnquiry> GetSourceOfEnquiry(int? SourceOfEnquiryID, string SourceOfEnquiry)
        {
            List<PDMS_SourceOfEnquiry> SourceOfEnquirys = new List<PDMS_SourceOfEnquiry>();
            DbParameter SourceOfEnquiryIDP = provider.CreateParameter("SourceOfEnquiryID", SourceOfEnquiryID, DbType.Int32);
            DbParameter SourceOfEnquiryP = provider.CreateParameter("SourceOfEnquiry", string.IsNullOrEmpty(SourceOfEnquiry) ? null : SourceOfEnquiry, DbType.String);
            DbParameter[] Params = new DbParameter[2] { SourceOfEnquiryIDP, SourceOfEnquiryP };
            try
            {
                using (DataSet ds = provider.Select("ZDMS_GetSourceOfEnquiry", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            SourceOfEnquirys.Add(new PDMS_SourceOfEnquiry()
                            {
                                SourceOfEnquiryID = Convert.ToInt32(dr["SourceOfEnquiryID"]),
                                SourceOfEnquiry = Convert.ToString(dr["SourceOfEnquiry"]) 
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
            return SourceOfEnquirys;
        }
        public void GetSourceOfEnquiryDDL(DropDownList ddl, int? SourceOfEnquiryID, string SourceOfEnquiry)
        {
            try
            { 
                ddl.DataValueField = "SourceOfEnquiryID";
                ddl.DataTextField = "SourceOfEnquiry";
                ddl.DataSource = GetSourceOfEnquiry(SourceOfEnquiryID, SourceOfEnquiry); 
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
