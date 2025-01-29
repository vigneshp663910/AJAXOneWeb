using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Business
{
    public class BDMS_MTTR
    {
        private IDataAccess provider;
        public BDMS_MTTR()
        {
            provider = new ProviderFactory().GetProvider();
        }
         
         public PApiResult GetMTTR(int? DealerID, string CustomerCode, string ICTicketNumber, string ICTicketDateF, string ICTicketDateT, int? ServiceStatusID, string Division, string Excel, int? PageIndex = null, int? PageSize = null)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "ICTicket/GetMTTR?DealerID=" + DealerID + "&CustomerCode=" + CustomerCode + "&ICTicketNumber=" + ICTicketNumber + "&ICTicketDateF=" + ICTicketDateF
                + "&ICTicketDateT=" + ICTicketDateT + "&ServiceStatusID=" + ServiceStatusID + "&Division=" + Division + "&Excel=" + Excel + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public DataSet GetMTTREscalationOnBreakdownCount(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {

            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTREscalationOnBreakdownCount", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet;
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public List<PDMS_MTTR_New> GetMTTRTeamLeader(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {
            List<PDMS_MTTR_New> Ws = new List<PDMS_MTTR_New>();
            PDMS_MTTR_New W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTRTeamLeader", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_MTTR_New();
                            Ws.Add(W);
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["Model"]) };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }
        public List<PDMS_MTTR_New> GetMTTRServiceManagers(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {
            List<PDMS_MTTR_New> Ws = new List<PDMS_MTTR_New>();
            PDMS_MTTR_New W = null;
            try
            {

                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTRServiceManagers", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_MTTR_New();
                            Ws.Add(W);
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["Model"]) };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }
        public List<PDMS_MTTR_New> GetMTTRReginalServiceManagers(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {
            List<PDMS_MTTR_New> Ws = new List<PDMS_MTTR_New>();
            PDMS_MTTR_New W = null;
            try
            {

                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTRReginalServiceManagers", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_MTTR_New();
                            Ws.Add(W);
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["Model"]) };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }
        public List<PDMS_MTTR_New> GetMTTRDM(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {
            List<PDMS_MTTR_New> Ws = new List<PDMS_MTTR_New>();
            PDMS_MTTR_New W = null;
            try
            {

                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTRDM", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_MTTR_New();
                            Ws.Add(W);
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["Model"]) };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }

        public DataSet GetDashCustomerSatisfactionInAfterSalesSupport(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {

            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                return provider.Select("ZDMS_GetDashCustomerSatisfactionInAfterSalesSupport", Params);

            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        
        public List<PDMS_ServiceNote> GetMTTRNote(long? ICTicketID, long? ServiceNoteID, int? NoteTypeID, string NoteType)
        {
            List<PDMS_ServiceNote> ServiceMaterials = new List<PDMS_ServiceNote>();
            try
            {


                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);


                DbParameter[] Params = new DbParameter[1] { ICTicketIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTRNote", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServiceMaterials.Add(new PDMS_ServiceNote()
                            {

                                ICTicketID = Convert.ToInt64(dr["ICTicketID"]),
                                Comments = Convert.ToString(dr["Comments"]),
                                NoteType = new PDMS_NoteType()
                                {
                                    NoteType = Convert.ToString(dr["NoteType"])
                                }
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return ServiceMaterials;
        }

        class MttrEscalationMatrix
        {
            public int EscalationMatrixID { get; set; }
            public string Region { get; set; }
            public int RepresentativeUserID { get; set; }
            public string Subject { get; set; }
            public string Description { get; set; }
            public List<string> ToMailID { get; set; }
            public List<string> CcMailID { get; set; }
            public List<string> BccMailID { get; set; }
            public string EscalationHours { get; set; }
            
        }
        public void SendMailMttrEscalationMatrix()
        {
            List<MttrEscalationMatrix> EMs = GetMttrEscalationMatrix();

            foreach(MttrEscalationMatrix Em in EMs)
            {

                string Message = Body(Em.EscalationHours, Em.Region, Em.Description);
                if (string.IsNullOrEmpty(Message))
                {
                    continue;
                }
                new EmailManager().MailSendByService(Em.ToMailID, Em.Subject, Message, Em.CcMailID, Em.BccMailID); 
            }
        }
        private List<MttrEscalationMatrix> GetMttrEscalationMatrix()
        {
            List<MttrEscalationMatrix> Ws = new List<MttrEscalationMatrix>();
            MttrEscalationMatrix W = null;
            try
            {
                using (DataSet DataSet = provider.Select("ZDMS_GetMttrEscalationMatrix"))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new MttrEscalationMatrix();
                            Ws.Add(W);
                            W.EscalationMatrixID = Convert.ToInt32(dr["EscalationMatrixID"]);
                            W.Region = Convert.ToString(dr["Region"]);
                            W.RepresentativeUserID = Convert.ToInt32(dr["RepresentativeUserID"]);
                            W.Subject = Convert.ToString(dr["Subject"]);
                            W.Description = Convert.ToString(dr["Description"]);
                            W.ToMailID = dr["ToMailID"] == DBNull.Value ? new List<string>() : Convert.ToString(dr["ToMailID"]).Split(',').ToList();
                            W.CcMailID = dr["CcMailID"] == DBNull.Value ? new List<string>() : Convert.ToString(dr["CcMailID"]).Split(',').ToList();
                            W.BccMailID = dr["BccMailID"] == DBNull.Value ? new List<string>() : Convert.ToString(dr["BccMailID"]).Split(',').ToList();
                            W.EscalationHours = Convert.ToString(dr["EscalationHours"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }

        private string Body(string EscalationHours,string DealerCode, string Description)
        {            
            List<PDMS_MTTR_New> MTTRs = new List<PDMS_MTTR_New>();
            string Message = "";

            //if (EscalationHours == "74")
            //    MTTRs = new BDMS_MTTR().GetMTTRDM(null, null, null, UserID);
            //else if (EscalationHours == "48")
            //    MTTRs = new BDMS_MTTR().GetMTTRReginalServiceManagers(null, null, null, UserID);
            //else if (EscalationHours == "24")
            //    MTTRs = new BDMS_MTTR().GetMTTRServiceManagers(null, null, null, UserID);
            //else if (EscalationHours == "24 BasedOnModels")
            //    MTTRs = new BDMS_MTTR().GetMTTRBasedOnModels();

            MTTRs = GetICTicketCrossedMTTR(EscalationHours, DealerCode);
            if (MTTRs.Count == 0)
            {
                return null;
            }         

            string Top = "<!DOCTYPE html><html><head><title></title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"></head>"
                         + "<body><div style=\"max-width: 1500px; margin:auto\"><form><div><p><span>Good Morning!</span></p><p>@@Description<br /></p>"
                          + "<p>-	Under Warranty : @@UnderWarranty <br /></p><p>-	Out of Warranty : @@OutOfWarranty <br /></p>";

            Top = Top.Replace("@@Description", Description);

            string RowTH = "<th style=\"background-color: lightblue;color: white; padding: 10px; text-align:center\">@@RowTH</th>";
            string RowTD = "<td style=\"background-color: #eee;color: black;padding: 3px; text-align:center;border: 1px solid lightblue\">@@RowTD</td>";
            string Header1 = "<table style=\" border: 1px solid lightblue;  border-collapse: collapse; width: 100%;\" ><thead><tr >";

            int i = 0;
            int UnderWarranty = 0;
            int OutOfWarranty = 0;
            string Header = Header1
                + RowTH.Replace("@@RowTH", "Serial Number")
                + RowTH.Replace("@@RowTH", "IC Ticket")
                + RowTH.Replace("@@RowTH", "IC Ticket Date")
                + RowTH.Replace("@@RowTH", "Status")
                + RowTH.Replace("@@RowTH", "Customer")
                + RowTH.Replace("@@RowTH", "Customer Name")
                + RowTH.Replace("@@RowTH", "Contact Name")
                + RowTH.Replace("@@RowTH", "Contact Number")
                + RowTH.Replace("@@RowTH", "Dealer")
                + RowTH.Replace("@@RowTH", "Dealer Name")
                + RowTH.Replace("@@RowTH", "Model")
                + RowTH.Replace("@@RowTH", "Serial No")
                + RowTH.Replace("@@RowTH", "Warranty Status")
                + RowTH.Replace("@@RowTH", "State")
                + RowTH.Replace("@@RowTH", "Respective TSM Name")
                + "</tr></thead><tbody>";
            string Row = "";
            foreach (PDMS_MTTR_New MTTR in MTTRs)
            {
                i = i + 1;
                if (MTTR.ICTicket.IsWarranty == true)
                {
                    UnderWarranty = UnderWarranty + 1;
                }
                else
                {
                    OutOfWarranty = OutOfWarranty + 1;
                }
                Row = Row + " <tr>"
                    + RowTD.Replace("@@RowTD", i.ToString())
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.ICTicketNumber)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.ICTicketDate.ToShortDateString())
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.ServiceStatus.ServiceStatus)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Customer.CustomerCode)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Customer.CustomerName)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.ContactPerson)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.PresentContactNumber)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Dealer.DealerCode)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Dealer.DealerName)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Equipment.EquipmentModel.Model)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Equipment.EquipmentSerialNo)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.IsWarranty == true ? "Yes" : "No")
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Address.State.State)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Dealer.TL.ContactName)
                    + "</tr>";
            }
            string Bottom = "</tbody></table><p>Thank You !</p></div></form></div></body></html>";

            Top = Top.Replace("@@UnderWarranty", UnderWarranty.ToString());
            Top = Top.Replace("@@OutOfWarranty", OutOfWarranty.ToString());
            Message = Top + Header + Row + Bottom;

            return Message;
        }

        public List<PDMS_MTTR_New> GetMTTRBasedOnModels()
        {
            List<PDMS_MTTR_New> Ws = new List<PDMS_MTTR_New>();
            PDMS_MTTR_New W = null;
            try
            {
                //    DbParameter ModelP = provider.CreateParameter("Model", Model, DbType.Int32);
                //    DbParameter[] Params = new DbParameter[1] { ModelP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTRBasedOnModels"))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_MTTR_New();
                            Ws.Add(W);
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["Model"]) };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }
        public List<PDMS_MTTR_New> GetICTicketCrossedMTTR(string EscalationHours, string DealerCode)
        {
            List<PDMS_MTTR_New> Ws = new List<PDMS_MTTR_New>();
            PDMS_MTTR_New W = null;
            try
            {
                 DbParameter EscalationHoursP = provider.CreateParameter("EscalationHours", EscalationHours, DbType.String);
                DbParameter DealerCodeP = provider.CreateParameter("DealerCode", DealerCode, DbType.String);

                DbParameter[] Params = new DbParameter[2] { EscalationHoursP, DealerCodeP };
                using (DataSet DataSet = provider.Select("GetICTicketCrossedMTTR", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_MTTR_New();
                            Ws.Add(W);
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer()
                            {
                                DealerCode = Convert.ToString(dr["DealerCode"]),
                                DealerName = Convert.ToString(dr["DealerName"]),
                                TL = new PUser() { ContactName = Convert.ToString(dr["TLContactName"]) }
                            };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["Model"]) };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.ICTicket.Address = new PDMS_Address()
                            {
                                State = new PDMS_State() { State = Convert.ToString(dr["State"]) }
                            }; 
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }

    }
}
