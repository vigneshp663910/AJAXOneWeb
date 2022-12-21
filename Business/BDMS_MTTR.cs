using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Business
{
    public class BDMS_MTTR
    {
        private IDataAccess provider;
        public BDMS_MTTR()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PDMS_MTTR> GetMttr(string filter, Boolean live = false)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_MTTR> Mttrs = new List<PDMS_MTTR>();
            try
            {
                //        string query = "SELECT t.f_ic_ticket_id, t.f_call_login_date, f_call_login_time, problem_reported, " + " status1, status2, reason_for_closure, t.f_cust_id, d_customer_name, "
                //+ " present_mc_city, present_mc_dist, present_mc_state, present_mc_region, mc_slno, mc_desc, warranty_date_start, warranty_date_end, dealer_code, "
                //+ " dealer_name, prior_srv_order, ser_req_date, ser_rec_time, ser_res_date, mttr1, mttr2, ibaseno, compcode, complaintdesc, ser_id, ser_name, "
                //+ "  ser_ord_no, ser_item, code, description1, hmr, hmr_date, fsr_no, breakdown_reason, breakdown_details, internal_notes1, contact_person, "
                //+ "  contact_person_pre, flag, sno, psrno, psrdate, ser_rec_no, r_application, r_hmr_desc ,r_priority_class_desc,r_priority_desc"
                //+" FROM temp_ic t"
                //+ "  inner join dsprr_psc_hdr psc on psc.f_ic_ticket_id = t.f_ic_ticket_id"
                //+ " inner join dsprr_psr_hdr psr on psr.f_ic_ticket_id = t.f_ic_ticket_id "
                //+ " inner join dsprr_psc_counter pscc on pscc.f_counter_ref_id = psc.p_sc_id "
                //+ filter;
                //        DataTable dt = new NpgsqlServer().ExecuteReader(query);
                //    string query = "SELECT  * from pr_getmttr3(" + filter + ")";
                string query = filter;
                List<PDMS_EquipmentHeader> Equipment = new BDMS_Equipment().GetEquipment(null, "");
                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_MTTR Mttr = null;
                string f_ic_ticket_id = "";
                foreach (DataRow dr in dt.Rows)
                {
                    if (f_ic_ticket_id != Convert.ToString(dr["f_ic_ticket_id"]) + Convert.ToString(dr["dealer_code"]) + Convert.ToString(dr["psr_status"]))
                    {
                        f_ic_ticket_id = Convert.ToString(dr["f_ic_ticket_id"]) + Convert.ToString(dr["dealer_code"]) + Convert.ToString(dr["psr_status"]);
                        Mttr = new PDMS_MTTR();
                        Mttr.sno = Convert.ToString(dr["sno"]);
                        Mttr.f_ic_ticket_id = Convert.ToString(dr["f_ic_ticket_id"]);
                        Mttr.f_call_login_date = Convert.ToDateTime(dr["f_call_login_date"]);
                        Mttr.f_call_login_time = Convert.ToString(dr["f_call_login_time"]);
                        Mttr.problem_reported = Convert.ToString(dr["problem_reported"]);
                        Mttr.PsrStatus = Convert.ToString(dr["psr_status"]);
                        Mttr.reason_for_closure = Convert.ToString(dr["reason_for_closure"]);
                        Mttr.f_cust_id = Convert.ToString(dr["f_cust_id"]);
                        Mttr.d_customer_name = Convert.ToString(dr["d_customer_name"]);
                        Mttr.present_mc_city = Convert.ToString(dr["present_mc_city"]);
                        Mttr.present_mc_dist = Convert.ToString(dr["present_mc_dist"]);
                        Mttr.present_mc_state = Convert.ToString(dr["present_mc_state"]);
                        Mttr.present_mc_region = Convert.ToString(dr["present_mc_region"]);
                        Mttr.mc_slno = Convert.ToString(dr["mc_slno"]);
                        Mttr.mc_desc = Convert.ToString(dr["mc_desc"]);
                        if ((string.IsNullOrEmpty(Mttr.mc_desc)) && (!string.IsNullOrEmpty(Mttr.mc_slno)))
                        {
                            var M = (from m in Equipment where m.EquipmentSerialNo == Mttr.mc_slno select m);
                            if (M.Count() == 1)
                            {
                                Mttr.mc_desc = M.ToList()[0].EquipmentModel.ModelDescription;
                            }
                        }
                        Mttr.warranty_date_start = DBNull.Value == dr["warranty_date_start"] ? (DateTime?)null : Convert.ToDateTime(dr["warranty_date_start"]);
                        Mttr.warranty_date_end = DBNull.Value == dr["warranty_date_end"] ? (DateTime?)null : Convert.ToDateTime(dr["warranty_date_end"]);
                        Mttr.dealer_code = Convert.ToString(dr["dealer_code"]);
                        Mttr.dealer_name = Convert.ToString(dr["dealer_name"]);
                        Mttr.prior_srv_order = Convert.ToString(dr["prior_srv_order"]);
                        Mttr.ser_req_date = DBNull.Value == dr["ser_req_date"] ? (DateTime?)null : Convert.ToDateTime(dr["ser_req_date"]);
                        Mttr.ser_rec_time = Convert.ToString(dr["ser_rec_time"]);
                        Mttr.ser_rec_date = DBNull.Value == dr["r_date_of_first_res"] ? (DateTime?)null : Convert.ToDateTime(dr["r_date_of_first_res"]);
                        Mttr.ser_res_date = DBNull.Value == dr["ser_res_date"] ? (DateTime?)null : Convert.ToDateTime(dr["ser_res_date"]);
                        Mttr.iBaseNo = Convert.ToString(dr["iBaseNo"]);
                        Mttr.compcode = Convert.ToString(dr["compcode"]);
                        Mttr.complaintdesc = Convert.ToString(dr["complaintdesc"]);
                        Mttr.ser_ord_no = Convert.ToString(dr["ser_ord_no"]);
                        Mttr.ser_item = Convert.ToString(dr["ser_item"]);
                        Mttr.ser_rec_no = Convert.ToString(dr["ser_rec_no"]);
                        Mttr.ser_id = Convert.ToString(dr["ser_id"]);
                        Mttr.ser_name = Convert.ToString(dr["r_first_name"]);
                        Mttr.f_part_id = Convert.ToString(dr["f_part_id"]);
                        if ("SER 00001" == Mttr.f_part_id)
                        {
                            Mttr.code = "P";
                            Mttr.description1 = "Paid service";
                        }
                        else if ("SER 00002" == Mttr.f_part_id)
                        {
                            Mttr.code = "COM";
                            Mttr.description1 = "Installation and commissioning";
                        }
                        else if ("SER 00003" == Mttr.f_part_id)
                        {
                            Mttr.code = "W";
                            Mttr.description1 = "Warranty service";
                        }
                        else if ("SER 00005" == Mttr.f_part_id)
                        {
                            Mttr.code = "PC";
                            Mttr.description1 = "Pre commissioning";
                        }
                        else if ("SER 00006" == Mttr.f_part_id)
                        {
                            Mttr.code = "AMC";
                            Mttr.description1 = "AMC";
                        }
                        else if ("OS 00009" == Mttr.f_part_id)
                        {
                            Mttr.code = "O";
                            Mttr.description1 = "Others";
                        }
                        else if ("NEPI 00001" == Mttr.f_part_id)
                        {
                            Mttr.code = "NEPI";
                            Mttr.description1 = "NEPI 100 hrs service";
                        }
                        else if ("NEPI 00005" == Mttr.f_part_id)
                        {
                            Mttr.code = "NEPI";
                            Mttr.description1 = "NEPI 500 hrs service";
                        }
                        else if ("NEPI 00010" == Mttr.f_part_id)
                        {
                            Mttr.code = "NEPI";
                            Mttr.description1 = "NEPI 1000 hrs service";
                        }
                        else if ("NEPI 00015" == Mttr.f_part_id)
                        {
                            Mttr.code = "NEPI";
                            Mttr.description1 = "NEPI 1500 hrs service";
                        }
                        Mttr.contact_person = Convert.ToString(dr["contact_person"]);
                        Mttr.contact_person_pre = Convert.ToString(dr["contact_person_pre"]);
                        Mttr.psrdate = DBNull.Value == dr["psrdate"] ? (DateTime?)null : Convert.ToDateTime(dr["psrdate"]);
                        Mttr.flag = Convert.ToString(dr["flag"]);

                        if (Mttr.ser_req_date != null)
                        {
                            Mttr.MTTR1 = Mttr.ser_rec_date == null ? (int?)null : (int)(((DateTime)Mttr.ser_rec_date - (DateTime)Mttr.ser_req_date).TotalDays);
                            Mttr.MTTR2 = Mttr.ser_res_date == null ? (int?)null : (int)(((DateTime)Mttr.ser_res_date - (DateTime)Mttr.ser_req_date).TotalDays);
                        }


                        Mttr.r_application = Convert.ToString(dr["r_application"]);
                        Mttr.r_counter_end = DBNull.Value == dr["r_counter_end"] ? (int?)null : Convert.ToInt32(dr["r_counter_end"]);


                        Mttr.r_priority_class_desc = Convert.ToString(dr["r_priority_class_desc"]);
                        Mttr.r_priority_desc = Convert.ToString(dr["r_priority_desc"]);
                        Mttr.p_sr_id = Convert.ToString(dr["p_sr_id"]);
                        Mttr.f_technician_id = Convert.ToString(dr["f_technician_id"]);

                        if (Mttr.ser_res_date != null)
                        {
                            Mttr.status1 = "Machine restored";
                        }
                        else if (Mttr.ser_rec_date != null)
                        {
                            Mttr.status1 = "SE reached";
                        }
                        else if (!string.IsNullOrEmpty(Mttr.f_technician_id))
                        {
                            Mttr.status1 = "In progress - SE assigned";
                        }
                        else if (!string.IsNullOrEmpty(Mttr.p_sr_id))
                        {
                            Mttr.status1 = "Moved to DMS";
                        }
                        else
                        {
                            Mttr.status1 = "Open";
                        }


                        Mttr.r_tsir_no = Convert.ToString(dr["r_tsir_no"]);
                        Mttrs.Add(Mttr);
                    }
                }
                return Mttrs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_MTTR", "GetMttr", ex);
                // throw ex;
            }
            return Mttrs;
        }
        public List<PDMS_MTTR> GetMttrWithText(string query, Boolean live = false)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_MTTR> Mttrs = new List<PDMS_MTTR>();
            try
            {
                //   string query = "SELECT  * from pr_getmttr3withtext(" + filter + ")";

                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_MTTR Mttr = new PDMS_MTTR();
                string f_ic_ticket_id = "";

                //var dtv =(DataTable) dt.AsEnumerable()
                //       .GroupBy(x => x)
                //       .Select(g => g.First());

                List<string> p_sc_tech_id = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {

                    if (f_ic_ticket_id != Convert.ToString(dr["f_ic_ticket_id"]) + Convert.ToString(dr["dealer_code"]) + Convert.ToString(dr["psr_status"]))
                    {
                        p_sc_tech_id = new List<string>();

                        f_ic_ticket_id = Convert.ToString(dr["f_ic_ticket_id"]) + Convert.ToString(dr["dealer_code"]) + Convert.ToString(dr["psr_status"]);
                        Mttr = new PDMS_MTTR();
                        Mttrs.Add(Mttr);
                        Mttr.sno = Convert.ToString(dr["sno"]);
                        Mttr.f_ic_ticket_id = Convert.ToString(dr["f_ic_ticket_id"]);
                        Mttr.f_call_login_date = Convert.ToDateTime(dr["f_call_login_date"]);
                        Mttr.f_call_login_time = Convert.ToString(dr["f_call_login_time"]);
                        Mttr.problem_reported = Convert.ToString(dr["problem_reported"]);


                        Mttr.PsrStatus = Convert.ToString(dr["psr_status"]);
                        //  Mttr.status2 = Convert.ToString(dr["status2"]);
                        Mttr.reason_for_closure = Convert.ToString(dr["reason_for_closure"]);
                        Mttr.f_cust_id = Convert.ToString(dr["f_cust_id"]);
                        Mttr.d_customer_name = Convert.ToString(dr["d_customer_name"]);
                        Mttr.present_mc_city = Convert.ToString(dr["present_mc_city"]);
                        Mttr.present_mc_dist = Convert.ToString(dr["present_mc_dist"]);
                        Mttr.present_mc_state = Convert.ToString(dr["present_mc_state"]);
                        Mttr.present_mc_region = Convert.ToString(dr["present_mc_region"]);
                        Mttr.mc_slno = Convert.ToString(dr["mc_slno"]);
                        Mttr.mc_desc = Convert.ToString(dr["mc_desc"]);
                        Mttr.warranty_date_start = DBNull.Value == dr["warranty_date_start"] ? (DateTime?)null : Convert.ToDateTime(dr["warranty_date_start"]);
                        Mttr.warranty_date_end = DBNull.Value == dr["warranty_date_end"] ? (DateTime?)null : Convert.ToDateTime(dr["warranty_date_end"]);
                        Mttr.dealer_code = Convert.ToString(dr["dealer_code"]);
                        Mttr.dealer_name = Convert.ToString(dr["dealer_name"]);
                        Mttr.prior_srv_order = Convert.ToString(dr["prior_srv_order"]);
                        Mttr.ser_req_date = DBNull.Value == dr["ser_req_date"] ? (DateTime?)null : Convert.ToDateTime(dr["ser_req_date"]);
                        Mttr.ser_rec_time = Convert.ToString(dr["ser_rec_time"]);
                        Mttr.ser_rec_date = DBNull.Value == dr["r_date_of_first_res"] ? (DateTime?)null : Convert.ToDateTime(dr["r_date_of_first_res"]);
                        Mttr.ser_res_date = DBNull.Value == dr["ser_res_date"] ? (DateTime?)null : Convert.ToDateTime(dr["ser_res_date"]);
                        Mttr.iBaseNo = Convert.ToString(dr["iBaseNo"]);
                        Mttr.compcode = Convert.ToString(dr["compcode"]);
                        Mttr.complaintdesc = Convert.ToString(dr["complaintdesc"]);
                        Mttr.ser_ord_no = Convert.ToString(dr["ser_ord_no"]);
                        Mttr.ser_item = Convert.ToString(dr["ser_item"]);
                        Mttr.ser_rec_no = Convert.ToString(dr["ser_rec_no"]);
                        Mttr.ser_id = Convert.ToString(dr["ser_id"]);
                        Mttr.ser_name = Convert.ToString(dr["r_first_name"]);
                        //Mttr.code = Convert.ToString(dr["code"]);
                        Mttr.f_part_id = Convert.ToString(dr["f_part_id"]);
                        if ("SER 00001" == Mttr.f_part_id)
                        {
                            Mttr.code = "P";
                            Mttr.description1 = "Paid service";
                        }
                        else if ("SER 00002" == Mttr.f_part_id)
                        {
                            Mttr.code = "COM";
                            Mttr.description1 = "Installation and commissioning";
                        }
                        else if ("SER 00003" == Mttr.f_part_id)
                        {
                            Mttr.code = "W";
                            Mttr.description1 = "Warranty service";
                        }
                        else if ("SER 00005" == Mttr.f_part_id)
                        {
                            Mttr.code = "PC";
                            Mttr.description1 = "Pre commissioning";
                        }
                        else if ("SER 00006" == Mttr.f_part_id)
                        {
                            Mttr.code = "AMC";
                            Mttr.description1 = "AMC";
                        }
                        else if ("OS 00009" == Mttr.f_part_id)
                        {
                            Mttr.code = "O";
                            Mttr.description1 = "Others";
                        }
                        else if ("NEPI 00001" == Mttr.f_part_id)
                        {
                            Mttr.code = "NEPI";
                            Mttr.description1 = "NEPI 100 hrs service";
                        }
                        else if ("NEPI 00005" == Mttr.f_part_id)
                        {
                            Mttr.code = "NEPI";
                            Mttr.description1 = "NEPI 500 hrs service";
                        }
                        else if ("NEPI 00010" == Mttr.f_part_id)
                        {
                            Mttr.code = "NEPI";
                            Mttr.description1 = "NEPI 1000 hrs service";
                        }
                        else if ("NEPI 00015" == Mttr.f_part_id)
                        {
                            Mttr.code = "NEPI";
                            Mttr.description1 = "NEPI 1500 hrs service";
                        }

                        //   Mttr.description1 = Convert.ToString(dr["description1"]);
                        Mttr.contact_person = Convert.ToString(dr["contact_person"]);
                        Mttr.contact_person_pre = Convert.ToString(dr["contact_person_pre"]);
                        // Mttr.psro = Convert.ToString(dr["psro"]);
                        Mttr.psrdate = DBNull.Value == dr["psrdate"] ? (DateTime?)null : Convert.ToDateTime(dr["psrdate"]);
                        Mttr.flag = Convert.ToString(dr["flag"]);

                        //  Mttr.MTTR1 = (Mttr.ser_req_date - StartDate).TotalDays;


                        if (Mttr.ser_req_date != null)
                        {
                            Mttr.MTTR1 = Mttr.ser_rec_date == null ? (int?)null : (int)(((DateTime)Mttr.ser_rec_date - (DateTime)Mttr.ser_req_date).TotalDays);
                            Mttr.MTTR2 = Mttr.ser_res_date == null ? (int?)null : (int)(((DateTime)Mttr.ser_res_date - (DateTime)Mttr.ser_req_date).TotalDays);
                        }

                        Mttr.r_application = Convert.ToString(dr["r_application"]);
                        Mttr.r_counter_end = DBNull.Value == dr["r_counter_end"] ? (int?)null : Convert.ToInt32(dr["r_counter_end"]);


                        Mttr.r_priority_class_desc = Convert.ToString(dr["r_priority_class_desc"]);
                        Mttr.r_priority_desc = Convert.ToString(dr["r_priority_desc"]);
                        Mttr.p_sr_id = Convert.ToString(dr["p_sr_id"]);
                        Mttr.f_technician_id = Convert.ToString(dr["f_technician_id"]);

                        if (Mttr.ser_res_date != null)
                        {
                            Mttr.status1 = "Machine restored";
                        }
                        else if (Mttr.ser_rec_date != null)
                        {
                            Mttr.status1 = "SE reached";
                        }
                        else if (!string.IsNullOrEmpty(Mttr.f_technician_id))
                        {
                            Mttr.status1 = "In progress - SE assigned";
                        }
                        else if (!string.IsNullOrEmpty(Mttr.p_sr_id))
                        {
                            Mttr.status1 = "Moved to DMS";
                        }
                        else
                        {
                            Mttr.status1 = "Open";
                        }

                        Mttr.r_tsir_no = Convert.ToString(dr["r_tsir_no"]);
                        Mttr.CustomerSatisfactionLevel = Convert.ToString(dr["Customer_Satisfaction_Level"]);
                        Mttr.Breakdown = new List<PBreakdown>();
                    }
                    Mttr.p_sc_tech_id = Convert.ToString(dr["p_sc_tech_id"]);
                    if (Mttr.MTTR2 != null)
                    {
                        if (Mttr.TotalMandays == null)
                            Mttr.TotalMandays = 0;
                        //if (Mttr.MTTR2 >= Mttr.TotalMandays)
                        //{
                        if (!p_sc_tech_id.Contains(Mttr.p_sc_tech_id))
                        {
                            Mttr.TotalMandays = Mttr.TotalMandays + 1;
                            p_sc_tech_id.Add(Mttr.p_sc_tech_id);
                        }
                        // }
                    }

                    if ((Mttr.Breakdown.Where(m => m.BreakdownDetails.Trim().ToLower() == Convert.ToString(dr["Breakdown_Details"]).Trim().ToLower() && m.BreakdownReason.Trim().ToLower() == Convert.ToString(dr["Breakdown_Reason"]).Trim().ToLower()).Count() == 0))
                    {
                        Mttr.Breakdown.Add(new PBreakdown()
                        {
                            BreakdownNoteType = Convert.ToString(dr["Breakdown_Note_Type"]),
                            BreakdownReason = Convert.ToString(dr["Breakdown_Reason"]),
                            BreakdownDetails = Convert.ToString(dr["Breakdown_Details"])
                        });
                    }
                }
                return Mttrs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_MTTR", "GetMttrWithText", ex);
                throw ex;
            }
            return Mttrs;
        }
        public int GetMttrCount(string filter)
        {
            int count = 0;

            try
            {

                DataTable dt = new NpgsqlServer().ExecuteReader("SELECT  * from pr_getmttr3Count(" + filter + ")");

                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
            }
            return count;
        }
        public List<PDMS_MTTR_New> GetMTTR1(string DealerCode, string CustomerCode, string ICTicketNumber, DateTime? ICTicketDateF, DateTime? ICTicketDateT, int? ServiceStatusID, int UserID)
        {
            List<PDMS_MTTR_New> Ws = new List<PDMS_MTTR_New>();
            PDMS_MTTR_New W = null;
            try
            {
                DbParameter DealerCodeP = provider.CreateParameter("DealerCode", string.IsNullOrEmpty(DealerCode) ? null : DealerCode, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);
                DbParameter ICTicketNumberP = provider.CreateParameter("ICTicketNumber", string.IsNullOrEmpty(ICTicketNumber) ? null : ICTicketNumber, DbType.String);
                DbParameter ICTicketDateFP = provider.CreateParameter("ICTicketDateF", ICTicketDateF, DbType.DateTime);
                DbParameter ICTicketDateTP = provider.CreateParameter("ICTicketDateT", ICTicketDateT, DbType.DateTime);
                DbParameter ServiceStatusIDP = provider.CreateParameter("ServiceStatusID", ServiceStatusID, DbType.Int32);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

                DbParameter[] Params = new DbParameter[7] { DealerCodeP, CustomerCodeP, ICTicketNumberP, ICTicketDateFP, ICTicketDateTP, ServiceStatusIDP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTR", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_MTTR_New();
                            Ws.Add(W);
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);

                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };

                            W.ICTicket.RequestedDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ICTicket.ReachedDate = dr["ReachedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ReachedDate"]);
                            W.ICTicket.RequestedEndDate = dr["RequestedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                            W.ICTicket.RestoreDate = dr["RestoreDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);


                            if (W.ICTicket.RequestedDate != null)
                            {
                                W.MTTR1 = W.ICTicket.ReachedDate == null ? (int?)null : (int)(((DateTime)W.ICTicket.ReachedDate - (DateTime)W.ICTicket.RequestedDate).TotalDays);
                                W.MTTR2 = W.ICTicket.RestoreDate == null ? (int?)null : (int)(((DateTime)W.ICTicket.RestoreDate - (DateTime)W.ICTicket.RequestedDate).TotalDays);

                                W.MTTR1H = W.ICTicket.ReachedDate == null ? (int?)null : (int)(((DateTime)W.ICTicket.ReachedDate - (DateTime)W.ICTicket.RequestedDate).TotalHours);
                                W.MTTR2H = W.ICTicket.RestoreDate == null ? (int?)null : (int)(((DateTime)W.ICTicket.RestoreDate - (DateTime)W.ICTicket.RequestedDate).TotalHours);
                            }

                            W.ICTicket.ServiceStatus = new PDMS_ServiceStatus()
                            {
                                ServiceStatusID = Convert.ToInt32(dr["ServiceStatusID"]),
                                ServiceStatus = Convert.ToString(dr["ServiceStatus"])
                            };


                            //  W.ICTicket.TechnicianAssignedDate = dr["TechnicianAssignedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["TechnicianAssignedDate"]);
                            W.ICTicket.ReqDeclinedDate = dr["ReqDeclinedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ReqDeclinedDate"]);
                            if ((W.ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Open) || (W.ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Reopen))
                            {
                                W.StatusSince = (int)((DateTime.Now - W.ICTicket.ICTicketDate).TotalHours);
                            }
                            else if (W.ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.TechnicianAssigned)
                            {
                                W.StatusSince = (int)((DateTime.Now - (DateTime)W.ICTicket.TechnicianAssignedDate).TotalHours);
                            }
                            else if (W.ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Reached)
                            {
                                W.StatusSince = (int)((DateTime.Now - (DateTime)W.ICTicket.ReachedDate).TotalHours);
                            }
                            else if (W.ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.ReqDeclined)
                            {
                                W.StatusSince = (int)((DateTime.Now - (DateTime)W.ICTicket.ReqDeclinedDate).TotalHours);
                            }

                            W.ICTicket.MainApplication = new PDMS_MainApplication() { MainApplication = Convert.ToString(dr["MainApplication"]) };

                            if (dr["TechnicianID"] != DBNull.Value)
                            {
                                W.ICTicket.Technician = new PUser() { UserName = Convert.ToString(dr["TechnicianID"]), ContactName = Convert.ToString(dr["TechnicianName"]) };
                            }
                            else
                            {
                                W.ICTicket.Technician = new PUser();
                            }

                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);

                            W.ICTicket.CurrentHMRDate = dr["CurrentHMRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CurrentHMRDate"]);
                            W.ICTicket.CurrentHMRValue = dr["CurrentHMRValue"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CurrentHMRValue"]);
                            W.ICTicket.LastHMRDate = dr["LastHMRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["LastHMRDate"]);
                            W.ICTicket.LastHMRValue = dr["LastHMRValue"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["LastHMRValue"]);

                            W.ICTicket.FSRNumber = Convert.ToString(dr["FSRNumber"]);
                            W.ICTicket.FSRDate = dr["FSRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FSRDate"]);// Convert.ToString(dr["FSRDate"]);
                            if (dr["CustomerSatisfactionLevelID"] != DBNull.Value)
                                W.ICTicket.CustomerSatisfactionLevel = new PDMS_CustomerSatisfactionLevel() { CustomerSatisfactionLevelID = Convert.ToInt32(dr["CustomerSatisfactionLevelID"]), CustomerSatisfactionLevel = Convert.ToString(dr["CustomerSatisfactionLevel"]) };
                            W.ICTicket.Address = new PDMS_Address();
                            W.ICTicket.Address.State = new PDMS_State() { State = Convert.ToString(dr["State"]) };
                            W.ICTicket.Address.District = new PDMS_District() { District = Convert.ToString(dr["District"]) };
                            W.ICTicket.Location = Convert.ToString(dr["District"]);
                            W.ICTicket.Material = new PDMS_Material();
                            W.ICTicket.Material.MaterialCode = Convert.ToString(dr["MaterialCode"]);

                            if ("SER 00001" == W.ICTicket.Material.MaterialCode)
                            {
                                W.code = "P";
                                W.description1 = "Paid service";
                            }
                            else if ("SER 00002" == W.ICTicket.Material.MaterialCode)
                            {
                                W.code = "COM";
                                W.description1 = "Installation and commissioning";
                            }
                            else if ("SER 00003" == W.ICTicket.Material.MaterialCode)
                            {
                                W.code = "W";
                                W.description1 = "Warranty service";
                            }
                            else if ("SER 00005" == W.ICTicket.Material.MaterialCode)
                            {
                                W.code = "PC";
                                W.description1 = "Pre commissioning";
                            }
                            else if ("SER 00006" == W.ICTicket.Material.MaterialCode)
                            {
                                W.code = "AMC";
                                W.description1 = "AMC";
                            }
                            else if ("OS 00009" == W.ICTicket.Material.MaterialCode)
                            {
                                W.code = "O";
                                W.description1 = "Others";
                            }
                            else if ("NEPI 00001" == W.ICTicket.Material.MaterialCode)
                            {
                                W.code = "NEPI";
                                W.description1 = "NEPI 100 hrs service";
                            }
                            else if ("NEPI 00005" == W.ICTicket.Material.MaterialCode)
                            {
                                W.code = "NEPI";
                                W.description1 = "NEPI 500 hrs service";
                            }
                            else if ("NEPI 00010" == W.ICTicket.Material.MaterialCode)
                            {
                                W.code = "NEPI";
                                W.description1 = "NEPI 1000 hrs service";
                            }
                            else if ("NEPI 00015" == W.ICTicket.Material.MaterialCode)
                            {
                                W.code = "NEPI";
                                W.description1 = "NEPI 1500 hrs service";
                            }

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
                            // W.ICTicket.Equipment.warrantyStartDate = Convert.ToDateTime(dr["warrantyStartDate"]);

                            W.ICTicket.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                            W.ICTicket.ComplaintCode = Convert.ToString(dr["ComplaintCode"]);


                            if (DBNull.Value != dr["ServiceTypeID"])
                                W.ICTicket.ServiceType = new PDMS_ServiceType() { ServiceTypeID = Convert.ToInt32(dr["ServiceTypeID"]), ServiceType = Convert.ToString(dr["ServiceType"]) };
                            if (DBNull.Value != dr["ICPriorityID"])
                                W.ICTicket.ICPriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ICPriorityID"]), ServicePriority = Convert.ToString(dr["ICPriority"]) };
                            if (DBNull.Value != dr["ServicePriorityID"])
                                W.ICTicket.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(dr["ServicePriorityID"]), ServicePriority = Convert.ToString(dr["ServicePriority"]) };
                            W.ICTicket.ServiceDescription = Convert.ToString(dr["ServiceDescription"]);

                            W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
                            W.ICTicket.IsMarginWarranty = Convert.ToBoolean(dr["IsMarginWarranty"]);


                            W.ICTicket.ServiceRecord = Convert.ToString(dr["ServiceRecord"]);
                            W.ICTicket.RegisteredBy = new PUser();
                            if (dr["RegisteredByID"] != DBNull.Value)
                                W.ICTicket.RegisteredBy = new PUser() { UserID = Convert.ToInt32(dr["RegisteredByID"]), ContactName = Convert.ToString(dr["RegisteredByName"]) };

                            W.TotalMandays = Convert.ToInt32(dr["WorkedDay"]);
                            W.TotalHours = Convert.ToDecimal(dr["WorkedHours"]);
                            W.ICTicket.ScopeOfWork = Convert.ToString(dr["ScopeOfWork"]);
                            W.ICTicket.HillyRegion = dr["HillyRegion"] == DBNull.Value ? false : Convert.ToBoolean(dr["HillyRegion"]);
                            W.ICTicket.MarginRemark = Convert.ToString(dr["MarginRemark"]);
                            //if (W.MTTR2 != null)
                            //{
                            //    if (W.TotalMandays == null)
                            //        W.TotalMandays = 0;
                            //    if (W.MTTR2 >= W.TotalMandays)
                            //    {
                            //        W.TotalMandays = W.TotalMandays + 1;
                            //    }
                            //}
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
        public PApiResult GetMTTR(int? DealerID, string CustomerCode, string ICTicketNumber, string ICTicketDateF, string ICTicketDateT, int? ServiceStatusID, string Division, int? PageIndex = null, int? PageSize = null)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "ICTicket/GetMTTR?DealerID=" + DealerID + "&CustomerCode=" + CustomerCode + "&ICTicketNumber=" + ICTicketNumber + "&ICTicketDateF=" + ICTicketDateF
                + "&ICTicketDateT=" + ICTicketDateT + "&ServiceStatusID=" + ServiceStatusID + "&Division=" + Division + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }

        public DataSet GetMTTREscalationOnBreakdownCount(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {

            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTREscalationOnBreakdownCount", Params))
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
        public List<PDMS_MTTR_New> GetMTTRTeamLeader(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {
            List<PDMS_MTTR_New> Ws = new List<PDMS_MTTR_New>();
            PDMS_MTTR_New W = null;
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTRTeamLeader", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_MTTR_New();
                            Ws.Add(W);
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["Model"]) };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
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
        public List<PDMS_MTTR_New> GetMTTRServiceManagers(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {
            List<PDMS_MTTR_New> Ws = new List<PDMS_MTTR_New>();
            PDMS_MTTR_New W = null;
            try
            {

                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTRServiceManagers", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_MTTR_New();
                            Ws.Add(W);
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["Model"]) };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
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
        public List<PDMS_MTTR_New> GetMTTRReginalServiceManagers(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {
            List<PDMS_MTTR_New> Ws = new List<PDMS_MTTR_New>();
            PDMS_MTTR_New W = null;
            try
            {

                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTRReginalServiceManagers", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_MTTR_New();
                            Ws.Add(W);
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["Model"]) };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
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
        public List<PDMS_MTTR_New> GetMTTRDM(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {
            List<PDMS_MTTR_New> Ws = new List<PDMS_MTTR_New>();
            PDMS_MTTR_New W = null;
            try
            {

                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTRDM", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_MTTR_New();
                            Ws.Add(W);
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["Model"]) };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
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

        public DataSet GetDashCustomerSatisfactionInAfterSalesSupport(int? DealerID, DateTime? DateF, DateTime? DateT, int UserID)
        {

            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.String);
                DbParameter DateFP = provider.CreateParameter("DateF", DateF, DbType.DateTime);
                DbParameter DateTP = provider.CreateParameter("DateT", DateT, DbType.DateTime);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);

                DbParameter[] Params = new DbParameter[4] { DealerIDP, DateFP, DateTP, UserIDP };
                return provider.Select("ZDMS_GetDashCustomerSatisfactionInAfterSalesSupport", Params);

            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return null;
        }
        public int IntegrationICTicket(string DealerCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_MTTR> Mttrs = new List<PDMS_MTTR>();
            try
            {

                string QueryMTTRTest = " SELECT  t.f_ic_ticket_id,t.f_call_login_date,t.f_call_login_time,t.problem_reported,t.reason_for_closure,t.f_cust_id,t.d_customer_name ,  "
         + " t.present_mc_city,t.present_mc_dist,t.present_mc_state,t.present_mc_region,t.mc_slno,t.mc_desc,t.warranty_date_start,t.warranty_date_end,   "
         + " t.dealer_code,t.dealer_name,t.prior_srv_order,psc.r_date_of_req as ser_req_date,t.ser_rec_time,psc.r_closure_date as ser_res_date, t.ibaseno,t.compcode,   "
       + " t.complaintdesc,t.ser_id,t.ser_name,t.ser_ord_no, t.ser_item,t.code, t.description1, pscc.r_counter_end as hmr, pscc.r_counter_read_date as hmr_date,   "
         + " psc.f_sr_id, psc.r_tsir_no,pscn.r_note_type as breakdown_note_type,pscnn.r_description as breakdown_reason,pscn.r_comments as breakdown_Details,t.contact_person,   "
     + " t.contact_person_pre,t.flag,t.sno,t.psrno,t.psrdate,t.ser_rec_no, psc.r_application,pscc.r_counter_end,psr.r_priority_class_desc,r_priority_desc,   "
  + " pscs.f_part_id,e.r_first_name,psc.r_date_of_first_res,psr.p_sr_id,psr.s_status as psr_status,psr.f_technician_id,f.r_value Customer_Satisfaction_Level ,pscs.p_sc_tech_id  "
      + " from  af_ic_tickets t  "
          + " left join dsprr_psc_hdr psc on psc.f_ic_ticket_id = t.f_ic_ticket_id and  psc.s_tenant_id <> 20  "
          + " left join dsprr_psr_hdr psr on psr.f_ic_ticket_id = t.f_ic_ticket_id  and  psr.s_tenant_id <> 20  "
          + " left join dsprr_psc_counter pscc on pscc.f_counter_ref_id = psc.p_sc_id  and  pscc.s_tenant_id <> 20  and pscc.s_modified_by = 'SM_USER'  "
          + " left join dsprr_psc_serv_charge_itms pscs on pscs.p_sc_id= psc.p_sc_id  and  pscs.s_tenant_id <> 20  and pscs.s_modified_by = 'SM_USER'  "
          + " left join dmmer_employee e on e.r_external_emp_id = pscs.f_technician_id  and e.s_tenant_id <> 20    "
          + " left join dsprr_psc_note pscn on pscn.s_tenant_id= psc.s_tenant_id and pscn.s_tenant_id <> 20  and  r_note_type ='ZS04' and pscn.f_sc_id =psc.p_sc_id  "
          + " left join dsp_notes_master pscnn  on pscnn.p_note_type =pscn.r_note_type and pscnn.s_tenant_id=pscn.s_tenant_id  "
          + " left join dsprr_feedback_master f on f.p_key = psc.r_answer_key  ";
                string Filter = " Where 1=1  and dealer_code = '" + DealerCode + "'";
                string groupBy = " group by t.f_ic_ticket_id,t.f_call_login_date,t.f_call_login_time, "
                    + " t.problem_reported,t.reason_for_closure,t.f_cust_id,t.d_customer_name ,t.present_mc_city, t.present_mc_dist, t.present_mc_state, t.present_mc_region, t.mc_slno,  t.mc_desc,  "
                  + " t.warranty_date_start, t.warranty_date_end, t.dealer_code,  t.dealer_name,  t.prior_srv_order, psc.r_date_of_req ,psc.r_closure_date ,t.ibaseno, t.compcode, t.complaintdesc, t.ser_id,  "
                   + " t.ser_name, t.ser_ord_no, t.ser_item,t.code, t.description1, pscc.r_counter_end ,pscc.r_counter_read_date , psc.f_sr_id,  psc.r_tsir_no, t.description1  , "
                       + " t.description1 , t.description1  , t.contact_person, t.contact_person_pre, t.flag,  t.sno, t.psrno,  t.psrdate, t.ser_rec_no , psc.r_application, pscc.r_counter_end  , "
                        + "  psr.r_priority_class_desc, r_priority_desc,  pscs.f_part_id , e.r_first_name, psc.r_date_of_first_res, psr.p_sr_id, psr.s_status  , psr.f_technician_id order by t.f_ic_ticket_id ";

                QueryMTTRTest = QueryMTTRTest + Filter + " order by t.f_ic_ticket_id ";

                DataTable dt = new NpgsqlServer().ExecuteReader(QueryMTTRTest);
                PDMS_MTTR Mttr = new PDMS_MTTR();
                string f_ic_ticket_id = "";



                List<string> p_sc_tech_id = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {

                    if (f_ic_ticket_id != Convert.ToString(dr["f_ic_ticket_id"]))
                    {
                        p_sc_tech_id = new List<string>();

                        f_ic_ticket_id = Convert.ToString(dr["f_ic_ticket_id"]);
                        Mttr = new PDMS_MTTR();
                        Mttrs.Add(Mttr);
                        Mttr.sno = Convert.ToString(dr["sno"]);
                        Mttr.f_ic_ticket_id = Convert.ToString(dr["f_ic_ticket_id"]);
                        Mttr.f_call_login_date = Convert.ToDateTime(dr["f_call_login_date"]);
                        Mttr.f_call_login_time = Convert.ToString(dr["f_call_login_time"]);
                        Mttr.problem_reported = Convert.ToString(dr["problem_reported"]);


                        Mttr.PsrStatus = Convert.ToString(dr["psr_status"]);
                        //  Mttr.status2 = Convert.ToString(dr["status2"]);
                        Mttr.reason_for_closure = Convert.ToString(dr["reason_for_closure"]);
                        Mttr.f_cust_id = Convert.ToString(dr["f_cust_id"]);
                        Mttr.d_customer_name = Convert.ToString(dr["d_customer_name"]);
                        Mttr.present_mc_city = Convert.ToString(dr["present_mc_city"]);
                        Mttr.present_mc_dist = Convert.ToString(dr["present_mc_dist"]);
                        Mttr.present_mc_state = Convert.ToString(dr["present_mc_state"]);
                        Mttr.present_mc_region = Convert.ToString(dr["present_mc_region"]);
                        Mttr.mc_slno = Convert.ToString(dr["mc_slno"]);
                        Mttr.mc_desc = Convert.ToString(dr["mc_desc"]);
                        Mttr.warranty_date_start = DBNull.Value == dr["warranty_date_start"] ? (DateTime?)null : Convert.ToDateTime(dr["warranty_date_start"]);
                        Mttr.warranty_date_end = DBNull.Value == dr["warranty_date_end"] ? (DateTime?)null : Convert.ToDateTime(dr["warranty_date_end"]);
                        Mttr.dealer_code = Convert.ToString(dr["dealer_code"]);
                        Mttr.dealer_name = Convert.ToString(dr["dealer_name"]);
                        Mttr.prior_srv_order = Convert.ToString(dr["prior_srv_order"]);
                        Mttr.ser_req_date = DBNull.Value == dr["ser_req_date"] ? (DateTime?)null : Convert.ToDateTime(dr["ser_req_date"]);
                        Mttr.ser_rec_time = Convert.ToString(dr["ser_rec_time"]);
                        Mttr.ser_rec_date = DBNull.Value == dr["r_date_of_first_res"] ? (DateTime?)null : Convert.ToDateTime(dr["r_date_of_first_res"]);
                        Mttr.ser_res_date = DBNull.Value == dr["ser_res_date"] ? (DateTime?)null : Convert.ToDateTime(dr["ser_res_date"]);
                        Mttr.iBaseNo = Convert.ToString(dr["iBaseNo"]);
                        Mttr.compcode = Convert.ToString(dr["compcode"]);
                        Mttr.complaintdesc = Convert.ToString(dr["complaintdesc"]);
                        Mttr.ser_ord_no = Convert.ToString(dr["ser_ord_no"]);
                        Mttr.ser_item = Convert.ToString(dr["ser_item"]);
                        Mttr.ser_rec_no = Convert.ToString(dr["ser_rec_no"]);
                        Mttr.ser_id = Convert.ToString(dr["ser_id"]);
                        Mttr.ser_name = Convert.ToString(dr["r_first_name"]);
                        //Mttr.code = Convert.ToString(dr["code"]);
                        Mttr.f_part_id = Convert.ToString(dr["f_part_id"]);
                        if ("SER 00001" == Mttr.f_part_id)
                        {
                            Mttr.code = "P";
                            Mttr.description1 = "Paid service";
                        }
                        else if ("SER 00002" == Mttr.f_part_id)
                        {
                            Mttr.code = "COM";
                            Mttr.description1 = "Installation and commissioning";
                        }
                        else if ("SER 00003" == Mttr.f_part_id)
                        {
                            Mttr.code = "W";
                            Mttr.description1 = "Warranty service";
                        }
                        else if ("SER 00005" == Mttr.f_part_id)
                        {
                            Mttr.code = "PC";
                            Mttr.description1 = "Pre commissioning";
                        }
                        else if ("SER 00006" == Mttr.f_part_id)
                        {
                            Mttr.code = "AMC";
                            Mttr.description1 = "AMC";
                        }
                        else if ("OS 00009" == Mttr.f_part_id)
                        {
                            Mttr.code = "O";
                            Mttr.description1 = "Others";
                        }
                        else if ("NEPI 00001" == Mttr.f_part_id)
                        {
                            Mttr.code = "NEPI";
                            Mttr.description1 = "NEPI 100 hrs service";
                        }
                        else if ("NEPI 00005" == Mttr.f_part_id)
                        {
                            Mttr.code = "NEPI";
                            Mttr.description1 = "NEPI 500 hrs service";
                        }
                        else if ("NEPI 00010" == Mttr.f_part_id)
                        {
                            Mttr.code = "NEPI";
                            Mttr.description1 = "NEPI 1000 hrs service";
                        }
                        else if ("NEPI 00015" == Mttr.f_part_id)
                        {
                            Mttr.code = "NEPI";
                            Mttr.description1 = "NEPI 1500 hrs service";
                        }

                        //   Mttr.description1 = Convert.ToString(dr["description1"]);
                        Mttr.contact_person = Convert.ToString(dr["contact_person"]);
                        Mttr.contact_person_pre = Convert.ToString(dr["contact_person_pre"]);
                        // Mttr.psro = Convert.ToString(dr["psro"]);
                        Mttr.psrdate = DBNull.Value == dr["psrdate"] ? (DateTime?)null : Convert.ToDateTime(dr["psrdate"]);
                        Mttr.flag = Convert.ToString(dr["flag"]);

                        //  Mttr.MTTR1 = (Mttr.ser_req_date - StartDate).TotalDays;


                        if (Mttr.ser_req_date != null)
                        {
                            Mttr.MTTR1 = Mttr.ser_rec_date == null ? (int?)null : (int)(((DateTime)Mttr.ser_rec_date - (DateTime)Mttr.ser_req_date).TotalDays);
                            Mttr.MTTR2 = Mttr.ser_res_date == null ? (int?)null : (int)(((DateTime)Mttr.ser_res_date - (DateTime)Mttr.ser_req_date).TotalDays);
                        }

                        Mttr.r_application = Convert.ToString(dr["r_application"]);
                        Mttr.r_counter_end = DBNull.Value == dr["r_counter_end"] ? (int?)null : Convert.ToInt32(dr["r_counter_end"]);


                        Mttr.r_priority_class_desc = Convert.ToString(dr["r_priority_class_desc"]);
                        Mttr.r_priority_desc = Convert.ToString(dr["r_priority_desc"]);
                        Mttr.p_sr_id = Convert.ToString(dr["p_sr_id"]);
                        Mttr.f_technician_id = Convert.ToString(dr["f_technician_id"]);

                        if (Mttr.ser_res_date != null)
                        {
                            Mttr.status1 = "Machine restored";
                        }
                        else if (Mttr.ser_rec_date != null)
                        {
                            Mttr.status1 = "SE reached";
                        }
                        else if (!string.IsNullOrEmpty(Mttr.f_technician_id))
                        {
                            Mttr.status1 = "In progress - SE assigned";
                        }
                        else if (!string.IsNullOrEmpty(Mttr.p_sr_id))
                        {
                            Mttr.status1 = "Moved to DMS";
                        }
                        else
                        {
                            Mttr.status1 = "Open";
                        }

                        Mttr.r_tsir_no = Convert.ToString(dr["r_tsir_no"]);
                        Mttr.CustomerSatisfactionLevel = Convert.ToString(dr["Customer_Satisfaction_Level"]);
                        Mttr.Breakdown = new List<PBreakdown>();
                    }
                    Mttr.p_sc_tech_id = Convert.ToString(dr["p_sc_tech_id"]);
                    if (Mttr.MTTR2 != null)
                    {
                        if (Mttr.TotalMandays == null)
                            Mttr.TotalMandays = 0;
                        //if (Mttr.MTTR2 >= Mttr.TotalMandays)
                        //{
                        if (!p_sc_tech_id.Contains(Mttr.p_sc_tech_id))
                        {
                            Mttr.TotalMandays = Mttr.TotalMandays + 1;
                            p_sc_tech_id.Add(Mttr.p_sc_tech_id);
                        }
                        // }
                    }

                    if ((Mttr.Breakdown.Where(m => m.BreakdownDetails.Trim().ToLower() == Convert.ToString(dr["Breakdown_Details"]).Trim().ToLower() && m.BreakdownReason.Trim().ToLower() == Convert.ToString(dr["Breakdown_Reason"]).Trim().ToLower()).Count() == 0))
                    {
                        Mttr.Breakdown.Add(new PBreakdown()
                        {
                            BreakdownNoteType = Convert.ToString(dr["Breakdown_Note_Type"]),
                            BreakdownReason = Convert.ToString(dr["Breakdown_Reason"]),
                            BreakdownDetails = Convert.ToString(dr["Breakdown_Details"])
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_MTTR", "GetMttrWithText", ex);
                throw ex;
            }


            long WarrantyInvoiceHeaderID = 0;


            foreach (PDMS_MTTR M in Mttrs)
            {

                DbParameter ICTicketNumber = provider.CreateParameter("ICTicketNumber", M.f_ic_ticket_id, DbType.String);
                DbParameter ICTicketDate = provider.CreateParameter("ICTicketDate", M.f_call_login_date, DbType.DateTime);
                DbParameter RequestedDate = provider.CreateParameter("RequestedDate", M.ser_req_date, DbType.DateTime);
                DbParameter ReachedDate = provider.CreateParameter("ReachedDate", M.ser_rec_date, DbType.DateTime);
                DbParameter RestoreDate = provider.CreateParameter("RestoreDate", M.ser_res_date, DbType.DateTime);
                DbParameter CustomerCode = provider.CreateParameter("CustomerCode", M.f_cust_id, DbType.String);
                DbParameter CustomerName = provider.CreateParameter("CustomerName", M.d_customer_name, DbType.String);

                DbParameter DealerCodeP = provider.CreateParameter("DealerCode", M.dealer_code, DbType.String);
                DbParameter DealerName = provider.CreateParameter("DealerName", M.dealer_name, DbType.String);

                DbParameter Location = provider.CreateParameter("Location", M.present_mc_city, DbType.String);
                DbParameter District = provider.CreateParameter("District", M.present_mc_dist, DbType.String);
                DbParameter State = provider.CreateParameter("State", M.present_mc_state, DbType.String);
                //     DbParameter State = provider.CreateParameter("State", M.present_mc_region, DbType.String);
                DbParameter ComplaintDescription = provider.CreateParameter("ComplaintDescription", M.problem_reported, DbType.String);
                DbParameter Model = provider.CreateParameter("Model", M.mc_desc, DbType.String);
                DbParameter EquipmentSerialNo = provider.CreateParameter("EquipmentSerialNo", M.mc_slno, DbType.String);
                DbParameter SerEnggCode = provider.CreateParameter("SerEnggCode", M.ser_name, DbType.String);
                //    DbParameter ContactName = provider.CreateParameter("ContactName", M., DbType.String);
                DbParameter ServicechargeCode = provider.CreateParameter("ServicechargeCode", M.f_part_id, DbType.String);
                DbParameter MaterialCode = provider.CreateParameter("MaterialCode", M.code, DbType.String);
                DbParameter Description = provider.CreateParameter("Description", M.description1, DbType.String);
                DbParameter CurrentHMRValue = provider.CreateParameter("CurrentHMRValue", M.r_counter_end, DbType.String);


                DbParameter ServicePriorityIC = provider.CreateParameter("ServicePriorityIC", M.r_priority_class_desc, DbType.String);
                DbParameter ServicePrioritySR = provider.CreateParameter("ServicePrioritySR", M.r_priority_desc, DbType.String);
                DbParameter MainApplication = provider.CreateParameter("MainApplication", M.r_application, DbType.String);
                DbParameter PresentContactNumber = provider.CreateParameter("PresentContactNumber", M.contact_person_pre, DbType.String);
                DbParameter ContactPerson = provider.CreateParameter("ContactPerson", M.contact_person, DbType.String);
                DbParameter FSRNumber = provider.CreateParameter("FSRNumber", M.r_tsir_no, DbType.String);
                DbParameter CustomerSatisfactionLevel = provider.CreateParameter("CustomerSatisfactionLevel", M.CustomerSatisfactionLevel, DbType.String);
                DbParameter TotalMandays = provider.CreateParameter("TotalMandays", M.TotalMandays, DbType.String);
                //   DbParameter MarginRemark = provider.CreateParameter("MarginRemark", M.dealer_name, DbType.String);
                DbParameter MTTRID = provider.CreateParameter("OutValue", WarrantyInvoiceHeaderID, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                DbParameter[] Params1 = new DbParameter[29] { ICTicketNumber, ICTicketDate, RequestedDate, ReachedDate, RestoreDate, CustomerCode, CustomerName, DealerCodeP,
                    DealerName,Location,District,State,ComplaintDescription,Model,EquipmentSerialNo,SerEnggCode,ServicechargeCode,MaterialCode,Description,CurrentHMRValue
                    ,ServicePriorityIC,ServicePrioritySR,MainApplication,PresentContactNumber,ContactPerson,FSRNumber,CustomerSatisfactionLevel,TotalMandays,MTTRID};
                try
                {
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        provider.Insert("ZDMS_InsertOrUpdateMTTRFromMavan", Params1); ;


                        foreach (PBreakdown B in M.Breakdown)
                        {
                            DbParameter MTTRIDP = provider.CreateParameter("MTTRID", Convert.ToInt64(MTTRID.Value), DbType.Int64);
                            //DbParameter Warranty = provider.CreateParameter("Warranty", Customer.r_date_warr_expiry, DbType.String);
                            DbParameter NoteType = provider.CreateParameter("NoteType", B.BreakdownReason, DbType.String);
                            DbParameter Comments = provider.CreateParameter("Comments", B.BreakdownDetails, DbType.String);
                            DbParameter[] Params = new DbParameter[3] { MTTRIDP, NoteType, Comments };
                            provider.Insert("ZDMS_InsertOrUpdateMTTRNoteFromMavan", Params); ;

                        }
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    new FileLogger().LogMessageService("BDMS_Material", "IntegrationMaterial", ex);
                    throw ex;
                }
            }
            TraceLogger.Log(DateTime.Now);


            return 0;
        }

        public List<PDMS_ServiceNote> GetMTTRNote(long? ICTicketID, long? ServiceNoteID, int? NoteTypeID, string NoteType)
        {
            List<PDMS_ServiceNote> ServiceMaterials = new List<PDMS_ServiceNote>();
            try
            {


                DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);


                DbParameter[] Params = new DbParameter[1] { ICTicketIDP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTRNote", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            ServiceMaterials.Add(new PDMS_ServiceNote()
                            {

                                ICTicketID = Convert.ToInt64(dr["ICTicketID"]),
                                Comments = Convert.ToString(dr["Comments"]),
                                NoteType = new PDMS_NoteType()
                                {
                                    NoteType = Convert.ToString(dr["NoteType"])
                                }
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

        class MttrEscalationMatrix
        {
            public int EscalationMatrixID { get; set; }
            public string Region { get; set; }
            public int RepresentativeUserID { get; set; }
            public string Subject { get; set; }
            public string Description { get; set; }
            public List<string> ToMailID { get; set; }
            public List<string> CcMailID { get; set; }
            public List<string> BccMailID { get; set; }
            public string EscalationHours { get; set; }
            
        }
        public void SendMailMttrEscalationMatrix()
        {
            List<MttrEscalationMatrix> EMs = GetMttrEscalationMatrix();

            foreach(MttrEscalationMatrix Em in EMs)
            {

                string Message = Body(Em.EscalationHours, Em.RepresentativeUserID, Em.Description);
                if (string.IsNullOrEmpty(Message))
                {
                    continue;
                }
                new EmailManager().MailSendByService(Em.ToMailID, Em.Subject, Message, Em.CcMailID, Em.BccMailID);
            }
        }
        private List<MttrEscalationMatrix> GetMttrEscalationMatrix()
        {
            List<MttrEscalationMatrix> Ws = new List<MttrEscalationMatrix>();
            MttrEscalationMatrix W = null;
            try
            {
                using (DataSet DataSet = provider.Select("ZDMS_GetMttrEscalationMatrix"))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new MttrEscalationMatrix();
                            Ws.Add(W);
                            W.EscalationMatrixID = Convert.ToInt32(dr["EscalationMatrixID"]);
                            W.Region = Convert.ToString(dr["Region"]);
                            W.RepresentativeUserID = Convert.ToInt32(dr["RepresentativeUserID"]);
                            W.Subject = Convert.ToString(dr["Subject"]);
                            W.Description = Convert.ToString(dr["Description"]);
                            W.ToMailID = dr["ToMailID"] == DBNull.Value ? new List<string>() : Convert.ToString(dr["ToMailID"]).Split(',').ToList();
                            W.CcMailID = dr["CcMailID"] == DBNull.Value ? new List<string>() : Convert.ToString(dr["CcMailID"]).Split(',').ToList();
                            W.BccMailID = dr["BccMailID"] == DBNull.Value ? new List<string>() : Convert.ToString(dr["BccMailID"]).Split(',').ToList();
                            W.EscalationHours = Convert.ToString(dr["EscalationHours"]);
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

        private string Body(string EscalationHours, int UserID, string Description)
        {            
            List<PDMS_MTTR_New> MTTRs = new List<PDMS_MTTR_New>();
            string Message = "";

            if (EscalationHours == "74")
                MTTRs = new BDMS_MTTR().GetMTTRDM(null, null, null, UserID);
            else if (EscalationHours == "48")
                MTTRs = new BDMS_MTTR().GetMTTRReginalServiceManagers(null, null, null, UserID);
            else if (EscalationHours == "24")
                MTTRs = new BDMS_MTTR().GetMTTRServiceManagers(null, null, null, UserID);
            else if (EscalationHours == "24 BasedOnModels")
                MTTRs = new BDMS_MTTR().GetMTTRBasedOnModels();

            if (MTTRs.Count == 0)
            {
                return null;
            }         

            string Top = "<!DOCTYPE html><html><head><title></title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"></head>"
                         + "<body><div style=\"max-width: 1500px; margin:auto\"><form><div><p><span>Good Morning!</span></p><p>@@Description<br /></p>"
                          + "<p>-	Under Warranty : @@UnderWarranty <br /></p><p>-	Out of Warranty : @@OutOfWarranty <br /></p>";

            Top = Top.Replace("@@Description", Description);

            string RowTH = "<th style=\"background-color: lightblue;color: white; padding: 10px; text-align:center\">@@RowTH</th>";
            string RowTD = "<td style=\"background-color: #eee;color: black;padding: 3px; text-align:center;border: 1px solid lightblue\">@@RowTD</td>";
            string Header1 = "<table style=\" border: 1px solid lightblue;  border-collapse: collapse; width: 100%;\" ><thead><tr >";

            int i = 0;
            int UnderWarranty = 0;
            int OutOfWarranty = 0;
            string Header = Header1
                + RowTH.Replace("@@RowTH", "Serial Number")
                + RowTH.Replace("@@RowTH", "IC Ticket")
                + RowTH.Replace("@@RowTH", "IC Ticket Date")
                + RowTH.Replace("@@RowTH", "Status")
                + RowTH.Replace("@@RowTH", "Customer")
                + RowTH.Replace("@@RowTH", "Customer Name")
                + RowTH.Replace("@@RowTH", "Contact Name")
                + RowTH.Replace("@@RowTH", "Contact Number")
                + RowTH.Replace("@@RowTH", "Dealer")
                + RowTH.Replace("@@RowTH", "Dealer Name")
                + RowTH.Replace("@@RowTH", "Model")
                + RowTH.Replace("@@RowTH", "Serial No")
                + RowTH.Replace("@@RowTH", "Warranty Status")
                + "</tr></thead><tbody>";
            string Row = "";
            foreach (PDMS_MTTR_New MTTR in MTTRs)
            {
                i = i + 1;
                if (MTTR.ICTicket.IsWarranty == true)
                {
                    UnderWarranty = UnderWarranty + 1;
                }
                else
                {
                    OutOfWarranty = OutOfWarranty + 1;
                }
                Row = Row + " <tr>"
                    + RowTD.Replace("@@RowTD", i.ToString())
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.ICTicketNumber)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.ICTicketDate.ToShortDateString())
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.ServiceStatus.ServiceStatus)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Customer.CustomerCode)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Customer.CustomerName)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.ContactPerson)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.PresentContactNumber)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Dealer.DealerCode)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Dealer.DealerName)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Equipment.EquipmentModel.Model)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.Equipment.EquipmentSerialNo)
                    + RowTD.Replace("@@RowTD", MTTR.ICTicket.IsWarranty == true ? "Yes" : "No")
                    + "</tr>";
            }
            string Bottom = "</tbody></table><p>Thank You !</p></div></form></div></body></html>";

            Top = Top.Replace("@@UnderWarranty", UnderWarranty.ToString());
            Top = Top.Replace("@@OutOfWarranty", OutOfWarranty.ToString());
            Message = Top + Header + Row + Bottom;

            return Message;
        }

        public List<PDMS_MTTR_New> GetMTTRBasedOnModels()
        {
            List<PDMS_MTTR_New> Ws = new List<PDMS_MTTR_New>();
            PDMS_MTTR_New W = null;
            try
            {
                //    DbParameter ModelP = provider.CreateParameter("Model", Model, DbType.Int32);
                //    DbParameter[] Params = new DbParameter[1] { ModelP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMTTRBasedOnModels"))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            W = new PDMS_MTTR_New();
                            Ws.Add(W);
                            W.ICTicket = new PDMS_ICTicket();
                            W.ICTicketID = Convert.ToInt64(dr["ICTicketID"]);
                            W.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                            W.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                            W.ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                            W.ICTicket.PresentContactNumber = Convert.ToString(dr["PresentContactNumber"]);
                            W.ICTicket.ServiceStatus = new PDMS_ServiceStatus() { ServiceStatus = Convert.ToString(dr["ServiceStatus"]) };
                            W.ICTicket.Equipment = new PDMS_EquipmentHeader();
                            W.ICTicket.Equipment.EquipmentModel = new PDMS_Model() { Model = Convert.ToString(dr["Model"]) };
                            W.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);
                            W.ICTicket.IsWarranty = Convert.ToBoolean(dr["IsWarranty"]);
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

        //public void MailEscalationMoreThan72Hrs()
        //{

        //    string[] EscalationMttr74To = ConfigurationManager.AppSettings["EscalationMttr74To"].Split(',');
        //    string[] EscalationMttr74Cc = ConfigurationManager.AppSettings["EscalationMttr74Cc"].Split(',');

        //    List<string> MailCcS = new List<string>();
        //    foreach (string MailCc in EscalationMttr74Cc)
        //    {
        //        if (!string.IsNullOrEmpty(MailCc))
        //        {
        //            MailCcS.Add(MailCc);
        //        }
        //    }

        //    foreach (string UserIDs in EscalationMttr74To)
        //    {
        //        PUser User = new BUser().GetUsers(Convert.ToInt32(UserIDs), "", null, "")[0];
        //        List<PDMS_MTTR_New> MTTRs = new BDMS_MTTR().GetMTTRDM(null, null, null, User.UserID);
        //        if (MTTRs.Count == 0)
        //        {
        //            continue;
        //        }
        //        List<string> ToMails = new List<string>();
        //        ToMails.Add(User.Mail);
        //        string Message = "";
        //        string Subject = "Escalation - Service Tickets More than 72 Hrs";

        //        string Top = "<!DOCTYPE html><html><head><title></title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"></head>"
        //                     + "<body><div style=\"max-width: 1500px; margin:auto\"><form><div><p><span>Good Morning!</span></p><p>@@Description<br /></p>"
        //                      + "<p>-	Under Warranty : @@UnderWarranty <br /></p><p>-	Out of Warranty : @@OutOfWarranty <br /></p>";

        //        Top = Top.Replace("@@Description", "List of calls –  machine not restored - more than 72 hrs");

        //        string RowTH = "<th style=\"background-color: lightblue;color: white; padding: 10px; text-align:center\">@@RowTH</th>";
        //        string RowTD = "<td style=\"background-color: #eee;color: black;padding: 3px; text-align:center;border: 1px solid lightblue\">@@RowTD</td>";

        //        string Header1 = "<table style=\" border: 1px solid lightblue;  border-collapse: collapse; width: 100%;\" ><thead><tr >";

        //        int i = 0;
        //        int UnderWarranty = 0;
        //        int OutOfWarranty = 0;
        //        string Header = Header1
        //            + RowTH.Replace("@@RowTH", "Serial Number")
        //            + RowTH.Replace("@@RowTH", "IC Ticket")
        //            + RowTH.Replace("@@RowTH", "IC Ticket Date")
        //            + RowTH.Replace("@@RowTH", "Status")
        //            + RowTH.Replace("@@RowTH", "Customer")
        //            + RowTH.Replace("@@RowTH", "Customer Name")
        //            + RowTH.Replace("@@RowTH", "Contact Name")
        //            + RowTH.Replace("@@RowTH", "Contact Number")
        //            + RowTH.Replace("@@RowTH", "Dealer")
        //            + RowTH.Replace("@@RowTH", "Dealer Name")
        //            + RowTH.Replace("@@RowTH", "Model")
        //            + RowTH.Replace("@@RowTH", "Serial No")
        //            + RowTH.Replace("@@RowTH", "Warranty Status")
        //            + "</tr></thead><tbody>";
        //        string Row = "";
        //        foreach (PDMS_MTTR_New MTTR in MTTRs)
        //        {
        //            i = i + 1;
        //            if (MTTR.ICTicket.IsWarranty == true)
        //            {
        //                UnderWarranty = UnderWarranty + 1;
        //            }
        //            else
        //            {
        //                OutOfWarranty = OutOfWarranty + 1;
        //            }
        //            Row = Row + " <tr>"
        //                + RowTD.Replace("@@RowTD", i.ToString())
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.ICTicketNumber)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.ICTicketDate.ToShortDateString())
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.ServiceStatus.ServiceStatus)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Customer.CustomerCode)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Customer.CustomerName)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.ContactPerson)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.PresentContactNumber)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Dealer.DealerCode)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Dealer.DealerName)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Equipment.EquipmentModel.Model)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Equipment.EquipmentSerialNo)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.IsWarranty == true ? "Yes" : "No")
        //                + "</tr>";
        //        }
        //        string Bottom = "</tbody></table><p>Thank You !</p></div></form></div></body></html>";

        //        Top = Top.Replace("@@UnderWarranty", UnderWarranty.ToString());
        //        Top = Top.Replace("@@OutOfWarranty", OutOfWarranty.ToString());

        //        Message = Top + Header + Row + Bottom;
        //        List<string> MailBccS = new List<string>();
        //        MailBccS.Add("john.peter@ajax-engg.com");
        //        new EmailManager().MailSendByService(ToMails, Subject, Message, MailCcS, MailBccS);
        //    }
        //}
        //public void MailEscalationMoreThan48Hrs()
        //{

        //    string[] EscalationMttr48To = ConfigurationManager.AppSettings["EscalationMttr48To"].Split(',');
        //    string[] EscalationMttr48Cc = ConfigurationManager.AppSettings["EscalationMttr48Cc"].Split(',');

        //    List<string> MailCcS = new List<string>();
        //    foreach (string MailCc in EscalationMttr48Cc)
        //    {
        //        if (!string.IsNullOrEmpty(MailCc))
        //        {
        //            MailCcS.Add(MailCc);
        //        }
        //    }

        //    foreach (string UserIDs in EscalationMttr48To)
        //    {
        //        PUser User = new BUser().GetUsers(Convert.ToInt32(UserIDs), "", null, "")[0];
        //        List<PDMS_MTTR_New> MTTRs = new BDMS_MTTR().GetMTTRReginalServiceManagers(null, null, null, User.UserID);
        //        if (MTTRs.Count == 0)
        //        {
        //            continue;
        //        }
        //        List<string> ToMails = new List<string>();
        //        ToMails.Add(User.Mail);
        //        string Message = "";
        //        string Subject = "Escalation - Service Tickets More than 48 Hrs";

        //        string Top = "<!DOCTYPE html><html><head><title></title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"></head>"
        //                     + "<body><div style=\"max-width: 1500px; margin:auto\"><form><div><p><span>Good Morning!</span></p><p>@@Description<br /><br /></p> "
        //                     + "<p>-	Under Warranty : @@UnderWarranty <br /></p><p>-	Out of Warranty : @@OutOfWarranty <br /></p>";

        //        Top = Top.Replace("@@Description", "List of calls –  machine not restored - more than 48 hrs");

        //        string RowTH = "<th style=\"background-color: lightblue;color: white; padding: 10px; text-align:center\">@@RowTH</th>";
        //        string RowTD = "<td style=\"background-color: #eee;color: black;padding: 3px; text-align:center;border: 1px solid lightblue\">@@RowTD</td>";

        //        string Header1 = "<table style=\" border: 1px solid lightblue;  border-collapse: collapse; width: 100%;\" ><thead><tr >";
        //        int i = 0;
        //        int UnderWarranty = 0;
        //        int OutOfWarranty = 0;
        //        string Header = Header1
        //            + RowTH.Replace("@@RowTH", "Serial Number")
        //            + RowTH.Replace("@@RowTH", "IC Ticket")
        //            + RowTH.Replace("@@RowTH", "IC Ticket Date")
        //            + RowTH.Replace("@@RowTH", "Status")
        //            + RowTH.Replace("@@RowTH", "Customer")
        //            + RowTH.Replace("@@RowTH", "Customer Name")
        //            + RowTH.Replace("@@RowTH", "Contact Name")
        //            + RowTH.Replace("@@RowTH", "Contact Number")
        //            + RowTH.Replace("@@RowTH", "Dealer")
        //            + RowTH.Replace("@@RowTH", "Dealer Name")
        //            + RowTH.Replace("@@RowTH", "Model")
        //            + RowTH.Replace("@@RowTH", "Serial No")
        //            + RowTH.Replace("@@RowTH", "Warranty Status")
        //            + "</tr></thead><tbody>";
        //        string Row = "";
        //        foreach (PDMS_MTTR_New MTTR in MTTRs)
        //        {
        //            i = i + 1;
        //            if (MTTR.ICTicket.IsWarranty == true)
        //            {
        //                UnderWarranty = UnderWarranty + 1;
        //            }
        //            else
        //            {
        //                OutOfWarranty = OutOfWarranty + 1;
        //            }
        //            Row = Row + " <tr>"
        //                + RowTD.Replace("@@RowTD", i.ToString())
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.ICTicketNumber)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.ICTicketDate.ToShortDateString())
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.ServiceStatus.ServiceStatus)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Customer.CustomerCode)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Customer.CustomerName)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.ContactPerson)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.PresentContactNumber)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Dealer.DealerCode)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Dealer.DealerName)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Equipment.EquipmentModel.Model)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Equipment.EquipmentSerialNo)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.IsWarranty == true ? "Yes" : "No")
        //                + "</tr>";
        //        }
        //        string Bottom = "</tbody></table><p>Thank You !</p></div></form></div></body></html>";

        //        Top = Top.Replace("@@UnderWarranty", UnderWarranty.ToString());
        //        Top = Top.Replace("@@OutOfWarranty", OutOfWarranty.ToString());

        //        Message = Top + Header + Row + Bottom;
        //        List<string> MailBccS = new List<string>();
        //        MailBccS.Add("john.peter@ajax-engg.com");
        //        new EmailManager().MailSendByService(ToMails, Subject, Message, MailCcS, MailBccS);
        //    }
        //}
        //public void MailEscalationMoreThan24Hrs()
        //{

        //    string[] EscalationMttr24To = ConfigurationManager.AppSettings["EscalationMttr24To"].Split(',');

        //    foreach (string UserIDs in EscalationMttr24To)
        //    {
        //        PUser User = new BUser().GetUsers(Convert.ToInt32(UserIDs), "", null, "")[0];
        //        List<PDMS_MTTR_New> MTTRs = new BDMS_MTTR().GetMTTRServiceManagers(null, null, null, User.UserID);
        //        if (MTTRs.Count == 0)
        //        {
        //            continue;
        //        }
        //        List<string> ToMails = new List<string>();
        //        ToMails.Add(User.Mail);
        //        string Message = "";
        //        string Subject = "Escalation - Service Tickets More than 24 Hrs";

        //        string Top = "<!DOCTYPE html><html><head><title></title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"></head>"
        //                     + "<body><div style=\"max-width: 1500px; margin:auto\"><form><div><p><span>Good Morning!</span></p><p>@@Description<br /></p>"
        //                     + "<p>-	Under Warranty : @@UnderWarranty <br /></p><p>-	Out of Warranty : @@OutOfWarranty <br /></p>";

        //        Top = Top.Replace("@@Description", "List of calls –  machine not restored - more than 24 hrs");

        //        string RowTH = "<th style=\"background-color: lightblue;color: white; padding: 10px; text-align:center\">@@RowTH</th>";
        //        string RowTD = "<td style=\"background-color: #eee;color: black;padding: 3px; text-align:center;border: 1px solid lightblue\">@@RowTD</td>";

        //        string Header1 = "<table style=\" border: 1px solid lightblue;  border-collapse: collapse; width: 100%;\" ><thead><tr>";
        //        int i = 0;
        //        int UnderWarranty = 0;
        //        int OutOfWarranty = 0;

        //        string Header = Header1
        //            + RowTH.Replace("@@RowTH", "Serial Number")
        //            + RowTH.Replace("@@RowTH", "IC Ticket")
        //            + RowTH.Replace("@@RowTH", "IC Ticket Date")
        //            + RowTH.Replace("@@RowTH", "Status")
        //            + RowTH.Replace("@@RowTH", "Customer")
        //            + RowTH.Replace("@@RowTH", "Customer Name")
        //            + RowTH.Replace("@@RowTH", "Contact Name")
        //            + RowTH.Replace("@@RowTH", "Contact Number")
        //            + RowTH.Replace("@@RowTH", "Dealer")
        //            + RowTH.Replace("@@RowTH", "Dealer Name")
        //            + RowTH.Replace("@@RowTH", "Model")
        //            + RowTH.Replace("@@RowTH", "Serial No")
        //            + RowTH.Replace("@@RowTH", "Warranty Status")
        //            + "</tr></thead><tbody>";
        //        string Row = "";
        //        foreach (PDMS_MTTR_New MTTR in MTTRs)
        //        {
        //            i = i + 1;
        //            if (MTTR.ICTicket.IsWarranty == true)
        //            {
        //                UnderWarranty = UnderWarranty + 1;
        //            }
        //            else
        //            {
        //                OutOfWarranty = OutOfWarranty + 1;
        //            }

        //            Row = Row + " <tr>"
        //                + RowTD.Replace("@@RowTD", i.ToString())
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.ICTicketNumber)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.ICTicketDate.ToShortDateString())
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.ServiceStatus.ServiceStatus)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Customer.CustomerCode)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Customer.CustomerName)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.ContactPerson)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.PresentContactNumber)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Dealer.DealerCode)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Dealer.DealerName)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Equipment.EquipmentModel.Model)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.Equipment.EquipmentSerialNo)
        //                + RowTD.Replace("@@RowTD", MTTR.ICTicket.IsWarranty == true ? "Yes" : "No")
        //                + "</tr>";
        //        }
        //        string Bottom = "</tbody></table><p>Thank You !</p></div></form></div></body></html>";

        //        Top = Top.Replace("@@UnderWarranty", UnderWarranty.ToString());
        //        Top = Top.Replace("@@OutOfWarranty", OutOfWarranty.ToString());

        //        Message = Top + Header + Row + Bottom;
        //        List<string> MailBccS = new List<string>();
        //        MailBccS.Add("john.peter@ajax-engg.com");
        //        new EmailManager().MailSendByService(ToMails, Subject, Message, null, MailBccS);
        //    }
        //}
    }
}
