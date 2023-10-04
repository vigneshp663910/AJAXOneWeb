using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.UI.WebControls;

namespace Business
{
    public class BDMS_ICTicketTSIR
    {
        private IDataAccess provider;
        public BDMS_ICTicketTSIR()
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
        public PApiResult GetICTicketTSIR(long? ICTicketID, int? DealerID, string CustomerCode, string TsirNo, DateTime? TsirDateF, DateTime? TsirDateT
           , string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, String SroCode, int? TechnicianID, int? TypeOfWarrantyID, int? ModelID
            , String MachineSerialNumber, int? TsirStatusID, int? PageIndex = null, int? PageSize = null)
        {


            TraceLogger.Log(DateTime.Now);
            string endPoint = "ICTicketTsir/GetICTicketTSIR?ICTicketID=" + ICTicketID + "&DealerID=" + DealerID + "&CustomerCode=" + CustomerCode + "&TsirNo=" + TsirNo + "&TsirDateF=" + TsirDateF + "&TsirDateT=" + TsirDateT
                + "&ICTicketNumber=" + ICTicketNumber + "&ICTicketDateF=" + ICTicketDateF + "&ICTicketDateT=" + ICTicketDateT + "&SroCode=" + SroCode
                + "&TechnicianID=" + TechnicianID + "&TypeOfWarrantyID=" + TypeOfWarrantyID + "&ModelID=" + ModelID + "&MachineSerialNumber=" + MachineSerialNumber 
                + "&TsirStatusID=" + TsirStatusID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return  JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)) ;
        }
        public DataTable GetICTicketTSIRExcel(long? ICTicketID, int? DealerID, string CustomerCode, string TsirNo, DateTime? TsirDateF, DateTime? TsirDateT
        , string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, String SroCode, int? TechnicianID, int? TypeOfWarrantyID, int? ModelID
         , String MachineSerialNumber, int? TsirStatusID)
        {


            TraceLogger.Log(DateTime.Now);
            string endPoint = "ICTicketTsir/GetICTicketTSIRExcel?ICTicketID=" + ICTicketID + "&DealerID=" + DealerID + "&CustomerCode=" + CustomerCode + "&TsirNo=" + TsirNo + "&TsirDateF=" + TsirDateF + "&TsirDateT=" + TsirDateT
                + "&ICTicketNumber=" + ICTicketNumber + "&ICTicketDateF=" + ICTicketDateF + "&ICTicketDateT=" + ICTicketDateT + "&SroCode=" + SroCode
                + "&TechnicianID=" + TechnicianID + "&TypeOfWarrantyID=" + TypeOfWarrantyID + "&ModelID=" + ModelID + "&MachineSerialNumber=" + MachineSerialNumber
                + "&TsirStatusID=" + TsirStatusID;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public PDMS_ICTicketTSIR GetICTicketTSIRByTsirID(long? TsirID, long? ServiceChargeID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "ICTicketTsir/GetICTicketTSIRByTsirID?TsirID=" + TsirID + "&ServiceChargeID=" + ServiceChargeID;
            return JsonConvert.DeserializeObject<PDMS_ICTicketTSIR>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }
        public List<PDMS_ICTicketTSIR> GetICTicketTSIRBasicDetails(long? ICTicketID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "ICTicketTsir/ICTicketTSIRBasicDetails?ICTicketID=" + ICTicketID ;
            return JsonConvert.DeserializeObject<List<PDMS_ICTicketTSIR>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }

        //public List<PDMS_ICTicketTSIR> GetICTicketTSIR(int? DealerID, string CustomerCode, string TsirNo, DateTime? TsirDateF, DateTime? TsirDateT
        //    , string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, String SroCode, int? TechnicianID, int? TypeOfWarrantyID, int? ModelID, String MachineSerialNumber, int? TsirStatusID)
        //{
        //    List<PDMS_ICTicketTSIR> Ws = new List<PDMS_ICTicketTSIR>();
        //    PDMS_ICTicketTSIR W = null;
        //    try
        //    {
        //        DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
        //        DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);

        //        DbParameter TsirNoP = provider.CreateParameter("TsirNo", string.IsNullOrEmpty(TsirNo) ? null : TsirNo, DbType.String);
        //        DbParameter TsirDateFP = provider.CreateParameter("TsirDateF", TsirDateF, DbType.DateTime);
        //        DbParameter TsirDateTP = provider.CreateParameter("TsirDateT", TsirDateT, DbType.DateTime);

        //        DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
        //        DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
        //        DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

        //        DbParameter SroCodeP = provider.CreateParameter("SroCode", string.IsNullOrEmpty(SroCode) ? null : SroCode, DbType.String);
        //        DbParameter TechnicianIDP = provider.CreateParameter("TechnicianID", TechnicianID, DbType.Int32);

        //        DbParameter TypeOfWarrantyIDP = provider.CreateParameter("TypeOfWarrantyID", TypeOfWarrantyID, DbType.Int32);
        //        DbParameter ModelIDP = provider.CreateParameter("ModelID", ModelID, DbType.Int32);
        //        DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", string.IsNullOrEmpty(MachineSerialNumber) ? null : MachineSerialNumber, DbType.String);
        //        DbParameter TsirStatusIDP = provider.CreateParameter("TsirStatusID", TsirStatusID, DbType.Int32);

        //        DbParameter[] Params = new DbParameter[14] { DealerIDP, CustomerCodeP,TsirNoP,TsirDateFP,TsirDateTP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP,
        //            SroCodeP, TechnicianIDP, TypeOfWarrantyIDP, ModelIDP, MachineSerialNumberP,TsirStatusIDP };
        //        using (DataSet DataSet = provider.Select("ZDMS_GetICTicketTSIR", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    W = new PDMS_ICTicketTSIR();
        //                    Ws.Add(W);
        //                    W.TsirID = Convert.ToInt64(dr["TsirID"]);
        //                    W.TsirNumber = Convert.ToString(dr["TsirNumber"]);
        //                    W.TsirDate = Convert.ToDateTime(dr["TsirDate"]);
        //                    W.NatureOfFailures = Convert.ToString(dr["NatureOfFailures"]);
        //                    W.QualityComments = Convert.ToString(dr["QualityComments"]);
        //                    W.ServiceComments = Convert.ToString(dr["ServiceComments"]);

        //                    W.ICTicket = new PDMS_ICTicket();
        //                    W.ICTicket.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
        //                    W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
        //                    W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);

        //                    W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
        //                    W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };

        //                    W.ICTicket.Equipment = new PDMS_EquipmentHeader();
        //                    W.ICTicket.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
        //                    W.ICTicket.Equipment.EquipmentModel = new PDMS_Model()
        //                    {
        //                        Model = Convert.ToString(dr["EquipmentModel"]),
        //                        Division = new PDMS_Division() { DivisionID = Convert.ToInt32(dr["DivisionID"]) }
        //                    };
        //                    W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);

        //                    // W.ICTicket.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
        //                    // W.ICTicket.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
        //                    // W.ICTicket.Equipment.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);
        //                    // W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
        //                    // W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);

        //                    // if (dr["ServiceTypeID"] != DBNull.Value)
        //                    // {
        //                    //    W.ICTicket.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
        //                    // }
        //                    // if (dr["ServicePriorityID"] != DBNull.Value)
        //                    // {
        //                    //    W.ICTicket.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
        //                    // }
        //                    // W.ICTicket.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

        //                    // if (dr["ServiceStatusID"] != DBNull.Value)
        //                    // {
        //                    //    W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
        //                    // }
        //                    // W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
        //                    // W.ICTicket.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);

        //                    // W.ICTicket.RegisteredBy = new PUser();
        //                    // if (dr["RegisteredByID"] != DBNull.Value)
        //                    //W.ICTicket.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

        //                    W.ICTicket.Technician = dr["TechnicianID"] == DBNull.Value ? new PUser() : new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
        //                    W.ICTicket.TypeOfWarranty = dr["TypeOfWarrantyID"] == DBNull.Value ? null : new PDMS_TypeOfWarranty() { TypeOfWarrantyID = Convert.ToInt32(dr["TypeOfWarrantyID"]), TypeOfWarranty = Convert.ToString(dr["TypeOfWarranty"]) };
        //                    W.ICTicket.CurrentHMRValue = dr["CurrentHMRValue"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CurrentHMRValue"]);
        //                    W.Status = new PDMS_ICTicketTSIRStatus() { StatusID = Convert.ToInt32(dr["StatusID"]), Status = Convert.ToString(dr["Status"]) };

        //                    W.ICTicket.FSR = dr["FSRDate"] == DBNull.Value ? null : new PDMS_ICTicketFSR() { FSRNumber = Convert.ToString(dr["FSRNumber"]), FSRDate = Convert.ToDateTime(dr["FSRDate"]) };
        //                    W.ICTicket.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
        //                    W.ICTicket.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
        //                    W.FailureDetails = Convert.ToString(dr["FailureDetails"]);
        //                    W.PointsChecked = Convert.ToString(dr["PointsChecked"]);
        //                    W.PossibleRootCauses = Convert.ToString(dr["PossibleRootCauses"]);
        //                    W.ICTicket.RestoreDate = dr["RestoreDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
        //                    W.ICTicket.MainApplication = new PDMS_MainApplication() { MainApplication = Convert.ToString(dr["MainApplication"]) };
        //                    W.ICTicket.Address = new PDMS_Address()
        //                        {
        //                            State = new PDMS_State() { State = Convert.ToString(dr["State"]) },
        //                            District = new PDMS_District() { District = Convert.ToString(dr["District"]) }
        //                        };
        //                    W.ICTicket.Location = Convert.ToString(dr["Location"]);
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

        public List<PDMS_ICTicketTSIR> GetICTicketTSIRDetailReport(int? DealerID, string CustomerCode, string TsirNo, DateTime? TsirDateF, DateTime? TsirDateT
           , string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, String SroCode, int? TechnicianID, int? TypeOfWarrantyID, int? ModelID, String MachineSerialNumber, int? TsirStatusID)
        {
            List<PDMS_ICTicketTSIR> Ws = new List<PDMS_ICTicketTSIR>();
            PDMS_ICTicketTSIR W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);

                DbParameter TsirNoP = provider.CreateParameter("TsirNo", string.IsNullOrEmpty(TsirNo) ? null : TsirNo, DbType.String);
                DbParameter TsirDateFP = provider.CreateParameter("TsirDateF", TsirDateF, DbType.DateTime);
                DbParameter TsirDateTP = provider.CreateParameter("TsirDateT", TsirDateT, DbType.DateTime);

                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

                DbParameter SroCodeP = provider.CreateParameter("SroCode", string.IsNullOrEmpty(SroCode) ? null : SroCode, DbType.String);
                DbParameter TechnicianIDP = provider.CreateParameter("TechnicianID", TechnicianID, DbType.Int32);

                DbParameter TypeOfWarrantyIDP = provider.CreateParameter("TypeOfWarrantyID", TypeOfWarrantyID, DbType.Int32);
                DbParameter ModelIDP = provider.CreateParameter("ModelID", ModelID, DbType.Int32);
                DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", string.IsNullOrEmpty(MachineSerialNumber) ? null : MachineSerialNumber, DbType.String);
                DbParameter TsirStatusIDP = provider.CreateParameter("TsirStatusID", TsirStatusID, DbType.Int32);

                DbParameter[] Params = new DbParameter[14] { DealerIDP, CustomerCodeP,TsirNoP,TsirDateFP,TsirDateTP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP,
                    SroCodeP, TechnicianIDP, TypeOfWarrantyIDP, ModelIDP, MachineSerialNumberP,TsirStatusIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketTSIRDetailReport", Params))
                {
                    if (DataSet != null)
                    {
                        long TsirID = 0;
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            if (TsirID != Convert.ToInt64(dr["TsirID"]))
                            {
                                W = new PDMS_ICTicketTSIR();
                                Ws.Add(W);
                                W.TsirID = Convert.ToInt64(dr["TsirID"]);
                                W.TsirNumber = Convert.ToString(dr["TsirNumber"]);
                                W.TsirDate = Convert.ToDateTime(dr["TsirDate"]);
                                W.NatureOfFailures = Convert.ToString(dr["NatureOfFailures"]);
                                W.FailureDetails = Convert.ToString(dr["FailureDetails"]);
                                W.PointsChecked = Convert.ToString(dr["PointsChecked"]);
                                W.PossibleRootCauses = Convert.ToString(dr["PossibleRootCauses"]);

                                W.ServiceCharge = new PDMS_ServiceCharge()
                                {
                                    Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["ServiceCode"]), MaterialDescription = Convert.ToString(dr["ServiceDescription"]) },
                                    CountOverall = dr["CountOverall"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CountOverall"]),
                                    CountBasedMC = dr["CountBasedMC"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CountBasedMC"])
                                };

                                W.ICTicket = new PDMS_ICTicket();
                                W.ICTicket.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                                W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                                W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);


                                W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                                W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };

                                W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                                W.ICTicket.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                                W.ICTicket.Equipment.EquipmentModel = new PDMS_Model()
                                {
                                    Model = Convert.ToString(dr["EquipmentModel"]),
                                    Division = new PDMS_Division() { DivisionID = Convert.ToInt32(dr["DivisionID"]) }
                                };
                                W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                                W.ICTicket.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                                W.ICTicket.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);

                                W.ICTicket.Technician = new PUser() { ContactName = Convert.ToString(dr["TechnicianName"]) };
                                W.ICTicket.TypeOfWarranty = new PDMS_TypeOfWarranty() { TypeOfWarranty = Convert.ToString(dr["TypeOfWarranty"]) };
                                W.ICTicket.CurrentHMRValue = dr["CurrentHMRValue"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CurrentHMRValue"]);
                                W.Status = new PDMS_ICTicketTSIRStatus() { Status = Convert.ToString(dr["Status"]) };

                                W.ICTicket.FSR = dr["FSRDate"] == DBNull.Value ? null : new PDMS_ICTicketFSR() { FSRNumber = Convert.ToString(dr["FSRNumber"]), FSRDate = Convert.ToDateTime(dr["FSRDate"]) };
                               
                               
                                W.ICTicket.RestoreDate = dr["RestoreDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                                W.ICTicket.MainApplication = new PDMS_MainApplication() { MainApplication = Convert.ToString(dr["MainApplication"]) };
                                W.ICTicket.Address = new PDMS_Address()
                                {
                                    State = new PDMS_State() { State = Convert.ToString(dr["State"]) },
                                    District = new PDMS_District() { District = Convert.ToString(dr["District"]) }
                                };
                                W.ICTicket.Location = Convert.ToString(dr["Location"]);
                                W.SMaterials = new List<PDMS_ServiceMaterial>();
                                TsirID = Convert.ToInt64(dr["TsirID"]);
                            }
                            if (dr["ServiceMaterialID"] != DBNull.Value)
                            {
                                W.SMaterials.Add(new PDMS_ServiceMaterial()
                                {
                                    ServiceMaterialID = Convert.ToInt64(dr["ServiceMaterialID"]),
                                    Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["MaterialCode"]), MaterialDescription = Convert.ToString(dr["MaterialDescription"]) },
                                    Qty = Convert.ToInt32(dr["Qty"]),
                                    //  IsFaultyPart = Convert.ToBoolean(dr["IsFaultyPart"]),
                                    //  BasePrice = Convert.ToDecimal(dr["BasePrice"]),
                                    //  Discount = Convert.ToDecimal(dr["Discount"]),
                                    //  SGST = Convert.ToInt32(dr["SGST"]),
                                    //  IGST = Convert.ToInt32(dr["IGST"]),
                                    //  SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                    //  IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                                    //  QuotationNumber = Convert.ToString(dr["QuotationNumber"]),
                                    DeliveryNumber = Convert.ToString(dr["DeliveryNumber"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            return Ws;
        }
        public long InsertOrUpdateICTicketTSIR(PDMS_ICTicketTSIR TSIR, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            long EmployeeID = 0;
            try
            {
                DbParameter TsirID = provider.CreateParameter("TsirID", TSIR.TsirID, DbType.Int64);
                DbParameter ServiceChargeID = provider.CreateParameter("ServiceChargeID", TSIR.ServiceCharge.ServiceChargeID, DbType.Int64);
                //  DbParameter FailureMaterialID = provider.CreateParameter("FailureMaterialID", TSIR.Material.MaterialID, DbType.Int64);
                DbParameter ICTicketID = provider.CreateParameter("ICTicketID", TSIR.ICTicket.ICTicketID, DbType.Int64);
                //  DbParameter FailureRepeats = provider.CreateParameter("FailureRepeats", TSIR.FailureRepeats, DbType.String);
                DbParameter NatureOfFailures = provider.CreateParameter("NatureOfFailures", TSIR.NatureOfFailures, DbType.String);
                DbParameter ProblemNoticedBy = provider.CreateParameter("ProblemNoticedBy", TSIR.ProblemNoticedBy, DbType.String);
                DbParameter UnderWhatConditionFailureTaken = provider.CreateParameter("UnderWhatConditionFailureTaken", TSIR.UnderWhatConditionFailureTaken, DbType.String);
                DbParameter FailureDetails = provider.CreateParameter("FailureDetails", TSIR.FailureDetails, DbType.String);
                DbParameter PointsChecked = provider.CreateParameter("PointsChecked", TSIR.PointsChecked, DbType.String);
                DbParameter PossibleRootCauses = provider.CreateParameter("PossibleRootCauses", TSIR.PossibleRootCauses, DbType.String);
                DbParameter SpecificPointsNoticed = provider.CreateParameter("SpecificPointsNoticed", TSIR.SpecificPointsNoticed, DbType.String);
                DbParameter PartsInvoiceNumber = provider.CreateParameter("PartsInvoiceNumber", TSIR.PartsInvoiceNumber, DbType.String);
                // DbParameter HOComments = provider.CreateParameter("HOComments", TSIR.HOComments, DbType.String);
                //  DbParameter SERecommendedParts = provider.CreateParameter("SERecommendedParts", TSIR.SERecommendedParts, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter OutValueDParam = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter[] Params = new DbParameter[13] {TsirID,ServiceChargeID, ICTicketID,  NatureOfFailures, ProblemNoticedBy, UnderWhatConditionFailureTaken, 
                      FailureDetails,PointsChecked, PossibleRootCauses, SpecificPointsNoticed,PartsInvoiceNumber, UserIDP,OutValueDParam};

                    provider.Insert("ZDMS_InsertOrUpdateICTicketTSIR", Params);
                    scope.Complete();
                    EmployeeID = Convert.ToInt64(OutValueDParam.Value);
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_ICTicketTSIR", "InsertOrUpdateICTicketTSIR", ex);
                return 0;
            }
            return EmployeeID;
        }
        //public PDMS_ICTicketTSIR GetICTicketTSIRByTsirID(long? TsirID, long? ServiceChargeID)
        //{
        //    PDMS_ICTicketTSIR W = new PDMS_ICTicketTSIR();
        //    try
        //    {
        //        DbParameter TsirIDP = provider.CreateParameter("TsirID", TsirID, DbType.Int64);
        //        DbParameter ServiceChargeIDP = provider.CreateParameter("ServiceChargeID", ServiceChargeID, DbType.Int64);
        //        DbParameter[] Params = new DbParameter[2] { TsirIDP, ServiceChargeIDP };
        //        using (DataSet DataSet = provider.Select("ZDMS_GetICTicketTSIRByTsirID", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    W.TsirID = Convert.ToInt64(dr["TsirID"]);
        //                    W.TsirNumber = Convert.ToString(dr["TsirNumber"]);
        //                    W.TsirDate = Convert.ToDateTime(dr["TsirDate"]);
        //                    //W.FailureRepeats = Convert.ToString(dr["FailureRepeats"]);
        //                    W.NatureOfFailures = Convert.ToString(dr["NatureOfFailures"]);
        //                    W.ProblemNoticedBy = Convert.ToString(dr["ProblemNoticedBy"]);
        //                    W.UnderWhatConditionFailureTaken = Convert.ToString(dr["UnderWhatConditionFailureTaken"]);
        //                    W.FailureDetails = Convert.ToString(dr["FailureDetails"]);
        //                    W.PointsChecked = Convert.ToString(dr["PointsChecked"]);
        //                    W.PossibleRootCauses = Convert.ToString(dr["PossibleRootCauses"]);
        //                    W.SpecificPointsNoticed = Convert.ToString(dr["SpecificPointsNoticed"]);
        //                    W.PartsInvoiceNumber = Convert.ToString(dr["PartsInvoiceNumber"]);

        //                    W.Sales1ApproveAmount = dr["Sales1ApproveAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Sales1ApproveAmount"]);
        //                    W.Sales2ApproveAmount = dr["Sales2ApproveAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Sales2ApproveAmount"]);
                           

        //                    //W.SERecommendedParts = Convert.ToString(dr["SERecommendedParts"]);

        //                    W.ServiceCharge = new PDMS_ServiceCharge()
        //                    {
        //                        Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["ServiceMaterial"]), MaterialDescription = Convert.ToString(dr["ServiceMaterialDescription"]) },

        //                    };
        //                    if (dr["QualityCommentsBy"] != DBNull.Value)
        //                    {
        //                        W.QualityComments = Convert.ToString(dr["QualityComments"]);
        //                        W.QualityCommentsBy = new PUser() { ContactName = Convert.ToString(dr["QualityCommentsBy"]) };
        //                        W.QualityCommentsOn = Convert.ToDateTime(dr["QualityCommentsOn"]);
        //                    }
        //                    if (dr["ServiceCommentsBy"] != DBNull.Value)
        //                    {
        //                        W.ServiceComments = Convert.ToString(dr["ServiceComments"]);
        //                        W.ServiceCommentsBy = new PUser() { ContactName = Convert.ToString(dr["ServiceCommentsBy"]) };
        //                        W.ServiceCommentsOn = Convert.ToDateTime(dr["ServiceCommentsOn"]);
        //                    }
        //                    W.ICTicket = new PDMS_ICTicket();
        //                    W.ICTicket.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
        //                    W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
        //                    W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
        //                    W.ICTicket.Location = Convert.ToString(dr["Location"]);
        //                    W.ICTicket.FSRNumber = Convert.ToString(dr["FSRNumber"]);
        //                    W.ICTicket.CurrentHMRValue = dr["CurrentHMRValue"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CurrentHMRValue"]);
        //                    W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
        //                    W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
        //                    W.ICTicket.Equipment = new PDMS_EquipmentHeader();
        //                    W.ICTicket.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
        //                    W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };
        //                    W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
        //                    W.ICTicket.Equipment.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);
        //                    W.ICTicket.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
        //                    W.ICTicket.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
        //                    W.ICTicket.Equipment.EquipmentModel.Division = new PDMS_Division();
        //                    W.ICTicket.Equipment.EquipmentModel.Division.UOM = Convert.ToString(dr["UOM"]);
        //                    W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
        //                    W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);

                           


        //                    if (dr["ServiceTypeID"] != DBNull.Value)
        //                    {
        //                        W.ICTicket.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
        //                    }
        //                    if (dr["ServicePriorityID"] != DBNull.Value)
        //                    {
        //                        W.ICTicket.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
        //                    }
        //                    if (dr["MainApplicationID"] != DBNull.Value)
        //                    {
        //                        W.ICTicket.MainApplication = new PDMS_MainApplication() { MainApplicationID = Convert.ToInt32(dr["MainApplicationID"]), MainApplication = Convert.ToString(dr["MainApplication"]) };
        //                    }
        //                    W.ICTicket.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

        //                    //if (dr["ServiceStatusID"] != DBNull.Value)
        //                    //{
        //                    //    W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
        //                    //}
        //                    //W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
        //                    //W.ICTicket.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);
        //                    //W.ICTicket.RegisteredBy = new PUser();
        //                    //if (dr["RegisteredByID"] != DBNull.Value)
        //                    //    W.ICTicket.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

        //                    if (dr["TechnicianID"] != DBNull.Value)
        //                    {
        //                        W.ICTicket.Technician = new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
        //                    }
        //                    else
        //                    {
        //                        W.ICTicket.Technician = new PUser();
        //                    }
        //                    W.Status = new PDMS_ICTicketTSIRStatus() { StatusID = Convert.ToInt32(dr["StatusID"]), Status = Convert.ToString(dr["Status"]) };
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return W;
        //}

        public PDMS_TSIRAttachedFile GetICTicketTSIRAttachedFileByID(long AttachedFileID)
        {
            PDMS_TSIRAttachedFile TSIR = new PDMS_TSIRAttachedFile();
            try
            {

                DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { AttachedFileIDP };
                using (DataSet DS = provider.Select("ZDMS_GetICTicketTSIRAttachedFileByID", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {

                            TSIR.AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]);
                            TSIR.AttachedFile = (Byte[])(dr["AttachedFile"]);
                            TSIR.FileType = Convert.ToString(dr["ContentType"]);
                            TSIR.FileName = Convert.ToString(dr["FileName"]);
                            TSIR.FileSize = Convert.ToInt32(dr["FileSize"]);
                            TSIR.FSRAttachedName = new PDMS_FSRAttachedName()
                          {
                              FSRAttachedFileNameID = Convert.ToInt32(dr["TSIRAttachedFileNameID"]),
                              FSRAttachedName = Convert.ToString(dr["TSIRAttachedName"])
                          };

                        }
                    }
                }
                return TSIR;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "GetICTicketAttachedFile", ex);
                return null;
            }
        }
        public PAttachedFile GetICTicketTSIRAttachedFileForDownload(long AttachedFileID)
        {
            string endPoint = "ICTicketTsir/AttachmentsForDownload?AttachedFileID=" + AttachedFileID;
            // return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
            return JsonConvert.DeserializeObject<PAttachedFile>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));


        }
        public List<PDMS_TSIRAttachedFile> GetICTicketTSIRAttachedFileDetails(long? ICTicketID, long? TsirID, long? AttachedFileID)
        {
            List<PDMS_TSIRAttachedFile> D8 = new List<PDMS_TSIRAttachedFile>();
            try
            {
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter TsirIDP = provider.CreateParameter("TsirID", TsirID, DbType.Int64);
                DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
                DbParameter[] Params = new DbParameter[3] { ICTicketIDP, TsirIDP, AttachedFileIDP };
                using (DataSet DS = provider.Select("ZDMS_GetICTicketTSIRAttachedFileDetails", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            D8.Add(new PDMS_TSIRAttachedFile()
                            {
                                AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]),
                                FileName = Convert.ToString(dr["FileName"]),
                                FSRAttachedName = new PDMS_FSRAttachedName()
                                {
                                    FSRAttachedFileNameID = Convert.ToInt32(dr["TSIRAttachedFileNameID"]),
                                    FSRAttachedName = Convert.ToString(dr["TSIRAttachedName"])
                                }
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
        public Boolean InsertOrUpdateICTicketTSIRAttachedFileAddOrRemove(PDMS_TSIRAttachedFile AttachedFile, int UserID)
        {
            int success = 0;
            // PDMS_ServiceMaterial MM = new SMaterial().getMaterialTax(Customer, Vendor, OrderType, 1, MaterialService, 1);
            // new SCustomer().getCustomerAddress(Customer);
            DbParameter AttachedFileID = provider.CreateParameter("AttachedFileID", AttachedFile.AttachedFileID, DbType.Int64);
            DbParameter ICTicketID = provider.CreateParameter("ICTicketID", AttachedFile.ICTicket.ICTicketID, DbType.Int64);
            DbParameter TsirID = provider.CreateParameter("TsirID", AttachedFile.TSIR.TsirID, DbType.Int64);
            DbParameter FSRAttachedFileNameID = provider.CreateParameter("FSRAttachedFileNameID", AttachedFile.FSRAttachedName == null ? 0 : AttachedFile.FSRAttachedName.FSRAttachedFileNameID, DbType.Int32);
            DbParameter AttachedFileP = provider.CreateParameter("AttachedFile", AttachedFile.AttachedFile, DbType.Binary);
            DbParameter FileType = provider.CreateParameter("ContentType", AttachedFile.FileType, DbType.String);
            DbParameter FileName = provider.CreateParameter("FileName", AttachedFile.FileName, DbType.String);
            DbParameter FileSize = provider.CreateParameter("FileSize", AttachedFile.FileSize, DbType.Int32);
            DbParameter IsDeleted = provider.CreateParameter("IsDeleted", AttachedFile.IsDeleted, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[10] { AttachedFileID, ICTicketID, TsirID, FSRAttachedFileNameID, AttachedFileP, FileType, FileName, FileSize, IsDeleted, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateICTicketTSIRAttachedFileAddOrRemove", Params);
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
        public Boolean UpdateICTicketTSIRComments(long TsirID, string Remarks, int ApproveNo, long UserID)
        {
            try
            {
                DbParameter TsirIDP = provider.CreateParameter("TsirID", TsirID, DbType.Int64);
                DbParameter Remarksp = provider.CreateParameter("Remarks", Remarks, DbType.String);
                DbParameter ApproveNoP = provider.CreateParameter("ApproveNo", ApproveNo, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

                DbParameter[] Params = new DbParameter[4] { TsirIDP, Remarksp, ApproveNoP, UserIDP };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateICTicketTSIRComments", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (SqlException sqlEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public void GetTSIRStatusDDL(DropDownList ddl, int? StatusID, string Status)
        {
            ddl.DataValueField = "StatusID";
            ddl.DataTextField = "Status";
            ddl.DataSource = GetTSIRStatus(StatusID, Status);
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }

        public List<PDMS_ICTicketTSIRStatus> GetTSIRStatus(int? StatusID, string Status)
        {
            List<PDMS_ICTicketTSIRStatus> MML = new List<PDMS_ICTicketTSIRStatus>();
            try
            {
                DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
                DbParameter StatusP = provider.CreateParameter("Status", string.IsNullOrEmpty(Status) ? null : Status, DbType.String);

                DbParameter[] Params = new DbParameter[2] { StatusIDP, StatusP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketTSIRStatus", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PDMS_ICTicketTSIRStatus()
                            {
                                StatusID = Convert.ToInt32(dr["StatusID"]),
                                Status = Convert.ToString(dr["Status"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return MML;
        }
        public Boolean UpdateICTicketTSIRApproveOrReject(long TsirID, string Remarks, int StatusID, int UserID)
        {
            try
            {
                DbParameter TsirIDP = provider.CreateParameter("TsirID", TsirID, DbType.Int64);
                DbParameter Remarksp = provider.CreateParameter("Remarks", Remarks, DbType.String);
                DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

                DbParameter[] Params = new DbParameter[4] { TsirIDP, Remarksp, StatusIDP, UserIDP };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateICTicketTSIRApproveOrReject", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (SqlException sqlEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public List<PDMS_Division> GetICTicketTSIRUserDivisionList(long UserID)
        {
            List<PDMS_Division> Ws = new List<PDMS_Division>();
            PDMS_Division W = null;
            try
            {
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);


                DbParameter[] Params = new DbParameter[1] { UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketTSIRUserDivisionList", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_Division();
                            Ws.Add(W);
                            W.DivisionID = Convert.ToInt32(dr["DivisionID"]);
                            W.DivisionCode = Convert.ToString(dr["DivisionCode"]);
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
        public void InsertICTicketTSIRMailToVendor(long TsirID, string MailID, int UserID, Boolean Success)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter TsirIDP = provider.CreateParameter("TsirID", TsirID, DbType.Int64);
                DbParameter MailIDP = provider.CreateParameter("MailID", MailID, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter SuccessP = provider.CreateParameter("Success", Success, DbType.Boolean);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter[] Params = new DbParameter[4] { TsirIDP, MailIDP, UserIDP, SuccessP };

                    provider.Insert("ZDMS_InsertICTicketTSIRMailToVendor", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_ICTicketTSIR", "InsertICTicketTSIRMailToVendor", ex);

            }
        }
        public DataTable GetICTicketTSIRMailToVendor(int? DealerID,  string TsirNo, DateTime? TsirDateF, DateTime? TsirDateT , string ICTicketNumber,  String MachineSerialNumber)
        {
            DataTable dt = new DataTable();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
               
                DbParameter TsirNoP = provider.CreateParameter("TsirNo", string.IsNullOrEmpty(TsirNo) ? null : TsirNo, DbType.String);
                DbParameter TsirDateFP = provider.CreateParameter("TsirDateF", TsirDateF, DbType.DateTime);
                DbParameter TsirDateTP = provider.CreateParameter("TsirDateT", TsirDateT, DbType.DateTime);

                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
                DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", string.IsNullOrEmpty(MachineSerialNumber) ? null : MachineSerialNumber, DbType.String);

                DbParameter[] Params = new DbParameter[6] { DealerIDP, TsirNoP, TsirDateFP, TsirDateTP, ICTicketNumberP, MachineSerialNumberP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketTSIRMailToVendor", Params))
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
        public List<PDMS_ICTicketTSIRMessage> GetICTicketTSIRMessage(long? TSIRMessageID, long? TsirID, Boolean? DisplayToDealer)
        {
            List<PDMS_ICTicketTSIRMessage> TSIRs = new List<PDMS_ICTicketTSIRMessage>();
            PDMS_ICTicketTSIRMessage TSIR =null;
            try
            {
                DbParameter TsirIDP = provider.CreateParameter("TsirID", TsirID, DbType.Int64);
                DbParameter TSIRMessageIDP = provider.CreateParameter("TSIRMessageID", TSIRMessageID, DbType.Int64);
                DbParameter DisplayToDealerP = provider.CreateParameter("DisplayToDealer", DisplayToDealer, DbType.Boolean);
                DbParameter[] Params = new DbParameter[3] { TsirIDP, TSIRMessageIDP, DisplayToDealerP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketTSIRMessage", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            TSIR = new PDMS_ICTicketTSIRMessage();
                            TSIRs.Add(TSIR);
                            TSIR.TSIRMessageID = Convert.ToInt64(dr["TSIRMessageID"]);
                            TSIR.TsirID = Convert.ToInt64(dr["TsirID"]);
                            TSIR.TSIRMessage = Convert.ToString(dr["TSIRMessage"]);
                            TSIR.DisplayToDealer = Convert.ToBoolean(dr["DisplayToDealer"]);
                            TSIR.CreatedBy = new PUser() { UserID = Convert.ToInt32(dr["UserID"]), ContactName = Convert.ToString(dr["ContactName"]) };
                            TSIR.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return TSIRs;
        }
        public Boolean UpdateICTicketTSIRMessage(long TsirID, string TSIRMessage, Boolean DisplayToDealer, long UserID)
        {
            try
            {
                DbParameter TsirIDP = provider.CreateParameter("TsirID", TsirID, DbType.Int64);
                DbParameter TSIRMessageP = provider.CreateParameter("TSIRMessage", TSIRMessage, DbType.String);
                DbParameter DisplayToDealerP = provider.CreateParameter("DisplayToDealer", DisplayToDealer, DbType.Boolean);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

                DbParameter[] Params = new DbParameter[4] { TsirIDP, TSIRMessageP, DisplayToDealerP, UserIDP };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateICTicketTSIRMessage", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (SqlException sqlEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public Boolean UpdateICTicketTSIRStatus(long TsirID, int StatusID, long UserID, decimal ApproveAmount)
        {
            try
            {
                DbParameter TsirIDP = provider.CreateParameter("TsirID", TsirID, DbType.Int64);
                DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32); 
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter ApproveAmountP = provider.CreateParameter("ApproveAmount", ApproveAmount, DbType.Decimal);

                DbParameter[] Params = new DbParameter[4] { TsirIDP, StatusIDP, UserIDP, ApproveAmountP };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateICTicketTSIRStatus", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (SqlException sqlEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public PApiResult GetICTicketTSIRForApprove(int? DealerID, string CustomerCode, string TsirNo, DateTime? TsirDateF, DateTime? TsirDateT
             , string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int TsirStatusID, int? ServiceTypeID)
        {
            string endPoint = "ICTicketTsir/GetICTicketTSIRForApprove?DealerID=" + DealerID + "&CustomerCode=" + CustomerCode + "&TsirNo=" + TsirNo + 
                "&TsirDateF=" + TsirDateF + "&TsirDateT=" + TsirDateT + "&ICTicketNumber=" + ICTicketNumber + "&ICTicketDateF=" + ICTicketDateF
               + "&ICTicketDateT=" + ICTicketDateT + "&TsirStatusID=" + TsirStatusID + "&ServiceTypeID=" + ServiceTypeID;
             return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public List<PDMS_ICTicketTSIR> GetICTicketTSIRMessage(int? DealerID, string CustomerCode, string TsirNo, DateTime? TsirDateF, DateTime? TsirDateT
          , string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT,  DateTime? TsirMDateF, DateTime? TsirMDateT,   String MachineSerialNumber, int? TsirStatusID)
        {
            List<PDMS_ICTicketTSIR> Ws = new List<PDMS_ICTicketTSIR>();
            PDMS_ICTicketTSIR W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter TsirNoP = provider.CreateParameter("TsirNo", string.IsNullOrEmpty(TsirNo) ? null : TsirNo, DbType.String);
                DbParameter TsirDateFP = provider.CreateParameter("TsirDateF", TsirDateF, DbType.DateTime);
                DbParameter TsirDateTP = provider.CreateParameter("TsirDateT", TsirDateT, DbType.DateTime);

                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                  
                DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", string.IsNullOrEmpty(MachineSerialNumber) ? null : MachineSerialNumber, DbType.String);
                DbParameter TsirStatusIDP = provider.CreateParameter("TsirStatusID", TsirStatusID, DbType.Int32);


                DbParameter TsirMDateFP = provider.CreateParameter("TsirMDateF", TsirMDateF, DbType.DateTime);
                DbParameter TsirMDateTP = provider.CreateParameter("TsirMDateT", TsirMDateT, DbType.DateTime);

                DbParameter[] Params = new DbParameter[12] { DealerIDP, CustomerCodeP,TsirNoP,TsirDateFP,TsirDateTP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP,
                   TsirMDateFP, TsirMDateTP, MachineSerialNumberP,TsirStatusIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketTSIRMessageDetails", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            W = new PDMS_ICTicketTSIR();
                            Ws.Add(W);
                            W.TsirID = Convert.ToInt64(dr["TsirID"]);
                            W.TsirNumber = Convert.ToString(dr["TsirNumber"]);
                            W.TsirDate = Convert.ToDateTime(dr["TsirDate"]);

                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };

                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model()
                            {
                                Model = Convert.ToString(dr["EquipmentModel"])
                            };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.Status = new PDMS_ICTicketTSIRStatus() { Status = Convert.ToString(dr["Status"]) };
                            W.ICTicket.RestoreDate = dr["RestoreDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);

                            W.TSIRMessage = new PDMS_ICTicketTSIRMessage()
                            {
                                TSIRMessage = Convert.ToString(dr["TSIRMessage"]),
                                TSIRMessageID = Convert.ToInt64(dr["TSIRMessageID"]),
                                CreatedBy = new PUser() { ContactName = Convert.ToString(dr["MContactName"]), ContactNumber = Convert.ToString(dr["MContactNumber"]) },
                                CreatedOn = Convert.ToDateTime(dr["MCreatedOn"])
                            };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            return Ws;
        }
     }
}