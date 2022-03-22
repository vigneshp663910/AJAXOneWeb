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
            if (!string.IsNullOrEmpty(pSalesQuotation.QuotationNo))
            {
                tagListBapi.SetValue("QUOTATION_NO", pSalesQuotation.QuotationNo);
            }
            if (pSalesQuotation.ShipTo.CustomerCode != null)
            {
                tagListBapi.SetValue("SHIP_TO_PARTY", pSalesQuotation.ShipTo.CustomerCode);
            }
            else
            {
                if (pSalesQuotation.Lead.Customer.CustomerCode != null)
                {
                    tagListBapi.SetValue("SHIP_TO_PARTY", pSalesQuotation.Lead.Customer.CustomerCode);
                }
            }

            if (pSalesQuotation.Lead.Customer.CustomerCode != null)
            {
                tagListBapi.SetValue("SOLD_TO_PARTY", pSalesQuotation.Lead.Customer.CustomerCode);
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

            IRfcTable QT_TEXT = tagListBapi.GetTable("QUOTATION_TEXT");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "REF");//Reference
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "NAME");//Name
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "DS01");//Designation
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "MOB");//Mobile NO
            QT_TEXT.SetValue("TEXT_LINE", "123456789");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "SE01");//Sales Engineer
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "0013");//Terms of payment
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "SOR1");//source
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "KA01");//Kind Attention
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "MOD1");//Model
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "IN01");//Inquiry NO
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "DL01");//Delivery
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "NOTE");//Note - CBC and Printers
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "0001");//Hypothecation
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "ZCST");//CST No
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "ZRE");//revision
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "ZMM");//Balance
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "ZSC");//Special Charges
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "ZMN");//Margin  money
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "ZVA");//Validity date
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "ZDLR");//DMS Order confirmation
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "ZFC");//Foc
            QT_TEXT.SetValue("TEXT_LINE", "");
            QT_TEXT.Append();
            QT_TEXT.SetValue("TEXT_ID", "0002");//Header note 1
            QT_TEXT.SetValue("TEXT_LINE", "");


            tagListBapi.Invoke(SAP.RfcDes());
            QuotationNo = tagListBapi.GetValue("P_QUOTATION_NO").ToString();
            IRfcTable table = tagListBapi.GetTable("RETURN");

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
