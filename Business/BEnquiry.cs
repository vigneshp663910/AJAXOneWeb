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
        public PEnquiry GetEnquiryByID(long? EnquiryID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Enquiry/EnquiryByID?EnquiryID=" + EnquiryID ; 
            return JsonConvert.DeserializeObject<PEnquiry>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
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
    }
}
