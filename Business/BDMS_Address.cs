using DataAccess;
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
        public List<PDMS_Country> GetCountry(int? CountryID, string Country)
        {
            List<PDMS_Country> MML = new List<PDMS_Country>();
            try
            {
                DbParameter RegionIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
                DbParameter RegionP = provider.CreateParameter("Country", string.IsNullOrEmpty(Country) ? null : Country, DbType.String);
                DbParameter[] Params = new DbParameter[2] { RegionIDP, RegionP };
                using (DataSet DataSet = provider.Select("ZDMS_GetCountry", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PDMS_Country()
                            {
                                CountryID = Convert.ToString(dr["CountryID"]),
                                Country = Convert.ToString(dr["Country"])
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
        public void GetRegion(DropDownList ddl, int? RegionID, string Region)
        {
            try
            {
                List<PDMS_Region> MML = GetRegion(RegionID, Region);
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
        public List<PDMS_Region> GetRegion(int? RegionID, string Region)
        {
            List<PDMS_Region> MML = new List<PDMS_Region>();
            try
            {
                DbParameter RegionIDP = provider.CreateParameter("RegionID", RegionID, DbType.Int32);
                DbParameter RegionP = provider.CreateParameter("Region", string.IsNullOrEmpty(Region) ? null : Region, DbType.String);
                DbParameter[] Params = new DbParameter[2] { RegionIDP, RegionP };
                using (DataSet DataSet = provider.Select("ZDMS_GetRegion", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PDMS_Region()
                            {
                                RegionID = Convert.ToInt32(dr["RegionID"]),
                                Region = Convert.ToString(dr["Region"])                              
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

        public void GetState(DropDownList ddl, int? StateID, string State)
        {
            try
            {
                List<PDMS_State> MML = GetState(StateID, State);
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
        public List<PDMS_State> GetState( int? StateID, string State)
        {
            List<PDMS_State> MML = new List<PDMS_State>();
            try
            {

                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter StateP = provider.CreateParameter("State", string.IsNullOrEmpty(State) ? null : State, DbType.String);

                DbParameter[] Params = new DbParameter[2] { StateIDP, StateP };
                using (DataSet DataSet = provider.Select("ZDMS_GetState", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PDMS_State()
                            {
                                StateID = Convert.ToInt32(dr["StateID"]),
                                State = Convert.ToString(dr["State"]),
                                StateCode = Convert.ToString(dr["StateCode"]),
                                StateSAP = Convert.ToString(dr["StateSAP"]),
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
        public Boolean InsertOrUpdateAddressState(int? StateID, string State, string StateCode, string StateSAP, int UserID)
        {
            TraceLogger.Log(DateTime.Now); 
            DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
            DbParameter StateP = provider.CreateParameter("State", string.IsNullOrEmpty(State) ? null : State, DbType.String);
            DbParameter StateCodeP = provider.CreateParameter("StateCode", string.IsNullOrEmpty(StateCode) ? null : StateCode, DbType.String);
            DbParameter StateSAPP = provider.CreateParameter("StateSAP", string.IsNullOrEmpty(StateSAP) ? null : StateSAP, DbType.String); 
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32); 
            DbParameter[] Params = new DbParameter[5] { StateIDP, StateP, StateCodeP, StateSAPP, UserIDP };
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

        public void GetDistrict(DropDownList ddl, int? DistrictID, int? StateID, string District)
        {
            try
            {
                List<PDMS_District> MML = GetDistrict(DistrictID, StateID, District);
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
        public List<PDMS_District> GetDistrict( int? DistrictID, int? StateID, string District)
        {
            List<PDMS_District> MML = new List<PDMS_District>();
            try
            {
                DbParameter DistrictIDP = provider.CreateParameter("DistrictID", DistrictID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter DistrictP = provider.CreateParameter("District", string.IsNullOrEmpty(District) ? null : District, DbType.String);

                DbParameter[] Params = new DbParameter[3] { DistrictIDP, StateIDP, DistrictP };
                using (DataSet DataSet = provider.Select("ZDMS_GetDistrict", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PDMS_District()
                            {
                                DistrictID = Convert.ToInt32(dr["DistrictID"]),
                                District = Convert.ToString(dr["District"]),
                                DistrictSAP = Convert.ToString(dr["DistrictSAP"]),
                                State = new PDMS_State()
                                {
                                    StateID = Convert.ToInt32(dr["StateID"]),
                                    State = Convert.ToString(dr["State"])
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
        public Boolean InsertOrUpdateAddressDistrict(int? DistrictID, int StateID, string District, string DistrictSAP, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            DbParameter DistrictIDP = provider.CreateParameter("DistrictID", DistrictID, DbType.Int32);
            DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
            DbParameter DistrictP = provider.CreateParameter("District", District, DbType.String);
            DbParameter DistrictSAPP = provider.CreateParameter("DistrictSAP", string.IsNullOrEmpty(DistrictSAP) ? null : DistrictSAP, DbType.String); 
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[5] { DistrictIDP, StateIDP, DistrictP, DistrictSAPP, UserIDP };
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
                new FileLogger().LogMessage("BDMS_Address", "InsertOrUpdateAddressDistrict", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Address", " InsertOrUpdateAddressDistrict", ex);
            }
            TraceLogger.Log(DateTime.Now);
            return false;
        }
         
        public void GetTehsil(DropDownList ddl, int? TehsilID, int? DistrictID, string Tehsil)
        {
            List<PDMS_Tehsil> MML = new List<PDMS_Tehsil>();
            try
            {
                DbParameter TehsilIDP = provider.CreateParameter("TehsilID", TehsilID, DbType.Int32);
                DbParameter DistrictIDP = provider.CreateParameter("DistrictID", DistrictID, DbType.Int32);
                DbParameter TehsilP = provider.CreateParameter("Tehsil", string.IsNullOrEmpty(Tehsil) ? null : Tehsil, DbType.String);

                DbParameter[] Params = new DbParameter[3] { TehsilIDP, DistrictIDP, TehsilP };
                using (DataSet DataSet = provider.Select("ZDMS_GetTehsil", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PDMS_Tehsil()
                            {
                                TehsilID = Convert.ToInt32(dr["TehsilID"]),
                                Tehsil = Convert.ToString(dr["Tehsil"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataValueField = "TehsilID";
            ddl.DataTextField = "Tehsil";
            ddl.DataSource = MML;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
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
    }
}
