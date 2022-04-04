using DataAccess;
using Properties;
using SapIntegration;
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
    public class BDMS_Dealer
    {
        private IDataAccess provider;
        public BDMS_Dealer()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PDMS_Dealer> GetDealer(int? DealerID, string DealerCode)
        {
            List<PDMS_Dealer> Dealers = new List<PDMS_Dealer>();
            PDMS_Dealer Dealer = null;
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter[] Params = new DbParameter[2] { DealerIDP, DealerCodeP };
            try
            {
                using (DataSet DS = provider.Select("ZDMS_GetDealer", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow Dr in DS.Tables[0].Rows)
                        {
                            Dealer = new PDMS_Dealer();
                            Dealer.DealerID = Convert.ToInt32(Dr["DealerID"]);
                            Dealer.DealerCode = Convert.ToString(Dr["DealerCode"]);
                            Dealer.DealerName = Convert.ToString(Dr["DealerName"]);

                            Dealer.AuthorityName = Convert.ToString(Dr["AuthorityName"]);
                            Dealer.AuthorityDesignation = Convert.ToString(Dr["AuthorityDesignation"]);
                            Dealer.AuthorityMobile = Convert.ToString(Dr["AuthorityMobile"]);

                            Dealer.IsEInvoice = Dr["EInvoiceDate"] == DBNull.Value ? false : Convert.ToBoolean(Dr["IsEInvoice"]);
                            Dealer.EInvoiceDate = Dr["EInvoiceDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dr["EInvoiceDate"]);
                            Dealer.EInvoiceFTPPath = Convert.ToString(Dr["EInvoiceFTPPath"]);
                            Dealer.EInvoiceFTPUserID = Convert.ToString(Dr["EInvoiceFTPUserID"]);
                            Dealer.EInvoiceFTPPassword = Convert.ToString(Dr["EInvoiceFTPPassword"]);
                            Dealer.StateN = DBNull.Value == Dr["StateID"] ? null : new PDMS_State() { StateID = Convert.ToInt32(Dr["StateID"]), State = Convert.ToString(Dr["State"]) };
                            Dealers.Add(Dealer);                             
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Dealers;
        }
        public List<PDMS_DealerOffice> GetDealerOffice(int? DealerID, int? DealerOfficeID, string DealerOfficeCode)
        {
            List<PDMS_DealerOffice> DealerOffice = new List<PDMS_DealerOffice>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

                DbParameter DealerOfficeIDP = provider.CreateParameter("OfficeID", DealerOfficeID, DbType.Int32);
                DbParameter DealerOfficeCodeP;
                if (!string.IsNullOrEmpty(DealerOfficeCode))
                    DealerOfficeCodeP = provider.CreateParameter("OfficeCode", DealerOfficeCode, DbType.String);
                else
                    DealerOfficeCodeP = provider.CreateParameter("OfficeCode", null, DbType.String);

                DbParameter[] Params = new DbParameter[3] { DealerIDP, DealerOfficeIDP, DealerOfficeCodeP };

                using (DataSet DataSet = provider.Select("ZDMS_GetDealerOffice", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            DealerOffice.Add(new PDMS_DealerOffice()
                            {
                                OfficeID = Convert.ToInt32(dr["OfficeCodeID"]),
                                OfficeCode = Convert.ToString(dr["OfficeCode"]),
                                OfficeName = Convert.ToString(dr["OfficeName"]),
                                OfficeName_OfficeCode = Convert.ToString(dr["OfficeName"]) + " " + Convert.ToString(dr["OfficeCode"]),
                                SapLocationCode = Convert.ToString(dr["SapLocationCode"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return DealerOffice;
        }
        public List<PDMS_Dealer> GetDealerBankDetails(int? DealerID, string DealerCode, int? DealerBankID)
        {
            List<PDMS_Dealer> Dealers = new List<PDMS_Dealer>();
            PDMS_Dealer Dealer = null;
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
            DbParameter DealerBankIDP = provider.CreateParameter("DealerBankID", DealerBankID, DbType.Int32);
            DbParameter[] Params = new DbParameter[2] { DealerIDP, DealerCodeP };
            try
            {
                using (DataSet DS = provider.Select("ZDMS_GetDealerBankDetails", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            Dealer = new PDMS_Dealer();
                            Dealer.DealerID = Convert.ToInt32(dr["DealerID"]);
                            Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                            Dealer.DealerName = Convert.ToString(dr["DealerName"]);
                            Dealer.DealerBank = new PDMS_DealerBankDetails();
                            Dealer.DealerBank.DealerBankID = Convert.ToInt32(dr["DealerBankID"]);
                            Dealer.DealerBank.BankName = Convert.ToString(dr["BankName"]);
                            Dealer.DealerBank.Branch = Convert.ToString(dr["Branch"]);
                            Dealer.DealerBank.AcNumber = Convert.ToString(dr["AcNumber"]);
                            Dealer.DealerBank.IfscCode = Convert.ToString(dr["IfscCode"]); 
                            Dealers.Add(Dealer); 
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Dealers;
        }
        public Boolean InsertOrUpdateDealerBankDetails(PDMS_DealerBankDetails BankDetails, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter DealerBankID = provider.CreateParameter("DealerBankID", BankDetails.DealerBankID, DbType.Int32);
                DbParameter DealerID = provider.CreateParameter("DealerID", BankDetails.DealerID, DbType.Int32);
                DbParameter BankName = provider.CreateParameter("BankName", BankDetails.BankName, DbType.String);
                DbParameter Branch = provider.CreateParameter("Branch", BankDetails.Branch, DbType.String);
                DbParameter AcNumber = provider.CreateParameter("AcNumber", BankDetails.AcNumber, DbType.String);
                DbParameter IfscCode = provider.CreateParameter("IfscCode", BankDetails.IfscCode, DbType.String);
               // DbParameter IsActive = provider.CreateParameter("IsActive", BankDetails.IsActive, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    DbParameter[] Params = new DbParameter[7] { DealerBankID,DealerID, BankName, Branch, AcNumber, IfscCode,  UserIDP };
                    provider.Insert("ZDMS_InsertOrUpdateDealerBankDetails", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Dealer", "InsertOrUpdateDealerBankDetails", ex);
                return false;
            }
            return true;
        }

        public int InsertOrUpdateDealerEmployee(PDMS_DealerEmployee Emp, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            int EmployeeID = 0;
            try
            {
                DbParameter DealerEmployeeID = provider.CreateParameter("DealerEmployeeID", Emp.DealerEmployeeID, DbType.Int32);
                 //DbParameter LoginUserName = provider.CreateParameter("LoginUserName", Emp.LoginUserName, DbType.String);
                DbParameter Name = provider.CreateParameter("Name", Emp.Name.ToUpper(), DbType.String);
                DbParameter FatherName = provider.CreateParameter("FatherName", Emp.FatherName.ToUpper(), DbType.String);
                DbParameter DOB = provider.CreateParameter("DOB", Emp.DOB, DbType.DateTime);
                DbParameter ContactNumber = provider.CreateParameter("ContactNumber", Emp.ContactNumber, DbType.String);
                DbParameter ContactNumber1 = provider.CreateParameter("ContactNumber1", Emp.ContactNumber1, DbType.String);
            //    DbParameter DateOfJoining = provider.CreateParameter("DateOfJoining", Emp.DateOfJoining, DbType.DateTime);
             //   DbParameter DealerDepartmentID = provider.CreateParameter("DealerDepartmentID", Emp.DealerDepartment == null ? (int?)null : Emp.DealerDepartment.DealerDepartmentID, DbType.Int32);
             //   DbParameter DealerDesignationID = provider.CreateParameter("DealerDesignationID", Emp.DealerDesignation == null ? (int?)null : Emp.DealerDesignation.DealerDesignationID, DbType.Int32);
             //   DbParameter ReportingTo = provider.CreateParameter("ReportingTo", Emp.ReportingTo == null ? (int?)null : Emp.ReportingTo.DealerEmployeeID, DbType.Int32);

                DbParameter Email = provider.CreateParameter("Email", Emp.Email, DbType.String);
                DbParameter Address = provider.CreateParameter("Address", Emp.Address.ToUpper(), DbType.String);
                DbParameter StateID = provider.CreateParameter("StateID", Emp.State == null ? (int?)null : Emp.State.StateID, DbType.Int32);
                DbParameter DistrictID = provider.CreateParameter("DistrictID", Emp.District == null ? (int?)null : Emp.District.DistrictID, DbType.Int32);
                DbParameter TehsilID = provider.CreateParameter("TehsilID", Emp.Tehsil == null ? (int?)null : Emp.Tehsil.TehsilID, DbType.Int32);
                DbParameter Village = provider.CreateParameter("Village", Emp.Village, DbType.String);
                DbParameter BloodGroupID = provider.CreateParameter("BloodGroupID", Emp.BloodGroup == null ? (int?)null : Emp.BloodGroup.BloodGroupID, DbType.Int32);
                DbParameter EmergencyContactNumber = provider.CreateParameter("EmergencyContactNumber", Emp.EmergencyContactNumber, DbType.String);
                DbParameter Location = provider.CreateParameter("Location", Emp.Location, DbType.String);
                DbParameter AadhaarCardNo = provider.CreateParameter("AadhaarCardNo", Emp.AadhaarCardNo, DbType.String);
                DbParameter EqucationalQualificationID = provider.CreateParameter("EqucationalQualificationID", Emp.EqucationalQualification == null ? (int?)null : Emp.EqucationalQualification.EqucationalQualificationID, DbType.Int64);
                DbParameter TotalExperience = provider.CreateParameter("TotalExperience", Emp.TotalExperience, DbType.Decimal);
                DbParameter PANNo = provider.CreateParameter("PANNo", Emp.PANNo.ToUpper(), DbType.String);
                DbParameter BankName = provider.CreateParameter("BankName", Emp.BankName.ToUpper(), DbType.String);
                DbParameter AccountNo = provider.CreateParameter("AccountNo", Emp.AccountNo, DbType.String);
                DbParameter IFSCCode = provider.CreateParameter("IFSCCode", Emp.IFSCCode.ToUpper(), DbType.String);

                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter OutValueDParam = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //DbParameter PhotoID = provider.CreateParameter("PhotoID", string.IsNullOrEmpty(Emp.Photo.FileName) ? (long?)null : InsertOrUpdateDealerEmployeeAttachedFile(Emp.Photo, UserID), DbType.Int64);
                    //DbParameter AdhaarCardCopyFrontSideID = provider.CreateParameter("AdhaarCardCopyFrontSideID", string.IsNullOrEmpty(Emp.AdhaarCardCopyFrontSide.FileName) ? (long?)null : InsertOrUpdateDealerEmployeeAttachedFile(Emp.AdhaarCardCopyFrontSide, UserID), DbType.Int64);
                    //DbParameter AdhaarCardCopyBackSideID = provider.CreateParameter("AdhaarCardCopyBackSideID", string.IsNullOrEmpty(Emp.AdhaarCardCopyBackSide.FileName) ? (long?)null : InsertOrUpdateDealerEmployeeAttachedFile(Emp.AdhaarCardCopyBackSide, UserID), DbType.Int64);
                    //DbParameter PANCardCopyID = provider.CreateParameter("PANCardCopyID", string.IsNullOrEmpty(Emp.PANCardCopy.FileName) ? (long?)null : InsertOrUpdateDealerEmployeeAttachedFile(Emp.PANCardCopy, UserID), DbType.Int64);
                    //DbParameter ChequeCopyID = provider.CreateParameter("ChequeCopyID", string.IsNullOrEmpty(Emp.ChequeCopy.FileName) ? (long?)null : InsertOrUpdateDealerEmployeeAttachedFile(Emp.ChequeCopy, UserID), DbType.Int64);


                    DbParameter PhotoID = provider.CreateParameter("PhotoID", Emp.Photo.AttachedFileID != 0 ? Emp.Photo.AttachedFileID : InsertOrUpdateDealerEmployeeAttachedFile(Emp.Photo, UserID), DbType.Int64);
                    DbParameter AdhaarCardCopyFrontSideID = provider.CreateParameter("AdhaarCardCopyFrontSideID", Emp.AdhaarCardCopyFrontSide.AttachedFileID != 0 ? Emp.AdhaarCardCopyFrontSide.AttachedFileID : InsertOrUpdateDealerEmployeeAttachedFile(Emp.AdhaarCardCopyFrontSide, UserID), DbType.Int64);
                    DbParameter AdhaarCardCopyBackSideID = provider.CreateParameter("AdhaarCardCopyBackSideID", Emp.AdhaarCardCopyBackSide.AttachedFileID != 0 ? Emp.AdhaarCardCopyBackSide.AttachedFileID : InsertOrUpdateDealerEmployeeAttachedFile(Emp.AdhaarCardCopyBackSide, UserID), DbType.Int64);
                    DbParameter PANCardCopyID = provider.CreateParameter("PANCardCopyID", Emp.PANCardCopy.AttachedFileID != 0 ? Emp.PANCardCopy.AttachedFileID : InsertOrUpdateDealerEmployeeAttachedFile(Emp.PANCardCopy, UserID), DbType.Int64);
                    DbParameter ChequeCopyID = provider.CreateParameter("ChequeCopyID", Emp.ChequeCopy.AttachedFileID != 0 ? Emp.ChequeCopy.AttachedFileID : InsertOrUpdateDealerEmployeeAttachedFile(Emp.ChequeCopy, UserID), DbType.Int64);


                    DbParameter[] Params = new DbParameter[29] { DealerEmployeeID, Name, FatherName, PhotoID, DOB, ContactNumber,ContactNumber1, Email, Address, StateID, DistrictID, TehsilID, Village,BloodGroupID,EmergencyContactNumber
                     ,Location,AadhaarCardNo,AdhaarCardCopyFrontSideID,AdhaarCardCopyBackSideID,EqucationalQualificationID,TotalExperience,PANNo,PANCardCopyID
                     ,BankName,AccountNo,IFSCCode,ChequeCopyID,UserIDP,OutValueDParam};

                    provider.Insert("ZDMS_InsertOrUpdateDealerEmployee", Params);
                    scope.Complete();
                    EmployeeID = Convert.ToInt32(OutValueDParam.Value);
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Dealer", "InsertOrUpdateDealerEmployee", ex);
                return 0;
            }
            return EmployeeID;
        }
        public Int64 InsertOrUpdateDealerEmployeeAttachedFile(PDMS_DealerEmployeeAttachedFile AttachedFile, int UserID)
        {

            int success = 0;
            long AttachedFileID = 0;
            // PDMS_ServiceMaterial MM = new SMaterial().getMaterialTax(Customer, Vendor, OrderType, 1, MaterialService, 1);
            // new SCustomer().getCustomerAddress(Customer);
            DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFile.AttachedFileID, DbType.Int64);
            DbParameter DealerEmployeeID = provider.CreateParameter("DealerEmployeeID", AttachedFile.DealerEmployeeID, DbType.Int64);
            DbParameter AttachedFileP = provider.CreateParameter("AttachedFile", AttachedFile.AttachedFile, DbType.Binary);
            DbParameter FileType = provider.CreateParameter("ContentType", AttachedFile.FileType, DbType.String);
            DbParameter FileName = provider.CreateParameter("FileName", AttachedFile.FileName, DbType.String);
            DbParameter FileSize = provider.CreateParameter("FileSize", AttachedFile.FileSize, DbType.Int32);
            DbParameter IsDeleted = provider.CreateParameter("IsDeleted", AttachedFile.IsDeleted, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter OutValueDParam = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[9] { AttachedFileIDP, DealerEmployeeID, AttachedFileP, FileType, FileName, FileSize, IsDeleted, UserIDP, OutValueDParam };
            try
            {
                success = provider.Insert("ZDMS_InsertOrUpdateDealerEmployeeAttachedFile", Params);
                AttachedFileID = Convert.ToInt64(OutValueDParam.Value);
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Dealer", "InsertOrUpdateDealerEmployeeAttachedFile", sqlEx);
                return 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Dealer", " InsertOrUpdateDealerEmployeeAttachedFile", ex);
                return 0;
            }
            return AttachedFileID;
        }

        //public int InsertOrUpdateDealerEmployeeRole1(PDMS_DealerEmployee Emp, int UserID)
        //{
        //    TraceLogger.Log(DateTime.Now);
        //    try
        //    {
        //        DbParameter DealerEmployeeID = provider.CreateParameter("DealerEmployeeID", Emp.DealerEmployeeID, DbType.Int32);
        //        //   DbParameter DealerID = provider.CreateParameter("DealerID", Emp.Dealer.DealerID, DbType.Int32);
        //        DbParameter Name = provider.CreateParameter("Name", Emp.Name, DbType.String);
        //        DbParameter FatherName = provider.CreateParameter("FatherName", Emp.FatherName, DbType.String);
        //        DbParameter DOB = provider.CreateParameter("DOB", Emp.DOB, DbType.DateTime);
        //        DbParameter ContactNumber = provider.CreateParameter("ContactNumber", Emp.ContactNumber, DbType.String);

        //        //    DbParameter DateOfJoining = provider.CreateParameter("DateOfJoining", Emp.DateOfJoining, DbType.DateTime);
        //        //   DbParameter DealerDepartmentID = provider.CreateParameter("DealerDepartmentID", Emp.DealerDepartment == null ? (int?)null : Emp.DealerDepartment.DealerDepartmentID, DbType.Int32);
        //        //   DbParameter DealerDesignationID = provider.CreateParameter("DealerDesignationID", Emp.DealerDesignation == null ? (int?)null : Emp.DealerDesignation.DealerDesignationID, DbType.Int32);
        //        //   DbParameter ReportingTo = provider.CreateParameter("ReportingTo", Emp.ReportingTo == null ? (int?)null : Emp.ReportingTo.DealerEmployeeID, DbType.Int32);

        //        DbParameter Email = provider.CreateParameter("Email", Emp.Email, DbType.String);
        //        DbParameter Address = provider.CreateParameter("Address", Emp.Address, DbType.String);
        //        DbParameter StateID = provider.CreateParameter("StateID", Emp.State == null ? (int?)null : Emp.State.StateID, DbType.Int32);
        //        DbParameter DistrictID = provider.CreateParameter("DistrictID", Emp.District == null ? (int?)null : Emp.District.DistrictID, DbType.Int32);
        //        DbParameter TehsilID = provider.CreateParameter("TehsilID", Emp.Tehsil == null ? (int?)null : Emp.Tehsil.TehsilID, DbType.Int32);
        //        DbParameter VillageID = provider.CreateParameter("VillageID", Emp.Village == null ? (int?)null : Emp.Village.VillageID, DbType.Int32);
        //        DbParameter Location = provider.CreateParameter("Location", Emp.Location, DbType.String);
        //        DbParameter AadhaarCardNo = provider.CreateParameter("AadhaarCardNo", Emp.AadhaarCardNo, DbType.String);
        //        DbParameter EqucationalQualificationID = provider.CreateParameter("EqucationalQualificationID", Emp.EqucationalQualification == null ? (int?)null : Emp.EqucationalQualification.EqucationalQualificationID, DbType.Int64);
        //        DbParameter TotalExperience = provider.CreateParameter("TotalExperience", Emp.TotalExperience, DbType.Decimal);
        //        DbParameter PANNo = provider.CreateParameter("PANNo", Emp.PANNo, DbType.String);
        //        DbParameter BankName = provider.CreateParameter("BankName", Emp.BankName, DbType.String);
        //        DbParameter AccountNo = provider.CreateParameter("AccountNo", Emp.AccountNo, DbType.String);
        //        DbParameter IFSCCode = provider.CreateParameter("IFSCCode", Emp.IFSCCode, DbType.String);

        //        DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //        DbParameter OutValueDParam = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));

        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            DbParameter PhotoID = provider.CreateParameter("PhotoID", string.IsNullOrEmpty(Emp.Photo.FileName) ? (long?)null : InsertOrUpdateDealerEmployeeAttachedFile(Emp.Photo, UserID), DbType.Int64);
        //            DbParameter AdhaarCardCopyFrontSideID = provider.CreateParameter("AdhaarCardCopyFrontSideID", string.IsNullOrEmpty(Emp.AdhaarCardCopyFrontSide.FileName) ? (long?)null : InsertOrUpdateDealerEmployeeAttachedFile(Emp.AdhaarCardCopyFrontSide, UserID), DbType.Int64);
        //            DbParameter AdhaarCardCopyBackSideID = provider.CreateParameter("AdhaarCardCopyBackSideID", string.IsNullOrEmpty(Emp.AdhaarCardCopyBackSide.FileName) ? (long?)null : InsertOrUpdateDealerEmployeeAttachedFile(Emp.AdhaarCardCopyBackSide, UserID), DbType.Int64);
        //            DbParameter PANCardCopyID = provider.CreateParameter("PANCardCopyID", string.IsNullOrEmpty(Emp.PANCardCopy.FileName) ? (long?)null : InsertOrUpdateDealerEmployeeAttachedFile(Emp.PANCardCopy, UserID), DbType.Int64);
        //            DbParameter ChequeCopyID = provider.CreateParameter("ChequeCopyID", string.IsNullOrEmpty(Emp.ChequeCopy.FileName) ? (long?)null : InsertOrUpdateDealerEmployeeAttachedFile(Emp.ChequeCopy, UserID), DbType.Int64);

        //            DbParameter[] Params = new DbParameter[26] { DealerEmployeeID, Name, FatherName, PhotoID, DOB, ContactNumber, Email, Address, StateID, DistrictID, TehsilID, VillageID
        //             ,Location,AadhaarCardNo,AdhaarCardCopyFrontSideID,AdhaarCardCopyBackSideID,EqucationalQualificationID,TotalExperience,PANNo,PANCardCopyID
        //             ,BankName,AccountNo,IFSCCode,ChequeCopyID,UserIDP,OutValueDParam};

        //            provider.Insert("ZDMS_InsertOrUpdateDealerEmployee", Params);
        //            scope.Complete();
        //        }
        //        TraceLogger.Log(DateTime.Now);
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessageService("BDMS_Dealer", "InsertOrUpdateDealerEmployee", ex);
        //        return 0;
        //    }
        //    return 1;
        //}
        public void GetEqucationalQualificationDDL(DropDownList ddl, int? EqucationalQualificationID, string EqucationalQualification)
        {
            List<PDMS_EqucationalQualification> Qualification = new List<PDMS_EqucationalQualification>();
            try
            {
                DbParameter EqucationalQualificationIDP = provider.CreateParameter("EqucationalQualificationID", EqucationalQualificationID, DbType.Int32);
                DbParameter EqucationalQualificationP;
                if (!string.IsNullOrEmpty(EqucationalQualification))
                    EqucationalQualificationP = provider.CreateParameter("EqucationalQualification", EqucationalQualification, DbType.String);
                else
                    EqucationalQualificationP = provider.CreateParameter("EqucationalQualification", null, DbType.String);
                DbParameter[] Params = new DbParameter[2] { EqucationalQualificationIDP, EqucationalQualificationP };
                using (DataSet DataSet = provider.Select("ZDMS_GetEqucationalQualification", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Qualification.Add(new PDMS_EqucationalQualification()
                            {
                                EqucationalQualificationID = Convert.ToInt32(dr["EqucationalQualificationID"]),
                                EqucationalQualification = Convert.ToString(dr["EqucationalQualification"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataValueField = "EqucationalQualificationID";
            ddl.DataTextField = "EqucationalQualification";
            ddl.DataSource = Qualification;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }

        public void GetBloodGroupDDL(DropDownList ddl, int? BloodGroupID, string BloodGroup)
        {
            List<PDMS_BloodGroup> BG = new List<PDMS_BloodGroup>();
            try
            {
                DbParameter BloodGroupIDP = provider.CreateParameter("BloodGroupID", BloodGroupID, DbType.Int32);
                DbParameter BloodGroupP;
                if (!string.IsNullOrEmpty(BloodGroup))
                    BloodGroupP = provider.CreateParameter("BloodGroup", BloodGroup, DbType.String);
                else
                    BloodGroupP = provider.CreateParameter("BloodGroup", null, DbType.String);
                DbParameter[] Params = new DbParameter[2] { BloodGroupIDP, BloodGroupP };
                using (DataSet DataSet = provider.Select("ZDMS_GetBloodGroup", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            BG.Add(new PDMS_BloodGroup()
                            {
                                BloodGroupID = Convert.ToInt32(dr["BloodGroupID"]),
                                BloodGroup = Convert.ToString(dr["BloodGroup"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataValueField = "BloodGroupID";
            ddl.DataTextField = "BloodGroup";
            ddl.DataSource = BG;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }

        public List<PDMS_DealerEmployee> GetDealerEmployeeManage(int? DealerID, string AadhaarCardNo, int? StateID, int? DistrictID, string Name, string SAPEmpCode, Boolean? StatusID)
        {
            List<PDMS_DealerEmployee> EMP = new List<PDMS_DealerEmployee>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter AadhaarCardNoP = provider.CreateParameter("AadhaarCardNo", string.IsNullOrEmpty(AadhaarCardNo) ? null : AadhaarCardNo, DbType.String);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter DistrictIDP = provider.CreateParameter("DistrictID", DistrictID, DbType.Int32);
                DbParameter NameP = provider.CreateParameter("Name", Name, DbType.String);
                DbParameter SAPEmpCodeP = provider.CreateParameter("SAPEmpCode", string.IsNullOrEmpty(SAPEmpCode) ? null : SAPEmpCode, DbType.String);
                DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Boolean);
                DbParameter[] Params = new DbParameter[7] { DealerIDP, AadhaarCardNoP, StateIDP, DistrictIDP, NameP, SAPEmpCodeP, StatusIDP };

                using (DataSet DataSet = provider.Select("ZDMS_GetDealerEmployeeManage", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            EMP.Add(new PDMS_DealerEmployee()
                            {
                                DealerEmployeeID = Convert.ToInt32(dr["DealerEmployeeID"]),
                                Name = Convert.ToString(dr["Name"]),
                                FatherName = Convert.ToString(dr["FatherName"]),
                                DOB = Convert.ToDateTime(dr["DOB"]),
                                ContactNumber = Convert.ToString(dr["ContactNumber"]),
                                Email = Convert.ToString(dr["EmailID"]),
                                State = new PDMS_State() { State = Convert.ToString(dr["State"]) },
                                District = new PDMS_District() { District = Convert.ToString(dr["District"]) },
                                Location = Convert.ToString(dr["Location"]),
                                AadhaarCardNo = Convert.ToString(dr["AadhaarCardNo"]),
                                PANNo = Convert.ToString(dr["PANNo"]),

                                BankName = Convert.ToString(dr["BankName"]),
                                AccountNo = Convert.ToString(dr["AccountNo"]),
                                IFSCCode = Convert.ToString(dr["IFSCCode"]),
                                TotalExperience = Convert.ToDecimal("0" + Convert.ToString(dr["TotalExperience"])),

                                DealerEmployeeRole = DBNull.Value == dr["DealerEmployeeRoleID"] ? null : new PDMS_DealerEmployeeRole()
                                {
                                    DealerEmployeeRoleID = Convert.ToInt64(dr["DealerEmployeeRoleID"]),
                                    Dealer = new PDMS_Dealer()
                                    {
                                        DealerID = Convert.ToInt32(dr["DealerID"]),
                                        DealerCode = Convert.ToString(dr["DealerCode"]),
                                        DealerName = Convert.ToString(dr["DealerName"]),
                                        State = Convert.ToString(dr["DealerState"]),
                                       // StateCode = Convert.ToString(dr["StateCode"])
                                    },
                                    DealerOffice = new PDMS_DealerOffice() { OfficeName = Convert.ToString(dr["OfficeName"]) },
                                    DealerDepartment = new PDMS_DealerDepartment() { DealerDepartment = Convert.ToString(dr["DealerDepartment"]) },
                                    DealerDesignation = new PDMS_DealerDesignation() { DealerDesignation = Convert.ToString(dr["DealerDesignation"]) },
                                    ReportingTo = DBNull.Value == dr["ReportingToID"] ? null : new PDMS_DealerEmployee() { DealerEmployeeID = Convert.ToInt32(dr["ReportingToID"]), Name = Convert.ToString(dr["ReportingToName"]) },
                                    DateOfLeaving = DBNull.Value == dr["DateOfLeaving"] ? (DateTime?)null : Convert.ToDateTime(dr["DateOfLeaving"]),
                                    DateOfJoining = Convert.ToDateTime(dr["DateOfJoining"]),
                                    IsActive = Convert.ToBoolean(dr["IsActive"]),
                                    SAPEmpCode = Convert.ToString(dr["SAPEmpCode"])
                                },
                                IsAjaxHPApproved = Convert.ToBoolean(dr["IsAjaxHPApproved"]),
                         //       CreatedBy = new PUser() { ContactName = Convert.ToString(dr["ContactName"]), UserID = Convert.ToInt32(dr["UserID"]) }
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return EMP;
        }
        public List<PDMS_DealerEmployee> GetDealerEmployeeManageBasedRole( string AadhaarCardNo )
        {
            List<PDMS_DealerEmployee> EMP = new List<PDMS_DealerEmployee>();
            try
            {
                 DbParameter AadhaarCardNoP = provider.CreateParameter("AadhaarCardNo", string.IsNullOrEmpty(AadhaarCardNo) ? null : AadhaarCardNo, DbType.String);
                 DbParameter[] Params = new DbParameter[1] {  AadhaarCardNoP };

                 using (DataSet DataSet = provider.Select("ZDMS_GetDealerEmployeeManageBasedRole", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            EMP.Add(new PDMS_DealerEmployee()
                            {
                                DealerEmployeeID = Convert.ToInt32(dr["DealerEmployeeID"]),
                                Name = Convert.ToString(dr["Name"]),
                                FatherName = Convert.ToString(dr["FatherName"]),
                                DOB = Convert.ToDateTime(dr["DOB"]),
                                ContactNumber = Convert.ToString(dr["ContactNumber"]),
                                Email = Convert.ToString(dr["EmailID"]),
                                State = new PDMS_State() { State = Convert.ToString(dr["State"]) },
                                District = new PDMS_District() { District = Convert.ToString(dr["District"]) },
                                Location = Convert.ToString(dr["Location"]),
                                AadhaarCardNo = Convert.ToString(dr["AadhaarCardNo"]),
                                PANNo = Convert.ToString(dr["PANNo"]),

                                BankName = Convert.ToString(dr["BankName"]),
                                AccountNo = Convert.ToString(dr["AccountNo"]),
                                IFSCCode = Convert.ToString(dr["IFSCCode"]),
                                TotalExperience = Convert.ToDecimal("0" + Convert.ToString(dr["TotalExperience"])),

                                DealerEmployeeRole = DBNull.Value == dr["DealerEmployeeRoleID"] ? null : new PDMS_DealerEmployeeRole()
                                {
                                    DealerEmployeeRoleID = Convert.ToInt64(dr["DealerEmployeeRoleID"]),
                                    Dealer = new PDMS_Dealer()
                                    {
                                        DealerID = Convert.ToInt32(dr["DealerID"]),
                                        DealerCode = Convert.ToString(dr["DealerCode"]),
                                        DealerName = Convert.ToString(dr["DealerName"]),
                                        State = Convert.ToString(dr["DealerState"]),
                                        // StateCode = Convert.ToString(dr["StateCode"])
                                    },
                                    DealerOffice = new PDMS_DealerOffice() { OfficeName = Convert.ToString(dr["OfficeName"]) },
                                    DealerDepartment = new PDMS_DealerDepartment() { DealerDepartment = Convert.ToString(dr["DealerDepartment"]) },
                                    DealerDesignation = new PDMS_DealerDesignation() { DealerDesignation = Convert.ToString(dr["DealerDesignation"]) },
                                    ReportingTo = DBNull.Value == dr["ReportingToID"] ? null : new PDMS_DealerEmployee() { DealerEmployeeID = Convert.ToInt32(dr["ReportingToID"]), Name = Convert.ToString(dr["ReportingToName"]) },
                                    DateOfLeaving = DBNull.Value == dr["DateOfLeaving"] ? (DateTime?)null : Convert.ToDateTime(dr["DateOfLeaving"]),
                                    DateOfJoining = Convert.ToDateTime(dr["DateOfJoining"]),
                                    IsActive = Convert.ToBoolean(dr["IsActive"]),
                                    SAPEmpCode = Convert.ToString(dr["SAPEmpCode"])
                                },
                                IsAjaxHPApproved = Convert.ToBoolean(dr["IsAjaxHPApproved"]),
                                CreatedBy = new PUser() { ContactName = Convert.ToString(dr["ContactName"]), UserID = Convert.ToInt32(dr["UserID"]) }
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return EMP;
        }
        public  PDMS_DealerEmployee GetDealerEmployeeByDealerEmployeeID(int DealerEmployeeID)
        {
            PDMS_DealerEmployee EMP = new PDMS_DealerEmployee();
            try
            {
                DbParameter DealerEmployeeIDP = provider.CreateParameter("DealerEmployeeID", DealerEmployeeID, DbType.Int32);
                 DbParameter[] Params = new DbParameter[1] { DealerEmployeeIDP};

                 using (DataSet DataSet = provider.Select("ZDMS_GetDealerEmployeeByDealerEmployeeID", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            EMP = new PDMS_DealerEmployee()
                            {
                                DealerEmployeeID = Convert.ToInt32(dr["DealerEmployeeID"]),

                                Name = Convert.ToString(dr["Name"]),
                                FatherName = Convert.ToString(dr["FatherName"]),
                                DOB =  Convert.ToDateTime(dr["DOB"]),
                                ContactNumber = Convert.ToString(dr["ContactNumber"]),
                                ContactNumber1 = Convert.ToString(dr["ContactNumber1"]),
                                Email = Convert.ToString(dr["EmailID"]),
                                Address = Convert.ToString(dr["Address"]),
                                State = DBNull.Value == dr["StateID"] ? null : new PDMS_State() { StateID = Convert.ToInt32(dr["StateID"]), State = Convert.ToString(dr["State"]) },
                                District = DBNull.Value == dr["DistrictID"] ? null : new PDMS_District() { DistrictID = Convert.ToInt32(dr["DistrictID"]), District = Convert.ToString(dr["District"]) },
                                Tehsil = DBNull.Value == dr["TehsilID"] ? null : new PDMS_Tehsil() { TehsilID = Convert.ToInt32(dr["TehsilID"]), Tehsil = Convert.ToString(dr["Tehsil"]) },
                                Village =    Convert.ToString(dr["Village"]),
                                Location = Convert.ToString(dr["Location"]),
                                AadhaarCardNo = Convert.ToString(dr["AadhaarCardNo"]),
                                EqucationalQualification = DBNull.Value == dr["EqucationalQualificationID"] ? null : new PDMS_EqucationalQualification() { EqucationalQualificationID = Convert.ToInt32(dr["EqucationalQualificationID"]), EqucationalQualification = Convert.ToString(dr["EqucationalQualification"]) },
                                TotalExperience = DBNull.Value == dr["TotalExperience"] ? (decimal?)null : Convert.ToDecimal(dr["TotalExperience"]),
                                PANNo = Convert.ToString(dr["PANNo"]),
                                BankName = Convert.ToString(dr["BankName"]),
                                AccountNo = Convert.ToString(dr["AccountNo"]),
                                IFSCCode = Convert.ToString(dr["IFSCCode"]),
                                EmergencyContactNumber = Convert.ToString(dr["EmergencyContactNumber"]),
                                BloodGroup = DBNull.Value == dr["BloodGroupID"] ? null : new PDMS_BloodGroup() { BloodGroupID = Convert.ToInt32(dr["BloodGroupID"]), BloodGroup = Convert.ToString(dr["BloodGroup"]) },
                              

                                Photo = DBNull.Value == dr["APHAttachedFileID"] ? null : new PDMS_DealerEmployeeAttachedFile() { AttachedFileID = Convert.ToInt32(dr["APHAttachedFileID"]), FileName = Convert.ToString(dr["APHFileName"]) },
                                AdhaarCardCopyFrontSide = DBNull.Value == dr["AAFAttachedFileID"] ? null : new PDMS_DealerEmployeeAttachedFile() { AttachedFileID = Convert.ToInt32(dr["AAFAttachedFileID"]), FileName = Convert.ToString(dr["AAFFileName"]) },
                                AdhaarCardCopyBackSide = DBNull.Value == dr["AABAttachedFileID"] ? null : new PDMS_DealerEmployeeAttachedFile() { AttachedFileID = Convert.ToInt32(dr["AABAttachedFileID"]), FileName = Convert.ToString(dr["AABFileName"]) },
                                PANCardCopy = DBNull.Value == dr["APAAttachedFileID"] ? null : new PDMS_DealerEmployeeAttachedFile() { AttachedFileID = Convert.ToInt32(dr["APAAttachedFileID"]), FileName = Convert.ToString(dr["APAFileName"]) },
                                ChequeCopy = DBNull.Value == dr["ACAttachedFileID"] ? null : new PDMS_DealerEmployeeAttachedFile() { AttachedFileID = Convert.ToInt32(dr["ACAttachedFileID"]), FileName = Convert.ToString(dr["ACFileName"]) },
                                DealerEmployeeRole = DBNull.Value == dr["DealerEmployeeRoleID"] ? null : new PDMS_DealerEmployeeRole()
                                {
                                    DealerEmployeeRoleID = Convert.ToInt64(dr["DealerEmployeeRoleID"]),
                                    Dealer = DBNull.Value == dr["DealerID"] ? null : new PDMS_Dealer() { DealerID = Convert.ToInt32(dr["DealerID"]), DealerName = Convert.ToString(dr["DealerName"]) },
                                    DealerOffice = DBNull.Value == dr["OfficeID"] ? null : new PDMS_DealerOffice() { OfficeID = Convert.ToInt32(dr["OfficeID"]), OfficeName = Convert.ToString(dr["OfficeName"]) },
                                    DateOfJoining =  Convert.ToDateTime(dr["DateOfJoining"]),
                                    DealerDepartment = DBNull.Value == dr["DealerDepartmentID"] ? null : new PDMS_DealerDepartment() { DealerDepartmentID = Convert.ToInt32(dr["DealerDepartmentID"]), DealerDepartment = Convert.ToString(dr["DealerDepartment"]) },
                                    DealerDesignation = DBNull.Value == dr["DealerDesignationID"] ? null : new PDMS_DealerDesignation() { DealerDesignationID = Convert.ToInt32(dr["DealerDesignationID"]), DealerDesignation = Convert.ToString(dr["DealerDesignation"]) },
                                    ReportingTo = DBNull.Value == dr["ReportingToID"] ? null : new PDMS_DealerEmployee() { DealerEmployeeID = Convert.ToInt32(dr["ReportingToID"]), Name = Convert.ToString(dr["ReportingToName"]) },
                                    DateOfLeaving = DBNull.Value == dr["DateOfLeaving"] ? (DateTime?)null : Convert.ToDateTime(dr["DateOfLeaving"]),
                                    WishToLeave = DBNull.Value == dr["WishToLeave"] ? (Boolean?)null : Convert.ToBoolean(dr["WishToLeave"]),
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
            return EMP;
        }
        public PDMS_DealerEmployeeAttachedFile GetDealerEmployeeAttachedFile(long AttachedFileID)
        {
            PDMS_DealerEmployeeAttachedFile EMP = new PDMS_DealerEmployeeAttachedFile();
            try
            {
                DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { AttachedFileIDP };

                using (DataSet DataSet = provider.Select("ZDMS_GetDealerEmployeeAttachedFile", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            EMP = new PDMS_DealerEmployeeAttachedFile()
                            {
                                AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]),
                                AttachedFile = (Byte[])(dr["AttachedFile"]),
                                FileType = Convert.ToString(dr["ContentType"]),
                                FileName = Convert.ToString(dr["FileName"]),
                                FileSize = Convert.ToInt32(dr["FileSize"]) 
                            };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return EMP;
        }

        public void GetDealerDepartmentDDL(DropDownList ddl, int? DealerDepartmentID, string DealerDepartment)
        {
            List<PDMS_DealerDepartment> Qualification = new List<PDMS_DealerDepartment>();
            try
            {
                DbParameter DealerDepartmentIDP = provider.CreateParameter("DealerDepartmentID", DealerDepartmentID, DbType.Int32);
                DbParameter DealerDepartmentP;
                if (!string.IsNullOrEmpty(DealerDepartment))
                    DealerDepartmentP = provider.CreateParameter("DealerDepartment", DealerDepartment, DbType.String);
                else
                    DealerDepartmentP = provider.CreateParameter("DealerDepartment", null, DbType.String);
                DbParameter[] Params = new DbParameter[2] { DealerDepartmentIDP, DealerDepartmentP };
                using (DataSet DataSet = provider.Select("ZDMS_GetDealerDepartment", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Qualification.Add(new PDMS_DealerDepartment()
                            {
                                DealerDepartmentID = Convert.ToInt32(dr["DealerDepartmentID"]),
                                DealerDepartment = Convert.ToString(dr["DealerDepartment"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataValueField = "DealerDepartmentID";
            ddl.DataTextField = "DealerDepartment";
            ddl.DataSource = Qualification;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public void GetDealerDesignationDDL(DropDownList ddl,int? DealerDepartmentID, int? DealerDesignationID, string DealerDesignation)
        {
            List<PDMS_DealerDesignation> Qualification = new List<PDMS_DealerDesignation>();
            try
            {
                DbParameter DealerDepartmentIDP = provider.CreateParameter("DealerDepartmentID", DealerDepartmentID, DbType.Int32);
                DbParameter DealerDesignationIDP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int32);
                DbParameter DealerDesignationP;
                if (!string.IsNullOrEmpty(DealerDesignation))
                    DealerDesignationP = provider.CreateParameter("DealerDesignation", DealerDesignation, DbType.String);
                else
                    DealerDesignationP = provider.CreateParameter("DealerDesignation", null, DbType.String);
                DbParameter[] Params = new DbParameter[3] { DealerDepartmentIDP,DealerDesignationIDP, DealerDesignationP };
                using (DataSet DataSet = provider.Select("ZDMS_GetDealerDesignation", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Qualification.Add(new PDMS_DealerDesignation()
                            {
                                DealerDesignationID = Convert.ToInt32(dr["DealerDesignationID"]),
                                DealerDesignation = Convert.ToString(dr["DealerDesignation"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataValueField = "DealerDesignationID";
            ddl.DataTextField = "DealerDesignation";
            ddl.DataSource = Qualification;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }

        public void GetDealerEmployeeDDL(DropDownList ddl, int? DealerID)
        {
            List<PDMS_DealerEmployee> Employee = GetDealerEmployeeManage(DealerID, null, null, null, null, null,null);
            ddl.DataValueField = "DealerEmployeeID";
            ddl.DataTextField = "Name";
            ddl.DataSource = Employee;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        
        public List<PDMS_DealerEmployee> GetDealerEmployeeForApproval(int? DealerID)
        {
            List<PDMS_DealerEmployee> EMP = new List<PDMS_DealerEmployee>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32); 
                DbParameter[] Params = new DbParameter[1] { DealerIDP};
                using (DataSet DataSet = provider.Select("ZDMS_GetDealerEmployeeForApproval", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            EMP.Add(new PDMS_DealerEmployee()
                            {
                                DealerEmployeeID = Convert.ToInt32(dr["DealerEmployeeID"]),
                                Name = Convert.ToString(dr["Name"]),
                                FatherName = Convert.ToString(dr["FatherName"]),
                                ContactNumber = Convert.ToString(dr["ContactNumber"]), 
                                Email = Convert.ToString(dr["EmailID"]),
                                State = new PDMS_State() { State = Convert.ToString(dr["State"]) },
                                District = new PDMS_District() { District = Convert.ToString(dr["District"]) },
                                Location = Convert.ToString(dr["Location"]),
                                AadhaarCardNo = Convert.ToString(dr["AadhaarCardNo"]),
                                PANNo = Convert.ToString(dr["PANNo"]),
                                //DealerEmployeeRole = new PDMS_DealerEmployeeRole()
                                //{
                                //    Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(dr["DealerID"]), DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) }
                                //}
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return EMP;
        }
        public Boolean ApproveDealerEmployee(int DealerEmployeeID, int UserID)
        {
            try
            {
                DbParameter DealerEmployeeIDP = provider.CreateParameter("DealerEmployeeID", DealerEmployeeID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[2] { DealerEmployeeIDP, UserIDP };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_ApproveDealerEmployee", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (SqlException sqlEx)
            { return false; }
            catch (Exception ex)
            { return false; }
            return true;
        }


        public List<PDMS_DealerEmployee> GetDealerEmployeeManageRole(int DealerID, string AadhaarCardNo, string Name)
        {
            List<PDMS_DealerEmployee> EMP = new List<PDMS_DealerEmployee>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter AadhaarCardNoP = provider.CreateParameter("AadhaarCardNo", string.IsNullOrEmpty(AadhaarCardNo) ? null : AadhaarCardNo, DbType.String);
                DbParameter NameP = provider.CreateParameter("Name", Name, DbType.String);
                DbParameter[] Params = new DbParameter[3] { DealerIDP, AadhaarCardNoP,  NameP };

                using (DataSet DataSet = provider.Select("ZDMS_GetDealerEmployeeManageRole", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            EMP.Add(new PDMS_DealerEmployee()
                            {
                                DealerEmployeeID = Convert.ToInt32(dr["DealerEmployeeID"]),
                                Name = Convert.ToString(dr["Name"]),
                                FatherName = Convert.ToString(dr["FatherName"]),
                                ContactNumber = Convert.ToString(dr["ContactNumber"]),
                                Email = Convert.ToString(dr["EmailID"]),
                                State = new PDMS_State() { State = Convert.ToString(dr["State"]) },
                                District = new PDMS_District() { District = Convert.ToString(dr["District"]) },
                                Location = Convert.ToString(dr["Location"]),
                                AadhaarCardNo = Convert.ToString(dr["AadhaarCardNo"]),
                                PANNo = Convert.ToString(dr["PANNo"])                               
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return EMP;
        }

        public Boolean InsertDealerEmployeeRole(PDMS_DealerEmployeeRole Emp, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter DealerEmployeeID = provider.CreateParameter("DealerEmployeeID", Emp.DealerEmployeeID, DbType.Int32);
                DbParameter DealerID = provider.CreateParameter("DealerID", Emp.Dealer.DealerID, DbType.Int32);
                DbParameter OfficeCodeID = provider.CreateParameter("OfficeCodeID", Emp.Dealer.DealerOffice.OfficeID, DbType.Int32);
                DbParameter DateOfJoining = provider.CreateParameter("DateOfJoining", Emp.DateOfJoining, DbType.DateTime);
                DbParameter SAPEmpCode = provider.CreateParameter("SAPEmpCode", Emp.SAPEmpCode, DbType.String);
                DbParameter DealerDepartmentID = provider.CreateParameter("DealerDepartmentID", Emp.DealerDepartment == null ? (int?)null : Emp.DealerDepartment.DealerDepartmentID, DbType.Int32);
                DbParameter DealerDesignationID = provider.CreateParameter("DealerDesignationID", Emp.DealerDesignation == null ? (int?)null : Emp.DealerDesignation.DealerDesignationID, DbType.Int32);
                DbParameter ReportingTo = provider.CreateParameter("ReportingTo", Emp.ReportingTo == null ? (int?)null : Emp.ReportingTo.DealerEmployeeID, DbType.Int32); 
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                //DbParameter LoginUserName = provider.CreateParameter("LoginUserName", Emp.LoginUserName, DbType.String);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    DbParameter[] Params = new DbParameter[9] { DealerEmployeeID, DealerID, OfficeCodeID, DateOfJoining, SAPEmpCode, DealerDepartmentID, DealerDesignationID, ReportingTo, UserIDP };
                    provider.Insert("ZDMS_InsertDealerEmployeeRole", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Dealer", "InsertOrUpdateDealerEmployee", ex);
                return false;
            }
            return true;
        }
        public List<PDMS_DealerEmployeeRole> GetDealerEmployeeRole(int? DealerEmployeeRoleID, int? DealerEmployeeID, int? DealerID, Boolean? IsActive)
        {
            List<PDMS_DealerEmployeeRole> EMP = new List<PDMS_DealerEmployeeRole>();
            try
            {
                DbParameter DealerEmployeeRoleIDP = provider.CreateParameter("DealerEmployeeRoleID", DealerEmployeeRoleID, DbType.Int32);
                DbParameter DealerEmployeeIDP = provider.CreateParameter("DealerEmployeeID", DealerEmployeeID, DbType.Int32);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
                DbParameter[] Params = new DbParameter[4] { DealerEmployeeRoleIDP, DealerEmployeeIDP, DealerIDP, IsActiveP };

                using (DataSet DataSet = provider.Select("ZDMS_GetDealerEmployeeRole", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            EMP.Add(new PDMS_DealerEmployeeRole()
                            {
                                DealerEmployeeRoleID = Convert.ToInt64(dr["DealerEmployeeRoleID"]),
                                DealerEmployeeID = Convert.ToInt32(dr["DealerEmployeeID"]),
                                DealerEmployee = new PDMS_DealerEmployee() { Name = Convert.ToString(dr["Name"]) },
                                Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(dr["DealerID"]), DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) },
                                DealerOffice = new PDMS_DealerOffice() { OfficeID = Convert.ToInt32(dr["DealerOfficeID"]), OfficeName = Convert.ToString(dr["OfficeName"]) },
                                DealerDesignation = new PDMS_DealerDesignation() { DealerDesignationID = Convert.ToInt32(dr["DealerDesignationID"]), DealerDesignation = Convert.ToString(dr["DealerDesignation"]) },
                                DealerDepartment = new PDMS_DealerDepartment() { DealerDepartmentID = Convert.ToInt32(dr["DealerDepartmentID"]), DealerDepartment = Convert.ToString(dr["DealerDepartment"]) },
                                ReportingTo = DBNull.Value == dr["ReportingToID"]?null: new PDMS_DealerEmployee() { DealerEmployeeID = Convert.ToInt32(dr["ReportingToID"]), Name = Convert.ToString(dr["ReportingToName"]) },
                                DateOfJoining = Convert.ToDateTime(dr["DateOfJoining"]),
                                DateOfLeaving = DBNull.Value== dr["DateOfLeaving"]?(DateTime?)null: Convert.ToDateTime(dr["DateOfLeaving"]),
                                WishToLeave = DBNull.Value == dr["WishToLeave"] ? (Boolean?)null : Convert.ToBoolean(dr["WishToLeave"]),
                                IsActive = Convert.ToBoolean(dr["IsActive"]),
                                SAPEmpCode = Convert.ToString(dr["SAPEmpCode"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return EMP;
        }
        public List<PDMS_DealerEmployee> GetDealerEmployeeManageLeaving(int DealerID,  string Name)
        {
            List<PDMS_DealerEmployee> EMP = new List<PDMS_DealerEmployee>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32); 
                DbParameter NameP = provider.CreateParameter("Name", Name, DbType.String);
                DbParameter[] Params = new DbParameter[2] { DealerIDP, NameP };

                using (DataSet DataSet = provider.Select("ZDMS_GetDealerEmployeeManageLeaving", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            EMP.Add(new PDMS_DealerEmployee()
                            {
                                DealerEmployeeID = Convert.ToInt32(dr["DealerEmployeeID"]),
                                Name = Convert.ToString(dr["Name"]),
                                FatherName = Convert.ToString(dr["FatherName"]),
                                ContactNumber = Convert.ToString(dr["ContactNumber"]),
                                Email = Convert.ToString(dr["EmailID"]),
                                State = new PDMS_State() { State = Convert.ToString(dr["State"]) },
                                District = new PDMS_District() { District = Convert.ToString(dr["District"]) },
                                Location = Convert.ToString(dr["Location"]),
                                AadhaarCardNo = Convert.ToString(dr["AadhaarCardNo"]),
                                PANNo = Convert.ToString(dr["PANNo"]),

                                DealerEmployeeRole = DBNull.Value == dr["DealerEmployeeRoleID"] ? null : new PDMS_DealerEmployeeRole()
                               {
                                   DealerEmployeeRoleID = Convert.ToInt64(dr["DealerEmployeeRoleID"]),
                                   Dealer = DBNull.Value == dr["DealerID"] ? null : new PDMS_Dealer() { DealerID = Convert.ToInt32(dr["DealerID"]), DealerName = Convert.ToString(dr["DealerName"]), DealerCode = Convert.ToString(dr["DealerCode"]) },
                                   DealerOffice = DBNull.Value == dr["OfficeID"] ? null : new PDMS_DealerOffice() { OfficeID = Convert.ToInt32(dr["OfficeID"]), OfficeName = Convert.ToString(dr["OfficeName"]) },
                                   DateOfJoining =  Convert.ToDateTime(dr["DateOfJoining"]),
                                   DealerDepartment = DBNull.Value == dr["DealerDepartmentID"] ? null : new PDMS_DealerDepartment() { DealerDepartmentID = Convert.ToInt32(dr["DealerDepartmentID"]), DealerDepartment = Convert.ToString(dr["DealerDepartment"]) },
                                   DealerDesignation = DBNull.Value == dr["DealerDesignationID"] ? null : new PDMS_DealerDesignation() { DealerDesignationID = Convert.ToInt32(dr["DealerDesignationID"]), DealerDesignation = Convert.ToString(dr["DealerDesignation"]) },
                                   ReportingTo = DBNull.Value == dr["ReportingToID"] ? null : new PDMS_DealerEmployee() { DealerEmployeeID = Convert.ToInt32(dr["ReportingToID"]), Name = Convert.ToString(dr["ReportingToName"]) },
                                   DateOfLeaving = DBNull.Value == dr["DateOfLeaving"] ? (DateTime?)null : Convert.ToDateTime(dr["DateOfLeaving"]),
                                   WishToLeave = DBNull.Value == dr["WishToLeave"] ? (Boolean?)null : Convert.ToBoolean(dr["WishToLeave"]),
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
            return EMP;
        }
        public Boolean UpdateDealerEmployeeLeaving(PDMS_DealerEmployeeRole Emp, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter DealerEmployeeID = provider.CreateParameter("DealerEmployeeID", Emp.DealerEmployeeID, DbType.Int32);
                DbParameter DateOfLeaving = provider.CreateParameter("DateOfLeaving", Emp.DateOfLeaving, DbType.DateTime);
                DbParameter WishToLeave = provider.CreateParameter("WishToLeave", Emp.WishToLeave, DbType.Boolean);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    DbParameter[] Params = new DbParameter[4] { DealerEmployeeID, DateOfLeaving, WishToLeave, UserIDP };
                    provider.Insert("ZDMS_UpdateDealerEmployeeLeaving", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Dealer", "UpdateDealerEmployeeLeaving", ex);
                return false;
            }
            return true;
        }

        public Boolean UpdateDealerEmployeeRole(long DealerEmployeeRoleID, int OfficeCodeID,int? DealerDepartmentID,int? DealerDesignationID, int? ReportingTo, string SAPEmpCode, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter DealerEmployeeRoleIDP = provider.CreateParameter("DealerEmployeeRoleID", DealerEmployeeRoleID, DbType.Int64);
                DbParameter OfficeCodeIDP = provider.CreateParameter("OfficeCodeID", OfficeCodeID, DbType.Int32);

                DbParameter DealerDepartmentIDP = provider.CreateParameter("DealerDepartmentID", DealerDepartmentID, DbType.Int32);
                DbParameter DealerDesignationIDP = provider.CreateParameter("DealerDesignationID", DealerDesignationID, DbType.Int32);


                DbParameter ReportingToP = provider.CreateParameter("ReportingTo", ReportingTo, DbType.Int32);
                DbParameter SAPEmpCodeP = provider.CreateParameter("SAPEmpCode", string.IsNullOrEmpty(SAPEmpCode) ? null : SAPEmpCode, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);


                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    DbParameter[] Params = new DbParameter[7] { DealerEmployeeRoleIDP, OfficeCodeIDP, DealerDepartmentIDP, DealerDesignationIDP, ReportingToP, SAPEmpCodeP, UserIDP };
                    provider.Insert("ZDMS_UpdateDealerEmployeeRole", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Dealer", "InsertOrUpdateDealerEmployee", ex);
                return false;
            }
            return true;
        }


        public List<PDMS_DealerEmployee> GetDealerEmployeeByDealerID(int? DealerID, string AadhaarCardNo, int? DistrictID, string Name, string SAPEmpCode)
        {
            List<PDMS_DealerEmployee> EMP = new List<PDMS_DealerEmployee>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter AadhaarCardNoP = provider.CreateParameter("AadhaarCardNo", string.IsNullOrEmpty(AadhaarCardNo) ? null : AadhaarCardNo, DbType.String); 
                DbParameter DistrictIDP = provider.CreateParameter("DistrictID", DistrictID, DbType.Int32);
                DbParameter NameP = provider.CreateParameter("Name", Name, DbType.String);
                DbParameter SAPEmpCodeP = provider.CreateParameter("SAPEmpCode", string.IsNullOrEmpty(SAPEmpCode) ? null : SAPEmpCode, DbType.String); 
                DbParameter[] Params = new DbParameter[5] { DealerIDP, AadhaarCardNoP, DistrictIDP, NameP, SAPEmpCodeP };

                using (DataSet DataSet = provider.Select("ZDMS_GetDealerEmployeeByDealerID", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            EMP.Add(new PDMS_DealerEmployee()
                            {
                                DealerEmployeeID = Convert.ToInt32(dr["DealerEmployeeID"]),
                                Name = Convert.ToString(dr["Name"]),
                                FatherName = Convert.ToString(dr["FatherName"]),
                                DOB = Convert.ToDateTime(dr["DOB"]),
                       //         ContactNumber = Convert.ToString(dr["ContactNumber"]),
                                Email = Convert.ToString(dr["EmailID"]),
                                State = new PDMS_State() { State = Convert.ToString(dr["State"]) },
                                District = new PDMS_District() { District = Convert.ToString(dr["District"]) },
                                Location = Convert.ToString(dr["Location"]),
                                AadhaarCardNo = Convert.ToString(dr["AadhaarCardNo"]),
                                PANNo = Convert.ToString(dr["PANNo"]),

                                BankName = Convert.ToString(dr["BankName"]),
                                AccountNo = Convert.ToString(dr["AccountNo"]),
                                IFSCCode = Convert.ToString(dr["IFSCCode"]),
                                TotalExperience = Convert.ToDecimal("0" + Convert.ToString(dr["TotalExperience"])),

                                DealerEmployeeRole = DBNull.Value == dr["DealerEmployeeRoleID"] ? null : new PDMS_DealerEmployeeRole()
                                {
                                    DealerEmployeeRoleID = Convert.ToInt64(dr["DealerEmployeeRoleID"]),
                                    Dealer = new PDMS_Dealer()
                                    {
                                        DealerID = Convert.ToInt32(dr["DealerID"]),
                                        DealerCode = Convert.ToString(dr["DealerCode"]),
                                        DealerName = Convert.ToString(dr["DealerName"]),
                                  //      State = Convert.ToString(dr["DealerState"]),
                                        // StateCode = Convert.ToString(dr["StateCode"])
                                    },
                                    DealerOffice = new PDMS_DealerOffice() { OfficeName = Convert.ToString(dr["OfficeName"]) },
                                    DealerDepartment = new PDMS_DealerDepartment() { DealerDepartment = Convert.ToString(dr["DealerDepartment"]) },
                                    DealerDesignation = new PDMS_DealerDesignation() { DealerDesignation = Convert.ToString(dr["DealerDesignation"]) },
                                    ReportingTo = DBNull.Value == dr["ReportingToID"] ? null : new PDMS_DealerEmployee() { DealerEmployeeID = Convert.ToInt32(dr["ReportingToID"]), Name = Convert.ToString(dr["ReportingToName"]) },
                                    DateOfLeaving = DBNull.Value == dr["DateOfLeaving"] ? (DateTime?)null : Convert.ToDateTime(dr["DateOfLeaving"]),
                                    DateOfJoining = Convert.ToDateTime(dr["DateOfJoining"]),
                                    IsActive = Convert.ToBoolean(dr["IsActive"]),
                                    SAPEmpCode = Convert.ToString(dr["SAPEmpCode"])
                                },
                                IsAjaxHPApproved = Convert.ToBoolean(dr["IsAjaxHPApproved"]),
                          //      CreatedBy = new PUser() { ContactName = Convert.ToString(dr["ContactName"]), UserID = Convert.ToInt32(dr["UserID"]) }
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return EMP;
        }

        public void LoadDealerDDL(DropDownList ddl)
        { 
            ddl.DataValueField = "DID";
            ddl.DataTextField = "CodeWithName";
            ddl.DataSource = PSession.User.Dealer;
            ddl.DataBind(); 
           // ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}
