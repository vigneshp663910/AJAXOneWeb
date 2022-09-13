using DataAccess;
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
    public class BHelp
    {
        private IDataAccess provider;
        public BHelp()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PModuleAccess> GetApplication()
        {
            DateTime traceStartTime = DateTime.Now;
            List<PModuleAccess> MAs = new List<PModuleAccess>();
            PModuleAccess MA = null;
            try
            {
                using (DataSet ds = provider.Select("GetApplication"))
                {
                    if (ds != null)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            MA = new PModuleAccess();
                            MAs.Add(MA);
                            MA.ModuleMasterID = Convert.ToInt32(dr["ApplicationID"]);
                            MA.ModuleName = Convert.ToString(dr["ApplicationName"]);
                        }
                }
                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);
                return MAs;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public List<PSubModuleAccess> GetModule()
        {
            DateTime traceStartTime = DateTime.Now;
            List<PSubModuleAccess> SAs = new List<PSubModuleAccess>();
            int ID = 0;
            PSubModuleAccess SA = null;
            try
            {
                using (DataSet ds = provider.Select("GetModule"))
                {
                    if (ds != null)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            SA = new PSubModuleAccess();
                            SAs.Add(SA);
                            SA.SubModuleMasterID = Convert.ToInt32(dr["ModuleID"]);
                            SA.DisplayName1 = Convert.ToString(dr["ModuleName"]);
                        }
                }
                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);
                return SAs;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }

            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public Boolean InsertOrUpdateDocumentAttachment(PHelp help)
        {

            int success = 0;
            // PDMS_ServiceMaterial MM = new SMaterial().getMaterialTax(Customer, Vendor, OrderType, 1, MaterialService, 1);
            // new SCustomer().getCustomerAddress(Customer);
            DbParameter DocumentAttachmentID = provider.CreateParameter("DocumentAttachmentID", help.DocumentAttachmentID, DbType.Int64);
            DbParameter Sno = provider.CreateParameter("Sno", help.Sno, DbType.String);
            DbParameter Description = provider.CreateParameter("Description", help.Description, DbType.String);
            DbParameter PDFAttachment = provider.CreateParameter("PDFAttachment", help.PDFAttachment, DbType.String);
            DbParameter PPSAttachment = provider.CreateParameter("PPSAttachment", help.PPSAttachment, DbType.String);
            DbParameter VideoLink = provider.CreateParameter("VideoLink", help.VideoLink, DbType.String);
            DbParameter OrderNo = provider.CreateParameter("OrderNo", help.OrderNo, DbType.Int32);
            DbParameter IsDeleted = provider.CreateParameter("IsDeleted", help.IsDeleted, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", help.CreatedBy, DbType.Int32);
            DbParameter OutValue = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[10] { DocumentAttachmentID, Sno, Description, PDFAttachment, PPSAttachment, VideoLink, OrderNo, IsDeleted, UserIDP, OutValue };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("InsertOrUpdateDocumentAttachment", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BHelp", "InsertOrUpdateDocumentAttachment", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BHelp", " InsertOrUpdateDocumentAttachment", ex);
                return false;
            }
            return true;
        }
        public List<PHelp> GetDocumentAttachment(Int32? DocumentAttachmentID)
        {
            DateTime traceStartTime = DateTime.Now;
            List<PHelp> helps = new List<PHelp>();
            try
            {
                DbParameter DocumentAttachmentIDP = provider.CreateParameter("DocumentAttachmentID", DocumentAttachmentID, DbType.Int32);
                DbParameter[] Params = new DbParameter[1] { DocumentAttachmentIDP };
                using (DataSet ds = provider.Select("GetDocumentAttachment", Params))
                {
                    if (ds != null)
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            //PHelp help = new PHelp();
                            helps.Add(new PHelp()
                            {
                                DocumentAttachmentID = Convert.ToInt32(dr["DocumentAttachmentID"]),
                                Sno = Convert.ToString(dr["Sno"]),
                                Description = Convert.ToString(dr["Description"]),
                                PDFAttachment = Convert.ToString(dr["PDFAttachment"]),
                                PPSAttachment = Convert.ToString(dr["PPSAttachment"]),
                                VideoLink = Convert.ToString(dr["VideoLink"]),
                                OrderNo = Convert.ToInt32(dr["OrderNo"])
                            });
                        }
                }
                TraceLogger.Log(traceStartTime);
                return helps;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
    }
}
