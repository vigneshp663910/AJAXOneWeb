using DataAccess;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
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
    public class BDMS_Equipment
    {
        private IDataAccess provider;
        public BDMS_Equipment()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public int IntegrationEquipment(string[] EquipmentFiles)
        {
            TraceLogger.Log(DateTime.Now);
            PDMS_EquipmentJSON Equipment = new PDMS_EquipmentJSON();
            try
            {
                foreach (string file in EquipmentFiles)
                {

                    try
                    {
                        string json = File.ReadAllText(file);
                        JavaScriptSerializer ser = new JavaScriptSerializer();
                        Equipment = ser.Deserialize<PDMS_EquipmentJSON>(json);
                        foreach (PDMS_EquipmentResultsJSON EQ in Equipment.results)
                        {
                            DbParameter CustomerCodeP;
                            DbParameter EquipmentSerNo;
                            //    DbParameter ModelDescription;
                            DbParameter WarrantyExpiryDate;
                            DbParameter HMRDate;
                            DbParameter HMRValue;
                            DbParameter CounterObjectID;
                            string Customer = "";
                            foreach (PDMS_get_equip_details equip_details in EQ.get_equip_details)
                            {
                                Customer = equip_details.p_customer_id;
                            }
                            CustomerCodeP = provider.CreateParameter("CustomerCode", Customer, DbType.String);
                            foreach (PDMS_equip_eqipdetails eqipdetails in EQ.equip_eqipdetails)
                            {
                                if (eqipdetails.equipdet_warranty.Count() == 0)
                                {
                                    break;
                                    // WarrantyExpiryDate = provider.CreateParameter("WarrantyExpiryDate", DBNull.Value, DbType.String);
                                }
                                else
                                {
                                    string ExpiryDate = "";
                                    foreach (PDMS_equipdet_warranty warranty in eqipdetails.equipdet_warranty)
                                    {
                                        ExpiryDate = warranty.r_end_date;
                                    }
                                    if (string.IsNullOrEmpty(ExpiryDate))
                                    {
                                        break;
                                    }
                                    WarrantyExpiryDate = provider.CreateParameter("WarrantyExpiryDate", ExpiryDate, DbType.String);
                                }

                                EquipmentSerNo = provider.CreateParameter("EquipmentSerNo", eqipdetails.p_serial_num, DbType.String);
                                //  ModelDescription = provider.CreateParameter("ModelDescription", eqipdetails.r_description, DbType.String);

                                List<string> Model = new SDMS_ICTicket().getModelByProductID(eqipdetails.p_serial_num);
                                DbParameter ModelP = provider.CreateParameter("Model", Model[0], DbType.String);
                                DbParameter DivisionP = provider.CreateParameter("Division", Model[1], DbType.String);
                                if (eqipdetails.equipdet_counter.Count() == 0)
                                {
                                    CounterObjectID = provider.CreateParameter("CounterObjectID", DBNull.Value, DbType.String);
                                    HMRDate = provider.CreateParameter("HMRDate", DBNull.Value, DbType.String);
                                    HMRValue = provider.CreateParameter("HMRValue", DBNull.Value, DbType.String);
                                }
                                else
                                {
                                    string HMRDate_ = "";
                                    string HMRValue_ = "";
                                    string p_counter_obj_id_ = "";
                                    foreach (PDMS_equipdet_counter counter in eqipdetails.equipdet_counter)
                                    {
                                        p_counter_obj_id_ = counter.p_counter_obj_id;
                                        HMRDate_ = counter.r_read_date;
                                        HMRValue_ = counter.r_value;
                                    }
                                    CounterObjectID = provider.CreateParameter("CounterObjectID", p_counter_obj_id_, DbType.String);
                                    HMRDate = provider.CreateParameter("HMRDate", HMRDate_, DbType.String);
                                    HMRValue = provider.CreateParameter("HMRValue", HMRValue_, DbType.String);
                                }
                                DbParameter[] Params = new DbParameter[8] { CustomerCodeP, EquipmentSerNo, WarrantyExpiryDate, HMRDate, HMRValue, CounterObjectID, ModelP, DivisionP };
                                try
                                {
                                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                                    {
                                        provider.Insert("ZDMS_InsertOrUpdateEquipmentHeader", Params);
                                        scope.Complete();
                                    }
                                }
                                catch (SqlException sqlEx)
                                {
                                    new FileLogger().LogMessageService("BDMS_Equipment", "InsertOrUpdateEquipment", sqlEx);
                                    throw;
                                }
                                catch (Exception ex)
                                {
                                    new FileLogger().LogMessageService("BDMS_Equipment", " InsertOrUpdateEquipment", ex);
                                    throw;
                                }
                            }
                        }
                        File.Move(file, file.Replace("DCONNECT", "DCONNECT\\Processed"));
                    }
                    catch (Exception e1)
                    {
                        File.Move(file, file.Replace("DCONNECT", "DCONNECT\\FAILED"));
                        new FileLogger().LogMessageService("BDMS_Material", "IntegrationMaterial", e1);
                        throw e1;
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Material", "IntegrationMaterial", ex);
                throw ex;
            }
            if (Equipment.results != null)
                return Equipment.results.Count();
            else
                return 0;
        }
        public int IntegrationEquipmentByBapi(string EquipmentSerNo)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                try
                {
                    PDMS_Equipment Equipment = new SDMS_Equipment().getEquipmentFromCRM(EquipmentSerNo);

                    string ch = new NpgsqlServer().ExecuteScalar(" select p_serial_no from af_cust_mc_serial_no where p_bp_id = '" + Equipment.Customer.CustomerCode + "' and  p_serial_no = " + EquipmentSerNo);
                    if (string.IsNullOrEmpty(ch))
                    {
                        string Query = "INSERT INTO public.af_cust_mc_serial_no(s_establishment,s_tenant_id,p_bp_id,p_serial_no,s_created_on)  VALUES (1000," + 0 + ",'" + Equipment.Customer.CustomerCode + "','" + EquipmentSerNo + "',now() )";
                        new NpgsqlServer().ExecuteNonQuery(Query);
                    }
                    DbParameter HMRDate;
                    DbParameter EquipmentSerNoP = provider.CreateParameter("EquipmentSerNo", EquipmentSerNo, DbType.String);
                    DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", Equipment.Customer.CustomerCode, DbType.String);
                    DbParameter WarrantyExpiryDate = provider.CreateParameter("WarrantyExpiryDate", Equipment.WarrantyExpiryDate, DbType.DateTime);
                    DbParameter CounterObjectID = provider.CreateParameter("CounterObjectID", Equipment.CounterObjectID, DbType.String);
                    if (Equipment.CurrentHMRDate < Convert.ToDateTime("1947-01-01"))
                    {
                        HMRDate = provider.CreateParameter("HMRDate", DBNull.Value, DbType.DateTime);
                    }
                    else
                    {
                        HMRDate = provider.CreateParameter("HMRDate", Equipment.CurrentHMRDate, DbType.DateTime);
                    }
                    DbParameter HMRValue = provider.CreateParameter("HMRValue", Equipment.CurrentHMRValue, DbType.String);

                    List<string> Model = new SDMS_ICTicket().getModelByProductID(EquipmentSerNo);
                    DbParameter ModelP = provider.CreateParameter("Model", string.IsNullOrEmpty(Model[0]) ? null : Model[0], DbType.String);
                    DbParameter DivisionP = provider.CreateParameter("Division", Model[1], DbType.String);
                    DbParameter[] Params = new DbParameter[8] { CustomerCodeP, EquipmentSerNoP, WarrantyExpiryDate, HMRDate, HMRValue, CounterObjectID, ModelP, DivisionP };

                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        provider.Insert("ZDMS_InsertOrUpdateEquipmentHeader", Params);
                        scope.Complete();
                    }
                }
                catch (Exception e1)
                {
                    new FileLogger().LogMessageService("BDMS_Equipment", "IntegrationEquipmentByBapi", e1);
                    throw e1;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Equipment", "IntegrationEquipmentByBapi", ex);
                throw ex;
            }
            return 0;
        }
        public List<PDMS_EquipmentHeader> GetEquipment(long? EquipmentHeaderID, string EquipmentSerialNo)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_EquipmentHeader> Equipment = new List<PDMS_EquipmentHeader>();
            try
            {
                DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
                DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", string.IsNullOrEmpty(EquipmentSerialNo) ? null : EquipmentSerialNo, DbType.String);
                DbParameter[] Params = new DbParameter[2] { EquipmentHeaderIDP, EquipmentSerialNoP };

                PDMS_EquipmentHeader Equip = new PDMS_EquipmentHeader();
                using (DataSet ds = provider.Select("ZDMS_GetEquipment", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Equip = new PDMS_EquipmentHeader();
                            Equip.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                            Equip.Customer = new PDMS_Customer() { CustomerID = Convert.ToInt32(dr["CustomerID"]), CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            Equip.EquipmentModel = new PDMS_Model()
                            {
                                ModelID = Convert.ToInt32(dr["ModelID"]),
                                ModelCode = Convert.ToString(dr["ModelCode"]),
                                Model = Convert.ToString(dr["Model"]),
                                ModelDescription = Convert.ToString(dr["ModelDescription"]),
                                Division = new PDMS_Division() { DivisionID = Convert.ToInt32(dr["DivisionID"]), DivisionCode = Convert.ToString(dr["DivisionCode"]), DivisionDescription = Convert.ToString(dr["DivisionDescription"]) }
                            };
                            Equip.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            Equip.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                            Equipment.Add(Equip);
                        }
                    }
                }
                return Equipment;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "GetEquipment", ex);
                throw ex;
            }
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
                            };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.Address = new PDMS_Address();
                            W.Address.State = new PDMS_State() { State = Convert.ToString(dr["State"]) };
                            W.Address.District = new PDMS_District() { District = Convert.ToString(dr["District"]) };
                            W.Equipment = new PDMS_EquipmentHeader()
                            {
                                EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]),
                                EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) },
                                EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]),
                                EngineModel = Convert.ToString(dr["EngineModel"]),
                                EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]),
                                CorrectSMR = Convert.ToString(dr["CorrectSMR"]),
                                DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]),
                                CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]),
                                WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]),
                                CounterObjectID = Convert.ToString(dr["CounterObjectID"])

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
                                W.Technician = new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
                            }
                            else
                            {
                                W.Technician = new PUser();
                            }
                            W.Location = Convert.ToString(dr["Location"]);
                            if (dr["OfficeID"] != DBNull.Value)
                                W.DealerOffice = new PDMS_DealerOffice() { OfficeID = Convert.ToInt32(dr["OfficeID"]), OfficeName = Convert.ToString(dr["OfficeName"]), OfficeCode = Convert.ToString(dr["OfficeCode"]), OfficeName_OfficeCode = Convert.ToString(dr["OfficeName"]) + "-" + Convert.ToString(dr["OfficeCode"]) };

                            W.ReachedDate = dr["ReachedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ReachedDate"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            if (dr["ServicePriorityID"] != DBNull.Value)
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            if (dr["Category1ID"] != DBNull.Value)
                                W.Category1 = new PDMS_Category1()
                                {
                                    Category1ID = Convert.ToInt32(dr["Category1ID"]),
                                    Category1 = Convert.ToString(dr["Category1ID"]),
                                    Category1Code = Convert.ToString(dr["Category1Code"])
                                };
                            if (dr["Category2ID"] != DBNull.Value)
                                W.Category2 = new PDMS_Category2()
                                {
                                    Category2ID = Convert.ToInt32(dr["Category2ID"]),
                                    Category2 = Convert.ToString(dr["Category2"]),
                                    Category2Code = Convert.ToString(dr["Category2Code"])
                                };
                            if (dr["Category3ID"] != DBNull.Value)
                                W.Category3 = new PDMS_Category3()
                                {
                                    Category3ID = Convert.ToInt32(dr["Category3ID"]),
                                    Category3 = Convert.ToString(dr["Category3"]),
                                    Category3Code = Convert.ToString(dr["Category3Code"])
                                };
                            if (dr["Category4ID"] != DBNull.Value)
                                W.Category4 = new PDMS_Category4()
                                {
                                    Category4ID = Convert.ToInt32(dr["Category4ID"]),
                                    Category4 = Convert.ToString(dr["Category4"]),
                                    Category4Code = Convert.ToString(dr["Category4Code"])
                                };
                            if (dr["Category5ID"] != DBNull.Value)
                                W.Category5 = new PDMS_Category5()
                                {
                                    Category5ID = Convert.ToInt32(dr["Category5ID"]),
                                    Category5 = Convert.ToString(dr["Category5"]),
                                    Category5Code = Convert.ToString(dr["Category5Code"])
                                };
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
        public DataSet GetEquipmentHistory(long? EquipmentHeaderID, string EquipmentSerialNo)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_ICTicket> ICTickets = new List<PDMS_ICTicket>();
            try
            {
                DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
                DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", string.IsNullOrEmpty(EquipmentSerialNo) ? null : EquipmentSerialNo, DbType.String);
                DbParameter[] Params = new DbParameter[2] { EquipmentHeaderIDP, EquipmentSerialNoP };
                PDMS_ICTicket ICTicket = new PDMS_ICTicket();
                return provider.Select("ZDMS_GetEquipmentHistory", Params);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "GetEquipmentHistory", ex);
                throw ex;
            }
        }
        public DataSet GetEquipmentPopulationReport(int? DealerID, string EquipmentSerialNo, string Customer, DateTime? WarrantyStart, DateTime? WarrantyEnd, int? RegionID, int? StateID, int? DivisionID)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_ICTicket> ICTickets = new List<PDMS_ICTicket>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", string.IsNullOrEmpty(EquipmentSerialNo) ? null : EquipmentSerialNo, DbType.String);
                DbParameter CustomerP = provider.CreateParameter("Customer", string.IsNullOrEmpty(Customer) ? null : Customer, DbType.String);
                DbParameter WarrantyStartP = provider.CreateParameter("WarrantyStart", WarrantyStart, DbType.DateTime);
                DbParameter WarrantyEndP = provider.CreateParameter("WarrantyEnd", WarrantyEnd, DbType.DateTime);
                DbParameter RegionIDP = provider.CreateParameter("RegionID", RegionID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter DivisionIDP = provider.CreateParameter("DivisionID", DivisionID, DbType.Int32);

                DbParameter[] Params = new DbParameter[8] { DealerIDP, EquipmentSerialNoP, CustomerP, WarrantyStartP, WarrantyEndP, RegionIDP, StateIDP, DivisionIDP };
                PDMS_ICTicket ICTicket = new PDMS_ICTicket();
                return provider.Select("ZDMS_GetEquipmentPopulationReport", Params);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "GetEquipmentPopulationReport", ex);
                throw ex;
            }
        }
        public DataSet GetEquipmentPopulationReportForAE(string EquipmentSerialNo, string Customer, DateTime? WarrantyStart, DateTime? WarrantyEnd, int? RegionID, int? StateID, int? DivisionID)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_ICTicket> ICTickets = new List<PDMS_ICTicket>();
            try
            { 
                DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", string.IsNullOrEmpty(EquipmentSerialNo) ? null : EquipmentSerialNo, DbType.String);
                DbParameter CustomerP = provider.CreateParameter("Customer", string.IsNullOrEmpty(Customer) ? null : Customer, DbType.String);
                DbParameter WarrantyStartP = provider.CreateParameter("WarrantyStart", WarrantyStart, DbType.DateTime);
                DbParameter WarrantyEndP = provider.CreateParameter("WarrantyEnd", WarrantyEnd, DbType.DateTime);
                DbParameter RegionIDP = provider.CreateParameter("RegionID", RegionID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter DivisionIDP = provider.CreateParameter("DivisionID", DivisionID, DbType.Int32);

                DbParameter[] Params = new DbParameter[7] {  EquipmentSerialNoP, CustomerP, WarrantyStartP, WarrantyEndP, RegionIDP, StateIDP, DivisionIDP };
                PDMS_ICTicket ICTicket = new PDMS_ICTicket();
                return provider.Select("ZDMS_GetEquipmentPopulationReportForAE", Params);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "GetEquipmentPopulationReportForAE", ex);
                throw ex;
            }
        }

        public PDMS_EquipmentHeader GetEquipmentFromMBR(string EquipmentSerialNo)
        {
            TraceLogger.Log(DateTime.Now);
            PDMS_EquipmentHeader Equip = new PDMS_EquipmentHeader();
            try
            {
                DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", EquipmentSerialNo, DbType.String);
                DbParameter[] Params = new DbParameter[1] { EquipmentSerialNoP };
                using (DataSet ds = provider.Select("ZDMS_GetEquipmentFromMBR", Params))
                {
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables[0];
                        DataRow dr = ds.Tables[0].Rows[0];
                        Equip.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                        Equip.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                        Equip.TypeOfWheelAssembly = Convert.ToString(dr["TypeOfWheelAssembly"]);
                        Equip.ChassisSlNo = Convert.ToString(dr["ChassisSlNo"]);
                        Equip.ESN = Convert.ToString(dr["ESN"]);
                        Equip.Plant = Convert.ToString(dr["Plant"]);
                        Equip.Dispatch = Convert.ToString(dr["Dispatch"]);
                        Equip.SpecialVariants = Convert.ToString(dr["SpecialVariants"]);

                        Equip.ProductionStatus = Convert.ToString(dr["ProductionStatus"]);
                        Equip.VariantsFittingDate = dr["VariantsFittingDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["VariantsFittingDate"]);
                        Equip.EquipmentModel = new PDMS_Model()
                       {
                           ModelID = Convert.ToInt32(dr["ModelID"]),
                           ModelCode = Convert.ToString(dr["ModelCode"]),
                           Model = Convert.ToString(dr["Model"])
                       };
                    }
                }
                return Equip;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "GetEquipment", ex);
                throw ex;
            }
        }
        public Boolean InsertOrUpdateEquipment(PDMS_EquipmentHeader EQ, int UserID)
        {
            DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EQ.EquipmentHeaderID, DbType.Int64);
            DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", EQ.EquipmentSerialNo, DbType.String);
            DbParameter EquipmentModelIDP = provider.CreateParameter("EquipmentModelID", EQ.EquipmentModel.ModelID, DbType.Int32);
            DbParameter EngineSerialNoP = provider.CreateParameter("EngineSerialNo", EQ.EngineSerialNo, DbType.String);
            DbParameter TypeOfWheelAssemblyP = provider.CreateParameter("TypeOfWheelAssembly", EQ.TypeOfWheelAssembly, DbType.String);
            DbParameter MaterialID = provider.CreateParameter("MaterialID", EQ.Material.MaterialID, DbType.String);
            DbParameter ChassisSlNoP = provider.CreateParameter("ChassisSlNo", EQ.ChassisSlNo, DbType.String);
            DbParameter ESNP = provider.CreateParameter("ESN", EQ.ESN, DbType.String);
            DbParameter PlantP = provider.CreateParameter("Plant", EQ.Plant, DbType.String);
            DbParameter Dispatch = provider.CreateParameter("Dispatch", EQ.Dispatch, DbType.String);
            DbParameter SpecialVariantsP = provider.CreateParameter("SpecialVariants", EQ.SpecialVariants, DbType.String);
            DbParameter ProductionStatusP = provider.CreateParameter("ProductionStatus", EQ.ProductionStatus, DbType.String);
            DbParameter VariantsFittingDateP = provider.CreateParameter("VariantsFittingDate", EQ.VariantsFittingDate, DbType.DateTime);
            DbParameter ManufacturingDate = provider.CreateParameter("ManufacturingDate", EQ.ManufacturingDate, DbType.DateTime);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            DbParameter[] Params = new DbParameter[15] { EquipmentHeaderIDP, EquipmentSerialNoP,EquipmentModelIDP,EngineSerialNoP,TypeOfWheelAssemblyP,MaterialID,ChassisSlNoP
                ,ESNP,PlantP,Dispatch,SpecialVariantsP,ProductionStatusP,VariantsFittingDateP,ManufacturingDate,UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("InsertOrUpdateEquipment", Params);
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
        public List<PDMS_EquipmentHeader> GetEquipmentNotSold(long? EquipmentHeaderID, string EquipmentSerialNo, int? DivisionID)
        {
            List<PDMS_EquipmentHeader> Equipments = new List<PDMS_EquipmentHeader>();
            PDMS_EquipmentHeader Equipment = null;
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
                DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", string.IsNullOrEmpty(EquipmentSerialNo) ? null : EquipmentSerialNo, DbType.String);
                DbParameter DivisionIDP = provider.CreateParameter("DivisionID", DivisionID, DbType.Int32);

                DbParameter[] Params = new DbParameter[3] { EquipmentHeaderIDP, EquipmentSerialNoP, DivisionIDP };

                using (DataSet ds = provider.Select("ZDMS_GetEquipmentNotSold", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Equipment = new PDMS_EquipmentHeader();
                            Equipments.Add(Equipment);
                            Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                            Equipment.EquipmentModel = new PDMS_Model()
                            {
                                ModelID = Convert.ToInt32(dr["ModelID"]),
                                ModelCode = Convert.ToString(dr["ModelCode"]),
                                Model = Convert.ToString(dr["Model"]),
                                ModelDescription = Convert.ToString(dr["ModelDescription"]),
                                Division = new PDMS_Division() { DivisionID = Convert.ToInt32(dr["DivisionID"]), DivisionCode = Convert.ToString(dr["DivisionCode"]), DivisionDescription = Convert.ToString(dr["DivisionDescription"]) }
                            };

                            Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            Equipment.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);

                            Equipment.Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["MaterialCode"]) };
                            Equipment.TypeOfWheelAssembly = Convert.ToString(dr["TypeOfWheelAssembly"]);
                            Equipment.ChassisSlNo = Convert.ToString(dr["ChassisSlNo"]);
                            Equipment.ESN = Convert.ToString(dr["ESN"]);
                            Equipment.Plant = Convert.ToString(dr["Plant"]);

                            Equipment.Dispatch = Convert.ToString(dr["Dispatch"]);
                            //  Equipment.SpecialVariants = Convert.ToString(dr["ModelCode"]);
                            //  Equipment.ProductionStatus = Convert.ToString(dr["ModelCode"]);
                            //  Equipment.VariantsFittingDate = string.IsNullOrEmpty(txtVariantsFittingDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtVariantsFittingDate.Text.Trim());
                            Equipment.ManufacturingDate = dr["ManufacturingDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ManufacturingDate"]);
                        }
                    }
                }
                return Equipments;
                //  return provider.Select("ZDMS_GetEquipmentNotSold", Params).Tables[0];
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("GetEquipmentNotSold", "GetEquipmentHistory", ex);
                throw ex;
            }
        }

        public Boolean InsertOrUpdateEquipment(long EquipmentHeaderID, string EquipmentModelNumber, string EngineModel, string EngineSerialNo, int UserID)
        {
            DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
            DbParameter EquipmentModelNumberP = provider.CreateParameter("EquipmentModelNumber", EquipmentModelNumber, DbType.String);
            DbParameter EngineModelP = provider.CreateParameter("EngineModel", EngineModel, DbType.String);
            DbParameter EngineSerialNoP = provider.CreateParameter("EngineSerialNo", EngineSerialNo, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[5] { EquipmentHeaderIDP, EquipmentModelNumberP, EngineModelP, EngineSerialNoP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateEquipmentHeaderEquipmentModelNumber", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "InsertOrUpdateEquipment", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", " InsertOrUpdateEquipment", ex);
                return false;
            }
            return true;
        }
        public Boolean UpdateICTicketHMR(long ICTicketID, int HMR, int UserID)
        {
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
            DbParameter HMRP = provider.CreateParameter("HMR", HMR, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[3] { ICTicketIDP, HMRP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateICTicketHMR", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "UpdateICTicketHMR", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "UpdateICTicketHMR", ex);
                return false;
            }
            return true;
        }
        public Boolean UpdateCommissioningDate(long EquipmentHeaderID, DateTime CommissioningOn, int UserID)
        {
            DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
            DbParameter CommissioningOnP = provider.CreateParameter("CommissioningOn", CommissioningOn, DbType.DateTime);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[3] { EquipmentHeaderIDP, CommissioningOnP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateCommissioningDate", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "UpdateCommissioningDate", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "UpdateCommissioningDate", ex);
                return false;
            }
            return true;
        }


        public void IntegrationEquipmentFromSAP()
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                List<PDMS_Equipment> Equipment = new SDMS_Equipment().getEquipmentFromSAP();
                List<string> DeliveryNos = new List<string>();
                foreach (PDMS_Equipment Equ in Equipment)
                {
                    DbParameter EquipmentSerialNo = provider.CreateParameter("EquipmentSerialNo", Equ.EquipmentSerialNo, DbType.String);
                    DbParameter EngineSerialNo = provider.CreateParameter("EngineSerialNo", Equ.EngineSerialNo, DbType.String);
                    DbParameter InstalledBaseNo = provider.CreateParameter("InstalledBaseNo", Equ.Ibase.InstalledBaseNo, DbType.String);
                    DbParameter IBaseCreatedOn = provider.CreateParameter("IBaseCreatedOn", Equ.Ibase.IBaseCreatedOn, DbType.DateTime);
                    DbParameter IBaseLocation = provider.CreateParameter("IBaseLocation", Equ.Ibase.IBaseLocation, DbType.String);
                    DbParameter MajorRegion = provider.CreateParameter("MajorRegion", Equ.Ibase.MajorRegion.Region, DbType.String);
                    DbParameter Item = provider.CreateParameter("Item", Equ.Ibase.Item, DbType.Int32);
                    DbParameter DeliveryNo = provider.CreateParameter("DeliveryNo", Equ.Ibase.DeliveryNo, DbType.String);
                    DbParameter DeliveryDate = provider.CreateParameter("DeliveryDate", Equ.Ibase.DeliveryDate, DbType.DateTime);
                    DbParameter ProductCode = provider.CreateParameter("ProductCode", Equ.Ibase.ProductCode, DbType.String);
                    DbParameter MaterialCode = provider.CreateParameter("MaterialCode", Equ.Material.MaterialCodeWithOutZero, DbType.String);

                    long? CustomerID = null;
                    if (!string.IsNullOrEmpty(Equ.Customer.CustomerCodeWithOutZero))
                    {
                        List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerByCode(null, Equ.Customer.CustomerCodeWithOutZero);
                        if (Customer.Count == 0)
                        {
                            CustomerID = new BDMS_Customer().InsertOrUpdateCustomerSap(Equ.Customer.CustomerCodeWithOutZero);
                        }
                        else
                        {
                            CustomerID = Customer[0].CustomerID;
                        }
                    }

                    long? ShipToPartyID = null;
                    if (!string.IsNullOrEmpty(Equ.Ibase.ShipToParty.CustomerCodeWithOutZero))
                    {
                        List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerByCode(null, Equ.Ibase.ShipToParty.CustomerCodeWithOutZero);
                        if (Customer.Count == 0)
                        {
                            ShipToPartyID = new BDMS_Customer().InsertOrUpdateCustomerSap(Equ.Customer.CustomerCodeWithOutZero);
                        }
                        else
                        {
                            ShipToPartyID = Customer[0].CustomerID;
                        }
                    }

                    long? Buyer1stID = null;
                    if (!string.IsNullOrEmpty(Equ.Ibase.Buyer1st.CustomerCodeWithOutZero))
                    {
                        List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerByCode(null, Equ.Ibase.Buyer1st.CustomerCodeWithOutZero);

                        if (Customer.Count == 0)
                        {
                            Buyer1stID = new BDMS_Customer().InsertOrUpdateCustomerSap(Equ.Customer.CustomerCodeWithOutZero);
                        }
                        else
                        {
                            Buyer1stID = Customer[0].CustomerID;
                        }
                    }
                    long? Buyer2ndID = null;
                    if (!string.IsNullOrEmpty(Equ.Ibase.Buyer2nd.CustomerCodeWithOutZero))
                    {
                        List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerByCode(null, Equ.Ibase.Buyer2nd.CustomerCodeWithOutZero);
                        if (Customer.Count == 0)
                        {
                            Buyer2ndID = new BDMS_Customer().InsertOrUpdateCustomerSap(Equ.Customer.CustomerCodeWithOutZero);
                        }
                        else
                        {
                            Buyer2ndID = Customer[0].CustomerID;
                        }
                    }

                    DbParameter CustomerCode = provider.CreateParameter("CustomerID", CustomerID, DbType.Int32);
                    DbParameter ShipToParty = provider.CreateParameter("ShipToPartyID", ShipToPartyID, DbType.Int32);
                    DbParameter ShipToPartyDealer = provider.CreateParameter("ShipToPartyDealer", Equ.Ibase.ShipToPartyDealer.DealerCode, DbType.String);
                    DbParameter SoleToDealer = provider.CreateParameter("SoleToDealer", Equ.Ibase.SoleToDealer.DealerCode, DbType.String);
                    DbParameter Buyer1st = provider.CreateParameter("Buyer1stID", Buyer1stID, DbType.Int32);
                    DbParameter Buyer2nd = provider.CreateParameter("Buyer2ndID", Buyer2ndID, DbType.Int32);

                    DbParameter WarrantyStart = provider.CreateParameter("WarrantyStart", Equ.Ibase.WarrantyStart, DbType.DateTime);
                    DbParameter WarrantyEnd = provider.CreateParameter("WarrantyEnd", Equ.Ibase.WarrantyEnd, DbType.DateTime);
                    DbParameter FinancialYearOfDispatch = provider.CreateParameter("FinancialYearOfDispatch", Equ.Ibase.FinancialYearOfDispatch, DbType.Int32);

                    List<string> Model = new SDMS_ICTicket().getModelByProductID(Equ.EquipmentSerialNo);
                    DbParameter ModelP = provider.CreateParameter("Model", string.IsNullOrEmpty(Model[0]) ? null : Model[0], DbType.String);
                    DbParameter DivisionP = provider.CreateParameter("Division", Model[1], DbType.String);


                    DbParameter UpdateOn = provider.CreateParameter("UpdateOn", Equ.Ibase.UpdateOn, DbType.DateTime);

                    DbParameter[] Params = new DbParameter[23] { EquipmentSerialNo,EngineSerialNo,
                        InstalledBaseNo, IBaseCreatedOn,IBaseLocation,MajorRegion,  DeliveryNo, Item, DeliveryDate, ProductCode,  MaterialCode,
                    CustomerCode,ShipToParty,ShipToPartyDealer,SoleToDealer,Buyer1st,Buyer2nd,
                    WarrantyStart,WarrantyEnd,FinancialYearOfDispatch,UpdateOn ,ModelP,DivisionP};
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            provider.Insert("ZDMS_InsertOrUpdateEquipmentFromSAP", Params);
                            scope.Complete();
                        }
                        DeliveryNos.Add(Equ.Ibase.DeliveryNo);
                    }
                    catch (SqlException sqlEx)
                    {
                        new FileLogger().LogMessageService("BDMS_Equipment", "IntegrationEquipmentFromSAP", sqlEx); 
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessageService("BDMS_Equipment", " IntegrationEquipmentFromSAP", ex); 
                    }
                }
                new SDMS_Equipment().UpdateICTicketRequestedDateToSAP(DeliveryNos);
            }
            catch (Exception e11)
            {
                new FileLogger().LogMessageService("BDMS_Material", "IntegrationEquipmentFromSAP", e11);
                throw e11;
            }

            TraceLogger.Log(DateTime.Now);
        }
    }
}
    
