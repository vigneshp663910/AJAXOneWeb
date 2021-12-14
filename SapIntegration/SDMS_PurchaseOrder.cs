using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SapIntegration
{
  public  class SDMS_PurchaseOrder
    {
      public PDMS_PurchaseOrderInvoice getPurchaseOrderInvoiceFromSAP(string DeliveryNo)
      { 

          PDMS_PurchaseOrderInvoice Invoice = new PDMS_PurchaseOrderInvoice();
          Invoice.InvoiceItemS = new List<PDMS_PurchaseOrderInvoiceItem>();

          decimal TotalValue = 0;
          decimal TotalTCSValue = 0;

          PDMS_PurchaseOrderInvoiceItem InvoiceItem = null;
          IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSD_INVOICE_GET");
          tagListBapi.SetValue("P_DEL", DeliveryNo); 
          tagListBapi.Invoke(SAP.RfcDes());

          IRfcTable IT_HEAD = tagListBapi.GetTable("IT_HEAD");
          IRfcTable IT_ITEM = tagListBapi.GetTable("IT_ITEM");
          IRfcTable IT_COND = tagListBapi.GetTable("IT_COND");

          try
          {
              if (IT_HEAD.RowCount > 0)
              {
                  IT_HEAD.CurrentIndex = 0;
                  Invoice.Invoice = IT_HEAD.GetString("VBELN");
                  Invoice.InvoiceDate = Convert.ToDateTime(IT_HEAD.GetString("FKDAT"));
                  Invoice.Currency = IT_HEAD.GetString("STWAE");
              }
            // Invoice.NetAmount = IT_HEAD.GetString("DEVICEID");
            //  Invoice.Invoice = IT_HEAD.GetString("DEVICEID");
            // Invoice.Invoice = IT_HEAD.GetString("DEVICEID");             
          }
          catch (Exception e1)
          {
              string s = e1.Message;
          }

          for (int i = 0; i < IT_ITEM.RowCount; i++)
          {
              try
              {
                  InvoiceItem = new PDMS_PurchaseOrderInvoiceItem();
                  Invoice.InvoiceItemS.Add(InvoiceItem);

                  IT_ITEM.CurrentIndex = i;
                  InvoiceItem.Item = Convert.ToInt32(IT_ITEM.GetString("POSNR"));
                  InvoiceItem.Material = new PDMS_Material() { MaterialCode = IT_ITEM.GetString("MATNR") };
                  InvoiceItem.Qty = Convert.ToDecimal(IT_ITEM.GetString("FKIMG"));


                  InvoiceItem.Rate = 0;
                  InvoiceItem.NetAmount = 0;
                  InvoiceItem.GrossAmount = 0;
                  InvoiceItem.Discount = 0;
                  InvoiceItem.TaxableValue = 0;

                  InvoiceItem.SGSTValue = 0;
                  InvoiceItem.CGSTValue = 0;
                  InvoiceItem.IGSTValue = 0;


                  InvoiceItem.SGST = 0;
                  InvoiceItem.CGST = 0;
                  InvoiceItem.IGST = 0;

                  string ConditionType = "";
                  int Item = 0;
                  for (int j = 0; j < IT_COND.RowCount; j++)
                  {
                      IT_COND.CurrentIndex = j;
                      Item = Convert.ToInt32(IT_COND.GetString("KPOSN"));
                      if (Item != InvoiceItem.Item) { continue; }
                      ConditionType = IT_COND.GetString("KSCHL"); 
                      if ((ConditionType == "ZPRP") || (ConditionType == "ZAEP") || (ConditionType == "ZPMC") || (ConditionType == "ZVSP") || (ConditionType == "ZACP"))
                      {
                          InvoiceItem.Rate = Convert.ToDecimal(IT_COND.GetString("KBETR"));
                      }
                      else if (ConditionType == "ZFRT")
                      {
                          InvoiceItem.Freight = Convert.ToDecimal(IT_COND.GetString("KBETR"));
                      }
                      else if (ConditionType == "ZINS")
                      {
                          InvoiceItem.Insururance = Convert.ToDecimal(IT_COND.GetString("KBETR"));
                      }
                      else if (ConditionType == "ZPKF")
                      {
                          InvoiceItem.Packing = Convert.ToDecimal(IT_COND.GetString("KBETR"));
                      }
                  }

                  ConditionType = "";
                  Item = 0;
                  for (int j = 0; j < IT_COND.RowCount; j++)
                  {
                      IT_COND.CurrentIndex = j;
                      Item = Convert.ToInt32(IT_COND.GetString("KPOSN"));
                      if (Item != InvoiceItem.Item) { continue; }
                      ConditionType = IT_COND.GetString("KSCHL");

                       

                      if ((ConditionType == "ZDD1") || (ConditionType == "ZDE1") || (ConditionType == "ZDLY") || (ConditionType == "ZSD2") || (ConditionType == "ZWA%") || (ConditionType == "ZCD1")
                          || (ConditionType == "ZDD4") || (ConditionType == "ZDD5") || (ConditionType == "ZCD3") || (ConditionType == "ZDD9"))
                      {
                          InvoiceItem.Discount = InvoiceItem.Discount + ((InvoiceItem.Rate * Convert.ToDecimal(IT_COND.GetString("KBETR")) / 1000) * InvoiceItem.Qty);
                      }
                      else if ((ConditionType == "ZDE2") || (ConditionType == "ZSD1") || (ConditionType == "ZDD2") || (ConditionType == "ZWAR") || (ConditionType == "ZCD2")
                           || (ConditionType == "ZDD6") || (ConditionType == "ZCD4"))
                      {
                          InvoiceItem.Discount = InvoiceItem.Discount + (Convert.ToDecimal(IT_COND.GetString("KBETR")) * InvoiceItem.Qty);
                      }

                  }
                  InvoiceItem.TaxableValue = (InvoiceItem.Rate * InvoiceItem.Qty) + InvoiceItem.Discount;

                  ConditionType = "";
                  Item = 0;
                  for (int j = 0; j < IT_COND.RowCount; j++)
                  {
                      IT_COND.CurrentIndex = j;
                      Item = Convert.ToInt32(IT_COND.GetString("KPOSN"));
                      if (Item != InvoiceItem.Item) { continue; }

                      ConditionType = IT_COND.GetString("KSCHL");

                      if ((ConditionType == "ZOSG") || (ConditionType == "JOSG"))
                      {
                          InvoiceItem.SGST = Convert.ToDecimal(Convert.ToDecimal(IT_COND.GetString("KBETR"))) / 10;
                          InvoiceItem.SGSTValue = InvoiceItem.TaxableValue * InvoiceItem.SGST / 100;
                      }
                      else if ((ConditionType == "ZOIG") || (ConditionType == "JOIG"))
                      {
                          InvoiceItem.IGST = Convert.ToDecimal(Convert.ToDecimal(IT_COND.GetString("KBETR"))) / 10;
                          InvoiceItem.IGSTValue = InvoiceItem.TaxableValue * InvoiceItem.IGST / 100;
                      }                      
                  }

                  ConditionType = "";
                  Item = 0;
                  for (int j = 0; j < IT_COND.RowCount; j++)
                  {
                      IT_COND.CurrentIndex = j;
                      Item = Convert.ToInt32(IT_COND.GetString("KPOSN"));
                      if (Item != InvoiceItem.Item) { continue; }

                      ConditionType = IT_COND.GetString("KSCHL");
                      if (ConditionType == "ZTC1")
                      {
                          InvoiceItem.TCS = Convert.ToDecimal(Convert.ToDecimal(IT_COND.GetString("KBETR"))) / 10;
                          InvoiceItem.TCSValue = (InvoiceItem.TaxableValue + InvoiceItem.SGSTValue + InvoiceItem.SGSTValue + InvoiceItem.IGSTValue) * InvoiceItem.TCS / 100;
                      }
                  }

                  TotalValue = TotalValue + InvoiceItem.TaxableValue + InvoiceItem.SGSTValue + InvoiceItem.SGSTValue + InvoiceItem.IGSTValue + InvoiceItem.TCSValue + InvoiceItem.Freight + InvoiceItem.Insururance + InvoiceItem.Packing;
                  TotalTCSValue = TotalTCSValue + InvoiceItem.TCSValue;
              }
              catch (Exception e1)
              {
                  string s = e1.Message;
              }
          }
          Invoice.TotalValue = TotalValue;
          Invoice.TotalTCSValue = TotalTCSValue;
          return Invoice;
      }
    }
}
