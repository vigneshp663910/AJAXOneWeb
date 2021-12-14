using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class SDMS_WarrantyClaimAnnexure
    {
        public Boolean UpdateICTicketRequestedDateToSAP(PDMS_WarrantyClaimAnnexureHeader Annexure)
        {
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZBAPI_ANNEXURE_UPLOAD");
            IRfcTable IS_REQ_DATE = tagListBapi.GetTable("ANNEXURE_VALUES");
            IS_REQ_DATE.Append();
            IS_REQ_DATE.SetValue("ZTICKET", Annexure.AnnexureItem.ICTicketID);
            IS_REQ_DATE.SetValue("ZANNEXNUM", Annexure.AnnexureNumber);
            IS_REQ_DATE.SetValue("MATNR", Annexure.AnnexureItem.Material);
            IS_REQ_DATE.SetValue("SERNR", Annexure.AnnexureItem.MachineSerialNumber);
            IS_REQ_DATE.SetValue("DEALER", Annexure.Dealer.DealerCode);
            IS_REQ_DATE.SetValue("DEALER_NAME", Annexure.Dealer.DealerName);
            IS_REQ_DATE.SetValue("ZTKTDATE", Annexure.AnnexureItem.ICTicketDate);
            IS_REQ_DATE.SetValue("ZRESTDATE", Annexure.AnnexureItem.RestoreDate);
            IS_REQ_DATE.SetValue("ZAPRDATE",  Annexure.AnnexureItem.ApprovedDate );
            IS_REQ_DATE.SetValue("ZANXDATE", Annexure.CreatedOn);
            IS_REQ_DATE.SetValue("GJAHR", Annexure.Year);
            IS_REQ_DATE.SetValue("ZPRDFRM", Annexure.PeriodFrom);
            IS_REQ_DATE.SetValue("ZPRDTO", Annexure.PeriodTo);
            IS_REQ_DATE.SetValue("ZINVOICE", Annexure.InvoiceNumber);
            IS_REQ_DATE.SetValue("KUNNR", Annexure.AnnexureItem.CustomerCode);
            IS_REQ_DATE.SetValue("CUST_NAME", Annexure.AnnexureItem.CustomerName);
            IS_REQ_DATE.SetValue("MODEL", Annexure.AnnexureItem.Model);
            IS_REQ_DATE.SetValue("HMR", Annexure.AnnexureItem.HMR);
            IS_REQ_DATE.SetValue("HSN_SAC", Annexure.AnnexureItem.HSNCode);
            IS_REQ_DATE.SetValue("DMBTR", Annexure.AnnexureItem.ClaimAmount);
            IS_REQ_DATE.SetValue("ZAPRAMT", Annexure.AnnexureItem.ApprovedAmount);
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
