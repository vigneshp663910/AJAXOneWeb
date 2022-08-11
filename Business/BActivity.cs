using Newtonsoft.Json;
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
    public class BActivity
    {
        private IDataAccess provider;
        public BActivity()
        {
            provider = new ProviderFactory().GetProvider();
        }

        //public List<PActivityType> GetActivityType(int? ActivityTypeID, string ActivityTypeCode, DateTime? LastSyncDate)
        //{
        //    List<PActivityType> ActivityTypes = new List<PActivityType>();
        //    PActivityType ActivityType = null;
        //    DbParameter ActivityTypeIDP = provider.CreateParameter("ActivityTypeID", ActivityTypeID, DbType.Int32);
        //    DbParameter ActivityTypeCodeP = provider.CreateParameter("ActivityTypeCode", string.IsNullOrEmpty(ActivityTypeCode) ? null : ActivityTypeCode, DbType.String);
        //    DbParameter IsActiveP = provider.CreateParameter("LastSyncDate", LastSyncDate, DbType.DateTime);

        //    DbParameter[] Params = new DbParameter[3] { ActivityTypeIDP, ActivityTypeCodeP, IsActiveP };
        //    try
        //    {
        //        using (DataSet ds = provider.Select("ZDMS_GetActivityType", Params))
        //        {
        //            if (ds != null)
        //            {
        //                foreach (DataRow dr in ds.Tables[0].Rows)
        //                {
        //                    ActivityType = new PActivityType();
        //                    ActivityType.ActivityTypeID = Convert.ToInt32(dr["ActivityTypeID"]);
        //                    ActivityType.ActivityTypeCode = Convert.ToString(dr["ActivityTypeCode"]);
        //                    ActivityType.ActivityTypeName = Convert.ToString(dr["ActivityTypeName"]); ActivityTypes.Add(ActivityType);
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return ActivityTypes;
        //}

        public List<PActivityType> GetActivityType(int? ActivityTypeID, string ActivityTypeCode)
        {
            string endPoint = "Activity/ActivityType?ActivityTypeID=" + ActivityTypeID + "&ActivityTypeCode=" + ActivityTypeCode ;
            return JsonConvert.DeserializeObject<List<PActivityType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        //public List<PActivityReferenceType> GetActivityReferenceType(int? ActivityReferenceTableID, string ReferenceTable)
        //{
        //    List<PActivityReferenceType> ActivityReferenceTypes = new List<PActivityReferenceType>();
        //    PActivityReferenceType ActivityReferenceType1 = null;
        //    DbParameter ActivityReferenceTypeIDP = provider.CreateParameter("ActivityReferenceTableID", ActivityReferenceTableID, DbType.Int32);
        //    DbParameter ActivityReferenceTypeP = provider.CreateParameter("ReferenceTable", string.IsNullOrEmpty(ReferenceTable) ? null : @ReferenceTable, DbType.String);

        //    DbParameter[] Params = new DbParameter[2] { ActivityReferenceTypeIDP, ActivityReferenceTypeP };
        //    try
        //    {
        //        using (DataSet ds = provider.Select("GetActivityReferenceType", Params))
        //        {
        //            if (ds != null)
        //            {
        //                foreach (DataRow dr in ds.Tables[0].Rows)
        //                {
        //                    ActivityReferenceType1 = new PActivityReferenceType();
        //                    ActivityReferenceType1.ActivityReferenceTableID = Convert.ToInt32(dr["ActivityReferenceTableID"]);
        //                    ActivityReferenceType1.ReferenceTable = Convert.ToString(dr["ReferenceTable"]);
        //                    ActivityReferenceTypes.Add(ActivityReferenceType1);
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return ActivityReferenceTypes;
        //}

        public List<PActivityReferenceType> GetActivityReferenceType(int? ActivityReferenceTableID, string ReferenceTable)
        {
            string endPoint = "Activity/ActivityReferenceType?ActivityTypeID=" + ActivityReferenceTableID + "&ReferenceTable=" + ReferenceTable;
            return JsonConvert.DeserializeObject<List<PActivityReferenceType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        //public List<PActivity> GetActivity(PActivitySearch ActivityS, int UserID)
        //{
        //    TraceLogger.Log(DateTime.Now);
        //    List<PActivity> Activities = new List<PActivity>();
        //    try
        //    {
        //        DbParameter ActivityTypeIDP = provider.CreateParameter("ActivityTypeID", ActivityS.ActivityTypeID, DbType.Int32);
        //        DbParameter ActivityIDP = provider.CreateParameter("ActivityID", ActivityS.ActivityID, DbType.Int32);
        //        DbParameter ActivityDateFromP = provider.CreateParameter("ActivityDateFrom", ActivityS.ActivityDateFrom, DbType.DateTime);
        //        DbParameter ActivityDateToP = provider.CreateParameter("ActivityDateTo", ActivityS.ActivityDateTo, DbType.DateTime);
        //        //DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", String.IsNullOrEmpty(ActivityS.CustomerCode) ? null : ActivityS.CustomerCode, DbType.String);
        //        //DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", String.IsNullOrEmpty(ActivityS.EquipmentSerialNo) ? null : ActivityS.EquipmentSerialNo, DbType.String);
        //        DbParameter ActivityReferenceTableIDP = provider.CreateParameter("ActivityReferenceTableID", ActivityS.ActivityReferenceTableID, DbType.Int32);
        //        DbParameter ReferenceNumberP = provider.CreateParameter("ReferenceNumber", String.IsNullOrEmpty(ActivityS.ReferenceNumber) ? null : ActivityS.ReferenceNumber, DbType.String);
        //        DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

        //        DbParameter[] Params = new DbParameter[7] { ActivityTypeIDP, ActivityIDP, ActivityDateFromP, ActivityDateToP, ActivityReferenceTableIDP, ReferenceNumberP, UserIDP };


        //        PActivity Activity = new PActivity();
        //        using (DataSet DataSet = provider.Select("GetActivity", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    Activity = new PActivity();
        //                    Activity.ActivityID = Convert.ToInt64(dr["ActivityID"]);
        //                    Activity.ActivityType = new PActivityType() { ActivityTypeName = Convert.ToString(dr["ActivityTypeName"]) };
        //                    //Activity.Customer = new PDMS_Customer()
        //                    //{
        //                    //    CustomerCode = Convert.ToString(dr["CustomerCode"]),
        //                    //    CustomerName = Convert.ToString(dr["CustomerName"]),
        //                    //};
        //                    Activity.ActivityStartDate = Convert.ToDateTime(dr["StartDate"]);
        //                    Activity.ActivityEndDate = dr["EndDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["EndDate"]);
        //                    Activity.Amount = dr["Amount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Amount"]);
        //                    Activity.Location = Convert.ToString(dr["Location"]);
        //                    //Activity.Equipment = new PDMS_EquipmentHeader() { EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]) };
        //                    Activity.ActivityReference = new PActivityReferenceType() { ReferenceTable = Convert.ToString(dr["ReferenceActivity"]) };
        //                    Activity.ReferenceNumber = Convert.ToString(dr["ReferenceNumber"]);
        //                    Activity.Remark = Convert.ToString(dr["Remark"]);

        //                    Activity.ActivityStartLatitude = dr["StartLatitude"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["StartLatitude"]);
        //                    Activity.ActivityStartLongitude = dr["StartLongitude"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["StartLongitude"]);
        //                    Activity.ActivityEndLatitude = dr["EndLatitude"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["EndLatitude"]);
        //                    Activity.ActivityEndLongitude = dr["EndLongitude"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["EndLongitude"]);
        //                    Activity.StartMapImage = Convert.ToString(dr["StartMapImage"]);
        //                    Activity.EndMapImage = Convert.ToString(dr["EndMapImage"]);
        //                    Activity.StartLatitudeLongitudeDate = Convert.ToString(dr["StartLatitudeLongitudeDate"]);
        //                    Activity.EndLatitudeLongitudeDate = Convert.ToString(dr["EndLatitudeLongitudeDate"]);
        //                    Activities.Add(Activity);
        //                }
        //            }
        //        }
        //        return Activities;
        //        TraceLogger.Log(DateTime.Now);
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BActivity", "GetActivity", ex);
        //        throw ex;
        //    }
        //    return Activities;
        //}

        //public List<PActivity> GetActivity(PActivitySearch ActivityS)
        //{
        //    string endPoint = "Activity?" + JsonConvert.SerializeObject(ActivityS);
        //    return JsonConvert.DeserializeObject<List<PActivity>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Activity/GetActivities", ActivityS)).Data));
        //}

        public List<PActivity> GetActivity(long? ActivityID, int? ActivityTypeID, string ActivityDateFrom, string ActivityDateTo, int? ActivityReferenceTableID, string ReferenceNumber)
        {
            string endPoint = "Activity/GetActivities?ActivityID=" + ActivityID + "&ActivityTypeID=" + ActivityTypeID + "&ActivityDateFrom=" + ActivityDateFrom + "&ActivityDateTo=" + ActivityDateTo + "&ActivityReferenceTableID=" + ActivityReferenceTableID + "&ReferenceNumber=" + ReferenceNumber;
            return JsonConvert.DeserializeObject<List<PActivity>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }


        public List<PActivity> GetActivityByID(long? ActivityID, long? VisitID)
        {
            string endPoint = "Activity/GetActivityByID?ActivityID=" + ActivityID + "&VisitID=" + VisitID;
            return JsonConvert.DeserializeObject<List<PActivity>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PActivity> GetActivityWithVisitByID(long? ActivityID, long? VisitID)
        {
            string endPoint = "Activity/GetActivityWithVisitByID?ActivityID=" + ActivityID + "&VisitID=" + VisitID;
            return JsonConvert.DeserializeObject<List<PActivity>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        
        //public long InsertOrUpdateActivity(PActivity Activity)
        //{
        //    int success;
        //    long ActivityOut = 0;
        //    DbParameter ActivityID = provider.CreateParameter("ActivityID", Activity.ActivityID, DbType.Int64);
        //    DbParameter ActivitySalesEngineerUserIDP = provider.CreateParameter("ActivitySalesEngineerUserID", Activity.SalesEngineer.UserID, DbType.Int32);
        //    //DbParameter ActivityTypeIDP = provider.CreateParameter("ActivityTypeID", Activity.ActivityType == null? (int?)null: Activity.ActivityType.ActivityTypeID, DbType.Int32);
        //    DbParameter ActivityTypeIDP = provider.CreateParameter("ActivityTypeID", Activity.ActivityType.ActivityTypeID, DbType.Int32);
        //    DbParameter ActivityStartDateP = provider.CreateParameter("ActivityStartDate", Activity.ActivityStartDate, DbType.DateTime);
        //    DbParameter ActivityEndDateP = provider.CreateParameter("ActivityEndDate", Activity.ActivityEndDate, DbType.DateTime);
        //    DbParameter LocationP = provider.CreateParameter("Location", Activity.Location, DbType.String);
        //    //DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", Activity.Customer == null ?  null : Activity.Customer.CustomerCode, DbType.String);
        //    //DbParameter CustomerIDP = provider.CreateParameter("CustomerID", Activity.Customer == null ? (Int64?)null : Activity.Customer.CustomerID, DbType.Int64);
        //    //DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", Activity.Equipment == null ? null : Activity.Equipment.EquipmentSerialNo, DbType.String);
        //    //DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", Activity.Equipment == null ? (Int64?)null : Activity.Equipment.EquipmentHeaderID, DbType.Int64);
        //    DbParameter RemarkP = provider.CreateParameter("Remark", string.IsNullOrEmpty(Activity.Remark) ? null : Activity.Remark, DbType.String);
        //    DbParameter StartLatitudeP = provider.CreateParameter("StartLatitude", Activity.ActivityStartLatitude == 0 ? (Decimal?)null : Activity.ActivityStartLatitude, DbType.Decimal);
        //    DbParameter StartLongitudeP = provider.CreateParameter("StartLongitude", Activity.ActivityStartLongitude == 0 ? (Decimal?)null : Activity.ActivityStartLongitude, DbType.Decimal);
        //    DbParameter EndLatitudeP = provider.CreateParameter("EndLatitude", Activity.ActivityEndLatitude == 0 ? (Decimal?)null : Activity.ActivityEndLatitude, DbType.Decimal);
        //    DbParameter EndLongitudeP = provider.CreateParameter("EndLongitude", Activity.ActivityEndLongitude == 0 ? (Decimal?)null : Activity.ActivityEndLongitude, DbType.Decimal);
        //    DbParameter AmountP = provider.CreateParameter("Amount", Activity.Amount == 0 ? (Decimal?)null : Activity.Amount, DbType.Decimal);
        //    DbParameter ReferenceTableIDP = provider.CreateParameter("RefereneceTableID", Activity.ActivityReference == null ? (Int32?)null : Activity.ActivityReference.ActivityReferenceTableID, DbType.Int32);
        //    DbParameter ReferenceNumberP = provider.CreateParameter("ReferenceNumber", string.IsNullOrEmpty(Activity.ReferenceNumber) ? null : Activity.ReferenceNumber, DbType.String);
        //    DbParameter ReferenceNumberIDP = provider.CreateParameter("ReferenceNumberID", Activity.ReferenceNumberID == null ? (Int64?)null : Activity.ReferenceNumberID, DbType.Int64);

        //    DbParameter OutP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));

        //    DbParameter[] Params = new DbParameter[16] { ActivityID, ActivitySalesEngineerUserIDP, ActivityTypeIDP, ActivityStartDateP, ActivityEndDateP, LocationP, RemarkP, StartLatitudeP, StartLongitudeP, EndLatitudeP, EndLongitudeP, AmountP, ReferenceTableIDP, ReferenceNumberP, ReferenceNumberIDP, OutP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            success = provider.Insert("InsertOrUpdateActivity", Params);
        //            scope.Complete();
        //        }
        //        ActivityOut = Convert.ToInt64(OutP.Value);
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BActivity", "InsertOrUpdateActivity", sqlEx);
        //        throw sqlEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BActivity", " InsertOrUpdateActivity", ex);
        //        throw ex;
        //    }
        //    return ActivityOut;
        //}

        public PApiResult StartActivity(long VisitID, string Location, string Remark, decimal Latitude, decimal Longitude, int ActivityTypeID)
        {
            string endPoint = "Activity/StartActivity?VisitID="+ VisitID + "&Location=" + Location + "&Remark=" + Remark + "&Latitude=" + Latitude + "&Longitude=" + Longitude + "&ActivityTypeID=" + ActivityTypeID;
            //return JsonConvert.DeserializeObject<List<PActivity>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public PApiResult EndActivity(long ActivityID, string Remark, decimal Latitude, decimal Longitude, int? ExpenseTypeID, decimal? Amount, int? EffortTypeID, decimal? EffortDuration)
        {
            string endPoint = "Activity/EndActivity?ActivityID=" + ActivityID + "&Remark=" + Remark + "&Latitude=" + Latitude + "&Longitude=" + Longitude
                + "&ExpenseTypeID=" + ExpenseTypeID + "&Amount=" + Amount + "&EffortTypeID=" + EffortTypeID + "&EffortDuration=" + EffortDuration;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public PApiResult StartActivityWithVisit(long VisitID, string Location, string Remark, decimal Latitude, decimal Longitude, int ActivityTypeID)
        {
            string endPoint = "Activity/StartActivityWithVisit?VisitID=" + VisitID + "&Location=" + Location + "&Remark=" + Remark + "&Latitude=" + Latitude + "&Longitude=" + Longitude + "&ActivityTypeID=" + ActivityTypeID;
            //return JsonConvert.DeserializeObject<List<PActivity>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public PApiResult EndActivityWithVisit(long ActivityID, string Remark, decimal Latitude, decimal Longitude, int? ExpenseTypeID, decimal? Amount, int? EffortTypeID, decimal? EffortDuration)
        {
            string endPoint = "Activity/EndActivityWithVisit?ActivityID=" + ActivityID + "&Remark=" + Remark + "&Latitude=" + Latitude + "&Longitude=" + Longitude
                + "&ExpenseTypeID=" + ExpenseTypeID + "&Amount=" + Amount + "&EffortTypeID=" + EffortTypeID + "&EffortDuration=" + EffortDuration;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public List<PActivity> GetPendingUserActivitiy(int? DealerID, int? SalesEnineerUserID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            List<PActivity> Activities = new List<PActivity>();
            try
            {
                string endPoint = "Activity/PendingUserActivitiy?DealerID=" + DealerID + "&SalesEnineerUserID=" + SalesEnineerUserID;
                return JsonConvert.DeserializeObject<List<PActivity>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
                 
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BActivity", "GetPendingUserActivitiy", ex);
                throw ex;
            } 
        }        
    }
}
