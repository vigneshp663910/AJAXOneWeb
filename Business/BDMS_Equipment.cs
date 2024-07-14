using DataAccess;
using Newtonsoft.Json;
using Properties; 
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
         
        public List<PDMS_EquipmentHeader> GetEquipment(long? EquipmentHeaderID, string EquipmentSerialNo)
        {
            string endPoint = "Equipment/GetEquipment?EquipmentHeaderID=" + EquipmentHeaderID + "&EquipmentSerialNo=" + EquipmentSerialNo;
            return JsonConvert.DeserializeObject<List<PDMS_EquipmentHeader>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
             
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
        public List<PDMS_Equipment> GetEquipmentHeader(int? DealerID, string EquipmentSerialNo, string Customer, DateTime? WarrantyStart, DateTime? WarrantyEnd, int? RegionID, int? StateID, int? DivisionID, int UserID, int? PageIndex , int? PageSize , out int RowCount)
        {
            TraceLogger.Log(DateTime.Now);
            RowCount = 0;
            List<PDMS_Equipment> pDMS_Equipment = new List<PDMS_Equipment>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", string.IsNullOrEmpty(EquipmentSerialNo) ? null : EquipmentSerialNo, DbType.String);
                DbParameter CustomerP = provider.CreateParameter("Customer", string.IsNullOrEmpty(Customer) ? null : Customer, DbType.String);
                DbParameter WarrantyStartP = provider.CreateParameter("WarrantyExpiryFrom", WarrantyStart, DbType.DateTime);
                DbParameter WarrantyEndP = provider.CreateParameter("WarrantyExpiryTo", WarrantyEnd, DbType.DateTime);
                DbParameter RegionIDP = provider.CreateParameter("RegionID", RegionID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter DivisionIDP = provider.CreateParameter("DivisionID", DivisionID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);
                DbParameter[] Params = new DbParameter[11] { DealerIDP, EquipmentSerialNoP, CustomerP, WarrantyStartP, WarrantyEndP
                    , RegionIDP, StateIDP, DivisionIDP, UserIDP,PageIndexP, PageSizeP};
                using (DataSet ds = provider.Select("GetEquipmentHeader", Params))
                {
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables[0];

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PDMS_Equipment Equip = new PDMS_Equipment();
                            Equip.EquipmentHeaderID = Convert.ToInt32(dr["EquipmentHeaderID"]);
                            Equip.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                            Equip.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            Equip.EquipmentModel = new PDMS_Model()
                            {
                                Model = Convert.ToString(dr["Model"]),
                                ModelDescription = Convert.ToString(dr["ModelDescription"]),
                            };
                            Equip.Customer = new PDMS_Customer
                            {
                                CustomerCode = Convert.ToString(dr["CustomerCode"]),
                                CustomerName = Convert.ToString(dr["CustomerName"]),
                                District = (dr["District"] == null) ? null : new PDMS_District
                                {
                                    District = Convert.ToString(dr["District"])
                                },
                                State = (dr["State"] == null) ? null : new PDMS_State
                                {
                                    State = Convert.ToString(dr["State"])
                                },
                            };
                            Equip.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
                            Equip.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);
                            //Equip.EquipmentClient = new PEquipmentClient()
                            //{
                            //    Client = Convert.ToString(dr["Client"])
                            //};
                            pDMS_Equipment.Add(Equip);
                            RowCount = Convert.ToInt32(dr["RowCount"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "GetEquipmentPopulationReport", ex);
                throw ex;
            }
            return pDMS_Equipment;
        }
        public PDMS_EquipmentHeader GetEquipmentHeaderByID(Int32? EquipmentHeaderID)
        {
            TraceLogger.Log(DateTime.Now);
            PDMS_EquipmentHeader Equip = new PDMS_EquipmentHeader();
            try
            {
                DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int32);
                DbParameter[] Params = new DbParameter[1] { EquipmentHeaderIDP };
                using (DataSet ds = provider.Select("GetEquipmentHeaderByID", Params))
                {
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables[0];
                        DataRow dr = ds.Tables[0].Rows[0];
                        Equip.EquipmentHeaderID = Convert.ToInt32(dr["EquipmentHeaderID"]);
                        Equip.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                        Equip.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                        Equip.EquipmentModel = new PDMS_Model()
                        {
                            Model = Convert.ToString(dr["Model"]),
                            ModelDescription = Convert.ToString(dr["ModelDescription"]),
                        };
                        Equip.Customer = new PDMS_Customer
                        {
                            CustomerID = Convert.ToInt32(dr["CustomerID"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            District = (dr["District"] == null) ? null : new PDMS_District
                            {
                                District = Convert.ToString(dr["District"])
                            },
                            State = (dr["State"] == null) ? null : new PDMS_State
                            {
                                State = Convert.ToString(dr["State"])
                            },
                        };
                        Equip.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
                        Equip.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);
                        Equip.EngineModel = Convert.ToString(dr["EngineModel"]);
                        Equip.CurrentHMRValue = dr["CurrentHMRValue"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CurrentHMRValue"]);
                        Equip.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                        Equip.CurrentHMRDate = dr["CurrentHMRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CurrentHMRDate"]);
                        Equip.IsRefurbished = dr["IsRefurbished"] == DBNull.Value ? (Boolean?)null : Convert.ToBoolean(dr["IsRefurbished"]);
                        // Equip.RefurbishedBy = Convert.ToString(dr["RefurbishedBy"]);
                        Equip.RFWarrantyStartDate = dr["RFWarrantyStartDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RFWarrantyStartDate"]);
                        Equip.RFWarrantyExpiryDate = dr["RFWarrantyExpiryDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RFWarrantyExpiryDate"]);
                        Equip.IsAMC = dr["IsAMC"] == DBNull.Value ? (Boolean?)null : Convert.ToBoolean(dr["IsAMC"]);
                        Equip.AMCStartDate = dr["AMCStartDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["AMCStartDate"]);
                        Equip.AMCExpiryDate = dr["AMCExpiryDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["AMCExpiryDate"]);
                        Equip.TypeOfWheelAssembly = Convert.ToString(dr["RefurbishedBy"]);
                        Equip.Material = new PDMS_Material
                        {
                            MaterialCode = Convert.ToString(dr["MaterialCode"])
                        };
                        Equip.ChassisSlNo = Convert.ToString(dr["ChassisSlNo"]);
                        Equip.ESN = Convert.ToString(dr["ESN"]);
                        Equip.Plant = Convert.ToString(dr["Plant"]);
                        Equip.SpecialVariants = Convert.ToString(dr["SpecialVariants"]);
                        Equip.ProductionStatus = Convert.ToString(dr["ProductionStatus"]);
                        Equip.VariantsFittingDate = dr["VariantsFittingDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["VariantsFittingDate"]);
                        //   Equip.ManufacturingDate = Convert.ToString(dr["ManufacturingDate"]);

                        if (dr["InstalledBaseNo"] != DBNull.Value)
                        {
                            Equip.Ibase = new PDMS_EquipmentIbase();
                            Equip.Ibase.InstalledBaseNo = Convert.ToString(dr["InstalledBaseNo"]);
                            Equip.Ibase.IBaseLocation = Convert.ToString(dr["IBaseLocation"]);
                            Equip.Ibase.DeliveryDate = Convert.ToDateTime(dr["DeliveryDate"]);
                            Equip.Ibase.IBaseCreatedOn = dr["IBaseCreatedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["IBaseCreatedOn"]);
                            Equip.Ibase.WarrantyStart = dr["IbaseWarrantyStart"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["IbaseWarrantyStart"]);
                            Equip.Ibase.WarrantyEnd = dr["IbaseWarrantyEnd"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["IbaseWarrantyEnd"]);
                            Equip.Ibase.FinancialYearOfDispatch = Convert.ToInt32(dr["FinancialYearOfDispatch"]);
                                Equip.Ibase.MajorRegion = new PDMS_Region
                                {
                                    Region = Convert.ToString(dr["MajorRegion"])
                                };
                            
                        }
                                                   //    Equip.Ibase = dr["InstalledBaseNo"] == DBNull.Value ? null : new PDMS_EquipmentIbase
                        //{
                        //    InstalledBaseNo = Convert.ToString(dr["InstalledBaseNo"]),
                        //    IBaseLocation = Convert.ToString(dr["IBaseLocation"]),
                        //    DeliveryDate = Convert.ToDateTime(dr["DeliveryDate"]),
                        //    IBaseCreatedOn = dr["IBaseCreatedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["IBaseCreatedOn"]),
                        //    WarrantyStart = dr["IbaseWarrantyStart"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["IbaseWarrantyStart"]),
                        //    WarrantyEnd = dr["IbaseWarrantyEnd"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["IbaseWarrantyEnd"]),
                        //    FinancialYearOfDispatch = Convert.ToInt32(dr["FinancialYearOfDispatch"]),
                        //    MajorRegion = new PDMS_Region
                        //    {
                        //        Region = Convert.ToString(dr["MajorRegion"])
                        //    },
                        //};


                        Equip.EquipmentWarrantyType = dr["EquipmentWarrantyTypeID"] == DBNull.Value ? null : new PDMS_EquipmentWarrantyType
                        {
                            EquipmentWarrantyTypeID = Convert.ToInt32(dr["EquipmentWarrantyTypeID"]),
                            WarrantyType = Convert.ToString(dr["WarrantyType"]),
                            Description = Convert.ToString(dr["Description"])
                        };
                        Equip.WarrantyStartDate = dr["WarrantyStartDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["WarrantyStartDate"]);
                        Equip.WarrantyHMR =  Convert.ToInt32(dr["WarrantyHMR"]);
                        Equip.EquipmentClient = new PEquipmentClient()
                        {
                            Client = Convert.ToString(dr["Client"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "GetEquipmentPopulationReport", ex);
                throw ex;
            }
            return Equip;
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
        public DataSet GetEquipmentPopulationReportForAE(string EquipmentSerialNo, string Customer, DateTime? WarrantyStart, DateTime? WarrantyEnd, int? RegionID, int? StateID, int? DivisionID,Boolean? WarrantyStatusID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", string.IsNullOrEmpty(EquipmentSerialNo) ? null : EquipmentSerialNo, DbType.String);
                DbParameter CustomerP = provider.CreateParameter("Customer", string.IsNullOrEmpty(Customer) ? null : Customer, DbType.String);
                DbParameter WarrantyStartP = provider.CreateParameter("WarrantyStart", WarrantyStart, DbType.DateTime);
                DbParameter WarrantyEndP = provider.CreateParameter("WarrantyEnd", WarrantyEnd, DbType.DateTime);
                DbParameter RegionIDP = provider.CreateParameter("RegionID", RegionID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter DivisionIDP = provider.CreateParameter("DivisionID", DivisionID, DbType.Int32);
                DbParameter WarrantyStatusIDP = provider.CreateParameter("WarrantyStatusID", WarrantyStatusID, DbType.Boolean);
                DbParameter[] Params = new DbParameter[8] { EquipmentSerialNoP, CustomerP, WarrantyStartP, WarrantyEndP, RegionIDP, StateIDP, DivisionIDP, WarrantyStatusIDP };
                return provider.Select("GetEquipmentPopulationReportForAE_N", Params);
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


        //public void IntegrationEquipmentFromSAP()
        //{
        //    TraceLogger.Log(DateTime.Now);
        //    try
        //    {
        //        List<PDMS_Equipment> Equipment = new SDMS_Equipment().getEquipmentFromSAP();
        //        List<string> DeliveryNos = new List<string>();
        //        foreach (PDMS_Equipment Equ in Equipment)
        //        {
        //            DbParameter EquipmentSerialNo = provider.CreateParameter("EquipmentSerialNo", Equ.EquipmentSerialNo, DbType.String);
        //            DbParameter EngineSerialNo = provider.CreateParameter("EngineSerialNo", Equ.EngineSerialNo, DbType.String);
        //            DbParameter InstalledBaseNo = provider.CreateParameter("InstalledBaseNo", Equ.Ibase.InstalledBaseNo, DbType.String);
        //            DbParameter IBaseCreatedOn = provider.CreateParameter("IBaseCreatedOn", Equ.Ibase.IBaseCreatedOn, DbType.DateTime);
        //            DbParameter IBaseLocation = provider.CreateParameter("IBaseLocation", Equ.Ibase.IBaseLocation, DbType.String);
        //            DbParameter MajorRegion = provider.CreateParameter("MajorRegion", Equ.Ibase.MajorRegion.Region, DbType.String);
        //            DbParameter Item = provider.CreateParameter("Item", Equ.Ibase.Item, DbType.Int32);
        //            DbParameter DeliveryNo = provider.CreateParameter("DeliveryNo", Equ.Ibase.DeliveryNo, DbType.String);
        //            DbParameter DeliveryDate = provider.CreateParameter("DeliveryDate", Equ.Ibase.DeliveryDate, DbType.DateTime);
        //            DbParameter ProductCode = provider.CreateParameter("ProductCode", Equ.Ibase.ProductCode, DbType.String);
        //            DbParameter MaterialCode = provider.CreateParameter("MaterialCode", Equ.Material.MaterialCodeWithOutZero, DbType.String);

        //            long? CustomerID = null;
        //            if (!string.IsNullOrEmpty(Equ.Customer.CustomerCodeWithOutZero))
        //            {
        //                List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerByCode(null, Equ.Customer.CustomerCodeWithOutZero);
        //                if (Customer.Count == 0)
        //                {
        //                    CustomerID = new BDMS_Customer().InsertOrUpdateCustomerSap(Equ.Customer.CustomerCodeWithOutZero);
        //                }
        //                else
        //                {
        //                    CustomerID = Customer[0].CustomerID;
        //                }
        //            }

        //            long? ShipToPartyID = null;
        //            if (!string.IsNullOrEmpty(Equ.Ibase.ShipToParty.CustomerCodeWithOutZero))
        //            {
        //                List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerByCode(null, Equ.Ibase.ShipToParty.CustomerCodeWithOutZero);
        //                if (Customer.Count == 0)
        //                {
        //                    ShipToPartyID = new BDMS_Customer().InsertOrUpdateCustomerSap(Equ.Ibase.ShipToParty.CustomerCodeWithOutZero);
        //                }
        //                else
        //                {
        //                    ShipToPartyID = Customer[0].CustomerID;
        //                }
        //            }

        //            long? Buyer1stID = null;
        //            if (!string.IsNullOrEmpty(Equ.Ibase.Buyer1st.CustomerCodeWithOutZero))
        //            {
        //                List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerByCode(null, Equ.Ibase.Buyer1st.CustomerCodeWithOutZero);

        //                if (Customer.Count == 0)
        //                {
        //                    Buyer1stID = new BDMS_Customer().InsertOrUpdateCustomerSap(Equ.Ibase.Buyer1st.CustomerCodeWithOutZero);
        //                }
        //                else
        //                {
        //                    Buyer1stID = Customer[0].CustomerID;
        //                }
        //            }
        //            long? Buyer2ndID = null;
        //            if (!string.IsNullOrEmpty(Equ.Ibase.Buyer2nd.CustomerCodeWithOutZero))
        //            {
        //                List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerByCode(null, Equ.Ibase.Buyer2nd.CustomerCodeWithOutZero);
        //                if (Customer.Count == 0)
        //                {
        //                    Buyer2ndID = new BDMS_Customer().InsertOrUpdateCustomerSap(Equ.Ibase.Buyer2nd.CustomerCodeWithOutZero);
        //                }
        //                else
        //                {
        //                    Buyer2ndID = Customer[0].CustomerID;
        //                }
        //            }

        //            DbParameter CustomerCode = provider.CreateParameter("CustomerID", CustomerID, DbType.Int32);
        //            DbParameter ShipToParty = provider.CreateParameter("ShipToPartyID", ShipToPartyID, DbType.Int32);
        //            DbParameter ShipToPartyDealer = provider.CreateParameter("ShipToPartyDealer", Equ.Ibase.ShipToPartyDealer.DealerCode, DbType.String);
        //            DbParameter SoleToDealer = provider.CreateParameter("SoleToDealer", Equ.Ibase.SoleToDealer.DealerCode, DbType.String);
        //            DbParameter Buyer1st = provider.CreateParameter("Buyer1stID", Buyer1stID, DbType.Int32);
        //            DbParameter Buyer2nd = provider.CreateParameter("Buyer2ndID", Buyer2ndID, DbType.Int32);

        //            DbParameter WarrantyStart = provider.CreateParameter("WarrantyStart", Equ.Ibase.WarrantyStart, DbType.DateTime);
        //            DbParameter WarrantyEnd = provider.CreateParameter("WarrantyEnd", Equ.Ibase.WarrantyEnd, DbType.DateTime);
        //            DbParameter FinancialYearOfDispatch = provider.CreateParameter("FinancialYearOfDispatch", Equ.Ibase.FinancialYearOfDispatch, DbType.Int32);

        //            List<string> Model = new SDMS_ICTicket().getModelByProductID(Equ.EquipmentSerialNo);
        //            DbParameter ModelP = provider.CreateParameter("Model", string.IsNullOrEmpty(Model[0]) ? null : Model[0], DbType.String);
        //            DbParameter DivisionP = provider.CreateParameter("Division", Model[1], DbType.String);


        //            DbParameter UpdateOn = provider.CreateParameter("UpdateOn", Equ.Ibase.UpdateOn, DbType.DateTime);

        //            DbParameter[] Params = new DbParameter[23] { EquipmentSerialNo,EngineSerialNo,
        //                InstalledBaseNo, IBaseCreatedOn,IBaseLocation,MajorRegion,  DeliveryNo, Item, DeliveryDate, ProductCode,  MaterialCode,
        //            CustomerCode,ShipToParty,ShipToPartyDealer,SoleToDealer,Buyer1st,Buyer2nd,
        //            WarrantyStart,WarrantyEnd,FinancialYearOfDispatch,UpdateOn ,ModelP,DivisionP};
        //            try
        //            {
        //                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //                {
        //                    provider.Insert("ZDMS_InsertOrUpdateEquipmentFromSAP", Params);
        //                    scope.Complete();
        //                }
        //                DeliveryNos.Add(Equ.Ibase.DeliveryNo);
        //            }
        //            catch (SqlException sqlEx)
        //            {
        //                new FileLogger().LogMessageService("BDMS_Equipment", "IntegrationEquipmentFromSAP", sqlEx);
        //            }
        //            catch (Exception ex)
        //            {
        //                new FileLogger().LogMessageService("BDMS_Equipment", " IntegrationEquipmentFromSAP", ex);
        //            }
        //        }
        //        new SDMS_Equipment().UpdateICTicketRequestedDateToSAP(DeliveryNos);
        //    }
        //    catch (Exception e11)
        //    {
        //        new FileLogger().LogMessageService("BDMS_Material", "IntegrationEquipmentFromSAP", e11);
        //        throw e11;
        //    }

        //    TraceLogger.Log(DateTime.Now);
        //}
        public List<PDMS_EquipmentHeader> GetEquipmentForCreateICTicket(long? CustomerID, string EquipmentSerialNo, string Customer)
        {
            string endPoint = "Equipment/EquipmentForCreateICTicket?CustomerID=" + CustomerID + "&EquipmentSerialNo=" + EquipmentSerialNo + "&Customer=" + Customer;
            return JsonConvert.DeserializeObject<List<PDMS_EquipmentHeader>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PDMS_EquipmentWarrantyType> GetEquipmentWarrantyType(int? EquipmentWarrantyTypeID, string WarrantyType)
        {
            List<PDMS_EquipmentWarrantyType> MML = new List<PDMS_EquipmentWarrantyType>();
            try
            {
                DbParameter EquipmentWarrantyTypeIDP = provider.CreateParameter("EquipmentWarrantyTypeID", EquipmentWarrantyTypeID, DbType.Int32);
                DbParameter WarrantyTypeP = provider.CreateParameter("WarrantyType", string.IsNullOrEmpty(WarrantyType) ? null : WarrantyType, DbType.String);
                DbParameter[] Params = new DbParameter[2] { EquipmentWarrantyTypeIDP, WarrantyTypeP };
                using (DataSet DataSet = provider.Select("ZDMS_GetEquipmentWarrantyType", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PDMS_EquipmentWarrantyType()
                            {
                                EquipmentWarrantyTypeID = Convert.ToInt32(dr["EquipmentWarrantyTypeID"]),
                                WarrantyType = Convert.ToString(dr["WarrantyType"]),
                                Description = Convert.ToString(dr["Description"]),
                                HMR = Convert.ToInt32(dr["HMR"]),
                                Period = Convert.ToDecimal(dr["Period"]),
                                TimeUnit = Convert.ToString(dr["TimeUnit"]),
                                BaseCategory = Convert.ToString(dr["BaseCategory"])
                            }); ;
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
        public Boolean UpdateEquipmentWarrantyType(long EquipmentHeaderID, int EquipmentWarrantyTypeID)
        {
            DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
            DbParameter EquipmentWarrantyTypeIDP = provider.CreateParameter("EquipmentWarrantyTypeID", EquipmentWarrantyTypeID, DbType.Int32);
            DbParameter[] Params = new DbParameter[2] { EquipmentHeaderIDP, EquipmentWarrantyTypeIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateEquipmentWarrantyType", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "ZDMS_UpdateEquipmentWarrantyType", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "ZDMS_UpdateEquipmentWarrantyType", ex);
                return false;
            }
            return true;
        }
      
        //public List<PDMS_Equipment> GetEquipmentWarrantTypeChangeForApproval(DateTime? WarrantyChangeRequestedFrom, DateTime? WarrantyChangeRequestedTo, string EquipmentSerialNo)
        //{
        //    List<PDMS_Equipment> Equips = new List<PDMS_Equipment>();
        //    PDMS_Equipment Equip = null;
        //    try
        //    {
        //        DbParameter WarrantyChangeRequestedFromP = provider.CreateParameter("WarrantyChangeRequestedFrom", WarrantyChangeRequestedFrom, DbType.DateTime);
        //        DbParameter WarrantyChangeRequestedToP = provider.CreateParameter("WarrantyChangeRequestedTo", WarrantyChangeRequestedTo, DbType.DateTime);

        //        DbParameter EquipmentSerialNoP;
        //        if (!string.IsNullOrEmpty(EquipmentSerialNo))
        //            EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", EquipmentSerialNo, DbType.String);
        //        else
        //            EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", null, DbType.String);

        //        DbParameter[] Params = new DbParameter[3] { WarrantyChangeRequestedFromP, WarrantyChangeRequestedToP, EquipmentSerialNoP };
        //        using (DataSet DataSet = provider.Select("GetEquipmentWarrantTypeChangeForApproval", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    Equip = new PDMS_Equipment();
        //                    Equips.Add(Equip);

        //                    Equip.EquipmentHeaderID = Convert.ToInt32(dr["EquipmentHeaderID"]);
        //                    Equip.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
        //                    Equip.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
        //                    Equip.CommissioningOn = Convert.ToDateTime(dr["CommissioningOn"]);
        //                    Equip.EquipmentModel = new PDMS_Model()
        //                    {
        //                        Model = Convert.ToString(dr["Model"]),
        //                        ModelDescription = Convert.ToString(dr["ModelDescription"]),
        //                    };
        //                    Equip.Customer = new PDMS_Customer
        //                    {
        //                        CustomerID = Convert.ToInt32(dr["CustomerID"]),
        //                        CustomerCode = Convert.ToString(dr["CustomerCode"]),
        //                        CustomerName = Convert.ToString(dr["CustomerName"]),
        //                    };
        //                    Equip.EquipmentWarrantyType = dr["EquipmentWarrantyTypeID"] == DBNull.Value ? null : new PDMS_EquipmentWarrantyType
        //                    {
        //                        EquipmentWarrantyTypeID = Convert.ToInt32(dr["EquipmentWarrantyTypeID"]),
        //                        WarrantyType = Convert.ToString(dr["WarrantyType"]),
        //                        Description = Convert.ToString(dr["Description"])
        //                    };
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return Equips;
        //}
        public List<PEquipmentWarrantyTypeApproval> GetEquipmentWarrantTypeChangeForApproval(DateTime? WarrantyChangeRequestedFrom, DateTime? WarrantyChangeRequestedTo, string EquipmentSerialNo)
        {
            List<PEquipmentWarrantyTypeApproval> Equips = new List<PEquipmentWarrantyTypeApproval>();
            PEquipmentWarrantyTypeApproval Equip = null;
            try
            {
                DbParameter WarrantyChangeRequestedFromP = provider.CreateParameter("WarrantyChangeRequestedFrom", WarrantyChangeRequestedFrom, DbType.DateTime);
                DbParameter WarrantyChangeRequestedToP = provider.CreateParameter("WarrantyChangeRequestedTo", WarrantyChangeRequestedTo, DbType.DateTime);

                DbParameter EquipmentSerialNoP;
                if (!string.IsNullOrEmpty(EquipmentSerialNo))
                    EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", EquipmentSerialNo, DbType.String);
                else
                    EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", null, DbType.String);

                DbParameter[] Params = new DbParameter[3] { WarrantyChangeRequestedFromP, WarrantyChangeRequestedToP, EquipmentSerialNoP };
                using (DataSet DataSet = provider.Select("GetEquipmentWarrantTypeChangeForApproval", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Equip = new PEquipmentWarrantyTypeApproval();
                            Equips.Add(Equip);

                            Equip.WarrantyTypeChangeID = Convert.ToInt32(dr["WarrantyTypeChangeID"]);

                            Equip.Equipment = new PDMS_EquipmentHeader()
                            {
                                EquipmentHeaderID = Convert.ToInt32(dr["EquipmentHeaderID"]),
                                EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]),
                                EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]),
                                CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]),
                                EquipmentModel = new PDMS_Model()
                                {
                                    Model = Convert.ToString(dr["Model"]),
                                    ModelDescription = Convert.ToString(dr["ModelDescription"]),
                                },
                                Customer = new PDMS_Customer
                                {
                                    CustomerID = Convert.ToInt32(dr["CustomerID"]),
                                    CustomerCode = Convert.ToString(dr["CustomerCode"]),
                                    CustomerName = Convert.ToString(dr["CustomerName"]),
                                },
                                EquipmentWarrantyType = new PDMS_EquipmentWarrantyType()
                                {
                                    EquipmentWarrantyTypeID = Convert.ToInt32(dr["EquipmentWarrantyTypeID"]),
                                    WarrantyType = Convert.ToString(dr["WarrantyType"]),
                                    Description = Convert.ToString(dr["Description"])
                                }
                            };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Equips;
        }
        public Boolean ApproveOrRejectEquipmentWarrrantyTypeChange(long WarrantyTypeChangeID, long EquipmentHeaderID, int ApprovedBy, Boolean IsApproved)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter WarrantyTypeChangeIDP = provider.CreateParameter("WarrantyTypeChangeID", WarrantyTypeChangeID, DbType.Int64);
                    DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
                    DbParameter IsApprovedP = provider.CreateParameter("IsApproved", IsApproved, DbType.Boolean);
                    DbParameter ApprovedByP = provider.CreateParameter("ApprovedBy", ApprovedBy, DbType.Int32);
                    DbParameter[] Paramss = new DbParameter[4] { WarrantyTypeChangeIDP, EquipmentHeaderIDP, IsApprovedP, ApprovedByP };
                    provider.Insert("ApproveOrRejectEquipmentWarrrantyTypeChange", Paramss);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "ApproveOrRejectEquipmentWarrrantyTypeChange", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", " ApproveOrRejectEquipmentWarrrantyTypeChange", ex);
                return false;
            }
            return true;
        }
       
        public List<PEquipmentAttachedFile> GetEquipmentWarrantyTypeAttachedFileDetails(long EquipmentHeaderID, long? AttachedFileID)
        {
            List<PEquipmentAttachedFile> AF = new List<PEquipmentAttachedFile>();
            try
            {
                DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
                DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
                DbParameter[] Params = new DbParameter[2] { EquipmentHeaderIDP, AttachedFileIDP };
                using (DataSet DS = provider.Select("GetEquipmentWarrantyTypeAttachedFileDetails", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            AF.Add(new PEquipmentAttachedFile()
                            {
                                AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]),
                                FileName = Convert.ToString(dr["FileName"]),
                                CreatedDate = Convert.ToDateTime(dr["CreatedOn"]),
                                CreatedBy = new PUser() { ContactName = Convert.ToString(dr["CreatedByName"]) }
                            });
                        }
                    }
                }
                return AF;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "GetEquipmentWarrantyTypeAttachedFileDetails", ex);
                return null;
            }
        }
        public List<PEquipmentAttachedFile> GetEquipmentOwnershipChangeAttachedFileDetails(long EquipmentHeaderID, long? AttachedFileID)
        {
            List<PEquipmentAttachedFile> AF = new List<PEquipmentAttachedFile>();
            try
            {
                DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
                DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
                DbParameter[] Params = new DbParameter[2] { EquipmentHeaderIDP, AttachedFileIDP };
                using (DataSet DS = provider.Select("GetEquipmentOwnershipChangeAttachedFileDetails", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            AF.Add(new PEquipmentAttachedFile()
                            {
                                AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]),
                                FileName = Convert.ToString(dr["FileName"]),
                                CreatedDate = Convert.ToDateTime(dr["CreatedOn"]),
                                CreatedBy = new PUser() { ContactName = Convert.ToString(dr["CreatedByName"]) }
                            });
                        }
                    }
                }
                return AF;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "GetEquipmentOwnershipChangeAttachedFileDetails", ex);
                return null;
            }
        }
        public List<PEquipmentAttachedFile> GetEquipmentWarrantyExpiryDateChangeAttachedFileDetails(long EquipmentHeaderID, long? AttachedFileID)
        {
            List<PEquipmentAttachedFile> AF = new List<PEquipmentAttachedFile>();
            try
            {
                DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
                DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
                DbParameter[] Params = new DbParameter[2] { EquipmentHeaderIDP, AttachedFileIDP };
                using (DataSet DS = provider.Select("GetEquipmentWarrantyExpiryDateChangeAttachedFileDetails", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            AF.Add(new PEquipmentAttachedFile()
                            {
                                AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]),
                                FileName = Convert.ToString(dr["FileName"]),
                                CreatedDate = Convert.ToDateTime(dr["CreatedOn"]),
                                CreatedBy = new PUser() { ContactName = Convert.ToString(dr["CreatedByName"]) }
                            });
                        }
                    }
                }
                return AF;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "GetEquipmentWarrantyExpiryDateChangeAttachedFileDetails", ex);
                return null;
            }
        }
        public List<PEquipmentOwnershipChangeApproval> GetEquipmentOwnershipChangeForApproval(DateTime? OwnershipChangeRequestedFrom, DateTime? OwnershipChangeRequestedTo, string EquipmentSerialNo)
        {
            List<PEquipmentOwnershipChangeApproval> Equips = new List<PEquipmentOwnershipChangeApproval>();
            PEquipmentOwnershipChangeApproval Equip = null;
            try
            {
                DbParameter OwnershipChangeRequestedFromP = provider.CreateParameter("OwnershipChangeRequestedFrom", OwnershipChangeRequestedFrom, DbType.DateTime);
                DbParameter OwnershipChangeRequestedToP = provider.CreateParameter("OwnershipChangeRequestedTo", OwnershipChangeRequestedTo, DbType.DateTime);

                DbParameter EquipmentSerialNoP;
                if (!string.IsNullOrEmpty(EquipmentSerialNo))
                    EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", EquipmentSerialNo, DbType.String);
                else
                    EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", null, DbType.String);

                DbParameter[] Params = new DbParameter[3] { OwnershipChangeRequestedFromP, OwnershipChangeRequestedToP, EquipmentSerialNoP };
                using (DataSet DataSet = provider.Select("GetEquipmentOwnershipChangeForApproval", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Equip = new PEquipmentOwnershipChangeApproval();
                            Equips.Add(Equip);

                            Equip.OwnershipChangeID = Convert.ToInt32(dr["OwnershipChangeID"]);

                            Equip.Equipment = new PDMS_EquipmentHeader()
                            {
                                EquipmentHeaderID = Convert.ToInt32(dr["EquipmentHeaderID"]),
                                EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]),
                                EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]),
                                CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]),
                                EquipmentModel = new PDMS_Model()
                                {
                                    Model = Convert.ToString(dr["Model"]),
                                    ModelDescription = Convert.ToString(dr["ModelDescription"]),
                                },
                                Customer = new PDMS_Customer
                                {
                                    CustomerID = Convert.ToInt32(dr["OwnershipChgReqCustomerID"]),
                                    CustomerCode = Convert.ToString(dr["OwnershipChgReqCustomerCode"]),
                                    CustomerName = Convert.ToString(dr["OwnershipChgReqCustomerName"]),
                                },
                                EquipmentWarrantyType = new PDMS_EquipmentWarrantyType()
                                {
                                    EquipmentWarrantyTypeID = Convert.ToInt32(dr["EquipmentWarrantyTypeID"]),
                                    WarrantyType = Convert.ToString(dr["WarrantyType"]),
                                    Description = Convert.ToString(dr["Description"])
                                }
                            };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Equips;
        }
        public Boolean ApproveOrRejectEquipmentOwnershipChange(long OwnershipChangeID, long EquipmentHeaderID, int ApprovedBy, Boolean IsApproved)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter OwnershipChangeIDP = provider.CreateParameter("OwnershipChangeID", OwnershipChangeID, DbType.Int64);
                    DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
                    DbParameter IsApprovedP = provider.CreateParameter("IsApproved", IsApproved, DbType.Boolean);
                    DbParameter ApprovedByP = provider.CreateParameter("ApprovedBy", ApprovedBy, DbType.Int32);
                    DbParameter[] Paramss = new DbParameter[4] { OwnershipChangeIDP, EquipmentHeaderIDP, IsApprovedP, ApprovedByP };
                    provider.Insert("ApproveOrRejectEquipmentOwnershipChange", Paramss);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "ApproveOrRejectEquipmentOwnershipChange", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", " ApproveOrRejectEquipmentOwnershipChange", ex);
                return false;
            }
            return true;
        }
        public List<PWarrantyExpiryDateChangeApproval> GetEquipmentWarrantyExpiryDateChangeForApproval(DateTime? WarrantyExpiryDateChangeRequestedFrom, DateTime? WarrantyExpiryDateChangeRequestedTo, string EquipmentSerialNo)
        {
            List<PWarrantyExpiryDateChangeApproval> Equips = new List<PWarrantyExpiryDateChangeApproval>();
            PWarrantyExpiryDateChangeApproval Equip = null;
            try
            {
                DbParameter WarrantyExpiryDateChangeRequestedFromP = provider.CreateParameter("WarrantyExpiryDateChangeRequestedFrom", WarrantyExpiryDateChangeRequestedFrom, DbType.DateTime);
                DbParameter WarrantyExpiryDateChangeRequestedToP = provider.CreateParameter("WarrantyExpiryDateChangeRequestedTo", WarrantyExpiryDateChangeRequestedTo, DbType.DateTime);

                DbParameter EquipmentSerialNoP;
                if (!string.IsNullOrEmpty(EquipmentSerialNo))
                    EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", EquipmentSerialNo, DbType.String);
                else
                    EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", null, DbType.String);

                DbParameter[] Params = new DbParameter[3] { WarrantyExpiryDateChangeRequestedFromP, WarrantyExpiryDateChangeRequestedToP, EquipmentSerialNoP };
                using (DataSet DataSet = provider.Select("GetEquipmentWarrantyExpiryDateChangeForApproval", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Equip = new PWarrantyExpiryDateChangeApproval();
                            Equips.Add(Equip);

                            Equip.WarrantyExpiryDateChangeID = Convert.ToInt32(dr["WarrantyExpiryDateChangeID"]);
                            Equip.NewWarrantyExpiryDate = Convert.ToDateTime(dr["NewWarrantyExpiryDate"]);

                            Equip.Equipment = new PDMS_EquipmentHeader()
                            {
                                EquipmentHeaderID = Convert.ToInt32(dr["EquipmentHeaderID"]),
                                EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]),
                                EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]),
                                CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]),
                                EquipmentModel = new PDMS_Model()
                                {
                                    Model = Convert.ToString(dr["Model"]),
                                    ModelDescription = Convert.ToString(dr["ModelDescription"]),
                                },
                                Customer = new PDMS_Customer
                                {
                                    CustomerID = Convert.ToInt32(dr["OwnershipChgReqCustomerID"]),
                                    CustomerCode = Convert.ToString(dr["OwnershipChgReqCustomerCode"]),
                                    CustomerName = Convert.ToString(dr["OwnershipChgReqCustomerName"]),
                                },
                                EquipmentWarrantyType = new PDMS_EquipmentWarrantyType()
                                {
                                    EquipmentWarrantyTypeID = Convert.ToInt32(dr["EquipmentWarrantyTypeID"]),
                                    WarrantyType = Convert.ToString(dr["WarrantyType"]),
                                    Description = Convert.ToString(dr["Description"])
                                }
                            };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Equips;
        }
        public Boolean ApproveOrRejectEquipmentWarrantyExpiryDateChange(long WarrantyExpiryDateChangeID, long EquipmentHeaderID, int ApprovedBy, Boolean IsApproved)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter WarrantyExpiryDateChangeIDP = provider.CreateParameter("WarrantyExpiryDateChangeID", WarrantyExpiryDateChangeID, DbType.Int64);
                    DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
                    DbParameter IsApprovedP = provider.CreateParameter("IsApproved", IsApproved, DbType.Boolean);
                    DbParameter ApprovedByP = provider.CreateParameter("ApprovedBy", ApprovedBy, DbType.Int32);
                    DbParameter[] Paramss = new DbParameter[4] { WarrantyExpiryDateChangeIDP, EquipmentHeaderIDP, IsApprovedP, ApprovedByP };
                    provider.Insert("ApproveOrRejectEquipmentWarrantyExpiryDateChange", Paramss);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "ApproveOrRejectEquipmentWarrantyExpiryDateChange", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", " ApproveOrRejectEquipmentWarrantyExpiryDateChange", ex);
                return false;
            }
            return true;
        }
        public List<PEquipmentAttachedFile> GetEquipmentAttachedFileDetails(long EquipmentHeaderID, long? AttachedFileID, long? ChangeID)
        {
            List<PEquipmentAttachedFile> AF = new List<PEquipmentAttachedFile>();
            try
            {
                DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
                DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
                DbParameter[] Params = new DbParameter[2] { EquipmentHeaderIDP, AttachedFileIDP };
                using (DataSet DS = provider.Select("GetEquipmentAttachedFileDetails", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            AF.Add(new PEquipmentAttachedFile()
                            {
                                AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]),
                                FileName = Convert.ToString(dr["FileName"]),
                                ReferenceName = Convert.ToString(dr["FileType"]),
                                CreatedDate = Convert.ToDateTime(dr["CreatedOn"]),
                                CreatedBy = new PUser() { ContactName = Convert.ToString(dr["CreatedByName"]) }
                            });
                        }
                    }
                }
                return AF;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "GetEquipmentAttachedFileDetails", ex);
                return null;
            }
        }
        public DataTable GetEquipmentChangeForApproval(DateTime? RequestedFrom, DateTime? RequestedTo, string EquipmentSerialNo)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter RequestedFromP = provider.CreateParameter("RequestedFrom", RequestedFrom, DbType.DateTime);
                DbParameter RequestedToP = provider.CreateParameter("RequestedTo", RequestedTo, DbType.DateTime);

                DbParameter EquipmentSerialNoP;
                if (!string.IsNullOrEmpty(EquipmentSerialNo))
                    EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", EquipmentSerialNo, DbType.String);
                else
                    EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", null, DbType.String);

                DbParameter[] Params = new DbParameter[3] { RequestedFromP, RequestedToP, EquipmentSerialNoP };

                
                using (DataSet DataSet = provider.Select("GetEquipmentChangeForApproval", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
                return null;
                TraceLogger.Log(DateTime.Now);
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public PEquipmentAttachedFile GetEquipmentAttachedFileByID(string DocumentName)
        {
            string endPoint = "Equipment/GetEquipmentAttachedFileByID?DocumentName=" + DocumentName;
            return JsonConvert.DeserializeObject<PEquipmentAttachedFile>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataTable GetEquipmentChangeRequestHistory(long EquipmentHeaderID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { EquipmentHeaderIDP };
                
                using (DataSet DataSet = provider.Select("GetEquipmentChangeRequestHistory", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
                return null;
                TraceLogger.Log(DateTime.Now);
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }

        public PApiResult GetNepiDueReport(int? DealerID, string EquipmentSerialNo, string Customer, int? RegionID, int? StateID,int OverDueID, int? PageIndex = null, int? PageSize = null)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Equipment/GetNepiDueReport?DealerID=" + DealerID + "&EquipmentSerialNo=" + EquipmentSerialNo + "&Customer=" + Customer
                + "&RegionID=" + RegionID + "&StateID=" + StateID + "&OverDueID=" + OverDueID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetEquipmentClient(int? EquipmentClientID, string Client, bool IsActive, int? PageIndex, int? PageSize)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Equipment/GetEquipmentClient?EquipmentClientID=" + EquipmentClientID + "&Client=" + Client + "&IsActive=" + IsActive
                + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
}

