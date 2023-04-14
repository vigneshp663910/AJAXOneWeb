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
using System.Threading.Tasks;
using System.Transactions;

namespace Business
{
    public class BMachineOperator
    {
        private IDataAccess provider;
        public BMachineOperator()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PMachineOperator> GetMachineOperatorDetailsForApproval()
        {
            string endPoint = "Operator/GetApprovalDetails";
            return JsonConvert.DeserializeObject<List<PMachineOperator>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PMachineOperator> GetMachineOperatorDetailsManage(string AadhaarCardNo, string DLNumber, string Name)
        {
            string endPoint = "Operator/Manage?AadhaarCardNo=" + AadhaarCardNo + "&DLNumber=" + DLNumber + "&Name=" + Name;
            return JsonConvert.DeserializeObject<List<PMachineOperator>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PMachineOperator GetMachineOperatorDetailsByID(long MachineOperatorDetailsID)
        {
            string endPoint = "Operator/DetailsByID?MachineOperatorDetailsID=" + MachineOperatorDetailsID;
            return JsonConvert.DeserializeObject<PMachineOperator>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PMachineOperatorProductTypes> GetMachineOperatorProductTypesByID(long MachineOperatorDetailsID)
        {
            string endPoint = "Operator/ProductTypesByID?MachineOperatorDetailsID=" + MachineOperatorDetailsID;
            return JsonConvert.DeserializeObject<List<PMachineOperatorProductTypes>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PMachineOperatorAttachedFile GetMachineOperatorAttachedFile(long AttachedFileID)
        {
            string endPoint = "Operator/AttachedFile?AttachedFileID=" + AttachedFileID;
            return JsonConvert.DeserializeObject<PMachineOperatorAttachedFile>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PAttachedFile GetAttachedFileCustomerForDownload(string DocumentName)
        {
            string endPoint = "Operator/AttachedFileForDownload?DocumentName=" + DocumentName;
            return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
        }
        public DataTable GetMachineOperatorDetailsInExcel(string AadhaarCardNo, string DLNumber, string Name, int? PageIndex = null, int? PageSize = null)
        {
            DataTable dtDealerOperatorDetails = new DataTable();
            try
            {
                DbParameter AadhaarCardNoP = provider.CreateParameter("AadhaarCardNo", AadhaarCardNo, DbType.String);
                DbParameter DLNumberP = provider.CreateParameter("DLNumber", DLNumber, DbType.String);
                DbParameter NameP = provider.CreateParameter("Name", Name, DbType.String);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);
                DbParameter[] Params = new DbParameter[5] { AadhaarCardNoP, DLNumberP, NameP, PageIndexP, PageSizeP };

                using (DataSet DataSet = provider.Select("GetMachineOperatorDetailsInExcel", Params))
                {
                    if (DataSet != null)
                    {
                        dtDealerOperatorDetails = DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDealerOperatorDetails;
        }
        //public long InsertOrUpdateMachineOperatorDetails(PMachineOperator OP)
        //{
        //    TraceLogger.Log(DateTime.Now);
        //    long EmployeeID = 0;
        //    try
        //    {
        //        DbParameter DealerEmployeeID = provider.CreateParameter("MachineOperatorDetailsID", OP.MachineOperatorDetailsID, DbType.Int64);
        //        DbParameter Name = provider.CreateParameter("Name", OP.Name.ToUpper(), DbType.String);
        //        DbParameter FatherName = provider.CreateParameter("FatherName", OP.FatherName.ToUpper(), DbType.String);
        //        DbParameter DOB = provider.CreateParameter("DOB", OP.DOB, DbType.DateTime);
        //        DbParameter ContactNumber = provider.CreateParameter("ContactNumber", OP.ContactNumber, DbType.String);
        //        DbParameter ContactNumber1 = provider.CreateParameter("ContactNumber1", OP.ContactNumber1, DbType.String);
        //        DbParameter DepartmentID = provider.CreateParameter("DepartmentID", OP.Department == null ? (int?)null : OP.Department.DealerDepartmentID, DbType.Int32);
        //        DbParameter DesignationID = provider.CreateParameter("DesignationID", OP.Designation == null ? (int?)null : OP.Designation.DealerDesignationID, DbType.Int32);
        //        DbParameter ReportingToID = provider.CreateParameter("ReportingToID", OP.ReportingTo == null ? (int?)null : OP.ReportingTo.DealerEmployeeID, DbType.Int32);
        //        DbParameter Email = provider.CreateParameter("Email", OP.Email, DbType.String);
        //        DbParameter Address = provider.CreateParameter("Address", OP.Address.ToUpper(), DbType.String);
        //        DbParameter StateID = provider.CreateParameter("StateID", OP.State == null ? (int?)null : OP.State.StateID, DbType.Int32);
        //        DbParameter DistrictID = provider.CreateParameter("DistrictID", OP.District == null ? (int?)null : OP.District.DistrictID, DbType.Int32);
        //        DbParameter TehsilID = provider.CreateParameter("TehsilID", OP.Tehsil == null ? (int?)null : OP.Tehsil.TehsilID, DbType.Int32);
        //        DbParameter Village = provider.CreateParameter("Village", OP.Village, DbType.String);
        //        DbParameter BloodGroupID = provider.CreateParameter("BloodGroupID", OP.BloodGroup == null ? (int?)null : OP.BloodGroup.BloodGroupID, DbType.Int32);
        //        DbParameter EmergencyContactNumber = provider.CreateParameter("EmergencyContactNumber", OP.EmergencyContactNumber, DbType.String);
        //        DbParameter Location = provider.CreateParameter("Location", OP.Location, DbType.String);
        //        DbParameter AadhaarCardNo = provider.CreateParameter("AadhaarCardNo", OP.AadhaarCardNo, DbType.String);
        //        DbParameter EqucationalQualificationID = provider.CreateParameter("EqucationalQualificationID", OP.EqucationalQualification == null ? (int?)null : OP.EqucationalQualification.EqucationalQualificationID, DbType.Int64);
        //        DbParameter TotalExperience = provider.CreateParameter("TotalExperience", OP.TotalExperience, DbType.Decimal);
        //        DbParameter PANNo = provider.CreateParameter("PANNo", OP.PANNo.ToUpper(), DbType.String);
        //        DbParameter BankName = provider.CreateParameter("BankName", OP.BankName.ToUpper(), DbType.String);
        //        DbParameter AccountNo = provider.CreateParameter("AccountNo", OP.AccountNo, DbType.String);
        //        DbParameter IFSCCode = provider.CreateParameter("IFSCCode", OP.IFSCCode.ToUpper(), DbType.String);
        //        DbParameter DLNumber = provider.CreateParameter("DLNumber", OP.DLNumber.ToUpper(), DbType.String);
        //        DbParameter DLIssueDate = provider.CreateParameter("DLIssueDate", OP.DLIssueDate, DbType.DateTime);
        //        DbParameter DLIssueingOffice = provider.CreateParameter("DLIssueingOffice", OP.DLIssueingOffice.ToUpper(), DbType.String);
        //        DbParameter DLExpiryDate = provider.CreateParameter("DLExpiryDate", OP.DLExpiryDate, DbType.DateTime);
        //        DbParameter DLFor = provider.CreateParameter("DLFor", OP.DLFor.ToUpper(), DbType.String);

        //        DbParameter UserIDP = provider.CreateParameter("UserID", OP.UserID, DbType.Int32);
        //        DbParameter OutValueDParam = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));

        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            DbParameter PhotoID = provider.CreateParameter("PhotoID", OP.Photo.AttachedFileID != 0 ? OP.Photo.AttachedFileID : InsertOrUpdateMachineOperatorAttachedFile(OP.Photo), DbType.Int64);
        //            DbParameter AdhaarCardCopyFrontSideID = provider.CreateParameter("AdhaarCardCopyFrontSideID", OP.AdhaarCardCopyFrontSide.AttachedFileID != 0 ? OP.AdhaarCardCopyFrontSide.AttachedFileID : InsertOrUpdateMachineOperatorAttachedFile(OP.AdhaarCardCopyFrontSide), DbType.Int64);
        //            DbParameter AdhaarCardCopyBackSideID = provider.CreateParameter("AdhaarCardCopyBackSideID", OP.AdhaarCardCopyBackSide.AttachedFileID != 0 ? OP.AdhaarCardCopyBackSide.AttachedFileID : InsertOrUpdateMachineOperatorAttachedFile(OP.AdhaarCardCopyBackSide), DbType.Int64);
        //            DbParameter PANCardCopyID = provider.CreateParameter("PANCardCopyID", OP.PANCardCopy.AttachedFileID != 0 ? OP.PANCardCopy.AttachedFileID : InsertOrUpdateMachineOperatorAttachedFile(OP.PANCardCopy), DbType.Int64);
        //            DbParameter ChequeCopyID = provider.CreateParameter("ChequeCopyID", OP.ChequeCopy.AttachedFileID != 0 ? OP.ChequeCopy.AttachedFileID : InsertOrUpdateMachineOperatorAttachedFile(OP.ChequeCopy), DbType.Int64);
        //            DbParameter DLFrontSideID = provider.CreateParameter("DLFrontSideID", OP.DLFrontSide.AttachedFileID != 0 ? OP.DLFrontSide.AttachedFileID : InsertOrUpdateMachineOperatorAttachedFile(OP.DLFrontSide), DbType.Int64);
        //            DbParameter DLBackSideID = provider.CreateParameter("DLBackSideID", OP.DLBackSide.AttachedFileID != 0 ? OP.DLBackSide.AttachedFileID : InsertOrUpdateMachineOperatorAttachedFile(OP.DLBackSide), DbType.Int64);

        //            DbParameter[] Params = new DbParameter[39] { DealerEmployeeID, Name, FatherName, PhotoID, DOB, ContactNumber,ContactNumber1, Email, Address, StateID, DistrictID, TehsilID, Village,BloodGroupID,EmergencyContactNumber
        //             ,Location,AadhaarCardNo,AdhaarCardCopyFrontSideID,AdhaarCardCopyBackSideID,EqucationalQualificationID,TotalExperience,PANNo,PANCardCopyID
        //             ,BankName,AccountNo,IFSCCode,DepartmentID,DesignationID,ReportingToID,DLNumber,DLIssueDate,DLIssueingOffice,DLExpiryDate,DLFor,ChequeCopyID,DLFrontSideID,DLBackSideID,UserIDP,OutValueDParam};

        //            provider.Insert("InsertOrUpdateMachineOperatorDetails", Params);
        //            OP.MachineOperatorDetailsID = Convert.ToInt64(OutValueDParam.Value);
        //            InsertOrUpdateMachineOperatorProductTypes(OP);
        //            scope.Complete();
        //        }
        //        TraceLogger.Log(DateTime.Now);
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessageService("BMachineOperator", "InsertOrUpdateMachineOperatorDetails", ex);
        //        return 0;
        //    }
        //    return EmployeeID;
        //}

        //public Boolean ApproveMachineOperatorDetails(long MachineOperatorDetailsID, int UserID)
        //{
        //    try
        //    {
        //        DbParameter MachineOperatorDetailsIDP = provider.CreateParameter("MachineOperatorDetailsID", MachineOperatorDetailsID, DbType.Int32);
        //        DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //        DbParameter[] Params = new DbParameter[2] { MachineOperatorDetailsIDP, UserIDP };
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            provider.Insert("ApproveMachineOperatorDetails", Params);
        //            scope.Complete();
        //        }
        //        TraceLogger.Log(DateTime.Now);
        //    }
        //    catch (SqlException sqlEx)
        //    { return false; }
        //    catch (Exception ex)
        //    { return false; }
        //    return true;
        //}



        //public List<PMachineOperator> GetMachineOperatorDetailsManage(string AadhaarCardNo, string DLNumber, string Name, int? DealerDepartmentID, int? DealerDesignationID)
        //{
        //    List<PMachineOperator> MOPs = new List<PMachineOperator>();
        //    try
        //    {
        //        DbParameter AadhaarCardNoP = provider.CreateParameter("AadhaarCardNo", string.IsNullOrEmpty(AadhaarCardNo) ? null : AadhaarCardNo, DbType.String);
        //        DbParameter DLNumberP = provider.CreateParameter("DLNumber", DLNumber, DbType.String);
        //        DbParameter NameP = provider.CreateParameter("Name", Name, DbType.String);
        //        DbParameter DealerDepartmentIDP = provider.CreateParameter("DepartmentID", DealerDepartmentID, DbType.Int32);
        //        DbParameter DealerDesignationIDP = provider.CreateParameter("DesignationID", DealerDesignationID, DbType.Int32);
        //        DbParameter[] Params = new DbParameter[5] { AadhaarCardNoP, DLNumberP, NameP, DealerDepartmentIDP, DealerDesignationIDP };

        //        using (DataSet DataSet = provider.Select("GetMachineOperatorDetailsManage", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    MOPs.Add(new PMachineOperator()
        //                    {
        //                        MachineOperatorDetailsID = Convert.ToInt64(dr["MachineOperatorDetailsID"]),
        //                        Name = Convert.ToString(dr["Name"]),
        //                        FatherName = Convert.ToString(dr["FatherName"]),
        //                        DOB = DBNull.Value == dr["DOB"] ? (DateTime?)null : Convert.ToDateTime(dr["DOB"]),
        //                        ContactNumber = Convert.ToString(dr["ContactNumber"]),
        //                        Email = Convert.ToString(dr["EmailID"]),
        //                        State = new PDMS_State() { State = Convert.ToString(dr["State"]) },
        //                        District = new PDMS_District() { District = Convert.ToString(dr["District"]) },
        //                        Location = Convert.ToString(dr["Location"]),
        //                        AadhaarCardNo = Convert.ToString(dr["AadhaarCardNo"]),
        //                        PANNo = Convert.ToString(dr["PANNo"]),

        //                        BankName = Convert.ToString(dr["BankName"]),
        //                        AccountNo = Convert.ToString(dr["AccountNo"]),
        //                        IFSCCode = Convert.ToString(dr["IFSCCode"]),
        //                        TotalExperience = Convert.ToDecimal("0" + Convert.ToString(dr["TotalExperience"])),
        //                        Department = new PDMS_DealerDepartment() { DealerDepartment = Convert.ToString(dr["DealerDepartment"]) },
        //                        Designation = new PDMS_DealerDesignation() { DealerDesignation = Convert.ToString(dr["DealerDesignation"]) },
        //                        ReportingTo = DBNull.Value == dr["ReportingToID"] ? null : new PDMS_DealerEmployee() { DealerEmployeeID = Convert.ToInt32(dr["ReportingToID"]), Name = Convert.ToString(dr["ReportingToName"]) },
        //                        IsAjaxHPApproved = Convert.ToBoolean(dr["IsAjaxHPApproved"]),
        //                        DLNumber = dr["DLNumber"].ToString(),
        //                        CreatedBy = new PUser() { UserID = Convert.ToInt32(dr["CreatedBy"]), ContactName = dr["ContactName"].ToString() },
        //                        //       CreatedBy = new PUser() { ContactName = Convert.ToString(dr["ContactName"]), UserID = Convert.ToInt32(dr["UserID"]) }

        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return MOPs;
        //}
        //public List<PMachineOperator> GetMachineOperatorDetailsForApproval()
        //{
        //    List<PMachineOperator> EMP = new List<PMachineOperator>();
        //    try
        //    {
        //        using (DataSet DataSet = provider.Select("GetMachineOperatorDetailsForApproval"))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    EMP.Add(new PMachineOperator()
        //                    {
        //                        MachineOperatorDetailsID = Convert.ToInt64(dr["MachineOperatorDetailsID"]),
        //                        Name = Convert.ToString(dr["Name"]),
        //                        FatherName = Convert.ToString(dr["FatherName"]),
        //                        ContactNumber = Convert.ToString(dr["ContactNumber"]),
        //                        Email = Convert.ToString(dr["EmailID"]),
        //                        State = new PDMS_State() { State = Convert.ToString(dr["State"]) },
        //                        District = new PDMS_District() { District = Convert.ToString(dr["District"]) },
        //                        Location = Convert.ToString(dr["Location"]),
        //                        AadhaarCardNo = Convert.ToString(dr["AadhaarCardNo"]),
        //                        PANNo = Convert.ToString(dr["PANNo"]),
        //                        DLNumber = dr["DLNumber"].ToString(),
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return EMP;
        //}
        //public Int64 InsertOrUpdateMachineOperatorAttachedFile(PMachineOperatorAttachedFile AttachedFile)
        //{

        //    int success = 0;
        //    long AttachedFileID = 0;
        //    DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFile.AttachedFileID, DbType.Int64);
        //    DbParameter AttachedFileP = provider.CreateParameter("AttachedFile", AttachedFile.AttachedFile, DbType.Binary);
        //    DbParameter FileType = provider.CreateParameter("ContentType", AttachedFile.FileType, DbType.String);
        //    DbParameter FileName = provider.CreateParameter("FileName", AttachedFile.FileName, DbType.String);
        //    DbParameter FileSize = provider.CreateParameter("FileSize", AttachedFile.FileSize, DbType.Int32);
        //    DbParameter IsDeleted = provider.CreateParameter("IsDeleted", AttachedFile.IsDeleted, DbType.Boolean);
        //    DbParameter UserIDP = provider.CreateParameter("UserID", AttachedFile.UserID, DbType.Int32);
        //    DbParameter OutValueDParam = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
        //    DbParameter[] Params = new DbParameter[8] { AttachedFileIDP, AttachedFileP, FileType, FileName, FileSize, IsDeleted, UserIDP, OutValueDParam };
        //    try
        //    {
        //        success = provider.Insert("InsertOrUpdateMachineOperatorAttachedFile", Params);
        //        AttachedFileID = Convert.ToInt64(OutValueDParam.Value);
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BMachineOperator", "InsertOrUpdateMachineOperatorAttachedFile", sqlEx);
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BMachineOperator", " InsertOrUpdateMachineOperatorAttachedFile", ex);
        //        return 0;
        //    }
        //    return AttachedFileID;
        //}
        //public Int32 InsertOrUpdateMachineOperatorProductTypes(PMachineOperator OP)
        //{
        //    int success = 0;
        //    try
        //    {
        //        foreach (PMachineOperatorProductTypes pProductType in OP.ProductTypes)
        //        {
        //            DbParameter MachineOperatorProductTypesID = provider.CreateParameter("MachineOperatorProductTypesID", pProductType.MachineOperatorProductTypesID, DbType.Int64);
        //            DbParameter MachineOperatorDetailsIDP = provider.CreateParameter("MachineOperatorDetailsID", OP.MachineOperatorDetailsID, DbType.Int64);
        //            DbParameter ProductTypeID = provider.CreateParameter("ProductTypeID", pProductType.ProductType.ProductTypeID, DbType.Int32);
        //            DbParameter IsActive = provider.CreateParameter("IsActive", pProductType.IsActive, DbType.Boolean);
        //            DbParameter[] Params = new DbParameter[4] { MachineOperatorProductTypesID, MachineOperatorDetailsIDP, ProductTypeID, IsActive };
        //            success = provider.Insert("InsertOrUpdateMachineOperatorProductTypes", Params);
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BMachineOperator", "InsertOrUpdateMachineOperatorProductTypes", sqlEx);
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BMachineOperator", " InsertOrUpdateMachineOperatorProductTypes", ex);
        //        return 0;
        //    }
        //    return success;
        //}
        //public PMachineOperator GetMachineOperatorDetailsByID(long MachineOperatorDetailsID)
        //{
        //    PMachineOperator EMP = new PMachineOperator();
        //    try
        //    {
        //        DbParameter MachineOperatorDetailsIDP = provider.CreateParameter("MachineOperatorDetailsID", MachineOperatorDetailsID, DbType.Int32);
        //        DbParameter[] Params = new DbParameter[1] { MachineOperatorDetailsIDP };

        //        using (DataSet DataSet = provider.Select("GetMachineOperatorDetailsByID", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    EMP = new PMachineOperator()
        //                    {
        //                        MachineOperatorDetailsID = Convert.ToInt64(dr["MachineOperatorDetailsID"]),
        //                        Name = Convert.ToString(dr["Name"]),
        //                        FatherName = Convert.ToString(dr["FatherName"]),
        //                        DOB = DBNull.Value == dr["DOB"] ? (DateTime?)null : Convert.ToDateTime(dr["DOB"]),
        //                        ContactNumber = Convert.ToString(dr["ContactNumber"]),
        //                        ContactNumber1 = Convert.ToString(dr["ContactNumber1"]),
        //                        Email = Convert.ToString(dr["EmailID"]),
        //                        Address = Convert.ToString(dr["Address"]),
        //                        State = DBNull.Value == dr["StateID"] ? null : new PDMS_State() { StateID = Convert.ToInt32(dr["StateID"]), State = Convert.ToString(dr["State"]) },
        //                        District = DBNull.Value == dr["DistrictID"] ? null : new PDMS_District() { DistrictID = Convert.ToInt32(dr["DistrictID"]), District = Convert.ToString(dr["District"]) },
        //                        Tehsil = DBNull.Value == dr["TehsilID"] ? null : new PDMS_Tehsil() { TehsilID = Convert.ToInt32(dr["TehsilID"]), Tehsil = Convert.ToString(dr["Tehsil"]) },
        //                        Village = Convert.ToString(dr["Village"]),
        //                        Location = Convert.ToString(dr["Location"]),
        //                        AadhaarCardNo = Convert.ToString(dr["AadhaarCardNo"]),
        //                        EqucationalQualification = DBNull.Value == dr["EqucationalQualificationID"] ? null : new PDMS_EqucationalQualification() { EqucationalQualificationID = Convert.ToInt32(dr["EqucationalQualificationID"]), EqucationalQualification = Convert.ToString(dr["EqucationalQualification"]) },
        //                        TotalExperience = DBNull.Value == dr["TotalExperience"] ? (decimal?)null : Convert.ToDecimal(dr["TotalExperience"]),
        //                        PANNo = Convert.ToString(dr["PANNo"]),
        //                        BankName = Convert.ToString(dr["BankName"]),
        //                        AccountNo = Convert.ToString(dr["AccountNo"]),
        //                        IFSCCode = Convert.ToString(dr["IFSCCode"]),
        //                        EmergencyContactNumber = Convert.ToString(dr["EmergencyContactNumber"]),
        //                        BloodGroup = DBNull.Value == dr["BloodGroupID"] ? null : new PDMS_BloodGroup() { BloodGroupID = Convert.ToInt32(dr["BloodGroupID"]), BloodGroup = Convert.ToString(dr["BloodGroup"]) },
        //                        Photo = DBNull.Value == dr["APHAttachedFileID"] ? null : new PMachineOperatorAttachedFile() { AttachedFileID = Convert.ToInt32(dr["APHAttachedFileID"]), FileName = Convert.ToString(dr["APHFileName"]) },
        //                        AdhaarCardCopyFrontSide = DBNull.Value == dr["AAFAttachedFileID"] ? null : new PMachineOperatorAttachedFile() { AttachedFileID = Convert.ToInt32(dr["AAFAttachedFileID"]), FileName = Convert.ToString(dr["AAFFileName"]) },
        //                        AdhaarCardCopyBackSide = DBNull.Value == dr["AABAttachedFileID"] ? null : new PMachineOperatorAttachedFile() { AttachedFileID = Convert.ToInt32(dr["AABAttachedFileID"]), FileName = Convert.ToString(dr["AABFileName"]) },
        //                        PANCardCopy = DBNull.Value == dr["APAAttachedFileID"] ? null : new PMachineOperatorAttachedFile() { AttachedFileID = Convert.ToInt32(dr["APAAttachedFileID"]), FileName = Convert.ToString(dr["APAFileName"]) },
        //                        ChequeCopy = DBNull.Value == dr["ACAttachedFileID"] ? null : new PMachineOperatorAttachedFile() { AttachedFileID = Convert.ToInt32(dr["ACAttachedFileID"]), FileName = Convert.ToString(dr["ACFileName"]) },
        //                        DLNumber = Convert.ToString(dr["DLNumber"]),
        //                        DLIssueDate = DBNull.Value == dr["DLIssueDate"] ? (DateTime?)null : Convert.ToDateTime(dr["DLIssueDate"]),
        //                        DLIssueingOffice = Convert.ToString(dr["DLIssueingOffice"]),
        //                        DLExpiryDate = DBNull.Value == dr["DLExpiryDate"] ? (DateTime?)null : Convert.ToDateTime(dr["DLExpiryDate"]),
        //                        DLFor = Convert.ToString(dr["DLFor"]),
        //                        DLFrontSide = DBNull.Value == dr["ADFAttachedFileID"] ? null : new PMachineOperatorAttachedFile() { AttachedFileID = Convert.ToInt32(dr["ADFAttachedFileID"]), FileName = Convert.ToString(dr["ADFFileName"]) },
        //                        DLBackSide = DBNull.Value == dr["ADBAttachedFileID"] ? null : new PMachineOperatorAttachedFile() { AttachedFileID = Convert.ToInt32(dr["ADBAttachedFileID"]), FileName = Convert.ToString(dr["ADBFileName"]) },
        //                        Department = DBNull.Value == dr["DealerDepartmentID"] ? null : new PDMS_DealerDepartment() { DealerDepartmentID = Convert.ToInt32(dr["DealerDepartmentID"]), DealerDepartment = Convert.ToString(dr["DealerDepartment"]) },
        //                        Designation = DBNull.Value == dr["DealerDesignationID"] ? null : new PDMS_DealerDesignation() { DealerDesignationID = Convert.ToInt32(dr["DealerDesignationID"]), DealerDesignation = Convert.ToString(dr["DealerDesignation"]) },
        //                        ReportingTo = DBNull.Value == dr["ReportingToID"] ? null : new PDMS_DealerEmployee() { DealerEmployeeID = Convert.ToInt32(dr["ReportingToID"]), Name = Convert.ToString(dr["ReportingToName"]) },
        //                    };
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return EMP;
        //}
        //public List<PMachineOperatorProductTypes> GetMachineOperatorProductTypesByID(long MachineOperatorDetailsID)
        //{
        //    List<PMachineOperatorProductTypes> MOPTs = new List<PMachineOperatorProductTypes>();
        //    try
        //    {
        //        DbParameter MachineOperatorDetailsIDP = provider.CreateParameter("MachineOperatorDetailsID", MachineOperatorDetailsID, DbType.Int64);
        //        DbParameter[] Params = new DbParameter[1] { MachineOperatorDetailsIDP };

        //        using (DataSet DataSet = provider.Select("GetMachineOperatorProductTypesByID", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    PMachineOperatorProductTypes MOPT = new PMachineOperatorProductTypes()
        //                    {
        //                        MachineOperatorProductTypesID = Convert.ToInt64(dr["MachineOperatorProductTypesID"]),
        //                        MachineOperatorDetailsID = Convert.ToInt64(dr["MachineOperatorDetailsID"]),
        //                        ProductType = DBNull.Value == dr["ProductTypeID"] ? null : new PProductType() { ProductTypeID = Convert.ToInt32(dr["ProductTypeID"]), ProductType = Convert.ToString(dr["ProductType"]) },
        //                        IsActive = Convert.ToBoolean(dr["IsActive"])
        //                    };
        //                    MOPTs.Add(MOPT);
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return MOPTs;
        //}
        //public PMachineOperatorAttachedFile GetMachineOperatorAttachedFile(long AttachedFileID)
        //{
        //    PMachineOperatorAttachedFile EMP = new PMachineOperatorAttachedFile();
        //    try
        //    {
        //        DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
        //        DbParameter[] Params = new DbParameter[1] { AttachedFileIDP };

        //        using (DataSet DataSet = provider.Select("GetMachineOperatorAttachedFile", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    EMP = new PMachineOperatorAttachedFile()
        //                    {
        //                        AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]),
        //                        AttachedFile = (Byte[])(dr["AttachedFile"]),
        //                        FileType = Convert.ToString(dr["ContentType"]),
        //                        FileName = Convert.ToString(dr["FileName"]),
        //                        FileSize = Convert.ToInt32(dr["FileSize"])
        //                    };
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return EMP;
        //}
    }
}