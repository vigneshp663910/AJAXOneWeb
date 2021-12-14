using DataAccess;
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
    public class BDMS_PDICheckList
    {
        private IDataAccess provider;

        public BDMS_PDICheckList()
        {
            try
            {
                provider = new ProviderFactory().GetProvider();
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_PDICheckList", "provider : " + e1.Message, null);
            }
        }

        public PDMS_ICTicketPDICheckList GetICTicketPDICheckList1ByICTicketID(long ICTicketID)
        {
            PDMS_ICTicketPDICheckList CheckList = new PDMS_ICTicketPDICheckList();
            PDMS_ICTicketPDICheckListItem Item = null;
            try
            {
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { ICTicketIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketPDICheckList1ByICTicketID", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            CheckList.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            CheckList.ICTicketPDICheckListID = Convert.ToInt64(dr["ICTicketPDICheckList1ID"]);
                            CheckList.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                            CheckList.CreatedBy = new PUser() { ContactName = Convert.ToString(dr["CreatedBy_ContactName"]) };
                            CheckList.PDICheckListItems = new List<PDMS_ICTicketPDICheckListItem>();
                            break;
                        }
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Item = new PDMS_ICTicketPDICheckListItem();
                            CheckList.PDICheckListItems.Add(Item);

                            Item.ICTicketPDICheckListItemID = Convert.ToInt64(dr["IcTicketPDICheckList1ItemID"]);
                            Item.PDICheckList = new PDMS_PDICheckList()
                            {
                                PDICheckListID = Convert.ToInt32(dr["PDICheckList1ID"]),
                                Title = Convert.ToString(dr["Title"]),
                                Description = Convert.ToString(dr["Description"])
                            };
                            Item.ObservationValue = Convert.ToString(dr["ObservationValue"]);
                            Item.ObservationJudgement = Convert.ToString(dr["ObservationJudgement"]);
                            Item.Picture = string.IsNullOrEmpty(Convert.ToString(dr["FileName"])) ? new PAttachedFile() : new PAttachedFile()
                            {
                                FileName = Convert.ToString(dr["FileName"]),
                                FileType = Convert.ToString(dr["ContentType"]),
                                AttachedFile = (Byte[])(dr["AttachedFile"])
                            };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return CheckList;
        }
        public long InsertOrUpdateICTicketPDICheckList1(PDMS_ICTicketPDICheckList CheckList, int UserID)
        {
            int success = 0;
            long ClaimDebitID = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (CheckList.ICTicketPDICheckListID == 0)
                    {
                        DbParameter ICTicketPDICheckListIDP = provider.CreateParameter("ICTicketPDICheckListID", CheckList.ICTicketPDICheckListID, DbType.Int64); 
                        DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", CheckList.ICTicketID, DbType.Int32);
                        DbParameter CreatedByP = provider.CreateParameter("CreatedBy", UserID, DbType.Boolean);
                        DbParameter OutValueP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                        DbParameter[] Params = new DbParameter[4] { ICTicketPDICheckListIDP, ICTicketIDP, CreatedByP, OutValueP };
                        success = provider.Insert("ZDMS_InsertOrUpdateICTicketPDICheckList1", Params);
                        CheckList.ICTicketPDICheckListID = Convert.ToInt64(OutValueP.Value);
                    }
                    foreach (PDMS_ICTicketPDICheckListItem CL in CheckList.PDICheckListItems)
                    {

                        DbParameter ICTicketPDICheckListItemIDP = provider.CreateParameter("ICTicketPDICheckListItemID", CL.ICTicketPDICheckListItemID, DbType.Int64);
                        DbParameter ICTicketPDICheckListIDP = provider.CreateParameter("ICTicketPDICheckListID", CheckList.ICTicketPDICheckListID, DbType.Int64);
                        DbParameter PDICheckListIDP = provider.CreateParameter("PDICheckListID", CL.PDICheckList.PDICheckListID, DbType.Int32);
                        DbParameter ObservationValueP = provider.CreateParameter("ObservationValue", CL.ObservationValue, DbType.String);
                        DbParameter ObservationJudgementP = provider.CreateParameter("ObservationJudgement", CL.ObservationJudgement, DbType.String);
                        DbParameter FileNameP = provider.CreateParameter("FileName", CL.Picture.FileName, DbType.String);

                        DbParameter ContentTypeP = provider.CreateParameter("ContentType", CL.Picture.FileType, DbType.String);
                        DbParameter AttachedFileP = provider.CreateParameter("AttachedFile", CL.Picture.AttachedFile, DbType.Binary);
                        DbParameter CreatedByPI = provider.CreateParameter("CreatedBy", UserID, DbType.Int32);

                        DbParameter[] ParamsI = new DbParameter[9] { ICTicketPDICheckListItemIDP, ICTicketPDICheckListIDP,  PDICheckListIDP
                            ,ObservationValueP,ObservationJudgementP,FileNameP,ContentTypeP,AttachedFileP,CreatedByPI };

                        success = provider.Insert("ZDMS_InsertOrUpdateICTicketPDICheckList1Item", ParamsI);
                    }
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_PDICheckList", "InsertOrUpdateICTicketPDICheckList1", sqlEx);
                return 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_PDICheckList", " InsertOrUpdateICTicketPDICheckList1", ex);
                return 0;
            }
            return ClaimDebitID;
        }
        public DataTable GetICTicketPDICheckList1ForApproval(int UserID)
        {
            try
            {
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[1] { UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketPDICheckList1ForApproval", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public DataTable GetICTicketPDICheckList1ForVerify(int UserID)
        {
            try
            {
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[1] { UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketPDICheckList1ForVerify", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public long UpdateICTicketPDICheckList1Status(long ICTicketID, int UserID, String Status)
        {
            int success = 0;
            long ClaimDebitID = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                    DbParameter StatusP = provider.CreateParameter("Status", Status, DbType.String);
                    DbParameter[] Params = new DbParameter[3] { ICTicketIDP, UserIDP, StatusP };
                    success = provider.Insert("ZDMS_UpdateICTicketPDICheckList1Status", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_PDICheckList", "UpdateICTicketPDICheckList1Status", sqlEx);
                return 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_PDICheckList", " UpdateICTicketPDICheckList1Status", ex);
                return 0;
            }
            return ClaimDebitID;
        }

        public DataTable GetICTicketPDICheckList1ForReport(int UserID)
        {
            try
            {
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[1] { UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketPDICheckList1ForApproval", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }



        public PDMS_ICTicketPDICheckList GetICTicketPDICheckList2ByICTicketID(long ICTicketID)
        {
            PDMS_ICTicketPDICheckList CheckList = new PDMS_ICTicketPDICheckList();
            PDMS_ICTicketPDICheckListItem Item = null;
            try
            {
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);

                DbParameter[] Params = new DbParameter[1] { ICTicketIDP };
                using (DataSet DataSet = provider.Select("ZDMS_ICTicketPDICheckList1ByICTicketID", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            CheckList.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            CheckList.ICTicketPDICheckListID = Convert.ToInt64(dr["ICTicketPDICheckList1ID"]);
                            CheckList.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                            CheckList.CreatedBy = new PUser() { ContactName = Convert.ToString(dr["CreatedBy_ContactName"]) };
                            CheckList.PDICheckListItems = new List<PDMS_ICTicketPDICheckListItem>();
                            break;
                        }
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Item = new PDMS_ICTicketPDICheckListItem();
                            CheckList.PDICheckListItems.Add(Item);

                            Item.ICTicketPDICheckListItemID = Convert.ToInt64(dr["IcTicketPDICheckList1ItemID"]);
                            Item.PDICheckList = new PDMS_PDICheckList()
                            {
                                PDICheckListID = Convert.ToInt32(dr["PDICheckList1ID"]),
                                Title = Convert.ToString(dr["Title"]),
                                Description = Convert.ToString(dr["Description"])
                            };
                            Item.ObservationValue = Convert.ToString(dr["ObservationValue"]);
                            Item.ObservationJudgement = Convert.ToString(dr["ObservationJudgement"]);

                            Item.Picture = new PAttachedFile()
                            {
                                FileName = Convert.ToString(dr["FileName"])
                            };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return CheckList;
        }
    }
}
