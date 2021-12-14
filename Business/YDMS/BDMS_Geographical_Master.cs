using Properties;
using DataAccess;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Business
{
    public class BDMS_Geographical_Master
    {


        private IDataAccess provider;
      
        public DataTable GetCountry()
        {
            DataTable dtcountry = new DataTable();
            dtcountry = null;
         
            try
            {
                using (DataSet DataSet = provider.Select("YDMS_SP_GetCountry"))
                {
                    if (DataSet != null)
                    {
                        dtcountry = DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dtcountry;
        }
        public DataTable GetState(int CountryID)
        {
            DataTable dtState = new DataTable();
            dtState = null;

            try
            {
                DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { CountryIDP };
                using (DataSet DataSet = provider.Select("YDMS_SP_GetStatebyCountry",Params))
                {
                    if (DataSet != null)
                    {
                        dtState = DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dtState;
        }

        public DataTable GetDistt(int CountryID,int StateID)
        {
            DataTable dtDistt= new DataTable();
            dtDistt = null;

            try
            {
                DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int64);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int64);
                DbParameter[] Params = new DbParameter[2] { CountryIDP, StateIDP };
                using (DataSet DataSet = provider.Select("YDMS_SP_GetDistt", Params))
                {
                    if (DataSet != null)
                    {
                        dtDistt = DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dtDistt;
        }

        public DataTable GetTehsil(int CountryID, int StateID,int DisttID)
        {
            DataTable dtTehsil = new DataTable();
            dtTehsil = null;

            try
            {
                DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int64);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int64);
                DbParameter DisttIDP = provider.CreateParameter("DisttID", DisttID, DbType.Int64);
                DbParameter[] Params = new DbParameter[3] { CountryIDP, StateIDP, DisttIDP };
                using (DataSet DataSet = provider.Select("YDMS_SP_GetTehsil", Params))
                {
                    if (DataSet != null)
                    {
                        dtTehsil = DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dtTehsil;
        }


        public DataTable GetVillage(int TehsilID)
        {
            DataTable dtVill = new DataTable();
            dtVill = null;

            try
            {
                DbParameter TehsilIDP = provider.CreateParameter("TehsilID", TehsilID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { TehsilIDP };
                using (DataSet DataSet = provider.Select("YDMS_SP_GetVillage", Params))
                {
                    if (DataSet != null)
                    {
                        dtVill = DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dtVill;
        }
        public BDMS_Geographical_Master()
        {
            provider = new ProviderFactory().GetProvider();
        }

        public String SaveCountry(string CountryName,string Tag)
        {
            string sReturn = "";
            try
            {
                DbParameter CountrynameP = provider.CreateParameter("CountryName", CountryName, DbType.String);
                DbParameter TagP = provider.CreateParameter("Tag", Tag, DbType.String);
                DbParameter[] Params = new DbParameter[2] { CountrynameP, TagP };
                sReturn= provider.GetScalar("YDMS_SP_SaveCountry", Params).ToString();
               
            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }
        public String UpdateGeograpichics(string Name, int ID, int ParentID=0,string tag="" )
        {
            string sReturn = "";
            try
            {
                DbParameter nameP = provider.CreateParameter("Name", Name, DbType.String);
                DbParameter IDP = provider.CreateParameter("ID", ID, DbType.Int32);
                DbParameter ParentIDP = provider.CreateParameter("ParentID", ParentID, DbType.Int32);
                DbParameter tagP = provider.CreateParameter("tag", tag, DbType.String);
                DbParameter[] Params = new DbParameter[4] { nameP, IDP, ParentIDP, tagP };
                sReturn = provider.GetScalar("YDMS_SP_UpdateGeographics", Params).ToString();

            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }
        public String DeleteGeograpichics(int ID,string Tag)
        {
            string sReturn = "";
            try
            {
                DbParameter IDP = provider.CreateParameter("ID", ID, DbType.Int32);
                DbParameter TagP = provider.CreateParameter("Tag", Tag, DbType.String);
                DbParameter[] Params = new DbParameter[2] { IDP, TagP };
                sReturn = provider.GetScalar("YDMS_SP_DeleteGeographics", Params).ToString();

            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }

        public String DeleteVillage(int ID)
        {
            string sReturn = "";
            try
            {
                DbParameter IDP = provider.CreateParameter("VillageID", ID, DbType.Int32);
                DbParameter[] Params = new DbParameter[1] { IDP };
                sReturn = provider.GetScalar("YDMS_SP_DeleteVillage", Params).ToString();

            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }

        public String UpdateVillage(string Name, int TehsilID, int ID)
        {
            string sReturn = "";
            try
            {
                DbParameter nameP = provider.CreateParameter("Name", Name, DbType.String);
                DbParameter TehsilIDP = provider.CreateParameter("TehsilID", TehsilID, DbType.Int32);
                DbParameter IDP = provider.CreateParameter("ID", ID, DbType.Int32);
                DbParameter[] Params = new DbParameter[3] { nameP, TehsilIDP, IDP };
                sReturn = provider.GetScalar("YDMS_SP_UpdateVillage", Params).ToString();

            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }
        public String SaveState(string Name, string Tag,int CountryID)
        {
            string sReturn = "";
            try
            {
                DbParameter nameP = provider.CreateParameter("Name", Name, DbType.String);
                DbParameter TagP = provider.CreateParameter("Tag", Tag, DbType.String);
                DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
                DbParameter[] Params = new DbParameter[3] {nameP, TagP, CountryIDP };
                sReturn = provider.GetScalar("YDMS_SP_SaveStates", Params).ToString();

            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }

        public String SaveDistt(string Name, string Tag, int CountryID,int StateID)
        {
            string sReturn = "";
            try
            {
                DbParameter NameP = provider.CreateParameter("Name", Name, DbType.String);
                DbParameter TagP = provider.CreateParameter("Tag", Tag, DbType.String);               
                DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { NameP, TagP, CountryIDP, StateIDP };
                sReturn = provider.GetScalar("YDMS_SP_SaveDistrict", Params).ToString();

            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }

        public String SaveTehsil(string Name, string Tag, int CountryID, int StateID,int DisttID)
        {
            string sReturn = "";
            try
            {
                DbParameter NameP = provider.CreateParameter("Name", Name, DbType.String);
                DbParameter TagP = provider.CreateParameter("Tag", Tag, DbType.String);
                DbParameter CountryIDP = provider.CreateParameter("CountryID", CountryID, DbType.Int32);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
                DbParameter DisttIDP = provider.CreateParameter("DisttID", DisttID, DbType.Int32);
                DbParameter[] Params = new DbParameter[5] { NameP, TagP, CountryIDP, StateIDP,DisttIDP };
                sReturn = provider.GetScalar("YDMS_SP_SaveTehsil", Params).ToString();

            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }

        public String SaveVillage(string Name, int TehsilID)
        {
            string sReturn = "";
            try
            {
                DbParameter NameP = provider.CreateParameter("Name", Name, DbType.String);
             
                DbParameter TehsilIDP = provider.CreateParameter("TehsilID", TehsilID, DbType.Int32);
                DbParameter[] Params = new DbParameter[2] { NameP, TehsilIDP };
                sReturn = provider.GetScalar("YDMS_SP_SaveVillage", Params).ToString();

            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }

        #region-----TransferVillage Web Forms

        public String TransferVillage(string ID,int TehsilID)
        {
            string sReturn = "";
            try
            {
                DbParameter IDP = provider.CreateParameter("ID", ID, DbType.String);
                DbParameter TehsilIDP = provider.CreateParameter("TehsilID", TehsilID, DbType.Int32);
                DbParameter[] Params = new DbParameter[2] { IDP , TehsilIDP };
                sReturn = provider.GetScalar("SP_YDMS_TrasferVillages_Tehsil", Params).ToString();

            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }
        public String TransferTehsil(string ID, int DisttID)
        {
            string sReturn = "";
            try
            {
                DbParameter IDP = provider.CreateParameter("ID", ID, DbType.String);
                DbParameter DisttIDP = provider.CreateParameter("DisttID", DisttID, DbType.Int32);
                DbParameter[] Params = new DbParameter[2] { IDP, DisttIDP };
                sReturn = provider.GetScalar("YDMS_SP_TrasferTehsil", Params).ToString();

            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }
        #endregion
    }
}

