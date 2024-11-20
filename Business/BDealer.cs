using DataAccess;
using Newtonsoft.Json;
using Properties; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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
                            Dealer.CodeWithDisplayName = Convert.ToString(dr["DealerCode"]) + " " + Convert.ToString(dr["DisplayName"]);
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
                        foreach (DataRow Row in EmployeeDataSet.Tables[0].Rows)
                        {
                            L1SupportMapping = new PL1SupportMapping();
                            L1SupportMapping.L1SupportUserMappingID = Row["L1SupportUserMappingID"] == DBNull.Value ? (int?)null : Convert.ToInt32(Row["L1SupportUserMappingID"]);
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

        public List<PDealer> GetDealerByUserID(long UserID)
        {
            List<PDealer> Dealers = new List<PDealer>();
            PDealer Dealer = null;
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);



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
        public PDealer GetDealerByID(int? DealerID, string DealerCode)
        {

            PDealer Dealer = null;

            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.Int32);
            DbParameter[] Params = new DbParameter[2] { DealerIDP, DealerCodeP };
            try
            {
                using (DataSet ds = provider.Select("GetDealerByID", Params))
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
                            Dealer.Phone = Convert.ToString(dr["Phone"]);
                            Dealer.UserTypeID = Convert.ToInt32(dr["UserTypeID"]);
                            Dealer.IsActive = Convert.ToBoolean(Convert.ToString(dr["IsActive"]));
                            Dealer.HeadOfficeID = Convert.ToString(dr["HeadOfficeID"]).Trim();
                            Dealer.StateCode = Convert.ToString(dr["StateCode"]).Trim();
                            Dealer.Country = new PDMS_Country() { CountryID = Convert.ToInt32(dr["CountryID"]) };
                            Dealer.State = new PDMS_State() { StateID = Convert.ToInt32(dr["StateID"]) };

                            Dealer.EInvAPI = dr["EInvAPI"] == DBNull.Value ? false : Convert.ToBoolean(dr["EInvAPI"]);
                            Dealer.GspCode = Convert.ToString(dr["GspCode"]);
                            Dealer.Gstin = Convert.ToString(dr["Gstin"]);
                            Dealer.ApiUserName = Convert.ToString(dr["ApiUserName"]);
                            Dealer.ApiPassword = Convert.ToString(dr["ApiPassword"]);

                            Dealer.EInvUserAPI = new PEInvUserAPI()
                            {
                                Handle = Convert.ToString(dr["EInvHandle"]),
                                HandleType = Convert.ToString(dr["EInvHandleType"]),
                                Password = Convert.ToString(dr["EInvMailPassword"])
                            };
                            Dealer.DealerType = new PDealerType() { DealerTypeID = Convert.ToInt32(dr["DealerTypeID"]) };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Dealer;
        }
        public int InsertOrUpdateDealerAddress(string Code)
        {
            //PDMS_Customer Dealer = new SCustomer().getCustomerAddress(Code);
            PDMS_Customer Dealer = new BDMS_Customer().getDealerAddressFromSAP(Code);
            if (String.IsNullOrEmpty(Dealer.CustomerName))
            {
                return 0;
            }
            int success = 0;
            DbParameter DealerCode = provider.CreateParameter("DealerCode", Dealer.CustomerCode, DbType.String);
            DbParameter Address1 = provider.CreateParameter("Address1", Dealer.Address12, DbType.String);
            DbParameter Address2 = provider.CreateParameter("Address2", Dealer.Address3, DbType.String);
            DbParameter City = provider.CreateParameter("City", Dealer.City, DbType.String);
            DbParameter State = provider.CreateParameter("State", Dealer.State.State, DbType.String);
            DbParameter StateCode = provider.CreateParameter("StateCode", Dealer.State.StateCode, DbType.String);
            DbParameter Pincode = provider.CreateParameter("Pincode", Dealer.Pincode, DbType.String);
            DbParameter GSTIN = provider.CreateParameter("GSTIN", Dealer.GSTIN, DbType.String);
            DbParameter PAN = provider.CreateParameter("PAN", Dealer.PAN, DbType.String);
            DbParameter Mobile = provider.CreateParameter("Mobile", Dealer.Mobile, DbType.String);
            DbParameter Email = provider.CreateParameter("Email", Dealer.Email, DbType.String);
            DbParameter ContactPerson = provider.CreateParameter("ContactPerson", Dealer.ContactPerson, DbType.String);

            DbParameter[] Params = new DbParameter[12] { DealerCode, Address1, Address2, City, State, StateCode, Pincode, GSTIN, PAN, Mobile, Email, ContactPerson };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("InsertOrUpdateDealerAddress", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                string m = sqlEx.Message;
            }
            catch (Exception ex)
            { }
            return success;
        }


        //public PDealerAddress GetDealerAddress(int? DealerID, string DealerCode)
        //{

        //    PDealerAddress Dealer = null;

        //    DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
        //    DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.Int32);
        //    DbParameter[] Params = new DbParameter[2] { DealerIDP, DealerCodeP };
        //    try
        //    {
        //        using (DataSet ds = provider.Select("GetDealerAddress", Params))
        //        {
        //            if (ds != null)
        //            {
        //                foreach (DataRow dr in ds.Tables[0].Rows)
        //                {
        //                    Dealer = new PDealer();
        //                    Dealer.DID = Convert.ToInt32(dr["DID"]);
        //                    Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
        //                    Dealer.UserName = Convert.ToString(dr["UserName"]);
        //                    Dealer.ContactName = Convert.ToString(dr["ContactName"]);
        //                    Dealer.MailID1 = Convert.ToString(dr["MailID"]);
        //                    Dealer.Phone = Convert.ToString(dr["Phone"]);
        //                    Dealer.UserTypeID = Convert.ToInt32(dr["UserTypeID"]);
        //                    Dealer.IsActive = Convert.ToBoolean(Convert.ToString(dr["IsActive"]));
        //                    Dealer.HeadOfficeID = Convert.ToString(dr["HeadOfficeID"]).Trim();
        //                    Dealer.StateCode = Convert.ToString(dr["StateCode"]).Trim();
        //                    Dealer.Country = new PDMS_Country() { CountryID = Convert.ToInt32(dr["CountryID"]) };
        //                    Dealer.State = new PDMS_State() { StateID = Convert.ToInt32(dr["StateID"]) };

        //                    Dealer.EInvAPI = Convert.ToBoolean(dr["EInvAPI"]);
        //                    Dealer.GspCode = Convert.ToString(dr["GspCode"]);
        //                    Dealer.Gstin = Convert.ToString(dr["Gstin"]);
        //                    Dealer.ApiUserName = Convert.ToString(dr["ApiUserName"]);
        //                    Dealer.ApiPassword = Convert.ToString(dr["ApiPassword"]);

        //                    Dealer.EInvUserAPI = new PEInvUserAPI()
        //                    {
        //                        Handle = Convert.ToString(dr["EInvHandle"]),
        //                        HandleType = Convert.ToString(dr["EInvHandleType"]),
        //                        Password = Convert.ToString(dr["EInvMailPassword"])
        //                    };
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return Dealer;
        //}

        public List<PDealerStateMappingID> GetDealerStateMapping(int? DealerID, int? CountryID, int? StateID)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDealerStateMappingID> DealerStateMappings = new List<PDealerStateMappingID>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter[] Params = new DbParameter[3] { DealerIDP, CountryIDP, StateIDP };

                PDealerStateMappingID DealerStateMapping = new PDealerStateMappingID();
                using (DataSet DataSet = provider.Select("GetDealerStateMapping", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            //DealerStateMappings.Add(new PDealerStateMappingID()
                            //{

                            //    State = new PDMS_State() { StateID = Convert.ToInt32(dr["StateID"]) , State = Convert.ToString(dr["State"]) },
                            //    Dealer = new PDealer() { DID = Convert.ToInt32(dr["DID"]) , DealerCode = Convert.ToString(dr["DealerCode"]) },

                            //});
                            DealerStateMapping = new PDealerStateMappingID();
                            DealerStateMapping.DealerStateMappingID = Convert.ToInt32(dr["DealerStateMappingID"]);
                            DealerStateMapping.Dealer = new PDealer() { DealerID = Convert.ToInt32(dr["DID"]), DealerCode = Convert.ToString(dr["DealerCode"]) };
                            DealerStateMapping.State = new PDMS_State() { StateID = Convert.ToInt32(dr["StateID"]), State = Convert.ToString(dr["State"]) };
                            DealerStateMappings.Add(DealerStateMapping);
                        }
                    }
                }
                return DealerStateMappings;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("GetDealerStateMapping", "GetDealerStateMapping", ex);
                throw ex;
            }
            return DealerStateMappings;
        }

        public int InsertOrUpdateDealerStateMapping(int? DealerStateMappingID, int? DealerID, int? CountryID, int? StateID, int UserID, Boolean IsActive)
        {
            TraceLogger.Log(DateTime.Now);
            DbParameter DealerStateMappingIDP = provider.CreateParameter("DealerStateMappingID", DealerStateMappingID, DbType.Int32);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter[] Params = new DbParameter[5] { DealerStateMappingIDP, DealerIDP, StateIDP, UserIDP, IsActiveP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("InsertOrUpdateDealerStateMapping", Params);
                    scope.Complete();
                }
                return 1;
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDealer", "InsertOrUpdateDealerStateMapping", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDealer", "InsertOrUpdateDealerStateMapping", ex);
            }

            TraceLogger.Log(DateTime.Now);
            return 0;
        }

        public List<PDMS_Dealer> GetDealerAddress(int? DealerID)
        {
            string endPoint = "Dealer/GetDealerAddress?DealerID=" + DealerID ;
            return JsonConvert.DeserializeObject<List<PDMS_Dealer>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
}
