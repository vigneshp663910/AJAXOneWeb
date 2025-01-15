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

namespace Business
{
    public class BDMS_ICTicketFSR
    {
        private IDataAccess provider;
        public BDMS_ICTicketFSR()
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
        public List<PDMS_ICTicketFSR> GetICTicketFSRManage(int? DealerID, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? StatusID, int? TechnicianID)
        {
            List<PDMS_ICTicketFSR> Ws = new List<PDMS_ICTicketFSR>();
            PDMS_ICTicketFSR W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

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

                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, StatusIDP, TechnicianIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketFSRManage", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicketFSR();
                            Ws.Add(W);
                            W.FsrID = Convert.ToInt64(dr["FsrID"]);
                            W.FSRNumber = Convert.ToString(dr["FSRNumber"]);
                            W.FSRDate = Convert.ToDateTime(dr["FSRDate"]);

                            W.ComplaintStatus = Convert.ToString(dr["ComplaintStatus"]);
                            //   W.PresentContactNumberA = Convert.ToString(dr["ContactNumberA"]);
                            W.Report = Convert.ToString(dr["Report"]);
                           
                            W.CustomerRemarks = Convert.ToString(dr["CustomerRemarks"]);
                            W.Designation = Convert.ToString(dr["Designation"]);
                            W.Complaint = Convert.ToString(dr["Complaint"]);

                            if (dr["ModeOfPaymentID"] != DBNull.Value)
                                W.ModeOfPayment = new PDMS_ModeOfPayment() { ModeOfPaymentID = Convert.ToInt32(dr["ModeOfPaymentID"]), ModeOfPayment = Convert.ToString(dr["ModeOfPayment"]) };

                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicket.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);

                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };


                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ICTicket.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ICTicket.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ICTicket.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            //     W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.ICTicket.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);
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
 
         
        public PDMS_ICTicketFSR GetICTicketFSRByFsrID(long? FsrID,long? ICTicketID, int? DealerID, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? StatusID)
        {
            PDMS_ICTicketFSR W = new PDMS_ICTicketFSR();
            try
            {
                DbParameter FsrIDP = provider.CreateParameter("FsrID", FsrID, DbType.Int64);
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

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


                DbParameter[] Params = new DbParameter[8] { FsrIDP,ICTicketIDP, DealerIDP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, StatusIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketFSRByFsrID", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            W.FsrID = Convert.ToInt64(dr["FsrID"]);
                            W.FSRNumber = Convert.ToString(dr["FSRNumber"]);
                            W.FSRDate = Convert.ToDateTime(dr["FSRDate"]);
                          
                            W.Complaint = Convert.ToString(dr["Complaint"]);
                            W.CustomerRemarks = Convert.ToString(dr["CustomerRemarks"]);
                            W.Designation = Convert.ToString(dr["Designation"]);
                            if (dr["ModeOfPaymentID"] != DBNull.Value)
                                W.ModeOfPayment = new PDMS_ModeOfPayment() { ModeOfPaymentID = Convert.ToInt32(dr["ModeOfPaymentID"]), ModeOfPayment = Convert.ToString(dr["ModeOfPayment"]) };
                            if (dr["MachineMaintenanceLevelID"] != DBNull.Value)
                                W.MachineMaintenanceLevel = new PDMS_MachineMaintenanceLevel() { MachineMaintenanceLevelID = Convert.ToInt32(dr["MachineMaintenanceLevelID"]), MachineMaintenanceLevel = Convert.ToString(dr["MachineMaintenanceLevel"]) };



                            W.ComplaintStatus = Convert.ToString(dr["ComplaintStatus"]);
                            // W.PresentContactNumberA = Convert.ToString(dr["ContactNumberA"]);
                            W.Report = Convert.ToString(dr["Report"]);


                            W.IsRental = DBNull.Value == dr["IsRental"] ? false : Convert.ToBoolean(dr["IsRental"]);

                            W.RentalName = Convert.ToString(dr["RentalName"]);
                            W.RentalNumber = Convert.ToString(dr["RentalNumber"]);
                            W.OperatorName = Convert.ToString(dr["OperatorName"]);
                            W.OperatorNumber = Convert.ToString(dr["OperatorNumber"]);

                            W.NatureOfComplaint = Convert.ToString(dr["NatureOfComplaint"]);
                            W.Observation = Convert.ToString(dr["Observation"]);
                            W.WorkCarriedOut = Convert.ToString(dr["WorkCarriedOut"]);
                            W.SERecommendedParts = Convert.ToString(dr["SERecommendedParts"]);
                            W.MachineBeforeAttachedFile = dr["MachineBeforeAttachedFileID"] == DBNull.Value ? null : new PDMS_MachineAttachedFile()
                            {
                                AttachedFileID = Convert.ToInt32(dr["MachineBeforeAttachedFileID"]),
                                FileName = Convert.ToString(dr["MBAttachedFileName"]),
                                AttachedFile = (Byte[])(dr["MBAttachedFile"]),
                                FileType = Convert.ToString(dr["MBContentType"]),
                                FileSize = Convert.ToInt32(dr["MBFileSize"])
                            };
                            W.MachineAfterAttachedFile = dr["MachineAfterAttachedFileID"] == DBNull.Value ? null : new PDMS_MachineAttachedFile()
                            {
                                AttachedFileID = Convert.ToInt32(dr["MachineAfterAttachedFileID"]),
                                FileName = Convert.ToString(dr["MAAttachedFileName"]),
                                AttachedFile = (Byte[])(dr["MAAttachedFile"]),
                                FileType = Convert.ToString(dr["MAContentType"]),
                                FileSize = Convert.ToInt32(dr["MAFileSize"])
                            };

                            W.ICTicket = new PDMS_ICTicket();

                            W.ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(Convert.ToInt64(dr["ICTicketID"]));

                            //W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            //W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            //W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            //W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            //W.ICTicket.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            //W.ICTicket.ReachedDate = dr["ReachedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ReachedDate"]);
                            //W.ICTicket.RestoreDate = dr["RestoreDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]); 
                            //W.ICTicket.SiteContactPersonName = Convert.ToString(dr["SiteContactPersonName"]);
                            //W.ICTicket.SiteContactPersonNumber = Convert.ToString(dr["SiteContactPersonNumber"]);
                            //W.ICTicket.Equipment = new PDMS_EquipmentHeader() { EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]) };
                            //W.ICTicket.CurrentHMRValue = Convert.ToInt32(dr["CurrentHMRValue"]);                           
                            //W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };
                            //W.ICTicket.Equipment.EquipmentModel.Division = new PDMS_Division() { UOM = Convert.ToString(dr["UOM"]) };
                            //W.ICTicket.CustomerSatisfactionLevel = new PDMS_CustomerSatisfactionLevel() { CustomerSatisfactionLevel = Convert.ToString(dr["CustomerSatisfactionLevel"]) };


                            //if (dr["ServiceTypeID"] != DBNull.Value)
                            //{
                            //    W.ICTicket.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            //}
                            //if (dr["ServicePriorityID"] != DBNull.Value)
                            //{
                            //    W.ICTicket.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            //}

                            //W.ICTicket.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            //if (dr["ServiceStatusID"] != DBNull.Value)
                            //{
                            //    W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            //}
                            ////     W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            //W.ICTicket.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);
                            //if (dr["CustomerSatisfactionLevelID"] != DBNull.Value)
                            //    W.ICTicket.CustomerSatisfactionLevel = new PDMS_CustomerSatisfactionLevel() { CustomerSatisfactionLevelID = Convert.ToInt32(dr["CustomerSatisfactionLevelID"]), CustomerSatisfactionLevel = Convert.ToString(dr["CustomerSatisfactionLevel"]) };

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
        public PDMS_ICTicketFSRSignature GetICTicketFSRSignatureByFsrID(long FsrID )
        {
            PDMS_ICTicketFSRSignature W = new PDMS_ICTicketFSRSignature();
            try
            {
                DbParameter FsrIDP = provider.CreateParameter("FsrID", FsrID, DbType.Int64); 

                DbParameter[] Params = new DbParameter[1] { FsrIDP};
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketFSRSignatureByFsrID", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W.FSRSignatureID = Convert.ToInt64(dr["FSRSignatureID"]);
                            W.FsrID = Convert.ToInt64(dr["FsrID"]);

                            W.TPhoto = Convert.ToString(dr["TPhoto"]);
                            W.TSignature = Convert.ToString(dr["TSignature"]);
                            W.TName = Convert.ToString(dr["TName"]);

                            W.CPhoto = Convert.ToString(dr["CPhoto"]);
                            W.CSignature = Convert.ToString(dr["CSignature"]);
                            W.CName = Convert.ToString(dr["CName"]);
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
        
        public long InsertOrUpdateICTicketFSRSignature(long FsrID, string Photo, int UserID, Int32 ImageType, Boolean IsDeleted, string Name)
        {
            long success = 0; 
            DbParameter FsrIDP = provider.CreateParameter("FsrID", FsrID, DbType.Int64);
            DbParameter PhotoP = provider.CreateParameter("Image", Photo, DbType.String);
          //  DbParameter SignatureP = provider.CreateParameter("Signature", Signature, DbType.String); 
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter ImageTypeP = provider.CreateParameter("ImageType", ImageType, DbType.Int32);
            DbParameter IsDeleteP = provider.CreateParameter("IsDeleted", IsDeleted, DbType.Boolean);
            DbParameter NameP = provider.CreateParameter("Name", Name, DbType.String);
            DbParameter[] Params = new DbParameter[6] { FsrIDP, PhotoP, UserIDP, ImageTypeP, IsDeleteP, NameP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateICTicketFSRSignature", Params);
                    scope.Complete();
                    success = 1;
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicketFSR", "InsertOrUpdateICTicketFSRSignature", sqlEx);
                return 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicketFSR", " InsertOrUpdateICTicketFSRSignature", ex);
                return 0;
            }
            return success;
        }

        public List<PDMS_ServiceMaterial> GetMaterialsRecommendedBySE(long? ICTicketID, long? MaterialID, string Material, Boolean? IsDeleted)
        {
            List<PDMS_ServiceMaterial> ServiceMaterials = new List<PDMS_ServiceMaterial>();
            try
            {


                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter MaterialIDP = provider.CreateParameter("MaterialID", MaterialID, DbType.Int64);

                DbParameter MaterialP;
                if (!string.IsNullOrEmpty(Material))
                    MaterialP = provider.CreateParameter("Material", Material, DbType.String);
                else
                    MaterialP = provider.CreateParameter("Material", null, DbType.String);
                DbParameter IsDeletedP = provider.CreateParameter("IsDeleted", IsDeleted, DbType.Boolean);
                DbParameter[] Params = new DbParameter[4] { ICTicketIDP, MaterialIDP, MaterialP, IsDeletedP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMaterialsRecommendedBySE", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServiceMaterials.Add(new PDMS_ServiceMaterial()
                            {
                                ServiceMaterialID = Convert.ToInt64(dr["MaterialID"]),
                                ICTicketID = Convert.ToInt64(dr["ICTicketID"]),
                                Item = Convert.ToInt32(dr["Item"]),
                                Material = new PDMS_Material()
                                {
                                    MaterialID = Convert.ToInt32(dr["MaterialID"]),
                                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                    MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                    MaterialSerialNumber = Convert.ToString(dr["MaterialSN"]),
                                    MaterialGroup = Convert.ToString(dr["MaterialGroup"])
                                },
                                Qty = Convert.ToInt32(dr["Qty"])
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

        public List<PDMS_FSRAttachedFile> GetICTicketFSRAttachedFileDetails(long? ICTicketID, long? AttachedFileID)
        {
            List<PDMS_FSRAttachedFile> D8 = new List<PDMS_FSRAttachedFile>();
            try
            {
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
                DbParameter[] Params = new DbParameter[2] { ICTicketIDP, AttachedFileIDP };
                using (DataSet DS = provider.Select("ZDMS_GetICTicketFSRAttachedFileDetails", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            D8.Add(new PDMS_FSRAttachedFile()
                            {
                                AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]), 
                                FileName = Convert.ToString(dr["FileName"]), 
                                FSRAttachedName = new PDMS_FSRAttachedName()
                                {
                                    FSRAttachedFileNameID = Convert.ToInt32(dr["FSRAttachedFileNameID"]),
                                    FSRAttachedName = Convert.ToString(dr["FSRAttachedName"])
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
        public PDMS_FSRAttachedFile GetICTicketFSRAttachedFileByID(long AttachedFileID)
        {
            PDMS_FSRAttachedFile FSR_File = new PDMS_FSRAttachedFile();
            try
            {
                DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { AttachedFileIDP };
                using (DataSet DS = provider.Select("ZDMS_GetICTicketFSRAttachedFileByID", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            FSR_File.AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]);
                            //FSR_File.AttachedFile = (Byte[])(dr["AttachedFile"]);
                            FSR_File.FileType = Convert.ToString(dr["ContentType"]);
                            FSR_File.FileName = Convert.ToString(dr["FileName"]);
                            FSR_File.FileSize = Convert.ToInt32(dr["FileSize"]);
                            FSR_File.FSRAttachedName = new PDMS_FSRAttachedName()
                            {
                                FSRAttachedFileNameID = Convert.ToInt32(dr["FSRAttachedFileNameID"]),
                                FSRAttachedName = Convert.ToString(dr["FSRAttachedName"])
                            };
                        }
                    }
                }
                return FSR_File;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "GetICTicketAttachedFile", ex);
                return null;
            }
        }

        public PAttachedFile GetICTicketFSRAttachedFileForDownload(long AttachedFileID)
        {
            string endPoint = "ICTicketFSR/AttachmentsForDownload?AttachedFileID=" + AttachedFileID ;
           // return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
            return JsonConvert.DeserializeObject<PAttachedFile>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));


        }

        public Boolean InsertOrUpdateICTicketFSRAttachedFileAddOrRemove(PDMS_FSRAttachedFile AttachedFile, int UserID)
        {
            int success = 0;
            // PDMS_ServiceMaterial MM = new SMaterial().getMaterialTax(Customer, Vendor, OrderType, 1, MaterialService, 1);
            // new SCustomer().getCustomerAddress(Customer);
            DbParameter AttachedFileID = provider.CreateParameter("AttachedFileID", AttachedFile.AttachedFileID, DbType.Int64);
            DbParameter ICTicketID = provider.CreateParameter("ICTicketID", AttachedFile.ICTicket == null ? 0 : AttachedFile.ICTicket.ICTicketID, DbType.Int64);
            DbParameter FSRAttachedFileNameID = provider.CreateParameter("FSRAttachedFileNameID",AttachedFile.FSRAttachedName==null?0: AttachedFile.FSRAttachedName.FSRAttachedFileNameID, DbType.Int32);
            DbParameter AttachedFileP = provider.CreateParameter("AttachedFile", AttachedFile.AttachedFile, DbType.Binary);
            DbParameter FileType = provider.CreateParameter("ContentType", AttachedFile.FileType, DbType.String);
            DbParameter FileName = provider.CreateParameter("FileName", AttachedFile.FileName, DbType.String);
            DbParameter FileSize = provider.CreateParameter("FileSize", AttachedFile.FileSize, DbType.Int32);
            DbParameter IsDeleted = provider.CreateParameter("IsDeleted", AttachedFile.IsDeleted, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[9] { AttachedFileID, ICTicketID, FSRAttachedFileNameID, AttachedFileP, FileType, FileName, FileSize, IsDeleted, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateICTicketFSRAttachedFileAddOrRemove", Params);
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
        public List<PDMS_FSRAttachedName> GetFSRAttachedFileName(int? FSRAttachedFileNameID, string FSRAttachedName, Boolean? IsFSR, Boolean? IsTSIR)
        {
            List<PDMS_FSRAttachedName> FSRAttached = new List<PDMS_FSRAttachedName>();
            try
            {

                DbParameter FSRAttachedFileNameIDP = provider.CreateParameter("FSRAttachedFileNameID", FSRAttachedFileNameID, DbType.Int32);
                DbParameter FSRAttachedNameP = provider.CreateParameter("FSRAttachedName",string.IsNullOrEmpty(FSRAttachedName)?null: FSRAttachedName, DbType.String);
                DbParameter IsFSRP = provider.CreateParameter("IsFSR", IsFSR, DbType.Boolean);
                DbParameter IsTSIRP = provider.CreateParameter("IsTSIR", IsTSIR, DbType.Boolean);

                DbParameter[] Params = new DbParameter[4] { FSRAttachedFileNameIDP, FSRAttachedNameP, IsFSRP, IsTSIRP };
                using (DataSet DataSet = provider.Select("ZDMS_GetFSRAttachedFileName", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            FSRAttached.Add(new PDMS_FSRAttachedName()
                            {
                                FSRAttachedFileNameID = Convert.ToInt32(dr["FSRAttachedFileNameID"]),
                                FSRAttachedName = Convert.ToString(dr["FSRAttachedName"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return FSRAttached;
        }

        public PICTicketFSRSignature GetICTicketFSRSignatureByFsrIDDownload(long FsrID)
        {
            string endPoint = "ICTicketFSR/TicketFSRSignature?FsrID=" + FsrID; 
            return JsonConvert.DeserializeObject<PICTicketFSRSignature>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }
        public void ICTicket_Directorys(string Path)
        {

            if (!Directory.Exists(Path + "/ICTickrtFSR_Files/Org"))
            {
                Directory.CreateDirectory(Path + "/ICTickrtFSR_Files/Org");
            }
            if (!Directory.Exists(Path + "/ICTickrtTSIR_Files/Org"))
            {
                Directory.CreateDirectory(Path + "/ICTickrtTSIR_Files/Org");
            }
            if (!Directory.Exists(Path + "/ICTickrtFSR_Files/Signature"))
            {
                Directory.CreateDirectory(Path + "/ICTickrtFSR_Files/Signature");
            }
        }
    }
}
