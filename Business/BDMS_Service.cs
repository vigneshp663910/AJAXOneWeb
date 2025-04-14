using DataAccess;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties;
using QRCoder; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI.WebControls;

namespace Business
{
    public class BDMS_Service
    {
        private IDataAccess provider;
        public BDMS_Service()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PDMS_ServiceStatus> GetServiceStatus(int? ServiceStatusID, string ServiceStatus)
        {
            List<PDMS_ServiceStatus> Status = new List<PDMS_ServiceStatus>();
            try
            {
                DbParameter ServiceStatusP;
                DbParameter ServiceStatusIDP = provider.CreateParameter("ServiceStatusID", ServiceStatusID, DbType.Int32);

                if (!string.IsNullOrEmpty(ServiceStatus))
                    ServiceStatusP = provider.CreateParameter("ServiceStatus", ServiceStatus, DbType.String);
                else
                    ServiceStatusP = provider.CreateParameter("ServiceStatus", null, DbType.String);

                DbParameter[] Params = new DbParameter[2] { ServiceStatusIDP, ServiceStatusP };
                using (DataSet DataSet = provider.Select("ZDMS_GetServiceStatus", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Status.Add(new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Status;
        }
        public List<PDMS_ServiceType> GetServiceType(int? ServiceTypeID, string ServiceType, int? IsFree)
        {
            List<PDMS_ServiceType> ServiceTypes = new List<PDMS_ServiceType>();
            try
            {
                DbParameter ServiceTypeP;
                DbParameter ServiceTypeIDP = provider.CreateParameter("ServiceTypeID", ServiceTypeID, DbType.Int32);
                if (!string.IsNullOrEmpty(ServiceType))
                    ServiceTypeP = provider.CreateParameter("ServiceType", ServiceType, DbType.String);
                else
                    ServiceTypeP = provider.CreateParameter("ServiceType", null, DbType.String);
                DbParameter IsFreeP = provider.CreateParameter("IsFree", IsFree, DbType.Int32);
                DbParameter[] Params = new DbParameter[3] { ServiceTypeP, ServiceTypeIDP, IsFreeP };
                using (DataSet DataSet = provider.Select("ZDMS_GetServiceType", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServiceTypes.Add(new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return ServiceTypes;
        }
        public List<PDMS_ServiceSubType> GetServiceSubType(int? ServiceSubTypeID,int? ServiceTypeID)
        {
            List<PDMS_ServiceSubType> ServiceTypes = new List<PDMS_ServiceSubType>();
            try
            {
                DbParameter ServiceSubTypeIDP = provider.CreateParameter("ServiceSubTypeID", ServiceSubTypeID, DbType.Int32);
                DbParameter ServiceTypeIDP = provider.CreateParameter("ServiceTypeID", ServiceTypeID, DbType.Int32);
                DbParameter[] Params = new DbParameter[2] { ServiceSubTypeIDP, ServiceTypeIDP  };
                using (DataSet DataSet = provider.Select("ZDMS_GetServiceSubType", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServiceTypes.Add(new PDMS_ServiceSubType() { ServiceSubTypeID = Convert.ToInt32(dr["ServiceSubTypeID"]), ServiceSubType = Convert.ToString(dr["ServiceSubType"]) });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return ServiceTypes;
        }
        public List<PDMS_ServiceTypeOverhaul> GetServiceTypeddlServiceTypeOverhaul(int? ServiceTypeOverhaulID, string ServiceTypeOverhaul)
        {
            List<PDMS_ServiceTypeOverhaul> ServiceTypes = new List<PDMS_ServiceTypeOverhaul>();
            try
            {
               
                DbParameter ServiceTypeOverhaulIDP = provider.CreateParameter("ServiceTypeOverhaulID", ServiceTypeOverhaulID, DbType.Int32);
                DbParameter ServiceTypeOverhaulP = provider.CreateParameter("ServiceTypeOverhaul", string.IsNullOrEmpty(ServiceTypeOverhaul) ? null : ServiceTypeOverhaul, DbType.String);
              
                DbParameter[] Params = new DbParameter[2] { ServiceTypeOverhaulIDP, ServiceTypeOverhaulP };
                using (DataSet DataSet = provider.Select("ZDMS_GetServiceTypeOverhaul", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServiceTypes.Add(new PDMS_ServiceTypeOverhaul() { ServiceTypeOverhaulID = Convert.ToInt32(dr["ServiceTypeOverhaulID"]), ServiceTypeOverhaul = Convert.ToString(dr["ServiceTypeOverhaul"]) });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return ServiceTypes;
        }
        public List<PDMS_ServicePriority> GetServicePriority(int? ServicePriorityID, string ServicePriority)
        {
            List<PDMS_ServicePriority> ServicePrioritys = new List<PDMS_ServicePriority>();
            try
            {
                DbParameter ServicePriorityP;
                DbParameter ServicePriorityIDP = provider.CreateParameter("ServicePriorityID", ServicePriorityID, DbType.Int32);
                if (!string.IsNullOrEmpty(ServicePriority))
                    ServicePriorityP = provider.CreateParameter("ServicePriority", ServicePriority, DbType.String);
                else
                    ServicePriorityP = provider.CreateParameter("ServicePriority", null, DbType.String);

                DbParameter[] Params = new DbParameter[2] { ServicePriorityIDP, ServicePriorityP };

                using (DataSet DataSet = provider.Select("ZDMS_GetServicePriority", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServicePrioritys.Add(new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return ServicePrioritys;
        }
         
        public List<PDMS_ServiceTechnician> GetTechniciansByDealerID(int DealerID)
        {
            List<PDMS_ServiceTechnician> Technicians = new List<PDMS_ServiceTechnician>();
            try
            {

                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

                DbParameter[] Params = new DbParameter[1] { DealerIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetTechniciansByDealerID", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Technicians.Add(new PDMS_ServiceTechnician()
                            {
                                UserID = Convert.ToInt32(dr["UserID"]),
                                ContactName = Convert.ToString(dr["ContactName"]),
                                UserName = Convert.ToString(dr["UserName"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Technicians;
        }
        public List<PDMS_ServiceTechnician> GetTechniciansByTicketID(long ICTicketID)
        {
            List<PDMS_ServiceTechnician> Technicians = new List<PDMS_ServiceTechnician>();
            PDMS_ServiceTechnician Technician = null;
            long UserID = 0;

            try
            {

                DbParameter TicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);

                DbParameter[] Params = new DbParameter[1] { TicketIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetTechniciansByTicketID", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            if (UserID != Convert.ToInt64(dr["UserID"]))
                            {
                                Technician = new PDMS_ServiceTechnician();
                                Technicians.Add(Technician);
                                Technician.UserID = Convert.ToInt32(dr["UserID"]);
                                Technician.ContactName = Convert.ToString(dr["ContactName"]);
                                Technician.UserName = Convert.ToString(dr["UserName"]);
                                Technician.ServiceTechnicianWorkedDate = new List<PDMS_ServiceTechnicianWorkedDate>();
                                UserID = Technician.UserID;
                                Technician.AssignedOn = Convert.ToDateTime(dr["AssignedOn"]);
                                Technician.AssignedBy = new PUser() { ContactName = Convert.ToString(dr["AssignedBy"]) };
                            }
                            if (DBNull.Value != dr["ServiceTechnicianWorkDateID"])
                            {
                                Technician.ServiceTechnicianWorkedDate.Add(new PDMS_ServiceTechnicianWorkedDate()
                                {
                                    ServiceTechnicianWorkDateID = Convert.ToInt64(dr["ServiceTechnicianWorkDateID"]),
                                    UserName_ContactName = Technician.UserName + "-" + Technician.ContactName,
                                    WorkedDate = Convert.ToDateTime(dr["WorkedDate"]),
                                    WorkedHours = Convert.ToDecimal(dr["WorkedHours"]),
                                    UserID = Convert.ToInt32(dr["UserID"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Technicians;
        }
        public List<PDMS_ServiceTechnician> GetTechniciansForMTTR(int? DealerID, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? ServiceStatusID, int UserID)
        {
            int UserIDTemp = 0;
            List<PDMS_ServiceTechnician> Technicians = new List<PDMS_ServiceTechnician>();
            PDMS_ServiceTechnician Technician = null; 
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter ServiceStatusIDP = provider.CreateParameter("ServiceStatusID", ServiceStatusID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, ServiceStatusIDP, UserIDP };

                using (DataSet DataSet = provider.Select("ZDMS_GetTechniciansForMTTR", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            if (UserIDTemp != Convert.ToInt64(dr["UserID"]))
                            {
                                Technician = new PDMS_ServiceTechnician();
                                Technicians.Add(Technician);
                                Technician.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            //    Technician.UserID = Convert.ToInt32(dr["UserID"]);
                                Technician.ContactName = Convert.ToString(dr["ContactName"]);
                             //   Technician.UserName = Convert.ToString(dr["UserName"]);
                                Technician.ServiceTechnicianWorkedDate = new List<PDMS_ServiceTechnicianWorkedDate>();
                               // UserIDTemp = Technician.UserID;
                               // Technician.AssignedOn = Convert.ToDateTime(dr["AssignedOn"]);
                               // Technician.AssignedBy = new PUser() { ContactName = Convert.ToString(dr["AssignedBy"]) };
                            }
                            if (DBNull.Value != dr["ServiceTechnicianWorkDateID"])
                            {
                                Technician.ServiceTechnicianWorkedDate.Add(new PDMS_ServiceTechnicianWorkedDate()
                                {
                                    // ServiceTechnicianWorkDateID = Convert.ToInt64(dr["ServiceTechnicianWorkDateID"]),
                                    // UserName_ContactName = Technician.UserName + "-" + Technician.ContactName,
                                    WorkedDate = Convert.ToDateTime(dr["WorkedDate"]),
                                    WorkedHours = Convert.ToDecimal(dr["WorkedHours"]),
                                    ICTicketID = Convert.ToInt64(dr["ICTicketID"])
                                    //UserID = Convert.ToInt32(dr["UserID"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Technicians;
        }
         

        public List<PDMS_MainApplication> GetMainApplication(int? MainApplicationID, string MainApplication)
        {
            List<PDMS_MainApplication> Main = new List<PDMS_MainApplication>();
            try
            {
                DbParameter MainApplicationIDP = provider.CreateParameter("MainApplicationID", MainApplicationID, DbType.Int32);

                DbParameter MainApplicationP;
                if (!string.IsNullOrEmpty(MainApplication))
                    MainApplicationP = provider.CreateParameter("MainApplication", MainApplication, DbType.String);
                else
                    MainApplicationP = provider.CreateParameter("MainApplication", null, DbType.String);

                DbParameter[] Params = new DbParameter[2] { MainApplicationIDP, MainApplicationP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMainApplication", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Main.Add(new PDMS_MainApplication()
                            {
                                MainApplicationID = Convert.ToInt32(dr["MainApplicationID"]),
                                MainApplication = Convert.ToString(dr["MainApplication"]),
                                IsActive=Convert.ToBoolean(dr["IsActive"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Service", "ZDMS_GetMainApplication", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", "ZDMS_GetMainApplication", ex);
                throw ex;
            }
            return Main;
        }
        public List<PDMS_SubApplication> GetSubApplication(int? MainApplicationID, int? SubApplicationID, string SubApplication)
        {
            List<PDMS_SubApplication> Sub = new List<PDMS_SubApplication>();
            try
            {


                DbParameter MainApplicationIDP = provider.CreateParameter("MainApplicationID", MainApplicationID, DbType.Int32);
                DbParameter SubApplicationIDP = provider.CreateParameter("SubApplicationID", SubApplicationID, DbType.Int32);

                DbParameter SubApplicationP;
                if (!string.IsNullOrEmpty(SubApplication))
                    SubApplicationP = provider.CreateParameter("SubApplication", SubApplication, DbType.String);
                else
                    SubApplicationP = provider.CreateParameter("SubApplication", null, DbType.String);

                DbParameter[] Params = new DbParameter[2] { MainApplicationIDP, SubApplicationP };
                using (DataSet DataSet = provider.Select("ZDMS_GetSubApplication", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Sub.Add(new PDMS_SubApplication()
                            {
                                MainApplicationID = Convert.ToInt32(dr["MainApplicationID"]),
                                SubApplicationID = Convert.ToInt32(dr["SubApplicationID"]),
                                SubApplication = Convert.ToString(dr["SubApplication"]),
                                MainApplication=new PDMS_MainApplication()
                                {
                                    MainApplicationID = Convert.ToInt32(dr["MainApplicationID"]),
                                    MainApplication = Convert.ToString(dr["MainApplication"])
                                }
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Service", "ZDMS_GetSubApplication", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", "ZDMS_GetSubApplication", ex);
                throw ex;
            }
            return Sub;
        }
        public List<PDMS_ServiceCharge> GetServiceCharges(long ICTicketID, long? ServiceChargeID, string MaterialCode, Boolean? IsDeleted)
        {
            List<PDMS_ServiceCharge> ServiceCharges = new List<PDMS_ServiceCharge>();
            try
            {
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter ServiceChargeIDP = provider.CreateParameter("ServiceChargeID", ServiceChargeID, DbType.Int64);

                DbParameter MaterialCodeP;
                if (!string.IsNullOrEmpty(MaterialCode))
                    MaterialCodeP = provider.CreateParameter("MaterialCode", MaterialCode, DbType.String);
                else
                    MaterialCodeP = provider.CreateParameter("MaterialCode", null, DbType.String);
                DbParameter IsDeletedP = provider.CreateParameter("IsDeleted", IsDeleted, DbType.Boolean);

                DbParameter[] Params = new DbParameter[4] { ICTicketIDP, ServiceChargeIDP, MaterialCodeP, IsDeletedP };
                using (DataSet DataSet = provider.Select("ZDMS_GetServiceCharges", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServiceCharges.Add(new PDMS_ServiceCharge()
                            {
                                ServiceChargeID = Convert.ToInt64(dr["ServiceChargeID"]),
                                ICTicketID = Convert.ToInt64(dr["ICTicketID"]),
                                Item = Convert.ToInt32(dr["Item"]),
                                Material = new PDMS_Material()
                                {
                                    MaterialID = Convert.ToInt32(dr["MaterialID"]),
                                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                    MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                    IsMainServiceMaterial = dr["IsMainServiceMaterial"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsMainServiceMaterial"]),
                                    MaterialGroup = Convert.ToString(dr["MaterialGroup"]),
                                },
                                Date = Convert.ToDateTime(dr["Date"]),
                                WorkedHours = Convert.ToDecimal(dr["WorkedHours"]),
                                BasePrice = Convert.ToDecimal(dr["BasePrice"]),
                                Discount = Convert.ToDecimal(dr["Discount"]),
                                SGST = Convert.ToInt32(dr["SGST"]),
                                IGST = Convert.ToInt32(dr["IGST"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                                IsClaimOrInvRequested = dr["IsClaimOrInvRequested"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsClaimOrInvRequested"]),
                                ClaimNumber = Convert.ToString(dr["ClaimNumber"]),
                                QuotationNumber = Convert.ToString(dr["QuotationNumber"]),
                                ProformaInvoiceNumber = Convert.ToString(dr["ProformaInvoiceNumber"]),
                                InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]),
                                IsDeleted = Convert.ToBoolean(dr["IsDeleted"]),
                                TSIR = dr["TsirID"] == DBNull.Value ? null : new PDMS_ICTicketTSIR()
                                {
                                    TsirID = Convert.ToInt64(dr["TsirID"]),
                                    TsirNumber = Convert.ToString(dr["TsirNumber"]),
                                    TsirDate = Convert.ToDateTime(dr["TsirDate"]),
                                    Status = new PDMS_ICTicketTSIRStatus { StatusID = Convert.ToInt32(dr["StatusID"]) }
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
            return ServiceCharges;
        }
        public List<PDMS_ServiceMaterial> GetServiceMaterials(long? ICTicketID, long? ServiceMaterialID, long? TsirID, string Material, Boolean? IsDeleted, string QuotationNumber)
        {
            List<PDMS_ServiceMaterial> ServiceMaterials = new List<PDMS_ServiceMaterial>();
            try
            {
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter ServiceMaterialIDP = provider.CreateParameter("ServiceMaterialID", ServiceMaterialID, DbType.Int64);
                DbParameter TsirIDP = provider.CreateParameter("TsirID", TsirID, DbType.Int64);
                DbParameter MaterialP = provider.CreateParameter("Material", string.IsNullOrEmpty(Material) ? null : Material, DbType.String);
                DbParameter QuotationNumberP = provider.CreateParameter("QuotationNumber", string.IsNullOrEmpty(QuotationNumber) ? null : QuotationNumber, DbType.String);

                DbParameter IsDeletedP = provider.CreateParameter("IsDeleted", IsDeleted, DbType.Boolean);
                DbParameter[] Params = new DbParameter[6] { ICTicketIDP, ServiceMaterialIDP, TsirIDP, MaterialP, IsDeletedP, QuotationNumberP };
                using (DataSet DataSet = provider.Select("ZDMS_GetServiceMaterials", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServiceMaterials.Add(new PDMS_ServiceMaterial()
                            {
                                ServiceMaterialID = Convert.ToInt64(dr["ServiceMaterialID"]),
                                ICTicketID = Convert.ToInt64(dr["ICTicketID"]),
                                Item = Convert.ToInt32(dr["Item"]),
                                TSIR = dr["TsirID"] == DBNull.Value ? null : new PDMS_ICTicketTSIR() 
                                { 
                                    TsirID = Convert.ToInt64(dr["TsirID"]), 
                                    TsirNumber = Convert.ToString(dr["TsirNumber"]),
                                    Status = new PDMS_ICTicketTSIRStatus() 
                                    { StatusID = Convert.ToInt32(dr["TsirStatusID"]) } 
                                },
                                Material = new PDMS_Material()
                                {
                                    MaterialID = Convert.ToInt32(dr["MaterialID"]),
                                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                    MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                    MaterialSerialNumber = Convert.ToString(dr["MaterialSN"]),
                                    MaterialGroup = Convert.ToString(dr["MaterialGroup"])
                                },

                                DefectiveMaterial = dr["DefectiveMaterialID"] == DBNull.Value ? null : new PDMS_Material() { MaterialID = Convert.ToInt32(dr["DefectiveMaterialID"]), MaterialCode = Convert.ToString(dr["DefectiveMaterialCode"]), MaterialDescription = Convert.ToString(dr["DefectiveMaterialDescription"]), MaterialSerialNumber = Convert.ToString(dr["DefectiveMaterialSN"]) },

                                Qty = Convert.ToInt32(dr["Qty"]),
                                AvailableQty = Convert.ToInt32(dr["AvailableQty"]),
                                //  IsCustomerStock = Convert.ToBoolean(dr["IsCustomerStock"]),
                                IsFaultyPart = Convert.ToBoolean(dr["IsFaultyPart"]),
                                ReceivingStatus = Convert.ToString(dr["ReceivingStatus"]),
                                BasePrice = Convert.ToDecimal(dr["BasePrice"]),
                                Discount = Convert.ToDecimal(dr["Discount"]),
                                SGST = Convert.ToInt32(dr["SGST"]),
                                IGST = Convert.ToInt32(dr["IGST"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                                IsClaimOrQtnRequested = dr["IsClaimOrQtnRequested"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsClaimOrQtnRequested"]),
                                QuotationNumber = Convert.ToString(dr["QuotationNumber"]),
                                DeliveryNumber = Convert.ToString(dr["DeliveryNumber"]),
                                SaleOrderNumber = Convert.ToString(dr["SaleOrderNumber"]),
                                ClaimNumber = Convert.ToString(dr["ClaimNumber"]),
                                IsDeleted = Convert.ToBoolean(dr["IsDeleted"]),
                                PONumber = Convert.ToString(dr["PONumber"]),
                                MaterialSource = dr["MaterialSourceID"] == DBNull.Value ? null : new PDMS_MaterialSource() { MaterialSourceID = Convert.ToInt32(dr["MaterialSourceID"]), MaterialSource = Convert.ToString(dr["MaterialSource"]) },
                                IsRecomenedParts = dr["IsRecomenedParts"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsRecomenedParts"]),
                                IsQuotationParts = dr["IsQuotationParts"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsQuotationParts"]),
                                WarrantyMaterialReturnStatus = Convert.ToString(dr["WarrantyMaterialReturnStatus"]), 
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

        public List<PDMS_NoteType> GetNoteType(int? NoteTypeID, string NoteType)
        {
            List<PDMS_NoteType> NoteTypes = new List<PDMS_NoteType>();
            try
            {

                DbParameter NoteTypeIDP = provider.CreateParameter("NoteTypeID", NoteTypeID, DbType.Int32);
                DbParameter NoteTypeP;
                if (!string.IsNullOrEmpty(NoteType))
                    NoteTypeP = provider.CreateParameter("NoteType", NoteType, DbType.String);
                else
                    NoteTypeP = provider.CreateParameter("NoteType", null, DbType.String);

                DbParameter[] Params = new DbParameter[2] { NoteTypeIDP, NoteTypeP };
                using (DataSet DataSet = provider.Select("ZDMS_GetNoteType", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            NoteTypes.Add(new PDMS_NoteType()
                            {
                                NoteTypeID = Convert.ToInt32(dr["NoteTypeID"]),
                                NoteType = Convert.ToString(dr["NoteType"]),
                                NoteCode = Convert.ToString(dr["NoteCode"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return NoteTypes;
        }
        public List<PDMS_ServiceNote> GetServiceNote(long? ICTicketID, long? ServiceNoteID, int? NoteTypeID, string NoteType)
        {
            List<PDMS_ServiceNote> ServiceMaterials = new List<PDMS_ServiceNote>();
            try
            {


                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter ServiceNoteIDP = provider.CreateParameter("ServiceNoteID", ServiceNoteID, DbType.Int64);
                DbParameter NoteTypeIDP = provider.CreateParameter("NoteTypeID", NoteTypeID, DbType.Int64);
                DbParameter NoteTypeP;
                if (!string.IsNullOrEmpty(NoteType))
                    NoteTypeP = provider.CreateParameter("NoteType", NoteType, DbType.String);
                else
                    NoteTypeP = provider.CreateParameter("NoteType", null, DbType.String);

                DbParameter[] Params = new DbParameter[4] { ICTicketIDP, ServiceNoteIDP, NoteTypeIDP, NoteTypeP };
                using (DataSet DataSet = provider.Select("ZDMS_GetServiceNote", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServiceMaterials.Add(new PDMS_ServiceNote()
                            {
                                ServiceNoteID = Convert.ToInt64(dr["ServiceNoteID"]),
                                ICTicketID = Convert.ToInt64(dr["ICTicketID"]),
                                Comments = Convert.ToString(dr["Comments"]),
                                NoteType = new PDMS_NoteType()
                                {
                                    NoteTypeID = Convert.ToInt32(dr["NoteTypeID"]),
                                    NoteType = Convert.ToString(dr["NoteType"]),
                                    NoteCode = Convert.ToString(dr["NoteCode"])
                                },
                                CreatedOn = Convert.ToDateTime(dr["CreatedOn"])

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
        public List<PDMS_ServiceNote> GetServiceNoteForMTTR(int? DealerID, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? ServiceStatusID,int UserID)
        {
            List<PDMS_ServiceNote> ServiceMaterials = new List<PDMS_ServiceNote>();
            try
            { 
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter ServiceStatusIDP = provider.CreateParameter("ServiceStatusID", ServiceStatusID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, ServiceStatusIDP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetServiceNoteForMTTR", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServiceMaterials.Add(new PDMS_ServiceNote()
                            {
                               // ServiceNoteID = Convert.ToInt64(dr["ServiceNoteID"]),
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
        public List<PDMS_CustomerSatisfactionLevel> GetCustomerSatisfactionLevel(int? CustomerSatisfactionLevelID, string CustomerSatisfactionLevel)
        {
            List<PDMS_CustomerSatisfactionLevel> Category1s = new List<PDMS_CustomerSatisfactionLevel>();
            try
            {
                DbParameter CustomerSatisfactionLevelP;
                DbParameter CustomerSatisfactionLevelIDP = provider.CreateParameter("CustomerSatisfactionLevelID", CustomerSatisfactionLevelID, DbType.Int32);
                if (!string.IsNullOrEmpty(CustomerSatisfactionLevel))
                    CustomerSatisfactionLevelP = provider.CreateParameter("CustomerSatisfactionLevel", CustomerSatisfactionLevel, DbType.String);
                else
                    CustomerSatisfactionLevelP = provider.CreateParameter("CustomerSatisfactionLevel", null, DbType.String);

                DbParameter[] Params = new DbParameter[2] { CustomerSatisfactionLevelIDP, CustomerSatisfactionLevelP };
                using (DataSet DataSet = provider.Select("ZDMS_GetCustomerSatisfactionLevel", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Category1s.Add(new PDMS_CustomerSatisfactionLevel()
                            {
                                CustomerSatisfactionLevelID = Convert.ToInt32(dr["CustomerSatisfactionLevelID"]),
                                CustomerSatisfactionLevel = Convert.ToString(dr["CustomerSatisfactionLevel"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Category1s;
        }
         public List<PDMS_PaidServiceInvoice> GetPaidServiceInvoice(long? ServiceInvoiceID, long? ICTicketID, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID, string CustomerCode, Boolean? IsNotDeleted)
        {
            List<PDMS_PaidServiceInvoice> Services = new List<PDMS_PaidServiceInvoice>();
            try
            {
                DbParameter ServiceInvoiceIDP = provider.CreateParameter("ServiceInvoiceID", ServiceInvoiceID, DbType.Int64);
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter InvoiceNumberP;
                if (!string.IsNullOrEmpty(InvoiceNumber))
                    InvoiceNumberP = provider.CreateParameter("InvoiceNumber", InvoiceNumber, DbType.String);
                else
                    InvoiceNumberP = provider.CreateParameter("InvoiceNumber", null, DbType.String);

                DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

                DbParameter CustomerCodeP;
                if (!string.IsNullOrEmpty(CustomerCode))
                    CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                else
                    CustomerCodeP = provider.CreateParameter("CustomerCode", null, DbType.String);

                DbParameter IsNotDeletedP = provider.CreateParameter("IsNotDeleted", IsNotDeleted, DbType.Boolean);
                DbParameter[] Params = new DbParameter[8] { ServiceInvoiceIDP, ICTicketIDP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerIDP, CustomerCodeP, IsNotDeletedP };

                PDMS_PaidServiceInvoice Service = null;
                long InvoiceID = 0;
                using (DataSet DataSet = provider.Select("ZDMS_GetPaidServiceInvoice", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["ServiceInvoiceID"]))
                            {
                                Service = new PDMS_PaidServiceInvoice();
                                Services.Add(Service);
                                Service.PaidServiceInvoiceID = Convert.ToInt64(dr["ServiceInvoiceID"]);
                                Service.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                Service.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                                Service.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                //Service.TCSValue = DBNull.Value == dr["TCSValue"] ? 0 : Convert.ToDecimal(dr["TCSValue"]);
                                //Service.TCSTax = DBNull.Value == dr["TCSTax"] ? 0 : Convert.ToDecimal(dr["TCSTax"]);
                                Service.Through = Convert.ToString(dr["Through"]);
                                Service.LRNumber = Convert.ToString(dr["LRNumber"]);
                                Service.IsActiveInvoice = Convert.ToBoolean(dr["IsActiveInvoice"]);
                                Service.IRN = Convert.ToString(dr["IRN"]);
                                //Service.ScopOfWork = Convert.ToString(dr["ScopOfWork"]);
                                //Service.Remarks = Convert.ToString(dr["Remarks"]);
                                
                                Service.ICTicket = new PDMS_ICTicket();
                                Service.ICTicket.ICTicketID = Convert.ToInt32(dr["ICTicketID"]);
                                Service.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                                Service.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                                Service.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                                Service.ICTicket.FSRNumber = Convert.ToString(dr["FSRNumber"]);
                                Service.ICTicket.FSRDate = dr["FSRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FSRDate"]);//Convert.ToString(dr["FSRDate"]);

                                Service.ICTicket.Equipment = new PDMS_EquipmentHeader();
                                Service.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);

                                Service.ICTicket.Dealer = new PDMS_Dealer();
                                Service.ICTicket.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                                Service.ICTicket.Dealer.DealerName = Convert.ToString(dr["ContactName"]);
                                Service.ICTicket.Dealer.IsEInvoice = dr["IsEInvoice"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsEInvoice"]);
                                Service.ICTicket.Dealer.EInvoiceDate = DBNull.Value == dr["EInvoiceDate"] ? (DateTime?)null : Convert.ToDateTime(dr["EInvoiceDate"]);
                                Service.ICTicket.Dealer.DealerBank = new PDealerBankDetails();
                                Service.ICTicket.Dealer.DealerBank.BankName = Convert.ToString(dr["BankName"]);
                                Service.ICTicket.Dealer.DealerBank.Branch = Convert.ToString(dr["Branch"]);
                                Service.ICTicket.Dealer.DealerBank.AcNumber = Convert.ToString(dr["AcNumber"]);
                                Service.ICTicket.Dealer.DealerBank.IfscCode = Convert.ToString(dr["IfscCode"]);

                                Service.ICTicket.Customer = new PDMS_Customer();
                                Service.ICTicket.Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                                Service.ICTicket.Customer.CustomerName = Convert.ToString(dr["CustomerName"]);
                                Service.ICTicket.Customer.GSTIN = Convert.ToString(dr["GSTNo"]);

                                Service.ICTicket.ScopeOfWork = Convert.ToString(dr["ScopeOfWork"]);
                                Service.ICTicket.NoOfDays = Convert.ToDecimal(dr["WorkedDay"]);
                                Service.ICTicket.Remarks = Convert.ToString(dr["Remarks"]);
                                Service.ICTicket.KindAttn = Convert.ToString(dr["KindAttn"]);

                                InvoiceID = Service.PaidServiceInvoiceID;

                                Service.InvoiceDetails = new PDMS_PaidServiceInvoiceDetails();
                                Service.InvoiceDetails.SupplierGSTIN = Convert.ToString(dr["SupplierGSTIN"]);
                                Service.InvoiceDetails.Supplier_addr1 = Convert.ToString(dr["Supplier_addr1"]);
                                Service.InvoiceDetails.SupplierLocation = Convert.ToString(dr["SupplierLocation"]);
                                Service.InvoiceDetails.SupplierPincode = Convert.ToString(dr["SupplierPincode"]);
                                Service.InvoiceDetails.SupplierStateCode = Convert.ToString(dr["SupplierStateCode"]);

                                Service.InvoiceDetails.BuyerGSTIN = Convert.ToString(dr["BuyerGSTIN"]);
                                Service.InvoiceDetails.BuyerName = Convert.ToString(dr["CustomerName"]);
                                Service.InvoiceDetails.BuyerStateCode = Convert.ToString(dr["BuyerStateCode"]);
                                Service.InvoiceDetails.Buyer_addr1 = Convert.ToString(dr["Buyer_addr1"]);
                                Service.InvoiceDetails.Buyer_loc = Convert.ToString(dr["Buyer_loc"]);
                                Service.InvoiceDetails.BuyerPincode = Convert.ToString(dr["BuyerPincode"]);


                                Service.InvoiceItems = new List<PDMS_PaidServiceInvoiceItem>();
                            }
                            Service.InvoiceItems.Add(new PDMS_PaidServiceInvoiceItem()
                            {
                                Material = new PDMS_Material()
                                {
                                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                    MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                    HSN = Convert.ToString(dr["HSNCode"])
                                },
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Rate = Convert.ToDecimal(dr["Rate"]),
                                Discount = Convert.ToDecimal(dr["Discount"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToInt32(dr["CGST"]),
                                SGST = Convert.ToInt32(dr["SGST"]),
                                IGST = Convert.ToInt32(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                                CessValue = dr["CessValue"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CessValue"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Services;
        }
        //public PDMS_PaidServiceInvoiceE GetPaidServiceInvoiceE(long ServiceInvoiceID)
        //{
        //    PDMS_PaidServiceInvoiceE Service = new PDMS_PaidServiceInvoiceE();
        //    try
        //    {
        //        DbParameter ServiceInvoiceIDP = provider.CreateParameter("ServiceInvoiceID", ServiceInvoiceID, DbType.Int64);
        //        DbParameter[] Params = new DbParameter[1] { ServiceInvoiceIDP };
        //        using (DataSet DataSet = provider.Select("ZDMS_GetPaidServiceInvoice", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {

        //                    Service.PaidServiceInvoiceEID = Convert.ToInt64(dr["PaidServiceInvoiceEID"]);
        //                    Service.PaidServiceInvoiceID = Convert.ToInt64(dr["PaidServiceInvoiceID"]);
        //                    Service.IRN = Convert.ToString(dr["IRN"]);
        //                    Service.SignedQRCode = Convert.ToString(dr["SignedQRCode"]);
        //                    Service.SignedInvoice = Convert.ToString(dr["SignedInvoice"]);
        //                    Service.Comments = Convert.ToString(dr["Comments"]);
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return Service;
        //}
        
        private PAttachedFile ServiceInvoicefile(long ServiceInvoiceHeaderID)
        {
            try
            {
                PDMS_PaidServiceInvoice PaidServiceInvoice = new BDMS_Service().GetPaidServiceInvoice(ServiceInvoiceHeaderID, null, "", null, null, null, "",null)[0];
                // PDMS_PaidServiceInvoiceE PaidServiceInvoiceE = new BDMS_Service().GetPaidServiceInvoiceE(ServiceInvoiceHeaderID) ;
                //PDMS_Customer Dealer = new SCustomer().getCustomerAddress(PaidServiceInvoice.ICTicket.Dealer.DealerCode);
                PDMS_Customer Dealer = new BDMS_Customer().getDealerAddressFromSAP(PaidServiceInvoice.ICTicket.Dealer.DealerCode);
                string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

                //PDMS_Customer Customer = new SCustomer().getCustomerAddress(PaidServiceInvoice.ICTicket.Customer.CustomerCode);
                //PDMS_Customer Customer = new BDMS_Customer().getCustomerAddressFromSAP(PaidServiceInvoice.ICTicket.Customer.CustomerCode);
                PDMS_Customer CustomerC = new BDMS_Customer().GetCustomerByCode(null, PaidServiceInvoice.ICTicket.Customer.CustomerCode)[0];
                PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(CustomerC.CustomerID);
                string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

                //if (string.IsNullOrEmpty(Customer.GSTIN))
                //{
                //    DataTable dt = new NpgsqlServer().ExecuteReader("select r_value as GSTIN from doohr_bp_statutory where r_statutory_type='GSTIN' and s_tenant_id=" + PaidServiceInvoice.ICTicket.Dealer.DealerCode + " and p_bp_id='" + PaidServiceInvoice.ICTicket.Customer.CustomerCode + "' limit 1");
                //    if (dt.Rows.Count == 1)
                //    {
                //        Customer.GSTIN = Convert.ToString(dt.Rows[0][0]);
                //    }
                //}

                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("Rate");
                CommissionDT.Columns.Add("Value", typeof(decimal));
                CommissionDT.Columns.Add("CGST");
                CommissionDT.Columns.Add("SGST");
                CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                //  decimal GrandTotal = 0;
                string StateCode = Dealer.State.StateCode;
                string GST_Header = "";
                int i = 0;
                decimal CessValue = 0;

                decimal CessSubTotal = 0;
                foreach (PDMS_PaidServiceInvoiceItem item in PaidServiceInvoice.InvoiceItems)
                {
                    i = i + 1;
                    if (item.SGST != 0)
                    {
                        GST_Header = "CGST & SGST";
                        CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, item.TaxableValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.TaxableValue + item.CGSTValue + item.SGSTValue);

                        CessValue = CessValue + item.CessValue;
                        CessSubTotal = item.TaxableValue + item.CGSTValue + item.SGSTValue + item.CessValue;
                    }
                    else
                    {
                        GST_Header = "IGST";
                        CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, item.TaxableValue, item.IGST, null, item.IGSTValue, null, item.TaxableValue + item.IGSTValue);

                        CessValue = CessValue + item.CessValue;
                        CessSubTotal = item.TaxableValue + item.IGSTValue + item.CessValue;

                    }
                }
                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;

                ReportParameter[] P =null;
                PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().GetPaidServiceInvoiceESigned(ServiceInvoiceHeaderID);
                if (PaidServiceInvoice.ICTicket.Dealer.ServicePaidEInvoice == false && string.IsNullOrEmpty(PaidServiceInvoice.IRN))
                {
                    P = new ReportParameter[28];
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_PaidServiceInvoice.rdlc");
                }
                else if((PaidServiceInvoice.ICTicket.Dealer.IsEInvoice) && (PaidServiceInvoice.ICTicket.Dealer.EInvoiceDate <= PaidServiceInvoice.InvoiceDate) && (Customer.GSTIN != "URD"))
                { 
                    P = new ReportParameter[30];
                    P[28] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, PaidServiceInvoice.InvoiceNumber), false);
                    P[29] = new ReportParameter("IRN", "IRN : " + PaidServiceInvoice.IRN, false);
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_PaidServiceInvoiceQRCode.rdlc");
                }
                else
                {
                    P = new ReportParameter[28];
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_PaidServiceInvoice.rdlc");
                }
               
                //   ViewState["Month"] = ddlMonth.SelectedValue;
                P[0] = new ReportParameter("DealerCode", PaidServiceInvoice.ICTicket.Dealer.DealerCode, false);
                P[1] = new ReportParameter("DealerName", PaidServiceInvoice.ICTicket.Dealer.DealerName, false);
                P[2] = new ReportParameter("Address1", DealerAddress1, false);
                P[3] = new ReportParameter("Address2", DealerAddress2, false);
                P[4] = new ReportParameter("Contact", "Contact", false);
                P[5] = new ReportParameter("GSTIN", Dealer.GSTIN, false);
                P[6] = new ReportParameter("GST_Header", GST_Header, false);
                P[7] = new ReportParameter("GrandTotal", (PaidServiceInvoice.GrandTotal).ToString(), false);
                P[8] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(PaidServiceInvoice.GrandTotal)), false);
                P[9] = new ReportParameter("InvoiceNumber", PaidServiceInvoice.InvoiceNumber, false);

                P[10] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                P[11] = new ReportParameter("CustomerName", PaidServiceInvoice.ICTicket.Customer.CustomerName, false);
                P[12] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                P[13] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                P[14] = new ReportParameter("CustomerMail", Customer.Email, false);
                P[15] = new ReportParameter("CustomerStateCode", Customer.State.StateCode, false);
                P[16] = new ReportParameter("CustomerGST", Customer.GSTIN, false);
                P[17] = new ReportParameter("ICTicketNo", PaidServiceInvoice.ICTicket.ICTicketNumber, false);
                P[18] = new ReportParameter("KindAttn", PaidServiceInvoice.ICTicket.KindAttn, false);
                P[19] = new ReportParameter("YourRef", PaidServiceInvoice.ICTicket.FSRNumber + " " + PaidServiceInvoice.ICTicket.FSRDate, false);
                P[20] = new ReportParameter("OurRef", PaidServiceInvoice.ICTicket.ICTicketNumber + " " + PaidServiceInvoice.ICTicket.Equipment.EquipmentSerialNo, false);
                P[21] = new ReportParameter("Remarks", PaidServiceInvoice.ICTicket.Remarks, false);
                P[22] = new ReportParameter("NoOfDays", Convert.ToString(PaidServiceInvoice.ICTicket.NoOfDays), false);
                P[23] = new ReportParameter("ScopOfWork", PaidServiceInvoice.ICTicket.ScopeOfWork, false);
                P[24] = new ReportParameter("BankDetails", "Our Bank details are : A/C No " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.AcNumber + ", Bank : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.BankName + ", Branch : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.Branch + ", IFSC Code : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.IfscCode, false);
                P[25] = new ReportParameter("InvDate", PaidServiceInvoice.InvoiceDate.ToShortDateString(), false);
                P[26] = new ReportParameter("CessValue", Convert.ToString(CessValue), false);
                P[27] = new ReportParameter("CessSubTotal", Convert.ToString(CessSubTotal), false);
               
              
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);
                report.SetParameters(P);
                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                PAttachedFile InvF = new PAttachedFile();

                InvF.FileType = mimeType;
                InvF.AttachedFile = mybytes;
                InvF.AttachedFileID = 0;
                return InvF;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public void insertServiceInvoiceFile(long ServiceInvoiceID, PAttachedFile InvFile)
        {
            DbParameter ServiceInvoiceIDP = provider.CreateParameter("ServiceInvoiceID", ServiceInvoiceID, DbType.String);
            DbParameter InvFileP = provider.CreateParameter("InvFile", InvFile.AttachedFile, DbType.Binary);
            DbParameter FileTypeP = provider.CreateParameter("FileType", InvFile.FileType, DbType.String);
            DbParameter[] Params = new DbParameter[3] { ServiceInvoiceIDP, InvFileP, FileTypeP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertServiceInvoiceFile", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Service", "insertServiceInvoiceFile", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", " insertServiceInvoiceFile", ex);
            }
        }
        public PAttachedFile GetServiceInvoiceFile(long ServiceInvoiceID)
        {
            DbParameter ServiceInvoiceIDP = provider.CreateParameter("ServiceInvoiceID", ServiceInvoiceID, DbType.Int64);
            PAttachedFile Files = null;
            try
            {

                string endPoint = "ICTicket/GetServiceInvoiceFile?ServiceInvoiceID=" + ServiceInvoiceID ;
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                Files   = JsonConvert.DeserializeObject<PAttachedFile>(JsonConvert.SerializeObject(Result.Data));

                if (Files == null)
                {
                    new BDMS_Service().insertServiceInvoiceFile(ServiceInvoiceID, ServiceInvoicefile(ServiceInvoiceID));
                }
                return Files;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", "GetServiceInvoiceFile", ex);
                return null;
            }

        }

        public Boolean InsertServiceQuotationOrProformaOrInvoice(PDMS_ICTicket ICTicket, Boolean IsIGST, int CreatedBy, int Type, PDMS_Customer Dealer, PDMS_Customer Customer)
        {

            int success = 0;

            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicket.ICTicketID, DbType.Int64);
            DbParameter CreatedByP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int32);
            DbParameter TypeP = provider.CreateParameter("Type", Type, DbType.Int32);

            DbParameter SGSTIN = provider.CreateParameter("SupplierGSTIN", Dealer.GSTIN, DbType.String);
            DbParameter SAddr1 = provider.CreateParameter("Supplier_addr1", Dealer.Address12, DbType.String);
            DbParameter SAddr2 = provider.CreateParameter("Supplier_addr2", Dealer.Address3, DbType.String);
            DbParameter SLocation = provider.CreateParameter("SupplierLocation", Dealer.City, DbType.String);
            DbParameter SPincode = provider.CreateParameter("SupplierPincode", Dealer.Pincode, DbType.String);
            DbParameter SStateCode = provider.CreateParameter("SupplierStateCode", Dealer.State.StateCode, DbType.String);

            DbParameter BGSTIN = provider.CreateParameter("BuyerGSTIN", Customer.GSTIN, DbType.String);
            DbParameter BStateCode = provider.CreateParameter("BuyerStateCode", Customer.State.StateCode, DbType.String);
            DbParameter BAddr1 = provider.CreateParameter("Buyer_addr1", Customer.Address12, DbType.String);
            DbParameter BAddr2 = provider.CreateParameter("Buyer_addr2", Customer.Address3, DbType.String);
            DbParameter BLoc = provider.CreateParameter("Buyer_loc", Customer.City, DbType.String);
            DbParameter BPincode = provider.CreateParameter("BuyerPincode", Customer.Pincode, DbType.String);

            DbParameter[] Params = new DbParameter[15] { ICTicketIDP, CreatedByP, TypeP, SGSTIN, SAddr1, SAddr2, SLocation, SPincode, SStateCode, BGSTIN, BStateCode, BAddr1, BAddr2, BLoc, BPincode };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
                    {
                        success = provider.Insert("ZDMS_InsertServiceQuotationOrProformaOrInvoiceOverhaul", Params);
                    }
                    else
                    {
                        success = provider.Insert("ZDMS_InsertServiceQuotationOrProformaOrInvoice1", Params);
                    }
                    scope.Complete();
                }
                //  new BDMS_Service().insertServiceInvoiceFile(ServiceInvoiceHeaderID, ServiceInvoicefile(ServiceInvoiceHeaderID));
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Service", "InsertServiceQuotationOrProformaOrInvoice", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", " InsertServiceQuotationOrProformaOrInvoice", ex);
                return false;
            }
            return true;
        }
        public Boolean CancelServiceQuotationOrProformaOrInvoice(long ServiceID, int CreatedBy, int Type)
        {

            int success = 0;

            DbParameter ServiceIDP = provider.CreateParameter("ServiceID", ServiceID, DbType.Int64);
            DbParameter CreatedByP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int32);
            DbParameter TypeP = provider.CreateParameter("Type", Type, DbType.Int32);

            DbParameter[] Params = new DbParameter[3] { ServiceIDP, CreatedByP, TypeP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_CancelServiceQuotationOrProformaOrInvoice", Params);
                    scope.Complete();
                }
                //  new BDMS_Service().insertServiceInvoiceFile(ServiceInvoiceHeaderID, ServiceInvoicefile(ServiceInvoiceHeaderID));
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Service", "CancelServiceQuotationOrProformaOrInvoice", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", " CancelServiceQuotationOrProformaOrInvoice", ex);
                return false;
            }
            return true;
        }
        public List<PDMS_PaidServiceInvoice> GetPaidServiceQuotation(long? ServiceQuotationID, long? ICTicketID, string QuotationNumber, DateTime? QuotationDateF, DateTime? QuotationDateT, int? DealerID, string CustomerCode)
        {
            List<PDMS_PaidServiceInvoice> Services = new List<PDMS_PaidServiceInvoice>();
            try
            {
                DbParameter ServiceQuotationIDP = provider.CreateParameter("ServiceQuotationID", ServiceQuotationID, DbType.Int64);
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                //  DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int64);
                DbParameter QuotationNumberP;
                if (!string.IsNullOrEmpty(QuotationNumber))
                    QuotationNumberP = provider.CreateParameter("QuotationNumber", QuotationNumber, DbType.String);
                else
                    QuotationNumberP = provider.CreateParameter("QuotationNumber", null, DbType.String);

                DbParameter QuotationDateFP = provider.CreateParameter("QuotationDateF", QuotationDateF, DbType.DateTime);
                DbParameter QuotationDateTP = provider.CreateParameter("QuotationDateT", QuotationDateT, DbType.DateTime);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

                DbParameter CustomerCodeP;
                if (!string.IsNullOrEmpty(CustomerCode))
                    CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                else
                    CustomerCodeP = provider.CreateParameter("CustomerCode", null, DbType.String);

                DbParameter[] Params = new DbParameter[7] { ServiceQuotationIDP, ICTicketIDP, QuotationNumberP, QuotationDateFP, QuotationDateTP, DealerIDP, CustomerCodeP };

                PDMS_PaidServiceInvoice Service = null;
                long InvoiceID = 0;
                using (DataSet DataSet = provider.Select("ZDMS_GetPaidServiceQuotation", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["ServiceQuotationID"]))
                            {
                                Service = new PDMS_PaidServiceInvoice();
                                Services.Add(Service);
                                Service.PaidServiceInvoiceID = Convert.ToInt64(dr["ServiceQuotationID"]);
                                Service.InvoiceNumber = Convert.ToString(dr["QuotationNumber"]);
                                Service.InvoiceDate = Convert.ToDateTime(dr["QuotationDate"]);
                                Service.GrandTotal = (dr["GrandTotal"]== DBNull.Value) ?0:Convert.ToInt32(dr["GrandTotal"]);
                                Service.Through = Convert.ToString(dr["Through"]);
                                Service.LRNumber = Convert.ToString(dr["LRNumber"]);

                                Service.ICTicket = new PDMS_ICTicket();
                                Service.ICTicket.ICTicketID = Convert.ToInt32(dr["ICTicketID"]);
                                Service.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                                Service.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                                Service.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                                Service.ICTicket.FSRNumber = Convert.ToString(dr["FSRNumber"]);
                                Service.ICTicket.FSRDate = dr["FSRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FSRDate"]);// Convert.ToString(dr["FSRDate"]);

                                Service.ICTicket.Equipment = new PDMS_EquipmentHeader();
                                Service.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);

                                Service.ICTicket.Dealer = new PDMS_Dealer();
                                Service.ICTicket.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                                Service.ICTicket.Dealer.DealerName = Convert.ToString(dr["ContactName"]);
                                Service.ICTicket.Dealer.DealerBank = new PDealerBankDetails();
                                Service.ICTicket.Dealer.DealerBank.BankName = Convert.ToString(dr["BankName"]);
                                Service.ICTicket.Dealer.DealerBank.Branch = Convert.ToString(dr["Branch"]);
                                Service.ICTicket.Dealer.DealerBank.AcNumber = Convert.ToString(dr["AcNumber"]);
                                Service.ICTicket.Dealer.DealerBank.IfscCode = Convert.ToString(dr["IfscCode"]);

                                Service.ICTicket.Customer = new PDMS_Customer();
                                Service.ICTicket.Customer.CustomerID =  Convert.ToInt64(dr["CustomerID"]);
                                Service.ICTicket.Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                                Service.ICTicket.Customer.CustomerName = Convert.ToString(dr["CustomerName"]);
                                Service.ICTicket.ScopeOfWork = Convert.ToString(dr["ScopeOfWork"]);
                                Service.ICTicket.Remarks = Convert.ToString(dr["Remarks"]);
                                Service.ICTicket.KindAttn = Convert.ToString(dr["KindAttn"]);
                                Service.ICTicket.NoOfDays = Convert.ToDecimal(dr["WorkedDay"]);
                                InvoiceID = Service.PaidServiceInvoiceID;
                                Service.IsDeletionAllowed = Convert.ToBoolean(dr["IsDeletionAllowed"]);
                                Service.InvoiceItems = new List<PDMS_PaidServiceInvoiceItem>();
                            }
                            Service.InvoiceItems.Add(new PDMS_PaidServiceInvoiceItem()
                            {
                                Material = new PDMS_Material()
                                {
                                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                    MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                    HSN = Convert.ToString(dr["HSNCode"])
                                },
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Rate = Convert.ToDecimal(dr["Rate"]),
                                Discount = Convert.ToDecimal(dr["Discount"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToInt32(dr["CGST"]),
                                SGST = Convert.ToInt32(dr["SGST"]),
                                IGST = Convert.ToInt32(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                                CessValue = dr["CessValue"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CessValue"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Services;
        }
        public PAttachedFile ServiceQuotationfile(long ServiceQuotationID)
        {
            try
            {
                PDMS_PaidServiceInvoice PaidServiceInvoice = new BDMS_Service().GetPaidServiceQuotation(ServiceQuotationID, null, "", null, null, null, "")[0];
                //PDMS_Customer Dealer = new SCustomer().getCustomerAddress(PaidServiceInvoice.ICTicket.Dealer.DealerCode);
                PDMS_Customer Dealer = new BDMS_Customer().getDealerAddressFromSAP(PaidServiceInvoice.ICTicket.Dealer.DealerCode);
                string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');


                //PDMS_Customer Customer = new SCustomer().getCustomerAddress(PaidServiceInvoice.ICTicket.Customer.CustomerCode);
                //PDMS_Customer Customer = new BDMS_Customer().getCustomerAddressFromSAP(PaidServiceInvoice.ICTicket.Customer.CustomerCode);
                PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(PaidServiceInvoice.ICTicket.Customer.CustomerID);
                string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("Rate");
                CommissionDT.Columns.Add("Value", typeof(decimal));
                CommissionDT.Columns.Add("CGST");
                CommissionDT.Columns.Add("SGST");
                CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                //  decimal GrandTotal = 0;
                string StateCode = Dealer.State.StateCode;
                string GST_Header = "";
                int i = 0;
                decimal CessValue = 0;
                decimal CessSubTotal = 0;

                foreach (PDMS_PaidServiceInvoiceItem item in PaidServiceInvoice.InvoiceItems)
                {

                    i = i + 1;
                    if (item.SGST != 0)
                    {
                        GST_Header = "CGST & SGST";
                        CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, item.TaxableValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.TaxableValue + item.CGSTValue + item.SGSTValue);
                       
                        CessValue = CessValue + item.CessValue;
                        CessSubTotal = item.TaxableValue + item.CGSTValue + item.SGSTValue + item.CessValue;
                    }
                    else
                    {
                        GST_Header = "IGST";
                        CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, item.TaxableValue, item.IGST, null, item.IGSTValue, null, item.TaxableValue + item.IGSTValue);

                        CessValue = CessValue + item.CessValue;
                        CessSubTotal = item.TaxableValue + item.IGSTValue + item.CessValue;
                    }
                }

                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;

                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;

                ReportParameter[] P = new ReportParameter[28];
                //   ViewState["Month"] = ddlMonth.SelectedValue;
                P[0] = new ReportParameter("DealerCode", PaidServiceInvoice.ICTicket.Dealer.DealerCode, false);
                P[1] = new ReportParameter("DealerName", PaidServiceInvoice.ICTicket.Dealer.DealerName, false);
                P[2] = new ReportParameter("Address1", DealerAddress1, false);
                P[3] = new ReportParameter("Address2", DealerAddress2, false);
                P[4] = new ReportParameter("Contact", "Contact", false);
                P[5] = new ReportParameter("GSTIN", Dealer.GSTIN, false);
                P[6] = new ReportParameter("GST_Header", GST_Header, false);
                P[7] = new ReportParameter("GrandTotal", (PaidServiceInvoice.GrandTotal).ToString(), false);
                P[8] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(PaidServiceInvoice.GrandTotal)), false);
                P[9] = new ReportParameter("InvoiceNumber", PaidServiceInvoice.InvoiceNumber, false);

                P[10] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                P[11] = new ReportParameter("CustomerName", PaidServiceInvoice.ICTicket.Customer.CustomerName, false);
                P[12] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                P[13] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                P[14] = new ReportParameter("CustomerMail", Customer.Email, false);
                P[15] = new ReportParameter("CustomerStateCode", Customer.GSTIN.Length > 2 ? Customer.GSTIN.Substring(0, 2) : "", false);
                P[16] = new ReportParameter("CustomerGST", Customer.GSTIN, false);
                P[17] = new ReportParameter("ICTicketNo", PaidServiceInvoice.ICTicket.ICTicketNumber, false);
                P[18] = new ReportParameter("KindAttn", PaidServiceInvoice.ICTicket.KindAttn, false);
                P[19] = new ReportParameter("YourRef", PaidServiceInvoice.ICTicket.FSRNumber + " " + PaidServiceInvoice.ICTicket.FSRDate, false);
                P[20] = new ReportParameter("OurRef", PaidServiceInvoice.ICTicket.ICTicketNumber + " " + PaidServiceInvoice.ICTicket.Equipment.EquipmentSerialNo, false);
                P[21] = new ReportParameter("Remarks", PaidServiceInvoice.ICTicket.Remarks, false);
                P[22] = new ReportParameter("NoOfDays", Convert.ToString(PaidServiceInvoice.ICTicket.NoOfDays), false);
                P[23] = new ReportParameter("ScopOfWork", PaidServiceInvoice.ICTicket.ScopeOfWork, false);
                P[24] = new ReportParameter("BankDetails", "Our Bank details are : A/C No " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.AcNumber + ", Bank : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.BankName + ", Branch : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.Branch + ", IFSC Code : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.IfscCode, false);
                P[25] = new ReportParameter("InvDate", PaidServiceInvoice.InvoiceDate.ToShortDateString(), false);
                P[26] = new ReportParameter("CessValue", Convert.ToString(CessValue), false);
                P[27] = new ReportParameter("CessSubTotal", Convert.ToString(CessSubTotal), false);

                report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_PaidServiceQuotation.rdlc");
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);
                report.SetParameters(P);
                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                PAttachedFile InvF = new PAttachedFile();

                InvF.FileType = mimeType;
                InvF.AttachedFile = mybytes;
                InvF.AttachedFileID = 0;
                InvF.FileName = "Quotation " + PaidServiceInvoice.InvoiceNumber;
                return InvF;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public Boolean InsertServiceProformaInvoice(long ICTicketID, int CreatedBy)
        {

            int success = 0;

            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
            DbParameter CreatedByP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int32);
            DbParameter[] Params = new DbParameter[2] { ICTicketIDP, CreatedByP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertServiceProformaInvoice", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Service", "InsertServiceProformaInvoice", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", " InsertServiceProformaInvoice", ex);
                return false;
            }
            return true;
        }
        public List<PDMS_PaidServiceInvoice> GetPaidServiceProformaInvoice(long? ServiceInvoiceID, long? ICTicketID, string Proforma, DateTime? ProformaDateF, DateTime? ProformaDateT, int? DealerID, string CustomerCode)
        {
            List<PDMS_PaidServiceInvoice> Services = new List<PDMS_PaidServiceInvoice>();
            try
            {
                DbParameter ServiceInvoiceIDP = provider.CreateParameter("ServiceInvoiceID", ServiceInvoiceID, DbType.Int64);
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter ProformaP;
                if (!string.IsNullOrEmpty(Proforma))
                    ProformaP = provider.CreateParameter("Proforma", Proforma, DbType.String);
                else
                    ProformaP = provider.CreateParameter("Proforma", null, DbType.String);

                DbParameter QuotationDateFP = provider.CreateParameter("ProformaDateF", ProformaDateF, DbType.DateTime);
                DbParameter QuotationDateTP = provider.CreateParameter("ProformaDateT", ProformaDateT, DbType.DateTime);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

                DbParameter CustomerCodeP;
                if (!string.IsNullOrEmpty(CustomerCode))
                    CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                else
                    CustomerCodeP = provider.CreateParameter("CustomerCode", null, DbType.String);

                DbParameter[] Params = new DbParameter[7] { ServiceInvoiceIDP, ICTicketIDP, ProformaP, QuotationDateFP, QuotationDateTP, DealerIDP, CustomerCodeP };

                PDMS_PaidServiceInvoice Service = null;
                long InvoiceID = 0;
                using (DataSet DataSet = provider.Select("ZDMS_GetPaidServiceProformaInvoice", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            if (InvoiceID != Convert.ToInt64(dr["ServiceInvoiceID"]))
                            {
                                Service = new PDMS_PaidServiceInvoice();
                                Services.Add(Service);
                                Service.PaidServiceInvoiceID = Convert.ToInt64(dr["ServiceInvoiceID"]);
                                Service.ProformaInvoiceNumber = Convert.ToString(dr["ProformaInvoiceNumber"]);
                                Service.ProformaInvoiceDate = Convert.ToDateTime(dr["ProformaInvoiceDate"]);
                                Service.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                Service.Through = Convert.ToString(dr["Through"]);
                                Service.LRNumber = Convert.ToString(dr["LRNumber"]);



                                Service.ICTicket = new PDMS_ICTicket();
                                Service.ICTicket.ICTicketID = Convert.ToInt32(dr["ICTicketID"]);
                                Service.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                                Service.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                                Service.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                                Service.ICTicket.FSRNumber = Convert.ToString(dr["FSRNumber"]);
                                Service.ICTicket.FSRDate = dr["FSRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FSRDate"]);//Convert.ToString(dr["FSRDate"]);

                                Service.ICTicket.Equipment = new PDMS_EquipmentHeader();
                                Service.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);

                                Service.ICTicket.Dealer = new PDMS_Dealer();
                                Service.ICTicket.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                                Service.ICTicket.Dealer.DealerName = Convert.ToString(dr["ContactName"]);
                                Service.ICTicket.Dealer.DealerBank = new PDealerBankDetails();
                                Service.ICTicket.Dealer.DealerBank.BankName = Convert.ToString(dr["BankName"]);
                                Service.ICTicket.Dealer.DealerBank.Branch = Convert.ToString(dr["Branch"]);
                                Service.ICTicket.Dealer.DealerBank.AcNumber = Convert.ToString(dr["AcNumber"]);
                                Service.ICTicket.Dealer.DealerBank.IfscCode = Convert.ToString(dr["IfscCode"]);

                                Service.ICTicket.Customer = new PDMS_Customer();
                                Service.ICTicket.Customer.CustomerID = Convert.ToInt64(dr["CustomerID"]);
                                Service.ICTicket.Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                                Service.ICTicket.Customer.CustomerName = Convert.ToString(dr["CustomerName"]);
                                Service.ICTicket.ScopeOfWork = Convert.ToString(dr["ScopeOfWork"]);
                                Service.ICTicket.Remarks = Convert.ToString(dr["Remarks"]);
                                Service.ICTicket.KindAttn = Convert.ToString(dr["KindAttn"]);
                                Service.ICTicket.NoOfDays = Convert.ToDecimal(dr["WorkedDay"]);
                                InvoiceID = Service.PaidServiceInvoiceID;
                                Service.IsDeletionAllowed = Convert.ToBoolean(dr["IsDeletionAllowedProforma"]);
                                Service.InvoiceItems = new List<PDMS_PaidServiceInvoiceItem>();
                            }
                            Service.InvoiceItems.Add(new PDMS_PaidServiceInvoiceItem()
                            {
                                Material = new PDMS_Material()
                                {
                                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                    MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                    HSN = Convert.ToString(dr["HSNCode"])
                                },
                                Qty = Convert.ToInt32(dr["Qty"]),
                                Rate = Convert.ToDecimal(dr["Rate"]),
                                Discount = Convert.ToDecimal(dr["Discount"]),
                                TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                CGST = Convert.ToInt32(dr["CGST"]),
                                SGST = Convert.ToInt32(dr["SGST"]),
                                IGST = Convert.ToInt32(dr["IGST"]),
                                CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                                CessValue = dr["CessValue"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CessValue"])

                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Services;
        }
        public PAttachedFile ServiceProformaInvoicefile(long ServiceQuotationID)
        {
            try
            {
                PDMS_PaidServiceInvoice PaidServiceInvoice = new BDMS_Service().GetPaidServiceProformaInvoice(ServiceQuotationID, null, "", null, null, null, "")[0];
                //PDMS_Customer Dealer = new SCustomer().getCustomerAddress(PaidServiceInvoice.ICTicket.Dealer.DealerCode);
                PDMS_Customer Dealer = new BDMS_Customer().getDealerAddressFromSAP(PaidServiceInvoice.ICTicket.Dealer.DealerCode);
                string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');


                //PDMS_Customer Customer = new SCustomer().getCustomerAddress(PaidServiceInvoice.ICTicket.Customer.CustomerCode);
                //PDMS_Customer Customer = new BDMS_Customer().getCustomerAddressFromSAP(PaidServiceInvoice.ICTicket.Customer.CustomerCode);
                PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(PaidServiceInvoice.ICTicket.Customer.CustomerID);
                string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');


                //if (string.IsNullOrEmpty(Customer.GSTIN))
                //{
                //    DataTable dt = new NpgsqlServer().ExecuteReader("select r_value as GSTIN from doohr_bp_statutory where r_statutory_type='GSTIN' and s_tenant_id=" + PaidServiceInvoice.ICTicket.Dealer.DealerCode + " and p_bp_id='" + PaidServiceInvoice.ICTicket.Customer.CustomerCode + "' limit 1");
                //    if (dt.Rows.Count == 1)
                //    {
                //        Customer.GSTIN = Convert.ToString(dt.Rows[0][0]);
                //    }
                //}

                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("Rate");
                CommissionDT.Columns.Add("Value", typeof(decimal));
                CommissionDT.Columns.Add("CGST");
                CommissionDT.Columns.Add("SGST");
                CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                //  decimal GrandTotal = 0;
                string StateCode = Dealer.State.StateCode;
                string GST_Header = "";
                int i = 0;
                decimal CessValue = 0;
                decimal CessSubTotal = 0;

                foreach (PDMS_PaidServiceInvoiceItem item in PaidServiceInvoice.InvoiceItems)
                {

                    i = i + 1;
                    if (item.SGST != 0)
                    {
                        GST_Header = "CGST & SGST";
                        CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, item.TaxableValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.TaxableValue + item.CGSTValue + item.SGSTValue);

                        CessValue = CessValue + item.CessValue;
                        CessSubTotal = item.TaxableValue + item.CGSTValue + item.SGSTValue + item.CessValue;
                    }
                    else
                    {
                        GST_Header = "IGST";
                        CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, item.TaxableValue, item.IGST, null, item.IGSTValue, null, item.TaxableValue + item.IGSTValue);

                        CessValue = CessValue + item.CessValue;
                        CessSubTotal = item.TaxableValue + item.IGSTValue + item.CessValue;
                    }
                }

                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;

                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;

                ReportParameter[] P = new ReportParameter[28];
                //   ViewState["Month"] = ddlMonth.SelectedValue;
                P[0] = new ReportParameter("DealerCode", PaidServiceInvoice.ICTicket.Dealer.DealerCode, false);
                P[1] = new ReportParameter("DealerName", PaidServiceInvoice.ICTicket.Dealer.DealerName, false);
                P[2] = new ReportParameter("Address1", DealerAddress1, false);
                P[3] = new ReportParameter("Address2", DealerAddress2, false);
                P[4] = new ReportParameter("Contact", "Contact", false);
                P[5] = new ReportParameter("GSTIN", Dealer.GSTIN, false);
                P[6] = new ReportParameter("GST_Header", GST_Header, false);
                P[7] = new ReportParameter("GrandTotal", (PaidServiceInvoice.GrandTotal).ToString(), false);
                P[8] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(PaidServiceInvoice.GrandTotal)), false);
                P[9] = new ReportParameter("InvoiceNumber", PaidServiceInvoice.ProformaInvoiceNumber, false);

                P[10] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                P[11] = new ReportParameter("CustomerName", PaidServiceInvoice.ICTicket.Customer.CustomerName, false);
                P[12] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                P[13] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                P[14] = new ReportParameter("CustomerMail", Customer.Email, false);
                P[15] = new ReportParameter("CustomerStateCode", Customer.GSTIN.Length > 2 ? Customer.GSTIN.Substring(0, 2) : "", false);
                P[16] = new ReportParameter("CustomerGST", Customer.GSTIN, false);
                P[17] = new ReportParameter("ICTicketNo", PaidServiceInvoice.ICTicket.ICTicketNumber, false);
                P[18] = new ReportParameter("KindAttn", PaidServiceInvoice.ICTicket.KindAttn, false);
                P[19] = new ReportParameter("YourRef", PaidServiceInvoice.ICTicket.FSRNumber + " " + PaidServiceInvoice.ICTicket.FSRDate, false);
                P[20] = new ReportParameter("OurRef", PaidServiceInvoice.ICTicket.ICTicketNumber + " " + PaidServiceInvoice.ICTicket.Equipment.EquipmentSerialNo, false);
                P[21] = new ReportParameter("Remarks", PaidServiceInvoice.ICTicket.Remarks, false);
                P[22] = new ReportParameter("NoOfDays", Convert.ToString(PaidServiceInvoice.ICTicket.NoOfDays), false);
                P[23] = new ReportParameter("ScopOfWork", PaidServiceInvoice.ICTicket.ScopeOfWork, false);
                P[24] = new ReportParameter("BankDetails", "Our Bank details are : A/C No " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.AcNumber + ", Bank : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.BankName + ", Branch : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.Branch + ", IFSC Code : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.IfscCode, false);
                P[25] = new ReportParameter("InvDate", PaidServiceInvoice.ProformaInvoiceDate.ToShortDateString(), false);
                P[26] = new ReportParameter("CessValue", Convert.ToString(CessValue), false);
                P[27] = new ReportParameter("CessSubTotal", Convert.ToString(CessSubTotal), false);

                report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_PaidServiceProformaInvoice.rdlc");
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);
                report.SetParameters(P);
                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                PAttachedFile InvF = new PAttachedFile();

                InvF.FileType = mimeType;
                InvF.AttachedFile = mybytes;
                InvF.AttachedFileID = 0;
                InvF.FileName = "Proforma Invoice " + PaidServiceInvoice.ProformaInvoiceNumber;
                return InvF;
            }
            catch (Exception ex)
            {

            }
            return null;
        } 
        public List<PDMS_PaidServiceHeader> GetPaidServiceReport(string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? DealerID, string CustomerCode, int? ServiceStatusID, int? ServiceTypeID)
        {
            List<PDMS_PaidServiceHeader> Services = new List<PDMS_PaidServiceHeader>();
            try
            {

                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);

                DbParameter DateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

                DbParameter CustomerIDP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter ServiceStatusIDP = provider.CreateParameter("ServiceStatusID", ServiceStatusID, DbType.Int32);
                DbParameter ServiceTypeIDP = provider.CreateParameter("ServiceTypeID", ServiceTypeID, DbType.Int32);
                DbParameter[] Params = new DbParameter[7] { ICTicketIDP, DateFP, DateTP, DealerIDP, CustomerIDP, ServiceStatusIDP, ServiceTypeIDP };

                PDMS_PaidServiceHeader Service = null;
                using (DataSet DataSet = provider.Select("ZDMS_GetPaidServiceReport", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Service = new PDMS_PaidServiceHeader();
                            Services.Add(Service);
                            Service.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            Service.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);

                            Service.Dealer = new PDMS_Dealer();
                            Service.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                            Service.Dealer.DealerName = Convert.ToString(dr["DealerName"]);

                            Service.Customer = new PDMS_Customer();
                            Service.Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                            Service.Customer.CustomerName = Convert.ToString(dr["CustomerName"]);
                            Service.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            Service.Model = Convert.ToString(dr["Model"]);
                            Service.ServiceType = new PDMS_ServiceType() { ServiceType = Convert.ToString(dr["ServiceType"]) };
                            Service.Technician = new PUser() { ContactName = Convert.ToString(dr["TechnicianName"]) };
                            Service.ServiceItem = new PDMS_PaidServiceItem();
                            if (!string.IsNullOrEmpty(Convert.ToString(dr["MaterialCode"])))
                            {
                                Service.ServiceItem.Material = Convert.ToString(dr["MaterialCode"]);
                                Service.ServiceItem.MaterialDesc = Convert.ToString(dr["MaterialDescription"]);
                                Service.ServiceItem.QuotationNumber = Convert.ToString(dr["QuotationNumber"]);
                                Service.ServiceItem.ProformaInvoiceNumber = Convert.ToString(dr["ProformaInvoiceNumber"]);
                                Service.ServiceItem.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                Service.ServiceItem.InvoiceDate = dr["InvoiceDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["InvoiceDate"]);
                                Service.ServiceItem.ValueBeforeTax = Convert.ToDecimal(dr["TaxableValue"]);
                                Service.ServiceItem.Tax = Convert.ToDecimal(dr["Tax"]);
                                Service.ServiceItem.Total = Service.ServiceItem.ValueBeforeTax + Service.ServiceItem.Tax;
                                Service.Peirod = Service.ICTicketDate.Year + "-" + new DateTime(2010, Service.ICTicketDate.Month, 1).ToString("MMM", CultureInfo.InvariantCulture);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Services;
        }

        public DataTable GetWarrantyMaterialAnalysis(int? DealerID, DateTime? DateF, DateTime? DateT, string Material, int UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter MaterialP = provider.CreateParameter("Material", string.IsNullOrEmpty(Material) ? null : Material, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[5] { DealerIDP, DateFP, DateTP, MaterialP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetWarrantyMaterialAnalysis", Params))
                {
                    if (DataSet != null)
                    {
                        dt = DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            {
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public void GetMaterialSourceDDL(DropDownList ddl, int? MaterialSourceID, string MaterialSource)
        {
            ddl.DataTextField = "MaterialSource";
            ddl.DataValueField = "MaterialSourceID";
            ddl.DataSource = GetMaterialSource(MaterialSourceID, MaterialSource);
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public List<PDMS_MaterialSource> GetMaterialSource(int? MaterialSourceID, string MaterialSource)
        {
            List<PDMS_MaterialSource> Category1s = new List<PDMS_MaterialSource>();
            try
            {
                DbParameter MaterialSourceP;
                DbParameter MaterialSourceIDP = provider.CreateParameter("MaterialSourceID", MaterialSourceID, DbType.Int32);
                if (!string.IsNullOrEmpty(MaterialSource))
                    MaterialSourceP = provider.CreateParameter("MaterialSource", MaterialSource, DbType.String);
                else
                    MaterialSourceP = provider.CreateParameter("MaterialSource", null, DbType.String);

                DbParameter[] Params = new DbParameter[2] { MaterialSourceIDP, MaterialSourceP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMaterialSource", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Category1s.Add(new PDMS_MaterialSource()
                            {
                                MaterialSourceID = Convert.ToInt32(dr["MaterialSourceID"]),
                                MaterialSource = Convert.ToString(dr["MaterialSource"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Category1s;
        }

        public Boolean UpdateICTicketMaterial(PDMS_ServiceMaterial ServiceMaterial, int UserID)
        {
            try
            {

                DbParameter ServiceMaterialID = provider.CreateParameter("ServiceMaterialID", ServiceMaterial.ServiceMaterialID, DbType.Int64);
                DbParameter MaterialSNP = provider.CreateParameter("MaterialSN", ServiceMaterial.Material.MaterialSerialNumber, DbType.String);
                DbParameter DefectiveMaterialSNP = provider.CreateParameter("DefectiveMaterialSN", ServiceMaterial.DefectiveMaterial.MaterialSerialNumber, DbType.String);
                DbParameter QtyP = provider.CreateParameter("Qty", ServiceMaterial.Qty, DbType.Decimal);
                DbParameter IsFaultyPartP = provider.CreateParameter("IsFaultyPart", ServiceMaterial.IsFaultyPart, DbType.Boolean);

                DbParameter IsRecomenedParts = provider.CreateParameter("IsRecomenedParts", ServiceMaterial.IsRecomenedParts, DbType.Boolean);
                DbParameter IsQuotationParts = provider.CreateParameter("IsQuotationParts", ServiceMaterial.IsQuotationParts, DbType.Boolean);
                DbParameter MaterialSourceID = provider.CreateParameter("MaterialSourceID", ServiceMaterial.MaterialSource == null ? (int?)null : ServiceMaterial.MaterialSource.MaterialSourceID, DbType.Int32);
                DbParameter TsirID = provider.CreateParameter("TsirID", ServiceMaterial.TSIR == null ? (long?)null : ServiceMaterial.TSIR.TsirID, DbType.Int64);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[10] { ServiceMaterialID, MaterialSNP, DefectiveMaterialSNP, QtyP, IsFaultyPartP, IsRecomenedParts, IsQuotationParts, MaterialSourceID, TsirID, UserIDP };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateICTicketMaterial", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Service", "UpdateICTicketMaterialTsirID", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", " UpdateICTicketMaterialTsirID", ex);
                return false;
            }
            return true;
        }

        public List<PDMS_ServiceMaterial> GetWarrantyPartsAvailabilityReport(int? DealerID, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, string Material, int UserID)
        {
            List<PDMS_ServiceMaterial> ServiceMaterials = new List<PDMS_ServiceMaterial>();
            try
            {
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter MaterialP = provider.CreateParameter("Material", string.IsNullOrEmpty(Material) ? null : Material, DbType.String);
              //  DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", string.IsNullOrEmpty(MachineSerialNumber) ? null : MachineSerialNumber, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[6] { ICTicketIDP, DealerIDP, DateFP, DateTP, MaterialP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetWarrantyPartsAvailabilityReport", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServiceMaterials.Add(new PDMS_ServiceMaterial()
                            {
                                ICTicket = new PDMS_ICTicket()
                                {
                                    ICTicketID = Convert.ToInt64(dr["ICTicketID"]),
                                    ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]),
                                    ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]),
                                    Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) },
                                    Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) },
                                    Equipment = new PDMS_EquipmentHeader() { EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["Model"]) } }
                                },
                                Material = new PDMS_Material()
                                {
                                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                    MaterialDescription = Convert.ToString(dr["MaterialDescription"])
                                },
                                Qty = Convert.ToInt32(dr["Qty"]),
                                AvailableQty = Convert.ToInt32(dr["AvailableQty"]),
                                QuotationNumber = Convert.ToString(dr["QuotationNumber"]),
                                QuotationDate = DBNull.Value == dr["QuotationDate"] ? (DateTime?)null : Convert.ToDateTime(dr["QuotationDate"]),
                                DeliveryNumber = Convert.ToString(dr["DeliveryNumber"]),
                                DeliveryDate = DBNull.Value == dr["DeliveryDate"] ? (DateTime?)null : Convert.ToDateTime(dr["DeliveryDate"]),
                                AvailablePercentage = Convert.ToInt32(dr["AvailablePercentage"]),


                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Service", "GetWarrantyPartsAvailabilityReport", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", "GetWarrantyPartsAvailabilityReport", ex);
            }
            return ServiceMaterials;
        }
        public int InsertOrUpdateMainApplication(long? MainApplicationID, string MainApplication, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            DbParameter MainApplicationIDP = provider.CreateParameter("MainApplicationID", MainApplicationID, DbType.Int32);
            DbParameter MainApplicationP = provider.CreateParameter("MainApplication", MainApplication, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { MainApplicationIDP, MainApplicationP, IsActiveP, UserIDP};
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateMainApplication", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Service", "ZDMS_InsertOrUpdateMainApplication", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", "ZDMS_InsertOrUpdateMainApplication", ex);
                throw ex;
            }
            return success;
        }
        public int InsertOrUpdateSubApplication(long? SubApplicationID, int MainApplicationID, string SubApplication, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            DbParameter SubApplicationIDP = provider.CreateParameter("SubApplicationID", SubApplicationID, DbType.Int32);
            DbParameter MainApplicationIDP = provider.CreateParameter("MainApplicationID", MainApplicationID, DbType.Int32);
            DbParameter SubApplicationP = provider.CreateParameter("SubApplication", SubApplication, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[5] { SubApplicationIDP,MainApplicationIDP, SubApplicationP, IsActiveP, UserIDP};
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateSubApplication", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Service", "ZDMS_InsertOrUpdateSubApplication", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Service", "ZDMS_InsertOrUpdateSubApplication", ex);
                throw ex;
            }
            return success;
        }
    }
}