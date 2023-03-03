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
using System.Transactions;
using System.Web.UI.WebControls;

namespace Business
{
    public class BDMS_Address
    {
        private IDataAccess provider;
        public BDMS_Address()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public void GetCountry(DropDownList ddl, int? CountryID, string Country)
        {
            try
            {
                List<PDMS_Country> MML = GetCountry(CountryID, Country);
                ddl.DataValueField = "CountryID";
                ddl.DataTextField = "Country";
                ddl.DataSource = MML;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
        }
       
        public void GetRegion(DropDownList ddl, int? CountryID, int? RegionID, string Region)
        {
            try
            {
                List<PDMS_Region> MML = GetRegion(CountryID, RegionID, Region);
                ddl.DataValueField = "RegionID";
                ddl.DataTextField = "Region";
                ddl.DataSource = MML;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
        }
        

        public void GetStateDDL(DropDownList ddl, int? CountryID, int? RegionID, int? StateID, string State)
        {
            try
            {
                List<PDMS_State> MML = GetState(null, CountryID, RegionID, StateID, State);
                ddl.DataValueField = "StateID";
                ddl.DataTextField = "State";
                ddl.DataSource = MML;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
        }
        
        public Boolean InsertOrUpdateAddressCountry(int? CountryID, string Country, string CountryCode, int CurrencyID, string SalesOrganization, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
            DbParameter CountryP = provider.CreateParameter("Country", string.IsNullOrEmpty(Country) ? null : Country, DbType.String);
            DbParameter CountryCodeP = provider.CreateParameter("CountryCode", string.IsNullOrEmpty(CountryCode) ? null : CountryCode, DbType.String);
            DbParameter CurrencyIDP = provider.CreateParameter("CurrencyID", CurrencyID, DbType.Int32);
            DbParameter SalesOrganizationP = provider.CreateParameter("SalesOrganization", string.IsNullOrEmpty(SalesOrganization) ? null : SalesOrganization, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[7] { CountryIDP, CountryP, CountryCodeP, CurrencyIDP, SalesOrganizationP, IsActiveP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateAddressCountry", Params);
                    scope.Complete();
                }
                return true;
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Address", "ZDMS_InsertOrUpdateAddressCountry", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Address", " ZDMS_InsertOrUpdateAddressCountry", ex);
                throw ex;
            }

            TraceLogger.Log(DateTime.Now);
            return false;
        }
        public Boolean InsertOrUpdateAddressRegion(int? RegionID, string Region, int? CountryID, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            DbParameter RegionIDP = provider.CreateParameter("RegionID", RegionID, DbType.Int32);
            DbParameter RegionP = provider.CreateParameter("Region", string.IsNullOrEmpty(Region) ? null : Region, DbType.String);
            DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[5] { RegionIDP, RegionP, CountryIDP, IsActiveP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateAddressRegion", Params);
                    scope.Complete();
                }
                return true;
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Address", "ZDMS_InsertOrUpdateAddressRegion", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Address", " ZDMS_InsertOrUpdateAddressRegion", ex);
            }

            TraceLogger.Log(DateTime.Now);
            return false;
        }
        public Boolean InsertOrUpdateAddressState(int? StateID, string State, string StateCode, string StateSAP, int? CountryID, Boolean IsActive, int? RegionID, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
            DbParameter StateP = provider.CreateParameter("State", string.IsNullOrEmpty(State) ? null : State, DbType.String);
            DbParameter StateCodeP = provider.CreateParameter("StateCode", string.IsNullOrEmpty(StateCode) ? null : StateCode, DbType.String);
            DbParameter StateSAPP = provider.CreateParameter("StateSAP", string.IsNullOrEmpty(StateSAP) ? null : StateSAP, DbType.String);
            DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter RegionIDP = provider.CreateParameter("RegionID", RegionID, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[8] { StateIDP, StateP, StateCodeP, StateSAPP, CountryIDP, IsActiveP, RegionIDP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateAddressState", Params);
                    scope.Complete();
                }
                return true;
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Address", "InsertOrUpdateAddressState", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Address", " InsertOrUpdateAddressState", ex);
            }

            TraceLogger.Log(DateTime.Now);
            return false;
        }

        public void GetDistrict(DropDownList ddl,int? CountryID,int? RegionID, int? DistrictID, int? StateID, string District, int? DealerID)
        {
            try
            {
                List<PDMS_District> MML = GetDistrict(CountryID, RegionID, StateID, DistrictID, District, DealerID);
                ddl.DataValueField = "DistrictID";
                ddl.DataTextField = "District";
                ddl.DataSource = MML;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
        }
       
        public Boolean InsertOrUpdateAddressDistrict(int? DistrictID, int CountryID, int StateID, int SalesOfficeID, int DealerID, int? SalesEngineerUserID, string District, string DistrictSAP,Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            DbParameter DistrictIDP = provider.CreateParameter("DistrictID", DistrictID, DbType.Int32);
            DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
            DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
            DbParameter SalesOfficeIDP = provider.CreateParameter("SalesOfficeID", SalesOfficeID, DbType.Int32);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter SalesEngineerUserIDP = provider.CreateParameter("SalesEngineerUserID", SalesEngineerUserID, DbType.Int32);
            DbParameter DistrictP = provider.CreateParameter("District", District, DbType.String);
            DbParameter DistrictSAPP = provider.CreateParameter("DistrictSAP", string.IsNullOrEmpty(DistrictSAP) ? null : DistrictSAP, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[10] { DistrictIDP, CountryIDP, StateIDP, DealerIDP, SalesOfficeIDP, DistrictP, DistrictSAPP, IsActiveP, UserIDP, SalesEngineerUserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateAddressDistrict", Params);
                    scope.Complete();
                }
                return true;
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Address", "ZDMS_InsertOrUpdateAddressDistrict", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Address", " ZDMS_InsertOrUpdateAddressDistrict", ex);
            }
            TraceLogger.Log(DateTime.Now);
            return false;
        }
        public void GetTehsil(DropDownList ddl, int? CountryID, int? StateID, int? DistrictID, string Tehsil)
        {
            try
            {
                List<PDMS_Tehsil> MML = GetTehsil(CountryID, StateID,DistrictID, Tehsil);
                ddl.DataValueField = "TehsilID";
                ddl.DataTextField = "Tehsil";
                ddl.DataSource = MML;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
        }
        
        //public void GetTehsil(DropDownList ddl, int? TehsilID,int? CountryID,int? StateID, int? DistrictID, string Tehsil)
        //{
        //    List<PDMS_Tehsil> MML = new List<PDMS_Tehsil>();
        //    try
        //    {
        //        DbParameter TehsilIDP = provider.CreateParameter("TehsilID", TehsilID, DbType.Int32);
        //        DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
        //        DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
        //        DbParameter DistrictIDP = provider.CreateParameter("DistrictID", DistrictID, DbType.Int32);
        //        DbParameter TehsilP = provider.CreateParameter("Tehsil", string.IsNullOrEmpty(Tehsil) ? null : Tehsil, DbType.String);

        //        DbParameter[] Params = new DbParameter[5] { TehsilIDP, CountryIDP, StateIDP, DistrictIDP, TehsilP };
        //        using (DataSet DataSet = provider.Select("ZDMS_GetTehsil", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    MML.Add(new PDMS_Tehsil()
        //                    {
        //                        TehsilID = Convert.ToInt32(dr["TehsilID"]),
        //                        Tehsil = Convert.ToString(dr["Tehsil"])
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    ddl.DataValueField = "TehsilID";
        //    ddl.DataTextField = "Tehsil";
        //    ddl.DataSource = MML;
        //    ddl.DataBind();
        //    ddl.Items.Insert(0, new ListItem("Select", "0"));
        //}
        public void GetVillage(DropDownList ddl, int? VillageID, int? TehsilID, string Village)
        {
            List<PDMS_Village> MML = new List<PDMS_Village>();
            try
            {
                DbParameter VillageIDP = provider.CreateParameter("VillageID", VillageID, DbType.Int32);
                DbParameter TehsilIDP = provider.CreateParameter("TehsilID", TehsilID, DbType.Int32);
                DbParameter VillageP = provider.CreateParameter("Village", string.IsNullOrEmpty(Village) ? null : Village, DbType.String);

                DbParameter[] Params = new DbParameter[3] { VillageIDP, TehsilIDP, VillageP };
                using (DataSet DataSet = provider.Select("ZDMS_GetVillage", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PDMS_Village()
                            {
                                VillageID = Convert.ToInt32(dr["VillageID"]),
                                Village = Convert.ToString(dr["Village"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataValueField = "DistrictID";
            ddl.DataTextField = "District";
            ddl.DataSource = MML;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public Boolean InsertOrUpdateAddressTehsil(int? TehsilID,int? DistrictID, string Tehsil, Boolean IsActive, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            DbParameter TehsilIDP = provider.CreateParameter("TehsilID", TehsilID, DbType.Int32);
            DbParameter DistrictIDP = provider.CreateParameter("DistrictID", DistrictID, DbType.Int32);
            DbParameter TehsilP = provider.CreateParameter("Tehsil", Tehsil, DbType.String);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[5] { TehsilIDP,DistrictIDP, TehsilP, IsActiveP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateAddressTehsil", Params);
                    scope.Complete();
                }
                return true;
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Address", "ZDMS_InsertOrUpdateAddressTehsil", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Address", " ZDMS_InsertOrUpdateAddressTehsil", ex);
            }
            TraceLogger.Log(DateTime.Now);
            return false;
        }

        public List<PCurrency> GetCurrency(int? CurrencyID, string Currency)
        {
            List<PCurrency> MML = new List<PCurrency>();
            try
            {
                DbParameter CurrencyIDP = provider.CreateParameter("CurrencyID", CurrencyID, DbType.Int32);
                DbParameter CurrencyP = provider.CreateParameter("Currency", string.IsNullOrEmpty(Currency) ? null : Currency, DbType.String);
                DbParameter[] Params = new DbParameter[2] { CurrencyIDP, CurrencyP };
                using (DataSet DataSet = provider.Select("ZDMS_GetCurrency", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PCurrency()
                            {
                                CurrencyID = Convert.ToInt32(dr["CurrencyID"]),
                                Currency = Convert.ToString(dr["Currency"])
                                
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return MML;
        }

        public void GetSalesOrganization(DropDownList ddl, int? CountryID, string Country)
        {
            try
            {
                List<PDMS_Country> MML = GetCountry(CountryID, Country);
                ddl.DataValueField = "SalesOrganization";
                ddl.DataTextField = "SalesOrganization";
                ddl.DataSource = MML.Distinct();
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select Sales Organization", "0"));
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
        }
        public List<PSalesOffice> GetSalesOffice(int? SalesOfficeID, string SalesOffice)
        {
            List<PSalesOffice> MML = new List<PSalesOffice>();
            try
            {
                DbParameter SalesOfficeIDP = provider.CreateParameter("SalesOfficeID", SalesOfficeID, DbType.Int32);
                DbParameter SalesOfficeP = provider.CreateParameter("SalesOffice", string.IsNullOrEmpty(SalesOffice) ? null : SalesOffice, DbType.String);

                DbParameter[] Params = new DbParameter[2] { SalesOfficeIDP, SalesOfficeP };
                using (DataSet DataSet = provider.Select("ZDMS_GetSalesOffice", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PSalesOffice()
                            {
                                SalesOfficeID = Convert.ToInt32(dr["SalesOfficeID"]),
                                SalesOffice = Convert.ToString(dr["SalesOffice"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return MML;
        }

       
        public List<PDMS_Country> GetCountry(int? CountryID, string Country)
        {
            string endPoint = "Location/Country?CountryID=" + CountryID + "&Country=" + Country;
            return JsonConvert.DeserializeObject<List<PDMS_Country>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
             
        }
        public List<PDMS_Region> GetRegion(int? CountryID, int? RegionID, string Region)
        {
            string endPoint = "Location/Region?CountryID=" + CountryID + "&RegionID=" + RegionID + "&Region=" + Region;
            return JsonConvert.DeserializeObject<List<PDMS_Region>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PDMS_State> GetState(int? DealerID, int? CountryID, int? RegionID, int? StateID, string State)
        {
            string endPoint = "Location/State?DealerID=" + DealerID + "&CountryID=" + CountryID + "&RegionID=" + RegionID + "&StateID=" + StateID + "&State=" + State;
            return JsonConvert.DeserializeObject<List<PDMS_State>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
             
        }
        public List<PDMS_District> GetDistrict(int? CountryID, int? RegionID, int? StateID, int? DistrictID, string District, int? DealerID, string All = "false")
        {
            string endPoint = "Location/District?CountryID=" + CountryID + "&RegionID=" + RegionID + "&StateID=" + StateID
                + "&DistrictID=" + DistrictID + "&District=" + District + "&DealerID=" + DealerID + "&All=" + All; ;
            return JsonConvert.DeserializeObject<List<PDMS_District>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PDMS_District> GetDistrictBySalesEngineerUserID(int? DealerID)
        {
            string endPoint = "Location/DistrictBySalesEngineerUserID?DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<List<PDMS_District>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PDMS_Tehsil> GetTehsil(int? CountryID, int? StateID, int? DistrictID, string Tehsil)
        {
            List<PDMS_Tehsil> MML = new List<PDMS_Tehsil>();
            try
            {
                DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter DistrictIDP = provider.CreateParameter("DistrictID", DistrictID, DbType.Int32);
                DbParameter TehsilP = provider.CreateParameter("Tehsil", string.IsNullOrEmpty(Tehsil) ? null : Tehsil, DbType.String);

                DbParameter[] Params = new DbParameter[4] { CountryIDP, StateIDP, DistrictIDP, TehsilP };
                using (DataSet DataSet = provider.Select("ZDMS_GetTehsil", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PDMS_Tehsil()
                            {
                                TehsilID = Convert.ToInt32(dr["TehsilID"]),
                                Tehsil = Convert.ToString(dr["Tehsil"]),
                                District = new PDMS_District()
                                {
                                    DistrictID = Convert.ToInt32(dr["DistrictID"]),
                                    District = Convert.ToString(dr["District"]),
                                },
                                State = new PDMS_State()
                                {
                                    StateID = Convert.ToInt32(dr["StateID"]),
                                    State = Convert.ToString(dr["State"]),
                                },
                                Country = new PDMS_Country()
                                {
                                    CountryID = Convert.ToInt32(dr["CountryID"]),
                                    Country = Convert.ToString(dr["Country"]),
                                }
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return MML;
        }
    }
}
