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

namespace Business
{
    public class BActivity
    {
        private IDataAccess provider;
        public BActivity()
        {
            provider = new ProviderFactory().GetProvider();
        }

        public List<PActivityType> GetActivityType(int? ActivityTypeID, string ActivityTypeCode, string ActivityTypeName, Boolean IsActive)
        {
            List<PActivityType> ActivityTypes = new List<PActivityType>();
            PActivityType ActivityType = null;
            DbParameter ActivityTypeIDP = provider.CreateParameter("ActivityTypeID", ActivityTypeID, DbType.Int32);
            DbParameter ActivityTypeCodeP = provider.CreateParameter("ActivityTypeCode", string.IsNullOrEmpty(ActivityTypeCode) ? null : ActivityTypeCode, DbType.String);
            DbParameter ActivityTypeNameP = provider.CreateParameter("ActivityTypeName", string.IsNullOrEmpty(ActivityTypeName) ? null : ActivityTypeName, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Int32); ;
            
            DbParameter[] Params = new DbParameter[4] { ActivityTypeIDP, ActivityTypeCodeP, ActivityTypeNameP, IsActiveP };
            try
            {
                using (DataSet ds = provider.Select("GetActivityType", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            ActivityType = new PActivityType();
                            ActivityType.ActivityTypeID = Convert.ToInt32(dr["ActivityTypeID"]);
                            ActivityType.ActivityTypeCode = Convert.ToString(dr["ActivityTypeCode"]);
                            ActivityType.ActivityTypeName = Convert.ToString(dr["ActivityTypeName"]);
                            ActivityType.IsActive = Convert.ToBoolean(Convert.ToString(dr["IsActive"]));
                            ActivityType.CreatedBy = new PUser() { ContactName = Convert.ToString(dr["CreatedBy"]) };
                            ActivityType.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                            ActivityType.ModifiedBy = new PUser() { ContactName = Convert.ToString(dr["ModifiedBy"]) };
                            ActivityType.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
                            ActivityTypes.Add(ActivityType);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return ActivityTypes;
        }

        public List<PActivityReferenceType> GetActivityReferenceType(int? ActivityReferenceTypeID, string ActivityReferenceType, Boolean IsActive)
        {
            List<PActivityReferenceType> ActivityReferenceTypes = new List<PActivityReferenceType>();
            PActivityReferenceType ActivityReferenceType1 = null;
            DbParameter ActivityReferenceTypeIDP = provider.CreateParameter("ActivityReferenceTypeID", ActivityReferenceTypeID, DbType.Int32);
            DbParameter ActivityReferenceTypeP = provider.CreateParameter("ActivityTypeCode", string.IsNullOrEmpty(ActivityReferenceType) ? null : ActivityReferenceType, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Int32); ;

            DbParameter[] Params = new DbParameter[3] { ActivityReferenceTypeIDP, ActivityReferenceTypeP, IsActiveP };
            try
            {
                using (DataSet ds = provider.Select("GetActivityReferenceType", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            ActivityReferenceType1 = new PActivityReferenceType();
                            ActivityReferenceType1.ActivityReferenceTypeID = Convert.ToInt32(dr["ActivityReferenceTypeID"]);
                            ActivityReferenceType1.ActivityReferenceType = Convert.ToString(dr["ActivityReferenceType"]);
                            ActivityReferenceType1.IsActive = Convert.ToBoolean(Convert.ToString(dr["IsActive"]));
                            ActivityReferenceType1.CreatedBy = new PUser() { ContactName = Convert.ToString(dr["CreatedBy"]) };
                            ActivityReferenceType1.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                            ActivityReferenceType1.ModifiedBy = new PUser() { ContactName = Convert.ToString(dr["ModifiedBy"]) };
                            ActivityReferenceType1.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
                            ActivityReferenceTypes.Add(ActivityReferenceType1);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return ActivityReferenceTypes;
        }
       
        public List<PActivity> GetActivity(int? ActivityTypeID, DateTime? ActivityDateFrom, DateTime? ActivityDateTo, Int64? CustomerCode, string CustomerName, string Equipment, int? ReferenceID)
        {
            TraceLogger.Log(DateTime.Now);
            List<PActivity> Activities = new List<PActivity>();
            try
            {
                DbParameter ActivityTypeIDP = provider.CreateParameter("ActivityTypeID", ActivityTypeID, DbType.Int32);
                DbParameter ActivityDateFromP = provider.CreateParameter("ActivityDateFrom", ActivityDateFrom, DbType.DateTime);
                DbParameter ActivityDateToP = provider.CreateParameter("ActivityDateTo", ActivityDateTo, DbType.DateTime);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.Int64);
                DbParameter CustomerNameP = provider.CreateParameter("CustomerName", String.IsNullOrEmpty(CustomerName) ? null : CustomerName, DbType.String);
                DbParameter EquipmentP = provider.CreateParameter("Equipment", Equipment, DbType.String);
                DbParameter ReferenceIDP = provider.CreateParameter("ReferenceID", ReferenceID, DbType.Int32);

                DbParameter[] Params = new DbParameter[7] { ActivityTypeIDP, ActivityDateFromP, ActivityDateToP, CustomerCodeP, CustomerNameP, EquipmentP, ReferenceIDP };


                PActivity Lead = new PActivity();
                using (DataSet DataSet = provider.Select("Getactivity", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            Lead = new PActivity();
                            //Lead.LeadID = Convert.ToInt64(dr["LeadID"]);
                            //Lead.LeadNumber = Convert.ToString(dr["LeadNumber"]);
                            //Lead.LeadDate = Convert.ToDateTime(dr["LeadDate"]);
                            //Lead.ProductType = new PProductType() { ProductType = Convert.ToString(dr["ProductType"]), ProductTypeID = Convert.ToInt32(dr["ProductTypeID"]) };
                            //Lead.ProgressStatus = new PLeadProgressStatus() { ProgressStatusID = Convert.ToInt32(dr["ProgressStatusID"]), ProgressStatus = Convert.ToString(dr["LeadProgressStatus"]) };
                            //Lead.Status = new PLeadStatus() { StatusID = Convert.ToInt32(dr["StatusID"]), Status = Convert.ToString(dr["Status"]) };

                            //Lead.Category = new PLeadCategory() { CategoryID = Convert.ToInt32(dr["CategoryID"]), Category = Convert.ToString(dr["Category"]) };
                            //Lead.Qualification = new PLeadQualification() { QualificationID = Convert.ToInt32(dr["QualificationID"]), Qualification = Convert.ToString(dr["Qualification"]) };
                            //Lead.Source = new PLeadSource() { SourceID = Convert.ToInt32(dr["SourceID"]), Source = Convert.ToString(dr["Source"]) };
                            //Lead.Type = new PLeadType() { TypeID = Convert.ToInt32(dr["TypeID"]), Type = Convert.ToString(dr["Type"]) };
                            //Lead.Customer = new PDMS_Customer()
                            //{
                            //    CustomerID = Convert.ToInt64(dr["CustomerID"]),
                            //    CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            //    CustomerName = Convert.ToString(dr["CustomerName"]),

                            //    ContactPerson = Convert.ToString(dr["ContactPerson"]),
                            //    Email = Convert.ToString(dr["Email"]),
                            //    Mobile = Convert.ToString(dr["Mobile"]),

                            //    Address1 = Convert.ToString(dr["Address1"]),
                            //    Address2 = Convert.ToString(dr["Address2"]),
                            //    Country = new PDMS_Country() { Country = Convert.ToString(dr["Country"]) },
                            //    State = new PDMS_State() { State = Convert.ToString(dr["State"]) },
                            //    District = new PDMS_District() { District = Convert.ToString(dr["District"]) },
                            //};
                            //Lead.Remarks = Convert.ToString(dr["Remarks"]);

                            //Lead.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };

                             
                            Activities.Add(Lead);
                        }
                    }
                }
                return Activities;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BLead", "GetLead", ex);
                throw ex;
            }
            return Activities;
        }
    }
}
