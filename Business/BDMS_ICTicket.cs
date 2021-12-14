using DataAccess;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Script.Serialization;


namespace Business
{
    public class BDMS_ICTicket
    {
        private IDataAccess provider;
        public BDMS_ICTicket()
        {
            try
            {
                provider = new ProviderFactory().GetProvider();
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_ICTicket", "provider : " + e1.Message, null);
            }
        }
        public int IntegrationICTicket()
        {
            string folderPath = ConfigurationManager.AppSettings["ICTicketPath"];
            string[] ICTicketFiles = Directory.GetFiles(folderPath, "*dsprr_psr_hdr*");
            foreach (string file in ICTicketFiles)
            {
                File.Delete(file); 
            }
            string[] EquipmentFiles = Directory.GetFiles(folderPath, "*cust_equipment*");
            foreach (string file in EquipmentFiles)
            {
                File.Delete(file);
            }
            string[] RepControFiles = Directory.GetFiles(folderPath, "*doohr_rep_contro*");
            foreach (string file in RepControFiles)
            {
                File.Delete(file);
            }

            string[] CustomerFiles = Directory.GetFiles(folderPath, "*doohr_bp*");

            
            ICTicketFiles = Directory.GetFiles(folderPath, "*dsprr_psr_hdr*");
            new BDMS_Customer().IntegrationCustomer(CustomerFiles);
            //   new BDMS_Equipment().IntegrationEquipment(EquipmentFiles);

            TraceLogger.Log(DateTime.Now);
            PDMS_ICTicketJSON Customers = new PDMS_ICTicketJSON();
            string fileT = "";
            try
            { 
                foreach (string file in ICTicketFiles)
                {
                    fileT = file;
                    string json = File.ReadAllText(file);
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    Customers = ser.Deserialize<PDMS_ICTicketJSON>(json);
                    try
                    {
                        foreach (PDMS_ICTicketResultsJSON Customer in Customers.results)
                        {
                            string CallDate = Customer.f_call_login_date;
                            string ReqDate = Customer.r_date_of_req;
                            //  DateTime TicketDate = Convert.ToDateTime(CallDate.Substring(8, 2) + "/" + CallDate.Substring(5, 2) + "/" + CallDate.Substring(0, 4) + " " + CallDate.Substring(10, 2) + ":" + CallDate.Substring(12, 2) + "/" + CallDate.Substring(14, 2));

                            DateTime TicketDate = Convert.ToDateTime(CallDate.Substring(6, 2) + "/" + CallDate.Substring(4, 2) + "/" + CallDate.Substring(0, 4) + " " + CallDate.Substring(8, 2) + ":" + CallDate.Substring(10, 2) + ":" + CallDate.Substring(12, 2));
                            DateTime RequestedDateT = Convert.ToDateTime(ReqDate.Substring(6, 2) + "/" + ReqDate.Substring(4, 2) + "/" + ReqDate.Substring(0, 4) + " " + ReqDate.Substring(8, 2) + ":" + ReqDate.Substring(10, 2) + ":" + ReqDate.Substring(12, 2));

                            DbParameter ICTicketNumber = provider.CreateParameter("ICTicketNumber", Customer.f_ic_ticket_id, DbType.String);
                            DbParameter ICTicketDate = provider.CreateParameter("ICTicketDate", TicketDate, DbType.DateTime);
                            DbParameter ServiceRecord = provider.CreateParameter("ServiceRecord", Customer.r_ext_id, DbType.String);
                            DbParameter DealerCode = provider.CreateParameter("DealerCode", Customer.f_franchisee_id, DbType.String);
                            DbParameter CustomerCode = provider.CreateParameter("CustomerCode", Customer.f_cust_id, DbType.String);
                            DbParameter EquipmentSerNo = provider.CreateParameter("EquipmentSerNo", Customer.r_equipment_ser_no, DbType.String);
                            new BDMS_Equipment().IntegrationEquipmentByBapi(Customer.r_equipment_ser_no);
                            // DbParameter ModelDescription = provider.CreateParameter("ModelDescription", Customer.f_equipment_id, DbType.String);
                            DbParameter ModelDescription = provider.CreateParameter("ModelDescription", "", DbType.String);

                            DbParameter PresentContactNumber = provider.CreateParameter("PresentContactNumber", "", DbType.String);
                            DbParameter ContactPerson = provider.CreateParameter("ContactPerson", "", DbType.String);
                            DbParameter ComplaintCode = provider.CreateParameter("ComplaintCode", "", DbType.String);
                            DbParameter ComplaintDescription = provider.CreateParameter("ComplaintDescription", "", DbType.String);
                            DbParameter Information = provider.CreateParameter("Information", "", DbType.String);
                            DbParameter ReasonForCloser = provider.CreateParameter("ReasonForCloser", "", DbType.String);
                            DbParameter OldICTicketNumber = provider.CreateParameter("OldICTicketNumber", "", DbType.String);
                            foreach (PDMS_dsprr_psr_hdr_notesJSON Note in Customer.dsprr_psr_hdr_notes)
                            {
                                switch (Note.r_note_type)
                                {
                                    case "ZCF1":
                                        PresentContactNumber = provider.CreateParameter("PresentContactNumber", Note.r_comments, DbType.String);
                                        break;
                                    case "ZCF2":
                                        ContactPerson = provider.CreateParameter("ContactPerson", Note.r_comments, DbType.String);
                                        break;
                                    case "ZCF3":
                                        ComplaintCode = provider.CreateParameter("ComplaintCode", Note.r_comments, DbType.String);
                                        break;
                                    case "ZCF4":
                                        ComplaintDescription = provider.CreateParameter("ComplaintDescription", Note.r_comments, DbType.String);
                                        break;
                                    case "ZCF5":
                                        Information = provider.CreateParameter("Information", Note.r_comments, DbType.String);
                                        break;
                                    case "ZCF6":
                                        ReasonForCloser = provider.CreateParameter("ReasonForCloser", Note.r_comments, DbType.String);
                                        break;
                                    case "ZCF7":
                                        OldICTicketNumber = provider.CreateParameter("OldICTicketNumber", Note.r_comments, DbType.String);
                                        break;
                                }
                            }
                            //DbParameter ServiceType = provider.CreateParameter("ServiceType", "", DbType.String);
                            DbParameter ServicePriorityID = provider.CreateParameter("ServicePriorityID", Customer.r_priority_class, DbType.String);
                            //DbParameter Warranty = provider.CreateParameter("Warranty", Customer.r_date_warr_expiry, DbType.String);
                            DbParameter WarrantyExpiryDate = provider.CreateParameter("WarrantyExpiryDate", Customer.r_date_warr_expiry, DbType.String);
                            DbParameter RequestedDate = provider.CreateParameter("RequestedDate", RequestedDateT, DbType.DateTime);
                            //DbParameter ServiceRecord = provider.CreateParameter("ServiceRecord", Customer.s, DbType.String);
                            DbParameter Country = provider.CreateParameter("Country", Customer.r_country, DbType.String);
                            DbParameter State = provider.CreateParameter("State", Customer.r_state_desc, DbType.String);
                            DbParameter StateSAP = provider.CreateParameter("StateSAP", Customer.r_state, DbType.String);
                            DbParameter District = provider.CreateParameter("District", Customer.r_district_desc, DbType.String);
                            DbParameter DistrictSAP = provider.CreateParameter("DistrictSAP", Customer.r_district, DbType.String);
                            DbParameter[] Params = new DbParameter[22] { ICTicketNumber, ICTicketDate,ServiceRecord, DealerCode, EquipmentSerNo,WarrantyExpiryDate,ModelDescription
                                , PresentContactNumber,ContactPerson,ComplaintCode,ComplaintDescription,Information, ReasonForCloser,OldICTicketNumber
                            ,CustomerCode,ServicePriorityID,RequestedDate
                            ,Country,State,StateSAP,District,DistrictSAP};

                            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                            {
                                provider.Insert("ZDMS_InsertOrUpdateICTicket", Params); ;
                                scope.Complete();
                            }
                        }
                        File.Move(file, file.Replace("DCONNECT", "DCONNECT\\Processed"));
                    }
                    catch (Exception e1)
                    {
                        File.Move(file, file.Replace("DCONNECT", "DCONNECT\\FAILED"));
                        new FileLogger().LogMessageService("BDMS_ICTicket", "IntegrationICTicket", e1);
                        //  throw e1;
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                File.Move(fileT, fileT.Replace("DCONNECT", "DCONNECT\\FAILED"));
                new FileLogger().LogMessageService("BDMS_Material", "IntegrationMaterial", ex);
                //   throw ex;
            }
            if (Customers.results != null)
                return Customers.results.Count();
            else
                return 0;
        }
        public int IntegrationICTicketByBapi()
        {
            //  new BDMS_Customer().IntegrationCustomer(CustomerFiles);
            //   new BDMS_Equipment().IntegrationEquipment(EquipmentFiles);

            TraceLogger.Log(DateTime.Now);
            //   PDMS_ICTicketJSON Customers = new PDMS_ICTicketJSON();
            List<PDMS_ICTicket> ICTickets = new SDMS_ICTicket().GetICTicketFromSAP();
            try
            {
                foreach (PDMS_ICTicket IC in ICTickets)
                {
                    //    string CallDate = Customer.f_call_login_date;
                    //   string ReqDate = Customer.r_date_of_req;
                    //    DateTime TicketDate = Convert.ToDateTime(CallDate.Substring(6, 2) + "/" + CallDate.Substring(4, 2) + "/" + CallDate.Substring(0, 4) + " " + CallDate.Substring(8, 2) + ":" + CallDate.Substring(10, 2) + ":" + CallDate.Substring(12, 2));
                    //    DateTime RequestedDateT = Convert.ToDateTime(ReqDate.Substring(6, 2) + "/" + ReqDate.Substring(4, 2) + "/" + ReqDate.Substring(0, 4) + " " + ReqDate.Substring(8, 2) + ":" + ReqDate.Substring(10, 2) + ":" + ReqDate.Substring(12, 2));

                    DbParameter ICTicketNumber = provider.CreateParameter("ICTicketNumber", IC.ICTicketNumber, DbType.String);
                    DbParameter ICTicketDate = provider.CreateParameter("ICTicketDate", IC.ICTicketDate, DbType.DateTime);
                    DbParameter ServiceRecord = provider.CreateParameter("ServiceRecord", IC.ServiceRecord, DbType.String);
                    DbParameter DealerCode = provider.CreateParameter("DealerCode", IC.Dealer.DealerCode, DbType.String);
                    DbParameter PresentContactNumber = provider.CreateParameter("PresentContactNumber", IC.PresentContactNumber, DbType.String);
                    DbParameter ContactPerson = provider.CreateParameter("ContactPerson", IC.ContactPerson, DbType.String);
                    DbParameter ComplaintCode = provider.CreateParameter("ComplaintCode", IC.ComplaintCode, DbType.String);
                    DbParameter ComplaintDescription = provider.CreateParameter("ComplaintDescription", IC.ComplaintDescription, DbType.String);
                    DbParameter Information = provider.CreateParameter("Information", IC.Information, DbType.String);
                    DbParameter ReasonForCloser = provider.CreateParameter("ReasonForCloser", IC.ReasonForCloser, DbType.String);
                    DbParameter OldICTicketNumber = provider.CreateParameter("OldICTicketNumber", IC.OldICTicketNumber, DbType.String);

                    //DbParameter ServiceType = provider.CreateParameter("ServiceType", "", DbType.String);
                    DbParameter ServicePriorityID = provider.CreateParameter("ServicePriorityID", IC.ServicePriority.ServicePriority, DbType.String);
                    DbParameter RequestedDate = provider.CreateParameter("RequestedDate", IC.RequestedDate, DbType.DateTime);
                    //DbParameter ServiceRecord = provider.CreateParameter("ServiceRecord", Customer.s, DbType.String);
                    DbParameter Country = provider.CreateParameter("Country", IC.Address.Country.CountryCode, DbType.String);
                    DbParameter State = provider.CreateParameter("State", IC.Address.State.State, DbType.String);
                    DbParameter StateSAP = provider.CreateParameter("StateSAP", IC.Address.State.StateSAP, DbType.String);
                    DbParameter District = provider.CreateParameter("District", IC.Address.District.District, DbType.String);
                    DbParameter DistrictSAP = provider.CreateParameter("DistrictSAP", IC.Address.District.DistrictSAP, DbType.String);

                    DbParameter CustomerCode = provider.CreateParameter("CustomerCode", IC.Customer.CustomerCode, DbType.String);
                    DbParameter CustomerName = provider.CreateParameter("CustomerName", IC.Customer.CustomerName, DbType.String);
                    DbParameter EquipmentSerNo = provider.CreateParameter("EquipmentSerNo", IC.Equipment.EngineSerialNo, DbType.String);
                    DbParameter WarrantyExpiryDate = provider.CreateParameter("WarrantyExpiryDate", IC.Equipment.WarrantyExpiryDate, DbType.DateTime);
                    DbParameter CounterObjectID = provider.CreateParameter("CounterObjectID", IC.Equipment.CounterObjectID, DbType.String);
                    DbParameter HMRDate = provider.CreateParameter("HMRDate", IC.Equipment.HMRDate, DbType.DateTime);
                    DbParameter HMRValue = provider.CreateParameter("HMRValue", IC.Equipment.HMRValue, DbType.String);

                    List<string> Model = new SDMS_ICTicket().getModelByProductID(IC.Equipment.EngineSerialNo);
                    DbParameter ModelP = provider.CreateParameter("Model", Model[0], DbType.String);
                    DbParameter DivisionP = provider.CreateParameter("Division", Model[1], DbType.String);
                    DbParameter EngineModelP = provider.CreateParameter("EngineModel", Model[2], DbType.String);
                    DbParameter EngineSerialNoP = provider.CreateParameter("EngineSerialNo", Model[3], DbType.String);



                    DbParameter[] Params = new DbParameter[29] { ICTicketNumber, ICTicketDate,ServiceRecord, DealerCode
                                , PresentContactNumber,ContactPerson,ComplaintCode,ComplaintDescription,Information, ReasonForCloser,OldICTicketNumber
                            ,ServicePriorityID,RequestedDate,Country,State,StateSAP,District,DistrictSAP,CustomerCode,CustomerName
                    , EquipmentSerNo,WarrantyExpiryDate,CounterObjectID,HMRDate,HMRValue,ModelP,DivisionP,EngineModelP,EngineSerialNoP};

                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        provider.Insert("ZDMS_InsertOrUpdateICTicketByBapi", Params);
                        new SDMS_ICTicket().GetICTicketToSAP(IC.GUID, IC.ICTicketNumber, "X");
                        scope.Complete();
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Material", "IntegrationMaterial", ex);
                throw ex;
            }

            return ICTickets.Count();
        }
        public string GetICTicketForIntegrationVerification()
        {
            List<PDMS_ICTicket> ICTicketFromSAP = new List<PDMS_ICTicket>();
            string ICT = "";
            try
            {
                DateTime ICTicketDateF = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                ICTicketDateF = ICTicketDateF.AddHours(DateTime.Now.Hour - 50);
                DateTime ICTicketDateT = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                ICTicketDateT = ICTicketDateT.AddHours(DateTime.Now.Hour);
                ICTicketDateT = ICTicketDateT.AddMinutes(DateTime.Now.Minute - 1);

                ICTicketFromSAP = new SDMS_ICTicket().ZBAPI_GET_ICTICKETNO_BY_DATE(ICTicketDateF, ICTicketDateT);

                foreach (PDMS_ICTicket IC in ICTicketFromSAP)
                {
                    DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", IC.ICTicketNumber.TrimStart('0'), DbType.String);
                    DbParameter DealerCodeP = provider.CreateParameter("DealerCode", IC.Dealer.DealerCode, DbType.String);

                    DbParameter[] Params = new DbParameter[2] { ICTicketNumberP, DealerCodeP };
                    using (DataSet DataSet = provider.Select("ZDMS_GetICTicketForIntegrationVerification", Params))
                    {
                        if (DataSet != null)
                        {
                            if (Convert.ToString(DataSet.Tables[0].Rows[0][0]) == "0")
                            {
                                ICT = ICT + IC.ICTicketNumber.TrimStart('0');
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return ICT;
        }
        public List<PDMS_ICTicket> GetICTicketManage(string DealerCode, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? StatusID, int? TechnicianID, int? ServiceTypeID,string Division)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter DealerCodeP;
                if (!string.IsNullOrEmpty(DealerCode))
                    DealerCodeP = provider.CreateParameter("DealerCode", DealerCode, DbType.String);
                else
                    DealerCodeP = provider.CreateParameter("DealerCode", null, DbType.String);
                DbParameter CustomerCodeP;
                if (!string.IsNullOrEmpty(CustomerCode))
                    CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                else
                    CustomerCodeP = provider.CreateParameter("CustomerCode", null, DbType.String);
                DbParameter ICTicketNumberP;
                if (!string.IsNullOrEmpty(ICTicketNumber))
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", ICTicketNumber, DbType.String);
                else
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", null, DbType.String);

                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter StatusIDP = provider.CreateParameter("ServiceStatusID", StatusID, DbType.Int32);
                DbParameter TechnicianIDP = provider.CreateParameter("TechnicianID", TechnicianID, DbType.Int32);
                DbParameter ServiceTypeIDP = provider.CreateParameter("ServiceTypeID", ServiceTypeID, DbType.Int32);

                DbParameter DivisionP = provider.CreateParameter("Division", string.IsNullOrEmpty(Division) ? null : Division, DbType.String);

                DbParameter[] Params = new DbParameter[9] { DealerCodeP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, StatusIDP, TechnicianIDP, ServiceTypeIDP, DivisionP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketManage", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);

                            W.Equipment = new PDMS_EquipmentHeader();
                            W.Equipment.EquipmentModel = new PDMS_Model()
                            {
                                Model = Convert.ToString(dr["EquipmentModel"]),
                                Division = new PDMS_Division()
                                {
                                    DivisionCode = Convert.ToString(dr["DivisionCode"])
                                }
                            };

                            // W.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]); 
                            // W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            //  W.Equipment.EngineModel = Convert.ToString(dr["EngineModel"]);
                            //   W.Equipment.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                            //  W.Equipment.CorrectSMR = Convert.ToString(dr["CorrectSMR"]);
                            //    W.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
                            //   W.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                            //   W.Equipment.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);

                            //  W.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            //    W.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            // W.ComplaintCode = Convert.ToString(dr["ComplaintCode"]);
                            //  W.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                            // W.Information = Convert.ToString(dr["Information"]);
                            //  W.ReasonForCloser = Convert.ToString(dr["ReasonForCloser"]);
                            //  W.OldICTicketNumber = Convert.ToString(dr["OldICTicketNumber"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            //     W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);


                            //   W.ReachedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            //   W.RequestedEndDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            //  W.ServiceRecord = Convert.ToString(dr["ServiceRecord"]);
                            //W.RegisteredBy = new PUser();
                            //if (dr["RegisteredByID"] != DBNull.Value)
                            //    W.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

                            //if (dr["TechnicianID"] != DBNull.Value)
                            //{
                            //    W.Technician = new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
                            //}
                            //else
                            //{
                            //    W.Technician = new PUser();
                            //}

                            // W.Address = new PDMS_Address();
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

        public List<PDMS_ICTicket> GetICTicket(string DealerCode, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? StatusID, int? TechnicianID)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter DealerCodeP;
                if (!string.IsNullOrEmpty(DealerCode))
                    DealerCodeP = provider.CreateParameter("DealerCode", DealerCode, DbType.String);
                else
                    DealerCodeP = provider.CreateParameter("DealerCode", null, DbType.String);
                DbParameter CustomerCodeP;
                if (!string.IsNullOrEmpty(CustomerCode))
                    CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                else
                    CustomerCodeP = provider.CreateParameter("CustomerCode", null, DbType.String);
                DbParameter ICTicketNumberP;
                if (!string.IsNullOrEmpty(ICTicketNumber))
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", ICTicketNumber, DbType.String);
                else
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", null, DbType.String);

                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter StatusIDP = provider.CreateParameter("ServiceStatusID", StatusID, DbType.Int32);
                DbParameter TechnicianIDP = provider.CreateParameter("TechnicianID", TechnicianID, DbType.Int32);

                DbParameter[] Params = new DbParameter[7] { DealerCodeP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, StatusIDP, TechnicianIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicket", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.Equipment = new PDMS_EquipmentHeader();

                            W.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                            W.Equipment.EquipmentModel = new PDMS_Model()
                            {
                                Model = Convert.ToString(dr["EquipmentModel"])
                            };
                            W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.Equipment.EngineModel = Convert.ToString(dr["EngineModel"]);

                            W.Equipment.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                            W.Equipment.CorrectSMR = Convert.ToString(dr["CorrectSMR"]);
                            W.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
                            W.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                            W.Equipment.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);

                            W.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ComplaintCode = Convert.ToString(dr["ComplaintCode"]);
                            W.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                            W.Information = Convert.ToString(dr["Information"]);
                            W.ReasonForCloser = Convert.ToString(dr["ReasonForCloser"]);
                            W.OldICTicketNumber = Convert.ToString(dr["OldICTicketNumber"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);

                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ReachedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.RequestedEndDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ServiceRecord = Convert.ToString(dr["ServiceRecord"]);
                            W.RegisteredBy = new PUser();
                            if (dr["RegisteredByID"] != DBNull.Value)
                                W.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

                            if (dr["TechnicianID"] != DBNull.Value)
                            {
                                W.Technician = new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
                            }
                            else
                            {
                                W.Technician = new PUser();
                            }

                            // W.Address = new PDMS_Address();
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
        public List<PDMS_ICTicket> GetICTicketForDeclinedApproval(string DealerCode, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter DealerCodeP;
                if (!string.IsNullOrEmpty(DealerCode))
                    DealerCodeP = provider.CreateParameter("DealerCode", DealerCode, DbType.String);
                else
                    DealerCodeP = provider.CreateParameter("DealerCode", null, DbType.String);
                DbParameter CustomerCodeP;
                if (!string.IsNullOrEmpty(CustomerCode))
                    CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                else
                    CustomerCodeP = provider.CreateParameter("CustomerCode", null, DbType.String);
                DbParameter ICTicketNumberP;
                if (!string.IsNullOrEmpty(ICTicketNumber))
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", ICTicketNumber, DbType.String);
                else
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", null, DbType.String);

                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

                DbParameter[] Params = new DbParameter[5] { DealerCodeP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketForDeclinedApproval", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.Equipment = new PDMS_EquipmentHeader();

                            W.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                            W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };
                            W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.Equipment.EngineModel = Convert.ToString(dr["EngineModel"]);
                            W.Equipment.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                            W.Equipment.CorrectSMR = Convert.ToString(dr["CorrectSMR"]);
                            W.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
                            W.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                            W.Equipment.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);

                            W.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ComplaintCode = Convert.ToString(dr["ComplaintCode"]);
                            W.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                            W.Information = Convert.ToString(dr["Information"]);
                            W.ReasonForCloser = Convert.ToString(dr["ReasonForCloser"]);
                            W.OldICTicketNumber = Convert.ToString(dr["OldICTicketNumber"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);

                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ReachedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.RequestedEndDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ServiceRecord = Convert.ToString(dr["ServiceRecord"]);
                            W.RegisteredBy = new PUser();
                            if (dr["RegisteredByID"] != DBNull.Value)
                                W.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

                            if (dr["TechnicianID"] != DBNull.Value)
                            {
                                W.Technician = new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
                            }
                            else
                            {
                                W.Technician = new PUser();
                            }
                            W.ReqDeclinedReason = Convert.ToString(dr["ReqDeclinedReason"]);
                            // W.Address = new PDMS_Address();
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

        public List<PDMS_ICTicket> GetICTicketReport(string DealerCode, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? StatusID, string EquipmentSerialNo)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter DealerCodeP;
                if (!string.IsNullOrEmpty(DealerCode))
                    DealerCodeP = provider.CreateParameter("DealerCode", DealerCode, DbType.String);
                else
                    DealerCodeP = provider.CreateParameter("DealerCode", null, DbType.String);
                DbParameter CustomerCodeP;
                if (!string.IsNullOrEmpty(CustomerCode))
                    CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                else
                    CustomerCodeP = provider.CreateParameter("CustomerCode", null, DbType.String);
                DbParameter ICTicketNumberP;
                if (!string.IsNullOrEmpty(ICTicketNumber))
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", ICTicketNumber, DbType.String);
                else
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", null, DbType.String);

                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter StatusIDP = provider.CreateParameter("ServiceStatusID", StatusID, DbType.Int32);
                DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", string.IsNullOrEmpty(EquipmentSerialNo) ? null : EquipmentSerialNo, DbType.String);
                DbParameter[] Params = new DbParameter[7] { DealerCodeP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, StatusIDP, EquipmentSerialNoP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketReport", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.Equipment = new PDMS_EquipmentHeader();

                            //  W.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                            W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };
                            W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            //    W.Equipment.EngineModel = Convert.ToString(dr["EngineModel"]);
                            //    W.Equipment.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                            //    W.Equipment.CorrectSMR = Convert.ToString(dr["CorrectSMR"]);
                            W.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
                            W.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                            W.Equipment.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);

                            W.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ComplaintCode = Convert.ToString(dr["ComplaintCode"]);
                            W.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                            W.Information = Convert.ToString(dr["Information"]);
                            W.ReasonForCloser = Convert.ToString(dr["ReasonForCloser"]);
                            W.OldICTicketNumber = Convert.ToString(dr["OldICTicketNumber"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);

                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ReachedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.RequestedEndDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ServiceRecord = Convert.ToString(dr["ServiceRecord"]);
                            W.RegisteredBy = new PUser();
                            if (dr["RegisteredByID"] != DBNull.Value)
                                W.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

                            if (dr["TechnicianID"] != DBNull.Value)
                            {
                                W.Technician = new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
                            }
                            else
                            {
                                W.Technician = new PUser();
                            }
                            W.ServiceMaterial = new PDMS_ServiceCharge();
                            W.ServiceMaterial.Material = new PDMS_Material();

                            W.ServiceMaterial.Material.MaterialCode = Convert.ToString(dr["MaterialCode"]);
                            W.ServiceMaterial.Material.MaterialDescription = Convert.ToString(dr["MaterialDescription"]);

                            W.ServiceMaterial.QuotationNumber = Convert.ToString(dr["QuotationNumber"]);
                            W.ServiceMaterial.ProformaInvoiceNumber = Convert.ToString(dr["ProformaInvoiceNumber"]);
                            W.ServiceMaterial.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            W.ServiceMaterial.ClaimNumber = Convert.ToString(dr["ClaimNumber"]);
                            W.ServiceMaterial.BasePrice = Convert.ToDecimal(dr["TaxableValue"]);
                            W.ServiceMaterial.Date = dr["Date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["Date"]);
                            W.ServiceMaterial.SGSTValue = Convert.ToDecimal(dr["Tax"]);

                            W.ServiceMaterial.Total = W.ServiceMaterial.BasePrice + W.ServiceMaterial.SGSTValue;
                            W.DeliveryNumber = Convert.ToString(dr["DeliveryNumber"]);
                            //  W.ServiceMaterial.Date = Convert.ToString(dr["InvoiceNumber"]);
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
        public PDMS_ICTicket GetICTicketByICTIcketID(long ICTicketID)
        {
            PDMS_ICTicket W = null;
            try
            {
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { ICTicketIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketByICTIcketID", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ServiceOrderNumber = Convert.ToString(dr["ServiceOrderNumber"]);
                            W.Dealer = new PDMS_Dealer()
                            {
                                DealerID = Convert.ToInt32(dr["DealerID"]),
                                DealerCode = Convert.ToString(dr["DealerCode"]),
                                DealerName = Convert.ToString(dr["DealerName"]),
                                TL = dr["TLContactName"] == DBNull.Value ? null : new PUser() { ContactName = Convert.ToString(dr["TLContactName"]), ContactNumber = Convert.ToString(dr["TLContactNumber"]) },
                                SM = dr["SMContactName"] == DBNull.Value ? null : new PUser() { ContactName = Convert.ToString(dr["SMContactName"]), ContactNumber = Convert.ToString(dr["SMContactNumber"]) },
                                IsEInvoice = dr["IsEInvoice"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsEInvoice"]),
                                EInvoiceDate = dr["EInvoiceDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["EInvoiceDate"])
                            };
                            W.Customer = new PDMS_Customer() { 
                                CustomerCode = Convert.ToString(dr["CustomerCode"]),
                                CustomerName = Convert.ToString(dr["CustomerName"]),
                                CustomerCategory = new PDMS_CustomerCategory() { CustomerCategory = Convert.ToString(dr["CustomerCategory"]) }
                            };
                            W.Address = new PDMS_Address();
                            W.Address.State = new PDMS_State() { State = Convert.ToString(dr["State"]) };
                            W.Address.Region = new PDMS_Region() { RegionID = Convert.ToInt32(dr["RegionID"]) };
                            W.Address.District = new PDMS_District() { District = Convert.ToString(dr["District"]) };
                            W.Equipment = new PDMS_EquipmentHeader()
                            {
                                EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]),
                                EquipmentModel = new PDMS_Model()
                                {
                                    Model = Convert.ToString(dr["EquipmentModel"]),
                                    Division = new PDMS_Division() { UOM = Convert.ToString(dr["UOM"]) }
                                },
                                EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]),
                                EngineModel = Convert.ToString(dr["EngineModel"]),
                                EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]),
                                CorrectSMR = Convert.ToString(dr["CorrectSMR"]),
                                DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]),
                                CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]),
                                WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]),
                                CounterObjectID = Convert.ToString(dr["CounterObjectID"]),

                                IsRefurbished = dr["IsRefurbished"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsRefurbished"]),
                                RefurbishedBy = dr["RefurbishedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["RefurbishedBy"]),
                                RFWarrantyStartDate = dr["RFWarrantyStartDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RFWarrantyStartDate"]),
                                RFWarrantyExpiryDate = dr["RFWarrantyExpiryDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RFWarrantyExpiryDate"]),

                                IsAMC = dr["IsAMC"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsAMC"]),
                                AMCStartDate = dr["AMCStartDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["AMCStartDate"]),
                                AMCExpiryDate = dr["AMCExpiryDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["AMCExpiryDate"]),
                                EquipmentWarrantyType = dr["EquipmentWarrantyTypeID"] == DBNull.Value ? null : new PDMS_EquipmentWarrantyType() { HMR = Convert.ToInt32(dr["WarrantyHMR"]) }
                            };

                            W.CurrentHMRDate = dr["CurrentHMRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CurrentHMRDate"]);
                            W.CurrentHMRValue = dr["CurrentHMRValue"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CurrentHMRValue"]);
                            W.LastHMRValue = dr["LastHMRValue"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["LastHMRValue"]);

                            if (W.LastHMRValue != 0)
                                W.LastHMRDate = dr["LastHMRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["LastHMRDate"]);

                            W.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ComplaintCode = Convert.ToString(dr["ComplaintCode"]);
                            W.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                            W.Information = Convert.ToString(dr["Information"]);
                            W.ReasonForCloser = Convert.ToString(dr["ReasonForCloser"]);
                            W.OldICTicketNumber = Convert.ToString(dr["OldICTicketNumber"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServiceSubTypeID"] != DBNull.Value)
                            {
                                W.ServiceSubType = new PDMS_ServiceSubType() { ServiceSubTypeID = Convert.ToInt32(dr["ServiceSubTypeID"]), ServiceSubType = Convert.ToString(dr["ServiceSubType"]) };
                            }
                            if (dr["ServiceTypeOverhaulID"] != DBNull.Value)
                            {
                                W.ServiceTypeOverhaul = new PDMS_ServiceTypeOverhaul() { ServiceTypeOverhaulID = Convert.ToInt32(dr["ServiceTypeOverhaulID"]), ServiceTypeOverhaul = Convert.ToString(dr["ServiceTypeOverhaul"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }
                            W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);
                            W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };

                            W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);

                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ReachedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.RestoreDate = dr["RestoreDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                            W.RequestedEndDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);

                            W.DepartureDate = dr["DepartureDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DepartureDate"]);
                            W.ArrivalBack = dr["ArrivalBack"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ArrivalBack"]);
                            W.ServiceRecord = Convert.ToString(dr["ServiceRecord"]);

                            if (dr["RegisteredByID"] != DBNull.Value)
                            {
                                W.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };
                            }
                            else
                            {
                                W.RegisteredBy = new PUser();
                            }
                            if (dr["TechnicianID"] != DBNull.Value)
                            {
                                W.Technician = new PUser()
                                {
                                    UserID = Convert.ToInt32(dr["TechnicianID"]),
                                    UserName = Convert.ToString(dr["TechnicianUserName"]),
                                    ContactName = Convert.ToString(dr["TechnicianName"]),
                                    ContactNumber = Convert.ToString(dr["TechnicianContactNumber"])
                                };
                            }
                            else
                            {
                                W.Technician = new PUser();
                            }

                            W.Location = Convert.ToString(dr["Location"]);
                            if (dr["OfficeID"] != DBNull.Value)
                                W.DealerOffice = new PDMS_DealerOffice()
                                {
                                    OfficeID = Convert.ToInt32(dr["OfficeID"]),
                                    OfficeName = Convert.ToString(dr["OfficeName"]),
                                    OfficeCode = Convert.ToString(dr["OfficeCode"]),
                                    OfficeName_OfficeCode = Convert.ToString(dr["OfficeCode"]) + "-" + Convert.ToString(dr["OfficeName"])
                                };

                            W.ReachedDate = dr["ReachedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ReachedDate"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            if (dr["ServicePriorityID"] != DBNull.Value)
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                           
                            //if (dr["Category1ID"] != DBNull.Value)
                            //    W.Category1 = new PDMS_Category1()
                            //    {
                            //        Category1ID = Convert.ToInt32(dr["Category1ID"]),
                            //        Category1 = Convert.ToString(dr["Category1ID"]),
                            //        Category1Code = Convert.ToString(dr["Category1Code"])
                            //    };
                            //if (dr["Category2ID"] != DBNull.Value)
                            //    W.Category2 = new PDMS_Category2()
                            //    {
                            //        Category2ID = Convert.ToInt32(dr["Category2ID"]),
                            //        Category2 = Convert.ToString(dr["Category2"]),
                            //        Category2Code = Convert.ToString(dr["Category2Code"])
                            //    };
                            //if (dr["Category3ID"] != DBNull.Value)
                            //    W.Category3 = new PDMS_Category3()
                            //    {
                            //        Category3ID = Convert.ToInt32(dr["Category3ID"]),
                            //        Category3 = Convert.ToString(dr["Category3"]),
                            //        Category3Code = Convert.ToString(dr["Category3Code"])
                            //    };
                            //if (dr["Category4ID"] != DBNull.Value)
                            //    W.Category4 = new PDMS_Category4()
                            //    {
                            //        Category4ID = Convert.ToInt32(dr["Category4ID"]),
                            //        Category4 = Convert.ToString(dr["Category4"]),
                            //        Category4Code = Convert.ToString(dr["Category4Code"])
                            //    };
                            //if (dr["Category5ID"] != DBNull.Value)
                            //    W.Category5 = new PDMS_Category5()
                            //    {
                            //        Category5ID = Convert.ToInt32(dr["Category5ID"]),
                            //        Category5 = Convert.ToString(dr["Category5"]),
                            //        Category5Code = Convert.ToString(dr["Category5Code"])
                            //    };


                            W.FSRNumber = Convert.ToString(dr["FSRNumber"]);
                            W.FSRDate = dr["FSRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FSRDate"]);//Convert.ToString(dr["FSRDate"]);
                            if (dr["MainApplicationID"] != DBNull.Value)
                                W.MainApplication = new PDMS_MainApplication() { MainApplicationID = Convert.ToInt32(dr["MainApplicationID"]), MainApplication = Convert.ToString(dr["MainApplication"]) };
                            if (dr["SubApplicationID"] != DBNull.Value)
                                W.SubApplication = new PDMS_SubApplication() { SubApplicationID = Convert.ToInt32(dr["SubApplicationID"]), SubApplication = Convert.ToString(dr["SubApplication"]) };
                            if (dr["CustomerSatisfactionLevelID"] != DBNull.Value)
                                W.CustomerSatisfactionLevel = new PDMS_CustomerSatisfactionLevel() { CustomerSatisfactionLevelID = Convert.ToInt32(dr["CustomerSatisfactionLevelID"]), CustomerSatisfactionLevel = Convert.ToString(dr["CustomerSatisfactionLevel"]) };
                            W.ScopeOfWork = Convert.ToString(dr["ScopeOfWork"]);

                            W.TSIRNumber = Convert.ToString(dr["TSIRNumber"]);
                            W.TSIRDate = Convert.ToString(dr["TSIRDate"]);
                            W.KindAttn = Convert.ToString(dr["KindAttn"]);
                            W.Remarks = Convert.ToString(dr["Remarks"]);

                            W.SiteContactPersonName = Convert.ToString(dr["SiteContactPersonName"]);
                            W.SiteContactPersonNumber = Convert.ToString(dr["SiteContactPersonNumber"]);
                            W.SiteContactPersonNumber2 = Convert.ToString(dr["SiteContactPersonNumber2"]);

                            W.SiteContactPersonDesignation = dr["SiteContactPersonDesignationID"] == DBNull.Value ? null : new PDMS_SiteContactPersonDesignation() { DesignationID = Convert.ToInt32(dr["SiteContactPersonDesignationID"]), Designation = Convert.ToString(dr["SiteContactPersonDesignation"]) };
                            W.TypeOfWarranty = dr["TypeOfWarrantyID"] == DBNull.Value ? null : new PDMS_TypeOfWarranty() { TypeOfWarrantyID = Convert.ToInt32(dr["TypeOfWarrantyID"]), TypeOfWarranty = Convert.ToString(dr["TypeOfWarranty"]) };
                            W.IsCess = dr["IsCess"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsCess"]);
                            W.IsMachineActive = dr["IsMachineActive"] == DBNull.Value ? true : Convert.ToBoolean(dr["IsMachineActive"]);
                            W.SubApplicationEntry = Convert.ToString(dr["SubApplicationEntry"]);

                            W.NoClaim = dr["NoClaim"] == DBNull.Value ? false : Convert.ToBoolean(dr["NoClaim"]);
                            W.NoClaimReason = Convert.ToString(dr["NoClaimReason"]);

                            //W.OverHaulHMR = dr["OverHaulHMR"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["OverHaulHMR"]);
                            //W.OverHaulWarrantyExpiryDate = dr["OverHaulWarrantyExpiryDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["OverHaulWarrantyExpiryDate"]);

                            W.McEnteredServiceDate = dr["McEnteredServiceDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["McEnteredServiceDate"]);
                            W.ServiceStartedDate = dr["ServiceStartedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ServiceStartedDate"]);
                            W.ServiceEndedDate = dr["ServiceEndedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ServiceEndedDate"]);
                            W.CustomerPayPercentage = dr["CustomerPayPercentage"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["CustomerPayPercentage"]);
                            W.DealerPayPercentage = dr["DealerPayPercentage"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["DealerPayPercentage"]);
                            W.AEPayPercentage = dr["AEPayPercentage"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["AEPayPercentage"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return W;
        }
        public List<PDMS_ICTicket> GetICTicketByEquipmentSerialNo(string EquipmentSerialNo)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", string.IsNullOrEmpty(EquipmentSerialNo) ? null : EquipmentSerialNo, DbType.String);
                DbParameter[] Params = new DbParameter[1] { EquipmentSerialNoP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketByEquipmentSerialNo", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.Equipment = new PDMS_EquipmentHeader();

                            //  W.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                            W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };
                            W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            //    W.Equipment.EngineModel = Convert.ToString(dr["EngineModel"]);
                            //    W.Equipment.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                            //    W.Equipment.CorrectSMR = Convert.ToString(dr["CorrectSMR"]);
                            W.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
                            W.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                            W.Equipment.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);

                            W.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ComplaintCode = Convert.ToString(dr["ComplaintCode"]);
                            W.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                            W.Information = Convert.ToString(dr["Information"]);
                            W.ReasonForCloser = Convert.ToString(dr["ReasonForCloser"]);
                            W.OldICTicketNumber = Convert.ToString(dr["OldICTicketNumber"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);

                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ReachedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.RequestedEndDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ServiceRecord = Convert.ToString(dr["ServiceRecord"]);
                            W.RegisteredBy = new PUser();
                            if (dr["RegisteredByID"] != DBNull.Value)
                                W.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

                            if (dr["TechnicianID"] != DBNull.Value)
                            {
                                W.Technician = new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
                            }
                            else
                            {
                                W.Technician = new PUser();
                            }

                            //  W.ServiceMaterial.Date = Convert.ToString(dr["InvoiceNumber"]);
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
        public Boolean InsertOrUpdateTechnicianForICTicket(long ICTicket, int TechnicianID, int UserID)
        {

            int success = 0;


            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicket, DbType.Int64);
            DbParameter TechnicianIDP = provider.CreateParameter("TechnicianID", TechnicianID, DbType.Int32);

            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            //  DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[3] { ICTicketP, TechnicianIDP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateTechnicianForICTicket", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateTechnicianForICTicket", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertOrUpdateTechnicianForICTicket", ex);
                return false;
            }
            return true;
        }
        public Boolean InsertOrUpdateTechnicianAddOrRemoveICTicket(long ICTicket, int TechnicianID, Boolean IsDeleted, int UserID)
        {
            int success = 0;
            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicket, DbType.Int64);
            DbParameter TechnicianIDP = provider.CreateParameter("TechnicianID", TechnicianID, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter IsDeleteP = provider.CreateParameter("IsDeleted", IsDeleted, DbType.Boolean);
            DbParameter[] Params = new DbParameter[4] { ICTicketP, TechnicianIDP, UserIDP, IsDeleteP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateTechnicianAddOrRemoveICTicket", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateTechnicianAddOrRemoveICTicket", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertOrUpdateTechnicianAddOrRemoveICTicket", ex);
                return false;
            }
            return true;
        }

        public Boolean InsertOrUpdateTechnicianWorkedDateAddOrRemoveICTicket(long ServiceTechnicianWorkDateID, long? ICTicket, int? TechnicianID, DateTime? WorkedDay, decimal WorkedHours, Boolean IsDeleted, int UserID)
        {
            int success = 0;
            DbParameter ServiceTechnicianWorkDateIDP = provider.CreateParameter("ServiceTechnicianWorkDateID", ServiceTechnicianWorkDateID, DbType.Int64);
            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicket, DbType.Int64);
            DbParameter TechnicianIDP = provider.CreateParameter("TechnicianID", TechnicianID, DbType.Int32);
            DbParameter WorkedDayP = provider.CreateParameter("WorkedDay", WorkedDay, DbType.DateTime);
            DbParameter WorkedHoursP = provider.CreateParameter("WorkedHours", WorkedHours, DbType.Decimal);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter IsDeleteP = provider.CreateParameter("IsDeleted", IsDeleted, DbType.Boolean);
            DbParameter[] Params = new DbParameter[7] { ServiceTechnicianWorkDateIDP, ICTicketP, TechnicianIDP, WorkedDayP, WorkedHoursP, UserIDP, IsDeleteP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateTechnicianWorkedDateForICTicket", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateTechnicianAddOrRemoveICTicket", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertOrUpdateTechnicianAddOrRemoveICTicket", ex);
                return false;
            }
            return true;
        }

        public Boolean InsertOrUpdateServiceConfirmation(long ICTicket, string Location, int? DealerOffice, DateTime? DepartureDate, DateTime? ReachedDate, int? ServiceType, int? ServiceSubTypeID, int? ServiceTypeOverhaul
            , int? ServicePriority, DateTime? HMRdate, int? HMRValue, Boolean IsWarranty, int? TypeOfWarrantyID, int? MainApplication, int? SubApplication, string ScopeOfWork,
            string KindAttn, string Remarks, string SiteContactPersonName, string SiteContactPersonNumber, string SiteContactPersonNumber2, int? DesignationID, Boolean IsCess, Boolean IsMachineActive, string SubApplicationEntry, int UserID, Boolean NoClaim, string NoClaimReason
            , DateTime? McEnteredServiceDate, DateTime? ServiceStartedDate, DateTime? ServiceEndedDate, Boolean IsIGST)
        {
            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicket, DbType.Int64);
            DbParameter LocationP = provider.CreateParameter("Location", Location, DbType.String);
            DbParameter DealerOfficeP = provider.CreateParameter("DealerOffice", DealerOffice, DbType.Int32);
            DbParameter DepartureDateP = provider.CreateParameter("DepartureDate", DepartureDate, DbType.DateTime);
            DbParameter ReachedDateP = provider.CreateParameter("ReachedDate", ReachedDate, DbType.DateTime);
            DbParameter ServiceTypeP = provider.CreateParameter("ServiceType", ServiceType, DbType.Int32);
            DbParameter ServiceSubTypeIDP = provider.CreateParameter("ServiceSubTypeID", ServiceSubTypeID, DbType.Int32);
            DbParameter ServiceTypeOverhaulP = provider.CreateParameter("ServiceTypeOverhaul", ServiceTypeOverhaul, DbType.Int32);
            DbParameter ServicePriorityP = provider.CreateParameter("ServicePriority", ServicePriority, DbType.Int32);

            DbParameter HMRdateP = provider.CreateParameter("HMRdate", HMRdate, DbType.DateTime);
            DbParameter HMRValueP = provider.CreateParameter("HMRValue", HMRValue, DbType.Int32);
            DbParameter IsWarrantyP = provider.CreateParameter("IsWarranty", IsWarranty, DbType.Boolean);
            DbParameter TypeOfWarrantyIDP = provider.CreateParameter("TypeOfWarrantyID", TypeOfWarrantyID, DbType.Int32);

            //DbParameter Category1P = provider.CreateParameter("Category1", Category1, DbType.Int32);
            //DbParameter Category2P = provider.CreateParameter("Category2", Category2, DbType.Int32);
            //DbParameter Category3P = provider.CreateParameter("Category3", Category3, DbType.Int32);
            //DbParameter Category4P = provider.CreateParameter("Category4", Category4, DbType.Int32);
            //DbParameter Category5P = provider.CreateParameter("Category5", Category5, DbType.Int32);

            DbParameter MainApplicationP = provider.CreateParameter("MainApplication", MainApplication, DbType.Int32);
            DbParameter SubApplicationP = provider.CreateParameter("SubApplication", SubApplication, DbType.Int32);


            DbParameter ScopeOfWorkP = provider.CreateParameter("ScopeOfWork", ScopeOfWork, DbType.String);


            DbParameter KindAttnP = provider.CreateParameter("KindAttn", KindAttn, DbType.String);
            DbParameter RemarksP = provider.CreateParameter("Remarks", Remarks, DbType.String);

            DbParameter SiteContactPersonNameP = provider.CreateParameter("SiteContactPersonName", SiteContactPersonName, DbType.String);
            DbParameter SiteContactPersonNumberP = provider.CreateParameter("SiteContactPersonNumber", SiteContactPersonNumber, DbType.String);
            DbParameter SiteContactPersonNumber2P = provider.CreateParameter("SiteContactPersonNumber2", SiteContactPersonNumber2, DbType.String);
            DbParameter DesignationIDP = provider.CreateParameter("DesignationID", DesignationID, DbType.String);

            DbParameter IsCessP = provider.CreateParameter("IsCess", IsCess, DbType.Boolean);
            DbParameter IsMachineActiveP = provider.CreateParameter("IsMachineActive", IsMachineActive, DbType.Boolean);
            DbParameter SubApplicationEntryP = provider.CreateParameter("SubApplicationEntry", SubApplicationEntry, DbType.String);

            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter NoClaimP = provider.CreateParameter("NoClaim", NoClaim, DbType.Boolean);
            DbParameter NoClaimReasonP = provider.CreateParameter("NoClaimReason", NoClaimReason, DbType.String);

            DbParameter McEnteredServiceDateP = provider.CreateParameter("McEnteredServiceDate", McEnteredServiceDate, DbType.DateTime);
            DbParameter ServiceStartedDateP = provider.CreateParameter("ServiceStartedDate", ServiceStartedDate, DbType.DateTime);
            DbParameter ServiceEndedDateP = provider.CreateParameter("ServiceEndedDate", ServiceEndedDate, DbType.DateTime);
            DbParameter IsIGSTP = provider.CreateParameter("IsIGST", IsIGST, DbType.Boolean); 

            //  DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[32] { ICTicketP, LocationP,DealerOfficeP,DepartureDateP,ReachedDateP,ServiceTypeP,ServiceSubTypeIDP,ServiceTypeOverhaulP,ServicePriorityP
                ,HMRdateP,HMRValueP,IsWarrantyP,TypeOfWarrantyIDP,MainApplicationP,SubApplicationP,ScopeOfWorkP
               ,KindAttnP,RemarksP,SiteContactPersonNameP,SiteContactPersonNumberP,SiteContactPersonNumber2P,DesignationIDP,IsCessP,IsMachineActiveP,SubApplicationEntryP,UserIDP,NoClaimP,NoClaimReasonP
            ,McEnteredServiceDateP,ServiceStartedDateP,ServiceEndedDateP,IsIGSTP};
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateServiceConfirmation", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateServiceConfirmation", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertOrUpdateServiceConfirmation", ex);
                return false;
            }
            return true;
        }

        public Boolean UpdateICTicketWarrantyDistribution(long ICTicket, decimal? CustomerPayPercentage, decimal? DealerPayPercentage, decimal? AEPayPercentage)
        {
            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicket, DbType.Int64);

            DbParameter CustomerPayPercentageP = provider.CreateParameter("CustomerPayPercentage", CustomerPayPercentage, DbType.Decimal);
            DbParameter DealerPayPercentageP = provider.CreateParameter("DealerPayPercentage", DealerPayPercentage, DbType.Decimal);
            DbParameter AEPayPercentageP = provider.CreateParameter("AEPayPercentage", AEPayPercentage, DbType.Decimal);

            //  DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[4] { ICTicketP, CustomerPayPercentageP, DealerPayPercentageP, AEPayPercentageP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateICTicketWarrantyDistribution", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "UpdateICTicketWarrantyDistribution", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " UpdateICTicketWarrantyDistribution", ex);
                return false;
            }
            return true;
        }

        public Boolean InsertOrUpdateNoteAddOrRemoveICTicket(long? ServiceNoteID, long ICTicket, int NoteTypeID, string Comments, Boolean IsDeleted, int UserID)
        {
            int success = 0;
            DbParameter ServiceNoteIDP = provider.CreateParameter("ServiceNoteID", ServiceNoteID, DbType.Int64);
            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicket, DbType.Int64);
            DbParameter NoteTypeIDP = provider.CreateParameter("NoteTypeID", NoteTypeID, DbType.Int32);
            DbParameter CommentsP = provider.CreateParameter("Comments", Comments, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter IsDeleteP = provider.CreateParameter("IsDeleted", IsDeleted, DbType.Boolean);
            DbParameter[] Params = new DbParameter[6] { ServiceNoteIDP, ICTicketP, NoteTypeIDP, CommentsP, UserIDP, IsDeleteP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateNoteAddOrRemoveICTicket", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateNoteAddOrRemoveICTicket", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertOrUpdateNoteAddOrRemoveICTicket", ex);
                return false;
            }
            return true;
        }
        public long InsertOrUpdateMaterialAddOrRemoveICTicket(long ServiceMaterialID,  long ICTicketID
            , string Material, string MaterialSN, string DefectiveMaterial, string DefectiveMaterialSN, decimal Qty, decimal AvailableQty, int? MaterialSourceID
            , Boolean IsFaultyPart, long? TsirID, Boolean IsDeleted, int UserID, Boolean IsIGST          , Boolean IsRecomenedParts, Boolean IsQuotationParts  , PDMS_ServiceMaterial MaterialTax)
        {
            long success = 0;
            long ID = 0;
           
            DbParameter ServiceMaterialIDP = provider.CreateParameter("ServiceMaterialID", ServiceMaterialID, DbType.Int64);
            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicketID, DbType.Int64);
            DbParameter MaterialP = provider.CreateParameter("Material", Material, DbType.String);
            DbParameter MaterialSNP = provider.CreateParameter("MaterialSN", MaterialSN, DbType.String);

            DbParameter DefectiveMaterialP = provider.CreateParameter("DefectiveMaterial", DefectiveMaterial, DbType.String);
            DbParameter DefectiveMaterialSNP = provider.CreateParameter("DefectiveMaterialSN", DefectiveMaterialSN, DbType.String);

            DbParameter QtyP = provider.CreateParameter("Qty", Qty, DbType.Decimal);
            DbParameter AvailableQtyP = provider.CreateParameter("AvailableQty", AvailableQty, DbType.Decimal);
            DbParameter BasePrice = provider.CreateParameter("BasePrice", MaterialTax.BasePrice, DbType.Decimal);
            DbParameter SGSTP = provider.CreateParameter("SGST", MaterialTax.SGST, DbType.Decimal);
            DbParameter SGSTValueP = provider.CreateParameter("SGSTValue", MaterialTax.SGSTValue, DbType.Decimal);
            DbParameter IGSTP = provider.CreateParameter("IGST", MaterialTax.IGST, DbType.Decimal);
            DbParameter IGSTValueP = provider.CreateParameter("IGSTValue", MaterialTax.IGSTValue, DbType.Decimal);

            DbParameter MaterialSourceIDP = provider.CreateParameter("MaterialSourceID", MaterialSourceID, DbType.Int32);
            DbParameter IsFaultyPartP = provider.CreateParameter("IsFaultyPart", IsFaultyPart, DbType.Boolean);
            DbParameter TsirIDP = provider.CreateParameter("TsirID", TsirID, DbType.Int64);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter IsDeleteP = provider.CreateParameter("IsDeleted", IsDeleted, DbType.Boolean);
            DbParameter IsIGSTP = provider.CreateParameter("IsIGST", IsIGST, DbType.Decimal);

            DbParameter IsRecomenedPartsP = provider.CreateParameter("IsRecomenedParts", IsRecomenedParts, DbType.Boolean);
            DbParameter IsQuotationPartsP = provider.CreateParameter("IsQuotationParts", IsQuotationParts, DbType.Boolean);

            DbParameter OldInvoice = provider.CreateParameter("OldInvoice", MaterialTax.OldInvoice, DbType.String);

            DbParameter IDP = provider.CreateParameter("OutValue", ID, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[23] { ServiceMaterialIDP, ICTicketP, MaterialP, MaterialSNP,DefectiveMaterialP,DefectiveMaterialSNP
                , QtyP, AvailableQtyP, BasePrice, SGSTP, SGSTValueP, IGSTP, IGSTValueP, MaterialSourceIDP, IsFaultyPartP, TsirIDP, UserIDP, IsDeleteP, IDP, IsIGSTP
            ,IsRecomenedPartsP,IsQuotationPartsP,OldInvoice};
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateMaterialAddOrRemoveICTicket", Params);
                    scope.Complete();
                    success = Convert.ToInt64(IDP.Value);
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateMaterialAddOrRemoveICTicket", sqlEx);
                return 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateMaterialAddOrRemoveICTicket", ex);
                return 0;
            }
            return success;
        }
        public Boolean UpdateMaterialRemoveICTicketSapStatus(long ServiceMaterialID, Boolean Status)
        {
            DbParameter ServiceMaterialIDP = provider.CreateParameter("ServiceMaterialID", ServiceMaterialID, DbType.Int64);
            DbParameter StatusP = provider.CreateParameter("Status", Status, DbType.Boolean);
            DbParameter[] Params = new DbParameter[2] { ServiceMaterialIDP, StatusP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateMaterialRemoveICTicketSapStatus", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "UpdateMaterialRemoveICTicketSapStatus", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " UpdateMaterialRemoveICTicketSapStatus", ex);
                return false;
            }
            return true;
        }
     

        public string dssor_sales_order_hdr = " insert into dssor_sales_order_hdr ( s_establishment,p_so_id,s_tenant_id,f_customer_id,f_location,f_currency,f_bill_to,s_modified_by "
                + " ,r_insurance_p,r_discount_amt_additional,s_status,r_tax_amt,s_created_on,r_net_amt,f_ship_to"
                + " ,r_contact_no,r_gross_amt,r_contact_prsn,r_discount_amt,s_created_by,s_modified_on,f_office,f_order_type,s_object_type,r_remarks,r_exp_del_date"
                + " ,r_frieght_p,r_order_date,channel,f_division,r_auto,r_ref_obj_name,r_ref_obj_type,r_price_grp,r_model,r_model_no,s_last_request_index,r_insurance_amt,r_packing_chrgs,r_freight_amt,r_our_ref_id ,r_ref_date) values ";       

        public string dssor_sales_order_item = " insert into dssor_sales_order_item (s_establishment,p_so_item, p_so_id,s_tenant_id,f_location,s_modified_by,f_uom,r_tax_amt,f_division"
                + " ,s_status,f_office,r_exp_del_date,f_material_id,s_last_request_index,r_order_qty,r_add_discount_amt"
                + " ,s_created_on,r_net_amt,d_material_desc,r_resvered_qty,r_gross_amt,r_cancel_qty ,r_shiped_qty,r_discount_amt,s_created_by,r_unit_price,r_pending_qty,s_modified_on"
                + " ,s_object_type,r_approved_qty,s_channel) values ";

        public string dssor_sales_order_cond = "insert into dssor_sales_order_cond (s_establishment,p_so_item,p_so_id,s_tenant_id,p_condition_type,f_currency,"
                + " r_cond_amt,r_order_qty,r_pric_date,s_created_by,s_created_on,d_cond_desc,r_cond_cls,f_percentage,channel) values ";


        public string CreateQuotationForMaterial(PDMS_ICTicket ICTicket, List<PDMS_ServiceMaterial> ServiceMaterials, PUser User, List<string> Quotation)
        {
            //         s_modified_on  

            string f_bill_to = "";
            string r_insurance_p = "Seller";
            string f_order_type = "101";
            string s_object_type = "101";
            string r_remarks = "";
            string r_frieght_p = "Seller";
            string r_auto = "false";
            string r_ref_obj_name = "";
            string r_ref_obj_type = "null";
            string r_price_grp = "07";
            string r_model = ICTicket.Equipment.EquipmentModel.Model;
            string r_model_no = ICTicket.Equipment.EquipmentSerialNo;

            if (ICTicket.IsWarranty)
            {
                f_bill_to = "B001";
                r_insurance_p = "";
                f_order_type = "108";
                s_object_type = "108";
                r_remarks = "In Warranty Quotation";
                r_frieght_p = "";
                r_auto = "null";
                r_ref_obj_name = "dsprr_psc_hdr";
                r_ref_obj_type = "101";
                r_price_grp = "";
            }
            if (ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PreCommission)
            {
                f_order_type = "108";
                s_object_type = "108";
                r_ref_obj_name = "Pre Commission";
                r_remarks = "Pre Commission";
            }
            else if (ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
            {
                f_order_type = "302";
                s_object_type = "302";
                r_ref_obj_name = "Ser-Center-Quotation";
                r_remarks = "Overhaul Service Quotation";
            }
            else if (ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PolicyWarranty)
            {
                f_order_type = "108";
                s_object_type = "108";
                r_ref_obj_name = "Policy Warranty";
                r_remarks = "Policy Warranty Quotation";
            }
            else if (ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PartsWarranty)
            {
                f_order_type = "108";
                s_object_type = "108";
                r_ref_obj_name = "Parts Warranty";
                r_remarks = "Parts Warranty Quotation";
            }
            else if (ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.GoodwillWarranty)
            {
                f_order_type = "108";
                s_object_type = "108";
                r_ref_obj_name = "Goodwil Warranty";
                r_remarks = "Goodwil Warranty Quotation";
            }
            int success = 0;
            string QuotationNumber = "@@QuotationNumber";
            List<string> querys = new List<string>();
            Boolean HeaderCheck = false;
            string query = "";
            try
            {
                foreach (PDMS_ServiceMaterial ServiceMaterial in ServiceMaterials)
                {
                    if ((ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Warranty) || (ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PartsWarranty) || (ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.GoodwillWarranty))
                    {
                        decimal Percentage= (ICTicket.DealerPayPercentage == null ?0: (decimal)ICTicket.DealerPayPercentage)+(ICTicket.AEPayPercentage == null ?0: (decimal)ICTicket.AEPayPercentage);
                        ServiceMaterial.Discount = ServiceMaterial.BasePrice * ServiceMaterial.Qty * Percentage / 100;
                    }

                    string f_location = "'LOC" + ICTicket.Dealer.DealerCode + "_" + ICTicket.DealerOffice.OfficeName + "'";

                    decimal r_tax_amt = ServiceMaterial.IGSTValue + ServiceMaterial.SGSTValue + ServiceMaterial.SGSTValue;
                    decimal r_net_amt = ServiceMaterial.BasePrice + r_tax_amt - ServiceMaterial.Discount;


                  
                   

                    if (HeaderCheck == false)
                    {
                        string f_ship_to = new NpgsqlServer().ExecuteScalar("select r_def_address_id   from doohr_bp where p_bp_type='CU'   and s_tenant_id= " + ICTicket.Dealer.DealerCode + " and  p_bp_id = '" + ICTicket.Customer.CustomerCode + "' limit 1");
                        string ICTicketDate = ICTicket.ICTicketDate.Year + "/" + ICTicket.ICTicketDate.Month + "/" + ICTicket.ICTicketDate.Day;
                        query = dssor_sales_order_hdr + "(1000,'" + QuotationNumber + "','" + ICTicket.Dealer.DealerCode + "','" + ICTicket.Customer.CustomerCode
           + "'," + f_location + ",'INR','" + f_bill_to + "','" + User.UserName + "','" + r_insurance_p + "',0,'DRAFT'"
           + "," + r_tax_amt + ",now()," + r_net_amt + ",'" + f_ship_to + "','" + ICTicket.PresentContactNumber + "'," + ServiceMaterial.BasePrice
           + ",'" + ICTicket.ContactPerson + "'," + ServiceMaterial.Discount + ",'" + User.UserName + "',now(),'" + ICTicket.DealerOffice.OfficeCode + "'," + f_order_type + "," + s_object_type + ",'" + r_remarks + "',now(),'" + r_frieght_p + "',now(),'UI','SP',"
           + r_auto + ",'" + r_ref_obj_name + "'," + r_ref_obj_type + ",'" + r_price_grp + "','" + r_model + "','" + r_model_no + "',0,0,0,0,'" + ICTicket.ICTicketNumber + "','" + ICTicketDate + "')";

                        querys.Add(query);
                        HeaderCheck = true;
                    }
                    query = dssor_sales_order_item

                        + "(1000," + ServiceMaterial.Item + ", '" + QuotationNumber + "'," + ICTicket.Dealer.DealerCode + "," + f_location + ",'" + User.UserName
                        + "','EA'," + r_tax_amt + ",'SP','DRAFT','" + ICTicket.DealerOffice.OfficeCode + "',now(),'" + ServiceMaterial.Material.MaterialCode + "',0," + ServiceMaterial.Qty
                        + ",0,now()," + r_net_amt + ",'" + ServiceMaterial.Material.MaterialDescription.Replace("'", "''") + "',0," + ServiceMaterial.BasePrice
        + ",0,0," + ServiceMaterial.Discount + ",'" + User.UserName + "'," + ServiceMaterial.Material.CurrentPrice + "," + ServiceMaterial.Qty + ",now()," + s_object_type + "," + ServiceMaterial.Qty + ",'UI')";

                    querys.Add(query);

                    query = dssor_sales_order_cond
    + "(1000," + ServiceMaterial.Item + ",'" + QuotationNumber + "'," + ICTicket.Dealer.DealerCode + ",'ZPRP','INR'," + ServiceMaterial.BasePrice
    + "," + ServiceMaterial.Qty + ",now(),'" + User.UserName + "',now(),'Price-Ajax Parts','B',  null  ,'UI')";
                    querys.Add(query);
                    if (ServiceMaterial.Discount != 0)
                    {
                        query = dssor_sales_order_cond
        + "(1000," + ServiceMaterial.Item + ",'" + QuotationNumber + "'," + ICTicket.Dealer.DealerCode + ",'ZCD2','INR'," + ServiceMaterial.Discount
        + "," + ServiceMaterial.Qty + ",now(),'" + User.UserName + "',now(),'Customer Discount','A'," + ServiceMaterial.Material.TaxPercentage + ",'UI')";
                        querys.Add(query);
                    }
                    if (ServiceMaterial.SGST != 0)
                    {
                        query = dssor_sales_order_cond
        + "(1000," + ServiceMaterial.Item + ",'" + QuotationNumber + "'," + ICTicket.Dealer.DealerCode + ",'ZOCG','INR'," + ServiceMaterial.SGSTValue
        + "," + ServiceMaterial.Qty + ",now(),'" + User.UserName + "',now(),'DLR Central GST OP','D'," + ServiceMaterial.SGST + ",'UI')";
                        querys.Add(query);
                        query = dssor_sales_order_cond
        + "(1000," + ServiceMaterial.Item + ",'" + QuotationNumber + "'," + ICTicket.Dealer.DealerCode + ",'ZOSG','INR'," + ServiceMaterial.SGSTValue
        + "," + ServiceMaterial.Qty + ",now(),'" + User.UserName + "',now(),'DLR Central GST OP','D'," + ServiceMaterial.SGST + ",'UI')";
                        querys.Add(query);
                    }
                    else
                    {
                        query = dssor_sales_order_cond
        + "(1000," + ServiceMaterial.Item + ",'" + QuotationNumber + "'," + ICTicket.Dealer.DealerCode + ",'ZOIG','INR'," + ServiceMaterial.IGSTValue
        + "," + ServiceMaterial.Qty + ",now(),'" + User.UserName + "',now(),'DLR Central GST OP','D'," + ServiceMaterial.IGST + ",'UI')";
                        querys.Add(query);
                    }
                    //             query = "INSERT INTO public.dppor_purc_order_hdr("
                    //+ " s_establishment, s_tenant_id,                     p_po_id, f_location                                           , f_currency, f_bill_to, s_modified_by          , r_insurance_p, r_tax_amt, s_created_on, f_sold_to, s_status, r_net_amt, r_req_del_date, r_gross_amt, s_created_by           , s_modified_on, f_order_type, f_office                                  , s_object_type, r_exp_del_date, r_remarks          , r_frieght_p,r_order_date,channel, f_division,  r_auto,f_vendor_id,s_channel, s_status_custom)"
                    //+ " VALUES (1000," + ICTicket.Dealer.DealerCode + " , 100100 , '" + ICTicket.DealerOffice.OfficeName_OfficeCode + "', 'INR'     , '100235' , '" + User.UserName + "', 'SELLER'     , r_tax_amt, now()       , '100235' , 'DRAFT' , r_net_amt,  now()        , r_gross_amt, '" + User.UserName + "', now()        , 104         , '" + ICTicket.DealerOffice.OfficeCode + "', 104          , now()         , 'In Warranty Order', 'SELLER'   , now()      , 'UI'  , 'SP'      , true   , '100235'  , 'MI'    , 'DRAFT')";
                    //             querys.Add(query);
                }

                QuotationNumber = new NpgsqlServer().QuotationCreationUpdateTransactions(querys, ICTicket.Dealer.DealerCode, Quotation);

                if (!string.IsNullOrEmpty(QuotationNumber))
                {
                    foreach (PDMS_ServiceMaterial ServiceMaterial in ServiceMaterials)
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            DbParameter ServiceMaterialIDPP = provider.CreateParameter("ServiceMaterialID", ServiceMaterial.ServiceMaterialID, DbType.Int64);
                            DbParameter QuotationNumberP = provider.CreateParameter("QuotationNumber", QuotationNumber, DbType.String);
                            DbParameter[] Paramss = new DbParameter[2] { ServiceMaterialIDPP, QuotationNumberP };
                            provider.Insert("ZDMS_UpdateICTicketMaterialQuotationNumber", Paramss);
                            scope.Complete();
                        }
                    }
                }
                else
                {

                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateMaterialAddOrRemoveICTicket", sqlEx);
                return "";
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertOrUpdateMaterialAddOrRemoveICTicket", ex);
                return "";
            }
            return QuotationNumber;
        }

        public Boolean InsertOrUpdateMaterialServiceAddOrRemoveICTicket(long? ServiceChargeID, string Customer, long? ICTicket, string MaterialService
            , DateTime? ServiceDate, decimal WorkedHours, decimal BasePrice, decimal Discount, Boolean IsDeleted, Boolean IsIGST, int UserID)
        {

            int success = 0;
            // PDMS_ServiceMaterial MM = new SMaterial().getMaterialTax(Customer, Vendor, OrderType, 1, MaterialService, 1);
            // new SCustomer().getCustomerAddress(Customer);
            DbParameter ServiceChargeIDP = provider.CreateParameter("ServiceChargeID", ServiceChargeID, DbType.Int64);
            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicket, DbType.Int64);
            DbParameter MaterialServiceP = provider.CreateParameter("MaterialService", MaterialService, DbType.String);
            DbParameter ServiceDateP = provider.CreateParameter("ServiceDate", ServiceDate, DbType.DateTime);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter IsDeleteP = provider.CreateParameter("IsDeleted", IsDeleted, DbType.Boolean);

            DbParameter WorkedHoursP = provider.CreateParameter("WorkedHours", WorkedHours, DbType.Decimal);
            DbParameter BasePriceP = provider.CreateParameter("BasePrice", BasePrice, DbType.Decimal);
            DbParameter DiscountP = provider.CreateParameter("Discount", Discount, DbType.Decimal);
            DbParameter IsIGSTP = provider.CreateParameter("IsIGST", IsIGST, DbType.Decimal);

            DbParameter[] Params = new DbParameter[10] { ServiceChargeIDP, ICTicketP, MaterialServiceP, ServiceDateP, WorkedHoursP, BasePriceP, DiscountP, UserIDP, IsDeleteP, IsIGSTP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateMaterialServiceAddOrRemoveICTicket1", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateMaterialServiceAddOrRemoveICTicket", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertOrUpdateMaterialServiceAddOrRemoveICTicket", ex);
                return false;
            }
            return true;
        }
        public Boolean UpdateICTicketDecline(long ICTicket, string Reason, int UserID)
        {

            int success = 0;


            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicket, DbType.Int64);
            DbParameter ReasonP = provider.CreateParameter("Reason", Reason, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            //  DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[3] { ICTicketP, ReasonP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_UpdateICTicketDecline", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "UpdateICTicketDecline", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " UpdateICTicketDecline", ex);
                return false;
            }
            return true;
        }

        //public List<PDMS_MTTR> GetMttrWithText(string query, Boolean live = false)
        //{


        //                Mttr.problem_reported = Convert.ToString(dr["problem_reported"]);




        //                Mttr.present_mc_region = Convert.ToString(dr["present_mc_region"]);


        //                Mttr.prior_srv_order = Convert.ToString(dr["prior_srv_order"]);

        //                Mttr.ser_rec_time = Convert.ToString(dr["ser_rec_time"]);


        //                Mttr.complaintdesc = Convert.ToString(dr["complaintdesc"]);
        //                Mttr.ser_ord_no = Convert.ToString(dr["ser_ord_no"]);
        //                Mttr.ser_item = Convert.ToString(dr["ser_item"]);
        //                Mttr.ser_rec_no = Convert.ToString(dr["ser_rec_no"]);
        //                Mttr.ser_id = Convert.ToString(dr["ser_id"]);
        //                Mttr.ser_name = Convert.ToString(dr["r_first_name"]); 
        //                Mttr.f_part_id = Convert.ToString(dr["f_part_id"]);

        //                Mttr.flag = Convert.ToString(dr["flag"]);



        //                Mttr.r_priority_class_desc = Convert.ToString(dr["r_priority_class_desc"]);
        //                Mttr.r_priority_desc = Convert.ToString(dr["r_priority_desc"]); 

        //                Mttr.Breakdown = new List<PBreakdown>();


        //            if ((Mttr.Breakdown.Where(m => m.BreakdownDetails.Trim().ToLower() == Convert.ToString(dr["Breakdown_Details"]).Trim().ToLower() && m.BreakdownReason.Trim().ToLower() == Convert.ToString(dr["Breakdown_Reason"]).Trim().ToLower()).Count() == 0))
        //            {
        //                Mttr.Breakdown.Add(new PBreakdown()
        //                {
        //                    BreakdownNoteType = Convert.ToString(dr["Breakdown_Note_Type"]),
        //                    BreakdownReason = Convert.ToString(dr["Breakdown_Reason"]),
        //                    BreakdownDetails = Convert.ToString(dr["Breakdown_Details"])
        //                });
        //            }


        //    return Mttrs;
        //}
        public Boolean InsertClaimForMaterial(long ICTicketID, int UserID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32); 
                    DbParameter[] Paramss = new DbParameter[2] { ICTicketIDP, UserIDP };
                    provider.Insert("ZDMS_InsertClaimForMaterial", Paramss);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertClaimForMaterial", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertClaimForMaterial", ex);
                return false;
            }
            return true;
        }
        public Boolean InsertClaimForService(long ICTicketID, int UserID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                    DbParameter[] Paramss = new DbParameter[2] { ICTicketIDP, UserIDP };
                    provider.Insert("ZDMS_InsertClaimForService", Paramss);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertClaimForService", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertClaimForService", ex);
                return false;
            }
            return true;
        }
        public List<PAttachedFile> GetICTicketAttachedFile(long? ICTicketID, long? AttachedFileID)
        {
            List<PAttachedFile> D8 = new List<PAttachedFile>();
            try
            {
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
                DbParameter[] Params = new DbParameter[2] { ICTicketIDP, AttachedFileIDP };
                using (DataSet DS = provider.Select("ZDMS_GetICTicketAttachedFile", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            D8.Add(new PAttachedFile()
                            {
                                AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]),
                                AttachedFile = (Byte[])(dr["AttachedFile"]),
                                FileType = Convert.ToString(dr["ContentType"]),
                                FileName = Convert.ToString(dr["FileName"]),
                                FileSize = Convert.ToInt32(dr["FileSize"])
                            });
                        }
                    }
                }
                return D8;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "GetICTicketAttachedFile", ex);
                return null;
            }
        }
        public Boolean InsertOrUpdateICTicketAttachedFileAddOrRemove(PAttachedFile AttachedFile, int UserID)
        {

            int success = 0;
            // PDMS_ServiceMaterial MM = new SMaterial().getMaterialTax(Customer, Vendor, OrderType, 1, MaterialService, 1);
            // new SCustomer().getCustomerAddress(Customer);
            DbParameter AttachedFileID = provider.CreateParameter("AttachedFileID", AttachedFile.AttachedFileID, DbType.Int64);
            DbParameter ICTicketID = provider.CreateParameter("ICTicketID", AttachedFile.TicketID, DbType.Int64);
            DbParameter AttachedFileP = provider.CreateParameter("AttachedFile", AttachedFile.AttachedFile, DbType.Binary);
            DbParameter FileType = provider.CreateParameter("ContentType", AttachedFile.FileType, DbType.String);
            DbParameter FileName = provider.CreateParameter("FileName", AttachedFile.FileName, DbType.String);
            DbParameter FileSize = provider.CreateParameter("FileSize", AttachedFile.FileSize, DbType.Int32);
            DbParameter IsDeleted = provider.CreateParameter("IsDeleted", AttachedFile.IsDeleted, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[8] { AttachedFileID, ICTicketID, AttachedFileP, FileType, FileName, FileSize, IsDeleted, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateICTicketAttachedFileAddOrRemove", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateMaterialServiceAddOrRemoveICTicket", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertOrUpdateMaterialServiceAddOrRemoveICTicket", ex);
                return false;
            }
            return true;
        }
        public Boolean UpdateICTicketMarginWarranty(long ICTicketID, Boolean MarginWarranty, string MarginRemark, int UserID)
        {
            int success = 0;
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
            DbParameter MarginWarrantyP = provider.CreateParameter("MarginWarranty", MarginWarranty, DbType.Boolean);
            DbParameter MarginRemarkP = provider.CreateParameter("MarginRemark", MarginRemark, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { ICTicketIDP, MarginWarrantyP, MarginRemarkP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_UpdateICTicketMarginWarranty", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "UpdateICTicketMarginWarranty", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " UpdateICTicketMarginWarranty", ex);
                return false;
            }
            return true;
        }
        public Boolean ApproveOrDeclineICTicketReqDecline(long ICTicketID, Boolean Approve)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                    DbParameter ApproveP = provider.CreateParameter("Approve", Approve, DbType.Boolean);
                    DbParameter[] Paramss = new DbParameter[2] { ICTicketIDP, ApproveP };
                    provider.Insert("ZDMS_ApproveOrDeclineICTicketReqDecline", Paramss);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "ApproveOrDeclineICTicketReqDecline", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " ApproveOrDeclineICTicketReqDecline", ex);
                return false;
            }
            return true;
        }

        public void UpdateICTicketToSAP()
        {
            PDMS_ICTicket Ticket = new PDMS_ICTicket();
            Ticket = GetICTicketByICTIcketID(56);


            List<PDMS_ServiceCharge> Service = new BDMS_Service().GetServiceCharges(Ticket.ICTicketID, null, "", null);


            List<PDMS_ServiceMaterial> Material = new BDMS_Service().GetServiceMaterials(Ticket.ICTicketID, null, null, "", null, "");


            List<PDMS_ServiceNote> NoteType = new List<PDMS_ServiceNote>();
            NoteType = new BDMS_Service().GetServiceNote(Ticket.ICTicketID, null, null, "");


            List<PAttachedFile> AFile = GetICTicketAttachedFile(Ticket.ICTicketID, null);



            new SDMS_ICTicket().UpdateICTicketToSAP(Ticket, Service, Material, NoteType, AFile);

        }

        public int ValidateNEPI(string ServiceMaterial, string EquipmentSerialNo)
        {
            DbParameter ServiceMaterialP = provider.CreateParameter("ServiceMaterial", ServiceMaterial, DbType.String);
            DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", EquipmentSerialNo, DbType.String);
            DbParameter[] Params = new DbParameter[2] { ServiceMaterialP, EquipmentSerialNoP };
            using (DataSet DataSet = provider.Select("ZDMS_ValidateNEPI", Params))
            {
                if (DataSet != null)
                {
                    foreach (DataRow dr in DataSet.Tables[0].Rows)
                    {
                        return Convert.ToInt32(dr["Count"]);
                    }
                }
            }
            return 1;
        }
        public int ValidateNEPIUsingServiceSubType(int ServiceSubTypeID, string EquipmentSerialNo, long ICTicketID)
        {
            DbParameter ServiceSubTypeIDP = provider.CreateParameter("ServiceSubTypeID", ServiceSubTypeID, DbType.Int32);
            DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", EquipmentSerialNo, DbType.String);
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);

            DbParameter[] Params = new DbParameter[3] { ServiceSubTypeIDP, EquipmentSerialNoP, ICTicketIDP };
            using (DataSet DataSet = provider.Select("ZDMS_ValidateNEPIUsingServiceSubType", Params))
            {
                if (DataSet != null)
                {
                    foreach (DataRow dr in DataSet.Tables[0].Rows)
                    {
                        return Convert.ToInt32(dr["Count"]);
                    }
                }
            }
            return 1;
        }

        public Boolean ChangeICTicketRequestedDate(long ICTicketID, DateTime RequestedDate, int UserID)
        {
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
            DbParameter RequestedDateP = provider.CreateParameter("RequestedDate", RequestedDate, DbType.DateTime);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            DbParameter[] Params = new DbParameter[3] { ICTicketIDP, RequestedDateP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_ChangeICTicketRequestedDate", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "ChangeICTicketRequestedDate", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " ChangeICTicketRequestedDate", ex);
                return false;
            }
            return true;
        }
        public List<PDMS_ICTicket> GetICTicketFirstTimeRightForWarrantyService(int? DealerID, DateTime? DateFrom, DateTime? DateTo, int? UserID)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateFrom, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateTo, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketFirstTimeRightForWarrantyService", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.Equipment = new PDMS_EquipmentHeader();

                            W.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                            W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };
                            W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.Equipment.EngineModel = Convert.ToString(dr["EngineModel"]);
                            W.Equipment.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                            W.Equipment.CorrectSMR = Convert.ToString(dr["CorrectSMR"]);
                            W.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
                            W.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                            W.Equipment.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);

                            W.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ComplaintCode = Convert.ToString(dr["ComplaintCode"]);
                            W.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                            W.Information = Convert.ToString(dr["Information"]);
                            W.ReasonForCloser = Convert.ToString(dr["ReasonForCloser"]);
                            W.OldICTicketNumber = Convert.ToString(dr["OldICTicketNumber"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);

                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ReachedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.RequestedEndDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ServiceRecord = Convert.ToString(dr["ServiceRecord"]);
                            W.RegisteredBy = new PUser();
                            if (dr["RegisteredByID"] != DBNull.Value)
                                W.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

                            if (dr["TechnicianID"] != DBNull.Value)
                            {
                                W.Technician = new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
                            }
                            else
                            {
                                W.Technician = new PUser();
                            }
                            W.LastICTicket = new PDMS_ICTicket()
                            {
                                ICTicketNumber = Convert.ToString(dr["LastICTicketNumber"]),
                                Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["LastDealerCode"]) },
                                Technician = new PUser() { ContactName = Convert.ToString(dr["LastTechnicianName"]) }
                            };
                            if (dr["LastICTicketDate"] != DBNull.Value)
                            {
                                W.LastICTicket.ICTicketDate = Convert.ToDateTime(dr["LastICTicketDate"]);
                            }
                            // W.Address = new PDMS_Address();
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

        public string dppor_purc_order_hdr = "insert into dppor_purc_order_hdr ( s_establishment, s_tenant_id, p_po_id, f_location, f_currency, f_bill_to, s_modified_by, r_insurance_p, r_tax_amt, s_created_on, f_sold_to, s_status, r_net_amt, r_req_del_date, f_ship_to, r_description, r_contact_no, r_gross_amt, r_contact_prsn, f_so_id, r_discount_amt, s_created_by, s_modified_on, f_order_type, f_office, s_object_type, r_exp_del_date, r_remarks, r_frieght_p, f_doc_flow_id, r_order_date, s_sync_status, s_action, channel, r_discount_amt_additional, f_division, is_ack, r_auto, r_ext_id, f_vendor_id, s_last_request_id, s_last_request_index, s_channel, s_status_custom ) values  ";
        public string dppor_purc_order_item = "insert into  dppor_purc_order_item (s_establishment, k_po_id, p_po_item, s_tenant_id, f_so_item, f_oem_id, f_location, f_material_id, r_order_qty, s_modified_by, r_item_type, r_tax_amt, f_uom, s_created_on, r_indicator, s_status, r_net_amt, r_doc_flow_id, d_material_desc, r_resvered_qty, r_gross_amt, r_hgl_item, f_so_id, r_cancel_qty, r_shiped_qty, r_discount_amt, s_created_by, r_unit_price, r_pending_qty, s_modified_on, f_office, s_object_type, r_exp_del_date, r_approved_qty, r_add_discount_amt, s_sync_status, s_action, channel, f_division, is_ack, r_ref_obj_type, r_ref_obj_name, s_last_request_id, s_last_request_index, s_channel, s_status_custom, r_delv_qty, r_gr_qty) values ";
        public string CreateWarrantyPOForMaterial(PDMS_ICTicket ICTicket, long SMaterialID, PUser User)
        {
            string PONumber = "";
            int success = 0;
            List<PDMS_ServiceMaterial> SMaterialForPO = new BDMS_Service().GetServiceMaterials(ICTicket.ICTicketID, null, null, "", false, "");
            List<string> POs = new List<string>();
            foreach (PDMS_ServiceMaterial SMaterial in SMaterialForPO)
            {
                if (!string.IsNullOrEmpty(SMaterial.PONumber))
                {
                    if (!POs.Contains(SMaterial.PONumber))
                        POs.Add(SMaterial.PONumber);
                }
            }
            List<string> querys = new List<string>();
            string query = "";
            PDMS_ServiceMaterial ServiceMaterial = new BDMS_Service().GetServiceMaterials(ICTicket.ICTicketID, SMaterialID, null, "", false, "")[0];
            try
            {
                //foreach (PDMS_ServiceMaterial ServiceMaterial in ServiceMaterials)
                //{

                string s_establishment = "1000";
                string s_tenant_id = "'" + ICTicket.Dealer.DealerCode + "'";
                string p_po_id = "'@@QuotationNumber'";

                string f_location = "'LOC" + ICTicket.Dealer.DealerCode + "_" + ICTicket.DealerOffice.OfficeName + "'";
                string f_currency = "'INR'";
                string f_bill_to = "null";
                string s_modified_by = "'" + User.UserName + "'";
                string r_insurance_p = "'SELLER'";
                string r_tax_amt = "0";
                string s_created_on = "now()";
                string f_sold_to = "'100235'";
                string s_status = "'DRAFT'";
                string r_net_amt = "0";
                string r_req_del_date = "null";
                string f_ship_to = "null";
                string r_description = "null";
                string r_contact_no = "null";
                string r_gross_amt = "0";
                string r_contact_prsn = "null";
                string f_so_id = "null";
                string r_discount_amt = "0";
                string s_created_by = "'" + User.UserName + "'";
                string s_modified_on = "now()";
                string f_order_type = "104";
                string f_office = "'" + ICTicket.DealerOffice.OfficeCode + "'";
                string s_object_type = "104";
                string r_exp_del_date = "null";
                string r_remarks = "'" + ICTicket.ICTicketNumber + "," + ICTicket.ICTicketDate.ToShortDateString()
                    + "," + ICTicket.Customer.CustomerName + "," + ICTicket.Equipment.EquipmentSerialNo + "'";
                string r_frieght_p = "'SELLER'";
                string f_doc_flow_id = "'OE'";
                string r_order_date = "now()"; //
                string s_sync_status = "null";
                string s_action = "null";
                string channel = "'UI'";
                string r_discount_amt_additional = "0";
                string f_division = "'SP'";
                string is_ack = "null";
                string r_auto = "'False'";
                string r_ext_id = "null";
                string f_vendor_id = "'100235'";
                string s_last_request_id = "null";
                string s_last_request_index = "null";
                string s_channel = "null";
                string s_status_custom = "null";
                //if ((string.IsNullOrEmpty(ServiceMaterial.QuotationNumber)) && (ServiceMaterial.IsCustomerStock == false))
                //{
                //if (HeaderCheck == false)
                //{
                query = dppor_purc_order_hdr + "(" + s_establishment + "," + s_tenant_id + "," + p_po_id + "," + f_location + "," + f_currency + "," + f_bill_to + "," + s_modified_by + "," + r_insurance_p + "," + r_tax_amt + ","
                    + s_created_on + "," + f_sold_to + "," + s_status + "," + r_net_amt + "," + r_req_del_date + "," + f_ship_to + "," + r_description + "," + r_contact_no + "," + r_gross_amt
                    + "," + r_contact_prsn + "," + f_so_id + "," + r_discount_amt + "," + s_created_by + "," + s_modified_on + "," + f_order_type + "," + f_office + "," + s_object_type + "," + r_exp_del_date + ","
                    + r_remarks + "," + r_frieght_p + "," + f_doc_flow_id + "," + r_order_date + "," + s_sync_status + "," + s_action + "," + channel + "," + r_discount_amt_additional
                    + "," + f_division + "," + is_ack + "," + r_auto + "," + r_ext_id + "," + f_vendor_id + "," + s_last_request_id + "," + s_last_request_index + "," + s_channel + "," + s_status_custom + " )";

                querys.Add(query);
                //    HeaderCheck = true;
                //}

                string k_po_id = "'@@QuotationNumber'";
                string p_po_item = Convert.ToString(ServiceMaterial.Item);
                string f_so_item = Convert.ToString(ServiceMaterial.Item);
                string f_oem_id = "null";
                string f_material_id = "'" + ServiceMaterial.Material.MaterialCode + "'";
                string r_order_qty = Convert.ToString(ServiceMaterial.Qty - ServiceMaterial.AvailableQty);
                string r_item_type = "0";
                r_tax_amt = "0";
                string f_uom = "'EA'";
                string r_indicator = "null";
                r_net_amt = "0";
                string r_doc_flow_id = "null";
                string d_material_desc = "'" + ServiceMaterial.Material.MaterialDescription + "'";
                string r_resvered_qty = "0";
                r_gross_amt = "0";
                string r_hgl_item = "0";
                string r_cancel_qty = "0";
                string r_shiped_qty = "0";
                r_discount_amt = "0";
                string r_unit_price = "0";
                string r_pending_qty = "0";
                string r_approved_qty = "0";
                string r_add_discount_amt = "0";
                string r_ref_obj_type = "104";
                string r_ref_obj_name = "'dppor_purc_order_hdr'";
                string r_delv_qty = "0";
                string r_gr_qty = "0";
                query = dppor_purc_order_item
                              + "(" + s_establishment + "," + k_po_id + "," + p_po_item + "," + s_tenant_id + "," + f_so_item + "," + f_oem_id + "," + f_location + "," + f_material_id + ","
                              + r_order_qty + "," + s_modified_by + "," + r_item_type + "," + r_tax_amt + "," + f_uom + "," + s_created_on + "," + r_indicator + "," + s_status + ","
                              + r_net_amt + "," + r_doc_flow_id + "," + d_material_desc + "," + r_resvered_qty + "," + r_gross_amt + "," + r_hgl_item + "," + f_so_id + "," + r_cancel_qty
                              + "," + r_shiped_qty + "," + r_discount_amt + "," + s_created_by + "," + r_unit_price + "," + r_pending_qty + "," + s_modified_on + "," + f_office
                              + "," + s_object_type + "," + r_exp_del_date + "," + r_approved_qty + "," + r_add_discount_amt + "," + s_sync_status + "," + s_action + "," + channel
                              + "," + f_division + "," + is_ack + "," + r_ref_obj_type + "," + r_ref_obj_name + "," + s_last_request_id + "," + s_last_request_index + "," + s_channel
                              + "," + s_status_custom + "," + r_delv_qty + "," + r_gr_qty + ")";

                querys.Add(query);
                //}
                //}
                PONumber = "";
                PONumber = new NpgsqlServer().WarrantyPOQuotationCreationUpdateTransactions(querys, ICTicket.Dealer.DealerCode, ICTicket.ICTicketNumber.ToString(), POs);

                if (!string.IsNullOrEmpty(PONumber))
                {
                    //foreach (PDMS_ServiceMaterial ServiceMaterial in ServiceMaterials)
                    //{
                    //if ((string.IsNullOrEmpty(ServiceMaterial.QuotationNumber)) && (ServiceMaterial.IsCustomerStock == false))
                    //{
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        DbParameter ServiceMaterialIDPP = provider.CreateParameter("ServiceMaterialID", ServiceMaterial.ServiceMaterialID, DbType.Int64);
                        DbParameter QuotationNumberP = provider.CreateParameter("PONumber", PONumber, DbType.String);
                        DbParameter[] Paramss = new DbParameter[2] { ServiceMaterialIDPP, QuotationNumberP };
                        provider.Insert("ZDMS_UpdateICTicketMaterialWarrantyPONumber", Paramss);
                        scope.Complete();
                    }
                    //}
                    //}
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "CreateWarrantyPOForMaterial", sqlEx);
                return "";
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " CreateWarrantyPOForMaterial", ex);
                return "";
            }
            return PONumber;
        }

        public Boolean InsertOrUpdateICTicketRestore(long ICTicket, DateTime? RestoreDate, DateTime? ArrivalBack, int CustomerSatisfactionLevelID, string CustomerRemarks, string ComplaintStatus, int UserID)
        {
            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicket, DbType.Int64);
            DbParameter CustomerRemarksP = provider.CreateParameter("CustomerRemarks", CustomerRemarks, DbType.String);
            DbParameter RestoreDateP = provider.CreateParameter("RestoreDate", RestoreDate, DbType.DateTime);
            DbParameter CustomerSatisfactionLevelIDP = provider.CreateParameter("CustomerSatisfactionLevelID", CustomerSatisfactionLevelID, DbType.Int32);
            DbParameter ComplaintStatusP = provider.CreateParameter("ComplaintStatus", ComplaintStatus, DbType.String);
            DbParameter ArrivalBackP = provider.CreateParameter("ArrivalBack", ArrivalBack, DbType.DateTime);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            //  DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[7] { ICTicketP, RestoreDateP, ArrivalBackP, CustomerSatisfactionLevelIDP, CustomerRemarksP, ComplaintStatusP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateICTicketRestore", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateICTicketRestore", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertOrUpdateICTicketRestore", ex);
                return false;
            }
            return true;
        }

        public List<PDMS_ICTicket> GetICTicketStatusReport(int? DealerID, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? StatusID, string MachineSerialNumber)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);

                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter StatusIDP = provider.CreateParameter("ServiceStatusID", StatusID, DbType.Int32);
                DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", string.IsNullOrEmpty(MachineSerialNumber) ? null : MachineSerialNumber, DbType.String);
                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, StatusIDP, MachineSerialNumberP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketStatusReport", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };

                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ServiceType = new PDMS_ServiceType() { ServiceType = Convert.ToString(dr["ServiceType"]) };
                            W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ReqDeclinedReason = Convert.ToString(dr["DeclineReason"]);
                            W.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);

                            W.Equipment = new PDMS_EquipmentHeader();
                            W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };
                            W.NoClaim = dr["NoClaim"] == DBNull.Value ? false : Convert.ToBoolean(dr["NoClaim"]);
                            W.NoClaimReason = Convert.ToString(dr["NoClaimReason"]);

                            W.Invoice = dr["InvoiceNumber"] == DBNull.Value ? null : new PDMS_PaidServiceInvoice() { InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]), InvoiceDate = Convert.ToDateTime(dr["ICTicketDate"]) };
                          
                            
                            W.Claim =  new PDMS_WarrantyInvoiceHeader();
                            if (dr["ClaimNumber"] != DBNull.Value)
                            {
                                W.Claim.InvoiceNumber = Convert.ToString(dr["ClaimNumber"]);
                                W.Claim.InvoiceDate = Convert.ToDateTime(dr["ClaimDate"]);
                                W.Claim.ClaimStatus = Convert.ToString(dr["ClaimStatus"]);
                                if (Convert.ToInt32(dr["ClaimStatusID"]) == 1 || Convert.ToInt32(dr["ClaimStatusID"]) == 3 || Convert.ToInt32(dr["ClaimStatusID"]) == 11)
                                {
                                    W.Claim.DaysSinceClaimCreation = ((DateTime.Now - Convert.ToDateTime(dr["ClaimDate"])).Days);
                                }
                                else
                                {
                                    W.Claim.DaysSinceClaimCreation = 0;
                                }
                            }
                            else
                            {
                                W.Claim.DaysSinceClaimCreation = 0;
                            }

                            int ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]);
                            if (ServiceTypeID == 1 || ServiceTypeID == 6)
                            {
                                W.DayLeftForClaimCreation = 0;
                            }
                            else if ((dr["NoClaim"] == DBNull.Value ? false : Convert.ToBoolean(dr["NoClaim"])) == true)
                            {
                                W.DayLeftForClaimCreation = 0;
                            }
                            else
                            {
                                W.DayLeftForClaimCreation = dr["ClaimNumber"] == DBNull.Value ? 60 - ((DateTime.Now - W.ICTicketDate).Days) : (int?)null;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertDeviatedICTicketRequestForApproval", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertDeviatedICTicketRequestForApproval", ex);
            }
            return Ws;
        }

        public Boolean InsertDeviatedICTicketRequestForApproval(long ICTicketID, int ICTicketDeviationTypeID, int UserID)
        { 

            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter ICTicketDeviationTypeIDP = provider.CreateParameter("ICTicketDeviationTypeID", ICTicketDeviationTypeID, DbType.Int32);
            DbParameter[] Params = new DbParameter[3] { ICTicketIDP, ICTicketDeviationTypeIDP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertDeviatedICTicketRequestForApproval", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertDeviatedICTicketRequestForApproval", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertDeviatedICTicketRequestForApproval", ex);
                return false;
            }
            return true;
        }
        public Boolean ApproveOrRejectDeviatedICTicketRequest(long ICTicketID, Boolean? IsApproved, Boolean? IsRejected, int UserID)
        { 
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
            DbParameter IsApprovedP = provider.CreateParameter("IsApproved", IsApproved, DbType.Int32);
            DbParameter IsRejectedP = provider.CreateParameter("IsRejected", IsRejected, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { ICTicketIDP, IsApprovedP, IsRejectedP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                     provider.Insert("ZDMS_ApproveOrRejectDeviatedICTicketRequest", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertDeviatedICTicketRequestForApproval", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertDeviatedICTicketRequestForApproval", ex);
                return false;
            }
            return true;
        }
        public List<PDMS_ICTicket> GetDeviatedICTicketForApproval(int? DealerID, string ICTicketNumber, int ICTicketDeviationTypeID, DateTime? RequestedDateF, DateTime? RequestedDateT)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);                
                DbParameter ICTicketNumberP   = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber)? null:ICTicketNumber, DbType.String);
                DbParameter ICTicketDeviationTypeIDP = provider.CreateParameter("ICTicketDeviationTypeID", ICTicketDeviationTypeID, DbType.Int32);
                DbParameter RequestedDateFP = provider.CreateParameter("RequestedDateF", RequestedDateF, DbType.DateTime);
                DbParameter RequestedDateTP = provider.CreateParameter("RequestedDateT", RequestedDateT, DbType.DateTime);

                DbParameter[] Params = new DbParameter[5] { DealerIDP, ICTicketNumberP, ICTicketDeviationTypeIDP, RequestedDateFP, RequestedDateTP };
                using (DataSet DataSet = provider.Select("ZDMS_GetDeviatedICTicketForApproval", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);

                            W.Equipment = new PDMS_EquipmentHeader();
                            W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };

                       
                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]); 
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
        public DataTable GetDeviatedICTicketReport(int? DealerID, string ICTicketNumber, int? ICTicketDeviationTypeID, DateTime? RequestedDateF, DateTime? RequestedDateT, int UserID)
        {
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter RequestedDateFP = provider.CreateParameter("RequestedDateF", RequestedDateF, DbType.DateTime);
                DbParameter RequestedDateTP = provider.CreateParameter("RequestedDateT", RequestedDateT, DbType.DateTime);
                DbParameter ICTicketDeviationTypeIDP = provider.CreateParameter("ICTicketDeviationTypeID", ICTicketDeviationTypeID, DbType.Int32);
                DbParameter[] Params = new DbParameter[6] { DealerIDP, ICTicketNumberP, ICTicketDeviationTypeIDP, RequestedDateFP, RequestedDateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetDeviatedICTicketReport", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0]; 
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }

        public DataSet GetICTicketServiceEngineerUtilisationReport(int? DealerID, string EmployeeCode, DateTime? DateF, DateTime? DateT, int? StatusID, int UserID)
        {
            
            try
            {
                DbParameter DealerCodeP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter EmployeeCodeP = provider.CreateParameter("EmployeeCode", string.IsNullOrEmpty(EmployeeCode) ? null : EmployeeCode, DbType.String);

                DbParameter ICTicketDateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[6] { DealerCodeP, EmployeeCodeP, ICTicketDateFP, ICTicketDateTP, StatusIDP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketServiceEngineerUtilisationReport", Params))
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
        public DataSet GetICTicketServiceEngineerUtilisationReportDetails(int? DealerID, string EmployeeCode, DateTime? DateF, DateTime? DateT, int? StatusID, int UserID)
        {

            try
            {
                DbParameter DealerCodeP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter EmployeeCodeP = provider.CreateParameter("EmployeeCode", string.IsNullOrEmpty(EmployeeCode) ? null : EmployeeCode, DbType.String);

                DbParameter ICTicketDateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[6] { DealerCodeP, EmployeeCodeP, ICTicketDateFP, ICTicketDateTP, StatusIDP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketServiceEngineerUtilisationReportDetails", Params))
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
        public DataTable GetICTicketListDUMP(DateTime? DateF, DateTime? DateT)
        {
            try
            {                
                DbParameter ICTicketDateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                 DbParameter[] Params = new DbParameter[2] {ICTicketDateFP, ICTicketDateTP };
                 using (DataSet DataSet = provider.Select("ZDMS_GetICTicketListDUMP", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }

        public DataSet GetICTicketServiceCount(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {

            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketServiceCount", Params))
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
        public DataSet GetDashboardICTicketClaimCount(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {

            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetDashboardICTicketClaimCount", Params))
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

        public List<PAttachedFile> GetICTicketServiceCenterAttachedFile(long? ICTicketID, long? AttachedFileID)
        {
            List<PAttachedFile> D8 = new List<PAttachedFile>();
            try
            {
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
                DbParameter[] Params = new DbParameter[2] { ICTicketIDP, AttachedFileIDP };
                using (DataSet DS = provider.Select("GetICTicketServiceCenterAttachedFile", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            D8.Add(new PAttachedFile()
                            {
                                AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]),
                                AttachedFile = (Byte[])(dr["AttachedFile"]),
                                FileType = Convert.ToString(dr["ContentType"]),
                                FileName = Convert.ToString(dr["FileName"]),
                                FileSize = Convert.ToInt32(dr["FileSize"])
                            });
                        }
                    }
                }
                return D8;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "GetICTicketAttachedFile", ex);
                return null;
            }
        }
        public Boolean InsertOrUpdateICTicketServiceCenterAttachedFileAddOrRemove(PAttachedFile AttachedFile, int UserID)
        {

            int success = 0;
            // PDMS_ServiceMaterial MM = new SMaterial().getMaterialTax(Customer, Vendor, OrderType, 1, MaterialService, 1);
            // new SCustomer().getCustomerAddress(Customer);
            DbParameter AttachedFileID = provider.CreateParameter("AttachedFileID", AttachedFile.AttachedFileID, DbType.Int64);
            DbParameter ICTicketID = provider.CreateParameter("ICTicketID", AttachedFile.TicketID, DbType.Int64);
            DbParameter AttachedFileP = provider.CreateParameter("AttachedFile", AttachedFile.AttachedFile, DbType.Binary);
            DbParameter FileType = provider.CreateParameter("ContentType", AttachedFile.FileType, DbType.String);
            DbParameter FileName = provider.CreateParameter("FileName", AttachedFile.FileName, DbType.String);
            DbParameter FileSize = provider.CreateParameter("FileSize", AttachedFile.FileSize, DbType.Int32);
            DbParameter IsDeleted = provider.CreateParameter("IsDeleted", AttachedFile.IsDeleted, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[8] { AttachedFileID, ICTicketID, AttachedFileP, FileType, FileName, FileSize, IsDeleted, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("InsertOrUpdateICTicketServiceCenterAttachedFileAddOrRemove", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateICTicketServiceCenterAttachedFileAddOrRemove", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertOrUpdateICTicketServiceCenterAttachedFileAddOrRemove", ex);
                return false;
            }
            return true;
        }


        //public Boolean InsertDeviatedICTicketCommissioningRequestForApproval(long ICTicketID, int UserID)
        //{
        //    DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
        //    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //    DbParameter[] Params = new DbParameter[2] { ICTicketIDP, UserIDP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            provider.Insert("ZDMS_InsertDeviatedICTicketCommissioningRequestForApproval", Params);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BDMS_ICTicket", "InsertDeviatedICTicketCommissioningRequestForApproval", sqlEx);
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BDMS_ICTicket", " InsertDeviatedICTicketCommissioningRequestForApproval", ex);
        //        return false;
        //    }
        //    return true;
        //}
        //public Boolean ApproveOrRejectDeviatedICTicketCommissioningRequest(long ICTicketID, Boolean? IsApproved, Boolean? IsRejected, int UserID)
        //{
        //    DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
        //    DbParameter IsApprovedP = provider.CreateParameter("IsApproved", IsApproved, DbType.Int32);
        //    DbParameter IsRejectedP = provider.CreateParameter("IsRejected", IsRejected, DbType.Int32);
        //    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //    DbParameter[] Params = new DbParameter[4] { ICTicketIDP, IsApprovedP, IsRejectedP, UserIDP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            provider.Insert("ZDMS_ApproveOrRejectDeviatedICTicketCommissioningRequest", Params);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BDMS_ICTicket", "InsertDeviatedICTicketCommissioningRequestForApproval", sqlEx);
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BDMS_ICTicket", " InsertDeviatedICTicketCommissioningRequestForApproval", ex);
        //        return false;
        //    }
        //    return true;
        //}
        //public List<PDMS_ICTicket> GetDeviatedICTicketCommissioningForApproval(int? DealerID, string ICTicketNumber, DateTime? RequestedDateF, DateTime? RequestedDateT)
        //{
        //    List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
        //    PDMS_ICTicket W = null;
        //    try
        //    {
        //        DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
        //        DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
        //        DbParameter RequestedDateFP = provider.CreateParameter("RequestedDateF", RequestedDateF, DbType.DateTime);
        //        DbParameter RequestedDateTP = provider.CreateParameter("RequestedDateT", RequestedDateT, DbType.DateTime);

        //        DbParameter[] Params = new DbParameter[4] { DealerIDP, ICTicketNumberP, RequestedDateFP, RequestedDateTP };
        //        using (DataSet DataSet = provider.Select("ZDMS_GetDeviatedICTicketCommissioningForApproval", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    W = new PDMS_ICTicket();
        //                    Ws.Add(W);
        //                    W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
        //                    W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
        //                    W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
        //                    W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
        //                    W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
        //                    W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);

        //                    W.Equipment = new PDMS_EquipmentHeader();
        //                    W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };


        //                    if (dr["ServiceTypeID"] != DBNull.Value)
        //                    {
        //                        W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
        //                    }
        //                    if (dr["ServicePriorityID"] != DBNull.Value)
        //                    {
        //                        W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
        //                    }

        //                    W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

        //                    if (dr["ServiceStatusID"] != DBNull.Value)
        //                    {
        //                        W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
        //                    }
        //                    W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return Ws;
        //}
        //public DataTable GetDeviatedICTicketCommissioningReport(int? DealerID, string ICTicketNumber, DateTime? RequestedDateF, DateTime? RequestedDateT, int UserID)
        //{

        //    try
        //    {
        //        DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
        //        DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
        //        DbParameter RequestedDateFP = provider.CreateParameter("RequestedDateF", RequestedDateF, DbType.DateTime);
        //        DbParameter RequestedDateTP = provider.CreateParameter("RequestedDateT", RequestedDateT, DbType.DateTime);
        //        DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //        DbParameter[] Params = new DbParameter[5] { DealerIDP, ICTicketNumberP, RequestedDateFP, RequestedDateTP, UserIDP };
        //        using (DataSet DataSet = provider.Select("ZDMS_GetDeviatedICTicketCommissioningReport", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                return DataSet.Tables[0];
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return null;
        //}

        public DataTable GetCommissionMailTo(int RegionID, string EngineSerialNumberStartWith)
        {
            try
            {
                DbParameter RegionIDP = provider.CreateParameter("RegionID", RegionID, DbType.Int32);
                DbParameter EngineSerialNumberStartWithP = provider.CreateParameter("EngineSerialNumberStartWith", EngineSerialNumberStartWith, DbType.String);
                DbParameter[] Params = new DbParameter[2] { RegionIDP, EngineSerialNumberStartWithP };
                using (DataSet DataSet = provider.Select("ZDMS_GetCommissionMailTo", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public void InsertCommissionMailTo(long ICTicketID, string MailID, int UserID, Boolean Success, string Message)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter MailIDP = provider.CreateParameter("MailID", MailID, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter SuccessP = provider.CreateParameter("Success", Success, DbType.Boolean);
                DbParameter MessageP = provider.CreateParameter("Message", Message, DbType.String);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter[] Params = new DbParameter[5] { ICTicketIDP, MailIDP, UserIDP, SuccessP, MessageP };

                    provider.Insert("ZDMS_InsertICTicketCommissionMailTo", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_ICTicketTSIR", "InsertICTicketTSIRMailToVendor", ex);

            }
        }
        public DataTable GetICTicketCommissionMailTo(int? DealerID, DateTime? ICTicketDateF, DateTime? ICTicketDateT, string ICTicketNumber, String MachineSerialNumber)
        {
            DataTable dt = new DataTable();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                 
                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
                DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", string.IsNullOrEmpty(MachineSerialNumber) ? null : MachineSerialNumber, DbType.String);

                DbParameter[] Params = new DbParameter[5] { DealerIDP,  ICTicketDateFP, ICTicketDateTP, ICTicketNumberP, MachineSerialNumberP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketCommissionMailTo", Params))
                {
                    if (DataSet != null)
                    {
                        dt = DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return dt;
        }

    }
}
