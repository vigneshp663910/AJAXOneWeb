using DataAccess;
using Newtonsoft.Json;
using Properties;
using SapIntegration;
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
        public void EnquirySync(DateTime From, DateTime To, string EnquiryNo, string DealerCode, string CustomerCode)
        {
            DataTable dtEnquiry  = new SEnquiry().getEnquiry(From, To, EnquiryNo, DealerCode, CustomerCode);
            foreach (DataRow dr in dtEnquiry.Rows)
            {
                List<PLeadSource> DBMasterSource = new BLead().GetLeadSource(null, null);
                List<PLeadSource> Source = new BLead().GetLeadSource(null, dr["SOURCE"].ToString());
                List<PDMS_State> State = new BDMS_Address().GetState(1, null, null, dr["C_STATE"].ToString());
                List<PDMS_District> District = new BDMS_Address().GetDistrict(1, null, (State.Count == 0) ?(int?)null: Convert.ToInt32(State[0].StateID), null, string.IsNullOrEmpty(dr["C_DISTRICT"].ToString())? "UNKNOWN": dr["C_DISTRICT"].ToString(), null);
                Int32? SourceID = null;
                if(dr["SOURCE"].ToString().Contains("Through Hoardings") || dr["SOURCE"].ToString().Contains("Dontuse") || dr["SOURCE"].ToString().Contains("Direct Enquiry"))
                {
                    SourceID = DBMasterSource[10].SourceID;
                }
                else if(dr["SOURCE"].ToString().Contains("Indiamart -  Email")|| dr["SOURCE"].ToString().Contains("Indiamart- Call"))
                {
                    SourceID = DBMasterSource[8].SourceID;
                }
                else if (dr["SOURCE"].ToString().Contains("Indiamart-Buylead"))
                {
                    SourceID = DBMasterSource[7].SourceID;
                }
                else if (dr["SOURCE"].ToString().Contains("Monsoon Scheme") || dr["SOURCE"].ToString().Contains("Billboard Campaign"))
                {
                    SourceID = DBMasterSource[6].SourceID;
                }
                else if (dr["SOURCE"].ToString().Contains("Exhibition"))
                {
                    SourceID = DBMasterSource[5].SourceID;
                }
                else if (dr["SOURCE"].ToString().Contains("Existing Customer-Repeat order"))
                {
                    SourceID = DBMasterSource[3].SourceID;
                }

                TraceLogger.Log(DateTime.Now);
                try
                {
                    //DbParameter EnquiryID = provider.CreateParameter("EnquiryID", enquiry.EnquiryID, DbType.Int32);
                    DbParameter EnquiryDate = provider.CreateParameter("EnquiryDate", Convert.ToDateTime(dr["CFCDAT"].ToString()), DbType.DateTime);
                    DbParameter SapEnquiryNumber = provider.CreateParameter("SapEnquiryNumber", dr["S_OPPID"].ToString(), DbType.String);
                    DbParameter CustomerName = provider.CreateParameter("CustomerName", dr["NAME"].ToString(), DbType.String);
                    DbParameter PersonName = provider.CreateParameter("PersonName", dr["C_NO"].ToString(), DbType.String);
                    DbParameter Mail = provider.CreateParameter("Mail", "", DbType.String);
                    DbParameter Mobile = provider.CreateParameter("Mobile", dr["C_PER"].ToString(), DbType.String);
                    DbParameter Address = provider.CreateParameter("Address", dr["C_ADDR1"].ToString(), DbType.String);
                    DbParameter Address2 = provider.CreateParameter("Address2", dr["C_ADDR2"].ToString(), DbType.String);
                    DbParameter Address3 = provider.CreateParameter("Address3", dr["C_STREET"].ToString(), DbType.String);
                    DbParameter SourceIDP = provider.CreateParameter("SourceID", SourceID, DbType.Int32);
                    DbParameter CountryID = provider.CreateParameter("CountryID", 1, DbType.Int32);
                    DbParameter StateID = provider.CreateParameter("StateID", (State.Count == 0) ? (Int32?)null : State[0].StateID, DbType.Int32);
                    DbParameter DistrictID = provider.CreateParameter("DistrictID", (District.Count==0)? (Int32?)null:District[0].DistrictID, DbType.Int32);
                    DbParameter Product = provider.CreateParameter("Product", dr["PRODUCT"].ToString(), DbType.String);
                    DbParameter Remarks = provider.CreateParameter("Remarks", dr["ENQ_REASON"].ToString(), DbType.String);
                    DbParameter CreatedBy = provider.CreateParameter("CreatedBy", PSession.User.UserID, DbType.Int32);
                    DbParameter OutValue = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        DbParameter[] Params = new DbParameter[17] { /*EnquiryID, */EnquiryDate, SapEnquiryNumber, CustomerName, PersonName, Mail, Mobile, Address, Address2, Address3, SourceIDP, CountryID, StateID, DistrictID, Product, Remarks, CreatedBy, OutValue };
                        provider.Insert("InsertOrUpdateEnquiryFromSAP", Params);
                        scope.Complete();
                    }
                    TraceLogger.Log(DateTime.Now);
                }
                catch (Exception ex)
                {
                    new FileLogger().LogMessageService("BEnquiry", "InsertOrUpdateEnquiry", ex);
                }
            }
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
                new FileLogger().LogMessageService("BEnquiry", "InsertOrUpdateEnquiry", ex);
                return false;
            }
            return true;
        }
        public List<PEnquiry> GetEnquiry(long? EnquiryID, int? DealerID, string EnquiryNumber, string CustomerName, int? CountryID, int? StateID, int? DistrictID, DateTime? DateFrom, DateTime? DateTo, int? SourceID, int? StatusID,int UserID)
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
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[12] { EnquiryIDP, DealerIDP, EnquiryNumberP, CountryIDP, StateIDP, DistrictIDP, CustomerNameP, DateFromP, DateToP, SourceIDP, StatusIDP, UserIDP };

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
                                Remarks = Convert.ToString(dr["Remarks"]),
                                CreatedBy = new PUser() { ContactName = Convert.ToString(dr["CreatedByName"]) },
                                CreatedOn = Convert.ToDateTime(dr["CreatedOn"])

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

        public List<PPreSaleStatus> GetEnquiryCountByStatus(DateTime? From, DateTime? To, int? DealerID, int? UserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Enquiry/CountByStatus?From=" + From + "&To=" + To + "&DealerID=" + DealerID + "&UserID=" + UserID;
            return JsonConvert.DeserializeObject<List<PPreSaleStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}
