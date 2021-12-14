using DataAccess;
using Properties;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Business
{
    public class BDMS_Planning
    {
        private IDataAccess provider;
        public BDMS_Planning()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public void BindYear(DropDownList ddl)
        {
            List<PDMS_DDLList> lst = new List<PDMS_DDLList>();
            
            for(int i = DateTime.Now.Year;i>DateTime.Now.Year-5;i--)
            {
                lst.Add(new PDMS_DDLList(i.ToString(), i.ToString()));
            }
            ddl.DataTextField = "_Text";
            ddl.DataValueField = "_Value";
            ddl.DataSource = lst;
            ddl.DataBind();
        }
        public void BindFinancialYear(DropDownList ddl)
        {
            List<PDMS_DDLList> lst = new List<PDMS_DDLList>();

            for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 5; i--)
            {
                lst.Add(new PDMS_DDLList(i.ToString() + "-" + (i + 1).ToString(), i.ToString() + "-" + (i + 1).ToString()));
            }
            ddl.DataTextField = "_Text";
            ddl.DataValueField = "_Value";
            ddl.DataSource = lst;
            ddl.DataBind();
        }
        public void BindMonths(DropDownList ddl)
        {
            List<PDMS_DDLList> lst = new List<PDMS_DDLList>();
            DateTime datStart = DateTime.Parse("01-Jan-2020");

            for (int i = 1; i < 13 ; i++)
            {
                lst.Add(new PDMS_DDLList(datStart.ToString("MMM").ToString(), i.ToString()));
                datStart = datStart.AddMonths(1);
            }
            ddl.DataTextField = "_Text";
            ddl.DataValueField = "_Value";
            ddl.DataSource = lst;
            ddl.DataBind();
        }
        public DataTable GetAllProducts()
        {
            DataTable dt = new DataTable();
            using (DataSet DataSet = provider.Select("YDMS_Planning_GetProduct"))
            {
                dt = DataSet.Tables[0];
            }
            return dt;
        }
        public DataTable GetModelsByProductID(int _ProductID )
        {
            DataTable dt = new DataTable();
            DbParameter ProductID = provider.CreateParameter("ProductID", _ProductID, DbType.Int32);
            DbParameter[] Params = new DbParameter[1] { ProductID };
            using (DataSet DataSet = provider.Select("YDMS_Planning_GetModels", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;
        }
        public void SavePlan(int RPM_FKDealerID,DateTime RPM_PlanDate,int RPM_FKModelID,int RPM_PlanNo,long RPM_CreatedBy,string Tag)
        {

            DbParameter _FKDealerID = provider.CreateParameter("RPM_FKDealerID", RPM_FKDealerID, DbType.Int32);
            DbParameter _PlanDate = provider.CreateParameter("RPM_PlanDate", RPM_PlanDate, DbType.DateTime);
            DbParameter _FKModelID = provider.CreateParameter("RPM_FKModelID", RPM_FKModelID, DbType.Int32);
            DbParameter _PlanNo = provider.CreateParameter("RPM_PlanNo", RPM_PlanNo, DbType.Int32);
            DbParameter _CreatedBy = provider.CreateParameter("RPM_CreatedBy", RPM_CreatedBy, DbType.Int64);
            DbParameter _Tag = provider.CreateParameter("Tag", Tag, DbType.String);
            DbParameter[] Params = new DbParameter[] { _FKDealerID, _PlanDate, _FKModelID, _PlanNo, _CreatedBy, _Tag };
            provider.Insert("YDMS_TRollingPlanningModelWise_Save", Params);
        }
        public DataTable GetPlanForDealer(int DealerID, int Month,int Year)
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("RPM_FKDealerID", DealerID, DbType.Int32);
            DbParameter MonthDP = provider.CreateParameter("Month", Month, DbType.Int32);
            DbParameter YearDP = provider.CreateParameter("Year", Year, DbType.Int32);
            DbParameter[] Params = new DbParameter[] { DealerIDDP, MonthDP, YearDP };
            using (DataSet DataSet = provider.Select("YDMS_TRollingPlanningModelWise_Get", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;
        }
        public DataTable GetABPForDealer(int DealerID, string FY)
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("MPM_FKDealerID", DealerID, DbType.Int32);
            
            DbParameter FYDP = provider.CreateParameter("MPM_FY", FY, DbType.String);
            DbParameter[] Params = new DbParameter[] { DealerIDDP, FYDP };
            using (DataSet DataSet = provider.Select("YDMS_SP_ModelPlanMonthWise_Get", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;
        }
        public DataTable GetABPForDealer_All(long UserID, string FY)
        {
            DataTable dt = new DataTable();
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            DbParameter FYDP = provider.CreateParameter("MPM_FY", FY, DbType.String);
            DbParameter[] Params = new DbParameter[] { UserIDDP, FYDP };
            using (DataSet DataSet = provider.Select("YDMS_SP_ModelPlanMonthWise_Get_AllDealer", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;
        }
        public DataTable GetPartABPForDealer(int DealerID, string FY,int SPM_PlanType)
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("SPM_FKDealerID", DealerID, DbType.Int32);
            DbParameter _PlanType = provider.CreateParameter("SPM_PlanType", SPM_PlanType, DbType.Int32);
            DbParameter FYDP = provider.CreateParameter("SPM_FY", FY, DbType.String);
            DbParameter[] Params = new DbParameter[] { DealerIDDP, FYDP, _PlanType };
            using (DataSet DataSet = provider.Select("YDMS_SP_TSparePartPlanMonthWise_Get", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;
        }
        public DataTable GetPartABPForDealer_All(long UserID, string FY, int SPM_PlanType)
        {
            DataTable dt = new DataTable();
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter _PlanType = provider.CreateParameter("SPM_PlanType", SPM_PlanType, DbType.Int32);
            DbParameter FYDP = provider.CreateParameter("SPM_FY", FY, DbType.String);
            DbParameter[] Params = new DbParameter[] { UserIDDP, FYDP, _PlanType };
            using (DataSet DataSet = provider.Select("YDMS_SP_TSparePartPlanMonthWise_Get_All", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;
        }
        public DataTable DeleteABPForDealer(int DealerID, string FY, string MPM_MLevel, int MPM_MLevelID,long MPM_CreatedBy)
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("MPM_FKDealerID", DealerID, DbType.Int32);
            DbParameter FYDP = provider.CreateParameter("MPM_FY", FY, DbType.String);
            DbParameter _Level = provider.CreateParameter("MPM_MLevel", MPM_MLevel, DbType.String);
            DbParameter _LevelID = provider.CreateParameter("MPM_MLevelID", MPM_MLevelID, DbType.Int32);
            DbParameter _CreatedBy = provider.CreateParameter("MPM_CreatedBy", MPM_CreatedBy, DbType.Int64);
            DbParameter[] Params = new DbParameter[] { DealerIDDP, FYDP,_Level,_LevelID,_CreatedBy };
            provider.Delete("YDMS_SP_ModelPlanMonthWise_Delete", Params);
            
            return dt;
        }
        public DataTable DeletePartABPForDealer(int DealerID, string FY, string PartCat, long MPM_CreatedBy,int SPM_PlanType)
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("SPM_FKDealerID", DealerID, DbType.Int32);
            DbParameter FYDP = provider.CreateParameter("SPM_FY", FY, DbType.String);
            DbParameter _PartCategory = provider.CreateParameter("SPM_PartCategory", PartCat, DbType.String);
            DbParameter _CreatedBy = provider.CreateParameter("SPM_CreatedBy", MPM_CreatedBy, DbType.Int64);
            DbParameter _PlanType = provider.CreateParameter("SPM_PlanType", SPM_PlanType, DbType.Int32);
            DbParameter[] Params = new DbParameter[] { DealerIDDP, FYDP, _PartCategory, _CreatedBy,_PlanType };
            provider.Delete("YDMS_SP_SparePartPlanMonthWise_Delete", Params);

            return dt;
        }
        public void SaveABP(int MPM_FKDealerID, int MPM_Year, int MPM_Month, string MPM_MLevel, int MPM_MLevelID,int MPM_PlanNo, long MPM_CreatedBy)
        {

            DbParameter _FKDealerID = provider.CreateParameter("MPM_FKDealerID", MPM_FKDealerID, DbType.Int32);
            DbParameter _Year = provider.CreateParameter("MPM_Year", MPM_Year, DbType.Int32);
            DbParameter _Month = provider.CreateParameter("MPM_Month", MPM_Month, DbType.Int32);
            DbParameter _Level = provider.CreateParameter("MPM_MLevel", MPM_MLevel, DbType.String);
            DbParameter _LevelID = provider.CreateParameter("MPM_MLevelID", MPM_MLevelID, DbType.Int32);
            DbParameter _PlanNo = provider.CreateParameter("MPM_PlanNo", MPM_PlanNo, DbType.Int32);
            DbParameter _CreatedBy = provider.CreateParameter("MPM_CreatedBy", MPM_CreatedBy, DbType.Int64);            
            DbParameter[] Params = new DbParameter[] { _FKDealerID, _Year, _Month, _Level,_LevelID,_PlanNo, _CreatedBy};
            provider.Insert("YDMS_SP_ModelPlanMonthWise_Save", Params);
        }
        public void SavePartABP(int SPM_FKDealerID,int SPM_PlanType, int SPM_Year, int SPM_Month, string SPM_PartCategory, double SPM_PlanNo, long SPM_CreatedBy)
        {

            DbParameter _FKDealerID = provider.CreateParameter("SPM_FKDealerID", SPM_FKDealerID, DbType.Int32);
            DbParameter _Year = provider.CreateParameter("SPM_Year", SPM_Year, DbType.Int32);
            DbParameter _Month = provider.CreateParameter("SPM_Month", SPM_Month, DbType.Int32);
            DbParameter _Level = provider.CreateParameter("SPM_PartCategory", SPM_PartCategory, DbType.String);
            DbParameter _PlanType = provider.CreateParameter("SPM_PlanType", SPM_PlanType, DbType.Int32);
            DbParameter _PlanNo = provider.CreateParameter("SPM_PlanNo", SPM_PlanNo, DbType.Decimal);
            DbParameter _CreatedBy = provider.CreateParameter("SPM_CreatedBy", SPM_CreatedBy, DbType.Int64);
            DbParameter[] Params = new DbParameter[] { _FKDealerID, _Year, _Month, _Level, _PlanNo, _CreatedBy, _PlanType };
            provider.Insert("YDMS_SP_SparePartPlanMonthWise_Save", Params);
        }
    }

}
