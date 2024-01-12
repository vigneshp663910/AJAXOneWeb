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

namespace Business
{
    public class BApplicationSettings
    {
        //#region Class Variables
        //private IDataAccess provider;
        //#endregion

        //#region Constructor
        //public BApplicationSettings()
        //{
        //    provider = new ProviderFactory().GetProvider();
        //}
        //#endregion
        //public List< PApplicationSettings> getAppSetting(int? SettingID)
        //{
        //    List<PApplicationSettings> appSetting = new List<PApplicationSettings>();

        //    try
        //    {
        //        DbParameter SettingIDP = provider.CreateParameter("SettingID", SettingID, DbType.Int32);
        //        DbParameter[] Params = new DbParameter[1] { SettingIDP };
        //        using (DataSet TicketTypeDataSet = provider.Select("GetApplicationSettings", Params))
        //        {
        //            if (TicketTypeDataSet != null)
        //            {
        //                foreach (DataRow dr in TicketTypeDataSet.Tables[0].Rows)
        //                {
        //                    appSetting.Add(new PApplicationSettings()
        //                    {
        //                        SettingID = Convert.ToInt32(dr["SettingID"]),
        //                        Name = Convert.ToString(dr["Name"]),
        //                        Value1 = Convert.ToString(dr["Value1"]),
        //                        Value2 = Convert.ToString(dr["Value2"]),
        //                        Value3 = Convert.ToString(dr["Value3"])
        //                    });
        //                }
        //            }
        //        }
        //        // This call is for track the status and loged into the trace logeer
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return appSetting;
        //}
        //public void UpdateApplicationSetting(int SettingID, string Value1 , string Value2, string Value3)
        //{
        //    try
        //    { 
        //        DbParameter SettingIDP = provider.CreateParameter("SettingID", SettingID, DbType.String);
        //        DbParameter Value1P = provider.CreateParameter("Value1", Value1, DbType.String);
        //        DbParameter Value2P = provider.CreateParameter("Value2", Value2, DbType.String);
        //        DbParameter Value3P = provider.CreateParameter("Value3", Value3, DbType.String);
        //        DbParameter[] Params = new DbParameter[4] { SettingIDP, Value1P, Value2P, Value3P};

        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            provider.Insert("UpdateApplicationSetting", Params);
        //            scope.Complete();
        //        }
        //    }
        //    catch (Exception e1)
        //    {
        //        new FileLogger().LogMessageService("BApplicationSettings", "UpdateApplicationSetting", e1);
        //    }

        //}

    }
}
