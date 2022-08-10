using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapIntegration
{
    public class SEnquiry
    {
        public DataTable getEnquiry(DateTime From, DateTime To, string EnquiryNo, string DealerCode, string CustomerCode)
        {
            IRfcFunction tagListBapi = SSAP_CRM.CRM_RfcRep().CreateFunction("ZCRM_SALES_QA_GET");
            tagListBapi.SetValue("IC_DATE_FROM", From);
            tagListBapi.SetValue("IC_DATE_TO", To);
            tagListBapi.SetValue("IC_ENQUIRY_NO", EnquiryNo);
            tagListBapi.SetValue("DEALER_CODE", DealerCode);
            tagListBapi.SetValue("CUSTOMER_CODE", CustomerCode);
            tagListBapi.Invoke(SSAP_CRM.CRM_RfcDes());
            IRfcTable tagTable = tagListBapi.GetTable("IT_DISPLAY");

            DataTable dtRet = new DataTable();

            for (int Column = 0; Column < 97; Column++)
            {
                RfcElementMetadata rfcEMD = tagTable.GetElementMetadata(Column);
                dtRet.Columns.Add(rfcEMD.Name);
            }

            foreach (IRfcStructure row in tagTable)
            {
                DataRow dr = dtRet.NewRow();
                for (int Column = 0; Column < 97; Column++)
                {
                    RfcElementMetadata rfcEMD = tagTable.GetElementMetadata(Column);
                    dr[rfcEMD.Name] = row.GetString(rfcEMD.Name);
                }
                dtRet.Rows.Add(dr);
            }
            
            return dtRet;
        }
    }
}
