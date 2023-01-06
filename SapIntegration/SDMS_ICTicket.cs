using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class SDMS_ICTicket
    {
        public List<PDMS_ICTicket> getCustomerAddress(string CustomerCode)
        {
            List<PDMS_ICTicket> ICTickets = new List<PDMS_ICTicket>();
            PDMS_ICTicket ICTicket = null;
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZBAPI_GET_CUST_ADD_FOR_DMSR");
            tagListBapi.SetValue("V_KUNNR", CustomerCode.PadLeft(10, '0'));
            tagListBapi.Invoke(SAP.RfcDes());
            IRfcTable tagTable = tagListBapi.GetTable("It_MARA");
            for (int i = 0; i < tagTable.RowCount; i++)
            {
                ICTicket = new PDMS_ICTicket();
                ICTickets.Add(ICTicket);
                ICTicket.ICTicketNumber = tagListBapi.GetString("ICTicketNumber");
                ICTicket.ICTicketDate = Convert.ToDateTime(tagListBapi.GetString("ADD1"));
                ICTicket.PresentContactNumber = tagListBapi.GetString("PresentContactNumber");
                ICTicket.ContactPerson = tagListBapi.GetString("ContactPerson");

                ICTicket.ComplaintCode = tagListBapi.GetString("ComplaintCode");
                ICTicket.ComplaintDescription = tagListBapi.GetString("ComplaintDescription");
                ICTicket.Information = tagListBapi.GetString("Information");
                ICTicket.ReasonForCloser = tagListBapi.GetString("ReasonForCloser");
                ICTicket.OldICTicketNumber = tagListBapi.GetString("OldICTicketNumber");
                ICTicket.ContactPerson = tagListBapi.GetString("ContactPerson");
                ICTicket.ContactPerson = tagListBapi.GetString("ContactPerson");
                ICTicket.ContactPerson = tagListBapi.GetString("ContactPerson");
                ICTicket.ContactPerson = tagListBapi.GetString("ContactPerson");
                ICTicket.ContactPerson = tagListBapi.GetString("ContactPerson");
                ICTicket.ContactPerson = tagListBapi.GetString("ContactPerson");
                ICTicket.ContactPerson = tagListBapi.GetString("ContactPerson");
                ICTicket.ContactPerson = tagListBapi.GetString("ContactPerson");

            }
            return ICTickets;
        }
       
         
        public List<string> getModelByProductID(string ProductID)
        {
            long n;
            if (long.TryParse(ProductID, out n))
            {
                ProductID = ProductID.PadLeft(18, '0');
            }
            List<string> Model = new List<string>();
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZBAPI_GET_MODEL_BY_PRODUCT");
            tagListBapi.SetValue("PRODUCT", ProductID);
            tagListBapi.Invoke(SAP.RfcDes());
            Model.Add(Convert.ToString(tagListBapi.GetValue("P_MATKL")));
            Model.Add(Convert.ToString(tagListBapi.GetValue("P_SPART")));
            Model.Add(Convert.ToString(tagListBapi.GetValue("P_MAKTX")));
            Model.Add(Convert.ToString(tagListBapi.GetValue("P_SERNR")));
            return Model;
        }

        //public void UpdateICTicketRequestedDateToSAP(string ICTicket, DateTime Date)
        //{

        //    IRfcFunction tagListBapi = SSAP_CRM.CRM_RfcRep().CreateFunction("ZCRM_SER_ORDER_REQ_DATE_UPD");
        //    IRfcStructure IS_REQ_DATE = tagListBapi.GetStructure("IS_REQ_DATE");
        //    IS_REQ_DATE.SetValue("IC_TICKET_NO", ICTicket);
        //    IS_REQ_DATE.SetValue("REQUEST_DATE", Date.ToShortDateString());
        //    IS_REQ_DATE.SetValue("REQUEST_TIME", Date.ToShortTimeString());
        //    tagListBapi.Invoke(SSAP_CRM.CRM_RfcDes());
        //}
         
    }
}
