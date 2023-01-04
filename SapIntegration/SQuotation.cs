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

                    if ((ConditionType == "JOCG") || (ConditionType == "JOSG") || (ConditionType == "JOIG"))
                    {
                        if (Quotation.Lead.Dealer.StateCode == Quotation.Lead.Customer.State.StateCode)
                        {
                            if ((ConditionType == "JOCG") || (ConditionType == "JOSG"))
                            {
                                Material.SGST = Convert.ToInt32(Convert.ToDecimal(tagTable.CurrentRow.GetString("PERCENTAGE")));
                                Material.SGSTValue = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                            }
                            else if (ConditionType == "JOIG")
                            {
                                Material.SGST = Convert.ToInt32(Convert.ToDecimal(tagTable.CurrentRow.GetString("PERCENTAGE"))) / 2;
                                Material.SGSTValue = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE")) / 2;
                            }
                        }
                        else
                        {
                            if ((ConditionType == "JOCG") || (ConditionType == "JOSG"))
                            {
                                Material.IGST = Convert.ToInt32(Convert.ToDecimal(tagTable.CurrentRow.GetString("PERCENTAGE"))) * 2;
                                Material.IGSTValue = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE")) * 2;
                            }
                            else if (ConditionType == "JOIG")
                            {
                                Material.IGST = Convert.ToInt32(Convert.ToDecimal(tagTable.CurrentRow.GetString("PERCENTAGE")));
                                Material.IGSTValue = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                            }
                        }
                    }
                    //if ((ConditionType == "JOCG") || (ConditionType == "JOSG"))
                    //{
                    //    Material.SGST = Convert.ToInt32(Convert.ToDecimal(tagTable.CurrentRow.GetString("PERCENTAGE")));
                    //    Material.SGSTValue = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                    //}
                    //else if (ConditionType == "JOIG")
                    //{
                    //    Material.IGST = Convert.ToInt32(Convert.ToDecimal(tagTable.CurrentRow.GetString("PERCENTAGE")));
                    //    Material.IGSTValue = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                    //}
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
      
    }
}
