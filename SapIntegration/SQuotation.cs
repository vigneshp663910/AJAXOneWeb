using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class SQuotation
    {
        public DataTable getQuotationIntegration(PSalesQuotation pSalesQuotation)
        {
            string QuotationNo = null;
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSD_QUOTATION_DETAILS");

            if (pSalesQuotation.ShipTo != null)
            {
                tagListBapi.SetValue("SHIP_TO_PARTY", "0000300525");
            }
            else
            {
                if (pSalesQuotation.Lead.Customer.CustomerCode != null)
                {
                    tagListBapi.SetValue("SHIP_TO_PARTY", "0000300525");
                }
            }

            if (pSalesQuotation.Lead.Customer.CustomerCode != null)
            {
                tagListBapi.SetValue("SOLD_TO_PARTY", "0000300525");
            }



            IRfcStructure QTHeader = tagListBapi.GetStructure("QUOTATION_HEADER_IN");
            QTHeader.SetValue("DOC_TYPE", "ZMQT"/*pSalesQuotation.QuotationType.QuotationType*/);
            QTHeader.SetValue("SALES_ORG", "AJF");//pSalesQuotation.Lead.Customer.Country.SalesOrganization);
            QTHeader.SetValue("DISTR_CHAN", "GT");
            QTHeader.SetValue("DIVISION", "CM");
            QTHeader.SetValue("QT_VALID_T", pSalesQuotation.ValidTo);

            IRfcTable QT_Item = tagListBapi.GetTable("QUOTATION_ITEMS_IN");
            for (int i = 0; i < pSalesQuotation.QuotationItems.Count; i++)
            {
                QT_Item.Append();
                QT_Item.SetValue("ITM_NUMBER", pSalesQuotation.QuotationItems[i].Item);//"000010"
                QT_Item.SetValue("MATERIAL", pSalesQuotation.QuotationItems[i].Material.MaterialCode);//"L.900.508"
                QT_Item.SetValue("PLANT", pSalesQuotation.QuotationItems[i].Plant.PlantCode);//"P003"
                QT_Item.SetValue("TARGET_QTY", pSalesQuotation.QuotationItems[i].Qty);//"1.000"
            }


            //IRfcTable QT_TEXT = tagListBapi.GetTable("QUOTATION_TEXT");
            //for (int i = 0; i < pSalesQuotation.QuotationItems.Count; i++)
            //{
            //    QT_TEXT.Append();
            //    QT_TEXT.SetValue("TEXT_ID", "MOB");
            //    QT_TEXT.SetValue("TEXT_LINE", "123456789");
            //}

            tagListBapi.Invoke(SAP.RfcDes());
            QuotationNo = tagListBapi.GetValue("P_QUOTATION_NO").ToString();
            IRfcTable table = tagListBapi.GetTable("RETURN");
            string Msg = table.GetValue("MESSAGE").ToString();
            string Type = table.GetValue("Type").ToString();

            DataTable dtRet = new DataTable();

            for (int Column = 0; Column < 4; Column++)
            {
                RfcElementMetadata rfcEMD = table.GetElementMetadata(Column);
                dtRet.Columns.Add(rfcEMD.Name);
            }

            foreach (IRfcStructure row in table)
            {
                DataRow dr = dtRet.NewRow();
                for (int Column = 0; Column < 4; Column++)
                {
                    RfcElementMetadata rfcEMD = table.GetElementMetadata(Column);
                    dr[rfcEMD.Name] = row.GetString(rfcEMD.Name);
                    // Console.WriteLine("{0} is {1}", rfcEMD.Documentation, dr[rfcEMD.Name]);
                }
                dtRet.Rows.Add(dr);
            }
            if (dtRet.Rows.Count == 0) { dtRet.Rows.Add("S", "", "", QuotationNo); }
            return dtRet;
        }
    }
}
