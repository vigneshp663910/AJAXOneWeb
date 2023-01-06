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

        public long InsertOrUpdateICTicketFSR(long? FsrID, long ICTicketID, Boolean IsWarranty, DateTime? DepartureDate, DateTime? ReachedDate, string Location
            , int? ServiceTypeID, int? DealerOffice, int? ServicePriorityID, DateTime? CurrentHMRDate, int? CurrentHMRValue, DateTime? RestoreDate, DateTime? ArrivalBack, Boolean IsRental
            , int? Category1ID, int? Category2ID, int? Category3ID, int? Category4ID, int? Category5ID
            , string SiteContactPersonName, string SiteContactPersonNumber, string Designation, string OperatorName, string OperatorNumber, string RentalName
            , int? MachineMaintenanceLevelID, string ComplaintStatus, string CustomerRemarks, int? ModeOfPaymentID, int? MainApplicationID
            , int? SubApplicationID, int? CustomerSatisfactionLevelID, string Complaint, string Report, string RentalNumber
            , string NatureOfComplaint, string Observation, string WorkCarriedOut, string Advice
            , PDMS_MachineAttachedFile MachineBeforeFile, PDMS_MachineAttachedFile MachineAfterFile
            , int UserID)
        {
            int success = 0;
            long ClaimDebitID = 0;

            DbParameter FsrIDP = provider.CreateParameter("FsrID", FsrID, DbType.Int64);
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int32);
            DbParameter IsWarrantyP = provider.CreateParameter("IsWarranty", IsWarranty, DbType.Boolean);

            DbParameter DepartureDateP = provider.CreateParameter("DepartureDate", DepartureDate, DbType.DateTime);
            DbParameter ReachedDateP = provider.CreateParameter("ReachedDate", ReachedDate, DbType.DateTime);
            DbParameter LocationP = provider.CreateParameter("Location", Location, DbType.String);
            DbParameter ServiceTypeIDP = provider.CreateParameter("ServiceTypeID", ServiceTypeID, DbType.Int32);
            DbParameter DealerOfficeP = provider.CreateParameter("DealerOffice", DealerOffice, DbType.Int32);
            DbParameter ServicePriorityIDP = provider.CreateParameter("ServicePriorityID", ServicePriorityID, DbType.Int32);
            DbParameter CurrentHMRValueP = provider.CreateParameter("CurrentHMRValue", CurrentHMRValue, DbType.Int32);
            DbParameter CurrentHMRDateP = provider.CreateParameter("CurrentHMRDate", CurrentHMRDate, DbType.DateTime);
            DbParameter RestoreDateP = provider.CreateParameter("RestoreDate", RestoreDate, DbType.DateTime);
            DbParameter ArrivalBackP = provider.CreateParameter("ArrivalBack", ArrivalBack, DbType.DateTime);
            DbParameter IsRentalP = provider.CreateParameter("IsRental", IsRental, DbType.Boolean);

            DbParameter Category1P = provider.CreateParameter("Category1ID", Category1ID, DbType.Int32);
            DbParameter Category2P = provider.CreateParameter("Category2ID", Category2ID, DbType.Int32);
            DbParameter Category3P = provider.CreateParameter("Category3ID", Category3ID, DbType.Int32);
            DbParameter Category4P = provider.CreateParameter("Category4ID", Category4ID, DbType.Int32);
            DbParameter Category5P = provider.CreateParameter("Category5ID", Category5ID, DbType.Int32);
            DbParameter SiteContactPersonNameP = provider.CreateParameter("SiteContactPersonName", SiteContactPersonName, DbType.String);
            DbParameter SiteContactPersonNumberP = provider.CreateParameter("SiteContactPersonNumber", SiteContactPersonNumber, DbType.String);
            DbParameter DesignationP = provider.CreateParameter("Designation", Designation, DbType.String);
            DbParameter OperatorNameP = provider.CreateParameter("OperatorName", OperatorName, DbType.String);
            DbParameter OperatorNumberP = provider.CreateParameter("OperatorNumber", OperatorNumber, DbType.String);
            DbParameter RentalNameP = provider.CreateParameter("RentalName", RentalName, DbType.String);


            DbParameter MachineMaintenanceLevelIDP = provider.CreateParameter("MachineMaintenanceLevelID", MachineMaintenanceLevelID, DbType.Int32);
            DbParameter ComplaintStatusP = provider.CreateParameter("ComplaintStatus", ComplaintStatus, DbType.String);
            DbParameter CustomerRemarksP = provider.CreateParameter("CustomerRemarks", CustomerRemarks, DbType.String);
            DbParameter ModeOfPaymentIDP = provider.CreateParameter("ModeOfPaymentID", ModeOfPaymentID, DbType.Int32);
            DbParameter MainApplicationIDP = provider.CreateParameter("MainApplicationID", MainApplicationID, DbType.Int32);
            DbParameter SubApplicationIDP = provider.CreateParameter("SubApplicationID", SubApplicationID, DbType.Int32);
            DbParameter CustomerSatisfactionLevelIDP = provider.CreateParameter("CustomerSatisfactionLevelID", CustomerSatisfactionLevelID, DbType.Int32);
            DbParameter ComplaintP = provider.CreateParameter("Complaint", Complaint, DbType.String);
            DbParameter ReportP = provider.CreateParameter("Report", Report, DbType.String);
            DbParameter RentalNumberP = provider.CreateParameter("RentalNumber", RentalNumber, DbType.String);

            //DbParameter ContactNumberAP = provider.CreateParameter("ContactNumberA", PresentContactNumberA, DbType.String);           

            DbParameter NatureOfComplaintP = provider.CreateParameter("NatureOfComplaint", NatureOfComplaint, DbType.String);
            DbParameter ObservationP = provider.CreateParameter("Observation", Observation, DbType.String);
            DbParameter WorkCarriedOutP = provider.CreateParameter("WorkCarriedOut", WorkCarriedOut, DbType.String);
            DbParameter AdviceP = provider.CreateParameter("Advice", Advice, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);


            DbParameter MBAttachedFileID = provider.CreateParameter("MBAttachedFileID", MachineBeforeFile.FileSize == 0 ? (Int64?)null : MachineBeforeFile.AttachedFileID, DbType.Int64);
            DbParameter MBAttachedFile;
            if (MachineBeforeFile.FileSize != 0)
            {
                MBAttachedFile = provider.CreateParameter("MBAttachedFile", MachineBeforeFile.AttachedFile, DbType.Binary);
            }
            else
            {
                MBAttachedFile = provider.CreateParameter("MBAttachedFile", DBNull.Value, DbType.Binary);
            }
            DbParameter MBContentType = provider.CreateParameter("MBContentType", MachineBeforeFile.FileSize == 0 ? null : MachineBeforeFile.FileType, DbType.String);
            DbParameter MBFileName = provider.CreateParameter("MBFileName", MachineBeforeFile.FileSize == 0 ? null : MachineBeforeFile.FileName, DbType.String);
            DbParameter MBFileSize = provider.CreateParameter("MBFileSize", MachineBeforeFile.FileSize == 0 ? (Int64?)null : MachineBeforeFile.FileSize, DbType.Int64);
            DbParameter MBIsDeleted = provider.CreateParameter("MBIsDeleted", MachineBeforeFile.FileSize == 0 ? (Boolean?)null : MachineBeforeFile.IsDeleted, DbType.Boolean);

            DbParameter MAAttachedFileID = provider.CreateParameter("MAAttachedFileID", MachineAfterFile.FileSize == 0 ? (Int64?)null : MachineAfterFile.AttachedFileID, DbType.Int64);
            DbParameter MAAttachedFile;
            if (MachineBeforeFile.FileSize != 0)
            {
                MAAttachedFile = provider.CreateParameter("MAAttachedFile", MachineAfterFile.AttachedFile, DbType.Binary);
            }
            else
            {
                MAAttachedFile = provider.CreateParameter("MAAttachedFile", DBNull.Value, DbType.Binary);
            }

            DbParameter MAContentType = provider.CreateParameter("MAContentType", MachineAfterFile.FileSize == 0 ? null : MachineAfterFile.FileType, DbType.String);
            DbParameter MAFileName = provider.CreateParameter("MAFileName", MachineAfterFile.FileSize == 0 ? null : MachineAfterFile.FileName, DbType.String);
            DbParameter MAFileSize = provider.CreateParameter("MAFileSize", MachineAfterFile.FileSize == 0 ? (Int64?)null : MachineAfterFile.FileSize, DbType.Int64);
            DbParameter MAIsDeleted = provider.CreateParameter("MAIsDeleted", MachineAfterFile.FileSize == 0 ? (Boolean?)null : MachineAfterFile.IsDeleted, DbType.Boolean);


            DbParameter WarrantyIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[52] { FsrIDP, ICTicketIDP,IsWarrantyP,DepartureDateP, ReachedDateP, LocationP, ServiceTypeIDP, DealerOfficeP, ServicePriorityIDP, CurrentHMRValueP, CurrentHMRDateP, RestoreDateP, ArrivalBackP, IsRentalP
                 ,Category1P,Category2P,Category3P,Category4P,SiteContactPersonNameP,SiteContactPersonNumberP,DesignationP,OperatorNameP,OperatorNumberP,RentalNameP
                 ,MachineMaintenanceLevelIDP,ComplaintStatusP,CustomerRemarksP,ModeOfPaymentIDP,MainApplicationIDP,SubApplicationIDP,CustomerSatisfactionLevelIDP,ComplaintP,ReportP,RentalNumberP, UserIDP, WarrantyIDP
             ,NatureOfComplaintP,ObservationP,WorkCarriedOutP,AdviceP
             ,MBAttachedFileID,MBAttachedFile,MBContentType,MBFileName,MBFileSize,MBIsDeleted,MAAttachedFileID,MAAttachedFile,MAContentType,MAFileName,MAFileSize,MAIsDeleted};
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateICTicketFSR", Params);
                    ClaimDebitID = Convert.ToInt64(WarrantyIDP.Value);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicketFSR", "InsertOrUpdateICTicketFSR", sqlEx);
                return 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicketFSR", " InsertOrUpdateICTicketFSR", ex);
                return 0;
            }
            return ClaimDebitID;
        }

        public long InsertOrUpdateICTicketFSR_New(long? FsrID, long ICTicketID, Boolean IsRental
            , string OperatorName, string OperatorNumber, string RentalName, int? MachineMaintenanceLevelID,    int? ModeOfPaymentID
         , string Report, string RentalNumber, string NatureOfComplaint, string Observation, string WorkCarriedOut, string SERecommendedParts
           ,   int UserID)
        {
            int success = 0;
            long ClaimDebitID = 0;

            DbParameter FsrIDP = provider.CreateParameter("FsrID", FsrID, DbType.Int64);
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int32);

            
           
            DbParameter IsRentalP = provider.CreateParameter("IsRental", IsRental, DbType.Boolean);

           
            DbParameter OperatorNameP = provider.CreateParameter("OperatorName", OperatorName, DbType.String);
            DbParameter OperatorNumberP = provider.CreateParameter("OperatorNumber", OperatorNumber, DbType.String);
            DbParameter RentalNameP = provider.CreateParameter("RentalName", RentalName, DbType.String);


            DbParameter MachineMaintenanceLevelIDP = provider.CreateParameter("MachineMaintenanceLevelID", MachineMaintenanceLevelID, DbType.Int32);
          //  DbParameter ComplaintStatusP = provider.CreateParameter("ComplaintStatus", ComplaintStatus, DbType.String);
          //  DbParameter CustomerRemarksP = provider.CreateParameter("CustomerRemarks", CustomerRemarks, DbType.String);
            DbParameter ModeOfPaymentIDP = provider.CreateParameter("ModeOfPaymentID", ModeOfPaymentID, DbType.Int32);
          //  DbParameter ComplaintP = provider.CreateParameter("Complaint", Complaint, DbType.String);
            DbParameter ReportP = provider.CreateParameter("Report", Report, DbType.String);
            DbParameter RentalNumberP = provider.CreateParameter("RentalNumber", RentalNumber, DbType.String);

            //DbParameter ContactNumberAP = provider.CreateParameter("ContactNumberA", PresentContactNumberA, DbType.String);           

            DbParameter NatureOfComplaintP = provider.CreateParameter("NatureOfComplaint", NatureOfComplaint, DbType.String);
            DbParameter ObservationP = provider.CreateParameter("Observation", Observation, DbType.String);
            DbParameter WorkCarriedOutP = provider.CreateParameter("WorkCarriedOut", WorkCarriedOut, DbType.String);
            DbParameter AdviceP = provider.CreateParameter("SERecommendedParts", SERecommendedParts, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

           // DbParameter MBAttachedFileID = provider.CreateParameter("MBAttachedFileID", MachineBeforeFile.AttachedFileID == 0 ? (Int64?)null : MachineBeforeFile.AttachedFileID, DbType.Int64);


            //   DbParameter MBAttachedFileID = provider.CreateParameter("MBAttachedFileID", MachineBeforeFile.FileSize == 0 ? (Int64?)null : MachineBeforeFile.AttachedFileID, DbType.Int64);
            //DbParameter MBAttachedFile;
            //if (MachineBeforeFile.AttachedFileID != 0)
            //{
            //    MBAttachedFile = provider.CreateParameter("MBAttachedFile", DBNull.Value, DbType.Binary);
            //}
            //else if (MachineBeforeFile.FileSize != 0)
            //{

            //    MBAttachedFile = provider.CreateParameter("MBAttachedFile", MachineBeforeFile.AttachedFile, DbType.Binary);
            //}
            //else
            //{
            //    MBAttachedFile = provider.CreateParameter("MBAttachedFile", DBNull.Value, DbType.Binary);
            //}
            //DbParameter MBContentType = provider.CreateParameter("MBContentType", MachineBeforeFile.FileSize == 0 ? null : MachineBeforeFile.FileType, DbType.String);
            //DbParameter MBFileName = provider.CreateParameter("MBFileName", MachineBeforeFile.FileSize == 0 ? null : MachineBeforeFile.FileName, DbType.String);
            //DbParameter MBFileSize = provider.CreateParameter("MBFileSize", MachineBeforeFile.FileSize == 0 ? (Int64?)null : MachineBeforeFile.FileSize, DbType.Int64);
            //DbParameter MBIsDeleted = provider.CreateParameter("MBIsDeleted", MachineBeforeFile.FileSize == 0 ? (Boolean?)null : MachineBeforeFile.IsDeleted, DbType.Boolean);

            //DbParameter MAAttachedFileID = provider.CreateParameter("MAAttachedFileID", MachineAfterFile.AttachedFileID == 0 ? (Int64?)null : MachineAfterFile.AttachedFileID, DbType.Int64);

            // DbParameter MAAttachedFile;
            //if (MachineAfterFile.AttachedFileID != 0)
            //{
            //    MAAttachedFile = provider.CreateParameter("MAAttachedFile", DBNull.Value, DbType.Binary);
            //}
            //else if (MachineBeforeFile.FileSize != 0)
            //{
            //    MAAttachedFile = provider.CreateParameter("MAAttachedFile", MachineAfterFile.AttachedFile, DbType.Binary);
            //}
            //else
            //{
            //    MAAttachedFile = provider.CreateParameter("MAAttachedFile", DBNull.Value, DbType.Binary);
            //}

            //DbParameter MAContentType = provider.CreateParameter("MAContentType", MachineAfterFile.FileSize == 0 ? null : MachineAfterFile.FileType, DbType.String);
            //DbParameter MAFileName = provider.CreateParameter("MAFileName", MachineAfterFile.FileSize == 0 ? null : MachineAfterFile.FileName, DbType.String);
            //DbParameter MAFileSize = provider.CreateParameter("MAFileSize", MachineAfterFile.FileSize == 0 ? (Int64?)null : MachineAfterFile.FileSize, DbType.Int64);
            //DbParameter MAIsDeleted = provider.CreateParameter("MAIsDeleted", MachineAfterFile.FileSize == 0 ? (Boolean?)null : MachineAfterFile.IsDeleted, DbType.Boolean);


            DbParameter WarrantyIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[16] { FsrIDP, ICTicketIDP,  IsRentalP
                 ,OperatorNameP,OperatorNumberP,RentalNameP
                 ,MachineMaintenanceLevelIDP,ModeOfPaymentIDP,ReportP,RentalNumberP, UserIDP, WarrantyIDP
             ,NatureOfComplaintP,ObservationP,WorkCarriedOutP,AdviceP
            // ,MBAttachedFileID,MBAttachedFile,MBContentType,MBFileName,MBFileSize,MBIsDeleted,MAAttachedFileID,MAAttachedFile,MAContentType,MAFileName,MAFileSize,MAIsDeleted
            };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateICTicketFSR", Params);
                    ClaimDebitID = Convert.ToInt64(WarrantyIDP.Value);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicketFSR", "InsertOrUpdateICTicketFSR", sqlEx);
                return 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicketFSR", " InsertOrUpdateICTicketFSR", ex);
                return 0;
            }
            return ClaimDebitID;
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
       
        public long InsertOrUpdateMaterialRecommendedBySEICTicketFSR(long ServiceMaterialID, long ICTicketID, string Material, decimal Qty, Boolean IsDeleted, int UserID)
        {
            long success = 0;
            long ID = 0;
            DbParameter ServiceMaterialIDP = provider.CreateParameter("ServiceMaterialID", ServiceMaterialID, DbType.Int64);
            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicketID, DbType.Int64);
            DbParameter MaterialP = provider.CreateParameter("Material", Material, DbType.String);
            DbParameter QtyP = provider.CreateParameter("Qty", Qty, DbType.Decimal);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter IsDeleteP = provider.CreateParameter("IsDeleted", IsDeleted, DbType.Boolean);
            DbParameter IDP = provider.CreateParameter("OutValue", ID, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[7] { ServiceMaterialIDP, ICTicketP, MaterialP, QtyP, UserIDP, IsDeleteP, IDP };
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
                new FileLogger().LogMessage("BDMS_ICTicketFSR", "InsertOrUpdateMaterialAddOrRemoveICTicket", sqlEx);
                return 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicketFSR", " InsertOrUpdateMaterialAddOrRemoveICTicket", ex);
                return 0;
            }
            return success;
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
                                    MaterialID = Convert.ToInt64(dr["MaterialID"]),
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
                            FSR_File.AttachedFile = (Byte[])(dr["AttachedFile"]);
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
    }
}
