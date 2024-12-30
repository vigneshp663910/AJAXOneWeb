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
   public class BEnquiry
    {
        private IDataAccess provider;
        public BEnquiry()
        {
            provider = new ProviderFactory().GetProvider();
        }
        //public void EnquirySync(DateTime From, DateTime To, string EnquiryNo, string DealerCode, string CustomerCode)
        //{
        //    DataTable dtEnquiry  = new SEnquiry().getEnquiry(From, To, EnquiryNo, DealerCode, CustomerCode);
        //    foreach (DataRow dr in dtEnquiry.Rows)
        //    {
        //        List<PLeadSource> DBMasterSource = new BLead().GetLeadSource(null, null);
        //        List<PLeadSource> Source = new BLead().GetLeadSource(null, dr["SOURCE"].ToString());
        //        List<PDMS_State> State = new BDMS_Address().GetState(null, 1, null, null, dr["C_STATE"].ToString());
        //        List<PDMS_District> District = new BDMS_Address().GetDistrict(1, null, (State.Count == 0) ?(int?)null: Convert.ToInt32(State[0].StateID), null, string.IsNullOrEmpty(dr["C_DISTRICT"].ToString())? "UNKNOWN": dr["C_DISTRICT"].ToString(), null);
        //        Int32? SourceID = null;
        //        if(dr["SOURCE"].ToString().Contains("Through Hoardings") || dr["SOURCE"].ToString().Contains("Dontuse") || dr["SOURCE"].ToString().Contains("Direct Enquiry"))
        //        {
        //            SourceID = DBMasterSource[10].SourceID;
        //        }
        //        else if(dr["SOURCE"].ToString().Contains("Indiamart -  Email")|| dr["SOURCE"].ToString().Contains("Indiamart- Call"))
        //        {
        //            SourceID = DBMasterSource[8].SourceID;
        //        }
        //        else if (dr["SOURCE"].ToString().Contains("Indiamart-Buylead"))
        //        {
        //            SourceID = DBMasterSource[7].SourceID;
        //        }
        //        else if (dr["SOURCE"].ToString().Contains("Monsoon Scheme") || dr["SOURCE"].ToString().Contains("Billboard Campaign"))
        //        {
        //            SourceID = DBMasterSource[6].SourceID;
        //        }
        //        else if (dr["SOURCE"].ToString().Contains("Exhibition"))
        //        {
        //            SourceID = DBMasterSource[5].SourceID;
        //        }
        //        else if (dr["SOURCE"].ToString().Contains("Existing Customer-Repeat order"))
        //        {
        //            SourceID = DBMasterSource[3].SourceID;
        //        }

        //        TraceLogger.Log(DateTime.Now);
        //        try
        //        {
        //            //DbParameter EnquiryID = provider.CreateParameter("EnquiryID", enquiry.EnquiryID, DbType.Int32);
        //            DbParameter EnquiryDate = provider.CreateParameter("EnquiryDate", Convert.ToDateTime(dr["CFCDAT"].ToString()), DbType.DateTime);
        //            DbParameter SapEnquiryNumber = provider.CreateParameter("SapEnquiryNumber", dr["S_OPPID"].ToString(), DbType.String);
        //            DbParameter CustomerName = provider.CreateParameter("CustomerName", dr["NAME"].ToString(), DbType.String);
        //            DbParameter PersonName = provider.CreateParameter("PersonName", dr["C_NO"].ToString(), DbType.String);
        //            DbParameter Mail = provider.CreateParameter("Mail", "", DbType.String);
        //            DbParameter Mobile = provider.CreateParameter("Mobile", dr["C_PER"].ToString(), DbType.String);
        //            DbParameter Address = provider.CreateParameter("Address", dr["C_ADDR1"].ToString(), DbType.String);
        //            DbParameter Address2 = provider.CreateParameter("Address2", dr["C_ADDR2"].ToString(), DbType.String);
        //            DbParameter Address3 = provider.CreateParameter("Address3", dr["C_STREET"].ToString(), DbType.String);
        //            DbParameter SourceIDP = provider.CreateParameter("SourceID", SourceID, DbType.Int32);
        //            DbParameter CountryID = provider.CreateParameter("CountryID", 1, DbType.Int32);
        //            DbParameter StateID = provider.CreateParameter("StateID", (State.Count == 0) ? (Int32?)null : State[0].StateID, DbType.Int32);
        //            DbParameter DistrictID = provider.CreateParameter("DistrictID", (District.Count==0)? (Int32?)null:District[0].DistrictID, DbType.Int32);
        //            DbParameter Product = provider.CreateParameter("Product", dr["PRODUCT"].ToString(), DbType.String);
        //            DbParameter Remarks = provider.CreateParameter("Remarks", dr["ENQ_REASON"].ToString(), DbType.String);
        //            DbParameter CreatedBy = provider.CreateParameter("CreatedBy", PSession.User.UserID, DbType.Int32);
        //            DbParameter OutValue = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
        //            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //            {
        //                DbParameter[] Params = new DbParameter[17] { /*EnquiryID, */EnquiryDate, SapEnquiryNumber, CustomerName, PersonName, Mail, Mobile, Address, Address2, Address3, SourceIDP, CountryID, StateID, DistrictID, Product, Remarks, CreatedBy, OutValue };
        //                provider.Insert("InsertOrUpdateEnquiryFromSAP", Params);
        //                scope.Complete();
        //            }
        //            TraceLogger.Log(DateTime.Now);
        //        }
        //        catch (Exception ex)
        //        {
        //            new FileLogger().LogMessageService("BEnquiry", "InsertOrUpdateEnquiry", ex);
        //        }
        //    }
        //}
        public Boolean InsertOrUpdateEnquiry(PEnquiry enquiry,int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                DbParameter EnquiryID = provider.CreateParameter("EnquiryID", enquiry.EnquiryID, DbType.Int32);
                DbParameter EnquiryNextFollowUpDate = provider.CreateParameter("EnquiryNextFollowUpDate", enquiry.EnquiryNextFollowUpDate, DbType.DateTime); 
                DbParameter CustomerName = provider.CreateParameter("CustomerName", enquiry.CustomerName, DbType.String);
                DbParameter PersonName = provider.CreateParameter("PersonName", enquiry.PersonName, DbType.String);
                DbParameter Mail = provider.CreateParameter("Mail", enquiry.Mail, DbType.String);
                DbParameter Mobile = provider.CreateParameter("Mobile", enquiry.Mobile, DbType.String);
                DbParameter Address = provider.CreateParameter("Address", enquiry.Address, DbType.String);
                DbParameter Address2 = provider.CreateParameter("Address2", enquiry.Address2, DbType.String);
                DbParameter Address3 = provider.CreateParameter("Address3", enquiry.Address3, DbType.String);
                DbParameter ProductTypeID = provider.CreateParameter("ProductTypeID", enquiry.ProductType.ProductTypeID, DbType.Int32);
                DbParameter SourceID = provider.CreateParameter("SourceID", enquiry.Source.SourceID, DbType.Int32); 
                DbParameter CountryID = provider.CreateParameter("CountryID", enquiry.Country.CountryID, DbType.Int32);
                DbParameter StateID = provider.CreateParameter("StateID", enquiry.State.StateID, DbType.Int32);
                DbParameter DistrictID = provider.CreateParameter("DistrictID", enquiry.District.DistrictID, DbType.Int32);                
                DbParameter Product = provider.CreateParameter("Product", enquiry.Product, DbType.String);
                DbParameter Remarks = provider.CreateParameter("Remarks", enquiry.Remarks, DbType.String);
                DbParameter SalesChannelTypeID = provider.CreateParameter("SalesChannelTypeID", enquiry.SalesChannelType.MasterItemID, DbType.Boolean);
                DbParameter CreatedBy = provider.CreateParameter("CreatedBy", UserID, DbType.Int32);
                DbParameter OutValue = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter[] Params = new DbParameter[19] { EnquiryID, EnquiryNextFollowUpDate,   CustomerName, PersonName, Mail, Mobile, Address, Address2, Address3, ProductTypeID, SourceID,  CountryID, StateID, DistrictID, Product, Remarks, SalesChannelTypeID, CreatedBy, OutValue };
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
        public PApiResult GetEnquiry(long? EnquiryID, int? DealerID, int? EngineerUserID, string EnquiryNumber, string CustomerName
            , int? CountryID, int? StateID, int? DistrictID, DateTime? DateFrom, DateTime? DateTo, int? SourceID
            , int? StatusID, int? PageIndex = null, int? PageSize = null)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Enquiry/GetEnquiry?EnquiryID=" + EnquiryID + "&DealerID=" + DealerID + "&EngineerUserID=" + EngineerUserID + "&EnquiryNumber=" + EnquiryNumber
                + "&CustomerName=" + CustomerName + "&CountryID=" + CountryID + "&StateID=" + StateID + "&DistrictID=" + DistrictID
                + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&SourceID=" + SourceID + "&StatusID=" + StatusID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetEnquiryExcel(long? EnquiryID, int? DealerID, int? EngineerUserID, string EnquiryNumber, string CustomerName
            , int? CountryID, int? StateID, int? DistrictID, DateTime? DateFrom, DateTime? DateTo, int? SourceID, int? StatusID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Enquiry/GetEnquiryExcel?EnquiryID=" + EnquiryID + "&DealerID=" + DealerID + "&EngineerUserID=" + EngineerUserID + "&EnquiryNumber=" + EnquiryNumber
                + "&CustomerName=" + CustomerName + "&CountryID=" + CountryID + "&StateID=" + StateID + "&DistrictID=" + DistrictID
                + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&SourceID=" + SourceID + "&StatusID=" + StatusID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PEnquiry GetEnquiryByID(long? EnquiryID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Enquiry/EnquiryByID?EnquiryID=" + EnquiryID ; 
            return JsonConvert.DeserializeObject<PEnquiry>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        //public Boolean UpdateEnquiryReject(long EnquiryID,string Remark, int UserID)
        //{
        //    int success = 0;
        //    DbParameter EnquiryIDP = provider.CreateParameter("EnquiryID", EnquiryID, DbType.Int64);
        //    DbParameter RemarkP = provider.CreateParameter("Remarks", Remark, DbType.String); 
        //    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);


        //    DbParameter[] Params = new DbParameter[3] { EnquiryIDP, RemarkP,  UserIDP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            success = provider.Insert("UpdateEnquiryReject", Params);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BEnquiry", "UpdateEnquiryReject", sqlEx);
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BEnquiry", " UpdateEnquiryReject", ex);
        //        throw ex;
        //    }
        //    return true;
        //}

        public List<PPreSaleStatus> GetEnquiryCountByStatus(DateTime? From, DateTime? To, int? DealerID, int? EngineerUserID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Enquiry/CountByStatus?From=" + From + "&To=" + To + "&DealerID=" + DealerID + "&EngineerUserID=" + EngineerUserID ;
            return JsonConvert.DeserializeObject<List<PPreSaleStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetEnquiryTimeLineReport(string DateFrom, string DateTo, int? DealerID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Enquiry/EnquiryTimeLineReport?DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&DealerID=" + DealerID;
            //return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            //  TraceLogger.Log(DateTime.Now);
        }


        public DataTable GetEnquiryIndiamart(DateTime? DateFrom, DateTime? DateTo, int? PreSaleStatusID, int? SourceID, int? PageIndex = null, int? PageSize = null)
        {
            DbParameter DateFromP = provider.CreateParameter("DateFrom", DateFrom, DbType.DateTime);
            DbParameter DateToP = provider.CreateParameter("DateTo", DateTo, DbType.DateTime);
            DbParameter PreSaleStatusIDP = provider.CreateParameter("PreSaleStatusID", PreSaleStatusID, DbType.Int32);
            DbParameter SourceIDP = provider.CreateParameter("SourceID", SourceID, DbType.Int32);
            DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
            DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);
            try
            {
                using (DataSet DS = provider.Select("GetEnquiryIndiamart", new DbParameter[6] { DateFromP, DateToP, PreSaleStatusIDP, SourceIDP, PageIndexP, PageSizeP }))
                {
                    if (DS != null)
                    {
                        return DS.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public DataTable GetEnquiryIndiamartByID(long? EnquiryIndiamartID)
        {
            DbParameter EnquiryIndiamartIDP = provider.CreateParameter("EnquiryIndiamartID", EnquiryIndiamartID, DbType.Int64);
            try
            {
                using (DataSet DS = provider.Select("GetEnquiryIndiamartByID", new DbParameter[1] { EnquiryIndiamartIDP }))
                {
                    if (DS != null)
                    {
                        return DS.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public DataTable GetEnquiryIndiamartStatusHistory(long? EnquiryIndiamartID)
        {
            DbParameter EnquiryIndiamartIDP = provider.CreateParameter("EnquiryIndiamartID", EnquiryIndiamartID, DbType.Int64);
            try
            {
                using (DataSet DS = provider.Select("GetEnquiryIndiamartStatusHistory", new DbParameter[1] { EnquiryIndiamartIDP }))
                {
                    if (DS != null)
                    {
                        return DS.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public Boolean UpdateEnquiryIndiamartStatus(long EnquiryIndiamartID, int? EnquiryRemarkID, int StatusID, string Reason, int UserID)
        {
            try
            {
                provider = new ProviderFactory().GetProvider();
                DbParameter EnquiryIndiamartIDP = provider.CreateParameter("EnquiryIndiamartID", EnquiryIndiamartID, DbType.Int64);
                DbParameter EnquiryRemarkIDP = provider.CreateParameter("EnquiryRemarkID", EnquiryRemarkID, DbType.Int32);
                DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
                DbParameter ReasonP = provider.CreateParameter("Reason", Reason, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

                DbParameter[] Params = new DbParameter[5] { EnquiryIndiamartIDP, EnquiryRemarkIDP, StatusIDP, ReasonP, UserIDP };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("UpdateEnquiryIndiamartStatus", Params);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public List<PEnquiryRemark> GetEnquiryRemark(int? EnquiryRemarkID, string Remark, bool? IsIndiaMartInProgress, bool? IsIndiaMartReject, bool? IsInProgress, bool? IsReject)
        {
            List<PEnquiryRemark> pEnquiryRemarks = new List<PEnquiryRemark>();
            try
            {
                DbParameter EnquiryRemarkIDP = provider.CreateParameter("EnquiryRemarkID", EnquiryRemarkID, DbType.Int32);
                DbParameter RemarkP = provider.CreateParameter("Remark", Remark, DbType.String);
                DbParameter IsIndiaMartInProgressP = provider.CreateParameter("IsIndiaMartInProgress", IsIndiaMartInProgress, DbType.Boolean);
                DbParameter IsIndiaMartRejectP = provider.CreateParameter("IsIndiaMartReject", IsIndiaMartReject, DbType.Boolean);
                DbParameter IsInProgressP = provider.CreateParameter("IsInProgress", IsInProgress, DbType.Boolean);
                DbParameter IsRejectP = provider.CreateParameter("IsReject", IsReject, DbType.Boolean);

                DbParameter[] Params = new DbParameter[6] { EnquiryRemarkIDP, RemarkP, IsIndiaMartInProgressP, IsIndiaMartRejectP, IsInProgressP, IsRejectP };
                using (DataSet DataSet = provider.Select("GetEnquiryRemark", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            pEnquiryRemarks.Add(new PEnquiryRemark()
                            {
                                EnquiryRemarkID = Convert.ToInt32(dr["EnquiryRemarkID"]),
                                Remark = Convert.ToString(dr["Remark"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BEnquiry", "GetEnquiryRemark", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BEnquiry", "GetEnquiryRemark", ex);
                throw ex;
            }
            return pEnquiryRemarks;
        }

        public DataTable GetEnquiryRegionWiseCount(string DateFrom, string DateTo, string DealerID,  int? CountryID, string RegionID, string ProductTypeID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Enquiry/EnquiryRegionWiseCount?DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Dealer=" + DealerID
                + "&Country=" + CountryID + "&Region=" + RegionID + "&ProductType=" + ProductTypeID;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public DataTable GetEnquirySourceWiseCount(string DateFrom, string DateTo, string DealerID, int? CountryID, string RegionID, string ProductTypeID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Enquiry/EnquirySourceWiseCount?DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Dealer=" + DealerID
                + "&Country=" + CountryID + "&Region=" + RegionID + "&ProductType=" + ProductTypeID;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public DataSet GetEnquiryConversionPercentage(string DateFrom, string DateTo, string DealerID, int? CountryID, string RegionID, string ProductTypeID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Enquiry/EnquiryConversionPercentage?DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Dealer=" + DealerID
                + "&Country=" + CountryID + "&Region=" + RegionID + "&ProductType=" + ProductTypeID;
            return JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataSet GetEnquiryVelocityCount(string DateFrom, string DateTo, string DealerID, int? CountryID, string RegionID, string ProductTypeID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Enquiry/GetEnquiryVelocityCount?DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&Dealer=" + DealerID
                + "&Country=" + CountryID + "&Region=" + RegionID + "&ProductType=" + ProductTypeID;
            return JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataTable GetEnquiryStatusHistory(long EnquiryID)
        {
            DbParameter EnquiryIDP = provider.CreateParameter("EnquiryID", EnquiryID, DbType.Int64);
            try
            {
                using (DataSet DS = provider.Select("GetEnquiryStatusHistory", new DbParameter[1] { EnquiryIDP }))
                {
                    if (DS != null)
                    {
                        return DS.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public Boolean UpdateEnquiryStatus(long EnquiryID, int EnquiryRemarkID, int StatusID, string Remark, int UserID)
        {
            try
            {
                provider = new ProviderFactory().GetProvider();
                DbParameter EnquiryIDP = provider.CreateParameter("EnquiryID", EnquiryID, DbType.Int64);
                DbParameter EnquiryRemarkIDP = provider.CreateParameter("EnquiryRemarkID", EnquiryRemarkID, DbType.Int32);
                DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
                DbParameter RemarkP = provider.CreateParameter("Reason", Remark, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                

                DbParameter[] Params = new DbParameter[5] { EnquiryIDP, EnquiryRemarkIDP, StatusIDP, RemarkP, UserIDP };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("UpdateEnquiryReject", Params);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public DataTable GetEnquiryUnattendedAgeing(int? DealerID, int? RegionID)
        {
            string endPoint = "Enquiry/GetEnquiryUnattendedAgeing?DealerID=" + DealerID + "&RegionID=" + RegionID;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataTable GetEnquiryUnattendedAgeingDetails(int? DealerID, int? RegionID, int ReportTypeID)
        {
            string endPoint = "Enquiry/GetEnquiryUnattendedAgeingDetails?DealerID=" + DealerID + "&RegionID=" + RegionID + "&ReportTypeID=" + ReportTypeID;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

    }  
}
