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
        //private int GetFileFormate(string FileName)
        //{
        //    string Formate = FileName.Split('.')[FileName.Split('.').Count() - 1];

        //    int FileTypeID = 0;
        //    Formate = Formate.ToLower();
        //    if (Formate == "docx")
        //    {
        //        FileTypeID = (short)FileType.Word;
        //    }
        //    else if (Formate == "rar")
        //    {
        //        FileTypeID = (short)FileType.RAR;
        //    }
        //    else if ((Formate == "xls") || (Formate == "xlsx"))
        //    {
        //        FileTypeID = (short)FileType.Excel;
        //    }
        //    else if (Formate == "msg")
        //    {
        //        FileTypeID = (short)FileType.MSG;
        //    }
        //    else if (Formate == "Pdf")
        //    {
        //        FileTypeID = (short)FileType.Pdf;
        //    }
        //    else if (Formate == "XML")
        //    {
        //        FileTypeID = (short)FileType.XML;
        //    }
        //    else if (Formate == "zip")
        //    {
        //        FileTypeID = (short)FileType.zipped;
        //    }
        //    else if (Formate == "jpg")
        //    {
        //        FileTypeID = (short)FileType.Jpeg;
        //    }
        //    else if (Formate == "png")
        //    {
        //        FileTypeID = (short)FileType.Png;
        //    }
        //    else
        //    {
        //        FileTypeID = (short)FileType.Unknown;
        //    }
        //    return FileTypeID;
        //}
        //public int UpdateReassignTicket(int TicketNo, int ItemNo, string AssignerRemark, int AssignedTo, decimal ActualDuration, int UserId, string SupportType)
        //{

        //    DbParameter TicketNoP = provider.CreateParameter("TicketNo", TicketNo, DbType.Int32);
        //    DbParameter ItemNoP = provider.CreateParameter("ItemNo", ItemNo, DbType.Int32);
        //    DbParameter AssignerRemarkParam;
        //    DbParameter AssignedToParam = provider.CreateParameter("AssignedTo", AssignedTo, DbType.Int32);
        //    DbParameter AssignedByParam = provider.CreateParameter("AssignedBy", UserId, DbType.Int32);
        //    DbParameter ActualDurationParam = provider.CreateParameter("ActualDuration", ActualDuration, DbType.Decimal);
        //    DbParameter SupportTypeP = provider.CreateParameter("SupportType", SupportType, DbType.String);
        //    int success = 0;

        //    if (!string.IsNullOrEmpty(AssignerRemark))
        //        AssignerRemarkParam = provider.CreateParameter("AssignerRemark", AssignerRemark, DbType.String);
        //    else
        //        AssignerRemarkParam = provider.CreateParameter("AssignerRemark", DBNull.Value, DbType.String);

        //    DbParameter[] ItemParams = new DbParameter[7] { TicketNoP, ItemNoP, AssignerRemarkParam, AssignedToParam, AssignedByParam, ActualDurationParam, SupportTypeP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            success = provider.Insert("UpdateReassignTicket", ItemParams);
        //            insertForum(TicketNo, UserId, AssignerRemark, "");
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BTickets", "UpdateReassignTicket", sqlEx);
        //        success = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BTickets", "UpdateReassignTicket", ex);
        //        success = 0;
        //    }
        //    return success;
        //}        
        //public int UpdateTicketReject(long EID, int HeaderID, string Remark)
        //{
        //    int success = 0;
        //    DbParameter EIDParam = provider.CreateParameter("EID", EID, DbType.Int32);
        //    DbParameter TicketNoParam = provider.CreateParameter("HeaderID", HeaderID, DbType.Int32);
        //    DbParameter RemarkParam = provider.CreateParameter("Remark", Remark, DbType.String);
        //    DbParameter[] Params = new DbParameter[3] { EIDParam, TicketNoParam, RemarkParam };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            success = provider.Insert("UpdateTicketReject", Params);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BTickets", "UpdateTicketApproval", sqlEx);
        //        success = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BTickets", "UpdateTicketApproval", ex);
        //        success = 0;
        //    }
        //    return success;
        //}
        //public void insertForum(long HeaderID, long UserID, string Message, string FileName)
        //{
        //    Int32 TTicket = 1;
        //    int FileTypeID = 0;
        //    if (string.IsNullOrEmpty(FileName))
        //    {
        //        FileTypeID = 2;
        //    }
        //    else
        //    {
        //        FileTypeID = GetFileFormate(FileName);
        //    }
        //    DbParameter HeaderIDP = provider.CreateParameter("HeaderID", HeaderID, DbType.Int64);
        //    DbParameter UserID1P = provider.CreateParameter("UserID", UserID, DbType.Int64);
        //    DbParameter MessageP = provider.CreateParameter("Message", Message, DbType.String);
        //    DbParameter FileTypeIDP = provider.CreateParameter("FileTypeID", FileTypeID, DbType.Int32);
        //    DbParameter AttchedFileID = provider.CreateParameter("OutValue", TTicket, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
        //    DbParameter[] Params = new DbParameter[5] { HeaderIDP, UserID1P, MessageP, FileTypeIDP, AttchedFileID };


        //    try
        //    {
        //        //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        //{
        //        provider.Insert("insertForum", Params);
        //        if (FileTypeID != (short)FileType.Message)
        //        {
        //            string SourceFileName = ConfigurationManager.AppSettings["BasePath"] + "/File/" + PSession.User.UserName + "/" + FileName;
        //            int count = FileName.Split('.').Count();
        //            if (!Directory.Exists(ConfigurationManager.AppSettings["BasePath"] + "/AttachedFile/"))
        //            {
        //                Directory.CreateDirectory(ConfigurationManager.AppSettings["BasePath"] + "/AttachedFile/");
        //            }
        //            string DestFileName = ConfigurationManager.AppSettings["BasePath"] + "/AttachedFile/" + Convert.ToInt64(AttchedFileID.Value) + "." + FileName.Split('.')[count - 1];

        //            File.Move(SourceFileName, DestFileName);
        //        }
        //        //}
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BTickets", "insertForum", sqlEx);
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BTickets", "insertForum", ex);
        //    }
        //}

        //BTickets//
        public PApiResult GetTicketDetailsSupport(int? DealerId, int? HeaderId, int? CategoryID, int? SubCategoryID, int? Severity, int? Type, int? CreatedBy, int? AssignedTo, int? ApprovalTo, int? UserId, string HeaderStatus, string TicketFrom, string TicketTo, int? PageIndex, int? PageSize)
        {
            string endPoint = "Task/GetTicketDetailsSupport?DealerId=" + DealerId + "&HeaderId=" + HeaderId + "&CategoryID=" + CategoryID + "&SubCategoryID=" + SubCategoryID + "&Severity=" + Severity + "&Type=" + Type + "&CreatedBy=" + CreatedBy + "&AssignedTo=" + AssignedTo + "&ApprovalTo=" + ApprovalTo + "&UserId=" + UserId + "&HeaderStatus=" + HeaderStatus + "&TicketFrom=" + TicketFrom + "&TicketTo=" + TicketTo + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetTicketDetailsMonthwiseCountByStatus(int? DealerEmployeeUserID, int? DealerID, bool Dealerwise, string TicketFrom, string TicketTo)
        {
            string endPoint = "Task/GetTicketDetailsMonthwiseCountByStatus?DealerEmployeeUserID=" + DealerEmployeeUserID + "&DealerID=" + DealerID + "&Dealerwise=" + Dealerwise + "&TicketFrom=" + TicketFrom + "&TicketTo=" + TicketTo;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetTicketDetailsDealerwiseMonthwiseCountByStatus(int? DealerEmployeeUserID, int? DealerID, string TicketFrom, string TicketTo)
        {
            string endPoint = "Task/GetTicketDetailsDealerwiseMonthwiseCountByStatus?DealerEmployeeUserID=" + DealerEmployeeUserID + "&DealerID=" + DealerID + "&TicketFrom=" + TicketFrom + "&TicketTo=" + TicketTo;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetTicketDetailsDealerwiseCountByStatus(int? DealerID, int? DealerEmployeeUserID)
        {
            string endPoint = "Task/GetTicketDetailsDealerwiseCountByStatus?DealerID=" + DealerID + "&DealerEmployeeUserID=" + DealerEmployeeUserID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetTicketDetailsCountByStatus(int? DealerID, int? DealerEmployeeUserID, bool Dealerwise)
        {
            string endPoint = "Task/GetTicketDetailsCountByStatus?DealerID=" + DealerID + "&DealerEmployeeUserID=" + DealerEmployeeUserID + "&Dealerwise=" + Dealerwise;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetTicketDetailsDealerwiseReport(int? DealerID, int? Year = null, int? Month = null, int? StatusID = null)
        {
            string endPoint = "Task/GetTicketDetailsDealerwiseReport?DealerID=" + DealerID + "&Year=" + Year + "&Month=" + Month + "&StatusID=" + StatusID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetTicketDetailsDealerwiseCountByStatusForChart(int? DealerEmployeeUserID, int? DealerID)
        {
            string endPoint = "Task/GetTicketDetailsDealerwiseCountByStatusForChart?DealerEmployeeUserID=" + DealerEmployeeUserID + "&DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetTicketDetailsCountByStatusForChart(int? DealerEmployeeUserID, int? DealerID, bool Dealerwise)
        {
            string endPoint = "Task/GetTicketDetailsCountByStatusForChart?DealerEmployeeUserID=" + DealerEmployeeUserID + "&DealerID=" + DealerID + "&Dealerwise=" + Dealerwise;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetTicketToClose(long? HeaderID, int? CategoryID, int? SubCategoryID, int? PageIndex, int? PageSize)
        {
            string endPoint = "Task/GetTicketToClose?HeaderID=" + HeaderID + "&CategoryID=" + CategoryID + "&SubCategoryID=" + SubCategoryID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetForumDetails(int HeaderId)
        {
            string endPoint = "Task/GetForumDetails?HeaderId=" + HeaderId;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetInProgressTickets(long? HeaderId, int? CategoryID, int? SubCategoryID, int? SeverityID, int? PageIndex, int? PageSize)
        {
            string endPoint = "Task/GetInProgressTickets?HeaderId=" + HeaderId + "&CategoryID=" + CategoryID + "&SubCategoryID=" + SubCategoryID + "&SeverityID=" + SeverityID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetTicketDetails(int? HeaderId, int? ItemId, int? CategoryID, int? SubCategoryID, int? Severity, int? Type, int? AssignedBy, int? AssignedTo, int? UserId, string HeaderStatus, string TicketFrom, string TicketTo, int? PageIndex, int? PageSize)
        {
            string endPoint = "Task/GetTicketDetails?HeaderId=" + HeaderId + "&ItemId=" + ItemId + "&CategoryID=" + CategoryID + "&SubCategoryID=" + SubCategoryID + "&Severity=" + Severity + "&Type=" + Type + "&AssignedBy=" + AssignedBy + "&AssignedTo=" + AssignedTo + "&UserId=" + UserId + "&HeaderStatus=" + HeaderStatus + "&TicketFrom=" + TicketFrom + "&TicketTo=" + TicketTo + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetTicketByID(int? HeaderId)
        {
            string endPoint = "Task/GetTicketByID?HeaderId=" + HeaderId;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetTicketDetails_Excel(int? HeaderId, int? ItemId, int? CategoryID, int? SubCategoryID, int? Severity, int? Type, int? AssignedBy, int? AssignedTo, int? UserId, string HeaderStatus, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "Task/GetTicketDetails_Excel?HeaderId=" + HeaderId + "&ItemId=" + ItemId + "&CategoryID=" + CategoryID + "&SubCategoryID=" + SubCategoryID + "&Severity=" + Severity + "&Type=" + Type + "&AssignedBy=" + AssignedBy + "&AssignedTo=" + AssignedTo + "&UserId=" + UserId + "&HeaderStatus=" + HeaderStatus + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetTicketsApprovaldetails(long? HeaderId, string CreatedDateFrom, string CreatedDateTo, int? CategoryID, int? SubCategoryID, int? SeverityID, long UserID, int? PageIndex, int? PageSize)
        {
            string endPoint = "Task/GetTicketsApprovaldetails?HeaderId=" + HeaderId + "&CreatedDateFrom=" + CreatedDateFrom + "&CreatedDateTo=" + CreatedDateTo + "&CategoryID=" + CategoryID + "&SubCategoryID=" + SubCategoryID + "&SeverityID=" + SeverityID + "&UserID=" + UserID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetOpenTickets(int? HeaderId, int? TicketCategoryID, int? Status, long? CreatedBy, string CreatedDateFrom, string CreatedDateTo, long UserID, int? PageIndex, int? PageSize)
        {
            string endPoint = "Task/GetOpenTickets?HeaderId=" + HeaderId + "&TicketCategoryID=" + TicketCategoryID + "&Status=" + Status + "&CreatedBy=" + CreatedBy + "&CreatedDateFrom=" + CreatedDateFrom + "&CreatedDateTo=" + CreatedDateTo + "&UserID=" + UserID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetAssignedTickets(long? HeaderID, int? CategoryID, int? SubCategoryID, int? SeverityID, int UserID, int? PageIndex, int? PageSize)
        {
            string endPoint = "Task/GetAssignedTickets?HeaderID=" + HeaderID + "&CategoryID=" + CategoryID + "&SubCategoryID=" + SubCategoryID + "&SeverityID=" + SeverityID + "&UserID=" + UserID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PAttachedFile GetAttachedFileTaskForDownload(string DocumentName)
        {
            string endPoint = "Task/AttachedFileForDownload?DocumentName=" + DocumentName;
            return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
        }
        //BTicketSeverity//
        public PApiResult getTicketSeverity(int? TicketSeverityID, string TicketSeverity)
        {
            string endPoint = "Task/getTicketSeverity?TicketSeverityID=" + TicketSeverityID + "&TicketSeverity=" + TicketSeverity;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        //BTicketCategory//
        public PApiResult getTicketCategory(int? TicketCategoryID, string TicketCategory)
        {
            string endPoint = "Task/getTicketCategory?TicketCategoryID=" + TicketCategoryID + "&TicketCategory=" + TicketCategory;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        //BTicketResolutionType//
        public PApiResult getTicketResolutionType(int? TicketResolutionTypeID, string TicketResolutionType)
        {
            string endPoint = "Task/getTicketResolutionType?TicketResolutionTypeID=" + TicketResolutionTypeID + "&TicketResolutionType=" + TicketResolutionType;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        //BTicketStatus//
        public PApiResult getTicketStatus(int? TicketStatusID, string TicketStatus)
        {
            string endPoint = "Task/getTicketStatus?TicketStatusID=" + TicketStatusID + "&TicketStatus=" + TicketStatus;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        //BTicketSubCategory//
        public PApiResult getTicketSubCategory(int? TicketSubCategoryID, string TicketSubCategory, int? TicketCategoryId)
        {
            string endPoint = "Task/getTicketSubCategory?TicketSubCategoryID=" + TicketSubCategoryID + "&TicketSubCategory=" + TicketSubCategory + "&TicketCategoryId=" + TicketCategoryId;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        //BTicketType//
        public PApiResult getTicketType(int? TicketTypeID, string TicketType)
        {
            string endPoint = "Task/getTicketType?TicketTypeID=" + TicketTypeID + "&TicketType=" + TicketType;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
}