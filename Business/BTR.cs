using DataAccess;
using Properties;
using System;
using System.Collections.Generic;
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
    public class BTR
    {
        private IDataAccess provider;
        public BTR()
        {
            provider = new ProviderFactory().GetProvider();
        }
        //public long insertTR(int EID, string TicketNo, string Purpose, string MailNote, string ToMail)
        //{
        //    long TicketID = 0;
        //    int success = 0;
        //    DbParameter EIDParam = provider.CreateParameter("SendBy", EID, DbType.Int32);
        //    DbParameter TicketNoParam = provider.CreateParameter("TicketId", TicketNo.Substring(3, 7), DbType.Int32);
        //    DbParameter PurposeParam = provider.CreateParameter("Purpose", Purpose, DbType.String);
        //    DbParameter MailNoteParam = provider.CreateParameter("MailNote", MailNote, DbType.String);
        //    DbParameter ToMailParam = provider.CreateParameter("ToMail", ToMail, DbType.String);
        //    DbParameter IDParam = provider.CreateParameter("OutValue", TicketID, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
        //    DbParameter[] Params = new DbParameter[6] { EIDParam, TicketNoParam, PurposeParam, MailNoteParam, ToMailParam, IDParam };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            success = provider.Insert("insertTRDetails", Params);
        //            if (success != 0)
        //            {
        //                TicketID = Convert.ToInt64(IDParam.Value);
        //            }
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return TicketID;
        //}
        public List<PTR> GetTRForApproval()
        {
            List<PTR> TRs = new List<PTR>();
            PTR TR = null;
            try
            {
                using (DataSet DS = provider.Select("GetTRForApproval"))
                {
                    if (DS != null)
                    {
                        foreach (DataRow DR in DS.Tables[0].Rows)
                        {
                            TR = new PTR();
                            TR.ID = Convert.ToInt32(DR["ID"]);
                            TR.TicketId = DR["TicketId"] == DBNull.Value ? (int?)null : Convert.ToInt32(DR["TicketId"]);

                            TR.RequestedOn = DR["RequestedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["RequestedOn"]);
                            TR.RequestedBy = new PUser() { ContactName = Convert.ToString(DR["RequestedBy_ContactName"]) };
                            TR.DevelopedOn = DR["DevelopedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["DevelopedOn"]);
                            TR.DevelopedBy = new PUser() { ContactName = Convert.ToString(DR["DevelopedBy_ContactName"]) };

                            TR.TRNumber = Convert.ToString(DR["TRNumber"]);
                            TR.Purpose = Convert.ToString(DR["Purpose"]);
                            TR.MailNote = Convert.ToString(DR["MailNote"]);
                            TR.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);
                            TR.CreatedBy = new PUser() { ContactName = Convert.ToString(DR["CreatedBy_ContactName"]) };
                            TR.Category = new PCategory() { Category = Convert.ToString(DR["Category"]) };
                            TR.SubCategory = new PSubCategory() { SubCategory = Convert.ToString(DR["SubCategory"]) };

                            TR.ChangeApprovedOn = DR["ChangeApprovedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["ChangeApprovedOn"]);
                            TR.ChangeApprovedBy = new PUser() { ContactName = Convert.ToString(DR["ChangeApprovedBy_ContactName"]) };
                            TR.UATOn = DR["UATOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["UATOn"]);
                            TR.UATBy = new PUser() { ContactName = Convert.ToString(DR["UATBy_ContactName"]) };

                            TRs.Add(TR);
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
            return TRs;
        }

        public int UpdateTRApprove(int TRId, int EID)
        {
            int success = 0;
            DbParameter IDP = provider.CreateParameter("ID", TRId, DbType.Int32);
            DbParameter EIDP = provider.CreateParameter("EID", EID, DbType.Int32);
            DbParameter[] Params = new DbParameter[2] { IDP, EIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTRApprove", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return success;
        }
        public int UpdateTRMove(int TRId, int EID)
        {
            int success = 0;
            DbParameter IDP = provider.CreateParameter("ID", TRId, DbType.Int32);
            DbParameter EIDP = provider.CreateParameter("EID", EID, DbType.Int32);
            DbParameter[] Params = new DbParameter[2] { IDP, EIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateTRMove", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return success;
        }
        //public int UpdateTRClose(int TicketNo)
        //{
        //    DbParameter TicketNoParam;
        //    int success = 0;
        //    TicketNoParam = provider.CreateParameter("TicketNo", TicketNo, DbType.Int32);
        //    DbParameter[] Params = new DbParameter[1] { TicketNoParam };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            success = provider.Insert("UpdateTRClose", Params);
        //            if (success != 0)
        //            { }
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return success;
        //}
        public List<PTR> GetTRDetails(DateTime? RequestedFrom, DateTime? RequestedTo, int? Requester, string TRNumber, string Purpose, string Note, string Status, string ExternalReferenceID)
        {
            List<PTR> TRs = new List<PTR>();
            PTR TR = null;
            try
            {
                DataTable dt = GetTRDetailsDT(RequestedFrom, RequestedTo, Requester, TRNumber, Purpose, Note, Status, ExternalReferenceID);
                foreach (DataRow DR in dt.Rows)
                {
                    TR = new PTR();
                    TR.ID = Convert.ToInt32(DR["ID"]);
                    TR.TicketId = DR["TicketId"] == DBNull.Value ? (int?)null : Convert.ToInt32(DR["TicketId"]);
                    TR.TRNumber = Convert.ToString(DR["TRNumber"]);
                    TR.Purpose = Convert.ToString(DR["Purpose"]);
                    TR.MailNote = Convert.ToString(DR["MailNote"]);
                    TR.Status = Convert.ToString(DR["Status"]);
                    TR.CreatedBy = new PUser() { UserID = Convert.ToInt32(DR["CreatedBy"]), ContactName = Convert.ToString(DR["CreatedBy_ContactName"]) };
                    TR.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);
                    if (DR["ApprovedBy"] == DBNull.Value)
                    {
                        TR.ApprovedBy = new PUser();
                    }
                    else
                        TR.ApprovedBy = new PUser() { UserID = Convert.ToInt32(DR["ApprovedBy"]), ContactName = Convert.ToString(DR["ApprovedBy_ContactName"]) };
                    TR.ApprovedOn = DR["ApprovedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["ApprovedOn"]);
                    TR.TRMovedOn = DR["TRMovedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["TRMovedOn"]);

                    TR.ChangeApprovedOn = DR["Change Approved On"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["Change Approved On"]);
                    TR.ChangeApprovedBy = new PUser() { ContactName = Convert.ToString(DR["Change Approved By"]) };
                    TR.UATOn = DR["UAT On"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["UAT On"]);
                    TR.UATBy = new PUser() { ContactName = Convert.ToString(DR["UAT By"]) };

                    TR.Category = new PCategory() { Category = Convert.ToString(DR["Category"]) };
                    TR.SubCategory = new PSubCategory() { SubCategory = Convert.ToString(DR["SubCategory"]) };
                    TRs.Add(TR);
                }
            }
            catch (SqlException sqlEx)
            {
            }
            catch (Exception ex)
            {
            }
            return TRs;
        }
        public DataTable GetTRDetailsDT(DateTime? RequestedFrom, DateTime? RequestedTo, int? Requester, string TRNumber, string Purpose, string Note, string Status, string ExternalReferenceID)
        {

            DataTable dt = new DataTable();
            DbParameter RequestedFromP;
            DbParameter RequestedToP;
            DbParameter RequesterP;
            DbParameter TRNumberP;
            DbParameter PurposeP;
            DbParameter NoteP;
            DbParameter StatusP;
            DbParameter ExternalReferenceIDP;


            try
            {
                if (RequestedFrom != null)
                    RequestedFromP = provider.CreateParameter("RequestedFrom", RequestedFrom, DbType.DateTime);
                else
                    RequestedFromP = provider.CreateParameter("RequestedFrom", DBNull.Value, DbType.DateTime);

                if (RequestedTo != null)
                    RequestedToP = provider.CreateParameter("RequestedTo", RequestedTo, DbType.DateTime);
                else
                    RequestedToP = provider.CreateParameter("RequestedTo", DBNull.Value, DbType.DateTime);

                if (Requester != null)
                    RequesterP = provider.CreateParameter("Requester", Requester, DbType.Int32);
                else
                    RequesterP = provider.CreateParameter("Requester", DBNull.Value, DbType.Int32);

                if (!string.IsNullOrEmpty(TRNumber))
                    TRNumberP = provider.CreateParameter("TRNumber", TRNumber, DbType.String);
                else
                    TRNumberP = provider.CreateParameter("TRNumber", DBNull.Value, DbType.String);

                if (!string.IsNullOrEmpty(Purpose))
                    PurposeP = provider.CreateParameter("Purpose", Purpose, DbType.String);
                else
                    PurposeP = provider.CreateParameter("Purpose", DBNull.Value, DbType.String);

                if (!string.IsNullOrEmpty(Note))
                    NoteP = provider.CreateParameter("Note", Note, DbType.String);
                else
                    NoteP = provider.CreateParameter("Note", DBNull.Value, DbType.String);

                if (!string.IsNullOrEmpty(Status))
                    StatusP = provider.CreateParameter("Status", Status, DbType.String);
                else
                    StatusP = provider.CreateParameter("Status", DBNull.Value, DbType.String);

                if (!string.IsNullOrEmpty(ExternalReferenceID))
                    ExternalReferenceIDP = provider.CreateParameter("ExternalReferenceID", ExternalReferenceID, DbType.String);
                else
                    ExternalReferenceIDP = provider.CreateParameter("ExternalReferenceID", DBNull.Value, DbType.String);

                DbParameter[] Params = new DbParameter[8] { RequestedFromP, RequestedToP, RequesterP, TRNumberP, PurposeP, NoteP, StatusP, ExternalReferenceIDP };

                using (DataSet DS = provider.Select("GetTRDetails", Params))
                {
                    if (DS != null)
                    {
                        dt = DS.Tables[0];

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
        public List<PTR> GetTrForDelete(string ExternalReferenceID)
        {
            List<PTR> TRs = new List<PTR>();
            PTR TR = null;
            try
            {
                DataTable dt = GetTRDetailsDT(null, null, null, null, null, null, "Requested", ExternalReferenceID);
                foreach (DataRow DR in dt.Rows)
                {
                    TR = new PTR();
                    TR.ID = Convert.ToInt32(DR["ID"]);
                    TR.TicketId = DR["TicketId"] == DBNull.Value ? (int?)null : Convert.ToInt32(DR["TicketId"]);
                    TR.TRNumber = Convert.ToString(DR["TRNumber"]);
                    TR.Purpose = Convert.ToString(DR["Purpose"]);
                    TR.MailNote = Convert.ToString(DR["MailNote"]);
                    TR.Status = Convert.ToString(DR["Status"]);
                    TR.CreatedBy = new PUser() { UserID = Convert.ToInt32(DR["CreatedBy"]), ContactName = Convert.ToString(DR["CreatedBy_ContactName"]) };
                    TR.CreatedOn = Convert.ToDateTime(DR["CreatedOn"]);                    
                    TR.ChangeApprovedOn = DR["Change Approved On"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["Change Approved On"]);
                    TR.ChangeApprovedBy = new PUser() { ContactName = Convert.ToString(DR["Change Approved By"]) };
                    TR.UATOn = DR["UAT On"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(DR["UAT On"]);
                    TR.UATBy = new PUser() { ContactName = Convert.ToString(DR["UAT By"]) };

                    TR.Category = new PCategory() { Category = Convert.ToString(DR["Category"]) };
                    TR.SubCategory = new PSubCategory() { SubCategory = Convert.ToString(DR["SubCategory"]) };
                    TRs.Add(TR);
                }
            }
            catch (SqlException sqlEx)
            {
            }
            catch (Exception ex)
            {
            }
            return TRs;
        }
        public int UpdateDeleteTR(int TrID)
        {
            DbParameter TrIDP = provider.CreateParameter("TrID", TrID, DbType.Int32);
            int success = 0;
            DbParameter[] Params = new DbParameter[1] { TrIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateDeleteTR", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BTR", "UpdateDeleteTR", sqlEx);
                success = 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BTR", "UpdateDeleteTR", ex);
                success = 0;
            }
            return success;
        } 
    }
}
