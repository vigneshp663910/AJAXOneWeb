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
        public Boolean InsertOrUpdateEnquiry(PEnquiry enquiry,int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter EnquiryID = provider.CreateParameter("EnquiryID", enquiry.EnquiryID, DbType.Int32);
                DbParameter EnquiryDate = provider.CreateParameter("EnquiryDate", enquiry.EnquiryDate, DbType.DateTime); 
                DbParameter CustomerName = provider.CreateParameter("CustomerName", enquiry.CustomerName, DbType.String);
                DbParameter PersonName = provider.CreateParameter("PersonName", enquiry.PersonName, DbType.String);
                DbParameter Mail = provider.CreateParameter("Mail", enquiry.Mail, DbType.String);
                DbParameter Mobile = provider.CreateParameter("Mobile", enquiry.Mobile, DbType.String);
                DbParameter Address = provider.CreateParameter("Address", enquiry.Address, DbType.String);
                DbParameter Address2 = provider.CreateParameter("Address2", enquiry.Address2, DbType.String);
                DbParameter Address3 = provider.CreateParameter("Address3", enquiry.Address3, DbType.String);
                DbParameter SourceID = provider.CreateParameter("SourceID", enquiry.Source.SourceID, DbType.Int32); 
                DbParameter CountryID = provider.CreateParameter("CountryID", enquiry.Country.CountryID, DbType.Int32);
                DbParameter StateID = provider.CreateParameter("StateID", enquiry.State.StateID, DbType.Int32);
                DbParameter DistrictID = provider.CreateParameter("DistrictID", enquiry.District.DistrictID, DbType.Int32);                
                DbParameter Product = provider.CreateParameter("Product", enquiry.Product, DbType.String);
                DbParameter Remarks = provider.CreateParameter("Remarks", enquiry.Remarks, DbType.String);
                DbParameter CreatedBy = provider.CreateParameter("CreatedBy", UserID, DbType.Int32);
                DbParameter OutValue = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter[] Params = new DbParameter[17] { EnquiryID, EnquiryDate,  CustomerName, PersonName, Mail, Mobile, Address, Address2, Address3, SourceID,  CountryID, StateID, DistrictID, Product, Remarks, CreatedBy, OutValue };
                    provider.Insert("InsertOrUpdateEnquiry", Params);
                    scope.Complete();
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BProject", "InsertOrUpdateEnquiry", ex);
                return false;
            }
            return true;
        }
        public List<PEnquiry> GetEnquiry(long? EnquiryID, int? DealerID, string EnquiryNumber, string CustomerName, int? CountryID, int? StateID, int? DistrictID, DateTime? DateFrom, DateTime? DateTo, int? SourceID, int? StatusID)
        {
            List<PEnquiry> projects = new List<PEnquiry>();
            try
            {
                DbParameter EnquiryIDP = provider.CreateParameter("EnquiryID", EnquiryID, DbType.Int32); 
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32); 
                DbParameter EnquiryNumberP = provider.CreateParameter("EnquiryNumber", EnquiryNumber == "" ? null : EnquiryNumber, DbType.String);
                DbParameter CustomerNameP = provider.CreateParameter("CustomerName", string.IsNullOrEmpty(CustomerName) ? null : CustomerName, DbType.String);
                DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter DistrictIDP = provider.CreateParameter("DistrictID", DistrictID, DbType.Int32);
                DbParameter DateFromP = provider.CreateParameter("DateFrom", DateFrom, DbType.DateTime);
                DbParameter DateToP = provider.CreateParameter("DateTo", DateTo, DbType.DateTime);
                DbParameter SourceIDP = provider.CreateParameter("SourceID", SourceID, DbType.Int32);
                DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
                DbParameter[] Params = new DbParameter[11] { EnquiryIDP, DealerIDP, EnquiryNumberP, CountryIDP, StateIDP, DistrictIDP, CustomerNameP, DateFromP, DateToP, SourceIDP, StatusIDP };

                using (DataSet DataSet = provider.Select("GetEnquiry", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            projects.Add(new PEnquiry()
                            {
                                EnquiryID = Convert.ToInt64(dr["EnquiryID"]),
                                LeadID = DBNull.Value == dr["LeadID"] ? (long?)null: Convert.ToInt64(dr["LeadID"]),
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
                                Address2 = Convert.ToString(dr["Address2"]),
                                Address3 = Convert.ToString(dr["Address3"]),
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
        public Boolean UpdateEnquiryReject(long EnquiryID,string Remark, int UserID)
        {
            int success = 0;
            DbParameter EnquiryIDP = provider.CreateParameter("EnquiryID", EnquiryID, DbType.Int64);
            DbParameter RemarkP = provider.CreateParameter("Remarks", Remark, DbType.String); 
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);


            DbParameter[] Params = new DbParameter[3] { EnquiryIDP, RemarkP,  UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("UpdateEnquiryReject", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BEnquiry", "UpdateEnquiryReject", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BEnquiry", " UpdateEnquiryReject", ex);
                throw ex;
            }
            return true;
        }
    }
}
