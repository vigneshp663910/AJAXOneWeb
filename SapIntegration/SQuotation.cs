using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class SQuotation
    {
        //public List<string> getQuotationIntegration(PSalesQuotation pSalesQuotation, List<PLeadProduct> leadProducts, DateTime VisitDate, PSalesQuotationItem QuotationItem)
        //{
        //    string QuotationNo = null, QuotationDate = null;

        //    IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSD_QUOTATION_DETAILS");
        //    if (!string.IsNullOrEmpty(pSalesQuotation.SapQuotationNo))
        //    {
        //        tagListBapi.SetValue("QUOTATION_NO", pSalesQuotation.SapQuotationNo);
        //    }
        //    if (string.IsNullOrEmpty(pSalesQuotation.SapQuotationNo))
        //    {
        //        if (pSalesQuotation.ShipTo != null)
        //        {
        //            tagListBapi.SetValue("SHIP_TO_PARTY", pSalesQuotation.ShipTo.CustomerCode);
        //        }
        //        else
        //        {
        //            if (pSalesQuotation.Lead.Customer.CustomerCode != null)
        //            {
        //                tagListBapi.SetValue("SHIP_TO_PARTY", pSalesQuotation.Lead.Customer.CustomerCode);
        //            }
        //        }

        //        if (pSalesQuotation.Lead.Customer.CustomerCode != null)
        //        {
        //            //tagListBapi.SetValue("SHIP_TO_PARTY", pSalesQuotation.Lead.Customer.CustomerCode);
        //            tagListBapi.SetValue("SOLD_TO_PARTY", pSalesQuotation.Lead.Customer.CustomerCode);
        //        }
        //    }
        //    tagListBapi.SetValue("QUO_VALID_TO", pSalesQuotation.ValidTo);
        //    //tagListBapi.SetValue("INCOTERMS1", "EXF");
        //    //tagListBapi.SetValue("INCOTERMS2", "FRT-AJAX,INS-CUST");

        //    IRfcStructure QTHeader = tagListBapi.GetStructure("QUOTATION_HEADER_IN");
        //    QTHeader.SetValue("PRICE_DATE", pSalesQuotation.PricingDate);

        //    IRfcStructure QT_FINANCIER_FIELDS = tagListBapi.GetStructure("FINANCIER_FIELDS");
        //    QT_FINANCIER_FIELDS.SetValue("ZZVISIT_DATE", VisitDate);
        //    QT_FINANCIER_FIELDS.SetValue("ZZCOMPETITOR", pSalesQuotation.Competitor[0].Make.Make);//pSalesQuotation.Lead.Customer.Country.SalesOrganization);
        //    QT_FINANCIER_FIELDS.SetValue("ZZFLD00000L", pSalesQuotation.Competitor[0].Product.Product);
        //    QT_FINANCIER_FIELDS.SetValue("ZZPRODUCT", leadProducts[0].Product.Product);

        //    IRfcTable QT_Item = tagListBapi.GetTable("QUOTATION_ITEMS_IN");
        //    int q = 0;
        //    for (int i = 0; i < pSalesQuotation.QuotationItems.Count; i++)
        //    {
        //        QT_Item.Append();
        //        string UPDATEFLAG = string.Empty;
        //        if (QuotationItem.Material != null)
        //        {
        //            if (pSalesQuotation.QuotationItems[i].Material.MaterialCode == QuotationItem.Material.MaterialCode) { UPDATEFLAG = QuotationItem.SapFlag; }
        //            //else
        //            //{
        //            //    UPDATEFLAG = (!string.IsNullOrEmpty(pSalesQuotation.QuotationNo)) ? "" : "I";
        //            //}
        //        }
        //        else
        //        {
        //            //UPDATEFLAG = (!string.IsNullOrEmpty(pSalesQuotation.QuotationNo)) ? "" : "I";
        //        }
        //        QT_Item.SetValue("UPDATEFLAG", UPDATEFLAG);
        //        q = q + 1;
        //        QT_Item.SetValue("ITM_NUMBER", "0000" + q + "0");//"000010"
        //        QT_Item.SetValue("MATERIAL", pSalesQuotation.QuotationItems[i].Material.MaterialCode);//"L.900.508"
        //        QT_Item.SetValue("TARGET_QTY", pSalesQuotation.QuotationItems[i].Qty.ToString("#,###,###,###,###.000"));//"1.000"

        //        //if (!string.IsNullOrEmpty(pSalesQuotation.QuotationNo))
        //        //{
        //        //    QT_Item.SetValue("REASON_REJ", "83");
        //        //}
        //        //QT_Item.SetValue("DISCOUNT_PER", "");
        //        QT_Item.SetValue("DISCOUNT_VALUE", pSalesQuotation.QuotationItems[i].Discount);
        //        QT_Item.SetValue("LIFE_TIME_TAX", pSalesQuotation.LifeTimeTax);
        //    }
        //    if (QuotationItem.Material != null)
        //    {
        //        if (QuotationItem.SapFlag != "D")
        //        {
        //            QT_Item.Append();
        //            QT_Item.SetValue("UPDATEFLAG", "");
        //            q = q + 1;
        //            QT_Item.SetValue("ITM_NUMBER", "0000" + q + "0");//"000010"
        //            QT_Item.SetValue("MATERIAL", QuotationItem.Material.MaterialCode);//"L.900.508"
        //            QT_Item.SetValue("TARGET_QTY", QuotationItem.Qty.ToString("#,###,###,###,###.000"));//"1.000"
        //            //if (!string.IsNullOrEmpty(pSalesQuotation.QuotationNo))
        //            //{
        //            //    QT_Item.SetValue("REASON_REJ", "83");
        //            //}
        //            QT_Item.SetValue("DISCOUNT_VALUE", QuotationItem.Discount);
        //            QT_Item.SetValue("LIFE_TIME_TAX", pSalesQuotation.LifeTimeTax);
        //        }
        //    }


        //    //IRfcTable QUOTATION_CONDITIONS = tagListBapi.GetTable("QUOTATION_CONDITIONS_IN");
        //    //if (pSalesQuotation.QuotationItems[0].Discount > 0)
        //    //{
        //    //    QUOTATION_CONDITIONS.Append();
        //    //    QUOTATION_CONDITIONS.SetValue("ITM_NUMBER", "000010");//"000010"
        //    //    QUOTATION_CONDITIONS.SetValue("COND_TYPE", "ZCD4");
        //    //    QUOTATION_CONDITIONS.SetValue("COND_VALUE", pSalesQuotation.QuotationItems[0].Discount);
        //    //}
        //    //if (pSalesQuotation.LifeTimeTax > 0)
        //    //{
        //    //    QUOTATION_CONDITIONS.Append();
        //    //    QUOTATION_CONDITIONS.SetValue("ITM_NUMBER", "000000");//"000010"
        //    //    QUOTATION_CONDITIONS.SetValue("COND_TYPE", "ZLTT");
        //    //    QUOTATION_CONDITIONS.SetValue("COND_VALUE", pSalesQuotation.LifeTimeTax);
        //    //}



        //    string Reference = "", KindAttention = "", QNote = "", Hypothecation = "", TermsOfPayment = "", Delivery = "", Validity = "", Foc = "", MarginMoney = "", Subject = "";
        //    foreach (PSalesQuotationNote Note in pSalesQuotation.Notes)
        //    {
        //        if (Note.Note.SalesQuotationNoteListID == 1) { Reference = Note.Remark; }
        //        if (Note.Note.SalesQuotationNoteListID == 2) { KindAttention = Note.Remark; }
        //        if (Note.Note.SalesQuotationNoteListID == 3) { QNote = Note.Remark; }
        //        if (Note.Note.SalesQuotationNoteListID == 4) { Hypothecation = Note.Remark; }
        //        if (Note.Note.SalesQuotationNoteListID == 5) { TermsOfPayment = Note.Remark; }
        //        if (Note.Note.SalesQuotationNoteListID == 6) { Delivery = Note.Remark; }
        //        if (Note.Note.SalesQuotationNoteListID == 7) { Validity = Note.Remark; }
        //        if (Note.Note.SalesQuotationNoteListID == 8) { Foc = Note.Remark; }
        //        if (Note.Note.SalesQuotationNoteListID == 9) { MarginMoney = Note.Remark; }
        //        if (Note.Note.SalesQuotationNoteListID == 10) { Subject = Note.Remark; }
        //    }

        //    IRfcTable QT_TEXT = tagListBapi.GetTable("QUOTATION_TEXT");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "REF");//Reference
        //    QT_TEXT.SetValue("TEXT_LINE", Reference);
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "MOB");//Mobile NO
        //    QT_TEXT.SetValue("TEXT_LINE", pSalesQuotation.Lead.Dealer.AuthorityMobile);
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "0013");//Terms of payment
        //    QT_TEXT.SetValue("TEXT_LINE", TermsOfPayment);
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "KA01");//Kind Attention
        //    QT_TEXT.SetValue("TEXT_LINE", KindAttention);
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "DL01");//Delivery
        //    QT_TEXT.SetValue("TEXT_LINE", Delivery);
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "0001");//Hypothecation
        //    QT_TEXT.SetValue("TEXT_LINE", Hypothecation);
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "0002");//Header note 1
        //    QT_TEXT.SetValue("TEXT_LINE", QNote);
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "ZVA");//Validity date
        //    QT_TEXT.SetValue("TEXT_LINE", Validity);
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "ZMN");//Margin  money
        //    QT_TEXT.SetValue("TEXT_LINE", MarginMoney);
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "ZFC");//Foc
        //    QT_TEXT.SetValue("TEXT_LINE", Foc);
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "NAME");//Name
        //    QT_TEXT.SetValue("TEXT_LINE", pSalesQuotation.Lead.Dealer.AuthorityName);
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "DS01");//Designation
        //    QT_TEXT.SetValue("TEXT_LINE", pSalesQuotation.Lead.Dealer.AuthorityDesignation);
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "SE01");//Sales Engineer
        //    QT_TEXT.SetValue("TEXT_LINE", "");
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "SOR1");//source
        //    QT_TEXT.SetValue("TEXT_LINE", "");
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "MOD1");//Model
        //    QT_TEXT.SetValue("TEXT_LINE", "");
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "IN01");//Inquiry NO
        //    QT_TEXT.SetValue("TEXT_LINE", "");
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "NOTE");//Note - CBC and Printers
        //    QT_TEXT.SetValue("TEXT_LINE", "");
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "ZCST");//CST No
        //    QT_TEXT.SetValue("TEXT_LINE", "");
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "ZRE");//revision
        //    QT_TEXT.SetValue("TEXT_LINE", "");
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "ZMM");//Balance
        //    QT_TEXT.SetValue("TEXT_LINE", "");
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "ZSC");//Special Charges
        //    QT_TEXT.SetValue("TEXT_LINE", "");
        //    QT_TEXT.SetValue("LANGU", "EN");
        //    QT_TEXT.Append();
        //    QT_TEXT.SetValue("TEXT_ID", "ZDLR");//DMS Order confirmation
        //    QT_TEXT.SetValue("TEXT_LINE", "");
        //    QT_TEXT.SetValue("LANGU", "EN");

        //    tagListBapi.Invoke(SAP.RfcDes());
        //    QuotationNo = tagListBapi.GetValue("P_QUOTATION_NO").ToString();
        //    QuotationDate = (QuotationNo=="")?null:tagListBapi.GetValue("P_DATE").ToString();
        //    string P_RESULT = tagListBapi.GetValue("P_RESULT").ToString();
        //    IRfcTable table = tagListBapi.GetTable("RETURN");

        //    DataTable dtRet = new DataTable();

        //    for (int Column = 0; Column < 4; Column++)
        //    {
        //        RfcElementMetadata rfcEMD = table.GetElementMetadata(Column);
        //        dtRet.Columns.Add(rfcEMD.Name);
        //    }

        //    foreach (IRfcStructure row in table)
        //    {
        //        DataRow dr = dtRet.NewRow();
        //        for (int Column = 0; Column < 4; Column++)
        //        {
        //            RfcElementMetadata rfcEMD = table.GetElementMetadata(Column);
        //            dr[rfcEMD.Name] = row.GetString(rfcEMD.Name);
        //            // Console.WriteLine("{0} is {1}", rfcEMD.Documentation, dr[rfcEMD.Name]);
        //        }
        //        dtRet.Rows.Add(dr);
        //    }
        //    if (dtRet.Rows.Count == 0) { dtRet.Rows.Add("S", "", "", QuotationNo); }

        //    List<string> list = new List<string>();

        //    string Subrc = "", Number = "", Type = "", Message = "", QtNo = "", QtDate = "";
        //    foreach (DataRow dr in dtRet.Rows)
        //    {
        //        Subrc = P_RESULT;
        //        Number = dr["NUMBER"].ToString();
        //        Type = dr["TYPE"].ToString();
        //        Message = dr["MESSAGE"].ToString();
        //        QtNo = QuotationNo;
        //        QtDate = Convert.ToDateTime(QuotationDate).ToString();
        //    }
        //    list.Add(Subrc);
        //    list.Add(Number);
        //    list.Add(Type);
        //    list.Add(Message);
        //    list.Add(QtNo);
        //    list.Add(QtDate);
        //    return list;
        //}
        public PSalesQuotationItem getMaterialTaxForQuotation(PSalesQuotation Quotation, string MaterialCode, Boolean IsWarrenty, decimal qty)
        {

            PSalesQuotationItem Material = new PSalesQuotationItem();
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSIMULATE_QUO");
            tagListBapi.SetValue("CUSTOMER", string.IsNullOrEmpty(Quotation.Lead.Customer.CustomerCode) ? "" : Quotation.Lead.Customer.CustomerCode.Trim().PadLeft(6, '0'));

            IRfcTable IT_SO_ITEMS = tagListBapi.GetTable("IT_SO_ITEMS");
            long n;
            if (long.TryParse(MaterialCode, out n))
            {
                MaterialCode = MaterialCode.PadLeft(18, '0');
            }
            int q = 0;
            foreach (PSalesQuotationItem item in Quotation.QuotationItems)
            {
                if (item.Material.MaterialCode != null)
                {
                    q = q + 1;
                    IT_SO_ITEMS.Append();
                    IT_SO_ITEMS.SetValue("ITEM_NO", "0000" + q + "0");//To Discuss
                    IT_SO_ITEMS.SetValue("MATERIAL", item.Material.MaterialCode);//"L.900.508"
                    IT_SO_ITEMS.SetValue("QUANTITY", item.Qty);
                }
            }
            IT_SO_ITEMS.Append();
            IT_SO_ITEMS.SetValue("ITEM_NO", "0000" + q + 1 + "0");//To Discuss
            IT_SO_ITEMS.SetValue("MATERIAL", MaterialCode);//"L.900.508"
            IT_SO_ITEMS.SetValue("QUANTITY", qty);

            tagListBapi.Invoke(SAP.RfcDes());
            IRfcTable tagTable = tagListBapi.GetTable("IT_SO_COND");
            IRfcStructure ES_ERROR = tagListBapi.GetStructure("ES_ERROR");

            string ConditionType;
            for (int i = 0; i < tagTable.RowCount; i++)
            {
                tagTable.CurrentIndex = i;
                if (MaterialCode == tagTable.CurrentRow.GetString("MATERIAL"))
                {
                    ConditionType = tagTable.CurrentRow.GetString("COND_TYPE");
                    if ((ConditionType == "JOCG") || (ConditionType == "JOSG"))
                    {
                        Material.SGST = Convert.ToInt32(Convert.ToDecimal(tagTable.CurrentRow.GetString("PERCENTAGE")));
                        Material.SGSTValue = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                    }
                    else if (ConditionType == "JOIG")
                    {
                        Material.IGST = Convert.ToInt32(Convert.ToDecimal(tagTable.CurrentRow.GetString("PERCENTAGE")));
                        Material.IGSTValue = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                    }
                    else if ((ConditionType == "ZAEP") || (ConditionType == "ZASS"))
                    {
                        if (IsWarrenty)
                        {
                            if (ConditionType == "ZASS")
                                Material.Rate = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                        }
                        else
                        {
                            if (ConditionType == "ZAEP")
                                Material.Rate = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                        }
                    }
                    else if (ConditionType == "ZTC1")
                    {
                        Material.TCSTax = Convert.ToDecimal(tagTable.CurrentRow.GetString("PERCENTAGE"));
                        Material.TCSValue = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                    }
                }
            }
            if (tagTable.RowCount == 0)
            {
                string Type = ES_ERROR.GetValue("Type").ToString();
                string Message = ES_ERROR.GetValue("Message").ToString();
                try
                {
                    throw new Exception(Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return Material;
        }
        public DataTable getMaterialTextForQuotation(string MaterialCode)
        {
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZREAD_TEXT");
            tagListBapi.SetValue("MATERIAL", MaterialCode);//"L.900.508         AJF GT");
            //tagListBapi.SetValue("SALES_ORG", "AJF");
            //tagListBapi.SetValue("DIST_CHANNEL", "GT");
            tagListBapi.Invoke(SAP.RfcDes());
            IRfcTable tagTable = tagListBapi.GetTable("LINES");
            DataTable dtRet = new DataTable();

            for (int Column = 0; Column < 2; Column++)
            {
                RfcElementMetadata rfcEMD = tagTable.GetElementMetadata(Column);
                dtRet.Columns.Add(rfcEMD.Name);
            }

            foreach (IRfcStructure row in tagTable)
            {
                DataRow dr = dtRet.NewRow();
                for (int Column = 0; Column < 2; Column++)
                {
                    RfcElementMetadata rfcEMD = tagTable.GetElementMetadata(Column);
                    dr[rfcEMD.Name] = row.GetString(rfcEMD.Name);
                    // Console.WriteLine("{0} is {1}", rfcEMD.Documentation, dr[rfcEMD.Name]);
                }
                dtRet.Rows.Add(dr);
            }
            string ConditionType;

            return dtRet;
        }
        //public List<PSalesQuotationDocumentDetails> GetSalesQuotationFlow()
        //{
        //    IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSD_QUOTATION_FLOW_DET");
        //    tagListBapi.Invoke(SAP.RfcDes());
        //    IRfcTable tagTable = tagListBapi.GetTable("IT_DETAILS");
        //    DataTable dtRet = new DataTable();
            
        //    for (int Column = 0; Column < 14; Column++)
        //    {
        //        RfcElementMetadata rfcEMD = tagTable.GetElementMetadata(Column);
        //        dtRet.Columns.Add(rfcEMD.Name);
        //    }

        //    foreach (IRfcStructure row in tagTable)
        //    {
        //        DataRow dr = dtRet.NewRow();
        //        for (int Column = 0; Column < 14; Column++)
        //        {
        //            RfcElementMetadata rfcEMD = tagTable.GetElementMetadata(Column);
        //            dr[rfcEMD.Name] = row.GetString(rfcEMD.Name);
        //        }
        //        dtRet.Rows.Add(dr);
        //    }
        //    //if (dtRet.Rows.Count == 0) { dtRet.Rows.Add("", "", "", "S", "", "", "", SUBRC, "", "", "", "", ""); }
        //    List<PSalesQuotationDocumentDetails> SalesQuotationDocumentDetails = new List<PSalesQuotationDocumentDetails>();
            
        //    foreach (DataRow dr in dtRet.Rows)
        //    {
        //        PSalesQuotationDocumentDetails SalesQuotationDocumentDetail = new PSalesQuotationDocumentDetails();
        //        SalesQuotationDocumentDetail.QuotationNo = dr["VBELV"].ToString();
        //        SalesQuotationDocumentDetail.Item = Convert.ToInt32(dr["POSNV"]);
        //        SalesQuotationDocumentDetail.SubSequentItem = Convert.ToInt32(dr["POSNN"]);
        //        SalesQuotationDocumentDetail.DocumentNumber = dr["VBELN"].ToString();
        //        SalesQuotationDocumentDetail.DocumentCode = dr["VBTYP_N"].ToString();
        //        SalesQuotationDocumentDetail.DocumentName = dr["DDTEXT"].ToString();
        //        if (dr["ERDAT"] != "0000-00-00")
        //        {
        //            SalesQuotationDocumentDetail.DocumentDate = Convert.ToDateTime(dr["ERDAT"]);
        //        }
        //        SalesQuotationDocumentDetail.Material = dr["MATNR"].ToString();
        //        SalesQuotationDocumentDetail.MachineSerialNumber = dr["SERNR"].ToString();
        //        SalesQuotationDocumentDetails.Add(SalesQuotationDocumentDetail);
        //    }
        //    return SalesQuotationDocumentDetails;
        //}
        //public void UpdateStatusSalesQuotationFlow(PSalesQuotationDocumentDetails SalesQuotationDocumentDetail)
        //{
        //    IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSD_QUOTATION_FLOW_DET");
        //    tagListBapi.SetValue("UPDATE", "X");
        //    IRfcTable tagTable = tagListBapi.GetTable("IT_UPDATE");
        //    tagTable.Append();
        //    tagTable.SetValue("VBELV", SalesQuotationDocumentDetail.QuotationNo);
        //    tagTable.SetValue("POSNV", SalesQuotationDocumentDetail.Item);
        //    tagTable.SetValue("VBELN", SalesQuotationDocumentDetail.DocumentNumber);
        //    tagTable.SetValue("POSNN", SalesQuotationDocumentDetail.SubSequentItem);
        //    tagTable.SetValue("VBTYP_N", SalesQuotationDocumentDetail.DocumentCode);
        //    tagListBapi.Invoke(SAP.RfcDes());
        //}
    }
}
