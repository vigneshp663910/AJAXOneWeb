using Properties;
using DataAccess;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Business
{
    public class BDMS_CustomerSale
    {
        private IDataAccess provider;

        public BDMS_CustomerSale()
        {
            provider = new ProviderFactory().GetProvider();
        }

        public DataTable GetDealerByStateID(long UserID, int StateID)
        {
            DataTable dtDealer = new DataTable();
            dtDealer = null;
         
            try
            {
                DbParameter UserIDP = provider.CreateParameter("UserId", UserID, DbType.Int64);
                DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int64);
                DbParameter[] Params = new DbParameter[2] { UserIDP, StateIDP };
                using (DataSet DataSet = provider.Select("YDMS_SP_GetDealerByStateID_UserID",Params))
                {
                    if (DataSet != null)
                    {
                        dtDealer = DataSet.Tables[0];
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
            return dtDealer;
        }
        public void BindCustomerSale_GetAjaxModel(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            using (DataSet DataSet = provider.Select("YDMS_CustomerSale_GetAjaxModel"))
            {
                dt = DataSet.Tables[0];
                ddl.DataTextField = "Model";
                ddl.DataValueField = "ModelID";
                ddl.DataSource =dt;
                ddl.DataBind();
            }
            
        }
        public void BindCustomerSale_GetCompetitorModel(DropDownList ddl,int MakeID)
        {
            DataTable dt = new DataTable();
            DbParameter MakeIDDP = provider.CreateParameter("MakeID", MakeID, DbType.Int64);
            DbParameter[] Params = new DbParameter[1] { MakeIDDP};
            using (DataSet DataSet = provider.Select("YDMS_CustomerSale_GetCompetitorModel",Params))
            {
                dt = DataSet.Tables[0];
                ddl.DataTextField = "Model";
                ddl.DataValueField = "ModelID";
                ddl.DataSource = dt;
                ddl.DataBind();
            }
            
        }
        public void BindYear(DropDownList ddl)
        {
            for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 25; i--)
            {
                ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        public DataSet GetCustomerSaleData(int StateID, int DealerID,int From_Year, int From_Month, int To_Year, int To_Month, int AjaxModelID, 
            int CompmakeID, int compModelID,long UserID)
        {
            TraceLogger.Log(DateTime.Now);
            DbParameter StateIDDP = provider.CreateParameter("StateID", StateID, DbType.Int64);
            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int64);
            DbParameter From_YearDP = provider.CreateParameter("From_Year", From_Year, DbType.Int64);
            DbParameter To_YearDP = provider.CreateParameter("To_Year", To_Year, DbType.Int64);
            DbParameter To_MonthDP = provider.CreateParameter("To_Month", To_Month, DbType.Int64);
            DbParameter From_MonthDP = provider.CreateParameter("From_Month", From_Month, DbType.Int64);
            DbParameter AjaxModelIDDP = provider.CreateParameter("AjaxModelID", AjaxModelID, DbType.Int64);
            DbParameter CompmakeIDDP = provider.CreateParameter("CompmakeID", CompmakeID, DbType.Int64);
            DbParameter compModelIDDP = provider.CreateParameter("compModelID", compModelID, DbType.Int64);
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter[] dbparams = new DbParameter[] {StateIDDP,DealerIDDP,From_YearDP,To_YearDP,From_MonthDP,To_MonthDP,AjaxModelIDDP, CompmakeIDDP, compModelIDDP,UserIDDP };
            DataSet ds = provider.Select("YDMS_CustomerSale_Search", dbparams);
            TraceLogger.Log(DateTime.Now);
            return ds;

        }
        public DataSet GetCustomerSaleDataByID(int CustomerID)
        {
            DbParameter CustomerIDDP = provider.CreateParameter("ID", CustomerID, DbType.Int32);
            DbParameter[] dbparams = new DbParameter[] { CustomerIDDP};
            DataSet ds = provider.Select("YDMS_CustomerSale_GetDataByID", dbparams);
            return ds;
        }
        public String SaveCustomerSale(int CS_FkDealerID, int CS_Month, int CS_Year , string CS_CustomerName, 
            string CS_ContactPerson ,string CS_ContaceNumber,int CS_FKAjaxModelID,double CS_AjaxPrice,
            int CS_FkCompmakeID , int CS_FKcompModelID, double CS_CompPrice, int CS_Qty,int  CS_Noofvisit,
            int CS_FKResonTypeID,string CS_ReasonRemarks, long CS_CeatedBy,int CS_PkCustSaleID)
        {

            string endPoint = "MarketingActivity/SaveCustomerSale?CS_FkDealerID=" + CS_FkDealerID + "&CS_Month=" + CS_Month + "&CS_Year=" + CS_Year
            + "&CS_CustomerName=" + CS_CustomerName + "&CS_ContactPerson=" + CS_ContactPerson + "&CS_ContaceNumber=" + CS_ContaceNumber
            + "&CS_FKAjaxModelID=" + CS_FKAjaxModelID + "&CS_AjaxPrice=" + CS_AjaxPrice + "&CS_FkCompmakeID=" + CS_FkCompmakeID + "&CS_FKcompModelID=" + CS_FKcompModelID
            + "&CS_CompPrice=" + CS_CompPrice + "&CS_Qty=" + CS_Qty + "&CS_Noofvisit=" + CS_Noofvisit + "&CS_FKResonTypeID=" + CS_FKResonTypeID
            + "&CS_ReasonRemarks=" + CS_ReasonRemarks + "&CS_CeatedBy=" + CS_CeatedBy + "&CS_PkCustSaleID=" + CS_PkCustSaleID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                throw new Exception(Results.Message);
            }
            return JsonConvert.DeserializeObject<String>(JsonConvert.SerializeObject(Results.Data));

            // TraceLogger.Log(DateTime.Now);
            //string sReturn = "";
            //try
            //{
            //    DbParameter DealerIDP = provider.CreateParameter("CS_FkDealerID", CS_FkDealerID, DbType.Int64);
            //    DbParameter MonthP = provider.CreateParameter("CS_Month", CS_Month, DbType.Int32);
            //    DbParameter YearP = provider.CreateParameter("CS_Year", CS_Year, DbType.Int32);
            //    DbParameter CustomerNameP = provider.CreateParameter("CS_CustomerName", CS_CustomerName, DbType.String);
            //    DbParameter ConPersonP = provider.CreateParameter("CS_ContactPerson", CS_ContactPerson, DbType.String);

            //    DbParameter ContNoP = provider.CreateParameter("CS_ContaceNumber", CS_ContaceNumber, DbType.String);
            //    DbParameter ModelIDP = provider.CreateParameter("CS_FKAjaxModelID", CS_FKAjaxModelID, DbType.Int32);
            //    DbParameter PriceP = provider.CreateParameter("CS_AjaxPrice", CS_AjaxPrice, DbType.Decimal);
            //    DbParameter CompMakeIDP = provider.CreateParameter("CS_FkCompmakeID", CS_FkCompmakeID, DbType.Int64);
            //    DbParameter CompModelIDP = provider.CreateParameter("CS_FKcompModelID", CS_FKcompModelID, DbType.Int64);

            //    DbParameter compPriceP = provider.CreateParameter("CS_CompPrice", CS_CompPrice, DbType.Decimal);
            //    DbParameter QtyP = provider.CreateParameter("CS_Qty", CS_Qty, DbType.Int32);
            //    DbParameter NoofVP = provider.CreateParameter("CS_Noofvisit", CS_Noofvisit, DbType.Int64);
            //    DbParameter ReasonIDP = provider.CreateParameter("CS_FKResonTypeID", CS_FKResonTypeID, DbType.Int64);
            //    DbParameter ReasonRemarksP = provider.CreateParameter("CS_ReasonRemarks", CS_ReasonRemarks, DbType.String);
            //    DbParameter CreatedBYP = provider.CreateParameter("CS_CeatedBy", CS_CeatedBy, DbType.Int64);
            //    DbParameter PKIDP = provider.CreateParameter("CS_PkCustSaleID", CS_PkCustSaleID, DbType.Int64);
            //    DbParameter[] Params = new DbParameter[17] {DealerIDP, MonthP, YearP,CustomerNameP,ConPersonP,ContNoP,ModelIDP,PriceP,CompMakeIDP,CompModelIDP,compPriceP,QtyP,NoofVP,ReasonIDP,ReasonRemarksP, CreatedBYP, PKIDP};
            //    sReturn = provider.GetScalar("YDMS_SP_AddUpdate_CustomerSale", Params).ToString();
            //}
            //catch (Exception ex)
            //{
            //    sReturn = ex.Message;
            //}
            //TraceLogger.Log(DateTime.Now);
            //return sReturn;
        }

    }
}

