using DataAccess;
using Properties; 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Business
{
    [Serializable]
    public class PDMS_TypeOfMachine
    {
        public int TypeOfMachineID { get; set; }
        public string TypeOfMachine { get; set; }
        public Boolean IsActive { get; set; }
    }
    [Serializable]
    public class PDMS_AvailabilityOfOtherMachine
    {
        public long AvailabilityOfOtherMachineID { get; set; }
        public long ICTicketID { get; set; }
        public PDMS_TypeOfMachine TypeOfMachine { get; set; }
        public PMake Make { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedOn { get; set; }

    }
    public class BDMS_AvailabilityOfOtherMachine
    {
        private IDataAccess provider;
        public BDMS_AvailabilityOfOtherMachine()
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
        public void GetTypeOfMachine(DropDownList ddl, int? TypeOfMachineID, string TypeOfMachine)
        {
            List<PDMS_TypeOfMachine> MML = new List<PDMS_TypeOfMachine> ();
            try
            {
                DbParameter TypeOfMachineP;
                DbParameter TypeOfMachineIDP = provider.CreateParameter("TypeOfMachineID", TypeOfMachineID, DbType.Int32);
                if (!string.IsNullOrEmpty(TypeOfMachine))
                    TypeOfMachineP = provider.CreateParameter("TypeOfMachine", TypeOfMachine, DbType.String);
                else
                    TypeOfMachineP = provider.CreateParameter("TypeOfMachine", null, DbType.String);

                DbParameter[] Params = new DbParameter[2] { TypeOfMachineIDP, TypeOfMachineP };
                using (DataSet DataSet = provider.Select("ZDMS_GetTypeOfMachine", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PDMS_TypeOfMachine()
                            {
                                TypeOfMachineID = Convert.ToInt32(dr["TypeOfMachineID"]),
                                TypeOfMachine = Convert.ToString(dr["TypeOfMachine"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataTextField = "TypeOfMachine";
            ddl.DataValueField = "TypeOfMachineID";
            ddl.DataSource = MML;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public void GetMake(DropDownList ddl, int? MakeID, string Make)
        {
            List<PMake> MML = new List<PMake>();
            try
            {
                DbParameter MakeP;
                DbParameter MakeIDP = provider.CreateParameter("MakeID", MakeID, DbType.Int32);
                if (!string.IsNullOrEmpty(Make))
                    MakeP = provider.CreateParameter("Make", Make, DbType.String);
                else
                    MakeP = provider.CreateParameter("Make", null, DbType.String);

                DbParameter[] Params = new DbParameter[2] { MakeIDP, MakeP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMake", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PMake()
                            {
                                MakeID = Convert.ToInt32(dr["MakeID"]),
                                Make = Convert.ToString(dr["Make"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            ddl.DataTextField = "Make";
            ddl.DataValueField = "MakeID";
            ddl.DataSource = MML;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public Boolean InsertOrUpdateAvailabilityOfOtherMachineAddOrRemoveICTicket(long? AvailabilityOfOtherMachineID, long ICTicketID, int TypeOfMachineID, int Quantity, int MakeID, Boolean IsDeleted, int UserID)
        {

            int success = 0;

            DbParameter AvailabilityOfOtherMachineIDP = provider.CreateParameter("AvailabilityOfOtherMachineID", AvailabilityOfOtherMachineID, DbType.Int64);
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
            DbParameter TypeOfMachineIDP = provider.CreateParameter("TypeOfMachineID", TypeOfMachineID, DbType.Int32);
            DbParameter QuantityP = provider.CreateParameter("Quantity", Quantity, DbType.Int32);
            DbParameter MakeIDP = provider.CreateParameter("MakeID", MakeID, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter IsDeleteP = provider.CreateParameter("IsDeleted", IsDeleted, DbType.Boolean);
            DbParameter[] Params = new DbParameter[7] { AvailabilityOfOtherMachineIDP, ICTicketIDP, TypeOfMachineIDP, QuantityP, MakeIDP, UserIDP, IsDeleteP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateAvailabilityOfOtherMachineAddOrRemoveICTicket", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_AvailabilityOfOtherMachine", "InsertOrUpdateAvailabilityOfOtherMachineAddOrRemoveICTicket", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_AvailabilityOfOtherMachine", " InsertOrUpdateAvailabilityOfOtherMachineAddOrRemoveICTicket", ex);
                return false;
            }
            return true;
        }
        public List<PDMS_AvailabilityOfOtherMachine> GetAvailabilityOfOtherMachine(long? ICTicketID, long? AvailabilityOfOtherMachineID, int? TypeOfMachineID, int? MakeID)
        {
            List<PDMS_AvailabilityOfOtherMachine> ServiceMaterials = new List<PDMS_AvailabilityOfOtherMachine>();
            try
            {


                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter AvailabilityOfOtherMachineIDP = provider.CreateParameter("AvailabilityOfOtherMachineID", AvailabilityOfOtherMachineID, DbType.Int64);
                DbParameter TypeOfMachineIDP = provider.CreateParameter("TypeOfMachineID", TypeOfMachineID, DbType.Int64);
                DbParameter MakeIDP = provider.CreateParameter("MakeID", MakeID, DbType.Int64);
                DbParameter[] Params = new DbParameter[4] { ICTicketIDP, AvailabilityOfOtherMachineIDP, TypeOfMachineIDP, MakeIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetAvailabilityOfOtherMachine", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServiceMaterials.Add(new PDMS_AvailabilityOfOtherMachine()
                            {
                                AvailabilityOfOtherMachineID = Convert.ToInt64(dr["AvailabilityOfOtherMachineID"]),
                                ICTicketID = Convert.ToInt64(dr["ICTicketID"]),
                                Quantity = Convert.ToInt32(dr["Quantity"]),
                                TypeOfMachine = new PDMS_TypeOfMachine()
                                {
                                    TypeOfMachineID = Convert.ToInt32(dr["TypeOfMachineID"]),
                                    TypeOfMachine = Convert.ToString(dr["TypeOfMachine"])
                                },
                                Make = new PMake()
                                {
                                    MakeID = Convert.ToInt32(dr["MakeID"]),
                                    Make = Convert.ToString(dr["Make"])
                                },
                                CreatedOn = Convert.ToDateTime(dr["CreatedOn"])

                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return ServiceMaterials;
        } 
    }
}
