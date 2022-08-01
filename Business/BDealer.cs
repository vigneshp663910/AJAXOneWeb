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
using System.Web.UI.WebControls;

namespace Business
{
    public class BDealer
    {
        private IDataAccess provider;
        public BDealer()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PDealer> GetDealerList(int? DID, string UserName, string ContactName)
        {
            List<PDealer> Dealers = new List<PDealer>();
            PDealer Dealer = null;
            DbParameter DIDP;
            DbParameter UserNameP;
            DbParameter ContactNameP;

            if (DID != null)
                DIDP = provider.CreateParameter("DID", DID, DbType.Int32);
            else
                DIDP = provider.CreateParameter("DID", DBNull.Value, DbType.Int32);

            if (!string.IsNullOrEmpty(UserName))
                UserNameP = provider.CreateParameter("UserName", UserName, DbType.String);
            else
                UserNameP = provider.CreateParameter("UserName", DBNull.Value, DbType.String);

            if (!string.IsNullOrEmpty(ContactName))
                ContactNameP = provider.CreateParameter("ContactName", ContactName, DbType.String);
            else
                ContactNameP = provider.CreateParameter("ContactName", DBNull.Value, DbType.String);


            DbParameter[] Params = new DbParameter[3] { DIDP, UserNameP, ContactNameP };
            try
            {
                using (DataSet ds = provider.Select("GetDealerList", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Dealer = new PDealer();
                            Dealer.DID = Convert.ToInt32(dr["DID"]);
                            Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                            Dealer.UserName = Convert.ToString(dr["UserName"]);
                            Dealer.ContactName = Convert.ToString(dr["ContactName"]);
                            Dealer.MailID1 = Convert.ToString(dr["MailID"]);
                            Dealer.UserTypeID = Convert.ToInt32(dr["UserTypeID"]);
                            Dealer.IsActive = Convert.ToBoolean(Convert.ToString(dr["IsActive"]));
                            Dealer.HeadOfficeID = Convert.ToString(dr["HeadOfficeID"]).Trim();
                            Dealer.StateCode = Convert.ToString(dr["StateCode"]).Trim();
                            Dealer.Country = new PDMS_Country() { CountryID = Convert.ToInt32(dr["CountryID"]) };
                            Dealer.State = new PDMS_State() { StateID = Convert.ToInt32(dr["StateID"]) };
                            Dealers.Add(Dealer);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Dealers;
        }

        public int insertOrUpdateL1SupportMapping(int DealerID, int CategoryID, int UserId, Boolean IsActive)
        {
            int success = 0;
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter CategoryIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
            DbParameter UserIdP = provider.CreateParameter("UserId", UserId, DbType.Int32);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Int32);

            DbParameter[] Params = new DbParameter[4] { DealerIDP, CategoryIDP, UserIdP, IsActiveP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("insertOrUpdateL1SupportMapping", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return success;
        }

        public List<PL1SupportMapping> GetL1SupportMapping(int? DealerID, int? CategoryID)
        {
            List<PL1SupportMapping> L1SupportMappingS = new List<PL1SupportMapping>();
            PL1SupportMapping L1SupportMapping = null;
            DbParameter DealerIDP;
            DbParameter CategoryIDP;

            if (DealerID != null)
                DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            else
                DealerIDP = provider.CreateParameter("DealerID", DBNull.Value, DbType.Int32);


            if (CategoryID != null)
                CategoryIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
            else
                CategoryIDP = provider.CreateParameter("CategoryID", DBNull.Value, DbType.Int32);

            DbParameter[] Params = new DbParameter[2] { DealerIDP, CategoryIDP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("GetL1SupportMapping", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow  Row in EmployeeDataSet.Tables[0].Rows)
                        {
                            L1SupportMapping = new PL1SupportMapping();
                            L1SupportMapping.L1SupportUserMappingID = Row["L1SupportUserMappingID"] == DBNull.Value? (int?) null: Convert.ToInt32(Row["L1SupportUserMappingID"]);
                            L1SupportMapping.DealerID = Row["DealerID"] == DBNull.Value ? (int?)null : Convert.ToInt32(Row["DealerID"]);
                            L1SupportMapping.CategoryID = Convert.ToInt32(Row["CategoryID"]);
                            L1SupportMapping.UserId = Row["UserId"] == DBNull.Value ? (int?)null : Convert.ToInt32(Row["UserId"]);
                            L1SupportMapping.IsActive = Row["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(Row["IsActive"]);
                            L1SupportMapping.Category = Convert.ToString(Convert.ToString(Row["Category"]));
                            L1SupportMappingS.Add(L1SupportMapping);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return L1SupportMappingS;
        }

        public string DealerMaster()
        {
            if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer)
                return "~/Dealer.master";
            else if (PSession.User.SystemCategoryID == (short)SystemCategory.SupportTR)
                return "~/SupportTR.master";
            else if (PSession.User.SystemCategoryID == (short)SystemCategory.Support)
                        return "~/Support.master";
                return "";
        }
        public List<PDealer> GetDealerByUserID(long UserID)
        {
            List<PDealer> Dealers = new List<PDealer>();
            PDealer Dealer = null;
            DbParameter   UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);



            DbParameter[] Params = new DbParameter[1] { UserIDP };
            try
            {
                using (DataSet DataSet = provider.Select("GetDealerByUserID", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow EmployeeRow in DataSet.Tables[0].Rows)
                        {
                            Dealer = new PDealer();
                            Dealer.DID = Convert.ToInt32(EmployeeRow["DID"]);
                            Dealer.UserName = Convert.ToString(EmployeeRow["UserName"]);
                            Dealer.ContactName = Convert.ToString(EmployeeRow["ContactName"]);
                            Dealer.CodeWithName = Dealer.UserName + "-" + Dealer.ContactName;
                            Dealer.MailID1 = Convert.ToString(EmployeeRow["MailID"]);
                            Dealer.UserTypeID = Convert.ToInt32(EmployeeRow["UserTypeID"]);
                            Dealer.IsActive = Convert.ToBoolean(Convert.ToString(EmployeeRow["IsActive"]));
                            Dealer.HeadOfficeID = Convert.ToString(EmployeeRow["HeadOfficeID"]).Trim();
                            Dealer.StateCode = Convert.ToString(EmployeeRow["StateCode"]).Trim();
                            Dealer.Country = new PDMS_Country() { CountryID = Convert.ToInt32(EmployeeRow["CountryID"]) };
                            Dealer.State = new PDMS_State() { StateID = Convert.ToInt32(EmployeeRow["StateID"]) };
                            Dealers.Add(Dealer);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Dealers;
        }

      
    }
}
