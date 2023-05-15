using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business
{
    public class BTickets
    {
        private IDataAccess provider;
        public BTickets()
        {
            provider = new ProviderFactory().GetProvider();
        }

        public PAttachedFile GetAttachedFileTaskForDownload(string DocumentName)
        {
            string endPoint = "Task/AttachedFileForDownload?DocumentName=" + DocumentName;
            return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
        }
        public long insertTicketHeader(int TicketTypeID, int CategoryID, int? SubCategoryID, PUser User, string Subject, string TicketDescription, string MobileNo, string ContactName, Boolean Repeat, List<string> AttchedFile, int? ActualCreater, int? PriorityLevel, long? UATBy)
        {

            DbParameter TicketTypeIDParam;
            DbParameter CategoryIDParam;
            DbParameter SubCategoryIDParam;
            DbParameter UserIDP;
            DbParameter SubjectP;
            DbParameter TicketDescriptionParam;
            DbParameter RepeatParam;
            DbParameter TicketIDParam;
            DbParameter ActualCreaterParam;
            DbParameter MobileNoP;
            DbParameter ContactNameP;
            DbParameter PriorityLevelP;
            long HeaderID = 0;
            Int32 TTicketID = 1;
            int success = 0;



            TicketTypeIDParam = provider.CreateParameter("TicketTypeID", TicketTypeID, DbType.Int32);
            CategoryIDParam = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
            if (SubCategoryID != null)
                SubCategoryIDParam = provider.CreateParameter("SubCategoryID", SubCategoryID, DbType.Int32);
            else
                SubCategoryIDParam = provider.CreateParameter("SubCategoryID", DBNull.Value, DbType.Int32);


            RepeatParam = provider.CreateParameter("Repeat", Repeat, DbType.Boolean);
            UserIDP = provider.CreateParameter("CreatedBy", User.UserID, DbType.Int32);


            if (!string.IsNullOrEmpty(TicketDescription))
                TicketDescriptionParam = provider.CreateParameter("TicketDescription", TicketDescription, DbType.String);
            else
                TicketDescriptionParam = provider.CreateParameter("TicketDescription", DBNull.Value, DbType.String);

            if (!string.IsNullOrEmpty(Subject))
                SubjectP = provider.CreateParameter("Subject", Subject, DbType.String);
            else
                SubjectP = provider.CreateParameter("Subject", DBNull.Value, DbType.String);


            if (!string.IsNullOrEmpty(MobileNo))
                MobileNoP = provider.CreateParameter("MobileNo", MobileNo, DbType.String);
            else
                MobileNoP = provider.CreateParameter("MobileNo", DBNull.Value, DbType.String);

            if (!string.IsNullOrEmpty(ContactName))
                ContactNameP = provider.CreateParameter("ContactName", ContactName, DbType.String);
            else
                ContactNameP = provider.CreateParameter("ContactName", DBNull.Value, DbType.String);


            if (ActualCreater != null)
                ActualCreaterParam = provider.CreateParameter("ActualCreater", ActualCreater, DbType.Int32);
            else
                ActualCreaterParam = provider.CreateParameter("ActualCreater", DBNull.Value, DbType.Int32);

            if (PriorityLevel != null)
                PriorityLevelP = provider.CreateParameter("PriorityLevel", PriorityLevel, DbType.Int32);
            else
                PriorityLevelP = provider.CreateParameter("PriorityLevel", DBNull.Value, DbType.Int32);

            DbParameter UATByP = provider.CreateParameter("UATBy", UATBy, DbType.Int32);

            TicketIDParam = provider.CreateParameter("OutValue", TTicketID, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] TicketTypeParams = new DbParameter[13] { TicketTypeIDParam, CategoryIDParam, SubCategoryIDParam, UserIDP, SubjectP, TicketDescriptionParam, MobileNoP, ContactNameP, RepeatParam, ActualCreaterParam, PriorityLevelP, TicketIDParam, UATByP };

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("insertTicketHeader", TicketTypeParams);

                    if (success != 0)
                    {

                        HeaderID = Convert.ToInt64(TicketIDParam.Value);
                        foreach (string filename in AttchedFile)
                        {

                            insertForum(HeaderID, User.UserID, filename, filename);
                        }
                    }
                    scope.Complete();
                    if (AttchedFile.Count() != 0)
                    {
                        string path = ConfigurationManager.AppSettings["BasePath"] + "/File/" + PSession.User.UserName + "/";
                        if (Directory.Exists(path))
                        {
                            foreach (string file in Directory.GetFiles(path))
                            {
                                File.Delete(file);
                            }
                            // Directory.Delete(ConfigurationManager.AppSettings["BasePath"] + "/File/" + PSession.User.UserName + "/");
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "insertTicketHeader", sqlEx);
                HeaderID = 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", " insertTicketHeader", ex);
                HeaderID = 0;
            }
            return HeaderID;
        }

        public int InsertTicketItem(long TicketNo, int SubCategoryID, int Severity, string AssignerRemark, int AssignedTo, decimal ActualDuration, int UserId, List<string> AttchedFile, string SupportType)
        {

            DbParameter TicketNoHParam = provider.CreateParameter("TicketNo", TicketNo, DbType.Int64);
            DbParameter TicketNoTParam = provider.CreateParameter("TicketNo", TicketNo, DbType.Int64);
            DbParameter SubCategoryIDParam = provider.CreateParameter("SubCategoryID", SubCategoryID, DbType.Int32);
            DbParameter SeverityParam = provider.CreateParameter("Severity", Severity, DbType.Int32);
            DbParameter StatusHParam = provider.CreateParameter("Status", 2, DbType.Int32);
            DbParameter StatusIParam = provider.CreateParameter("Status", 2, DbType.Int32);
            DbParameter AssignerRemarkParam;
            DbParameter AssignedToParam = provider.CreateParameter("AssignedTo", AssignedTo, DbType.Int32);
            DbParameter AssignedByParam = provider.CreateParameter("AssignedBy", UserId, DbType.Int32);
            DbParameter ActualDurationParam = provider.CreateParameter("ActualDuration", ActualDuration, DbType.Decimal);
            DbParameter TicketIDParam;
            Int32 TTicketID = 1;
            int success = 0;

            if (!string.IsNullOrEmpty(AssignerRemark))
                AssignerRemarkParam = provider.CreateParameter("AssignerRemark", AssignerRemark, DbType.String);
            else
                AssignerRemarkParam = provider.CreateParameter("AssignerRemark", DBNull.Value, DbType.String);

            DbParameter SupportTypeP = provider.CreateParameter("SupportType", SupportType, DbType.String);

            TicketIDParam = provider.CreateParameter("OutValue", TTicketID, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] HeaderParams = new DbParameter[5] { TicketNoHParam, SubCategoryIDParam, SeverityParam, StatusHParam, SupportTypeP };
            DbParameter[] ItemParams = new DbParameter[7] { TicketNoTParam, AssignerRemarkParam, AssignedToParam, AssignedByParam, ActualDurationParam, StatusIParam, TicketIDParam };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketHeader", HeaderParams);
                    if (success != 0)
                    {
                        // insertTicketFile(TicketNo, AttchedFile, SourceFileName, DestFileName);
                        provider.Insert("InsertTicketItem", ItemParams);
                        success = Convert.ToInt32(TicketIDParam.Value);
                        if (!string.IsNullOrEmpty(AssignerRemark))
                            insertForum(TicketNo, UserId, AssignerRemark, "");
                        foreach (string filename in AttchedFile)
                        {
                            insertForum(TicketNo, UserId, filename, filename);
                        }

                    }
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "InsertTicketItem", sqlEx);
                success = 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "InsertTicketItem", ex);
                success = 0;
            }
            return success;
        }
        public Dictionary<string, int> getAttachedFiles(int TicketNO)
        {
            Dictionary<string, int> AttachedFiles = new Dictionary<string, int>();
            DbParameter TicketNOParam = provider.CreateParameter("TicketNO", TicketNO, DbType.Int32);
            DbParameter[] TicketTypeParams = new DbParameter[1] { TicketNOParam };
            using (DataSet Files = provider.Select("getAttachedFiles", TicketTypeParams))
            {
                if (Files != null)
                {
                    foreach (DataRow f in Files.Tables[0].Rows)
                    {
                        AttachedFiles.Add(Convert.ToString(f["FileName"]), Convert.ToInt32(f["AttachedFileID"]));
                    }
                }
            }
            return AttachedFiles;
        }
        public List<PTicketHeader> GetOpenTickets(int? HeaderId, int? TicketCategoryID, int? Status, long? CreatedBy, DateTime? CreatedDateFrom, DateTime? CreatedDateTo, long UserID, int? PageIndex, int? PageSize, out int RowCount)
        {
            DbParameter THeaderIdP;
            DbParameter TicketCategoryIDParam;
            DbParameter StatusParam;
            DbParameter CreatedByParam;
            DbParameter CreatedDateFromParam;
            DbParameter CreatedDateToParam;
            RowCount = 0;

            List<PTicketHeader> TicketsList = new List<PTicketHeader>();
            PTicketHeader pTickets;
            try
            {
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);

                if (HeaderId != null)
                    THeaderIdP = provider.CreateParameter("HeaderId", HeaderId, DbType.Int32);
                else
                    THeaderIdP = provider.CreateParameter("HeaderId", DBNull.Value, DbType.Int32);

                if (TicketCategoryID != null)
                    TicketCategoryIDParam = provider.CreateParameter("CategoryID", TicketCategoryID, DbType.Int32);
                else
                    TicketCategoryIDParam = provider.CreateParameter("CategoryID", DBNull.Value, DbType.Int32);

                if (Status != null)
                    StatusParam = provider.CreateParameter("Status", Status, DbType.Int32);
                else
                    StatusParam = provider.CreateParameter("Status", DBNull.Value, DbType.Int32);


                if (CreatedBy != null)
                    CreatedByParam = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int64);
                else
                    CreatedByParam = provider.CreateParameter("CreatedBy", DBNull.Value, DbType.Int64);


                if (CreatedDateFrom != null)
                    CreatedDateFromParam = provider.CreateParameter("CreatedDateFrom", CreatedDateFrom, DbType.DateTime);
                else
                    CreatedDateFromParam = provider.CreateParameter("CreatedDateFrom", DBNull.Value, DbType.DateTime);

                if (CreatedDateTo != null)
                    CreatedDateToParam = provider.CreateParameter("CreatedDateTo", CreatedDateTo, DbType.DateTime);
                else
                    CreatedDateToParam = provider.CreateParameter("CreatedDateTo", DBNull.Value, DbType.DateTime);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);

                // AssignedByParam = provider.CreateParameter("AssignedBy", UserId, DbType.Int32);

                DbParameter[] TicketTypeParams = new DbParameter[9] { THeaderIdP, TicketCategoryIDParam, StatusParam, CreatedByParam, CreatedDateFromParam, CreatedDateToParam, UserIDP, PageIndexP, PageSizeP };

                using (DataSet TicketTypeDataSet = provider.Select("GetOpenTickets", TicketTypeParams))
                {
                    if (TicketTypeDataSet != null)
                    {
                        foreach (DataRow TicketTypeRow in TicketTypeDataSet.Tables[0].Rows)
                        {

                            pTickets = new PTicketHeader();
                            pTickets.CreatedBy = new PUser()
                            {
                                UserID = Convert.ToInt32(TicketTypeRow["CreatedBy"]),
                                ContactName = Convert.ToString(TicketTypeRow["CreatedByEmployeeName"])
                            };
                            pTickets.CreatedOn = Convert.ToDateTime(TicketTypeRow["CreatedOn"]);
                            //  pTickets.Justification = Convert.ToString(TicketTypeRow["Justification"]);
                            pTickets.Repeat = Convert.ToBoolean(TicketTypeRow["Repeat"]);
                            pTickets.Subject = Convert.ToString(TicketTypeRow["Subject"]);
                            pTickets.Description = Convert.ToString(TicketTypeRow["Description"]);
                            pTickets.HeaderID = Convert.ToInt32(TicketTypeRow["HeaderID"]);

                            pTickets.Severity = TicketTypeRow["SeverityID"] == DBNull.Value ? null :
                            new PSeverity
                            {
                                SeverityID = Convert.ToInt32(TicketTypeRow["SeverityID"]),
                                Severity = Convert.ToString(TicketTypeRow["Severity"])
                            };
                            pTickets.Status = TicketTypeRow["StatusID"] == DBNull.Value ? null :
                            new PStatus
                            {
                                StatusID = Convert.ToInt32(TicketTypeRow["StatusID"]),
                                Status = Convert.ToString(TicketTypeRow["Status"])
                            };
                            pTickets.Category = TicketTypeRow["CategoryID"] == DBNull.Value ? null :
                            new PCategory
                            {
                                Category = Convert.ToString(TicketTypeRow["Category"]),
                                CategoryID = Convert.ToInt32(TicketTypeRow["CategoryID"])
                            };
                            pTickets.SubCategory = TicketTypeRow["SubCategoryID"] == DBNull.Value ? null :
                            new PSubCategory
                            {
                                CategoryId = Convert.ToInt32(TicketTypeRow["CategoryId"]),
                                SubCategory = Convert.ToString(TicketTypeRow["SubCategory"]),
                                SubCategoryID = Convert.ToInt32(TicketTypeRow["SubCategoryID"])
                            };
                            pTickets.Type = TicketTypeRow["TypeID"] == DBNull.Value ? null :
                            new PType
                            {
                                TypeID = Convert.ToInt32(TicketTypeRow["TypeID"]),
                                Type = Convert.ToString(TicketTypeRow["Type"])
                            };
                            pTickets.MobileNo = Convert.ToString(TicketTypeRow["MobileNo"]);
                            pTickets.ContactName = Convert.ToString(TicketTypeRow["ContactName"]);
                            pTickets.age = Convert.ToInt32(TicketTypeRow["Age"]);
                            TicketsList.Add(pTickets);
                            RowCount = Convert.ToInt32(TicketTypeRow["RowCount"]);
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return TicketsList;
        }
        public List<PTicketHeader> GetAssignedTickets(long? HeaderID, int? CategoryID, int? SubCategoryID, int? SeverityID, int UserID, int? PageIndex, int? PageSize, out int RowCount)
        {
            List<PTicketHeader> Header = new List<PTicketHeader>();
            PTicketHeader pHeader;
            RowCount = 0;
            try
            {
                DbParameter HeaderIDP = provider.CreateParameter("HeaderID", HeaderID, DbType.Int64);
                DbParameter CategoryIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
                DbParameter SubCategoryIDP = provider.CreateParameter("SubCategoryID", SubCategoryID, DbType.Int32);
                DbParameter SeverityIDP = provider.CreateParameter("SeverityID", SeverityID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);
                DbParameter[] Params = new DbParameter[7] { HeaderIDP, CategoryIDP, SubCategoryIDP, SeverityIDP, UserIDP, PageIndexP, PageSizeP };

                using (DataSet DS = provider.Select("GetAssignedTickets", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {
                            pHeader = new PTicketHeader();

                            pHeader.HeaderID = Convert.ToInt32(DR["HeaderID"]);
                            pHeader.Category = new PCategory { Category = Convert.ToString(DR["Category"]), CategoryID = Convert.ToInt32(DR["CategoryID"]) };
                            pHeader.SubCategory = DR["SubCategoryID"] == DBNull.Value ? null : new PSubCategory { CategoryId = Convert.ToInt32(DR["CategoryId"]), SubCategory = Convert.ToString(DR["SubCategory"]), SubCategoryID = Convert.ToInt32(DR["SubCategoryID"]) };
                            pHeader.CreatedBy = new PUser
                            {
                                UserID = Convert.ToInt32(DR["CreatedBy"]),
                                ContactName = Convert.ToString(DR["CreatedByEmployeeName"]),
                                //Department = new PDMS_DealerDepartment { DealerDepartment = Convert.ToString(DR["CreatedByDepartment"]) }
                            };
                            pHeader.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);
                            pHeader.Subject = Convert.ToString(DR["Subject"]);
                            pHeader.Description = Convert.ToString(DR["Description"]);
                            pHeader.Severity = DR["SeverityID"] == DBNull.Value ? null : new PSeverity { SeverityID = Convert.ToInt32(DR["SeverityID"]), Severity = Convert.ToString(DR["Severity"]) };
                            pHeader.Status = DR["HeaderStatusID"] == DBNull.Value ? null : new PStatus { StatusID = Convert.ToInt32(DR["HeaderStatusID"]), Status = Convert.ToString(DR["HeaderStatus"]) };
                            pHeader.MobileNo = Convert.ToString(DR["MobileNo"]);
                            pHeader.ContactName = Convert.ToString(DR["ContactName"]);
                            pHeader.age = Convert.ToInt32(DR["Age"]);
                            pHeader.TicketItem = new PTicketItem();
                            Header.Add(pHeader);
                            RowCount = Convert.ToInt32(DR["RowCount"]);
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            return Header;
        }
        public int UpdateReopenTicket(string TicketNo)
        {

            DbParameter TicketNoParam;
            int success = 0;
            TicketNoParam = provider.CreateParameter("TicketNo", TicketNo, DbType.String);
            DbParameter[] TicketTypeParams = new DbParameter[1] { TicketNoParam };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateReopenTicket", TicketTypeParams);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "UpdateReopenTicket", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "UpdateReopenTicket", ex);
            }
            return success;
        }

        //public long insertTicketFile(int TicketID, List<string> AttchedFile, string SourceFileName, string DestFileName)
        //{
        //    DbParameter AttchedFileParam;
        //    DbParameter AttchedFileID;
        //    Int32 TTicket = 1;
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            foreach (string filename in AttchedFile)
        //            {
        //                AttchedFileParam = provider.CreateParameter("AttchedFile", filename, DbType.String);
        //                DbParameter TicketIDP = provider.CreateParameter("TicketID", TicketID, DbType.Int64);
        //                AttchedFileID = provider.CreateParameter("OutValue", TTicket, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
        //                DbParameter[] AttchedFileParams = new DbParameter[3] { TicketIDP, AttchedFileParam, AttchedFileID };
        //                provider.Insert("insertAttchedFile", AttchedFileParams);
        //                string SourceFileName1 = SourceFileName + filename;
        //                int count = filename.Split('.').Count();
        //                string DestFileName1 = DestFileName + Convert.ToInt64(AttchedFileID.Value) + "." + filename.Split('.')[count - 1];
        //                File.Move(SourceFileName1, DestFileName1);
        //            }
        //            scope.Complete();
        //            if (AttchedFile.Count() != 0)
        //            {
        //                Directory.Delete(SourceFileName);
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return TicketID;
        //}

        public int insertTicketApprovalDetails(int EID, int HeaderId, int? Approver)
        {

            int success = 0;
            DbParameter EIDParam = provider.CreateParameter("SendBy", EID, DbType.Int32);
            DbParameter HeaderIdP = provider.CreateParameter("HeaderId", HeaderId, DbType.Int32);
            DbParameter ApproverParam = provider.CreateParameter("Approver", Approver, DbType.Int32);
            DbParameter[] Params = new DbParameter[3] { EIDParam, HeaderIdP, ApproverParam };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("insertTicketApprovalDetails", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "insertTicketApprovalDetails", sqlEx);
                success = 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "insertTicketApprovalDetails", ex);
                success = 0;
            }
            return success;
        }
        public List<PTicketHeader> GetTicketsForApproval(int? HeaderID, int? CategoryID, int? Type, string CreatedBy, DateTime? CreatedDateFrom, DateTime? CreatedDateTo, int loginUser, Boolean? Approved, int? UserTypeID)
        {
            DbParameter HeaderIDP;
            DbParameter CategoryIDParam;
            DbParameter TypeParam;
            DbParameter CreatedByParam;
            DbParameter CreatedDateFromParam;
            DbParameter CreatedDateToParam;
            DbParameter ApprovedParam;

            List<PTicketHeader> TicketsList = new List<PTicketHeader>();
            PTicketHeader pTickets;
            try
            {
                if (HeaderID != null)
                    HeaderIDP = provider.CreateParameter("HeaderID", HeaderID, DbType.Int32);
                else
                    HeaderIDP = provider.CreateParameter("HeaderID", DBNull.Value, DbType.Int32);

                if (CategoryID != null)
                    CategoryIDParam = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
                else
                    CategoryIDParam = provider.CreateParameter("CategoryID", DBNull.Value, DbType.Int32);

                if (Type != null)
                    TypeParam = provider.CreateParameter("Type", Type, DbType.Int32);
                else
                    TypeParam = provider.CreateParameter("Type", DBNull.Value, DbType.Int32);


                if (!string.IsNullOrEmpty(CreatedBy))
                    CreatedByParam = provider.CreateParameter("CreatedBy", CreatedBy, DbType.String);
                else
                    CreatedByParam = provider.CreateParameter("CreatedBy", DBNull.Value, DbType.String);


                if (CreatedDateFrom != null)
                    CreatedDateFromParam = provider.CreateParameter("CreatedDateFrom", CreatedDateFrom, DbType.DateTime);
                else
                    CreatedDateFromParam = provider.CreateParameter("CreatedDateFrom", DBNull.Value, DbType.DateTime);

                if (CreatedDateTo != null)
                    CreatedDateToParam = provider.CreateParameter("CreatedDateTo", CreatedDateTo, DbType.DateTime);
                else
                    CreatedDateToParam = provider.CreateParameter("CreatedDateTo", DBNull.Value, DbType.DateTime);

                DbParameter loginUserParam = provider.CreateParameter("loginUser", loginUser, DbType.Int32);
                if (Approved != null)
                    ApprovedParam = provider.CreateParameter("Approved", Approved, DbType.Boolean);
                else
                    ApprovedParam = provider.CreateParameter("Approved", DBNull.Value, DbType.Boolean);

                DbParameter UserTypeIDP = provider.CreateParameter("UserTypeID", UserTypeID, DbType.Int32);
                // AssignedByParam = provider.CreateParameter("AssignedBy", UserId, DbType.Int32);

                DbParameter[] TicketTypeParams = new DbParameter[9] { HeaderIDP, CategoryIDParam, TypeParam, CreatedByParam, CreatedDateFromParam, CreatedDateToParam, loginUserParam, ApprovedParam, UserTypeIDP };

                using (DataSet TicketTypeDataSet = provider.Select("GetTicketsForApproval", TicketTypeParams))
                {
                    if (TicketTypeDataSet != null)
                    {
                        foreach (DataRow TicketTypeRow in TicketTypeDataSet.Tables[0].Rows)
                        {
                            pTickets = new PTicketHeader();


                            pTickets.CreatedBy = new PUser
                            {
                                UserID = Convert.ToInt32(TicketTypeRow["CreatedBy"]),
                                ContactName = Convert.ToString(TicketTypeRow["CreatedByEmployeeName"])
                            };
                            pTickets.CreatedOn = Convert.ToDateTime(TicketTypeRow["CreatedOn"]);
                            // pTickets.Justification = Convert.ToString(TicketTypeRow["Justification"]);
                            pTickets.Repeat = Convert.ToBoolean(TicketTypeRow["Repeat"]);
                            pTickets.Description = Convert.ToString(TicketTypeRow["Description"]);
                            pTickets.HeaderID = Convert.ToInt32(TicketTypeRow["HeaderID"]);

                            pTickets.Severity = TicketTypeRow["Severity"] == DBNull.Value ? null :
                            new PSeverity
                            {
                                SeverityID = Convert.ToInt32(TicketTypeRow["SeverityID"]),
                                Severity = Convert.ToString(TicketTypeRow["Severity"])
                            };
                            pTickets.Status = TicketTypeRow["StatusID"] == DBNull.Value ? null :
                            new PStatus
                            {
                                StatusID = Convert.ToInt32(TicketTypeRow["StatusID"]),
                                Status = Convert.ToString(TicketTypeRow["Status"])
                            };
                            pTickets.Category = TicketTypeRow["CategoryID"] == DBNull.Value ? null :
                            new PCategory
                            {
                                Category = Convert.ToString(TicketTypeRow["Category"]),
                                CategoryID = Convert.ToInt32(TicketTypeRow["CategoryID"])
                            };
                            pTickets.SubCategory = TicketTypeRow["SubCategoryID"] == DBNull.Value ? null :
                            new PSubCategory
                            {
                                CategoryId = Convert.ToInt32(TicketTypeRow["CategoryId"]),
                                SubCategory = Convert.ToString(TicketTypeRow["SubCategory"]),
                                SubCategoryID = Convert.ToInt32(TicketTypeRow["SubCategoryID"])
                            };
                            pTickets.Type = TicketTypeRow["TypeID"] == DBNull.Value ? null :
                            new PType
                            {
                                TypeID = Convert.ToInt32(TicketTypeRow["TypeID"]),
                                Type = Convert.ToString(TicketTypeRow["Type"])
                            };
                            TicketsList.Add(pTickets);
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            {

            }
            return TicketsList;
        }
        public int UpdateTicketApproval(long EID, int TicketID, string Remark, Boolean Approved)
        {

            int success = 0;
            DbParameter EIDParam = provider.CreateParameter("EID", EID, DbType.Int32);
            DbParameter TicketNoParam = provider.CreateParameter("TicketId", TicketID, DbType.Int32);
            DbParameter RemarkParam = provider.CreateParameter("Remark", Remark, DbType.String);
            DbParameter ApprovedParam = provider.CreateParameter("Approved", Approved, DbType.Boolean);
            DbParameter[] Params = new DbParameter[4] { EIDParam, TicketNoParam, RemarkParam, ApprovedParam };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketApproval", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketApproval", sqlEx);
                success = 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketApproval", ex);
                success = 0;
            }
            return success;
        }
        public List<PTicketHeader> GetTicketsApprovaldetails(long? HeaderId, DateTime? CreatedDateFrom, DateTime? CreatedDateTo, int? CategoryID, int? SubCategoryID, int? SeverityID, long UserID, int? PageIndex, int? PageSize, out int RowCount)
        {
            List<PTicketHeader> Header = new List<PTicketHeader>();
            PTicketHeader pHeader;
            RowCount = 0;
            try
            {
                DbParameter HeaderIdParam = provider.CreateParameter("HeaderId", HeaderId, DbType.Int64);
                DbParameter DateFromParam = provider.CreateParameter("DateFrom", CreatedDateFrom, DbType.DateTime);
                DbParameter DateToParam = provider.CreateParameter("DateTo", CreatedDateTo, DbType.DateTime);
                DbParameter CategoryIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
                DbParameter SubCategoryIDP = provider.CreateParameter("SubCategoryID", SubCategoryID, DbType.Int32);
                DbParameter SeverityIDP = provider.CreateParameter("SeverityID", SeverityID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);

                DbParameter[] TicketTypeParams = new DbParameter[9] { HeaderIdParam, DateFromParam, DateToParam, CategoryIDP, SubCategoryIDP, SeverityIDP, UserIDP, PageIndexP, PageSizeP };

                using (DataSet DS = provider.Select("GetTicketsApprovaldetails", TicketTypeParams))
                {
                    if (DS != null)
                    {
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {
                            pHeader = new PTicketHeader();
                            pHeader.HeaderID = Convert.ToInt32(DR["HeaderID"]);
                            pHeader.Category = new PCategory { Category = Convert.ToString(DR["Category"]), CategoryID = Convert.ToInt32(DR["CategoryID"]) };
                            pHeader.SubCategory = DR["SubCategoryID"] == DBNull.Value ? null : new PSubCategory { CategoryId = Convert.ToInt32(DR["CategoryId"]), SubCategory = Convert.ToString(DR["SubCategory"]), SubCategoryID = Convert.ToInt32(DR["SubCategoryID"]) };
                            pHeader.CreatedBy = new PUser
                            {
                                UserID = Convert.ToInt32(DR["CreatedBy"]),
                                ContactName = Convert.ToString(DR["CreatedByEmployeeName"]),
                                //Department = new PDMS_DealerDepartment { DealerDepartment = Convert.ToString(DR["CreatedByDepartment"]) }
                            };
                            pHeader.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);
                            pHeader.Subject = Convert.ToString(DR["Subject"]);
                            pHeader.Description = Convert.ToString(DR["Description"]);
                            pHeader.Severity = DR["SeverityID"] == DBNull.Value ? null : new PSeverity { SeverityID = Convert.ToInt32(DR["SeverityID"]), Severity = Convert.ToString(DR["Severity"]) };
                            pHeader.Status = DR["HeaderStatusID"] == DBNull.Value ? null : new PStatus { StatusID = Convert.ToInt32(DR["HeaderStatusID"]), Status = Convert.ToString(DR["HeaderStatus"]) };
                            pHeader.MobileNo = Convert.ToString(DR["MobileNo"]);
                            pHeader.ContactName = Convert.ToString(DR["ContactName"]);
                            pHeader.age = Convert.ToInt32(DR["Age"]);
                            pHeader.TicketItem = new PTicketItem();
                            Header.Add(pHeader);
                            RowCount = Convert.ToInt32(DR["RowCount"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            {

            }
            return Header;
        }
        public int UpdateTicketStatus(int ItemID, int StatusId)
        {

            int success = 0;
            DbParameter ItemIDParam = provider.CreateParameter("ItemID", ItemID, DbType.Int32);
            DbParameter StatusIdParam = provider.CreateParameter("StatusId", StatusId, DbType.Int32);
            DbParameter[] Params = new DbParameter[2] { ItemIDParam, StatusIdParam };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketStatus", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketStatus", sqlEx);
                success = 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketStatus", ex);
                success = 0;
            }
            return success;
        }
        public List<PTicketHeader> GetInProgressTickets(long? HeaderId, int? CategoryID, int? SubCategoryID, int? SeverityID, int UserID, int? PageIndex, int? PageSize, out int RowCount)
        {
            List<PTicketHeader> Header = new List<PTicketHeader>();
            PTicketHeader pHeader;
            RowCount = 0;
            try
            {
                DbParameter HeaderIdP = provider.CreateParameter("HeaderID", HeaderId, DbType.Int64);
                DbParameter CategoryIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
                DbParameter SubCategoryIDP = provider.CreateParameter("SubCategoryID", SubCategoryID, DbType.Int32);
                DbParameter SeverityIDP = provider.CreateParameter("SeverityID", SeverityID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);

                DbParameter[] TicketTypeParams = new DbParameter[7] { HeaderIdP, CategoryIDP, SubCategoryIDP, SeverityIDP, UserIDP, PageIndexP, PageSizeP };

                using (DataSet DS = provider.Select("GetInProgressTickets", TicketTypeParams))
                {
                    if (DS != null)
                    {
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {
                            pHeader = new PTicketHeader();

                            pHeader.HeaderID = Convert.ToInt32(DR["HeaderID"]);
                            pHeader.Category = new PCategory { Category = Convert.ToString(DR["Category"]), CategoryID = Convert.ToInt32(DR["CategoryID"]) };
                            pHeader.SubCategory = DR["SubCategoryID"] == DBNull.Value ? null : new PSubCategory { CategoryId = Convert.ToInt32(DR["CategoryId"]), SubCategory = Convert.ToString(DR["SubCategory"]), SubCategoryID = Convert.ToInt32(DR["SubCategoryID"]) };
                            pHeader.Subject = Convert.ToString(DR["Subject"]);
                            pHeader.Description = Convert.ToString(DR["Description"]);
                            pHeader.CreatedBy = new PUser
                            {
                                UserID = Convert.ToInt32(DR["CreatedBy"]),
                                ContactName = Convert.ToString(DR["CreatedByEmployeeName"]),
                            };
                            pHeader.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);
                            pHeader.Severity = DR["SeverityID"] == DBNull.Value ? null : new PSeverity { SeverityID = Convert.ToInt32(DR["SeverityID"]), Severity = Convert.ToString(DR["Severity"]) };
                            pHeader.Status = DR["HeaderStatusID"] == DBNull.Value ? null : new PStatus { StatusID = Convert.ToInt32(DR["HeaderStatusID"]), Status = Convert.ToString(DR["HeaderStatus"]) };

                            pHeader.MobileNo = Convert.ToString(DR["MobileNo"]);
                            pHeader.ContactName = Convert.ToString(DR["ContactName"]);
                            pHeader.age = Convert.ToInt32(DR["Age"]);
                            pHeader.TicketItem = new PTicketItem();

                            Header.Add(pHeader);
                            RowCount = Convert.ToInt32(DR["RowCount"]);
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "GetInProgressTickets", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "GetInProgressTickets", ex);
            }
            return Header;
        }
        public int UpdateTicketResolvedStatus(int HeaderId, int ItemId, decimal Effort, int? ResolutionType, string Resolution, string SupportType, long UserId, Boolean NewTR, PTR TR, List<string> AttchedFile)
        {
            DbParameter ItemIdP;
            DbParameter EffortParam;
            DbParameter ResolutionTypeParam;
            DbParameter ResolutionParam;
            DbParameter SupportTypeP = provider.CreateParameter("SupportType", SupportType, DbType.String);
            int success = 0;
            ItemIdP = provider.CreateParameter("ItemId", ItemId, DbType.Int32);
            EffortParam = provider.CreateParameter("Effort", Effort, DbType.Decimal);

            if (ResolutionType != null)
            {
                ResolutionTypeParam = provider.CreateParameter("ResolutionType", ResolutionType, DbType.Int32);
            }
            else
            {
                ResolutionTypeParam = provider.CreateParameter("ResolutionType", DBNull.Value, DbType.Int32);
            }

            if (!string.IsNullOrEmpty(Resolution))
                ResolutionParam = provider.CreateParameter("Resolution", Resolution, DbType.String);
            else
                ResolutionParam = provider.CreateParameter("Resolution", DBNull.Value, DbType.String);


            DbParameter NewTRParam = provider.CreateParameter("NewTR", NewTR, DbType.Boolean);
            //if (!string.IsNullOrEmpty(TRNumber))
            //{
            //    TRNumberParam = provider.CreateParameter("TRNumber", TRNumber, DbType.String);
            //}
            //else
            //{
            //    TRNumberParam = provider.CreateParameter("TRNumber", DBNull.Value, DbType.String);
            //}
            DbParameter[] TicketTypeParams = new DbParameter[6] { ItemIdP,EffortParam,
                ResolutionTypeParam,ResolutionParam,NewTRParam,SupportTypeP };

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketResolvedStatus", TicketTypeParams);
                    if (success != 0)
                    {
                        if (NewTR)
                        {
                            DbParameter HeaderIdP = provider.CreateParameter("HeaderId", TR.TicketId, DbType.Int32);
                            DbParameter SubCategoryIDP = provider.CreateParameter("SubCategoryID", TR.SubCategory.SubCategoryID, DbType.Int32);
                            DbParameter TRNumberP = provider.CreateParameter("TRNumber", TR.TRNumber, DbType.String);
                            DbParameter PurposeP = provider.CreateParameter("Purpose", TR.Purpose, DbType.String);
                            DbParameter MailNoteP = provider.CreateParameter("MailNote", TR.MailNote, DbType.String);
                            DbParameter CreatedByP = provider.CreateParameter("CreatedBy", TR.CreatedBy.UserID, DbType.Int32);

                            DbParameter ChangeApprovedBy = provider.CreateParameter("ChangeApprovedBy", TR.ChangeApprovedBy == null ? (int?)null : TR.ChangeApprovedBy.UserID, DbType.Int32);
                            DbParameter ChangeApprovedOn = provider.CreateParameter("ChangeApprovedOn", TR.ChangeApprovedOn, DbType.DateTime);
                            DbParameter UATBy = provider.CreateParameter("UATBy", TR.UATBy == null ? (int?)null : TR.UATBy.UserID, DbType.Int32);
                            DbParameter UATOn = provider.CreateParameter("UATOn", TR.UATOn, DbType.DateTime);


                            DbParameter[] Params = new DbParameter[10] { HeaderIdP, TRNumberP, PurposeP, MailNoteP, CreatedByP, SubCategoryIDP, ChangeApprovedBy, ChangeApprovedOn, UATBy, UATOn };
                            success = provider.Insert("InsertTR", Params);
                        }
                        //  insertTicketFile(HeaderId, AttchedFile, SourceFileName, DestFileName);
                        if (!string.IsNullOrEmpty(Resolution))
                            insertForum(HeaderId, UserId, Resolution, "");
                        foreach (string filename in AttchedFile)
                        {
                            insertForum(HeaderId, UserId, filename, filename);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                success = 0;
                new FileLogger().LogMessage("BTickets", "UpdateTicketResolvedStatus", sqlEx);

            }
            catch (Exception ex)
            {
                success = 0;
                new FileLogger().LogMessage("BTickets", "UpdateTicketResolvedStatus", ex);
            }
            return success;
        }
        public List<PTicketHeader> GetTicketDetails(int? HeaderId, int? ItemId, int? CategoryID, int? SubCategoryID, int? Severity, int? Type, int? AssignedBy, int? AssignedTo, int? UserId, string HeaderStatus, DateTime? TicketFrom, DateTime? TicketTo, int? PageIndex, int? PageSize, out int RowCount)
        {
            DbParameter HeaderIdP;
            DbParameter ItemIdP;
            DbParameter CategoryIDP;
            DbParameter SubCategoryIDP;
            DbParameter SeverityP;
            DbParameter TypeP;
            DbParameter AssignedToP;
            DbParameter AssignedByP;
            DbParameter UserIdP;


            List<PTicketHeader> Header = new List<PTicketHeader>();
            PTicketHeader pHeader = null;
            RowCount = 0;
            try
            {
                if (HeaderId != null)
                    HeaderIdP = provider.CreateParameter("HeaderId", HeaderId, DbType.Int32);
                else
                    HeaderIdP = provider.CreateParameter("HeaderId", DBNull.Value, DbType.Int32);

                if (ItemId != null)
                    ItemIdP = provider.CreateParameter("ItemId", ItemId, DbType.Int32);
                else
                    ItemIdP = provider.CreateParameter("ItemId", DBNull.Value, DbType.Int32);


                if (CategoryID != null)
                    CategoryIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
                else
                    CategoryIDP = provider.CreateParameter("CategoryID", DBNull.Value, DbType.Int32);

                if (SubCategoryID != null)
                    SubCategoryIDP = provider.CreateParameter("SubCategoryID", SubCategoryID, DbType.Int32);
                else
                    SubCategoryIDP = provider.CreateParameter("SubCategoryID", DBNull.Value, DbType.Int32);


                if (Severity != null)
                    SeverityP = provider.CreateParameter("Severity", Severity, DbType.Int32);
                else
                    SeverityP = provider.CreateParameter("Severity", DBNull.Value, DbType.Int32);

                if (Type != null)
                    TypeP = provider.CreateParameter("Type", Type, DbType.Int32);
                else
                    TypeP = provider.CreateParameter("Type", DBNull.Value, DbType.Int32);

                if (AssignedTo != null)
                    AssignedToP = provider.CreateParameter("AssignedTo", AssignedTo, DbType.String);
                else
                    AssignedToP = provider.CreateParameter("AssignedTo", DBNull.Value, DbType.String);

                if (AssignedBy != null)
                    AssignedByP = provider.CreateParameter("AssignedBy", AssignedBy, DbType.Int32);
                else
                    AssignedByP = provider.CreateParameter("AssignedBy", DBNull.Value, DbType.Int32);
                if (UserId != null)
                    UserIdP = provider.CreateParameter("UserId", UserId, DbType.Int32);
                else
                    UserIdP = provider.CreateParameter("UserId", DBNull.Value, DbType.Int32);


                DbParameter HeaderStatusP;
                if (!string.IsNullOrEmpty(HeaderStatus))
                    HeaderStatusP = provider.CreateParameter("HeaderStatus", HeaderStatus, DbType.String);
                else
                    HeaderStatusP = provider.CreateParameter("HeaderStatus", DBNull.Value, DbType.String);

                DbParameter TicketFromP = provider.CreateParameter("TicketFrom", TicketFrom, DbType.DateTime);
                DbParameter TicketToP = provider.CreateParameter("TicketTo", TicketTo, DbType.DateTime);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);

                DbParameter[] TicketTypeParams = new DbParameter[14] { HeaderIdP, ItemIdP, CategoryIDP, SubCategoryIDP, SeverityP, TypeP, AssignedToP, AssignedByP, UserIdP, HeaderStatusP, TicketFromP, TicketToP, PageIndexP, PageSizeP };

                using (DataSet DS = provider.Select("GetTicketDetails", TicketTypeParams))
                {
                    if (DS != null)
                    {
                        Int32 cHeaderId = 0;
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {

                            if (cHeaderId != Convert.ToInt32(DR["HeaderID"]))
                            {
                                pHeader = new PTicketHeader();
                                pHeader.HeaderID = Convert.ToInt32(DR["HeaderID"]);
                                pHeader.Category = new PCategory { Category = Convert.ToString(DR["Category"]), CategoryID = Convert.ToInt32(DR["CategoryID"]) };
                                pHeader.SubCategory = DR["SubCategoryID"] == DBNull.Value ? null : new PSubCategory { CategoryId = Convert.ToInt32(DR["CategoryId"]), SubCategory = Convert.ToString(DR["SubCategory"]), SubCategoryID = Convert.ToInt32(DR["SubCategoryID"]) };
                                pHeader.CreatedBy = new PUser
                                {
                                    UserID = Convert.ToInt32(DR["CreatedBy"]),
                                    ContactName = Convert.ToString(DR["CreatedByEmployeeName"]),
                                    //Department = new PDMS_DealerDepartment { DealerDepartment = Convert.ToString(DR["CreatedByDepartment"]) }
                                };
                                pHeader.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);
                                pHeader.Subject = Convert.ToString(DR["Subject"]);
                                pHeader.Description = Convert.ToString(DR["Description"]);
                                pHeader.Severity = DR["SeverityID"] == DBNull.Value ? null : new PSeverity { SeverityID = Convert.ToInt32(DR["SeverityID"]), Severity = Convert.ToString(DR["Severity"]) };
                                pHeader.Status = DR["HeaderStatusID"] == DBNull.Value ? null : new PStatus { StatusID = Convert.ToInt32(DR["HeaderStatusID"]), Status = Convert.ToString(DR["HeaderStatus"]) };
                                cHeaderId = Convert.ToInt32(DR["HeaderID"]);
                                pHeader.ContactName = Convert.ToString(DR["ContactName"]);
                                pHeader.MobileNo = Convert.ToString(DR["MobileNo"]);
                                pHeader.TicketItems = new List<PTicketItem>();
                                pHeader.ApprovalDetails = new List<PTicketsApprovalDetails>();
                            }
                            if (!Header.Exists(s => s.HeaderID == cHeaderId))
                                Header.Add(pHeader);
                            RowCount = Convert.ToInt32(DR["RowCount"]);
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Header;
        }
        public List<PTicketHeader> GetTicketByID(int? HeaderId)
        {
            DbParameter HeaderIdP;

            List<PTicketHeader> Header = new List<PTicketHeader>();
            PTicketHeader pHeader = null;
            PTicketItem pItem;
            PTicketsApprovalDetails ApprovalDetails;
            try
            {
                if (HeaderId != null)
                    HeaderIdP = provider.CreateParameter("HeaderId", HeaderId, DbType.Int32);
                else
                    HeaderIdP = provider.CreateParameter("HeaderId", DBNull.Value, DbType.Int32);
                DbParameter[] TicketTypeParams = new DbParameter[1] { HeaderIdP };

                using (DataSet DS = provider.Select("GetTicketByID", TicketTypeParams))
                {
                    if (DS != null)
                    {
                        Int32 cHeaderId = 0;
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {

                            if (cHeaderId != Convert.ToInt32(DR["HeaderID"]))
                            {
                                pHeader = new PTicketHeader();
                                pHeader.HeaderID = Convert.ToInt32(DR["HeaderID"]);
                                pHeader.Category = new PCategory { Category = Convert.ToString(DR["Category"]), CategoryID = Convert.ToInt32(DR["CategoryID"]) };
                                pHeader.SubCategory = DR["SubCategoryID"] == DBNull.Value ? null : new PSubCategory { CategoryId = Convert.ToInt32(DR["CategoryId"]), SubCategory = Convert.ToString(DR["SubCategory"]), SubCategoryID = Convert.ToInt32(DR["SubCategoryID"]) };
                                pHeader.Type = DR["TypeID"] == DBNull.Value ? null : new PType { TypeID = Convert.ToInt32(DR["TypeID"]), Type = Convert.ToString(DR["Type"]) };
                                pHeader.CreatedBy = new PUser
                                {
                                    UserID = Convert.ToInt32(DR["CreatedBy"]),
                                    ContactName = Convert.ToString(DR["CreatedByEmployeeName"]),
                                    ContactNumber = Convert.ToString(DR["CreatedByContactNumber"]),
                                    Department = new PDMS_DealerDepartment { DealerDepartment = Convert.ToString(DR["CreatedByDepartment"]) }
                                };
                                pHeader.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);
                                pHeader.Subject = Convert.ToString(DR["Subject"]);
                                pHeader.Description = Convert.ToString(DR["Description"]);
                                pHeader.Repeat = Convert.ToBoolean(DR["Repeat"]);
                                pHeader.Severity = DR["SeverityID"] == DBNull.Value ? null : new PSeverity { SeverityID = Convert.ToInt32(DR["SeverityID"]), Severity = Convert.ToString(DR["Severity"]) };
                                pHeader.Status = DR["HeaderStatusID"] == DBNull.Value ? null : new PStatus { StatusID = Convert.ToInt32(DR["HeaderStatusID"]), Status = Convert.ToString(DR["HeaderStatus"]) };
                                cHeaderId = Convert.ToInt32(DR["HeaderID"]);
                                pHeader.PriorityLevel = DR["PriorityLevel"] == DBNull.Value ? (int?)null : Convert.ToInt32(DR["PriorityLevel"]);
                                pHeader.ContactName = Convert.ToString(DR["ContactName"]);
                                pHeader.MobileNo = Convert.ToString(DR["MobileNo"]);
                                pHeader.age = Convert.ToInt32(DR["age"]);
                                pHeader.TicketItems = new List<PTicketItem>();
                                pHeader.ApprovalDetails = new List<PTicketsApprovalDetails>();
                            }
                            if (DR["ItemID"] != DBNull.Value)
                            {
                                if (!pHeader.TicketItems.Exists(s => s.ItemID == (Convert.ToInt32(DR["ItemID"]))))
                                {
                                    pItem = new PTicketItem();
                                    pItem.ItemID = Convert.ToInt32(DR["ItemID"]);
                                    pItem.ActualDuration = DR["ActualDuration"] != DBNull.Value ? Convert.ToDecimal(DR["ActualDuration"]) : (decimal?)null;

                                    pItem.AssignedBy = DR["AssignedBy"] == DBNull.Value ? null : new PUser { UserID = Convert.ToInt32(DR["AssignedBy"]), ContactName = Convert.ToString(DR["AssignedByEmployeeName"]), ContactNumber = Convert.ToString(DR["AssignedByContactNumber"]) };
                                    pItem.AssignedOn = Convert.ToDateTime(DR["AssignedOn"]);
                                    pItem.AssignedTo = DR["AssignedTo"] == DBNull.Value ? null : new PUser { UserID = Convert.ToInt32(DR["AssignedTo"]), ContactName = Convert.ToString(DR["AssignedToEmployeeName"]), ContactNumber = Convert.ToString(DR["AssignedToContactNumber"]) };

                                    pItem.Effort = DR["Effort"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(DR["Effort"]);

                                    pItem.Resolution = Convert.ToString(DR["Resolution"]);
                                    pItem.AssignerRemark = Convert.ToString(DR["AssignerRemark"]);
                                    pItem.HeaderID = Convert.ToInt32(DR["HeaderID"]);
                                    pItem.ResolutionType = DR["ResolutionTypeID"] == DBNull.Value ? null : new PResolutionType { ResolutionType = Convert.ToString(DR["ResolutionType"]) };
                                    pItem.ResolvedOn = DR["ResolvedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["ResolvedOn"]);
                                    pItem.InProgressOn = DR["InProgressOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["InProgressOn"]);
                                    pItem.InActive = DR["InActive"] == DBNull.Value ? false : Convert.ToBoolean(DR["InActive"]);
                                    pItem.ItemStatus = DR["ItemStatus"] == DBNull.Value ? null :
                                    new PStatus
                                    {
                                        StatusID = Convert.ToInt32(DR["ItemStatusID"]),
                                        Status = Convert.ToString(DR["ItemStatus"])
                                    };
                                    pHeader.TicketItems.Add(pItem);
                                }
                            }
                            if (DR["TicketsApprovalId"] != DBNull.Value)
                            {
                                if (!pHeader.ApprovalDetails.Exists(s => s.Id == Convert.ToInt32(DR["TicketsApprovalId"])))
                                {
                                    ApprovalDetails = new PTicketsApprovalDetails();
                                    ApprovalDetails.Id = Convert.ToInt32(DR["TicketsApprovalId"]);
                                    ApprovalDetails.IsAppoved = DR["IsAppoved"] == DBNull.Value ? (Boolean?)null : Convert.ToBoolean(DR["IsAppoved"]);
                                    ApprovalDetails.RequestedBy = new PUser() { UserID = Convert.ToInt32(DR["ApproveRequestedById"]), ContactName = Convert.ToString(DR["ApproveRequestedByName"]), ContactNumber = Convert.ToString(DR["ApproveRequestedByContactNumber"]) };
                                    ApprovalDetails.RequestedOn = Convert.ToDateTime(DR["RequestedOn"]);
                                    ApprovalDetails.Approver = new PUser() { UserID = Convert.ToInt32(DR["TicketApproverID"]), ContactName = Convert.ToString(DR["TicketApproverName"]), ContactNumber = Convert.ToString(DR["TicketApproverContactNumber"]) };
                                    ApprovalDetails.ApprovedOn = DR["ApprovedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["ApprovedOn"]);
                                    ApprovalDetails.RejectedBy = DR["RejectedBy"] == DBNull.Value ? null : new PUser() { UserID = Convert.ToInt32(DR["RejectedBy"]), ContactName = Convert.ToString(DR["RejectedByName"]), ContactNumber = Convert.ToString(DR["RejectedByContactNumber"]) };
                                    ApprovalDetails.RejectedOn = DR["RejectedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["RejectedOn"]);
                                    ApprovalDetails.ApproverRemark = Convert.ToString(DR["ApproverRemark"]);
                                    ApprovalDetails.InActive = DR["InActiveApproval"] == DBNull.Value ? false : Convert.ToBoolean(DR["InActiveApproval"]);
                                    pHeader.ApprovalDetails.Add(ApprovalDetails);
                                }
                            }

                            if (!Header.Exists(s => s.HeaderID == cHeaderId))
                                Header.Add(pHeader);
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Header;
        }
        public DataTable GetTicketDetails_DT(int? HeaderId, int? ItemId, int? CategoryID, int? SubCategoryID, int? Severity, int? Type, int? AssignedBy, int? AssignedTo, int? UserId, string HeaderStatus, int? PageIndex = null, int? PageSize = null)
        {
            DbParameter HeaderIdP;
            DbParameter ItemIdP;
            DbParameter CategoryIDP;
            DbParameter SubCategoryIDP;
            DbParameter SeverityP;
            DbParameter TypeP;
            DbParameter AssignedToP;
            DbParameter AssignedByP;
            DbParameter UserIdP;

            try
            {
                if (HeaderId != null)
                    HeaderIdP = provider.CreateParameter("HeaderId", HeaderId, DbType.Int32);
                else
                    HeaderIdP = provider.CreateParameter("HeaderId", DBNull.Value, DbType.Int32);

                if (ItemId != null)
                    ItemIdP = provider.CreateParameter("ItemId", ItemId, DbType.Int32);
                else
                    ItemIdP = provider.CreateParameter("ItemId", DBNull.Value, DbType.Int32);


                if (CategoryID != null)
                    CategoryIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
                else
                    CategoryIDP = provider.CreateParameter("CategoryID", DBNull.Value, DbType.Int32);

                if (SubCategoryID != null)
                    SubCategoryIDP = provider.CreateParameter("SubCategoryID", SubCategoryID, DbType.Int32);
                else
                    SubCategoryIDP = provider.CreateParameter("SubCategoryID", DBNull.Value, DbType.Int32);


                if (Severity != null)
                    SeverityP = provider.CreateParameter("Severity", Severity, DbType.Int32);
                else
                    SeverityP = provider.CreateParameter("Severity", DBNull.Value, DbType.Int32);

                if (Type != null)
                    TypeP = provider.CreateParameter("Type", Type, DbType.Int32);
                else
                    TypeP = provider.CreateParameter("Type", DBNull.Value, DbType.Int32);

                if (AssignedTo != null)
                    AssignedToP = provider.CreateParameter("AssignedTo", AssignedTo, DbType.String);
                else
                    AssignedToP = provider.CreateParameter("AssignedTo", DBNull.Value, DbType.String);

                if (AssignedBy != null)
                    AssignedByP = provider.CreateParameter("AssignedBy", AssignedBy, DbType.Int32);
                else
                    AssignedByP = provider.CreateParameter("AssignedBy", DBNull.Value, DbType.Int32);
                if (UserId != null)
                    UserIdP = provider.CreateParameter("UserId", UserId, DbType.Int32);
                else
                    UserIdP = provider.CreateParameter("UserId", DBNull.Value, DbType.Int32);


                DbParameter HeaderStatusP;
                if (!string.IsNullOrEmpty(HeaderStatus))
                    HeaderStatusP = provider.CreateParameter("HeaderStatus", HeaderStatus, DbType.String);
                else
                    HeaderStatusP = provider.CreateParameter("HeaderStatus", DBNull.Value, DbType.String);

                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);


                DbParameter[] TicketTypeParams = new DbParameter[12] { HeaderIdP, ItemIdP, CategoryIDP, SubCategoryIDP, SeverityP, TypeP, AssignedToP, AssignedByP, UserIdP, HeaderStatusP, PageIndexP, PageSizeP };

                using (DataSet DS = provider.Select("GetTicketDetails", TicketTypeParams))
                {
                    if (DS != null)
                    {
                        DS.Tables[0].Columns.Remove("CategoryID");
                        DS.Tables[0].Columns.Remove("SubCategoryID");
                        DS.Tables[0].Columns.Remove("SeverityID");
                        DS.Tables[0].Columns.Remove("HeaderStatusID");
                        DS.Tables[0].Columns.Remove("CreatedBy");
                        DS.Tables[0].Columns.Remove("RowCount");
                        return DS.Tables[0];

                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }

        public int InsertTicketResolvedStatus1(int HeaderId, int SubCategoryID, int Severity, string AssignerRemark, int AssignedTo, decimal ActualDuration, string SupportType, int UserId, List<string> AttchedFile, int? ResolutionType, string Resolution, Boolean NewTR, PTR TR)
        {
            int ItemId = 0;
            ItemId = new BTickets().InsertTicketItem(HeaderId, SubCategoryID, Severity, AssignerRemark, AssignedTo, ActualDuration, UserId, AttchedFile, SupportType);
            new BTickets().UpdateTicketStatus(ItemId, 3);
            AttchedFile = new List<string>();
            new BTickets().UpdateTicketResolvedStatus(HeaderId, ItemId, ActualDuration, ResolutionType, Resolution, SupportType, PSession.User.UserID, NewTR, TR, AttchedFile);
            return ItemId;
        }
        public List<PForum> GetForumDetails(int HeaderId)
        {
            DbParameter HeaderIdP = provider.CreateParameter("HeaderId", HeaderId, DbType.Int32);
            DbParameter[] Params = new DbParameter[1] { HeaderIdP };
            List<PForum> Forum = new List<PForum>();
            try
            {
                using (DataSet DS = provider.Select("GetForumDetails", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {
                            Forum.Add(
                                new PForum()
                                {
                                    ID = Convert.ToInt64(DR["ID"]),
                                    HeaderID = Convert.ToInt32(DR["HeaderID"]),
                                    CreatedOn = Convert.ToDateTime(DR["CreatedOn"]),
                                    FromUser = new PUser
                                    {
                                        UserID = Convert.ToInt32(DR["FromUserID"]),
                                        ContactName = Convert.ToString(DR["ContactName"])
                                    },
                                    Message = Convert.ToString(DR["Message"]),
                                    FileTypeID = Convert.ToInt32(DR["FileTypeID"])
                                });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Forum;
        }
        public void insertForum(long HeaderID, long UserID, string Message, string FileName)
        {
            Int32 TTicket = 1;
            int FileTypeID = 0;
            if (string.IsNullOrEmpty(FileName))
            {
                FileTypeID = 2;
            }
            else
            {
                FileTypeID = GetFileFormate(FileName);
            }
            DbParameter HeaderIDP = provider.CreateParameter("HeaderID", HeaderID, DbType.Int64);
            DbParameter UserID1P = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter MessageP = provider.CreateParameter("Message", Message, DbType.String);
            DbParameter FileTypeIDP = provider.CreateParameter("FileTypeID", FileTypeID, DbType.Int32);
            DbParameter AttchedFileID = provider.CreateParameter("OutValue", TTicket, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[5] { HeaderIDP, UserID1P, MessageP, FileTypeIDP, AttchedFileID };


            try
            {
                //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                //{
                provider.Insert("insertForum", Params);
                if (FileTypeID != (short)FileType.Message)
                {
                    string SourceFileName = ConfigurationManager.AppSettings["BasePath"] + "/File/" + PSession.User.UserName + "/" + FileName;
                    int count = FileName.Split('.').Count();
                    if (!Directory.Exists(ConfigurationManager.AppSettings["BasePath"] + "/AttachedFile/"))
                    {
                        Directory.CreateDirectory(ConfigurationManager.AppSettings["BasePath"] + "/AttachedFile/");
                    }
                    string DestFileName = ConfigurationManager.AppSettings["BasePath"] + "/AttachedFile/" + Convert.ToInt64(AttchedFileID.Value) + "." + FileName.Split('.')[count - 1];

                    File.Move(SourceFileName, DestFileName);
                }
                //}
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "insertForum", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "insertForum", ex);
            }
        }
        private int GetFileFormate(string FileName)
        {
            string Formate = FileName.Split('.')[FileName.Split('.').Count() - 1];

            int FileTypeID = 0;
            Formate = Formate.ToLower();
            if (Formate == "docx")
            {
                FileTypeID = (short)FileType.Word;
            }
            else if (Formate == "rar")
            {
                FileTypeID = (short)FileType.RAR;
            }
            else if ((Formate == "xls") || (Formate == "xlsx"))
            {
                FileTypeID = (short)FileType.Excel;
            }
            else if (Formate == "msg")
            {
                FileTypeID = (short)FileType.MSG;
            }
            else if (Formate == "Pdf")
            {
                FileTypeID = (short)FileType.Pdf;
            }
            else if (Formate == "XML")
            {
                FileTypeID = (short)FileType.XML;
            }
            else if (Formate == "zip")
            {
                FileTypeID = (short)FileType.zipped;
            }
            else if (Formate == "jpg")
            {
                FileTypeID = (short)FileType.Jpeg;
            }
            else if (Formate == "png")
            {
                FileTypeID = (short)FileType.Png;
            }
            else
            {
                FileTypeID = (short)FileType.Unknown;
            }
            return FileTypeID;
        }
        public int UpdateTicketClosedStatus(int HeaderID, int UserID)
        {
            DbParameter HeaderIDP = provider.CreateParameter("HeaderID", HeaderID, DbType.Int64);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            int success = 0;
            DbParameter[] TicketTypeParams = new DbParameter[2] { HeaderIDP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketClosedStatus", TicketTypeParams);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketClosedStatus", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketClosedStatus", ex);
            }
            return success;
        }
        public int UpdateTicketReopenStatus(int HeaderID, int UserID)
        {
            DbParameter HeaderIDP = provider.CreateParameter("HeaderID", HeaderID, DbType.Int64);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            int success = 0;
            DbParameter[] TicketTypeParams = new DbParameter[2] { HeaderIDP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketReopenStatus", TicketTypeParams);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketReopenStatus", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketReopenStatus", ex);
            }
            return success;
        }
        public int GetL1SupportUserMapping(int RequesterUserId, int CategoryID)
        {
            try
            {
                DbParameter RequesterUserIdP = provider.CreateParameter("RequesterUserId", RequesterUserId, DbType.Int32);
                DbParameter CategoryIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
                DbParameter[] Params = new DbParameter[2] { RequesterUserIdP, CategoryIDP };
                using (DataSet DS = provider.Select("GetL1SupportUserMapping", Params))
                {
                    if (DS != null)
                    {
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            return DS.Tables[0].Rows[0][0] == DBNull.Value ? 0 : Convert.ToInt32(DS.Tables[0].Rows[0][0]);
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 0;
        }

        public List<PTicketHeader> GetManageTickets(int? HeaderId, int? CategoryID, int? SubCategoryID, string HeaderStatus, int? CreatedBy, int? AssignedTo, int? DepartmentID, DateTime? TicketFrom, DateTime? TicketTo, int? UserId, int SystemCategoryID)
        {
            DbParameter HeaderIdP;
            DbParameter CategoryIDP;
            DbParameter SubCategoryIDP;
            DbParameter AssignedToP;
            DbParameter CreatedByP;
            DbParameter UserIdP;
            DbParameter DepartmentIDP;
            DbParameter TicketFromP;
            DbParameter TicketToP;

            List<PTicketHeader> Header = new List<PTicketHeader>();
            PTicketHeader pHeader = null;
            PTicketItem pItem;
            PTicketsApprovalDetails ApprovalDetails;
            try
            {
                if (HeaderId != null)
                    HeaderIdP = provider.CreateParameter("HeaderId", HeaderId, DbType.Int32);
                else
                    HeaderIdP = provider.CreateParameter("HeaderId", DBNull.Value, DbType.Int32);



                if (CategoryID != null)
                    CategoryIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
                else
                    CategoryIDP = provider.CreateParameter("CategoryID", DBNull.Value, DbType.Int32);

                if (SubCategoryID != null)
                    SubCategoryIDP = provider.CreateParameter("SubCategoryID", SubCategoryID, DbType.Int32);
                else
                    SubCategoryIDP = provider.CreateParameter("SubCategoryID", DBNull.Value, DbType.Int32);




                if (AssignedTo != null)
                    AssignedToP = provider.CreateParameter("AssignedTo", AssignedTo, DbType.String);
                else
                    AssignedToP = provider.CreateParameter("AssignedTo", DBNull.Value, DbType.String);

                if (CreatedBy != null)
                    CreatedByP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int32);
                else
                    CreatedByP = provider.CreateParameter("CreatedBy", DBNull.Value, DbType.Int32);
                if (UserId != null)
                    UserIdP = provider.CreateParameter("UserId", UserId, DbType.Int32);
                else
                    UserIdP = provider.CreateParameter("UserId", DBNull.Value, DbType.Int32);

                if (DepartmentID != null)
                    DepartmentIDP = provider.CreateParameter("DepartmentID", DepartmentID, DbType.Int32);
                else
                    DepartmentIDP = provider.CreateParameter("DepartmentID", DBNull.Value, DbType.Int32);


                if (TicketFrom != null)

                    TicketFromP = provider.CreateParameter("TicketFrom", TicketFrom, DbType.DateTime);
                else
                    TicketFromP = provider.CreateParameter("TicketFrom", DBNull.Value, DbType.DateTime);


                if (TicketTo != null)

                    TicketToP = provider.CreateParameter("TicketTo", TicketTo, DbType.DateTime);
                else
                    TicketToP = provider.CreateParameter("TicketTo", DBNull.Value, DbType.DateTime);

                DbParameter HeaderStatusP;
                if (!string.IsNullOrEmpty(HeaderStatus))
                    HeaderStatusP = provider.CreateParameter("HeaderStatus", HeaderStatus, DbType.String);
                else
                    HeaderStatusP = provider.CreateParameter("HeaderStatus", DBNull.Value, DbType.String);

                DbParameter SystemCategoryIDP = provider.CreateParameter("SystemCategoryID", SystemCategoryID, DbType.String);


                DbParameter[] TicketTypeParams = new DbParameter[11] { HeaderIdP,   CategoryIDP, SubCategoryIDP, AssignedToP, CreatedByP,
                    UserIdP, HeaderStatusP,DepartmentIDP,TicketFromP ,TicketToP,SystemCategoryIDP};

                using (DataSet DS = provider.Select("GetManageTickets", TicketTypeParams))
                {
                    if (DS != null)
                    {
                        Int32 cHeaderId = 0;
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {

                            if (cHeaderId != Convert.ToInt32(DR["HeaderID"]))
                            {
                                pHeader = new PTicketHeader();
                                pHeader.HeaderID = Convert.ToInt32(DR["HeaderID"]);
                                pHeader.Category = new PCategory { Category = Convert.ToString(DR["Category"]), CategoryID = Convert.ToInt32(DR["CategoryID"]) };
                                pHeader.SubCategory = DR["SubCategoryID"] == DBNull.Value ? null : new PSubCategory { CategoryId = Convert.ToInt32(DR["CategoryId"]), SubCategory = Convert.ToString(DR["SubCategory"]), SubCategoryID = Convert.ToInt32(DR["SubCategoryID"]) };
                                pHeader.Type = DR["TypeID"] == DBNull.Value ? null : new PType { TypeID = Convert.ToInt32(DR["TypeID"]), Type = Convert.ToString(DR["Type"]) };
                                pHeader.CreatedBy = new PUser
                                {
                                    UserID = Convert.ToInt32(DR["CreatedBy"]),
                                    ContactName = Convert.ToString(DR["CreatedByEmployeeName"]),
                                    Department = new PDMS_DealerDepartment { DealerDepartment = Convert.ToString(DR["CreatedByDepartment"]) }
                                };
                                pHeader.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);

                                pHeader.Description = Convert.ToString(DR["Description"]);
                                pHeader.Repeat = Convert.ToBoolean(DR["Repeat"]);
                                pHeader.Severity = DR["SeverityID"] == DBNull.Value ? null : new PSeverity { SeverityID = Convert.ToInt32(DR["SeverityID"]), Severity = Convert.ToString(DR["Severity"]) };
                                pHeader.Status = DR["HeaderStatusID"] == DBNull.Value ? null : new PStatus { StatusID = Convert.ToInt32(DR["HeaderStatusID"]), Status = Convert.ToString(DR["HeaderStatus"]) };
                                cHeaderId = Convert.ToInt32(DR["HeaderID"]);
                                pHeader.PriorityLevel = DR["PriorityLevel"] == DBNull.Value ? (int?)null : Convert.ToInt32(DR["PriorityLevel"]);
                                pHeader.ContactName = Convert.ToString(DR["ContactName"]);
                                pHeader.MobileNo = Convert.ToString(DR["MobileNo"]);
                                pHeader.ClosedOn = DR["ClosedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["ClosedOn"]);
                                //pHeader.TR = new PTR();
                                //pHeader.TR.TRNumber = Convert.ToString(DR["TRNumber"]);
                                //pHeader.TR.Purpose = Convert.ToString(DR["Purpose"]);
                                //pHeader.TR.MailNote = Convert.ToString(DR["MailNote"]);
                                //pHeader.TR.Status = Convert.ToString(DR["Status"]);

                                pHeader.UATBy = new PUser { ContactName = Convert.ToString(DR["UATBy"]) };
                                pHeader.UATOn = DR["UATOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["UATOn"]);
                                pHeader.UATRemark = Convert.ToString(DR["UATRemark"]);
                                pHeader.ApprovalDetail = new PTicketsApprovalDetails()
                                {
                                    Approver = new PUser() { ContactName = Convert.ToString(DR["TicketApproverName"]) },
                                    ApprovedOn = DR["ApprovedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["ApprovedOn"]),
                                };
                                pHeader.TicketItems = new List<PTicketItem>();
                                pHeader.ApprovalDetails = new List<PTicketsApprovalDetails>();
                            }
                            if (DR["ItemID"] != DBNull.Value)
                            {
                                if (!pHeader.TicketItems.Exists(s => s.ItemID == (Convert.ToInt32(DR["ItemID"]))))
                                {
                                    pItem = new PTicketItem();
                                    pItem.ItemID = Convert.ToInt32(DR["ItemID"]);
                                    pItem.ActualDuration = DR["ActualDuration"] != DBNull.Value ? Convert.ToDecimal(DR["ActualDuration"]) : (decimal?)null;

                                    pItem.AssignedBy = DR["AssignedBy"] == DBNull.Value ? null : new PUser { UserID = Convert.ToInt32(DR["AssignedBy"]), ContactName = Convert.ToString(DR["AssignedByEmployeeName"]) };
                                    pItem.AssignedOn = Convert.ToDateTime(DR["AssignedOn"]);
                                    pItem.AssignedTo = DR["AssignedTo"] == DBNull.Value ? null : new PUser { UserID = Convert.ToInt32(DR["AssignedTo"]), ContactName = Convert.ToString(DR["AssignedToEmployeeName"]) };

                                    pItem.Effort = DR["Effort"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(DR["Effort"]);

                                    pItem.Resolution = Convert.ToString(DR["Resolution"]);
                                    pItem.AssignerRemark = Convert.ToString(DR["AssignerRemark"]);
                                    pItem.HeaderID = Convert.ToInt32(DR["HeaderID"]);
                                    pItem.ResolutionType = DR["ResolutionTypeID"] == DBNull.Value ? null : new PResolutionType { ResolutionType = Convert.ToString(DR["ResolutionType"]) };
                                    pItem.ResolvedOn = DR["ResolvedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["ResolvedOn"]);
                                    pItem.InProgressOn = DR["InProgressOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["InProgressOn"]);
                                    pItem.ItemStatus = DR["ItemStatus"] == DBNull.Value ? null :
                                    new PStatus
                                    {
                                        StatusID = Convert.ToInt32(DR["ItemStatusID"]),
                                        Status = Convert.ToString(DR["ItemStatus"])
                                    };
                                    pItem.WithInSLA1 = Convert.ToString(DR["WithInSLA1"]);
                                    pItem.WithInSLA2 = Convert.ToString(DR["WithInSLA2"]);

                                    pHeader.TicketItems.Add(pItem);
                                }
                            }
                            if (DR["TicketsApprovalId"] != DBNull.Value)
                            {
                                if (!pHeader.ApprovalDetails.Exists(s => s.Id == Convert.ToInt32(DR["TicketsApprovalId"])))
                                {
                                    ApprovalDetails = new PTicketsApprovalDetails();
                                    ApprovalDetails.Id = Convert.ToInt32(DR["TicketsApprovalId"]);
                                    ApprovalDetails.IsAppoved = DR["IsAppoved"] == DBNull.Value ? (Boolean?)null : Convert.ToBoolean(DR["IsAppoved"]);
                                    ApprovalDetails.RequestedBy = new PUser() { ContactName = Convert.ToString(DR["ApproveRequestedByName"]) };
                                    ApprovalDetails.RequestedOn = Convert.ToDateTime(DR["RequestedOn"]);
                                    ApprovalDetails.Approver = new PUser() { ContactName = Convert.ToString(DR["TicketApproverName"]) };
                                    ApprovalDetails.ApprovedOn = DR["ApprovedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["ApprovedOn"]);
                                    ApprovalDetails.ApproverRemark = Convert.ToString(DR["ApproverRemark"]);
                                    pHeader.ApprovalDetails.Add(ApprovalDetails);
                                }
                            }

                            if (!Header.Exists(s => s.HeaderID == cHeaderId))
                                Header.Add(pHeader);
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Header;
        }


        public List<PTicketHeader> GetTicketToClose(long? HeaderID, int? CategoryID, int? SubCategoryID, int UserID, int? PageIndex, int? PageSize, out int RowCount)
        {

            List<PTicketHeader> Header = new List<PTicketHeader>();
            PTicketHeader pHeader = null;
            RowCount = 0;
            try
            {
                DbParameter HeaderIdP = provider.CreateParameter("HeaderID", HeaderID, DbType.Int64);
                DbParameter CategoryIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
                DbParameter SubCategoryIDP = provider.CreateParameter("SubCategoryID", SubCategoryID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);

                DbParameter[] TicketTypeParams = new DbParameter[6] { HeaderIdP, CategoryIDP, SubCategoryIDP, UserIDP, PageIndexP, PageSizeP };

                using (DataSet DS = provider.Select("GetTicketToClose", TicketTypeParams))
                {
                    if (DS != null)
                    {
                        Int32 cHeaderId = 0;
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {

                            if (cHeaderId != Convert.ToInt32(DR["HeaderID"]))
                            {
                                pHeader = new PTicketHeader();
                                pHeader.HeaderID = Convert.ToInt32(DR["HeaderID"]);
                                pHeader.Category = new PCategory { Category = Convert.ToString(DR["Category"]), CategoryID = Convert.ToInt32(DR["CategoryID"]) };
                                pHeader.SubCategory = DR["SubCategoryID"] == DBNull.Value ? null : new PSubCategory { CategoryId = Convert.ToInt32(DR["CategoryId"]), SubCategory = Convert.ToString(DR["SubCategory"]), SubCategoryID = Convert.ToInt32(DR["SubCategoryID"]) };
                                pHeader.CreatedBy = new PUser
                                {
                                    UserID = Convert.ToInt32(DR["CreatedBy"]),
                                    ContactName = Convert.ToString(DR["CreatedByEmployeeName"]),
                                    //Department = new PDMS_DealerDepartment { DealerDepartment = Convert.ToString(DR["CreatedByDepartment"]) }
                                };
                                pHeader.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);

                                pHeader.Description = Convert.ToString(DR["Description"]);
                                pHeader.Repeat = Convert.ToBoolean(DR["Repeat"]);
                                //pHeader.Severity = DR["SeverityID"] == DBNull.Value ? null : new PSeverity { SeverityID = Convert.ToInt32(DR["SeverityID"]), Severity = Convert.ToString(DR["Severity"]) };
                                pHeader.Status = DR["HeaderStatusID"] == DBNull.Value ? null : new PStatus { StatusID = Convert.ToInt32(DR["HeaderStatusID"]), Status = Convert.ToString(DR["HeaderStatus"]) };
                                cHeaderId = Convert.ToInt32(DR["HeaderID"]);
                                pHeader.PriorityLevel = DR["PriorityLevel"] == DBNull.Value ? (int?)null : Convert.ToInt32(DR["PriorityLevel"]);
                                pHeader.ContactName = Convert.ToString(DR["ContactName"]);
                                pHeader.MobileNo = Convert.ToString(DR["MobileNo"]);
                                pHeader.age = Convert.ToInt32(DR["Age"]);
                                pHeader.TicketItems = new List<PTicketItem>();
                                pHeader.ApprovalDetails = new List<PTicketsApprovalDetails>();
                            }
                            if (!Header.Exists(s => s.HeaderID == cHeaderId))
                                Header.Add(pHeader);
                            RowCount = Convert.ToInt32(DR["RowCount"]);
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Header;
        }
        public int UpdateReassignTicket(int TicketNo, int ItemNo, string AssignerRemark, int AssignedTo, decimal ActualDuration, int UserId, string SupportType)
        {

            DbParameter TicketNoP = provider.CreateParameter("TicketNo", TicketNo, DbType.Int32);
            DbParameter ItemNoP = provider.CreateParameter("ItemNo", ItemNo, DbType.Int32);
            DbParameter AssignerRemarkParam;
            DbParameter AssignedToParam = provider.CreateParameter("AssignedTo", AssignedTo, DbType.Int32);
            DbParameter AssignedByParam = provider.CreateParameter("AssignedBy", UserId, DbType.Int32);
            DbParameter ActualDurationParam = provider.CreateParameter("ActualDuration", ActualDuration, DbType.Decimal);
            DbParameter SupportTypeP = provider.CreateParameter("SupportType", SupportType, DbType.String);
            int success = 0;

            if (!string.IsNullOrEmpty(AssignerRemark))
                AssignerRemarkParam = provider.CreateParameter("AssignerRemark", AssignerRemark, DbType.String);
            else
                AssignerRemarkParam = provider.CreateParameter("AssignerRemark", DBNull.Value, DbType.String);

            DbParameter[] ItemParams = new DbParameter[7] { TicketNoP, ItemNoP, AssignerRemarkParam, AssignedToParam, AssignedByParam, ActualDurationParam, SupportTypeP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateReassignTicket", ItemParams);
                    insertForum(TicketNo, UserId, AssignerRemark, "");
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "UpdateReassignTicket", sqlEx);
                success = 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "UpdateReassignTicket", ex);
                success = 0;
            }
            return success;
        }



        //public int UpdateTicketReopendStatus(int HeaderId, int ItemId, string Reparks)
        //{
        //    int success = 0;
        //    DbParameter HeaderIdP = provider.CreateParameter("HeaderId", HeaderId, DbType.Int64);
        //    DbParameter ItemIdP = provider.CreateParameter("ItemId", ItemId, DbType.Int32);
        //    DbParameter ReparksP;
        //    if (!string.IsNullOrEmpty(Reparks))
        //        ReparksP = provider.CreateParameter("Reparks", Reparks, DbType.String);
        //    else
        //        ReparksP = provider.CreateParameter("Reparks", DBNull.Value, DbType.String);
        //    DbParameter[] TicketTypeParams = new DbParameter[3] { HeaderIdP, ItemIdP, ReparksP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            success = provider.Insert("UpdateTicketReopendStatus", TicketTypeParams);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BTickets", "UpdateTicketReopendStatus", sqlEx);
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BTickets", "UpdateTicketReopendStatus", ex);
        //    }
        //    return success;
        //}

        public List<PTicketHeader> GetTicketsForUAT(long? HeaderId, int? CategoryID, int? UserID)
        {
            List<PTicketHeader> Header = new List<PTicketHeader>();
            PTicketHeader pHeader;
            try
            {
                DbParameter HeaderIdParam = provider.CreateParameter("HeaderId", HeaderId, DbType.Int64);
                DbParameter CategoryIDParam = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
                DbParameter[] TicketTypeParams = new DbParameter[3] { HeaderIdParam, CategoryIDParam, UserIDP };
                using (DataSet DS = provider.Select("GetTicketsForUAT", TicketTypeParams))
                {
                    if (DS != null)
                    {
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {
                            pHeader = new PTicketHeader();
                            pHeader.HeaderID = Convert.ToInt32(DR["HeaderID"]);
                            pHeader.Category = new PCategory { Category = Convert.ToString(DR["Category"]), CategoryID = Convert.ToInt32(DR["CategoryID"]) };
                            pHeader.SubCategory = DR["SubCategoryID"] == DBNull.Value ? null : new PSubCategory { CategoryId = Convert.ToInt32(DR["CategoryId"]), SubCategory = Convert.ToString(DR["SubCategory"]), SubCategoryID = Convert.ToInt32(DR["SubCategoryID"]) };
                            pHeader.Type = DR["TypeID"] == DBNull.Value ? null : new PType { TypeID = Convert.ToInt32(DR["TypeID"]), Type = Convert.ToString(DR["Type"]) };
                            pHeader.Subject = Convert.ToString(DR["Subject"]);
                            pHeader.Description = Convert.ToString(DR["Description"]);
                            //pHeader.CreatedBy = new PUser
                            //{
                            //    UserID = Convert.ToInt32(DR["CreatedBy"]),
                            //    ContactName = Convert.ToString(DR["CreatedByEmployeeName"]),
                            //    Department = new PDepartment { DepartmentName = Convert.ToString(DR["CreatedByDepartment"]) }
                            //};
                            pHeader.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);
                            pHeader.Repeat = Convert.ToBoolean(DR["Repeat"]);
                            pHeader.Severity = DR["SeverityID"] == DBNull.Value ? null : new PSeverity { SeverityID = Convert.ToInt32(DR["SeverityID"]), Severity = Convert.ToString(DR["Severity"]) };
                            pHeader.Status = DR["HeaderStatusID"] == DBNull.Value ? null : new PStatus { StatusID = Convert.ToInt32(DR["HeaderStatusID"]), Status = Convert.ToString(DR["HeaderStatus"]) };
                            pHeader.MobileNo = Convert.ToString(DR["MobileNo"]);
                            pHeader.ContactName = Convert.ToString(DR["ContactName"]);
                            pHeader.TicketItem = new PTicketItem();
                            Header.Add(pHeader);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "GetTicketsForUAT", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "GetTicketsForUAT", ex);
            }
            return Header;
        }

        public int UpdateTicketUAT(int HeaderId, DateTime UATOn, string UATRemark, long UserId, List<string> AttchedFile)
        {
            int success = 0;
            DbParameter HeaderIdP = provider.CreateParameter("HeaderId", HeaderId, DbType.Int64);
            DbParameter UATOnP = provider.CreateParameter("UATOn", UATOn, DbType.DateTime);
            DbParameter UATRemarkP = provider.CreateParameter("UATRemark", UATRemark, DbType.String);
            DbParameter[] TicketTypeParams = new DbParameter[3] { HeaderIdP, UATOnP, UATRemarkP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketUAT", TicketTypeParams);
                    if (success != 0)
                    {
                        foreach (string filename in AttchedFile)
                        {
                            insertForum(HeaderId, UserId, filename, filename);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                success = 0;
                new FileLogger().LogMessage("BTickets", "UpdateTicketUAT", sqlEx);

            }
            catch (Exception ex)
            {
                success = 0;
                new FileLogger().LogMessage("BTickets", "UpdateTicketUAT", ex);
            }
            return success;
        }
        public List<PTicketHeader> GetTicketsForDelete(int UserId)
        {
            List<PTicketHeader> Header = new List<PTicketHeader>();
            PTicketHeader pHeader = null;
            try
            {
                DbParameter UserIdP = provider.CreateParameter("UserId", UserId, DbType.Int32);
                DbParameter[] TicketTypeParams = new DbParameter[1] { UserIdP };
                using (DataSet DS = provider.Select("GetTicketsForDelete", TicketTypeParams))
                {
                    if (DS != null)
                    {
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {

                            pHeader = new PTicketHeader();
                            Header.Add(pHeader);
                            pHeader.HeaderID = Convert.ToInt32(DR["HeaderID"]);
                            pHeader.Category = new PCategory { Category = Convert.ToString(DR["Category"]) };
                            pHeader.SubCategory = DR["SubCategoryID"] == DBNull.Value ? null : new PSubCategory { SubCategory = Convert.ToString(DR["SubCategory"]) };
                            pHeader.Type = DR["TypeID"] == DBNull.Value ? null : new PType { Type = Convert.ToString(DR["Type"]) };
                            pHeader.CreatedBy = new PUser
                            {
                                UserID = Convert.ToInt32(DR["CreatedBy"]),
                                ContactName = Convert.ToString(DR["CreatedByEmployeeName"]),
                                Department = new PDMS_DealerDepartment { DealerDepartment = Convert.ToString(DR["CreatedByDepartment"]) }
                            };
                            pHeader.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);
                            pHeader.Description = Convert.ToString(DR["Description"]);
                            pHeader.Severity = DR["SeverityID"] == DBNull.Value ? null : new PSeverity { Severity = Convert.ToString(DR["Severity"]) };
                            pHeader.Status = DR["HeaderStatusID"] == DBNull.Value ? null : new PStatus { Status = Convert.ToString(DR["HeaderStatus"]) };
                            pHeader.PriorityLevel = DR["PriorityLevel"] == DBNull.Value ? (int?)null : Convert.ToInt32(DR["PriorityLevel"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Header;
        }
        public int UpdateDeleteTicket(int TicketNo)
        {
            DbParameter TicketNoP = provider.CreateParameter("TicketNo", TicketNo, DbType.Int32);
            int success = 0;
            DbParameter[] Params = new DbParameter[1] { TicketNoP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateDeleteTicket", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "UpdateDeleteTicket", sqlEx);
                success = 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "UpdateDeleteTicket", ex);
                success = 0;
            }
            return success;
        }

        public int UpdateTicketReject(long EID, int HeaderID, string Remark)
        {

            int success = 0;
            DbParameter EIDParam = provider.CreateParameter("EID", EID, DbType.Int32);
            DbParameter TicketNoParam = provider.CreateParameter("HeaderID", HeaderID, DbType.Int32);
            DbParameter RemarkParam = provider.CreateParameter("Remark", Remark, DbType.String);
            DbParameter[] Params = new DbParameter[3] { EIDParam, TicketNoParam, RemarkParam };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketReject", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketApproval", sqlEx);
                success = 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketApproval", ex);
                success = 0;
            }
            return success;
        }
        public int UpdateTicketStatusApprove(int HeaderId, int? Approver, string ApproverRemark)
        {
            int success = 0;

            DbParameter HeaderIdP = provider.CreateParameter("HeaderId", HeaderId, DbType.Int32);
            DbParameter ApproverParam = provider.CreateParameter("Approver", Approver, DbType.Int32);
            DbParameter ApproverRemarkParam = provider.CreateParameter("ApproverRemark", ApproverRemark, DbType.String);
            DbParameter[] Params = new DbParameter[3] { HeaderIdP, ApproverParam, ApproverRemarkParam };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketStatusApprove", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketStatusApprove", sqlEx);
                success = 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketStatusApprove", ex);
                success = 0;
            }
            return success;
        }
        public int UpdateTicketRejectStatus(int HeaderId, int? Approver, string ApproverRemark)
        {
            int success = 0;

            DbParameter HeaderIdP = provider.CreateParameter("HeaderId", HeaderId, DbType.Int32);
            DbParameter ApproverParam = provider.CreateParameter("Approver", Approver, DbType.Int32);
            DbParameter ApproverRemarkParam = provider.CreateParameter("ApproverRemark", ApproverRemark, DbType.String);
            DbParameter[] Params = new DbParameter[3] { HeaderIdP, ApproverParam, ApproverRemarkParam };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketRejectStatus", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketRejectStatus", sqlEx);
                success = 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketRejectStatus", ex);
                success = 0;
            }
            return success;
        }
        public int UpdateTicketForceClosedStatus(int HeaderID, int UserID)
        {
            DbParameter HeaderIDP = provider.CreateParameter("HeaderID", HeaderID, DbType.Int64);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            int success = 0;
            DbParameter[] TicketTypeParams = new DbParameter[2] { HeaderIDP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketStatusForceClose", TicketTypeParams);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketStatusForceClose", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketStatusForceClose", ex);
            }
            return success;
        }
        public int UpdateTicketCancelStatus(int HeaderID, int ItemID, int UserID)
        {
            DbParameter HeaderIDP = provider.CreateParameter("HeaderID", HeaderID, DbType.Int64);
            DbParameter ItemIDP = provider.CreateParameter("ItemID", ItemID, DbType.Int64);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            int success = 0;
            DbParameter[] TicketTypeParams = new DbParameter[3] { HeaderIDP, ItemIDP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketStatusCancel", TicketTypeParams);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketStatusCancel", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "UpdateTicketStatusCancel", ex);
            }
            return success;
        }
        public DataSet GetTicketDetailsCountByStatus(int? DealerEmployeeUserID, DateTime? TicketFrom = null, DateTime? TicketTo = null)
        {
            DbParameter CategoryIDP;
            DbParameter SubCategoryIDP;
            DbParameter DealerEmployeeUserIDP;
            DataSet ds = new DataSet();
            try
            {
                if (DealerEmployeeUserID != null)
                    DealerEmployeeUserIDP = provider.CreateParameter("DealerEmployeeUserID", DealerEmployeeUserID, DbType.Int32);
                else
                    DealerEmployeeUserIDP = provider.CreateParameter("DealerEmployeeUserID", DBNull.Value, DbType.Int32);

                DbParameter TicketFromP = provider.CreateParameter("TicketFrom", TicketFrom, DbType.DateTime);
                DbParameter TicketToP = provider.CreateParameter("TicketTo", TicketTo, DbType.DateTime);
                DbParameter[] TicketTypeParams = new DbParameter[3] { DealerEmployeeUserIDP, TicketFromP, TicketToP };
                ds = provider.Select("GetTicketDetailsCountByStatus", TicketTypeParams);
                return ds;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "GetTicketDetailsCountByStatus", ex);
                throw ex;
            }
        }
        public DataSet GetTicketDetailsCountByStatusForChart(int? DealerEmployeeUserID, DateTime? TicketFrom = null, DateTime? TicketTo = null)
        {
            DbParameter CategoryIDP;
            DbParameter SubCategoryIDP;
            DbParameter DealerEmployeeUserIDP;

            try
            {
                //if (CategoryID != null)
                //    CategoryIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
                //else
                //    CategoryIDP = provider.CreateParameter("CategoryID", DBNull.Value, DbType.Int32);

                //if (SubCategoryID != null)
                //    SubCategoryIDP = provider.CreateParameter("SubCategoryID", SubCategoryID, DbType.Int32);
                //else
                //    SubCategoryIDP = provider.CreateParameter("SubCategoryID", DBNull.Value, DbType.Int32);

                if (DealerEmployeeUserID != null)
                    DealerEmployeeUserIDP = provider.CreateParameter("DealerEmployeeUserID", DealerEmployeeUserID, DbType.Int32);
                else
                    DealerEmployeeUserIDP = provider.CreateParameter("DealerEmployeeUserID", DBNull.Value, DbType.Int32);

                DbParameter TicketFromP = provider.CreateParameter("TicketFrom", TicketFrom, DbType.DateTime);
                DbParameter TicketToP = provider.CreateParameter("TicketTo", TicketTo, DbType.DateTime);
                DbParameter[] TicketTypeParams = new DbParameter[3] { DealerEmployeeUserIDP, TicketFromP, TicketToP };
                return provider.Select("GetTicketDetailsCountByStatusForChart", TicketTypeParams);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "GetTicketDetailsCountByStatusForChart", ex);
                throw ex;
            }
        }
        public List<PTicketHeader> GetTicketDetailsSupport(int? HeaderId, int? ItemId, int? CategoryID, int? SubCategoryID, int? Severity, int? Type, int? CreatedBy, int? AssignedTo, int? ApprovalTo, int? UserId, string HeaderStatus, DateTime? TicketFrom, DateTime? TicketTo, int? PageIndex, int? PageSize, out int RowCount)
        {
            DbParameter HeaderIdP;
            DbParameter ItemIdP;
            DbParameter CategoryIDP;
            DbParameter SubCategoryIDP;
            DbParameter SeverityP;
            DbParameter TypeP;
            DbParameter CreatedByP;
            DbParameter AssignedToP;
            DbParameter ApprovalToP;
            DbParameter UserIdP;


            List<PTicketHeader> Header = new List<PTicketHeader>();
            PTicketHeader pHeader = null;
            RowCount = 0;
            try
            {
                if (HeaderId != null)
                    HeaderIdP = provider.CreateParameter("HeaderId", HeaderId, DbType.Int32);
                else
                    HeaderIdP = provider.CreateParameter("HeaderId", DBNull.Value, DbType.Int32);

                if (ItemId != null)
                    ItemIdP = provider.CreateParameter("ItemId", ItemId, DbType.Int32);
                else
                    ItemIdP = provider.CreateParameter("ItemId", DBNull.Value, DbType.Int32);


                if (CategoryID != null)
                    CategoryIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
                else
                    CategoryIDP = provider.CreateParameter("CategoryID", DBNull.Value, DbType.Int32);

                if (SubCategoryID != null)
                    SubCategoryIDP = provider.CreateParameter("SubCategoryID", SubCategoryID, DbType.Int32);
                else
                    SubCategoryIDP = provider.CreateParameter("SubCategoryID", DBNull.Value, DbType.Int32);


                if (Severity != null)
                    SeverityP = provider.CreateParameter("Severity", Severity, DbType.Int32);
                else
                    SeverityP = provider.CreateParameter("Severity", DBNull.Value, DbType.Int32);

                if (Type != null)
                    TypeP = provider.CreateParameter("Type", Type, DbType.Int32);
                else
                    TypeP = provider.CreateParameter("Type", DBNull.Value, DbType.Int32);

                if (CreatedBy != null)
                    CreatedByP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int32);
                else
                    CreatedByP = provider.CreateParameter("CreatedBy", DBNull.Value, DbType.Int32);

                if (AssignedTo != null)
                    AssignedToP = provider.CreateParameter("AssignedTo", AssignedTo, DbType.Int32);
                else
                    AssignedToP = provider.CreateParameter("AssignedTo", DBNull.Value, DbType.Int32);

                if (ApprovalTo != null)
                    ApprovalToP = provider.CreateParameter("ApprovalTo", ApprovalTo, DbType.Int32);
                else
                    ApprovalToP = provider.CreateParameter("ApprovalTo", DBNull.Value, DbType.Int32);

                if (UserId != null)
                    UserIdP = provider.CreateParameter("UserId", UserId, DbType.Int32);
                else
                    UserIdP = provider.CreateParameter("UserId", DBNull.Value, DbType.Int32);


                DbParameter HeaderStatusP;
                if (!string.IsNullOrEmpty(HeaderStatus))
                    HeaderStatusP = provider.CreateParameter("HeaderStatus", HeaderStatus, DbType.String);
                else
                    HeaderStatusP = provider.CreateParameter("HeaderStatus", DBNull.Value, DbType.String);

                DbParameter TicketFromP = provider.CreateParameter("TicketFrom", TicketFrom, DbType.DateTime);
                DbParameter TicketToP = provider.CreateParameter("TicketTo", TicketTo, DbType.DateTime);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);

                DbParameter[] TicketTypeParams = new DbParameter[15] { HeaderIdP, ItemIdP, CategoryIDP, SubCategoryIDP, SeverityP, TypeP, CreatedByP, AssignedToP, ApprovalToP, UserIdP, HeaderStatusP, TicketFromP, TicketToP, PageIndexP, PageSizeP };

                using (DataSet DS = provider.Select("GetTicketDetailsSupport1", TicketTypeParams))
                {
                    if (DS != null)
                    {
                        Int32 cHeaderId = 0;
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {

                            if (cHeaderId != Convert.ToInt32(DR["HeaderID"]))
                            {
                                pHeader = new PTicketHeader();
                                pHeader.HeaderID = Convert.ToInt32(DR["HeaderID"]);
                                pHeader.Category = new PCategory { Category = Convert.ToString(DR["Category"]), CategoryID = Convert.ToInt32(DR["CategoryID"]) };
                                pHeader.SubCategory = DR["SubCategoryID"] == DBNull.Value ? null : new PSubCategory { CategoryId = Convert.ToInt32(DR["CategoryId"]), SubCategory = Convert.ToString(DR["SubCategory"]), SubCategoryID = Convert.ToInt32(DR["SubCategoryID"]) };
                                pHeader.CreatedBy = new PUser
                                {
                                    UserID = Convert.ToInt32(DR["CreatedBy"]),
                                    ContactName = Convert.ToString(DR["CreatedByEmployeeName"]),
                                    //Department = new PDMS_DealerDepartment { DealerDepartment = Convert.ToString(DR["CreatedByDepartment"]) }
                                };
                                pHeader.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);
                                pHeader.Subject = Convert.ToString(DR["Subject"]);
                                pHeader.Description = Convert.ToString(DR["Description"]);
                                pHeader.Severity = DR["SeverityID"] == DBNull.Value ? null : new PSeverity { SeverityID = Convert.ToInt32(DR["SeverityID"]), Severity = Convert.ToString(DR["Severity"]) };
                                pHeader.Status = DR["HeaderStatusID"] == DBNull.Value ? null : new PStatus { StatusID = Convert.ToInt32(DR["HeaderStatusID"]), Status = Convert.ToString(DR["HeaderStatus"]) };
                                cHeaderId = Convert.ToInt32(DR["HeaderID"]);
                                pHeader.ContactName = Convert.ToString(DR["ContactName"]);
                                pHeader.MobileNo = Convert.ToString(DR["MobileNo"]);
                                pHeader.age = Convert.ToInt32(DR["Age"]);
                                pHeader.SLA = Convert.ToString(DR["SLA"]);
                                pHeader.TicketItems = new List<PTicketItem>();
                                pHeader.ApprovalDetails = new List<PTicketsApprovalDetails>();
                            }
                            if (!Header.Exists(s => s.HeaderID == cHeaderId))
                                Header.Add(pHeader);
                            RowCount = Convert.ToInt32(DR["RowCount"]);
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer

            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Header;
        }
        public DataSet GetTicketDetailsMonthwiseCountByStatus1(int? DealerEmployeeUserID, string Year, string Month)
        {
            try
            {
                DbParameter DealerEmployeeUserIDP = provider.CreateParameter("DealerEmployeeUserID", DealerEmployeeUserID, DbType.Int32);
                DbParameter YearP = provider.CreateParameter("Year", Year, DbType.String);
                DbParameter MonthP = provider.CreateParameter("Month", Month, DbType.String);
                DbParameter[] TicketTypeParams = new DbParameter[3] { DealerEmployeeUserIDP, YearP, MonthP };
                return provider.Select("GetTicketDetailsMonthwiseCountByStatus1", TicketTypeParams);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "GetTicketDetailsMonthwiseCountByStatus1", ex);
                throw ex;
            }
        }
        public DataSet GetTicketDetailsMonthwiseCountByStatus(int? DealerEmployeeUserID, DateTime? DateFrom, DateTime? DateTo)
        {
            try
            {
                DbParameter DealerEmployeeUserIDP = provider.CreateParameter("DealerEmployeeUserID", DealerEmployeeUserID, DbType.Int32);
                DbParameter DateFromP = provider.CreateParameter("DateFrom", DateFrom, DbType.DateTime);
                DbParameter DateToP = provider.CreateParameter("DateTo", DateTo, DbType.DateTime);
                DbParameter[] TicketTypeParams = new DbParameter[3] { DealerEmployeeUserIDP, DateFromP, DateToP };
                return provider.Select("GetTicketDetailsMonthwiseCountByStatus", TicketTypeParams);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "GetTicketDetailsMonthwiseCountByStatus", ex);
                throw ex;
            }
        }
        public DataSet GetTicketDetailsDaywiseCountByStatus(int? DealerEmployeeUserID, DateTime? DateFrom, DateTime? DateTo)
        {
            try
            {
                DbParameter DealerEmployeeUserIDP = provider.CreateParameter("DealerEmployeeUserID", DealerEmployeeUserID, DbType.Int32);
                DbParameter DateFromP = provider.CreateParameter("DateFrom", DateFrom, DbType.DateTime);
                DbParameter DateToP = provider.CreateParameter("DateTo", DateTo, DbType.DateTime);
                DbParameter[] TicketTypeParams = new DbParameter[3] { DealerEmployeeUserIDP, DateFromP, DateToP };
                return provider.Select("GetTicketDetailsDaywiseCountByStatus", TicketTypeParams);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", "GetTicketDetailsDaywiseCountByStatus", ex);
                throw ex;
            }
        }
        public long UpdateHeaderInfo(PTask_Insert TaskHeader, int UserID)
        {
            long success = 0;
            DbParameter HeaderIDParam = provider.CreateParameter("HeaderID", TaskHeader.HeaderID, DbType.Int32);
            DbParameter CategoryIDParam = provider.CreateParameter("CategoryID", TaskHeader.CategoryID, DbType.Int32);
            DbParameter SubCategoryIDParam = provider.CreateParameter("SubCategoryID", TaskHeader.SubCategoryID, DbType.Int32);
            DbParameter TicketTypeIDParam = provider.CreateParameter("TicketTypeID", TaskHeader.TicketTypeID, DbType.Int32);
            DbParameter SeverityIDParam = provider.CreateParameter("SeverityID", TaskHeader.SeverityID, DbType.Int32);
            DbParameter SubjectParam = provider.CreateParameter("Subject", TaskHeader.Subject, DbType.String);
            DbParameter DescriptionParam = provider.CreateParameter("Description", TaskHeader.Description, DbType.String);
            DbParameter UserIDParam = provider.CreateParameter("UserID", UserID, DbType.Int32);

            DbParameter[] TicketTypeParams = new DbParameter[8] { HeaderIDParam, CategoryIDParam, SubCategoryIDParam, TicketTypeIDParam, SeverityIDParam, SubjectParam, DescriptionParam, UserIDParam };

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTicketHeaderInfo", TicketTypeParams);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTickets", "insertTicketHeader", sqlEx);
                success = 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTickets", " insertTicketHeader", ex);
                success = 0;
            }
            return success;
        }
    }
}