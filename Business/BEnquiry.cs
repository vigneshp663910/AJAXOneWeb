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
   public class BEnquiry
    {
        private IDataAccess provider;
        public BEnquiry()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public Boolean InsertOrUpdateEnquiry(PEnquiry enquiry)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter EnquiryID = provider.CreateParameter("EnquiryID", enquiry.EnquiryID, DbType.Int32);
                DbParameter EnquiryDate = provider.CreateParameter("EnquiryDate", enquiry.EnquiryDate, DbType.DateTime);
                DbParameter EnquiryNumber = provider.CreateParameter("EnquiryNumber", enquiry.EnquiryNumber, DbType.String);
                DbParameter CustomerName = provider.CreateParameter("CustomerName", enquiry.CustomerName, DbType.String);
                DbParameter PersonName = provider.CreateParameter("PersonName", enquiry.PersonName, DbType.String);
                DbParameter Mail = provider.CreateParameter("Mail", enquiry.Mail, DbType.String);
                DbParameter Mobile = provider.CreateParameter("Mobile", enquiry.Mobile, DbType.String);
                DbParameter Address = provider.CreateParameter("Address", enquiry.Address, DbType.String);
                DbParameter SourceID = provider.CreateParameter("SourceID", enquiry.Source.SourceID, DbType.Int32);
                DbParameter StatusID = provider.CreateParameter("StatusID", enquiry.Status.StatusID, DbType.Int32);
                DbParameter CountryID = provider.CreateParameter("CountryID", enquiry.Country.CountryID, DbType.Int32);
                DbParameter StateID = provider.CreateParameter("StateID", enquiry.State.StateID, DbType.Int32);
                DbParameter DistrictID = provider.CreateParameter("DistrictID", enquiry.District.DistrictID, DbType.Int32);                
                DbParameter Product = provider.CreateParameter("Product", enquiry.Product, DbType.String);
                DbParameter Remarks = provider.CreateParameter("Remarks", enquiry.Remarks, DbType.String);
                DbParameter CreatedBy = provider.CreateParameter("CreatedBy", enquiry.CreatedBy.CreatedBy, DbType.Int32);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter[] Params = new DbParameter[16] { EnquiryID, EnquiryDate, EnquiryNumber, CustomerName, PersonName, Mail, Mobile, Address, SourceID, StatusID, CountryID, StateID, DistrictID, Product, Remarks, CreatedBy };
                    provider.Insert("ZDMS_InsertOrUpdateEnquiry", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BProject", "ZDMS_InsertOrUpdateEnquiry", ex);
                return false;
            }
            return true;
        }
        public List<PEnquiry> GetEnquiry(int? EnquiryID, string CustomerName, int? CountryID, int? StateID, int? DistrictID)
        {
            List<PEnquiry> projects = new List<PEnquiry>();
            try
            {
                DbParameter CustomerNameP = provider.CreateParameter("CustomerName", CustomerName, DbType.String);
                DbParameter EnquiryIDP = provider.CreateParameter("EnquiryID", EnquiryID, DbType.Int32);
                DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter DistrictIDP = provider.CreateParameter("DistrictID", DistrictID, DbType.Int32);
                DbParameter[] Params = new DbParameter[5] { EnquiryIDP, CountryIDP, StateIDP, DistrictIDP, CustomerNameP };

                using (DataSet DataSet = provider.Select("GetEnquiry", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            projects.Add(new PEnquiry()
                            {
                                EnquiryID = Convert.ToInt64(dr["EnquiryID"]),
                                CustomerName = Convert.ToString(dr["CustomerName"]),
                                EnquiryDate = Convert.ToDateTime(dr["EnquiryDate"]),
                                EnquiryNumber = Convert.ToString(dr["EnquiryNumber"]),
                                Source = DBNull.Value == dr["LeadSourceID"] ? null : new PLeadSource()
                                {
                                    Source = Convert.ToString(dr["LeadSource"]),
                                    SourceID = Convert.ToInt32(dr["LeadSourceID"]),
                                },
                                Status = DBNull.Value == dr["PreSaleStatusID"] ? null : new PPreSaleStatus()
                                {
                                    Status = Convert.ToString(dr["PreSaleStatus"]),
                                    StatusID = Convert.ToInt32(dr["PreSaleStatusID"]),
                                },
                                Country = DBNull.Value == dr["CountryID"] ? null : new PDMS_Country()
                                {
                                    Country = Convert.ToString(dr["Country"]),
                                    CountryID = Convert.ToInt32(dr["CountryID"]),
                                },
                                State = DBNull.Value == dr["StateID"] ? null : new PDMS_State()
                                {
                                    State = Convert.ToString(dr["State"]),
                                    StateID = Convert.ToInt32(dr["StateID"]),
                                },
                                District = DBNull.Value == dr["DistrictID"] ? null : new PDMS_District()
                                {
                                    District = Convert.ToString(dr["District"]),
                                    DistrictID = Convert.ToInt32(dr["DistrictID"]),
                                },
                                Address = Convert.ToString(dr["Address"]),
                                PersonName = Convert.ToString(dr["PersonName"]),
                                Mobile = Convert.ToString(dr["Mobile"]),
                                Mail = Convert.ToString(dr["Mail"]),
                                Product = Convert.ToString(dr["Product"]),
                                Remarks = Convert.ToString(dr["Remarks"])
                            });
                        }
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
            return projects;
        }
        public Boolean InsertOrUpdateLeadFollowUpStatus(PLeadFollowUp FollowUpStatus, int UserID)
        {
            int success = 0;
            DbParameter LeadFollowUpID = provider.CreateParameter("LeadFollowUpID", FollowUpStatus.LeadFollowUpID, DbType.Int64);
            DbParameter Remark = provider.CreateParameter("Remark", FollowUpStatus.Remark, DbType.String);
            DbParameter StatusID = provider.CreateParameter("StatusID", FollowUpStatus.Status.StatusID, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);


            DbParameter[] Params = new DbParameter[4] { LeadFollowUpID, Remark, StatusID, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateLeadFollowUpStatus", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BLead", "InsertOrUpdateLeadFollowUp", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BLead", " InsertOrUpdateLeadFollowUp", ex);
                throw ex;
            }
            return true;
        }
    }
}
