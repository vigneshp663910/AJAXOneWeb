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
    public class BDMS_MachineMaintenanceLevel
    {
        private IDataAccess provider;
        public BDMS_MachineMaintenanceLevel()
        {
            try
            {
                provider = new ProviderFactory().GetProvider();
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_MachineMaintenanceLevel", "provider : " + e1.Message, null);
            }
        }
        public void GetMachineMaintenanceLevelDDL(DropDownList ddl, int? MachineMaintenanceLevelID, string MachineMaintenanceLevel)
        {
            List<PDMS_MachineMaintenanceLevel> MML = new List<PDMS_MachineMaintenanceLevel>();
            try
            {
                DbParameter MachineMaintenanceLevelP;
                DbParameter MachineMaintenanceLevelIDP = provider.CreateParameter("MachineMaintenanceLevelID", MachineMaintenanceLevelID, DbType.Int32);
                if (!string.IsNullOrEmpty(MachineMaintenanceLevel))
                    MachineMaintenanceLevelP = provider.CreateParameter("MachineMaintenanceLevel", MachineMaintenanceLevel, DbType.String);
                else
                    MachineMaintenanceLevelP = provider.CreateParameter("MachineMaintenanceLevel", null, DbType.String);

                DbParameter[] Params = new DbParameter[2] { MachineMaintenanceLevelIDP, MachineMaintenanceLevelP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMachineMaintenanceLevel", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PDMS_MachineMaintenanceLevel()
                            {
                                MachineMaintenanceLevelID = Convert.ToInt32(dr["MachineMaintenanceLevelID"]),
                                MachineMaintenanceLevel = Convert.ToString(dr["MachineMaintenanceLevel"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataTextField = "MachineMaintenanceLevel";
            ddl.DataValueField = "MachineMaintenanceLevelID";
            ddl.DataSource = MML;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}
