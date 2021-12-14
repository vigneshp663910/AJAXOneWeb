using DataAccess;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Business
{
    public class BAppSetting
    {
        #region Class Variables
        private IDataAccess provider;
        #endregion

        #region Constructor
        public BAppSetting()
        {
            provider = new ProviderFactory().GetProvider();
        }
        #endregion
        public PAppSetting getAppSetting(int UserId)
        {
            PAppSetting appSetting = new PAppSetting();

            try
            {
                DbParameter UserIdP = provider.CreateParameter("UserId", UserId, DbType.Int32);
                DbParameter[] Params = new DbParameter[1] { UserIdP };
                using (DataSet TicketTypeDataSet = provider.Select("getAppSetting", Params))
                {
                    if (TicketTypeDataSet != null)
                    {
                        foreach (DataRow dr in TicketTypeDataSet.Tables[0].Rows)
                        {
                            appSetting.Admin = dr["Admin"] == DBNull.Value ? false : true;
                            appSetting.HOD = dr["HOD"] == DBNull.Value ? false : true;
                            appSetting.Manger = dr["Manger"] == DBNull.Value ? false : true;
                            appSetting.SupperUser = dr["SupperUser"] == DBNull.Value ? false : true;
                            appSetting.TicketAssign = dr["TicketAssign"] == DBNull.Value ? false : true;
                            appSetting.TRApprover = dr["TRApprover"] == DBNull.Value ? false : true;
                        }
                    }
                }
                // This call is for track the status and loged into the trace logeer
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return appSetting;
        }
    }
}
