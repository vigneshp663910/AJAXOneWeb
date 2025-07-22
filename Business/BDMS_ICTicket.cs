using DataAccess;
using Newtonsoft.Json;
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


namespace Business
{
    public class BDMS_ICTicket
    {
         private IDataAccess provider;
        public BDMS_ICTicket()
        {
            try
            {
                 provider = new ProviderFactory().GetProvider();
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessageService("BDMS_ICTicket", "provider : " + e1.Message, null);
            }
        }
        //public List<PDMS_ICTicket> GetICTicketManage(long? DealerID, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? StatusID, int? TechnicianID, int? ServiceTypeID,string Division)
        //{
        //    string endPoint = "Service/ICTicketManage?DealerID=" + DealerID + "&CustomerCode=" + CustomerCode + "&ICTicketNumber=" + ICTicketNumber + "&ICTicketDateF="
        //       + ICTicketDateF + "&ICTicketDateT=" + ICTicketDateT + "&StatusID=" + StatusID + "&TechnicianID=" + TechnicianID + "&ServiceTypeID=" + ServiceTypeID + "&Division=" + Division;
        //    return JsonConvert.DeserializeObject<List<PDMS_ICTicket>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        //}
        public PApiResult GetICTicketManage(long? DealerID, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? StatusID, int? TechnicianID, int? ServiceTypeID, string Division,int Excel, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "Service/ICTicketManage?DealerID=" + DealerID + "&CustomerCode=" + CustomerCode + "&ICTicketNumber=" + ICTicketNumber + "&ICTicketDateF="
               + ICTicketDateF + "&ICTicketDateT=" + ICTicketDateT + "&StatusID=" + StatusID + "&TechnicianID=" + TechnicianID + "&ServiceTypeID=" + ServiceTypeID + "&Division=" + Division + "&Excel=" + Excel + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));

        }

        public List<PDMS_ICTicket> GetICTicket(int? DealerID, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? StatusID, int? TechnicianID)
        {

            string endPoint = "Service/ICTicketManage?DealerID=" + DealerID + "&CustomerCode=" + CustomerCode + "&ICTicketNumber=" + ICTicketNumber + "&ICTicketDateF="
                + ICTicketDateF + "&ICTicketDateT=" + ICTicketDateT + "&StatusID=" + StatusID + "&TechnicianID=" + TechnicianID;
            return JsonConvert.DeserializeObject<List<PDMS_ICTicket>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
             
        }

        public Boolean UpdateICTicketWarrantyDistribution(long ICTicketID, decimal CustomerPayPercentage, decimal DealerPayPercentage, decimal AEPayPercentage)
        { 
            string endPoint = "ICTicket/UpdateICTicketWarrantyDistribution?ICTicketID=" + ICTicketID + "&CustomerPayPercentage=" + CustomerPayPercentage
                + "&DealerPayPercentage=" + DealerPayPercentage + "&AEPayPercentage=" + AEPayPercentage;
            return JsonConvert.DeserializeObject<Boolean>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data)); 
        }
       
         public PDMS_ICTicket GetICTicketByICTIcketID(long ICTicketID)
        {
            string endPoint = "Service/ICTicketByID?ICTicketID=" + ICTicketID;
            return JsonConvert.DeserializeObject<PDMS_ICTicket>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data)); 
        }
        public PDMS_ICTicket GetICTicketByICTIcketID_All(long ICTicketID)
        {
            string endPoint = "ICTicket/ICTicketByID_All?ICTicketID=" + ICTicketID;
            return JsonConvert.DeserializeObject<PDMS_ICTicket>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
       
        

        //public Boolean UpdateICTicketWarrantyDistribution(long ICTicket, decimal? CustomerPayPercentage, decimal? DealerPayPercentage, decimal? AEPayPercentage)
        //{
        //    DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicket, DbType.Int64);

        //    DbParameter CustomerPayPercentageP = provider.CreateParameter("CustomerPayPercentage", CustomerPayPercentage, DbType.Decimal);
        //    DbParameter DealerPayPercentageP = provider.CreateParameter("DealerPayPercentage", DealerPayPercentage, DbType.Decimal);
        //    DbParameter AEPayPercentageP = provider.CreateParameter("AEPayPercentage", AEPayPercentage, DbType.Decimal);

        //    //  DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
        //    DbParameter[] Params = new DbParameter[4] { ICTicketP, CustomerPayPercentageP, DealerPayPercentageP, AEPayPercentageP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            provider.Insert("ZDMS_UpdateICTicketWarrantyDistribution", Params);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BDMS_ICTicket", "UpdateICTicketWarrantyDistribution", sqlEx);
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BDMS_ICTicket", " UpdateICTicketWarrantyDistribution", ex);
        //        return false;
        //    }
        //    return true;
        //}

        

        public string dssor_sales_order_hdr = " insert into dssor_sales_order_hdr ( s_establishment,p_so_id,s_tenant_id,f_customer_id,f_location,f_currency,f_bill_to,s_modified_by "
                + " ,r_insurance_p,r_discount_amt_additional,s_status,r_tax_amt,s_created_on,r_net_amt,f_ship_to"
                + " ,r_contact_no,r_gross_amt,r_contact_prsn,r_discount_amt,s_created_by,s_modified_on,f_office,f_order_type,s_object_type,r_remarks,r_exp_del_date"
                + " ,r_frieght_p,r_order_date,channel,f_division,r_auto,r_ref_obj_name,r_ref_obj_type,r_price_grp,r_model,r_model_no,s_last_request_index,r_insurance_amt,r_packing_chrgs,r_freight_amt,r_our_ref_id ,r_ref_date) values ";       

        public string dssor_sales_order_item = " insert into dssor_sales_order_item (s_establishment,p_so_item, p_so_id,s_tenant_id,f_location,s_modified_by,f_uom,r_tax_amt,f_division"
                + " ,s_status,f_office,r_exp_del_date,f_material_id,s_last_request_index,r_order_qty,r_add_discount_amt"
                + " ,s_created_on,r_net_amt,d_material_desc,r_resvered_qty,r_gross_amt,r_cancel_qty ,r_shiped_qty,r_discount_amt,s_created_by,r_unit_price,r_pending_qty,s_modified_on"
                + " ,s_object_type,r_approved_qty,s_channel) values ";

        public string dssor_sales_order_cond = "insert into dssor_sales_order_cond (s_establishment,p_so_item,p_so_id,s_tenant_id,p_condition_type,f_currency,"
                + " r_cond_amt,r_order_qty,r_pric_date,s_created_by,s_created_on,d_cond_desc,r_cond_cls,f_percentage,channel) values ";

       public Boolean UpdateICTicketDecline(long ICTicket, string Reason, int UserID)
        {

            int success = 0;


            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicket, DbType.Int64);
            DbParameter ReasonP = provider.CreateParameter("Reason", Reason, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            //  DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[3] { ICTicketP, ReasonP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_UpdateICTicketDecline", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "UpdateICTicketDecline", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " UpdateICTicketDecline", ex);
                return false;
            }
            return true;
        }

       
        public List<PAttachedFile> GetICTicketAttachedFile(long? ICTicketID, long? AttachedFileID)
        {
            List<PAttachedFile> D8 = new List<PAttachedFile>();
            try
            {
                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                DbParameter AttachedFileIDP = provider.CreateParameter("AttachedFileID", AttachedFileID, DbType.Int64);
                DbParameter[] Params = new DbParameter[2] { ICTicketIDP, AttachedFileIDP };
                using (DataSet DS = provider.Select("ZDMS_GetICTicketAttachedFile", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            D8.Add(new PAttachedFile()
                            {
                                AttachedFileID = Convert.ToInt64(dr["AttachedFileID"]),
                                AttachedFile = (Byte[])(dr["AttachedFile"]),
                                FileType = Convert.ToString(dr["ContentType"]),
                                FileName = Convert.ToString(dr["FileName"]),
                                FileSize = Convert.ToInt32(dr["FileSize"])
                            });
                        }
                    }
                }
                return D8;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "GetICTicketAttachedFile", ex);
                return null;
            }
        }      
        public Boolean UpdateICTicketMarginWarranty(long ICTicketID, Boolean MarginWarranty, string MarginRemark, int UserID)
        {
            int success = 0;
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
            DbParameter MarginWarrantyP = provider.CreateParameter("MarginWarranty", MarginWarranty, DbType.Boolean);
            DbParameter MarginRemarkP = provider.CreateParameter("MarginRemark", MarginRemark, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { ICTicketIDP, MarginWarrantyP, MarginRemarkP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_UpdateICTicketMarginWarranty", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "UpdateICTicketMarginWarranty", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " UpdateICTicketMarginWarranty", ex);
                return false;
            }
            return true;
        }
        public Boolean ApproveOrDeclineICTicketReqDecline(long ICTicketID, Boolean Approve)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                    DbParameter ApproveP = provider.CreateParameter("Approve", Approve, DbType.Boolean);
                    DbParameter[] Paramss = new DbParameter[2] { ICTicketIDP, ApproveP };
                    provider.Insert("ZDMS_ApproveOrDeclineICTicketReqDecline", Paramss);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "ApproveOrDeclineICTicketReqDecline", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " ApproveOrDeclineICTicketReqDecline", ex);
                return false;
            }
            return true;
        }
         
        
        public Boolean ChangeICTicketRequestedDate(long ICTicketID, DateTime RequestedDate, int UserID)
        {
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
            DbParameter RequestedDateP = provider.CreateParameter("RequestedDate", RequestedDate, DbType.DateTime);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

            DbParameter[] Params = new DbParameter[3] { ICTicketIDP, RequestedDateP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_ChangeICTicketRequestedDate", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "ChangeICTicketRequestedDate", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " ChangeICTicketRequestedDate", ex);
                return false;
            }
            return true;
        }
        
        public string dppor_purc_order_hdr = "insert into dppor_purc_order_hdr ( s_establishment, s_tenant_id, p_po_id, f_location, f_currency, f_bill_to, s_modified_by, r_insurance_p, r_tax_amt, s_created_on, f_sold_to, s_status, r_net_amt, r_req_del_date, f_ship_to, r_description, r_contact_no, r_gross_amt, r_contact_prsn, f_so_id, r_discount_amt, s_created_by, s_modified_on, f_order_type, f_office, s_object_type, r_exp_del_date, r_remarks, r_frieght_p, f_doc_flow_id, r_order_date, s_sync_status, s_action, channel, r_discount_amt_additional, f_division, is_ack, r_auto, r_ext_id, f_vendor_id, s_last_request_id, s_last_request_index, s_channel, s_status_custom ) values  ";
        public string dppor_purc_order_item = "insert into  dppor_purc_order_item (s_establishment, k_po_id, p_po_item, s_tenant_id, f_so_item, f_oem_id, f_location, f_material_id, r_order_qty, s_modified_by, r_item_type, r_tax_amt, f_uom, s_created_on, r_indicator, s_status, r_net_amt, r_doc_flow_id, d_material_desc, r_resvered_qty, r_gross_amt, r_hgl_item, f_so_id, r_cancel_qty, r_shiped_qty, r_discount_amt, s_created_by, r_unit_price, r_pending_qty, s_modified_on, f_office, s_object_type, r_exp_del_date, r_approved_qty, r_add_discount_amt, s_sync_status, s_action, channel, f_division, is_ack, r_ref_obj_type, r_ref_obj_name, s_last_request_id, s_last_request_index, s_channel, s_status_custom, r_delv_qty, r_gr_qty) values ";
       
       
      

        //public Boolean InsertDeviatedICTicketCommissioningRequestForApproval(long ICTicketID, int UserID)
        //{
        //    DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
        //    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //    DbParameter[] Params = new DbParameter[2] { ICTicketIDP, UserIDP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            provider.Insert("ZDMS_InsertDeviatedICTicketCommissioningRequestForApproval", Params);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BDMS_ICTicket", "InsertDeviatedICTicketCommissioningRequestForApproval", sqlEx);
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BDMS_ICTicket", " InsertDeviatedICTicketCommissioningRequestForApproval", ex);
        //        return false;
        //    }
        //    return true;
        //}
        //public Boolean ApproveOrRejectDeviatedICTicketCommissioningRequest(long ICTicketID, Boolean? IsApproved, Boolean? IsRejected, int UserID)
        //{
        //    DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
        //    DbParameter IsApprovedP = provider.CreateParameter("IsApproved", IsApproved, DbType.Int32);
        //    DbParameter IsRejectedP = provider.CreateParameter("IsRejected", IsRejected, DbType.Int32);
        //    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //    DbParameter[] Params = new DbParameter[4] { ICTicketIDP, IsApprovedP, IsRejectedP, UserIDP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            provider.Insert("ZDMS_ApproveOrRejectDeviatedICTicketCommissioningRequest", Params);
        //            scope.Complete();
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BDMS_ICTicket", "InsertDeviatedICTicketCommissioningRequestForApproval", sqlEx);
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BDMS_ICTicket", " InsertDeviatedICTicketCommissioningRequestForApproval", ex);
        //        return false;
        //    }
        //    return true;
        //}
        //public List<PDMS_ICTicket> GetDeviatedICTicketCommissioningForApproval(int? DealerID, string ICTicketNumber, DateTime? RequestedDateF, DateTime? RequestedDateT)
        //{
        //    List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
        //    PDMS_ICTicket W = null;
        //    try
        //    {
        //        DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
        //        DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
        //        DbParameter RequestedDateFP = provider.CreateParameter("RequestedDateF", RequestedDateF, DbType.DateTime);
        //        DbParameter RequestedDateTP = provider.CreateParameter("RequestedDateT", RequestedDateT, DbType.DateTime);

        //        DbParameter[] Params = new DbParameter[4] { DealerIDP, ICTicketNumberP, RequestedDateFP, RequestedDateTP };
        //        using (DataSet DataSet = provider.Select("ZDMS_GetDeviatedICTicketCommissioningForApproval", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    W = new PDMS_ICTicket();
        //                    Ws.Add(W);
        //                    W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
        //                    W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
        //                    W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
        //                    W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
        //                    W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
        //                    W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);

        //                    W.Equipment = new PDMS_EquipmentHeader();
        //                    W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };


        //                    if (dr["ServiceTypeID"] != DBNull.Value)
        //                    {
        //                        W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
        //                    }
        //                    if (dr["ServicePriorityID"] != DBNull.Value)
        //                    {
        //                        W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
        //                    }

        //                    W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

        //                    if (dr["ServiceStatusID"] != DBNull.Value)
        //                    {
        //                        W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
        //                    }
        //                    W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return Ws;
        //}
        //public DataTable GetDeviatedICTicketCommissioningReport(int? DealerID, string ICTicketNumber, DateTime? RequestedDateF, DateTime? RequestedDateT, int UserID)
        //{

        //    try
        //    {
        //        DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
        //        DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
        //        DbParameter RequestedDateFP = provider.CreateParameter("RequestedDateF", RequestedDateF, DbType.DateTime);
        //        DbParameter RequestedDateTP = provider.CreateParameter("RequestedDateT", RequestedDateT, DbType.DateTime);
        //        DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //        DbParameter[] Params = new DbParameter[5] { DealerIDP, ICTicketNumberP, RequestedDateFP, RequestedDateTP, UserIDP };
        //        using (DataSet DataSet = provider.Select("ZDMS_GetDeviatedICTicketCommissioningReport", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                return DataSet.Tables[0];
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return null;
        //}

      
        public DataTable GetICTicketCommissionMailTo(int? DealerID, DateTime? ICTicketDateF, DateTime? ICTicketDateT, string ICTicketNumber, String MachineSerialNumber)
        {
            DataTable dt = new DataTable();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                 
                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
                DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", string.IsNullOrEmpty(MachineSerialNumber) ? null : MachineSerialNumber, DbType.String);

                DbParameter[] Params = new DbParameter[5] { DealerIDP,  ICTicketDateFP, ICTicketDateTP, ICTicketNumberP, MachineSerialNumberP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketCommissionMailTo", Params))
                {
                    if (DataSet != null)
                    {
                        dt = DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return dt;
        }

        public PICTicketCustomerFeedback GetICTicketCustomerFeedback(long ICTicketID)
        {
            string endPoint = "ICTicket/CustomerFeedback?ICTicketID=" + ICTicketID;
            return JsonConvert.DeserializeObject<PICTicketCustomerFeedback>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataTable GetMarginWarrantyChange(int? DealerID, DateTime? MarginWarrantyChangeRequestedFrom, DateTime? MarginWarrantyChangeRequestedTo, string ICTicketNumber, bool? IsApproved, int UserID, int? PageIndex, int? PageSize, out int RowCount)
        {
            TraceLogger.Log(DateTime.Now);
            RowCount = 0;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter MarginWarrantyChangeRequestedFromP = provider.CreateParameter("MarginWarrantyChangeRequestedFrom", MarginWarrantyChangeRequestedFrom, DbType.DateTime);
                DbParameter MarginWarrantyChangeRequestedToP = provider.CreateParameter("MarginWarrantyChangeRequestedTo", MarginWarrantyChangeRequestedTo, DbType.DateTime);

                DbParameter ICTicketNumberP;
                if (!string.IsNullOrEmpty(ICTicketNumber))
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", ICTicketNumber, DbType.String);
                else
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", null, DbType.String);
                DbParameter IsApprovedP = provider.CreateParameter("IsApproved", IsApproved, DbType.Boolean);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);

                DbParameter[] Params = new DbParameter[8] { DealerIDP, MarginWarrantyChangeRequestedFromP, MarginWarrantyChangeRequestedToP, ICTicketNumberP, IsApprovedP, UserIDP, PageIndexP, PageSizeP };


                using (DataSet DataSet = provider.Select("GetMarginWarrantyChange", Params))
                {
                    if (DataSet != null)
                    {
                        if (DataSet.Tables[0].Rows.Count > 0)
                        {
                            RowCount = Convert.ToInt32(DataSet.Tables[0].Rows[0]["RowCount"]);
                        }
                        return DataSet.Tables[0];
                    }
                }
                return null;
                TraceLogger.Log(DateTime.Now);
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public DataTable GetMarginWarrantyChangeForApproval(int? DealerID, DateTime? MarginWarrantyChangeRequestedFrom, DateTime? MarginWarrantyChangeRequestedTo, string ICTicketNumber, int UserID, int? PageIndex, int? PageSize, out int RowCount)
        {
            TraceLogger.Log(DateTime.Now);
            RowCount = 0;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter MarginWarrantyChangeRequestedFromP = provider.CreateParameter("MarginWarrantyChangeRequestedFrom", MarginWarrantyChangeRequestedFrom, DbType.DateTime);
                DbParameter MarginWarrantyChangeRequestedToP = provider.CreateParameter("MarginWarrantyChangeRequestedTo", MarginWarrantyChangeRequestedTo, DbType.DateTime);

                DbParameter ICTicketNumberP;
                if (!string.IsNullOrEmpty(ICTicketNumber))
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", ICTicketNumber, DbType.String);
                else
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", null, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);

                DbParameter[] Params = new DbParameter[7] { DealerIDP, MarginWarrantyChangeRequestedFromP, MarginWarrantyChangeRequestedToP, ICTicketNumberP, UserIDP, PageIndexP, PageSizeP };


                using (DataSet DataSet = provider.Select("GetMarginWarrantyChangeForApproval", Params))
                {
                    if (DataSet != null)
                    {
                        if (DataSet.Tables[0].Rows.Count > 0)
                        {
                            RowCount = Convert.ToInt32(DataSet.Tables[0].Rows[0]["RowCount"]);
                        }
                        return DataSet.Tables[0];
                    }
                }
                return null;
                TraceLogger.Log(DateTime.Now);
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public Boolean ApproveOrRejectMarginWarrantyChange(  long ICTicketID, int ApprovedBy, Boolean IsApproved,string ApproverRemarks)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                { 
                    DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                    DbParameter IsApprovedP = provider.CreateParameter("IsApproved", IsApproved, DbType.Boolean);
                    DbParameter ApprovedByP = provider.CreateParameter("ApprovedBy", ApprovedBy, DbType.Int32);
                    DbParameter ApproverRemarksP = provider.CreateParameter("ApproverRemarks", ApproverRemarks, DbType.String);
                    DbParameter[] Paramss = new DbParameter[4] { ICTicketIDP, IsApprovedP, ApprovedByP, ApproverRemarksP };
                    provider.Insert("ApproveOrRejectMarginWarrantyChange", Paramss);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "ApproveOrRejectMarginWarrantyChange", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " ApproveOrRejectMarginWarrantyChange", ex);
                return false;
            }
            return true;
        }


        public PApiResult InsertICTicketDepartureDate(long ICTicketID, decimal Latitude, decimal Longitude)
        {
            string endPoint = "ICTicket/InsertICTicketDepartureDate?ICTicketID=" + ICTicketID + "&Latitude=" + Latitude + "&Longitude=" + Longitude;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult InsertICTicketSeReached(long ICTicketID, string Location, int? HMRValue, string SiteContactPersonName, string SiteContactPersonNumber, int? DesignationID, decimal Latitude, decimal Longitude)
        {
            string endPoint = "ICTicket/InsertICTicketSeReached?ICTicketID=" + ICTicketID + "&Location=" + Location+ "&HMRValue=" + HMRValue 
                + "&SiteContactPersonName=" + SiteContactPersonName + "&SiteContactPersonNumber=" + SiteContactPersonNumber + "&DesignationID=" + DesignationID + "&Latitude=" + Latitude + "&Longitude=" + Longitude;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult InsertICTicketArrivalBackDate(long ICTicketID, decimal Latitude, decimal Longitude)
        {
            string endPoint = "ICTicket/InsertICTicketArrivalBackDate?ICTicketID=" + ICTicketID + "&Latitude=" + Latitude + "&Longitude=" + Longitude;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public List<PDMS_ICTicket> GetICTicketFirstTimeRightForWarrantyService(int? DealerID, DateTime? DateFrom, DateTime? DateTo, int? UserID)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateFrom, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateTo, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketFirstTimeRightForWarrantyService", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.Equipment = new PDMS_EquipmentHeader();

                            W.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                            W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };
                            W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.Equipment.EngineModel = Convert.ToString(dr["EngineModel"]);
                            W.Equipment.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                            W.Equipment.CorrectSMR = Convert.ToString(dr["CorrectSMR"]);
                            W.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
                            W.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                            W.Equipment.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);

                            W.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ComplaintCode = Convert.ToString(dr["ComplaintCode"]);
                            W.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                            W.Information = Convert.ToString(dr["Information"]);
                            W.ReasonForCloser = Convert.ToString(dr["ReasonForCloser"]);
                            W.OldICTicketNumber = Convert.ToString(dr["OldICTicketNumber"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);

                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ReachedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]); 
                            W.ServiceRecord = Convert.ToString(dr["ServiceRecord"]);
                            W.RegisteredBy = new PUser();
                            if (dr["RegisteredByID"] != DBNull.Value)
                                W.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

                            if (dr["TechnicianID"] != DBNull.Value)
                            {
                                W.Technician = new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
                            }
                            else
                            {
                                W.Technician = new PUser();
                            }
                            W.LastICTicket = new PDMS_ICTicket()
                            {
                                ICTicketNumber = Convert.ToString(dr["LastICTicketNumber"]),
                                Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["LastDealerCode"]) },
                                Technician = new PUser() { ContactName = Convert.ToString(dr["LastTechnicianName"]) }
                            };
                            if (dr["LastICTicketDate"] != DBNull.Value)
                            {
                                W.LastICTicket.ICTicketDate = Convert.ToDateTime(dr["LastICTicketDate"]);
                            } 
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

        public List<PDMS_ICTicket> GetICTicketByEquipmentSerialNo(long EquipmentHeaderID)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {      string Q = "select I.ICTicketID ,ICTicketNumber ,ICTicketDate ,DealerCode ,D.DisplayName,ServiceType"
                    + " ,ServiceStatus ,IsWarranty ,IsMarginWarranty ,RequestedDate ,ReachedDate ,RestoreDate,I.CurrentHMRDate,I.CurrentHMRValue"
                    + " from ZDMS_TICTicket I inner join MDealer D on D.DID = I.DealerID" 
     +" left join ZDMS_MServiceType ST on ST.ServiceTypeID = I.ServiceTypeID" 
     + " left join ZDMS_MServiceStatus SS on SS.ServiceStatusID = I.ServiceStatusID" 
     + " where EquipmentHeaderID  ="+ EquipmentHeaderID + " order by I.ICTicketID desc ";

                using (DataSet DataSet = provider.SelectUsingQuery(Q))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer()
                            {
                                DealerCode = Convert.ToString(dr["DealerCode"]),
                                DealerName = Convert.ToString(dr["DisplayName"])
                            }; 
                            W.ServiceType = new PDMS_ServiceType() { ServiceType = Convert.ToString(dr["ServiceType"]) };
                            W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) }; 
                            W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]); 
                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ReachedDate = dr["ReachedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ReachedDate"]);
                            W.RestoreDate = dr["RestoreDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                            W.CurrentHMRDate = dr["CurrentHMRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CurrentHMRDate"]);
                            W.CurrentHMRValue = dr["CurrentHMRValue"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CurrentHMRValue"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "GetICTicketByEquipmentSerialNo_Table", sqlEx);
                throw;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "GetICTicketByEquipmentSerialNo_Table", ex);
                throw;
            }
            return Ws;
        }
        public PApiResult GetOnlineServiceTicket(long? OnlineServiceTicketID, string CustomerCode, string ICTicketNumber, string DateF, string DateT, int? StatusID, string Division, long? EquipmentHeaderID, int Excel, int? PageIndex = null, int? PageSize = null)
        {
            string endPoint = "ICTicket/GetOnlineServiceTicket?OnlineServiceTicketID=" + OnlineServiceTicketID + "&CustomerCode=" + CustomerCode
                + "&ICTicketNumber=" + ICTicketNumber + "&DateF=" + DateF + "&DateT=" + DateT + "&StatusID=" + StatusID
                + "&Division=" + Division + "&EquipmentHeaderID=" + EquipmentHeaderID + "&Excel=" + Excel + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult UpdateOnlineServiceTicketStatus(long OnlineServiceTicketID, int StatusID, string Remarks, string EmployeeUserID)
        {
            string endPoint = "ICTicket/UpdateOnlineServiceTicketStatus?OnlineServiceTicketID=" + OnlineServiceTicketID + "&StatusID="
                + StatusID + "&Remarks=" + Remarks + "&EmployeeUserID=" + EmployeeUserID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)); 
        }
        public PApiResult UpdateOnlineServiceTickeCustomerSatisfactionLevel(long OnlineServiceTicketID, string SatisfactionLevelID)
        {
            string endPoint = "ICTicket/UpdateOnlineServiceTickeCustomerSatisfactionLevel?OnlineServiceTicketID=" + OnlineServiceTicketID + "&SatisfactionLevelID="
                + SatisfactionLevelID;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public PApiResult GetUserForOnlineServiceTicketSupport()
        {
            string endPoint = "ICTicket/GetUserForOnlineServiceTicketSupport";
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }




        public List<PDMS_ICTicket> GetICTicketForDeclinedApproval(string DealerCode, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter DealerCodeP;
                if (!string.IsNullOrEmpty(DealerCode))
                    DealerCodeP = provider.CreateParameter("DealerCode", DealerCode, DbType.String);
                else
                    DealerCodeP = provider.CreateParameter("DealerCode", null, DbType.String);
                DbParameter CustomerCodeP;
                if (!string.IsNullOrEmpty(CustomerCode))
                    CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                else
                    CustomerCodeP = provider.CreateParameter("CustomerCode", null, DbType.String);
                DbParameter ICTicketNumberP;
                if (!string.IsNullOrEmpty(ICTicketNumber))
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", ICTicketNumber, DbType.String);
                else
                    ICTicketNumberP = provider.CreateParameter("ICTicketNumber", null, DbType.String);

                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);

                DbParameter[] Params = new DbParameter[5] { DealerCodeP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketForDeclinedApproval", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.Equipment = new PDMS_EquipmentHeader();

                            W.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                            W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };
                            W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.Equipment.EngineModel = Convert.ToString(dr["EngineModel"]);
                            W.Equipment.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                            W.Equipment.CorrectSMR = Convert.ToString(dr["CorrectSMR"]);
                            W.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
                            W.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                            W.Equipment.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);

                            W.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ComplaintCode = Convert.ToString(dr["ComplaintCode"]);
                            W.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                            W.Information = Convert.ToString(dr["Information"]);
                            W.ReasonForCloser = Convert.ToString(dr["ReasonForCloser"]);
                            W.OldICTicketNumber = Convert.ToString(dr["OldICTicketNumber"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);

                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ReachedDate = dr["ReachedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ReachedDate"]);

                            W.ServiceRecord = Convert.ToString(dr["ServiceRecord"]);
                            W.RegisteredBy = new PUser();
                            if (dr["RegisteredByID"] != DBNull.Value)
                                W.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

                            if (dr["TechnicianID"] != DBNull.Value)
                            {
                                W.Technician = new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
                            }
                            else
                            {
                                W.Technician = new PUser();
                            }
                            W.ReqDeclinedReason = Convert.ToString(dr["ReqDeclinedReason"]);
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
        public List<PDMS_ICTicket> GetICTicketByEquipmentSerialNo(string EquipmentSerialNo)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter EquipmentSerialNoP = provider.CreateParameter("EquipmentSerialNo", string.IsNullOrEmpty(EquipmentSerialNo) ? null : EquipmentSerialNo, DbType.String);
                DbParameter[] Params = new DbParameter[1] { EquipmentSerialNoP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketByEquipmentSerialNo", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.Equipment = new PDMS_EquipmentHeader();

                            //  W.Equipment.EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]);
                            W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };
                            W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            //    W.Equipment.EngineModel = Convert.ToString(dr["EngineModel"]);
                            //    W.Equipment.EngineSerialNo = Convert.ToString(dr["EngineSerialNo"]);
                            //    W.Equipment.CorrectSMR = Convert.ToString(dr["CorrectSMR"]);
                            W.Equipment.DispatchedOn = dr["DispatchedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DispatchedOn"]);
                            W.Equipment.CommissioningOn = dr["CommissioningOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CommissioningOn"]);
                            W.Equipment.WarrantyExpiryDate = Convert.ToDateTime(dr["WarrantyExpiryDate"]);

                            W.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ComplaintCode = Convert.ToString(dr["ComplaintCode"]);
                            W.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                            W.Information = Convert.ToString(dr["Information"]);
                            W.ReasonForCloser = Convert.ToString(dr["ReasonForCloser"]);
                            W.OldICTicketNumber = Convert.ToString(dr["OldICTicketNumber"]);
                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            W.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);

                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            //W.ReachedDate = dr["ReachedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ReachedDate"]); 
                            W.ServiceRecord = Convert.ToString(dr["ServiceRecord"]);
                            W.RegisteredBy = new PUser();
                            if (dr["RegisteredByID"] != DBNull.Value)
                                W.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

                            if (dr["TechnicianID"] != DBNull.Value)
                            {
                                W.Technician = new PUser() { UserID = Convert.ToInt32(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
                            }
                            else
                            {
                                W.Technician = new PUser();
                            }

                            //  W.ServiceMaterial.Date = Convert.ToString(dr["InvoiceNumber"]);
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

        public Boolean InsertOrUpdateTechnicianWorkedDateAddOrRemoveICTicket(long ServiceTechnicianWorkDateID, long? ICTicket, int? TechnicianID, DateTime? WorkedDay, decimal WorkedHours, Boolean IsDeleted, int UserID)
        {
            int success = 0;
            DbParameter ServiceTechnicianWorkDateIDP = provider.CreateParameter("ServiceTechnicianWorkDateID", ServiceTechnicianWorkDateID, DbType.Int64);
            DbParameter ICTicketP = provider.CreateParameter("ICTicket", ICTicket, DbType.Int64);
            DbParameter TechnicianIDP = provider.CreateParameter("TechnicianID", TechnicianID, DbType.Int32);
            DbParameter WorkedDayP = provider.CreateParameter("WorkedDay", WorkedDay, DbType.DateTime);
            DbParameter WorkedHoursP = provider.CreateParameter("WorkedHours", WorkedHours, DbType.Decimal);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter IsDeleteP = provider.CreateParameter("IsDeleted", IsDeleted, DbType.Boolean);
            DbParameter[] Params = new DbParameter[7] { ServiceTechnicianWorkDateIDP, ICTicketP, TechnicianIDP, WorkedDayP, WorkedHoursP, UserIDP, IsDeleteP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateTechnicianWorkedDateForICTicket", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertOrUpdateTechnicianAddOrRemoveICTicket", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertOrUpdateTechnicianAddOrRemoveICTicket", ex);
                return false;
            }
            return true;
        }
        public List<PDMS_ICTicket> GetICTicketStatusReport(int? DealerID, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? StatusID, string MachineSerialNumber)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);

                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter StatusIDP = provider.CreateParameter("ServiceStatusID", StatusID, DbType.Int32);
                DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", string.IsNullOrEmpty(MachineSerialNumber) ? null : MachineSerialNumber, DbType.String);
                DbParameter[] Params = new DbParameter[7] { DealerIDP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, StatusIDP, MachineSerialNumberP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketStatusReport", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };

                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ServiceType = new PDMS_ServiceType() { ServiceType = Convert.ToString(dr["ServiceType"]) };
                            W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ReqDeclinedReason = Convert.ToString(dr["DeclineReason"]);
                            W.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);

                            W.Equipment = new PDMS_EquipmentHeader();
                            W.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };
                            W.NoClaim = dr["NoClaim"] == DBNull.Value ? false : Convert.ToBoolean(dr["NoClaim"]);
                            W.NoClaimReason = Convert.ToString(dr["NoClaimReason"]);

                            W.Invoice = dr["InvoiceNumber"] == DBNull.Value ? null : new PDMS_PaidServiceInvoice() { InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]), InvoiceDate = Convert.ToDateTime(dr["ICTicketDate"]) };


                            W.Claim = new PDMS_WarrantyInvoiceHeader();
                            if (dr["ClaimNumber"] != DBNull.Value)
                            {
                                W.Claim.InvoiceNumber = Convert.ToString(dr["ClaimNumber"]);
                                W.Claim.InvoiceDate = Convert.ToDateTime(dr["ClaimDate"]);
                                W.Claim.ClaimStatus = Convert.ToString(dr["ClaimStatus"]);
                                if (Convert.ToInt32(dr["ClaimStatusID"]) == 1 || Convert.ToInt32(dr["ClaimStatusID"]) == 3 || Convert.ToInt32(dr["ClaimStatusID"]) == 11)
                                {
                                    W.Claim.DaysSinceClaimCreation = ((DateTime.Now - Convert.ToDateTime(dr["ClaimDate"])).Days);
                                }
                                else
                                {
                                    W.Claim.DaysSinceClaimCreation = 0;
                                }
                            }
                            else
                            {
                                W.Claim.DaysSinceClaimCreation = 0;
                            }

                            int ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]);
                            if (ServiceTypeID == 1 || ServiceTypeID == 6)
                            {
                                W.DayLeftForClaimCreation = 0;
                            }
                            else if ((dr["NoClaim"] == DBNull.Value ? false : Convert.ToBoolean(dr["NoClaim"])) == true)
                            {
                                W.DayLeftForClaimCreation = 0;
                            }
                            else
                            {
                                W.DayLeftForClaimCreation = dr["ClaimNumber"] == DBNull.Value ? 15 - ((DateTime.Now - W.ICTicketDate).Days) : (int?)null;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertDeviatedICTicketRequestForApproval", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertDeviatedICTicketRequestForApproval", ex);
            }
            return Ws;
        }
        public DataTable GetICTicketStatusReportForIC(long? EquipmentHeaderID, int? DealerID, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? StatusID, string MachineSerialNumber)
        {

            try
            {
                DbParameter EquipmentHeaderIDP = provider.CreateParameter("EquipmentHeaderID", EquipmentHeaderID, DbType.Int64);
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);

                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter StatusIDP = provider.CreateParameter("ServiceStatusID", StatusID, DbType.Int32);
                DbParameter MachineSerialNumberP = provider.CreateParameter("MachineSerialNumber", string.IsNullOrEmpty(MachineSerialNumber) ? null : MachineSerialNumber, DbType.String);
                DbParameter[] Params = new DbParameter[8] { EquipmentHeaderIDP, DealerIDP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, StatusIDP, MachineSerialNumberP };
                using (DataSet DataSet = provider.Select("GetICTicketStatusReportForIC", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertDeviatedICTicketRequestForApproval", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertDeviatedICTicketRequestForApproval", ex);
                throw ex;
            }
            return null;
        }

        public Boolean InsertDeviatedICTicketRequestForApproval(long ICTicketID, int ICTicketDeviationTypeID, int UserID)
        {

            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter ICTicketDeviationTypeIDP = provider.CreateParameter("ICTicketDeviationTypeID", ICTicketDeviationTypeID, DbType.Int32);
            DbParameter[] Params = new DbParameter[3] { ICTicketIDP, ICTicketDeviationTypeIDP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertDeviatedICTicketRequestForApproval", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertDeviatedICTicketRequestForApproval", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertDeviatedICTicketRequestForApproval", ex);
                return false;
            }
            return true;
        }
        public Boolean ApproveOrRejectDeviatedICTicketRequest(long ICTicketID, Boolean? IsApproved, Boolean? IsRejected, int UserID)
        {
            DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
            DbParameter IsApprovedP = provider.CreateParameter("IsApproved", IsApproved, DbType.Int32);
            DbParameter IsRejectedP = provider.CreateParameter("IsRejected", IsRejected, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[4] { ICTicketIDP, IsApprovedP, IsRejectedP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_ApproveOrRejectDeviatedICTicketRequest", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", "InsertDeviatedICTicketRequestForApproval", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_ICTicket", " InsertDeviatedICTicketRequestForApproval", ex);
                return false;
            }
            return true;
        }
        public List<PDMS_ICTicket> GetDeviatedICTicketForApproval(int? DealerID, string ICTicketNumber, int ICTicketDeviationTypeID, DateTime? RequestedDateF, DateTime? RequestedDateT)
        {
            List<PDMS_ICTicket> Ws = new List<PDMS_ICTicket>();
            PDMS_ICTicket W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
                DbParameter ICTicketDeviationTypeIDP = provider.CreateParameter("ICTicketDeviationTypeID", ICTicketDeviationTypeID, DbType.Int32);
                DbParameter RequestedDateFP = provider.CreateParameter("RequestedDateF", RequestedDateF, DbType.DateTime);
                DbParameter RequestedDateTP = provider.CreateParameter("RequestedDateT", RequestedDateT, DbType.DateTime);

                DbParameter[] Params = new DbParameter[5] { DealerIDP, ICTicketNumberP, ICTicketDeviationTypeIDP, RequestedDateFP, RequestedDateTP };
                using (DataSet DataSet = provider.Select("ZDMS_GetDeviatedICTicketForApproval", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_ICTicket();
                            Ws.Add(W);
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);

                            W.Equipment = new PDMS_EquipmentHeader();
                            W.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["EquipmentModel"]) };


                            if (dr["ServiceTypeID"] != DBNull.Value)
                            {
                                W.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            }
                            if (dr["ServicePriorityID"] != DBNull.Value)
                            {
                                W.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            }

                            W.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            if (dr["ServiceStatusID"] != DBNull.Value)
                            {
                                W.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]), ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            }
                            W.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);
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
        public DataTable GetDeviatedICTicketReport(int? DealerID, string ICTicketNumber, int? ICTicketDeviationTypeID, DateTime? RequestedDateF, DateTime? RequestedDateT, int UserID)
        {
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter RequestedDateFP = provider.CreateParameter("RequestedDateF", RequestedDateF, DbType.DateTime);
                DbParameter RequestedDateTP = provider.CreateParameter("RequestedDateT", RequestedDateT, DbType.DateTime);
                DbParameter ICTicketDeviationTypeIDP = provider.CreateParameter("ICTicketDeviationTypeID", ICTicketDeviationTypeID, DbType.Int32);
                DbParameter[] Params = new DbParameter[6] { DealerIDP, ICTicketNumberP, ICTicketDeviationTypeIDP, RequestedDateFP, RequestedDateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetDeviatedICTicketReport", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }

        public DataSet GetICTicketServiceEngineerUtilisationReport(int? DealerID, string EmployeeCode, DateTime? DateF, DateTime? DateT, int? StatusID, int UserID)
        {

            try
            {
                DbParameter DealerCodeP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter EmployeeCodeP = provider.CreateParameter("EmployeeCode", string.IsNullOrEmpty(EmployeeCode) ? null : EmployeeCode, DbType.String);

                DbParameter ICTicketDateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[6] { DealerCodeP, EmployeeCodeP, ICTicketDateFP, ICTicketDateTP, StatusIDP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketServiceEngineerUtilisationReport", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet;
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public DataSet GetICTicketServiceEngineerUtilisationReportDetails(int? DealerID, string EmployeeCode, DateTime? DateF, DateTime? DateT, int? StatusID, int UserID)
        {

            try
            {
                DbParameter DealerCodeP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter EmployeeCodeP = provider.CreateParameter("EmployeeCode", string.IsNullOrEmpty(EmployeeCode) ? null : EmployeeCode, DbType.String);

                DbParameter ICTicketDateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[6] { DealerCodeP, EmployeeCodeP, ICTicketDateFP, ICTicketDateTP, StatusIDP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetICTicketServiceEngineerUtilisationReportDetails", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet;
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public PApiResult InsertICTicketHmrDeviation(long ICTicketID, int OldHMR, int NewHMR)
        {
            string endPoint = "ICTicket/InsertICTicketHmrDeviation?ICTicketID=" + ICTicketID + "&OldHMR=" + OldHMR + "&NewHMR=" + NewHMR;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

    }
}
