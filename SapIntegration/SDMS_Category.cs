using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class SDMS_Category
    {
        public DataTable getCategoryFromSAP()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CAT_ID");
            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("TYPE");
            dt.Columns.Add("CATEGORY_1");
            dt.Columns.Add("CATEGORY_2");
            dt.Columns.Add("CATEGORY_3");
            dt.Columns.Add("CATEGORY_4");

            IRfcFunction tagListBapi = SSAP_CRM.CRM_RfcRep().CreateFunction("ZFM_FIELD_HELP_JP");
            tagListBapi.SetValue("IV_CAT_CHECK", "X");
            tagListBapi.Invoke(SSAP_CRM.CRM_RfcDes());
            IRfcTable tagT = tagListBapi.GetTable("ET_CATEGORY");
            for (int i = 0; i < tagT.RowCount; i++)
            {
                try
                {
                    tagT.CurrentIndex = i;
                    dt.Rows.Add(tagT.CurrentRow.GetString("CAT_ID")
                   , tagT.CurrentRow.GetString("DESCRIPTION")
                    , tagT.CurrentRow.GetString("TYPE")
                     , tagT.CurrentRow.GetString("CATEGORY_1")
                      , tagT.CurrentRow.GetString("CATEGORY_2")
                       , tagT.CurrentRow.GetString("CATEGORY_3")
                        , tagT.CurrentRow.GetString("CATEGORY_4"));
                }
                catch (Exception ex)
                { }
            }

            return dt;
        }
    }
}
