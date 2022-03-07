using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class SDMS_WebQuotation
    {
        public Boolean UpdateICTicketRequestedDateToSAP(PDMS_WebQuotation Quot)
        {
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSD_ORDER_CHANGE");

            tagListBapi.SetValue("ORDER_NO", Quot.SalesOrderNumber);
            tagListBapi.SetValue("CUSTOMER", Quot.Customer.CustomerCode);
            tagListBapi.SetValue("PO_NUMBER", Quot.PrimaryPurchaseOrder.PurchaseOrderNumber);
            tagListBapi.SetValue("PO_DATE", Quot.PrimaryPurchaseOrder.PurchaseOrderDate);
            tagListBapi.SetValue("DO_NUMBER", Quot.DoNumber);
            tagListBapi.SetValue("DO_DATE", Quot.DoDate);
            tagListBapi.SetValue("TERMS_PAYMENT", Quot.PaymentTerms == null ? "" : Quot.PaymentTerms.PaymentTerms);
            tagListBapi.SetValue("FINANCIER_AMT", Quot.DoAmount);
            tagListBapi.SetValue("ADVANCE_AMT", Quot.AdvanceAmount);
            tagListBapi.SetValue("FREIGHT_AMT", Quot.FreightAmount);
            tagListBapi.SetValue("INVOICE_VALUE",Convert.ToString( Quot.InvoiceValue));
            tagListBapi.SetValue("HORSE_POWER", Quot.Equipment.HorsePower);

            tagListBapi.SetValue("MONTH_YR_MANUFACT", Quot.Equipment.ManufacturingMonthYear);
            tagListBapi.SetValue("APPLICATION_USAGE", Quot.Usage == null ? "" : Quot.Usage.MainApplication);
            tagListBapi.SetValue("SHIP_TO_PARTY", Quot.ShipTo.CustomerCode);
            tagListBapi.SetValue("INCOTERMS", Quot.IncoTerms == null ? "" : Quot.IncoTerms.IncoTerms);
            tagListBapi.SetValue("FINANCIER_CODE", Quot.Financier == null ? "" : Quot.Financier.FinancierCode);
            tagListBapi.SetValue("BENIFICIARY_DO", Quot.BenificiaryOfDO);
            tagListBapi.SetValue("SUBVENTION_AMOUNT", Quot.SubventionAmount);
            tagListBapi.SetValue("FOC_KIT", Quot.FocServiceKit);
            tagListBapi.SetValue("RETAIL_CUSTOMER", Quot.RetailCustomer);
            tagListBapi.SetValue("TR_DATE", Convert.ToString( Quot.TRDate));
            tagListBapi.SetValue("CONSLIDATE_INV_PRT",Convert.ToString( Quot.ConsolidationInvoicePrint));

            IRfcTable ITEM = tagListBapi.GetTable("ITEM");
            int ItemNo = 0;
            foreach (PDMS_WebQuotationItem S in Quot.WebQuotationItems)
            {
                ITEM.Append();
                ItemNo = ItemNo + 10;
                ITEM.SetValue("ITEM", ItemNo);
                ITEM.SetValue("MATERIAL", S.Material.MaterialCode);
                ITEM.SetValue("DESCRIPTION", S.Material.MaterialDescription);
                ITEM.SetValue("PLANT", Quot.Equipment.Plant);
                ITEM.SetValue("QUANTITY", S.Qty);
                ITEM.SetValue("PRICE", S.BasicPrice);
                ITEM.SetValue("DISCOUNT", S.Discount1);
                ITEM.SetValue("PRICE_LIST", Quot.ModeOfBilling.DiscountTypeCode);
                // ITEM.SetValue("INSURANCE", S.Date);
                // ITEM.SetValue("FREIGHT", Ticket.ICTicketNumber);
                ITEM.SetValue("MACHINE_SERIAL_N0", Quot.Equipment.EquipmentSerialNo);
                ITEM.SetValue("CHASSIS_NUMBER", Quot.Equipment.ChassisSlNo);
                ITEM.SetValue("ENGINE_NUMBER", Quot.Equipment.EngineSerialNo);
            }

            tagListBapi.Invoke(SAP.RfcDes()); 
            IRfcTable IT_RETURN = tagListBapi.GetTable("IT_RETURN");
            for (int i = 0; i < IT_RETURN.RowCount; i++)
            {
                IT_RETURN.CurrentIndex = i;
                if (IT_RETURN.CurrentRow.GetString("TYPE") == "S")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
