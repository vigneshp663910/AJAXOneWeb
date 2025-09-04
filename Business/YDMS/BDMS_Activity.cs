using DataAccess;
using Newtonsoft.Json;
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
//############ Business Class for Activity Process
namespace Business
{
    public class BDMS_Activity
    {
        private IDataAccess provider;
        private string MaterialGroup = "889";
        private string EncryptionKey = "ENC@KEY@123";

        public void BindActivityActualDataForApproval(GridView gvData, int DealerID, string FromDate, string ToDate)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Marketing/MarketingActivityActualForApproval?DealerID=" + DealerID + "&FromDate=" + FromDate + "&ToDate=" + ToDate;
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            gvData.DataSource = dt;
            gvData.DataBind();
        }
        public DataTable GetActivityActualDataForApproval_ForExcel(int DealerID, string FromDate, string ToDate)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Marketing/MarketingActivityActualForApproval?DealerID=" + DealerID + "&FromDate=" + FromDate + "&ToDate=" + ToDate;
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

            return dt;
        }

        public List<PDealer> GetDealerByUserID(long UserID)
        {
            List<PDealer> Dealers = new List<PDealer>();
            PDealer Dealer = null;
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter[] Params = new DbParameter[1] { UserIDP };
            try
            {
                using (DataSet DataSet = provider.Select("YDMS_SP_GetDealerByUserID", Params))
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


        public BDMS_Activity()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public void GetActivity(DropDownList ddl)
        {
            List<PDMS_ActivityMaster> listActivityMaster = new List<PDMS_ActivityMaster>();
            try
            {


                using (DataSet DataSet = provider.SelectUsingQuery("select MaterialID,MaterialDescription,MaterialCode from ZDMS_MMaterial where materialgroup='889'"))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            listActivityMaster.Add(new PDMS_ActivityMaster()
                            {
                                ActivityID = Convert.ToInt32(dr["MaterialID"]),
                                ActivityName = dr["MaterialDescription"].ToString(),
                                ActivityCode = dr["MaterialCode"].ToString()
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataValueField = "ActivityID";
            ddl.DataTextField = "ActivityName";
            ddl.DataSource = listActivityMaster;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public void GetActivity(DropDownList ddl, string ActType)
        {
            List<PDMS_ActivityMaster> listActivityMaster = new List<PDMS_ActivityMaster>();
            try
            {


                using (DataSet DataSet = provider.SelectUsingQuery(@"select MaterialID,MaterialDescription,MaterialCode from ZDMS_MMaterial A 
                                                                    inner join YDMS_MActivityInfo b on AI_FKMaterialID=MaterialID
                                                                    where materialgroup = '889' and AI_ActivityType = '" + ActType + "'"))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            listActivityMaster.Add(new PDMS_ActivityMaster()
                            {
                                ActivityID = Convert.ToInt32(dr["MaterialID"]),
                                ActivityName = dr["MaterialDescription"].ToString(),
                                ActivityCode = dr["MaterialCode"].ToString()
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataValueField = "ActivityID";
            ddl.DataTextField = "ActivityName";
            ddl.DataSource = listActivityMaster;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public void GetPlannedActivity(DropDownList ddl, int DID)
        {
            List<PDMS_PlannedActivity> listActivityMaster = new List<PDMS_PlannedActivity>();
            try
            {
                DbParameter DIDDP = provider.CreateParameter("DID", DID, DbType.Int32);
                DbParameter[] Params = new DbParameter[1] { DIDDP };
                using (DataSet DataSet = provider.Select("YDMS_SP_GetPendingPlannedActivity", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            listActivityMaster.Add(new PDMS_PlannedActivity()
                            {
                                PkPlanID = Convert.ToInt32(dr["PkPlanID"]),
                                ActivityID = Convert.ToInt32(dr["ActivityID"]),
                                ActivityName = dr["Activity"].ToString()
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataValueField = "PkPlanID";
            ddl.DataTextField = "ActivityName";
            ddl.DataSource = listActivityMaster;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public void GetCommonMaster(DropDownList ddl, string Tag, int ParentID)
        {
            List<PDMS_CommonMaster> listCommonMaster = new List<PDMS_CommonMaster>();
            try
            {
                DbParameter TagDP = provider.CreateParameter("Tag", Tag, DbType.String);
                DbParameter ParentIDDP = provider.CreateParameter("ParentID", ParentID, DbType.Int32);
                DbParameter[] Params = new DbParameter[2] { TagDP, ParentIDDP };

                using (DataSet DataSet = provider.Select("YDMS_GetCommonMaster", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            listCommonMaster.Add(new PDMS_CommonMaster()
                            {
                                _MasterID = Convert.ToInt32(dr["MC_PKMasterID"]),
                                _Description = Convert.ToString(dr["MC_Description"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataValueField = "_MasterID";
            ddl.DataTextField = "_Description";
            ddl.DataSource = listCommonMaster;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public String SaveActivityInfo(int ActivityID, int FunctionaAreaID, int UnitID, double dblBudget, double dblAjaxSharing, double dblDealerSharing, string SAC, double GST, long UserID, string ActivityType = "")
        {
            string sReturn = "";
            try
            {
                DbParameter AI_FKMaterialID = provider.CreateParameter("AI_FKMaterialID", ActivityID, DbType.Int32);
                DbParameter AI_Budget = provider.CreateParameter("AI_Budget", dblBudget, DbType.Double);
                DbParameter AI_FKFunctionaAreaID = provider.CreateParameter("AI_FKFunctionaAreaID", FunctionaAreaID, DbType.Int32);
                DbParameter AI_CreatedBy = provider.CreateParameter("AI_CreatedBy", UserID, DbType.Int64);
                DbParameter AI_FKUnitID = provider.CreateParameter("AI_FKUnitID", UnitID, DbType.Int32);
                DbParameter AI_AjaxSharing = provider.CreateParameter("AI_AjaxSharing", dblAjaxSharing, DbType.Double);
                DbParameter AI_DealerSharing = provider.CreateParameter("AI_DealerSharing", dblDealerSharing, DbType.Double);
                DbParameter AI_SAC = provider.CreateParameter("AI_SAC", SAC, DbType.String);
                DbParameter AI_GST = provider.CreateParameter("AI_GST", GST, DbType.Double);
                DbParameter AI_ActivityType = provider.CreateParameter("AI_ActivityType", ActivityType, DbType.String);
                DbParameter[] Params = new DbParameter[10] { AI_FKMaterialID, AI_Budget, AI_FKFunctionaAreaID, AI_CreatedBy, AI_FKUnitID, AI_AjaxSharing, AI_DealerSharing, AI_SAC, AI_GST, AI_ActivityType };
                provider.Insert("YDMS_SP_MActivityInfo_Save", Params, false);
                sReturn = "Saved";
            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }
        public List<PDMS_ActivityMaster> GetActivityMaster()
        {
            List<PDMS_ActivityMaster> listActivityMaster = new List<PDMS_ActivityMaster>();

            return listActivityMaster;
        }
        public void GetActivityInfoData(GridView gridView, int ActivityID)
        {
            DataTable dt = new DataTable();
            DbParameter ActivityIDDP = provider.CreateParameter("ActivityID", ActivityID, DbType.Int32);
            DbParameter[] Params = new DbParameter[1] { ActivityIDDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_GetMActivityInfo_Grid", Params))
            {
                if (DataSet != null)
                {
                    gridView.DataSource = DataSet;
                    gridView.DataBind();
                }
            }

        }
        public DataTable GetActivityInfoData(int ActivityID)
        {
            DataTable dt = new DataTable();
            DbParameter ActivityIDDP = provider.CreateParameter("ActivityID", ActivityID, DbType.Int32);
            DbParameter[] Params = new DbParameter[1] { ActivityIDDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_GetMActivityInfo_Excel", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;
        }
        public List<PDMS_ActivityInfo> GetActivityInfoDataByID(int ActivityID)
        {
            List<PDMS_ActivityInfo> listActivityInfo = new List<PDMS_ActivityInfo>();
            DbParameter ActivityIDDP = provider.CreateParameter("ActivityID", ActivityID, DbType.Int32);
            DbParameter[] Params = new DbParameter[1] { ActivityIDDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_GetMActivityInfo", Params))
            {
                if (DataSet != null)
                {
                    foreach (DataRow dr in DataSet.Tables[0].Rows)
                    {
                        listActivityInfo.Add(new PDMS_ActivityInfo()
                        {
                            ActivityID = Convert.ToInt32(dr["ActivityID"]),
                            FunctionalAreaID = Convert.ToInt32(dr["FunctionaAreaID"]),
                            FunctionalArea = dr["FunctionalArea"].ToString(),
                            Unit = Convert.ToInt32(dr["UnitID"]),
                            UnitDesc = dr["Unit"].ToString(),
                            Budget = Convert.ToDouble(dr["AI_Budget"]),
                            AjaxSharing = Convert.ToDouble(dr["AI_AjaxSharing"]),
                            DealerSharing = Convert.ToDouble(dr["AI_DealerSharing"]),
                            SAC = dr["SAC"].ToString(),
                            GST = Convert.ToDouble(dr["GST"]),
                            ActivityType = dr["ActivityType"].ToString()
                        });
                    }
                }
            }
            return listActivityInfo;
        }
        public String SaveActivityPlan(int PKPlanID, int ActivityID, int DealerID, int NoofUnits, string FromDate, string ToDate, string Location, string Remarks, long UserID)
        {
            string endPoint = "MarketingActivity/SaveActivityPlan?PKPlanID=" + PKPlanID + "&ActivityID=" + ActivityID + "&DealerID="+ DealerID 
                + "&NoofUnits=" + NoofUnits + "&FromDate=" + FromDate + "&ToDate=" + ToDate + "&Location=" + Location + "&Remarks=" + Remarks;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                throw new Exception(Results.Message);
            }
            return JsonConvert.DeserializeObject<String>(JsonConvert.SerializeObject(Results.Data));

            //string sReturn = "";
            //try
            //{
            //    DbParameter AP_PKPlanID = provider.CreateParameter("AP_PKPlanID", PKPlanID, DbType.Int32);
            //    DbParameter AP_FKDealerID = provider.CreateParameter("AP_FKDealerID", DealerID, DbType.Int32);
            //    DbParameter AP_FKActivityID = provider.CreateParameter("AP_FKActivityID", ActivityID, DbType.Int32);
            //    DbParameter AP_NoofUnits = provider.CreateParameter("AP_NoofUnits", NoofUnits, DbType.Int32);
            //    DbParameter AP_FromDate = provider.CreateParameter("AP_FromDate", FromDate, DbType.Date);
            //    DbParameter AP_ToDate = provider.CreateParameter("AP_ToDate", ToDate, DbType.Date);
            //    DbParameter AP_Location = provider.CreateParameter("AP_Location", Location, DbType.String);
            //    DbParameter AP_Remarks = provider.CreateParameter("AP_Remarks", Remarks, DbType.String);

            //    DbParameter AP_CreatedBy = provider.CreateParameter("AP_CreatedBy", UserID, DbType.Int64);


            //    DbParameter[] Params = new DbParameter[9] { AP_PKPlanID, AP_FKDealerID, AP_FKActivityID, AP_NoofUnits, AP_FromDate, AP_ToDate, AP_Location, AP_Remarks, AP_CreatedBy };

            //    sReturn = "Saved|" + Convert.ToString(provider.GetScalar("YDMS_SP_TActivityPlan_Save", Params));
            //}
            //catch (Exception ex)
            //{
            //    sReturn = ex.Message;
            //}

            //return sReturn;
        }
        public void BindActivityPlanData(GridView gvData, int DealerID, int ActivityID, string FromDate, string ToDate, long UserID, int Status)
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter ActivityIDDP = provider.CreateParameter("ActivityID", ActivityID, DbType.Int32);
            DbParameter FromDateDP = provider.CreateParameter("FromDate", FromDate, DbType.String);
            DbParameter ToDateDP = provider.CreateParameter("ToDate", ToDate, DbType.String);
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter StatusDP = provider.CreateParameter("Status", Status, DbType.Int64);
            DbParameter[] Params = new DbParameter[6] { DealerIDDP, ActivityIDDP, FromDateDP, ToDateDP, UserIDDP, StatusDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_TActivityPlan_GetData", Params))
            {
                dt = DataSet.Tables[0];
                gvData.DataSource = dt;
                gvData.DataBind();
                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    LinkButton lnkEdit = gvData.Rows[i].FindControl("lnkEdit") as LinkButton;
                    if (dt.Rows[i]["IsUpdated"].ToString() == "Y" && PSession.User.UserTypeID == 7)
                    {
                        lnkEdit.Visible = false;
                    }
                    else if (dt.Rows[i]["AP_Status"].ToString() != "0")
                    {
                        lnkEdit.Visible = false;
                    }
                }
            }

        }
        public DataTable GetActivityPlanData(int DealerID, int ActivityID, string FromDate, string ToDate, int Status)
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter ActivityIDDP = provider.CreateParameter("ActivityID", ActivityID, DbType.Int32);
            DbParameter FromDateDP = provider.CreateParameter("FromDate", FromDate, DbType.String);
            DbParameter ToDateDP = provider.CreateParameter("ToDate", ToDate, DbType.String);
            DbParameter UserIDDP = provider.CreateParameter("UserID", PSession.User.UserID, DbType.String);
            DbParameter StatusDP = provider.CreateParameter("Status", Status, DbType.Int32);
            DbParameter[] Params = new DbParameter[6] { DealerIDDP, ActivityIDDP, FromDateDP, ToDateDP, UserIDDP, StatusDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_TActivityPlan_GetData", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;
        }
        public List<PDMS_ActivityPlan> GetActivityPlanDataByID(int PKPLanID)
        {
            List<PDMS_ActivityPlan> listActivityPlan = new List<PDMS_ActivityPlan>();
            DbParameter PKPLanIDDP = provider.CreateParameter("PKPLanID", PKPLanID, DbType.Int32);
            DbParameter[] Params = new DbParameter[1] { PKPLanIDDP };

            using (DataSet DataSet = provider.Select("YDMS_GetActivityPlanDataByID", Params))
            {
                if (DataSet != null)
                {
                    foreach (DataRow dr in DataSet.Tables[0].Rows)
                    {
                        listActivityPlan.Add(new PDMS_ActivityPlan()
                        {
                            AP_PKPlanID = Convert.ToInt32(dr["AP_PKPlanID"]),
                            AP_FKDealerID = Convert.ToInt32(dr["AP_FKDealerID"]),
                            AP_FKActivityID = Convert.ToInt32(dr["AP_FKActivityID"]),
                            AP_NoofUnits = Convert.ToInt32(dr["AP_NoofUnits"]),
                            AP_FromDate = Convert.ToDateTime(dr["AP_FromDate"]),
                            AP_ToDate = Convert.ToDateTime(dr["AP_ToDate"]),
                            AP_Location = (dr["AP_Location"].ToString()),
                            AP_Remarks = (dr["AP_Remarks"].ToString()),
                            AP_BudgetPerUnit = Convert.ToDouble(dr["BudgetPerUnit"].ToString()),
                            AP_ExpBudget = Convert.ToDouble(dr["ExpBudget"].ToString()),
                            AP_AjaxSharing = Convert.ToDouble(dr["AP_AjaxSharing"].ToString()),
                            AI_AjaxSharing = Convert.ToDouble(dr["AI_AjaxSharing"].ToString()),
                            AP_Unit = (dr["Unit"].ToString()),

                        });
                    }
                }
            }
            return listActivityPlan;
        }
        public String SaveActivityActual(int PKPlanID, int NoofUnits, string FromDate, string ToDate, string Location, string Remarks, long UserID, int Status, string NDRemarks, double dblExpenses)
        {
            string sReturn = "";
            try
            {
                DbParameter AA_FKPlanID = provider.CreateParameter("AA_FKPlanID", PKPlanID, DbType.Int32);
                DbParameter AA_Status = provider.CreateParameter("AA_Status", Status, DbType.Int32);
                DbParameter AA_NoofUnits = provider.CreateParameter("AA_NoofUnits", NoofUnits, DbType.Int32);
                DbParameter AA_FromDate = provider.CreateParameter("AA_FromDate", FromDate, DbType.Date);
                DbParameter AA_ToDate = provider.CreateParameter("AA_ToDate", ToDate, DbType.Date);
                DbParameter AA_Location = provider.CreateParameter("AA_Location", Location, DbType.String);
                DbParameter AA_Remarks = provider.CreateParameter("AA_Remarks", Remarks, DbType.String);
                DbParameter AA_NDRemarks = provider.CreateParameter("AA_NDRemarks", NDRemarks, DbType.String);
                DbParameter AA_CreatedBy = provider.CreateParameter("AA_CreatedBy", UserID, DbType.Int64);
                DbParameter AA_Expense = provider.CreateParameter("AA_Expenses", dblExpenses, DbType.Double);


                DbParameter[] Params = new DbParameter[10] { AA_FKPlanID, AA_NoofUnits, AA_FromDate, AA_ToDate, AA_Location, AA_Remarks, AA_CreatedBy, AA_Status, AA_NDRemarks, AA_Expense };
                provider.Insert("YDMS_SP_TActivityActual_Save", Params, false);
                sReturn = "Saved";
            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }
        public String SaveActivityClaim(int PKActualID, int DealerID, int ActivityID, int NoofUnits, string FromDate, string ToDate, string Location, string Remarks, long UserID, double dblExpenses)
        {
            string endPoint = "MarketingActivity/SaveActivityClaim?PKActualID=" + PKActualID + "&DealerID=" + DealerID + "&ActivityID=" + ActivityID
               + "&NoofUnits=" + NoofUnits + "&FromDate=" + FromDate + "&ToDate=" + ToDate + "&Location=" + Location + "&Remarks=" + Remarks + "&dblExpenses=" + dblExpenses;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                throw new Exception(Results.Message);
            }
            return JsonConvert.DeserializeObject<String>(JsonConvert.SerializeObject(Results.Data));

            //string sReturn = "";
            //try
            //{
            //    DbParameter AA_PKActualID = provider.CreateParameter("AA_PKActualID", PKActualID, DbType.Int32);
            //    DbParameter AA_FKDEALERID = provider.CreateParameter("AA_FKDEALERID", DealerID, DbType.Int32);
            //    DbParameter AA_FKActivityID = provider.CreateParameter("AA_FKActivityID", ActivityID, DbType.Int32);
            //    DbParameter AA_NoofUnits = provider.CreateParameter("AA_NoofUnits", NoofUnits, DbType.Int32);
            //    DbParameter AA_FromDate = provider.CreateParameter("AA_FromDate", FromDate, DbType.Date);
            //    DbParameter AA_ToDate = provider.CreateParameter("AA_ToDate", ToDate, DbType.Date);
            //    DbParameter AA_Location = provider.CreateParameter("AA_Location", Location, DbType.String);
            //    DbParameter AA_Remarks = provider.CreateParameter("AA_Remarks", Remarks, DbType.String);

            //    DbParameter AA_CreatedBy = provider.CreateParameter("AA_CreatedBy", UserID, DbType.Int64);
            //    DbParameter AA_Expense = provider.CreateParameter("AA_Expenses", dblExpenses, DbType.Double);


            //    DbParameter[] Params = new DbParameter[10] { AA_PKActualID, AA_FKDEALERID, AA_FKActivityID, AA_NoofUnits, AA_FromDate, AA_ToDate, AA_Location, AA_Remarks, AA_CreatedBy, AA_Expense };
            //    sReturn = Convert.ToInt32(provider.GetScalar("YDMS_SP_DirectActivityClaim_Save", Params)).ToString();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            //return sReturn;
        }
        public string SaveActivityAttachments(int PKPlanID, List<PDMS_ActivityDocs> lstDocs)
        {
            string sReturn = "";
            try
            {
                foreach (PDMS_ActivityDocs obj in lstDocs)
                {
                    DbParameter AA_FKPlanID = provider.CreateParameter("AA_FKPlanID", PKPlanID, DbType.Int32);
                    DbParameter AD_Sno = provider.CreateParameter("AD_Sno", obj.AD_Sno, DbType.Int32);
                    DbParameter AD_ContentType = provider.CreateParameter("AD_ContentType", obj.AD_ContentType, DbType.String);
                    DbParameter AD_Description = provider.CreateParameter("AD_Description", obj.AD_Description, DbType.String);
                    DbParameter AD_FileName = provider.CreateParameter("AD_FileName", obj.AD_FileName, DbType.String);
                    DbParameter AD_AttachedFile = provider.CreateParameter("AD_AttachedFile", obj.AD_AttachedFile, DbType.Binary);
                    DbParameter AD_FileSize = provider.CreateParameter("AD_FileSize", obj.AD_FileSize, DbType.Int32);
                    DbParameter[] Params = new DbParameter[7] { AA_FKPlanID, AD_Sno, AD_ContentType, AD_FileName, AD_AttachedFile, AD_FileSize, AD_Description };
                    provider.Insert("YDMS_SP_TActivityDocs_Save", Params, false);
                    sReturn = "Saved";

                }
            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }
        public string SaveActivityAttachments_ActualID(int PKActualID, List<PDMS_ActivityDocs> lstDocs)
        {
            string sReturn = "";
            try
            {
                foreach (PDMS_ActivityDocs obj in lstDocs)
                {
                    DbParameter AA_PKActualID = provider.CreateParameter("AA_PKActualID", PKActualID, DbType.Int32);
                    DbParameter AD_Sno = provider.CreateParameter("AD_Sno", obj.AD_Sno, DbType.Int32);
                    DbParameter AD_ContentType = provider.CreateParameter("AD_ContentType", obj.AD_ContentType, DbType.String);
                    DbParameter AD_Description = provider.CreateParameter("AD_Description", obj.AD_Description, DbType.String);
                    DbParameter AD_FileName = provider.CreateParameter("AD_FileName", obj.AD_FileName, DbType.String);
                    DbParameter AD_AttachedFile = provider.CreateParameter("AD_AttachedFile", obj.AD_AttachedFile, DbType.Binary);
                    DbParameter AD_FileSize = provider.CreateParameter("AD_FileSize", obj.AD_FileSize, DbType.Int32);
                    DbParameter[] Params = new DbParameter[7] { AA_PKActualID, AD_Sno, AD_ContentType, AD_FileName, AD_AttachedFile, AD_FileSize, AD_Description };
                    provider.Insert("YDMS_SP_TActivityDocs_Save_ActualID", Params, false);
                    sReturn = "Saved";

                }
            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }
        public string DeleteActivityAttachments(int PKPlanID, int iSno)
        {
            string sReturn = "";
            try
            {
                DbParameter AA_FKPlanID = provider.CreateParameter("AA_FKPlanID", PKPlanID, DbType.Int32);
                DbParameter AD_Sno = provider.CreateParameter("AD_Sno", iSno, DbType.Int32);


                DbParameter[] Params = new DbParameter[2] { AA_FKPlanID, AD_Sno };
                provider.Insert("YDMS_SP_TActivityDocs_Delete", Params, false);
                sReturn = "Saved";

            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }

            return sReturn;
        }
        public void BindActivityActualData(GridView gvData, int DealerID, string DateOn, string FromDate, string ToDate, long UserID)
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter DateOnDP = provider.CreateParameter("DateOn", DateOn, DbType.String);
            DbParameter FromDateDP = provider.CreateParameter("FromDate", FromDate, DbType.String);
            DbParameter ToDateDP = provider.CreateParameter("ToDate", ToDate, DbType.String);
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter[] Params = new DbParameter[5] { DealerIDDP, DateOnDP, FromDateDP, ToDateDP, UserIDDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_TActivityActual_GetData", Params))
            {
                dt = DataSet.Tables[0];
                gvData.DataSource = dt;
                gvData.DataBind();
            }

        }
        public DataTable GetActivityClaimData_WOPlan(int DealerID, string FromDate, string ToDate, long UserID)
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter FromDateDP = provider.CreateParameter("FromDate", FromDate, DbType.String);
            DbParameter ToDateDP = provider.CreateParameter("ToDate", ToDate, DbType.String);
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter[] Params = new DbParameter[4] { DealerIDDP, FromDateDP, ToDateDP, UserIDDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_GetData_ActivityClaim_WOPlan", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;
        }

        public void BindActivityActualDataForApproval(GridView gvData, int DealerID, string FromDate, string ToDate, int AppStatus, long UserID, int AppLevel, int FAID)
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter FromDateDP = provider.CreateParameter("FromDate", FromDate, DbType.String);
            DbParameter ToDateDP = provider.CreateParameter("ToDate", ToDate, DbType.String);
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter AppStatusDP = provider.CreateParameter("AppStatus", AppStatus, DbType.Int32);
            DbParameter AppLevelDP = provider.CreateParameter("AppLevel", AppLevel, DbType.Int32);
            DbParameter FAIDDP = provider.CreateParameter("FAID", FAID, DbType.Int32);
            DbParameter[] Params = new DbParameter[7] { DealerIDDP, AppStatusDP, FromDateDP, ToDateDP, UserIDDP, AppLevelDP, FAIDDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_TActivityActual_GetData_ForApproval", Params))
            {
                dt = DataSet.Tables[0];
                gvData.DataSource = dt;
                gvData.DataBind();
            }

        }
        public DataTable GetActivityActualData(int DealerID, string DateOn, string FromDate, string ToDate, long UserID)
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter DateOnDP = provider.CreateParameter("DateOn", DateOn, DbType.String);
            DbParameter FromDateDP = provider.CreateParameter("FromDate", FromDate, DbType.String);
            DbParameter ToDateDP = provider.CreateParameter("ToDate", ToDate, DbType.String);
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter[] Params = new DbParameter[5] { DealerIDDP, DateOnDP, FromDateDP, ToDateDP, UserIDDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_TActivityActual_GetData", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;
        }
        public DataTable GetActivityActualDataForExcel(int DealerID, string DateOn, string FromDate, string ToDate, long UserID)
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter DateOnDP = provider.CreateParameter("DateOn", DateOn, DbType.String);
            DbParameter FromDateDP = provider.CreateParameter("FromDate", FromDate, DbType.String);
            DbParameter ToDateDP = provider.CreateParameter("ToDate", ToDate, DbType.String);
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter[] Params = new DbParameter[5] { DealerIDDP, DateOnDP, FromDateDP, ToDateDP, UserIDDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_TActivityActual_GetData_ForExcel", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;
        }
        public DataTable GetActivityActualDataForApproval(int DealerID, string FromDate, string ToDate, int AppStatus, long UserID, int AppLevel)
        {
            DataTable dt = new DataTable();

            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter FromDateDP = provider.CreateParameter("FromDate", FromDate, DbType.String);
            DbParameter ToDateDP = provider.CreateParameter("ToDate", ToDate, DbType.String);
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter AppStatusDP = provider.CreateParameter("AppStatus", AppStatus, DbType.Int32);
            DbParameter AppLevelDP = provider.CreateParameter("AppLevel", AppLevel, DbType.Int32);
            DbParameter[] Params = new DbParameter[6] { DealerIDDP, AppStatusDP, FromDateDP, ToDateDP, UserIDDP, AppLevelDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_TActivityActual_GetData_ForApproval", Params))
            {
                dt = DataSet.Tables[0];
            }

            return dt;
        }
        public DataTable GetActivityActualDataForApproval_ForExcel(int DealerID, string FromDate, string ToDate, int AppStatus, long UserID, int AppLevel, int FAID)
        {
            DataTable dt = new DataTable();

            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter FromDateDP = provider.CreateParameter("FromDate", FromDate, DbType.String);
            DbParameter ToDateDP = provider.CreateParameter("ToDate", ToDate, DbType.String);
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter AppStatusDP = provider.CreateParameter("AppStatus", AppStatus, DbType.Int32);
            DbParameter AppLevelDP = provider.CreateParameter("AppLevel", AppLevel, DbType.Int32);
            DbParameter FAIDDP = provider.CreateParameter("FAID", FAID, DbType.Int32);
            DbParameter[] Params = new DbParameter[7] { DealerIDDP, AppStatusDP, FromDateDP, ToDateDP, UserIDDP, AppLevelDP, FAIDDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_TActivityActual_GetData_ForApproval_ForExcel", Params))
            {
                dt = DataSet.Tables[0];
            }

            return dt;
        }
        public DataTable GetActivityActualDataByID(int PKPLanID)
        {
            DataTable dtReturn = new DataTable();
            DbParameter PKPLanIDDP = provider.CreateParameter("PKPLanID", PKPLanID, DbType.Int32);
            DbParameter[] Params = new DbParameter[1] { PKPLanIDDP };

            using (DataSet DataSet = provider.Select("YDMS_SP_TActivityActual_GetData_ByID", Params))
            {
                dtReturn = DataSet.Tables[0];
            }
            return dtReturn;


        }
        public DataSet GetActualDataByID(int PlanID)
        {
            DbParameter PlanIDDP = provider.CreateParameter("PKPlanID", PlanID, DbType.Int32);
            DbParameter[] Params = new DbParameter[1] { PlanIDDP };
            DataSet dsReturn = provider.Select("YDMS_SP_ActivityActualDetail_ByID", Params);
            return dsReturn;

        }
        public DataSet GetActivityActualDataByActualID(int ActualID)
        {
            DbParameter ActualIDDP = provider.CreateParameter("PKActualID", ActualID, DbType.Int32);
            DbParameter[] Params = new DbParameter[1] { ActualIDDP };
            DataSet dsReturn = provider.Select("YDMS_SP_ActivityActualDetail_ByActualID", Params);
            return dsReturn;

        }
        public string SaveActivityApproval(int AAP_FKActualID, int AAP_ApprovalLevel, long AAP_UpdatedBy, int AAP_Status, double AAP_Amount, string AAP_Remarks)
        {
            string endPoint = "MarketingActivity/SaveActivityApproval?AAP_FKActualID=" + AAP_FKActualID + "&AAP_ApprovalLevel=" + AAP_ApprovalLevel + "&AAP_UpdatedBy=" + AAP_UpdatedBy
              + "&AAP_Status=" + AAP_Status + "&AAP_Amount=" + AAP_Amount + "&AAP_Remarks=" + AAP_Remarks;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                throw new Exception(Results.Message);
            }
            return JsonConvert.DeserializeObject<String>(JsonConvert.SerializeObject(Results.Data));

            //string sReturn = "";
            //try
            //{
            //    DbParameter AAP_FKActualIDDP = provider.CreateParameter("AAP_FKActualID", AAP_FKActualID, DbType.Int32);
            //    DbParameter AAP_ApprovalLevelDP = provider.CreateParameter("AAP_ApprovalLevel", AAP_ApprovalLevel, DbType.Int32);
            //    DbParameter AAP_UpdatedByDP = provider.CreateParameter("AAP_UpdatedBy", AAP_UpdatedBy, DbType.Int64);
            //    DbParameter AAP_StatusDP = provider.CreateParameter("AAP_Status", AAP_Status, DbType.Int32);
            //    DbParameter AAP_AmountDP = provider.CreateParameter("AAP_Amount", AAP_Amount, DbType.Double);
            //    DbParameter AAP_RemarksDP = provider.CreateParameter("AAP_Remarks", AAP_Remarks, DbType.String);
            //    DbParameter[] Params = new DbParameter[6] { AAP_FKActualIDDP, AAP_ApprovalLevelDP, AAP_UpdatedByDP, AAP_StatusDP, AAP_AmountDP, AAP_RemarksDP };
            //    provider.Insert("YDMS_SP_ActivityApproval_Save", Params, false);
            //    sReturn = "Saved";


            //}
            //catch (Exception ex)
            //{
            //    sReturn = ex.Message;
            //}

            //return sReturn;
        }
        public string GenerateInvoice(int PkActualID, long UserID)
        {

            string endPoint = "MarketingActivity/GenerateInvoice?PkActualID=" + PkActualID + "&UserID=" + UserID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                throw new Exception(Results.Message);
            }
            return JsonConvert.DeserializeObject<String>(JsonConvert.SerializeObject(Results.Data));
            //string sReturn = "";
            //try
            //{
            //    DbParameter PkActualIDDP = provider.CreateParameter("PkActualID", PkActualID, DbType.Int32);
            //    DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            //    DbParameter[] Params = new DbParameter[2] { PkActualIDDP, UserIDDP };
            //    sReturn = provider.GetScalar("YDMS_Activity_GenerateInvoice", Params).ToString(); 
            //}
            //catch (Exception ex)
            //{
            //    sReturn = "Error:" + ex.Message;
            //}

            //return sReturn;
        }
        public string EncryptStringToBase64String(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);

        }
        public string DecryptStringFrombase64(string base64String, byte[] Key, byte[] IV)
        {
            byte[] cipherText = Convert.FromBase64String(base64String);
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
        public DataSet GetActivityInvoiceDetail(int ActualID)
        {
            DbParameter ActualIDDP = provider.CreateParameter("ActualID", ActualID, DbType.Int32);
            DbParameter[] Params = new DbParameter[1] { ActualIDDP };
            DataSet dsReturn = provider.Select("YDMS_GetActivityInvoiceDetail", Params);
            return dsReturn;

        }
        public string Encrypt(string clearText)
        {

            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x6e, 0x20, 0x4d, 0x49, 0x76, 0x61, 0x65, 0x64, 0x64, 0x76, 0x65, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+").PadRight(cipherText.Length + cipherText.Length % 4, '+');

            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x6e, 0x20, 0x4d, 0x49, 0x76, 0x61, 0x65, 0x64, 0x64, 0x76, 0x65, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public DataTable GetInvoiceReportData_grid(int DealerID, string FromDate, string ToDate, long UserID, int Status, string ActivityType = "")
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter FromDateDP = provider.CreateParameter("FromDate", FromDate, DbType.String);
            DbParameter ToDateDP = provider.CreateParameter("ToDate", ToDate, DbType.String);
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter StatusDP = provider.CreateParameter("Status", Status, DbType.Int32);
            DbParameter ActivityTypeDP = provider.CreateParameter("ActivityType", ActivityType, DbType.String);
            DbParameter[] Params = new DbParameter[6] { DealerIDDP, FromDateDP, ToDateDP, UserIDDP, StatusDP, ActivityTypeDP };

            using (DataSet DataSet = provider.Select("YDMS_ActivityInvoiceReport_Grid", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;

        }
        public DataTable GetInvoiceReportData(int DealerID, string FromDate, string ToDate, long UserID, int Status, string ActivityType = "")
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter FromDateDP = provider.CreateParameter("FromDate", FromDate, DbType.String);
            DbParameter ToDateDP = provider.CreateParameter("ToDate", ToDate, DbType.String);
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter StatusDP = provider.CreateParameter("Status", Status, DbType.Int32);
            DbParameter ActivityTypeDP = provider.CreateParameter("ActivityType", ActivityType, DbType.String);
            DbParameter[] Params = new DbParameter[6] { DealerIDDP, FromDateDP, ToDateDP, UserIDDP, StatusDP, ActivityTypeDP };

            using (DataSet DataSet = provider.Select("YDMS_ActivityInvoiceReport", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;

        }
        public void UpdateInvoice_SapData(int HdrID, string SAP_Doc, string AE_Inv_Accounted_Date, string Payment_Voucher_No, DateTime? Payment_Date, double Payment_Value, double TDS)
        {
            DbParameter AIH_SAP_Doc = provider.CreateParameter("AIH_SAP_Doc", SAP_Doc, DbType.String);
            DbParameter AIH_AE_Inv_Accounted_Date = provider.CreateParameter("AIH_AE_Inv_Accounted_Date", AE_Inv_Accounted_Date, DbType.String);
            DbParameter AIH_Payment_Voucher_No = provider.CreateParameter("AIH_Payment_Voucher_No", Payment_Voucher_No, DbType.String);
            DbParameter AIH_Payment_Date = provider.CreateParameter("AIH_Payment_Date", Payment_Date, DbType.DateTime);
            DbParameter AIH_Payment_Value = provider.CreateParameter("AIH_Payment_Value", Payment_Value, DbType.Double);
            DbParameter AIH_TDS = provider.CreateParameter("AIH_TDS", TDS, DbType.Double);
            DbParameter AIH_PkHdrID = provider.CreateParameter("AIH_PkHdrID", HdrID, DbType.Int32);
            DbParameter[] Params = new DbParameter[7] { AIH_SAP_Doc, AIH_AE_Inv_Accounted_Date, AIH_Payment_Voucher_No, AIH_Payment_Date, AIH_Payment_Value, AIH_TDS, AIH_PkHdrID };
            provider.Update("YDMS_ACTIVITYINVOICEHDR_UPDATE", Params, false);
        }
        public DataTable GetInvoiceReportData_ForSAP(int DealerID, string FromDate, string ToDate, long UserID, int Status, string ActivityType = "")
        {
            DataTable dt = new DataTable();
            DbParameter DealerIDDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter FromDateDP = provider.CreateParameter("FromDate", FromDate, DbType.String);
            DbParameter ToDateDP = provider.CreateParameter("ToDate", ToDate, DbType.String);
            DbParameter UserIDDP = provider.CreateParameter("UserID", UserID, DbType.Int64);
            DbParameter StatusDP = provider.CreateParameter("Status", Status, DbType.Int32);
            DbParameter ActivityTypeDP = provider.CreateParameter("ActivityType", ActivityType, DbType.String);
            DbParameter[] Params = new DbParameter[6] { DealerIDDP, FromDateDP, ToDateDP, UserIDDP, StatusDP, ActivityTypeDP };

            using (DataSet DataSet = provider.Select("YDMS_ActivityInvoiceData_ForSAP", Params))
            {
                dt = DataSet.Tables[0];
            }
            return dt;

        } 
        public PAttachedFile DowloadActivityDocs(String FileName)
        {
            string endPoint = "MarketingActivity/DowloadActivityDocs?FileName=" + FileName;
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            return JsonConvert.DeserializeObject<PAttachedFile>(JsonConvert.SerializeObject(Result.Data));
        }

        public PApiResult GetMarketingClaimReport(int? DealerID, string ActivityType, int? StatusID, string FromDate, string ToDate, int Excel, int? PageIndex = null, int? PageSize = null)
        { 
            string endPoint = "Marketing/GetMarketingClaimReport?DealerID=" + DealerID + "&ActivityType=" + ActivityType + "&StatusID=" + StatusID + "&FromDate=" + FromDate + "&ToDate=" + ToDate
                + "&Excel=" + Excel + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize; ;

            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
}
