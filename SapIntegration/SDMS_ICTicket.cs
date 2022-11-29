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
        public List<PDMS_ICTicket> GetICTicketFromSAP()
        {
            List<PDMS_ICTicket> ICTickets = new List<PDMS_ICTicket>();
            PDMS_ICTicket ICTicket = null;
            IRfcFunction tagListBapi = SSAP_CRM.CRM_RfcRep().CreateFunction("ZFM_GET_IC_TICKET");
            tagListBapi.Invoke(SSAP_CRM.CRM_RfcDes());
            IRfcTable tagTable = tagListBapi.GetTable("ICTICKET");
            for (int i = 0; i < tagTable.RowCount; i++)
            {
                try
                {
                    tagTable.CurrentIndex = i;
                    ICTicket = new PDMS_ICTicket();
                    ICTicket.GUID = tagTable.GetString("GUID");
                    ICTicket.ICTicketNumber = tagTable.GetString("F_IC_TICKET_ID");
                    string CallDate = tagTable.GetString("CREATED_AT");
                   // DateTime TicketDate = Convert.ToDateTime(CallDate.Substring(6, 2) + "/" + CallDate.Substring(4, 2) + "/" + CallDate.Substring(0, 4) + " " + CallDate.Substring(8, 2) + ":" + CallDate.Substring(10, 2) + ":" + CallDate.Substring(12, 2));
                    DateTime TicketDate = Convert.ToDateTime(tagTable.GetString("POSTING_DATE") + " " + tagTable.GetString("POSTING_TIME"));
                     
                    ICTicket.ICTicketDate = TicketDate;
                    ICTicket.ServiceRecord = tagTable.GetString("R_EXT_ID");
                    ICTicket.Dealer = new PDMS_Dealer() { DealerCode = tagTable.GetString("F_FRANCHISEE_ID") };

                    ICTicket.PresentContactNumber = tagTable.GetString("PRESENTCONTACTNUMBER");
                    ICTicket.ContactPerson = tagTable.GetString("CONTACTPERSON");
                    ICTicket.ComplaintCode = tagTable.GetString("COMPLAINTCODE");
                    ICTicket.ComplaintDescription = tagTable.GetString("COMPLAINTDESCRIPTION");
                    ICTicket.Information = tagTable.GetString("INFORMATION");
                    ICTicket.ReasonForCloser = tagTable.GetString("REASONFORCLOSE");
                    ICTicket.OldICTicketNumber = tagTable.GetString("OLDICTICKETNUMBER");
                    ICTicket.ServicePriority = new PDMS_ServicePriority() { ServicePriority = tagTable.GetString("R_PRIORITY_CLASS") };
                    DateTime Rdate = Convert.ToDateTime(tagTable.GetString("REQUESTED_DATE") + " " + tagTable.GetString("REQUESTED_TIME"));
                    ICTicket.RequestedDate = Rdate;

                    ICTicket.Customer = new PDMS_Customer() { CustomerCode = tagTable.GetString("F_CUST_ID"), CustomerName = tagTable.GetString("CUSTOMERNAME") };
                    ICTicket.Address = new PDMS_Address()
                    {
                        Country = new PDMS_Country(),
                        State = new PDMS_State() { State = tagTable.GetString("R_STATE_DESC"), StateSAP = tagTable.GetString("R_STATE") },
                        District = new PDMS_District() { District = tagTable.GetString("R_DISTRICT_DESC"), DistrictSAP = tagTable.GetString("R_DISTRICT") }
                    };
                    ICTicket.Equipment = new PDMS_EquipmentHeader()
                    {
                        EngineSerialNo = tagTable.GetString("R_EQUIPMENT_SER_NO"),
                        WarrantyExpiryDate = Convert.ToDateTime(tagTable.GetString("R_DATE_WARR_EXPIRY")),
                        HMRDate = string.IsNullOrEmpty(tagTable.GetString("CURRENTHMRDATE").Replace("0000-00-00","")) ? (DateTime?)null : Convert.ToDateTime(tagTable.GetString("CURRENTHMRDATE")),
                        HMRValue = string.IsNullOrEmpty(tagTable.GetString("CURRENTHMRVALUE")) ? (Int32?)null : Convert.ToInt32(tagTable.GetString("CURRENTHMRVALUE")),
                        CounterObjectID = tagTable.GetString("COUNTEROBJECTID")
                    };
                    ICTickets.Add(ICTicket);
                }
                catch(Exception e1)
                {

                }
            }
            return ICTickets;
        }

        public void GetICTicketToSAP(string GUID, string IcTicket, string Status)
        { 
            IRfcFunction tagListBapi = SSAP_CRM.CRM_RfcRep().CreateFunction("ZFM_GET_IC_TICKET_STATUS");
            tagListBapi.SetValue("P_GUID", GUID);
            tagListBapi.SetValue("P_F_IC_TICKET_ID", IcTicket);
            tagListBapi.SetValue("P_STATUS", Status);
            tagListBapi.Invoke(SSAP_CRM.CRM_RfcDes());  
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

        public void UpdateICTicketRequestedDateToSAP(string ICTicket, DateTime Date)
        {

            IRfcFunction tagListBapi = SSAP_CRM.CRM_RfcRep().CreateFunction("ZCRM_SER_ORDER_REQ_DATE_UPD");
            IRfcStructure IS_REQ_DATE = tagListBapi.GetStructure("IS_REQ_DATE");
            IS_REQ_DATE.SetValue("IC_TICKET_NO", ICTicket);
            IS_REQ_DATE.SetValue("REQUEST_DATE", Date.ToShortDateString());
            IS_REQ_DATE.SetValue("REQUEST_TIME", Date.ToShortTimeString());
            tagListBapi.Invoke(SSAP_CRM.CRM_RfcDes());
        }
        
        public List<PDMS_ICTicket> ZBAPI_GET_ICTICKETNO_BY_DATE(DateTime ICTicketDateF, DateTime ICTicketDateT)
        {
            List<PDMS_ICTicket> ICTicketFromSAP = new List<PDMS_ICTicket>();
            PDMS_ICTicket ICTicket = null;
            IRfcFunction tagListBapi = SSAP_CRM.CRM_RfcRep().CreateFunction("ZBAPI_GET_ICTICKETNO_BY_DATE");

            string DateF = ICTicketDateF.Year.ToString() + ICTicketDateF.Month.ToString("00") + ICTicketDateF.Day.ToString("00") + ICTicketDateF.Hour.ToString("00") + ICTicketDateF.Minute.ToString("00") + ICTicketDateF.Second.ToString("00");


            string DateT = ICTicketDateT.Year.ToString() + ICTicketDateT.Month.ToString("00") + ICTicketDateT.Day.ToString("00") + ICTicketDateT.Hour.ToString("00") + ICTicketDateT.Minute.ToString("00") + ICTicketDateT.Second.ToString("00"); 

            tagListBapi.SetValue("POSTING_DATE_F", DateF);
            tagListBapi.SetValue("POSTING_DATE_T", DateT);
            tagListBapi.Invoke(SSAP_CRM.CRM_RfcDes());
            IRfcTable tagTable = tagListBapi.GetTable("IT_ORDER");
            for (int i = 0; i < tagTable.RowCount; i++)
            {
                tagTable.CurrentIndex = i;
                ICTicket = new PDMS_ICTicket();
                ICTicketFromSAP.Add(ICTicket);
                ICTicket.ICTicketNumber = tagTable.GetString("OBJECT_ID");
                string dt = tagTable.GetString("CREATED_AT");
                ICTicket.ICTicketDate = Convert.ToDateTime(dt.Substring(6, 2) + "/" + dt.Substring(4, 2) + "/" + dt.Substring(0, 4) + " " + dt.Substring(8, 2) + ":" + dt.Substring(10, 2) + ":" + dt.Substring(12, 2));

                ICTicket.Dealer = new PDMS_Dealer() { DealerCode = tagTable.GetString("DESCRIPTION") };
            }
            return ICTicketFromSAP;
        }
    }
}
