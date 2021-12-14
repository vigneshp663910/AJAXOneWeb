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
        public void UpdateICTicketToSAP(PDMS_ICTicket Ticket, List<PDMS_ServiceCharge> Service, List<PDMS_ServiceMaterial> Material, List<PDMS_ServiceNote> NoteType, List<PAttachedFile> AFile)
        {

            IRfcFunction tagListBapi = SSAP_CRM.CRM_RfcRep().CreateFunction("ZFM_SERVICE_CONFIRM_UPDATE");
            //       IRfcFunction tagListBapi = SAP_CRM.RfcRep().CreateFunction("ZBAPI_GET_ICTICKET2");
            IRfcStructure IS_SC_HEADER = tagListBapi.GetStructure("IS_SC_HEADER");
            //  IS_SC_HEADER.SetValue("P_SC_ID", Ticket.ICTicketNumber);
            //  IS_SC_HEADER.SetValue("F_CRM_SC_ID", Ticket.ICTicketNumber);
            IS_SC_HEADER.SetValue("F_CRM_SERVICE_ID", Ticket.ServiceOrderNumber);
            //  IS_SC_HEADER.SetValue("F_CURRENCY", Ticket.ICTicketNumber);
            IS_SC_HEADER.SetValue("F_CUST_ID", Ticket.Customer.CustomerCode);
            // IS_SC_HEADER.SetValue("F_EQUIPMENT_ID", Ticket.ICTicketNumber);
            //  IS_SC_HEADER.SetValue("F_FRANCHISE_ID", Ticket.ICTicketNumber);
            //  IS_SC_HEADER.SetValue("F_SR_ID", Ticket.ICTicketNumber);
            IS_SC_HEADER.SetValue("R_ACTUAL_CLOSURE_DATE", Ticket.RestoreDate);
            // IS_SC_HEADER.SetValue("R_ADDN_DISCOUNT", Ticket.ICTicketNumber);
            IS_SC_HEADER.SetValue("R_CATEGORY1", Ticket.Category1.Category1Code);
            IS_SC_HEADER.SetValue("R_CATEGORY2", Ticket.Category2.Category2Code);
            IS_SC_HEADER.SetValue("R_CATEGORY3", Ticket.Category3.Category3Code);
            IS_SC_HEADER.SetValue("R_CATEGORY4", Ticket.Category4.Category4Code);
            //  IS_SC_HEADER.SetValue("R_CLAIM_STATUS", Ticket.ICTicketNumber);


            //   IS_SC_HEADER.SetValue("R_CLOSURE_DATE", Ticket.ICTicketNumber); //-----------------
            if (Ticket.RestoreDate != null)
            {
                IS_SC_HEADER.SetValue("R_DATE_OF_FIRST_RES", Ticket.RestoreDate);
            }
            IS_SC_HEADER.SetValue("R_DATE_OF_REQ", Ticket.RequestedDate);
            //  IS_SC_HEADER.SetValue("R_DISCOUNT_AMT", Ticket.ICTicketNumber);
            //  IS_SC_HEADER.SetValue("R_IN_AMC", Ticket.ICTicketNumber);
            //  IS_SC_HEADER.SetValue("R_IN_WARRANTY", Ticket.ICTicketNumber);
            //  IS_SC_HEADER.SetValue("R_NET_AMOUNT", Ticket.ICTicketNumber);
            IS_SC_HEADER.SetValue("R_PRIORITY", Ticket.ServicePriority.ServicePriority);
            IS_SC_HEADER.SetValue("R_PRIORITY_CLASS", Ticket.ServicePriority.ServicePriority);
            //  IS_SC_HEADER.SetValue("R_TAX_AMT", Ticket.ICTicketNumber);
            //  IS_SC_HEADER.SetValue("R_TOTAL_AMT", Ticket.ICTicketNumber);

            string STATUS = "";
            if (Ticket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Open)
            {
                STATUS = "OPEN";
            }
            else if (Ticket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Declined)
            {
                STATUS = "DECLINED";
            }
            else if (Ticket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Restored)
            {
                // STATUS = "COMPLETED";
                STATUS = "CONFIRMED";
            }
            else
            {
                STATUS = "IN PROCESS";
            }
            IS_SC_HEADER.SetValue("S_STATUS", STATUS);
            IS_SC_HEADER.SetValue("R_LOCATION", Ticket.Location);
            IS_SC_HEADER.SetValue("R_ANSWER_ID", "a1");

            string R_ANSWER_KEY = "";
            if (Ticket.CustomerSatisfactionLevel != null)
            {
                if (Ticket.CustomerSatisfactionLevel.CustomerSatisfactionLevelID == 1)
                    R_ANSWER_KEY = "id_5cf3fc210af61ee69181e708a36679c3";
                else if (Ticket.CustomerSatisfactionLevel.CustomerSatisfactionLevelID == 2)
                    R_ANSWER_KEY = "id_5cf3fc210af61ee69181ed8c9d5619c3";
                else if (Ticket.CustomerSatisfactionLevel.CustomerSatisfactionLevelID == 3)
                     R_ANSWER_KEY = "id_5cf3fc210af61ee69181ee168136b9c3";
                else if (Ticket.CustomerSatisfactionLevel.CustomerSatisfactionLevelID == 4)
                     R_ANSWER_KEY = "id_5cf3fc210af61ee69181ee6f55f1d9c3";
            }
            IS_SC_HEADER.SetValue("R_ANSWER_KEY", R_ANSWER_KEY);
            IS_SC_HEADER.SetValue("R_QUESTION_ID", "q1");
            if (Ticket.SubApplication != null)
                IS_SC_HEADER.SetValue("R_APPLICATION", Ticket.SubApplication.SubApplication);



            IRfcStructure IS_COUNTER_READING = tagListBapi.GetStructure("IS_COUNTER_READING");
            //  IS_COUNTER_READING.SetValue("P_COUNTER_ID", Ticket.ICTicketNumber);
            //   IS_COUNTER_READING.SetValue("F_COUNTER_REF_ID", Ticket.ICTicketNumber);
            IS_COUNTER_READING.SetValue("R_COUNTER_END", Ticket.CurrentHMRValue);
            IS_COUNTER_READING.SetValue("R_COUNTER_OBJ", Ticket.Equipment.CounterObjectID);
            IS_COUNTER_READING.SetValue("R_COUNTER_READ_DATE", Ticket.CurrentHMRDate);
            //  IS_COUNTER_READING.SetValue("R_COUNTER_START", Ticket.ICTicketNumber);
            //  IS_COUNTER_READING.SetValue("R_COUNTER_TYPE", Ticket.ICTicketNumber);
            //   IS_COUNTER_READING.SetValue("R_COUNTER_UNIT", Ticket.ICTicketNumber);
            IS_COUNTER_READING.SetValue("R_DESC", "");



            IRfcTable IT_CHARGE_ITEMS = tagListBapi.GetTable("IT_CHARGE_ITEMS");
            foreach (PDMS_ServiceCharge S in Service)
            {
                IT_CHARGE_ITEMS.Append();
                // IT_CHARGE_ITEMS.SetValue("P_SC_ID", Ticket.ICTicketNumber);
                IT_CHARGE_ITEMS.SetValue("P_SC_TECH_ID", S.Item);
                //  IT_CHARGE_ITEMS.SetValue("F_CURRENCY", Ticket.ICTicketNumber);
                IT_CHARGE_ITEMS.SetValue("F_PART_ID", S.Material.MaterialCode);
                IT_CHARGE_ITEMS.SetValue("F_TECHNICIAN_ID", Ticket.Technician.UserName);
                IT_CHARGE_ITEMS.SetValue("R_AMOUNT", S.BasePrice + S.IGSTValue + S.SGSTValue + S.SGSTValue);
                IT_CHARGE_ITEMS.SetValue("R_DISCOUNT_AMT", S.Discount);
                //  IT_CHARGE_ITEMS.SetValue("R_ITEM_CATG", Ticket.ICTicketNumber);
                IT_CHARGE_ITEMS.SetValue("R_START_DATE", S.Date);
                //  IT_CHARGE_ITEMS.SetValue("R_TAX_AMT", Ticket.ICTicketNumber);
                //  IT_CHARGE_ITEMS.SetValue("R_TECHNICIAN_CATG", Ticket.ICTicketNumber);
                // IT_CHARGE_ITEMS.SetValue("R_TOTAL_AMT", Ticket.ICTicketNumber);

             //   IT_CHARGE_ITEMS.SetValue("R_TOTAL_HRS_WORKED", S.WorkedHours); //------------

                IT_CHARGE_ITEMS.SetValue("T_ACTION", S.IsDeleted ? "U_D" : ""); 
               //   IT_CHARGE_ITEMS.SetValue("S_STATUS", S.de Ticket.ICTicketNumber); //------------
            }
            IRfcTable IT_PARTS_ITEMS = tagListBapi.GetTable("IT_PARTS_ITEMS");
            foreach (PDMS_ServiceMaterial M in Material)
            {
                IT_PARTS_ITEMS.Append();
                IT_PARTS_ITEMS.SetValue("P_SC_PARTS_ID", M.Item);
                // IT_PARTS_ITEMS.SetValue("P_SC_ID", Ticket.ICTicketNumber);
                //  IT_PARTS_ITEMS.SetValue("F_CURRENCY", Ticket.ICTicketNumber);
                IT_PARTS_ITEMS.SetValue("F_PART_ID", M.Material.MaterialCode);
                IT_PARTS_ITEMS.SetValue("F_PART_SERIAL_NO", M.Material.MaterialSerialNumber);
                IT_PARTS_ITEMS.SetValue("R_AMOUNT", M.BasePrice + M.IGSTValue + M.SGSTValue + M.SGSTValue);  
                // IT_PARTS_ITEMS.SetValue("R_CLAIM_STATUS", Ticket.ICTicketNumber);
                // IT_PARTS_ITEMS.SetValue("R_CLAIM_STATUS_COMMENTS", Ticket.ICTicketNumber);
                // IT_PARTS_ITEMS.SetValue("R_CLAIM_STATUS_REASON", Ticket.ICTicketNumber);
                // IT_PARTS_ITEMS.SetValue("R_DATE", "");
                IT_PARTS_ITEMS.SetValue("R_DEFECTIVE_PART", M.Material.MaterialCode);
               // IT_PARTS_ITEMS.SetValue("R_DEFECTIVE_SR_NO", Ticket.ICTicketNumber); //---
                IT_PARTS_ITEMS.SetValue("R_DISCOUNT_AMT", M.Discount); 
                //  IT_PARTS_ITEMS.SetValue("R_IS_CUST_STOCK", Ticket.ICTicketNumber);
                // IT_PARTS_ITEMS.SetValue("R_ITEM_CATEGORY", Ticket.ICTicketNumber);
                IT_PARTS_ITEMS.SetValue("R_QUANTITY", M.Qty);
                //  IT_PARTS_ITEMS.SetValue("R_TAX_AMT", Ticket.ICTicketNumber);
                // IT_PARTS_ITEMS.SetValue("R_TOTAL_AMT", Ticket.ICTicketNumber);
                 IT_PARTS_ITEMS.SetValue("T_ACTION", M.IsDeleted ? "U_D" : "");   
            }
            IRfcTable IT_SO_NOTES = tagListBapi.GetTable("IT_SO_NOTES");
            foreach (PDMS_ServiceNote Note in NoteType)
            {
                IT_SO_NOTES.Append();
                //  IT_SO_NOTES.SetValue("P_SC_NOTE_ID", Note);
                // IT_SO_NOTES.SetValue("F_SC_ID", Note);
                // IT_SO_NOTES.SetValue("IS_READ_ONLY", Note);
                IT_SO_NOTES.SetValue("R_COMMENTS", Note.Comments);
                //  IT_SO_NOTES.SetValue("R_NOTE_GRP_KEY", Note);
                IT_SO_NOTES.SetValue("R_NOTE_TYPE", Note.NoteType.NoteCode);
                //  IT_SO_NOTES.SetValue("T_ACTION", Note);
            }
            IRfcTable IT_ATTACHEMENT = tagListBapi.GetTable("IT_ATTACHEMENT");

            foreach (PAttachedFile AF in AFile)
            {
                IT_ATTACHEMENT.Append();
                IT_ATTACHEMENT.SetValue("FILE_SIZE", AF.FileSize);
                IT_ATTACHEMENT.SetValue("FILE_NAME", AF.FileName);
                IT_ATTACHEMENT.SetValue("MIMETYPE", AF.FileType);
                // IT_ATTACHEMENT.SetValue("PROPERTY", Ticket.ICTicketNumber);
                IRfcTable IT_BINSTRING_CON = IT_ATTACHEMENT.GetTable("IT_BINSTRING_CON");
                string AttachedFile = Convert.ToString(AF.AttachedFile) ;
                string FileLent = "";
                for (int i = 0; i < AF.AttachedFile.Length; i++)
                {
                    IT_BINSTRING_CON.Append();
                    IT_BINSTRING_CON.SetValue(0, AF.AttachedFile[i]);

                    //FileLent = FileLent + AF.AttachedFile[i];
                    //if (FileLent.Length == 1022)
                    //{
                    //    IT_BINSTRING_CON.Append();
                    //    IT_BINSTRING_CON.SetValue("SDOK_SDATX", FileLent);
                    //    FileLent = "";
                    //}
                }
            }
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
