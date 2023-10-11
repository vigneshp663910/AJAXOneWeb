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
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Web;
using System.Web.UI;
using System.Configuration;

namespace Business
{
    public class BDMS_Campign
    {
        private IDataAccess provider;
        public BDMS_Campign()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PDMS_Campign> GetCampign(int? CampignID, string CampignName)
        {
            List<PDMS_Campign> Campign = new List<PDMS_Campign>();
            PDMS_Campign W = null;
            DbParameter CampignIDP = provider.CreateParameter("CampignID", CampignID, DbType.Int32);
            DbParameter CampignNameP = provider.CreateParameter("CampignName", CampignName, DbType.String);

            DbParameter[] Params = new DbParameter[2] { CampignIDP, CampignNameP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetCampign", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            W = new PDMS_Campign();
                            Campign.Add(W);
                            W.CampignID = Convert.ToInt32(dr["CampignID"]);
                            W.CampignName = Convert.ToString(dr["CampignName"]);
                        }
                    }
                }

            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Campign;
        }
        public long InsertCampignTicket(PDMS_CampignTicket  Ticket , int UserID)
        {
            long HeaderID = 0;
            try
            {
               
                Int32 TTicketID = 1;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter CampignIDP = provider.CreateParameter("CampignID", Ticket.Campign.CampignID, DbType.Int32);
                    DbParameter IsCrackedP = provider.CreateParameter("IsCracked", Ticket.IsCracked, DbType.Boolean);
                    DbParameter WeldingDateP = provider.CreateParameter("WeldingDate", Ticket.WeldingDate, DbType.DateTime);
                    DbParameter Remark1P = provider.CreateParameter("Remark1", Ticket.Remark1, DbType.String);
                    DbParameter Remark2P = provider.CreateParameter("Remark2", Ticket.Remark2, DbType.String);
                    DbParameter Remark3P = provider.CreateParameter("Remark3", Ticket.Remark3, DbType.String);
                    DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", Ticket.ICTicket.ICTicketID, DbType.Int64);
                    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                    DbParameter TicketIDParam = provider.CreateParameter("OutValue", TTicketID, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                    DbParameter[] Paramss = new DbParameter[9] { CampignIDP, IsCrackedP, WeldingDateP, Remark1P, Remark2P, Remark3P, ICTicketIDP, UserIDP, TicketIDParam };
                    provider.Insert("ZDMS_InsertCampignTicket", Paramss);
                    HeaderID = Convert.ToInt64(TicketIDParam.Value);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertClaimForMaterial", sqlEx); 
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertClaimForMaterial", ex); 
            }
            return HeaderID;
        }
        public List<PDMS_CampignTicket> GetCampignTicketReport(long? CampignTicketID, string DealerCode, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? StatusID, int? TechnicianID)
        {
            List<PDMS_CampignTicket> Ws = new List<PDMS_CampignTicket>();
            PDMS_CampignTicket W = null;
            try
            {
                DbParameter CampignTicketIDP = provider.CreateParameter("CampignTicketID", CampignTicketID, DbType.Int32);
                DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);              
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);               
                DbParameter ICTicketNumberP  = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);               

                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter StatusIDP = provider.CreateParameter("ServiceStatusID", StatusID, DbType.Int32);
                DbParameter TechnicianIDP = provider.CreateParameter("TechnicianID", TechnicianID, DbType.Int32);

                DbParameter[] Params = new DbParameter[8] {CampignTicketIDP, DealerCodeP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, StatusIDP, TechnicianIDP };


                using (DataSet DataSet = provider.Select("ZDMS_GetCampignTicketReport", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_CampignTicket();
                            Ws.Add(W);
                            W.CampignTicketID = Convert.ToInt64(dr["CampignTicketID"]);
                            W.IsCracked = Convert.ToBoolean(dr["IsCracked"]);
                            W.WeldingDate = dr["WeldingDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["WeldingDate"]);  
                            W.Remark1 = Convert.ToString(dr["Remark1"]);
                            W.Remark2 = Convert.ToString(dr["Remark2"]);
                            W.Remark3 = Convert.ToString(dr["Remark3"]);
                            W.Campign = new PDMS_Campign() { CampignID = Convert.ToInt32(dr["CampignID"]), CampignName = Convert.ToString(dr["CampignName"]) };
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicket.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();

                            W.ICTicket.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.ICTicket.Equipment.EngineModel = Convert.ToString(dr["EngineModel"]);
                            W.ICTicket.Equipment.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                            W.ICTicket.Equipment.CorrectSMR = Convert.ToString(dr["CorrectSMR"]);
                            W.ICTicket.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
                            W.ICTicket.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                            W.ICTicket.Equipment.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);

                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ICTicket.ComplaintCode = Convert.ToString(dr["ComplaintCode"]);
                            W.ICTicket.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                            W.ICTicket.Information = Convert.ToString(dr["Information"]);
                            W.ICTicket.ReasonForCloser = Convert.ToString(dr["ReasonForCloser"]);
                            W.ICTicket.OldICTicketNumber = Convert.ToString(dr["OldICTicketNumber"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ICTicket.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ICTicket.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ICTicket.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.ICTicket.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);

                            W.ICTicket.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ICTicket.ReachedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]); 
                            W.ICTicket.ServiceRecord = Convert.ToString(dr["ServiceRecord"]);
                            W.ICTicket.RegisteredBy = new PUser();
                            if (dr["RegisteredByID"] != DBNull.Value)
                                W.ICTicket.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

                            if (dr["TechnicianID"] != DBNull.Value)
                            {
                                W.ICTicket.Technician = new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
                            }
                            else
                            {
                                W.ICTicket.Technician = new PUser();
                            }

                            // W.Address = new PDMS_Address();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }
       
    }
}
